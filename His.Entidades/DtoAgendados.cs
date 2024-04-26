using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public  class DtoAgendados
    {
        public Int64 Codigo { get; set; }
        public string Medico { get; set; }
        public string Especialidad { get; set; }
        public string Consultorio { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
    }
}
