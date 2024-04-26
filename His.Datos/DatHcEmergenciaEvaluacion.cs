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
    public class DatHcEmergenciaEvaluacion
    {
        public int RecuperaMaximoHcEmergenciaEvaluacion()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var id = contexto.HC_EMERGENCIA_EVALUACION.Select(h => h.INT_CODIGO).Count();
                    if (id > 0)
                        return contexto.HC_EMERGENCIA_EVALUACION.Select(h => h.INT_CODIGO).Max();
                    else
                        return 0;
                }
            }
            catch (Exception err) { throw err; }

        }

        public void CrearHcEmergenciaEvaluacion(HC_EMERGENCIA_EVALUACION emergenciaEvaluacion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_EMERGENCIA_EVALUACION(emergenciaEvaluacion);
                contexto.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// Recupera la lista de Evaluación según Emergencia
        /// </summary>
        /// <param name="codigoTipoTratamiento"></param>
        /// <returns></returns>

        public List<HC_EMERGENCIA_EVALUACION> RecuperarHcEmergenciaEvaluacion(int codigoEmergencia)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.HC_EMERGENCIA_EVALUACION.Where(c => c.INT_EMERGENCIA == codigoEmergencia).ToList();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void ActualizarHcEvaluacion(HC_EMERGENCIA_EVALUACION evaluacion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA_EVALUACION eval = contexto.HC_EMERGENCIA_EVALUACION.FirstOrDefault(e => e.INT_CODIGO == evaluacion.INT_CODIGO);
                    eval.INT_HC_CATALOGOS = evaluacion.INT_HC_CATALOGOS;
                    eval.INT_COMUNICADO = evaluacion.INT_COMUNICADO;
                    eval.INT_REALIZADO = evaluacion.INT_REALIZADO;
                    eval.INT_OBSERVACIONES = evaluacion.INT_OBSERVACIONES;
                    contexto.SaveChanges();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Eliminar Evaluación
        /// </summary>
        /// <param name="enferPrevia"></param>
        public void EliminarHcEmergenciaEvaluacion(HC_EMERGENCIA_EVALUACION evaluacion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA_EVALUACION compl = contexto.HC_EMERGENCIA_EVALUACION.Where(e => e.INT_CODIGO == evaluacion.INT_CODIGO).FirstOrDefault();
                    contexto.DeleteObject(compl);
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
