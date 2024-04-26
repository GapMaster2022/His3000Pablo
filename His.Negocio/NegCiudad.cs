using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegCiudad
    {
        public static Int16 RecuperaMaximoCiudad()
        {
            return new DatCiudad().RecuperaMaximoCiudad();
        }
        public static List<DtoCiudad> RecuperaCiudades()
        {
            return new DatCiudad().RecuperaCiudades();
        }
        public static void CrearCiudad(CIUDAD ciudad)
        {
            new DatCiudad().CrearCiudad(ciudad);
        }
        public static void GrabarCiudad(CIUDAD ciudadModificada, CIUDAD ciudadOriginal)
        {
            new DatCiudad().GrabarCiudad(ciudadModificada, ciudadOriginal);
        }
        public static void EliminarCiudad(CIUDAD ciudad)
        {
            new DatCiudad().EliminarCiudad(ciudad);
        }
        public static List<CIUDAD> ListaCiudades()
        {
            return new DatCiudad().ListaCiudades();
        }
        public static CIUDAD RecuperarCiudadID(int codigoCiudad)
        {
            return new DatCiudad().RecuperarCiudadID(codigoCiudad);
        }
    }
}
