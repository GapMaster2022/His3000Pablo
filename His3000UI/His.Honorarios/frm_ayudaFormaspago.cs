using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;


namespace His.Honorarios
{
    public partial class frm_ayudaFormaspago : Form
    {
        Datos.Atencion pagos = new His.Honorarios.Datos.Atencion();
        DataTable datos = new DataTable();
        public TextBox txtcodigo = new TextBox();
        public TextBox txtdescripcion = new TextBox();
        public string tipo = "";
        public string columnacodigo = "CODIGO";
        public string columnadescripcion = "DESCRIPCION";

        public frm_ayudaFormaspago(string tipo_empresa,string atencion)
        {
            InitializeComponent();
            tipo = tipo_empresa;
            cargar_datos(tipo_empresa,atencion);
            cargar_combo_tipo_pagos();
        }
        public void cargar_combo_tipo_pagos()
        {

            DataSet dts_combos = new DataSet();
            dts_combos = pagos.combo_tipo_pagos();

            cmbpagos.DataSource = dts_combos.Tables[0];
            cmbpagos.DisplayMember = dts_combos.Tables[0].Columns["TIF_NOMBRE"].ColumnName.ToString();
            cmbpagos.ValueMember = dts_combos.Tables[0].Columns["TIF_CODIGO"].ColumnName.ToString();

        }

        public void cargar_datos(string tipo_empresa,string atencion)
        {
            if (tipo == "3")
            {
                ultraGroupBox1.Visible = true;
            }

            if (tipo_empresa == "1")
            {
                datos = pagos.cargar_aseguradoras_empresas(tipo_empresa, atencion);
                grid.DataSource = datos;
                grid.DisplayLayout.Bands[0].Columns["ASE_NOMBRE"].Width = 250;
                grid.DisplayLayout.Bands[0].Columns["CAT_NOMBRE"].Width = 200;
                grid.DisplayLayout.Bands[0].Columns["ASE_NOMBRE"].Header.Caption = "ASEGURADORA";
                grid.DisplayLayout.Bands[0].Columns["CAT_NOMBRE"].Header.Caption = "CONVENIO";
                grid.DisplayLayout.Bands[0].Columns["FOR_COMISION"].Header.Caption = "COMISION";
                grid.DisplayLayout.Bands[0].Columns["FOR_REFERIDO"].Header.Caption = "REFERIDO";
               


             
            }
            if (tipo_empresa == "2")
            {
                datos = pagos.cargar_aseguradoras_empresas(tipo_empresa, atencion);
                grid.DataSource = datos;
                grid.DisplayLayout.Bands[0].Columns["ASE_NOMBRE"].Width = 250;
                grid.DisplayLayout.Bands[0].Columns["CAT_NOMBRE"].Width = 200;
                grid.DisplayLayout.Bands[0].Columns["ASE_NOMBRE"].Header.Caption = "ASEGURADORA";
                grid.DisplayLayout.Bands[0].Columns["CAT_NOMBRE"].Header.Caption = "CONVENIO";
                grid.DisplayLayout.Bands[0].Columns["FOR_COMISION"].Header.Caption = "COMISION";
                grid.DisplayLayout.Bands[0].Columns["FOR_REFERIDO"].Header.Caption = "REFERIDO";

            }
            //else
            //{
            //    datos = pagos.cargar_aseguradoras_empresas(tipo_empresa, atencion);
            //    grid.DataSource = datos;
            //    grid.DisplayLayout.Bands[0].Columns["ASE_NOMBRE"].Width = 250;
            //    grid.DisplayLayout.Bands[0].Columns["CAT_NOMBRE"].Width = 200;
            //    grid.DisplayLayout.Bands[0].Columns["ASE_NOMBRE"].Header.Caption = "ASEGURADORA";
            //    grid.DisplayLayout.Bands[0].Columns["CAT_NOMBRE"].Header.Caption = "CONVENIO";
            //    grid.DisplayLayout.Bands[0].Columns["FOR_COMISION"].Header.Caption = "COMISION";
            //    grid.DisplayLayout.Bands[0].Columns["FOR_REFERIDO"].Header.Caption = "REFERIDO";
            //}

        }
        private void frm_ayudaFormaspago_Load(object sender, EventArgs e)
        {
           

        }

        private void grid_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            try
            {
                if (tipo== "3")
                {
                    enviarCodigo();
                }
                if (tipo == "2")
                {
                    enviarCodigo_aseguradora_empresa();
                }
                if (tipo == "1")
                {
                    enviarCodigo_aseguradora_empresa();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void enviarCodigo()
        {
            try
            {
                txtcodigo.Text = grid.ActiveRow.Cells[columnacodigo].Value.ToString();
              
                txtdescripcion.Text = grid.ActiveRow.Cells[columnadescripcion].Value.ToString();
             
            

                this.Close();
            }
            catch (Exception es)
            {
            }
        }
        private void enviarCodigo_aseguradora_empresa()
        {
            try
            {   txtcodigo.Text = grid.ActiveRow.Cells["FOR_CODIGO"].Value.ToString();
                txtdescripcion.Text = grid.ActiveRow.Cells["FOR_DESCRIPCION"].Value.ToString();
               this.Close();
            }
            catch (Exception es)
            {
            }
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

            UltraGridBand bandUno = grid.DisplayLayout.Bands[0];

            grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            if (tipo == "3")
                e.Layout.Bands[0].Columns["CODIGO"].Hidden = true;
            else
            {
                e.Layout.Bands[0].Columns["ATE_CODIGO"].Hidden = true;
                e.Layout.Bands[0].Columns["FOR_CODIGO"].Hidden = true;
                e.Layout.Bands[0].Columns["FOR_DESCRIPCION"].Hidden = true;
            }
          

            
           
        }

        private void grid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (tipo == "3")
                    {
                        enviarCodigo();
                    }
                    if (tipo == "2")
                    {
                        enviarCodigo_aseguradora_empresa();
                    }
                    if (tipo == "1")
                    {
                        enviarCodigo_aseguradora_empresa();
                    }
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

        private void cmbpagos_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string codigo_tipo = cmbpagos.SelectedValue.ToString();
                if (tipo == "3")
                {
                    datos = pagos.cargar_formas_pago(codigo_tipo.Trim());
                    grid.DataSource = datos;
                    grid.DisplayLayout.Bands[0].Columns["DESCRIPCION"].Width = 350;

                }
            }
            catch (Exception ex)
            {

            }
           
        }
   
    }
}
