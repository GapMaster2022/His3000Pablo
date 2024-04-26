using His.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using GeneralApp.ControlesWinForms;
using Recursos;

namespace His.Formulario
{
    public partial class frm_InterconsultaB : Form
    {
        int codigoMedico = 0;
        public int pac_codigo = 0;
        public Int64 ate_codigo = 0;
        public Int32 hin_codigo = 0;
        int index3 = 0;
        string diagnostico = string.Empty;
        string codigoCIE = string.Empty;
        string diagnostico2 = string.Empty;
        string codigoCIE2 = string.Empty;
        string modo = "SAVE";
        MEDICOS medico = null;
        ATENCIONES atencion = null;
        PACIENTES paciente = null;
        ASEGURADORAS_EMPRESAS aseguradora = null;
        HC_INTERCONSULTA interconsulta = null;
        HC_INTERCONSULTA inter = null;
        List<HC_INTERCONSULTA_DIAGNOSTICO> diagnosticos4 = null;
        List<HC_INTERCONSULTA_DIAGNOSTICO> diagnosticos8 = null;
        public bool editar = false;
        public frm_InterconsultaB(Int64 Ate_codigo)
        {
            InitializeComponent();
            habilitaGrid(false);
            ate_codigo = Ate_codigo;
            if (Ate_codigo != 0)
                cargarAtencion(Ate_codigo);
            habilitarBotones(false, false, false);
            refrescarSolicitudes();
            cargarHora();
            NegAtenciones atenciones = new NegAtenciones();
            string estado = atenciones.EstadoCuenta(Convert.ToString(Ate_codigo));
            List<PERFILES> perfilUsuario = new NegPerfil().RecuperarPerfil(His.Entidades.Clases.Sesion.codUsuario);

        }
        private void cargarAtencion(Int64 codAtencion)
        {
            try
            {
                atencion = NegAtenciones.RecuperarAtencionID(codAtencion);
                HABITACIONES hab = new HABITACIONES();
                hab.hab_Codigo = atencion.HABITACIONES.hab_Codigo;
                cargarPaciente(atencion.PACIENTES.PAC_CODIGO);
                lblatencion.Text = atencion.ATE_NUMERO_ATENCION;
                lblHabitacion.Text = atencion.HABITACIONES.hab_Numero;
                pac_codigo = atencion.PACIENTES.PAC_CODIGO;
                aseguradora = NegAseguradoras.recuperaAseguradoraPorAtencion(codAtencion);
                //lbl_aseguradora.Text = aseguradora.ASE_NOMBRE;
                List<MEDICOS> medicos = NegMedicos.listaMedicos();
                int codigoMedico = medicos.FirstOrDefault(m => m.EntityKey == atencion.MEDICOSReference.EntityKey).MED_CODIGO;
                if (codigoMedico != 0)
                    cargarMedico(codigoMedico);

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar los datos de la atencion, error: " + ex.Message, "error");
            }

        }
        private void habilitarBotones(bool guardar, bool imprimir, bool cierre)
        {
            //btnGuardar.Image = Archivo.imgBtnGoneSave48;
            //btnImprimir.Image = Archivo.imgBtnGonePrint48;
            //btnSalir.Image = Archivo.imgBtnGoneExit48;
            //pictureBox1.Image = Archivo.F1;
            btnGuardar.Enabled = guardar;
            btnImprimir.Enabled = imprimir;
            btnCierre.Enabled = cierre;
            btnSalir.Image = Archivo.imgBtnGoneExit48;
            pictureBox1.Image = Archivo.F1;
        }
        private void habilitaGrid(bool grp)
        {
            gb6.Enabled = grp;
            gb7.Enabled = grp;
            gb8.Enabled = grp;
            gb9.Enabled = grp;
            gb10.Enabled = grp;
        }
        private void cargarPaciente(int codPac)
        {
            DateTime actual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime nacido = DateTime.Now.Date;
            paciente = NegPacientes.RecuperarPacienteID(codPac);
            if (paciente != null)
            {
                lblPaciente.Text = paciente.PAC_APELLIDO_PATERNO + " " +
                                     paciente.PAC_APELLIDO_MATERNO + " " +
                                     paciente.PAC_NOMBRE1 + " " +
                                     paciente.PAC_NOMBRE2;
                lblHc.Text = paciente.PAC_HISTORIA_CLINICA+ "-"+ atencion.ATE_NUMERO_ATENCION;
                lblsexo.Text = paciente.PAC_GENERO;
            }
            else
            {
                lblHc.Text = string.Empty;
                lblPaciente.Text = string.Empty;
                lblsexo.Text = string.Empty;
            }
            nacido = (DateTime)paciente.PAC_FECHA_NACIMIENTO;
            int edadAnos = actual.Year - nacido.Year;
            if (actual.Month < nacido.Month || (actual.Month == nacido.Month && actual.Day < nacido.Day))
                edadAnos--;
            lbledad.Text = Convert.ToString(edadAnos);

        }
        private void cargarMedico(int cod)
        {
            medico = NegMedicos.RecuperaMedicoId(cod);
            lblmedico.Text = medico.MED_APELLIDO_PATERNO.Trim() + " " +
                medico.MED_APELLIDO_MATERNO.Trim() + " " +
                medico.MED_NOMBRE1.Trim() + " " + medico.MED_NOMBRE2.Trim();
        }
        private void refrescarSolicitudes()
        {
            gridSol.DataSource = NegImagen.getInterconsultas(ate_codigo);
            gridSol.Columns["ID"].Visible = false;
            gridSol.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void gridSol_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            limpiarCampos();
            hin_codigo = Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["ID"].Value.ToString());
            inter = NegInterconsulta.recuperarInterconsultaID(hin_codigo);
            if (inter.HIN_ESTADO == true)
            {
                habilitaGrid(true);
                medico = NegMedicos.RecuperaMedicoId((int)inter.HIN_MEDICO_INTERCONSULTADO);
                txtInterconsultado.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                if (inter.HIN_FECHARESPUESTA == null)
                {
                    DialogResult dialogResult = MessageBox.Show("¿Desea Responder la Interconsulta?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        habilitarBotones(true, false, false);
                        editar = false;
                    }
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("¿Desea editar la Interconsulta?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        habilitarBotones(true, true, true);
                        editar = true;
                        cargaInformacion();
                    }
                }
            }
            else if (inter.HIN_ESTADO == null)
            {
                DialogResult dialogResult = MessageBox.Show("La interconsulta no ha sido cerrada por lo que no se puede responder", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                habilitarBotones(false, true, false);
                habilitaGrid(false);
                cargaInformacion();
            }

            NegAtenciones atenciones = new NegAtenciones();
            string estado = atenciones.EstadoCuenta(Convert.ToString(ate_codigo));
            if (estado != "1")
            {

            }

        }
        public void cargaInformacion()
        {
            txt_ccInterconsulta.Text = inter.HIN_CUADRO_INTERCONSULTA;
            txt_plan_diagnostico.Text = inter.HIN_PLAN_DIAGNOSTICO;
            txt_plan_tratamiento.Text = inter.HIN_PLAN_TRATAMIENTO;
            txt_resumen.Text = inter.HIN_RESUMEN_CRITERIO;
            List<HC_INTERCONSULTA_DIAGNOSTICO> diagnosticos8 = NegInterconsulta.recuperarDiagnosticosIntercEgre(inter.HIN_CODIGO);
            if (diagnosticos8 != null)
            {
                foreach (HC_INTERCONSULTA_DIAGNOSTICO diag in diagnosticos8)
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell txtcell2 = new DataGridViewTextBoxCell();
                    txtcell.Value = diag.HID_DIAGNOSTICO;
                    txtcell2.Value = diag.CIE_CODIGO;
                    fila.Cells.Add(txtcell);
                    fila.Cells.Add(txtcell2);
                    DataGridViewCheckBoxCell c1 = new DataGridViewCheckBoxCell();
                    DataGridViewCheckBoxCell c2 = new DataGridViewCheckBoxCell();
                    if (diag.HID_ESTADO.Value)
                    {
                        c1.Value = true;
                        c2.Value = false;
                    }
                    else
                    {
                        c1.Value = false;
                        c2.Value = true;
                    }
                    fila.Cells.Add(c1);
                    fila.Cells.Add(c2);
                    dtg_8.Rows.Add(fila);
                    index3++;
                }
            }
        }
        public void limpiarCampos()
        {
            txt_ccInterconsulta.Text = "";
            txt_resumen.Text = "";
            txt_plan_diagnostico.Text = "";
            txt_plan_tratamiento.Text = "";
            dtg_8.Rows.Clear();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validarFormulario())
                {

                    interconsulta = new HC_INTERCONSULTA();
                    interconsulta.HIN_CODIGO = hin_codigo;
                    interconsulta.HIN_CUADRO_INTERCONSULTA = txt_ccInterconsulta.Text;
                    interconsulta.HIN_RESUMEN_CRITERIO = txt_resumen.Text;
                    interconsulta.HIN_PLAN_DIAGNOSTICO = txt_plan_diagnostico.Text;
                    interconsulta.HIN_PLAN_TRATAMIENTO = txt_plan_tratamiento.Text;
                    interconsulta.HIN_FECHARESPUESTA = DateTime.Now;
                    if (NegInterconsulta.completaInterconsulta(interconsulta))
                    {
                        if (editar)
                            NegInterconsulta.BorraDiagnosticosIntercIng(interconsulta.HIN_CODIGO, "E");
                        guardarDiagnosticos();
                        MessageBox.Show("Datos almacenados correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Imprimir();
                        limpiarCampos();

                        habilitarBotones(false, false, false); //Deshabilito todos los botones

                        habilitaGrid(false); 
                    }
                    else
                        MessageBox.Show("Los datos no se pudieron almacenar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Datos Incompletos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }
        private void guardarDiagnosticos()
        {
            foreach (DataGridViewRow fila in dtg_8.Rows)
            {
                if (fila.Cells[1].RowIndex < dtg_8.Rows.Count - 1)
                {
                    HC_INTERCONSULTA_DIAGNOSTICO detalle = new HC_INTERCONSULTA_DIAGNOSTICO();
                    if (fila.Cells[1].Value != null)
                        detalle.CIE_CODIGO = fila.Cells[1].Value.ToString();
                    else
                        detalle.CIE_CODIGO = "";

                    if (fila.Cells[2].Value != null)
                        if ((bool)fila.Cells[2].Value)
                            detalle.HID_ESTADO = true;
                        else
                            detalle.HID_ESTADO = false;
                    else
                        detalle.HID_ESTADO = false;


                    detalle.HID_DIAGNOSTICO = fila.Cells[0].Value.ToString();
                    detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    detalle.HC_INTERCONSULTAReference.EntityKey = inter.EntityKey;
                    detalle.HID_TIPO = "E";
                    detalle.HID_CODIGO = NegInterconsulta.ultimoCodigoDiagnostico() + 1;
                    NegInterconsulta.crearInterconsultaDiagnosticos(detalle);
                }
            }
        }
        public void Imprimir()
        {
            try
            {
                atencion = NegAtenciones.RecuperarAtencionID(ate_codigo);
                paciente = NegPacientes.RecuperarPacienteID(atencion.PACIENTES.PAC_CODIGO);
                medico = NegMedicos.RecuperaMedicoId((int)inter.HIN_MEDICO_INTERCONSULTADO);
                frmReportes x = new frmReportes(1, "InterconsultaB", DSInterconsulta());
                x.Show();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void envioCorreo()
        {
            try
            {
                atencion = NegAtenciones.RecuperarAtencionID(ate_codigo);
                paciente = NegPacientes.RecuperarPacienteID(atencion.PACIENTES.PAC_CODIGO);
                medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(inter.HIN_MEDICO_CODIGO));
                DataTable IO = NegDietetica.getDataTable("EMPRESA");
                frmReportes x = new frmReportes(1, "InterconsultaBCorreo", medico, DSInterconsulta());
                x.Show();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public DSInterconsulta DSInterconsulta()
        {
            DataTable IO = NegDietetica.getDataTable("EMPRESA");
            DSInterconsulta ds = new DSInterconsulta();
            DataRow dr;
            dr = ds.Tables["InterconsultaB"].NewRow();


            dr["institucion"] = NegUtilitarios.RutaLogo("General");
            dr["establecimiento"] = His.Entidades.Clases.Sesion.nomEmpresa;
            dr["path"] = IO.Rows[0]["EMP_PATHIMAGEN"].ToString();
            dr["nombre1"] = paciente.PAC_NOMBRE1;
            dr["nombre2"] = paciente.PAC_NOMBRE2;
            dr["apellido1"] = paciente.PAC_APELLIDO_PATERNO;
            dr["apellido2"] = paciente.PAC_APELLIDO_MATERNO;
            dr["cedula"] = paciente.PAC_IDENTIFICACION;
            dr["ea"] = "X";
            dr["sexo"] = lblsexo.Text;
            dr["edad"] = lbledad.Text;
            dr["hc"] = lblHc.Text;
            dr["cuadroclinico"] = txt_ccInterconsulta.Text;
            dr["diagnostico"] = txt_plan_diagnostico.Text;
            dr["tratamiento"] = txt_plan_tratamiento.Text;
            dr["resumen"] = txt_resumen.Text;
            dtg_8.Refresh();
            for (int i = 0; i < dtg_8.Rows.Count - 1; i++)
            {
                if (i == 0)
                {
                    dr["d1c"] = dtg_8.Rows[0].Cells[1].Value.ToString();
                    dr["d1"] = (dtg_8.Rows[0].Cells[0].Value.ToString()).Replace("'", "´");
                    if (!(bool)dtg_8.Rows[i].Cells[3].Value)
                        dr["d1p"] = dtg_8.Rows[0].Cells[2].Value != null ? "X" : " ";
                    else
                        dr["d1d"] = dtg_8.Rows[0].Cells[3].Value != null ? "X" : " ";
                }
                if (i == 1)
                {
                    dr["d2c"] = dtg_8.Rows[1].Cells[1].Value.ToString();
                    dr["d2"] = (dtg_8.Rows[1].Cells[0].Value.ToString()).Replace("'", "´");
                    if (!(bool)dtg_8.Rows[i].Cells[3].Value)
                        dr["d2p"] = dtg_8.Rows[1].Cells[2].Value != null ? "X" : " ";
                    else
                        dr["d2d"] = dtg_8.Rows[1].Cells[3].Value != null ? "X" : " ";
                }
                if (i == 2)
                {
                    dr["d3c"] = dtg_8.Rows[2].Cells[1].Value.ToString();
                    dr["d3"] = (dtg_8.Rows[2].Cells[0].Value.ToString()).Replace("'", "´");
                    if (!(bool)dtg_8.Rows[i].Cells[3].Value)
                        dr["d3p"] = dtg_8.Rows[2].Cells[2].Value != null ? "X" : " ";
                    else
                        dr["d3d"] = dtg_8.Rows[2].Cells[3].Value != null ? "X" : " ";
                }
                if (i == 3)
                {
                    dr["d4c"] = dtg_8.Rows[3].Cells[1].Value.ToString();
                    dr["d4"] = (dtg_8.Rows[3].Cells[0].Value.ToString()).Replace("'", "´");
                    if (!(bool)dtg_8.Rows[i].Cells[3].Value)
                        dr["d4p"] = dtg_8.Rows[3].Cells[2].Value != null ? "X" : " ";
                    else
                        dr["d4d"] = dtg_8.Rows[3].Cells[3].Value != null ? "X" : " ";
                }
                if (i == 4)
                {
                    dr["d5c"] = dtg_8.Rows[4].Cells[1].Value.ToString();
                    dr["d5"] = (dtg_8.Rows[4].Cells[0].Value.ToString()).Replace("'", "´");
                    if (!(bool)dtg_8.Rows[i].Cells[3].Value)
                        dr["d5p"] = dtg_8.Rows[4].Cells[2].Value != null ? "X" : " ";
                    else
                        dr["d5d"] = dtg_8.Rows[4].Cells[3].Value != null ? "X" : " ";
                }
                if (i == 5)
                {
                    dr["d6c"] = dtg_8.Rows[5].Cells[1].Value.ToString();
                    dr["d6"] = (dtg_8.Rows[5].Cells[0].Value.ToString()).Replace("'", "´");
                    if (!(bool)dtg_8.Rows[i].Cells[3].Value)
                        dr["d6p"] = dtg_8.Rows[5].Cells[2].Value != null ? "X" : " ";
                    else
                        dr["d6d"] = dtg_8.Rows[5].Cells[3].Value != null ? "X" : " ";
                }

            }
            dr["fecha"] = textBox9.Text;
            dr["hora"] = textBox8.Text;
            dr["prnombre1"] = medico.MED_NOMBRE1;
            dr["prapellido1"] = medico.MED_APELLIDO_PATERNO;
            dr["prapellido2"] = medico.MED_APELLIDO_MATERNO;
            dr["ci"] = medico.MED_RUC.Substring(0, 10);
            ds.Tables["InterconsultaB"].Rows.Add(dr);
            return ds;
        }
        private void BuscaCIEDTG8()
        {
            //if (dtg_8.CurrentRow != null)
            //{

                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                diagnostico2 = busqueda.resultado;
                codigoCIE2 = busqueda.codigo;

                if ((diagnostico2 != "") && (diagnostico2 != null))
                {
                    if (dtg_8.Rows.Count < 7)
                    {
                        if (dtg_8.Rows.Count > 1)
                        {
                            for (int i = 0; i < dtg_8.Rows.Count - 1; i++)
                            {
                                if (busqueda.codigo == dtg_8.Rows[i].Cells[1].Value.ToString())
                                {
                                    MessageBox.Show("El procedimiento ya ha sido agregado.\r\nIntente con uno diferente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                        }
                    //DataGridViewTextBoxCell txtcell = (DataGridViewTextBoxCell)this.dtg_8.CurrentRow.Cells[0]; // se comenta por el proceso y genere una fila de manera automatica // Mario Valencia //11-03-2024
                    //DataGridViewTextBoxCell txtcell2 = (DataGridViewTextBoxCell)this.dtg_8.CurrentRow.Cells[1];

                    //if (diagnostico2 != null)
                    //{
                    //    txtcell.Value = diagnostico2;
                    //    txtcell2.Value = codigoCIE2;
                    //    diagnostico2 = "";
                    //}
                    DataGridViewRow fila = new DataGridViewRow();

                    DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell CodigoPcell = new DataGridViewTextBoxCell();

                    DataGridViewCheckBoxCell Check1Cell = new DataGridViewCheckBoxCell();
                    DataGridViewCheckBoxCell Check2Cell = new DataGridViewCheckBoxCell();

                    codigoCell.Value = null;
                    CodigoPcell.Value = busqueda.resultado;
                    textcell.Value = busqueda.codigo;

                    Check1Cell = null;
                    Check2Cell = null;

                    fila.Cells.Add(CodigoPcell);
                    fila.Cells.Add(textcell);

                    dtg_8.Rows.Add(fila);
                    index3++;
                    }
                    else
                        MessageBox.Show("No puede agregar mas de 6 procedimientos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            //}
            //else
            //{
            //    MessageBox.Show("Seleccione una fila antes de ingresar el diagnóstico.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }
        private void dtg_8_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    //case Keys.Delete:
                    //    if (dtg_8.CurrentRow != null)
                    //    {
                    //        //Int32 codigoDetAnam = 0;
                    //        if (!Int32.TryParse(dtg_8.CurrentRow.Cells[0].Value.ToString(), out Int32 codigoDetAnam))
                    //            codigoDetAnam = 0;

                    //        if (codigoDetAnam != 0)
                    //        {
                    //            if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                    //            {
                    //                MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //                return;
                    //            }
                    //            dtg_8.Rows.Remove(dtg_8.CurrentRow);
                    //            MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        }
                    //        else
                    //        {
                    //            try
                    //            {
                    //                dtg_8.Rows.Remove(dtg_8.CurrentRow);
                    //                MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            }
                    //            catch (Exception)
                    //            {

                    //                //throw;
                    //            }
                    //        }
                    //    }
                    //    break;
                    case Keys.F1:
                        BuscaCIEDTG8();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        private void dtg_8_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtg_8.CurrentRow.Cells[1].Value != null)
                {
                    if (e.ColumnIndex == this.dtg_8.Columns[2].Index)
                    {
                        DataGridViewCheckBoxCell chkpres = (DataGridViewCheckBoxCell)this.dtg_8.Rows[e.RowIndex].Cells[2];
                        if (chkpres.Value == null)
                            chkpres.Value = false;
                        else
                            chkpres.Value = true;

                        DataGridViewCheckBoxCell chkdef = (DataGridViewCheckBoxCell)this.dtg_8.Rows[e.RowIndex].Cells[3];
                        chkdef.Value = false;
                    }
                    else
                    {
                        if (e.ColumnIndex == this.dtg_8.Columns[3].Index)
                        {
                            DataGridViewCheckBoxCell chkdef = (DataGridViewCheckBoxCell)this.dtg_8.Rows[e.RowIndex].Cells[3];
                            if (chkdef.Value == null)
                                chkdef.Value = false;
                            else
                                chkdef.Value = true;

                            DataGridViewCheckBoxCell chkpres = (DataGridViewCheckBoxCell)this.dtg_8.Rows[e.RowIndex].Cells[2];
                            chkpres.Value = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se ha agregado diagnostico en esta fila.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtg_8.Rows.RemoveAt(dtg_8.CurrentRow.Index);
                }
            }
            catch
            {
            }
        }

        private void btnF1_8_Click(object sender, EventArgs e)
        {
            BuscaCIEDTG8();
        }
        private void cargarHora()
        {
            textBox9.Text = System.DateTime.Now.ToString("yyyy/MM/dd");
            textBox8.Text = System.DateTime.Now.ToString("HH:mm:ss");
        }
        private void AgregarError(Control control)
        {
            error.SetError(control, "Campo Requerido");
        }
        private bool validarFormulario()
        {
            bool flag = false;
            error.Clear();
            if (txt_ccInterconsulta.Text.ToString() == string.Empty)
            {
                AgregarError(txt_ccInterconsulta);
                flag = true;
            }
            if (txt_plan_diagnostico.Text.ToString() == string.Empty)
            {
                AgregarError(txt_plan_diagnostico);
                flag = true;
            }
            if (txt_plan_tratamiento.Text.ToString() == string.Empty)
            {
                AgregarError(txt_plan_tratamiento);
                flag = true;
            }
            if (txt_resumen.Text.ToString() == string.Empty)
            {
                AgregarError(txt_resumen);
                flag = true;
            }
            if (dtg_8.Rows.Count == 1)
            {
                AgregarError(dtg_8);
                flag = true;
            }
            foreach (DataGridViewRow fila in dtg_8.Rows)
            {
                DataGridViewCheckBoxCell txtcell = (DataGridViewCheckBoxCell)this.dtg_8.Rows[fila.Index].Cells[2];
                DataGridViewCheckBoxCell txtcell2 = (DataGridViewCheckBoxCell)this.dtg_8.Rows[fila.Index].Cells[3];
                DataGridViewTextBoxCell caja = (DataGridViewTextBoxCell)this.dtg_8.Rows[fila.Index].Cells[0];
                if ((txtcell.Value == null) && (txtcell2.Value == null) && (caja.Value != null))
                {
                    AgregarError(dtg_8);
                    flag = true;
                }
            }
            return flag;
        }

        private void btnimagen_Click(object sender, EventArgs e)
        {

        }

        private void btnCierre_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Usted cerrara la interconsulta y enviara \n los resultados por correo electronico", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("interB");
                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                usuario.ShowDialog();
                if (!usuario.aceptado)
                    return;              
                MEDICOS MED = NegMedicos.RecuperaMedicoIdUsuario(usuario.usuarioActual);
                if (MED != null)
                {
                    if (inter.HIN_MEDICO_INTERCONSULTADO == MED.MED_CODIGO)
                    {
                        interconsulta = new HC_INTERCONSULTA();
                        interconsulta.HIN_CODIGO = hin_codigo;
                        interconsulta.HIN_ESTADO = false;
                        if (!NegInterconsulta.cerrarInterconsulta(interconsulta))
                            MessageBox.Show("No se pudo cerrar la interconsulta", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        else
                        {
                            envioCorreo();
                            //Imprimir();
                            limpiarCampos();
                            habilitaGrid(false);
                            refrescarSolicitudes();
                            habilitarBotones(false, false, false);
                        }
                    }
                    else
                        MessageBox.Show("Solo el medico interconsultado puede responder", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnabrir_Click(object sender, EventArgs e)
        {

        }
    }
}
