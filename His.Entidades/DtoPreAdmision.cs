using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoPreAdmision
    {
        public Int64 CODIGO { get; set; }
        public string NOMBRE { get; set; }
        public string ID { get; set; }
        public string F_INGRESO { get; set; }
        public string TIPO_TRATAMIENTO { get; set; }
        public Int32 TIA_CODIGO { get; set; }
        public string PRIORIDAD { get; set; }
    }
}
