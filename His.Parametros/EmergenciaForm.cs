using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public class EmergenciaForm
    {
        private static int codigoAccidentes = 46;
        private static int codigoAntecedentePF = 47;
        private static int codigoExaFD = 48;
        private static int codigoLocalizacionL = 49;
        private static int codigoSolicitudExa = 50;
        private static int codigoAlta = 51;
        private static int codigoEnfActual = 52;
        private static int codigoLugarEvento = 53;


        public static int CodigoAccidentes
        {
            get { return codigoAccidentes; }
            set { codigoAccidentes = value; }
        }

        public static int CodigoAntecedentePF
        {
            get { return codigoAntecedentePF; }
            set { codigoAntecedentePF = value; }
        }

        public static int CodigoExaFD
        {
            get { return codigoExaFD; }
            set { codigoExaFD = value; }
        }

        public static int CodigoSolicitudExa
        {
            get { return codigoSolicitudExa; }
            set { codigoSolicitudExa = value; }
        }

        public static int CodigoAlta
        {
            get { return codigoAlta; }
            set { codigoAlta = value; }
        }

        public static int CodigoEnfActual
        {
            get { return codigoEnfActual; }
            set { codigoEnfActual = value; }
        }
        
        
        public static int CodigoLocalizacionL
        {
            get { return codigoLocalizacionL; }
            set { codigoLocalizacionL = value; }
        }

        public static int CodigoLugarEvento
        {
            get { return codigoLugarEvento; }
            set { codigoLugarEvento = value; }
        }
    }
}
