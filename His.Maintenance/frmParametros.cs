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

namespace His.Maintenance
{
    public partial class frmParametros : Form
    {
        string xAux;


        public frmParametros()
        {
            InitializeComponent();
            refreshObject();
        }


        private void refreshObject() //actualizar grid, combos, etc 
        {
            cmbTipo.DataSource = NegMaintenance.getQuery("Parametros");
            cmbTipo.ValueMember = "PAR_CODIGO";
            cmbTipo.DisplayMember = "PAR_NOMBRE";
            cmbTipo.SelectedIndex = 0;
            grid.DataSource = NegMaintenance.getQuery("ParametrosArchivos", Convert.ToInt32(cmbTipo.SelectedValue));
            var gridBand = grid.DisplayLayout.Bands[0];
            for (int i = 0; i < gridBand.Columns.Count; i++)
            {
                gridBand.Columns[i].CellActivation = Activation.NoEdit;
            }
            gridBand.Columns["PAD_VALOR"].CellActivation = Activation.AllowEdit;
            gridBand.Columns["PAD_VALOR"].Width = 350;
        }



        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    this.ultraGridExcelExporter1.Export(grid, PathExcel);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipo.SelectedIndex>0)
            {
                grid.DataSource = NegMaintenance.getQuery("ParametrosArchivos", Convert.ToInt32(cmbTipo.SelectedValue));
            }
            
        }

        private void grid_TextChanged(object sender, EventArgs e)
        {
         //   MessageBox.Show("");
        }

        private void grpDatos_Click(object sender, EventArgs e)
        {

        }

        private void grid_AfterCellUpdate(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
          //  MessageBox.Show(grid.Rows[grid.ActiveRow.Index].Cells["OBSERVACION"].Value.ToString());
        }

        private void grid_BeforeExitEditMode(object sender, BeforeExitEditModeEventArgs e)
        {
            if (e.CancellingEditOperation)
                return;

            if (this.grid.ActiveCell.Column.Key == "PAD_VALOR")
            {

                if (xAux != this.grid.ActiveCell.Text)
                {
                    DialogResult result = MessageBox.Show("Confirma que desea cambiar el valor del parametro?", "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (this.grid.ActiveCell.Text == "")
                            this.grid.ActiveCell.Value = "";
                        setDetalleParametros();    
                        return;
                    }
                    else
                    {
                            this.grid.ActiveCell.CancelUpdate();
                            return;   
                    }
                }
            }
        }


        private void setDetalleParametros()
        {
            PARAMETROS_DETALLE pad = new PARAMETROS_DETALLE();
            pad.PAD_VALOR = grid.Rows[grid.ActiveRow.Index].Cells["PAD_VALOR"].Text.ToString();
            //pad.PAD_VALOR = valor;
            pad.PAD_CODIGO = Convert.ToInt16(grid.Rows[grid.ActiveRow.Index].Cells["PAD_CODIGO"].Value);
            pad.PAD_ACTIVO = Convert.ToBoolean(grid.Rows[grid.ActiveRow.Index].Cells["PAD_ACTIVO"].Value);
            pad.PAD_NOMBRE = Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["PAD_NOMBRE"].Value);
            pad.PAD_TIPO = Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["PAD_TIPO"].Value);
            pad.PARAMETROSReference.EntityKey = ((PARAMETROS)cmbTipo.SelectedItem).EntityKey;
            

            NegMaintenance.setQuery("DetalleParametros", pad);
        }

        private void grid_BeforeEnterEditMode(object sender, CancelEventArgs e)
        {
            xAux = this.grid.ActiveCell.Text;
        }

        private void grid_AfterExitEditMode(object sender, EventArgs e)
        {
           
        }
    }
}
