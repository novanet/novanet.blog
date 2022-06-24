using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace UseForwardedHeadersExample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor |
                    ForwardedHeaders.XForwardedProto |
                    ForwardedHeaders.XForwardedHost,
                RequireHeaderSymmetry = true,
                ForwardLimit = 3,
                KnownProxies = {
                    IPAddress.Parse("70.70.70.70"),
                    IPAddress.Parse("80.80.80.80")
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}