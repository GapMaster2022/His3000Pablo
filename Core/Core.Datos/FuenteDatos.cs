using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Entidades;
using System.Data;
using System.Reflection;
using System.Data.Objects.DataClasses;
using System.Configuration;

namespace Core.Datos
{
    /// <summary>
    /// Clase para el manejo de la fuente de datos
    /// </summary>
    public class FuenteDatos
    {

        #region "Declaración de Variables"

      

        //Proveedor que permite ejecutar los store procedures de acuerdo a una base de datos
        private Proveedor proveedor;

        //Conexión a la base de datos
        private Conexion conexion;

        //Objeto transaccional cuando se va a realizar una aplicación en 1 capa fisica (Eje. Dispositivos móbiles)
        private ControlTransaccion transaccion;
       
        string sentencia = string.Empty;
  
        BaseDatos baseDatos ;
        #endregion

        #region Constructores


        /// <summary>
        /// Contiene transaciconalidad
        /// </summary>
        /// <param name="unaBaseDatos"></param>
        /// <param name="unprocedimiento"></param>
        /// <param name="unatransaccion"></param>
        public FuenteDatos()
        {
            string baseDat =ConfigurationManager.AppSettings["Base"].ToString();
            switch (baseDat)
            {
                case "SqlServer":
                    baseDatos = BaseDatos.SqlServer;
                    break;
            }


            this.proveedor = new Proveedor(baseDatos);
            this.InicializaConexion();
            this.transaccion = new ControlTransaccion(this.conexion.ConexionBDD, baseDatos);
        }
       

       

         /// <summary>
        /// Permite obtener la transaccionalidad
        /// </summary>
        public ControlTransaccion TransaccionBdd
        {
            get { return this.transaccion; }
        }

        #endregion

      

        /// <summary>
        /// Método que inicializa la conexión
        /// </summary>
        private void InicializaConexion()
        {
            this.conexion = new Conexion(baseDatos);
        }

        /// <summary>
        /// Método que permite abrir la conexión a la base de datos y utiliza transaccionalidad en 
        /// caso de que se use el escenario adecuado (Eje: PDA)
        /// </summary>
        public void Open()
        {
            if (this.conexion == null)
                InicializaConexion();

            this.conexion.Open();

          
        }

        /// <summary>
        /// Método que permite cerrar la conexión a la base de datos        
        /// </summary>
        public void Close()
        {
            if (this.transaccion.TransaccionBdd == null)
                this.conexion.Close();
        }


        #region Publicos Create
        /// <summary>
        /// Método que permite crear datos ejecutanto el sp
        /// </summary>
        /// <param name="ArrObject">Arreglo de Parametros del Store procedura</param>
        /// <returns>Retorna el Id para el catalogo creado</returns>
        public int Crear(Object[] ArrObject, string procedimiento)
        {
                 
            try
            {
                this.Open();
                object obj =null;
                if(transaccion!=null)
                    obj = proveedor.ExecuteScalar(transaccion.TransaccionBdd, procedimiento, ArrObject);
                else
                    obj = proveedor.ExecuteScalar(this.conexion.ConexionBDD, procedimiento, ArrObject);

                return int.Parse(obj.ToString());
            
            }
            catch (Exception E)
            {
                throw E;
            }
            finally
            {
                this.Close();
            }
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        internal int Actualizar(Object[] ArrObject, string procedimiento)
        {
            int ret;
           
            try
            {
                this.Open();
          
                if (this.transaccion.TransaccionBdd == null)
                {

                    ret = proveedor.ExecuteNonQuery(this.conexion.ConexionBDD, procedimiento, ArrObject);
                }
                else
                {
                    ret = proveedor.ExecuteNonQuery(this.transaccion.TransaccionBdd, procedimiento, ArrObject);
                }


             
            }
            catch (Exception E)
            {
                throw E;
            }
            finally
            {
                this.Close();
            }

            return ret;
        }



        /// <summary>
        /// Permite leer una Lista de objetos genéricos T, de acuerdo al id dentro de la tabla
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<T> Leer<T>(object id, string procedimiento) where T : new()
        {
            List<T> list = null;

            try
            {
                this.Open();
           

                Object[] objParms = new Object[1];
                objParms[0] = id;

                if (this.transaccion.TransaccionBdd == null)
                    list = proveedor.ExecuteReader<T>(this.conexion.ConexionBDD, procedimiento, objParms);
                else
                    list = proveedor.ExecuteReader<T>(this.transaccion.TransaccionBdd, procedimiento, objParms);

            }
            catch (Exception E)
            {
                throw E;
            }
            finally
            {
                this.Close();
            }

            return list;
        }



       



        #endregion


        #region Otros

        /// <summary>
        /// Método que permite ejecutar un store procedure para recuperar al final un dato primitivo
        /// este mejora el performance
        /// </summary>
        /// <param name="SP_Key">Nombre que se va a asignar al store procedure</param>
        /// <param name="Args">Argumentos que se envían al store procedure</param>
        /// <returns>Retorna cualquier primitivo en un object</returns>
        public object EjecutarScalar(string SP_Key, string procedimiento,params System.Object[] Args)
        {
            try
            {
                this.Open();

            

                if (this.transaccion.TransaccionBdd == null)
                    return proveedor.ExecuteScalar(this.conexion.ConexionBDD, procedimiento, Args);
                else
                    return proveedor.ExecuteScalar(this.transaccion.TransaccionBdd, procedimiento, Args);

            }
            catch (Exception E)
            {
                throw E;
            }
            finally
            {
                this.Close();
            }
        }


        /// <summary>
        ///  Método que permite ejecutar un store procedure para recuperar al final un dato primitivo
        /// </summary>
        /// <param name="SP_Key">Nombre que se va a asignar al store procedur</param>
        /// <returns>Retorna cualquier primitivo en un object</returns>
        public object EjecutarEscalar(string SP_Key,string procedimiento)
        {
            try
            {
                this.Open();

              

                if (this.transaccion.TransaccionBdd == null)
                    return proveedor.ExecuteScalar(this.conexion.ConexionBDD, procedimiento, null);
                else
                    return proveedor.ExecuteScalar(this.transaccion.TransaccionBdd, procedimiento, null);

            }
            catch (Exception E)
            {
                throw E;
            }
            finally
            {
                this.Close();
            }
        }

        /// <summary>
        /// Método que permite ejecutar un store procedure enviando el nombre del store procedure a ejecutar
        /// </summary>
        /// <param name="procedimiento">Nombre del store procedure</param>
        /// <param name="Args">Argumentos enviados al store procedure</param>
        /// <returns>Retorna el número de filas afectadas</returns>
        public void Ejecutar(string procedimiento, params System.Object[] Args)
        {
            try
            {

                this.Open();

                if (this.transaccion.TransaccionBdd == null)
                    proveedor.ExecuteScalar(this.conexion.ConexionBDD, procedimiento, Args);
                else
                    proveedor.ExecuteScalar(this.transaccion.TransaccionBdd, procedimiento, Args);

            }
            catch (Exception E)
            {
                throw E;
            }
            finally
            {
                this.Close();
            }
        }


        /// <summary>
        /// Método que permite ejecutar un store procedure enviando el nombre del store procedure a ejecutar
        /// </summary>
        /// <param name="procedimiento">Nombre del store procedure</param>
        /// <param name="Args">Argumentos enviados al store procedure</param>
        /// <returns>Retorna el número de filas afectadas</returns>
        public DataSet EjecutarDataSet(string procedimiento, params System.Object[] Args)
        {
            try
            {
                this.Open();

                if (this.transaccion.TransaccionBdd == null)
                    return proveedor.ExecuteDataset(this.conexion.ConexionBDD, procedimiento, Args);
                else
                    return proveedor.ExecuteDataset(this.transaccion.TransaccionBdd, procedimiento, Args);

            }
            catch (Exception E)
            {
                throw E;
            }
            finally
            {
                this.Close();
            }
        }

        /// <summary>
        /// Método que permite ejecutar un store procedure enviando el nombre del store procedure a ejecutar
        /// </summary>
        /// <param name="procedimiento">Nombre del store procedure</param>
        /// <returns>Retorna el número de filas afectadas</returns>
        public int Ejecutar(string procedimiento)
        {
            try
            {
                this.Open();

                if (this.transaccion.TransaccionBdd == null)
                    return proveedor.ExecuteNonQuery(this.conexion.ConexionBDD, procedimiento, null);
                else
                    return proveedor.ExecuteNonQuery(this.transaccion.TransaccionBdd, procedimiento, null);

            }
            catch (Exception E)
            {
                throw E;
            }
            finally
            {
                this.Close();
            }
        }



        /// <summary>
        /// Método que permite ejecutar un Sql
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="strUnSql">Sql enviado</param>
        /// <returns>retorna una lista tipificada</returns>
        public List<T> EjecutarSql<T>(string strUnSql) where T : new()
        {
            List<T> list = null;

            try
            {
                this.Open();

                if (this.transaccion.TransaccionBdd == null)
                    list = proveedor.ExecuteReader<T>(this.conexion.ConexionBDD, strUnSql);
                else
                    list = proveedor.ExecuteReader<T>(this.transaccion.TransaccionBdd, strUnSql);

            }
            catch (Exception E)
            {
                throw E;
            }
            finally
            {
                this.Close();
            }

            return list;
        }


        /// <summary>
        /// Permite ejecutar un query sql para los catalogos
        /// </summary>
        /// <param name="strUnSql">Query sql</param>
        /// <returns>retorna el dataset con información</returns>
        public DataSet EjecutarSql(string strUnSql)
        {
            try
            {
                this.Open();

                DataSet ds = null;

                if (this.transaccion.TransaccionBdd == null)
                    ds = proveedor.ExecuteDataset(this.conexion.ConexionBDD, strUnSql);
                else
                    ds = proveedor.ExecuteDataset(this.transaccion.TransaccionBdd, strUnSql);

                return ds;
            }
            catch (Exception E)
            {
                throw E;
            }
            finally
            {
                this.Close();
            }
        }


        /// <summary>
        /// Método que permite ejecutar un proceso y obtener una lista de objetos tipificados
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="SP_Key">Nombre a asignarse al store procedure</param>
        /// <param name="Args">Argumentos enviados al store procedure</param>
        /// <returns>retorna una lista de objetos tipificados</returns>

        public List<T> EjecutarProceso<T>(string procedimiento,params System.Object[] Args) where T : new()
        {

            List<T> list = null;

            try
            {
                this.Open();
                

                if (this.transaccion.TransaccionBdd == null)
                    list = proveedor.ExecuteReader<T>(this.conexion.ConexionBDD, procedimiento, Args);
                else
                    list = proveedor.ExecuteReader<T>(this.transaccion.TransaccionBdd, procedimiento, Args);

            }
            catch (Exception E)
            {
                throw E;
            }
            finally
            {
                this.Close();
            }

            return list;
        }


        /// <summary>
        /// Método que permite ejecutar un proceso y retornar una lista de objetos tipificados
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="SP_Key">Nombre que se va a asignar al store procedure</param>
        /// <returns>retorna una lista de objetos tipificados</returns>
        public List<T> LeerProceso<T>(string SP_Key) where T : new()
        {
            return this.LeerProceso<T>(SP_Key);
        }




        /// <summary>
        /// Obtiene el arreglo de una entidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private Object[] ObtenerArray<T>(EntityObject obj)
        {
            PropertyInfo[] fieldInfos = obj.GetType().GetProperties();
            Object[] objArray = new Object[fieldInfos.Length - 2];

            for (int i = 0; i < fieldInfos.Length; i++)
            {
                if (fieldInfos[i].Name != "EntityKey" && fieldInfos[i].Name != "EntityState")
                    objArray[i] = fieldInfos[i].GetValue(obj, null);
            }

            return objArray;
        }


        #endregion


        #region Destructor

        /// <summary>
        /// 
        /// </summary>
        ~FuenteDatos()
        {
            GC.SuppressFinalize(this);
        }



        #endregion


       
    }
}
