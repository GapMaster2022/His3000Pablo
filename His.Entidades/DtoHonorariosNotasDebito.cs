using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoHonorariosNotasDebito
    {
        public int HOM_CODIGO { get; set; }
        public int MED_CODIGO { get; set; }
        public string NOT_RAZON_SOCIAL { get; set; }
        public string MED_RUC { get; set; }
        public string HOM_FACTURA_MEDICO { get; set; }
        public DateTime HOM_FACTURA_FECHA { get; set; }
        public decimal HOM_VALOR_NETO { get; set; }
        public decimal HOM_VALOR_PAGADO { get; set; }
        public decimal HOM_COMISION_CLINICA { get; set; }
        public decimal HOM_APORTE_LLAMADA { get; set; }
        public decimal DIFERENCIA { get; set; }
        public bool CONNOTAD { get; set; }
        public string OBSERVACION { get; set; }
        public string NOT_NUMERO { get; set; }
    }
}
