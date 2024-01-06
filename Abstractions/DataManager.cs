using Library.Models;
using Library.Models.Readers;
using Microsoft.AspNetCore.Mvc;

namespace Library.Abstractions
{
    public class DataManager
    {
        private readonly ReaderManager _readerManager;
        private readonly BookManager _bookManager;
        private readonly ApplicationContext _context;
        public DataManager(BookManager bookManager, ReaderManager readerManager, ApplicationContext context)
        {
            _bookManager = bookManager;
            _readerManager = readerManager;
            _context = context;
        }
        public async Task<Reader?> BookToReaderAsync(int readerId, int bookId)
        {

            var reader = await _readerManager.GetReaderAsync(readerId, false);
            var book = await _bookManager.GetBookAsync(bookId, false);
            if (reader == null && book == null)
            {
                return null;
            }
            book.IsPicked = true;
            reader.Books.Add(book);
            await _context.SaveChangesAsync();
            return reader;
        }
        public async Task<Reader?> BookToLibraryAsync(int readerId, int bookId)
        {
            var reader = await _readerManager.GetReaderAsync(readerId, false);
            var book = await _bookManager.GetBookAsync(bookId, false);
            if (reader == null && book == null)
            {
                return null;
            }
            book.IsPicked = false;
            reader.Books.Remove(book);
            await _context.SaveChangesAsync();
            return reader;
        }


    }
}
