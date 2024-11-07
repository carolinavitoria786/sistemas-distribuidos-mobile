using Microsoft.EntityFrameworkCore;
using CinemaApi.Data;
using CinemaApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CinemaApiContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CinemaApiContext") ?? throw new InvalidOperationException("Connection string 'CinemaApiContext' not found.")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCinemaEndpoints();

app.MapFilmeEndpoints();

app.Run();