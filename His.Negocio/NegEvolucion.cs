using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using System.Data;

namespace His.Negocio
{
    public class NegEvolucion
    {
        public static void crearEvolucion(HC_EVOLUCION nuevaEvolucion)
        {
            new DatEvolucion().crearEvolucion(nuevaEvolucion);
        }

        public static HC_EVOLUCION recuperarEvolucionPorAtencion(Int64 codAtencion)
        {
            return new DatEvolucion().recuperarEvolucionPorAtencion(codAtencion);
        }

        public static DataTable VerificaDepartamento (int usuario)
        {
            return new DatEvolucion().VerificaDepartamento(usuario);
        }

        public static int ultimoCodigo()
        {
            return new DatEvolucion().ultimoCodigo();
        }

        public static DataTable Dietetica(string historiaClinica)
        {
            return new DatEvolucion().Dietetica(historiaClinica);
        }
    }
}