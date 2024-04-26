using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.General; 

namespace His.Negocio
{
    public class NegHcEmergenciaTratamiento
    {       
        public static int RecuperaMaximoHcEmergenciaTratamiento()
        {
            return new DatHcEmergenciaTratamiento().RecuperaMaximoHcEmergenciaTratamiento();
        }

        public static void crearHcEmergenciaTratamiento(HC_EMERGENCIA_TRATAMIENTO emergenciaTratamiento)
        {
            new DatHcEmergenciaTratamiento().CrearHcEmergenciaTratamiento(emergenciaTratamiento);
        }

        /// <summary>
        /// Retorna la lista de Tratamientos según el número de Emergencia
        /// </summary>
        /// <returns></returns>

        public static List<HC_EMERGENCIA_TRATAMIENTO> RecuperarHcEmergenciaTratamientos(int codigoEmergencia)
        {
            return new DatHcEmergenciaTratamiento().RecuperarHcEmergenciaTratamientos(codigoEmergencia);
        }

        /// <summary>
        /// Eliminar Tratamiento
        /// </summary>
        /// <param name="tratamiento"></param>
        public static void EliminarHcEmergenciaTratamiento(HC_EMERGENCIA_TRATAMIENTO tratamiento)
        {
            new DatHcEmergenciaTratamiento().EliminarHcEmergenciaTratamiento(tratamiento);
        }
    }
}
