using DncerBackend.AppContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddCors();
builder.Services.AddMvc();

builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DincerDB;Trusted_Connection=True;"));



var app = builder.Build();

app.UseCors(builder => builder
    .WithOrigins("http://localhost:5173") // Ýzin verilecek origin. react 5173 portunda ayaða kalkýyor.
    .AllowAnyMethod()
    .AllowAnyHeader());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
