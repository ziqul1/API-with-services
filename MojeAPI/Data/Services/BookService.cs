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

        //// GET: api/Books
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        //{
        //    return await _libraryContext.Books
        //        .Select(x => BookToDTO(x))
        //        .ToListAsync();
        //}


        // GET: api/Books/1
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetSingleBook(long id)
        {
            var book = await _libraryContext.Books.FindAsync(id);

            if (book == null)
                return NotFound();

            return BookToDTO(book);
        }


        // PUT: api/Books/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(long id, BookDTO bookDTO)
        {
            if (id != bookDTO.Id)
                return BadRequest();

            var book = await _libraryContext.Books.FindAsync(id);

            if (book == null)
                return NotFound();

            book.Title = bookDTO.Title;
            book.Price = bookDTO.Price;

            try
            {
                await _libraryContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!BookExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }


        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<BookDTO>> CreateBook(BookDTO bookDTO)
        {
            var book = new Book
            {
                Title = bookDTO.Title,
                Price = bookDTO.Price,
            };

            _libraryContext.Books.Add(book);
            await _libraryContext.SaveChangesAsync();

            return Create

            return CreatedAtAction(
                nameof(GetSingleBook),
                new { id = book.Id },
                BookToDTO(book)
                );
        }



        // DELETE: api/Books/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(long id)
        {
            var book = await _libraryContext.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            _libraryContext.Books.Remove(book);
            await _libraryContext.SaveChangesAsync();

            return NoContent();
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


       public async Task<IEnumerable<BookDTO>> IBookService.GetBooks()
        {
            return await _libraryContext.Books
                .Select(x => BookToDTO(x))
                .ToListAsync();
        }

        Task<IEnumerable<BookDTO>> IBookService.GetSingleBook(long id)
        {
            throw new NotImplementedException();
        }
    }
}
