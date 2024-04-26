using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;
namespace His.Negocio
{
    public class NegAtencionesFormasLlegada
    {
        public static List<ATENCION_FORMAS_LLEGADA> listaAtencionesFormasLlegada()
        {
            return new DatAtencionesFormasLlegada().listaAtencionesFormasLlegada();
        }

        /// <summary>
        /// Metodo que devuelve una forma de llegada ebnviando como parametro el codigo
        /// </summary>
        /// <param name="codigoFormaLlegada">codigo de la forma de llegada</param>
        /// <returns>devuelve un objeto de ATENCION_FORMAS_LLEGADA</returns>
        public static ATENCION_FORMAS_LLEGADA atencionesFormasLlegadaPorID(Int16 codigoFormaLlegada)
        {
            try
            {
                return new DatAtencionesFormasLlegada().atencionesFormasLlegadaPorID(codigoFormaLlegada);
            }
            catch (Exception err) { throw err; } 
        }
        /// <summary>
        /// Metodo que devuelve una forma de llegada ebnviando como parametro el codigo
        /// </summary>
        /// <param name="codigoAtencion">codigo de la forma de llegada</param>
        /// <returns>devuelve un objeto de ATENCION_FORMAS_LLEGADA</returns>
        public static ATENCION_FORMAS_LLEGADA atencionesFormasLlegadaPorAtencion(int codigoAtencion)
        {
            try
            {
                return new DatAtencionesFormasLlegada().atencionesFormasLlegadaPorAtencion(codigoAtencion);
            }
            catch (Exception err) { throw err; }
        }

        public static void actualizar(ATENCION_FORMAS_LLEGADA atencionFormaLLegada)
        {
            try
            {
                new DatAtencionesFormasLlegada().actualizar(atencionFormaLLegada);
            }
            catch (Exception err) { throw err; }

        }

        public static void eliminar(ATENCION_FORMAS_LLEGADA atencionFormaLLegada)
        {
            try
            {
                new DatAtencionesFormasLlegada().eliminar(atencionFormaLLegada);
            }
            catch (Exception err) { throw err; }

        }
    }
}
