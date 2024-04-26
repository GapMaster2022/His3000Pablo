using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoPacientesEmergencia
    {
        public int PAC_CODIGO { get; set; }
        public string PAC_IDENTIFICACION { get; set; }
        public string PAC_HISTORIA_CLINICA { get; set; }
        public string PAC_NOMBRE1 { get; set; }
        public string PAC_NOMBRE2 { get; set; }
        public string PAC_APELLIDO_PATERNO { get; set; }
        public string PAC_APELLIDO_MATERNO { get; set; }
        public int ATE_CODIGO { get; set; }
        public int TIP_CODIGO { get; set; }
        public DateTime PAC_FECHA_ATENCION { get; set; }        
        public DateTime PAC_FECHA_NACIMIENTO { get; set; }
    }
}
