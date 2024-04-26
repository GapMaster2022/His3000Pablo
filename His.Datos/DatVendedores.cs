using Core.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using His.Entidades;

namespace His.Datos
{
    public class DatVendedores
    {
        
        public DataTable getAteImagen(int x)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
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
            Sqlcmd = new SqlCommand("SELECT  dbo.CUENTAS_PACIENTES.CUE_CODIGO as codigo, dbo.PRODUCTO.PRO_DESCRIPCION AS Producto FROM  dbo.ATENCIONES INNER JOIN dbo.CUENTAS_PACIENTES ON dbo.ATENCIONES.ATE_CODIGO = dbo.CUENTAS_PACIENTES.ATE_CODIGO INNER JOIN\n" +
                "dbo.PRODUCTO INNER JOIN Sic3000.dbo.ProductoSubdivision ON dbo.PRODUCTO.PRO_CODSUB = Sic3000.dbo.ProductoSubdivision.codsub AND dbo.PRODUCTO.PRO_CODDIV = Sic3000.dbo.ProductoSubdivision.coddiv INNER JOIN\n" +
                "dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = dbo.RUBROS.RUB_CODIGO INNER JOIN dbo.PEDIDOS_AREAS ON dbo.RUBROS.PED_CODIGO = dbo.PEDIDOS_AREAS.DIV_CODIGO ON dbo.CUENTAS_PACIENTES.PRO_CODIGO = dbo.PRODUCTO.PRO_CODIGO\n" +
            "WHERE  dbo.RUBROS.RUB_CODIGO = 8 AND dbo.ATENCIONES.ATE_CODIGO = " + x + " "  , Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }

        public DataTable getVendedores()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
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
            Sqlcmd = new SqlCommand("select * from vendedores", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }

        public DataTable getReporteVendedor(string condicion)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
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
            Sqlcmd = new SqlCommand("SELECT dbo.FACTURA.FAC_FECHA, dbo.FACTURA.FAC_NUMERO, dbo.FACTURA.CLI_NOMBRE AS PACIENTE, dbo.FACTURA.FAC_TOTAL, \n" +
                     "concat('Dr.', dbo.MEDICOS.MED_APELLIDO_PATERNO, ' ', dbo.MEDICOS.MED_APELLIDO_MATERNO, ' ', dbo.MEDICOS.MED_NOMBRE1, ' ', dbo.MEDICOS.MED_NOMBRE2) as MEDICO\n" +
                    ", dbo.HONORARIOS_MEDICOS.HOM_VALOR_NETO AS HON_MEDICO, dbo.vendedores.comision, ((dbo.vendedores.comision*dbo.FACTURA.FAC_TOTAL)/100) AS COM_TOTAL, ((dbo.vendedores.comision*dbo.HONORARIOS_MEDICOS.HOM_VALOR_NETO)/100) AS COM_HON_MED, dbo.vendedores.nombre, dbo.vendedores.nro_identificacion, dbo.HONORARIOS_MEDICOS.HOM_FACTURA_MEDICO\n" +
                "FROM dbo.medico_vendedor INNER JOIN dbo.vendedores ON dbo.medico_vendedor.cod_vendedor = dbo.vendedores.codigo INNER JOIN\n" +
                " dbo.FACTURA INNER JOIN dbo.HONORARIOS_MEDICOS ON dbo.FACTURA.ATE_CODIGO = dbo.HONORARIOS_MEDICOS.ATE_CODIGO INNER JOIN\n" +
                " dbo.MEDICOS ON dbo.HONORARIOS_MEDICOS.MED_CODIGO = dbo.MEDICOS.MED_CODIGO ON dbo.medico_vendedor.cod_medico = dbo.MEDICOS.MED_CODIGO INNER JOIN\n" +
                " dbo.ATENCIONES ON dbo.FACTURA.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO \n" + condicion, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }

        public DataTable getReporteVendedor2(string condicion)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
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
            Sqlcmd = new SqlCommand("SELECT	Sic3000.dbo.Nota.fecha AS Fecha_Factura, Sic3000.dbo.Nota.numfac,  CONCAT(dbo.PACIENTES.PAC_NOMBRE1,' ', dbo.PACIENTES.PAC_NOMBRE2,' ', dbo.PACIENTES.PAC_APELLIDO_PATERNO,' ', dbo.PACIENTES.PAC_APELLIDO_MATERNO) AS PACIENTE , \n" +
        "CONCAT('Dr.', dbo.MEDICOS.MED_NOMBRE1, ' ', dbo.MEDICOS.MED_NOMBRE2, ' ', dbo.MEDICOS.MED_APELLIDO_PATERNO, ' ', dbo.MEDICOS.MED_APELLIDO_MATERNO) AS MEDICO_TRATANTE\n" +
       " , ROUND(Sic3000.dbo.Nota.total, 2, 0) AS TOTAL, isnull((SELECT         sum(dbo.CUENTAS_PACIENTES.CUE_VALOR + dbo.CUENTAS_PACIENTES.CUE_IVA) as Total\n" +
        "    FROM  dbo.PRODUCTO INNER JOIN Sic3000.dbo.ProductoSubdivision ON dbo.PRODUCTO.PRO_CODSUB = Sic3000.dbo.ProductoSubdivision.codsub AND dbo.PRODUCTO.PRO_CODDIV = Sic3000.dbo.ProductoSubdivision.coddiv INNER JOIN\n" +
        "        dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = dbo.RUBROS.RUB_CODIGO INNER JOIN    dbo.PEDIDOS_AREAS ON dbo.RUBROS.PED_CODIGO = dbo.PEDIDOS_AREAS.DIV_CODIGO INNER JOIN\n" +
        "        dbo.CUENTAS_PACIENTES ON dbo.PRODUCTO.PRO_CODIGO = dbo.CUENTAS_PACIENTES.PRO_CODIGO INNER JOIN    dbo.ATENCIONES ON dbo.CUENTAS_PACIENTES.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO INNER JOIN\n" +
        "        dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO LEFT OUTER JOIN    Sic3000.dbo.Nota ON dbo.ATENCIONES.ATE_FACTURA_PACIENTE = Sic3000.dbo.Nota.numnot LEFT OUTER JOIN\n" +
        "        dbo.HABITACIONES ON dbo.ATENCIONES.HAB_CODIGO = dbo.HABITACIONES.hab_Codigo LEFT OUTER JOIN    dbo.USUARIOS ON dbo.CUENTAS_PACIENTES.ID_USUARIO = dbo.USUARIOS.ID_USUARIO LEFT OUTER JOIN\n" +
        "            dbo.MEDICOS ON dbo.ATENCIONES.MED_CODIGO = dbo.MEDICOS.MED_CODIGO\n" +
        "            where dbo.RUBROS.RUB_GRUPO = 'SERVICIOS INSTITUCIONALES' and  dbo.ATENCIONES.ATE_NUMERO_ATENCION = a.ATE_NUMERO_ATENCION\n" +
        "            group by dbo.ATENCIONES.ATE_NUMERO_ATENCION), 0) as SERVICIOS_INSTITUCIONALES,\n" +
        "dbo.vendedores.nombre AS VENDEDOR, dbo.vendedores.comision ,ROUND(((dbo.vendedores.comision * Sic3000.dbo.Nota.total) / 100), 2, 0) AS COM_TOTAL,\n" +
        "     isnull(((dbo.vendedores.comision * (SELECT        sum(dbo.CUENTAS_PACIENTES.CUE_VALOR + dbo.CUENTAS_PACIENTES.CUE_IVA) as Total\n" +
        "         FROM  dbo.PRODUCTO INNER JOIN Sic3000.dbo.ProductoSubdivision ON dbo.PRODUCTO.PRO_CODSUB = Sic3000.dbo.ProductoSubdivision.codsub AND dbo.PRODUCTO.PRO_CODDIV = Sic3000.dbo.ProductoSubdivision.coddiv INNER JOIN\n" +
        "             dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = dbo.RUBROS.RUB_CODIGO INNER JOIN    dbo.PEDIDOS_AREAS ON dbo.RUBROS.PED_CODIGO = dbo.PEDIDOS_AREAS.DIV_CODIGO INNER JOIN\n" +
        "             dbo.CUENTAS_PACIENTES ON dbo.PRODUCTO.PRO_CODIGO = dbo.CUENTAS_PACIENTES.PRO_CODIGO INNER JOIN    dbo.ATENCIONES ON dbo.CUENTAS_PACIENTES.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO INNER JOIN\n" +
        "             dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO LEFT OUTER JOIN    Sic3000.dbo.Nota ON dbo.ATENCIONES.ATE_FACTURA_PACIENTE = Sic3000.dbo.Nota.numnot LEFT OUTER JOIN\n" +
        "             dbo.HABITACIONES ON dbo.ATENCIONES.HAB_CODIGO = dbo.HABITACIONES.hab_Codigo LEFT OUTER JOIN    dbo.USUARIOS ON dbo.CUENTAS_PACIENTES.ID_USUARIO = dbo.USUARIOS.ID_USUARIO LEFT OUTER JOIN\n" +
        "                 dbo.MEDICOS ON dbo.ATENCIONES.MED_CODIGO = dbo.MEDICOS.MED_CODIGO where dbo.RUBROS.RUB_GRUPO = 'SERVICIOS INSTITUCIONALES' and  dbo.ATENCIONES.ATE_NUMERO_ATENCION = a.ATE_NUMERO_ATENCION\n" +
        "                 group by dbo.ATENCIONES.ATE_NUMERO_ATENCION))/ 100),0) AS COM_SERV_INSTIT\n" +
"FROM dbo.ATENCIONES a INNER JOIN dbo.medico_vendedor INNER JOIN dbo.vendedores ON dbo.medico_vendedor.cod_vendedor = dbo.vendedores.codigo\n" +
 "       INNER JOIN dbo.MEDICOS ON dbo.medico_vendedor.cod_medico = dbo.MEDICOS.MED_CODIGO ON a.MED_CODIGO = dbo.MEDICOS.MED_CODIGO\n" +
  "      INNER JOIN dbo.PACIENTES ON a.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO  INNER JOIN Sic3000.dbo.Nota ON a.ATE_FACTURA_PACIENTE = Sic3000.dbo.Nota.numnot\n" + condicion, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }

        public bool deleteVendedor(string codVendedor)
        {
            string cadena_sql;
            
                cadena_sql = "DELETE FROM [dbo].[vendedores] WHERE codigo= " + codVendedor + " ";
            
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public void saveVendedor(vendedor v)
        {
            string cadena_sql;
            if (v.codigo.Trim() == string.Empty)
            {
                cadena_sql = "INSERT INTO [dbo].[vendedores] ( [nro_identificacion],[fec_ingreso],[fec_salida],[comision],[nombre])VALUES (" +
                               "'" + v.nro_identificacion + "'" +
                               ",'" + v.fec_ingreso + "'" +
                               ",'" + v.fec_salida + "'" +
                               ", " + v.comision + "" +
                               ",'" + v.nombre + "')" ;
            }
            else
            {
                cadena_sql = "UPDATE [dbo].[vendedores]\n" +
                               "SET [fec_ingreso] = '" + v.fec_ingreso + "'\n" +
                                "  ,[fec_salida] = '" + v.fec_salida + "'\n" +
                                ", [nro_identificacion] = '" + v.nro_identificacion + "'\n" +
                                 " ,[comision] =  " + v.comision + "\n" +
                                 " ,[nombre] = '" + v.nombre + "'\n" +
                             "WHERE codigo= " + v.codigo+ " ";
            }
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public DataTable getVendedoresS()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
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
            Sqlcmd = new SqlCommand("SELECT [codigo] ,[nombre]  as Nombre_Vendedor ,[comision] FROM [His3000].[dbo].[vendedores]", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }
        public DataTable getTarjetas()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
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
            Sqlcmd = new SqlCommand("select forpag as codigo, despag as name from Sic3000..Forma_Pago where claspag=4", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }
        public DataTable getBancos()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
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
            Sqlcmd = new SqlCommand("Select BAN_CODIGO as Codigo, BAN_NOMBRE as Banco From BANCOS", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }

        public bool existCedVendedor(string ced,string codigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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

            Sqlcmd = new SqlCommand("SELECT COUNT(*)  FROM vendedores v where v.nro_identificacion='"+ ced +"' and codigo<>"+ codigo, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);
            int rows = Convert.ToInt32(Dts.Rows[0][0].ToString());

            bool existe; 
            if (rows == 0) //si existe
            {
                existe = false;
            }
            else
            {
                existe = true;
            }

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return existe;
        }

    }
}
