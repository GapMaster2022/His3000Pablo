using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using System.IO;

namespace His.Honorarios
{
    public partial class frmExploradorGeneral : Form
    {
        NegHonorarioExplorador Honorario = new NegHonorarioExplorador();
        internal static string cod_medico; //almacena el codigo del medico recibe de frm_ayudas
        internal static string pac_codigo; //almacena el codigo del paciente
        internal static string pac_hc; // almacena el hc del paciente
        public frmExploradorGeneral()
        {
            InitializeComponent();
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmExploradorGeneral_Load(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            dtpFiltroDesde.Value = oPrimerDiaDelMes;
            Bloqueo();
            //CargarPacientes();
            //if (ultraGridHonorarios.Rows.Count == 0)
            //{
            //    MessageBox.Show("Fecha Actual no contiene Registros", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }
        public void CargarPacientes()
        {
            //DataTable Hono = new DataTable();
            //foreach (DataRow item in Hono.Rows)
            //{
            //    ultraGridHonorarios.ActiveRow.Cells[0].Value = item[0].ToString();
            //}
            ultraGridHonorarios.DataSource = Honorario.VerPacientes();
            //Redimensionar();
        }
        public void Redimensionar()
        {
            try
            {
                UltraGridBand bandUno = ultraGridHonorarios.DisplayLayout.Bands[0];

                ultraGridHonorarios.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                ultraGridHonorarios.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                ultraGridHonorarios.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                ultraGridHonorarios.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                ultraGridHonorarios.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                ultraGridHonorarios.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                ultraGridHonorarios.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;
               

                ultraGridHonorarios.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

                //Caracteristicas de Filtro en la grilla
                ultraGridHonorarios.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                ultraGridHonorarios.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                ultraGridHonorarios.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                ultraGridHonorarios.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                ultraGridHonorarios.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                ultraGridHonorarios.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                ultraGridHonorarios.DisplayLayout.Bands[0].Columns[0].Width = 100;
                ultraGridHonorarios.DisplayLayout.Bands[0].Columns[1].Width = 100;
                ultraGridHonorarios.DisplayLayout.Bands[0].Columns[2].Width = 120;
                ultraGridHonorarios.DisplayLayout.Bands[0].Columns[3].Width = 250;
                ultraGridHonorarios.DisplayLayout.Bands[0].Columns[4].Width = 250;
                ultraGridHonorarios.DisplayLayout.Bands[0].Columns[5].Width = 80;
                ultraGridHonorarios.DisplayLayout.Bands[0].Columns[6].Width = 60;
                ultraGridHonorarios.DisplayLayout.Bands[0].Columns[7].Width = 50;
                ultraGridHonorarios.DisplayLayout.Bands[0].Columns[8].Width = 120;
                //ultraGridHonorarios.DisplayLayout.Bands[0].Columns[9].Width = 80;
               

                //Ocultar columnas, que son fundamentales al levantar informacion

                ultraGridHonorarios.DisplayLayout.Bands[0].Columns[9].Hidden = true;
                ultraGridHonorarios.DisplayLayout.Bands[0].Columns[10].Hidden = true;
                //ultraGridHonorarios.DisplayLayout.Bands[0].Columns[6].Hidden = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void Bloqueo()
        {
            txthc.Enabled = false;
            btnf1.Enabled = false;
            txtmedico.Enabled = false;
            btnf1med.Enabled = false;
            txtvale.Enabled = false;
            txtfactura.Enabled = false;
        }

        private void chkfecha_CheckedChanged(object sender, EventArgs e)
        {
            if (chkfecha.Checked == true)
            {
                dtpFiltroDesde.Enabled = true;
                dtpFiltroHasta.Enabled = true;
            }
            else
            {
                dtpFiltroDesde.Enabled = false;
                dtpFiltroHasta.Enabled = false;
            }
        }

        private void chkhc_CheckedChanged(object sender, EventArgs e)
        {
            if(chkhc.Checked == true)
            {
                txthc.Enabled = true;
                btnf1.Enabled = true;
            }
            else
            {
                txthc.Enabled = false;
                btnf1.Enabled = false;
            }
        }

        private void chkmedico_CheckedChanged(object sender, EventArgs e)
        {
            if(chkmedico.Checked == true)
            {
                txtmedico.Enabled = true;
                btnf1med.Enabled = true;
            }
            else
            {
                txtmedico.Enabled = false;
                btnf1med.Enabled = false;
            }
        }

        private void chkVale_CheckedChanged(object sender, EventArgs e)
        {
            if(chkVale.Checked == true)
            {
                txtvale.Enabled = true;
            }
            else
            {
                txtvale.Enabled = false;
            }
        }

        private void chkFactura_CheckedChanged(object sender, EventArgs e)
        {
            if(chkFactura.Checked == true)
            {
                txtfactura.Enabled = true;
            }
            else
            {
                txtfactura.Enabled = false;
            }
        }

        private void toolStripEliminar_Click(object sender, EventArgs e)
        {
            if(ultraGridHonorarios.Selected.Rows.Count > 0)
            {
                UltraGridRow Fila = ultraGridHonorarios.ActiveRow;
                string pdd_codigo, pro_codigo, pro_descripcion, valor, cantidad, iva;
                if (MessageBox.Show("¿Está Seguro de Eliminarlo?", "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                == DialogResult.Yes)
                {
                    //11 columna de cue_codigo
                    DataTable TablaPedidoRecuperado = Honorario.RecuperarPedido(Fila.Cells[11].Value.ToString());
                    if(TablaPedidoRecuperado.Rows.Count < 0)
                    {
                        MessageBox.Show("No se encontro Pedido", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); //no se encontro pedido de acuerdo con codigo de pedido dentro de cuentapacientes
                        return;
                    }
                    else
                    {
                        //Creamos el Nro de pedido devolucion
                        try
                        {
                            Honorario.GuardarPedidoDevolucion(Fila.Cells[11].Value.ToString(), Convert.ToString(His.Entidades.Clases.Sesion.codUsuario));
                            foreach (DataRow item in TablaPedidoRecuperado.Rows)
                            {
                                pdd_codigo = item[0].ToString();
                                pro_codigo = item[1].ToString();
                                pro_descripcion = item[2].ToString();
                                cantidad = item[3].ToString();
                                valor = item[4].ToString();
                                iva = item[5].ToString();
                                Honorario.GuardarPedidoDevolucionDetalle(pro_codigo, pro_descripcion, cantidad, valor, iva, pdd_codigo);
                            }
                            //Eliminamos de las demas tablas
                            Honorario.Eliminar(Fila.Cells[11].Value.ToString());
                            MessageBox.Show("Datos Eliminados Correctamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Algo Ocurrio al Eliminar.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw;
                        }
                        //Insertamos de Pedido detalle a Pedido devolucion detalle
                        
                    }
                }
            }
            
        }

        private void toolStripButtonExportar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    this.ultraGridExcelExporter1.Export(ultraGridHonorarios, PathExcel);
                    MessageBox.Show("Exportación Exitosa en... " + PathExcel);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
        }
        private String FindSavePath()
        {
            Stream myStream;
            string myFilepath = null;
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        myFilepath = saveFileDialog1.FileName;
                        myStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myFilepath;
        }

        private void btnf1med_Click(object sender, EventArgs e)
        {
            frm_Ayudas x = new frm_Ayudas();
            frm_Ayudas.medico = true;
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void X_FormClosed(object sender, FormClosedEventArgs e)
        {
            txtmedico.Text = cod_medico;
            frm_Ayudas.medico = false;
        }

        private void btnf1_Click(object sender, EventArgs e)
        {
            frm_ayudapac x = new frm_ayudapac();
            frm_ayudapac.paciente = true;
            x.Show();
            x.FormClosed += X_FormClosed1;
        }

        private void X_FormClosed1(object sender, FormClosedEventArgs e)
        {
            txthc.Text = pac_codigo;
            frm_ayudapac.paciente = false;
        }

        private void toolStripButtonBuscar_Click(object sender, EventArgs e)
        {
            ControlFiltros();
        }
        public void ControlFiltros()
        {
            frmTiempodeEspera x = new frmTiempodeEspera();
            DataTable TablaHonorario = new DataTable();
            #region 1
            //Filtro por Fecha
            if(dtpFiltroDesde.Value > dtpFiltroHasta.Value)
            {
                MessageBox.Show("Fecha \"Desde\" no puede ser mayor a Fecha \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (chkfecha.Checked == true && chkhc.Checked == false && chkmedico.Checked == false
                && chkVale.Checked == false && chkFactura.Checked == false)
            {
                try
                {
                    errorProvider1.Clear();
                    ultraGridHonorarios.Visible = false;
                    if(dtpFiltroHasta.Value < DateTime.Now)
                    {
                        dtpFiltroHasta.Value = dtpFiltroHasta.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                    }
                    ultraGridHonorarios.DataSource = Honorario.FiltroFecha(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value);
                    for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                    {
                        if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                        {
                            ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                            ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                        }
                        if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                        {
                            ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                        }
                    }
                    x.ShowDialog();
                    ultraGridHonorarios.Visible = true;
                    return;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Algo Ocurrio al Consultar Registros, Intente nuevamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show(ex.Message);
                }
            }
            //Filtro por Hc
            if (chkfecha.Checked == false && chkhc.Checked == true && chkmedico.Checked == false
                && chkVale.Checked == false && chkFactura.Checked == false)
            {
                try
                {
                    if(txthc.Text == "")
                    {
                        errorProvider1.SetError(txthc, "Elija Hc del Paciente");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroHc(txthc.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Consulte con el Administrador.\r\nMás detalles: " + ex.Message, "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Filtro por Medico
            if (chkfecha.Checked == false && chkhc.Checked == false && chkmedico.Checked == true
                && chkVale.Checked == false && chkFactura.Checked == false)
            {
                try
                {
                    if(txtmedico.Text == "")
                    {
                        errorProvider1.SetError(txtmedico, "Elija Código de Médico");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroMedico(txtmedico.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Consulte con el Administrador.\r\nMás detelles: " +ex.Message, "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Filtro por Nro de Vale
            if (chkfecha.Checked == false && chkhc.Checked == false && chkmedico.Checked == false
                && chkVale.Checked == true && chkFactura.Checked == false)
            {
                try
                {
                    if(txtvale.Text == "")
                    {
                        errorProvider1.SetError(txtvale, "Ingrese Nro de Vale");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroVale(txtvale.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Consulte con el Administrador.\r\nMás detelles: " + ex.Message, "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Filtro por Factura
            if (chkfecha.Checked == false && chkhc.Checked == false && chkmedico.Checked == false
                && chkVale.Checked == false && chkFactura.Checked == true)
            {
                try
                {
                    if(txtfactura.Text == "")
                    {
                        errorProvider1.SetError(txtfactura, "Ingrese Nro de Factura");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroFactura(txtfactura.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Consulte con el Administrador.\r\nMás detelles: " + ex.Message, "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
            #region 2
            //Filtro por Fecha y Hc
            if (chkfecha.Checked == true && chkhc.Checked == true && chkmedico.Checked == false
                && chkVale.Checked == false && chkFactura.Checked == false)
            {
                try
                {
                    if(txthc.Text == "")
                    {
                        errorProvider1.SetError(txthc, "Eliga Hc del Paciente");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroFecha_Hc(dtpFiltroDesde.Value.ToString(), dtpFiltroHasta.Value.ToString(), txthc.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Consulte con el Administrador.\r\nMás detelles: " + ex.Message, "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Filtro por Fecha y Medico
            if (chkfecha.Checked == true && chkhc.Checked == false && chkmedico.Checked == true
                && chkVale.Checked == false && chkFactura.Checked == false)
            {
                try
                {
                    if(txtmedico.Text == "")
                    {
                        errorProvider1.SetError(txtmedico, "Eliga Codigo de Médico");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroFecha_Medico(dtpFiltroDesde.Value.ToString(), dtpFiltroHasta.Value.ToString(), txtmedico.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Consulte con el Administrador.\r\nMás detelles: " + ex.Message, "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Filtro por Fecha y Nro Vale
            if (chkfecha.Checked == true && chkhc.Checked == false && chkmedico.Checked == false
                && chkVale.Checked == true && chkFactura.Checked == false)
            {
                try
                {
                    if(txtvale.Text == "")
                    {
                        errorProvider1.SetError(txtvale, "Ingrese Nro de Vale.");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroFecha_Vale(dtpFiltroDesde.Value.ToString(), dtpFiltroHasta.Value.ToString(), txtvale.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Consulte con el Administrador.\r\nMás detelles: " + ex.Message, "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Filtro por Fecha y Nro Factura
            if (chkfecha.Checked == true && chkhc.Checked == false && chkmedico.Checked == false
                && chkVale.Checked == false && chkFactura.Checked == true)
            {
                try
                {
                    if(txtfactura.Text == "")
                    {
                        errorProvider1.SetError(txtfactura, "Ingrese Nro de Factura");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroFecha_Factura(dtpFiltroDesde.Value.ToString(), dtpFiltroHasta.Value.ToString(), txtfactura.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Intente nuevamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Filtro por HC y Medico
            if (chkfecha.Checked == false && chkhc.Checked == true && chkmedico.Checked == true
                && chkVale.Checked == false && chkFactura.Checked == false)
            {
                try
                {
                    if(txthc.Text == "")
                    {
                        errorProvider1.SetError(txthc, "Elija Hc de Paciente");
                    }
                    else if(txtmedico.Text == "")
                    {
                        errorProvider1.SetError(txtmedico, "Elija Codigo de Médico");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroHc_Medico(txthc.Text, txtmedico.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Intente nuevamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Filtro por Hc y Nro Vale
            if (chkfecha.Checked == false && chkhc.Checked == true && chkmedico.Checked == false
                && chkVale.Checked == true && chkFactura.Checked == false)
            {
                try
                {
                    if (txthc.Text == "")
                    {
                        errorProvider1.SetError(txthc, "Elija Hc de Paciente");
                    }
                    else if (txtvale.Text == "")
                    {
                        errorProvider1.SetError(txtvale, "Ingrese Nro de Vale");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroHc_Vale(txthc.Text, txtvale.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Intente nuevamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Filtro por Hc y Nro Factura
            if (chkfecha.Checked == false && chkhc.Checked == true && chkmedico.Checked == false
                && chkVale.Checked == false && chkFactura.Checked == true)
            {
                try
                {
                    if (txthc.Text == "")
                    {
                        errorProvider1.SetError(txthc, "Elija Hc de Paciente");
                    }
                    else if (txtfactura.Text == "")
                    {
                        errorProvider1.SetError(txtfactura, "Ingrese Nro de Factura");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroHc_Factura(txthc.Text, txtfactura.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Intente nuevamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Filtro por Medico y Nro Vale
            if (chkfecha.Checked == false && chkhc.Checked == false && chkmedico.Checked == true
                && chkVale.Checked == true && chkFactura.Checked == false)
            {
                try
                {
                    if (txtmedico.Text == "")
                    {
                        errorProvider1.SetError(txtmedico, "Elija Código de Médico");
                    }
                    else if (txtvale.Text == "")
                    {
                        errorProvider1.SetError(txtvale, "Ingrese Nro de Vale");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroMedico_Vale(txtmedico.Text, txtvale.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Intente nuevamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Filtro por Medico y Nro Factura
            if (chkfecha.Checked == false && chkhc.Checked == false && chkmedico.Checked == true
               && chkVale.Checked == false && chkFactura.Checked == true)
            {
                try
                {
                    if (txtmedico.Text == "")
                    {
                        errorProvider1.SetError(txtmedico, "Elija Código de Médico");
                    }
                    else if (txtfactura.Text == "")
                    {
                        errorProvider1.SetError(txtfactura, "Ingrese Nro de Factura");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroMedico_Factura(txtmedico.Text, txtfactura.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Intente nuevamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Filtro por Nro de Vale y Nro de Factura
            if (chkfecha.Checked == false && chkhc.Checked == false && chkmedico.Checked == true
               && chkVale.Checked == false && chkFactura.Checked == true)
            {
                try
                {
                    if (txtvale.Text == "")
                    {
                        errorProvider1.SetError(txtvale, "Ingrese Nro de Vale");
                    }
                    else if (txtfactura.Text == "")
                    {
                        errorProvider1.SetError(txtfactura, "Ingrese Nro de Factura");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroVale_Factura(txtvale.Text, txtfactura.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Intente nuevamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion 2
            #region 3
            //Filtro Fecha, Hc y Medico
            if (chkfecha.Checked == true && chkhc.Checked == true && chkmedico.Checked == true
                && chkVale.Checked == false && chkFactura.Checked == false)
            {
                try
                {
                    if (txthc.Text == "")
                    {
                        errorProvider1.SetError(txthc, "Eliga Hc del Paciente");
                    }
                    else if(txtmedico.Text == "")
                    {
                        errorProvider1.SetError(txtmedico, "Elija Código de Médico");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroFecha_Hc_Medico(dtpFiltroDesde.Value.ToString(), dtpFiltroHasta.Value.ToString(), txthc.Text, txtmedico.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Intente nuevamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Filtro por Fecha, Hc, Nro Vale
            if (chkfecha.Checked == true && chkhc.Checked == true && chkmedico.Checked == false
                && chkVale.Checked == true && chkFactura.Checked == false)
            {
                try
                {
                    if (txthc.Text == "")
                    {
                        errorProvider1.SetError(txthc, "Eliga Hc del Paciente");
                    }
                    else if (txtvale.Text == "")
                    {
                        errorProvider1.SetError(txtvale, "Ingrese Nro de Vale");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroFecha_Hc_Vale(dtpFiltroDesde.Value.ToString(), dtpFiltroHasta.Value.ToString(), txthc.Text, txtvale.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Intente nuevamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Filtro por Fecha, Hc y Nro de Factura
            if (chkfecha.Checked == true && chkhc.Checked == true && chkmedico.Checked == false
                && chkVale.Checked == false && chkFactura.Checked == true)
            {
                try
                {
                    if (txthc.Text == "")
                    {
                        errorProvider1.SetError(txthc, "Eliga Hc del Paciente");
                    }
                    else if (txtfactura.Text == "")
                    {
                        errorProvider1.SetError(txtfactura, "Ingrese Nro de Factura");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroFecha_Hc_Factura(dtpFiltroDesde.Value.ToString(), dtpFiltroHasta.Value.ToString(), txthc.Text, txtfactura.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Intente nuevamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Filtro por Fecha, Medico y Nro Vale
            if (chkfecha.Checked == true && chkhc.Checked == false && chkmedico.Checked == true
                && chkVale.Checked == true && chkFactura.Checked == false)
            {
                try
                {
                    if (txtmedico.Text == "")
                    {
                        errorProvider1.SetError(txtmedico, "Eliga Código de Médico");
                    }
                    else if (txtvale.Text == "")
                    {
                        errorProvider1.SetError(txtvale, "Ingrese Nro de Vale");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroFecha_Medico_Vale(dtpFiltroDesde.Value.ToString(), dtpFiltroHasta.Value.ToString(), txtmedico.Text, txtvale.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Intente nuevamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Filtro por Fecha, Medico y Nro de Factura
            if (chkfecha.Checked == true && chkhc.Checked == false && chkmedico.Checked == true
                && chkVale.Checked == false && chkFactura.Checked == true)
            {
                try
                {
                    if (txtmedico.Text == "")
                    {
                        errorProvider1.SetError(txtmedico, "Eliga Código de Médico");
                    }
                    else if (txtfactura.Text == "")
                    {
                        errorProvider1.SetError(txtfactura, "Ingrese Nro de Factura");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroFecha_Medico_Factura(dtpFiltroDesde.Value.ToString(), dtpFiltroHasta.Value.ToString(), txtmedico.Text, txtfactura.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Intente nuevamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Filtro por Fecha, Nro Vale y Nro Factura
            if (chkfecha.Checked == true && chkhc.Checked == false && chkmedico.Checked == false
                && chkVale.Checked == true && chkFactura.Checked == true)
            {
                try
                {
                    if (txtvale.Text == "")
                    {
                        errorProvider1.SetError(txtvale, "Ingrese Nro de Vale");
                    }
                    else if (txtfactura.Text == "")
                    {
                        errorProvider1.SetError(txtfactura, "Ingrese Nro de Factura");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroFecha_Vale_Factura(dtpFiltroDesde.Value.ToString(), dtpFiltroHasta.Value.ToString(), txtvale.Text, txtfactura.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Intente nuevamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //FILTRO POR HC, MEDICO Y NRO VALE 
            if (chkfecha.Checked == false && chkhc.Checked == true && chkmedico.Checked == true
                && chkVale.Checked == true && chkFactura.Checked == false)
            {
                try
                {
                    if (txthc.Text == "")
                    {
                        errorProvider1.SetError(txtvale, "Elija el Hc del Paciente");
                    }
                    else if (txtmedico.Text == "")
                    {
                        errorProvider1.SetError(txtfactura, "Elija Código del Médico");
                    }
                    else if(txtvale.Text == "")
                    {
                        errorProvider1.SetError(txtvale, "Ingrese el Nro de Vale");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        ultraGridHonorarios.Visible = false;
                        ultraGridHonorarios.DataSource = Honorario.FiltroHc_Medico_Vale(txthc.Text, txtmedico.Text, txtvale.Text);
                        for (int i = 0; i < ultraGridHonorarios.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[7].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["cuenta"].Value = true;
                                ultraGridHonorarios.Rows[i].Cells["hono"].Value = true;
                            }
                            if (Convert.ToDouble(ultraGridHonorarios.Rows[i].Cells[9].Value.ToString()) > 0)
                            {
                                ultraGridHonorarios.Rows[i].Cells["tari"].Value = true;
                            }
                        }
                        x.ShowDialog();
                        ultraGridHonorarios.Visible = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Algo Ocurrio al Consultar Registros, Intente nuevamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion 3

        }
        private static void OnlyNumber(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789" + Convert.ToChar(8);
            }
            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtvale_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }

        private void txtfactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }

        private void ultraGridHonorarios_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ultraGridHonorarios.DisplayLayout.Bands[0];

            ultraGridHonorarios.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            //ultraGridHonorarios.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            ultraGridHonorarios.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            ultraGridHonorarios.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            //bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            //bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            //ultraGridHonorarios.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            //ultraGridHonorarios.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            //ultraGridHonorarios.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;
            ultraGridHonorarios.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGridHonorarios.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGridHonorarios.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGridHonorarios.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            ultraGridHonorarios.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

            //Dimension los registros
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[0].Width = 100;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[1].Width = 100;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[2].Width = 120;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[3].Width = 250;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[4].Width = 250;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[5].Width = 80;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[6].Width = 60;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[7].Width = 50;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[8].Width = 200;

            //procedimiento para bloquear las columnas que no deben ser editadas
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.NoEdit;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[1].CellActivation = Activation.NoEdit;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[2].CellActivation = Activation.NoEdit;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[3].CellActivation = Activation.NoEdit;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[4].CellActivation = Activation.NoEdit;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[5].CellActivation = Activation.NoEdit;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[6].CellActivation = Activation.NoEdit;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[7].CellActivation = Activation.NoEdit;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[8].CellActivation = Activation.NoEdit;
            //ultraGridHonorarios.DisplayLayout.Bands[0].Columns[9].Width = 80;


            //Ocultar columnas, que son fundamentales al levantar informacion

            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[9].Hidden = true;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[10].Hidden = true;
            ultraGridHonorarios.DisplayLayout.Bands[0].Columns[11].Hidden = true;
            //agregamos las columnas checks
            UltraGridColumn columHonorario = e.Layout.Bands[0].Columns.Add("hono", "Honorario");
            UltraGridColumn ColumnCuenta = e.Layout.Bands[0].Columns.Add("cuenta", "Cuenta Paciente");
            UltraGridColumn ColumnTarifario = e.Layout.Bands[0].Columns.Add("tari", "Tarifario");
            //Honorario
            columHonorario.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            columHonorario.CellActivation = Activation.NoEdit;
            columHonorario.Header.VisiblePosition = 11;
            //Cuenta Paciente
            ColumnCuenta.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            ColumnCuenta.CellActivation = Activation.NoEdit;
            ColumnCuenta.Header.VisiblePosition = 12;
            //Tarifario
            ColumnTarifario.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            ColumnTarifario.CellActivation = Activation.NoEdit;
            ColumnTarifario.Header.VisiblePosition = 13;

        }
    }
}
