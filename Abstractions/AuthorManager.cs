using Library.Models.Readers;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Library.Models.Authors;

namespace Library.Abstractions
{
    public class AuthorManager
    {
        private readonly ApplicationContext _context;
        public AuthorManager(ApplicationContext context)
        {
            _context = context;
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.Include(x => x.Books).FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (author != null)
            {
                author.Books.Clear();
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddAuthor(string firstName, string lastName, string? pseudonym)
        {
            var author = new Author { FirstName = firstName, LastName = lastName, Pseudonym = pseudonym };
            _context.Authors.Add(author);
            UpdateDataBase();
        }

        public async Task<Author?> GetAuthorAsync(int id, bool includeBooks = true)
        {
            if (includeBooks)
            {
                var author = await _context.Authors.Include(x => x.Books).FirstOrDefaultAsync(x => x.Id.Equals(id));
                return author;
            }
            return await _context.Authors.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
        public async Task<IEnumerable<Author?>> GetAllAuthorsAsync(bool includeBooks = true)
        {
            if (includeBooks)
            {
                var list = await _context.Authors.Include(x => x.Books).ToListAsync();
                return list;
            }
            return await _context.Authors.ToListAsync();
        }
        public async Task UpdateDataBase()
        {
            _context.SaveChanges();
        }
    }
}
