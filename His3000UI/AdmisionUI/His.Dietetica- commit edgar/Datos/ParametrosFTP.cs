using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Core.Datos;
using System.Data;


namespace His.Admision.Datos
{
  public   class ParametrosFTP
    {
        SqlConnection Sqlcon;
        SqlCommand Sqlcmd;
        SqlDataAdapter Sqldap;
        DataSet Dts;
        public DataTable cargar_parametrosFtP(string parametro)
        {
           
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            string sql = "SELECT dbo.PARAMETROS.PAR_CODIGO, dbo.PARAMETROS_DETALLE.PAD_VALOR, dbo.PARAMETROS_DETALLE.PAD_CODIGO" +
                          " FROM  dbo.PARAMETROS INNER JOIN dbo.PARAMETROS_DETALLE ON dbo.PARAMETROS.PAR_CODIGO = dbo.PARAMETROS_DETALLE.PAR_CODIGO" +
                            " WHERE     (dbo.PARAMETROS.PAR_CODIGO = 3) AND (dbo.PARAMETROS_DETALLE.PAD_CODIGO =" + parametro + ")";

            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }
    }
}
