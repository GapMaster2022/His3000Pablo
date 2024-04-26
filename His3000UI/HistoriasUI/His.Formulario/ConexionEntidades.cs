using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.EntityClient;
using Core.Datos;

namespace His.Formulario
{
    public class ConexionEntidades
    {
        /// <summary>
        /// Conexion al Modelo de Datos de Evaluadora
        /// </summary>
        /// <returns></returns>
        public static EntityConnection ConexionEDM
        {
            get
            {
                return BaseContextoDatos.ConexionEDM();
            }
            
        }
    }
}
