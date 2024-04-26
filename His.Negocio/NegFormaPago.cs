using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;
using System.Data;

namespace His.Negocio
{
    public class NegFormaPago
    {
        public static List<TIPO_FORMA_PAGO> RecuperaTipoFormaPagos()
        {
            return new DatFormaPago().RecuperaTipoFormaPagos();
        }

        public static DataTable RecuperarClasificacion()
        {
            return new DatFormaPago().RecuperarClasificacion();
        }
        public static List<DtoFormaPago> RecuperaFormaPagos()
        {
            return new DatFormaPago().RecuperaFormaPagos();
        }
        public static void GrabarFormaPago(FORMA_PAGO formapagoModificada, FORMA_PAGO formapagoOriginal)
        {
            new DatFormaPago().GrabarFormaPago(formapagoModificada, formapagoOriginal);
        }
        public static List<FORMA_PAGO> listaFormasPago()
        {
            return new DatFormaPago().listaFormasPago();
        }
        /// <summary>
        /// Retorna la lista de Formas de Pago
        /// </summary>
        /// <param name="codigoTipo">filtro por tipo</param>
        /// <returns>lista de Formas de Pago</returns>
        public static List<FORMA_PAGO> RecuperaFormaPago(Int16 codigoTipo)
        { 
            return new DatFormaPago().RecuperaFormaPago(codigoTipo);
        }
        /// <summary>
        /// Retorna el objeto FORMA_PAGO
        /// </summary>
        /// <param name="codigoFormaPago">id del objeto</param>
        /// <returns>Objeto Forma de Pago</returns>
        /// 
        public static DataTable RecuperaDescuento(string codAte, int CodigoAseguradora)
        {
            return new DatFormaPago().RecuperaDescuento(codAte, CodigoAseguradora);   
        }

        public static DataTable RecuperaFormaPagoSIC(string codAte)
        {
            return new DatFormaPago().RecuperaFormaPagoSIC(codAte);
        }

        public static DataTable DescuentoClienteFacturado(int codAte)
        {
            return new DatFormaPago().DescuentoClienteFacturado(codAte);
        }

        public static DataTable ClienteFacturado(int codAte)
        {
            return new DatFormaPago().ClienteFacturado(codAte);
        }
        public static DataTable RecuperaCodiSri(string codAte)
        {
            return new DatFormaPago().RecuperaCodiSri(codAte);
        }

        public static FORMA_PAGO RecuperaFormaPagoID(Int16 codigoFormaPago)
        {
            return new DatFormaPago().RecuperaFormaPagoID(codigoFormaPago);   
        }

         public static FORMA_PAGO FormaPagoID(int codPago)
        {
            return new DatFormaPago().FormaPagoID(codPago);
        }


        public static FORMA_PAGO FormaPagoAseguradoraEmpresa(int codAsegEmp)
        {
            return new DatFormaPago().FormaPagoAseguradoraEmpresa(codAsegEmp);
        }

        public static void ActualizarFormaPago(string forpag, int for_codigo, string descripcion, double comision, double referido)
        {
            new DatFormaPago().ActualizarFormaPago(forpag, for_codigo, descripcion, comision, referido);
        }
        public static void CrearFormaPago(string forpag, int for_codigo, string descripcion, double comision, double referido)
        {
            new DatFormaPago().CrearFormaPago(forpag, for_codigo, descripcion, comision, referido);
        }
    
        public static bool NoRepetir(string descripcion)
        {
            return new DatFormaPago().NoRepetir(descripcion);
        }
        public static int UltimoCodigoFormaPago()
        {
            return new DatFormaPago().UltimocodigoFormaPago();
        }
        public static DataTable FormaPagoAnticipo(string numrec)
        {
            return new DatFormaPago().AnticipoFormaPago(numrec);
        }
        public static FORMA_PAGO recuperarFormaPago_forpag(string forpag)
        {
            return new DatFormaPago().recuperarFormaPago_forpag(forpag);
        }
    }
}
