using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;
using Core.Entidades;
using His.Entidades.Clases;
using His.Parametros;

namespace His.Honorarios
{
    public partial class frm_NotaCreditoDebitoHonorario : Form
    {
        #region Variables
        public DtoNotaCreditoDebito notacreddebOriginal = new DtoNotaCreditoDebito();
        public DtoNotaCreditoDebito notacreddebModificada = new DtoNotaCreditoDebito();
        public NOTAS_CREDITO_DEBITO notcdOrigen = new NOTAS_CREDITO_DEBITO();
        public NOTAS_CREDITO_DEBITO notcdModificada = new NOTAS_CREDITO_DEBITO();
        public List<DtoNotaCreditoDebito> notacreditodebito = new List<DtoNotaCreditoDebito>();
        public MEDICOS medicos = new MEDICOS();
        public CAJAS caja = new CAJAS();
        public HONORARIOS_MEDICOS honorario = new HONORARIOS_MEDICOS();
        public Int16 tipodoc;
        public int columnabuscada;
        public bool chkmedico;
        public bool activaiva;
        public bool decm;
        public Int16 tiponota;
        public bool cargafp;
        public List<FORMA_PAGO> formapago = new List<FORMA_PAGO>();
        #endregion

        #region Constructor
        public frm_NotaCreditoDebitoHonorario()
        {
            InitializeComponent();
            cargarDatos();
        }
        #endregion

        #region Eventos
        private void frm_NotaCreditoDebitoHonorario_Load(object sender, EventArgs e)
        {
        }
        private void cargarDatos()
        {
            try
            {
                if (NegValidaciones.localAsignado() == false)
                {
                    frm_AsignaLocal lista = new frm_AsignaLocal();
                    lista.ShowDialog();
                }


                HalitarControles(false, false, false, false, false, true, false, false);
                if (tipodoc == 2)
                {
                    this.Text = "Notas de Crédito";
                    label1.Text = "No Nota Crédito";
                }
                else if (tipodoc == 3)
                {
                    this.Text = "Notas de Débito";
                    label1.Text = "No Nota Débito";
                }
                else if (tipodoc == 5)
                {
                    this.Text = "Nota de Débito Interna";
                    label1.Text = "No NDI";
                }
                else if (tipodoc == 6)
                {
                    this.Text = "Nota Crédito Interna";
                    label1.Text = "No NCI";
                }
                RecuperaNotaCreditoDebito();
                chk_iva.Visible = activaiva;
                txt_iva.Visible = activaiva;
                chk_iva.Checked = false;
                if (activaiva == false)
                    label2.Text = "Medico:";
                //carga los forma pago en el combobox
                cargafp = false;
                formapago = NegFormaPago.listaFormasPago();
                //cmb_formapago.DataSource = formapago;
                //cmb_formapago.ValueMember = "FOR_CODIGO";
                //cmb_formapago.DisplayMember = "FOR_DESCRIPCION";
                var tipfp = NegFormaPago.RecuperaTipoFormaPagos();
                cmb_tipoformapago.DataSource = tipfp;
                cmb_tipoformapago.ValueMember = "TIF_CODIGO";
                cmb_tipoformapago.DisplayMember = "TIF_NOMBRE";
                cargafp = true;

                cargarFormaPago();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
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
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                GrabarDatos();
                
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
        }
        private void gridnotacd_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, false, false, true,false, true,true);
                notacreddebModificada = gridnotacd.CurrentRow.DataBoundItem as DtoNotaCreditoDebito;
                notacreddebOriginal = notacreddebModificada.ClonarEntidad();
                AgregarBindigControles();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HalitarControles(true, true, false, true, false, false, true, false);
                chk_interno.Enabled = false;
                NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();
                ResetearControles();
                caja = NegCajas.ListaCajas().Where(cod => cod.LOCALES.LOC_CODIGO == Sesion.codLocal).FirstOrDefault();
                notacreddebModificada.CAJ_CODIGO = caja.CAJ_CODIGO;
                notacreddebModificada.NOT_FECHA = DateTime.Now;
                //if (tipodoc == 3)
                //{
                if (tipodoc != 5 && tipodoc!=6)
                {
                    if (NegNumeroControl.NumerodeControlAutomatico(tipodoc + 1))
                    {
                        numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == tipodoc + 1).FirstOrDefault();
                        notacreddebModificada.NOT_NUMERO = string.Format("{0:000}-{1:000}-{2:0000000}", caja.CAJ_NUMERO.ToString().Substring(0, 3), caja.CAJ_NUMERO.ToString().Substring(3), Int16.Parse(numerocontrol.NUMCON));
                        //txt_codigo.Text = numerocontrol.NUMCON;
                    }
                    else
                        txt_codigo.ReadOnly = false;
                }
                else
                {
                    notacreddebModificada.NOT_NUMERO = caja.CAJ_NUMERO + string.Format("{0:0000000}", NegNotaCreditoDebito.RecuperaNotaCreditoDebitoMaximo(tipodoc) + 1);
                }
                //}
                //else
                //{ 
                //}
                notacreddebModificada.TID_CODIGO = tipodoc;
                notacreddebModificada.TNO_CODIGO = tiponota;
                AgregarBindigControles();
                txt_medico.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txt_observacion.Focus();
            RecuperaNotaCreditoDebito();
            HalitarControles(false, false, false, false, false, true, false,false);
            ResetearControles();
            chk_interno.Enabled = true;
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txt_medico_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {

                    if (chkmedico == true)
                    {
                        List<MEDICOS> listaMedicos = NegMedicos.listaMedicos();
                        frm_Ayudas lista = new frm_Ayudas(listaMedicos);
                        //lista.consultamedicos = NegMedicos.RecuperaMedicosFormulario();
                        //lista.tabla = "MEDICOS";
                        lista.ShowDialog();
                        if (lista.campoPadre.Text != string.Empty)
                        {
                            string codusuario = lista.campoPadre.Text;
                            medicos = NegMedicos.listaMedicos().Where(cod => cod.MED_CODIGO == Int16.Parse(codusuario)).FirstOrDefault();
                            notacreddebModificada.MED_CODIGO1 = medicos.MED_CODIGO;
                            notacreddebModificada.NOT_RAZON_SOCIAL =  medicos.MED_APELLIDO_PATERNO + " " + medicos.MED_NOMBRE1;
                            notacreddebModificada.NOT_RUC = medicos.MED_RUC;
                            //txt_medico.Text = medicos.MED_CODIGO + " " + medicos.MED_NOMBRE1 + " " + medicos.MED_APELLIDO_PATERNO;
                            AgregarBindigControles();
                            txt_factura.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void txt_factura_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {

                    //if (tipodoc == 3)
                    //{
                    if (chkmedico == true)
                    {
                        frm_Ayudas lista = new frm_Ayudas(NegHonorariosMedicos.ListaHonorariosNoutilizadosNotaDebito(medicos.MED_CODIGO.ToString()).ToList());
                        lista.tabla = "HONORARIOSNONOTADEBITO";
                        //lista.consultaHonorarios = NegHonorariosMedicos.ListaHonorariosNoutilizadosNotaDebito(medicos.MED_CODIGO.ToString()).ToList();
                        lista.ShowDialog();
                        if (lista.campoPadre.Text != string.Empty)
                        {
                            string codusuario = lista.campoPadre.Text;
                            honorario = NegHonorariosMedicos.RecuperaHonorariosMedicos().Where(cod => cod.HOM_CODIGO == int.Parse(codusuario)).FirstOrDefault();
                            notacreddebModificada.NOT_DOCUMENTO_AFECTADO = honorario.HOM_FACTURA_MEDICO;
                            notacreddebModificada.NOT_VALOR = (decimal)honorario.HOM_VALOR_TOTAL;
                            AgregarBindigControles();

                            txt_observacion.Focus();
                        }
                    }
                    //}


                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException!=null)
                    MessageBox.Show(ex.InnerException.Message);
                else
                    MessageBox.Show(ex.Message);
            }
        }
        private void chk_iva_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_iva.Checked == true && txt_monto.Text != "0" && txt_monto.Text != string.Empty)
            {
                decimal valor;
                valor = decimal.Parse(txt_monto.Text) / ((100 + Sesion.porIva) / 100);
                notacreddebModificada.NOT_IVA = decimal.Parse(String.Format("{0:0.000}", (valor * (Sesion.porIva / 100))));
                //txt_iva.Text = String.Format("{0:0.000}",(valor * (Sesion.porIva / 100)));
                AgregarBindigControles();
            }
            else
                txt_iva.Text = "0";
        }
        private void txt_codigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                txt_medico.Focus();
            }
        }
        private void txt_codigo_Leave(object sender, EventArgs e)
        {
            if (txt_codigo.Text != string.Empty && txt_codigo.Text.Length<=7)
            {
                notacreddebModificada.NOT_NUMERO = string.Format("{0:000}-{1:000}-{2:0000000}", caja.CAJ_NUMERO.ToString().Substring(0, 3), caja.CAJ_NUMERO.ToString().Substring(3), Int16.Parse(txt_codigo.Text));
                AgregarBindigControles();
            }
        }
        private void txt_medico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                txt_factura.Focus();
            }
        }
        private void txt_ruc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    txt_factura.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_ruc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txt_ruc.Text != string.Empty)
                {
                    if (NegValidaciones.esCedulaValida(txt_ruc.Text) == false)
                    {

                        MessageBox.Show("Ruc incorrecto ", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_ruc.Focus();
                    }
                    else
                    { 

                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void txt_factura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    txt_monto.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_monto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 46 && decm == false)
            {
                e.Handled = false;
                decm = true;
            }
            else if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar) )
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    txt_observacion.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_monto_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    string ulimodig = txt_monto.Text.Substring(txt_monto.Text.Length - 1);
                    if (ulimodig == ".")
                        decm = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void txt_monto_Enter(object sender, EventArgs e)
        {
            decm = false;
        }
        private void txt_observacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                txt_cuentacontable.Focus();
            }
        }
        private void gridnotacd_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridnotacd.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                notacreditodebito.Sort(new NotaDCComparer(strColumnName, strSortOrder));
                gridnotacd.DataSource = null;
                gridnotacd.DataSource = notacreditodebito;
                // redimensiona el ancho de la columna con los datos que carga
                //gridMedicos.AutoResizeColumns();
                //gridMedicos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                gridnotacd.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridnotacd.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                gridnotacd.Columns["NOT_NUMERO"].HeaderText = "No NOTA";
                gridnotacd.Columns["NOT_RAZON_SOCIAL"].HeaderText = "MEDICO";
                gridnotacd.Columns["NOT_FECHA"].HeaderText = "FECHA";
                gridnotacd.Columns["NOT_RUC"].HeaderText = "RUC";
                gridnotacd.Columns["NOT_DOCUMENTO_AFECTADO"].HeaderText = "FACTURA";
                gridnotacd.Columns["NOT_VALOR"].HeaderText = "VALOR";
                gridnotacd.Columns["NOT_IVA"].Visible = false;
                gridnotacd.Columns["CAJ_CODIGO"].Visible = false;
                gridnotacd.Columns["ID_USUARIO"].Visible = false;
                gridnotacd.Columns["NOT_DOCUMENTO_TIPO"].Visible = false;
                gridnotacd.Columns["NOT_MOTIVO_MODIFICACION"].Visible = false;
                gridnotacd.Columns["TID_CODIGO"].Visible = false;
                gridnotacd.Columns["NOT_CANCELADO"].Visible = false;
                gridnotacd.Columns["NOT_CUENTA_CONTABLE"].Visible = false;
                gridnotacd.Columns["NOT_ANULADO"].Visible = false;
                gridnotacd.Columns["TNO_CODIGO"].Visible = false;
                gridnotacd.Columns["HOM_CODIGO1"].Visible = false;
                gridnotacd.Columns["MED_CODIGO1"].Visible = false;
                gridnotacd.Columns["FOR_CODIGO1"].Visible = false;
                gridnotacd.Columns["ENTITYSETNAME"].Visible = false;
                gridnotacd.Columns["ENTITYID"].Visible = false;
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridnotacd, new Point(MousePosition.X - this.Left - gridnotacd.Columns[e.ColumnIndex].Width, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Notas", gridnotacd.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridnotacd.RowCount - 1; i++)
                    {
                        if (gridnotacd.Rows[i].Cells[columnabuscada].Value!=null)
                        {
                            if (gridnotacd.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                                gridnotacd.CurrentCell = gridnotacd.Rows[i].Cells[columnabuscada];
                        }
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
            if (tipodoc != 5 && tipodoc != 6)
            {
                frm.reporte = "rNotaCreditoDebito";
                if (txt_codigo.Text.Length > 13)
                    frm.campo1 = txt_codigo.Text.Substring(0, 3) + txt_codigo.Text.Substring(4, 3) + txt_codigo.Text.Substring(8);
                else
                    frm.campo1 = txt_codigo.Text;
                frm.campo2 = tipodoc.ToString();
            }
            else 
            {
                frm.reporte = "rNotaCreditoDebitoInterna";
                if (txt_codigo.Text.Length > 13)
                    frm.campo1 = txt_codigo.Text.Substring(0, 3) + txt_codigo.Text.Substring(4, 3) + txt_codigo.Text.Substring(8);
                else
                    frm.campo1 = txt_codigo.Text;
                frm.campo2 = tipodoc.ToString();
                if (tipodoc == 6)
                    frm.campo3 = "NOTA DE CREDITO INTERNA";
                else if (tipodoc == 5)
                    frm.campo3 = "NOTA DE DEBITO INTERNA";
            }
            frm.Show();
        }
        private void txt_monto_Leave(object sender, EventArgs e)
        {
            try
            {
                if (tipodoc == 3)
                {
                    if (txt_factura.Text != string.Empty && honorario.HOM_VALOR_TOTAL!=0)
                    {
                        if (decimal.Parse(txt_monto.Text) > honorario.HOM_VALOR_TOTAL)
                        {
                            MessageBox.Show("Para Notas de Debito el Valor no puede ser mayor al de la factura afectada", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_monto.Focus();
                        }

                    }
                }
                if (txt_monto.Text != string.Empty)
                {
                    txt_monto.Text = String.Format("{0:0.00}", decimal.Parse(txt_monto.Text));
                }
            }
            catch (Exception ex)
            {
                if(ex.InnerException!=null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult resultado;

                resultado = MessageBox.Show("Desea Anular la Retención?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    //NOTAS_CREDITO_DEBITO ndmodif = new NOTAS_CREDITO_DEBITO();
                    //NOTAS_CREDITO_DEBITO ndorigi = new NOTAS_CREDITO_DEBITO();
                    //ndmodif = NegNotaCreditoDebito.ListaNotasCreditoDebito().Where(cod => cod.NOT_NUMERO == notacreddebModificada.NOT_NUMERO).FirstOrDefault();
                    //ndorigi = ndmodif.ClonarEntidad();
                    //ndmodif.NOT_ANULADO = true;
                    //NegNotaCreditoDebito.EliminarNotaDebito(ndorigi.ClonarEntidad());

                    
                    notcdModificada.NOT_NUMERO = notacreddebModificada.NOT_NUMERO;
                    notcdModificada.TID_CODIGO = notacreddebModificada.TID_CODIGO;
                    notcdModificada.TNO_CODIGO = notacreddebModificada.TNO_CODIGO;
                    USUARIOS usuModificada = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == notacreddebModificada.ID_USUARIO).FirstOrDefault();
                    notcdModificada.USUARIOSReference.EntityKey = usuModificada.EntityKey;
                    CAJAS cajaModificada = NegCajas.ListaCajas().Where(cod => cod.CAJ_CODIGO == notacreddebModificada.CAJ_CODIGO).FirstOrDefault();
                    notcdModificada.CAJASReference.EntityKey = cajaModificada.EntityKey;
                    TIPO_DOCUMENTO tipdModificada = NegTipoDocumentos.RecuperaTipoDocumentos().Where(cod => cod.TID_CODIGO == notacreddebModificada.TID_CODIGO).FirstOrDefault();
                    notcdModificada.TIPO_DOCUMENTOReference.EntityKey = tipdModificada.EntityKey;
                    notcdModificada.NOT_ANULADO = true;
                    notcdModificada.NOT_CUENTA_CONTABLE = notacreddebModificada.NOT_CUENTA_CONTABLE;
                    notcdModificada.NOT_DOCUMENTO_AFECTADO = notacreddebModificada.NOT_DOCUMENTO_AFECTADO;
                    notcdModificada.NOT_DOCUMENTO_TIPO = notacreddebModificada.NOT_DOCUMENTO_TIPO;
                    notcdModificada.NOT_FECHA = notacreddebModificada.NOT_FECHA;
                    notcdModificada.NOT_IVA = notacreddebModificada.NOT_IVA;
                    notcdModificada.NOT_MOTIVO_MODIFICACION = notacreddebModificada.NOT_MOTIVO_MODIFICACION;
                    notcdModificada.NOT_RAZON_SOCIAL = notacreddebModificada.NOT_RAZON_SOCIAL;
                    notcdModificada.NOT_RUC = notacreddebModificada.NOT_RUC;
                    notcdModificada.NOT_VALOR = notacreddebModificada.NOT_VALOR;
                    notcdModificada.NOT_CANCELADO = notacreddebModificada.NOT_CANCELADO;
                    notcdModificada.MED_CODIGO1 = notacreddebModificada.MED_CODIGO1;
                    notcdModificada.FOR_CODIGO1 = notacreddebModificada.FOR_CODIGO1;
                    if (honorario.HOM_CODIGO != 0)
                        notcdModificada.HOM_CODIGO1 = honorario.HOM_CODIGO;
                    else
                        notcdModificada.HOM_CODIGO1 = null;
                    //notcdModificada.EntityKey= new EntityKey(notacreddebModificada.ENTITYSETNAME
                    //        , "NOT_NUMERO", notacreddebModificada.NOT_NUMERO);
                    //notcdModificada.EntityKey = new EntityKey(notacreddebModificada.ENTITYSETNAME
                    //        , "TID_CODIGO", notacreddebModificada.TID_CODIGO);
                    EntityKeyMember[] ekm = new EntityKeyMember[] { new EntityKeyMember("NOT_NUMERO", notacreddebModificada.NOT_NUMERO), new EntityKeyMember("TID_CODIGO", notacreddebModificada.TID_CODIGO) };
                    notcdModificada.EntityKey = new EntityKey(notacreddebModificada.ENTITYSETNAME, ekm);
                    //notcdModificada.EntityKey.EntitySetName = notacreddebModificada.ENTITYSETNAME;
                    //notcdModificada.EntityKey.EntityKeyValues[0].Key = "NOT_NUMERO";
                    //notcdModificada.EntityKey.EntityKeyValues[0].Value = notacreddebModificada.NOT_NUMERO;
                    //notcdModificada.EntityKey.EntityKeyValues[1].Key = "TID_CODIGO";
                    //notcdModificada.EntityKey.EntityKeyValues[1].Value = notacreddebModificada.TID_CODIGO;

                    notcdOrigen.NOT_NUMERO = notacreddebOriginal.NOT_NUMERO;
                    notcdOrigen.TID_CODIGO = notacreddebOriginal.TID_CODIGO;
                    notcdOrigen.TNO_CODIGO = notacreddebOriginal.TNO_CODIGO;
                    USUARIOS usuOriginal = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == notacreddebOriginal.ID_USUARIO).FirstOrDefault();
                    notcdOrigen.USUARIOSReference.EntityKey = usuOriginal.EntityKey;
                    CAJAS cajaOriginal = NegCajas.ListaCajas().Where(cod => cod.CAJ_CODIGO == notacreddebOriginal.CAJ_CODIGO).FirstOrDefault();
                    notcdOrigen.CAJASReference.EntityKey = cajaOriginal.EntityKey;
                    TIPO_DOCUMENTO tipdOriginal = NegTipoDocumentos.RecuperaTipoDocumentos().Where(cod => cod.TID_CODIGO == notacreddebOriginal.TID_CODIGO).FirstOrDefault();
                    notcdOrigen.TIPO_DOCUMENTOReference.EntityKey = tipdOriginal.EntityKey;
                    notcdOrigen.NOT_CANCELADO = notacreddebOriginal.NOT_CANCELADO;
                    notcdOrigen.NOT_CUENTA_CONTABLE = notacreddebOriginal.NOT_CUENTA_CONTABLE;
                    notcdOrigen.NOT_DOCUMENTO_AFECTADO = notacreddebOriginal.NOT_DOCUMENTO_AFECTADO;
                    notcdOrigen.NOT_DOCUMENTO_TIPO = notacreddebOriginal.NOT_DOCUMENTO_TIPO;
                    notcdOrigen.NOT_FECHA = notacreddebOriginal.NOT_FECHA;
                    notcdOrigen.NOT_IVA = notacreddebOriginal.NOT_IVA;
                    notcdOrigen.NOT_MOTIVO_MODIFICACION = notacreddebOriginal.NOT_MOTIVO_MODIFICACION;
                    notcdOrigen.NOT_RAZON_SOCIAL = notacreddebOriginal.NOT_RAZON_SOCIAL;
                    notcdOrigen.NOT_RUC = notacreddebOriginal.NOT_RUC;
                    notcdOrigen.NOT_VALOR = notacreddebOriginal.NOT_VALOR;
                    notcdOrigen.NOT_ANULADO = notacreddebOriginal.NOT_ANULADO;
                    notcdOrigen.MED_CODIGO1 = notacreddebOriginal.MED_CODIGO1;
                    notcdOrigen.FOR_CODIGO1 = notacreddebOriginal.FOR_CODIGO1;
                    notcdOrigen.HOM_CODIGO1 = notacreddebOriginal.HOM_CODIGO1;
                    //notcdOrigen.EntityKey = new EntityKey(notacreddebOriginal.ENTITYSETNAME
                    //        , "NOT_NUMERO", notacreddebOriginal.NOT_NUMERO);
                    //notcdOrigen.EntityKey. = new EntityKey(notacreddebOriginal.ENTITYSETNAME
                    //        , "TID_CODIGO", notacreddebOriginal.TID_CODIGO);
                    EntityKeyMember[] ekmo = new EntityKeyMember[] { new EntityKeyMember("NOT_NUMERO", notacreddebOriginal.NOT_NUMERO), new EntityKeyMember("TID_CODIGO", notacreddebOriginal.TID_CODIGO) };
                    notcdOrigen.EntityKey = new EntityKey(notacreddebOriginal.ENTITYSETNAME, ekmo);

                    NegNotaCreditoDebito.GrabarNotaDebito(notcdModificada, notcdOrigen);


                    RecuperaNotaCreditoDebito();
                    ResetearControles();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true, false, false);
                    MessageBox.Show("Datos Eliminados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Operación Invalida", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txt_factura_Leave(object sender, EventArgs e)
        {
            if (txt_factura.Text.Replace(" ","").Replace("-","") != string.Empty)
            {
                string factura = txt_factura.Text.Replace("-", "");
                factura = factura.Replace(" ", "");
                if (factura.Length > 7)
                    factura = factura.Substring(6);

                if (medicos != null)
                {
                    //if (medicos.MED_FACTURA_INICIAL != null && medicos.MED_FACTURA_INICIAL != string.Empty)

                    //    if (int.Parse(medicos.MED_FACTURA_INICIAL.Substring(6)) <= int.Parse(factura) && int.Parse(medicos.MED_FACTURA_FINAL.Substring(6)) >= int.Parse(factura))
                    //        txt_factura.Text = medicos.MED_FACTURA_INICIAL.Substring(0, 3) + medicos.MED_FACTURA_INICIAL.Substring(3, 3) + string.Format("{0:0000000}", int.Parse(factura));
                    //    else
                    //    {
                    //        MessageBox.Show("Factura incorrecta, no se encuentra dentro del rango de Facturas del Medico.", "Medicos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        txt_factura.Focus();
                    //    }
                    //else
                    //{
                    //    if (factura.Length < 13)
                    //        txt_factura.Text = "001-001-" + string.Format("{0:0000000}", int.Parse(factura));

                    //}
                    var hon = NegHonorariosMedicos.RecuperaHonorariosMedicos().Where(cod => cod.MEDICOS.MED_CODIGO == medicos.MED_CODIGO).ToList();
                    if (hon != null)
                    {
                        honorario = hon.Where(cd => cd.HOM_FACTURA_MEDICO == factura).FirstOrDefault();
                        if (honorario==null)
                            txt_factura.Text = "001-001-" + string.Format("{0:0000000}", int.Parse(factura));

                    }
                    else if (factura.Length == 13)
                    {
                        notacreddebModificada.NOT_DOCUMENTO_AFECTADO = txt_factura.Text.Replace("-", "");//txt_factura.Text.Substring(0, 3) + txt_factura.Text.Substring(4, 3) + txt_factura.Text.Substring(8);
                    }
                    else
                    {
                        txt_factura.Text = "001-001-" + string.Format("{0:0000000}", int.Parse(factura));
                    }
                }

                else
                {
                    if (factura.Length < 13)
                        txt_factura.Text = "001-001-" + string.Format("{0:0000000}", int.Parse(factura));
                }
                notacreddebModificada.NOT_DOCUMENTO_AFECTADO = txt_factura.Text.Replace("-", "");//txt_factura.Text.Substring(0, 3) + txt_factura.Text.Substring(4, 3) + txt_factura.Text.Substring(8);

            }

        }
        private void txt_medico_Leave(object sender, EventArgs e)
        {
            if ((txt_medico.Text != "0" && txt_medico.Text != string.Empty) && medicos.MED_CODIGO == 0)
            {
                medicos = NegMedicos.listaMedicos().Where(cod => cod.MED_CODIGO == int.Parse(txt_medico.Text)).FirstOrDefault();
                if (medicos == null)
                {
                    MessageBox.Show("Médico no Existente", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_medico.Text = string.Empty;
                    txt_medico.Focus();
                }
                else
                {
                    notacreddebModificada.MED_CODIGO1 = medicos.MED_CODIGO;
                    notacreddebModificada.NOT_RAZON_SOCIAL = medicos.MED_APELLIDO_PATERNO + " " + medicos.MED_NOMBRE1;
                    notacreddebModificada.NOT_RUC = medicos.MED_RUC;
                    //txt_medico.Text = medicos.MED_CODIGO + " " + medicos.MED_NOMBRE1 + " " + medicos.MED_APELLIDO_PATERNO;
                    AgregarBindigControles();
                    txt_factura.Focus();
                }

            }
        }
        private void chk_interno_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_interno.Checked == true && tipodoc == 3)
            {
                tipodoc = 5;
                tiponota = HonorariosPAR.codigoTipoNotaDebitoInterna;
            }
            else if (chk_interno.Checked == true && tipodoc == 2)
            {
                tipodoc = 6;
                tiponota = HonorariosPAR.codigoTipoNotaCreditoInterna;
            }
            else if (chk_interno.Checked == false && tipodoc == 6)
            {
                tipodoc = 2;
                tiponota = HonorariosPAR.codigoTipoNotaCredito;
            }
            else if (chk_interno.Checked == false && tipodoc == 5)
            {
                tipodoc = 3;
                tiponota = HonorariosPAR.codigoTipoNotaDebito;
            }
            RecuperaNotaCreditoDebito();

        }
        private void txt_cuentacontable_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    cmb_tipoformapago.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void cmb_tipoformapago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmb_tipoformapago.SelectedIndex >= 0 && cargafp==true)
                {
                    var formap = NegFormaPago.listaFormasPago().Where(cod => cod.TIPO_FORMA_PAGO.TIF_CODIGO ==Int16.Parse(cmb_tipoformapago.SelectedValue.ToString())).ToList();
                    cmb_formapago.DataSource = formap;
                    cmb_formapago.ValueMember = "FOR_CODIGO";
                    cmb_formapago.DisplayMember = "FOR_DESCRIPCION";
                    
                }
                else
                {
                    //formaPago = null;
                    //cboFiltroFormaPago.SelectedIndex = 0;
                    //cboFiltroFormaPago.Enabled = false;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error");
            }
        }
       
        #endregion

        #region Metodos Privados
        private void AgregarBindigControles()
        {
            Binding NOT_NUMERO = new Binding("Text", notacreddebModificada, "NOT_NUMERO", true);
            Binding NOT_RAZON_SOCIAL = new Binding("Text", notacreddebModificada, "NOT_RAZON_SOCIAL", true);
            Binding NOT_FECHA = new Binding("Text", notacreddebModificada, "NOT_FECHA", true);
            Binding NOT_RUC = new Binding("Text", notacreddebModificada, "NOT_RUC", true);
            Binding MED_CODIGO1 = new Binding("Text", notacreddebModificada, "MED_CODIGO1", true);
            //Binding NOT_DOCUMENTO_AFECTADO = new Binding("Text", notacreddebModificada, "NOT_DOCUMENTO_AFECTADO", true);
            Binding NOT_MOTIVO_MODIFICACION = new Binding("Text", notacreddebModificada, "NOT_MOTIVO_MODIFICACION", true);
            Binding NOT_VALOR = new Binding("text", notacreddebModificada, "NOT_VALOR", true);
            Binding NOT_IVA = new Binding("text", notacreddebModificada, "NOT_IVA", true);
            Binding NOT_CUENTA_CONTABLE = new Binding("text", notacreddebModificada, "NOT_CUENTA_CONTABLE", true);
            Binding FOR_CODIGO = new Binding("SelectedValue", notacreddebModificada, "FOR_CODIGO1", true);
            txt_codigo.DataBindings.Clear();
            txt_medico.DataBindings.Clear();
            txt_ingreso.DataBindings.Clear();

            txt_ruc.DataBindings.Clear();
            txt_nombre.DataBindings.Clear();
            txt_monto.DataBindings.Clear();
            txt_iva.DataBindings.Clear();
            txt_observacion.DataBindings.Clear();
            txt_cuentacontable.DataBindings.Clear();
            chk_iva.DataBindings.Clear();
            cmb_formapago.DataBindings.Clear();

            txt_codigo.DataBindings.Add(NOT_NUMERO);
            txt_medico.DataBindings.Add(MED_CODIGO1);
            txt_ingreso.DataBindings.Add(NOT_FECHA);
            txt_ruc.DataBindings.Add(NOT_RUC);
            txt_nombre.DataBindings.Add(NOT_RAZON_SOCIAL);
            txt_monto.DataBindings.Add(NOT_VALOR);
            txt_iva.DataBindings.Add(NOT_IVA);
            txt_observacion.DataBindings.Add(NOT_MOTIVO_MODIFICACION);
            txt_cuentacontable.DataBindings.Add(NOT_CUENTA_CONTABLE);
            if (notacreddebModificada.NOT_IVA != 0)
                chk_iva.Checked = true;
            else
                chk_iva.Checked = false;
            txt_factura.Text = notacreddebModificada.NOT_DOCUMENTO_AFECTADO;
            if (notacreddebModificada.FOR_CODIGO1 == 0)
            {
                cmb_tipoformapago.SelectedValue = -1;
                cmb_formapago.SelectedValue = -1;
            }
            var formap = formapago.Where(cod => cod.FOR_CODIGO == notacreddebModificada.FOR_CODIGO1).FirstOrDefault();
            if (formap != null)
            {
                cmb_tipoformapago.SelectedValue = formap.TIPO_FORMA_PAGO.TIF_CODIGO;
                cmb_formapago.DataBindings.Add(FOR_CODIGO);
            }

        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            notacreddebOriginal = new DtoNotaCreditoDebito();
            notacreddebModificada = new DtoNotaCreditoDebito();
            notcdOrigen = new NOTAS_CREDITO_DEBITO();
            notcdModificada = new NOTAS_CREDITO_DEBITO();

            medicos = new MEDICOS();
            honorario = new HONORARIOS_MEDICOS();
            txt_codigo.Text = string.Empty;
            txt_medico.Text = string.Empty;
            txt_factura.Text = string.Empty;
            txt_ingreso.Text = string.Empty;
            txt_monto.Text = string.Empty;
            txt_iva.Text = string.Empty;
            txt_nombre.Text = string.Empty;
            txt_ruc.Text = string.Empty;
            txt_cuentacontable.Text = string.Empty;
            txt_observacion.Text = string.Empty;
            chk_iva.Checked = true;
            cmb_tipoformapago.SelectedValue = -1;
            cmb_formapago.SelectedValue = -1;
        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (notacreddebModificada.NOT_NUMERO == null || notacreddebModificada.NOT_NUMERO == string.Empty)
            {
                AgregarError(txt_codigo);
                valido = false;
            }
            if (notacreddebModificada.NOT_RAZON_SOCIAL == null || notacreddebModificada.NOT_RAZON_SOCIAL == string.Empty)
            {
                AgregarError(txt_medico);
                valido = false;
            }
            if (notacreddebModificada.NOT_RUC == null || notacreddebModificada.NOT_RUC == string.Empty)
            {
                AgregarError(txt_ruc);
                valido = false;
            }
            if (notacreddebModificada.NOT_VALOR == null || notacreddebModificada.NOT_VALOR == 0)
            {
                AgregarError(txt_monto);
                valido = false;
            }
            return valido;

        }
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridnotacd.Focus();
            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    notcdModificada = new NOTAS_CREDITO_DEBITO();
                    notcdModificada.NOT_NUMERO = notacreddebModificada.NOT_NUMERO;
                    USUARIOS usuModificada =NegUsuarios.RecuperaUsuarios().Where(cod=>cod.ID_USUARIO==Sesion.codUsuario).FirstOrDefault();
                    notcdModificada.USUARIOSReference.EntityKey = usuModificada.EntityKey;
                    CAJAS cajaModificada = NegCajas.ListaCajas().Where(cod => cod.CAJ_CODIGO == caja.CAJ_CODIGO).FirstOrDefault();
                    notcdModificada.CAJASReference.EntityKey = cajaModificada.EntityKey;
                    TIPO_DOCUMENTO tipdModificada = NegTipoDocumentos.RecuperaTipoDocumentos().Where(cod => cod.TID_CODIGO == notacreddebModificada.TID_CODIGO).FirstOrDefault();
                    notcdModificada.TIPO_DOCUMENTOReference.EntityKey = tipdModificada.EntityKey;
                    notcdModificada.TID_CODIGO = notacreddebModificada.TID_CODIGO;
                    notcdModificada.TNO_CODIGO = notacreddebModificada.TNO_CODIGO;
                    notcdModificada.NOT_CANCELADO = notacreddebModificada.NOT_CANCELADO;
                    notcdModificada.NOT_CUENTA_CONTABLE = notacreddebModificada.NOT_CUENTA_CONTABLE;
                    notcdModificada.NOT_DOCUMENTO_AFECTADO = notacreddebModificada.NOT_DOCUMENTO_AFECTADO;
                    if (txt_factura.Text.Replace("-","").Replace(" ","") != string.Empty)
                        notcdModificada.NOT_DOCUMENTO_TIPO = "F";
                    else
                        notcdModificada.NOT_DOCUMENTO_TIPO = notacreddebModificada.NOT_DOCUMENTO_TIPO;
                    notcdModificada.NOT_FECHA = notacreddebModificada.NOT_FECHA;
                    notcdModificada.NOT_IVA = notacreddebModificada.NOT_IVA;
                    notcdModificada.NOT_MOTIVO_MODIFICACION = notacreddebModificada.NOT_MOTIVO_MODIFICACION;
                    notcdModificada.NOT_RAZON_SOCIAL = notacreddebModificada.NOT_RAZON_SOCIAL;
                    notcdModificada.NOT_RUC = notacreddebModificada.NOT_RUC;
                    notcdModificada.NOT_VALOR = notacreddebModificada.NOT_VALOR;
                    notcdModificada.MED_CODIGO1 = notacreddebModificada.MED_CODIGO1;
                    if (cmb_formapago.SelectedValue != null)
                        notcdModificada.FOR_CODIGO1 = Int16.Parse(cmb_formapago.SelectedValue.ToString());
                    else
                        notcdModificada.FOR_CODIGO1 = null;
                    if (honorario!= null)
                        notcdModificada.HOM_CODIGO1 = honorario.HOM_CODIGO;
                    else
                        notcdModificada.HOM_CODIGO1 = null;
                    if (notacreddebOriginal.CAJ_CODIGO == 0)
                    {
                        if (tipodoc != 5 && tipodoc!=6)
                        {
                            if (NegNumeroControl.NumerodeControlAutomatico(tipodoc + 1))
                            {
                                notcdModificada.NOT_NUMERO = caja.CAJ_NUMERO + string.Format("{0:0000000}", NegNumeroControl.NumeroControlOptine(tipodoc + 1)); // int.Parse((NegMedicos.RecuperaMaximoMedicos() + 1).ToString());
                                NegNotaCreditoDebito.CrearNotaDebito(notcdModificada);
                                NegNumeroControl.LiberaNumeroControl(tipodoc + 1);
                            }
                            else
                            {
                                notcdModificada.NOT_NUMERO = txt_codigo.Text.Substring(0, 3) + txt_codigo.Text.Substring(4, 3) + txt_codigo.Text.Substring(8);
                                NegNotaCreditoDebito.CrearNotaDebito(notcdModificada);
                            }
                        }
                        else
                        {
                            notcdModificada.NOT_NUMERO = caja.CAJ_NUMERO + string.Format("{0:0000000}", NegNotaCreditoDebito.RecuperaNotaCreditoDebitoMaximo(tipodoc)+1);
                            NegNotaCreditoDebito.CrearNotaDebito(notcdModificada);
                        }

                    }
                    //else
                    //{
                    //    notcdModificada.EntityKey = new EntityKey(notacreditodebito.First().ENTITYSETNAME
                    //    , notacreditodebito.First().ENTITYID, notacreddebModificada.CAJ_CODIGO);

                    //    NegTipoDocumentos.GrabarTipoDocumento(tipodocModificada, tipodocOriginal);

                    //}

                    NOTAS_CREDITO_DEBITO_DETALLE dnotadeb = new NOTAS_CREDITO_DEBITO_DETALLE();
                    dnotadeb.NCD_CODIGO = NegNotaCreditoDebito.RecuperaDetalleNotaDebitoMaximo() + 1;
                    NOTAS_CREDITO_DEBITO ncd = NegNotaCreditoDebito.ListaNotasCreditoDebito().Where(cod => cod.NOT_NUMERO == notcdModificada.NOT_NUMERO && cod.TID_CODIGO==tipodoc).FirstOrDefault();
                    dnotadeb.NOTAS_CREDITO_DEBITOReference.EntityKey = ncd.EntityKey;
                    dnotadeb.NCD_DESCRIPCION = ncd.NOT_MOTIVO_MODIFICACION;
                    dnotadeb.NCD_VALOR = ncd.NOT_VALOR;
                    NegNotaCreditoDebito.CreaDetalleNotaDebito(dnotadeb);

                    //Guardo el honorario tipo nota de debito 
                    ////////HONORARIOS_MEDICOS nuevoHonorario = new HONORARIOS_MEDICOS();

                    ////////nuevoHonorario.HOM_CODIGO = Convert.ToInt32(ultimoCodigoHonorario() + 1);
                    ////////nuevoHonorario.ATENCIONESReference.EntityKey = NegAtenciones.AtencionID(atencionDtoActual.ATE_CODIGO).EntityKey;

                    ////////int codMedico = Convert.ToInt32(notacreddebModificada.MED_CODIGO1);
                    ////////nuevoHonorario.MEDICOSReference.EntityKey = NegMedicos.MedicoID(codMedico).EntityKey;

                    ////////KeyValuePair<int, string> item = (KeyValuePair<int, string>)cboFiltroFormaPago.SelectedItem;
                    ////////Int16 codigoPago = (Int16)(item.Key);
                    ////////String descPago = (String)(item.Value);

                    ////////nuevoHonorario.FORMA_PAGOReference.EntityKey = NegFormaPago.FormaPagoID(codigoPago).EntityKey;

                    ////////nuevoHonorario.HOM_FECHA_INGRESO = System.DateTime.Now;

                    ////////bool flag = false;
                    ////////if (formaPago.FOR_CODIGO == His.Parametros.HonorariosPAR.getCodigoFPHonorarioDirecto() && txtFacturaMedico.Text == "   -   -")
                    ////////{
                    ////////    txtFacturaMedico.Text = NegNumeroControl.NumeroControlOp(7);
                    ////////    flag = true;
                    ////////}

                    ////////nuevoHonorario.HOM_FACTURA_MEDICO = txtFacturaMedico.Text.Replace("-", string.Empty);
                    ////////nuevoHonorario.HOM_FACTURA_FECHA = dateTimePickerFecha.Value;
                    ////////nuevoHonorario.HOM_VALOR_NETO = Convert.ToDecimal(txtValorNeto.Text.ToString());
                    ////////nuevoHonorario.HOM_COMISION_CLINICA = Convert.ToDecimal(txtComisionClinica.Text.ToString());
                    ////////nuevoHonorario.HOM_APORTE_LLAMADA = Convert.ToDecimal(txtAporteMedLLam.Text.ToString());
                    ////////nuevoHonorario.HOM_RETENCION = Convert.ToDecimal(txtRetencion.Text.ToString());
                    ////////nuevoHonorario.HOM_VALOR_TOTAL = Convert.ToDecimal(txtValorTotal.Text.ToString());
                    ////////nuevoHonorario.HOM_ESTADO = 0;
                    ////////nuevoHonorario.HOM_VALOR_PAGADO = 0;
                    ////////nuevoHonorario.HOM_VALOR_CANCELADO = 0;

                    ////////if (txtLote.Visible == true)
                    ////////    nuevoHonorario.HOM_LOTE = txtLote.Text.ToString();
                    ////////if (txtObservacion.Text == string.Empty)
                    ////////    nuevoHonorario.HOM_OBSERVACION = null;
                    ////////else
                    ////////    nuevoHonorario.HOM_OBSERVACION = txtObservacion.Text.ToString();

                    ////////nuevoHonorario.USUARIOSReference.EntityKey = NegUsuarios.RecuperaUsuario(His.Entidades.Clases.Sesion.codUsuario).EntityKey;

                    ////////NegHonorariosMedicos.CrearHonorarioMedico(nuevoHonorario);

                    ////////if (flag == true)
                    ////////    NegNumeroControl.LiberaNumeroControl(7);


                    ////

                    RecuperaNotaCreditoDebito();
                    HalitarControles(false, false, false, false, true, false, true,false);
                    MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public Int64 ultimoCodigoHonorario()
        {
            return NegHonorariosMedicos.ultimoCodigoHonorarios();
        }

        /// <summary>
        /// funcion para habilitar los botones
        /// </summary>
        /// <param name="control">boton que se acciona</param>
        protected virtual void HalitarControles(bool datosPrincipales, bool datosSecundarios, bool Modificar, bool Grabar, bool Imprimir, bool Nuevo, bool Cancelar, bool Eliminar)
        {
            btnNuevo.Enabled = Nuevo;
            btnActualizar.Enabled = Modificar;
            btnImprimir.Enabled = Imprimir;
            btnGuardar.Enabled = Grabar;
            btnCancelar.Enabled = Cancelar;
            grpDatosPrincipales.Enabled = datosPrincipales;
            btnEliminar.Enabled = Eliminar;
            pictureBox1.Visible = Cancelar;
            pictureBox2.Visible = Cancelar;
        }
        private void RecuperaNotaCreditoDebito()
        {
            
                notacreditodebito = NegNotaCreditoDebito.RecuperaNotasCreditoDebito().Where(cod=>cod.TID_CODIGO==tipodoc).Where(an=>an.NOT_ANULADO==false).ToList();
                gridnotacd.DataSource = notacreditodebito;
                gridnotacd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                gridnotacd.Columns["NOT_NUMERO"].HeaderText = "No NOTA";
                gridnotacd.Columns["NOT_RAZON_SOCIAL"].HeaderText = "MEDICO";
                gridnotacd.Columns["NOT_FECHA"].HeaderText = "FECHA";
                gridnotacd.Columns["NOT_RUC"].HeaderText = "RUC";
                gridnotacd.Columns["NOT_DOCUMENTO_AFECTADO"].HeaderText = "FACTURA";
                gridnotacd.Columns["NOT_VALOR"].HeaderText = "VALOR";
                gridnotacd.Columns["NOT_IVA"].Visible = false;
                gridnotacd.Columns["CAJ_CODIGO"].Visible = false;
                gridnotacd.Columns["ID_USUARIO"].Visible = false;
                gridnotacd.Columns["NOT_DOCUMENTO_TIPO"].Visible = false;
                gridnotacd.Columns["NOT_MOTIVO_MODIFICACION"].Visible = false;
                gridnotacd.Columns["TID_CODIGO"].Visible = false;
                gridnotacd.Columns["NOT_CANCELADO"].Visible = false;
                gridnotacd.Columns["NOT_CUENTA_CONTABLE"].Visible = false;
                gridnotacd.Columns["NOT_ANULADO"].Visible = false;
                gridnotacd.Columns["TNO_CODIGO"].Visible = false;
                gridnotacd.Columns["HOM_CODIGO1"].Visible = false;
                gridnotacd.Columns["MED_CODIGO1"].Visible = false;
                gridnotacd.Columns["FOR_CODIGO1"].Visible = false;
                gridnotacd.Columns["ENTITYSETNAME"].Visible = false;
                gridnotacd.Columns["ENTITYID"].Visible = false;
            
            

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
        class NotaDCComparer : IComparer<DtoNotaCreditoDebito>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public NotaDCComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(DtoNotaCreditoDebito Student1, DtoNotaCreditoDebito Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "NOT_NUMERO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.NOT_NUMERO.CompareTo(Student2.NOT_NUMERO);
                        }
                        else
                        {
                            returnValue = Student2.NOT_NUMERO.CompareTo(Student1.NOT_NUMERO);
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
                    case "NOT_FECHA":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.NOT_FECHA.CompareTo(Student2.NOT_FECHA);
                        }
                        else
                        {
                            returnValue = Student2.NOT_FECHA.CompareTo(Student1.NOT_FECHA);
                        }
                        break;
                   
                    case "NOT_VALOR":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.NOT_VALOR.CompareTo(Student2.NOT_VALOR);
                        }
                        else
                        {
                            returnValue = Student2.NOT_VALOR.CompareTo(Student1.NOT_VALOR);
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
        //Carga los combos con los tipos de formas de pago y con sus respectivos pagos
        
        public void cargarFormaPago()
        {
            try
            {
                cmb_tipoformapago.SelectedItem = 0;
                cmb_formapago.Items.Clear();
                if (cmb_tipoformapago.SelectedIndex >= 0 && cargafp == true)
                {
                    var formap = NegFormaPago.listaFormasPago().Where(cod => cod.TIPO_FORMA_PAGO.TIF_CODIGO == Int16.Parse(cmb_tipoformapago.SelectedValue.ToString())).ToList();
                    cmb_formapago.DataSource = formap;
                    cmb_formapago.ValueMember = "FOR_CODIGO";
                    cmb_formapago.DisplayMember = "FOR_DESCRIPCION";

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "err");
            }
        }
        #endregion

        private void cmb_tipoformapago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                //txt_cuentacontable.Focus();
                cmb_formapago.Focus();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                    if (chkmedico == true)
                    {
                        List<MEDICOS> listaMedicos = NegMedicos.listaMedicos();
                        frm_Ayudas lista = new frm_Ayudas(listaMedicos);
                        //lista.consultamedicos = NegMedicos.RecuperaMedicosFormulario();
                        //lista.tabla = "MEDICOS";
                        lista.ShowDialog();
                        if (lista.campoPadre.Text != string.Empty)
                        {
                            string codusuario = lista.campoPadre.Text;
                            medicos = NegMedicos.listaMedicos().Where(cod => cod.MED_CODIGO == Int16.Parse(codusuario)).FirstOrDefault();
                            notacreddebModificada.MED_CODIGO1 = medicos.MED_CODIGO;
                            notacreddebModificada.NOT_RAZON_SOCIAL = medicos.MED_APELLIDO_PATERNO + " " + medicos.MED_NOMBRE1;
                            notacreddebModificada.NOT_RUC = medicos.MED_RUC;
                            //txt_medico.Text = medicos.MED_CODIGO + " " + medicos.MED_NOMBRE1 + " " + medicos.MED_APELLIDO_PATERNO;
                            AgregarBindigControles();
                            txt_factura.Focus();
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                    if (chkmedico == true)
                    {
                        frm_Ayudas lista = new frm_Ayudas(NegHonorariosMedicos.ListaHonorariosNoutilizadosNotaDebito(medicos.MED_CODIGO.ToString()).ToList());
                        lista.tabla = "HONORARIOSNONOTADEBITO";
                        //lista.consultaHonorarios = NegHonorariosMedicos.ListaHonorariosNoutilizadosNotaDebito(medicos.MED_CODIGO.ToString()).ToList();
                        lista.ShowDialog();
                        if (lista.campoPadre.Text != string.Empty)
                        {
                            string codusuario = lista.campoPadre.Text;
                            honorario = NegHonorariosMedicos.RecuperaHonorariosMedicos().Where(cod => cod.HOM_CODIGO == int.Parse(codusuario)).FirstOrDefault();
                            notacreddebModificada.NOT_DOCUMENTO_AFECTADO = honorario.HOM_FACTURA_MEDICO;
                            notacreddebModificada.NOT_VALOR = (decimal)honorario.HOM_VALOR_TOTAL;
                            AgregarBindigControles();

                            txt_observacion.Focus();
                        }
                    }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
                else
                    MessageBox.Show(ex.Message);
            }
        }

        

        

        
        

        

        

       

        

        

        
        

        

       

        

        
       

        

       

        

        

        

        
        

        

        
        
        

        

        

       

    }
}
