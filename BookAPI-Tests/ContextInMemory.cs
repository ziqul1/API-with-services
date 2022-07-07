using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MojeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAPI_Tests
{
    public class ContextInMemory : IDisposable
    {
        private bool disposedValue = false;

        public LibraryContext libraryContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: "BookTest_Database").Options;

            var context = new LibraryContext(option);
            if(context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        public LibraryContext libraryContextForSQLite()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var option = new DbContextOptionsBuilder<LibraryContext>()
                .UseSqlite(connection).Options;

            var context = new LibraryContext(option);

            if(context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
