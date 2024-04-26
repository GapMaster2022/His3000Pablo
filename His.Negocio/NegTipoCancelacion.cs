using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;
using His.Parametros; 
namespace His.Negocio
{
    public class NegTipoCancelacion
    {
        /// <summary>
        /// Metodo que crea un nuevo Tipo de Cancelacion
        /// </summary>
        /// <param name="tipo">instancia que se añadira a la base de datos</param>
        /// <returns>estado de la creacion</returns>
        public static bool CrearTipoCancelacion(TIPO_CANCELACION tipo)
        {
            try
            {
                new DatTipoCancelacion().CrearTipoCancelacion(tipo);
                return true;
            }
            catch (Exception err)
            {
                return false;
                throw err;
            }
        }

        public static List<TIPO_CANCELACION> RecuperarListaTipoCancelacion()
        {
            return new DatTipoCancelacion().RecuperarListaTipoCancelacion();  

        }
    }
}
