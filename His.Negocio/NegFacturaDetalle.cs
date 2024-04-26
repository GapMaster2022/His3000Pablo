using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.General; 

namespace His.Negocio
{
    public class NegFacturaDetalle
    {
        public static int RecuperaMaximoFacturaDetalle()
        {
            return new DatFacturaDetalle().RecuperaMaximoFacturaDetalle();
        }

        public static void crearFacturaDetalle(FACTURA_DETALLE facturaDetalle)
        {
            new DatFacturaDetalle().CrearFacturaDetalle(facturaDetalle);
        }

    }
}
