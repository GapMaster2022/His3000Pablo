using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatCatalogosCostos
    {
        public Int16 RecuperaMaximoCcostos()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<CATALOGO_COSTOS> ccostos = contexto.CATALOGO_COSTOS.ToList();
                if (ccostos.Count > 0)
                    maxim = contexto.CATALOGO_COSTOS.Max(emp => emp.CAC_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }


        }
        public List<CATALOGO_COSTOS> RecuperaCcostos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.CATALOGO_COSTOS.Include("CATALOGO_COSTOS_TIPO").ToList();
            }
        }
        public void CrearCcostos(CATALOGO_COSTOS ccostos)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("CATALOGO_COSTOS", ccostos);

            }
        }
        public void GrabarCcostos(CATALOGO_COSTOS ccostosModificada, CATALOGO_COSTOS ccostosOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                CATALOGO_COSTOS catagolo = contexto.CATALOGO_COSTOS.FirstOrDefault(C => C.CAC_CODIGO == ccostosModificada.CAC_CODIGO);
                catagolo.CAC_NOMBRE = ccostosModificada.CAC_NOMBRE;   
                contexto.SaveChanges(); 
            }
        }
        public void EliminarCcostos(CATALOGO_COSTOS ccostos)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                CATALOGO_COSTOS catagolo = contexto.CATALOGO_COSTOS.FirstOrDefault(C => C.CAC_CODIGO == ccostos.CAC_CODIGO);
                contexto.DeleteObject(catagolo);
                contexto.SaveChanges();  
            }
        }
        public List<CATALOGO_COSTOS> ListaCcostos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                return (from cc in contexto.CATALOGO_COSTOS
                        join ct in contexto.CATALOGO_COSTOS_TIPO on cc.CATALOGO_COSTOS_TIPO.CCT_CODIGO equals ct.CCT_CODIGO
                        select cc).ToList(); 

            }
        }
        public List<CATALOGO_COSTOS> ListaCcostos(int codCctipo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from cc in contexto.CATALOGO_COSTOS 
                        join ct in contexto.CATALOGO_COSTOS_TIPO on cc.CATALOGO_COSTOS_TIPO.CCT_CODIGO equals ct.CCT_CODIGO 
                        where cc.CATALOGO_COSTOS_TIPO.CCT_CODIGO  == codCctipo
                        select cc).ToList();
            }
        }

        public List<CATALOGO_COSTOS_TIPO> RecuperarEstructuraCatalogos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.CATALOGO_COSTOS_TIPO.Include("CATALOGO_COSTOS").ToList();
            }
        }
    }
}
