using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoPacienteReceta
    {
        public Int64 Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public string Atencion { get; set; }
        public string HCL { get; set; }
        public string Hab { get; set; }
        public string Paciente { get; set; }
        public string Medico { get; set; }
        public Int64 ate_codigo { get; set; }
    }
}
