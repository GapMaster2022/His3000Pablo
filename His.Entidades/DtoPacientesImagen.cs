using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoPacientesImagen
    {
        public string Habitacion { get; set; }
        public int ID { get; set; }
        public DateTime Agendamiento { get; set; }
        public DateTime Solicitud { get; set; }
        public string HC { get; set; }
        public int Atencion { get; set; }
        public string Paciente { get; set; }
        public string Identificacion { get; set; }
        public string Radiologo { get; set; }
        public string Tecnologo { get; set; }
        public string Observacion { get; set; }
        public string Estado { get; set; }
    }
}
