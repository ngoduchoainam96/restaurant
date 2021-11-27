using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Data.SqlClient;
using razor08.efcore.Data;
using razor09.efcore.Data;
using Album.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using razor08.efcore.Models;
using Album.Models;


namespace _02.middleware
{
public class Startup {
    IServiceCollection  _services;
    IConfiguration _configuration;
    public Startup (IConfiguration configuration) {
        _configuration = configuration;
    }
    public IConfiguration Configuration { get; }
   
    public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FrontMiddleware>();
            services.AddDistributedMemoryCache();       
            services.AddSession(); 
            services.AddSingleton<IListProductName, PhoneName>();   
            services.AddTransient<LaptopName, LaptopName>();  
            services.AddTransient<ProductController, ProductController>(); 
            services.AddOptions (); 
              var mailsettings = _configuration.GetSection ("MailSettings");  
        services.Configure<MailSettings> (mailsettings); 
            services.AddTransient<IEmailSender, SendMailService>();  
            services.AddDistributedMemoryCache();           
        services.AddSession(cfg => {                    
            cfg.Cookie.Name = "xuanthulab";             
            cfg.IdleTimeout = new TimeSpan(0,60, 0);    
         }); 
                                                    
            services.Configure<RouteOptions> (options => {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddDbContext<ArticleContext>(options => {
                string connectstring = Configuration.GetConnectionString("ArticleContext");
                options.UseSqlServer("Data Source=localhost,1433; Initial Catalog=articledb; User ID=SA;Password=Password123");
            });
             services.AddDbContext<AppDbContext>(options => {
        // Đọc chuỗi kết nối
        string connectstring = Configuration.GetConnectionString("AppDbContext");
        // Sử dụng MS SQL Server
        options.UseSqlServer("Data Source=localhost,1433; Initial Catalog=albumdb; User ID=SA;Password=Password123");
    });
    services.AddDbContext<MenuContext>(options=> {
                string connectstring = Configuration.GetConnectionString("MenuContext");
                options.UseSqlServer("Data Source=localhost,1433; Initial Catalog=menudb; User ID=SA;Password=Password123");
            });
        
            services.AddIdentity<AppUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();
        services.Configure<IdentityOptions> (options => {
            
    // Thiết lập về Password
    options.Password.RequireDigit = false; 
    options.Password.RequireLowercase = false; 
    options.Password.RequireNonAlphanumeric = false; 
    options.Password.RequireUppercase = false; 
    options.Password.RequiredLength = 3; 
    options.Password.RequiredUniqueChars = 1; 

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes (2); 
    options.Lockout.MaxFailedAccessAttempts = 3; 
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = 
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;  

    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = false;            
    options.SignIn.RequireConfirmedPhoneNumber = false;     

});

services.ConfigureApplicationCookie (options => {
    
    options.LoginPath = $"/login/";
    options.LogoutPath = $"/logout/";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

            services.AddRazorPages().
            AddRazorPagesOptions(options => {

             
             options.Conventions.AddPageRoute(
                 "/Privacy",
                 "/chinh-sach.html"
             );

             
             options.Conventions.AddAreaPageRoute(
                 areaName : "Product",
                 pageName : "/Detail",
                 route : "/sanpham/{nameproduct?}"
                 );
         });;
            _services = services;    
        }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }       app.UseHttpsRedirection();   
            app.UseRouting();
            app.UseAuthentication();   
app.UseAuthorization (); 
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseSession();
            app.UseCheckAccess();  
            app.UseEndpoints(endpoints =>
                { 
                    endpoints.MapGet("/home", async context => {
                        string html = @"
                        <!DOCTYPE html>
                        <html>
                        <head>
                            <meta charset=""UTF-8"">
                            <title>Trang chủ</title>
                            <link rel=""stylesheet"" href=""/css/site.css"" />
                               <link rel=""stylesheet"" href=""/css/site2.css"" />
                               <link rel=""stylesheet"" href=""/css/site3.css"" />
                            <link rel=""stylesheet"" href=""/css/bootstrap.min.css"" />
                            <link rel=""stylesheet"" href=""https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css"">
                             <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"">
                            <link rel=""stylesheet"" type=""text/css"" href=""//cdn.jsdelivr.net/npm/slick-carousel@1.8.1/slick/slick.css""/>
                            <script src=""/js/jquery.min.js""></script>
                            <script src=""/js/popper.min.js""></script>
                            <script src=""/js/bootstrap.min.js""></script>
                        </head>
                        <body>
                        <div class=""body"">
                        <div class=""header"">
                        <marquee>Chào mừng bạn đến website của NHÀ HÀNG HOÀI NAM</marquee>
                        </div>
                        <div class=""middle_header"">
                            <img src=""./restaurant.png"" class=""restaurant_img""/>
                            <span class=""address"">Số 51 Trần Bình, Cầu Giấy, Hà Nội</span>
                            <span class = ""vertical""></span>
                            <span class=""phone"">0342162027</span>
                            <span class = ""vertical2""></span>
                            <span class=""hours"">Giờ mở cửa: 10h - 24h </span>
                        </div>
                        </div>
                        <div class=""bar"">
                         <nav class=""navbar navbar-expand-lg navbar-dark bg-primary"">
                            <div class=""collapse navbar-collapse"" id=""my-nav-bar"">
                            <ul class=""navbar-nav"">
                                <li class=""nav-item active"">
                                    <a class=""nav-link"" href=""#""><div class=""home_button"">TRANG CHỦ</div></a>
                                </li>
                            
                                <li class=""nav-item"">
                    <div class=""btn-group"">
   <button class=""btn btn-primary dropdown-toggle""
      type=""button""
      id=""dropdownMenuButton"" data-toggle=""dropdown"">
   DỊCH VỤ
   </button>
   <div class=""dropdown-menu"">
      <a class=""dropdown-item"" href=""/html/table.html"">ĐẶT BÀN</a>
      <a class=""dropdown-item"" href=""/html/dish.html"">ĐẶT MÓN</a>
      <div class=""dropdown-divider""></div>
      <a class=""dropdown-item"" href=""/html/order.html"">ĐẶT TOÀN BỘ</a>
   </div>
</div>
                                </li>
                        </ul>
                        </div>
                    </nav> 
                    </div>
                    </div>
                     <div class=""autoplay"">
                    <div><img class=""img"" src=""anh1.jpg"" /></div>
                    <div><img class=""img"" src=""anh2.jpg"" /></div>
                    <div><img class=""img"" src=""anh3.jpg"" /></div>
                </div>
                         <script src=""https://code.jquery.com/jquery-3.3.1.slim.min.js""></script>
      <script src=""https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js""></script>
      <script src=""https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js""></script>
                <script type=""text/javascript"" src=""//cdn.jsdelivr.net/npm/slick-carousel@1.8.1/slick/slick.min.js""></script>
       
                   <script>
        $('.autoplay').slick(
            {
 slidesToScroll: 1,
  autoplay: true,
  autoplaySpeed: 2000,
  prevArrow:""<button type='button' class='slick-prev pull-left'><i class='fa fa-angle-left' aria-hidden='true'></i></button>"",
            nextArrow:""<button type='button' class='button2' ><i class='fa fa-angle-right' aria-hidden='true'></i></button>""
}
        );
    </script> 
                        </body>
                        </html>
        "; 
        
        await context.Response.WriteAsync(html);
    });
                    endpoints.Map("/RequestInfo", async context => {
                    string menu = HtmlHelper.MenuTop (HtmlHelper.DefaultMenuTopItems (), context.Request);
        string requestinfo = RequestProcess.RequestInfo (context.Request).HtmlTag ("div", "container");
        string html = HtmlHelper.HtmlDocument ("Thông tin Request", (menu + requestinfo));
        await context.Response.WriteAsync (html);
                });
                    endpoints.MapGet("/Encoding", async context => {
                    string menu = HtmlHelper.MenuTop (HtmlHelper.DefaultMenuTopItems (), context.Request);
        string htmlec = RequestProcess.Encoding (context.Request).HtmlTag ("div", "container");
        string html = HtmlHelper.HtmlDocument ("Encoding", (menu + htmlec));
        await context.Response.WriteAsync (html);
                });
                    endpoints.MapGet("/Cookies/{*action}", async context => {
                    string menu     = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);
        string cookies  = RequestProcess.Cookies(context.Request, context.Response).HtmlTag("div", "container");
        string html    = HtmlHelper.HtmlDocument("Đọc / Ghi Cookies", (menu + cookies));
        await context.Response.WriteAsync(html);
                });
                endpoints.MapGet("/testmail", async context => {

        // Lấy dịch vụ sendmailservice
        var sendmailservice = context.RequestServices.GetService<ISendMailService>();

        MailContent content = new MailContent {
            To = "xuanthulab.net@gmail.com",
            Subject = "Kiểm tra thử",
            Body = "<p><strong>Xin chào xuanthulab.net</strong></p>"
        };

        await sendmailservice.SendMail(content);
        await context.Response.WriteAsync("Send mail");
    });
  
    endpoints.MapRazorPages();
});
app.Map("/Json", app => {
                app.Run(async context => {
                    string Json  = RequestProcess.GetJson();
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(Json);
                });
            });
  app.Map("/Form", app => {
                app.Run(async context => {
                    // code ở đây
                    string menu = HtmlHelper.MenuTop (HtmlHelper.DefaultMenuTopItems (), context.Request);
        string formhtml = await RequestProcess.FormProcess (context.Request);
        formhtml = formhtml.HtmlTag ("div", "container");
        string html = HtmlHelper.HtmlDocument ("Form Post", (menu + formhtml));
        await context.Response.WriteAsync (html);
                });
            });
            app.Map("/Product", app => {
    app.Run(async (context) => {
        var productcontroller = app.ApplicationServices.GetService<ProductController>();
        await productcontroller.List(context);
    });
});
app.MapWhen (
                (context) => {
                    return context.Request.Path.Value.StartsWith ("/Abcxyz");
                },

                appProduct => {
                    appProduct.Run (async (context) => {
                        await appProduct.ApplicationServices.GetService<ProductController> ().List (context);
                    });
                });
                app.Map("/RequestInfo", app01 => {
    app01.Run(async (context) => {
        string menu         = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);
        string requestinfo  = RequestProcess.RequestInfo(context.Request).HtmlTag("div", "container");

        string accessinfo  = ProductController.CountAccessInfo(context).HtmlTag("div", "container");

        string html         = HtmlHelper.HtmlDocument("Thông tin Request", (menu + accessinfo + requestinfo));
        await context.Response.WriteAsync(html);
    });
});

 app.Run(async context  => {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync("Không tìm thấy trang");
            });


}
}
}
