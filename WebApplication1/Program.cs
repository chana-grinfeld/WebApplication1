using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.Controllers;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//ADD IT
builder.Services.AddCors(p => p.AddPolicy("AlowAll", option =>
{
    option.AllowAnyMethod();
    option.AllowAnyHeader();
    option.AllowAnyOrigin();
}));
//
//ADD IT
builder.Services.AddScoped<CusttomersController>();
//
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AlowAll");
}

app.UseAuthorization();

app.MapControllers();

app.Run();
