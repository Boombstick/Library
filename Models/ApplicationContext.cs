using Library.Models.Account;
using Library.Models.Books;
using Library.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Library.Models;

public class ApplicationContext : IdentityDbContext<LibraryUser>
{
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<BookCase> Bookshelf { get; set; } = null!;
    public DbSet<BookShelf> AllBookshelf { get; set; } = null!;

    public DbSet<Reader> Readers { get; set; } = null!;

    public ApplicationContext()
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = MYBOOK_ZENITH\\SQLEXPRESS; Database = LibraryDb; Trusted_Connection = True; TrustServerCertificate = Yes;");
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
