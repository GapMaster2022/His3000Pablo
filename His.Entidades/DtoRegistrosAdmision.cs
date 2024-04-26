using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public  class DtoRegistrosAdmision
    {
        public Int64 ate_codigo { get; set; }
        public DateTime ingreso { get; set; }
        public int edad { get; set; }
        public string referido { get; set; }
        public string admisionista { get; set; }
        public Int64 pac_codigo { get; set; }
        public int admision { get; set; }
    }
}
