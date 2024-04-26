using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;
using His.Formulario;
using His.Entidades.Clases;

namespace His.Maintenance
{
    public partial class frm_CambioTipoAtencion : Form
    {
        public string original = "";

        ATENCIONES ultimaAtencion = new ATENCIONES();
        public frm_CambioTipoAtencion()
        {
            InitializeComponent();
        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            try
            {
                His.Admision.frm_AyudaPacientes form = new His.Admision.frm_AyudaPacientes();
                form.campoPadre = txt_historiaclinica;
                form.ShowDialog();
                form.Dispose();

                CargarPaciente(txt_historiaclinica.Text.Trim(), 0);


            }
            catch (System.Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargarPaciente(string historia, int aux)
        {
            PACIENTES pacienteActual = null;
            try
            {

                if (historia.Trim() != string.Empty)
                {
                    pacienteActual = NegPacientes.RecuperarPacienteID(historia);
                    txt_nombre1.Text = pacienteActual.PAC_NOMBRE1;
                    txt_nombre2.Text = pacienteActual.PAC_NOMBRE2;
                    txt_apellido1.Text = pacienteActual.PAC_APELLIDO_PATERNO;
                    txt_apellido2.Text = pacienteActual.PAC_APELLIDO_MATERNO;


                    CargarAtencionesPaciente(pacienteActual.PAC_CODIGO);
                }

            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (e.InnerException != null)
                    MessageBox.Show(e.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CargarAtencionesPaciente(int keyPaciente)
        {
            List<DtoAtenciones> dataAtenciones = NegAtenciones.RecuperarAtencionesPaciente(keyPaciente);

            if (dataAtenciones != null)
            {
                gridAtenciones.DataSource = dataAtenciones.Select(n => new
                {
                    CODIGO = n.ATE_CODIGO,
                    NUM_ATENCION = n.ATE_NUMERO_ATENCION,
                    FECHA_INGRESO = n.ATE_FECHA_INGRESO,
                    FECHA_ALTA = n.ATE_FECHA_ALTA,
                    NUM_CONTROL = n.ATE_NUMERO_CONTROL,
                    FACTURA = n.ATE_FACTURA_PACIENTE,
                    FECHA_FACTURA = n.ATE_FACTURA_FECHA,
                    REFERIDO = n.ATE_REFERIDO,
                    ESTADO = n.ATE_ESTADO,
                    HABITACION = n.HAB_NUMERO
                    ,
                    DIAGNOSTICO = n.ATE_DIAGNOSTICOINICIAL
                }).ToList();
            }
            else
            {
                gridAtenciones.DataSource = null;
            }

        }

        private void gridAtenciones_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            try
            {
                List<TIPO_INGRESO> tipoIngreso = new List<TIPO_INGRESO>();
                tipoIngreso = NegTipoIngreso.ListaTipoIngreso();
                cmb_tipoingreso.Enabled = true;
                cmb_tipoingreso.DataSource = tipoIngreso;
                cmb_tipoingreso.ValueMember = "TIP_CODIGO";
                cmb_tipoingreso.DisplayMember = "TIP_DESCRIPCION";
                cmb_tipoingreso.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmb_tipoingreso.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                string numatencion = gridAtenciones.ActiveRow.Cells["CODIGO"].Value.ToString();
                ultimaAtencion = NegAtenciones.RecuperarAtencionPorNumero(numatencion);
                cmb_tipoingreso.SelectedItem = tipoIngreso.FirstOrDefault(t => t.EntityKey == ultimaAtencion.TIPO_INGRESOReference.EntityKey);
                original = cmb_tipoingreso.Text;
            }
            catch (System.Exception r)
            {

                MessageBox.Show(r.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            if (txt_detalle.Text != "")
            {
                AUDITORIA_TIPO_INGRESO obj = new AUDITORIA_TIPO_INGRESO();
                obj.idUsuario = Sesion.codUsuario;
                obj.detalle = txt_detalle.Text;
                obj.fechaRegistro = DateTime.Now;
                obj.tipoIngresoOriginal = original;
                obj.tipoIngresoActual = cmb_tipoingreso.Text;
                obj.ate_codigo = ultimaAtencion.ATE_CODIGO;

                if(NegAtenciones.AutidoriaTipoIngreso(obj, Convert.ToInt16(cmb_tipoingreso.SelectedValue)))
                {
                    MessageBox.Show("Información actualizada con exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }

            }
            else
            {
                MessageBox.Show("Ingrese una descripción para cambiar el Tipo de Ingreso", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
