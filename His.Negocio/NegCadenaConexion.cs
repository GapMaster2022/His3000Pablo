using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;

namespace His.Negocio
{
    public class NegCadenaConexion
    {
        public static string cadenaConexionBaseDatos()
        {
            return new DatCadenaConexion().cadenaConexionBaseDatos();
        }
        public static string cadenaConexionServidor()
        {
            return new DatCadenaConexion().cadenaConexionServidor();
        }
    }
}
