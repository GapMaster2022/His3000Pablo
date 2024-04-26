using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;

namespace His.Entidades
{
    public class DtoPreciosConvenios
    {
        public int PRE_CODIGO { get; set; }
        public int CAC_CODIGO { get; set; }
        public string CAC_NOMBRE { get; set; }
        public int CAT_CODIGO { get; set; }
        public string CAT_NOMBRE { get; set; }
        public decimal PRE_VALOR { get; set; }
        public decimal PRE_PORCENTAJE { get; set; }

    }
}
