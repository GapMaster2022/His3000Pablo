using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoPreFactura
    {

        public Int32 PREFAC_CODIGO { get; set; }
        public Int32 ATE_CODIGO { get; set; }
        public String PREFAC_NUMERO { get; set; } 
        public Int32 PREFAC_AUTORIZACION { get; set; }
        public DateTime PREFAC_FECHA { get; set; }
        public String CLI_NOMBRE { get; set; }
        public String CLI_RUC { get; set; }
        public String CLI_TELEFONO { get; set; }
        public Decimal PREFAC_TOTAL { get; set; }
        public Decimal PREFAC_SUBTOTAL { get; set; }
        public Decimal PREFAC_IVAUNO { get; set; }
        public Decimal PREFAC_IVADOS { get; set; }
        public Decimal PREFAC_IVATRES { get; set; }
        public String PREFAC_ESTADO { get; set; }
        public String PREFAC_CAJA { get; set; }
        public String PREFAC_VENDEDOR { get; set; }
        public String PREFAC_LOCAL { get; set; }
        public Int16 PREFAC_ARQUEO { get; set; }
        public Decimal PREFAC_DESCUENTO { get; set; }
        public Decimal PREFAC_CONIVA { get; set; }
        public Decimal PREFAC_SINIVA { get; set; }
        public List<DtoDetallePrefactura> DetallePreFactura { get; set; }


    }
}
