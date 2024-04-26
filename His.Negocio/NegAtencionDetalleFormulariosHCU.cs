using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegAtencionDetalleFormulariosHCU
    {
        public static void Crear(ATENCION_DETALLE_FORMULARIOS_HCU nuevoDetalle)
        {
            new DatAtencionDetalleFormulariosHCU().Crear(nuevoDetalle);
        }

        public static List<ATENCION_DETALLE_FORMULARIOS_HCU> listaFormulariosAtencion(int codAtencion)
        {
            return new DatAtencionDetalleFormulariosHCU().listaFormulariosAtencion(codAtencion);
        }

        public static int MaxCodigo()
        {
            return new DatAtencionDetalleFormulariosHCU().maxCodigo();
        }

        /// <summary>
        /// Metodo que devuelve el detalle de los formularios pertenecientes a una atención
        /// </summary>
        /// <param name="codAtencion">Codigo de la atención</param>
        /// <returns>Retorna una lista de ATENCION_DETALLE_FORMULARIOS_HCU</returns>
        public static List<ATENCION_DETALLE_FORMULARIOS_HCU> listaAtencionDetalleFormularios(int codAtencion)
        {
            return new DatAtencionDetalleFormulariosHCU().listaAtencionDetalleFormularios(codAtencion);  
        }
    }
}
