using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegPreAtencion
    {
        public static int recuperaMaximoPreAtencion()
        {
            return new DatPreAtencion().recuperaMaximoPreAtencion();
        }
        public static void crearPreAtencion(PREATENCIONES preAtencion)
        {
            new DatPreAtencion().crearPreAtencion(preAtencion);
        }

        public static PREATENCIONES recuperarPreAtencion(int numAtencion)
        {
            return new DatPreAtencion().recuperaPreAtencion(numAtencion);
        }

        public static void editarPreAtencion(PREATENCIONES preAtencionModificada)
        {
            new DatPreAtencion().editarPreAtencion(preAtencionModificada);
        }

    }
}
