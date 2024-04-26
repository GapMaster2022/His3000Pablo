using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoCgDetmae
    {
        public string tipdoc { get; set; }
        public double numdoc { get; set; }
        public int linea { get; set; }
        public string año { get; set; }
        public DateTime fechatran { get; set; }
        public int codzona { get; set; }
        public double codloc { get; set; }
        public string codcue_cp { get; set; }
        public string cuenta_pc { get; set; }
        public string subcta_pc { get; set; }
        public string codpre_pc { get; set; }
        public double codigo_c { get; set; }
        public string nocomp { get; set; }
        public string beneficiario { get; set; }
        public double debe { get; set; }
        public double haber { get; set; }
        public string comentario { get; set; }
        public string movbanc { get; set; }
        public Int64 hom_codigo { get; set; }
        public string observacion { get; set; }
        public string forpag { get; set; }
        public string despag { get; set; }
    }
}
