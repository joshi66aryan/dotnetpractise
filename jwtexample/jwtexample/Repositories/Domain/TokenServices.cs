using System;
using jwtexample.Models.DTO;
using System.Security.Claims;
using jwtexample.Repositories.Abstract;
using jwtexample.DatabaseConnection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace jwtexample.Repositories.Domain
{
	public class TokenServices: ITokenServices
	{
        //IConfiguration is injected into the constructor, and it allows the TokenServices
        //class to access configuration settings, such as connection strings or other
        //application-specific settings.

        private readonly IConfiguration _configuration;    

        public TokenServices(IConfiguration configuration)  
        {
            _configuration = configuration;
        }


        /* A Claims Principal is an object that holds information about an authenticated entity, such as a user. It contains claims, 
         * which are pieces of information about the entity (e.g., roles, permissions, or custom attributes).In the provided code, 
         * the GetPrincipalFromExpiredToken method validates an expired token and retrieves the claims principal associated with it. 
         * The claims principal holds the claims extracted from the token, which can be used to make decisions or perform actions
         * based on the authenticated entity's identity and attributes.*/


        // This method takes an accessToken as input and returns the claims principal associated with it.
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,

                // Set the issuer signing key from the configuration
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();  // which is responsible for validating and handling JWT tokens.
            SecurityToken SecurityToken;

            
            /*The method then validates the provided token using the 'ValidateToken' method of the JwtSecurityTokenHandler. 
             * It passes the token, the TokenValidationParameters, and an out parameter SecurityToken to retrieve the 
             * validated token and the associated claims principal.*/

            // Validate the token and retrieve the principal
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken);
            var jwtsecuritytoken = SecurityToken as JwtSecurityToken;

            // Check if the token is valid and has the correct algorithm
            if (jwtsecuritytoken == null || !jwtsecuritytoken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid Token");

            return principal;

            /*
             * claime Principla contain: 
             * 1) Identity-related claims: These can include the user's unique identifier, username, or email address.
             * 2) Role-based claims: These specify the roles or permissions associated with the user, such as "admin" or "user".
             * 3) Custom claims: These are additional claims that can be defined by the application to store any specific 
                                 * information about the user, such as their date of birth, address, or any other relevant data
            */
        }


        // This method generates a JWT token based on the provided claims.
        public TokenResponse GetToken(IEnumerable<Claim> claim)
        {
            // Create a symmetric security key from the JWT secret in the configuration
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            // Create a new JWT token
            var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:ValidIssuer"],
                    audience: _configuration["Jwt:ValidAudience"],
                    expires: DateTime.Now.AddDays(7),
                    claims: claim,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            // Generate the token string
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Return the token and its valid-to date
            return new TokenResponse { TokenString = tokenString, ValidTo = token.ValidTo };
        }


        // This method generates a refresh token.
        public string GetRefreshToken()
        {
            var randomNumber = new byte[32];

            // Generate a random number for the refresh token
            using (var temp = RandomNumberGenerator.Create())
            {
                temp.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }

        }

    }
}

