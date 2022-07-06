using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MojeAPI.Data.Services;
using MojeAPI.Models;


namespace MojeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksDTOController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksDTOController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            return Ok( await _bookService.GetBooks());
        }

        // GET: api/Books/1
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetSingleBook(long id)
        {
            var book = await _bookService.GetSingleBook(id);

            if (book == null)
                return NotFound();

            return book;
        }

        // PUT: api/Books/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(long id, BookDTO bookDTO)
        {
            if (id != bookDTO.Id)
                return BadRequest();

            var book = await _bookService.GetSingleBook(id);

            if (book == null)
                return NotFound();

            await _bookService.UpdateBook(id, bookDTO);

            return NoContent();
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<BookDTO>> CreateBook(BookDTO bookDTO)
        {
            var book = _bookService.CreateBook(bookDTO);

            return CreatedAtAction(
                nameof(GetSingleBook),
                new { id = book.Id },
                book
                );
        }

        // DELETE: api/Books/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(long id)
        {
            var book = await _bookService.GetSingleBook(id);
            if (book == null)
                return NotFound();

            await _bookService.DeleteBook(id);

            return NoContent();
        }
    }
}
