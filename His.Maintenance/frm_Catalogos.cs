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
using Recursos;
using System.Drawing.Drawing2D;
using His.General;
using His.Parametros;
using Infragistics.Win.UltraWinGrid;
using Core.Entidades;

namespace His.Maintenance
{
    public partial class frm_Catalogos : Form
    {

        #region Variables
        //private Int16 accion;
        ImageList iconImages;
        public string tipoc;
        enum tipoGrid { eRaiz = 1, eCatalogo };
        private Int16 accionGrid;
        private int idTipoCodigo;

        List<HC_CATALOGOS> catalogo = new List<HC_CATALOGOS>();
        List<DtoCatalogos> tipocatalogo = new List<DtoCatalogos>();
        //List<DtoCatalogos> listaCatalogos;
        HC_CATALOGOS tempCatalogo;

        #endregion

        public frm_Catalogos()
        {
            InitializeComponent();
            cargarArbol(); 
            cargarRecursos();            
            inicializarControles();
            HalitarControles(false, true, false, false, true);
            toolStripButtonSalir.Visible = false;
            toolStripButtonSalir.Enabled= false;
        }

        public void cargarRecursos()
        {
            //this.tssMedicos.Image  = Recursos.Archivo.btnOrganigrama;  
            //imagenes del menu principal
            toolStripSplitButtonNuevo.Image = Archivo.imgBtnAdd2;
            toolStripButtonActualizar.Image = Archivo.imgBtnRestart;
            toolStripButtonModificar.Image = Archivo.imgBtnRefresh;
            toolStripButtonEliminar.Image = Archivo.imgDelete;
            HalitarControles(false, true, false, false, true);
 
        }
        public void inicializarControles()
        {
            //Inicializo el imagenList
            iconImages = new System.Windows.Forms.ImageList();
            iconImages.ColorDepth = ColorDepth.Depth16Bit;
            iconImages.ImageSize = new Size(16, 16);
            iconImages.Images.Add(Archivo.icono);
            iconImages.Images.Add(Archivo.icoBtnOrganize);
            treeViewCatalogo.ImageList = iconImages;
            toolStripButtonSalir.Visible = false;
            //oculto controles no utilizados
            //toolStripButtonModificar.Visible = false;
            toolStripButtonSalir.Visible = false;
        }



        //private void frm_Catalogos_Load(object sender, EventArgs e)
        //{
        //    this.WindowState = FormWindowState.Maximized;
        //    //cargo arbol paciente
        //    tipoc = "Catálogo";
        //    splitContainer1.Panel2Collapsed = true;
        //}


        #region Metodos Privados
       
        protected virtual void HalitarControles(bool Nuevo, bool Actualizar, bool Modificar, bool Eliminar, bool Cancelar)
        {
            toolStripSplitButtonNuevo.Enabled = Nuevo;
            toolStripButtonActualizar.Enabled = Actualizar;
            toolStripButtonModificar.Enabled = Modificar;
            toolStripButtonEliminar.Enabled = Eliminar;
          ///  toolStripButtonSalir.Enabled = Cancelar;
        }

        private void cargarArbol()
        {
            try
            {
                //cargo el arbol de acuerdo a la opcion activa
                treeViewCatalogo.Nodes.Clear();
                TreeNode raiz = new TreeNode();
                raiz.Name = "0";
                raiz.Text = "Catálogo";
                raiz.Tag = "raiz";
                treeViewCatalogo.Nodes.Add(raiz);
                var catalogotipo = NegCatalogos.ListaCatalogos();
                if (catalogotipo.Count > 0)
                {
                    foreach (var c in catalogotipo)
                    {
                        TreeNode nodoCatalogo = new TreeNode();
                        nodoCatalogo.Text = c.HCT_NOMBRE.ToString();
                        nodoCatalogo.Name = c.HCT_CODIGO.ToString();
                        idTipoCodigo = c.HCT_CODIGO;
                        nodoCatalogo.Tag = c;
                        raiz.Nodes.Add(nodoCatalogo);                         
                    }                
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void tvw_datos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                treeViewCatalogo.SelectedNode.ImageIndex = 0;
                if (treeViewCatalogo.SelectedNode.Tag != null)
                {
                    if (treeViewCatalogo.SelectedNode.Tag.ToString() == "raiz")
                    {
                        //accionGrid = (Int16)tipoGrid.eRaiz;
                        //ultraGridCatalogo.DataSource = NegCatalogos.recuperarCatalogos();                        
                        return;
                    }
                    else
                    {
                        int codigoCatalogo = Convert.ToInt16(treeViewCatalogo.SelectedNode.Name);
                        //toolStripSplitButtonNuevo.Enabled = true;                   
                        CargarCatalogo(codigoCatalogo);
                        HalitarControles(true, true, false, false, true); 
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //cargo las atenciones de un determinado paciente
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
            //bandUno.Columns["HCT_CODIGO"].Header.Caption = "TIPO";
            bandUno.Columns["HCC_NOMBRE"].Header.Caption = "NOMBRE";
            bandUno.Columns["HCC_ESTADO"].Header.Caption = "HCC_ESTADO";
           
            
            bandUno.Columns["HCC_CODIGO"].MaxWidth = 100;
            bandUno.Columns["HCC_NOMBRE"].MaxWidth = 350;
            //bandUno.Columns["HCC_ESTADO"].MaxWidth = 50;

            bandUno.Columns["HCC_CODIGO"].Hidden = false;
            //bandUno.Columns["HCT_CODIGO"].Header.Caption = "TIPO";
            bandUno.Columns["HCC_NOMBRE"].Hidden = false;
            bandUno.Columns["HCC_ESTADO"].Hidden = true;
            bandUno.Columns["HC_CATALOGOS_TIPO"].Hidden = true;
        }

        private void ultraGrid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            try
            {
                if (ultraGridCatalogo.ActiveRow.Index > -1)
                {
                    HalitarControles(false, false, true, true, true);
                    int codCatalogo = (Int32)ultraGridCatalogo.ActiveRow.Cells["HCC_CODIGO"].Value;                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
     

        private void treeViewCatalogo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                TreeViewHitTestInfo hit = treeViewCatalogo.HitTest(e.X, e.Y);
            }
        }

        private void treeViewCatalogo_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.ImageIndex = 1;
        }
 
      
        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {            
            //cargarArbol(); 
            CargarCatalogo(Convert.ToInt32(treeViewCatalogo.SelectedNode.Name));
        }     

        private void toolStripSplitButtonNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = (treeViewCatalogo.SelectedNode.Text);
                if (nombre != "Catálogo")
                {
                    HC_CATALOGOS_TIPO tipoCatalogo = (HC_CATALOGOS_TIPO)treeViewCatalogo.SelectedNode.Tag;
                    frm_CatalogoTipo form = new frm_CatalogoTipo(tipoCatalogo, nombre, 1);
                    form.Show(); 
                    CargarCatalogo(tipoCatalogo.HCT_CODIGO);
                    HalitarControles(true, true, false, false, true);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void ultraGridCatalogo_DoubleClick(object sender, EventArgs e)
        {
            if (ultraGridCatalogo.ActiveRow != null)
            {
                tempCatalogo = (HC_CATALOGOS)ultraGridCatalogo.ActiveRow.ListObject;                 
                HalitarControles(true, true, true, true, true);
            }
        }

        private void toolStripButtonModificar_Click(object sender, EventArgs e)
        {
            string nombre = (treeViewCatalogo.SelectedNode.Text);
            if (nombre != "Catálogo")
            {
                HC_CATALOGOS_TIPO tipoCatalogo = (HC_CATALOGOS_TIPO)treeViewCatalogo.SelectedNode.Tag;
                frm_CatalogoTipo form = new frm_CatalogoTipo(tempCatalogo, tipoCatalogo, nombre, 2);
                form.Show();               
                HalitarControles(false, true, false, false, true);
            }  
        }

        private void toolStripButtonEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Desea eliminar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    tempCatalogo = (HC_CATALOGOS)ultraGridCatalogo.ActiveRow.ListObject;
                    NegCatalogos.EliminarCatalogo(tempCatalogo.ClonarEntidad());
                    CargarCatalogo(Convert.ToInt32(treeViewCatalogo.SelectedNode.Name));
                    HalitarControles(false, true, false, false, true);
                    MessageBox.Show("Datos Eliminados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch 
            {
                MessageBox.Show("Operación Invalida", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
