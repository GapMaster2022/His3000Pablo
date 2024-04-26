using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;

namespace His.Negocio
{
    public class NegEmail
    {
        public static void EnviarCorreo(int cat_codigo, DateTime fechacaducar, string cat_nombre, bool enviar)
        {
            new DatMaintenance().EnviarCorreo(cat_codigo, fechacaducar, cat_nombre, enviar);
        }
    }
}
