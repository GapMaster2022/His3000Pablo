using His.Entidades;
using His.Negocio;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Formulario
{
    public partial class frmIngestaEliminacion : Form
    {
        USUARIOS usr = new USUARIOS();
        ATENCIONES atencion = null;
        PACIENTES paciente = null;
        MEDICOS medico = null;
        public Int64 CodigoAtencion = 0;
        DataTable Ingesta = new DataTable();
        DataTable Eliminacion = new DataTable();
        Int32 IE_CODIGO = 0;
        Int32 ID_USUARIO = 0;
        bool modo = true;
        bool validacion = true;
        HC_INGESTA_ELIMINACION_DETALLE det = new HC_INGESTA_ELIMINACION_DETALLE();
        Int64 cod = 0;
        string compuesto = "";
        bool valorSvActividad = false;
        public frmIngestaEliminacion(Int64 ate_codigo, string hc)
        {
            InitializeComponent();
            cargaGrid();
            CodigoAtencion = ate_codigo;
            dtp_Creacion.Value = DateTime.Now;
            CargarAtencion(CodigoAtencion);
            refrescarSolicitudes();
            habilitarGrid(false);
            HabilitarBotones(true, false, false, false, false);

        }
        public void cargaGrid()
        {
            cmbDrenaje.DataSource = NegIngestaEliminacion.cargaDrenaje();
            cmbDrenaje.ValueMember = "TDR_CODIGO";
            cmbDrenaje.DisplayMember = "TDR_DESCRIPCION";
            cmbDrenaje.SelectedIndex = -1;
        }
        private void CargarAtencion(Int64 codAtencion)
        {
            atencion = NegAtenciones.RecuperarAtencionID(codAtencion);
            lblSeguro.Text = atencion.HABITACIONES.hab_Numero;
            lblatencion.Text = atencion.ATE_NUMERO_ATENCION;
            cargarPaciente(atencion.PACIENTES.PAC_CODIGO);

            List<MEDICOS> medicos = NegMedicos.listaMedicos();
            int codigoMedico = medicos.FirstOrDefault(m => m.EntityKey == atencion.MEDICOSReference.EntityKey).MED_CODIGO;
            if (codigoMedico != 0)
                cargarMedico(codigoMedico);
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
                lblHc.Text = paciente.PAC_HISTORIA_CLINICA;
                lblsexo.Text = paciente.PAC_GENERO;
                lblidentificacion.Text = paciente.PAC_IDENTIFICACION;
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

        private void gridSol_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            HabilitarBotones(false, false, true, true, true);
            limpiar();
            modo = false;
            cargarGrids(Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["IE_CODIGO"].Value.ToString()));
            IE_CODIGO = Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["IE_CODIGO"].Value.ToString());
            habilitarGrid(false);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            valorSvActividad = NegSignosVitales.validadeSv((Int32)CodigoAtencion, dtp_Creacion.Value);
            if (valorSvActividad)
                txtDescripcionR.Enabled = true; //No se valida si no es Ambulatorio 20240313 Andrés
            DataTable ing = NegIngestaEliminacion.ingestaXfecha((Int32)CodigoAtencion, dtp_Creacion.Value);
            if (ing.Rows.Count == 0)
            {
                habilitarGrid(true);
                HabilitarBotones(false, true, false, false, true);
                modo = true;
            }
            else
                MessageBox.Show("Fecha ya registrada", "His-3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarIngestaEliminacion();
            refrescarSolicitudes();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirFormulario();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            valorSvActividad = NegSignosVitales.validadeSv((Int32)CodigoAtencion, dtp_Creacion.Value);
            if (valorSvActividad)
                txtDescripcionR.Enabled = true;
            habilitarGrid(true);
            HabilitarBotones(false, true, true, false, true);
            modo = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            DataTable vSignos = new DataTable();
            vSignos = NegIngestaEliminacion.ExistenciaIngElm((Int32)CodigoAtencion, paciente.PAC_CODIGO);
            HabilitarBotones(true, false, false, false, false);
            habilitarGrid(false);
            modo = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void limpiar()
        {
            dtgOral.Rows.Clear();
            dtgOralt.Rows.Clear();
            dtgOralN.Rows.Clear();
            dtgParental.Rows.Clear();
            dtgParentalT.Rows.Clear();
            dtgParentalN.Rows.Clear();
            dtgEnteral.Rows.Clear();
            dtgEnteralT.Rows.Clear();
            dtgEnteralN.Rows.Clear();
            dtgOrina.Rows.Clear();
            dtgOrinaT.Rows.Clear();
            dtgOrinaN.Rows.Clear();
            dtgOtros.Rows.Clear();
            dtgOtrosT.Rows.Clear();
            dtgOtrosN.Rows.Clear();
            dtgVomito.Rows.Clear();
            dtgVomitoT.Rows.Clear();
            dtgVomitoN.Rows.Clear();
            dtgDeposiciones.Rows.Clear();
            dtgDeposicionesT.Rows.Clear();
            dtgDeposicionesN.Rows.Clear();
            dtgDrenaje.Rows.Clear();
            dtgDrenajeT.Rows.Clear();
            dtgDrenajeN.Rows.Clear();
            valorSvActividad = false;
            txtDescripcionR.Enabled = false;
            dtp_Creacion.Value = DateTime.Now;
        }
        private void cargarGrids(Int32 IE_CODIGO)
        {
            try
            {
                HC_INGESTA_ELIMINACION ingElm = NegIngestaEliminacion.recuperaIngestaXcodigo(IE_CODIGO);
                dtp_Creacion.Value = (DateTime)ingElm.IE_FECHA;
                DataTable OralM = NegIngestaEliminacion.cargaGrid("IOM", IE_CODIGO);
                DataTable OralT = NegIngestaEliminacion.cargaGrid("IOT", IE_CODIGO);
                DataTable OralN = NegIngestaEliminacion.cargaGrid("ION", IE_CODIGO);
                DataTable ParentalM = NegIngestaEliminacion.cargaGrid("IPM", IE_CODIGO);
                DataTable ParentalT = NegIngestaEliminacion.cargaGrid("IPT", IE_CODIGO);
                DataTable ParentalN = NegIngestaEliminacion.cargaGrid("IPN", IE_CODIGO);
                DataTable EnteralM = NegIngestaEliminacion.cargaGrid("IEM", IE_CODIGO);
                DataTable EnteralT = NegIngestaEliminacion.cargaGrid("IET", IE_CODIGO);
                DataTable EnteralN = NegIngestaEliminacion.cargaGrid("IEN", IE_CODIGO);
                DataTable OrinaM = NegIngestaEliminacion.cargaGrid("EOM", IE_CODIGO);
                DataTable OrinaT = NegIngestaEliminacion.cargaGrid("EOT", IE_CODIGO);
                DataTable OrinaN = NegIngestaEliminacion.cargaGrid("EON", IE_CODIGO);
                DataTable OtrosM = NegIngestaEliminacion.cargaGrid("ERM", IE_CODIGO);
                DataTable OtrosT = NegIngestaEliminacion.cargaGrid("ERT", IE_CODIGO);
                DataTable OtrosN = NegIngestaEliminacion.cargaGrid("ERN", IE_CODIGO);
                DataTable VomitoM = NegIngestaEliminacion.cargaGrid("EVM", IE_CODIGO);
                DataTable VomitoT = NegIngestaEliminacion.cargaGrid("EVT", IE_CODIGO);
                DataTable VomitoN = NegIngestaEliminacion.cargaGrid("EVN", IE_CODIGO);
                DataTable DrenajeM = NegIngestaEliminacion.cargaGrid("EDM", IE_CODIGO);
                DataTable DrenajeT = NegIngestaEliminacion.cargaGrid("EDT", IE_CODIGO);
                DataTable DrenajeN = NegIngestaEliminacion.cargaGrid("EDN", IE_CODIGO);
                DataTable DeposicionM = NegIngestaEliminacion.cargaGrid("EPM", IE_CODIGO);
                DataTable DeposicionT = NegIngestaEliminacion.cargaGrid("EPT", IE_CODIGO);
                DataTable DeposicionN = NegIngestaEliminacion.cargaGrid("EPN", IE_CODIGO);
                for (int i = 0; i < OralM.Rows.Count; i++)
                {
                    dtgOral.Rows.Add(OralM.Rows[i][0].ToString(), OralM.Rows[i][2].ToString(), OralM.Rows[i][3].ToString(), OralM.Rows[i][4].ToString());
                }
                for (int i = 0; i < OralT.Rows.Count; i++)
                {
                    dtgOralt.Rows.Add(OralT.Rows[i][0].ToString(), OralT.Rows[i][2].ToString(), OralT.Rows[i][3].ToString(), OralT.Rows[i][4].ToString());
                }
                for (int i = 0; i < OralN.Rows.Count; i++)
                {
                    dtgOralN.Rows.Add(OralN.Rows[i][0].ToString(), OralN.Rows[i][2].ToString(), OralN.Rows[i][3].ToString(), OralN.Rows[i][4].ToString());
                }
                for (int i = 0; i < ParentalM.Rows.Count; i++)
                {
                    dtgParental.Rows.Add(ParentalM.Rows[i][0].ToString(), ParentalM.Rows[i][2].ToString(), ParentalM.Rows[i][3].ToString(), ParentalM.Rows[i][4].ToString());
                }
                for (int i = 0; i < ParentalT.Rows.Count; i++)
                {
                    dtgParentalT.Rows.Add(ParentalT.Rows[i][0].ToString(), ParentalT.Rows[i][2].ToString(), ParentalT.Rows[i][3].ToString(), ParentalT.Rows[i][4].ToString());
                }
                for (int i = 0; i < ParentalN.Rows.Count; i++)
                {
                    dtgParentalN.Rows.Add(ParentalN.Rows[i][0].ToString(), ParentalN.Rows[i][2].ToString(), ParentalN.Rows[i][3].ToString(), ParentalN.Rows[i][4].ToString());
                }
                for (int i = 0; i < EnteralM.Rows.Count; i++) //ENTERAL DE MAÑANA
                {
                    dtgEnteral.Rows.Add(EnteralM.Rows[i][0].ToString(), EnteralM.Rows[i][2].ToString(), EnteralM.Rows[i][3].ToString(), EnteralM.Rows[i][4].ToString());
                }
                for (int i = 0; i < EnteralT.Rows.Count; i++) //ENTERAL DE TARDE
                {
                    dtgEnteralT.Rows.Add(EnteralT.Rows[i][0].ToString(), EnteralT.Rows[i][2].ToString(), EnteralT.Rows[i][3].ToString(), EnteralT.Rows[i][4].ToString());
                }
                for (int i = 0; i < EnteralN.Rows.Count; i++) //ENTERAL DE NOCHE
                {
                    dtgEnteralN.Rows.Add(EnteralN.Rows[i][0].ToString(), EnteralN.Rows[i][2].ToString(), EnteralN.Rows[i][3].ToString(), EnteralN.Rows[i][4].ToString());
                }
                for (int i = 0; i < OrinaM.Rows.Count; i++)
                {
                    dtgOrina.Rows.Add(OrinaM.Rows[i][0].ToString(), OrinaM.Rows[i][2].ToString(), OrinaM.Rows[i][3].ToString(), OrinaM.Rows[i][4].ToString(), OrinaM.Rows[i][7].ToString());
                }
                for (int i = 0; i < OrinaT.Rows.Count; i++)
                {
                    dtgOrinaT.Rows.Add(OrinaT.Rows[i][0].ToString(), OrinaT.Rows[i][2].ToString(), OrinaT.Rows[i][3].ToString(), OrinaT.Rows[i][4].ToString(), OrinaT.Rows[i][7].ToString());
                }
                for (int i = 0; i < OrinaN.Rows.Count; i++)
                {
                    dtgOrinaN.Rows.Add(OrinaN.Rows[i][0].ToString(), OrinaN.Rows[i][2].ToString(), OrinaN.Rows[i][3].ToString(), OrinaN.Rows[i][4].ToString(), OrinaN.Rows[i][7].ToString());
                }
                for (int i = 0; i < OtrosM.Rows.Count; i++)
                {
                    dtgOtros.Rows.Add(OtrosM.Rows[i][0].ToString(), OtrosM.Rows[i][2].ToString(), OtrosM.Rows[i][3].ToString(), OtrosM.Rows[i][4].ToString());
                }
                for (int i = 0; i < OtrosT.Rows.Count; i++)
                {
                    dtgOtrosT.Rows.Add(OtrosT.Rows[i][0].ToString(), OtrosT.Rows[i][2].ToString(), OtrosT.Rows[i][3].ToString(), OtrosT.Rows[i][4].ToString());
                }
                for (int i = 0; i < OtrosN.Rows.Count; i++)
                {
                    dtgOtrosN.Rows.Add(OtrosN.Rows[i][0].ToString(), OtrosN.Rows[i][2].ToString(), OtrosN.Rows[i][3].ToString(), OtrosN.Rows[i][4].ToString());
                }
                for (int i = 0; i < VomitoM.Rows.Count; i++)
                {
                    dtgVomito.Rows.Add(VomitoM.Rows[i][0].ToString(), VomitoM.Rows[i][2].ToString(), VomitoM.Rows[i][3].ToString(), VomitoM.Rows[i][4].ToString());
                }
                for (int i = 0; i < VomitoT.Rows.Count; i++)
                {
                    dtgVomitoT.Rows.Add(VomitoT.Rows[i][0].ToString(), VomitoT.Rows[i][2].ToString(), VomitoT.Rows[i][3].ToString(), VomitoT.Rows[i][4].ToString());
                }
                for (int i = 0; i < VomitoN.Rows.Count; i++)
                {
                    dtgVomitoN.Rows.Add(VomitoN.Rows[i][0].ToString(), VomitoN.Rows[i][2].ToString(), VomitoN.Rows[i][3].ToString(), VomitoN.Rows[i][4].ToString());
                }
                for (int i = 0; i < DrenajeM.Rows.Count; i++)
                {
                    dtgDrenaje.Rows.Add(DrenajeM.Rows[i][0].ToString(), DrenajeM.Rows[i][2].ToString(), DrenajeM.Rows[i][3].ToString(), DrenajeM.Rows[i][4].ToString());
                }
                for (int i = 0; i < DrenajeT.Rows.Count; i++)
                {
                    dtgDrenajeT.Rows.Add(DrenajeT.Rows[i][0].ToString(), DrenajeT.Rows[i][2].ToString(), DrenajeT.Rows[i][3].ToString(), DrenajeT.Rows[i][4].ToString());
                }
                for (int i = 0; i < DrenajeN.Rows.Count; i++)
                {
                    dtgDrenajeN.Rows.Add(DrenajeN.Rows[i][0].ToString(), DrenajeN.Rows[i][2].ToString(), DrenajeN.Rows[i][3].ToString(), DrenajeN.Rows[i][4].ToString());
                }
                for (int i = 0; i < DeposicionM.Rows.Count; i++)
                {
                    dtgDeposiciones.Rows.Add(DeposicionM.Rows[i][0].ToString(), DeposicionM.Rows[i][2].ToString(), DeposicionM.Rows[i][3].ToString(), DeposicionM.Rows[i][4].ToString(), DeposicionM.Rows[i][7].ToString());
                }
                for (int i = 0; i < DeposicionT.Rows.Count; i++)
                {
                    dtgDeposicionesT.Rows.Add(DeposicionT.Rows[i][0].ToString(), DeposicionT.Rows[i][2].ToString(), DeposicionT.Rows[i][3].ToString(), DeposicionT.Rows[i][4].ToString(), DeposicionT.Rows[i][7].ToString());
                }
                for (int i = 0; i < DeposicionN.Rows.Count; i++)
                {
                    dtgDeposicionesN.Rows.Add(DeposicionN.Rows[i][0].ToString(), DeposicionN.Rows[i][2].ToString(), DeposicionN.Rows[i][3].ToString(), DeposicionN.Rows[i][4].ToString(), DeposicionN.Rows[i][7].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }


        }
        private void GuardarIngestaEliminacion()
        {
            try
            {
                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                usuario.ShowDialog();
                if (!usuario.aceptado)
                    return;
                if (validaGrid())
                {
                    //IngElm = NegIngestaEliminacion.ExistenciaIngElm((Int32)CodigoAtencion, paciente.PAC_CODIGO);
                    if (modo)
                        GrabarIngElm();
                    GrabaDatos(usuario.usuarioActual);
                    ModificaSV();
                    MessageBox.Show("Registro Guardado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    errorProvider1.Clear();
                    habilitarGrid(false);
                    limpiar();
                    HabilitarBotones(true, false, false, false, false);
                    ImprimirFormulario();
                }
                else
                    MessageBox.Show("Faltan campos por llenar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }

        public bool validaGrid()
        {
            errorProvider1.Clear();
            validacion = true;
            for (int i = 0; i < dtgOral.Rows.Count - 1; i++)
            {
                if (dtgOral.Rows[i].Cells[2].Value == null || dtgOral.Rows[i].Cells[3].Value == null)
                {
                    errorProvider1.SetError(grbOral, "Campos incompletos");
                    validacion = false;
                }
            }
            for (int i = 0; i < dtgOralt.Rows.Count - 1; i++)
            {
                if (dtgOralt.Rows[i].Cells[2].Value == null || dtgOralt.Rows[i].Cells[3].Value == null)
                {
                    errorProvider1.SetError(grbOralT, "Campos incompletos");
                    validacion = false;
                }
            }
            for (int i = 0; i < dtgOralN.Rows.Count - 1; i++)
            {
                if (dtgOralN.Rows[i].Cells[2].Value == null || dtgOralN.Rows[i].Cells[3].Value == null)
                {
                    errorProvider1.SetError(grbOralN, "Campos incompletos");
                    validacion = false;
                }
            }
            for (int i = 0; i < dtgParental.Rows.Count - 1; i++)
            {
                if (dtgParental.Rows[i].Cells[2].Value == null || dtgParental.Rows[i].Cells[3].Value == null)
                {
                    errorProvider1.SetError(grbParental, "Campos incompletos");
                    validacion = false;
                }
            }
            for (int i = 0; i < dtgParentalT.Rows.Count - 1; i++)
            {
                if (dtgParentalT.Rows[i].Cells[2].Value == null || dtgParentalT.Rows[i].Cells[3].Value == null)
                {
                    errorProvider1.SetError(grbParentalT, "Campos incompletos");
                    validacion = false;
                }
            }
            for (int i = 0; i < dtgParentalN.Rows.Count - 1; i++)
            {
                if (dtgParentalN.Rows[i].Cells[2].Value == null || dtgParentalN.Rows[i].Cells[3].Value == null)
                {
                    errorProvider1.SetError(grbParentalN, "Campos incompletos");
                    validacion = false;
                }
            }
            for (int i = 0; i < dtgOrina.Rows.Count - 1; i++)
            {
                if (dtgOrina.Rows[i].Cells[2].Value == null || dtgOrina.Rows[i].Cells[3].Value == null)
                {
                    errorProvider1.SetError(grbOrina, "Campos incompletos");
                    validacion = false;
                }
            }
            for (int i = 0; i < dtgOrinaT.Rows.Count - 1; i++)
            {
                if (dtgOrinaT.Rows[i].Cells[2].Value == null || dtgOrinaT.Rows[i].Cells[3].Value == null)
                {
                    errorProvider1.SetError(grbOrinaT, "Campos incompletos");
                    validacion = false;
                }
            }
            for (int i = 0; i < dtgOrinaN.Rows.Count - 1; i++)
            {
                if (dtgOrinaN.Rows[i].Cells[2].Value == null || dtgOrinaN.Rows[i].Cells[3].Value == null)
                {
                    errorProvider1.SetError(grbOrinaN, "Campos incompletos");
                    validacion = false;
                }
            }
            for (int i = 0; i < dtgOtros.Rows.Count - 1; i++)
            {
                if (dtgOtros.Rows[i].Cells[2].Value == null || dtgOtros.Rows[i].Cells[3].Value == null)
                {
                    errorProvider1.SetError(grbOtros, "Campos incompletos");
                    validacion = false;
                }
            }
            for (int i = 0; i < dtgOtrosT.Rows.Count - 1; i++)
            {
                if (dtgOtrosT.Rows[i].Cells[2].Value == null || dtgOtrosT.Rows[i].Cells[3].Value == null)
                {
                    errorProvider1.SetError(grbOtrosT, "Campos incompletos");
                    validacion = false;
                }
            }
            for (int i = 0; i < dtgOtrosN.Rows.Count - 1; i++)
            {
                if (dtgOtrosN.Rows[i].Cells[2].Value == null || dtgOtrosN.Rows[i].Cells[3].Value == null)
                {
                    errorProvider1.SetError(grbOtrosN, "Campos incompletos");
                    validacion = false;
                }
            }
            return validacion;
        }
        public string hora = "";
        public void GrabaDatos(Int32 ID_USUARIO)
        {
            try
            {
                for (int i = 0; i < dtgOral.Rows.Count - 1; i++)
                {
                    hora = dtgOral.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "IOM";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgOral.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgOral.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    if (dtgOral.Rows[i].Cells[0].Value == null || dtgOral.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgOralt.Rows.Count - 1; i++)
                {
                    hora = dtgOralt.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "IOT";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgOralt.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgOralt.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    if (dtgOralt.Rows[i].Cells[0].Value == null || dtgOralt.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgOralN.Rows.Count - 1; i++)
                {
                    hora = dtgOralN.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "ION";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgOralN.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgOralN.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    if (ts >= TimeSpan.Zero && ts <= TimeSpan.FromHours(7))
                    {
                        // Si la hora está entre 00:00 y 07:00, agregamos 1 día a la fecha seleccionada
                        det.IED_FECHA = dtp_Creacion.Value.Date.AddDays(1).Add(ts);
                    }
                    else
                    {
                        // Si la hora está después de las 07:00, simplemente agregamos la hora a la fecha seleccionada
                        det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    }
                    if (dtgOralN.Rows[i].Cells[0].Value == null || dtgOralN.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgParental.Rows.Count - 1; i++)
                {
                    hora = dtgParental.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "IPM";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgParental.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgParental.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    if (dtgParental.Rows[i].Cells[0].Value == null || dtgParental.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                    //if (Convert.ToInt64(dtgParental.Rows[i].Cells[4].Value) != 0) // se comenta para manejarse por parametro 24 horas // Mario // 16-08-2023
                    //{
                    //    if (!NegIngestaEliminacion.desacticaKardex(Convert.ToInt64(dtgParental.Rows[i].Cells[4].Value)))
                    //        MessageBox.Show("No se pudo aregar el medicamento compuesto", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                }
                for (int i = 0; i < dtgParentalT.Rows.Count - 1; i++)
                {
                    hora = dtgParentalT.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "IPT";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgParentalT.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgParentalT.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    if (dtgParentalT.Rows[i].Cells[0].Value == null || dtgParentalT.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                    //if (Convert.ToInt64(dtgParentalT.Rows[i].Cells[4].Value) != 0)
                    //{
                    //    if (!NegIngestaEliminacion.desacticaKardex(Convert.ToInt64(dtgParentalT.Rows[i].Cells[4].Value)))
                    //        MessageBox.Show("No se pudo aregar el medicamento compuesto", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                }
                for (int i = 0; i < dtgParentalN.Rows.Count - 1; i++)
                {
                    hora = dtgParentalN.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "IPN";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgParentalN.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgParentalN.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    if (ts >= TimeSpan.Zero && ts <= TimeSpan.FromHours(7))
                    {
                        // Si la hora está entre 00:00 y 07:00, agregamos 1 día a la fecha seleccionada
                        det.IED_FECHA = dtp_Creacion.Value.Date.AddDays(1).Add(ts);
                    }
                    else
                    {
                        // Si la hora está después de las 07:00, simplemente agregamos la hora a la fecha seleccionada
                        det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    }
                    if (dtgParentalN.Rows[i].Cells[0].Value == null || dtgParentalN.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                    //if (Convert.ToInt64(dtgParentalN.Rows[i].Cells[4].Value) != 0)
                    //{
                    //    if (!NegIngestaEliminacion.desacticaKardex(Convert.ToInt64(dtgParentalN.Rows[i].Cells[4].Value)))
                    //        MessageBox.Show("No se pudo aregar el medicamento compuesto", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                }
                for (int i = 0; i < dtgEnteral.Rows.Count - 1; i++)
                {
                    hora = dtgEnteral.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "IEM";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgEnteral.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgEnteral.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    if (dtgEnteral.Rows[i].Cells[0].Value == null || dtgEnteral.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgEnteralT.Rows.Count - 1; i++)
                {
                    hora = dtgEnteralT.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "IET";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgEnteralT.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgEnteralT.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    if (dtgEnteralT.Rows[i].Cells[0].Value == null || dtgEnteralT.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgEnteralN.Rows.Count - 1; i++)
                {
                    hora = dtgEnteralN.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "IEN";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgEnteralN.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgEnteralN.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    if (ts >= TimeSpan.Zero && ts <= TimeSpan.FromHours(7))
                    {
                        // Si la hora está entre 00:00 y 07:00, agregamos 1 día a la fecha seleccionada
                        det.IED_FECHA = dtp_Creacion.Value.Date.AddDays(1).Add(ts);
                    }
                    else
                    {
                        // Si la hora está después de las 07:00, simplemente agregamos la hora a la fecha seleccionada
                        det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    }
                    if (dtgEnteralN.Rows[i].Cells[0].Value == null || dtgEnteralN.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgOrina.Rows.Count - 1; i++)
                {
                    hora = dtgOrina.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "EOM";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgOrina.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgOrina.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = Convert.ToString(dtgOrina.Rows[i].Cells[4].Value);
                    det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    if (dtgOrina.Rows[i].Cells[0].Value == null || dtgOrina.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgOrinaT.Rows.Count - 1; i++)
                {
                    hora = dtgOrinaT.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "EOT";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgOrinaT.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgOrinaT.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = Convert.ToString(dtgOrinaT.Rows[i].Cells[4].Value);
                    det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    if (dtgOrinaT.Rows[i].Cells[0].Value == null || dtgOrinaT.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgOrinaN.Rows.Count - 1; i++)
                {
                    hora = dtgOrinaN.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "EON";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgOrinaN.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgOrinaN.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = Convert.ToString(dtgOrinaN.Rows[i].Cells[4].Value);
                    if (ts >= TimeSpan.Zero && ts <= TimeSpan.FromHours(7))
                    {
                        // Si la hora está entre 00:00 y 07:00, agregamos 1 día a la fecha seleccionada
                        det.IED_FECHA = dtp_Creacion.Value.Date.AddDays(1).Add(ts);
                    }
                    else
                    {
                        // Si la hora está después de las 07:00, simplemente agregamos la hora a la fecha seleccionada
                        det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    }
                    if (dtgOrinaN.Rows[i].Cells[0].Value == null || dtgOrinaN.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgOtros.Rows.Count - 1; i++)
                {
                    hora = dtgOtros.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "ERM";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgOtros.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgOtros.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    if (dtgOtros.Rows[i].Cells[0].Value == null || dtgOtros.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgOtrosT.Rows.Count - 1; i++)
                {
                    hora = dtgOtrosT.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "ERT";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgOtrosT.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgOtrosT.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    if (dtgOtrosT.Rows[i].Cells[0].Value == null || dtgOtrosT.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgOtrosN.Rows.Count - 1; i++)
                {
                    hora = dtgOtrosN.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "ERN";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgOtrosN.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgOtrosN.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    if (ts >= TimeSpan.Zero && ts <= TimeSpan.FromHours(7))
                    {
                        // Si la hora está entre 00:00 y 07:00, agregamos 1 día a la fecha seleccionada
                        det.IED_FECHA = dtp_Creacion.Value.Date.AddDays(1).Add(ts);
                    }
                    else
                    {
                        // Si la hora está después de las 07:00, simplemente agregamos la hora a la fecha seleccionada
                        det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    }
                    if (dtgOtrosN.Rows[i].Cells[0].Value == null || dtgOtrosN.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgVomito.Rows.Count - 1; i++)
                {
                    hora = dtgVomito.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "EVM";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgVomito.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgVomito.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    if (dtgVomito.Rows[i].Cells[0].Value == null || dtgVomito.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgVomitoT.Rows.Count - 1; i++)
                {
                    hora = dtgVomitoT.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "EVT";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgVomitoT.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgVomitoT.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    if (dtgVomitoT.Rows[i].Cells[0].Value == null || dtgVomitoT.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgVomitoN.Rows.Count - 1; i++)
                {
                    hora = dtgVomitoN.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "EVN";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgVomitoN.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgVomitoN.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    if (ts >= TimeSpan.Zero && ts <= TimeSpan.FromHours(7))
                    {
                        // Si la hora está entre 00:00 y 07:00, agregamos 1 día a la fecha seleccionada
                        det.IED_FECHA = dtp_Creacion.Value.Date.AddDays(1).Add(ts);
                    }
                    else
                    {
                        // Si la hora está después de las 07:00, simplemente agregamos la hora a la fecha seleccionada
                        det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    }
                    if (dtgVomitoN.Rows[i].Cells[0].Value == null || dtgVomitoN.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgDrenaje.Rows.Count - 1; i++)
                {
                    hora = dtgDrenaje.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "EDM";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgDrenaje.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgDrenaje.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    if (dtgDrenaje.Rows[i].Cells[0].Value == null || dtgDrenaje.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgDrenajeT.Rows.Count - 1; i++)
                {
                    hora = dtgDrenajeT.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "EDT";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgDrenajeT.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgDrenajeT.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    if (dtgDrenajeT.Rows[i].Cells[0].Value == null || dtgDrenajeT.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgDrenajeN.Rows.Count - 1; i++)
                {
                    hora = dtgDrenajeN.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "EDN";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgDrenajeN.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgDrenajeN.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = "";
                    if (ts >= TimeSpan.Zero && ts <= TimeSpan.FromHours(7))
                    {
                        // Si la hora está entre 00:00 y 07:00, agregamos 1 día a la fecha seleccionada
                        det.IED_FECHA = dtp_Creacion.Value.Date.AddDays(1).Add(ts);
                    }
                    else
                    {
                        // Si la hora está después de las 07:00, simplemente agregamos la hora a la fecha seleccionada
                        det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    }
                    if (dtgDrenajeN.Rows[i].Cells[0].Value == null || dtgDrenajeN.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgDeposiciones.Rows.Count - 1; i++)
                {
                    hora = dtgDeposiciones.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "EPM";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgDeposiciones.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgDeposiciones.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = Convert.ToString(dtgDeposiciones.Rows[i].Cells[4].Value);
                    det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    if (dtgDeposiciones.Rows[i].Cells[0].Value == null || dtgDeposiciones.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgDeposicionesT.Rows.Count - 1; i++)
                {
                    hora = dtgDeposicionesT.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "EPT";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgDeposicionesT.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgDeposicionesT.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = Convert.ToString(dtgDeposicionesT.Rows[i].Cells[4].Value);
                    det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    if (dtgDeposicionesT.Rows[i].Cells[0].Value == null || dtgDeposicionesT.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
                for (int i = 0; i < dtgDeposicionesN.Rows.Count - 1; i++)
                {
                    hora = dtgDeposicionesN.Rows[i].Cells[1].Value.ToString();
                    hora = hora.Substring(0, 2) + ":" + Strings.Right(hora, 2);
                    TimeSpan ts = TimeSpan.Parse(hora);
                    det = new HC_INGESTA_ELIMINACION_DETALLE();
                    det.IED_TIPO = "EPN";
                    det.IED_HORA = ts;
                    det.IED_CLASE = dtgDeposicionesN.Rows[i].Cells[2].Value.ToString();
                    det.IED_CANTIDAD = Convert.ToString(dtgDeposicionesN.Rows[i].Cells[3].Value);
                    det.IE_CODIGO = IE_CODIGO;
                    det.IED_DETALLE = Convert.ToString(dtgDeposicionesN.Rows[i].Cells[4].Value);

                    if (ts >= TimeSpan.Zero && ts <= TimeSpan.FromHours(7))
                    {
                        // Si la hora está entre 00:00 y 07:00, agregamos 1 día a la fecha seleccionada
                        det.IED_FECHA = dtp_Creacion.Value.Date.AddDays(1).Add(ts);
                    }
                    else
                    {
                        // Si la hora está después de las 07:00, simplemente agregamos la hora a la fecha seleccionada
                        det.IED_FECHA = dtp_Creacion.Value.Date + ts;
                    }

                    if (dtgDeposicionesN.Rows[i].Cells[0].Value == null || dtgDeposicionesN.Rows[i].Cells[0].Value == "")
                    {
                        det.ID_USUARIO = ID_USUARIO;
                        NegIngestaEliminacion.GrabarDetalleIngElm(det, "SAVE");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void GrabarIngElm()
        {
            NegIngestaEliminacion.GrabarIngElm((Int32)CodigoAtencion, paciente.PAC_CODIGO, dtp_Creacion.Value);
            DataTable ultimoRegistro = NegIngestaEliminacion.UltimoRegistro();
            IE_CODIGO = Convert.ToInt32(ultimoRegistro.Rows[0][0].ToString());
            modo = false;
        }
        private void refrescarSolicitudes()
        {
            gridSol.DataSource = NegIngestaEliminacion.getIngElm(CodigoAtencion);
            gridSol.Columns["IE_CODIGO"].Visible = false;
            gridSol.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            if (gridSol.RowCount > 1)
                HabilitarBotones(true, false, false, true, false);
            else
                HabilitarBotones(true, false, false, false, false);
        }
        public void HabilitarBotones(bool nuevo, bool guardar, bool imprimir, bool actualizar, bool cancelar)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnImprimir.Enabled = imprimir;
            btnActualizar.Enabled = actualizar;
            btnCancelar.Enabled = cancelar;
        }
        public void habilitarGrid(bool grid)
        {
            grbDatOral.Enabled = grid;
            grbDatParenteral.Enabled = grid;
            grbDatEnteral.Enabled = grid;
            grbDatOrina.Enabled = grid;
            grbDatOtros.Enabled = grid;
            grbDatDeposiciones.Enabled = grid;
            grbDatDrenaje.Enabled = grid;
            grbDatVomito.Enabled = grid;
            dtgOral.Enabled = grid;
            dtgOralt.Enabled = grid;
            dtgOralN.Enabled = grid;
            dtgParental.Enabled = grid;
            dtgParentalT.Enabled = grid;
            dtgParentalN.Enabled = grid;
            dtgEnteral.Enabled = grid;
            dtgEnteralT.Enabled = grid;
            dtgEnteralN.Enabled = grid;
            dtgOrina.Enabled = grid;
            dtgOrinaT.Enabled = grid;
            dtgOrinaN.Enabled = grid;
            dtgOtros.Enabled = grid;
            dtgOtrosT.Enabled = grid;
            dtgOtrosN.Enabled = grid;
            dtgVomito.Enabled = grid;
            dtgVomitoT.Enabled = grid;
            dtgVomitoN.Enabled = grid;
            dtgDrenaje.Enabled = grid;
            dtgDrenajeT.Enabled = grid;
            dtgDrenajeN.Enabled = grid;

        }

        #region Validacion para eliminar
        private void dtgOral_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgOral.CurrentRow != null)
                        {
                            //Int32 codigoDetAnam = 0;
                            if (!Int32.TryParse(dtgOral.CurrentRow.Cells[0].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;

                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgOral.Rows.Remove(dtgOral.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                            }
                            else
                            {
                                try
                                {
                                    dtgOral.Rows.Remove(dtgOral.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {

                                    //throw;
                                }
                            }
                        }
                        break;
                    //case Keys.Tab:
                    //    validaIOM();
                    //    break;
                    //case Keys.Enter:
                    //    validaIOME();
                    //    break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        private void dtgOralt_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgOralt.CurrentRow != null)
                        {
                            if (!Int32.TryParse(dtgOralt.CurrentRow.Cells["codigoIOT"].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;
                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgOralt.Rows.Remove(dtgOralt.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                try
                                {
                                    dtgOralt.Rows.Remove(dtgOralt.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                        }
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

        private void dtgOralN_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgOralN.CurrentRow != null)
                        {
                            if (!Int32.TryParse(dtgOralN.CurrentRow.Cells["codigoION"].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;
                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgOralN.Rows.Remove(dtgOralN.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                try
                                {
                                    dtgOralN.Rows.Remove(dtgOralN.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                        }
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

        private void dtgParental_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgParental.CurrentRow != null)
                        {
                            if (!Int32.TryParse(dtgParental.CurrentRow.Cells["codigoIPM"].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;
                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgParental.Rows.Remove(dtgParental.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                try
                                {
                                    dtgParental.Rows.Remove(dtgParental.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                        }
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

        private void dtgParentalT_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgParentalT.CurrentRow != null)
                        {
                            if (!Int32.TryParse(dtgParentalT.CurrentRow.Cells["codigoIPT"].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;
                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgParentalT.Rows.Remove(dtgParentalT.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                try
                                {
                                    dtgParentalT.Rows.Remove(dtgParentalT.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                        }
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

        private void dtgParentalN_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgParentalN.CurrentRow != null)
                        {
                            if (!Int32.TryParse(dtgParentalN.CurrentRow.Cells["codigoIPN"].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;
                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgParentalN.Rows.Remove(dtgParentalN.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                try
                                {
                                    dtgOtrosN.Rows.Remove(dtgParentalN.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                        }
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

        private void dtgOrina_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgOrina.CurrentRow != null)
                        {
                            if (!Int32.TryParse(dtgOrina.CurrentRow.Cells["codigoEOM"].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;
                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgOrina.Rows.Remove(dtgOrina.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                try
                                {
                                    dtgOrina.Rows.Remove(dtgOrina.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                        }
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

        private void dtgOrinaT_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgOrinaT.CurrentRow != null)
                        {
                            if (!Int32.TryParse(dtgOrinaT.CurrentRow.Cells["codigoEOT"].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;
                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgOrinaT.Rows.Remove(dtgOrinaT.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                try
                                {
                                    dtgOrinaT.Rows.Remove(dtgOrinaT.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                        }
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

        private void dtgOrinaN_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgOrinaN.CurrentRow != null)
                        {
                            if (!Int32.TryParse(dtgOrinaN.CurrentRow.Cells["codigoEON"].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;
                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgOrinaN.Rows.Remove(dtgOrinaN.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                try
                                {
                                    dtgOrinaN.Rows.Remove(dtgOrinaN.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                        }
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

        private void dtgOtros_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgOtros.CurrentRow != null)
                        {
                            if (!Int32.TryParse(dtgOtros.CurrentRow.Cells["codigoERM"].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;
                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgOtros.Rows.Remove(dtgOtros.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                try
                                {
                                    dtgOtros.Rows.Remove(dtgOtros.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                        }
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

        private void dtgOtrosT_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgOtrosT.CurrentRow != null)
                        {
                            if (!Int32.TryParse(dtgOtrosT.CurrentRow.Cells["codigoERT"].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;
                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgOtrosT.Rows.Remove(dtgOtrosT.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                try
                                {
                                    dtgOtrosT.Rows.Remove(dtgOtrosT.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                        }
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

        private void dtgOtrosN_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgOtrosN.CurrentRow != null)
                        {
                            if (!Int32.TryParse(dtgOtrosN.CurrentRow.Cells["codigoERN"].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;
                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgOtrosN.Rows.Remove(dtgOtrosN.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                try
                                {
                                    dtgOtrosN.Rows.Remove(dtgOtrosN.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                        }
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
        private void dtgDrenaje_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgDrenaje.CurrentRow != null)
                        {
                            if (!Int32.TryParse(dtgDrenaje.CurrentRow.Cells["codigoEDM"].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;
                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgDrenaje.Rows.Remove(dtgDrenaje.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                try
                                {
                                    dtgDrenaje.Rows.Remove(dtgDrenaje.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                        }
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

        private void dtgDrenajeT_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgDrenajeT.CurrentRow != null)
                        {
                            if (!Int32.TryParse(dtgDrenajeT.CurrentRow.Cells["codigoEDT"].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;
                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgDrenajeT.Rows.Remove(dtgDrenajeT.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                try
                                {
                                    dtgDrenajeT.Rows.Remove(dtgDrenajeT.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                        }
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

        private void dtgDrenajeN_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgDrenajeN.CurrentRow != null)
                        {
                            if (!Int32.TryParse(dtgDrenajeN.CurrentRow.Cells["codigoEDN"].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;
                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgDrenajeN.Rows.Remove(dtgDrenajeN.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                try
                                {
                                    dtgDrenajeN.Rows.Remove(dtgDrenajeN.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                        }
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
        private void dtgDeposiciones_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgDeposiciones.CurrentRow != null)
                        {
                            if (!Int32.TryParse(dtgDeposiciones.CurrentRow.Cells["codigoEPM"].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;
                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgDeposiciones.Rows.Remove(dtgDeposiciones.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                try
                                {
                                    dtgDeposiciones.Rows.Remove(dtgDeposiciones.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                        }
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

        private void dtgDeposicionesT_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgDeposicionesT.CurrentRow != null)
                        {
                            if (!Int32.TryParse(dtgDeposicionesT.CurrentRow.Cells["codigoEPT"].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;
                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgDeposicionesT.Rows.Remove(dtgDeposicionesT.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                try
                                {
                                    dtgDeposicionesT.Rows.Remove(dtgDeposicionesT.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                        }
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

        private void dtgDeposicionesN_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtgDeposicionesN.CurrentRow != null)
                        {
                            if (!Int32.TryParse(dtgDeposicionesN.CurrentRow.Cells["codigoEPN"].Value.ToString(), out Int32 codigoDetAnam))
                                codigoDetAnam = 0;
                            if (codigoDetAnam != 0)
                            {
                                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                usuario.ShowDialog();
                                if (!usuario.aceptado)
                                    return;
                                HC_INGESTA_ELIMINACION_DETALLE ing = NegIngestaEliminacion.recuperaDetalleUsuario(codigoDetAnam);
                                if (ing.ID_USUARIO == usuario.usuarioActual)
                                {
                                    if (!NegIngestaEliminacion.eliminarIngElm(codigoDetAnam))
                                    {
                                        MessageBox.Show("No se puedo eliminar el registro", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    dtgDeposicionesN.Rows.Remove(dtgDeposicionesN.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Solo el ususario que registro la informacion puede eliminarla", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                try
                                {
                                    dtgDeposicionesN.Rows.Remove(dtgDeposicionesN.CurrentRow);
                                    MessageBox.Show("registro eliminado exitosamente", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                            }
                        }
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
        #endregion

        private void ImprimirFormulario()
        {
            atencion = NegAtenciones.RecuperarAtencionID(CodigoAtencion);
            paciente = NegPacientes.RecuperarPacienteID(atencion.PACIENTES.PAC_CODIGO);
            EMPRESA emp = NegEmpresa.RecuperaEmpresa();
            NegCertificadoMedico medico = new NegCertificadoMedico();
            DataTable IO = NegDietetica.getDataTable("EMPRESA");
            dtsIngestaEliminacion frm = new dtsIngestaEliminacion();
            DataRow dr;
            dr = frm.Tables["Paciente"].NewRow();
            dr["hcl"] = lblHc.Text;
            dr["habitacion"] = lblSeguro.Text;
            dr["identificacion"] = lblidentificacion.Text;
            dr["paciente"] = lblPaciente.Text;
            dr["sexo"] = lblsexo.Text;
            dr["medico"] = lblmedico.Text;
            dr["fecha"] = dtp_Creacion.Value;
            dr["edad"] = lbledad.Text;
            dr["logo"] = IO.Rows[0]["EMP_PATHIMAGEN"].ToString();
            dr["nombre"] = paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
            dr["apellido"] = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO;
            dr["empresa"] = emp.EMP_NOMBRE;
            frm.Tables["Paciente"].Rows.Add(dr);

            HC_INGESTA_ELIMINACION ingElm = NegIngestaEliminacion.recuperaIngestaXcodigo(IE_CODIGO);
            DataRow cabecera;
            cabecera = frm.Tables["Encabezado"].NewRow();
            cabecera["institucion"] = His.Entidades.Clases.Sesion.nomEmpresa;
            cabecera["unicodigo"] = IO.Rows[0]["EMP_PATHIMAGEN"].ToString();
            cabecera["establecimiento"] = His.Entidades.Clases.Sesion.nomEmpresa;
            cabecera["hc"] = paciente.PAC_IDENTIFICACION;
            cabecera["narchivo"] = paciente.PAC_HISTORIA_CLINICA + "-" + atencion.ATE_NUMERO_ATENCION;
            cabecera["papellido"] = paciente.PAC_APELLIDO_PATERNO;
            cabecera["sapellido"] = paciente.PAC_APELLIDO_MATERNO;
            cabecera["pnombre"] = paciente.PAC_NOMBRE1;
            cabecera["snombre"] = paciente.PAC_NOMBRE2;
            cabecera["fnacimiento"] = paciente.PAC_FECHA_NACIMIENTO;
            if (paciente.PAC_GENERO == "M")
                cabecera["sexo"] = "HOMBRE";
            else
                cabecera["sexo"] = "MUJER";
            cabecera["edad"] = NegUtilitarios.EdadCalculada(Convert.ToDateTime(paciente.PAC_FECHA_NACIMIENTO), Convert.ToDateTime(ingElm.IE_FECHA));
            frm.Tables["Encabezado"].Rows.Add(cabecera);


            DataTable OralM = NegIngestaEliminacion.cargaGrid("IOM", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "IOM");
            if (OralM.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < OralM.Rows.Count; i++)
            {
                dr = frm.Tables["Oral"].NewRow();
                dr["Ohora"] = OralM.Rows[i][2].ToString();
                dr["OClase"] = OralM.Rows[i][3].ToString();
                dr["Ocantidad"] = OralM.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["Oral"].Rows.Add(dr);
            }

            DataTable ParentalM = NegIngestaEliminacion.cargaGrid("IPM", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "IPM");
            if (ParentalM.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < ParentalM.Rows.Count; i++)
            {
                dr = frm.Tables["Parental"].NewRow();
                dr["Phora"] = ParentalM.Rows[i][2].ToString();
                dr["PClase"] = ParentalM.Rows[i][3].ToString();
                dr["Pcantidad"] = ParentalM.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["Parental"].Rows.Add(dr);
            }

            DataTable EnteralM = NegIngestaEliminacion.cargaGrid("IEM", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "IEM");
            if (EnteralM.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < EnteralM.Rows.Count; i++)
            {
                dr = frm.Tables["Enteral"].NewRow();
                dr["Ehora"] = EnteralM.Rows[i][2].ToString();
                dr["Eclase"] = EnteralM.Rows[i][3].ToString();
                dr["Ecantidad"] = EnteralM.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["Enteral"].Rows.Add(dr);
            }

            DataTable OrinaM = NegIngestaEliminacion.cargaGrid("EOM", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "EOM");
            if (OrinaM.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < OrinaM.Rows.Count; i++)
            {
                dr = frm.Tables["Orina"].NewRow();
                dr["Ahora"] = OrinaM.Rows[i][2].ToString();
                dr["Acaracteristica"] = OrinaM.Rows[i][3].ToString();
                dr["Acantidad"] = OrinaM.Rows[i][4].ToString();
                dr["Atex"] = OrinaM.Rows[i][7].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["Orina"].Rows.Add(dr);
            }

            DataTable OtrosM = NegIngestaEliminacion.cargaGrid("ERM", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "ERM");
            if (OtrosM.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < OtrosM.Rows.Count; i++)
            {
                dr = frm.Tables["Otros"].NewRow();
                dr["Rhora"] = OtrosM.Rows[i][2].ToString();
                dr["RClase"] = OtrosM.Rows[i][3].ToString();
                dr["Rcantidad"] = OtrosM.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["Otros"].Rows.Add(dr);
            }

            DataTable VomitoM = NegIngestaEliminacion.cargaGrid("EVM", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "EVM");
            if (VomitoM.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < VomitoM.Rows.Count; i++)
            {
                dr = frm.Tables["Vomito"].NewRow();
                dr["Vhora"] = VomitoM.Rows[i][2].ToString();
                dr["Vclase"] = VomitoM.Rows[i][3].ToString();
                dr["Vcantidad"] = VomitoM.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["Vomito"].Rows.Add(dr);
            }

            DataTable DrenajeM = NegIngestaEliminacion.cargaGrid("EDM", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "EDM");
            if (DrenajeM.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < DrenajeM.Rows.Count; i++)
            {
                dr = frm.Tables["Drenaje"].NewRow();
                dr["Dhora"] = DrenajeM.Rows[i][2].ToString();
                dr["Dclase"] = DrenajeM.Rows[i][3].ToString();
                dr["Dcantidad"] = DrenajeM.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["Drenaje"].Rows.Add(dr);
            }
            DataTable DepisicionesM = NegIngestaEliminacion.cargaGrid("EPM", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "EPM");
            if (DepisicionesM.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < DepisicionesM.Rows.Count; i++)
            {
                dr = frm.Tables["Deposiciones"].NewRow();
                dr["Ehora"] = DepisicionesM.Rows[i][2].ToString();
                dr["Eclase"] = DepisicionesM.Rows[i][3].ToString();
                dr["Ecantidad"] = DepisicionesM.Rows[i][4].ToString();
                dr["Etex"] = DepisicionesM.Rows[i][7].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["Deposiciones"].Rows.Add(dr);
            }
            DataTable OralT = NegIngestaEliminacion.cargaGrid("IOT", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "IOT");
            if (OralT.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < OralT.Rows.Count; i++)
            {
                dr = frm.Tables["OralT"].NewRow();
                dr["OThora"] = OralT.Rows[i][2].ToString();
                dr["OTclase"] = OralT.Rows[i][3].ToString();
                dr["OTcantidad"] = OralT.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["OralT"].Rows.Add(dr);
            }
            DataTable ParentalT = NegIngestaEliminacion.cargaGrid("IPT", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "IPT");
            if (ParentalT.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < ParentalT.Rows.Count; i++)
            {
                dr = frm.Tables["ParentalT"].NewRow();
                dr["PThora"] = ParentalT.Rows[i][2].ToString();
                dr["PTclase"] = ParentalT.Rows[i][3].ToString();
                dr["PTcantidad"] = ParentalT.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["ParentalT"].Rows.Add(dr);
            }

            DataTable EnteralT = NegIngestaEliminacion.cargaGrid("IET", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "IET");
            if (EnteralM.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < EnteralM.Rows.Count; i++)
            {
                dr = frm.Tables["EnteralT"].NewRow();
                dr["EThora"] = EnteralT.Rows[i][2].ToString();
                dr["ETclase"] = EnteralT.Rows[i][3].ToString();
                dr["ETcantidad"] = EnteralT.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["EnteralT"].Rows.Add(dr);
            }

            DataTable OrinaT = NegIngestaEliminacion.cargaGrid("EOT", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "EOT");
            if (OrinaT.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < OrinaT.Rows.Count; i++)
            {
                dr = frm.Tables["OrinaT"].NewRow();
                dr["AThora"] = OrinaT.Rows[i][2].ToString();
                dr["ATcaracteristica"] = OrinaT.Rows[i][3].ToString();
                dr["ATcantidad"] = OrinaT.Rows[i][4].ToString();
                dr["ATtex"] = OrinaT.Rows[i][7].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["OrinaT"].Rows.Add(dr);
            }

            DataTable OtrosT = NegIngestaEliminacion.cargaGrid("ERT", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "ERT");
            if (OtrosT.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < OtrosT.Rows.Count; i++)
            {
                dr = frm.Tables["OtrosT"].NewRow();
                dr["RThora"] = OtrosT.Rows[i][2].ToString();
                dr["RTclase"] = OtrosT.Rows[i][3].ToString();
                dr["RTcantidad"] = OtrosT.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["OtrosT"].Rows.Add(dr);
            }

            DataTable VomitoT = NegIngestaEliminacion.cargaGrid("EVT", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "EVT");
            if (VomitoT.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < VomitoT.Rows.Count; i++)
            {
                dr = frm.Tables["VomitoT"].NewRow();
                dr["VThora"] = VomitoT.Rows[i][2].ToString();
                dr["VTclase"] = VomitoT.Rows[i][3].ToString();
                dr["VTcantidad"] = VomitoT.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["VomitoT"].Rows.Add(dr);
            }

            DataTable DrenajeT = NegIngestaEliminacion.cargaGrid("EDT", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "EDT");
            if (DrenajeT.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < DrenajeT.Rows.Count; i++)
            {
                dr = frm.Tables["DrenajeT"].NewRow();
                dr["DThora"] = DrenajeT.Rows[i][2].ToString();
                dr["DTclase"] = DrenajeT.Rows[i][3].ToString();
                dr["DTcantidad"] = DrenajeT.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["DrenajeT"].Rows.Add(dr);
            }
            DataTable DepisicionesT = NegIngestaEliminacion.cargaGrid("EPT", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "EPT");
            if (DepisicionesT.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < DepisicionesT.Rows.Count; i++)
            {
                dr = frm.Tables["DeposicionesT"].NewRow();
                dr["EThora"] = DepisicionesT.Rows[i][2].ToString();
                dr["ETclase"] = DepisicionesT.Rows[i][3].ToString();
                dr["ETcantidad"] = DepisicionesT.Rows[i][4].ToString();
                dr["ETtex"] = DepisicionesT.Rows[i][7].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["DeposicionesT"].Rows.Add(dr);
            }
            DataTable OralN = NegIngestaEliminacion.cargaGrid("ION", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "ION");
            if (OralN.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < OralN.Rows.Count; i++)
            {
                dr = frm.Tables["OralN"].NewRow();
                dr["ONhora"] = OralN.Rows[i][2].ToString();
                dr["ONclase"] = OralN.Rows[i][3].ToString();
                dr["ONcantidad"] = OralN.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["OralN"].Rows.Add(dr);
            }

            DataTable ParentalN = NegIngestaEliminacion.cargaGrid("IPN", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "IPN");
            if (ParentalN.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < ParentalN.Rows.Count; i++)
            {
                dr = frm.Tables["ParentalN"].NewRow();
                dr["PNhora"] = ParentalN.Rows[i][2].ToString();
                dr["PNclase"] = ParentalN.Rows[i][3].ToString();
                dr["PNcantidad"] = ParentalN.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["ParentalN"].Rows.Add(dr);
            }

            DataTable EnteralN = NegIngestaEliminacion.cargaGrid("IEN", IE_CODIGO); //Se cambio de IEM Mañana a IEN Tarde la ENTERAL
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "IEN");
            if (EnteralN.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < EnteralN.Rows.Count; i++)
            {
                dr = frm.Tables["EnteralN"].NewRow();
                dr["ENhora"] = EnteralN.Rows[i][2].ToString();
                dr["ENclase"] = EnteralN.Rows[i][3].ToString();
                dr["ENcantidad"] = EnteralN.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["EnteralN"].Rows.Add(dr);
            }

            DataTable OrinaN = NegIngestaEliminacion.cargaGrid("EON", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "EON");
            if (OrinaN.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < OrinaN.Rows.Count; i++)
            {
                dr = frm.Tables["OrinaN"].NewRow();
                dr["ANhora"] = OrinaN.Rows[i][2].ToString();
                dr["ANcaracteristica"] = OrinaN.Rows[i][3].ToString();
                dr["ANcantidad"] = OrinaN.Rows[i][4].ToString();
                dr["ANtex"] = OrinaN.Rows[i][7].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["OrinaN"].Rows.Add(dr);
            }

            DataTable OtrosN = NegIngestaEliminacion.cargaGrid("ERN", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "ERN");
            if (OtrosN.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < OtrosN.Rows.Count; i++)
            {
                dr = frm.Tables["OtrosN"].NewRow();
                dr["RNhora"] = OtrosN.Rows[i][2].ToString();
                dr["RNclase"] = OtrosN.Rows[i][3].ToString();
                dr["RNcantidad"] = OtrosN.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["OtrosN"].Rows.Add(dr);
            }

            DataTable VomitoN = NegIngestaEliminacion.cargaGrid("EVN", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "EVN");
            if (VomitoN.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < VomitoM.Rows.Count; i++)
            {
                dr = frm.Tables["VomitoN"].NewRow();
                dr["VNhora"] = VomitoN.Rows[i][2].ToString();
                dr["VNclase"] = VomitoN.Rows[i][3].ToString();
                dr["VNcantidad"] = VomitoN.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["VomitoN"].Rows.Add(dr);
            }

            DataTable DrenajeN = NegIngestaEliminacion.cargaGrid("EDN", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "EDN");
            if (DrenajeN.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < DrenajeN.Rows.Count; i++)
            {
                dr = frm.Tables["DrenajeN"].NewRow();
                dr["DNhora"] = DrenajeN.Rows[i][2].ToString();
                dr["DNclase"] = DrenajeN.Rows[i][3].ToString();
                dr["DNcantidad"] = DrenajeN.Rows[i][4].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["DrenajeN"].Rows.Add(dr);
            }
            DataTable DepisicionesN = NegIngestaEliminacion.cargaGrid("EPN", IE_CODIGO);
            ID_USUARIO = NegIngestaEliminacion.idUsuario(IE_CODIGO, "EPN");
            if (DepisicionesN.Rows.Count > 0)
                usr = NegUsuarios.RecuperaUsuario(ID_USUARIO);
            for (int i = 0; i < DepisicionesN.Rows.Count; i++)
            {
                dr = frm.Tables["DeposicionesN"].NewRow();
                dr["ENhora"] = DepisicionesN.Rows[i][2].ToString();
                dr["ENclase"] = DepisicionesN.Rows[i][3].ToString();
                dr["ENcantidad"] = DepisicionesN.Rows[i][4].ToString();
                dr["ENtex"] = DepisicionesN.Rows[i][7].ToString();
                dr["usr"] = usr.APELLIDOS + " " + usr.NOMBRES;
                frm.Tables["DeposicionesN"].Rows.Add(dr);
            }
            frmReportes x = new frmReportes(1, "IngestaEliminacion", frm);
            x.Show();

        }
        public DateTime desde = new DateTime();
        public DateTime hasta = new DateTime();
        public Int32 thora;
        public bool valida;
        #region Validaciones TAB
        //private void validaIOM()
        //{
        //    string prb = Convert.ToString(this.dtgOral.CurrentRow.Cells[1].Value.ToString());
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOral.CurrentRow.Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 559 || thora >= 1200)
        //                {
        //                    MessageBox.Show("La hora de la mañana no puede der mayor a las 12:00 o menor de las 06:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //                    dtgOral.CurrentRow.Cells[1].Value = val;
        //                }
        //            }


        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }

        //    }
        //}
        //private void validaIOT()
        //{
        //    string prb = Convert.ToString(dtgOralt.CurrentRow.Cells[1].Value);
        //    desde = new DateTime(2022, 8, 03, 12, 0, 0);
        //    hasta = new DateTime(2022, 8, 03, 18, 0, 0);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOralt.CurrentRow.Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 1159 || thora >= 1800)
        //                {
        //                    MessageBox.Show("La hora de la tarde no puede der mayor a las 18:00 o menor de las 12:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //                    dtgOralt.CurrentRow.Cells[1].Value = val;
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //        dtgOralt.CurrentRow.Cells[1].Value = val;
        //    }
        //}
        //private void validaION()
        //{
        //    string prb = Convert.ToString(dtgOralN.CurrentRow.Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOralN.CurrentRow.Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 2400 && thora > 1759)
        //                {
        //                    if (thora < 1759)
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgOralN.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }
        //                else if (thora < 600 && thora > 0)
        //                {
        //                    if (thora > 600)
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgOralN.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                    dtgOralN.CurrentRow.Cells[1].Value = val;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //        dtgOralN.CurrentRow.Cells[1].Value = val;
        //    }
        //}
        //private void valitaIPM()
        //{
        //    string prb = Convert.ToString(dtgParental.CurrentRow.Cells[1].Value);

        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgParental.CurrentRow.Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 559 || thora >= 1200)
        //                {
        //                    MessageBox.Show("La hora de la mañana no puede der mayor a las 12:00 o menor de las 06:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //                    dtgParental.CurrentRow.Cells[1].Value = val;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //        dtgParental.CurrentRow.Cells[1].Value = val;
        //    }
        //}
        //private void valitaIPT()
        //{
        //    string prb = Convert.ToString(dtgParentalT.CurrentRow.Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgParentalT.CurrentRow.Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 1159 || thora >= 1800)
        //                {
        //                    MessageBox.Show("La hora de la tarde no puede der mayor a las 18:00 o menor de las 12:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //                    dtgParentalT.CurrentRow.Cells[1].Value = val;
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //        dtgParentalT.CurrentRow.Cells[1].Value = val;
        //    }
        //}
        //private void valitaIPN()
        //{
        //    string prb = Convert.ToString(dtgParentalN.CurrentRow.Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgParentalN.CurrentRow.Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 2400 && thora > 1759)
        //                {
        //                    if (thora < 1759)
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgParentalN.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }
        //                else if (thora < 600 && thora > 0)
        //                {
        //                    if (thora > 600)
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgParentalN.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                    dtgParentalN.CurrentRow.Cells[1].Value = val;
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //        dtgParentalN.CurrentRow.Cells[1].Value = val;
        //    }
        //}
        //private void validaEOM()
        //{
        //    string prb = Convert.ToString(dtgOrina.CurrentRow.Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOrina.CurrentRow.Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 559 || thora >= 1200)
        //                {
        //                    MessageBox.Show("La hora de la mañana no puede der mayor a las 12:00 o menor de las 06:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //                    dtgOrina.CurrentRow.Cells[1].Value = val;
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //        dtgOrina.CurrentRow.Cells[1].Value = val;
        //    }
        //}
        //private void validaEOT()
        //{
        //    string prb = Convert.ToString(dtgOrinaT.CurrentRow.Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOrinaT.CurrentRow.Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 1159 || thora >= 1800)
        //                {
        //                    MessageBox.Show("La hora de la tarde no puede der mayor a las 18:00 o menor de las 12:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //                    dtgOrinaT.CurrentRow.Cells[1].Value = val;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //        dtgOrinaT.CurrentRow.Cells[1].Value = val;
        //    }
        //}
        //private void validaEON()
        //{
        //    string prb = Convert.ToString(dtgOrinaN.CurrentRow.Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOrinaN.CurrentRow.Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 2400 && thora > 1759)
        //                {
        //                    if (thora < 1759)
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgOrinaN.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }
        //                else if (thora < 600 && thora > 0)
        //                {
        //                    if (thora > 600)
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgOrinaN.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                    dtgOrinaN.CurrentRow.Cells[1].Value = val;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //        dtgOrinaN.CurrentRow.Cells[1].Value = val;
        //    }
        //}
        //private void validaERM()
        //{
        //    string prb = Convert.ToString(dtgOtros.CurrentRow.Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOtros.CurrentRow.Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 559 || thora >= 1200)
        //                {
        //                    MessageBox.Show("La hora de la mañana no puede der mayor a las 12:00 o menor de las 06:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //                    dtgOtros.CurrentRow.Cells[1].Value = val;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //        dtgOtros.CurrentRow.Cells[1].Value = val;
        //    }
        //}
        //private void validaERT()
        //{
        //    string prb = Convert.ToString(dtgOtrosT.CurrentRow.Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOtrosT.CurrentRow.Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 1159 || thora >= 1800)
        //                {
        //                    MessageBox.Show("La hora de la tarde no puede der mayor a las 18:00 o menor de las 12:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //                    dtgOtrosT.CurrentRow.Cells[1].Value = val;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //        dtgOtrosT.CurrentRow.Cells[1].Value = val;
        //    }
        //}
        //private void validaERN()
        //{
        //    string prb = Convert.ToString(dtgOtrosN.CurrentRow.Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOtrosN.CurrentRow.Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 2400 && thora > 1759)
        //                {
        //                    if (thora < 1759)
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgOtrosN.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }
        //                else if (thora < 600 && thora > 0)
        //                {
        //                    if (thora > 600)
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgOtrosN.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                    dtgOtrosN.CurrentRow.Cells[1].Value = val;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //        dtgOtrosN.CurrentRow.Cells[1].Value = val;
        //    }
        //}
        #endregion

        #region Validaciones Enter
        //public int fila = 0;
        //private void validaIOME()
        //{
        //    fila = dtgOral.CurrentRow.Index - 1;
        //    string prb = Convert.ToString(this.dtgOral.Rows[fila].Cells[1].Value.ToString());
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOral.Rows[fila].Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 559 || thora >= 1200)
        //                {
        //                    MessageBox.Show("La hora de la mañana no puede der mayor a las 12:00 o menor de las 06:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //                    dtgOral.Rows[fila].Cells[1].Value = val;
        //                }
        //            }


        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //        dtgOral.Rows[fila].Cells[1].Value = val;
        //    }
        //}
        //private void validaIOTE()
        //{
        //    fila = dtgOralt.CurrentRow.Index - 1;
        //    string prb = Convert.ToString(dtgOralt.Rows[fila].Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOralt.Rows[fila].Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 1159 || thora >= 1800)
        //                {
        //                    MessageBox.Show("La hora de la tarde no puede der mayor a las 18:00 o menor de las 12:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //                    dtgOralt.Rows[fila].Cells[1].Value = val;
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //        dtgOralt.Rows[fila].Cells[1].Value = val;
        //    }
        //}
        //private void validaIONE()
        //{
        //    fila = dtgOralN.CurrentRow.Index - 1;
        //    string prb = Convert.ToString(dtgOralN.Rows[fila].Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOralN.Rows[fila].Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 2400 && thora > 1759)
        //                {
        //                    if (thora < 1759)
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgOralN.Rows[fila].Cells[1].Value = val;
        //                    }
        //                }
        //                else if (thora < 600 && thora > 0)
        //                {
        //                    if (thora > 600)
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgOralN.Rows[fila].Cells[1].Value = val;
        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                    dtgOralN.Rows[fila].Cells[1].Value = val;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //        dtgOralN.Rows[fila].Cells[1].Value = val;
        //    }
        //}
        //private void valitaIPME()
        //{
        //    fila = dtgParental.CurrentRow.Index - 1;
        //    string prb = Convert.ToString(dtgParental.Rows[fila].Cells[1].Value);

        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgParental.Rows[fila].Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 559 || thora >= 1200)
        //                {
        //                    MessageBox.Show("La hora de la mañana no puede der mayor a las 12:00 o menor de las 06:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //                    dtgParental.Rows[fila].Cells[1].Value = val;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //        dtgParental.Rows[fila].Cells[1].Value = val;
        //    }
        //}
        //private void valitaIPTE()
        //{
        //    fila = dtgParentalT.CurrentRow.Index - 1;
        //    string prb = Convert.ToString(dtgParentalT.Rows[fila].Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgParentalT.Rows[fila].Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 1159 || thora >= 1800)
        //                {
        //                    MessageBox.Show("La hora de la tarde no puede der mayor a las 18:00 o menor de las 12:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //                    dtgParentalT.Rows[fila].Cells[1].Value = val;
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //        dtgParentalT.Rows[fila].Cells[1].Value = val;
        //    }
        //}
        //private void valitaIPNE()
        //{
        //    fila = dtgParentalN.CurrentRow.Index - 1;
        //    string prb = Convert.ToString(dtgParentalN.Rows[fila].Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgParentalN.Rows[fila].Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 2400 && thora > 1759)
        //                {
        //                    if (thora < 1759)
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgParentalN.Rows[fila].Cells[1].Value = val;
        //                    }
        //                }
        //                else if (thora < 600 && thora > 0)
        //                {
        //                    if (thora > 600)
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgParentalN.Rows[fila].Cells[1].Value = val;
        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                    dtgParentalN.Rows[fila].Cells[1].Value = val;
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //        dtgParentalN.Rows[fila].Cells[1].Value = val;
        //    }
        //}
        //private void validaEOME()
        //{
        //    fila = dtgOrina.CurrentRow.Index - 1;
        //    string prb = Convert.ToString(dtgOrina.Rows[fila].Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOrina.Rows[fila].Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 559 || thora >= 1200)
        //                {
        //                    MessageBox.Show("La hora de la mañana no puede der mayor a las 12:00 o menor de las 06:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //                    dtgOrina.Rows[fila].Cells[1].Value = val;
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //        dtgOrina.Rows[fila].Cells[1].Value = val;
        //    }
        //}
        //private void validaEOTE()
        //{
        //    fila = dtgOrinaT.CurrentRow.Index - 1;
        //    string prb = Convert.ToString(dtgOrinaT.Rows[fila].Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOrinaT.Rows[fila].Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 1159 || thora >= 1800)
        //                {
        //                    MessageBox.Show("La hora de la tarde no puede der mayor a las 18:00 o menor de las 12:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //                    dtgOrinaT.Rows[fila].Cells[1].Value = val;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //        dtgOrinaT.Rows[fila].Cells[1].Value = val;
        //    }
        //}
        //private void validaEONE()
        //{
        //    fila = dtgOrinaN.CurrentRow.Index - 1;
        //    string prb = Convert.ToString(dtgOrinaN.Rows[fila].Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOrinaN.Rows[fila].Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 2400 && thora > 1759)
        //                {
        //                    if (thora < 1759)
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgOrinaN.Rows[fila].Cells[1].Value = val;
        //                    }
        //                }
        //                else if (thora < 600 && thora > 0)
        //                {
        //                    if (thora > 600)
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgOrinaN.Rows[fila].Cells[1].Value = val;
        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                    dtgOrinaN.Rows[fila].Cells[1].Value = val;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //        dtgOrinaN.CurrentRow.Cells[1].Value = val;
        //    }
        //}
        //private void validaERME()
        //{
        //    fila = dtgOtros.CurrentRow.Index - 1;
        //    string prb = Convert.ToString(dtgOtros.Rows[fila].Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOtros.Rows[fila].Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 559 || thora >= 1200)
        //                {
        //                    MessageBox.Show("La hora de la mañana no puede der mayor a las 12:00 o menor de las 06:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //                    dtgOtros.Rows[fila].Cells[1].Value = val;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //        dtgOtros.Rows[fila].Cells[1].Value = val;
        //    }
        //}
        //private void validaERTE()
        //{
        //    fila = dtgOtrosT.CurrentRow.Index - 1;
        //    string prb = Convert.ToString(dtgOtrosT.Rows[fila].Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOtrosT.Rows[fila].Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 1159 || thora >= 1800)
        //                {
        //                    MessageBox.Show("La hora de la tarde no puede der mayor a las 18:00 o menor de las 12:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //                    dtgOtrosT.Rows[fila].Cells[1].Value = val;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //        dtgOtrosT.Rows[fila].Cells[1].Value = val;
        //    }
        //}
        //private void validaERNE()
        //{
        //    fila = dtgOtrosN.CurrentRow.Index - 1;
        //    string prb = Convert.ToString(dtgOtrosN.Rows[fila].Cells[1].Value);
        //    if (prb != "")
        //    {
        //        try
        //        {
        //            valida = Int32.TryParse(Convert.ToString(dtgOtrosN.Rows[fila].Cells[1].Value), out thora);
        //            if (valida)
        //            {
        //                if (thora < 2400 && thora > 1759)
        //                {
        //                    if (thora < 1759)
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgOtrosN.Rows[fila].Cells[1].Value = val;
        //                    }
        //                }
        //                else if (thora < 600 && thora > 0)
        //                {
        //                }
        //                else
        //                {
        //                    MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                    DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                    dtgOtrosN.Rows[fila].Cells[1].Value = val;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error", ex);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //        dtgOtrosN.Rows[fila].Cells[1].Value = val;
        //    }
        //}
        #endregion


        #region Validacion Horas Click
        //private void dtgOral_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        //{
        //    string prb = Convert.ToString(dtgOral.CurrentRow.Cells[1].Value);
        //    if (e.ColumnIndex == this.dtgOral.Columns[2].Index || e.ColumnIndex == this.dtgOral.Columns[3].Index)
        //    {
        //        if (prb != "")
        //        {
        //            try
        //            {
        //                valida = Int32.TryParse(Convert.ToString(dtgOral.CurrentRow.Cells[1].Value), out thora);
        //                if (valida)
        //                {
        //                    if (thora < 559 || thora >= 1200)
        //                    {
        //                        MessageBox.Show("La hora de la mañana no puede der mayor a las 12:00 o menor de las 06:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //                        dtgOral.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }


        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception("Error", ex);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //            dtgOral.CurrentRow.Cells[1].Value = val;
        //        }

        //    }
        //}

        //private void dtgOralt_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        //{
        //    string prb = Convert.ToString(dtgOralt.CurrentRow.Cells[1].Value);
        //    if (e.ColumnIndex == this.dtgOralt.Columns[2].Index || e.ColumnIndex == this.dtgOralt.Columns[3].Index)
        //    {
        //        if (prb != "")
        //        {
        //            try
        //            {
        //                valida = Int32.TryParse(Convert.ToString(dtgOralt.CurrentRow.Cells[1].Value), out thora);
        //                if (valida)
        //                {
        //                    if (thora < 1159 || thora >= 1800)
        //                    {
        //                        MessageBox.Show("La hora de la tarde no puede der mayor a las 18:00 o menor de las 12:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //                        dtgOralt.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception("Error", ex);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //            dtgOralt.CurrentRow.Cells[1].Value = val;
        //        }

        //    }
        //}

        //private void dtgOralN_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        //{
        //    string prb = Convert.ToString(dtgOralN.CurrentRow.Cells[1].Value);
        //    if (e.ColumnIndex == this.dtgOralN.Columns[2].Index || e.ColumnIndex == this.dtgOralN.Columns[3].Index)
        //    {
        //        if (prb != "")
        //        {
        //            try
        //            {
        //                valida = Int32.TryParse(Convert.ToString(dtgOralN.CurrentRow.Cells[1].Value), out thora);
        //                if (valida)
        //                {
        //                    if (thora < 2400 && thora > 1759)
        //                    {
        //                        if (thora < 1759)
        //                        {
        //                            MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                            DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                            dtgOralN.CurrentRow.Cells[1].Value = val;
        //                        }
        //                    }
        //                    else if (thora < 600 && thora > 0)
        //                    {
        //                        if (thora > 600)
        //                        {
        //                            MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                            DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                            dtgOralN.CurrentRow.Cells[1].Value = val;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgOralN.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception("Error", ex);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //            dtgOralN.CurrentRow.Cells[1].Value = val;
        //        }

        //    }
        //}

        //private void dtgParental_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        //{
        //    string prb = Convert.ToString(dtgParental.CurrentRow.Cells[1].Value);
        //    if (e.ColumnIndex == this.dtgParental.Columns[2].Index || e.ColumnIndex == this.dtgParental.Columns[3].Index)
        //    {
        //        if (prb != "")
        //        {
        //            try
        //            {
        //                valida = Int32.TryParse(Convert.ToString(dtgParental.CurrentRow.Cells[1].Value), out thora);
        //                if (valida)
        //                {
        //                    if (thora < 559 || thora >= 1200)
        //                    {
        //                        MessageBox.Show("La hora de la mañana no puede der mayor a las 12:00 o menor de las 06:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //                        dtgParental.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception("Error", ex);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //            dtgParental.CurrentRow.Cells[1].Value = val;
        //        }

        //    }
        //}

        //private void dtgParentalT_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        //{
        //    string prb = Convert.ToString(dtgParentalT.CurrentRow.Cells[1].Value);
        //    if (e.ColumnIndex == this.dtgParentalT.Columns[2].Index || e.ColumnIndex == this.dtgParentalT.Columns[3].Index)
        //    {
        //        if (prb != "")
        //        {
        //            try
        //            {
        //                valida = Int32.TryParse(Convert.ToString(dtgParentalT.CurrentRow.Cells[1].Value), out thora);
        //                if (valida)
        //                {
        //                    if (thora < 1159 || thora >= 1800)
        //                    {
        //                        MessageBox.Show("La hora de la tarde no puede der mayor a las 18:00 o menor de las 12:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //                        dtgParentalT.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception("Error", ex);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //            dtgParentalT.CurrentRow.Cells[1].Value = val;
        //        }

        //    }
        //}

        //private void dtgParentalN_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        //{
        //    string prb = Convert.ToString(dtgParentalN.CurrentRow.Cells[1].Value);
        //    if (e.ColumnIndex == this.dtgParentalN.Columns[2].Index || e.ColumnIndex == this.dtgParentalN.Columns[3].Index)
        //    {
        //        if (prb != "")
        //        {
        //            try
        //            {
        //                valida = Int32.TryParse(Convert.ToString(dtgParentalN.CurrentRow.Cells[1].Value), out thora);
        //                if (valida)
        //                {
        //                    if (thora < 2400 && thora > 1759)
        //                    {
        //                        if (thora < 1759)
        //                        {
        //                            MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                            DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                            dtgParentalN.CurrentRow.Cells[1].Value = val;
        //                        }
        //                    }
        //                    else if (thora < 600 && thora > 0)
        //                    {
        //                        if (thora > 600)
        //                        {
        //                            MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                            DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                            dtgParentalN.CurrentRow.Cells[1].Value = val;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgParentalN.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception("Error", ex);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //            dtgParentalN.CurrentRow.Cells[1].Value = val;
        //        }

        //    }
        //}

        //private void dtgOrina_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        //{
        //    string prb = Convert.ToString(dtgOrina.CurrentRow.Cells[1].Value);
        //    if (e.ColumnIndex == this.dtgOrina.Columns[2].Index || e.ColumnIndex == this.dtgOrina.Columns[3].Index)
        //    {
        //        if (prb != "")
        //        {
        //            try
        //            {
        //                valida = Int32.TryParse(Convert.ToString(dtgOrina.CurrentRow.Cells[1].Value), out thora);
        //                if (valida)
        //                {
        //                    if (thora < 559 || thora >= 1200)
        //                    {
        //                        MessageBox.Show("La hora de la mañana no puede der mayor a las 12:00 o menor de las 06:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //                        dtgOrina.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception("Error", ex);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //            dtgOrina.CurrentRow.Cells[1].Value = val;
        //        }

        //    }
        //}

        //private void dtgOrinaT_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        //{
        //    string prb = Convert.ToString(dtgOrinaT.CurrentRow.Cells[1].Value);
        //    if (e.ColumnIndex == this.dtgOrinaT.Columns[2].Index || e.ColumnIndex == this.dtgOrinaT.Columns[3].Index)
        //    {
        //        if (prb != "")
        //        {
        //            try
        //            {
        //                valida = Int32.TryParse(Convert.ToString(dtgOrinaT.CurrentRow.Cells[1].Value), out thora);
        //                if (valida)
        //                {
        //                    if (thora < 1159 || thora >= 1800)
        //                    {
        //                        MessageBox.Show("La hora de la tarde no puede der mayor a las 18:00 o menor de las 12:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //                        dtgOrinaT.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception("Error", ex);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //            dtgOrinaT.CurrentRow.Cells[1].Value = val;
        //        }

        //    }
        //}

        //private void dtgOrinaN_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        //{
        //    string prb = Convert.ToString(dtgOrinaN.CurrentRow.Cells[1].Value);

        //    if (e.ColumnIndex == this.dtgOrinaN.Columns[2].Index || e.ColumnIndex == this.dtgOrinaN.Columns[3].Index)
        //    {
        //        if (prb != "")
        //        {
        //            try
        //            {
        //                valida = Int32.TryParse(Convert.ToString(dtgOrinaN.CurrentRow.Cells[1].Value), out thora);
        //                if (valida)
        //                {
        //                    if (thora < 2400 && thora > 1759)
        //                    {
        //                        if (thora < 1759)
        //                        {
        //                            MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                            DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                            dtgOrinaN.CurrentRow.Cells[1].Value = val;
        //                        }
        //                    }
        //                    else if (thora < 600 && thora > 0)
        //                    {
        //                        if (thora > 600)
        //                        {
        //                            MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                            DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                            dtgOrinaN.CurrentRow.Cells[1].Value = val;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgOrinaN.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception("Error", ex);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //            dtgOrinaN.CurrentRow.Cells[1].Value = val;
        //        }
        //    }
        //}

        //private void dtgOtros_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        //{
        //    string prb = Convert.ToString(dtgOtros.CurrentRow.Cells[1].Value);
        //    if (e.ColumnIndex == this.dtgOtros.Columns[2].Index || e.ColumnIndex == this.dtgOtros.Columns[3].Index)
        //    {
        //        if (prb != "")
        //        {
        //            try
        //            {
        //                valida = Int32.TryParse(Convert.ToString(dtgOtros.CurrentRow.Cells[1].Value), out thora);
        //                if (valida)
        //                {
        //                    if (thora < 559 || thora >= 1200)
        //                    {
        //                        MessageBox.Show("La hora de la mañana no puede der mayor a las 12:00 o menor de las 06:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //                        dtgOtros.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception("Error", ex);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            DateTime val = new DateTime(2022, 8, 03, 6, 0, 0);
        //            dtgOtros.CurrentRow.Cells[1].Value = val;
        //        }

        //    }
        //}

        //private void dtgOtrosT_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        //{
        //    string prb = Convert.ToString(dtgOtrosT.CurrentRow.Cells[1].Value);
        //    if (e.ColumnIndex == this.dtgOtrosT.Columns[2].Index || e.ColumnIndex == this.dtgOtrosT.Columns[3].Index)
        //    {
        //        if (prb != "")
        //        {
        //            try
        //            {
        //                valida = Int32.TryParse(Convert.ToString(dtgOtrosT.CurrentRow.Cells[1].Value), out thora);
        //                if (valida)
        //                {
        //                    if (thora < 1159 || thora >= 1800)
        //                    {
        //                        MessageBox.Show("La hora de la tarde no puede der mayor a las 18:00 o menor de las 12:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //                        dtgOtrosT.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }
        //                //else
        //                //{
        //                //    if (Convert.ToDateTime(dtgOtrosT.CurrentRow.Cells[1].Value) < desde || Convert.ToDateTime(dtgOtrosT.CurrentRow.Cells[1].Value) > hasta)
        //                //    {
        //                //        MessageBox.Show("La hora de la tarde no puede der mayor a las 18:00 o menor de las 12:00");
        //                //        DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //                //        dtgOtrosT.CurrentRow.Cells[1].Value = val;
        //                //    }
        //                //}
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception("Error", ex);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            DateTime val = new DateTime(2022, 8, 03, 12, 0, 0);
        //            dtgOtrosT.CurrentRow.Cells[1].Value = val;
        //        }

        //    }
        //}

        //private void dtgOtrosN_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        //{
        //    string prb = Convert.ToString(dtgOtrosN.CurrentRow.Cells[1].Value);

        //    if (e.ColumnIndex == this.dtgOtrosN.Columns[2].Index || e.ColumnIndex == this.dtgOtrosN.Columns[3].Index)
        //    {
        //        if (prb != "")
        //        {
        //            try
        //            {
        //                valida = Int32.TryParse(Convert.ToString(dtgOtrosN.CurrentRow.Cells[1].Value), out thora);
        //                if (valida)
        //                {
        //                    if (thora < 2400 && thora > 1759)
        //                    {
        //                        if (thora < 1759)
        //                        {
        //                            MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                            DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                            dtgOtrosN.CurrentRow.Cells[1].Value = val;
        //                        }
        //                    }
        //                    else if (thora < 600 && thora > 0)
        //                    {
        //                        if (thora > 600)
        //                        {
        //                            MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                            DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                            dtgOtrosN.CurrentRow.Cells[1].Value = val;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("La hora de la noche no puede der mayor a las 6:00 o menor de las 18:00");
        //                        DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //                        dtgOtrosN.CurrentRow.Cells[1].Value = val;
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception("Error", ex);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Debe ingresar una hora para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            DateTime val = new DateTime(2022, 8, 03, 18, 0, 0);
        //            dtgOtrosN.CurrentRow.Cells[1].Value = val;
        //        }
        //    }
        //}

        #endregion

        private void textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }
        private void dtgOral_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl text = (DataGridViewTextBoxEditingControl)e.Control;
            text.KeyPress -= new KeyPressEventHandler(textbox_KeyPress);
            text.KeyPress += new KeyPressEventHandler(textbox_KeyPress);
        }

        private void dtgOralt_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl text = (DataGridViewTextBoxEditingControl)e.Control;
            text.KeyPress -= new KeyPressEventHandler(textbox_KeyPress);
            text.KeyPress += new KeyPressEventHandler(textbox_KeyPress);
        }

        private void dtgOralN_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl text = (DataGridViewTextBoxEditingControl)e.Control;
            text.KeyPress -= new KeyPressEventHandler(textbox_KeyPress);
            text.KeyPress += new KeyPressEventHandler(textbox_KeyPress);
        }

        private void dtgParentalN_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl text = (DataGridViewTextBoxEditingControl)e.Control;
            text.KeyPress -= new KeyPressEventHandler(textbox_KeyPress);
            text.KeyPress += new KeyPressEventHandler(textbox_KeyPress);
        }

        private void dtgOrina_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl text = (DataGridViewTextBoxEditingControl)e.Control;
            text.KeyPress -= new KeyPressEventHandler(textbox_KeyPress);
            text.KeyPress += new KeyPressEventHandler(textbox_KeyPress);
        }

        private void dtgOrinaT_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl text = (DataGridViewTextBoxEditingControl)e.Control;
            text.KeyPress -= new KeyPressEventHandler(textbox_KeyPress);
            text.KeyPress += new KeyPressEventHandler(textbox_KeyPress);
        }

        private void dtgOrinaN_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl text = (DataGridViewTextBoxEditingControl)e.Control;
            text.KeyPress -= new KeyPressEventHandler(textbox_KeyPress);
            text.KeyPress += new KeyPressEventHandler(textbox_KeyPress);
        }

        private void dtgOtros_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl text = (DataGridViewTextBoxEditingControl)e.Control;
            text.KeyPress -= new KeyPressEventHandler(textbox_KeyPress);
            text.KeyPress += new KeyPressEventHandler(textbox_KeyPress);
        }

        private void dtgOtrosT_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl text = (DataGridViewTextBoxEditingControl)e.Control;
            text.KeyPress -= new KeyPressEventHandler(textbox_KeyPress);
            text.KeyPress += new KeyPressEventHandler(textbox_KeyPress);
        }

        private void dtgOtrosN_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl text = (DataGridViewTextBoxEditingControl)e.Control;
            text.KeyPress -= new KeyPressEventHandler(textbox_KeyPress);
            text.KeyPress += new KeyPressEventHandler(textbox_KeyPress);
        }

        private void dtgParental_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl text = (DataGridViewTextBoxEditingControl)e.Control;
            text.KeyPress -= new KeyPressEventHandler(textbox_KeyPress);
            text.KeyPress += new KeyPressEventHandler(textbox_KeyPress);
        }

        private void dtgParentalT_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl text = (DataGridViewTextBoxEditingControl)e.Control;
            text.KeyPress -= new KeyPressEventHandler(textbox_KeyPress);
            text.KeyPress += new KeyPressEventHandler(textbox_KeyPress);
        }
        public void LimpiarOral()
        {
            umskHoraO.Text = "";
            txtClaseO.Text = "";
            txtCantidadO.Text = "";
        }
        public void LimpiarParenteral()
        {
            umskHoraP.Text = "";
            txtClaseP.Text = "";
            txtClaseP1.Text = "";
            txtCantidadP.Text = "";
            cod = 0;
        }
        public void LimpiarEnteral()
        {
            umskHoraE.Text = "";
            txtClaseE.Text = "";
            txtCantidadE.Text = "";
            cod = 0;
        }
        public void LimpiarOrina()
        {
            umskHoraR.Text = "";
            txtClaseR.Text = "";
            txtCantidadR.Text = "";
            txtDescripcionR.Text = "";
        }
        public void LimpiarOtros()
        {
            umskHoraT.Text = "";
            txtClaseT.Text = "";
            txtCantidadT.Text = "";
        }
        public void LimpiarVomito()
        {
            umskHoraV.Text = "";
            txtClaseV.Text = "";
            txtCantidadV.Text = "";
        }
        public void LimpiarDrenaje()
        {
            umskHoraD.Text = "";
            txtClaseD.Text = "";
            cmbDrenaje.Text = "";
            cmbDrenaje.SelectedIndex = -1;
            txtCantidadD.Text = "";
        }
        public void LimpiarDeposiciones()
        {
            umskHoraDP.Text = "";
            txtClaseDP.Text = "";
            txtCantidadDP.Text = "";
            txtDescripcionDP.Text = "";
        }
        private void RegisraOral()
        {
            var nowTime = DateTime.Parse(umskHoraO.Text);
            var startTimeM = DateTime.Parse("07:01");
            var endTimeM = DateTime.Parse("13:00");

            var startTimeT = DateTime.Parse("13:01");
            var endTimeT = DateTime.Parse("19:00");

            var startTimeN = DateTime.Parse("19:01");
            var endTimeN = DateTime.Parse("07:00");

            if ((nowTime <= endTimeM) && (nowTime >= startTimeM))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraO.Text;
                    _clase.Value = txtClaseO.Text;
                    _cantidad.Value = txtCantidadO.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    dtgOral.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            if ((nowTime <= endTimeT) && (nowTime >= startTimeT))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraO.Text;
                    _clase.Value = txtClaseO.Text;
                    _cantidad.Value = txtCantidadO.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    dtgOralt.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            if ((nowTime <= endTimeN) || (nowTime >= startTimeN))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraO.Text;
                    _clase.Value = txtClaseO.Text;
                    _cantidad.Value = txtCantidadO.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    dtgOralN.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
        private void RegisraParenteral()
        {
            var nowTime = DateTime.Parse(umskHoraP.Text);
            var startTimeM = DateTime.Parse("07:01");
            var endTimeM = DateTime.Parse("13:00");

            var startTimeT = DateTime.Parse("13:01");
            var endTimeT = DateTime.Parse("19:00");

            var startTimeN = DateTime.Parse("19:01");
            var endTimeN = DateTime.Parse("07:00");

            if ((nowTime <= endTimeM) && (nowTime >= startTimeM))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _compuesto = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraP.Text;
                    _clase.Value = txtClaseP1.Text;
                    _cantidad.Value = txtCantidadP.Text;
                    _compuesto.Value = cod;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    fila.Cells.Add(_compuesto);
                    dtgParental.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            if ((nowTime <= endTimeT) && (nowTime >= startTimeT))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _compuesto = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraP.Text;
                    _clase.Value = txtClaseP1.Text;
                    _cantidad.Value = txtCantidadP.Text;
                    _compuesto.Value = cod;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    fila.Cells.Add(_compuesto);
                    dtgParentalT.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            if ((nowTime <= endTimeN) || (nowTime >= startTimeN))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _compuesto = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraP.Text;
                    _clase.Value = txtClaseP1.Text;
                    _cantidad.Value = txtCantidadP.Text;
                    _compuesto.Value = cod;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    fila.Cells.Add(_compuesto);
                    dtgParentalN.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
        private void RegisraEnteral()
        {
            var nowTime = DateTime.Parse(umskHoraE.Text);
            var startTimeM = DateTime.Parse("07:01");
            var endTimeM = DateTime.Parse("13:00");

            var startTimeT = DateTime.Parse("13:01");
            var endTimeT = DateTime.Parse("19:00");

            var startTimeN = DateTime.Parse("19:01");
            var endTimeN = DateTime.Parse("07:00");

            if ((nowTime <= endTimeM) && (nowTime >= startTimeM))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _compuesto = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraE.Text;
                    _clase.Value = txtClaseE.Text;
                    _cantidad.Value = txtCantidadE.Text;
                    _compuesto.Value = cod;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    fila.Cells.Add(_compuesto);
                    dtgEnteral.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            if ((nowTime <= endTimeT) && (nowTime >= startTimeT))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _compuesto = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraE.Text;
                    _clase.Value = txtClaseE.Text;
                    _cantidad.Value = txtCantidadE.Text;
                    _compuesto.Value = cod;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    fila.Cells.Add(_compuesto);
                    dtgEnteralT.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            if ((nowTime <= endTimeN) || (nowTime >= startTimeN))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _compuesto = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraE.Text;
                    _clase.Value = txtClaseE.Text;
                    _cantidad.Value = txtCantidadE.Text;
                    _compuesto.Value = cod;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    fila.Cells.Add(_compuesto);
                    dtgEnteralN.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
        private void RegisraOrina()
        {
            var nowTime = DateTime.Parse(umskHoraR.Text);
            var startTimeM = DateTime.Parse("07:01");
            var endTimeM = DateTime.Parse("13:00");

            var startTimeT = DateTime.Parse("13:01");
            var endTimeT = DateTime.Parse("19:00");

            var startTimeN = DateTime.Parse("19:01");
            var endTimeN = DateTime.Parse("07:00");

            if ((nowTime <= endTimeM) && (nowTime >= startTimeM))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _detalle = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraR.Text;
                    _clase.Value = txtClaseR.Text;
                    _cantidad.Value = txtCantidadR.Text;
                    _detalle.Value = txtDescripcionR.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    fila.Cells.Add(_detalle);
                    dtgOrina.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            if ((nowTime <= endTimeT) && (nowTime >= startTimeT))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _detalle = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraR.Text;
                    _clase.Value = txtClaseR.Text;
                    _cantidad.Value = txtCantidadR.Text;
                    _detalle.Value = txtDescripcionR.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    fila.Cells.Add(_detalle);
                    dtgOrinaT.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            if ((nowTime <= endTimeN) || (nowTime >= startTimeN))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _detalle = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraR.Text;
                    _clase.Value = txtClaseR.Text;
                    _cantidad.Value = txtCantidadR.Text;
                    _detalle.Value = txtDescripcionR.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    fila.Cells.Add(_detalle);
                    dtgOrinaN.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
        private void RegisraOtros()
        {
            var nowTime = DateTime.Parse(umskHoraT.Text);
            var startTimeM = DateTime.Parse("07:01");
            var endTimeM = DateTime.Parse("13:00");

            var startTimeT = DateTime.Parse("13:01");
            var endTimeT = DateTime.Parse("19:00");

            var startTimeN = DateTime.Parse("19:01");
            var endTimeN = DateTime.Parse("07:00");

            if ((nowTime <= endTimeM) && (nowTime >= startTimeM))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraT.Text;
                    _clase.Value = txtClaseT.Text;
                    _cantidad.Value = txtCantidadT.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    dtgOtros.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            if ((nowTime <= endTimeT) && (nowTime >= startTimeT))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraT.Text;
                    _clase.Value = txtClaseT.Text;
                    _cantidad.Value = txtCantidadT.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    dtgOtrosT.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            if ((nowTime <= endTimeN) || (nowTime >= startTimeN))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraT.Text;
                    _clase.Value = txtClaseT.Text;
                    _cantidad.Value = txtCantidadT.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    dtgOtrosN.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
        private void RegisraVomito()
        {
            var nowTime = DateTime.Parse(umskHoraV.Text);
            var startTimeM = DateTime.Parse("07:01");
            var endTimeM = DateTime.Parse("13:00");

            var startTimeT = DateTime.Parse("13:01");
            var endTimeT = DateTime.Parse("19:00");

            var startTimeN = DateTime.Parse("19:01");
            var endTimeN = DateTime.Parse("07:00");

            if ((nowTime <= endTimeM) && (nowTime >= startTimeM))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraV.Text;
                    _clase.Value = txtClaseV.Text;
                    _cantidad.Value = txtCantidadV.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    dtgVomito.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            if ((nowTime <= endTimeT) && (nowTime >= startTimeT))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraV.Text;
                    _clase.Value = txtClaseV.Text;
                    _cantidad.Value = txtCantidadV.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    dtgVomitoT.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            if ((nowTime <= endTimeN) || (nowTime >= startTimeN))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraV.Text;
                    _clase.Value = txtClaseV.Text;
                    _cantidad.Value = txtCantidadV.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    dtgVomitoN.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
        private void RegisraDrenaje()
        {
            var nowTime = DateTime.Parse(umskHoraD.Text);
            var startTimeM = DateTime.Parse("07:01");
            var endTimeM = DateTime.Parse("13:00");

            var startTimeT = DateTime.Parse("13:01");
            var endTimeT = DateTime.Parse("19:00");

            var startTimeN = DateTime.Parse("19:01");
            var endTimeN = DateTime.Parse("07:00");

            if ((nowTime <= endTimeM) && (nowTime >= startTimeM))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraD.Text;
                    _clase.Value = cmbDrenaje.Text.Trim();
                    _cantidad.Value = txtCantidadD.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    dtgDrenaje.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            if ((nowTime <= endTimeT) && (nowTime >= startTimeT))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraD.Text;
                    _clase.Value = cmbDrenaje.Text.Trim();
                    _cantidad.Value = txtCantidadD.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    dtgDrenajeT.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            if ((nowTime <= endTimeN) || (nowTime >= startTimeN))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraD.Text;
                    _clase.Value = cmbDrenaje.Text.Trim();
                    _cantidad.Value = txtCantidadD.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    dtgDrenajeN.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
        private void RegisraDeposiciones()
        {
            var nowTime = DateTime.Parse(umskHoraDP.Text);
            var startTimeM = DateTime.Parse("07:01");
            var endTimeM = DateTime.Parse("13:00");

            var startTimeT = DateTime.Parse("13:01");
            var endTimeT = DateTime.Parse("19:00");

            var startTimeN = DateTime.Parse("19:01");
            var endTimeN = DateTime.Parse("07:00");

            if ((nowTime <= endTimeM) && (nowTime >= startTimeM))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _detalle = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraDP.Text;
                    _clase.Value = txtClaseDP.Text;
                    _cantidad.Value = txtCantidadDP.Text;
                    _detalle.Value = txtDescripcionDP.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    fila.Cells.Add(_detalle);
                    dtgDeposiciones.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            if ((nowTime <= endTimeT) && (nowTime >= startTimeT))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _detalle = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraDP.Text;
                    _clase.Value = txtClaseDP.Text;
                    _cantidad.Value = txtCantidadDP.Text;
                    _detalle.Value = txtDescripcionDP.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    fila.Cells.Add(_detalle);
                    dtgDeposicionesT.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            if ((nowTime <= endTimeN) || (nowTime >= startTimeN))
            {
                try
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _hora = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _clase = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cantidad = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _detalle = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _hora.Value = umskHoraDP.Text;
                    _clase.Value = txtClaseDP.Text;
                    _cantidad.Value = txtCantidadDP.Text;
                    _detalle.Value = txtDescripcionDP.Text;

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_hora);
                    fila.Cells.Add(_clase);
                    fila.Cells.Add(_cantidad);
                    fila.Cells.Add(_detalle);
                    dtgDeposicionesN.Rows.Add(fila);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
        private void umskHoraO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtClaseO.Focus();
            }
        }

        private void txtClaseO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCantidadO.Focus();
            }
        }

        private void txtCantidadO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btnOral.Focus();
            }
        }
        private void umskHoraP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtClaseP1.Focus();
            }
        }
        private void txtClaseP1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCantidadP.Focus();
            }
        }
        private void txtClaseP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCantidadP.Focus();
            }
        }

        private void txtCantidadP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btnParenteral.Focus();
            }
        }
        private void btnOral_Click(object sender, EventArgs e)
        {
            if (txtCantidadO.Text != "" && umskHoraO.Text != ":")
            {
                errorProvider1.Clear();
                RegisraOral();
                LimpiarOral();
                umskHoraO.Focus();
            }
            else
                errorProvider1.SetError(txtCantidadO, "Ingrese hora y cantidad");
        }
        private void btnParenteral_Click(object sender, EventArgs e)
        {
            if (txtCantidadP.Text != "" && umskHoraP.Text != ":")
            {
                if (!validaParenteral())
                {
                    errorProvider1.Clear();
                    RegisraParenteral();
                    LimpiarParenteral();
                    umskHoraP.Focus();
                }
                else
                {
                    MessageBox.Show("El compuesto ya ha sigo agregado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtClaseP1.Text = "";
                    txtClaseP1.Focus();
                    cod = 0;
                }
            }
            else
                errorProvider1.SetError(txtCantidadP, "Ingrese hora y cantidad");
        }
        private void btnOrina_Click(object sender, EventArgs e)
        {
            if (txtCantidadR.Text != "" && umskHoraR.Text != ":")
            {
                errorProvider1.Clear();
                RegisraOrina();
                LimpiarOrina();
                umskHoraR.Focus();
            }
            else
                errorProvider1.SetError(txtCantidadR, "Ingrese hora y cantidad");
        }
        private void btnDeposiciones_Click(object sender, EventArgs e)
        {
            if (txtCantidadDP.Text != "" && umskHoraDP.Text != ":")
            {
                errorProvider1.Clear();
                RegisraDeposiciones();
                LimpiarDeposiciones();
                umskHoraDP.Focus();
            }
            else
                errorProvider1.SetError(txtCantidadDP, "Ingrese hora y cantidad");
        }
        private void btnDrenaje_Click(object sender, EventArgs e)
        {
            if (txtCantidadD.Text != "" && umskHoraD.Text != ":")
            {
                errorProvider1.Clear();
                RegisraDrenaje();
                LimpiarDrenaje();
                umskHoraD.Focus();
            }
            else
                errorProvider1.SetError(txtCantidadD, "Ingrese hora y cantidad");
        }
        private void btnOtros_Click(object sender, EventArgs e)
        {
            if (txtCantidadT.Text != "" && umskHoraT.Text != ":")
            {
                errorProvider1.Clear();
                RegisraOtros();
                LimpiarOtros();
                umskHoraT.Focus();
            }
            else
                errorProvider1.SetError(txtCantidadT, "Ingrese hora y cantidad");
        }


        private void txtCantidadR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btnOrina.Focus();
            }
        }


        private void btnOral_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtCantidadO.Text != "" && umskHoraO.Text != ":")
                {
                    errorProvider1.Clear();
                    RegisraOral();
                    LimpiarOral();
                    umskHoraO.Focus();
                }
                else
                    errorProvider1.SetError(txtCantidadO, "Ingrese hora y cantidad");
            }
        }
        private void btnParenteral_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtCantidadP.Text != "" && umskHoraP.Text != ":")
                {
                    if (!validaParenteral())
                    {
                        errorProvider1.Clear();
                        RegisraParenteral();
                        LimpiarParenteral();
                        umskHoraP.Focus();
                    }
                    else
                    {
                        MessageBox.Show("El compuesto ya ha sigo agregado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtClaseP1.Text = "";
                        txtClaseP1.Focus();
                        cod = 0;
                    }
                }
                else
                    errorProvider1.SetError(txtCantidadP, "Ingrese hora y cantidad");
            }
        }
        private void btnOrina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtCantidadR.Text != "" && umskHoraR.Text != ":")
                {
                    errorProvider1.Clear();
                    RegisraOrina();
                    LimpiarOrina();
                    umskHoraR.Focus();
                }
                else
                    errorProvider1.SetError(txtCantidadR, "Ingrese hora y cantidad");
            }
        }
        private void btnDeposiciones_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCantidadDP.Text != "" && umskHoraDP.Text != ":")
            {
                errorProvider1.Clear();
                RegisraDeposiciones();
                LimpiarDeposiciones();
                umskHoraDP.Focus();
            }
            else
                errorProvider1.SetError(txtCantidadDP, "Ingrese hora y cantidad");
        }
        private void btnDrenaje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtCantidadD.Text != "" && umskHoraD.Text != ":")
                {
                    errorProvider1.Clear();
                    RegisraDrenaje();
                    LimpiarDrenaje();
                    umskHoraD.Focus();
                }
                else
                    errorProvider1.SetError(txtCantidadD, "Ingrese hora y cantidad");
            }
        }
        private void btnOtros_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtCantidadT.Text != "" && umskHoraT.Text != ":")
                {
                    errorProvider1.Clear();
                    RegisraOtros();
                    LimpiarOtros();
                    umskHoraT.Focus();
                }
                else
                    errorProvider1.SetError(txtCantidadT, "Ingrese hora y cantidad");
            }
        }
        private void umskHoraR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtClaseR.Focus();
            }
        }

        private void txtClaseR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (valorSvActividad)
                    txtDescripcionR.Focus();
                else
                    txtCantidadR.Focus();
            }
        }
        private void umskHoraT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtClaseT.Focus();
            }
        }

        private void txtClaseT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCantidadT.Focus();
            }
        }

        private void txtCantidadT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btnOtros.Focus();
            }
        }
        private void umskHoraD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cmbDrenaje.Focus();
            }
        }

        private void txtClaseD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCantidadD.Focus();
            }
        }

        private void txtCantidadD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btnDrenaje.Focus();
            }
        }
        private void umskHoraDP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtClaseDP.Focus();
            }
        }

        private void txtClaseDP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtDescripcionDP.Focus();
            }
        }

        private void txtCantidadDP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btnDeposiciones.Focus();
            }
        }
        private void txtDescripcionDP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCantidadDP.Focus();
            }
        }

        private void txtDescripcionR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCantidadR.Focus();
            }
        }

        private void cmbDrenaje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCantidadD.Focus();
            }
        }
        private void ModificaSV()
        {
            Int32 ING_TOTAL = 0;
            Int32 ELM_TOTAL = 0;
            HC_SIGNOS_VITALES sv = new HC_SIGNOS_VITALES();
            sv.SV_ING_ORAL = Convert.ToString(NegIngestaEliminacion.SumaTotales(IE_CODIGO, "IO"));
            sv.SV_ING_PARENTAL = Convert.ToString(NegIngestaEliminacion.SumaTotales(IE_CODIGO, "IP"));
            ING_TOTAL = Convert.ToInt32(sv.SV_ING_ORAL) + Convert.ToInt32(sv.SV_ING_PARENTAL);
            sv.SV_ING_TOTAL = Convert.ToString(ING_TOTAL);
            sv.SV_ELM_ORINA = Convert.ToString(NegIngestaEliminacion.SumaTotales(IE_CODIGO, "EO"));
            sv.SV_ELM_DRENAJE = Convert.ToString(NegIngestaEliminacion.SumaTotales(IE_CODIGO, "ED"));
            sv.SV_ELM_OTROS = Convert.ToString(NegIngestaEliminacion.SumaTotales(IE_CODIGO, "ER"));
            ELM_TOTAL = Convert.ToInt32(sv.SV_ELM_ORINA) + Convert.ToInt32(sv.SV_ELM_DRENAJE) + Convert.ToInt32(sv.SV_ELM_OTROS);
            sv.SV_ELM_TOTAL = Convert.ToString(ELM_TOTAL);
            sv.SV_NUMERO_COMIDAS = Convert.ToString(NegIngestaEliminacion.sumaIngesta(IE_CODIGO, "IO"));
            sv.SV_NUMERO_MEDICIONES = Convert.ToString(NegIngestaEliminacion.sumaOrinaDeposiciones(IE_CODIGO, "EO"));
            sv.SV_NUMERO_DEPOSICIONES = Convert.ToString(NegIngestaEliminacion.sumaOrinaDeposiciones(IE_CODIGO, "EP"));
            if (!NegSignosVitales.EditarDesdeIngestaEliminacion(sv, CodigoAtencion, dtp_Creacion.Value.Date))
            {
                MessageBox.Show("No se lograron editar los valores en Formulario \n de Signos Vitales", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
        private void txtCantidadO_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txtCantidadP_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txtCantidadR_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if(!valorSvActividad)
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txtCantidadD_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txtCantidadT_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txtCantidadDP_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }
        private void txtCantidadV_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txtCantidadE_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }
        private void btnAyuda_Click(object sender, EventArgs e)
        {
            List<ABREVIACIONES> ab = NegIngestaEliminacion.listadoAbrevioaciones();
            frmAyudaIngElm frm = new frmAyudaIngElm(CodigoAtencion);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            if (frm.codigo != 0)
            {
                cod = frm.codigo;
                compuesto = NegIngestaEliminacion.compuestoKardex(frm.codigo);
                string[] arr = compuesto.Split(' ');
                compuesto = "";
                for (int i = 1; i < arr.Length; i++)
                {
                    foreach (var item in ab)
                    {
                        if (arr[i] == item.AB_ABREVIACION)
                            arr[i] = item.AB_DESCRIPCION;
                    }
                }
                for (int i = 2; i < arr.Length; i++)
                {
                    compuesto += arr[i] + " ";
                }
                txtClaseP1.Text = compuesto;
            }
        }
        public bool validaParenteral()
        {
            bool existe = false;
            //if (cod != 0) // se coemnta el proceso que valida el registro ya inresado
            //{
            //    for (int i = 0; i < dtgParental.Rows.Count - 1; i++)
            //    {
            //        if (Convert.ToInt64(dtgParental.Rows[i].Cells[4].Value) == cod)
            //        {
            //            existe = true;
            //        }
            //    }
            //    for (int i = 0; i < dtgParentalT.Rows.Count - 1; i++)
            //    {
            //        if (Convert.ToInt64(dtgParentalT.Rows[i].Cells[4].Value) == cod)
            //        {
            //            existe = true;
            //        }
            //    }
            //    for (int i = 0; i < dtgParentalN.Rows.Count - 1; i++)
            //    {
            //        if (Convert.ToInt64(dtgParentalN.Rows[i].Cells[4].Value) == cod)
            //        {
            //            existe = true;
            //        }
            //    }
            //}
            return existe;
        }

        private void btnAyudaEnteral_Click(object sender, EventArgs e)
        {
            List<ABREVIACIONES> ab = NegIngestaEliminacion.listadoAbrevioaciones();
            frmAyudaIngElm frm = new frmAyudaIngElm(CodigoAtencion);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            if (frm.codigo != 0)
            {
                cod = frm.codigo;
                compuesto = NegIngestaEliminacion.compuestoKardex(frm.codigo);
                string[] arr = compuesto.Split(' ');
                compuesto = "";
                for (int i = 1; i < arr.Length; i++)
                {
                    foreach (var item in ab)
                    {
                        if (arr[i] == item.AB_ABREVIACION)
                            arr[i] = item.AB_DESCRIPCION;
                    }
                }
                for (int i = 2; i < arr.Length; i++)
                {
                    compuesto += arr[i] + " ";
                }
                txtClaseE.Text = compuesto;
            }
        }

        private void btnEnteral_Click(object sender, EventArgs e)
        {
            if (txtCantidadE.Text != "" && umskHoraE.Text != ":")
            {
                if (!validaParenteral())
                {
                    errorProvider1.Clear();
                    RegisraEnteral();
                    LimpiarEnteral();
                    umskHoraE.Focus();
                }
                else
                {
                    MessageBox.Show("El compuesto ya ha sigo agregado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtClaseE.Text = "";
                    txtClaseE.Focus();
                    cod = 0;
                }
            }
            else
                errorProvider1.SetError(txtCantidadE, "Ingrese hora y cantidad");
        }

        private void btnVomito_Click(object sender, EventArgs e)
        {
            if (txtCantidadV.Text != "" && umskHoraV.Text != ":")
            {
                errorProvider1.Clear();
                RegisraVomito();
                LimpiarVomito();
                umskHoraV.Focus();
            }
            else
                errorProvider1.SetError(txtCantidadV, "Ingrese hora y cantidad");
        }

        private void umskHoraE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtClaseE.Focus();
            }
        }

        private void txtClaseE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCantidadE.Focus();
            }
        }

        private void txtCantidadE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btnEnteral.Focus();
            }
        }

        private void umskHoraV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtClaseV.Focus();
            }
        }

        private void txtClaseV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCantidadV.Focus();
            }
        }

        private void txtCantidadV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btnVomito.Focus();
            }
        }

    }
}
