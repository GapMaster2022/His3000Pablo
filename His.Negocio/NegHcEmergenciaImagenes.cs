using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.General; 


namespace His.Negocio
{
    public class NegHcEmergenciaImagenes
    {
        public static int RecuperaMaximoHcEmergenciaImagenes()
        {
            return new DatHcEmergenciaImagen().RecuperaMaximoHcEmergenciaImagen();
        }

        public static void crearHcEmergenciaImagenes(HC_EMERGENCIA_IMAGENES emergenciaImagenes)
        {
            new DatHcEmergenciaImagen().CrearHcEmergenciaImagen(emergenciaImagenes);
        }

        /// <summary>
        /// Retorna la lista de Imágenes según el número de Emergencia
        /// </summary>
        /// <returns></returns>

        public static List<HC_EMERGENCIA_IMAGENES> RecuperarHcEmergenciaImagenes(int codigoEmergencia)
        {
            return new DatHcEmergenciaImagen().RecuperarHcEmergenciaImagenes(codigoEmergencia);
        }

        public static void actualizarHcImagen(HC_EMERGENCIA_IMAGENES imagen)
        {
            new DatHcEmergenciaImagen().ActualizarHcImagen(imagen);
        }

        /// <summary>
        /// Eliminar Imagen
        /// </summary>
        /// <param name="Imagen"></param>
        public static void EliminarHcEmergenciaImagen(HC_EMERGENCIA_IMAGENES imagen)
        {
            new DatHcEmergenciaImagen().EliminarHcEmergenciaImagenes(imagen);
        }

    }
}
