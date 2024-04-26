using His.Entidades;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Honorarios
{
    public partial class frmExploradorLiquidacion : Form
    {
        public frmExploradorLiquidacion()
        {
            InitializeComponent();
        }

        private void txtLiquidacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txtdocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void chknumero_CheckedChanged(object sender, EventArgs e)
        {
            if (chknumero.Checked)
                txtLiquidacion.Enabled = true;
            else
            {
                txtLiquidacion.Enabled = false;
                txtLiquidacion.Text = "0";
            }
        }

        private void chkLiquidados_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLiquidados.Checked)
            {
                txtdocumento.Text = "";
                txtdocumento.Enabled = true;
            }
            else
            {
                txtdocumento.Text = "";
                txtdocumento.Enabled = false;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CargarLiquidaciones()
        {
            ultraGridLiquidados.DataSource = NegLiquidacion.listarLiquidaciones(dtpDesde.Value, dtpHasta.Value, Convert.ToInt32(txtLiquidacion.Text), txtdocumento.Text, Convert.ToInt32(txtMedico.Text), chkLiquidados.Checked);
        }
        private void frmExploradorLiquidacion_Load(object sender, EventArgs e)
        {
            #region ASIGNACION DE FECHA
            //Primero obtenemos el día actual
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);

            dtpDesde.Value = oPrimerDiaDelMes;
            dtpHasta.Value = oUltimoDiaDelMes;
            #endregion
            CargarLiquidaciones();
        }

        public void Medico()
        {
            List<MEDICOS> medicos = NegMedicos.listaMedicos();
            medicos = NegMedicos.listaMedicosIncTipoMedico();
            MEDICOS medico = new MEDICOS();
            Formulario.frm_AyudaMedicos ayuda = new Formulario.frm_AyudaMedicos(medicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();

            if (ayuda.campoPadre.Text != string.Empty)
            {
                medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                agregarMedico(medico);
            }
        }
        private void agregarMedico(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                lblMedico.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                txtMedico.Text = medicoTratante.MED_CODIGO.ToString();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtMedico.Enabled = true;
                btnF1.Enabled = true;
            }
            else
            {
                txtMedico.Enabled = false;
                btnF1.Enabled = false;
                lblMedico.Text = "";
                txtMedico.Text = "0";
            }
        }

        private void txtMedico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                Medico();
        }

        private void btnF1_Click(object sender, EventArgs e)
        {
            Medico();
        }

        private void txtLiquidacion_Enter(object sender, EventArgs e)
        {
            if (txtLiquidacion.Text == "0")
                txtLiquidacion.Text = "";
        }

        private void txtLiquidacion_Leave(object sender, EventArgs e)
        {
            if (txtLiquidacion.Text == "")
                txtLiquidacion.Text = "0";
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarLiquidaciones();
        }

        private void ultraGridLiquidados_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ultraGridLiquidados.DisplayLayout.Bands[0];


            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            ultraGridLiquidados.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
            ultraGridLiquidados.DisplayLayout.Override.FilterEvaluationTrigger = FilterEvaluationTrigger.OnCellValueChange;
            ultraGridLiquidados.DisplayLayout.Override.FilterOperatorLocation = FilterOperatorLocation.WithOperand;
            ultraGridLiquidados.DisplayLayout.Override.FilterClearButtonLocation = FilterClearButtonLocation.RowAndCell;
            //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro";  
            ultraGridLiquidados.DisplayLayout.Override.SpecialRowSeparator = SpecialRowSeparator.FilterRow;



            //Cambio la apariencia de las sumas
            bandUno.Summaries.Clear();
            bandUno.SummaryFooterCaption = "Totales: ";
            bandUno.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.SummaryFooterCaptionAppearance.BackColor = Color.FromArgb(189, 191, 191);
            bandUno.Override.SummaryFooterCaptionAppearance.ForeColor = Color.Black;
            //totalizo las columnas
            SummarySettings sumHonorarios = bandUno.Summaries.Add("Valor", SummaryType.Sum, bandUno.Columns["Valor"]);
            //sumHonorarios.DisplayFormat = "Tot = {0:#####.00}";
            sumHonorarios.DisplayFormat = "{0:#####.00}";
            sumHonorarios.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
            SummarySettings sumComision = bandUno.Summaries.Add("Aporte", SummaryType.Sum, bandUno.Columns["Aporte"]);
            //sumComision.DisplayFormat = "Tot = {0:#####.00}";
            sumComision.DisplayFormat = "{0:#####.00}";
            sumComision.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
            SummarySettings sumReferido = bandUno.Summaries.Add("Comision", SummaryType.Sum, bandUno.Columns["Comision"]);
            //sumReferido.DisplayFormat = "Tot = {0:#####.00}";
            sumReferido.DisplayFormat = "{0:#####.00}";
            sumReferido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            SummarySettings sumRetenido = bandUno.Summaries.Add("Retencion", SummaryType.Sum, bandUno.Columns["Retencion"]);
            sumRetenido.DisplayFormat = "{0:#####.00}";
            sumRetenido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            SummarySettings sumValorPagar = bandUno.Summaries.Add("Total", SummaryType.Sum, bandUno.Columns["Total"]);
            sumValorPagar.DisplayFormat = "{0:#####.00}";
            sumValorPagar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            ultraGridLiquidados.DisplayLayout.Bands[0].Columns["MED_CODIGO"].Hidden = true;
            ultraGridLiquidados.DisplayLayout.Bands[0].Columns["Fecha"].Width = 100;
            ultraGridLiquidados.DisplayLayout.Bands[0].Columns["Medico"].Width = 300;
            ultraGridLiquidados.DisplayLayout.Bands[0].Columns["Paciente"].Width = 300;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CargarLiquidaciones();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (ultraGridLiquidados.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(ultraGridLiquidados, PathExcel);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
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
        public void Imprimir(Int64 numdoc)
        {
            List<LIQUIDACION> x = new List<LIQUIDACION>(); //cargo  la liquidacion realizada consultando en la base
            x = NegLiquidacion.recuperarLiquidacion(numdoc);
            EMPRESA e = NegEmpresa.RecuperaEmpresa();
            if (x.Count > 0)
            {
                DsLiquidacion liquidacion = new DsLiquidacion();
                DataRow dr;

                Int64 numero = 0;
                foreach (var item in x)
                {
                    MEDICOS m = NegMedicos.recuperarMedico((int)item.MED_CODIGO);
                    HONORARIOS_MEDICOS h = NegLiquidacion.recuperarHonorario((Int64)item.HOM_CODIGO);
                    ATENCIONES a = NegAtenciones.RecuperarAtencionID((Int64)h.ATE_CODIGO);
                    PACIENTES p = NegPacientes.recuperarPacientePorAtencion((int)h.ATE_CODIGO);

                    dr = liquidacion.Tables["Liquidacion"].NewRow();
                    dr["Medico"] = m.MED_APELLIDO_PATERNO + " " + m.MED_APELLIDO_MATERNO + " " + m.MED_NOMBRE1 + " " + m.MED_NOMBRE2;
                    dr["Paciente"] = p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2;
                    dr["Factura"] = a.ATE_FACTURA_PACIENTE;
                    dr["Atencion"] = a.ATE_FECHA_INGRESO;
                    dr["Hc"] = p.PAC_HISTORIA_CLINICA;
                    dr["Fecha"] = item.LIQ_FECHA;
                    dr["Liquidacion"] = item.LIQ_NUMDOC;
                    numero = (Int64)item.LIQ_NUMDOC;
                    dr["ValorNeto"] = h.HOM_VALOR_NETO;
                    liquidacion.Tables["Liquidacion"].Rows.Add(dr);
                }

                dr = liquidacion.Tables["Principal"].NewRow();
                dr["Logo"] = NegUtilitarios.RutaLogo("General");
                dr["Empresa"] = e.EMP_NOMBRE;
                dr["Telefono"] = e.EMP_TELEFONO;
                dr["Direccion"] = e.EMP_DIRECCION;
                dr["Liquidacion"] = numero;
                liquidacion.Tables["Principal"].Rows.Add(dr);

                frmReportes view = new frmReportes("Liquidacion", liquidacion);
                view.ShowDialog();
            }
            else
                MessageBox.Show("No tiene liquidacion realizadas.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void liquidaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ultraGridLiquidados.Selected.Rows.Count == 1)
            {
                UltraGridRow fila = ultraGridLiquidados.ActiveRow;
                Imprimir(Convert.ToInt64(fila.Cells[0].Value.ToString()));
            }
        }
        public void ImprimirAsiento()
        {
            if(ultraGridLiquidados.Selected.Rows.Count == 1)
            {
                Formulario.DSAsiento asiento = new Formulario.DSAsiento();
                DataRow cabecera;
                bool asientos = false;

                UltraGridRow fila = ultraGridLiquidados.ActiveRow;
                USUARIOS users = new USUARIOS();
                try
                {
                    DataTable TCabecera = NegLiquidacion.reporteAsientoLiquidacion(Convert.ToDouble(fila.Cells["Asiento"].Value.ToString()), "AD", 0);

                    //DataTable TDetalle = NegHonorariosMedicos.ImpresionAsientos(Convert.ToInt64(item.Cells["COD"].Value.ToString()), 1);
                    if (TCabecera.Rows.Count > 0)
                    {
                        asientos = true;

                        if (TCabecera.Rows.Count > 0)
                        {
                            for (int i = 0; i < TCabecera.Rows.Count; i++)
                            {
                                cabecera = asiento.Tables["Cabecera"].NewRow();
                                cabecera["numdoc"] = TCabecera.Rows[i]["numdoc"].ToString();
                                users = NegUsuarios.RecuperaUsuario(Convert.ToInt16(TCabecera.Rows[i]["codrespon"].ToString()));
                                cabecera["usuario"] = users.USR;
                                cabecera["logo"] = NegUtilitarios.RutaLogo("General");
                                cabecera["fecha"] = TCabecera.Rows[i]["fechatran"].ToString();
                                cabecera["observacion"] = TCabecera.Rows[i]["observacion"].ToString();
                                cabecera["beneficiario"] = TCabecera.Rows[i]["beneficiario"].ToString();
                                cabecera["numCuenta"] = TCabecera.Rows[i]["CODIGO"].ToString();
                                cabecera["Cuenta"] = TCabecera.Rows[i]["nomcue_pc"].ToString();
                                cabecera["Auxiliar"] = TCabecera.Rows[i]["codigo_c"].ToString();
                                cabecera["numFac"] = TCabecera.Rows[i]["nocomp"].ToString();
                                cabecera["Debe"] = TCabecera.Rows[i]["debe"].ToString();
                                cabecera["Haber"] = TCabecera.Rows[i]["haber"].ToString();

                                asiento.Tables["Cabecera"].Rows.Add(cabecera);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (asientos)
                {
                    Formulario.frmReportes x = new Formulario.frmReportes(1, "HonorariosAsiento", asiento);
                    x.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No tiene asiento generados.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void asientoContableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chkLiquidados.Checked)
            {
                ImprimirAsiento();
            }
            else
                MessageBox.Show("Debe activar el estado \"Liquidado\" y buscar una factura.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void reversarLiquidacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ultraGridLiquidados.Selected.Rows.Count == 1)
            {
                UltraGridRow fila = ultraGridLiquidados.ActiveRow;
                if(!NegLiquidacion.validaReversoLiquidacion(Convert.ToInt64(fila.Cells["Liquidacion"].Value.ToString())))
                {
                    MessageBox.Show("Esta liquidacion tiene algun honorario con un asiento contable.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }    
                if(MessageBox.Show("Esta seguro de Reversar la liquidacion: " + fila.Cells["Liquidacion"].Value.ToString() + "?",
                    "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (NegLiquidacion.reversarLiquidacion(Convert.ToInt64(fila.Cells["Liquidacion"].Value.ToString())))
                        MessageBox.Show("Reversion exitosa.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Algo ocurrio al reversar liquidacion.\r\nRevise que no tenga asientos generados.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            CargarLiquidaciones();
        }

        private void anularAsientoContableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ultraGridLiquidados.Selected.Rows.Count == 1)
            {
                if (chkLiquidados.Checked)
                {
                    UltraGridRow Fila = ultraGridLiquidados.ActiveRow;
                    if (MessageBox.Show("Esta seguro de anular asiento: " + Fila.Cells["Asiento"].Value.ToString() + "?",
                        "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        MEDICOS med = NegMedicos.recuperarMedico(Convert.ToInt32(Fila.Cells["MED_CODIGO"].Value.ToString()));
                        if (NegLiquidacion.ValidaLiquidacionUsada(Convert.ToDouble(Fila.Cells["Asiento"].Value.ToString()), "AD"))
                        {
                            if (NegLiquidacion.anulacionLiquidacion(Convert.ToDouble(Fila.Cells["Asiento"].Value.ToString())))
                            {
                                //proceso de anulacion al eliminar de tabla HONORARIOS_MEDICOS el HOM_FACTURA_MEDICOS
                                if (NegLiquidacion.anularHonoratio(Fila.Cells["Honorario"].Value.ToString()))
                                {
                                    //proceso de anulacion CG
                                    if (NegLiquidacion.anulacionCG(Convert.ToDouble(Fila.Cells["Asiento"].Value.ToString()), "AD", Convert.ToDouble(med.MED_CODIGO_MEDICO.Trim())))
                                        MessageBox.Show("Asiento anulado con exito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else
                                        MessageBox.Show("Algo ocurrio al anular asiento.\r\nIntente mas tarde.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    chkLiquidados.Checked = false;
                                }
                                else
                                    MessageBox.Show("Algo ocurrio al anular asiento His.\r\nIntente mas tarde.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                MessageBox.Show("Algo ocurrio al restaurar liquidaciones.\r\nIntente mas tarde nuevamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            MessageBox.Show("Retencion activa o cxp ya ha sido cancelado.\r\nPor favor verifique e intente nuevamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Debe activar el estado \"Liquidado\" y buscar una factura.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarLiquidaciones();
            }
        }
    }
}
