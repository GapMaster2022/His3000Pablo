using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegAnamnesis
    {
        public static void crearAnamnesis(HC_ANAMNESIS nuevaAnamnesis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_ANAMNESIS(nuevaAnamnesis);
                contexto.SaveChanges();
            }
        }

        public static void actualizarAnamnesis(HC_ANAMNESIS anamnesis)
        {
            new DatAnamnesis().actualizarAnamnesis(anamnesis);
        }

        public static void crearMotivosConsulta(HC_ANAMNESIS_MOTIVOS_CONSULTA motivo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_ANAMNESIS_MOTIVOS_CONSULTA(motivo);
                contexto.SaveChanges();
            }
        }

        public static void crearAntecedentesMujer(HC_ANAMNESIS_ANTEC_MUJER motivo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_ANAMNESIS_ANTEC_MUJER(motivo);
                contexto.SaveChanges();
            }
        }
        public static int ultimoCodigoMotivoConsuta()
        {
            return new DatAnamnesis().ultimoCodigoMotivoConsuta();
        }

        public static int ultimoCodigoMujer()
        {
            return new DatAnamnesis().ultimoCodigoMujer();
        }

        public static int ultimoCodigo()
        {
            return new DatAnamnesis().ultimoCodigo();
        }

        public static HC_ANAMNESIS recuperarAnamnesisPorAtencion(int codAtencion)
        {
            return new DatAnamnesis().recuperarAnamnesisPorAtencion(codAtencion);
        }

        public static HC_ANAMNESIS_DIAGNOSTICOS recuperarAnamnesisDiagnostico(int CodigoAnamnesis)
        {
            return new DatAnamnesis().recuperarAnamnesisDiagnostico(CodigoAnamnesis);
        }

        /// <summary>
        /// Metodo que retorna los antecedentes de una mujer por codigo de Anamnesis
        /// </summary>
        /// <param name="codAnamnesis">Codigo de Anamnesis</param>
        /// <returns>Objeto tipo HC_ANAMNESIS_ANTEC_MUJER</returns>
        public static HC_ANAMNESIS_ANTEC_MUJER recuperarAnamnesisDatosMujer(int codAnamnesis)
        {
            try
            {
                return new DatAnamnesis().recuperarAnamnesisDatosMujer(codAnamnesis);
            }
            catch (Exception err) { throw err; }
        }

        public static void actualizarAnamnesisDatosMujer(HC_ANAMNESIS_ANTEC_MUJER anamnesisDatosM)
        { 
            new DatAnamnesis().actualizarAnamnesisDatosMujer(anamnesisDatosM);
        }

        public static void saveDA(DtoAnamnesis_DA x,string modo)
        {
            new DatAnamnesis().saveDA(x);
        }

        public static DtoAnamnesis_DA getDA(int codigoPaciente)
        {
            return new DatAnamnesis().getDA(codigoPaciente);
        }

    }
}
