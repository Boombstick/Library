using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Library.Models.Account;
using Library.helper_classes;
using Library.Abstractions;

namespace Library
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("LibraryContextConnection") ?? throw new InvalidOperationException("Connection string 'LibraryContextConnection' not found.");

            builder.Services.AddDbContext<ApplicationContext>();


            builder.Services.AddDefaultIdentity<LibraryUser>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();
            builder.Services.AddScoped<IUserClaimsPrincipalFactory<LibraryUser>, AppClaimsPrincipalFactory>();

            builder.Services.AddScoped(typeof(BookManager), typeof(BookManager));
            builder.Services.AddScoped(typeof(ReaderManager), typeof(ReaderManager));
            builder.Services.AddScoped(typeof(DataManager), typeof(DataManager));
            builder.Services.AddScoped(typeof(AuthorManager), typeof(AuthorManager));

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost";
                options.InstanceName = "local";
            });
            builder.Services.AddControllersWithViews().AddNewtonsoftJson();


            builder.Services.ConfigureApplicationCookie(options => {
                options.AccessDeniedPath = "/Error/AccessDenied";
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                //options.ReturnUrlParameter = "";
            });
            var app = builder.Build();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=StartPage}/{id?}");

            app.Run();
        }
    }

}