using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using System.Data.SqlClient;
using System.Data;
using Core.Datos;
using Core.Entidades;
using His.Entidades.General;
using System.Data.EntityClient;
using System.Data.Common;
using Microsoft.Data.Extensions;

namespace His.Datos
{
    public class DatCuentasPacientes
    {
        #region metodos generales

        /// <summary>
        /// Método que crea un nuevo registro de estado de cuenta del paciente
        /// </summary>
        /// <param name="cuenta">Objeto CUENTAS_PACIENTES que se guardara en la base de datos</param>
        public Int64 CrearCuenta(CUENTAS_PACIENTES cuenta)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    Int64 codCuenta;
                    CUENTAS_PACIENTES cueCodigo = contexto.CUENTAS_PACIENTES.OrderByDescending(c => c.CUE_CODIGO).FirstOrDefault();
                    codCuenta = cueCodigo != null ? cueCodigo.CUE_CODIGO + 1 : 1;
                    cuenta.CUE_CODIGO = codCuenta;
                    cuenta.Codigo_Pedido = 1;
                    contexto.AddToCUENTAS_PACIENTES(cuenta);
                    contexto.SaveChanges();
                    return codCuenta;
                }
            }
            catch (Exception err) { throw err; }
        }

        /// <summary>
        /// Método que actualiza un registro de cuenta del paciente
        /// </summary>
        /// <param name="cuenta">Objeto CUENTAS_PACIENTES que se actualizara en la base de datos</param>
        public void ModificarCuenta(CUENTAS_PACIENTES cuenta)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    CUENTAS_PACIENTES cuentaOri = contexto.CUENTAS_PACIENTES.Where(c => c.CUE_CODIGO == cuenta.CUE_CODIGO).FirstOrDefault();
                    cuentaOri.CUE_CANTIDAD = cuenta.CUE_CANTIDAD;
                    cuentaOri.PRO_CODIGO = cuenta.PRO_CODIGO;
                    cuentaOri.CUE_DETALLE = cuenta.CUE_DETALLE;
                    cuentaOri.CUE_ESTADO = cuenta.CUE_ESTADO;
                    cuentaOri.CUE_FECHA = cuenta.CUE_FECHA;
                    cuentaOri.CUE_IVA = cuenta.CUE_IVA;
                    cuentaOri.CUE_NUM_FAC = cuenta.CUE_NUM_FAC;
                    cuentaOri.CUE_VALOR_UNITARIO = cuenta.CUE_VALOR_UNITARIO;
                    cuentaOri.CUE_VALOR = cuenta.CUE_VALOR;
                    cuentaOri.ID_USUARIO = cuenta.ID_USUARIO;
                    cuentaOri.PED_CODIGO = cuenta.PED_CODIGO;
                    cuentaOri.RUB_CODIGO = cuenta.RUB_CODIGO;
                    cuentaOri.PRO_CODIGO_BARRAS = cuenta.PRO_CODIGO_BARRAS;
                    cuentaOri.CUE_OBSERVACION = cuenta.CUE_OBSERVACION;
                    cuentaOri.MED_CODIGO = cuenta.MED_CODIGO;
                    cuentaOri.Id_Tipo_Medico = cuenta.Id_Tipo_Medico;
                    contexto.SaveChanges();
                }
            }
            catch (Exception err) { throw err; }
        }


        public CUENTAS_PACIENTES RecuperarCuentaId(int codCuenta)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from c in contexto.CUENTAS_PACIENTES
                            where c.CUE_CODIGO == codCuenta
                            select c).FirstOrDefault();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Método que elimina un registro de cuenta del paciente
        /// </summary>
        /// <param name="cuenta">Objeto CUENTAS_PACIENTES que se eliminara de la base de datos</param>
        public void EliminarCuenta(Int64 codCuenta)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    CUENTAS_PACIENTES cuentaOri = contexto.CUENTAS_PACIENTES.Where(c => c.CUE_CODIGO == codCuenta).FirstOrDefault();
                    if (cuentaOri != null)
                    {
                        contexto.DeleteObject(cuentaOri);
                        contexto.SaveChanges();
                    }
                }
            }
            catch (Exception err) { throw err; }
        }

        public void EliminarCuentaArea(int codPedido, int codAtencion)
        {
            //try
            //{
            //    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            //    {

            //        var listaCuenta = (from c in contexto.CUENTAS_PACIENTES
            //                           where c.ATE_CODIGO == codAtencion && c.PED_CODIGO == codPedido
            //                           select c);
            //        contexto.DeleteObject();
            //        //List<CUENTAS_PACIENTES> listaCuenta = contexto.CUENTAS_PACIENTES.Where(c => c.ATE_CODIGO == codAtencion && c.PED_CODIGO == codPedido).ToList();
            //        //contexto.DeleteObject(listaCuenta);
            //        //contexto.SaveChanges();
            //    }
            //}
            //catch (Exception err) { throw err; }
        }


        public void ActualizarFechaCuentas(int codAtencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var entityConnection = (EntityConnection)contexto.Connection;
                    DbConnection storeConnection = entityConnection.StoreConnection;
                    DbCommand command = storeConnection.CreateCommand();
                    command.CommandText = "sp_ActualizaFechaCuenta";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@p_codigoAtencion", codAtencion));
                    using (contexto.Connection.CreateConnectionScope())
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err) { throw err; }
        }

        /// <summary>
        /// Método para recuperar la cuenta de paciente
        /// </summary>
        /// <param name="codAtencion"></param>
        /// <returns>Retorna la lista de cuentas del paciente según la atención</returns>
        public List<CUENTAS_PACIENTES> RecuperarCuenta(Int64 codAtencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return
                        (from c in contexto.CUENTAS_PACIENTES
                         where c.ATE_CODIGO == codAtencion && c.CUE_ESTADO == 1
                         select c).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }


        public List<CUENTAS_PACIENTES> RecuperarCuentasRubros(int codAtencion, int codRubro)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return
                        (from c in contexto.CUENTAS_PACIENTES
                         where c.ATE_CODIGO == codAtencion && c.CUE_ESTADO == 1 && c.RUB_CODIGO == codRubro
                         select c).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }


        public CUENTAS_PACIENTES RecuperarCuentasIvaS(int codAtencion, string codIvaS)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return
                        (from c in contexto.CUENTAS_PACIENTES
                         where c.ATE_CODIGO == codAtencion && c.CUE_ESTADO == 1 && c.PRO_CODIGO_BARRAS == codIvaS
                         select c).FirstOrDefault();
                }
            }
            catch (Exception err) { throw err; }
        }

        public List<CUENTAS_PACIENTES> RecuperarCuentaArea(int codAtencion, int codArea)
        {
            // se cambia porque se repite insumos y medicamentos en impresion David Mantilla 10/02/2014
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return
                        (from c in contexto.CUENTAS_PACIENTES
                         where c.ATE_CODIGO == codAtencion && c.PED_CODIGO == codArea && c.CUE_ESTADO == 1 && c.CUE_CANTIDAD > 0 && c.RUB_CODIGO != 1 && c.RUB_CODIGO != 27
                         select c).OrderBy(c => c.CUE_CODIGO).ToList();
                }
            }
            catch (Exception err) { throw err; }

        }
        /*Aumento para no duplicar reporte de rubros Medicamentos y suministros*/
        public List<CUENTAS_PACIENTES> RecuperarCuentaArea2(int codAtencion, int codArea)
        {
            // se cambia porque se repite insumos y medicamentos en impresion David Mantilla 10/02/2014
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return
                        (from c in contexto.CUENTAS_PACIENTES
                         where c.ATE_CODIGO == codAtencion && c.PED_CODIGO == codArea && c.CUE_ESTADO == 1 && c.CUE_CANTIDAD > 0 //&& c.RUB_CODIGO != 1 && c.RUB_CODIGO != 27//
                         select c).OrderBy(c => c.CUE_CODIGO).ToList();
                }
            }
            catch (Exception err) { throw err; }

        }

        public int RecuperarCodigoPaquete()
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
            Sqlcmd = new SqlCommand("sp_SecuencialPaqueteCuentas", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            if (Dts.Tables["tabla"].Rows.Count > 0)
            {
                return Convert.ToInt32(Dts.Tables["tabla"].Rows[0][0]);
            }
            else
            {
                return 0;
            }


        }


        public Int64 RecuperarCodigoPaqueteRadicacion()
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
            Sqlcmd = new SqlCommand("sp_RecuperaSecuencialPaquete", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            if (Dts.Tables["tabla"].Rows.Count > 0)
            {
                return Convert.ToInt64(Dts.Tables["tabla"].Rows[0][0]);
            }
            else
            {
                return 0;
            }


        }

        public int CrearPaqueteCuentas(Int64 PaqueteId, string FechaControlPaquete, Int64 NumeroRadicacionPaquete, Int64 NumeroTramitePaquete, string Observacion, List<Int32> ListaAtenciones, Int32 Usuario)
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

            foreach (Int32 AtencionesCodigo in ListaAtenciones)
            {
                Sqlcmd = new SqlCommand("sp_CreaPaqueteCuentas", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Parameters.Add("@PaqueteId", SqlDbType.BigInt);
                Sqlcmd.Parameters["@PaqueteId"].Value = Convert.ToInt64(PaqueteId);

                Sqlcmd.Parameters.Add("@FechaControlPaquete", SqlDbType.DateTime);
                Sqlcmd.Parameters["@FechaControlPaquete"].Value = Convert.ToDateTime(FechaControlPaquete);

                Sqlcmd.Parameters.Add("@NumeroRadicacionPaquete", SqlDbType.BigInt);
                Sqlcmd.Parameters["@NumeroRadicacionPaquete"].Value = NumeroRadicacionPaquete;

                Sqlcmd.Parameters.Add("@NumeroTramitePaquete", SqlDbType.BigInt);
                Sqlcmd.Parameters["@NumeroTramitePaquete"].Value = NumeroTramitePaquete;

                Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.Int);
                Sqlcmd.Parameters["@ATE_CODIGO"].Value = AtencionesCodigo;

                Sqlcmd.Parameters.Add("@Observacion", SqlDbType.VarChar);
                Sqlcmd.Parameters["@Observacion"].Value = Observacion;

                Sqlcmd.Parameters.Add("@Id_usuario", SqlDbType.VarChar);
                Sqlcmd.Parameters["@Id_usuario"].Value = Usuario;

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;//ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts, "tabla");
            }

            return 0;
        }

        public int ActualizarPaquete(Int64 NumeroRadicacionPaquete, Int64 NumeroTramitePaquete, Int32 Id_Usuario)
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

            Sqlcmd = new SqlCommand("sp_ActualizaDatosPaquete", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@NumeroRadicacionPaquete", SqlDbType.BigInt);
            Sqlcmd.Parameters["@NumeroRadicacionPaquete"].Value = NumeroRadicacionPaquete;

            Sqlcmd.Parameters.Add("@NumeroTramitePaquete", SqlDbType.BigInt);
            Sqlcmd.Parameters["@NumeroTramitePaquete"].Value = NumeroTramitePaquete;

            Sqlcmd.Parameters.Add("@Id_Usuario", SqlDbType.Int);
            Sqlcmd.Parameters["@Id_Usuario"].Value = Id_Usuario;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;//ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            return 0;

        }

        public int EliminarCuentasPaquete(Int64 PaqueteId, List<Int32> ListaAtenciones, Int32 Id_Usuario)
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
            foreach (Int32 AtencionesCodigo in ListaAtenciones)
            {
                Sqlcmd = new SqlCommand("sp_EliminaCuentasPaquetes", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Parameters.Add("@PaqueteId", SqlDbType.BigInt);
                Sqlcmd.Parameters["@PaqueteId"].Value = PaqueteId;

                Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.Int);
                Sqlcmd.Parameters["@ATE_CODIGO"].Value = AtencionesCodigo;

                Sqlcmd.Parameters.Add("@Id_usuario", SqlDbType.Int);
                Sqlcmd.Parameters["@Id_usuario"].Value = Id_Usuario;

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;//ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts, "tabla");
            }

            return 0;

        }

        public int EliminarPaquete(Int64 PaqueteId, Int32 Id_Usuario)
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

            Sqlcmd = new SqlCommand("sp_EliminarPaquete", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@PaqueteId", SqlDbType.BigInt);
            Sqlcmd.Parameters["@PaqueteId"].Value = PaqueteId;

            Sqlcmd.Parameters.Add("@Id_usuario", SqlDbType.Int);
            Sqlcmd.Parameters["@Id_usuario"].Value = Id_Usuario;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;//ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            return 0;

        }

        public int VerificaAtencionesPaquete(Int32 NumeroAtencion)
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

            Sqlcmd = new SqlCommand("sp_VerificaAtencionesPaquetes", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@Ate_Codigo"].Value = Convert.ToInt64(NumeroAtencion);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;//ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            if (Dts.Tables["tabla"].Rows.Count > 0)
            {
                return Convert.ToInt32(Dts.Tables["tabla"].Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }


        #endregion

        #region mantenimiento cuenta paciente

        #endregion

        public List<ESTADOS_CUENTA> RecuperarEstadosCuenta()
        {
            try
            {
                using (var context = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from c in context.ESTADOS_CUENTA
                            where c.ESC_ESTADO == true
                            select c).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }

        public DataTable ValoresHabitacionUCI(Int64 ateCodigo)
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

            Sqlcmd = new SqlCommand("sp_ValoresHabitacionUCI", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ateCodigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ateCodigo"].Value = ateCodigo;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        public DataTable ValoresHabitacionUCIxFecha(Int64 ateCodigo, DateTime f_Ingreso, DateTime f_Fin)
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

            Sqlcmd = new SqlCommand("sp_ValoresHabitacionUCIxFexha", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ateCodigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ateCodigo"].Value = ateCodigo;

            Sqlcmd.Parameters.Add("@f_Ingreso", SqlDbType.Date);
            Sqlcmd.Parameters["@f_Ingreso"].Value = f_Ingreso;

            Sqlcmd.Parameters.Add("@f_Fin", SqlDbType.Date);
            Sqlcmd.Parameters["@f_Fin"].Value = f_Fin;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        public void GuardaTotalUCI(Int64 ateCodigo, int CodigoUsuario, int NumeroDias, int HAB_CODIGO, int CodigoConvenio)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;

            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            transaction = Sqlcon.BeginTransaction();

            try
            {
                Sqlcmd = new SqlCommand("sp_GuardaTotalUCI", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@ateCodigo", SqlDbType.BigInt);
                Sqlcmd.Parameters["@ateCodigo"].Value = ateCodigo;

                Sqlcmd.Parameters.Add("@p_CodigoUsuario", SqlDbType.Int);
                Sqlcmd.Parameters["@p_CodigoUsuario"].Value = CodigoUsuario;

                Sqlcmd.Parameters.Add("@p_Dias", SqlDbType.Int);
                Sqlcmd.Parameters["@p_Dias"].Value = NumeroDias;

                Sqlcmd.Parameters.Add("@HAB_CODIGO", SqlDbType.Int);
                Sqlcmd.Parameters["@HAB_CODIGO"].Value = HAB_CODIGO;

                Sqlcmd.Parameters.Add("@CodigoConvenio", SqlDbType.Int);
                Sqlcmd.Parameters["@CodigoConvenio"].Value = CodigoConvenio;

                Sqldap = new SqlDataAdapter();
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts, "tabla");

                transaction.Commit();

            }

            catch (Exception ex)
            {
                transaction.Rollback();
            }

        }

        public void GuardaTotalUCIxFecha(Int64 ateCodigo, int NumeroDias, int CodigoConvenio)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;

            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            transaction = Sqlcon.BeginTransaction();

            try
            {
                Sqlcmd = new SqlCommand("sp_GuardaTotalUCIxFecha", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@ateCodigo", SqlDbType.BigInt);
                Sqlcmd.Parameters["@ateCodigo"].Value = ateCodigo;

                Sqlcmd.Parameters.Add("@p_Dias", SqlDbType.Int);
                Sqlcmd.Parameters["@p_Dias"].Value = NumeroDias;

                Sqlcmd.Parameters.Add("@CodigoConvenio", SqlDbType.Int);
                Sqlcmd.Parameters["@CodigoConvenio"].Value = CodigoConvenio;

                Sqldap = new SqlDataAdapter();
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts, "tabla");

                transaction.Commit();

            }

            catch (Exception ex)
            {
                transaction.Rollback();
            }

        }

        public DataTable ValoresHabitacion(Int64 ateCodigo)
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

            Sqlcmd = new SqlCommand("sp_ValoresHabitacion", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ateCodigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ateCodigo"].Value = ateCodigo;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }


        public DataTable ValoresHabitacionxFecha(Int64 ateCodigo, DateTime f_Ingreso, DateTime f_fin)
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

            Sqlcmd = new SqlCommand("sp_ValoresHabitacionxFecha", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ateCodigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ateCodigo"].Value = ateCodigo;

            Sqlcmd.Parameters.Add("@f_Ingreso", SqlDbType.Date);
            Sqlcmd.Parameters["@f_Ingreso"].Value = f_Ingreso;

            Sqlcmd.Parameters.Add("@f_Fin", SqlDbType.Date);
            Sqlcmd.Parameters["@f_Fin"].Value = f_fin;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        public DataTable ValidaHabitacion(int p_Convenio, string p_Habitacion)
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

            Sqlcmd = new SqlCommand("sp_ValidaHabitacion", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_Convenio", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Convenio"].Value = p_Convenio;

            Sqlcmd.Parameters.Add("@p_Habitacion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_Habitacion"].Value = p_Habitacion;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        public int RecuperaCodigoHabitacion(Int64 ateCodigo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    int codigo = (from a in contexto.ATENCIONES
                                  where a.ATE_CODIGO == ateCodigo
                                  select a.HABITACIONES.hab_Codigo).FirstOrDefault();
                    return codigo;
                }
            }
            catch (Exception err) { throw err; }
        }

        public List<ASEGURADORAS_EMPRESAS> RecuperarCategoriasConvenios()
        {
            try
            {
                using (var context = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from c in context.ASEGURADORAS_EMPRESAS
                            select c).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }

        public List<DtoCuentasPacientes> recuperarListaCuentaAtencion1(string id, string historia, string nombre, string habitacion, int cantidad, int estado)
        {
            try
            {
                var mes = DateTime.Now.Month;
                var año = DateTime.Now.Year;
                var fecha = Convert.ToDateTime("01-" + mes + "-" + año);
                List<DtoCuentasPacientes> lista = new List<DtoCuentasPacientes>();
                //List<DtoPacientesEmergencia> pacientes = new List<DtoPacientesEmergencia>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    if (id == string.Empty && historia == string.Empty && nombre == string.Empty && habitacion == string.Empty)
                    {
                        //lista de pacientes con atenciones activas
                        return (from a in contexto.ATENCIONES
                                    //join c in contexto.CUENTAS_PACIENTES on a.ATE_CODIGO equals c.ATE_CODIGO
                                join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                                join h in contexto.HABITACIONES on a.HABITACIONES.hab_Codigo equals h.hab_Codigo
                                join d in contexto.ATENCION_DETALLE_CATEGORIAS on a.ATE_CODIGO equals d.ATENCIONES.ATE_CODIGO
                                //where a.ESC_CODIGO <= 2 && (a.ATE_FECHA_INGRESO > fecha) && (d.CATEGORIAS_CONVENIOS.CAT_CODIGO == 21 || d.CATEGORIAS_CONVENIOS.CAT_CODIGO == 54 || d.CATEGORIAS_CONVENIOS.CAT_CODIGO == 125 || d.CATEGORIAS_CONVENIOS.CAT_CODIGO == 62) //c.CUE_ESTADO == 1//&& (a.ATE_FECHA_ALTA != null) (21 iess / 54 MSP / 62 convenio fonsat /125 Convenio SOAT)
                                where a.ESC_CODIGO == 7 //&& (a.ATE_FECHA_INGRESO > fecha) && (d.CATEGORIAS_CONVENIOS.CAT_CODIGO > 8) //c.CUE_ESTADO == 1//&& (a.ATE_FECHA_ALTA != null) (21 iess / 54 MSP / 62 convenio fonsat /125 Convenio SOAT)
                                                        //ANTES ESTABA a.ESC_CODIGO <= 2  CAMBIOS EDGAR 20201218
                                select new DtoCuentasPacientes
                                {
                                    CODIGO = pa.PAC_CODIGO,
                                    ATE_CODIGO = a.ATE_CODIGO,
                                    PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
                                    PED_FECHA = a.ATE_FECHA_INGRESO.Value,
                                    PED_FECHA_ALTA = a.ATE_FECHA_ALTA.Value != null ? a.ATE_FECHA_ALTA.Value : DateTime.Now,
                                    PRO_CODIGO = pa.PAC_CODIGO,
                                    IDENTIFICACION = pa.PAC_IDENTIFICACION,
                                    HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
                                    HABITACION = h.hab_Numero != null ? h.hab_Numero : "",
                                    FACTURA = a.ATE_FACTURA_PACIENTE != null ? a.ATE_FACTURA_PACIENTE : "",
                                    NUMCONTROL = a.ATE_NUMERO_CONTROL != null ? a.ATE_NUMERO_CONTROL : "",
                                    FECHAFACTURA = a.ATE_FACTURA_FECHA.Value != null ? a.ATE_FACTURA_FECHA.Value : DateTime.Now,
                                    REFERIDO = a.ATE_REFERIDO.Value,
                                    ESC_CODIGO = a.ESC_CODIGO.Value
                                }).ToList();//OrderByDescending(a => a.PED_FECHA);

                        //result = result.OrderByDescending(a => a.PED_FECHA);
                        //return result.Take(cantidad).ToList();

                    }
                    else
                    {
                        var result = (from a in contexto.ATENCIONES
                                          //join c in contexto.CUENTAS_PACIENTES on a.ATE_CODIGO equals c.ATE_CODIGO
                                      join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                                      join h in contexto.HABITACIONES on a.HABITACIONES.hab_Codigo equals h.hab_Codigo
                                      join d in contexto.ATENCION_DETALLE_CATEGORIAS on a.ATE_CODIGO equals d.ATENCIONES.ATE_CODIGO
                                      where a.ESC_CODIGO <= 2 && (a.ATE_FECHA_INGRESO > fecha) && (d.CATEGORIAS_CONVENIOS.CAT_CODIGO == 21 || d.CATEGORIAS_CONVENIOS.CAT_CODIGO == 54 || d.CATEGORIAS_CONVENIOS.CAT_CODIGO == 125 || d.CATEGORIAS_CONVENIOS.CAT_CODIGO == 62) //c.CUE_ESTADO == 1//&& (a.ATE_FECHA_ALTA != null) (21 iess / 54 MSP / 62 convenio fonsat /125 Convenio SOAT)
                                      select new DtoCuentasPacientes
                                      {
                                          CODIGO = pa.PAC_CODIGO,
                                          ATE_CODIGO = a.ATE_CODIGO,
                                          PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
                                          PED_FECHA = a.ATE_FECHA_INGRESO.Value,
                                          PED_FECHA_ALTA = a.ATE_FECHA_ALTA.Value != null ? a.ATE_FECHA_ALTA.Value : DateTime.Now,
                                          PRO_CODIGO = pa.PAC_CODIGO,
                                          IDENTIFICACION = pa.PAC_IDENTIFICACION,
                                          HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
                                          HABITACION = h.hab_Numero != null ? h.hab_Numero : "",
                                          FACTURA = a.ATE_FACTURA_PACIENTE != null ? a.ATE_FACTURA_PACIENTE : "",
                                          NUMCONTROL = a.ATE_NUMERO_CONTROL != null ? a.ATE_NUMERO_CONTROL : "",
                                          FECHAFACTURA = a.ATE_FACTURA_FECHA.Value != null ? a.ATE_FACTURA_FECHA.Value : DateTime.Now,
                                          REFERIDO = a.ATE_REFERIDO.Value,
                                          ESC_CODIGO = a.ESC_CODIGO.Value
                                      });

                        if (id != string.Empty)
                            result = result.Where(pa => (pa.IDENTIFICACION).StartsWith(id));

                        if (historia != string.Empty)
                            result = result.Where(pa => (pa.HISTORIA_CLINICA).Trim().Contains(historia));

                        if (nombre != string.Empty)
                        {
                            string[] cadena = nombre.Split();
                            if (cadena.Length == 1)
                            {
                                string n = cadena[0].Trim();
                                var porApellidoPaterno = result.Where(pa => (pa.PACIENTE).StartsWith(n)).OrderBy(pa => pa.PACIENTE).Take(cantidad).ToList();
                                if (porApellidoPaterno.Count == 0)
                                {
                                    var porNombreUno = result.Where(pa => (pa.PACIENTE).StartsWith(n)).OrderBy(pa => pa.PACIENTE).Take(cantidad).ToList();
                                    if (porNombreUno.Count > 0)
                                    {
                                        return porNombreUno;
                                    }
                                }
                                else
                                {
                                    return porApellidoPaterno;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < cadena.Length; i++)
                                {
                                    string n = cadena[i].Trim();
                                    result = result.Where(pa => (pa.PACIENTE).Contains(n)).Distinct();
                                }
                            }
                        }
                        if (habitacion != string.Empty)
                        {
                            cantidad = 10;
                            result = result.Where(pa => (pa.HABITACION).Trim().Contains(habitacion)).Take(cantidad).Distinct();

                            return result.OrderByDescending(a => a.PED_FECHA_ALTA).ToList();
                        }
                        return result.OrderByDescending(a => a.PED_FECHA).Take(cantidad).Distinct().ToList();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        //Pablo Rocha sp para generar valores de las cuentas
        public DataTable GeneraValoresCuentas(string Fecha1, string Fecha2, int Area, int Rubro, int todosRubros)
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

            Sqlcmd = new SqlCommand("sp_ListaValoresCuenta", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Area", SqlDbType.Int);
            Sqlcmd.Parameters["@Area"].Value = Area;

            Sqlcmd.Parameters.Add("@SubArea", SqlDbType.Int);
            Sqlcmd.Parameters["@SubArea"].Value = Rubro;

            Sqlcmd.Parameters.Add("@Fecha1", SqlDbType.Date);
            Sqlcmd.Parameters["@Fecha1"].Value = Fecha1;

            Sqlcmd.Parameters.Add("@Fecha2", SqlDbType.Date);
            Sqlcmd.Parameters["@Fecha2"].Value = Fecha2;

            Sqlcmd.Parameters.Add("@todosRubros", SqlDbType.Int);
            Sqlcmd.Parameters["@todosRubros"].Value = todosRubros;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 360;
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        //Extrae dataset con forma de pago desde el SIC3000 28/06/2013 Pablo Rocha
        public DataTable FormaPagoSic(string Factura)
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
            Sqlcmd = new SqlCommand("sp_FormaPago", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Factura", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Factura"].Value = Factura;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 360;
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            Sqlcon.Close();

            return Dts.Tables["tabla"];



        }

        #region Estados de Cuenta
        public DataTable Datos_reporte(string fechaIni, string fechaFin, int estado)
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
            Sqlcmd = new SqlCommand("sp_reporte_PRUEBA", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaInicio"].Value = Convert.ToDateTime(fechaIni);

            Sqlcmd.Parameters.Add("@FechaFin", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaFin"].Value = Convert.ToDateTime(fechaFin);

            Sqlcmd.Parameters.Add("@estadofactura", SqlDbType.Int);
            Sqlcmd.Parameters["@estadofactura"].Value = estado;

            Sqlcmd.Parameters.Add("@consulta", SqlDbType.Int);
            Sqlcmd.Parameters["@consulta"].Value = 5;

            Sqlcmd.Parameters.Add("@atencion", SqlDbType.Xml);
            Sqlcmd.Parameters["@atencion"].Value = 1;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;//ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }
        public void actualizarEstadofactura(Int32 codAtencion, Int16 estado)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ATENCIONES atenciones = contexto.ATENCIONES.FirstOrDefault(p => p.ATE_CODIGO == codAtencion);
                atenciones.ESC_CODIGO = estado;
                contexto.SaveChanges();
            }
        }
        public void actualizarCuentasPaciente(Int32 atencionActual, Int32 atencionesC)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ATENCIONES atenciones = contexto.ATENCIONES.FirstOrDefault(p => p.ATE_CODIGO == atencionesC);
                atenciones.ESC_CODIGO = 10;
                contexto.SaveChanges();
            }
        }

        public void ActualizaFechasCuenta(int CodigoAtencion, int CodigoPedidoArea, int CodigoRubro, DateTime Fecha, int CodigoUsuario)// Permite actualizar las fechas de una cuenta en forma masiva // Giovanny Tapia // 15/01/2013
        {
            // VERIFICA SI UN USUARIO TIENES LOS PERMISOS NECESARIOS PARA CAMBIAR EL ESTADO DE UNA CUENTA / GIOVANNY TAPIA / 07/08/2012

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
            Sqlcmd = new SqlCommand("sp_ActualizaFechasCuenta", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = CodigoAtencion;

            Sqlcmd.Parameters.Add("@CodigoPedidoArea", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoPedidoArea"].Value = CodigoPedidoArea;

            Sqlcmd.Parameters.Add("@CodigoRubro", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoRubro"].Value = CodigoRubro;

            Sqlcmd.Parameters.Add("@Fecha", SqlDbType.DateTime);
            Sqlcmd.Parameters["@Fecha"].Value = Fecha;

            Sqlcmd.Parameters.Add("@Usuario", SqlDbType.Int);
            Sqlcmd.Parameters["@Usuario"].Value = CodigoUsuario;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            //if (Dts.Tables["tabla"].Rows.Count > 0)
            //{
            //    return 1;
            //}
            //else
            //{
            //    return 0;
            //}
        }

        public void actualizarCuenta(string atencionA, string atencioC)
        {
            string fecha = Convert.ToString(DateTime.Now);
            string usuario = His.Entidades.Clases.Sesion.nomUsuario;
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlCommand SqlGuardar;
            string cadena_sql = "UPDATE CUENTAS_PACIENTES SET ATE_CODIGO=" + atencionA.Trim() + " WHERE ATE_CODIGO=" + atencioC.Trim();
            string cadena_guardar = "insert into CUENTAS_CONSOLIDADAS VALUES('" + atencioC + "','" + atencionA + "','" + fecha + "','" + usuario + "',1)";
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
            SqlGuardar = new SqlCommand(cadena_guardar, Sqlcon);
            try
            {
                Sqlcmd.ExecuteNonQuery();
                SqlGuardar.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);



            }


        }

        public void actualizarCuentaNumFac(Int64 atencion, string numFac)
        {
            string fecha = Convert.ToString(DateTime.Now);
            string usuario = His.Entidades.Clases.Sesion.nomUsuario;
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            string cadena_sql = "UPDATE CUENTAS_PACIENTES SET CUE_NUM_FAC=" + numFac + " WHERE ATE_CODIGO=" + atencion;
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
            }
        }

        public void actualizarProseso(string codAtencion, string estado)
        {
            Int32 codigo = Convert.ToInt32(codAtencion.Trim());
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ATENCIONES atenciones = contexto.ATENCIONES.FirstOrDefault(p => p.ATE_CODIGO == codigo);
                atenciones.CUE_ESTADO = estado;
                contexto.SaveChanges();
            }
        }
        public DataTable CuentasAtenciones(string atencion)
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
            Sqlcmd = new SqlCommand("sp_reporte", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaInicio"].Value = Convert.ToDateTime("01/01/2012");

            Sqlcmd.Parameters.Add("@FechaFin", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaFin"].Value = Convert.ToDateTime("01/01/2012");

            Sqlcmd.Parameters.Add("@estadofactura", SqlDbType.Int);
            Sqlcmd.Parameters["@estadofactura"].Value = 3;

            Sqlcmd.Parameters.Add("@consulta", SqlDbType.Int);
            Sqlcmd.Parameters["@consulta"].Value = 3;

            Sqlcmd.Parameters.Add("@atencion", SqlDbType.Xml);
            Sqlcmd.Parameters["@atencion"].Value = atencion;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }
        public DataTable DatosExportar(string atencion)
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
            Sqlcmd = new SqlCommand("sp_reporte", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaInicio"].Value = Convert.ToDateTime("01/01/2012");

            Sqlcmd.Parameters.Add("@FechaFin", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaFin"].Value = Convert.ToDateTime("01/01/2012");

            Sqlcmd.Parameters.Add("@estadofactura", SqlDbType.Int);
            Sqlcmd.Parameters["@estadofactura"].Value = 3;

            Sqlcmd.Parameters.Add("@consulta", SqlDbType.Int);
            Sqlcmd.Parameters["@consulta"].Value = 4;

            Sqlcmd.Parameters.Add("@atencion", SqlDbType.Xml);
            Sqlcmd.Parameters["@atencion"].Value = atencion;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        public DataTable DatosExportarAseguradora(string atencion, int CodigoAseguradora)
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
            Sqlcmd = new SqlCommand("sp_reporte_Aseguradora", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaInicio"].Value = Convert.ToDateTime("01/01/2012");

            Sqlcmd.Parameters.Add("@FechaFin", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaFin"].Value = Convert.ToDateTime("01/01/2012");

            Sqlcmd.Parameters.Add("@estadofactura", SqlDbType.Int);
            Sqlcmd.Parameters["@estadofactura"].Value = 3;

            Sqlcmd.Parameters.Add("@consulta", SqlDbType.Int);
            Sqlcmd.Parameters["@consulta"].Value = 4;

            Sqlcmd.Parameters.Add("@atencion", SqlDbType.Xml);
            Sqlcmd.Parameters["@atencion"].Value = atencion;

            Sqlcmd.Parameters.Add("@Aseguradora", SqlDbType.Int);
            Sqlcmd.Parameters["@Aseguradora"].Value = CodigoAseguradora;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        public DataTable AtencionesCuentas(string fechaIni, string fechaFin, int estadoFactura, string Nombre, string HC, string atencion, int TipoSeguro)
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

            //if (His.Entidades.Clases.Sesion.codUsuario.ToString() == "2274" || His.Entidades.Clases.Sesion.codUsuario.ToString() == "2229") // este proceso solo sirve para victoria y doris / Giovanny Tapia /31/08/2012
            //{
            //    Sqlcmd = new SqlCommand("sp_reporte_PRUEBA1_Evaluacion", Sqlcon);

            //}
            //else
            //{
            Sqlcmd = new SqlCommand("sp_reporte_PRUEBA1", Sqlcon);
            //}

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@FechaInicio", SqlDbType.Date);
            Sqlcmd.Parameters["@FechaInicio"].Value = Convert.ToDateTime(fechaIni);

            Sqlcmd.Parameters.Add("@FechaFin", SqlDbType.Date);
            Sqlcmd.Parameters["@FechaFin"].Value = Convert.ToDateTime(fechaFin);

            Sqlcmd.Parameters.Add("@estadofactura", SqlDbType.Int);
            Sqlcmd.Parameters["@estadofactura"].Value = estadoFactura;

            Sqlcmd.Parameters.Add("@consulta", SqlDbType.Int);
            Sqlcmd.Parameters["@consulta"].Value = 2;

            Sqlcmd.Parameters.Add("@atencion", SqlDbType.Xml);
            Sqlcmd.Parameters["@atencion"].Value = 1;

            Sqlcmd.Parameters.Add("@Nombres", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Nombres"].Value = Nombre;

            Sqlcmd.Parameters.Add("@HC", SqlDbType.VarChar);
            Sqlcmd.Parameters["@HC"].Value = HC;

            Sqlcmd.Parameters.Add("@NumeroAtencion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@NumeroAtencion"].Value = atencion;

            Sqlcmd.Parameters.Add("@TipoSeguro", SqlDbType.Int);
            Sqlcmd.Parameters["@TipoSeguro"].Value = TipoSeguro;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        public DataTable AtencionesCuentas_Paquetes(DateTime fechaIni, DateTime fechaFin, int estadoFactura, string Nombre, string HC, string atencion, int TipoSeguro)
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

            if (His.Entidades.Clases.Sesion.codUsuario.ToString() == "2274" || His.Entidades.Clases.Sesion.codUsuario.ToString() == "2229") // este proceso solo sirve para victoria y doris / Giovanny Tapia /31/08/2012
            {
                Sqlcmd = new SqlCommand("sp_reporte_PRUEBA1_Evaluacion", Sqlcon);

            }
            else
            {
                Sqlcmd = new SqlCommand("sp_reporte_PRUEBA1_paquete", Sqlcon);
            }

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaInicio"].Value = fechaIni;

            Sqlcmd.Parameters.Add("@FechaFin", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaFin"].Value = fechaFin;

            Sqlcmd.Parameters.Add("@estadofactura", SqlDbType.Int);
            Sqlcmd.Parameters["@estadofactura"].Value = estadoFactura;

            Sqlcmd.Parameters.Add("@consulta", SqlDbType.Int);
            Sqlcmd.Parameters["@consulta"].Value = 2;

            Sqlcmd.Parameters.Add("@atencion", SqlDbType.Xml);
            Sqlcmd.Parameters["@atencion"].Value = 1;

            Sqlcmd.Parameters.Add("@Nombres", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Nombres"].Value = Nombre;

            Sqlcmd.Parameters.Add("@HC", SqlDbType.VarChar);
            Sqlcmd.Parameters["@HC"].Value = HC;

            Sqlcmd.Parameters.Add("@NumeroAtencion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@NumeroAtencion"].Value = atencion;

            //Sqlcmd.Parameters.Add("@TipoSeguro", SqlDbType.Int);
            //Sqlcmd.Parameters["@TipoSeguro"].Value = TipoSeguro;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        public DataTable AtencionesCuentasPaquete(Int64 NumeroTramite)
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

            Sqlcmd = new SqlCommand("sp_CargaCuentasPaquetes", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@NumeroTramite", SqlDbType.BigInt);
            Sqlcmd.Parameters["@NumeroTramite"].Value = NumeroTramite;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        public DataTable RecuperarPaquete(Int64 NumeroTramite)
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

            Sqlcmd = new SqlCommand("sp_RecuperaPaquete", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@NumeroRadicacionPaquete", SqlDbType.BigInt);
            Sqlcmd.Parameters["@NumeroRadicacionPaquete"].Value = NumeroTramite;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        public DataTable DetalleCuentasModificadas(string fechaIni, string fechaFin, string HC, string atencion, string area)
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


            Sqlcmd = new SqlCommand("sp_cuentasmodificadas", Sqlcon);
            //}

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@HC", SqlDbType.VarChar);
            Sqlcmd.Parameters["@HC"].Value = HC;

            Sqlcmd.Parameters.Add("@NumeroAtencion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@NumeroAtencion"].Value = 1;

            Sqlcmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaInicio"].Value = Convert.ToDateTime(fechaIni);

            Sqlcmd.Parameters.Add("@FechaFin", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaFin"].Value = Convert.ToDateTime(fechaFin);

            Sqlcmd.Parameters.Add("@area", SqlDbType.VarChar);
            Sqlcmd.Parameters["@area"].Value = area;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }


        public DataTable AtencionesCuentasTodos(string fechaIni, string fechaFin, int estadoFactura, string Nombre, string HC, string atencion, int TipoSeguro)
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
            //if (His.Entidades.Clases.Sesion.codUsuario.ToString() == "2274" || His.Entidades.Clases.Sesion.codUsuario.ToString() == "2229") // este proceso solo sirve para victoria y doris / Giovanny Tapia /31/08/2012
            //{
            //    Sqlcmd = new SqlCommand("sp_reporte_todos_Evaluacion", Sqlcon);

            //}
            //else 
            //{
            Sqlcmd = new SqlCommand("sp_reporte_todos", Sqlcon);
            //}

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaInicio"].Value = Convert.ToDateTime(fechaIni);

            Sqlcmd.Parameters.Add("@FechaFin", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaFin"].Value = Convert.ToDateTime(fechaFin);

            Sqlcmd.Parameters.Add("@estadofactura", SqlDbType.Int);
            Sqlcmd.Parameters["@estadofactura"].Value = estadoFactura;

            Sqlcmd.Parameters.Add("@consulta", SqlDbType.Int);
            Sqlcmd.Parameters["@consulta"].Value = 2;

            Sqlcmd.Parameters.Add("@atencion", SqlDbType.Xml);
            Sqlcmd.Parameters["@atencion"].Value = 1;

            Sqlcmd.Parameters.Add("@Nombres", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Nombres"].Value = Nombre;

            Sqlcmd.Parameters.Add("@HC", SqlDbType.VarChar);
            Sqlcmd.Parameters["@HC"].Value = HC;

            Sqlcmd.Parameters.Add("@NumeroAtencion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@NumeroAtencion"].Value = atencion;

            Sqlcmd.Parameters.Add("@TipoSeguro", SqlDbType.Int);
            Sqlcmd.Parameters["@TipoSeguro"].Value = TipoSeguro;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        public DataTable AtencionesCuentasTodosPaquete(DateTime fechaIni, DateTime fechaFin, int estadoFactura, string Nombre, string HC, string atencion, int TipoSeguro)
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
            //if (His.Entidades.Clases.Sesion.codUsuario.ToString() == "2274" || His.Entidades.Clases.Sesion.codUsuario.ToString() == "2229") // este proceso solo sirve para victoria y doris / Giovanny Tapia /31/08/2012
            //{
            //    Sqlcmd = new SqlCommand("sp_reporte_todos_Evaluacion", Sqlcon);

            //}
            //else 
            //{
            Sqlcmd = new SqlCommand("sp_reporte_todos_paquete", Sqlcon);
            //}

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaInicio"].Value = fechaIni;

            Sqlcmd.Parameters.Add("@FechaFin", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaFin"].Value = fechaFin;

            Sqlcmd.Parameters.Add("@estadofactura", SqlDbType.Int);
            Sqlcmd.Parameters["@estadofactura"].Value = estadoFactura;

            Sqlcmd.Parameters.Add("@consulta", SqlDbType.Int);
            Sqlcmd.Parameters["@consulta"].Value = 2;

            Sqlcmd.Parameters.Add("@atencion", SqlDbType.Xml);
            Sqlcmd.Parameters["@atencion"].Value = 1;

            Sqlcmd.Parameters.Add("@Nombres", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Nombres"].Value = Nombre;

            Sqlcmd.Parameters.Add("@HC", SqlDbType.VarChar);
            Sqlcmd.Parameters["@HC"].Value = HC;

            Sqlcmd.Parameters.Add("@NumeroAtencion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@NumeroAtencion"].Value = atencion;

            Sqlcmd.Parameters.Add("@TipoSeguro", SqlDbType.Int);
            Sqlcmd.Parameters["@TipoSeguro"].Value = TipoSeguro;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }


        public int PermisosActualizacionCuentas(int Usuario, int CuentaDesde, int CuentaHacia)
        {
            // VERIFICA SI UN USUARIO TIENES LOS PERMISOS NECESARIOS PARA CAMBIAR EL ESTADO DE UNA CUENTA / GIOVANNY TAPIA / 07/08/2012

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
            Sqlcmd = new SqlCommand("sp_VerificaPermisosCambioCuentas", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Usuario", SqlDbType.Int);
            Sqlcmd.Parameters["@Usuario"].Value = Usuario;

            Sqlcmd.Parameters.Add("@EstadoDesde", SqlDbType.Int);
            Sqlcmd.Parameters["@EstadoDesde"].Value = CuentaDesde;

            Sqlcmd.Parameters.Add("@EstadoHacia", SqlDbType.Int);
            Sqlcmd.Parameters["@EstadoHacia"].Value = CuentaHacia;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            if (Dts.Tables["tabla"].Rows.Count > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }



        public DataTable AtencionesValorTotal(string atencion)
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
            string sql = "  SELECT SUM(CUE_VALOR) AS   valor    FROM CUENTAS_PACIENTES WHERE ATE_CODIGO=" + atencion.Trim();
            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        public DataTable AtencionesConsolidar(string hc)
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
            Sqlcmd = new SqlCommand("sp_ProcesoConsolidar", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@historia", SqlDbType.VarChar);
            Sqlcmd.Parameters["@historia"].Value = hc.Trim();
            Sqlcmd.Parameters.Add("@consulta", SqlDbType.Int);
            Sqlcmd.Parameters["@consulta"].Value = 1;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        //public DataTable GeneraValoresCuentas(string Fecha1, string Fecha2, int Area, int Rubro)
        //{
        //    SqlConnection Sqlcon;
        //    SqlCommand Sqlcmd;
        //    SqlDataAdapter Sqldap;
        //    DataSet Dts;
        //    BaseContextoDatos obj = new BaseContextoDatos();
        //    Sqlcon = obj.ConectarBd();
        //    try
        //    {
        //        Sqlcon.Open();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    Sqlcmd = new SqlCommand("sp_ListaValoresCuenta", Sqlcon);
        //    Sqlcmd.CommandType = CommandType.StoredProcedure;

        //    Sqlcmd.Parameters.Add("@Area", SqlDbType.Int);
        //    Sqlcmd.Parameters["@Area"].Value = Area;

        //    Sqlcmd.Parameters.Add("@SubArea", SqlDbType.Int);
        //    Sqlcmd.Parameters["@SubArea"].Value = Rubro;

        //    Sqlcmd.Parameters.Add("@Fecha1", SqlDbType.VarChar);
        //    Sqlcmd.Parameters["@Fecha1"].Value = Fecha1;

        //    Sqlcmd.Parameters.Add("@Fecha2", SqlDbType.VarChar);
        //    Sqlcmd.Parameters["@Fecha2"].Value = Fecha2;

        //    Sqldap = new SqlDataAdapter();
        //    Sqldap.SelectCommand = Sqlcmd;
        //    Dts = new DataSet();
        //    Sqldap.Fill(Dts, "tabla");
        //    return Dts.Tables["tabla"];

        //}

        public void GeneraValoresautomaticos(Int64 CodigoAtencion, Int32 CodigoUsuario, Int32 NumeroDias, string Habitacion, Int32 Aseguradora, Int32 Empresa)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;

            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            transaction = Sqlcon.BeginTransaction();
            int numHabitacion = Habitacion.Length;
            Habitacion = Habitacion.Substring(numHabitacion - 3, 3);
            try
            {
                Sqlcmd = new SqlCommand("sp_ValoresAutomaticosCuentas", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.Int);
                Sqlcmd.Parameters["@p_CodigoAtencion"].Value = CodigoAtencion;

                Sqlcmd.Parameters.Add("@p_CodigoUsuario", SqlDbType.Int);
                Sqlcmd.Parameters["@p_CodigoUsuario"].Value = CodigoUsuario;

                Sqlcmd.Parameters.Add("@p_Dias", SqlDbType.Int);
                Sqlcmd.Parameters["@p_Dias"].Value = NumeroDias;

                Sqlcmd.Parameters.Add("@p_Habitacion", SqlDbType.VarChar);
                Sqlcmd.Parameters["@p_Habitacion"].Value = Habitacion;

                Sqlcmd.Parameters.Add("@p_Convenio", SqlDbType.Int);
                Sqlcmd.Parameters["@p_Convenio"].Value = Aseguradora;

                Sqlcmd.Parameters.Add("@p_Empresa", SqlDbType.Int);
                Sqlcmd.Parameters["@p_Empresa"].Value = Empresa;

                Sqldap = new SqlDataAdapter();
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts, "tabla");

                transaction.Commit();

            }

            catch (Exception ex)
            {
                transaction.Rollback();
            }

        }

        public void GeneraValoresautomaticosxFechas(Int64 CodigoAtencion, Int32 CodigoUsuario, Int32 NumeroDias, string Habitacion, Int32 Aseguradora, Int32 Empresa)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;

            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            transaction = Sqlcon.BeginTransaction();
            int numHabitacion = Habitacion.Length;
            Habitacion = Habitacion.Substring(numHabitacion - 3, 3);
            try
            {
                Sqlcmd = new SqlCommand("sp_ValoresAutomaticosCuentasxFechas", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.Int);
                Sqlcmd.Parameters["@p_CodigoAtencion"].Value = CodigoAtencion;

                Sqlcmd.Parameters.Add("@p_CodigoUsuario", SqlDbType.Int);
                Sqlcmd.Parameters["@p_CodigoUsuario"].Value = CodigoUsuario;

                Sqlcmd.Parameters.Add("@p_Dias", SqlDbType.Int);
                Sqlcmd.Parameters["@p_Dias"].Value = NumeroDias;

                Sqlcmd.Parameters.Add("@p_Habitacion", SqlDbType.VarChar);
                Sqlcmd.Parameters["@p_Habitacion"].Value = Habitacion;

                Sqlcmd.Parameters.Add("@p_Convenio", SqlDbType.Int);
                Sqlcmd.Parameters["@p_Convenio"].Value = Aseguradora;

                Sqlcmd.Parameters.Add("@p_Empresa", SqlDbType.Int);
                Sqlcmd.Parameters["@p_Empresa"].Value = Empresa;

                Sqldap = new SqlDataAdapter();
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts, "tabla");

                transaction.Commit();

            }

            catch (Exception ex)
            {
                transaction.Rollback();
            }

        }

        public void AdministracionMedicamentos(Int64 CodigoAtencion, Int32 CodigoUsuario, Int32 Aseguradora, Int32 Empresa)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;

            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            transaction = Sqlcon.BeginTransaction();
            try
            {
                Sqlcmd = new SqlCommand("sp_AdministracionMedicamentos", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.Int);
                Sqlcmd.Parameters["@p_CodigoAtencion"].Value = CodigoAtencion;

                Sqlcmd.Parameters.Add("@p_CodigoUsuario", SqlDbType.Int);
                Sqlcmd.Parameters["@p_CodigoUsuario"].Value = CodigoUsuario;

                Sqlcmd.Parameters.Add("@p_Convenio", SqlDbType.Int);
                Sqlcmd.Parameters["@p_Convenio"].Value = Aseguradora;

                Sqlcmd.Parameters.Add("@p_Empresa", SqlDbType.Int);
                Sqlcmd.Parameters["@p_Empresa"].Value = Empresa;

                Sqldap = new SqlDataAdapter();
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts, "tabla");

                transaction.Commit();

            }

            catch (Exception ex)
            {
                transaction.Rollback();
            }

        }

        public void AdministracionMedicamentosxFecha(Int64 CodigoAtencion, Int32 CodigoUsuario, Int32 Aseguradora, Int32 Empresa)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;

            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            transaction = Sqlcon.BeginTransaction();
            try
            {
                Sqlcmd = new SqlCommand("sp_AdministracionMedicamentosxFecha", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.Int);
                Sqlcmd.Parameters["@p_CodigoAtencion"].Value = CodigoAtencion;

                Sqlcmd.Parameters.Add("@p_CodigoUsuario", SqlDbType.Int);
                Sqlcmd.Parameters["@p_CodigoUsuario"].Value = CodigoUsuario;

                Sqlcmd.Parameters.Add("@p_Convenio", SqlDbType.Int);
                Sqlcmd.Parameters["@p_Convenio"].Value = Aseguradora;

                Sqlcmd.Parameters.Add("@p_Empresa", SqlDbType.Int);
                Sqlcmd.Parameters["@p_Empresa"].Value = Empresa;

                Sqldap = new SqlDataAdapter();
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts, "tabla");

                transaction.Commit();

            }

            catch (Exception ex)
            {
                transaction.Rollback();
            }

        }

        public int IngresaHonorario(Int64 MEDCODIGO, Int64 ATECODIGO, Int64 IDUSUARIO, DateTime FECHA,
                                    String CODIGO, String VALE)
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

            Sqlcmd = new SqlCommand("sp_IngresaHonorario", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@MEDCODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@MEDCODIGO"].Value = Convert.ToInt64(MEDCODIGO);

            Sqlcmd.Parameters.Add("@ATECODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ATECODIGO"].Value = Convert.ToInt64(ATECODIGO);

            Sqlcmd.Parameters.Add("@IDUSUARIO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@IDUSUARIO"].Value = Convert.ToInt64(IDUSUARIO);

            Sqlcmd.Parameters.Add("@FECHA", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FECHA"].Value = Convert.ToDateTime(FECHA);

            Sqlcmd.Parameters.Add("@CODIGO", SqlDbType.VarChar);
            Sqlcmd.Parameters["@CODIGO"].Value = CODIGO;

            Sqlcmd.Parameters.Add("@VALE", SqlDbType.VarChar);
            Sqlcmd.Parameters["@VALE"].Value = VALE;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;//ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");


            return 0;

        }
        #endregion

        #region CUENTAS_HISTORIAL
        public void CrearCuentaHistorial(CUENTAS_PACIENTES_HISTORIAL cuentaHistorial)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    Int64 codCuentaH;
                    CUENTAS_PACIENTES_HISTORIAL cueCodigoH = contexto.CUENTAS_PACIENTES_HISTORIAL.OrderByDescending(c => c.CPH_CODIGO).FirstOrDefault();
                    codCuentaH = cueCodigoH != null ? cueCodigoH.CPH_CODIGO + 1 : 1;
                    cuentaHistorial.CPH_CODIGO = codCuentaH;
                    contexto.AddToCUENTAS_PACIENTES_HISTORIAL(cuentaHistorial);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err) { throw err; }
        }

        public void Detallemodificadas(CUENTAS_PACIENTES cuenta)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    Int64 codCuenta;
                    CUENTAS_PACIENTES cueCodigo = contexto.CUENTAS_PACIENTES.OrderByDescending(c => c.CUE_CODIGO).FirstOrDefault();
                    codCuenta = cueCodigo != null ? cueCodigo.CUE_CODIGO + 1 : 1;
                    cuenta.CUE_CODIGO = codCuenta;
                    contexto.AddToCUENTAS_PACIENTES(cuenta);
                    contexto.SaveChanges();

                }
            }
            catch (Exception err) { throw err; }
        }

        #endregion

        #region CUENTAS_PACIENTES_TOTALES

        public void CrearCuentaPT(CUENTAS_PACIENTES_TOTALES cuentaPaciente)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    Int32 codCuentaPT;
                    CUENTAS_PACIENTES_TOTALES cueCodigo = contexto.CUENTAS_PACIENTES_TOTALES.OrderByDescending(c => c.CPT_CODIGO).FirstOrDefault();
                    codCuentaPT = cueCodigo != null ? cueCodigo.CPT_CODIGO + 1 : 1;
                    cuentaPaciente.CPT_CODIGO = codCuentaPT;
                    contexto.AddToCUENTAS_PACIENTES_TOTALES(cuentaPaciente);
                    contexto.SaveChanges();

                }
            }
            catch (Exception err) { throw err; }
        }

        public CUENTAS_PACIENTES_TOTALES RecuperarCuentasTotal(int codAtencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return
                        (from c in contexto.CUENTAS_PACIENTES_TOTALES
                         where c.ATE_CODIGO == codAtencion && c.CPT_ESTADO == true
                         select c).FirstOrDefault();
                }
            }
            catch (Exception err) { throw err; }
        }

        public List<CUENTAS_PACIENTES> RecuperaCuenta(Int64 ateCodigo)
        {
            try
            {
                List<CUENTAS_PACIENTES> lista = new List<CUENTAS_PACIENTES>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    lista = (from c in contexto.CUENTAS_PACIENTES
                             where c.ATE_CODIGO == ateCodigo
                             select c).ToList();
                    return lista;
                }
            }
            catch (Exception err) { throw err; }
        }

        public DataTable CargaTipoMedico()
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
            Sqlcmd = new SqlCommand("sp_RecuperaTipomedico2", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        /*public int VerificaValorAutomatico(Int64 Atenciones)
        {
            // PABLO ROCHA / 28/10/2013
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
            Sqlcmd = new SqlCommand("sp_Verificavalorautomatico", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Codigoatencion", SqlDbType.BigInt);
            Sqlcmd.Parameters["@Codigoatencion"].Value = Convert.ToInt64(Atenciones);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            if (Dts.Rows.Count > 0)
            {
                return Convert.ToInt32(Dts.Rows[0]);//   Rows[0][0]);
            }
            else
            {
                return 0;
            }

        }*/

        public int VerificaValorAutomatico(Int64 Atenciones)
        {
            // PABLO ROCHA / 28/10/2013
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
            Sqlcmd = new SqlCommand("sp_Verificavalorautomatico", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Codigoatencion", SqlDbType.BigInt);
            Sqlcmd.Parameters["@Codigoatencion"].Value = Convert.ToInt64(Atenciones);

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

        //PABLO ROCHA RECUPERA EL PRECIO DE COSTO 09/10/2014
        public double RecuperaCosto(string Codigo, int rub_cod)
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
            Sqlcmd = new SqlCommand("sp_RecuperaCosto", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CODPRO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@CODPRO"].Value = Convert.ToInt64(Codigo);

            Sqlcmd.Parameters.Add("@RUB_COD", SqlDbType.BigInt);
            Sqlcmd.Parameters["@RUB_COD"].Value = Convert.ToInt16(rub_cod);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            Sqlcon.Close();
            if (Dts.Rows.Count > 0)
            {
                return Convert.ToDouble(Dts.Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }

        public DataTable GeneraArchivoPlano(String Atenciones)
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
            Sqlcmd = new SqlCommand("sp_GeneraArchivoPlano", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Codigoatencion", SqlDbType.BigInt);
            Sqlcmd.Parameters["@Codigoatencion"].Value = Convert.ToInt64(Atenciones);

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            Sqlcon.Close();

            return Dts.Tables["tabla"];

        }


        public DataTable GeneraArchivoPlanoISSPOL(String Atenciones, int contador, string periodo)
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
            Sqlcmd = new SqlCommand("sp_GeneraArchivoPlanoISSPOL", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Codigoatencion", SqlDbType.BigInt);
            Sqlcmd.Parameters["@Codigoatencion"].Value = Convert.ToInt64(Atenciones);

            Sqlcmd.Parameters.Add("@TotalExpediente", SqlDbType.Int);
            Sqlcmd.Parameters["@TotalExpediente"].Value = contador;

            Sqlcmd.Parameters.Add("@Periodo", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Periodo"].Value = periodo;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            Sqlcon.Close();

            return Dts.Tables["tabla"];

        }

        public DataTable GeneraArchivoPlanoISSPOL(String Atenciones)
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
            Sqlcmd = new SqlCommand("sp_GeneraArchivoPlanoISSPOL", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Codigoatencion", SqlDbType.BigInt);
            Sqlcmd.Parameters["@Codigoatencion"].Value = Convert.ToInt64(Atenciones);

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            Sqlcon.Close();

            return Dts.Tables["tabla"];

        }


        //ACTUALIZA LOS IVA DE LA CUENTA PACIENTE PABLO ROCHA 13/12/2013
        public int GeneraArchivoPlanoIVA(string CadenaAtenciones)
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

            Sqlcmd = new SqlCommand("sp_GeneraArchivoPlanoIVA", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@Atenciones", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Atenciones"].Value = (CadenaAtenciones);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            return 0;
        }


        public DataTable GeneraValoresAutomaticosCuentas(Int64 CodigoAtencion, Int32 CodigoUsuario, Int32 NumeroDias, String Habitacion, Int32 Aseguradora, Int32 Empresa)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;

            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("sp_GeneraValoresAutomaticos", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.Int);
            Sqlcmd.Parameters["@p_CodigoAtencion"].Value = CodigoAtencion;

            Sqlcmd.Parameters.Add("@p_CodigoUsuario", SqlDbType.Int);
            Sqlcmd.Parameters["@p_CodigoUsuario"].Value = CodigoUsuario;

            Sqlcmd.Parameters.Add("@p_Dias", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Dias"].Value = NumeroDias;

            Sqlcmd.Parameters.Add("@p_Habitacion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_Habitacion"].Value = Habitacion;

            Sqlcmd.Parameters.Add("@p_Convenio", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Convenio"].Value = Aseguradora;

            Sqlcmd.Parameters.Add("@p_Empresa", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Empresa"].Value = Empresa;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            return Dts.Tables["tabla"];


        }

        public DataTable RecuperaGinecologia(string HCG)
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

            Sqlcmd = new SqlCommand("sp_RecuperaGinecologia", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@HCG", SqlDbType.VarChar);
            Sqlcmd.Parameters["@HCG"].Value = HCG;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            return Dts.Tables["tabla"];


        }

        public int GuardaGinecologia(string HistoriaClinica, int Menarquia, string Cliclos, DateTime FUM, int G, int P,
                                     int A, int C, int HV, int GO, int DIU, string OM, string CV, string APP, string APF, string GS,
                                     string HOPITALIZACION, string OPERACIONES, string RECOMENDADO)
        {
            //Pablo Rocha 22-04-2014 Guarda Datos Ginecologia
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

            Sqlcmd = new SqlCommand("sp_GuardaGinecologia", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@HistoriaClinica", SqlDbType.VarChar);
            Sqlcmd.Parameters["@HistoriaClinica"].Value = (HistoriaClinica);

            Sqlcmd.Parameters.Add("@Menarquia", SqlDbType.Int);
            Sqlcmd.Parameters["@Menarquia"].Value = (Menarquia);

            Sqlcmd.Parameters.Add("@Cliclos", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Cliclos"].Value = (Cliclos);

            Sqlcmd.Parameters.Add("@Fum", SqlDbType.Date);
            Sqlcmd.Parameters["@Fum"].Value = (FUM);

            Sqlcmd.Parameters.Add("@G", SqlDbType.Int);
            Sqlcmd.Parameters["@G"].Value = (G);

            Sqlcmd.Parameters.Add("@P", SqlDbType.Int);
            Sqlcmd.Parameters["@P"].Value = (P);

            Sqlcmd.Parameters.Add("@A", SqlDbType.Int);
            Sqlcmd.Parameters["@A"].Value = (A);

            Sqlcmd.Parameters.Add("@C", SqlDbType.Int);
            Sqlcmd.Parameters["@C"].Value = (C);

            Sqlcmd.Parameters.Add("@HV", SqlDbType.Int);
            Sqlcmd.Parameters["@HV"].Value = (HV);

            Sqlcmd.Parameters.Add("@GO", SqlDbType.Int);
            Sqlcmd.Parameters["@GO"].Value = (GO);

            Sqlcmd.Parameters.Add("@DIU", SqlDbType.Int);
            Sqlcmd.Parameters["@DIU"].Value = (DIU);

            Sqlcmd.Parameters.Add("@OM", SqlDbType.VarChar);
            Sqlcmd.Parameters["@OM"].Value = (OM);

            Sqlcmd.Parameters.Add("@CV", SqlDbType.VarChar);
            Sqlcmd.Parameters["@CV"].Value = (CV);

            Sqlcmd.Parameters.Add("@APP", SqlDbType.VarChar);
            Sqlcmd.Parameters["@APP"].Value = (APP);

            Sqlcmd.Parameters.Add("@APF", SqlDbType.VarChar);
            Sqlcmd.Parameters["@APF"].Value = (APF);

            Sqlcmd.Parameters.Add("@GS", SqlDbType.VarChar);
            Sqlcmd.Parameters["@GS"].Value = (GS);

            Sqlcmd.Parameters.Add("@HOPITALIZACION", SqlDbType.VarChar);
            Sqlcmd.Parameters["@HOPITALIZACION"].Value = (HOPITALIZACION);

            Sqlcmd.Parameters.Add("@OPERACIONES", SqlDbType.VarChar);
            Sqlcmd.Parameters["@OPERACIONES"].Value = (OPERACIONES);

            Sqlcmd.Parameters.Add("@RECOMENDADO", SqlDbType.VarChar);
            Sqlcmd.Parameters["@RECOMENDADO"].Value = (RECOMENDADO);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            return 0;
        }

        public List<PRODUCTOCOPAGO> DivideFactura1(int Ate_Codigo, int Rub_Codigo)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            BaseContextoDatos obj = new BaseContextoDatos();
            List<PRODUCTOCOPAGO> lista = new List<PRODUCTOCOPAGO>();
            try
            {
                con = obj.ConectarBd();
                cmd = new SqlCommand("sp_DivideFactura", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ATE_CODIGO", Ate_Codigo);
                cmd.Parameters.AddWithValue("@Rub_Codigo", Rub_Codigo);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    PRODUCTOCOPAGO objProducto = new PRODUCTOCOPAGO();
                    objProducto.CUE_CODIGO = Convert.ToInt64(dr["CUE_CODIGO"].ToString());
                    objProducto.CUE_DETALLE = dr["CUE_DETALLE"].ToString();
                    objProducto.CUE_VALOR_UNITARIO = Convert.ToDecimal(dr["CUE_VALOR_UNITARIO"].ToString());
                    objProducto.CUE_VALOR = Convert.ToDecimal(dr["CUE_VALOR"].ToString());
                    objProducto.CUE_CANTIDAD = Convert.ToDecimal(dr["CUE_CANTIDAD"].ToString());
                    objProducto.CUE_IVA = Convert.ToDecimal(dr["CUE_IVA"].ToString());
                    objProducto.paga_iva = Convert.ToBoolean(dr["paga_iva"].ToString());
                    objProducto.iva = Convert.ToDecimal(dr["iva"].ToString());
                    lista.Add(objProducto);
                }
            }
            catch (Exception ex)
            {
                lista = null;
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return lista;

        }

        public int CopagoFactura1(int Ate_Codigo, double Cue_Valor, string Cue_Detalle, double Cue_Valor1, int Cue_Rubro)
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
            Sqlcmd = new SqlCommand("sp_CopagoFactura", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = (Ate_Codigo);

            Sqlcmd.Parameters.Add("@CueValorUnitario", SqlDbType.Decimal);
            Sqlcmd.Parameters["@CueValorUnitario"].Value = (Cue_Valor);

            Sqlcmd.Parameters.Add("@cueDetalle", SqlDbType.VarChar);
            Sqlcmd.Parameters["@cueDetalle"].Value = (Cue_Detalle);

            Sqlcmd.Parameters.Add("@CueValorUnitario1", SqlDbType.Decimal);
            Sqlcmd.Parameters["@CueValorUnitario1"].Value = (Cue_Valor1);

            Sqlcmd.Parameters.Add("@Cue_Rubro", SqlDbType.Int);
            Sqlcmd.Parameters["@Cue_Rubro"].Value = (Cue_Rubro);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts);

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;

        }

        public int DivideFactura2(int Ate_Codigo)
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
            Sqlcmd = new SqlCommand("sp_DivideFactura2", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo1", SqlDbType.Int);
            Sqlcmd.Parameters["@Ate_Codigo1"].Value = (Ate_Codigo);

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
            return 0;

        }

        public int DivideFactura4(int Ate_Codigo)
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
            Sqlcmd = new SqlCommand("sp_DivideFactura4", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo1", SqlDbType.Int);
            Sqlcmd.Parameters["@Ate_Codigo1"].Value = (Ate_Codigo);

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
            return 0;

        }

        public void DividirFactura(Int64 ate_codigo, decimal cantidad, Int64 cue_codigo)
        {
            SqlCommand command;
            SqlConnection connection;

            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_DivisionCuenta", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@cantidad", cantidad);
            command.Parameters.AddWithValue("@cue_codigo", cue_codigo);

            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();

        }

        public void CreaHistorialNuevoAuditoria(NuevoAuditoria nuevo)
        {
            SqlCommand command;
            SqlConnection connection;

            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_CreaHistorialNuevoAuditoria", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@CUE_CODIGO", nuevo.cue_codigoAud);
            command.Parameters.AddWithValue("@CUE_DETALLE", nuevo.cue_detalleAud);
            command.Parameters.AddWithValue("@CUE_CANTIDAD", nuevo.cue_cantidadAdi);
            command.Parameters.AddWithValue("@CUE_VALOR_UNITARIO", nuevo.cue_valorUnitarioAdi);
            command.Parameters.AddWithValue("@CUE_IVA", nuevo.cue_ivaAdi);
            command.Parameters.AddWithValue("@CUE_VALOR", nuevo.cue_valorAdi);
            command.Parameters.AddWithValue("@USUARIO", nuevo.usuarioAdi);
            command.Parameters.AddWithValue("@ATE_CODIGO", nuevo.ate_codigoAud);

            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();

        }
        public int DivideFacturaNO(int Ate_Codigo)
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
            Sqlcmd = new SqlCommand("update CUENTAS_PACIENTES set DivideFactura='N'"
                + "WHERE ATE_CODIGO = " + Ate_Codigo + " ", Sqlcon);
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
            return 0;

        }

        public int DivideFacturaProd1(int Ate_Codigo, Int64 cod_pro)
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
            Sqlcmd = new SqlCommand("update CUENTAS_PACIENTES set DivideFactura='S'"
                + "WHERE ATE_CODIGO = " + Ate_Codigo + " and CUE_CODIGO = " + cod_pro + " ", Sqlcon);
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
            return 0;

        }
        public int DivideFactura3(int Ate_Codigo, int rubro)
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
            Sqlcmd = new SqlCommand("sp_DivideFactura3", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@Ate_Codigo"].Value = (Ate_Codigo);

            Sqlcmd.Parameters.Add("@Rub_Codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@Rub_Codigo"].Value = (rubro);

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
            return 0;

        }
        public int ValoresAutomaticos(int ATE_CODIGO, int RUBRO, int TOTALDIAS, int USUARIO, string DESCRIPCION, string PRO_CODIGO, double CUE_VALOR_UNITARIO)
        {
            //Pablo Rocha 29-05-2014 Guarda valores automaticos
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

            Sqlcmd = new SqlCommand("sp_GuardaValoresAutomaticos", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = (ATE_CODIGO);

            Sqlcmd.Parameters.Add("@RUBRO", SqlDbType.Int);
            Sqlcmd.Parameters["@RUBRO"].Value = (RUBRO);

            Sqlcmd.Parameters.Add("@TOTALDIAS", SqlDbType.Int);
            Sqlcmd.Parameters["@TOTALDIAS"].Value = (TOTALDIAS);

            Sqlcmd.Parameters.Add("@USUARIO", SqlDbType.Int);
            Sqlcmd.Parameters["@USUARIO"].Value = (USUARIO);

            Sqlcmd.Parameters.Add("@CUE_DETALLE", SqlDbType.VarChar);
            Sqlcmd.Parameters["@CUE_DETALLE"].Value = (DESCRIPCION);

            Sqlcmd.Parameters.Add("@PRO_CODIGO", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PRO_CODIGO"].Value = (PRO_CODIGO);

            Sqlcmd.Parameters.Add("@CUE_VALOR_UNITARIO", SqlDbType.Float);
            Sqlcmd.Parameters["@CUE_VALOR_UNITARIO"].Value = (CUE_VALOR_UNITARIO);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            return 0;
        }

        public bool ActualizaProductos(List<PRODUCTOCOPAGO> objProducto, int ateCodigo)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool ok = false;
            try
            {
                con = obj.ConectarBd();
                con.Open();


                foreach (var inspector in objProducto)
                {
                    cmd = new SqlCommand("sp_ActualizaProductos", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cue_codigo", inspector.CUE_CODIGO);
                    cmd.Parameters.AddWithValue("@cue_valor_unitario", inspector.CUE_VALOR_UNITARIO);
                    cmd.Parameters.AddWithValue("@cue_valor", inspector.CUE_VALOR);
                    cmd.Parameters.AddWithValue("@cue_iva", inspector.CUE_IVA);
                    cmd.Parameters.AddWithValue("@ateCodigo", ateCodigo);


                    cmd.ExecuteNonQuery();

                    //using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                    //{
                    //    CUENTAS_PACIENTES cuenta = contexto.CUENTAS_PACIENTES.Where(i => i.CUE_CODIGO == CUE_CODIGO).FirstOrDefault();
                    //    cuenta.CUE_DETALLE = ingresoSala.ID_USUARIO;
                    //    ingresoSalaOri.INT_ESTADO = ingresoSala.INT_ESTADO;
                    //    ingresoSalaOri.INT_FECHA_FIN = ingresoSala.INT_FECHA_FIN;
                    //    ingresoSalaOri.INT_FECHA_INI = ingresoSala.INT_FECHA_INI;
                    //    ingresoSalaOri.INT_TIPO = ingresoSala.INT_TIPO;
                    //    ingresoSalaOri.MED_CODIGO = ingresoSala.MED_CODIGO;
                    //    contexto.SaveChanges();
                    //}


                }
                ok = true;
            }
            catch (Exception ex)
            {
                ok = false;
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return ok;
        }

        #endregion
        public bool LlamaParametro()
        {
            bool ok = false;
            SqlConnection conexion = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            BaseContextoDatos obj = new BaseContextoDatos();
            try
            {
                conexion = obj.ConectarBd();
                command = new SqlCommand("sp_LlamaParametro", conexion);
                command.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                reader = command.ExecuteReader();
                while (reader != null)
                {
                    ok = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());

                }
            }
            catch (Exception)
            {
                ok = false;
            }
            finally
            {
                conexion.Close();
            }
            return ok;
        }
        public bool LlamaParametroInventariable()
        {
            bool ok = false;
            SqlConnection conexion = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            BaseContextoDatos obj = new BaseContextoDatos();
            try
            {
                conexion = obj.ConectarBd();
                command = new SqlCommand("sp_ParametroInventariable", conexion);
                command.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                reader = command.ExecuteReader();
                while (reader != null)
                {
                    ok = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());

                }
            }
            catch (Exception ex)
            {
                ok = false;
            }
            finally
            {
                conexion.Close();
            }
            return ok;
        }
        public string VerificaBien(Int64 codpro)
        {
            string ok = "";
            SqlConnection conexion = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            BaseContextoDatos obj = new BaseContextoDatos();
            try
            {
                conexion = obj.ConectarBd();
                command = new SqlCommand("sp_VerificarBien", conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@codpro", codpro);
                conexion.Open();
                reader = command.ExecuteReader();
                while (reader != null)
                {
                    ok = reader["clasprod"].ToString();

                }
            }
            catch (Exception)
            {
                ok = "B";
            }
            finally
            {
                conexion.Close();
            }
            return ok;
        }
        public Int64 generaCuentaAuditoria(Int64 ate_codigo, List<DtoCopago> copago, List<DtopCuenta> cuenta, Int64 _id_usuario)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    List<CUENTAS_PACIENTES_COPAGO> lCopago = new List<CUENTAS_PACIENTES_COPAGO>();
                    List<CUENTAS_PACIENTES> cuentaPrincipal = new List<CUENTAS_PACIENTES>();
                    List<CUENTAS_PACIENTES> cuentaSecundaria = new List<CUENTAS_PACIENTES>();
                    List<CUENTAS_PACIENTES> lcuentPaciente = db.CUENTAS_PACIENTES.Where(x => x.CUE_CANTIDAD > 0 && x.CUE_VALOR > 0 && x.ATE_CODIGO == ate_codigo).ToList();
                    ATENCIONES atencionPrincipal = db.ATENCIONES.FirstOrDefault(y => y.ATE_CODIGO == ate_codigo);
                    Int64 pac_codigo = Convert.ToInt64(atencionPrincipal.PACIENTESReference.EntityKey.EntityKeyValues[0].Value);
                    Int64 dap_codigo = Convert.ToInt64(atencionPrincipal.PACIENTES_DATOS_ADICIONALESReference.EntityKey.EntityKeyValues[0].Value);
                    Int64 hab_codigo = Convert.ToInt64(atencionPrincipal.HABITACIONESReference.EntityKey.EntityKeyValues[0].Value);
                    Int64 caj_codigo = Convert.ToInt64(atencionPrincipal.CAJASReference.EntityKey.EntityKeyValues[0].Value);
                    Int64 tia_codigo = Convert.ToInt64(atencionPrincipal.TIPO_TRATAMIENTOReference.EntityKey.EntityKeyValues[0].Value);
                    Int64 id_usuario = Convert.ToInt64(atencionPrincipal.USUARIOSReference.EntityKey.EntityKeyValues[0].Value);
                    Int64 tir_codigo = Convert.ToInt64(atencionPrincipal.TIPO_REFERIDOReference.EntityKey.EntityKeyValues[0].Value);
                    Int64 afl_codigo = Convert.ToInt64(atencionPrincipal.ATENCION_FORMAS_LLEGADAReference.EntityKey.EntityKeyValues[0].Value);
                    Int64 med_codigo = Convert.ToInt64(atencionPrincipal.MEDICOSReference.EntityKey.EntityKeyValues[0].Value);
                    Int64 tip_codigo = Convert.ToInt64(atencionPrincipal.TIPO_INGRESOReference.EntityKey.EntityKeyValues[0].Value);
                    PACIENTES pacientes = db.PACIENTES.FirstOrDefault(w => w.PAC_CODIGO == pac_codigo);
                    PACIENTES_DATOS_ADICIONALES datosAdicionales = db.PACIENTES_DATOS_ADICIONALES.FirstOrDefault(a => a.DAP_CODIGO == dap_codigo);
                    HABITACIONES habitaciones = db.HABITACIONES.FirstOrDefault(b => b.hab_Codigo == hab_codigo);
                    CAJAS caja = db.CAJAS.FirstOrDefault(c => c.CAJ_CODIGO == caj_codigo);
                    TIPO_TRATAMIENTO tratamiento = db.TIPO_TRATAMIENTO.FirstOrDefault(d => d.TIA_CODIGO == tia_codigo);
                    USUARIOS ususario = db.USUARIOS.FirstOrDefault(e => e.ID_USUARIO == id_usuario);
                    TIPO_REFERIDO referido = db.TIPO_REFERIDO.FirstOrDefault(f => f.TIR_CODIGO == tir_codigo);
                    ATENCION_FORMAS_LLEGADA llegada = db.ATENCION_FORMAS_LLEGADA.FirstOrDefault(g => g.AFL_CODIGO == afl_codigo);
                    MEDICOS medico = db.MEDICOS.FirstOrDefault(h => h.MED_CODIGO == med_codigo);
                    TIPO_INGRESO ingreso = db.TIPO_INGRESO.FirstOrDefault(i => i.TIP_CODIGO == tip_codigo);

                    foreach (var item in lcuentPaciente)
                    {
                        CUENTAS_PACIENTES_COPAGO cp = new CUENTAS_PACIENTES_COPAGO();
                        cp.CUE_CODIGO = item.CUE_CODIGO;
                        cp.ATE_CODIGO = item.ATE_CODIGO;
                        cp.CUE_FECHA = item.CUE_FECHA;
                        cp.PRO_CODIGO = item.PRO_CODIGO;
                        cp.CUE_DETALLE = item.CUE_DETALLE;
                        cp.CUE_VALOR_UNITARIO = item.CUE_VALOR_UNITARIO;
                        cp.CUE_CANTIDAD = item.CUE_CANTIDAD;
                        cp.CUE_VALOR = item.CUE_VALOR;
                        cp.CUE_IVA = item.CUE_IVA;
                        cp.CUE_ESTADO = item.CUE_ESTADO;
                        cp.CUE_NUM_FAC = item.CUE_NUM_FAC;
                        cp.RUB_CODIGO = item.RUB_CODIGO;
                        cp.PED_CODIGO = item.PED_CODIGO;
                        cp.ID_USUARIO = item.ID_USUARIO;
                        cp.CAT_CODIGO = item.CAT_CODIGO;
                        cp.PRO_CODIGO_BARRAS = item.PRO_CODIGO_BARRAS;
                        cp.CUE_NUM_CONTROL = item.CUE_NUM_CONTROL;
                        cp.CUE_OBSERVACION = item.CUE_OBSERVACION;
                        cp.MED_CODIGO = item.MED_CODIGO;
                        cp.CUE_ORDER_IMPRESION = item.CUE_ORDER_IMPRESION;
                        cp.Codigo_Pedido = item.Codigo_Pedido;
                        cp.Id_Tipo_Medico = item.Id_Tipo_Medico;
                        cp.COSTO = item.COSTO;
                        cp.NumVale = item.NumVale;
                        cp.DivideFactura = item.DivideFactura;
                        cp.Descuento = item.Descuento;
                        cp.PorDescuento = item.PorDescuento;
                        cp.USUARIO_FACTURA = item.USUARIO_FACTURA;
                        cp.FECHA_FACTURA = item.FECHA_FACTURA;
                        cp.ID_USUARIO_MODIFICA = _id_usuario;
                        cp.ESTADO = true;
                        lCopago.Add(cp);
                    }
                    db.CrearLista("CUENTAS_PACIENTES_COPAGO", lCopago);

                    ATENCIONES atencionSecundaria = new ATENCIONES();
                    var maxNumeroAtencion = db.ATENCIONES.OrderByDescending(z => z.ATE_CODIGO).FirstOrDefault();
                    Int64 ATE_NUMERO_CONTROL = maxNumeroAtencion.ATE_CODIGO + 1;
                    atencionSecundaria.ATE_CODIGO = (Int32)ATE_NUMERO_CONTROL;
                    atencionSecundaria.ATE_NUMERO_ATENCION = atencionPrincipal.ATE_NUMERO_ATENCION;
                    atencionSecundaria.ATE_FECHA = atencionPrincipal.ATE_FECHA;
                    atencionSecundaria.ATE_NUMERO_CONTROL = atencionPrincipal.ATE_NUMERO_CONTROL;
                    atencionSecundaria.ATE_FACTURA_PACIENTE = atencionPrincipal.ATE_FACTURA_PACIENTE;
                    atencionSecundaria.ATE_FACTURA_FECHA = atencionPrincipal.ATE_FACTURA_FECHA;
                    atencionSecundaria.ATE_FECHA_INGRESO = atencionPrincipal.ATE_FECHA_INGRESO;
                    atencionSecundaria.ATE_FECHA_ALTA = DateTime.Now;
                    atencionSecundaria.ATE_REFERIDO = atencionPrincipal.ATE_REFERIDO;
                    atencionSecundaria.ATE_REFERIDO_DE = atencionPrincipal.ATE_REFERIDO_DE;
                    atencionSecundaria.ATE_EDAD_PACIENTE = atencionPrincipal.ATE_EDAD_PACIENTE;
                    atencionSecundaria.ATE_ACOMPANANTE_NOMBRE = atencionPrincipal.ATE_ACOMPANANTE_NOMBRE;
                    atencionSecundaria.ATE_ACOMPANANTE_CEDULA = atencionPrincipal.ATE_ACOMPANANTE_CEDULA;
                    atencionSecundaria.ATE_ACOMPANANTE_PARENTESCO = atencionPrincipal.ATE_ACOMPANANTE_PARENTESCO;
                    atencionSecundaria.ATE_ACOMPANANTE_TELEFONO = atencionPrincipal.ATE_ACOMPANANTE_TELEFONO;
                    atencionSecundaria.ATE_ACOMPANANTE_DIRECCION = atencionPrincipal.ATE_ACOMPANANTE_DIRECCION;
                    atencionSecundaria.ATE_ACOMPANANTE_CIUDAD = atencionPrincipal.ATE_ACOMPANANTE_CIUDAD;
                    atencionSecundaria.ATE_GARANTE_NOMBRE = atencionPrincipal.ATE_GARANTE_NOMBRE;
                    atencionSecundaria.ATE_GARANTE_CEDULA = atencionPrincipal.ATE_GARANTE_CEDULA;
                    atencionSecundaria.ATE_GARANTE_PARENTESCO = atencionPrincipal.ATE_GARANTE_PARENTESCO;
                    atencionSecundaria.ATE_GARANTE_MONTO_GARANTIA = atencionPrincipal.ATE_GARANTE_MONTO_GARANTIA;
                    atencionSecundaria.ATE_GARANTE_TELEFONO = atencionPrincipal.ATE_GARANTE_TELEFONO;
                    atencionSecundaria.ATE_GARANTE_DIRECCION = atencionPrincipal.ATE_GARANTE_DIRECCION;
                    atencionSecundaria.ATE_GARANTE_CIUDAD = atencionPrincipal.ATE_GARANTE_CIUDAD;
                    atencionSecundaria.ATE_DIAGNOSTICO_INICIAL = atencionPrincipal.ATE_DIAGNOSTICO_INICIAL;
                    atencionSecundaria.ATE_DIAGNOSTICO_FINAL = atencionPrincipal.ATE_DIAGNOSTICO_FINAL;
                    atencionSecundaria.ATE_OBSERVACIONES = atencionPrincipal.ATE_OBSERVACIONES;
                    atencionSecundaria.ATE_ESTADO = atencionPrincipal.ATE_ESTADO;
                    atencionSecundaria.ATE_FACTURA_NOMBRE = atencionPrincipal.ATE_FACTURA_NOMBRE;
                    atencionSecundaria.ATE_DIRECTORIO = atencionPrincipal.ATE_DIRECTORIO;
                    atencionSecundaria.PACIENTESReference.EntityKey = pacientes.EntityKey;
                    atencionSecundaria.PACIENTES_DATOS_ADICIONALESReference.EntityKey = datosAdicionales.EntityKey;
                    atencionSecundaria.HABITACIONESReference.EntityKey = habitaciones.EntityKey;
                    atencionSecundaria.CAJASReference.EntityKey = caja.EntityKey;
                    atencionSecundaria.TIPO_TRATAMIENTOReference.EntityKey = tratamiento.EntityKey;
                    atencionSecundaria.USUARIOSReference.EntityKey = ususario.EntityKey;
                    atencionSecundaria.TIPO_REFERIDOReference.EntityKey = referido.EntityKey;
                    atencionSecundaria.ATENCION_FORMAS_LLEGADAReference.EntityKey = llegada.EntityKey;
                    atencionSecundaria.MEDICOSReference.EntityKey = medico.EntityKey;
                    atencionSecundaria.TIPO_INGRESOReference.EntityKey = ingreso.EntityKey;
                    atencionSecundaria.TIF_CODIGO = atencionPrincipal.TIF_CODIGO;
                    atencionSecundaria.TIF_OBSERVACION = atencionPrincipal.TIF_OBSERVACION;
                    atencionSecundaria.ATE_NUMERO_ADMISION = atencionPrincipal.ATE_NUMERO_ADMISION;
                    atencionSecundaria.ATE_EN_QUIROFANO = atencionPrincipal.ATE_EN_QUIROFANO;
                    atencionSecundaria.FOR_PAGO = atencionPrincipal.FOR_PAGO;
                    atencionSecundaria.ATE_QUIEN_ENTREGA_PAC = atencionPrincipal.ATE_QUIEN_ENTREGA_PAC;
                    atencionSecundaria.ATE_CIERRE_HC = atencionPrincipal.ATE_CIERRE_HC;
                    atencionSecundaria.ATE_FEC_ING_HABITACION = atencionPrincipal.ATE_FEC_ING_HABITACION;
                    atencionSecundaria.ESC_CODIGO = 2;
                    atencionSecundaria.CUE_ESTADO = atencionPrincipal.CUE_ESTADO;
                    atencionSecundaria.TipoAtencion = atencionPrincipal.TipoAtencion;
                    atencionSecundaria.ate_discapacidad = atencionPrincipal.ate_discapacidad;
                    atencionSecundaria.ate_carnet_conadis = atencionPrincipal.ate_carnet_conadis;
                    atencionSecundaria.ATE_ID_ACCIDENTE = atencionPrincipal.ATE_ID_ACCIDENTE;
                    atencionSecundaria.idTipoDescuento = atencionPrincipal.idTipoDescuento;
                    db.Crear("ATENCIONES", atencionSecundaria);

                    ATENCION_DETALLE_CATEGORIAS detCat = db.ATENCION_DETALLE_CATEGORIAS.FirstOrDefault(x => x.ATENCIONES.ATE_CODIGO == ate_codigo);
                    var maxNumeroAtencionCategoria = db.ATENCION_DETALLE_CATEGORIAS.OrderByDescending(z => z.ADA_CODIGO).FirstOrDefault();
                    Int64 ADA_NUMERO_CONTROL = maxNumeroAtencionCategoria.ADA_CODIGO + 1;
                    ATENCION_DETALLE_CATEGORIAS detGraba = new ATENCION_DETALLE_CATEGORIAS();
                    detGraba.ADA_CODIGO = (int)ADA_NUMERO_CONTROL;
                    detGraba.ATENCIONESReference.EntityKey = atencionSecundaria.EntityKey;
                    detGraba.CATEGORIAS_CONVENIOSReference.EntityKey = detCat.CATEGORIAS_CONVENIOSReference.EntityKey;
                    detGraba.ADA_FECHA_INICIO = detCat.ADA_FECHA_INICIO;
                    detGraba.ADA_FECHA_FIN = detCat.ADA_FECHA_FIN;
                    detGraba.ADA_AUTORIZACION = detCat.ADA_AUTORIZACION;
                    detGraba.ADA_CONTRATO = detCat.ADA_CONTRATO;
                    detGraba.ADA_MONTO_COBERTURA = detCat.ADA_MONTO_COBERTURA;
                    detGraba.ADA_ORDEN = detCat.ADA_ORDEN;
                    detGraba.ADA_ESTADO = detCat.ADA_ESTADO;
                    detGraba.HCC_CODIGO_TS = detCat.HCC_CODIGO_TS;
                    detGraba.HCC_CODIGO_DE = detCat.HCC_CODIGO_DE;
                    detGraba.HCC_CODIGO_ES = detCat.HCC_CODIGO_ES;
                    db.Crear("ATENCION_DETALLE_CATEGORIAS", detGraba);

                    var maxNumeroCuentaPaciente = db.CUENTAS_PACIENTES.OrderByDescending(z => z.CUE_CODIGO).FirstOrDefault();
                    Int64 CUE_NUMERO_CONTROL = maxNumeroCuentaPaciente.CUE_CODIGO + 1;
                    foreach (var item in lcuentPaciente)
                    {
                        CUENTAS_PACIENTES cp = new CUENTAS_PACIENTES();
                        DtoCopago cop = copago.FirstOrDefault(x => x.CPpro_codigo == Convert.ToInt64(item.PRO_CODIGO) && x.id == item.CUE_CODIGO);
                        if (cop != null)
                        {
                            cp.CUE_CODIGO = CUE_NUMERO_CONTROL;
                            cp.ATE_CODIGO = (Int32)ATE_NUMERO_CONTROL;
                            cp.CUE_FECHA = item.CUE_FECHA;
                            cp.PRO_CODIGO = item.PRO_CODIGO;
                            cp.CUE_DETALLE = item.CUE_DETALLE;
                            cp.CUE_VALOR_UNITARIO = cop.CPv_unitario;
                            cp.CUE_CANTIDAD = item.CUE_CANTIDAD;
                            cp.CUE_VALOR = cop.CPtotal;
                            cp.CUE_IVA = cop.CPiva;
                            cp.CUE_ESTADO = item.CUE_ESTADO;
                            cp.CUE_NUM_FAC = item.CUE_NUM_FAC;
                            cp.RUB_CODIGO = item.RUB_CODIGO;
                            cp.PED_CODIGO = item.PED_CODIGO;
                            cp.ID_USUARIO = item.ID_USUARIO;
                            cp.CAT_CODIGO = item.CAT_CODIGO;
                            cp.PRO_CODIGO_BARRAS = item.PRO_CODIGO_BARRAS;
                            cp.CUE_NUM_CONTROL = item.CUE_NUM_CONTROL;
                            cp.CUE_OBSERVACION = item.CUE_OBSERVACION;
                            cp.MED_CODIGO = item.MED_CODIGO;
                            cp.CUE_ORDER_IMPRESION = item.CUE_ORDER_IMPRESION;
                            cp.Codigo_Pedido = item.Codigo_Pedido;
                            cp.Id_Tipo_Medico = item.Id_Tipo_Medico;
                            cp.COSTO = item.COSTO;
                            cp.NumVale = item.NumVale;
                            cp.DivideFactura = item.DivideFactura;
                            cp.Descuento = item.Descuento;
                            cp.PorDescuento = item.PorDescuento;
                            cp.USUARIO_FACTURA = item.USUARIO_FACTURA;
                            cp.FECHA_FACTURA = item.FECHA_FACTURA;
                            cuentaSecundaria.Add(cp);
                            CUE_NUMERO_CONTROL++;
                        }
                    }
                    db.CrearLista("CUENTAS_PACIENTES", cuentaSecundaria);

                    foreach (var item in lcuentPaciente)
                    {
                        DtopCuenta cue = cuenta.FirstOrDefault(x => x.COpro_codigo == Convert.ToInt64(item.PRO_CODIGO) && x.id == item.CUE_CODIGO);
                        if (cue != null)
                        {
                            item.CUE_VALOR_UNITARIO = cue.COv_unitario;
                            item.CUE_VALOR = cue.COtotal;
                            item.CUE_IVA = cue.COiva;
                        }
                    }

                    COPAGO aud_copago = new COPAGO();
                    aud_copago.ATE_CODIGO = ate_codigo;
                    aud_copago.ATE_CODIGO_COPAGO = ATE_NUMERO_CONTROL;
                    aud_copago.ESTADO = true;
                    db.Crear("COPAGO", aud_copago);

                    db.SaveChanges();
                    transa.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return ATE_NUMERO_CONTROL;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transa.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return 0;
                }
            }
        }
    }
}
