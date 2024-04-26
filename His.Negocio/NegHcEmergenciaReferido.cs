using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.General;

namespace His.Negocio
{
    public class NegHcEmergenciaReferido
    {
        public static int RecuperaMaximoHcEmergenciaReferido()
        {
            return new DatHcEmergenciaReferido().RecuperaMaximoHcEmergenciaReferido();
        }

        public static void crearHcEmergenciaReferido(HC_EMERGENCIA_REFERIDOS emergenciaReferido)
        {
            new DatHcEmergenciaReferido().CrearHcEmergenciaReferido(emergenciaReferido);
        }

        /// <summary>
        /// Retorna la lista de Rferencias según el número de Emergencia
        /// </summary>
        /// <returns></returns>

        public static List<HC_EMERGENCIA_REFERIDOS> RecuperarHcEmergenciaReferencias(int codigoEmergencia)
        {
            return new DatHcEmergenciaReferido().RecuperarHcEmergenciaReferencias(codigoEmergencia);
        }

        public static void actualizarHcReferido(HC_EMERGENCIA_REFERIDOS referido)
        {
            new DatHcEmergenciaReferido().ActualizarHcReferido(referido);
        }

       /// <summary>
       /// Eliminar Referido
       /// </summary>
       /// <param name="referido"></param>
        public static void EliminarHcEmergenciaReferido(HC_EMERGENCIA_REFERIDOS referido)
        {
            new DatHcEmergenciaReferido().EliminarHcEmergenciaReferido(referido);
        }
    }
}
