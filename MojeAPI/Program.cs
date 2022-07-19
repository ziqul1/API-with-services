using MojeAPI.Models;
using Microsoft.EntityFrameworkCore;
using MojeAPI.Data.Services;
using MojeAPI.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.UseMiddleware<SwaggerBasicAuthMiddleware>();

builder.Services.AddDbContext<LibraryContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("BooksConnectionString"))
    );



builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IFileService, FileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseAuthorization();
app.UseSwaggerAuthorized();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MojeAPI v1"));

app.UseHttpsRedirection();

app.MapControllers();

// Using CORS to working API properly with Angular FRONT
app.UseCors(x => x.AllowAnyHeader()
      .AllowAnyMethod()
      .AllowAnyOrigin()
      .WithExposedHeaders("content-disposition")
      );

app.Run();

