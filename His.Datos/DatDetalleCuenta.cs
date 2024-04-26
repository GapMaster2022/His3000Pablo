using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using His.Entidades.Pedidos;
using System.Data.EntityClient;
using System.Data.Common;
using Microsoft.Data.Extensions;
using System.Data.SqlClient;
using System.Data;

namespace His.Datos
{
    public class DatDetalleCuenta
    {
        public List<PEDIDOS> recuperarPedidos(int codigoAtencion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PEDIDOS
                        where p.ATE_CODIGO == codigoAtencion
                        select p).ToList();
            }
        }

        public List<PEDIDOS_AREAS> recuperarPedidosAreas()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PEDIDOS_AREAS
                        where p.PEA_ESTADO == true
                        select p).ToList();
            }
        }

        public PEDIDOS_AREAS recuperarPedidosAreas(int codPedidoA)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PEDIDOS_AREAS
                        where p.PEA_CODIGO == codPedidoA
                        select p).FirstOrDefault();
            }
        }

        public List<DtoDetalleCuentaPaciente> recuperarPedidosPaciente(int codigoAtencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return
                        (from pa in contexto.PEDIDOS_AREAS
                         join p in contexto.PEDIDOS on pa.PEA_CODIGO equals p.PEDIDOS_AREAS.PEA_CODIGO
                         join r in contexto.RUBROS on pa.RUB_CODIGO equals r.RUB_CODIGO
                         join d in contexto.PEDIDOS_DETALLE on p.PED_CODIGO equals d.PEDIDOS.PED_CODIGO
                         where p.ATE_CODIGO == codigoAtencion
                         select new DtoDetalleCuentaPaciente
                         {
                             INDICE = p.PED_CODIGO,
                             //PEA_CODIGO = p.PEDIDOS_AREAS.PEA_CODIGO,
                             AREA = r.RUB_GRUPO,
                             SUBAREA = r.RUB_NOMBRE,
                             //PED_FECHA = p.PED_FECHA.Value,                       
                             CODIGO = (d.PRODUCTO.PRO_CODIGO).ToString(),
                             DESCRIPCION = d.PRO_DESCRIPCION,
                             CANTIDAD = d.PDD_CANTIDAD.Value,
                             VALOR = d.PDD_VALOR.Value,
                             IVA = d.PDD_IVA.Value,
                             TOTAL = d.PDD_TOTAL.Value
                         }).ToList();


                    //(from p in contexto.PEDIDOS
                    // join pa in contexto.PEDIDOS_AREAS on p.PEDIDOS_AREAS.PEA_CODIGO equals pa.PEA_CODIGO
                    // join d in contexto.PEDIDOS_DETALLE on p.PED_CODIGO equals d.PEDIDOS.PED_CODIGO                                                  
                    // where p.ATE_CODIGO == codigoAtencion
                    // select new DtoDetalleCuentaPaciente
                    // {
                    //     INDICE = p.PED_CODIGO,
                    //     //PEA_CODIGO = p.PEDIDOS_AREAS.PEA_CODIGO,
                    //     AREA = pa.PEA_NOMBRE,                             
                    //     //PED_FECHA = p.PED_FECHA.Value,                       
                    //     CODIGO = d.PRODUCTO.PRO_CODIGO,
                    //     DESCRIPCION = d.PRO_DESCRIPCION,
                    //     CANTIDAD = d.PDD_CANTIDAD.Value,
                    //     VALOR = d.PDD_VALOR.Value,
                    //     IVA = d.PDD_IVA.Value,
                    //     TOTAL = d.PDD_TOTAL.Value
                    // }).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }

        /// <summary>
        /// Método que permite recuperar los Detalles de la Cuenta Paciente, según Áreas, Servicios, Productos
        /// </summary>
        /// <param name="codigoAtencion"> el parametro recibido es el código de la atención</param>
        /// <returns>una lista con los datos de la cuenta según el detalle cuenta</returns>
        /// 

        public DataTable ListaNuevos(Int64 ateCodigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_MuestraItemsNuevosAuditoria", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = ateCodigo;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }
        public DataTable MuestraItemsModificados(Int64 ateCodigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_MuestraItemsModificadosAuditoria", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = ateCodigo;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }

        public DataTable RecuperaCuentaPacinteSP(Int64 ate_codigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_RecuperarCuentaPaciente", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = ate_codigo;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }

        public DataTable RecuperaCuentaPacinteSPFacturado(Int64 ate_codigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_RecuperarCuentaPacienteFacturado", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@Ate_Codigo"].Value = ate_codigo;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }

        public List<DtoDetalleCuentaPaciente> recuperarCuentaPaciente(Int64 ate_codigo)
        {
            try
            {
                List<DtoDetalleCuentaPaciente> lista = new List<DtoDetalleCuentaPaciente>();
                SqlConnection Sqlcon;
                SqlCommand Sqlcmd;
                SqlDataAdapter Sqldap;
                DataTable Dts;
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
                Sqlcmd = new SqlCommand("sp_Detalle_Auditoria", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Parameters.Add("@ate_Codigo", SqlDbType.BigInt);
                Sqlcmd.Parameters["@ate_Codigo"].Value = ate_codigo;

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataTable();
                Sqldap.Fill(Dts);
                int r = Convert.ToInt16(Dts.Rows.Count);
                for (int i = 0; i < r; i++)
                {
                    DtoDetalleCuentaPaciente pda = new DtoDetalleCuentaPaciente();
                    pda.INDICE = Convert.ToInt64(Dts.Rows[i]["INDEX"]);
                    pda.AREA = Convert.ToString(Dts.Rows[i]["AREA"]);
                    pda.SUBAREA = Convert.ToString(Dts.Rows[i]["SUBAREA"]);
                    pda.FECHA = Convert.ToDateTime(Dts.Rows[i]["FECHA"]);
                    pda.DESCRIPCION = Convert.ToString(Dts.Rows[i]["DESCRIPCION"]);
                    pda.CODIGO = Convert.ToString(Dts.Rows[i]["CODIGO"]);
                    pda.VALOR = Convert.ToDecimal(Dts.Rows[i]["VALOR"]);
                    pda.CANTIDAD_ORIGINAL = Convert.ToDouble(Dts.Rows[i]["CANTIDAD_ORIGINAL"]);
                    pda.CANTIDAD_DEVUELTA = Convert.ToDouble(Dts.Rows[i]["CANTIDAD_DEVUELTA"]);
                    pda.CANTIDAD = Convert.ToDecimal(Dts.Rows[i]["CANTIDAD"]);
                    pda.TOTAL = Convert.ToDecimal(Dts.Rows[i]["TOTAL"]);
                    pda.IVA = Convert.ToDecimal(Dts.Rows[i]["IVA"]);
                    pda.RUBRO = Convert.ToInt64(Dts.Rows[i]["RUBRO"]);
                    pda.RUBRO_NOMBRE = Convert.ToString(Dts.Rows[i]["RUBRO_NOMBRE"]);
                    pda.MEDICO_COD = Convert.ToInt16(Dts.Rows[i]["MEDICO_COD"]);
                    pda.MEDICO_NOMBRE = Convert.ToString(Dts.Rows[i]["MEDICO_NOMBRE"]);
                    pda.NumeroControl = Convert.ToString(Dts.Rows[i]["NumeroControl"]);
                    pda.TipoMedico = Convert.ToInt16(Dts.Rows[i]["TipoMedico"]);
                    pda.AREA_PEDIDO = Convert.ToString(Dts.Rows[i]["AREA_PEDIDO"]);
                    pda.OBSERVACION = Convert.ToString(Dts.Rows[i]["OBSERVACION"]);

                    lista.Add(pda);

                }
                try
                {
                    Sqlcon.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return lista;
                //using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                //{
                //    return
                //        (from cp in contexto.CUENTAS_PACIENTES
                //         join a in contexto.ATENCIONES on cp.ATE_CODIGO equals a.ATE_CODIGO
                //         join r in contexto.RUBROS on cp.RUB_CODIGO equals r.RUB_CODIGO //aumenta la subarea en el detalle de cuentas david mantilla 04-09-2012
                //         join m in contexto.MEDICOS on cp.MED_CODIGO equals m.MED_CODIGO
                //         join p in contexto.PEDIDOS on cp.Codigo_Pedido equals p.PED_CODIGO into ped
                //         from pe in ped.DefaultIfEmpty()
                //         join pedDeta in contexto.PEDIDOS_DETALLE on pe.PED_CODIGO equals pedDeta.PEDIDOS.PED_CODIGO
                //         join h in contexto.HABITACIONES on pe.HAB_CODIGO equals h.hab_Codigo into hab
                //         from ha in hab.DefaultIfEmpty()
                //         join n in contexto.NIVEL_PISO on ha.NIVEL_PISO.NIV_CODIGO equals n.NIV_CODIGO into nivel
                //         from niv in nivel.DefaultIfEmpty()
                //         join dev in contexto.PEDIDO_DEVOLUCION on pe.PED_CODIGO equals dev.Ped_Codigo into devolucion
                //         from devo in devolucion.DefaultIfEmpty()
                //         join deta in contexto.PEDIDO_DEVOLUCION_DETALLE on devo.DevCodigo equals deta.DevCodigo into devo_deta
                //         from detall in devo_deta.DefaultIfEmpty()
                //         where cp.ATE_CODIGO == codigoAtencion && cp.CUE_ESTADO == 1 && cp.CUE_CANTIDAD > 0
                //         select new DtoDetalleCuentaPaciente
                //         {
                //INDICE = cp.CUE_CODIGO,
                //AREA = r.RUB_GRUPO,
                //SUBAREA = r.RUB_NOMBRE,
                //FECHA = cp.CUE_FECHA.Value,
                //DESCRIPCION = cp.CUE_DETALLE,
                //CODIGO = cp.PRO_CODIGO_BARRAS,
                //             VALOR = cp.CUE_VALOR_UNITARIO.Value,
                //             CANTIDAD_ORIGINAL = pedDeta.PDD_CANTIDAD > 0 ? Convert.ToDouble(pedDeta.PDD_CANTIDAD) : Convert.ToDouble(0.00),
                //             CANTIDAD_DEVUELTA = detall.DevDetCantidad > 0 ? Convert.ToDouble(detall.DevDetCantidad) : Convert.ToDouble(0.00),
                //             CANTIDAD = cp.CUE_CANTIDAD.Value,
                //             TOTAL = cp.CUE_VALOR.Value,
                //             IVA = cp.CUE_IVA.Value,
                //             RUBRO = cp.RUB_CODIGO.Value,
                //             RUBRO_NOMBRE = r.RUB_GRUPO,
                //             MEDICO_COD = m.MED_CODIGO,
                //             MEDICO_NOMBRE = m.MED_APELLIDO_PATERNO + " " + m.MED_APELLIDO_MATERNO + " " + m.MED_NOMBRE1 + " " + m.MED_APELLIDO_MATERNO,
                //             NumeroControl = cp.CUE_NUM_CONTROL,
                //             TipoMedico = cp.Id_Tipo_Medico.Value != null ? cp.Id_Tipo_Medico.Value : 0,
                //             AREA_PEDIDO = niv.NIV_NOMBRE
                //         }).ToList();
                //}
            }
            catch (Exception err) { throw err; }
        }

        public int VerificaCambioCuenta(long ATE_CODIGO, string PRO_CODIGO_BARRAS, long DetalleCodigo)
        {
            // Verifico si un item tiene registrado cambios / GIOVANNY TAPIA / 20/08/2012

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
            Sqlcmd = new SqlCommand("sp_VerificaCambioCuenta", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = ATE_CODIGO;

            Sqlcmd.Parameters.Add("@PRO_CODIGO_BARRAS", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PRO_CODIGO_BARRAS"].Value = PRO_CODIGO_BARRAS;

            Sqlcmd.Parameters.Add("@CUE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@CUE_CODIGO"].Value = DetalleCodigo;


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            if (Dts.Tables["tabla"].Rows.Count > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public DataTable CargaItemsModificados(long ATE_CODIGO)
        {
            // Verifico si un item tiene registrado cambios / GIOVANNY TAPIA / 20/08/2012

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_CargaCuentasModificadas", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.BigInt);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = ATE_CODIGO;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return Dts;
        }

        public DataTable MuestraItemsModificados(long ATE_CODIGO, string PRO_CODIGO_BARRAS, long DetalleCodigo)
        {
            // Muestra los items modificados en una cuenta / GIOVANNY TAPIA / 20/08/2012

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_MuestraItemsModificados", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = ATE_CODIGO;

            Sqlcmd.Parameters.Add("@PRO_CODIGO_BARRAS", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PRO_CODIGO_BARRAS"].Value = PRO_CODIGO_BARRAS;

            Sqlcmd.Parameters.Add("@CUE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@CUE_CODIGO"].Value = DetalleCodigo;


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return Dts;
        }

        public DataTable MuestraItemsModificados1(long ATE_CODIGO)
        {
            // Muestra los items modificados en una cuenta / GIOVANNY TAPIA / 20/08/2012

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_MuestraItemsModificados1", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = ATE_CODIGO;


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return Dts;
        }

        public DataTable MuestraItemsNuevos(long ATE_CODIGO)
        {
            // Muestra los items modificados en una cuenta / GIOVANNY TAPIA / 20/08/2012

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_MuestraItemsNuevos", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = ATE_CODIGO;


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return Dts;
        }

        public DataTable ListaItemsEliminadosCuenta(long CodigoCuenta)
        {
            // Lista todos los items eliminados de una cuenta / GIOVANNY TAPIA / 20/08/2012

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_VerificaItemsEliminadosCuenta", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = CodigoCuenta;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return Dts;

        }

        public DataTable RecuperaOtroCliente(string Ruc)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_RecuperaOtroPaciente", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ruc", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Ruc"].Value = Ruc;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }

        public DataTable RecuperaClienteSIC(string Nombre)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_RecuperaClienteSIC", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Nombre", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Nombre"].Value = Nombre;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }

        public DataTable ReferidoPaciente(Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ReferidoPaciente", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

        public DataTable ConvenioPago(string cat_nombre)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ConvenioPago", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cat_nombre", cat_nombre);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
        public DataTable PacientesAuditoria(DateTime desde, DateTime hasta, bool ingreso, bool alta, string hc)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();
            DataTable tabla = new DataTable();

            string xWhere = "WHERE ";

            if (ingreso)
            {
                if (xWhere.Length <= 6)
                {
                    xWhere += "CONVERT (date, A.ATE_FECHA_INGRESO) BETWEEN '" + desde.ToString("dd-MM-yyyy 00:00:00") + "' AND '" + hasta.ToString("dd-MM-yyyy 23:59:59") + "'";
                }

            }
            if (alta)
            {
                if (xWhere.Length <= 6)
                    xWhere += "CONVERT (date, A.ATE_FECHA_ALTA) BETWEEN '" + desde.ToString("dd-MM-yyyy 00:00:00") + "' AND '" + hasta.ToString("dd-MM-yyyy 23:59:59") + "'";
                else
                    xWhere += " AND CONVERT (date, A.ATE_FECHA_ALTA) BETWEEN '" + desde.ToString("dd-MM-yyyy 00:00:00") + "' AND '" + hasta.ToString("dd-MM-yyyy 23:59:59") + "'";
            }
            if (hc != "0")
            {
                if (xWhere.Length <= 6)
                    xWhere += "P.PAC_HISTORIA_CLINICA = '" + hc + "'";
                else
                    xWhere += " AND P.PAC_HISTORIA_CLINICA = '" + hc + "'";
            }
            if (!ingreso && !alta && hc == "0")
                xWhere = "";
            command = new SqlCommand("SELECT A.ATE_FECHA_INGRESO AS 'F. INGRESO', A.ATE_FECHA_ALTA AS 'F. ALTA', "
            + "P.PAC_HISTORIA_CLINICA AS 'HIST. CLINICO', A.ATE_NUMERO_ATENCION AS 'No ATENCION', "
            + "P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS PACIENTE, "
            + "P.PAC_IDENTIFICACION AS 'IDENTIFICACION', H.hab_Numero AS 'HAB.', CC.CAT_NOMBRE AS ASEGURADORA, A.ATE_CODIGO "
            + "FROM CUENTAS_PACIENTES_HISTORIAL CPH "
            + "INNER JOIN ATENCIONES A ON CPH.ATE_CODIGO = A.ATE_CODIGO "
            + "INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO "
            + "INNER JOIN HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo "
            + "INNER JOIN ATENCION_DETALLE_CATEGORIAS ADC ON A.ATE_CODIGO = ADC.ATE_CODIGO "
            + "INNER JOIN CATEGORIAS_CONVENIOS CC ON ADC.CAT_CODIGO = CC.CAT_CODIGO " + xWhere
            + " GROUP BY A.ATE_FECHA_INGRESO, A.ATE_FECHA_ALTA, P.PAC_HISTORIA_CLINICA, A.ATE_NUMERO_ATENCION, "
            + "P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 , "
            + "P.PAC_IDENTIFICACION, H.hab_Numero, CC.CAT_NOMBRE, A.ATE_CODIGO ", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            tabla.Load(reader);
            reader.Close();
            connection.Close();
            return tabla;
        }

    }
}
