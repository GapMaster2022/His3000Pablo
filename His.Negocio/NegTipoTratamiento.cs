using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegTipoTratamiento
    {
        public static List<TIPO_TRATAMIENTO> RecuperaTipoTratamiento()
        {
            return new DatTipoTratamiento().RecuperaTipoTratamiento();
        }
        public static TIPO_TRATAMIENTO recuperaTipoTratamiento(short codigo)
        {
            return new DatTipoTratamiento().recuperaTipoTratamiento(codigo);
        }
    }
}
