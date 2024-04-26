using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Negocio;
using His.Entidades.Clases;
using Core.Entidades;
using His.Parametros;

namespace His.Honorarios
{
    public partial class frm_NotaDautomaticas : Form
    {

        #region Variables
        public NOTAS_CREDITO_DEBITO notadebModificada = new NOTAS_CREDITO_DEBITO();
        public List<DtoHonorariosNotasDebito> honorarios = new List<DtoHonorariosNotasDebito>();
        public CAJAS caja = new CAJAS();
        public int columnabuscada;
        public bool comisionreferido;
        #endregion

        #region Constructores
        public frm_NotaDautomaticas()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frm_NotaDautomaticas_Load(object sender, EventArgs e)
        {
            try
            {
                if (NegValidaciones.localAsignado() == false)
                {
                    frm_AsignaLocal lista = new frm_AsignaLocal();
                    lista.ShowDialog();
                }
                HalitarControles(true, false, false, true, false, true, false);
                label2.Text = (NegGeneracionesAutomaticas.RecuperaMaximo()+1).ToString();
                NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();
                caja = NegCajas.ListaCajas().Where(cod => cod.LOCALES.LOC_CODIGO == Sesion.codLocal).FirstOrDefault();
                txt_ingreso.Text = DateTime.Now.ToString();
                RecuperaHonorariossinRetencion();
                //this.gridnotacd.CellValidating += new DataGridViewCellValidatingEventHandler(gridnotacd_CellValidating);

                if (NegNumeroControl.NumerodeControlAutomatico(4))
                {
                    numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 4).FirstOrDefault();
                    txt_codigo.Text = string.Format("{0:000}-{1:000}-{2:0000000}", caja.CAJ_NUMERO.ToString().Substring(0, 3), caja.CAJ_NUMERO.ToString().Substring(3), Int16.Parse(numerocontrol.NUMCON));
                    chk_Maratodas.Focus();
                }
                else
                {
                    txt_codigo.ReadOnly = false;
                    txt_codigo.Focus();
                }



            }
            catch (Exception ex)
            {
                if(ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                GrabarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void chk_Maratodas_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Maratodas.Checked == true)
            {
                for (int i = 0; i <= gridnotacd.RowCount - 1; i++)
                {
                    gridnotacd.Rows[i].Cells[11].Value = true;
                }
            }
        }
        private void txt_codigo_Leave(object sender, EventArgs e)
        {
            if (txt_codigo.Text != string.Empty)
            {
                txt_codigo.Text = string.Format("{0:000}-{1:000}-{2:0000000}", caja.CAJ_NUMERO.ToString().Substring(0, 3), caja.CAJ_NUMERO.ToString().Substring(3), Int16.Parse(txt_codigo.Text));
            }
        }
        private void txt_codigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(Keys.Enter))
                {
                    e.Handled = true;
                    chk_Maratodas.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void gridnotacd_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridnotacd.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                honorarios.Sort(new HonorariosComparer(strColumnName, strSortOrder));
                gridnotacd.DataSource = null;
                gridnotacd.DataSource = honorarios;
                // redimensiona el ancho de la columna con los datos que carga
                //gridMedicos.AutoResizeColumns();
                //gridMedicos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                gridnotacd.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridnotacd.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                gridnotacd.Columns["MED_CODIGO"].HeaderText = "CODIGO";
                gridnotacd.Columns["NOT_RAZON_SOCIAL"].HeaderText = "RAZON SOCIAL";
                gridnotacd.Columns["MED_RUC"].HeaderText = "RUC";
                gridnotacd.Columns["HOM_FACTURA_MEDICO"].HeaderText = "FACTURA";
                gridnotacd.Columns["HOM_FACTURA_FECHA"].HeaderText = "FECHA FAC";
                gridnotacd.Columns["HOM_VALOR_NETO"].HeaderText = "VALOR";
                gridnotacd.Columns["HOM_VALOR_PAGADO"].HeaderText = "VALOR PAGADO";
                gridnotacd.Columns["HOM_COMISION_CLINICA"].HeaderText = "COMISION CLINICA";
                gridnotacd.Columns["HOM_APORTE_LLAMADA"].HeaderText = "VALOR REFERIDO";
                if (comisionreferido == false)
                {
                    gridnotacd.Columns["DIFERENCIA"].HeaderText = "DIFERENCIA";
                    gridnotacd.Columns["HOM_COMISION_CLINICA"].Visible = false;
                    gridnotacd.Columns["HOM_APORTE_LLAMADA"].Visible = false;
                }
                else
                {
                    gridnotacd.Columns["DIFERENCIA"].HeaderText = "VALOR TOTAL";
                    gridnotacd.Columns["HOM_VALOR_NETO"].Visible = false;
                    gridnotacd.Columns["HOM_VALOR_PAGADO"].Visible = false;
                }
                gridnotacd.Columns["CONNOTAD"].HeaderText = " ";
                gridnotacd.Columns["NOT_NUMERO"].HeaderText = "No NOTA DE DEBITO";

                gridnotacd.Columns["HOM_CODIGO"].Visible = false;
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridnotacd, new Point(MousePosition.X - e.X - gridnotacd.Columns[e.ColumnIndex].Width, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Honorarios", gridnotacd.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridnotacd.RowCount - 1; i++)
                    {
                        if (gridnotacd.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridnotacd.CurrentCell = gridnotacd.Rows[i].Cells[columnabuscada];
                        // gridMedicos.Rows[i].Cells[columnabuscada].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            frmReportes frm = new frmReportes();
            frm.reporte = "rNotasDebitoAutomaticas";
            frm.campo1 = label2.Text;
            frm.Show();

            frmReportes frm1 = new frmReportes();
            frm1.reporte = "rNotasDebitoAInforme";
            frm1.campo1 = label2.Text;
            frm1.Show();
        }
        #endregion

        #region MetodosPrivados
        private bool ValidarFormulario()
        {
            bool valido = true;
            bool validagrid = false;
            for (int i = 0; i <= gridnotacd.RowCount - 1; i++)
            {
                if (gridnotacd.Rows[i].Cells[11].Value.ToString() == true.ToString())
                {
                    validagrid = true;
                }
                valido = validagrid;
            }
            if (txt_codigo.Text == null || txt_codigo.Text == string.Empty)
            {
                AgregarError(txt_codigo);
                valido = false;
            }
            
            return valido;
        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        private void GrabarDatos()
        {
            DialogResult resultado;
            HONORARIOS_MEDICOS honorario = new HONORARIOS_MEDICOS();
            int j;
            chk_Maratodas.Focus();
            j = 0;
            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    GENERACIONES_AUTOMATICAS genNuevo = new GENERACIONES_AUTOMATICAS();
                    
                    genNuevo.GEN_CODIGO = int.Parse(label2.Text);
                    genNuevo.GEN_ESTADO = true;
                    genNuevo.GEN_FECHA = DateTime.Parse(txt_ingreso.Text);
                    TIPO_DOCUMENTO tipdocgenera = NegTipoDocumentos.RecuperaTipoDocumentos().Where(cod => cod.TID_CODIGO == 3).FirstOrDefault();
                    genNuevo.TIPO_DOCUMENTOReference.EntityKey = tipdocgenera.EntityKey;
                    USUARIOS usugenera = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == Sesion.codUsuario).FirstOrDefault();
                    genNuevo.USUARIOSReference.EntityKey = usugenera.EntityKey;
                    NegGeneracionesAutomaticas.CrearGeneracionAutomatica(genNuevo);
                    for (int i = 0; i <= gridnotacd.RowCount - 1; i++)
                    {
                        notadebModificada = new NOTAS_CREDITO_DEBITO();
                        honorario = new HONORARIOS_MEDICOS();
                        honorario = new HONORARIOS_MEDICOS();
                        if (gridnotacd.Rows[i].Cells[11].Value.ToString() == true.ToString())
                        {
                            honorario = NegHonorariosMedicos.RecuperaHonorariosMedicos().Where(cod => cod.HOM_CODIGO == int.Parse(gridnotacd.Rows[i].Cells[0].Value.ToString())).FirstOrDefault();
                            notadebModificada.HOM_CODIGO1 = honorario.HOM_CODIGO;
                            MEDICOS medico = NegMedicos.listaMedicos().Where(cod => cod.MED_CODIGO == honorario.MEDICOS.MED_CODIGO).FirstOrDefault();
                            notadebModificada.MED_CODIGO1 = medico.MED_CODIGO;
                            USUARIOS usuModificada = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == Sesion.codUsuario).FirstOrDefault();
                            notadebModificada.USUARIOSReference.EntityKey = usuModificada.EntityKey;
                            CAJAS cajaModificada = NegCajas.ListaCajas().Where(cod => cod.CAJ_CODIGO == caja.CAJ_CODIGO).FirstOrDefault();
                            notadebModificada.CAJASReference.EntityKey = cajaModificada.EntityKey;
                            TIPO_DOCUMENTO tipdocModificada = NegTipoDocumentos.RecuperaTipoDocumentos().Where(cod => cod.TID_CODIGO == 3).FirstOrDefault();
                            notadebModificada.TIPO_DOCUMENTOReference.EntityKey = tipdocModificada.EntityKey;
                            notadebModificada.NOT_FECHA = DateTime.Parse(txt_ingreso.Text); //DateTime.Now;
                            notadebModificada.NOT_RAZON_SOCIAL = medico.MED_APELLIDO_PATERNO + " " + medico.MED_NOMBRE1;
                            notadebModificada.NOT_RUC = medico.MED_RUC;
                            notadebModificada.TID_CODIGO = 3;
                            notadebModificada.NOT_DOCUMENTO_AFECTADO = honorario.HOM_FACTURA_MEDICO;
                            
                            notadebModificada.NOT_DOCUMENTO_TIPO = "F";
                            notadebModificada.FOR_CODIGO1 = null;
                            
                            if (chk_iva.Checked == true && notadebModificada.NOT_VALOR != 0)
                            {
                                decimal valor;
                                valor = notadebModificada.NOT_VALOR / ((100 + Sesion.porIva) / 100);
                                notadebModificada.NOT_IVA = decimal.Parse(String.Format("{0:0.000}", (valor * (Sesion.porIva / 100))));

                            }
                            else
                                notadebModificada.NOT_IVA = 0;

                            //notadebModificada.NOT_IVA = medico.MED_RUC;
                            notadebModificada.NOT_CUENTA_CONTABLE = medico.MED_CUENTA_CONTABLE;
                            if (comisionreferido == false)
                            {
                                if (gridnotacd.Rows[i].Cells[12].Value!=null)
                                    notadebModificada.NOT_MOTIVO_MODIFICACION = gridnotacd.Rows[i].Cells[12].Value.ToString();
                                else
                                    notadebModificada.NOT_MOTIVO_MODIFICACION = "VALORES NO CUBIERTOS";
                                notadebModificada.TNO_CODIGO = HonorariosPAR.codigoTipoNotaDebitoValoresNoCubiertos;
                                notadebModificada.NOT_VALOR = decimal.Parse(honorario.HOM_VALOR_NETO.ToString()) - decimal.Parse(honorario.HOM_VALOR_PAGADO.ToString());
                            }
                            else
                            {
                                if ( gridnotacd.Rows[i].Cells[12].Value!=null)
                                    notadebModificada.NOT_MOTIVO_MODIFICACION = gridnotacd.Rows[i].Cells[12].Value.ToString();
                                else
                                    notadebModificada.NOT_MOTIVO_MODIFICACION = "VALORES DE COMISION Y REFERIDO";
                                notadebModificada.TNO_CODIGO = HonorariosPAR.codigoTipoNotaDebitoComisionesYreferidos;
                                notadebModificada.NOT_VALOR = decimal.Parse(honorario.HOM_COMISION_CLINICA.ToString()) + decimal.Parse(honorario.HOM_APORTE_LLAMADA.ToString());
                            }
                            if (NegNumeroControl.NumerodeControlAutomatico(4))
                            {
                                notadebModificada.NOT_NUMERO = caja.CAJ_NUMERO + string.Format("{0:0000000}", NegNumeroControl.NumeroControlOptine(4)); // int.Parse((NegMedicos.RecuperaMaximoMedicos() + 1).ToString());
                                NegNotaCreditoDebito.CrearNotaDebito(notadebModificada);
                                NegNumeroControl.LiberaNumeroControl(4);
                            }
                            else
                            {
                                notadebModificada.NOT_NUMERO = caja.CAJ_NUMERO + string.Format("{0:0000000}", int.Parse(txt_codigo.Text.Substring(8)) + j); // int.Parse((NegMedicos.RecuperaMaximoMedicos() + 1).ToString());
                                NegNotaCreditoDebito.CrearNotaDebito(notadebModificada);
                            }
                            j = j + 1;
                            gridnotacd.Rows[i].Cells[13].Value = notadebModificada.NOT_NUMERO;

                            if (comisionreferido == false)
                            {
                                NOTAS_CREDITO_DEBITO_DETALLE dnotadeb = new NOTAS_CREDITO_DEBITO_DETALLE();
                                dnotadeb.NCD_CODIGO = NegNotaCreditoDebito.RecuperaDetalleNotaDebitoMaximo() + 1;
                                NOTAS_CREDITO_DEBITO ncd = NegNotaCreditoDebito.ListaNotasCreditoDebito().Where(cod => cod.NOT_NUMERO == notadebModificada.NOT_NUMERO && cod.TID_CODIGO == 3).FirstOrDefault();
                                dnotadeb.NOTAS_CREDITO_DEBITOReference.EntityKey = ncd.EntityKey;
                                dnotadeb.NCD_DESCRIPCION = ncd.NOT_MOTIVO_MODIFICACION;
                                dnotadeb.NCD_VALOR = ncd.NOT_VALOR;
                                NegNotaCreditoDebito.CreaDetalleNotaDebito(dnotadeb);
                            }
                            else
                            {
                                if (honorario.HOM_COMISION_CLINICA != 0)
                                {
                                    NOTAS_CREDITO_DEBITO_DETALLE dnotadeb = new NOTAS_CREDITO_DEBITO_DETALLE();
                                    dnotadeb.NCD_CODIGO = NegNotaCreditoDebito.RecuperaDetalleNotaDebitoMaximo() + 1;
                                    NOTAS_CREDITO_DEBITO ncd = NegNotaCreditoDebito.ListaNotasCreditoDebito().Where(cod => cod.NOT_NUMERO == notadebModificada.NOT_NUMERO && cod.TID_CODIGO==3).FirstOrDefault();
                                    dnotadeb.NOTAS_CREDITO_DEBITOReference.EntityKey = ncd.EntityKey;
                                    dnotadeb.NCD_DESCRIPCION = "COMISION DE CLINICA";
                                    dnotadeb.NCD_VALOR = honorario.HOM_COMISION_CLINICA;
                                    NegNotaCreditoDebito.CreaDetalleNotaDebito(dnotadeb);
                                }
                                if (honorario.HOM_APORTE_LLAMADA != 0)
                                {
                                    NOTAS_CREDITO_DEBITO_DETALLE dnotadeb = new NOTAS_CREDITO_DEBITO_DETALLE();
                                    dnotadeb.NCD_CODIGO = NegNotaCreditoDebito.RecuperaDetalleNotaDebitoMaximo() + 1;
                                    NOTAS_CREDITO_DEBITO ncd = NegNotaCreditoDebito.ListaNotasCreditoDebito().Where(cod => cod.NOT_NUMERO == notadebModificada.NOT_NUMERO && cod.TID_CODIGO == 3).FirstOrDefault();
                                    dnotadeb.NOTAS_CREDITO_DEBITOReference.EntityKey = ncd.EntityKey;
                                    dnotadeb.NCD_DESCRIPCION = "VALOR REFERIDO";
                                    dnotadeb.NCD_VALOR = honorario.HOM_APORTE_LLAMADA;
                                    NegNotaCreditoDebito.CreaDetalleNotaDebito(dnotadeb);
                                }

                            }
                            // ****
                            GENERACIONES_AUTOMATICAS_DETALLE genNuevodet = new GENERACIONES_AUTOMATICAS_DETALLE();
                            genNuevodet.GAD_CODIGO = NegGeneracionesAutomaticasDetalle.RecuperaMaximoDetalle() + 1;
                            genNuevodet.GAD_NUM_DOCUMENTO = notadebModificada.NOT_NUMERO;
                            GENERACIONES_AUTOMATICAS genek = NegGeneracionesAutomaticas.ListaGeneracionesAutomaticas().Where(cod=>cod.GEN_CODIGO==int.Parse(label2.Text)).FirstOrDefault();
                            genNuevodet.GENERACIONES_AUTOMATICASReference.EntityKey = genek.EntityKey;

                            NegGeneracionesAutomaticasDetalle.CrearGeneracionAutomaticaDetalle(genNuevodet);

                            //****
                            //DtoHonorariosMedicos honorarioM = NegHonorariosMedicos.RecuperaHonorariosMedicosFormulario().Where(cod => cod.HOM_CODIGO == honorario.HOM_CODIGO).FirstOrDefault();
                            //DtoHonorariosMedicos honorarioO = honorarioM.ClonarEntidad();
                            //honorarioM.RET_CODIGO = retModificada.RET_CODIGO;

                            //HONORARIOS_MEDICOS hModificado = new HONORARIOS_MEDICOS();
                            //HONORARIOS_MEDICOS hOriginal = new HONORARIOS_MEDICOS();


                            //ATENCIONES atModdifi = NegAtenciones.listaAtenciones().Where(cod => cod.ATE_CODIGO == honorarioM.ATE_CODIGO).FirstOrDefault();
                            //hModificado.ATENCIONESReference.EntityKey = atModdifi.EntityKey;
                            //USUARIOS usuModifi = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == honorario.USUARIOS.ID_USUARIO).FirstOrDefault();
                            //hModificado.USUARIOSReference.EntityKey = usuModifi.EntityKey;
                            //MEDICOS medModifi = NegMedicos.listaMedicos().Where(cod => cod.MED_CODIGO == honorarioM.MED_CODIGO).FirstOrDefault();
                            //hModificado.MEDICOSReference.EntityKey = medModifi.EntityKey;
                            //FORMA_PAGO forModifi = NegFormaPago.listaFormasPago().Where(cod => cod.FOR_CODIGO == honorarioM.FOR_CODIGO).FirstOrDefault();
                            //hModificado.FORMA_PAGOReference.EntityKey = forModifi.EntityKey;
                            //hModificado.RET_CODIGO = honorarioM.RET_CODIGO;
                            //hModificado.HOM_APORTE_LLAMADA = honorarioM.HOM_APORTE_LLAMADA;
                            //hModificado.HOM_CODIGO = honorarioM.HOM_CODIGO;
                            //hModificado.HOM_COMISION_CLINICA = honorarioM.HOM_COMISION_CLINICA;
                            //hModificado.HOM_ESTADO = honorarioM.HOM_ESTADO;
                            //hModificado.HOM_FACTURA_FECHA = honorarioM.HOM_FACTURA_FECHA;
                            //hModificado.HOM_FACTURA_MEDICO = honorarioM.HOM_FACTURA_MEDICO;
                            //hModificado.HOM_FECHA_INGRESO = honorarioM.HOM_FECHA_INGRESO;
                            //hModificado.HOM_RETENCION = honorarioM.HOM_RETENCION;
                            //hModificado.HOM_VALOR_NETO = honorarioM.HOM_VALOR_NETO;
                            //hModificado.HOM_VALOR_TOTAL = honorarioM.HOM_VALOR_TOTAL;
                            //hModificado.EntityKey = new EntityKey(honorarioM.ENTITYSETNAME
                            //    , honorarioM.ENTITYID, honorarioM.HOM_CODIGO);

                            //ATENCIONES atOrig = NegAtenciones.listaAtenciones().Where(cod => cod.ATE_CODIGO == honorarioO.ATE_CODIGO).FirstOrDefault();
                            //hOriginal.ATENCIONESReference.EntityKey = atOrig.EntityKey;
                            //USUARIOS usuOrig = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == honorario.USUARIOS.ID_USUARIO).FirstOrDefault();
                            //hOriginal.USUARIOSReference.EntityKey = usuOrig.EntityKey;
                            //MEDICOS medOrig = NegMedicos.listaMedicos().Where(cod => cod.MED_CODIGO == honorarioO.MED_CODIGO).FirstOrDefault();
                            //hOriginal.MEDICOSReference.EntityKey = medOrig.EntityKey;
                            //FORMA_PAGO forOrig = NegFormaPago.listaFormasPago().Where(cod => cod.FOR_CODIGO == honorarioO.FOR_CODIGO).FirstOrDefault();
                            //hOriginal.FORMA_PAGOReference.EntityKey = forOrig.EntityKey;
                            //hOriginal.RET_CODIGO = honorarioO.RET_CODIGO;
                            //hOriginal.HOM_APORTE_LLAMADA = honorarioO.HOM_APORTE_LLAMADA;
                            //hOriginal.HOM_CODIGO = honorarioO.HOM_CODIGO;
                            //hOriginal.HOM_COMISION_CLINICA = honorarioO.HOM_COMISION_CLINICA;
                            //hOriginal.HOM_ESTADO = honorarioO.HOM_ESTADO;
                            //hOriginal.HOM_FACTURA_FECHA = honorarioO.HOM_FACTURA_FECHA;
                            //hOriginal.HOM_FACTURA_MEDICO = honorarioO.HOM_FACTURA_MEDICO;
                            //hOriginal.HOM_FECHA_INGRESO = honorarioO.HOM_FECHA_INGRESO;
                            //hOriginal.HOM_RETENCION = honorarioO.HOM_RETENCION;
                            //hOriginal.HOM_VALOR_NETO = honorarioO.HOM_VALOR_NETO;
                            //hOriginal.HOM_VALOR_TOTAL = honorarioO.HOM_VALOR_TOTAL;
                            //hOriginal.EntityKey = new EntityKey(honorarioO.ENTITYSETNAME
                            //    , honorarioO.ENTITYID, honorarioO.HOM_CODIGO);

                            //NegHonorariosMedicos.GrabarHonorarioMedico(hModificado, hOriginal);
                        }
                    }


                    HalitarControles(false, false, false, false, true, false, false);
                    MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else 
            {
                MessageBox.Show("Revise los Datos a guardar, debe estar por lo menos una factura marcada", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        protected virtual void HalitarControles(bool datosPrincipales, bool datosSecundarios, bool Modificar, bool Grabar, bool Eliminar, bool Nuevo, bool Cancelar)
        {
            btnNuevo.Enabled = Nuevo;
            btnActualizar.Enabled = Modificar;
            btnImprimir.Enabled = Eliminar;
            btnGuardar.Enabled = Grabar;
            btnCancelar.Enabled = Cancelar;
            grpDatosPrincipales.Enabled = datosPrincipales;

        }
        private void RecuperaHonorariossinRetencion()
        {
            if (comisionreferido == false)
                honorarios = NegHonorariosMedicos.ListaHonorariosPagosMenores().ToList();
            else
                honorarios = NegHonorariosMedicos.ListaHonorariosSinNDComisionesAportes();
            gridnotacd.DataSource = honorarios;
            gridnotacd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            
            gridnotacd.Columns["MED_CODIGO"].HeaderText = "CODIGO";
            gridnotacd.Columns["NOT_RAZON_SOCIAL"].HeaderText = "RAZON SOCIAL";
            gridnotacd.Columns["MED_RUC"].HeaderText = "RUC";
            gridnotacd.Columns["HOM_FACTURA_MEDICO"].HeaderText = "FACTURA";
            gridnotacd.Columns["HOM_FACTURA_FECHA"].HeaderText = "FECHA FAC";
            gridnotacd.Columns["HOM_VALOR_NETO"].HeaderText = "VALOR";
            gridnotacd.Columns["HOM_VALOR_PAGADO"].HeaderText = "VALOR PAGADO";
            gridnotacd.Columns["HOM_COMISION_CLINICA"].HeaderText = "COMISION CLINICA";
            gridnotacd.Columns["HOM_APORTE_LLAMADA"].HeaderText = "VALOR REFERIDO";
            if (comisionreferido == false)
            {
                gridnotacd.Columns["DIFERENCIA"].HeaderText = "DIFERENCIA";
                gridnotacd.Columns["HOM_COMISION_CLINICA"].Visible = false;
                gridnotacd.Columns["HOM_APORTE_LLAMADA"].Visible = false;
            }
            else
            {
                gridnotacd.Columns["DIFERENCIA"].HeaderText = "VALOR TOTAL";
                gridnotacd.Columns["HOM_VALOR_NETO"].Visible = false;
                gridnotacd.Columns["HOM_VALOR_PAGADO"].Visible = false;
            }
            gridnotacd.Columns["CONNOTAD"].HeaderText = " ";
            gridnotacd.Columns["NOT_NUMERO"].HeaderText = "No NOTA DE DEBITO";

            gridnotacd.Columns["HOM_CODIGO"].Visible = false;

        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridnotacd.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridnotacd.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridnotacd.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridnotacd.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridnotacd.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class HonorariosComparer : IComparer<DtoHonorariosNotasDebito>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public HonorariosComparer(string strMemberName, SortOrder sortingOrder)
            {
                memberName = strMemberName;
                sortOrder = sortingOrder;
            }

            /// <summary> 
            /// Compares two Students based on member name and sort order 
            /// and return the result. 
            /// </summary> 
            /// <param name="Student1"></param> 
            /// <param name="Student2"></param> 
            /// <returns></returns> 
            public int Compare(DtoHonorariosNotasDebito Student1, DtoHonorariosNotasDebito Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "MED_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.MED_CODIGO.CompareTo(Student2.MED_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.MED_CODIGO.CompareTo(Student1.MED_CODIGO);
                        }

                        break;
                    case "NOT_RAZON_SOCIAL":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.NOT_RAZON_SOCIAL.CompareTo(Student2.NOT_RAZON_SOCIAL);
                        }
                        else
                        {
                            returnValue = Student2.NOT_RAZON_SOCIAL.CompareTo(Student1.NOT_RAZON_SOCIAL);
                        }
                        break;
                    case "MED_RUC":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.MED_RUC.CompareTo(Student2.MED_RUC);
                        }
                        else
                        {
                            returnValue = Student2.MED_RUC.CompareTo(Student1.MED_RUC);
                        }
                        break;

                    case "HOM_FACTURA_MEDICO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.HOM_FACTURA_MEDICO.CompareTo(Student2.HOM_FACTURA_MEDICO);
                        }
                        else
                        {
                            returnValue = Student2.HOM_FACTURA_MEDICO.CompareTo(Student1.HOM_FACTURA_MEDICO);
                        }
                        break;
                    case "HOM_FACTURA_FECHA":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.HOM_FACTURA_FECHA.CompareTo(Student2.HOM_FACTURA_FECHA);
                        }
                        else
                        {
                            returnValue = Student2.HOM_FACTURA_FECHA.CompareTo(Student1.HOM_FACTURA_FECHA);
                        }
                        break;
                    case "HOM_VALOR_NETO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.HOM_VALOR_NETO.CompareTo(Student2.HOM_VALOR_NETO);
                        }
                        else
                        {
                            returnValue = Student2.HOM_VALOR_NETO.CompareTo(Student1.HOM_VALOR_NETO);
                        }
                        break;
                    case "HOM_VALOR_PAGADO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.HOM_VALOR_PAGADO.CompareTo(Student2.HOM_VALOR_PAGADO);
                        }
                        else
                        {
                            returnValue = Student2.HOM_VALOR_PAGADO.CompareTo(Student1.HOM_VALOR_PAGADO);
                        }
                        break;

                }
                return returnValue;
            }
        }
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        #endregion

        //private void gridnotacd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (gridnotacd.== 7)
        //    { 
        //    }
        //}

        

        
        

        

       

        

        

       

        

        

        


    }
}
