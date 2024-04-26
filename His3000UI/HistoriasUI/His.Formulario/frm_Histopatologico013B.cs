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

namespace His.Formulario
{
    public partial class frm_Histopatologico013B : Form
    {
        public Int64 ateCodigo = 0;
        ATENCIONES ate = new ATENCIONES();
        PACIENTES pac = new PACIENTES();
        USUARIOS usr = new USUARIOS();
        int _id_usuario = His.Entidades.Clases.Sesion.codUsuario;
        string diagnostico = string.Empty;
        string codigoCIE = string.Empty;
        public frm_Histopatologico013B(Int64 _ateCodigo)
        {
            InitializeComponent();
            ateCodigo = _ateCodigo;
            ate = NegAtenciones.RecuperarAtencionID(ateCodigo);
            pac = NegPacientes.RecuperarPacienteID(ate.PACIENTES.PAC_CODIGO);
            usr = NegUsuarios.RecuperarUsuarioID(_id_usuario);
            MEDICOS medico = new MEDICOS();
            medico = NegMedicos.recuperarMedico(ate.MEDICOS.MED_CODIGO);
            lblSeguro.Text = NegAtenciones.RecuperaAseguradoraAtencion(Convert.ToInt64(ate.ATE_CODIGO));
            txtMedico.Text = usr.APELLIDOS + " " + usr.NOMBRES;
            lblPaciente.Text = pac.PAC_APELLIDO_PATERNO + ' ' + pac.PAC_APELLIDO_MATERNO + ' ' + pac.PAC_NOMBRE1 + ' ' + pac.PAC_NOMBRE2;
            lblHc.Text = pac.PAC_HISTORIA_CLINICA;
            lblmedico.Text = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
            lblatencion.Text = ate.ATE_NUMERO_ATENCION;
            DateTime birthDate = (DateTime)pac.PAC_FECHA_NACIMIENTO;
            int age = (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.25D);
            lbledad.Text = age.ToString();
            lblsexo.Text = "Femenino";
            refrescarSolicitudes();
            if (pac.PAC_GENERO == "M")
            {
                lblsexo.Text = "Masculino";
            }
        }

        private void refrescarSolicitudes()
        {
            gridSol.DataSource = NegImagen.getHistopatologico(ate.ATE_CODIGO);
            gridSol.Columns["ID"].Visible = false;
            gridSol.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            if (gridSol.RowCount > 0)
                btnImprimir.Enabled = true;
            else
                btnImprimir.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                HC_HISTOPATOLOGICO_B histo = new HC_HISTOPATOLOGICO_B();
                histo.ate_codigo = ate.ATE_CODIGO;
                histo.descripcion_macroscopica = txt_macros.Text;
                histo.descripcion_microscopica = txt_micros.Text;
                histo.numero_pieza = txt_numero_pieza.Text;
                histo.numero_informe = txt_numero_informe.Text;
                histo.recomendaciones = txtRecomendaciones.Text;
                histo.usuario = _id_usuario;
                histo.medico = Convert.ToInt32(txt_codigo_medico.Text);
                if (chb_Histopatologia.Checked)
                {
                    histo.histopatologia = true;
                }
                else
                {
                    histo.histopatologia = false;
                }
                if (chb_citologia.Checked)
                {
                    histo.citologia = true;
                }
                else
                {
                    histo.citologia = false;
                }

                List<HC_HISTOPATOLOGIA_B_DETALLE> lista = new List<HC_HISTOPATOLOGIA_B_DETALLE>();
                foreach (DataGridViewRow item in dgvCie.Rows)
                {
                    HC_HISTOPATOLOGIA_B_DETALLE detalle = new HC_HISTOPATOLOGIA_B_DETALLE();

                    if (item.Cells[0].Value != null)
                    {
                        detalle.codigo = item.Cells[1].Value.ToString();

                        lista.Add(detalle);
                    }
                }

                if (NegFormulariosHCU.CrearHistopatologicoB(histo, lista))
                {
                    MessageBox.Show("Informacion registrada con exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al querer registrar la informacion, valide la misma y vuelva a intenar, caso contrario comuniquese con soporte.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool validar()
        {
            if (txt_macros.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese la descripcion macroscopica.");
                return false;
            }
            if (txt_micros.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese la descripcion microscopica.");
                return false;
            }
            if (txtMedico.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese el nombre del profesional de la salud responsable.");
                return false;
            }
            if (txtRecomendaciones.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese las recomendaciones.");
                return false;
            }
            if (dgvCie.Rows.Count == 0)
            {
                MessageBox.Show("Por favor ingrese por lo menos un diagnostico del paciente");
                return false;
            }
            if (!chb_Histopatologia.Checked && !chb_citologia.Checked)
            {
                MessageBox.Show("Por favor seleccione una o varias opciones de Histopatologia y Citologia");
                return false;
            }

            return true;
        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            HabilitarBotones(false, true, false, false, true, true);
        }
        public void HabilitarBotones(bool nuevo, bool guardar, bool modificar, bool imprimir, bool cancelar, bool paneles)
        {
            btnnuevo.Enabled = nuevo;
            btnModificar.Enabled = modificar;
            btnGuardar.Enabled = guardar;
            btnCancelar.Enabled = cancelar;
            btnImprimir.Enabled = imprimir;
            gr1.Enabled = paneles;
            gr2.Enabled = paneles;
            dgvCie.Rows.Clear();
        }

        private void dgvCie_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvCie.CurrentCell.ColumnIndex == 0)
            {
                if (e.KeyCode == Keys.F1)
                {
                    frm_BusquedaCIE10 x = new frm_BusquedaCIE10();
                    x.ShowDialog();

                    if (x.codigo != string.Empty)
                    {
                        if (!BuscarItem(x.codigo, dgvCie))
                        {
                            this.dgvCie.Rows.Add(x.codigo, x.resultado);
                        }
                        else
                            MessageBox.Show("El item ya fue ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                if (dgvCie.Rows.Count > 1)
                {
                    for (int i = 0; i < dgvCie.Rows.Count - 1; i++)
                    {
                        if (dgvCie.Rows[i].Cells[0].Value == null && dgvCie.Rows[i].Cells[1].Value == null)
                            dgvCie.Rows.RemoveAt(dgvCie.Rows[i].Index);
                    }
                }
            }
        }
        private bool BuscarItem(string searchValue, DataGridView grid)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    if (row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        MessageBox.Show("El diagnostico ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                }
            }
            return false;
        }

        MEDICOS medico = null;
        private void btnAddMedico_Click(object sender, EventArgs e)
        {
            MaskedTextBox codMedico;
            List<MEDICOS> medicos = NegMedicos.listaMedicos();
            frm_Ayudas frm = new frm_Ayudas(medicos);
            frm.bandCampo = true;
            frm.ShowDialog();
            if (frm.campoPadre2.Text != string.Empty)
            {
                codMedico = (frm.campoPadre2);
                medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(codMedico.Text));
                agregarMedico(medico);
            }
        }
        private void agregarMedico(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                txtMedico.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                txt_codigo_medico.Text = medicoTratante.MED_CODIGO.ToString();
                //if (medico.MED_RUC.Length > 10)
                //{
                //    txt_CodMSPE.Text = medico.MED_RUC.Substring(0, 10);
                //}
                //else
                //    txt_CodMSPE.Text = medico.MED_RUC;

            }

        }
    }
}
