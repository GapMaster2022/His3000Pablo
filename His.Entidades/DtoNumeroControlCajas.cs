using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoNumeroControlCajas
    {
        public int NCC_CODIGO { get; set;}
        public int NCC_NUMERO { get; set; }
        public int NCC_FACTURA_INICIAL { get; set; }
        public int NCC_FACTURA_FINAL { get; set; }
        public string NCC_TIPO { get; set; }
        public string CAJ_NOMBRE { get; set;}
        public int CAJ_CODIGO { get; set; }
        public string TID_DESCRIPCION { get; set; }
        public int TID_CODIGO { get; set;}
    }
}
