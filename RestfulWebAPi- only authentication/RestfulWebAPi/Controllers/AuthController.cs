using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using NuGet.Common;



namespace RestfulWebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;      // for email service


        public AuthController(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }


        [HttpPost("register")]

        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (await _userRepository.UserExists(request.Email))
            {
                return BadRequest("User Already Exists");
            }

            // passing parameter to CreatePasswordHash, inorder to create hash.
            CreatePasswordHashAsync(request.Password, out byte[] passwordHash, out byte[] passwordSalt);  // Generate password hash and salt.

            // Create random verification token.
            string randomVerificationToken = CreateRandomTokenAsync();


            // populating users model.

            var userItems = new Users  // Set the username, passwordsalt, passwordhash in the user model.
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = randomVerificationToken,
                CreatedAt = DateTime.Now 
            };

            await _userRepository.AddUser(userItems);
            await _userRepository.SaveChangesAsync();

            return Ok("Just registered, now verify email");

        }


        // Send verification mail to user for conforming users.

        [HttpPost("email")]
        public IActionResult SendEmail(EmailDTO request)
        {
            _emailService.SendEmail(request);
            return Ok("Email send sucessfully ");
        }

        // just after registration process is completed in front end, front end will send email to user via 'email api' in server that includes 
        // verification link --> /api/Authcontroller/Verify/token. 
        // after that, user will click the verification link that is send to mail, upon clicking verification link user is directed to another page 
        // with UI that confirm verification, at the same time the same link verifies the user. 


        // after Clicking the verification link sent via email to registered users, this endpoint is used.

        [HttpPost("verify")]

        public async Task<IActionResult> Verify(string token)
        {
            var user = await _userRepository.GetUserByVerificationToken(token);
            if (user == null)
            {
                return BadRequest("Invalid token.");
            }

            user.VerifiedAt = DateTime.Now;
            await _userRepository.SaveChangesAsync();

            return Ok("User Verified and Registered");
        }



        [HttpPost("login")]

        public async Task<IActionResult> Login(UserloginRequest request)
        {
            var userValue = await _userRepository.GetUserByEmail(request.Email);
            if (userValue == null)
            {
                return BadRequest("User doesnot exists.");
            }

            if (!VerifyPasswordHashAsync(request.Password, userValue.PasswordHash, userValue.PasswordSalt))
            {
                return BadRequest("Wrong Credential");
            }

            if (userValue.VerifiedAt == null)
            {
                return BadRequest("User not verified.");
            }

            //  var token = CreateToken(user);   // calling CreateToken method for creating token.
            return Ok($"Loged In {userValue.Email}");
        }


        [HttpPost("forgot-password")]

        public async Task<IActionResult> ForgotPassword(string email)
        {
            var userValue = await _userRepository.GetUserByEmail(email);

            if (userValue == null)
            {
                return BadRequest("User doesnot found.");
            }

            userValue.PasswordResetToken = CreateRandomTokenAsync();
            userValue.ResetTokenExpries = DateTime.Now.AddDays(1);
            await _userRepository.SaveChangesAsync();

            return Ok($"Reset your password");  // instead of sending text, return token to PasswordResetToken to front end. And front end  will consume send email api 
                                                // and front end will  send email to particular user that contain link that will redirect to reset password page, after user 
                                                // enter credential, front end  use this api --> /api/AuthController/reset-password/{ResetpasswordBody} that will reset the password.
                                                
        }


        [HttpPost("reset-password")]

        public async Task<IActionResult> ResetPassword(ResetPassword request)
        {
            var userValue =  await _userRepository.GetUserByResetToken(request.Token);

            if (userValue == null || userValue.ResetTokenExpries < DateTime.Now)
            {
                return BadRequest("Invalid Token.");
            }

            CreatePasswordHashAsync(request.Password, out byte[] passwordHash, out byte[] passwordSalt);  // Generate password hash and salt.

            userValue.PasswordHash = passwordHash;
            userValue.PasswordSalt = passwordSalt;
            userValue.PasswordResetToken = null;
            userValue.ResetTokenExpries = null;

            await _userRepository.SaveChangesAsync();

            return Ok($"Password successfully Changed");

        }


        [HttpPost("delete-useraccount")]
        public async Task<IActionResult> DeleteAccount(string email, string password)
        {
            var userValue = await _userRepository.GetUserByEmail(email);
            if (userValue == null)
            {
                return BadRequest("User not found");
            }

            if (!VerifyPasswordHashAsync(password, userValue.PasswordHash, userValue.PasswordSalt))
            {
                return BadRequest("Wrong Credential");
            }

            _userRepository.RemoveUser(userValue);
            await _userRepository.SaveChangesAsync();

            return Ok("User account deleted succesfully");
        }

        private string CreateRandomTokenAsync()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }


        /// bcrypt is specifically designed for secure password hashing,
        /// while HMAC-SHA512 is a general-purpose algorithm for message authentication and integrity. 

        // verifying user's entered password.


        private bool VerifyPasswordHashAsync(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }


        // Hashing the user's password.
        private void CreatePasswordHashAsync(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())  // Create an instance of HMACSHA512 for password hashing.
            {
                passwordSalt = hmac.Key;    // Get the generated key as the password salt.
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));    // Compute the hash of the password using UTF8 encoding.
            }

        }


    }
}
