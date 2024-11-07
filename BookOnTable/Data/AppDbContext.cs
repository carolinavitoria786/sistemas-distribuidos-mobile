using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using BookOnTable.Models;

namespace BookOnTable.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) {}
    public DbSet<Book> Book { get; set; } = null!;

}