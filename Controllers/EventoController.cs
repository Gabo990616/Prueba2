using Datos;
using Entidad;
using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using prueba2.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static prueba2.Models.EventoModel;

namespace prueba2.Controllers
{
    [Authorize/*(Roles = "ADMINISTRADOR")*/]
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController:ControllerBase
    {
        private EventoService _service;
        private UserService service;

        private readonly IHubContext<SignalHub> _hubContext;
        public EventoController(PruebaFinal2Context context, IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
            _service = new EventoService(context);
            service = new UserService(context);
        }
        [HttpPost]
        public async Task<ActionResult<Evento>> Guardar(EventoInputModel eventoInput)
        {
            var request = _service.Save(mapServicio(eventoInput));
            if (request.Error)
            {
                ModelState.AddModelError("Guardar Evento", request.Mensaje);
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                return BadRequest(problemDetails);
            }
            await _hubContext.Clients.All.SendAsync("SignalMessageReceived", eventoInput);
            return Ok(request.Evento);
        }

        private Evento mapServicio(EventoInputModel eventoInput)
        {
            var evento = new Evento()
            {
                Fecha = eventoInput.Fecha,
                Descripcion = eventoInput.Descripcion,
                AforoPermitido = eventoInput.AforoPermitido,
                CantidadInscrita = eventoInput.CantidadInscrita,
            };

            return evento;
        }

        [HttpGet("ConsultarEventos")]
        public ActionResult<List<Evento>> Eventos()
        {
            var request = _service.Consult();
            if (request.Error)
            {
                ModelState.AddModelError("Consultar Eventos", request.Mensaje);
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                return BadRequest(problemDetails);
            }

            return Ok(request.Eventos);
        }

        [HttpGet("ConsultarEventosPermitidos")]
        public ActionResult<List<Evento>> EventosPermitidos()
        {
            var request = _service.ConsultAforoPermitido();
            if (request.Error)
            {
                ModelState.AddModelError("Consultar Eventos Permitidos", request.Mensaje);
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                return BadRequest(problemDetails);
            }
            return Ok(request.Eventos);
        }
    }
}
