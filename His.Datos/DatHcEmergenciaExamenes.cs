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
    public class DatHcEmergenciaExamenes
    {
        public int RecuperaMaximoHcEmergenciaExamenes()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var id = contexto.HC_EMERGENCIA_EXAMENES.Select(h => h.EXA_CODIGO).Count();
                    if (id > 0)
                        return contexto.HC_EMERGENCIA_EXAMENES.Select(h => h.EXA_CODIGO).Max();
                    else
                        return 0;
                }
            }
            catch (Exception err) { throw err; }

        }

        public void CrearHcEmergenciaExamenes(HC_EMERGENCIA_EXAMENES emergenciaExamenes)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_EMERGENCIA_EXAMENES(emergenciaExamenes);
                contexto.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// Recupera la lista de Exámenes según Emergencia
        /// </summary>
        /// <param name="codigoTipoTratamiento"></param>
        /// <returns></returns>

        public List<HC_EMERGENCIA_EXAMENES> RecuperarHcEmergenciaExamenes(int codigoEmergencia)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.HC_EMERGENCIA_EXAMENES.Where(c => c.EXA_EMERGENCIA == codigoEmergencia).ToList();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Eliminar Exámenes
        /// </summary>
        /// <param name="enferPrevia"></param>
        public void EliminarHcEmergenciaExamenes(HC_EMERGENCIA_EXAMENES examenes)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA_EXAMENES compl = contexto.HC_EMERGENCIA_EXAMENES.Where(e => e.EXA_CODIGO == examenes.EXA_CODIGO).FirstOrDefault();
                    contexto.DeleteObject(compl);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void ActualizarHcExamen(HC_EMERGENCIA_EXAMENES examen)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA_EXAMENES exam = contexto.HC_EMERGENCIA_EXAMENES.FirstOrDefault(e => e.EXA_CODIGO == examen.EXA_CODIGO);
                    exam.EXA_HC_CATALOGOS = examen.EXA_HC_CATALOGOS;
                    exam.EXA_COMUNICADO = examen.EXA_COMUNICADO;
                    exam.EXA_RESULTADO = examen.EXA_RESULTADO;
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
