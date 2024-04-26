using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.General;  

namespace His.Negocio
{
    public class NegIess
    {
        public static List<DtoIess>RecuperarTarifario()
        {
            return new DatIess().RecuperarTarifario();
        } 

    }
}
