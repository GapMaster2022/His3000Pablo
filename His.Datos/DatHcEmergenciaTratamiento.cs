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
    public class DatHcEmergenciaTratamiento
    {
        public int RecuperaMaximoHcEmergenciaTratamiento()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from p in contexto.HC_EMERGENCIA_TRATAMIENTO
                             select p.TRA_CODIGO).ToList();
                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }

        }

        public void CrearHcEmergenciaTratamiento(HC_EMERGENCIA_TRATAMIENTO emergenciaTratamiento)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_EMERGENCIA_TRATAMIENTO(emergenciaTratamiento);
                contexto.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// Recupera la lista por enferm
        /// es Previas según Emergencia
        /// </summary>
        /// <param name="codigoTipoTratamiento"></param>
        /// <returns></returns>

        public List<HC_EMERGENCIA_TRATAMIENTO> RecuperarHcEmergenciaTratamientos(int codigoEmergencia)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.HC_EMERGENCIA_TRATAMIENTO.Where(c => c.TRA_EMERGENCIA == codigoEmergencia).ToList();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Eliminar Tratamiento
        /// </summary>
        /// <param name="enferPrevia"></param>
        public void EliminarHcEmergenciaTratamiento(HC_EMERGENCIA_TRATAMIENTO tratamiento)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA_TRATAMIENTO trat = contexto.HC_EMERGENCIA_TRATAMIENTO.Where(t => t.TRA_CODIGO == tratamiento.TRA_CODIGO).FirstOrDefault();
                    contexto.DeleteObject(trat);
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
