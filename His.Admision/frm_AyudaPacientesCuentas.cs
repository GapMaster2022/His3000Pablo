using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades.Pedidos;
using His.Honorarios.Datos;
using His.Negocio;
using His.Parametros;

namespace His.Admision
{
    public partial class frm_AyudaPacientesCuentas : Form
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
        
        public string columnahabitacion = "HABITACION";
        public string columnafactura = "FACTURA";
        public string columnacontrol = "CONTROL";
        public string columnabuscada = "CODIGO";
        public string columnaAtencion = "ATENCION";
        public string columnaReferido = "referido";
        Atencion atencion = new His.Honorarios.Datos.Atencion();
        DataTable datos = new DataTable();
        FrmLaboratorio frmL = new FrmLaboratorio();
        public frm_AyudaPacientesCuentas()
        {
            InitializeComponent();
            txtfactura.Mask = "000-000-0000000";
        }

        private void frm_AyudaPacien_Load(object sender, EventArgs e)
        {
            
            datos=atencion.cargar_atenciones();
            
             grid.DataSource = datos;
             grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
        }
        private void enviarCodigo()
        {
            try
            {
                campoPadre.Text = grid.ActiveRow.Cells[columnabuscada].Value.ToString();
                campoPadre.Tag = true;
                campoAtencion.Text = grid.ActiveRow.Cells[columnaAtencion].Value.ToString();
                campoAtencion.Tag = true;
                frmL.codigoAtencion = Convert.ToInt32(campoAtencion.Text);
                campoHabitacion.Text = grid.ActiveRow.Cells[columnahabitacion].Value.ToString();
                campoHabitacion.Tag = true;
                txtfactura.Text = grid.ActiveRow.Cells[columnafactura].Value.ToString();
                txtfactura.Tag = true;
                txtNumControl.Text = grid.ActiveRow.Cells[columnacontrol].Value.ToString();
                txtNumControl.Tag = true;
                if (grid.ActiveRow.Cells["fecha1"].Value.ToString() == "")
                    fechafacturacion.Value = Convert.ToDateTime("12/12/2011");
                else
                    fechafacturacion.Value = Convert.ToDateTime(grid.ActiveRow.Cells["fecha1"].Value);
                if (grid.ActiveRow.Cells["fecha3"].Value.ToString() == "")
                    fechaalta.Value = Convert.ToDateTime("12/12/2011");
                else
                    fechaalta.Value = Convert.ToDateTime(grid.ActiveRow.Cells["fecha3"].Value);
                if (grid.ActiveRow.Cells["fecha2"].Value.ToString() == "")
                    fechaingreso.Value = Convert.ToDateTime("12/12/2011");
                else
                    fechaingreso.Value = Convert.ToDateTime(grid.ActiveRow.Cells["fecha2"].Value);
                string refer = grid.ActiveRow.Cells[columnaReferido].Value.ToString();
                if (refer.Trim() == "INSTITUCIONAL")
                {
                    campoReferido.Checked = true;
                }
                if (refer.Trim() == "PRIVADO")
                {
                    campoReferido.Checked = false;
                }
                //if (campoAtencion.Text != null && campoAtencion.Text.Trim() != string.Empty)
                //{
                //    frmCuentaPacientesIess frm = new frmCuentaPacientesIess();
                //    frm.codigoAtencion = Convert.ToInt32(campoAtencion.Text);
                ////}
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
                this.Show();
            }
            catch (Exception ex)
            {

            }
        }

        private void txt_busqHist_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                datos = atencion.buscar_atenciones(txt_busqHist.Text.Trim(), "", "","");

                grid.DataSource = datos;
                grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
                txt_busqCi.Text = "";
                txt_busqNom.Text = "";
            }
        }

        private void txt_busqNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                datos = atencion.buscar_atenciones("", txt_busqNom.Text, "","");

                grid.DataSource = datos;
                grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
                txt_busqCi.Text = "";
                txt_busqHist.Text = "";
          

            }
        }

        private void txt_busqCi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                datos = atencion.buscar_atenciones("", "", txt_busqCi.Text,"");

                grid.DataSource = datos;
                grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
              
                txt_busqHist.Text = "";
                txt_busqNom.Text = "";

            }
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.Bands[0].Columns["CODIGO"].Hidden = true;
            e.Layout.Bands[0].Columns["referido"].Hidden = true;
            e.Layout.Bands[0].Columns["FACTURA"].Hidden = true;
            e.Layout.Bands[0].Columns["CONTROL"].Hidden = true;
            e.Layout.Bands[0].Columns["fecha1"].Hidden = true;
            //e.Layout.Bands[0].Columns["fecha2"].Hidden = true;
            //e.Layout.Bands[0].Columns["fecha3"].Hidden = true;
            //e.Layout.Bands[0].Columns["HABITACION"].Hidden = true;
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
    }
}
