using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegNotaCreditoDebito
    {
        public static int RecuperaNotaCreditoDebitoMaximo(Int16 tipcod)
        {
            return new DatNotaCreditoDebito().RecuperaNotaCreditoDebitoMaximo(tipcod);
        }
        public static Int64 RecuperaDetalleNotaDebitoMaximo()
        {
            return new DatNotaCreditoDebito().RecuperaDetalleNotaDebitoMaximo();
        }
        /// <summary>
        /// Metodo que crea una nueva nota de debito
        /// </summary>
        /// <param name="notaDebito">Objeto Nota de Debito a guardar</param>
        public static void CrearNotaDebito(NOTAS_CREDITO_DEBITO notaDebito)
        {
            new DatNotaCreditoDebito().CrearNotaDebito(notaDebito);
        }
        public static void CreaDetalleNotaDebito(NOTAS_CREDITO_DEBITO_DETALLE dnotaDebito)
        {
            new DatNotaCreditoDebito().CreaDetalleNotaDebito(dnotaDebito);
        }
        public static void GrabarNotaDebito(NOTAS_CREDITO_DEBITO notaDebitoModificada, NOTAS_CREDITO_DEBITO notaDebitoOriginal)
        {
            new DatNotaCreditoDebito().GrabarNotaDebito(notaDebitoModificada, notaDebitoOriginal);
        }
        public static void EliminarNotaDebito(NOTAS_CREDITO_DEBITO notaDebito)
        {
            new DatNotaCreditoDebito().EliminarNotaDebito(notaDebito);
        }
        public static List<DtoNotaCreditoDebito> RecuperaNotasCreditoDebito()
        {
            return new DatNotaCreditoDebito().RecuperaNotasCreditoDebito();
        }
        public static List<NOTAS_CREDITO_DEBITO> ListaNotasCreditoDebito()
        {
            return new DatNotaCreditoDebito().ListaNotasCreditoDebito();
        }
        /// <summary>
        /// Recupera una lista con las notas de credito internas que tienen relacionada una forma de pago
        /// </summary>
        /// <param name="codigoFormaPago">Codigo de la Forma de Pago</param>
        /// <returns>lista de NOTAS_CREDITO_DEBITO que tienen relacionada una forma de pago</returns>
        public static  List<NOTAS_CREDITO_DEBITO> RecuperarNotasCreditoInternasPorFormaPago(Int16 codigoTipoFormaPago)
        {
            try
            {
                return new DatNotaCreditoDebito().RecuperarNotasCreditoInternasPorFormaPago(codigoTipoFormaPago);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
