using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;

namespace His.Entidades
{
    public class DtoPacienteDatosAdicionales2
    {
        public Int64 COD_PACIENTE { get; set; }
        public string REF_TELEFONO_2 { get; set; }
        public bool FALLECIDO { get; set; }
        public string FEC_FALLECIDO { get; set; }
        public string FOLIO { get; set; }
        public string email { get; set; }

        public string motivo { get; set; }
        public string diagnostico { get; set; }
        public string id_persona_tramita { get; set; }
        public string nombre_apellido_tramita { get; set; }
        public string telf_convencional { get; set; }
        public DateTime fecha_entrega_documento { get; set; }
        public int id_usuario { get; set; }
        public int id_usuario_revierte { get; set; }
        public string nac_etnica { get; set; }
        public string pueblo { get; set; }
        public string neducacion { get; set; }
        public string tempresa { get; set; }
        public string ssalud { get; set; }
        public string tbono { get; set; }
    }
}
