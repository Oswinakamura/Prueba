using System;
using System.Collections.Generic;

#nullable disable

namespace Calltech.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
