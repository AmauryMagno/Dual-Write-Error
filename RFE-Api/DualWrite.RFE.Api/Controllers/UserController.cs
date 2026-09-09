using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace DualWrite.RFE.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Implement your login logic here
            // For example, validate the user credentials and generate a token

            if (request.Email == "test@test" && request.Password == "password")
            {
                return Ok(new { Token = "fake-jwt-token" });
            }

            return Unauthorized();
        }
    }

}