using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using jwtexample.DatabaseConnection;
using jwtexample.Models;
using jwtexample.Models.Domain;
using jwtexample.Models.DTO;
using jwtexample.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace jwtexample.Controllers
{

    /*
     * ControllerBase is a foundational class in ASP.NET Core for creating controllers that handle HTTP requests and produce 
     * responses in web applications. It provides common functionality and methods for working with HTTP requests and responses,
     * making it easier to build controllers that implement specific business logic.
     */

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly DatabaseConnectionContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenServices _tokenServices;


        public AuthorizationController( DatabaseConnectionContext dbContext,
                                        UserManager<ApplicationUser> userManager,
                                        RoleManager<IdentityRole> roleManager,
                                        ITokenServices tokenServices)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenServices = tokenServices;
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginModel loginModel)
        {
            // getting user by user's UserName.
            var user = await _userManager.FindByNameAsync(loginModel.UserName);

            // if user isn't null and credential matches.
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {

                //returns a list of roles assigned to the user.
                var userRoles = await _userManager.GetRolesAsync(user);


                // store the claims that will be used to generate a JWT.
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),  // (ClaimTypes.Name) represents the username of the user and is set to user.UserName.


                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  // (JwtRegisteredClaimNames.Jti) represents the "JWT ID"
                                                                                        // and is set to a newly generated unique identifier (GUID).
                                                                                        // This claim is used to uniquely identify the JWT.
                };

                // iterating over list of roles assigned to the user and adding to authClaims list.
                foreach ( var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                // getting token and refresh token.
                var token = _tokenServices.GetToken(authClaims);
                var refreshToken = _tokenServices.GetRefreshToken();

                // insert into TokenInfo.
                var tokenInfo = _dbContext.TokenInfo.FirstOrDefault(a => a.UserName == user.UserName);


                if(tokenInfo == null)
                {
                    var info = new TokenInfo
                    {
                        UserName = user.UserName,
                        RefreshToken = refreshToken,
                        RefreshTokenExpiry = DateTime.Now.AddDays(7)
                    };

                   await _dbContext.TokenInfo.AddAsync(info);
                }
                else
                {
                    tokenInfo.RefreshToken = refreshToken;
                    tokenInfo.RefreshTokenExpiry = DateTime.Now.AddDays(7);

                }

                try
                {
                   _dbContext.SaveChanges();
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }

               
                return Ok( new LoginResponse
                {
                    Name = user.Name,
                    UserName = user.UserName,
                    Token = token.TokenString,
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo,
                    statusCode = 1,
                    statusMessage = "Logged In"
                });
             
            }

            // logedin failed 
            return Ok(new LoginResponse
            {
                statusCode = 0,
                statusMessage = " Invalid Username or Password",
                Token = "",
                Expiration = null

            });
        }


        [HttpPost]
        public async Task<IActionResult> Registration([FromBody]RegistrationModel registrationModel)
        {
            var status = new Status();

            //chaeck validation of entered data.
            if (!ModelState.IsValid)
            {
                status.statusCode = 0;
                status.statusMessage = "Please pass all the required fields";
                return Ok(status);
            }

            // check if user exists or not
            var userExists = await _userManager.FindByNameAsync(registrationModel.UserName);     


            // if user isn't null.
            if (userExists != null)
            {
                status.statusCode = 0;
                status.statusMessage = "Invalid Username";
                return Ok(status);
            }


            // create user.
            var user = new ApplicationUser
            {
                UserName = registrationModel.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = registrationModel.Email,
                Name = registrationModel.Name
            };

            // create users
            var result = await _userManager.CreateAsync(user, registrationModel.Password);

            // if failed
            if (!result.Succeeded)
            {
                status.statusCode = 0;
                status.statusMessage = "User Creation Failed";
                return Ok(status);
            }

            // add roles here
            // for admin registration use UserRoles.Admin instead of UserRoles.User
            if (!await _roleManager.RoleExistsAsync(UsersRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UsersRoles.User));

            if (await _roleManager.RoleExistsAsync(UsersRoles.User))
                await _userManager.AddToRoleAsync(user, UsersRoles.User);

            status.statusCode = 1;
            status.statusMessage = "User Registration Successfull";
            return Ok(status);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword changePasswordModel)
        {
            var status = new Status();

            //chaeck validation of entered data.
            if (!ModelState.IsValid)
            {
                status.statusCode = 0;
                status.statusMessage = "Pass all the fields";
                return Ok(status);
            }

            // find the user credentials in database.
            var user = await _userManager.FindByNameAsync(changePasswordModel.UserName);
            if(user is null)
            {
                status.statusCode = 0;
                status.statusMessage = "Invalid Credential";
                return Ok(status);
            }

            // validating user entered current password with database password.
            if(!await _userManager.CheckPasswordAsync(user, changePasswordModel.CurrentPassword))
            {
                status.statusCode = 0;
                status.statusMessage = "Invalid Credentials";
                return Ok(status);
            }

            // changeing user current password.
            var result = await _userManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);

            if (!result.Succeeded)
            {
                status.statusCode = 0;
                status.statusMessage = "Failed to Change Password";
                return Ok(status);
            }

            status.statusCode = 1;
            status.statusMessage = "Password has changed successfully ";
            return Ok(result);
        }

        // only using once, beacuse we need only one admin in this application. After registering only one user, we are commenting this code snippet.


        /*
         
        [HttpPost]
        public async Task<IActionResult> RegistrationAdmin([FromBody] RegistrationModel registrationModel)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.statusCode = 0;
                status.statusMessage = "Please pass all the required fields";
                return Ok(status);
            }

            // check if user exists or not
            var userExists = await _userManager.FindByNameAsync(registrationModel.UserName);     // might be logicla error.

            if (userExists != null)
            {
                status.statusCode = 0;
                status.statusMessage = "Invalid Username";
                return Ok(status);
            }


            // create user 
            var userData = new ApplicationUser
            {
                UserName = registrationModel.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = registrationModel.Email,
                Name = registrationModel.Name
            };

            var result = await _userManager.CreateAsync(userData, registrationModel.Password);

            if (!result.Succeeded)
            {
                status.statusCode = 0;
                status.statusMessage = "User Creation Failed";
                return Ok(status);
            }

            // for admin registration use UserRoles.Admin instead of UserRoles.User

            if (!await _roleManager.RoleExistsAsync(UsersRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UsersRoles.Admin));

            if (await _roleManager.RoleExistsAsync(UsersRoles.Admin))
                await _userManager.AddToRoleAsync(userData, UsersRoles.Admin);

            status.statusCode = 1;
            status.statusMessage = "Admin User Registration Successfull";
            return Ok(status);
        }

        */


    }
}
