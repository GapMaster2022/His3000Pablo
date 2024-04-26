using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegDivisionPolitica
    {
        public static List<DIVISION_POLITICA> listaDivisionPolitica()
        {
            return new DatDivisionPolitica().listaDivisionPolitica();
        }
        public static List<CLASE_LOCALIDAD> listaClasesLocalidad()
        {
            return new DatDivisionPolitica().listaClasesLocalidad();
        }
        public static List<DIVISION_POLITICA> listaDivisionPolitica(string codClase)
        {
            return new DatDivisionPolitica().listaDivisionPolitica(codClase);
        }
        public static List<DIVISION_POLITICA> RecuperarDivisionPolitica(string codPadre)
        {
            return new DatDivisionPolitica().RecuperarDivisionPolitica(codPadre);
        }
        public static DIVISION_POLITICA DivisionPolitica(string codigo)
        {
            return new DatDivisionPolitica().DivisionPolitica(codigo);
        }
        public static List<TIPO_LOCALIDAD> tiposLocalidad()
        {
            return new DatDivisionPolitica().tiposLocalidad();
        }
        public static List<CLASE_LOCALIDAD> clasesLocalidad()
        {
            return new DatDivisionPolitica().clasesLocalidad();
        }
        public static CLASE_LOCALIDAD claseLocalidad(int codDivPolitica)
        {
            return new DatDivisionPolitica().claseLocalidad(codDivPolitica);
        }
        public static TIPO_LOCALIDAD tipoLocalidad(int codDivPolitica)
        {
            return new DatDivisionPolitica().tipoLocalidad(codDivPolitica);
        }
        public static void EditarDivisionPolitica(DIVISION_POLITICA dpModificada)
        {
            new DatDivisionPolitica().EditarDivisionPolitica(dpModificada);
        }

        public static void CrearDivisionPolitica(DIVISION_POLITICA dpNueva)
        {
            new DatDivisionPolitica().CrearDivisionPolitica(dpNueva);
        }

        public static int maxCodigo()
        {
            return new DatDivisionPolitica().maxCodigo();
        }
        public static void EliminarDivisionPolitica(string codInec)
        {
            new DatDivisionPolitica().EliminarDivisionPolitica(codInec);
        }
    }
}
