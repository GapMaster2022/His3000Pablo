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
using His.Entidades.Pedidos;
using His.Negocio;
using His.Parametros;

namespace CuentaPaciente
{
    public partial class frmAyudaPaciente : Form
    {
        #region Variables
        frmDetalleCuenta detalleC = new frmDetalleCuenta();
        List<DtoPedidos> consultaPacientes = new List<DtoPedidos>();
        //List<DtoPacientesEmergencia> consultaPacientes = new List<DtoPacientesEmergencia>();
        public TextBox campoCodigo = new TextBox();
        public TextBox campoPadre = new TextBox();
        public TextBox campoAtencion = new TextBox();
        public TextBox campoAtencionNumero = new TextBox();
        public TextBox campoEstado = new TextBox();
        public string columnabuscada = "HCL";
        public string columnaAtencion = "ATENCION";
        public string columnaCodigo = "CODIGO";
        public string columnaFecha = "FECHA_INGRESO";
        public string columnaAtencionNumero = "ATENCION_NUMERO";
        public string columnaEstado = "ESTADO";
        public bool inicio = true;
        //public List<PACIENTES> listaPacientes;
        #endregion

        #region Constructor
        //public frm_AyudaPacientes(List<PACIENTES> pacientes)
        //{
        public frmAyudaPaciente()
        {
            InitializeComponent();
            cb_numFilas.SelectedIndex = 0;
            Buscar();
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
                    //KeyValuePair<int, string> sitem = (KeyValuePair<int, string>)cb_numFilas.SelectedItem;
                    //cantidad = sitem.Key;
                    cantidad = Convert.ToInt32(cb_numFilas.SelectedItem);
                }
                if (NegUtilitarios.ParametroAuditoria())
                    consultaPacientes = NegPedidos.recuperarListaCuentaAtencionTodos(id, historia, nombre, cantidad, 1);
                else
                    consultaPacientes = NegPedidos.recuperarListaCuentaAtencion(id, historia, nombre, cantidad, 1);

                grid.DataSource = consultaPacientes.Select(
                    p => new
                    {
                        CODIGO = p.CODIGO,
                        HCL = p.HISTORIA_CLINICA,
                        NOMBRE = p.PACIENTE,
                        ID = p.IDENTIFICACION,
                        ATENCION = p.ATE_CODIGO,
                        FECHA_ATENCION = p.PED_FECHA,
                        FECHA_ALTA = p.PED_FECHA_ALTA,
                        ATENCION_NUMERO = p.ATE_NUMERO,  // Tomo el numero de atencion /30/10/2012 / GIOVANNY TAPIA
                        ESC_CODIGO = p.ESC_CODIGO,
                        ESTADO = p.ESC_DESCRIPCION
                    }
                    ).Distinct().ToList();
                grid.DisplayLayout.Bands[0].Columns["NOMBRE"].Width = 350;
                grid.DisplayLayout.Bands[0].Columns["ATENCION"].Hidden = false;// Oculto el codigo de atencion /30/10/2012 / GIOVANNY TAPIA
                grid.DisplayLayout.Bands[0].Columns["ESC_CODIGO"].Hidden = true;
                grid.DisplayLayout.Bands[0].Columns["ESTADO"].Width = 100;
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
                campoAtencionNumero.Text = grid.ActiveRow.Cells[columnaAtencionNumero].Value.ToString();
                detalleC.codigoAtencionA = campoAtencion.Text;
                campoPadre.Text = grid.ActiveRow.Cells[columnabuscada].Value.ToString();
                campoCodigo.Text = grid.ActiveRow.Cells[columnaCodigo].Value.ToString();
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

        private void grid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            Int64 estado = 0;
            estado = Convert.ToInt64(e.Row.Cells["ESC_CODIGO"].Value);

            if (estado == 3)
            {
                e.Row.Appearance.BackColor = Color.FromArgb(162,237,206);
            }
            if (estado == 2)
            {
                e.Row.Appearance.BackColor = Color.FromArgb(154, 217, 234);
            }
            if (estado == 1)
            {
                e.Row.Appearance.BackColor = Color.FromArgb(255, 238, 188);
            }
            //else
            //{
            //    e.Row.Appearance.BackColor = Color.White;
            //}
        }
    }
}
