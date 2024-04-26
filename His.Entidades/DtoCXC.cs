using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoCXC
    {
        public string codcli { get; set; }
        public string  numdoc { get; set; }
        public DateTime fecha { get; set; }
        public  string tipo { get; set; }
        public decimal debe { get; set; }
        public decimal haber { get; set; }
        public decimal saldo { get; set; }
        public string fecha1 { get; set; }
        public DateTime fechapago { get; set; }
        public string tipest { get; set; }
        public DateTime fecven { get; set; }
        public string forpag { get; set; }
        public string claspag { get; set; }
        public string fecven1 { get; set; }
        public int fila { get; set; }
        public string Marca { get; set; }
    }
}
