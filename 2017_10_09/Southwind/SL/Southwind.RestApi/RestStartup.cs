using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Southwind.BusinessObjects;
using Southwind.Contracts.Interfaces;
using Southwind.Data;
using Southwind.Data.Repositories;
using System;
using System.Data.Entity;
using System.Text;

namespace Southwind.RestApi
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
            services.AddLogging();

            services.AddMvc();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Jwt";
                options.DefaultChallengeScheme = "Jwt";
            }).AddJwtBearer("Jwt", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the secret that needs to be at least 16 characeters long for HmacSha256")),
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });

            services.AddScoped<IShopService, ShopService>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<DbContext, SouthwindDb>(CreateContext);
        }

        private SouthwindDb CreateContext(IServiceProvider sp)
        {
            string connection = Configuration.GetSection("ConnectionStrings")["SouthwindConnection"];
            var result = new SouthwindDb(connection);
            ILogger logger = sp.GetRequiredService<ILogger<DbContext>>();
            result.Configuration.LazyLoadingEnabled = false;
            result.Database.Log = s=>logger.LogInformation(s);
            return result;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMiddleware<SimpleAuthentication>();
            app.UseAuthentication();

            app.UseMvc();
        }
    }

}
