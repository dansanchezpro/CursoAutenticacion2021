using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RCLSharedComponents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAppTodo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Repository.SetExternalUsersData(new List<User>
            {
                new User(1, AuthenticationProvider.Google, "103774181139922043800"),
                new User(2, AuthenticationProvider.Facebook, "499354781055002"),
                new User(3, AuthenticationProvider.Microsoft, "f95d1d2d7ebaef3b"),
                new User(4, AuthenticationProvider.Twitter, "1287473944178688000")
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
