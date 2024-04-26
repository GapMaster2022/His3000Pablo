using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegHonorarios
    {

        #region Afectaciones
        /// <summary>
        /// Inserta usuarios
        /// </summary>
        /// <param name="procedimiento">Nombre del procedimiento</param>
        /// <param name="usuario">Datos del ususario</param>
        //public void InsertaMedicos(string procedimietno, Medicos medicos) 
        //{
        //    His.Datos.DatHonorarios datCliente = new His.Datos.DatHonorarios();
        //    datCliente.InsertaMedicos(procedimietno, medicos);
        //}
        /// <summary>
        /// Modifica usuarios
        /// </summary>
        /// <param name="procedimiento">Nombre del procedimiento</param>
        /// <param name="usuario">Datos del ususario</param>
        //public void ModificaMedicos(string procedimietno, Medicos medicos)
        //{
        //    His.Datos.DatHonorarios datCliente = new His.Datos.DatHonorarios();
        //    datCliente.ModificaMedicos(procedimietno, medicos);
        //}
        #endregion
        public static List<DtoHCEX> listaHCEX()
        {
            return new DatHonorarios().listaHCEX();
        }
        public static bool insertarHCEX(HONORARIOS_CONSULTA_EXTERNA hcex)
        {
            return new DatHonorarios().insertarHCEX(hcex);
        }
        public static bool eliminarHCEX(string PRO_CODIGO)
        {
            return new DatHonorarios().eliminarHCEX(PRO_CODIGO);
        }
        public static HONORARIOS_CONSULTA_EXTERNA existeProductoHonorarrio(string PRO_CODIGO)
        {
            return new DatHonorarios().existeProductoHonorarrio(PRO_CODIGO);
        }
    }
}
