using Library.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AuthorManager _authorManager;

        public AuthorController(AuthorManager authorManager)
        {
            _authorManager = authorManager;
        }

        [HttpGet]
        public IActionResult AddNewAuthor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNewAuthor(string firstName, string lastName, string? pseudonym)
        {
            _authorManager.AddAuthor(firstName, lastName, pseudonym);
            return RedirectToAction("AddBook", "Book");
        }
    }
}
