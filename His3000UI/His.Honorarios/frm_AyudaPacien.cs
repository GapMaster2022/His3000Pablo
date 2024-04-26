using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;

namespace His.Honorarios
{
    public partial class frm_AyudaPacien : Form
    {
        public TextBox campoPadre = new TextBox();
        public TextBox campoAtencion = new TextBox();
        public TextBox campoHabitacion = new TextBox();
        public CheckBox campoReferido = new CheckBox();
        public MaskedTextBox txtfactura = new MaskedTextBox();
        public MaskedTextBox txtNumControl = new MaskedTextBox();
        public DateTimePicker fechafacturacion = new DateTimePicker();
        public DateTimePicker fechaingreso = new DateTimePicker();
        public DateTimePicker fechaalta = new DateTimePicker();
        public TextBox ate_num = new TextBox();
        public bool anuladas = false;

        public string fechaFacturaHis;
        //public  TextBox txtpago = new TextBox();

        //public string columnaPago_codigo = "pago";
        //public string columna = "pago";
        public string columnahabitacion = "HABITACION";
        public string columnafactura = "FACTURA";
        public string columnacontrol = "CONTROL";
        public string columnabuscada = "CODIGO";
        public string columnaAtencion = "ATENCION";
        public string columnanumatencion = "NRO ATENCION";
        public string columnaReferido = "referido";
        Datos.Atencion atencion = new His.Honorarios.Datos.Atencion();
        DataTable datos = new DataTable();
        public frm_AyudaPacien()
        {
            InitializeComponent();
            txtfactura.Mask = "000-000-0000000";


            //datos = atencion.buscar_atenciones(txt_busqHist.Text.Trim(), "", "", "", fechaFacturaHis);
            datos = atencion.buscar_atenciones(txt_busqHist.Text.Trim(), "", "", "");

            grid.DataSource = datos;
            grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;

            //txt_busqCi.Text = "";
            //txtNHabitacion.Text = "";
            //txt_busqNom.Text = "";

        }

        private void frm_AyudaPacien_Load(object sender, EventArgs e)
        {

            datos = atencion.cargar_atenciones();

            grid.DataSource = datos;
            grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
            //datos = atencion.buscar_atenciones(txt_busqHist.Text.Trim(), "", "", "", fechaFacturaHis);
            datos = atencion.buscar_atenciones(txt_busqHist.Text.Trim(), "", "", "");

            grid.DataSource = datos;
            grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
            txt_busqCi.Text = "";
            txtNHabitacion.Text = "";
            txt_busqNom.Text = "";
            if (anuladas == true)
            {
                try
                {
                    grid.DataSource = NegHonorariosMedicos.PacientesFac_Anuladas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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

                ate_num.Text = grid.ActiveRow.Cells[columnanumatencion].Value.ToString();
                ate_num.Tag = true;




                txtNumControl.Text = grid.ActiveRow.Cells[columnacontrol].Value.ToString();
                txtNumControl.Tag = true;
                if (grid.ActiveRow.Cells["fecha1"].Value.ToString() == "")
                    fechafacturacion.Value = Convert.ToDateTime("01/01/1900");
                else
                    fechafacturacion.Value = Convert.ToDateTime(grid.ActiveRow.Cells["fecha1"].Value);

                if (grid.ActiveRow.Cells["FECHA_ALTA"].Value.ToString() == "")
                    fechaalta.Value = Convert.ToDateTime("01/01/1900");
                else
                    fechaalta.Value = Convert.ToDateTime(grid.ActiveRow.Cells["FECHA_ALTA"].Value);

                if (grid.ActiveRow.Cells["FECHA_INGRESO"].Value.ToString() == "")
                    fechaingreso.Value = Convert.ToDateTime("01/01/1900");
                else
                    fechaingreso.Value = Convert.ToDateTime(grid.ActiveRow.Cells["FECHA_INGRESO"].Value);

                string refer = grid.ActiveRow.Cells["referido"].Value.ToString();
                if (refer.Trim() == "HOSPITALARIO" || refer.Trim() == "INSTITUCIONAL")
                {
                    campoReferido.Checked = true;
                }
                if (refer.Trim() == "PRIVADO")
                {
                    campoReferido.Checked = false;
                }
                this.Close();

                this.Close();
            }
            catch (Exception es)
            {
                throw es;
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

            if (e.KeyChar == (char)Keys.Enter)
            {
                //datos = atencion.buscar_atenciones(txt_busqHist.Text.Trim(), "", "","", fechaFacturaHis);
                datos = atencion.buscar_atenciones(txt_busqHist.Text.Trim(), "", "", "");
                datos = atencion.buscar_atenciones(txt_busqHist.Text.Trim(), "", "", "");
                grid.DataSource = datos;
                grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
                txt_busqCi.Text = "";
                txtNHabitacion.Text = "";
                txt_busqNom.Text = "";

            }
        }

        private void txt_busqNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //datos = atencion.buscar_atenciones("", txt_busqNom.Text, "","", fechaFacturaHis);
                datos = atencion.buscar_atenciones("", txt_busqNom.Text, "", "");

                grid.DataSource = datos;
                grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
                txt_busqCi.Text = "";
                txt_busqHist.Text = "";
                txtNHabitacion.Text = "";

            }
        }

        private void txt_busqCi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //datos = atencion.buscar_atenciones("", "", txt_busqCi.Text,"", fechaFacturaHis);
                datos = atencion.buscar_atenciones("", "", txt_busqCi.Text, "");

                grid.DataSource = datos;
                grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;

                txt_busqHist.Text = "";
                txt_busqNom.Text = "";
                txtNHabitacion.Text = "";
            }
        }

        private void txtNHabitacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // datos = atencion.buscar_atenciones("", "", "", txtNHabitacion.Text.Trim(), fechaFacturaHis);
                datos = atencion.buscar_atenciones("", "", "", txtNHabitacion.Text.Trim(), "");
                grid.DataSource = datos;
                grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
                txt_busqCi.Text = "";
                txtNHabitacion.Text = "";
                txt_busqNom.Text = "";

            }
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.Bands[0].Columns["referido"].Hidden = true;
            e.Layout.Bands[0].Columns["FACTURA"].Hidden = false;
            e.Layout.Bands[0].Columns["CONTROL"].Hidden = true;
            e.Layout.Bands[0].Columns["fecha1"].Hidden = true;
            e.Layout.Bands[0].Columns["CODIGO"].Hidden = true;
            e.Layout.Bands[0].Columns["ATENCION"].Hidden = false;
            //e.Layout.Bands[0].Columns["fecha3"].Hidden = true;



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

        private void txt_busqHist_ValueChanged(object sender, EventArgs e)
        {

        }

        private void frm_AyudaPacien_Activated(object sender, EventArgs e)
        {
            if (datos.Rows.Count == 0)
            {
                //datos = atencion.buscar_atenciones(txt_busqHist.Text.Trim(), "", "", "", fechaFacturaHis);
                datos = atencion.buscar_atenciones(txt_busqHist.Text.Trim(), "", "", "");

                grid.DataSource = datos;
                grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
                txt_busqCi.Text = "";
                txtNHabitacion.Text = "";
                txt_busqNom.Text = "";

            }
            else
            {
                return;
            }
        }
    }
}
