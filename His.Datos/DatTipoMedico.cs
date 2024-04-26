using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatTipoMedico
    {
        public Int16 RecuperaMaximoTipoMedico()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<TIPO_MEDICO> tipomedico = contexto.TIPO_MEDICO.ToList();
                if (tipomedico.Count > 0)
                    maxim = contexto.TIPO_MEDICO.Max(emp => emp.TIM_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }

        }
        public List<TIPO_MEDICO> RecuperaTipoMedicos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.TIPO_MEDICO.ToList();
            }
        }
        public void CrearTipoMedico(TIPO_MEDICO tipomedico)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("TIPO_MEDICO", tipomedico);

            }
        }
        public void GrabarTipoMedico(TIPO_MEDICO tipomedicoModificada, TIPO_MEDICO tipomedicoOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(tipomedicoModificada, tipomedicoOriginal);
            }
        }
        public void EliminarTipoMedico(TIPO_MEDICO tipomedico)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(tipomedico);
            }
        }
    }
}
