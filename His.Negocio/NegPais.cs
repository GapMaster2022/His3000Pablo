using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegPais
    {
        public static Int16 RecuperaMaximoPais()
        {
            return new DatPais().RecuperaMaximoPais();
        }
        public static List<PAIS> RecuperaPaises()
        {
            return new DatPais().RecuperaPaises();
        }
        public static void CrearPais(PAIS pais)
        {
            new DatPais().CrearPais(pais);

        }
        public static void GrabarPais(PAIS paisModificada, PAIS paisOriginal)
        {
            new DatPais().GrabarPais(paisModificada, paisOriginal);

        }
        public static void EliminarPais(PAIS pais)
        {
            new DatPais().EliminarPais(pais);
        }
        public static PAIS RecuperarPaisID(int codigoPais)
        {
            return new DatPais().RecuperarPaisID(codigoPais);
        }
        public static List<PAIS> ListaNacionalidades()
        {
            return new DatPais().ListaNacionalidades();
        }
    }
}
