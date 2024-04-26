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
    public class DatHcEmergenciaEnfPrevias
    {
        public int RecuperaMaximoHcEmergenciaEnfPrevias()
        {
            try{
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var id = contexto.HC_EMERGENCIA_EPREVIAS.Select(h=>h.EFP_CODIGO).Count();
                    if (id > 0)
                        return contexto.HC_EMERGENCIA_EPREVIAS.Select(h => h.EFP_CODIGO).Max();
                    else
                        return 0;
                }
            }
            catch(Exception err){throw err;}

        }

        public void CrearHcEmergenciaEnfPrevias(HC_EMERGENCIA_EPREVIAS emergenciaPrevias)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_EMERGENCIA_EPREVIAS(emergenciaPrevias);
                contexto.SaveChanges();
            }
        }
        
        
        /// <summary>
        /// 
        /// Recupera la lista por enfermedades Previas según Emergencia
        /// </summary>
        /// <param name="codigoTipoTratamiento"></param>
        /// <returns></returns>

        public List<HC_EMERGENCIA_EPREVIAS> RecuperarHcEmergenciaEnfPrevias(int codigoEmergencia)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.HC_EMERGENCIA_EPREVIAS.Where(c => c.EFP_EMERGENCIA == codigoEmergencia).ToList();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        //public void EliminarHcEmergenciaEnfPrevias(HC_EMERGENCIA_EPREVIAS enferPrevia)
        //{
        //    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //    {
        //        contexto.DeleteObject(enferPrevia);
        //        contexto.SaveChanges();  
        //    }
        //}

        public void EliminarHcEmergenciaEnfPrevias(HC_EMERGENCIA_EPREVIAS enferPrevia)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA_EPREVIAS emergenciaPRevia = contexto.HC_EMERGENCIA_EPREVIAS.Where(e => e.EFP_CODIGO == enferPrevia.EFP_CODIGO).FirstOrDefault();    
                    contexto.DeleteObject(emergenciaPRevia);
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
