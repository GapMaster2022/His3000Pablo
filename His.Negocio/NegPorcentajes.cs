using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;


namespace His.Negocio
{
    public class NegPorcentajes
    {
        public static List<PORCENTAJES> RecuperaPorcentaje()
        {
            return new DatPorcentajes().RecuperaPorcentajes();
        }
    }
}
