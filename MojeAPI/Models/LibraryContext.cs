using Microsoft.EntityFrameworkCore;

namespace MojeAPI.Models
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=LibraryDB;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
