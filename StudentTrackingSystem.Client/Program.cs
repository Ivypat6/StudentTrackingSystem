using Google.Apis.Analytics.v3;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StudentTrackingSystem.Client;
using StudentTrackingSystem.Service.Services;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args); 
builder.RootComponents.Add<App>("#app"); 
builder.RootComponents.Add<HeadOutlet>("head::after"); 
 
builder.Services.AddScoped(sp => new HttpClient  
    { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }); 
 
// Add auth services 
builder.Services.AddOidcAuthentication(options =>{ 
    builder.Configuration.Bind("Local", options.ProviderOptions); 
    options.ProviderOptions.DefaultScopes.Add("email"); 
    options.ProviderOptions.DefaultScopes.Add("profile"); 
    
options.ProviderOptions.DefaultScopes.Add("StudentTrackingSystem.API")
 ; 
}); 
 
// Add HTTP client for API with auth 
builder.Services.AddHttpClient("ServerAPI",  
        client => client.BaseAddress = new 
Uri(builder.HostEnvironment.BaseAddress)) 
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>(); 
 
builder.Services.AddScoped(sp => 
sp.GetRequiredService<IHttpClientFactory>() 
    .CreateClient("ServerAPI")); 
 
// Add services 
builder.Services.AddScoped<IStudentService, StudentService>(); 
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>(); 
 
// Add MudBlazor (optional for UI components) 
builder.Services.AddMudServices(); 
 
await builder.Build().RunAsync();