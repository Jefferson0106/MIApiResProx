using System;
using System.Collections.Generic;

namespace APIRES.Models
{
    public partial class Relacione
    {
        public int IdRelaciones { get; set; }
        public int IdRol { get; set; }
        public int IdModulo { get; set; }

        public virtual Modulo IdModulos { get; set; } = null!;
        public virtual Rol IdRols{ get; set; } = null!;
    }
}
