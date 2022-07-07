using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MojeAPI.Models;

namespace MojeAPI.Data.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _libraryContext;

        public BookService(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        
        public async Task<IEnumerable<BookDTO>> GetBooksAsync()
        {
            return await _libraryContext.Books
                .Select(x => BookToDTO(x))
                .ToListAsync();
        }

        public async Task<BookDTO> GetSingleBookAsync(int id)
        {
            var book = await _libraryContext.Books.FindAsync(id);

            if (BookExists(id))
                BookToDTO(book);
            
            return null;

            // BookExists(id) ? BookToDTO(book) : null;
        }

        public async Task UpdateBookAsync(int id, BookDTO bookDTO)
        {
            var book = await _libraryContext.Books.FindAsync(id);

            book.Title = bookDTO.Title;
            book.Price = bookDTO.Price;

            await _libraryContext.SaveChangesAsync();
        }

        public async Task<BookDTO> CreateBookAsync(BookDTO bookDTO)
        {
            var book = new Book
            {
                Title = bookDTO.Title,
                Price = bookDTO.Price,
            };

            _libraryContext.Books.Add(book);
            await _libraryContext.SaveChangesAsync();

            return BookToDTO(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _libraryContext.Books.FindAsync(id);

            _libraryContext.Books.Remove(book);
            await _libraryContext.SaveChangesAsync();
        }

        private bool BookExists(int id)
        {
            return (_libraryContext.Books.Any(e => e.Id == id));
        }

        private static BookDTO BookToDTO(Book book) =>
            new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
            };
    }
}
