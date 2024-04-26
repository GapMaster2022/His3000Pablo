using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using His.Negocio;

namespace TarifariosUI
{
    public partial class frmAdministrarAseguradoras : GeneralApp.FrmAdministracion
    {
        private HIS3000BDEntities conexion;
        public frmAdministrarAseguradoras()
        {
            InitializeComponent();
            //menu.Enabled = His.Parametros.AccesosModuloTarifario.AseguradorasEmpresasCRUD;
            conexion = new HIS3000BDEntities(ConexionEntidades.ConexionEDM); 
        }

        private void frmAdministrarAseguradoras2_Load(object sender, EventArgs e)
        {
            try
            {
                //Actualizo la fecha de los controles
                dtpFinConvenio.Value = DateTime.Today;
                dtpInicioConvenio.Value = DateTime.Today;

                //cargo el tipo de empresa
                var tipoEmpresaLista = from t in conexion.TIPO_EMPRESA
                                       where t.TE_ESTADO == true
                                       select t;
                cboTipoEmpresa.DataSource = tipoEmpresaLista;
                cboTipoEmpresa.DisplayMember = "TE_DESCRIPCION";
                cboTipoEmpresa.ValueMember = "TE_CODIGO"; 
                //this.listaGridview.Columns["ASE_ESTADO"].Visible = false;
                //this.listaGridview.Columns["CONVENIOS_TARIFARIOS"].Visible = false;
                //this.listaGridview.Columns["PACIENTES_ASEGURADORAS"].Visible = false;
                //this.listaGridview.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                this.listaGridview.DisplayLayout.Bands[0].Columns["ASE_ESTADO"].Hidden  = true;
                this.listaGridview.DisplayLayout.Bands[0].Columns["CONVENIOS_TARIFARIOS"].Hidden = true;
                this.listaGridview.DisplayLayout.Bands[0].Columns["PACIENTES_ASEGURADORAS"].Hidden = true;
                this.listaGridview.DisplayLayout.Bands[0].Columns["TIPO_EMPRESA"].Hidden = true;

                this.listaGridview.DisplayLayout.Bands[0].Columns["ASE_CODIGO"].Header.Caption = "CODIGO";
                this.listaGridview.DisplayLayout.Bands[0].Columns["ASE_NOMBRE"].Header.Caption = "NOMBRE";
                this.listaGridview.DisplayLayout.Bands[0].Columns["ASE_RUC"].Header.Caption = "RUC";
                this.listaGridview.DisplayLayout.Bands[0].Columns["ASE_DIRECCION"].Header.Caption = "DIRECCION";
                this.listaGridview.DisplayLayout.Bands[0].Columns["ASE_TELEFONO"].Header.Caption = "TELEFONO";
                this.listaGridview.DisplayLayout.Bands[0].Columns["ASE_INICIO_CONVENIO"].Header.Caption = "INI. CONVENIO";
                this.listaGridview.DisplayLayout.Bands[0].Columns["ASE_FIN_CONVENIO"].Header.Caption = "FIN CONVENIO";
                this.listaGridview.DisplayLayout.Bands[0].Columns["ASE_CIUDAD"].Header.Caption = "CIUDAD";
                this.listaGridview.DisplayLayout.Bands[0].Columns["ASE_CONVENIO"].Header.Caption = "CON CONVENIO";

                this.listaGridview.DisplayLayout.PerformAutoResizeColumns(true, Infragistics.Win.UltraWinGrid.PerformAutoSizeType.VisibleRows);

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override bool Cancelar()
        {
            txtNombre.ReadOnly = true;
            txtDireccion.ReadOnly = true;
            txtCiudad.ReadOnly = true;
            txtRUC.ReadOnly = true;
            txtTelefono.Text="";
            cboTipoEmpresa.Enabled = true;
            return true;
        }

        protected override bool CargarItem()
        {
            try
            {
                //
                txtNombre.ReadOnly = false;
                txtDireccion.ReadOnly = false;
                txtCiudad.ReadOnly = false;
                txtRUC.ReadOnly = false;
                txtTelefono.Text = "";
                //
                cboTipoEmpresa.Enabled = false; 
                //
                Infragistics.Win.UltraWinGrid.UltraGridRow fila = listaGridview.ActiveRow;     
                Int16 codigo = Convert.ToInt16(fila.Cells[0].Value.ToString());
                ASEGURADORAS_EMPRESAS aseguradora = conexion.ASEGURADORAS_EMPRESAS.FirstOrDefault(
                    a => a.ASE_CODIGO == codigo);
                this.txtCodigo.Text = aseguradora.ASE_CODIGO.ToString();
                this.txtNombre.Text = aseguradora.ASE_NOMBRE;
                this.txtDireccion.Text = aseguradora.ASE_DIRECCION;
                this.txtRUC.Text = aseguradora.ASE_RUC;
                this.txtTelefono.Text = aseguradora.ASE_TELEFONO;
                if (aseguradora.ASE_CONVENIO == true)
                {
                    grpFechasConvenio.Enabled = true; 
                    chkConConvenio.Checked = true;
                    this.dtpFinConvenio.Value = aseguradora.ASE_FIN_CONVENIO!=null?aseguradora.ASE_FIN_CONVENIO.Value:DateTime.Now ;
                    this.dtpInicioConvenio.Value = aseguradora.ASE_INICIO_CONVENIO!=null?aseguradora.ASE_INICIO_CONVENIO.Value:DateTime.Now ;
                }
                else
                {
                    grpFechasConvenio.Enabled = false; 
                    grpFechasConvenio.Enabled = true; 
                    chkConConvenio.Checked = false;
                }
                txtCiudad.Text = aseguradora.ASE_CIUDAD; 

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
            ASEGURADORAS_EMPRESAS aseguradora;
            try
            {
                if (txtCodigo.Text == "")
                {
                    TIPO_EMPRESA tipoEmpresa = (TIPO_EMPRESA)cboTipoEmpresa.SelectedItem;   
                    int codigo = conexion.ASEGURADORAS_EMPRESAS.Max(a => a.ASE_CODIGO);
                    aseguradora = new ASEGURADORAS_EMPRESAS();
                    aseguradora.ASE_CODIGO = Convert.ToInt16(codigo + 1);
                    aseguradora.ASE_DIRECCION = txtDireccion.Text;
                    aseguradora.ASE_ESTADO = true;
                    if (chkConConvenio.Checked == true)
                    {
                        aseguradora.ASE_CONVENIO = true;   
                        aseguradora.ASE_FIN_CONVENIO = dtpFinConvenio.Value;
                        aseguradora.ASE_INICIO_CONVENIO = dtpInicioConvenio.Value;
                    }
                    else
                    {
                        aseguradora.ASE_CONVENIO = false;
                        aseguradora.ASE_FIN_CONVENIO = null;
                        aseguradora.ASE_INICIO_CONVENIO = null;
                    }
                    aseguradora.ASE_NOMBRE = txtNombre.Text;
                    aseguradora.ASE_RUC = txtRUC.Text;
                    aseguradora.ASE_TELEFONO = txtTelefono.Text.Replace("-",string.Empty).ToString();
                    aseguradora.ASE_CIUDAD = txtCiudad.Text;
                    aseguradora.TIPO_EMPRESAReference.EntityKey = tipoEmpresa.EntityKey;  
                    conexion.AddToASEGURADORAS_EMPRESAS(aseguradora);
                }
                else
                {
                    int codigo = Convert.ToInt16(txtCodigo.Text);
                    aseguradora = conexion.ASEGURADORAS_EMPRESAS.FirstOrDefault(
                                                a => a.ASE_CODIGO == codigo);
                    aseguradora.ASE_DIRECCION = txtDireccion.Text;
                    aseguradora.ASE_ESTADO = true;
                    if (chkConConvenio.Checked == true)
                    {
                        aseguradora.ASE_CONVENIO = true;
                        aseguradora.ASE_FIN_CONVENIO = dtpFinConvenio.Value;
                        aseguradora.ASE_INICIO_CONVENIO = dtpInicioConvenio.Value;
                    }
                    else
                    {
                        aseguradora.ASE_CONVENIO = false;
                        aseguradora.ASE_FIN_CONVENIO = null;
                        aseguradora.ASE_INICIO_CONVENIO = null;
                    }
                    aseguradora.ASE_NOMBRE = txtNombre.Text;
                    aseguradora.ASE_RUC = txtRUC.Text;
                    aseguradora.ASE_TELEFONO = txtTelefono.Text.Replace("-", string.Empty).ToString();
                    aseguradora.ASE_CIUDAD = txtCiudad.Text;   
                }

                conexion.SaveChanges();
                //
                TIPO_EMPRESA tipoEmpresaCombo = (TIPO_EMPRESA)cboTipoEmpresa.SelectedItem;
                //cargo el grid
                var QueryAseguradoras = from a in conexion.ASEGURADORAS_EMPRESAS
                                        where a.ASE_ESTADO == true && a.TIPO_EMPRESA.TE_CODIGO == tipoEmpresaCombo.TE_CODIGO
                                        select a;

                this.listaGridview.DataSource = QueryAseguradoras;
                //
                cboTipoEmpresa.Enabled = true;
                //
                txtNombre.ReadOnly = true;
                txtDireccion.ReadOnly = true;
                txtCiudad.ReadOnly = true;
                txtRUC.ReadOnly = true;
                txtTelefono.Text="";
                //
                MessageBox.Show("Datos guardados exitosamente ", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);    
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
                //if (this.listaGridview.CurrentCell != null)
                if (this.listaGridview.ActiveRow != null)
                {
                    //Int16 codigo = Convert.ToInt16(listaGridview.CurrentRow.Cells["ASE_CODIGO"].Value);
                    Int16 codigo = Convert.ToInt16(listaGridview.ActiveRow.Cells["ASE_CODIGO"].Value);
                    ASEGURADORAS_EMPRESAS aseguradora = conexion.ASEGURADORAS_EMPRESAS.FirstOrDefault(
                        a => a.ASE_CODIGO == codigo);
                    conexion.DeleteObject(aseguradora);
                    conexion.SaveChanges();
                    txtNombre.ReadOnly = true;
                    txtDireccion.ReadOnly = true;
                    txtCiudad.ReadOnly = true;
                    txtRUC.ReadOnly = true;
                    txtTelefono.Text = "";
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }

        protected override bool NuevoItem()
        {
            try
            {
                //txtCodigo.Text = conexion.ASEGURADORAS_EMPRESAS.Max(a => a.ASE_CODIGO).ToString();
                txtNombre.ReadOnly = false;
                txtDireccion.ReadOnly = false;
                txtCiudad.ReadOnly = false;
                txtRUC.ReadOnly = false;
                txtTelefono.Mask ="00-000-0000";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }

     

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboTipoEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoEmpresa.Items.Count > 0)
            {
                TIPO_EMPRESA tipoEmpresa = (TIPO_EMPRESA)cboTipoEmpresa.SelectedItem; 
                //cargo el grid
                var QueryAseguradoras = from a in conexion.ASEGURADORAS_EMPRESAS 
                                        where a.ASE_ESTADO == true && a.TIPO_EMPRESA.TE_CODIGO ==tipoEmpresa.TE_CODIGO  
                                        select a;

                this.listaGridview.DataSource = QueryAseguradoras;
            }
            else
            { 
                this.listaGridview.DataSource =null;
            }
        }

        private void chkConConvenio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConConvenio.Checked == true)
                grpFechasConvenio.Enabled = true;
            else
                grpFechasConvenio.Enabled = false;  
        }

        private void txtCiudad_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void cboTipoEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtCodigo.Focus();
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtNombre.Focus();
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtDireccion.Focus();
            }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtCiudad.Focus();
            }
        }

        private void txtCiudad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtRUC.Focus();
            }
        }

        private void txtRUC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtTelefono.Focus();
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                dtpInicioConvenio.Focus();
            }
        }

        private void dtpInicioConvenio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                dtpFinConvenio.Focus();
            }
        }

        private void txtRUC_Leave(object sender, EventArgs e)
        {
            if (txtRUC.Text.ToString() != string.Empty)
                if (NegValidaciones.esCedulaValida(txtRUC.Text) != true)
                {
                    MessageBox.Show("RUC no válido", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRUC.Focus();
                }
        }

        private void txtTelefono_Leave(object sender, EventArgs e)
        {
            if (txtTelefono.Text.ToString() != "  -   -")
            {
                if (NegValidaciones.esTelefonoValido(txtTelefono.Text.Replace("-", string.Empty).ToString()) == false)
                {
                    MessageBox.Show("Numero de teléfono incorrecto");
                    txtTelefono.Focus();
                }
            }
        }

        private void txtRUC_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpInicioConvenio_ValueChanged(object sender, EventArgs e)
        {
        
        }

        private void dtpFinConvenio_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dtpInicioConvenio.MaxDate = dtpFinConvenio.Value;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
            
        }

        private void listaGridview_DoubleClickCell(object sender, Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs e)
        {
            AdminMenu("actualizar");
            CargarItem();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listaGridview_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

    }
}
