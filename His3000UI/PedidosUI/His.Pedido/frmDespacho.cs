using DevExpress.XtraEditors;
using His.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace His.Pedido
{
    public partial class frmDespacho : Form
    {
        public frmDespacho()
        {
            InitializeComponent();
            dtpDesde.EditValue = DateTime.Now.Date;
            dtphasta.EditValue = DateTime.Now;
        }

        private void navSalir_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            if((DateTime)dtpDesde.EditValue > (DateTime)dtphasta.EditValue)
            {
                XtraMessageBox.Show("Fecha \"Desde\" no puede ser mayor a fecha \"Hasta\"", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                VerDespechos();
            }
        }
        public void VerDespechos()
        {
            try
            {
                ultraGridDespachos.DataSource = NegPedidos.VerDespacho(Convert.ToDateTime(dtpDesde.EditValue).Date, Convert.ToDateTime(dtphasta.EditValue).AddHours(23).AddMinutes(59).AddSeconds(59));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
    }
}
