using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Negocio;
using His.Parametros;

namespace CuentaPaciente
{
    public partial class frmEliminarProductoCuenta : Form
    {
        private int codigoProducto;

        public frmEliminarProductoCuenta(int codigoProducto)
        {
            InitializeComponent();
            this.codigoProducto = codigoProducto;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(txtObservacion.Text.Trim()!= string.Empty)
            {
                CUENTAS_PACIENTES cuenta = NegCuentasPacientes.RecuperarCuentaId(codigoProducto);
                cuenta.CUE_ESTADO = 0;
                cuenta.CUE_OBSERVACION = txtObservacion.Text;
                NegCuentasPacientes.ModificarCuenta(cuenta);
                MessageBox.Show("Datos Almacenados Correctamenmte");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ingrese la Observación de Eliminación");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtObservacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void btnAceptar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void btnCancelar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void btnAceptar_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter))
            {
                if (txtObservacion.Text.Trim() != string.Empty)
                {
                    CUENTAS_PACIENTES cuenta = NegCuentasPacientes.RecuperarCuentaId(codigoProducto);
                    cuenta.CUE_ESTADO = 0;
                    cuenta.CUE_OBSERVACION = txtObservacion.Text;
                    NegCuentasPacientes.ModificarCuenta(cuenta);
                    MessageBox.Show("Datos Almacenados Correctamenmte");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ingrese la Observación de Eliminación");
                }
            }
        }
    }
}
