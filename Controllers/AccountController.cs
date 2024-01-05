using Library.Models;
using Library.Models.Account;
using Library.Models.Readers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<LibraryUser> _userManager;
        private readonly SignInManager<LibraryUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationContext _db;
        private const string _startPage = "StartPage";
        private const string _homeController = "Home";

        private const string _userRole = "User";

        public AccountController(SignInManager<LibraryUser> signInManager, UserManager<LibraryUser> userManager, ApplicationContext applicationContext, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _db = applicationContext;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            LibraryUser user = new LibraryUser { UserName = model.Name, Email = model.Email};
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                Reader reader = new Reader { FirstName = model.FirstName, LastName = model.LastName };
                _db.Readers.Add(reader);
                var liss = await _db.Readers.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                user.Reader_Id = liss.Id+1;
                await _userManager.AddToRoleAsync(user, _userRole);
                await _signInManager.SignInAsync(user, false);
                await _db.SaveChangesAsync();
                return RedirectToAction(_startPage, _homeController);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnurl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnurl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Login);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsRemember, false);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            ViewBag.Id = user.Id;
                            return RedirectToAction(_startPage, _homeController);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Неправильный логин или пароль");
                    }
                }
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(_startPage, _homeController);
        }


    }
}
