using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public class ReporteFormularios
    {
        private static int codigoCatAPersonales = 2; //Se crea la variable que registra el codigo 2 del catalogo Andrés 20240315
        private static int codigoCatAFam = 3;
        private static int codigoCatOSistemas = 4;
        private static int codigoCatExamFisic = 5;

        public static int codigoCatalogo_AFamiliares
        {
            get { return codigoCatAFam; }
            set { codigoCatAFam = value; }
        }

        public static int codigoCatalogo_OSistemas
        {
            get { return codigoCatOSistemas; }
            set { codigoCatOSistemas = value; }
        }

        public static int codigoCatalogo_ExamenFisico
        {
            get { return codigoCatExamFisic; }
            set { codigoCatExamFisic = value; }
        }
        public static int codigoCatalogo_APersonales
        {
            get { return codigoCatAPersonales; }
            set { codigoCatAPersonales = value; }

        }
    }

}