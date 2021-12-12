using Datos;
using Entidad;
using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using prueba2.Hubs;
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
    public class UsuarioController : ControllerBase
    {
        private EventoService _service;
        private UserService service;

        private readonly IHubContext<SignalHub> _hubContext;
        public UsuarioController(PruebaFinal2Context context, IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
            _service = new EventoService(context);
            service = new UserService(context);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Inscripcion(UsuarioInputModel inputModel)
        {
            var request = service.Inscripcion(inputModel.Identificacion, inputModel.CodEvento);
            if (request.Error)
            {
                ModelState.AddModelError("Asignar Evento", request.Mensaje);
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                return BadRequest(problemDetails);
            }
            await _hubContext.Clients.All.SendAsync("SignalMessageReceived", request.Usuario);
            return Ok(request.Usuario);
        }

        [HttpGet("ConsultarUsuarios")]
        public ActionResult<List<Usuario>> Usuarios()
        {
            var request = service.Consult();
            if (request.Error)
            {
                ModelState.AddModelError("Consultar Usuarios", request.Mensaje);
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                return BadRequest(problemDetails);
            }

            return Ok(request.Usuarios);
        }

        [HttpGet("ConsultarEstudiantes")]
        public ActionResult<List<Usuario>> Estudiantes()
        {
            var request = service.ConsultIncritos();
            if (request.Error)
            {
                ModelState.AddModelError("Consultar Estudiantes", request.Mensaje);
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                return BadRequest(problemDetails);
            }
            return Ok(request.Usuarios);
        }

    }
}
