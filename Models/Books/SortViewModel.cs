﻿namespace Library.Models.Books
{
    public class SortViewModel
    {
        public SortState NameSort { get; set; }
        public SortState PublicationSort { get; set; }
        public SortState AuthorSort { get; set; }
        public SortState BookShelfSort { get; set; }
        public SortState Current { get; set; }

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            PublicationSort = sortOrder == SortState.PublicatinAsc ? SortState.PublicatinDesc : SortState.PublicatinAsc;
            AuthorSort = sortOrder == SortState.AuthorAsc ? SortState.AuthorDesc : SortState.AuthorAsc;
            BookShelfSort = sortOrder == SortState.BookShelfAsc ? SortState.BookShelfDesc : SortState.BookShelfAsc;
            Current = sortOrder;
        }
    }
}
