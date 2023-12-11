using Library.Models.Account;
using Library.Models.Books;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Users
{

    public class Reader
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get { return FirstName+" "+LastName; } }

        public List<Book> Books { get; set; } = new();

    }
}
