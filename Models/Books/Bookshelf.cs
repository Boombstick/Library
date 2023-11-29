using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Books
{
    public class Bookshelf
    {
        public int Id { get; set; }
        [NotMapped]
        public Book[,] Books { get; set; } = new Book[2, 5];
    }
}
