using Library.Models.Users;
namespace Library.Models.Books
{
    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Publication { get; set; }


        public int? BookShelfId { get; set; }
        public Bookshelf BookShelf { get; set; }


        public int? AuthorId { get; set; }
        public Author Author { get; set; }


        public int? ReaderId { get; set; }
        public Reader Reader { get; set; }

        public bool IsPicked { get; set; } = false;


    }
}
