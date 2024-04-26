using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using Microsoft.Data.Extensions;

namespace His.Datos
{
    public class DatCatalogos
    {
        #region catalogos Historia Clinica
        public int RecuperaMaximoCatalogo()
        {
            int maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HC_CATALOGOS> tipoCatalogo = contexto.HC_CATALOGOS.ToList();
                if (tipoCatalogo.Count > 0)
                    maxim = contexto.HC_CATALOGOS.Max(emp => emp.HCC_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }

        }       

        public List<DtoCatalogos> RecuperarCatalogosPorTipo(int codTipo)
        {
            try
            {
                List<DtoCatalogos> antecedentesGrid = new List<DtoCatalogos>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    //return contexto.CIUDAD.FirstOrDefault(c => c.CODCIUDAD == codigoCiudad);
                    List<HC_CATALOGOS> antList = new List<HC_CATALOGOS>();
                    antList = contexto.HC_CATALOGOS.Where(c => c.HC_CATALOGOS_TIPO.HCT_CODIGO == codTipo).ToList();
                    foreach (var valor in antList)
                    {
                        antecedentesGrid.Add(new DtoCatalogos()
                        {
                            HCC_CODIGO = valor.HCC_CODIGO,
                            HCC_ESTADO = (bool)valor.HCC_ESTADO,
                            HCC_NOMBRE = valor.HCC_NOMBRE,
                            HCT_TIPO = codTipo,
                            ENTITYSETNAME = valor.EntityKey.GetFullEntitySetName(),
                            ENTITYID = valor.EntityKey.EntityKeyValues[0].Key
                        }
                        );
                    }
                    return antecedentesGrid;
                }
            }
            catch (Exception err) { throw err; } 
        }

        public List<HC_CATALOGOS> RecuperarHcCatalogosPorTipo(int codTipo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.HC_CATALOGOS.Where(c => c.HC_CATALOGOS_TIPO.HCT_CODIGO == codTipo).ToList();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        /// <summary>
        /// Método para recuperar Datos de Catálogos de Dependencias con su datos de subniveles
        /// </summary>
        /// <param name="codTipo">código de Catalogo</param>
        /// <returns>Lista de datos de catálogo y sus subniveles</returns>


        public List<HC_CATALOGOS> RecuperarHcCatalogosPorTipoSubnivel(int codTipo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.HC_CATALOGOS.Include("HC_CATALOGO_SUBNIVEL").Where(c => c.HCC_CODIGO == codTipo).ToList();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        /// <summary>
        /// Metodo que recupera un objeto de TIPO CATALOGO
        /// </summary>
        /// <param name="codTipo">codigo del tipo de objeto a recuperar</param>
        /// <returns>objeto HC_CATALOGOS_TIPO</returns>
        public HC_CATALOGOS_TIPO RecuperarHcCatalogoTipo(int codTipo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.HC_CATALOGOS_TIPO.Where(c => c.HCT_CODIGO==codTipo).FirstOrDefault();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<HC_CATALOGOS> RecuperaCatalogos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.HC_CATALOGOS.OrderBy(m=>m.HCC_NOMBRE).ToList();
            }
        }
        

        public List<HC_CATALOGOS_TIPO> ListaCatalogos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.HC_CATALOGOS_TIPO.ToList().OrderBy(zon => zon.HCT_NOMBRE).ToList();
            }
        }

        public static List<DtoLocales> RecuperaLocales()
        {
            return new DatLocales().RecuperaLocales();
        }

        public List<DtoCatalogos> RecuperarCatalogosPor2Tipos(int codTipo1, int codTipo2)
        {
            List<DtoCatalogos> antecedentesGrid = new List<DtoCatalogos>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //return contexto.CIUDAD.FirstOrDefault(c => c.CODCIUDAD == codigoCiudad);
                List<HC_CATALOGOS> antList = new List<HC_CATALOGOS>();
                antList = contexto.HC_CATALOGOS.Where(c => c.HC_CATALOGOS_TIPO.HCT_CODIGO == (codTipo1)).Union(contexto.HC_CATALOGOS.Where(c => c.HC_CATALOGOS_TIPO.HCT_CODIGO == (codTipo2))).ToList();
                foreach (var valor in antList)
                {
                    antecedentesGrid.Add(new DtoCatalogos()
                    {
                        HCC_CODIGO = valor.HCC_CODIGO,
                        HCC_ESTADO = (bool)valor.HCC_ESTADO,
                        HCC_NOMBRE = valor.HCC_NOMBRE,
                        HCT_TIPO = codTipo1,
                        ENTITYSETNAME = valor.EntityKey.GetFullEntitySetName(),
                        ENTITYID = valor.EntityKey.EntityKeyValues[0].Key
                    }
                        );
                }
                return antecedentesGrid;
            }
        }

        public HC_CATALOGOS RecuperarCatalogoPorNombre(string nombre)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return(from c in contexto.HC_CATALOGOS
                       where c.HCC_NOMBRE == nombre
                       select c).FirstOrDefault();
            }
        }
        public HC_CATALOGOS RecuperarCatalgoPorID(int codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from c in db.HC_CATALOGOS
                        where c.HCC_CODIGO == codigo
                        select c).FirstOrDefault();
            }
        }

        public List<HC_CATALOGOS_TIPO> RecuperarCatalogoLaboratoriosA()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.HC_CATALOGOS_TIPO.Include("HC_CATALOGOS").Where(h=>h.HCT_CODIGO == 6 || h.HCT_CODIGO == 7 ||h.HCT_CODIGO == 8 ||h.HCT_CODIGO == 9 ||h.HCT_CODIGO == 10).ToList();  
            }
        }

        public List<HC_CATALOGOS> RecuperarCatalogoPorID(int idcatalogo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from c in contexto.HC_CATALOGOS
                        where c.HCC_CODIGO == idcatalogo
                        select c).ToList();
            }
        }
        public void CrearCatalogo(HC_CATALOGOS catalogo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.Crear("HC_CATALOGOS", catalogo);
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public void GrabarCatalogo(HC_CATALOGOS catalogoModificado)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_CATALOGOS catalogo = contexto.HC_CATALOGOS.Where(c => c.HCC_CODIGO == catalogoModificado.HCC_CODIGO).FirstOrDefault();
                catalogo.HCC_NOMBRE = catalogoModificado.HCC_NOMBRE;
                catalogo.HCC_ESTADO = catalogoModificado.HCC_ESTADO;
                contexto.SaveChanges();
            }
        }
        //public void EliminarCatalogo(int idCatalogo)
        public void EliminarCatalogo(HC_CATALOGOS catalogoModificado)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.Eliminar(catalogoModificado);
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        #endregion
        #region catalogos generales

        /// <summary>
        /// Devuelve una lista de DtoCatalogosLista que es un formato de catalogos generales para el llenado de listas
        /// </summary>
        /// <param name="tabla">Tabla a la que esta relacionada la lista</param>
        /// <param name="campo">Campo al que esta relacionado la lista</param>
        /// <returns>lista de DtoCatalogosLista</returns>
        public List<DtoCatalogosLista> RecuperarCatalogoListaGen(string tabla, string campo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var query = from c in contexto.CATALOGOS
                                where c.CAD_TABLA == tabla.Trim() && c.CAD_CAMPO == campo.Trim()
                                let cadValor = c.CAD_VALOR
                                where cadValor != null
                                orderby cadValor
                                select new DtoCatalogosLista
                                {
                                    codigo=cadValor.Value,
                                    nombre=c.CAD_NOMBRE,
                                    prefijo = c.CAD_PREFI
                                };
                    return query.ToList();
                }
            }
            catch (Exception err){throw err;}
        }

        /// <summary>
        /// Devuelve una lista de Medicos en forma de listado
        /// </summary>
        /// <param name="tipo">Codigo del tipo de medico, en caso de no requerir enviar el parametro como nulo </param>
        /// <returns>lista de DtoCatalogosLista</returns>
        public List<DtoCatalogosLista> RecuperarCatalogoListaMedicos(string tipo)
        {
            try
            {
                List<DtoCatalogosLista> lstMedicos = new List<DtoCatalogosLista>();
                DtoCatalogosLista medicoDefault = new DtoCatalogosLista {codigo=-1,nombre=""};
                lstMedicos.Add(medicoDefault);
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    if(tipo!=null)
                    {
                        Int16 codTipo = Convert.ToInt16(tipo);
                        var query = from m in contexto.MEDICOS
                                    where m.MED_ESTADO == true && m.TIPO_MEDICO.TIM_CODIGO == codTipo
                                    orderby m.MED_APELLIDO_PATERNO, m.MED_APELLIDO_MATERNO, m.MED_NOMBRE1
                                        , m.MED_NOMBRE2
                                    select new DtoCatalogosLista
                                    {
                                        codigo = m.MED_CODIGO,
                                        nombre = m.MED_APELLIDO_PATERNO + " " + m.MED_APELLIDO_MATERNO 
                                                 + " " + m.MED_NOMBRE1+ " " + m.MED_NOMBRE2
                                    };
                        lstMedicos.AddRange(query.ToList());
                    }
                    else
                    {
                        var query = from m in contexto.MEDICOS
                                    where m.MED_ESTADO == true 
                                    orderby m.MED_APELLIDO_PATERNO, m.MED_APELLIDO_MATERNO, m.MED_NOMBRE1
                                        , m.MED_NOMBRE2
                                    select new DtoCatalogosLista
                                    {
                                        codigo = m.MED_CODIGO,
                                        nombre = m.MED_APELLIDO_PATERNO + " " + m.MED_APELLIDO_MATERNO
                                                 + " " + m.MED_NOMBRE1 + " " + m.MED_NOMBRE2
                                    };
                        lstMedicos.AddRange(query.ToList());
                    } 
                }
                return lstMedicos;
            }
            catch (Exception err) { throw err; }
        }

        /// <summary>
        /// Devuelve una lista de Salas en forma de listado catalogos
        /// </summary>
        /// <returns>lista de DtoCatalogosLista</returns>
        public List<DtoCatalogosLista> RecuperarCatalogoListaSalas()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var query = from s in contexto.TIPO_INTER_MEDICA
                                    orderby s.TII_NOMBRE
                                    select new DtoCatalogosLista
                                    {
                                        codigo = s.TII_CODIGO,
                                        nombre = s.TII_NOMBRE
                                    };
                   return query.ToList();
                }
            }
            catch (Exception err) { throw err; }
        }


        public List<DtoCatalogoRecursivo> RecuperarCatalogo(string tabla)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var entityConnection = (EntityConnection)contexto.Connection;
                    DbConnection storeConnection = entityConnection.StoreConnection;
                    DbCommand command = storeConnection.CreateCommand();
                    command.CommandText = "sp_estructura_comercial";
                    command.CommandType = CommandType.StoredProcedure;

                    using (contexto.Connection.CreateConnectionScope())
                    {
                        return command.Materialize(r => new DtoCatalogoRecursivo
                        {
                            codigo = r.Field<float>("codigo"),
                            descripcion = r.Field<string>("descripcion"),
                            codPadre = r.Field<float>("cod_padre")
                        }).ToList();

                    }
                }
            }
            catch (Exception err) { throw err; }
        }

        #endregion 

        public DataTable EspecialidadesAdmision()
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select ESP_CODIGO as CODIGO, ESP_NOMBRE AS DESCRIPCION from ESPECIALIDADES_MEDICAS where ESP_ESTADO = 1", connection);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }
        public string Especialidad(int esp_codigo)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            string esp_nombre = "";
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select ESP_NOMBRE from ESPECIALIDADES_MEDICAS where ESP_CODIGO = @esp_codigo", connection);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@esp_codigo", esp_codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                esp_nombre = reader["ESP_NOMBRE"].ToString();
            }
            reader.Close();
            connection.Close();
            return esp_nombre;
        }
    } 
}
