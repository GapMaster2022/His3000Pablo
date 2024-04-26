using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.General; 

namespace His.Negocio
{
    public class NegHcEmergenciaComplicaciones
    {
        public static int RecuperaMaximoHcEmergenciaComplicaciones()
        {
            return new DatHcEmergenciaComplicaciones().RecuperaMaximoHcEmergenciaComplicaciones();
        }
        public static void crearHcEmergenciaComplicaciones(HC_EMERGENCIA_COMPLICACIONES emergenciaComplicaciones)
        {
            new DatHcEmergenciaComplicaciones().CrearHcEmergenciaComplicaciones(emergenciaComplicaciones);
        }

        /// <summary>
        /// Retorna la lista de Complicaciones según el número de Emergencia
        /// </summary>
        /// <returns></returns>

        public static List<HC_EMERGENCIA_COMPLICACIONES> RecuperarHcEmergenciaComplicaciones(int codigoEmergencia)
        {
            return new DatHcEmergenciaComplicaciones().RecuperarHcEmergenciaComplicaciones(codigoEmergencia);
        }

        /// <summary>
        /// Eliminar Complicaciones
        /// </summary>
        /// <param name="especialidad"></param>

        public static void EliminarHcEmergenciaComplicaciones(HC_EMERGENCIA_COMPLICACIONES complicacion)
        {
            new DatHcEmergenciaComplicaciones().EliminarHcEmergenciaComplicaciones(complicacion);
        }
    }
}
