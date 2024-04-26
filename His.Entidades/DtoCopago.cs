using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoCopago
    {
        public Int64 id { get; set; }
        public Int64 CPpro_codigo { get; set; }
        public decimal CPv_unitario { get; set; }
        public decimal CPiva { get; set; }
        public decimal CPtotal { get; set; }
    }
}
