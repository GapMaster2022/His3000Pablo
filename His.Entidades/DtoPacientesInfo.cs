using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoPacientesInfo
    {
        public int PAC_CODIGO { get; set; }
        public Int16 TIP_CODIGO { get; set; }
        public string PAC_HISTORIA_CLINICA { get; set; }
        public DateTime PAC_FECHA_CREACION { get; set; }
        public string PAC_APELLIDO_PATERNO { get; set; }
        public string PAC_APELLIDO_MATERNO { get; set; }
        public string PAC_NOMBRE1 { get; set; }
        public string PAC_NOMBRE2 { get; set; }
        public string PAC_CEDULA { get; set; }
        public DateTime PAC_EDAD { get; set; }
        public string PAC_GENERO { get; set; }
        public string PAC_CIVIL { get; set; }
        public string PAC_TELEFONO { get; set; }
        public string PAC_TELEFONO2 { get; set; }
        public string PAC_DIRECCION { get; set; }
        public string PAC_EMAIL { get; set; }
        public bool PAC_CON_SEGURO { get; set; }
        public int MED_CODIGO { get; set; }
    }
}
