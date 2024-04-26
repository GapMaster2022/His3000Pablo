using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using System.Data;
using His.Entidades;

namespace His.Negocio
{
    public class NegConsentimiento
    {
        public static HC_EXONERACION_RETIRO CargarDatosH2(Int64 ate_codigo)
        {
            return new DatHC_Consentimiento().CargarDatosH2(ate_codigo);
        }
        public static void GuardarConsentimientoH2(Int64 ate_codigo, string pdtestigo, string pdparentesco, string pdtelefono, string pdcedula, string abtestigo, string abparentesco, string abtelefono,
        string abcedula, string ahmedico, string ahtelefono, string ahcedula, string ahtTestigo, string ahtParentesco, string ahtTelefono, string ahtCedula, string memedido, string metelefono,
        string mecedula, string metTestigo, string metParentesco, string metTelefono, string metCedula, string odmedico, string odtelefono, string odcedula, string odtTestigo, string odtParentesco,
        string odtTelefono, string odtCedula, string anmedico, string antelefono, string ancedula, string antTestigo, string antParentesco, string antTelefono, string antCedula, string parentesco,
        string representante, string identificacion, string telefomo, string Organos, string Receptor)
        {
            new DatHC_Consentimiento().GuardarConsentimientoH2(ate_codigo, pdtestigo, pdparentesco, pdtelefono, pdcedula, abtestigo, abparentesco, abtelefono,
         abcedula, ahmedico, ahtelefono, ahcedula, ahtTestigo, ahtParentesco, ahtTelefono, ahtCedula, memedido, metelefono,
         mecedula, metTestigo, metParentesco, metTelefono, metCedula, odmedico, odtelefono, odcedula, odtTestigo, odtParentesco,
         odtTelefono, odtCedula, anmedico, antelefono, ancedula, antTestigo, antParentesco, antTelefono, antCedula, parentesco,
         representante, identificacion, telefomo, Organos, Receptor);
        }

        
        public static void GuardarConsentimiento(Int64 ate_codigo, string servicio, string sala, string proposito1,
            string resultado1, string procedimiento, string riesgo1, string proposito2, string resultado2,
            string quirurgico, string riesgo2, string proposito3, string resultado3, string anestesia,
            string riesgo3, string fecha, string hora, string tratante, string tespecialidad, string ttelefono,
            string tcodigo, string cirujano, string cespecialidad, string ctelefono, string ccodigo,
            string anestesista, string aespecialidad, string atelefono, string acodigo, string representante,
            string parentesco, string identificacion, string telefono)
        {
            new DatHC_Consentimiento().GuardarConsentimiento(ate_codigo, servicio, sala, proposito1, resultado1,
                procedimiento, riesgo1, proposito2, resultado2, quirurgico, riesgo2, proposito3, resultado3,
                anestesia, riesgo3, Convert.ToDateTime(fecha), Convert.ToDateTime(hora), tratante, tespecialidad, ttelefono, tcodigo, 
                cirujano, cespecialidad, ctelefono, ccodigo, anestesia, aespecialidad, atelefono, acodigo, representante,
                parentesco, identificacion, telefono);
        }

        public static DataTable CargarDatos(Int64 ate_codigo,Int64 CON_CODIGO)
        {
            return new DatHC_Consentimiento().CargarDatos(ate_codigo, CON_CODIGO);
        }
        public static bool guardaExoneracion(HC_EXONERACION_RETIRO exRet)
        {
            return new DatHC_Consentimiento().guardaExoneracion(exRet);
        }
        public static bool guardaConsentimiento(HC_CONSENTIMIENTO_INFORMADO conInf)
        {
            return new DatHC_Consentimiento().guardaConsentimiento(conInf);
        }
        public static bool actualizarExoneracion(Int64 ATE_CODIGO, HC_EXONERACION_RETIRO ret)
        {
            return new DatHC_Consentimiento().actualizarExoneracion(ATE_CODIGO, ret);
        }
        public static bool actualizarConsentimiento(Int64 ATE_CODIGO, HC_CONSENTIMIENTO_INFORMADO conInf)
        {
            return new DatHC_Consentimiento().actualizarConsentimiento(ATE_CODIGO, conInf);
        }
        public static DataTable getConsentimiento(Int64 ate_codigo)
        {
            return new DatHC_Consentimiento().getConsentimiento(ate_codigo);
        }
        public static Int32 ultimoRegistro()
        {
            return new DatHC_Consentimiento().ultimoRegistro();
        }
    }
}
