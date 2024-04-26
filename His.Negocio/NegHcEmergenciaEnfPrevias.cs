using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.General; 

namespace His.Negocio
{
    public class NegHcEmergenciaEnfPrevias
    {
        public static int RecuperaMaximoHcEmergenciaEnfPrevias()
        {
            return new DatHcEmergenciaEnfPrevias().RecuperaMaximoHcEmergenciaEnfPrevias();
        }

        /// <summary>
        /// Guarda una nueva Enfermedad Previa
        /// </summary>
        /// <param name="emergenciaPrevias"></param>


        public static void crearHcEmergenciaEnfPrevias(HC_EMERGENCIA_EPREVIAS emergenciaPrevias)
        {
            new DatHcEmergenciaEnfPrevias().CrearHcEmergenciaEnfPrevias(emergenciaPrevias);
        }
        /// <summary>
        /// Retorna la lista de Enfermedades Previas según el número de Emergencia
        /// </summary>
        /// <returns></returns>

        public static List<HC_EMERGENCIA_EPREVIAS> RecuperarHcEmergenciaEnfPrevias(int codigoEmergencia)
        {
            return new DatHcEmergenciaEnfPrevias().RecuperarHcEmergenciaEnfPrevias(codigoEmergencia);
        }
        /// <summary>
        /// Eliminar Enfermedades Previas
        /// </summary>
        /// <param name="especialidad"></param>

        public static void EliminarHcEmergenciaEnfPrevias(HC_EMERGENCIA_EPREVIAS enferPrevia)
        {
            new DatHcEmergenciaEnfPrevias().EliminarHcEmergenciaEnfPrevias(enferPrevia);
        }
    }
}
