using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Southwind.Presentation.Web.Hubs
{
    public class TestClient
    {
        private IConfiguration configuration;

        public TestClient(IConfiguration config)
        {
            configuration = config;
        }

        public void Run()
        {
            RunAsync();
        }
        private async void RunAsync()
        {
            await Task.Delay(3000);

            var url = configuration.GetValue<string>("serviceUrl");
            var connection = new HubConnectionBuilder()
                .WithUrl(Path.Combine(url, "notification"))
                .WithConsoleLogger()
                .Build();

            connection.On<string>("Send", data =>
            {
                Console.WriteLine($"Received: {data}");
            });

            await connection.StartAsync();

            await connection.InvokeAsync("Send", "Hello from Test-Client");
        }
    }
}
