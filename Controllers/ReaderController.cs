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
        private readonly DataManager _dataManager;


        public ReaderController(ReaderManager readerManager, BookManager bookManager, DataManager dataManager)
        {
            _readerManager = readerManager;
            _bookManager = bookManager;
            _dataManager = dataManager;
        }


        public async Task<IActionResult> AddBookToReader(int readerId, int bookId)
        {

            var result = await _dataManager.BookToReaderAsync(readerId, bookId);
            if (result ==null)
            {
                return Content("Error");
            }
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> AddBookToLibrary(int readerId, int bookId)
        {

            var result = await _dataManager.BookToLibraryAsync(readerId, bookId);
            if (result == null)
            {
                return Content("Error");
            }
            return RedirectToAction("Index");

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

        public async Task<IActionResult> Index()
        {
            var ReaderList = await _readerManager.GetAllReadersAsync();
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
