using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RCLSharedComponents.Models;

namespace MVCAppGoogle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Repository.SetExternalUsersData(new List<User>
            {
                new User(1, AuthenticationProvider.Google, "103774181139922043800")
            });
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
