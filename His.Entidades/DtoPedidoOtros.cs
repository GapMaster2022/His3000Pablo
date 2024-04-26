using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoPedidoOtros
    {
        public int PED_CODIGO { get; set; }
        public Int16 PEA_CODIGO { get; set; }
        public Int16 PEE_CODIGO { get; set; }
        public String PED_DESCRIPCION { get; set; }
        public Int16 PED_ESTADO{ get; set; }
        public DateTime PED_FECHA { get; set; }
        public Int16 ID_USUARIO { get; set; }
        public int ATE_CODIGO { get; set; }
        public Int16 TIP_PEDIDO { get; set; }
        public Int16 PED_PRIORIDAD { get; set; }
        public int PED_TRANSACCION { get; set; }
        public int MED_CODIGO { get; set; }
        public List<DtoPedidoDetalleOtros> DetallePedidoOtros{ get; set; }
    }
}
