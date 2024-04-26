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

namespace His.Maintenance
{
    public partial class Frm_ProductosSub : Form
    {
        public Int64 codsub = 0;
        public Frm_ProductosSub()
        {
            InitializeComponent();
            carcarCombo();
            HabilitarBotones(true, false, false, false);
            habilitarCampos(false);
            cargarGrid();
        }
        public void carcarCombo()
        {
            cmbDivision.DataSource = NegMaintenance.cmdDivision();
            cmbDivision.ValueMember = "codsub";
            cmbDivision.DisplayMember = "dessub";
            cmbDivision.SelectedIndex = -1;
        }
        public void limpiar()
        {
            cmbDivision.SelectedIndex = -1;
        }
        public void cargarGrid()
        {
            UltraGridNacionalidad.DataSource = NegMaintenance.productosSubdivision();
        }
        public void HabilitarBotones(bool nuevo, bool guardar, bool eliminar, bool cancelar)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnBorrar.Enabled = eliminar;
            btnCancelar.Enabled = cancelar;
        }
        public void habilitarCampos(bool combo)
        {
            cmbDivision.Enabled = combo;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            HabilitarBotones(false, true, false, true);
            habilitarCampos(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de agregar esta division", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {                
                if ((NegMaintenance.productosSubdivisionExiste(Convert.ToInt32(cmbDivision.SelectedValue)).Count())==0)
                {
                    if (NegMaintenance.InsertaProductosSubdivision(Convert.ToInt64(cmbDivision.SelectedValue)))
                    {
                        MessageBox.Show("Grupo generado correctamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        HabilitarBotones(true, false, false, false);
                        habilitarCampos(false);
                        cargarGrid();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar el grupo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                    MessageBox.Show("Ya tiege agregado el grupo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            habilitarCampos(false);
            HabilitarBotones(true, false, false, false);
        }

        private void UltraGridNacionalidad_DoubleClick(object sender, EventArgs e)
        {
            UltraGridRow fila = UltraGridNacionalidad.ActiveRow;
            if (UltraGridNacionalidad.Selected.Rows.Count > 0)
            {
                //Binding DEP_CODIGO = new Binding("SelectedValue", usuarioModificada, "DEP_CODIGO", true);
                cmbDivision.DataBindings.Clear();
                cmbDivision.SelectedItem = fila.Cells[0].Value.ToString();
                cmbDivision.SelectedValue = fila.Cells[0].Value.ToString();
                codsub = Convert.ToInt64(fila.Cells[0].Value.ToString());
                HabilitarBotones(false, false, true, true);
                cmbDivision.Focus();
            }
        }

        private void UltraGridNacionalidad_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = UltraGridNacionalidad.DisplayLayout.Bands[0];

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;

            UltraGridNacionalidad.DisplayLayout.Bands[0].Columns[0].Width = 100;
            UltraGridNacionalidad.DisplayLayout.Bands[0].Columns[1].Width = 200;
            UltraGridNacionalidad.DisplayLayout.Bands[0].Columns[2].Width = 200;
            UltraGridNacionalidad.DisplayLayout.Bands[0].Columns[3].Width = 100;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            UltraGridNacionalidad.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
            UltraGridNacionalidad.DisplayLayout.Override.FilterEvaluationTrigger = FilterEvaluationTrigger.OnCellValueChange;
            UltraGridNacionalidad.DisplayLayout.Override.FilterOperatorLocation = FilterOperatorLocation.WithOperand;
            UltraGridNacionalidad.DisplayLayout.Override.FilterClearButtonLocation = FilterClearButtonLocation.RowAndCell;
            //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro";  
            UltraGridNacionalidad.DisplayLayout.Override.SpecialRowSeparator = SpecialRowSeparator.FilterRow;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de agregar esta division", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (NegMaintenance.EliminarSubdivision(Convert.ToInt64(codsub)))
                {
                    MessageBox.Show("Grupo eliminado correctamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HabilitarBotones(true, false, false, false);
                    habilitarCampos(false);
                    limpiar();
                    cargarGrid();
                }
                else
                    MessageBox.Show("Algo ocurrio al eliminar la grupo, revise si este procedimiento no ha sido utilizado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}