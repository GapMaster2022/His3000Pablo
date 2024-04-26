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
using Infragistics.Win.UltraWinGrid;

namespace His.Honorarios
{
    public partial class frm_ayudapac : Form
    {
        List<PACIENTES> consultaPacientes = new List<PACIENTES>();
        public TextBox campoPadre = new TextBox();
        public string columnabuscada = "CODIGO";
        public bool inicio = true;
        internal static bool paciente; //envia valor true desde el Explorador general de honorarios. Edgar Ramos 20201109

        public frm_ayudapac()
        {
            InitializeComponent();
                cb_numFilas.Items.Add(new KeyValuePair<int, string>(10,"10"));
                cb_numFilas.Items.Add(new KeyValuePair<int, string>(100, "100"));
                cb_numFilas.Items.Add(new KeyValuePair<int, string>(1000, "1000"));
                cb_numFilas.Items.Add(new KeyValuePair<int, string>(10000, "10000"));
                cb_numFilas.DisplayMember = "Value";
                cb_numFilas.ValueMember = "Key";
                cb_numFilas.SelectedIndex = 0;
                inicio = false;

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

                    consultaPacientes = NegPacientes.RecuperarPacientesLista(id, historia, nombre, cantidad);
                    grid.DataSource = consultaPacientes.Select(
                        p => new {
                        
                            CODIGO = p.PAC_CODIGO,
                            HCL = p.PAC_HISTORIA_CLINICA,
                            NOMBRE = p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2,
                            ID = p.PAC_IDENTIFICACION
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

        private void cb_numFilas_SelectedValueChanged(object sender, EventArgs e)
        {
            if(inicio == false)
                Buscar();
        }

        private void txt_busqHist_Leave(object sender, EventArgs e)
        {

        }

        private void txt_busqHist_AfterExitEditMode(object sender, EventArgs e)
        {

        }

        private void txt_busqNom_AfterExitEditMode(object sender, EventArgs e)
        {

        }

        private void txt_busqCi_AfterExitEditMode(object sender, EventArgs e)
        {

        }

        private void txt_busqHist_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                if (e.KeyChar == (char)Keys.Enter)
                    Buscar();
            }
        }

        private void txt_busqNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");

                if (e.KeyChar == (char)Keys.Enter)
                    Buscar();
            }
        }

        private void txt_busqCi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");

                if (e.KeyChar == (char)Keys.Enter)
                    Buscar();
            }
        }

        private void enviarCodigo()
        {
            if(paciente == false)
            {
                campoPadre.Text = grid.ActiveRow.Cells[columnabuscada].Value.ToString();
                campoPadre.Tag = true;
                this.Close();
            }
            else
            {
                frmExploradorGeneral.pac_codigo = grid.ActiveRow.Cells[0].Value.ToString();
                frmExploradorGeneral.pac_hc = grid.ActiveRow.Cells[1].Value.ToString();       
                frm_RecetaMedica.hc = grid.ActiveRow.Cells[1].Value.ToString();
                this.Close();
            }   
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

        private void frm_AyudaPacientes_Load(object sender, EventArgs e)
        {
            if (inicio == false)
                Buscar();
        }

        private void txt_busqHist_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
