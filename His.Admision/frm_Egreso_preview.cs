using His.DatosReportes;
using His.Entidades;
using His.Entidades.Clases;
using His.Formulario;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Admision
{
    public partial class frm_Egreso_preview : Form
    {
        int AteCodigo;
        ATENCIONES ultimaAtencion = null;
        string hc = "";
        public Int64 _usuarioActual = 0;
        public frm_Egreso_preview(int x, string _hc)
        {
            InitializeComponent();
            AteCodigo = x;
            gridMedicos.DataSource = NegDietetica.getDataTable("GetEgreso_MedicosEvolucion", AteCodigo.ToString());
            gridMedicos.DisplayLayout.Bands[0].Columns[0].Header.Caption = "NOMBRE DEL MEDICO";
            hc = _hc;
            //txtx_ate.Text = x.ToString();

        }

        public void Egreso2020(string codigo, string hc2)
        {
            try
            {
                DataTable DatosPcte = NegDietetica.getDataTable("GetEgreso_DatosPaciente", codigo);
                DataTable Medicos = NegDietetica.getDataTable("GetEgreso_MedicosEvolucion", codigo);
                DataTable Garantias = NegDietetica.getDataTable("GetEgreso_Garantias", codigo);
                DataTable HistorialHabitacion = NegDietetica.getDataTable("GetEgreso_HistorialHabitacion", codigo);
                DataTable ConvenioSeguro = NegDietetica.getDataTable("GetEgreso_ConvenioSeguro", codigo);
                DataTable MedicosImp = NegHabitaciones.CargarMedicos(Convert.ToInt64(codigo));
                DateTime fechaNacimiento = NegHabitaciones.RecuperaFechaNacimiento(hc2);
                USUARIOS USUA = new USUARIOS();
                if (MedicosImp.Rows[0][3].ToString() != "")
                    USUA = NegUsuarios.RecuperarUsuarioID(Convert.ToInt32(MedicosImp.Rows[0][3].ToString()));
                #region//limpiar tablas
                ReportesHistoriaClinica r1 = new ReportesHistoriaClinica(); r1.DeleteTable("rptEgreso_DatosPcte");
                ReportesHistoriaClinica r2 = new ReportesHistoriaClinica(); r2.DeleteTable("rptEgreso_Medicos");
                ReportesHistoriaClinica r3 = new ReportesHistoriaClinica(); r3.DeleteTable("rptEgreso_Garantias");
                ReportesHistoriaClinica r4 = new ReportesHistoriaClinica(); r4.DeleteTable("rptEgreso_HabitacionHistorial");
                ReportesHistoriaClinica r5 = new ReportesHistoriaClinica(); r5.DeleteTable("rptEgreso_ConvenioSeguro");
                #endregion
                //#region //empaquetar y guardar en tablas access
                foreach (DataRow row in DatosPcte.Rows)
                {
                    ///edad
                    var now = DateTime.Now;
                    var birthday = fechaNacimiento;
                    var yearsOld = now - birthday;

                    int years = (int)(yearsOld.TotalDays / 365.25);
                    string[] x = new string[] {
                                row["PAC_HISTORIA_CLINICA"].ToString(),
                                row["PACIENTE"].ToString(),
                                row["PAC_IDENTIFICACION"].ToString(),
                                row["PAC_FECHA_NACIMIENTO"].ToString(),
                                row["hab_Numero"].ToString(),
                                row["ATE_FECHA_INGRESO"].ToString(),
                                row["ATE_CODIGO"].ToString(),
                                row["ATE_NUMERO_ATENCION"].ToString(),
                                row["MEDICO"].ToString(),
                                row["TIP_DESCRIPCION"].ToString(),
                                row["TIA_DESCRIPCION"].ToString(),
                                row["ATE_DIAGNOSTICO_INICIAL"].ToString(),
                                row["ATE_DIAGNOSTICO_FINAL"].ToString(),
                                row["USUARIO"].ToString(),
                                row["REFERIDO"].ToString(),
                                row["FECHA_ALTA"].ToString(),
                                years.ToString()
                        };
                    ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                    AUXma.InsertTable("rptEgreso_DatosPcte", x);
                }
                if (gridMedicos.Rows.Count > 0)
                {
                    for (int i = 0; i < gridMedicos.Rows.Count; i++)
                    {
                        string[] x = new string[] {
                                gridMedicos.Rows[i].Cells[0].Value.ToString()
                        };
                        ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                        AUXma.InsertTable("rptEgreso_Medicos", x);
                    }
                }
                else
                {
                    foreach (DataRow row in Medicos.Rows)
                    {
                        string[] x = new string[] {
                                row["NOM_USUARIO"].ToString()
                        };
                        ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                        AUXma.InsertTable("rptEgreso_Medicos", x);
                    }
                }

                foreach (DataRow row in Garantias.Rows)
                {
                    string[] x = new string[] {
                                row["ADG_FECHA"].ToString(),
                                row["TG_NOMBRE"].ToString(),
                                row["ADG_VALOR"].ToString(),
                                row["TITULAR"].ToString(),
                                row["ADG_BANCO"].ToString(),
                                row["ADG_DOCUMENTO"].ToString(),
                                row["ADG_TIPOTARJETA"].ToString(),
                                row["ADG_ESTATUS"].ToString(),
                                row["ADG_OBSERVACION"].ToString()
                        };
                    ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                    AUXma.InsertTable("rptEgreso_Garantias", x);
                }


                foreach (DataRow row in HistorialHabitacion.Rows)
                {
                    string[] x = new string[] {
                                row["OBSERVACION"].ToString(),
                                row["FECHA_MOVIMIENTO"].ToString(),
                                row["HORA"].ToString(),
                                row["HABITACION"].ToString(),
                                row["ANTERIOR"].ToString(),
                                row["ESTADO"].ToString(),
                                row["USUARIO"].ToString()
                        };
                    ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                    AUXma.InsertTable("rptEgreso_HabitacionHistorial", x);
                }

                foreach (DataRow row in ConvenioSeguro.Rows)
                {
                    string[] x = new string[] {
                                row["CAT_NOMBRE"].ToString(),
                                row["ADA_FECHA_INICIO"].ToString(),
                                row["ADA_FECHA_FIN"].ToString(),
                                row["ADA_MONTO_COBERTURA"].ToString()
                        };
                    ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                    AUXma.InsertTable("rptEgreso_ConvenioSeguro", x);
                }

                string[] x2 = new string[] { MedicosImp.Rows[0][1].ToString(), USUA.APELLIDOS + ' ' + USUA.NOMBRES };
                ReportesHistoriaClinica AUXmha = new ReportesHistoriaClinica();

                AUXmha.InsertTable("rptupdate_dtos", x2);
                System.Threading.Thread.Sleep(3000);
                //limpiar tablas
                //ReportesHistoriaClinica r2 = new ReportesHistoriaClinica(); r2.DeleteTable("rptEgreso_Medicos");
                //foreach (DataRow row in Medicos.Rows)
                //{
                //    string[] x = new string[] {
                //            row["NOM_USUARIO"].ToString()
                //    };
                //    ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                //    AUXma.InsertTable("rptEgreso_Medicos", x);
                //}
                //UltraGridBand band = this.gridMedicos.DisplayLayout.Bands[0];
                //foreach (UltraGridRow row in band.GetRowEnumerator(GridRowType.DataRow))
                //{

                //    if ((row.Cells[0].Value.ToString().Trim())!="")
                //    {
                //        string[] x = new string[] {
                //                row.Cells[0].Value.ToString(), txtobservacion.Text
                //        };
                //        ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                //        AUXma.InsertTable("rptEgreso_Medicos", x);
                //    }
                //}
                //string[] x2 = new string[] {txtobservacion.Text.Trim(), Sesion.nomUsuario};

                //ReportesHistoriaClinica AUXmha = new ReportesHistoriaClinica();
                //AUXmha.InsertTable("rptupdate_dtos", x2);
                ////System.Threading.Thread.Sleep(3000);


                //llamo al reporte
                frmReportes form = new frmReportes();
                form.reporte = "EGRESO_LX";
                form.ShowDialog();

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ultimaAtencion = NegAtenciones.AtencionID(AteCodigo);
            //Cambios Edgar 20210201
            //CAMBIO EL ESTADO DE LA HABITACION A ALTA PROGRAMADA Y ESC_CODIGO=2 POR MEDIO DEL USUARIO
            if (gridMedicos.Rows.Count > 0)
            {
                if (txtobservacion.Text != "")
                {
                    if (ultimaAtencion.ESC_CODIGO == 1)
                    {
                        if (MessageBox.Show("¿Desea dar de alta a paciente?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios();
                            usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                            usuario.ShowDialog();
                            _usuarioActual = usuario.usuarioActual;
                            if (!usuario.aceptado)
                                return;
                            try
                            {
                                ultimaAtencion.ATE_FECHA_ALTA = DateTime.Today;
                                if (txtobservacion.Text != "")
                                {
                                    foreach (UltraGridRow item in gridMedicos.Rows)
                                    {
                                        NegHabitaciones.SaveMedicosAlta(item.Cells[0].Value.ToString(), AteCodigo, txtobservacion.Text, _usuarioActual);
                                    }
                                }

                                if (NegFactura.AltaProgramada(ultimaAtencion.ATE_CODIGO) == 1)
                                {
                                    NegAtenciones.CrearCAMBIO_ESTADO_ATENCIONES(ultimaAtencion.ATE_CODIGO, 2, _usuarioActual, "HOJA ALTA");
                                    MessageBox.Show("Paciente ha sido dado de alta.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Egreso2020(AteCodigo.ToString(), hc);
                                }
                                else
                                    MessageBox.Show("Paciente no pudo ser dado de Alta.\nComuniquese con el Administrador.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Paciente no pudo ser dado de Alta.\nComuniquese con el Administrador.\r\n" + ex, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                            this.Close();
                        this.Close();
                    }
                    else
                        MessageBox.Show("Pacienta Ya Fue Dado De Alta", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Debe agregar una observacion de Alta", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Debe agregar como minimo un médico", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);


        }

        private void btnAgregaEstudio_Click(object sender, EventArgs e)
        {

            List<MEDICOS> listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
                cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
        }

        private void cargarMedico(int codMedico)
        {
            bool existe = false;
            string medicoexiste = "";
            MEDICOS medico = NegMedicos.MedicoID(codMedico);
            if (medico != null)
            {
                DataTable med = NegMedicos.MedicoIDValida(codMedico);
                if (med.Rows[0][0].ToString() == "7")
                {
                    MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //txtNombre.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                //txtCOD.Text = Convert.ToString(medico.MED_CODIGO);
                medicoexiste = "Dr." + medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                if (gridMedicos.Rows.Count > 0)
                {
                    foreach (var item in gridMedicos.Rows)
                    {
                        if (medicoexiste == item.Cells[0].Value.ToString())
                        {
                            existe = true;
                            break;
                        }
                        else
                            existe = false;
                    }
                }
                if (existe == false)
                {
                    UltraGridRow row = this.gridMedicos.DisplayLayout.Bands[0].AddNew();
                    row.Cells[0].Value = "Dr." + medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                    //row.Cells[2].Value = Color.Red;
                }
                else
                {
                    MessageBox.Show("El médico ya ha sido asignado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
