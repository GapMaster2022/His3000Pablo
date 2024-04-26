using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Negocio;

namespace His.Pedidos
{
    public partial class frmSeleccionArea : Form
    {
        #region variables
        PEDIDOS_AREAS _area;
        List<PEDIDOS_AREAS> _lstPedidosAreas;
        #endregion

        #region propiedades
        public List<PEDIDOS_AREAS> LstPedidosAreas
        {
            get { return _lstPedidosAreas; }
            set { _lstPedidosAreas= value; }
        }
        public PEDIDOS_AREAS Area
        {
            get { return _area; }
            set { _area = value; }
        }
        #endregion

        #region constructor
        public frmSeleccionArea()
        {
            InitializeComponent();
        }
        #endregion

        private void frmSeleccionArea_Load(object sender, EventArgs e)
        {
            lsbAreasAsignadas.DataSource = _lstPedidosAreas;
            lsbAreasAsignadas.DisplayMember = "PEA_NOMBRE";
            lsbAreasAsignadas.ValueMember = "PEA_CODIGO";
        }

        #region eventos
        private void lsbAreasAsignadas_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lsbAreasAsignadas.SelectedItem != null)
                {
                    _area = (PEDIDOS_AREAS)lsbAreasAsignadas.SelectedItem;
                    this.Close();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private void lsbAreasAsignadas_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (lsbAreasAsignadas.SelectedItem != null)
                {
                    _area = (PEDIDOS_AREAS)lsbAreasAsignadas.SelectedItem;
                    this.Close();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        #endregion
    }
}
