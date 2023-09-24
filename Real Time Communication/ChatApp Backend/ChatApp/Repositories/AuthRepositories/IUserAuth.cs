using System;
using ChatApp.Models.authenticationModel.domain;


namespace ChatApp.Repositories.AuthRepositories
{
	public interface IUserAuth
	{
        Task<bool> UserExists(string email);
        Task AddUser(Loginregister user);
        Task SaveChangesAsync();
        Task<Loginregister> GetUserByEmail(string email);
        Task<Loginregister> GetUserById(int Id);

    }
}

