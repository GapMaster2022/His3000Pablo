using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using His.Entidades;
using His.Negocio;

namespace His.Formulario
{
    public partial class frm_AyudaGeneral : Form
    {
        public string resultado = "";
        public string codigo = "";

        public bool tarifario = false;
        public bool quirofano = false;
        public bool quirofanoTarifario = false;
        public bool gastroTarifario = false;
        public int bodega = His.Entidades.Clases.Sesion.bodega; //por defecto 12 de quirofano
        public frm_AyudaGeneral(bool _quirofanoTarifario = false, bool _gastro = false)
        {
            InitializeComponent();

            UltraGridDatos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            UltraGridDatos.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            //Inicializo las opciones del timer por defecto
            timerBusqueda.Interval = 1500;
            quirofanoTarifario = _quirofanoTarifario;
            gastroTarifario = _gastro;
            txtBuscar.Focus();
            Tarifarios();
        }

        public frm_AyudaGeneral(int bodega)
        {
            InitializeComponent();
            UltraGridDatos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            UltraGridDatos.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            //Inicializo las opciones del timer por defecto
            timerBusqueda.Interval = 1500;

            txtBuscar.Focus();
            this.bodega = bodega;
        }

        private void UltraGridDatos_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                if (tarifario)
                {
                    UltraGridDatos.DisplayLayout.Bands[0].Columns["CODIGO"].Width = 80;
                    UltraGridDatos.DisplayLayout.Bands[0].Columns["DESCRIPCION"].Width = 450;
                    UltraGridDatos.DisplayLayout.Bands[0].Columns["UVR"].Width = 80;

                    UltraGridDatos.DisplayLayout.Bands[0].Columns["CODIGO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    UltraGridDatos.DisplayLayout.Bands[0].Columns["DESCRIPCION"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    UltraGridDatos.DisplayLayout.Bands[0].Columns["UVR"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                    UltraGridDatos.DisplayLayout.Bands[0].Override.CellAppearance.BackColor = Color.LightCyan;
                    UltraGridDatos.DisplayLayout.Bands[0].Override.CellAppearance.BackColor2 = Color.Azure;
                    UltraGridDatos.DisplayLayout.Bands[0].Override.CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;

                }
                else
                {
                    UltraGridDatos.DisplayLayout.Bands[0].Columns["CODIGO"].Width = 80;
                    UltraGridDatos.DisplayLayout.Bands[0].Columns["DESCRIPCION"].Width = 450;
                    UltraGridDatos.DisplayLayout.Bands[0].Columns["CODIGO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    UltraGridDatos.DisplayLayout.Bands[0].Columns["DESCRIPCION"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                    UltraGridDatos.DisplayLayout.Bands[0].Override.CellAppearance.BackColor = Color.LightCyan;
                    UltraGridDatos.DisplayLayout.Bands[0].Override.CellAppearance.BackColor2 = Color.Azure;
                    UltraGridDatos.DisplayLayout.Bands[0].Override.CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;

                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void UltraGridDatos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                resultado = UltraGridDatos.ActiveRow.Cells[1].Text;
                codigo = UltraGridDatos.ActiveRow.Cells[0].Text;
                this.Close();
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (chkAutoBusqueda.Checked == true)
            {
                if (!timerBusqueda.Enabled)
                {
                    timerBusqueda.Start();
                }
            }
        }

        private void chkAutoBusqueda_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoBusqueda.Checked)
            {
                timerBusqueda.Enabled = true;
            }
            else
            {
                timerBusqueda.Stop();
                timerBusqueda.Enabled = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (tarifario == true)
                Tarifarios();
            else if (quirofano == true)
                Quirofano();
            else if (gastroTarifario == true)
                Gastro();
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (timerBusqueda.Enabled)
            {
                timerBusqueda.Stop();
            }
        }

        private void UltraGridDatos_DoubleClickCell(object sender, Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs e)
        {
            if (UltraGridDatos.ActiveRow.Index > -1)
            {
                resultado = UltraGridDatos.ActiveRow.Cells[1].Value.ToString();
                codigo = UltraGridDatos.ActiveRow.Cells[0].Value.ToString();
            }
            if(codigo == "")
            {
                resultado = "";
                MessageBox.Show("El Procedimiento que quiere ocupar no tiene codigo, por lo que no se puede ocupar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.Close();
        }

        private void frm_AyudaGeneral_Load(object sender, EventArgs e)
        {
            if (tarifario == true)
                Tarifarios();
            else if (quirofano == true)
                Quirofano();
            else if (gastroTarifario == true)
                Gastro();
        }

        public void Quirofano()
        {
            try
            {
                UltraGridDatos.DataSource = NegTarifario.recuperar_Tarifarios_Cirugia();
                //DataTable Quirofano = NegQuirofano.ProcedimientosCirugia(txtBuscar.Text, rdbPorCodigo.Checked, rdbPorDescripcion.Checked, bodega);
                //UltraGridDatos.DataSource = Quirofano;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Gastro()
        {
            try
            {
                DataTable Quirofano = NegQuirofano.ProcedimientosCirugia(txtBuscar.Text, rdbPorCodigo.Checked, rdbPorDescripcion.Checked, bodega);
                UltraGridDatos.DataSource = Quirofano;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Tarifarios()
        {
            try
            {
                //if (quirofanoTarifario)
                //{
                if (txtBuscar.Text == "")
                {
                    if (gastroTarifario)
                    {
                        Gastro();
                    }
                    else
                    {
                        Quirofano();
                    }
                }
                else
                {
                    HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                    if (rdbPorCodigo.Checked)
                    {
                        var tarifarioDetalleQuery = (from t in contexto.TARIFARIOS_DETALLE
                                                     where t.ESPECIALIDADES_TARIFARIOS.TARIFARIOS.TAR_CODIGO == 1
                                                     && t.TAD_REFERENCIA.Contains(txtBuscar.Text)
                                                     orderby t.TAD_CODIGO
                                                     select new { CODIGO = t.TAD_REFERENCIA, DESCRIPCION = t.TAD_NOMBRE.ToUpper().Trim(), UVR = t.TAD_UVR, t.TAD_ANESTESIA }).Take(100).ToList();
                        UltraGridDatos.DataSource = tarifarioDetalleQuery;
                    }
                    else
                    {
                        var tarifarioDetalleQuery = (from t in contexto.TARIFARIOS_DETALLE
                                                     where t.ESPECIALIDADES_TARIFARIOS.TARIFARIOS.TAR_CODIGO == 1
                                                     && t.TAD_DESCRIPCION.Contains(txtBuscar.Text) && t.TAD_REFERENCIA != ""
                                                     orderby t.TAD_CODIGO
                                                     select new { CODIGO = t.TAD_REFERENCIA, DESCRIPCION = t.TAD_NOMBRE.ToUpper().Trim(), UVR = t.TAD_UVR, t.TAD_ANESTESIA }).Take(100).ToList();
                        UltraGridDatos.DataSource = tarifarioDetalleQuery;
                    }
                }
                //}
                //else
                //{
                //    DataTable Tarifarios = new DataTable();
                //    Tarifarios = NegTarifario.ListaTarifario(txtBuscar.Text, rdbPorCodigo.Checked, rdbPorDescripcion.Checked);
                //    UltraGridDatos.DataSource = Tarifarios;
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timerBusqueda_Tick(object sender, EventArgs e)
        {
            try
            {
                btnBuscar_Click(null, null);
                timerBusqueda.Stop();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
