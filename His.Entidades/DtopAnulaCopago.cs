using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtopAnulaCopago
    {
        public Int64 C_ATENCION { get; set; }
        public string ATENCION { get; set; }
        public string HC { get; set; }
        public string PACIENTE { get; set; }
        public string ASEGURADORA { get; set; }
        public decimal T_CUENTA { get; set; }
    }
}
