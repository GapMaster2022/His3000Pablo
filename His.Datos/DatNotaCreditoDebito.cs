using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using His.Parametros;


namespace His.Datos
{
    public class DatNotaCreditoDebito
    {
        public int RecuperaNotaCreditoDebitoMaximo(Int16 tipcod)
        {
            int maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<NOTAS_CREDITO_DEBITO> notacd = contexto.NOTAS_CREDITO_DEBITO.Include("TIPO_DOCUMENTO").Where(cod => cod.TID_CODIGO == tipcod).ToList();
                if (notacd.Count > 0)
                    maxim = int.Parse(contexto.NOTAS_CREDITO_DEBITO.Include("TIPO_DOCUMENTO").Where(cod => cod.TID_CODIGO == tipcod).Max(emp => emp.NOT_NUMERO).Substring(6));
                else
                    maxim = 0;
                return maxim;
            }

        }
        public Int64 RecuperaDetalleNotaDebitoMaximo()
        {
            Int64 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<NOTAS_CREDITO_DEBITO_DETALLE> notacd = contexto.NOTAS_CREDITO_DEBITO_DETALLE.ToList();
                if (notacd.Count > 0)
                    maxim = contexto.NOTAS_CREDITO_DEBITO_DETALLE.Max(emp => emp.NCD_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }
        }
        /// <summary>
        /// Metodo que crea una nueva nota de debito
        /// </summary>
        /// <param name="notaDebito">Objeto Nota de Debito</param>
        public void CrearNotaDebito(NOTAS_CREDITO_DEBITO notaDebito)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("NOTAS_CREDITO_DEBITO", notaDebito);
            }
        }
        public void CreaDetalleNotaDebito(NOTAS_CREDITO_DEBITO_DETALLE dnotaDebito)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("NOTAS_CREDITO_DEBITO_DETALLE", dnotaDebito);
            }
        }
        public void GrabarNotaDebito(NOTAS_CREDITO_DEBITO notaDebitoModificada, NOTAS_CREDITO_DEBITO notaDebitoOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                contexto.Grabar(notaDebitoModificada, notaDebitoOriginal);
            }
        }
        public void EliminarNotaDebito(NOTAS_CREDITO_DEBITO notaDebito)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(notaDebito);
            }
        }
       
        public List<DtoNotaCreditoDebito> RecuperaNotasCreditoDebito()
        {
            List<DtoNotaCreditoDebito> medicogrid = new List<DtoNotaCreditoDebito>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<NOTAS_CREDITO_DEBITO> medicos = new List<NOTAS_CREDITO_DEBITO>();
                medicos = contexto.NOTAS_CREDITO_DEBITO.Include("CAJAS").Include("USUARIOS").Include("TIPO_DOCUMENTO").ToList();
                foreach (var acceso in medicos)
                {
                    medicogrid.Add(new DtoNotaCreditoDebito()
                    {
                        CAJ_CODIGO=acceso.CAJAS.CAJ_CODIGO,
                        ID_USUARIO=acceso.USUARIOS.ID_USUARIO,
                        NOT_CANCELADO=acceso.NOT_CANCELADO == null ? false : bool.Parse(acceso.NOT_CANCELADO.ToString()),
                        NOT_CUENTA_CONTABLE=acceso.NOT_CUENTA_CONTABLE,
                        NOT_DOCUMENTO_AFECTADO= acceso.NOT_DOCUMENTO_AFECTADO,
                        NOT_DOCUMENTO_TIPO=acceso.NOT_DOCUMENTO_TIPO,
                        NOT_FECHA=acceso.NOT_FECHA,
                        NOT_IVA=acceso.NOT_IVA==null? decimal.Parse("0") : decimal.Parse(acceso.NOT_IVA.ToString()),
                        NOT_MOTIVO_MODIFICACION=acceso.NOT_MOTIVO_MODIFICACION,
                        NOT_NUMERO=acceso.NOT_NUMERO,
                        NOT_RAZON_SOCIAL=acceso.NOT_RAZON_SOCIAL,
                        NOT_RUC=acceso.NOT_RUC,
                        NOT_VALOR=acceso.NOT_VALOR,
                        TNO_CODIGO= acceso.TNO_CODIGO== null? Int16.Parse("0"): Int16.Parse(acceso.TNO_CODIGO.ToString()),
                        TID_CODIGO=acceso.TID_CODIGO,
                        NOT_ANULADO=acceso.NOT_ANULADO==null ? false: bool.Parse(acceso.NOT_ANULADO.ToString()),
                        FOR_CODIGO1= acceso.FOR_CODIGO1==null? Int16.Parse("0"): Int16.Parse(acceso.FOR_CODIGO1.ToString()),
                        HOM_CODIGO1 = acceso.HOM_CODIGO1 == null ? int.Parse("0") : int.Parse(acceso.HOM_CODIGO1.ToString()),
                        MED_CODIGO1 = acceso.MED_CODIGO1 == null ? Int16.Parse("0") : Int16.Parse(acceso.MED_CODIGO1.ToString()),
                        
                        ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName(),
                        ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key
                    });
                }
                return medicogrid;
            }
        }

        public List<NOTAS_CREDITO_DEBITO> ListaNotasCreditoDebito()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                return contexto.NOTAS_CREDITO_DEBITO.Include("CAJAS").Include("TIPO_DOCUMENTO").Include("USUARIOS").ToList();
            }
        }
        /// <summary>
        /// Recupera una lista con las notas de credito internas que tienen relacionada una forma de pago
        /// </summary>
        /// <param name="codigoFormaPago">Codigo de la Forma de Pago</param>
        /// <returns>lista de NOTAS_CREDITO_DEBITO que tienen relacionada una forma de pago</returns>
        public List<NOTAS_CREDITO_DEBITO> RecuperarNotasCreditoInternasPorFormaPago(Int16  codigoTipoFormaPago)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<NOTAS_CREDITO_DEBITO> notas = (from n in contexto.NOTAS_CREDITO_DEBITO
                                join f in contexto.FORMA_PAGO on n.FOR_CODIGO1 equals f.FOR_CODIGO
                                where (n.TNO_CODIGO == HonorariosPAR.codigoTipoNotaCreditoInterna ||
                                n.TNO_CODIGO == HonorariosPAR.codigoTipoNotaDebitoInterna) && n.NOT_CANCELADO == false && f.TIPO_FORMA_PAGO.TIF_CODIGO == codigoTipoFormaPago 
                                select n).ToList() ;
                    return notas ;
                    //return contexto.NOTAS_CREDITO_DEBITO.Where(n => (n.TNO_CODIGO == 5 ||n.TNO_CODIGO == 6)  && n. == codigoFormaPago && n.NOT_CANCELADO == false).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
