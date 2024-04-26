using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegTipoReferido
    {
        public static List<TIPO_REFERIDO> listaTipoReferido()
        {
            return new DatTipoReferido().listaTipoReferido();
        }
        public static TIPO_REFERIDO RecuperarReferido(int codigo)
        {
            return new DatTipoReferido().RecuperarTipoReferido(codigo);
        }
    }
}
