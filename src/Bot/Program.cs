using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();


            //using (IServiceScope scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    try
            //    {
            //        var db = services.GetRequiredService<ApplicationContext>();

            //        AppUser user1 = new AppUser { Username = "Tom"};
            //        AppUser user2 = new AppUser { Username = "Alice"};

            //        db.AppUsers.Add(user1);
            //        db.AppUsers.Add(user2);
            //        Console.WriteLine("Objects are created");

            //        var success = db.SaveChangesAsync().GetAwaiter().GetResult();
            //        Console.WriteLine("Objects are saved successfully!");

            //        var users = db.AppUsers.ToList();
            //        Console.WriteLine("----- Users list ------ :");
            //        foreach (var u in users)
            //        {
            //            Console.WriteLine($"Id: {u.Id}, Username: {u.Username}");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"DATABASE ERROR! {ex}");
            //    }
            //}
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}