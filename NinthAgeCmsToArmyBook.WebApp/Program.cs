using System;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NinthAgeCmsToArmyBook.Api;
using NinthAgeCmsToArmyBook.WebApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
jsonSerializerOptions.Converters.Add(new ObjectIdConverter());
builder.Services.AddSingleton(jsonSerializerOptions);

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
});

await builder.Build().RunAsync();