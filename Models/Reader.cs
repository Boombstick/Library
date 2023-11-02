namespace Library.Models
{
    public class Reader
    {
        public string Name { get; set; }
        public int  Age { get; set; }
        public int Id { get; set; }
        public List<Book> Books { get; set; } = new();

    }
}
