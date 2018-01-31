using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Southwind.BusinessObjects;
using Southwind.DataAccess;
using Southwind.Interfaces;
using Southwind.Models;
using Southwind.RestApi.Middleware;

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

            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
                opt.SerializerSettings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
                opt.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                opt.SerializerSettings.ObjectCreationHandling = ObjectCreationHandling.Auto;
            });

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

            services.AddSignalR();

            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IRepository<Category>, GenericRepository<Category>>();
            services.AddScoped<DbContext, SouthwindDb>(CreateContext);
        }

        private SouthwindDb CreateContext(IServiceProvider sp)
        {
            string connectionString = Configuration.GetSection("ConnectionStrings")["NorthwindConnection"];
            var db = new SouthwindDb(connectionString);
            ILogger logger = sp.GetRequiredService<ILogger<DbContext>>();
            db.Database.Log = s=>logger.LogInformation(s);
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            return db;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCultureAdaption();

            app.UseAuthentication();

            app.UseIdentityAdaption();

            app.UseMvc();

            app.UseSignalR(routes => routes.MapHub<ActivityLog>("activities"));
            new TestClient(Configuration).Run();
        }
    }
}
