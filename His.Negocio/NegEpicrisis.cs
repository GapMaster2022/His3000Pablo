using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using System.Data;

namespace His.Negocio
{
    public class NegEpicrisis
    {
        public static void crearEpicrisis(HC_EPICRISIS nuevaEpicrisis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_EPICRISIS(nuevaEpicrisis);
                contexto.SaveChanges();
            }
        }

        public static int ultimoCodigo()
        {
            return new DatEpicrisis().ultimoCodigo();
        }

        public static int ultimoCodigoDiagnostico()
        {
            return new DatEpicrisis().ultimoCodigoDiagnostico();
        }

        public static HC_EPICRISIS recuperarEpicrisisPorAtencion(int codigoAtencion)
        {
            return new DatEpicrisis().recuperarEpicrisisPorAtencion(codigoAtencion);
        }

        public static DataTable RecuperaEvolucionAnalisis(Int64 Ate_Codigo)
        {
            return new DatEpicrisis().RecuperaEvolucionAnalisis(Ate_Codigo);
        }
        //public static HC_EPICRISIS_DIAGNOSTICO recuperarDiagnosticos(int codEpicrisis)
        //{
        //    return new DatEpicrisis().
        //}

        public static void crearEpicrisisDiagnosticos(HC_EPICRISIS_DIAGNOSTICO nuevoDiagnostico)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.AddToHC_EPICRISIS_DIAGNOSTICO(nuevoDiagnostico);
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public static void crearEpicrisisMedicos(HC_EPICRISIS_MEDICOS nuevoMedico)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_EPICRISIS_MEDICOS(nuevoMedico);
                contexto.SaveChanges();
            }
        }

        public static int ultimoCodigoMedico()
        {
            return new DatEpicrisis().ultimoCodigoMedico();
        }

        public static void ActualizarEpicrisis(HC_EPICRISIS epicrisis)
        {
            new DatEpicrisis().ActualizarEpicrisis(epicrisis);
        }

        public static void ActualizarDiagnostico(HC_EPICRISIS_DIAGNOSTICO diagnostico)
        {
            new DatEpicrisis().ActualizarDiagnosticos(diagnostico);
        }

        public static List<HC_EPICRISIS_DIAGNOSTICO> recuperarDiagnosticosEpicrisisIngreso(int codEpicrisis)
        {
            return new DatEpicrisis().recuperarDiagnosticosEpicrisisIngreso(codEpicrisis);
        }

        public static List<HC_EPICRISIS_DIAGNOSTICO> recuperarDiagnosticosEpicrisisEgreso(int codEpicrisis)
        {
            return new DatEpicrisis().recuperarDiagnosticosEpicrisisEgreso(codEpicrisis);
        }

        public static List<HC_EPICRISIS_MEDICOS> recuperarMedicosEpicrisis(int codEpicrisis)
        {
            return new DatEpicrisis().recuperarMedicosEpicrisis(codEpicrisis);
        }

        public static HC_EPICRISIS_MEDICOS recuperarMedicosEpicrisisCodigo(int codEpicrisis) // Recupera el codigo de la tabla HC_EPICRISIS_MEDICOS / Giovanny Tapia /18/09/2012
        {
            return new DatEpicrisis().recuperarMedicosEpicrisisCodigo(codEpicrisis);
        }

        public static void ActualizarMedicos(HC_EPICRISIS_MEDICOS medico)
        {
            new DatEpicrisis().ActualizarMedicos(medico);
        }

        public static void EliminarrMedicos(int codigoMedico)
        {
            new DatEpicrisis().EliminarMedicos(codigoMedico);
        }
    }
}
