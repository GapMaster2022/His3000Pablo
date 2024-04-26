using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;

namespace His.Datos
{
    public class DatTipoNegocio
    {
        public Int16 RecuperaMaximoTipoNegocio()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<TIPO_NEGOCIO> tinegocio = contexto.TIPO_NEGOCIO.ToList();
                if (tinegocio.Count > 0)
                    maxim = contexto.TIPO_NEGOCIO.Max(emp => emp.CODTIPNEG);
                else
                    maxim = 0;
                return maxim;
            }
            
        }
        public List<TIPO_NEGOCIO> RecuperaTipoNegocios()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.TIPO_NEGOCIO.ToList();
            }
        }
        public void CrearTipoNegocio(TIPO_NEGOCIO tiponeg)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("TIPO_NEGOCIO", tiponeg);

            }
        }
        public void GrabarTipoNegocio(TIPO_NEGOCIO tiponegModificada, TIPO_NEGOCIO tiponegOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(tiponegModificada, tiponegOriginal);
            }
        }
        public void EliminarTipoNegocio(TIPO_NEGOCIO tiponeg)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(tiponeg);
            }
        }
    }
}
