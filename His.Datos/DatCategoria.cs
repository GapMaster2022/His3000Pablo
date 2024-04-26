using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;


namespace His.Datos
{
    public class DatCategoria
    {
        public Int16 RecuperaMaximoCategorias()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<CATEGORIAS_CONVENIOS> categorias = contexto.CATEGORIAS_CONVENIOS.ToList(); 
                if (categorias.Count > 0)
                    maxim = contexto.CATEGORIAS_CONVENIOS.Max(emp => emp.CAT_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }
        }

        public List<CATEGORIAS_CONVENIOS> RecuperaCategorias()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                //contexto.CATEGORIAS_CONVENIOS.Where(a => a.CAT_CODIGO == codigoCategoria).FirstOrDefault()
                
                return contexto.CATEGORIAS_CONVENIOS.Include("ASEGURADORAS_EMPRESAS").ToList();
            }
        }
        public List<CATEGORIAS_CONVENIOS> RecuperaCategorias(Int16 codigoAseguradora)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.CATEGORIAS_CONVENIOS.Include("ASEGURADORAS_EMPRESAS").Where(a=>a.ASEGURADORAS_EMPRESAS.ASE_CODIGO==codigoAseguradora).ToList();
            }
        }

    
    public List<CATEGORIAS_CONVENIOS> RecuperaCategoriasVigente(bool xvigente)
    {
        using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        {
            if(xvigente)
                return contexto.CATEGORIAS_CONVENIOS.Include("ASEGURADORAS_EMPRESAS").Where(a => a.CAT_FECHA_FIN > DateTime.Now && a.CAT_FECHA_INICIO < DateTime.Now).ToList();
            else
                return contexto.CATEGORIAS_CONVENIOS.Include("ASEGURADORAS_EMPRESAS").ToList();
            }
    }

    /// <summary>
    /// Metodo que recupera una Categoria por ID
    /// </summary>
    /// <param name="codigoCategoria">id de la Categoria</param>
    /// <returns>objeto CATEGORIAS_CONVENIOS</returns>
    public CATEGORIAS_CONVENIOS RecuperaCategoriaID(Int16 codigoCategoria)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.CATEGORIAS_CONVENIOS.Where(a => a.CAT_CODIGO == codigoCategoria).FirstOrDefault();
                }
            }
            catch (Exception err) { throw err; } 
        }
        public void CrearCategorias(CATEGORIAS_CONVENIOS categorias)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("CATEGORIAS_CONVENIOS", categorias);
                contexto.SaveChanges();
            }
        }
        public void GrabarCategorias(CATEGORIAS_CONVENIOS categoriasModificada, CATEGORIAS_CONVENIOS categoriasOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                CATEGORIAS_CONVENIOS convenios = contexto.CATEGORIAS_CONVENIOS.FirstOrDefault(C => C.CAT_CODIGO == categoriasModificada.CAT_CODIGO);
                convenios.CAT_NOMBRE = categoriasModificada.CAT_NOMBRE;
                convenios.CAT_DESCRIPCION = categoriasModificada.CAT_DESCRIPCION;
                convenios.ASEGURADORAS_EMPRESASReference.EntityKey = categoriasModificada.ASEGURADORAS_EMPRESASReference.EntityKey;
                convenios.CAT_FECHA_INICIO = categoriasModificada.CAT_FECHA_INICIO;
                convenios.CAT_FECHA_FIN = categoriasModificada.CAT_FECHA_FIN;
                convenios.CAT_TIPO_PRECIO = categoriasModificada.CAT_TIPO_PRECIO;
                convenios.CAT_POR_DESCUENTO = categoriasModificada.CAT_POR_DESCUENTO;
                contexto.SaveChanges(); 
                //5contexto.Grabar(categoriasModificada, categoriasOriginal);
            }
        }

        
        public void EliminarCategorias(CATEGORIAS_CONVENIOS categorias)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                CATEGORIAS_CONVENIOS cconvenios = contexto.CATEGORIAS_CONVENIOS.FirstOrDefault(c => c.CAT_CODIGO == categorias.CAT_CODIGO);
                contexto.DeleteObject(cconvenios);
                contexto.SaveChanges();
            }
        }

       
        public List<CATEGORIAS_CONVENIOS> ListaCategorias()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from c in contexto.CATEGORIAS_CONVENIOS
                        //join e in contexto.ASEGURADORAS_EMPRESAS on c.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals e.ASE_CODIGO
                        orderby c.CAT_NOMBRE
                        select c).ToList();
            }
        }

        public List<CATEGORIAS_CONVENIOS> ListaCategorias(int codAseguradora)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from c in contexto.CATEGORIAS_CONVENIOS
                        join e in contexto.ASEGURADORAS_EMPRESAS on c.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals e.ASE_CODIGO
                        where c.ASEGURADORAS_EMPRESAS.ASE_CODIGO==codAseguradora
                        select c).ToList();
            }
        }

        public List<CATEGORIAS_CONVENIOS> ListaCategorias(TIPO_EMPRESA tipoEmpresa)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from c in contexto.CATEGORIAS_CONVENIOS 
                        join e in contexto.ASEGURADORAS_EMPRESAS on c.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals e.ASE_CODIGO
                        where c.ASEGURADORAS_EMPRESAS.TIPO_EMPRESA.TE_CODIGO == tipoEmpresa.TE_CODIGO
                        && c.CAT_FECHA_INICIO < DateTime.Now && c.CAT_FECHA_FIN > DateTime.Now
                        orderby c.CAT_NOMBRE ascending
                        select c).ToList();
            }
        }

        /// <summary>
        /// Metodo que devuelve el listado de Convenios y Categorias que poseen precios y porcentajes 
        /// </summary>
        /// <returns>Lista de Objetos CATEGORIAS_CONVENIOS</returns>
        public List<CATEGORIAS_CONVENIOS> ListaCategoriasConPrecios(Int16 codigoTipo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                return (from p in contexto.PRECIOS_POR_CONVENIOS
                        join c in contexto.CATEGORIAS_CONVENIOS on p.CATEGORIAS_CONVENIOS.CAT_CODIGO equals c.CAT_CODIGO
                        join a in contexto.ASEGURADORAS_EMPRESAS on c.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
                        where a.TIPO_EMPRESA.TE_CODIGO == codigoTipo 
                        orderby c.CAT_NOMBRE
                        select c).Distinct().ToList();

                //return  (from c in contexto.CATEGORIAS_CONVENIOS
                //                            where (from p in contexto.PRECIOS_POR_CONVENIOS
                //                                    select p.CATEGORIAS_CONVENIOS.CAT_CODIGO).Contains(c.CAT_CODIGO)
                //                            select c).ToList() ;
            }
        }

        /// <summary>
        /// Metodo que devuelve el listado de Convenios y Categorias que no poseen precios y porcentajes 
        /// </summary>
        /// <returns>Lista de Objetos CATEGORIAS_CONVENIOS</returns>
        public List<CATEGORIAS_CONVENIOS> ListaCategoriasSinPrecios(Int16 codigoTipo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var ConPrecios =(from p in contexto.PRECIOS_POR_CONVENIOS
                        join c in contexto.CATEGORIAS_CONVENIOS on p.CATEGORIAS_CONVENIOS.CAT_CODIGO equals c.CAT_CODIGO
                        join a in contexto.ASEGURADORAS_EMPRESAS on c.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
                        where a.TIPO_EMPRESA.TE_CODIGO == codigoTipo
                        select c).Distinct().ToList();

                var Todas = (from c in contexto.CATEGORIAS_CONVENIOS
                             join a in contexto.ASEGURADORAS_EMPRESAS on c.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
                             where a.TIPO_EMPRESA.TE_CODIGO == codigoTipo
                             select c).Distinct().ToList();

                var categoriasSinPrecios =Todas.Except(ConPrecios).ToList();
                return categoriasSinPrecios.OrderBy(c=>c.CAT_NOMBRE ).ToList() ;  
            }
        }

    }
}
