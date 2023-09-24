using System;
namespace RestfulWebAPi.Models.AuthModel
{
    public class UserloginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;


    }
}


