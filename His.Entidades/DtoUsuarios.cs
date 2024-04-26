using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{

    public class DtoUsuarios
    {
        public Int16 ID_USUARIO { get; set; }
        public Int16 DEP_CODIGO { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDOS { get; set; }
        public string IDENTIFICACION { get; set; }
        public DateTime FECHA_INGRESO { get; set; }
        public DateTime FECHA_VENCIMIENTO { get; set; }
        public string DIRECCION { get; set; }
        public bool ESTADO { get; set; }
        public string USR { get; set; }
        public string PWD { get; set; }
        public bool LOGEADO { get; set; }
        public string ENTITYSETNAME { get; set; }
        public string ENTITYID { get; set; }
        public Int64 Codigo_Rol { get; set; }
    }
}
