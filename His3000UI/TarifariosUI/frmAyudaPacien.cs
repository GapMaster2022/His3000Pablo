using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TarifariosUI
{
    public partial class frmAyudaPacien : Form
    {    public string valor="";
        public TextBox campoPadre = new TextBox();
        public TextBox campoAtencion = new TextBox();
        public string columnahabitacion = "HABITACION";
        public string columnafactura = "FACTURA";
        public string columnacontrol = "CONTROL";
        public string columnabuscada = "CODIGO";
        public string columnaAtencion = "ATENCION";
        public string columnaReferido = "referido";
        Atencion atencion = new Atencion();
        DataTable datos = new DataTable();
        public frmAyudaPacien()
        {
            InitializeComponent();
    
        }

        private void frm_AyudaPacien_Load(object sender, EventArgs e)
        {

            datos = atencion.cargar_atenciones();

            grid.DataSource = datos;
            grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
        }
        private void enviarCodigo()
        {
            try
            {
                string atencion = grid.ActiveRow.Cells[columnaAtencion].Value.ToString();
                campoAtencion.Text = atencion;
                string codigo = grid.ActiveRow.Cells[columnabuscada].Value.ToString();
                campoPadre.Text = codigo;

              

                valor = atencion;
           
          

                this.Close();
            }
            catch (Exception es)
            {
            }
        }

        public string atencion_valor()
        {
            return valor;
        }

        private void txt_busqHist_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                datos = atencion.buscar_atenciones(txt_busqHist.Text.Trim(), "", "");

                grid.DataSource = datos;
                grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
                txt_busqCi.Text = "";

                txt_busqNom.Text = "";

            }
        }

    

       

      
        private void frmAyudaPacien_Load(object sender, EventArgs e)
        {
            datos = atencion.cargar_atenciones();

            grid.DataSource = datos;
            grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
      

        }

        private void txt_busqHist_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (txt_busqHist.Text != "")
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    datos = atencion.buscar_atenciones(txt_busqHist.Text.Trim(), "", "");

                    grid.DataSource = datos;
                    grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
                    txt_busqCi.Text = "";

                    txt_busqNom.Text = "";

                }
            }
            else {
                datos = atencion.cargar_atenciones();

                grid.DataSource = datos;
                grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
            }
        }

        private void txt_busqCi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txt_busqCi.Text != "")
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    datos = atencion.buscar_atenciones("", "", txt_busqCi.Text);

                    grid.DataSource = datos;
                    grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;

                    txt_busqHist.Text = "";
                    txt_busqNom.Text = "";

                }
            }
            else
            {
                datos = atencion.cargar_atenciones();

                grid.DataSource = datos;
                grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
            }
        }

        private void txt_busqNom_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (txt_busqNom.Text != "")
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    datos = atencion.buscar_atenciones("", txt_busqNom.Text, "");

                    grid.DataSource = datos;
                    grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
                    txt_busqCi.Text = "";
                    txt_busqHist.Text = "";


                }
            }
            else
            {
                datos = atencion.cargar_atenciones();

                grid.DataSource = datos;
                grid.DisplayLayout.Bands[0].Columns["NOMBRES"].Width = 350;
            }
        }

        private void grid_DoubleClickRow_1(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            try
            {

                enviarCodigo();
            }
            catch (Exception ex)
            {

            }
        }

        private void grid_InitializeLayout_1(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.Bands[0].Columns["referido"].Hidden = true;
            e.Layout.Bands[0].Columns["FACTURA"].Hidden = true;
            e.Layout.Bands[0].Columns["CONTROL"].Hidden = true;
            e.Layout.Bands[0].Columns["FECHA_FACTURA"].Hidden = true;  
            e.Layout.Bands[0].Columns["FECHA_INGRESO"].Hidden = false;
            e.Layout.Bands[0].Columns["FECHA_ALTA"].Hidden = false;
            ////e.Layout.Bands[0].Columns["HABITACION"].Hidden = true;

        }

        private void grid_KeyDown_1(object sender, KeyEventArgs e)
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
