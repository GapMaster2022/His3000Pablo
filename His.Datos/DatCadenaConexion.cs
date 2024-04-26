using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;

namespace His.Datos
{
    public class DatCadenaConexion
    {
        public string cadenaConexionBaseDatos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.Connection.Database;
            }
        }
        public string cadenaConexionServidor()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.Connection.DataSource;
            }
        }
    }
}
