using System;
using System.Collections.Generic;
using jwtexample.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace jwtexample.DatabaseConnection
{
    /*By extending IdentityDbContext<ApplicationUser>, the DatabaseConnectionContext class inherits the functionality provided 
     * by IdentityDbContext, such as managing user authentication and authorization-related data, including user accounts, 
     * roles, and claims.In summary, the DatabaseConnectionContext class serves as a DbContext for the database connection 
     * and provides access to the underlying database tables, including the TokenInfo table, which likely stores information 
     * related to authentication tokens.*/

    public class DatabaseConnectionContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseConnectionContext(DbContextOptions<DatabaseConnectionContext> options) : base(options)
        {
        }

        public DbSet<TokenInfo> TokenInfo { get; set; }
    }
}

