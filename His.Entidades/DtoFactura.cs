using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoFactura
    {

        public Int64 FAC_CODIGO { get; set; }
        public Int64 ATE_CODIGO { get; set; }
        public string FAC_NUMERO { get; set; }
        public Int64 FAC_AUTORIZACION { get; set; }
        public DateTime FAC_FECHA { get; set; }
        public string CLI_NOMBRE { get; set; }
        public string CLI_RUC { get; set; }
        public string CLI_TELEFONO { get; set; }
        public decimal FAC_TOTAL { get; set; }
        public decimal FAC_SUBTOTAL { get; set; }
        public decimal FAC_IVAUNO { get; set; }
        public decimal FAC_IVADOS { get; set; }
        public decimal FAC_IVATRES { get; set; }
        public string FAC_ESTADO { get; set; }
        public string FAC_CAJA { get; set; }
        public string FAC_VENDEDOR { get; set; }
        public string FAC_LOCAL { get; set; }
        public Int16 FAC_ARQUEO { get; set; }
        public decimal FAC_DESCUENTO { get; set; }
        public List<DtoDetalleFactura> DetalleFactura { get; set; }

    }
}

