using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using LilyAbramSite.Prismic.Services;
using LilyAbramSite.Prismic.Models;
using System.Net.Http;

namespace LilyAbramSite.Wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient<HttpClient>();
            builder.Services.AddMemoryCache();
            builder.Services.AddPrismic();
            builder.Services.AddSingleton<IPrismicService, PrismicService>();

            builder.Services.AddScoped(async services =>
            {
                var prismic = services.GetRequiredService<IPrismicService>();
                return await prismic.GetGlobals();
            });

            await builder.Build().RunAsync();
        }
    }
}
