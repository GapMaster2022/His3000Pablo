using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegPrecioConvenios
    {
        public static int RecuperaMaximoPconvenios()
        {
            return new DatPreciosConvenios().RecuperaMaximoPconvenios();

        }

        public static List<DtoPreciosConvenios> RecuperaPconvenios()
        {
            return new DatPreciosConvenios().RecuperaPconvenios();
        }

        public static void CrearPconvenios(PRECIOS_POR_CONVENIOS pconvenios)
        {
            new DatPreciosConvenios().CrearPconvenios(pconvenios);
        }

        public static void ActualizaCodigosSic()
        {
            new DatPreciosConvenios().ActualizaCodigosSic();
        }
        //INGRESA EL CODIGO PARA SER DIFERENCIADO EN SEGUROS PABLO ROCHA 21-11-2018
        public static void IngresaCodigo(string cac_nombre, int cat_codigo, int cac_codigo)
        {
            new DatPreciosConvenios().IngresaCodigo(cac_nombre, cat_codigo, cac_codigo);
        }

        public static void GrabarPconvenios(PRECIOS_POR_CONVENIOS pconveniosModificada, PRECIOS_POR_CONVENIOS pconveniosOriginal)
        {
            new DatPreciosConvenios().GrabarPconvenios(pconveniosModificada, pconveniosOriginal);
        }


        public static void EliminarPconvenios(PRECIOS_POR_CONVENIOS pconvenios)
        {
            new DatPreciosConvenios().EliminarPconvenios(pconvenios);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigoConvenio"></param>
        /// <returns></returns>
        public static bool ConPrecios(Int16 codigoConvenio)
        {
            return new DatPreciosConvenios().ConPrecios(codigoConvenio);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigoConvenio"></param>
        /// <returns></returns>
        public static List<PRECIOS_POR_CONVENIOS> RecuperarPreciosPorConvenio(Int16 codigoConvenio)
        {
            return new DatPreciosConvenios().RecuperarPreciosPorConvenio(codigoConvenio);
        }

        /// <summary>
        /// Eliminar los precios de un convenio
        /// </summary>
        /// <param name="codigoConvenio"></param>
        public static void EliminarPreciosPorConvenio(int codigoConvenio)
        {
            new DatPreciosConvenios().EliminarPreciosPorConvenio(codigoConvenio);
        }
        public static bool creaPreciosConvenio(Int64 CAT_CODIGO, List<PRECIOS_POR_CONVENIOS> precon)
        {
            return new DatPreciosConvenios().creaPreciosConvenio(CAT_CODIGO, precon);
        }


    }
}
