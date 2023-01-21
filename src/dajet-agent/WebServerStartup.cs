using DaJet.Agent.Service.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DaJet.Agent.Service
{
    public sealed class WebServerStartup
    {
        public WebServerStartup() { }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddHsts((options) =>
            //{
            //    options.MaxAge = TimeSpan.FromDays(30)
            //});
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            services.AddSingleton<AuthenticationProvider>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseDefaultFiles(); // index.html
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}