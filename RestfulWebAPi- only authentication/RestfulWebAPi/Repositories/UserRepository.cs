using System;
using RestfulWebAPi.DatabaseConnection;
using RestfulWebAPi.Models.AuthModel;
using RestfulWebAPi.Repositories;

namespace RestfulWebAPi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Database_Context _dbContext;

        public UserRepository(Database_Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> UserExists(string email)
        {
            return await _dbContext.user.AnyAsync(user => user.Email == email);
        }

        public async Task<Users> GetUserByEmail(string email)
        {
            return await _dbContext.user.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task AddUser(Users user)
        {
            await _dbContext.user.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void RemoveUser(Users user)
        {
            _dbContext.user.Remove(user);
        }

        public async Task<Users> GetUserByVerificationToken(string token)
        {
            return await _dbContext.user.FirstOrDefaultAsync(user => user.VerificationToken == token);
        }

        public async Task<Users> GetUserByResetToken(string resetToken)
        {
            return await _dbContext.user.FirstOrDefaultAsync(eachUser => eachUser.PasswordResetToken == resetToken);
        }
    }


}


