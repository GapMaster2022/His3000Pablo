using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegRetencionesFuente
    {
        public static List<RETENCIONES_FUENTE> RecuperaRetencionesFuente()
        {
            return new DatRetencionesFuente().RecuperaRetencionesFuente();
        }
        public static Int16 RecuperaMaximoRetencionFuente()
        {
            return new DatRetencionesFuente().RecuperaMaximoRetencionFuente();
        }
        public static void CrearRetencionFuente(RETENCIONES_FUENTE retencionfuente)
        {
            new DatRetencionesFuente().CrearRetencionFuente(retencionfuente);
        }
        public static void GrabarRetencionFuente(RETENCIONES_FUENTE retencionfuenteModificada, RETENCIONES_FUENTE retencionfuenteOriginal)
        {
            new DatRetencionesFuente().GrabarRetencionFuente(retencionfuenteModificada, retencionfuenteOriginal);
        }
        public static void EliminarRetencionFuente(RETENCIONES_FUENTE retencionfuente)
        {
            new DatRetencionesFuente().EliminarRetencionFuente(retencionfuente);
        }
        public static RETENCIONES_FUENTE recuperarPorId(int ret_codigo)
        {
            return new DatRetencionesFuente().recuperarPorId(ret_codigo);
        }
    }
}
