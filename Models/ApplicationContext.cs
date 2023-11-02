﻿using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Bookshelf> Bookshelf { get; set; } = null!;
        public DbSet<Reader> Readers { get; set; } = null!;
        public DbSet<Librarian> Librarians { get; set; } = null!;
        public ApplicationContext() {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = DESKTOP-6PRC2H4\\SQLEXPRESS; Database = LibraryDb; Trusted_Connection = True; TrustServerCertificate = Yes;");
        }

    }
}
