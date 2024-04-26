using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegCategorias
    {
        public static Int16 RecuperaMaximoCategorias()
        {
            return new DatCategoria().RecuperaMaximoCategorias ();  
        }
        
        public static List<CATEGORIAS_CONVENIOS> RecuperaCategorias()
        {

            return new DatCategoria().RecuperaCategorias();
        }
        /// <summary>
        /// Metodo que recupera una Categoria por ID
        /// </summary>
        /// <param name="codigoCategoria">id de la Categoria</param>
        /// <returns>objeto CATEGORIAS_CONVENIOS</returns>
        public static CATEGORIAS_CONVENIOS RecuperaCategoriaID(Int16 codigoCategoria)
        {
            try
            {
                return new DatCategoria().RecuperaCategoriaID(codigoCategoria);
            }
            catch (Exception err) { throw err; } 
        }

        public static List<CATEGORIAS_CONVENIOS> RecuperaCategorias(Int16 codigoAseguradora)
        {
            return new DatCategoria().RecuperaCategorias(codigoAseguradora );
        }
        public static List<CATEGORIAS_CONVENIOS> RecuperaCategoriasVigente(bool codigoAseguradora)
        {
            return new DatCategoria().RecuperaCategoriasVigente(codigoAseguradora);
        }
        


        public static void CrearCategoria(CATEGORIAS_CONVENIOS categorias)
        {
            new DatCategoria().CrearCategorias(categorias);
        }
        public static void GrabarCategorias(CATEGORIAS_CONVENIOS categoriasModificada, CATEGORIAS_CONVENIOS categoriasOriginal)
        {
            new DatCategoria().GrabarCategorias(categoriasModificada, categoriasOriginal);
        }
        public static void EliminarCategorias(CATEGORIAS_CONVENIOS categorias)
        {
            new DatCategoria().EliminarCategorias(categorias);
        }
        public static List<CATEGORIAS_CONVENIOS> ListaCategorias()
        {
            return new DatCategoria().ListaCategorias();
        }
        public static List<CATEGORIAS_CONVENIOS> ListaCategorias(int codAseg)
        {
            return new DatCategoria().ListaCategorias(codAseg);
        }
        public static List<CATEGORIAS_CONVENIOS> ListaCategorias(TIPO_EMPRESA tipoEmpresa)
        {
            return new DatCategoria().ListaCategorias(tipoEmpresa);
        }
        /// <summary>
        /// Metodo que devuelve el listado de Convenios y Categorias que poseen precios y porcentajes 
        /// </summary>
        /// <returns>Lista de Objetos CATEGORIAS_CONVENIOS</returns>
        public static List<CATEGORIAS_CONVENIOS> ListaCategoriasConPrecios(Int16 codigoTipo)
        {
            return new DatCategoria().ListaCategoriasConPrecios(codigoTipo); 
        }

        /// <summary>
        /// Metodo que devuelve el listado de Convenios y Categorias que no poseen precios y porcentajes 
        /// </summary>
        /// <returns>Lista de Objetos CATEGORIAS_CONVENIOS</returns>
        public static List<CATEGORIAS_CONVENIOS> ListaCategoriasSinPrecios(Int16 codigoTipo)
        {
            return new DatCategoria().ListaCategoriasSinPrecios(codigoTipo); 
        }
    }
}
