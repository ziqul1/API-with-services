﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MojeAPI.Models;

namespace MojeAPI.Data.Services
{
    public interface IBookService
    {
        public Task<IEnumerable<BookDTO>> GetBooksAsync();
        public Task<BookDTO> GetSingleBookAsync(int id);
        public Task UpdateBookAsync(int id, BookDTO bookDTO);
        public Task<BookDTO> CreateBookAsync(BookDTO bookDTO);
        public Task DeleteBookAsync(int id);
    }
}
