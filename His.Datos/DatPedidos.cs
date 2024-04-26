using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Entidades.Pedidos;
using Core.Datos;
using Core.Entidades;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Transactions;

namespace His.Datos
{
    public class DatPedidos
    {
        #region PEDIDOS

        public int ultimoCodigoPedidos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from p in contexto.PEDIDOS
                             select p.PED_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }
        public void crearPedido(PEDIDOS npedido)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.AddToPEDIDOS(npedido);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public List<PEDIDOS> recuperarListaPedidos(int estado, string busqPedido, string desde, string hasta)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                if (isNumeric(busqPedido) && desde != null && hasta != null)
                {
                    int codPedido = Convert.ToInt32(busqPedido);
                    DateTime fdesde = Convert.ToDateTime(desde);
                    DateTime fhasta = Convert.ToDateTime(hasta);

                    return (from p in contexto.PEDIDOS
                            where p.PED_ESTADO == estado
                            && p.PED_CODIGO == codPedido
                            && p.PED_FECHA >= fdesde
                            && p.PED_FECHA <= fhasta
                            select p).ToList();

                }
                else if (isNumeric(busqPedido))
                {
                    int codPedido = Convert.ToInt32(busqPedido);

                    return (from p in contexto.PEDIDOS
                            where p.PED_ESTADO == estado
                            && p.PED_CODIGO == codPedido
                            select p).ToList();
                }
                else if (desde != null && hasta != null)
                {
                    DateTime fdesde = Convert.ToDateTime(desde);
                    DateTime fhasta = Convert.ToDateTime(hasta);

                    return (from p in contexto.PEDIDOS
                            where p.PED_ESTADO == estado
                            && p.PED_FECHA >= fdesde
                            && p.PED_FECHA <= fhasta
                            select p).ToList();
                }

                return (from p in contexto.PEDIDOS
                        where p.PED_ESTADO == estado
                        select p).ToList();

            }
        }

        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo PEDIDO
        /// </summary>
        /// <param name="codigoAtencion">codigo de la atencion</param>
        /// <param name="tipo">tipo de pedido</param>
        /// <returns>lista de objetos de tipo PEDIDO</returns>
        public List<PEDIDOS> recuperarListaPedidos(Int64 codigoAtencion, Int16 tipo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return
                        (from p in contexto.PEDIDOS.Include("PEDIDOS_DETALLE")
                         where p.ATE_CODIGO == codigoAtencion && p.TIP_PEDIDO == tipo
                         select p).ToList();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo PEDIDO
        /// </summary>
        /// <param name="codigoAtencion">codigo de la atencion</param>
        /// <param name="tipo">tipo de pedido</param>
        /// <param name="tipo">tipo del Area</param>
        /// <returns>lista de objetos de tipo PEDIDO</returns>
        public List<PEDIDOS> recuperarListaPedidosPorArea(Int64 codigoAtencion, Int16 tipo, Int16 codigoArea)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return
                        (from p in contexto.PEDIDOS.Include("PEDIDOS_DETALLE")
                         where p.ATE_CODIGO == codigoAtencion && p.TIP_PEDIDO == tipo && p.PEDIDOS_AREAS.PEA_CODIGO == codigoArea
                         select p).ToList();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo PEDIDO
        /// </summary>
        /// <param name="fechaIni">Fecha de inicio para el filtro</param>
        /// <param name="fechaFin">Fecha Final para el filtro</param>
        /// <param name="codigoArea">tipo del Area</param>
        /// <returns>lista de objetos de tipo PEDIDO</returns>
        public List<PEDIDOS> recuperarListaPedidosPorArea(DateTime fechaIni, DateTime fechaFin, Int16 codigoArea)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {

                    return
                        (from p in contexto.PEDIDOS.Include("PEDIDOS_DETALLE")
                         where p.PED_FECHA >= fechaIni && p.PED_FECHA <= fechaFin && p.PEDIDOS_AREAS.PEA_CODIGO == codigoArea
                         select p).ToList();

                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo PEDIDO
        /// </summary>
        /// <param name="codigoArea">codigo de Area</param>
        /// <returns>lista de objetos de tipo PEDIDO</returns>
        public List<PEDIDOS> recuperarListaPedidosPorArea(Int16 codigoArea)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return
                        (from p in contexto.PEDIDOS.Include("PEDIDOS_DETALLE")
                         where p.PEDIDOS_AREAS.PEA_CODIGO == codigoArea
                         select p).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }

        public PEDIDOS recuperarPedidoID(int codPedido)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PEDIDOS
                        where p.PED_CODIGO == codPedido
                        select p).FirstOrDefault();
            }
        }
        public List<PEDIDOS> recuperarListaPedidosAtencion(int codAtencion, int estado, string busqPedido, string desde, string hasta)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                if (isNumeric(busqPedido) && desde != null && hasta != null)
                {
                    int codPedido = Convert.ToInt32(busqPedido);
                    DateTime fdesde = Convert.ToDateTime(desde);
                    DateTime fhasta = Convert.ToDateTime(hasta);

                    return (from p in contexto.PEDIDOS
                            where p.PED_ESTADO == estado
                            && p.PED_CODIGO == codPedido
                            && p.PED_FECHA >= fdesde
                            && p.PED_FECHA <= fhasta
                            && p.ATE_CODIGO == codAtencion
                            select p).ToList();

                }
                else if (isNumeric(busqPedido))
                {
                    int codPedido = Convert.ToInt32(busqPedido);

                    return (from p in contexto.PEDIDOS
                            where p.PED_ESTADO == estado
                            && p.PED_CODIGO == codPedido
                            && p.ATE_CODIGO == codAtencion
                            select p).ToList();
                }
                else if (desde != null && hasta != null)
                {
                    DateTime fdesde = Convert.ToDateTime(desde);
                    DateTime fhasta = Convert.ToDateTime(hasta);

                    return (from p in contexto.PEDIDOS
                            where p.PED_ESTADO == estado
                            && p.PED_FECHA >= fdesde
                            && p.PED_FECHA <= fhasta
                            && p.ATE_CODIGO == codAtencion
                            select p).ToList();
                }

                return (from p in contexto.PEDIDOS
                        where p.ATE_CODIGO == codAtencion
                        && p.PED_ESTADO == estado
                        select p).ToList();
            }
        }
        public void actualizarEstadoPedido(PEDIDOS pedido, Int16 estado)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PEDIDOS opedido = contexto.PEDIDOS.FirstOrDefault(p => p.PED_CODIGO == pedido.PED_CODIGO);
                opedido.PED_ESTADO = estado;
                contexto.SaveChanges();
            }
        }
        public void actualizarEstadoPedidoDetalle(Int32 codPedido, Boolean estado)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //PEDIDOS opedido = contexto.PEDIDOS.FirstOrDefault(p => p.PED_CODIGO == codPedido);
                //opedido.PED_ESTADO = estado;
                //contexto.SaveChanges();

                //EL PEDIDO SE LO HACE ITEM POR ITEM / GIOVANNY TAPIA / 02/01/2012

                PEDIDOS_DETALLE opedido = contexto.PEDIDOS_DETALLE.FirstOrDefault(p => p.PDD_CODIGO == codPedido);
                opedido.PDD_ESTADO = estado;
                contexto.SaveChanges();

            }
        }


        public void actualizarEstadoPedido(Int32 codPedido, Int16 estado)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PEDIDOS opedido = contexto.PEDIDOS.FirstOrDefault(p => p.PED_CODIGO == codPedido);
                opedido.PED_ESTADO = estado;
                contexto.SaveChanges();

            }
        }

        #endregion

        #region PEDIDOS_DETALLES
        public Int64 ultimoCodigoPedidosDetalles()
        {
            Int64 codigo = 0;
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("select MAX(PDD_CODIGO) CODIGO from PEDIDOS_DETALLE", connection);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                codigo = Convert.ToInt64(reader["CODIGO"].ToString());
            }
            reader.Close();
            connection.Close();
            return codigo;
        }

        public Int64 CreaDevolucionQuirofano(DtoPedidoDevolucion PedidoDevolucion, Int64 CodigoAtencion, int bodega, string modulo, Int64 cue_Codigo = 0)
        {
            Int64 Resultado;
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            SqlTransaction transaction;
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

            transaction = Sqlcon.BeginTransaction();

            try
            {

                Sqlcmd = new SqlCommand("sp_GuardaDevolucionPedido", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@Ped_Codigo", SqlDbType.BigInt);
                Sqlcmd.Parameters["@Ped_Codigo"].Value = PedidoDevolucion.Ped_Codigo;

                Sqlcmd.Parameters.Add("@DevFecha", SqlDbType.DateTime);
                Sqlcmd.Parameters["@DevFecha"].Value = PedidoDevolucion.DevFecha;

                Sqlcmd.Parameters.Add("@ID_USUARIO", SqlDbType.SmallInt);
                Sqlcmd.Parameters["@ID_USUARIO"].Value = PedidoDevolucion.ID_USUARIO;

                Sqlcmd.Parameters.Add("@DevObservacion", SqlDbType.VarChar);
                Sqlcmd.Parameters["@DevObservacion"].Value = PedidoDevolucion.DevObservacion;

                Sqlcmd.Parameters.Add("@cue_Codigo", SqlDbType.BigInt);
                Sqlcmd.Parameters["@cue_Codigo"].Value = cue_Codigo;

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;

                Sqldap.Fill(Dts);

                Resultado = Convert.ToInt32(Dts.Rows[0][0]);
                foreach (var Devolucion in PedidoDevolucion.DetalleDevolucion)
                {

                    Sqlcmd = new SqlCommand("sp_GuardaPedidoDevolucionDetalleQuirofano", Sqlcon);
                    Sqlcmd.CommandType = CommandType.StoredProcedure;

                    Sqlcmd.Transaction = transaction;

                    Sqlcmd.Parameters.Add("@DevCodigo", SqlDbType.Int);
                    Sqlcmd.Parameters["@DevCodigo"].Value = Resultado;

                    Sqlcmd.Parameters.Add("@PRO_CODIGO", SqlDbType.Int);
                    Sqlcmd.Parameters["@PRO_CODIGO"].Value = Devolucion.PRO_CODIGO;

                    Sqlcmd.Parameters.Add("@PRO_DESCRIPCION", SqlDbType.VarChar);
                    Sqlcmd.Parameters["@PRO_DESCRIPCION"].Value = Devolucion.PRO_DESCRIPCION;

                    Sqlcmd.Parameters.Add("@DevDetCantidad", SqlDbType.Int);
                    Sqlcmd.Parameters["@DevDetCantidad"].Value = Devolucion.DevDetCantidad;

                    Sqlcmd.Parameters.Add("@DevDetValor", SqlDbType.Decimal);
                    Sqlcmd.Parameters["@DevDetValor"].Value = Devolucion.DevDetValor;

                    Sqlcmd.Parameters.Add("@DevDetIva", SqlDbType.Decimal);
                    Sqlcmd.Parameters["@DevDetIva"].Value = Devolucion.DevDetIva;

                    Sqlcmd.Parameters.Add("@DevDetIvaTotal", SqlDbType.Decimal);
                    Sqlcmd.Parameters["@DevDetIvaTotal"].Value = Devolucion.DevDetIvaTotal;

                    Sqlcmd.Parameters.Add("@PDD_CODIGO", SqlDbType.BigInt);
                    Sqlcmd.Parameters["@PDD_CODIGO"].Value = Devolucion.PDD_CODIGO;

                    Sqlcmd.Parameters.Add("@PED_CODIGO", SqlDbType.Int);
                    Sqlcmd.Parameters["@PED_CODIGO"].Value = PedidoDevolucion.Ped_Codigo;

                    Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
                    Sqlcmd.Parameters["@ATE_CODIGO"].Value = CodigoAtencion;

                    Sqlcmd.Parameters.Add("@OBSERVACION", SqlDbType.VarChar);
                    Sqlcmd.Parameters["@OBSERVACION"].Value = PedidoDevolucion.DevObservacion;

                    Sqlcmd.Parameters.Add("@BODEGA", SqlDbType.Int);
                    Sqlcmd.Parameters["@BODEGA"].Value = bodega;

                    Sqlcmd.Parameters.Add("@MODULO", SqlDbType.VarChar);
                    Sqlcmd.Parameters["@MODULO"].Value = modulo;

                    Sqldap = new SqlDataAdapter();
                    Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                    Sqldap.SelectCommand = Sqlcmd;

                    Sqldap.Fill(Dts);

                }

                transaction.Commit();

                return Resultado;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
                return 0;
            }
        }

        public Int32 CrearDevolucionPedido(DtoPedidoDevolucion PedidoDevolucion, Int64 CodigoAtencion, Int64 cue_Codigo = 0)
        {

            // GIOVANNY TAPIA / 07/08/201

            Int32 Resultado = 0;

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            SqlTransaction transaction;
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

            transaction = Sqlcon.BeginTransaction();

            try
            {

                Sqlcmd = new SqlCommand("sp_GuardaDevolucionPedido", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@Ped_Codigo", SqlDbType.BigInt);
                Sqlcmd.Parameters["@Ped_Codigo"].Value = PedidoDevolucion.Ped_Codigo;

                Sqlcmd.Parameters.Add("@DevFecha", SqlDbType.DateTime);
                Sqlcmd.Parameters["@DevFecha"].Value = PedidoDevolucion.DevFecha;

                Sqlcmd.Parameters.Add("@ID_USUARIO", SqlDbType.SmallInt);
                Sqlcmd.Parameters["@ID_USUARIO"].Value = PedidoDevolucion.ID_USUARIO;

                Sqlcmd.Parameters.Add("@DevObservacion", SqlDbType.VarChar);
                Sqlcmd.Parameters["@DevObservacion"].Value = PedidoDevolucion.DevObservacion;

                Sqlcmd.Parameters.Add("@cue_Codigo", SqlDbType.BigInt);
                Sqlcmd.Parameters["@cue_Codigo"].Value = cue_Codigo;

                Sqlcmd.Parameters.Add("@ip_maquina", SqlDbType.VarChar);
                Sqlcmd.Parameters["@ip_maquina"].Value = PedidoDevolucion.IP_MAQUINA;

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;

                Sqldap.Fill(Dts);

                Resultado = Convert.ToInt32(Dts.Rows[0][0]);

                foreach (var Devolucion in PedidoDevolucion.DetalleDevolucion)
                {

                    Sqlcmd = new SqlCommand("sp_GuardaPedidoDevolucionDetalle", Sqlcon);
                    Sqlcmd.CommandType = CommandType.StoredProcedure;

                    Sqlcmd.Transaction = transaction;

                    Sqlcmd.Parameters.Add("@DevCodigo", SqlDbType.Int);
                    Sqlcmd.Parameters["@DevCodigo"].Value = Resultado;

                    Sqlcmd.Parameters.Add("@PRO_CODIGO", SqlDbType.Int);
                    Sqlcmd.Parameters["@PRO_CODIGO"].Value = Devolucion.PRO_CODIGO;

                    Sqlcmd.Parameters.Add("@PRO_DESCRIPCION", SqlDbType.VarChar);
                    Sqlcmd.Parameters["@PRO_DESCRIPCION"].Value = Devolucion.PRO_DESCRIPCION;

                    Sqlcmd.Parameters.Add("@DevDetCantidad", SqlDbType.Decimal);
                    Sqlcmd.Parameters["@DevDetCantidad"].Value = Devolucion.DevDetCantidad;

                    Sqlcmd.Parameters.Add("@DevDetValor", SqlDbType.Decimal);
                    Sqlcmd.Parameters["@DevDetValor"].Value = Devolucion.DevDetValor;

                    Sqlcmd.Parameters.Add("@DevDetIva", SqlDbType.Decimal);
                    Sqlcmd.Parameters["@DevDetIva"].Value = Devolucion.DevDetIva;

                    Sqlcmd.Parameters.Add("@DevDetIvaTotal", SqlDbType.Decimal);
                    Sqlcmd.Parameters["@DevDetIvaTotal"].Value = Devolucion.DevDetIvaTotal;

                    Sqlcmd.Parameters.Add("@PDD_CODIGO", SqlDbType.BigInt);
                    Sqlcmd.Parameters["@PDD_CODIGO"].Value = Devolucion.PDD_CODIGO;

                    Sqlcmd.Parameters.Add("@PED_CODIGO", SqlDbType.Int);
                    Sqlcmd.Parameters["@PED_CODIGO"].Value = PedidoDevolucion.Ped_Codigo;

                    Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
                    Sqlcmd.Parameters["@ATE_CODIGO"].Value = CodigoAtencion;

                    Sqlcmd.Parameters.Add("@OBSERVACION", SqlDbType.VarChar);
                    Sqlcmd.Parameters["@OBSERVACION"].Value = PedidoDevolucion.DevObservacion;

                    Sqldap = new SqlDataAdapter();
                    Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                    Sqldap.SelectCommand = Sqlcmd;

                    Sqldap.Fill(Dts);

                }

                transaction.Commit();

                return Resultado;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return 0;
            }

        }

        public DataTable recuperaCodRubro(Int32 codPro)
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
            Sqlcmd = new SqlCommand("SELECT Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His, His3000.dbo.RUBROS.PED_CODIGO AS coddiv FROM Sic3000.dbo.Producto INNER JOIN Sic3000.dbo.ProductoSubdivision ON Sic3000.dbo.Producto.codsub = Sic3000.dbo.ProductoSubdivision.codsub INNER JOIN His3000.dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = His3000.dbo.RUBROS.RUB_CODIGO where Sic3000.dbo.Producto.codpro = " + codPro, Sqlcon);
            //Sqlcmd = new SqlCommand("SELECT His3000.dbo.RUBROS.RUB_ASOCIADO, His3000.dbo.RUBROS.PED_CODIGO AS coddiv FROM Sic3000.dbo.Producto INNER JOIN Sic3000.dbo.ProductoSubdivision ON Sic3000.dbo.Producto.codsub = Sic3000.dbo.ProductoSubdivision.codsub INNER JOIN His3000.dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = His3000.dbo.RUBROS.RUB_CODIGO where Sic3000.dbo.Producto.codpro = " + codPro, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts.Tables["tabla"];

        }


        public bool crearDetallePedido(List<PEDIDOS_DETALLE> ndetalle, PEDIDOS pedido, string NumVale)
        {
            bool resultado = false;
            using (HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();
                {
                    try
                    {

                        contexto.AddToPEDIDOS(pedido);
                        contexto.SaveChanges();
                        Int64 codCuenta = 0;
                        COPAGO coriginal = contexto.COPAGO.FirstOrDefault(x => x.ATE_CODIGO == pedido.ATE_CODIGO);
                        COPAGO cseguro = contexto.COPAGO.FirstOrDefault(x => x.ATE_CODIGO_COPAGO == pedido.ATE_CODIGO);
                        bool copago = false;
                        if (coriginal != null)
                            copago = true;
                        else if (cseguro != null)
                            copago = true;

                        foreach (var item in ndetalle)
                        {
                            Int64 codigo = (from p in contexto.PEDIDOS_DETALLE
                                            orderby p.PDD_CODIGO descending
                                            select p.PDD_CODIGO).FirstOrDefault();
                            item.PDD_CODIGO = codigo + 1;
                            item.PEDIDOSReference.EntityKey = pedido.EntityKey;
                            contexto.AddToPEDIDOS_DETALLE(item);
                            contexto.SaveChanges();


                            //guardo el estado de cuenta
                            String g = Convert.ToString(item.PRODUCTOReference.EntityKey.EntityKeyValues.GetValue(0));
                            Int32 codpro = Convert.ToInt32(item.PRO_CODIGO_BARRAS);
                            Int32 xcodDiv = 0;
                            Int16 XRubro = 0;
                            DataTable auxDT = recuperaCodRubro(codpro);
                            foreach (DataRow row in auxDT.Rows)
                            {
                                XRubro = Convert.ToInt16(row[0].ToString());
                                xcodDiv = Convert.ToInt32(row[1].ToString());
                            }
                            var cuenta = new CUENTAS_PACIENTES
                            {
                                ATE_CODIGO = pedido.ATE_CODIGO,
                                PRO_CODIGO = Convert.ToString(item.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value),
                                CUE_ESTADO = 1,
                                CUE_FECHA = (DateTime)pedido.PED_FECHA,
                                CUE_VALOR_UNITARIO = item.PDD_VALOR,
                                CUE_IVA = item.PDD_IVA,
                                CUE_VALOR = item.PDD_VALOR * item.PDD_CANTIDAD,
                                ID_USUARIO = pedido.ID_USUARIO,
                                PED_CODIGO = xcodDiv,
                                RUB_CODIGO = XRubro,
                                CAT_CODIGO = 0,
                                CUE_CANTIDAD = Convert.ToDecimal(item.PDD_CANTIDAD),
                                CUE_DETALLE = item.PRO_DESCRIPCION,
                                CUE_NUM_FAC = "0",
                                PRO_CODIGO_BARRAS = item.PRO_CODIGO_BARRAS,
                                MED_CODIGO = pedido.MED_CODIGO,
                                Codigo_Pedido = pedido.PED_CODIGO,
                                DivideFactura = "N",
                                FECHA_FACTURA = (DateTime)pedido.PED_FECHA,
                            };

                            CUENTAS_PACIENTES cueCodigo = contexto.CUENTAS_PACIENTES.OrderByDescending(c => c.CUE_CODIGO).FirstOrDefault();
                            codCuenta = cueCodigo != null ? cueCodigo.CUE_CODIGO + 1 : 1;
                            cuenta.CUE_CODIGO = codCuenta;
                            contexto.AddToCUENTAS_PACIENTES(cuenta);
                            contexto.SaveChanges();
                            if (copago)
                            {
                                CUENTAS_PACIENTES_COPAGO ccopago = new CUENTAS_PACIENTES_COPAGO();
                                if (coriginal != null)
                                    ccopago.ATE_CODIGO = (int?)coriginal.ATE_CODIGO;
                                else if (cseguro != null)
                                    ccopago.ATE_CODIGO = (int?)cseguro.ATE_CODIGO;
                                ccopago.CUE_CODIGO = codCuenta;
                                ccopago.PRO_CODIGO = Convert.ToString(item.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value);
                                ccopago.CUE_ESTADO = 1;
                                ccopago.CUE_FECHA = (DateTime)pedido.PED_FECHA;
                                ccopago.CUE_VALOR_UNITARIO = item.PDD_VALOR;
                                ccopago.CUE_IVA = item.PDD_IVA;
                                ccopago.CUE_VALOR = item.PDD_VALOR * item.PDD_CANTIDAD;
                                ccopago.ID_USUARIO = pedido.ID_USUARIO;
                                ccopago.PED_CODIGO = xcodDiv;
                                ccopago.RUB_CODIGO = XRubro;
                                ccopago.CAT_CODIGO = 0;
                                ccopago.CUE_CANTIDAD = Convert.ToDecimal(item.PDD_CANTIDAD);
                                ccopago.CUE_DETALLE = item.PRO_DESCRIPCION;
                                ccopago.CUE_NUM_FAC = "0";
                                ccopago.PRO_CODIGO_BARRAS = item.PRO_CODIGO_BARRAS;
                                ccopago.MED_CODIGO = pedido.MED_CODIGO;
                                ccopago.Codigo_Pedido = pedido.PED_CODIGO;
                                ccopago.DivideFactura = "N";
                                ccopago.FECHA_FACTURA = (DateTime)pedido.PED_FECHA;
                                contexto.Crear("CUENTAS_PACIENTES_COPAGO", ccopago);
                                contexto.SaveChanges();
                            }
                        }
                        transa.Commit();
                        resultado = true;
                        foreach (var item in ndetalle)
                        {
                            GuardaCostoCuentaPacientes(Convert.ToString(item.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value),
                                pedido.PED_CODIGO, NumVale);
                            //ArreglaIVA(Convert.ToString(pedido.ATE_CODIGO), codCuenta, Convert.ToString(item.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value));
                        }
                    }
                    catch (Exception ex)
                    {
                        transa.Rollback();
                        if (ex.InnerException != null)
                            Console.Write(ex.InnerException.Message);
                        resultado = false;
                        //throw;
                    }
                    ConexionEntidades.ConexionEDM.Close();
                    return resultado;
                }
            }
        }
        public static bool quirofano = false;
        public void PedidoDetalle(string codpro, string prodesc, double cantidad, double valor, double total, int ped_codigo, double iva)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection = obj.ConectarBd();
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command = new SqlCommand("sp_QuirofanoAgregarPedidoProducto", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@prodesc", prodesc);
            command.Parameters.AddWithValue("@cantidad", cantidad);
            command.Parameters.AddWithValue("@valor", valor);
            command.Parameters.AddWithValue("@total", total);
            command.Parameters.AddWithValue("@iva", iva);
            command.Parameters.AddWithValue("@ped_codigo", ped_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
        public bool nuevoPedidoProcedimientos(PEDIDOS pedido, List<PEDIDOS_DETALLE> pDetalle,
            int bodega, int pci_codigo, string NumVale)
        {
            bool resultado = false;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();
                {

                    try
                    {
                        contexto.AddToPEDIDOS(pedido);
                        contexto.SaveChanges();

                        REPOSICION_PENDIENTE repo = new REPOSICION_PENDIENTE();
                        repo.id_registro_quirofano = pci_codigo;
                        repo.ped_codigo = pedido.PED_CODIGO;
                        repo.estado = false;

                        contexto.AddToREPOSICION_PENDIENTE(repo);
                        contexto.SaveChanges();


                        foreach (var item in pDetalle)
                        {
                            item.PEDIDOSReference.EntityKey = pedido.EntityKey;
                            Int64 codigo = (from p in contexto.PEDIDOS_DETALLE
                                            orderby p.PDD_CODIGO descending
                                            select p.PDD_CODIGO).FirstOrDefault();
                            item.PDD_CODIGO = codigo + 1;
                            //item.PRODUCTO.PRO_CODIGO = Convert.ToInt32(item.PRO_CODIGO_BARRAS);
                            contexto.AddToPEDIDOS_DETALLE(item);
                            contexto.SaveChanges();

                            //REPOSICION_QUIROFANO repo = contexto.REPOSICION_QUIROFANO.OrderByDescending(c => c.RQ_CODIGO).FirstOrDefault();
                            //Int64 repos = repo.RQ_CODIGO + 1;
                            //var quirofano1 = new REPOSICION_QUIROFANO
                            //{
                            //    RQ_CODIGO = repos,
                            //    RQ_CANTIDAD = Convert.ToInt32(item.PDD_CANTIDAD),
                            //    CODPRO = item.PRO_CODIGO_BARRAS,
                            //    ATE_CODIGO = pedido.ATE_CODIGO,
                            //    PCI_CODIGO = pci_codigo,
                            //    RQ_CANTIDADADICIONAL = 0,
                            //    RQ_CANTIDADDEVOLUCION = 0,
                            //    RQ_FECHACREACION = pedido.PED_FECHA,
                            //    RQ_FECHAPEDIDO = DateTime.Now,
                            //    PED_CODIGO = pedido.PED_CODIGO,
                            //    ID_USUARIO = pedido.ID_USUARIO,

                            //};
                            //contexto.AddToREPOSICION_QUIROFANO(quirofano1);
                            //contexto.SaveChanges();

                            Int16 XRubro = 0;
                            DataTable auxDT = new DataTable();
                            auxDT = recuperaCodRubro(Convert.ToInt32(item.PRO_CODIGO_BARRAS));
                            XRubro = Convert.ToInt16(auxDT.Rows[0][0].ToString());

                            var cuentas = new CUENTAS_PACIENTES()
                            {
                                ATE_CODIGO = pedido.ATE_CODIGO,
                                CUE_FECHA = DateTime.Now,
                                FECHA_FACTURA = DateTime.Now,
                                ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario,
                                CAT_CODIGO = 0,
                                CUE_ESTADO = 1,
                                CUE_NUM_FAC = "0",
                                PED_CODIGO = 1,
                                MED_CODIGO = 0,
                                COSTO = 0,
                                DivideFactura = "N",
                                Descuento = 0,
                                Codigo_Pedido = pedido.PED_CODIGO,
                                PorDescuento = 0,
                                USUARIO_FACTURA = 0,
                                PRO_CODIGO = item.PRO_CODIGO_BARRAS,
                                PRO_CODIGO_BARRAS = item.PRO_CODIGO_BARRAS,
                                CUE_DETALLE = item.PRO_DESCRIPCION,
                                CUE_VALOR_UNITARIO = item.PDD_VALOR,
                                CUE_CANTIDAD = Convert.ToDecimal(item.PDD_CANTIDAD),
                                CUE_VALOR = item.PDD_VALOR * item.PDD_CANTIDAD,
                                CUE_IVA = item.PDD_IVA,
                                RUB_CODIGO = XRubro,
                                CUE_OBSERVACION = "CARGA AUTOMATICA DESDE PEDIDOS ESPECIALES BODEGA: " + His.Entidades.Clases.Sesion.bodega
                            };
                            CUENTAS_PACIENTES cueCodigo = contexto.CUENTAS_PACIENTES.OrderByDescending(c => c.CUE_CODIGO).FirstOrDefault();
                            Int64 codCuenta = cueCodigo != null ? cueCodigo.CUE_CODIGO + 1 : 1;
                            cuentas.CUE_CODIGO = codCuenta;

                            contexto.AddToCUENTAS_PACIENTES(cuentas);
                            contexto.SaveChanges();
                        }
                        transa.Commit();
                        resultado = true;
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                        transa.Rollback();
                    }
                    ConexionEntidades.ConexionEDM.Close();
                    return resultado;
                }
            }



            //SqlCommand command;
            //SqlConnection connection;
            //SqlTransaction transaction;
            //BaseContextoDatos obj = new BaseContextoDatos();

            //connection = obj.ConectarBd();
            //connection.Open();

            //transaction = connection.BeginTransaction();


            #region Creo el pedido
            //command = new SqlCommand("sp_PedidoInsert", connection);
            //command.CommandType = CommandType.StoredProcedure;
            //command.Transaction = transaction;
            //command.Parameters.AddWithValue("@ped_codigo", pedido.PED_CODIGO);
            //command.Parameters.AddWithValue("@pea_codigo", p_a.PEA_CODIGO);
            //command.Parameters.AddWithValue("@pee_codigo", pedido.PEE_CODIGO);
            //command.Parameters.AddWithValue("@ped_descripcion", pedido.PED_DESCRIPCION);
            //command.Parameters.AddWithValue("@ped_fecha", pedido.PED_FECHA);
            //command.Parameters.AddWithValue("@id_usuario", pedido.ID_USUARIO);
            //command.Parameters.AddWithValue("@ate_codigo", pedido.ATE_CODIGO);
            //command.Parameters.AddWithValue("@tip_pedido", pedido.TIP_PEDIDO);
            //command.Parameters.AddWithValue("@ped_prioridad", pedido.PED_PRIORIDAD);
            //command.Parameters.AddWithValue("@ped_transaccion", pedido.PED_TRANSACCION);
            //command.Parameters.AddWithValue("@med_codigo", pedido.MED_CODIGO);
            //command.Parameters.AddWithValue("@hab_codigo", pedido.HAB_CODIGO);

            //command.CommandTimeout = 180;
            //command.ExecuteNonQuery();
            //command.Parameters.Clear();
            #endregion

            #region Creo Detalle pedido y cargo a la cuenta 
            //foreach (var item in pDetalle)
            //{
            //command = new SqlCommand("sp_QuirofanoAgregarPedidoProducto", connection);
            //command.CommandType = CommandType.StoredProcedure;
            //command.Transaction = transaction;
            //command.Parameters.AddWithValue("@codpro", item.PRO_CODIGO_BARRAS);
            //command.Parameters.AddWithValue("@prodesc", item.PRO_DESCRIPCION);
            //command.Parameters.AddWithValue("@cantidad", item.PDD_CANTIDAD);
            //command.Parameters.AddWithValue("@valor", item.PDD_VALOR);
            //command.Parameters.AddWithValue("@total", item.PDD_TOTAL);
            //command.Parameters.AddWithValue("@iva", item.PDD_IVA);
            //command.Parameters.AddWithValue("@ped_codigo", pedido.PED_CODIGO);
            //command.Parameters.AddWithValue("@bodega", item.PRO_BODEGA_SIC);
            //command.CommandTimeout = 180;
            //command.ExecuteNonQuery();
            //command.Parameters.Clear();

            //    Int32 xcodDiv = 0;
            //    Int16 XRubro = 0;
            //    DataTable auxDT = new DataTable();
            //    auxDT = recuperaCodRubro(Convert.ToInt32(item.PRO_CODIGO_BARRAS));
            //    foreach (DataRow row in auxDT.Rows)
            //    {
            //        XRubro = Convert.ToInt16(row[0].ToString());
            //        xcodDiv = Convert.ToInt32(row[1].ToString());
            //    }

            //    command = new SqlCommand("sp_QuirofanoCuentaPacientes", connection);
            //    command.CommandType = CommandType.StoredProcedure;
            //    command.Transaction = transaction;
            //    command.Parameters.AddWithValue("@ate_codigo", pedido.ATE_CODIGO);
            //    command.Parameters.AddWithValue("@codpro", Convert.ToString(item.PRO_CODIGO_BARRAS));
            //    command.Parameters.AddWithValue("@cue_detalle", item.PRO_DESCRIPCION);
            //    command.Parameters.AddWithValue("@cue_valor", item.PDD_VALOR);
            //    command.Parameters.AddWithValue("@cue_cantidad", Convert.ToDecimal(item.PDD_CANTIDAD));
            //    command.Parameters.AddWithValue("@cue_total", item.PDD_VALOR * item.PDD_CANTIDAD);
            //    command.Parameters.AddWithValue("@cue_iva", item.PDD_IVA);
            //    command.Parameters.AddWithValue("@rub_codigo", XRubro);
            //    command.Parameters.AddWithValue("@id_usuario", pedido.ID_USUARIO);
            //    command.Parameters.AddWithValue("@codigo_pedido", pedido.PED_CODIGO);
            //    command.Parameters.AddWithValue("@costo", 0);
            //    command.Parameters.AddWithValue("@descripcion", NumVale); //lo uso para poder usa la descripcion

            //    command.ExecuteNonQuery();
            //    command.Parameters.Clear();
            //}

            #endregion

            #region Actualizo Bodega
            //foreach (var item in pDetalle)
            //{
            //    command = new SqlCommand("sp_QuirofanoBodega", connection);
            //    command.CommandType = CommandType.StoredProcedure;
            //    command.Transaction = transaction;
            //    command.Parameters.AddWithValue("@codpro", item.PRO_CODIGO_BARRAS);
            //    command.Parameters.AddWithValue("@existe", item.PDD_CANTIDAD);
            //    command.Parameters.AddWithValue("@bodega", bodega);
            //    command.CommandTimeout = 180;
            //    command.ExecuteNonQuery();
            //    command.Parameters.Clear();

            //}
            #endregion

            #region Actualizo Kardex
            //command = new SqlCommand("sp_ActualizaKardexSic", connection);
            //command.CommandType = CommandType.StoredProcedure;
            //command.Transaction = transaction;
            //command.Parameters.AddWithValue("@numdoc", pedido.PED_CODIGO);
            //command.Parameters.AddWithValue("@bodega", bodega);
            //command.Parameters.AddWithValue("@id_usuario", pedido.ID_USUARIO);
            //command.CommandTimeout = 180;
            //command.ExecuteNonQuery();
            //command.Parameters.Clear();

            #endregion

            #region Creo la reposicion
            //foreach (var item in pDetalle)
            //{
            //    command = new SqlCommand("sp_DatosReposicion", connection);
            //    command.CommandType = CommandType.StoredProcedure;
            //    command.Transaction = transaction;
            //    command.Parameters.AddWithValue("@ate_codigo", pedido.ATE_CODIGO);
            //    command.Parameters.AddWithValue("@pci_codigo", pci_codigo);
            //    command.Parameters.AddWithValue("@cantidad", item.PDD_CANTIDAD);
            //    command.Parameters.AddWithValue("@fechacreacion", pedido.PED_FECHA);
            //    command.Parameters.AddWithValue("@ped_codigo", pedido.PED_CODIGO);
            //    command.Parameters.AddWithValue("@codpro", item.PRO_CODIGO_BARRAS);
            //    command.Parameters.AddWithValue("@usuario", pedido.ID_USUARIO);
            //    command.CommandTimeout = 180;
            //    command.ExecuteNonQuery();
            //    command.Parameters.Clear();
            //}

            #endregion

            //    transaction.Commit();
            //    return true;

        }
        public void crearDetallePedidoQuirofano(PEDIDOS_DETALLE ndetalle, PEDIDOS pedido, Int16 Rubro, Int32 PedidoDivision, string NumVale)
        {

            if (!ValidaRepetido(Convert.ToInt64(pedido.ATE_CODIGO), 0, Convert.ToString(ndetalle.PRO_CODIGO_BARRAS), pedido.PED_CODIGO))
            {
                try
                {
                    //Pedido Detalle de Quirofano
                    PedidoDetalle(ndetalle.PRO_CODIGO_BARRAS, ndetalle.PRO_DESCRIPCION, Convert.ToDouble(ndetalle.PDD_CANTIDAD), Convert.ToDouble(ndetalle.PDD_VALOR), Convert.ToDouble(ndetalle.PDD_TOTAL), pedido.PED_CODIGO, Convert.ToDouble(ndetalle.PDD_IVA));
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
                try
                {
                    SqlCommand command;
                    SqlConnection connection;
                    BaseContextoDatos obj = new BaseContextoDatos();
                    connection = obj.ConectarBd();

                    connection.Open();

                    command = new SqlCommand("sp_QuirofanoCuentaPacientes", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ate_codigo", pedido.ATE_CODIGO);
                    command.Parameters.AddWithValue("@codpro", Convert.ToString(ndetalle.PRO_CODIGO_BARRAS));
                    command.Parameters.AddWithValue("@cue_detalle", ndetalle.PRO_DESCRIPCION);
                    command.Parameters.AddWithValue("@cue_valor", ndetalle.PDD_VALOR);
                    command.Parameters.AddWithValue("@cue_cantidad", Convert.ToDecimal(ndetalle.PDD_CANTIDAD));
                    command.Parameters.AddWithValue("@cue_total", ndetalle.PDD_VALOR * ndetalle.PDD_CANTIDAD);
                    command.Parameters.AddWithValue("@cue_iva", ndetalle.PDD_IVA);
                    command.Parameters.AddWithValue("@rub_codigo", Rubro);
                    command.Parameters.AddWithValue("@id_usuario", pedido.ID_USUARIO);
                    command.Parameters.AddWithValue("@codigo_pedido", pedido.PED_CODIGO);
                    command.Parameters.AddWithValue("@costo", 0);
                    command.Parameters.AddWithValue("@descripcion", NumVale); //lo uso para poder usa la descripcion

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    connection.Close();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }
                try
                {
                    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                    {

                        GuardaCostoCuentaPacientes(Convert.ToString(ndetalle.PRO_CODIGO_BARRAS),
                            pedido.PED_CODIGO, NumVale);


                        ArreglaIVA(Convert.ToString(pedido.ATE_CODIGO), pedido.PED_CODIGO, Convert.ToString(ndetalle.PRO_CODIGO_BARRAS));
                        quirofano = false;
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }
            }
        }
        public int ArreglaIVA(string Atencion, Int64 cueCod, string codPro)
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

            Sqlcmd = new SqlCommand("sp_ArreglaIVA", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@Atenciones", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Atenciones"].Value = (Atencion);

            Sqlcmd.Parameters.Add("@CUE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@CUE_CODIGO"].Value = (cueCod);

            Sqlcmd.Parameters.Add("@CODPRO", SqlDbType.VarChar);
            Sqlcmd.Parameters["@CODPRO"].Value = (codPro);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public int ArreglaIVABase(string Atencion)
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

            Sqlcmd = new SqlCommand("sp_ArreglaIVABase", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@ate", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ate"].Value = (Atencion);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public void VerifcaCuentasFacturadas()
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

            Sqlcmd = new SqlCommand("sp_VerifcaCuentasFacturadas", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void GuardarPedidoHonorario(Int64 pdd_codigo, Int64 ped_codigo, string codpro, string despro,
            decimal pdd_cantidad, decimal pdd_valor, decimal pdd_iva, decimal pdd_total, PEDIDOS pedido, Int16 Rubro,
            Int32 PedidoDivision, string NumVale, Int32 bodega)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("INSERT INTO PEDIDOS_DETALLE VALUES(@pdd_codigo, @ped_codigo, @codpro, @despro, @pdd_cantidad, @pdd_valor, @pdd_iva, @pdd_total, 1, 0, NULL, 0, NULL, NULL, @codpro,@bodega)", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@pdd_codigo", pdd_codigo);
            command.Parameters.AddWithValue("@ped_codigo", ped_codigo);
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@despro", despro);
            command.Parameters.AddWithValue("@pdd_cantidad", pdd_cantidad);
            command.Parameters.AddWithValue("@pdd_valor", pdd_valor);
            command.Parameters.AddWithValue("@pdd_iva", pdd_iva);
            command.Parameters.AddWithValue("@pdd_total", pdd_total);
            command.Parameters.AddWithValue("@bodega", bodega);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            connection.Close();

            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //guardo el estado de cuenta
                var cuenta = new CUENTAS_PACIENTES
                {
                    ATE_CODIGO = pedido.ATE_CODIGO,
                    PRO_CODIGO = codpro,
                    CUE_ESTADO = 1,
                    CUE_FECHA = (DateTime)pedido.PED_FECHA,
                    CUE_VALOR_UNITARIO = pdd_valor,
                    CUE_IVA = pdd_iva,
                    CUE_VALOR = pdd_valor * pdd_cantidad,
                    ID_USUARIO = pedido.ID_USUARIO,
                    PED_CODIGO = PedidoDivision,
                    RUB_CODIGO = Rubro,
                    CAT_CODIGO = 0,
                    CUE_CANTIDAD = pdd_cantidad,
                    CUE_DETALLE = despro,
                    CUE_NUM_FAC = "0",
                    PRO_CODIGO_BARRAS = codpro,
                    MED_CODIGO = pedido.MED_CODIGO,
                    Codigo_Pedido = pedido.PED_CODIGO,
                    DivideFactura = "N",
                    FECHA_FACTURA = (DateTime)pedido.PED_FECHA
                };
                Int64 codCuenta;
                CUENTAS_PACIENTES cueCodigo = contexto.CUENTAS_PACIENTES.OrderByDescending(c => c.CUE_CODIGO).FirstOrDefault();
                codCuenta = cueCodigo != null ? cueCodigo.CUE_CODIGO + 1 : 1;
                cuenta.CUE_CODIGO = codCuenta;
                contexto.AddToCUENTAS_PACIENTES(cuenta);
                contexto.SaveChanges();
                CostoCPHonorario(codpro, NumVale, pdd_cantidad, ped_codigo, codCuenta);
                //GuardaCostoCuentaPacientes(codpro, pedido.PED_CODIGO, NumVale);
            }
        }


        public void CostoCPHonorario(string codpro, string NumVale, decimal cantidad, Int64 ped_codigo, Int64 cue_codigo)
        {
            decimal costo = 0;
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("SELECT cospro from Sic3000..Producto where codpro=@CODPRO", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@CODPRO", codpro);
            command.CommandTimeout = 180;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                costo = Convert.ToDecimal(reader["cospro"].ToString());
            }
            reader.Close();
            command.Parameters.Clear();

            command = new SqlCommand("UPDATE CUENTAS_PACIENTES SET costo=	round((@CostoPromedio*@Cantidad),2), NumVale = @NumVale WHERE PRO_CODIGO = @CODPRO AND Codigo_Pedido = @CODPED AND CUE_CODIGO = @cue_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@CostoPromedio", costo);
            command.Parameters.AddWithValue("@Cantidad", cantidad);
            command.Parameters.AddWithValue("@NumVale", NumVale);
            command.Parameters.AddWithValue("@CODPRO", codpro);
            command.Parameters.AddWithValue("@CODPED", ped_codigo);
            command.Parameters.AddWithValue("@cue_codigo", cue_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
        //INGRESA COSTO A CUENTA PACIENTES PABLO ROCHA 20/10/2014
        public int GuardaCostoCuentaPacientes(string codpro, Int64 codped, string NumVale)
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
            Sqlcmd = new SqlCommand("sp_GuardaCostoCuentaPacientes", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CODPRO", SqlDbType.VarChar);
            Sqlcmd.Parameters["@CODPRO"].Value = codpro;

            Sqlcmd.Parameters.Add("@CODPED", SqlDbType.BigInt);
            Sqlcmd.Parameters["@CODPED"].Value = codped;

            Sqlcmd.Parameters.Add("@NumVale", SqlDbType.VarChar);
            Sqlcmd.Parameters["@NumVale"].Value = NumVale;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            return 1;
        }

        public int GuardaDetalleHonorarios(DtoDetalleHonorariosMedicos DetalleHonorarios)
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
            Sqlcmd = new SqlCommand("sp_Guarda_PEDIDO_DETALLE_MEDICO", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@PED_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@PED_CODIGO"].Value = DetalleHonorarios.PED_CODIGO;

            Sqlcmd.Parameters.Add("@ID_LINEA", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ID_LINEA"].Value = DetalleHonorarios.ID_LINEA;

            Sqlcmd.Parameters.Add("@CODPRO", SqlDbType.VarChar);
            Sqlcmd.Parameters["@CODPRO"].Value = DetalleHonorarios.CODPRO;

            Sqlcmd.Parameters.Add("@MED_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@MED_CODIGO"].Value = DetalleHonorarios.MED_CODIGO;

            Sqlcmd.Parameters.Add("@FECHA", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FECHA"].Value = DetalleHonorarios.FECHA;

            Sqlcmd.Parameters.Add("@VALOR", SqlDbType.Decimal);
            Sqlcmd.Parameters["@VALOR"].Value = DetalleHonorarios.VALOR;

            Sqlcmd.Parameters.Add("@CODIGO2", SqlDbType.VarChar);
            Sqlcmd.Parameters["@CODIGO2"].Value = "";

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            return 1;

        }



        public List<PEDIDOS_DETALLE> RecuperarDetallePedido(int codPedido)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from pd in contexto.PEDIDOS_DETALLE
                        join p in contexto.PEDIDOS on pd.PEDIDOS.PED_CODIGO equals p.PED_CODIGO
                        join pr in contexto.PRODUCTO on pd.PRODUCTO.PRO_CODIGO equals pr.PRO_CODIGO
                        where p.PED_CODIGO == codPedido
                        select pd).ToList();
            }
        }
        public void actulizarEstadoDetallePedido(Int64 codDetalle, bool estado)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PEDIDOS_DETALLE detalle = contexto.PEDIDOS_DETALLE.FirstOrDefault(p => p.PDD_CODIGO == codDetalle);
                detalle.PDD_ESTADO = estado;
                contexto.SaveChanges();
            }
        }

        public int PermiososUsuario(int IdUsuario, string ParametroBusqueda)
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
            Sqlcmd = new SqlCommand("sp_VerificaPermisos", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@IdUsuario", SqlDbType.Int);
            Sqlcmd.Parameters["@IdUsuario"].Value = IdUsuario;

            Sqlcmd.Parameters.Add("@FiltroAcceso", SqlDbType.VarChar);
            Sqlcmd.Parameters["@FiltroAcceso"].Value = ParametroBusqueda;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            if (Dts.Rows.Count > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        #endregion

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

        #region AREAS_PEDIDOS
        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo PEDIDO_AREAS
        /// </summary>
        /// <returns>lista de objetos de tipo PEDIDOS_AREAS</returns>
        public List<PEDIDOS_AREAS> recuperarListaAreas()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from a in contexto.PEDIDOS_AREAS
                            where a.PEA_ESTADO == true
                            select a
                    ).OrderBy(a => a.PEA_ORDEN_IMPR).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }

        public List<NIVEL_PISO> recuperarListaPisos()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from a in contexto.NIVEL_PISO
                            select a
                    ).OrderBy(a => a.NIV_CODIGO).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }

        public List<PEDIDOS_AREAS> recuperarListaAreasTodas()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from a in contexto.PEDIDOS_AREAS
                            select a
                    ).OrderBy(a => a.PEA_NOMBRE).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }

        public List<PEDIDOS_AREAS> RecuperarListaServicios() //solo para la pasteur 
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from a in contexto.PEDIDOS_AREAS
                            where a.PEA_NOMBRE == "SERVICIOS HOSPITALARIOS" || a.PEA_NOMBRE == "SERVICIOS EXTERNOS"
                            || a.PEA_NOMBRE == "TODAS LAS AREAS"
                            select a).OrderBy(a => a.PEA_NOMBRE).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public List<PEDIDOS_ESTACIONES> recuperarListaEstaciones()
        //{
        //    try
        //    {
        //        using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //        {
        //            return (from a in contexto.PEDIDOS_ESTACIONES
        //                    where a.PEE_ESTADO == true
        //                    select a
        //            ).OrderBy(a => a.PEE_NOMBRE).ToList();
        //        }
        //    }
        //    catch (Exception err) { throw err; }
        //}

        /// <summary>
        /// Metodo que devuelve una instancia de PEDIDOS_AREAS recibiendo como parametro el codigo 
        /// </summary>
        /// <param name="codigoArea">codigo del Area</param>
        /// <returns>devuelve un objeto PEDIDOS_AREAS</returns>
        public PEDIDOS_AREAS recuperarAreaPorID(Int16 codigoArea)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.PEDIDOS_AREAS.Where(a => a.PEA_CODIGO == codigoArea).FirstOrDefault();
                }
            }
            catch (Exception err) { throw err; }
        }
        /// <summary>
        /// Metodo que devuelve las Areas a las q el usuario puede acceder
        /// </summary>
        /// <param name="codigoUsuario">codigo del usuario</param>
        /// <returns>Lista de objetos PEDIDOS_AREAS</returns>
        public List<PEDIDOS_AREAS> recuperarListaAreasPorUsuario(int codigoUsuario)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from a in contexto.PEDIDOS_AREAS
                            join u in contexto.PEDIDOS_AREAS_USUARIOS on a.PEA_CODIGO equals u.PEA_CODIGO
                            where u.ID_USUARIO == codigoUsuario
                            select a).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }
        #endregion
        #region PEDIDOS_VISTA
        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo DtoPedidos
        /// </summary>
        /// <param name="codigoArea">codigo de Area</param>
        /// <returns>lista de objetos de tipo DtoPedidos</returns>
        public List<DtoPedidos> recuperarListaPedidosVistaPendientesPorArea(Int16 codigoArea, DateTime fechaIni, DateTime fechaFin, byte estado)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return
                        (from p in contexto.PEDIDOS
                         join d in contexto.PEDIDOS_DETALLE on p.PED_CODIGO equals d.PEDIDOS.PED_CODIGO
                         join e in contexto.PEDIDOS_ESTACIONES on p.PEE_CODIGO equals e.PEE_CODIGO
                         join u in contexto.USUARIOS on p.ID_USUARIO equals u.ID_USUARIO
                         join a in contexto.ATENCIONES on p.ATE_CODIGO equals a.ATE_CODIGO
                         join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                         where p.PEDIDOS_AREAS.PEA_CODIGO == codigoArea && p.PED_FECHA >= fechaIni && p.PED_FECHA <= fechaFin
                         select new DtoPedidos
                         {
                             PED_CODIGO = p.PED_CODIGO,
                             PEA_CODIGO = codigoArea,
                             PEE_CODIGO = e.PEE_CODIGO,
                             ESTACION = e.PEE_NOMBRE,
                             PED_DESCRIPCION = p.PED_DESCRIPCION,
                             PED_ESTADO = p.PED_ESTADO,
                             ESTADO = null,
                             PED_FECHA = p.PED_FECHA.Value,
                             ID_USUARIO = p.ID_USUARIO.Value,
                             USUARIO = (u.APELLIDOS + "  " + u.NOMBRES),
                             HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
                             ATE_CODIGO = p.ATE_CODIGO.Value,
                             PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
                             TIP_PEDIDO = p.TIP_PEDIDO.Value,
                             TIPO = null,
                             PDD_CODIGO = d.PDD_CODIGO,
                             PRO_CODIGO = d.PRODUCTO.PRO_CODIGO,
                             PRO_DESCRIPCION = d.PRO_DESCRIPCION,
                             PDD_CANTIDAD = d.PDD_CANTIDAD.Value,
                             PDD_VALOR = d.PDD_VALOR.Value,
                             PDD_IVA = d.PDD_IVA.Value,
                             PDD_TOTAL = d.PDD_TOTAL.Value,
                             PDD_ESTADO = d.PDD_ESTADO.Value
                         }).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }
        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo DtoPedidos
        /// </summary>
        /// <param name="codigoArea">codigo de Area</param>
        /// <returns>lista de objetos de tipo DtoPedidos</returns>
        public List<DtoPedidos> recuperarListaPedidosVistaPendientesPorArea(Int16 codigoArea)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return
                        (from p in contexto.PEDIDOS
                         join d in contexto.PEDIDOS_DETALLE on p.PED_CODIGO equals d.PEDIDOS.PED_CODIGO
                         join e in contexto.PEDIDOS_ESTACIONES on p.PEE_CODIGO equals e.PEE_CODIGO
                         join u in contexto.USUARIOS on p.ID_USUARIO equals u.ID_USUARIO
                         join a in contexto.ATENCIONES on p.ATE_CODIGO equals a.ATE_CODIGO
                         join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                         where p.PEDIDOS_AREAS.PEA_CODIGO == codigoArea
                         select new DtoPedidos
                         {
                             PED_CODIGO = p.PED_CODIGO,
                             PEA_CODIGO = codigoArea,
                             PEE_CODIGO = e.PEE_CODIGO,
                             ESTACION = e.PEE_NOMBRE,
                             PED_DESCRIPCION = p.PED_DESCRIPCION,
                             PED_ESTADO = p.PED_ESTADO,
                             ESTADO = null,
                             PED_FECHA = p.PED_FECHA.Value,
                             ID_USUARIO = p.ID_USUARIO.Value,
                             USUARIO = (u.APELLIDOS + "  " + u.NOMBRES),
                             HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
                             ATE_CODIGO = p.ATE_CODIGO.Value,
                             PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
                             TIP_PEDIDO = p.TIP_PEDIDO.Value,
                             TIPO = null,
                             PDD_CODIGO = d.PDD_CODIGO,
                             PRO_CODIGO = d.PRODUCTO.PRO_CODIGO,
                             PRO_DESCRIPCION = d.PRO_DESCRIPCION,
                             PDD_CANTIDAD = d.PDD_CANTIDAD.Value,
                             PDD_VALOR = d.PDD_VALOR.Value,
                             PDD_IVA = d.PDD_IVA.Value,
                             PDD_TOTAL = d.PDD_TOTAL.Value,
                             PDD_ESTADO = d.PDD_ESTADO.Value
                         }).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }
        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo DtoPedidos
        /// </summary>
        /// <param name="codigoArea">codigo de Area</param>
        /// <param name="codigoArea">codigo de la Atencion</param>
        /// <returns>lista de objetos de tipo DtoPedidos</returns>
        public List<DtoPedidos> recuperarListaPedidosVistaPendientesPorArea(Int16 codigoArea, int codigoAtencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return
                        (from a in contexto.ATENCIONES
                         join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                         join c in contexto.CUENTAS_PACIENTES on a.ATE_CODIGO equals c.ATE_CODIGO
                         join u in contexto.USUARIOS on c.ID_USUARIO equals u.ID_USUARIO
                         join m in contexto.MEDICOS on c.MED_CODIGO equals m.MED_CODIGO
                         where c.RUB_CODIGO == codigoArea && c.ATE_CODIGO == codigoAtencion
                         orderby c.PED_CODIGO descending
                         select new DtoPedidos
                         {
                             PED_CODIGO = c.CUE_CODIGO,
                             PEA_CODIGO = codigoArea,
                             PEE_CODIGO = 0,
                             ESTACION = "",
                             PED_DESCRIPCION = c.CUE_DETALLE,
                             PED_ESTADO = 1,
                             ESTADO = null,
                             PED_FECHA = c.CUE_FECHA.Value,
                             ID_USUARIO = u.ID_USUARIO,
                             USUARIO = (u.APELLIDOS + "  " + u.NOMBRES),
                             HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
                             ATE_CODIGO = a.ATE_CODIGO,
                             PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
                             TIP_PEDIDO = 3,
                             TIPO = null,
                             PDD_CODIGO = c.CUE_CODIGO,
                             PRO_CODIGO = 0,
                             PRO_DESCRIPCION = c.CUE_DETALLE,
                             PDD_CANTIDAD = 0,
                             PDD_VALOR = 0,
                             PDD_IVA = 0,
                             PDD_TOTAL = 0,
                             PDD_ESTADO = true,
                             MedicoDatos = m.MED_APELLIDO_PATERNO + " " + m.MED_APELLIDO_MATERNO + " " + m.MED_NOMBRE1 + " " + m.MED_NOMBRE2
                             //PED_FECHA2 = c.CUE_FECHA.Value
                         }).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }

        public List<DtoPedidos> ListaPedidos(int codigoArea, int codigoAtencion)
        {
            List<DtoPedidos> lista = new List<DtoPedidos>();
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            BaseContextoDatos obj = new BaseContextoDatos();
            try
            {
                con = obj.ConectarBd();
                cmd = new SqlCommand("sp_ListaPedidoPaciente", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigoArea", codigoArea);
                cmd.Parameters.AddWithValue("@codigoAtencion", codigoAtencion);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DtoPedidos objPacientes = new DtoPedidos();
                        objPacientes.PED_CODIGO = Convert.ToInt64(dr["PED_CODIGO"].ToString());
                        objPacientes.PEA_CODIGO = Convert.ToInt64(dr["PEA_CODIGO"].ToString());
                        objPacientes.PEE_CODIGO = Convert.ToInt64(dr["PEE_CODIGO"].ToString());
                        objPacientes.ESTACION = "";
                        objPacientes.PED_DESCRIPCION = dr["PED_DESCRIPCION"].ToString();
                        objPacientes.PED_ESTADO = 1;
                        objPacientes.ESTADO = null;
                        objPacientes.PED_FECHA = Convert.ToDateTime(dr["PED_FECHA"].ToString());
                        objPacientes.ID_USUARIO = Convert.ToInt64(dr["ID_USUARIO"].ToString());
                        objPacientes.USUARIO = dr["USUARIO"].ToString();
                        objPacientes.HISTORIA_CLINICA = dr["HISTORIA_CLINICA"].ToString();
                        objPacientes.ATE_CODIGO = Convert.ToInt64(dr["ATE_CODIGO"].ToString());
                        objPacientes.PACIENTE = dr["PACIENTE"].ToString();
                        objPacientes.TIP_PEDIDO = 3;
                        objPacientes.TIPO = null;
                        objPacientes.PDD_CODIGO = Convert.ToInt64(dr["PDD_CODIGO"].ToString());
                        objPacientes.PRO_CODIGO = Convert.ToInt64(dr["PRO_CODIGO"].ToString());
                        objPacientes.PRO_DESCRIPCION = dr["PRO_DESCRIPCION"].ToString();
                        objPacientes.PDD_CANTIDAD = Convert.ToDecimal(dr["CUE_CANTIDAD"].ToString());
                        objPacientes.PDD_VALOR = 0;
                        objPacientes.PDD_IVA = 0;
                        objPacientes.PDD_TOTAL = 0;
                        objPacientes.PDD_ESTADO = true;
                        objPacientes.MEDICO = dr["MEDICO"].ToString();
                        objPacientes.fechaPablo = dr["PED_FECHA"].ToString();
                        objPacientes.CANT_DEV = dr["CANT. DEV"].ToString();
                        lista.Add(objPacientes);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return lista;
        }
        public List<DtoPedidos> ListaPedidosTodosRubros(int codigoAtencion)
        {
            List<DtoPedidos> lista = new List<DtoPedidos>();
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            BaseContextoDatos obj = new BaseContextoDatos();
            try
            {
                con = obj.ConectarBd();
                cmd = new SqlCommand("sp_ListaPedidoPacienteTodosRubros", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigoAtencion", codigoAtencion);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DtoPedidos objPacientes = new DtoPedidos();
                        objPacientes.PED_CODIGO = Convert.ToInt64(dr["PED_CODIGO"].ToString());
                        objPacientes.PEA_CODIGO = Convert.ToInt64(dr["PEA_CODIGO"].ToString());
                        objPacientes.PEE_CODIGO = Convert.ToInt64(dr["PEE_CODIGO"].ToString());
                        objPacientes.ESTACION = "";
                        objPacientes.PED_DESCRIPCION = dr["PED_DESCRIPCION"].ToString();
                        objPacientes.PED_ESTADO = 1;
                        objPacientes.ESTADO = null;
                        objPacientes.PED_FECHA = Convert.ToDateTime(dr["PED_FECHA"].ToString());
                        objPacientes.ID_USUARIO = Convert.ToInt64(dr["ID_USUARIO"].ToString());
                        objPacientes.USUARIO = dr["USUARIO"].ToString();
                        objPacientes.HISTORIA_CLINICA = dr["HISTORIA_CLINICA"].ToString();
                        objPacientes.ATE_CODIGO = Convert.ToInt64(dr["ATE_CODIGO"].ToString());
                        objPacientes.PACIENTE = dr["PACIENTE"].ToString();
                        objPacientes.TIP_PEDIDO = 3;
                        objPacientes.TIPO = null;
                        objPacientes.PDD_CODIGO = Convert.ToInt64(dr["PDD_CODIGO"].ToString());
                        objPacientes.PRO_CODIGO = Convert.ToInt64(dr["PRO_CODIGO"].ToString());
                        objPacientes.PRO_DESCRIPCION = dr["PRO_DESCRIPCION"].ToString();
                        objPacientes.PDD_CANTIDAD = Convert.ToDecimal(dr["CUE_CANTIDAD"].ToString());
                        objPacientes.PDD_VALOR = 0;
                        objPacientes.PDD_IVA = 0;
                        objPacientes.PDD_TOTAL = 0;
                        objPacientes.PDD_ESTADO = true;
                        objPacientes.MEDICO = dr["MEDICO"].ToString();
                        objPacientes.fechaPablo = dr["PED_FECHA"].ToString();
                        objPacientes.CANT_DEV = dr["CANT. DEV"].ToString();
                        lista.Add(objPacientes);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return lista;
        }


        //public List<DtoPedidos> recuperarListaPedidosAtencion(string id, string historia, string nombre, int cantidad, int estado)
        //{
        //    try
        //    {
        //        //List<DtoPacientesEmergencia> pacientes = new List<DtoPacientesEmergencia>();
        //        using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //        {
        //            if (id == string.Empty && historia == string.Empty && nombre == string.Empty)
        //            {
        //                //lista de pacientes con atenciones activas
        //                var result = (from a in contexto.ATENCIONES
        //                              join p in contexto.PEDIDOS on a.ATE_CODIGO equals p.ATE_CODIGO
        //                              join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
        //                              where a.ATE_ESTADO == true
        //                              select new DtoPedidos
        //                              {
        //                                  ATE_CODIGO = p.ATE_CODIGO.Value,
        //                                  PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
        //                                  PED_FECHA = a.ATE_FECHA_INGRESO.Value,
        //                                  PRO_CODIGO = pa.PAC_CODIGO,
        //                                  IDENTIFICACION = pa.PAC_IDENTIFICACION,
        //                                  HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA
        //                              }).Distinct();
        //                return result.Take(cantidad).ToList();
        //            }
        //            else
        //            {
        //                var result = (from a in contexto.ATENCIONES
        //                              join p in contexto.PEDIDOS on a.ATE_CODIGO equals p.ATE_CODIGO
        //                              join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
        //                              where a.ATE_ESTADO == true
        //                              select new DtoPedidos
        //                              {
        //                                  ATE_CODIGO = p.ATE_CODIGO.Value,
        //                                  PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
        //                                  PED_FECHA = a.ATE_FECHA_INGRESO.Value,
        //                                  PRO_CODIGO = pa.PAC_CODIGO,
        //                                  IDENTIFICACION = pa.PAC_IDENTIFICACION,
        //                                  HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA
        //                              }).Distinct();

        //                if (id != string.Empty)
        //                    result = result.Where(pa => (pa.IDENTIFICACION).StartsWith(id)).Distinct();

        //                if (historia != string.Empty)
        //                    result = result.Where(pa => (pa.HISTORIA_CLINICA).Trim().Contains(historia)).Distinct();

        //                if (nombre != string.Empty)
        //                {
        //                    string[] cadena = nombre.Split();

        //                    if (cadena.Length == 1)
        //                    {
        //                        string n = cadena[0].Trim();
        //                        var porApellidoPaterno = result.Where(pa => (pa.PACIENTE).StartsWith(n)).OrderBy(pa => pa.PACIENTE).Take(cantidad).Distinct().ToList();
        //                        if (porApellidoPaterno.Count == 0)
        //                        {
        //                            var porNombreUno = result.Where(p => (p.PACIENTE).StartsWith(n)).OrderBy(pa => pa.PACIENTE).Take(cantidad).Distinct().ToList();
        //                            if (porNombreUno.Count > 0)
        //                            {
        //                                return porNombreUno;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            return porApellidoPaterno;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        for (int i = 0; i < cadena.Length; i++)
        //                        {
        //                            string n = cadena[i].Trim();
        //                            result = result.Where(pa => (pa.PACIENTE).Contains(n)).Distinct();
        //                        }
        //                    }
        //                }
        //                return result.OrderBy(pa => pa.PACIENTE).Take(cantidad).Distinct().ToList();
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }
        //}


        public List<PEDIDOS_DETALLE> RecuperaDetallePedidos(int NumeroPedido)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.PEDIDOS_DETALLE.Where(a => a.PEDIDOS.PED_CODIGO == NumeroPedido).ToList();
            }
        }

        public List<DtoPedidos> recuperarListaCuentaAtencionTodos(string id, string historia, string nombre, int cantidad, int estado)
        {
            try
            {
                //List<DtoPacientesEmergencia> pacientes = new List<DtoPacientesEmergencia>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    if (id == string.Empty && historia == string.Empty && nombre == string.Empty)
                    {
                        //lista de pacientes con atenciones activas
                        var result = (from a in contexto.ATENCIONES
                                      join c in contexto.CUENTAS_PACIENTES on a.ATE_CODIGO equals c.ATE_CODIGO
                                      join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                                      join e in contexto.ESTADOS_CUENTA on a.ESC_CODIGO equals e.ESC_CODIGO
                                      where a.ESC_CODIGO == 3 || a.ESC_CODIGO == 2 || a.ESC_CODIGO == 1
                                      select new DtoPedidos
                                      {
                                          CODIGO = pa.PAC_CODIGO,
                                          ATE_CODIGO = c.ATE_CODIGO.Value,
                                          PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
                                          PED_FECHA = a.ATE_FECHA_INGRESO.Value,
                                          PED_FECHA_ALTA = a.ATE_FECHA_ALTA.Value != null ? a.ATE_FECHA_ALTA.Value : DateTime.Now,
                                          PRO_CODIGO = pa.PAC_CODIGO,
                                          IDENTIFICACION = pa.PAC_IDENTIFICACION,
                                          HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
                                          ATE_NUMERO = a.ATE_NUMERO_ATENCION, // aumento el numero de atencion para ser utilizado en vez de codigo /30/10/2012 / GIOVANNY TAPIA
                                          ESC_CODIGO = a.ESC_CODIGO.Value,
                                          ESC_DESCRIPCION = e.ESC_NOMBRE
                                      }).Distinct();
                        return result.Take(cantidad).OrderBy(c => c.PED_FECHA).ToList();
                    }
                    else
                    {
                        var result = (from a in contexto.ATENCIONES
                                      join c in contexto.CUENTAS_PACIENTES on a.ATE_CODIGO equals c.ATE_CODIGO
                                      join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                                      where a.ESC_CODIGO == 3 || a.ESC_CODIGO == 2 || a.ESC_CODIGO == 1
                                      select new DtoPedidos
                                      {
                                          CODIGO = pa.PAC_CODIGO,
                                          ATE_CODIGO = c.ATE_CODIGO.Value,
                                          PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
                                          PED_FECHA = a.ATE_FECHA_INGRESO.Value,
                                          PED_FECHA_ALTA = a.ATE_FECHA_ALTA.Value != null ? a.ATE_FECHA_ALTA.Value : DateTime.Now,
                                          PRO_CODIGO = pa.PAC_CODIGO,
                                          IDENTIFICACION = pa.PAC_IDENTIFICACION,
                                          HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
                                          ATE_NUMERO = a.ATE_NUMERO_ATENCION, // aumento el numero de atencion para ser utilizado en vez de codigo /30/10/2012 / GIOVANNY TAPIA
                                          ESC_CODIGO = a.ESC_CODIGO.Value
                                      }).Distinct();

                        if (id != string.Empty)
                            result = result.Where(pa => (pa.IDENTIFICACION).StartsWith(id));

                        if (historia != string.Empty)
                            result = result.Where(pa => (pa.HISTORIA_CLINICA).Trim().Contains(historia));

                        if (nombre != string.Empty)
                        {
                            string[] cadena = nombre.Split();
                            if (cadena.Length == 1)
                            {
                                string n = cadena[0].Trim();
                                var porApellidoPaterno = result.Where(pa => (pa.PACIENTE).StartsWith(n)).OrderBy(pa => pa.PACIENTE).Take(cantidad).ToList();
                                if (porApellidoPaterno.Count == 0)
                                {
                                    var porNombreUno = result.Where(pa => (pa.PACIENTE).StartsWith(n)).OrderBy(pa => pa.PACIENTE).Take(cantidad).ToList();
                                    if (porNombreUno.Count > 0)
                                    {
                                        return porNombreUno;
                                    }
                                }
                                else
                                {
                                    return porApellidoPaterno;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < cadena.Length; i++)
                                {
                                    string n = cadena[i].Trim();
                                    result = result.Where(pa => (pa.PACIENTE).Contains(n)).Distinct();
                                }
                            }
                        }
                        return result.OrderBy(pa => pa.PACIENTE).Take(cantidad).Distinct().OrderByDescending(c => c.ATE_CODIGO).ToList();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<DtoPedidos> recuperarListaCuentaAtencion(string id, string historia, string nombre, int cantidad, int estado)
        {
            try
            {
                //List<DtoPacientesEmergencia> pacientes = new List<DtoPacientesEmergencia>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    if (id == string.Empty && historia == string.Empty && nombre == string.Empty)
                    {
                        //lista de pacientes con atenciones activas
                        var result = (from a in contexto.ATENCIONES
                                      join c in contexto.CUENTAS_PACIENTES on a.ATE_CODIGO equals c.ATE_CODIGO
                                      join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                                      join e in contexto.ESTADOS_CUENTA on a.ESC_CODIGO equals e.ESC_CODIGO
                                      where a.ESC_CODIGO == 3 //&& c.CUE_ESTADO == 1
                                      select new DtoPedidos
                                      {
                                          CODIGO = pa.PAC_CODIGO,
                                          ATE_CODIGO = c.ATE_CODIGO.Value,
                                          PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
                                          PED_FECHA = a.ATE_FECHA_INGRESO.Value,
                                          PED_FECHA_ALTA = a.ATE_FECHA_ALTA.Value != null ? a.ATE_FECHA_ALTA.Value : DateTime.Now,
                                          PRO_CODIGO = pa.PAC_CODIGO,
                                          IDENTIFICACION = pa.PAC_IDENTIFICACION,
                                          HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
                                          ATE_NUMERO = a.ATE_NUMERO_ATENCION, // aumento el numero de atencion para ser utilizado en vez de codigo /30/10/2012 / GIOVANNY TAPIA
                                          ESC_CODIGO = a.ESC_CODIGO.Value,
                                          ESC_DESCRIPCION = e.ESC_NOMBRE
                                      }).Distinct();
                        return result.Take(cantidad).OrderBy(c => c.PED_FECHA).ToList();
                    }
                    else
                    {
                        var result = (from a in contexto.ATENCIONES
                                      join c in contexto.CUENTAS_PACIENTES on a.ATE_CODIGO equals c.ATE_CODIGO
                                      join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                                      where a.ESC_CODIGO == 3 //&& c.CUE_ESTADO == 1
                                      select new DtoPedidos
                                      {
                                          CODIGO = pa.PAC_CODIGO,
                                          ATE_CODIGO = c.ATE_CODIGO.Value,
                                          PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
                                          PED_FECHA = a.ATE_FECHA_INGRESO.Value,
                                          PED_FECHA_ALTA = a.ATE_FECHA_ALTA.Value != null ? a.ATE_FECHA_ALTA.Value : DateTime.Now,
                                          PRO_CODIGO = pa.PAC_CODIGO,
                                          IDENTIFICACION = pa.PAC_IDENTIFICACION,
                                          HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
                                          ATE_NUMERO = a.ATE_NUMERO_ATENCION, // aumento el numero de atencion para ser utilizado en vez de codigo /30/10/2012 / GIOVANNY TAPIA
                                          ESC_CODIGO = a.ESC_CODIGO.Value
                                      }).Distinct();

                        if (id != string.Empty)
                            result = result.Where(pa => (pa.IDENTIFICACION).StartsWith(id));

                        if (historia != string.Empty)
                            result = result.Where(pa => (pa.HISTORIA_CLINICA).Trim().Contains(historia));

                        if (nombre != string.Empty)
                        {
                            string[] cadena = nombre.Split();
                            if (cadena.Length == 1)
                            {
                                string n = cadena[0].Trim();
                                var porApellidoPaterno = result.Where(pa => (pa.PACIENTE).StartsWith(n)).OrderBy(pa => pa.PACIENTE).Take(cantidad).ToList();
                                if (porApellidoPaterno.Count == 0)
                                {
                                    var porNombreUno = result.Where(pa => (pa.PACIENTE).StartsWith(n)).OrderBy(pa => pa.PACIENTE).Take(cantidad).ToList();
                                    if (porNombreUno.Count > 0)
                                    {
                                        return porNombreUno;
                                    }
                                }
                                else
                                {
                                    return porApellidoPaterno;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < cadena.Length; i++)
                                {
                                    string n = cadena[i].Trim();
                                    result = result.Where(pa => (pa.PACIENTE).Contains(n)).Distinct();
                                }
                            }
                        }
                        return result.OrderBy(pa => pa.PACIENTE).Take(cantidad).Distinct().OrderByDescending(c => c.ATE_CODIGO).ToList();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        // Recupera el listado de pacientes en estado por radicar para ser facturados / Giovanny Tapia / 16/01/2012

        //public List<DtoPedidos> recuperarListaPacientesFacturacion(string id, string historia, string nombre, int cantidad, int estado)
        //{
        //    try
        //    {
        //        //List<DtoPacientesEmergencia> pacientes = new List<DtoPacientesEmergencia>();
        //        using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //        {
        //            if (id == string.Empty && historia == string.Empty && nombre == string.Empty)
        //            {
        //                //lista de pacientes con atenciones activas
        //                var result = (from a in contexto.ATENCIONES
        //                              /*join c in contexto.CUENTAS_PACIENTES on a.ATE_CODIGO equals c.ATE_CODIGO*/
        //                              join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
        //                              join atc in contexto.ATENCION_DETALLE_CATEGORIAS on a.ATE_CODIGO equals atc.ATENCIONES.ATE_CODIGO
        //                              join cc in contexto.CATEGORIAS_CONVENIOS on atc.CATEGORIAS_CONVENIOS.CAT_CODIGO equals cc.CAT_CODIGO
        //                              join h in contexto.HABITACIONES on a.HABITACIONES.hab_Codigo equals h.hab_Codigo 

        //                              /*where a.ESC_CODIGO ==5 && c.CUE_ESTADO == 1*/
        //                              select new DtoPedidos
        //                              {
        //                                  CODIGO = pa.PAC_CODIGO,
        //                                  ATE_CODIGO = a.ATE_CODIGO,
        //                                  PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
        //                                  PED_FECHA = a.ATE_FECHA_INGRESO.Value,
        //                                  PED_FECHA_ALTA = a.ATE_FECHA_ALTA.Value != null ? a.ATE_FECHA_ALTA.Value : a.ATE_FECHA_INGRESO.Value,
        //                                  PRO_CODIGO = pa.PAC_CODIGO,
        //                                  IDENTIFICACION = pa.PAC_IDENTIFICACION,
        //                                  HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
        //                                  HABITACION = h.hab_Numero, 
        //                                  ATE_NUMERO = a.ATE_NUMERO_ATENCION, // aumento el numero de atencion para ser utilizado en vez de codigo /30/10/2012 / GIOVANNY TAPIA
        //                                  PRO_DESCRIPCION=cc.CAT_NOMBRE
        //                              }).OrderByDescending(c => c.PED_FECHA).Take(cantidad).ToList();

        //                              return result.ToList();
        //            }
        //            else
        //            {
        //                var result = (from a in contexto.ATENCIONES
        //                              /*join c in contexto.CUENTAS_PACIENTES on a.ATE_CODIGO equals c.ATE_CODIGO*/
        //                              join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
        //                              join atc in contexto.ATENCION_DETALLE_CATEGORIAS on a.ATE_CODIGO equals atc.ATENCIONES.ATE_CODIGO
        //                              join cc in contexto.CATEGORIAS_CONVENIOS on atc.CATEGORIAS_CONVENIOS.CAT_CODIGO equals cc.CAT_CODIGO
        //                              join h in contexto.HABITACIONES on a.HABITACIONES.hab_Codigo equals h.hab_Codigo

        //                              /*where a.ESC_CODIGO == 5 && c.CUE_ESTADO == 1*/
        //                                  select new DtoPedidos
        //                                  {
        //                                  CODIGO = pa.PAC_CODIGO,
        //                                  ATE_CODIGO = a.ATE_CODIGO,
        //                                  PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
        //                                  PED_FECHA = a.ATE_FECHA_INGRESO.Value,
        //                                  PED_FECHA_ALTA = a.ATE_FECHA_ALTA.Value != null ? a.ATE_FECHA_ALTA.Value : a.ATE_FECHA_INGRESO.Value,
        //                                  PRO_CODIGO = pa.PAC_CODIGO,
        //                                  IDENTIFICACION = pa.PAC_IDENTIFICACION,
        //                                  HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
        //                                  HABITACION = h.hab_Numero,
        //                                  ATE_NUMERO = a.ATE_NUMERO_ATENCION, // aumento el numero de atencion para ser utilizado en vez de codigo /30/10/2012 / GIOVANNY TAPIA
        //                                  PRO_DESCRIPCION = cc.CAT_NOMBRE
        //                              }).Distinct();

        //                if (id != string.Empty)
        //                    result = result.Where(pa => (pa.IDENTIFICACION).StartsWith(id));

        //                if (historia != string.Empty)
        //                    result = result.Where(pa => (pa.HISTORIA_CLINICA).Trim().Contains(historia));

        //                if (nombre != string.Empty)
        //                {
        //                    string[] cadena = nombre.Split();
        //                    if (cadena.Length == 1)
        //                    {
        //                        string n = cadena[0].Trim();
        //                        var porApellidoPaterno = result.Where(pa => (pa.PACIENTE).StartsWith(n)).OrderBy(pa => pa.PACIENTE).Take(cantidad).ToList();
        //                        if (porApellidoPaterno.Count == 0)
        //                        {
        //                            var porNombreUno = result.Where(pa => (pa.PACIENTE).StartsWith(n)).OrderBy(pa => pa.PACIENTE).Take(cantidad).ToList();
        //                            if (porNombreUno.Count > 0)
        //                            {
        //                                return porNombreUno;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            return porApellidoPaterno;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        for (int i = 0; i < cadena.Length; i++)
        //                        {
        //                            string n = cadena[i].Trim();
        //                            result = result.Where(pa => (pa.PACIENTE).Contains(n)).Distinct();
        //                        }
        //                    }
        //                }
        //                return result.OrderBy(pa => pa.PACIENTE).Take(cantidad).Distinct().OrderByDescending(c => c.ATE_CODIGO).ToList();
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }
        //}

        public List<DtoPedidos> recuperarListaPacientesFacturacion(string id, string historia, string nombre, int cantidad, int estado, string NumFac)
        {
            try
            {
                //List<DtoPacientesEmergencia> pacientes = new List<DtoPacientesEmergencia>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    if (id == string.Empty && historia == string.Empty && nombre == string.Empty && NumFac != "ConsultaExterna")
                    {
                        //lista de pacientes con atenciones activas
                        var result = (from a in contexto.ATENCIONES
                                          /*join c in contexto.CUENTAS_PACIENTES on a.ATE_CODIGO equals c.ATE_CODIGO*/
                                      join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                                      join atc in contexto.ATENCION_DETALLE_CATEGORIAS on a.ATE_CODIGO equals atc.ATENCIONES.ATE_CODIGO
                                      join cc in contexto.CATEGORIAS_CONVENIOS on atc.CATEGORIAS_CONVENIOS.CAT_CODIGO equals cc.CAT_CODIGO
                                      join h in contexto.HABITACIONES on a.HABITACIONES.hab_Codigo equals h.hab_Codigo
                                      //                                      join g in contexto.ATE on a.HABITACIONES.hab_Codigo equals h.hab_Codigo
                                      /*where a.ESC_CODIGO != 6 && c.CUE_ESTADO == 1*/
                                      select new DtoPedidos
                                      {
                                          CODIGO = pa.PAC_CODIGO,
                                          ATE_CODIGO = a.ATE_CODIGO,
                                          PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
                                          PED_FECHA = a.ATE_FECHA_INGRESO.Value,
                                          PED_FECHA_ALTA = a.ATE_FECHA_ALTA.Value != null ? a.ATE_FECHA_ALTA.Value : a.ATE_FECHA_INGRESO.Value,
                                          PRO_CODIGO = pa.PAC_CODIGO,
                                          IDENTIFICACION = pa.PAC_IDENTIFICACION,
                                          HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
                                          HABITACION = h.hab_Numero,
                                          ATE_NUMERO = a.ATE_NUMERO_ATENCION, // aumento el numero de atencion para ser utilizado en vez de codigo /30/10/2012 / GIOVANNY TAPIA
                                          PRO_DESCRIPCION = cc.CAT_NOMBRE,
                                          FACTURA = a.ATE_FACTURA_PACIENTE,
                                          ESC_CODIGO = a.ESC_CODIGO.Value,
                                          ATE_FECHA_FACTURA = a.ATE_FACTURA_FECHA.Value != null ? a.ATE_FACTURA_FECHA.Value : DateTime.Now
                                      }).OrderByDescending(c => c.PED_FECHA).Take(cantidad).ToList();

                        return result.ToList();

                        //return result.Take(cantidad).OrderByDescending(c => c.PED_FECHA).ToList();

                    }
                    else if (NumFac == "ConsultaExterna")
                    {
                        //lista de pacientes con atenciones activas
                        var result = (from a in contexto.ATENCIONES
                                          /*join c in contexto.CUENTAS_PACIENTES on a.ATE_CODIGO equals c.ATE_CODIGO*/
                                      join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                                      join atc in contexto.ATENCION_DETALLE_CATEGORIAS on a.ATE_CODIGO equals atc.ATENCIONES.ATE_CODIGO
                                      join cc in contexto.CATEGORIAS_CONVENIOS on atc.CATEGORIAS_CONVENIOS.CAT_CODIGO equals cc.CAT_CODIGO
                                      join h in contexto.HABITACIONES on a.HABITACIONES.hab_Codigo equals h.hab_Codigo
                                      where a.TIPO_INGRESO.TIP_CODIGO == 4 || a.TIPO_INGRESO.TIP_CODIGO == 10
                                      //                                      join g in contexto.ATE on a.HABITACIONES.hab_Codigo equals h.hab_Codigo
                                      /*where a.ESC_CODIGO != 6 && c.CUE_ESTADO == 1*/
                                      select new DtoPedidos
                                      {
                                          CODIGO = pa.PAC_CODIGO,
                                          ATE_CODIGO = a.ATE_CODIGO,
                                          PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
                                          PED_FECHA = a.ATE_FECHA_INGRESO.Value,
                                          PED_FECHA_ALTA = a.ATE_FECHA_ALTA.Value != null ? a.ATE_FECHA_ALTA.Value : a.ATE_FECHA_INGRESO.Value,
                                          PRO_CODIGO = pa.PAC_CODIGO,
                                          IDENTIFICACION = pa.PAC_IDENTIFICACION,
                                          HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
                                          HABITACION = h.hab_Numero,
                                          ATE_NUMERO = a.ATE_NUMERO_ATENCION, // aumento el numero de atencion para ser utilizado en vez de codigo /30/10/2012 / GIOVANNY TAPIA
                                          PRO_DESCRIPCION = cc.CAT_NOMBRE,
                                          ESC_CODIGO = a.ESC_CODIGO.Value != null ? a.ESC_CODIGO.Value : 0,
                                          FACTURA = a.ATE_FACTURA_PACIENTE,
                                          TipoAtencion = a.TipoAtencion
                                      }).OrderByDescending(c => c.PED_FECHA).Take(cantidad).ToList();
                        return result.ToList();
                    }
                    else
                    {
                        var result = (from a in contexto.ATENCIONES
                                          /*join c in contexto.CUENTAS_PACIENTES on a.ATE_CODIGO equals c.ATE_CODIGO*/
                                      join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                                      join atc in contexto.ATENCION_DETALLE_CATEGORIAS on a.ATE_CODIGO equals atc.ATENCIONES.ATE_CODIGO
                                      join cc in contexto.CATEGORIAS_CONVENIOS on atc.CATEGORIAS_CONVENIOS.CAT_CODIGO equals cc.CAT_CODIGO
                                      join h in contexto.HABITACIONES on a.HABITACIONES.hab_Codigo equals h.hab_Codigo
                                      /*where a.ESC_CODIGO != 6 && c.CUE_ESTADO == 1*/
                                      select new DtoPedidos
                                      {
                                          CODIGO = pa.PAC_CODIGO,
                                          ATE_CODIGO = a.ATE_CODIGO,
                                          PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
                                          PED_FECHA = a.ATE_FECHA_INGRESO.Value,
                                          PED_FECHA_ALTA = a.ATE_FECHA_ALTA.Value != null ? a.ATE_FECHA_ALTA.Value : a.ATE_FECHA_INGRESO.Value,
                                          PRO_CODIGO = pa.PAC_CODIGO,
                                          IDENTIFICACION = pa.PAC_IDENTIFICACION,
                                          HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
                                          HABITACION = h.hab_Numero,
                                          ATE_NUMERO = a.ATE_NUMERO_ATENCION, // aumento el numero de atencion para ser utilizado en vez de codigo /30/10/2012 / GIOVANNY TAPIA
                                          PRO_DESCRIPCION = cc.CAT_NOMBRE,
                                          ESC_CODIGO = a.ESC_CODIGO.Value != null ? a.ESC_CODIGO.Value : 0,
                                          FACTURA = a.ATE_FACTURA_PACIENTE,
                                          ATE_FECHA_FACTURA = a.ATE_FACTURA_FECHA.Value != null ? a.ATE_FACTURA_FECHA.Value : DateTime.Now

                                      }).Distinct();

                        if (id != string.Empty)
                            result = result.Where(pa => (pa.IDENTIFICACION).StartsWith(id));

                        if (historia != string.Empty)
                            result = result.Where(pa => (pa.HISTORIA_CLINICA).Trim().StartsWith(historia));

                        if (NumFac != string.Empty)
                            result = result.Where(pa => (pa.FACTURA).StartsWith(NumFac));

                        if (nombre != string.Empty)
                        {
                            string[] cadena = nombre.Split();
                            if (cadena.Length == 1)
                            {
                                string n = cadena[0].Trim();
                                var porApellidoPaterno = result.Where(pa => (pa.PACIENTE).StartsWith(n)).OrderBy(pa => pa.PACIENTE).Take(cantidad).ToList();
                                if (porApellidoPaterno.Count == 0)
                                {
                                    var porNombreUno = result.Where(pa => (pa.PACIENTE).StartsWith(n)).OrderBy(pa => pa.PACIENTE).Take(cantidad).ToList();
                                    if (porNombreUno.Count > 0)
                                    {
                                        return porNombreUno;
                                    }
                                }
                                else
                                {
                                    return porApellidoPaterno;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < cadena.Length; i++)
                                {
                                    string n = cadena[i].Trim();
                                    result = result.Where(pa => (pa.PACIENTE).Contains(n)).Distinct();
                                }
                            }
                        }
                        return result.OrderBy(pa => pa.PACIENTE).Take(cantidad).Distinct().OrderByDescending(c => c.ATE_CODIGO).ToList();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public List<DtoPedidos> recuperarListaPacientesFacturacionMushuñan(string id, string historia, string nombre, int cantidad, int estado, string NumFac)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();
            string xWhere = "";
            if (id != "")
            {
                if (xWhere.Length <= 0)
                    xWhere = "AND P.PAC_IDENTIFICACION = '" + id + "' ";

            }
            if (historia != "")
            {
                xWhere += " AND P.PAC_HISTORIA_CLINICA = '" + historia + "' ";
            }
            if (nombre != "")
            {
                xWhere += "AND P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO  LIKE '%" + nombre + "%' ";
            }
            if (estado == 6)
            {
                xWhere += "AND A.ESC_CODIGO = " + estado + " ";
            }



            command = new SqlCommand("SELECT p.PAC_CODIGO, A.ATE_CODIGO, ISNULL(A.ATE_FECHA_ALTA, GETDATE()) AS ATE_FECHA_ALTA, A.ATE_FECHA_INGRESO, "
            + "P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 as PACIENTE, "
            + "P.PAC_IDENTIFICACION, P.PAC_HISTORIA_CLINICA, H.hab_Numero, A.ATE_NUMERO_ATENCION, CC.CAT_NOMBRE, A.ESC_CODIGO, "
            + "A.ATE_FACTURA_PACIENTE, ISNULL(A.ATE_FACTURA_FECHA, GETDATE()) AS ATE_FACTURA_FECHA "
            + "FROM ATENCIONES A "
            + "INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO "
            + "INNER JOIN ATENCION_DETALLE_CATEGORIAS ADC ON A.ATE_CODIGO = ADC.ATE_CODIGO "
            + "INNER JOIN CATEGORIAS_CONVENIOS CC ON ADC.CAT_CODIGO = CC.CAT_CODIGO "
            + "INNER JOIN HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo "
            + "WHERE A.TIP_CODIGO in (10,13) " + xWhere, connection);
            command.CommandType = CommandType.Text;
            //command = new SqlCommand("sp_PacientesMushuñan", connection);
            //command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();

            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            List<DtoPedidos> result = new List<DtoPedidos>();
            foreach (DataRow item in Tabla.Rows)
            {
                DtoPedidos pacientes = new DtoPedidos();
                pacientes.CODIGO = Convert.ToInt32(item["PAC_CODIGO"].ToString());
                pacientes.ATE_CODIGO = Convert.ToInt64(item["ATE_CODIGO"].ToString());
                pacientes.PACIENTE = item["PACIENTE"].ToString();
                pacientes.PED_FECHA = Convert.ToDateTime(item["ATE_FECHA_INGRESO"].ToString());
                pacientes.PED_FECHA_ALTA = Convert.ToDateTime(item["ATE_FECHA_ALTA"].ToString());
                pacientes.PRO_CODIGO = Convert.ToInt64(item["PAC_CODIGO"].ToString());
                pacientes.IDENTIFICACION = item["PAC_IDENTIFICACION"].ToString();
                pacientes.HISTORIA_CLINICA = item["PAC_HISTORIA_CLINICA"].ToString();
                pacientes.HABITACION = item["hab_Numero"].ToString();
                pacientes.ATE_NUMERO = item["ATE_NUMERO_ATENCION"].ToString();
                pacientes.PRO_DESCRIPCION = item["CAT_NOMBRE"].ToString();
                pacientes.ESC_CODIGO = Convert.ToInt32(item["ESC_CODIGO"].ToString());
                pacientes.FACTURA = item["ATE_FACTURA_PACIENTE"].ToString();
                pacientes.ATE_FECHA_FACTURA = Convert.ToDateTime(item["ATE_FACTURA_FECHA"].ToString());

                result.Add(pacientes);

            }
            result.Take(cantidad);
            return result;
        }

        public List<DtoPedidos> recuperarListaPacientesFacturacionBrigada(string id, string historia, string nombre, int cantidad, int estado, string NumFac)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();
            string xWhere = "";
            if (id != "")
            {
                if (xWhere.Length <= 0)
                    xWhere = "AND P.PAC_IDENTIFICACION = '" + id + "' ";

            }
            if (historia != "")
            {
                xWhere += " AND P.PAC_HISTORIA_CLINICA = '" + historia + "' ";
            }
            if (nombre != "")
            {
                xWhere += "AND P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO  LIKE '%" + nombre + "%' ";
            }
            if (estado == 6)
            {
                xWhere += "AND A.ESC_CODIGO = " + estado + " ";
            }



            command = new SqlCommand("SELECT p.PAC_CODIGO, A.ATE_CODIGO, ISNULL(A.ATE_FECHA_ALTA, GETDATE()) AS ATE_FECHA_ALTA, A.ATE_FECHA_INGRESO, "
            + "P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 as PACIENTE, "
            + "P.PAC_IDENTIFICACION, P.PAC_HISTORIA_CLINICA, H.hab_Numero, A.ATE_NUMERO_ATENCION, CC.CAT_NOMBRE, A.ESC_CODIGO, "
            + "A.ATE_FACTURA_PACIENTE, ISNULL(A.ATE_FACTURA_FECHA, GETDATE()) AS ATE_FACTURA_FECHA "
            + "FROM ATENCIONES A "
            + "INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO "
            + "INNER JOIN ATENCION_DETALLE_CATEGORIAS ADC ON A.ATE_CODIGO = ADC.ATE_CODIGO "
            + "INNER JOIN CATEGORIAS_CONVENIOS CC ON ADC.CAT_CODIGO = CC.CAT_CODIGO "
            + "INNER JOIN HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo "
            + "WHERE A.TIP_CODIGO in(12,14) " + xWhere, connection);
            command.CommandType = CommandType.Text;
            //command = new SqlCommand("sp_PacientesMushuñan", connection);
            //command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();

            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            List<DtoPedidos> result = new List<DtoPedidos>();
            foreach (DataRow item in Tabla.Rows)
            {
                DtoPedidos pacientes = new DtoPedidos();
                pacientes.CODIGO = Convert.ToInt32(item["PAC_CODIGO"].ToString());
                pacientes.ATE_CODIGO = Convert.ToInt64(item["ATE_CODIGO"].ToString());
                pacientes.PACIENTE = item["PACIENTE"].ToString();
                pacientes.PED_FECHA = Convert.ToDateTime(item["ATE_FECHA_INGRESO"].ToString());
                pacientes.PED_FECHA_ALTA = Convert.ToDateTime(item["ATE_FECHA_ALTA"].ToString());
                pacientes.PRO_CODIGO = Convert.ToInt64(item["PAC_CODIGO"].ToString());
                pacientes.IDENTIFICACION = item["PAC_IDENTIFICACION"].ToString();
                pacientes.HISTORIA_CLINICA = item["PAC_HISTORIA_CLINICA"].ToString();
                pacientes.HABITACION = item["hab_Numero"].ToString();
                pacientes.ATE_NUMERO = item["ATE_NUMERO_ATENCION"].ToString();
                pacientes.PRO_DESCRIPCION = item["CAT_NOMBRE"].ToString();
                pacientes.ESC_CODIGO = Convert.ToInt32(item["ESC_CODIGO"].ToString());
                pacientes.FACTURA = item["ATE_FACTURA_PACIENTE"].ToString();
                pacientes.ATE_FECHA_FACTURA = Convert.ToDateTime(item["ATE_FACTURA_FECHA"].ToString());

                result.Add(pacientes);

            }
            result.Take(cantidad);
            return result;
        }
        /// <summary>
        /// Método que devuelve una lista de objetos de tipo DtoPedidos
        /// </summary>
        /// <param name="codigoArea">Código del Area de Pedidos</param>
        /// <param name="codigoEstacion"> Código de la Estación</param>
        /// <param name="estadoPedido">Estado de los pedidos</param>
        /// <param name="fechaIni">Filtro de fecha inicial </param>
        /// <param name="fechaFin">Filtro de fecha final</param>
        /// <param name="desde">numero incial de registro desde donde empezaran a tomarse los valores</param>
        /// <param name="cantidad">cantidad de registros que se recuperaran</param>
        /// <returns>lista de objetos de tipo DtoPedidos</returns>
        public List<DtoPedidos> recuperarListaPedidosVistaPorArea(Int16 codigoArea, byte codigoEstacion, Int32 estadoPedido, DateTime fechaIni, DateTime fechaFin, int desde, Int16 cantidad)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    if (codigoArea > 0 && codigoEstacion > 0 && estadoPedido > 0 && fechaIni != null)
                    {
                        return (from p in contexto.PEDIDOS
                                join d in contexto.PEDIDOS_DETALLE on p.PED_CODIGO equals d.PEDIDOS.PED_CODIGO
                                join e in contexto.PEDIDOS_ESTACIONES on p.PEE_CODIGO equals e.PEE_CODIGO
                                join u in contexto.USUARIOS on p.ID_USUARIO equals u.ID_USUARIO
                                join a in contexto.ATENCIONES on p.ATE_CODIGO equals a.ATE_CODIGO
                                join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                                where p.PEDIDOS_AREAS.PEA_CODIGO == codigoArea && p.PEE_CODIGO == codigoEstacion && p.PED_ESTADO == estadoPedido
                                    && p.PED_FECHA >= fechaIni && p.PED_FECHA <= fechaFin
                                orderby p.PED_CODIGO
                                select new DtoPedidos
                                {
                                    PED_CODIGO = p.PED_CODIGO,
                                    PEA_CODIGO = codigoArea,
                                    PEE_CODIGO = e.PEE_CODIGO,
                                    ESTACION = e.PEE_NOMBRE,
                                    PED_DESCRIPCION = p.PED_DESCRIPCION,
                                    PED_ESTADO = p.PED_ESTADO,
                                    ESTADO = null,
                                    PED_FECHA = p.PED_FECHA.Value,
                                    ID_USUARIO = p.ID_USUARIO.Value,
                                    USUARIO = (u.APELLIDOS + "  " + u.NOMBRES),
                                    HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
                                    ATE_CODIGO = p.ATE_CODIGO.Value,
                                    PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
                                    TIP_PEDIDO = p.TIP_PEDIDO.Value,
                                    TIPO = null,
                                    PDD_CODIGO = d.PDD_CODIGO,
                                    PRO_CODIGO = d.PRODUCTO.PRO_CODIGO,
                                    PRO_DESCRIPCION = d.PRO_DESCRIPCION,
                                    PDD_CANTIDAD = d.PDD_CANTIDAD.Value,
                                    PDD_VALOR = d.PDD_VALOR.Value,
                                    PDD_IVA = d.PDD_IVA.Value,
                                    PDD_TOTAL = d.PDD_TOTAL.Value,
                                    PDD_ESTADO = d.PDD_ESTADO.Value
                                }).Skip(desde).Take(cantidad).ToList();
                    }
                    else if (codigoArea > 0 && codigoEstacion > 0 && estadoPedido > 0 && fechaIni == null)
                    {
                        return (from p in contexto.PEDIDOS
                                join d in contexto.PEDIDOS_DETALLE on p.PED_CODIGO equals d.PEDIDOS.PED_CODIGO
                                join e in contexto.PEDIDOS_ESTACIONES on p.PEE_CODIGO equals e.PEE_CODIGO
                                join u in contexto.USUARIOS on p.ID_USUARIO equals u.ID_USUARIO
                                join a in contexto.ATENCIONES on p.ATE_CODIGO equals a.ATE_CODIGO
                                join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                                where p.PEDIDOS_AREAS.PEA_CODIGO == codigoArea && p.PEE_CODIGO == codigoEstacion && p.PED_ESTADO == estadoPedido
                                orderby p.PED_CODIGO
                                select new DtoPedidos
                                {
                                    PED_CODIGO = p.PED_CODIGO,
                                    PEA_CODIGO = codigoArea,
                                    PEE_CODIGO = e.PEE_CODIGO,
                                    ESTACION = e.PEE_NOMBRE,
                                    PED_DESCRIPCION = p.PED_DESCRIPCION,
                                    PED_ESTADO = p.PED_ESTADO,
                                    ESTADO = null,
                                    PED_FECHA = p.PED_FECHA.Value,
                                    ID_USUARIO = p.ID_USUARIO.Value,
                                    USUARIO = (u.APELLIDOS + "  " + u.NOMBRES),
                                    HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
                                    ATE_CODIGO = p.ATE_CODIGO.Value,
                                    PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
                                    TIP_PEDIDO = p.TIP_PEDIDO.Value,
                                    TIPO = null,
                                    PDD_CODIGO = d.PDD_CODIGO,
                                    PRO_CODIGO = d.PRODUCTO.PRO_CODIGO,
                                    PRO_DESCRIPCION = d.PRO_DESCRIPCION,
                                    PDD_CANTIDAD = d.PDD_CANTIDAD.Value,
                                    PDD_VALOR = d.PDD_VALOR.Value,
                                    PDD_IVA = d.PDD_IVA.Value,
                                    PDD_TOTAL = d.PDD_TOTAL.Value,
                                    PDD_ESTADO = d.PDD_ESTADO.Value
                                }).Skip(desde).Take(cantidad).ToList();
                    }
                    else if (codigoArea > 0 && codigoEstacion > 0 && estadoPedido <= 0 && fechaIni != null)
                    {
                        return (from p in contexto.PEDIDOS
                                join d in contexto.PEDIDOS_DETALLE on p.PED_CODIGO equals d.PEDIDOS.PED_CODIGO
                                join e in contexto.PEDIDOS_ESTACIONES on p.PEE_CODIGO equals e.PEE_CODIGO
                                join u in contexto.USUARIOS on p.ID_USUARIO equals u.ID_USUARIO
                                join a in contexto.ATENCIONES on p.ATE_CODIGO equals a.ATE_CODIGO
                                join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                                where p.PEDIDOS_AREAS.PEA_CODIGO == codigoArea && p.PEE_CODIGO == codigoEstacion
                                    && p.PED_FECHA >= fechaIni && p.PED_FECHA <= fechaFin
                                orderby p.PED_CODIGO
                                select new DtoPedidos
                                {
                                    PED_CODIGO = p.PED_CODIGO,
                                    PEA_CODIGO = codigoArea,
                                    PEE_CODIGO = e.PEE_CODIGO,
                                    ESTACION = e.PEE_NOMBRE,
                                    PED_DESCRIPCION = p.PED_DESCRIPCION,
                                    PED_ESTADO = p.PED_ESTADO,
                                    ESTADO = null,
                                    PED_FECHA = p.PED_FECHA.Value,
                                    ID_USUARIO = p.ID_USUARIO.Value,
                                    USUARIO = (u.APELLIDOS + "  " + u.NOMBRES),
                                    HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
                                    ATE_CODIGO = p.ATE_CODIGO.Value,
                                    PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
                                    TIP_PEDIDO = p.TIP_PEDIDO.Value,
                                    TIPO = null,
                                    PDD_CODIGO = d.PDD_CODIGO,
                                    PRO_CODIGO = d.PRODUCTO.PRO_CODIGO,
                                    PRO_DESCRIPCION = d.PRO_DESCRIPCION,
                                    PDD_CANTIDAD = d.PDD_CANTIDAD.Value,
                                    PDD_VALOR = d.PDD_VALOR.Value,
                                    PDD_IVA = d.PDD_IVA.Value,
                                    PDD_TOTAL = d.PDD_TOTAL.Value,
                                    PDD_ESTADO = d.PDD_ESTADO.Value
                                }).Skip(desde).Take(cantidad).ToList();
                    }
                    else if (codigoArea > 0 && codigoEstacion <= 0 && estadoPedido <= 0 && fechaIni != null)
                    {
                        return (from p in contexto.PEDIDOS
                                join d in contexto.PEDIDOS_DETALLE on p.PED_CODIGO equals d.PEDIDOS.PED_CODIGO
                                join e in contexto.PEDIDOS_ESTACIONES on p.PEE_CODIGO equals e.PEE_CODIGO
                                join u in contexto.USUARIOS on p.ID_USUARIO equals u.ID_USUARIO
                                join a in contexto.ATENCIONES on p.ATE_CODIGO equals a.ATE_CODIGO
                                join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                                where p.PEDIDOS_AREAS.PEA_CODIGO == codigoArea
                                    && p.PED_FECHA >= fechaIni && p.PED_FECHA <= fechaFin
                                orderby p.PED_CODIGO
                                select new DtoPedidos
                                {
                                    PED_CODIGO = p.PED_CODIGO,
                                    PEA_CODIGO = codigoArea,
                                    PEE_CODIGO = e.PEE_CODIGO,
                                    ESTACION = e.PEE_NOMBRE,
                                    PED_DESCRIPCION = p.PED_DESCRIPCION,
                                    PED_ESTADO = p.PED_ESTADO,
                                    ESTADO = null,
                                    PED_FECHA = p.PED_FECHA.Value,
                                    ID_USUARIO = p.ID_USUARIO.Value,
                                    USUARIO = (u.APELLIDOS + "  " + u.NOMBRES),
                                    HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
                                    ATE_CODIGO = p.ATE_CODIGO.Value,
                                    PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
                                    TIP_PEDIDO = p.TIP_PEDIDO.Value,
                                    TIPO = null,
                                    PDD_CODIGO = d.PDD_CODIGO,
                                    PRO_CODIGO = d.PRODUCTO.PRO_CODIGO,
                                    PRO_DESCRIPCION = d.PRO_DESCRIPCION,
                                    PDD_CANTIDAD = d.PDD_CANTIDAD.Value,
                                    PDD_VALOR = d.PDD_VALOR.Value,
                                    PDD_IVA = d.PDD_IVA.Value,
                                    PDD_TOTAL = d.PDD_TOTAL.Value,
                                    PDD_ESTADO = d.PDD_ESTADO.Value
                                }).Skip(desde).Take(cantidad).ToList();
                    }
                    else
                    {
                        return (from p in contexto.PEDIDOS
                                join d in contexto.PEDIDOS_DETALLE on p.PED_CODIGO equals d.PEDIDOS.PED_CODIGO
                                join e in contexto.PEDIDOS_ESTACIONES on p.PEE_CODIGO equals e.PEE_CODIGO
                                join u in contexto.USUARIOS on p.ID_USUARIO equals u.ID_USUARIO
                                join a in contexto.ATENCIONES on p.ATE_CODIGO equals a.ATE_CODIGO
                                join pa in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                                orderby p.PED_CODIGO
                                select new DtoPedidos
                                {
                                    PED_CODIGO = p.PED_CODIGO,
                                    PEA_CODIGO = codigoArea,
                                    PEE_CODIGO = e.PEE_CODIGO,
                                    ESTACION = e.PEE_NOMBRE,
                                    PED_DESCRIPCION = p.PED_DESCRIPCION,
                                    PED_ESTADO = p.PED_ESTADO,
                                    ESTADO = null,
                                    PED_FECHA = p.PED_FECHA.Value,
                                    ID_USUARIO = p.ID_USUARIO.Value,
                                    USUARIO = (u.APELLIDOS + "  " + u.NOMBRES),
                                    HISTORIA_CLINICA = pa.PAC_HISTORIA_CLINICA,
                                    ATE_CODIGO = p.ATE_CODIGO.Value,
                                    PACIENTE = pa.PAC_APELLIDO_PATERNO + " " + pa.PAC_APELLIDO_MATERNO + " " + pa.PAC_NOMBRE1 + " " + pa.PAC_NOMBRE2,
                                    TIP_PEDIDO = p.TIP_PEDIDO.Value,
                                    TIPO = null,
                                    PDD_CODIGO = d.PDD_CODIGO,
                                    PRO_CODIGO = d.PRODUCTO.PRO_CODIGO,
                                    PRO_DESCRIPCION = d.PRO_DESCRIPCION,
                                    PDD_CANTIDAD = d.PDD_CANTIDAD.Value,
                                    PDD_VALOR = d.PDD_VALOR.Value,
                                    PDD_IVA = d.PDD_IVA.Value,
                                    PDD_TOTAL = d.PDD_TOTAL.Value,
                                    PDD_ESTADO = d.PDD_ESTADO.Value
                                }).Skip(desde).Take(cantidad).ToList();
                    }
                }
            }
            catch (Exception err) { throw err; }
        }

        #endregion
        #region ESTACIONES_PEDIDOS
        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo PEDIDOS_ESTACIONES
        /// </summary>
        /// <returns>lista de objetos de tipo PEDIDOS_ESTACIONES</returns>
        public List<PEDIDOS_ESTACIONES> recuperarListaEstaciones()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.PEDIDOS_ESTACIONES.OrderBy(p => p.PEE_CODIGO).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }
        /// <summary>
        /// Metodo que devuelve una instancia de PEDIDOS_ESTACIONES recibiendo como parametro el codigo 
        /// </summary>
        /// <param name="codigoArea">codigo de la estacion</param>
        /// <returns>devuelve un objeto PEDIDOS_ESTACIONES</returns>
        public PEDIDOS_ESTACIONES recuperarEstacionPorID(Int16 codigoPedido)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.PEDIDOS_ESTACIONES.Where(a => a.PEE_CODIGO == codigoPedido).FirstOrDefault();
                }
            }
            catch (Exception err) { throw err; }
        }
        #endregion

        #region PRODUCTO ESTRUCTURAS
        //public List<DtoLaboratorioEstructura> recuperarEstructura(string area, short codigoEstructura)
        //{
        //    try
        //    {
        //        using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //        {
        //            return (from p in contexto.PRODUCTO_ESTRUCTURA
        //                    where p.PRE_PADRE == codigoEstructura && p.PRE_ESTADO == true
        //                    select new DtoLaboratorioEstructura
        //                        {
        //                            CODIGO_AREA = p.PRE_CODIGO,
        //                            AREA = p.PRE_DESCRIPCION,
        //                            COD_PRODUCTO = pe.PRO_CODIGO,
        //                            COD_EXAMEN = pe.PRO_CODIGO_BARRAS,
        //                            EXAMEN = pe.PRO_DESCRIPCION
        //                        }).ToList();
        //        }
        //    }
        //    catch (Exception err) { throw err; }
        //}



        #endregion

        #region ImpresionPedido

        public DataTable DatosPedido(int NumeroPedido)
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
            Sqlcmd = new SqlCommand("sp_ImpresionPedido", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Pedido", SqlDbType.Int);
            Sqlcmd.Parameters["@Pedido"].Value = NumeroPedido;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            if (Dts.Rows.Count > 0)
            {
                return Dts;
            }
            else
            {
                return Dts;
            }

        }

        public DataTable DatosPedidoMushuñan(int NumeroPedido)
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
            Sqlcmd = new SqlCommand("sp_ImpresionPedidoMushuñan", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Pedido", SqlDbType.Int);
            Sqlcmd.Parameters["@Pedido"].Value = NumeroPedido;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            if (Dts.Rows.Count > 0)
            {
                return Dts;
            }
            else
            {
                return Dts;
            }

        }

        public DataTable RetornaDevCodigo(Int64 ped_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();

            connection.Open();
            command = new SqlCommand("SELECT PD.DevCodigo FROM PEDIDOS P INNER JOIN PEDIDO_DEVOLUCION PD ON P.PED_CODIGO = PD.Ped_Codigo WHERE P.PED_CODIGO = @ped_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@ped_codigo", ped_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
        public DataTable DatosPedido2(int NumeroPedido)
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
            Sqlcmd = new SqlCommand("sp_ImpresionDevolucion", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Pedido", SqlDbType.Int);
            Sqlcmd.Parameters["@Pedido"].Value = NumeroPedido;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            if (Dts.Rows.Count > 0)
            {
                return Dts;
            }
            else
            {
                return Dts;
            }

        }

        public DataTable DatosImpresionPedido(int CodigoArea)
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
            Sqlcmd = new SqlCommand("sp_ImpresoraArea", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@area", SqlDbType.Int);
            Sqlcmd.Parameters["@area"].Value = CodigoArea;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            if (Dts.Rows.Count > 0)
            {
                return Dts;
            }
            else
            {
                return Dts;
            }

        }


        public DataTable ListaPedidosRealizados(int CodigoEstacion, int EstadoPedido, bool FiltroFechas, DateTime Fecha1, DateTime Fecha2)
        {
            //// GIOVANNY TAPIA / 07/08/2012

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
            Sqlcmd = new SqlCommand("sp_ListaPedidosEstaciones", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoEstacion", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoEstacion"].Value = CodigoEstacion;

            Sqlcmd.Parameters.Add("@EstadoDetallePedido", SqlDbType.Int);
            Sqlcmd.Parameters["@EstadoDetallePedido"].Value = EstadoPedido;

            Sqlcmd.Parameters.Add("@FiltroFecha", SqlDbType.Bit);
            Sqlcmd.Parameters["@FiltroFecha"].Value = FiltroFechas;

            Sqlcmd.Parameters.Add("@FechaInicio", SqlDbType.Date);
            Sqlcmd.Parameters["@FechaInicio"].Value = Fecha1;

            Sqlcmd.Parameters.Add("@FechaFin", SqlDbType.Date);
            Sqlcmd.Parameters["@FechaFin"].Value = Fecha2;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            if (Dts.Rows.Count > 0)
            {
                return Dts;
            }
            else
            {
                return Dts;
            }


            ////SOLVENCIA DE TIEMPO DE RESPUESTA EDGAR 20210903
            //SqlCommand command;
            //SqlConnection connection;
            //BaseContextoDatos obj = new BaseContextoDatos();
            //DataTable Tabla = new DataTable();
            //SqlDataReader reader;

            //connection = obj.ConectarBd();
            //connection.Open();

            //string xWhere = "WHERE PE.PEE_CODIGO = @CodigoEstacion AND PD.PDD_ESTADO = @EstadoDetallePedido AND P.PED_FECHA BETWEEN @FechaInicio AND @FechaFin";
            //command = new SqlCommand("SELECT * FROM MONITORPEDIDO " + xWhere, connection);
            //command.Parameters.AddWithValue("@CodigoEstacion", CodigoEstacion);
            //command.Parameters.AddWithValue("@EstadoDetallePedido", EstadoPedido);
            //command.Parameters.AddWithValue("@FechaInicio", Fecha1);
            //command.Parameters.AddWithValue("@FechaFin", Fecha2);
            //command.CommandTimeout = 200;
            //reader = command.ExecuteReader();
            //Tabla.Load(reader);
            //reader.Close();
            //command.Parameters.Clear();
            //connection.Close();
            //return Tabla;

        }

        #endregion


        #region PedidoOtros

        public Int64 CreaPedido(DtoPedidoOtros Pedido, Int64 Numvale)
        {
            #region Codigo annterior pedido 
            // GIOVANNY TAPIA / 07/08/2012
            //SqlConnection Sqlcon;   // Se comenta para el nuevo codigo y trabajar con el modelo // Mario Valencia // 07-03-2024
            //SqlCommand Sqlcmd;
            //SqlDataAdapter Sqldap;
            //DataTable Dts = new DataTable();
            //SqlTransaction transaction;
            //BaseContextoDatos obj = new BaseContextoDatos();
            //Sqlcon = obj.ConectarBd();
            //try
            //{
            //    Sqlcon.Open();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            //transaction = Sqlcon.BeginTransaction();

            //try
            //{
            //    Sqlcmd = new SqlCommand("sp_GuardaEncabezadoPedido", Sqlcon);
            //    Sqlcmd.CommandType = CommandType.StoredProcedure;

            //    Sqlcmd.Transaction = transaction;

            //    Sqlcmd.Parameters.Add("@Ped_Codigo", SqlDbType.Int);
            //    Sqlcmd.Parameters["@Ped_Codigo"].Value = Pedido.PED_CODIGO;

            //    Sqlcmd.Parameters.Add("@PEA_CODIGO", SqlDbType.SmallInt);
            //    Sqlcmd.Parameters["@PEA_CODIGO"].Value = Pedido.PEA_CODIGO;

            //    Sqlcmd.Parameters.Add("@PEE_CODIGO", SqlDbType.TinyInt);
            //    Sqlcmd.Parameters["@PEE_CODIGO"].Value = Pedido.PEE_CODIGO;

            //    Sqlcmd.Parameters.Add("@PED_DESCRIPCION", SqlDbType.VarChar);
            //    Sqlcmd.Parameters["@PED_DESCRIPCION"].Value = Pedido.PED_DESCRIPCION;

            //    Sqlcmd.Parameters.Add("@PED_ESTADO", SqlDbType.SmallInt);
            //    Sqlcmd.Parameters["@PED_ESTADO"].Value = Pedido.PED_ESTADO;

            //    Sqlcmd.Parameters.Add("@PED_FECHA", SqlDbType.DateTime);
            //    Sqlcmd.Parameters["@PED_FECHA"].Value = Pedido.PED_FECHA;

            //    Sqlcmd.Parameters.Add("@ID_USUARIO", SqlDbType.SmallInt);
            //    Sqlcmd.Parameters["@ID_USUARIO"].Value = Pedido.ID_USUARIO;

            //    Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.Int);
            //    Sqlcmd.Parameters["@ATE_CODIGO"].Value = Pedido.ATE_CODIGO;

            //    Sqlcmd.Parameters.Add("@TIP_PEDIDO", SqlDbType.SmallInt);
            //    Sqlcmd.Parameters["@TIP_PEDIDO"].Value = Pedido.TIP_PEDIDO;

            //    Sqlcmd.Parameters.Add("@PED_PRIORIDAD", SqlDbType.TinyInt);
            //    Sqlcmd.Parameters["@PED_PRIORIDAD"].Value = Pedido.PED_PRIORIDAD;

            //    Sqlcmd.Parameters.Add("@PED_TRANSACCION", SqlDbType.Int);
            //    Sqlcmd.Parameters["@PED_TRANSACCION"].Value = Pedido.PED_TRANSACCION;

            //    Sqlcmd.Parameters.Add("@MED_CODIGO", SqlDbType.Int);
            //    Sqlcmd.Parameters["@MED_CODIGO"].Value = Pedido.MED_CODIGO;

            //    Sqldap = new SqlDataAdapter();
            //    Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            //    Sqldap.SelectCommand = Sqlcmd;

            //    Sqldap.Fill(Dts);

            //    Resultado = Convert.ToInt32(Dts.Rows[0][0]);

            //    foreach (var _Pedido in Pedido.DetallePedidoOtros)
            //    {

            //        Sqlcmd = new SqlCommand("sp_GuardaPedidoDetalle", Sqlcon);
            //        Sqlcmd.CommandType = CommandType.StoredProcedure;

            //        Sqlcmd.Transaction = transaction;

            //        Sqlcmd.Parameters.Add("@PDD_CODIGO", SqlDbType.BigInt);
            //        Sqlcmd.Parameters["@PDD_CODIGO"].Value = _Pedido.PDD_CODIGO;

            //        Sqlcmd.Parameters.Add("@PED_CODIGO", SqlDbType.Int);
            //        Sqlcmd.Parameters["@PED_CODIGO"].Value = Resultado;

            //        Sqlcmd.Parameters.Add("@PRO_CODIGO", SqlDbType.Int);
            //        Sqlcmd.Parameters["@PRO_CODIGO"].Value = _Pedido.PRO_CODIGO;

            //        Sqlcmd.Parameters.Add("@PRO_DESCRIPCION", SqlDbType.NVarChar);
            //        Sqlcmd.Parameters["@PRO_DESCRIPCION"].Value = _Pedido.PRO_DESCRIPCION;

            //        Sqlcmd.Parameters.Add("@PDD_CANTIDAD", SqlDbType.Decimal);
            //        Sqlcmd.Parameters["@PDD_CANTIDAD"].Value = _Pedido.PDD_CANTIDAD;

            //        Sqlcmd.Parameters.Add("@PDD_VALOR", SqlDbType.Decimal);
            //        Sqlcmd.Parameters["@PDD_VALOR"].Value = _Pedido.PDD_VALOR;

            //        Sqlcmd.Parameters.Add("@PDD_IVA", SqlDbType.Decimal);
            //        Sqlcmd.Parameters["@PDD_IVA"].Value = _Pedido.PDD_IVA;

            //        Sqlcmd.Parameters.Add("@PDD_TOTAL", SqlDbType.Decimal);
            //        Sqlcmd.Parameters["@PDD_TOTAL"].Value = _Pedido.PDD_TOTAL;

            //        Sqlcmd.Parameters.Add("@PDD_ESTADO", SqlDbType.Bit);
            //        Sqlcmd.Parameters["@PDD_ESTADO"].Value = _Pedido.PDD_ESTADO;

            //        Sqlcmd.Parameters.Add("@PDD_COSTO", SqlDbType.Decimal);
            //        Sqlcmd.Parameters["@PDD_COSTO"].Value = _Pedido.PDD_COSTO;

            //        Sqlcmd.Parameters.Add("@PDD_FACTURA", SqlDbType.VarChar);
            //        Sqlcmd.Parameters["@PDD_FACTURA"].Value = _Pedido.PDD_FACTURA;

            //        Sqlcmd.Parameters.Add("@PDD_ESTADO_FACTURA", SqlDbType.Int);
            //        Sqlcmd.Parameters["@PDD_ESTADO_FACTURA"].Value = _Pedido.PDD_ESTADO_FACTURA;

            //        Sqlcmd.Parameters.Add("@PDD_FECHA_FACTURA", SqlDbType.VarChar);
            //        Sqlcmd.Parameters["@PDD_FECHA_FACTURA"].Value = _Pedido.PDD_FECHA_FACTURA;

            //        Sqlcmd.Parameters.Add("@PDD_RESULTADO", SqlDbType.VarChar);
            //        Sqlcmd.Parameters["@PDD_RESULTADO"].Value = _Pedido.PDD_RESULTADO;

            //        Sqlcmd.Parameters.Add("@PRO_CODIGO_BARRAS", SqlDbType.VarChar);
            //        Sqlcmd.Parameters["@PRO_CODIGO_BARRAS"].Value = _Pedido.PRO_CODIGO_BARRAS;

            //        Sqlcmd.Parameters.Add("@Atencion_codigo", SqlDbType.VarChar);
            //        Sqlcmd.Parameters["@Atencion_codigo"].Value = Pedido.ATE_CODIGO;

            //        Sqlcmd.Parameters.Add("@Rub_Codigo", SqlDbType.VarChar);
            //        Sqlcmd.Parameters["@Rub_Codigo"].Value = 31;

            //        Sqlcmd.Parameters.Add("@Pedido_Area", SqlDbType.VarChar);
            //        Sqlcmd.Parameters["@Pedido_Area"].Value = Pedido.PEA_CODIGO;

            //        Sqlcmd.Parameters.Add("@Usuario_Id", SqlDbType.VarChar);
            //        Sqlcmd.Parameters["@Usuario_Id"].Value = Pedido.ID_USUARIO;

            //        Sqlcmd.Parameters.Add("@NumVale", SqlDbType.Int);
            //        Sqlcmd.Parameters["@Numvale"].Value = Numvale;

            //        Sqldap = new SqlDataAdapter();
            //        Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            //        Sqldap.SelectCommand = Sqlcmd;

            //        Sqldap.Fill(Dts);

            //    }

            //    transaction.Commit();

            //    return Resultado;

            //}
            //catch (Exception ex)
            //{
            //    transaction.Rollback();
            //    return 0;
            //}
            #endregion
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    var maxPedido = db.PEDIDOS.OrderByDescending(x => x.PED_CODIGO).FirstOrDefault();
                    Int64 maxp = Convert.ToInt64(maxPedido.PED_CODIGO) + 1;
                    PEDIDOS pedido = new PEDIDOS();
                    pedido.PED_CODIGO = maxPedido.PED_CODIGO + 1;
                    pedido.PEDIDOS_AREAS.PEA_CODIGO = Pedido.PEA_CODIGO;
                    pedido.PEE_CODIGO = (byte?)Pedido.PEE_CODIGO;
                    pedido.PED_DESCRIPCION = Pedido.PED_DESCRIPCION;
                    pedido.PED_ESTADO = Pedido.PED_ESTADO;
                    pedido.PED_FECHA = Pedido.PED_FECHA;
                    pedido.ID_USUARIO = Pedido.ID_USUARIO;
                    pedido.ATE_CODIGO = Pedido.ATE_CODIGO;
                    pedido.TIP_PEDIDO = Pedido.TIP_PEDIDO;
                    pedido.PED_PRIORIDAD = (byte?)Pedido.PED_PRIORIDAD;
                    pedido.PED_TRANSACCION = Pedido.PED_TRANSACCION;
                    pedido.MED_CODIGO = Pedido.MED_CODIGO;
                    db.Crear("PEDIDOS", pedido);

                    List<PEDIDOS_DETALLE> peddetalle = new List<PEDIDOS_DETALLE>();
                    var maxdetalle = db.PEDIDOS_DETALLE.OrderByDescending(x => x.PDD_CODIGO).FirstOrDefault();
                    Int64 max = Convert.ToInt64(maxdetalle.PDD_CODIGO);
                    foreach (var ped in Pedido.DetallePedidoOtros)
                    {
                        PEDIDOS_DETALLE detalle = new PEDIDOS_DETALLE();
                        detalle.PDD_CODIGO = max + 1;
                        detalle.PEDIDOS.PED_CODIGO = maxPedido.PED_CODIGO + 1;
                        detalle.PRODUCTO.PRO_CODIGO = ped.PRO_CODIGO;
                        detalle.PRO_DESCRIPCION = ped.PRO_DESCRIPCION;
                        detalle.PDD_CANTIDAD = ped.PDD_CANTIDAD;
                        detalle.PDD_VALOR = ped.PDD_VALOR;
                        detalle.PDD_IVA = ped.PDD_IVA;
                        detalle.PDD_TOTAL = ped.PDD_TOTAL;
                        detalle.PDD_ESTADO = ped.PDD_ESTADO;
                        detalle.PDD_COSTO = ped.PDD_CODIGO;
                        detalle.PDD_FACTURA = ped.PDD_FACTURA;
                        detalle.PDD_ESTADO_FACTURA = ped.PDD_ESTADO_FACTURA;
                        detalle.PDD_FECHA_FACTURA = ped.PDD_FECHA_FACTURA;
                        detalle.PDD_RESULTADO = ped.PDD_RESULTADO;
                        detalle.PRO_CODIGO_BARRAS = ped.PRO_CODIGO_BARRAS;
                        peddetalle.Add(detalle);
                    }
                    db.CrearLista("PEDIDOS_DETALLE", peddetalle);

                    List<CUENTAS_PACIENTES> cuenta = new List<CUENTAS_PACIENTES>();
                    var maxNumeroCuentaPaciente = db.CUENTAS_PACIENTES.OrderByDescending(z => z.CUE_CODIGO).FirstOrDefault();
                    Int64 CUE_NUMERO_CONTROL = maxNumeroCuentaPaciente.CUE_CODIGO + 1;
                    foreach (var item in Pedido.DetallePedidoOtros)
                    {
                        CUENTAS_PACIENTES cp = new CUENTAS_PACIENTES();
                        cp.CUE_CODIGO = CUE_NUMERO_CONTROL;
                        cp.ATE_CODIGO = Pedido.ATE_CODIGO;
                        cp.CUE_FECHA = DateTime.Now;
                        cp.PRO_CODIGO = Convert.ToString(item.PRO_CODIGO);
                        cp.CUE_DETALLE = item.PRO_DESCRIPCION;
                        cp.CUE_VALOR_UNITARIO = item.PDD_VALOR;
                        cp.CUE_CANTIDAD = item.PDD_CANTIDAD;
                        cp.CUE_VALOR = item.PDD_TOTAL;
                        cp.CUE_IVA = item.PDD_IVA;
                        cp.CUE_ESTADO = 1;
                        cp.CUE_NUM_FAC = "";
                        cp.RUB_CODIGO = 31;
                        cp.PED_CODIGO = Pedido.PEA_CODIGO;
                        cp.ID_USUARIO = Pedido.ID_USUARIO;
                        cp.CAT_CODIGO = 0;
                        cp.PRO_CODIGO_BARRAS = item.PRO_CODIGO_BARRAS;
                        cp.CUE_NUM_CONTROL = "0";
                        cp.CUE_OBSERVACION = "OTROS RUBROS";
                        cp.MED_CODIGO = 0;
                        cp.CUE_ORDER_IMPRESION = null;
                        //cp.Codigo_Pedido = item.Codigo_Pedido;
                        //cp.Id_Tipo_Medico = item.Id_Tipo_Medico;
                        //cp.COSTO = item.COSTO;
                        //cp.NumVale = item.NumVale;
                        //cp.DivideFactura = item.DivideFactura;
                        //cp.Descuento = item.Descuento;
                        //cp.PorDescuento = item.PorDescuento;
                        //cp.USUARIO_FACTURA = item.USUARIO_FACTURA;
                        //cp.FECHA_FACTURA = item.FECHA_FACTURA;
                        cuenta.Add(cp);
                        CUE_NUMERO_CONTROL++;
                    }
                    db.CrearLista("CUENTAS_PACIENTES", cuenta);

                    db.SaveChanges();
                    transa.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return maxp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transa.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return 0;
                }

            }
        }

        public DataTable ListaPedidos(DateTime Fecha1, DateTime Fecha2)
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

            Sqlcmd = new SqlCommand("SP_LISTAPEDIDOS", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@FECHA1", SqlDbType.Date);
            Sqlcmd.Parameters["@FECHA1"].Value = Fecha1;

            Sqlcmd.Parameters.Add("@FECHA2", SqlDbType.Date);
            Sqlcmd.Parameters["@FECHA2"].Value = Fecha2;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            if (Dts.Rows.Count > 0)
            {
                return Dts;
            }
            else
            {
                return Dts;
            }

        }

        public DataTable CierreReporte(DateTime Fecha1, string Usuario)
        {
            // PABLO ROCHA / 26/04/2013

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

            Sqlcmd = new SqlCommand("sp_GeneraCierrePacientes", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@FECHA1", SqlDbType.Date);
            Sqlcmd.Parameters["@FECHA1"].Value = Fecha1;

            Sqlcmd.Parameters.Add("@USUARIO", SqlDbType.NVarChar);
            Sqlcmd.Parameters["@USUARIO"].Value = Usuario;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            if (Dts.Rows.Count > 0)
            {
                return Dts;
            }
            else
            {
                return Dts;
            }

        }

        public void GuardaCierreTurno(List<DtoCierreTurno> CierreTurno)
        {
            // PABLO ROCHA / 26/04/2013

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

            foreach (var _Detalle in CierreTurno)
            {

                Sqlcmd = new SqlCommand("sp_GuardaCajeroCierreTurno", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Parameters.Add("@Fecha", SqlDbType.Date);
                Sqlcmd.Parameters["@Fecha"].Value = _Detalle.Fecha;

                Sqlcmd.Parameters.Add("@PAC_HISTORIA_CLINICA", SqlDbType.NChar);
                Sqlcmd.Parameters["@PAC_HISTORIA_CLINICA"].Value = _Detalle.PAC_HISTORIA_CLINICA;

                Sqlcmd.Parameters.Add("@ATE_NUMERO_ATENCION", SqlDbType.NChar);
                Sqlcmd.Parameters["@ATE_NUMERO_ATENCION"].Value = _Detalle.ATE_NUMERO_ATENCION;

                Sqlcmd.Parameters.Add("@hab_Numero", SqlDbType.VarChar);
                Sqlcmd.Parameters["@hab_Numero"].Value = _Detalle.hab_Numero;

                Sqlcmd.Parameters.Add("@PACIENTE", SqlDbType.VarChar);
                Sqlcmd.Parameters["@PACIENTE"].Value = _Detalle.PACIENTE;

                Sqlcmd.Parameters.Add("@CATEGORIA", SqlDbType.VarChar);
                Sqlcmd.Parameters["@CATEGORIA"].Value = _Detalle.CATEGORIA;

                Sqlcmd.Parameters.Add("@CAJERO_NOMBRE", SqlDbType.VarChar);
                Sqlcmd.Parameters["@CAJERO_NOMBRE"].Value = _Detalle.CAJERO_NOMBRE;

                Sqlcmd.Parameters.Add("@CAJERO_CODIGO", SqlDbType.Int);
                Sqlcmd.Parameters["@CAJERO_CODIGO"].Value = _Detalle.CAJERO_CODIGO;

                Sqlcmd.Parameters.Add("@ESTADO", SqlDbType.VarChar);
                Sqlcmd.Parameters["@ESTADO"].Value = _Detalle.ESTADO;

                Sqlcmd.Parameters.Add("@ID_USUARIO", SqlDbType.Int);
                Sqlcmd.Parameters["@ID_USUARIO"].Value = _Detalle.ID_USUARIO;

                Sqlcmd.Parameters.Add("@OBSERVACION", SqlDbType.VarChar);
                Sqlcmd.Parameters["@OBSERVACION"].Value = _Detalle.OBSERVACION;


                Sqlcmd.Parameters.Add("@MEDICO_TURNO", SqlDbType.VarChar);
                Sqlcmd.Parameters["@MEDICO_TURNO"].Value = "PRUEBA";

                Sqlcmd.Parameters.Add("@MEDICO_TURNO", SqlDbType.VarChar);
                Sqlcmd.Parameters["@MEDICO_TURNO"].Value = _Detalle.MEDICO_TURNO;


                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts);

            }

            //if (Dts.Rows.Count > 0)
            //{
            //    return Dts;
            //}
            //else
            //{
            //    return Dts;
            //}

        }

        #endregion

        public int ValidarAseguradoraPaciente(Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            int te_codigo = 0;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ValidarAseguradoraPaciente", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                te_codigo = Convert.ToInt32(reader["TE_CODIGO"].ToString());
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return te_codigo;
        }
        public bool ParametroDieta()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            bool valido = false;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ParametroDieta", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                valido = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            reader.Close();
            connection.Close();
            return valido;
        }
        public int ValidarRubroDieta(string codpro)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            int rubro = 0;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_RubroProdcuto", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                rubro = Convert.ToInt32(reader["Pea_Codigo_His"].ToString());
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return rubro;
        }

        public bool ValidaRepetido(Int64 ate_codigo, int pci_codigo, string codpro, Int64 ped_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            bool duplicado = false;
            string codproducto;
            command = new SqlCommand("sp_QuirofanoDuplicado", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pci_codigo", pci_codigo);
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@ped_codigo", ped_codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                codproducto = reader["PRO_CODIGO"].ToString();
                if (codproducto != null)
                {
                    duplicado = true;
                }
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return duplicado;

        }
        public DataTable Area_Subarea(int ped_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_AreaYSubArea", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ped_codigo", ped_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

        public DataTable Devoluciones(DateTime desde, DateTime hasta, string hc)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            //esto se lo hizo pensando en que se agreguen mas filtros
            string xwhere = "WHERE CONVERT (date, PD.DevFecha)  BETWEEN @desde AND @hasta\r\n";

            if (hc != "0")
                xwhere += " AND P.PAC_HISTORIA_CLINICA = " + hc + "\r\n";
            command = new SqlCommand("SELECT P.PAC_HISTORIA_CLINICA AS HC,\r\n"
            + "P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS PACIENTE,\r\n"
            + "A.ATE_NUMERO_ATENCION AS ATENCION, H.hab_Numero AS 'HAB', A.ATE_FECHA_INGRESO AS 'F. INGRESO', A.ATE_FECHA_ALTA AS 'F. ALTA',\r\n"
            + "PD.DevFecha AS 'F. DEVOLUCION', PDD.DevCodigo AS 'N° DEVOLUCIÓN', U.APELLIDOS + ' ' + U.NOMBRES AS 'DEVUELTO POR',\r\n"
            + "PD.DevObservacion AS OBSERVACION, PDD.DevDetCantidad AS CANT, PDD.DevDetValor AS VALOR, PDD.DevDetIvaTotal AS TOTAL,\r\n"
            + "PA.PEA_NOMBRE AS AREA, R.RUB_NOMBRE AS SUBAREA, PDD.PRO_CODIGO AS 'COD. PRODUCTO', PDD.PRO_DESCRIPCION AS 'PRODUCTO'\r\n"
            + "FROM CUENTAS_PACIENTES CP \r\n"
            + "INNER JOIN ATENCIONES A ON CP.ATE_CODIGO = A.ATE_CODIGO \r\n"
            + "INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO\r\n"
            + "INNER JOIN HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo\r\n"
            + "INNER JOIN PEDIDO_DEVOLUCION PD ON CP.Codigo_Pedido = PD.Ped_Codigo\r\n"
            + "INNER JOIN PEDIDO_DEVOLUCION_DETALLE PDD ON PD.DevCodigo = PDD.DevCodigo AND CP.PRO_CODIGO = PDD.PRO_CODIGO\r\n"
            + "INNER JOIN USUARIOS U ON PD.ID_USUARIO = U.ID_USUARIO\r\n"
            + "INNER JOIN RUBROS R ON CP.RUB_CODIGO = R.RUB_CODIGO\r\n"
            + "INNER JOIN PEDIDOS_AREAS PA ON R.PED_CODIGO = PA.DIV_CODIGO\r\n"
            //+ "WHERE PD.DevFecha BETWEEN @desde AND @hasta\r\n"
            + xwhere
            + "ORDER BY PDD.DevCodigo DESC\r\n", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@desde", desde);
            command.Parameters.AddWithValue("@hasta", hasta);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
        public DataTable MonitorPedidos(DateTime fechainicio, DateTime fechafin)
        {
            //SOLVENCIA DE TIEMPO DE RESPUESTA EDGAR 20210903
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            SqlDataReader reader;

            connection = obj.ConectarBd();
            connection.Open();

            string xWhere = "WHERE CONVERT (date,P.PED_FECHA) BETWEEN @FechaInicio AND @FechaFin ORDER BY P.PED_FECHA DESC";
            command = new SqlCommand("SELECT P.PED_CODIGO AS 'CODIGO', P.PED_FECHA AS 'F_PEDIDO', PE.PEE_NOMBRE AS ESTACION, " +

            "PA.PAC_APELLIDO_PATERNO + ' ' + PA.PAC_APELLIDO_MATERNO + ' ' + PA.PAC_NOMBRE1 + ' ' + PA.PAC_NOMBRE2 AS PACIENTE, " +
            "H.hab_Numero AS HAB, " +
            "U.APELLIDOS + ' ' + U.NOMBRES AS 'PEDIDO POR', " +
            "M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO, " +
            "CP.PRO_CODIGO AS 'COD. PRODUCTO', CP.CUE_DETALLE AS PRODUCTO, CP.CUE_CANTIDAD AS CANTIDAD " +

            "FROM PEDIDOS P " +

            //"INNER JOIN PEDIDOS_DETALLE PD ON P.PED_CODIGO = PD.PED_CODIGO "+

            "INNER JOIN CUENTAS_PACIENTES CP ON P.PED_CODIGO = CP.Codigo_Pedido " +

            "LEFT JOIN PEDIDOS_ESTACIONES PE ON P.PEE_CODIGO = PE.PEE_CODIGO " +

            "INNER JOIN USUARIOS U ON P.ID_USUARIO = U.ID_USUARIO " +

            "INNER JOIN ATENCIONES A ON CP.ATE_CODIGO = A.ATE_CODIGO " +

            "INNER JOIN HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo " +

            "INNER JOIN PACIENTES PA ON A.PAC_CODIGO = PA.PAC_CODIGO " +

            "INNER JOIN MEDICOS M ON P.MED_CODIGO = M.MED_CODIGO " + xWhere, connection);
            //command.Parameters.AddWithValue("@CodigoEstacion", CodigoEstacion);
            //command.Parameters.AddWithValue("@EstadoDetallePedido", EstadoPedido);
            command.Parameters.AddWithValue("@FechaInicio", fechainicio);
            command.Parameters.AddWithValue("@FechaFin", fechafin);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

        public void crearDetallePedido2(PEDIDOS_DETALLE ndetalle, PEDIDOS pedido, Int16 Rubro, Int32 PedidoDivision, string NumVale)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.AddToPEDIDOS_DETALLE(ndetalle);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    //guardo el estado de cuenta
                    String g = Convert.ToString(ndetalle.PRODUCTOReference.EntityKey.EntityKeyValues.GetValue(0));
                    var cuenta = new CUENTAS_PACIENTES
                    {
                        ATE_CODIGO = pedido.ATE_CODIGO,
                        PRO_CODIGO = Convert.ToString(ndetalle.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value),
                        CUE_ESTADO = 1,
                        CUE_FECHA = pedido.PED_FECHA,
                        CUE_VALOR_UNITARIO = ndetalle.PDD_VALOR,
                        CUE_IVA = ndetalle.PDD_IVA,
                        CUE_VALOR = ndetalle.PDD_VALOR * ndetalle.PDD_CANTIDAD,
                        ID_USUARIO = pedido.ID_USUARIO,
                        PED_CODIGO = PedidoDivision,
                        RUB_CODIGO = Rubro,
                        CAT_CODIGO = 0,
                        CUE_CANTIDAD = Convert.ToDecimal(ndetalle.PDD_CANTIDAD),
                        CUE_DETALLE = ndetalle.PRO_DESCRIPCION,
                        CUE_NUM_FAC = "0",
                        PRO_CODIGO_BARRAS = ndetalle.PRO_CODIGO_BARRAS,
                        MED_CODIGO = pedido.MED_CODIGO,
                        Codigo_Pedido = pedido.PED_CODIGO


                    };
                    Int64 codCuenta;
                    CUENTAS_PACIENTES cueCodigo = contexto.CUENTAS_PACIENTES.OrderByDescending(c => c.CUE_CODIGO).FirstOrDefault();
                    codCuenta = cueCodigo != null ? cueCodigo.CUE_CODIGO + 1 : 1;
                    cuenta.CUE_CODIGO = codCuenta;
                    contexto.AddToCUENTAS_PACIENTES(cuenta);
                    contexto.SaveChanges();
                    GuardaCostoCuentaPacientes(Convert.ToString(ndetalle.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value),
                        pedido.PED_CODIGO, NumVale);


                    ArreglaIVA(Convert.ToString(pedido.ATE_CODIGO), codCuenta, Convert.ToString(ndetalle.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value));
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }

        }

        public DataTable VerDespachos(DateTime desde, DateTime hasta, bool pedido, bool despacho, string hc, string piso, string hab, int ped_codigo)
        {

            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            string xWhere = "WHERE ";
            if (pedido)
            {
                if (xWhere.Length <= 6)
                    xWhere += " CONVERT (date,FECHA) BETWEEN '" + desde.ToString("dd-MM-yyyy 00:00:00") + "' AND '" + hasta.ToString("dd-MM-yyyy 23:59:59") + "' ";
            }
            if (ped_codigo != 0)
            {
                if (xWhere.Length <= 6)
                    xWhere += " [CODIGO PEDIDO] = " + ped_codigo.ToString() + " ";
                else
                    xWhere += " AND [CODIGO PEDIDO] = " + ped_codigo.ToString() + " ";
            }
            if (despacho)
            {
                if (xWhere.Length <= 6)
                    xWhere += " despachados = 1 ";
                else
                    xWhere += " AND despachados = 1 ";
            }
            else
            {
                if (xWhere.Length <= 6)
                    xWhere += " despachados = 0 ";
                else
                    xWhere += " AND despachados = 0 ";
            }
            if (hc != "0")
            {
                if (xWhere.Length <= 6)
                    xWhere += " [HIST. CLINICA] = " + hc;
                else
                    xWhere += " AND [HIST. CLINICA] = " + hc;
            }

            if (piso != "0")
            {
                if (hab != "0")
                {
                    if (xWhere.Length <= 6)
                    {
                        xWhere += " NIV_CODIGO = " + piso;
                        xWhere += " AND hab_Codigo = " + hab;
                    }
                    else
                    {
                        xWhere += " AND NIV_CODIGO = " + piso;
                        xWhere += " AND hab_Codigo = " + hab;
                    }
                }
                else
                {
                    if (xWhere.Length <= 6)
                        xWhere += " NIV_CODIGO = " + piso;
                    else
                        xWhere += " AND NIV_CODIGO = " + piso;
                }
            }
            if (hab != "0" && piso == "0")
            {
                if (xWhere.Length <= 6)
                    xWhere += " hab_Codigo = " + hab;
                else
                    xWhere += " AND hab_Codigo = " + hab;
            }

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("SELECT * FROM DESPACHOS " + xWhere + " ORDER BY FECHA DESC", connection);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;

            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
        public void ProductoBodega(string codpro, double existe, double bodega)
        {
            SqlCommand command = new SqlCommand();
            SqlConnection conexion;
            BaseContextoDatos obj = new BaseContextoDatos();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoBodega";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@existe", existe);
            command.Parameters.AddWithValue("@bodega", bodega);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public bool ActualizarKardexSic(string numdoc, int bodega)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;
            connection = obj.ConectarBd();
            connection.Open();
            transaction = connection.BeginTransaction();
            try
            {
                command = new SqlCommand("sp_ActualizaKardexSic", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@numdoc", numdoc);
                command.Parameters.AddWithValue("@bodega", bodega);
                command.Parameters.AddWithValue("@id_usuario", His.Entidades.Clases.Sesion.codUsuario);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                transaction.Commit();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                transaction.Rollback();
                return false;
            }


        }
        public bool ValidaDespacho(Int64 ped_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                DESPACHOS despachado = (from d in db.DESPACHOS
                                        where d.CODIGO_PEDIDO == ped_codigo
                                        select d).FirstOrDefault();

                if (despachado.DESPACHADO_POR != "")
                    return true;
                else
                    return false;
            }
        }
        public bool InsertarDespachos(List<DtoDespachos> dtoDespachos, int despacho, DateTime fecha)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;
            connection = obj.ConectarBd();
            connection.Open();

            transaction = connection.BeginTransaction();
            try
            {
                foreach (var item in dtoDespachos)
                {
                    command = new SqlCommand("sp_Pedidos_InsertDespacho", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transaction;
                    command.Parameters.AddWithValue("@ped_codigo", item.ped_codigo);
                    command.Parameters.AddWithValue("@id_usuario", His.Entidades.Clases.Sesion.codUsuario);
                    command.Parameters.AddWithValue("@observacion", item.observacion);
                    command.Parameters.AddWithValue("@despachado", despacho);
                    command.Parameters.AddWithValue("@fecha", fecha);
                    command.CommandTimeout = 180;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                transaction.Commit();
                connection.Close();
                return true;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false;
            }
        }
        public DataTable verPisos()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable table = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_VerPisos", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            table.Load(reader);
            reader.Close();
            connection.Close();
            return table;
        }
        public void DeleteDespacho(Int64 ped_codigo)

        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;
            connection = obj.ConectarBd();
            connection.Open();

            transaction = connection.BeginTransaction();
            try
            {
                command = new SqlCommand("sp_HonorarioEliminarDespacho", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@ped_codigo", ped_codigo);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                transaction.Commit();
                connection.Close();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }

        }
        public DataTable verHabitaciones(int piso)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable table = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_VerHabitaciones", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@niv_piso", piso);
            reader = command.ExecuteReader();
            table.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return table;
        }
        public string recureraBodega(Int64 bodega)
        {
            string _bodega = "";
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("select nomlocal from Sic3000..Locales where codlocal = " + bodega, connection);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                _bodega = Convert.ToString(reader["nomlocal"].ToString());
            }
            reader.Close();
            connection.Close();
            return _bodega;
        }
        public decimal validaCantidad(Int64 ATE_CODIGO, string PRO_CODIGO, Int64 Codigo_Pedido)
        {
            decimal codigo = 0;
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var cp = (from c in db.CUENTAS_PACIENTES
                          where c.ATE_CODIGO == ATE_CODIGO && c.PRO_CODIGO == PRO_CODIGO && c.Codigo_Pedido == Codigo_Pedido
                          select c).FirstOrDefault();
                codigo = (decimal)cp.CUE_CANTIDAD;
                return codigo;
            }
        }
    }
}

