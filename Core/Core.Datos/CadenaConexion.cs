using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Datos
{
   /// <summary>
    /// Generación de la Conexión
    /// </summary>
    internal class CadenaConexion
    {
        /// <summary>
        /// Cadena de Conexión hacia una BDD
        /// </summary>
        public string CadenaConexionBDD { get; set; }
        /// <summary>
        /// Cadena de Conexión Modelo de Entidades
        /// </summary>
        public string CadenaConexionEDM { get; set; }
        /// <summary>
        /// Cadena de Conexión a FTP Servers
        /// </summary>
        public string CadenaConexionFTP { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CadenaConexion()
        {
        }
    }
}
