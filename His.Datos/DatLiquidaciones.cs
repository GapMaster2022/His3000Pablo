using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using System.Data;

namespace His.Datos
{
    public class DatLiquidaciones
    {
        public DsHonorarios HonorariosCxE(DateTime desde, DateTime hasta)
        {
            DsHonorarios HonorariosCxE = new DsHonorarios();

            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var cabecera = (from v in db.Vista_HonorariosCxE
                                select v);

                if (cabecera != null)
                {
                    foreach (var item in cabecera)
                    {
                        var detalle = (from hm in db.HONORARIOS_MEDICOS
                                       join hmd in db.HONORARIOS_MEDICOS_DATOSADICIONALES on hm.HOM_CODIGO equals hmd.HOM_CODIGO
                                       join a in db.ATENCIONES on hm.ATE_CODIGO equals a.ATE_CODIGO
                                       where hm.MEDICOS.MED_CODIGO == item.CODIGO && hmd.GENERADO_ASIENTO == 0
                                       && a.TIPO_INGRESO.TIP_CODIGO == 4 && hmd.HON_FUERA == false
                                       select new
                                       {
                                           hm,
                                           hmd,
                                           a
                                       }).ToList();
                        double valortotal = 0;
                        foreach (var a in detalle)
                        {
                            valortotal = valortotal + (double)a.hm.HOM_VALOR_TOTAL;
                        }

                        if (detalle.Count > 0)
                        {
                            object[] x = new object[]
                        {
                            false,
                            item.CODIGO,
                            item.MEDICO,
                            detalle.Count,
                            Math.Round(valortotal, 2)
                        };
                            HonorariosCxE.Honorario.Rows.Add(x);

                            foreach (var i in detalle)
                            {
                                int id = Convert.ToInt32(i.hm.USUARIOSReference.EntityKey.EntityKeyValues[0].Value);
                                Int64 pac = Convert.ToInt64(i.a.PACIENTESReference.EntityKey.EntityKeyValues[0].Value);
                                PACIENTES pacientes = (from p in db.PACIENTES
                                                       where p.PAC_CODIGO == pac
                                                       select p).FirstOrDefault();
                                USUARIOS us = (from u in db.USUARIOS
                                               where u.ID_USUARIO == id
                                               select u).FirstOrDefault();
                                FORMA_PAGO f = (from fp in db.FORMA_PAGO
                                                where fp.FOR_CODIGO == i.hm.FOR_CODIGO
                                                select fp).FirstOrDefault();
                                var se = (from ad in db.ATENCION_DETALLE_CATEGORIAS
                                          join ca in db.CATEGORIAS_CONVENIOS on ad.CATEGORIAS_CONVENIOS.CAT_CODIGO equals ca.CAT_CODIGO
                                          where ad.ATENCIONES.ATE_CODIGO == i.a.ATE_CODIGO
                                          select new
                                          {
                                              ca
                                          }).FirstOrDefault();

                                object[] y = new object[]
                                {
                                false,
                                pacientes.PAC_APELLIDO_PATERNO + " " + pacientes.PAC_APELLIDO_MATERNO + " " + pacientes.PAC_NOMBRE1 + " " + pacientes.PAC_NOMBRE2,
                                i.hm.HOM_FECHA_INGRESO,
                                i.hm.HOM_VALE,
                                i.hm.HOM_VALOR_NETO,
                                i.hm.HOM_COMISION_CLINICA,
                                i.hm.HOM_APORTE_LLAMADA,
                                i.hm.HOM_RETENCION,
                                i.hm.HOM_VALOR_TOTAL,
                                us.APELLIDOS + " " + us.NOMBRES,
                                i.hmd.HON_FUERA,
                                f.FOR_DESCRIPCION,
                                se.ca.CAT_NOMBRE,
                                f.forpag,
                                i.hm.HOM_CODIGO,
                                i.hm.MEDICOSReference.EntityKey.EntityKeyValues[0].Value
                                };

                                HonorariosCxE.Detalle.Rows.Add(y);
                            }
                        }
                    }

                }
                return HonorariosCxE;
            }
        }

        public bool guardarLiquidacion(List<LIQUIDACION> liquidacion)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;

            connection = obj.ConectarBd();
            connection.Open();
            transaction = connection.BeginTransaction();
            try
            {
                foreach (var item in liquidacion)
                {
                    command = new SqlCommand("INSERT INTO LIQUIDACION VALUES(@med_codigo, @hom_codigo, @id_usuario, 1, @numdoc, @fecha, 0)", connection);
                    command.CommandType = System.Data.CommandType.Text;
                    command.Transaction = transaction;
                    command.Parameters.AddWithValue("@med_codigo", item.MED_CODIGO);
                    command.Parameters.AddWithValue("@hom_codigo", item.HOM_CODIGO);
                    command.Parameters.AddWithValue("@id_usuario", item.ID_USUARIO);
                    command.Parameters.AddWithValue("@numdoc", item.LIQ_NUMDOC);
                    command.Parameters.AddWithValue("@fecha", item.LIQ_FECHA);

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                transaction.Commit();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
                connection.Close();
                return false;
            }
        }

        public bool bloquearHonorario(List<LIQUIDACION> liquidados)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;
            connection = obj.ConectarBd();

            connection.Open();
            transaction = connection.BeginTransaction();
            try
            {
                foreach (var item in liquidados)
                {
                    command = new SqlCommand("UPDATE HONORARIOS_MEDICOS_DATOSADICIONALES SET GENERADO_ASIENTO = 1 WHERE HOM_CODIGO = @hom_codigo", connection);
                    command.CommandType = System.Data.CommandType.Text;
                    command.Transaction = transaction;
                    command.Parameters.AddWithValue("@hom_codigo", item.HOM_CODIGO);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                transaction.Commit();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
                connection.Close();
                return false;
            }
        }

        public List<LIQUIDACION> RecuperarLiquidacion(Int64 numdoc)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<LIQUIDACION> xliq = (from l in db.LIQUIDACION
                                          where l.LIQ_NUMDOC == numdoc
                                          select l).ToList();
                return xliq;
            }
        }

        public List<LIQUIDACION> RecuperarLiquidacionPendiente(Int64 numdoc)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<LIQUIDACION> xliq = (from l in db.LIQUIDACION
                                          where l.LIQ_NUMDOC == numdoc
                                          && l.LIQ_LIQUIDADO == false
                                          select l).ToList();
                return xliq;
            }
        }
        public HONORARIOS_MEDICOS recuperarHonorario(Int64 hom_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HONORARIOS_MEDICOS x = db.HONORARIOS_MEDICOS.FirstOrDefault(a => a.HOM_CODIGO == hom_codigo);
                return x;
            }
        }
        public IEnumerable<object> listarLiquidaciones(DateTime desde, DateTime hasta, int liquidacion, string factura, int medico, bool liquidado)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from l in db.LIQUIDACION
                             join m in db.MEDICOS on l.MED_CODIGO equals m.MED_CODIGO
                             join h in db.HONORARIOS_MEDICOS on l.HOM_CODIGO equals h.HOM_CODIGO
                             join a in db.ATENCIONES on h.ATE_CODIGO equals a.ATE_CODIGO
                             join p in db.PACIENTES on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                             where l.LIQ_FECHA >= desde && l.LIQ_FECHA <= hasta && l.LIQ_LIQUIDADO == true
                             select new
                             {
                                 Liquidacion = l.LIQ_NUMDOC,
                                 Fecha = l.LIQ_FECHA,
                                 Honorario = h.HOM_CODIGO,
                                 Medico = m.MED_APELLIDO_PATERNO + " " + m.MED_APELLIDO_MATERNO + " " + m.MED_NOMBRE1 + " " + m.MED_NOMBRE2,
                                 Paciente = p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2,
                                 Valor = h.HOM_VALOR_NETO,
                                 Retencion = h.HOM_RETENCION,
                                 Aporte = h.HOM_APORTE_LLAMADA,
                                 Comision = h.HOM_COMISION_CLINICA,
                                 Total = h.HOM_VALOR_TOTAL,
                                 Vale = h.HOM_VALE,
                                 MED_CODIGO = l.MED_CODIGO
                             }).ToList();
                if (liquidacion != 0)
                    lista = lista.Where(x => x.Liquidacion == liquidacion).ToList();

                if (medico != 0)
                    lista = lista.Where(x => x.MED_CODIGO == medico).ToList();

                if (liquidado)
                {
                    var lista1 = (from l in db.LIQUIDACION
                                  join m in db.MEDICOS on l.MED_CODIGO equals m.MED_CODIGO
                                  join h in db.HONORARIOS_MEDICOS on l.HOM_CODIGO equals h.HOM_CODIGO
                                  join a in db.ATENCIONES on h.ATE_CODIGO equals a.ATE_CODIGO
                                  join p in db.PACIENTES on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                  where l.LIQ_FECHA >= desde && l.LIQ_FECHA <= hasta && l.LIQ_LIQUIDADO == true
                                  select new
                                  {
                                      Liquidacion = l.LIQ_NUMDOC,
                                      Fecha = l.LIQ_FECHA,
                                      Honorario = h.HOM_CODIGO,
                                      Medico = m.MED_APELLIDO_PATERNO + " " + m.MED_APELLIDO_MATERNO + " " + m.MED_NOMBRE1 + " " + m.MED_NOMBRE2,
                                      Paciente = p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2,
                                      Valor = h.HOM_VALOR_NETO,
                                      Retencion = h.HOM_RETENCION,
                                      Aporte = h.HOM_APORTE_LLAMADA,
                                      Comision = h.HOM_COMISION_CLINICA,
                                      Total = h.HOM_VALOR_TOTAL,
                                      Vale = h.HOM_VALE,
                                      MED_CODIGO = l.MED_CODIGO,
                                      Asiento = l.LIQ_ASIENTO,
                                      Factura = (from ld in db.LIQUIDACION_DETALLE
                                                 where ld.LIQ_NUMDOC == l.LIQ_ASIENTO
                                                 select ld.LDE_FACTURA).FirstOrDefault()
                                  }).ToList();
                    if (factura != "")
                    {
                        lista1 = lista1.Where(x => x.Factura.Contains(factura)).ToList();

                        if (liquidacion != 0)
                        {
                            lista1 = lista1.Where(x => x.Liquidacion == liquidacion).ToList();
                        }
                        if (medico != 0)
                            lista1 = lista1.Where(x => x.MED_CODIGO == medico).ToList();
                        return (IEnumerable<object>)lista1;
                    }
                    else
                    {
                        if (liquidacion != 0)
                        {
                            lista1 = lista1.Where(x => x.Liquidacion == liquidacion).ToList();
                        }
                        if (medico != 0)
                            lista1 = lista1.Where(x => x.MED_CODIGO == medico).ToList();
                        return (IEnumerable<object>)lista1;
                    }
                }
                return (IEnumerable<object>)lista;
            }
        }
        public DsLiquidacion Liquidar(DateTime desde, DateTime hasta)
        {
            DsLiquidacion liquidacion = new DsLiquidacion();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var maestro = (from l in db.LIQUIDACION
                               join m in db.MEDICOS on l.MED_CODIGO equals m.MED_CODIGO
                               where l.LIQ_FECHA >= desde && l.LIQ_FECHA <= hasta
                               && l.LIQ_ASIENTO == 0 && l.LIQ_LIQUIDADO == true
                               select new
                               {
                                   l,
                                   m
                               }).ToList();
                maestro = maestro.GroupBy(x => x.m.MED_CODIGO,
                (key, group) => group.FirstOrDefault()).ToList();

                foreach (var item in maestro)
                {
                    var detalle = (from h in db.HONORARIOS_MEDICOS
                                   join a in db.ATENCIONES on h.ATE_CODIGO equals a.ATE_CODIGO
                                   join p in db.PACIENTES on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                   join hd in db.HONORARIOS_MEDICOS_DATOSADICIONALES on h.HOM_CODIGO equals hd.HOM_CODIGO
                                   where h.MEDICOS.MED_CODIGO == item.m.MED_CODIGO
                                   //&& a.TIPO_INGRESO.TIP_CODIGO == 4 && h.HOM_FACTURA_MEDICO == "" && hd.GENERADO_ASIENTO == 1 // se modifica por cambio de Consula externa // Mario Valencia // 21-12-2023
                                   && h.HOM_FACTURA_MEDICO == "" && hd.GENERADO_ASIENTO == 1 && h.HOM_OBSERVACION == "CONSULTA EXTERNA"
                                   select new
                                   {
                                       h,
                                       a,
                                       p,
                                   }).ToList();

                    foreach (var item1 in detalle)
                    {
                        object[] x = new object[]
                        {
                            false,
                            item1.h.HOM_CODIGO,
                            item1.p.PAC_APELLIDO_PATERNO + " " + item1.p.PAC_APELLIDO_MATERNO + " " + item1.p.PAC_NOMBRE1 + " " + item1.p.PAC_NOMBRE2,
                            item1.h.HOM_VALOR_NETO,
                            item1.h.HOM_COMISION_CLINICA,
                            item1.h.HOM_APORTE_LLAMADA,
                            item1.h.HOM_RETENCION,
                            item1.h.HOM_VALOR_TOTAL,
                            item.l.LIQ_FECHA,
                            item1.h.MEDICOSReference.EntityKey.EntityKeyValues[0].Value
                        };
                        liquidacion.Detalle.Rows.Add(x);
                    }
                    object[] y = new object[]
                    {
                        false,
                        false,
                        item.l.LIQ_NUMDOC,
                        item.m.MED_CODIGO,
                        item.m.MED_APELLIDO_PATERNO + " " + item.m.MED_APELLIDO_MATERNO + " " + item.m.MED_NOMBRE1 + " " + item.m.MED_NOMBRE2,
                        ""
                    };
                    liquidacion.Maestro.Rows.Add(y);
                }
                return liquidacion;
            }
        }

        public PARAMETROS_DETALLE parametroHonorarios(int pad_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PARAMETROS_DETALLE parametros = (from p in db.PARAMETROS_DETALLE
                                                 where p.PAD_CODIGO == pad_codigo
                                                 select p).FirstOrDefault();
                return parametros;
            }
        }
        public DataTable DatosSRI(double codigo_c, Int64 factura)
        {
            DataTable dt = new DataTable();
            SqlConnection connection;
            SqlCommand command;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select * from CG3000..CgDatosSRI where codigo_c = @codigo_c and cast(factinicio as float) <= @factura and cast(factfin as float) >= @factura", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@codigo_c", codigo_c);
            command.Parameters.AddWithValue("@factura", factura);

            reader = command.ExecuteReader();
            dt.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            return dt;
        }

        public bool LiquidacionGlobal(List<Cgcabmae> cabmae, List<Cgdetmae> detmae)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlTransaction transaction;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            transaction = connection.BeginTransaction();
            bool cabInsertado = false;
            double numdoc = 0;
            DateTime fecha = DateTime.Now;
            try
            {
                //creo el registro en el cabmae
                #region CABMAE
                foreach (var item in cabmae)
                {
                    command = new SqlCommand("sp_LiquidacionCABMAE", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transaction;

                    command.Parameters.AddWithValue("@usuario", His.Entidades.Clases.Sesion.codUsuario);
                    command.Parameters.AddWithValue("@zona", item.codzona);
                    command.Parameters.AddWithValue("@tipo", item.tipdoc);
                    command.Parameters.AddWithValue("@numdoc", item.numdoc);
                    command.Parameters.AddWithValue("@anio", item.año);
                    command.Parameters.AddWithValue("@fechatran", item.fechatran);
                    command.Parameters.AddWithValue("@fechaingreso", item.fechaing);
                    command.Parameters.AddWithValue("@observacion", item.observacion);
                    command.Parameters.AddWithValue("@debe", item.totdebe);
                    command.Parameters.AddWithValue("@haber", item.tothaber);
                    command.Parameters.AddWithValue("@fecha1", item.fecha1);
                    command.Parameters.AddWithValue("@fecha2", item.fecha2);
                    command.Parameters.AddWithValue("@beneficiario", item.beneficiario);
                    command.Parameters.AddWithValue("@vdolares", item.vdolares);
                    command.Parameters.AddWithValue("@cierre", item.cierre);
                    command.Parameters.AddWithValue("@borrar", item.borrar);
                    command.Parameters.AddWithValue("@solicitado", item.solicitado);
                    command.Parameters.AddWithValue("@depto", item.depto);
                    command.Parameters.AddWithValue("@autorizado", item.autorizado);
                    command.Parameters.AddWithValue("@hom_codigo", item.HOM_CODIGO);

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    cabInsertado = true;
                    numdoc = item.numdoc;
                    fecha = item.fechatran;

                    //CREO LSO REGISTROS EN EL DETMAE
                    #region DETMAE
                    foreach (var x in detmae)
                    {
                        if (item.numdoc == x.numdoc)
                        {
                            command = new SqlCommand("sp_LiquidacionDETMAE", connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = transaction;

                            command.Parameters.AddWithValue("@tipo", x.tipdoc);
                            command.Parameters.AddWithValue("@numdoc", x.numdoc);
                            command.Parameters.AddWithValue("@linea", x.numlinea);
                            command.Parameters.AddWithValue("@anio", x.año);
                            command.Parameters.AddWithValue("@fechatran", x.fechatran);
                            command.Parameters.AddWithValue("@zona", x.codzona);
                            command.Parameters.AddWithValue("@local", x.codloc);
                            command.Parameters.AddWithValue("@codcue_cp", x.codcue_cp);
                            command.Parameters.AddWithValue("@cuenta_pc", x.cuenta_pc);
                            command.Parameters.AddWithValue("@subcta_pc", x.subcta_pc);
                            command.Parameters.AddWithValue("@codpre_pc", x.codpre_pc);
                            command.Parameters.AddWithValue("@codigo_c", x.codigo_c);
                            command.Parameters.AddWithValue("@nocomp", x.nocomp);
                            command.Parameters.AddWithValue("@cheque", x.cheque);
                            command.Parameters.AddWithValue("@beneficiario", x.beneficiario);
                            command.Parameters.AddWithValue("@debe", x.debe);
                            command.Parameters.AddWithValue("@haber", x.haber);
                            command.Parameters.AddWithValue("@comentario", x.comentario);
                            command.Parameters.AddWithValue("@movbanc", x.movbanc);
                            command.Parameters.AddWithValue("@fechaing", x.fechaing);
                            command.Parameters.AddWithValue("@fecha1", x.fecha1);
                            command.Parameters.AddWithValue("@fecha2", x.fecha2);
                            command.Parameters.AddWithValue("@printed", x.printed);
                            command.Parameters.AddWithValue("@cierre", x.cierre);
                            command.Parameters.AddWithValue("@conciliado", x.conciliado);
                            command.Parameters.AddWithValue("@autorizacion", x.autorizacion);
                            command.Parameters.AddWithValue("@sustento", x.sustentotrib);
                            command.Parameters.AddWithValue("@tipcomprobante", x.tipcomprob);
                            command.Parameters.AddWithValue("@fechacaduca", x.feccaduca);
                            command.Parameters.AddWithValue("@codretencion", x.codretfuente);
                            command.Parameters.AddWithValue("@estado", x.estado);

                            command.ExecuteNonQuery();
                            command.Parameters.Clear();
                        }
                    }
                    #endregion

                    #region CXP
                    //Listo todas las cuentas por pagar 
                    DataTable CuentasxPagar = ListarCuentasPorPagar();
                    //Inserto la cuenta por pagar
                    foreach (var x in detmae)
                    {
                        for (int i = 0; i < CuentasxPagar.Rows.Count; i++)
                        {
                            if (x.codpre_pc == CuentasxPagar.Rows[i][0].ToString())
                            {
                                //connection.Open();
                                command = new SqlCommand("sp_HonorarioCxp", connection);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Transaction = transaction;
                                command.Parameters.AddWithValue("@codigo_c", x.codigo_c);
                                command.Parameters.AddWithValue("@numasi", x.numdoc);
                                command.Parameters.AddWithValue("@usuario", His.Entidades.Clases.Sesion.codUsuario);
                                command.Parameters.AddWithValue("@nocomp", x.nocomp);
                                if (x.haber > 0)
                                {
                                    command.Parameters.AddWithValue("@haber", x.haber);
                                    command.Parameters.AddWithValue("@debe", 0);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@debe", x.debe);
                                    command.Parameters.AddWithValue("@haber", 0);
                                }
                                command.Parameters.AddWithValue("@cuenta", CuentasxPagar.Rows[i][0].ToString());
                                command.Parameters.AddWithValue("@numlinea", x.numlinea);
                                command.Parameters.AddWithValue("@forpag", 0);
                                command.Parameters.AddWithValue("@despag", 0);
                                command.Parameters.AddWithValue("@hom_codigo", item.HOM_CODIGO);
                                command.Parameters.AddWithValue("@fechatran", fecha);

                                command.CommandTimeout = 180;
                                command.ExecuteNonQuery();
                                command.Parameters.Clear();
                                break;
                            }
                        }
                    }
                    #endregion
                }
                #endregion

                transaction.Commit();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
                if (cabInsertado && numdoc != 0)
                {
                    AnulaCabmae(0, numdoc);
                    LiberarControlADS(fecha);
                }
                connection.Close();
                return false;
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
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex.Message);
                connection.Close();
                return false;
            }
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
        public bool BloquearLiquidacion(List<Cgdetmae> ldetmae, List<LIQUIDACION> l)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    double numdoc = 0;
                    string factura = "";
                    foreach (var item in ldetmae)
                    {
                        numdoc = item.numdoc;
                        factura = item.nocomp;
                        break;
                    }
                    foreach (var item in l)
                    {
                        LIQUIDACION li = db.LIQUIDACION.FirstOrDefault(x => x.LIQ_NUMDOC == item.LIQ_NUMDOC && x.HOM_CODIGO == item.HOM_CODIGO);
                        li.LIQ_ASIENTO = numdoc;

                        HONORARIOS_MEDICOS hm = db.HONORARIOS_MEDICOS.FirstOrDefault(x => x.HOM_CODIGO == item.HOM_CODIGO);
                        hm.HOM_FACTURA_MEDICO = factura;

                        db.SaveChanges();
                    }
                    transa.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    guardaDetalleLiq(ldetmae);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    transa.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }

            }
        }
        public bool desbloquearLiquidacion(Int64 liq_codigo, int med_codigo, double numdoc, List<Cgdetmae> ldetmae)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    LIQUIDACION l = db.LIQUIDACION.FirstOrDefault(x => x.LIQ_NUMDOC == liq_codigo && x.MED_CODIGO == med_codigo && x.LIQ_ASIENTO == numdoc);

                    l.LIQ_LIQUIDADO = false;
                    l.LIQ_ASIENTO = 0;

                    db.SaveChanges();

                    foreach (var item in ldetmae)
                    {
                        LIQUIDACION_DETALLE x = db.LIQUIDACION_DETALLE.FirstOrDefault(y => y.LIQ_NUMDOC == item.numdoc && y.LDE_CUENTA == item.codpre_pc);
                        x.LDE_ESTADO = false;
                        x.ID_USUARIO_ANULA = His.Entidades.Clases.Sesion.codUsuario;
                        x.LDE_ANULA = "ANULACION POR SISTEMA ALGO OCURRIO AL GENERAR ASIENTO";

                        db.SaveChanges();
                    }
                    transa.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transa.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }
        }
        public void guardaDetalleLiq(List<Cgdetmae> ldetmae)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            foreach (var item in ldetmae)
            {
                command = new SqlCommand("sp_LiquidacionDetalleSave", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@tipo", item.tipdoc);
                command.Parameters.AddWithValue("@numdoc", item.numdoc);
                command.Parameters.AddWithValue("@fecha", item.fechaing);
                command.Parameters.AddWithValue("@cuenta", item.codpre_pc);
                command.Parameters.AddWithValue("@codigo_c", item.codigo_c);
                command.Parameters.AddWithValue("@factura", item.nocomp);
                command.Parameters.AddWithValue("@debe", item.debe);
                command.Parameters.AddWithValue("@haber", item.haber);
                command.Parameters.AddWithValue("@usuario", Entidades.Clases.Sesion.codUsuario);

                command.ExecuteNonQuery();
                command.Parameters.Clear();

            }
            connection.Close();
        }
        public bool validaFactura(string factura, double codigo_c)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<LIQUIDACION_DETALLE> l = (from li in db.LIQUIDACION_DETALLE
                                               where li.LDE_FACTURA == factura
                                               && li.LDE_ESTADO == true && li.LDE_CODIGO_C == codigo_c
                                               select li).ToList();
                if (l.Count == 0)
                    return false;
                else
                    return true;
            }
        }

        public bool validaFacturaCG(string factura, double codigo_c)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool existe = false;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_LiquidacionValidaFacturaCG", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@factura", factura);
            command.Parameters.AddWithValue("@codigo_c", codigo_c);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                existe = true;
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return existe;
        }
        public DataTable ReporteAsientoLiquidacion(double numdoc, string tipo, int parametro)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_LiquidacionReporte", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@numdoc", numdoc);
            command.Parameters.AddWithValue("@tipo", tipo);
            command.Parameters.AddWithValue("@parametro", parametro);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
        public bool guardarAuditoria(List<Cgcabmae> cab, Int64 codigo, string estado, DateTime fecha)
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
                foreach (var item in cab)
                {
                    command = new SqlCommand("sp_LiquidacionCG", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transaction;
                    command.Parameters.AddWithValue("@tipo", item.tipdoc);
                    command.Parameters.AddWithValue("@numdoc", item.numdoc);
                    command.Parameters.AddWithValue("@codcli", codigo);
                    command.Parameters.AddWithValue("@usuario", item.codrespon);
                    command.Parameters.AddWithValue("@debe", item.totdebe);
                    command.Parameters.AddWithValue("@haber", item.tothaber);
                    command.Parameters.AddWithValue("@observacion", item.observacion);
                    command.Parameters.AddWithValue("@beneficiario", item.beneficiario);
                    command.Parameters.AddWithValue("@hom_codigo", item.HOM_CODIGO);
                    command.Parameters.AddWithValue("@estado", estado);
                    command.Parameters.AddWithValue("@fecha", fecha);

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }

                transaction.Commit();
                connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
                connection.Close();
                return false;
            }
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
        public List<LIQUIDACION> consultarLiquidados(Int64 liquidacion)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();

                List<LIQUIDACION> l = (from li in db.LIQUIDACION
                                       where li.LIQ_NUMDOC == liquidacion
                                       select li).ToList();
                try
                {
                    foreach (var item in l)
                    {
                        LIQUIDACION li = db.LIQUIDACION.FirstOrDefault(x => x.LIQ_CODIGO == item.LIQ_CODIGO);
                        li.LIQ_LIQUIDADO = false;
                        db.SaveChanges();
                    }
                    transa.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transa.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return null;
                }
                return l;
            }
        }
        public bool ReversionLiquidacion(Int64 liquidacion)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;
            connection = obj.ConectarBd();

            connection.Open();
            transaction = connection.BeginTransaction();

            List<LIQUIDACION> liquidados = new List<LIQUIDACION>();
            liquidados = consultarLiquidados(liquidacion);
            if (liquidados.Count > 0)
            {
                try
                {
                    foreach (var item in liquidados)
                    {
                        command = new SqlCommand("UPDATE HONORARIOS_MEDICOS SET HOM_FACTURA_MEDICO = '' WHERE HOM_CODIGO = @hom_codigo \nUPDATE HONORARIOS_MEDICOS_DATOSADICIONALES SET GENERADO_ASIENTO = 0 WHERE HOM_CODIGO = @hom_codigo", connection);
                        command.CommandType = CommandType.Text;
                        command.Transaction = transaction;
                        command.Parameters.AddWithValue("@hom_codigo", item.HOM_CODIGO);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    transaction.Commit();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            else
                return false;
        }
        public bool ValidaLiquidacionUsada(double numdoc, string tipo)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool retencion = false;
            bool cxp = false;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select COUNT(CodCompEg) as numero from CG3000..CgRetenciones WHERE Tipdoc = @tipo AND CodCompEg = @numdoc", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@tipo", tipo);
            command.Parameters.AddWithValue("@numdoc", numdoc);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string r = reader["numero"].ToString();
                if (r != "0")
                    retencion = true;
            }
            reader.Close();
            command.Parameters.Clear();

            command = new SqlCommand("select COUNT(numasi) as numero from CG3000..CgCuentasXPagar where numasi = @numdoc and tipasi = @tipo and estadopago = 'P' and estado = 'A'", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@tipo", tipo);
            command.Parameters.AddWithValue("@numdoc", numdoc);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string r = reader["numero"].ToString();
                if (r != "0")
                    cxp = true;
            }
            if (!cxp && !retencion)
                return true;
            else
                return false;
        }
        public bool anulacionCG(double numdoc, string tipo, double codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select * from CG3000..Cgcabmae where numdoc = @numdoc and tipdoc = @tipo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@numdoc", numdoc);
            command.Parameters.AddWithValue("@tipo", tipo);
            reader = command.ExecuteReader();
            tabla.Load(reader);

            reader.Close();
            command.Parameters.Clear();

            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                if (tabla.Rows.Count > 0)
                {
                    command = new SqlCommand("sp_LiquidacionCG", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transaction;
                    command.Parameters.AddWithValue("@tipo", tabla.Rows[0]["tipdoc"].ToString());
                    command.Parameters.AddWithValue("@numdoc", tabla.Rows[0]["numdoc"].ToString());
                    command.Parameters.AddWithValue("@codcli", codigo);
                    command.Parameters.AddWithValue("@usuario", Entidades.Clases.Sesion.codUsuario);
                    command.Parameters.AddWithValue("@debe", tabla.Rows[0]["totdebe"].ToString());
                    command.Parameters.AddWithValue("@haber", tabla.Rows[0]["tothaber"].ToString());
                    command.Parameters.AddWithValue("@observacion", tabla.Rows[0]["observacion"].ToString());
                    command.Parameters.AddWithValue("@beneficiario", tabla.Rows[0]["beneficiario"].ToString());
                    command.Parameters.AddWithValue("@hom_codigo", 0);
                    command.Parameters.AddWithValue("@estado", "ANULACION DE AD POR HIS CE");
                    command.Parameters.AddWithValue("@fecha", DateTime.Now);

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    command = new SqlCommand("sp_LiquidacionAnulacion", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transaction;
                    command.Parameters.AddWithValue("@numdoc", numdoc);
                    command.Parameters.AddWithValue("@tipo", tipo);

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    transaction.Commit();
                    connection.Close();
                    return true;
                }
                else
                {
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
                connection.Close();
                return false;
            }
        }

        public bool anulacionLiquidacion(double numdoc)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                DbTransaction transaction;
                ConexionEntidades.ConexionEDM.Open();
                transaction = ConexionEntidades.ConexionEDM.BeginTransaction();

                try
                {
                    List<LIQUIDACION> l = (from li in db.LIQUIDACION
                                           where li.LIQ_ASIENTO == numdoc
                                           select li).ToList();
                    foreach (var item in l)
                    {
                        LIQUIDACION li = db.LIQUIDACION.FirstOrDefault(x => x.LIQ_CODIGO == item.LIQ_CODIGO);
                        li.LIQ_ASIENTO = 0;

                        db.SaveChanges();
                    }
                    List<LIQUIDACION_DETALLE> ld = (from ll in db.LIQUIDACION_DETALLE
                                                    where ll.LIQ_NUMDOC == numdoc
                                                    select ll).ToList();
                    foreach (var item in ld)
                    {
                        LIQUIDACION_DETALLE le = db.LIQUIDACION_DETALLE.FirstOrDefault(x => x.LDE_CODIGO == item.LDE_CODIGO);
                        le.LDE_ANULA = "ANULACION AD POR EL HIS CE";
                        le.ID_USUARIO_ANULA = Entidades.Clases.Sesion.codUsuario;
                        le.LDE_ESTADO = false;

                        db.SaveChanges();
                    }

                    transaction.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }
        }
        public bool anularHonoratio(string HOM_CODIGO)
        {
            bool valida = true;
            try
            {
                SqlCommand command;
                SqlConnection connection;
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();

                connection.Open();

                command = new SqlCommand("update HONORARIOS_MEDICOS set HOM_FACTURA_MEDICO = '' where HOM_CODIGO = @HOM_CODIGO", connection);
                command.CommandType = System.Data.CommandType.Text;
  
                command.Parameters.AddWithValue("@HOM_CODIGO", HOM_CODIGO);
                command.ExecuteNonQuery();
                command.Parameters.Clear();


                connection.Close();


            }
            catch (Exception ex)
            {
                valida = false;
                //throw;
            }
            return valida;
        }
        public bool validaReversionLiquidacion(Int64 liquidacion)
        {
            bool valido = true;
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<LIQUIDACION> liq = (from l in db.LIQUIDACION
                                         where l.LIQ_NUMDOC == liquidacion
                                         select l).ToList();
                if (liq.Count > 0)
                {
                    foreach (var item in liq)
                    {
                        if (item.LIQ_ASIENTO == 0)
                        {
                            valido = true; //no tiene asiento contable puede reversar
                        }
                        else
                            valido = false; //tiene asientos contables
                    }
                }
            }
            return valido;
        }
    }
}
