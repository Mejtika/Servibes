using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coravel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Servibes.Appointments.Application;
using Servibes.Appointments.Infrastructure;
using Servibes.Appointments.Infrastructure.Tasks;

namespace Servibes.Bootstrapper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Services.UseScheduler(scheduler =>
            {
                scheduler.Schedule<AppointmentFinisher>()
                    .EveryMinute();

                scheduler.Schedule<AppointmentFinisher>()
                    .EveryFiveMinutes();
            });
                
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
