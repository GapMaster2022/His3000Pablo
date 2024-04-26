using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatPagosFacturasMedicos {
        /// <summary>
        /// Devuelve una instancia de PAGOS_FAC_MEDICOS
        /// </summary>
        /// <param name="codigo">codigo del pago de factura medico</param>
        /// <returns>Instancia de PAGOS_FAC_MEDICOS</returns>
        public PAGOS_FAC_MEDICOS RecuperaPagosFacturaMedicoID(Int64 codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.PAGOS_FAC_MEDICOS.FirstOrDefault(p => p.PAM_CODIGO == codigo);  
            }
        }
        /// <summary>
        /// Devuelve una instancia de PAGOS_FAC_MEDICOS_DETALLE
        /// </summary>
        /// <param name="codigo"> codigo del detalle del pago de factura medico</param>
        /// <returns>Instancia de PAGOS_FAC_MEDICOS_DETALLE</returns>
        public PAGOS_FAC_MEDICOS_DETALLE RecuperaPagosFacturaMedicoDetalleID(Int64 codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.PAGOS_FAC_MEDICOS_DETALLE.FirstOrDefault(p => p.PMD_CODIGO == codigo);
            }
        }
        /// <summary>
        /// Recupera la lista con los cancelaciones a las facturas de  medicos
        /// </summary>
        /// <returns>Lista de cancelaciones honorarios medicos</returns>
        public List<PAGOS_FAC_MEDICOS_VIEW> RecuperarPagosFacturasMedicos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PAGOS_FAC_MEDICOS_VIEW> pagosFacturasMedicos = new List<PAGOS_FAC_MEDICOS_VIEW>(); 
                pagosFacturasMedicos =contexto.PAGOS_FAC_MEDICOS_VIEW.ToList();
                return pagosFacturasMedicos;
            }
        }
        /// <summary>
        /// Metodo que devuelve una lista de la cartera recuperada
        /// </summary>
        /// <param name="fecDocIni">Fecha inicial del documento</param>
        /// <param name="fecDocFin">Fecha final del documento</param>
        /// <param name="codTipoFormaPago">Codigo del tipo de forma de pago</param>
        /// <param name="codFormaPago">Codigo de la Forma de pago</param>
        /// <returns>lista de PAGOS_FAC_MEDICOS_VIEW</returns>
        public List<PAGOS_FAC_MEDICOS_VIEW> RecuperarPagosFacturasMedicos(string fecDocIni, string fecDocFin , string codTipoFormaPago,string codFormaPago)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PAGOS_FAC_MEDICOS_VIEW> pagosFacturasMedicos = new List<PAGOS_FAC_MEDICOS_VIEW>();
                if (fecDocIni == null && codTipoFormaPago==null)
                {
                    pagosFacturasMedicos = contexto.PAGOS_FAC_MEDICOS_VIEW.ToList();
                }
                else if (fecDocIni != null && codTipoFormaPago == null)
                { 
                    DateTime fechaIni = Convert.ToDateTime(fecDocIni + " 00:00:00");
                    DateTime fechaFin = Convert.ToDateTime(fecDocFin + " 23:59:59");
                    pagosFacturasMedicos = contexto.PAGOS_FAC_MEDICOS_VIEW.Where(p => p.PAM_FECHA >= fechaIni && p.PAM_FECHA <= fechaFin).OrderBy(p => p.PAM_FECHA).ToList();      
                }
                else if (fecDocIni != null && codTipoFormaPago != null)
                {
                    DateTime fechaIni = Convert.ToDateTime(fecDocIni + " 00:00:00");
                    DateTime fechaFin = Convert.ToDateTime(fecDocFin + " 23:59:59");
                    Int16 tipoFormaPago = Convert.ToInt16(codTipoFormaPago); 
                    if (codFormaPago == null)
                    {
                        pagosFacturasMedicos = (from p in contexto.PAGOS_FAC_MEDICOS_VIEW
                                               join f in contexto.FORMA_PAGO on p.FOR_CODIGO equals f.FOR_CODIGO
                                               join t in contexto.TIPO_FORMA_PAGO on f.TIPO_FORMA_PAGO.TIF_CODIGO equals t.TIF_CODIGO 
                                               where p.PAM_FECHA >= fechaIni && p.PAM_FECHA <= fechaFin && t.TIF_CODIGO == tipoFormaPago 
                                               orderby p.PAM_FECHA
                                               select p).ToList() ;
                    }
                    else
                    {
                        Int16 formaPago = Convert.ToInt16(codFormaPago );
                        pagosFacturasMedicos = contexto.PAGOS_FAC_MEDICOS_VIEW.Where(p => p.PAM_FECHA >= fechaIni && p.PAM_FECHA <= fechaFin && p.FOR_CODIGO==formaPago ).OrderBy(p => p.PAM_FECHA).ToList();      
                    }
                }
                else if (fecDocIni == null && codTipoFormaPago != null)
                {
                    Int16 tipoFormaPago = Convert.ToInt16(codTipoFormaPago);
                    if (codFormaPago == null)
                    {
                        pagosFacturasMedicos = (from p in contexto.PAGOS_FAC_MEDICOS_VIEW
                                                join f in contexto.FORMA_PAGO on p.FOR_CODIGO equals f.FOR_CODIGO
                                                join t in contexto.TIPO_FORMA_PAGO on f.TIPO_FORMA_PAGO.TIF_CODIGO equals t.TIF_CODIGO
                                                where t.TIF_CODIGO == tipoFormaPago
                                                orderby p.PAM_FECHA
                                                select p).ToList();
                    }
                    else
                    {
                        Int16 formaPago = Convert.ToInt16(codFormaPago);
                        pagosFacturasMedicos = contexto.PAGOS_FAC_MEDICOS_VIEW.Where(p=>p.FOR_CODIGO == formaPago).OrderBy(p => p.PAM_FECHA).ToList();
                    }
                }
                return pagosFacturasMedicos;
            }
        }
        /// <summary>
        /// Recupera el Detalle de un pago especifico
        /// </summary>
        /// <param name="codigoPago">codigo del pago de la factura del medico</param>
        /// <returns>lista de PAGOS_FAC_MEDICOS_DETALLE de un determinado pago </returns>
        public List<PAGOS_FAC_MEDICOS_DETALLE_VIEW> RecuperarPagosFacturasMedicosDetalle(Int64 codigoPago)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.PAGOS_FAC_MEDICOS_DETALLE_VIEW.Where(p =>p.PAM_CODIGO ==codigoPago).ToList();
            }
        }
        /// <summary>
        /// Crear un nueva cancelacion de las facturas de medicos
        /// </summary>
        /// <param name="pagoFacturasMedicos">pago facturas de medicos</param>
        public PAGOS_FAC_MEDICOS CrearPagoFacturasMedicos(PAGOS_FAC_MEDICOS pagoFacturasMedicos)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.AddToPAGOS_FAC_MEDICOS(pagoFacturasMedicos);
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
            return pagoFacturasMedicos; 
        }
        /// <summary>
        /// Crear un nuevo detalle de la cancelacion de facturas medicas
        /// </summary>
        /// <param name="pagoFacturasMedicosDetalle">detalle pago facturas medicos</param>
        public void CrearPagoFacturasMedicosDetalle(PAGOS_FAC_MEDICOS_DETALLE pagoFacturasMedicosDetalle)
        {
            using(var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM)) 
            {
                contexto.Crear("PAGOS_FAC_MEDICOS_DETALLE", pagoFacturasMedicosDetalle);  
            }
        }
    }
}