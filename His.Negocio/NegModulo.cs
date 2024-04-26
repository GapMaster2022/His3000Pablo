using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegModulo
    {
        public static List<MODULO> RecuperaModulos()
        {
            return new DatModulo().RecuperaModulos();
        }
        public static List<DtoModulo> ListaModulo()
        {
            return new DatModulo().ListaModulo();
        }
        public static bool CrearModulo(MODULO modulo)
        {
            return new DatModulo().CrearModulo(modulo);
        }
        public static Int32 maxModulo()
        {
            return new DatModulo().maxModulo();
        }
        public static bool EditarModulo(Int64 id_modulo, string descripcion)
        {
            return new DatModulo().EditarModulo(id_modulo, descripcion);
        }
        public static bool EliminarModulo(Int32 id_modulo)
        {
            return new DatModulo().EliminarModulo(id_modulo);
        }
    }
}
