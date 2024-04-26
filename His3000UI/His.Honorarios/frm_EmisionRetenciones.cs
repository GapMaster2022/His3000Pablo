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
using Core.Entidades;
using His.Entidades.Clases;
using His.Parametros;

namespace His.Honorarios
{
    public partial class frm_EmisionRetenciones : Form
    {
        #region Varibles
        public DtoRetenciones retencionOriginal = new DtoRetenciones();
        public DtoRetenciones retencionModificada = new DtoRetenciones();
        public RETENCIONES retOrigen = new RETENCIONES();
        public RETENCIONES retModificada = new RETENCIONES();
        public List<DtoRetenciones> retenciones = new List<DtoRetenciones>();
        public MEDICOS medicos = new MEDICOS();
        public CAJAS caja = new CAJAS();
        public HONORARIOS_MEDICOS honorario = new HONORARIOS_MEDICOS();
        public HONORARIOS_MEDICOS honorarioOriginal = new HONORARIOS_MEDICOS();
        public int columnabuscada;
        public bool chkmedico;
        #endregion

        #region Constructor
        public frm_EmisionRetenciones()
        {
            InitializeComponent();
            cargarDatos();
        }
        #endregion

        #region Eventos
        private void frm_EmisionRetenciones_Load(object sender, EventArgs e)
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
                if (chkmedico == true)
                    label2.Text = "Medico:";
                RecuperaRetenciones();
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
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void gridnotacd_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, false, false, true, false, true,true);
                retencionModificada = gridnotacd.CurrentRow.DataBoundItem as DtoRetenciones;
                retencionOriginal = retencionModificada.ClonarEntidad();
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
                HalitarControles(true, true, false, true, false, false, true,false);
                NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();
                ResetearControles();
                caja = NegCajas.ListaCajas().Where(cod => cod.LOCALES.LOC_CODIGO == Sesion.codLocal).FirstOrDefault();
                retencionModificada.CAJ_CODIGO = caja.CAJ_CODIGO;
                retencionModificada.RET_FECHA = DateTime.Now;
                retencionModificada.RET_EJERCICIO_FISCAL =Int16.Parse(DateTime.Now.Year.ToString());
                if (NegNumeroControl.NumerodeControlAutomatico(5))
                {
                    numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 5).FirstOrDefault();
                    retencionModificada.RET_CODIGO = string.Format("{0:000}-{1:000}-{2:0000000}", caja.CAJ_NUMERO.ToString().Substring(0, 3), caja.CAJ_NUMERO.ToString().Substring(3), Int16.Parse(numerocontrol.NUMCON));
                    //txt_codigo.Text = numerocontrol.NUMCON;
                    AgregarBindigControles();
                    txt_sujetoderetencion.Focus();
                }
                else
                {
                    txt_codigo.ReadOnly = false;
                    txt_codigo.Focus();
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {

            ResetearControles();
            RecuperaRetenciones();
            HalitarControles(false, false, false, false, false, true, false,false);
            txt_codigo.DataBindings.Clear();
            txt_sujetoderetencion.DataBindings.Clear();
            txt_ingreso.DataBindings.Clear();

            txt_ruc.DataBindings.Clear();
            txt_factura.DataBindings.Clear();
            txt_base.DataBindings.Clear();
            txt_porcentaje.DataBindings.Clear();
            txt_valorretenido.DataBindings.Clear();
    
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txt_sujetoderetencion_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {

                    if (chkmedico == true)
                    {
                        frm_Ayudas lista = new frm_Ayudas(NegMedicos.RecuperaMedicosFormulario());
                        lista.ShowDialog();
                        if (lista.campoPadre.Text != string.Empty)
                        {
                            string codusuario = lista.campoPadre.Text;
                            medicos = NegMedicos.listaMedicos().Where(cod => cod.MED_CODIGO == Int16.Parse(codusuario)).FirstOrDefault();
                            retencionModificada.RET_SUJETO_RETENCION = medicos.MED_CODIGO + " " + medicos.MED_NOMBRE1 + " " + medicos.MED_APELLIDO_PATERNO;
                            retencionModificada.RET_RUC = medicos.MED_RUC;
                            retencionModificada.RET_RET_CODIGO = medicos.RETENCIONES_FUENTE.RET_CODIGO;
                            //txt_medico.Text = medicos.MED_CODIGO + " " + medicos.MED_NOMBRE1 + " " + medicos.MED_APELLIDO_PATERNO;
                            AgregarBindigControles();
                            txt_factura.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
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
                        frm_Ayudas lista = new frm_Ayudas(NegHonorariosMedicos.RecuperaHonorariosMedicosFormulario(medicos.MED_CODIGO.ToString(),"sinRetencion"));

                        lista.ShowDialog();
                        if (lista.campoPadre.Text != string.Empty)
                        {
                            string codusuario = lista.campoPadre.Text;
                            honorario = NegHonorariosMedicos.RecuperaHonorariosMedicos().Where(cod => cod.HOM_CODIGO == int.Parse(codusuario)).FirstOrDefault();
                            retencionModificada.RET_DOCUMENTO_AFECTADO = honorario.HOM_FACTURA_MEDICO;
                            retencionModificada.RET_BASE = honorario.HOM_VALOR_NETO;
                            var ret = NegRetencionesFuente.RecuperaRetencionesFuente().Where(cod => cod.RET_CODIGO == medicos.RETENCIONES_FUENTE.RET_CODIGO).FirstOrDefault();
                            retencionModificada.RET_PORCENTAJE = ret.RET_PORCENTAJE; //honorario.HOM_RETENCION==null? decimal.Parse("0"):decimal.Parse(honorario.HOM_RETENCION.ToString());
                            retencionModificada.RET_VALOR = retencionModificada.RET_BASE * retencionModificada.RET_PORCENTAJE/100;
                            AgregarBindigControles();

                            //txt_valorretenido.Focus();
                        }
                    }
                    //}

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
                    txt_base.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_codigo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txt_codigo.Text != string.Empty)
                {
                    retencionModificada.RET_CODIGO = string.Format("{0:000}-{1:000}-{2:0000000}", caja.CAJ_NUMERO.ToString().Substring(0, 3), caja.CAJ_NUMERO.ToString().Substring(3), Int16.Parse(txt_codigo.Text));
                    AgregarBindigControles();
                }
            }
            catch (Exception err)
            { 

            }
        }
        private void txt_codigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    txt_sujetoderetencion.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_sujetoderetencion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                txt_ruc.Focus();
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
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void gridnotacd_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridnotacd.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                retenciones.Sort(new RetencionesComparer(strColumnName, strSortOrder));
                gridnotacd.DataSource = null;
                gridnotacd.DataSource = retenciones;
                // redimensiona el ancho de la columna con los datos que carga
                //gridMedicos.AutoResizeColumns();
                //gridMedicos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                gridnotacd.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridnotacd.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                gridnotacd.Columns["RET_CODIGO"].HeaderText = "RETENCION";
                gridnotacd.Columns["RET_SUJETO_RETENCION"].HeaderText = "SUJETO DE RETENCION";
                gridnotacd.Columns["RET_FECHA"].HeaderText = "FECHA";
                gridnotacd.Columns["RET_RUC"].HeaderText = "RUC";
                gridnotacd.Columns["RET_DOCUMENTO_AFECTADO"].HeaderText = "FACTURA";
                gridnotacd.Columns["RET_VALOR"].HeaderText = "VALOR";
                gridnotacd.Columns["RET_PORCENTAJE"].HeaderText = "% RETENCION";
                gridnotacd.Columns["CAJ_CODIGO"].Visible = false;
                gridnotacd.Columns["ID_USUARIO"].Visible = false;
                gridnotacd.Columns["RET_DOCUMENTO_TIPO"].Visible = false;
                gridnotacd.Columns["RET_BASE"].Visible = false;
                gridnotacd.Columns["RET_EJERCICIO_FISCAL"].Visible = false;
                gridnotacd.Columns["RET_IMPRESA"].Visible = false;
                gridnotacd.Columns["RET_RET_CODIGO"].Visible = false;
                gridnotacd.Columns["RET_ANULADO"].Visible = false;
                gridnotacd.Columns["ENTITYSETNAME"].Visible = false;
                gridnotacd.Columns["ENTITYID"].Visible = false;
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridnotacd, new Point(MousePosition.X - this.Left- gridnotacd.Columns[e.ColumnIndex].Width, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Retenciones", gridnotacd.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
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
            frm.reporte = "rRetenciones";
            if (txt_codigo.Text.Length > 13)
                frm.campo1 = txt_codigo.Text.Substring(0, 3) + txt_codigo.Text.Substring(4, 3) + txt_codigo.Text.Substring(8);
            else
                frm.campo1 = txt_codigo.Text;
            frm.Show();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult resultado;

                resultado = MessageBox.Show("Desea Anular la Retención?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    retModificada.RET_CODIGO = retencionModificada.RET_CODIGO;
                    USUARIOS usuModificada = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == retencionModificada.ID_USUARIO).FirstOrDefault();
                    retModificada.USUARIOSReference.EntityKey = usuModificada.EntityKey;
                    CAJAS cajaModificada = NegCajas.ListaCajas().Where(cod => cod.CAJ_CODIGO == retencionModificada.CAJ_CODIGO).FirstOrDefault();
                    retModificada.CAJASReference.EntityKey = cajaModificada.EntityKey;
                    RETENCIONES_FUENTE retfuenteModificada = NegRetencionesFuente.RecuperaRetencionesFuente().Where(cod => cod.RET_CODIGO == retencionModificada.RET_RET_CODIGO).FirstOrDefault();
                    retModificada.RETENCIONES_FUENTEReference.EntityKey = retfuenteModificada.EntityKey;
                    retModificada.RET_BASE = retencionModificada.RET_BASE;
                    retModificada.RET_DOCUMENTO_AFECTADO = retencionModificada.RET_DOCUMENTO_AFECTADO;
                    retModificada.RET_DOCUMENTO_TIPO = retencionModificada.RET_DOCUMENTO_TIPO;
                    retModificada.RET_EJERCICIO_FISCAL = retencionModificada.RET_EJERCICIO_FISCAL;
                    retModificada.RET_FECHA = retencionModificada.RET_FECHA;
                    retModificada.RET_IMPRESA = retencionModificada.RET_IMPRESA;
                    retModificada.RET_PORCENTAJE = retencionModificada.RET_PORCENTAJE;
                    retModificada.RET_RUC = retencionModificada.RET_RUC;
                    retModificada.RET_SUJETO_RETENCION = retencionModificada.RET_SUJETO_RETENCION;
                    retModificada.RET_VALOR = retencionModificada.RET_VALOR;
                    retModificada.RET_ANULADO = true;
                    retModificada.EntityKey = new EntityKey(retencionModificada.ENTITYSETNAME
                            , retencionModificada.ENTITYID, retencionModificada.RET_CODIGO);

                    retOrigen.RET_CODIGO = retencionOriginal.RET_CODIGO;
                    USUARIOS usuOriginal = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == retencionOriginal.ID_USUARIO).FirstOrDefault();
                    retOrigen.USUARIOSReference.EntityKey = usuOriginal.EntityKey;
                    CAJAS cajaOriginal = NegCajas.ListaCajas().Where(cod => cod.CAJ_CODIGO == retencionOriginal.CAJ_CODIGO).FirstOrDefault();
                    retOrigen.CAJASReference.EntityKey = cajaOriginal.EntityKey;
                    RETENCIONES_FUENTE retfuenteOriginal = NegRetencionesFuente.RecuperaRetencionesFuente().Where(cod => cod.RET_CODIGO == retencionOriginal.RET_RET_CODIGO).FirstOrDefault();
                    retOrigen.RETENCIONES_FUENTEReference.EntityKey = retfuenteOriginal.EntityKey;
                    retOrigen.RET_BASE = retencionOriginal.RET_BASE;
                    retOrigen.RET_DOCUMENTO_AFECTADO = retencionOriginal.RET_DOCUMENTO_AFECTADO;
                    retOrigen.RET_DOCUMENTO_TIPO = retencionOriginal.RET_DOCUMENTO_TIPO;
                    retOrigen.RET_EJERCICIO_FISCAL = retencionOriginal.RET_EJERCICIO_FISCAL;
                    retOrigen.RET_FECHA = retencionOriginal.RET_FECHA;
                    retOrigen.RET_IMPRESA = retencionOriginal.RET_IMPRESA;
                    retOrigen.RET_PORCENTAJE = retencionOriginal.RET_PORCENTAJE;
                    retOrigen.RET_RUC = retencionOriginal.RET_RUC;
                    retOrigen.RET_SUJETO_RETENCION = retencionOriginal.RET_SUJETO_RETENCION;
                    retOrigen.RET_VALOR = retencionOriginal.RET_VALOR;
                    retOrigen.RET_ANULADO = retencionOriginal.RET_ANULADO;
                    retOrigen.EntityKey = new EntityKey(retencionOriginal.ENTITYSETNAME
                            , retencionOriginal.ENTITYID, retencionOriginal.RET_CODIGO);

                    NegRetenciones.GrabarRetencion(retModificada, retOrigen);

                    DtoHonorariosMedicos honorarioM = NegHonorariosMedicos.RecuperaHonorariosMedicosFormulario(null,null).Where(cod => cod.HOM_FACTURA_MEDICO == retencionModificada.RET_DOCUMENTO_AFECTADO).Where(ret => ret.RET_CODIGO == retModificada.RET_CODIGO).FirstOrDefault();
                    DtoHonorariosMedicos honorarioO = honorarioM.ClonarEntidad();
                    honorarioM.RET_CODIGO = null;

                    HONORARIOS_MEDICOS hModificado = new HONORARIOS_MEDICOS();
                    HONORARIOS_MEDICOS hOriginal = new HONORARIOS_MEDICOS();


                    ATENCIONES atModdifi = NegAtenciones.listaAtenciones().Where(cod => cod.ATE_CODIGO == honorarioM.ATE_CODIGO).FirstOrDefault();
                    //hModificado.ATENCIONESReference.EntityKey = atModdifi.EntityKey;
                    hModificado.ATE_CODIGO = atModdifi.ATE_CODIGO;
                    USUARIOS usuModifi = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == honorarioM.ID_USUARIO).FirstOrDefault();
                    hModificado.USUARIOSReference.EntityKey = usuModifi.EntityKey;
                    MEDICOS medModifi = NegMedicos.listaMedicos().Where(cod => cod.MED_CODIGO == honorarioM.MED_CODIGO).FirstOrDefault();
                    hModificado.MEDICOSReference.EntityKey = medModifi.EntityKey;
                    FORMA_PAGO forModifi = NegFormaPago.listaFormasPago().Where(cod => cod.FOR_CODIGO == honorarioM.FOR_CODIGO).FirstOrDefault();
                    hModificado.FOR_CODIGO = forModifi.FOR_CODIGO;
                    hModificado.RET_CODIGO1 = honorarioM.RET_CODIGO;
                    hModificado.HOM_APORTE_LLAMADA = honorarioM.HOM_APORTE_LLAMADA;
                    hModificado.HOM_CODIGO = honorarioM.HOM_CODIGO;
                    hModificado.HOM_COMISION_CLINICA = honorarioM.HOM_COMISION_CLINICA;
                    hModificado.HOM_ESTADO = honorarioM.HOM_ESTADO;
                    hModificado.HOM_FACTURA_FECHA = honorarioM.HOM_FACTURA_FECHA;
                    hModificado.HOM_FACTURA_MEDICO = honorarioM.HOM_FACTURA_MEDICO;
                    hModificado.HOM_FECHA_INGRESO = honorarioM.HOM_FECHA_INGRESO;
                    hModificado.HOM_RETENCION = honorarioM.HOM_RETENCION;
                    hModificado.HOM_VALOR_NETO = honorarioM.HOM_VALOR_NETO;
                    hModificado.HOM_VALOR_TOTAL = honorarioM.HOM_VALOR_TOTAL;
                    hModificado.HOM_VALOR_PAGADO = honorarioM.HOM_VALOR_PAGADO;
                    hModificado.EntityKey = new EntityKey(honorarioM.ENTITYSETNAME
                        , honorarioM.ENTITYID, honorarioM.HOM_CODIGO);

                    ATENCIONES atOrig = NegAtenciones.listaAtenciones().Where(cod => cod.ATE_CODIGO == honorarioO.ATE_CODIGO).FirstOrDefault();
                    hOriginal.ATE_CODIGO = atOrig.ATE_CODIGO;
                    USUARIOS usuOrig = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == honorarioO.ID_USUARIO).FirstOrDefault();
                    hOriginal.USUARIOSReference.EntityKey = usuOrig.EntityKey;
                    MEDICOS medOrig = NegMedicos.listaMedicos().Where(cod => cod.MED_CODIGO == honorarioO.MED_CODIGO).FirstOrDefault();
                    hOriginal.MEDICOSReference.EntityKey = medOrig.EntityKey;
                    FORMA_PAGO forOrig = NegFormaPago.listaFormasPago().Where(cod => cod.FOR_CODIGO == honorarioO.FOR_CODIGO).FirstOrDefault();
                    hOriginal.FOR_CODIGO = forOrig.FOR_CODIGO;
                    hOriginal.RET_CODIGO1 = honorarioO.RET_CODIGO;
                    hOriginal.HOM_APORTE_LLAMADA = honorarioO.HOM_APORTE_LLAMADA;
                    hOriginal.HOM_CODIGO = honorarioO.HOM_CODIGO;
                    hOriginal.HOM_COMISION_CLINICA = honorarioO.HOM_COMISION_CLINICA;
                    hOriginal.HOM_ESTADO = honorarioO.HOM_ESTADO;
                    hOriginal.HOM_FACTURA_FECHA = honorarioO.HOM_FACTURA_FECHA;
                    hOriginal.HOM_FACTURA_MEDICO = honorarioO.HOM_FACTURA_MEDICO;
                    hOriginal.HOM_FECHA_INGRESO = honorarioO.HOM_FECHA_INGRESO;
                    hOriginal.HOM_RETENCION = honorarioO.HOM_RETENCION;
                    hOriginal.HOM_VALOR_NETO = honorarioO.HOM_VALOR_NETO;
                    hOriginal.HOM_VALOR_TOTAL = honorarioO.HOM_VALOR_TOTAL;
                    hOriginal.HOM_VALOR_PAGADO = honorarioO.HOM_VALOR_PAGADO;
                    hOriginal.EntityKey = new EntityKey(honorarioO.ENTITYSETNAME
                        , honorarioO.ENTITYID, honorarioO.HOM_CODIGO);

                    NegHonorariosMedicos.GrabarHonorarioMedico(hModificado, hOriginal);




                    RecuperaRetenciones();
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

        #endregion

        #region Metodos Privados
        private void AgregarBindigControles()
        {
            Binding RET_CODIGO = new Binding("Text", retencionModificada, "RET_CODIGO", true);
            Binding RET_SUJETO_RETENCION = new Binding("Text", retencionModificada, "RET_SUJETO_RETENCION", true);
            Binding RET_FECHA = new Binding("Text", retencionModificada, "RET_FECHA", true);
            Binding RET_RUC = new Binding("Text", retencionModificada, "RET_RUC", true);
            Binding RET_DOCUMENTO_AFECTADO = new Binding("Text", retencionModificada, "RET_DOCUMENTO_AFECTADO", true);
            Binding RET_VALOR = new Binding("text", retencionModificada, "RET_VALOR", true);
            Binding RET_PORCENTAJE = new Binding("text", retencionModificada, "RET_PORCENTAJE", true);
            Binding RET_BASE = new Binding("text", retencionModificada, "RET_BASE", true);
            txt_codigo.DataBindings.Clear();
            txt_sujetoderetencion.DataBindings.Clear();
            txt_ingreso.DataBindings.Clear();

            txt_ruc.DataBindings.Clear();
            txt_factura.DataBindings.Clear();
            txt_base.DataBindings.Clear();
            txt_porcentaje.DataBindings.Clear();
            txt_valorretenido.DataBindings.Clear();
            
            txt_codigo.DataBindings.Add(RET_CODIGO);
            txt_sujetoderetencion.DataBindings.Add(RET_SUJETO_RETENCION);
            txt_ingreso.DataBindings.Add(RET_FECHA);
            txt_ruc.DataBindings.Add(RET_RUC);
            txt_factura.DataBindings.Add(RET_DOCUMENTO_AFECTADO);
            txt_base.DataBindings.Add(RET_BASE);
            txt_porcentaje.DataBindings.Add(RET_PORCENTAJE);
            txt_valorretenido.DataBindings.Add(RET_VALOR);
            


        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            retencionOriginal = new DtoRetenciones();
            retencionModificada = new DtoRetenciones();
            retOrigen = new RETENCIONES();
            retModificada = new RETENCIONES();
            honorario = new HONORARIOS_MEDICOS();
            medicos = new MEDICOS();
            txt_codigo.Text = string.Empty;
            txt_ruc.Text = string.Empty;
            txt_sujetoderetencion.Text = string.Empty;
            txt_factura.Text = string.Empty;
            txt_ingreso.Text = string.Empty;
            txt_base.Text = string.Empty;
            txt_porcentaje.Text = string.Empty;
            txt_valorretenido.Text = string.Empty;
           
        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (retencionModificada.RET_CODIGO == null || retencionModificada.RET_CODIGO == string.Empty)
            {
                AgregarError(txt_codigo);
                valido = false;
            }
            if (retencionModificada.RET_SUJETO_RETENCION == null || retencionModificada.RET_SUJETO_RETENCION == string.Empty)
            {
                AgregarError(txt_sujetoderetencion);
                valido = false;
            }
            if (retencionModificada.RET_RUC == null || retencionModificada.RET_RUC == string.Empty)
            {
                AgregarError(txt_ruc);
                valido = false;
            }
            if (retencionModificada.RET_VALOR == null || retencionModificada.RET_VALOR == 0)
            {
                AgregarError(txt_base);
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
                    retModificada.RET_CODIGO = retencionModificada.RET_CODIGO;
                    USUARIOS usuModificada = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == Sesion.codUsuario).FirstOrDefault();
                    retModificada.USUARIOSReference.EntityKey = usuModificada.EntityKey;
                    CAJAS cajaModificada = NegCajas.ListaCajas().Where(cod => cod.CAJ_CODIGO == caja.CAJ_CODIGO).FirstOrDefault();
                    retModificada.CAJASReference.EntityKey = cajaModificada.EntityKey;
                    RETENCIONES_FUENTE retfuenteModificada = NegRetencionesFuente.RecuperaRetencionesFuente().Where(cod => cod.RET_CODIGO == retencionModificada.RET_RET_CODIGO).FirstOrDefault();
                    retModificada.RETENCIONES_FUENTEReference.EntityKey = retfuenteModificada.EntityKey;
                    retModificada.RET_BASE = retencionModificada.RET_BASE;
                    retModificada.RET_DOCUMENTO_AFECTADO = retencionModificada.RET_DOCUMENTO_AFECTADO;
                    retModificada.RET_DOCUMENTO_TIPO = retencionModificada.RET_DOCUMENTO_TIPO;
                    retModificada.RET_EJERCICIO_FISCAL = retencionModificada.RET_EJERCICIO_FISCAL;
                    retModificada.RET_FECHA = retencionModificada.RET_FECHA;
                    retModificada.RET_IMPRESA = retencionModificada.RET_IMPRESA;
                    retModificada.RET_PORCENTAJE = retencionModificada.RET_PORCENTAJE;
                    retModificada.RET_RUC = retencionModificada.RET_RUC;
                    retModificada.RET_SUJETO_RETENCION = retencionModificada.RET_SUJETO_RETENCION;
                    retModificada.RET_VALOR = retencionModificada.RET_VALOR;
                    
                   
                    if (retencionOriginal.CAJ_CODIGO == 0)
                    {
                        if (NegNumeroControl.NumerodeControlAutomatico(5))
                        {
                            retModificada.RET_CODIGO = caja.CAJ_NUMERO + string.Format("{0:0000000}", NegNumeroControl.NumeroControlOptine(5)); // int.Parse((NegMedicos.RecuperaMaximoMedicos() + 1).ToString());
                            NegRetenciones.CrearRetencion(retModificada);
                            NegNumeroControl.LiberaNumeroControl(5);
                        }
                        else
                            retModificada.RET_CODIGO = txt_codigo.Text.Substring(0, 3) + txt_codigo.Text.Substring(4, 3) + txt_codigo.Text.Substring(8);
                            NegRetenciones.CrearRetencion(retModificada);


                            DtoHonorariosMedicos honorarioM = NegHonorariosMedicos.RecuperaHonorariosMedicosFormulario(medicos.MED_CODIGO.ToString(),null).Where(cod => cod.HOM_CODIGO == honorario.HOM_CODIGO).FirstOrDefault();
                        DtoHonorariosMedicos honorarioO = honorarioM.ClonarEntidad();
                        honorarioM.RET_CODIGO = retModificada.RET_CODIGO;

                        HONORARIOS_MEDICOS hModificado = new HONORARIOS_MEDICOS();
                        HONORARIOS_MEDICOS hOriginal = new HONORARIOS_MEDICOS();


                        ATENCIONES atModdifi = NegAtenciones.listaAtenciones().Where(cod => cod.ATE_CODIGO == honorarioM.ATE_CODIGO).FirstOrDefault();
                        hModificado.ATE_CODIGO = atModdifi.ATE_CODIGO;
                        USUARIOS usuModifi = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == honorario.USUARIOS.ID_USUARIO ).FirstOrDefault();
                        hModificado.USUARIOSReference.EntityKey = usuModifi.EntityKey;
                        MEDICOS medModifi = NegMedicos.listaMedicos().Where(cod => cod.MED_CODIGO == honorarioM.MED_CODIGO).FirstOrDefault();
                        hModificado.MEDICOSReference.EntityKey = medModifi.EntityKey;
                        FORMA_PAGO forModifi = NegFormaPago.listaFormasPago().Where(cod => cod.FOR_CODIGO == honorarioM.FOR_CODIGO).FirstOrDefault();
                        hModificado.FOR_CODIGO = forModifi.FOR_CODIGO;
                        hModificado.RET_CODIGO1 = honorarioM.RET_CODIGO;
                        hModificado.HOM_APORTE_LLAMADA = honorarioM.HOM_APORTE_LLAMADA;
                        hModificado.HOM_CODIGO = honorarioM.HOM_CODIGO;
                        hModificado.HOM_COMISION_CLINICA = honorarioM.HOM_COMISION_CLINICA;
                        hModificado.HOM_ESTADO = honorarioM.HOM_ESTADO;
                        hModificado.HOM_FACTURA_FECHA = honorarioM.HOM_FACTURA_FECHA;
                        hModificado.HOM_FACTURA_MEDICO = honorarioM.HOM_FACTURA_MEDICO;
                        hModificado.HOM_FECHA_INGRESO = honorarioM.HOM_FECHA_INGRESO;
                        hModificado.HOM_RETENCION = honorarioM.HOM_RETENCION;
                        hModificado.HOM_VALOR_NETO = honorarioM.HOM_VALOR_NETO;
                        hModificado.HOM_VALOR_TOTAL = honorarioM.HOM_VALOR_TOTAL;
                        hModificado.HOM_VALOR_PAGADO = honorarioM.HOM_VALOR_PAGADO;
                        hModificado.EntityKey = new EntityKey(honorarioM.ENTITYSETNAME
                            , honorarioM.ENTITYID, honorarioM.HOM_CODIGO);

                        ATENCIONES atOrig = NegAtenciones.listaAtenciones().Where(cod => cod.ATE_CODIGO == honorarioO.ATE_CODIGO).FirstOrDefault();
                        hOriginal.ATE_CODIGO = atOrig.ATE_CODIGO;
                        USUARIOS usuOrig = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == honorario.USUARIOS.ID_USUARIO).FirstOrDefault();
                        hOriginal.USUARIOSReference.EntityKey = usuOrig.EntityKey;
                        MEDICOS medOrig = NegMedicos.listaMedicos().Where(cod => cod.MED_CODIGO == honorarioO.MED_CODIGO).FirstOrDefault();
                        hOriginal.MEDICOSReference.EntityKey = medOrig.EntityKey;
                        FORMA_PAGO forOrig = NegFormaPago.listaFormasPago().Where(cod => cod.FOR_CODIGO == honorarioO.FOR_CODIGO).FirstOrDefault();
                        hOriginal.FOR_CODIGO = forOrig.FOR_CODIGO;
                        hOriginal.RET_CODIGO1 = honorarioO.RET_CODIGO;
                        hOriginal.HOM_APORTE_LLAMADA = honorarioO.HOM_APORTE_LLAMADA;
                        hOriginal.HOM_CODIGO = honorarioO.HOM_CODIGO;
                        hOriginal.HOM_COMISION_CLINICA = honorarioO.HOM_COMISION_CLINICA;
                        hOriginal.HOM_ESTADO = honorarioO.HOM_ESTADO;
                        hOriginal.HOM_FACTURA_FECHA = honorarioO.HOM_FACTURA_FECHA;
                        hOriginal.HOM_FACTURA_MEDICO = honorarioO.HOM_FACTURA_MEDICO;
                        hOriginal.HOM_FECHA_INGRESO = honorarioO.HOM_FECHA_INGRESO;
                        hOriginal.HOM_RETENCION = honorarioO.HOM_RETENCION;
                        hOriginal.HOM_VALOR_NETO = honorarioO.HOM_VALOR_NETO;
                        hOriginal.HOM_VALOR_TOTAL = honorarioO.HOM_VALOR_TOTAL;
                        hOriginal.HOM_VALOR_PAGADO = honorarioO.HOM_VALOR_PAGADO;
                        hOriginal.EntityKey = new EntityKey(honorarioO.ENTITYSETNAME
                            , honorarioO.ENTITYID, honorarioO.HOM_CODIGO);

                        NegHonorariosMedicos.GrabarHonorarioMedico(hModificado, hOriginal);

                    }
                    
                    RecuperaRetenciones();
                    HalitarControles(false, false, false, false, true, false, true,false);
                    MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        /// <summary>
        /// fuencion para habilitar los botones
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
        }
        private void RecuperaRetenciones()
        {

            retenciones =NegRetenciones.RecuperaRetenciones().Where(an=>an.RET_ANULADO==false).ToList();
            gridnotacd.DataSource = retenciones;
            gridnotacd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridnotacd.Columns["RET_CODIGO"].HeaderText = "RETENCION";
            gridnotacd.Columns["RET_SUJETO_RETENCION"].HeaderText = "SUJETO DE RETENCION";
            gridnotacd.Columns["RET_FECHA"].HeaderText = "FECHA";
            gridnotacd.Columns["RET_RUC"].HeaderText = "RUC";
            gridnotacd.Columns["RET_DOCUMENTO_AFECTADO"].HeaderText = "FACTURA";
            gridnotacd.Columns["RET_VALOR"].HeaderText = "VALOR";
            gridnotacd.Columns["RET_PORCENTAJE"].HeaderText = "% RETENCION";
            gridnotacd.Columns["CAJ_CODIGO"].Visible = false;
            gridnotacd.Columns["ID_USUARIO"].Visible = false;
            gridnotacd.Columns["RET_DOCUMENTO_TIPO"].Visible = false;
            gridnotacd.Columns["RET_BASE"].Visible = false;
            gridnotacd.Columns["RET_EJERCICIO_FISCAL"].Visible = false;
            gridnotacd.Columns["RET_IMPRESA"].Visible = false;
            gridnotacd.Columns["RET_RET_CODIGO"].Visible = false;
            gridnotacd.Columns["RET_ANULADO"].Visible = false;
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
        class RetencionesComparer : IComparer<DtoRetenciones>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public RetencionesComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(DtoRetenciones Student1, DtoRetenciones Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "RET_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.RET_CODIGO.CompareTo(Student2.RET_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.RET_CODIGO.CompareTo(Student1.RET_CODIGO);
                        }

                        break;
                    case "RET_SUJETO_RETENCION":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.RET_SUJETO_RETENCION.CompareTo(Student2.RET_SUJETO_RETENCION);
                        }
                        else
                        {
                            returnValue = Student2.RET_SUJETO_RETENCION.CompareTo(Student1.RET_SUJETO_RETENCION);
                        }
                        break;
                    case "RET_FECHA":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.RET_FECHA.CompareTo(Student2.RET_FECHA);
                        }
                        else
                        {
                            returnValue = Student2.RET_FECHA.CompareTo(Student1.RET_FECHA);
                        }
                        break;

                    case "RET_DOCUMENTO_AFECTADO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.RET_DOCUMENTO_AFECTADO.CompareTo(Student2.RET_DOCUMENTO_AFECTADO);
                        }
                        else
                        {
                            returnValue = Student2.RET_DOCUMENTO_AFECTADO.CompareTo(Student1.RET_DOCUMENTO_AFECTADO);
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

        private void txt_factura_Leave(object sender, EventArgs e)
        {
            if (txt_factura.Text != "   -   -")
            {
                string factura = txt_factura.Text.Replace("-", "");
                factura = factura.Replace(" ", "");
                if (factura.Length > 7)
                    factura = factura.Substring(6);

                if (medicos != null)
                {
                    if (medicos.MED_FACTURA_INICIAL != null && medicos.MED_FACTURA_INICIAL != string.Empty)

                        if (int.Parse(medicos.MED_FACTURA_INICIAL.Substring(6)) <= int.Parse(factura) && int.Parse(medicos.MED_FACTURA_FINAL.Substring(6)) >= int.Parse(factura))
                            txt_factura.Text = medicos.MED_FACTURA_INICIAL.Substring(0, 3) + medicos.MED_FACTURA_INICIAL.Substring(3, 3) + string.Format("{0:0000000}", int.Parse(factura));
                        else
                        {
                            MessageBox.Show("Factura incorrecta, no se encuentra dentro del rango de Facturas del Medico.", "Medicos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_factura.Focus();
                        }
                    else
                    {
                        if (factura.Length < 13)
                            txt_factura.Text = "001-001-" + string.Format("{0:0000000}", int.Parse(factura));
                    }
                }
                else
                {
                    if (factura.Length < 13)
                        txt_factura.Text = "001-001-" + string.Format("{0:0000000}", int.Parse(factura));
                }
                retencionModificada.RET_DOCUMENTO_AFECTADO = txt_factura.Text.Replace("-", "");//txt_factura.Text.Substring(0, 3) + txt_factura.Text.Substring(4, 3) + txt_factura.Text.Substring(8);

            }
        }

        

        

        
     }
}
