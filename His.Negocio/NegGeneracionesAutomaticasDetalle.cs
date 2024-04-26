using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegGeneracionesAutomaticasDetalle
    {
        public static long RecuperaMaximoDetalle()
        {
            return new DatGeneracionesAutomaticasDetalle().RecuperaMaximoDetalle();
        }
        public static void CrearGeneracionAutomaticaDetalle(GENERACIONES_AUTOMATICAS_DETALLE generaAutomdet)
        {
            new DatGeneracionesAutomaticasDetalle().CrearGeneracionAutomaticaDetalle(generaAutomdet);
        }
    }
}
