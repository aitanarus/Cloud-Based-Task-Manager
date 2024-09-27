using BlazorWASM;
using BlazorWASM.Config;
using BlazorWASM.HttpClients;
using BlazorWASM.HttpClients.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Load configuration from appsettings.json
builder.Services.Configure<ApiEndpoints>(builder.Configuration.GetSection("ApiEndpoints"));

// Add named HttpClient for TaskService based on configuration
builder.Services.AddHttpClient("TaskService", (sp, client) =>
{
    var endpoints = sp.GetRequiredService<IOptions<ApiEndpoints>>().Value;
    client.BaseAddress = new Uri(endpoints.TaskService);
});

builder.Services.AddScoped<ITaskService, TaskHttpClient>();

await builder.Build().RunAsync();
