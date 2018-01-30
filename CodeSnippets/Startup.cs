using Autofac;
using Autofac.Configuration;
using Autofac.Integration.WebApi;
using Owin;
using Southwind.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Southwind.WebApiHost
{
	public class Startup
	{
		// This code configures Web API. The Startup class is specified as a type
		// parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
		{
			// Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
			config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
           );
            config.MapHttpAttributeRoutes();

		   //config.EnableCors();
            appBuilder.UseCors(new Microsoft.Owin.Cors.CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context =>
                        {
                            var result = new CorsPolicy();
                            result.AllowAnyOrigin = true;
                            result.AllowAnyHeader = true;
                            result.AllowAnyMethod = true;
                            return Task.FromResult(result);
                        }
                }
            });

            InitializeIoC(config);
            appBuilder.UseWebApi(config);
        }

        private void InitializeIoC(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();                                    // Create the container builder.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());         // Register the Web API controllers.
            builder.RegisterModule(new ConfigurationSettingsReader());
            //builder.RegisterInstance<string>("Name=nw").Named<string>("connection");
            //builder.Register<Func<ISouthwindDb>>(c => c.ResolveNamed<Func<ISouthwindDb>>("OnlineConnection"));
            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);           // Create an assign a dependency resolver for Web API to use.
            config.DependencyResolver = resolver;         // Configure Web API with the dependency resolver

        }

    }
}
