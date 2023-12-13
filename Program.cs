using Library.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using Library.Models.Account;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

            builder.Services.AddControllersWithViews();


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