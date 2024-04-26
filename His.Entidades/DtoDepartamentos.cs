using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoDepartamentos
    {
        public string ENTITYSETNAME { get; set; }
        public string ENTITYID { get; set; }
        public Int16 DEP_CODIGO { get; set; }
        public string DEP_NOMBRE { get; set; }
        public Int16 EMP_CODIGO { get; set; }
        public string EMP_NOMBRE { get; set; }
        public Int16 DEP_PADRE { get; set; }
        public bool DEP_ESTADO { get; set; }
    }
}
