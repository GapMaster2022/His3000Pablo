using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Datos.Sql
{
    /// <summary>
    /// Clase que pemite tener un diccionario de tipos de datos
    /// </summary>
    public class SqlTypeMap : DictionaryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SqlDbType this[string key]
        { get { return (SqlDbType)Dictionary[key]; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, SqlDbType value)
        { Dictionary.Add(key, value); }
    }

    /// <summary>
    /// Clase que permite descubrir los parametros de los store procedures
    /// </summary>
    public class CmdBuilder
    {
        static SqlTypeMap _sqlTypeMap = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        public static void DeriveParameters(SqlCommand cmd)
        {
            EnsureTypeMap();

            SqlParameter prmRet = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
            prmRet.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(prmRet);

            string qrySProc =
                "SELECT parameter_name as name"
                    + ", data_type as xtype"
                    + ", cast(isnull(character_maximum_length, " +
                                       "numeric_scale) as int) as prec"
                    + ", case when parameter_mode like '%out%' " +
                                       "then 1 else 0 end as isoutparam"
                + " FROM INFORMATION_SCHEMA.PARAMETERS"
                + " WHERE specific_name = '" + cmd.CommandText + "'"
                + " ORDER BY ordinal_position";

            //query SQL-server for given sproc's parameter info
            DataTable dt = new DataTable();
            new SqlDataAdapter(qrySProc, cmd.Connection).Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                SqlParameter prm = new SqlParameter(
                        (string)dr[0],               //dr["name"] 
                        _sqlTypeMap[(string)dr[1]],  //dr["xtype"]                        
                        (int)(dr[2] is System.DBNull ? 0 : dr[2]));   //dr["prec"]
                if ((int)dr[3] == 1)                 //isoutparam?
                    prm.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(prm);
            }
        }

        /// <summary>
        /// Método estático que transforma y mapea el tipo de dato de la base de datos a ADO .NET
        /// </summary>
        static void EnsureTypeMap()
        {
            if (_sqlTypeMap == null)
            {
                _sqlTypeMap = new SqlTypeMap();
                _sqlTypeMap.Add("date", SqlDbType.Date);
                _sqlTypeMap.Add("bigint", SqlDbType.BigInt);
                _sqlTypeMap.Add("binary", SqlDbType.Binary);
                _sqlTypeMap.Add("bit", SqlDbType.Bit);
                _sqlTypeMap.Add("char", SqlDbType.Char);
                _sqlTypeMap.Add("datetime", SqlDbType.DateTime);
                _sqlTypeMap.Add("decimal", SqlDbType.Decimal);
                _sqlTypeMap.Add("float", SqlDbType.Float);
                _sqlTypeMap.Add("image", SqlDbType.VarBinary);
                _sqlTypeMap.Add("int", SqlDbType.Int);
                _sqlTypeMap.Add("money", SqlDbType.Money);
                _sqlTypeMap.Add("nchar", SqlDbType.NChar);
                _sqlTypeMap.Add("ntext", SqlDbType.NVarChar);
                _sqlTypeMap.Add("numeric", SqlDbType.Decimal);
                _sqlTypeMap.Add("nvarchar", SqlDbType.NVarChar);
                _sqlTypeMap.Add("real", SqlDbType.Real);
                _sqlTypeMap.Add("smalldatetime", SqlDbType.SmallDateTime);
                _sqlTypeMap.Add("smallint", SqlDbType.SmallInt);
                _sqlTypeMap.Add("smallmoney", SqlDbType.SmallMoney);
                _sqlTypeMap.Add("sql_variant", SqlDbType.Variant);
                _sqlTypeMap.Add("text", SqlDbType.VarChar);
                _sqlTypeMap.Add("timestamp", SqlDbType.VarBinary);
                _sqlTypeMap.Add("tinyint", SqlDbType.TinyInt);
                _sqlTypeMap.Add("uniqueidentifier", SqlDbType.UniqueIdentifier);
                _sqlTypeMap.Add("varbinary", SqlDbType.VarBinary);
                _sqlTypeMap.Add("varchar", SqlDbType.VarChar);
                _sqlTypeMap.Add("xml", SqlDbType.Xml);

            }
        }

    }
}
