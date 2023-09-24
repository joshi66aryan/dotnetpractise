using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Models.authenticationModel.dto
{
	public class LoginregisterResponse : Response
	{
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

