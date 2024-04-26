using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoFacturaPago
    {public string numdoc { get; set; }
        public int tipdoc { get; set; }
        public string forpag { get; set; }
        public string tipomov { get; set; }
        public string codcli { get; set; }
        public string parcial { get; set; }
        public string parcial1 { get; set; }
        public string claspag { get; set; }
        public string tipoventa { get; set; }
        public DateTime fecha { get; set; }
        public string  fecha1 { get; set; }
        public string banco { get; set; }
        public string numcuenta_tarj { get; set; }
        public string cheque_caduca { get; set; }
        public string dueño { get; set; }
        public string autoriza { get; set; }
        public string obs { get; set; }
        public int fila { get; set; }
        public string caja { get; set; }
        public string cajero { get; set; }
        public string Vendedor { get; set; }
        public string local { get; set; }
        public Boolean Arqueada { get; set; }
        public Boolean imprime { get; set; }
        public Boolean detalle { get; set; }
    }
}
