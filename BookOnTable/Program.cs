using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using BookOnTable.Data;
using BookOnTable.Models;
internal class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHttpClient<OpenLibraryService>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Book On Table API", Description = "Todos os seus livros num só lugar", Version = "v1" });
        });

     
        var connectionString = builder.Configuration.GetConnectionString("Books") ?? "Data Source=Book.db";
        builder.Services.AddSqlite<AppDbContext>(connectionString);

        var app = builder.Build();

    
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book"));
        }

        app.UseHttpsRedirection();

     
        app.MapGet("/Book", async (AppDbContext db) => await db.Book.ToListAsync());
        app.MapGet("/Book/{id}", async (AppDbContext db, int id) => await db.Book.FindAsync(id));
        app.MapPut("/Book/{id}", async (AppDbContext db, Book updatebook, int id) =>
        {
            var book = await db.Book.FindAsync(id);
            if (book is null) return Results.NotFound();

            book.Title = updatebook.Title;
            book.Author = updatebook.Author;
            book.PublishDate = updatebook.PublishDate;

            await db.SaveChangesAsync();
            return Results.NoContent();
        });
        app.MapDelete("/book/{id}", async (AppDbContext db, int id) =>
        {
            var book = await db.Book.FindAsync(id);
            if (book is null) return Results.NotFound();

            db.Book.Remove(book);
            await db.SaveChangesAsync();
            return Results.Ok();
        });
        app.MapPost("/book", async (AppDbContext db, Book book) =>
        {
            await db.Book.AddAsync(book);
            await db.SaveChangesAsync();
            return Results.Created($"/book/{book.Id}", book);
        });


        app.MapGet("/external/book/{bookId}", async (OpenLibraryService openLibraryService, string bookId) =>
        {
            var bookInfo = await openLibraryService.GetBookInfoByIdAsync(bookId);
            return bookInfo is not null ? Results.Ok(bookInfo) : Results.NotFound();
        })
        .WithName("GetBookInfoById")
        .WithOpenApi();

        app.MapGet("/external/book/isbn/{isbn}", async (OpenLibraryService openLibraryService, string isbn) =>
        {
            var bookInfo = await openLibraryService.GetBookInfoByISBNAsync(isbn);
            return bookInfo is not null ? Results.Ok(bookInfo) : Results.NotFound();
        })
        .WithName("GetBookInfoByISBN")
        .WithOpenApi();

        app.Run();
    }
}