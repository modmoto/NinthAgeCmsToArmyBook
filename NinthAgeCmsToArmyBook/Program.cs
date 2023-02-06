using MongoDB.Driver;
using NinthAgeCmsToArmyBook.ArmyBooks;
using NinthAgeCmsToArmyBook.Changes;
using NinthAgeCmsToArmyBook.Latex;

var builder = WebApplication.CreateBuilder(args);

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