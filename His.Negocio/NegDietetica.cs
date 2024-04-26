using His.Datos;
using His.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace His.Negocio
{
    public class NegDietetica
    {

        public static DataTable getExploradorRubros(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fhc, int hc, bool Fformulario, string formulario, bool departamento)
        {
            return new DatDietetica().getExploradorRubros(desde, hasta, ingreso, alta, facturacion, Fingreso, Cod_Ingreso, Ftratamiento, Cod_Tratamiento, Fhc, hc, Fformulario, formulario, departamento);
        }

        public static DataTable getExploradorRubrosNew(DateTime desde, DateTime hasta, string hc,
            bool ingreso, bool alta, bool area, int pea_codigo, bool subarea, int rub_codigo,
            bool facturacion, bool departamento, string coddep)
        {
            return new DatDietetica().getExploradorRubrosNew(desde, hasta, hc, ingreso, alta, area, pea_codigo, subarea, rub_codigo,
                facturacion, departamento, coddep);
        }
        public static DataTable getExploradorRubrosNew2(DateTime desde, DateTime hasta, string hc, bool ingreso,
bool pisos, bool habitacion, Int64 hab_Codigo, Int64 NIV_CODIGO)
        {
            return new DatDietetica().getExploradorRubrosNew2(desde, hasta, hc, ingreso, pisos, habitacion, hab_Codigo, NIV_CODIGO);
        }
        public static DataTable getDataTable(string Tabla, string codigo = "0", string codigo2 = "0", string[] values = null, string codigo3 = "0")
        {
            return new DatDietetica().getDataTable(Tabla, codigo, codigo2, values, codigo3);
        }

        public DataTable DevolucionDatos(string codigobarra)
        {
            DatDietetica n = new DatDietetica();
            DataTable Tabla = n.VerDatosDevolucion(Convert.ToInt64(codigobarra));
            return Tabla;
        }

        public static void setROW(string tabla, object[] values, string code = "")
        {
            new DatDietetica().setROW(tabla, values, code);
        }


        public static void saveDxContrareferencia(List<PedidoImagen_diagnostico> dx, int codigo)
        {
            new DatDietetica().saveDxContrareferencia(dx, codigo);
        }

        public static void saveDxReferencia(List<PedidoImagen_diagnostico> dx, int codigo)
        {
            new DatDietetica().saveDxReferencia(dx, codigo);
        }

        public DataTable ProductosDevolucion(Int64 ped_codigo)
        {
            DatDietetica dieta = new DatDietetica();
            DataTable Tabla = dieta.ProductosDevolucion(ped_codigo);
            return Tabla;
        }

        public void Devolucion(Int64 ate_codigo, string codigo_barra, Int64 codigo_pedido)
        {
            DatDietetica dieta = new DatDietetica();
            dieta.Devolucion(ate_codigo, codigo_barra, codigo_pedido);
        }
        public static bool DiasFestivos(DateTime Hoy)
        {
            return new DatDietetica().DiasFestivos(Hoy);

        }
        public void RecargoImagen(string cue_codigo)
        {
            DatDietetica dieta = new DatDietetica();
            dieta.RecargoImagen(Convert.ToInt64(cue_codigo));
        }

        public static DataTable getReferencias(Int64 ate_codigo)
        {
            return new DatDietetica().getReferencias(ate_codigo);
        }
        public static DataTable ReporteHonorario(Int64 ate_codigo, Int64 codigo_pedido)
        {
            return new DatDietetica().ReporteHonorarios(ate_codigo, codigo_pedido);
        }
        public static DataTable HonorariosAsientoContable(DateTime fechaInicio, DateTime fechaFinal, bool porFuera, bool porSeguro, int valor)
        {
            return new DatDietetica().HonorariosAsientosContables(fechaInicio, fechaFinal, porFuera, porSeguro, valor);
        }
    }

}
