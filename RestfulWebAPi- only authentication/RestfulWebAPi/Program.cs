global using System;
global using Microsoft.EntityFrameworkCore;
global using RestfulWebAPi.Models.AuthModel;
global using RestfulWebAPi.Services.EmailService;
global using System.ComponentModel.DataAnnotations;
global using RestfulWebAPi.DatabaseConnection;
global using RestfulWebAPi.Repositories;

using System.Text;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// it is going to configure sql server, using connection strings and connect with database.
builder.Services.AddDbContext<Database_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// For email services
builder.Services.AddScoped<IEmailService, EmailService>();

// For Repositories.
builder.Services.AddScoped<IUserRepository, UserRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();   // allow authentication capabilities.

app.UseAuthorization();

app.MapControllers();

app.Run();



