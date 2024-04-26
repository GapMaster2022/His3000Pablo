using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoPreAtencion
    {
        public string HC { get; set; }
        public string ATENCION { get; set; }
        public string PACIENTE { get; set; }
        public string IDENTIFICACION { get; set; }
        public string EMAIL { get; set; }
        public string MEDICO { get; set; }
        public string USUARIO { get; set; }
        public string FECHA_PREINGRESO { get; set; }
        public string FECHA_CAMBIO_ATENCION { get; set; }
        public string TIEMPO_CAMBIO_ATENCION { get; set; }
        public string PRIORIDAD { get; set; }
        public string TIPO_REFERIDO { get; set; }
        public string TIPO_TRATAMIENTO { get; set; }
        public string SEGURO_MEDICO { get; set; }
        public string PROCEDIMIENTO { get; set; }
        public string DIRECCION { get; set; }
        public string CELULAR{ get; set; }
        public bool ESTADO { get; set; }
    }
}
