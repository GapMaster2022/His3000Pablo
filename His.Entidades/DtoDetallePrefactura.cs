using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoDetallePrefactura
    {

        public Int32 COD_PFDETALLE { get; set; }
        public String PREFAC_NUMERO { get; set; }
        public String DET_DESCIPCION  { get; set; }
        public Decimal DET_VALOR { get; set; }
        public Int16 DET_ESTADO { get; set; }

    }
}
