using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoAtencionDatosAdicionales
    {
        public int cod_atencion { get; set; }
        public string empresa { get; set; }
        public string observaciones { get; set; }
        public string tipo_discapacidad { get; set; }
        public int porcentage_discapacidad { get; set; }
        public string paquete { get; set; }
    }
}
