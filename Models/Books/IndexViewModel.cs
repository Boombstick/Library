namespace Library.Models.Books
{
    public class IndexViewModel
    {
        public IEnumerable<BookShelf> Bookshelf { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }

        public IndexViewModel(IEnumerable<Book> books, PageViewModel pageViewModel,
            FilterViewModel filterViewModel, SortViewModel sortViewModel)
        {
            Books = books;
            FilterViewModel = filterViewModel;
            SortViewModel = sortViewModel;
            PageViewModel = pageViewModel;
        }
    }
}
