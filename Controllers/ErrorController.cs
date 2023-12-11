using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
