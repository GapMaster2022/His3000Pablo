using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;

namespace His.Negocio
{
    public class NegCg3000
    {
        public static bool AnularAD(Int64 hom_codigo)
        {
            return new ControllerCgMaestro().AnularAD(hom_codigo);
        }
    }
}
