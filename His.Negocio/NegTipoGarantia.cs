using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegTipoGarantia
    {
        public static List<TIPO_GARANTIA> listaTipoGarantia()
        {
            return new DatTipoGarantias().listaTipoGarantia();
        }
    }
}
