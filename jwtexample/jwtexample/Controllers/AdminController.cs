using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwtexample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles="Admin")]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetData()
        {
            return Ok("Data from admin controller");

        }
    }
}
