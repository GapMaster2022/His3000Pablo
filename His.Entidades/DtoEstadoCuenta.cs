using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoEstadoCuenta
    {
        public Int64 id { get; set; }
        public string numfac { get; set; }
        public string  tipodoc { get; set; }
        public  string iddoc { get; set; }
        public  string numdoc { get; set; }
        public  string codcli { get; set; }
        public  string fecha { get; set; }
        public  string obs { get; set; }
        public decimal debe { get; set; }
        public decimal haber { get; set; }
        public decimal saldo { get; set; }
        public string fecha1 { get; set; }
        public string forpag { get; set; }
        public string claspag { get; set; }
        public string caja { get; set; }
    }
}
