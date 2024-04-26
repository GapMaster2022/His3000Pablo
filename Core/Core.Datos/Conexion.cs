using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.EntityClient;
using Core.Entidades;

namespace Core.Datos
{
    /// <summary>    
    /// Descripción: Objeto que contiene la conexión a la base de datos
    /// </summary>
    internal class Conexion
    {
        private DbConnection conexionBDD;

        ///// <summary>
        ///// Provee un nuevo acceso a la conexión a Bases de Datos
        ///// </summary>
        public DbConnection ConexionBDD
        {
            get
            {
                return conexionBDD;
            }
            set { conexionBDD = value; }
        }

        /// <summary>
        /// Provee un nuevo acceso a la conexión al Modelo de Entidades
        /// </summary>
        public EntityConnection ConexionEDM
        {
            get
            {
                return new EntityConnection(cadenaConexion.CadenaConexionEDM);
            }
        }
        
        /// <summary>
        /// Permite crear una fabrica a cualquier base de datos
        /// </summary>
        private DbProviderFactory fabrica = null;

        /// <summary>
        /// Contiene la(s) cadena(s) de conexión a la fuente de datos
        /// </summary>
        private CadenaConexion cadenaConexion;
        BaseDatos baseDatos;
        #region Constructores
        /// <summary>
        /// Contructor del objeto recibe una Conexión
        /// </summary>
        /// <param name="unaConneccion">Objeto Common que contiene la conexion</param>
        /// <param name="unaBasededatos">Base de Datos</param>
        public Conexion(DbConnection unaConneccion, BaseDatos unaBasededatos)
        {
            this.ConexionBDD = unaConneccion;
            baseDatos = unaBasededatos;
        }

        /// <summary>
        /// Contructor del objeto enviando el módulo a conectar
        /// </summary>
        /// <param name="modulo">Modulo</param>
        public Conexion(BaseDatos modulo)
        {
            this.cadenaConexion = ProveedorCadenaConexion.ObtenerCadenaConexion(modulo);

        }

        #endregion

        /// <summary>
        /// Método que permite crear la fabrica
        /// </summary>
        private void CreaFabrica()
        {
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    this.fabrica = DbProviderFactories.GetFactory("System.Data.SqlClient");
                    break;
                case BaseDatos.Odbc:
                    this.fabrica = DbProviderFactories.GetFactory("System.Data.Odbc");
                    break;
                case BaseDatos.OleDB:
                    this.fabrica = DbProviderFactories.GetFactory("System.Data.OleDb");
                    break;
                case BaseDatos.Oracle:
                    this.fabrica = DbProviderFactories.GetFactory("System.Data.OracleClient");
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Método que permite abrir la conexión
        /// </summary>
        public void Open()
        {

            if (this.conexionBDD == null)
            {
                this.CreaFabrica();
                this.conexionBDD = this.fabrica.CreateConnection();
                this.conexionBDD.ConnectionString = cadenaConexion.CadenaConexionBDD;
            }

            if (this.conexionBDD.State.Equals(ConnectionState.Closed))
                this.conexionBDD.Open();
        }

        /// <summary>
        /// Método que permite cerrar la conexión
        /// </summary>
        public void Close()
        {
            this.ConexionBDD.Close();
        }
    }
}
