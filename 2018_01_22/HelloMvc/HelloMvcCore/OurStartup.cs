using Microsoft.AspNetCore.Builder;

namespace HelloMvcCore
{
    public class OurStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
        }

        public void ConfigureDevelopment(IApplicationBuilder app)
        {
            System.Console.WriteLine("In development");
            app.UseStaticFiles();
        }

        public void ConfigureProduction(IApplicationBuilder app)
        {
            System.Console.WriteLine("In production");
            app.UseStaticFiles();
        }

        public void ConfigureStaging()
        {
        }

        public void ConfigureQATests()
        {
        }
    }
}