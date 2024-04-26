using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoDetalleHonorariosMedicos
    {
        public Int64 PED_CODIGO { get; set; }
        public Int64 ID_LINEA { get; set; }
        public String CODPRO { get; set; }
        public Int32 MED_CODIGO { get; set; }
        public string MED_NOMBRE { get; set; }
        public string MED_ESPECIALIDAD { get; set; }
        public DateTime FECHA { get; set; }
        public Decimal VALOR { get; set; }
        public String CODIGO2 { get; set; }

        public string numdoc { get; set; }

    }
}
