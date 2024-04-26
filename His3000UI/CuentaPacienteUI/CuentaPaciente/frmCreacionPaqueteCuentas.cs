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
using Infragistics.Win.UltraWinGrid;
using Recursos;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Core.Datos;
using His.Parametros;
using System.IO;
using System.Diagnostics;

namespace CuentaPaciente
{
    public partial class frmCreacionPaqueteCuentas : Form
    {
        int CodigoSecuencial = 0;
        public List<Int32> _ListaAtenciones = new List<Int32>();
        string _tipo = "";
        Int32 CodigoPaquete = 0;
        Int64 SecuencialRadicacion = 0;

        DataTable dtDatosPaquete = new DataTable();

        public frmCreacionPaqueteCuentas(List<Int32> ListaAtenciones,string Tipo)
        {

            _ListaAtenciones = ListaAtenciones;
            _tipo = Tipo;

            InitializeComponent();
        }

        private void frmCreacionPaqueteCuentas_Load(object sender, EventArgs e)
        {
            if (_tipo == "NUEVO")
            {                

                SecuencialRadicacion = NegCuentasPacientes.RecuperarCodigoPaqueteRadicacion();

                this.lblUsuario.Text = His.Entidades.Clases.Sesion.nomUsuario;
                dtpFechaControl.Enabled = true;
                txtNumeroRadicacion.Enabled = true;
                txtNumeroRadicacion.Text = SecuencialRadicacion.ToString();
                txtNumeroTramite.Enabled = true;
                txtObservacion.Enabled = true;
            }
            if (_tipo == "MODIFICA")
            { 
                dtpFechaControl.Enabled=false;
                //txtNumeroRadicacion.Enabled = false;
                txtNumeroTramite.Enabled = false;
                txtObservacion.Enabled = false;
                btnGuardar.Enabled = false;
            }
            if (_tipo == "MODIFICA_NUMERO_TRAMITE")
            {
                dtpFechaControl.Enabled = false;
                txtNumeroRadicacion.Enabled = true;
                txtNumeroTramite.Enabled = true;
                txtObservacion.Enabled = false;
                btnGuardar.Enabled = false;
            }

            if (_tipo == "ELIMINAR")
            {
                dtpFechaControl.Enabled = false;
                txtNumeroRadicacion.Enabled = true;
                txtNumeroTramite.Enabled = false;
                txtObservacion.Enabled = false;
                btnGuardar.Enabled = false;
                btnGuardar.Text = "ELIMINAR";
            }
            if (_tipo == "ELIMINARP")
            {
                dtpFechaControl.Enabled = false;
                txtNumeroRadicacion.Enabled = true;
                txtNumeroTramite.Enabled = false;
                txtObservacion.Enabled = false;
                btnGuardar.Enabled = false;
                btnGuardar.Text = "ELIMINAR";
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (_tipo == "NUEVO")
            {
                CodigoSecuencial = NegCuentasPacientes.RecuperarCodigoPaquete(); // Secuencial Paquetes
                SecuencialRadicacion = NegCuentasPacientes.RecuperarCodigoPaqueteRadicacion(); // Numeros de Radicacion

                NegCuentasPacientes.CrearPaqueteCuentas(CodigoSecuencial, dtpFechaControl.Value.ToShortDateString(), Convert.ToInt64(SecuencialRadicacion), Convert.ToInt64(txtNumeroTramite.Text), txtObservacion.Text, _ListaAtenciones, His.Entidades.Clases.Sesion.codUsuario);

                MessageBox.Show("El paquete a sido generado correctamente.");
                this.Dispose();
            }

            if (_tipo == "MODIFICA")
            {
                NegCuentasPacientes.CrearPaqueteCuentas(CodigoPaquete, dtpFechaControl.Value.ToShortDateString(), Convert.ToInt64(txtNumeroRadicacion.Text), Convert.ToInt64(txtNumeroTramite.Text), txtObservacion.Text, _ListaAtenciones, His.Entidades.Clases.Sesion.codUsuario);

                MessageBox.Show("El paquete se a modificado.");
                this.Dispose();
            }

            if (_tipo == "MODIFICA_NUMERO_TRAMITE")
            {
                NegCuentasPacientes.ActualizarPaquete(Convert.ToInt64(this.txtNumeroRadicacion.Text), Convert.ToInt64(this.txtNumeroTramite.Text), His.Entidades.Clases.Sesion.codUsuario);

                MessageBox.Show("El paquete se a modificado.");
                this.Dispose();
            }
            if (_tipo == "ELIMINAR")
            {
                DialogResult reply = MessageBox.Show("Realmente Desea Eliminar el Registro ?", "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (reply == DialogResult.No)
                {
                    return;
                }
                NegCuentasPacientes.EliminarCuentasPaquete(Convert.ToInt64(CodigoPaquete), _ListaAtenciones, His.Entidades.Clases.Sesion.codUsuario);

                MessageBox.Show("Los datos se han eliminado correctamente.");
                this.Dispose();

            }
            if (_tipo == "ELIMINARP")
            {
                DialogResult reply = MessageBox.Show("Realmente Desea Eliminar el Paquete ?", "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (reply == DialogResult.No)
                {
                    return;
                }
                NegCuentasPacientes.EliminarPaquete(Convert.ToInt64(CodigoPaquete), His.Entidades.Clases.Sesion.codUsuario);

                MessageBox.Show("El paquete se a eliminado.");
                this.Dispose();
            }
            
        }

        private void txtNumeroRadicacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNumeroRadicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (_tipo != "NUEVO")
                {
                    dtDatosPaquete=NegCuentasPacientes.RecuperarPaquete(Convert.ToInt64(txtNumeroRadicacion.Text));

                    if (dtDatosPaquete.Rows.Count > 0)
                    {

                        this.dtpFechaControl.Value = Convert.ToDateTime( dtDatosPaquete.Rows[0]["FechaControlPaquete"]);
                        //this.txtNumeroRadicacion.Text = Convert.ToString(dtDatosPaquete.Rows[0]["FechaControlPaquete"]);
                        this.txtNumeroTramite.Text = Convert.ToString(dtDatosPaquete.Rows[0]["NumeroTramitePaquete"]);
                        this.txtObservacion.Text = Convert.ToString(dtDatosPaquete.Rows[0]["Observacion"]);
                        this.lblUsuario.Text = Convert.ToString(dtDatosPaquete.Rows[0]["USR"]);

                        CodigoPaquete = Convert.ToInt32(dtDatosPaquete.Rows[0]["PaqueteId"]);
                        btnGuardar.Enabled = true;

                    }
                    else
                    {
                        MessageBox.Show("El numero de tramite ingresado no existe");
                        btnGuardar.Enabled = true;
                    }

                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
