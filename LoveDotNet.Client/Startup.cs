using Blazor.FileReader;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LoveDotNet.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup user state
            services.AddScoped<UserState>();
            services.AddScoped<BlogState>();
            services.AddFileReaderService();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
