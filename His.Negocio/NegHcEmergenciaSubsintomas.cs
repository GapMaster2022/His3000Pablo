using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.General; 

namespace His.Negocio
{
    public class NegHcEmergenciaSubsintomas
    {
        public static int RecuperaMaximoHcEmergenciaSubsintomas()
        {
            return new DatHcEmergenciaSubsintomas().RecuperaMaximoHcEmergenciaSubsintomas();
        }



        public static void crearHcEmergenciaSubsintomas(HC_EMERGENCIA_SS emergenciaSubsintomas)
        {
            new DatHcEmergenciaSubsintomas().CrearHcEmergenciaSubsintomas(emergenciaSubsintomas);
        }

        /// <summary>
        /// Retorna la lista de Subsintomas según el número de Emergencia
        /// </summary>
        /// <returns></returns>

        public static List<HC_EMERGENCIA_SS> RecuperarHcEmergenciaSubSintomas(int codigoEmergencia)
        {
            return new DatHcEmergenciaSubsintomas().RecuperarHcEmergenciaSubSintomas(codigoEmergencia);
        }

        public static void actualizarHcSunsintoma(HC_EMERGENCIA_SS subsintoma)
        {
            new DatHcEmergenciaSubsintomas().ActualizarHcSubsintoma(subsintoma);
        }

        /// <summary>
        /// Eliminar SubSintoma
        /// </summary>
        /// <param name="SubSintoma"></param>
        public static void EliminarHcEmergenciaSubSintoma(HC_EMERGENCIA_SS subSintoma)
        {
            new DatHcEmergenciaSubsintomas().EliminarHcEmergenciaSubsintomas(subSintoma);
        }
    }
}
