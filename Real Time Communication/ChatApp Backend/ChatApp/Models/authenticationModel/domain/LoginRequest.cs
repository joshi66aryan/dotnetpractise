using System;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.authenticationModel.domain
{
	public class LoginRequest
	{
        [Required]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        [StringLength(100)]
        public string? Password { get; set; }
    }
}

