using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using His.Entidades.General;
using Microsoft.Data.Extensions;

namespace His.Datos
{
    public class DatHabitaciones
    {
        #region Habitaciones

        public void GrabarHabitaciones(HABITACIONES habitacionModificada, HABITACIONES habitacionOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                contexto.Grabar(habitacionModificada, habitacionOriginal);
            }
        }

        public HABITACIONES RecuperarHabitacionID(Int16 codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from h in contexto.HABITACIONES.Include("HABITACIONES_ESTADO")
                        where h.hab_Codigo == codigo
                        select h).FirstOrDefault();
            }
        }

        public DataTable HistorialHabitaciones(Int64 HISTORIACLINICA)
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
            Sqlcmd = new SqlCommand("sp_HistorialHabitaciones", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@HISTORIACLINICA", SqlDbType.BigInt);
            Sqlcmd.Parameters["@HISTORIACLINICA"].Value = HISTORIACLINICA;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        public DataTable HistorialHabitacionesFecha(DateTime fechaIni, DateTime fechaFin)
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
            Sqlcmd = new SqlCommand("sp_HistorialHabitacionesFecha", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@fechaIni", SqlDbType.Date);
            Sqlcmd.Parameters["@fechaIni"].Value = fechaIni;

            Sqlcmd.Parameters.Add("@fechaFin", SqlDbType.Date);
            Sqlcmd.Parameters["@fechaFin"].Value = fechaFin;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        public DataTable AreaActualHab(Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_AreaActualHab", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);

            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public bool CambiarEstadoHabitacion(HABITACIONES mHabitacion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                int hab = (from h in contexto.HABITACIONES
                           where h.hab_Codigo == mHabitacion.hab_Codigo
                           select h.HABITACIONES_ESTADO.HES_CODIGO).FirstOrDefault();
                HABITACIONES oHabitacion = contexto.HABITACIONES.FirstOrDefault(h => h.hab_Codigo == mHabitacion.hab_Codigo);

                if (hab == 5 || hab == 2)
                {
                    oHabitacion.hab_fec_cambio_est = DateTime.Now;
                    oHabitacion.HABITACIONES_ESTADOReference.EntityKey = mHabitacion.HABITACIONES_ESTADOReference.EntityKey;
                    contexto.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool CambiaEstadoHabitacion(Int64 ateCodigo, int HABITACION)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            SqlTransaction transaction;
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

            transaction = Sqlcon.BeginTransaction();
            try
            {
                Sqlcmd = new SqlCommand("SP_CambiaEstadoHabitacion", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;
                Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
                Sqlcmd.Parameters["@ATE_CODIGO"].Value = ateCodigo;

                Sqlcmd.Parameters.Add("@HABITACION", SqlDbType.Int);
                Sqlcmd.Parameters["@HABITACION"].Value = HABITACION;

                Sqldap = new SqlDataAdapter();
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts, "tabla");
                transaction.Commit();
                return true;
            }
            catch (Exception err)
            {
                transaction.Rollback();
                return false;
            }

        }
        public bool CambiaEstadoHabitacionMantenimiento(int had_codigo, int estado)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            SqlTransaction transaction;
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

            transaction = Sqlcon.BeginTransaction();
            try
            {
                Sqlcmd = new SqlCommand("SP_CambiaEstadoHabitacionMantenimiento", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@HAB_CODIGO", SqlDbType.Int);
                Sqlcmd.Parameters["@HAB_CODIGO"].Value = had_codigo;

                Sqlcmd.Parameters.Add("@HES_NOMBRE", SqlDbType.Int);
                Sqlcmd.Parameters["@HES_NOMBRE"].Value = estado;

                Sqldap = new SqlDataAdapter();
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts, "tabla");
                transaction.Commit();
                return true;
            }
            catch (Exception err)
            {
                transaction.Rollback();
                return false;
            }

        }

        /// <summary>
        /// Metodo que recupera una habitacion por su numero
        /// </summary>
        /// <returns>Retorna una habitacion</returns>
        public HABITACIONES RecuperaHabitacionPorNumero(string numero)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from h in contexto.HABITACIONES
                        where h.hab_Numero == numero
                        select h).FirstOrDefault();
            }
        }

        public DataTable RevertirMovimientoHabitacion(HABITACIONES habitacion)
        {
            try
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

                Sqlcmd = new SqlCommand("sp_ope_revertir_cierre_habitacion", Sqlcon);

                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Parameters.Add("@codHabitacion", SqlDbType.Int);
                Sqlcmd.Parameters["@codHabitacion"].Value = (habitacion.hab_Codigo);

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

            }
            catch (Exception err) { throw err; }
        }

        public DataTable InformacionPaciente(HABITACIONES habitacion)
        {
            try
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

                Sqlcmd = new SqlCommand("sp_RecuperaHistoriaClinicaHabitaciones", Sqlcon);

                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Parameters.Add("@codHabitacion", SqlDbType.Int);
                Sqlcmd.Parameters["@codHabitacion"].Value = (habitacion.hab_Codigo);

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

            }
            catch (Exception err) { throw err; }
        }


        #region Listas
        public List<HABITACIONES> listaHabitaciones(Int32 CodigoPiso)/*Carga la lista de habitaciones segun el piso / Giovanny Tapia / 04/02/2012*/
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HABITACIONES> habitaciones = contexto.HABITACIONES.Include("NIVEL_PISO").Include("HABITACIONES_ESTADO").Where(h => h.hab_Activo == true && h.NIVEL_PISO.NIV_CODIGO == CodigoPiso).OrderBy(h => h.hab_Numero).ToList();
                return habitaciones;
            }
        }

        public bool ParametroHabEmergencia()
        {
            bool valor = false;
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("SELECT PAD_ACTIVO FROM PARAMETROS_DETALLE WHERE PAD_CODIGO = 28", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                valor = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            reader.Close();
            connection.Close();
            return valor;
        }
        public List<HABITACIONES> listaHabitacionesEmergenciaLX()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                if (ParametroHabEmergencia())
                {
                    List<HABITACIONES> habitaciones = contexto.HABITACIONES.Include("NIVEL_PISO").Include("HABITACIONES_ESTADO").Where(h => h.hab_Activo == true && h.NIVEL_PISO.NIV_NOMBRE == "EMERGENCIA" || h.NIVEL_PISO.NIV_NOMBRE == "EMERGENCIA ALEMANIA").OrderBy(h => h.hab_Numero).ToList();
                    return habitaciones;
                }
                else
                {
                    List<HABITACIONES> habitaciones = contexto.HABITACIONES.Include("NIVEL_PISO").Include("HABITACIONES_ESTADO").Where(h => h.hab_Activo == true && h.NIVEL_PISO.NIV_NOMBRE == "EMERGENCIA").OrderBy(h => h.hab_Numero).ToList();
                    return habitaciones;
                }
            }
        }

        public List<HABITACIONES> listaHabitacionesMushuñan()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HABITACIONES> habitaciones = contexto.HABITACIONES.Include("NIVEL_PISO").Include("HABITACIONES_ESTADO").Where(h => h.hab_Activo == true && h.NIVEL_PISO.NIV_NOMBRE == "MUSHUÑAN").OrderBy(h => h.hab_Numero).ToList();
                return habitaciones;
            }
        }
        public List<HABITACIONES> listaHabitacionesBrigadaMedica()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HABITACIONES> habitaciones = contexto.HABITACIONES.Include("NIVEL_PISO").Include("HABITACIONES_ESTADO").Where(h => h.hab_Activo == true && h.NIVEL_PISO.NIV_NOMBRE == "BRIGADA MEDICA").OrderBy(h => h.hab_Numero).ToList();
                return habitaciones;
            }
        }
        public List<HABITACIONES> listaHabitacionesConsultorios()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HABITACIONES> habitaciones = contexto.HABITACIONES.Include("NIVEL_PISO").Include("HABITACIONES_ESTADO").Where(h => h.hab_Activo == true && h.NIVEL_PISO.NIV_NOMBRE == "CONSULTA EXTERNA").OrderBy(h => h.hab_Numero).ToList();
                return habitaciones;
            }
        }
        public List<HABITACIONES> listaHabitaciones()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HABITACIONES> habitaciones = contexto.HABITACIONES.Include("NIVEL_PISO").Include("HABITACIONES_ESTADO").Where(h => h.hab_Activo == true).OrderBy(h => h.NIVEL_PISO.NIV_NOMBRE).ToList();
                return habitaciones;
            }
        }
        public List<HABITACIONES> listaHabitacionesXpiso(Int64 NIV_CODIGO)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HABITACIONES> habitaciones = contexto.HABITACIONES.Include("NIVEL_PISO").Include("HABITACIONES_ESTADO").Where(h => h.hab_Activo == true && h.NIVEL_PISO.NIV_CODIGO == NIV_CODIGO).OrderBy(h => h.NIVEL_PISO.NIV_NOMBRE).ToList();
                return habitaciones;
            }
        }
        public DataTable listaHabitacionesActivas()
        {
            DataTable Habitaciones = new DataTable();
            SqlConnection Sqlcon;
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
            SqlCommand cmd = new SqlCommand("sp_HabitacionesDisponibles", Sqlcon);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(Habitaciones);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Habitaciones;
        }

        public List<HABITACIONES> ListaTodasHabitaciones()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HABITACIONES> habitaciones = new List<HABITACIONES>();
                habitaciones = contexto.HABITACIONES.Include("NIVEL_PISO").Include("HABITACIONES_ESTADO").ToList();
                return habitaciones;
            }
        }

        public bool VerificaEpicrisis(string NumeroHabitacion) // Verifica si la atencion en una habitacion activa tiene epicrisis  / Giovanny Tapia / 11/09/20012
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var fecha = Convert.ToDateTime("31/12/2011 0:00:00");
                HABITACIONES atencion = (from h in contexto.HABITACIONES
                                         join a in contexto.ATENCIONES on h.hab_Codigo equals a.HABITACIONES.hab_Codigo
                                         join hc in contexto.HC_EPICRISIS on a.ATE_CODIGO equals hc.ATENCIONES.ATE_CODIGO
                                         where ((h.hab_Numero == NumeroHabitacion) && (a.ATE_FECHA_INGRESO > fecha) && (h.hab_Activo == true) && (a.ATE_ESTADO == true) && (a.ATE_FECHA_ALTA == null) && (a.ESC_CODIGO == 1))
                                         select h).FirstOrDefault();
                if (atencion != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<HABITACIONES> listaHabitacionesEmergencia()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HABITACIONES> habitaciones = new List<HABITACIONES>();
                habitaciones = contexto.HABITACIONES.Include("NIVEL_PISO").Include("HABITACIONES_ESTADO").Where(h => h.hab_Codigo == 0).ToList();
                return habitaciones;
            }
        }

        /// <summary>
        /// Metodo que devuelve un listado de habitacioens por piso
        /// </summary>
        /// <param name="codigoNivelPiso">Codigo del NivelPiso</param>
        /// <returns>
        /// Lista de HABITACIONES
        /// </returns>
        public List<HABITACIONES> listaHabitaciones(Int16 codigoNivelPiso)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //List<HABITACIONES>  habitacionesLista = (from h in contexto.HABITACIONES
                //                                         join e in contexto.HABITACIONES_ESTADO on h.HABITACIONES_ESTADO.HES_CODIGO equals e.HES_CODIGO
                //                   where h.NIVEL_PISO.NIV_CODIGO == codigoNivelPiso
                //                   select h).ToList() ;
                List<HABITACIONES> habitacionesLista = new List<HABITACIONES>();
                habitacionesLista = contexto.HABITACIONES.Include("NIVEL_PISO").Include("HABITACIONES_ESTADO").Where(h => h.NIVEL_PISO.NIV_CODIGO == codigoNivelPiso && h.hab_Activo == true).ToList();
                return habitacionesLista;
            }
        }

        /// <summary>
        /// Metodo que devuelve un listado de habitaciones por piso y estado
        /// </summary>
        /// <param name="codigoNivelPiso">Codigo del NivelPiso</param>
        /// <returns>
        /// Lista de HABITACIONES
        /// </returns>
        public List<HABITACIONES> listaHabitaciones(Int16 codigoNivelPiso, Int16 codigoEstadoHabitacion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<HABITACIONES> habitacionesLista = new List<HABITACIONES>();
                    habitacionesLista = contexto.HABITACIONES.Where(h => h.NIVEL_PISO.NIV_CODIGO == codigoNivelPiso && h.hab_Activo == true && h.HABITACIONES_ESTADO.HES_CODIGO == codigoEstadoHabitacion).ToList();
                    return habitacionesLista;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        #endregion
        #endregion

        #region Habitaciones Detalle

        public void CrearHabitacionDetalle(HABITACIONES_DETALLE dethabitacion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("HABITACIONES_DETALLE", dethabitacion);
            }
        }
        /// <summary>
        /// Metodo que actualiza la informacion del detalle de habitacion
        /// </summary>
        /// <param name="habitacionDetalleModificada">HABITACIONES_DETALLE</param>
        public void ActualizarDetallehabitacion(HABITACIONES_DETALLE habitacionDetalleModificada)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                try
                {
                    var habDetalle = contexto.HABITACIONES_DETALLE.FirstOrDefault(h => h.HAD_CODIGO == habitacionDetalleModificada.HAD_CODIGO);
                    habDetalle.HAD_ENCARGADO = habitacionDetalleModificada.HAD_ENCARGADO;
                    habDetalle.HAD_FECHA_ALTA_MEDICO = habitacionDetalleModificada.HAD_FECHA_ALTA_MEDICO;
                    habDetalle.HAD_FECHA_DISPONIBILIDAD = habitacionDetalleModificada.HAD_FECHA_DISPONIBILIDAD;
                    habDetalle.HAD_FECHA_FACTURACION = habitacionDetalleModificada.HAD_FECHA_FACTURACION;
                    habDetalle.HAD_FECHA_INGRESO = habitacionDetalleModificada.HAD_FECHA_INGRESO;
                    habDetalle.HAD_OBSERVACION = habitacionDetalleModificada.HAD_OBSERVACION;
                    habDetalle.HAD_REGISTRO_ANTERIOR = habitacionDetalleModificada.HAD_REGISTRO_ANTERIOR;
                    habDetalle.ID_USUARIO = habitacionDetalleModificada.ID_USUARIO;
                    contexto.SaveChanges();
                }
                catch (Exception e) { throw (e); }
            }
        }

        public int RecuperaMaximoDetalleHabitacion()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var id = contexto.HABITACIONES_DETALLE.OrderByDescending(h => h.HAD_CODIGO).FirstOrDefault();
                if (id != null) return id.HAD_CODIGO + 1;
                return 1;
            }
        }
        #region Listas
        public List<HABITACIONES_DETALLE> DetalleHabitacion()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                return contexto.HABITACIONES_DETALLE.Include("HABITACIONES").ToList();
            }
        }
        public HABITACIONES_DETALLE RecuperarDetalleHabitacion(Int16 numdetalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HABITACIONES_DETALLE detalle = contexto.HABITACIONES_DETALLE.Where(h => h.HAD_CODIGO == numdetalle).FirstOrDefault();
                return detalle;
            }
        }
        /// <summary>
        /// Metodo que recupera el detalle de una habitacion
        /// </summary>
        /// <param name="codigoAtencion">codigo de la atencion</param>
        /// <returns>Objeto HABITACIONES_DETALLE</returns>
        public HABITACIONES_DETALLE RecuperarDetalleHabitacion(ATENCIONES atencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HABITACIONES_DETALLE detalle = (from h in contexto.HABITACIONES_DETALLE
                                                    where h.ATE_CODIGO == atencion.ATE_CODIGO && atencion.ESC_CODIGO == 1 && h.HAD_FECHA_DISPONIBILIDAD == null
                                                    orderby h.HAD_FECHA_INGRESO descending
                                                    select h).FirstOrDefault();
                    return detalle;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Metodo que recupera el detalle de una habitacion
        /// </summary>
        /// <param name="codigoAtencion">codigo habitacion</param>
        /// <returns>Objeto HABITACIONES_DETALLE</returns>
        public HABITACIONES_DETALLE RecuperarDetalleHabitacion(HABITACIONES habitacion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HABITACIONES_DETALLE detalle = (from h in contexto.HABITACIONES_DETALLE
                                                    where h.HABITACIONES.hab_Codigo == habitacion.hab_Codigo
                                                            && h.HAD_FECHA_DISPONIBILIDAD == null
                                                    select h).OrderByDescending(h => h.HAD_FECHA_INGRESO).FirstOrDefault();
                    return detalle;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Metodo que recupera el detalle de una habitacion
        /// </summary>
        /// <param name="codHabitacion">Codigo de la habitación</param>
        /// <param name="codAtencion">Codigo de la Atención</param>
        /// <returns>Objeto HABITACIONES_DETALLE</returns>
        public HABITACIONES_DETALLE RecuperarDetalleHabitacion(Int16 codHabitacion, int codAtencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HABITACIONES_DETALLE detalle = (from h in contexto.HABITACIONES_DETALLE
                                                    where h.HABITACIONES.hab_Codigo == codHabitacion
                                                            && h.HAD_FECHA_DISPONIBILIDAD == null
                                                            && h.ATE_CODIGO == codAtencion
                                                    select h).OrderByDescending(h => h.HAD_FECHA_INGRESO).FirstOrDefault();
                    return detalle;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        #endregion

        #endregion

        #region Habitaciones Estado

        public List<HABITACIONES_ESTADO> ListaEstadosdeHabitacion()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.HABITACIONES_ESTADO.Where(h => h.HES_ACTIVO == true).ToList();
            }
        }

        public HABITACIONES_ESTADO RecuperarEstadoHabitacion(Int16 codEstado)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from e in contexto.HABITACIONES_ESTADO
                        where e.HES_CODIGO == codEstado
                        select e).FirstOrDefault();
            }
        }



        #endregion

        #region Habitaciones Tipo

        /// <summary>
        /// Metodo que recupera la lista de tipo de habitaciones
        /// </summary>
        /// <returns>Retorna la lista de tipo de habitaciones</returns>
        public List<HABITACIONES_TIPO> RecuperaListaHabitacionTipo()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HABITACIONES_TIPO> listaTipoHabitaciones = contexto.HABITACIONES_TIPO.Where(h => h.HAT_ESTADO == true).ToList();
                return listaTipoHabitaciones;
            }
        }

        #endregion region

        #region Pisos
        public List<NIVEL_PISO> listaNivelesPiso()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<NIVEL_PISO> listaNivelPiso = (from n in contexto.NIVEL_PISO
                                                       select n).ToList();
                    return listaNivelPiso;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        //PABLO ROCHA 07-03-2019 RECUEPRA EL NOMBRE DEL PACIENTE
        public Int32 RecuperaNombrePacientes()
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
            Sqlcmd = new SqlCommand("sp_RecuperaNombrePacientes", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

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
            return 1;
        }
        public DataTable PisoBodega(string ip)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("SELECT * FROM NIVEL_PISO_MAQUINA WHERE IP_MAQUINA = @ip", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@ip", ip);
            reader = command.ExecuteReader();
            tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return tabla;
        }
        public Int32 RecuperaCodigoPiso(string IpMaquina)/*Recupera el codigo del piso*/
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
            Sqlcmd = new SqlCommand("sp_CodigoPiso", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@IpMaquina", SqlDbType.VarChar);
            Sqlcmd.Parameters["@IpMaquina"].Value = (IpMaquina);

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            if (Dts.Tables["tabla"].Rows.Count > 0)
            {
                return Convert.ToInt32(Dts.Tables["tabla"].Rows[0]["NIV_CODIGO"].ToString());
            }
            else
            {
                return -1;
            }

        }

        public List<NIVEL_PISO> listaNivelesPiso(Int32 CodigoPiso)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<NIVEL_PISO> listaNivelPiso = (from n in contexto.NIVEL_PISO
                                                       where n.NIV_CODIGO == CodigoPiso
                                                       select n).ToList();
                    return listaNivelPiso;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        #endregion region

        #region Vistas
        public List<HABITACIONES_ATENCION_VISTA> RecuperarDetallesHabitacion(string medCodigo, string ateCodigo, string habCodigo, string pacCodigo, string habNumero, string hadCodigo, string estHabCodigo, string atencionEstado, string hadDisponible)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.ConsultaDetalleHabitaciones(medCodigo, ateCodigo, habCodigo, pacCodigo, habNumero, hadCodigo, estHabCodigo, atencionEstado, hadDisponible).ToList();
            }
        }
        #endregion

        public bool HabitacionesEnOrden()
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

                Sqlcmd = new SqlCommand("sp_HabitacionesOcupadas", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts, "tabla");
                DataTable habitacion = Dts.Tables["tabla"];



                Sqlcmd = new SqlCommand("sp_AtencionesActivas", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts, "tabla");
                DataTable atencion = Dts.Tables["tabla"];


                //if (habitacion.Rows.Count != atencion.Rows.Count)
                //{
                    Int64[] id1 = new Int64[habitacion.Rows.Count];
                    Int64[] id2 = new Int64[atencion.Rows.Count];

                    for (int i = 0; i < habitacion.Rows.Count; i++)
                    {
                        id1[i] = Convert.ToInt64(habitacion.Rows[i][0].ToString());
                    }
                    for (int i = 0; i < atencion.Rows.Count; i++)
                    {
                        id2[i] = Convert.ToInt64(atencion.Rows[i][0].ToString());
                    }

                    Int64 [] nonintersect = id1.Except(id2).ToArray();

                    for (int i = 0; i < nonintersect.Length; i++)
                    {
                        Int64 habit = nonintersect[i];
                        Sqlcmd = new SqlCommand("sp_ArreglaHabitacionesAtenciones", Sqlcon);
                        Sqlcmd.CommandType = CommandType.StoredProcedure;
                        Sqlcmd.Parameters.Add("@hab_codigo", SqlDbType.Int);
                        Sqlcmd.Parameters["@hab_codigo"].Value = (habit);
                        Sqldap = new SqlDataAdapter();
                        Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                        Sqldap.SelectCommand = Sqlcmd;
                        Dts = new DataSet();
                        Sqldap.Fill(Dts);
                    }
                //}

                Sqlcon.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }



        //RECUPERA INFORMACION GARANTIAS PABLO ROCHA 05/09/2014
        public DataTable sp_HabitacionesCenso(int cod)
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

            Sqlcmd = new SqlCommand("sp_DtoPacientesAtencionesActivas_2", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@PISO", SqlDbType.Int);
            Sqlcmd.Parameters["@PISO"].Value = (cod);
            //HOPITALIZADOS 1   --TODOS 0  --EMERGENCI 2  --OTROS 3
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

        }

        public DataTable VerificaCantidadStock(Int64 cod, int bodega)
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
            Sqlcmd = new SqlCommand("sp_VerificaCantidadStock", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@bodega", SqlDbType.Int);
            Sqlcmd.Parameters["@bodega"].Value = (bodega);

            Sqlcmd.Parameters.Add("@cod", SqlDbType.BigInt);
            Sqlcmd.Parameters["@cod"].Value = (cod);
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
        }


        public string getNombreHabitacion(int ate_codigo)
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
            Sqlcmd = new SqlCommand("SELECT       dbo.HABITACIONES.hab_Numero FROM            dbo.HABITACIONES INNER JOIN dbo.ATENCIONES ON dbo.HABITACIONES.hab_Codigo = dbo.ATENCIONES.HAB_CODIGO "
                   + " WHERE dbo.ATENCIONES.ATE_CODIGO = " + ate_codigo + " ", Sqlcon);
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
                return (Convert.ToString(Dts.Rows[0][0]));
            }
            else
            {
                return "0";
            }

        }

        public DataTable HabitacionNombre(int codHabitacion)
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
            Sqlcmd = new SqlCommand("sp_HabitacionNombre", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@codHabitacion", SqlDbType.Int);
            Sqlcmd.Parameters["@codHabitacion"].Value = (codHabitacion);

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
        }

        public DataTable VerificaH008(Int64 ate_codigo)
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
            Sqlcmd = new SqlCommand("sp_VerificaH008", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ate_codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@ate_codigo"].Value = (ate_codigo);

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
        }

        public DataTable Devolviendo(Int64 cue_codigo)
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
            Sqlcmd = new SqlCommand("sp_Devolviendo", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@cue_codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@cue_codigo"].Value = (cue_codigo);

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
        }


        public DataTable VerificaParametroHoja008()
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
            Sqlcmd = new SqlCommand("sp_VerificaParametroHoja008", Sqlcon);
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
        }

        public DataTable ValidadorHabitaciones()
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
            Sqlcmd = new SqlCommand("sp_ValidadorHabitaciones", Sqlcon);
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
        }

        public DataTable ObtieneCodPedido(Int64 codPed)
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
            Sqlcmd = new SqlCommand("sp_ObtieneCodPedido", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@codPed", SqlDbType.Int);
            Sqlcmd.Parameters["@codPed"].Value = (codPed);

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
        }

        public DateTime RecuperaFechaNacimiento(string hc)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            DateTime fecha = DateTime.Now;
            BaseContextoDatos obj = new BaseContextoDatos();
            try
            {
                con = obj.ConectarBd();
                cmd = new SqlCommand("sp_RecuperaFechaNacimiento", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hc", hc);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    fecha = Convert.ToDateTime(dr["PAC_FECHA_NACIMIENTO"].ToString());
                }
            }
            catch (Exception ex)
            {
                fecha = DateTime.Now;
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return fecha;
        }
        public void GuardarMedicosAlta(string medico, Int64 ate_codigo, string observacion, Int64 usuario)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_SaveMedicosAlta", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@medico", medico);
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@observacion", observacion);
            command.Parameters.AddWithValue("@usuario", usuario);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
        public DataTable CargarMedicosAlta(Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_CargarMedicosAlta", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;

        }
        public int HabDisponible(int hab_codigo)//Verifica si la habitacion aun sigue disponible
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            int estado = 0;
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_Hab_Disponible", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hab_codigo", hab_codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                estado = Convert.ToInt32(reader["HES_CODIGO"].ToString());
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return estado;
        }
        public double HorasPermitidas(Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            double Horas = 0;
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ReversionTiempo", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Horas = Convert.ToDouble(reader["HORAS"].ToString());
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Horas;
        }
    }
}
