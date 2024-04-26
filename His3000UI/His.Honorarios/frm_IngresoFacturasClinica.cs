using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Recursos;
using His.Entidades;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using His.Parametros;

namespace His.Honorarios
{
    public partial class frm_IngresoFacturasClinica : Form
    {

        private bool inicio = true;
        private ATENCIONES atencion = null;
        public frm_IngresoFacturasClinica()
        {
            InitializeComponent();
            limpiarCampos();
            cargarRecursos();
        }
        public void cargarRecursos()
        {
            //this.tssMedicos.Image  = Recursos.Archivo.btnOrganigrama;  
            //imagenes del menu principal
            toolStripSplitButtonNuevo.Image = Archivo.imgBtnAdd2;
            toolStripButtonCancelar.Image = Archivo.imgBtnStop;
            toolStripButtonGuardar.Image = Archivo.imgBtnFloppy;
            toolStripButtonActualizar.Image = Archivo.imgBtnRestart;
            toolStripButtonExportar.Image = Archivo.imgOfficeExcel;
            toolStripButtonImprimir.Image = Archivo.imgBtnImprimir32;
            toolStripButtonSalir.Image = Archivo.imgBtnSalir32;
            //tssMedicos.Image = Archivo.imgSptOrganizar;
            //btnOcultarInfMedico.Appearance.Image = Archivo.imgBtnFlechaArriba16;

            DateTime max = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 23, 59, 59);
            //DateTime min = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 0, 0, 0);

            dateTimePickerFechaFactura.MaxDate = max;
            dateTimePickerFechaAlta.MaxDate = max;
            dateTimePickerFechaIngreso.MaxDate = max;
        }

        private void ultraGroupBox2_Click(object sender, EventArgs e)
        {

        }

        public void RecuperarAtenciones()
        {
            

            gridAtenciones.DataSource = NegAtenciones.atencionesPorFacturar().Select(a => new
            {
                CODIGO = a.ATE_CODIGO,
                NUM_ATENCION = a.ATE_NUMERO_ATENCION,
                HCL = a.PAC_HCL,
                PACIENTE = a.PAC_APELLIDO_PATERNO+' '+a.PAC_APELLIDO_MATERNO+' '+a.PAC_NOMBRE+' '+a.PAC_NOMBRE2,
                FECHA_INGRESO = a.ATE_FECHA_INGRESO,
                HABITACION = a.HAB_NUMERO,
                REFERIDO = a.ATE_REFERIDO
            }).ToList();
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            RecuperarAtenciones();
        }

        private void gridAtenciones_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            if (inicio == true)
            {
                UltraGridBand bandUno = gridAtenciones.DisplayLayout.Bands[0];

                gridAtenciones.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //gridAtenciones.DisplayLayout.Override.Allow

                gridAtenciones.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //gridAtenciones.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                gridAtenciones.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                gridAtenciones.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                gridAtenciones.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
                gridAtenciones.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
                gridAtenciones.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                gridAtenciones.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                gridAtenciones.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                gridAtenciones.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                gridAtenciones.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                gridAtenciones.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                gridAtenciones.DisplayLayout.UseFixedHeaders = true;

                bandUno.Columns["PACIENTE"].MinWidth = 250;
                bandUno.Columns["PACIENTE"].MaxWidth = 250;

                inicio = false;
            }
        }

        private void limpiarCampos()
        {
            txtCodigoAtencion.Text = string.Empty;
            txtPacienteHCL.Text = string.Empty;
            txtPacienteNombre.Text = string.Empty;
            txtHabitacion.Text = string.Empty;
            dateTimePickerFechaIngreso.Value = DateTime.Now;
            dateTimePickerFechaAlta.Value = DateTime.Now;
            dateTimePickerFechaFactura.Value = DateTime.Now;
            txtNumControl.Text = string.Empty;
            txtFactura.Text = string.Empty;
            checkBoxReferido.Checked = false;
        }

        private void gridAtenciones_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            try
            {
                if (gridAtenciones.ActiveRow.Index > -1)
                {
                    limpiarCampos();
                    int codAtencion = Convert.ToInt32(gridAtenciones.DisplayLayout.ActiveRow.Cells["CODIGO"].Value);
                    atencion = NegAtenciones.AtencionID(codAtencion);
                    txtCodigoAtencion.Text = codAtencion.ToString();
                    txtPacienteHCL.Text = gridAtenciones.DisplayLayout.ActiveRow.Cells["HCL"].Value.ToString();
                    txtPacienteNombre.Text = gridAtenciones.DisplayLayout.ActiveRow.Cells["PACIENTE"].Value.ToString();
                    txtHabitacion.Text = gridAtenciones.DisplayLayout.ActiveRow.Cells["HABITACION"].Value.ToString();
                    dateTimePickerFechaIngreso.Value = Convert.ToDateTime(gridAtenciones.DisplayLayout.ActiveRow.Cells["FECHA_INGRESO"].Value);
                    checkBoxReferido.Checked = Convert.ToBoolean(gridAtenciones.DisplayLayout.ActiveRow.Cells["REFERIDO"].Value);
                    //dateTimePickerFechaAlta.Value = DateTime.Now;
                    //dateTimePickerFechaFactura.Value = DateTime.Now;
                    //txtNumControl.Text = string.Empty;
                    //txtFactura.Text = string.Empty;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonCancelar_Click(object sender, EventArgs e)
        {
            atencion = null;
            limpiarCampos();
            error.Clear();
        }

        private void toolStripButtonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (atencion != null)
                {
                    if (ValidarFormulario() == true)
                    {
                        atencion.ATE_FACTURA_FECHA = dateTimePickerFechaFactura.Value;
                        atencion.ATE_FECHA_ALTA = dateTimePickerFechaAlta.Value;
                        atencion.ATE_FACTURA_PACIENTE = txtFactura.Text.Replace("-",string.Empty);
                        atencion.ATE_NUMERO_CONTROL = txtNumControl.Text;
                        NegAtenciones.ingresarDatosFactura(atencion);
                        limpiarCampos();
                        RecuperarAtenciones();
                        atencion = null;
                        MessageBox.Show("Información guardada exitosamente","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Información Incompleta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione una atencion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if(r.InnerException != null)
                    MessageBox.Show(r.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarFormulario()
        {
            error.Clear();

            bool valido = true;
            //if (txtCodigoPaciente.Text == string.Empty && paciente !=null)
            //{
            //    AgregarErrorAtenciones(txtCodigoPaciente);
            //    valido = false;
            //}
            
            if (txtNumControl.Text == string.Empty)
            {
                AgregarError(txtNumControl);
                valido = false;
            }
            if (txtFactura.Text == "   -   -")
            {
                AgregarError(txtFactura);
                valido = false;
            }

            return valido;
        }

        private void AgregarError(Control control)
        {
            error.SetError(control, "Campo Requerido");
        }

        private void txtFactura_Leave(object sender, EventArgs e)
        {
            if (txtFactura.Text != "   -   -")
            {
                string factura = txtFactura.Text.Trim().Replace("-", string.Empty);
                factura = factura.Replace(" ", "");
                if (factura.Length > 7)
                    factura = factura.Substring(6);

                if (txtFactura.Text.Replace("-", string.Empty).Length < 13)
                    txtFactura.Text = "001-001-" + string.Format("{0:0000000}", int.Parse(factura));

            }
        }

        private void txtNumControl_Leave(object sender, EventArgs e)
        {
            string ncontrol = txtNumControl.Text.ToString().Trim();

            if (ncontrol != string.Empty)
            {
                if (ncontrol.Length < 6)
                {

                    ncontrol = ncontrol.Replace(" ", string.Empty);
                    string cerosizq = "";
                    for (int i = 0; i < 6 - ncontrol.Length; i++)
                        cerosizq = cerosizq + "0";

                    ncontrol = cerosizq + ncontrol;
                    txtNumControl.Text = ncontrol;
                }
            }
        }

        private void txtCodigoAtencion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
