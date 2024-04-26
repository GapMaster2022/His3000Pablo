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
using His.Parametros;
using His.General;
using Recursos;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinEditors;

namespace His.Farmacia
{
    public partial class frm_AyudaProductos : Form
    {
        List<PRODUCTO> consultaPacientes = new List<PRODUCTO>();
        public DataGridViewRow  campoPadre;
        public UltraTextEditor campoPadre2;
        public string columnabuscada = "CODIGO";
        public bool inicio = true;
        public string entidad = string.Empty;

        public frm_AyudaProductos(String Entidad)
        {
            InitializeComponent();
            entidad = Entidad;
            btnActualizar.Appearance.Image = Archivo.ButtonRefresh;

                cb_numFilas.Items.Add(new KeyValuePair<int, string>(10,"10"));
                cb_numFilas.Items.Add(new KeyValuePair<int, string>(100, "100"));
                cb_numFilas.Items.Add(new KeyValuePair<int, string>(1000, "1000"));
                cb_numFilas.Items.Add(new KeyValuePair<int, string>(10000, "10000"));
                cb_numFilas.DisplayMember = "Value";
                cb_numFilas.ValueMember = "Key";
                cb_numFilas.SelectedIndex = 0;

                if (entidad == "PRODUCTOS")
                {
                    cbCriterio.Items.Add(new KeyValuePair<int, string>(1, "CODIGO"));
                    cbCriterio.Items.Add(new KeyValuePair<int, string>(2, "NOMBRE_COMERCIAL"));
                    cbCriterio.Items.Add(new KeyValuePair<int, string>(3, "NOMBRE_GENERICO"));
                    cbCriterio.Items.Add(new KeyValuePair<int, string>(4, "DESCRIPCION"));
                    cbCriterio.Items.Add(new KeyValuePair<int, string>(5, "CODIGO_BARRAS"));
                    cbCriterio.DisplayMember = "Value";
                    cbCriterio.ValueMember = "Key";
                    cbCriterio.SelectedIndex = 0;
                }
                else if (entidad == "ATENCIONES")
                {
                    cbCriterio.Items.Add(new KeyValuePair<int, string>(1, "NUMERO_ATENCION"));
                    cbCriterio.Items.Add(new KeyValuePair<int, string>(2, "HISTORIA_CLINICA"));
                    cbCriterio.Items.Add(new KeyValuePair<int, string>(3, "PACIENTE"));
                    cbCriterio.Items.Add(new KeyValuePair<int, string>(4, "HABITACION"));
                    cbCriterio.DisplayMember = "Value";
                    cbCriterio.ValueMember = "Key";
                    cbCriterio.SelectedIndex = 0;
                    campoPadre2 = new UltraTextEditor();
                    columnabuscada = "NUM_ATENCION";
                }

                inicio = false;

                cbCriterio.Focus();

        }
        private void Buscar()
        {
            try
            {
                if (entidad == "PRODUCTOS")
                {
                    string buscar = txt_busqNomCom.Text.ToString();

                    if (buscar.Trim() != string.Empty)
                    {
                        int cantidad = 100;
                        int criterio = 1;
                        int codigo = 0;

                        if (cb_numFilas.SelectedItem != null)
                        {
                            KeyValuePair<int, string> sitem = (KeyValuePair<int, string>)cb_numFilas.SelectedItem;
                            cantidad = sitem.Key;
                        }

                        if (cbCriterio.SelectedItem != null)
                        {
                            KeyValuePair<int, string> sitem = (KeyValuePair<int, string>)cbCriterio.SelectedItem;
                            criterio = sitem.Key;
                        }

                        if (criterio == 1)
                            if (!Microsoft.VisualBasic.Information.IsNumeric(buscar))
                                codigo = 0;
                            else
                                codigo = Convert.ToInt32(buscar);

                        consultaPacientes = NegProducto.RecuperarProductosLista(0,cantidad,buscar);
                        grid.DataSource = consultaPacientes.Select(
                            p => new
                            {
                                CODIGO = p.PRO_CODIGO,
                                NOMBRE_COMERCIAL = p.PRO_NOMBRE_COMERCIAL,
                                NOMBRE_GENERICO = p.PRO_NOMBRE_COMERCIAL,
                                NOMBRE_DESCRIPCION = p.PRO_NOMBRE_COMERCIAL,
                                CODIGO_BARRAS = p.PRO_CODIGO_BARRAS,
                                PRECIO = p.PRO_PRECIO,
                                IVA = p.PRO_IVA,
                                STOCK = p.PRO_CANTIDAD
                            }
                            ).ToList();
                        grid.DisplayLayout.Bands[0].Columns["NOMBRE_COMERCIAL"].Width = 250;
                    }
                    else
                    {
                        grid.DataSource = null;
                    }
                }
                else if (entidad == "ATENCIONES")
                {
                    string buscar = txt_busqNomCom.Text.ToString();

                    if (buscar.Trim() != string.Empty)
                    {
                        int cantidad = 100;
                        int criterio = 1;

                        if (cb_numFilas.SelectedItem != null)
                        {
                            KeyValuePair<int, string> sitem = (KeyValuePair<int, string>)cb_numFilas.SelectedItem;
                            cantidad = sitem.Key;
                        }

                        if (cbCriterio.SelectedItem != null)
                        {
                            KeyValuePair<int, string> sitem = (KeyValuePair<int, string>)cbCriterio.SelectedItem;
                            criterio = sitem.Key;
                        }

                        //consultaPacientes = NegProducto.RecuperarProductosLista(buscar, criterio, cantidad, codigo);
                        grid.DataSource = NegAtenciones.RecuperaAtencionesActivas(buscar,criterio,cantidad).Select(
                            a => new
                            {
                                NUM_ATENCION = a.ATE_NUMERO_ATENCION,
                                //FACTURA = a.ATE_FACTURA_PACIENTE,
                                PACIENTE = a.PAC_APELLIDO_PATERNO + " " + a.PAC_APELLIDO_MATERNO + " " + a.PAC_NOMBRE + " " + a.PAC_NOMBRE2,
                                HCL = a.PAC_HCL,
                                HABITACION = a.HAB_NUMERO,
                                FEC_INGRESO = a.ATE_FECHA_INGRESO
                            }
                            ).ToList();
                        grid.DisplayLayout.Bands[0].Columns["PACIENTE"].Width = 250;
                    }
                    else
                    {
                        grid.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void cb_numFilas_SelectedValueChanged(object sender, EventArgs e)
        {
            if (inicio == false)
                Buscar();

        }

        private void txt_busqNom_AfterExitEditMode(object sender, EventArgs e)
        {

        }

        private void txt_busqHist_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                Buscar();
            }
        }

        private void txt_busqNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                Buscar();
            }
        }

        private void txt_busqCi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                Buscar();
            }
        }

        private void grid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

        private void grid_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void enviarCodigo()
        {
            try
            {
                if (entidad == "PRODUCTOS")
                {
                    if (grid.ActiveRow != null)
                    {
                        campoPadre.Cells[2].Value = grid.ActiveRow.Cells[columnabuscada].Value.ToString();

                        if (grid.ActiveRow.Cells["NOMBRE_COMERCIAL"].Value != null)
                            campoPadre.Cells[3].Value = grid.ActiveRow.Cells["NOMBRE_COMERCIAL"].Value.ToString();

                        if (grid.ActiveRow.Cells["PRECIO"].Value != null)
                            campoPadre.Cells[4].Value = grid.ActiveRow.Cells["PRECIO"].Value.ToString();

                        if (grid.ActiveRow.Cells["IVA"].Value != null)
                            campoPadre.Cells[5].Value = grid.ActiveRow.Cells["IVA"].Value.ToString();

                        campoPadre.Cells[6].Value = 0;
                        campoPadre.Cells[6].ReadOnly = false;
                        campoPadre.Cells[7].Value = 0;
                        //campoPadre.Cells[6]

                        this.Close();
                    }
                }
                else if (entidad == "ATENCIONES")
                {
                    if (grid.ActiveRow != null)
                    {
                        campoPadre2.Text = grid.ActiveRow.Cells[columnabuscada].Value.ToString();
                        //campoPadre.Cells[6]

                        this.Close();
                    }
                }
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (r.InnerException != null)
                    MessageBox.Show(r.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void frm_AyudaPacientes_Load(object sender, EventArgs e)
        {
            txt_busqNomCom.Focus();
        }

        private void ultraGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    enviarCodigo();
                }
                else if (e.KeyCode == Keys.End)
                {
                    grid.ActiveCell = grid.Rows[grid.Rows.Count - 1].Cells[grid.ActiveCell.Column.Index];
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Home)
                {
                    grid.ActiveCell = grid.Rows[0].Cells[grid.ActiveCell.Column.Index];
                    e.Handled = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGrid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            enviarCodigo();
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            grid.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
            grid.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;
        }

        private void cbCriterio_SelectedValueChanged(object sender, EventArgs e)
        {
            if (inicio == false)
                Buscar();
        }

        private void cb_numFilas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                Buscar();
            }
        }
    }
}
