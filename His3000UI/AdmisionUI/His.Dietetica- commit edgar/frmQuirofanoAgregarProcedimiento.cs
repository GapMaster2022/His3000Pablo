using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using System.Windows.Data;
using System.IO;
using Infragistics.Win.UltraWinGrid;

namespace His.Dietetica
{
    public partial class frmQuirofanoAgregarProcedimiento : Form
    {
        NegQuirofano Quirofano = new NegQuirofano();
        internal static string codpro; //variable para obtener el codigo de producto
        internal static string despro; //variable para obtener la descripcion del producto
        internal static string cie_codigo; //varible para obtener el codigo del procedimiento CIE
        internal static string cie_descripcion; // variable para obtener la descripcion del procedimiento
        private static string cie_codigo1 = null; // variable que recupera los productos de ese procedimiento
        private static string codpro1; //variable que sierve para eliminar el producto dentro del procedimiento
        private static bool Editar; //variable para activar edidcion de procedimiento con respecto a los productos
        private static string descripcion; //variable que recupera el nombre del procedimiento
        private static string qpp_codigo; //variable para eliminar productos dentro de la tabla Quirofano_proce_produ
        private static bool Eliminar; //Determinara si Eliminar el procedimiento ó el producto dentro de un procedimiento
        //List<Procedimiento> proced = new List<Procedimiento>();
        //Subro.Controls.DataGridViewGrouper Procedimientos = new Subro.Controls.DataGridViewGrouper();

        public frmQuirofanoAgregarProcedimiento()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmayudaQuirofano x = new frmayudaQuirofano();
            frmayudaQuirofano.productosQuirofano = true;
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void X_FormClosed(object sender, FormClosedEventArgs e)
        {
            txtDesc.Text = despro;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Desbloquear();
            CargarProcedimientos();
            btnNuevo.Enabled = false;
            ultraGridProductos.Visible = false;
        }
        public void Desbloquear()
        {
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            grpDatos.Enabled = true;
        }
        public void Bloquear()
        {
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            grpDatos.Enabled = false;
            ultraGridProductos.Enabled = false;
            ultraGridProductos.Visible = false;
            btnModificar.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Bloquear();
            Limpiar();
            cie_codigo = null;
            cie_codigo1 = null;
            btnNuevo.Enabled = true;
        }
        public void Limpiar()
        {
            txtDesc.Text = "";
            txtproce.Text = "";
            txtorden.Text = "";
            txtcantidad.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmayudaProcedimiento x = new frmayudaProcedimiento();
            x.Show();
            x.FormClosed += X_FormClosed1;
        }

        private void X_FormClosed1(object sender, FormClosedEventArgs e)
        {
            txtproce.Text = cie_descripcion;
        }

        private void txtorden_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }
        private static void OnlyNumber(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789" + Convert.ToChar(8);
            }
            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(txtproce.Text != "" && txtorden.Text != "" && txtDesc.Text != "" && txtcantidad.Text != "")
            {
                try
                {
                    if(Editar == false)
                    {
                        string producto = null;
                        producto = Quirofano.ProductoRepetido(codpro, cie_codigo);
                        if (codpro == producto)
                        {
                            MessageBox.Show("El Producto ya Existe dentro del Procedimiento", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtDesc.Text = null;
                            txtcantidad.Text = null;
                            txtorden.Text = null;
                        }
                        else
                        {
                            Quirofano.AgregarProcedimiento(txtorden.Text, codpro, cie_codigo, txtcantidad.Text);
                            if (MessageBox.Show("¿Desea Seguir Agregando más Productos al Procedimiento?", "His3000", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Exclamation) == DialogResult.Yes)
                            {
                                txtDesc.Text = null;
                                txtorden.Text = null;
                                txtcantidad.Text = null;
                                button1.Enabled = false;
                                ultraGridProductos.Visible = true;
                                CargarProductos();
                            }
                            else
                            {
                                MessageBox.Show("Los Productos han sido Agregados Correctamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Limpiar();
                                Bloquear();
                                CargarProcedimientos();
                                UltraGridProcedimiento.Enabled = true;
                                button1.Enabled = true;
                                ultraGridProductos.Visible = true;
                                CargarProductos();
                                cie_codigo = null;
                            }
                        }
                        btnNuevo.Enabled = true;
                    }
                    else
                    {
                        Quirofano.ActualizarProcedimiento(cie_codigo1, codpro1, txtorden.Text, txtcantidad.Text);
                        MessageBox.Show("El Producto ha sido Actualizado Correctamente", "His3000",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                        Bloquear();
                        CargarProcedimientos();
                        ultraGridProductos.Visible = false;
                        btnModificar.Enabled = true;
                        Editar = false;
                        UltraGridProcedimiento.Enabled = true;
                    }
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Complete Todos Los Cuadros de Texto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmQuirofanoAgregarProcedimiento_Load(object sender, EventArgs e)
        {
            CargarProcedimientos();
        }

        public void CargarProcedimientos()
        {
            UltraGridProcedimiento.DataSource = Quirofano.TodosProcedimiento();
            RedimencionarTabla();
        }
        private void UltraGridProcedimiento_DoubleClick(object sender, EventArgs e)
        {
            UltraGridRow Fila = UltraGridProcedimiento.ActiveRow; //eligue el numero de fila que esta seleccionada
            cie_codigo1 = Fila.Cells[1].Value.ToString(); //Recupera los productos al otro grid
            if(MessageBox.Show("¿Desea Agregar más Productos ha este Procedimiento?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                == DialogResult.Yes)
            {
                cie_codigo = null;
                ultraGridProductos.Visible = true;
                CargarProductos();
                txtproce.Text = Fila.Cells[0].Value.ToString();
                cie_codigo = Fila.Cells[1].Value.ToString();
                Desbloquear();
                button1.Enabled = false;
            }
            else
            {
                cie_codigo = null;
                ultraGridProductos.Visible = true;
                CargarProductos();
                btnModificar.Enabled = true;
            }
        }
        public void CargarProductos()
        {
            if(cie_codigo != null)
            {
                ultraGridProductos.DataSource = Quirofano.TodosProductos(cie_codigo);
                RedimencionarTabla2();
            }
            else
            {
                ultraGridProductos.DataSource = Quirofano.TodosProductos(cie_codigo1);
                RedimencionarTabla2();
            }
        }
        public void RedimencionarTabla()
        {
            try
            {
                UltraGridBand bandUno = UltraGridProcedimiento.DisplayLayout.Bands[0];

                UltraGridProcedimiento.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                UltraGridProcedimiento.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                UltraGridProcedimiento.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                UltraGridProcedimiento.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                UltraGridProcedimiento.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                UltraGridProcedimiento.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                UltraGridProcedimiento.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                UltraGridProcedimiento.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                UltraGridProcedimiento.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                UltraGridProcedimiento.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                UltraGridProcedimiento.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                UltraGridProcedimiento.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                UltraGridProcedimiento.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                UltraGridProcedimiento.DisplayLayout.Bands[0].Columns[0].Width = 80;

                //Ocultar columnas, que son fundamentales al levantar informacion
                UltraGridProcedimiento.DisplayLayout.Bands[0].Columns[1].Hidden = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void RedimencionarTabla2()
        {
            try
            {
                UltraGridBand bandUno = ultraGridProductos.DisplayLayout.Bands[0];

                ultraGridProductos.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                ultraGridProductos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                ultraGridProductos.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                ultraGridProductos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                ultraGridProductos.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                ultraGridProductos.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                ultraGridProductos.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                ultraGridProductos.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                ultraGridProductos.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                ultraGridProductos.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                ultraGridProductos.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                ultraGridProductos.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                ultraGridProductos.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                ultraGridProductos.DisplayLayout.Bands[0].Columns[0].Width = 80;
                ultraGridProductos.DisplayLayout.Bands[0].Columns[1].Width = 400;
                ultraGridProductos.DisplayLayout.Bands[0].Columns[2].Width = 80;

                //Ocultar columnas, que son fundamentales al levantar informacion
                ultraGridProductos.DisplayLayout.Bands[0].Columns[3].Hidden = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if(Eliminar == false)//Elimina procedimiento
            {
                if (UltraGridProcedimiento.Selected.Rows.Count > 0)
                {
                    if (MessageBox.Show("¿Está Seguro de Eliminar ese Procedimiento?", "Warning", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        try
                        {
                            Quirofano.EliminarProcedimiento(cie_codigo1);
                            MessageBox.Show("Procedimiento Eliminado Correctamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Limpiar();
                            Bloquear();
                            CargarProcedimientos();
                            UltraGridProcedimiento.Enabled = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            else
            {
                if(ultraGridProductos.Selected.Rows.Count > 0)
                {
                    if(MessageBox.Show("¿Está Seguro de Eliminar Producto del Procedimiento?", "Warning", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        try
                        {
                            Quirofano.EliminarProduProce(cie_codigo1, codpro1);
                            MessageBox.Show("El Producto ha sido Eliminado del Procedimiento", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarProcedimientos();
                            Bloquear();
                            Limpiar();
                            Eliminar = false;
                            UltraGridProcedimiento.Enabled = true;
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            
        }

        private void UltraGridProcedimiento_Click(object sender, EventArgs e)
        {
            UltraGridRow Fila = UltraGridProcedimiento.ActiveRow; //eligue el numero de fila que esta seleccionada
            cie_codigo1 = Fila.Cells[1].Value.ToString(); //Recupera el codigo de procedimiento para eliminar todo producto que contenga este codigo
            descripcion = Fila.Cells[0].Value.ToString();
            btnCancelar.Enabled = true;
            Eliminar = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ultraGridProductos.Enabled = true;
            btnGuardar.Enabled = true;
            UltraGridProcedimiento.Enabled = false;
            Editar = true;
            btnModificar.Enabled = false;
            Eliminar = true;
        }

        private void ultraGridProductos_DoubleClick(object sender, EventArgs e)
        {
            UltraGridRow Fila = ultraGridProductos.ActiveRow;
            codpro1 = Fila.Cells[0].Value.ToString();
            txtDesc.Text = Fila.Cells[1].Value.ToString();
            txtorden.Text = Fila.Cells[2].Value.ToString();
            txtproce.Text = descripcion;
            txtcantidad.Text = Fila.Cells[4].Value.ToString();
            qpp_codigo = Fila.Cells[3].Value.ToString();
            grpDatos.Enabled = true;
            Eliminar = true;
        }

        private void txtcantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }
    }
}
