using System;
namespace jwtexample.Models.DTO
{
	public class TokenResponse
	{
		public string? TokenString { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}

