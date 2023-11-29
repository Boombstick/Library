using Library.Models;
using Library.Models.Books;
using Library.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Library.Controllers
{
    public class ReaderController : Controller
    {
        private ApplicationContext db;
        IEnumerable<Reader> readers  = new List<Reader>();
        IEnumerable<Book> books  = new List<Book>();
        
        public ReaderController(ApplicationContext context)
        {
            db = context;
            readers = db.Readers.Select(x => x);
            books = db.Books.Select(x => x);

        }
        public IActionResult Index()
        {
            List<Reader> list = new List<Reader>();
            list = db.Readers.Include(x=>x.Books).ToList();
            return View(list);

        }

        public IActionResult AddReader() => View();

        [HttpPost]
        public async Task<IActionResult> AddReader(string name, int age)
        {

                Reader reader = new Reader { Name = name, Age = age };
                db.Readers.Add(reader);
                await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteReader(int? id)
        {
            if (id != null)
            {
                Reader reader = db.Readers.Include(x=>x.Books).FirstOrDefault(x => x.Id == id);
                    if (reader != null)
                {
                    reader.Books.Clear();
                    //foreach (var item in db.Books)
                    //{
                    //    if (item.ReaderId == reader.Id)
                    //    {
                    //        item.ReaderId = null;
                    //    }
                    //}
                    db.Remove(reader);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

            }
            
            return RedirectToAction("Index");
        }



        public IActionResult AddBookToReader()
        {
            ViewBag.Readers = new SelectList(readers, "Id", "Name");
            ViewBag.Books = new SelectList(books, "Id", "Name");


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBookToReader(int? readerId, int? bookId)
        {
            if (readerId != null && bookId != null)
            {
                Reader reader = db.Readers.FirstOrDefault(x=>x.Id== readerId);
                Book book = db.Books.FirstOrDefault(x=>x.Id== bookId);
                book.IsPicked = true;
                reader.Books.Add(book);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
                
            }
            return Content("Error");
        }

        public IActionResult ShowReader(int readerId)
        {
            Reader reader = db.Readers.Include(x => x.Books).FirstOrDefault(x => x.Id == readerId);
            if (reader != null) return View(reader);    
            
            return Content("Error");
        }
    }
}
 