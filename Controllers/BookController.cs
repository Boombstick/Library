using Library.Models;
using Library.Models.Books;
using Library.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        ApplicationContext db;

        public BookController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult AddBook() => View();

        [HttpPost]
        public async Task<IActionResult> AddBook(Book book, string authorName, string bookShelfId, CoverColor coverColor)
        {
            int number = Convert.ToInt32(bookShelfId);
            IEnumerable<Author> authors = db.Authors.ToList();
            IEnumerable<Bookshelf> bookshelves = db.Bookshelf.ToList();
            if (authors.Any(x => x.Name == authorName))
            {
                Book Book = new Book { Name = book.Name, Author = authors.FirstOrDefault(x=>x.Name==authorName), Publication = book.Publication, BookShelf = bookshelves.FirstOrDefault(x => x.Id == number), CoverPath = GetCoverColor(coverColor) };
                db.Books.Add(Book);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            Author author = new Author { Name = authorName };
            Book Book1 = new Book { Name = book.Name, Author = author, Publication = book.Publication, BookShelf = bookshelves.FirstOrDefault(x => x.Id == number), CoverPath = GetCoverColor(coverColor) };
            db.Authors.Add(author);
            db.Books.Add(Book1);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowAllBooks(string name, int author = 0, int page = 1, SortState sortOrder = SortState.NameAsc, int bookShelfId = 0)
        {
            int pageSize = 4;

            //Filter
            IQueryable<Book> books = db.Books.Include(x => x.Author);
            if (author != 0)
            {
                books = books.Where(p => p.AuthorId == author);
            }
            if (!string.IsNullOrEmpty(name))
            {
                books = books.Where(p => p.Name!.Contains(name));
            }
            if (bookShelfId != 0)
            {
                books = books.Where(p => p.BookShelfId == bookShelfId);
            }

            //Sort
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    books = books.OrderByDescending(p => p.Name);
                    break;
                case SortState.PublicatinAsc:
                    books = books.OrderBy(p => p.Publication);
                    break;
                case SortState.PublicatinDesc:
                    books = books.OrderByDescending(p => p.Publication);
                    break;
                case SortState.AuthorAsc:
                    books = books.OrderBy(p => p.Author);
                    break;
                case SortState.AuthorDesc:
                    books = books.OrderByDescending(p => p.Author);
                    break;
                case SortState.BookShelfAsc:
                    books = books.OrderBy(p => p.BookShelf);
                    break;
                case SortState.BookShelfDesc:
                    books = books.OrderByDescending(p => p.BookShelf);
                    break;
                default:
                    books = books.OrderBy(p => p.Name);
                    break;
            }

            //Paging
            var count = await books.CountAsync();
            var items = await books.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            //ViewModel

            IndexViewModel viewModel = new IndexViewModel(
                items,
                new PageViewModel(count, page, pageSize),
                new FilterViewModel(db.Authors.ToList(), author, name, bookShelfId),
                new SortViewModel(sortOrder)
                );

            return View(viewModel);
        }

        public async Task<IActionResult> DeleteBook(int? id)
        {
            if (id != null)
            {
                var book = await db.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (book != null)
                {
                    db.Books.Remove(book);
                    await db.SaveChangesAsync();
                    return RedirectToAction("ShowAllBooks");
                }
            }
            return RedirectToAction("ShowAllBooks");
        }

        public IActionResult Index()
        {

            return View();
        }



        public async Task<IActionResult> ShowBook(int? bookId)
        {
            if (bookId != null)
            {
                var book = await db.Books.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == bookId);
                return View(book);
            }
            return NotFound();
        }


        private string GetCoverColor(CoverColor coverColor)
        {
            switch (coverColor)
            {
                case CoverColor.Red: return "/previewBooks/redCover.jpg";
                case CoverColor.Green: return "/previewBooks/greenCover.jpg";
                case CoverColor.Blue: return "/previewBooks/blueCover.jpg";
                default: return String.Empty;
            }
        }
    }
}