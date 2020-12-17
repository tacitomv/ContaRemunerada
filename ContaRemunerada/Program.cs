using System;
using System.Linq;
using System.Threading.Tasks;
using ContaRemunerada.Data;
using ContaRemunerada.Jobs;
using ContaRemunerada.Services;
using FluentScheduler;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContaRemunerada
{
    public class Program
    {
        public static IServiceProvider ServiceProvider;
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                ServiceProvider = scope.ServiceProvider;
                var context = ServiceProvider.GetRequiredService<AppDbContext>();

                DbInitializer.Initialize(context);

                //JobManager.Initialize(new YieldRegistry());
            }

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
