using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoCierreTurno
    {
        public DateTime Fecha { get; set; }
        public string PAC_HISTORIA_CLINICA { get; set; }
        public string ATE_NUMERO_ATENCION { get; set; }
        public string hab_Numero { get; set; }
        public string PACIENTE { get; set; }
        public string CATEGORIA { get; set; }
        public string CAJERO_NOMBRE { get; set; }
        public Int32 CAJERO_CODIGO { get; set; }
        public string ESTADO { get; set; }
        public Int32 ID_USUARIO { get; set; }
        public string MEDICO_TURNO { get; set; }
        public string OBSERVACION { get; set; }
    }
}
