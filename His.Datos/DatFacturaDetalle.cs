using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using His.Entidades.General; 

namespace His.Datos
{
    public class DatFacturaDetalle
    {
        //. Recupera el numero mayor del codigo de Detalle de Factura
        public int RecuperaMaximoFacturaDetalle()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var id = contexto.FACTURA_DETALLE.Select(d => d.COD_FDETALLE).Count();
                    if (id > 0)
                        return contexto.FACTURA_DETALLE.Select(d => d.COD_FDETALLE).Max();
                    else
                        return 0;
                }
            }
            catch (Exception err) { throw err; }

        }

        public void CrearFacturaDetalle(FACTURA_DETALLE facturaDetalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToFACTURA_DETALLE(facturaDetalle);
                contexto.SaveChanges();
            }
        }
    }
}
