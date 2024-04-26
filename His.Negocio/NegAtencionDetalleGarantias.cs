using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using System.Data;

namespace His.Negocio
{
    public class NegAtencionDetalleGarantias
    {
        public static int ultimoCodigoDetalleGarantias()
        {
            return new DatAtencionDetalleGarantias().ultimoCodigoDetalleGarantias();
        }
        public static void CrearDetalleGarantias(ATENCION_DETALLE_GARANTIAS nuevoDetalle)
        {
            new DatAtencionDetalleGarantias().Crear(nuevoDetalle);
        }
        public static List<ATENCION_DETALLE_GARANTIAS> RecuperarDetalleGarantiasAtencion(int codAtencion)
        {
            return new DatAtencionDetalleGarantias().RecuperarDetalleGarantiasAtencion(codAtencion);
        }

        //RECUPERA GARANTIA EN DATATABLE PABLO ROCHA 05/09/2014
        public static DataTable SPRecuperaGarantia(int codAtencion)
        {
            return new DatAtencionDetalleGarantias().SPRecuperaGarantia(codAtencion);
        }

        //RECUPERA GARANTIAS CON FILTROS PABLO ROCHA 05/09/2014
        public static DataTable SPRecuperaGarantiaFecha(string fechaInicio, string fechaFin)
        {
            return new DatAtencionDetalleGarantias().SPRecuperaGarantiaFecha(fechaInicio,fechaFin);
        }

        public static void eliminarDetalleGarantias(int codAtencion)
        {
            new DatAtencionDetalleGarantias().eliminarDetalleGarantias(codAtencion);
        }
        public static ATENCION_DETALLE_GARANTIAS RecuperarDetalleGarantiasID(int codDetalle)
        {
            return new DatAtencionDetalleGarantias().RecuperarDetalleGarantiasID(codDetalle);
        }
        public static void editarDetalleGarantia(ATENCION_DETALLE_GARANTIAS detalleModif)
        {
            new DatAtencionDetalleGarantias().editarDetalleGarantia(detalleModif);
        }
    }
}
