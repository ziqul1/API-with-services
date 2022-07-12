using Microsoft.AspNetCore.Mvc;
using System.IO;
using CsvHelper;
using MojeAPI.Models;
using System.Globalization;
using CsvHelper.Configuration;
using MojeAPI.Data.Services;

namespace MojeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
            => _fileService = fileService;

        [HttpPost]
        public async Task<ActionResult> ReadFile(IFormFile filePath)
        {
            if(await _fileService.ReadFile(filePath))
                return Ok();
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<BookDTO>> WriteFile(int numberOfRecordsToTake, int skip, string filteredBooks)
        {
            var bytes = await _fileService.WriteFile(numberOfRecordsToTake, skip, filteredBooks);

            if (bytes != null)
                return File(bytes, "application/json", Path.GetFileName("newBooks.csv"));
            else
                return NoContent();
        }
    }
}
