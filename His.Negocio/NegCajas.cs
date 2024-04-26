using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegCajas
    {
        public static Int16 RecuperaMaximoCaja()
        {
            return new DatCajas().RecuperaMaximoCaja();
        }
        public static int recuperaFacturaInicial()
        {
            return new DatCajas().recuperaFacturaInicial();
        }
        public static List<CAJAS> ListaCajas()
        {
            return new DatCajas().ListaCajas();
        }
        public static List<DtoCajas> RecuperaCajas()
        {
            return new DatCajas().RecuperaCajas();
        }
        public static void CrearCaja(CAJAS caja)
        {
            new DatCajas().CrearCaja(caja);
        }
        public static void GrabarCaja(CAJAS cajaModificada, CAJAS cajaOriginal)
        {
            new DatCajas().GrabarCaja(cajaModificada, cajaOriginal);
        }
        public static void EliminarCaja(CAJAS caja)
        {
            new DatCajas().EliminarCaja(caja);
        }
        public static CAJAS RecuperarCajaID(Int16 codCaja)
        {
            return new DatCajas().RecuperarCajaID(codCaja);
        }
    }
}
