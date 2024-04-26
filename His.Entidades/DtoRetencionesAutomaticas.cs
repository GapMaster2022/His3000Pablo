using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoRetencionesAutomaticas
    {
        public int HOM_CODIGO { get; set; }
        public int MED_CODIGO { get; set; }
        public string SUJETO_RETENCION { get; set; }
        public string MED_RUC { get; set; }
        public string HOM_FACTURA_MEDICO { get; set; }
        public DateTime HOM_FACTURA_FECHA { get; set; }
        public decimal HOM_VALOR_NETO { get; set; }
        public decimal RET_PORCENTAJE { get; set; }
        public decimal VALOR_RETENCION { get; set; }
        public bool CONRETENCION { get; set; }
        public string RET_CODIGO { get; set; }
    }
}
