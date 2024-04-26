using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Core.Datos;
using Recursos;
using His.Parametros;
using His.Negocio;
using frm_Ayudas = His.Honorarios.frm_Ayudas;
using Infragistics.Win.UltraWinGrid;
using System.IO;

using Infragistics.Win.UltraWinTabControl;
using His.Entidades.General;
using System.Diagnostics;

namespace CuentaPaciente
{
    public partial class frmAyudaTarifarios : Form
    {

        TextBox text = new TextBox();
        string medicoCod = "0";
        private MEDICOS medico;
        public   decimal porcentajeFacturar = 100;
        public string cantidad = "0";
        public string valorU = "0";
        public string referencia = "0";
        public string descripcion = "0";
        public DateTimePicker fecha =new DateTimePicker();
        public    DataGridView lista = new DataGridView();
        public DataGridViewRow fila=null;

        public frmAyudaTarifarios(string codigoMed,DateTimePicker fechaAlta)
        {
            InitializeComponent();
            cargarGrid();
            text.Text = "0";
            dtpFechaPedido.Value = fechaAlta.Value;
            medicoCod = codigoMed;
            if (codigoMed != "")
            {
                CargarMedico(Convert.ToInt16(codigoMed));
            }
            else
                medicoCod = "0";


        }

        private void frmAyudaTarifarios_Load(object sender, EventArgs e)
        {
            toolStripButton1.Image = Archivo.imgBtnRestart;
            int a = 0;
        }
        public void cargarGrid()
        {

            DataTable datos = new DataTable();
            datos = NegTarifario.recuperar_Tarifarios("1", "a");
            grid.DataSource = datos;
        }

        private void btn_Buscar_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(this.optCodigo.Checked.ToString()))
            {
            //por  referencia
                DataTable datos = new DataTable();
                datos = NegTarifario.recuperar_Tarifarios(txt_Nombre.Text.Trim(), "0");
                grid.DataSource = datos;
            }
            else if (Convert.ToBoolean(this.optDescripcion.Checked.ToString()))
            {
       //por  descripcion

                DataTable datos = new DataTable();
                datos = NegTarifario.recuperar_Tarifarios("1", txt_Nombre.Text.Trim());
                grid.DataSource = datos;
            }
          
        }

        private void ugridTarifarios_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
    
        }

        private void grid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = grid.DisplayLayout.Bands[0];
            bandUno.Columns["tad_nombre"].Header.Caption = "DESCRIPCION";
            bandUno.Columns["tad_REFERENCIA"].Header.Caption = "CODIGO";
            bandUno.Columns["Honorario"].Header.Caption = "HONORARIO";
            bandUno.Columns["anestecia"].Header.Caption = "ANESTESIA";

            bandUno.Columns["tad_nombre"].Width = 400;
            bandUno.Columns["Control"].Hidden = true;
            grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
        
        }

        private void grid_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            try
            {
                enviarCodigo();
            }
            catch (Exception ex)
            {

            }
        }
        private void enviarCodigo()
        { 
               decimal vunitario =Convert.ToDecimal( grid.ActiveRow.Cells["Honorario"].Value.ToString());
            int  vcantidad=Convert.ToInt16(txt_Cantidad.Text.Trim());

             bool uvr = false;
          
            if (Convert.ToBoolean(this.tpuvr.Checked.ToString()))
                uvr = true;
          
                if (txt_valor1.Text == "0")
                {
                    if (uvr == true)
                        vunitario =Convert.ToDecimal( grid.ActiveRow.Cells["Honorario"].Value.ToString());
                    else
                        vunitario = Convert.ToDecimal(grid.ActiveRow.Cells["anestecia"].Value.ToString());
                }
                else
                    vunitario = Convert.ToDecimal(txt_valor1.Text.Trim());

               decimal valor_total=(vunitario * Convert.ToInt16(porcentajeFacturar)) / 100;      
            
            DataGridViewRow dt = new DataGridViewRow();
                    dt.CreateCells(gridAñadir);
                    dt.Cells[0].Value =grid.ActiveRow.Cells["TAD_REFERENCIA"].Value.ToString(); 
                    dt.Cells[1].Value =grid.ActiveRow.Cells["tad_nombre"].Value.ToString();
                    dt.Cells[2].Value=Convert.ToString(vunitario);
                    dt.Cells[3].Value=Convert.ToString(vcantidad);
                    dt.Cells[4].Value=Convert.ToString(porcentajeFacturar);
                    dt.Cells[5].Value = Convert.ToString(valor_total*vcantidad);
                    dt.Cells[6].Value= Convert.ToString(dtpFechaPedido.Value);
                    dt.Cells[7].Value = medicoCod;
                    gridAñadir.Rows.Add(dt);
                    string control = grid.ActiveRow.Cells["Control"].Value.ToString().Trim();
                    if (grid.ActiveRow.Cells["Control"].Value.ToString().Trim() != "")
                    {
                        dt = new DataGridViewRow();
                        dt.CreateCells(gridAñadir);
                        dt.Cells[0].Value = grid.ActiveRow.Cells["TAD_REFERENCIA"].Value.ToString();
                        dt.Cells[1].Value = "1.5% EQUIPO DIGITAL";
                        dt.Cells[2].Value = Convert.ToString(vunitario);
                        dt.Cells[3].Value = Convert.ToString(vcantidad);
                        dt.Cells[4].Value = "1.5";
                        decimal porcentaje = 1.5M;
                        decimal valor_totalP = (vunitario * porcentaje) / 100;
                        dt.Cells[5].Value = Convert.ToString(valor_totalP * vcantidad);
                        dt.Cells[6].Value = Convert.ToString(dtpFechaPedido.Value);
                        dt.Cells[7].Value = medicoCod;
                        gridAñadir.Rows.Add(dt);


                    }
                 
                    txt_Cantidad.Text = "1";
                    txt_valor1.Text = "0";
                   
                  

     
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
           
            try
            {
                enviarCodigo();
            }
            catch (Exception ex)
            {

            }

        }

        private void tpuvr_Click(object sender, EventArgs e)
        {
            tpanestesia.Checked = false;
            tpuvr.Checked = true;
        }

        private void tpanestesia_Click(object sender, EventArgs e)
        {
            tpuvr.Checked = false;
            tpanestesia.Checked = true;
        }

        private void tsmiCienPor_Click(object sender, EventArgs e)
        {
            tsmiSetentaCincoPor.Checked = false;
            tsmiCienPor.Checked = true;
            tsmiCincuentaPor.Checked = false;
            tsmiVeinteCincoPor.Checked = false;
            tsmiOtro.Checked = false;
            porcentajeFacturar = Convert.ToInt16(tsmiCienPor.Tag);
            tsTxtPorcentajeCobrar.Visible = false; 
        }

        private void tsmiSetentaCincoPor_Click(object sender, EventArgs e)
        {
            tsmiSetentaCincoPor.Checked = true;
            tsmiCienPor.Checked = false;
            tsmiCincuentaPor.Checked = false;
            tsmiVeinteCincoPor.Checked = false;
            tsmiOtro.Checked = false;
            porcentajeFacturar = Convert.ToInt16(tsmiSetentaCincoPor.Tag);
            tsTxtPorcentajeCobrar.Visible = false; 
        }

        private void tsmiCincuentaPor_Click(object sender, EventArgs e)
        {
            tsmiSetentaCincoPor.Checked = false;
            tsmiCienPor.Checked = false;
            tsmiCincuentaPor.Checked = true;
            tsmiVeinteCincoPor.Checked = false;
            tsmiOtro.Checked = false;
            porcentajeFacturar = Convert.ToInt16(tsmiCincuentaPor.Tag);
            tsTxtPorcentajeCobrar.Visible = false; 
        }

        private void tsmiVeinteCincoPor_Click(object sender, EventArgs e)
        {
            tsmiSetentaCincoPor.Checked = false;
            tsmiCienPor.Checked = false;
            tsmiCincuentaPor.Checked = false;
            tsmiVeinteCincoPor.Checked = true;
            tsmiOtro.Checked = false;
            porcentajeFacturar = Convert.ToInt16(tsmiVeinteCincoPor.Tag);
            tsTxtPorcentajeCobrar.Visible = false; 
        }

        private void tsmiOtro_Click(object sender, EventArgs e)
        {
            tsmiSetentaCincoPor.Checked = false;
            tsmiCienPor.Checked = false;
            tsmiCincuentaPor.Checked = false;
            tsmiVeinteCincoPor.Checked = false;
            tsmiOtro.Checked = true;
            porcentajeFacturar = Convert.ToInt16(tsTxtPorcentajeCobrar.Text);
            tsTxtPorcentajeCobrar.Visible = true; 
        }

        private void tsTxtPorcentajeCobrar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (Char)Keys.Back == e.KeyChar))
                e.Handled = true;
        }

        private void txt_Nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (Convert.ToBoolean(this.optCodigo.Checked.ToString()))
                {
                    //por  referencia
                    DataTable datos = new DataTable();
                    datos = NegTarifario.recuperar_Tarifarios(txt_Nombre.Text.Trim(), "0");
                    grid.DataSource = datos;
                }
                else if (Convert.ToBoolean(this.optDescripcion.Checked.ToString()))
                {
                    //por  descripcion

                    DataTable datos = new DataTable();
                    datos = NegTarifario.recuperar_Tarifarios("1", txt_Nombre.Text.Trim());
                    grid.DataSource = datos;
                }
            }
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;          
                grid.Focus();
            }
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

        private void ultraGroupBox3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            lista = gridAñadir;
            this.Close();
        }

        private void CargarMedico(int codMedico)
        {
            DataTable med = NegMedicos.MedicoIDValida(codMedico);
            if (med.Rows[0][0].ToString() == "7")
            {
                MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            medico = NegMedicos.MedicoID(codMedico);
            if (medico != null)
                toolStripTextBox2.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + "  " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
            else
                toolStripTextBox2.Text = string.Empty;
        }

        private void toolStripTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = new TextBox();
            try
            {
                if ((e.KeyCode == Keys.F1))
                {
                    List<MEDICOS> listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
                    His.Honorarios.frm_Ayudas ayuda = new frm_Ayudas(listaMedicos);
                      ayuda.campoPadre=text;
                    ayuda.ShowDialog();

                    medicoCod = text.Text.Trim();
                  
                    
                    if (ayuda.campoPadre.Text != string.Empty)
                        CargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                }
                else
                {
                    if ((e.KeyCode == Keys.Enter))
                    {
                        if (toolStripTextBox2.Text.Trim() != string.Empty)
                            CargarMedico(Convert.ToInt32(toolStripTextBox2.Text.Trim()));
                        else
                        {
                            toolStripTextBox2.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
        
            List<MEDICOS> listaMedicos = NegMedicos.listaMedicosIncTipoMedico();

            His.Honorarios.frm_Ayudas ayuda = new frm_Ayudas(listaMedicos);
            ayuda.campoPadre = text;
            ayuda.ShowDialog();

            if (ayuda.campoPadre.Text != string.Empty)
                CargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
 
        }

        private void toolStripTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void txt_valor1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (Char)Keys.Back == e.KeyChar))
                e.Handled = true;
        }

        private void txt_Cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (Char)Keys.Back == e.KeyChar))
                e.Handled = true;
        }

    
    }
}
