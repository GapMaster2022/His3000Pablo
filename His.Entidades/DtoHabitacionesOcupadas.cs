using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoHabitacionesOcupadas
    {
        public Int16 CODIGO_HABITACION { get; set; }
        public string NUMERO_HABITACION { get; set; }
        public int ATENCION { get; set; }
        public DateTime FECHA_INGRESO { get; set; }
        public DateTime FECHA_ALTA_MEDICO{ get; set; }
    }
}
