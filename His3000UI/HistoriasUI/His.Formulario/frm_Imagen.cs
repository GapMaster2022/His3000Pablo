using His.DatosReportes;
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
using His.Entidades.Clases;
using System.Threading;
using System.Windows.Input;
using His.Formulario;

namespace His.Formulario
{
    public partial class frm_Imagen : Form
    {
        private SplitContainer splitContainer1;
        private DataGridView gridSol;
        private Label label1;
        private TextBox txtRadiologo;
        private TextBox txtCODRadiologo;
        private TextBox txtTecnologo;
        private TextBox txtCODTecnologo;
        private TextBox txtMedico;
        private TextBox txtMedicoCOD;
        private Button btnAddRadiologo;
        private Infragistics.Win.Misc.UltraLabel ultraLabel9;
        private Button btnAddTecnologo;
        private Infragistics.Win.Misc.UltraLabel ultraLabel8;
        private Button btnAddMedico;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private RadioButton optControl;
        private RadioButton optRutina;
        private RadioButton optUrgente;
        private Infragistics.Win.Misc.UltraLabel ultraLabel5;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
        private DateTimePicker dtpFechaNota;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl utcEvolucion;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
        private GroupBox groupBox1;
        private DataGridView gridEstudios;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn ESTUDIO;
        private DataGridViewTextBoxColumn CODSUB;
        private DataGridViewTextBoxColumn NOTA;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Button btnAgregaEstudio;
        private GroupBox groupBox2;
        private CheckBox chk3;
        private CheckBox chk4;
        private CheckBox chk2;
        private CheckBox chk1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl2;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtResumen;
        private Infragistics.Win.Misc.UltraLabel ultraLabel6;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtMotivo;
        private Infragistics.Win.Misc.UltraLabel ultraLabel4;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl3;
        private GroupBox groupBox3;
        private Button btnAddDiagnotico;
        private DataGridView gridDiagnosticos;
        private DataGridViewTextBoxColumn CIE10;
        private DataGridViewTextBoxColumn DIAGNOSTICO;
        private DataGridViewComboBoxColumn TIPO;
        private Infragistics.Win.Misc.UltraLabel ultraLabel7;
        private Label label2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private ToolStrip tools;
        private ToolStripButton btnNuevo;
        private ToolStripButton btnModificar;
        private ToolStripButton btnGuardar;
        private ToolStripButton btnCancelar;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripDropDownButton btnImprimir;
        private ToolStripMenuItem btnImprimirResumen;
        private ToolStripMenuItem btnImprimirIndividual;
        private Panel panel1;
        private TextBox txtSolicitudActiva;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private IContainer components;
        int nAtencion = 0;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl4;
        private Infragistics.Win.Misc.UltraLabel ultraLabel10;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txt_concluciones;
        public bool mushuñan = false;
        NegCertificadoMedico Certificado = new NegCertificadoMedico();

        #region constructor
        public frm_Imagen()
        {
            InitializeComponent();
            tools.Enabled = false;
        }

        public frm_Imagen(int nAtencion)
        {
            InitializeComponent();
            controles_reset();
            this.nAtencion = nAtencion;
            refrescarSolicitudes();

            int ingreso = NegTipoIngreso.RecuperarporAtencion(nAtencion);
            if (ingreso == 10)
                mushuñan = true;
            //aqui va el estado de cuenta para validar el bloqueo 
            NegAtenciones atencion = new NegAtenciones();
            string estado = atencion.EstadoCuenta(Convert.ToString(nAtencion));
            if(estado != "1")
            {
                Bloquear();
            }
            
           
        }
        #endregion

        public void Bloquear()
        {
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            //btnImprimir.Enabled = false;
            btnModificar.Enabled = false;
        }
        private PedidoImagen empaquetar()
        {
            PedidoImagen p = new PedidoImagen();
            p.id_imagenologia = Convert.ToInt32(txtSolicitudActiva.Text);
            p.MED_CODIGO = Convert.ToInt32((txtMedicoCOD.Text).Trim());
            p.FECHA_CREACION = dtpFechaNota.Value;
            p.PRIORIDAD = 1;
            if (optRutina.Checked)
                p.PRIORIDAD = 2;
            if (optUrgente.Checked)
                p.PRIORIDAD = 3;
            p.estado_movimiento = 0;
            if (chk1.Checked)
                p.estado_movimiento = 1;
            p.estado_retirarsevendas = 0;
            if(chk2.Checked)
                p.estado_retirarsevendas = 1;
            p.estado_medicopresente = 0;
            if (chk4.Checked)
                p.estado_medicopresente = 1;
            p.estado_encama = 0;
            if(chk3.Checked)
                p.estado_encama = 1;
            p.motivo = txtMotivo.Text;
            p.resumen_clinico = txtResumen.Text;
            USUARIOS u = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
            p.ID_USUARIO = u.ID_USUARIO;
            p.MED_RADIOLOGO = Convert.ToInt32(txtCODRadiologo.Text);
            p.MED_TECNOLOGO = Convert.ToInt32(txtCODTecnologo.Text);
            p.ATE_CODIGO = nAtencion;
                List<PedidoImagen_estudios> ListEstudios = new List<PedidoImagen_estudios>();
                foreach (DataGridViewRow row in gridEstudios.Rows)
                {
                    PedidoImagen_estudios estudio = new PedidoImagen_estudios();
                    estudio.PRO_CODIGO = Convert.ToInt32(row.Cells["ID"].Value);
                    estudio.PRO_CODSUB = Convert.ToInt32(row.Cells["CODSUB"].Value);
                    estudio.dato_adicional = Convert.ToString(row.Cells["NOTA"].Value);
                    ListEstudios.Add(estudio);
                }
            p.estudios = ListEstudios;
                List<PedidoImagen_diagnostico> ListDiagnosticos = new List<PedidoImagen_diagnostico>();
                foreach (DataGridViewRow row in gridDiagnosticos.Rows)
                {
                    PedidoImagen_diagnostico estudio = new PedidoImagen_diagnostico();
                    estudio.CIE_CODIGO = Convert.ToString(row.Cells["CIE10"].Value);
                    if (Convert.ToString(row.Cells["TIPO"].Value) == "DEFINITIVO")
                        estudio.DEFINITIVO = 1;
                    else
                        estudio.DEFINITIVO = 0;
                    ListDiagnosticos.Add(estudio);
                }
            p.diagnosticos = ListDiagnosticos;
            p.CONCLUCIONES = txt_concluciones.Text;

            return p;
        }

        #region validaciones y comportamiento de formulario
        private void controles_reset()
        {
            //limpiar campos
            txtMedico.Text = string.Empty;
            txtMedicoCOD.Text = string.Empty;
            txtCODRadiologo.Text = "0";
            txtCODTecnologo.Text = "0";
            txtRadiologo.Text = string.Empty;
            txtTecnologo.Text = string.Empty;
            dtpFechaNota.Text = DateTime.Now.ToString();
            optControl.Checked = true;
            optRutina.Checked = false;
            optUrgente.Checked = false;
            txtSolicitudActiva.Text = "0";
            gridEstudios.Rows.Clear();
            chk1.Checked = false;
            chk2.Checked = false;
            chk3.Checked = false;
            chk4.Checked = false;        
            txtMotivo.Text = string.Empty;
            txtResumen.Text = string.Empty;
            gridDiagnosticos.Rows.Clear();
            //Control de botones
            btnNuevo.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            //inhabilitar
            enabledCampos(false);
        }

        private bool validar()
        {
            if (txtMedico.Text.Trim() == string.Empty)
                return false;
            if (txtMotivo.Text.Trim() == string.Empty)
                return false;
            if (txtResumen.Text.Trim() == string.Empty)
                return false;
            if(gridEstudios.RowCount==0)
                return false;
            if (gridDiagnosticos.RowCount == 0)
                return false;
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
                cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), txtMedico, txtMedicoCOD);
        }

        private void cargarMedico(int codMedico, TextBox txtNombre, TextBox txtCOD)
        {

            MEDICOS medico = NegMedicos.MedicoID(codMedico);
            if (medico != null)
            {
                DataTable med = NegMedicos.MedicoIDValida(Convert.ToInt16(codMedico));
                if (med.Rows[0][0].ToString() == "7")
                {
                    MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                txtNombre.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                txtCOD.Text = Convert.ToString(medico.MED_CODIGO);
            }


        }

        private void btnAgregaEstudio_Click(object sender, EventArgs e)
        {

            frm_ImagenAyuda ayuda = new frm_ImagenAyuda("PRODUCTOS");
            ayuda.ShowDialog();
            if (ayuda.codigo != string.Empty)
            {
                if (!BuscarItem(ayuda.codigo, gridEstudios))
                    this.gridEstudios.Rows.Add(ayuda.codigo, ayuda.producto, ayuda.adicional1);
                else
                    MessageBox.Show("El item ya fue ingresado.");
            }
        }

        private void btnAddDiagnotico_Click(object sender, EventArgs e)
        {
            
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

        private void controles_prepararedicion()
        { ///LIMPIA TODO - habilita campos - solo guardar o cancelar
            //limpiar
            txtMedico.Text = string.Empty;
            txtMedicoCOD.Text = string.Empty;
            txtCODRadiologo.Text = "0";
            txtCODTecnologo.Text = "0";
            txtRadiologo.Text = string.Empty;
            txtTecnologo.Text = string.Empty;
            dtpFechaNota.Text = DateTime.Now.ToString();
            optControl.Checked = true;
            optRutina.Checked = false;
            optUrgente.Checked = false;
            //txtSolicitudActiva.Text = "0";
            gridEstudios.Rows.Clear();
            chk1.Checked = false;
            chk2.Checked = false;
            chk3.Checked = false;
            chk4.Checked = false;
            txtMotivo.Text = string.Empty;
            txtResumen.Text = string.Empty;
            gridDiagnosticos.Rows.Clear();
            //Control de botones
            btnNuevo.Enabled = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            //inhabilitar
            enabledCampos(true);
        }

        private void controles_visualizaPedido()
        {///solo BLOQUEA campos, permite MODIFICAR O CANCELAR 

            //Control de botones
            btnNuevo.Enabled = false;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = true;
            //inabilitar
            enabledCampos(false);

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            controles_prepararedicion();

        }

        #endregion

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                try
                {
                    NegImagen.saveSolicitud(empaquetar());
                    MessageBox.Show("Se guardo exitosamente.", "HIS3000");
                    controles_reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sucito algun error. /n [" + ex + "]", "HIS3000");
                }

                refrescarSolicitudes();
            }
            else
                MessageBox.Show("Revise que todos los campos esten llenos.");
        }

        private void refrescarSolicitudes()
        {
            gridSol.DataSource = NegImagen.getSolicitudes(this.nAtencion);
            gridSol.Columns["id"].Visible = false;
            gridSol.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            if (gridSol.RowCount > 0)
                btnImprimir.Enabled = true;
            else
                btnImprimir.Enabled = false;
        }


        private void cargarPedido(int idImagenologia)
        {
            #region recupero tabla principal
            DataTable c = NegImagen.getPedidoCabecera(idImagenologia);
            txtMedico.Text = Convert.ToString(c.Rows[0]["MED_APELLIDO_PATERNO"]) + " " + Convert.ToString(c.Rows[0]["MED_APELLIDO_MATERNO"]) + " " + Convert.ToString(c.Rows[0]["MED_NOMBRE1"]) + " " + Convert.ToString(c.Rows[0]["MED_NOMBRE2"]);
            txtMedicoCOD.Text = Convert.ToString(c.Rows[0]["MED_CODIGO"]) ;
            txtTecnologo.Text = Convert.ToString(c.Rows[0]["TEC_AP"]) + " " + Convert.ToString(c.Rows[0]["TEC_AM"]) + " " + Convert.ToString(c.Rows[0]["TEC_N1"]) + " " + Convert.ToString(c.Rows[0]["TEC_N2"]);
            txtCODTecnologo.Text = Convert.ToString(c.Rows[0]["COD_TEC"]);
            txtRadiologo.Text = Convert.ToString(c.Rows[0]["RAD_AP"]) + " " + Convert.ToString(c.Rows[0]["RAD_AM"]) + " " + Convert.ToString(c.Rows[0]["RAD_N1"]) + " " + Convert.ToString(c.Rows[0]["RAD_N2"]);
            txtCODRadiologo.Text = Convert.ToString(c.Rows[0]["COD_RAD"]);
            dtpFechaNota.Value = Convert.ToDateTime(c.Rows[0]["FECHA_CREACION"]);
            if (Convert.ToInt32(c.Rows[0]["PRIORIDAD"]) == 1)
                optControl.Checked = true;
            if (Convert.ToInt32(c.Rows[0]["PRIORIDAD"]) == 2)
                optRutina.Checked = true;
            if (Convert.ToInt32(c.Rows[0]["PRIORIDAD"]) == 3)
                optUrgente.Checked = true;
            if (Convert.ToInt32(c.Rows[0]["estado_movimiento"]) == 1)
                chk1.Checked = true;
            if (Convert.ToInt32(c.Rows[0]["estado_retirarsevendas"]) == 1)
                chk2.Checked = true;
            if (Convert.ToInt32(c.Rows[0]["estado_medicopresente"]) == 1)
                chk4.Checked = true;
            if (Convert.ToInt32(c.Rows[0]["estado_encama"]) == 1)
                chk3.Checked = true;
            txtMotivo.Text = Convert.ToString(c.Rows[0]["motivo"]);
            txtResumen.Text = Convert.ToString(c.Rows[0]["resumen_clinico"]);
            #endregion

            DataTable estudios = NegImagen.getPedidoEstudios(idImagenologia);

            foreach (DataRow row in estudios.Rows)
            {
                this.gridEstudios.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString());
            }

            DataTable diagnosticos = NegImagen.getPedidoDiagnosticos(idImagenologia);

            foreach (DataRow row in diagnosticos.Rows)
            {
                string aux;
                if ((row[2].ToString()) == "True")
                    aux = "DEFINITIVO";
                else
                    aux = "PRESUNTIVO";
               
                this.gridDiagnosticos.Rows.Add(row[0].ToString(), row[1].ToString(),aux );
            }


        }
      
        private void enabledCampos(bool x)
        {
            if (x)
            {
                btnAddMedico.Visible = true;
                btnAddRadiologo.Visible = true;
                btnAddTecnologo.Visible = true;
                optControl.Enabled = true;
                optRutina.Enabled = true;
                optUrgente.Enabled = true;
            
                groupBox2.Enabled = true;
                groupBox1.Enabled = true;
                txtMotivo.Enabled = true;
                txtResumen.Enabled = true;
                groupBox3.Enabled = true;

            }else
            {
                btnAddMedico.Visible = false;
                btnAddRadiologo.Visible = false;
                btnAddTecnologo.Visible = false;
                optControl.Enabled = false;
                optRutina.Enabled = false;
                optUrgente.Enabled = false;
                
                groupBox2.Enabled = false;
                groupBox1.Enabled = false;
                txtMotivo.Enabled = false;
                txtResumen.Enabled = false;
                groupBox3.Enabled = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            controles_reset();
        }

      

        private void btnModificar_Click(object sender, EventArgs e)
        {
            enabledCampos(true);
            //Control de botones
            btnNuevo.Enabled = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void txtMotivo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (this.GetNextControl(ActiveControl, true) != null)
                {
                    e.Handled = true;
                    this.GetNextControl(ActiveControl, true).Focus();

                }
            }
        }

        private void txtResumen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (this.GetNextControl(ActiveControl, true) != null)
                {
                    e.Handled = true;
                    this.GetNextControl(ActiveControl, true).Focus();

                }
            }
        }

        public int CalcularEdad(DateTime birthDate)
        {
            DateTime now = DateTime.Now; ;
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                age--;
            return age;
        }

        private void ImprimirReporteResumen()
        {
            try
            {
                //limpiar talas
                ReportesHistoriaClinica imagenL = new ReportesHistoriaClinica();
                imagenL.limpiarImagenologia();
                ReportesHistoriaClinica imagend = new ReportesHistoriaClinica();
                imagend.limpiarImagenologiaDiagnostico();
                //empaquetar y guardar en tablas access
                foreach (DataGridViewRow row in gridSol.Rows)
                {
                    ReportesHistoriaClinica imagen = new ReportesHistoriaClinica();                    
                    imagen.ingresarImagenologia(empaquetarReporteCabecera(Convert.ToInt32(row.Cells[0].Value)));
                    List<PedidoImagen_reporteDiagnosticos> ListDx = empaquetarReporteDx(Convert.ToInt32(row.Cells[0].Value));
                    foreach (var dx in ListDx)
                    {
                        ReportesHistoriaClinica imagendx = new ReportesHistoriaClinica();
                        imagendx.ingresarImagenologiaDiagnostico(dx);
                    }
                }





                frmReportes ventana = new frmReportes(1, "imagenologiaResumen");
                ventana.Show();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void imprimir(int imagen)
        {
            DataTable i = NegImagen.getReporteEncabezado(imagen);
            DsFromImagen ds = new DsFromImagen();
            DataRow ima;
            ima = ds.Tables["cabImagen"].NewRow();
            if (!mushuñan)
            {
                ima["path"] = NegUtilitarios.RutaLogo("General"); //retorna la ruta del logo general.
                ima["clinica"] = Convert.ToString(i.Rows[0]["clinica"]);
            }
            else
            {
                ima["path"] = NegUtilitarios.RutaLogo("Mushuñan"); //retarna la ruta de mushuñan
                ima["clinica"] = "SANTA CATALINA DE SENA";
            }
            ima["parroquia"] = Convert.ToString(i.Rows[0]["COD_PARROQUIA"]);
            ima["canton"] = Convert.ToString(i.Rows[0]["COD_CANTON"]);
            ima["provincia"] = Convert.ToString(i.Rows[0]["COD_PROVINCIA"]);
            ima["HC"] = Convert.ToString(i.Rows[0]["PAC_HISTORIA_CLINICA"]);
            ima["edad"] = Convert.ToString(CalcularEdad(Convert.ToDateTime(i.Rows[0]["PAC_FECHA_NACIMIENTO"])));
            ima["apellidoPaterno"] = Convert.ToString(i.Rows[0]["PAC_APELLIDO_PATERNO"]);
            ima["apellidoMaterno"] = Convert.ToString(i.Rows[0]["PAC_APELLIDO_MATERNO"]);
            ima["primerNombre"] = Convert.ToString(i.Rows[0]["PAC_NOMBRE1"]);
            ima["segundoNombre"] = Convert.ToString(i.Rows[0]["PAC_NOMBRE2"]);
            ima["idImagenologia"] = Convert.ToString(i.Rows[0]["PAC_IDENTIFICACION"]);
            ima["descripcion"] = Convert.ToString(i.Rows[0]["TIP_DESCRIPCION"]);
            ima["habitacion"] = Convert.ToString(i.Rows[0]["HAB_CODIGO"]);
            if (Convert.ToString(i.Rows[0]["PRIORIDAD"]) == "1")
                ima["control"] = "X";
            if (Convert.ToString(i.Rows[0]["PRIORIDAD"]) == "3")
                ima["urgente"] = "X";
            if (Convert.ToString(i.Rows[0]["PRIORIDAD"]) == "2")
                ima["rutina"] = "X";
            ima["fechaCreacion"] = Convert.ToString(i.Rows[0]["FECHA_CREACION"]);
            ima["medico"] = Convert.ToString(i.Rows[0]["medico"]);
            ima["radiologo"] = Convert.ToString(i.Rows[0]["radiologo"]);
            ima["tecnologo"] = Convert.ToString(i.Rows[0]["PAC_IDENTIFICACION"]);
            ima["codMedico"] = Convert.ToString(i.Rows[0]["COD_medico"]);
            ima["codTecnologo"] = Convert.ToString(i.Rows[0]["COD_tecn"]);
            ima["codRadiologo"] = Convert.ToString(i.Rows[0]["COD_rad"]);
            ima["motivo"] = Convert.ToString(i.Rows[0]["motivo"]);
            ima["resumenclinico"] = Convert.ToString(i.Rows[0]["resumen_clinico"]);
            if (Convert.ToInt32(i.Rows[0]["estado_movimiento"]) == 1)
                ima["estadoMovimiento"] = "X";
            if (Convert.ToInt32(i.Rows[0]["estado_retirarsevendas"]) == 1)
                ima["estadoRetiraVendas"] = "X";
            if (Convert.ToInt32(i.Rows[0]["estado_medicopresente"]) == 1)
                ima["estadoMedicoPresente"] = "X";
            if (Convert.ToInt32(i.Rows[0]["estado_encama"]) == 1)
                ima["estadoEnCama"] = "X";
            DataTable rub = NegImagen.getReporteRubros(imagen);
            string auxRubros = "";
            foreach (DataRow row in rub.Rows)
            {
                auxRubros += (row["RUB_NOMBRE"].ToString().PadRight(18)).PadLeft(22);
                if (row["vez"].ToString() == "2")
                    auxRubros += " X ";
                else
                    auxRubros += "   ";
            }
            ima["rubros"] = auxRubros;

            DataTable estudios = NegImagen.getPedidoEstudios(imagen);
            auxRubros = "";
            foreach (DataRow row in estudios.Rows)
            {
                auxRubros += row[1].ToString() + ".      ";
            }
            ima["estudios"] = auxRubros;

            ima["concluciones"] = Convert.ToString(i.Rows[0]["concluciones"]);
            ds.Tables["cabImagen"].Rows.Add(ima);

            DataTable diagnosticos = NegImagen.getPedidoDiagnosticos(imagen);
            DataRow cie;
            foreach (DataRow item in diagnosticos.Rows)
            {
                cie = ds.Tables["cie10"].NewRow();
                cie["diagnostico"] = item[1].ToString();
                cie["cie10"] = item[0].ToString();
                cie["id"] = imagen;
                if ((item[2].ToString()) == "True")
                    cie["definitivo"] = "X";
                else
                    cie["presuntivo"] = "X";
                ds.Tables["cie10"].Rows.Add(cie);
            }

            frmReportes myreport = new frmReportes(1, "formImagen", ds);
            myreport.Show();

        }

        private PedidoImagen_reporte empaquetarReporteCabecera(int idImagenologia)
        {
            PedidoImagen_reporte repor = new PedidoImagen_reporte();

            DataTable e = NegImagen.getReporteEncabezado(idImagenologia);///aqui revisar cuando y de donde obtener
            if (!mushuñan)
            {
                repor.path = NegUtilitarios.RutaLogo("General"); //retorna la ruta del logo general.
                repor.clinica = Convert.ToString(e.Rows[0]["clinica"]);
            }
            else
            {
                repor.path = NegUtilitarios.RutaLogo("Mushuñan"); //retarna la ruta de mushuñan
                repor.clinica = "SANTA CATALINA DE SENA";
            }
            repor.id_imagenologia = idImagenologia.ToString();
            repor.COD_PARROQUIA = Convert.ToString(e.Rows[0]["COD_PARROQUIA"]);
            repor.COD_CANTON = Convert.ToString(e.Rows[0]["COD_CANTON"]);
            repor.COD_PROVINCIA = Convert.ToString(e.Rows[0]["COD_PROVINCIA"]);
            repor.PAC_HISTORIA_CLINICA = Convert.ToString(e.Rows[0]["PAC_HISTORIA_CLINICA"]);
            repor.edad = Convert.ToString(CalcularEdad(Convert.ToDateTime(e.Rows[0]["PAC_FECHA_NACIMIENTO"])));
            repor.PAC_APELLIDO_PATERNO = Convert.ToString(e.Rows[0]["PAC_APELLIDO_PATERNO"]);
            repor.PAC_APELLIDO_MATERNO = Convert.ToString(e.Rows[0]["PAC_APELLIDO_MATERNO"]);
            repor.PAC_NOMBRE1 = Convert.ToString(e.Rows[0]["PAC_NOMBRE1"]);
            repor.PAC_NOMBRE2 = Convert.ToString(e.Rows[0]["PAC_NOMBRE2"]);
            repor.TIP_DESCRIPCION = Convert.ToString(e.Rows[0]["TIP_DESCRIPCION"]);
            repor.HAB_CODIGO = Convert.ToString(e.Rows[0]["HAB_CODIGO"]);
            if (Convert.ToString(e.Rows[0]["PRIORIDAD"]) == "1")
                repor.control = "X";
            if (Convert.ToString(e.Rows[0]["PRIORIDAD"]) == "3")
                repor.urgente = "X";
            if (Convert.ToString(e.Rows[0]["PRIORIDAD"]) == "2")
                repor.rutina = "X";
            repor.FECHA_CREACION = Convert.ToDateTime(e.Rows[0]["FECHA_CREACION"]);
            repor.medico = Convert.ToString(e.Rows[0]["medico"]).ToUpper();
            repor.radiologo = Convert.ToString(e.Rows[0]["radiologo"]).ToUpper();
            repor.tecnologo = Convert.ToString(e.Rows[0]["PAC_IDENTIFICACION"]).ToUpper();
            repor.cod_medico = Convert.ToString(e.Rows[0]["COD_medico"]).Substring(0, 10);
            repor.cod_tecnologo = Convert.ToString(e.Rows[0]["COD_tecn"]);
            repor.cod_radiologo = Convert.ToString(e.Rows[0]["COD_rad"]);

            repor.motivo = Convert.ToString(e.Rows[0]["motivo"]);
            repor.resumen_clinico = Convert.ToString(e.Rows[0]["resumen_clinico"]);
            if (Convert.ToInt32(e.Rows[0]["estado_movimiento"]) == 1)
                repor.estado_movimiento = "X";
            if (Convert.ToInt32(e.Rows[0]["estado_retirarsevendas"]) == 1)
                repor.estado_retirarsevendas = "X";
            if (Convert.ToInt32(e.Rows[0]["estado_medicopresente"]) == 1)
                repor.estado_medicopresente = "X";
            if (Convert.ToInt32(e.Rows[0]["estado_encama"]) == 1)
                repor.estado_encama = "X";
            DataTable rub = NegImagen.getReporteRubros(idImagenologia);
            string auxRubros = "";
            foreach (DataRow row in rub.Rows)
            {
                auxRubros += (row["RUB_NOMBRE"].ToString().PadRight(18)).PadLeft(22);
                if (row["vez"].ToString() == "2")
                    auxRubros += "X";
                else
                    auxRubros += " ";
            }
            repor.rubros = auxRubros;

            DataTable estudios = NegImagen.getPedidoEstudios(idImagenologia);
            auxRubros = "";
            foreach (DataRow row in estudios.Rows)
            {
                auxRubros += row[1].ToString() + ".      ";
            }
            repor.estudios = auxRubros;

            return repor;
        }

        private List<PedidoImagen_reporteDiagnosticos> empaquetarReporteDx(int idImagenologia)
        {
            List<PedidoImagen_reporteDiagnosticos> ListDx = new List<PedidoImagen_reporteDiagnosticos>();
            DataTable diagnosticos = NegImagen.getPedidoDiagnosticos(idImagenologia);
            foreach (DataRow row in diagnosticos.Rows)
            {
                PedidoImagen_reporteDiagnosticos dx = new PedidoImagen_reporteDiagnosticos();
                dx.diagnostico = row[1].ToString();
                dx.CIE = row[0].ToString();
                dx.id_imagenologia = idImagenologia.ToString();
                if ((row[2].ToString()) == "True")
                    dx.definitivo = "X";
                else
                    dx.presuntivo = "X";
                ListDx.Add(dx);
            }

            return ListDx;
        }


        private void ImprimirReporte(int idImagenologia)
        {
            try
            {
                ReportesHistoriaClinica imagenL = new ReportesHistoriaClinica();
                imagenL.limpiarImagenologia();
                ReportesHistoriaClinica imagen = new ReportesHistoriaClinica();
                imagen.ingresarImagenologia(empaquetarReporteCabecera(idImagenologia));
                
                List<PedidoImagen_reporteDiagnosticos> ListDx = empaquetarReporteDx(idImagenologia);

                ReportesHistoriaClinica imagend = new ReportesHistoriaClinica();
                imagend.limpiarImagenologiaDiagnostico();
                               
                foreach(var dx in ListDx)
                {
                    ReportesHistoriaClinica imagendx = new ReportesHistoriaClinica();
                    imagendx.ingresarImagenologiaDiagnostico(dx);
                }

                frmReportes ventana = new frmReportes(1, "imagenologia");
                ventana.Show();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimirIndividual_Click(object sender, EventArgs e)
        {
            if (gridSol.RowCount > 0)
            {
                //ImprimirReporte(Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["id"].Value.ToString()));//Reporte nuevo Mario 23-01-2023
                imprimir(Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["id"].Value.ToString()));
            }
        }

        private void btnImprimirResumen_Click(object sender, EventArgs e)
        {
            if (gridSol.RowCount > 0)
            {
                ImprimirReporteResumen();
            }
        }

        private void btnAddTecnologo_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
                cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), txtTecnologo, txtCODTecnologo);
        }

        private void btnAddRadiologo_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
                cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), txtRadiologo, txtCODRadiologo);
        }

        private void txtTecnologo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                txtTecnologo.Text = string.Empty;
                txtCODTecnologo.Text = "0";
                e.Handled = true;
            }
        }

        private void txtRadiologo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                txtRadiologo.Text = string.Empty;
                txtCODRadiologo.Text = "0";
                e.Handled = true;
            }
        }

        private void txtMedico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                txtMedico.Text = string.Empty;
                txtMedicoCOD.Text = string.Empty;
                e.Handled = true;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Imagen));
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridEstudios = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESTUDIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODSUB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOTA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.btnAgregaEstudio = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk3 = new System.Windows.Forms.CheckBox();
            this.chk4 = new System.Windows.Forms.CheckBox();
            this.chk2 = new System.Windows.Forms.CheckBox();
            this.chk1 = new System.Windows.Forms.CheckBox();
            this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.txtResumen = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
            this.txtMotivo = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnAddDiagnotico = new System.Windows.Forms.Button();
            this.gridDiagnosticos = new System.Windows.Forms.DataGridView();
            this.CIE10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DIAGNOSTICO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIPO = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
            this.txt_concluciones = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gridSol = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRadiologo = new System.Windows.Forms.TextBox();
            this.txtCODRadiologo = new System.Windows.Forms.TextBox();
            this.txtTecnologo = new System.Windows.Forms.TextBox();
            this.txtCODTecnologo = new System.Windows.Forms.TextBox();
            this.txtMedico = new System.Windows.Forms.TextBox();
            this.txtMedicoCOD = new System.Windows.Forms.TextBox();
            this.btnAddRadiologo = new System.Windows.Forms.Button();
            this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
            this.btnAddTecnologo = new System.Windows.Forms.Button();
            this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
            this.btnAddMedico = new System.Windows.Forms.Button();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.optControl = new System.Windows.Forms.RadioButton();
            this.optRutina = new System.Windows.Forms.RadioButton();
            this.optUrgente = new System.Windows.Forms.RadioButton();
            this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.dtpFechaNota = new System.Windows.Forms.DateTimePicker();
            this.utcEvolucion = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tools = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnImprimirResumen = new System.Windows.Forms.ToolStripMenuItem();
            this.btnImprimirIndividual = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSolicitudActiva = new System.Windows.Forms.TextBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ultraTabPageControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEstudios)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.ultraTabPageControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtResumen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMotivo)).BeginInit();
            this.ultraTabPageControl3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDiagnosticos)).BeginInit();
            this.ultraTabPageControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_concluciones)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.utcEvolucion)).BeginInit();
            this.utcEvolucion.SuspendLayout();
            this.tools.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraTabPageControl1
            // 
            this.ultraTabPageControl1.Controls.Add(this.groupBox1);
            this.ultraTabPageControl1.Controls.Add(this.groupBox2);
            this.ultraTabPageControl1.Location = new System.Drawing.Point(1, 23);
            this.ultraTabPageControl1.Name = "ultraTabPageControl1";
            this.ultraTabPageControl1.Size = new System.Drawing.Size(790, 455);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.gridEstudios);
            this.groupBox1.Controls.Add(this.ultraLabel2);
            this.groupBox1.Controls.Add(this.btnAgregaEstudio);
            this.groupBox1.Location = new System.Drawing.Point(14, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(765, 394);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            // 
            // gridEstudios
            // 
            this.gridEstudios.AllowUserToAddRows = false;
            this.gridEstudios.AllowUserToOrderColumns = true;
            this.gridEstudios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridEstudios.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.gridEstudios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridEstudios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ESTUDIO,
            this.CODSUB,
            this.NOTA});
            this.gridEstudios.Location = new System.Drawing.Point(20, 50);
            this.gridEstudios.Name = "gridEstudios";
            this.gridEstudios.RowHeadersWidth = 62;
            this.gridEstudios.Size = new System.Drawing.Size(684, 333);
            this.gridEstudios.TabIndex = 34;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 8;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 50;
            // 
            // ESTUDIO
            // 
            this.ESTUDIO.HeaderText = "ESTUDIO";
            this.ESTUDIO.MinimumWidth = 8;
            this.ESTUDIO.Name = "ESTUDIO";
            this.ESTUDIO.ReadOnly = true;
            this.ESTUDIO.Width = 250;
            // 
            // CODSUB
            // 
            this.CODSUB.HeaderText = "CODSUB";
            this.CODSUB.MinimumWidth = 8;
            this.CODSUB.Name = "CODSUB";
            this.CODSUB.ReadOnly = true;
            this.CODSUB.Visible = false;
            this.CODSUB.Width = 150;
            // 
            // NOTA
            // 
            this.NOTA.HeaderText = "NOTA";
            this.NOTA.MaxInputLength = 20;
            this.NOTA.MinimumWidth = 8;
            this.NOTA.Name = "NOTA";
            this.NOTA.Width = 170;
            // 
            // ultraLabel2
            // 
            this.ultraLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel2.Location = new System.Drawing.Point(20, 24);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(61, 20);
            this.ultraLabel2.TabIndex = 33;
            this.ultraLabel2.Text = "Estudios :";
            // 
            // btnAgregaEstudio
            // 
            this.btnAgregaEstudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregaEstudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregaEstudio.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregaEstudio.Image")));
            this.btnAgregaEstudio.Location = new System.Drawing.Point(710, 50);
            this.btnAgregaEstudio.Name = "btnAgregaEstudio";
            this.btnAgregaEstudio.Size = new System.Drawing.Size(49, 40);
            this.btnAgregaEstudio.TabIndex = 2;
            this.btnAgregaEstudio.UseVisualStyleBackColor = true;
            this.btnAgregaEstudio.Click += new System.EventHandler(this.btnAgregaEstudio_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.chk3);
            this.groupBox2.Controls.Add(this.chk4);
            this.groupBox2.Controls.Add(this.chk2);
            this.groupBox2.Controls.Add(this.chk1);
            this.groupBox2.Location = new System.Drawing.Point(16, 397);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(767, 55);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            // 
            // chk3
            // 
            this.chk3.AutoSize = true;
            this.chk3.Location = new System.Drawing.Point(20, 32);
            this.chk3.Name = "chk3";
            this.chk3.Size = new System.Drawing.Size(160, 17);
            this.chk3.TabIndex = 3;
            this.chk3.Text = "Toma radiografia en la cama";
            this.chk3.UseVisualStyleBackColor = true;
            // 
            // chk4
            // 
            this.chk4.AutoSize = true;
            this.chk4.Location = new System.Drawing.Point(212, 32);
            this.chk4.Name = "chk4";
            this.chk4.Size = new System.Drawing.Size(214, 17);
            this.chk4.TabIndex = 2;
            this.chk4.Text = "El medico estara presente en el examen";
            this.chk4.UseVisualStyleBackColor = true;
            // 
            // chk2
            // 
            this.chk2.AutoSize = true;
            this.chk2.Location = new System.Drawing.Point(212, 10);
            this.chk2.Name = "chk2";
            this.chk2.Size = new System.Drawing.Size(219, 17);
            this.chk2.TabIndex = 1;
            this.chk2.Text = "Puede retirarse vendas, apositos o yesos";
            this.chk2.UseVisualStyleBackColor = true;
            // 
            // chk1
            // 
            this.chk1.AutoSize = true;
            this.chk1.Location = new System.Drawing.Point(20, 10);
            this.chk1.Name = "chk1";
            this.chk1.Size = new System.Drawing.Size(112, 17);
            this.chk1.TabIndex = 0;
            this.chk1.Text = "Puede Movilizarse";
            this.chk1.UseVisualStyleBackColor = true;
            // 
            // ultraTabPageControl2
            // 
            this.ultraTabPageControl2.Controls.Add(this.txtResumen);
            this.ultraTabPageControl2.Controls.Add(this.ultraLabel6);
            this.ultraTabPageControl2.Controls.Add(this.txtMotivo);
            this.ultraTabPageControl2.Controls.Add(this.ultraLabel4);
            this.ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabPageControl2.Name = "ultraTabPageControl2";
            this.ultraTabPageControl2.Size = new System.Drawing.Size(790, 455);
            // 
            // txtResumen
            // 
            this.txtResumen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance5.BackColor = System.Drawing.Color.White;
            this.txtResumen.Appearance = appearance5;
            this.txtResumen.BackColor = System.Drawing.Color.White;
            this.txtResumen.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded1;
            this.txtResumen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResumen.Location = new System.Drawing.Point(16, 125);
            this.txtResumen.MaxLength = 499;
            this.txtResumen.Multiline = true;
            this.txtResumen.Name = "txtResumen";
            this.txtResumen.Size = new System.Drawing.Size(771, 342);
            this.txtResumen.TabIndex = 30;
            this.txtResumen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtResumen_KeyDown);
            // 
            // ultraLabel6
            // 
            appearance2.BackColor = System.Drawing.Color.Transparent;
            appearance2.BackColor2 = System.Drawing.Color.Transparent;
            this.ultraLabel6.Appearance = appearance2;
            this.ultraLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel6.Location = new System.Drawing.Point(16, 104);
            this.ultraLabel6.Name = "ultraLabel6";
            this.ultraLabel6.Size = new System.Drawing.Size(136, 18);
            this.ultraLabel6.TabIndex = 29;
            this.ultraLabel6.Text = "Resumen clinico:";
            // 
            // txtMotivo
            // 
            this.txtMotivo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance3.BackColor = System.Drawing.Color.White;
            this.txtMotivo.Appearance = appearance3;
            this.txtMotivo.BackColor = System.Drawing.Color.White;
            this.txtMotivo.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded1;
            this.txtMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMotivo.Location = new System.Drawing.Point(16, 38);
            this.txtMotivo.MaxLength = 253;
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(771, 66);
            this.txtMotivo.TabIndex = 28;
            this.txtMotivo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMotivo_KeyDown);
            // 
            // ultraLabel4
            // 
            appearance7.BackColor = System.Drawing.Color.Transparent;
            appearance7.BackColor2 = System.Drawing.Color.Transparent;
            this.ultraLabel4.Appearance = appearance7;
            this.ultraLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel4.Location = new System.Drawing.Point(16, 14);
            this.ultraLabel4.Name = "ultraLabel4";
            this.ultraLabel4.Size = new System.Drawing.Size(136, 18);
            this.ultraLabel4.TabIndex = 27;
            this.ultraLabel4.Text = "Motivo de la solicitud:";
            // 
            // ultraTabPageControl3
            // 
            this.ultraTabPageControl3.Controls.Add(this.groupBox3);
            this.ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabPageControl3.Name = "ultraTabPageControl3";
            this.ultraTabPageControl3.Size = new System.Drawing.Size(790, 455);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.btnAddDiagnotico);
            this.groupBox3.Controls.Add(this.gridDiagnosticos);
            this.groupBox3.Controls.Add(this.ultraLabel7);
            this.groupBox3.Location = new System.Drawing.Point(16, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(834, 435);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            // 
            // btnAddDiagnotico
            // 
            this.btnAddDiagnotico.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddDiagnotico.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDiagnotico.Image = ((System.Drawing.Image)(resources.GetObject("btnAddDiagnotico.Image")));
            this.btnAddDiagnotico.Location = new System.Drawing.Point(717, 50);
            this.btnAddDiagnotico.Name = "btnAddDiagnotico";
            this.btnAddDiagnotico.Size = new System.Drawing.Size(48, 40);
            this.btnAddDiagnotico.TabIndex = 35;
            this.btnAddDiagnotico.UseVisualStyleBackColor = true;
            this.btnAddDiagnotico.Click += new System.EventHandler(this.btnAddDiagnotico_Click_1);
            // 
            // gridDiagnosticos
            // 
            this.gridDiagnosticos.AllowUserToAddRows = false;
            this.gridDiagnosticos.AllowUserToOrderColumns = true;
            this.gridDiagnosticos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDiagnosticos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.gridDiagnosticos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDiagnosticos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CIE10,
            this.DIAGNOSTICO,
            this.TIPO});
            this.gridDiagnosticos.Location = new System.Drawing.Point(20, 50);
            this.gridDiagnosticos.Name = "gridDiagnosticos";
            this.gridDiagnosticos.RowHeadersWidth = 62;
            this.gridDiagnosticos.Size = new System.Drawing.Size(691, 366);
            this.gridDiagnosticos.TabIndex = 34;
            // 
            // CIE10
            // 
            this.CIE10.HeaderText = "CIE10";
            this.CIE10.MinimumWidth = 8;
            this.CIE10.Name = "CIE10";
            this.CIE10.ReadOnly = true;
            this.CIE10.Width = 150;
            // 
            // DIAGNOSTICO
            // 
            this.DIAGNOSTICO.HeaderText = "DIAGNOSTICO";
            this.DIAGNOSTICO.MinimumWidth = 8;
            this.DIAGNOSTICO.Name = "DIAGNOSTICO";
            this.DIAGNOSTICO.ReadOnly = true;
            this.DIAGNOSTICO.Width = 230;
            // 
            // TIPO
            // 
            this.TIPO.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.TIPO.DisplayStyleForCurrentCellOnly = true;
            this.TIPO.HeaderText = "TIPO";
            this.TIPO.Items.AddRange(new object[] {
            "PRESUNTIVO",
            "DEFINITIVO"});
            this.TIPO.MinimumWidth = 8;
            this.TIPO.Name = "TIPO";
            this.TIPO.Width = 120;
            // 
            // ultraLabel7
            // 
            this.ultraLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel7.Location = new System.Drawing.Point(20, 24);
            this.ultraLabel7.Name = "ultraLabel7";
            this.ultraLabel7.Size = new System.Drawing.Size(150, 20);
            this.ultraLabel7.TabIndex = 33;
            this.ultraLabel7.Text = "Diagnostico(s):";
            // 
            // ultraTabPageControl4
            // 
            this.ultraTabPageControl4.Controls.Add(this.ultraLabel10);
            this.ultraTabPageControl4.Controls.Add(this.txt_concluciones);
            this.ultraTabPageControl4.Enabled = false;
            this.ultraTabPageControl4.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabPageControl4.Name = "ultraTabPageControl4";
            this.ultraTabPageControl4.Size = new System.Drawing.Size(790, 455);
            // 
            // ultraLabel10
            // 
            appearance4.BackColor = System.Drawing.Color.Transparent;
            appearance4.BackColor2 = System.Drawing.Color.Transparent;
            this.ultraLabel10.Appearance = appearance4;
            this.ultraLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel10.Location = new System.Drawing.Point(10, 17);
            this.ultraLabel10.Name = "ultraLabel10";
            this.ultraLabel10.Size = new System.Drawing.Size(136, 18);
            this.ultraLabel10.TabIndex = 32;
            this.ultraLabel10.Text = "Conclusiones:";
            // 
            // txt_concluciones
            // 
            this.txt_concluciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.White;
            this.txt_concluciones.Appearance = appearance1;
            this.txt_concluciones.BackColor = System.Drawing.Color.White;
            this.txt_concluciones.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded1;
            this.txt_concluciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_concluciones.Location = new System.Drawing.Point(10, 41);
            this.txt_concluciones.MaxLength = 499;
            this.txt_concluciones.Multiline = true;
            this.txt_concluciones.Name = "txt_concluciones";
            this.txt_concluciones.Size = new System.Drawing.Size(771, 401);
            this.txt_concluciones.TabIndex = 31;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 48);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gridSol);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtRadiologo);
            this.splitContainer1.Panel2.Controls.Add(this.txtCODRadiologo);
            this.splitContainer1.Panel2.Controls.Add(this.txtTecnologo);
            this.splitContainer1.Panel2.Controls.Add(this.txtCODTecnologo);
            this.splitContainer1.Panel2.Controls.Add(this.txtMedico);
            this.splitContainer1.Panel2.Controls.Add(this.txtMedicoCOD);
            this.splitContainer1.Panel2.Controls.Add(this.btnAddRadiologo);
            this.splitContainer1.Panel2.Controls.Add(this.ultraLabel9);
            this.splitContainer1.Panel2.Controls.Add(this.btnAddTecnologo);
            this.splitContainer1.Panel2.Controls.Add(this.ultraLabel8);
            this.splitContainer1.Panel2.Controls.Add(this.btnAddMedico);
            this.splitContainer1.Panel2.Controls.Add(this.ultraLabel1);
            this.splitContainer1.Panel2.Controls.Add(this.optControl);
            this.splitContainer1.Panel2.Controls.Add(this.optRutina);
            this.splitContainer1.Panel2.Controls.Add(this.optUrgente);
            this.splitContainer1.Panel2.Controls.Add(this.ultraLabel5);
            this.splitContainer1.Panel2.Controls.Add(this.ultraLabel3);
            this.splitContainer1.Panel2.Controls.Add(this.dtpFechaNota);
            this.splitContainer1.Panel2.Controls.Add(this.utcEvolucion);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(1011, 592);
            this.splitContainer1.SplitterDistance = 188;
            this.splitContainer1.TabIndex = 36;
            // 
            // gridSol
            // 
            this.gridSol.AllowUserToAddRows = false;
            this.gridSol.AllowUserToDeleteRows = false;
            this.gridSol.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSol.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridSol.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridSol.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridSol.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridSol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSol.ColumnHeadersVisible = false;
            this.gridSol.GridColor = System.Drawing.SystemColors.Window;
            this.gridSol.Location = new System.Drawing.Point(13, 33);
            this.gridSol.Name = "gridSol";
            this.gridSol.ReadOnly = true;
            this.gridSol.RowHeadersVisible = false;
            this.gridSol.RowHeadersWidth = 62;
            this.gridSol.Size = new System.Drawing.Size(160, 534);
            this.gridSol.TabIndex = 2;
            this.gridSol.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSol_CellContentClick);
            this.gridSol.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSol_CellDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "SOLICITUDES:";
            // 
            // txtRadiologo
            // 
            this.txtRadiologo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRadiologo.Location = new System.Drawing.Point(368, 74);
            this.txtRadiologo.Name = "txtRadiologo";
            this.txtRadiologo.ReadOnly = true;
            this.txtRadiologo.Size = new System.Drawing.Size(439, 20);
            this.txtRadiologo.TabIndex = 46;
            this.txtRadiologo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRadiologo_KeyDown);
            // 
            // txtCODRadiologo
            // 
            this.txtCODRadiologo.Location = new System.Drawing.Point(725, 7);
            this.txtCODRadiologo.Name = "txtCODRadiologo";
            this.txtCODRadiologo.Size = new System.Drawing.Size(38, 20);
            this.txtCODRadiologo.TabIndex = 45;
            this.txtCODRadiologo.Text = "0";
            this.txtCODRadiologo.Visible = false;
            // 
            // txtTecnologo
            // 
            this.txtTecnologo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTecnologo.Location = new System.Drawing.Point(368, 51);
            this.txtTecnologo.Name = "txtTecnologo";
            this.txtTecnologo.ReadOnly = true;
            this.txtTecnologo.Size = new System.Drawing.Size(439, 20);
            this.txtTecnologo.TabIndex = 44;
            this.txtTecnologo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTecnologo_KeyDown);
            // 
            // txtCODTecnologo
            // 
            this.txtCODTecnologo.Location = new System.Drawing.Point(769, 7);
            this.txtCODTecnologo.Name = "txtCODTecnologo";
            this.txtCODTecnologo.Size = new System.Drawing.Size(38, 20);
            this.txtCODTecnologo.TabIndex = 43;
            this.txtCODTecnologo.Text = "0";
            this.txtCODTecnologo.Visible = false;
            // 
            // txtMedico
            // 
            this.txtMedico.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMedico.Location = new System.Drawing.Point(368, 28);
            this.txtMedico.Name = "txtMedico";
            this.txtMedico.ReadOnly = true;
            this.txtMedico.Size = new System.Drawing.Size(439, 20);
            this.txtMedico.TabIndex = 42;
            this.txtMedico.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMedico_KeyDown);
            // 
            // txtMedicoCOD
            // 
            this.txtMedicoCOD.Location = new System.Drawing.Point(681, 7);
            this.txtMedicoCOD.Name = "txtMedicoCOD";
            this.txtMedicoCOD.Size = new System.Drawing.Size(38, 20);
            this.txtMedicoCOD.TabIndex = 41;
            this.txtMedicoCOD.Visible = false;
            // 
            // btnAddRadiologo
            // 
            this.btnAddRadiologo.Location = new System.Drawing.Point(334, 72);
            this.btnAddRadiologo.Name = "btnAddRadiologo";
            this.btnAddRadiologo.Size = new System.Drawing.Size(28, 22);
            this.btnAddRadiologo.TabIndex = 40;
            this.btnAddRadiologo.Text = "...";
            this.btnAddRadiologo.UseVisualStyleBackColor = true;
            this.btnAddRadiologo.Click += new System.EventHandler(this.btnAddRadiologo_Click);
            // 
            // ultraLabel9
            // 
            this.ultraLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel9.Location = new System.Drawing.Point(273, 79);
            this.ultraLabel9.Name = "ultraLabel9";
            this.ultraLabel9.Size = new System.Drawing.Size(66, 16);
            this.ultraLabel9.TabIndex = 39;
            this.ultraLabel9.Text = "Radiólogo:";
            // 
            // btnAddTecnologo
            // 
            this.btnAddTecnologo.Location = new System.Drawing.Point(334, 50);
            this.btnAddTecnologo.Name = "btnAddTecnologo";
            this.btnAddTecnologo.Size = new System.Drawing.Size(28, 22);
            this.btnAddTecnologo.TabIndex = 37;
            this.btnAddTecnologo.Text = "...";
            this.btnAddTecnologo.UseVisualStyleBackColor = true;
            this.btnAddTecnologo.Click += new System.EventHandler(this.btnAddTecnologo_Click);
            // 
            // ultraLabel8
            // 
            this.ultraLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel8.Location = new System.Drawing.Point(273, 57);
            this.ultraLabel8.Name = "ultraLabel8";
            this.ultraLabel8.Size = new System.Drawing.Size(66, 15);
            this.ultraLabel8.TabIndex = 36;
            this.ultraLabel8.Text = "Tecnólogo:";
            // 
            // btnAddMedico
            // 
            this.btnAddMedico.Location = new System.Drawing.Point(334, 28);
            this.btnAddMedico.Name = "btnAddMedico";
            this.btnAddMedico.Size = new System.Drawing.Size(28, 22);
            this.btnAddMedico.TabIndex = 33;
            this.btnAddMedico.Text = "...";
            this.btnAddMedico.UseVisualStyleBackColor = true;
            this.btnAddMedico.Click += new System.EventHandler(this.button3_Click);
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel1.Location = new System.Drawing.Point(30, 67);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(54, 17);
            this.ultraLabel1.TabIndex = 32;
            this.ultraLabel1.Text = "Prioridad:";
            // 
            // optControl
            // 
            this.optControl.AutoSize = true;
            this.optControl.Checked = true;
            this.optControl.Location = new System.Drawing.Point(211, 65);
            this.optControl.Name = "optControl";
            this.optControl.Size = new System.Drawing.Size(58, 17);
            this.optControl.TabIndex = 13;
            this.optControl.TabStop = true;
            this.optControl.Text = "Control";
            this.optControl.UseVisualStyleBackColor = true;
            // 
            // optRutina
            // 
            this.optRutina.AutoSize = true;
            this.optRutina.Location = new System.Drawing.Point(149, 65);
            this.optRutina.Name = "optRutina";
            this.optRutina.Size = new System.Drawing.Size(56, 17);
            this.optRutina.TabIndex = 12;
            this.optRutina.Text = "Rutina";
            this.optRutina.UseVisualStyleBackColor = true;
            // 
            // optUrgente
            // 
            this.optUrgente.AutoSize = true;
            this.optUrgente.Location = new System.Drawing.Point(87, 65);
            this.optUrgente.Name = "optUrgente";
            this.optUrgente.Size = new System.Drawing.Size(63, 17);
            this.optUrgente.TabIndex = 11;
            this.optUrgente.Text = "Urgente";
            this.optUrgente.UseVisualStyleBackColor = true;
            // 
            // ultraLabel5
            // 
            this.ultraLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel5.Location = new System.Drawing.Point(273, 35);
            this.ultraLabel5.Name = "ultraLabel5";
            this.ultraLabel5.Size = new System.Drawing.Size(66, 14);
            this.ultraLabel5.TabIndex = 31;
            this.ultraLabel5.Text = "Médico :";
            // 
            // ultraLabel3
            // 
            appearance6.BackColor = System.Drawing.Color.Transparent;
            appearance6.BackColor2 = System.Drawing.Color.Transparent;
            this.ultraLabel3.Appearance = appearance6;
            this.ultraLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel3.Location = new System.Drawing.Point(31, 33);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(77, 17);
            this.ultraLabel3.TabIndex = 30;
            this.ultraLabel3.Text = "Fecha y Hora:";
            // 
            // dtpFechaNota
            // 
            this.dtpFechaNota.CustomFormat = "dd-MM-yyyy hh:mm";
            this.dtpFechaNota.Enabled = false;
            this.dtpFechaNota.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaNota.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaNota.Location = new System.Drawing.Point(125, 30);
            this.dtpFechaNota.Name = "dtpFechaNota";
            this.dtpFechaNota.Size = new System.Drawing.Size(132, 20);
            this.dtpFechaNota.TabIndex = 28;
            // 
            // utcEvolucion
            // 
            this.utcEvolucion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.utcEvolucion.Controls.Add(this.ultraTabSharedControlsPage1);
            this.utcEvolucion.Controls.Add(this.ultraTabPageControl1);
            this.utcEvolucion.Controls.Add(this.ultraTabPageControl2);
            this.utcEvolucion.Controls.Add(this.ultraTabPageControl3);
            this.utcEvolucion.Controls.Add(this.ultraTabPageControl4);
            this.utcEvolucion.Location = new System.Drawing.Point(13, 102);
            this.utcEvolucion.Name = "utcEvolucion";
            this.utcEvolucion.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.utcEvolucion.Size = new System.Drawing.Size(794, 481);
            this.utcEvolucion.TabIndex = 27;
            ultraTab1.Key = "datos";
            ultraTab1.TabPage = this.ultraTabPageControl1;
            ultraTab1.Text = "Estudio Solicitado";
            ultraTab2.Key = "resumen";
            ultraTab2.TabPage = this.ultraTabPageControl2;
            ultraTab2.Text = "Motivo - Resumen Clinico";
            ultraTab3.Key = "historial";
            ultraTab3.TabPage = this.ultraTabPageControl3;
            ultraTab3.Text = "Diagnostico";
            ultraTab4.Key = "conclusiones";
            ultraTab4.TabPage = this.ultraTabPageControl4;
            ultraTab4.Text = "Concluciones";
            ultraTab4.Visible = false;
            this.utcEvolucion.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1,
            ultraTab2,
            ultraTab3,
            ultraTab4});
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(790, 455);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "DETALLE:";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "DIAGNOSTICO";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 230;
            // 
            // tools
            // 
            this.tools.AutoSize = false;
            this.tools.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnModificar,
            this.btnGuardar,
            this.btnCancelar,
            this.toolStripSeparator1,
            this.btnImprimir});
            this.tools.Location = new System.Drawing.Point(0, 0);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(1011, 45);
            this.tools.TabIndex = 37;
            this.tools.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.AutoSize = false;
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(42, 42);
            this.btnNuevo.Text = "toolStripButton1";
            this.btnNuevo.ToolTipText = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.AutoSize = false;
            this.btnModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
            this.btnModificar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(42, 42);
            this.btnModificar.Text = "toolStripButton1";
            this.btnModificar.ToolTipText = "Modificar";
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.AutoSize = false;
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(42, 42);
            this.btnGuardar.Text = "toolStripButton1";
            this.btnGuardar.ToolTipText = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoSize = false;
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(42, 42);
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // btnImprimir
            // 
            this.btnImprimir.AutoSize = false;
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnImprimirResumen,
            this.btnImprimirIndividual});
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(42, 42);
            this.btnImprimir.Text = "toolStripButton1";
            this.btnImprimir.ToolTipText = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnImprimirResumen
            // 
            this.btnImprimirResumen.Name = "btnImprimirResumen";
            this.btnImprimirResumen.Size = new System.Drawing.Size(134, 22);
            this.btnImprimirResumen.Text = "Resumen";
            this.btnImprimirResumen.Click += new System.EventHandler(this.btnImprimirResumen_Click);
            // 
            // btnImprimirIndividual
            // 
            this.btnImprimirIndividual.Name = "btnImprimirIndividual";
            this.btnImprimirIndividual.Size = new System.Drawing.Size(134, 22);
            this.btnImprimirIndividual.Text = "Form. 012A";
            this.btnImprimirIndividual.Click += new System.EventHandler(this.btnImprimirIndividual_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(0, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1011, 10);
            this.panel1.TabIndex = 35;
            // 
            // txtSolicitudActiva
            // 
            this.txtSolicitudActiva.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtSolicitudActiva.Location = new System.Drawing.Point(658, 4);
            this.txtSolicitudActiva.Name = "txtSolicitudActiva";
            this.txtSolicitudActiva.ReadOnly = true;
            this.txtSolicitudActiva.Size = new System.Drawing.Size(59, 38);
            this.txtSolicitudActiva.TabIndex = 38;
            this.txtSolicitudActiva.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "ESTUDIO";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "NOTA";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 20;
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            this.dataGridViewTextBoxColumn3.Width = 170;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "CIE10";
            this.dataGridViewTextBoxColumn4.MaxInputLength = 20;
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 170;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "DIAGNOSTICO";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 230;
            // 
            // frm_Imagen
            // 
            this.ClientSize = new System.Drawing.Size(1011, 641);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tools);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtSolicitudActiva);
            this.Name = "frm_Imagen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IMAGENOLOGIA";
            this.Load += new System.EventHandler(this.frm_Imagen_Load);
            this.ultraTabPageControl1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridEstudios)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ultraTabPageControl2.ResumeLayout(false);
            this.ultraTabPageControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtResumen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMotivo)).EndInit();
            this.ultraTabPageControl3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDiagnosticos)).EndInit();
            this.ultraTabPageControl4.ResumeLayout(false);
            this.ultraTabPageControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_concluciones)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.utcEvolucion)).EndInit();
            this.utcEvolucion.ResumeLayout(false);
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void gridSol_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Desea cargar la atencion.?", "HIS3000", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                controles_reset();
                txtSolicitudActiva.Text = gridSol.Rows[gridSol.CurrentRow.Index].Cells["id"].Value.ToString();
                //controles_prepararedicion();
                cargarPedido(Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["id"].Value));
                controles_visualizaPedido();
                NegAtenciones atenciones = new NegAtenciones();
                string estado = atenciones.EstadoCuenta(Convert.ToString(nAtencion));

                int ingreso = NegTipoIngreso.RecuperarporAtencion(nAtencion);
                if (ingreso == 10)
                    mushuñan = true;
                else
                    mushuñan = false;
                if(estado != "1")
                {
                    Bloquear();
                }
                ValidarEnfermeria();
                //do something
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void btnAddDiagnotico_Click_1(object sender, EventArgs e)
        {
            //frm_ImagenAyuda ayuda = new frm_ImagenAyuda("DIAGNOSTICOS");
            //ayuda.ShowDialog();
            //if (ayuda.codigo != string.Empty)
            //{
            //    if (!BuscarItem(ayuda.codigo, gridDiagnosticos))
            //        this.gridDiagnosticos.Rows.Add(ayuda.codigo, ayuda.producto, "PRESUNTIVO");
            //    else
            //        MessageBox.Show("El item ya fue ingresado.");
            //}
            frm_BusquedaCIE10 x = new frm_BusquedaCIE10();
            x.ShowDialog();
            if(x.codigo != string.Empty)
            {
                if (!BuscarItem(x.codigo, gridDiagnosticos))
                    this.gridDiagnosticos.Rows.Add(x.codigo, x.resultado, "PRESUNTIVO");
                else
                    MessageBox.Show("El item ya fue ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gridSol_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        public void ValidarEnfermeria()
        {
            //CAmbios Edgar 20210303 Requerimientos de la pasteur por Alex
            if(Sesion.codDepartamento == 6)
            {
                btnModificar.Enabled = false;
                btnNuevo.Enabled = false;
                btnCancelar.Enabled = false;
                btnImprimir.Enabled = false;
                btnGuardar.Enabled = false;
            }
        }
        private void frm_Imagen_Load(object sender, EventArgs e)
        {
            ValidarEnfermeria();


        }
    }
}
