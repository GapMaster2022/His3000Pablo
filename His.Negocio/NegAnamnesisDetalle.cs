using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegAnamnesisDetalle
    {
        public static void crearAnamnesisDetalle(HC_ANAMNESIS_DETALLE nuevaAnamnesis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_ANAMNESIS_DETALLE(nuevaAnamnesis);
                contexto.SaveChanges();
            }
        }

        public static void eliminarAnamnesisDetalle(int codigoDetalleAnamnesis, Int64 aneCodigo = 0)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                if (aneCodigo == 0)
                {
                    HC_ANAMNESIS_DETALLE anamDetalle = contexto.HC_ANAMNESIS_DETALLE.FirstOrDefault(h => h.ADE_CODIGO == codigoDetalleAnamnesis);
                    contexto.DeleteObject(anamDetalle);
                    contexto.SaveChanges();
                }
                else
                {
                    List<HC_ANAMNESIS_DETALLE> anamDetalle = (from hc in contexto.HC_ANAMNESIS_DETALLE
                                                              join a in contexto.HC_ANAMNESIS on hc.HC_ANAMNESIS.ANE_CODIGO equals a.ANE_CODIGO
                                                              where a.ANE_CODIGO == aneCodigo
                                                              select hc).ToList();
                    foreach (var item in anamDetalle)
                    {
                        contexto.DeleteObject(item);
                        contexto.SaveChanges();
                    }
                }
            }
        }

        public static bool eliminarDiagnosticoDetalle(string codigoDiagnosticoDetalle, int aneCod)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                if (codigoDiagnosticoDetalle != "0")
                {
                    HC_ANAMNESIS_DIAGNOSTICOS diagDetalle = (from h in contexto.HC_ANAMNESIS_DIAGNOSTICOS
                                                             where h.CIE_CODIGO == codigoDiagnosticoDetalle && h.HC_ANAMNESIS.ANE_CODIGO == aneCod
                                                             select h).FirstOrDefault();
                    if (diagDetalle == null)
                        return false;
                    contexto.DeleteObject(diagDetalle);
                    contexto.SaveChanges();
                }
                else
                {
                    List<HC_ANAMNESIS_DIAGNOSTICOS> diagDetalle = (from h in contexto.HC_ANAMNESIS_DIAGNOSTICOS
                                                                   where h.HC_ANAMNESIS.ANE_CODIGO == aneCod
                                                                   select h).ToList();
                    if (diagDetalle == null)
                        return false;
                    foreach (var item in diagDetalle)
                    {
                        contexto.DeleteObject(item);
                        contexto.SaveChanges();
                    }
                }
                return true;
            }
        }

        public static bool eliminarDiagnosticoDetalleEpi(string codigoDiagnosticoDetalle, int aneCod, string hedTipo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_EPICRISIS_DIAGNOSTICO diagDetalle = (from h in contexto.HC_EPICRISIS_DIAGNOSTICO
                                                        where h.CIE_CODIGO == codigoDiagnosticoDetalle && h.HC_EPICRISIS.EPI_CODIGO == aneCod && h.HED_TIPO == hedTipo
                                                        select h).FirstOrDefault();
                if (diagDetalle == null)
                    return false;
                contexto.DeleteObject(diagDetalle);
                contexto.SaveChanges();
                return true;
            }
        }

        public static bool eliminarTodosDiagnosticoDetalleEpi(int aneCod)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HC_EPICRISIS_DIAGNOSTICO> diagDetalle = (from h in contexto.HC_EPICRISIS_DIAGNOSTICO
                                                              where h.HC_EPICRISIS.EPI_CODIGO == aneCod
                                                              select h).ToList();
                if (diagDetalle == null)
                    return false;

                foreach (var item in diagDetalle)
                {
                    contexto.DeleteObject(item);
                    contexto.SaveChanges();
                }
                return true;
            }
        }

        public static void crearAnamnesisConsultas(HC_ANAMNESIS_MOTIVOS_CONSULTA nuevaConsulta)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_ANAMNESIS_MOTIVOS_CONSULTA(nuevaConsulta);
                contexto.SaveChanges();
            }
        }

        public static void crearAnamnesisDiagnosticos(HC_ANAMNESIS_DIAGNOSTICOS nuevoDiagnostico)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    int codigo;
                    if (contexto.HC_ANAMNESIS_DIAGNOSTICOS.Count() > 0)
                        codigo = contexto.HC_ANAMNESIS_DIAGNOSTICOS.Max(h => h.CDA_CODIGO) + 1;
                    else
                        codigo = 1;

                    nuevoDiagnostico.CDA_CODIGO = codigo;
                    contexto.AddToHC_ANAMNESIS_DIAGNOSTICOS(nuevoDiagnostico);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err) { throw err; }
        }

        public static void actualizarAnamnesisDiagnosticos(HC_ANAMNESIS_DIAGNOSTICOS detalle)
        {
            new DatAnamnesisDetalle().actualizarAnamnesisDiagnostico(detalle);
        }

        public static void actualizarMotivosConsulta(HC_ANAMNESIS_MOTIVOS_CONSULTA motivos)
        {
            new DatAnamnesisDetalle().actualizarMotivosConsulta(motivos);
        }

        public static List<HC_ANAMNESIS_DETALLE> listaDetallesAnamnesis(int codAnamnesis)
        {
            return new DatAnamnesisDetalle().listaDetallesAnamnesis(codAnamnesis);
        }

        public static List<OrganosSistemas> listaDetallesAnamnesisOrganos(int codAnamnesis)
        {
            return new DatAnamnesisDetalle().listaDetallesAnamnesisOrganos(codAnamnesis);
        }
        public static List<HC_ANAMNESIS_DETALLE> listaDetallesAnamnesisOrganos1(int codAnamnesis)
        {
            return new DatAnamnesisDetalle().listaDetallesAnamnesisOrganos1(codAnamnesis);
        }        
        public static List<HC_ANAMNESIS_DETALLE> listaDetallesAnamExamenFisico(int codAnamnesis)
        {
            return new DatAnamnesisDetalle().listaDetallesAnamExamenFisico(codAnamnesis);
        }
        public static List<HC_ANAMNESIS_DETALLE> listaDetallesAnamnesisAntPerso(int codAnamnesis)
        {
            return new DatAnamnesisDetalle().listaDetallesAnamnesisAntPerso(codAnamnesis);
        }        
        public static List<HC_ANAMNESIS_DETALLE> listaDetallesAnamnesisAntFam(int codAnamnesis)
        {
            return new DatAnamnesisDetalle().listaDetallesAnamnesisAntFam(codAnamnesis);
        }
        public static void actualizarDetalle(HC_ANAMNESIS_DETALLE detalle)
        {
            new DatAnamnesisDetalle().actualizarDetalle(detalle);
        }

        public static List<HC_ANAMNESIS_MOTIVOS_CONSULTA> listaMotivosConsulta(int codAnamnesis)
        {
            return new DatAnamnesisDetalle().listaMotivosConsulta(codAnamnesis);
        }

        public static HC_ANAMNESIS_DETALLE ultimaAnamnesis(int codAnamnesis)
        {
            return new DatAnamnesisDetalle().ultimaAnamnesis(codAnamnesis);
        }

        public static int ultimoCodigo()
        {
            return new DatAnamnesisDetalle().ultimoCodigo();
        }

        public static int ultimoCodigoADiagnostico()
        {
            return new DatAnamnesisDetalle().ultimoCodigoADiagnostico();
        }

        public static HC_ANAMNESIS_DIAGNOSTICOS buscarDiagnostico(string codigo)
        {
            return new DatAnamnesisDetalle().buscarDiagnostico(codigo);
        }

        public static List<HC_ANAMNESIS_MOTIVOS_CONSULTA> motivosConsultaPorId(int codAnamnesis)
        {
            return new DatAnamnesisDetalle().motivosConsultaPorId(codAnamnesis);
        }

        public static List<HC_ANAMNESIS_DIAGNOSTICOS> recuperarDiagnosticosAnamnesis(int codAnamnesis)
        {
            return new DatAnamnesisDetalle().recuperarDiagnosticosAnamnesis(codAnamnesis);
        }
        public static HC_ANAMNESIS_DIAGNOSTICOS recuperarDiagnosticosAnamnesisCodigo()
        {
            return new DatAnamnesisDetalle().recuperarDiagnosticosAnamnesisCodigo();
        }
        public static HC_ANAMNESIS_DETALLE rescataDetalleXcodigo(Int64 ade_codigo)
        {
            return new DatAnamnesisDetalle().rescataDetalleXcodigo(ade_codigo);
        }
    }
}
