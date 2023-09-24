using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ChatApp.DbConnection;
using ChatApp.Models.authenticationModel.domain;
using ChatApp.Models.authenticationModel.dto;
using ChatApp.Repositories.AuthRepositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("AllowLocalhost")]  
    [ApiController]
    public class authenticationController : ControllerBase
    {

        private readonly IUserAuth? _userAuthRepository;

        public authenticationController(IUserAuth userAuthRepository)
        {
            _userAuthRepository = userAuthRepository ?? throw new ArgumentNullException(nameof(userAuthRepository));
        }


        LoginregisterResponse response = new LoginregisterResponse();   // creating instance of response model, it is saved  in response model.


        [HttpPost]
        public async Task<LoginregisterResponse> Registration(Loginregister request)
        {
            // destructure request properties
            var (Name, Email, Password) = (request.Name, request.Email, request.Password);

            // check if user already exists.
            if (await _userAuthRepository.UserExists(Email))
            {
                response.StatusMessage = "User Already Exists";
                response.StatusCode = 400;
                return response;
            }


            // make skeleton to populate register.
            var userRegisterItems = new Loginregister
            {
                Name = Name,
                Email = Email,
                Password = Password,
                CreatedOn = DateTime.Now

            };

            await _userAuthRepository.AddUser(userRegisterItems);  // Set registering user  in repository user --> register in database.
            await _userAuthRepository.SaveChangesAsync();

            response.StatusMessage = "Registration is successful";
            response.StatusCode = 200;


            return response;
        }


        [HttpPost]
        public async Task<LoginregisterResponse> Login(LoginRequest request)
        {

            // destructure request properties
            var (Email, Password) = ( request.Email, request.Password);


            var userByEmail = await _userAuthRepository.GetUserByEmail(Email);

            if (userByEmail == null)
            {
                response.StatusCode = 400;
                response.StatusMessage = "User does not exists";
                return response;
            }

            if (Password != userByEmail.Password)
            {
                response.StatusCode = 400;
                response.StatusMessage = "User's credential is wrong";
                return response;
            }

            var logedinUserCredential = new Loginregister()
            {
                Name = userByEmail.Name,
                Email = userByEmail.Email
            };

            response.StatusCode = 200;
            response.StatusMessage = "Login Successful";
            response.Email = userByEmail.Email;
            response.Name = userByEmail.Name;
            response.CreatedOn = userByEmail.CreatedOn;

            return response;
        }
    }
}
