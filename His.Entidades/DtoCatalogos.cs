using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoCatalogos
    {
        public int HCC_CODIGO { get; set; }
        public int HCT_TIPO { get; set; }
        public string HCC_NOMBRE { get; set; }
        public bool HCC_ESTADO { get; set; }
        public string ENTITYSETNAME { get; set; }
        public string ENTITYID { get; set; }
    }
}
