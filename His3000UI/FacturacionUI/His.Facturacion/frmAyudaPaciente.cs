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
using His.Parametros;

namespace His.Facturacion
{
    public partial class frmAyudaPaciente : Form
    {

        #region Variables
        frmDetalleCuenta detalleC = new frmDetalleCuenta();
        //List<PACIENTES> consultaPacientes = new List<PACIENTES>();
        List<DtoPacientesEmergencia> consultaPacientes = new List<DtoPacientesEmergencia>();
        public TextBox campoPadre = new TextBox();
        public TextBox campoAtencion = new TextBox();
        public string columnabuscada = "HCL";
        public string columnaAtencion = "ATENCION";
        public bool inicio = true;
        //public List<PACIENTES> listaPacientes;

        #endregion

        #region Constructor  
        //public frm_AyudaPacientes(List<PACIENTES> pacientes)
        //{
        public frmAyudaPaciente()
        {
            InitializeComponent();
        }

        private void Buscar()
        {
            try
            {
                string id = txt_busqCi.Text.ToString();
                string historia = txt_busqHist.Text.ToString();
                string nombre = txt_busqNom.Text.ToString();
                int cantidad = 100;

                if (cb_numFilas.SelectedItem != null)
                {
                    KeyValuePair<int, string> sitem = (KeyValuePair<int, string>)cb_numFilas.SelectedItem;
                    cantidad = sitem.Key;
                }
                consultaPacientes = NegPacientes.RecuperarPacientesListaEmerg(id, historia, nombre, cantidad,EmergenciaPAR.CodigoEmergencia );
                grid.DataSource = consultaPacientes.Select(
                    p => new
                    {
                        HCL = p.PAC_HISTORIA_CLINICA,
                        NOMBRE = p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2,
                        ID = p.PAC_IDENTIFICACION,
                        ATENCION = p.ATE_CODIGO,
                        FECHA_ATENCION = p.PAC_FECHA_ATENCION
                    }
                    ).ToList();
                grid.DisplayLayout.Bands[0].Columns["NOMBRE"].Width = 350;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }
        #endregion

        #region Metodos Privados

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
                campoPadre.Tag = true;
                campoAtencion.Text = grid.ActiveRow.Cells[columnaAtencion].Value.ToString();
                detalleC.codigoAtencionA = campoAtencion.Text;
                campoPadre.Text = grid.ActiveRow.Cells[columnabuscada].Value.ToString();                
                this.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void grid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Buscar();
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

        #endregion
            
    }
}
