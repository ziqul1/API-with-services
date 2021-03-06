using MojeAPI.Models;
using Microsoft.EntityFrameworkCore;
using MojeAPI.Data.Services;
using MojeAPI.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:5001",
            ValidAudience = "https://localhost:5001",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
        };
    });

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MojeAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


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
    app.UseSwagger();
    // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MojeAPI v1"));
    app.UseSwaggerUI();

}

// app.UseSwaggerAuthorized();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//
// Using CORS to working API properly with Angular FRONT
app.UseCors(x => x.AllowAnyHeader()
      .AllowAnyMethod()
      .AllowAnyOrigin()
      .WithExposedHeaders("content-disposition")
      );

app.Run();

