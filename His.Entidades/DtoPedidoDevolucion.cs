using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoPedidoDevolucion
    {
        public Int64 DevCodigo { get; set; }
        public Int64 Ped_Codigo { get; set; }
        public DateTime DevFecha { get; set; }
        public Int64 ID_USUARIO { get; set; }
        public string DevObservacion { get; set; }
        public List<DtoPedidoDevolucionDetalle> DetalleDevolucion { get; set; }
        public string IP_MAQUINA { get; set; }
    }
}
