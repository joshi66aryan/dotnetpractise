using jwtexample.DatabaseConnection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using jwtexample.Models.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.DataProtection;

using jwtexample.Repositories.Domain;
using jwtexample.Repositories.Abstract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// it is going to configure sql server, using connection strings and connect with database.
builder.Services.AddDbContext<DatabaseConnectionContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")
         )
);


/*
     * IdentityRole is designed to be used with the ASP.NET Core Identity system, which provides functionality for 
                    * user authentication, authorization, and user management. It is part of the role-based authorization 
                    * feature, where roles are used to group users with similar permissions or access levels.


     * IdentityRole provides properties and methods for managing role-related information, such as the name of the role.
                    * Roles can be used to control access to various parts of an application by associating users with 
                    * specific roles and then granting or restricting access based on those roles. 


    * IdentityRole is typically used in conjunction with IdentityUser (or a custom user entity derived from IdentityUser) to establish 
                    * role-based authorization in an application.
                
    * The IdentityRole class can be extended or customized to include additional properties or behavior specific to the application's 
                    * role management needs.
                
*/


/*The code below configures the identity system in the application. It adds the Identity services with the specified ApplicationUser 
 * class representing the user and IdentityRole class representing roles. It also configures the storage of identity data using 
 * AddEntityFrameworkStores<DatabaseConnectionContext>(), where DatabaseConnectionContext is the database context class. 
 * Additionally, it adds the default token providers for generating and validating tokens used in the identity system.*/


// For Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseConnectionContext>()
                .AddDefaultTokenProviders();


/*The code below configures the authentication system in the application. It adds the authentication services using 
 * AddAuthentication. It also sets the default authentication scheme for various operations to be the JWT Bearer
 * authentication scheme (JwtBearerDefaults.AuthenticationScheme).*/


// Adding Authentication
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })


    /*The code below further configures the JWT Bearer authentication scheme. It adds the JWT Bearer authentication scheme using AddJwtBearer.
     * It specifies various options for token validation and security. The SaveToken option is set to true to save the received token on 
     * the authentication properties. The TokenValidationParameters specify the parameters for validating the JWT token. It enables issuer 
     * and audience validation (ValidateIssuer and ValidateAudience set to true), and sets the valid issuer, valid audience, and the 
     * symmetric security key used for signing and validating the token based on the application's configuration settings.*/


    // Adding jwt brearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
            ValidAudience = builder.Configuration["Jwt:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
        };
    });


/*The line builder.Services.AddTransient<ITokenServices, TokenServices>(); tells the system that whenever something needs an object 
 * that implements the ITokenServices interface, it should create a new instance of the TokenServices class.This ensures that each part
 * of the application gets its own fresh copy of the TokenServices object when it needs one. It helps with managing dependencies and 
 * prevents potential problems caused by sharing the same instance across multiple components.*/

builder.Services.AddTransient<ITokenServices, TokenServices>();


// adding cors policy for trasnfering data that are hosted in another hosting platform.
var AllowLocalhost = "_AllowLocalhost";
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
        {
            policy.WithOrigins("*")
                    .AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// for cors policy.
app.UseCors(AllowLocalhost);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

