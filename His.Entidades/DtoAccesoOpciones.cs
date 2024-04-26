using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoAccesoOpciones
    {
        public int ID_ACCESO { get; set; }
        public string DESCRIPCION { get; set; }
        public Int16 ID_MODULO { get; set; }
        public string DESCRIPCIONMod { get; set; }
        public string ENTITYSETNAME { get; set; }
        public string ENTITYID { get; set; }
    }
}
