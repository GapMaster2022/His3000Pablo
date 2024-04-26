using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
   public class DtoCuentaCargas
    {
        public string ATENCION { get; set; }
        public DateTime FECHA { get; set; }
        public string CODIGO { get; set; }
        //public string CODIGO_SECCION { get; set; }
        public string DESCRIPCION { get; set; }
        public double VALOR_UNITARIO { get; set; }
        public int CANTIDAD { get; set; }
        public double VALOR_TOTAL { get; set; }
        public string AREA { get; set; }
    }
}
