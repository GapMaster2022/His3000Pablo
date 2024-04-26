using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegAtencionDetalleSeguros
    {
        public static int ultimoCodigoDetalleCategorias()
        {
            return new DatAtencionDetalleSeguros().ultimoCodigoDetalleSeguros();
        }

        public static void CrearDetalleCategorias(ATENCIONES_DETALLE_SEGUROS nuevoDetalle)
        {
            try
            {
                new DatAtencionDetalleSeguros().Crear(nuevoDetalle);
            }
            catch (Exception err) { throw err; }
        }
        public static ATENCIONES_DETALLE_SEGUROS RecuAtencionesDetalleSeguros(int codAtencion)
        {
            return new DatAtencionDetalleSeguros().RecuperarDetalleSegurosAtencion(codAtencion);
        }

        public static void eliminarAtencionDetalleSeguro(int codAtencion)
        {
            new DatAtencionDetalleSeguros().eliminarAtencionDetalleSeguro(codAtencion);
        }
        //public static ATENCIONES_DETALLE_SEGUROS RecuperarDetalleCategoriasID(int codDetalle)
        //{
        //    return new DatAtencionDetalleCategorias().RecuperarDetalleCategoriasID(codDetalle);
        //}
        public static void editarDetalleSeguro(ATENCIONES_DETALLE_SEGUROS detalleModif)
        {
            new DatAtencionDetalleSeguros().editarDetalleSeguros(detalleModif);
        }

        public static List<ATENCIONES_SEGUROS_DIAGNOSTICOS> recuperarDiagnosticosAtencion(int codAtenDetalle)
        {
            return new DatAtencionDetalleSeguros().recuperarDiagnosticosAtencion(codAtenDetalle);
        }

        /// <summary>
        /// Método para guadar un objeto ATENCIONES_SEGUROS_DIAGNOSTICOS en la Base de Datos
        /// </summary>
        /// <param name="diagnostico">ATENCIONES_SEGUROS_DIAGNOSTICOS</param>
        public static void crearASDiagnostico(ATENCIONES_SEGUROS_DIAGNOSTICOS diagnostico)
        {
             try
             {
                 new DatAtencionDetalleSeguros().crearASDiagnostico(diagnostico);
             }
             catch (Exception err) { throw err; }
        }

        /// <summary>
        /// Actualizar ATENCIONES_SEGUROS_DIAGNOSTICOS
        /// </summary>
        /// <param name="detalle">ATENCIONES_SEGUROS_DIAGNOSTICOS</param>
        public static void actualizarASDiagnostico(ATENCIONES_SEGUROS_DIAGNOSTICOS detalle)
        {
            new DatAtencionDetalleSeguros().actualizarASDiagnostico(detalle);
        }

        public static void eliminarASDiagnosticoDetalle(int codigoDiagnosticoDetalle)
        {
            new DatAtencionDetalleSeguros().eliminarASDiagnosticoDetalle(codigoDiagnosticoDetalle);
        }


    }
}
