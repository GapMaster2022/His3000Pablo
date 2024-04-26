using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Core.Datos;
using His.Entidades.Productos;

namespace His.Datos.ConexionSic3000
{
    public class DatProducto
    {
        private BaseContextoDatos obj;
        private SqlConnection Sqlcon;
        private SqlCommand Sqlcmd;
        private SqlDataAdapter Sqldap;
        private DataSet Dts;

        private void conexion()
        {
            obj = new BaseContextoDatos();
            //Sqlcon = obj.ConectarBdSic3000();
            Sqlcon = obj.ConectarBd();
        }

        public DataTable RecuperarProductos(string codpro)
        {
            conexion();
            try
            {
                Sqlcon.Open();
                
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            string sql = "select p.codpro as Codigo, p.despro," +
                             "tippro as TipoProducto, p.codsub, p.coddiv," +
                             "p.coddep, Stocks as Stocks, PvPSinIva as SinIva," +
                             "activo as Activo, p.iva, p.codsec, p.preven, despro, p.precos, p.clasprod from Sic3000..Producto p where p.codpro = '" + codpro + "'";
            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "Producto");
            return Dts.Tables["Producto"];
        }
        //using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            //{
            //    return (from f in contexto.ATENCION_FORMAS_LLEGADA
            //            orderby f.AFL_DESCRIPCION
            //            select f).ToList();
            //}

        public DataTable cargar_estaciones()
        {
            conexion();
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
        //public Boolean grabar(string codigo, string nombre, string descripcion, string estado)
        //{
        //    conexion();
        //    Sqlcon = obj.ConectarBd();
        //    try
        //    {
        //        Sqlcon.Open();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex.Message);
        //    }
        //    Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);
        //    try
        //    {
        //        Sqlcmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //    return true;

        //}
        //public Boolean modificar(string codigo, string nombre, string descripcion, string estado)
        //{
        //    conexion();
        //    Sqlcon = obj.ConectarBd();
        //    try
        //    {
        //        Sqlcon.Open();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex.Message);
        //    }
        //    Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);
        //    try
        //    {
        //        Sqlcmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //    return true;
        //}
        
        //public DataTable UltimoCodigo()
        //{
        //    conexion();
        //    try
        //    {
        //        Sqlcon.Open();
        //        string sql = "select max(codpro)+1 as maximo from Producto";
        //        Sqlcmd = new SqlCommand(sql, Sqlcon);
        //        Sqldap = new SqlDataAdapter();
        //        Sqldap.SelectCommand = Sqlcmd;
        //        Dts = new DataSet();
        //        Sqldap.Fill(Dts, "estaciones");
                
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return Dts.Tables["estaciones"];
        //}
    }
}
