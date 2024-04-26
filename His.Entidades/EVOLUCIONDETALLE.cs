using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class EVOLUCIONDETALLE
    {
        public Int64 evd_codigo { get; set; }
        public int evo_codigo { get; set; }
        public int id_usuario{get;set;}
        public string nom_usuario { get; set; }
        public DateTime evd_fecha { get; set; }
        public string evd_descripcion { get; set; }
        public DateTime fechInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public Int64 med_codigo { get; set; }
        
    }
}
