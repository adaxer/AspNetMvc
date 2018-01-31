using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Southwind.BusinessObjects;
using Southwind.Contracts.Interfaces;
using Southwind.Data;
using Southwind.Data.Repositories;
using Southwind.Presentation.Common;
using Southwind.Presentation.Web.Hubs;
using System;
using System.Data.Entity;

namespace Southwind.Presentation.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddLogging();
            services.AddSignalR();

            services.AddScoped<IRestService, RestService>();
            services.AddScoped<IShopService, RestShopService>((sp)=>
                new RestShopService(sp.GetRequiredService<IRestService>(), Configuration["serviceUrl"]));
            services.AddScoped<IAuthenticationClient>((sp) => new AuthenticationClient(new LoginData { LoginUrl = "http://localhost:52222/api/token", UserName = "test", Password = "test" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSignalR(hrb => hrb.MapHub<NotificationHub>("notification"));
            new TestClient(Configuration).Run();
        }
    }

}
