using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegAtencionDetalleCategorias
    {
        public static int ultimoCodigoDetalleCategorias()
        {
            return new DatAtencionDetalleCategorias().ultimoCodigoDetalleCategorias();
        }

        public static int EliminaAtencion_Detalle_Categorias(Int64 ate_codigo)
        {
            return new DatAtencionDetalleCategorias().EliminaAtencion_Detalle_Categorias(ate_codigo);
        }

        public static int ultimoCodigoDetalleCategorias_sp()
        {
            return new DatAtencionDetalleCategorias().ultimoCodigoDetalleCategorias_sp();
        }

        public static void CrearDetalleCategorias(ATENCION_DETALLE_CATEGORIAS nuevoDetalle)
        {
            try
            {
                new DatAtencionDetalleCategorias().Crear(nuevoDetalle);
            }
            catch (Exception err) { throw err; }
        }
        
        public static List<ATENCION_DETALLE_CATEGORIAS> RecuperarDetalleCategoriasAtencion(int codAtencion)
        {
            return new DatAtencionDetalleCategorias().RecuperarDetalleCategoriasAtencion(codAtencion);
        }
        

        public static bool eliminarAtencionDetalleCategorias(Int64 codDetalleCategoria)
        {
            try
            {
                return new DatAtencionDetalleCategorias().eliminarAtencionDetalleCategorias(codDetalleCategoria);
            }
            catch (Exception err) { throw err; }
        }

        public static void eliminarDetalleCategorias(int codAtencion)
        {
            new DatAtencionDetalleCategorias().eliminarDetalleCategorias(codAtencion);
        }
        public static ATENCION_DETALLE_CATEGORIAS RecuperarDetalleCategoriasID(int codDetalle)
        {
            return new DatAtencionDetalleCategorias().RecuperarDetalleCategoriasID(codDetalle);
        }
        public static void editarDetalleCategoria(ATENCION_DETALLE_CATEGORIAS detalleModif)
        {
            new DatAtencionDetalleCategorias().editarDetalleCategoria(detalleModif);
        }
    }
}
