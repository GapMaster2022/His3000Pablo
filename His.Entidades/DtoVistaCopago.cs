using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoVistaCopago
    {
        public Int64 ID { get; set; }
        public Int64 PEDIDO { get; set; }
        public string RUBRO { get; set; }
        public string PRO_CODIGO { get; set; }
        public string DETALLE { get; set; }
        public Int64 CANTIDAD { get; set; }
        public decimal VALOR_UNITARIO { get; set; }
        public decimal VALOR_IVA { get; set; }
        public decimal VALOR_TOTAL { get; set; }
        public decimal PORCENTAJE_COPAGO { get; set; }
        public decimal VALOR_COPAGO { get; set; }
        public decimal UNITARIO_COPAGO { get; set; }
        public decimal IVA_COPAGO { get; set; }
        public decimal TOTAL_COPAGO { get; set; }
        public decimal UNITARIO { get; set; }
        public decimal IVA { get; set; }
        public decimal TOTAL { get; set; }
    }
}
