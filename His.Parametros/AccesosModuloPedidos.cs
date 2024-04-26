using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public class AccesosModuloPedidos
    {
        #region Metodos
        public static bool herramienta = false;
        public static bool controlDespscho = false;
        public static bool exploradorPedidos = false;
        public static bool monitoreoPedidos = false;
        public static bool monitoreoDevoluciones = false;
        public static bool reportes = false;
        public static bool consultaPedidos = false;

        #endregion
        #region get y set
        public static bool Herramienta
        {
            get { return herramienta; }
            set { herramienta = value; }
        }
        public static bool ControlDespscho
        {
            get { return controlDespscho; }
            set { controlDespscho = value; }
        }
        public static bool ExploradorPedidos
        {
            get { return exploradorPedidos; }
            set { exploradorPedidos = value; }
        }
        public static bool MonitoreoPedidos
        {
            get { return monitoreoPedidos; }
            set { monitoreoPedidos = value; }
        }
        public static bool MonitoreoDevoluciones
        {
            get { return monitoreoDevoluciones; }
            set { monitoreoDevoluciones = value; }
        }
        public static bool Reportes
        {
            get { return reportes; }
            set { reportes = value; }
        }
        public static bool ConsultaPedidos
        {
            get { return consultaPedidos; }
            set { consultaPedidos = value; }
        }
        #endregion
    }
}
