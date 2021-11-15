using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Utils;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(DateConvertors.GetDateTimeEstoniaNow());


            Console.WriteLine(DateTime.Now);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}