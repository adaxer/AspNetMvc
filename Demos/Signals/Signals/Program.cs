using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Signals
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StartClient();
            BuildWebHost(args).Run();
        }

        private static async void StartClient()
        {
            await Task.Delay(10000);

            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:64861/chat")
                .WithConsoleLogger()
                .Build();

            connection.On<string>("Send", data =>
            {
                Console.WriteLine($"Received: {data}");
            });

            await connection.StartAsync();

            await connection.InvokeAsync("Send", "Hello");
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
