using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;


namespace His.Negocio
{
    public class NegPrecios
    {
        public static int RecuperaMaximoPrecios()
        {
            return new DatPreciosPorcentajes().RecuperaMaximoPrecios();
        }

        public static List<PRECIOS_POR_CONVENIOS> RecuperaPrecios()
        {

            return new DatPreciosPorcentajes().RecuperaPrecios();  
        }

        public static List<PRECIOS_POR_CONVENIOS> RecuperaPrecios(Int16 codigoCategoria, Int16 tipo)
        {

            return new DatPreciosPorcentajes().RecuperaPrecios(codigoCategoria,tipo);
        }

        public static void CrearPrecios(PRECIOS_POR_CONVENIOS precios)
        {
            new DatPreciosPorcentajes().CrearPrecios(precios); 
           
        }
        public static void GrabarPrecios(PRECIOS_POR_CONVENIOS preiciosModificada, PRECIOS_POR_CONVENIOS preciosOriginal)
        {
            new DatPreciosPorcentajes().GrabarPrecios(preiciosModificada, preciosOriginal);
        }
        public static void EliminarPrecios(PRECIOS_POR_CONVENIOS precios)
        {
            new DatPreciosPorcentajes().EliminarPrecios(precios);  
        }
        

    }
}
