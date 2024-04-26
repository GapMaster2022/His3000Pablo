using His.Datos;
using His.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace His.Negocio
{
    public class NegLiquidacion
    {
        public static DsHonorarios honorariosCxE(DateTime desde, DateTime hasta)
        {
            return new DatLiquidaciones().HonorariosCxE(desde, hasta);
        } 
        public static bool guardarLiquidacion(List<LIQUIDACION> liquidacion)
        {
            return new DatLiquidaciones().guardarLiquidacion(liquidacion);
        }
        public static bool Bloquearhonorario(List<LIQUIDACION> liquidado)
        {
            return new DatLiquidaciones().bloquearHonorario(liquidado);
        }
        public static List<LIQUIDACION> recuperarLiquidacion(Int64 numdoc)
        {
            return new DatLiquidaciones().RecuperarLiquidacion(numdoc);
        }
        public static List<LIQUIDACION> recuperarLiquidacionPendiente(Int64 numdoc)
        {
            return new DatLiquidaciones().RecuperarLiquidacionPendiente(numdoc);
        }
        public static HONORARIOS_MEDICOS recuperarHonorario(Int64 hom_codigo)
        {
            return new DatLiquidaciones().recuperarHonorario(hom_codigo);
        }
        public static IEnumerable<object> listarLiquidaciones(DateTime desde, DateTime hasta, int liquidacion, string factura, int medico,bool liquidado)
        {
            return new DatLiquidaciones().listarLiquidaciones(desde, hasta, liquidacion, factura, medico, liquidado);
        }
        public static DsLiquidacion liquidar(DateTime desde, DateTime hasta)
        {
            return new DatLiquidaciones().Liquidar(desde, hasta);
        }
        public static PARAMETROS_DETALLE parametrosHonorarios(int pad_codido)
        {
            return new DatLiquidaciones().parametroHonorarios(pad_codido);
        }
        public static DataTable DatosSRI(double codigo_c, Int64 factura)
        {
            return new DatLiquidaciones().DatosSRI(codigo_c, factura);
        }
        public static bool LiquidacionGlobal(List<Cgcabmae> cabmae, List<Cgdetmae> detmae)
        {
            return new DatLiquidaciones().LiquidacionGlobal(cabmae, detmae);
        }
        public static void LiberarControlADS(DateTime fecha)
        {
            new DatLiquidaciones().LiberarControlADS(fecha);
        }
        public static bool bloquearLiquidacion(List<Cgdetmae> ldetmae, List<LIQUIDACION> l)
        {
            return new DatLiquidaciones().BloquearLiquidacion(ldetmae, l);
        }
        public static bool desbloquearLiquidacion(Int64 liq_codigo, int med_codigo, double numdoc, List<Cgdetmae> ldetmae)
        {
            return new DatLiquidaciones().desbloquearLiquidacion(liq_codigo, med_codigo, numdoc, ldetmae);
        }
        public static bool validaFactura(string factura, double codigo_c)
        {
            return new DatLiquidaciones().validaFactura(factura, codigo_c);   
        }
        public static bool validaFacturaCG(string factura, double codigo_c)
        {
            return new DatLiquidaciones().validaFacturaCG(factura, codigo_c);
        }
        public static DataTable reporteAsientoLiquidacion(double numdoc, string tipo, int parametro)
        {
            return new DatLiquidaciones().ReporteAsientoLiquidacion(numdoc, tipo, parametro);
        }
        public static bool guardoAudtoria(List<Cgcabmae> cab, Int64 codigo, string estado, DateTime fecha)
        {
            return new DatLiquidaciones().guardarAuditoria(cab, codigo, estado, fecha);
        }
        public static bool reversarLiquidacion(Int64 liquidacion)
        {
            return new DatLiquidaciones().ReversionLiquidacion(liquidacion);
        }
        public static bool ValidaLiquidacionUsada(double numdoc, string tipo)
        {
            return new DatLiquidaciones().ValidaLiquidacionUsada(numdoc, tipo);
        }
        public static bool anulacionCG(double numdoc, string tipo, double codigo)
        {
            return new DatLiquidaciones().anulacionCG(numdoc, tipo, codigo);
        }
        public static bool anularHonoratio(string HOM_CODIGO)
        {
            return new DatLiquidaciones().anularHonoratio(HOM_CODIGO);
        }
        public static bool anulacionLiquidacion(double numdoc)
        {
            return new DatLiquidaciones().anulacionLiquidacion(numdoc);
        }
        public static bool validaReversoLiquidacion(Int64 liquidacion)
        {
            return new DatLiquidaciones().validaReversionLiquidacion(liquidacion);
        }
    }
}
