using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using His.Negocio;
using System.Windows.Forms;
using His.Admision;
using His.Formulario;
using His.Entidades;
using System.IO;
using CuentaPaciente;
using Core.Datos;
using His.Parametros;
using Core.Utilitarios;
using Infragistics.Win.UltraWinGrid;

namespace His.Dietetica
{
    public partial class ImagenAgendamiento : Form
    {
        string usuario;
        string cod_usuario;
        internal static string ate_codigo; //recibira el codigo de atencion del paciente desde el formulario de ayuda 
        internal static string paciente; // recibira el nombre del paciente desde el frm de ayuda.
        public ImagenAgendamiento(string x, string y)
        {
            InitializeComponent();            
            //monthCalendar1.SelectionRange.Start = (DateTime.Now).AddDays(-30);
            //monthCalendar1.SelectionRange.Start = Convert.ToDateTime(String.Format("{0:g}", (DateTime.Now.Year).ToString() + "/" + DateTime.Now.Month + "/01"));
            monthCalendar1.SelectionRange.Start = DateTime.Now;
            //dtpDesde.Value = (DateTime.Now).AddDays(-30);
            //dtpDesde.Value = Convert.ToDateTime(String.Format("{0:g}", (DateTime.Now.Year).ToString() + "/" + DateTime.Now.Month + "/01"));
            dtpDesde.Value = DateTime.Now;
            monthCalendar1.SelectionRange.End = (DateTime.Now).AddDays(30);
            dtpHasta.Value = (DateTime.Now).AddDays(30);
            dtpDesde.Value = Convert.ToDateTime(dtpDesde.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'00"));
            dtpHasta.Value = Convert.ToDateTime(dtpHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"));
            actualizarGrid();
            usuario = x; cod_usuario = y;

            ///////////////////////////
            DataTable dtRad = NegDietetica.getDataTable("getTurnoRadiologo");
            DataTable dtTec = NegDietetica.getDataTable("getTurnoTecnologo");
            string codRadiologo = "0";
            string codTecnologo = "0";
            if (dtRad.Rows.Count == 1)
                codRadiologo = dtRad.Rows[0][0].ToString();
            if (dtTec.Rows.Count == 1)
                codTecnologo = dtTec.Rows[0][0].ToString();
            txtCODRadiologo.Text = codRadiologo;
            txtCODTecnologo.Text = codTecnologo;
            cargarMedico(Convert.ToInt32(codTecnologo), txtTecnologo, txtCODTecnologo);
            cargarMedico(Convert.ToInt32(codRadiologo), txtRadiologo, txtCODRadiologo);
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            actualizarGrid();
        }

        private void actualizarGrid()
        {
            DateTime fi = monthCalendar1.SelectionRange.Start;
            DateTime ff = monthCalendar1.SelectionRange.End;
            ff = ff.AddDays(1);
            string frango = "'" + fi.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "' and '" + ff.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "'";

            grid.DataSource = NegImagen.getAgendamientos(frango);
            grid.DisplayLayout.Bands[0].Columns["id"].Hidden = true;
        }

        private void grpDatos_Click(object sender, EventArgs e)
        {

        }



        private void limpiar()
        {
            gridEstudios.Rows.Clear();
            txtObservaciones.Clear();
            txtPaciente.Clear();
            txtRadiologo.Clear();
            txtTecnologo.Clear();
            txtCODRadiologo.Text = "0";
            txtCODTecnologo.Text = "0";
            txtHC.Text = "0";

        }

        private void modificar(int x = 0)
        {




        }

        private void button2_Click(object sender, EventArgs e)
        {
            His.Admision.frm_AyudaPacientes form = new His.Admision.frm_AyudaPacientes();
            form.campoPadre = txtHC;

            form.ShowDialog();
            form.Dispose();

            PACIENTES P = NegPacientes.RecuperarPacienteID((form.campoPadre.Text.Trim().ToString()));
            txtPaciente.Visible = true;
            txtPaciente.Text = P.PAC_APELLIDO_PATERNO + " " + P.PAC_APELLIDO_MATERNO + " " + P.PAC_NOMBRE1 + " " + P.PAC_NOMBRE2;
        }

        private void btnAgregaEstudio_Click(object sender, EventArgs e)
        {
            frm_ImagenAyuda ayuda = new frm_ImagenAyuda("PRODUCTOS");
            ayuda.ShowDialog();
            if (ayuda.codigo != string.Empty)
            {
                if (!BuscarItem(ayuda.codigo, gridEstudios))
                    this.gridEstudios.Rows.Add(ayuda.codigo, ayuda.producto, ayuda.adicional1);
                else
                    MessageBox.Show("El item ya fue ingresado.");
            }
        }

        private bool BuscarItem(string searchValue, DataGridView grid)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(searchValue))
                {
                    return true;
                }
            }
            return false;
        }

        private void btnTecnologo_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
                cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), txtTecnologo, txtCODTecnologo);
        }

        private void btnRadiologo_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
                cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), txtRadiologo, txtCODRadiologo);
        }

        private void cargarMedico(int codMedico, TextBox txtNombre, TextBox txtCOD)
        {

            MEDICOS medico = NegMedicos.MedicoID(codMedico);
            if (medico != null)
            {
                DataTable med = NegMedicos.MedicoIDValida(codMedico);
                if (med.Rows[0][0].ToString() == "7")
                {
                    MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                txtNombre.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                txtCOD.Text = Convert.ToString(medico.MED_CODIGO);
            }

        }

        private void txtObservaciones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (this.GetNextControl(ActiveControl, true) != null)
                {
                    e.Handled = true;
                    this.GetNextControl(ActiveControl, true).Focus();

                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            pnl1.Visible = true; pnl2.Visible = false;
            limpiar();
            actualizarGrid();
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            pnl1.Visible = false; pnl2.Visible = true;
            dtpFechaNota.Value = dtpDesde.Value;
            txtIdAgendamiento.Text = "0";
            DataTable dtRad = NegDietetica.getDataTable("getTurnoRadiologo");
            DataTable dtTec = NegDietetica.getDataTable("getTurnoTecnologo");
            string codRadiologo = "0";
            string codTecnologo = "0";
            if (dtRad.Rows.Count == 1)
                codRadiologo = dtRad.Rows[0][0].ToString();
            if (dtTec.Rows.Count == 1)
                codTecnologo = dtTec.Rows[0][0].ToString();
            txtCODRadiologo.Text = codRadiologo;
            txtCODTecnologo.Text = codTecnologo;
            cargarMedico(Convert.ToInt32(codTecnologo), txtTecnologo, txtCODTecnologo);
            cargarMedico(Convert.ToInt32(codRadiologo), txtRadiologo, txtCODRadiologo);

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if ((dtpFechaNota.Value) < DateTime.Now && txtIdAgendamiento.Text.Trim() == "0")
            {
                MessageBox.Show("No pueden generar agendamientos con fecha menor a la actual.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (validar())
            {
                pnl1.Visible = true; pnl2.Visible = false;

                USUARIOS u = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);

                string[] a = new string[] { DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), txtHC.Text.Trim(), dtpFechaNota.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), txtCODTecnologo.Text.Trim(), txtCODRadiologo.Text.Trim(), txtObservaciones.Text.Trim(), cod_usuario };
                List<PedidoImagen_estudios> ListEstudios = new List<PedidoImagen_estudios>();
                foreach (DataGridViewRow row in gridEstudios.Rows)
                {
                    PedidoImagen_estudios estudio = new PedidoImagen_estudios();
                    estudio.PRO_CODIGO = Convert.ToInt32(row.Cells["ID"].Value);
                    estudio.PRO_CODSUB = Convert.ToInt32(row.Cells["ID"].Value);
                    ListEstudios.Add(estudio);
                }
                DataTable xEstudios = null;
                if (Convert.ToInt32(txtIdAgendamiento.Text) != 0)
                {
                    xEstudios = NegDietetica.getDataTable("GetEstudiosAgendamientoImagen", (txtIdAgendamiento.Text));
                }

                NegImagen.saveAgendamiento(Convert.ToInt32(txtIdAgendamiento.Text), a, ListEstudios, xEstudios);


                ///Imprimir Ticket



                limpiar();
                actualizarGrid();
            }
            else
            {
                MessageBox.Show("Datos incompletos, Revise las alertas para poder continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private bool validar()
        {
            bool valido = true;
            if (txtCODRadiologo.Text.Trim() == "0")
            {
                errorProvider1.SetError(txtRadiologo, "Radiologo no puede ser el predeterminado por el sistema. Debe seleccionar uno diferente.");
                valido = false;
            }

            if (txtCODTecnologo.Text.Trim() == "0")
            {
                errorProvider1.SetError(txtTecnologo, "Tecnologo no puede ser el predeterminado por el sistema. Debe seleccinar uno diferente.");
                valido = false;
            }

            if (txtRadiologo.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(txtRadiologo, "Debe agregar el Radiologo");
                valido = false;
            }

            if (txtTecnologo.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(txtTecnologo, "Debe agregar el Tecnologo");
                valido = false;
            }
            if (txtHC.Text.Trim() == "0")
            {
                errorProvider1.SetError(txtHC, "Debe elegir el paciente.");
                valido = false;
            }

            if (gridEstudios.RowCount == 0)
            {
                errorProvider1.SetError(gridEstudios, "Debe agregar estudio(s).");
                valido = false;
            }
            //if ((dtpFechaNota.Value).AddHours(+1) < DateTime.Now)
            //   return false;
            return valido;
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {

            try
            {
                if (grid.Rows.Count > 0)
                {
                    pnl1.Visible = false; pnl2.Visible = true;
                    txtIdAgendamiento.Text = grid.ActiveRow.Cells["id"].Value.ToString();
                    DataTable ag = NegImagen.getAgendamiento(grid.ActiveRow.Cells["id"].Value.ToString());
                    txtHC.Text = ag.Rows[0]["ATE_CODIGO"].ToString();
                    PACIENTES P = NegPacientes.RecuperarPacienteID(grid.ActiveRow.Cells["HISTORIAS"].Value.ToString().Trim());
                    txtPaciente.Visible = true;
                    txtPaciente.Text = P.PAC_APELLIDO_PATERNO + " " + P.PAC_APELLIDO_MATERNO + " " + P.PAC_NOMBRE1 + " " + P.PAC_NOMBRE2;
                    txtCODRadiologo.Text = ag.Rows[0]["med_radiologo"].ToString();
                    cargarMedico(Convert.ToInt32(txtCODRadiologo.Text.ToString()), txtRadiologo, txtCODRadiologo);
                    txtCODTecnologo.Text = ag.Rows[0]["med_tecnologo"].ToString();
                    cargarMedico(Convert.ToInt32(txtCODTecnologo.Text.ToString()), txtTecnologo, txtCODTecnologo);
                    txtObservaciones.Text = ag.Rows[0]["observacion"].ToString();
                    dtpFechaNota.Text = (ag.Rows[0]["fecha_agendamiento"].ToString());

                    DataTable est = NegImagen.getAgendamientoEstudios(txtIdAgendamiento.Text);
                    foreach (DataRow row in est.Rows)
                    {
                        gridEstudios.Rows.Add(row["CUE_CODIGO"].ToString(), row["PRO_DESCRIPCION"].ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            //DateTime fi = monthCalendar1.SelectionRange.Start;
            //dtpDesde.Value = fi;
            //DateTime ff = monthCalendar1.SelectionRange.End;
            //dtpHasta.Value = ff;
            //ff = ff.AddDays(1);
            //string frango = "'" + fi.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "' and '" + ff.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "'";
            //MessageBox.Show("DateChanged  " + frango);
        }




        private void tabulador_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {

        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {

        }

        private String FindSavePath()
        {
            Stream myStream;
            string myFilepath = null;
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xls)|*.xls";
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

        private void toolStripButtonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (grid.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(grid, PathExcel);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
        }

        private void btnPaciente_Click(object sender, EventArgs e)
        {
            frm_AyudaImagenPaciente x = new frm_AyudaImagenPaciente();
            x.ShowDialog();
            txtPaciente.Text = paciente;
            txtHC.Text = ate_codigo;
        }

        private void pnl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            dtpDesde.Value = monthCalendar1.SelectionRange.Start;
            dtpHasta.Value = monthCalendar1.SelectionRange.End;
            actualizarGrid();
        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            monthCalendar1.SetSelectionRange(dtpDesde.Value, dtpHasta.Value);
            dtpHasta.MinDate = dtpDesde.Value;
            actualizarGrid();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            monthCalendar1.SetSelectionRange(dtpDesde.Value, dtpHasta.Value);
            dtpDesde.MaxDate = dtpHasta.Value;
            actualizarGrid();
        }

        private void dtpHasta_MouseLeave(object sender, EventArgs e)
        {
            // monthCalendar1.SetSelectionRange(dtpDesde.Value, dtpHasta.Value);
        }

        private void dtpHasta_Leave(object sender, EventArgs e)
        {
            // monthCalendar1.SetSelectionRange(dtpDesde.Value, dtpHasta.Value);
        }

        private void HProd()
        {


            DataTable ag = NegImagen.getAteImagenCount(Convert.ToInt32(txtHC.Text));

            if ((Convert.ToInt32(ag.Rows[0]["IItems"].ToString())) == 0)
                lblAlertaImagen.Visible = true;
            else
                lblAlertaImagen.Visible = false;

        }



        private void btnAgregaEstudio_Click_1(object sender, EventArgs e)
        {
            Ayuda ayuda = new Ayuda("ATE_IMAGEN", Convert.ToInt32(txtHC.Text));
            ayuda.ShowDialog();
            bool existe = false;
            if (ayuda.codigo != string.Empty)
            {
                if (gridEstudios.Rows.Count > 0)
                {
                    foreach (DataGridViewRow item in gridEstudios.Rows)
                    {
                        if (item.Cells[0].Value.ToString() == ayuda.codigo)
                        {
                            existe = true;
                            MessageBox.Show("Codigo de producto ya ha sido agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    }
                    if (!existe)
                        this.gridEstudios.Rows.Add(ayuda.codigo, ayuda.producto);
                }
                else
                {
                    this.gridEstudios.Rows.Add(ayuda.codigo, ayuda.producto);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (grid.Rows.Count > 0)
                {
                    lblDelete.Text = "Se cancelara el agendamiento del " + grid.ActiveRow.Cells["FECHA AGENDAMIENTO"].Value.ToString() + "de Pcte." + grid.ActiveRow.Cells["PACIENTE"].Value.ToString();
                    pnl1.Visible = false; pnl3.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            USUARIOS u = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);

            if (txtMotivoCancelacion.Text.Trim() != string.Empty)
            {
                NegImagen.deleteAgendamiento(Convert.ToInt32(grid.ActiveRow.Cells["id"].Value), DateTime.Now.ToString() + " por " + usuario + ", motivo: " + txtMotivoCancelacion.Text);
                pnl1.Visible = true;
                pnl3.Visible = false;
                txtMotivoCancelacion.Text = "";
                actualizarGrid();
            }
            else
                MessageBox.Show("Es necesario ingrese el motivo de la cancelacion.");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            pnl1.Visible = true; pnl3.Visible = false;
        }

        private void txtRadiologo_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpFechaNota_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txtPaciente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txtTecnologo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txtRadiologo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txtObservaciones_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = grid.DisplayLayout.Bands[0];
            grid.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            grid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            grid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            grid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;
            grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            grid.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            grid.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;
            //Caracteristicas de Filtro en la grilla
            grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            grid.DisplayLayout.UseFixedHeaders = true;
        }
    }
}
