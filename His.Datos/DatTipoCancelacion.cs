using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatTipoCancelacion
    {
        /// <summary>
        /// Metodo que crea un nuevo Tipo de Cancelacion
        /// </summary>
        /// <param name="tipo">instancia que se añadira a la base de datos</param>
        /// <returns>estado de la creacion</returns>
        public bool CrearTipoCancelacion(TIPO_CANCELACION tipo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.AddToTIPO_CANCELACION(tipo);
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception err)
            {
                return false;
                throw err;
            }
        }
        /// <summary>
        /// Metodo que recupera la lista de Tipo de Cancelacion 
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public List<TIPO_CANCELACION> RecuperarListaTipoCancelacion()
        {

                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.TIPO_CANCELACION.ToList(); 
                }

        }
    }
}
