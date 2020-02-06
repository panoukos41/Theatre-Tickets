using Blazored.LocalStorage;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TG.Blazor.IndexedDB;
using TheatreTickets.Client.Utilities;

namespace TheatreTickets.Client
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            // Create builder and add services.
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            AddServices(builder.Services);

            // Add app.
            builder.RootComponents.Add<App>("app");

            // Build the host and configure services.
            var host = builder.Build();
            Configure(host);

            // Start the app.
            await host.RunAsync();
        }

        public static void AddServices(IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            services.UseRepos();
        }

        public static void Configure(WebAssemblyHost host)
        {
            host.Services.GetService<IndexedDBManager>().ActionCompleted += ActionCompletedAddSampleData;

            void ActionCompletedAddSampleData(object sender, IndexedDBNotificationArgs e)
            {
                var db = (IndexedDBManager)sender;
                db.ActionCompleted -= ActionCompletedAddSampleData;

                host.Services.AddSampleData();
            }
        }        
    }
}