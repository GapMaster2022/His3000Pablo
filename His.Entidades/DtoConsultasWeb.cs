using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoConsultasWeb
    {
        public DateTime FECHA { get; set; }
        public string USUARIO { get; set; }
        public string TIPO_CONSULTA { get; set; }
        public string IP_MAQUINA { get; set; }
        public string DESCRIPCION { get; set; }
    }
}
