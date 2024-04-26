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
    public partial class frm_LaboratorioClinico : Form
    {
        #region Variables
        PACIENTES paciente = new PACIENTES();
        PACIENTES_DATOS_ADICIONALES pac_adicional = new PACIENTES_DATOS_ADICIONALES();
        ATENCIONES ultimaAtencion = new ATENCIONES();
        MEDICOS medico = new MEDICOS();
        MEDICOS _medico = new MEDICOS();
        CATEGORIAS_CONVENIOS seguro = new CATEGORIAS_CONVENIOS();
        TIPO_INGRESO ingreso = new TIPO_INGRESO();
        List<ATENCION_DETALLE_CATEGORIAS> aSeguro = new List<ATENCION_DETALLE_CATEGORIAS>();
        List<HC_LABORATORIO_CLINICO_DETALLE> detalle = new List<HC_LABORATORIO_CLINICO_DETALLE>();
        HC_LABORATORIO_CLINICO laboratorio = new HC_LABORATORIO_CLINICO();
        public bool Editar = false;
        public Int64 lclCodigo;
        List<DtoLaboratorioVarios> lab1 = new List<DtoLaboratorioVarios>();
        List<DtoLaboratorioVarios> lab2 = new List<DtoLaboratorioVarios>();
        List<DtoLaboratorioVarios> labMas = new List<DtoLaboratorioVarios>();
        #endregion

        public frm_LaboratorioClinico()
        {
            InitializeComponent();
        }
        public frm_LaboratorioClinico(Int64 ate_codigo, bool estado)
        {
            InitializeComponent();
            //datos del paciente
            CargarAtencionPaciente(ate_codigo);
            listarPerfiles();
        }
        public void CargarAtencionPaciente(Int64 ate_codigo)
        {
            paciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));
            ultimaAtencion = NegAtenciones.RecuperarAtencionID(ate_codigo);
            medico = NegMedicos.recuperarMedico(Convert.ToInt32(ultimaAtencion.MEDICOSReference.EntityKey.EntityKeyValues[0].Value));
            aSeguro = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(ultimaAtencion.ATE_CODIGO);
            pac_adicional = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(paciente.PAC_CODIGO);
            ingreso = NegAtenciones.RecuperaTipoIngresoCodigoAtencion(ate_codigo);
            HABITACIONES hab = new HABITACIONES();
            hab = NegHabitaciones.RecuperarHabitacionId(Convert.ToInt16(ultimaAtencion.HABITACIONESReference.EntityKey.EntityKeyValues[0].Value));

            lblPaciente.Text = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO + " " + paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
            if (paciente.PAC_GENERO == "M")
                lblsexo.Text = "MASCULINO";
            else
                lblsexo.Text = "FEMENINO";
            lblmedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
            lblHc.Text = paciente.PAC_HISTORIA_CLINICA.Trim();
            lblatencion.Text = ultimaAtencion.ATE_NUMERO_ATENCION;

            DateTime birthDate = (DateTime)paciente.PAC_FECHA_NACIMIENTO;
            int age = (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.25D);
            lbledad.Text = age.ToString();
            foreach (var item in aSeguro)
            {
                seguro = NegCategorias.RecuperaCategoriaID(Convert.ToInt16(item.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value));
            }
            lblSeguro.Text = seguro.CAT_NOMBRE;
            txtRecibe.Text = lblmedico.Text;
            txtCama.Text = hab.hab_Numero;
            txtServicio.Text = ingreso.TIP_DESCRIPCION;
        }
        public void habilitaBotones(bool nuevo, bool guardar, bool editar, bool imprimir, bool cancelar)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnEditar.Enabled = editar;
            btnImprimir.Enabled = imprimir;
            btnCancelar.Enabled = cancelar;
            panel1.Enabled = guardar;
        }
        private void frm_LaboratorioClinico_Load(object sender, EventArgs e)
        {
            CargarProductos();
            habilitaBotones(true, false, false, false, false);
            refrescarSolicitudes();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CargarProductos()
        {
            List<HC_CATALOGOS_TIPO> tipos = new List<HC_CATALOGOS_TIPO>();
            tipos = NegCatalogos.ListaCatalogos();

            foreach (var item in tipos)
            {
                if (item.HCT_NOMBRE.Contains("HEMATOLOGIA"))
                {
                    cmb_productoH.DataSource = NegLaboratorio.listarProductos(item.HCT_CODIGO);
                    cmb_productoH.DisplayMember = "EXAMEN";
                }
                else if (item.HCT_NOMBRE.Contains("UROANALISIS"))
                {
                    cmb_productoU.DataSource = NegLaboratorio.listarProductos(item.HCT_CODIGO);
                    cmb_productoU.DisplayMember = "EXAMEN";
                }
                else if (item.HCT_NOMBRE.Contains("COPROLOGICO"))
                {
                    cmb_productoC.DataSource = NegLaboratorio.listarProductos(item.HCT_CODIGO);
                    cmb_productoC.DisplayMember = "EXAMEN";
                }
                else if (item.HCT_NOMBRE.Contains("QUIMICA SANGUINEA"))
                {
                    cmb_productoQ.DataSource = NegLaboratorio.listarProductos(item.HCT_CODIGO);
                    cmb_productoQ.DisplayMember = "EXAMEN";
                }
                else if (item.HCT_NOMBRE.Contains("SEROLOGIA"))
                {
                    cmb_productoS.DataSource = NegLaboratorio.listarProductos(item.HCT_CODIGO);
                    cmb_productoS.DisplayMember = "EXAMEN";
                }
                else if (item.HCT_NOMBRE.Contains("BACTERIOLOGIA"))
                {
                    cmb_productoB.DataSource = NegLaboratorio.listarProductos(item.HCT_CODIGO);
                    cmb_productoB.DisplayMember = "EXAMEN";
                }
                else if (item.HCT_NOMBRE.Contains("OTROS"))
                {
                    cmb_productoO.DataSource = NegLaboratorio.listarProductos(item.HCT_CODIGO);
                    cmb_productoO.DisplayMember = "EXAMEN";
                }
            }
        }
        #region VALIDACIONES DE GRID
        private DataGridViewComboBoxEditingControl cboxEdit;
        private void EditComboBox_SelectionChangeCommittedH(object sender, System.EventArgs e)
        {
            // set the value as needed
            List<DtoLaboratorioEstructura> x = new List<DtoLaboratorioEstructura>();
            x = NegLaboratorio.listarProductos(6); //codigo de la tabla hc_catalogos_tipos
            ComboBox combo = sender as ComboBox;
            foreach (var item in x)
            {
                for (int i = 0; i < dgvHematologia.Rows.Count - 1; i++)
                {
                    if (dgvHematologia.Rows[i].Cells[1].FormattedValue.ToString() == combo.Text)
                    {
                        MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        combo.SelectedIndex = -1;
                        return;
                    }
                }
                if (combo.Text == item.EXAMEN)
                {
                    dgvHematologia.CurrentRow.Cells["codPro"].Value = item.COD_PRODUCTO;
                    dgvHematologia.CurrentRow.Cells["codigoarea"].Value = item.CODIGO_AREA;
                    dgvHematologia.CurrentRow.Cells["area"].Value = item.AREA;
                    dgvHematologia.Focus();
                    break;
                }
            }
        }
        private void refrescarSolicitudes()
        {
            gridSol.DataSource = NegImagen.getLaboratorio(ultimaAtencion.ATE_CODIGO);
            gridSol.Columns["ID"].Visible = false;
            gridSol.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            if (gridSol.RowCount > 1)
                habilitaBotones(true, false, false, false, false);
            else
                habilitaBotones(true, false, false, false, false);
        }
        private void dgvHematologia_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvHematologia.CurrentCell.ColumnIndex == 1 && e.Control is DataGridViewComboBoxEditingControl)
            {
                if (cboxEdit != null)
                {
                    cboxEdit.SelectionChangeCommitted -= EditComboBox_SelectionChangeCommittedH;
                }
                cboxEdit = (DataGridViewComboBoxEditingControl)e.Control;
                cboxEdit.SelectionChangeCommitted += EditComboBox_SelectionChangeCommittedH;
            }
        }
        private void EditComboBox_SelectionChangeCommittedU(object sender, System.EventArgs e)
        {
            // set the value as needed
            List<DtoLaboratorioEstructura> x = new List<DtoLaboratorioEstructura>();
            x = NegLaboratorio.listarProductos(7); //codigo de la tabla hc_catalogos_tipo
            ComboBox combo = sender as ComboBox;
            foreach (var item in x)
            {
                for (int i = 0; i < dgvUroanalisis.Rows.Count - 1; i++)
                {
                    if (dgvUroanalisis.Rows[i].Cells[1].FormattedValue.ToString() == combo.Text)
                    {
                        MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        combo.SelectedIndex = -1;
                        return;
                    }
                }
                if (combo.Text == item.EXAMEN)
                {
                    dgvUroanalisis.CurrentRow.Cells["codprou"].Value = item.COD_PRODUCTO;
                    dgvUroanalisis.CurrentRow.Cells["codareau"].Value = item.CODIGO_AREA;
                    dgvUroanalisis.CurrentRow.Cells["areau"].Value = item.AREA;
                    dgvUroanalisis.Focus();
                    break;
                }
            }
        }
        private void dgvUroanalisis_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvUroanalisis.CurrentCell.ColumnIndex == 1 && e.Control is DataGridViewComboBoxEditingControl)
            {
                if (cboxEdit != null)
                {
                    cboxEdit.SelectionChangeCommitted -= EditComboBox_SelectionChangeCommittedU;
                }
                cboxEdit = (DataGridViewComboBoxEditingControl)e.Control;
                cboxEdit.SelectionChangeCommitted += EditComboBox_SelectionChangeCommittedU;
            }
        }
        private void EditComboBox_SelectionChangeCommittedC(object sender, System.EventArgs e)
        {
            // set the value as needed
            List<DtoLaboratorioEstructura> x = new List<DtoLaboratorioEstructura>();
            x = NegLaboratorio.listarProductos(8); //codigo de la tabla hc_catalogos_tipos
            ComboBox combo = sender as ComboBox;
            foreach (var item in x)
            {
                for (int i = 0; i < dvgCoprologico.Rows.Count - 1; i++)
                {
                    if (dvgCoprologico.Rows[i].Cells[1].FormattedValue.ToString() == combo.Text)
                    {
                        MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        combo.SelectedIndex = -1;
                        return;
                    }
                }
                if (combo.Text == item.EXAMEN)
                {
                    dvgCoprologico.CurrentRow.Cells["codproc"].Value = item.COD_PRODUCTO;
                    dvgCoprologico.CurrentRow.Cells["codareac"].Value = item.CODIGO_AREA;
                    dvgCoprologico.CurrentRow.Cells["areac"].Value = item.AREA;
                    dvgCoprologico.Focus();
                    break;
                }
            }
        }
        private void dvgCoprologico_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dvgCoprologico.CurrentCell.ColumnIndex == 1 && e.Control is DataGridViewComboBoxEditingControl)
            {
                if (cboxEdit != null)
                {
                    cboxEdit.SelectionChangeCommitted -= EditComboBox_SelectionChangeCommittedC;
                }
                cboxEdit = (DataGridViewComboBoxEditingControl)e.Control;
                cboxEdit.SelectionChangeCommitted += EditComboBox_SelectionChangeCommittedC;
            }
        }
        private void EditComboBox_SelectionChangeCommittedQ(object sender, System.EventArgs e)
        {
            // set the value as needed
            List<DtoLaboratorioEstructura> x = new List<DtoLaboratorioEstructura>();
            x = NegLaboratorio.listarProductos(9); //codigo de la tabla hc_catalogos_tipos
            ComboBox combo = sender as ComboBox;
            foreach (var item in x)
            {
                for (int i = 0; i < dgvQSanguinea.Rows.Count - 1; i++)
                {
                    if (dgvQSanguinea.Rows[i].Cells[1].FormattedValue.ToString() == combo.Text)
                    {
                        MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        combo.SelectedIndex = -1;
                        return;
                    }
                }
                if (combo.Text == item.EXAMEN)
                {
                    dgvQSanguinea.CurrentRow.Cells["codproq"].Value = item.COD_PRODUCTO;
                    dgvQSanguinea.CurrentRow.Cells["codareaq"].Value = item.CODIGO_AREA;
                    dgvQSanguinea.CurrentRow.Cells["areaq"].Value = item.AREA;
                    dgvQSanguinea.Focus();
                    break;
                }
            }
        }
        private void dgvQSanguinea_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvQSanguinea.CurrentCell.ColumnIndex == 1 && e.Control is DataGridViewComboBoxEditingControl)
            {
                if (cboxEdit != null)
                {
                    cboxEdit.SelectionChangeCommitted -= EditComboBox_SelectionChangeCommittedQ;
                }
                cboxEdit = (DataGridViewComboBoxEditingControl)e.Control;
                cboxEdit.SelectionChangeCommitted += EditComboBox_SelectionChangeCommittedQ;
            }
        }
        private void EditComboBox_SelectionChangeCommittedS(object sender, System.EventArgs e)
        {
            // set the value as needed
            List<DtoLaboratorioEstructura> x = new List<DtoLaboratorioEstructura>();
            x = NegLaboratorio.listarProductos(10); //codigo de la tabla hc_catalogos_tipos
            ComboBox combo = sender as ComboBox;
            foreach (var item in x)
            {
                for (int i = 0; i < dgvSerologia.Rows.Count - 1; i++)
                {
                    if (dgvSerologia.Rows[i].Cells[1].FormattedValue.ToString() == combo.Text)
                    {
                        MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        combo.SelectedIndex = -1;
                        return;
                    }
                }
                if (combo.Text == item.EXAMEN)
                {
                    dgvSerologia.CurrentRow.Cells["codpros"].Value = item.COD_PRODUCTO;
                    dgvSerologia.CurrentRow.Cells["codareas"].Value = item.CODIGO_AREA;
                    dgvSerologia.CurrentRow.Cells["areas"].Value = item.AREA;
                    dgvSerologia.Focus();
                    break;
                }
            }
        }
        private void dgvSerologia_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvSerologia.CurrentCell.ColumnIndex == 1 && e.Control is DataGridViewComboBoxEditingControl)
            {
                if (cboxEdit != null)
                {
                    cboxEdit.SelectionChangeCommitted -= EditComboBox_SelectionChangeCommittedS;
                }
                cboxEdit = (DataGridViewComboBoxEditingControl)e.Control;
                cboxEdit.SelectionChangeCommitted += EditComboBox_SelectionChangeCommittedS;
            }
        }
        private void EditComboBox_SelectionChangeCommittedB(object sender, System.EventArgs e)
        {
            // set the value as needed
            List<DtoLaboratorioEstructura> x = new List<DtoLaboratorioEstructura>();
            x = NegLaboratorio.listarProductos(11); //codigo de la tabla hc_catalogos_tipos
            ComboBox combo = sender as ComboBox;
            foreach (var item in x)
            {
                for (int i = 0; i < dgvBacteriologia.Rows.Count - 1; i++)
                {
                    if (dgvBacteriologia.Rows[i].Cells[1].FormattedValue.ToString() == combo.Text)
                    {
                        MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        combo.SelectedIndex = -1;
                        return;
                    }
                }
                if (combo.Text == item.EXAMEN)
                {
                    dgvBacteriologia.CurrentRow.Cells["codprob"].Value = item.COD_PRODUCTO;
                    dgvBacteriologia.CurrentRow.Cells["codareab"].Value = item.CODIGO_AREA;
                    dgvBacteriologia.CurrentRow.Cells["areab"].Value = item.AREA;
                    dgvBacteriologia.Focus();
                    break;
                }
            }
        }
        private void dgvBacteriologia_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvBacteriologia.CurrentCell.ColumnIndex == 1 && e.Control is DataGridViewComboBoxEditingControl)
            {
                if (cboxEdit != null)
                {
                    cboxEdit.SelectionChangeCommitted -= EditComboBox_SelectionChangeCommittedB;
                }
                cboxEdit = (DataGridViewComboBoxEditingControl)e.Control;
                cboxEdit.SelectionChangeCommitted += EditComboBox_SelectionChangeCommittedB;
            }
        }
        private void EditComboBox_SelectionChangeCommittedO(object sender, System.EventArgs e)
        {
            // set the value as needed
            List<DtoLaboratorioEstructura> x = new List<DtoLaboratorioEstructura>();
            x = NegLaboratorio.listarProductos(12); //codigo de la tabla hc_catalogos_tipos
            ComboBox combo = sender as ComboBox;
            foreach (var item in x)
            {
                for (int i = 0; i < dgvOtros.Rows.Count - 1; i++)
                {
                    if (dgvOtros.Rows[i].Cells[1].FormattedValue.ToString() == combo.Text)
                    {
                        MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        combo.SelectedIndex = -1;
                        return;
                    }
                }
                if (combo.Text == item.EXAMEN)
                {
                    dgvOtros.CurrentRow.Cells["codproo"].Value = item.COD_PRODUCTO;
                    dgvOtros.CurrentRow.Cells["codareao"].Value = item.CODIGO_AREA;
                    dgvOtros.CurrentRow.Cells["areao"].Value = item.AREA;
                    dgvOtros.Focus();
                    break;
                }
            }
        }
        private void dgvOtros_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvOtros.CurrentCell.ColumnIndex == 1 && e.Control is DataGridViewComboBoxEditingControl)
            {
                if (cboxEdit != null)
                {
                    cboxEdit.SelectionChangeCommitted -= EditComboBox_SelectionChangeCommittedO;
                }
                cboxEdit = (DataGridViewComboBoxEditingControl)e.Control;
                cboxEdit.SelectionChangeCommitted += EditComboBox_SelectionChangeCommittedO;
            }
        }
        #endregion

        private void btnAyudaMedicoS_Click(object sender, EventArgs e)
        {
            List<MEDICOS> medicos = NegMedicos.listaMedicos();
            medicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(medicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();

            if (ayuda.campoPadre.Text != string.Empty)
            {
                _medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                agregarMedico(_medico);
            }
        }
        private void agregarMedico(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                txtSolicitante.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            habilitaBotones(false, true, false, false, true);
            CargarAtencionPaciente(ultimaAtencion.ATE_CODIGO);
            cmbPerfil.SelectedIndex = -1;
        }
        public void limpiarTablas()
        {
            dgvBacteriologia.Rows.Clear();
            dgvHematologia.Rows.Clear();
            dgvOtros.Rows.Clear();
            dgvQSanguinea.Rows.Clear();
            dgvSerologia.Rows.Clear();
            dgvUroanalisis.Rows.Clear();
            dvgCoprologico.Rows.Clear();
        }
        public void Limpiar()
        {
            dgvBacteriologia.Rows.Clear();
            dgvHematologia.Rows.Clear();
            dgvOtros.Rows.Clear();
            dgvQSanguinea.Rows.Clear();
            dgvSerologia.Rows.Clear();
            dgvUroanalisis.Rows.Clear();
            dvgCoprologico.Rows.Clear();
            txtSolicitante.Text = "";
            txtRecibe.Text = "";
            txtServicio.Text = "";
            txtSala.Text = "";
            txtCama.Text = "";
            dtpFechaMuestra.Value = DateTime.Now;
            rburgente.Checked = false;
            rbrutina.Checked = false;
            rbcontrol.Checked = false;
            lclCodigo = 0;
            Editar = false;
            cmbPerfil.SelectedIndex = -1;
            habilitaBotones(true, false, false, false, false);
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de cancelar?", "HIS3000", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Limpiar();
            }
        }
        public void guardar()
        {
            laboratorio = new HC_LABORATORIO_CLINICO();
            laboratorio.LCL_SALA = txtSala.Text.Trim();
            laboratorio.LCL_FECHA_MUESTRA = dtpFechaMuestra.Value;
            laboratorio.LCL_HABITACION = txtCama.Text.Trim();
            laboratorio.LCL_FECHA_CREACION = DateTime.Now;
            laboratorio.ATE_CODIGO = ultimaAtencion.ATE_CODIGO;
            laboratorio.LCL_CODIGO = 0;
            laboratorio.LCL_SERVICIO = txtServicio.Text.Trim();
            laboratorio.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
            laboratorio.LCL_NOMBRE_RECIBE = txtRecibe.Text.Trim();
            laboratorio.MED_CODIGO = _medico.MED_CODIGO;
            laboratorio.LCL_PRIORIDAD_U = rburgente.Checked;
            laboratorio.LCL_PRIORIDAD_C = rbcontrol.Checked;
            laboratorio.LCL_PRIORIDAD_R = rbrutina.Checked;
            laboratorio.LCL_MUESTRA = txtmuestra.Text.Trim();

            #region Examenes
            //cargamos los detalles
            detalle = new List<HC_LABORATORIO_CLINICO_DETALLE>();
            DataTable productoSic = new DataTable();
            for (int i = 0; i < dgvHematologia.Rows.Count - 1; i++)
            {
                HC_LABORATORIO_CLINICO_DETALLE x = new HC_LABORATORIO_CLINICO_DETALLE();
                if (dgvHematologia.Rows[i].Cells[0].Value != null)
                {
                    productoSic = NegProducto.RecuperarProductoSic(dgvHematologia.Rows[i].Cells[0].Value.ToString());
                    x.LCD_CODIGO = 0;
                    x.LCD_NOMBRE_EXAMEN = productoSic.Rows[0][1].ToString();
                    x.LCD_CODPRO = dgvHematologia.Rows[i].Cells[0].Value.ToString();
                    x.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    x.LCL_CODIGO = 0;

                    detalle.Add(x);
                }
            }
            for (int i = 0; i < dgvUroanalisis.Rows.Count - 1; i++)
            {
                HC_LABORATORIO_CLINICO_DETALLE x = new HC_LABORATORIO_CLINICO_DETALLE();
                if (dgvUroanalisis.Rows[i].Cells[0].Value != null)
                {
                    productoSic = NegProducto.RecuperarProductoSic(dgvUroanalisis.Rows[i].Cells[0].Value.ToString());
                    x.LCD_CODIGO = 0;
                    x.LCD_NOMBRE_EXAMEN = productoSic.Rows[0][1].ToString();
                    x.LCD_CODPRO = dgvUroanalisis.Rows[i].Cells[0].Value.ToString();
                    x.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    x.LCL_CODIGO = 0;

                    detalle.Add(x);
                }
            }
            for (int i = 0; i < dvgCoprologico.Rows.Count - 1; i++)
            {
                HC_LABORATORIO_CLINICO_DETALLE x = new HC_LABORATORIO_CLINICO_DETALLE();
                if (dvgCoprologico.Rows[i].Cells[0].Value != null)
                {
                    productoSic = NegProducto.RecuperarProductoSic(dvgCoprologico.Rows[i].Cells[0].Value.ToString());
                    x.LCD_CODIGO = 0;
                    x.LCD_NOMBRE_EXAMEN = productoSic.Rows[0][1].ToString();
                    x.LCD_CODPRO = dvgCoprologico.Rows[i].Cells[0].Value.ToString();
                    x.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    x.LCL_CODIGO = 0;

                    detalle.Add(x);
                }
            }
            for (int i = 0; i < dgvQSanguinea.Rows.Count - 1; i++)
            {
                HC_LABORATORIO_CLINICO_DETALLE x = new HC_LABORATORIO_CLINICO_DETALLE();
                if (dgvQSanguinea.Rows[i].Cells[0].Value != null)
                {
                    productoSic = NegProducto.RecuperarProductoSic(dgvQSanguinea.Rows[i].Cells[0].Value.ToString());
                    x.LCD_CODIGO = 0;
                    x.LCD_NOMBRE_EXAMEN = productoSic.Rows[0][1].ToString();
                    x.LCD_CODPRO = dgvQSanguinea.Rows[i].Cells[0].Value.ToString();
                    x.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    x.LCL_CODIGO = 0;

                    detalle.Add(x);
                }
            }
            for (int i = 0; i < dgvSerologia.Rows.Count - 1; i++)
            {
                HC_LABORATORIO_CLINICO_DETALLE x = new HC_LABORATORIO_CLINICO_DETALLE();
                if (dgvSerologia.Rows[i].Cells[0].Value != null)
                {
                    productoSic = NegProducto.RecuperarProductoSic(dgvSerologia.Rows[i].Cells[0].Value.ToString());
                    x.LCD_CODIGO = 0;
                    x.LCD_NOMBRE_EXAMEN = productoSic.Rows[0][1].ToString();
                    x.LCD_CODPRO = dgvSerologia.Rows[i].Cells[0].Value.ToString();
                    x.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    x.LCL_CODIGO = 0;

                    detalle.Add(x);
                }
            }
            for (int i = 0; i < dgvBacteriologia.Rows.Count - 1; i++)
            {
                HC_LABORATORIO_CLINICO_DETALLE x = new HC_LABORATORIO_CLINICO_DETALLE();
                if (dgvBacteriologia.Rows[i].Cells[0].Value != null)
                {
                    productoSic = NegProducto.RecuperarProductoSic(dgvBacteriologia.Rows[i].Cells[0].Value.ToString());
                    x.LCD_CODIGO = 0;
                    x.LCD_NOMBRE_EXAMEN = productoSic.Rows[0][1].ToString();
                    x.LCD_CODPRO = dgvBacteriologia.Rows[i].Cells[0].Value.ToString();
                    x.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    x.LCL_CODIGO = 0;

                    detalle.Add(x);
                }
            }
            for (int i = 0; i < dgvOtros.Rows.Count - 1; i++)
            {
                HC_LABORATORIO_CLINICO_DETALLE x = new HC_LABORATORIO_CLINICO_DETALLE();
                if (dgvOtros.Rows[i].Cells[0].Value != null)
                {
                    productoSic = NegProducto.RecuperarProductoSic(dgvOtros.Rows[i].Cells[0].Value.ToString());
                    x.LCD_CODIGO = 0;
                    x.LCD_NOMBRE_EXAMEN = productoSic.Rows[0][1].ToString();
                    x.LCD_CODPRO = dgvOtros.Rows[i].Cells[0].Value.ToString();
                    x.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    x.LCL_CODIGO = 0;

                    detalle.Add(x);
                }
            }
            #endregion

            if (!Editar)
            {
                if (NegLaboratorio.crearLaboratorio(laboratorio, detalle))
                {
                    MessageBox.Show("Guardado con exito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    habilitaBotones(true, false, false, false, false);
                }
                else
                    MessageBox.Show("Algo ocurrio al guardar los datos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (NegLaboratorio.editarLaboratorio(laboratorio, detalle, lclCodigo))
                {
                    MessageBox.Show("Cambio realizado con exito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    habilitaBotones(true, false, false, false, false);
                }
                else
                    MessageBox.Show("Algo ocurrio al actualizar los datos", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validaFormulario())
            {
                //guardamos los datos
                guardar();
                Imprimir();
                refrescarSolicitudes();
            }
        }
        public bool validaFormulario()
        {
            Errores.Clear();
            bool valido = true;
            if (txtRecibe.Text.Trim() == "")
            {
                Errores.SetError(txtRecibe, "Campo obligatorio.");
                valido = false;
            }
            if (txtSolicitante.Text.Trim() == "")
            {
                Errores.SetError(txtSolicitante, "Debe elegir el medico solicitante");
                valido = false;
            }
            if (txtServicio.Text.Trim() == "")
            {
                Errores.SetError(txtServicio, "Campo obligatorio");
                valido = false;
            }
            if (txtSala.Text.Trim() == "")
            {
                Errores.SetError(txtSala, "Campo obligatorio");
                valido = false;
            }
            if (txtCama.Text.Trim() == "")
            {
                Errores.SetError(txtCama, "Campo obligatorio");
                valido = false;
            }
            if (!rbcontrol.Checked && !rbrutina.Checked && !rburgente.Checked)
            {
                Errores.SetError(ultraGroupBox4, "Seleccione prioridad");
                valido = false;
            }
            return valido;
        }
        public void cargarDatos()
        {
            if (laboratorio != null)
            {
                txtCama.Text = laboratorio.LCL_HABITACION;
                txtRecibe.Text = laboratorio.LCL_NOMBRE_RECIBE;
                txtSala.Text = laboratorio.LCL_SALA;
                txtServicio.Text = laboratorio.LCL_SERVICIO;
                txtSolicitante.Text = _medico.MED_APELLIDO_PATERNO + " " + _medico.MED_APELLIDO_MATERNO + " " + _medico.MED_NOMBRE1 + " " + _medico.MED_NOMBRE2;
                dtpFechaMuestra.Value = (DateTime)laboratorio.LCL_FECHA_MUESTRA;
                rbcontrol.Checked = (bool)laboratorio.LCL_PRIORIDAD_C;
                rbrutina.Checked = (bool)laboratorio.LCL_PRIORIDAD_R;
                rburgente.Checked = (bool)laboratorio.LCL_PRIORIDAD_U;
                txtmuestra.Text = laboratorio.LCL_MUESTRA;
            }
            #region Cargar Detalle
            if (detalle.Count > 0)
            {
                foreach (var item in detalle)
                {
                    DataTable productoLab = new DataTable();
                    productoLab = NegLaboratorio.recuperaProductoLaboratorio(item.LCD_CODPRO);
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewComboBoxCell _cmbcell = new DataGridViewComboBoxCell();
                    DataGridViewTextBoxCell _codigopro = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _codigoarea = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _area = new DataGridViewTextBoxCell();

                    if (productoLab.Rows[0]["coddep"].ToString() == "401001") //hematologia
                    {
                        _cmbcell.DataSource = NegLaboratorio.listarProductos(6);
                        _cmbcell.DisplayMember = "EXAMEN";
                        _cmbcell.ValueMember = "COD_PRODUCTO";
                        _cmbcell.Value = item.LCD_CODPRO;

                        _codigopro.Value = item.LCD_CODPRO;
                        _codigoarea.Value = productoLab.Rows[0]["coddep"].ToString();
                        _area.Value = productoLab.Rows[0]["desdep"].ToString();

                        fila.Cells.Add(_codigopro);
                        fila.Cells.Add(_cmbcell);
                        fila.Cells.Add(_codigoarea);
                        fila.Cells.Add(_area);

                        dgvHematologia.Rows.Add(fila);
                    }

                    else if (productoLab.Rows[0]["coddep"].ToString() == "401002") //uroanalisis
                    {
                        _cmbcell.DataSource = NegLaboratorio.listarProductos(7);
                        _cmbcell.DisplayMember = "EXAMEN";
                        _cmbcell.ValueMember = "COD_PRODUCTO";
                        _cmbcell.Value = item.LCD_CODPRO;

                        _codigopro.Value = item.LCD_CODPRO;
                        _codigoarea.Value = productoLab.Rows[0]["coddep"].ToString();
                        _area.Value = productoLab.Rows[0]["desdep"].ToString();

                        fila.Cells.Add(_codigopro);
                        fila.Cells.Add(_cmbcell);
                        fila.Cells.Add(_codigoarea);
                        fila.Cells.Add(_area);

                        dgvUroanalisis.Rows.Add(fila);
                    }
                    else if (productoLab.Rows[0]["coddep"].ToString() == "401003") //coprologico
                    {
                        _cmbcell.DataSource = NegLaboratorio.listarProductos(8);
                        _cmbcell.DisplayMember = "EXAMEN";
                        _cmbcell.ValueMember = "COD_PRODUCTO";
                        _cmbcell.Value = item.LCD_CODPRO;

                        _codigopro.Value = item.LCD_CODPRO;
                        _codigoarea.Value = productoLab.Rows[0]["coddep"].ToString();
                        _area.Value = productoLab.Rows[0]["desdep"].ToString();

                        fila.Cells.Add(_codigopro);
                        fila.Cells.Add(_cmbcell);
                        fila.Cells.Add(_codigoarea);
                        fila.Cells.Add(_area);

                        dvgCoprologico.Rows.Add(fila);
                    }
                    else if (productoLab.Rows[0]["coddep"].ToString() == "401004") //quimica sanguinea
                    {
                        _cmbcell.DataSource = NegLaboratorio.listarProductos(9);
                        _cmbcell.DisplayMember = "EXAMEN";
                        _cmbcell.ValueMember = "COD_PRODUCTO";
                        _cmbcell.Value = item.LCD_CODPRO;

                        _codigopro.Value = item.LCD_CODPRO;
                        _codigoarea.Value = productoLab.Rows[0]["coddep"].ToString();
                        _area.Value = productoLab.Rows[0]["desdep"].ToString();

                        fila.Cells.Add(_codigopro);
                        fila.Cells.Add(_cmbcell);
                        fila.Cells.Add(_codigoarea);
                        fila.Cells.Add(_area);

                        dgvQSanguinea.Rows.Add(fila);
                    }
                    else if (productoLab.Rows[0]["coddep"].ToString() == "401005") //quimica sanguinea
                    {
                        _cmbcell.DataSource = NegLaboratorio.listarProductos(10);
                        _cmbcell.DisplayMember = "EXAMEN";
                        _cmbcell.ValueMember = "COD_PRODUCTO";
                        _cmbcell.Value = item.LCD_CODPRO;

                        _codigopro.Value = item.LCD_CODPRO;
                        _codigoarea.Value = productoLab.Rows[0]["coddep"].ToString();
                        _area.Value = productoLab.Rows[0]["desdep"].ToString();

                        fila.Cells.Add(_codigopro);
                        fila.Cells.Add(_cmbcell);
                        fila.Cells.Add(_codigoarea);
                        fila.Cells.Add(_area);

                        dgvSerologia.Rows.Add(fila);
                    }
                    else if (productoLab.Rows[0]["coddep"].ToString() == "401006") //quimica sanguinea
                    {
                        _cmbcell.DataSource = NegLaboratorio.listarProductos(11);
                        _cmbcell.DisplayMember = "EXAMEN";
                        _cmbcell.ValueMember = "COD_PRODUCTO";
                        _cmbcell.Value = item.LCD_CODPRO;

                        _codigopro.Value = item.LCD_CODPRO;
                        _codigoarea.Value = productoLab.Rows[0]["coddep"].ToString();
                        _area.Value = productoLab.Rows[0]["desdep"].ToString();

                        fila.Cells.Add(_codigopro);
                        fila.Cells.Add(_cmbcell);
                        fila.Cells.Add(_codigoarea);
                        fila.Cells.Add(_area);

                        dgvBacteriologia.Rows.Add(fila);
                    }
                    else if (productoLab.Rows[0]["coddep"].ToString() == "401007") //quimica sanguinea
                    {
                        _cmbcell.DataSource = NegLaboratorio.listarProductos(12);
                        _cmbcell.DisplayMember = "EXAMEN";
                        _cmbcell.ValueMember = "COD_PRODUCTO";
                        _cmbcell.Value = item.LCD_CODPRO;

                        _codigopro.Value = item.LCD_CODPRO;
                        _codigoarea.Value = productoLab.Rows[0]["coddep"].ToString();
                        _area.Value = productoLab.Rows[0]["desdep"].ToString();

                        fila.Cells.Add(_codigopro);
                        fila.Cells.Add(_cmbcell);
                        fila.Cells.Add(_codigoarea);
                        fila.Cells.Add(_area);

                        dgvOtros.Rows.Add(fila);
                    }
                }
            }
            #endregion
        }
        private void gridSol_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("¿Desea cargar el laboratorio?", "HIS3000", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                limpiarTablas();
                detalle = NegLaboratorio.recuperarLaboratorioDetalle(Convert.ToInt64(gridSol.Rows[gridSol.CurrentRow.Index].Cells["ID"].Value.ToString()));
                laboratorio = NegLaboratorio.recuperarLaboratorio(Convert.ToInt64(gridSol.Rows[gridSol.CurrentRow.Index].Cells["ID"].Value.ToString()));
                lclCodigo = laboratorio.LCL_CODIGO;
                _medico = NegMedicos.recuperarMedico((int)laboratorio.MED_CODIGO);
                cargarDatos();
                habilitaBotones(false, false, true, true, true);
                List<DtoAccesosHis> acceso = NegAccesoOpciones.recuperaAccesoUsuario(His.Entidades.Clases.Sesion.codUsuario);
                foreach (var item in acceso)
                {
                    switch (item.ID_ACCESO)
                    {
                        case 43000:
                            btnEditar.Visible = true;
                            break;
                    }
                }
                    
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar = true;
            cmbPerfil.SelectedIndex = -1;
            habilitaBotones(false, true, false, false, true);
        }
        public void Imprimir()
        {
            DsForm010A lab = new DsForm010A();
            DataRow dr;

            dr = lab.Tables["Form010A"].NewRow();
            #region Cabecera
            dr["Path"] = NegUtilitarios.RutaLogo("General");
            dr["UnidadOperativo"] = "";
            dr["Empresa"] = His.Entidades.Clases.Sesion.nomEmpresa;
            dr["Cod"] = pac_adicional.COD_PROVINCIA;
            dr["Parroquia"] = pac_adicional.COD_PARROQUIA;
            dr["Canton"] = pac_adicional.COD_CANTON;
            dr["Provincia"] = pac_adicional.COD_PROVINCIA;
            dr["ApellidoP"] = paciente.PAC_APELLIDO_PATERNO;
            dr["ApellidoM"] = paciente.PAC_APELLIDO_MATERNO;
            dr["Nombre1"] = paciente.PAC_NOMBRE1;
            dr["Nombre2"] = paciente.PAC_NOMBRE2;
            dr["Edad"] = lbledad.Text;
            dr["Servicio"] = laboratorio.LCL_SERVICIO;
            dr["Sala"] = laboratorio.LCL_SALA;
            dr["Cama"] = laboratorio.LCL_HABITACION;
            if ((bool)laboratorio.LCL_PRIORIDAD_U)
                dr["Urgente"] = "X";
            else if ((bool)laboratorio.LCL_PRIORIDAD_R)
                dr["Rutina"] = "X";
            else if ((bool)laboratorio.LCL_PRIORIDAD_C)
                dr["Control"] = "X";
            dr["FechaToma"] = Convert.ToDateTime(laboratorio.LCL_FECHA_MUESTRA).ToShortDateString();
            dr["Fecha"] = Convert.ToDateTime(laboratorio.LCL_FECHA_CREACION).ToShortDateString();
            dr["Hora"] = Convert.ToDateTime(laboratorio.LCL_FECHA_CREACION).ToShortTimeString();
            dr["Profesional"] = _medico.MED_APELLIDO_PATERNO + " " + _medico.MED_APELLIDO_MATERNO + " " + _medico.MED_NOMBRE1 + " " + _medico.MED_NOMBRE2;
            dr["Identificacion"] = paciente.PAC_IDENTIFICACION.Trim();
            if (NegParametros.ParametroFormularios())
                dr["HC"] = paciente.PAC_IDENTIFICACION.Trim();
            else
                dr["HC"] = paciente.PAC_HISTORIA_CLINICA.Trim();
            if (_medico.MED_RUC.Length <= 10)
                dr["Med_Codigo"] = _medico.MED_RUC;
            else
                dr["Med_Codigo"] = _medico.MED_RUC.Substring(0, 10);
            #endregion

            int hemato = 0, uro = 0, qs = 0, se = 0, ba = 0, ot = 0;
            DataRow dr1;
            bool exceso = false;
            dr1 = lab.Tables["Excesos"].NewRow();
            foreach (var item in detalle)
            {
                DataTable productoLab = NegLaboratorio.recuperaProductoLaboratorio(item.LCD_CODPRO);
                if (exceso == true)
                {
                    dr1 = lab.Tables["Excesos"].NewRow();
                    exceso = false;
                }
                #region Hematologia
                if (productoLab.Rows[0]["coddep"].ToString() == "401001")
                {
                    if (item.LCD_NOMBRE_EXAMEN.Contains("BIOME"))
                        dr["Bio_Hematica"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("PLAQUETA"))
                        dr["Plaquetas"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("GRUPO S"))
                        dr["GSanguineo"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("RETICU"))
                        dr["Reticulocitos"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("HEMATO"))
                        dr["Hematozoario"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("CELULA"))
                        dr["Celula"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("CUAGULA"))
                        dr["TCoagulacion"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("INDICE"))
                        dr["IHematicos"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("PROTROM"))
                        dr["TP"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("TROMBOP"))
                        dr["TIP"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("DREPANO"))
                        dr["Drepanocitos"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("DIRECTO"))
                        dr["CDirecto"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("INDIRECTO"))
                        dr["CIndirecto"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("SANGRIA"))
                        dr["TSangria"] = "X";
                    else
                    {
                        if (hemato == 0)
                        {
                            dr["H1"] = item.LCD_NOMBRE_EXAMEN;
                            dr["H1_1"] = "X";
                            hemato++;
                        }
                        else if (hemato == 1)
                        {
                            dr["H2"] = item.LCD_NOMBRE_EXAMEN;
                            dr["H2_1"] = "X";
                            hemato++;
                        }
                        else if (hemato == 2)
                        {
                            dr["H3"] = item.LCD_NOMBRE_EXAMEN;
                            dr["H3_1"] = "X";
                            hemato++;
                        }
                        else if (hemato == 3)
                        {
                            dr["H4"] = item.LCD_NOMBRE_EXAMEN;
                            dr["H4__1"] = "X";
                            hemato++;
                        }
                        else if (hemato == 4)
                        {
                            dr["H5"] = item.LCD_NOMBRE_EXAMEN;
                            dr["H5_1"] = "X";
                            hemato++;
                        }
                        else if (hemato == 5)
                        {
                            dr["H6"] = item.LCD_NOMBRE_EXAMEN;
                            dr["H6_1"] = "X";
                            hemato++;
                        }
                        else if (hemato == 6)
                        {
                            dr["H7"] = item.LCD_NOMBRE_EXAMEN;
                            dr["H7_1"] = "X";
                            hemato++;
                        }
                        else if (hemato == 7)
                        {
                            dr["H8"] = item.LCD_NOMBRE_EXAMEN;
                            dr["H8_1"] = "X";
                        }
                        else if (hemato == 8)
                        {
                            dr["H9"] = item.LCD_NOMBRE_EXAMEN;
                            dr["H9_1"] = "X";
                            hemato++;
                        }
                        else if (hemato == 9)
                        {
                            dr["H10"] = item.LCD_NOMBRE_EXAMEN;
                            dr["H10_1"] = "X";
                            hemato++;
                        }
                        else
                        {
                            dr1["despro"] = "HEMATOLOGIA - " + item.LCD_NOMBRE_EXAMEN;
                            dr1["marca"] = "X";
                            exceso = true;
                        }
                    }
                }
                #endregion

                #region Uroanalisis
                else if (productoLab.Rows[0]["coddep"].ToString() == "401002")
                {
                    if (item.LCD_NOMBRE_EXAMEN.Contains("ELEMENTAL"))
                        dr["EMicroscopico"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("GOTA"))
                        dr["GFresca"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("EMBARAZO"))
                        dr["PEmbarazo"] = "X";
                    else
                    {
                        if (uro == 0)
                        {
                            dr["U1"] = item.LCD_NOMBRE_EXAMEN;
                            dr["U1_1"] = "X";
                            uro++;
                        }
                        else if (uro == 1)
                        {
                            dr["U2"] = item.LCD_NOMBRE_EXAMEN;
                            dr["U2_1"] = "X";
                            uro++;
                        }
                        else
                        {
                            dr1["despro"] = "UROANALISIS - " + item.LCD_NOMBRE_EXAMEN;
                            dr1["marca"] = "X";
                            exceso = true;
                        }
                    }
                }
                #endregion

                #region Coprologico
                else if (productoLab.Rows[0]["coddep"].ToString() == "401003")
                {
                    if (item.LCD_NOMBRE_EXAMEN.Contains("COPROPARA"))
                        dr["CParasitatrio"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("SERIADO"))
                        dr["CSeriadp"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("OCULTA"))
                        dr["SOculta"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("POLIMORFO"))
                        dr["IDP"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("ROTAVIR"))
                        dr["IDR"] = "X";
                    else
                    {
                        dr1["despro"] = "COPROLOGICO - " + item.LCD_NOMBRE_EXAMEN;
                        dr1["marca"] = "X";
                        exceso = true;
                    }
                }
                #endregion

                #region Quimica Sanguinea
                else if (productoLab.Rows[0]["coddep"].ToString() == "401004")
                {
                    if (item.LCD_NOMBRE_EXAMEN.Contains("AYUNAS"))
                        dr["GAyunas"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("PRANDIAL"))
                        dr["GPP"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("UREA"))
                        dr["Urea"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("CREATINI"))
                        dr["Creatinina"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("BILIRRUBINA"))
                        dr["BTotal"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("DIRECTA"))
                        dr["BDirecta"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("ÚRICO"))
                        dr["AUrico"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("PROTE"))
                        dr["PTotal"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("ALBUMINA"))
                        dr["Albumina"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("GLOBULINA"))
                        dr["Globulina"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("ALT"))
                        dr["ALT"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("AST"))
                        dr["AST"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("ALCALINA"))
                        dr["FAlcalina"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("ÁCIDA"))
                        dr["FAcida"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN == "COLESTEROL")
                        dr["CTotal"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("HDL"))
                        dr["CHDL"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("LDL"))
                        dr["CLDL"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("TRIGLICE"))
                        dr["Trigliceridos"] = "";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("HIERRO"))
                        dr["Hierro"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("AMILASA"))
                        dr["Amilasa"] = "X";
                    else
                    {
                        if (qs == 0)
                        {
                            dr["QS1"] = item.LCD_NOMBRE_EXAMEN;
                            dr["QS1_1"] = "X";
                            qs++;
                        }
                        else if (qs == 1)
                        {
                            dr["QS2"] = item.LCD_NOMBRE_EXAMEN;
                            dr["QS2_1"] = "X";
                            qs++;
                        }
                        else if (qs == 2)
                        {
                            dr["QS3"] = item.LCD_NOMBRE_EXAMEN;
                            dr["QS3_1"] = "X";
                            qs++;
                        }
                        else if (qs == 3)
                        {
                            dr["QS4"] = item.LCD_NOMBRE_EXAMEN;
                            dr["QS4_1"] = "X";
                            qs++;
                        }
                        else
                        {
                            dr1["despro"] = "QUIMICA SANGUINEA - " + item.LCD_NOMBRE_EXAMEN;
                            dr1["marca"] = "X";
                            exceso = true;
                        }
                    }
                }
                #endregion

                #region Serologia
                else if (productoLab.Rows[0]["coddep"].ToString() == "401005")
                {
                    if (item.LCD_NOMBRE_EXAMEN.Contains("VDRL"))
                        dr["VDRL"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("FEBRILES"))
                        dr["AFebriles"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("LATEX"))
                        dr["Latex"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("ASTO"))
                        dr["Asto"] = "X";
                    else
                    {
                        if (se == 0)
                        {
                            dr["S1"] = item.LCD_NOMBRE_EXAMEN;
                            dr["S1_1"] = "X";
                            se++;
                        }
                        else if (se == 1)
                        {
                            dr["S2"] = item.LCD_NOMBRE_EXAMEN;
                            dr["S2_1"] = "X";
                            se++;
                        }
                        else if (se == 2)
                        {
                            dr["S3"] = item.LCD_NOMBRE_EXAMEN;
                            dr["S3_1"] = "X";
                            se++;
                        }
                        else if (se == 3)
                        {
                            dr["S4"] = "";
                            dr["S4_1"] = "";
                            se++;
                        }
                        else
                        {
                            dr1["despro"] = "SEROLOGIA - " + item.LCD_NOMBRE_EXAMEN;
                            dr1["marca"] = "X";
                            exceso = true;
                        }
                    }
                }

                #endregion

                #region Bacteriologia
                else if (productoLab.Rows[0]["coddep"].ToString() == "401006")
                {
                    if (item.LCD_NOMBRE_EXAMEN.Contains("GRAM"))
                        dr["GRAM"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("ZIEH"))
                        dr["ZIEHL"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("HONGO"))
                        dr["Hongos"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("FRESCO"))
                        dr["Fresco"] = "X";
                    else if (item.LCD_NOMBRE_EXAMEN.Contains("CULTIVO"))
                        dr["Cultivo"] = "X";
                    else
                    {
                        if (ba == 0)
                        {
                            dr["B1"] = item.LCD_NOMBRE_EXAMEN;
                            dr["B1_1"] = "X";
                            ba++;
                        }
                        else
                        {
                            dr1["despro"] = "BACTEROLOGIA - " + item.LCD_NOMBRE_EXAMEN;
                            dr1["marca"] = "X";
                            exceso = true;
                        }
                    }
                    dr["Muestra"] = laboratorio.LCL_MUESTRA;
                }
                #endregion

                #region Otros
                else if (productoLab.Rows[0]["coddep"].ToString() == "401007")
                {
                    if (ot == 0)
                    {
                        dr["O1"] = item.LCD_NOMBRE_EXAMEN;
                        dr["O1_1"] = "X";
                        ot++;
                    }
                    else if (ot == 1)
                    {
                        dr["O2"] = item.LCD_NOMBRE_EXAMEN;
                        dr["O2_1"] = "X";
                        ot++;
                    }
                    else if (ot == 2)
                    {
                        dr["O3"] = item.LCD_NOMBRE_EXAMEN;
                        dr["O3_1"] = "X";
                        ot++;
                    }
                    else if (ot == 3)
                    {
                        dr["O4"] = item.LCD_NOMBRE_EXAMEN;
                        dr["O4_1"] = "X";
                        ot++;
                    }
                    else
                    {
                        dr1["despro"] = "OTROS - " + item.LCD_NOMBRE_EXAMEN;
                        dr1["marca"] = "X";
                        exceso = true;
                    }
                }

                #endregion
                if (exceso == true)
                {
                    lab.Tables["Excesos"].Rows.Add(dr1);
                }
            }

            lab.Tables["Form010A"].Rows.Add(dr);

            frmReportes x = new frmReportes(1, "Laboratorio", lab);
            x.Show();
            x = new frmReportes(1, "Excedentes", lab);
            x.Show();
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void dtpFechaMuestra_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaMuestra.Value > DateTime.Now)
            {
                MessageBox.Show("Fecha de toma de muestra no puede ser mayor a fecha actual.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFechaMuestra.Value = DateTime.Now;
            }
        }
        public void listarPerfiles()
        {
            cmbPerfil.DataSource = NegLaboratorio.cargaPerfiles(His.Entidades.Clases.Sesion.codUsuario);
            cmbPerfil.DisplayMember = "PL_PERFIL";
            cmbPerfil.ValueMember = "PL_CODIGO";
            cmbPerfil.SelectedIndex = -1;
        }

        private void cmbPerfil_ValueChanged(object sender, EventArgs e)
        {
            limpiar();
            cargaDatagrid();
        }
        public void limpiar()
        {
            dgvHematologia.Rows.Clear();
            dgvUroanalisis.Rows.Clear();
            dvgCoprologico.Rows.Clear();
            dgvQSanguinea.Rows.Clear();
            dgvSerologia.Rows.Clear();
            dgvBacteriologia.Rows.Clear();
            dgvOtros.Rows.Clear();
        }
        public void cargaDatagrid()
        {
            try
            {
                if (cmbPerfil.SelectedIndex != -1)
                {
                    #region cargar grid
                    List<DtoLaboratorioEstructura> x = new List<DtoLaboratorioEstructura>();
                    x = NegLaboratorio.cargadgvHematologia(Convert.ToInt64(cmbPerfil.SelectedItem.DataValue.ToString())); //codigo de la tabla hc_catalogos_tipos
                    foreach (var item in x)
                    {
                        DataGridViewRow fila = new DataGridViewRow();
                        DataGridViewComboBoxCell _cmbcell = new DataGridViewComboBoxCell();
                        DataGridViewTextBoxCell _codigopro = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell _codigoarea = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell _area = new DataGridViewTextBoxCell();

                        _cmbcell.DataSource = NegLaboratorio.listarProductos(6);
                        _cmbcell.DisplayMember = "EXAMEN";
                        _cmbcell.ValueMember = "COD_PRODUCTO";
                        _cmbcell.Value = item.COD_PRODUCTO;

                        _codigopro.Value = item.COD_PRODUCTO;
                        _codigoarea.Value = item.CODIGO_AREA;
                        _area.Value = item.AREA;

                        fila.Cells.Add(_codigopro);
                        fila.Cells.Add(_cmbcell);
                        fila.Cells.Add(_codigoarea);
                        fila.Cells.Add(_area);

                        dgvHematologia.Rows.Add(fila);
                    }

                    x = NegLaboratorio.cargadgvUroanalisis(Convert.ToInt64(cmbPerfil.SelectedItem.DataValue.ToString()));
                    foreach (var item in x)
                    {
                        DataGridViewRow fila1 = new DataGridViewRow();
                        DataGridViewComboBoxCell _cmbcell1 = new DataGridViewComboBoxCell();
                        DataGridViewTextBoxCell _codigopro1 = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell _codigoarea1 = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell _area1 = new DataGridViewTextBoxCell();

                        _cmbcell1.DataSource = NegLaboratorio.listarProductos(7);
                        _cmbcell1.DisplayMember = "EXAMEN";
                        _cmbcell1.ValueMember = "COD_PRODUCTO";
                        _cmbcell1.Value = item.COD_PRODUCTO;

                        _codigopro1.Value = item.COD_PRODUCTO;
                        _codigoarea1.Value = item.CODIGO_AREA;
                        _area1.Value = item.AREA;

                        fila1.Cells.Add(_codigopro1);
                        fila1.Cells.Add(_cmbcell1);
                        fila1.Cells.Add(_codigoarea1);
                        fila1.Cells.Add(_area1);

                        dgvUroanalisis.Rows.Add(fila1);
                    }

                    x = NegLaboratorio.cargadvgCoprologico(Convert.ToInt64(cmbPerfil.SelectedItem.DataValue.ToString()));
                    foreach (var item in x)
                    {
                        DataGridViewRow fila2 = new DataGridViewRow();
                        DataGridViewComboBoxCell _cmbcell2 = new DataGridViewComboBoxCell();
                        DataGridViewTextBoxCell _codigopro2 = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell _codigoarea2 = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell _area2 = new DataGridViewTextBoxCell();

                        _cmbcell2.DataSource = NegLaboratorio.listarProductos(8);
                        _cmbcell2.DisplayMember = "EXAMEN";
                        _cmbcell2.ValueMember = "COD_PRODUCTO";
                        _cmbcell2.Value = item.COD_PRODUCTO;

                        _codigopro2.Value = item.COD_PRODUCTO;
                        _codigoarea2.Value = item.CODIGO_AREA;
                        _area2.Value = item.AREA;

                        fila2.Cells.Add(_codigopro2);
                        fila2.Cells.Add(_cmbcell2);
                        fila2.Cells.Add(_codigoarea2);
                        fila2.Cells.Add(_area2);

                        dvgCoprologico.Rows.Add(fila2);
                    }

                    x = NegLaboratorio.cargadgvQSanguinea(Convert.ToInt64(cmbPerfil.SelectedItem.DataValue.ToString()));
                    foreach (var item in x)
                    {
                        DataGridViewRow fila3 = new DataGridViewRow();
                        DataGridViewComboBoxCell _cmbcell3 = new DataGridViewComboBoxCell();
                        DataGridViewTextBoxCell _codigopro3 = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell _codigoarea3 = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell _area3 = new DataGridViewTextBoxCell(); ;

                        _cmbcell3.DataSource = NegLaboratorio.listarProductos(9);
                        _cmbcell3.DisplayMember = "EXAMEN";
                        _cmbcell3.ValueMember = "COD_PRODUCTO";
                        _cmbcell3.Value = item.COD_PRODUCTO;

                        _codigopro3.Value = item.COD_PRODUCTO;
                        _codigoarea3.Value = item.CODIGO_AREA;
                        _area3.Value = item.AREA;

                        fila3.Cells.Add(_codigopro3);
                        fila3.Cells.Add(_cmbcell3);
                        fila3.Cells.Add(_codigoarea3);
                        fila3.Cells.Add(_area3);

                        dgvQSanguinea.Rows.Add(fila3);
                    }

                    x = NegLaboratorio.cargadgvSerologia(Convert.ToInt64(cmbPerfil.SelectedItem.DataValue.ToString()));
                    foreach (var item in x)
                    {
                        DataGridViewRow fila4 = new DataGridViewRow();
                        DataGridViewComboBoxCell _cmbcell4 = new DataGridViewComboBoxCell();
                        DataGridViewTextBoxCell _codigopro4 = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell _codigoarea4 = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell _area4 = new DataGridViewTextBoxCell();

                        _cmbcell4.DataSource = NegLaboratorio.listarProductos(10);
                        _cmbcell4.DisplayMember = "EXAMEN";
                        _cmbcell4.ValueMember = "COD_PRODUCTO";
                        _cmbcell4.Value = item.COD_PRODUCTO;

                        _codigopro4.Value = item.COD_PRODUCTO;
                        _codigoarea4.Value = item.CODIGO_AREA;
                        _area4.Value = item.AREA;

                        fila4.Cells.Add(_codigopro4);
                        fila4.Cells.Add(_cmbcell4);
                        fila4.Cells.Add(_codigoarea4);
                        fila4.Cells.Add(_area4);

                        dgvSerologia.Rows.Add(fila4);
                    }
                    x = NegLaboratorio.cargadgvBacteriologia(Convert.ToInt64(cmbPerfil.SelectedItem.DataValue.ToString()));
                    foreach (var item in x)
                    {
                        DataGridViewRow fila5 = new DataGridViewRow();
                        DataGridViewComboBoxCell _cmbcell5 = new DataGridViewComboBoxCell();
                        DataGridViewTextBoxCell _codigopro5 = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell _codigoarea5 = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell _area5 = new DataGridViewTextBoxCell();

                        _cmbcell5.DataSource = NegLaboratorio.listarProductos(11);
                        _cmbcell5.DisplayMember = "EXAMEN";
                        _cmbcell5.ValueMember = "COD_PRODUCTO";
                        _cmbcell5.Value = item.COD_PRODUCTO;

                        _codigopro5.Value = item.COD_PRODUCTO;
                        _codigoarea5.Value = item.CODIGO_AREA;
                        _area5.Value = item.AREA;

                        fila5.Cells.Add(_codigopro5);
                        fila5.Cells.Add(_cmbcell5);
                        fila5.Cells.Add(_codigoarea5);
                        fila5.Cells.Add(_area5);

                        dgvBacteriologia.Rows.Add(fila5);
                    }
                    x = NegLaboratorio.cargadgvOtros(Convert.ToInt64(cmbPerfil.SelectedItem.DataValue.ToString()));
                    foreach (var item in x)
                    {
                        DataGridViewRow fila6 = new DataGridViewRow();
                        DataGridViewComboBoxCell _cmbcell6 = new DataGridViewComboBoxCell();
                        DataGridViewTextBoxCell _codigopro6 = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell _codigoarea6 = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell _area6 = new DataGridViewTextBoxCell();

                        _cmbcell6.DataSource = NegLaboratorio.listarProductos(12);
                        _cmbcell6.DisplayMember = "EXAMEN";
                        _cmbcell6.ValueMember = "COD_PRODUCTO";
                        _cmbcell6.Value = item.COD_PRODUCTO;

                        _codigopro6.Value = item.COD_PRODUCTO;
                        _codigoarea6.Value = item.CODIGO_AREA;
                        _area6.Value = item.AREA;

                        fila6.Cells.Add(_codigopro6);
                        fila6.Cells.Add(_cmbcell6);
                        fila6.Cells.Add(_codigoarea6);
                        fila6.Cells.Add(_area6);

                        dgvOtros.Rows.Add(fila6);
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {

                // throw;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            lab1 = new List<DtoLaboratorioVarios>();
            lab2 = new List<DtoLaboratorioVarios>();
            labMas = new List<DtoLaboratorioVarios>();
            if (dgvHematologia.Rows.Count > 1)
            {
                foreach (DataGridViewRow row in dgvHematologia.Rows)
                {
                    DtoLaboratorioVarios dlb = new DtoLaboratorioVarios();
                    dlb.CODIGO = (string)row.Cells[0].Value;
                    labMas.Add(dlb);
                }

            }
            dgvHematologia.Rows.Clear();
            frm_AyudaLaboratorio frm = new frm_AyudaLaboratorio(1);
            frm.parteMas = labMas;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            lab1 = frm.parteRes1;
            lab2 = frm.parteRes2;
            if (lab1 == null || lab2 == null)
                return;
            foreach (var item in lab1)
            {
                DtoLaboratorioEstructura x = NegLaboratorio.cargadgvHematologiaAyuda(Convert.ToInt64(item.CODIGO));

                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewComboBoxCell _cmbcell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell _codigopro = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _codigoarea = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _area = new DataGridViewTextBoxCell();

                _cmbcell.DataSource = NegLaboratorio.listarProductos(6);
                _cmbcell.DisplayMember = "EXAMEN";
                _cmbcell.ValueMember = "COD_PRODUCTO";
                _cmbcell.Value = x.COD_PRODUCTO;

                _codigopro.Value = x.COD_PRODUCTO;
                _codigoarea.Value = x.CODIGO_AREA;
                _area.Value = x.AREA;

                fila.Cells.Add(_codigopro);
                fila.Cells.Add(_cmbcell);
                fila.Cells.Add(_codigoarea);
                fila.Cells.Add(_area);
                if (dgvHematologia.Rows.Count > 1)
                {
                    bool validador = false;
                    for (int i = 0; i < dgvHematologia.Rows.Count - 1; i++)
                    {
                        if (dgvHematologia.Rows[i].Cells[1].FormattedValue.ToString() == item.EXAMEN)
                        {
                            MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            validador = true;
                            break;
                        }
                    }
                    if (!validador)
                        dgvHematologia.Rows.Add(fila);

                }
                else
                {
                    dgvHematologia.Rows.Add(fila);
                }

            }
            foreach (var item in lab2)
            {
                DtoLaboratorioEstructura x = NegLaboratorio.cargadgvHematologiaAyuda(Convert.ToInt64(item.CODIGO));

                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewComboBoxCell _cmbcell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell _codigopro = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _codigoarea = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _area = new DataGridViewTextBoxCell();

                _cmbcell.DataSource = NegLaboratorio.listarProductos(6);
                _cmbcell.DisplayMember = "EXAMEN";
                _cmbcell.ValueMember = "COD_PRODUCTO";
                _cmbcell.Value = x.COD_PRODUCTO;

                _codigopro.Value = x.COD_PRODUCTO;
                _codigoarea.Value = x.CODIGO_AREA;
                _area.Value = x.AREA;

                fila.Cells.Add(_codigopro);
                fila.Cells.Add(_cmbcell);
                fila.Cells.Add(_codigoarea);
                fila.Cells.Add(_area);
                if (dgvHematologia.Rows.Count > 1)
                {
                    bool validador = false;
                    for (int i = 0; i < dgvHematologia.Rows.Count - 1; i++)
                    {
                        if (dgvHematologia.Rows[i].Cells[1].FormattedValue.ToString() == item.EXAMEN)
                        {
                            MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            validador = true;
                            break;
                        }
                    }
                    if (!validador)
                        dgvHematologia.Rows.Add(fila);

                }
                else
                {
                    dgvHematologia.Rows.Add(fila);
                }

            }
        }

        private void btnUroanalisis_Click(object sender, EventArgs e)
        {
            lab1 = new List<DtoLaboratorioVarios>();
            lab2 = new List<DtoLaboratorioVarios>();
            labMas = new List<DtoLaboratorioVarios>();
            if (dgvUroanalisis.Rows.Count > 1)
            {
                foreach (DataGridViewRow row in dgvUroanalisis.Rows)
                {
                    DtoLaboratorioVarios dlb = new DtoLaboratorioVarios();
                    dlb.CODIGO = (string)row.Cells[0].Value;
                    labMas.Add(dlb);
                }

            }
            dgvUroanalisis.Rows.Clear();
            frm_AyudaLaboratorio frm = new frm_AyudaLaboratorio(2);
            frm.parteMas = labMas;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            lab1 = frm.parteRes1;
            lab2 = frm.parteRes2;
            if (lab1 == null || lab2 == null)
                return;
            foreach (var item in lab1)
            {
                DtoLaboratorioEstructura x = NegLaboratorio.cargadgvUroanalisiAyuda(Convert.ToInt64(item.CODIGO));

                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewComboBoxCell _cmbcell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell _codigopro = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _codigoarea = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _area = new DataGridViewTextBoxCell();

                _cmbcell.DataSource = NegLaboratorio.listarProductos(7);
                _cmbcell.DisplayMember = "EXAMEN";
                _cmbcell.ValueMember = "COD_PRODUCTO";
                _cmbcell.Value = x.COD_PRODUCTO;

                _codigopro.Value = x.COD_PRODUCTO;
                _codigoarea.Value = x.CODIGO_AREA;
                _area.Value = x.AREA;

                fila.Cells.Add(_codigopro);
                fila.Cells.Add(_cmbcell);
                fila.Cells.Add(_codigoarea);
                fila.Cells.Add(_area);
                if (dgvUroanalisis.Rows.Count > 1)
                {
                    bool validador = false;
                    for (int i = 0; i < dgvUroanalisis.Rows.Count - 1; i++)
                    {
                        if (dgvUroanalisis.Rows[i].Cells[1].FormattedValue.ToString() == item.EXAMEN)
                        {
                            MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            validador = true;
                            break;
                        }
                    }
                    if (!validador)
                        dgvUroanalisis.Rows.Add(fila);

                }
                else
                {
                    dgvUroanalisis.Rows.Add(fila);
                }

            }
            foreach (var item in lab2)
            {
                DtoLaboratorioEstructura x = NegLaboratorio.cargadgvUroanalisiAyuda(Convert.ToInt64(item.CODIGO));

                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewComboBoxCell _cmbcell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell _codigopro = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _codigoarea = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _area = new DataGridViewTextBoxCell();

                _cmbcell.DataSource = NegLaboratorio.listarProductos(7);
                _cmbcell.DisplayMember = "EXAMEN";
                _cmbcell.ValueMember = "COD_PRODUCTO";
                _cmbcell.Value = x.COD_PRODUCTO;

                _codigopro.Value = x.COD_PRODUCTO;
                _codigoarea.Value = x.CODIGO_AREA;
                _area.Value = x.AREA;

                fila.Cells.Add(_codigopro);
                fila.Cells.Add(_cmbcell);
                fila.Cells.Add(_codigoarea);
                fila.Cells.Add(_area);
                if (dgvUroanalisis.Rows.Count > 1)
                {
                    bool validador = false;
                    for (int i = 0; i < dgvUroanalisis.Rows.Count - 1; i++)
                    {
                        if (dgvUroanalisis.Rows[i].Cells[1].FormattedValue.ToString() == item.EXAMEN)
                        {
                            MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            validador = true;
                            break;
                        }
                    }
                    if (!validador)
                        dgvUroanalisis.Rows.Add(fila);

                }
                else
                {
                    dgvUroanalisis.Rows.Add(fila);
                }

            }
        }

        private void btnCoprologico_Click(object sender, EventArgs e)
        {
            lab1 = new List<DtoLaboratorioVarios>();
            lab2 = new List<DtoLaboratorioVarios>();
            labMas = new List<DtoLaboratorioVarios>();
            if (dvgCoprologico.Rows.Count > 1)
            {
                foreach (DataGridViewRow row in dvgCoprologico.Rows)
                {
                    DtoLaboratorioVarios dlb = new DtoLaboratorioVarios();
                    dlb.CODIGO = (string)row.Cells[0].Value;
                    labMas.Add(dlb);
                }

            }
            dvgCoprologico.Rows.Clear();
            frm_AyudaLaboratorio frm = new frm_AyudaLaboratorio(3);
            frm.parteMas = labMas;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            lab1 = frm.parteRes1;
            lab2 = frm.parteRes2;
            if (lab1 == null || lab2 == null)
                return;
            foreach (var item in lab1)
            {
                DtoLaboratorioEstructura x = NegLaboratorio.cargadgvCoprologicoAyuda(Convert.ToInt64(item.CODIGO));

                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewComboBoxCell _cmbcell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell _codigopro = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _codigoarea = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _area = new DataGridViewTextBoxCell();

                _cmbcell.DataSource = NegLaboratorio.listarProductos(8);
                _cmbcell.DisplayMember = "EXAMEN";
                _cmbcell.ValueMember = "COD_PRODUCTO";
                _cmbcell.Value = x.COD_PRODUCTO;

                _codigopro.Value = x.COD_PRODUCTO;
                _codigoarea.Value = x.CODIGO_AREA;
                _area.Value = x.AREA;

                fila.Cells.Add(_codigopro);
                fila.Cells.Add(_cmbcell);
                fila.Cells.Add(_codigoarea);
                fila.Cells.Add(_area);
                if (dvgCoprologico.Rows.Count > 1)
                {
                    bool validador = false;
                    for (int i = 0; i < dvgCoprologico.Rows.Count - 1; i++)
                    {
                        if (dvgCoprologico.Rows[i].Cells[1].FormattedValue.ToString() == item.EXAMEN)
                        {
                            MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            validador = true;
                            break;
                        }
                    }
                    if (!validador)
                        dvgCoprologico.Rows.Add(fila);

                }
                else
                {
                    dvgCoprologico.Rows.Add(fila);
                }

            }
            foreach (var item in lab2)
            {
                DtoLaboratorioEstructura x = NegLaboratorio.cargadgvCoprologicoAyuda(Convert.ToInt64(item.CODIGO));

                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewComboBoxCell _cmbcell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell _codigopro = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _codigoarea = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _area = new DataGridViewTextBoxCell();

                _cmbcell.DataSource = NegLaboratorio.listarProductos(8);
                _cmbcell.DisplayMember = "EXAMEN";
                _cmbcell.ValueMember = "COD_PRODUCTO";
                _cmbcell.Value = x.COD_PRODUCTO;

                _codigopro.Value = x.COD_PRODUCTO;
                _codigoarea.Value = x.CODIGO_AREA;
                _area.Value = x.AREA;

                fila.Cells.Add(_codigopro);
                fila.Cells.Add(_cmbcell);
                fila.Cells.Add(_codigoarea);
                fila.Cells.Add(_area);
                if (dvgCoprologico.Rows.Count > 1)
                {
                    bool validador = false;
                    for (int i = 0; i < dvgCoprologico.Rows.Count - 1; i++)
                    {
                        if (dvgCoprologico.Rows[i].Cells[1].FormattedValue.ToString() == item.EXAMEN)
                        {
                            MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            validador = true;
                            break;
                        }
                    }
                    if (!validador)
                        dvgCoprologico.Rows.Add(fila);

                }
                else
                {
                    dvgCoprologico.Rows.Add(fila);
                }

            }
        }

        private void btnQsangui_Click(object sender, EventArgs e)
        {
            lab1 = new List<DtoLaboratorioVarios>();
            lab2 = new List<DtoLaboratorioVarios>();
            labMas = new List<DtoLaboratorioVarios>();
            if (dgvQSanguinea.Rows.Count > 1)
            {
                foreach (DataGridViewRow row in dgvQSanguinea.Rows)
                {
                    DtoLaboratorioVarios dlb = new DtoLaboratorioVarios();
                    dlb.CODIGO = (string)row.Cells[0].Value;
                    labMas.Add(dlb);
                }

            }
            dgvQSanguinea.Rows.Clear();
            frm_AyudaLaboratorio frm = new frm_AyudaLaboratorio(4);
            frm.parteMas = labMas;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            lab1 = frm.parteRes1;
            lab2 = frm.parteRes2;
            if (lab1 == null || lab2 == null)
                return;
            foreach (var item in lab1)
            {
                DtoLaboratorioEstructura x = NegLaboratorio.cargadgvQsanguineaAyuda(Convert.ToInt64(item.CODIGO));

                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewComboBoxCell _cmbcell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell _codigopro = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _codigoarea = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _area = new DataGridViewTextBoxCell();

                _cmbcell.DataSource = NegLaboratorio.listarProductos(9);
                _cmbcell.DisplayMember = "EXAMEN";
                _cmbcell.ValueMember = "COD_PRODUCTO";
                _cmbcell.Value = x.COD_PRODUCTO;

                _codigopro.Value = x.COD_PRODUCTO;
                _codigoarea.Value = x.CODIGO_AREA;
                _area.Value = x.AREA;

                fila.Cells.Add(_codigopro);
                fila.Cells.Add(_cmbcell);
                fila.Cells.Add(_codigoarea);
                fila.Cells.Add(_area);
                if (dgvQSanguinea.Rows.Count > 1)
                {
                    bool validador = false;
                    for (int i = 0; i < dgvQSanguinea.Rows.Count - 1; i++)
                    {
                        if (dgvQSanguinea.Rows[i].Cells[1].FormattedValue.ToString() == item.EXAMEN)
                        {
                            MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            validador = true;
                            break;
                        }
                    }
                    if (!validador)
                        dgvQSanguinea.Rows.Add(fila);

                }
                else
                {
                    dgvQSanguinea.Rows.Add(fila);
                }

            }
            foreach (var item in lab2)
            {
                DtoLaboratorioEstructura x = NegLaboratorio.cargadgvQsanguineaAyuda(Convert.ToInt64(item.CODIGO));

                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewComboBoxCell _cmbcell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell _codigopro = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _codigoarea = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _area = new DataGridViewTextBoxCell();

                _cmbcell.DataSource = NegLaboratorio.listarProductos(9);
                _cmbcell.DisplayMember = "EXAMEN";
                _cmbcell.ValueMember = "COD_PRODUCTO";
                _cmbcell.Value = x.COD_PRODUCTO;

                _codigopro.Value = x.COD_PRODUCTO;
                _codigoarea.Value = x.CODIGO_AREA;
                _area.Value = x.AREA;

                fila.Cells.Add(_codigopro);
                fila.Cells.Add(_cmbcell);
                fila.Cells.Add(_codigoarea);
                fila.Cells.Add(_area);
                if (dgvQSanguinea.Rows.Count > 1)
                {
                    bool validador = false;
                    for (int i = 0; i < dgvQSanguinea.Rows.Count - 1; i++)
                    {
                        if (dgvQSanguinea.Rows[i].Cells[1].FormattedValue.ToString() == item.EXAMEN)
                        {
                            MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            validador = true;
                            break;
                        }
                    }
                    if (!validador)
                        dgvQSanguinea.Rows.Add(fila);

                }
                else
                {
                    dgvQSanguinea.Rows.Add(fila);
                }

            }
        }

        private void btnSerologia_Click(object sender, EventArgs e)
        {
            lab1 = new List<DtoLaboratorioVarios>();
            lab2 = new List<DtoLaboratorioVarios>();
            labMas = new List<DtoLaboratorioVarios>();
            if (dgvSerologia.Rows.Count > 1)
            {
                foreach (DataGridViewRow row in dgvSerologia.Rows)
                {
                    DtoLaboratorioVarios dlb = new DtoLaboratorioVarios();
                    dlb.CODIGO = (string)row.Cells[0].Value;
                    labMas.Add(dlb);
                }

            }
            dgvSerologia.Rows.Clear();
            frm_AyudaLaboratorio frm = new frm_AyudaLaboratorio(5);
            frm.parteMas = labMas;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            lab1 = frm.parteRes1;
            lab2 = frm.parteRes2;
            if (lab1 == null || lab2 == null)
                return;
            foreach (var item in lab1)
            {
                DtoLaboratorioEstructura x = NegLaboratorio.cargadgvSerologiaAyuda(Convert.ToInt64(item.CODIGO));

                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewComboBoxCell _cmbcell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell _codigopro = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _codigoarea = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _area = new DataGridViewTextBoxCell();

                _cmbcell.DataSource = NegLaboratorio.listarProductos(10);
                _cmbcell.DisplayMember = "EXAMEN";
                _cmbcell.ValueMember = "COD_PRODUCTO";
                _cmbcell.Value = x.COD_PRODUCTO;

                _codigopro.Value = x.COD_PRODUCTO;
                _codigoarea.Value = x.CODIGO_AREA;
                _area.Value = x.AREA;

                fila.Cells.Add(_codigopro);
                fila.Cells.Add(_cmbcell);
                fila.Cells.Add(_codigoarea);
                fila.Cells.Add(_area);
                if (dgvSerologia.Rows.Count > 1)
                {
                    bool validador = false;
                    for (int i = 0; i < dgvSerologia.Rows.Count - 1; i++)
                    {
                        if (dgvSerologia.Rows[i].Cells[1].FormattedValue.ToString() == item.EXAMEN)
                        {
                            MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            validador = true;
                            break;
                        }
                    }
                    if (!validador)
                        dgvSerologia.Rows.Add(fila);

                }
                else
                {
                    dgvSerologia.Rows.Add(fila);
                }

            }
            foreach (var item in lab2)
            {
                DtoLaboratorioEstructura x = NegLaboratorio.cargadgvSerologiaAyuda(Convert.ToInt64(item.CODIGO));

                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewComboBoxCell _cmbcell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell _codigopro = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _codigoarea = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _area = new DataGridViewTextBoxCell();

                _cmbcell.DataSource = NegLaboratorio.listarProductos(10);
                _cmbcell.DisplayMember = "EXAMEN";
                _cmbcell.ValueMember = "COD_PRODUCTO";
                _cmbcell.Value = x.COD_PRODUCTO;

                _codigopro.Value = x.COD_PRODUCTO;
                _codigoarea.Value = x.CODIGO_AREA;
                _area.Value = x.AREA;

                fila.Cells.Add(_codigopro);
                fila.Cells.Add(_cmbcell);
                fila.Cells.Add(_codigoarea);
                fila.Cells.Add(_area);
                if (dgvSerologia.Rows.Count > 1)
                {
                    bool validador = false;
                    for (int i = 0; i < dgvSerologia.Rows.Count - 1; i++)
                    {
                        if (dgvSerologia.Rows[i].Cells[1].FormattedValue.ToString() == item.EXAMEN)
                        {
                            MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            validador = true;
                            break;
                        }
                    }
                    if (!validador)
                        dgvSerologia.Rows.Add(fila);

                }
                else
                {
                    dgvSerologia.Rows.Add(fila);
                }

            }
        }

        private void btnBacteriologia_Click(object sender, EventArgs e)
        {
            lab1 = new List<DtoLaboratorioVarios>();
            lab2 = new List<DtoLaboratorioVarios>();
            labMas = new List<DtoLaboratorioVarios>();
            if (dgvBacteriologia.Rows.Count > 1)
            {
                foreach (DataGridViewRow row in dgvBacteriologia.Rows)
                {
                    DtoLaboratorioVarios dlb = new DtoLaboratorioVarios();
                    dlb.CODIGO = (string)row.Cells[0].Value;
                    labMas.Add(dlb);
                }

            }
            dgvBacteriologia.Rows.Clear();
            frm_AyudaLaboratorio frm = new frm_AyudaLaboratorio(6);
            frm.parteMas = labMas;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            lab1 = frm.parteRes1;
            lab2 = frm.parteRes2;
            if (lab1 == null || lab2 == null)
                return;
            foreach (var item in lab1)
            {
                DtoLaboratorioEstructura x = NegLaboratorio.cargadgvBacteriologiaAyuda(Convert.ToInt64(item.CODIGO));

                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewComboBoxCell _cmbcell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell _codigopro = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _codigoarea = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _area = new DataGridViewTextBoxCell();

                _cmbcell.DataSource = NegLaboratorio.listarProductos(11);
                _cmbcell.DisplayMember = "EXAMEN";
                _cmbcell.ValueMember = "COD_PRODUCTO";
                _cmbcell.Value = x.COD_PRODUCTO;

                _codigopro.Value = x.COD_PRODUCTO;
                _codigoarea.Value = x.CODIGO_AREA;
                _area.Value = x.AREA;

                fila.Cells.Add(_codigopro);
                fila.Cells.Add(_cmbcell);
                fila.Cells.Add(_codigoarea);
                fila.Cells.Add(_area);
                if (dgvBacteriologia.Rows.Count > 1)
                {
                    bool validador = false;
                    for (int i = 0; i < dgvBacteriologia.Rows.Count - 1; i++)
                    {
                        if (dgvBacteriologia.Rows[i].Cells[1].FormattedValue.ToString() == item.EXAMEN)
                        {
                            MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            validador = true;
                            break;
                        }
                    }
                    if (!validador)
                        dgvBacteriologia.Rows.Add(fila);

                }
                else
                {
                    dgvBacteriologia.Rows.Add(fila);
                }

            }
            foreach (var item in lab2)
            {
                DtoLaboratorioEstructura x = NegLaboratorio.cargadgvBacteriologiaAyuda(Convert.ToInt64(item.CODIGO));

                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewComboBoxCell _cmbcell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell _codigopro = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _codigoarea = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _area = new DataGridViewTextBoxCell();

                _cmbcell.DataSource = NegLaboratorio.listarProductos(11);
                _cmbcell.DisplayMember = "EXAMEN";
                _cmbcell.ValueMember = "COD_PRODUCTO";
                _cmbcell.Value = x.COD_PRODUCTO;

                _codigopro.Value = x.COD_PRODUCTO;
                _codigoarea.Value = x.CODIGO_AREA;
                _area.Value = x.AREA;

                fila.Cells.Add(_codigopro);
                fila.Cells.Add(_cmbcell);
                fila.Cells.Add(_codigoarea);
                fila.Cells.Add(_area);
                if (dgvBacteriologia.Rows.Count > 1)
                {
                    bool validador = false;
                    for (int i = 0; i < dgvBacteriologia.Rows.Count - 1; i++)
                    {
                        if (dgvBacteriologia.Rows[i].Cells[1].FormattedValue.ToString() == item.EXAMEN)
                        {
                            MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            validador = true;
                            break;
                        }
                    }
                    if (!validador)
                        dgvBacteriologia.Rows.Add(fila);

                }
                else
                {
                    dgvBacteriologia.Rows.Add(fila);
                }

            }
        }

        private void btnOtros_Click(object sender, EventArgs e)
        {
            lab1 = new List<DtoLaboratorioVarios>();
            lab2 = new List<DtoLaboratorioVarios>();
            labMas = new List<DtoLaboratorioVarios>();
            if (dgvOtros.Rows.Count > 1)
            {
                foreach (DataGridViewRow row in dgvOtros.Rows)
                {
                    DtoLaboratorioVarios dlb = new DtoLaboratorioVarios();
                    dlb.CODIGO = (string)row.Cells[0].Value;
                    labMas.Add(dlb);
                }

            }
            dgvOtros.Rows.Clear();
            frm_AyudaLaboratorio frm = new frm_AyudaLaboratorio(7);
            frm.parteMas = labMas;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            lab1 = frm.parteRes1;
            lab2 = frm.parteRes2;
            if (lab1 == null || lab2 == null)
                return;
            foreach (var item in lab1)
            {
                DtoLaboratorioEstructura x = NegLaboratorio.cargadgvOtrosAyuda(Convert.ToInt64(item.CODIGO));

                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewComboBoxCell _cmbcell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell _codigopro = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _codigoarea = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _area = new DataGridViewTextBoxCell();

                _cmbcell.DataSource = NegLaboratorio.listarProductos(12);
                _cmbcell.DisplayMember = "EXAMEN";
                _cmbcell.ValueMember = "COD_PRODUCTO";
                _cmbcell.Value = x.COD_PRODUCTO;

                _codigopro.Value = x.COD_PRODUCTO;
                _codigoarea.Value = x.CODIGO_AREA;
                _area.Value = x.AREA;

                fila.Cells.Add(_codigopro);
                fila.Cells.Add(_cmbcell);
                fila.Cells.Add(_codigoarea);
                fila.Cells.Add(_area);
                if (dgvOtros.Rows.Count > 1)
                {
                    bool validador = false;
                    for (int i = 0; i < dgvOtros.Rows.Count - 1; i++)
                    {
                        if (dgvOtros.Rows[i].Cells[1].FormattedValue.ToString() == item.EXAMEN)
                        {
                            MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            validador = true;
                            break;
                        }
                    }
                    if (!validador)
                        dgvOtros.Rows.Add(fila);

                }
                else
                {
                    dgvOtros.Rows.Add(fila);
                }

            }
            foreach (var item in lab2)
            {
                DtoLaboratorioEstructura x = NegLaboratorio.cargadgvOtrosAyuda(Convert.ToInt64(item.CODIGO));

                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewComboBoxCell _cmbcell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell _codigopro = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _codigoarea = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _area = new DataGridViewTextBoxCell();

                _cmbcell.DataSource = NegLaboratorio.listarProductos(12);
                _cmbcell.DisplayMember = "EXAMEN";
                _cmbcell.ValueMember = "COD_PRODUCTO";
                _cmbcell.Value = x.COD_PRODUCTO;

                _codigopro.Value = x.COD_PRODUCTO;
                _codigoarea.Value = x.CODIGO_AREA;
                _area.Value = x.AREA;

                fila.Cells.Add(_codigopro);
                fila.Cells.Add(_cmbcell);
                fila.Cells.Add(_codigoarea);
                fila.Cells.Add(_area);
                if (dgvOtros.Rows.Count > 1)
                {
                    bool validador = false;
                    for (int i = 0; i < dgvOtros.Rows.Count - 1; i++)
                    {
                        if (dgvOtros.Rows[i].Cells[1].FormattedValue.ToString() == item.EXAMEN)
                        {
                            MessageBox.Show("Este examén ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            validador = true;
                            break;
                        }
                    }
                    if (!validador)
                        dgvOtros.Rows.Add(fila);

                }
                else
                {
                    dgvOtros.Rows.Add(fila);
                }

            }
        }
    }
}
