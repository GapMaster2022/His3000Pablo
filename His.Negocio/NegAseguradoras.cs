using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using System.Data;

namespace His.Negocio
{
    public class NegAseguradoras
    {
        public static List<ASEGURADORAS_EMPRESAS> ListaEmpresas(bool convenio)
        {
            return new DatAseguradoras().ListaEmpresas(convenio);
        }
        public static List<ASEGURADORAS_EMPRESAS> ListaEmpresas()
        {
            return new DatAseguradoras().ListaEmpresas();
        }
        public static List<TIPO_EMPRESA> RecuperaTipoEmpresa()
        {
            return new DatAseguradoras().RecuperaTipoEmpresa();  
            
        }
        public static List<ASEGURADORAS_EMPRESAS> RecuperarListaEmpresas(Int16 codigoTipoEmpresa)
        {
            return new DatAseguradoras().RecuperarListaEmpresas(codigoTipoEmpresa); 
        }
        public static ASEGURADORAS_EMPRESAS RecuperaEmpresa(Int16 codigoTipoEmpresa)
        {
            return new DatAseguradoras().RecuperaEmpresa(codigoTipoEmpresa);
        }
        public static ASEGURADORAS_EMPRESAS RecuperarEmpresa(string RUC)
        {
            return new DatAseguradoras().RecuperarEmpresa(RUC);
        }

        public static void actualizarConvenio(ASEGURADORAS_EMPRESAS aseguradora, bool estado)
        {
            new DatAseguradoras().actualizarConvenio(aseguradora,estado);
        }

        public static Int16 UltimoCodigoAseguradoras()
        {
            return new DatAseguradoras().RecuperaMaximoCodigoEmpresas();
        }
        public static void Crear(ASEGURADORAS_EMPRESAS aseguradora)
        {
            new DatAseguradoras().Crear(aseguradora);
        }
        public static void ModificarAseguradora(ASEGURADORAS_EMPRESAS aseguradora)
        {
            new DatAseguradoras().ModificarAseguradora(aseguradora);
        }
        public static List<TIPO_EMPRESA> ListaTiposEmpresa()
        {
            return new DatAseguradoras().ListaTiposEmpresa();
        }
       
        public static ASEGURADORAS_EMPRESAS recuperaAseguradoraPorAtencion(Int64 codAtencion)
        {
            return new DatAseguradoras().recuperaAseguradoraPorAtencion(codAtencion);
        }
       
    }
}
