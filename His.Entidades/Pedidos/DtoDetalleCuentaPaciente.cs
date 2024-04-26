using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades.Pedidos
{
    public class DtoDetalleCuentaPaciente
    {
        public Int64 INDICE { get; set; }
        public string AREA { get; set; }
        public string SUBAREA { get; set; }
        public string AREA_PEDIDO { get; set; }
        public DateTime FECHA { get; set; }
        public String CODIGO { get; set; }
        public string DESCRIPCION { get; set; }
        public Decimal VALOR { get; set; }
        public Decimal TOTAL { get; set; }
        public Decimal IVA { get; set; }
        public decimal CANTIDAD { get; set; }
        public double CANTIDAD_ORIGINAL { get; set; }
        public double CANTIDAD_DEVUELTA { get; set; }
        public Int64 RUBRO { get; set; }
        public string RUBRO_NOMBRE { get; set; }
        public string MEDICO_NOMBRE { get; set; }
        public int MEDICO_COD { get; set; }
        public string NumeroControl { get; set; }
        public double DESCUENTO { get; set; }
        public Int32 TipoMedico { get; set; }
        public string OBSERVACION { get; set; }
    }
}
