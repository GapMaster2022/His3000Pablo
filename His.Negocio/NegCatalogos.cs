using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using System.Data;

namespace His.Negocio
{
    public class NegCatalogos
    {
        #region catalogos Historia Clinica
        public static int RecuperaMaximoCatalogo()
        {
            //return new DatTipoMedico().RecuperaMaximoTipoMedico();
            return new DatCatalogos().RecuperaMaximoCatalogo();
        }
        public static List<DtoCatalogos> RecuperarCatalogosPorTipo(int codTipo)
        {
            try
            {
                return new DatCatalogos().RecuperarCatalogosPorTipo(codTipo);
            }
            catch (Exception err) { throw err; } 
        }

        public static List<HC_CATALOGOS> RecuperarHcCatalogosPorTipo(int codTipo)
        {
            return new DatCatalogos().RecuperarHcCatalogosPorTipo(codTipo);
        }

        public static List<HC_CATALOGOS> RecuperarHcCatalogosPorTipoSubnivel(int codTipo)
        {
            return new DatCatalogos().RecuperarHcCatalogosPorTipoSubnivel(codTipo);
        }

        /// <summary>
        /// Metodo que recupera un objeto de TIPO CATALOGO
        /// </summary>
        /// <param name="codTipo">codigo del tipo de objeto a recuperar</param>
        /// <returns>objeto HC_CATALOGOS_TIPO</returns>
        public static HC_CATALOGOS_TIPO RecuperarHcCatalogoTipo(int codTipo)
        {
            try
            {
                return new DatCatalogos().RecuperarHcCatalogoTipo(codTipo);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public static List<HC_CATALOGOS_TIPO> ListaCatalogos()
        {
            return new DatCatalogos().ListaCatalogos();
        }

        public static List<HC_CATALOGOS> recuperarCatalogos()
        {
            return new DatCatalogos().RecuperaCatalogos();
        }

        public static List<HC_CATALOGOS> listaCatalogos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from c in contexto.HC_CATALOGOS
                        select c).ToList();
            }
        }

        public static List<HC_CATALOGOS_TIPO> listaTipoCatalogos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from c in contexto.HC_CATALOGOS_TIPO
                        select c).ToList();
            }
        }

        public static List<DtoCatalogos> RecuperarCatalogosPor2Tipos(int codTipo1, int codTipo2)
        {
            return new DatCatalogos().RecuperarCatalogosPor2Tipos(codTipo1,codTipo2);
        }

        public static HC_CATALOGOS RecuperarCatalogoPorNombre(string nombre)
        {
            return new DatCatalogos().RecuperarCatalogoPorNombre(nombre);
        }
        public static HC_CATALOGOS RecuperarPorID(int codigo)
        {
            return new DatCatalogos().RecuperarCatalgoPorID(codigo);
        }
        public static List<HC_CATALOGOS> RecuperarCatalogoPorID(int idcatalogo)
        {
            return new DatCatalogos().RecuperarCatalogoPorID(idcatalogo);
        }

        public static List<HC_CATALOGOS_TIPO> RecuperarCatalogoLaboratoriosA()
        {
            return new DatCatalogos().RecuperarCatalogoLaboratoriosA();
        }
        public static List<HC_CATALOGOS> RecuperaCatalogos()
        {
            return new DatCatalogos().RecuperaCatalogos();
        }
        public static void CrearCatalogo(HC_CATALOGOS catalogo)
        {
            try
            {
                new DatCatalogos().CrearCatalogo(catalogo);
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public static void GrabarCatalogo(HC_CATALOGOS catalogoModificado)
        {
            new DatCatalogos().GrabarCatalogo(catalogoModificado);
        }
        
        public static void EliminarCatalogo(HC_CATALOGOS catalogo)
        {
            try
            {
                new DatCatalogos().EliminarCatalogo(catalogo);
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        #endregion

        public static DataTable EspecilidadAdmision()
        {
            return new DatCatalogos().EspecialidadesAdmision();
        }

        public static string Especialidad(int esp_codigo)
        {
            return new DatCatalogos().Especialidad(esp_codigo);
        }
        #region catalogos generales
        /// <summary>
        /// Devuelve una lista de DtoCatalogosLista que es un formato de catalogos generales para el llenado de listas
        /// </summary>
        /// <param name="tabla">Tabla a la que esta relacionada la lista</param>
        /// <param name="campo">Campo al que esta relacionado la lista</param>
        /// <returns>lista de DtoCatalogosLista</returns>
        public static List<DtoCatalogosLista> RecuperarCatalogoListaGen(string tabla, string campo)
        {
            try
            {
                return new DatCatalogos().RecuperarCatalogoListaGen(tabla, campo); ;
            }
            catch (Exception err){throw err;}
        }
        /// <summary>
        /// Devuelve una lista de Medicos en forma de listado
        /// </summary>
        /// <param name="tipo">Codigo del tipo de medico, en caso de no requerir enviar el parametro como nulo </param>
        /// <returns>lista de DtoCatalogosLista</returns>
        public static List<DtoCatalogosLista> RecuperarCatalogoListaMedicos(string tipo)
        {
            try
            {
                return new DatCatalogos().RecuperarCatalogoListaMedicos(tipo);
            }
            catch (Exception err) { throw err; }
        }
        /// <summary>
        /// Devuelve una lista de Salas en forma de listado catalogos
        /// </summary>
        /// <returns>lista de DtoCatalogosLista</returns>
        public static List<DtoCatalogosLista> RecuperarCatalogoListaSalas()
        {
            try
            {
                return new DatCatalogos().RecuperarCatalogoListaSalas();
            }
            catch (Exception err) { throw err; }
        }

        public static List<DtoCatalogoRecursivo> RecuperarCatalogo(string tabla)
        {
            try
            {
                return new DatCatalogos().RecuperarCatalogo(tabla);
            }
            catch (Exception err) { throw err; }
        }  
        #endregion 

    }
}
