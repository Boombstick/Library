using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Books
{
    public class BookCase
    {
        public int Id { get; set; }
        public int row { get; set; } = 1;
        public int column { get; set; } = 1;
        public string? BookName { get; set; }

        public bool IsEmpty { get; set; } = true;

        public int? BookShelfId { get; set; }
        public BookShelf BookShelf { get; set; }
    }
}
