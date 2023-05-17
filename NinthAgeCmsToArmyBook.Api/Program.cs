using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using NinthAgeCmsToArmyBook.Api.Latex;
using NinthAgeCmsToArmyBook.Api.MongoDb;
using NinthAgeCmsToArmyBook.Shared.ArmyBooks;
using NinthAgeCmsToArmyBook.Shared.Changes;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    var executingAssembly = Assembly.GetExecutingAssembly().GetName().Name?.Replace(".", "-");
    configuration.Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithCorrelationId()
        .Enrich.WithAssemblyName()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(Environment.GetEnvironmentVariable("ELASTIC_URL")!))
        {
            ModifyConnectionSettings = x => x.BasicAuthentication("elastic", Environment.GetEnvironmentVariable("ELASTIC_SECRET")),
            IndexFormat = $"{executingAssembly}-logs-{DateTime.UtcNow:yyyy-MM}",
            AutoRegisterTemplate = true,
            NumberOfShards = 2,
            NumberOfReplicas = 1
        })
        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
        .ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ArmyRepository>();
builder.Services.AddTransient<ChangeManager>();
builder.Services.AddTransient<TexTransformer>();

builder.Services.AddSingleton(_ =>
{
    var mongoConnectionString = Environment.GetEnvironmentVariable("MONGO_DB_CONNECTION_STRING");
    return new MongoClient(mongoConnectionString);
});

var latexExecutablePath = Environment.GetEnvironmentVariable("LATEX_EXECUTABLE_PATH");
builder.Services.AddSingleton(new LatexConfiguration(latexExecutablePath));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();