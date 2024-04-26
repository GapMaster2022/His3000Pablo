using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoLocales
    {
        public Int16 LOC_CODIGO { get; set; }
        public Int16 CODZONA { get; set; }
        public string NOMZONA { get; set; }
        public Int16 CODTIPNEG { get; set; }
        //public Int16 EMP_CODIGO { get; set; }
        public string LOC_NOMBRE { get; set; }
        public string LOC_DIRECCION { get; set; }
        public string LOC_TELEFONO { get; set; }
        public string LOC_RUC { get; set; }
        public float LOC_AREA { get; set; }
        public Int16 CODCIUDAD { get; set; }
        public Int16 ID_USUARIO { get; set; }
        public string LOC_TEL1 { get; set; }
        public string LOC_TEL2 { get; set; }
        public string LOC_FAX { get; set; }
        public Int16 LOC_NUMEMPLE { get; set; }
        public bool LOC_BODEGA { get; set; }
        public bool LOC_PRINCIPAL { get; set; }
        public int LOC_PRIORIDAD { get; set; }
        public int LOC_PORCENTAJE_DIS { get; set; }
        public bool LOC_MATRIZ { get; set; }
        public string ENTITYSETNAME { get; set; }
        public string ENTITYID { get; set; }

    }
}
