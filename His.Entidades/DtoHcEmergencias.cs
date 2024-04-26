using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoHcEmergencias
    {
        public Int64 codEmergencia { get; set; }
        public Int64 codPaciente { get; set; }
        public Int64 codHistoriaClinica { get; set; }
        public string nombrePaciente { get; set; }
        public string apellidoPaciente { get; set; }
        public string genero { get; set; }
        //public string generof { get; set; }
        public Boolean estado { get; set; }
        public Boolean urgente { get; set; }
        public Boolean emergente { get; set; }
        public Boolean critico { get; set; }
        public string otras { get; set; }
        public int clinicas { get; set; }
        public int quirur { get; set; }
        public int otrasEsp { get; set; }
        public int edad { get; set; }

    }
}
