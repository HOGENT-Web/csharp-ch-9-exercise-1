using Append.Blazor.Sidepanel;
using Client.Infrastructure;
using Client.Ordering;
using Client.Products;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, FakeAuthenticationProvider>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSidepanel();
            builder.Services.AddHttpClient<StorageService>();
            builder.Services.AddScoped<Cart>();
            builder.Services.AddClientsideProductServices();
            await builder.Build().RunAsync();
        }
    }
}
