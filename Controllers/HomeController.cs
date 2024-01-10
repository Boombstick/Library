using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult StartPage()
        {
            //if (User.Identity.IsAuthenticated)
            //{
                
            //    int readerId = int.Parse(s: User.Claims.Where(x => x.Type == "readerId").Select(x => x.Value).FirstOrDefault());
            //    return RedirectToAction("ShowReader","Reader",new { readerId });
            //}
            return View();
        }
    }
}
