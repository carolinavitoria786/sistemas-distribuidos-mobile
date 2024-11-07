using CinemaApi.Models;
using Microsoft.EntityFrameworkCore;


namespace CinemaApi.Data {

    public class CinemaApiContext : DbContext {

           public CinemaApiContext (DbContextOptions<CinemaApiContext> options)
            : base(options)
        {
        }

        public DbSet<Cinema> Cinema { get; set; } = default!;
        public DbSet<Filme> Filme { get; set; } = default!;
    }
}