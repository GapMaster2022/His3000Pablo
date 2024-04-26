using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;

namespace TarifariosUI
{
    public partial class frmPorcentajes : Form
    {
        public frmPorcentajes()
        {
            InitializeComponent();
            cargar();
        }
        public int codigo = 0;
        private void cargar()
        {
            HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
            var query = (from a in contexto.PORCENTAJES
                         select new { CODIGO = a.POR_CODIGO,
                                      MISMA_VIA1 = a.POR_PORC1,
                                      MISMA_VIA2 = a.POR_PORC2,
                                      DOBLE_VIA = a.POR_DOBLEVIA,
                                      NOMBRE = a.POR_ASEG}

               );

            
            dtgPorcentaje.DataSource = query;
            dtgPorcentaje.AllowUserToOrderColumns = true;
            dtgPorcentaje.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dtgPorcentaje.Columns["CODIGO"].Width = 60;
            dtgPorcentaje.Columns["MISMA_VIA1"].Width = 100;
            dtgPorcentaje.Columns["MISMA_VIA2"].Width = 100;
            dtgPorcentaje.Columns["DOBLE_VIA"].Width = 100;
            dtgPorcentaje.Columns["NOMBRE"].Width = 100;
            
        }

        private void dtgPorcentaje_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            codigo = Convert.ToInt32(dtgPorcentaje.CurrentRow.Cells["CODIGO"].Value.ToString());
            this.Close();
        }
    }
}
