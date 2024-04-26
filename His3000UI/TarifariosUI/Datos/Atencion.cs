using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Core.Datos;
using System.Data;


namespace TarifariosUI
{
   public  class Atencion
    {
        SqlConnection Sqlcon;
        SqlCommand Sqlcmd;
        SqlDataAdapter Sqldap;
        DataSet Dts;
        public DataTable cargar_atenciones()
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
            string sql = "select TOP  1000 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION,P.PAC_HISTORIA_CLINICA AS  HCL," +
                          "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  ' +P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                          "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION"+
                          ",r.TIR_NOMBRE as referido,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ,"+
                          " convert(datetime,a.ATE_FACTURA_FECHA,120)as FECHA_FACTURA"+
                           " ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO,"+
                          " convert(datetime,a.ATE_FECHA_ALTA,120)as FECHA_ALTA FROM  ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H  WHERE   " +
                          "p.PAC_CODIGO=a.PAC_CODIGO AND A.HAB_CODIGO=H.hab_Codigo"+
                          " and a.TIR_CODIGO =r.TIR_CODIGO  ORDER  BY  a.ate_fecha_ingreso desc ";

            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "atenciones");
            return Dts.Tables["atenciones"];

        }
        public DataSet combo_tipo_empresa()
        {
            BaseContextoDatos conect = new BaseContextoDatos();
            Sqlcon = conect.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            string cad1 = "select * from TIPO_EMPRESA";
            Sqlcmd = new SqlCommand(cad1, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "Tabla");
            return Dts;
        }

        public DataSet combo_tipo_pagos()
        {
            BaseContextoDatos conect = new BaseContextoDatos();
            Sqlcon = conect.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            string cad1 = "select * from TIPO_FORMA_PAGO where TIF_CODIGO !=3 and TIF_CODIGO!=4 ";
            Sqlcmd = new SqlCommand(cad1, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "Tabla");
            return Dts;
        }
        public DataTable cargar_formas_pago(string codigo)
        {
            BaseContextoDatos conect = new BaseContextoDatos();
            Sqlcon = conect.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            string cad1 = "  select FOR_CODIGO as CODIGO ,FOR_DESCRIPCION AS DESCRIPCION" +
                           ",FOR_COMISION AS COMISION , FOR_REFERIDO AS REFERIDO "+
                            " from FORMA_PAGO where TIF_CODIGO !=4 and TIF_CODIGO!=3 and TIF_CODIGO="+codigo+" ORDER BY DESCRIPCION ASC";
            Sqlcmd = new SqlCommand(cad1, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "Tabla");
            return Dts.Tables["Tabla"];
        }
        public DataTable buscar_forma_pagos(string descripcion)
        {
            BaseContextoDatos conect = new BaseContextoDatos();
            Sqlcon = conect.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            string cad1 = "select *from FORMA_PAGO  where FOR_DESCRIPCION like '"+descripcion+"%'";
            Sqlcmd = new SqlCommand(cad1, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "Tabla");
            return Dts.Tables["Tabla"];
        }
        public DataTable buscar_honorarios(string codigo)
        {
            BaseContextoDatos conect = new BaseContextoDatos();
            Sqlcon = conect.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            string cad1 = "SELECT     dbo.FORMA_PAGO.FOR_CODIGO, dbo.FORMA_PAGO.FOR_DESCRIPCION, dbo.HONORARIOS_MEDICOS.HOM_CODIGO,"+ 
                          " dbo.TIPO_EMPRESA.TE_CODIGO, dbo.TIPO_EMPRESA.TE_DESCRIPCION FROM dbo.ATENCIONES INNER JOIN dbo.FORMA_PAGO INNER JOIN"+
                      " dbo.HONORARIOS_MEDICOS ON dbo.FORMA_PAGO.FOR_CODIGO = dbo.HONORARIOS_MEDICOS.FOR_CODIGO ON dbo.ATENCIONES.ATE_CODIGO ="+
                      " dbo.HONORARIOS_MEDICOS.ATE_CODIGO INNER JOIN dbo.ATENCION_DETALLE_CATEGORIAS ON dbo.ATENCIONES.ATE_CODIGO = "+
                      " dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO INNER JOIN dbo.CATEGORIAS_CONVENIOS ON dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO ="+
                      " dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO INNER JOIN dbo.ASEGURADORAS_EMPRESAS INNER JOIN dbo.TIPO_EMPRESA ON dbo.ASEGURADORAS_EMPRESAS.TE_CODIGO = "+
                      " dbo.TIPO_EMPRESA.TE_CODIGO ON dbo.CATEGORIAS_CONVENIOS.ASE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO WHERE     (dbo.HONORARIOS_MEDICOS.HOM_CODIGO ="+codigo+")";
            Sqlcmd = new SqlCommand(cad1, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "Tabla");
            return Dts.Tables["Tabla"];
        }
        public DataTable cargar_aseguradoras_empresas(string tipo_empresa,string atencion)
        {
            BaseContextoDatos conect = new BaseContextoDatos();
            Sqlcon = conect.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            string cad1 = "SELECT     dbo.ATENCIONES.ATE_CODIGO, dbo.ASEGURADORAS_EMPRESAS.ASE_NOMBRE, dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE,"+ 
                          " dbo.FORMA_PAGO.FOR_CODIGO, dbo.FORMA_PAGO.FOR_DESCRIPCION , dbo.FORMA_PAGO.FOR_COMISION,dbo.FORMA_PAGO.FOR_REFERIDO"+
                          " FROM dbo.ATENCION_DETALLE_CATEGORIAS INNER JOIN"+
                          " dbo.ATENCIONES ON dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO INNER JOIN "+
                          " dbo.CATEGORIAS_CONVENIOS ON dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO = dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO INNER JOIN"+
                          " dbo.ASEGURADORAS_EMPRESAS ON dbo.CATEGORIAS_CONVENIOS.ASE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO INNER JOIN"+
                          " dbo.TIPO_EMPRESA ON dbo.ASEGURADORAS_EMPRESAS.TE_CODIGO = dbo.TIPO_EMPRESA.TE_CODIGO INNER JOIN"+
                          " dbo.FORMA_PAGO ON dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO = dbo.FORMA_PAGO.ASE_CODIGO WHERE (dbo.ATENCIONES.ATE_CODIGO ="+atencion+")"+
                          " AND (dbo.TIPO_EMPRESA.TE_CODIGO ="+tipo_empresa+")";
            Sqlcmd = new SqlCommand(cad1, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "Tabla");
            return Dts.Tables["Tabla"];
        }
       public DataTable buscar_atenciones(string hcl,string nombre,string id)
        {
            string sql = "";
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
            if (hcl != "")
            {
                sql = "select TOP  1000 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION,P.PAC_HISTORIA_CLINICA AS  HCL," +
                          "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  ' +P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                          "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION" +
                          ",r.TIR_NOMBRE as referido,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                          " convert(datetime,a.ATE_FACTURA_FECHA,120)as FECHA_FACTURA" +
                           " ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
                          " convert(datetime,a.ATE_FECHA_ALTA,120)as FECHA_ALTA FROM   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                              " p.PAC_CODIGO=a.PAC_CODIGO and p.PAC_HISTORIA_CLINICA like '%"+hcl+"%'"+
                              " AND A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO  ORDER  BY  a.ate_fecha_ingreso desc";


            }
            if (id != "")
            {
                sql = "select TOP  1000 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION,P.PAC_HISTORIA_CLINICA AS  HCL," +
                          "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  ' +P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                          "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION" +
                          ",r.TIR_NOMBRE as referido,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                          " convert(datetime,a.ATE_FACTURA_FECHA,120)as FECHA_FACTURA" +
                           " ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
                          " convert(datetime,a.ATE_FECHA_ALTA,120)as FECHA_ALTAfrom   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                              " p.PAC_CODIGO=a.PAC_CODIGO and p.PAC_IDENTIFICACION='" + id + "'" +
                              " AND A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO  ORDER  BY  a.ate_fecha_ingreso desc ";


            }
            if (nombre != "")
            {
                string cadena_nombre = nombre + " ";
                int y = cadena_nombre.IndexOf(" ");
                string nombre_1 = cadena_nombre.Substring(y + 1);
                string apellido = cadena_nombre.Substring(0, y + 1);

                sql = "select TOP  1000 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION,P.PAC_HISTORIA_CLINICA AS  HCL," +
                          "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  ' +P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                          "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION" +
                          ",r.TIR_NOMBRE as referido,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                          " convert(datetime,a.ATE_FACTURA_FECHA,120)as FECHA_FACTURA" +
                           " ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
                          " convert(datetime,a.ATE_FECHA_ALTA,120)as FECHA_ALTA  FROM   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                              "p.PAC_CODIGO=a.PAC_CODIGO and p.PAC_APELLIDO_PATERNO like '" + apellido.Trim() + "%' and  p.PAC_APELLIDO_MATERNO like '" + nombre_1.Trim() + "%'" +
                              "  AND A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO AND A.ATE_FECHA_ALTA IS NULL ORDER  BY  a.ate_fecha_ingreso desc ";


            }
            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "atenciones");
            return Dts.Tables["atenciones"];

        }
       public Boolean modificar(string codigo, string factura, string control, string fecha_factura,string fecha_ingreso,string fecha_alta)
       {
           string cadena_sql = "update ATENCIONES set ATE_FACTURA_FECHA=CONVERT(datetime, '" + fecha_factura + "',120)," +
                           " ATE_FECHA_ALTA=CONVERT(datetime, '" + fecha_alta + "',120) ,ATE_FECHA_INGRESO=CONVERT(datetime, '" + fecha_ingreso + "',120)" +
                           " ,ATE_NUMERO_CONTROL='" + control + "' ,ATE_FACTURA_PACIENTE='" + factura + "'" +
                           " where ATE_CODIGO=" + codigo;

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
      
    }
}
