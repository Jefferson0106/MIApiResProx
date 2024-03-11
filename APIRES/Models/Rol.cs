using System;
using System.Collections.Generic;

namespace APIRES.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Relaciones = new HashSet<Relacione>();
            Usuarios = new HashSet<Usuario>();
        }

        public int IdRol { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Relacione> Relaciones { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
