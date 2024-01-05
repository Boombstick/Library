using Library.Models;
using Library.Models.Readers;
using Microsoft.EntityFrameworkCore;

namespace Library.Abstractions
{
    public class ReaderManager
    {
        private readonly ApplicationContext _context;
        public ReaderManager(ApplicationContext context)
        {
            _context = context;
        }

        public async Task DeleteReaderAsync(int id)
        {
            var reader = await _context.Readers.Include(x => x.Books).FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (reader != null)
            {
                reader.Books.Clear();
                _context.Readers.Remove(reader);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Reader?> GetReaderAsync(int id, bool includeBooks = true)
        {
            if (includeBooks)
            {
                var reader = await _context.Readers.Include(x => x.Books).FirstOrDefaultAsync(x => x.Id.Equals(id));
                return reader;
            }
            return await _context.Readers.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
        public async Task<IEnumerable<Reader?>> GetAllReadersAsync(bool includeBooks = true)
        {
            if (includeBooks)
            {
                var list = await _context.Readers.Include(x => x.Books).ToListAsync();
                return list;
            }
            return await _context.Readers.ToListAsync();
        }
        public async Task UpdateDataBase()
        {
            _context.SaveChanges();
        }
    }
}
