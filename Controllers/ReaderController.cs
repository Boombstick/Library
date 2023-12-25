using Library.Models;
using Library.Models.Account;
using Library.Models.Books;
using Library.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Library.Controllers
{

    public class ReaderController : Controller
    {
        private readonly ApplicationContext db;
        private readonly UserManager<LibraryUser> _userManager;  
        IEnumerable<Reader> readers = new List<Reader>();
        IEnumerable<Book> books = new List<Book>();

        public ReaderController(ApplicationContext context, UserManager<LibraryUser> userManager)
        {
            db = context;
            _userManager = userManager;
            readers = db.Readers.Select(x => x);
            books = db.Books.Select(x => x);
        }


        public IActionResult Index()
        {
            List<Reader> list = new List<Reader>();
            list = db.Readers.Include(x => x.Books).ToList();
            return View(list);

        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteReader(int? id)
        {
            if (id != null)
            {
                Reader reader = db.Readers.Include(x => x.Books).FirstOrDefault(x => x.Id == id);
                if (reader != null)
                {
                    reader.Books.Clear();
                    db.Remove(reader);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

            }

            return RedirectToAction("Index");
        }
        public IActionResult AddBookToReaderByAdmin()
        {

            ViewBag.Readers = new SelectList(readers, "Id", "FullName");
            ViewBag.Books = new SelectList(books, "Id", "Name");


            return View();
        }
        public async Task<IActionResult> AddBookToReader(int readerId, int? bookId)
        {
            if (readerId != null && bookId != null)
            {
                var reader = db.Readers.FirstOrDefault(x=> x.Id == readerId);
                Book book = db.Books.FirstOrDefault(x => x.Id == bookId);
                book.IsPicked = true;
                reader.Books.Add(book);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return Content("Error");
        }

        public IActionResult ShowReader(int readerId)
        {
            var reader = db.Readers.Include(x => x.Books).FirstOrDefault(x => x.Id == readerId);
            if (reader != null) return View(reader);
            return Content("Error");
        }
    }
}
