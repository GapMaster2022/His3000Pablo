using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.General; 

namespace His.Negocio
{
    public class NegHcEmergenciaCirugias
    {
        
        public static int RecuperaMaximoHcEmergenciaCirugias()
        {
            return new DatHcEmergenciaCirugias().RecuperaMaximoHcEmergenciaCirugias();
        }

        public static void crearHcEmergenciaCirugias(HC_EMERGENCIA_CIRUGIAS emergenciaCirugias)
        {
            new DatHcEmergenciaCirugias().CrearHcEmergenciaCirugias(emergenciaCirugias);
        }


        /// <summary>
        /// Retorna la lista de Cirugias según el número de Emergencia
        /// </summary>
        /// <returns></returns>

        public static List<HC_EMERGENCIA_CIRUGIAS> RecuperarHcEmergenciaCirugias(int codigoEmergencia)
        {
            return new DatHcEmergenciaCirugias().RecuperarHcEmergenciaCirugias(codigoEmergencia);
        }

        /// <summary>
        /// Eliminar Cirugía
        /// </summary>
        /// <param name="especialidad"></param>

        public static void EliminarHcEmergenciaCirugia(HC_EMERGENCIA_CIRUGIAS cirugia)
        {
            new DatHcEmergenciaCirugias().EliminarHcEmergenciaCirugia(cirugia);
        }
    }  
}
