using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class PRODUCTOCOPAGO
    {
        public Int64 CUE_CODIGO { get; set; }
        public string CUE_DETALLE { get; set; }
        public decimal CUE_VALOR_UNITARIO { get; set; }
        public decimal CUE_VALOR { get; set; }
        public decimal CUE_CANTIDAD { get; set; }
        public decimal CUE_IVA { get; set; }
        public bool paga_iva { get; set; }
        public decimal iva { get; set; }
    }
}
