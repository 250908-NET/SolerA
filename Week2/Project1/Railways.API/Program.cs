using Microsoft.EntityFrameworkCore;
using Railways.Data;
using Railways.Models;


var builder = WebApplication.CreateBuilder(args);

string CS = File.ReadAllText("../connection_string.env");

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RailwaysDbContext>(options => options.UseSqlServer(CS));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    return "Hello World";
});

app.Run();
