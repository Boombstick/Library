using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Models
{
    public class FilterViewModel
    {
        public SelectList Authors { get; set; }
        public int SelectedAuthor { get; set; }
        public string SelectedName { get; set; }
        public int SelectedBookShelf { get; set; }

        public FilterViewModel(List<Author> authors, int author, string name, int selectedBookShelf)
        {
            authors.Insert(0, new Author { Name = "Any", Id = 0 });
            Authors = new SelectList(authors, "Id", "Name", author);
            SelectedAuthor = author;
            SelectedName = name;
            SelectedBookShelf = selectedBookShelf;
        }
    }
}
