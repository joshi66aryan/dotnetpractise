using ChatApp.DbConnection;
using ChatApp.Hubs;
using ChatApp.Repositories.AuthRepositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRazorPages();
builder.Services.AddSignalR();   // for real-time communication.


// it is going to configure sql server, using connection strings and connect with database.
builder.Services.AddDbContext<DbConnectionContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")
         )
);


// cors policy.
var AllowLocalhost = "_AllowLocalhost";

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowCredentials(); 
        });
});

// For Repositories (accessing database separately.)
builder.Services.AddScoped<IUserAuth, UserRepo>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// UseCors must be called before MapHub.
app.UseCors(AllowLocalhost);

app.MapHub<ChatHubs>("/chathub");  // for real-time communication.

app.Run();

