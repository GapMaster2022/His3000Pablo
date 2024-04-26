using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegEtnias
    {
        public static List<ETNIA> ListaEtnias()
        {
            return new DatEtnias().ListaEtnias();
        }
        public static ETNIA RecuperarEtniaID(int codigoEtnia)
        {
            return new DatEtnias().RecuperarEtniaID(codigoEtnia);
        }
        public static List<NACIONALIDAD_ETNIA> ListaNacionalidadEtnica()
        {
            return new DatEtnias().ListaNacionalidadEtnica();
        }
    }
}
