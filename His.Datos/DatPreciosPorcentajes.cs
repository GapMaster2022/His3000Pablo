using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;


namespace His.Datos
{
    public class DatPreciosPorcentajes
    {
        public int RecuperaMaximoPrecios()
        {
            int maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PRECIOS_POR_CONVENIOS> precios = contexto.PRECIOS_POR_CONVENIOS.ToList();
                if (precios.Count > 0)
                    maxim = contexto.PRECIOS_POR_CONVENIOS.Max(emp => emp.PRE_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }

        }
        public List<PRECIOS_POR_CONVENIOS> RecuperaPrecios()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.PRECIOS_POR_CONVENIOS.ToList();
            }
        }
        /// <summary>
        /// Metodo que recupera precios y porcentajes por categoria y tipo
        /// </summary>
        /// <param name="codigoCategoria">Codigo categoria</param>
        /// <param name="tipo">tipo de precio y porcentaje</param>
        /// <returns>Lista de precios y porcentajes</returns>
        public List<PRECIOS_POR_CONVENIOS> RecuperaPrecios(Int16 codigoCategoria,Int16 tipo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var precios = from p in contexto.PRECIOS_POR_CONVENIOS
                              join c in contexto.CATEGORIAS_CONVENIOS on p.CATEGORIAS_CONVENIOS.CAT_CODIGO equals c.CAT_CODIGO
                              where p.PRE_CODIGO == tipo 
                              select p;
                return contexto.PRECIOS_POR_CONVENIOS.ToList();
            }
        }
        public void CrearPrecios(PRECIOS_POR_CONVENIOS  precios)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("PRECIOS_POR_CONVENIOS", precios);

            }
        }
        public void GrabarPrecios(PRECIOS_POR_CONVENIOS preciosModificada, PRECIOS_POR_CONVENIOS  preciosOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(preciosModificada, preciosOriginal);
            }
        }
        public void EliminarPrecios(PRECIOS_POR_CONVENIOS precios)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(precios);
            }
        }
    
    }
}
