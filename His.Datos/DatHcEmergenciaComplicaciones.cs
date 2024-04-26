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
    public class DatHcEmergenciaComplicaciones
    {
        public int RecuperaMaximoHcEmergenciaComplicaciones()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from p in contexto.HC_EMERGENCIA_COMPLICACIONES
                             select p.COM_CODIGO).ToList();
                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }

        }

        public void CrearHcEmergenciaComplicaciones(HC_EMERGENCIA_COMPLICACIONES emergenciaComplicaciones)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_EMERGENCIA_COMPLICACIONES(emergenciaComplicaciones);
                contexto.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// Recupera la lista por Complicaciones según Emergencia
        /// </summary>
        /// <param name="codigoTipoTratamiento"></param>
        /// <returns></returns>

        public List<HC_EMERGENCIA_COMPLICACIONES> RecuperarHcEmergenciaComplicaciones(int codigoEmergencia)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.HC_EMERGENCIA_COMPLICACIONES.Where(c => c.COM_EMERGENCIA == codigoEmergencia).ToList();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        /// <summary>
        /// Eliminar Complicaciones
        /// </summary>
        /// <param name="enferPrevia"></param>
        public void EliminarHcEmergenciaComplicaciones(HC_EMERGENCIA_COMPLICACIONES complicacion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA_COMPLICACIONES compl = contexto.HC_EMERGENCIA_COMPLICACIONES.Where(c => c.COM_CODIGO == complicacion.COM_CODIGO).FirstOrDefault();
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
