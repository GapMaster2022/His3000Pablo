using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;
using System.Data;

namespace His.Negocio
{
    public class NegParametros
    {
        public static List<DtoParametros> RecuperaParametros(Int16 codigopar)
        {
            return new DatParametros().RecuperaParametros(codigopar);
        }
        public static PARAMETROS_DETALLE RecuperaPorCodigo(int codigo)
        {
            return new DatParametros().RecuperaPorCodigo(codigo);
        }
        public static FORMULARIOS_HCU RecuperaFechaPorCodigo(int codigo)
        {
            return new DatParametros().RecuperaFechaPorCodigo(codigo);
        }
        public static DataTable RecuepraHorasyLitros(int hora)
        {
            return new DatParametros().RecuepraHorasyLitros(hora);
        }

        public static bool EditarEvolucion()
        {
            return new DatParametros().EDITAEVOLUCION();
        }
        public static bool ParametroAdmisionDesactivacion()
        {
            return new DatParametros().ParametroAdmisionDesactivacion();
        }
        public static double ParametroIva()
        {
            return new DatParametros().ParametroIva();
        }
        public static decimal ProductoPagaIVA(Int64 codPro)
        {
            return new DatParametros().ProductoPagaIVA(codPro);
        }

        public static bool ParametroDevolucionBienes()
        {
            return new DatParametros().ParametroDevolucionBienes();
        }
        public static bool ParametroComboQuirofano()
        {
            return new DatParametros().ParametroComboQuirofano();
        }
        public static double ParametroBodegaQuirofano()
        {
            return new DatParametros().ParametroBodegaQuirofano();
        }
        public static DataTable ParametroHabitacionDefault()
        {
            return new DatParametros().ParametroHabitacionDefault();
        }
        public static bool ParametroAdmisionAcceso()
        {
            return new DatParametros().ParametroAdmisionAcceso();
        }
        public static bool ParametroReceta()
        {
            return new DatParametros().ParametroReceta();
        }

        public static bool ParametroFacturaHisSic()
        {
            return new DatParametros().ParametroFacturaHisSic();
        }
        public static bool ParametroFormularios()
        {
            return new DatParametros().ParametroFormularios();
        }
        public static int ParametroBodegaGastro()
        {
            return new DatParametros().ParametroBodegaGastro();
        }
        public static bool ParametroFacturaFecha()
        {
            return new DatParametros().ParametroFacturaFecha();
        }
        public static bool ParametroReposicionesBienes()
        {
            return new DatParametros().ParametroReposicionesBienes();
        }
        public static Int64 RecuperaValorParSvXcodigo(int codigo)
        {
            return new DatParametros().RecuperaValorParSvXcodigo(codigo);
        }
        public static LOGOS_EMPRESA logosEmpresa(Int64 LEM_CODIGO)
        {
            return new DatParametros().logosEmpresa(LEM_CODIGO);
        }
        public static TABLA17sri tablaIva()
        {
            return new DatParametros().tablaIva();
        }
    }
}
