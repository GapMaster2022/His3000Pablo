using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using System.IO;
using His.Entidades.Clases;

namespace His.Dietetica
{
    public partial class frmQuirofanoAgregarProducto : Form
    {
        NegQuirofano Quirofano = new NegQuirofano();
        internal static string codpro; //codigo de la tabla producto del sic
        internal static string despro; //descripcion de la tabla producto del sic
        private static string qp_codigo; //variable que contiene el codigo de la tabla Quirofano_Productos
        private static bool Editar; //para editar dentro del boton de guardar
        public int bodega = Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()); //por defecto inicializa con 12 que es quirofano
        public frmQuirofanoAgregarProducto()
        {
            InitializeComponent();
        }

        public frmQuirofanoAgregarProducto(int bodega)
        {
            InitializeComponent();
            this.bodega = bodega;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_SolicitarProductos x = new frm_SolicitarProductos();
            x.ShowDialog();
            CargarTablaProductos();
        }

        private void frmQuirofanoAgregarProducto_Load(object sender, EventArgs e)
        {
            CargarTablaProductos();
            CargarGrupo();
        }
        public void HabilitarBotones(bool nuevo, bool guardar, bool modificar, bool eliminar, bool cancelar)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnModificar.Enabled = modificar;
            btnCancelar.Enabled = cancelar;
            btnBorrar.Enabled = eliminar;
        }
        public void CargarGrupo()
        {
            try
            {
                if (NegParametros.ParametroComboQuirofano())
                {
                    cmbTipo.DataSource = NegQuirofano.MostrarGruposAliansa();
                    cmbTipo.DisplayMember = "DESCRIPCION";
                    cmbTipo.ValueMember = "CODIGO";
                }
                else
                {
                    cmbTipo.DataSource = NegQuirofano.MostrarGrupo();
                    cmbTipo.DisplayMember = "DESCRIPCION";
                    cmbTipo.ValueMember = "CODIGO";
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio al cargar los grupos. Consulte con el Administrador.\r\nMás Detalles: " + ex.Message,
                    "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CargarTablaProductos()
        {
            ultraGridProducto.DataSource = NegQuirofano.Productos(bodega);
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Desbloquear();
        }
        public void Desbloquear()
        {
            grpDatos.Enabled = true;
            ultraGridProducto.Enabled = true;
            btnBorrar.Enabled = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }
        public void Bloquear()
        {
            grpDatos.Enabled = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtDesc.Text = "";
            Bloquear();
        }
        public void Limpiar()
        {
            cmbTipo.SelectedIndex = -1;
            txtDesc.Text = "";
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string producto = NegQuirofano.SoloProductoRepetido(codpro, bodega);
            if(cmbTipo.SelectedIndex != -1 && txtDesc.Text != "")
            {
                try
                {
                    if(Editar == false)
                    {
                        if(codpro == producto)
                        {
                            MessageBox.Show("¡El Producto ya Existe!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Limpiar();
                        }
                        else
                        {
                            NegQuirofano.AgregarProducto(codpro, cmbTipo.GetItemText(cmbTipo.SelectedItem), bodega);
                            MessageBox.Show("El Producto ha sido Agregado Correctamente!", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Bloquear();
                            Limpiar();
                            CargarTablaProductos();
                        }
                    }
                    else
                    {
                        Quirofano.ActualizarProducto(qp_codigo, cmbTipo.GetItemText(cmbTipo.SelectedItem));
                        MessageBox.Show("El Producto se ha Modificado Correctamente!", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Bloquear();
                        Limpiar();
                        CargarTablaProductos();
                        Editar = false;
                        button1.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            else
            {
                MessageBox.Show("Grupo ó Descripcion no pueden ser (Vacío)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ultraGridProducto_DoubleClick(object sender, EventArgs e)
        {
            UltraGridRow Fila = ultraGridProducto.ActiveRow; //eligue el numero de fila que esta seleccionada
            txtDesc.Text = Fila.Cells[1].Value.ToString(); //enviamos el valor al txtdes
            cmbTipo.SelectedItem = Fila.Cells[2].Value.ToString(); // enviamos el valor al combo
            qp_codigo = Fila.Cells[3].Value.ToString(); //almacenamos la clave del codigo de la tabla quirofano_productos
            btnModificar.Enabled = true;
            btnBorrar.Enabled = true;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if(ultraGridProducto.Selected.Rows.Count > 0)
            {
                try
                {
                    UltraGridRow fila = ultraGridProducto.ActiveRow;
                    qp_codigo = fila.Cells[3].Value.ToString();
                    if(MessageBox.Show("¿Está Seguro de Eliminar ese Producto?","Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) 
                        == DialogResult.Yes)
                    {
                        Quirofano.EliminarProducto(qp_codigo);
                        MessageBox.Show("Se ha Eliminado Correctamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Bloquear();
                        CargarTablaProductos();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Editar = true;
            Desbloquear();
            btnModificar.Enabled = false;
            button1.Enabled = false;
            btnNuevo.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm_SolicitarProductos x = new frm_SolicitarProductos(bodega);
            x.codsub = Convert.ToDouble(cmbTipo.SelectedValue.ToString());
            x.grupo = cmbTipo.Text;
            x.ShowDialog();
            CargarTablaProductos();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    this.ultraGridExcelExporter1.Export(ultraGridProducto, PathExcel);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
        }
        private String FindSavePath()
        {
            Stream myStream;
            string myFilepath = null;
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        myFilepath = saveFileDialog1.FileName;
                        myStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myFilepath;
        }

        private void ultraGridProducto_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                UltraGridBand bandUno = ultraGridProducto.DisplayLayout.Bands[0];

                ultraGridProducto.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                ultraGridProducto.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                ultraGridProducto.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                ultraGridProducto.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                ultraGridProducto.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                ultraGridProducto.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                ultraGridProducto.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                ultraGridProducto.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                ultraGridProducto.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                ultraGridProducto.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                ultraGridProducto.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                ultraGridProducto.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                ultraGridProducto.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                ultraGridProducto.DisplayLayout.Bands[0].Columns[0].Width = 60;
                ultraGridProducto.DisplayLayout.Bands[0].Columns[1].Width = 400;
                ultraGridProducto.DisplayLayout.Bands[0].Columns[2].Width = 100;

                //Ocultar columnas, que son fundamentales al levantar informacion
                ultraGridProducto.DisplayLayout.Bands[0].Columns[3].Hidden = true;
                //TablaProductos.DisplayLayout.Bands[0].Columns[12].Hidden = true;
                //TablaProductos.DisplayLayout.Bands[0].Columns[13].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
