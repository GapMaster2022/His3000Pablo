using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using His.Entidades.Reportes;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace His.Datos
{
    public class DatAtenciones
    {
        public bool RPIS(int codigo)
        {

            bool r = false;
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

            Sqlcmd = new SqlCommand("select t.TE_CODIGO  from CATEGORIAS_CONVENIOS as c, ASEGURADORAS_EMPRESAS as e, TIPO_EMPRESA as t "
                + " where c.ASE_CODIGO = e.ASE_CODIGO and e.TE_CODIGO = t.TE_CODIGO and CAT_CODIGO = " + codigo + " ", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            if ((Convert.ToInt32(Dts.Rows[0][0])) == 5)
                r = true;
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return r;

        }
        public ATENCIONES_SUBSECUENTES RecuperaAtencionSub(Int64 ATE_CODIGO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from sub in db.ATENCIONES_SUBSECUENTES
                        where sub.ate_codigo_subsecuente == ATE_CODIGO
                        select sub).FirstOrDefault();
            }
        }
        public string tipo_atencion(int cod_tipoAtencion)
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
            Sqlcmd = new SqlCommand("SELECT  name  FROM [His3000].[dbo].[tipos_atenciones] where id =" + cod_tipoAtencion + " ", Sqlcon);

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

            if (Dts.Rows.Count > 0)
            {
                return (cod_tipoAtencion + "  - " + Convert.ToString(Dts.Rows[0][0]));
            }
            else
            {
                return "0";
            }

        }
        public string tipo_discapacidad(int cod_tipoAtencion)
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
            Sqlcmd = new SqlCommand("SELECT  name  FROM [His3000].[dbo].[tipos_discapacidades] where id =" + cod_tipoAtencion + " ", Sqlcon);

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

            if (Dts.Rows.Count > 0)
            {
                return (cod_tipoAtencion + "  - " + Convert.ToString(Dts.Rows[0][0]));
            }
            else
            {
                return "0";
            }

        }
        public DataTable tipos_discapacidades()
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
            Sqlcmd = new SqlCommand("SELECT  id, name  FROM [His3000].[dbo].[tipos_discapacidades]", Sqlcon);

            Sqlcmd.CommandType = CommandType.Text;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        }
        public DtoAtencionDatosAdicionales atencionDA_find(int codigo)
        {
            DtoAtencionDatosAdicionales pda = null;
            int r;
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

            Sqlcmd = new SqlCommand("SELECT  count(id)  FROM [His3000].[dbo].[atenciones_datos_adicionales] where ate_codigo=" + codigo + " ", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            r = Convert.ToInt16(Dts.Rows[0][0]);

            if (r == 1)
            {
                DataTable Dts2 = new DataTable();
                //      Sqlcmd = new SqlCommand(" select *, (SELECT  CONCAT(id, '  - ', name) AS TIPO  FROM[His3000].[dbo].[tipos_discapacidades] AS TD WHERE A.id_tiposdiscapacidades = TD.id) AS TIPO "
                //          +" from atenciones_datos_adicionales AS A where ate_codigo=" + codigo + " ", Sqlcon);
                Sqlcmd = new SqlCommand(" select * from atenciones_datos_adicionales AS A where ate_codigo=" + codigo + " ", Sqlcon);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts2);

                pda = new DtoAtencionDatosAdicionales();

                pda.cod_atencion = Convert.ToInt32(Dts2.Rows[0]["ate_codigo"]);
                pda.empresa = Convert.ToString(Dts2.Rows[0]["ASEGURADO_EMPRESA"]);
                pda.observaciones = Convert.ToString(Dts2.Rows[0]["ASEGURADO_OBSERVACION"]);
                //pda.tipo_discapacidad = Convert.ToString(Dts2.Rows[0]["TIPO"]);
                pda.porcentage_discapacidad = Convert.ToInt32(Dts2.Rows[0]["porcentage"]);
                pda.tipo_discapacidad = Convert.ToString(Dts2.Rows[0]["id_tiposdiscapacidades"]);
                pda.cod_atencion = Convert.ToInt32(Dts2.Rows[0]["ate_codigo"]);
                if (Convert.ToString(Dts2.Rows[0]["paquete"]).Trim() == string.Empty)
                    pda.paquete = "0";
                else if (Convert.ToString(Dts2.Rows[0]["paquete"]).Trim() == null)
                    pda.paquete = "0";
                else
                    pda.paquete = Convert.ToString(Dts2.Rows[0]["paquete"]);
            }

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return pda;



        }
        public void atencionDA_save(DtoAtencionDatosAdicionales pda)
        {
            if (pda.paquete == null)
            {
                pda.paquete = "";
            }
            string cadena_sql = "IF not EXISTS(SELECT id FROM atenciones_datos_adicionales WHERE ate_codigo = '" + pda.cod_atencion + "') "
                    + "INSERT INTO atenciones_datos_adicionales(ate_codigo, ASEGURADO_EMPRESA, ASEGURADO_OBSERVACION,"
                                                                + " id_tiposdiscapacidades,porcentage,paquete) "
                                                + "VALUES(" + pda.cod_atencion + ", '"
                                                + pda.empresa + "', '" + pda.observaciones + "', "
                                                + pda.tipo_discapacidad + ", " + pda.porcentage_discapacidad + ", '" + pda.paquete + "') "
                + "ELSE "
                    + "UPDATE atenciones_datos_adicionales SET ASEGURADO_EMPRESA = '" + pda.empresa + "', "
                    + "ASEGURADO_OBSERVACION = '" + pda.observaciones + "', "
                    + "id_tiposdiscapacidades = " + pda.tipo_discapacidad + ", "
                    + "paquete = '" + pda.paquete + "', "
                    + "porcentage = " + pda.porcentage_discapacidad + " "
                    + "WHERE ate_codigo = " + pda.cod_atencion + " "
                ;

            BaseContextoDatos obj = new BaseContextoDatos();

            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();

            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            Console.WriteLine("Se guardo/actualizo datos adicionales de atencion " + pda.cod_atencion + ".");

            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<DtoAtenciones> RecuperaAtencionesFormulario()
        {
            //List<DtoAtenciones> atenciongrid = new List<DtoAtenciones>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //List<ATENCIONES> honorarios = new List<ATENCIONES>();

                return (from t in contexto.ATENCIONES
                        join p in contexto.PACIENTES on t.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                        join h in contexto.HABITACIONES on t.HABITACIONES.hab_Codigo equals h.hab_Codigo
                        join a in contexto.PACIENTES_DATOS_ADICIONALES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                        join c in contexto.CAJAS on t.CAJAS.CAJ_CODIGO equals c.CAJ_CODIGO
                        where p.PAC_CODIGO == a.PACIENTES.PAC_CODIGO && a.DAP_ESTADO == true
                        orderby t.ATE_FECHA
                        select new DtoAtenciones
                        {
                            ATE_CODIGO = t.ATE_CODIGO,
                            ATE_NUMERO_CONTROL = t.ATE_NUMERO_CONTROL,
                            ATE_FACTURA_PACIENTE = t.ATE_FACTURA_PACIENTE,
                            ATE_FACTURA_FECHA = t.ATE_FACTURA_FECHA.Value,
                            PAC_CODIGO = p.PAC_CODIGO,
                            PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                            PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                            PAC_NOMBRE = p.PAC_NOMBRE1,
                            PAC_NOMBRE2 = p.PAC_NOMBRE2,
                            PAC_HCL = p.PAC_HISTORIA_CLINICA,
                            PAC_CEDULA = p.PAC_IDENTIFICACION,
                            PAC_DIRECCION = a.DAP_DIRECCION_DOMICILIO,
                            PAC_TELEFONO = a.DAP_TELEFONO1,
                            HAB_CODIGO = h.hab_Codigo,
                            HAB_NUMERO = h.hab_Numero,
                            ATE_FECHA = t.ATE_FECHA.Value,
                            ATE_FECHA_INGRESO = t.ATE_FECHA_INGRESO.Value,
                            ATE_FECHA_ALTA = t.ATE_FECHA_ALTA.Value,
                            ATE_ESTADO = t.ATE_ESTADO.Value,
                            ATE_REFERIDO = t.ATE_REFERIDO.Value,
                            CAJ_CODIGO = c.CAJ_CODIGO

                        }).ToList();


                ////honorarios = contexto.ATENCIONES.Include("PACIENTES").Include("HABITACIONES").OrderBy(p => p.ATE_FECHA).ToList();
                //foreach (var acceso in atenciones)
                //{
                //    atenciongrid.Add(new DtoAtenciones()
                //    {
                //        //ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName(),
                //        //ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key,
                //        ATE_CODIGO = acceso.ATE_CODIGO,
                //        ATE_NUMERO_CONTROL = acceso.ATE_NUMERO_CONTROL,
                //        ATE_FACTURA_PACIENTE = acceso.ATE_FACTURA_PACIENTE,
                //        PAC_CODIGO = acceso.PAC_CODIGO,
                //        PAC_APELLIDO_PATERNO=acceso.PAC_APELLIDO_PATERNO,
                //        PAC_APELLIDO_MATERNO=acceso.PAC_APELLIDO_MATERNO,
                //        PAC_NOMBRE=acceso.PAC_NOMBRE1,
                //        PAC_NOMBRE2=acceso.PAC_NOMBRE2,
                //        PAC_HCL=acceso.PAC_HISTORIA_CLINICA,
                //        PAC_CEDULA=acceso.PAC_IDENTIFICACION,
                //        PAC_DIRECCION=acceso.DAP_DIRECCION_DOMICILIO,
                //        PAC_TELEFONO=acceso.DAP_TELEFONO1,
                //        //HAB_CODIGO = acceso.HAB_CODIGO.Value,
                //        HAB_NUMERO = acceso.hab_Numero,
                //        ATE_FACTURA_FECHA=Convert.ToDateTime(acceso.ATE_FACTURA_FECHA),
                //        ATE_FECHA=Convert.ToDateTime(acceso.ATE_FECHA),
                //        ATE_FECHA_INGRESO=Convert.ToDateTime(acceso.ATE_FECHA_INGRESO),
                //        ATE_FECHA_ALTA=Convert.ToDateTime(acceso.ATE_FECHA_ALTA),
                //        ATE_ESTADO=Convert.ToBoolean(acceso.ATE_ESTADO),
                //        ATE_REFERIDO=Convert.ToBoolean(acceso.ATE_REFERIDO),
                //        CAJ_CODIGO=acceso.CAJ_CODIGO
                //    });
                //}
                //return atenciongrid;
            }
        }

        public string RecuperaAseguradoraAtencion(Int64 ateCodigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                string aseguradora = (from a in contexto.ATENCIONES
                                      join d in contexto.ATENCION_DETALLE_CATEGORIAS on a.ATE_CODIGO equals d.ATENCIONES.ATE_CODIGO
                                      join c in contexto.CATEGORIAS_CONVENIOS on d.CATEGORIAS_CONVENIOS.CAT_CODIGO equals c.CAT_CODIGO
                                      where a.ATE_CODIGO == ateCodigo
                                      select c.CAT_NOMBRE).FirstOrDefault();
                return aseguradora;
            }
        }

        public List<DtoAtenciones> RecuperaAtenciones(int codigoCaja, DateTime fecha, Int16 codUsuario)
        {
            DateTime fecFactura = new DateTime(fecha.Year, fecha.Month, fecha.Day);
            //DateTime min = new DateTime(fecha.Year, fecha.Month, fecha.Day,0,0,0);
            //DateTime max = new DateTime(fecha.Year, fecha.Month, fecha.Day, 23,59,59);

            List<DtoAtenciones> atenciongrid = new List<DtoAtenciones>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var atenciones = (from t in contexto.ATENCIONES
                                  join p in contexto.PACIENTES on t.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                  join h in contexto.HABITACIONES on t.HABITACIONES.hab_Codigo equals h.hab_Codigo
                                  //join a in contexto.PACIENTES_DATOS_ADICIONALES on t.PACIENTES_DATOS_ADICIONALES.DAP_CODIGO equals a.DAP_CODIGO
                                  join c in contexto.CAJAS on t.CAJAS.CAJ_CODIGO equals c.CAJ_CODIGO
                                  join u in contexto.USUARIOS on t.USUARIOS.ID_USUARIO equals u.ID_USUARIO
                                  //p.PAC_CODIGO == a.PACIENTES.PAC_CODIGO
                                  where //u.ID_USUARIO == codUsuario
                                        //&& a.DAP_ESTADO == true
                                  t.ATE_FACTURA_FECHA != null
                                  && t.ATE_FECHA != null
                                  && t.ATE_FECHA_ALTA != null
                                  && t.ATE_FACTURA_FECHA == fecFactura
                                  orderby t.ATE_FECHA
                                  select new
                                  {
                                      t.ATE_CODIGO,
                                      t.ATE_NUMERO_CONTROL,
                                      t.ATE_FACTURA_PACIENTE,
                                      t.PACIENTES.PAC_CODIGO,
                                      t.HABITACIONES.hab_Codigo,
                                      p.PAC_APELLIDO_PATERNO,
                                      p.PAC_APELLIDO_MATERNO,
                                      p.PAC_NOMBRE1,
                                      p.PAC_NOMBRE2,
                                      p.PAC_HISTORIA_CLINICA,
                                      //p.PAC_TIPO_IDENTIFICACION,
                                      //p.PAC_IDENTIFICACION,
                                      //a.DAP_DIRECCION_DOMICILIO,
                                      //a.DAP_TELEFONO1,
                                      h.hab_Numero,
                                      t.PACIENTES_DATOS_ADICIONALES.DAP_CODIGO,
                                      t.ATE_FACTURA_FECHA,
                                      t.ATE_FECHA,
                                      t.ATE_FECHA_INGRESO,
                                      t.ATE_FECHA_ALTA,
                                      t.ATE_ESTADO,
                                      t.ATE_REFERIDO,
                                      c.CAJ_CODIGO
                                  }).ToList();

                //if (codigoCaja != 0)
                //{
                //    atenciones.Where(a=>a.CAJ_CODIGO==codigoCaja);   
                //}

                foreach (var acceso in atenciones)
                {
                    atenciongrid.Add(new DtoAtenciones()
                    {
                        //ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName(),
                        //ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key,
                        ATE_CODIGO = acceso.ATE_CODIGO,
                        ATE_NUMERO_CONTROL = acceso.ATE_NUMERO_CONTROL,
                        ATE_FACTURA_PACIENTE = acceso.ATE_FACTURA_PACIENTE,
                        PAC_CODIGO = acceso.PAC_CODIGO,
                        DAP_CODIGO = acceso.DAP_CODIGO,
                        PAC_APELLIDO_PATERNO = acceso.PAC_APELLIDO_PATERNO,
                        PAC_APELLIDO_MATERNO = acceso.PAC_APELLIDO_MATERNO,
                        PAC_NOMBRE = acceso.PAC_NOMBRE1,
                        PAC_NOMBRE2 = acceso.PAC_NOMBRE2,
                        PAC_HCL = acceso.PAC_HISTORIA_CLINICA,
                        //PAC_CEDULA = acceso.PAC_IDENTIFICACION,
                        //PAC_DIRECCION = acceso.DAP_DIRECCION_DOMICILIO,
                        //PAC_TELEFONO = acceso.DAP_TELEFONO1,
                        HAB_CODIGO = acceso.hab_Codigo,
                        HAB_NUMERO = acceso.hab_Numero,
                        ATE_FACTURA_FECHA = Convert.ToDateTime(acceso.ATE_FACTURA_FECHA),
                        ATE_FECHA = Convert.ToDateTime(acceso.ATE_FECHA),
                        ATE_FECHA_INGRESO = Convert.ToDateTime(acceso.ATE_FECHA_INGRESO),
                        ATE_FECHA_ALTA = Convert.ToDateTime(acceso.ATE_FECHA_ALTA),
                        ATE_ESTADO = Convert.ToBoolean(acceso.ATE_ESTADO),
                        ATE_REFERIDO = Convert.ToBoolean(acceso.ATE_REFERIDO),
                        CAJ_CODIGO = acceso.CAJ_CODIGO
                    });
                }
                return atenciongrid;
            }
        }

        public List<DtoAtenciones> RecuperaAtencionesActivas(string buscar, int criterio, int cantidad)
        {
            //DateTime fecFactura = new DateTime(fecha.Year, fecha.Month, fecha.Day);
            //DateTime min = new DateTime(fecha.Year, fecha.Month, fecha.Day,0,0,0);
            //DateTime max = new DateTime(fecha.Year, fecha.Month, fecha.Day, 23,59,59);

            List<DtoAtenciones> atenciongrid = new List<DtoAtenciones>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                #region NUMERO_ATENCION
                if (criterio == 1)
                {
                    var atenciones = from t in contexto.ATENCIONES
                                     join p in contexto.PACIENTES on t.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                     join h in contexto.HABITACIONES on t.HABITACIONES.hab_Codigo equals h.hab_Codigo
                                     join a in contexto.PACIENTES_DATOS_ADICIONALES on t.PACIENTES_DATOS_ADICIONALES.DAP_CODIGO equals a.DAP_CODIGO
                                     join c in contexto.CAJAS on t.CAJAS.CAJ_CODIGO equals c.CAJ_CODIGO
                                     join u in contexto.USUARIOS on t.USUARIOS.ID_USUARIO equals u.ID_USUARIO
                                     where a.DAP_ESTADO == true
                                     && t.ATE_FECHA_ALTA == null
                                     && t.ATE_NUMERO_ATENCION.StartsWith(buscar)
                                     orderby t.ATE_FECHA
                                     select new
                                     {
                                         t.ATE_CODIGO,
                                         t.ATE_NUMERO_CONTROL,
                                         t.ATE_NUMERO_ATENCION,
                                         t.ATE_FACTURA_PACIENTE,
                                         t.PACIENTES.PAC_CODIGO,
                                         t.HABITACIONES.hab_Codigo,
                                         p.PAC_APELLIDO_PATERNO,
                                         p.PAC_APELLIDO_MATERNO,
                                         p.PAC_NOMBRE1,
                                         p.PAC_NOMBRE2,
                                         p.PAC_HISTORIA_CLINICA,
                                         h.hab_Numero,
                                         t.PACIENTES_DATOS_ADICIONALES.DAP_CODIGO,
                                         t.ATE_FACTURA_FECHA,
                                         t.ATE_FECHA,
                                         t.ATE_FECHA_INGRESO,
                                         t.ATE_FECHA_ALTA,
                                         t.ATE_ESTADO,
                                         t.ATE_REFERIDO,
                                         c.CAJ_CODIGO
                                     };
                    atenciones = atenciones.Take(cantidad);
                    foreach (var acceso in atenciones)
                    {
                        atenciongrid.Add(new DtoAtenciones()
                        {

                            ATE_CODIGO = acceso.ATE_CODIGO,
                            ATE_NUMERO_CONTROL = acceso.ATE_NUMERO_CONTROL,
                            ATE_NUMERO_ATENCION = acceso.ATE_NUMERO_ATENCION,
                            ATE_FACTURA_PACIENTE = acceso.ATE_FACTURA_PACIENTE,
                            PAC_CODIGO = acceso.PAC_CODIGO,
                            DAP_CODIGO = acceso.DAP_CODIGO,
                            PAC_APELLIDO_PATERNO = acceso.PAC_APELLIDO_PATERNO,
                            PAC_APELLIDO_MATERNO = acceso.PAC_APELLIDO_MATERNO,
                            PAC_NOMBRE = acceso.PAC_NOMBRE1,
                            PAC_NOMBRE2 = acceso.PAC_NOMBRE2,
                            PAC_HCL = acceso.PAC_HISTORIA_CLINICA,
                            HAB_CODIGO = acceso.hab_Codigo,
                            HAB_NUMERO = acceso.hab_Numero,
                            ATE_FECHA = Convert.ToDateTime(acceso.ATE_FECHA),
                            ATE_FECHA_INGRESO = Convert.ToDateTime(acceso.ATE_FECHA_INGRESO),
                            ATE_ESTADO = Convert.ToBoolean(acceso.ATE_ESTADO),
                            ATE_REFERIDO = Convert.ToBoolean(acceso.ATE_REFERIDO),
                            CAJ_CODIGO = acceso.CAJ_CODIGO
                        });
                    }
                }
                #endregion

                #region POR HISTORIA CLINICA
                else if (criterio == 2)
                {
                    var atenciones = from t in contexto.ATENCIONES
                                     join p in contexto.PACIENTES on t.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                     join h in contexto.HABITACIONES on t.HABITACIONES.hab_Codigo equals h.hab_Codigo
                                     join a in contexto.PACIENTES_DATOS_ADICIONALES on t.PACIENTES_DATOS_ADICIONALES.DAP_CODIGO equals a.DAP_CODIGO
                                     join c in contexto.CAJAS on t.CAJAS.CAJ_CODIGO equals c.CAJ_CODIGO
                                     join u in contexto.USUARIOS on t.USUARIOS.ID_USUARIO equals u.ID_USUARIO
                                     where a.DAP_ESTADO == true
                                     && t.ATE_FECHA_ALTA == null
                                     && p.PAC_HISTORIA_CLINICA.StartsWith(buscar)
                                     orderby t.ATE_FECHA
                                     select new
                                     {
                                         t.ATE_CODIGO,
                                         t.ATE_NUMERO_CONTROL,
                                         t.ATE_NUMERO_ATENCION,
                                         t.ATE_FACTURA_PACIENTE,
                                         t.PACIENTES.PAC_CODIGO,
                                         t.HABITACIONES.hab_Codigo,
                                         p.PAC_APELLIDO_PATERNO,
                                         p.PAC_APELLIDO_MATERNO,
                                         p.PAC_NOMBRE1,
                                         p.PAC_NOMBRE2,
                                         p.PAC_HISTORIA_CLINICA,
                                         h.hab_Numero,
                                         t.PACIENTES_DATOS_ADICIONALES.DAP_CODIGO,
                                         t.ATE_FACTURA_FECHA,
                                         t.ATE_FECHA,
                                         t.ATE_FECHA_INGRESO,
                                         t.ATE_FECHA_ALTA,
                                         t.ATE_ESTADO,
                                         t.ATE_REFERIDO,
                                         c.CAJ_CODIGO
                                     };
                    atenciones = atenciones.Take(cantidad);
                    foreach (var acceso in atenciones)
                    {
                        atenciongrid.Add(new DtoAtenciones()
                        {

                            ATE_CODIGO = acceso.ATE_CODIGO,
                            ATE_NUMERO_CONTROL = acceso.ATE_NUMERO_CONTROL,
                            ATE_NUMERO_ATENCION = acceso.ATE_NUMERO_ATENCION,
                            ATE_FACTURA_PACIENTE = acceso.ATE_FACTURA_PACIENTE,
                            PAC_CODIGO = acceso.PAC_CODIGO,
                            DAP_CODIGO = acceso.DAP_CODIGO,
                            PAC_APELLIDO_PATERNO = acceso.PAC_APELLIDO_PATERNO,
                            PAC_APELLIDO_MATERNO = acceso.PAC_APELLIDO_MATERNO,
                            PAC_NOMBRE = acceso.PAC_NOMBRE1,
                            PAC_NOMBRE2 = acceso.PAC_NOMBRE2,
                            PAC_HCL = acceso.PAC_HISTORIA_CLINICA,
                            HAB_CODIGO = acceso.hab_Codigo,
                            HAB_NUMERO = acceso.hab_Numero,
                            ATE_FECHA = Convert.ToDateTime(acceso.ATE_FECHA),
                            ATE_FECHA_INGRESO = Convert.ToDateTime(acceso.ATE_FECHA_INGRESO),
                            ATE_ESTADO = Convert.ToBoolean(acceso.ATE_ESTADO),
                            ATE_REFERIDO = Convert.ToBoolean(acceso.ATE_REFERIDO),
                            CAJ_CODIGO = acceso.CAJ_CODIGO
                        });
                    }

                }
                #endregion

                #region PACIENTE
                else if (criterio == 3)
                {
                    var atenciones = from t in contexto.ATENCIONES
                                     join p in contexto.PACIENTES on t.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                     join h in contexto.HABITACIONES on t.HABITACIONES.hab_Codigo equals h.hab_Codigo
                                     join a in contexto.PACIENTES_DATOS_ADICIONALES on t.PACIENTES_DATOS_ADICIONALES.DAP_CODIGO equals a.DAP_CODIGO
                                     join c in contexto.CAJAS on t.CAJAS.CAJ_CODIGO equals c.CAJ_CODIGO
                                     join u in contexto.USUARIOS on t.USUARIOS.ID_USUARIO equals u.ID_USUARIO
                                     where a.DAP_ESTADO == true
                                     && t.ATE_FECHA_ALTA == null
                                     && p.PAC_APELLIDO_PATERNO.Contains(buscar)
                                     orderby t.ATE_FECHA
                                     select new
                                     {
                                         t.ATE_CODIGO,
                                         t.ATE_NUMERO_CONTROL,
                                         t.ATE_NUMERO_ATENCION,
                                         t.ATE_FACTURA_PACIENTE,
                                         t.PACIENTES.PAC_CODIGO,
                                         t.HABITACIONES.hab_Codigo,
                                         p.PAC_APELLIDO_PATERNO,
                                         p.PAC_APELLIDO_MATERNO,
                                         p.PAC_NOMBRE1,
                                         p.PAC_NOMBRE2,
                                         p.PAC_HISTORIA_CLINICA,
                                         h.hab_Numero,
                                         t.PACIENTES_DATOS_ADICIONALES.DAP_CODIGO,
                                         t.ATE_FACTURA_FECHA,
                                         t.ATE_FECHA,
                                         t.ATE_FECHA_INGRESO,
                                         t.ATE_FECHA_ALTA,
                                         t.ATE_ESTADO,
                                         t.ATE_REFERIDO,
                                         c.CAJ_CODIGO
                                     };
                    atenciones = atenciones.Take(cantidad);
                    foreach (var acceso in atenciones)
                    {
                        atenciongrid.Add(new DtoAtenciones()
                        {

                            ATE_CODIGO = acceso.ATE_CODIGO,
                            ATE_NUMERO_CONTROL = acceso.ATE_NUMERO_CONTROL,
                            ATE_NUMERO_ATENCION = acceso.ATE_NUMERO_ATENCION,
                            ATE_FACTURA_PACIENTE = acceso.ATE_FACTURA_PACIENTE,
                            PAC_CODIGO = acceso.PAC_CODIGO,
                            DAP_CODIGO = acceso.DAP_CODIGO,
                            PAC_APELLIDO_PATERNO = acceso.PAC_APELLIDO_PATERNO,
                            PAC_APELLIDO_MATERNO = acceso.PAC_APELLIDO_MATERNO,
                            PAC_NOMBRE = acceso.PAC_NOMBRE1,
                            PAC_NOMBRE2 = acceso.PAC_NOMBRE2,
                            PAC_HCL = acceso.PAC_HISTORIA_CLINICA,
                            HAB_CODIGO = acceso.hab_Codigo,
                            HAB_NUMERO = acceso.hab_Numero,
                            ATE_FECHA = Convert.ToDateTime(acceso.ATE_FECHA),
                            ATE_FECHA_INGRESO = Convert.ToDateTime(acceso.ATE_FECHA_INGRESO),
                            ATE_ESTADO = Convert.ToBoolean(acceso.ATE_ESTADO),
                            ATE_REFERIDO = Convert.ToBoolean(acceso.ATE_REFERIDO),
                            CAJ_CODIGO = acceso.CAJ_CODIGO
                        });
                    }
                }
                #endregion

                #region HABITACION
                else if (criterio == 4)
                {
                    var atenciones = from t in contexto.ATENCIONES
                                     join p in contexto.PACIENTES on t.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                     join h in contexto.HABITACIONES on t.HABITACIONES.hab_Codigo equals h.hab_Codigo
                                     join a in contexto.PACIENTES_DATOS_ADICIONALES on t.PACIENTES_DATOS_ADICIONALES.DAP_CODIGO equals a.DAP_CODIGO
                                     join c in contexto.CAJAS on t.CAJAS.CAJ_CODIGO equals c.CAJ_CODIGO
                                     join u in contexto.USUARIOS on t.USUARIOS.ID_USUARIO equals u.ID_USUARIO
                                     where a.DAP_ESTADO == true
                                     && t.ATE_FECHA_ALTA == null
                                     && h.hab_Numero == buscar
                                     orderby t.ATE_FECHA
                                     select new
                                     {
                                         t.ATE_CODIGO,
                                         t.ATE_NUMERO_CONTROL,
                                         t.ATE_NUMERO_ATENCION,
                                         t.ATE_FACTURA_PACIENTE,
                                         t.PACIENTES.PAC_CODIGO,
                                         t.HABITACIONES.hab_Codigo,
                                         p.PAC_APELLIDO_PATERNO,
                                         p.PAC_APELLIDO_MATERNO,
                                         p.PAC_NOMBRE1,
                                         p.PAC_NOMBRE2,
                                         p.PAC_HISTORIA_CLINICA,
                                         h.hab_Numero,
                                         t.PACIENTES_DATOS_ADICIONALES.DAP_CODIGO,
                                         t.ATE_FACTURA_FECHA,
                                         t.ATE_FECHA,
                                         t.ATE_FECHA_INGRESO,
                                         t.ATE_FECHA_ALTA,
                                         t.ATE_ESTADO,
                                         t.ATE_REFERIDO,
                                         c.CAJ_CODIGO
                                     };
                    atenciones = atenciones.Take(cantidad);
                    foreach (var acceso in atenciones)
                    {
                        atenciongrid.Add(new DtoAtenciones()
                        {

                            ATE_CODIGO = acceso.ATE_CODIGO,
                            ATE_NUMERO_CONTROL = acceso.ATE_NUMERO_CONTROL,
                            ATE_NUMERO_ATENCION = acceso.ATE_NUMERO_ATENCION,
                            ATE_FACTURA_PACIENTE = acceso.ATE_FACTURA_PACIENTE,
                            PAC_CODIGO = acceso.PAC_CODIGO,
                            DAP_CODIGO = acceso.DAP_CODIGO,
                            PAC_APELLIDO_PATERNO = acceso.PAC_APELLIDO_PATERNO,
                            PAC_APELLIDO_MATERNO = acceso.PAC_APELLIDO_MATERNO,
                            PAC_NOMBRE = acceso.PAC_NOMBRE1,
                            PAC_NOMBRE2 = acceso.PAC_NOMBRE2,
                            PAC_HCL = acceso.PAC_HISTORIA_CLINICA,
                            HAB_CODIGO = acceso.hab_Codigo,
                            HAB_NUMERO = acceso.hab_Numero,
                            ATE_FECHA = Convert.ToDateTime(acceso.ATE_FECHA),
                            ATE_FECHA_INGRESO = Convert.ToDateTime(acceso.ATE_FECHA_INGRESO),
                            ATE_ESTADO = Convert.ToBoolean(acceso.ATE_ESTADO),
                            ATE_REFERIDO = Convert.ToBoolean(acceso.ATE_REFERIDO),
                            CAJ_CODIGO = acceso.CAJ_CODIGO
                        });
                    }
                }
                #endregion

                return atenciongrid;
            }
        }

        public List<DtoAtenciones> RecuperaAtencionesPaciente(int keyPaciente)
        {
            List<DtoAtenciones> atenciongrid = new List<DtoAtenciones>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                var atenciones = (from t in contexto.ATENCIONES
                                  join p in contexto.PACIENTES on t.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                  join h in contexto.HABITACIONES on t.HABITACIONES.hab_Codigo equals h.hab_Codigo
                                  join a in contexto.PACIENTES_DATOS_ADICIONALES on t.PACIENTES_DATOS_ADICIONALES.DAP_CODIGO equals a.DAP_CODIGO
                                  where p.PAC_CODIGO == keyPaciente
                                  orderby t.ATE_FECHA
                                  select new
                                  {
                                      t.ATE_NUMERO_ATENCION,
                                      t.ATE_CODIGO,
                                      t.ATE_NUMERO_CONTROL,
                                      t.ATE_FACTURA_PACIENTE,
                                      p.PAC_CODIGO,
                                      h.hab_Codigo,
                                      p.PAC_APELLIDO_PATERNO,
                                      p.PAC_APELLIDO_MATERNO,
                                      p.PAC_NOMBRE1,
                                      p.PAC_NOMBRE2,
                                      p.PAC_HISTORIA_CLINICA,
                                      p.PAC_TIPO_IDENTIFICACION,
                                      p.PAC_IDENTIFICACION,
                                      a.DAP_DIRECCION_DOMICILIO,
                                      a.DAP_TELEFONO1,
                                      h.hab_Numero,
                                      t.ATE_FACTURA_FECHA,
                                      t.ATE_FECHA,
                                      t.ATE_FECHA_INGRESO,
                                      t.ATE_FECHA_ALTA,
                                      t.ATE_ESTADO,
                                      t.ATE_REFERIDO,
                                      t.ATE_DIAGNOSTICO_INICIAL,
                                  }).ToList();

                if (atenciones != null)
                {
                    foreach (var acceso in atenciones)
                    {
                        atenciongrid.Add(new DtoAtenciones()
                        {
                            ATE_NUMERO_ATENCION = acceso.ATE_NUMERO_ATENCION,
                            ATE_CODIGO = acceso.ATE_CODIGO,
                            ATE_NUMERO_CONTROL = acceso.ATE_NUMERO_CONTROL,
                            ATE_FACTURA_PACIENTE = acceso.ATE_FACTURA_PACIENTE,
                            PAC_CODIGO = acceso.PAC_CODIGO,
                            PAC_APELLIDO_PATERNO = acceso.PAC_APELLIDO_PATERNO,
                            PAC_APELLIDO_MATERNO = acceso.PAC_APELLIDO_MATERNO,
                            PAC_NOMBRE = acceso.PAC_NOMBRE1,
                            PAC_NOMBRE2 = acceso.PAC_NOMBRE2,
                            PAC_HCL = acceso.PAC_HISTORIA_CLINICA,
                            PAC_CEDULA = acceso.PAC_IDENTIFICACION,
                            PAC_DIRECCION = acceso.DAP_DIRECCION_DOMICILIO,
                            PAC_TELEFONO = acceso.DAP_TELEFONO1,
                            //HAB_CODIGO = acceso.HAB_CODIGO.Value,
                            HAB_CODIGO = acceso.hab_Codigo,
                            HAB_NUMERO = acceso.hab_Numero,
                            ATE_FACTURA_FECHA = Convert.ToDateTime(acceso.ATE_FACTURA_FECHA),
                            ATE_FECHA = Convert.ToDateTime(acceso.ATE_FECHA),
                            ATE_FECHA_INGRESO = Convert.ToDateTime(acceso.ATE_FECHA_INGRESO),
                            ATE_FECHA_ALTA = Convert.ToDateTime(acceso.ATE_FECHA_ALTA),
                            ATE_ESTADO = Convert.ToBoolean(acceso.ATE_ESTADO),
                            ATE_REFERIDO = Convert.ToBoolean(acceso.ATE_REFERIDO),
                            ATE_DIAGNOSTICOINICIAL = Convert.ToString(acceso.ATE_DIAGNOSTICO_INICIAL),
                        });
                    }
                }
                else
                {
                    atenciongrid = null;
                }
                return atenciongrid;
            }
        }

        public List<DtoAtenciones> RecuperaAtencionesPaciente(int keyPaciente, Int16 codigoCaja, Int16 codUsuario)
        {
            try
            {
                List<DtoAtenciones> atenciongrid = new List<DtoAtenciones>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    atenciongrid = (from t in contexto.ATENCIONES
                                    join p in contexto.PACIENTES on t.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                    join h in contexto.HABITACIONES on t.HABITACIONES.hab_Codigo equals h.hab_Codigo
                                    join a in contexto.PACIENTES_DATOS_ADICIONALES on t.PACIENTES_DATOS_ADICIONALES.DAP_CODIGO equals a.DAP_CODIGO
                                    where t.PACIENTES.PAC_CODIGO == keyPaciente && t.CAJAS.CAJ_CODIGO == codigoCaja && t.USUARIOS.ID_USUARIO == codUsuario
                                    orderby t.ATE_FECHA
                                    select new DtoAtenciones()
                                    {
                                        ATE_NUMERO_ATENCION = t.ATE_NUMERO_ATENCION,
                                        ATE_CODIGO = t.ATE_CODIGO,
                                        ATE_NUMERO_CONTROL = t.ATE_NUMERO_CONTROL,
                                        ATE_FACTURA_PACIENTE = t.ATE_FACTURA_PACIENTE,
                                        PAC_CODIGO = t.PACIENTES.PAC_CODIGO,
                                        PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                        PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                        PAC_NOMBRE = p.PAC_NOMBRE1,
                                        PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                        PAC_HCL = p.PAC_HISTORIA_CLINICA,
                                        PAC_CEDULA = p.PAC_IDENTIFICACION,
                                        PAC_DIRECCION = a.DAP_DIRECCION_DOMICILIO,
                                        PAC_TELEFONO = a.DAP_TELEFONO1,
                                        HAB_CODIGO = h.hab_Codigo,
                                        HAB_NUMERO = h.hab_Numero,
                                        ATE_FACTURA_FECHA = t.ATE_FACTURA_FECHA.Value,
                                        ATE_FECHA = t.ATE_FECHA.Value,
                                        ATE_FECHA_INGRESO = t.ATE_FECHA_INGRESO.Value,
                                        ATE_FECHA_ALTA = t.ATE_FECHA_ALTA.Value,
                                        ATE_ESTADO = t.ATE_ESTADO.Value,
                                        ATE_REFERIDO = t.ATE_REFERIDO.Value
                                    }).ToList();
                    return atenciongrid;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public ATENCIONES RecuperarUltimaAtencion(int keyPaciente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in contexto.ATENCIONES
                        join p in contexto.PACIENTES on t.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                        where p.PAC_CODIGO == keyPaciente && t.ESC_CODIGO != 6
                        orderby t.ATE_CODIGO descending
                        select t).FirstOrDefault();
            }
        }

        public ATENCIONES RecuperarUltimaAtencionHonorarios(Int32 ateCodigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in contexto.ATENCIONES
                        join p in contexto.PACIENTES on t.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                        where t.ATE_CODIGO == ateCodigo && t.ESC_CODIGO != 6
                        select t).FirstOrDefault();
            }
        }


        /// <summary>
        /// Metodo que recupera una atencion enviando como parametro el codigo del paciente
        /// </summary>
        /// <param name="keyPaciente">codigo del paciente</param>
        /// <returns>retorna un objeto ATENCIONES incluidos ATENCION_FORMAS_LLEGADA,MEDICOS</returns>
        public ATENCIONES RecuperarUltimaAtencionExt(int keyPaciente)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.ATENCIONES.Include("ATENCION_FORMAS_LLEGADA").Include("MEDICOS").Include("USUARIOS").Where(a => a.PACIENTES.PAC_CODIGO == keyPaciente).First();
                }
            }
            catch (Exception err) { throw err; }
        }

        public ATENCIONES RecuperarUltimaAtencionEmergencia(int keyPaciente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                return (from t in contexto.ATENCIONES
                            //join h in contexto.HABITACIONES on t.HABITACIONES.hab_Codigo equals h.hab_Codigo
                            //join i in contexto.TIPO_TRATAMIENTO on t.TIPO_TRATAMIENTO.TIA_CODIGO equals i.TIA_CODIGO
                        join m in contexto.ATENCION_FORMAS_LLEGADA on t.ATENCION_FORMAS_LLEGADA.AFL_CODIGO equals m.AFL_CODIGO
                        join p in contexto.PACIENTES on t.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                        join a in contexto.PACIENTES_DATOS_ADICIONALES on t.PACIENTES_DATOS_ADICIONALES.DAP_CODIGO equals a.DAP_CODIGO
                        //join r in contexto.TIPO_REFERIDO on t.TIPO_REFERIDO.TIR_CODIGO equals r.TIR_CODIGO
                        join ti in contexto.TIPO_INGRESO on t.TIPO_INGRESO.TIP_CODIGO equals ti.TIP_CODIGO
                        join e in contexto.MEDICOS on t.MEDICOS.MED_CODIGO equals e.MED_CODIGO
                        where t.ATE_ESTADO == true && a.PACIENTES.PAC_CODIGO == keyPaciente
                        orderby t.ATE_FECHA descending
                        select t).FirstOrDefault();
            }
        }

        public ATENCIONES RecuperarAtencionPorNumero(string numAtencion)
        {
            Int64 ate = Convert.ToInt64(numAtencion);
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                return (from t in contexto.ATENCIONES
                        join h in contexto.HABITACIONES on t.HABITACIONES.hab_Codigo equals h.hab_Codigo
                        join i in contexto.TIPO_TRATAMIENTO on t.TIPO_TRATAMIENTO.TIA_CODIGO equals i.TIA_CODIGO
                        join m in contexto.ATENCION_FORMAS_LLEGADA on t.ATENCION_FORMAS_LLEGADA.AFL_CODIGO equals m.AFL_CODIGO
                        join p in contexto.PACIENTES on t.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                        join a in contexto.PACIENTES_DATOS_ADICIONALES on t.PACIENTES_DATOS_ADICIONALES.DAP_CODIGO equals a.DAP_CODIGO
                        join r in contexto.TIPO_REFERIDO on t.TIPO_REFERIDO.TIR_CODIGO equals r.TIR_CODIGO
                        join ti in contexto.TIPO_INGRESO on t.TIPO_INGRESO.TIP_CODIGO equals ti.TIP_CODIGO
                        join e in contexto.MEDICOS on t.MEDICOS.MED_CODIGO equals e.MED_CODIGO
                        where t.ATE_CODIGO == ate
                        orderby t.ATE_FECHA
                        select t).FirstOrDefault();
            }
        }
        public ATENCIONES RecuepraAtencionNumeroAtencion(Int64 ateNumeroAtencion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from a in contexto.ATENCIONES
                        where a.ATE_CODIGO == ateNumeroAtencion
                        select a).FirstOrDefault();
            }
        }
        public ATENCIONES RecuepraAtencionNumeroAtencion2(string ateNumeroAtencion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from a in contexto.ATENCIONES
                        where a.ATE_NUMERO_ATENCION == ateNumeroAtencion
                        select a).FirstOrDefault();
            }
        }
        public TIPO_INGRESO RecuperaTipoIngreso(string ateNumeroAtencion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from a in contexto.ATENCIONES
                        join t in contexto.TIPO_INGRESO on a.TIPO_INGRESO.TIP_CODIGO equals t.TIP_CODIGO
                        where a.ATE_NUMERO_ATENCION == ateNumeroAtencion
                        select t).FirstOrDefault();
            }
        }
        //recupera notas de evolucion Pablo Rocha 30-05-2020
        public DataTable RecuperaNotasEvolucion()
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
            Sqlcmd = new SqlCommand("sp_RecuperaNostasEvolucion", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            { }

            return Dts;
        }

        public bool ValidarAseguradora(Int16 cat_codigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            bool ok = false;
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("sp_ValidarAseguradora", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.AddWithValue("@cat_codigo", cat_codigo);
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            SqlDataReader reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                short valor = reader.GetInt16(0);
                if (valor == 1)
                {
                    ok = true;
                }
                else
                    ok = false;
            }

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return ok;
        }
        public DataTable RecuperaMedicosEvolucion(int evo_codigo)
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
            Sqlcmd = new SqlCommand("sp_RecuperaMedicosEvolucion", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@EVO_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@EVO_CODIGO"].Value = evo_codigo;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            { }

            return Dts;
        }

        public DataTable NumeroAtencion(Int64 pacCod)
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
            Sqlcmd = new SqlCommand("sp_NumeroAtencion", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@EVO_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@EVO_CODIGO"].Value = pacCod;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            { }

            return Dts;
        }

        public void CrearAtencion(ATENCIONES atencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    ATENCIONES ate = (from a in contexto.ATENCIONES
                                      where a.ATE_NUMERO_ATENCION == atencion.ATE_NUMERO_ATENCION
                                      select a).FirstOrDefault();
                    if (ate == null)
                    {
                        contexto.Crear("ATENCIONES", atencion);
                    }
                    else
                    {
                        Int64 ATE_NUMERO_CONTROL = RecuperaMaximoPacienteNumeroAtencion() + 1;
                        atencion.ATE_NUMERO_ATENCION = Convert.ToString(ATE_NUMERO_CONTROL);
                        contexto.Crear("ATENCIONES", atencion);
                    }
                }
            }
            catch (Exception EX)
            {

                throw;
            }
        }

        public Int64 RecuperaMaximoPacienteNumeroAtencion()
        {


            Int64 r;
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

            Sqlcmd = new SqlCommand("SELECT  MAX(CAST(ATE_NUMERO_ATENCION AS BIGINT) ) + 1 from ATENCIONES", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            //Mario 202.09.16 Cambio poe error en admision emergencia;
            r = Convert.ToInt64(Dts.Rows[0][0]);
            return r;
            //Int64 maxim;
            //using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            //{
            //    List<ATENCIONES> paciente = new List<ATENCIONES>();
            //    paciente = contexto.ATENCIONES.ToList();
            //    if (paciente.Count > 0)
            //    {
            //        maxim = contexto.ATENCIONES.Max(loc => Int64.Parse(loc.ATE_NUMERO_ATENCION)) + 1;
            //        if (maxim < 1000)
            //            maxim = 1000;
            //    }
            //    else
            //        maxim = 0;
            //    return maxim;
            //}

        }

        public DataTable CrearAtencionSP(ATENCIONES atencion, int CodigoDatosAdicionales, Boolean Nuevo, PACIENTES_DATOS_ADICIONALES PDA)
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

            Sqlcmd = new SqlCommand("sp_DatosAtencionSimplificada", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;


            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = atencion.ATE_CODIGO;

            Sqlcmd.Parameters.Add("@ATE_NUMERO_ATENCION", SqlDbType.NChar);
            Sqlcmd.Parameters["@ATE_NUMERO_ATENCION"].Value = atencion.ATE_NUMERO_ATENCION;

            Sqlcmd.Parameters.Add("@ATE_FECHA", SqlDbType.DateTime);
            Sqlcmd.Parameters["@ATE_FECHA"].Value = atencion.ATE_FECHA;

            Sqlcmd.Parameters.Add("@ATE_NUMERO_CONTROL", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_NUMERO_CONTROL"].Value = atencion.ATE_NUMERO_CONTROL;

            Sqlcmd.Parameters.Add("@ATE_FACTURA_PACIENTE", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_FACTURA_PACIENTE"].Value = atencion.ATE_FACTURA_PACIENTE;

            Sqlcmd.Parameters.Add("@ATE_FACTURA_FECHA", SqlDbType.Date);
            Sqlcmd.Parameters["@ATE_FACTURA_FECHA"].Value = atencion.ATE_FACTURA_FECHA;

            Sqlcmd.Parameters.Add("@ATE_FECHA_INGRESO", SqlDbType.DateTime);
            Sqlcmd.Parameters["@ATE_FECHA_INGRESO"].Value = atencion.ATE_FECHA_INGRESO;

            Sqlcmd.Parameters.Add("@ATE_FECHA_ALTA", SqlDbType.DateTime);
            Sqlcmd.Parameters["@ATE_FECHA_ALTA"].Value = atencion.ATE_FECHA_ALTA;

            Sqlcmd.Parameters.Add("@ATE_REFERIDO", SqlDbType.Bit);
            Sqlcmd.Parameters["@ATE_REFERIDO"].Value = 1;

            Sqlcmd.Parameters.Add("@ATE_REFERIDO_DE", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_REFERIDO_DE"].Value = atencion.ATE_REFERIDO_DE;

            Sqlcmd.Parameters.Add("@ATE_EDAD_PACIENTE", SqlDbType.SmallInt);
            Sqlcmd.Parameters["@ATE_EDAD_PACIENTE"].Value = atencion.ATE_EDAD_PACIENTE;

            Sqlcmd.Parameters.Add("@ATE_ACOMPANANTE_NOMBRE", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_ACOMPANANTE_NOMBRE"].Value = atencion.ATE_ACOMPANANTE_NOMBRE;

            Sqlcmd.Parameters.Add("@ATE_ACOMPANANTE_CEDULA", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_ACOMPANANTE_CEDULA"].Value = atencion.ATE_ACOMPANANTE_CEDULA;

            Sqlcmd.Parameters.Add("@ATE_ACOMPANANTE_PARENTESCO", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_ACOMPANANTE_PARENTESCO"].Value = atencion.ATE_ACOMPANANTE_PARENTESCO;

            Sqlcmd.Parameters.Add("@ATE_ACOMPANANTE_TELEFONO", SqlDbType.NChar);
            Sqlcmd.Parameters["@ATE_ACOMPANANTE_TELEFONO"].Value = "S/T";

            Sqlcmd.Parameters.Add("@ATE_ACOMPANANTE_DIRECCION", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_ACOMPANANTE_DIRECCION"].Value = "S/D";

            Sqlcmd.Parameters.Add("@ATE_ACOMPANANTE_CIUDAD", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_ACOMPANANTE_CIUDAD"].Value = atencion.ATE_ACOMPANANTE_CIUDAD;

            Sqlcmd.Parameters.Add("@ATE_GARANTE_NOMBRE", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_GARANTE_NOMBRE"].Value = atencion.ATE_GARANTE_NOMBRE;

            Sqlcmd.Parameters.Add("@ATE_GARANTE_CEDULA", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_GARANTE_CEDULA"].Value = atencion.ATE_GARANTE_CEDULA;

            Sqlcmd.Parameters.Add("@ATE_GARANTE_PARENTESCO", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_GARANTE_PARENTESCO"].Value = atencion.ATE_GARANTE_PARENTESCO;

            Sqlcmd.Parameters.Add("@ATE_GARANTE_MONTO_GARANTIA", SqlDbType.Decimal);
            Sqlcmd.Parameters["@ATE_GARANTE_MONTO_GARANTIA"].Value = atencion.ATE_GARANTE_MONTO_GARANTIA;

            Sqlcmd.Parameters.Add("@ATE_GARANTE_TELEFONO", SqlDbType.NChar);
            Sqlcmd.Parameters["@ATE_GARANTE_TELEFONO"].Value = atencion.ATE_GARANTE_TELEFONO;

            Sqlcmd.Parameters.Add("@ATE_GARANTE_DIRECCION", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_GARANTE_DIRECCION"].Value = atencion.ATE_GARANTE_DIRECCION;

            Sqlcmd.Parameters.Add("@ATE_GARANTE_CIUDAD", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_GARANTE_CIUDAD"].Value = atencion.ATE_GARANTE_CIUDAD;

            Sqlcmd.Parameters.Add("@ATE_DIAGNOSTICO_INICIAL", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_DIAGNOSTICO_INICIAL"].Value = atencion.ATE_DIAGNOSTICO_INICIAL;

            Sqlcmd.Parameters.Add("@ATE_DIAGNOSTICO_FINAL", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_DIAGNOSTICO_FINAL"].Value = atencion.ATE_DIAGNOSTICO_FINAL;

            Sqlcmd.Parameters.Add("@ATE_OBSERVACIONES", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_OBSERVACIONES"].Value = atencion.ATE_OBSERVACIONES;

            Sqlcmd.Parameters.Add("@ATE_ESTADO", SqlDbType.Bit);
            Sqlcmd.Parameters["@ATE_ESTADO"].Value = 1;

            Sqlcmd.Parameters.Add("@ATE_FACTURA_NOMBRE", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_FACTURA_NOMBRE"].Value = atencion.ATE_FACTURA_NOMBRE;

            Sqlcmd.Parameters.Add("@ATE_DIRECTORIO", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_DIRECTORIO"].Value = atencion.ATE_DIRECTORIO;

            Sqlcmd.Parameters.Add("@PAC_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@PAC_CODIGO"].Value = atencion.PACIENTESReference.EntityKey.EntityKeyValues[0].Value;

            Sqlcmd.Parameters.Add("@DAP_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@DAP_CODIGO"].Value = CodigoDatosAdicionales;

            Sqlcmd.Parameters.Add("@HAB_CODIGO", SqlDbType.SmallInt);
            //Sqlcmd.Parameters["@HAB_CODIGO"].Value = atencion.HABITACIONESReference.EntityKey.EntityKeyValues[0].Value; ;//habitacion
            Sqlcmd.Parameters["@HAB_CODIGO"].Value = 0;//habitacion

            Sqlcmd.Parameters.Add("@CAJ_CODIGO", SqlDbType.SmallInt);
            Sqlcmd.Parameters["@CAJ_CODIGO"].Value = 0;

            Sqlcmd.Parameters.Add("@TIA_CODIGO", SqlDbType.SmallInt);
            Sqlcmd.Parameters["@TIA_CODIGO"].Value = 0;

            Sqlcmd.Parameters.Add("@ID_USUSARIO", SqlDbType.SmallInt);
            Sqlcmd.Parameters["@ID_USUSARIO"].Value = atencion.USUARIOSReference.EntityKey.EntityKeyValues[0].Value;

            Sqlcmd.Parameters.Add("@TIR_CODIGO", SqlDbType.SmallInt);
            Sqlcmd.Parameters["@TIR_CODIGO"].Value = 1;

            Sqlcmd.Parameters.Add("@AFL_CODIGO", SqlDbType.SmallInt);
            Sqlcmd.Parameters["@AFL_CODIGO"].Value = 0;

            Sqlcmd.Parameters.Add("@MED_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@MED_CODIGO"].Value = 0;

            Sqlcmd.Parameters.Add("@TIP_CODIGO", SqlDbType.SmallInt);
            Sqlcmd.Parameters["@TIP_CODIGO"].Value = atencion.TIPO_INGRESOReference.EntityKey.EntityKeyValues[0].Value;
            Sqlcmd.Parameters.Add("@DIRECCION2", SqlDbType.VarChar);
            Sqlcmd.Parameters["@DIRECCION2"].Value = PDA.DAP_DIRECCION_DOMICILIO2;
            Sqlcmd.Parameters.Add("@REFERENCIA", SqlDbType.VarChar);
            Sqlcmd.Parameters["@REFERENCIA"].Value = PDA.DAP_REFERENCIA;
            Sqlcmd.Parameters.Add("@TIF_CODIGO", SqlDbType.SmallInt);
            Sqlcmd.Parameters["@TIF_CODIGO"].Value = 1;

            Sqlcmd.Parameters.Add("@TIF_OBSERVACION", SqlDbType.VarChar);
            Sqlcmd.Parameters["@TIF_OBSERVACION"].Value = atencion.TIF_OBSERVACION;

            Sqlcmd.Parameters.Add("@ATE_NUMERO_ADMISION", SqlDbType.Int);
            Sqlcmd.Parameters["@ATE_NUMERO_ADMISION"].Value = atencion.ATE_NUMERO_ADMISION;

            Sqlcmd.Parameters.Add("@ATE_EN_QUIROFANO", SqlDbType.Bit);
            Sqlcmd.Parameters["@ATE_EN_QUIROFANO"].Value = 0;

            Sqlcmd.Parameters.Add("@FOR_PAGO", SqlDbType.Int);
            Sqlcmd.Parameters["@FOR_PAGO"].Value = 0;

            Sqlcmd.Parameters.Add("@ATE_QUIEN_ENTREGA_PAC", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_QUIEN_ENTREGA_PAC"].Value = atencion.ATE_QUIEN_ENTREGA_PAC;

            Sqlcmd.Parameters.Add("@ATE_CIERRE_HC", SqlDbType.Bit);
            Sqlcmd.Parameters["@ATE_CIERRE_HC"].Value = atencion.ATE_CIERRE_HC;

            Sqlcmd.Parameters.Add("@ATE_FEC_ING_HABITACION", SqlDbType.DateTime);
            Sqlcmd.Parameters["@ATE_FEC_ING_HABITACION"].Value = Convert.ToDateTime(DateTime.Now.ToString());

            Sqlcmd.Parameters.Add("@ESC_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@ESC_CODIGO"].Value = atencion.ESC_CODIGO;

            Sqlcmd.Parameters.Add("@CUE_ESTADO", SqlDbType.VarChar);
            Sqlcmd.Parameters["@CUE_ESTADO"].Value = 1;

            Sqlcmd.Parameters.Add("@TipoAtencion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@TipoAtencion"].Value = atencion.TipoAtencion;

            Sqlcmd.Parameters.Add("@DireccionPaciente", SqlDbType.VarChar);
            Sqlcmd.Parameters["@DireccionPaciente"].Value = atencion.ATE_ACOMPANANTE_DIRECCION;

            Sqlcmd.Parameters.Add("@TelefonoPaciente", SqlDbType.VarChar);
            Sqlcmd.Parameters["@TelefonoPaciente"].Value = atencion.ATE_ACOMPANANTE_TELEFONO;

            Sqlcmd.Parameters.Add("@CelularPaciente", SqlDbType.VarChar);
            Sqlcmd.Parameters["@CelularPaciente"].Value = atencion.ATE_GARANTE_TELEFONO;

            Sqlcmd.Parameters.Add("@Nuevo", SqlDbType.Bit);

            if (Nuevo == true)
            {
                Sqlcmd.Parameters["@Nuevo"].Value = 1;
            }
            else
            {
                Sqlcmd.Parameters["@Nuevo"].Value = 0;
            }


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];
        }

        public void GrabarAtencion(ATENCIONES atencionModificada, ATENCIONES atencionOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                contexto.Grabar(atencionModificada, atencionOriginal);
            }
        }

        public void ActualizarAtencion(ATENCIONES atencionModificada)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    //contexto.ApplyPropertyChanges("ATENCIONES", atencionModificada);
                    contexto.AttachUpdated(atencionModificada);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err) { throw err; }
        }
        public bool GuardaAtencionSubsecuente(ATENCIONES_SUBSECUENTES atencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    ATENCIONES_SUBSECUENTES ate = contexto.ATENCIONES_SUBSECUENTES.FirstOrDefault(a => a.ate_codigo_subsecuente == atencion.ate_codigo_subsecuente);
                    if (ate != null)
                    {
                        ate.ate_codigo_principal = atencion.ate_codigo_principal;
                        contexto.SaveChanges();
                        return true;
                    }
                    else
                    {
                        contexto.AddToATENCIONES_SUBSECUENTES(atencion);
                        contexto.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception err) { throw err; }
        }
        public bool EditarAtencionSubsecuente(ATENCIONES_SUBSECUENTES atencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    ATENCIONES_SUBSECUENTES ate = contexto.ATENCIONES_SUBSECUENTES.FirstOrDefault(a => a.ate_codigo_subsecuente == atencion.ate_codigo_subsecuente);
                    ate.ate_codigo_principal = atencion.ate_codigo_principal;
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception err) { throw err; }
        }
        public bool ActualizarUAtencion(ATENCIONES obj)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    ATENCIONES ate = contexto.ATENCIONES.FirstOrDefault(a => a.ATE_CODIGO == obj.ATE_CODIGO);
                    ate.ATE_FACTURA_NOMBRE = obj.ATE_FACTURA_NOMBRE;
                    ate.ATE_FACTURA_FECHA = obj.ATE_FACTURA_FECHA;
                    ate.ATE_ESTADO = obj.ATE_ESTADO;
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }
        }
        public bool AutidoriaTipoIngreso(AUDITORIA_TIPO_INGRESO obj2, Int16 tipoIngreso)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.Crear("AUDITORIA_TIPO_INGRESO", obj2);

                    SqlCommand command;
                    SqlConnection connection;
                    SqlDataReader reader;
                    BaseContextoDatos obj = new BaseContextoDatos();
                    connection = obj.ConectarBd();
                    connection.Open();
                    command = new SqlCommand("update atenciones set tip_codigo=" + tipoIngreso + " where ate_codigo =" + obj2.ate_codigo + "", connection);

                    reader = command.ExecuteReader();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }
        }
        public void EliminarAtencion(ATENCIONES atencion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(atencion);
            }
        }

        public List<ATENCIONES> listaAtenciones()
        {

            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ATENCIONES> atenciones = new List<ATENCIONES>();
                //atenciones = contexto.ATENCIONES.Include("PACIENTES").Include("HABITACIONES").Include("CAJAS").OrderBy(p => p.ATE_FECHA).ToList();
                atenciones = (from a in contexto.ATENCIONES
                              join p in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                              join h in contexto.HABITACIONES on a.HABITACIONES.hab_Codigo equals h.hab_Codigo
                              join c in contexto.CAJAS on a.CAJAS.CAJ_CODIGO equals c.CAJ_CODIGO
                              orderby a.ATE_FECHA
                              select a).ToList();
                return atenciones;


            }

        }


        public DataTable RecuperaReferidoDiagnostico(int atencion)
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
            Sqlcmd = new SqlCommand("sp_RecuperaReferidoDiagnostico", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;


            Sqlcmd.Parameters.Add("@atencion", SqlDbType.Int);
            Sqlcmd.Parameters["@atencion"].Value = atencion;

            Sqldap = new SqlDataAdapter();
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
        }

        public DataTable RecuperaFechaNacimiento(string hc)
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
            Sqlcmd = new SqlCommand("sp_RecuperaFechaNacimiento", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;


            Sqlcmd.Parameters.Add("@hc", SqlDbType.VarChar);
            Sqlcmd.Parameters["@hc"].Value = hc;

            Sqldap = new SqlDataAdapter();
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
        }

        public DataTable RecuperaEmpleadoSC(string cedula)
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
            Sqlcmd = new SqlCommand("sp_RecuperaEmpleadoSC", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;


            Sqlcmd.Parameters.Add("@cedula", SqlDbType.VarChar);
            Sqlcmd.Parameters["@cedula"].Value = cedula;

            Sqldap = new SqlDataAdapter();
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
        }

        public ATENCIONES RecuperarAtencionID(Int64 codigo)
        {

            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.ATENCIONES.Include("PACIENTES").Include("PACIENTES_DATOS_ADICIONALES").Include("MEDICOS").Include("HABITACIONES").Include("CAJAS").FirstOrDefault(a => a.ATE_CODIGO == codigo);

            }

        }
        public string Estado_Cuenta(Int64 ate_codigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
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
            Sqlcmd = new SqlCommand("sp_EstadoAtencion", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            Sqlcmd.CommandTimeout = 180;
            SqlDataReader reader = Sqlcmd.ExecuteReader();
            string estado = null;
            reader.Read();
            if (!reader.IsDBNull(reader.GetOrdinal("ESC_CODIGO")))
            {
                estado = Convert.ToString(reader.GetInt32(0));
                Sqlcmd.Parameters.Clear();
                Sqlcon.Close();
                return estado;
            }
            else
            {
                estado = "";
                Sqlcmd.Parameters.Clear();
                Sqlcon.Close();
                return estado;
            }
        }
        public List<DtoProtocolos> RecuperarProtocolos(int codigo) // Recupera el listado de protocolos operatorios de un paciente / Giovanny Tapia / 20/09/2012
        {
            List<DtoProtocolos> Resultado = new List<DtoProtocolos>();

            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var Protocolos = (from a in contexto.ATENCION_DETALLE_FORMULARIOS_HCU.Include("ATENCIONES")
                                  join u in contexto.USUARIOS on a.ID_USUARIO equals u.ID_USUARIO
                                  where a.ATENCIONES.ATE_CODIGO == codigo && a.FORMULARIOS_HCU.FH_CODIGO == 21
                                  select new
                                  {
                                      a.ADF_CODIGO,
                                      a.ATENCIONES.ATE_CODIGO,
                                      a.ADF_FECHA_INGRESO,
                                      u.NOMBRES,
                                      u.APELLIDOS
                                  });

                foreach (var Item in Protocolos)
                {
                    Resultado.Add(new DtoProtocolos()
                    {
                        Codigo = Item.ADF_CODIGO,
                        CodigoAtencion = Item.ATE_CODIGO,
                        FechaIngreso = Item.ADF_FECHA_INGRESO,
                        Usuario = Item.NOMBRES + " " + Item.APELLIDOS

                    });
                }

                return Resultado;

            }
        }


        public bool VerificaProtocolos(int Atencion, int Protocolo) // Verifica si el formulario tiene datos / Giovanny Tapia / 20/09/2012
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from a in contexto.HC_PROTOCOLO_OPERATORIO
                             where a.ATENCIONES.ATE_CODIGO == Atencion && a.ADF_CODIGO == Protocolo
                             select a.ATENCIONES.ATE_CODIGO).ToList();

                if (lista.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public ATENCIONES RecuperarAtencionIDEmerg(int codigo)
        {

            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {


                return contexto.ATENCIONES.Include("PACIENTES").Include("PACIENTES_DATOS_ADICIONALES").Include("MEDICOS").Include("ATENCION_FORMAS_LLEGADA").Include("USUARIOS").FirstOrDefault(a => a.ATE_CODIGO == codigo);
                //return contexto.ATENCIONES.Include("ATENCION_FORMAS_LLEGADA").Include("MEDICOS").Include("USUARIOS").Where(a => a.PACIENTES.PAC_CODIGO == keyPaciente).First();
            }

        }

        public ATENCIONES AtencionID(Int64 codigo)
        {

            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                //return contexto.ATENCIONES.Include("PACIENTES").Include("HABITACIONES").Include("CAJAS").FirstOrDefault(a => a.ATE_CODIGO == codigo);
                return (from a in contexto.ATENCIONES
                        where a.ATE_CODIGO == codigo
                        select a).FirstOrDefault();
            }

        }
        public DataTable atencionesID(Int64 ATE_CODIGO)
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
            Sqlcmd = new SqlCommand("select T.TIP_DESCRIPCION from ATENCIONES A inner join TIPO_INGRESO T ON A.TIP_CODIGO = T.TIP_CODIGO where ATE_CODIGO = " + ATE_CODIGO + "", Sqlcon);
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
        public DataTable RecuperaAtencionesSubsecuentes(Int64 PAC_CODIGO)
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
            Sqlcmd = new SqlCommand("select a.ATE_CODIGO ID, CONCAT(m.MED_APELLIDO_PATERNO, ' ', m.MED_APELLIDO_MATERNO, ' ', m.MED_NOMBRE1, ' ', m.MED_NOMBRE2) as MEDICO, t.TIP_DESCRIPCION 'TIPO DE INGRESO', a.ATE_FECHA_INGRESO INGRESO, a.ATE_FECHA_ALTA ALTA from ATENCIONES a inner join MEDICOS m on a.MED_CODIGO=m.MED_CODIGO inner join TIPO_INGRESO t on a.TIP_CODIGO=t.TIP_CODIGO inner join Form002MSP f on a.ATE_CODIGO = f.AteCodigo where a.ESC_CODIGO NOT IN (13,7) and a.PAC_CODIGO = " + PAC_CODIGO + " and a.TIP_CODIGO IN (4,10,12) ORDER BY 1 DESC", Sqlcon);
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
        //Recupera los datos de la atencion
        public DataTable CodigoConvenio(Int64 AtencionCodigo)
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

            Sqlcmd = new SqlCommand("sp_DatosConvenioAtencion", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = AtencionCodigo;


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        public int UltimoCodigoAtenciones()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from a in contexto.ATENCIONES
                             select a.ATE_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();


                return 0;
            }
        }

        public Int32 UltimoCodigoAtenciones_sp()
        {
            // GIOVANNY TAPIA / 07/08/2012
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
            Sqlcmd = new SqlCommand("sp_SecuencialAtencionCodigo", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            if (Dts.Rows.Count > 0)
            {
                return Convert.ToInt32(Dts.Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }

        public void EditarAtencionAdmision(ATENCIONES atencionModificada, int aux)
        {
            //if (atencionModificada.ATE_ACOMPANANTE_CEDULA.Length > 10)
            //{
            //    atencionModificada.ATE_ACOMPANANTE_CEDULA = atencionModificada.ATE_ACOMPANANTE_CEDULA.Substring(0, 10);
            //}
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ATENCIONES atencion = contexto.ATENCIONES.FirstOrDefault(a => a.ATE_CODIGO == atencionModificada.ATE_CODIGO);
                atencion.ATE_FECHA_INGRESO = atencionModificada.ATE_FECHA_INGRESO;
                atencion.ATE_FECHA_ALTA = atencionModificada.ATE_FECHA_ALTA;
                atencion.ATE_ACOMPANANTE_CEDULA = atencionModificada.ATE_ACOMPANANTE_CEDULA;
                atencion.ATE_ACOMPANANTE_CIUDAD = atencionModificada.ATE_ACOMPANANTE_CIUDAD;
                atencion.ATE_ACOMPANANTE_DIRECCION = atencionModificada.ATE_ACOMPANANTE_DIRECCION;
                atencion.ATE_ACOMPANANTE_NOMBRE = atencionModificada.ATE_ACOMPANANTE_NOMBRE;
                atencion.ATE_ACOMPANANTE_PARENTESCO = atencionModificada.ATE_ACOMPANANTE_PARENTESCO;
                atencion.ATE_ACOMPANANTE_TELEFONO = atencionModificada.ATE_ACOMPANANTE_TELEFONO;
                atencion.ATE_DIAGNOSTICO_FINAL = atencionModificada.ATE_DIAGNOSTICO_FINAL;
                atencion.ATE_DIAGNOSTICO_INICIAL = atencionModificada.ATE_DIAGNOSTICO_INICIAL;
                atencion.ATE_GARANTE_CEDULA = atencionModificada.ATE_GARANTE_CEDULA;
                atencion.ATE_GARANTE_CIUDAD = atencionModificada.ATE_GARANTE_CIUDAD;
                atencion.ATE_GARANTE_DIRECCION = atencionModificada.ATE_GARANTE_DIRECCION;
                atencion.ATE_GARANTE_MONTO_GARANTIA = atencionModificada.ATE_GARANTE_MONTO_GARANTIA;
                atencion.ATE_GARANTE_NOMBRE = atencionModificada.ATE_GARANTE_NOMBRE;
                atencion.ATE_GARANTE_PARENTESCO = atencionModificada.ATE_GARANTE_PARENTESCO;
                atencion.ATE_GARANTE_TELEFONO = atencionModificada.ATE_GARANTE_TELEFONO;
                atencion.ATE_OBSERVACIONES = atencionModificada.ATE_OBSERVACIONES;
                atencion.ATE_REFERIDO = atencionModificada.ATE_REFERIDO;
                atencion.TIF_CODIGO = atencionModificada.TIF_CODIGO;
                atencion.ATENCION_FORMAS_LLEGADAReference.EntityKey = atencionModificada.ATENCION_FORMAS_LLEGADAReference.EntityKey;
                atencion.ATE_DIRECTORIO = atencionModificada.ATE_DIRECTORIO;
                atencion.HABITACIONESReference.EntityKey = atencionModificada.HABITACIONESReference.EntityKey;
                atencion.MEDICOSReference.EntityKey = atencionModificada.MEDICOSReference.EntityKey;
                atencion.PACIENTES_DATOS_ADICIONALESReference.EntityKey = atencionModificada.PACIENTES_DATOS_ADICIONALESReference.EntityKey;
                atencion.PACIENTESReference.EntityKey = atencionModificada.PACIENTESReference.EntityKey;
                atencion.TIPO_TRATAMIENTOReference.EntityKey = atencionModificada.TIPO_TRATAMIENTOReference.EntityKey;
                atencion.TIPO_REFERIDOReference.EntityKey = atencionModificada.TIPO_REFERIDOReference.EntityKey;
                atencion.TIPO_INGRESOReference.EntityKey = atencionModificada.TIPO_INGRESOReference.EntityKey;
                atencion.USUARIOSReference.EntityKey = atencionModificada.USUARIOSReference.EntityKey;
                atencion.TIF_OBSERVACION = atencionModificada.TIF_OBSERVACION;
                //atencion.ATE_FACTURA_NOMBRE = atencionModificada.ATE_FACTURA_NOMBRE;
                atencion.ATE_EDAD_PACIENTE = atencionModificada.ATE_EDAD_PACIENTE;
                atencion.ATE_REFERIDO_DE = atencionModificada.ATE_REFERIDO_DE;
                atencion.ATE_ESTADO = atencionModificada.ATE_ESTADO;
                atencion.ATE_EN_QUIROFANO = atencionModificada.ATE_EN_QUIROFANO;
                atencion.ATE_QUIEN_ENTREGA_PAC = atencionModificada.ATE_QUIEN_ENTREGA_PAC;
                //if (aux == 0)
                //    atencion.ATE_FECHA_ALTA = null;
                if (aux == 2)
                    atencion.ATE_FECHA_ALTA = atencionModificada.ATE_FECHA_ALTA;
                atencion.ESC_CODIGO = atencionModificada.ESC_CODIGO;
                //if (atencion.ESC_CODIGO == 2)
                //{
                //    atencion.ATE_FACTURA_PACIENTE = null;
                //    atencion.ATE_FACTURA_FECHA = null;
                //}
                atencion.TipoAtencion = atencionModificada.TipoAtencion;

                /*discapacidad*/

                atencion.ate_discapacidad = atencionModificada.ate_discapacidad;
                atencion.ate_carnet_conadis = atencionModificada.ate_carnet_conadis;
                atencion.ATE_ID_ACCIDENTE = atencionModificada.ATE_ID_ACCIDENTE;
                atencion.ATE_FUENTE_INFORMACION = atencionModificada.ATE_FUENTE_INFORMACION;
                atencion.ATE_INSTITUCION_ENTREGA = atencionModificada.ATE_INSTITUCION_ENTREGA;
                atencion.ATE_INSTITUCION_TELEFONO = atencionModificada.ATE_INSTITUCION_TELEFONO;
                /**/
                try
                {
                    contexto.SaveChanges();

                }
                catch (Exception ex)
                {

                    //throw;
                }


            }
        }

        public List<ATENCIONES> listaAtencionesPaciente(Int64 codPaciente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from a in contexto.ATENCIONES
                        join p in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                        where a.PACIENTES.PAC_CODIGO == codPaciente
                        select a).ToList();
            }
        }

        public List<ATENCIONES> listaAtencionesPacienteConPedidos(int codPaciente, int estado, string busqPedido, string desde, string hasta)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                if (isNumeric(busqPedido) && desde != null && hasta != null)
                {
                    int codPedido = Convert.ToInt32(busqPedido);
                    DateTime fdesde = Convert.ToDateTime(desde);
                    DateTime fhasta = Convert.ToDateTime(hasta);

                    return (from a in contexto.ATENCIONES
                            join p in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                            join ped in contexto.PEDIDOS on a.ATE_CODIGO equals ped.ATE_CODIGO
                            where a.PACIENTES.PAC_CODIGO == codPaciente
                                && ped.PED_ESTADO == estado
                                && ped.PED_CODIGO == codPedido
                                && ped.PED_FECHA >= fdesde
                                && ped.PED_FECHA <= fhasta
                            select a).Distinct().ToList();
                }
                else if (isNumeric(busqPedido))
                {
                    int codPedido = Convert.ToInt32(busqPedido);

                    return (from a in contexto.ATENCIONES
                            join p in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                            join ped in contexto.PEDIDOS on a.ATE_CODIGO equals ped.ATE_CODIGO
                            where a.PACIENTES.PAC_CODIGO == codPaciente
                                && ped.PED_ESTADO == estado
                                && ped.PED_CODIGO == codPedido
                            select a).Distinct().ToList();
                }
                else if (desde != null && hasta != null)
                {
                    DateTime fdesde = Convert.ToDateTime(desde);
                    DateTime fhasta = Convert.ToDateTime(hasta);

                    return (from a in contexto.ATENCIONES
                            join p in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                            join ped in contexto.PEDIDOS on a.ATE_CODIGO equals ped.ATE_CODIGO
                            where a.PACIENTES.PAC_CODIGO == codPaciente
                                && ped.PED_ESTADO == estado
                                && ped.PED_FECHA >= fdesde
                                && ped.PED_FECHA <= fhasta
                            select a).Distinct().ToList();
                }
                return (from a in contexto.ATENCIONES
                        join p in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                        join ped in contexto.PEDIDOS on a.ATE_CODIGO equals ped.ATE_CODIGO
                        where a.PACIENTES.PAC_CODIGO == codPaciente
                        && ped.PED_ESTADO == estado
                        select a).Distinct().ToList();
            }
        }

        public bool existeAtencion(string numControl, string numFactura)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ATENCIONES atencion = (from a in contexto.ATENCIONES
                                       where a.ATE_NUMERO_CONTROL == numControl || a.ATE_FACTURA_PACIENTE == numFactura
                                       select a).FirstOrDefault();
                if (atencion != null)
                    return true;

                return false;
            }
        }

        public bool existeAtencionAdmision(string numControl)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ATENCIONES atencion = (from a in contexto.ATENCIONES
                                       where a.ATE_NUMERO_ATENCION == numControl
                                       select a).FirstOrDefault();
                if (atencion != null)
                    return false;

                return true;
            }
        }

        //public bool RecuperarPacientes (string cedula, string numeroAtencion)
        //{
        //    PACIENTES paciente = new PACIENTES();
        //    ATENCIONES atencion = new ATENCIONES();
        //    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //    {
        //        paciente = (from p in contexto.PACIENTES
        //                    where p.PAC_IDENTIFICACION == cedula
        //                    select p).FirstOrDefault();
        //        if (paciente != null)
        //        {
        //            atencion = (from a in contexto.ATENCIONES
        //                        where a.ATE_NUMERO_ATENCION == numeroAtencion && a.PACIENTES.PAC_CODIGO == paciente.PAC_CODIGO
        //                        select a).FirstOrDefault();
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //}

        public List<DtoAtenciones> atencionesActivas()
        {
            List<DtoAtenciones> atenciongrid = new List<DtoAtenciones>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                var atenciones = from t in contexto.ATENCIONES
                                 join p in contexto.PACIENTES on t.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                 join h in contexto.HABITACIONES on t.HABITACIONES.hab_Codigo equals h.hab_Codigo
                                 join a in contexto.PACIENTES_DATOS_ADICIONALES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                 //join c in contexto.CAJAS on t.CAJAS.CAJ_CODIGO equals c.CAJ_CODIGO
                                 //join u in contexto.USUARIOS on t.USUARIOS.ID_USUARIO equals u.ID_USUARIO
                                 where a.DAP_ESTADO == true
                                 && t.ATE_FECHA_ALTA == null
                                 orderby t.ATE_FECHA
                                 select new
                                 {
                                     t.ATE_CODIGO,
                                     t.ATE_NUMERO_CONTROL,
                                     t.ATE_FACTURA_PACIENTE,
                                     t.PACIENTES.PAC_CODIGO,
                                     t.HABITACIONES.hab_Codigo,
                                     p.PAC_APELLIDO_PATERNO,
                                     p.PAC_APELLIDO_MATERNO,
                                     p.PAC_NOMBRE1,
                                     p.PAC_NOMBRE2,
                                     p.PAC_HISTORIA_CLINICA,
                                     //p.PAC_TIPO_IDENTIFICACION,
                                     //p.PAC_IDENTIFICACION,
                                     //a.DAP_DIRECCION_DOMICILIO,
                                     //a.DAP_TELEFONO1,
                                     h.hab_Numero,
                                     t.PACIENTES_DATOS_ADICIONALES.DAP_CODIGO,
                                     t.ATE_FACTURA_FECHA,
                                     t.ATE_FECHA,
                                     t.ATE_FECHA_INGRESO,
                                     t.ATE_FECHA_ALTA,
                                     t.ATE_ESTADO,
                                     t.ATE_REFERIDO,
                                     t.ATE_NUMERO_ATENCION
                                 };

                //if (codigoCaja != 0)
                //{
                //    atenciones.Where(a => a.CAJ_CODIGO == codigoCaja);
                //}

                foreach (var acceso in atenciones)
                {
                    atenciongrid.Add(new DtoAtenciones()
                    {
                        //ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName(),
                        //ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key,
                        ATE_CODIGO = acceso.ATE_CODIGO,
                        ATE_NUMERO_CONTROL = acceso.ATE_NUMERO_CONTROL,
                        ATE_FACTURA_PACIENTE = acceso.ATE_FACTURA_PACIENTE,
                        ATE_NUMERO_ATENCION = acceso.ATE_NUMERO_ATENCION.Trim().ToString(),
                        PAC_CODIGO = acceso.PAC_CODIGO,
                        DAP_CODIGO = acceso.DAP_CODIGO,
                        PAC_APELLIDO_PATERNO = acceso.PAC_APELLIDO_PATERNO,
                        PAC_APELLIDO_MATERNO = acceso.PAC_APELLIDO_MATERNO,
                        PAC_NOMBRE = acceso.PAC_NOMBRE1,
                        PAC_NOMBRE2 = acceso.PAC_NOMBRE2,
                        PAC_HCL = acceso.PAC_HISTORIA_CLINICA,
                        //PAC_CEDULA = acceso.PAC_IDENTIFICACION,
                        //PAC_DIRECCION = acceso.DAP_DIRECCION_DOMICILIO,
                        //PAC_TELEFONO = acceso.DAP_TELEFONO1,
                        HAB_CODIGO = acceso.hab_Codigo,
                        HAB_NUMERO = acceso.hab_Numero,
                        ATE_FACTURA_FECHA = Convert.ToDateTime(acceso.ATE_FACTURA_FECHA),
                        ATE_FECHA = Convert.ToDateTime(acceso.ATE_FECHA),
                        ATE_FECHA_INGRESO = Convert.ToDateTime(acceso.ATE_FECHA_INGRESO),
                        ATE_FECHA_ALTA = Convert.ToDateTime(acceso.ATE_FECHA_ALTA),
                        ATE_ESTADO = Convert.ToBoolean(acceso.ATE_ESTADO),
                        ATE_REFERIDO = Convert.ToBoolean(acceso.ATE_REFERIDO)
                    });
                }
                return atenciongrid;
            }
        }

        public int ultimoNumeroAdmision(int codPaciente)
        {
            int r;
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

            Sqlcmd = new SqlCommand("SELECT  COUNT(*) + 1 from ATENCIONES WHERE PAC_CODIGO = " + codPaciente, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            r = Convert.ToInt16(Dts.Rows[0][0]);

            //int maxim;
            //using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            //{
            //    List<ATENCIONES> paciente = new List<ATENCIONES>();
            //    paciente = contexto.ATENCIONES.ToList();
            //    if (paciente.Count > 0)
            //    {
            //        maxim = contexto.ATENCIONES.Max(loc => int.Parse(loc.ATE_NUMERO_ATENCION));
            //        if (maxim < 1000)
            //            maxim = 1000;
            //    }
            //    else
            //        maxim = 0;
            //    return maxim;
            //}

            //try
            //{
            //    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            //    {
            //        var listaCodigos = (from e in contexto.ATENCIONES
            //                            join p in contexto.PACIENTES on e.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
            //                            where e.PACIENTES.PAC_CODIGO == codPaciente
            //                            select e.ATE_NUMERO_ADMISION).ToList();

            //        if (listaCodigos.Count > 0)
            //            return (Int32)listaCodigos.Max();

            //        return 0;
            //    }
            //}
            //catch (Exception err)
            //{
            return r;
            //}
        }

        public List<DtoAtenciones> atencionesPorFacturar()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var atenciones = (from a in contexto.ATENCIONES
                                  join h in contexto.HABITACIONES on a.HABITACIONES.hab_Codigo equals h.hab_Codigo
                                  join p in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                  where a.ATE_FACTURA_FECHA == null
                                  select new
                                  {
                                      a.ATE_CODIGO,
                                      a.ATE_NUMERO_ATENCION,
                                      a.ATE_FECHA_INGRESO,
                                      a.ATE_REFERIDO,
                                      h.hab_Numero,
                                      p.PAC_APELLIDO_PATERNO,
                                      p.PAC_APELLIDO_MATERNO,
                                      p.PAC_NOMBRE1,
                                      p.PAC_NOMBRE2,
                                      p.PAC_HISTORIA_CLINICA
                                  }).ToList();

                List<DtoAtenciones> dtoAtenciones = new List<DtoAtenciones>();

                foreach (var acceso in atenciones)
                {
                    dtoAtenciones.Add(new DtoAtenciones()
                    {
                        ATE_CODIGO = acceso.ATE_CODIGO,
                        ATE_NUMERO_ATENCION = acceso.ATE_NUMERO_ATENCION,
                        ATE_FECHA_INGRESO = Convert.ToDateTime(acceso.ATE_FECHA_INGRESO),
                        HAB_NUMERO = acceso.hab_Numero,
                        PAC_APELLIDO_PATERNO = acceso.PAC_APELLIDO_PATERNO,
                        PAC_APELLIDO_MATERNO = acceso.PAC_APELLIDO_MATERNO,
                        PAC_NOMBRE = acceso.PAC_NOMBRE1,
                        PAC_NOMBRE2 = acceso.PAC_NOMBRE2,
                        PAC_HCL = acceso.PAC_HISTORIA_CLINICA,
                        ATE_REFERIDO = Convert.ToBoolean(acceso.ATE_REFERIDO)
                    });
                }
                return dtoAtenciones;
            }
        }

        public void ingresarDatosFactura(ATENCIONES atencionM)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ATENCIONES atencionO = contexto.ATENCIONES.FirstOrDefault(a => a.ATE_CODIGO == atencionM.ATE_CODIGO);
                atencionO.ATE_FACTURA_FECHA = atencionM.ATE_FACTURA_FECHA;
                atencionO.ATE_FECHA_ALTA = atencionM.ATE_FECHA_ALTA;
                atencionO.ATE_FACTURA_PACIENTE = atencionM.ATE_FACTURA_PACIENTE;
                atencionO.ATE_NUMERO_CONTROL = atencionM.ATE_NUMERO_CONTROL;
                contexto.SaveChanges();
            }
        }

        //valida si un string es numerico
        public static bool isNumeric(object value)
        {
            try
            {
                double d = System.Double.Parse(value.ToString(), System.Globalization.NumberStyles.Any);
                return true;
            }
            catch (System.FormatException)
            {
                return false;
            }
        }

        public DataTable ListaMedicamentos(string Filtro)
        {
            // GIOVANNY TAPIA / 07/08/2012
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
            Sqlcmd = new SqlCommand("sp_Medicamentos", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Codigo", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Codigo"].Value = Filtro;

            Sqlcmd.Parameters.Add("@Generico", SqlDbType.Int);
            Sqlcmd.Parameters["@Generico"].Value = 0;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            return Dts;

        }

        public DataTable ListaMedicos(string Filtro)
        {
            // GIOVANNY TAPIA / 07/08/2012
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
            Sqlcmd = new SqlCommand("sp_RecuperaListaMedico", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Tipo", SqlDbType.Int);
            Sqlcmd.Parameters["@Tipo"].Value = Filtro;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            return Dts;

        }

        public void OtrosSeguros(Int64 CodigoAtencion, Int32 CodigoAnexo, String Descripcion, String CodigoValidacion)
        {
            // GIOVANNY TAPIA / 07/08/2012
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
            Sqlcmd = new SqlCommand("sp_InsertaDatosOtroSeguro", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = CodigoAtencion;

            Sqlcmd.Parameters.Add("@ANI_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@ANI_CODIGO"].Value = CodigoAnexo;

            Sqlcmd.Parameters.Add("@DESCRIPCION", SqlDbType.VarChar);
            Sqlcmd.Parameters["@DESCRIPCION"].Value = Descripcion;

            Sqlcmd.Parameters.Add("@CODIGO_VALIDACION", SqlDbType.VarChar);
            Sqlcmd.Parameters["@CODIGO_VALIDACION"].Value = CodigoValidacion;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

        }

        public DataTable RecuperaOtrosSeguros(Int64 CodigoAtencion)
        {
            // GIOVANNY TAPIA / 07/08/2012
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
            Sqlcmd = new SqlCommand("sp_RecuperaOtrosSeguros", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = CodigoAtencion;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            return Dts;

        }

        public void EliminaOtrosSeguros(Int64 CodigoAtencion)
        {
            // GIOVANNY TAPIA / 07/08/2012
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
            Sqlcmd = new SqlCommand("sp_EliminaOtrosSeguros", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = CodigoAtencion;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

        }

        public void EliminaDerivado(Int64 CodigoAtencion)
        {
            // GIOVANNY TAPIA / 07/08/2012
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
            Sqlcmd = new SqlCommand("sp_EliminaDerivados", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = CodigoAtencion;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

        }

        public void AgregarDerivado(Int64 CodigoAtencion, Int32 CodigoAnexoDerivacion, Int32 CodigoAnexoRed, String Ruc, String Descripcion)
        {
            // GIOVANNY TAPIA / 07/08/2012
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
            Sqlcmd = new SqlCommand("sp_InsertaDatosDerivado", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = CodigoAtencion;

            Sqlcmd.Parameters.Add("@ANI_CODIGO_DERIVACION", SqlDbType.Int);
            Sqlcmd.Parameters["@ANI_CODIGO_DERIVACION"].Value = CodigoAnexoDerivacion;

            Sqlcmd.Parameters.Add("@ANI_CODIGO_RED", SqlDbType.Int);
            Sqlcmd.Parameters["@ANI_CODIGO_RED"].Value = CodigoAnexoRed;

            Sqlcmd.Parameters.Add("@RUC_RED", SqlDbType.VarChar);
            Sqlcmd.Parameters["@RUC_RED"].Value = Ruc;

            Sqlcmd.Parameters.Add("@DESCRIPCION_OTROS", SqlDbType.VarChar);
            Sqlcmd.Parameters["@DESCRIPCION_OTROS"].Value = Descripcion;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

        }

        public DataTable RecuperaDerivado(Int64 CodigoAtencion)
        {
            // GIOVANNY TAPIA / 07/08/2012
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
            Sqlcmd = new SqlCommand("sp_RecuperaDerivado", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = CodigoAtencion;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            return Dts;

        }
        public DataTable VerificaFactura(Int32 CodigoAtencion)
        {
            // DAVID MANTILLA verifica si tiene # factura no ingresa pedidos/ 26/12/2013
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

            Sqlcmd = new SqlCommand("sp_Verificafactura", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = (CodigoAtencion);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            return Dts;
        }

        public DataTable tipos_atenciones(string listado)
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
            Sqlcmd = new SqlCommand("SELECT  *  FROM [His3000].[dbo].[tipos_atenciones] where list like '" + listado + "%'", Sqlcon);

            Sqlcmd.CommandType = CommandType.Text;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        }
        public DataTable Recuperaatencion(Int32 CodigoAtencion)
        {
            // DAVID MANTILLA verifica si tiene # factura no ingresa pedidos/ 26/12/2013
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

            Sqlcmd = new SqlCommand("sp_recuperaatencion", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Atencion", SqlDbType.Int);
            Sqlcmd.Parameters["@Atencion"].Value = (CodigoAtencion);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            return Dts;
        }
        public string TipoPaquete(int cod_tipoAtencion)
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
            Sqlcmd = new SqlCommand("select CAC_NOMBRE from CATALOGO_COSTOS where CAC_CODIGO =" + cod_tipoAtencion + " ", Sqlcon);

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

            if (Dts.Rows.Count > 0)
            {
                //return (cod_tipoAtencion + "  - " + Convert.ToString(Dts.Rows[0][0]));
                return (Convert.ToString(Dts.Rows[0][0]));
            }
            else
            {
                return "0";
            }

        }
        public List<CATALOGO_COSTOS> TiposPaquetes()
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from cc in db.CATALOGO_COSTOS
                        join ct in db.CATALOGO_COSTOS_TIPO on cc.CATALOGO_COSTOS_TIPO.CCT_CODIGO equals ct.CCT_CODIGO
                        where ct.CCT_CODIGO == 11
                        select cc).ToList();
            }

            // Se comenta por que se empieza a trabajar con el modelo // MarioValencia // 2024-04-12
            //    SqlConnection Sqlcon;
            //SqlCommand Sqlcmd;
            //SqlDataAdapter Sqldap;
            //DataSet Dts;
            //BaseContextoDatos obj = new BaseContextoDatos();
            //Sqlcon = obj.ConectarBd();
            //try
            //{
            //    Sqlcon.Open();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //Sqlcmd = new SqlCommand("SELECT CAC_CODIGO AS id, CAC_NOMBRE AS name FROM CATALOGO_COSTOS CC INNER JOIN CATALOGO_COSTOS_TIPO CCT ON CC.CCT_CODIGO = CCT.CCT_CODIGO WHERE CCT.CCT_CODIGO = 11", Sqlcon);

            //Sqlcmd.CommandType = CommandType.Text;

            //Sqldap = new SqlDataAdapter();
            //Sqlcmd.CommandTimeout = 180;
            //Sqldap.SelectCommand = Sqlcmd;
            //Dts = new DataSet();
            //Sqldap.Fill(Dts, "tabla");
            //try
            //{
            //    Sqlcon.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //return Dts.Tables["tabla"];

        }


        public DataTable RecuperaPermisos()
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
            Sqlcmd = new SqlCommand("select PAD_ACTIVO from PARAMETROS_DETALLE where PAD_CODIGO in(9,10)", Sqlcon);

            Sqlcmd.CommandType = CommandType.Text;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        }

        public DataTable RecuperaParametroCertificado()
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
            Sqlcmd = new SqlCommand("select PAD_VALOR from PARAMETROS_DETALLE where PAD_CODIGO in(39)", Sqlcon);

            Sqlcmd.CommandType = CommandType.Text;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        }

        public DataTable Atencion(Int64 ate_codigo)
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
            Sqlcmd = new SqlCommand("sp_Atencion", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@ate_codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@ate_codigo"].Value = ate_codigo;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            { }

            return Dts;

        }


        public bool ValidaEmergencia(Int64 ate_codigo)
        {

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            bool emergencia = false;
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
            Sqlcmd = new SqlCommand("select * from ATENCIONES where TIP_CODIGO = 1 and ESC_CODIGO = 1 and ATE_CODIGO = @ate_codigo", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            SqlDataReader reader = Sqlcmd.ExecuteReader();
            if (reader.HasRows)
            {
                emergencia = true;
            }
            else
            {
                emergencia = false;
            }
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            { Console.Write(ex.Message); }
            return emergencia;


        }
        public bool IngresaDesactivacionAtencion(Int64 atencion, string motivo)
        {
            SqlConnection Sqlcon = null;
            SqlCommand Sqlcmd = null;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool response = false;
            try
            {
                Sqlcon = obj.ConectarBd();
                Sqlcmd = new SqlCommand("sp_IngresaDesactivacionAtencion", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Parameters.AddWithValue("@atencion", atencion);
                Sqlcmd.Parameters.AddWithValue("@motivo", motivo);
                Sqlcon.Open();
                int filas = Sqlcmd.ExecuteNonQuery();
                if (filas > 0)
                    response = true;
            }
            catch (Exception ex)
            {
                return response;
                throw ex;
            }
            finally
            {
                Sqlcon.Close();
            }
            return response;
        }

        public DataTable ValidaEstatusAtencion(Int64 atencion)
        {

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcmd = new SqlCommand("sp_ValidaEstatusAtencion", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Parameters.Add("@atencion", SqlDbType.Int);
                Sqlcmd.Parameters["@atencion"].Value = atencion;
                Sqlcon.Open();
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Sqlcon.Close();
            }

            return Dts;
        }

        public Boolean actualizaEscCodigoPreadmision(int ateCodigo, int valor)
        {
            Boolean ok = false;
            string codigo = Convert.ToString(ateCodigo);
            try
            {

                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    ATENCIONES atencion = (from a in contexto.ATENCIONES
                                           where a.ATE_NUMERO_ATENCION == codigo
                                           select a).FirstOrDefault();
                    if (atencion != null)
                    {
                        if (atencion.ESC_CODIGO == 14)
                        {
                            atencion.ESC_CODIGO = valor;
                            atencion.ATE_FECHA_INGRESO = DateTime.Now;
                            contexto.SaveChanges();
                        }


                        if (atencion.ESC_CODIGO == 1)
                        {
                            int ate_codigo = (from a in contexto.ATENCIONES
                                              where a.ATE_NUMERO_ATENCION == codigo
                                              select a.ATE_CODIGO).FirstOrDefault();

                            PREATENCIONES pre = (from a in contexto.PREATENCIONES
                                                 where a.PREA_COD_ATENCION == ate_codigo
                                                 select a).FirstOrDefault();
                            if (pre != null)
                            {
                                contexto.DeleteObject(pre);
                                contexto.SaveChanges();
                            }
                        }
                    }
                    ok = true;
                }
            }
            catch (Exception err) { throw err; }
            return ok;
        }

        public int TipoEmpresa(Int64 ate_codigo)
        {
            int te_codigo = 0;
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_TipoEmpresa", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                te_codigo = Convert.ToInt32(reader["TE_CODIGO"].ToString());
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return te_codigo;
        }
        public string PacienteAltaFacturado(Int64 ate_codigo)
        {
            string factura = "";
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("SELECT ATE_FACTURA_PACIENTE FROM ATENCIONES WHERE ATE_CODIGO = " + ate_codigo, connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                factura = reader["ATE_FACTURA_PACIENTE"].ToString();
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return factura;
        }

        public string PacienteDatosAdicionales(Int64 ate_codigo)
        {
            string email = "";
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("SELECT TOP 1 EMAIL_ACOMPAÑANTE FROM PACIENTES_DATOS_ADICIONALES2 P INNER JOIN ATENCIONES A ON P.PAC_CODIGO=A.PAC_CODIGO WHERE A.ATE_CODIGO =" + ate_codigo + " ORDER BY A.ATE_CODIGO DESC", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                email = reader["EMAIL_ACOMPAÑANTE"].ToString();
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return email;
        }

        public string SeguroTelefono(int cat_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            string telefono = "";
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("SELECT AE.ASE_TELEFONO FROM CATEGORIAS_CONVENIOS CC INNER JOIN ASEGURADORAS_EMPRESAS AE ON CC.ASE_CODIGO = AE.ASE_CODIGO WHERE CC.CAT_CODIGO = @cat_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@cat_codigo", cat_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                telefono = reader["ASE_TELEFONO"].ToString();
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return telefono;
        }

        public DataTable RegistroAdmision1(Int64 pacCodigo)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ADMISIONES_nuevo", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codigoPaciente", pacCodigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public DataTable RegistroAdmision2(Int64 ate_codigo)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("ADMISIONES2", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codigoAtencion", ate_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public DataTable CambiosAdmision(Int64 pac_codigo)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("REGISTRO_CAMBIOS", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codigoPaciente", pac_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public void FacturaA(string cedula, string direccion, string nombre, string telefono, string email)
        {
            SqlCommand command;
            SqlConnection connection;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_FacturaA", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cedula", cedula);
            command.Parameters.AddWithValue("@celular", telefono);
            command.Parameters.AddWithValue("@direccion", direccion);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@cliente", nombre);
            command.Parameters.AddWithValue("@id_usuario", Sesion.codUsuario);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }

        public DataTable CargarClienteSic(string ruccli)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_DatosClienteSic", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ruccli", ruccli);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;

        }
        public List<ASEGURADORAS_EMPRESAS> SeguroDefault()
        {
            List<ASEGURADORAS_EMPRESAS> seguro = new List<ASEGURADORAS_EMPRESAS>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                seguro = (from s in db.ASEGURADORAS_EMPRESAS
                          where s.TIPO_EMPRESA.TE_CODIGO == 1
                          && s.ASE_ESTADO == true && s.ASE_FIN_CONVENIO >= DateTime.Now
                          select s).ToList();
                return seguro;
            }
        }
        public void BarridoNumeroAtencion(Int64 pac_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ATENCIONES> la = (from a in db.ATENCIONES
                                       where a.PACIENTES.PAC_CODIGO == pac_codigo
                                       select a).OrderBy(x => x.ATE_FECHA_INGRESO).ToList();
                DbTransaction transaction;
                ConexionEntidades.ConexionEDM.Open();
                transaction = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    int contador = 1;
                    foreach (var item in la)
                    {
                        ATENCIONES a = db.ATENCIONES.FirstOrDefault(x => x.ATE_CODIGO == item.ATE_CODIGO);
                        a.ATE_NUMERO_ADMISION = contador;
                        contador++;

                        db.SaveChanges();
                    }
                    transaction.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }
        }
        public CATEGORIAS_CONVENIOS tipoSeguro(Int64 ate_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                CATEGORIAS_CONVENIOS cc = (from a in db.ATENCIONES
                                           join ad in db.ATENCION_DETALLE_CATEGORIAS on a.ATE_CODIGO equals ad.ATENCIONES.ATE_CODIGO
                                           join c in db.CATEGORIAS_CONVENIOS on ad.CATEGORIAS_CONVENIOS.CAT_CODIGO equals c.CAT_CODIGO
                                           where a.ATE_CODIGO == ate_codigo
                                           select c).FirstOrDefault();
                return cc;
            }
        }
        public void CrearCAMBIO_ESTADO_ATENCIONES(Int64 ATE_CODIGO, Int32 ESC_CODIGO, Int64 ID_USUARIO, string CEA_MODULO)
        {
            CAMBIO_ESTADO_ATENCIONES cme = new CAMBIO_ESTADO_ATENCIONES();
            cme.ATE_CODIGO = ATE_CODIGO;
            cme.ESC_CODIGO = ESC_CODIGO;
            cme.ID_USUARIO = ID_USUARIO;
            cme.CEA_MODULO = CEA_MODULO;
            cme.CEA_FECHA = DateTime.Now;
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction trans = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    db.Crear("CAMBIO_ESTADO_ATENCIONES", cme);
                    db.SaveChanges();
                    trans.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    trans.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                }
            }
            //try // se comenta por que se cambia a grabado por modelo // Mario Valencia // 08-09-2023
            //{
            //    SqlCommand command;
            //    SqlConnection connection;
            //    SqlDataReader reader;
            //    BaseContextoDatos obj = new BaseContextoDatos();
            //    connection = obj.ConectarBd();
            //    connection.Open();
            //    command = new SqlCommand("INSERT INTO CAMBIO_ESTADO_ATENCIONES (ATE_CODIGO,ESC_CODIGO,ID_USUARIO,CEA_FECHA) VALUES (@ATE_CODIGO ,@ESC_CODIGO,@ID_USUARIO,getdate())", connection);
            //    command.CommandType = CommandType.Text;
            //    command.Parameters.AddWithValue("@ATE_CODIGO", ATE_CODIGO);
            //    command.Parameters.AddWithValue("@ESC_CODIGO", ESC_CODIGO);
            //    command.Parameters.AddWithValue("@ID_USUARIO", ID_USUARIO);
            //    reader = command.ExecuteReader();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

        }
        public TIPO_INGRESO RecuperaTipoIngresoCodigoAtencion(Int64 ATE_CODIGO)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from a in contexto.ATENCIONES
                        join t in contexto.TIPO_INGRESO on a.TIPO_INGRESO.TIP_CODIGO equals t.TIP_CODIGO
                        where a.ATE_CODIGO == ATE_CODIGO
                        select t).FirstOrDefault();
            }
        }
        public bool atencionReingreso(PACIENTES paciente)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                DateTime hace24Horas = DateTime.Now.AddHours(-24);
                //TIPO_INGRESO ti = (from t in db.TIPO_INGRESO where t.TIP_CODIGO == 1 select t).FirstOrDefault();
                var la = (from a in db.ATENCIONES where a.PACIENTES.PAC_CODIGO == paciente.PAC_CODIGO && a.TIPO_INGRESO.TIP_CODIGO == 1 && a.ATE_FECHA_ALTA >= hace24Horas select a).ToList();
                foreach (var item in la)
                {
                    HC_EMERGENCIA_FORM eform = (from fr in db.HC_EMERGENCIA_FORM where fr.ATENCIONES.ATE_CODIGO == item.ATE_CODIGO && fr.EMER_ESTADO == 1 select fr).FirstOrDefault();
                    if (eform != null)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public DataTable atencionesReingreso(Int64 PAC_CODIGO)
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
            Sqlcmd = new SqlCommand("select a.ATE_CODIGO ID, ISNULL((select top 1 CONCAT(m.MED_APELLIDO_PATERNO, ' ', m.MED_APELLIDO_MATERNO, ' ', m.MED_NOMBRE1, ' ', m.MED_NOMBRE2) from MEDICOS m where m.ID_USUARIO = f.ID_USUARIO),'') " +
                "as MEDICO, t.TIP_DESCRIPCION 'TIPO DE INGRESO',a.ATE_FECHA_INGRESO INGRESO, a.ATE_FECHA_ALTA ALTA from ATENCIONES a  " +
                "inner join TIPO_INGRESO t on a.TIP_CODIGO = t.TIP_CODIGO inner join HC_EMERGENCIA_FORM f on a.ATE_CODIGO = f.ATE_CODIGO where a.ESC_CODIGO NOT " +
                "IN(13, 7) and f.EMER_ESTADO = 1 and a.PAC_CODIGO = " + PAC_CODIGO + " and a.TIP_CODIGO IN (1) AND DATEDIFF(HOUR, a.ATE_FECHA_ALTA, GETDATE()) <= 24 ORDER BY 1 DESC", Sqlcon);
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
        public bool guardarReingredo(ATENCIONES_REINGRESO reIng)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    ATENCIONES_REINGRESO rein = db.ATENCIONES_REINGRESO.FirstOrDefault(a => a.ATE_CODIGO_REING == reIng.ATE_CODIGO_REING);
                    if (rein != null)
                    {
                        rein.ATE_CODIGO_PRINCIPAL = reIng.ATE_CODIGO_PRINCIPAL;
                        rein.ID_USUARIO = reIng.ID_USUARIO;
                        rein.ESTADO = true;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Crear("ATENCIONES_REINGRESO", reIng);
                        db.SaveChanges();
                    }
                    transac.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transac.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }
        }
        public List<ATENCIONES_REINGRESO> atencionReIngreso(Int64 ATE_CODIGO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from ar in db.ATENCIONES_REINGRESO select ar).Where(x => x.ATE_CODIGO_REING == ATE_CODIGO && x.ESTADO == true).ToList();
            }
        }
        public bool deshabilitaReIngreso(Int64 ATE_CODIGO, Int64 id_usuario)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                ATENCIONES_REINGRESO atReing = (from ar in db.ATENCIONES_REINGRESO where ar.ATE_CODIGO_REING == ATE_CODIGO select ar).FirstOrDefault();
                try
                {
                    atReing.ESTADO = false;
                    atReing.ID_USUARIO = id_usuario;
                    db.SaveChanges();
                    transac.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transac.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }
        }
        public ATENCIONES_REINGRESO buscaReIngresoXate_codigo(Int64 ate_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from ar in db.ATENCIONES_REINGRESO where ar.ATE_CODIGO_REING == ate_codigo select ar).FirstOrDefault();
            }
        }
    }

}

