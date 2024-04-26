using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public class AccesosModuloConsultaExterna
    {
        #region variables
        private static bool agendamiento = false;
        private static bool agendaPaciente = false;
        private static bool explCitasMedicas = false;

        private static bool consultaExterna = false;
        private static bool admision = false;
        private static bool triaje = false;
        private static bool signosViatles = false;
        private static bool habitaciones = false;
        private static bool consulta = false;
        private static bool facturacion = false;
        private static bool expConsultaExterna = false;
        private static bool explCertificado = false;
        private static bool explReceta = false;

        #endregion
        #region Metodos get y set
        public static bool Agendamiento
        {
            get { return agendamiento; }
            set { agendamiento = value; }
        }
        public static bool AgendaPacientes
        {
            get { return agendaPaciente; }
            set { agendaPaciente = value; }
        }
        public static bool ExpCitasMedicas
        {
            get { return explCitasMedicas; }
            set { explCitasMedicas = value; }
        }
        public static bool ConsultaExterna
        {
            get { return consultaExterna; }
            set { consultaExterna = value; }
        }
        public static bool Admision
        {
            get { return admision; }
            set { admision = value; }
        }
        public static bool Triaje
        {
            get { return triaje; }
            set { triaje = value; }
        }
        public static bool SignosVitales
        {
            get { return signosViatles; }
            set { signosViatles = value; }
        }
        public static bool Habitaciones
        {
            get { return habitaciones; }
            set { habitaciones = value; }
        }
        public static bool Consulta
        {
            get { return consulta; }
            set { consulta = value; }
        }
        public static bool Facturacion
        {
            get { return facturacion; }
            set { facturacion = value; }
        }
        public static bool ExpConsultaExterna
        {
            get { return expConsultaExterna; }
            set { expConsultaExterna = value; }
        }
        public static bool ExpCertificado
        {
            get { return explCertificado; }
            set { explCertificado = value; }
        }
        public static bool ExpReceta
        {
            get { return explReceta; }
            set { explReceta = value; }
        }
        #endregion
    }
}
