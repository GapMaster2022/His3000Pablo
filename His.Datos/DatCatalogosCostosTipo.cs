using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatCatalogosCostosTipo
    {
        public Int16 RecuperaMaximoCctipo()
        {
                                               
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<CATALOGO_COSTOS_TIPO> cctipo = contexto.CATALOGO_COSTOS_TIPO.ToList();
                if (cctipo.Count > 0)
                    maxim = contexto.CATALOGO_COSTOS_TIPO.Max(emp => emp.CCT_CODIGO) ;
                else
                    maxim = 0;
                return maxim;
            }

        }
        public List<CATALOGO_COSTOS_TIPO> RecuperaCctipo()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.CATALOGO_COSTOS_TIPO.ToList();
            }
        }
        public void CrearCctipo(CATALOGO_COSTOS_TIPO cctipo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("CATALOGO_COSTOS_TIPO", cctipo);

            }
        }
        public void GrabarCctipo(CATALOGO_COSTOS_TIPO  CctipoModificada, CATALOGO_COSTOS_TIPO CctipoOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(CctipoModificada, CctipoOriginal);
            }
        }
        public void EliminarCctipo(CATALOGO_COSTOS_TIPO cctipo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(cctipo);
            }
        }
    }
}
