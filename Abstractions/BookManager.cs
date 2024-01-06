using Library.Models;
using Library.Models.Books;
using Library.Models.Readers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Library.Abstractions
{
    public class BookManager
    {
        private readonly ApplicationContext _context;
        public BookManager(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetListOfBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }
        public async Task<IndexViewModel> GetAllBooksAsync(string name, int author = 0, int page = 1, SortState sortOrder = SortState.NameAsc, int bookShelfId = 0)
        {
            int pageSize = 5;

            //Filter
            IQueryable<Book> books = _context.Books.Include(x => x.Author);
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
                books = books.Where(p => p.BookCase.BookShelfId == bookShelfId);
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
                    books = books.OrderBy(p => p.BookCase.BookShelf);
                    break;
                case SortState.BookShelfDesc:
                    books = books.OrderByDescending(p => p.BookCase.BookShelf);
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
                new FilterViewModel(_context.Authors.ToList(), author, name, bookShelfId),
                new SortViewModel(sortOrder)
                );
            return viewModel;
        }
        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }

        }
        public async Task<Book> GetBookAsync(int id, bool includeAuthor = true)
        {
            if (includeAuthor)
            {
                
                var bookWithAuthor = await _context.Books.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id.Equals(id));
                return bookWithAuthor;
            }
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return book;

        }
        public async Task<Book> GetBookWithReaderAsync(int id, bool includeAuthor = true)
        {
            if (includeAuthor)
            { 
                var bookWithAuthor = await _context.Books.Include(x => x.Reader).Include(x => x.Author).FirstOrDefaultAsync(x => x.Id.Equals(id));
                return bookWithAuthor;
            }
            var book = await _context.Books.Include(x => x.Reader).FirstOrDefaultAsync(x => x.Id.Equals(id));
            return book;

        }
        public async Task AddBookAsync(Book book, string authorName, string bookShelfId, CoverColor coverColor)
        {
            int number = Convert.ToInt32(bookShelfId);
            var lastBookCase = await _context.Bookshelf.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            BookCase bookCase = GetNewBookCase(lastBookCase, number, book.Name);
            Book Book = new Book { Name = book.Name, Author = _context.Authors.FirstOrDefault(x => x.Name == authorName), Publication = book.Publication, BookCase = bookCase, CoverPath = GetCoverColor(coverColor) };


            //Вынести в отдельный Сервис
            _context.Books.Add(Book);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateDataBase()
        {
            _context.SaveChanges();
        }
        private BookCase GetNewBookCase(BookCase lastBookCase, int bookShelfId, string bookName)
        {
            IEnumerable<BookShelf> bookshelves = _context.AllBookshelf.ToList();
            var temp = bookshelves.FirstOrDefault(x => x.Id == bookShelfId);

            int row = lastBookCase.row;
            int column = lastBookCase.column + 1;

            if (column == 10)
            {
                row = lastBookCase.row + 1;
                column = 1;
            }
            BookCase result = new BookCase { column = column, row = row, IsEmpty = false, BookShelf = temp, BookName = bookName };
            if (temp.OccupiedСells > 50)
            {
                var newBookShelf = new BookShelf();
                _context.AllBookshelf.AddAsync(newBookShelf);
                result.BookShelf = newBookShelf;
            }
            return result;
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
