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
        
        public async Task<IEnumerable<BookDTO>> GetBooks()
        {
            return await _libraryContext.Books
                .Select(x => BookToDTO(x))
                .ToListAsync();
        }

        public async Task<BookDTO> GetSingleBook(long id)
        {
            var book = await _libraryContext.Books.FindAsync(id);

            return BookToDTO(book);
        }

        public async Task UpdateBook(long id, BookDTO bookDTO)
        {
            var book = await _libraryContext.Books.FindAsync(id);

            book.Title = bookDTO.Title;
            book.Price = bookDTO.Price;

            await _libraryContext.SaveChangesAsync();
        }

        public async Task<BookDTO> CreateBook(BookDTO bookDTO)
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

        public async Task DeleteBook(long id)
        {
            var book = await _libraryContext.Books.FindAsync(id);

            _libraryContext.Books.Remove(book);
            await _libraryContext.SaveChangesAsync();
        }

        private bool BookExists(long id)
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
