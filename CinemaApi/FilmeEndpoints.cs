using CinemaApi.Data;
using CinemaApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi;

public static class FilmeEndpoints
{
    public static void MapFilmeEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Filme").WithTags(nameof(Cinema));

        group.MapGet("/", async (CinemaApiContext db) =>
        {
            return await db.Filme.ToListAsync();
        })
        .WithName("GetAllMovies")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Filme>, NotFound>> (int id, CinemaApiContext db) =>
        {
            return await db.Filme.AsNoTracking()
                .FirstOrDefaultAsync(model => model.filmeId == id)
                is Filme model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetFilmeById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Filme filme, CinemaApiContext db) =>
        {
            var affected = await db.Filme
                .Where(model => model.filmeId== id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.filmeId, filme.filmeId)
                    .SetProperty(m => m.nome, filme.nome)
                    .SetProperty(m => m.genero, filme.genero)
                    .SetProperty(m => m.ano, filme.ano)
                    .SetProperty(m => m.cinemaId, filme.cinemaId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateFilme")
        .WithOpenApi();

        group.MapPost("/", async (Filme filme, CinemaApiContext db) =>
        {
            db.Filme.Add(filme);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Filme/{filme.filmeId}", filme);
        })
        .WithName("CreateFilme")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, CinemaApiContext db) =>
        {
            var affected = await db.Filme
                .Where(model => model.filmeId == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteFilme")
        .WithOpenApi();
    }
}