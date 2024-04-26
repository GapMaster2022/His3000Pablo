using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Core.Datos;

namespace His.Maintenance.Datos
{
    public class Estaciones
    {
        SqlConnection Sqlcon;
        SqlCommand Sqlcmd;
        SqlDataAdapter Sqldap;
        DataSet Dts;
        public DataTable cargar_estaciones()
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
            string sql = "select  p.PEE_CODIGO as  codigo ,p.PEE_NOMBRE as Nombre" +
                         ",p.PEE_DESCRIPCION as Descripcion , p.PEE_ESTADO as Activo   from PEDIDOS_ESTACIONES p";
            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "estaciones");
            return Dts.Tables["estaciones"];

        }
        public Boolean grabar(string codigo, string nombre, string descripcion, string estado)
        {
            string cadena_sql = "insert into PEDIDOS_ESTACIONES values(" + codigo + ",'" + nombre + "','" + descripcion + "'," + estado + ")"; ;

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
        public Boolean modificar(string codigo, string nombre, string descripcion, string estado)
        {
            string cadena_sql = "update PEDIDOS_ESTACIONES set pee_nombre='" + nombre + "',pee_descripcion='" + descripcion + "',pee_estado=" + estado + " where pee_codigo=" + codigo;

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
            string cadena_sql = "delete PEDIDOS_ESTACIONES where pee_codigo=" + codigo;

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
            string sql = "select max(PEE_CODIGO)+1 as maximo from PEDIDOS_ESTACIONES";
            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "estaciones");
            return Dts.Tables["estaciones"];

        }

    }
}
