using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegEstadoCivil
    {
        public static List<ESTADO_CIVIL> RecuperaEstadoCivil()
        {
            return new DatEstadoCivil().RecuperaEstadoCivil();
        }
        public static ESTADO_CIVIL RecuperarEstadoCivilID(int codigoEstadoCivil)
        {
            return new DatEstadoCivil().RecuperaEstadoCivilID(codigoEstadoCivil);
        }
    }
}
