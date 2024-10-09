using BlazorWASM;
using BlazorWASM.Config;
using BlazorWASM.HttpClients;
using BlazorWASM.HttpClients.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazorBootstrap();

// Configure HttpClient
builder.Services.Configure<ApiEndpointsOptions>(builder.Configuration.GetSection(ApiEndpointsOptions.ApiEndpoints));

builder.Services.AddHttpClient("TaskService", (sp, client) =>
{
    var endpoints = sp.GetRequiredService<IOptions<ApiEndpointsOptions>>().Value;
    client.BaseAddress = new Uri(endpoints.TaskService);
});

builder.Services.AddScoped<ITaskService, TaskHttpClient>();


await builder.Build().RunAsync();
