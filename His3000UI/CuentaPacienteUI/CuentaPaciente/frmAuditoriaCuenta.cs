using His.Entidades;
using His.Entidades.Pedidos;
using His.Negocio;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CuentaPaciente
{

    public partial class frmAuditoriaCuenta : Form
    {
        PACIENTES pacienteFactura = new PACIENTES();
        PACIENTES_DATOS_ADICIONALES datosPacienteActual = null;
        ATENCIONES ultimaAtencion = null;
        List<DtoDetalleCuentaPaciente> listaCuenta = new List<DtoDetalleCuentaPaciente>();

        public frmAuditoriaCuenta()
        {
            InitializeComponent();
           // grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            //grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
           // grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
           // grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
           // grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            
          //  grid.DisplayLayout.UseFixedHeaders = true;
           // grid.DisplayLayout.GroupByBox.Prompt = "Arrastra aquí alguna cabecera para agrupar por ella";


            gridN.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            gridN.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            gridN.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            gridN.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            gridN.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
        }
        public frmAuditoriaCuenta(string ateNumero, string hc, string nombrePaciente, string convenio)
        {
            InitializeComponent();
            gridN.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            gridN.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            gridN.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            gridN.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            gridN.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

            try
            {
                
                txt_Atencion.Text = ateNumero;
                txt_Historia_Pc.Text = hc;                
                this.txt_ApellidoH1.Text = nombrePaciente;
                lblConvenio.Text = convenio;
                btnBuscarDatos.Visible = false;
                cargartabla();
                totales();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }


        private void ultraTabControl1_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {

        }

        private void btnBuscarDatos_Click_1(object sender, EventArgs e)
        {
            try
            {
                frmAyudaPacientesFacturacion form = new frmAyudaPacientesFacturacion(true);
                //form.campoPadre = txt_Historia_Pc;
                //form.campoAtencion = txt_Atencion;

                form.ShowDialog();
                ///////////////////////////////
                txt_Historia_Pc.Text = form.campoPadre.Text;
                txt_Atencion.Text = form.campoAtencion.Text;
                this.txt_ApellidoH1.Text = form.campoNombre.Text;
                lblConvenio.Text = form.campoAseguradora.Text;
                cargartabla();
                totales();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void cargartabla()
        {
            //CargarGrid();
            if (txt_Atencion.Text.Trim() != "" && NegFactura.RecuperaAtencion(Convert.ToInt32(txt_Atencion.Text.Trim())))
            {
                DataTable dtValoresCuentas = NegFactura.ProductosFactura(Convert.ToInt32(txt_Atencion.Text.Trim()));
                // grid.DataSource = dtValoresCuentas;
                gridN.DataSource = dtValoresCuentas;
                foreach (var item in gridN.Rows)
                {
                    if (item.Cells["AUDITADA"].Value.ToString() == "")
                    {
                        item.Cells["AUDITADA"].Value = false;
                    }
                }
                //gridN.Columns[].Frozen = true;
                UltraGridColumn c = gridN.DisplayLayout.Bands[0].Columns["CUE_CODIGO"];
                c.Hidden = false;
                c = gridN.DisplayLayout.Bands[0].Columns["RUB_NOMBRE"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["CUE_FECHA"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["USUARIO"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["DEP_NOMBRE"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["PRO_CODIGO"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["CUE_DETALLE"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["CUE_VALOR_UNITARIO"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["CUE_CANTIDAD"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["CUE_VALOR"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["CUE_IVA"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["Descuento"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;

                DataTable auxDT = NegFactura.ObservacioAtencion(Convert.ToInt32(txt_Atencion.Text.Trim()));
           
                     

                foreach (DataRow row in auxDT.Rows)
                {
                    txtObs.Text= (row[0].ToString());
                    txtDerivada.Text = (row[1].ToString());
                }

            }
            else
            {
                MessageBox.Show("No se puede dividir una cuenta ya facturada", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txt_ApellidoH1.Text = "";
                txt_Historia_Pc.Text = "";
                lblConvenio.Text = "";
                txt_Atencion.Text = "";
            }
        }
    
        private void cargarobserva()
            {
            DataTable dtValoresCuentas = NegFactura.ProductosFactura(Convert.ToInt32(txt_Atencion.Text.Trim()));
        }        

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

     

        private void gridN_CellChange(object sender, CellEventArgs e)
        {
            //    if (grid.Rows.Count > 0)
            //  {
            //      try
            //       {
            //   MessageBox.Show(grid.Rows[grid.ActiveRow.Index].Cells["CUE_CODIGO"].Value.ToString());
            //Int32 CueCod = Convert.ToInt32(grid.Rows[grid.ActiveRow.Index].Cells["CUE_CODIGO"].Value.ToString());
            //frmObsAuditoria Form = new frmObsAuditoria();
            //Form.ShowDialog();

            //        }
            //       catch
            //       {
            //       }

            //    }
            if (this.gridN.ActiveRow.Cells["CANT. DIVIDIR"].Text != "")
            {
                string prueba = gridN.ActiveRow.Cells["CANT. DIVIDIR"].Text;
                //prueba = prueba.Split('_', '0');
                if (Convert.ToDouble(this.gridN.ActiveRow.Cells["CANT. DIVIDIR"].Text) == 0)
                {
                    if (Convert.ToBoolean(this.gridN.ActiveRow.Cells["AUDITADA"].Text) == true)
                    {
                        this.gridN.ActiveRow.Cells["CANT. DIVIDIR"].Value = this.gridN.ActiveRow.Cells["CUE_CANTIDAD"].Value;
                    }
                }

                else
                {
                    if (Convert.ToBoolean(this.gridN.ActiveRow.Cells["AUDITADA"].Text) == false)
                        this.gridN.ActiveRow.Cells["CANT. DIVIDIR"].Value = 0;
                }

                if (this.gridN.ActiveRow.Cells["CANT. DIVIDIR"].Text != "")
                {
                    if (Convert.ToDouble(this.gridN.ActiveRow.Cells["CUE_CANTIDAD"].Text) < Convert.ToDouble(this.gridN.ActiveRow.Cells["CANT. DIVIDIR"].Text) || Convert.ToDouble(this.gridN.ActiveRow.Cells["CANT. DIVIDIR"].Text) < 0)
                    {
                        if (Convert.ToDouble(this.gridN.ActiveRow.Cells["CANT. DIVIDIR"].Text) != 0)
                        {
                            MessageBox.Show("Cantidad a dividir no puede ser mayor a la cantidad original.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.gridN.ActiveRow.Cells["CANT. DIVIDIR"].Value = this.gridN.ActiveRow.Cells["CUE_CANTIDAD"].Value;
                        }
                    }
                }

                if (gridN.Rows.Count > 0)
                {
                    try
                    {
                        //MessageBox.Show(gridN.Rows[gridN.ActiveRow.Index].Cells["CUE_CODIGO"].Value.ToString() + " - " + gridN.Rows[gridN.ActiveRow.Index].Cells["AUDITADA"].Value.ToString() + " - " + gridN.Rows[grid.ActiveRow.Index].Cells["OBSERVACION"].Value.ToString());
                        Int32 cue_codigo = Convert.ToInt32(gridN.Rows[gridN.ActiveRow.Index].Cells["CUE_CODIGO"].Value.ToString());
                        String observacion = Convert.ToString(gridN.Rows[gridN.ActiveRow.Index].Cells["OBSERVACION"].Value.ToString());
                        decimal cantidad = Convert.ToDecimal(gridN.Rows[gridN.ActiveRow.Index].Cells["CANT. DIVIDIR"].Text);
                        int codigo = 0;
                        if (gridN.Rows[gridN.ActiveRow.Index].Cells["AUDITADA"].Value.ToString() != "")
                        {
                            if ((Convert.ToBoolean(gridN.Rows[gridN.ActiveRow.Index].Cells["AUDITADA"].Text) == true))
                                codigo = 1;
                        }
                        // MessageBox.Show(cue_codigo + "--" + observacion + "--" + codigo);
                        NegFactura.GuardaAuditoriaCuenta(cue_codigo, observacion, codigo, cantidad);
                    }
                    catch
                    {
                    }
                }

                totales();
            }
        }

        private void gridN_BeforeSelectChange(object sender, BeforeSelectChangeEventArgs e)
        {
            gridN.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExitEditMode);
        }

        private void gridN_AfterCellUpdate(object sender, CellEventArgs e)
        {
            //if (gridN.Rows.Count > 0)
            //{
            //    try
            //    {
            //        //MessageBox.Show(gridN.Rows[gridN.ActiveRow.Index].Cells["CUE_CODIGO"].Value.ToString() + " - " + gridN.Rows[gridN.ActiveRow.Index].Cells["AUDITADA"].Value.ToString() + " - " + gridN.Rows[grid.ActiveRow.Index].Cells["OBSERVACION"].Value.ToString());
            //        Int32 cue_codigo = Convert.ToInt32(gridN.Rows[gridN.ActiveRow.Index].Cells["CUE_CODIGO"].Value.ToString());
            //        String observacion = Convert.ToString(gridN.Rows[gridN.ActiveRow.Index].Cells["OBSERVACION"].Value.ToString());
            //        decimal cantidad = Convert.ToDecimal(gridN.Rows[gridN.ActiveRow.Index].Cells["CANT. DIVIDIR"].Value.ToString());
            //        int codigo = 0;
            //        if (gridN.Rows[gridN.ActiveRow.Index].Cells["AUDITADA"].Value.ToString() != "")
            //        {
            //            if ((Convert.ToBoolean(gridN.Rows[gridN.ActiveRow.Index].Cells["AUDITADA"].Value) == true))
            //                codigo = 1;
            //        }
            //        // MessageBox.Show(cue_codigo + "--" + observacion + "--" + codigo);
            //        NegFactura.GuardaAuditoriaCuenta(cue_codigo, observacion, codigo, cantidad);
            //        totales();
            //    }
            //    catch
            //    {
            //    }
            //}
        }
        
        private void txtObs_TextChanged(object sender, EventArgs e)
        {
            if (txt_Atencion.Text.Trim()!="")
            {
                NegFactura.GuardaObservacionAtencion(Convert.ToInt32(txt_Atencion.Text.Trim()), txtObs.Text.Trim());
            }
            

        }



        private void totales()
        {
            gridN.Refresh();
            double xDesmarcados = 0;
            double xMarcados = 0;
            double xTotal = 0;
            double xIva = NegParametros.ParametroIva();
            foreach (UltraGridRow fila in gridN.Rows)
            {
                
                 xTotal += Convert.ToDouble(fila.Cells["CUE_VALOR"].Value);
                if ((fila.Cells["AUDITADA"].Text == "True"))
                {
                    if (Convert.ToDecimal(fila.Cells["CANT. DIVIDIR"].Text) != 0 && Convert.ToDecimal(fila.Cells["CUE_IVA"].Value.ToString()) == 0)
                        xMarcados += Convert.ToDouble(fila.Cells["CUE_VALOR_UNITARIO"].Value) * Convert.ToDouble(fila.Cells["CANT. DIVIDIR"].Text);
                    else
                        //xMarcados += Convert.ToDouble(fila.Cells["CUE_VALOR"].Value);
                    if (Convert.ToDouble(fila.Cells["CUE_IVA"].Value.ToString()) > 0)
                    {
                        double iva = ((Convert.ToDouble(fila.Cells["CUE_VALOR_UNITARIO"].Value) * Convert.ToDouble(fila.Cells["CANT. DIVIDIR"].Value)) * xIva) / 100;
                        xMarcados += (Convert.ToDouble(fila.Cells["CUE_VALOR_UNITARIO"].Value) * Convert.ToDouble(fila.Cells["CANT. DIVIDIR"].Value)) + iva ;
                    }
                   
                }
                else
                {
                    //xDesmarcados += Convert.ToDouble(fila.Cells["CUE_VALOR"].Value);
                }

            }
            this.txtTotal.Text = xTotal.ToString();
            this.txtTotalAud.Text = Math.Round(Convert.ToDecimal(xMarcados), 2).ToString();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            gridN.Refresh();

            ATENCIONES ultimaAtencion = new ATENCIONES();
            PACIENTES paciente = new PACIENTES();
            if(txt_Atencion.Text=="")
            {
                MessageBox.Show("Debe escoger una cuenta para dividir", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("Usted Va Dividir Una Cuenta, \r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                NegCuentasPacientes.DivideFacturaNO(Convert.ToInt32(txt_Atencion.Text));
                for (int i = 0; i < gridN.Rows.Count; i++)  ///MARCAMOS CON S EN CAMPO dividecuenta, LOS PRODUCTOS QUE TIENEN CHECK 
                {
                    if ((bool)gridN.Rows[i].Cells["AUDITADA"].Value == true && Convert.ToDecimal(gridN.Rows[i].Cells["CUE_CANTIDAD"].Value.ToString()) == Convert.ToDecimal(gridN.Rows[i].Cells["CANT. DIVIDIR"].Value.ToString()))
                    {
                        NegCuentasPacientes.DivideFacturaProd1(Convert.ToInt32(txt_Atencion.Text), Convert.ToInt64(gridN.Rows[i].Cells["CUE_CODIGO"].Value.ToString()));
                    }
                }
                for (int i = 0; i < gridN.Rows.Count; i++)  /// 
                {
                    if ((bool)gridN.Rows[i].Cells["AUDITADA"].Value == true)
                    {
                        NegCuentasPacientes.DivideFactura4(Convert.ToInt32(txt_Atencion.Text)); //DIVIDE EN DOS ATENCIONE SEGUN CAMPO DIVIDE CTA'
                        i = gridN.Rows.Count + 1;
                        MessageBox.Show("Cuenta dividida correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                paciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(txt_Atencion.Text));
                ultimaAtencion = NegAtenciones.RecuperarUltimaAtencion(paciente.PAC_CODIGO);
                for (int i = 0; i < gridN.Rows.Count; i++)  /// 
                {
                    if ((bool)gridN.Rows[i].Cells["AUDITADA"].Value == true && Convert.ToDecimal(gridN.Rows[i].Cells["CUE_CANTIDAD"].Value.ToString()) != Convert.ToDecimal(gridN.Rows[i].Cells["CANT. DIVIDIR"].Value.ToString()))
                    {
                        NegCuentasPacientes.DivideFactura(ultimaAtencion.ATE_CODIGO, Convert.ToDecimal(gridN.Rows[i].Cells["CANT. DIVIDIR"].Value.ToString()), Convert.ToInt64(gridN.Rows[i].Cells["CUE_CODIGO"].Value.ToString()));
                        //NegCuentasPacientes.DivideFactura4(Convert.ToInt32(txt_Atencion.Text), Convert.ToDecimal(gridN.Rows[i].Cells["CANT. DIVIDIR"].Value.ToString()), Convert.ToInt64(gridN.Rows[i].Cells["CUE_CODIGO"].Value.ToString())); //DIVIDE EN DOS ATENCIONE SEGUN CAMPO DIVIDE CTA'
                        //i = gridN.Rows.Count + 1;
                        //MessageBox.Show("Se dividio la cuenta.");
                    }
                }
                NegFactura.ActualizaEstadoHabitacionAuditoria(Convert.ToInt64(txt_Atencion.Text));
                cargarobserva();
                cargartabla();
                totales();
            }
        }

        private void gridN_KeyPress(object sender, KeyPressEventArgs e)
        {
            //UltraGridRow aUGRow = this.gridN.GetRow(ChildRow.First);
            //this.gridN.ActiveRow = aUGRow;
            
            //if (this.gridN.ActiveCell == aUGRow.Cells["CANT. DIVIDIR"])
            //{
            //    NegUtilitarios.OnlyNumberDecimal(e, false);
            //}
        }

        private void gridN_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            //try
            //{
            //    e.Row.Cells["CANT. DIVIDIR"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Double;
            //    DefaultEditorOwnerSettings defaultEditorOwnerSettings = new DefaultEditorOwnerSettings();
            //    defaultEditorOwnerSettings.MaskInput = "0";
            //    DefaultEditorOwner defaultEditorOwner = new DefaultEditorOwner(defaultEditorOwnerSettings);
            //    EditorWithMask editorWithMask = new EditorWithMask(defaultEditorOwner);
            //    e.Row.Cells["CANT. DIVIDIR"].Editor = editorWithMask;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}  
        }
    }
}