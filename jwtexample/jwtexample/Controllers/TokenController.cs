using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwtexample.DatabaseConnection;
using jwtexample.Models.DTO;
using jwtexample.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwtexample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly DatabaseConnectionContext _dbContext;
        private readonly ITokenServices _tokenServices;

        public TokenController(DatabaseConnectionContext dbContext, ITokenServices tokenServices)
        {

            _dbContext = dbContext;
            _tokenServices = tokenServices;
        }

        /*IActionResult is an interface in ASP.NET Core that represents the outcome of processing an HTTP request. 
         * It allows you to return different types of responses from an action method in a web API or MVC application.
         * For example, you can use IActionResult to return success messages, error messages, redirects, or custom 
         * content to the client. It provides a way to control the HTTP response and its status code, content, and 
         * other related information.*/

        [HttpPost]
        public IActionResult Refresh(RefreshTokenRequest tokenApiModel) {

            try
            {
                if (tokenApiModel == null)
                    return BadRequest("Invalid Client Request");

                // from users
                string accessToken = tokenApiModel.AccessToken;
                string refreshToken = tokenApiModel.RefreshToken;


                // verify access token and get principal.
                var principal = _tokenServices.GetPrincipalFromExpiredToken(accessToken);
                var userName = principal.Identity.Name;

                // check the userName with 'TokenInfo' table's username and retireve matching info.
                var user = _dbContext.TokenInfo.SingleOrDefault(eachData => eachData.UserName == userName);

                if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.Now)
                    return BadRequest("Invalid Client Request");

                var newAccessToken = _tokenServices.GetToken(principal.Claims);
                var newRefreshToken = _tokenServices.GetRefreshToken();

                user.RefreshToken = newRefreshToken;
                _dbContext.SaveChanges();

                return Ok(new RefreshTokenRequest()
                {
                    AccessToken = newAccessToken.TokenString,
                    RefreshToken = newRefreshToken
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /*
         * When a request is made to this action method, ASP.NET Core authentication middleware processes the request, 
         * validates the access token, and populates the User property of the ControllerBase class with information 
         * about the authenticated user.
         */

        // It is for removing token entry

        [HttpPost, Authorize]
        public IActionResult Revoke()
        {

            try
            {
                var userName = User.Identity.Name;
                var user = _dbContext.TokenInfo.SingleOrDefault(eachData => eachData.UserName == userName);

                if (user is null)
                    return BadRequest();

                user.RefreshToken = null;
                _dbContext.SaveChanges();

                return Ok(true);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
