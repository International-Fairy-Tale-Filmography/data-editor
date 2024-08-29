using DataEditor.Core.Configuration;
using DataEditor.Core.Services;
using DataEditor.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddSingleton<GitService>();
builder.Services.AddSingleton<CoreSettingsModel>(i => new CoreSettingsModel()
{
    Branch = "features/devtests",
    Folder = "csv",
    Owner = "International-Fairy-Tale-Filmography",
    RepoName = "data"
});
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
