using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MojeAPI.Models;

namespace MojeAPI.Data.Services
{
    public interface IBookService
    {
        public Task<IEnumerable<BookDTO>> GetBooks();
        public Task<BookDTO> GetSingleBook(long id);
        public Task UpdateBook(long id, BookDTO bookDTO);
        public Task<BookDTO> CreateBook(BookDTO bookDTO);
        public Task DeleteBook(long id);
    }
}
