using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegHcEmergenciaForm
    {

        public static int ultimoCodigo()
        {
            return new DatHcEmergenciaForm().ultimoCodigo();
        }

        public static void CrearHCEmergenciaF(HC_EMERGENCIA_FORM emergenciaF)
        {
            new DatHcEmergenciaForm().CrearHCEmergenciaF(emergenciaF);
        }

        public static void GuardarObservacionGeneral(int ate_codigo, string observacion)
        {
            new DatHcEmergenciaForm().GuardarObservacionGeneral(ate_codigo, observacion);
        }

        public static void ModificarHCEmergenciaF(HC_EMERGENCIA_FORM emergenciaF)
        {
            new DatHcEmergenciaForm().ModificarHCEmergenciaF(emergenciaF);
        }

        public static string CargarObservacion(int ate_codigo)
        {
            return new DatHcEmergenciaForm().CargarObservacionGeneral(ate_codigo);
        }
        public static void EliminarHCEmergenciaF(HC_EMERGENCIA_FORM emergenciaF)
        {
            new DatHcEmergenciaForm().EliminarHCEmergenciaF(emergenciaF);
        }

        public static List<HC_EMERGENCIA_FORM> listaHcEmergenciasF()
        {
            return new DatHcEmergenciaForm().listaHcEmergenciasF();

        }

        public static HC_EMERGENCIA_FORM RecuperarHcEmergenciasFId(int codEmergF)
        {
            return new DatHcEmergenciaForm().RecuperarHcEmergenciasFId(codEmergF);
        }

        public static HC_EMERGENCIA_FORM RecuperarHcEmergenciasFAten(Int64 codAtencion)
        {
            return new DatHcEmergenciaForm().RecuperarHcEmergenciasFAten(codAtencion);
        }

        public string Aseguradora(string ate_codigo)
        {
            DatHcEmergenciaForm emergencia = new DatHcEmergenciaForm();
            string seguro = emergencia.Aseguradora(Convert.ToInt64(ate_codigo));
            return seguro;
        }
        public static HC_EMERGENCIA_FORM RecuperarUltimaEmergencia()
        {
            try
            {
                return new DatHcEmergenciaForm().RecuperarUltimaEmergencia();
            }
            catch (Exception err)
            { throw err; }
        }
        public static OBSTETRICA_CONSULTAEXTERNA RecuperarEObstetrica(Int64 ate_codigo)
        {
            return new DatHcEmergenciaForm().RecuperarEObstetrica(ate_codigo);
        }

        public static GRUPO_SANGUINEO RecuperarTipoSangre(int codigo)
        {
            return new DatHcEmergenciaForm().RecuperarTipoSangre(codigo);
        }
        public static ATENCIONES RecuperarAtencionIDEmerg(int codigo)
        {
            return new DatAtenciones().RecuperarAtencionIDEmerg(codigo);
        }

        public static void cerrarHcEmergenciaForm(int codigoEmergencia)
        {
            new DatHcEmergenciaForm().cerrarHcEmergenciaForm(codigoEmergencia);
        }

        public void GuardarGrupoS(Int64 pac_codigo, int gs_codigo)
        {
            DatHcEmergenciaForm emergencia = new DatHcEmergenciaForm();
            emergencia.GuardarGrupoS(pac_codigo, gs_codigo);
        }
        public static void ModificarHCEmergenciaFNEW(HC_EMERGENCIA_FORM emergenciaF)
        {
            new DatHcEmergenciaForm().ModificarHCEmergenciaFNEW(emergenciaF);
        }
#region Metodos Catalagos


        #endregion
    }
}
