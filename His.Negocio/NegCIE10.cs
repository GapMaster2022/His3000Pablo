using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegCIE10
    {
        public static CIE10 RecuperarCIE10(string codigOCIE10)
        {
            return new DatCIE10().RecuperarCIE10(codigOCIE10);
        }
        public static List<CIE10> RecuperarCieFormulario(Int64 ate_codigo)
        {
            return new DatCIE10().RecuperarCie10Formularios(ate_codigo);
        }
    }
}
