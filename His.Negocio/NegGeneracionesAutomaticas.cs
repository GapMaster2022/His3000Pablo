using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegGeneracionesAutomaticas
    {
        public static int RecuperaMaximo()
        {
            return new DatGeneracionesAutomaticas().RecuperaMaximo();
        }
        public static List<GENERACIONES_AUTOMATICAS> ListaGeneracionesAutomaticas()
        {
            return new DatGeneracionesAutomaticas().ListaGeneracionesAutomaticas();
        }
        public static void CrearGeneracionAutomatica(GENERACIONES_AUTOMATICAS generaAutom)
        {
            new DatGeneracionesAutomaticas().CrearGeneracionAutomatica(generaAutom);
        }

        public static void EliminaTipoDocumento(Int32 CodigoDocumento)
        {
            new DatGeneracionesAutomaticas().EliminaTipoDocumento(CodigoDocumento);
        }

    }
}
