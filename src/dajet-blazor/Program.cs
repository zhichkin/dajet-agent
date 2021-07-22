using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DaJet.Agent.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            #region "View models"
            builder.Services.AddTransient<INodeViewModel, NodeViewModel>();
            builder.Services.AddScoped<INodeListViewModel, NodeListViewModel>();
            builder.Services.AddTransient<IPublicationViewModel, PublicationViewModel>();
            builder.Services.AddTransient<IPublicationListViewModel, PublicationListViewModel>();
            #endregion

            await builder.Build().RunAsync();
        }
    }
}