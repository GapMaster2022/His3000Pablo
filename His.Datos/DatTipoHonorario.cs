using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;


namespace His.Datos
{
    public class DatTipoHonorario
    {
        public Int16 RecuperaMaximoTipoHonorario()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<TIPO_HONORARIO> tidocumento = contexto.TIPO_HONORARIO.ToList();
                if (tidocumento.Count > 0)
                    maxim = contexto.TIPO_HONORARIO.Max(emp => emp.TIH_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }
        }
        public List<TIPO_HONORARIO> RecuperaTipoHonorarios()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.TIPO_HONORARIO.ToList();
            }
        }
        public void CrearTipoHonorario(TIPO_HONORARIO tipohonorario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("TIPO_HONORARIO", tipohonorario);

            }
        }
        public void GrabarTipoHonorario(TIPO_HONORARIO tipohonorarioModificada, TIPO_HONORARIO tipohonorarioOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(tipohonorarioModificada, tipohonorarioOriginal);
            }
        }
        public void EliminarTipoHonorario(TIPO_HONORARIO tipohonorario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(tipohonorario);
            }
        }
    }
}
