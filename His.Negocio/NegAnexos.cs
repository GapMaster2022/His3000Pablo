using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegAnexos
    {
        public static ANEXOS_IESS RecuperarAnexos(int codidoAnexo)
        {
            return new DatAnexos().RecuperarAnexos(codidoAnexo);
        }

        public static List<ANEXOS_IESS> RecuperarListaAnexos(string codidoAnexo)
        {
            return new DatAnexos().RecuperarListaAnexos(codidoAnexo);
        }
    }
}
