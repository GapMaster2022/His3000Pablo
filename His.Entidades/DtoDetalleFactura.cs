using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoDetalleFactura
    {

        public Int64 COD_FDETALLE { get; set; }
        public Int64 FAC_CODIGO { get; set; }
        public String DET_DESCIPCION { get; set; }
        public Decimal DET_VALOR { get; set; }
        public Int16 DET_ESTADO { get; set; }

    }
}