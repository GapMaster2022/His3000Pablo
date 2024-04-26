using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using System.Windows.Forms;

namespace His.Datos
{
    public class DatAnamnesisDetalle
    {
        public void crearAnamnesisDetalle(HC_ANAMNESIS_DETALLE detalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_ANAMNESIS_DETALLE(detalle);
                contexto.SaveChanges();
            }
        }

        public List<HC_ANAMNESIS_DETALLE> listaDetallesAnamnesis(int codAnamnesis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.HC_ANAMNESIS_DETALLE.Where(n => n.HC_ANAMNESIS.ANE_CODIGO== codAnamnesis).ToList();
            }
        }
        public List<OrganosSistemas> listaDetallesAnamnesisOrganos(int codAnamnesis)
        {
            List<OrganosSistemas> org = new List<OrganosSistemas>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var obj = (from ad in contexto.HC_ANAMNESIS_DETALLE
                                                  join hc in contexto.HC_CATALOGOS on ad.HC_CATALOGOS.HCC_CODIGO equals hc.HCC_CODIGO
                                                  where hc.HC_CATALOGOS_TIPO.HCT_CODIGO == 4 && ad.HC_ANAMNESIS.ANE_CODIGO == codAnamnesis
                                                  select new { 
                                                  ad,hc                                                 
                                                  }).ToList();
                foreach (var item in obj)
                {
                    OrganosSistemas organos = new OrganosSistemas();
                    organos.codigoCell = item.ad.ADE_CODIGO;
                 
                    if (item.ad.ADE_TIPO =="CP")
                    {
                        organos.chk1 = true;
                        organos.chk2 = false;
                    }
                    else
                    {
                        organos.chk2 = true;
                        organos.chk1 = false;
                    }
                    organos.textcell = item.ad.ADE_DESCRIPCION;
                    org.Add(organos);
                }
                return org;
            }
        }
        public List<HC_ANAMNESIS_DETALLE> listaDetallesAnamnesisOrganos1(int codAnamnesis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var obj = (from ad in contexto.HC_ANAMNESIS_DETALLE
                           join hc in contexto.HC_CATALOGOS on ad.HC_CATALOGOS.HCC_CODIGO equals hc.HCC_CODIGO
                           where hc.HC_CATALOGOS_TIPO.HCT_CODIGO == 4 && ad.HC_ANAMNESIS.ANE_CODIGO == codAnamnesis
                           select ad).ToList();
                return obj;
            }
        }
        public List<HC_ANAMNESIS_DETALLE> listaDetallesAnamExamenFisico(int codAnamnesis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var obj = (from ad in contexto.HC_ANAMNESIS_DETALLE
                           join hc in contexto.HC_CATALOGOS on ad.HC_CATALOGOS.HCC_CODIGO equals hc.HCC_CODIGO
                           where hc.HC_CATALOGOS_TIPO.HCT_CODIGO == 5 && ad.HC_ANAMNESIS.ANE_CODIGO == codAnamnesis
                           select ad).ToList();
                return obj;
            }
        }
        public List<HC_ANAMNESIS_DETALLE> listaDetallesAnamnesisAntPerso(int codAnamnesis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var obj = (from ad in contexto.HC_ANAMNESIS_DETALLE
                           join hc in contexto.HC_CATALOGOS on ad.HC_CATALOGOS.HCC_CODIGO equals hc.HCC_CODIGO
                           where hc.HC_CATALOGOS_TIPO.HCT_CODIGO == 2 && ad.HC_ANAMNESIS.ANE_CODIGO == codAnamnesis
                           select ad).ToList();
                return obj;
            }
        }
        public List<HC_ANAMNESIS_DETALLE> listaDetallesAnamnesisAntFam(int codAnamnesis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var obj = (from ad in contexto.HC_ANAMNESIS_DETALLE
                           join hc in contexto.HC_CATALOGOS on ad.HC_CATALOGOS.HCC_CODIGO equals hc.HCC_CODIGO
                           where hc.HC_CATALOGOS_TIPO.HCT_CODIGO == 3 && ad.HC_ANAMNESIS.ANE_CODIGO == codAnamnesis
                           select ad).ToList();
                return obj;
            }
        }
        public void actualizarMotivosConsulta(HC_ANAMNESIS_MOTIVOS_CONSULTA motivos)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_ANAMNESIS_MOTIVOS_CONSULTA motivoDestino = contexto.HC_ANAMNESIS_MOTIVOS_CONSULTA.FirstOrDefault(m => m.MOC_CODIGO == motivos.MOC_CODIGO);
                motivoDestino.MOC_DESCRIPCION = motivos.MOC_DESCRIPCION;
                motivoDestino.HC_ANAMNESISReference.EntityKey = motivos.HC_ANAMNESISReference.EntityKey;
                contexto.SaveChanges();
            }
        }

        public void actualizarDetalle(HC_ANAMNESIS_DETALLE detalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_ANAMNESIS_DETALLE detalleDestino = contexto.HC_ANAMNESIS_DETALLE.FirstOrDefault(d => d.ADE_CODIGO == detalle.ADE_CODIGO);
                detalleDestino.ADE_DESCRIPCION = detalle.ADE_DESCRIPCION;
                detalleDestino.ADE_TIPO = detalle.ADE_TIPO;
                detalleDestino.HC_ANAMNESISReference.EntityKey = detalle.HC_ANAMNESISReference.EntityKey;
                detalleDestino.HC_CATALOGOSReference.EntityKey = detalle.HC_CATALOGOSReference.EntityKey;
                detalleDestino.ID_USUARIO = detalle.ID_USUARIO;
                contexto.SaveChanges();
                contexto.Connection.Close();
            }
        }

        public void actualizarAnamnesisDiagnostico(HC_ANAMNESIS_DIAGNOSTICOS detalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_ANAMNESIS_DIAGNOSTICOS detalleDestino = contexto.HC_ANAMNESIS_DIAGNOSTICOS.FirstOrDefault(d => d.CDA_CODIGO == detalle.CDA_CODIGO);
                detalleDestino.CIE_CODIGO = detalle.CIE_CODIGO;
                detalleDestino.CDA_ESTADO = detalle.CDA_ESTADO;
                detalleDestino.HC_ANAMNESISReference.EntityKey = detalle.HC_ANAMNESISReference.EntityKey;
                detalleDestino.CDA_DESCRIPCION = detalle.CDA_DESCRIPCION;
                detalleDestino.ID_USUARIO = detalle.ID_USUARIO;
                contexto.SaveChanges();
            }
        }

        public List<HC_ANAMNESIS_MOTIVOS_CONSULTA> listaMotivosConsulta(int codAnamnesis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.HC_ANAMNESIS_MOTIVOS_CONSULTA.Where(m => m.HC_ANAMNESIS.ANE_CODIGO == codAnamnesis).ToList();
            }
        }

        public HC_ANAMNESIS_DIAGNOSTICOS buscarDiagnostico(string codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //return contexto.HC_ANAMNESIS_DETALLE.Where(n => n.HC_ANAMNESIS.ANE_CODIGO == codAnamnesis).ToList();
                return (from n in contexto.HC_ANAMNESIS_DIAGNOSTICOS
                        where n.CIE_CODIGO == codigo
                        select n
                    ).FirstOrDefault();
            }
        }

        public HC_ANAMNESIS_DETALLE ultimaAnamnesis(int codAnamnesis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from n in contexto.HC_ANAMNESIS_DETALLE
                        join e in contexto.HC_ANAMNESIS on n.HC_ANAMNESIS.ANE_CODIGO equals e.ANE_CODIGO
                        where n.HC_ANAMNESIS.ANE_CODIGO == codAnamnesis
                        orderby n.ADE_CODIGO descending
                        select n).FirstOrDefault();
            }
        }

        public List<HC_ANAMNESIS_MOTIVOS_CONSULTA> motivosConsultaPorId(int codAnamnesis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from m in contexto.HC_ANAMNESIS_MOTIVOS_CONSULTA
                        where m.HC_ANAMNESIS.ANE_CODIGO == codAnamnesis
                        select m).ToList();
            }
        
            //using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            //{
            //    return (from n in contexto.HC_ANAMNESIS_MOTIVOS_CONSULTA
            //            join e in contexto.HC_ANAMNESIS on n.HC_ANAMNESIS.ANE_CODIGO equals e.ANE_CODIGO
            //            where n.HC_ANAMNESIS.ANE_CODIGO == codAnamnesis                        
            //            select n).ToList();
            //}
        }

        public List<HC_ANAMNESIS_DIAGNOSTICOS> recuperarDiagnosticosAnamnesis(int codAnamnesis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var diagnosticos = (from d in contexto.HC_ANAMNESIS_DIAGNOSTICOS
                                    where d.HC_ANAMNESIS.ANE_CODIGO == codAnamnesis
                                    select d).ToList();
                return diagnosticos;
            }
        }
        public HC_ANAMNESIS_DIAGNOSTICOS recuperarDiagnosticosAnamnesisCodigo()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var diagnosticos = (from d in contexto.HC_ANAMNESIS_DIAGNOSTICOS
                                    orderby d.CDA_CODIGO descending
                                    select (d)).FirstOrDefault();
                return diagnosticos;
            }
        }
        
        public int ultimoCodigo()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.HC_ANAMNESIS_DETALLE
                             select d.ADE_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }

        public int ultimoCodigoADiagnostico()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.HC_ANAMNESIS_DIAGNOSTICOS
                             select d.CDA_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }
        public HC_ANAMNESIS_DETALLE rescataDetalleXcodigo (Int64 ade_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return db.HC_ANAMNESIS_DETALLE.FirstOrDefault(x => x.ADE_CODIGO == ade_codigo);
            }
        }
    }
}
