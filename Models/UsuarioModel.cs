using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prueba2.Models
{
    public class UsuarioInputModel
    {
        public int CodEvento { get; set; }
        public string Identificacion { get; set; }
    }

    public class UsuarioViewModel:UsuarioInputModel
    {
        public Evento Evento { get; set; }
        public UsuarioViewModel(Usuario usuario)
        {

        }
    }
}
