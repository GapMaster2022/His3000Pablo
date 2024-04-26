using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegLocales
    {
        public static Int16 RecuperaMaximoLocal()
        {
            return new DatLocales().RecuperaMaximoLocal();
        }
        public static List<LOCALES> ListaLocales()
        {
            return new DatLocales().ListaLocales();
        }
        public static List<DtoLocales> RecuperaLocales()
        {
            return new DatLocales().RecuperaLocales();
        }
        public static void CrearLocal(LOCALES datlocal)
        {
            new DatLocales().CrearLocal(datlocal);

        }
        public static void GrabarLocal(LOCALES localModificada, LOCALES localOriginal)
        {
            new DatLocales().GrabarLocal(localModificada, localOriginal);
            
        }
        public static void EliminarLocal(LOCALES datlocal) 
        {
            new DatLocales().EliminarLocal(datlocal);
        }
        public static LOCALES RecuperarLocalID(Int16 codLocal)
        {
            return new DatLocales().RecuperarLocalID(codLocal);
        }
    }
}
