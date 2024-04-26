using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegZonas
    {
        public static List<DtoZonas> RecuperaZonasFormulario()
        {
            return new DatZonas().RecuperaZonasFormulario();
        }
        public static Int16 RecuperaMaximoZona()
        {
            return new DatZonas().RecuperaMaximoZona();
        }
        public static List<ZONAS> RecuperaZonas()
        {
            return new DatZonas().RecuperaZonas();
        }
        public static void CrearZona(ZONAS zona)
        {
            new DatZonas().CrearZona(zona);
            
        }
        public static void GrabarZona(ZONAS zonaModificada, ZONAS zonaOriginal)
        {
            new DatZonas().GrabarZona(zonaModificada, zonaOriginal);
           
        }
        public static void EliminarZona(ZONAS zona)
        {
            new DatZonas().EliminarZona(zona);
        }
    }
}
