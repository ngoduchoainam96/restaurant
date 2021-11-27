using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using razor08.efcore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using razor08.efcore.Data;

namespace _02.middleware
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Build -  tạo các dịch vụ đã đăng ký trả về WebHost
            // Run - chạy ứng dụng web
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope()) {
        InsertTestArticle.InsertArticle(scope.ServiceProvider);
    }


    host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                      .UseKestrel(options => {
                            options.Limits.MaxRequestBodySize = 104857600; // 100 MB - Cho phép upload
                      });
                });
    }
}
