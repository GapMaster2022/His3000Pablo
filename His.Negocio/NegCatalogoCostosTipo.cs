using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegCatalogoCostosTipo
    {
        public static Int16 RecuperaMaximoCctipo()
        {
            return new DatCatalogosCostosTipo().RecuperaMaximoCctipo();  
        }
        
        public static List<CATALOGO_COSTOS_TIPO> RecuperaCctipo()
        {
            return new DatCatalogosCostosTipo().RecuperaCctipo(); 
        }
        
        public static void CrearCctipo(CATALOGO_COSTOS_TIPO cctipo)
        {
            new DatCatalogosCostosTipo().CrearCctipo(cctipo);  
            
        }
        
        public static void GrabarCctipo(CATALOGO_COSTOS_TIPO CctipoModificada, CATALOGO_COSTOS_TIPO  CctipoOriginal)
        {
            new DatCatalogosCostosTipo().GrabarCctipo(CctipoModificada, CctipoOriginal);                
        }
        
        public static void EliminarCctipo(CATALOGO_COSTOS_TIPO Cctipo)
        {
            new DatCatalogosCostosTipo().EliminarCctipo(Cctipo);             
        }

    }
}
