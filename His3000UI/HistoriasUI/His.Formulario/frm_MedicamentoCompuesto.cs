using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Clases;
using His.Negocio;

namespace His.Formulario
{
    public partial class frm_MedicamentoCompuesto : Form
    {

        TextBox codMedicamento = new TextBox();
        TextBox cantidad = new TextBox();
        ATENCIONES atencion = new ATENCIONES();
        USUARIOS user = new USUARIOS();
        List<Int64> idReserva = new List<Int64>();
        List<RESERVA_KARDEX_MEDICAMENTO> listareservas = new List<RESERVA_KARDEX_MEDICAMENTO>();
        public frm_MedicamentoCompuesto(ATENCIONES _atencion, USUARIOS _user)
        {
            InitializeComponent();
            atencion = _atencion;
            user = _user;
            LlenaCombos();
        }

        public void LlenaCombos()
        {
            List<FRECUENCIA> frecuencia = new List<FRECUENCIA>();
            frecuencia = NegFormulariosHCU.RecuperarFrecuencias();
            cmbFrecuencia.DataSource = frecuencia;
            cmbFrecuencia.DisplayMember = "Detalle";
            cmbFrecuencia.ValueMember = "ID_FRECUENCIA";
            cmbFrecuencia.SelectedIndex = -1;

            DataTable via = new DataTable();
            via = NegFormulariosHCU.LlenaCombos("VIA");
            cmbVia.DataSource = via;
            cmbVia.DisplayMember = "Detalle";
            cmbVia.ValueMember = "ID_VIA";
            cmbVia.SelectedIndex = -1;

            List<LISTADO_COMPUESTOS> compuestos = new List<LISTADO_COMPUESTOS>();
            compuestos = NegFormulariosHCU.RecuperaListaCompuestso();
            cmb_listado_compuestos.DataSource = compuestos;
            cmb_listado_compuestos.DisplayMember = "detalle";
            cmb_listado_compuestos.ValueMember = "id_compuesto";
            cmb_listado_compuestos.SelectedIndex = -1;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (dtgCabeceraKardex.Rows.Count > 0)
            {
                if (MessageBox.Show("Si sale sin guardar va perder los datos. ¿Desea continuar?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void cmbFrecuencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            int combo = cmbFrecuencia.SelectedIndex + 1;
            List<FRECUENCIA_HORAS> obj = new List<FRECUENCIA_HORAS>();
            obj = NegFormulariosHCU.RecuperarFrecuenciasHoras(combo);
            cmb_frecuencia_hora.DataSource = obj;
            cmb_frecuencia_hora.DisplayMember = "detalle";
            cmb_frecuencia_hora.ValueMember = "id_hora_frecuencia";
            cmb_frecuencia_hora.SelectedIndex = -1;
        }

        private void btnMedicamento_Click(object sender, EventArgs e)
        {
            frm_AyudaKardex kardex = new frm_AyudaKardex(atencion.ATE_CODIGO.ToString(), 1, 0);
            kardex.ShowDialog();
            lblMedicamento.Text = kardex.medicamento;
            codMedicamento.Text = kardex.cue_codigo;
            txtCantidadManual.Text = kardex.cantidad;
            txt_dosis_turno.Text = kardex.cantidad;
            if (kardex.medicamento == "")
            {
                return;
            }
            if (Convert.ToDouble(kardex.cantidad) > 1)
            {
                txtDosisUnitaria.Focus();
            }
            else
            {
                txtCantidadManual.Enabled = true;
                txt_reserva.Text = "0";
            }

        }

        private void txtDosisUnitaria_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumberDecimal(e, false);
            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                if (txtDosisUnitaria.Text == "")
                {
                    MessageBox.Show("Se necesita que complete la cantidad total del medicamento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Enfocar otro cuadro de texto
                cmb_medidas.Focus();
                e.Handled = true; // Para evitar que se ingrese un salto de línea en el cuadro de texto actual
            }
        }

        private void cmb_medidas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                if (txtDosisUnitaria.Text == "")
                {
                    MessageBox.Show("Se necesita que complete la cantidad total del medicamento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Enfocar otro cuadro de texto
                txtCantidadManual.Focus();
                e.Handled = true; // Para evitar que se ingrese un salto de línea en el cuadro de texto actual
            }
        }

        private void txtCantidadManual_Leave(object sender, EventArgs e)
        {
            if (txtDosisUnitaria.Text != "")
            {
                if (txtCantidadManual.Text != "")
                {
                    double totalProducto = Convert.ToDouble(txtDosisUnitaria.Text);
                    double dosis = Convert.ToDouble(txtCantidadManual.Text);
                    double aplicar = Convert.ToDouble(txt_dosis_turno.Text);
                    if (totalProducto >= dosis)
                    {
                        if (txtCantidadManual.ReadOnly)
                        {
                            txt_dosis_registra.Text = txtDosisUnitaria.Text + " " + cmb_medidas.Text;
                        }
                        else
                        {
                            double registra = totalProducto / dosis;
                            txt_dosis_registra.Text = (Convert.ToInt32(registra)).ToString() + " " + cmb_medidas.Text;
                            double reserva = dosis - aplicar;
                            txt_reserva.Text = reserva.ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Division no puede proceder revise las cantidades.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDosisUnitaria.Text = totalProducto.ToString();
                        txtCantidadManual.Text = "";
                        txtCantidadManual.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Se necesita que complete la cantidad de la dosis del medicamento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void txtCantidadManual_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                if (txtDosisUnitaria.Text == "")
                {
                    MessageBox.Show("Se necesita que complete la cantidad del medicamento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Enfocar otro cuadro de texto
                txt_dosis_turno.Focus();
                e.Handled = true; // Para evitar que se ingrese un salto de línea en el cuadro de texto actual
            }
        }

        private void txt_dosis_turno_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumberDecimal(e, false);
            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                if (txtDosisUnitaria.Text == "")
                {
                    MessageBox.Show("Se necesita que complete la cantidad total del medicamento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Enfocar otro cuadro de texto
                btnCargar.Focus();
                e.Handled = true; // Para evitar que se ingrese un salto de línea en el cuadro de texto actual
            }
            if (txt_dosis_turno.Text != "" && txtCantidadManual.Text != "")
            {
                if (Convert.ToDouble(txt_dosis_turno.Text) > Convert.ToDouble(txtCantidadManual.Text))
                {
                    MessageBox.Show("La cantidad de dosis no puede ser mayor a las dosis divididas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_dosis_turno.Text = "";
                    txt_dosis_turno.Focus();
                }
            }
        }
        public bool Valida()
        {
            erroresPaciente.Clear();
            bool valido = true;
            if (cmb_medidas.SelectedIndex == -1)
            {
                erroresPaciente.SetError(cmb_medidas, "Campo Requerrido");
                valido = false;
            }
            if (cmbVia.SelectedIndex == -1)
            {
                erroresPaciente.SetError(cmbVia, "Campo Requerrido");
                valido = false;
            }
            if (cmbFrecuencia.SelectedIndex == -1)
            {
                erroresPaciente.SetError(cmbFrecuencia, "Campo Requerrido");
                valido = false;
            }
            if (cmb_frecuencia_hora.SelectedIndex == -1)
            {
                erroresPaciente.SetError(cmb_frecuencia_hora, "Campo Requerrido");
                valido = false;
            }
            if (cmb_listado_compuestos.SelectedIndex == -1)
            {
                erroresPaciente.SetError(cmb_listado_compuestos, "Campo Requerrido");
                valido = false;
            }
            if (txtDosisUnitaria.Text == "")
            {
                erroresPaciente.SetError(txtDosisUnitaria, "Campo Requerrido");
                valido = false;
            }
            if (txtCantidadManual.Text == "")
            {
                erroresPaciente.SetError(txtCantidadManual, "Campo Requerrido");
                valido = false;
            }
            if (txt_dosis_turno.Text == "")
            {
                erroresPaciente.SetError(txt_dosis_turno, "Campo Requerrido");
                valido = false;
            }
            if (lblMedicamento.Text == "")
            {
                erroresPaciente.SetError(lblMedicamento, "Campo Requerrido");
                valido = false;
            }


            return valido;
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (Valida())
            {
                cmbFrecuencia.Enabled = false;
                cmb_frecuencia_hora.Enabled = false;
                cmb_listado_compuestos.Enabled = false;
                cmbVia.Enabled = false;
                dtgCabeceraKardex.Rows.Add(codMedicamento.Text, lblMedicamento.Text, cmbVia.Text, txt_dosis_registra.Text, txtCantidadManual.Text);
                if (Convert.ToInt32(txt_reserva.Text) > 0)
                {
                    string[] dosisUni = txt_dosis_registra.Text.Split(' ');
                    RESERVA_KARDEX_MEDICAMENTO obj = new RESERVA_KARDEX_MEDICAMENTO();
                    obj.id_usuario = user.ID_USUARIO;
                    obj.ate_codigo = atencion.ATE_CODIGO;
                    obj.cue_codigo = Convert.ToInt64(codMedicamento.Text);
                    obj.presentacion = lblMedicamento.Text;
                    obj.frecuencia = Convert.ToInt32(cmbFrecuencia.SelectedIndex.ToString());
                    obj.via_administracion = Convert.ToInt32(cmbVia.SelectedIndex.ToString());
                    obj.dosis = Convert.ToDecimal(dosisUni[0]);
                    obj.medida = Convert.ToInt32(cmb_medidas.SelectedIndex.ToString());
                    obj.reserva = Convert.ToInt32(txt_reserva.Text);
                    obj.estado = true;
                    obj.fecha_registro = DateTime.Now;
                    listareservas.Add(obj);
                }
                lblMedicamento.Text = "";
                txt_dosis_registra.Text = "";
                txt_dosis_turno.Text = "";
                txtDosisUnitaria.Text = "";
                cmb_medidas.SelectedIndex = -1;
                txtCantidadManual.Text = "";
                txt_reserva.Text = "";
                codMedicamento.Text = "";

            }
        }

        private void txt_dosis_turno_Leave(object sender, EventArgs e)
        {
            if (txt_dosis_turno.Text != "" && txtCantidadManual.Text != "")
            {
                double dosis = Convert.ToDouble(txtCantidadManual.Text);
                double registra = Convert.ToDouble(txt_dosis_turno.Text);
                double reserva = dosis - registra;
                txt_reserva.Text = reserva.ToString();
            }
        }

        private void btn_reservas_Click(object sender, EventArgs e)
        {
            frm_AyudaKardex kardex = new frm_AyudaKardex(atencion.ATE_CODIGO, true);
            cmbFrecuencia.Enabled = false;
            cmbVia.Enabled = false;
            kardex.ShowDialog();
            if (kardex.medicamento != "")
            {
                lblMedicamento.Text = kardex.medicamento;
                codMedicamento.Text = kardex.cue_codigo;
                txtCantidadManual.Text = kardex.cantidad;
                txt_dosis_turno.Text = kardex.cantidad;
                cmb_medidas.SelectedIndex = kardex.medida;
                cmbVia.SelectedIndex = kardex.via;
                cmbFrecuencia.SelectedIndex = kardex.frecuencia;
                idReserva.Add(kardex.idReseerva);
                txt_dosis_registra.Text = (Convert.ToInt32(kardex.dosis)).ToString() + " " + cmb_medidas.Text;
                txtDosisUnitaria.Text = (Convert.ToInt32(kardex.dosis)).ToString();
                txt_reserva.Text = "0";
                btnMedicamento.Enabled = false;
                txt_dosis_turno.Enabled = true;
                txtDosisUnitaria.Enabled = false;
                txtCantidadManual.Enabled = false;
                cmb_medidas.Enabled = false;
                cmbVia.Enabled = false;
                cmbFrecuencia.Enabled = false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            List<MEDICAMENTO_COMPUESTO_DETALLE> med_detalle = new List<MEDICAMENTO_COMPUESTO_DETALLE>();
            MEDICAMENTO_COMPUESTO_ENCABEZADO med_encabezado = new MEDICAMENTO_COMPUESTO_ENCABEZADO();

            med_encabezado.ate_codigo = atencion.ATE_CODIGO;
            med_encabezado.id_frecuencia = cmbFrecuencia.SelectedIndex;
            med_encabezado.id_frecuencia_hora = cmb_frecuencia_hora.SelectedIndex;
            med_encabezado.id_lista_compuestos = cmb_listado_compuestos.SelectedIndex;
            med_encabezado.id_via_administracion = cmbVia.SelectedIndex;
            med_encabezado.fecha_registro = DateTime.Now;
            med_encabezado.estado = true;
            med_encabezado.id_usuario = user.ID_USUARIO;
            med_encabezado.v_infucion = txtInfusion.Text;
            med_encabezado.n_aplicacion = txtAplicacion.Text;
            Int64 idKardex = 0;
            if (NegFormulariosHCU.GuardaMedCompuestoEncabezado(med_encabezado))
            {

                MEDICAMENTO_COMPUESTO_ENCABEZADO medCod = NegFormulariosHCU.RecuperaMedCompuestoEncabezado(med_encabezado.ate_codigo);
                idKardex = medCod.id_med_compuesto;
                for (int i = 0; i < dtgCabeceraKardex.Rows.Count; i++)
                {
                    MEDICAMENTO_COMPUESTO_DETALLE medCompDet = new MEDICAMENTO_COMPUESTO_DETALLE();
                    medCompDet.id_med_compuesto = medCod.id_med_compuesto;
                    medCompDet.ate_codigo = atencion.ATE_CODIGO;
                    medCompDet.cue_codigo = Convert.ToInt64(dtgCabeceraKardex.Rows[i].Cells[0].Value.ToString());
                    medCompDet.cue_detalle = dtgCabeceraKardex.Rows[i].Cells[1].Value.ToString();
                    medCompDet.dosis = dtgCabeceraKardex.Rows[i].Cells[3].Value.ToString();
                    medCompDet.cantidad = dtgCabeceraKardex.Rows[i].Cells[4].Value.ToString();
                    med_detalle.Add(medCompDet);
                }
                if (NegFormulariosHCU.GuardaMedCompuesatoDetalle(med_detalle))
                {
                    MessageBox.Show("Medicamento Compuesto Guardado con exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Medicamento Compuesto no se guardo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //SE GUARDAN LAS RESERVAS EN UNA TABLA DIFERENTE PARA PODER RESCATARLAS

            foreach (var item in listareservas)
            {
                RESERVA_KARDEX_MEDICAMENTO obj = new RESERVA_KARDEX_MEDICAMENTO();
                obj.id_usuario = item.id_usuario;
                obj.ate_codigo = item.ate_codigo;
                obj.cue_codigo = item.cue_codigo;
                obj.presentacion = item.presentacion;
                obj.frecuencia = item.frecuencia;
                obj.via_administracion = item.via_administracion;
                obj.dosis = item.dosis;
                obj.medida = item.medida;
                obj.reserva = item.reserva;
                obj.estado = item.estado;
                obj.fecha_registro = item.fecha_registro;
                NegFormulariosHCU.GuardaReservas(obj);
            }

            foreach (var item in idReserva)
            {
                NegFormulariosHCU.ModificaReservas(item);
            }

            IngresaKardex objIngresa = new IngresaKardex();
            objIngresa.presentacion = cmb_listado_compuestos.Text;
            objIngresa.via = cmbVia.Text;
            objIngresa.frecuencia =cmbFrecuencia.Text;
            objIngresa.dosis = "(COMPUESTO)";

            objIngresa.hora = Convert.ToDateTime(cmb_frecuencia_hora.Text);
            objIngresa.fecha = (DateTime.Now);
            objIngresa.ate_codigo = atencion.ATE_NUMERO_ATENCION;
            objIngresa.eventual = false;
            objIngresa.id_kardex = idKardex;//este es el codigo cue_cuenta para poder verificar que medicamento ya fue puesto en kardex                
            objIngresa.medPropio = false;

            int hora = 0;
            DateTime fecha = DateTime.Now.Date;
            DateTime xfecha = DateTime.Now.Date;
            int cant = 1;

            for (int i = 0; i < cant; i++)
            {
                if (i == 0)
                {
                    string[] ho = cmb_frecuencia_hora.Text.Split(':');
                    hora = Convert.ToInt16(ho[0]);
                }
                else
                {
                    string frace = cmbFrecuencia.Text;
                    string[] spli = frace.Split(' ');
                    if (spli.Length > 1)
                    {
                        hora += Convert.ToInt16(spli[1]);
                    }
                    if (hora >= 24)
                    {
                        hora -= 24;
                        fecha = DateTime.Today.AddDays(1);
                        xfecha = xfecha.AddDays(1);

                    }
                }
                objIngresa.hora = Convert.ToDateTime(hora + ":00");
                objIngresa.fecha = xfecha;
                if (!NegFormulariosHCU.IngresaKardex(objIngresa, user.ID_USUARIO))
                {
                    MessageBox.Show("Kardex No Se Actualizo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }


        }
    }
}
