using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace His.Datos
{
    public class DatPreciosConvenios
    {
        public int RecuperaMaximoPconvenios()
        {
            int maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PRECIOS_POR_CONVENIOS> pconvenios = contexto.PRECIOS_POR_CONVENIOS.ToList();
                if (pconvenios.Count > 0)
                    maxim = contexto.PRECIOS_POR_CONVENIOS.Max(pc => pc.PRE_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }

        }
        public List<DtoPreciosConvenios> RecuperaPconvenios()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<DtoPreciosConvenios> listaPrecios = new List<DtoPreciosConvenios>();
                var lista = from c in contexto.CATEGORIAS_CONVENIOS
                            join p in contexto.PRECIOS_POR_CONVENIOS on c.CAT_CODIGO equals p.CATEGORIAS_CONVENIOS.CAT_CODIGO
                            join cc in contexto.CATALOGO_COSTOS on p.CATALOGO_COSTOS.CAC_CODIGO equals cc.CAC_CODIGO
                            select new { p.PRE_CODIGO, cc.CAC_CODIGO, cc.CAC_NOMBRE, c.CAT_CODIGO, c.CAT_NOMBRE, p.PRE_VALOR, p.PRE_PORCENTAJE };

                if (lista != null)
                {
                    foreach (var acceso in lista)
                    {
                        listaPrecios.Add(new DtoPreciosConvenios()
                        {
                            PRE_CODIGO = acceso.PRE_CODIGO,
                            CAC_CODIGO = acceso.CAC_CODIGO,
                            CAC_NOMBRE = acceso.CAC_NOMBRE,
                            CAT_CODIGO = acceso.CAT_CODIGO,
                            CAT_NOMBRE = acceso.CAT_NOMBRE,
                            PRE_PORCENTAJE = Convert.ToDecimal(acceso.PRE_PORCENTAJE),
                            PRE_VALOR = Convert.ToDecimal(acceso.PRE_VALOR)
                        });
                    }
                }
                else
                {
                    listaPrecios = null;
                }


                return listaPrecios;

            }
        }

        public void CrearPconvenios(PRECIOS_POR_CONVENIOS pconvenios)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.AddToPRECIOS_POR_CONVENIOS(pconvenios);
                    contexto.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void ActualizaCodigosSic()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();

            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            string query = "update PRECIOS_POR_CONVENIOS set PRO_CODIGO=CAC_CODIGO where PRO_CODIGO is null";
            //string sql = "update PRECIOS_POR_CONVENIOS set PRO_CODIGO=CAC_CODIGO where PRO_CODIGO is null";

            DataTable tablas = new DataTable();
            try
            {
                Sqlcmd = new SqlCommand(query, Sqlcon);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;

                SqlDataReader reader;
                Sqldap.SelectCommand = Sqlcmd;
                reader = Sqlcmd.ExecuteReader();
                tablas.Load(reader);
                reader.Close();
                Sqldap.Fill(Dts);
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Sqlcmd = new SqlCommand(sql, Sqlcon);
            //Sqldap = new SqlDataAdapter();
            //Sqldap.SelectCommand = Sqlcmd;
        }

        public int IngresaCodigo(string cac_nombre, int cat_codigo, int cac_codigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();

            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("[sp_IngresaProCodigoPRC]", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@Cac_Nombre", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Cac_Nombre"].Value = (cac_nombre);

            Sqlcmd.Parameters.Add("@Cat_Codigo", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Cat_Codigo"].Value = (cat_codigo);

            Sqlcmd.Parameters.Add("@Cac_Codigo", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Cac_Codigo"].Value = (cac_codigo);


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            return 0;
        }

        public void GrabarPconvenios(PRECIOS_POR_CONVENIOS pconveniosModificada, PRECIOS_POR_CONVENIOS pconveniosOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PRECIOS_POR_CONVENIOS pconvenios = contexto.PRECIOS_POR_CONVENIOS.FirstOrDefault(p => p.PRE_CODIGO == pconveniosModificada.PRE_CODIGO);
                pconvenios.PRE_VALOR = pconveniosModificada.PRE_VALOR;
                contexto.SaveChanges();
            }
        }




        public void EliminarPconvenios(PRECIOS_POR_CONVENIOS pconvenios)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PRECIOS_POR_CONVENIOS pconvneios = contexto.PRECIOS_POR_CONVENIOS.FirstOrDefault(p => p.PRE_CODIGO == pconvenios.PRE_CODIGO);
                contexto.DeleteObject(pconvenios);
                contexto.SaveChanges();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigoConvenio"></param>
        /// <returns></returns>
        public bool ConPrecios(Int16 codigoConvenio)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var contador = (from p in contexto.PRECIOS_POR_CONVENIOS
                                where p.CATEGORIAS_CONVENIOS.CAT_CODIGO == codigoConvenio
                                select p.PRE_CODIGO).Count();
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigoConvenio"></param>
        /// <returns></returns>
        public List<PRECIOS_POR_CONVENIOS> RecuperarPreciosPorConvenio(Int16 codigoConvenio)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.PRECIOS_POR_CONVENIOS.Include("CATEGORIAS_CONVENIOS").Include("CATALOGO_COSTOS").Where(p => p.CATEGORIAS_CONVENIOS.CAT_CODIGO == codigoConvenio).ToList();
                //  return contexto.PRECIOS_POR_CONVENIOS.Include("CATEGORIAS_CONVENIOS").Include("CATALOGO_COSTOS").ToList();
                //return contexto.PRECIOS_POR_CONVENIOS.Include("CATEGORIAS_CONVENIOS").Include("CATALOGO_COSTOS").ToList();
            }
        }


        /// <summary>
        /// Eliminar los precios de un convenio
        /// </summary>
        /// <param name="codigoConvenio"></param>
        public void EliminarPreciosPorConvenio(int codigoConvenio)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PRECIOS_POR_CONVENIOS> precios = (from p in contexto.PRECIOS_POR_CONVENIOS
                                                       where p.CATEGORIAS_CONVENIOS.CAT_CODIGO == codigoConvenio
                                                       select p).ToList();
                foreach (PRECIOS_POR_CONVENIOS precio in precios)
                {
                    contexto.DeleteObject(precio);
                }
                contexto.SaveChanges();
            }
        }
        public bool creaPreciosConvenio(Int64 CAT_CODIGO, List<PRECIOS_POR_CONVENIOS> precon)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    List<PRECIOS_POR_CONVENIOS> listapre = db.PRECIOS_POR_CONVENIOS.Where(x => x.CATEGORIAS_CONVENIOS.CAT_CODIGO == CAT_CODIGO).ToList();

                    db.EliminarLista(listapre);
                    db.CrearLista("PRECIOS_POR_CONVENIOS", precon);

                    db.SaveChanges();
                    transa.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;

                }
                catch (Exception ex)
                {
                    transa.Rollback();
                    Console.WriteLine(ex.Message);
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }

            }
        }

    }
}
