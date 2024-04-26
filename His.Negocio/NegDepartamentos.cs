using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegDepartamentos
    {
        public static Int16 RecuperaMaximoDepartamento()
        {
            return new DatDepartamentos().RecuperaMaximoDepartamento();
        }
        public static List<DtoDepartamentos> RecuperaDepartamentos()
        {
            return new DatDepartamentos().RecuperaDepartamentos();
        }
        /// <summary>
        /// Metodo que devuelve un objeto de tipo departamento
        /// </summary>
        /// <param name="codigoUsuario">Codigo del usuario</param>
        /// <returns>objeto de tipo departamento</returns>
        public static DEPARTAMENTOS RecuperarDepartamento(int codigoUsuario)
        {
            return new DatDepartamentos().RecuperarDepartamento(codigoUsuario);  
        }
        public static void CrearDepartamento(DEPARTAMENTOS departamento)
        {
            new DatDepartamentos().CrearDepartamento(departamento);
        }
        public static void GrabarDepartamento(DEPARTAMENTOS departamentoModificada, DEPARTAMENTOS departamentoOriginal)
        {
            new DatDepartamentos().GrabarDepartamento(departamentoModificada, departamentoOriginal);
        }
        public static void EliminarDepartamento(DEPARTAMENTOS departamento)
        {
            new DatDepartamentos().EliminarDepartamento(departamento);
        }
        public static List<DEPARTAMENTOS> ListaDepartamentos()
        {
            return new DatDepartamentos().ListaDepartamentos();
        }
    }
}
