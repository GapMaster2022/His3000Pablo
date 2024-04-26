using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class NuevoAuditoria
    {
        public Int64 cue_codigoAud { get; set; }
        public Int64 ate_codigoAud { get; set; }
        public string cue_detalleAud { get; set; }
        public decimal cue_cantidadAdi { get; set; }
        public decimal cue_valorUnitarioAdi { get; set; }
        public decimal cue_ivaAdi { get; set; }
        public decimal cue_valorAdi { get; set; }
        public string usuarioAdi { get; set; }
    }
}
