using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoLaboratorio
    {
        public long HISTORIA_CLINICA { get; set; }
        public DateTime FECHA { get; set; }
        public string APELLIDO { get; set; }
        public string NOMBRE { get; set; }
        public long NO_ORDEN { get; set; }
        public long AÑO_ORDEN { get; set; }
        public long COD_EXAMEN { get; set; } 
        public long IESS { get; set; }
        public long SOAT { get; set; }
        public string NOM_EXA { get; set; }
        public long COD_TARIFA { get; set; }
        public string NOM_TARIFA { get; set; }
        public decimal TARIFA { get; set; }
        public long COD_IESS  { get; set; }
        public decimal TAR_IESS { get; set; }
        public decimal TAR_DIFERENCIA { get; set; }
        public int CANTIDAD { get; set; }
        public decimal TOTAL { get; set; }
    }
}
