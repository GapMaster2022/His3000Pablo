using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegRetenciones
    {
        public static void CrearRetencion(RETENCIONES retencion)
        {
            new DatRetenciones().CrearRetencion(retencion);
        }
        public static void GrabarRetencion(RETENCIONES retencionModificada, RETENCIONES retencionOriginal)
        {
            new DatRetenciones().GrabarRetencion(retencionModificada, retencionOriginal);
        }
        public static List<DtoRetenciones> RecuperaRetenciones()
        {
            return new DatRetenciones().RecuperaRetenciones();
        }
    }
}
