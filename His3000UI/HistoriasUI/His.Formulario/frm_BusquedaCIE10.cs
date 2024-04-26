using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using His.Entidades;
using His.Negocio;
using Recursos;

namespace His.Formulario
{
    public partial class frm_BusquedaCIE10 : Form
    {
        //variables 
        //lista de CIE10
        List<CIE10> listaCIE;
        //atributo que guarda el resultado seleccionado
        public string resultado;
        public string codigo;
        public frm_BusquedaCIE10()
        {
            InitializeComponent();
            listaCIE = new List<CIE10>();
            //inicializo opciones pro defecto del ultragrid
            ulgdbListadoCIE.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            ulgdbListadoCIE.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            //Inicializo las opciones del timer por defecto
            timerBusqueda.Interval = 1500;

            txtBuscar.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                
                
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    
                    var listaCabecera = (from c in contexto.CIE10
                                         where c.CIE_IDPADRE.Equals("")
                                         select new { c.CIE_CODIGO, c.CIE_DESCRIPCION }).ToList();
                    
                    string buscar = txtBuscar.Text.ToString();

                    if (rdbPorDescripcion.Checked)
                    {
                        var lista = (from c in contexto.CIE10
                                     where c.CIE_DESCRIPCION.Contains(buscar)
                                     select c).ToList();

                        var listaFinal = (from lc in listaCabecera
                                          join ld in lista on lc.CIE_CODIGO equals ld.CIE_IDPADRE
                                          group ld by new { lc.CIE_DESCRIPCION, lc.CIE_CODIGO } into grupo
                                          select new
                                          {
                                              CODIGO = grupo.Key.CIE_CODIGO,
                                              DESCRIPCION = grupo.Key.CIE_DESCRIPCION
                                              ,
                                              DETALLE = grupo
                                          }).ToList();
                        ulgdbListadoCIE.DataSource = listaFinal;
                    }
                    else
                    {
                        var lista = (from c in contexto.CIE10
                                     where c.CIE_CODIGO.Contains(buscar)
                                     select c).ToList();

                        var listaFinal = (from lc in listaCabecera
                                          join ld in lista on lc.CIE_CODIGO equals ld.CIE_IDPADRE
                                          group ld by new { lc.CIE_DESCRIPCION, lc.CIE_CODIGO } into grupo
                                          select new
                                          {
                                              CODIGO = grupo.Key.CIE_CODIGO,
                                              DESCRIPCION = grupo.Key.CIE_DESCRIPCION,
                                              DETALLE = grupo
                                          }).ToList();
                        ulgdbListadoCIE.DataSource = listaFinal;
                    }

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ulgdbListadoCIE_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {

                ulgdbListadoCIE.DisplayLayout.Bands[1].ColHeadersVisible = false;

                ulgdbListadoCIE.DisplayLayout.Bands[0].Columns["CODIGO"].Width = 40;
                ulgdbListadoCIE.DisplayLayout.Bands[0].Columns["DESCRIPCION"].Width = 1200;
                ulgdbListadoCIE.DisplayLayout.Bands[1].Columns["CIE_IDPADRE"].Hidden = true;


                ulgdbListadoCIE.DisplayLayout.Bands[0].Columns["CODIGO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                ulgdbListadoCIE.DisplayLayout.Bands[0].Columns["DESCRIPCION"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                ulgdbListadoCIE.DisplayLayout.Bands[1].Columns["CIE_CODIGO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                ulgdbListadoCIE.DisplayLayout.Bands[1].Columns["CIE_DESCRIPCION"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                ulgdbListadoCIE.DisplayLayout.Bands[1].Columns["CIE_DESCRIPCION"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;

                ulgdbListadoCIE.DisplayLayout.Bands[0].Override.CellAppearance.BackColor = Color.LightCyan;
                ulgdbListadoCIE.DisplayLayout.Bands[0].Override.CellAppearance.BackColor2 = Color.Azure;
                ulgdbListadoCIE.DisplayLayout.Bands[0].Override.CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;

                //e.Layout.Rows.ExpandAllCards();
                //e.Row.ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
                //e.Row.Expanded = true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void ulgdbListadoCIE_DoubleClick(object sender, EventArgs e)
        {
            //resultado = ulgdbListadoCIE.ActiveRow.Cells[1].Text;
            //codigo = ulgdbListadoCIE.ActiveRow.Cells[0].Text;


            //this.Close();


            
        }

        private void ulgdbListadoCIE_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    resultado = ulgdbListadoCIE.ActiveRow.Cells[1].Text;
            //    codigo = ulgdbListadoCIE.ActiveRow.Cells[0].Text;
            //    this.Close();
            //}
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

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (timerBusqueda.Enabled)
            {
                timerBusqueda.Stop();
            }
        }

        private void ulgdbListadoCIE_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void ulgdbListadoCIE_DoubleClickCell(object sender, Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs e)
        {
            if (ulgdbListadoCIE.ActiveRow.Index > -1)
            {

                resultado = ulgdbListadoCIE.ActiveRow.Cells[1].Value.ToString();
                codigo = ulgdbListadoCIE.ActiveRow.Cells[0].Value.ToString();

            }
            this.Close();
        }
    }
}
