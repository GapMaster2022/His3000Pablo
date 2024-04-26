using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoPedidoDetalleOtros
    {
        public Int64 PDD_CODIGO { get; set; }
        public Int32 PED_CODIGO { get; set; }
        public Int32 PRO_CODIGO { get; set; }
        public String PRO_DESCRIPCION { get; set; }
        public Int32 PDD_CANTIDAD { get; set; }
        public Decimal PDD_VALOR { get; set; }
        public Decimal PDD_IVA { get; set; }
        public Decimal PDD_TOTAL { get; set; }
        public Boolean PDD_ESTADO { get; set; }
        public Decimal PDD_COSTO { get; set; }
        public String PDD_FACTURA { get; set; }
        public Int32 PDD_ESTADO_FACTURA { get; set; }
        public String  PDD_FECHA_FACTURA { get; set; }
        public String PDD_RESULTADO { get; set; }
        public String PRO_CODIGO_BARRAS { get; set; }
    }
}
