using CinemaApi.Data;
using CinemaApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi;

public static class CinemaEndpoints
{
    public static void MapCinemaEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Cinema").WithTags(nameof(Cinema));

        group.MapGet("/", async (CinemaApiContext db) =>
        {
            return await db.Cinema.ToListAsync();
        })
        .WithName("GetAllCinemas")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Cinema>, NotFound>> (int id, CinemaApiContext db) =>
        {
            return await db.Cinema.AsNoTracking()
                .FirstOrDefaultAsync(model => model.cinemaId == id)
                is Cinema model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCinemaById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Cinema cinema, CinemaApiContext db) =>
        {
            var affected = await db.Cinema
                .Where(model => model.cinemaId == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.cinemaId, cinema.cinemaId)
                    .SetProperty(m => m.nome, cinema.nome)
                    .SetProperty(m => m.cnpj, cinema.cnpj)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCinema")
        .WithOpenApi();

        group.MapPost("/", async (Cinema cinema, CinemaApiContext db) =>
        {
            db.Cinema.Add(cinema);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Cinema/{cinema.cinemaId}",cinema);
        })
        .WithName("CreateCinema")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, CinemaApiContext db) =>
        {
            var affected = await db.Cinema
                .Where(model => model.cinemaId == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCinema")
        .WithOpenApi();
    }
}