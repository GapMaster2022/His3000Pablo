using System;
using System.Windows.Forms;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using His.Entidades;
using Infragistics.Win;
using His.Negocio;

namespace TarifariosUI
{
    public partial class frmAdministrarTarifario : GeneralApp.FrmAdministracion
    {
        private HIS3000BDEntities  conexion;
        public frmAdministrarTarifario()
        {
            InitializeComponent();
            menu.Enabled = His.Parametros.AccesosModuloTarifario.TarifariosCRUD;
            conexion = new HIS3000BDEntities(ConexionEntidades.ConexionEDM); 
        }

        private void CargarControles()
        {
            cboTipoTarifario.DataSource = NegCatalogos.RecuperarCatalogoListaGen("TARIFARIOS", "TAR_TIPO");
            cboTipoTarifario.ValueMember = "prefijo";
            cboTipoTarifario.DisplayMember = "nombre";
        }

        private void frmAdministrarTarifario_Load(object sender, EventArgs e)
        {
            try
            {
                //cargo el grid
                var QueryTarifario = from q in conexion.TARIFARIOS
                                     orderby q.TAR_NOMBRE 
                                     select q;

                this.listaGridview.DataSource = QueryTarifario;
                this.listaGridview.DisplayLayout.Bands[0].Columns["ESPECIALIDADES_TARIFARIOS"].Hidden  = true;

                this.listaGridview.DisplayLayout.Bands[0].Columns["TAR_CODIGO"].Header.Caption = "CODIGO";
                this.listaGridview.DisplayLayout.Bands[0].Columns["TAR_NOMBRE"].Header.Caption = "NOMBRE";
                this.listaGridview.DisplayLayout.Bands[0].Columns["TAR_DESCRIPCION"].Header.Caption = "DESCRIPCION";
                this.listaGridview.DisplayLayout.Bands[0].Columns["TAR_ACTIVO"].Header.Caption = "ACTIVO";
                this.listaGridview.DisplayLayout.Bands[0].Columns["TAR_TIPO"].Header.Caption = "TIPO";

                this.listaGridview.DisplayLayout.PerformAutoResizeColumns(true, Infragistics.Win.UltraWinGrid.PerformAutoSizeType.VisibleRows);

                //this.listaGridview.Columns["ESPECIALIDADES_TARIFARIOS"].Visible = false;
                //this.listaGridview.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                CargarControles();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        protected override bool CargarItem()
        {
            try
            {
                //DataGridViewRow fila = this.listaGridview.CurrentRow;
                Infragistics.Win.UltraWinGrid.UltraGridRow   fila = this.listaGridview.ActiveRow;
                Int16 codigo = Convert.ToInt16(fila.Cells[0].Value.ToString());
                TARIFARIOS  tarifario = conexion.TARIFARIOS.FirstOrDefault(
                    t => t.TAR_CODIGO == codigo);
                if (tarifario != null)
                {
                    txtCodigo.Text = tarifario.TAR_CODIGO.ToString();  
                    txtNombre.Text   = tarifario.TAR_NOMBRE;
                    txtDescripcion.Text   = tarifario.TAR_DESCRIPCION;
                    cboTipoTarifario.SelectedValue = tarifario.TAR_TIPO;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        protected override bool GuardarItem()
        {
            TARIFARIOS tarifario;
            try
            {
                if (txtCodigo.Text == "")
                {
                    int codigo = conexion.TARIFARIOS.Max(t => t.TAR_CODIGO);
                    tarifario = new TARIFARIOS();
                    tarifario.TAR_CODIGO  = Convert.ToInt16(codigo + 1);
                    tarifario.TAR_NOMBRE = txtNombre.Text;
                    tarifario.TAR_DESCRIPCION  = txtDescripcion.Text;
                    tarifario.TAR_ACTIVO = true;
                    tarifario.TAR_TIPO = cboTipoTarifario.SelectedValue.ToString();
                    conexion.AddToTARIFARIOS(tarifario);
                }
                else
                {
                    int codigo = Convert.ToInt16(txtCodigo.Text);
                    tarifario = conexion.TARIFARIOS.FirstOrDefault(
                                                t => t.TAR_CODIGO== codigo);
                    tarifario.TAR_NOMBRE = txtNombre.Text;
                    tarifario.TAR_DESCRIPCION = txtDescripcion.Text;
                    tarifario.TAR_TIPO = cboTipoTarifario.SelectedValue.ToString();
                    tarifario.TAR_ACTIVO = true;  
                }

                conexion.SaveChanges();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        protected override bool EliminarItem()
        {
            try
            {
                if (this.listaGridview.ActiveRow != null)
                {
                    Int16 codigo = Convert.ToInt16(listaGridview.ActiveRow.Cells["TAR_CODIGO"].Value);
                    //Int16 codigo = Convert.ToInt16(listaGridview.CurrentRow.Cells["TAR_CODIGO"].Value);
                    TARIFARIOS tarifario = conexion.TARIFARIOS.FirstOrDefault(
                                               t => t.TAR_CODIGO == codigo);
                    conexion.DeleteObject(tarifario);
                    conexion.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }

        private void listaGridview_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void listaGridview_DoubleClickCell(object sender, Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs e)
        {
            AdminMenu("actualizar");
            CargarItem();
        }
    }
}
