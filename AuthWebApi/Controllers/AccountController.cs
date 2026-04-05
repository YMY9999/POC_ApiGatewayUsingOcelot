using JWTAuth.Handlers;
using JWTAuth.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;

        public AccountController(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost]
        public ActionResult<AuthResponse> Authinticate([FromBody] AuthRequest authRequest)
        {

            var authResponse = _jwtTokenHandler.GenerateJwtToken(authRequest);
            if (authResponse == null) return Unauthorized();

            return Ok(authResponse);
        }


    }
}
