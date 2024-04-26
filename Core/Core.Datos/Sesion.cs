using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Datos
{
    public static class Sesion
    {
        public static Int32 codMedico { get; set; }
        public static Int16 codUsuario { get; set; }
        public static decimal porIva { get; set; }
        public static Int16 codLocal { get; set; }
        public static int codHabitacion { get; set; }
        public static string numHabitacion { get; set; }
        //variables de conexion a la base de datos
        public static string nombreServidor { get; set; }
        public static string nombreBaseDatos { get; set; }
        public static string usrBaseDatos { get; set; }
        public static string pwdBaseDatos { get; set; }
    }
}
