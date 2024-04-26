using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades.Servicios
{
    public class DtoLstIngresoSala
    {
        public int INT_CODIGO {get; set;}
        public DateTime INT_FECHA_INI {get; set;}
        public DateTime INT_FECHA_FIN {get; set;}
        public Int16 ID_USUARIO {get; set;}
        public string NOM_USUARIO { get; set; }
        public int MED_CODIGO {get; set;}
        public string NOM_MEDICO { get; set; }
        public bool INT_ESTADO {get; set;}
        public byte INT_TIPO {get; set;}
        public string NOM_TIPO { get; set; }
        public int ATE_CODIGO {get; set;}
    }
}
