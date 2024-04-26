using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Datos
{
    public class DtoExplotadorUsuariosHis
    {
        public int ID { get; set; }
        public string USUARIO { get; set; }
        public string USR { get; set; }
        public string MODULO { get; set; }
        public string PERFIL { get; set; }
        public int ID_ACCESO { get; set; }
        public string ACCESO { get; set; }
        public bool ESTADO { get; set; }
        public DateTime FECHA_CADUCIDAD { get; set; }

    }
}
