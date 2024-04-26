using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.Odbc;
using System.Data.OleDb;
using Core.Entidades;

namespace Core.Datos
{
   
    internal class ProveedorCadenaConexion
    {
       
       


        //Atributo que contiene la lectura de conexiones del archivo        
        private static List<KeyValuePair<BaseDatos, CadenaConexion>> htConneccionStringPool = new List<KeyValuePair<BaseDatos, CadenaConexion>>();

        /// <summary>
        /// Obtener cadena de conexión según el módulo
        /// </summary>
        /// <param name="modulo">Modulo del Proyecto</param>
        /// <returns>Cadena de Conexión</returns>
        public static CadenaConexion ObtenerCadenaConexion(BaseDatos modulo)
        {
            CadenaConexion configConexion = new CadenaConexion();

            if (htConneccionStringPool.Where(con => con.Key == modulo).ToList().Count== 0)
            {
                configConexion = Leer(modulo);
                Add(modulo, configConexion);
            }
            else
            {
                configConexion = (CadenaConexion)htConneccionStringPool.Where(con => con.Key == modulo).FirstOrDefault().Value;
            }

            return configConexion;
        }

        private static CadenaConexion Leer(BaseDatos modulo)
        {
            CadenaConexion configConexion = new CadenaConexion();

            //Cadena de Conexión a Infraestructura, única en Archivo de Configuración
            string proveedorCadenaInfraestructura = LeerCadenaConexion(modulo.ToString());

            
            configConexion.CadenaConexionBDD = proveedorCadenaInfraestructura;

            //Retorna ConfiguracionConexion solicitada
            return configConexion;
        }

        private static string LeerCadenaConexion(string p)
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[p].ConnectionString;
            }
            catch (NullReferenceException)
            {
                throw new Exception(string.Format("{0} no existe", p));
            }
        }


        private static void Add(BaseDatos m, CadenaConexion c)
        {
            htConneccionStringPool.Add(new KeyValuePair<BaseDatos, CadenaConexion>(m,c));
        }
    }
}
