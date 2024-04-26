using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using His.Entidades.General;

namespace His.Datos
{
    public class DatHcEmergenciaSubsintomas
    {
        public int RecuperaMaximoHcEmergenciaSubsintomas()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from p in contexto.HC_EMERGENCIA_SS
                             select p.SS_CODIGO).ToList();
                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }

        }

        public void CrearHcEmergenciaSubsintomas(HC_EMERGENCIA_SS emergenciaSubsintomas)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_EMERGENCIA_SS(emergenciaSubsintomas);
                contexto.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// Recupera la lista por Subsintomas según Emergencia
        /// </summary>
        /// <param name="codigoTipoTratamiento"></param>
        /// <returns></returns>

        public List<HC_EMERGENCIA_SS> RecuperarHcEmergenciaSubSintomas(int codigoEmergencia)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.HC_EMERGENCIA_SS.Where(c => c.SS_EMERGENCIA == codigoEmergencia).ToList();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Eliminar Subsintomas
        /// </summary>
        /// <param name="enferPrevia"></param>
        public void EliminarHcEmergenciaSubsintomas(HC_EMERGENCIA_SS subsintoma)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA_SS subs = contexto.HC_EMERGENCIA_SS.Where(s => s.SS_CODIGO == subsintoma.SS_CODIGO).FirstOrDefault();
                    contexto.DeleteObject(subs);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void ActualizarHcSubsintoma(HC_EMERGENCIA_SS subsintoma)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA_SS subs = contexto.HC_EMERGENCIA_SS.FirstOrDefault(hc => hc.SS_CODIGO == subsintoma.SS_CODIGO);
                    subs.SS_CATALOGO = subsintoma.SS_CATALOGO;
                    subs.SS_DESCRIPCION = subsintoma.SS_DESCRIPCION;                   
                    contexto.SaveChanges();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

    }
}
