namespace Library.Models
{
    public class Author
    {
        public string? Name { get; set; }
        public int Id { get; set; }

        public List<Book> Books { get; set; } = new();
    }
}
