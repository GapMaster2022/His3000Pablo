using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public static class  NegEmpresa
    {
        public static Int16 RecuperaMaximoEmpresa()
        {
            return new DatEmpresa().RecuperaMaximoEmpresa();
        }
        public static EMPRESA RecuperaEmpresa()
        {
            try
            {
                return new DatEmpresa().RecuperaEmpresa();
            }
            catch (Exception err) { throw err; } 
        }
        public static List<EMPRESA> RecuperaEmpresas()
        {
            return new DatEmpresa().RecuperaEmpresas();
        }
        public static void CrearEmpresa(EMPRESA empresa)
        {
            new DatEmpresa().CrearEmpresa(empresa);
        }
        public static void GrabarEmpresa(EMPRESA empresaModificada, EMPRESA empresaOriginal)
        {
            new DatEmpresa().GrabarEmpresa(empresaModificada,empresaOriginal);
        }
        public static void EliminarEmpresa(EMPRESA empresa)
        {
            new DatEmpresa().EliminarEmpresa(empresa);
        }

        public static SUCURSALES RecuperaEmpresaID(int codigo)
        {
            return new DatEmpresa().RecuperarEmpresaID(codigo);
        }
    }
}
