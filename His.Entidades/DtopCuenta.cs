using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtopCuenta
    {
        public Int64 id { get; set; }
        public Int64 COpro_codigo { get; set; }
        public decimal COv_unitario { get; set; }
        public decimal COiva { get; set; }
        public decimal COtotal { get; set; }
    }
}
