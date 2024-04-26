using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.General; 


namespace His.Negocio
{
    public class NegHcEmergenciaExamenes
    {
        public static int RecuperaMaximoHcEmergenciaExamenes()
        {
            return new DatHcEmergenciaExamenes().RecuperaMaximoHcEmergenciaExamenes();
        }



        public static void crearHcEmergenciaExamenes(HC_EMERGENCIA_EXAMENES emergenciaExamenes)
        {
            new DatHcEmergenciaExamenes().CrearHcEmergenciaExamenes(emergenciaExamenes);
        }

        /// <summary>
        /// Retorna la lista de Exámenes según el número de Emergencia
        /// </summary>
        /// <returns></returns>

        public static List<HC_EMERGENCIA_EXAMENES> RecuperarHcEmergenciaExamenes(int codigoEmergencia)
        {
            return new DatHcEmergenciaExamenes().RecuperarHcEmergenciaExamenes(codigoEmergencia);
        }

        public static void actualizarHcExamen(HC_EMERGENCIA_EXAMENES examen)
        {
            new DatHcEmergenciaExamenes().ActualizarHcExamen(examen);
        }

        /// <summary>
        /// Eliminar Examen
        /// </summary>
        /// <param name="especialidad"></param>

        public static void EliminarHcEmergenciaExamen(HC_EMERGENCIA_EXAMENES examen)
        {
            new DatHcEmergenciaExamenes().EliminarHcEmergenciaExamenes(examen);
        }

    }
}
