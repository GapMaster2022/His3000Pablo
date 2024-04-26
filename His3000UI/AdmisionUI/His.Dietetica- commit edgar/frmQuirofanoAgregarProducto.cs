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

namespace His.Dietetica
{
    public partial class frmQuirofanoAgregarProducto : Form
    {
        NegQuirofano Quirofano = new NegQuirofano();
        internal static string codpro; //codigo de la tabla producto del sic
        internal static string despro; //descripcion de la tabla producto del sic
        private static string qp_codigo; //variable que contiene el codigo de la tabla Quirofano_Productos
        private static bool Editar; //para editar dentro del boton de guardar
        public frmQuirofanoAgregarProducto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmayudaQuirofano x = new frmayudaQuirofano();
            frmayudaQuirofano.producto = true;
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void X_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(codpro != null && despro != null)
            {
                txtDesc.Text = despro;
            }
        }

        private void frmQuirofanoAgregarProducto_Load(object sender, EventArgs e)
        {
            CargarTablaProductos();
        }
        public void CargarTablaProductos()
        {
            ultraGridProducto.DataSource = Quirofano.CargarTablaProductos();
            RedimencionarTabla();
            
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
            string producto = Quirofano.SoloProductoRepetido(codpro);
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
                            Quirofano.AgregarProducto(codpro, cmbTipo.GetItemText(cmbTipo.SelectedItem));
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
        public void RedimencionarTabla()
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
                    if(MessageBox.Show("¿Está Seguro de Eliminar ese Producto?","Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) 
                        == DialogResult.Yes)
                    {
                        Quirofano.EliminarProducto(qp_codigo);
                        MessageBox.Show("Se ha Eliminado Correctamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Bloquear();
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
        }
    }
}
