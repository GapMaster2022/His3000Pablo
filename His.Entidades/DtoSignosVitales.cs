using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoSignosVitales
    {
        public string PULSO { get; set; }
        public string TEMPERATURA { get; set; }

        public TimeSpan HORA { get; set; }
    }
}
