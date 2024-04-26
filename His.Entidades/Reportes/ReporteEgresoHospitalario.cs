using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades.Reportes
{
    public class ReporteEgresoHospitalario
    {
        public int idEpicrisis {get; set;}
        public DateTime fecIngreso {get; set;}
        public DateTime fecEgreso {get; set;}
        public int diasPermanencia {get; set;}
        public string servicio {get; set;}
        public int tipEgreso {get; set;}
        public string diagSescripcionIng {get; set;}
        public string diagCodigoIng {get; set;}
        public bool diagEstadoIng {get; set;}
        public string diagDescripcionEgreso {get; set;}
        public string diagCodigoEgreso {get; set;}
        public bool diagEstadoEgreso  {get; set;}
        public Int16 tipTratamiento {get; set;}
        public string procedimiento {get; set;}
        public string responsable { get; set; }
    }
}
