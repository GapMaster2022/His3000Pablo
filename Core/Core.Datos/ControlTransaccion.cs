using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Entidades;

namespace Core.Datos
{
    /// <summary>    
    /// Descripción: Objeto que contiene la transacción a utilizar
    /// </summary>   
    public class ControlTransaccion
    {
        /// <summary>
        /// Atributo que contiene la transacción
        /// </summary>
        private DbTransaction transaccion;
        /// <summary>
        /// Atributo que contiene la conexión
        /// </summary>
        private Conexion conexion;

        /// <summary>
        /// Propiedad que expone la transacción
        /// </summary>
        public DbTransaction TransaccionBdd
        {
            get { return this.transaccion; }
        }

        /// <summary>
        /// Propiedad que expone la conexión
        /// </summary>
        public DbConnection ConexionBDD
        {
            get { return this.conexion.ConexionBDD; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unaConexion">Objeto Conexión</param>
        public ControlTransaccion(DbConnection unaConexion, BaseDatos baseDatos)
        {
            this.conexion = new Conexion(unaConexion, baseDatos);
        }


        /// <summary>
        /// Inicio de Transacción
        /// </summary>
        public void BeginTransaction()
        {
            this.transaccion = this.conexion.ConexionBDD.BeginTransaction();
        }

        /// <summary>
        /// Commit de Transacción
        /// </summary>
        public void Commit()
        {
            this.transaccion.Commit();
        }


        /// <summary>
        /// Rollback
        /// </summary>
        public void Rollback()
        {
            this.transaccion.Rollback();
        }

        /// <summary>
        /// Cerrar conexión
        /// </summary>
        public void Close()
        {
            this.conexion.Close();
        }
    }
}
