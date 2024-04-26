using His.Entidades;
using His.Formulario;
using His.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Dietetica
{
    public partial class ImagenInforme : Form
    {
        public bool estado = true;
        private int id_agendamiento;
        private int actualiza = 0;
        public ImagenInforme(int id_agenda = 0, int actualizacion = 0)
        {
            InitializeComponent();
            id_agendamiento = id_agenda;
            actualiza = actualizacion;
            CargaDatos();
        }

        private void CargaDatos()
        {
            if (actualiza == 0)
            {
                DataTable ag = NegImagen.getAgendamiento(id_agendamiento.ToString());

                if (ag.Rows.Count == 0)
                {
                    estado = false;
                    MessageBox.Show("No se puede realizar informe de esta atención por fecha de atención fuera de rango.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                txtPaciente.Text = ag.Rows[0]["Paciente"].ToString();
                txtCODRadiologo.Text = ag.Rows[0]["med_radiologo"].ToString();
                txtCODTecnologo.Text = ag.Rows[0]["med_tecnologo"].ToString();
                txtMedicoCOD.Text = ag.Rows[0]["MED_CODIGO"].ToString();

                cargarMedico(Convert.ToInt32(txtCODRadiologo.Text.ToString()), txtRadiologo, txtCODRadiologo);
                cargarMedico(Convert.ToInt32(txtCODTecnologo.Text.ToString()), txtTecnologo, txtCODTecnologo);
                cargarMedico(Convert.ToInt32(txtMedicoCOD.Text.ToString()), txtMedico, txtMedicoCOD);

                DateTime fex = DateTime.Now;
                if (Convert.ToDateTime((ag.Rows[0]["fecha_agendamiento"].ToString())) > DateTime.Now)
                {
                    fex = Convert.ToDateTime((ag.Rows[0]["fecha_agendamiento"].ToString()));
                }
                dtpEntrega.Value = fex;
                dtpInforme.Value = fex;

                DataTable est = NegImagen.getAgendamientoEstudios(id_agendamiento.ToString());
                foreach (DataRow row in est.Rows)
                {
                    gridEstudios.Rows.Add(row["CUE_CODIGO"].ToString(), row["PRO_DESCRIPCION"].ToString());
                }


                DataTable aest = NegDietetica.getDataTable("getPlacasImagen", id_agendamiento.ToString());

                foreach (DataRow row in aest.Rows)
                {
                    txt30x40.Text = row["30X40"].ToString();
                    txt8x10.Text = row["8x10"].ToString();
                    txt14x14.Text = row["14x14"].ToString();
                    txt14x17.Text = row["14x17"].ToString();
                    txt18x24.Text = row["18x24"].ToString();
                    txtPOdonto.Text = row["ODONT"].ToString();
                    txtPDanada.Text = row["DANADAS"].ToString();
                    txtPEnviadas.Text = row["ENVIADAS"].ToString();
                    if (row["MEDIO_CONTRASTE"].ToString() != "0")
                    {
                        chkMedioContraste.Checked = true;
                    }
                    if (row["CD"].ToString() == "True")
                    {
                        ckbCD.Checked = true;
                    }
                    if (row["DVD"].ToString() == "True")
                    {
                        ckbDVD.Checked = true;
                    }
                }
            }
            else
            {
                DataTable DatosInforme = NegImagen.CargarImagen(id_agendamiento);

                DataTable DatosDx = NegImagen.CargarImagenDx(id_agendamiento);

                DataTable aest = NegDietetica.getDataTable("getPlacasImagen", id_agendamiento.ToString());

                foreach (DataRow row in aest.Rows)
                {
                    txt30x40.Text = row["30X40"].ToString();
                    txt8x10.Text = row["8x10"].ToString();
                    txt14x14.Text = row["14x14"].ToString();
                    txt14x17.Text = row["14x17"].ToString();
                    txt18x24.Text = row["18x24"].ToString();
                    txtPOdonto.Text = row["ODONT"].ToString();
                    txtPDanada.Text = row["DANADAS"].ToString();
                    txtPEnviadas.Text = row["ENVIADAS"].ToString();
                    if (row["MEDIO_CONTRASTE"].ToString() != "0")
                    {
                        chkMedioContraste.Checked = true;
                    }
                    if (row["CD"].ToString() == "True")
                    {
                        ckbCD.Checked = true;
                    }
                    if (row["DVD"].ToString() == "True")
                    {
                        ckbDVD.Checked = true;
                    }
                }

                if (DatosInforme.Rows.Count > 0)
                {
                    if (DatosInforme.Rows[0]["prioridad"].ToString() == "1")
                    {
                        rbControl.Checked = true;
                    }
                    else if (DatosInforme.Rows[0]["informe"].ToString() == "2")
                    {
                        rbNormal.Checked = true;
                    }
                    else
                        rdUrgente.Checked = true;
                    txt_diagnosticoMedico.Text = DatosInforme.Rows[0]["informe"].ToString();
                    txtRecomendaciones.Text = DatosInforme.Rows[0]["recomendaciones"].ToString();
                    txtConclusiones.Text = DatosInforme.Rows[0]["conclusiones"].ToString();
                    txt_dbv.Text = DatosInforme.Rows[0]["DB_V"].ToString();
                    txt_dbeg.Text = DatosInforme.Rows[0]["DB_EG"].ToString();
                    txt_dbp.Text = DatosInforme.Rows[0]["DB_P"].ToString();
                    txt_lfv.Text = DatosInforme.Rows[0]["LF_V"].ToString();
                    txt_lfeg.Text = DatosInforme.Rows[0]["LF_EG"].ToString();
                    txt_lfp.Text = DatosInforme.Rows[0]["LF_P"].ToString();
                    txt_pav.Text = DatosInforme.Rows[0]["PA_V"].ToString();
                    txt_paeg.Text = DatosInforme.Rows[0]["PA_EG"].ToString();
                    txt_pap.Text = DatosInforme.Rows[0]["PA_P"].ToString();
                    if (DatosInforme.Rows[0]["MASCULINO"].ToString() == "1")
                        chkMasculino.Checked = true;
                    else
                        chkMasculino.Checked = false;

                    if (DatosInforme.Rows[0]["FEMENINO"].ToString() == "1")
                        chkFemenino.Checked = true;
                    else
                        chkFemenino.Checked = false;

                    if (DatosInforme.Rows[0]["MULTIPLE"].ToString() == "1")
                        chkMultiple.Checked = true;
                    else
                        chkMultiple.Checked = false;

                    if (DatosInforme.Rows[0]["PLACENTA_F"].ToString() == "1")
                        chkFundica.Checked = true;
                    else
                        chkFundica.Checked = false;

                    if (DatosInforme.Rows[0]["PLACENTA_M"].ToString() == "1")
                        chkMarginal.Checked = true;
                    else
                        chkMarginal.Checked = false;

                    if (DatosInforme.Rows[0]["PLACENTA_P"].ToString() == "1")
                        chkPrevia.Checked = true;
                    else
                        chkPrevia.Checked = false;



                    txt_gradomadurez.Text = DatosInforme.Rows[0]["GRADO_MADUREZ"].ToString();

                    if (DatosInforme.Rows[0]["ANTEVERSION"].ToString() == "1")
                        chkAnteversion.Checked = true;
                    else
                        chkAnteversion.Checked = false;

                    if (DatosInforme.Rows[0]["RETROVERSION"].ToString() == "1")
                        chkRetroversion.Checked = true;
                    else
                        chkRetroversion.Checked = false;

                    if (DatosInforme.Rows[0]["DIU"].ToString() == "1")
                        chkDiu.Checked = true;
                    else
                        chkDiu.Checked = false;

                    if (DatosInforme.Rows[0]["FIBROMA"].ToString() == "1")
                        chkFibroma.Checked = true;
                    else
                        chkFibroma.Checked = false;

                    if (DatosInforme.Rows[0]["MIOMA"].ToString() == "1")
                        chkMioma.Checked = true;
                    else
                        chkMioma.Checked = false;

                    if (DatosInforme.Rows[0]["AUSENTE"].ToString() == "1")
                        chkAusente.Checked = true;
                    else
                        chkAusente.Checked = false;

                    if (DatosInforme.Rows[0]["AUSENTE"].ToString() == "1")
                        chkAusente.Checked = true;
                    else
                        chkAusente.Checked = false;

                    if (DatosInforme.Rows[0]["HIDROSALPIX"].ToString() == "1")
                        chkHidrosalpix.Checked = true;
                    else
                        chkHidrosalpix.Checked = false;

                    if (DatosInforme.Rows[0]["VACIA"].ToString() == "1")
                        chkVacia.Checked = true;
                    else
                        chkVacia.Checked = false;

                    if (DatosInforme.Rows[0]["QUISTE"].ToString() == "1")
                        chkQuiste.Checked = true;
                    else
                        chkQuiste.Checked = false;

                    if (DatosInforme.Rows[0]["OCUPADA"].ToString() == "1")
                        chkOcupada.Checked = true;
                    else
                        chkOcupada.Checked = false;

                    txt_sacoDouglas.Text = DatosInforme.Rows[0]["SACO_DOUGLAS"].ToString();

                    foreach (DataRow item in DatosDx.Rows)
                    {
                        var aux = "DEFINITIVO";
                        if (item["DEFINITIVO"].ToString() == "False")
                        {
                            aux = "PRESUNTIVO";
                        }
                        gridDiagnosticos.Rows.Add(item["CIE_CODIGO"].ToString(), item["CIE_DESCRIPCION"].ToString(), aux);
                    }

                    DataTable ag = NegImagen.getAgendamiento(id_agendamiento.ToString());



                    txtPaciente.Text = ag.Rows[0]["Paciente"].ToString();
                    txtCODRadiologo.Text = ag.Rows[0]["med_radiologo"].ToString();
                    txtCODTecnologo.Text = ag.Rows[0]["med_tecnologo"].ToString();
                    txtMedicoCOD.Text = ag.Rows[0]["MED_CODIGO"].ToString();

                    cargarMedico(Convert.ToInt32(txtCODRadiologo.Text.ToString()), txtRadiologo, txtCODRadiologo);
                    cargarMedico(Convert.ToInt32(txtCODTecnologo.Text.ToString()), txtTecnologo, txtCODTecnologo);
                    cargarMedico(Convert.ToInt32(txtMedicoCOD.Text.ToString()), txtMedico, txtMedicoCOD);

                    DateTime fex = DateTime.Now;
                    if (Convert.ToDateTime((ag.Rows[0]["fecha_agendamiento"].ToString())) > DateTime.Now)
                    {
                        fex = Convert.ToDateTime((ag.Rows[0]["fecha_agendamiento"].ToString()));
                    }
                    dtpEntrega.Value = fex;
                    dtpInforme.Value = fex;

                    DataTable est = NegImagen.getAgendamientoEstudios(id_agendamiento.ToString());
                    foreach (DataRow row in est.Rows)
                    {
                        gridEstudios.Rows.Add(row["CUE_CODIGO"].ToString(), row["PRO_DESCRIPCION"].ToString());
                    }

                }
            }
        }

        private void cargarMedico(int codMedico, TextBox txtNombre, TextBox txtCOD)
        {

            MEDICOS medico = NegMedicos.MedicoID(codMedico);
            if (medico != null)
            {
                DataTable med = NegMedicos.MedicoIDValida(codMedico);
                if (med.Rows[0][0].ToString() == "7")
                {
                    MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                txtNombre.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                txtCOD.Text = Convert.ToString(medico.MED_CODIGO);
            }

        }
        private void RecuperarUsuario(int id_usuario, TextBox txtNombre, TextBox txtCOD)
        {
            USUARIOS usu = NegUsuarios.RecuperarUsuarioID(id_usuario);
            if (usu != null)
            {
                txtNombre.Text = usu.APELLIDOS + " " + usu.NOMBRES;
                txtCOD.Text = Convert.ToString(usu.ID_USUARIO);
            }
        }

        private void ultraGroupBox8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddDiagnotico_Click(object sender, EventArgs e)
        {
            Formulario.frm_BusquedaCIE10 cieDiez = new His.Formulario.frm_BusquedaCIE10();
            cieDiez.ShowDialog();
            string xProducto = cieDiez.resultado;
            string xCodigo = cieDiez.codigo;
            if (xCodigo != string.Empty)
            {
                if (xCodigo != null)
                {


                    if (!BuscarItem(xCodigo, gridDiagnosticos))
                        this.gridDiagnosticos.Rows.Add(xCodigo, xProducto, "PRESUNTIVO");
                    else
                        MessageBox.Show("El item ya fue ingresado.");
                }
            }

            //frm_ImagenAyuda ayuda = new frm_ImagenAyuda("DIAGNOSTICOS");
            //ayuda.ShowDialog();
            //if (ayuda.codigo != string.Empty)
            //{
            //    if (!BuscarItem(ayuda.codigo, gridDiagnosticos))
            //        this.gridDiagnosticos.Rows.Add(ayuda.codigo, ayuda.producto, "PRESUNTIVO");
            //    else
            //        MessageBox.Show("El item ya fue ingresado.");
            //}
        }
        private bool BuscarItem(string searchValue, DataGridView grid)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(searchValue))
                {
                    return true;
                }
            }
            return false;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DateTime xFecInforme = dtpInforme.Value;
            DateTime xFecEntrega = dtpEntrega.Value;

            if (xFecEntrega < xFecInforme)
            {
                MessageBox.Show("La fecha de entrega no puede ser menor a la fecha del informe.");
                return;
            }
            if (txtMedicoCOD.Text.Trim() != string.Empty)
            {
                if (actualiza == 0)
                {
                    if (gridDiagnosticos.RowCount > 0)
                    {
                        NegImagen.saveInformeAgendamiento(empaquetar(), empaquetarDx(), id_agendamiento, Convert.ToInt16(txtCODRadiologo.Text));
                        MessageBox.Show("Informe guardado con exito. :)", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                        MessageBox.Show("Por favor ingrese al menos un diagnostico.");
                }
                else
                {
                    if (gridDiagnosticos.RowCount > 0)
                    {
                        NegImagen.guardarCambios(empaquetar(), empaquetarDx(), id_agendamiento);
                        MessageBox.Show("Informe actualizado con exito. :)", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //actualiza radiologo de imagen
                        NegImagen.actualiceRadiologo(id_agendamiento, Convert.ToInt32(txtCODRadiologo.Text));
                        this.Close();
                    }
                    else
                        MessageBox.Show("Por favor ingrese al menos un diagnostico.");
                }
            }
            else
            {
                MessageBox.Show("Por favor ingrese el medico solicitante.");
            }


        }

        private List<PedidoImagen_diagnostico> empaquetarDx()
        {
            List<PedidoImagen_diagnostico> ListDiagnosticos = new List<PedidoImagen_diagnostico>();
            foreach (DataGridViewRow row in gridDiagnosticos.Rows)
            {
                PedidoImagen_diagnostico estudio = new PedidoImagen_diagnostico();
                estudio.CIE_CODIGO = Convert.ToString(row.Cells["CIE10"].Value);

                ListDiagnosticos.Add(estudio);
            }
            return ListDiagnosticos;
        }

        private string[] empaquetar()
        {
            string prioridad = "1";
            if (rdUrgente.Checked)
                prioridad = "3";
            else if (rbControl.Checked)
                prioridad = "1";
            else if (rbNormal.Checked)
                prioridad = "2";

            string[] x = new string[] {
                id_agendamiento.ToString()
           ,txtMedicoCOD.Text
           ,prioridad
           ,dtpInforme.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
           ,dtpEntrega.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
           ,txt_diagnosticoMedico.Text
           ,txt_dbv.Text
           ,txt_lfv.Text
           ,txt_pav.Text
           ,txt_dbeg.Text
           ,txt_lfeg.Text
           ,txt_paeg.Text
           ,txt_dbp.Text
           ,txt_lfp.Text
           ,txt_pap.Text
           , (chkFundica.Checked==true?"1":"0")
           , (chkMarginal.Checked==true?"1":"0")
           , (chkPrevia.Checked==true?"1":"0")
           , (chkMasculino.Checked==true?"1":"0")
           , (chkFemenino.Checked==true?"1":"0")
           , (chkMultiple.Checked==true?"1":"0")
           , txt_gradomadurez.Text
           , (chkAnteversion.Checked==true?"1":"0")
           , (chkRetroversion.Checked==true?"1":"0")
           , (chkDiu.Checked==true?"1":"0")
           , (chkFibroma.Checked==true?"1":"0")
           , (chkMioma.Checked==true?"1":"0")
           , (chkAusente.Checked==true?"1":"0")
           , (chkHidrosalpix.Checked==true?"1":"0")
           , (chkQuiste.Checked==true?"1":"0")
           , (chkVacia.Checked==true?"1":"0")
           , (chkOcupada.Checked==true?"1":"0")
           , txt_sacoDouglas.Text
           , txtRecomendaciones.Text
           , (txtPEnviadas.Text.Trim()==string.Empty?"0":txtPEnviadas.Text.Trim())
           ,(txt30x40.Text.Trim()==string.Empty?"0":txt30x40.Text.Trim())
           ,(txt8x10.Text.Trim() == string.Empty ? "0" : txt8x10.Text.Trim())
           ,(txt14x14.Text.Trim() == string.Empty ? "0" : txt14x14.Text.Trim())
            ,(txt14x17.Text.Trim() == string.Empty ? "0" : txt14x17.Text.Trim())
          ,(txt18x24.Text.Trim() == string.Empty ? "0" : txt18x24.Text.Trim())
           ,(txtPOdonto.Text.Trim() == string.Empty ? "0" : txtPOdonto.Text.Trim())
           ,(txtPDanada.Text.Trim() == string.Empty ? "0" : txtPDanada.Text.Trim())
           ,(chkMedioContraste.Checked==true?"1":"0")
           ,txtConclusiones.Text
        };
            return x;
        }

        private void btnAddMedico_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
                cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), txtMedico, txtMedicoCOD);
        }

        private void txtPaciente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void rdUrgente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void rbNormal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void rbControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void dtpInforme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void dtpEntrega_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txtMedico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txt_dbv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txt_dbeg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txt_dbp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txt_lfv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txt_lfeg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txt_lfp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txt_pav_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txt_paeg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txt_pap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void chkFundica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void chkMarginal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void chkPrevia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void chkMasculino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void chkFemenino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void chkMultiple_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txt_gradomadurez_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void chkAnteversion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void chkRetroversion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void btnRadiologo_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();

            His.Admision.frm_Ayudas ayuda = new His.Admision.frm_Ayudas(listaMedicos, "MEDICOS", "CODIGO", "");
            ayuda.campoPadre = txtCODRadiologo;
            ayuda.ShowDialog();

            if (ayuda.campoPadre.Text != string.Empty)
                CargarMedicoAyuda(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));

            ayuda.Dispose();
        }

        private void CargarMedicoAyuda(int codMedico)
        {
            MEDICOS medico = NegMedicos.MedicoID(codMedico);
            DataTable med = NegMedicos.MedicoIDValida(codMedico);
            if (med.Rows[0][0].ToString() == "7")
            {
                MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCODRadiologo.Text = "";
                txtRadiologo.Text = "";
                return;
            }
            if (medico != null)
                txtRadiologo.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + "  " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
            else
                txtRadiologo.Text = string.Empty;
        }
    }


}
