using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using System.Data;

namespace His.Negocio
{
    public class NegHonorariosMedicos
    {
        //public static List<HonorariosMedicosFormulario> RecuperaMedicosHonorarios()
        //{
        //    return new DatHonorariosMedicos().RecuperaMedicosHonorarios();
        //}
        public static List<DtoHonorariosMedicos> RecuperaHonorariosMedicosFormulario(string medCodigo, string sinRetencion)
        {
            return new DatHonorariosMedicos().RecuperaHonorariosMedicosFormulario(medCodigo,sinRetencion);
        }
        public static List<DtoHonorariosMedicos> RecuperaHonorariosMedicosPorAtencion(Int32 codigoAtencion)
        {
            return new DatHonorariosMedicos().RecuperaHonorariosMedicosPorAtencion(codigoAtencion);
        }
        public static void CrearHonorarioMedico(HONORARIOS_MEDICOS honorario)
        {
            new DatHonorariosMedicos().CrearHonorario(honorario);
        }
        public static void ModificarHonorarioMedico(HONORARIOS_MEDICOS honorario, int med_codigo, DateTime fechaCaduca, decimal cubierto, string autorizacion, decimal exceso)
        {
            new DatHonorariosMedicos().ActualizarHonorario(honorario, med_codigo, fechaCaduca, cubierto, autorizacion, exceso);
        }
        public static void GrabarHonorarioMedico(HONORARIOS_MEDICOS honorarioModificado, HONORARIOS_MEDICOS honorarioOriginal)
        {
            new DatHonorariosMedicos().GrabarHonorario(honorarioModificado, honorarioOriginal);
        }
        public static void EliminarHonorarioMedico(int codigoHonorario)
        {
            new DatHonorariosMedicos().EliminarHonorario(codigoHonorario)  ;
        }

        //. Recupera lista por defecto de honorarios medicos
        public static List<HONORARIOS_MEDICOS> RecuperaHonorariosMedicos()
        {
            return DatHonorariosMedicos.RecuperarHonorariosMedicos();
        }
        public static DataTable EstadoCuenta(string fechaIni, string fechaFin, int medico)
        {
            return new DatHonorariosMedicos().EstadoCuenta(fechaIni, fechaFin, medico);
        }

        public static DataTable RecuperaHonorarios(int ATE_CODIGO)
        {
            return new DatHonorariosMedicos().RecuperaHonorarios(ATE_CODIGO);
        }
        /// <summary>
        /// Metodo que filtra la informacion de Honorarios medicos atraves de un stored procedure
        /// </summary>
        /// <param name="forCodigo">Codigo Forma de pago</param>
        /// <param name="fechaInicio">Fecha de Factura Inicial</param>
        /// <param name="fechaFin">Fecha de Factura Final</param>
        /// <param name="canceladas">(Bool) Si las facturas fueron canceladas</param>
        ///  <param name="sinRetencion">(Bool) Honorarios sin retencion</param>
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
        public static List<HONORARIOS_VISTA> RecuperarHonorariosMedicos(string tipo,string porRecuperar,string medCodigo, string espCodigo, string tihCodigo, string timCodigo, string medRecibeLlamada, string fechaIniFacturaMedico,
            string FechaFinFacturaMedico, string honorariosCancelados, string sinRetencion, string forCodigo,string tifCodigo, string lote, string numeroControl, string facturaClinica,
            string FechaIniFacturaCliente, string FechaFinFacturaCliente, string pacienteReferido, string pacienteFechaAlta, string ateCodigo, string pacCodigo)
        {
            return new DatHonorariosMedicos().RecuperarHonorariosMedicos(tipo,porRecuperar,medCodigo, espCodigo, tihCodigo, timCodigo, medRecibeLlamada, fechaIniFacturaMedico,
                    FechaFinFacturaMedico, honorariosCancelados, sinRetencion, forCodigo, tifCodigo,lote, numeroControl, facturaClinica, FechaIniFacturaCliente,
                    FechaFinFacturaCliente, pacienteReferido, pacienteFechaAlta,ateCodigo, pacCodigo);
        }
        /// <summary>
        /// Metodo que recupera la lista de Honorarios Pendientes de Cancelacion
        /// </summary>
        /// <returns>Retorna una lista de HONORARIOS_MEDICOS_TRANSFERENCIAS</returns>
        public static List<HONORARIOS_MEDICOS_TRANSFERENCIAS> RecuperarHonorariosMedicosPorTransferir()
        {
             return new DatHonorariosMedicos().RecuperarHonorariosMedicosPorTransferir() ;
        }
        /// <summary>
        /// Metodo que recupera la lista de Honorarios Pendientes de Cancelacion
        /// </summary>
        /// <param name="codigoMedico">codigo de Medico</param>
        /// <returns>Retorna una lista de HONORARIOS_MEDICOS_TRANSFERENCIAS</returns>
        public static List<HONORARIOS_MEDICOS_TRANSFERENCIAS> RecuperarHonorariosMedicosPorTransferir(string codigoMedico, string tipoFormaPagoId ,string formapPagoId, string fechaFacturaIni, string fechaFacturaFin)
        {
            return new DatHonorariosMedicos().RecuperarHonorariosMedicosPorTransferir(codigoMedico, tipoFormaPagoId, formapPagoId, fechaFacturaIni, fechaFacturaFin);
        }
        ////. Recupera lista de honorarios medicos por codigo del medico
        //public static List<HONORARIOS_MEDICOS> RecuperarHonorariosMedicos(int codigoMedico)
        //{
        //    return DatHonorariosMedicos.RecuperarHonorariosMedicos(codigoMedico);
        //}
        ////. Recupera lista de honorarios medicos por codigo de medico y codigo paciente
        //public static List<HONORARIOS_MEDICOS> RecuperarHonorariosMedicos(int codigoMedico, int codigoPaciente)
        //{
        //    return DatHonorariosMedicos.RecuperarHonorariosMedicos(codigoMedico, codigoPaciente);
        //}
        /// <summary>
        /// Actualiza en la base de datos los cambios en el objeto honorarios
        /// </summary>
        /// <param name="honorarios">Instancia de HONORARIOS_MEDICOS para actualizarce</param>
        public void ActualizarHonorariosMedicos(HONORARIOS_MEDICOS honorarios)
        {
            new DatHonorariosMedicos().ActualizarHonorariosMedicos(honorarios);  
        }

        public void ActualizarValoresHonorariosMedicos(HONORARIOS_MEDICOS honorarios)
        {
            new DatHonorariosMedicos().ActualizarValoresHonorariosMedicos(honorarios);
        }
        /// <summary>
        /// Devuelve una instancia de Honorarios Medicos
        /// </summary>
        /// <param name="codigo">codigo del honorario</param>
        /// <returns>objeto HONORARIOS_MEDICOS</returns>
        public HONORARIOS_MEDICOS RecuperaHonorariosMedicosID(Int64 codigo)
        {
            return new DatHonorariosMedicos().RecuperaHonorariosMedicosID(codigo);
        }
        /// <summary>
        /// recupera lista e honorarios medicos que no han sido ocupados
        /// </summary>
        /// <returns></returns>
        public static List<DtoHonorariosMedicos> ListaHonorariosNoutilizadosNotaDebito(string medCodigo)
        {
            return new DatHonorariosMedicos().ListaHonorariosNoutilizadosNotaDebito(medCodigo);
        }
        /// <summary>
        /// metodo para recuperar facturas sin retencion
        /// </summary>
        /// <returns></returns>
        public static List<DtoRetencionesAutomaticas> HonorariossinRetencion()
        {
            return new DatHonorariosMedicos().HonorariossinRetencion();
        }
        /// <summary>
        /// lista honorarios con pagos que no cubren la cantidad de la factura
        /// </summary>
        /// <returns></returns>
        public static List<DtoHonorariosNotasDebito> ListaHonorariosPagosMenores()
        {
            return new DatHonorariosMedicos().ListaHonorariosPagosMenores();
        }
        public static List<DtoHonorariosNotasDebito> ListaHonorariosSinNDComisionesAportes()
        {
            return new DatHonorariosMedicos().ListaHonorariosSinNDComisionesAportes();
        }

        public static bool VerificarExistenciaHonorario(String numFactura, Int32 codMedico, int codigoAtencion)
        {
            return new DatHonorariosMedicos().VerificarExistenciaHonorario(numFactura, codMedico, codigoAtencion);
        }

        public static bool honorariosGeneraronPagosRetenciones(Int32 codAtencion)
        {
            return new DatHonorariosMedicos().honorariosGeneraronPagosRetenciones(codAtencion);
        }

        public static HONORARIOS_MEDICOS HonorarioFacturaMedico(String numFactura, Int32 codMedico)
        {
            return new DatHonorariosMedicos().HonorarioFacturaMedico(numFactura, codMedico);
        }
        public static Int64 ultimoCodigoHonorarios()
        {
            return new DatHonorariosMedicos().ultimoCodigoHonorarios();
        }
        public static List<DtoHonorariosMedicos> listaHonorariosaAtencion(int codAtencion, int estado)
        {
            return new DatHonorariosMedicos().listaHonorariosAtencion(codAtencion,estado);
        }
        public static bool existeCabmael(Int64 codigo)
        {
            return new DatHonorariosMedicos().existeCabmael(codigo);
        }
        public static bool existeContabilidad(Int64 codigo)
        {
            return new DatHonorariosMedicos().existeContabilidad(codigo);
        }
        public static void cambiarEstadoHOMdatos(Int64 codigo)
        {
            new DatHonorariosMedicos().cambiarEstadoHOMdatos(codigo);
        }
        public static DataTable DatosRecuperaHonorarios(int Codigo_doc)
        {
            return new DatHonorariosMedicos().DatosRecuperaHonorarios(Codigo_doc);
        }

        public static DataTable RecuperaFichaMedico(int CodigoMedico)
        {
            return new DatHonorariosMedicos().RecuperaFichaMedico(CodigoMedico);
        }

        public static void saveHMDatosAdicionales(int HOMCODIGO, string FecCaducidad, int HonFuera, string autSRI, string caja, decimal cubierto, decimal exceso)
        {
            new DatHonorariosMedicos().saveHMDatosAdicionales( HOMCODIGO,  FecCaducidad,  HonFuera, autSRI, caja, cubierto, exceso);
        }
        public static void saveHMDatosAdicionales1(int HOMCODIGO, string FecCaducidad, int HonFuera, string autSRI, string caja, string numrec, decimal cubierto, decimal exceso)
        {
            new DatHonorariosMedicos().saveHMDatosAdicionales1(HOMCODIGO, FecCaducidad, HonFuera, autSRI, caja, numrec, cubierto, exceso);
        }
        public static void saveHMDatosAdicionales2(int HOMCODIGO, string FecCaducidad, int HonFuera, string autSRI, string caja, string numrec, decimal cubierto, decimal exceso)
        {
            new DatHonorariosMedicos().saveHMDatosAdicionales2(HOMCODIGO, FecCaducidad, HonFuera, autSRI, caja, numrec, cubierto, exceso);
        }
        public static DataTable HMDatosAdiciones(int hom_codigo)
        {
            return new DatHonorariosMedicos().HMDatosAdicionales(hom_codigo);
        }
        public static void deleteHMDatosAdicionales(int HOMCODIGO)
        {
            new DatHonorariosMedicos().deleteHMDatosAdicionales(HOMCODIGO);
        }
        public static DataTable getHMDatosAdicionales(int HOMCODIGO)
        {
            return new DatHonorariosMedicos().getHMDatosAdicionales(HOMCODIGO);
        }

        public static bool existHMDatosAdicionales(int HOMCODIGO)
        {
            return new DatHonorariosMedicos().existHMDatosAdicionales(HOMCODIGO);
        }
        public static bool DatosRecuperaFacturasMedicos(int codMedico, string factura)
        {
            return new DatHonorariosMedicos().DatosRecuperaFacturasMedicos(codMedico, factura);
        }
        public static DataTable FPHonorarios(int for_codigo)
        {
            return new DatHonorariosMedicos().FPHonorarios(for_codigo);
        }
        public static DataTable ConsultaAsiento(string facturaMedico, Int32 codMedico)
        {
            return new DatHonorariosMedicos().ConsultaAsiento(facturaMedico, codMedico);
        }

        public static DataTable ConsultaRetencion(Int64 numdoc)
        {
            return new DatHonorariosMedicos().ConsultaRetencion(numdoc);
        }
        public static bool EsOtraFormaPago(Int64 ate_codigo, int forcodigo, int med_codigo)
        {
            return new DatHonorariosMedicos().EsOtraFormaPago(ate_codigo, forcodigo, med_codigo);
        }
        public static bool FpAnticipo(Int64 ate_codigo, int med_codigo, string numrec)
        {
            return new DatHonorariosMedicos().FpAnticipo(ate_codigo, med_codigo, numrec);
        }
        public static bool FpTarjeta(Int64 ate_codigo, int med_codigo, string lote)
        {
            return new DatHonorariosMedicos().FpTarjeta(ate_codigo, med_codigo, lote);
        }
        public static void HonorarioAnticipoSic(int valido, double valorAnticipo, string numrec)
        {
            new DatHonorariosMedicos().HonorarioAnticipoSic(valido, valorAnticipo, numrec);
        }
        public static double ValorAnticipo(string numrec)
        {
            return new DatHonorariosMedicos().ValorAnticipo(numrec);
        }
        public static int HonorarioMedico(int hom_codigo)
        {
            return new DatHonorariosMedicos().HonorarioMedico(hom_codigo);
        }
        public static void ReponerAnticipoSic(string numrec, double monto)
        {
            new DatHonorariosMedicos().ReponerAnticipoSic(numrec, monto);
        }
        public static string RecuperarNUMREC(int hom_codigo)
        {
            return new DatHonorariosMedicos().RecuperarNUMREC(hom_codigo);
        }
        public static string NumVale()
        {
            return new DatHonorariosMedicos().NumVale();
        }
        public static void IncrementoVale(Int64 valor)
        {
            new DatHonorariosMedicos().IncrementaNumVale(valor.ToString());
        }
        public static DataTable DatosReporte(Int64 ate_codigo)
        {
            return new DatHonorariosMedicos().DatosReporte(ate_codigo);
        }
        public static DataTable HonoFormasPago()
        {
            return new DatHonorariosMedicos().HonoFormasPago();
        }
        public static DataTable Difiere(string claspag)
        {
            return new DatHonorariosMedicos().HonoDifierePago(claspag);
        }

        public static DataTable Agrupados(Int64 ate_codigo, string facturaMedico)
        {
            return new DatHonorariosMedicos().getAgrupados(ate_codigo, facturaMedico);
        }
        public static DataTable HMDentroPago(string numfac)
        {
            return new DatHonorariosMedicos().HMDentroPago(numfac);
        }
        public static DataTable ValidaCerrados(Int64 ate_codigo)
        {
            return new DatHonorariosMedicos().ValidaCerrar(ate_codigo);
        }
        public static void HonorariosCerrar(Int64 ate_codigo)
        {
            new DatHonorariosMedicos().HonorariosCerrar(ate_codigo);
        }
        public static void CambiarFactura(Int64 ate_codigo, string estado, string anterior, string nueva, string credito, int usuario, string observacion)
        {
            new DatHonorariosMedicos().CambioFactura(ate_codigo, estado, anterior, nueva, credito, usuario, observacion);
        }
        public static DataTable FacturasAnuladas()
        {
            return new DatHonorariosMedicos().FacturasAnuladas();
        }
        public static DataTable FacturaEstado(string numfac)
        {
            return new DatHonorariosMedicos().FacturaEstado(numfac);
        }
        public static DataTable FacturaValida(string numfac)
        {
            return new DatHonorariosMedicos().FacturaValida(numfac);
        }
        public static DataTable PacientesFac_Anuladas()
        {
            return new DatHonorariosMedicos().PacientesFac_Anulado();
        }
        public static DataTable ValidaFacturasAnuladas(string numfac)
        {
            return new DatHonorariosMedicos().ValidaFacturasAnulas(numfac);
        }
        public static DataTable FacturaAnuladaExiste(string numfac)
        {
            return new DatHonorariosMedicos().FacturaExisteAnulada(numfac);
        }
        public static DataTable FiltroPacientes(string nombre)
        {
            return new DatHonorariosMedicos().FiltroPacientes(nombre);
        }
        public static void CrearHonorarioAuditoria(HONORARIOS_MEDICOS HM, bool fuera, bool asiento, string estado, string caja, double cubierto, Int64 med_codigo)
        {
            new DatHonorariosMedicos().CreaHonorarioAuditoria(HM, fuera, asiento, estado, caja, cubierto, med_codigo);
        }
        public static string Porcentaje_MedicoAPC(Int64 med_codigo)
        {
            return new DatHonorariosMedicos().ValorApc_Medico(med_codigo);
        }
        public static bool AsientoGenerado(Int64 hom_codigo)
        {
            return new DatHonorariosMedicos().GeneradoAsiento(hom_codigo);
        }
        public static DataTable HonorarioIndividual(Int64 hom_codigo)
        {
            return new DatHonorariosMedicos().HonorarioIndividual(hom_codigo);
        }
        public static string GenerarAsientoContableHonorario(List<DtoCgDetmae> cgDetmaes, DateTime fecha)
        {
            return new DatHonorariosMedicos().GenerarAsientoContableHonorario(cgDetmaes, fecha);
        }
        public static DataTable ImpresionAsientos(Int64 hom_codigo, int parametro)
        {
            return new DatHonorariosMedicos().ReporteAsiento(hom_codigo, parametro);
        }

        public static DataTable ValidacionAD(Int64 hom_codigo, string codcli, string nocomp)
        {
            return new DatHonorariosMedicos().ValidacionAD(hom_codigo, codcli, nocomp);
        }

        public static double OcupoNumeroControl(DateTime fecha)
        {
            return new DatHonorariosMedicos().OcuparControlADS(fecha);
        }
    }
}
