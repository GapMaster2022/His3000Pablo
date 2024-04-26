using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Honorarios.Datos;
using Infragistics.Win.UltraWinGrid;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Negocio;
using His.Parametros;


namespace CuentaPaciente
{
    public partial class frmAyudaPacientesIess : Form
    {
        List<DtoCuentasPacientes> consultaPacientes = new List<DtoCuentasPacientes>();
        public TextBox campoPadre = new TextBox();
        public TextBox campoAtencion = new TextBox();
        public TextBox campoHabitacion = new TextBox();
        public CheckBox campoReferido = new CheckBox();
        public TextBox CampoEstado = new TextBox();
        public MaskedTextBox txtfactura = new MaskedTextBox();
        public MaskedTextBox txtNumControl = new MaskedTextBox();
        public DateTimePicker fechafacturacion = new DateTimePicker();
        public DateTimePicker fechaingreso = new DateTimePicker();
        public DateTimePicker fechaalta = new DateTimePicker();

        public string columnahabitacion = "HABITACION";
        public string columnafactura = "FACTURA";
        public string columnacontrol = "CONTROL";
        public string columnabuscada = "CODIGO";
        public string columnaAtencion = "ATENCION";
        public string columnaReferido = "referido";
        public string columnaEstado = "ESTADO";
        Atencion atencion = new His.Honorarios.Datos.Atencion();
        DataTable datos = new DataTable();
        public frmAyudaPacientesIess()
        {
            InitializeComponent();
            txtfactura.Mask = "000-000-0000000";
        }

        private void frm_AyudaPacien_Load(object sender, EventArgs e)
        {

            //datos=atencion.cargar_atencionesCuentas();
            // grid.DataSource = datos;
            Buscar();
            //grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 250;



        }
        private void enviarCodigo()
        {
            try
            {
                campoPadre.Text = grid.ActiveRow.Cells[columnabuscada].Value.ToString();
                campoPadre.Tag = true;
                campoAtencion.Text = grid.ActiveRow.Cells[columnaAtencion].Value.ToString();
                campoAtencion.Tag = true;
                campoHabitacion.Text = grid.ActiveRow.Cells[columnahabitacion].Value.ToString();
                campoHabitacion.Tag = true;
                txtfactura.Text = grid.ActiveRow.Cells[columnafactura].Value.ToString();
                txtfactura.Tag = true;
                txtNumControl.Text = grid.ActiveRow.Cells[columnacontrol].Value.ToString();
                txtNumControl.Tag = true;
                if (grid.ActiveRow.Cells["FECHAF"].Value.ToString() == "")
                    fechafacturacion.Value = Convert.ToDateTime("12/12/2011");
                else
                    fechafacturacion.Value = Convert.ToDateTime(grid.ActiveRow.Cells["FECHAF"].Value);
                if (grid.ActiveRow.Cells["FECHAA"].Value.ToString() == "")
                    fechaalta.Value = Convert.ToDateTime("12/12/2011");
                else
                    fechaalta.Value = Convert.ToDateTime(grid.ActiveRow.Cells["FECHAA"].Value);
                if (grid.ActiveRow.Cells["FECHA"].Value.ToString() == "")
                    fechaingreso.Value = Convert.ToDateTime("12/12/2011");
                else
                    fechaingreso.Value = Convert.ToDateTime(grid.ActiveRow.Cells["FECHA"].Value);
                Boolean refer = Convert.ToBoolean(grid.ActiveRow.Cells[columnaReferido].Value);
                if (refer == true)
                {
                    campoReferido.Checked = true;
                }
                if (refer == false)
                {
                    campoReferido.Checked = false;
                }
                //if (campoAtencion.Text != null && campoAtencion.Text.Trim() != string.Empty)
                //{
                //    frmCuentaPacientesIess frm = new frmCuentaPacientesIess();
                //    frm.codigoAtencion = Convert.ToInt32(campoAtencion.Text);
                //}
                this.Close();

            }
            catch (Exception es)
            {
            }
        }

        private void grid_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            try
            {
                enviarCodigo();
            }
            catch (Exception ex)
            {

            }
        }

        private void txt_busqHist_KeyPress(object sender, KeyPressEventArgs e)
        {

            //if (e.KeyChar == (char)Keys.Enter)
            //{
            //    datos = atencion.buscar_atencionesCuentas(txt_busqHist.Text.Trim(), "", "");

            //    grid.DataSource = datos;
            //    grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 250;
            //    txt_busqCi.Text = "";
            //    txt_busqNom.Text = "";
            //}

            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                Buscar();
            }

        }

        private void txt_busqNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)Keys.Enter)
            //{
           //datos = atencion.buscar_atencionesCuentas("", txt_busqNom.Text, "");

            //    grid.DataSource = datos;
            //    grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 250;
            //    txt_busqCi.Text = "";
            //    txt_busqHist.Text = "";
            //}

            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                Buscar();
            }
        }

        private void Buscar()
        {
            try
            {
                string id = txt_busqCi.Text.ToString();
                string historia = txt_busqHist.Text.ToString();
                string nombre = txt_busqNom.Text.ToString();
                string habitacion = textBox1.Text.ToString();
                int cantidad = 100;

                consultaPacientes = NegCuentasPacientes.recuperarListaCuentaAtencion1(id, historia, nombre, habitacion, cantidad, 1);

                grid.DataSource = consultaPacientes.Select(
                    p => new
                    {
                        CODIGO = p.CODIGO,
                        ATENCION = p.ATE_CODIGO,
                        HCL = p.HISTORIA_CLINICA,
                        NOMBRES = p.PACIENTE,
                        ID = p.IDENTIFICACION,
                        HABITACION = p.HABITACION,
                        FACTURA = p.FACTURA,
                        CONTROL = p.NUMCONTROL,
                        FECHAF = p.FECHAFACTURA,
                        FECHA = p.PED_FECHA,
                        FECHAA = p.PED_FECHA_ALTA,
                        REFERIDO = p.REFERIDO,
                        ESC_CODIGO = p.ESC_CODIGO
                    }
                    ).Distinct().ToList();
                grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
                //grid.DisplayLayout.Bands[0].Columns["ESC_CODIGO"].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void txt_busqCi_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)Keys.Enter)
            //{
            //    datos = atencion.buscar_atencionesCuentas("", "", txt_busqCi.Text);

            //    grid.DataSource = datos;
            //    grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 250;

            //    txt_busqHist.Text = "";
            //    txt_busqNom.Text = "";

            //}

            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                Buscar();
            }
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.Bands[0].Columns["CODIGO"].Hidden = true;
            e.Layout.Bands[0].Columns["referido"].Hidden = true;
            e.Layout.Bands[0].Columns["FACTURA"].Hidden = true;
            e.Layout.Bands[0].Columns["CONTROL"].Hidden = true;
            e.Layout.Bands[0].Columns["FECHAF"].Hidden = true;
            e.Layout.Bands[0].Columns["FECHA"].Hidden = false;
            e.Layout.Bands[0].Columns["FECHAA"].Hidden = false;

            e.Layout.Bands[0].Columns["ESC_CODIGO"].Hidden = true;

            grid.DisplayLayout.Bands[0].Columns["CODIGO"].Width = 30;
            grid.DisplayLayout.Bands[0].Columns["referido"].Width = 30;
            grid.DisplayLayout.Bands[0].Columns["FACTURA"].Width = 30;
            grid.DisplayLayout.Bands[0].Columns["CONTROL"].Width = 30;
            grid.DisplayLayout.Bands[0].Columns["FECHA"].Width = 110;
            grid.DisplayLayout.Bands[0].Columns["FECHAA"].Width = 80;
            grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 250;
            // e.Layout.Bands[0].Columns["HABITACION"].Hidden = true;
        }

        private void grid_KeyDown(object sender, KeyEventArgs e)
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

        private void grid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            Int64 estado = 0;
            estado = Convert.ToInt64(e.Row.Cells["ESC_CODIGO"].Value);

            if(estado == 5)
            {
                e.Row.Appearance.BackColor = Color.LightBlue;
            }
            else
            {
                e.Row.Appearance.BackColor = Color.White;
            }
        }
    }
}
