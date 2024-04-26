using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegTarifario
    {
        public static List<TARIFARIOS_DETALLE> listaTarifarios()
        {
            return new DatTarifario().listaTarifarios();
        }
        

        public static TARIFARIO_IESS RecuperarTarifarioIess(int codigoT)
        {
            return new DatTarifario().RecuperarTarifarioIess(codigoT);
        }

        public static TARIFARIOS_DETALLE RecuperarTarifarioHono(string codigoH)
        {
            return new DatTarifario().RecuperarTarifarioHono(codigoH);
        }

        public static List<TARIFARIOS_DETALLE> ListaRecuperarTarifarioHono(string codigoH)
        {
            return new DatTarifario().ListaRecuperarTarifarioHono(codigoH);
        }
        /// <summary>
        /// Método
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public static DataTable recuperar_Tarifarios_Cirugia()
        {
            return new DatTarifario().recuperar_Tarifarios_Cirugia();
        }
        public static DataTable recuperar_Tarifarios(string codigo, string descripcion)
        {
            return new DatTarifario().recuperar_Tarifarios(codigo, descripcion);
        }

        public static bool GuardaHonorarioCuentaPaciente(string ateCodigo, string total, string medico, int usuario, DateTime hora)
        {
            return new DatTarifario().GuardaHonorarioCuentaPaciente(ateCodigo, Convert.ToDouble(total), medico, usuario, hora);
        }

        public static DataTable ListaTarifario(string busqueda, bool codigo, bool descripcion)
        {
            return new DatTarifario().Tarifario(busqueda, codigo, descripcion);
        }
        public static DataTable ReporteTarifarioH(int hon_codigo)
        {
            return new DatTarifario().ReporteTarifarioH(hon_codigo);
        }
    }
}
