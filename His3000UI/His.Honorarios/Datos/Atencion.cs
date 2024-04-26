using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Core.Datos;
using System.Data;
using His.Entidades;

namespace His.Honorarios.Datos
{
   public  class Atencion
    {
        SqlConnection Sqlcon;
        SqlCommand Sqlcmd;
        SqlDataAdapter Sqldap;
        DataSet Dts;

        
        public DataTable CuentaPaciente(Int64 ateNumero)
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
            string sql = "select round(SUM((CUE_VALOR_UNITARIO * CUE_CANTIDAD)+CUE_IVA),2) as TOTAL from CUENTAS_PACIENTES where CUE_CANTIDAD <> 0 AND CUE_ESTADO=1 AND ATE_CODIGO=" + ateNumero + "";

            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "atenciones");
            return Dts.Tables["atenciones"];

        }

        public DataTable Banco(Int64 forPag)
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
            string sql = "select banco from Sic3000..Forma_Pago where forpag=" + forPag;

            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "atenciones");
            return Dts.Tables["atenciones"];

        }

        public DataTable FacturaElectronicaFisica(string caja)
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
            string sql = "select idTipoFact, nestablecimiento, caja from Sic3000..autorizacion where caja='" + caja + "'";

            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "atenciones");
            return Dts.Tables["atenciones"];

        }

        public DataTable ValidaFactura(Int64 codMedico)
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
            string sql = "select * from Cg3000..CgDatosSRI where codigo_c =(select codigo_c from Cg3000..Cgcodcon where codigo_c = (select RTRIM(MED_CODIGO_MEDICO) from MEDICOS where MED_CODIGO = " + codMedico + "))";

            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "atenciones");
            return Dts.Tables["atenciones"];

        }

        public DataTable TotalesSIC(string factura)
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
            string sql = "select subtotal, desctot, totsiva, totciva, iva,total from Sic3000..Nota where numnot = '" + factura + "'";

            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "atenciones");
            return Dts.Tables["atenciones"];

        }

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
            string sql = "select TOP  1000 P.PAC_HISTORIA_CLINICA AS  HCL, p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION, A.ATE_NUMERO_ATENCION AS ATE_NUM," +
                        "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
                        "+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                        "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                        "convert(datetime,a.ATE_FACTURA_FECHA,120)as FECHA1" +
                        "  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
                        "convert(datetime,a.ATE_FECHA_ALTA,120)as  FECHA_ALTA, CC.CAT_NOMBRE AS CONVENIO FROM  ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H," +
                        "CATEGORIAS_CONVENIOS CC, ATENCION_DETALLE_CATEGORIAS C WHERE   " +
                        "p.PAC_CODIGO=a.PAC_CODIGO AND A.HAB_CODIGO=H.hab_Codigo and A.ATE_CODIGO=C.ATE_CODIGO and C.CAT_CODIGO=CC.CAT_CODIGO" +
                        " and a.TIR_CODIGO =r.TIR_CODIGO AND  A.ATE_FECHA_INGRESO > '01/01/2020 0:00:00' /*AND A.ATE_FECHA_ALTA IS NOT  null*/  ORDER  BY   A.ATE_CODIGO DESC ";

            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "atenciones");
            return Dts.Tables["atenciones"];

        }

        public DataTable buscar_atenciones(string hcl, string nombre, string id, string habitacion)
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
                sql = "select top 200 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION, A.ATE_NUMERO_ATENCION as 'NRO ATENCION', P.PAC_HISTORIA_CLINICA AS  HCL," +
                "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
                "+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                " convert(datetime,a.ATE_FACTURA_FECHA,120)as fecha1" +
                "  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
                " convert(datetime,a.ATE_FECHA_ALTA,120)as  FECHA_ALTA  from   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                " p.PAC_CODIGO=a.PAC_CODIGO and p.PAC_HISTORIA_CLINICA like '%" + hcl + "%'" +
                " AND A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO  AND  ATE_FECHA_INGRESO > '01/01/2011 0:00:00' /*AND ATE_FECHA_ALTA IS NOT  null and esc_codigo<=2*/  ORDER  BY   ATE_CODIGO DESC ";


            }
            if (id != "")
            {
                sql = "select top 200 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION, A.ATE_NUMERO_ATENCION as 'NRO ATENCION', P.PAC_HISTORIA_CLINICA AS  HCL," +
                "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
                "+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                " convert(datetime,a.ATE_FACTURA_FECHA,120)as fecha1" +
                "  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
                " convert(datetime,a.ATE_FECHA_ALTA,120)as  FECHA_ALTA  from   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                " p.PAC_CODIGO=a.PAC_CODIGO and p.PAC_IDENTIFICACION='" + id + "'" +
                " AND A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO AND  ATE_FECHA_INGRESO > '01/01/2011 0:00:00' /*AND ATE_FECHA_ALTA IS NOT  null and esc_codigo<=2 */ ORDER  BY   ATE_CODIGO DESC ";


            }
            if (nombre != "")
            {
                string cadena_nombre = nombre + " ";
                int y = cadena_nombre.IndexOf(" ");
                string nombre_1 = cadena_nombre.Substring(y + 1);
                string apellido = cadena_nombre.Substring(0, y + 1);

                sql = "select top 200 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION, A.ATE_NUMERO_ATENCION as 'NRO ATENCION', P.PAC_HISTORIA_CLINICA AS  HCL," +
                "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
                "+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                " convert(datetime,a.ATE_FACTURA_FECHA,120)as fecha1" +
                "  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
                " convert(datetime,a.ATE_FECHA_ALTA,120)as  FECHA_ALTA  from   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                "p.PAC_CODIGO=a.PAC_CODIGO and p.PAC_APELLIDO_PATERNO like '" + apellido.Trim() + "%' and  p.PAC_APELLIDO_MATERNO like '" + nombre_1.Trim() + "%'" +
                "  AND A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO AND  ATE_FECHA_INGRESO > '01/01/2011 0:00:00' /*AND ATE_FECHA_ALTA IS NOT  null and esc_codigo<=2*/ ORDER  BY   ATE_CODIGO DESC ";


            }
            if (habitacion != "")
            {
                sql = "select top 200 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION, A.ATE_NUMERO_ATENCION as 'NRO ATENCION', P.PAC_HISTORIA_CLINICA AS  HCL," +
                        "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
                        "+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                        "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                        " convert(datetime,a.ATE_FACTURA_FECHA,120)as fecha1" +
                        "  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
                        " convert(datetime,a.ATE_FECHA_ALTA,120)as  FECHA_ALTA  from   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                        " p.PAC_CODIGO=a.PAC_CODIGO " +
                        " AND A.HAB_CODIGO=H.hab_Codigo and H.hab_Numero ='" + habitacion + "' and a.TIR_CODIGO =r.TIR_CODIGO  AND  ATE_FECHA_INGRESO > '01/01/2011 0:00:00' /*AND ATE_FECHA_ALTA IS NOT  null and esc_codigo<=2 */ ORDER  BY   a.ATE_FECHA_ALTA DESC ";


            }

            if (sql == "")
            {

                sql = "select top 200 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION, A.ATE_NUMERO_ATENCION as 'NRO ATENCION',P.PAC_HISTORIA_CLINICA AS  HCL," +
                "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
                "+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                " convert(datetime,a.ATE_FACTURA_FECHA,120)as fecha1" +
                "  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
                " convert(datetime,a.ATE_FECHA_ALTA,120)as  FECHA_ALTA  from   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                "  p.PAC_CODIGO=a.PAC_CODIGO " +
                " AND  A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO AND  ATE_FECHA_INGRESO > '01/01/2011 0:00:00' /*AND ATE_FECHA_ALTA IS NOT  null and esc_codigo<=2 */ ORDER  BY   ATE_CODIGO DESC ";

            }



            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "atenciones");
            return Dts.Tables["atenciones"];

        }
        public DataTable cargar_atencionesCuentas()
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
                          "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION" +
                          ",r.TIR_NOMBRE as referido,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                          " convert(datetime,a.ATE_FACTURA_FECHA,120)as FECHAF" +
                           " ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA," +
                          " convert(datetime,a.ATE_FECHA_ALTA,120)as FECHAA FROM  ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H  WHERE   " +
                          "p.PAC_CODIGO=a.PAC_CODIGO AND A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO AND  A.ATE_FECHA_INGRESO > '01/01/2011 0:00:00'"+
                          " AND A.ESC_CODIGO <= 2 ORDER  BY   ATE_CODIGO DESC ";

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
            //string cad1 = "SELECT [codclas] as TE_CODIGO ,[desclas] as TE_DESCRIPCION from [Sic3000].[dbo].[Clasificacion]";
            string cad1 = "SELECT * FROM Sic3000..Clasificacion where cartera=1 order by 2 asc";
            Sqlcmd = new SqlCommand(cad1, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "Tabla");
            return Dts;
        }        

        public DataSet combo_fomapago(String cod)
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
            string cad1 = "SELECT forpag as TE_CODIGO, despag as TE_DESCRIPCION FROM [Sic3000].[dbo].Forma_Pago where claspag=(SELECT codclas FROM SIC3000..CLASIFICACION where desclas='" + cod.Trim() + "') AND  ActivarFacturaHis = 1 order by 2 asc";
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
            //string cad1 = "select * from TIPO_FORMA_PAGO where TIF_CODIGO !=3 and TIF_CODIGO!=4 ";
            string cad1 = "select * from TIPO_FORMA_PAGO";
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
                            " from FORMA_PAGO where FOR_ESTADO = 1 AND TIF_CODIGO !=4 and TIF_CODIGO!=3 and TIF_CODIGO="+codigo+" ORDER BY DESCRIPCION ASC";
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
            string cad1 = "select FOR_CODIGO, FOR_COMISION, FOR_REFERIDO from FORMA_PAGO  where FOR_DESCRIPCION = '" + descripcion+"'";
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
        public DataTable cargar_aseguradoras_empresas(string tipo_empresa, string atencion)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
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

            Sqlcmd = new SqlCommand("sp_cargar_aseguradoras_empresas", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@tipo_empresa", SqlDbType.VarChar);
            Sqlcmd.Parameters["@tipo_empresa"].Value = (tipo_empresa);

            Sqlcmd.Parameters.Add("@atencion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@atencion"].Value = (atencion);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Dts.Tables["tabla"];

        
        //BaseContextoDatos conect = new BaseContextoDatos();
        //Sqlcon = conect.ConectarBd();
        //try
        //{
        //    Sqlcon.Open();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine(ex.Message);
        //}
        //string cad1 = "SELECT     dbo.ATENCIONES.ATE_CODIGO, dbo.ASEGURADORAS_EMPRESAS.ASE_NOMBRE, dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE," +
        //              "Sic3000..Forma_Pago.forpag, Sic3000..Forma_Pago.despag , Sic3000..Forma_Pago.comision" +
        //              "FROM dbo.ATENCION_DETALLE_CATEGORIAS INNER JOIN" +
        //              "dbo.ATENCIONES ON dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO INNER JOIN" +
        //              "dbo.CATEGORIAS_CONVENIOS ON dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO = dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO INNER JOIN" +
        //              "dbo.ASEGURADORAS_EMPRESAS ON dbo.CATEGORIAS_CONVENIOS.ASE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO INNER JOIN" +
        //              "dbo.TIPO_EMPRESA ON dbo.ASEGURADORAS_EMPRESAS.TE_CODIGO = dbo.TIPO_EMPRESA.TE_CODIGO INNER JOIN" +
        //              "Sic3000..Forma_Pago ON dbo.ASEGURADORAS_EMPRESAS.FORPAG_SIC = Sic3000..Forma_Pago.forpag WHERE(dbo.ATENCIONES.ATE_CODIGO =" + atencion + ")" +
        //              "AND(dbo.TIPO_EMPRESA.TE_CODIGO = " + tipo_empresa + ")";

        //    //"SELECT     dbo.ATENCIONES.ATE_CODIGO, dbo.ASEGURADORAS_EMPRESAS.ASE_NOMBRE, dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE," + 
        //    //          " dbo.FORMA_PAGO.FOR_CODIGO, dbo.FORMA_PAGO.FOR_DESCRIPCION , dbo.FORMA_PAGO.FOR_COMISION,dbo.FORMA_PAGO.FOR_REFERIDO"+
        //    //          " FROM dbo.ATENCION_DETALLE_CATEGORIAS INNER JOIN"+
        //    //          " dbo.ATENCIONES ON dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO INNER JOIN "+
        //    //          " dbo.CATEGORIAS_CONVENIOS ON dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO = dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO INNER JOIN"+
        //    //          " dbo.ASEGURADORAS_EMPRESAS ON dbo.CATEGORIAS_CONVENIOS.ASE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO INNER JOIN"+
        //    //          " dbo.TIPO_EMPRESA ON dbo.ASEGURADORAS_EMPRESAS.TE_CODIGO = dbo.TIPO_EMPRESA.TE_CODIGO INNER JOIN"+
        //    //          " dbo.FORMA_PAGO ON dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO = dbo.FORMA_PAGO.ASE_CODIGO WHERE (dbo.ATENCIONES.ATE_CODIGO ="+atencion+")"+
        //    //          " AND (dbo.TIPO_EMPRESA.TE_CODIGO ="+tipo_empresa+")";
        //Sqlcmd = new SqlCommand(cad1, Sqlcon);
        //Sqldap = new SqlDataAdapter();
        //Sqldap.SelectCommand = Sqlcmd;
        //Dts = new DataSet();
        //Sqldap.Fill(Dts, "Tabla");
        //return Dts.Tables["Tabla"];
    }
       public DataTable buscar_atenciones(string hcl,string nombre,string id, string habitacion, string fecFacHis)
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
                        "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
                        "+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                        "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                        " convert(datetime,a.ATE_FACTURA_FECHA,120)as fecha1" +
                        "  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
                        " convert(datetime,a.ATE_FECHA_ALTA,120)as  FECHA_ALTA  from   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                        " p.PAC_CODIGO=a.PAC_CODIGO and p.PAC_HISTORIA_CLINICA like '%"+hcl+"%'"+
                        " AND A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO  AND   convert(date,a.ATE_FACTURA_FECHA)  ='" + fecFacHis + "' and esc_codigo=6  ORDER  BY   ATE_CODIGO DESC ";

                      //sql = "select TOP  1000 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION,P.PAC_HISTORIA_CLINICA AS  HCL," +
                      // "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
                      // "+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                      // "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                      // " convert(datetime,a.ATE_FACTURA_FECHA,120)as fecha1" +
                      // "  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
                      // " convert(datetime,a.ATE_FECHA_ALTA,120)as  FECHA_ALTA  from   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                      // " p.PAC_CODIGO=a.PAC_CODIGO and p.PAC_HISTORIA_CLINICA like '%" + hcl + "%'" +
                      // " AND A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO  AND  ATE_FECHA_INGRESO > '01/01/2011 0:00:00' /*AND ATE_FECHA_ALTA IS NOT  null*/ and esc_codigo<=2  ORDER  BY   ATE_CODIGO DESC ";

            }
            if (id != "")
            {
                sql = "select TOP  1000 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION,P.PAC_HISTORIA_CLINICA AS  HCL," +
                "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
                "+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ,"+
                " convert(datetime,a.ATE_FACTURA_FECHA,120)as fecha1"+
                "  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
                " convert(datetime,a.ATE_FECHA_ALTA,120)as  FECHA_ALTA  from   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                " p.PAC_CODIGO=a.PAC_CODIGO and p.PAC_IDENTIFICACION='" + id + "'" +
                " AND A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO AND   convert(date,a.ATE_FACTURA_FECHA)  ='" + fecFacHis + "' and esc_codigo=6 ORDER  BY   ATE_CODIGO DESC ";

              //  sql = "select TOP  1000 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION,P.PAC_HISTORIA_CLINICA AS  HCL," +
              //"P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
              //"+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
              //"P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
              //" convert(datetime,a.ATE_FACTURA_FECHA,120)as fecha1" +
              //"  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
              //" convert(datetime,a.ATE_FECHA_ALTA,120)as  FECHA_ALTA  from   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
              //" p.PAC_CODIGO=a.PAC_CODIGO and p.PAC_IDENTIFICACION='" + id + "'" +
              //" AND A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO AND  ATE_FECHA_INGRESO > '01/01/2011 0:00:00' /*AND ATE_FECHA_ALTA IS NOT  null*/ and esc_codigo<=2 ORDER  BY   ATE_CODIGO DESC ";


            }
            if (nombre != "")
            {
                string cadena_nombre = nombre + " ";
                int y = cadena_nombre.IndexOf(" ");
                string nombre_1 = cadena_nombre.Substring(y + 1);
                string apellido = cadena_nombre.Substring(0, y + 1);

                sql = "select TOP  1000 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION,P.PAC_HISTORIA_CLINICA AS  HCL," +
                "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
                "+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                " convert(datetime,a.ATE_FACTURA_FECHA,120)as fecha1" +
                "  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
                " convert(datetime,a.ATE_FECHA_ALTA,120)as  FECHA_ALTA  from   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                "p.PAC_CODIGO=a.PAC_CODIGO and p.PAC_APELLIDO_PATERNO like '" + apellido.Trim() + "%' and  p.PAC_APELLIDO_MATERNO like '" + nombre_1.Trim() + "%'" +
                "  AND A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO AND   convert(date,a.ATE_FACTURA_FECHA)  ='" + fecFacHis + "' and esc_codigo=6 ORDER  BY   ATE_CODIGO DESC ";

              //  sql = "select TOP  1000 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION,P.PAC_HISTORIA_CLINICA AS  HCL," +
              //"P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
              //"+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
              //"P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
              //" convert(datetime,a.ATE_FACTURA_FECHA,120)as fecha1" +
              //"  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
              //" convert(datetime,a.ATE_FECHA_ALTA,120)as  FECHA_ALTA  from   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
              //"p.PAC_CODIGO=a.PAC_CODIGO and p.PAC_APELLIDO_PATERNO like '" + apellido.Trim() + "%' and  p.PAC_APELLIDO_MATERNO like '" + nombre_1.Trim() + "%'" +
              //"  AND A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO AND  ATE_FECHA_INGRESO > '01/01/2011 0:00:00' /*AND ATE_FECHA_ALTA IS NOT  null*/ and esc_codigo<=2 ORDER  BY   ATE_CODIGO DESC ";

            }
            if (habitacion != "")
            {
                sql = "select TOP  1000 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION,P.PAC_HISTORIA_CLINICA AS  HCL," +
                        "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
                        "+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                        "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                        " convert(datetime,a.ATE_FACTURA_FECHA,120)as fecha1" +
                        "  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
                        " convert(datetime,a.ATE_FECHA_ALTA,120)as  FECHA_ALTA  from   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                        " p.PAC_CODIGO=a.PAC_CODIGO " +
                        " AND A.HAB_CODIGO=H.hab_Codigo and H.hab_Numero ='" + habitacion + "' and a.TIR_CODIGO =r.TIR_CODIGO  AND   convert(date,a.ATE_FACTURA_FECHA)  ='" + fecFacHis + "' and esc_codigo=6  ORDER  BY   a.ATE_FECHA_ALTA DESC ";

                //sql = "select TOP  1000 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION,P.PAC_HISTORIA_CLINICA AS  HCL," +
                //       "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
                //       "+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                //       "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                //       " convert(datetime,a.ATE_FACTURA_FECHA,120)as fecha1" +
                //       "  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
                //       " convert(datetime,a.ATE_FECHA_ALTA,120)as  FECHA_ALTA  from   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                //       " p.PAC_CODIGO=a.PAC_CODIGO " +
                //       " AND A.HAB_CODIGO=H.hab_Codigo and H.hab_Numero ='" + habitacion + "' and a.TIR_CODIGO =r.TIR_CODIGO  AND  ATE_FECHA_INGRESO > '01/01/2011 0:00:00' /*AND ATE_FECHA_ALTA IS NOT  null*/ and esc_codigo<=2  ORDER  BY   a.ATE_FECHA_ALTA DESC ";


            }

            if (sql == "")
            {
                // CAMBIO HR 02122019

                sql = "select TOP  1000 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION,P.PAC_HISTORIA_CLINICA AS  HCL," +
                "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
                "+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                " convert(datetime,a.ATE_FACTURA_FECHA,120)as fecha1" +
                "  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
                " convert(datetime,a.ATE_FECHA_ALTA,120)as  FECHA_ALTA  from   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                " p.PAC_CODIGO=a.PAC_CODIGO " +
                " AND A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO AND  convert(date,a.ATE_FACTURA_FECHA)  ='" + fecFacHis + "' and esc_codigo=6 ORDER  BY   ATE_CODIGO DESC ";



                //  sql = "select TOP  1000 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION,P.PAC_HISTORIA_CLINICA AS  HCL," +
                //"P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
                //"+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                //"P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                //" convert(datetime,a.ATE_FACTURA_FECHA,120)as fecha1" +
                //"  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA_INGRESO," +
                //" convert(datetime,a.ATE_FECHA_ALTA,120)as  FECHA_ALTA  from   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                //" /* p.PAC_CODIGO=a.PAC_CODIGO and p.PAC_IDENTIFICACION='" + id + "'" +
                //" AND */ A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO AND  ATE_FECHA_INGRESO > '01/01/2011 0:00:00' /*AND ATE_FECHA_ALTA IS NOT  null*/ and esc_codigo<=2 ORDER  BY   ATE_CODIGO DESC ";


            }



            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "atenciones");
            return Dts.Tables["atenciones"];

        }


       public DataTable buscar_atencionesCuentas(string hcl, string nombre, string id)
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
                             "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
                             "+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                             "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                         " convert(datetime,a.ATE_FACTURA_FECHA,120)as FECHAF" +
                         " ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA," +
                         " convert(datetime,a.ATE_FECHA_ALTA,120) as FECHAA FROM   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                             " p.PAC_CODIGO=a.PAC_CODIGO and p.PAC_HISTORIA_CLINICA like '%" + hcl + "%' and ESC_CODIGO <= 2" +
                             " AND A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO  AND A.ATE_FECHA_INGRESO >= '01/12/2011 0:00:00' ORDER  BY   ATE_CODIGO DESC ";


           }
           if (id != "")
           {
               sql = "select TOP  1000 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION,P.PAC_HISTORIA_CLINICA AS  HCL," +
                             "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
                             "+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                             "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                        " convert(datetime,a.ATE_FACTURA_FECHA,120)as FECHAF" +
                        "  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA," +
                         " convert(datetime,a.ATE_FECHA_ALTA,120)as  FECHAA  from   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                             " p.PAC_CODIGO=a.PAC_CODIGO and p.PAC_IDENTIFICACION='" + id + "'" +
                             " AND A.ESC_CODIGO <= 2 AND A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO  AND A.ATE_FECHA_INGRESO >= '01/12/2011 0:00:00' ORDER  BY   ATE_CODIGO DESC ";


           }
           if (nombre != "")
           {
               string cadena_nombre = nombre + " ";
               int y = cadena_nombre.IndexOf(" ");
               string nombre_1 = cadena_nombre.Substring(y + 1);
               string apellido = cadena_nombre.Substring(0, y + 1);

               sql = "select TOP  1000 p.PAC_CODIGO AS  CODIGO,A.ATE_CODIGO AS  ATENCION,P.PAC_HISTORIA_CLINICA AS  HCL," +
                             "P.PAC_APELLIDO_PATERNO+' '+P.PAC_APELLIDO_MATERNO+'  '" +
                             "+P.PAC_NOMBRE1+ ' '+P.PAC_NOMBRE2 AS NOMBRES," +
                             "P.PAC_IDENTIFICACION AS ID ,H.hab_Numero AS HABITACION ,r.TIR_NOMBRE as referido ,A.ATE_NUMERO_CONTROL as CONTROL,A.ATE_FACTURA_PACIENTE AS FACTURA ," +
                        " convert(datetime,a.ATE_FACTURA_FECHA,120)as  FECHAF" +
                        "  ,convert(datetime,a.ATE_FECHA_INGRESO,120)as FECHA," +
                        "  convert(datetime,a.ATE_FECHA_ALTA,120)as FECHAA  FROM   ATENCIONES A,PACIENTES P ,TIPO_REFERIDO r ,HABITACIONES H WHERE   " +
                             "p.PAC_CODIGO=a.PAC_CODIGO and p.PAC_APELLIDO_PATERNO like '" + apellido.Trim() + "%' and  p.PAC_APELLIDO_MATERNO like '" + nombre_1.Trim() + "%'" +
                             "  AND A.ESC_CODIGO <= 2 AND A.HAB_CODIGO=H.hab_Codigo and a.TIR_CODIGO =r.TIR_CODIGO AND A.ATE_FECHA_INGRESO >= '01/12/2011 0:00:00' ORDER  BY   ATE_CODIGO DESC ";


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
