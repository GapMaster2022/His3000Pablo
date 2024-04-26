using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoAnamnesis
    {
        public Int16 ANE_CODIGO { get; set; }
        public Int16 PAC_CODIGO { get; set; }
        public Int16 ATE_CODIGO { get; set; }
        public string ANE_PROBLEMA { get; set; }
        public string ANE_FREC_CARDIACA { get; set; }
        public string ANE_FREC_RESPIRATORIA { get; set; }
        public Int16 ANE_TEMP_BUCAL { get; set; }
        public Int16 ANE_TEMP_AXILAR { get; set; }
        public Double ANE_PESO { get; set; }
        public Double ANE_TALLA { get; set; }
        public Double ANE_PERIMETRO { get; set; }
        public string ANE_PLAN_TRATAMIENTO { get; set; }
        public DateTime ANE_FECHA { get; set; }
        public Int16 ANE_PRESION_A { get; set; }
        public Int16 ANE_PRESION_B { get; set; }
        public Int16 ID_USUARIO { get; set; }
        public string ENTITYSETNAME { get; set; }
        public string ENTITYID { get; set; }
    }
}
