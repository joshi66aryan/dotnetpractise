using System;
using System.Xml.Linq;
using ChatApp.DbConnection;
using ChatApp.Models.authenticationModel.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace ChatApp.Repositories.AuthRepositories
{
	public class UserRepo : IUserAuth 
    {
        private readonly DbConnectionContext _dbContext;

        public UserRepo(DbConnectionContext dbContext)   // database dependency injection.
        {
            _dbContext = dbContext;
        }


        public async Task<bool> UserExists(string email)  // check if same user exists
        {
            return await _dbContext.loginregisters.AnyAsync(user => user.Email == email);
        }


        public async Task AddUser(Loginregister user)  // upload in register
        {
            await _dbContext.loginregisters.AddAsync(user);
        }


        public async Task SaveChangesAsync()     // save 
        {
            await _dbContext.SaveChangesAsync();
        }


        public async Task<Loginregister> GetUserByEmail(string email)  // request user credential by email.
        {
            return await _dbContext.loginregisters.FirstOrDefaultAsync(user => user.Email == email);
        }


        public async Task<Loginregister> GetUserById(int Id)   //  request user by id.
        {
            return await _dbContext.loginregisters.FirstOrDefaultAsync(user => user.ID == Id);
        }

    }
}

