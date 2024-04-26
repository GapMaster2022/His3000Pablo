using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class IngresaKardex
    {
        public Int64 id_kardex { get; set; }
        public string ate_codigo { get; set; }
        public string presentacion { get; set; }
        public string via { get; set; }
        public string dosis { get; set; }
        public string frecuencia { get; set; }
        public DateTime hora { get; set; }
        public bool eventual { get; set; }
        public bool medPropio { get; set; }
        public DateTime fecha { get; set; }
        public DateTime fechaSistema { get; set; }
        public string observaciones { get; set; }
        public bool administrador { get; set; }
        public bool noAdministrador { get; set; }

    }
}
