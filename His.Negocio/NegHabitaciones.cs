using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;
using System.Data;

namespace His.Negocio
{
	public class NegHabitaciones
	{
		public static List<HABITACIONES> listaHabitaciones()
		{
			return new DatHabitaciones().listaHabitaciones();
		}
		public static List<HABITACIONES> listaHabitacionesXpiso(Int64 NIV_CODIGO)
		{
			return new DatHabitaciones().listaHabitacionesXpiso(NIV_CODIGO);
		}
		public static DataTable listaHabitacionesActivas()
		{
			return new DatHabitaciones().listaHabitacionesActivas();
		}

		public static bool HabitacionesEnOrden()
		{
			try
			{
				return new DatHabitaciones().HabitacionesEnOrden();
			}
			catch (Exception)
			{

				throw;
			}
		}
		public static string getNombreHabitacion(int ate_codigo)
		{
			return new DatHabitaciones().getNombreHabitacion(ate_codigo);
		}


		public static List<HABITACIONES> ListaTodasHabitaciones()
		{
			return new DatHabitaciones().ListaTodasHabitaciones();
		}


		public static List<HABITACIONES> listaHabitaciones(Int32 CodigoPiso)
		{
			return new DatHabitaciones().listaHabitaciones(CodigoPiso);
		}

		//public static Int32 RecuperaCodigoPiso()
		//{
		//    return new DatHabitaciones().listaHabitaciones();
		//}

		public static bool VerificaEpicrisis(string NumeroHabitacion)// Verifica si la atencion en una habitacion activa tiene epicrisis  / Giovanny Tapia / 11/09/20012
		{
			return new DatHabitaciones().VerificaEpicrisis(NumeroHabitacion);
		}


		public static List<HABITACIONES> listaHabitacionesEmergencia()
		{
			return new DatHabitaciones().listaHabitacionesEmergencia();
		}
		public static List<HABITACIONES> listaHabitacionesEmergenciaLX()
		{
			return new DatHabitaciones().listaHabitacionesEmergenciaLX();
		}

		public static List<HABITACIONES> listaHabitacionesMushuñan()
		{
			return new DatHabitaciones().listaHabitacionesMushuñan();
		}
		public static List<HABITACIONES> listaHabitacionesBrigadaMedica()
		{
			return new DatHabitaciones().listaHabitacionesBrigadaMedica();
		}
		public static List<HABITACIONES> listaHabitacionesConsultorios()
		{
			return new DatHabitaciones().listaHabitacionesConsultorios();
		}
		public static List<NIVEL_PISO> listaNivelesPiso()
		{
			return new DatHabitaciones().listaNivelesPiso();
		}

		public static Int32 RecuperaCodigoPiso(string IpMaquina)/*Filtra los pisos segun la IP*/
		{
			return new DatHabitaciones().RecuperaCodigoPiso(IpMaquina);
		}
		public static DataTable RecuperarPisoBodega(string ip)
        {
			return new DatHabitaciones().PisoBodega(ip);
        }
		public static Int32 RecuperaNombrePacientes()
		{
			return new DatHabitaciones().RecuperaNombrePacientes();
		}

		public static List<NIVEL_PISO> listaNivelesPiso(Int32 CodigoPiso) /*Para filtrar las habitaciones por piso / Giovanny Tapia / 04/02/2013*/
		{
			return new DatHabitaciones().listaNivelesPiso(CodigoPiso);
		}

		public static List<HABITACIONES_ATENCION_VISTA> RecuperarDetallesHabitacion(string medCodigo, string ateCodigo, string habCodigo, string pacCodigo, string hab_numero, string had_codigo, string estHabCodigo, string atencionEstado, string hadDisponible)
		{
			return new DatHabitaciones().RecuperarDetallesHabitacion(medCodigo, ateCodigo, habCodigo, pacCodigo, hab_numero, had_codigo, estHabCodigo, atencionEstado, hadDisponible);
		}
		public static void CrearHabitacionDetalle(HABITACIONES_DETALLE dethabitacion)
		{
			new DatHabitaciones().CrearHabitacionDetalle(dethabitacion);
		}
		public static void GrabarHabitaciones(HABITACIONES habitacionModificada, HABITACIONES habitacionOriginal)
		{
			new DatHabitaciones().GrabarHabitaciones(habitacionModificada, habitacionOriginal);
		}
		public static DataTable HistorialHabitaciones(Int64 HISTORIACLINICA)
		{
			return new DatHabitaciones().HistorialHabitaciones(HISTORIACLINICA);
		}

		public static DataTable HistorialHabitacionesFecha(DateTime fechaIni, DateTime fechaFin)
		{
			return new DatHabitaciones().HistorialHabitacionesFecha(fechaIni, fechaFin);
		}

		/// <summary>
		/// Metodo que actualiza la informacion del detalle de habitacion
		/// </summary>
		/// <param name="habitacionDetalleModificada">HABITACIONES_DETALLE</param>
		public static void ActualizarDetallehabitacion(HABITACIONES_DETALLE habitacionDetalleModificada)
		{
			try
			{
				new DatHabitaciones().ActualizarDetallehabitacion(habitacionDetalleModificada);
			}
			catch (Exception err)
			{
				throw (err);
			}
		}

		public static int RecuperaMaximoDetalleHabitacion()
		{
			return new DatHabitaciones().RecuperaMaximoDetalleHabitacion();
		}
		public static List<HABITACIONES_ESTADO> ListaEstadosdeHabitacion()
		{
			return new DatHabitaciones().ListaEstadosdeHabitacion();
		}
		/// <summary>
		/// Metodo que devuelve un listado de habitacioens por piso
		/// </summary>
		/// <param name="codigoNivelPiso">Codigo del NivelPiso</param>
		/// <returns>
		/// Lista de HABITACIONES
		/// </returns>
		public static List<HABITACIONES> listaHabitaciones(Int16 codigoNivelPiso)
		{
			return new DatHabitaciones().listaHabitaciones(codigoNivelPiso);
		}
		/// <summary>
		/// Metodo que devuelve un listado de habitaciones por piso y estado
		/// </summary>
		/// <param name="codigoNivelPiso">Codigo del NivelPiso</param>
		/// <returns>
		/// Lista de HABITACIONES
		/// </returns>
		public static List<HABITACIONES> listaHabitaciones(Int16 codigoNivelPiso, Int16 codigoEstadoHabitacion)
		{
			try
			{
				return new DatHabitaciones().listaHabitaciones(codigoNivelPiso, codigoEstadoHabitacion);
			}
			catch (Exception err)
			{
				throw err;
			}
		}

		//public static int UltimoDetalle(string numHabitacion)
		//{
		//    return new DatHabitaciones().UltimoDetalle(numHabitacion);
		//}
		public static List<HABITACIONES_DETALLE> DetalleHabitacion()
		{
			return new DatHabitaciones().DetalleHabitacion();
		}
		public static HABITACIONES_DETALLE RecuperarDetalleHabitacion(Int16 numdetalle)
		{
			return new DatHabitaciones().RecuperarDetalleHabitacion(numdetalle);
		}
		/// <summary>
		/// Metodo que recupera el detalle de una habitacion
		/// </summary>
		/// <param name="codigoAtencion">codigo de la atencion</param>
		/// <returns>Objeto HABITACIONES_DETALLE</returns>
		public static HABITACIONES_DETALLE RecuperarDetalleHabitacion(ATENCIONES atencion)
		{
			try
			{
				return new DatHabitaciones().RecuperarDetalleHabitacion(atencion);
			}
			catch (Exception err)
			{
				throw err;
			}
		}
		/// <summary>
		/// Metodo que recupera el detalle de una habitacion
		/// </summary>
		/// <param name="codigoAtencion">codigo de la habitacion</param>
		/// <returns>Objeto HABITACIONES_DETALLE</returns>
		public static HABITACIONES_DETALLE RecuperarDetalleHabitacion(HABITACIONES habitacion)
		{
			try
			{
				return new DatHabitaciones().RecuperarDetalleHabitacion(habitacion);
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
		/// <returns></returns>
		public static HABITACIONES_DETALLE RecuperarDetalleHabitacion(Int16 codHabitacion, int codAtencion)
		{
			try
			{
				return new DatHabitaciones().RecuperarDetalleHabitacion(codHabitacion, codAtencion);
			}
			catch (Exception err) { throw err; }
		}

		/// <summary>
		/// Metodo que recupera la lista de tipo de habitaciones
		/// </summary>
		/// <returns>Retorna la lista de tipo de habitaciones</returns>
		public static List<HABITACIONES_TIPO> RecuperaListaHabitacionTipo()
		{
			return new DatHabitaciones().RecuperaListaHabitacionTipo();
		}
		public static HABITACIONES RecuperarHabitacionId(Int16 codigo)
		{
			return new DatHabitaciones().RecuperarHabitacionID(codigo);
		}
		/// <summary>
		/// Retorna las habitasines sigún su estado
		/// </summary>
		/// <param name="codigo">Estado de la habitación</param>
		/// <returns></returns>
		//public static List<DtoHabitacionesOcupadas> listaHabitacionesOcupadas(Int16 estado)
		//{
		//    return new DatHabitaciones().listaHabitacionesOcupadas(estado);
		//}

		/// <summary>
		/// Metodo que recupera una habitacion por su numero
		/// </summary>
		/// <returns>Retorna una habitacion</returns>
		public static HABITACIONES RecuperaHabitacionPorNumero(string numero)
		{
			return new DatHabitaciones().RecuperaHabitacionPorNumero(numero);
		}

		public static HABITACIONES_ESTADO RecuperarEstadoHabitacion(Int16 codEstado)
		{
			return new DatHabitaciones().RecuperarEstadoHabitacion(codEstado);
		}

		public static bool CambiarEstadoHabitacion(HABITACIONES mHabitacion)
		{
			return new DatHabitaciones().CambiarEstadoHabitacion(mHabitacion);
		}

		public static bool CambiaEstadoHabitacion(Int64 ateCodigo, int had_codigo)
		{
			return new DatHabitaciones().CambiaEstadoHabitacion(ateCodigo, had_codigo);
		}
		public static bool CambiaEstadoHabitacionMantenimiento(int had_codigo, int estado)
		{
			return new DatHabitaciones().CambiaEstadoHabitacionMantenimiento(had_codigo, estado);
		}

		public static DataTable RevertirMovimientoHabitacion(HABITACIONES mHabitacion)
		{
			return new DatHabitaciones().RevertirMovimientoHabitacion(mHabitacion);
		}

		public static DataTable InformacionPaciente(HABITACIONES mHabitacion)
		{
			return new DatHabitaciones().InformacionPaciente(mHabitacion);
		}

		public static DataTable sp_HabitacionesCenso(int cod)
		{
			return new DatHabitaciones().sp_HabitacionesCenso(cod);
		}

		public static DataTable VerificaCantidadStock(Int64 cod, int bodega)
		{
			return new DatHabitaciones().VerificaCantidadStock(cod, bodega);
		}

		public static DataTable HabitacionNombre(int codHabitacion)
		{
			return new DatHabitaciones().HabitacionNombre(codHabitacion);
		}

		public static DataTable VerificaH008(Int64 ate_codigo)
		{
			return new DatHabitaciones().VerificaH008(ate_codigo);
		}

		public static DataTable Devolviendo(Int64 cue_codigo)
		{
			return new DatHabitaciones().Devolviendo(cue_codigo);
		}

		public static DataTable VerificaParametroHoja008()
		{
			return new DatHabitaciones().VerificaParametroHoja008();
		}

		public static DataTable ValidadorHabitaciones()
		{
			return new DatHabitaciones().ValidadorHabitaciones();
		}

		public static DataTable ObtieneCodPedido(Int64 codPed)
		{
			return new DatHabitaciones().ObtieneCodPedido(codPed);
		}

		public static DateTime RecuperaFechaNacimiento(string hc)
		{
			return new DatHabitaciones().RecuperaFechaNacimiento(hc);
		}
		public static DataTable AreaActualHab(Int64 ate_codigo)
		{
			return new DatHabitaciones().AreaActualHab(ate_codigo);
		}
		public static void SaveMedicosAlta(string medico, Int64 ate_codigo, string observacion, Int64 usuario)
		{
			new DatHabitaciones().GuardarMedicosAlta(medico, ate_codigo, observacion, usuario);
		}
		public static DataTable CargarMedicos(Int64 ate_codigo)
		{
			return new DatHabitaciones().CargarMedicosAlta(ate_codigo);
		}
		public static int HabDisponible(int hab_codigo)
		{
			return new DatHabitaciones().HabDisponible(hab_codigo);
		}
		public static double HorasTranscurridas(Int64 ate_codigo)
		{
			return new DatHabitaciones().HorasPermitidas(ate_codigo);
		}
	}
}
