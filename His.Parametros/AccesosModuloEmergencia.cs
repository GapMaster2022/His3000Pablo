using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public class AccesosModuloEmergencia
    {
        #region Metodos
        public static bool emergencia = false;
        public static bool triaje = false;
        public static bool formulario = false;
        public static bool evolucion = false;
        public static bool certificado = false;
        public static bool receta = false;

        #endregion
        #region get y set
        public static bool Emergencia
        {
            get { return emergencia; }
            set { emergencia = value; }
        }
        public static bool Triaje
        {
            get { return triaje; }
            set { triaje = value; }
        }
        public static bool Formulario
        {
            get { return formulario; }
            set { formulario = value; }
        }
        public static bool Evolucion
        {
            get { return evolucion; }
            set { evolucion = value; }
        }
        public static bool Certificado
        {
            get { return certificado; }
            set { certificado = value; }
        }
        public static bool Receta
        {
            get { return receta; }
            set { receta = value; }
        }
        #endregion
    }
}
