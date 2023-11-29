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

namespace Library
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("LibraryContextConnection") ?? throw new InvalidOperationException("Connection string 'LibraryContextConnection' not found.");

            builder.Services.AddDbContext<ApplicationContext>();

            builder.Services.AddDefaultIdentity<LibraryUser>(/*options => options.SignIn.RequireConfirmedAccount = false*/)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            builder.Services.AddControllersWithViews();


            var app = builder.Build();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }

}