using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoPacientes
    {
        public string PAC_HISTORIA_CLINICA { get; set; }
        public int PAC_CODIGO { get; set; } 
        public Int16 ID_USUARIO { get; set; }
        public Int16 CODCIUDAD { get; set; }
        public Int16 ESC_CODIGO { get; set; }
        public string PAC_APELLIDO_PATERNO { get; set; }
        public string PAC_APELLIDO_MATERNO { get; set; }
        public string PAC_NOMBRE1 { get; set; }
        public string PAC_NOMBRE2 { get; set; }
        public DateTime PAC_FECHA_CREACION { get; set; }
        public DateTime PAC_FECHA_NACIMIENTO { get; set; }
        public string PAC_NACIONALIDAD { get; set; }
        public string PAC_ETNIA { get; set; }
        public string PAC_GRUPOSANQUINEO { get; set; }
        public string PAC_TIPO_IDENTIFICACION { get; set; }
        public string PAC_IDENTIFICACION { get; set; }
        public string PAC_TELEFONO { get; set; }
        public string PAC_TELEFONO2 { get; set; }
        public string PAC_DIRECCION { get; set; }
        public string PAC_BARRIO { get; set; }
        public string PAC_PARROQUIA { get; set; }
        public string PAC_CANTON { get; set; }
        public string PAC_PROVINCIA { get; set; }
        public string PAC_ZONA_URBANA { get; set; }
        public string PAC_EMAIL { get; set; }
        public string PAC_GENERO { get; set; }
        public string PAC_INSTRUCCION { get; set; }
        public string PAC_OCUPACION { get; set; }
        public string PAC_EMPRESA_TRABAJO { get; set; }
        public string PAC_EMPRESA_DIRECCION { get; set; }
        public string PAC_EMPRESA_TELEFONO { get; set; }
        public string PAC_EMPRESA_CIUDAD { get; set; }
        public string PAC_IMAGEN { get; set; }
        public bool PAC_ESTADO { get; set; }
        public string ENTITYSETNAME { get; set; }
        public string ENTITYID { get; set; }
    }
}
