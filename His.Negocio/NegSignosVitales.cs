using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegSignosVitales
    {
        public static HC_SIGNOS_VITALES CargarDatosSignosVitales(Int64 ate_codigo,Int32 dia)
        {
            return new DatHC_SignosVitales().CargarDatosSignosVitales(ate_codigo,dia);
        }
        public static List<HC_SIGNOS_VITALES> CargarImpresion(Int64 ate_codigo,Int32 SV_HOJA)
        {
            return new DatHC_SignosVitales().CargarImpresion(ate_codigo,SV_HOJA);
        }
        public static List<DtoExploSignosVitales> CargarSignosAtencion(Int64 ate_codigo)
        {
            return new DatHC_SignosVitales().CargarSignosAtencion(ate_codigo);
        }
        public static void GuardarSignosVitales(Int64 ate_codigo, Int32 SV_DIA, String SV_FECHA, string SV_INTERACCION, string SV_POSTQUIRURGICO,
            string SV_ING_PARENTAL, string SV_ING_ORAL, string SV_ING_TOTAL, string SV_ELM_ORINA, string SV_ELM_DRENAJE, string SV_ELM_OTROS,
            string SV_ELM_TOTAL, string SV_BAÑO, string SV_PESO, string SV_DIETA_ADMINISTRADA, string SV_NUMERO_COMIDAS, string SV_NUMERO_MEDICIONES, string SV_NUMERO_DEPOSICIONES,
            string SV_ACTIVIDAD_FISICA, string SV_CAMBIO_SONDA, string SV_RECANALIZACION, string SV_RESPONSABLE,string SV_PORCENTAJE, Int32 SV_HOJA, Int32 SV_RESPALDO_DIA)
        {
            new DatHC_SignosVitales().GuardarSignosVitales(ate_codigo, SV_DIA, Convert.ToDateTime(SV_FECHA), SV_INTERACCION, SV_POSTQUIRURGICO,
             SV_ING_PARENTAL, SV_ING_ORAL, SV_ING_TOTAL, SV_ELM_ORINA, SV_ELM_DRENAJE, SV_ELM_OTROS,
             SV_ELM_TOTAL, SV_BAÑO, SV_PESO, SV_DIETA_ADMINISTRADA, SV_NUMERO_COMIDAS, SV_NUMERO_MEDICIONES, SV_NUMERO_DEPOSICIONES,
             SV_ACTIVIDAD_FISICA, SV_CAMBIO_SONDA, SV_RECANALIZACION, SV_RESPONSABLE,SV_PORCENTAJE,SV_HOJA, SV_RESPALDO_DIA);
        }
        public static DataTable diaConsecutivo(Int64 ate_codigo)
        {
            return new DatHC_SignosVitales().diaConsecutivo(ate_codigo);
        }
        public static DataTable diaConsecutivoHoja(Int64 ate_codigo,Int64 hoja)
        {
            return new DatHC_SignosVitales().diaConsecutivoHoja(ate_codigo,hoja);
        }
        public static DataTable diaConsecutivoRespaldo(Int64 ate_codigo, string SV_RESPALDO_DIA)
        {
            return new DatHC_SignosVitales().diaConsecutivoRespaldo(ate_codigo, SV_RESPALDO_DIA);
        }
        public static DataTable getSignos(Int64 ate_codigo)
        {
            return new DatHC_SignosVitales().getSignos(ate_codigo);
        }
        public static bool GrabarSignosVitalesDat(HC_SIGNOS_DATOS_ADICIONALES svdatos)
        {
            return new DatHC_SignosVitales().GrabarSignosVitalesDat(svdatos);
        }
        public static bool GrabarSignosVitales(HC_SIGNOS_VITALES svitales)
        {
            return new DatHC_SignosVitales().GrabarSignosVitales(svitales);
        }
        public static Int32 ultimoRegistro()
        {
            return new DatHC_SignosVitales().ultimoRegistro();
        }
        public static bool EditarHojasignos(HC_SIGNOS_VITALES sv, Int64 SV_CODIGO)
        {
            return new DatHC_SignosVitales().EditarHojasignos(sv, SV_CODIGO);
        }
        public static bool EditarSignosVit(HC_SIGNOS_DATOS_ADICIONALES sv)
        {
            return new DatHC_SignosVitales().EditarSignosVit(sv);
        }
        public static HC_SIGNOS_DATOS_ADICIONALES CargarDatosSignosDatos(Int32 SVD_CODIGO)
        {
            return new DatHC_SignosVitales().CargarDatosSignosDatos(SVD_CODIGO);
        }
        public static DataTable ConsultaSignosXfecha(Int64 ate_codigo, DateTime fecha)
        {
            return new DatHC_SignosVitales().ConsultaSignosXfecha(ate_codigo,fecha);
        }
        public static List<HC_SIGNOS_DATOS_ADICIONALES> ValidaHora(Int64 ate_codigo, Int32 dia)
        {
            return new DatHC_SignosVitales().ValidaHora(ate_codigo, dia);
        }
        public static bool EditarDesdeIngestaEliminacion(HC_SIGNOS_VITALES sv, Int64 ATE_CODIGO, DateTime SV_FECHA)
        {
            return new DatHC_SignosVitales().EditarDesdeIngestaEliminacion(sv, ATE_CODIGO, SV_FECHA);
        }
        public static List<DtoExploSignosVitales> cargarSignosXatencion(Int64 ate_codigo)
        {
            return new DatHC_SignosVitales().cargarSignosXatencion(ate_codigo);
        }
        public static bool validadeSv(Int64 ate_codigo, DateTime SV_FECHA)
        {
            return new DatHC_SignosVitales().validadeSv(ate_codigo, SV_FECHA);
        }
        public static Int64 calculoDiasQuirurgico(Int64 ATE_CODIGO)
        {
            return new DatHC_SignosVitales().calculoDiasQuirurgico(ATE_CODIGO);
        }
        public static List<TIPO_DIETA> cargaDieta()
        {
            return new DatHC_SignosVitales().cargaDieta();
        }
        public static bool cargaSignosVitales(Int64 ATE_CODIGO, Int32 SV_HOJA)
        {
           return new DatHC_SignosVitales().cargaSignosVitales(ATE_CODIGO, SV_HOJA);
        }
        public static bool editarReporteSv()
        {
            return new DatHC_SignosVitales().editarReporteSv();
        }
        public static bool editarReporteCT()
        {
            return new DatHC_SignosVitales().editarReporteCT();
        }
        public static bool editarReporteCTF(int fila)
        {
            return new DatHC_SignosVitales().editarReporteCTF(fila);
        }
        public static bool cargaCurvaTermica(HC_SIGNOS_DATOS_ADICIONALES sv, Int64 contador)
        {
            return new DatHC_SignosVitales().cargaCurvaTermica(sv, contador);
        }
        public static List<HC_SIGNOS_DATOS_ADICIONALES> listaSVdatos(Int64 ATE_CODIGO)
        {
            return new DatHC_SignosVitales().listaSVdatos(ATE_CODIGO);
        }
    }
}
