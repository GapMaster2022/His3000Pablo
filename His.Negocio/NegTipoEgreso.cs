using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegTipoEgreso
    {
        public static List<TIPO_EGRESO> ListaTipoIngreso()
        {
            return new DatTipoEgreso().ListaTipoIngreso();
        }

        public static TIPO_EGRESO FiltrarPorId(Int32 codigo)
        {
            return new DatTipoEgreso().TipoPorId(codigo);
        }
    }
}
