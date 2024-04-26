using System;
using System.Windows.Forms;
using GeneralApp.Properties;
using Recursos;

namespace GeneralApp
{
    public partial class FrmAdministracion : Form
    {
        private bool _estadoSalir;

        public FrmAdministracion()
        {
            InitializeComponent();
        }

        #region Metodos

            // metodo que limpia los textbox
            protected virtual void LimpiarTextBox()
            {
                // hace un chequeo por todos los textbox del formulario
                foreach (Control oControls in camposPanel.Controls)
                {
                    if (oControls is TextBox)
                    {
                        oControls.Text = ""; // eliminar el texto
                    }
                }
            }

        #endregion

        #region Metodos Heredados

            protected virtual void AdminMenu(String control)
            {
                btnNuevo.Enabled = false;
                btnActualizar.Enabled = false;
                btnEliminar.Enabled = false;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false ;
                switch (control)
                {
                    case "nuevo":
                    case "actualizar":
                        btnGuardar.Enabled = true;
                        btnCancelar.Enabled = true;
                        btnCancelar.Image = Archivo.imgBtnStop;
                        btnCancelar.Text = Archivo.Botones_General_Texto_Cancelar; 
                        _estadoSalir = false;
                        break;
                    default:
                        btnNuevo.Enabled = true;
                        btnActualizar.Enabled = true;
                        btnEliminar.Enabled = true;
                        btnCancelar.Enabled = true;
                        btnCancelar.Image = Archivo.imgBtnSalir32;
                        btnCancelar.Text  = Resources.Botones_General_Texto_Salir; 
                        _estadoSalir = true;
                        break;
                }
            }

            protected virtual bool NuevoItem()
            {
                return true;
            }
            protected virtual  bool CargarItem()
            {
                return true;
            }

            protected virtual bool EliminarItem()
            {
                return true;
            }

            protected virtual bool GuardarItem()
            {
                return true;
            }

            protected virtual bool Cancelar()
            {
                return true;
            }

        #endregion

        #region Eventos

        private void FrmAdministracionLoad(object sender, EventArgs e)
        {
            LimpiarTextBox();
            AdminMenu("");
            //añado imagenes a los menus
            btnNuevo.Image = Archivo.imgBtnAdd;
            btnActualizar.Image = Archivo.imgBtnRefresh;
            btnEliminar.Image = Archivo.imgBtnDelete;
            btnGuardar.Image = Archivo.imgBtnGuardar32;
            btnCancelar.Image = Archivo.imgBtnSalir32;
            _estadoSalir = true;
        }

        private void BtnNuevoClick(object sender, EventArgs e)
        {
            AdminMenu("nuevo");
            LimpiarTextBox();
            NuevoItem();
        }

        private void BtnActualizarClick(object sender, EventArgs e)
        {
            AdminMenu("actualizar");
            CargarItem();
        }

        private void BtnCerrarClick(object sender, EventArgs e)
        {
            if (_estadoSalir)
            {
                Close();
            }
            else
            {
                AdminMenu("");
                LimpiarTextBox();
                Cancelar();
            }
        }

        private void BtnGuardarClick(object sender, EventArgs e)
        {
            AdminMenu("guardar");
            GuardarItem();
            listaGridview.Refresh();
            LimpiarTextBox();
        }

        private void BtnEliminarClick(object sender, EventArgs e)
        {
            AdminMenu("eliminar");
            LimpiarTextBox();
            EliminarItem();
            listaGridview.Refresh();
        }
        #endregion
    }
}