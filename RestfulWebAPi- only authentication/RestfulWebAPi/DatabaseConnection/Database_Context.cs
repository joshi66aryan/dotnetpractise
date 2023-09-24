using System;
using System.Collections.Generic;

namespace RestfulWebAPi.DatabaseConnection
{
    public class Database_Context : DbContext
    {
        public Database_Context(DbContextOptions<Database_Context> options) : base(options)
        {
        }

        public DbSet<Users> user { get; set; }

    }

}