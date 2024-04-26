using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.General;
using System.Data;  

namespace His.Negocio
{
    public class NegPacientes
    {
        public static List<DtoPacientesInfo> ListaPacientesInfo(string tiponivel)
        {
            return new DatPacientes().ListaPacientesInfo(tiponivel);
        }
        //. Recupera la lista por defecto de pacientes
        public static List<PACIENTES> RecuperarPacientesLista()
        {
            try
            {            
                return new DatPacientes().RecuperarPacientesLista();
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        //. Recupera la lista por defecto de pacientes
        public static List<PACIENTES> RecuperarPacientesLista(int desde, int cantidad)
        {
            try
            {
                return new DatPacientes().RecuperarPacientesLista(desde, cantidad);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Metodo que recupera un listado de los pacientes que se encuentran ingresados
        /// </summary>
        /// <returns>Listado de objectos DtoPacientesAtencionesActivas </returns>
        public static List<DtoPacientesAtencionesActivas> RecuperarPacientesAtencionActiva(string conEpicrisis)
        {
            try
            {
                return new DatPacientes().RecuperarPacientesAtencionActiva(conEpicrisis);
            }
            catch (Exception err){throw err;}
        }
        /// <summary>
        /// Metodo que recupera un listado de los pacientes y sus ultimos ingresos
        /// </summary>
        /// <returns>Listado de objectos DtoPacientesAtencionesActivas </returns>
        public static List<DtoPacientesAtencionesActivas> RecuperarPacientesAtencionUltimas(int cantidad, bool? estadoAtencion, int? codAtencion, Int16? codHabitacion)
        {
            try
            {
                return new DatPacientes().RecuperarPacientesAtencionUltimas(cantidad,estadoAtencion,codAtencion,codHabitacion);
            }
            catch (Exception err) { throw err; }
        }
       
        public static List<DtoPacientesAtencionesActivas> RecuperarPacientesAtencionUltimasReporte(int cantidad, bool? estadoAtencion, int? codAtencion, Int16? codHabitacion, int piso)
        {
            try
            {
                return new DatPacientes().RecuperarPacientesAtencionUltimasReporte(cantidad, estadoAtencion, codAtencion, codHabitacion, piso);
            }
            catch (Exception err) { throw err; }
        }

        public static List<DtoPacientesImagen> RecuperarPacientesImagen()
        {
            try
            {
                DatPacientes paciente = new DatPacientes();
                List<DtoPacientesImagen> lista = new List<DtoPacientesImagen>();
                return lista = paciente.RecuperarPacientesImagen();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<RecuperaDietetica> RecuperaDietetica(string historiaClinica)
        {
            return new DatPacientes().RecuperaDietetica(historiaClinica);
        }

        public static List<NIVEL_PISO> recuperarListaPisos()
        {
            return new DatPedidos().recuperarListaPisos();
        }

        public static DataTable registropaciente(string  atencion,int  condicion)
        {
            return new DatPacientes().registropaciente(atencion,condicion);
        }
        /// <summary>
        /// Metodo que recupera un listado de las atenciones de los pacientes
        /// </summary>
        /// <returns>Listado de objectos DtoPacientesAtenciones </returns>
        public static List<ATENCIONES_PACIENTES_VISTAS_DETALLE> RecuperarPacientesAtenciones(DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                return new DatPacientes().RecuperarPacientesAtenciones (fechaIni, fechaFin);
            }
            catch (Exception err) { throw err; }
        }

        public static List<ATENCIONES_PACIENTES_VISTAS_DETALLE> RecuperarPacientesAtenciones(DateTime fechaIni, DateTime fechaFin, string codTipoIngreso)
        {
            try
            {
                return new DatPacientes().RecuperarPacientesAtenciones(fechaIni, fechaFin, codTipoIngreso);
            }
            catch (Exception err) { throw err; }
        }

        //. Recupera la lista por defecto de pacientes
        public static List<PACIENTES> RecuperarPacientesLista(Int16 codigoTipoTratamiento)
        {
            return new DatPacientes().RecuperarPacientesLista(codigoTipoTratamiento);
        }

        //. Recupera la lista por defecto de pacientes
        public static List<PACIENTES_VISTA> RecuperarPacientesLista(string fechaCreacionIni, string fechaCreacionFin,
            string fechaIngresoIni, string fechaIngresoFin, string fechaAltaIni, string fechaAltaFin, string atencionActiva, string codigoMedico, string codigoAseguradoraEmpresa)
        {
            try
            {
            return new DatPacientes().RecuperarPacientesLista(Convert.ToDateTime(fechaCreacionIni), Convert.ToDateTime(fechaCreacionFin),
                Convert.ToDateTime(fechaIngresoIni), Convert.ToDateTime(fechaIngresoFin), Convert.ToDateTime(fechaAltaIni),
                Convert.ToDateTime(fechaAltaFin), Convert.ToBoolean(atencionActiva),codigoMedico,codigoAseguradoraEmpresa);

                }
            catch (Exception err)
            {
                throw err;
            }
        }
        //. Recupera el listado de pacientes y su codigo
        public static List<KeyValuePair<int, string>> RecuperarPacientesNombresLista()
        {
            return new DatPacientes().RecuperarPacientesNombresLista();
        }
        //. Recupera el numero mayor del codigo de pacientes
        public static int RecuperaMaximoPacienteCodigo()
        {
            return new DatPacientes().RecuperaMaximoPacienteCodigo();
        }
        //. Recupera el numero mayor de historia clinica del pacientes
        public static DataTable RecuperaMaximoPacienteHistoriaClinica()
        {
            return new DatPacientes().RecuperaMaximoPacienteHistoriaClinica();
        }
        public static DataTable RecuperaMaximoPacienteNumeroAtencion()
        {
            return new DatPacientes().RecuperaMaximoPacienteNumeroAtencion();
        }
        public static void crearPaciente(PACIENTES paciente)
        {
            new DatPacientes().CrearPaciente(paciente);
        }

        public static void crearPacienteSP(PACIENTES paciente)
        {
            new DatPacientes().CrearPacienteSP(paciente);
        }


        public static void ActualizarPacienteAtencion(PACIENTES paciente)
        {
            new DatPacientes().ActualizarPacienteAtencion(paciente);
        }
        public static DtoPacientes RecuperarDtoPacienteID(int codPaciente)
        {
            return new DatPacientes().RecuperarDtoPacienteID(codPaciente);
        }
        public static PACIENTES RecuperarPacienteID(Int64 codPaciente)
        {
            return new DatPacientes().RecuperarPacienteID(codPaciente);
        }
        public static PACIENTES RecuperarPacienteCedula(string codPaciente)
        {
            return new DatPacientes().RecuperarPacienteCedula(codPaciente);
        }
        public static PACIENTES RecuperarPacienteID(string historiaClinica)
        {
            return new DatPacientes().RecuperarPacienteID(historiaClinica);
        }
        public static DtoPacientes RecuperarDtoPacienteID(string historia)
        {
            return new DatPacientes().RecuperarDtoPacienteID(historia);
        }
        public static List<DtoPacientes> listaPacientes()
        {
            return new DatPacientes().listaPacientes();
        }
        public static int ultimoCodigoPacientes()
        {
            return new DatPacientes().ultimoCodigoPacientes();
        }
        public static void EditarPaciente(PACIENTES pacienteModificado)
        {
            new DatPacientes().EditarPaciente(pacienteModificado);
        }
        public static List<PACIENTES> listaPacientesFiltros(string id, string historia, string apellido, string nombre)
        {
            return new DatPacientes().listaPacientesFiltros(id, historia, apellido, nombre);
        }
        public static bool existeHCL(string HCL)
        {
            return new DatPacientes().existeHCL(HCL);
        }
        public static PACIENTES pacientePorIdentificacion(string id)
        {
            return new DatPacientes().pacientePorIdentificacion(id);
        }
        public static PACIENTES recuperarPacientePorAtencion(Int64 codAtencion)
        {
            return new DatPacientes().recuperarPacientePorAtencion(codAtencion);
        }

        
        public static DataTable RecuperaFarmacos(string fecha, string usuario, string detalle)
        {
            return new DatPacientes().RecuperaFarmacos(fecha, usuario, detalle);
        }
              
        public DataTable LetrasEvolucion()
        {
            DatPacientes p = new DatPacientes();
            DataTable Tabla = p.RecuperaLetrasEvolucion();
            return Tabla;
        }
        public static DataTable RecuperaResponsable(Int64 Pac_codigo,string Evo_Descripcion)
        {
            return new DatPacientes().RecuperaResponsable(Pac_codigo, Evo_Descripcion);
        }
        public static List<PACIENTES> RecuperarPacientesLista(string id, string historia, string nombre, int cantidad)
        {
            return new DatPacientes().RecuperarPacientesLista(id, historia, nombre, cantidad);
        }

        public static string RecuperarAseguradora(int codPaciente)
        {
            return new DatPacientes().RecuperarAseguradora(codPaciente);
        }
        public static string RecuperarTarifarios(int codAseguradora)
        {
            return new DatPacientes().RecuperarTarifario(codAseguradora);
        }
        /// <summary>
        /// Recupera la lista de pacientes por tipo
        /// </summary>
        /// <param name="id">numero de identificacion</param>
        /// <param name="historia">numero de historia clinica</param>
        /// <param name="nombre">nombre del paciente</param>
        /// <param name="cantidad">cantidad de registrar a recuperar</param>
        /// <returns>list de objectis PACIENTES</returns>
        public static List<PACIENTES> RecuperarPacientesLista(string id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            return new DatPacientes().RecuperarPacientesLista(id, historia, nombre, cantidad,tipoCodigo);
        }
        public static List<PACIENTES> recuperarListaPacientesPedidos(int estado,string busqPedido, string desde, string hasta)
        {
            return new DatPacientes().recuperarListaPacientesPedidos(estado,busqPedido,desde,hasta);
        }

        public static List<DtoPacientesEmergencia> RecuperarPacientesListaEmerg(string id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            return new DatPacientes().RecuperarPacientesListaEmerg(id, historia, nombre, cantidad, tipoCodigo);
        }
        public static List<DtoPacientesEmergencia> RecuperarPacientesListaEmergtriaje(string id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            return new DatPacientes().RecuperarPacientesListaEmergTriaje(id, historia, nombre, cantidad, tipoCodigo);
        }
        
        public static List<DtoPacientesEmergencia> RecuperarPacientesListaSubSecuentes(string id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            return new DatPacientes().RecuperarPacientesListaSubSecuentes(id, historia, nombre, cantidad, tipoCodigo);
        }

        public static List<DtoPacientesEmergencia> RecuperarPacientesListaConsultaExterna(string id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            return new DatPacientes().RecuperarPacientesListaConsultaExterna(id, historia, nombre, cantidad, tipoCodigo);
        }
        public static List<DtoPacientesEmergencia> recuperarPacientesMushunia(string id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            return new DatPacientes().RecuperarPacientesListaMushunia(id, historia, nombre, cantidad, tipoCodigo);  
        }
        public static List<DtoPacientesEmergencia> RecuperarPacientesListaBrigada(string id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            return new DatPacientes().RecuperarPacientesListaBrigada(id, historia, nombre, cantidad, tipoCodigo);  
        }
        public static List<DtoPacientesEmergencia> recuperarPacientesTodos(string id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            return new DatPacientes().RecuperarPacientesListaTodos(id, historia, nombre, cantidad, tipoCodigo);
        }
        public static List<DtoPacientesEmergencia> RecuperaPacienteCunsultaExterna(Int32 id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            return new DatPacientes().RecuperaPacienteCunsultaExterna(id, historia, nombre, cantidad, tipoCodigo);
        }
        public static DataTable EstadosHoja()
        {
            return new DatPacientes().EstadosHoja();
        }

        public static List<PACIENTES_VISTA> recuperarPacientePorHistoria(string HistoriaClinica)
        {
            return new DatPacientes().recuperarPacientePorHistoria(HistoriaClinica);
        }

        public static DataTable getAtencionesFormularios(DateTime desde, DateTime hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fhc, int hc, bool Fformulario, string formulario, bool mushugñan,Int16 areaAsignada=0)
        {
            return new DatPacientes().getAtencionesFormularios(desde, hasta, ingreso, alta, facturacion, Fingreso, Cod_Ingreso, Ftratamiento, Cod_Tratamiento, Fhc, hc, Fformulario, formulario, mushugñan,areaAsignada);
        }

        public static DataTable getPedidosImagen(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fhc, int hc, bool Fformulario, string formulario)
        {
            return new DatPacientes().getPedidosImagen(desde, hasta, ingreso, alta, facturacion, Fingreso, Cod_Ingreso, Ftratamiento, Cod_Tratamiento, Fhc, hc, Fformulario, formulario);
        }

        

        public static DataTable getINEN(string desde, string hasta)
        {
            return new DatPacientes().getINEN(desde, hasta);
        }

        public static DataTable getAtencionesIngresos(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fhc, int hc, bool Festado, bool divididas)
        {
            return new DatPacientes().getAtencionesIngresos(desde, hasta, ingreso, alta, facturacion, Fingreso, Cod_Ingreso, Ftratamiento, Cod_Tratamiento, Fhc, hc, Festado, divididas);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="desde"></param>
        /// <param name="hasta"></param>
        /// <param name="ingreso"></param>
        /// <param name="alta"></param>
        /// <param name="facturacion"></param>
        /// <param name="Fingreso"></param>
        /// <param name="Cod_Ingreso"></param>
        /// <param name="Ftratamiento"></param>
        /// <param name="Cod_Tratamiento"></param>
        /// <param name="Fuser"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static DataTable getCierreAdmision(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fuser, int user)
        {
            return new DatPacientes().getCierreAdmision(desde, hasta, ingreso, alta, facturacion, Fingreso, Cod_Ingreso, Ftratamiento, Cod_Tratamiento, Fuser, user);
        }
        public static DataTable getCierreAlta(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fuser, int user)
        {
            return new DatPacientes().getCierreAlta(desde, hasta, ingreso, alta, facturacion, Fingreso, Cod_Ingreso, Ftratamiento, Cod_Tratamiento, Fuser, user);
        }
        public static DataTable getCierreFacturacion(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fuser, int user)
        {
            return new DatPacientes().getCierreFacturacion(desde, hasta, ingreso, alta, facturacion, Fingreso, Cod_Ingreso, Ftratamiento, Cod_Tratamiento, Fuser, user);
        }
        public static DataTable getAtencionesCierre(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fuser, int user)
        {
            return new DatPacientes().getAtencionesCierre(desde, hasta, ingreso, alta, facturacion, Fingreso, Cod_Ingreso, Ftratamiento, Cod_Tratamiento, Fuser, user);
        }

        public static List<DtoPacientesAtencionesActivas> SinAnamnesis()
        {
            try
            {
                return new DatPacientes().SinAnamnesis();
            }
            catch (Exception err) { throw err; }
        }

        public static List<DtoPacientesAtencionesActivas> SinEpicrisis()
        {
            try
            {
                return new DatPacientes().SinEpicrisis();
            }
            catch (Exception err) { throw err; }
        }
        public static List<DtoPacientesAtencionesActivas> SinEmergencia()
        {
            try
            {
                return new DatPacientes().SinEmergencia();
            }
            catch (Exception err) { throw err; }
        }
        public static List<DtoPacientesAtencionesActivas> SinProtocolo()
        {
            try
            {
                return new DatPacientes().SinProtocolo();
            }
            catch (Exception err) { throw err; }
        }

        public static List<DtoPacientesAtencionesActivas> Todos()
        {
            try
            {
                return new DatPacientes().Todos();
            }
            catch (Exception err) { throw err; }
        }

        public static List<DtoPacientesAtencionesActivas> TipoEmergencia()
        {
            try
            {
                return new DatPacientes().TipoEmergencia();
            }
            catch (Exception err) { throw err; }
        }
        public static List<DtoPacientesAtencionesActivas> TipoHospitalizacion()
        {
            try
            {
                return new DatPacientes().TipoHospitalizacion();
            }
            catch (Exception err) { throw err; }
        }

        public static List<DtoPacientesAtencionesActivas> TipoOtros()
        {
            try
            {
                return new DatPacientes().TipoOtros();
            }
            catch (Exception err) { throw err; }
        }
        public static string EmergenciaEstado(Int64 ate_codigo)
        {
             return new DatPacientes().EmergenciaEstado(ate_codigo);
        }

        public static DataTable PacienteJire(string cedula)
        {
            return new DatPacientes().PacienteJire(cedula);
        }

        public static DataTable BuscaPacienteJire()
        {
            return new DatPacientes().BuscaPacienteJire();
        }
        
        public static DataTable BuscaPacienteEmergencia()
        {
            return new DatPacientes().BuscaPacienteEmergencia();
        }

        public static DataTable BuscaPacienteJireParametro(string hc, string nombre, string cedula)
        {
            return new DatPacientes().BuscaPacienteJireParametro(hc, nombre, cedula);
        }

        public static DataTable RecuperaResultadosImagen(Int64 ateCodigo)
        {
            return new DatPacientes().RecuperaResultadosImagen(ateCodigo);
        }
        public static List<DTOPacientesAtencion> PacientesAtenciones(string id, string historia, string nombre, int cantidad)
        {
            return new DatPacientes().RecuperarPacientesAtenciones(id, historia, nombre, cantidad);
        }
        public static List<DTOPacientesAtencion> PacientesAtencionesMushuñan(string id, string historia, string nombre, int cantidad, int estado)
        {
            return new DatPacientes().RecuperarPacientesAntencionesMushuñan(id, historia, nombre, cantidad, estado);
        }
        public static PACIENTES recuperaPorIdentificacion(string identificacion)
        {
            return new DatPacientes().recuperarPorIdentificacion(identificacion);
        }
        public static DataTable getPedidosLaboratorioClinico(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fhc, int hc, bool Fformulario, string formulario,bool chkCerrado)
        {
            return new DatPacientes().getPedidosLaboratorioClinico(desde, hasta, ingreso, alta, facturacion, Fingreso, Cod_Ingreso, Ftratamiento, Cod_Tratamiento, Fhc, hc, Fformulario, formulario,chkCerrado);
        }
        public static DataTable getPedidosLaboratorioPatologico(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fhc, int hc, bool Fformulario, string formulario, bool chkCerrado)
        {
            return new DatPacientes().getPedidosLaboratorioPatologico(desde, hasta, ingreso, alta, facturacion, Fingreso, Cod_Ingreso, Ftratamiento, Cod_Tratamiento, Fhc, hc, Fformulario, formulario, chkCerrado);
        }
        public static bool EliminarPaciente(Int64 pacCodigo)
        {
            return new DatPacientes().EliminarPAciente(pacCodigo);
        }
        public static DataTable getExploradorFormulariosCext(DateTime desde, DateTime hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fhc, int hc, bool Fformulario, string formulario, bool mushugñan, Int16 areaAsignada = 0)
        {
            return new DatPacientes().getExploradorFormulariosCext(desde, hasta, ingreso, alta, facturacion, Fingreso, Cod_Ingreso, Ftratamiento, Cod_Tratamiento, Fhc, hc, Fformulario, formulario, mushugñan, areaAsignada);
        }
    }
}
