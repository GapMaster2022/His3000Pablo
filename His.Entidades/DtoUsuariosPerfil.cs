using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoUsuariosPerfil
    {
        public int ID_PERFIL { get; set; }
        public string DESCRIPCION { get; set; }
        public bool TIENE_ACCESO { get; set; }
    }
}
