using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoCajas
    {
        public Int16 CAJ_CODIGO { get; set; }
        public Int16 LOC_CODIGO { get; set; }
        public string LOC_NOMBRE { get; set; }
        public string CAJ_NOMBRE { get; set; }
        public DateTime CAJ_FECHA { get; set; }
        public string CAJ_NUMERO { get; set; }
        public string CAJ_AUTORIZACION_SRI { get; set; }
        public DateTime CAJ_PERIDO_VALIDEZ { get; set; }
        public bool CAJ_ESTADO { get; set; }
        public string ENTITYSETNAME { get; set; }
        public string ENTITYID { get; set; }
    }
}
