using System.Collections.Generic;
using His.Entidades;
using His.Datos;
using System.Data;
using System;

namespace His.Negocio
{
    public  class NegRubros
    {
      public static int RecuperaMaximoRubro()
      {
          return new DatRubros().RecuperaMaximoRubro();
      }

      public static List<RUBROS> recuperarRubros()
      {
          return new DatRubros().recuperarRubros();
      }

      public static List<RUBROS> recuperarRubros(int codPedidoArea)
      {
          return new DatRubros().recuperarRubros(codPedidoArea);
      }

      public static bool ParametroServicios()
        {
            return new DatRubros().ParametroServicios();
        }
      public static bool ParametroGarantia()
        {
            return new DatRubros().ParametroGarantia();
        }
        public static DataTable getDepartamento()
        {
            return new DatRubros().getDepartamentos();
        }
        public static DataTable getCuentas(DateTime desde, DateTime hasta, bool ingreso, bool alta, string hc, bool cero)
        {
            return new DatRubros().getCuentas(desde, hasta, ingreso, alta, hc, cero);
        }
    }
}
