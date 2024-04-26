using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegCatalogoCostos
    {
        public static Int16 RecuperaMaximoCcostos()
        {
            return new DatCatalogosCostos().RecuperaMaximoCcostos();  
            
        }
        public static List<CATALOGO_COSTOS> RecuperaCcostos()
        {

            return new DatCatalogosCostos().RecuperaCcostos(); 
           
        }

        public static void CrearCcostos(CATALOGO_COSTOS ccostos)
        {
            new DatCatalogosCostos().CrearCcostos(ccostos);   
            
        }
        public static void GrabarCcostos(CATALOGO_COSTOS ccostosModificada, CATALOGO_COSTOS ccostosOriginal)
        {
            new DatCatalogosCostos().GrabarCcostos(ccostosModificada, ccostosOriginal);           
        }
        public static void EliminarCcostos(CATALOGO_COSTOS ccostos)
        {
            new DatCatalogosCostos().EliminarCcostos(ccostos);  
            
        }

        public List<CATALOGO_COSTOS> ListaCcostos()
        {
            return new DatCatalogosCostos().ListaCcostos();  
            
        }
        public List<CATALOGO_COSTOS> ListaCcostos(int codCctipo)
        {
            return new DatCatalogosCostos().ListaCcostos(codCctipo);   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigoConvenio"></param>
        /// <returns></returns>
        public static List<CATALOGO_COSTOS_TIPO> RecuperarEstructuraCatalogos()
        {
            return new DatCatalogosCostos().RecuperarEstructuraCatalogos();
        }
    }
}
