using System;
namespace RestfulWebAPi.Models.AuthModel
{
    public class ResetPassword
    {
        [Required]
        public string Token { get; set; } = string.Empty;

        [Required, MinLength(8, ErrorMessage = "Password must be minimum 8 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required, Compare("Password")]
        public string ConformPassword { get; set; } = string.Empty;

    }
}

