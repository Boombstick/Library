using Library.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Library.helper_classes
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<LibraryUser, IdentityRole>
    {
        public AppClaimsPrincipalFactory(
            UserManager<LibraryUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
        { }

        public async override Task<ClaimsPrincipal> CreateAsync(LibraryUser user)
        {
            var principal = await base.CreateAsync(user);

            if (user.Reader_Id > 0)
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim("readerId", user.Reader_Id.ToString())
            });
            }

            return principal;
        }
    }
}
