using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegBancos
    {
        public static Int16 RecuperaMaximoBanco()
        {
            return new DatBancos().RecuperaMaximoBanco();
        }
        public static List<BANCOS> RecuperaBancos()
        {
            return new DatBancos().RecuperaBancos();
        }
        public static void CrearBanco(BANCOS banco)
        {
            new DatBancos().CrearBanco(banco);
        }
        public static void GrabarBanco(BANCOS bancoModificada, BANCOS bancoOriginal)
        {
            new DatBancos().GrabarBanco(bancoModificada, bancoOriginal);
        }
        public static void EliminarBanco(BANCOS banco)
        {
            new DatBancos().EliminarBanco(banco);
        }

    }
}
