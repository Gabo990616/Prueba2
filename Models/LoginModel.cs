using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace prueba2.Models
{
    public class LoginInputModel
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginViewModel
    {
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string User { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Rol { get; set; }
        public string Token { get; set; }
    }
}
