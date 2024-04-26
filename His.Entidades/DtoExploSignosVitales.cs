using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoExploSignosVitales
    {
        public Int32 CODIGO { get; set; }
        public string FECHA { get; set; }
        public string HORA { get; set; }
        public string P_ARTERIAL { get; set; }
        public string F_CARDIACA { get; set; }
        public string F_RESPIRATORIA { get; set; }
        public string TEMPERATURA { get; set; }
        public string S_OXIGENO { get; set; }
        public string P_SISTONICA { get; set; }
        public string P_DIASTONICA { get; set; }
        public string MEDICO { get; set; }

    }
}
