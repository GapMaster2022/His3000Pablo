using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using System.Data;

namespace His.Negocio
{
    public class NegReporteEdades
    {
        DatReporteEdades ReporteEdades = new DatReporteEdades();

        public DataTable CargarAtenciones()
        {
            DataTable Tabla = ReporteEdades.CargarAtenciones();
            return Tabla;
        }
        public DataTable EdadesxAtencion(string tip_codigo, DateTime fechadesde, DateTime fechahasta)
        {
            DataTable Tabla = ReporteEdades.EdadesxAtencion(Convert.ToInt32(tip_codigo), fechadesde, fechahasta);
            return Tabla;
        }
    }
}
