using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using His.Negocio;
using His.Entidades;
using His.Parametros;
using System.Windows.Forms;
using Infragistics.Win.UltraDataGridView;
using Infragistics.Win.UltraWinGrid;

namespace His.Garantia
{
    public partial class frmAyuda : Form
    {
        List<PACIENTES> consultaPacientes = new List<PACIENTES>();
        public static TextBox campoPadre = new TextBox();
        public string columnabuscada = "HCL";
        public bool inicio = true;
        private int tipoIngreso;
        private int codigoAtencion;
        internal static bool autorizacion;
        internal static bool reporte;
        NegPacienteGarantia PacienteGarantia = new NegPacienteGarantia();
        //DataTable consultaPacientes = new DataTable();
        public frmAyuda()
        {
            InitializeComponent();
            //btnActualizar.Appearance.Image = Archivo.ButtonRefresh;
            cb_numFilas.Items.Add(new KeyValuePair<int, string>(50, "50"));
            cb_numFilas.Items.Add(new KeyValuePair<int, string>(100, "100"));
            cb_numFilas.Items.Add(new KeyValuePair<int, string>(1000, "1000"));
            cb_numFilas.Items.Add(new KeyValuePair<int, string>(10000, "10000"));
            cb_numFilas.DisplayMember = "Value";
            cb_numFilas.ValueMember = "Key";
            cb_numFilas.SelectedIndex = 0;
            inicio = false;
            tipoIngreso = 0;
        }

        private void frmAyuda_Load(object sender, EventArgs e)
        {
            consultaPacientes = NegPacientes.RecuperarPacientesLista("", "", "", 50);
            TablaPacientes.DataSource = consultaPacientes.Select(
                p => new
                {
                    HCL = p.PAC_HISTORIA_CLINICA,
                    NOMBRE = p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " +
                    p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2,
                    ID = p.PAC_IDENTIFICACION,
                }
                ).Distinct().ToList();
            TablaPacientes.DisplayLayout.Bands[0].Columns["NOMBRE"].Width = 350;
            txt_busqNom.Focus();
            if (reporte == true)
            {
                TablaPacientes.Visible = false;
                TablaGarantia.Visible = true;
                TablaGarantia.DataSource = PacienteGarantia.CargarGarantiasTodo();
                TablaGarantia.Columns[5].Visible = false;
                TablaGarantia.Columns[6].Visible = false;
                TablaGarantia.Columns[7].Visible = false;
                TablaGarantia.Columns[8].Visible = false;
                TablaGarantia.Columns[9].Visible = false;
                TablaGarantia.Columns[10].Visible = false;
                TablaGarantia.Columns[11].Visible = false;
                TablaGarantia.Columns[12].Visible = false;
                TablaGarantia.Columns[13].Visible = false;
                TablaGarantia.Columns[14].Visible = false;
                TablaGarantia.Columns[15].Visible = false;
                TablaGarantia.Columns[16].Visible = false;
                TablaGarantia.Columns[17].Visible = false;
                TablaGarantia.Columns[18].Visible = false;
                TablaGarantia.Columns[19].Visible = false;
                TablaGarantia.Columns[20].Visible = false;
                TablaGarantia.Columns[21].Visible = false;
                TablaGarantia.Columns[22].Visible = false;
                TablaGarantia.Columns[23].Visible = false;
                TablaGarantia.Columns[24].Visible = false;
                TablaGarantia.Columns[25].Visible = false;
                TablaGarantia.Columns[26].Visible = false;
                TablaGarantia.Columns[27].Visible = false;
                TablaGarantia.Columns[28].Visible = false;
                TablaGarantia.Columns[29].Visible = false;
                TablaGarantia.Columns[30].Visible = false;
            }
            if(autorizacion == true)
            {
                TablaPacientes.Visible = false;
                TablaGarantia.Visible = true;
                TablaGarantia.DataSource = PacienteGarantia.CargarGarantias();
                TablaGarantia.Columns[5].Visible = false;
                TablaGarantia.Columns[6].Visible = false;
                TablaGarantia.Columns[7].Visible = false;
                TablaGarantia.Columns[8].Visible = false;
                TablaGarantia.Columns[9].Visible = false;
                TablaGarantia.Columns[10].Visible = false;
                TablaGarantia.Columns[11].Visible = false;
                TablaGarantia.Columns[12].Visible = false;
                TablaGarantia.Columns[13].Visible = false;
                TablaGarantia.Columns[14].Visible = false;
                TablaGarantia.Columns[15].Visible = false;
                TablaGarantia.Columns[16].Visible = false;
                TablaGarantia.Columns[17].Visible = false;
                TablaGarantia.Columns[18].Visible = false;
                TablaGarantia.Columns[19].Visible = false;
                TablaGarantia.Columns[20].Visible = false;
                TablaGarantia.Columns[21].Visible = false;
                TablaGarantia.Columns[22].Visible = false;
                TablaGarantia.Columns[23].Visible = false;
                TablaGarantia.Columns[24].Visible = false;
                TablaGarantia.Columns[25].Visible = false;
                TablaGarantia.Columns[26].Visible = false;
                TablaGarantia.Columns[27].Visible = false;
                TablaGarantia.Columns[28].Visible = false;
                TablaGarantia.Columns[29].Visible = false;
                TablaGarantia.Columns[30].Visible = false;
            }
        }
        public void Buscar()
        {
            if(autorizacion == false && reporte == false)
            {
                try
                {
                    string id = txt_busqCi.Text;
                    string historia = txt_busqHist.Text;
                    string nombre = txt_busqNom.Text;
                    int cantidad = 100;
                    if (cb_numFilas.SelectedItem != null)
                    {
                        KeyValuePair<int, string> sitem = (KeyValuePair<int, string>)cb_numFilas.SelectedItem;
                        cantidad = sitem.Key;
                    }
                    consultaPacientes = NegPacientes.RecuperarPacientesLista(id, historia, nombre, cantidad);
                    TablaPacientes.DataSource = consultaPacientes.Select(
                        p => new
                        {
                            HCL = p.PAC_HISTORIA_CLINICA,
                            NOMBRE = p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " "
                            + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2,
                            ID = p.PAC_IDENTIFICACION,
                        }
                        ).OrderBy(p => p.NOMBRE).Distinct().ToList();
                    TablaPacientes.DisplayLayout.Bands[0].Columns["NOMBRE"].Width = 350;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio Algo por : " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (ex.InnerException != null)
                    {
                        MessageBox.Show(ex.InnerException.Message);
                    }
                }
            }
            else
            {
               //enviar datos por aqui a preautorizacion
            }
        }

        private void cb_numFilas_SelectedValueChanged(object sender, EventArgs e)
        {
            if(inicio == false)
            {
                Buscar();
            }
        }

        private void txt_busqHist_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (autorizacion == false && reporte == false)
            {
                if (e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    SendKeys.SendWait("{TAB}");
                    Buscar();
                }
            }
        }

        private void txt_busqHist_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txt_busqNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(autorizacion == false && reporte == false)
            {
                if (e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    SendKeys.SendWait("{TAB}");
                    Buscar();
                }
            }
        }

        private void txt_busqCi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(autorizacion == false && reporte == false)
            {
                if (e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    SendKeys.SendWait("{TAB}");
                    Buscar();
                }
            }
        }
        private void EnviarCodigo()
        {
            try
            {
                campoPadre.Tag = true;
                campoPadre.Text = TablaPacientes.ActiveRow.Cells[columnabuscada].Value.ToString();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if(autorizacion == false && reporte == false)
            {
                Buscar();
            }
            else
            {

            }
        }

        private void TablaPacientes_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode == Keys.Enter)
                {
                    EnviarCodigo();
                }
                if(e.KeyCode == Keys.End)
                {
                    TablaPacientes.ActiveCell = TablaPacientes.Rows[TablaPacientes.Rows.Count - 1].Cells[TablaPacientes.ActiveCell.Column.Index];
                    e.Handled = true;
                }
                if(e.KeyCode == Keys.Home)
                {
                    TablaPacientes.ActiveCell = TablaPacientes.Rows[0].Cells[TablaPacientes.ActiveCell.Column.Index];
                    e.Handled = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TablaPacientes_DoubleClick(object sender, EventArgs e)
        {
            EnviarCodigo();
        }

        private void TablaPacientes_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            TablaPacientes.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            TablaPacientes.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
            TablaPacientes.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;
        }

        private void txt_busqHist_TextChanged(object sender, EventArgs e)
        {
            if(autorizacion == true || reporte == true)
            {
                ((DataTable)TablaGarantia.DataSource).DefaultView.RowFilter = $"Hc LIKE '{txt_busqHist.Text}%'";
            }
        }

        private void txt_busqNom_TextChanged(object sender, EventArgs e)
        {
            if(autorizacion == true || reporte == true)
            {
                ((DataTable)TablaGarantia.DataSource).DefaultView.RowFilter = $"Nombre LIKE '{txt_busqNom.Text}%'";
            }
        }

        private void txt_busqCi_TextChanged(object sender, EventArgs e)
        {
            if(autorizacion == true || reporte == true)
            {
                ((DataTable)TablaGarantia.DataSource).DefaultView.RowFilter = $"Identificacion LIKE'{txt_busqCi.Text}%'";
            }
        }

        private void TablaGarantia_DoubleClick(object sender, EventArgs e)
        {
            if(autorizacion == true)
            {
                if (TablaGarantia.SelectedRows.Count > 0)
                {
                    frmPreAutorizacion.fechaing = TablaGarantia.CurrentRow.Cells[6].Value.ToString();
                    frmPreAutorizacion.atencion = TablaGarantia.CurrentRow.Cells[1].Value.ToString();
                    frmPreAutorizacion.hc = TablaGarantia.CurrentRow.Cells[2].Value.ToString();
                    frmPreAutorizacion.ape1 = TablaGarantia.CurrentRow.Cells[9].Value.ToString();
                    frmPreAutorizacion.ape2 = TablaGarantia.CurrentRow.Cells[10].Value.ToString();
                    frmPreAutorizacion.nom1 = TablaGarantia.CurrentRow.Cells[11].Value.ToString();
                    frmPreAutorizacion.nom2 = TablaGarantia.CurrentRow.Cells[12].Value.ToString();
                    frmPreAutorizacion.cedula = TablaGarantia.CurrentRow.Cells[4].Value.ToString();
                    frmPreAutorizacion.habitacion = TablaGarantia.CurrentRow.Cells[5].Value.ToString();
                    frmPreAutorizacion.fechagar = TablaGarantia.CurrentRow.Cells[0].Value.ToString();
                    frmPreAutorizacion.segu = TablaGarantia.CurrentRow.Cells[7].Value.ToString();
                    frmPreAutorizacion.codtipo = TablaGarantia.CurrentRow.Cells[8].Value.ToString();
                    frmPreAutorizacion.valor = TablaGarantia.CurrentRow.Cells[13].Value.ToString();
                    frmPreAutorizacion.diasve = TablaGarantia.CurrentRow.Cells[14].Value.ToString();
                    frmPreAutorizacion.beneficiario = TablaGarantia.CurrentRow.Cells[15].Value.ToString();
                    frmPreAutorizacion.numcb = TablaGarantia.CurrentRow.Cells[16].Value.ToString();
                    frmPreAutorizacion.fechaau = TablaGarantia.CurrentRow.Cells[17].Value.ToString();
                    frmPreAutorizacion.vaucher = TablaGarantia.CurrentRow.Cells[18].Value.ToString();
                    frmPreAutorizacion.codseguridad = TablaGarantia.CurrentRow.Cells[19].Value.ToString();
                    frmPreAutorizacion.establecimiento = TablaGarantia.CurrentRow.Cells[20].Value.ToString();
                    frmPreAutorizacion.autoriza = TablaGarantia.CurrentRow.Cells[21].Value.ToString();
                    frmPreAutorizacion.persona = TablaGarantia.CurrentRow.Cells[22].Value.ToString();
                    frmPreAutorizacion.lote = TablaGarantia.CurrentRow.Cells[23].Value.ToString();
                    frmPreAutorizacion.codigogarantia = TablaGarantia.CurrentRow.Cells[24].Value.ToString();
                    frmPreAutorizacion.numtarjeta = TablaGarantia.CurrentRow.Cells[25].Value.ToString();
                    frmPreAutorizacion.usuario = TablaGarantia.CurrentRow.Cells[26].Value.ToString();
                    frmPreAutorizacion.banco = TablaGarantia.CurrentRow.Cells[27].Value.ToString();
                    frmPreAutorizacion.vacio = false;
                }
            }
            if(reporte == true)
            {
                if( TablaGarantia.SelectedRows.Count > 0)
                {
                    frm_ExploradorFormularios.hc = TablaGarantia.CurrentRow.Cells[2].Value.ToString();
                }
            }
            this.Close();
        }
    }
}
