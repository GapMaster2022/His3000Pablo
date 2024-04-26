using DevExpress.XtraBars;
using DevExpress.XtraSplashScreen;
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
    public partial class MDI_Pedido : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MDI_Pedido()
        {
            InitializeComponent();
        }

        private void btnDespacho_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Open Wait Form
            SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
            //proceso
            frmDespacho x = new frmDespacho();
            x.MdiParent = this;
            x.Show();
            //Close Wait Form
            SplashScreenManager.CloseForm(false);
        }
    }
}