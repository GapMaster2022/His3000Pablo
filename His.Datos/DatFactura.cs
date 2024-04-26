using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using His.Entidades.General;
using System.Data.SqlClient;
using System.Data;

namespace His.Datos
{
    public class DatFactura
    {
        //. Recupera el numero mayor del codigo de Factura
        public int recuperaMaximoFactura()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var id = contexto.FACTURA.Select(f => f.FAC_CODIGO).Count();
                    if (id > 0)
                        return contexto.FACTURA.Select(f => f.FAC_CODIGO).Max();
                    else
                        return 0;
                }
            }
            catch (Exception err) { throw err; }

        }

        public bool RecuperaAtencion(Int64 ateCodigo)
        {
            try
            {
                ATENCIONES ate = new ATENCIONES();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    ate = (from a in contexto.ATENCIONES
                           where a.ATE_CODIGO == ateCodigo
                           select a).FirstOrDefault();
                    if (ate.ESC_CODIGO == 6)
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception err) { throw err; }

        }

        public bool AgrupacionCuentas(Int64 ateCodigo)
        {
            try
            {
                using (var constexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<AGRUPACION_CUENTAS> obj = (from a in constexto.AGRUPACION_CUENTAS
                                                    where a.ate_codigo_madre == ateCodigo
                                                    select a).ToList();

                    if (obj != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void crearFactura(FACTURA factura)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToFACTURA(factura);
                contexto.SaveChanges();
            }
        }

        public DataTable FacturaDuplicada(string FacturaActual)
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

            Sqlcmd = new SqlCommand("sp_FacturaActual", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@FacturaActual", SqlDbType.VarChar);
            Sqlcmd.Parameters["@FacturaActual"].Value = (FacturaActual);

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

        public DataTable FacturaDuplicada2(string FacturaActual)
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

            Sqlcmd = new SqlCommand("sp_FacturaActual2", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@FacturaActual", SqlDbType.VarChar);
            Sqlcmd.Parameters["@FacturaActual"].Value = (FacturaActual);

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
        public DataTable VerificaFactura(Int64 Ate_Codigo)
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

            Sqlcmd = new SqlCommand("sp_VerificaFactura", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@Ate_Codigo"].Value = (Ate_Codigo);

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

        public DataTable VerificaFactura2(Int64 Ate_Codigo)
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

            Sqlcmd = new SqlCommand("sp_VerificaFactura2", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@Ate_Codigo"].Value = (Ate_Codigo);

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

        public int CrearFacturaSic3000(DtoFacturaSic3000 Factura, Int64 HistoriaClinica, Int64 CodigoAtencion, List<DtoFacturaPago> FacturaPago, List<DtoEstadoCuenta> EstadoCuenta, List<DtoCXC> CxC, int Prefactura, int ateCodigo, string RucPaciente, string nombrePaciente, string Direc_Cliente, string txt_Telef_P, DateTime ATE_FECHA_INGRESO, DateTime FechaAlta, string Medico, Int16 caja, string identificador, string email)
        {
            Int32 Resultado = 0;
            String CodigoCliente = "";

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
                decimal total = Math.Round((Factura.total), 2);
                decimal subtotal = Math.Round((Factura.subtotal), 2);
                decimal iva = Math.Round((Factura.iva), 2);
                decimal conIva = Math.Round((Factura.totciva), 2);
                decimal sinIva = Math.Round((Factura.totsiva), 2);
                decimal descuento = Math.Round((Factura.desctot), 2);


                Sqlcmd = new SqlCommand("sp_IncrementaNumeroFactura", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@caja", SqlDbType.VarChar);
                Sqlcmd.Parameters["@caja"].Value = (caja);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts);


                Sqlcmd = new SqlCommand("sp_GuardaIdentificadorEmail", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@cod_Identificador", SqlDbType.VarChar);
                Sqlcmd.Parameters["@cod_Identificador"].Value = (identificador);
                Sqlcmd.Parameters.Add("@Factura", SqlDbType.VarChar);
                Sqlcmd.Parameters["@Factura"].Value = (Factura.numnot);
                Sqlcmd.Parameters.Add("@email_factura", SqlDbType.VarChar);
                Sqlcmd.Parameters["@email_factura"].Value = (email);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts);



                Sqlcmd = new SqlCommand("sp_GuardaFactura", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@numnot", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@numnot"].Value = (Factura.numnot);

                Sqlcmd.Parameters.Add("@tipdoc", SqlDbType.Float);
                Sqlcmd.Parameters["@tipdoc"].Value = (Factura.tipdoc);

                Sqlcmd.Parameters.Add("@codloc", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@codloc"].Value = (Factura.codloc);

                Sqlcmd.Parameters.Add("@codven", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@codven"].Value = (Factura.codven);

                Sqlcmd.Parameters.Add("@numfac", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@numfac"].Value = (Factura.numfac);

                Sqlcmd.Parameters.Add("@codcli", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@codcli"].Value = (Factura.codcli);

                Sqlcmd.Parameters.Add("@tipcli", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@tipcli"].Value = (Factura.tipcli);

                Sqlcmd.Parameters.Add("@fecha", SqlDbType.DateTime);
                Sqlcmd.Parameters["@fecha"].Value = (Factura.fecha);

                Sqlcmd.Parameters.Add("@hora", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@hora"].Value = (Factura.hora);

                Sqlcmd.Parameters.Add("@ruc", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@ruc"].Value = (Factura.ruc);

                Sqlcmd.Parameters.Add("@pordes", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@pordes"].Value = (Factura.pordes);

                Sqlcmd.Parameters.Add("@subtotal", SqlDbType.Float);
                Sqlcmd.Parameters["@subtotal"].Value = subtotal;

                Sqlcmd.Parameters.Add("@desctot", SqlDbType.Float);
                Sqlcmd.Parameters["@desctot"].Value = descuento;

                Sqlcmd.Parameters.Add("@totsiva", SqlDbType.Float);
                Sqlcmd.Parameters["@totsiva"].Value = sinIva;

                Sqlcmd.Parameters.Add("@totciva", SqlDbType.Float);
                Sqlcmd.Parameters["@totciva"].Value = conIva;

                Sqlcmd.Parameters.Add("@iva", SqlDbType.Float);
                Sqlcmd.Parameters["@iva"].Value = iva;

                Sqlcmd.Parameters.Add("@total", SqlDbType.Float);
                Sqlcmd.Parameters["@total"].Value = total;

                Sqlcmd.Parameters.Add("@regalia", SqlDbType.Float);
                Sqlcmd.Parameters["@regalia"].Value = (Factura.regalia);

                Sqlcmd.Parameters.Add("@fecha1", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@fecha1"].Value = (Factura.fecha1);

                Sqlcmd.Parameters.Add("@cancelado", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@cancelado"].Value = (Factura.cancelado);

                Sqlcmd.Parameters.Add("@items", SqlDbType.Float);
                Sqlcmd.Parameters["@items"].Value = (Factura.items);

                Sqlcmd.Parameters.Add("@caja", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@caja"].Value = (Factura.caja);

                Sqlcmd.Parameters.Add("@nomcli", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@nomcli"].Value = (Factura.nomcli);

                Sqlcmd.Parameters.Add("@dircli", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@dircli"].Value = (Factura.dircli);

                Sqlcmd.Parameters.Add("@telcli", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@telcli"].Value = (Factura.telcli);

                Sqlcmd.Parameters.Add("@ruccli", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@ruccli"].Value = (Factura.ruccli);

                Sqlcmd.Parameters.Add("@obs", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@obs"].Value = (Factura.obs);

                Sqlcmd.Parameters.Add("@numguirem", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@numguirem"].Value = (Factura.numguirem);

                Sqlcmd.Parameters.Add("@motivo", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@motivo"].Value = (Factura.motivo);

                Sqlcmd.Parameters.Add("@ructra", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@ructra"].Value = (Factura.ructra);

                Sqlcmd.Parameters.Add("@nomtra", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@nomtra"].Value = (Factura.nomtra);

                Sqlcmd.Parameters.Add("@codcobcli", SqlDbType.Float);
                Sqlcmd.Parameters["@codcobcli"].Value = (Factura.codcobcli);

                Sqlcmd.Parameters.Add("@codvencli", SqlDbType.Float);
                Sqlcmd.Parameters["@codvencli"].Value = (Factura.codvencli);

                Sqlcmd.Parameters.Add("@fecven", SqlDbType.DateTime);
                Sqlcmd.Parameters["@fecven"].Value = (Factura.fecven);

                Sqlcmd.Parameters.Add("@numorden", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@numorden"].Value = (Factura.numorden);

                Sqlcmd.Parameters.Add("@porven", SqlDbType.Float);
                Sqlcmd.Parameters["@porven"].Value = (Factura.porven);

                Sqlcmd.Parameters.Add("@formpagPro", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@formpagPro"].Value = (Factura.formpagPro);

                Sqlcmd.Parameters.Add("@validez", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@validez"].Value = (Factura.validez);

                Sqlcmd.Parameters.Add("@tiempoentrega", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@tiempoentrega"].Value = (Factura.tiempoentrega);

                Sqlcmd.Parameters.Add("@numfac2", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@numfac2"].Value = (Factura.numfac2);

                Sqlcmd.Parameters.Add("@porcobrar", SqlDbType.Bit);
                Sqlcmd.Parameters["@porcobrar"].Value = (Factura.porcobrar);

                Sqlcmd.Parameters.Add("@Impresa", SqlDbType.Bit);
                Sqlcmd.Parameters["@Impresa"].Value = (Factura.Impresa);

                Sqlcmd.Parameters.Add("@cajero", SqlDbType.Float);
                Sqlcmd.Parameters["@cajero"].Value = (Factura.cajero);

                Sqlcmd.Parameters.Add("@subt_Dev", SqlDbType.Float);
                Sqlcmd.Parameters["@subt_Dev"].Value = (Factura.subt_Dev);

                Sqlcmd.Parameters.Add("@coniva_Dev", SqlDbType.Float);
                Sqlcmd.Parameters["@coniva_Dev"].Value = (Factura.coniva_Dev);

                Sqlcmd.Parameters.Add("@siniva_Dev", SqlDbType.Float);
                Sqlcmd.Parameters["@siniva_Dev"].Value = (Factura.siniva_Dev);

                Sqlcmd.Parameters.Add("@desct_Dev", SqlDbType.Float);
                Sqlcmd.Parameters["@desct_Dev"].Value = (Factura.desct_Dev);

                Sqlcmd.Parameters.Add("@iva_Dev", SqlDbType.Float);
                Sqlcmd.Parameters["@iva_Dev"].Value = (Factura.iva_Dev);

                Sqlcmd.Parameters.Add("@Tot_Dev", SqlDbType.Float);
                Sqlcmd.Parameters["@Tot_Dev"].Value = (Factura.Tot_Dev);

                Sqlcmd.Parameters.Add("@pormayor", SqlDbType.Bit);
                Sqlcmd.Parameters["@pormayor"].Value = (Factura.pormayor);

                Sqlcmd.Parameters.Add("@facturada", SqlDbType.Bit);
                Sqlcmd.Parameters["@facturada"].Value = (Factura.facturada);

                Sqlcmd.Parameters.Add("@coniva", SqlDbType.Bit);
                Sqlcmd.Parameters["@coniva"].Value = (Factura.coniva);

                Sqlcmd.Parameters.Add("@imprimedesct", SqlDbType.Bit);
                Sqlcmd.Parameters["@imprimedesct"].Value = (Factura.imprimedesct);

                Sqlcmd.Parameters.Add("@autorizacion", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@autorizacion"].Value = (Factura.autorizacion);

                Sqlcmd.Parameters.Add("@GrupoCliente", SqlDbType.Bit);
                Sqlcmd.Parameters["@GrupoCliente"].Value = (Factura.GrupoCliente);

                Sqlcmd.Parameters.Add("@EmpId", SqlDbType.Int);
                Sqlcmd.Parameters["@EmpId"].Value = (Factura.EmpId);

                Sqlcmd.Parameters.Add("@ConvId", SqlDbType.Int);
                Sqlcmd.Parameters["@ConvId"].Value = (Factura.ConvId);

                Sqlcmd.Parameters.Add("@HistoriaClinica", SqlDbType.BigInt);
                Sqlcmd.Parameters["@HistoriaClinica"].Value = (HistoriaClinica);

                Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.BigInt);
                Sqlcmd.Parameters["@CodigoAtencion"].Value = (CodigoAtencion);

                Sqlcmd.Parameters.Add("@tipoIdentificacion", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@tipoIdentificacion"].Value = (identificador);

                Sqlcmd.Parameters.Add("@porcentajeIva", SqlDbType.Float);
                Sqlcmd.Parameters["@porcentajeIva"].Value = (Factura.porcentajeIva);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts);

                CodigoCliente = Dts.Rows[0]["RESULTADO"].ToString();

                string ayuda = (Factura.numnot);

                foreach (var Detalle in Factura.DetalleFactura)
                {
                    //mando valores automaticos a cuenta paciente **Pablo Rocha 19/10/2013

                    if ((Detalle.codpro).ToString() == "22" || (Detalle.codpro).ToString() == "22111" || (Detalle.codpro).ToString() == "2" || (Detalle.codpro).ToString() == "24" || (Detalle.codpro).ToString() == "23")
                    {
                        try
                        {
                            Sqlcmd = new SqlCommand("sp_GuardaCuentaPaciente", Sqlcon);
                            Sqlcmd.CommandType = CommandType.StoredProcedure;
                            Sqlcmd.Transaction = transaction;

                            Sqlcmd.Parameters.Add("@codpro", SqlDbType.NVarChar);
                            Sqlcmd.Parameters["@codpro"].Value = (Detalle.codpro);

                            Sqlcmd.Parameters.Add("@atecod", SqlDbType.Int);
                            Sqlcmd.Parameters["@atecod"].Value = (CodigoAtencion);

                            Sqlcmd.Parameters.Add("@rubro", SqlDbType.Int);
                            Sqlcmd.Parameters["@rubro"].Value = (Convert.ToInt16(Detalle.codpro));

                            Sqlcmd.Parameters.Add("@precio", SqlDbType.Decimal);
                            Sqlcmd.Parameters["@precio"].Value = (Detalle.precio);

                            Sqlcmd.Parameters.Add("@factura", SqlDbType.NVarChar);
                            Sqlcmd.Parameters["@factura"].Value = (Factura.numfac);

                            Sqlcmd.Parameters.Add("@usuario", SqlDbType.SmallInt);
                            Sqlcmd.Parameters["@usuario"].Value = (Sesion.codUsuario);

                            Sqldap = new SqlDataAdapter();
                            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                            Sqldap.SelectCommand = Sqlcmd;
                            Sqldap.Fill(Dts);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return 0; // los datos no se almacenaron
                        }

                    }
                    //****************************************************************

                    Sqlcmd = new SqlCommand("sp_GuardaDetalleFactura", Sqlcon);
                    Sqlcmd.CommandType = CommandType.StoredProcedure;
                    Sqlcmd.Transaction = transaction;

                    Sqlcmd.Parameters.Add("@numnot", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@numnot"].Value = ayuda;

                    Sqlcmd.Parameters.Add("@tipdoc", SqlDbType.Float);
                    Sqlcmd.Parameters["@tipdoc"].Value = (Detalle.tipdoc);

                    Sqlcmd.Parameters.Add("@codpro", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@codpro"].Value = (Detalle.codpro);

                    Sqlcmd.Parameters.Add("@id", SqlDbType.SmallInt);
                    Sqlcmd.Parameters["@id"].Value = (Detalle.id);

                    Sqlcmd.Parameters.Add("@codloc", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@codloc"].Value = (Detalle.codloc);

                    Sqlcmd.Parameters.Add("@cantid", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@cantid"].Value = (Detalle.cantid);

                    Sqlcmd.Parameters.Add("@precio", SqlDbType.Decimal);
                    Sqlcmd.Parameters["@precio"].Value = (Detalle.precio);

                    Sqlcmd.Parameters.Add("@costo", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@costo"].Value = (Detalle.costo);

                    Sqlcmd.Parameters.Add("@desind", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@desind"].Value = (Detalle.desind);

                    Sqlcmd.Parameters.Add("@uniman", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@uniman"].Value = (Detalle.uniman);

                    Sqlcmd.Parameters.Add("@regalia", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@regalia"].Value = (Detalle.regalia);

                    Sqlcmd.Parameters.Add("@coddiv", SqlDbType.Float);
                    Sqlcmd.Parameters["@coddiv"].Value = (Detalle.coddiv);

                    Sqlcmd.Parameters.Add("@coddep", SqlDbType.Float);
                    Sqlcmd.Parameters["@coddep"].Value = (Detalle.coddep);

                    Sqlcmd.Parameters.Add("@codsec", SqlDbType.Float);
                    Sqlcmd.Parameters["@codsec"].Value = (Detalle.codsec);

                    Sqlcmd.Parameters.Add("@fechaven", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@fechaven"].Value = (Detalle.fechaven);

                    Sqlcmd.Parameters.Add("@caja", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@caja"].Value = (Detalle.caja);

                    Sqlcmd.Parameters.Add("@porcobrar", SqlDbType.Bit);
                    Sqlcmd.Parameters["@porcobrar"].Value = (Detalle.porcobrar);

                    Sqlcmd.Parameters.Add("@Plista", SqlDbType.Float);
                    Sqlcmd.Parameters["@Plista"].Value = (Detalle.Plista);

                    Sqlcmd.Parameters.Add("@Iva", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@Iva"].Value = (Detalle.Iva);

                    Sqlcmd.Parameters.Add("@cajero", SqlDbType.Float);
                    Sqlcmd.Parameters["@cajero"].Value = (Detalle.cajero);

                    Sqlcmd.Parameters.Add("@candev", SqlDbType.Float);
                    Sqlcmd.Parameters["@candev"].Value = (Detalle.candev);

                    Sqlcmd.Parameters.Add("@encero", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@encero"].Value = (Detalle.encero);

                    Sqlcmd.Parameters.Add("@porcentajeIva", SqlDbType.Float);
                    Sqlcmd.Parameters["@porcentajeIva"].Value = (Detalle.porcentajeIva);

                    Sqlcmd.Parameters.Add("@descuento", SqlDbType.Float);
                    Sqlcmd.Parameters["@descuento"].Value = Math.Round((Detalle.descuento), 2);

                    Sqldap = new SqlDataAdapter();
                    Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                    Sqldap.SelectCommand = Sqlcmd;
                    Sqldap.Fill(Dts);

                }

                foreach (var FacturaP in FacturaPago)
                {
                    Sqlcmd = new SqlCommand("sp_CreaFacturaPago", Sqlcon);
                    Sqlcmd.CommandType = CommandType.StoredProcedure;

                    Sqlcmd.Transaction = transaction;

                    Sqlcmd.Parameters.Add("@numdoc", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@numdoc"].Value = FacturaP.numdoc;

                    Sqlcmd.Parameters.Add("@tipdoc", SqlDbType.Float);
                    Sqlcmd.Parameters["@tipdoc"].Value = Factura.tipdoc;

                    Sqlcmd.Parameters.Add("@forpag", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@forpag"].Value = FacturaP.forpag;

                    Sqlcmd.Parameters.Add("@tipomov", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@tipomov"].Value = FacturaP.tipomov;

                    Sqlcmd.Parameters.Add("@codcli", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@codcli"].Value = CodigoCliente;

                    Sqlcmd.Parameters.Add("@parcial", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@parcial"].Value = FacturaP.parcial;

                    Sqlcmd.Parameters.Add("@parcial1", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@parcial1"].Value = FacturaP.parcial1;

                    Sqlcmd.Parameters.Add("@claspag", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@claspag"].Value = FacturaP.claspag;

                    Sqlcmd.Parameters.Add("@tipoventa", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@tipoventa"].Value = FacturaP.tipoventa;

                    Sqlcmd.Parameters.Add("@fecha", SqlDbType.DateTime);
                    Sqlcmd.Parameters["@fecha"].Value = FacturaP.fecha;

                    Sqlcmd.Parameters.Add("@fecha1", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@fecha1"].Value = FacturaP.fecha1;

                    Sqlcmd.Parameters.Add("@banco", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@banco"].Value = FacturaP.banco;

                    Sqlcmd.Parameters.Add("@numcuenta_tarj", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@numcuenta_tarj"].Value = FacturaP.numcuenta_tarj;

                    Sqlcmd.Parameters.Add("@cheque_caduca", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@cheque_caduca"].Value = FacturaP.cheque_caduca;

                    Sqlcmd.Parameters.Add("@dueño", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@dueño"].Value = FacturaP.dueño;

                    Sqlcmd.Parameters.Add("@autoriza", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@autoriza"].Value = FacturaP.autoriza;

                    Sqlcmd.Parameters.Add("@obs", SqlDbType.NText);
                    Sqlcmd.Parameters["@obs"].Value = FacturaP.obs;

                    Sqlcmd.Parameters.Add("@fila", SqlDbType.Float);
                    Sqlcmd.Parameters["@fila"].Value = FacturaP.fila;

                    Sqlcmd.Parameters.Add("@caja", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@caja"].Value = FacturaP.caja;

                    Sqlcmd.Parameters.Add("@cajero", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@cajero"].Value = FacturaP.cajero;

                    Sqlcmd.Parameters.Add("@Vendedor", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@Vendedor"].Value = FacturaP.Vendedor;

                    Sqlcmd.Parameters.Add("@local", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@local"].Value = FacturaP.local;

                    Sqlcmd.Parameters.Add("@Arqueada", SqlDbType.Bit);
                    if (FacturaP.Arqueada == false)
                    {
                        Sqlcmd.Parameters["@Arqueada"].Value = 0;
                    }
                    else
                    {
                        Sqlcmd.Parameters["@Arqueada"].Value = 1;
                    }

                    Sqlcmd.Parameters.Add("@imprime", SqlDbType.Bit);
                    if (FacturaP.imprime == false)
                    {
                        Sqlcmd.Parameters["@imprime"].Value = 0;
                    }
                    else
                    {
                        Sqlcmd.Parameters["@imprime"].Value = 1;
                    }

                    Sqlcmd.Parameters.Add("@detalle", SqlDbType.Bit);

                    if (FacturaP.detalle == false)
                    {
                        Sqlcmd.Parameters["@detalle"].Value = 0;
                    }
                    else
                    {
                        Sqlcmd.Parameters["@detalle"].Value = 1;
                    }



                    Sqldap = new SqlDataAdapter();
                    Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                    Sqldap.SelectCommand = Sqlcmd;

                    Sqldap.Fill(Dts);
                }

                Resultado = 1;

                foreach (var Estado in EstadoCuenta)
                {

                    Sqlcmd = new SqlCommand("sp_CreaEstadoCuenta", Sqlcon);
                    Sqlcmd.CommandType = CommandType.StoredProcedure;

                    Sqlcmd.Transaction = transaction;

                    Sqlcmd.Parameters.Add("@id", SqlDbType.Int);
                    Sqlcmd.Parameters["@id"].Value = Estado.id;

                    Sqlcmd.Parameters.Add("@numfac", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@numfac"].Value = Estado.numfac;

                    Sqlcmd.Parameters.Add("@tipodoc", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@tipodoc"].Value = Estado.tipodoc;

                    Sqlcmd.Parameters.Add("@iddoc", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@iddoc"].Value = Estado.iddoc;

                    Sqlcmd.Parameters.Add("@numdoc", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@numdoc"].Value = Estado.numdoc;

                    Sqlcmd.Parameters.Add("@codcli", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@codcli"].Value = CodigoCliente;

                    Sqlcmd.Parameters.Add("@fecha", SqlDbType.Date);
                    Sqlcmd.Parameters["@fecha"].Value = Convert.ToDateTime(Estado.fecha);

                    Sqlcmd.Parameters.Add("@obs", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@obs"].Value = Estado.obs;

                    Sqlcmd.Parameters.Add("@debe", SqlDbType.Float);
                    Sqlcmd.Parameters["@debe"].Value = Estado.debe;

                    Sqlcmd.Parameters.Add("@haber", SqlDbType.Float);
                    Sqlcmd.Parameters["@haber"].Value = Estado.haber;

                    Sqlcmd.Parameters.Add("@saldo", SqlDbType.Float);
                    Sqlcmd.Parameters["@saldo"].Value = Estado.saldo;

                    Sqlcmd.Parameters.Add("@fecha1", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@fecha1"].Value = Estado.fecha1;

                    Sqlcmd.Parameters.Add("@forpag", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@forpag"].Value = Estado.forpag;

                    Sqlcmd.Parameters.Add("@claspag", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@claspag"].Value = Estado.claspag;

                    Sqlcmd.Parameters.Add("@caja", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@caja"].Value = Estado.caja;

                    Sqldap = new SqlDataAdapter();
                    Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                    Sqldap.SelectCommand = Sqlcmd;

                    Sqldap.Fill(Dts);

                }

                foreach (var CuentaCobrar in CxC)
                {

                    Sqlcmd = new SqlCommand("sp_CreaCxC", Sqlcon);
                    Sqlcmd.CommandType = CommandType.StoredProcedure;

                    Sqlcmd.Transaction = transaction;

                    Sqlcmd.Parameters.Add("@codcli", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@codcli"].Value = CodigoCliente;

                    Sqlcmd.Parameters.Add("@numdoc", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@numdoc"].Value = CuentaCobrar.numdoc;

                    Sqlcmd.Parameters.Add("@fecha", SqlDbType.DateTime);
                    Sqlcmd.Parameters["@fecha"].Value = CuentaCobrar.fecha;

                    Sqlcmd.Parameters.Add("@tipo", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@tipo"].Value = CuentaCobrar.tipo;

                    Sqlcmd.Parameters.Add("@debe", SqlDbType.Decimal);
                    Sqlcmd.Parameters["@debe"].Value = CuentaCobrar.debe;

                    Sqlcmd.Parameters.Add("@haber", SqlDbType.Decimal);
                    Sqlcmd.Parameters["@haber"].Value = CuentaCobrar.haber;

                    Sqlcmd.Parameters.Add("@saldo", SqlDbType.Decimal);
                    Sqlcmd.Parameters["@saldo"].Value = CuentaCobrar.saldo;

                    Sqlcmd.Parameters.Add("@fecha1", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@fecha1"].Value = CuentaCobrar.fecha1;

                    Sqlcmd.Parameters.Add("@fechapago", SqlDbType.DateTime);
                    Sqlcmd.Parameters["@fechapago"].Value = CuentaCobrar.fechapago;

                    Sqlcmd.Parameters.Add("@tipest", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@tipest"].Value = CuentaCobrar.tipest;

                    Sqlcmd.Parameters.Add("@fecven", SqlDbType.DateTime);
                    Sqlcmd.Parameters["@fecven"].Value = CuentaCobrar.fecven;

                    Sqlcmd.Parameters.Add("@forpag", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@forpag"].Value = CuentaCobrar.forpag;

                    Sqlcmd.Parameters.Add("@claspag", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@claspag"].Value = CuentaCobrar.claspag;

                    Sqlcmd.Parameters.Add("@fecven1", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@fecven1"].Value = CuentaCobrar.fecven1;

                    Sqlcmd.Parameters.Add("@fila", SqlDbType.Float);
                    Sqlcmd.Parameters["@fila"].Value = CuentaCobrar.fila;

                    Sqlcmd.Parameters.Add("@Marca", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@Marca"].Value = CuentaCobrar.Marca;

                    Sqldap = new SqlDataAdapter();
                    Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                    Sqldap.SelectCommand = Sqlcmd;

                    Sqldap.Fill(Dts);

                }

                Sqlcmd = new SqlCommand("sp_ActualizaEstadoCuenta", Sqlcon);

                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.Int);
                Sqlcmd.Parameters["@CodigoAtencion"].Value = (ateCodigo);

                Sqlcmd.Parameters.Add("@Estado", SqlDbType.Int);
                Sqlcmd.Parameters["@Estado"].Value = (0);

                Sqlcmd.Parameters.Add("@Factura", SqlDbType.VarChar);
                Sqlcmd.Parameters["@Factura"].Value = (Factura.numfac);

                Sqlcmd.Parameters.Add("@usuario", SqlDbType.Int);
                Sqlcmd.Parameters["@usuario"].Value = (Factura.cajero);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;

                Sqldap.Fill(Dts);

                Sqlcmd = new SqlCommand("sp_GuardaDatosAdicionales", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@Numdoc", SqlDbType.NChar);
                Sqlcmd.Parameters["@Numdoc"].Value = (Factura.numfac);

                Sqlcmd.Parameters.Add("@RucCedula", SqlDbType.VarChar);
                Sqlcmd.Parameters["@RucCedula"].Value = (RucPaciente);

                Sqlcmd.Parameters.Add("@Nombres", SqlDbType.VarChar);
                Sqlcmd.Parameters["@Nombres"].Value = (nombrePaciente);

                Sqlcmd.Parameters.Add("@Direccion", SqlDbType.VarChar);
                Sqlcmd.Parameters["@Direccion"].Value = (Direc_Cliente);

                Sqlcmd.Parameters.Add("@Telefono", SqlDbType.VarChar);
                Sqlcmd.Parameters["@Telefono"].Value = (txt_Telef_P);

                Sqlcmd.Parameters.Add("@HistoriaClinica", SqlDbType.VarChar);
                Sqlcmd.Parameters["@HistoriaClinica"].Value = (HistoriaClinica);

                Sqlcmd.Parameters.Add("@FechaIngreso", SqlDbType.DateTime);
                Sqlcmd.Parameters["@FechaIngreso"].Value = (ATE_FECHA_INGRESO);

                Sqlcmd.Parameters.Add("@FechaAlta", SqlDbType.DateTime);
                Sqlcmd.Parameters["@FechaAlta"].Value = (FechaAlta);

                Sqlcmd.Parameters.Add("@MedicoTratante", SqlDbType.VarChar);
                Sqlcmd.Parameters["@MedicoTratante"].Value = (Medico);

                Sqlcmd.Parameters.Add("@Ate_codigo", SqlDbType.BigInt);
                Sqlcmd.Parameters["@Ate_codigo"].Value = (ateCodigo);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;

                Sqldap.Fill(Dts);

                Sqlcmd = new SqlCommand("sp_GuardaCambiosCuentasAgrupadas", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@Ate_codigo", SqlDbType.BigInt);
                Sqlcmd.Parameters["@Ate_codigo"].Value = (ateCodigo);

                Sqlcmd.Parameters.Add("@Factura", SqlDbType.VarChar);
                Sqlcmd.Parameters["@Factura"].Value = (Factura.numfac);

                Sqlcmd.Parameters.Add("@usuario", SqlDbType.Int);
                Sqlcmd.Parameters["@usuario"].Value = (Factura.cajero);

                Sqlcmd.Parameters.Add("@total", SqlDbType.Float);
                Sqlcmd.Parameters["@total"].Value = total;

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;

                Sqldap.Fill(Dts);

                transaction.Commit();

                if (Prefactura == 1)
                {
                    DataTable Dts2 = new DataTable();

                    Sqlcmd = new SqlCommand("sp_ValidaParametroSIC", Sqlcon);
                    Sqlcmd.CommandType = CommandType.StoredProcedure;
                    Sqldap = new SqlDataAdapter();
                    Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                    Sqldap.SelectCommand = Sqlcmd;
                    Sqldap.Fill(Dts2);

                    try
                    {
                        if (Dts2.Rows[0][0].ToString() == "True")
                        {
                            GeneraAsientoContable(Factura);
                        }
                        Sqlcon.Close();
                    }
                    catch
                    { }
                }
                return 1; // los datos se han almacenado correctamente.
            }

            catch (Exception ex)
            {
                transaction.Rollback();
                return 0; // los datos no se almacenaron
            }
            //return Dts.Tables["tabla"];
        }

        private void GeneraAsientoContable(DtoFacturaSic3000 Factura)
        {
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


            //transaction = Sqlcon.BeginTransaction();
            USUARIOS usu = new USUARIOS();

            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                usu = (from a in contexto.USUARIOS
                       where a.ID_USUARIO == His.Entidades.Clases.Sesion.codUsuario
                       select a).FirstOrDefault();
            }



            Sqlcmd = new SqlCommand("AsientodeVentaContabilidadVT", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            //Sqlcmd.Transaction = transaction;
            string prueba = Factura.numnot.Substring(0, 3);
            Sqlcmd.Parameters.AddWithValue("@numeroFactura", Factura.numnot);
            Sqlcmd.Parameters.AddWithValue("@caja", Factura.numnot.Substring(0, 3));
            Sqlcmd.Parameters.AddWithValue("@cajero", usu.Codigo_Rol);
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            //transaction.Commit();

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #region Detalle Factura
        public int recuperaMaximoDetalleFactura()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var id = contexto.FACTURA_DETALLE.Select(f => f.COD_FDETALLE).Count();
                    if (id > 0)
                        return contexto.FACTURA_DETALLE.Select(f => f.COD_FDETALLE).Max();
                    else
                        return 0;
                }
            }
            catch (Exception err) { throw err; }
        }

        public void crearFacturaDetalle(FACTURA_DETALLE facturaDetalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToFACTURA_DETALLE(facturaDetalle);
                contexto.SaveChanges();
            }
        }

        #endregion

        #region Factura Forma de Pago
        public int recuperaMaximoFacturaPago()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var id = contexto.FACTURA_FORMA_PAGO.Select(f => f.COD_FAC_PAGO).Count();
                    if (id > 0)
                        return contexto.FACTURA_FORMA_PAGO.Select(f => f.COD_FAC_PAGO).Max();
                    else
                        return 0;
                }
            }
            catch (Exception err) { throw err; }

        }

        public void crearFacturaPago(FACTURA_FORMA_PAGO facturaPago)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToFACTURA_FORMA_PAGO(facturaPago);
                contexto.SaveChanges();
            }
        }
        #endregion

        #region SecuencialesFactura
        public DataTable Datosfactura(String NumeroFactura, String Caja, Int16 TipoDocumento)
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

            Sqlcmd = new SqlCommand("sp_VerificaNumeroFactura", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@NumeroFactura", SqlDbType.VarChar);
            Sqlcmd.Parameters["@NumeroFactura"].Value = (NumeroFactura);

            Sqlcmd.Parameters.Add("@Caja", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Caja"].Value = (Caja);

            Sqlcmd.Parameters.Add("@TipoDocumento", SqlDbType.Int);
            Sqlcmd.Parameters["@TipoDocumento"].Value = (TipoDocumento);

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


        //alex2020 --- devuelve datos totales de factura 
        public DataTable LXtotales(Int64 atecodigo)
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

            Sqlcmd = new SqlCommand("select top 1 FAC_SUBTOTAL as subtotal,FAC_IVAUNO as coniva, FAC_IVADOS as siniva, FAC_DESCUENTO as descuento,FAC_IVATRES as iva ,FAC_TOTAL as total from FACTURA where ATE_CODIGO=" + atecodigo, Sqlcon);

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
        ///alex2020 devuelve 1 si esta facturada, 0 sino
        public DataTable LXfacturada(Int64 atecodigo)
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

            Sqlcmd = new SqlCommand("select distinct COUNT(ATE_CODIGO) as existe from FACTURA where ATE_CODIGO= " + atecodigo, Sqlcon);

            Sqlcmd.CommandType = CommandType.Text;


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/
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


        //CAMBIOS DE lx
        public DataTable ObservacioAtencion(Int32 atecodigo)
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

            Sqlcmd = new SqlCommand(" declare @xExiste as int  "
                + " SELECT @xExiste = COUNT(dbo.ATENCIONES_OBSERVACION.ATE_CODIGO)"
                + " FROM dbo.ATENCIONES LEFT OUTER JOIN"
                + " dbo.ATENCIONES_OBSERVACION ON dbo.ATENCIONES.ATE_CODIGO = dbo.ATENCIONES_OBSERVACION.ATE_CODIGO"
                + " where dbo.ATENCIONES_OBSERVACION.ATE_CODIGO = " + atecodigo
                + " if (@xExiste = 0)"
                + " begin"
                + " insert into dbo.ATENCIONES_OBSERVACION(dbo.ATENCIONES_OBSERVACION.ATE_CODIGO, dbo.ATENCIONES_OBSERVACION.OBSERVACION)"
                + "  VALUES(" + atecodigo + ", '.')"
                + " end"
                + " SELECT dbo.ATENCIONES_OBSERVACION.OBSERVACION, dbo.ATENCIONES_OBSERVACION.ATE_CODIGO_ORG"
                + " FROM dbo.ATENCIONES LEFT OUTER JOIN"
                + " dbo.ATENCIONES_OBSERVACION ON dbo.ATENCIONES.ATE_CODIGO = dbo.ATENCIONES_OBSERVACION.ATE_CODIGO"
                + " where dbo.ATENCIONES_OBSERVACION.ATE_CODIGO = " + atecodigo, Sqlcon);

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


        public int GuardaAuditoriaCuenta(Int32 cue_codigo, string observacion, int auditada, decimal cantidad)
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

            Sqlcmd = new SqlCommand(" declare @xExiste as int "
                + " SELECT @xExiste = COUNT(CUE_CODIGO) FROM CUENTAS_PACIENTES_AUDITORIA WHERE(CUE_CODIGO = " + cue_codigo + ") "
                + " if (@xExiste = 0) "
                + " begin"
                    + " insert into CUENTAS_PACIENTES_AUDITORIA (CUE_CODIGO, OBSERVACION, AUDITADA, CANTIDAD)"
                    + " VALUES(" + cue_codigo + ", '" + observacion + "', " + auditada + ", " + cantidad + ")"
                + " end"
                + " else"
                + "  begin"
                 + "    UPDATE CUENTAS_PACIENTES_AUDITORIA"
                  + "   SET OBSERVACION = '" + observacion + "', AUDITADA = " + auditada + ", CANTIDAD = " + cantidad + ""
                  + "   WHERE CUE_CODIGO = " + cue_codigo + ""
               + "  end", Sqlcon);

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
            return 0;
        }

        public int GuardaObservacionAtencion(Int32 cue_codigo, string observacion)
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

            Sqlcmd = new SqlCommand("  UPDATE dbo.ATENCIONES_OBSERVACION "
                  + "   SET dbo.ATENCIONES_OBSERVACION.OBSERVACION = '" + observacion + "'"
                  + "   WHERE  dbo.ATENCIONES_OBSERVACION.ATE_CODIGO = " + cue_codigo, Sqlcon);

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
            return 0;
        }


        public bool ProductoConSinIVA(Int32 pro_codigo)
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

            Sqlcmd = new SqlCommand(" select paga_iva from Sic3000..Producto where codpro= " + pro_codigo, Sqlcon);

            Sqlcmd.CommandType = CommandType.Text;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);
            bool pagaIva = false;
            pagaIva = Convert.ToBoolean(Dts.Rows[0]["paga_iva"].ToString());
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return pagaIva;
        }


        //GUARDA LOS DESCUENTOS EN LA TABLA DEL SIC3000 DETALLES PABLO ROCHA 08-05-2018

        public int GuardaDescuentoenDetalles(string codRubro, double Descuento, string Factura)
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

            Sqlcmd = new SqlCommand("sp_GuardaDescuentos", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@codRubro", SqlDbType.VarChar);
            Sqlcmd.Parameters["@codRubro"].Value = (codRubro);

            Sqlcmd.Parameters.Add("@Descuento", SqlDbType.Float);
            Sqlcmd.Parameters["@Descuento"].Value = (Descuento);

            Sqlcmd.Parameters.Add("@Factura", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Factura"].Value = (Factura);

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


        public int GuardaDescuentoProductos(Int32 PcodRubro, string PCodpro, double Pdescuento, double Pporcentaje, Int64 PCueCodigo, Int32 Patencion)
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

            Sqlcmd = new SqlCommand("sp_GuardaDescuentosProductos", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@codRubro", SqlDbType.SmallInt);
            Sqlcmd.Parameters["@codRubro"].Value = (PcodRubro);

            Sqlcmd.Parameters.Add("@codProducto", SqlDbType.VarChar);
            Sqlcmd.Parameters["@codProducto"].Value = (PCodpro);

            Sqlcmd.Parameters.Add("@Descuento", SqlDbType.Float);
            Sqlcmd.Parameters["@Descuento"].Value = (Pdescuento);

            Sqlcmd.Parameters.Add("@Porcentaje", SqlDbType.Float);
            Sqlcmd.Parameters["@Porcentaje"].Value = (Pporcentaje);

            Sqlcmd.Parameters.Add("@Atencion", SqlDbType.Int);
            Sqlcmd.Parameters["@Atencion"].Value = (Patencion);

            Sqlcmd.Parameters.Add("@CueCodigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@CueCodigo"].Value = (PCueCodigo);


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

        public int GuardaDatosAdicionales(string NFactura, string Ruc, string Nombres, string Direccion, string Telefono, Int64 HistoriaClinica, DateTime FechaIngreso, DateTime FechaAlta, string MedicoTratante, Int64 ATE_CODIGO)
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

            Sqlcmd = new SqlCommand("sp_GuardaDatosAdicionales", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Numdoc", SqlDbType.NChar);
            Sqlcmd.Parameters["@Numdoc"].Value = (NFactura);

            Sqlcmd.Parameters.Add("@RucCedula", SqlDbType.VarChar);
            Sqlcmd.Parameters["@RucCedula"].Value = (Ruc);

            Sqlcmd.Parameters.Add("@Nombres", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Nombres"].Value = (Nombres);

            Sqlcmd.Parameters.Add("@Direccion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Direccion"].Value = (Direccion);

            Sqlcmd.Parameters.Add("@Telefono", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Telefono"].Value = (Telefono);

            Sqlcmd.Parameters.Add("@HistoriaClinica", SqlDbType.VarChar);
            Sqlcmd.Parameters["@HistoriaClinica"].Value = (HistoriaClinica);

            Sqlcmd.Parameters.Add("@FechaIngreso", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaIngreso"].Value = (FechaIngreso);

            Sqlcmd.Parameters.Add("@FechaAlta", SqlDbType.DateTime);
            Sqlcmd.Parameters["@FechaAlta"].Value = (FechaAlta);

            Sqlcmd.Parameters.Add("@MedicoTratante", SqlDbType.VarChar);
            Sqlcmd.Parameters["@MedicoTratante"].Value = (MedicoTratante);

            Sqlcmd.Parameters.Add("@Ate_codigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@Ate_codigo"].Value = (ATE_CODIGO);

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

        public DataTable RecuperaCodigoGrupoSic3000(String Descripcion)
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

            Sqlcmd = new SqlCommand("sp_codigoProducto", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoArea", SqlDbType.VarChar);
            Sqlcmd.Parameters["@CodigoArea"].Value = (Descripcion);


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



        public DataTable RecuperaInformacionCaja(String NumeroCaja)
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

            Sqlcmd = new SqlCommand("sp_RecuperaInformacionCaja", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_NumeroCaja", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_NumeroCaja"].Value = (NumeroCaja);


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




        public DataTable RecuperaInformacionUsuario(Int32 CodigoUsuario)
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

            Sqlcmd = new SqlCommand("sp_DatosUsuario", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_CodigoUsuario", SqlDbType.Int);
            Sqlcmd.Parameters["@p_CodigoUsuario"].Value = (CodigoUsuario);

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


        //RECUPERA INFORMACION DE EMPRESA PABLO ROCHA 04/09/2014
        public DataTable RecuperaEmpresa()
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

            Sqlcmd = new SqlCommand("sp_RecuperaEmpresa", Sqlcon);

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

            return Dts.Tables["tabla"];

        }

        //RECUPERA LAS FECHAS DE INGRESO ALTA Y ATENCION
        public DataTable FechaPaciente(Int64 ATE_CODIGO)
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

            Sqlcmd = new SqlCommand("sp_FechasPaciente", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@p_ATE_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@p_ATE_CODIGO"].Value = (ATE_CODIGO);

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

        //RECUPERA RUBROS ORDEN DE DESCUENTOS PABLO ROCHA 10/09/2014
        public DataTable RecuperaRubrosDescuento()
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

            Sqlcmd = new SqlCommand("sp_RecuperaRubrosDescuento", Sqlcon);

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

            return Dts.Tables["tabla"];

        }

        //RECUPERA PRIORIDAD DESCUENTO PABLO ROCHA 07/05/2018
        public DataTable RecuperaPrioridadDescuento()
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

            Sqlcmd = new SqlCommand("sp_RecuperaPrioridadDescuento", Sqlcon);

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

            return Dts.Tables["tabla"];

        }

        //RECUPERA RUBROS CON IVA PABLO ROCHA 10/09/2014
        public DataTable RecuperaRubrosIva(string NumFacturaActual)
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

            Sqlcmd = new SqlCommand("sp_RecuperaRubrosIva", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@ATE_FACTURA", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_FACTURA"].Value = (NumFacturaActual);

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

        public DataTable RecuperaRubrosConIva(string NumRubro)
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

            Sqlcmd = new SqlCommand("sp_RecuperaRubrosConIva", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@ATE_FACTURA", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_FACTURA"].Value = (NumRubro);

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

        //RECUPERA TIPO DE INDENTIFICACION DE CLIENTE Y EMAIL PABLO ROCHA 10/09/2014
        public DataTable RecuperatipoIdentificacionEmail(string NumFacturaActual)
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

            Sqlcmd = new SqlCommand("sp_RecuperaTipoIdentificacionEmail", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@ATE_FACTURA", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_FACTURA"].Value = (NumFacturaActual);

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
        //Solo recupera el mail del medico
        public DataTable RecuperaEmailMed(string NumFacturaActual)
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

            Sqlcmd = new SqlCommand("sp_Recuperaemail-Medico", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@PAC_CEDULA", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PAC_CEDULA"].Value = (NumFacturaActual);

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

        //RECUPERA TABLA17SRI PABLO ROCHA 08-02-2018
        public DataTable Tabla17SRI()
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

            Sqlcmd = new SqlCommand("sp_Tabla17SRI", Sqlcon);//sirve para elegir el porcentage de iva vigente

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

            return Dts.Tables["tabla"];

        }


        //RECUPERA PARAMETROS FACTURACION PABLO ROCHA 22/09/2014
        public DataTable RecuperaParametros()
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

            Sqlcmd = new SqlCommand("sp_RecuperaParametros", Sqlcon);

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

            return Dts.Tables["tabla"];

        }

        // Impresion desglose de la factura / Giovany Tapia / 04/01/2013
        public DataTable DatosFacturaDesglosexFecha(Int64 CodigoAtencion, DateTime f_inicio, DateTime f_fin)
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

            Sqlcmd = new SqlCommand("sp_DatosCuentaFacturaxFecha", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_CodigoAtencion"].Value = (CodigoAtencion);

            Sqlcmd.Parameters.Add("@f_inicio", SqlDbType.Date);
            Sqlcmd.Parameters["@f_inicio"].Value = (f_inicio);

            Sqlcmd.Parameters.Add("@f_fin", SqlDbType.Date);
            Sqlcmd.Parameters["@f_fin"].Value = (f_fin);


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

        public DataTable DatosFacturaDesglose(Int64 CodigoAtencion)
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

            Sqlcmd = new SqlCommand("sp_DatosCuentaFactura", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_CodigoAtencion"].Value = (CodigoAtencion);


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
        public DataTable DatosAgrupaFacturaDesglose(int CodigoAtencion)
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

            Sqlcmd = new SqlCommand("[sp_DatosCuentaAgrupaFactura]", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_CodigoAtencion"].Value = (CodigoAtencion);


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
        public DataTable DetalleCuentaAgrupada(Int64 CodigoAtencion)
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

            Sqlcmd = new SqlCommand("select * from AGRUPACION_CUENTAS AG INNER JOIN ATENCIONES A ON AG.ate_codigo_madre = A.ATE_CODIGO where ate_codigo_madre = " + CodigoAtencion, Sqlcon);

            Sqlcmd.CommandType = CommandType.Text;


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/
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
        public string RecuperaInformacionIVA(string IVA)
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

            Sqlcmd = new SqlCommand("sp_ValidaIVA", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@codpro", SqlDbType.VarChar);
            Sqlcmd.Parameters["@codpro"].Value = (IVA);


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);
            string medi;
            medi = Dts.Rows[0]["IVA"].ToString();
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return medi;

        }


        public string RecuperaPorcentajeIVA(string Atencion)
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

            Sqlcmd = new SqlCommand("sp_DevuelvePorcentajeIVA", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Atenciones", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Atenciones"].Value = (Atencion);


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);
            string medi;
            medi = Dts.Rows[0]["PorcentajeIva"].ToString();

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return medi;

        }

        public string Medico(int CodMedico)
        {

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
            BaseContextoDatos obj = new BaseContextoDatos();
            string medi;
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("sp_NombreMedico", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Medico", SqlDbType.Int);
            Sqlcmd.Parameters["@Medico"].Value = (CodMedico);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);
            try
            {
                medi = Dts.Rows[0]["MEDICO"].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "MEDICO NO REGISTRA HONORARIOS", "INFORMACION");
                medi = "";
            }

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return medi;

        }


        public DataTable GeneraCierreTurno(int CodigoCajero, DateTime Fecha)
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

            Sqlcmd = new SqlCommand("sp_GeneraCierreTurno", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoCajero", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoCajero"].Value = (CodigoCajero);

            Sqlcmd.Parameters.Add("@Fecha", SqlDbType.DateTime);
            Sqlcmd.Parameters["@Fecha"].Value = (Fecha);

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

        public DataTable VerificaCierreTurno(int CodigoCajero, DateTime Fecha)
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

            Sqlcmd = new SqlCommand("sp_VerificaCierreTurno", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoCajero", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoCajero"].Value = (CodigoCajero);

            Sqlcmd.Parameters.Add("@Fecha", SqlDbType.DateTime);
            Sqlcmd.Parameters["@Fecha"].Value = (Fecha);

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


        public DataTable DatosFacturaTotalPago(string NumeroFactura)
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

            Sqlcmd = new SqlCommand("sp_ValoresFacturaPago", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_Factura", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_Factura"].Value = (NumeroFactura);


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

        public DataTable DatosFacturaTotal(Int64 CodigoAtencion, string NumeroFactura)
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

            Sqlcmd = new SqlCommand("sp_ValoresFactura", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_CodigoAtencion"].Value = (CodigoAtencion);

            Sqlcmd.Parameters.Add("@p_Factura", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_Factura"].Value = (NumeroFactura);


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

        public DataTable DatosFacturaTotalxFecha(Int64 CodigoAtencion, string NumeroFactura, DateTime f_Inicio, DateTime F_Fin)
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

            Sqlcmd = new SqlCommand("sp_ValoresFacturaEstadoFecha", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_CodigoAtencion"].Value = (CodigoAtencion);

            Sqlcmd.Parameters.Add("@p_Factura", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_Factura"].Value = (NumeroFactura);

            Sqlcmd.Parameters.Add("@f_fechaInicio", SqlDbType.Date);
            Sqlcmd.Parameters["@f_fechaInicio"].Value = (f_Inicio);

            Sqlcmd.Parameters.Add("@f_fechaFin", SqlDbType.Date);
            Sqlcmd.Parameters["@f_fechaFin"].Value = (F_Fin);


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

        public DataTable RecuperaTemporales(Int64 CodigoAtencion)
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

            Sqlcmd = new SqlCommand("sp_sp_RecuperaTemporales", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_CodigoAtencion"].Value = (CodigoAtencion);

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

        public DataTable BorraTemporales(Int64 CodigoAtencion)
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

            Sqlcmd = new SqlCommand("spBorraTemporales", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_CodigoAtencion"].Value = (CodigoAtencion);

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

        public DataTable DatosConsolidado(string CadenaAtenciones)
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

            Sqlcmd = new SqlCommand("sp_Forma_datos", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Atenciones", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Atenciones"].Value = (CadenaAtenciones);


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

        public int DatosConsolidado2(string CadenaAtenciones)
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

            Sqlcmd = new SqlCommand("sp_BorraIVA", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@Atenciones", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Atenciones"].Value = (CadenaAtenciones);

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

        public DataTable GeneraHonorariosMedicos(Int32 CodigoMedico, DateTime Fecha1, DateTime Fecha2, Int64 NumeroTramite)
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

            Sqlcmd = new SqlCommand("sp_HonorariosMedicos", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoMedico", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoMedico"].Value = (CodigoMedico);

            Sqlcmd.Parameters.Add("@Fecha1", SqlDbType.DateTime);
            Sqlcmd.Parameters["@Fecha1"].Value = (Fecha1);

            Sqlcmd.Parameters.Add("@Fecha2", SqlDbType.DateTime);
            Sqlcmd.Parameters["@Fecha2"].Value = (Fecha2);

            Sqlcmd.Parameters.Add("@NumeroTramite", SqlDbType.BigInt);
            Sqlcmd.Parameters["@NumeroTramite"].Value = (NumeroTramite);

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

        #endregion


        public Int32 CreaFacturaHis3000(DtoFactura Factura)
        {

            // GIOVANNY TAPIA / 07/08/2012

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

                Sqlcmd = new SqlCommand("sp_GuardaEncabezadoFacturaHis3000", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@FAC_CODIGO", SqlDbType.Int);
                Sqlcmd.Parameters["@FAC_CODIGO"].Value = Factura.FAC_CODIGO;

                Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.Int);
                Sqlcmd.Parameters["@ATE_CODIGO"].Value = Factura.ATE_CODIGO;

                Sqlcmd.Parameters.Add("@FAC_NUMERO", SqlDbType.VarChar);
                Sqlcmd.Parameters["@FAC_NUMERO"].Value = Factura.FAC_NUMERO;

                Sqlcmd.Parameters.Add("@FAC_AUTORIZACION", SqlDbType.Int);
                Sqlcmd.Parameters["@FAC_AUTORIZACION"].Value = Factura.FAC_AUTORIZACION;

                Sqlcmd.Parameters.Add("@FAC_FECHA", SqlDbType.DateTime);
                Sqlcmd.Parameters["@FAC_FECHA"].Value = Factura.FAC_FECHA;

                Sqlcmd.Parameters.Add("@CLI_NOMBRE", SqlDbType.VarChar);
                Sqlcmd.Parameters["@CLI_NOMBRE"].Value = Factura.CLI_NOMBRE;

                Sqlcmd.Parameters.Add("@CLI_RUC", SqlDbType.VarChar);
                Sqlcmd.Parameters["@CLI_RUC"].Value = Factura.CLI_RUC;

                Sqlcmd.Parameters.Add("@CLI_TELEFONO", SqlDbType.VarChar);
                Sqlcmd.Parameters["@CLI_TELEFONO"].Value = Factura.CLI_TELEFONO;

                Sqlcmd.Parameters.Add("@FAC_TOTAL", SqlDbType.Decimal);
                Sqlcmd.Parameters["@FAC_TOTAL"].Value = Factura.FAC_TOTAL;

                Sqlcmd.Parameters.Add("@FAC_SUBTOTAL", SqlDbType.Decimal);
                Sqlcmd.Parameters["@FAC_SUBTOTAL"].Value = Factura.FAC_SUBTOTAL;

                Sqlcmd.Parameters.Add("@FAC_IVAUNO", SqlDbType.Decimal);
                Sqlcmd.Parameters["@FAC_IVAUNO"].Value = Factura.FAC_IVAUNO;

                Sqlcmd.Parameters.Add("@FAC_IVADOS", SqlDbType.Decimal);
                Sqlcmd.Parameters["@FAC_IVADOS"].Value = Factura.FAC_IVADOS;

                Sqlcmd.Parameters.Add("@FAC_IVATRES", SqlDbType.Decimal);
                Sqlcmd.Parameters["@FAC_IVATRES"].Value = Factura.FAC_IVATRES;

                Sqlcmd.Parameters.Add("@FAC_ESTADO", SqlDbType.VarChar);
                Sqlcmd.Parameters["@FAC_ESTADO"].Value = Factura.FAC_ESTADO;

                Sqlcmd.Parameters.Add("@FAC_CAJA", SqlDbType.VarChar);
                Sqlcmd.Parameters["@FAC_CAJA"].Value = Factura.FAC_CAJA;

                Sqlcmd.Parameters.Add("@FAC_VENDEDOR", SqlDbType.VarChar);
                Sqlcmd.Parameters["@FAC_VENDEDOR"].Value = Factura.FAC_VENDEDOR;

                Sqlcmd.Parameters.Add("@FAC_LOCAL", SqlDbType.VarChar);
                Sqlcmd.Parameters["@FAC_LOCAL"].Value = Factura.FAC_LOCAL;

                Sqlcmd.Parameters.Add("@FAC_ARQUEO", SqlDbType.TinyInt);
                Sqlcmd.Parameters["@FAC_ARQUEO"].Value = Factura.FAC_ARQUEO;

                Sqlcmd.Parameters.Add("@FAC_DESCUENTO", SqlDbType.Decimal);
                Sqlcmd.Parameters["@FAC_DESCUENTO"].Value = Factura.FAC_DESCUENTO;

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;

                Sqldap.Fill(Dts);

                Resultado = Convert.ToInt32(Dts.Rows[0][0]);

                foreach (var Detalle in Factura.DetalleFactura)
                {

                    Sqlcmd = new SqlCommand("sp_GuardaDetalleFacturaHis3000", Sqlcon);
                    Sqlcmd.CommandType = CommandType.StoredProcedure;

                    Sqlcmd.Transaction = transaction;

                    Sqlcmd.Parameters.Add("@COD_FDETALLE", SqlDbType.Int);
                    Sqlcmd.Parameters["@COD_FDETALLE"].Value = Resultado;

                    Sqlcmd.Parameters.Add("@FAC_CODIGO", SqlDbType.Int);
                    Sqlcmd.Parameters["@FAC_CODIGO"].Value = Detalle.FAC_CODIGO;

                    Sqlcmd.Parameters.Add("@DET_DESCIPCION", SqlDbType.VarChar);
                    Sqlcmd.Parameters["@DET_DESCIPCION"].Value = Detalle.DET_DESCIPCION;

                    Sqlcmd.Parameters.Add("@DET_VALOR", SqlDbType.Decimal);
                    Sqlcmd.Parameters["@DET_VALOR"].Value = Detalle.DET_VALOR;

                    Sqlcmd.Parameters.Add("@DET_ESTADO", SqlDbType.TinyInt);
                    Sqlcmd.Parameters["@DET_ESTADO"].Value = Detalle.DET_ESTADO;

                    Sqldap = new SqlDataAdapter();
                    Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                    Sqldap.SelectCommand = Sqlcmd;

                    Sqldap.Fill(Dts);

                }

                transaction.Commit();

                try
                {
                    Sqlcon.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return Resultado;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return 0;
            }

        }


        #region FormaPago

        public DataTable FormaPagoSic(Boolean TipoBusqueda, int Filtro)
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

            Sqlcmd = new SqlCommand("sp_FormasPagoSic", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_opcion", SqlDbType.Bit);

            if (TipoBusqueda == true)
            {
                Sqlcmd.Parameters["@p_opcion"].Value = 1;
            }
            else
            {
                Sqlcmd.Parameters["@p_opcion"].Value = 0;
            }

            Sqlcmd.Parameters.Add("@p_CodigoFormaPago", SqlDbType.Int);
            Sqlcmd.Parameters["@p_CodigoFormaPago"].Value = Filtro;


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

        public DataTable AnticiposSic_sp(string Ate_Codigo)
        {
            //PABLO RO7CHA 22/07/2014 EXTRAE LOS ANTICIPOS
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

            Sqlcmd = new SqlCommand("sp_AnticiposSic", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Ate_Codigo"].Value = Ate_Codigo;

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

        public DataTable PlazoPagoSic(int codigo)
        {

            //Pablo Rocha 02-05-2014 extrae plazo pago desde el sic
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

            Sqlcmd = new SqlCommand("sp_PlazoPago", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@codigo"].Value = codigo;

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

        public int PlazoPagoSicVerifica(string VerificaPlazoPago)
        {
            //Pablo Rocha 02-05-2014 verifica si existe el plazo pago desde el sic
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

            Sqlcmd = new SqlCommand("sp_VerificaPlazoPago", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@VerificaPlazoPago", SqlDbType.VarChar);
            Sqlcmd.Parameters["@VerificaPlazoPago"].Value = VerificaPlazoPago;

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

            if (Dts.Tables["tabla"].Rows.Count > 0)
                return 1;
            else
                return 0;

        }

        public DataTable RecuperaCodigoClienteSic(string Filtro)
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

            Sqlcmd = new SqlCommand("sp_RecuperaCodigoClienteSic", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_CodigoCliente", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_CodigoCliente"].Value = Filtro;


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

        //GUARDA TIPO DE INDENTIFICADOR Y EMAIL PABLO ROCHA 10-05-2018
        public int GuardaIdentificadorEmail(string codIdentificador, string factura, string email)
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

            Sqlcmd = new SqlCommand("sp_GuardaIdentificadorEmail", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@cod_Identificador", SqlDbType.VarChar);
            Sqlcmd.Parameters["@cod_Identificador"].Value = (codIdentificador);

            Sqlcmd.Parameters.Add("@Factura", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Factura"].Value = (factura);

            Sqlcmd.Parameters.Add("@email_factura", SqlDbType.VarChar);
            Sqlcmd.Parameters["@email_factura"].Value = (email);

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

            return 1;
        }

        public int GeneraCuentasSic(List<DtoFacturaPago> FacturaPago, List<DtoEstadoCuenta> EstadoCuenta, List<DtoCXC> CxC)
        {
            // GIOVANNY TAPIA / 07/08/2012

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
                foreach (var Factura in FacturaPago)
                {
                    Sqlcmd = new SqlCommand("sp_CreaFacturaPago", Sqlcon);
                    Sqlcmd.CommandType = CommandType.StoredProcedure;

                    Sqlcmd.Transaction = transaction;

                    Sqlcmd.Parameters.Add("@numdoc", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@numdoc"].Value = Factura.numdoc;

                    Sqlcmd.Parameters.Add("@tipdoc", SqlDbType.Float);
                    Sqlcmd.Parameters["@tipdoc"].Value = Factura.tipdoc;

                    Sqlcmd.Parameters.Add("@forpag", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@forpag"].Value = Factura.forpag;

                    Sqlcmd.Parameters.Add("@tipomov", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@tipomov"].Value = Factura.tipomov;

                    Sqlcmd.Parameters.Add("@codcli", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@codcli"].Value = Factura.codcli;

                    Sqlcmd.Parameters.Add("@parcial", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@parcial"].Value = Factura.parcial;

                    Sqlcmd.Parameters.Add("@parcial1", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@parcial1"].Value = Factura.parcial1;

                    Sqlcmd.Parameters.Add("@claspag", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@claspag"].Value = Factura.claspag;

                    Sqlcmd.Parameters.Add("@tipoventa", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@tipoventa"].Value = Factura.tipoventa;

                    Sqlcmd.Parameters.Add("@fecha", SqlDbType.DateTime);
                    Sqlcmd.Parameters["@fecha"].Value = Factura.fecha;

                    Sqlcmd.Parameters.Add("@fecha1", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@fecha1"].Value = Factura.fecha1;

                    Sqlcmd.Parameters.Add("@banco", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@banco"].Value = Factura.banco;

                    Sqlcmd.Parameters.Add("@numcuenta_tarj", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@numcuenta_tarj"].Value = Factura.numcuenta_tarj;

                    Sqlcmd.Parameters.Add("@cheque_caduca", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@cheque_caduca"].Value = Factura.cheque_caduca;

                    Sqlcmd.Parameters.Add("@dueño", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@dueño"].Value = Factura.dueño;

                    Sqlcmd.Parameters.Add("@autoriza", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@autoriza"].Value = Factura.autoriza;

                    Sqlcmd.Parameters.Add("@obs", SqlDbType.NText);
                    Sqlcmd.Parameters["@obs"].Value = Factura.obs;

                    Sqlcmd.Parameters.Add("@fila", SqlDbType.Float);
                    Sqlcmd.Parameters["@fila"].Value = Factura.fila;

                    Sqlcmd.Parameters.Add("@caja", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@caja"].Value = Factura.caja;

                    Sqlcmd.Parameters.Add("@cajero", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@cajero"].Value = Factura.cajero;

                    Sqlcmd.Parameters.Add("@Vendedor", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@Vendedor"].Value = Factura.Vendedor;

                    Sqlcmd.Parameters.Add("@local", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@local"].Value = Factura.local;

                    Sqlcmd.Parameters.Add("@Arqueada", SqlDbType.Bit);
                    if (Factura.Arqueada == false)
                    {
                        Sqlcmd.Parameters["@Arqueada"].Value = 0;
                    }
                    else
                    {
                        Sqlcmd.Parameters["@Arqueada"].Value = 1;
                    }

                    Sqlcmd.Parameters.Add("@imprime", SqlDbType.Bit);
                    if (Factura.imprime == false)
                    {
                        Sqlcmd.Parameters["@imprime"].Value = 0;
                    }
                    else
                    {
                        Sqlcmd.Parameters["@imprime"].Value = 1;
                    }


                    Sqlcmd.Parameters.Add("@detalle", SqlDbType.Bit);
                    if (Factura.detalle == false)
                    {
                        Sqlcmd.Parameters["@detalle"].Value = 0;
                    }
                    else
                    {
                        Sqlcmd.Parameters["@detalle"].Value = 1;
                    }

                    Sqldap = new SqlDataAdapter();
                    Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                    Sqldap.SelectCommand = Sqlcmd;

                    Sqldap.Fill(Dts);
                }

                Resultado = 1;

                foreach (var Estado in EstadoCuenta)
                {

                    Sqlcmd = new SqlCommand("sp_CreaEstadoCuenta", Sqlcon);
                    Sqlcmd.CommandType = CommandType.StoredProcedure;

                    Sqlcmd.Transaction = transaction;

                    Sqlcmd.Parameters.Add("@id", SqlDbType.Int);
                    Sqlcmd.Parameters["@id"].Value = Estado.id;

                    Sqlcmd.Parameters.Add("@numfac", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@numfac"].Value = Estado.numfac;

                    Sqlcmd.Parameters.Add("@tipodoc", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@tipodoc"].Value = Estado.tipodoc;

                    Sqlcmd.Parameters.Add("@iddoc", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@iddoc"].Value = Estado.iddoc;

                    Sqlcmd.Parameters.Add("@numdoc", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@numdoc"].Value = Estado.numdoc;

                    Sqlcmd.Parameters.Add("@codcli", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@codcli"].Value = Estado.codcli;

                    Sqlcmd.Parameters.Add("@fecha", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@fecha"].Value = Estado.fecha;

                    Sqlcmd.Parameters.Add("@obs", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@obs"].Value = Estado.obs;

                    Sqlcmd.Parameters.Add("@debe", SqlDbType.Float);
                    Sqlcmd.Parameters["@debe"].Value = Estado.debe;

                    Sqlcmd.Parameters.Add("@haber", SqlDbType.Float);
                    Sqlcmd.Parameters["@haber"].Value = Estado.haber;

                    Sqlcmd.Parameters.Add("@saldo", SqlDbType.Float);
                    Sqlcmd.Parameters["@saldo"].Value = Estado.saldo;

                    Sqlcmd.Parameters.Add("@fecha1", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@fecha1"].Value = Estado.fecha1;

                    Sqlcmd.Parameters.Add("@forpag", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@forpag"].Value = Estado.forpag;

                    Sqlcmd.Parameters.Add("@claspag", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@claspag"].Value = Estado.claspag;

                    Sqlcmd.Parameters.Add("@caja", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@caja"].Value = Estado.caja;

                    Sqldap = new SqlDataAdapter();
                    Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                    Sqldap.SelectCommand = Sqlcmd;

                    Sqldap.Fill(Dts);

                }

                foreach (var CuentaCobrar in CxC)
                {

                    Sqlcmd = new SqlCommand("sp_CreaCxC", Sqlcon);
                    Sqlcmd.CommandType = CommandType.StoredProcedure;

                    Sqlcmd.Transaction = transaction;

                    Sqlcmd.Parameters.Add("@codcli", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@codcli"].Value = CuentaCobrar.codcli;

                    Sqlcmd.Parameters.Add("@numdoc", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@numdoc"].Value = CuentaCobrar.numdoc;

                    Sqlcmd.Parameters.Add("@fecha", SqlDbType.DateTime);
                    Sqlcmd.Parameters["@fecha"].Value = CuentaCobrar.fecha;

                    Sqlcmd.Parameters.Add("@tipo", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@tipo"].Value = CuentaCobrar.tipo;

                    Sqlcmd.Parameters.Add("@debe", SqlDbType.Decimal);
                    Sqlcmd.Parameters["@debe"].Value = CuentaCobrar.debe;

                    Sqlcmd.Parameters.Add("@haber", SqlDbType.Decimal);
                    Sqlcmd.Parameters["@haber"].Value = CuentaCobrar.haber;

                    Sqlcmd.Parameters.Add("@saldo", SqlDbType.Decimal);
                    Sqlcmd.Parameters["@saldo"].Value = CuentaCobrar.saldo;

                    Sqlcmd.Parameters.Add("@fecha1", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@fecha1"].Value = CuentaCobrar.fecha1;

                    Sqlcmd.Parameters.Add("@fechapago", SqlDbType.DateTime);
                    Sqlcmd.Parameters["@fechapago"].Value = CuentaCobrar.fechapago;

                    Sqlcmd.Parameters.Add("@tipest", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@tipest"].Value = CuentaCobrar.tipest;

                    Sqlcmd.Parameters.Add("@fecven", SqlDbType.DateTime);
                    Sqlcmd.Parameters["@fecven"].Value = CuentaCobrar.fecven;

                    Sqlcmd.Parameters.Add("@forpag", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@forpag"].Value = CuentaCobrar.forpag;

                    Sqlcmd.Parameters.Add("@claspag", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@claspag"].Value = CuentaCobrar.claspag;

                    Sqlcmd.Parameters.Add("@fecven1", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@fecven1"].Value = CuentaCobrar.fecven1;

                    Sqlcmd.Parameters.Add("@fila", SqlDbType.Float);
                    Sqlcmd.Parameters["@fila"].Value = CuentaCobrar.fila;

                    Sqlcmd.Parameters.Add("@Marca", SqlDbType.NVarChar);
                    Sqlcmd.Parameters["@Marca"].Value = CuentaCobrar.Marca;

                    Sqldap = new SqlDataAdapter();
                    Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                    Sqldap.SelectCommand = Sqlcmd;

                    Sqldap.Fill(Dts);

                }


                transaction.Commit();

                try
                {
                    Sqlcon.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return Resultado;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return 0;
            }

        }

        public DataTable Bancos()
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
            Sqlcmd = new SqlCommand("SELECT  [BAN_NOMBRE]  FROM [His3000].[dbo].[BANCOS]  order by BAN_NOMBRE", Sqlcon);

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
            Sqlcmd = new SqlCommand("SELECT Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His, His3000.dbo.RUBROS.PED_CODIGO AS coddiv FROM            Sic3000.dbo.Producto INNER JOIN Sic3000.dbo.ProductoSubdivision ON Sic3000.dbo.Producto.codsub = Sic3000.dbo.ProductoSubdivision.codsub INNER JOIN His3000.dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = His3000.dbo.RUBROS.RUB_CODIGO where Sic3000.dbo.Producto.codpro = " + codPro, Sqlcon);
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
        public bool VerificaAltaPaciente(Int64 ate_codigo)
        {
            bool ok = true;
            try
            {
                ATENCIONES objAtenciones = new ATENCIONES();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    objAtenciones = (from a in contexto.ATENCIONES
                                     where a.ATE_CODIGO == ate_codigo
                                     select a).FirstOrDefault();
                    if (objAtenciones.ESC_CODIGO == 1)
                        ok = false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return ok;
        }
        public short RecuperarHabitacion(Int64 ate_codigo)
        {
            short atencion = 0;
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
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
            Sqlcmd = new SqlCommand("select HAB_CODIGO from ATENCIONES where ATE_CODIGO = @ate_codigo", Sqlcon);
            Sqlcmd.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.CommandTimeout = 180;
            SqlDataReader reader = Sqlcmd.ExecuteReader();
            if (reader.Read() == true)
            {
                atencion = reader.GetInt16(0);
                return atencion;
            }
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return atencion;

        }
        public short RecuperarEstado(short hab_codigo)
        {
            short estado = 0;
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
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
            Sqlcmd = new SqlCommand("select HE.HES_CODIGO from HABITACIONES H inner join HABITACIONES_ESTADO HE on H.HES_CODIGO = HE.HES_CODIGO where hab_Codigo =  @hab_codigo", Sqlcon);
            Sqlcmd.Parameters.AddWithValue("@hab_codigo", hab_codigo);
            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.CommandTimeout = 180;
            SqlDataReader reader = Sqlcmd.ExecuteReader();
            if (reader.Read() == true)
            {
                estado = reader.GetInt16(0);
                return estado;
            }
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return estado;

        }

        public string RecuperarClasPro(string codpro)
        {
            string claspro = "";
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
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
            Sqlcmd = new SqlCommand("select clasprod from Sic3000..Producto where codpro = @codpro", Sqlcon);
            Sqlcmd.Parameters.AddWithValue("@codpro", codpro);
            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.CommandTimeout = 180;
            SqlDataReader reader = Sqlcmd.ExecuteReader();
            if (reader.Read() == true)
            {
                claspro = reader.GetString(0);
                return claspro;
            }
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return claspro;

        }
        public DataTable TiposDescuento()
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
            Sqlcmd = new SqlCommand("select  CAST(id as varchar(10)) + ' - ' + Descripcion as tipos, ctaContable from tipodescuento", Sqlcon);

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
        public DataTable FormasDePago(String numfac)
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
            Sqlcmd = new SqlCommand("SELECT  Sic3000.dbo.Forma_Pago.forpag, Sic3000.dbo.Forma_Pago.despag, Sic3000.dbo.FacturaPago.parcial, Sic3000.dbo.FacturaPago.fecha, Sic3000.dbo.FacturaPago.banco, Sic3000.dbo.FacturaPago.CodPlazo, Sic3000.dbo.FacturaPago.numcuenta_tarj, Sic3000.dbo.FacturaPago.cheque_caduca, Sic3000.dbo.FacturaPago.autoriza, Sic3000.dbo.FacturaPago.dueño FROM  Sic3000.dbo.Nota INNER JOIN Sic3000.dbo.FacturaPago ON Sic3000.dbo.Nota.numnot = Sic3000.dbo.FacturaPago.numdoc INNER JOIN Sic3000.dbo.Forma_Pago ON Sic3000.dbo.FacturaPago.forpag = Sic3000.dbo.Forma_Pago.forpag WHERE(Sic3000.dbo.FacturaPago.numdoc = '" + numfac + "')", Sqlcon);

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
        public DataTable fechasINOUT(Int32 codigoatencion)
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
            Sqlcmd = new SqlCommand("select  IsNull(ATE_FECHA_INGRESO,0) as ingreso,  (ATE_FECHA_ALTA) as alta from ATENCIONES where ATE_CODIGO = " + codigoatencion, Sqlcon);

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
        public DataTable TipoDescuentoAtencionVer(Int32 CodAtencion)
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
            Sqlcmd = new SqlCommand("select  CAST(tipoDescuento.id as varchar(10)) + ' - ' + tipoDescuento.Descripcion as tipos FROM            dbo.ATENCIONES INNER JOIN dbo.tipoDescuento ON dbo.ATENCIONES.idTipoDescuento = dbo.tipoDescuento.id where dbo.ATENCIONES.ATE_CODIGO = " + CodAtencion, Sqlcon);

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

        public void TipoDescuentoAtencionActualizar(Int32 CodAtencion, Int32 CodDescuento)
        {
            //alex
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
            Sqlcmd = new SqlCommand("UPDATE ATENCIONES SET[idTipoDescuento] = " + CodDescuento + " WHERE [ATE_CODIGO] = " + CodAtencion, Sqlcon);

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
        }


        public int ActualizaDescuentoAtencion(Int64 CodigoAtencion)
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

            Sqlcmd = new SqlCommand("sp_ActualizaDescAtencion", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = (CodigoAtencion);


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

        public int ActualizaValorMSP(int CodigoAtencion)
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

            Sqlcmd = new SqlCommand("sp_RedMSP", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = (CodigoAtencion);


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

        public int ActualizaEstadoCuenta(Int64 CodigoAtencion, int Estado, string NumeroFactura, int usuario)
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

            Sqlcmd = new SqlCommand("sp_ActualizaEstadoCuenta", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = (CodigoAtencion);

            Sqlcmd.Parameters.Add("@Estado", SqlDbType.Int);
            Sqlcmd.Parameters["@Estado"].Value = (Estado);

            Sqlcmd.Parameters.Add("@Factura", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Factura"].Value = (NumeroFactura);

            Sqlcmd.Parameters.Add("@usuario", SqlDbType.Int);
            Sqlcmd.Parameters["@usuario"].Value = (usuario);

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

        public int ActualizaEstadoCuenta2(Int64 CodigoAtencion, string Ruc, string NumeroFactura, int Esc_Codigo, string usuario)
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

            Sqlcmd = new SqlCommand("sp_ActualizaEstadoCuenta2", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = (CodigoAtencion);

            Sqlcmd.Parameters.Add("@Ruc", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Ruc"].Value = (Ruc);

            Sqlcmd.Parameters.Add("@Email", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Email"].Value = (NumeroFactura);

            Sqlcmd.Parameters.Add("@Esc_Codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@Esc_Codigo"].Value = (Esc_Codigo);

            Sqlcmd.Parameters.Add("@usuario", SqlDbType.VarChar);
            Sqlcmd.Parameters["@usuario"].Value = (usuario);

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

        public void ActualizaEstadoHabitacion(Int64 Ate_Codigo)
        {
            // Cambio de estado / Pablo Rocha / 19/06/2013

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
            Sqlcmd = new SqlCommand("sp_CambioEstadoHabitacion", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = Ate_Codigo;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts);
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

        public void ActualizaEstadoHabitacionAuditoria(Int64 Ate_Codigo)
        {
            // Cambio de estado / Pablo Rocha / 23/07/2020

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
            Sqlcmd = new SqlCommand("sp_CambioEstadoHabitacionAuditoria", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = Ate_Codigo;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts);
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

        public DataTable AnticiposCliente(string Ate_Codigo)
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

            Sqlcmd = new SqlCommand("sp_AnticiposSic", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Ate_Codigo"].Value = Ate_Codigo;

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

        public DataTable RecuperaAteCodigo(Int32 CodigoAtencion)
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

            Sqlcmd = new SqlCommand("SELECT * FROM ATENCIONES WHERE ATE_NUMERO_ATENCION=" + CodigoAtencion, Sqlcon);

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
        public DataTable ProductosFactura(Int64 CodigoAtencion)
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

            Sqlcmd = new SqlCommand("SELECT dbo.CUENTAS_PACIENTES.Codigo_Pedido as PEDIDO, dbo.RUBROS.RUB_NOMBRE, dbo.CUENTAS_PACIENTES.CUE_FECHA, dbo.USUARIOS.APELLIDOS + ' ' + dbo.USUARIOS.NOMBRES AS USUARIO, dbo.DEPARTAMENTOS.DEP_NOMBRE, dbo.CUENTAS_PACIENTES.CUE_CODIGO, dbo.CUENTAS_PACIENTES.PRO_CODIGO, dbo.CUENTAS_PACIENTES.CUE_DETALLE, dbo.CUENTAS_PACIENTES.CUE_VALOR_UNITARIO, dbo.CUENTAS_PACIENTES.CUE_CANTIDAD, dbo.CUENTAS_PACIENTES.CUE_VALOR, dbo.CUENTAS_PACIENTES.CUE_IVA, dbo.CUENTAS_PACIENTES.Descuento, dbo.CUENTAS_PACIENTES_AUDITORIA.AUDITADA, ISNULL(dbo.CUENTAS_PACIENTES_AUDITORIA.CANTIDAD, 0) AS 'CANT. DIVIDIR', dbo.CUENTAS_PACIENTES_AUDITORIA.OBSERVACION FROM dbo.CUENTAS_PACIENTES INNER JOIN dbo.RUBROS ON dbo.CUENTAS_PACIENTES.RUB_CODIGO = dbo.RUBROS.RUB_CODIGO INNER JOIN dbo.USUARIOS ON dbo.CUENTAS_PACIENTES.ID_USUARIO = dbo.USUARIOS.ID_USUARIO INNER JOIN dbo.DEPARTAMENTOS ON dbo.USUARIOS.DEP_CODIGO = dbo.DEPARTAMENTOS.DEP_CODIGO LEFT OUTER JOIN dbo.CUENTAS_PACIENTES_AUDITORIA ON dbo.CUENTAS_PACIENTES.CUE_CODIGO = dbo.CUENTAS_PACIENTES_AUDITORIA.CUE_CODIGO where CUENTAS_PACIENTES.CUE_CANTIDAD > 0 and CUENTAS_PACIENTES.CUE_VALOR > 0 and dbo.CUENTAS_PACIENTES.ATE_CODIGO = " + CodigoAtencion + " ORDER BY CUE_FECHA ASC", Sqlcon);

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

        public DataTable recuperaobservacionAtencion(Int32 CodigoAtencion)
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

            Sqlcmd = new SqlCommand("SELECT dbo.RUBROS.RUB_NOMBRE, dbo.PEDIDOS.PED_FECHA, dbo.USUARIOS.APELLIDOS + ' ' + dbo.USUARIOS.NOMBRES AS USUARIO, dbo.DEPARTAMENTOS.DEP_NOMBRE, dbo.CUENTAS_PACIENTES.CUE_CODIGO, dbo.CUENTAS_PACIENTES.PRO_CODIGO, dbo.CUENTAS_PACIENTES.CUE_DETALLE, dbo.CUENTAS_PACIENTES.CUE_VALOR_UNITARIO, dbo.CUENTAS_PACIENTES.CUE_CANTIDAD, dbo.CUENTAS_PACIENTES.CUE_VALOR, dbo.CUENTAS_PACIENTES.CUE_IVA, dbo.CUENTAS_PACIENTES.Descuento, dbo.CUENTAS_PACIENTES_AUDITORIA.AUDITADA, dbo.CUENTAS_PACIENTES_AUDITORIA.OBSERVACION FROM            dbo.CUENTAS_PACIENTES INNER JOIN dbo.RUBROS ON dbo.CUENTAS_PACIENTES.RUB_CODIGO = dbo.RUBROS.RUB_CODIGO INNER JOIN dbo.PEDIDOS ON dbo.CUENTAS_PACIENTES.PED_CODIGO = dbo.PEDIDOS.PED_CODIGO INNER JOIN dbo.USUARIOS ON dbo.PEDIDOS.ID_USUARIO = dbo.USUARIOS.ID_USUARIO INNER JOIN dbo.DEPARTAMENTOS ON dbo.USUARIOS.DEP_CODIGO = dbo.DEPARTAMENTOS.DEP_CODIGO LEFT OUTER JOIN dbo.CUENTAS_PACIENTES_AUDITORIA ON dbo.CUENTAS_PACIENTES.CUE_CODIGO = dbo.CUENTAS_PACIENTES_AUDITORIA.CUE_CODIGO where dbo.CUENTAS_PACIENTES.ATE_CODIGO = " + CodigoAtencion, Sqlcon);

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

        public DataTable RecuperaDescuentoXrubro(Int64 ate_codigo, int rubro)
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

            Sqlcmd = new SqlCommand("sp_RecuoeraDescuentoXrubro", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ate_Codigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ate_Codigo"].Value = ate_codigo;

            Sqlcmd.Parameters.Add("@rubro", SqlDbType.Int);
            Sqlcmd.Parameters["@rubro"].Value = rubro;

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

        public DataTable RecuperaDescuentoXrubroConIva(Int64 ate_codigo, int rubro)
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

            Sqlcmd = new SqlCommand("sp_RecuperaDescuentoXrubroConIva", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ate_Codigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ate_Codigo"].Value = ate_codigo;

            Sqlcmd.Parameters.Add("@rubro", SqlDbType.Int);
            Sqlcmd.Parameters["@rubro"].Value = rubro;

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

        public DataTable RecuperaDescuentoXrubroSinIva(Int64 ate_codigo, int rubro)
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

            Sqlcmd = new SqlCommand("sp_RecuperaDescuentoXrubroSinIva", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ate_Codigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ate_Codigo"].Value = ate_codigo;

            Sqlcmd.Parameters.Add("@rubro", SqlDbType.Int);
            Sqlcmd.Parameters["@rubro"].Value = rubro;

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


        public DataTable RecuperaDescuentos()
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

            Sqlcmd = new SqlCommand("sp_RecuperaDescuento", Sqlcon);

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
            return Dts.Tables["tabla"];

        }

        public DataTable RecuperasUMAS(Int32 CodigoDescuento)
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

            Sqlcmd = new SqlCommand("SELECT  SUM(CUE_VALOR) as Total,   iif(CUE_IVA>0,'TRUE','FALSE') as iva FROM[His3000].[dbo].[CUENTAS_PACIENTES]   where ATE_CODIGO=  " + CodigoDescuento + " GROUP BY   iif(CUE_IVA > 0, 'TRUE', 'FALSE')", Sqlcon);

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

        public DataTable RecuperaDescuentosCodigo(Int32 CodigoDescuento)
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

            Sqlcmd = new SqlCommand("sp_RecuperaDescuentoCodigo", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoDescuento", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoDescuento"].Value = (CodigoDescuento);

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

        public int GeneraPrefactura(DtoPreFactura Prefactura)
        {
            // GIOVANNY TAPIA / 07/08/2012

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
                Sqlcmd = new SqlCommand("sp_InsertaDatosPrefactura", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@PREFAC_CODIGO", SqlDbType.Int);
                Sqlcmd.Parameters["@PREFAC_CODIGO"].Value = Prefactura.PREFAC_CODIGO;

                Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.Float);
                Sqlcmd.Parameters["@ATE_CODIGO"].Value = Prefactura.ATE_CODIGO;

                Sqlcmd.Parameters.Add("@PREFAC_NUMERO", SqlDbType.VarChar);
                Sqlcmd.Parameters["@PREFAC_NUMERO"].Value = Prefactura.PREFAC_NUMERO;

                Sqlcmd.Parameters.Add("@PREFAC_AUTORIZACION", SqlDbType.Int);
                Sqlcmd.Parameters["@PREFAC_AUTORIZACION"].Value = Prefactura.PREFAC_AUTORIZACION;

                Sqlcmd.Parameters.Add("@PREFAC_FECHA", SqlDbType.DateTime);
                Sqlcmd.Parameters["@PREFAC_FECHA"].Value = Prefactura.PREFAC_FECHA;

                Sqlcmd.Parameters.Add("@CLI_NOMBRE", SqlDbType.VarChar);
                Sqlcmd.Parameters["@CLI_NOMBRE"].Value = Prefactura.CLI_NOMBRE;

                Sqlcmd.Parameters.Add("@CLI_RUC", SqlDbType.VarChar);
                Sqlcmd.Parameters["@CLI_RUC"].Value = Prefactura.CLI_RUC;

                Sqlcmd.Parameters.Add("@CLI_TELEFONO", SqlDbType.VarChar);
                Sqlcmd.Parameters["@CLI_TELEFONO"].Value = Prefactura.CLI_TELEFONO;

                Sqlcmd.Parameters.Add("@PREFAC_TOTAL", SqlDbType.Decimal);
                Sqlcmd.Parameters["@PREFAC_TOTAL"].Value = Prefactura.PREFAC_TOTAL;

                Sqlcmd.Parameters.Add("@PREFAC_SUBTOTAL", SqlDbType.Decimal);
                Sqlcmd.Parameters["@PREFAC_SUBTOTAL"].Value = Prefactura.PREFAC_SUBTOTAL;

                Sqlcmd.Parameters.Add("@PREFAC_IVAUNO", SqlDbType.Decimal);
                Sqlcmd.Parameters["@PREFAC_IVAUNO"].Value = Prefactura.PREFAC_IVAUNO;

                Sqlcmd.Parameters.Add("@PREFAC_IVADOS", SqlDbType.Decimal);
                Sqlcmd.Parameters["@PREFAC_IVADOS"].Value = Prefactura.PREFAC_IVADOS;

                Sqlcmd.Parameters.Add("@PREFAC_IVATRES", SqlDbType.Decimal);
                Sqlcmd.Parameters["@PREFAC_IVATRES"].Value = Prefactura.PREFAC_IVATRES;

                Sqlcmd.Parameters.Add("@PREFAC_ESTADO", SqlDbType.VarChar);
                Sqlcmd.Parameters["@PREFAC_ESTADO"].Value = Prefactura.PREFAC_ESTADO;

                Sqlcmd.Parameters.Add("@PREFAC_CAJA", SqlDbType.VarChar);
                Sqlcmd.Parameters["@PREFAC_CAJA"].Value = Prefactura.PREFAC_CAJA;

                Sqlcmd.Parameters.Add("@PREFAC_VENDEDOR", SqlDbType.VarChar);
                Sqlcmd.Parameters["@PREFAC_VENDEDOR"].Value = Prefactura.PREFAC_VENDEDOR;

                Sqlcmd.Parameters.Add("@PREFAC_LOCAL", SqlDbType.VarChar);
                Sqlcmd.Parameters["@PREFAC_LOCAL"].Value = Prefactura.PREFAC_LOCAL;

                Sqlcmd.Parameters.Add("@PREFAC_ARQUEO", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@PREFAC_ARQUEO"].Value = Prefactura.PREFAC_ARQUEO;

                Sqlcmd.Parameters.Add("@PREFAC_DESCUENTO", SqlDbType.NVarChar);
                Sqlcmd.Parameters["@PREFAC_DESCUENTO"].Value = Prefactura.PREFAC_DESCUENTO;

                Sqlcmd.Parameters.Add("@PREFAC_CONIVA", SqlDbType.Decimal);
                Sqlcmd.Parameters["@PREFAC_CONIVA"].Value = Prefactura.PREFAC_CONIVA;

                Sqlcmd.Parameters.Add("@PREFAC_SINIVA", SqlDbType.Decimal);
                Sqlcmd.Parameters["@PREFAC_SINIVA"].Value = Prefactura.PREFAC_SINIVA;


                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;

                Sqldap.Fill(Dts);


                //Resultado = 1;

                foreach (var Detalle in Prefactura.DetallePreFactura)
                {

                    Sqlcmd = new SqlCommand("sp_InsertaDatosDetallePrefactura", Sqlcon);
                    Sqlcmd.CommandType = CommandType.StoredProcedure;

                    Sqlcmd.Transaction = transaction;

                    Sqlcmd.Parameters.Add("@COD_PFDETALLE", SqlDbType.Int);
                    Sqlcmd.Parameters["@COD_PFDETALLE"].Value = Detalle.COD_PFDETALLE;

                    Sqlcmd.Parameters.Add("@PREFAC_CODIGO", SqlDbType.Int);
                    Sqlcmd.Parameters["@PREFAC_CODIGO"].Value = Detalle.PREFAC_NUMERO;

                    Sqlcmd.Parameters.Add("@DET_DESCIPCION", SqlDbType.VarChar);
                    Sqlcmd.Parameters["@DET_DESCIPCION"].Value = Detalle.DET_DESCIPCION;

                    Sqlcmd.Parameters.Add("@DET_VALOR", SqlDbType.Decimal);
                    Sqlcmd.Parameters["@DET_VALOR"].Value = Detalle.DET_VALOR;

                    Sqlcmd.Parameters.Add("@DET_ESTADO", SqlDbType.TinyInt);
                    Sqlcmd.Parameters["@DET_ESTADO"].Value = Detalle.DET_ESTADO;


                    Sqldap = new SqlDataAdapter();
                    Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                    Sqldap.SelectCommand = Sqlcmd;

                    Sqldap.Fill(Dts);
                }

                transaction.Commit();
                try
                {
                    Sqlcon.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return Resultado;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return 0;
            }
        }

        public DataTable DatosPreFactura(String NumeroPrefactura)
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

            Sqlcmd = new SqlCommand("sp_GenereaPrefactura", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_NumeroPrefactura", SqlDbType.VarChar);
            Sqlcmd.Parameters["@p_NumeroPrefactura"].Value = (NumeroPrefactura);

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

        public DataTable DatosFacturaCambio(Int64 CodigoAtencion, Int32 CodigoArea, Int32 CodigoSubArea)
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

            Sqlcmd = new SqlCommand("sp_GeneraValoresCuenta", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.BigInt);
            Sqlcmd.Parameters["@p_CodigoAtencion"].Value = (CodigoAtencion);

            Sqlcmd.Parameters.Add("@p_CodigoArea", SqlDbType.Int);
            Sqlcmd.Parameters["@p_CodigoArea"].Value = (CodigoArea);

            Sqlcmd.Parameters.Add("@p_CodigosubArea", SqlDbType.Int);
            Sqlcmd.Parameters["@p_CodigosubArea"].Value = (CodigoSubArea);

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

        public DataTable DatosDescuento(Int32 CodigoAtencion, Int32 CodigoRubro)
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

            Sqlcmd = new SqlCommand("pa_DetalleUFactura", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodAtencion", SqlDbType.Int);
            Sqlcmd.Parameters["@CodAtencion"].Value = (CodigoAtencion);

            Sqlcmd.Parameters.Add("@CodRubro", SqlDbType.Int);
            Sqlcmd.Parameters["@CodRubro"].Value = (CodigoRubro);

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

        public DataTable DatosDescuentoHonorario(Int64 CodigoAtencion, Int32 CodigoRubro)
        {

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
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

            Sqlcmd = new SqlCommand("SELECT PRO_CODIGO,M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2  AS Medico, CUE_DETALLE as Producto, (CUE_CANTIDAD) as Cantidad, CUE_VALOR_UNITARIO AS 'Valor Unitario', CUE_VALOR as Total, pordescuento as Porcentage_desc, Descuento as Valor_Descuento, RUB_CODIGO, CUE_CODIGO, CUE_IVA as iva FROM CUENTAS_PACIENTES CP INNER JOIN MEDICOS M ON CP.MED_CODIGO = M.MED_CODIGO where ATE_CODIGO = @ate_codigo and RUB_CODIGO = @rub_codigo", Sqlcon);

            Sqlcmd.CommandType = CommandType.Text;

            Sqlcmd.Parameters.AddWithValue("@ate_codigo", CodigoAtencion);
            Sqlcmd.Parameters.AddWithValue("@rub_codigo", CodigoRubro);

            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012

            reader = Sqlcmd.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            Sqlcmd.Parameters.Clear();
            Sqlcon.Close();
            return Tabla;
        }

        public void GuardaCambiosFactura(Int64 CodigoAtencion, DataTable ListaItems, int coddepartamento)
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
            Sqlcmd = new SqlCommand("sp_ParametroInventariable", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = Sqlcmd.ExecuteReader();
            bool parametro = false;
            while (reader.Read())
            {
                parametro = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            reader.Close();
            Sqlcmd = null;
            Sqlcmd = new SqlCommand("sp_ParametroInventariableMedicamentos", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader1 = Sqlcmd.ExecuteReader();
            bool parametro2 = false;
            while (reader1.Read())
            {
                parametro2 = Convert.ToBoolean(reader1["PAD_ACTIVO"].ToString());
            }
            reader1.Close();
            Sqlcmd = null;
            foreach (DataRow Item1 in ListaItems.AsEnumerable())
            {
                if (Item1["Estado"].ToString() == "U")
                {
                    string codpro = Item1["CODIGO"].ToString();
                    string claspro = RecuperarClasPro(codpro);
                    if (parametro)
                    {
                        if (parametro2 && claspro.Trim() == "B" && coddepartamento == 1)
                        {
                            Sqlcmd = new SqlCommand("sp_ActualizaValoresCuentasBienes", Sqlcon);
                            Sqlcmd.CommandType = CommandType.StoredProcedure;

                            Sqlcmd.Parameters.Add("@p_CUE_CODIGO", SqlDbType.BigInt);
                            Sqlcmd.Parameters["@p_CUE_CODIGO"].Value = (Item1["ID"].ToString());

                            Sqlcmd.Parameters.Add("@CUE_CANTIDAD", SqlDbType.Decimal);
                            Sqlcmd.Parameters["@CUE_CANTIDAD"].Value = (Item1["CANTIDAD"].ToString());

                            Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.BigInt);
                            Sqlcmd.Parameters["@p_CodigoAtencion"].Value = (CodigoAtencion);

                            Sqlcmd.Parameters.Add("@p_CodigoPedido", SqlDbType.Int);
                            Sqlcmd.Parameters["@p_CodigoPedido"].Value = (Item1["PEDIDO"].ToString());

                            Sqlcmd.Parameters.Add("@p_CodigoProducto", SqlDbType.VarChar);
                            Sqlcmd.Parameters["@p_CodigoProducto"].Value = (Item1["CODIGO"].ToString());

                            Sqlcmd.Parameters.Add("@p_Valor", SqlDbType.Decimal);
                            Sqlcmd.Parameters["@p_Valor"].Value = Convert.ToDecimal(Item1["VALOR"].ToString());

                            Sqlcmd.Parameters.Add("@p_Iva", SqlDbType.Decimal);
                            Sqlcmd.Parameters["@p_Iva"].Value = Convert.ToDecimal(Item1["IVA"].ToString());

                            Sqlcmd.Parameters.Add("@p_Total", SqlDbType.Decimal);
                            Sqlcmd.Parameters["@p_Total"].Value = Convert.ToDecimal(Item1["TOTAL"].ToString());

                            Sqldap = new SqlDataAdapter();
                            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                            Sqldap.SelectCommand = Sqlcmd;
                            Dts = new DataSet();
                            Sqldap.Fill(Dts, "tabla");
                        }
                        if (parametro2 && claspro.Trim() == "B" && coddepartamento == 7)
                        {
                            Sqlcmd = new SqlCommand("sp_ActualizaValoresCuentasBienes", Sqlcon);
                            Sqlcmd.CommandType = CommandType.StoredProcedure;

                            Sqlcmd.Parameters.Add("@p_CUE_CODIGO", SqlDbType.BigInt);
                            Sqlcmd.Parameters["@p_CUE_CODIGO"].Value = (Item1["ID"].ToString());

                            Sqlcmd.Parameters.Add("@CUE_CANTIDAD", SqlDbType.Decimal);
                            Sqlcmd.Parameters["@CUE_CANTIDAD"].Value = (Item1["CANTIDAD"].ToString());

                            Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.BigInt);
                            Sqlcmd.Parameters["@p_CodigoAtencion"].Value = (CodigoAtencion);

                            Sqlcmd.Parameters.Add("@p_CodigoPedido", SqlDbType.Int);
                            Sqlcmd.Parameters["@p_CodigoPedido"].Value = (Item1["PEDIDO"].ToString());

                            Sqlcmd.Parameters.Add("@p_CodigoProducto", SqlDbType.VarChar);
                            Sqlcmd.Parameters["@p_CodigoProducto"].Value = (Item1["CODIGO"].ToString());

                            Sqlcmd.Parameters.Add("@p_Valor", SqlDbType.Decimal);
                            Sqlcmd.Parameters["@p_Valor"].Value = Convert.ToDecimal(Item1["VALOR"].ToString());

                            Sqlcmd.Parameters.Add("@p_Iva", SqlDbType.Decimal);
                            Sqlcmd.Parameters["@p_Iva"].Value = Convert.ToDecimal(Item1["IVA"].ToString());

                            Sqlcmd.Parameters.Add("@p_Total", SqlDbType.Decimal);
                            Sqlcmd.Parameters["@p_Total"].Value = Convert.ToDecimal(Item1["TOTAL"].ToString());

                            Sqldap = new SqlDataAdapter();
                            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                            Sqldap.SelectCommand = Sqlcmd;
                            Dts = new DataSet();
                            Sqldap.Fill(Dts, "tabla");
                        }
                        if (claspro.Trim() == "S" && coddepartamento == 1)
                        {
                            Sqlcmd = new SqlCommand("sp_ActualizaValoresCuentas", Sqlcon);
                            Sqlcmd.CommandType = CommandType.StoredProcedure;

                            Sqlcmd.Parameters.Add("@p_CUE_CODIGO", SqlDbType.BigInt);
                            Sqlcmd.Parameters["@p_CUE_CODIGO"].Value = (Item1["ID"].ToString());

                            Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.BigInt);
                            Sqlcmd.Parameters["@p_CodigoAtencion"].Value = (CodigoAtencion);

                            Sqlcmd.Parameters.Add("@p_CodigoPedido", SqlDbType.Int);
                            Sqlcmd.Parameters["@p_CodigoPedido"].Value = (Item1["PEDIDO"].ToString());

                            Sqlcmd.Parameters.Add("@p_CodigoProducto", SqlDbType.VarChar);
                            Sqlcmd.Parameters["@p_CodigoProducto"].Value = (Item1["CODIGO"].ToString());

                            Sqlcmd.Parameters.Add("@p_Cantidad", SqlDbType.Decimal);
                            Sqlcmd.Parameters["@p_Cantidad"].Value = Convert.ToDecimal(Item1["CANTIDAD"].ToString());

                            Sqlcmd.Parameters.Add("@p_Valor", SqlDbType.Decimal);
                            Sqlcmd.Parameters["@p_Valor"].Value = Convert.ToDecimal(Item1["VALOR"].ToString());

                            Sqlcmd.Parameters.Add("@p_Iva", SqlDbType.Decimal);
                            Sqlcmd.Parameters["@p_Iva"].Value = Convert.ToDecimal(Item1["IVA"].ToString());

                            Sqlcmd.Parameters.Add("@p_Total", SqlDbType.Decimal);
                            Sqlcmd.Parameters["@p_Total"].Value = Convert.ToDecimal(Item1["TOTAL"].ToString());

                            Sqldap = new SqlDataAdapter();
                            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                            Sqldap.SelectCommand = Sqlcmd;
                            Dts = new DataSet();
                            Sqldap.Fill(Dts, "tabla");
                        }
                        if (claspro.Trim() == "S" && coddepartamento == 7)
                        {
                            Sqlcmd = new SqlCommand("sp_ActualizaValoresCuentas", Sqlcon);
                            Sqlcmd.CommandType = CommandType.StoredProcedure;

                            Sqlcmd.Parameters.Add("@p_CUE_CODIGO", SqlDbType.BigInt);
                            Sqlcmd.Parameters["@p_CUE_CODIGO"].Value = (Item1["ID"].ToString());

                            Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.BigInt);
                            Sqlcmd.Parameters["@p_CodigoAtencion"].Value = (CodigoAtencion);

                            Sqlcmd.Parameters.Add("@p_CodigoPedido", SqlDbType.Int);
                            Sqlcmd.Parameters["@p_CodigoPedido"].Value = (Item1["PEDIDO"].ToString());

                            Sqlcmd.Parameters.Add("@p_CodigoProducto", SqlDbType.VarChar);
                            Sqlcmd.Parameters["@p_CodigoProducto"].Value = (Item1["CODIGO"].ToString());

                            Sqlcmd.Parameters.Add("@p_Cantidad", SqlDbType.Decimal);
                            Sqlcmd.Parameters["@p_Cantidad"].Value = Convert.ToDecimal(Item1["CANTIDAD"].ToString());

                            Sqlcmd.Parameters.Add("@p_Valor", SqlDbType.Decimal);
                            Sqlcmd.Parameters["@p_Valor"].Value = Convert.ToDecimal(Item1["VALOR"].ToString());

                            Sqlcmd.Parameters.Add("@p_Iva", SqlDbType.Decimal);
                            Sqlcmd.Parameters["@p_Iva"].Value = Convert.ToDecimal(Item1["IVA"].ToString());

                            Sqlcmd.Parameters.Add("@p_Total", SqlDbType.Decimal);
                            Sqlcmd.Parameters["@p_Total"].Value = Convert.ToDecimal(Item1["TOTAL"].ToString());

                            Sqldap = new SqlDataAdapter();
                            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                            Sqldap.SelectCommand = Sqlcmd;
                            Dts = new DataSet();
                            Sqldap.Fill(Dts, "tabla");
                        }
                    }
                    else
                    {
                        Sqlcmd = new SqlCommand("sp_ActualizaValoresCuentas", Sqlcon);
                        Sqlcmd.CommandType = CommandType.StoredProcedure;

                        Sqlcmd.Parameters.Add("@p_CUE_CODIGO", SqlDbType.BigInt);
                        Sqlcmd.Parameters["@p_CUE_CODIGO"].Value = (Item1["ID"].ToString());

                        Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.BigInt);
                        Sqlcmd.Parameters["@p_CodigoAtencion"].Value = (CodigoAtencion);

                        Sqlcmd.Parameters.Add("@p_CodigoPedido", SqlDbType.Int);
                        Sqlcmd.Parameters["@p_CodigoPedido"].Value = (Item1["PEDIDO"].ToString());

                        Sqlcmd.Parameters.Add("@p_CodigoProducto", SqlDbType.VarChar);
                        Sqlcmd.Parameters["@p_CodigoProducto"].Value = (Item1["CODIGO"].ToString());

                        Sqlcmd.Parameters.Add("@p_Cantidad", SqlDbType.Decimal);
                        Sqlcmd.Parameters["@p_Cantidad"].Value = Convert.ToDecimal(Item1["CANTIDAD"].ToString());

                        Sqlcmd.Parameters.Add("@p_Valor", SqlDbType.Decimal);
                        Sqlcmd.Parameters["@p_Valor"].Value = Convert.ToDecimal(Item1["VALOR"].ToString());

                        Sqlcmd.Parameters.Add("@p_Iva", SqlDbType.Decimal);
                        Sqlcmd.Parameters["@p_Iva"].Value = Convert.ToDecimal(Item1["IVA"].ToString());

                        Sqlcmd.Parameters.Add("@p_Total", SqlDbType.Decimal);
                        Sqlcmd.Parameters["@p_Total"].Value = Convert.ToDecimal(Item1["TOTAL"].ToString());

                        Sqldap = new SqlDataAdapter();
                        Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                        Sqldap.SelectCommand = Sqlcmd;
                        Dts = new DataSet();
                        Sqldap.Fill(Dts, "tabla");
                    }
                }
            }

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void EliminaValoresCuentas(Int64 CodigoAtencion, List<DtoItemEliminadoCuentas> ListaItems)  // genera los datos para el reporte de la prefactura
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

            foreach (var Linea in ListaItems)
            {


                Sqlcmd = new SqlCommand("sp_EliminaValoresCuentas", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;

                Sqlcmd.Parameters.Add("@p_CodigoAtencion", SqlDbType.BigInt);
                Sqlcmd.Parameters["@p_CodigoAtencion"].Value = Linea.CodigoAtencion;

                Sqlcmd.Parameters.Add("@p_CodigoPedido", SqlDbType.Int);
                Sqlcmd.Parameters["@p_CodigoPedido"].Value = Linea.CodigoPedido;

                Sqlcmd.Parameters.Add("@p_CodigoProducto", SqlDbType.VarChar);
                Sqlcmd.Parameters["@p_CodigoProducto"].Value = Linea.Producto;

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts, "tabla");

            }
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion


        public int ValidaSeguroConvenio(string Atencion, string Cod_pro)
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

            Sqlcmd = new SqlCommand("[sp_ValidaSeguroConvenio]", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@Atenciones", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Atenciones"].Value = (Atencion);

            Sqlcmd.Parameters.Add("@Cod_pro", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Cod_pro"].Value = (Cod_pro);

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

        public string ValidaCopago(string Atencion)
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

            Sqlcmd = new SqlCommand("[sp_VerificaCopago]", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = (Atencion);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (Dts.Rows.Count > 0)
            {
                return Convert.ToString(Dts.Rows[0][0]);
            }
            else
            {
                return "N";
            }


        }





        public int EstadoAnticipos(int Indicador, string Factura, decimal valor)
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

            Sqlcmd = new SqlCommand("sp_CambiaEstadoAnticipo", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@Indicador", SqlDbType.Int);
            Sqlcmd.Parameters["@Indicador"].Value = (Indicador);
            Sqlcmd.Parameters.Add("@Factura", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Factura"].Value = (Factura);
            Sqlcmd.Parameters.Add("@valor", SqlDbType.Decimal);
            Sqlcmd.Parameters["@valor"].Value = (valor);

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

        public DataTable ValidaAnticipos(Int64 Indicador)
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

            Sqlcmd = new SqlCommand("sp_ValidaAnticipos", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@Indicador", SqlDbType.BigInt);
            Sqlcmd.Parameters["@Indicador"].Value = (Indicador);


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

        public int AltaProgramada(Int64 Ate_Codigo)
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
            Sqlcmd = new SqlCommand("sp_AltaProgramada", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@Ate_Codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@Ate_Codigo"].Value = (Ate_Codigo);
            try
            {
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts, "tabla");
                Sqlcon.Close();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int GuardaAuxAgrupa(Int64 Ate_Codigo)
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
            Sqlcmd = new SqlCommand("sp_GuardaAuxAgrupa", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@Ate_Codigo"].Value = (Ate_Codigo);
            try
            {
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts, "tabla");
                Sqlcon.Close();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public bool GuardaAuxAgrupacion(Int64 Ate_Codigo, Int64 ate_codigo, int usuario)
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
            Sqlcmd = new SqlCommand("sp_AgrupacionCuentas", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_CodigoAgrupa", SqlDbType.Int);
            Sqlcmd.Parameters["@Ate_CodigoAgrupa"].Value = (Ate_Codigo);

            Sqlcmd.Parameters.Add("@ate_codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@ate_codigo"].Value = (ate_codigo);

            Sqlcmd.Parameters.Add("@usuario", SqlDbType.Int);
            Sqlcmd.Parameters["@usuario"].Value = (usuario);

            try
            {
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts, "tabla");
                Sqlcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public DataTable BuscaPaciente(string HistoriaClinica, string NombrePaciente, string Identificacion)
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

            Sqlcmd = new SqlCommand("sp_BuscaPaciente", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@HistoriaClinica", SqlDbType.VarChar);
            Sqlcmd.Parameters["@HistoriaClinica"].Value = (HistoriaClinica);

            Sqlcmd.Parameters.Add("@NombrePaciente", SqlDbType.VarChar);
            Sqlcmd.Parameters["@NombrePaciente"].Value = (NombrePaciente);

            Sqlcmd.Parameters.Add("@Identificacion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Identificacion"].Value = (Identificacion);

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

        public int EliminaAuxAgrupa()
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
            Sqlcmd = new SqlCommand("sp_EliminaAuxAgrupa", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            try
            {
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts, "tabla");
                Sqlcon.Close();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public DataTable RecuperaAuxAgrupa()
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

            Sqlcmd = new SqlCommand("sp_RecuperaAuxAgrupa", Sqlcon);
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
            return Dts.Tables["tabla"];
        }
        public DataTable AgrupaCuentas(Int64 Ate_Codigo1, Int64 Ate_Codigo2)
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

            Sqlcmd = new SqlCommand("sp_AgrupaCuentas", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo1", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Ate_Codigo1"].Value = (Ate_Codigo1);

            Sqlcmd.Parameters.Add("@Ate_Codigo2", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Ate_Codigo2"].Value = (Ate_Codigo2);

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

        public DataTable RecuperaNumeroFactura(int caja)
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

            Sqlcmd = new SqlCommand("sp_RecuperaNumeroFactura", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@caja", SqlDbType.Int);
            Sqlcmd.Parameters["@caja"].Value = (caja);

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

        public DataTable IncrementaNumeroFactura(int caja)
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

            Sqlcmd = new SqlCommand("sp_IncrementaNumeroFactura2", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@caja", SqlDbType.VarChar);
            Sqlcmd.Parameters["@caja"].Value = (caja);

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

        public DataTable RecuperaResolucion2020SRI()
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

            Sqlcmd = new SqlCommand("sp_RecuperaResolucion2020SRI", Sqlcon);
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
            return Dts.Tables["tabla"];
        }

        public bool CreaCopagoIva(string ateCoidgo, string totalConIva, string iva)
        {
            bool ok = false;
            SqlConnection con = null;
            SqlCommand cmd = null;
            BaseContextoDatos obj = new BaseContextoDatos();
            try
            {
                con = obj.ConectarBd();
                cmd = new SqlCommand("sp_CreaCopagoIva", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ateCodigo", ateCoidgo);
                cmd.Parameters.AddWithValue("@totalConIva", Convert.ToDecimal(totalConIva));
                cmd.Parameters.AddWithValue("@iva", Convert.ToDecimal(iva));
                con.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    ok = true;
            }
            catch (Exception ex)
            {
                ok = false;
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return ok;
        }

        public string RecuperaNumeroAtencion(Int64 ateCodigo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var id = (from d in contexto.ATENCIONES
                              where d.ATE_CODIGO == ateCodigo
                              select d.ATE_NUMERO_ATENCION).FirstOrDefault();
                    return id;
                }
            }
            catch (Exception err) { throw err; }
        }

        public Boolean actualizaEscCodigo(int ateCodigo)
        {
            Boolean ok = false;
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    ATENCIONES atencion = (from a in contexto.ATENCIONES
                                           where a.ATE_CODIGO == ateCodigo
                                           select a).FirstOrDefault();
                    atencion.ESC_CODIGO = 2;
                    atencion.ATE_FECHA_ALTA = DateTime.Now;
                    contexto.SaveChanges();
                    ok = true;
                }
            }
            catch (Exception err) { throw err; }
            return ok;
        }
        public Boolean actualizaEscCodigoPrefactura(int ateCodigo)
        {
            Boolean ok = false;
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    ATENCIONES atencion = (from a in contexto.ATENCIONES
                                           where a.ATE_CODIGO == ateCodigo
                                           select a).FirstOrDefault();
                    atencion.ESC_CODIGO = 3;
                    atencion.ATE_FECHA_ALTA = DateTime.Now;
                    atencion.ATE_ESTADO = false;
                    contexto.SaveChanges();
                    ok = true;
                }
            }
            catch (Exception err) { throw err; }
            return ok;
        }
        public Boolean RevertirEscCodigo(Int64 ateCodigo)
        {
            Boolean ok = false;
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    ATENCIONES atencion = (from a in contexto.ATENCIONES
                                           where a.ATE_CODIGO == ateCodigo
                                           select a).FirstOrDefault();
                    atencion.ESC_CODIGO = 1;
                    atencion.ATE_FECHA_ALTA = null;
                    atencion.ATE_ESTADO = true;
                    contexto.SaveChanges();
                    ok = true;
                }
            }
            catch (Exception err) { throw err; }
            return ok;
        }
        public void EliminaMEDICOS_ALTA(Int64 codigo)
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
            string query = "";
            query = ("DELETE FROM MEDICOS_ALTA  WHERE ATE_CODIGO = " + codigo + " ");
            try
            {
                Sqlcmd = new SqlCommand(query, Sqlcon);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;

                SqlDataReader reader;
                Sqldap.SelectCommand = Sqlcmd;
                reader = Sqlcmd.ExecuteReader();
                reader.Close();
                Sqldap.Fill(Dts);
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public List<CUENTAS_PACIENTES> RecuepraCuenta(Int64 ateCodigo)
        {
            List<CUENTAS_PACIENTES> lista = new List<CUENTAS_PACIENTES>();
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return lista = (from c in contexto.CUENTAS_PACIENTES
                                    where c.ATE_CODIGO == ateCodigo
                                    select c).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable DetalleItem(int ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_DetallePorItem", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ateCodigo", ate_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
        public DataTable DetalleArea(int ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_DetalleArea", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ateCodigo", ate_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
        public DataTable FormaPagoFactura(string numFactura)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_FormaPagoFactura", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@numFactura", numFactura);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
        public string ValidaPEmision(string numcaja)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            string pemision = "";
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ValidarPEmision", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@caja", Convert.ToInt32(numcaja));
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                pemision = reader["nestablecimiento"].ToString();
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return pemision;
        }

        public DataTable RecuperarLeyendas()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("SELECT ContribuyenteEspecial AS CONTRIBUYENTE, ObligadoContabilidad AS CONTABILIDAD, '' AS LEYENDA FROM Sic3000..Empresa UNION SELECT '' AS CONTIBUYENTE, '' AS CONTABILIDAD, ISNULL(Texto, '') AS LEYENDA FROM Sic3000..ParametrosFactura WHERE codpar = 166", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            DataTable Tabla = new DataTable();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }
        public List<CUENTAS_PACIENTES> Honorarios(Int64 ate_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<CUENTAS_PACIENTES> h = new List<CUENTAS_PACIENTES>();

                h = (from c in db.CUENTAS_PACIENTES
                     join hce in db.HONORARIOS_CONSULTA_EXTERNA on c.PRO_CODIGO equals hce.PRO_CODIGO
                     //where c.CUE_DETALLE.Contains("HONORARIOS") && c.ATE_CODIGO == ate_codigo // se cambia por cambio de proceso a rubros por la tabla // Mario Valencia // 21-12-2023
                     where c.ATE_CODIGO == ate_codigo
                     select c).ToList();
                return h;
            }
        }
        public DataTable FormasPagoFacturaHonorarios(string numfac)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();

            DataTable tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_FormaPagoHonorarios", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@numfac", numfac);
            reader = command.ExecuteReader();
            tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return tabla;
        }

        public bool ExportaCuentaPaciente(Int64 atencion, Int64 pacCodigo)
        {
            SqlConnection Sqlcon = null;
            SqlCommand Sqlcmd = null;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            bool response = false;
            try
            {
                Sqlcon = obj.ConectarBd();
                Sqlcmd = new SqlCommand("sp_ExportaCuentaPaciente", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Parameters.AddWithValue("@ATE_CODIGO", atencion);
                Sqlcmd.Parameters.AddWithValue("@PAC_CODIGO", pacCodigo);
                Sqlcon.Open();
                Sqlcmd.ExecuteNonQuery();
                reader = Sqlcmd.ExecuteReader();
                while (reader.Read())
                {
                    response = Convert.ToBoolean(reader["ok"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Sqlcon.Close();
            }
            return response;
        }
        public DataTable FacturaDetalleAgrupado(string numfac)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select * from Sic3000..Nota where numfac = '" + numfac + "'", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            DataTable Tabla = new DataTable();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public List<DtoVistaCopago> cargaCuentaPacienteXAtencion(Int64 ate_codigo)
        {
            List<DtoVistaCopago> lstCopago = new List<DtoVistaCopago>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var resultado = from cuenta in contexto.CUENTAS_PACIENTES
                                join rubro in contexto.RUBROS on cuenta.RUB_CODIGO equals rubro.RUB_CODIGO
                                where cuenta.CUE_CANTIDAD > 0 && cuenta.CUE_VALOR > 0 && cuenta.ATE_CODIGO == ate_codigo
                                orderby cuenta.CUE_FECHA ascending
                                select new
                                {
                                    ID = cuenta.CUE_CODIGO,
                                    PEDIDO = cuenta.Codigo_Pedido,
                                    RUBRO = rubro.RUB_NOMBRE,
                                    cuenta.PRO_CODIGO,
                                    DETALLE = cuenta.CUE_DETALLE,
                                    VALOR_UNITARIO = cuenta.CUE_VALOR_UNITARIO,
                                    CANTIDAD = cuenta.CUE_CANTIDAD,
                                    cuenta.CUE_VALOR,
                                    cuenta.CUE_IVA,
                                    VALOR_COPAGO = 0,
                                    PORCENTAJE_COPAGO = 0,
                                    UNITARIO_COPAGO = 0,
                                    TOTAL_COPAGO = 0,
                                    UNITARIO = 0,
                                    TOTAL = 0,
                                    TOTAL_CUENTA = 0,
                                    IVA = 0,
                                    IVA_COPAGO = 0
                                };

                foreach (var item in resultado)
                {
                    DtoVistaCopago copago = new DtoVistaCopago();
                    copago.ID = item.ID;
                    copago.PEDIDO = (long)item.PEDIDO;
                    copago.RUBRO = item.RUBRO;
                    copago.PRO_CODIGO = item.PRO_CODIGO;
                    copago.DETALLE = item.DETALLE;
                    copago.CANTIDAD = (long)item.CANTIDAD;
                    copago.VALOR_UNITARIO = (decimal)item.VALOR_UNITARIO;
                    copago.VALOR_IVA = (decimal)item.CUE_IVA;
                    copago.VALOR_TOTAL = (decimal)item.CUE_VALOR;
                    copago.PORCENTAJE_COPAGO = item.PORCENTAJE_COPAGO;
                    copago.VALOR_COPAGO = item.VALOR_COPAGO;
                    copago.UNITARIO_COPAGO = item.UNITARIO_COPAGO;
                    copago.IVA_COPAGO = item.IVA_COPAGO;
                    copago.TOTAL_COPAGO = item.TOTAL_COPAGO;
                    copago.UNITARIO = (decimal)item.VALOR_UNITARIO;
                    copago.IVA = (decimal)item.CUE_IVA;
                    copago.TOTAL = (decimal)item.CUE_VALOR;

                    lstCopago.Add(copago);

                }

                return lstCopago;
            }

        }
    }
}
