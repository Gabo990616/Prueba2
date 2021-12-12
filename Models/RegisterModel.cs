using Entidad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prueba2.Models
{
    public class RegisterInputModel
    {
        [Required]
        public string Identificacion { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Rol { get; set; }
    }

    public class RegisterViewModel : RegisterInputModel
    {
        public RegisterViewModel(Usuario usuario)
        {
            Identificacion = usuario.Identificacion;
            Nombre = usuario.Nombre;
            User = usuario.User;
            Password = usuario.Password;
            Rol = usuario.Rol;
        }
    }
}
