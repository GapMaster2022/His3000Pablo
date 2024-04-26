using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.General;

namespace His.Negocio
{
    public class NegHcEmergenciaEvaluacion
    {
        public static int RecuperaMaximoHcEmergenciaEvaluacion()
        {
            return new DatHcEmergenciaEvaluacion().RecuperaMaximoHcEmergenciaEvaluacion();
        }

        public static void crearHcEmergenciaEvaluacion(HC_EMERGENCIA_EVALUACION emergenciaEvaluacion)
        {
            new DatHcEmergenciaEvaluacion().CrearHcEmergenciaEvaluacion(emergenciaEvaluacion);
        }

        /// <summary>
        /// Retorna la lista de Evaluaciones según el número de Emergencia
        /// </summary>
        /// <returns></returns>

        public static List<HC_EMERGENCIA_EVALUACION> RecuperarHcEmergenciaEvaluacion(int codigoEmergencia)
        {
            return new DatHcEmergenciaEvaluacion().RecuperarHcEmergenciaEvaluacion(codigoEmergencia);
        }

        public static void actualizarHcEvaluacion(HC_EMERGENCIA_EVALUACION evaluacion)
        {
            new DatHcEmergenciaEvaluacion().ActualizarHcEvaluacion(evaluacion);
        }

        /// <summary>
        /// Eliminar Evaluación
        /// </summary>
        /// <param name="especialidad"></param>

        public static void EliminarHcEmergenciaEvaluacion(HC_EMERGENCIA_EVALUACION evaluacion)
        {
            new DatHcEmergenciaEvaluacion().EliminarHcEmergenciaEvaluacion(evaluacion);
        }

    }
}
