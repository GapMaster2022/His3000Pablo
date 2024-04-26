using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoReporteCopago
    {
        public string PRO_CODIGO { get; set; }
        public Int64 CANTIDAD { get; set; }
        public string DETALLE { get; set; }
        public decimal VALOR_UNITARIO { get; set; }
        public decimal IVA { get; set; }
        public decimal TOTAL { get; set; }
        public string RUBRO { get; set; }
        public decimal DESCUENTO { get; set; }
    }
}
