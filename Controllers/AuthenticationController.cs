using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using MyWebApp.Services;

namespace MyWebApp.Controllers
{
    [Route("authenticate")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenService _authenService;

        public AuthenticationController(IAuthenService authenticationService)
        {
            _authenService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginModel loginModel)
        {
            var user = _authenService.AuthenticateUser(loginModel);
            if(user== null)
            {
                return Ok(new ApiResponseModel
                {
                    Success = false,
                    Message = "Invalid email or password",
                    Data = null
                });
            }
            return Ok(new ApiResponseModel
            {
                Success = true,
                Message = "Authenticate Success",
                Data = _authenService.GenerateToken(user)
            });
        }
    }
}
