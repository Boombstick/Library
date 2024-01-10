using Library.Models.Books;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Authors
{
    public class Author
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }

        public string? Pseudonym { get; set; }
        public int Id { get; set; }

        public List<Book> Books { get; set; } = new();
    }
}
