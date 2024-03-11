using System;
using System.Collections.Generic;

namespace APIRES.Models
{
    public partial class Modulo
    {
        public Modulo()
        {
            Relaciones = new HashSet<Relacione>();
        }

        public int IdModulo { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Relacione> Relaciones { get; set; }
    }
}
