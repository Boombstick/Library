using Library.Abstractions;
using Library.Models;
using Library.Models.Account;
using Library.Models.Books;
using Library.Models.Readers;
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
        private readonly ReaderManager _readerManager;
        private readonly BookManager _bookManager;
        IEnumerable<Book> books = new List<Book>();

        public ReaderController(ReaderManager readerManager, BookManager bookManager)
        {
            _readerManager = readerManager;
            _bookManager = bookManager;
        }


        public async Task<IActionResult> AddBookToReader(int readerId, int bookId)
        {

            var reader = await _readerManager.GetReaderAsync(readerId, false);
            var book = await _bookManager.GetBookAsync(bookId, false);
            book.IsPicked = true;
            reader.Books.Add(book);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
            return Content("Error");
        }






        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddBookToReaderByAdmin()
        {
            var readers = await _readerManager.GetAllReadersAsync(false);
            var books = await _bookManager.GetListOfBooksAsync();
            ViewBag.Readers = new SelectList(readers, "Id", "FullName");
            ViewBag.Books = new SelectList(books, "Id", "Name");
            return View();
        }
        public IActionResult Index()
        {
            var ReaderList = _readerManager.GetAllReadersAsync();
            return View(ReaderList);
        }
        public async Task<IActionResult> DeleteReader(int id)
        {
            await _readerManager.DeleteReaderAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ShowReader(int readerId)
        {
            var reader = await _readerManager.GetReaderAsync(readerId);
            if (reader != null) return View(reader);
            return Content("Error");
        }
    }
}
