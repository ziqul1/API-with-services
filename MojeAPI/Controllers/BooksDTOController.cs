﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetBooksAsync()
        {
            return Ok( await _bookService.GetBooksAsync());
        }

        // GET: api/Books/1
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetSingleBookAsync(int id)
        {
            var book = await _bookService.GetSingleBookAsync(id);

            // book ? NotFound() : book

            if (book == null)
                return NotFound();

            return book;
        }

        // PUT: api/Books/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync(int id, BookDTO bookDTO)
        {
            if (id != bookDTO.Id)
                return BadRequest();

            var book = await _bookService.GetSingleBookAsync(id);

            if (book == null)
                return NotFound();

            await _bookService.UpdateBookAsync(id, bookDTO);

            return NoContent();
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<BookDTO>> CreateBookAsync(BookDTO bookDTO)
        {
            var book = await _bookService.CreateBookAsync(bookDTO);

            return CreatedAtAction(
                nameof(GetSingleBookAsync),
                new { id = book.Id },
                book
                );
        }

        // DELETE: api/Books/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            var book = await _bookService.GetSingleBookAsync(id);
            if (book == null)
                return NotFound();

            await _bookService.DeleteBookAsync(id);

            return NoContent();
        }
    }
}
