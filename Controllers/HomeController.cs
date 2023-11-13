using Library.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;

        public HomeController(ApplicationContext context)
        {
            db = context;
            if (!context.Authors.Any())
            {
                Librarian martin = new Librarian { Name = "Martin" };


                Reader zhenya = new Reader { Name = "Zhenya" ,Age =22};
                Reader vanya = new Reader { Name = "Vanya" ,Age = 12};

                Bookshelf bookshelf1 = new Bookshelf ();
                Bookshelf bookshelf2 = new Bookshelf ();

                Author oracle = new Author { Name = "Oracle" };
                Author google = new Author { Name = "Google" };
                Author microsoft = new Author { Name = "Microsoft" };
                Author apple = new Author { Name = "Apple" };

                Book user1 = new Book { Name = "Олег Васильев", Author = oracle, Publication = 26 ,BookShelf=bookshelf1};
                Book user2 = new Book { Name = "Александр Овсов", Author = oracle, Publication = 24, BookShelf = bookshelf1 };
                Book user3 = new Book { Name = "Алексей Петров", Author = microsoft, Publication = 25, BookShelf = bookshelf1 };
                Book user4 = new Book { Name = "Иван Иванов", Author = microsoft, Publication = 26, BookShelf = bookshelf1 };
                Book user5 = new Book { Name = "Петр Андреев", Author = microsoft, Publication = 23, BookShelf = bookshelf2 };
                Book user6 = new Book { Name = "Василий Иванов", Author = google, Publication   = 23, BookShelf = bookshelf2 };
                Book user7 = new Book { Name = "Олег Кузнецов", Author = google, Publication = 25, BookShelf = bookshelf2 };
                Book user8 = new Book { Name = "Андрей Петров", Author = apple, Publication = 24 ,BookShelf = bookshelf2 };

                context.Librarians.Add(martin);
                context.Readers.AddRange(zhenya, vanya);
                context.Bookshelf.AddRange(bookshelf1,bookshelf2);
                context.Authors.AddRange(oracle, google, microsoft, apple);
                context.Books.AddRange(user1, user2, user3, user4, user5, user6, user7, user8);
                context.SaveChanges();
            }
        }
        
        [HttpGet]
        public IActionResult AddBook() => View();

        [HttpPost]
        public async Task<IActionResult> AddBook(Book book, string authorName,string bookShelfId)
        {
            int number = Convert.ToInt32(bookShelfId);
            IEnumerable<Author> authors = db.Authors.ToList();
            IEnumerable<Bookshelf> bookshelves = db.Bookshelf.ToList();
            foreach (var a in authors)
            {
                if (a.Name == authorName)
                {
                    Book Book = new Book { Name = book.Name, Author = a, Publication = book.Publication, BookShelf=bookshelves.FirstOrDefault(x=> x.Id== number)};
                    db.Books.AddRange(Book);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            Author author = new Author { Name = authorName };
            Book Book1 = new Book { Name = book.Name, Author = author, Publication = book.Publication, BookShelf = bookshelves.FirstOrDefault(x => x.Id == number)};
            db.Authors.Add(author);
            db.Books.AddRange(Book1);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index(string name,int author = 0, int page = 1,SortState sortOrder = SortState.NameAsc,int bookShelfId= 0) { 
            int pageSize = 4;

            //Filter
            IQueryable<Book> books = db.Books.Include(x => x.Author);
            if (author != 0)
            {
                books = books.Where(p=> p.AuthorId == author);
            }
            if (!string.IsNullOrEmpty(name))
            {
                books = books.Where(p => p.Name!.Contains(name));
            }
            if (bookShelfId !=0)
            {
                books = books.Where(p=>p.BookShelfId == bookShelfId);
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
                    books=books.OrderByDescending(p=> p.Publication);
                    break;
                case SortState.AuthorAsc:
                    books=books.OrderBy(p=> p.Author);
                    break;
                case SortState.AuthorDesc:
                    books = books.OrderByDescending(p => p.Author);
                    break;
                case SortState.BookShelfAsc:
                    books=books.OrderBy(p=> p.BookShelf);
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
            var items = await books.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();

            //ViewModel

            IndexViewModel viewModel = new IndexViewModel(
                items,
                new PageViewModel(count, page, pageSize),
                new FilterViewModel(db.Authors.ToList(), author, name,bookShelfId),
                new SortViewModel(sortOrder)
                );

            return View(viewModel);
        }

        public async Task<IActionResult> DeleteBook(int? id)
        {
            if (id != null)
            {
                Book book = await db.Books.FirstOrDefaultAsync(x=>x.Id==id);
                if (book != null)
                {
                    db.Books.Remove(book);
                    await db.SaveChangesAsync();    
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowAllBooks()
        {
            var Books = await db.Books.ToListAsync();
            return View(Books);
        }
        
    }
}