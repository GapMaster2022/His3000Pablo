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
    public class DatHcEmergenciaCirugias
    {
        public int RecuperaMaximoHcEmergenciaCirugias()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from p in contexto.HC_EMERGENCIA_CIRUGIAS
                             select p.CIR_CODIGO).ToList();
                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }

        }

        public void CrearHcEmergenciaCirugias(HC_EMERGENCIA_CIRUGIAS emergenciaCirugias)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_EMERGENCIA_CIRUGIAS(emergenciaCirugias);
                contexto.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// Recupera la lista por Cirugias según Emergencia
        /// </summary>
        /// <param name="codigoTipoTratamiento"></param>
        /// <returns></returns>

        public List<HC_EMERGENCIA_CIRUGIAS> RecuperarHcEmergenciaCirugias(int codigoEmergencia)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.HC_EMERGENCIA_CIRUGIAS.Where(c => c.CIR_EMERGENCIA == codigoEmergencia).ToList();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        /// <summary>
        /// Elimina Cirugías
        /// </summary>
        /// <param name="enferPrevia"></param>
        public void EliminarHcEmergenciaCirugia(HC_EMERGENCIA_CIRUGIAS cirugia)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA_CIRUGIAS cir = contexto.HC_EMERGENCIA_CIRUGIAS.Where(c => c.CIR_CODIGO == cirugia.CIR_CODIGO).FirstOrDefault();
                    contexto.DeleteObject(cir);
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
