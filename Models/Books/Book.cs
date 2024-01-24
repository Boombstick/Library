using Library.Models.Authors;
using Library.Models.Readers;

namespace Library.Models.Books
{
    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Publication { get; set; }

        public int? AuthorId { get; set; }
        public Author Author { get; set; }

        public int? ReaderId { get; set; }
        public Reader Reader { get; set; }

        public int? BookCaseId { get; set; }
        public BookCase BookCase { get; set; }

        public int PageCount { get; set; }
        public int NumberOfReading { get; set; } = 0;

        public bool IsPicked { get; set; } = false;
        public string? CoverPath { get; set; } = null;

    }
}
