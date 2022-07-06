using Microsoft.EntityFrameworkCore;

namespace MojeAPI.Models
{
    public class Initialize
    {
        public static LibraryContext GetContext()
        {
            var connectionstring = @"Server=(localdb)\mssqllocaldb;Database=LibraryDB;Trusted_Connection=True;";
            DbContextOptionsBuilder<LibraryContext> options = new DbContextOptionsBuilder<LibraryContext>();
            options.UseSqlServer(connectionstring);
            return new LibraryContext(options.Options);
        }
    }
}
