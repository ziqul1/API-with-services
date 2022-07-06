using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MojeAPI.Models;

namespace MojeAPI.Data.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetBooks();
        Task<IEnumerable<BookDTO>> GetSingleBook(long id);
        Task<IActionResult> UpdateBook(long id, BookDTO bookDTO);
        Task<ActionResult<BookDTO>> CreateBook(BookDTO bookDTO);
        Task<IActionResult> DeleteBook(long id);
    }
}
