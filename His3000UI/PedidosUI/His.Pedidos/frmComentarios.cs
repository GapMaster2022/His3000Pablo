using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Pedidos
{
    public partial class frmComentarios : Form
    {
        public frmComentarios()
        {
            InitializeComponent();
        }

        private void frmComentarios_Load(object sender, EventArgs e)
        {
            GeneralApp.ControlesWinForms.Editor editor = new GeneralApp.ControlesWinForms.Editor();
            editor.Parent = this;
            editor.Width = this.Width - 10;
            editor.Height = this.Height - 10;
        }
    }
}
