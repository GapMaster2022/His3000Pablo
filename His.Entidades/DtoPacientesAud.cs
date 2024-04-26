using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoPacientesAud
    {
        public Int64 PAC_CODIGO { get; set; }
        public Int64 ID_USUARIO { get; set; }
        public DateTime PAC_FECHA_MODIFICACION { get; set; }
        public string PAC_HISTORIA_CLINICA { get; set; }
        public string DIPO_CODIINEC { get; set; }
        public int E_CODIGO { get; set; }
        public string PAC_NOMBRE1 { get; set; }
        public string PAC_NOMBRE2 { get; set; }
        public string PAC_APELLIDO_PATERNO { get; set; }
        public string PAC_APELLIDO_MATERNO { get; set; }
        public DateTime PAC_FECHA_NACIMIENTO { get; set; }
        public string PAC_NACIONALIDAD { get; set; }
        public string PAC_TIPO_IDENTIFICACION { get; set; }
        public string PAC_IDENTIFICACION { get; set; }
        public string PAC_EMAIL { get; set; }
        public string PAC_GENERO { get; set; }
        public string PAC_IMAGEN { get; set; }
        public bool PAC_ESTADO { get; set; }
        public string PAC_DIRECTORIO { get; set; }
        public string PAC_REFERENTE_NOMBRE { get; set; }
        public string PAC_REFERENTE_PARENTESCO { get; set; }
        public string PAC_REFERENTE_TELEFONO { get; set; }
        public string PAC_ALERGIAS { get; set; }
        public string PAC_OBSERVACIONES { get; set; }
        public int GS_CODIGO { get; set; }
        public string PAC_REFERENTE_DIRECCION { get; set; }
        public bool PAC_DATOS_INCOMPLETOS { get; set; }
    }
}
