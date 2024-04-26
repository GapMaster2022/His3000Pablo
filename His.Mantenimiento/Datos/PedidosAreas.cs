using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Datos;
using System.Data.SqlClient;
using System.Data;

namespace His.Mantenimiento.Datos
{
    public class PedidosAreas
    {
        SqlConnection Sqlcon;
        SqlCommand Sqlcmd;
        SqlDataAdapter Sqldap;
        DataSet Dts;

        public Boolean grabar(string codigo, string tipo, string nombre, string estado)
        {
            //string cadena_sql = "insert into PEDIDOS_AREAS (pea_codigo, pea_tipo, pea_nombre, pea_estado) values(" + codigo + ",'" + tipo + "','" + nombre + "'," + estado + ")"; ;
            
string cadena_sql = "insert into PEDIDOS_AREAS (pea_codigo, pea_tipo, pea_nombre, pea_estado) values((select max(PEA_CODIGO) + 1 as maximo from PEDIDOS_AREAS),(select max(PEA_CODIGO) + 1 as maximo from PEDIDOS_AREAS),'" + nombre + "'," + estado + ")"; ;
            BaseContextoDatos obj = new BaseContextoDatos();

            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);
            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;

            }
            return true;

        }
        public Boolean modificar(string codigo, string tipo, string nombre, string estado)
        {
            string cadena_sql = "update PEDIDOS_AREAS set pea_nombre='" + nombre + "',pea_tipo='" + tipo + "',pea_estado=" + estado + " where pea_codigo=" + codigo;

            BaseContextoDatos obj = new BaseContextoDatos();

            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);
            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;

            }
            return true;

        }
        public Boolean eliminar(string codigo)
        {
            string cadena_sql = "delete PEDIDOS_AREAS where pea_codigo=" + codigo;

            BaseContextoDatos obj = new BaseContextoDatos();

            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);
            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;

            }
            return true;

        }
        public DataTable cargar_areas()
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
            string sql = "select  p.PEA_CODIGO as codigo,p.PEA_NOMBRE as Nombre," +
             "p.PEA_ESTADO as  Activo ,p.PEA_TIPO as tipo from PEDIDOS_AREAS p order by codigo desc";
            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "area");
            return Dts.Tables["area"];

        }
        public DataTable UltimoCodigo()
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
            string sql = "select max(PEA_CODIGO)+1 as maximo from PEDIDOS_AREAS";
            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "area");
            return Dts.Tables["area"];

        }

    }
}
