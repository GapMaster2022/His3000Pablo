using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegTipoNegocio
    {
        public static Int16 RecuperaMaximoTipoNegocio()
        {
            return new DatTipoNegocio().RecuperaMaximoTipoNegocio();
        }
        public static List<TIPO_NEGOCIO> RecuperaTipoNegocios()
        {
            return new DatTipoNegocio().RecuperaTipoNegocios();
        }
        public static void CrearTipoNegocio(TIPO_NEGOCIO tiponeg)
        {
            new DatTipoNegocio().CrearTipoNegocio(tiponeg);

        }
        public static void GrabarTipoNegocio(TIPO_NEGOCIO tiponegModificada, TIPO_NEGOCIO tiponegOriginal)
        {
            new DatTipoNegocio().GrabarTipoNegocio(tiponegModificada, tiponegOriginal);

        }
        public static void EliminarTipoNegocio(TIPO_NEGOCIO tiponeg)
        {
            new DatTipoNegocio().EliminarTipoNegocio(tiponeg);
        }
    }
}
