using Datos;
using Entidad;
using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prueba2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prueba2.Controllers
{
   
        [Authorize]
        [Route("api/[controller]")]
        [ApiController]
        public class RegisterController : ControllerBase
        {
            private UserService _service;

            public RegisterController(PruebaFinal2Context context)
            {
                _service = new UserService(context);
            }

            [AllowAnonymous]
            [HttpPost]
            public ActionResult<Usuario> register(RegisterInputModel registerInput)
            {
                var request = _service.Register(mapUser(registerInput));

                if (request.Error)
                {
                    ModelState.AddModelError("Registrar Usuario", request.Mensaje);
                    var problemDetails = new ValidationProblemDetails(ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                    };
                    return BadRequest(problemDetails);
                }
                return Ok(request.Usuario);
            }

            private Usuario mapUser(RegisterInputModel registerInput)
            {
                var usuario = new Usuario()
                {
                    Identificacion = registerInput.Identificacion,
                    Nombre = registerInput.Nombre,
                    User = registerInput.User,
                    Password = registerInput.Password,
                    Rol = registerInput.Rol,
                };

                return usuario;
            }

          
        }
    }

