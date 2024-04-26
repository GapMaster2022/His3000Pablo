using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades.Clases
{
    public static class Sesion
    {
        public static Int32 codMedico { get; set; }
        public static Int16 codUsuario { get; set; }
        public static Acceso TipoAcceso { get; set; }
        public static decimal porIva { get; set; }
        public static Int16 codLocal { get; set; }
        public static Int16 codDepartamento { get; set; }
        public static int codHabitacion { get; set; }
        public static string numHabitacion { get; set; }
        //variables de conexion a la base de datos
        public static string servidor { get; set; }
        public static string baseDatos { get; set; }
        public static string usr { get; set; }
        public static string pwd { get; set; }
        //datos generales
        public static string nomUsuario { get; set; }
        public static string nomEmpresa { get; set; }
        public static string nomDepartamento { get; set; }
        public static string nomCaja { get; set; }
        public static int TipoBusquedaHabitacion { get; set; }
        public static int bodega { get; set; }
        public static int bodega_reposicion { get; set; }
        public static string ip { get; set; }
        public static string unicodigo { get; set; }
    }
}
