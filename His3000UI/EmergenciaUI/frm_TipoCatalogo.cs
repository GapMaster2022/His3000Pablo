using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using His.Entidades;
using His.Negocio;
using His.General;
using His.Parametros;
using Recursos;
using His.Entidades.Clases;
using Core.Entidades;
using System.Drawing.Drawing2D;

namespace His.Emergencia
{
    public partial class frm_TipoCatalogo : Form
    {

        #region Variables
      
        ImageList iconImages;
        public HC_CATALOGOS tipoprocedimientoOriginal = new HC_CATALOGOS();
        public HC_CATALOGOS catalogoModificado = new HC_CATALOGOS();
        public List<HC_CATALOGOS> procedimiento = new List<HC_CATALOGOS>();
        public List<HC_CATALOGOS_TIPO> listaCatalogosTipo = new List<HC_CATALOGOS_TIPO>();
        public int columnabuscada, idTipoCatalogo;
        private HC_CATALOGOS_TIPO tipoCatalogo;
        private HC_CATALOGOS modificarTipoCatalogo;
        HC_CATALOGOS tempCatalogo;
        int accion = 0;

        #endregion
       
        public frm_TipoCatalogo(HC_CATALOGOS_TIPO tipoCatalogo)
        {
            InitializeComponent();
            CargarCatalogo(tipoCatalogo.HCT_CODIGO); 
            cargarRecursos();            
            inicializarControles();
            HabilitarControles(true, false, false, true,false);
            this.tipoCatalogo = tipoCatalogo;
        }

        private void frm_TipoCatalogo_Load(object sender, EventArgs e)
        {
            try
            {
                HabilitarControles(false, false, false, true,false);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
                else
                    MessageBox.Show(ex.Message);
            }
        }

        public void cargarRecursos()
        {
            //this.tssMedicos.Image  = Recursos.Archivo.btnOrganigrama;  
            //imagenes del menu principal
            btnNuevo.Image = Archivo.imgBtnAdd2;
            btnGuardar.Image = Archivo.imgBtnRefresh;
            btnEliminar.Image = Archivo.imgDelete;
            btnSalir.Image = Archivo.imgBtnSalir32;
           
        }

        public void inicializarControles()
        {
            //Inicializo el imagenList
            iconImages = new System.Windows.Forms.ImageList();
            iconImages.ColorDepth = ColorDepth.Depth16Bit;
            iconImages.ImageSize = new Size(16, 16);
            iconImages.Images.Add(Archivo.icono);
            iconImages.Images.Add(Archivo.icoBtnOrganize);
            //estado
            cbxEstado.Items.Add("Activado");
            cbxEstado.Items.Add("Desactivado");
            cbxEstado.SelectedIndex = 0;
        }

        private void ResetearControles()
        {
            catalogoModificado = new HC_CATALOGOS();
            tipoprocedimientoOriginal = new HC_CATALOGOS();

            txtCodigo.Text = string.Empty;
            txtNombre.Text = string.Empty;
        }  

        #region Metodos Privados

        protected virtual void HabilitarControles(bool Nuevo, bool Modificar, bool Eliminar, bool Cancelar, bool pnlDatos)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Modificar;
            btnEliminar.Enabled = Eliminar;
            btnSalir.Enabled = Cancelar;
            txtCodigo.Enabled = false;
            pnlDatosGenerales.Enabled = pnlDatos;
        }

        public void CargarCatalogo(int keyCatalogo)
        {
            try
            {
                List<HC_CATALOGOS> lista;
                lista = NegCatalogos.RecuperarHcCatalogosPorTipo(keyCatalogo);
                ultraGridCatalogo.DataSource = lista;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void panelFiltros2_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ultraGridCatalogo.DisplayLayout.Bands[0];

            ultraGridCatalogo.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            ultraGridCatalogo.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            ultraGridCatalogo.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            ultraGridCatalogo.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            ultraGridCatalogo.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            ultraGridCatalogo.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            ultraGridCatalogo.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            ultraGridCatalogo.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGridCatalogo.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGridCatalogo.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGridCatalogo.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            ultraGridCatalogo.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //

            ultraGridCatalogo.DisplayLayout.UseFixedHeaders = true;

            bandUno.Columns["HCC_CODIGO"].Header.Caption = "CODIGO";
            bandUno.Columns["HCC_NOMBRE"].Header.Caption = "NOMBRE";
            bandUno.Columns["HCC_ESTADO"].Header.Caption = "HCC_ESTADO";

            bandUno.Columns["HCC_CODIGO"].Hidden = false;
            bandUno.Columns["HCC_NOMBRE"].Hidden = false;
            bandUno.Columns["HCC_ESTADO"].Hidden = true;
            bandUno.Columns["HC_CATALOGOS_TIPO"].Hidden = true;

            bandUno.Columns["HCC_CODIGO"].MaxWidth = 100;
            bandUno.Columns["HCC_NOMBRE"].MaxWidth = 300;
            //bandUno.Columns["HCC_ESTADO"].MaxWidth = 50;          

        }

        private void ultraGridCatalogo_DoubleClick(object sender, EventArgs e)
        {
            if (ultraGridCatalogo.ActiveRow != null)
            {
                txtCodigo.Text = string.Empty;
                txtNombre.Text = string.Empty;
                tempCatalogo = (HC_CATALOGOS)ultraGridCatalogo.ActiveRow.ListObject;
                txtCodigo.SelectedText = Convert.ToString(tempCatalogo.HCC_CODIGO);
                txtNombre.SelectedText = tempCatalogo.HCC_NOMBRE;
                HabilitarControles(true, true, true, true, true);
                catalogoModificado = tempCatalogo;
            }
        }

        private bool ValidarFormulario()
        {
            bool valido = true;
            if (txtNombre.Text == null || txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Campo Obligatorio", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombre.Focus();
                valido = false;
            }
            return valido;
        }

        private void GrabarDatos()
        {
            DialogResult resultado;
            //gridProcedimiento.Focus();
            try
            {
                if (ValidarFormulario())
                {
                    resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        if (catalogoModificado.EntityKey == null)
                        {
                            HC_CATALOGOS catalogo = new HC_CATALOGOS();
                            int codigoNuevo = NegCatalogos.RecuperaMaximoCatalogo();
                            codigoNuevo++;
                            catalogo.HCC_CODIGO = codigoNuevo;
                            catalogo.HCC_NOMBRE = txtNombre.Text;
                            catalogo.HCC_ESTADO = cbxEstado.SelectedIndex == 0 ? true : false;                               
                            catalogo.HC_CATALOGOS_TIPOReference.EntityKey = tipoCatalogo.EntityKey;                                
                            NegCatalogos.CrearCatalogo(catalogo);
                        }
                        else
                        {                            
                            catalogoModificado.HC_CATALOGOS_TIPOReference.EntityKey = tipoCatalogo.EntityKey;
                            catalogoModificado.HCC_NOMBRE = txtNombre.Text;
                            catalogoModificado.HCC_ESTADO = cbxEstado.SelectedIndex == 0 ? true : false;
                            NegCatalogos.GrabarCatalogo(catalogoModificado);
                        }              
                        ResetearControles();
                        MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarCatalogo(tipoCatalogo.HCT_CODIGO);
                    }                    
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
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

       
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {                
                HabilitarControles(false, true, false, true, true);
                txtCodigo.Text = string.Empty;
                txtNombre.Text = string.Empty;
                modificarTipoCatalogo = new HC_CATALOGOS();             
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

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                cbxEstado.Focus();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Desea eliminar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    tempCatalogo = (HC_CATALOGOS)ultraGridCatalogo.ActiveRow.ListObject;                    
                    NegCatalogos.EliminarCatalogo(tempCatalogo.ClonarEntidad());                   
                    CargarCatalogo(tipoCatalogo.HCT_CODIGO);
                    HabilitarControles(true, false, false, true, false);
                    MessageBox.Show("Datos Eliminados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Operación Invalida", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}
