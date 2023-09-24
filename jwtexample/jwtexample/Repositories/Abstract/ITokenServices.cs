using System;
using System.Security.Claims;
using jwtexample.Models.DTO;

namespace jwtexample.Repositories.Abstract
{
	public interface ITokenServices
	{
		TokenResponse GetToken(IEnumerable<Claim> claim);
		string GetRefreshToken();
		ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
	}
}

