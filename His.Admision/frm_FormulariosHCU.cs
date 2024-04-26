using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Recursos;
using His.Entidades;
using His.Negocio;
using His.General;
using His.Parametros;

namespace His.Admision
{
    public partial class frm_FormulariosHCU : Form
    {
        #region Variables

        private List<FORMULARIOS_HCU> formularios = null;
        private FORMULARIOS_HCU formulario = null;
        FtpClient ftp = null;
        bool formularioNuevo = false;

        #endregion

        public frm_FormulariosHCU()
        {
            //formularios = NegFormulariosHCU.RecuperarFormulariosHCU();
            InitializeComponent();
            cargarDatos();
        }

        private void cargarDatos()
        {
            try
            {
                //btnNuevo.Image = Archivo.btnNuevo16;
                //btnEliminar.Image = Archivo.imgBtnDelete;
                //btnGuardar.Image = Archivo.btnGuardar16;
                //btnCancelar.Image = Archivo.btnCancel16;

                btnEliminar.Enabled = false;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;

                panelDatos.Enabled = false;

                RecuperarFormulariosGrid();

                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                if (e.InnerException != null)
                    MessageBox.Show(e.InnerException.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = " Archivos de Excel  (*.xls)|*.xls";
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            txt_directorio.Text = openFileDialog1.FileName;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            formularioNuevo = true;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnNuevo.Enabled = false;
            panelDatos.Enabled = true;
            limpiarCampos();
            txt_codigo.Text = (NegFormulariosHCU.ultimoCodigoFormularios()+1).ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GrabarDatos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            error.Clear();
            formularioNuevo = false;
            formulario = null;
            btnNuevo.Enabled = true;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            panelDatos.Enabled = false;
            limpiarCampos();
        }

        private void limpiarCampos()
        {
            txt_codigo.Text = string.Empty;
            txt_form.Text = string.Empty;
            txt_nombre.Text = string.Empty;
            txt_descripcion.Text = string.Empty;
            txt_directorio.Text = string.Empty;
        }

        private void cargarFormulario(int codigo)
        {
            error.Clear();
            formulario = NegFormulariosHCU.RecuperarFormularioID(codigo);

            if (formulario != null)
            {
                txt_codigo.Text = formulario.FH_CODIGO.ToString();
                txt_form.Text = formulario.FH_FORM;
                txt_nombre.Text = formulario.FH_NOMBRE.ToString();
                txt_descripcion.Text = formulario.FH_DESCRIPCION.ToString();
                txt_directorio.Text = formulario.FH_DIRECTORIO;
                btnNuevo.Enabled = false;
                btnEliminar.Enabled = true;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
                formularioNuevo = false;
                panelDatos.Enabled = true;
            }
            else
            {

            }
        }

        private bool ValidarFormulario()
        {
            bool band = true;

            if (txt_form.Text == string.Empty)
            {
                AgregarError(txt_form);
                band = false;
            }

            if (txt_nombre.Text == string.Empty)
            {
                AgregarError(txt_nombre);
                band = false;
            }

            if (txt_descripcion.Text == string.Empty)
            {
                AgregarError(txt_descripcion);
                band = false;
            }

            if (txt_directorio.Text == string.Empty)
            {
                AgregarError(txt_directorio);
                band = false;
            }

            return band;
        }

        private void AgregarError(Control control)
        {
            error.SetError(control,"Campo Requerido");
        }

        private void GrabarDatos()
        {
            try
            {
                error.Clear();
                if (ValidarFormulario())
                {
                    if (formularioNuevo == true && formulario == null)
                    {
                        formulario = new FORMULARIOS_HCU();
                        formulario.FH_CODIGO = NegFormulariosHCU.ultimoCodigoFormularios() + 1;
                        formulario.FH_FORM = txt_form.Text.ToString();
                        formulario.FH_NOMBRE = txt_nombre.Text.ToString();
                        formulario.FH_DESCRIPCION = txt_descripcion.Text.ToString();

                        ftp = new FtpClient();
                        ftp.Login();
                        ftp.ChangeDir(GeneralPAR.getDirectorioMatrizFormularios());
                        ftp.Upload(txt_directorio.Text);
                        formulario.FH_DIRECTORIO = openFileDialog1.SafeFileName;
                        ftp.Close();
                        formulario.FH_FECHA_CREACION = DateTime.Now;
                        formulario.FH_ESTADO = true;
                        NegFormulariosHCU.Crear(formulario);
                    }
                    else
                    {
                        formulario.FH_FORM = txt_form.Text.ToString();
                        formulario.FH_NOMBRE = txt_nombre.Text.ToString();
                        formulario.FH_DESCRIPCION = txt_descripcion.Text.ToString();

                        if (formulario.FH_DIRECTORIO != txt_directorio.Text.ToString())
                        {
                            ftp = new FtpClient();
                            ftp.Login();
                            ftp.ChangeDir(GeneralPAR.getDirectorioMatrizFormularios());
                            ftp.DeleteFile(formulario.FH_DIRECTORIO);
                            ftp.Upload(txt_directorio.Text);
                            formulario.FH_DIRECTORIO = openFileDialog1.SafeFileName;
                            ftp.Close();
                        }
                        NegFormulariosHCU.Editar(formulario);
                    }

                    RecuperarFormulariosGrid();
                    btnNuevo.Enabled = true;
                    btnEliminar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnCancelar.Enabled = false;
                    panelDatos.Enabled = false;
                    limpiarCampos();
                    formularioNuevo = false;
                    formulario = null;
                    MessageBox.Show("Datos almacenados correctamente");

                }
                else
                {
                    MessageBox.Show("Informacion Incompleta");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                if (e.InnerException != null)
                    MessageBox.Show(e.InnerException.Message);
            }
        }

        private void gridFormularios_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int codigo = Convert.ToInt32(gridFormularios.Rows[e.RowIndex].Cells["CODIGO"].Value.ToString());
            cargarFormulario(codigo);
        }

        private void txt_form_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(char)Keys.Enter)
            {
                e.Handled = true;
                txt_nombre.Focus();
            }
        }

        private void txt_nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txt_descripcion.Focus();
            }
        }

        private void txt_descripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnBuscar.Focus();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                error.Clear();
                ftp = new FtpClient();
                ftp.ChangeDir(GeneralPAR.getDirectorioMatrizFormularios());
                ftp.DeleteFile(formulario.FH_DIRECTORIO);
                ftp.Close();
                NegFormulariosHCU.Eliminar(formulario);
                formularioNuevo = false;
                formulario = null;
                btnNuevo.Enabled = true;
                btnEliminar.Enabled = false;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                panelDatos.Enabled = false;
                limpiarCampos();
                RecuperarFormulariosGrid();
                MessageBox.Show("Formulario eliminado");
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message);
                if (r.InnerException != null)
                    MessageBox.Show(r.InnerException.Message);
            }
        }

        private void RecuperarFormulariosGrid()
        {
            formularios = NegFormulariosHCU.RecuperarFormulariosHCU();
            gridFormularios.DataSource = formularios.Select(f => new {CODIGO=f.FH_CODIGO, ID = f.FH_FORM, NOMBRE = f.FH_NOMBRE, DIRECTORIO = f.FH_DIRECTORIO}).ToList();
            gridFormularios.Columns["CODIGO"].Width = 75;
            gridFormularios.Columns["ID"].Width = 75;
            gridFormularios.Columns["NOMBRE"].Width = 200;
            gridFormularios.Columns["DIRECTORIO"].Width = 350;
        }

    }
}
