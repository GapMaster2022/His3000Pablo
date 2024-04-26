using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoParametros
    {
        public Int16 PAR_CODIGO { get; set; }
        public Int16 PAD_CODIGO { get; set; }
        public string PAD_NOMBRE { get; set; }
        public string PAD_TIPO { get; set; }
        public string PAD_VALOR { get; set; }
        public bool PAD_ACTIVO { get; set; }
        public string ENTITYSETNAME { get; set; }
        public string ENTITYID { get; set; }
    }
}
