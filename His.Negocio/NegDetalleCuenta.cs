using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;
using His.Entidades.Pedidos;
using System.Data;

namespace His.Negocio
{
    public class NegDetalleCuenta
    {
        public static List<PEDIDOS> recuperarPedidos(int codigoAtencion)
        {
            return new DatDetalleCuenta().recuperarPedidos(codigoAtencion);
        }

        public static List<PEDIDOS_AREAS> recuperarPedidosAreas()
        {
            return new DatDetalleCuenta().recuperarPedidosAreas();
        }  

        public static PEDIDOS_AREAS recuperarPedidosAreas(int codPedidoA)
        {
            return new DatDetalleCuenta().recuperarPedidosAreas(codPedidoA);
        }
        public static List<DtoDetalleCuentaPaciente> recuperarCuentaPaciente(int codPedidoA)
        {
            return new DatDetalleCuenta().recuperarCuentaPaciente(codPedidoA);
        }
        public static DataTable ReferidoPaciente(Int64 ate_codigo)
        {
            return new DatDetalleCuenta().ReferidoPaciente(ate_codigo);
        }

        public static DataTable RecuperaCuentaPacinteSP(Int64 ate_codigo)
        {
            return new DatDetalleCuenta().RecuperaCuentaPacinteSP(ate_codigo);
        }

        public static DataTable RecuperaCuentaPacinteSPFacturado(Int64 ate_codigo)
        {
            return new DatDetalleCuenta().RecuperaCuentaPacinteSPFacturado(ate_codigo);
        }

        public static int VerificaCambioCuenta(long ATE_CODIGO, string PRO_CODIGO_BARRAS, long DetalleCodigo)
        {
            return new DatDetalleCuenta().VerificaCambioCuenta(ATE_CODIGO, PRO_CODIGO_BARRAS, DetalleCodigo);
        }

        public static DataTable CargaItemsModificados(long ATE_CODIGO)
        {
            return new DatDetalleCuenta().CargaItemsModificados(ATE_CODIGO);
        }

        public static DataTable MuestraItemsModificados(long ATE_CODIGO, string PRO_CODIGO_BARRAS, long DetalleCodigo)
        {
            return new DatDetalleCuenta().MuestraItemsModificados(ATE_CODIGO, PRO_CODIGO_BARRAS, DetalleCodigo);
        }

        public static DataTable MuestraItemsModificados1(long ATE_CODIGO)
        {
            return new DatDetalleCuenta().MuestraItemsModificados1(ATE_CODIGO);
        }

        public static DataTable MuestraItemsNuevos(long ATE_CODIGO)
        {
            return new DatDetalleCuenta().MuestraItemsNuevos(ATE_CODIGO);
        }
        public static DataTable ListaItemsEliminadosCuenta(long CodigoCuenta)
        {
            return new DatDetalleCuenta().ListaItemsEliminadosCuenta(CodigoCuenta);
        }


        public static List<DtoDetalleCuentaPaciente> recuperarDetalleCuenta(int codAtencion)
        {
            return new DatDetalleCuenta().recuperarPedidosPaciente(codAtencion);
        }  

        public static DataTable GeneraValoresCuentas(long CodigoCuenta)
        {
            return new DatDetalleCuenta().ListaItemsEliminadosCuenta(CodigoCuenta);
        }

        public static DataTable RecuperaOtroCliente(string Ruc)
        {
            return new DatDetalleCuenta().RecuperaOtroCliente(Ruc);
        }

        public static DataTable RecuperaClienteSIC(string nombre)
        {
            return new DatDetalleCuenta().RecuperaClienteSIC(nombre);
        }

        public static DataTable ConvenioPago(string cat_nombre)
        {
            return new DatDetalleCuenta().ConvenioPago(cat_nombre);
        }

        public static DataTable MuestraItemsModificados(Int64 ateCodigo)
        {
            return new DatDetalleCuenta().MuestraItemsModificados(ateCodigo);
        }
        public static DataTable ListaNuevos(Int64 ateCodigo)
        {
            return new DatDetalleCuenta().ListaNuevos(ateCodigo);
        }
        public static DataTable PacientesAuditoria(DateTime desde, DateTime hasta, bool ingreso, bool alta, string hc)
        {
            return new DatDetalleCuenta().PacientesAuditoria(desde, hasta, ingreso, alta, hc);
        }
    }
}
