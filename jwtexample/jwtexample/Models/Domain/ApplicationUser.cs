using System;
using Microsoft.AspNetCore.Identity;

namespace jwtexample.Models.Domain
{
    /*
     * The ApplicationUser class inherits from the IdentityUser class, which is part of the ASP.NET Core Identity framework. 
     * IdentityUser provides properties and methods for managing user-related information and functionality, such as user 
     * authentication and authorization.
     * 
     * By creating the ApplicationUser class and extending IdentityUser, you are customizing the user entity by adding the 
     * Name property. This allows you to store and retrieve the name of a user alongside the standard user properties provided 
     * by IdentityUser, such as username, email, and password.
     * 
     */

    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}




