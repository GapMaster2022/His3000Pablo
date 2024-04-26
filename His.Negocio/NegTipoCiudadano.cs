using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegTipoCiudadano
    {
        public static TIPO_CIUDADANO RecuperarTipoCiudadanoID(int codigoTipoCiudadano)
        {
            return new DatTipoCiudadano().RecuperarTipoCiudadanoID(codigoTipoCiudadano);
        }
        public static List<TIPO_CIUDADANO> listaTiposCiudadano()
        {
            return new DatTipoCiudadano().listaTiposCiudadano();
        }
    }

}
