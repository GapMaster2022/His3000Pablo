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
    public class DatHcEmergenciaReferido
    {
        public int RecuperaMaximoHcEmergenciaReferido()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var id = contexto.HC_EMERGENCIA_REFERIDOS.Select(h => h.REF_CODIGO).Count();
                    if (id > 0)
                        return contexto.HC_EMERGENCIA_REFERIDOS.Select(h => h.REF_CODIGO).Max();
                    else
                        return 0;
                }
            }
            catch (Exception err) { throw err; }

        }

        public void CrearHcEmergenciaReferido(HC_EMERGENCIA_REFERIDOS emergenciaReferido)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_EMERGENCIA_REFERIDOS(emergenciaReferido);
                contexto.SaveChanges();
            }
        }
        /// <summary>
        /// 
        /// Recupera la lista de Referencias según Emergencia
        /// </summary>
        /// <param name="codigoTipoTratamiento"></param>
        /// <returns></returns>

        public List<HC_EMERGENCIA_REFERIDOS> RecuperarHcEmergenciaReferencias(int codigoEmergencia)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.HC_EMERGENCIA_REFERIDOS.Where(c => c.REF_EMERGENCIA == codigoEmergencia).ToList();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void ActualizarHcReferido(HC_EMERGENCIA_REFERIDOS referido)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA_REFERIDOS refer = contexto.HC_EMERGENCIA_REFERIDOS.FirstOrDefault(r=> r.REF_CODIGO == referido.REF_CODIGO);
                    refer.REF_HC_CATALOGO = referido.REF_HC_CATALOGO;
                    refer.REF_COMUNICADO = referido.REF_COMUNICADO;
                    refer.REF_REALIZADO = referido.REF_REALIZADO;
                    refer.REF_OBSERVACION = referido.REF_OBSERVACION;
                    contexto.SaveChanges();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        /// <summary>
        /// Eliminar Referidos
        /// </summary>
        /// <param name="enferPrevia"></param>
        public void EliminarHcEmergenciaReferido(HC_EMERGENCIA_REFERIDOS referido)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA_REFERIDOS refer = contexto.HC_EMERGENCIA_REFERIDOS.Where(r => r.REF_CODIGO == referido.REF_CODIGO).FirstOrDefault();
                    contexto.DeleteObject(refer);
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
