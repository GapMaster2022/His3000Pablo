using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using System.Data.SqlClient;
using System.Data;

using His.Entidades.Clases;

namespace His.Datos
{
    public class DatProducto
    {

        public DataTable RecuperarProductoIDSIC(Int64 codProducto)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_RecuperarProductoIDSIC", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codProducto);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
        public List<PRODUCTO> RecuperarProductosLista(string buscar, int criterio, int cantidad, int codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PRODUCTO> result = new List<PRODUCTO>();

                if (criterio == 1)
                    result = contexto.PRODUCTO.Where(p => p.PRO_CODIGO == codigo).ToList();

                //if (buscar != string.Empty && buscar != null)
                //{
                if (criterio == 2)
                    result = contexto.PRODUCTO.Where(p => p.PRO_NOMBRE_COMERCIAL.Contains(buscar)).ToList();

                if (criterio == 3)
                    result = contexto.PRODUCTO.Where(p => p.PRO_NOMBRE_GENERICO.Contains(buscar)).ToList();

                if (criterio == 4)
                    result = contexto.PRODUCTO.Where(p => p.PRO_DESCRIPCION.Contains(buscar)).ToList();

                if (criterio == 5)
                    result = contexto.PRODUCTO.Where(p => p.PRO_CODIGO_BARRAS.StartsWith(buscar)).ToList();
                //}

                //if (descripcion != string.Empty)
                //{
                //    string[] cadena = nombre.Split();

                //    for (int i = 0; i < cadena.Length; i++)
                //    {
                //        string n = cadena[i].Trim();
                //        result = result.Where(p => (p.PAC_APELLIDO_PATERNO + p.PAC_APELLIDO_MATERNO + p.PAC_NOMBRE1 + p.PAC_NOMBRE2).Contains(n));
                //    }
                //}

                result = result.OrderBy(p => p.PRO_NOMBRE_COMERCIAL).Take(cantidad).ToList();
                //pacientes = result.ToList();
                return result;
            }
        }

        public List<PRODUCTO> RecuperarProductosLista()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PRODUCTO
                        orderby p.PRO_NOMBRE_COMERCIAL
                        select p
                        ).ToList();
            }
        }
        /// <summary>
        /// Metodo que retorna la lista paginada de productos
        /// </summary>
        /// <param name="desde">registro desde el cual se empieza la seleccion</param>
        /// <param name="cantidad">cantidad de registros que se seleccionaran</param>
        /// <returns></returns>
        public List<PRODUCTO> RecuperarProductosLista(int desde, int cantidad, string busqueda)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                if (busqueda != null)
                {
                    if (isNumeric(busqueda))
                    {
                        Int64 codigo = Convert.ToInt64(busqueda);
                        return contexto.PRODUCTO.Where(p => p.PRO_CODIGO == codigo).OrderBy(p => p.PRO_NOMBRE_COMERCIAL).Skip(desde).Take(cantidad).ToList();
                    }
                    else
                    {
                        return contexto.PRODUCTO.Where(p => (p.PRO_DESCRIPCION.Contains(busqueda)
                           || p.PRO_NOMBRE_GENERICO.Contains(busqueda))
                           || p.PRO_NOMBRE_COMERCIAL.Contains(busqueda)).OrderBy(p => p.PRO_NOMBRE_COMERCIAL).Skip(desde).Take(cantidad).ToList();
                    }
                }
                else
                {
                    return contexto.PRODUCTO.OrderBy(p => p.PRO_NOMBRE_COMERCIAL).Skip(desde).Take(cantidad).OrderBy(p => p.PRO_NOMBRE_COMERCIAL).ToList();
                }
            }
        }

        /// <summary>
        /// Metodo que retorna la lista paginada de productos
        /// </summary>
        /// <param name="desde">registro desde el cual se empieza la seleccion</param>
        /// <param name="cantidad">cantidad de registros que se seleccionaran</param>
        /// <param name="codigoArea">Codigo del Area a la que esta relacionada la estructura</param>
        /// <returns></returns>
        public List<PRODUCTO> RecuperarProductosLista(int desde, int cantidad, string busqueda, Int16 codigoArea)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //List<Int16> listaArea = contexto.PRODUCTO_ESTRUCTURA_AREAS.Where(a => a.PEA_CODIGO == codigoArea).Select(a => (Int16)a.PRE_CODIGO).ToList();     
                if (busqueda != null)
                {
                    if (isNumeric(busqueda))
                    {
                        Int64 codigo = Convert.ToInt64(busqueda);
                        //return (from p in contexto.PRODUCTO
                        //        join e in contexto.PRODUCTO_ESTRUCTURA on p.PRODUCTO_ESTRUCTURA.PRE_CODIGO equals e.PRE_CODIGO
                        //        join a in contexto.PRODUCTO_ESTRUCTURA_AREAS on e.PRE_CODIGO equals a.PRE_CODIGO
                        //        where a.PEA_CODIGO == codigoArea && p.PRO_CODIGO == codigo
                        //        orderby p.PRO_NOMBRE_COMERCIAL
                        //        select p).Skip(desde).Take(cantidad).ToList();
                        return (from p in contexto.PRODUCTO
                                where p.PRODUCTO_ESTRUCTURA.PRE_CODIGO == codigoArea && p.PRO_CODIGO == codigo
                                orderby p.PRO_NOMBRE_COMERCIAL
                                select p).Skip(desde).Take(cantidad).ToList();
                        //return contexto.PRODUCTO.Where(p => p.PRO_CODIGO == codigo ).OrderBy(p => p.PRO_NOMBRE_COMERCIAL).Skip(desde).Take(cantidad).ToList();
                    }
                    else
                    {
                        //return (from p in contexto.PRODUCTO
                        //        join e in contexto.PRODUCTO_ESTRUCTURA on p.PRODUCTO_ESTRUCTURA.PRE_CODIGO equals e.PRE_CODIGO
                        //        join a in contexto.PRODUCTO_ESTRUCTURA_AREAS on e.PRE_CODIGO equals a.PRE_CODIGO
                        //        where a.PEA_CODIGO == codigoArea && (p.PRO_DESCRIPCION.Contains(busqueda)
                        //            || p.PRO_NOMBRE_GENERICO.Contains(busqueda)
                        //            || p.PRO_NOMBRE_COMERCIAL.Contains(busqueda))
                        //        orderby p.PRO_NOMBRE_COMERCIAL
                        //        select p).Skip(desde).Take(cantidad).ToList();
                        return (from p in contexto.PRODUCTO
                                where p.PRODUCTO_ESTRUCTURA.PRE_CODIGO == codigoArea && (p.PRO_DESCRIPCION.Contains(busqueda)
                                    || p.PRO_NOMBRE_GENERICO.Contains(busqueda)
                                    || p.PRO_NOMBRE_COMERCIAL.Contains(busqueda))
                                orderby p.PRO_NOMBRE_COMERCIAL
                                select p).Skip(desde).Take(cantidad).ToList();
                        //return contexto.PRODUCTO.Where(p => (p.PRO_DESCRIPCION.Contains(busqueda)
                        //    || p.PRO_NOMBRE_GENERICO.Contains(busqueda)
                        //    || p.PRO_NOMBRE_COMERCIAL.Contains(busqueda)) && listaArea.Contains(p.PRODUCTO_ESTRUCTURA.PRE_CODIGO)).OrderBy(p => p.PRO_NOMBRE_COMERCIAL).Skip(desde).Take(cantidad).ToList();
                    }
                }
                else
                {
                    //return (from p in contexto.PRODUCTO   
                    //        join e in contexto.PRODUCTO_ESTRUCTURA on p.PRODUCTO_ESTRUCTURA.PRE_CODIGO equals e.PRE_CODIGO
                    //        join a in contexto.PRODUCTO_ESTRUCTURA_AREAS on e.PRE_CODIGO equals a.PRE_CODIGO
                    //        where a.PEA_CODIGO ==codigoArea
                    //        orderby p.PRO_NOMBRE_COMERCIAL
                    //        select p).Skip(desde).Take(cantidad).ToList();
                    return (from p in contexto.PRODUCTO
                            where p.PRODUCTO_ESTRUCTURA.PRE_CODIGO == codigoArea
                            orderby p.PRO_NOMBRE_COMERCIAL
                            select p).Skip(desde).Take(cantidad).ToList();
                    //return contexto.PRODUCTO.Where(p=>listaArea.Contains(p.PRODUCTO_ESTRUCTURA.PRE_CODIGO)).OrderBy(p => p.PRO_NOMBRE_COMERCIAL).Skip(desde).Take(cantidad).OrderBy(p => p.PRO_NOMBRE_COMERCIAL).ToList();
                }
            }
        }

        //<summary>
        //Metodo que retorna la lista paginada de productos
        //</summary>
        //<param name="desde">registro desde el cual se empieza la seleccion</param>
        //<param name="cantidad">cantidad de registros que se seleccionaran</param>
        //<param name="codigoArea">Codigo del Area a la que esta relacionada la estructura</param>
        //<returns></returns>


        public DataTable RecuperarProductosListaSPPedidosAreas(int Opcion, string Filtro, int Division, int Bodega, Int32 CodigoEmpresa, Int32 CodigoConvenio, Int32 PedidoAreas)
        {

            // VERIFICA SI UN USUARIO TIENES LOS PERMISOS NECESARIOS PARA CAMBIAR EL ESTADO DE UNA CUENTA / GIOVANNY TAPIA / 07/08/2012

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

            Sqlcmd = new SqlCommand("sp_BuscaProductoSic3000PedidoArea", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Pea_Codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@Pea_Codigo"].Value = PedidoAreas;

            Sqlcmd.Parameters.Add("@p_Opcion", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Opcion"].Value = Opcion;

            Sqlcmd.Parameters.Add("@p_filtro", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_filtro"].Value = Filtro;

            Sqlcmd.Parameters.Add("@p_Division", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Division"].Value = Division;

            Sqlcmd.Parameters.Add("@p_Bodega", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Bodega"].Value = Bodega;

            Sqlcmd.Parameters.Add("@CodigoEmpresa", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoEmpresa"].Value = CodigoEmpresa;

            Sqlcmd.Parameters.Add("@CodigoConvenio", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoConvenio"].Value = CodigoConvenio;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            return Dts;

        }

        public DataTable RecuperarProductosListaSPall(int Opcion, string Filtro, int Division, int Bodega, Int32 CodigoEmpresa, Int32 CodigoConvenio)
        {

            // VERIFICA SI UN USUARIO TIENES LOS PERMISOS NECESARIOS PARA CAMBIAR EL ESTADO DE UNA CUENTA / GIOVANNY TAPIA / 07/08/2012

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

            Sqlcmd = new SqlCommand("sp_BuscaProductoSic3000all", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_Opcion", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Opcion"].Value = Opcion;

            Sqlcmd.Parameters.Add("@p_filtro", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_filtro"].Value = Filtro;

            Sqlcmd.Parameters.Add("@p_Division", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Division"].Value = Division;

            Sqlcmd.Parameters.Add("@p_Bodega", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Bodega"].Value = Bodega;

            Sqlcmd.Parameters.Add("@CodigoEmpresa", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoEmpresa"].Value = CodigoEmpresa;

            Sqlcmd.Parameters.Add("@CodigoConvenio", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoConvenio"].Value = CodigoConvenio;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;

            Sqldap.Fill(Dts);

            DataTable Ayuda = new DataTable();

            Ayuda.Clear();
            Ayuda.Columns.Add("DIVISION", typeof(string));

            Ayuda.Columns.Add("CODIGO", typeof(string));
            Ayuda.Columns.Add("PRODUCTO", typeof(string));
            Ayuda.Columns.Add("STOCK", typeof(string));
            Ayuda.Columns.Add("IVA", typeof(string));
            Ayuda.Columns.Add("VALOR", typeof(string));
            Ayuda.Columns.Add("Cantidad", typeof(string));
            Ayuda.Columns.Add("clasprod", typeof(string));

            foreach (DataRow item in Dts.Rows)
            {
                DataRow resultRow = Ayuda.NewRow();
                resultRow["DIVISION"] = item["DIVISION"].ToString();
                resultRow["CODIGO"] = item["CODIGO"].ToString();
                resultRow["PRODUCTO"] = item["PRODUCTO"].ToString();
                string prueba = item["clasprod"].ToString();
                if (prueba.Trim() == "S")
                    resultRow["STOCK"] = "9999";
                else
                    resultRow["STOCK"] = item["STOCK"].ToString();
                resultRow["IVA"] = item["IVA"].ToString();
                resultRow["VALOR"] = item["VALOR"].ToString();
                resultRow["Cantidad"] = item["Cantidad"].ToString();
                resultRow["clasprod"] = item["clasprod"].ToString();

                Ayuda.Rows.Add(resultRow);
            }

            return Ayuda;


        }
        public DataTable RecuperarProductosListaServicios(int Opcion, string Filtro, int Division, int Bodega, Int32 CodigoEmpresa, Int32 CodigoConvenio)
        {

            // VERIFICA SI UN USUARIO TIENES LOS PERMISOS NECESARIOS PARA CAMBIAR EL ESTADO DE UNA CUENTA / GIOVANNY TAPIA / 07/08/2012

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

            Sqlcmd = new SqlCommand("sp_BuscarProductoSic3000_ServiciosPasteur", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_Opcion", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Opcion"].Value = Opcion;

            Sqlcmd.Parameters.Add("@p_filtro", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_filtro"].Value = Filtro;

            Sqlcmd.Parameters.Add("@p_Division", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Division"].Value = Division;

            Sqlcmd.Parameters.Add("@p_Bodega", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Bodega"].Value = Bodega;

            Sqlcmd.Parameters.Add("@CodigoEmpresa", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoEmpresa"].Value = CodigoEmpresa;

            Sqlcmd.Parameters.Add("@CodigoConvenio", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoConvenio"].Value = CodigoConvenio;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;

            Sqldap.Fill(Dts);

            DataTable Ayuda = new DataTable();

            Ayuda.Clear();
            Ayuda.Columns.Add("DIVISION", typeof(string));
            Ayuda.Columns.Add("CODIGO", typeof(string));
            Ayuda.Columns.Add("PRODUCTO", typeof(string));
            Ayuda.Columns.Add("STOCK", typeof(string));
            Ayuda.Columns.Add("IVA", typeof(string));
            Ayuda.Columns.Add("VALOR", typeof(string));
            Ayuda.Columns.Add("Cantidad", typeof(string));
            Ayuda.Columns.Add("clasprod", typeof(string));

            foreach (DataRow item in Dts.Rows)
            {
                DataRow resultRow = Ayuda.NewRow();
                resultRow["DIVISION"] = item["DIVISION"].ToString();
                resultRow["CODIGO"] = item["CODIGO"].ToString();
                resultRow["PRODUCTO"] = item["PRODUCTO"].ToString();
                string prueba = item["clasprod"].ToString();
                if (prueba.Trim() == "S")
                    resultRow["STOCK"] = "9999";
                else
                    resultRow["STOCK"] = item["STOCK"].ToString();
                resultRow["IVA"] = item["IVA"].ToString();
                resultRow["VALOR"] = item["VALOR"].ToString();
                resultRow["Cantidad"] = item["Cantidad"].ToString();
                resultRow["clasprod"] = item["clasprod"].ToString();

                Ayuda.Rows.Add(resultRow);
            }

            return Ayuda;


        }

        public DataTable RecuperarProductosListaSP(int Opcion, string Filtro, int Division, int Bodega, Int32 CodigoEmpresa, Int32 CodigoConvenio)
        {

            // VERIFICA SI UN USUARIO TIENES LOS PERMISOS NECESARIOS PARA CAMBIAR EL ESTADO DE UNA CUENTA / GIOVANNY TAPIA / 07/08/2012

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

            Sqlcmd = new SqlCommand("sp_BuscaProductoSic3000", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_Opcion", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Opcion"].Value = Opcion;

            Sqlcmd.Parameters.Add("@p_filtro", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_filtro"].Value = Filtro;

            Sqlcmd.Parameters.Add("@p_Division", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Division"].Value = Division;

            Sqlcmd.Parameters.Add("@p_Bodega", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Bodega"].Value = Bodega;

            Sqlcmd.Parameters.Add("@CodigoEmpresa", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoEmpresa"].Value = CodigoEmpresa;
            Sqlcmd.Parameters.Add("@CodigoConvenio", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoConvenio"].Value = CodigoConvenio;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            DataTable Ayuda = new DataTable();

            Ayuda.Clear();
            Ayuda.Columns.Add("DIVISION", typeof(string));
            Ayuda.Columns.Add("CODIGO", typeof(string));
            Ayuda.Columns.Add("PRODUCTO", typeof(string));
            Ayuda.Columns.Add("STOCK", typeof(string));
            Ayuda.Columns.Add("IVA", typeof(string));
            Ayuda.Columns.Add("VALOR", typeof(string));
            Ayuda.Columns.Add("Cantidad", typeof(string));
            Ayuda.Columns.Add("clasprod", typeof(string));

            foreach (DataRow item in Dts.Rows)
            {
                DataRow resultRow = Ayuda.NewRow();
                resultRow["DIVISION"] = item["DIVISION"].ToString();
                resultRow["CODIGO"] = item["CODIGO"].ToString();
                resultRow["PRODUCTO"] = item["PRODUCTO"].ToString();
                string prueba = item["clasprod"].ToString();
                if (prueba.Trim() == "S")
                    resultRow["STOCK"] = "9999";
                else
                    resultRow["STOCK"] = item["STOCK"].ToString();
                resultRow["IVA"] = item["IVA"].ToString();
                resultRow["VALOR"] = item["VALOR"].ToString();
                resultRow["Cantidad"] = item["Cantidad"].ToString();
                resultRow["clasprod"] = item["clasprod"].ToString();

                Ayuda.Rows.Add(resultRow);
            }

            return Ayuda;


        }

        public DataTable RecuperarProductosListaSPconvenios(int Opcion, string Filtro, int Division, int Bodega, Int32 CodigoEmpresa, Int32 CodigoConvenio)
        {

            // Extrae los precios por convenios Pablo Rocha 30-04-2014

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

            Sqlcmd = new SqlCommand("sp_BuscaProductoHis3000Convenios", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_Opcion", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Opcion"].Value = Opcion;

            Sqlcmd.Parameters.Add("@p_filtro", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_filtro"].Value = Filtro;

            Sqlcmd.Parameters.Add("@p_Division", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Division"].Value = Division;

            Sqlcmd.Parameters.Add("@p_Bodega", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Bodega"].Value = Bodega;

            Sqlcmd.Parameters.Add("@CodigoEmpresa", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoEmpresa"].Value = CodigoEmpresa;

            Sqlcmd.Parameters.Add("@CodigoConvenio", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoConvenio"].Value = CodigoConvenio;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);



            return Dts;

        }

        public DataTable RecuperarProductosListaSP_Farmacia(int Opcion, string Filtro, int Division, int Bodega, Int32 CodigoEmpresa, Int32 CodigoConvenio)
        {

            // VERIFICA SI UN USUARIO TIENES LOS PERMISOS NECESARIOS PARA CAMBIAR EL ESTADO DE UNA CUENTA / GIOVANNY TAPIA / 07/08/2012

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

            Sqlcmd = new SqlCommand("sp_BuscaProductoSic3000_Farmacia", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_Opcion", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Opcion"].Value = Opcion;

            Sqlcmd.Parameters.Add("@p_filtro", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_filtro"].Value = Filtro;

            Sqlcmd.Parameters.Add("@p_Division", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Division"].Value = Division;

            Sqlcmd.Parameters.Add("@p_Bodega", SqlDbType.Int);
            Sqlcmd.Parameters["@p_Bodega"].Value = Bodega;

            Sqlcmd.Parameters.Add("@CodigoEmpresa", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoEmpresa"].Value = CodigoEmpresa;

            Sqlcmd.Parameters.Add("@CodigoConvenio", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoConvenio"].Value = CodigoConvenio;


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;

            Sqldap.Fill(Dts);


            DataTable Ayuda = new DataTable();

            Ayuda.Clear();
            Ayuda.Columns.Add("DIVISION", typeof(string));
            Ayuda.Columns.Add("CODIGO", typeof(string));
            Ayuda.Columns.Add("PRODUCTO", typeof(string));
            Ayuda.Columns.Add("STOCK", typeof(string));
            Ayuda.Columns.Add("IVA", typeof(string));
            Ayuda.Columns.Add("VALOR", typeof(string));
            Ayuda.Columns.Add("Cantidad", typeof(string));
            Ayuda.Columns.Add("clasprod", typeof(string));

            foreach (DataRow item in Dts.Rows)
            {
                DataRow resultRow = Ayuda.NewRow();
                resultRow["DIVISION"] = item["DIVISION"].ToString();
                resultRow["CODIGO"] = item["CODIGO"].ToString();
                resultRow["PRODUCTO"] = item["PRODUCTO"].ToString();
                string prueba = item["clasprod"].ToString();
                if (prueba.Trim() == "S")
                    resultRow["STOCK"] = "9999";
                else
                    resultRow["STOCK"] = item["STOCK"].ToString();
                resultRow["IVA"] = item["IVA"].ToString();
                resultRow["VALOR"] = item["VALOR"].ToString();
                resultRow["Cantidad"] = item["Cantidad"].ToString();
                resultRow["clasprod"] = item["clasprod"].ToString();

                Ayuda.Rows.Add(resultRow);
            }

            return Ayuda;

        }


        //<summary>
        //Metodo que retorna un pedido
        //</summary>
        //<param name="NumeroPedido">Numero de pedido</param>
        //<returns></returns>
        public DataTable RecuperarPedido(int NumeroPedido)
        {
            // GIOVANNY TAPIA / 07/08/2012

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
            Sqlcmd = new SqlCommand("sp_pedido", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@NumeroPedido", SqlDbType.Int);
            Sqlcmd.Parameters["@NumeroPedido"].Value = NumeroPedido;
            Sqlcmd.Parameters.Add("@Bodega", SqlDbType.Int);
            Sqlcmd.Parameters["@Bodega"].Value = Entidades.Clases.Sesion.bodega;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            return Dts;




        }

        public PRODUCTO RecuperarProductoID(int codProducto)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PRODUCTO
                        where p.PRO_CODIGO == codProducto
                        select p).FirstOrDefault();
            }
        }
        public List<PRODUCTO> RecuperarProducto(int codProductoEst)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PRODUCTO
                        join pe in contexto.PRODUCTO_ESTRUCTURA on p.PRODUCTO_ESTRUCTURA.PRE_CODIGO equals pe.PRE_CODIGO
                        where pe.PRE_CODIGO == codProductoEst
                        orderby p.PRO_DESCRIPCION
                        select p).ToList();
            }
        }


        public List<PRODUCTO> RecuperarProductoSubDiv(int codSubDivision)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PRODUCTO
                        join pe in contexto.PRODUCTO_ESTRUCTURA on p.PRODUCTO_ESTRUCTURA.PRE_CODIGO equals pe.PRE_CODIGO
                        where p.PRO_CODSUB == codSubDivision
                        orderby p.PRO_DESCRIPCION
                        select p).ToList();
            }
        }

        //public List<DtoLaboratorioEstructura> RecuperarProductoSub(Int16 codPadre)
        //{
        //    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //    {
        //        //return (from pe in contexto.PRODUCTO_ESTRUCTURA
        //        //        join p on contexto.PRODUCTO 
        //        //        where p.PRE_PADRE == codPadre
        //        //        select new DtoLaboratorioEstructura
        //        //                {
        //        //                    CODIGO_AREA = p.PRE_CODIGO,
        //        //                    AREA = p.PRE_DESCRIPCION
        //        //                    //COD_PRODUCTO = p.PRO_CODIGO,
        //        //                    //COD_EXAMEN = p.PRO_CODIGO_BARRAS,
        //        //                    //EXAMEN = p.PRO_DESCRIPCION
        //        //                }).ToList();

        //        return (from p in contexto.PRODUCTO
        //                join pe in contexto.PRODUCTO_ESTRUCTURA on p.PRODUCTO_ESTRUCTURA.PRE_PADRE equals pe.PRE_PADRE
        //                where p.PRO_CODDIV == codPadre
        //                orderby p.PRO_DESCRIPCION
        //                select new DtoLaboratorioEstructura
        //                {
        //                    CODIGO_AREA = pe.PRE_CODIGO,
        //                    AREA = pe.PRE_DESCRIPCION,
        //                    COD_PRODUCTO = p.PRO_CODIGO,
        //                    COD_EXAMEN = p.PRO_CODIGO_BARRAS,
        //                    EXAMEN = p.PRO_DESCRIPCION
        //                }).ToList();
        //    }
        //}


        //valida si un string es numerico
        public static bool isNumeric(object value)
        {
            try
            {
                double d = System.Double.Parse(value.ToString(), System.Globalization.NumberStyles.Any);
                return true;
            }
            catch (System.FormatException)
            {
                return false;
            }
        }

        public DataTable DivisionProducto(string codpro, int codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_DivisionProducto", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@codigo", codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

        public Int64 NumeroDocumento()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            Int64 numdoc = 0;

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_NumVale", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                numdoc = Convert.ToInt64(reader["PDD_CODIGO"].ToString());
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return numdoc;
        }
        public DataTable listaproductosXdescripcion(string descripcion)
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
            try
            {
                Sqlcmd = new SqlCommand("SELECT codpro,despro FROM Sic3000..Producto WHERE despro like '%" + descripcion + "%' and activo = 1 order by despro", Sqlcon);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts);
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }
        public DataTable recuperaProductoSicXcodpro(string codpro)
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
            try
            {
                Sqlcmd = new SqlCommand("select codpro,despro,iva from Sic3000..Producto where codpro =  " + codpro, Sqlcon);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts);
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }
    }
}
