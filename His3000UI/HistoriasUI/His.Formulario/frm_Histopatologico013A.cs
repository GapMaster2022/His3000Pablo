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
using System.Threading;

namespace His.Formulario
{
    public partial class frm_Histopatologico013A : Form
    {
        public Int64 ateCodigo = 0;
        ATENCIONES ate = new ATENCIONES();
        PACIENTES pac = new PACIENTES();
        USUARIOS usr = new USUARIOS();
        int _id_usuario = His.Entidades.Clases.Sesion.codUsuario;
        string diagnostico = string.Empty;
        string codigoCIE = string.Empty;
        int index2 = 0;

        public frm_Histopatologico013A(Int64 _ateCodigo)
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
                gb_material.Enabled = false;
                gb_anticonceptivo.Enabled = false;
                gb_edades.Enabled = false;
                gb_fechas.Enabled = false;
                gb_paridad.Enabled = false;
                chb_terapia_hormonal.Enabled = false;
                txt_otro_anticonceptivo.Enabled = false;
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                HC_HISTOPATOLOGICO histo = new HC_HISTOPATOLOGICO();

                histo.ate_codigo = ate.ATE_CODIGO;
                histo.histopatologia = chb_Histopatologia.Checked;
                histo.citologia = chb_citologia.Checked;
                histo.descripcion = chb_revision.Checked;
                histo.estudio = txt_estudio.Text;
                histo.resumen_clinico = txt_cuadro.Text;
                histo.muestra_pieza = txt_muestra.Text;
                histo.tratamiento = txt_tratamiento.Text;
                histo.endocervix = chb_endocervix.Checked;
                histo.exocervix = ckb_exocervix.Checked;
                histo.pared_vaginal = chb_pared_vaginal.Checked;
                histo.union_escamo = chb_union_escamo.Checked;
                histo.munion_cervical = chb_munion_cervical.Checked;
                histo.otro = chb_otro_material.Checked;
                histo.detalle_otro_material = txt_otro_material.Text;
                if (pac.PAC_GENERO == "F")
                {
                    histo.oral_inyectable = chb_oral_inyectable.Checked;
                    histo.diu = chb_diu.Checked;
                    histo.ligadura = chb_ligadura.Checked;
                    histo.detalle_otro_anticoncepcion = txt_otro_anticonceptivo.Text;
                    histo.terapia_hormonal = chb_terapia_hormonal.Checked;
                    if (txt_menarquia.Text != "")
                        histo.menarquia = Convert.ToInt32(txt_menarquia.Text);
                    if (txt_menopausia.Text != "")
                        histo.menopausia = Convert.ToInt32(txt_menopausia.Text);
                    if (txt_inicio_relaciones.Text != "")
                        histo.inicio_relaciones = Convert.ToInt32(txt_inicio_relaciones.Text);
                    if (txt_gestacion.Text != "")
                        histo.gestaciones = Convert.ToInt32(txt_gestacion.Text);
                    if (txt_partos.Text != "")
                        histo.partos = Convert.ToInt32(txt_partos.Text);
                    if (txt_abortos.Text != "")
                        histo.abortos = Convert.ToInt32(txt_abortos.Text);
                    if (txt_cesareas.Text != "")
                        histo.cesareas = Convert.ToInt32(txt_cesareas.Text);
                    if ((MessageBox.Show("Desea guardar fechas de Menstruacion, Parto y Citologia como registro valido del paciente", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)) == DialogResult.Yes)
                    {
                        histo.ultima_mestruacion = dt_menstruacion.Value;
                        histo.ultimo_parto = dt_parto.Value;
                        histo.ultima_citologia = dt_citologia.Value;
                    }
                }
                histo.medico = Convert.ToInt32(usr.ID_USUARIO);

                List<HC_HISTOPATOLOGICO_DIAGNOSTICOS> lista = new List<HC_HISTOPATOLOGICO_DIAGNOSTICOS>();

                foreach (DataGridViewRow item in dtg_4.Rows)
                {
                    HC_HISTOPATOLOGICO_DIAGNOSTICOS detalle = new HC_HISTOPATOLOGICO_DIAGNOSTICOS();

                    if (item.Cells[0].Value != null)
                    {
                        detalle.codigo = item.Cells[1].Value.ToString();
                        detalle.presuntivo = Convert.ToBoolean(item.Cells[2].Value.ToString());
                        if (item.Cells[3].Value == null)
                        {
                            detalle.definitivo = false;
                        }
                        else
                        {
                            detalle.definitivo = Convert.ToBoolean(item.Cells[3].Value.ToString());
                        }
                        lista.Add(detalle);
                    }
                }

                if (NegFormulariosHCU.CrearHistopatologico(histo, lista))
                {
                    MessageBox.Show("Histopatologico registrado con exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("No se guardo, existe falla en la coneccion con la base de datos, revise y vuelva a intentar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddMedico_Click(object sender, EventArgs e)
        {

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
            //gr1.Enabled = paneles;
            //gr2.Enabled = paneles;
            //gr3.Enabled = paneles;
            dtg_4.Rows.Clear();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            HabilitarBotones(false, true, false, false, true, true);
        }

        private bool validar()
        {
            if (txt_estudio.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese el estudio que va necesitar el paciente.");
                return false;
            }
            if (txt_cuadro.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese el resumen.");
                return false;
            }
            if (txt_muestra.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor detalle la muestra tomada.");
                return false;
            }
            if (txt_tratamiento.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese el tatamiento que esta realizando el paciente");
                return false;
            }
            if (txtMedico.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese el nombre del profesional de la salud responsable.");
                return false;
            }
            if (dtg_4.Rows.Count == 0)
            {
                MessageBox.Show("Por favor ingrese por lo menos un diagnostico del paciente");
                return false;
            }
            if (!chb_Histopatologia.Checked && !chb_citologia.Checked && !chb_revision.Checked)
            {
                MessageBox.Show("Por favor seleccione una o varias opciones de Histopatologia, Citologia, Descripcion");
                return false;
            }
            //if(pac.PAC_GENERO == "M")
            //{
            //    return true;
            //}
            //else
            //{
            //    if (!chb_oral_inyectable.Checked && !chb_diu.Checked && !chb_ligadura.Checked && !chb_otro_anticonceptivo.Checked)
            //    {
            //        MessageBox.Show("Por favor seleccione una o varias opciones del grupo de ANTICONSEPTIVOS");
            //        return false;
            //    }
            //    if (chb_otro_anticonceptivo.Checked && txt_otro_anticonceptivo.Text.Trim() == "")
            //    {
            //        MessageBox.Show("Por favor debe ingresar el detalle de OTROS en el grupo de ANTICONSEPTIVOS");
            //        return false;
            //    }
            //    if(txt_)
            //}

            return true;
        }

        private void BuscaCIEDTG4()
        {
            frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
            busqueda.ShowDialog();
            diagnostico = busqueda.resultado;
            codigoCIE = busqueda.codigo;

            if ((diagnostico != "") && (diagnostico != null))
            {
                if (dtg_4.Rows.Count < 7)
                {
                    if (dtg_4.Rows.Count > 1)
                    {
                        for (int i = 0; i < dtg_4.Rows.Count - 1; i++)
                        {
                            if (busqueda.codigo == dtg_4.Rows[i].Cells[1].Value.ToString())
                            {
                                MessageBox.Show("El procedimiento ya ha sido agregado.\r\nIntente con uno diferente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }
                    DataGridViewTextBoxCell txtcell = (DataGridViewTextBoxCell)this.dtg_4.CurrentRow.Cells[0];
                    DataGridViewTextBoxCell txtcell2 = (DataGridViewTextBoxCell)this.dtg_4.CurrentRow.Cells[1];
                    if (diagnostico != null)
                    {
                        txtcell.Value = diagnostico;
                        txtcell2.Value = codigoCIE;
                        diagnostico = "";
                    }

                    index2++;
                }
                else
                    MessageBox.Show("No puede agregar mas de 6 procedimientos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnMedico_Click(object sender, EventArgs e)
        {
            BuscaCIEDTG4();
        }


        private void dtg_4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                BuscaCIEDTG4();
            }
        }

        private void gridSol_DoubleClick(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Desea cargar el Histopatologico?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                limpiarCampos();
                Int64 hin_codigo = Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["ID"].Value.ToString());
                CargaHistopatologico(hin_codigo);
                //ValidarCerrado();
                ////controles_prepararedicion();
                ////cargarPedido(Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["id"].Value));
                ////controles_visualizaPedido();
                //NegAtenciones atenciones = new NegAtenciones();
                //string estado = atenciones.EstadoCuenta(Convert.ToString(ateCodigo));
                //if (estado != "1")
                //{
                //    Bloquear();
                //}
                //ValidarEnfermeria();
                //do something
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
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
        private void CargaHistopatologico(Int64 id)
        {
            HC_HISTOPATOLOGICO histo = new HC_HISTOPATOLOGICO();
            histo = NegFormulariosHCU.RecuperaHistopatologico(id);

            chb_Histopatologia.Checked = histo.histopatologia;
            chb_citologia.Checked = histo.citologia;
            chb_revision.Checked = histo.descripcion;
            txt_estudio.Text = histo.estudio;
            txt_cuadro.Text = histo.resumen_clinico;
            txt_muestra.Text = histo.muestra_pieza;
            txt_tratamiento.Text = histo.tratamiento;
            chb_endocervix.Checked = histo.endocervix;
            ckb_exocervix.Checked = histo.exocervix;
            chb_pared_vaginal.Checked = histo.pared_vaginal;
            chb_union_escamo.Checked = histo.union_escamo;
            chb_munion_cervical.Checked = histo.munion_cervical;
            chb_otro_material.Checked = histo.otro;
            txt_otro_material.Text = histo.detalle_otro_material;
            chb_oral_inyectable.Checked = histo.oral_inyectable;
            chb_diu.Checked = histo.diu;
            chb_ligadura.Checked = histo.ligadura;
            txt_otro_anticonceptivo.Text = histo.detalle_otro_anticoncepcion;
            chb_terapia_hormonal.Checked = histo.terapia_hormonal;
            if (histo.menarquia != null)
                txt_menarquia.Text = histo.menarquia.ToString();
            if (histo.menopausia != null)
                txt_menopausia.Text = histo.menopausia.ToString();
            if (histo.inicio_relaciones != null)
                txt_inicio_relaciones.Text = histo.inicio_relaciones.ToString();
            if (histo.gestaciones != null)
                txt_gestacion.Text = histo.gestaciones.ToString();
            if (histo.partos != null)
                txt_partos.Text = histo.partos.ToString();
            if (histo.abortos != null)
                txt_abortos.Text = histo.abortos.ToString();
            if (histo.cesareas != null)
                txt_cesareas.Text = histo.cesareas.ToString();
            if (histo.ultima_mestruacion != null)
                dt_menstruacion.Value = histo.ultima_mestruacion.Value;
            if (histo.ultimo_parto != null)
                dt_parto.Value = histo.ultimo_parto.Value;
            if (histo.ultima_citologia != null)
                dt_citologia.Value = histo.ultima_citologia.Value;

            List<HC_HISTOPATOLOGICO_DIAGNOSTICOS> diagnosticos4 = NegInterconsulta.recuperarHistopatologicoDiagnosticos(histo.id);
            if (diagnosticos4 != null)
            {
                foreach (HC_HISTOPATOLOGICO_DIAGNOSTICOS diag in diagnosticos4)
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell txtcell2 = new DataGridViewTextBoxCell();
                    CIE10 cie = new CIE10();
                    cie = NegFormulariosHCU.RecuperaCieCodigo(diag.codigo);
                    txtcell.Value = cie.CIE_DESCRIPCION;
                    txtcell2.Value = diag.codigo;
                    fila.Cells.Add(txtcell);
                    fila.Cells.Add(txtcell2);
                    DataGridViewCheckBoxCell c1 = new DataGridViewCheckBoxCell();
                    DataGridViewCheckBoxCell c2 = new DataGridViewCheckBoxCell();
                    c1.Value = diag.presuntivo;
                    c2.Value = diag.definitivo;
                    fila.Cells.Add(c1);
                    fila.Cells.Add(c2);
                    dtg_4.Rows.Add(fila);
                    index2++;
                }
            }
        }

        private void txt_menarquia_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_menopausia_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_inicio_relaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_gestacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_partos_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_abortos_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_cesareas_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void chb_otro_anticonceptivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_otro_anticonceptivo.Checked)
            {
                txt_otro_anticonceptivo.Enabled = true;
            }
            else
            {
                txt_otro_anticonceptivo.Enabled = false;
            }
        }

        private void chb_otro_material_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_otro_material.Checked)
            {
                txt_otro_material.Enabled = true;
            }
            else
            {
                txt_otro_material.Enabled = false;
                txt_otro_material.Text = "";
            }
        }

        private void limpiarCampos()
        {
            chb_Histopatologia.Checked = false;
            chb_citologia.Checked = false;
            chb_revision.Checked = false;
            txt_estudio.Text = "";
            txt_cuadro.Text = "";
            txt_muestra.Text = "";
            txt_tratamiento.Text = "";
            chb_endocervix.Checked = false;
            ckb_exocervix.Checked = false;
            chb_pared_vaginal.Checked = false;
            chb_union_escamo.Checked = false;
            chb_munion_cervical.Checked = false;
            chb_otro_material.Checked = false;
            txt_otro_material.Text = "";
            chb_oral_inyectable.Checked = false;
            chb_diu.Checked = false;
            chb_ligadura.Checked = false;
            txt_otro_anticonceptivo.Text = "";
            chb_terapia_hormonal.Checked = false;
            txt_menarquia.Text = "";
            txt_menopausia.Text = "";
            txt_inicio_relaciones.Text = "";
            txt_gestacion.Text = "";
            txt_partos.Text = "";
            txt_abortos.Text = "";
            txt_cesareas.Text = "";
            dt_menstruacion.Value = DateTime.Now;
            dt_parto.Value = DateTime.Now;
            dt_citologia.Value = DateTime.Now;
            dtg_4.Rows.Clear();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
    }

}
