using Datos;
using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using prueba2.Config;
using prueba2.Models;
using prueba2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prueba2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private JwtService _jwtService;
        private UserService _userService;

        public LoginController(PruebaFinal2Context context, IOptions<AppSetting> appSettings)
        {
            _jwtService = new JwtService(appSettings);
            _userService = new UserService(context);
        }

        [AllowAnonymous]
        [HttpPost()]
        public IActionResult Login([FromBody] LoginInputModel loginInput)
        {
            var user = _userService.Validate(loginInput.User, loginInput.Password);
            if (user == null)
            {
                ModelState.AddModelError("Acceso Denegado", "Username or password is incorrect");
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                return BadRequest(problemDetails);
            }
            var response = _jwtService.GenerateToken(user);
            return Ok(response);
        }
    }
}
