using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatGeneracionesAutomaticasDetalle
    {
        public long RecuperaMaximoDetalle()
        {
            long maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<GENERACIONES_AUTOMATICAS_DETALLE> generacionesa = contexto.GENERACIONES_AUTOMATICAS_DETALLE.ToList();
                if (generacionesa.Count > 0)
                    maxim = contexto.GENERACIONES_AUTOMATICAS_DETALLE.Max(emp => emp.GAD_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }

        }
        public void CrearGeneracionAutomaticaDetalle(GENERACIONES_AUTOMATICAS_DETALLE generaAutomdet)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("GENERACIONES_AUTOMATICAS_DETALLE", generaAutomdet);
            }
        }
    }
}
