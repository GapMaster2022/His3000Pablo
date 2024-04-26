using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.General; 

namespace His.Negocio
{
    public class NegHcEmergencia
    {
        public static int RecuperaMaximoHcEmergencia()
        {
            return new DatHcEmergencia().RecuperaMaximoHcEmergenciaCodigo();
        }

        public static void crearHcEmergencia(HC_EMERGENCIA emergencia)
        {
            new DatHcEmergencia().CrearHcEmergencia(emergencia);
        }

        /// <summary>
        /// Retorna la lista de Emergencias según el código de Paciente
        /// </summary>
        /// <returns></returns>

        public static List<HC_EMERGENCIA> RecuperarHcEmergencias(int codigoPaciente)
        {
            return new DatHcEmergencia().RecuperarHcEmergencias(codigoPaciente);
        }       
        
        public static HC_EMERGENCIA RecuperarUltimaEmergencia()
        {
            try
            {
                return new DatHcEmergencia().RecuperarUltimaEmergencia();
            }
            catch (Exception err)
            { throw err; }
        }
        public static HC_EMERGENCIA recuperaremergenciaPorAtencion(Int64 codAtencion)
        {
            try
            {
                return new DatHcEmergencia().recuperaremergenciaPorAtencion(codAtencion);
            }
            catch (Exception err)
            { throw err; }
        }
        public static HC_EMERGENCIA_FORM recuperaremergenciaFormPorAtencion(Int64 codAtencion)
        {
            try
            {
                return new DatHcEmergencia().recuperaremergenciaFormPorAtencion(codAtencion);
            }
            catch (Exception err)
            { throw err; }
        }
        public static HC_EMERGENCIA RecuperarUltimaEmergenciaPorPaciente(int codigoPaciente)
        {
            try
            {
                return new DatHcEmergencia().RecuperarUltimaEmergenciaPorPaciente(codigoPaciente);
            }
            catch (Exception err)
            { throw err; }
        }

        public static void actualizarHcEmergencia(HC_EMERGENCIA emergenciaNueva)
        {
            new DatHcEmergencia().ActualizarHcEmergencia(emergenciaNueva);
        }

        //EliminarHcEmergencia

        public static void eliminarHcEmergencia(HC_EMERGENCIA emergencia)
        {
            new DatHcEmergencia().EliminarHcEmergencia(emergencia);
        }

        public static void cerrarHcEmergencia(int codigoEmergencia)
        {
            new DatHcEmergencia().cerrarHcEmergencia(codigoEmergencia);
        }

        public static List<DtoHcEmergencias> RecuperarHcEmergenciasPorFechas(DateTime fechaInicio, DateTime fechaFinal)
        {
            return new DatHcEmergencia().RecuperarHcEmergenciasPorFechas(fechaInicio, fechaFinal);
        }

        public static int ActualizaDiagnostico(string Diagnostico, Int32 CodigoAtencion)
        {
            return new DatHcEmergencia().ActualizaDiagnostico(Diagnostico, CodigoAtencion);
        }   

        public static void ActualizaEstadoHoja08( Int32 CodigoAtencion)
        {
            new DatHcEmergencia().ActualizaEstadoHoja08( CodigoAtencion);
        }
        public static List<HC_EMERGENCIA_FORM_DIAGNOSTICOS> cieDiagnostico(Int64 ATE_CODIGO, string tipo)
        {
            return new DatHcEmergencia().cieDiagnostico(ATE_CODIGO, tipo);
        }
    }
}
