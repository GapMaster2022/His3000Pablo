using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public class EmergenciaPAR
    {
        private static Int16 codigoEmergencia = 1;

        public static Int16 CodigoEmergencia
        {
            get { return codigoEmergencia; }
            set { codigoEmergencia = value; }
        }
    }
}
