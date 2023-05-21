using BysinessServices.Interfaces;
using BysinessServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ResourceManagementSystemAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;

        }

        [HttpPost("login"), AllowAnonymous]
        [ProducesResponseType(typeof(JwtPairModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login(AuthInfoModel loginInfo)
        {
            int? id = await _userService.GetUserIdByLogin(loginInfo.Login);
            if (id is null) { return NotFound(); }
            //id ?? -1 is just a placehoplder. This code cannot be reached if id is NULL
            //BUT compiller still requres to convert int? to int somehow.
            if (await _authService.VerifyPasswordHash(id ?? -1, loginInfo.Password))
            {
                var user = await _userService.GetByIdAsync(id ?? -1);
                JwtPairModel jwtPair = new JwtPairModel()
                {
                    AccessToken = _authService.GenerateJwtAccessToken(user, TimeSpan.FromMinutes(15)),
                    RefreshToken = _authService.GenerateJwtRefreshToken(user, TimeSpan.FromDays(30))
                };

                await _authService.AddRefreshTokenToUserByIdAsync(user.Id ?? -1, jwtPair.RefreshToken);
                //Create JWT access token for 15 minutes
                return Ok(jwtPair);
            }
            else 
            {
                return BadRequest();
            }
        }

        /*
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(JwtPairModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Refresh(string refreshToken) 
        {

        }
        */
    }
}
