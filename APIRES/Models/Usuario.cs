using System;
using System.Collections.Generic;

namespace APIRES.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Apellido { get; set; }
        public string Correo { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public string Cargo { get; set; } = null!;
        public string? Telefono { get; set; }
        public int IdRol { get; set; }

        public virtual Rol Rols { get; set; }
    }
}
