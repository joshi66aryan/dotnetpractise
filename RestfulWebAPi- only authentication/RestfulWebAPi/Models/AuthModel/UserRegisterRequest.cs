using System;
namespace RestfulWebAPi.Models.AuthModel
{
    public class UserRegisterRequest
    {

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(8, ErrorMessage = "Password must be minimum 8 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required, Compare("Password")]
        public string ConformPassword { get; set; } = string.Empty;

    }
}