using System.Reflection;
using MongoDB.Driver;
using NinthAgeCmsToArmyBook.ArmyBooks;
using NinthAgeCmsToArmyBook.Changes;
using NinthAgeCmsToArmyBook.Latex;
using Serilog;
using Serilog.Debugging;
using Serilog.Sinks.Elasticsearch;

SelfLog.Enable(Console.Error);
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
{
    var executingAssembly = Assembly.GetExecutingAssembly().GetName().Name?.Replace(".", "-");
    var hostEnvironment = context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-");
    configuration.Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithCorrelationId()
        .Enrich.WithAssemblyName()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("https://elasticsearch.modmoto.dev/"))
        {
            // ModifyConnectionSettings = x => x.BasicAuthentication("elastic", "old pw"),
            IndexFormat = $"{executingAssembly}-logs-{hostEnvironment}-{DateTime.UtcNow:yyyy-MM}",
            AutoRegisterTemplate = true,
            NumberOfShards = 2,
            NumberOfReplicas = 1
        })
        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
        .ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<ArmyRepository>();
builder.Services.AddTransient<ChangeManager>();
builder.Services.AddTransient<LatexRepository>();
builder.Services.AddTransient<TexTransformer>();

builder.Services.AddSingleton(_ =>
{
    var mongoConnectionString = Environment.GetEnvironmentVariable("MONGO_DB_CONNECTION_STRING");
    return new MongoClient(mongoConnectionString);
});

var latexExecutablePath = Environment.GetEnvironmentVariable("LATEX_EXECUTABLE_PATH");
builder.Services.AddSingleton(new LatexConfiguration(latexExecutablePath));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();