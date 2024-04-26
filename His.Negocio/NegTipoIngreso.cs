using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegTipoIngreso
    {
        public static List<TIPO_INGRESO> ListaTipoIngreso()
        {
            return new DatTipoIngreso().ListaTipoIngreso();
        }

        public static List<TIPO_INGRESO> ListaTipoIngresoNombre(String Filtro)
        {
            return new DatTipoIngreso().ListaTipoIngresoNombre(Filtro);
        }

        public static TIPO_INGRESO FiltrarPorId(Int16 codigo)
        {
            return new DatTipoIngreso().TipoPorId(codigo);
        }
        public static int RecuperarporAtencion(Int64 ate_codigo)
        {
            return new DatTipoIngreso().RecuperarPorAtencion(ate_codigo);
        }
    }
}
