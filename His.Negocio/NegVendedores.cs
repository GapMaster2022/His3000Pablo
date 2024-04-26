using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using System.Data;
using His.Entidades;

namespace His.Negocio
{
    public class NegVendedores
    {
        
        public static DataTable getReporteVendedor(string condicion)
        {
            return new DatVendedores().getReporteVendedor(condicion);
        }
        public static DataTable getReporteVendedor2(string condicion)
        {
            return new DatVendedores().getReporteVendedor2(condicion);
        }
        public static DataTable getVendedores()
        {
            return new DatVendedores().getVendedores();
        }
        public static DataTable getAteImagen(int x)
        {
            return new DatVendedores().getAteImagen(x);
        }
        public static DataTable getTarjetas()
        {
            return new DatVendedores().getTarjetas();
        }
        public static DataTable getBancos()
        {
            return new DatVendedores().getBancos();
        }
        public static bool existCedVendedore(string ced, string codigo)
        {
            return new DatVendedores().existCedVendedor(ced, codigo);
        }
        public static DataTable getVendedoresS()
        {
            return new DatVendedores().getVendedoresS();
        }
        public static void saveVendedor(vendedor v)
        {
            new DatVendedores().saveVendedor(v);
        }

        public static bool deleteVendedor(string v)
        {
            return new DatVendedores().deleteVendedor(v);
        }

    }
}
