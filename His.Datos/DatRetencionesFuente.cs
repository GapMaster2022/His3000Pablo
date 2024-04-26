using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatRetencionesFuente
    {
        public List<RETENCIONES_FUENTE> RecuperaRetencionesFuente()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.RETENCIONES_FUENTE.ToList();
            }
        }
        public Int16 RecuperaMaximoRetencionFuente()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<RETENCIONES_FUENTE> retencionfuente = contexto.RETENCIONES_FUENTE.ToList();
                if (retencionfuente.Count > 0)
                    maxim = contexto.RETENCIONES_FUENTE.Max(emp => emp.RET_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }
        }
        public void CrearRetencionFuente(RETENCIONES_FUENTE retencionfuente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("RETENCIONES_FUENTE", retencionfuente);

            }
        }
        public void GrabarRetencionFuente(RETENCIONES_FUENTE retencionfuenteModificada, RETENCIONES_FUENTE retencionfuenteOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(retencionfuenteModificada, retencionfuenteOriginal);
            }
        }
        public void EliminarRetencionFuente(RETENCIONES_FUENTE retencionfuente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(retencionfuente);
            }
        }

        public RETENCIONES_FUENTE recuperarPorId(int ret_codigo)
        {
            using(var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                RETENCIONES_FUENTE retencion = (from r in db.RETENCIONES_FUENTE
                                                where r.RET_CODIGO == ret_codigo
                                                select r).FirstOrDefault();
                return retencion;
            }
        }

    }
}
