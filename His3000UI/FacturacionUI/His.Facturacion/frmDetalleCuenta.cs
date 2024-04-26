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
using His.Entidades.Clases;
using Recursos;
using System.Data.SqlClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;



namespace His.Facturacion
{
    public partial class frmDetalleCuenta : Form
    {
        #region Variables
        public bool pacienteNuevo = false;
        PACIENTES pacienteCuenta = new PACIENTES();
        List<PEDIDOS> listaPedido = new List<PEDIDOS>();
        List<PEDIDOS_AREAS> listaPedidoAreas = new List<PEDIDOS_AREAS>();
        public string codigoAtencionA;
        #endregion       

        public frmDetalleCuenta()
        {
            InitializeComponent();
        }

        #region Métodos 

        private void cargarDatosPaciente(PACIENTES pacienteActual)
        {
            txt_ApellidoH1.Text = pacienteActual.PAC_APELLIDO_PATERNO;
            txt_ApellidoH2.Text = pacienteActual.PAC_APELLIDO_MATERNO;
            txt_NombreH1.Text = pacienteActual.PAC_NOMBRE1;
            txt_NombreH2.Text = pacienteActual.PAC_NOMBRE2;

            //txt_PacienteN.Text = pacienteActual.PAC_NOMBRE1 + " " + pacienteActual.PAC_NOMBRE2 + " " + pacienteActual.PAC_APELLIDO_PATERNO + " " + pacienteActual.PAC_APELLIDO_MATERNO;
            //txt_Direccion_P.Text = pacienteActual.PAC_REFERENTE_DIRECCION;
            //txt_Telef_P.Text = pacienteActual.PAC_REFERENTE_TELEFONO;
            //txt_Historia_P.Text = pacienteActual.PAC_HISTORIA_CLINICA;
            //txt_Habitacion_P.Text = "302";
            //txt_Dias_P.Text = "3";
            //ultimaAtencion = NegAtenciones.RecuperarUltimaAtencion(pacienteActual.PAC_CODIGO);
            //MessageBox.Show(ultimaAtencion.ATE_FACTURA_NOMBRE);
            ////cbx_FacturaNombre.SelectedItem = ultimaAtencion.ATE_FACTURA_NOMBRE;
            //if (cbx_FacturaNombre.SelectedIndex > -1)
            //{
            //    if (ultimaAtencion.ATE_FACTURA_NOMBRE == "PACIENTE")
            //    {
            //        txt_Ruc.Text = pacienteActual.PAC_IDENTIFICACION;
            //        txt_Direc_Cliente.Text = pacienteActual.PAC_REFERENTE_DIRECCION;
            //        txt_Cliente.Text = pacienteActual.PAC_NOMBRE1 + " " + pacienteActual.PAC_NOMBRE2 + " " + pacienteActual.PAC_APELLIDO_PATERNO + " " + pacienteActual.PAC_APELLIDO_MATERNO;
            //        txt_telef_Cliente.Text = pacienteActual.PAC_REFERENTE_TELEFONO;
            //        ////cmb_tipopago.SelectedItem = tipoPago.FirstOrDefault(f => f.TIF_CODIGO == ultimaAtencion.TIF_CODIGO);                    
            //    }
            //    else
            //    {
            //        if (ultimaAtencion.ATE_FACTURA_NOMBRE == "ACOMPAÑANTE")
            //        {
            //            txt_Ruc.Text = ultimaAtencion.ATE_ACOMPANANTE_CEDULA;
            //            txt_Direc_Cliente.Text = ultimaAtencion.ATE_ACOMPANANTE_DIRECCION;
            //            txt_Cliente.Text = ultimaAtencion.ATE_ACOMPANANTE_NOMBRE;
            //            txt_telef_Cliente.Text = ultimaAtencion.ATE_ACOMPANANTE_TELEFONO; ;

            //        }
            //        else
            //        {
            //            if (ultimaAtencion.ATE_FACTURA_NOMBRE == "GARANTE")
            //            {

            //            }
            //            else
            //            {
            //                if (ultimaAtencion.ATE_FACTURA_NOMBRE == "EMPRESA")
            //                {

            //                }
            //                else
            //                {

            //                }
            //            }
            //        }
            //    }
            //}
            //txt_Ruc.Text = pacienteActual.PAC_IDENTIFICACION;
            //txt_Direc_Cliente.Text = pacienteActual.PAC_REFERENTE_DIRECCION;
            //txt_Cliente.Text = pacienteActual.PAC_NOMBRE1 + " " + pacienteActual.PAC_NOMBRE2 + " " + pacienteActual.PAC_APELLIDO_PATERNO + " " + pacienteActual.PAC_APELLIDO_MATERNO;
            //txt_telef_Cliente.Text = pacienteActual.PAC_REFERENTE_TELEFONO;            
        }

        public void cargarPaciente(int codigoAtencion)
        {
            try
            {
                pacienteCuenta = NegPacientes.recuperarPacientePorAtencion(codigoAtencion);
                cargarDatosPaciente(pacienteCuenta);
                listaPedidoAreas = NegDetalleCuenta.recuperarPedidosAreas();

                listaPedido = NegDetalleCuenta.recuperarPedidos(codigoAtencion);
                ulgdbListadoCIE.DataSource = listaPedido;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region Eventos
        private void btnBuscarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                if (pacienteNuevo == false)
                {
                    frmAyudaPaciente form = new frmAyudaPaciente();
                    form.campoPadre = txt_Historia_Pc;
                    form.ShowDialog();
                    //if (txt_Historia_Pc.Text != null)
                    //{
                    //    pacienteCuenta = NegPacientes.RecuperarPacienteID(txt_Historia_Pc.Text.Trim());
                    //    cargarDatosPaciente(pacienteCuenta);
                    //    //cargarFormasPagos();
                    //}
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_Historia_Pc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //string historia = txt_historiaclinica.Text.ToString();               
                if (pacienteNuevo == false)
                {
                    if (Convert.ToBoolean(txt_Historia_Pc.Tag) == true)
                    {
                        cargarPaciente(190225);
                        txt_Historia_Pc.Tag = false;
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        #endregion

        
    }   


}
