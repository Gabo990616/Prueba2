using Entidad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prueba2.Models
{
    public class EventoModel
    {
        public class EventoInputModel
        {
            [Required]
            public DateTime Fecha { get; set; }
            [Required]
            public String Descripcion{ get; set; }
            [Required]
            public int AforoPermitido { get; set; }
            [Required]
            public int CantidadInscrita { get; set; }
        }

        public class EventoViewModel : EventoInputModel
        {
            public EventoViewModel(Evento evento)
            {
                Fecha = evento.Fecha;
                Descripcion = evento.Descripcion;
                AforoPermitido = evento.AforoPermitido;
                CantidadInscrita= evento.CantidadInscrita;
            }
        }
    }
}
