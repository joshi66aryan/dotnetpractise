﻿using System;
using System.ComponentModel.DataAnnotations;

namespace jwtexample.Models.DTO
{
	public class LoginModel
	{
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}

