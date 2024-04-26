using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegPagosFacturasMedicos
    {
        public static List<PAGOS_FAC_MEDICOS_VIEW> RecuperarPagosFacturasMedicos()
        {
            return new DatPagosFacturasMedicos().RecuperarPagosFacturasMedicos();
        }

        /// <summary>
        /// Metodo que devuelve una lista de la cartera recuperada
        /// </summary>
        /// <param name="fecDocIni">Fecha inicial del documento</param>
        /// <param name="fecDocFin">Fecha final del documento</param>
        /// <param name="codTipoFormaPago">Codigo del tipo de forma de pago</param>
        /// <param name="codFormaPago">Codigo de la Forma de pago</param>
        /// <returns>lista de PAGOS_FAC_MEDICOS_VIEW</returns>
        public static List<PAGOS_FAC_MEDICOS_VIEW> RecuperarPagosFacturasMedicos(string fecDocIni, string fecDocFin , string codTipoFormaPago,string codFormaPago)
        {
            return new DatPagosFacturasMedicos().RecuperarPagosFacturasMedicos(fecDocIni, fecDocFin, codTipoFormaPago, codFormaPago);
        }
        /// <summary>
        /// Crear un nueva cancelacion de las facturas de medicos
        /// </summary>
        /// <param name="pagoFacturasMedicos">pago facturas de medicos</param>
        public static PAGOS_FAC_MEDICOS CrearPagoFacturasMedicos(PAGOS_FAC_MEDICOS pago)
        {
           return  new DatPagosFacturasMedicos().CrearPagoFacturasMedicos(pago); 
        }
        /// <summary>
        /// Crear un nuevo detalle de la cancelacion de facturas medicas
        /// </summary>
        /// <param name="pagoFacturasMedicosDetalle">detalle pago facturas medicos</param>
        public static void CrearPagoFacturasMedicosDetalle(PAGOS_FAC_MEDICOS_DETALLE detalle)
        {
            new DatPagosFacturasMedicos().CrearPagoFacturasMedicosDetalle(detalle);  
        }
        /// <summary>
        /// Devuelve una instancia de PAGOS_FAC_MEDICOS
        /// </summary>
        /// <param name="codigo">codigo del pago de factura medico</param>
        /// <returns>Instancia de PAGOS_FAC_MEDICOS</returns>
        public static PAGOS_FAC_MEDICOS RecuperaPagosFacturaMedicoID(Int64 codigo)
        {
            return new DatPagosFacturasMedicos().RecuperaPagosFacturaMedicoID(codigo);
        }
        /// <summary>
        /// Recupera el Detalle de un pago especifico
        /// </summary>
        /// <param name="codigoPago">codigo del pago de la factura del medico</param>
        /// <returns>lista de PAGOS_FAC_MEDICOS_DETALLE de un determinado pago </returns>
        public static List<PAGOS_FAC_MEDICOS_DETALLE_VIEW> RecuperarPagosFacturasMedicosDetalle(Int64 codigoPago)
        {
            return new DatPagosFacturasMedicos().RecuperarPagosFacturasMedicosDetalle(codigoPago); 
        }
        /// <summary>
        /// Devuelve una instancia de PAGOS_FAC_MEDICOS_DETALLE
        /// </summary>
        /// <param name="codigo"> codigo del detalle del pago de factura medico</param>
        /// <returns>Instancia de PAGOS_FAC_MEDICOS_DETALLE</returns>
        public static PAGOS_FAC_MEDICOS_DETALLE RecuperaPagosFacturaMedicoDetalleID(Int64 codigo)
        {
            return new DatPagosFacturasMedicos().RecuperaPagosFacturaMedicoDetalleID(codigo);  
        }
    }
}
