using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Entidades;
using System.Data;
using System.Data.SqlClient;
using Core.Datos.Sql;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.OracleClient;
using Core.Datos.Odbc;
using Core.Datos.Oledb;
using Core.Datos.Oracle;
using System.Data.Common;
using System.Reflection;
using Core.Datos.MappingType;
using System.Xml;

namespace Core.Datos
{
    /// <summary>
    /// Clase que maneja el proveedor de la base de datos
    /// </summary>
    internal class Proveedor
    {
        BaseDatos baseDatos;
       /// <summary>
       /// Base de Datos
       /// </summary>
       /// <param name="baseDatos">Base de Datos</param>
        public Proveedor(BaseDatos unabaseDatos)
        {
            baseDatos = unabaseDatos;
        }


        #region UpdateDataset

        /// <summary>
        /// Descripción: Permite actualizar un dataset
        ///              Solo se encuentra implementado para Sql Server
        /// </summary>
        /// <param name="idbConeccion">Coneccion</param>
        /// <param name="strSelect">Select</param>
        /// <param name="ds">Dataset</param>
        /// <param name="strTableName">Nombre de la Tabla</param>

        public void UpdateDataset(IDbConnection idbConeccion, string strSelect, DataSet ds, string strTableName)
        {
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    SqlDataAdapter adaptadorSql = new SqlDataAdapter(strSelect, (SqlConnection)idbConeccion);
                    SqlCommandBuilder cmdBuilderSql = new SqlCommandBuilder(adaptadorSql);
                    UpdateDataset(cmdBuilderSql.DataAdapter.InsertCommand, cmdBuilderSql.DataAdapter.DeleteCommand, cmdBuilderSql.DataAdapter.UpdateCommand, ds, strTableName);
             
                    break;
                case BaseDatos.Odbc:
                    break;
                case BaseDatos.OleDB:
                    break;
                case BaseDatos.Oracle:
                    break;
                default:
                    break;
            }

        }
     

        /// <summary>
        /// Descripción: Permite actualizar un dataset
        ///              Solo se encuentra implementado para SQL Server
        /// </summary>
        /// <param name="cmdInsert"></param>
        /// <param name="cmdDelete"></param>
        /// <param name="cmdUpdate"></param>
        /// <param name="ds"></param>
        /// <param name="strTableName"></param>
     
        public void UpdateDataset(IDbCommand cmdInsert, IDbCommand cmdDelete, IDbCommand cmdUpdate, DataSet ds, string strTableName)
        {
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    SqlHelper.UpdateDataset((SqlCommand)cmdInsert, (SqlCommand)cmdDelete, (SqlCommand)cmdUpdate, ds, strTableName);
                    break;
                case BaseDatos.Odbc:
                    break;
                case BaseDatos.OleDB:
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region ExecuteDataset

        /// <summary>
        /// Descripción: Permite ejecutar un query enviado como texto
        /// </summary>
        /// <param name="dbConeccion"></param>
        /// <param name="strCommandText"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(DbConnection dbConeccion, string strCommandText)
        {
            
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    return SqlHelper.ExecuteDataset((SqlConnection)dbConeccion, CommandType.Text, strCommandText);                
                   
                case BaseDatos.Odbc:
                    return OdbcHelper.ExecuteDataset((OdbcConnection)dbConeccion, CommandType.Text, strCommandText);  
                   
                case BaseDatos.OleDB:
                    return OleDbHelper.ExecuteDataset((OleDbConnection)dbConeccion, CommandType.Text, strCommandText); 
                   
                case BaseDatos.Oracle:
                    return OracleHelper.ExecuteDataset((OracleConnection)dbConeccion, CommandType.Text, strCommandText);
                    
                default:
                    return new DataSet();
                    
            }

        }
        
        /// <summary>
        /// Descripción: Permite ejecutar un query enviado como texto 
        ///              Se lo utiliza cuando la transaccionalidad es controlada por el negocio y no por el TransactionScope
        /// </summary>
        /// <param name="dbTransaccion"></param>
        /// <param name="strCommandText"></param>
       
        /// <returns></returns>
        public DataSet ExecuteDataset(DbTransaction dbTransaccion, string strCommandText)
        {
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    return SqlHelper.ExecuteDataset((SqlTransaction)dbTransaccion, CommandType.Text, strCommandText);
                    
                case BaseDatos.Odbc:
                    return OdbcHelper.ExecuteDataset((OdbcTransaction)dbTransaccion, CommandType.Text, strCommandText);
                   
                case BaseDatos.OleDB:
                    return OleDbHelper.ExecuteDataset((OleDbTransaction)dbTransaccion, CommandType.Text, strCommandText);
                    
                case BaseDatos.Oracle:
                    return OracleHelper.ExecuteDataset((OracleTransaction)dbTransaccion, CommandType.Text, strCommandText);
                   
                default:
                    return new DataSet();
                   
            }


        }


        /// <summary>
        /// Descripción: Permite ejecutar un store procedure
        /// </summary>
        /// <param name="dbConeccion"></param>
        /// <param name="procedimiento"></param>
        /// <param name="objParms"></param>
        /// <param name="objParms"></param>
        /// <returns></returns>

        public DataSet ExecuteDataset(DbConnection dbConeccion, string procedimiento, params object[] objParms)
        {

            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    return SqlHelper.ExecuteDataset((SqlConnection)dbConeccion, procedimiento, objParms);
                   
                case BaseDatos.Odbc:
                    return OdbcHelper.ExecuteDataset((OdbcConnection)dbConeccion, procedimiento, objParms);
                   
                case BaseDatos.OleDB:
                    return OleDbHelper.ExecuteDataset((OleDbConnection)dbConeccion, procedimiento, objParms);
                   
                case BaseDatos.Oracle:
                    return OracleHelper.ExecuteDataset((OracleConnection)dbConeccion, procedimiento, objParms);
                   
                default:
                    return new DataSet();
            }

        }

        /// <summary>
        /// Método que permite ejecutar un store procedure y devolver un DataSet
        /// </summary>
        /// <param name="dbTransaccion"></param>
        /// <param name="procedimiento"></param>
        /// <param name="objParms"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(DbTransaction dbTransaccion, string procedimiento, params object[] objParms)
        {
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    return SqlHelper.ExecuteDataset((SqlTransaction)dbTransaccion, procedimiento, objParms);
                   
                case BaseDatos.Odbc:
                    return OdbcHelper.ExecuteDataset((OdbcTransaction)dbTransaccion, procedimiento, objParms);
                  
                case BaseDatos.OleDB:
                    return OleDbHelper.ExecuteDataset((OleDbTransaction)dbTransaccion, procedimiento, objParms);
                    
                case BaseDatos.Oracle:
                    return OracleHelper.ExecuteDataset((OracleTransaction)dbTransaccion, procedimiento, objParms);
                  
                default:
                    return new DataSet();
            }

        }

        #endregion


        #region ExecuteReader

        /// <summary>
        /// Método que permite realizar una lectura de datos a una lista tipificada con Transaccionalidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbTransaccion"></param>
        /// <param name="procedimiento"></param>
        /// <param name="objParms"></param>
        /// <returns></returns>
        public List<T> ExecuteReader<T>(DbTransaction dbTransaccion, string procedimiento, params object[] objParms) where T : new()
        {
            List<T> list = null;
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    using (DbDataReader reader = SqlHelper.ExecuteReader((SqlTransaction)dbTransaccion, procedimiento, objParms))
                        list = this.GetArrayFromList<T>(reader);
                    break;
                case BaseDatos.Odbc:
                    using (DbDataReader reader = OdbcHelper.ExecuteReader((OdbcTransaction)dbTransaccion, procedimiento, objParms))
                        list = this.GetArrayFromList<T>(reader);
                    break;
                case BaseDatos.OleDB:
                    using (DbDataReader reader = OleDbHelper.ExecuteReader((OleDbTransaction)dbTransaccion, procedimiento, objParms))
                        list = this.GetArrayFromList<T>(reader);
                    break;
                case BaseDatos.Oracle:
                    using (DbDataReader reader = OracleHelper.ExecuteReader((OracleTransaction)dbTransaccion, procedimiento, objParms))
                        list = this.GetArrayFromList<T>(reader);
                    break;
                default:
                    break;
            }
       

            return list;
        }

        /// <summary>
        /// Método que permite realizar una lectura de datos a una lista tipificada sin  transaccion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConeccion"></param>
        /// <param name="procedimiento"></param>
        /// <param name="objParms"></param>
        /// <returns></returns>
        public List<T> ExecuteReader<T>(DbConnection dbConeccion, string procedimiento, params object[] objParms) where T : new()
        {
            List<T> list = null;
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    using (DbDataReader reader = SqlHelper.ExecuteReader((SqlConnection)dbConeccion, procedimiento, objParms))
                        list = this.GetArrayFromList<T>(reader);
                    break;
                case BaseDatos.Odbc:
                    using (DbDataReader reader = OdbcHelper.ExecuteReader((OdbcConnection)dbConeccion, procedimiento, objParms))
                        list = this.GetArrayFromList<T>(reader);
                    break;
                case BaseDatos.OleDB:

                    using (DbDataReader reader = OleDbHelper.ExecuteReader((OleDbConnection)dbConeccion, procedimiento, objParms))
                        list = this.GetArrayFromList<T>(reader);
                    break;
                case BaseDatos.Oracle:
                    using (DbDataReader reader = OracleHelper.ExecuteReader((OracleConnection)dbConeccion, procedimiento, objParms))
                        list = this.GetArrayFromList<T>(reader);
                    break;
                default:
                    break;
            }
       
            return list;
        }

        /// <summary>
        /// Método que permite crear al objeto tipificado desde la lectura de un reader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        private List<T> GetArrayFromList<T>(DbDataReader reader) where T : new()
        {
            Type objectType = typeof(T);

            List<T> list = new List<T>();

            int rowNum = 0;

            while (reader.Read())
            {
                rowNum++;

                T obj = new T();

                for (int i = 0; i < reader.FieldCount; i++)
                {

                    string columnName = reader.GetName(i);
                    PropertyInfo propertyInfo = objectType.GetProperty(columnName);
                    object columnValue = reader[i];

                    if (columnValue is DBNull)
                        columnValue = null;

                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(obj, CSHelper.ConvertType(columnValue, propertyInfo.PropertyType), null);
                    }
                    else
                    {
                        FieldInfo fieldInfo = objectType.GetField(columnName);

                        if (fieldInfo != null)
                        {
                            fieldInfo.SetValue(obj, CSHelper.ConvertType(columnValue, fieldInfo.FieldType));
                        }
                    }

                }


                list.Add(obj);
            }

            return list;
        }

        #endregion

        /// <summary>
        /// Descripción: Permite retornar un xml desde la base de datos
        ///              Solo se encuentra implementado para SQL Server
        /// </summary>
        /// <param name="dbConeccion"></param>
        /// <param name="procedimiento"></param>
        /// <param name="objParms"></param>
        /// <returns></returns>
        public XmlReader ExecuteXmlReader(DbConnection dbConeccion, string procedimiento, params object[] objParms)
        {
            XmlReader reader = null;
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    return SqlHelper.ExecuteXmlReader((SqlConnection)dbConeccion, procedimiento, objParms);
                    
                case BaseDatos.Odbc:
                  
                    
                case BaseDatos.OleDB:

                    
                    
                case BaseDatos.Oracle:
                  
                   
                default:
                    return reader;
            }


        }

        /// <summary>
        /// Descripción: Permite retornar un xml desde la base de datos
        ///              Solo se encuentra implementado para SQL Server
        /// </summary>
        /// <param name="dbTransaccion"></param>
        /// <param name="procedimiento"></param>
        /// <param name="objParms"></param>
        /// <returns></returns>
        public XmlReader ExecuteXmlReader(DbTransaction dbTransaccion, string procedimiento, params object[] objParms)
        {
            XmlReader reader=null;
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    return SqlHelper.ExecuteXmlReader((SqlTransaction)dbTransaccion, procedimiento, objParms);
                   
                case BaseDatos.Odbc:

                 
                case BaseDatos.OleDB:


                  
                case BaseDatos.Oracle:

                   
                default:
                    return reader;
            }

        }

        /// <summary>
        /// Método que permite ejecutar un store procedure y obtener un objeto, es más eficiente
        /// </summary>
        /// <param name="dbTransaccion"></param>
        /// <param name="procedimiento"></param>
        /// <param name="objParms"></param>
        /// <returns></returns>
        public object ExecuteScalar(DbTransaction dbTransaccion, string procedimiento, params object[] objParms)
        {
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    return SqlHelper.ExecuteScalar((SqlTransaction)dbTransaccion, procedimiento, objParms);
                  
                case BaseDatos.Odbc:
                    return OdbcHelper.ExecuteScalar((OdbcTransaction)dbTransaccion, procedimiento, objParms);
                    
                case BaseDatos.OleDB:
                    return OleDbHelper.ExecuteScalar((OleDbTransaction)dbTransaccion, procedimiento, objParms);

                   
                case BaseDatos.Oracle:
                    return OracleHelper.ExecuteScalar((OracleTransaction)dbTransaccion, procedimiento, objParms);
                    
                default:
                  return new object();
            }


        }

        /// <summary>
        /// Método que permite ejecutar un store procedure y obtener un objeto, es más eficiente
        /// </summary>
        /// <param name="dbConeccion"></param>
        /// <param name="procedimiento"></param>
        /// <param name="objParms"></param>
        /// <returns></returns>
        public object ExecuteScalar(DbConnection dbConeccion, string procedimiento, params object[] objParms)
        {
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    return SqlHelper.ExecuteScalar((SqlConnection)dbConeccion, procedimiento, objParms);
                    
                case BaseDatos.Odbc:
                    return OdbcHelper.ExecuteScalar((OdbcConnection)dbConeccion, procedimiento, objParms);
                    
                case BaseDatos.OleDB:
                    return OleDbHelper.ExecuteScalar((OleDbConnection)dbConeccion, procedimiento, objParms);

                   
                case BaseDatos.Oracle:
                    return OracleHelper.ExecuteScalar((OracleConnection)dbConeccion, procedimiento, objParms);
                    
                default:
                    return new object();
            }

        }

        /// <summary>
        /// Método que permite ejecutar un store procedure y obtener el número de registros afectados
        /// </summary>
        /// <param name="dbTransaccion"></param>
        /// <param name="procedimiento"></param>
        /// <param name="objParms"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(DbTransaction dbTransaccion, string procedimiento, params object[] objParms)
        {
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    return SqlHelper.ExecuteNonQuery((SqlTransaction)dbTransaccion, procedimiento, objParms);
                  
                case BaseDatos.Odbc:
                    return OdbcHelper.ExecuteNonQuery((OdbcTransaction)dbTransaccion, procedimiento, objParms);
                   
                case BaseDatos.OleDB:
                    return OleDbHelper.ExecuteNonQuery((OleDbTransaction)dbTransaccion, procedimiento, objParms);

                    
                case BaseDatos.Oracle:
                    return OracleHelper.ExecuteNonQuery((OracleTransaction)dbTransaccion, procedimiento, objParms);
                   
                default:
                    return 0;
            }

        }

        /// <summary>
        /// Método que permite ejecutar un store procedure y obtener el número de registros afectados
        /// </summary>
        /// <param name="dbConeccion"></param>
        /// <param name="procedimiento"></param>
        /// <param name="objParms"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(DbConnection dbConeccion, string procedimiento, params object[] objParms)
        {
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    return SqlHelper.ExecuteNonQuery((SqlConnection)dbConeccion, procedimiento, objParms);
                    
                case BaseDatos.Odbc:
                    return OdbcHelper.ExecuteNonQuery((OdbcConnection)dbConeccion, procedimiento, objParms);
                    
                case BaseDatos.OleDB:
                    return OleDbHelper.ExecuteNonQuery((OleDbConnection)dbConeccion, procedimiento, objParms);

                    
                case BaseDatos.Oracle:
                    return OracleHelper.ExecuteNonQuery((OracleConnection)dbConeccion, procedimiento, objParms);
                    
                default:
                    return 0;
            }

        }


        /// <summary>
        /// Método que permite ejecutar un store procedure y obtener en un dataset los argumentos de output
        /// </summary>
        /// <param name="dbConeccion"></param>
        /// <param name="procedimiento"></param>
        /// <param name="dsOut"></param>
        /// <param name="Args"></param>
        /// <returns></returns>
        public DataSet EjecutarProcesoOut(DbConnection dbConeccion, string procedimiento, DataSet dsOut, params object[] Args)
        {
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    return this.EjecutarProveedorBddOut(dbConeccion, procedimiento, dsOut, Args);
                    
                case BaseDatos.Odbc:
                   
                    
                case BaseDatos.OleDB:
                 

                   
                case BaseDatos.Oracle:
                   
                    
                default:
                    return new DataSet();
            }


        }


        /// <summary>
        /// Método que permite ejecutar un store procedure y obtener en un dataset los argumentos de output
        /// </summary>
        /// <param name="dbTransaccion"></param>
        /// <param name="procedimiento"></param>
        /// <param name="dsOut"></param>
        /// <param name="Args"></param>
        /// <returns></returns>
        public DataSet EjecutarProcesoOut(DbTransaction dbTransaccion, string procedimiento, DataSet dsOut, params object[] Args)
        {
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:

                    return this.EjecutarProveedorBddOut(dbTransaccion.Connection, procedimiento, dsOut, Args);
                  
                case BaseDatos.Odbc:

                   
                case BaseDatos.OleDB:


                   
                case BaseDatos.Oracle:

                   
                default:
                    return new DataSet();
            }

        }

        private DataSet EjecutarProveedorBddOut(DbConnection dbConeccion, string procedimiento, DataSet dsOut, params object[] Args)
        {
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:

                    return EjecutarSqlServerOut(dbConeccion, procedimiento, dsOut, Args);
                   
                case BaseDatos.Odbc:

                    
                case BaseDatos.OleDB:


                  
                case BaseDatos.Oracle:

                    
                default:
                    return new DataSet();
            }

        }

        private DataSet EjecutarSqlServerOut(DbConnection dbConeccion, string procedimiento, DataSet dsOut, params object[] Args)
        {
            DataSet ds = new DataSet();

            SqlCommand Com = new SqlCommand(procedimiento, (SqlConnection)dbConeccion);
            Com.CommandTimeout = 999;
            Com.CommandType = CommandType.StoredProcedure;
            SqlCommandBuilder.DeriveParameters(Com);

            SqlDataAdapter da = new SqlDataAdapter(Com);
            this.CargarParametros(da.SelectCommand, Args);
            da.SelectCommand.CommandTimeout = 999;
            da.Fill(ds);
            this.GetOutputParameters(da, dsOut);

            return ds;
        }


        private void GetOutputParameters(SqlDataAdapter da, DataSet dsOut)
        {
            DataTable dt = new DataTable();
            foreach (SqlParameter spar in da.SelectCommand.Parameters)
            {
                if (spar.Direction == ParameterDirection.Output || spar.Direction == ParameterDirection.InputOutput)
                {
                    DataColumn col = null;
                    StringBuilder stbColName = new StringBuilder(spar.ParameterName, 1, spar.ParameterName.Length - 1, spar.ParameterName.Length - 1);
                    string strColName = stbColName.ToString();
                    switch (spar.SqlDbType)
                    {
                        case SqlDbType.Char:
                        case SqlDbType.VarChar:
                            col = new DataColumn(strColName, typeof(string));
                            break;

                        case SqlDbType.Int:
                            col = new DataColumn(strColName, typeof(int));
                            break;

                        case SqlDbType.DateTime:
                            col = new DataColumn(strColName, typeof(DateTime));
                            break;

                        case SqlDbType.Decimal:
                        case SqlDbType.Money:
                            col = new DataColumn(strColName, typeof(decimal));
                            break;
                    }
                    dt.Columns.Add(col);
                }
            }
            dsOut.Tables.Add(dt);
            DataRow dtrFila = dsOut.Tables[0].NewRow();

            foreach (SqlParameter spar in da.SelectCommand.Parameters)
            {
                if (spar.Direction == ParameterDirection.Output || spar.Direction == ParameterDirection.InputOutput)
                {
                    StringBuilder stbColName = new StringBuilder(spar.ParameterName, 1, spar.ParameterName.Length - 1, spar.ParameterName.Length - 1);
                    string strColName = stbColName.ToString();
                    dtrFila[strColName] = spar.Value;
                }
            }
            dsOut.Tables[0].Rows.Add(dtrFila);
        }

        private int AsignarValoresSalidaComando(SqlTransaction tran, SqlConnection con, string ProcedimientoAlmacenado, params System.Object[] Args)
        {
            SqlCommand Com = null;

            if (tran == null)
                Com = new SqlCommand(ProcedimientoAlmacenado, con);
            else
                Com = new SqlCommand(ProcedimientoAlmacenado, con, tran);

            Com.CommandTimeout = 999;
            Com.CommandType = CommandType.StoredProcedure;
            SqlCommandBuilder.DeriveParameters(Com);
            this.CargarParametros(Com, Args);
            int Resp = Com.ExecuteNonQuery();
            for (int i = 1; i < Com.Parameters.Count; i++)
            {
                SqlParameter Par = Com.Parameters[i];
                if (Par.Direction == ParameterDirection.InputOutput || Par.Direction == ParameterDirection.Output)
                    Args.SetValue(Par.Value, i - 1);
            }

            return Resp;
        }

        private void CargarParametros(SqlCommand Com, System.Object[] Args)
        {
            int Limite = Com.Parameters.Count;
            for (int i = 1; i < Com.Parameters.Count; i++)
            {
                SqlParameter P = (SqlParameter)Com.Parameters[i];

                if (i <= Args.Length)
                    P.Value = Args[i - 1];
                else
                    P.Value = null;
            }
        }

        /// <summary>
        /// Llenamos dataset de varias consultas
        /// </summary>
        /// <param name="dbConeccion"></param>
        /// <param name="procedimiento"></param>
        /// <param name="ds"></param>
        /// <param name="strTables"></param>
        /// <param name="objParms"></param>
        public void FillDataset(DbConnection dbConeccion, string procedimiento, DataSet ds, string[] strTables, params object[] objParms)
        {
            DataSet dsTmp = null;

            switch (baseDatos)
            {
                case BaseDatos.SqlServer:

                    dsTmp = new DataSet();
                    dsTmp = SqlHelper.ExecuteDataset((SqlConnection)dbConeccion, procedimiento, objParms);
                    for (int i = 0; i < dsTmp.Tables.Count; i++)
                    {
                        if (strTables[i].ToString() != "")
                            dsTmp.Tables[i].TableName = strTables[i];

                        if (ds.Tables.Count > 0)
                        {
                            ds.Merge(dsTmp.Tables[i]);
                        }
                        else
                        {
                            ds.Tables.Add(dsTmp.Tables[i].Clone());
                            ds.Merge(dsTmp.Tables[i]);
                        }
                    }
                    break;
                case BaseDatos.Odbc:

                    break;
                case BaseDatos.OleDB:


                    break;
                case BaseDatos.Oracle:

                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbTransaccion"></param>
        /// <param name="procedimiento"></param>
        /// <param name="ds"></param>
        /// <param name="strTables"></param>
        /// <param name="objParms"></param>
        public void FillDataset(DbTransaction dbTransaccion, string procedimiento, DataSet ds, string[] strTables, params object[] objParms)
        {
            DataSet dsTmp = null;

            switch (baseDatos)
            {
                case BaseDatos.SqlServer:


                    dsTmp = new DataSet();
                    dsTmp = SqlHelper.ExecuteDataset((SqlTransaction)dbTransaccion, procedimiento, objParms);
                    for (int i = 0; i < dsTmp.Tables.Count; i++)
                    {
                        if (strTables[i].ToString() != "")
                            dsTmp.Tables[i].TableName = strTables[i];

                        if (ds.Tables.Count > 0)
                        {
                            ds.Merge(dsTmp.Tables[i]);
                        }
                        else
                        {
                            ds.Tables.Add(dsTmp.Tables[i].Clone());
                            ds.Merge(dsTmp.Tables[i]);
                        }

                    }

                    break;
                case BaseDatos.Odbc:

                    break;
                case BaseDatos.OleDB:


                    break;
                case BaseDatos.Oracle:

                    break;
                default:
                    break;
            }
           
        }

        /// <summary>
        /// Método que permite ejecutar un Sql y llenar un dataset 
        /// </summary>
        /// <param name="dbConeccion"></param>
        /// <param name="strSQLStatement"></param>
        /// <param name="ds"></param>
        /// <param name="strTables"></param>
        public void FillDataset(DbConnection dbConeccion, string strSQLStatement, DataSet ds, string[] strTables)
        {
            DataSet dsTmp = null;

            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    dsTmp = new DataSet();
                    dsTmp = SqlHelper.ExecuteDataset((SqlConnection)dbConeccion, CommandType.Text, strSQLStatement);
                    for (int i = 0; i < dsTmp.Tables.Count; i++)
                    {
                        if (strTables[i].ToString() != "")
                            dsTmp.Tables[i].TableName = strTables[i];

                        if (ds.Tables.Count > 0)
                        {
                            ds.Merge(dsTmp.Tables[i]);
                        }
                        else
                        {
                            ds.Tables.Add(dsTmp.Tables[i].Clone());
                            ds.Merge(dsTmp.Tables[i]);
                        }
                    }

                    break;
                case BaseDatos.Odbc:

                    break;
                case BaseDatos.OleDB:


                    break;
                case BaseDatos.Oracle:

                    break;
                default:
                    break;
            }
           

        }

        /// <summary>
        /// Método que permite ejecutar un Sql y llenar un dataset
        /// </summary>
        /// <param name="dbTransaccion"></param>
        /// <param name="strSQLStatement"></param>
        /// <param name="ds"></param>
        /// <param name="strTables"></param>
        public void FillDataset(DbTransaction dbTransaccion, string strSQLStatement, DataSet ds, string[] strTables)
        {

            DataSet dsTmp = null;

            switch (baseDatos)
            {
                case BaseDatos.SqlServer:

                    dsTmp = new DataSet();
                    dsTmp = SqlHelper.ExecuteDataset((SqlTransaction)dbTransaccion, CommandType.Text, strSQLStatement);
                    for (int i = 0; i < dsTmp.Tables.Count; i++)
                    {
                        if (strTables[i].ToString() != "")
                            dsTmp.Tables[i].TableName = strTables[i];

                        if (ds.Tables.Count > 0)
                        {
                            ds.Merge(dsTmp.Tables[i]);
                        }
                        else
                        {
                            ds.Tables.Add(dsTmp.Tables[i].Clone());
                            ds.Merge(dsTmp.Tables[i]);
                        }
                    }

                    break;
                case BaseDatos.Odbc:

                    break;
                case BaseDatos.OleDB:


                    break;
                case BaseDatos.Oracle:

                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Método que permite ejecutar un Sql y llenar un dataset
        /// </summary>
        /// <param name="dbConeccion"></param>
        /// <param name="spName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(DbConnection dbConeccion, string spName, params object[] parameterValues)
        {
            DbDataReader DatR = null;
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    DatR = SqlHelper.ExecuteReader((SqlConnection)dbConeccion, spName, parameterValues);

                    break;
                case BaseDatos.Odbc:
                    DatR = OdbcHelper.ExecuteReader((OdbcConnection)dbConeccion, spName, parameterValues);
                    break;
                case BaseDatos.OleDB:
                    DatR = OleDbHelper.ExecuteReader((OleDbConnection)dbConeccion, spName, parameterValues);

                    break;
                case BaseDatos.Oracle:
                    DatR = OracleHelper.ExecuteReader((OracleConnection)dbConeccion, spName, parameterValues);
                    break;
                default:
                    break;
            }

            return DatR;
        }

        /// <summary>
        /// Método que permite ejecutar un store procedure y obtener un dataReader
        /// </summary>
        /// <param name="dbTransaccion"></param>
        /// <param name="spName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(DbTransaction dbTransaccion, string spName, params object[] parameterValues)
        {
            DbDataReader DatR = null;
            switch (baseDatos)
            {
                case BaseDatos.SqlServer:
                    DatR = SqlHelper.ExecuteReader((SqlTransaction)dbTransaccion, spName, parameterValues);

                    break;
                case BaseDatos.Odbc:
                    DatR = OdbcHelper.ExecuteReader((OdbcTransaction)dbTransaccion, spName, parameterValues);
                    break;
                case BaseDatos.OleDB:
                    DatR = OleDbHelper.ExecuteReader((OleDbTransaction)dbTransaccion, spName, parameterValues);

                    break;
                case BaseDatos.Oracle:
                    DatR = OracleHelper.ExecuteReader((OracleTransaction)dbTransaccion, spName, parameterValues);
                    break;
                default:
                    break;
            }

            return DatR;
          
        }

    }
}
