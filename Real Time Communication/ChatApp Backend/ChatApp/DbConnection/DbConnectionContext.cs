using System;
using System.Collections.Generic;
using ChatApp.Models.authenticationModel.domain;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DbConnection
{
	public class DbConnectionContext: DbContext
	{
		public DbConnectionContext(DbContextOptions<DbConnectionContext> options): base(options)
        {
        }


        public DbSet<Loginregister> loginregisters { get; set; }
    }
}

