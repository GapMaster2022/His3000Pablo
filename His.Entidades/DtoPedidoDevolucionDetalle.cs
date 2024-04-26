using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoPedidoDevolucionDetalle
    {
        public Int64 DevCodigo { get; set; }
        public Int64 PRO_CODIGO { get; set; }
        public string PRO_DESCRIPCION { get; set; }
        public double DevDetCantidad { get; set; }
        public Decimal DevDetValor { get; set; }
        public Decimal DevDetIva { get; set; }
        public Decimal DevDetIvaTotal { get; set; }
        public Int64 PDD_CODIGO { get; set; }
    }
}
