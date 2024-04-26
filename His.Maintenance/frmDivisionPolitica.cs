using His.Entidades;
using His.Negocio;
using Recursos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Maintenance
{
    public partial class frmDivisionPolitica : Form
    {


        DIVISION_POLITICA divisionPolitica = null;
        List<CLASE_LOCALIDAD> clasesLocalidad = new List<CLASE_LOCALIDAD>();
        List<TIPO_LOCALIDAD> tiposLocalidad = new List<TIPO_LOCALIDAD>();
        List<DIVISION_POLITICA> paises = new List<DIVISION_POLITICA>();

        public frmDivisionPolitica()
        {
            clasesLocalidad = NegDivisionPolitica.clasesLocalidad();
            tiposLocalidad = NegDivisionPolitica.tiposLocalidad();
            string mascara1 = "n";

            for (int i = 0; i < 19; i++)
                mascara1 += "n";

            paises = NegDivisionPolitica.listaDivisionPolitica("2");

            InitializeComponent();

            InicializarForma();

            txtCodInec.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.Integer;
            txtLatitud.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.Integer;
            txtLongitud.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.Integer;

            txtCodInec.InputMask = mascara1;
            txtCodPadre.MaxLength = 20;
            txtLongitud.InputMask = mascara1;
            txtLatitud.InputMask = mascara1;
        }
        private void InicializarForma()
        {
            txtPadre.ReadOnly = true;
            txtCodPadre.ReadOnly = true;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;

            txtNombre.MaxLength = 200;


            ultraTree.Nodes.Clear();
            ultraTree.Override.CellClickAction = Infragistics.Win.UltraWinTree.CellClickAction.SelectNodeOnly;

            Infragistics.Win.UltraWinTree.UltraTreeNode raiz = new Infragistics.Win.UltraWinTree.UltraTreeNode();

            raiz.Key = "0";
            //raiz.Text = "TODOS";
            raiz.Override.NodeAppearance.Image = Archivo.mundO32X32;
            raiz.Override.NodeAppearance.ForeColor = Color.DarkBlue;
            ultraTree.Nodes.Add(raiz);

            foreach (var item in paises)
            {
                Infragistics.Win.UltraWinTree.UltraTreeNode nodo = new Infragistics.Win.UltraWinTree.UltraTreeNode();
                nodo.Key = item.DIPO_CODIINEC;
                nodo.Text = item.DIPO_NOMBRE;
                nodo.Override.NodeAppearance.ForeColor = Color.MidnightBlue;
                nodo.Override.NodeAppearance.Image = Archivo.mundo2_24x24;
                nodo.Override.NodeStyle = Infragistics.Win.UltraWinTree.NodeStyle.Standard;

                raiz.Nodes.Add(nodo);
            }


            cb_claseLocalidades.DataSource = clasesLocalidad;
            cb_claseLocalidades.ValueMember = "CLLO_CODIGO";
            cb_claseLocalidades.DisplayMember = "CLLO_NOMBRE";

            cb_tipoLocalidades.DataSource = tiposLocalidad;
            cb_tipoLocalidades.ValueMember = "TILO_CODIGO";
            cb_tipoLocalidades.DisplayMember = "TILO_NOMBRE";
        }
        public void CargarHijos(Infragistics.Win.UltraWinTree.UltraTreeNode nodoRaiz)
        {
            try
            {
                List<DIVISION_POLITICA> hijos = NegDivisionPolitica.RecuperarDivisionPolitica(nodoRaiz.Key);
                foreach (var item in hijos)
                {
                    Infragistics.Win.UltraWinTree.UltraTreeNode nodo = new Infragistics.Win.UltraWinTree.UltraTreeNode();

                    if (item.DIPO_CODIINEC != "0")
                    {

                        nodo.Key = item.DIPO_CODIINEC;
                        nodo.Text = item.DIPO_NOMBRE;
                        //nodo.Tag = codPadre;
                        nodo.Override.NodeAppearance.ForeColor = Color.FromArgb(30, 30, 30);
                        nodo.Override.NodeAppearance.Image = Archivo.ubicacion_16x16;
                        nodoRaiz.Nodes.Add(nodo);
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarInformacion(string CODINEC)
        {
            try
            {
                limpiarCampos();
                errorProvider.Clear();
                divisionPolitica = NegDivisionPolitica.DivisionPolitica(CODINEC);
                if (divisionPolitica != null)
                {
                    txtCodInec.Text = divisionPolitica.DIPO_CODIINEC;
                    txtNombre.Text = divisionPolitica.DIPO_NOMBRE;
                    txtLatitud.Text = divisionPolitica.DIPO_LATITUD;
                    txtLongitud.Text = divisionPolitica.DIPO_LONGITUD;

                    DIVISION_POLITICA padre = NegDivisionPolitica.DivisionPolitica(divisionPolitica.DIPO_DIPO_CODIINEC);
                    txtCodPadre.Text = padre.DIPO_CODIINEC;
                    txtPadre.Text = padre.DIPO_NOMBRE;

                    cb_claseLocalidades.SelectedItem = clasesLocalidad.FirstOrDefault(c => c.EntityKey == divisionPolitica.CLASE_LOCALIDADReference.EntityKey);
                    cb_tipoLocalidades.SelectedItem = tiposLocalidad.FirstOrDefault(t => t.EntityKey == divisionPolitica.TIPO_LOCALIDADReference.EntityKey);
                    txtCodInec.ReadOnly = true;
                    btnGuardar.Enabled = true;
                    btnCancelar.Enabled = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void limpiarCampos()
        {
            txtCodInec.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtPadre.Text = string.Empty;
            txtLatitud.Text = string.Empty;
            txtLongitud.Text = string.Empty;
            txtCodPadre.Text = string.Empty;
            cb_claseLocalidades.SelectedValue = 0;
            cb_tipoLocalidades.SelectedValue = 0;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void GuardarDatos()
        {
            try
            {
                if (divisionPolitica != null)
                {
                    divisionPolitica.DIPO_NOMBRE = txtNombre.Text.ToString();
                    divisionPolitica.DIPO_LONGITUD = txtLongitud.Text.ToString();
                    divisionPolitica.DIPO_LATITUD = txtLatitud.Text.ToString();
                    //divisionPolitica.CLASE_LOCALIDADReference.EntityKey = ((CLASE_LOCALIDAD)cb_claseLocalidades.SelectedItem).EntityKey;
                    divisionPolitica.TIPO_LOCALIDADReference.EntityKey = ((TIPO_LOCALIDAD)cb_tipoLocalidades.SelectedItem).EntityKey;
                    NegDivisionPolitica.EditarDivisionPolitica(divisionPolitica);

                    Infragistics.Win.UltraWinTree.UltraTreeNode node = new Infragistics.Win.UltraWinTree.UltraTreeNode();
                    node = ultraTree.GetNodeByKey(divisionPolitica.DIPO_CODIINEC);
                    node.Text = divisionPolitica.DIPO_NOMBRE;
                }
                else
                {
                    divisionPolitica = new DIVISION_POLITICA();
                    divisionPolitica.DIPO_CODIGO = NegDivisionPolitica.maxCodigo() + 1;
                    divisionPolitica.DIPO_CODIINEC = txtCodInec.Text;
                    divisionPolitica.DIPO_DIPO_CODIINEC = txtCodPadre.Text.Trim().ToString();
                    divisionPolitica.DIPO_NOMBRE = txtNombre.Text;
                    divisionPolitica.DIPO_LATITUD = txtLatitud.Text;
                    divisionPolitica.DIPO_LONGITUD = txtLongitud.Text;
                    divisionPolitica.CLASE_LOCALIDADReference.EntityKey = ((CLASE_LOCALIDAD)cb_claseLocalidades.SelectedItem).EntityKey;
                    divisionPolitica.TIPO_LOCALIDADReference.EntityKey = ((TIPO_LOCALIDAD)cb_tipoLocalidades.SelectedItem).EntityKey;
                    NegDivisionPolitica.CrearDivisionPolitica(divisionPolitica);

                    Infragistics.Win.UltraWinTree.UltraTreeNode raiz = new Infragistics.Win.UltraWinTree.UltraTreeNode();
                    raiz = ultraTree.GetNodeByKey(divisionPolitica.DIPO_DIPO_CODIINEC);

                    Infragistics.Win.UltraWinTree.UltraTreeNode nodo = new Infragistics.Win.UltraWinTree.UltraTreeNode();
                    nodo.Key = divisionPolitica.DIPO_CODIINEC;
                    nodo.Text = divisionPolitica.DIPO_NOMBRE;

                    if (divisionPolitica.DIPO_DIPO_CODIINEC == "0")
                    {
                        nodo.Override.NodeAppearance.ForeColor = Color.MidnightBlue;
                        nodo.Override.NodeAppearance.Image = Archivo.mundo2_24x24;
                    }
                    else
                    {
                        nodo.Override.NodeAppearance.ForeColor = Color.FromArgb(30, 30, 30);
                        nodo.Override.NodeAppearance.Image = Archivo.ubicacion_16x16;
                    }
                    nodo.Override.NodeStyle = Infragistics.Win.UltraWinTree.NodeStyle.Standard;

                    raiz.Nodes.Add(nodo);

                }
                MessageBox.Show("Información almacenada correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Infragistics.Win.UltraWinTree.UltraTreeNode nodoEliminar = ultraTree.GetNodeByKey(ultraTree.ActiveNode.Key);

                DialogResult resp = MessageBox.Show("Seguro desea eliminar esta division: " + nodoEliminar.Text, "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {

                    NegDivisionPolitica.EliminarDivisionPolitica(nodoEliminar.Key);
                    nodoEliminar.Remove();

                    limpiarCampos();
                    MessageBox.Show("Registro Eliminado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception w)
            {
                MessageBox.Show(w.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraTree_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                if (e.Button == MouseButtons.Right)
                {
                    Infragistics.Win.UltraWinTree.UltraTreeNode nodo = ultraTree.GetNodeFromPoint(e.X, e.Y);
                    if (nodo != null)
                    {
                        if (nodo.Key == ultraTree.ActiveNode.Key)
                        {

                            if (nodo.HasNodes == true)
                            {
                                eliminarToolStripMenuItem.Visible = false;
                            }
                            else
                            {
                                eliminarToolStripMenuItem.Visible = true;
                            }

                            infMenuStrip.Show(ultraTree, e.Location.X, e.Location.Y);
                        }
                    }
                }
            }
            catch (Exception w)
            {
                MessageBox.Show(w.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                Infragistics.Win.UltraWinTree.UltraTreeNode nodo = ultraTree.GetNodeByKey(txtCodInec.Text);

                if (divisionPolitica == null && nodo != null)
                    MessageBox.Show("Codigo del INEC ya existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ValidarFormulario() == true)
                    GuardarDatos();
                else
                    MessageBox.Show("Información Incompleta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            txtCodPadre.Text = ultraTree.ActiveNode.Key;
            if (ultraTree.ActiveNode.Key != "0")
                txtPadre.Text = ultraTree.ActiveNode.Text;
            else
                txtPadre.Text = "N/A";
            txtCodInec.Focus();
            divisionPolitica = null;
            txtCodInec.ReadOnly = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void ultraTree_BeforeActivate(object sender, Infragistics.Win.UltraWinTree.CancelableNodeEventArgs e)
        {
            if (e.TreeNode.HasNodes == false)
                CargarHijos(e.TreeNode);

            if (e.TreeNode.Key != "0")
                CargarInformacion(e.TreeNode.Key);
            else
            {
                limpiarCampos();
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
        }
        private bool ValidarFormulario()
        {
            bool band = true;

            if (txtCodInec.Text == string.Empty)
            {
                band = false;
                AgregarError(txtCodInec);
            }
            if (txtNombre.Text == string.Empty)
            {
                band = false;
                AgregarError(txtNombre);
            }
            if (txtCodPadre.Text == string.Empty)
            {
                band = false;
                AgregarError(txtCodPadre);
            }
            if (((CLASE_LOCALIDAD)cb_claseLocalidades.SelectedItem) == null)
            {
                band = false;
                AgregarError(cb_claseLocalidades);
            }
            if (((TIPO_LOCALIDAD)cb_tipoLocalidades.SelectedItem) == null)
            {
                band = false;
                AgregarError(cb_tipoLocalidades);
            }

            return band;
        }
        private void AgregarError(Control control)
        {
            errorProvider.SetError(control, "Campo Requerido");
        }
    }
}
