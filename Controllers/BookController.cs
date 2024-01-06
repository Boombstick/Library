using Library.Abstractions;
using Library.Models;
using Library.Models.Books;
using Library.Models.Readers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly BookManager _bookManager;

        public BookController(BookManager bookManager)
        {
            _bookManager = bookManager;
        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult AddBook() => View();
        [HttpPost]
        public async Task<IActionResult> AddBook(Book book, string authorName, string bookShelfId, CoverColor coverColor)
        {
            await _bookManager.AddBookAsync(book, authorName, bookShelfId, coverColor);

            //Author newAuthor = new Author { Name = authorName };
            //Book Book1 = new Book { Name = book.Name, Author = newAuthor, Publication = book.Publication, BookCase = bookCase, CoverPath = GetCoverColor(coverColor) };
            //db.Authors.Add(newAuthor);
            //db.Books.Add(Book1);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ShowAllBooks(string name, int author = 0, int page = 1, SortState sortOrder = SortState.NameAsc, int bookShelfId = 0)
        {
            var viewModel = await _bookManager.GetAllBooksAsync(name, author, page, sortOrder, bookShelfId);

            return View(viewModel);
        }
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookManager.DeleteBookAsync(id);

            return RedirectToAction("ShowAllBooks");
        }
        public async Task<IActionResult> ShowBook(int bookId, bool includeReader = false)
        {
            if (includeReader)
            {
                var bookWithReader = await _bookManager.GetBookWithReaderAsync(bookId);
                if (bookWithReader != null) return View(bookWithReader);
            }
            else
            {
                var book = await _bookManager.GetBookWithReaderAsync(bookId);
                if (book != null) return View(book);
            }
            return NotFound();
        }


    }
}