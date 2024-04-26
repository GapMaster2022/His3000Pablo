using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using System.Data;
using System.Data.SqlClient;
using His.Parametros;
using System.Threading;
using System.Data.Common;

namespace His.Datos
{
    public class DatHonorariosMedicos
    {
        //public List<HonorariosMedicosFormulario> RecuperaMedicosHonorarios()
        //{
        //    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //    {
        //        //return contexto.HonorariosMedicosFormulario.ToList();
        //        return null;
        //    }
        //}
        public List<DtoHonorariosMedicos> RecuperaHonorariosMedicosFormulario(string medCodigo,string sinRetencion)
        {

            try
            {
                List<DtoHonorariosMedicos> honorariogrid = new List<DtoHonorariosMedicos>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<HONORARIOS_VISTA> honorarios = new List<HONORARIOS_VISTA>();
                    honorarios =new DatHonorariosMedicos().RecuperarHonorariosMedicos("1",null,medCodigo, null, null, null, null, null,
            null, null,sinRetencion, null,null, null, null, null,
            null, null, null, null,null,null);

                    foreach (var acceso in honorarios)
                    {

                        honorariogrid.Add(new DtoHonorariosMedicos()
                        {
                            ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName(),
                            ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key,
                            MED_CODIGO = acceso.MED_CODIGO==null? int.Parse("0"): int.Parse(acceso.MED_CODIGO.ToString()),
                            MED_NOMBRE = acceso.MED_NOMBRE_MEDICO,
                            HOM_CODIGO = acceso.HOM_CODIGO,
                            HOM_FACTURA_MEDICO = acceso.HOM_FACTURA_MEDICO,
                            FOR_CODIGO = acceso.FOR_CODIGO == null ? int.Parse("0") : int.Parse(acceso.FOR_CODIGO.ToString()),
                            FOR_DESCRIPCION = acceso.FOR_DESCRIPCION,
                            HOM_FECHA_INGRESO = Convert.ToDateTime(acceso.HOM_FECHA_INGRESO),
                            HOM_FACTURA_FECHA = DateTime.Parse(acceso.HOM_FACTURA_FECHA.ToString()),
                            HOM_VALOR_NETO = Convert.ToDecimal(acceso.HOM_VALOR_NETO),
                            HOM_COMISION_CLINICA = Convert.ToDecimal(acceso.HOM_COMISION_CLINICA),
                            HOM_APORTE_LLAMADA = Convert.ToDecimal(acceso.HOM_APORTE_LLAMADA),
                            HOM_RETENCION = Convert.ToDecimal(acceso.HOM_RETENCION),
                            HOM_VALOR_TOTAL = Convert.ToDecimal(acceso.HOM_VALOR_TOTAL),
                            HOM_VALOR_PAGADO = Convert.ToDecimal(acceso.HOM_VALOR_PAGADO),
                            HOM_LOTE = acceso.HOM_LOTE,
                            HOM_OBSERVACION = acceso.HOM_OBSERVACION,
                            //HOM_VALOR_CANCELADO = acceso.HOM_VALOR_CANCELADO == null ? int.Parse("0") : int.Parse(acceso.HOM_VALOR_CANCELADO.ToString()),
                            //HOM_ESTADO = Convert.ToBoolean(acceso),
                            ATE_CODIGO = acceso.ATE_CODIGO == null ? int.Parse("0") : int.Parse(acceso.ATE_CODIGO.ToString()),
                            RET_CODIGO = acceso.RET_CODIGO1,
                            ID_USUARIO = Convert.ToInt16(acceso.ID_USUARIO)
                        });
                    }

                }
                return honorariogrid;
                
            }
            catch (Exception err)
            {
                Console.Write(err.Message);
                return null;
            }
        }
        public DataTable EstadoCuenta(string fechaIni, string fechaFin, int medico)
        {//hola
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            int num = 0;
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
            Sqlcmd = new SqlCommand("sp_EstadoCuentas", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaInicio"].Value = Convert.ToDateTime( fechaIni);

            Sqlcmd.Parameters.Add("@FechaFin", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaFin"].Value = Convert.ToDateTime(fechaFin);

            Sqlcmd.Parameters.Add("@medico", SqlDbType.Int);
            Sqlcmd.Parameters["@medico"].Value = medico;

  

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        public DataTable RecuperaHonorarios(int ATE_CODIGO)
        {//hola
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
            Sqlcmd = new SqlCommand("sp_RecuperaHonoraios", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;         

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = ATE_CODIGO;

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
        public List<DtoHonorariosMedicos> RecuperaHonorariosMedicosPorAtencion(Int32 codigoAtencion)
        {

            try
            {
                List<DtoHonorariosMedicos> honorariogrid = new List<DtoHonorariosMedicos>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<HonorariosMedicosFormulario> honorarios = new List<HonorariosMedicosFormulario>();
                    honorarios = contexto.HonorariosMedicosFormulario.Where(h => h.ATE_CODIGO == codigoAtencion).ToList();

                    foreach (var acceso in honorarios)
                    {
                        honorariogrid.Add(new DtoHonorariosMedicos()
                        {
                            ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName(),
                            ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key,
                            MED_CODIGO = acceso.MED_CODIGO,
                            MED_NOMBRE = acceso.MED_NOMBRE,
                            HOM_CODIGO = acceso.HOM_CODIGO,
                            HOM_FACTURA_MEDICO = acceso.HOM_FACTURA_MEDICO,
                            FOR_CODIGO = acceso.FOR_CODIGO,
                            FOR_DESCRIPCION = acceso.FOR_DESCRIPCION,
                            HOM_FECHA_INGRESO = Convert.ToDateTime(acceso.HOM_FECHA_INGRESO),
                            HOM_FACTURA_FECHA = DateTime.Parse(acceso.HOM_FACTURA_FECHA.ToString()),
                            HOM_VALOR_NETO = Convert.ToDecimal(acceso.HOM_VALOR_NETO),
                            HOM_COMISION_CLINICA = Convert.ToDecimal(acceso.HOM_COMISION_CLINICA),
                            HOM_APORTE_LLAMADA = Convert.ToDecimal(acceso.HOM_APORTE_LLAMADA),
                            HOM_RETENCION = Convert.ToDecimal(acceso.HOM_RETENCION),
                            HOM_VALOR_TOTAL = Convert.ToDecimal(acceso.HOM_VALOR_TOTAL),
                            HOM_VALOR_PAGADO = Convert.ToDecimal(acceso.HOM_VALOR_PAGADO),
                            HOM_LOTE = acceso.HOM_LOTE,
                            HOM_OBSERVACION = acceso.HOM_OBSERVACION,
                            HOM_ESTADO = acceso.HOM_ESTADO.Value ,
                            ATE_CODIGO = acceso.ATE_CODIGO,
                            RET_CODIGO = acceso.RET_CODIGO1,
                            ID_USUARIO = Convert.ToInt16(acceso.ID_USUARIO)
                        });
                    }

                }
                return honorariogrid;
                
            }
            catch (Exception err)
            {
                Console.Write(err.Message);
                return null;
            }
        }

        public bool VerificarExistenciaHonorario(String numFactura, Int32 codMedico, int codigoAtencion)
        {       
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                HONORARIOS_MEDICOS consultaHonorario = (from h in contexto.HONORARIOS_MEDICOS
                                                        join m in contexto.MEDICOS on h.MEDICOS.MED_CODIGO equals m.MED_CODIGO
                                                        where h.HOM_FACTURA_MEDICO == numFactura && m.MED_CODIGO == codMedico && h.ATE_CODIGO==codigoAtencion
                                                            select h).FirstOrDefault();
                if (consultaHonorario != null)
                    return true;

                return false;
            }   
        }

        public HONORARIOS_MEDICOS HonorarioFacturaMedico(String numFactura, Int32 codMedico)
        {

            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.HONORARIOS_MEDICOS.Include("MEDICOS").Include("FORMA_PAGO").FirstOrDefault(h => h.HOM_FACTURA_MEDICO == numFactura && h.MEDICOS.MED_CODIGO == codMedico);
            }
        }

        public bool honorariosGeneraronPagosRetenciones(Int32 codAtencion)
        {

            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                List<HONORARIOS_MEDICOS> honorarios = new List<HONORARIOS_MEDICOS>();
                    honorarios = (from h in contexto.HONORARIOS_MEDICOS
                                      where h.ATE_CODIGO==codAtencion
                                      orderby h.HOM_VALOR_PAGADO descending 
                                      select h).ToList();
                foreach (HONORARIOS_MEDICOS honorario in honorarios)
                    if (honorario.HOM_VALOR_PAGADO > 0 || honorario.RET_CODIGO1 != null)
                        return true;

                return false;
            }

        }

        public void CrearHonorario(HONORARIOS_MEDICOS honorario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("HONORARIOS_MEDICOS", honorario);
                //contexto.AddToHONORARIOS_MEDICOS(honorario);
                //contexto.SaveChanges();
            }
        }
        public void EditarHonorarioMedico(HONORARIOS_MEDICOS honorario)
        {
            var newHonorario = new HONORARIOS_MEDICOS();
            using(var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                newHonorario = contexto.HONORARIOS_MEDICOS.Where(a => a.HOM_CODIGO == honorario.HOM_CODIGO)
                    .SingleOrDefault();

                if(newHonorario != null)
                {
                    newHonorario.FOR_CODIGO = honorario.FOR_CODIGO;
                }
            }
        }
        public void GrabarHonorario(HONORARIOS_MEDICOS honorarioModificado, HONORARIOS_MEDICOS honorarioOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                contexto.Grabar(honorarioModificado, honorarioOriginal);
            }
        }
        public void EliminarHonorario(int codigoHonorario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HONORARIOS_MEDICOS honorarioaBorrar = contexto.HONORARIOS_MEDICOS.FirstOrDefault(
                    h => h.HOM_CODIGO == codigoHonorario);
                contexto.Eliminar(honorarioaBorrar);
            }
        }
        
        //. metodo que recupera la lista por defecto de los honorarios medicos
        public static List<HONORARIOS_MEDICOS> RecuperarHonorariosMedicos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var honorario = from h in contexto.HONORARIOS_MEDICOS
                                select h;
                List<HONORARIOS_MEDICOS> honorarios = new List<HONORARIOS_MEDICOS>(); 
                honorarios = contexto.HONORARIOS_MEDICOS.Include("USUARIOS").Include("PAGOS_FAC_MEDICOS_DETALLE").Include("MEDICOS").ToList();
                
                return honorarios; 
            }
        }
        /// <summary>
        /// Metodo que filtra la informacion de Honorarios medicos atraves de un stored procedure
        /// </summary>
        /// <param name="forCodigo">Codigo Forma de pago</param>
        /// <param name="fechaInicio">Fecha de Factura Inicial</param>
        /// <param name="fechaFin">Fecha de Factura Final</param>
        /// <param name="canceladas">(Bool) Si las facturas fueron canceladas</param>
        /// <param name="pacCodigo">Codigo del paciente</param>
        /// <param name="pacGenero">Genero del paciente</param>
        /// <param name="pacFechaNacimiento">Fecha de nacimiento del paciente</param>
        /// <param name="medCodigo">Codigo del Medico</param>
        /// <param name="espCodigo">Codigo de la Especialidad</param>
        /// <param name="tihCodigo">Codigo de Tipo de  Honorario</param>
        /// <param name="timCodigo">Codigo de Tipo de Medico</param>
        /// <param name="medRecibeLlamada">(Bool) Si el medico Recibe llamada</param>
        /// <param name="ateReferido">(Bool) Si el paciente es referido</param>
        /// <param name="ate_fecha_alta">Fecha de alta del paciente</param>
        /// <returns>Retorna la lista de Honorarios Medicos de acuerdo al filtro</returns>
        public List<HONORARIOS_VISTA> RecuperarHonorariosMedicos(string tipo, string porRecuperar, string medCodigo, string espCodigo, string tihCodigo, string timCodigo, string medRecibeLlamada, string fechaIniFacturaMedico,
            string FechaFinFacturaMedico, string honorariosCancelados,string sinRetencion, string forCodigo,string tifCodigo, string lote, string numeroControl, string facturaClinica,
            string FechaIniFacturaCliente, string FechaFinFacturaCliente, string pacienteReferido, string pacienteFechaAlta,string ateCodigo, string pacCodigo)
        {
            //using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            //{
            //    //return contexto.ConsultarHonorariosMedicosFiltrados(forCodigo,fechaInicio,fechaFin,canceladas,pacCodigo,pacGenero,pacFechaNacimiento,
            //    //    medCodigo,espCodigo,tihCodigo,timCodigo,medRecibeLlamada,ateReferido,ate_fecha_alta).ToList() ;
            //    return contexto.ConsultarHonorariosMedicosVistaFiltrados(tipo,porRecuperar,medCodigo, espCodigo, tihCodigo, timCodigo, medRecibeLlamada, fechaIniFacturaMedico,
            //        FechaFinFacturaMedico, honorariosCancelados, sinRetencion,null, forCodigo, tifCodigo, lote, numeroControl, facturaClinica, FechaIniFacturaCliente,
            //        FechaFinFacturaCliente, pacienteReferido, pacienteFechaAlta,ateCodigo, pacCodigo, null).ToList();
            //}
            return null;
        }


        public DataTable VistaHonorariosMedicos(int med_codigo, DateTime fechaInicio, DateTime fechaFin, int for_codigo,
            int tif_codigo, string lote, string numControl)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();

            string sql = "SELECT TMO_NOMBRE,TMO_CODIGO,v.HOM_CODIGO,ATE_CODIGO,FOR_CODIGO,ID_USUARIO,MED_CODIGO,MED_NOMBRE_MEDICO,MED_RUC,\r\n" +
                "HOM_FACTURA_MEDICO,HOM_FACTURA_FECHA,PAC_NOMBRE_PACIENTE,ATE_NUMERO_CONTROL,ATE_FACTURA_PACIENTE,ATE_FACTURA_FECHA,despag,forpag,\r\n" +
                "FOR_DESCRIPCION,HOM_LOTE,HOM_FECHA_INGRESO,HOM_VALOR_NETO,RET_CODIGO1,HOM_RETENCION,HOM_COMISION_CLINICA,HOM_APORTE_LLAMADA,\r\n" +
                "HOM_VALOR_TOTAL,VALOR_POR_RECUPERAR,HOM_RECORTE,HOM_OBSERVACION,HOM_VALOR_PAGADO,HOM_VALOR_CANCELADO,VALOR_RECUPERADO,DIFERENCIA,\r\n" +
                "HON_FUERA,RESPONSABLE,REFERIDO,ISNULL(cg.numdoc,0)as 'NRO ASIENTO' FROM HONORARIOS_VISTA  V\r\n" +
                "LEFT join Cg3000..Cgcabmae CG on CG.HOM_CODIGO = v.HOM_CODIGO WHERE \r\n";
            string FiltroFechas = "HOM_FACTURA_FECHA BETWEEN '" + fechaInicio + "' AND '" + fechaFin + "'\r\n";
            string FiltroMedico = "MED_CODIGO = " + med_codigo;
            string FiltroPago = "forpag = " + for_codigo;
            sql = sql + FiltroFechas;
            if (med_codigo != 0)
                sql = sql + " AND " + FiltroMedico;
            if (for_codigo != 0)
            {
                sql = sql + " AND " + FiltroPago;
            }

            command = new SqlCommand(sql, connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;

        }
        /// <summary>
        /// Metodo que recupera la lista de Honorarios Pendientes de Cancelacion
        /// </summary>
        /// <returns>Retorna una lista de HONORARIOS_MEDICOS_TRANSFERENCIAS</returns>
        public List<HONORARIOS_MEDICOS_TRANSFERENCIAS> RecuperarHonorariosMedicosPorTransferir()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.HONORARIOS_MEDICOS_TRANSFERENCIAS.ToList();  
            }
        }
        /// <summary>
        /// Metodo que recupera la lista de Honorarios Pendientes de Cancelacion
        /// </summary>
        /// <param name="codigoMedico"> codigo del Medico</param>
        /// <returns>Retorna una lista de HONORARIOS_MEDICOS_TRANSFERENCIAS</returns>
        public List<HONORARIOS_MEDICOS_TRANSFERENCIAS> RecuperarHonorariosMedicosPorTransferir(string codigoMedico, string codTipoFormaPago, string codFormaPago, string fechaFacturaIni, string fechaFacturaFin)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    if (codigoMedico == null && codTipoFormaPago == null && fechaFacturaIni == null)
                    {
                        List<HONORARIOS_MEDICOS_TRANSFERENCIAS> lista = contexto.HONORARIOS_MEDICOS_TRANSFERENCIAS.OrderBy(h=>h.NOMBRE_MEDICO).ToList();
                        return lista;
                    }
                    else if (codigoMedico != null && codTipoFormaPago == null && fechaFacturaIni == null)
                    {
                        int codMedico = Convert.ToInt32(codigoMedico);
                        return contexto.HONORARIOS_MEDICOS_TRANSFERENCIAS.Where(h => h.MED_CODIGO == codMedico).ToList();
                    }
                    else if (codigoMedico == null && codTipoFormaPago != null && fechaFacturaIni == null)
                    {
                        
                        Int16 tipoFormaPago = Convert.ToInt16(codTipoFormaPago);

                        if (codFormaPago == null)
                        {
                            return (from p in contexto.HONORARIOS_MEDICOS_TRANSFERENCIAS
                                                    join f in contexto.FORMA_PAGO on p.FOR_CODIGO equals f.FOR_CODIGO
                                                    join t in contexto.TIPO_FORMA_PAGO on f.TIPO_FORMA_PAGO.TIF_CODIGO equals t.TIF_CODIGO
                                                    where  t.TIF_CODIGO == tipoFormaPago
                                                    orderby p.NOMBRE_MEDICO
                                                    select p).ToList();
                        }
                        else
                        {
                            Int16 formaPago = Convert.ToInt16(codFormaPago);
                            return contexto.HONORARIOS_MEDICOS_TRANSFERENCIAS.Where(p => p.FOR_CODIGO == formaPago).OrderBy(p => p.NOMBRE_MEDICO).ToList();
                        }

                    }

                    else if (codigoMedico != null && codTipoFormaPago != null && fechaFacturaIni == null)
                    {
                        int codMedico = Convert.ToInt32(codigoMedico);
                        Int16 tipoFormaPago = Convert.ToInt16(codTipoFormaPago);

                        if (codFormaPago == null)
                        {
                            return (from p in contexto.HONORARIOS_MEDICOS_TRANSFERENCIAS
                                    join f in contexto.FORMA_PAGO on p.FOR_CODIGO equals f.FOR_CODIGO
                                    join t in contexto.TIPO_FORMA_PAGO on f.TIPO_FORMA_PAGO.TIF_CODIGO equals t.TIF_CODIGO
                                    where p.MED_CODIGO ==codMedico && t.TIF_CODIGO == tipoFormaPago
                                    orderby p.NOMBRE_MEDICO
                                    select p).ToList();
                        }
                        else
                        {
                            Int16 formaPago = Convert.ToInt16(codFormaPago);
                            return contexto.HONORARIOS_MEDICOS_TRANSFERENCIAS.Where(p => p.MED_CODIGO == codMedico && p.FOR_CODIGO ==formaPago).OrderBy(p => p.NOMBRE_MEDICO).ToList();
                        }

                    }
                    else if (codigoMedico == null && codTipoFormaPago == null && fechaFacturaIni != null)
                    {
                        DateTime fechaIni = Convert.ToDateTime(fechaFacturaIni + " 00:00:00");
                        DateTime fechaFin = Convert.ToDateTime(fechaFacturaFin + " 23:59:59");  
                        return contexto.HONORARIOS_MEDICOS_TRANSFERENCIAS.Where(h => h.HOM_FACTURA_FECHA>=fechaIni && h.HOM_FACTURA_FECHA <=fechaFin ).ToList();
                    }
                    else if (codigoMedico == null && codTipoFormaPago != null && fechaFacturaIni != null)
                    {
                        DateTime fechaIni = Convert.ToDateTime(fechaFacturaIni + " 00:00:00");
                        DateTime fechaFin = Convert.ToDateTime(fechaFacturaFin + " 23:59:59");
                        Int16 tipoFormaPago = Convert.ToInt16(codTipoFormaPago);

                        if (codFormaPago == null)
                        {
                            return (from p in contexto.HONORARIOS_MEDICOS_TRANSFERENCIAS
                                    join f in contexto.FORMA_PAGO on p.FOR_CODIGO equals f.FOR_CODIGO
                                    join t in contexto.TIPO_FORMA_PAGO on f.TIPO_FORMA_PAGO.TIF_CODIGO equals t.TIF_CODIGO
                                    where p.HOM_FACTURA_FECHA >=fechaIni && p.HOM_FACTURA_FECHA <= fechaFin && t.TIF_CODIGO == tipoFormaPago
                                    orderby p.NOMBRE_MEDICO
                                    select p).ToList();
                        }
                        else
                        {
                            Int16 formaPago = Convert.ToInt16(codFormaPago);
                            return contexto.HONORARIOS_MEDICOS_TRANSFERENCIAS.Where(p => p.HOM_FACTURA_FECHA >= fechaIni && p.HOM_FACTURA_FECHA <= fechaFin && p.FOR_CODIGO == formaPago).OrderBy(p => p.NOMBRE_MEDICO).ToList();
                        }
                    }
                    else if (codigoMedico != null && codTipoFormaPago != null && fechaFacturaIni != null)
                    {
                        DateTime fechaIni = Convert.ToDateTime(fechaFacturaIni + " 00:00:00");
                        DateTime fechaFin = Convert.ToDateTime(fechaFacturaFin + " 23:59:59");
                        Int16 tipoFormaPago = Convert.ToInt16(codTipoFormaPago);
                        int codMedico = Convert.ToInt32(codigoMedico);

                        if (codFormaPago == null)
                        {
                            return (from p in contexto.HONORARIOS_MEDICOS_TRANSFERENCIAS
                                    join f in contexto.FORMA_PAGO on p.FOR_CODIGO equals f.FOR_CODIGO
                                    join t in contexto.TIPO_FORMA_PAGO on f.TIPO_FORMA_PAGO.TIF_CODIGO equals t.TIF_CODIGO
                                    where p.MED_CODIGO == codMedico && p.HOM_FACTURA_FECHA >= fechaIni && p.HOM_FACTURA_FECHA <= fechaFin && t.TIF_CODIGO == tipoFormaPago
                                    orderby p.NOMBRE_MEDICO
                                    select p).ToList();
                        }
                        else
                        {
                            Int16 formaPago = Convert.ToInt16(codFormaPago);
                            return contexto.HONORARIOS_MEDICOS_TRANSFERENCIAS.Where(p => p.HOM_FACTURA_FECHA >= fechaIni && p.HOM_FACTURA_FECHA <= fechaFin &&  p.MED_CODIGO == codMedico && p.FOR_CODIGO == formaPago).OrderBy(p => p.NOMBRE_MEDICO).ToList();
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception err)
            {
                Console.Write(err.Message);
                return null;
            }
        }

        /// <summary>
        /// Actualiza en la base de datos los cambios en el objeto honorarios
        /// </summary>
        /// <param name="honorarios">Instancia de HONORARIOS_MEDICOS para actualizarce</param>
        public void ActualizarHonorariosMedicos(HONORARIOS_MEDICOS honorarios)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HONORARIOS_MEDICOS honorariosDestino = contexto.HONORARIOS_MEDICOS.FirstOrDefault(h=>h.HOM_CODIGO ==honorarios.HOM_CODIGO);
                honorariosDestino.HOM_VALOR_PAGADO = honorarios.HOM_VALOR_PAGADO ;
                honorariosDestino.HOM_VALOR_CANCELADO = honorarios.HOM_VALOR_CANCELADO;
                honorariosDestino.HOM_RECORTE = honorarios.HOM_RECORTE;  
                contexto.SaveChanges(); 
            }
        }

        public void ActualizarValoresHonorariosMedicos(HONORARIOS_MEDICOS honorarios)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HONORARIOS_MEDICOS honorariosDestino = contexto.HONORARIOS_MEDICOS.FirstOrDefault(h => h.HOM_CODIGO == honorarios.HOM_CODIGO);
                honorariosDestino.HOM_APORTE_LLAMADA = honorarios.HOM_APORTE_LLAMADA;
                honorariosDestino.HOM_VALOR_TOTAL = honorarios.HOM_VALOR_TOTAL;
                contexto.SaveChanges();
            }
        }

        /// <summary>
        /// Devuelve una instancia de Honorarios Medicos
        /// </summary>
        /// <param name="codigo">codigo del honorario</param>
        /// <returns>objeto HONORARIOS_MEDICOS</returns>
        public HONORARIOS_MEDICOS RecuperaHonorariosMedicosID(Int64 codigo)
        {
            HONORARIOS_MEDICOS medico = new HONORARIOS_MEDICOS();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                 medico = (from p in contexto.HONORARIOS_MEDICOS
                                where p.HOM_CODIGO == codigo
                                select p).Single();

                return medico;
                
               //return  contexto.HONORARIOS_MEDICOS.Include("MEDICOS").FirstOrDefault(h=>h.HOM_CODIGO==codigo) ;
            }
        }

        /// <summary>
        /// lista de honorarios medicos que no tiene nota de credito o debito
        /// </summary>
        /// <returns></returns>
        public List<DtoHonorariosMedicos> ListaHonorariosNoutilizadosNotaDebito(string medCodigo)
        {
            List<HONORARIOS_VISTA> usuarios = new List<HONORARIOS_VISTA>();
            List<NOTAS_CREDITO_DEBITO> med = new List<NOTAS_CREDITO_DEBITO>();
            List<DtoHonorariosMedicos> dtousu = new List<DtoHonorariosMedicos>();

            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //usuarios = contexto.HONORARIOS_MEDICOS.Include("MEDICOS").ToList();
                ////med = contexto.HONORARIOS_MEDICOS.Include("MEDICOS")
                ////   .Join(contexto.NOTAS_DEBITO_MEDICOS, b => b.HOM_CODIGO, (NOTAS_DEBITO_MEDICOS a) => a.HONORARIOS_MEDICOS.HOM_CODIGO, (b, a) => b)
                ////   .ToList();
                //med = contexto.NOTAS_CREDITO_DEBITO.Where(an=>an.NOT_ANULADO==false).ToList();
                usuarios = new DatHonorariosMedicos().RecuperarHonorariosMedicosSinNotaDebitoPorVNC(medCodigo, null, null, null, null, null,
            null, null, null, null, null, null, null,
            null, null, null, null, null, null);
                foreach (var acceso in usuarios)
                {
                    //bool valor = true;
                    if (med.Where(per => per.NOT_DOCUMENTO_AFECTADO == acceso.HOM_FACTURA_MEDICO).FirstOrDefault() == null)
                        dtousu.Add(new DtoHonorariosMedicos() { MED_CODIGO=acceso.MED_CODIGO==null?int.Parse("0"):int.Parse(acceso.MED_CODIGO.ToString()), HOM_CODIGO=acceso.HOM_CODIGO,
                            MED_NOMBRE=acceso.MED_NOMBRE_MEDICO, HOM_FACTURA_MEDICO=acceso.HOM_FACTURA_MEDICO, HOM_VALOR_NETO= acceso.HOM_VALOR_NETO, HOM_VALOR_TOTAL = Convert.ToDecimal(acceso.HOM_VALOR_TOTAL) });
                }

            }
            return dtousu;
        }
        /// <summary>
        /// metodo para recuperar facturas sin retencion
        /// </summary>
        /// <returns></returns>
        public List<DtoRetencionesAutomaticas> HonorariossinRetencion()
        {
            List<DtoRetencionesAutomaticas> datos = new List<DtoRetencionesAutomaticas>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                //List<HONORARIOS_MEDICOS> honorarios = contexto.HONORARIOS_MEDICOS.Include("MEDICOS").Include("MEDICOS.RETENCIONES_FUENTE").Where(cod=>cod.RET_CODIGO1==null).ToList();
                //List<DtoRetencionesAutomaticas> retenciones = new List<DtoRetencionesAutomaticas>();
                var retenciones = from m in contexto.MEDICOS
                                  join h in contexto.HONORARIOS_MEDICOS on m.MED_CODIGO equals h.MEDICOS.MED_CODIGO
                                  join r in contexto.RETENCIONES_FUENTE on m.RETENCIONES_FUENTE.RET_CODIGO equals r.RET_CODIGO
                                  where h.RET_CODIGO1 == null
                                  select new
                                  {
                                      h.HOM_CODIGO,
                                      m.MED_CODIGO,
                                      SUJETO_RETENCION = m.MED_APELLIDO_MATERNO + " " + m.MED_NOMBRE1,
                                      m.MED_RUC,
                                      h.HOM_FACTURA_MEDICO,
                                      h.HOM_FACTURA_FECHA,
                                      h.HOM_VALOR_NETO,
                                      r.RET_PORCENTAJE,
                                      VALOR_RETENCION = r.RET_PORCENTAJE * h.HOM_VALOR_NETO,
                                      CONRETENCION = false,
                                      h.RET_CODIGO1
                                  };

                foreach (var acceso in retenciones)
                {
                    bool valor = false;
                    datos.Add(new DtoRetencionesAutomaticas() { CONRETENCION = valor, HOM_CODIGO = acceso.HOM_CODIGO, HOM_FACTURA_MEDICO = acceso.HOM_FACTURA_MEDICO, HOM_VALOR_NETO = acceso.HOM_VALOR_NETO, MED_RUC = acceso.MED_RUC, RET_PORCENTAJE = acceso.RET_PORCENTAJE, SUJETO_RETENCION = acceso.SUJETO_RETENCION, MED_CODIGO = acceso.MED_CODIGO, HOM_FACTURA_FECHA = acceso.HOM_FACTURA_FECHA.Value, VALOR_RETENCION = acceso.VALOR_RETENCION });
                }

            }
            return datos;
        }
        public List<DtoHonorariosNotasDebito> ListaHonorariosPagosMenores()
        {
            List<DtoHonorariosNotasDebito> datos = new List<DtoHonorariosNotasDebito>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                List<HONORARIOS_VISTA> honorarios = new DatHonorariosMedicos().RecuperarHonorariosMedicosSinNotaDebitoPorVNC(null, null, null, null, null, null,
            null, null, null, null, null, null, null,
            null, null, null, null, null, null);
                //var medicos = contexto.MEDICOS.ToList();
//                    contexto.HONORARIOS_MEDICOS.Include("MEDICOS").Where(cod => cod.HOM_VALOR_PAGADO != 0).ToList();
               // List<NOTAS_CREDITO_DEBITO> notas = contexto.NOTAS_CREDITO_DEBITO.Include("TIPO_DOCUMENTO").Where(cod => cod.TIPO_DOCUMENTO.TID_CODIGO == 3).Where(an=>an.NOT_ANULADO==false || an.NOT_ANULADO==null).ToList();
                foreach (var acceso in honorarios)
                {
                    bool valor = false;
                    //var med = medicos.Where(cod => cod.MED_CODIGO == acceso.MED_CODIGO).FirstOrDefault();
                    //if (notas.Where(per => per.NOT_RUC == acceso.MEDICOS.MED_RUC).Where(fac=>fac.NOT_DOCUMENTO_AFECTADO==acceso.HOM_FACTURA_MEDICO).FirstOrDefault() == null)
                    datos.Add(new DtoHonorariosNotasDebito() { CONNOTAD = valor, HOM_CODIGO = acceso.HOM_CODIGO,MED_CODIGO=acceso.MED_CODIGO==null?int.Parse("0"):int.Parse(acceso.MED_CODIGO.ToString()),HOM_FACTURA_FECHA=acceso.HOM_FACTURA_FECHA==null?DateTime.Parse("01/01/0001"):DateTime.Parse(acceso.HOM_FACTURA_FECHA.ToString()), HOM_FACTURA_MEDICO = acceso.HOM_FACTURA_MEDICO, DIFERENCIA=acceso.HOM_VALOR_NETO-(acceso.HOM_VALOR_PAGADO==null? decimal.Parse("0"):decimal.Parse(acceso.HOM_VALOR_PAGADO.ToString())), HOM_VALOR_NETO = acceso.HOM_VALOR_NETO, MED_RUC = acceso.MED_RUC,  NOT_RAZON_SOCIAL = acceso.MED_NOMBRE_MEDICO, HOM_VALOR_PAGADO = acceso.HOM_VALOR_PAGADO==null? decimal.Parse("0") : decimal.Parse(acceso.HOM_VALOR_PAGADO.ToString()) });
                }
            }

            return datos;
        }
        public List<DtoHonorariosNotasDebito> ListaHonorariosSinNDComisionesAportes()
        {
            List<DtoHonorariosNotasDebito> datos = new List<DtoHonorariosNotasDebito>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                List<HONORARIOS_VISTA> honorarios =new DatHonorariosMedicos().RecuperarHonorariosMedicosSinNotaDebitoPorComisiones(null, null, null, null, null, null,
            null, null, null, null, null, null, null,
            null, null, null, null, null, null);
                    //contexto.HONORARIOS_MEDICOS.Include("MEDICOS").ToList();
                //var medicos = contexto.MEDICOS.ToList();
                List<NOTAS_CREDITO_DEBITO> notas = contexto.NOTAS_CREDITO_DEBITO.Include("TIPO_DOCUMENTO").Where(cod => cod.TIPO_DOCUMENTO.TID_CODIGO == HonorariosPAR.codigoTipoDocumentoNotaDebito  && cod.TNO_CODIGO == HonorariosPAR.codigoTipoNotaDebitoComisionesYreferidos).Where(an => an.NOT_ANULADO == false || an.NOT_ANULADO == null).ToList();
                foreach (var acceso in honorarios)
                {
                    bool valor = false;
                    //var med = medicos.Where(cod => cod.MED_CODIGO == acceso.MED_CODIGO).FirstOrDefault();
                    //if (notas.Where(per => per.NOT_RUC == acceso.MEDICOS.MED_RUC).Where(fac => fac.NOT_DOCUMENTO_AFECTADO == acceso.HOM_FACTURA_MEDICO).FirstOrDefault() == null)

                    datos.Add(new DtoHonorariosNotasDebito() { CONNOTAD = valor, HOM_CODIGO = acceso.HOM_CODIGO, MED_CODIGO = acceso.MED_CODIGO==null?int.Parse("0"):int.Parse(acceso.MED_CODIGO.ToString()), HOM_FACTURA_FECHA = acceso.HOM_FACTURA_FECHA == null ? DateTime.Parse("01/01/0001") : DateTime.Parse(acceso.HOM_FACTURA_FECHA.ToString()), HOM_FACTURA_MEDICO = acceso.HOM_FACTURA_MEDICO, DIFERENCIA = (acceso.HOM_COMISION_CLINICA == null ? decimal.Parse("0") : decimal.Parse(acceso.HOM_COMISION_CLINICA.ToString())) + (acceso.HOM_APORTE_LLAMADA == null ? decimal.Parse("0") : decimal.Parse(acceso.HOM_APORTE_LLAMADA.ToString())), HOM_COMISION_CLINICA = acceso.HOM_COMISION_CLINICA==null?decimal.Parse("0"):decimal.Parse(acceso.HOM_COMISION_CLINICA.ToString()),HOM_APORTE_LLAMADA=acceso.HOM_APORTE_LLAMADA==null?decimal.Parse("0"):decimal.Parse(acceso.HOM_APORTE_LLAMADA.ToString()), MED_RUC = acceso.MED_RUC, NOT_RAZON_SOCIAL = acceso.MED_NOMBRE_MEDICO, HOM_VALOR_PAGADO = acceso.HOM_VALOR_PAGADO == null ? decimal.Parse("0") : decimal.Parse(acceso.HOM_VALOR_PAGADO.ToString()) });
                }
            }

            return datos;
        }

        public bool ActualizarValores(Int64 codigo)
        { 
            using(var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return true; 
            }
        }
        public List<HONORARIOS_VISTA> RecuperarHonorariosMedicosSinNotaDebitoPorVNC(string medCodigo, string espCodigo, string tihCodigo, string timCodigo, string medRecibeLlamada, string fechaIniFacturaMedico,
            string FechaFinFacturaMedico, string honorariosCancelados, string sinRetencion, string forCodigo, string lote, string numeroControl, string facturaClinica,
            string FechaIniFacturaCliente, string FechaFinFacturaCliente, string pacienteReferido, string pacienteFechaAlta, string ateCodigo, string pacCodigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //return contexto.ConsultarHonorariosMedicosFiltrados(forCodigo,fechaInicio,fechaFin,canceladas,pacCodigo,pacGenero,pacFechaNacimiento,
                //    medCodigo,espCodigo,tihCodigo,timCodigo,medRecibeLlamada,ateReferido,ate_fecha_alta).ToList() ;
                return contexto.ConsultarHonorariosSinNotaDebitoPorVNC(medCodigo, espCodigo, tihCodigo, timCodigo, medRecibeLlamada, fechaIniFacturaMedico,
                    FechaFinFacturaMedico, honorariosCancelados, sinRetencion, forCodigo, lote, numeroControl, facturaClinica, FechaIniFacturaCliente,
                    FechaFinFacturaCliente, pacienteReferido, pacienteFechaAlta, ateCodigo, pacCodigo).ToList();
                //return contexto.ConsultarHonorariosSinNotaDebitoPorVNC(Convert.ToInt32(medCodigo)).ToList();
            }
        }
        public List<HONORARIOS_VISTA> RecuperarHonorariosMedicosSinNotaDebitoPorComisiones(string medCodigo, string espCodigo, string tihCodigo, string timCodigo, string medRecibeLlamada, string fechaIniFacturaMedico,
            string FechaFinFacturaMedico, string honorariosCancelados, string sinRetencion, string forCodigo, string lote, string numeroControl, string facturaClinica,
            string FechaIniFacturaCliente, string FechaFinFacturaCliente, string pacienteReferido, string pacienteFechaAlta, string ateCodigo, string pacCodigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //return contexto.ConsultarHonorariosMedicosFiltrados(forCodigo,fechaInicio,fechaFin,canceladas,pacCodigo,pacGenero,pacFechaNacimiento,
                //    medCodigo,espCodigo,tihCodigo,timCodigo,medRecibeLlamada,ateReferido,ate_fecha_alta).ToList() ;
                return contexto.ConsultarHonorariosSinNotaDebitoPorComisiones(medCodigo, espCodigo, tihCodigo, timCodigo, medRecibeLlamada, fechaIniFacturaMedico,
                    FechaFinFacturaMedico, honorariosCancelados, sinRetencion, forCodigo, lote, numeroControl, facturaClinica, FechaIniFacturaCliente,
                    FechaFinFacturaCliente, pacienteReferido, pacienteFechaAlta, ateCodigo, pacCodigo).ToList();
            }
        }
        public Int32 ultimoCodigoHonorarios()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    Int32 maximo = contexto.HONORARIOS_MEDICOS.Max(h=>h.HOM_CODIGO);
                    if (maximo > 0)
                        return (int)maximo;
                    else
                        return 0;

                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<DtoHonorariosMedicos> listaHonorariosAtencion(int codAtencion, int estado)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var honorarios = (from h in contexto.HONORARIOS_MEDICOS
                                  join a in contexto.ATENCIONES on h.ATE_CODIGO equals a.ATE_CODIGO
                                  join m in contexto.MEDICOS on h.MEDICOS.MED_CODIGO equals m.MED_CODIGO
                                  join f in contexto.FORMA_PAGO on h.FOR_CODIGO equals f.FOR_CODIGO
                                  where h.ATE_CODIGO==codAtencion && (h.TMO_CODIGO == 201 || h.TMO_CODIGO ==202)
                                  orderby h.HOM_FACTURA_FECHA
                                  select new
                                  {
                                      h.HOM_CODIGO,
                                      h.MEDICOS.MED_CODIGO,
                                      h.MEDICOS.MED_APELLIDO_PATERNO,
                                      h.MEDICOS.MED_APELLIDO_MATERNO,
                                      h.MEDICOS.MED_NOMBRE1,
                                      h.MEDICOS.MED_NOMBRE2,
                                      h.HOM_FACTURA_MEDICO,
                                      h.HOM_VALE,
                                      h.HOM_FACTURA_FECHA,
                                      h.FOR_CODIGO,
                                      f.FOR_DESCRIPCION,
                                      h.HOM_VALOR_NETO,
                                      h.HOM_RETENCION,
                                      h.HOM_APORTE_LLAMADA,
                                      h.HOM_COMISION_CLINICA,
                                      h.HOM_OBSERVACION,
                                      h.HOM_VALOR_TOTAL,
                                  }).ToList();

                List<DtoHonorariosMedicos> gridHonorarios = new List<DtoHonorariosMedicos>();
                    
                    foreach (var acceso in honorarios)
                    {
                    gridHonorarios.Add(new DtoHonorariosMedicos
                    {
                        HOM_CODIGO = acceso.HOM_CODIGO,
                        MED_CODIGO = acceso.MED_CODIGO,
                        MED_NOMBRE = acceso.MED_APELLIDO_PATERNO + " " + acceso.MED_APELLIDO_MATERNO + " " + acceso.MED_NOMBRE1 + " " + acceso.MED_NOMBRE2,
                        HOM_FACTURA_MEDICO = acceso.HOM_FACTURA_MEDICO,
                        HOM_VALE = acceso.HOM_VALE,
                        HOM_FACTURA_FECHA = Convert.ToDateTime(acceso.HOM_FACTURA_FECHA),
                        FORMAPAGO = HonorarioFormaPago((Int16)acceso.FOR_CODIGO),
                        FOR_CODIGO = (Int16)acceso.FOR_CODIGO,
                        FOR_DESCRIPCION = acceso.FOR_DESCRIPCION,
                        HOM_VALOR_NETO = acceso.HOM_VALOR_NETO,
                        HOM_RETENCION = Convert.ToDecimal(acceso.HOM_RETENCION),
                        HOM_APORTE_LLAMADA = Convert.ToDecimal(acceso.HOM_APORTE_LLAMADA),
                        HOM_COMISION_CLINICA = Convert.ToDecimal(acceso.HOM_COMISION_CLINICA),
                        HOM_VALOR_TOTAL = Convert.ToDecimal(acceso.HOM_VALOR_TOTAL),
                        HOM_OBSERVACION = acceso.HOM_OBSERVACION,
                        HOM_FUERA = HonorariosPorFuera(acceso.HOM_CODIGO)
                        }); 
                    }


                return gridHonorarios;
            }
        }

        public bool existeCabmael(Int64 codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_existeCgcabmae", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@HOM_CODIGO ", codigo);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            
        }
        public void cambiarEstadoHOMdatos(Int64 codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_cambiarEstadoHOMdatos", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@HOM_CODIGO ", codigo);

            reader = command.ExecuteReader();
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
        }
        public bool existeContabilidad(Int64 codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_existeCgContabilidad", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@HOM_CODIGO ", codigo);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();

        }
        public bool HonorariosPorFuera(int hon_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool PorFuera = false;

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HonorarioPorFuera", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hom_codigo", hon_codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                PorFuera = Convert.ToBoolean(reader["HON_FUERA"].ToString());
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return PorFuera;
        }

        public string HonorarioFormaPago(Int16 forcodigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            string formapago = "";

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HFomasPago", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@forcodigo", forcodigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                formapago = reader["despag"].ToString();
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return formapago;
        }
        public DataTable DatosRecuperaHonorarios(int Codigo_doc)
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

            Sqlcmd = new SqlCommand("sp_recuperahonorarios", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@pam_codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@pam_codigo"].Value = (Codigo_doc);


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }


        public DataTable RecuperaFichaMedico(int CodigoMedico)
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

            Sqlcmd = new SqlCommand("sp_FichaMedico", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoMedico", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoMedico"].Value = (CodigoMedico);


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }


       public string NumVale()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataReader Sqldap;
            string valor = "";
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

            Sqlcmd = new SqlCommand("select PAD_VALOR from PARAMETROS_DETALLE where PAD_CODIGO = 30", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = Sqlcmd.ExecuteReader();
            while (Sqldap.Read())
            {
                valor = Sqldap["PAD_VALOR"].ToString();
            }
            Sqldap.Close();
            Sqlcon.Close();
            return valor;
        }

        public void IncrementaNumVale(string valor)
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

            Sqlcmd = new SqlCommand("update PARAMETROS_DETALLE set PAD_VALOR = @valor where PAD_CODIGO = 30", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.Parameters.AddWithValue("@valor", valor);
            Sqlcmd.ExecuteNonQuery();
            Sqlcmd.Parameters.Clear();
            Sqlcon.Close();
        }

     public void saveHMDatosAdicionales(int HOMCODIGO, string FecCaducidad, int HonFuera, string autSRI, string caja, decimal cubierto, decimal exceso)
        {
            string cadena_sql;
            
                cadena_sql = "delete from [dbo].[HONORARIOS_MEDICOS_DATOSADICIONALES] where [HOM_CODIGO]=" + HOMCODIGO + "  \n" +
                               "INSERT INTO [dbo].[HONORARIOS_MEDICOS_DATOSADICIONALES] ([HOM_CODIGO] ,[FEC_CAD_FACTURA] ,[HON_FUERA],AUTORIZACION_SRI, caja, HON_CUBIERTO, HON_EXCESO) VALUES\n" +
                               "(" + HOMCODIGO  + "" +
                               ",'" + FecCaducidad + "'" +
                               ", " + HonFuera + ",'" + autSRI + "','" + caja + "', " + cubierto + ", " + exceso + ")";
            
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
                throw ex;
                //Console.WriteLine(ex.Message);
            }

        }

        public void saveHMDatosAdicionales1(int HOMCODIGO, string FecCaducidad, int HonFuera, string autSRI, string caja, string numrec, decimal cubierto, decimal exceso)
        {
            string cadena_sql;

            //cadena_sql = "delete from [dbo].[HONORARIOS_MEDICOS_DATOSADICIONALES] where [HOM_CODIGO]=" + HOMCODIGO + "  \n" +
            //               "INSERT INTO [dbo].[HONORARIOS_MEDICOS_DATOSADICIONALES] ([HOM_CODIGO] ,[FEC_CAD_FACTURA] ,[HON_FUERA],AUTORIZACION_SRI, caja, numrec, HON_CUBIERTO, HON_EXCESO) VALUES\n" +
            //               "(" + HOMCODIGO + "" +
            //               ",'" + FecCaducidad + "'" +
            //               ", " + HonFuera + ",'" + autSRI + "','" + caja + "', '" + numrec + "', " + cubierto + ", "+ exceso + ")";

            //BaseContextoDatos obj = new BaseContextoDatos();
            //SqlConnection Sqlcon = obj.ConectarBd();
            //Sqlcon.Open();
            //SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            //try
            //{
            //    Sqlcmd.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //    //Console.WriteLine(ex.Message);
            //}
            cadena_sql = "IF NOT EXISTS (SELECT * FROM HONORARIOS_MEDICOS_DATOSADICIONALES WHERE HOM_CODIGO = @hom_codigo) "
            + "BEGIN "
            + "INSERT INTO [dbo].[HONORARIOS_MEDICOS_DATOSADICIONALES] ([HOM_CODIGO] ,[FEC_CAD_FACTURA] ,[HON_FUERA],AUTORIZACION_SRI, caja, numrec, HON_CUBIERTO, HON_EXCESO) VALUES\n" +
                           "(" + HOMCODIGO + "" +
                           ",'" + FecCaducidad + "'" +
                           ", " + HonFuera + ",'" + autSRI + "','" + caja + "', '" + numrec + "', " + cubierto + ", " + exceso + ")" 
            + " END" 
            + " ELSE "
            + " BEGIN "

            + " UPDATE HONORARIOS_MEDICOS_DATOSADICIONALES SET FEC_CAD_FACTURA = @fechaCaduca, "
            + " AUTORIZACION_SRI = @autorizacion, numrec = @numrec, HON_CUBIERTO = @cubierto, "
            + "HON_EXCESO = @exceso where HOM_CODIGO = @hom_codigo "
            + "END ";

            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);
            Sqlcmd.Parameters.AddWithValue("@fechaCaduca", FecCaducidad);
            Sqlcmd.Parameters.AddWithValue("@autorizacion", autSRI);
            Sqlcmd.Parameters.AddWithValue("@numrec", numrec);
            Sqlcmd.Parameters.AddWithValue("@cubierto", cubierto);
            Sqlcmd.Parameters.AddWithValue("@exceso", exceso);
            Sqlcmd.Parameters.AddWithValue("@hom_codigo", HOMCODIGO);

            try
            {
                Sqlcmd.ExecuteNonQuery();
                Sqlcmd.Parameters.Clear();
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
                //Console.WriteLine(ex.Message);
            }

        }
        public void saveHMDatosAdicionales2(int HOMCODIGO, string FecCaducidad, int HonFuera, string autSRI, string caja, string numrec, decimal cubierto, decimal exceso)
        {
            string cadena_sql;

            //cadena_sql = "delete from [dbo].[HONORARIOS_MEDICOS_DATOSADICIONALES] where [HOM_CODIGO]=" + HOMCODIGO + "  \n" +
            //               "INSERT INTO [dbo].[HONORARIOS_MEDICOS_DATOSADICIONALES] ([HOM_CODIGO] ,[FEC_CAD_FACTURA] ,[HON_FUERA],AUTORIZACION_SRI, numrec, HON_CUBIERTO, HON_EXCESO) VALUES\n" +
            //               "(" + HOMCODIGO + "" +
            //               ",'" + FecCaducidad + "'" +
            //               ", " + HonFuera + ",'" + autSRI + "', '" + numrec + "', " + cubierto + ", " + exceso + ")";

            cadena_sql = "UPDATE HONORARIOS_MEDICOS_DATOSADICIONALES SET FEC_CAD_FACTURA = @fechaCaduca, AUTORIZACION_SRI = @autorizacion, numrec = @numrec, "
            + "HON_CUBIERTO = @cubierto, HON_EXCESO = @exceso where HOM_CODIGO = @hom_codigo";

            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);
            Sqlcmd.Parameters.AddWithValue("@fechaCaduca", FecCaducidad);
            Sqlcmd.Parameters.AddWithValue("@autorizacion", autSRI);
            Sqlcmd.Parameters.AddWithValue("@numrec", numrec);
            Sqlcmd.Parameters.AddWithValue("@cubierto", cubierto);
            Sqlcmd.Parameters.AddWithValue("@exceso", exceso);
            Sqlcmd.Parameters.AddWithValue("@hom_codigo", HOMCODIGO);

            try
            {
                Sqlcmd.ExecuteNonQuery();
                Sqlcmd.Parameters.Clear();
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
                //Console.WriteLine(ex.Message);
            }

        }

        public void deleteHMDatosAdicionales(int HOMCODIGO)
        {
            string cadena_sql;

            cadena_sql = "delete from [dbo].[HONORARIOS_MEDICOS_DATOSADICIONALES] where [HOM_CODIGO]=" + HOMCODIGO + " ";

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

        public bool DatosRecuperaFacturasMedicos(int codMedico, string factura)
        {
            try
            {
                HONORARIOS_MEDICOS obj = new HONORARIOS_MEDICOS();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    obj = (from d in contexto.HONORARIOS_MEDICOS
                           where d.MEDICOS.MED_CODIGO == codMedico && d.HOM_FACTURA_MEDICO == factura
                           select d).FirstOrDefault();
                    if (obj == null)
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception ex)
            {
                return true;
                throw ex;
            }
        }

        public bool existHMDatosAdicionales(int HOMCODIGO)
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

            Sqlcmd = new SqlCommand("SELECT COUNT(*)  from[dbo].[HONORARIOS_MEDICOS_DATOSADICIONALES] where[HOM_CODIGO] = " + HOMCODIGO + "  " , Sqlcon);
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

       


        public DataTable getHMDatosAdicionales(int HOMCODIGO)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            int num = 0;
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
            Sqlcmd = new SqlCommand("SELECT *  from[dbo].[HONORARIOS_MEDICOS_DATOSADICIONALES] where[HOM_CODIGO] = " + HOMCODIGO + "  ", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }
        
        public DataTable FPHonorarios(int for_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_CargarFPHonorario", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@for_codigo", for_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

        public DataTable ConsultaAsiento(string facturaMedico, Int32 codMedico)
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

            Sqlcmd = new SqlCommand("SELECT TOP 1 concat(tipdoc, cast(numdoc as numeric)), fechatran, numdoc FROM [Cg3000].[dbo].[CgDETmae] WHERE codigo_c = (select MED_CODIGO_MEDICO from medicos where MED_CODIGO=" + codMedico + ") AND nocomp = '" + facturaMedico +"'", Sqlcon);
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

        public DataTable ConsultaRetencion(Int64 numdoc)
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

            Sqlcmd = new SqlCommand("SELECT CodComp, Fecha FROM Cg3000..CgRetenciones WHERE DesComp='IVA' AND CodCompEg=" + numdoc, Sqlcon);
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

        public bool EsOtraFormaPago(Int64 ate_codigo, int for_cod, int med_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool for_codigo = false;

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_EsOtraFormaPago", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@med_codigo", med_codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (Convert.ToInt32(reader["FOR_CODIGO"].ToString()) == for_cod)
                    for_codigo = true;
                else
                    for_codigo = false;
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return for_codigo;
        }

        public bool FpAnticipo(Int64 ate_codigo, int med_codigo, string numrec) //valido con la atencion, el codigo del medico y el codigo del anticipo
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var honorarioExiste = (from h in db.HONORARIOS_MEDICOS
                                       join hd in db.HONORARIOS_MEDICOS_DATOSADICIONALES on h.HOM_CODIGO equals hd.HOM_CODIGO
                                       where h.ATE_CODIGO == ate_codigo && h.MEDICOS.MED_CODIGO == med_codigo && hd.numrec == numrec
                                       select new
                                       {
                                           h,
                                           hd
                                       }).FirstOrDefault();
                if (honorarioExiste != null)
                    return true;
                else
                    return false;
            } 
        }
        public bool FpTarjeta(Int64 ate_codigo, int med_codigo, string lote)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var HExiste = (from h in db.HONORARIOS_MEDICOS
                               join hd in db.HONORARIOS_MEDICOS_DATOSADICIONALES on h.HOM_CODIGO equals hd.HOM_CODIGO
                               where h.ATE_CODIGO == ate_codigo && h.MEDICOS.MED_CODIGO == med_codigo && h.HOM_LOTE == lote
                               select new
                               {
                                   h,
                                   hd
                               }).FirstOrDefault();
                if (HExiste != null)
                    return true;
                else
                    return false;
            }
        }
        public void HonorarioAnticipoSic(int valida, double valorAnticipo, string numrec)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HonorarioAnticipo", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@valido", valida);
            command.Parameters.AddWithValue("@valorAnticipo", valorAnticipo);
            command.Parameters.AddWithValue("@numrec", numrec);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
        public double ValorAnticipo(string numrec)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            double monto = 0;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_HonorarioValorAnticipo", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@numrec", numrec);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                monto = Convert.ToDouble(reader["monto"].ToString());
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return monto;
        }
        public int HonorarioMedico(int hom_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            int med_codigo = 0;
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select MED_CODIGO from HONORARIOS_MEDICOS WHERE HOM_CODIGO = @hom_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@hom_codigo", hom_codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                med_codigo = Convert.ToInt32(reader["MED_CODIGO"].ToString());
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return med_codigo;
        }
        public void ReponerAnticipoSic(string numrec, double monto)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ReponerAnticipoSic", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@numrec", numrec);
            command.Parameters.AddWithValue("@monto", monto);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }

        public string RecuperarNUMREC(int hom_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            string numrec = "";

            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("select numrec from HONORARIOS_MEDICOS HM INNER JOIN HONORARIOS_MEDICOS_DATOSADICIONALES HMD ON HM.HOM_CODIGO = HMD.HOM_CODIGO WHERE HM.HOM_CODIGO = @hom_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@hom_codigo", hom_codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                numrec = reader["numrec"].ToString();
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return numrec;
        }
        public void ActualizarHonorario(HONORARIOS_MEDICOS honorario, int med_codigo, DateTime fechaCaduca, decimal cubierto, string autorizacion, decimal exceso)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HonorarioActualizar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hom_codigo", honorario.HOM_CODIGO);
            command.Parameters.AddWithValue("@med_codigo", med_codigo);
            command.Parameters.AddWithValue("@for_codigo", honorario.FOR_CODIGO);
            command.Parameters.AddWithValue("@tmo_codigo", honorario.TMO_CODIGO);
            command.Parameters.AddWithValue("@factura", honorario.HOM_FACTURA_MEDICO);
            command.Parameters.AddWithValue("@fechaFactura", honorario.HOM_FACTURA_FECHA);
            command.Parameters.AddWithValue("@valorNeto", honorario.HOM_VALOR_NETO);
            command.Parameters.AddWithValue("@comision", honorario.HOM_COMISION_CLINICA);
            command.Parameters.AddWithValue("@aporte", honorario.HOM_APORTE_LLAMADA);
            command.Parameters.AddWithValue("@retencion", honorario.HOM_RETENCION);
            command.Parameters.AddWithValue("@Pagado", honorario.HOM_VALOR_PAGADO);
            command.Parameters.AddWithValue("@recorte", honorario.HOM_RECORTE);
            command.Parameters.AddWithValue("@cancelado", honorario.HOM_VALOR_CANCELADO);
            command.Parameters.AddWithValue("@lote", honorario.HOM_LOTE);
            command.Parameters.AddWithValue("@observacion", honorario.HOM_OBSERVACION);
            command.Parameters.AddWithValue("@vale", honorario.HOM_VALE);
            command.Parameters.AddWithValue("@fechaCaduca", fechaCaduca);
            command.Parameters.AddWithValue("@cubierto", cubierto);
            command.Parameters.AddWithValue("@autorizacion", autorizacion);
            command.Parameters.AddWithValue("@exceso", exceso);

            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
        public DataTable HMDatosAdicionales(int hom_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HMDatosAdicionales", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hom_codigo", hom_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

        public DataTable DatosReporte(Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HonorariosDetalleReporte", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

        public DataTable HonoFormasPago()
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select * from Sic3000..Clasificacion where cartera = 1 order by 2 asc", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }
        public DataTable HonoDifierePago(string claspag)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select * from Sic3000..Forma_Pago where claspag = @claspag and ActivarFacturaHis = 1 order by 2 asc", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@claspag", claspag);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }
        public DataTable getAgrupados(Int64 ate_codigo, string facturaMedico)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_AsientoAgrupado", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@facturaMedico", facturaMedico);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public DataTable HMDentroPago(string numfac)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HMDentroPaciente", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@numfac", numfac);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public DataTable ValidaCerrar(Int64 ate_codigo)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HonorarioValidaCerrado", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public void HonorariosCerrar(Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HonorariosCerrar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }

        public void CambioFactura(Int64 ate_codigo, string estado, string anterior, string nueva, string credito, int usuario, string observacion)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HM_FacturaNueva", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@estado", estado);
            command.Parameters.AddWithValue("@anterior", anterior);
            command.Parameters.AddWithValue("@nueva", nueva);
            command.Parameters.AddWithValue("@credito", credito);
            command.Parameters.AddWithValue("@usuario", usuario);
            command.Parameters.AddWithValue("@observacion", observacion);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }

        public DataTable FacturasAnuladas()
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_FacturaAnuladas", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public DataTable FacturaEstado(string numfac)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HM_EstadoFactura", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@numfac", numfac);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public DataTable FacturaValida(string numfac)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HM_ValidarFacturaActiva", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@numfac", numfac);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public DataTable PacientesFac_Anulado()
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_PacuentesFac_Anuladas", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public DataTable ValidaFacturasAnulas(string numfac)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_PacienteFacturasAnuladas", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@numfac", numfac);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }
        public DataTable FacturaExisteAnulada(string numfac)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HM_FacturaAnuladaRepetida", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@numfac", numfac);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public DataTable FiltroPacientes(string nombre)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HM_Pacientes", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@nombre", nombre);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public void CreaHonorarioAuditoria(HONORARIOS_MEDICOS HM, bool fuera, bool asiento, string estado, string caja, double cubierto, Int64 med_codigo)
        {
            HONORARIOS_MEDICOS honorarios = new HONORARIOS_MEDICOS();

            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                honorarios = (from hm in db.HONORARIOS_MEDICOS
                              where hm.HOM_CODIGO == HM.HOM_CODIGO
                              select hm).FirstOrDefault();
            }

            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HM_CreaAuditoria", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@hom_codigo", HM.HOM_CODIGO);
            command.Parameters.AddWithValue("@med_codigo", med_codigo);
            command.Parameters.AddWithValue("@ate_codigo", HM.ATE_CODIGO);
            command.Parameters.AddWithValue("@for_codigo", HM.FOR_CODIGO);
            command.Parameters.AddWithValue("@tmo_codigo", HM.TMO_CODIGO);
            command.Parameters.AddWithValue("@usuario", His.Entidades.Clases.Sesion.codUsuario);
            command.Parameters.AddWithValue("@hom_fecha", DateTime.Now);
            command.Parameters.AddWithValue("@factura", HM.HOM_FACTURA_MEDICO);
            command.Parameters.AddWithValue("@fecha_factura", HM.HOM_FACTURA_FECHA);
            command.Parameters.AddWithValue("@valor_neto", HM.HOM_VALOR_NETO);
            command.Parameters.AddWithValue("@comision", HM.HOM_COMISION_CLINICA);
            command.Parameters.AddWithValue("@aporte", HM.HOM_APORTE_LLAMADA);
            command.Parameters.AddWithValue("@retencion", HM.HOM_RETENCION);
            if(HM.HOM_POR_PAGAR != null)
                command.Parameters.AddWithValue("@por_pagar", HM.HOM_POR_PAGAR);
            else
                command.Parameters.AddWithValue("@por_pagar", honorarios.HOM_POR_PAGAR);
            if(HM.HOM_POR_RECUPERAR != null)
                command.Parameters.AddWithValue("@por_recuperar", HM.HOM_POR_RECUPERAR);
            else
                command.Parameters.AddWithValue("@por_recuperar", honorarios.HOM_POR_RECUPERAR);
            command.Parameters.AddWithValue("@valor_pagado", HM.HOM_VALOR_PAGADO);
            command.Parameters.AddWithValue("@recorte", HM.HOM_RECORTE);
            if(HM.HOM_NETO_PAGAR != null)
                command.Parameters.AddWithValue("@neto_pagar", HM.HOM_NETO_PAGAR);
            else
                command.Parameters.AddWithValue("@neto_pagar", honorarios.HOM_NETO_PAGAR);
            command.Parameters.AddWithValue("@cancelado", HM.HOM_VALOR_CANCELADO);
            command.Parameters.AddWithValue("@valor_total", HM.HOM_VALOR_TOTAL);
            command.Parameters.AddWithValue("@lote", HM.HOM_LOTE);
            command.Parameters.AddWithValue("@observacion", HM.HOM_OBSERVACION);
            command.Parameters.AddWithValue("@fuera", fuera);
            command.Parameters.AddWithValue("@asiento", asiento);
            command.Parameters.AddWithValue("@caja", caja);
            command.Parameters.AddWithValue("@cubierto", cubierto);
            command.Parameters.AddWithValue("@estado", estado);
            command.Parameters.AddWithValue("@vale", HM.HOM_VALE);

            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();

        }

        public string ValorApc_Medico(Int64 med_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            string porcentaje = "";
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();

            connection.Open();

            command = new SqlCommand("sp_MedicoAPC", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@med_codigo", med_codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                porcentaje = reader["MAPC_VALOR_APC"].ToString();
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return porcentaje;
        }
        public bool GeneradoAsiento(Int64 hom_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            bool asiento = false;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();

            connection.Open();

            command = new SqlCommand("sp_ValidaHonorariosGenerados", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hom_codigo", hom_codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (Convert.ToInt32(reader["GENERADO_ASIENTO"].ToString()) == 1)
                    asiento = true;
                else
                    asiento = false;
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return asiento;
        }
        public DataTable HonorarioIndividual(Int64 hom_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();

            connection.Open();

            command = new SqlCommand("sp_HonorarioIndividual", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hom_codigo", hom_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
        public int intentos = 0;

        #region CodigoAnterior
        //public string GenerarAsientoContableHonorario(List<DtoCgDetmae> TablaHonorario)
        //{
        //    SqlCommand command;
        //    SqlConnection connection;
        //    SqlTransaction transaction;
        //    BaseContextoDatos obj = new BaseContextoDatos();
        //    connection = obj.ConectarBd();
        //    connection.Open();
        //    Int64 hom_codigo = 0;
        //    intentos = 0;
        //    transaction = connection.BeginTransaction();

        //    try
        //    {
        //        double numControl = OcuparControlADS(); //ocupo el numero de control y tomo el numero de asiento
        //        double debe = 0, haber = 0;
        //        string observacion = "", beneficiario = "";
        //        if (numControl > 0)
        //        {
        //            foreach (var item in TablaHonorario)
        //            {
        //                if (item.debe > 0)
        //                    debe += item.debe;
        //                else if (item.haber > 0)
        //                    haber += item.haber;
        //                observacion = item.observacion;
        //                hom_codigo = item.hom_codigo;
        //                beneficiario = item.beneficiario;
        //            }
        //            //Valida que el debe y el haber tenga el mismo valor
        //            if (Math.Round(debe, 2) == Math.Round(haber, 2))
        //                InsertCgCabmae(Math.Round(debe, 2), numControl, observacion, hom_codigo, beneficiario); //se inserta dentro del cgcabmae
        //            else
        //            {
        //                transaction.Rollback();
        //                return "Existen diferentes entre el debe y el haber.";
        //            }
        //            //recorre la lista para insertar el detalle en el Cgdetmae
        //            foreach (var item in TablaHonorario)
        //            {
        //                InsertCgDetmae(item, numControl);
        //            }
        //            //Inserto la cuenta por pagar
        //            foreach (var item in TablaHonorario)
        //            {
        //                if (item.codpre_pc == "210101-005" && item.haber > 0)
        //                {
        //                    InsertCxp(item, numControl);
        //                }
        //            }

        //            //Genero la CxC y el estado de cuenta
        //            command = new SqlCommand("sp_HonorarioCxC_EC", connection);
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@hom_codigo", hom_codigo);
        //            command.CommandTimeout = 180;
        //            command.ExecuteNonQuery();
        //            command.Parameters.Clear();
        //            connection.Close();
        //        }
        //        else
        //        {
        //            LiberarHonorario(hom_codigo);
        //            LiberarControlADS();
        //            transaction.Rollback();
        //            return "No se ha podido obtener numero de control.\r\nComuniquese con sistemas.";
        //        }
        //        //libero el numero de control sumando 1
        //        LiberarControlADS();
        //        transaction.Commit();
        //        return "Asiento directo generado correctamente.";
        //    }
        //    catch (Exception ex)
        //    {
        //        LiberarHonorario(hom_codigo);
        //        LiberarControlADS();
        //        transaction.Rollback();
        //        Console.WriteLine(ex.Message);
        //        return ex.Message;
        //    }
        //}

        //public void InsertCgDetmae(DtoCgDetmae dtoCgDetmaes, double numeroControl)
        //{
        //    SqlCommand command;
        //    SqlConnection connection;
        //    BaseContextoDatos obj = new BaseContextoDatos();
        //    connection = obj.ConectarBd();
        //    connection.Open();

        //    command = new SqlCommand("sp_HonorarioCgDetmae", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.AddWithValue("@tipdoc", dtoCgDetmaes.tipdoc);
        //    command.Parameters.AddWithValue("@numdoc", numeroControl);
        //    command.Parameters.AddWithValue("@linea", dtoCgDetmaes.linea);
        //    command.Parameters.AddWithValue("@año", dtoCgDetmaes.año);
        //    command.Parameters.AddWithValue("@fechatran", dtoCgDetmaes.fechatran.Date);
        //    command.Parameters.AddWithValue("@codzona", dtoCgDetmaes.codzona);
        //    command.Parameters.AddWithValue("@codloc", dtoCgDetmaes.codloc);
        //    command.Parameters.AddWithValue("@codcue_cp", dtoCgDetmaes.codcue_cp.Substring(0, 1));
        //    command.Parameters.AddWithValue("@cuenta_pc", dtoCgDetmaes.cuenta_pc);
        //    command.Parameters.AddWithValue("@subcta_pc", dtoCgDetmaes.subcta_pc);
        //    command.Parameters.AddWithValue("@codpre_pc", dtoCgDetmaes.codpre_pc);
        //    command.Parameters.AddWithValue("@codigo_c", dtoCgDetmaes.codigo_c);
        //    command.Parameters.AddWithValue("@nocomp", dtoCgDetmaes.nocomp);
        //    command.Parameters.AddWithValue("@beneficiario", dtoCgDetmaes.beneficiario);
        //    command.Parameters.AddWithValue("@debe", dtoCgDetmaes.debe);
        //    command.Parameters.AddWithValue("@haber", dtoCgDetmaes.haber);
        //    command.Parameters.AddWithValue("@comentario", dtoCgDetmaes.comentario);
        //    command.Parameters.AddWithValue("@movbanc", dtoCgDetmaes.movbanc);
        //    command.CommandTimeout = 180;
        //    command.ExecuteNonQuery();
        //    command.Parameters.Clear();
        //    connection.Close();
        //}

        //public void InsertCxp(DtoCgDetmae dtoCgDetmae, double numasi)
        //{
        //    SqlCommand command;
        //    SqlConnection connection;
        //    BaseContextoDatos obj = new BaseContextoDatos();
        //    connection = obj.ConectarBd();
        //    connection.Open();

        //    command = new SqlCommand("sp_HonorarioCxp", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.AddWithValue("@codigo_c", dtoCgDetmae.codigo_c);
        //    command.Parameters.AddWithValue("@numasi", numasi);
        //    command.Parameters.AddWithValue("@usuario", His.Entidades.Clases.Sesion.codUsuario);
        //    command.Parameters.AddWithValue("@nocomp", dtoCgDetmae.nocomp);
        //    command.Parameters.AddWithValue("@valor", dtoCgDetmae.haber);
        //    command.Parameters.AddWithValue("@numlinea", dtoCgDetmae.linea);
        //    command.Parameters.AddWithValue("@forpag", dtoCgDetmae.forpag);
        //    command.Parameters.AddWithValue("@despag", dtoCgDetmae.despag);
        //    command.Parameters.AddWithValue("@hom_codigo", dtoCgDetmae.hom_codigo);

        //    command.CommandTimeout = 180;
        //    command.ExecuteNonQuery();
        //    command.Parameters.Clear();
        //    connection.Close();
        //}
        #endregion
        public double OcuparControlADS(DateTime fecha)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            double numcontrol = 0;
            command = new SqlCommand("sp_OcuparControlADS", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechaAsiento", fecha);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                if(reader["numdoc_zv"].ToString() != "")
                {
                    numcontrol = Convert.ToDouble(reader["numdoc_zv"].ToString());
                }
            }
            if(intentos <= 3)
            {
                if (numcontrol == 0)
                {
                    intentos++;
                    Thread.Sleep(2000); //Espero 2 segundos
                    OcuparControlADS(fecha);
                }
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return numcontrol;
        }
       
        public void InsertCgCabmae(double valor, double numcontrol, string observacion, Int64 hom_codigo, string beneficiario, DateTime fechatran)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HonorariosCgCabMae", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@usuario", Entidades.Clases.Sesion.codUsuario);
            command.Parameters.AddWithValue("@numdoc", numcontrol);
            command.Parameters.AddWithValue("@observacion", observacion);
            command.Parameters.AddWithValue("@valor", valor);
            command.Parameters.AddWithValue("@beneficiario", beneficiario);
            command.Parameters.AddWithValue("@hom_codigo", hom_codigo);
            command.Parameters.AddWithValue("@fechatran", fechatran.Date);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();

        }
        
        public void LiberarControlADS(DateTime fecha)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_LiberarControlADS", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechaAsiento", fecha);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
        public void LiberarHonorario(Int64 hom_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HonorarioLiberado", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hom_codigo", hom_codigo);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }

        #region CODIGO CON TRANSACTION
        public string GenerarAsientoContableHonorario(List<DtoCgDetmae> TablaHonorario, DateTime fechaAsiento)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlTransaction transaction;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();

            intentos = 0; //esto ayudaria a que no se quede colgado en un bucle tratando de esperar ha que se libere el numero de control
            Int64 hom_codigo = 0;
            double numcontrol = 0;
            double debe = 0, haber = 0;
            string observacion = "", beneficiario = "";
            string forpag = "";
            DateTime fecha = DateTime.Now;
            transaction = connection.BeginTransaction();

            try
            {
                numcontrol = OcuparControlADS(fechaAsiento);
                if (numcontrol > 0)
                {
                    foreach (var item in TablaHonorario)
                    {
                        if (item.debe > 0)
                            debe += item.debe;
                        else if (item.haber > 0)
                            haber += item.haber;
                        observacion = item.observacion;
                        hom_codigo = item.hom_codigo;
                        beneficiario = item.beneficiario;
                        forpag = item.forpag;
                        fecha = item.fechatran;
                    }
                    //Valida que el debe y el haber tenga el mismo valor
                    if (Math.Round(debe, 2) == Math.Round(haber, 2))
                    {
                        InsertCgCabmae(Math.Round(debe, 2), numcontrol, observacion, hom_codigo, beneficiario, fecha); //se inserta dentro del cgcabmae
                    }
                    else
                    {
                        AnulaCabmae(hom_codigo, numcontrol);
                        LiberarHonorario(hom_codigo);
                        LiberarControlADS(fechaAsiento);
                        transaction.Rollback();
                        return "Existen diferentes entre el debe y el haber"; //no se pudo insertar en el cgCabme por que los valores del debe y haber no son iguales
                    }
                    //recorre la lista para insertar el detalle en el Cgdetmae
                    foreach (var item in TablaHonorario)
                    {
                        //connection.Open();
                        command = new SqlCommand("sp_HonorarioCgDetmae", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Transaction = transaction;
                        command.Parameters.AddWithValue("@tipdoc", item.tipdoc);
                        command.Parameters.AddWithValue("@numdoc", numcontrol);
                        command.Parameters.AddWithValue("@linea", item.linea);
                        command.Parameters.AddWithValue("@año", item.año);
                        command.Parameters.AddWithValue("@fechatran", item.fechatran.Date);
                        command.Parameters.AddWithValue("@codzona", item.codzona);
                        command.Parameters.AddWithValue("@codloc", item.codloc);
                        command.Parameters.AddWithValue("@codcue_cp", item.codcue_cp.Substring(0, 1));
                        command.Parameters.AddWithValue("@cuenta_pc", item.cuenta_pc);
                        command.Parameters.AddWithValue("@subcta_pc", item.subcta_pc);
                        command.Parameters.AddWithValue("@codpre_pc", item.codpre_pc);
                        command.Parameters.AddWithValue("@codigo_c", item.codigo_c);
                        command.Parameters.AddWithValue("@nocomp", item.nocomp);
                        command.Parameters.AddWithValue("@beneficiario", item.beneficiario);
                        command.Parameters.AddWithValue("@debe", item.debe);
                        command.Parameters.AddWithValue("@haber", item.haber);
                        command.Parameters.AddWithValue("@comentario", item.comentario);
                        command.Parameters.AddWithValue("@movbanc", item.movbanc);
                        command.CommandTimeout = 180;
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                        

                    }

                    //Listo todas las cuentas por pagar 
                    DataTable CuentasxPagar = ListarCuentasPorPagar();
                    //Inserto la cuenta por pagar
                    foreach (var item in TablaHonorario)
                    {
                        for (int i = 0; i < CuentasxPagar.Rows.Count; i++)
                        {
                            if(item.codpre_pc == CuentasxPagar.Rows[i][0].ToString())
                            {
                                //connection.Open();
                                command = new SqlCommand("sp_HonorarioCxp", connection);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Transaction = transaction;
                                command.Parameters.AddWithValue("@codigo_c", item.codigo_c);
                                command.Parameters.AddWithValue("@numasi", numcontrol);
                                command.Parameters.AddWithValue("@usuario", His.Entidades.Clases.Sesion.codUsuario);
                                command.Parameters.AddWithValue("@nocomp", item.nocomp);
                                if (item.haber > 0)
                                {
                                    command.Parameters.AddWithValue("@haber", item.haber);
                                    command.Parameters.AddWithValue("@debe", 0);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@debe", item.debe);
                                    command.Parameters.AddWithValue("@haber", 0);
                                }
                                command.Parameters.AddWithValue("@cuenta", CuentasxPagar.Rows[i][0].ToString());
                                command.Parameters.AddWithValue("@numlinea", item.linea);
                                command.Parameters.AddWithValue("@forpag", item.forpag);
                                command.Parameters.AddWithValue("@despag", item.despag);
                                command.Parameters.AddWithValue("@hom_codigo", item.hom_codigo);
                                command.Parameters.AddWithValue("@fechatran", fecha);

                                command.CommandTimeout = 180;
                                command.ExecuteNonQuery();
                                command.Parameters.Clear();
                                break;
                            }
                        }
                    }

                    //Genero la CxC y el estado de cuenta
                    command = new SqlCommand("sp_HonorarioCxC_EC", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transaction;
                    command.Parameters.AddWithValue("@hom_codigo", hom_codigo);
                    command.Parameters.AddWithValue("@forpag", forpag);
                    command.Parameters.AddWithValue("@fechatran", fecha);
                    command.CommandTimeout = 180;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    //CREO EL REGISTRO EN AUDITORIA
                    command = new SqlCommand("sp_AuditoriaCg", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transaction;
                    command.Parameters.AddWithValue("@beneficiario", beneficiario);
                    command.Parameters.AddWithValue("@usuario", His.Entidades.Clases.Sesion.codUsuario);
                    command.Parameters.AddWithValue("@observacion", observacion);
                    command.Parameters.AddWithValue("@hom_codigo", hom_codigo);
                    command.CommandTimeout = 180;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                }
                else
                {
                    LiberarHonorario(hom_codigo);
                    transaction.Rollback();
                    return "No se ha podido obtener numero de control. Comuniquese con sistemas.";
                }
                transaction.Commit();
                LiberarControlADS(fechaAsiento);

                return "Asiento directo generado correctamente.";
            }
            catch (Exception ex)
            {
                LiberarHonorario(hom_codigo);
                LiberarControlADS(fechaAsiento);
                transaction.Rollback();
                return ex.Message;
            }
        }

        public bool AnulaCabmae(Int64 hom_codigo, double numasi)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlTransaction transaction;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();
            transaction = connection.BeginTransaction();

            try
            {
                command = new SqlCommand("sp_AnulaCgCabmae", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@hom_codigo", hom_codigo);
                command.Parameters.AddWithValue("@numasi", numasi);
                command.ExecuteNonQuery();
                command.Parameters.Clear();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Validacion de asiento si esta pagado
        public DataTable ValidacionAD(Int64 hom_codigo, string codcli, string nocomp) 
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HonorarioValidarAnulacion", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hom_codigo", hom_codigo);
            command.Parameters.AddWithValue("@codcli", codcli);
            command.Parameters.AddWithValue("@nocomp", nocomp);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
        #endregion

        public DataTable ReporteAsiento(Int64 hom_codigo, int parametro)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HonorarioImpresionAsiento", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hom_codigo", hom_codigo);
            command.Parameters.AddWithValue("@parametro", parametro);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
        public DataTable ListarCuentasPorPagar()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_HonorariosListaCuentasXPagar", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
    }
}
