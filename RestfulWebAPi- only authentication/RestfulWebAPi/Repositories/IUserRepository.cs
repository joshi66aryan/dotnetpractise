using System;
using RestfulWebAPi.Models.AuthModel;
using System.Threading.Tasks;

namespace RestfulWebAPi.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string email);
        Task<Users> GetUserByEmail(string email);
        Task AddUser(Users user);
        Task SaveChangesAsync();
        void RemoveUser(Users user);
        Task<Users> GetUserByVerificationToken(string token);
        Task<Users> GetUserByResetToken(string resetToken);
    }
}






