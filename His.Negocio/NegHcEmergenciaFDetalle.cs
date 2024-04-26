using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegHcEmergenciaFDetalle
    {
        public static void crearHcEmergenciaDetalle(HC_EMERGENCIA_FORM_EXAMENES detalle)
        {
            new DatHcEmergenciaFDetalle().crearHcEmergenciaDetalle(detalle);
        }

        public static List<HC_EMERGENCIA_FORM_EXAMENES> listaDetalleHcEmergencia(int codHcEmer, string codPadre)
        {
            return new DatHcEmergenciaFDetalle().listaDetalleHcEmergencia(codHcEmer,codPadre);
            
        }

        public static void actualizarDetalle(HC_EMERGENCIA_FORM_EXAMENES detalle)
        {
            new DatHcEmergenciaFDetalle().actualizarDetalle(detalle);
        }

        public static int ultimoCodigo()
        {
            return new DatHcEmergenciaFDetalle().ultimoCodigo();
        }
        
        #region EXAMEN FISICO Y DIAGNOSTICO

        public static void crearHcEmergenciaEFD(HC_EMERGENCIA_FORM_EXAMENFISICOD detalle)
        {
            new DatHcEmergenciaFDetalle().crearHcEmergenciaEFD(detalle);
        }

        public static List<HC_EMERGENCIA_FORM_EXAMENFISICOD> listaDetalleHcEmergenciaEFD(int codHcEmer)
        {
            return new DatHcEmergenciaFDetalle().listaEFDHcEmergencia(codHcEmer);
        }

        public static void actualizarEFD(HC_EMERGENCIA_FORM_EXAMENFISICOD detalle)
        {
            new DatHcEmergenciaFDetalle().actualizarEFD(detalle);
        }

        public static int ultimoCodigoEFD()
        {
            return new DatHcEmergenciaFDetalle().ultimoCodigoEFD();
        }

        public static void eliminarEFD(int codigoEFD)
        {
            new DatHcEmergenciaFDetalle().eliminarEFD(codigoEFD);
        }

        public static void eliminarEFisico(int codigoEFD)
        {
            new DatHcEmergenciaFDetalle().eliminarEFisico(codigoEFD);
        }

        #endregion


        #region DIAGNOSTICOS

        public static void crearHCEDiagnosticos(HC_EMERGENCIA_FORM_DIAGNOSTICOS nuevoDiagnostico)
        {
            new DatHcEmergenciaFDetalle().crearHCEDiagnosticos(nuevoDiagnostico);
        }

        public static void actualizarHcEmergenciaDiagnostico(HC_EMERGENCIA_FORM_DIAGNOSTICOS detalle)
        {
            new DatHcEmergenciaFDetalle().actualizarHcEmergenciaDiagnostico(detalle);
        }


        public static HC_EMERGENCIA_FORM_DIAGNOSTICOS buscarDiagnostico(string codigo)
        {
            return new DatHcEmergenciaFDetalle().buscarDiagnostico(codigo);
        }


        public static List<HC_EMERGENCIA_FORM_DIAGNOSTICOS> recuperarDiagnosticosHcEmergencia(int codHCEmergencia, string tipo)
        {
            return new DatHcEmergenciaFDetalle().recuperarDiagnosticosHcEmergencia(codHCEmergencia, tipo);
        }

        public static List<HC_EMERGENCIA_FORM_DIAGNOSTICOS> RecuperarDiagnosticos(Int64 codHCEmergencia)
        {
            return new DatHcEmergenciaFDetalle().RecuperarDiagnosticos(codHCEmergencia);
        }


        public static int ultimoCodigoADiagnostico()
        {
            return new DatHcEmergenciaFDetalle().ultimoCodigoADiagnostico();
        }

        public static void eliminarDiagnosticoDetalle(int codigoDiagnosticoDetalle)
        {
            new DatHcEmergenciaFDetalle().eliminarDiagnosticoDetalle(codigoDiagnosticoDetalle);
        }

        
        #endregion

        #region
        public static void crearHCEObstetrica(HC_EMERGENCIA_FORM_OBSTETRICA hcEmergencia)
        {
            new DatHcEmergenciaFDetalle().crearHCEObstetrica(hcEmergencia);
        }

        public static HC_EMERGENCIA_FORM_OBSTETRICA recuperarHCEObstetrica(int codHCEmergencia)
        {
            return new DatHcEmergenciaFDetalle().recuperarHCEObstetrica(codHCEmergencia);
        }


        public static void actualizarHCEObstetrica(HC_EMERGENCIA_FORM_OBSTETRICA hcEmergencia)
        {
            new DatHcEmergenciaFDetalle().actualizarHCEObstetrica(hcEmergencia);
        }


        public static int ultimoCodigoHCEObstetrica()
        {
            return new DatHcEmergenciaFDetalle().ultimoCodigoHCEObstetrica();
        }

        public static void eliminarHCEObstetrica(int codigoHCEObstetrica)
        {
            new DatHcEmergenciaFDetalle().eliminarHCEObstetrica(codigoHCEObstetrica);
        }
        #endregion

        #region PLAN DE TRATAMIENTO

        public static void crearTratamiento(HC_EMERGENCIA_FORM_TRATAMIENTO nuevoTratamiento)
        {
            new DatHcEmergenciaFDetalle().crearTratamiento(nuevoTratamiento);
        }

        public static void actualizarTratameinto(HC_EMERGENCIA_FORM_TRATAMIENTO nuevoTratamiento)
        {
            new DatHcEmergenciaFDetalle().actualizarTratameinto(nuevoTratamiento);
        }


        public static HC_EMERGENCIA_FORM_TRATAMIENTO buscarTratameinto(int codigo)
        {
            return new DatHcEmergenciaFDetalle().buscarTratameinto(codigo);
        }


        public static List<HC_EMERGENCIA_FORM_TRATAMIENTO> recuperarTratameinto(int codHCEmergencia, string tipo)
        {
            return new DatHcEmergenciaFDetalle().recuperarTratameinto(codHCEmergencia, tipo);
        }


        public static int ultimoCodigoTratameinto()
        {
            return new DatHcEmergenciaFDetalle().ultimoCodigoTratameinto();
        }

        public static void eliminarTratameinto(int codigoTratamiento)
        {
            new DatHcEmergenciaFDetalle().eliminarTratameinto(codigoTratamiento);
        }

        #endregion

    }
}
