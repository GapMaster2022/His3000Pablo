using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class vendedor
    {
            public string codigo { get; set; }
            public DateTime fec_ingreso { get; set; }
            public string fec_salida { get; set; }
            public string nro_identificacion { get; set; }
            public double comision { get; set; } ///por defecto 1 activo
            public string nombre { get; set; }       
    }
}
