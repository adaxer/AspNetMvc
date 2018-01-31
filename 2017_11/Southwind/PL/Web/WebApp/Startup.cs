using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Southwind.Contracts.Interfaces;
using Southwind.Logic.BusinessLogic;
using Southwind.Data.Repositories;
using System.Data.Entity;
using Microsoft.Extensions.Logging;
using Southwind.Presentation.Common;

namespace WebApp
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

            string serverUrl = Configuration["ServerUrl"];
            services.AddScoped<IShopService, RestShopService>(sp =>
                new RestShopService(sp.GetRequiredService<IRestService>(), serverUrl));
            services.AddScoped<IRestService, RestService>();
            services.AddScoped<IAuthenticationClient, AuthenticationClient>(sp => 
                new AuthenticationClient(
                    new LoginData { LoginUrl = $"{serverUrl}/token", UserName = "test", Password = "test" }));
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

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
