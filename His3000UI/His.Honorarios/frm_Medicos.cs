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
using Core.Entidades;
using His.Entidades.Clases;
using His.Parametros;
using Recursos;
using His.General;
using Infragistics.Win.UltraWinGrid;
using System.IO;
using His.Formulario;

namespace His.Honorarios
{
    public partial class frm_Medicos : Form
    {
        #region Variables
        public DtoMedicos medicoOriginal = new DtoMedicos();
        public DtoMedicos medicoModificada = new DtoMedicos();
        public MEDICOS medOrigen = new MEDICOS();
        public MEDICOS medModificada = new MEDICOS();
        public List<ESPECIALIDADES_MEDICAS> especialidades = new List<ESPECIALIDADES_MEDICAS>();
        public List<RETENCIONES_FUENTE> retencionesfuente = new List<RETENCIONES_FUENTE>();
        public List<TIPO_HONORARIO> tipohonorario = new List<TIPO_HONORARIO>();
        public List<BANCOS> bancos = new List<BANCOS>();
        public List<TIPO_MEDICO> tipomedico = new List<TIPO_MEDICO>();
        public List<DtoMedicos> medico = new List<DtoMedicos>();
        public USUARIOS DatosUsuario = new USUARIOS();
        public List<DEPARTAMENTOS> departamento = new List<DEPARTAMENTOS>();
        public List<USUARIOS> usuarioslista = new List<USUARIOS>();
        public List<ESTADO_CIVIL> estadocivil = new List<ESTADO_CIVIL>();
        public bool buscar;
        public int columnabuscada;
        List<ACCESO_OPCIONES> perfilesAccesos = new List<ACCESO_OPCIONES>();
        public bool datgenerales;
        public bool dattransfer;
        public bool datsri;
        public bool creaModifica;
        //cadena de caracteres que acumula las teclas pulsadas en un segundo
        public string teclas = "";
        private bool inicio = true;
        private bool acccion = false;
        #endregion

        #region Constructor
        public frm_Medicos()
        {
            InitializeComponent();

        }
        #endregion

        #region Eventos
        private void frm_Medicos_Load(object sender, EventArgs e)
        {
            try
            {
                //perfilesAccesos =new NegPerfilesAcceso().AccesosUsuarios(Sesion.codUsuario,Constantes.HONORARIOS);
                //foreach (var acceso in perfilesAccesos)
                //{
                //    if (acceso.ID_ACCESO == 200011)
                //    {
                //        datgenerales = true;
                //        creaModifica = true;
                //    }
                //    if (acceso.ID_ACCESO == 200012)
                //    {
                //        dattransfer = true;
                //        creaModifica = true;
                //    }
                //    if (acceso.ID_ACCESO == 200013)
                //    {
                //        datsri = true;
                //        creaModifica = true;
                //    }
                //}
                //cargo valores por defecto
                btnExportarExcel.Image = Archivo.imgOfficeExcel;
                datgenerales = true;
                dattransfer = true;
                datsri = true;
                creaModifica = true;
                HalitarControles(false, false, false, false, false, false, creaModifica, false, false);
                RecuperaMedicos();
                //carga los especialidades en el combobox
                especialidades = NegEspecialidades.ListaEspecialidades();
                cmb_especialidad.DataSource = especialidades;
                cmb_especialidad.ValueMember = "ESP_CODIGO";
                cmb_especialidad.DisplayMember = "ESP_NOMBRE";
                cmb_especialidad.SelectedIndex = 0;
                //carga los retencionesfuente en el combobox
                retencionesfuente = NegRetencionesFuente.RecuperaRetencionesFuente();
                List<DtoRetencionFuente> retencion = new List<DtoRetencionFuente>();
                foreach (var row in retencionesfuente)
                {
                    retencion.Add(new DtoRetencionFuente()
                    {
                        RET_CODIGO = row.RET_CODIGO,
                        RET_DESCRIPCIONTOTAL = row.RET_REFERENCIA + " " + row.RET_DESCRIPCION + " " + row.RET_PORCENTAJE.ToString()
                    });
                }
                cmb_retencionfuetne.DataSource = retencion;
                cmb_retencionfuetne.ValueMember = "RET_CODIGO";
                cmb_retencionfuetne.DisplayMember = "RET_DESCRIPCIONTOTAL";
                cmb_retencionfuetne.SelectedIndex = 0;
                //
                bancos = NegBancos.RecuperaBancos();
                cmb_bancos.DataSource = bancos;
                cmb_bancos.ValueMember = "BAN_CODIGO";
                cmb_bancos.DisplayMember = "BAN_NOMBRE";
                cmb_bancos.SelectedIndex = 0;
                //
                tipomedico = NegTipoMedico.RecuperaTipoMedicos();
                cmb_tipoMedico.DataSource = tipomedico;
                cmb_tipoMedico.ValueMember = "TIM_CODIGO";
                cmb_tipoMedico.DisplayMember = "TIM_NOMBRE";
                cmb_tipoMedico.SelectedIndex = 0;
                //
                estadocivil = NegEstadoCivil.RecuperaEstadoCivil();
                cmb_estadocivil.DataSource = estadocivil;
                cmb_estadocivil.ValueMember = "ESC_CODIGO";
                cmb_estadocivil.DisplayMember = "ESC_NOMBRE";
                cmb_estadocivil.SelectedIndex = 0;

                departamento = NegDepartamentos.ListaDepartamentos();

                tipohonorario = NegTipoHonorario.RecuperaTipoHonorarios();
                cmb_tipohonorario.DataSource = tipohonorario;
                cmb_tipohonorario.ValueMember = "TIH_CODIGO";
                cmb_tipohonorario.DisplayMember = "TIH_NOMBRE";
                cmb_tipohonorario.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }

        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                GrabarDatos();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                GrabarDatos();

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }

        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult resultado;

                resultado = MessageBox.Show("Desea eliminar los Datos?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {

                    if (rbn_h.Checked == true)
                        medicoModificada.MED_GENERO = "H";
                    else
                        medicoModificada.MED_GENERO = "M";
                    if (rbn_ahorros.Checked == true)
                        medicoModificada.MED_TIPO_CUENTA = "A";
                    else
                        medicoModificada.MED_TIPO_CUENTA = "C";



                    medModificada.MED_CODIGO = medicoModificada.MED_CODIGO;
                    ESPECIALIDADES_MEDICAS espModificado = cmb_especialidad.SelectedItem as ESPECIALIDADES_MEDICAS;
                    medModificada.ESPECIALIDADES_MEDICASReference.EntityKey = espModificado.EntityKey;
                    RETENCIONES_FUENTE retModificado = retencionesfuente.Where(ret => ret.RET_CODIGO == medicoModificada.RET_CODIGO).FirstOrDefault();
                    medModificada.RETENCIONES_FUENTEReference.EntityKey = retModificado.EntityKey;
                    BANCOS bancoModificado = cmb_bancos.SelectedItem as BANCOS;
                    medModificada.BANCOSReference.EntityKey = bancoModificado.EntityKey;
                    TIPO_MEDICO tipomModificado = cmb_tipoMedico.SelectedItem as TIPO_MEDICO;
                    medModificada.TIPO_MEDICOReference.EntityKey = tipomModificado.EntityKey;
                    USUARIOS usuModificado = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == medicoModificada.ID_USUARIO).FirstOrDefault();
                    medModificada.USUARIOSReference.EntityKey = usuModificado.EntityKey;
                    ESTADO_CIVIL estcivilModificado = cmb_estadocivil.SelectedItem as ESTADO_CIVIL;
                    medModificada.ESTADO_CIVILReference.EntityKey = estcivilModificado.EntityKey;
                    TIPO_HONORARIO tipohonorarioModificado = cmb_tipohonorario.SelectedItem as TIPO_HONORARIO;
                    medModificada.TIPO_HONORARIOReference.EntityKey = tipohonorarioModificado.EntityKey;
                    medModificada.MED_NOMBRE1 = medicoModificada.MED_NOMBRE1;
                    medModificada.MED_NOMBRE2 = medicoModificada.MED_NOMBRE2;
                    medModificada.MED_APELLIDO_PATERNO = medicoModificada.MED_APELLIDO_PATERNO;
                    medModificada.MED_APELLIDO_MATERNO = medicoModificada.MED_APELLIDO_MATERNO;
                    medModificada.MED_FECHA = medicoModificada.MED_FECHA;
                    medModificada.MED_FECHA_NACIMIENTO = medicoModificada.MED_FECHA_NACIMIENTO;
                    medModificada.MED_DIRECCION = medicoModificada.MED_DIRECCION;
                    medModificada.MED_DIRECCION_CONSULTORIO = medicoModificada.MED_DIRECCION_CONSULTORIO;
                    medModificada.MED_RUC = medicoModificada.MED_RUC;
                    medModificada.MED_EMAIL = medicoModificada.MED_EMAIL;
                    medModificada.MED_GENERO = medicoModificada.MED_GENERO;
                    //medModificada.MED_ESTADO_CIVIL = medicoModificada.MED_ESTADO_CIVIL;
                    medModificada.MED_NUM_CUENTA = medicoModificada.MED_NUM_CUENTA;
                    medModificada.MED_TIPO_CUENTA = medicoModificada.MED_TIPO_CUENTA;
                    medModificada.MED_CUENTA_CONTABLE = medicoModificada.MED_CUENTA_CONTABLE;
                    medModificada.MED_TELEFONO_CASA = medicoModificada.MED_TELEFONO_CASA;
                    medModificada.MED_TELEFONO_CONSULTORIO = medicoModificada.MED_TELEFONO_CONSULTORIO;
                    medModificada.MED_TELEFONO_CELULAR = medicoModificada.MED_TELEFONO_CELULAR;
                    medModificada.MED_CODIGO_MEDICO = medicoModificada.MED_CODIGO_MEDICO;
                    medModificada.MED_AUTORIZACION_SRI = medicoModificada.MED_AUTORIZACION_SRI;
                    medModificada.MED_VALIDEZ_AUTORIZACION = medicoModificada.MED_VALIDEZ_AUTORIZACION;
                    medModificada.MED_FACTURA_INICIAL = medicoModificada.MED_FACTURA_INICIAL.Replace("-", "");
                    medModificada.MED_FACTURA_FINAL = medicoModificada.MED_FACTURA_FINAL.Replace("-", "");
                    medModificada.MED_CON_TRANSFERENCIA = medicoModificada.MED_CON_TRANSFERENCIA;
                    medModificada.MED_RECIBE_LLAMADA = medicoModificada.MED_RECIBE_LLAMADA;
                    medModificada.MED_ESTADO = medicoModificada.MED_ESTADO;
                    medModificada.MED_FECHA_MODIFICACION = medicoModificada.MED_FECHA_MODIFICACION;
                    if (medicoModificada.MED_ESTADO != medicoOriginal.MED_ESTADO)
                        medModificada.MED_FECHA_MODIFICACION = DateTime.Now;

                    medModificada.EntityKey = new EntityKey(medico.First().ENTITYSETNAME
                            , medico.First().ENTITYID, medicoModificada.MED_CODIGO);

                    NegMedicos.EliminarMedico(medModificada.ClonarEntidad());
                    RecuperaMedicos();
                    ResetearControles();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, false, true, false, false);
                    MessageBox.Show("Datos Eliminados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Operación Invalida, no se puede eliminar el Registro hay datos relacionados.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HalitarControles(creaModifica, creaModifica, creaModifica, false, true, false, false, true, false);
                medicoOriginal = new DtoMedicos();
                medicoModificada = new DtoMedicos();
                NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();
                ResetearControles();
                MessageBox.Show("Para recuperar Medicos ya creados como Usuarios presione F1.", "Medicos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (NegNumeroControl.NumerodeControlAutomatico(2))
                {
                    numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 2).FirstOrDefault();
                    medicoModificada.MED_CODIGO = int.Parse(numerocontrol.NUMCON);
                    // medicoModificada.MED_CODIGO = int.Parse((NegMedicos.RecuperaMaximoMedicos() + 1).ToString());
                }
                else
                    txt_medcodigo.ReadOnly = false;
                btn_AyudaMedicos.Enabled = true;
                medicoModificada.MED_FECHA = DateTime.Now;
                medicoModificada.MED_ESTADO = true;
                medicoModificada.MED_CON_TRANSFERENCIA = false;

                AgregarBindigControles();

                //txt_fecing.Text = DateTime.Now.ToString("d");
                //txt_medcodigo.Focus();

                cmb_retencionfuetne.SelectedIndex = 0;
                cmb_bancos.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txt_Nombre1.Focus();
            ResetearControles();
            RecuperaMedicos();
            HalitarControles(false, false, false, false, false, false, true, false, false);
            controlErrores.Clear();

        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txt_Nombre1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                txt_Nombre2.Focus();
            }
        }
        private void txt_Nombre2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_Apellido1.Focus();
            }
        }
        private void txt_Apellido1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_Apellido2.Focus();
            }
        }
        private void txt_Apellido2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                txt_medruc.Focus();
            }
        }
        private void txt_medruc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    txt_fecnac.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void cmb_especialidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_medtelcasa.Focus();
            }
        }


        private void txt_medtelcasa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    txt_medtelconsultorio.Focus();
                }
            }
            //e.Handled = false;
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_medtelconsultorio_KeyPress(object sender, KeyPressEventArgs e)
        {
            //se quita validacion por correo emitido por sistemas ejem: 2425698 ext 20  20220310_1647
            //if (Char.IsDigit(e.KeyChar))
            //    e.Handled = false;
            //else if (Char.IsControl(e.KeyChar))
            //{
            //    if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            //    {
            //        e.Handled = true;
            //        txt_medtelcelular.Focus();
            //    }
            //}
            //else if (Char.IsSeparator(e.KeyChar))
            //    e.Handled = false;
            //else
            //    e.Handled = true;
        }
        private void txt_medtelcelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    chk_recibellamadas.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_medcuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    rbn_corriente.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_medcodigo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {

                    frm_Ayudas lista = new frm_Ayudas(NegMedicos.ListaUsuariosNoMedicos());
                    //lista.consulta = NegMedicos.ListaUsuariosNoMedicos();
                    lista.ShowDialog();
                    if (lista.campoPadre.Text != string.Empty)
                    {
                        string codusuario = lista.campoPadre.Text;
                        DatosUsuario = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == Int16.Parse(codusuario)).FirstOrDefault();
                        //medicoModificada.MED_CODIGO = DatosUsuario.ID_USUARIO;
                        medicoModificada.ID_USUARIO = DatosUsuario.ID_USUARIO;
                        medicoModificada.MED_APELLIDO_PATERNO = DatosUsuario.APELLIDOS;
                        medicoModificada.MED_NOMBRE1 = DatosUsuario.NOMBRES;
                        medicoModificada.MED_DIRECCION = DatosUsuario.DIRECCION;
                        medicoModificada.MED_RUC = DatosUsuario.IDENTIFICACION;
                        AgregarBindigControles();

                        txt_Nombre2.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }

        }
        private void txt_medcodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsDigit(e.KeyChar))
                    e.Handled = false;
                else if (Char.IsControl(e.KeyChar))
                {
                    if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                    {
                        if (medicoModificada.ID_USUARIO == 0)
                        {
                            //medicoModificada.MED_CODIGO = Int16.Parse((NegUsuarios.RecuperaMaximoUsuario() + 1).ToString());
                            medicoModificada.ID_USUARIO = Int16.Parse((NegUsuarios.RecuperaMaximoUsuario() + 1).ToString());
                            medicoModificada.MED_FECHA = DateTime.Now;
                            medicoModificada.MED_ESTADO = true;
                            AgregarBindigControles();
                        }
                        e.Handled = true;
                        txt_Nombre1.Focus();
                    }
                }
                else if (Char.IsSeparator(e.KeyChar))
                    e.Handled = false;
                else
                    e.Handled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void txt_medautorizaret_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    txt_ValidezAutoriz.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_facini_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    txt_facfin.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_facfin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    cmb_retencionfuetne.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_facfin_Leave(object sender, EventArgs e)
        {
            //if (txt_facfin.Text.Length < 15)
            //{
            //    MessageBox.Show("Ingrese la numeración de la factura completa", "Medicos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txt_facfin.Focus();
            //}    
            //else 
            if (txt_facfin.Text.Replace(" ", "").Replace("-", "").Length < 13 && txt_facfin.Text.Replace(" ", "").Replace("-", "") != string.Empty)
            {
                if (txt_facfin.Text.Replace(" ", "").Replace("-", "").Length <= 7)
                {
                    txt_facfin.Text = string.Format("{0:001-001-0000000}", int.Parse(txt_facfin.Text.Replace(" ", "").Replace("-", "")));
                }
                else if (txt_facfin.Text.Replace(" ", "").Replace("-", "").Length <= 10)
                {
                    txt_facfin.Text = string.Format("{0:001-000-0000000}", Int64.Parse(txt_facfin.Text.Replace(" ", "").Replace("-", "")));
                }
                else
                {
                    txt_facfin.Text = string.Format("{0:000-000-0000000}", Int64.Parse(txt_facfin.Text.Replace(" ", "").Replace("-", "")));
                }
                //txt_facfin.Text = string.Format("{0:000}-{1:000}-{2:0000000}", int.Parse(txt_facfin.Text.Substring(0,3)),int.Parse(txt_facfin.Text.Substring(4,3)),int.Parse(txt_facfin.Text.Substring(7)));

            }
            if (txt_facfin.Text.Replace(" ", "").Replace("-", "") != string.Empty && txt_facini.Text.Replace(" ", "").Replace("-", "") != string.Empty)
            {
                if (int.Parse(txt_facfin.Text.Substring(8)) <= int.Parse(txt_facini.Text.Substring(8)))
                {
                    MessageBox.Show("Factura Final incorrecta, no puede ser menor o igual al número de factura inicial", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_facfin.Focus();
                }

            }
            else if (txt_facfin.Text.Replace(" ", "").Replace("-", "") == string.Empty && txt_medautorizaret.Text != string.Empty)
            {
                MessageBox.Show("Ingrese la información de Facturas", "Medicos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_facfin.Focus();
            }


        }
        private void txt_fecnac_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsDigit(e.KeyChar))
                    e.Handled = false;
                else if (Char.IsControl(e.KeyChar))
                {
                    if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                    {
                        DateTime fec = DateTime.Parse(txt_fecnac.Text);
                        e.Handled = true;
                        txt_CodigoLibro.Focus();
                    }
                }
                else if (Char.IsSeparator(e.KeyChar))
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fecha mal ingresada", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // MessageBox.Show(ex.InnerException.Message);
            }

        }





        private void txt_fecnac_Leave(object sender, EventArgs e)
        {
            try
            {
                DateTime fec = DateTime.Parse(txt_fecnac.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fecha mal ingresada", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void txt_direccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                txt_direccionconsultorio.Focus();
            }
        }

        private void txt_ValidezAutoriz_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    if (txt_ValidezAutoriz.Text != "    /  /  " && txt_ValidezAutoriz.Text != "    /" && txt_ValidezAutoriz.Text.Length >= 7)
                    {
                        if (Int16.Parse(txt_ValidezAutoriz.Text.Substring(0, 4)) < Int16.Parse(DateTime.Now.Year.ToString()))
                        {
                            MessageBox.Show("Año mal ingresado, debe ser mayor al año en curso", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_ValidezAutoriz.Focus();
                        }
                        else
                        {
                            if (Int16.Parse(txt_ValidezAutoriz.Text.Substring(5, 2)) > 12)
                                txt_ValidezAutoriz.Text = txt_ValidezAutoriz.Text.Substring(0, 4) + "12";
                            e.Handled = true;
                            txt_facini.Focus();
                        }
                    }
                    else
                    {
                        e.Handled = true;
                        txt_facini.Focus();
                    }

                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;

        }
        private void txt_ValidezAutoriz_Leave(object sender, EventArgs e)
        {
            if (txt_ValidezAutoriz.Text.Replace("/", "").Replace(" ", "") != string.Empty && txt_ValidezAutoriz.Text.Replace("/", "").Replace(" ", "").Length == 6)
            {
                if (Int16.Parse(txt_ValidezAutoriz.Text.Substring(0, 4)) < Int16.Parse(DateTime.Now.Year.ToString()))
                {
                    MessageBox.Show("Año mal ingresado, debe ser mayor al año en curso", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_ValidezAutoriz.Focus();
                }
                else
                {
                    if (Int16.Parse(txt_ValidezAutoriz.Text.Substring(5)) > 12)
                        txt_ValidezAutoriz.Text = txt_ValidezAutoriz.Text.Substring(0, 4) + "12";

                }
            }
            else if (txt_ValidezAutoriz.Text.Replace("/", "").Replace(" ", "").Length < 6 && txt_medautorizaret.Text != string.Empty)
            {
                MessageBox.Show("Fecha mal ingresada", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_ValidezAutoriz.Focus();
            }
            else if (txt_ValidezAutoriz.Text.Replace("/", "").Replace(" ", "") == string.Empty && txt_medautorizaret.Text != string.Empty)
            {
                MessageBox.Show("Ingrese la fecha de validez de la Autorización del SRI", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_ValidezAutoriz.Focus();
            }
        }
        private void cmb_bancos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                txt_medcuenta.Focus();
            }
        }
        private void txt_cuentacontable_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                txt_medautorizaret.Focus();
            }
        }
        private void cmb_tipoMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                cmb_tipohonorario.Focus();
            }
        }
        private void cmb_tipohonorario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                txt_medcodigo.Focus();
            }

        }
        private void cmb_estadocivil_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_mail.Focus();
            }
        }
        private void txt_mail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                txt_direccion.Focus();
            }
        }
        private void txt_medruc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (medicoOriginal.MED_CODIGO == 0)
                {
                    if (txt_medruc.Text != string.Empty)
                    {
                        if (NegValidaciones.esCedulaValida(txt_medruc.Text))
                        {
                            List<USUARIOS> validaruclist = NegUsuarios.RecuperaUsuarios().Where(ruc => ruc.IDENTIFICACION.Length >= 10).ToList();


                            USUARIOS validaruc = validaruclist.Where(cod => cod.IDENTIFICACION.Substring(0, 10) == txt_medruc.Text.Substring(0, 10)).FirstOrDefault(); //NegUsuarios.RecuperaUsuarios().Where(ruc => ruc.IDENTIFICACION.Substring(0, 10) == txt_medruc.Text.Substring(0, 10)).FirstOrDefault();

                            if (validaruc != null)
                            {
                                DtoMedicos validarucmedico = NegMedicos.RecuperaMedicosFormulario().Where(cd => cd.MED_RUC.Length >= 10).Where(rucm => rucm.MED_RUC.Substring(0, 10) == txt_medruc.Text.Substring(0, 10)).FirstOrDefault();
                                if (validarucmedico == null)
                                    medicoModificada.ID_USUARIO = validaruc.ID_USUARIO;
                                else
                                {
                                    MessageBox.Show("Medico ya ingresado", "Medicos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    medicoModificada = validarucmedico;
                                    medicoOriginal = medicoModificada.ClonarEntidad();
                                    AgregarBindigControles();
                                }
                            }
                            else
                            {

                                //    MessageBox.Show("Ruc incorrecto", "Medicos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //    txt_medruc.Focus();
                                //}

                            }



                        }

                        else
                        {
                            MessageBox.Show("Ruc incorrecto ", "Medicos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_medruc.Focus();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void chk_cheque_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_cheque.Checked == true)
                chk_tranferencia.Checked = false;
        }
        private void txt_facini_Leave(object sender, EventArgs e)
        {

            if (txt_facini.Text.Replace(" ", "").Replace("-", "").Length < 13 && txt_facini.Text.Replace(" ", "").Replace("-", "") != string.Empty)
            {
                if (txt_facini.Text.Replace(" ", "").Replace("-", "").Length <= 7)
                {
                    txt_facini.Text = string.Format("{0:001-001-0000000}", int.Parse(txt_facini.Text.Replace(" ", "").Replace("-", "")));
                }
                else if (txt_facini.Text.Replace(" ", "").Replace("-", "").Length <= 10)
                {
                    txt_facini.Text = string.Format("{0:001-000-0000000}", Int64.Parse(txt_facini.Text.Replace(" ", "").Replace("-", "")));
                }
                else
                {
                    txt_facini.Text = string.Format("{0:000-000-0000000}", Int64.Parse(txt_facini.Text.Replace(" ", "").Replace("-", "")));
                }
                txt_facfin.Text = txt_facini.Text.Substring(0, 7);
            }
            else if (txt_facini.Text.Length < 15 && txt_facini.Text.Replace(" ", "").Replace("-", "") != string.Empty)
            {
                MessageBox.Show("Ingrese la numeración de la factura completa", "Medicos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_facini.Focus();
            }
            else if (txt_facini.Text.Replace(" ", "").Replace("-", "") == string.Empty && txt_medautorizaret.Text != string.Empty)
            {
                MessageBox.Show("Ingrese la información de Facturas", "Medicos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_facini.Focus();
            }
        }
        private void chk_tranferencia_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tranferencia.Checked == true)
                chk_cheque.Checked = false;
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            frmReportes frm = new frmReportes();
            frm.reporte = "rMedicoFicha";
            frm.campo1 = txt_medcodigo.Text;
            frm.Show();
        }
        private void rbn_h_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                rbn_m.Focus();
            }
        }
        private void rbn_m_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                cmb_estadocivil.Focus();
            }
        }
        private void txt_codigoMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                cmb_estadocivil.Focus();
            }

            //if (Char.IsDigit(e.KeyChar))
            //    e.Handled = false;
            //else if (Char.IsControl(e.KeyChar))
            //{
            //    if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            //    {
            //        e.Handled = true;
            //        txt_fecnac.Focus();
            //    }
            //}
            //else if (Char.IsSeparator(e.KeyChar))
            //    e.Handled = false;
            //else
            //    e.Handled = true;
        }
        private void chk_recibellamadas_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                chk_activo.Focus();
            }

        }
        private void chk_cheque_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                chk_tranferencia.Focus();
            }

        }
        private void chk_tranferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                cmb_bancos.Focus();
            }
        }
        private void rbn_corriente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                rbn_ahorros.Focus();
            }
        }
        private void rbn_ahorros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_cuentacontable.Focus();
            }
        }
        private void txt_medtelcasa_Leave(object sender, EventArgs e)
        {
            if (txt_medtelcasa.Text != string.Empty && txt_medtelcasa.Text.Length < 9)
            {
                MessageBox.Show("Ingrese los 9 digitos del número telefónico", "Medicos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_medtelcasa.Focus();
            }
        }
        #endregion

        #region Metodos Privados
        private bool validarNuevoUSR(string user)
        {
            var usuarioExiste = (from m in usuarioslista
                                 where m.USR == user.ToLower()
                                 select m).ToList();
            if (usuarioExiste.Count == 0)
                return true;
            else
                return false;
        }

        private string usuarioId()
        {
            string usuario; //inicial nombre apellido
            string nombre = medicoModificada.MED_NOMBRE1 + medicoModificada.MED_NOMBRE2 + medicoModificada.MED_APELLIDO_MATERNO + "123456789123456789";
            string apellido = medicoModificada.MED_APELLIDO_PATERNO;
            for (int i = 1; i < 18; i++)
            {
                usuario = nombre.Substring(0, i) + apellido;
                if (validarNuevoUSR(usuario) == true)
                    return usuario;
            }

            //if (usuarioExiste.Count==0)
            //    return usuario.ToLower();
            //else
            //{   
            //    if (txt_Nombre2.Text!=string.Empty)
            //    { // inicial nombre1 inicial nombre2 apellido
            //        usuario = medicoModificada.MED_NOMBRE1.Substring(0, 1) + medicoModificada.MED_NOMBRE2.Substring(0, 1) + medicoModificada.MED_APELLIDO_PATERNO;
            //        if (usuarioslista.Where(usu => usu.USR == usuario.ToLower()).FirstOrDefault() == null)
            //            return usuario.ToLower();
            //        else
            //        { //nombre inicial nombre2 apellido
            //            usuario= medicoModificada.MED_NOMBRE1 + medicoModificada.MED_NOMBRE2.Substring(0, 1) + medicoModificada.MED_APELLIDO_PATERNO;
            //            if (usuarioslista.Where(usu => usu.USR == usuario.ToLower()).FirstOrDefault() == null)
            //                    return usuario.ToLower();
            //            else
            //            { //nombre  nombre2 apellido 
            //                usuario= medicoModificada.MED_NOMBRE1 + medicoModificada.MED_NOMBRE2 + medicoModificada.MED_APELLIDO_PATERNO;
            //                if (usuarioslista.Where(usu => usu.USR == usuario.ToLower()).FirstOrDefault() == null)
            //                        return usuario.ToLower();
            //                else
            //                {
            //                    if (txt_Apellido2.Text != string.Empty)
            //                    { //inicial nombre1 inicial nombre2, apellido inicial apellido2
            //                        usuario = medicoModificada.MED_NOMBRE1.Substring(0, 1) + medicoModificada.MED_NOMBRE2.Substring(0, 1) + medicoModificada.MED_APELLIDO_PATERNO + medicoModificada.MED_APELLIDO_MATERNO.Substring(0,1);
            //                        if (usuarioslista.Where(usu => usu.USR == usuario.ToLower()).FirstOrDefault() == null)
            //                            return usuario.ToLower();
            //                        else
            //                        {  //nombre inicial nombre2 apellido inicial apellido2
            //                            usuario = medicoModificada.MED_NOMBRE1 + medicoModificada.MED_NOMBRE2.Substring(0, 1) + medicoModificada.MED_APELLIDO_PATERNO + medicoModificada.MED_APELLIDO_MATERNO.Substring(0, 1);
            //                            if (usuarioslista.Where(usu => usu.USR == usuario.ToLower()).FirstOrDefault() == null)
            //                                return usuario.ToLower();
            //                            else
            //                            {  //nombre  nombre2 apellido inicial apellido2
            //                                usuario = medicoModificada.MED_NOMBRE1 + medicoModificada.MED_NOMBRE2 + medicoModificada.MED_APELLIDO_PATERNO + medicoModificada.MED_APELLIDO_MATERNO.Substring(0, 1);
            //                                if (usuarioslista.Where(usu => usu.USR == usuario.ToLower()).FirstOrDefault() == null)
            //                                    return usuario.ToLower();
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {  //nombre _ apellido 
            //        usuario= medicoModificada.MED_NOMBRE1.Substring(0,1)+"_"+medicoModificada.MED_APELLIDO_PATERNO;
            //        if (usuarioslista.Where(usu => usu.USR == usuario.ToLower()).FirstOrDefault() == null)
            //            return usuario.ToLower();
            //    }
            //}
            return medicoModificada.MED_NOMBRE1 + medicoModificada.MED_APELLIDO_PATERNO + DateTime.Now.Second.ToString();

        }
        protected virtual void HalitarControles(bool datosPrincipales, bool datosSecundarios, bool datossri, bool Modificar, bool Grabar, bool Eliminar, bool Nuevo, bool Cancelar, bool imprimir)
        {
            int usus = Sesion.codUsuario;
            //if (usus == 2526 || usus == 1)
            //{
            //btnNuevo.Enabled = Nuevo;
            //}
            //else
            //{
            //    btnNuevo.Enabled = false;
            //}
            btnNuevo.Enabled = Nuevo;
            btnActualizar.Enabled = Modificar;
            btnEliminar.Enabled = Eliminar;
            btnGuardar.Enabled = Grabar;
            btnCancelar.Enabled = Cancelar;

            //grpDatosPrincipales.Enabled = datosPrincipales;
            //grpDatosSecundario1.Enabled = datosSecundarios;
            //grpDatosSecundario2.Enabled = datossri;
            panelInfMedico.Enabled = datosPrincipales;
            panelInfSRI.Enabled = datossri;
            panelInfTransferencias.Enabled = datosSecundarios;

            btnImprimir.Enabled = imprimir;
        }
        private void RecuperaMedicos()
        {
            medico = NegMedicos.RecuperaMedicosFormulario();
            usuarioslista = NegUsuarios.RecuperaUsuarios();

            //gridMedicos.DataSource = medico.Select(m => new
            //{
            //    CODIGO = m.MED_CODIGO,
            //    //APELLIDO_PATERNO = m.MED_APELLIDO_PATERNO,
            //    //APELLIDO_MATERNO = m.MED_APELLIDO_MATERNO,
            //    //NOMBRE_1 = m.MED_NOMBRE1,
            //    //NOMBRE_2 = m.MED_NOMBRE2,
            //    NOMBRE = m.MED_APELLIDO_PATERNO + ' ' + m.MED_APELLIDO_MATERNO + ' ' + m.MED_NOMBRE1 + ' ' + m.MED_NOMBRE2,
            //    ESPECIALIDAD = m.ESP_NOMBRE,
            //    DIRECCION = m.MED_DIRECCION,
            //    DIRECCION_CONSULTORIO = m.MED_DIRECCION_CONSULTORIO,
            //    RUC = m.MED_RUC,
            //    TLF_CASA = m.MED_TELEFONO_CASA,
            //    TLF_CONSULTORIO = m.MED_TELEFONO_CONSULTORIO,
            //    TLF_CELULAR = m.MED_TELEFONO_CELULAR,
            //    ACTIVO = m.MED_ESTADO,
            //    EMAIL = m.MED_EMAIL
            //}).ToList();

            gridMedicos.DataSource = NegMedicos.VerMedicos();
            //gridMedicos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            //gridMedicos.AllowUserToResizeColumns = true;
            //gridMedicos.AutoResizeColumns();
            //gridMedicos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            //gridMedicos.Columns["MED_CODIGO"].HeaderText = "CODIGO";
            //gridMedicos.Columns["MED_NOMBRE1"].HeaderText = "NOMBRES";
            //gridMedicos.Columns["MED_NOMBRE2"].HeaderText = "";
            //gridMedicos.Columns["MED_APELLIDO_PATERNO"].HeaderText = "APELLIDOS";
            //gridMedicos.Columns["MED_APELLIDO_MATERNO"].HeaderText = "";
            //gridMedicos.Columns["ESP_NOMBRE"].HeaderText = "ESPECIALIDAD";
            //gridMedicos.Columns["MED_DIRECCION"].HeaderText = "DIRECCION";
            //gridMedicos.Columns["MED_TELEFONO_CASA"].HeaderText = "TLF. CASA";
            //gridMedicos.Columns["MED_TELEFONO_CONSULTORIO"].HeaderText = "TLF. CONSULTORIO";
            //gridMedicos.Columns["MED_TELEFONO_CELULAR"].HeaderText = "TLF. CELULAR";
            //gridMedicos.Columns["MED_ESTADO"].HeaderText = "ACTIVO";
            //gridMedicos.Columns["MED_DIRECCION_CONSULTORIO"].HeaderText = "DIRECCION CONSULTORIO";
            //gridMedicos.Columns["MED_RUC"].HeaderText = "RUC";

            //gridMedicos.Columns["ESP_CODIGO"].Visible = false;
            //gridMedicos.Columns["ID_USUARIO"].Visible = false;
            //gridMedicos.Columns["BAN_CODIGO"].Visible = false;
            //gridMedicos.Columns["TIH_CODIGO"].Visible = false;
            //gridMedicos.Columns["MED_FECHA_MODIFICACION"].Visible = false;
            //gridMedicos.Columns["MED_EMAIL"].Visible = false;
            //gridMedicos.Columns["TIM_CODIGO"].Visible = false;
            //gridMedicos.Columns["MED_FECHA"].Visible = false;
            //gridMedicos.Columns["MED_GENERO"].Visible = false;
            //gridMedicos.Columns["ESC_CODIGO"].Visible = false;
            //gridMedicos.Columns["MED_NUM_CUENTA"].Visible = false;
            //gridMedicos.Columns["MED_TIPO_CUENTA"].Visible = false;
            //gridMedicos.Columns["MED_CUENTA_CONTABLE"].Visible = false;
            //gridMedicos.Columns["MED_FECHA_NACIMIENTO"].Visible = false;
            //gridMedicos.Columns["MED_AUTORIZACION_SRI"].Visible = false;
            //gridMedicos.Columns["MED_VALIDEZ_AUTORIZACION"].Visible = false;
            //gridMedicos.Columns["RET_CODIGO"].Visible = false;
            //gridMedicos.Columns["RET_DESCRIPCION"].Visible = false;
            //gridMedicos.Columns["RET_PORCENTAJE"].Visible = false;
            //gridMedicos.Columns["MED_FACTURA_INICIAL"].Visible = false;
            //gridMedicos.Columns["MED_FACTURA_FINAL"].Visible = false;
            //gridMedicos.Columns["MED_CON_TRANSFERENCIA"].Visible = false;
            //gridMedicos.Columns["MED_RECIBE_LLAMADA"].Visible = false;
            //gridMedicos.Columns["MED_CODIGO_MEDICO"].Visible = false;
            //gridMedicos.Columns["ENTITYSETNAME"].Visible = false;
            //gridMedicos.Columns["ENTITYID"].Visible = false;
        }
        private void AgregarBindigControles()
        {
            Binding MED_CODIGO = new Binding("Text", medicoModificada, "MED_CODIGO", true);
            Binding RET_CODIGO = new Binding("SelectedValue", medicoModificada, "RET_CODIGO", true);
            Binding ESP_CODIGO = new Binding("SelectedValue", medicoModificada, "ESP_CODIGO", true);
            Binding BAN_CODIGO = new Binding("SelectedValue", medicoModificada, "BAN_CODIGO", true);
            Binding TIM_CODIGO = new Binding("SelectedValue", medicoModificada, "TIM_CODIGO", true);
            Binding TIH_CODIGO = new Binding("SelectedValue", medicoModificada, "TIH_CODIGO", true);
            Binding MED_NOMBRE1 = new Binding("Text", medicoModificada, "MED_NOMBRE1", true);
            Binding MED_NOMBRE2 = new Binding("Text", medicoModificada, "MED_NOMBRE2", true);
            Binding MED_APELLIDO_PATERNO = new Binding("Text", medicoModificada, "MED_APELLIDO_PATERNO", true);
            Binding MED_APELLIDO_MATERNO = new Binding("Text", medicoModificada, "MED_APELLIDO_MATERNO", true);
            Binding MED_FECHA_NACIMIENTO = new Binding("Text", medicoModificada, "MED_FECHA_NACIMIENTO", true);
            Binding MED_DIRECCION = new Binding("Text", medicoModificada, "MED_DIRECCION", true);
            Binding MED_RUC = new Binding("Text", medicoModificada, "MED_RUC", true);

            Binding MED_CODIGO_MEDICO = new Binding("Text", medicoModificada, "MED_CODIGO_MEDICO", true);
            Binding MED_CODIGO_LIBRO = new Binding("Text", medicoModificada, "MED_CODIGO_LIBRO", true);
            Binding MED_CODIGO_FOLIO = new Binding("Text", medicoModificada, "MED_CODIGO_FOLIO", true);

            Binding MED_DIRECCION_CONSULTORIO = new Binding("Text", medicoModificada, "MED_DIRECCION_CONSULTORIO", true);
            Binding ESC_CODIGO = new Binding("SelectedValue", medicoModificada, "ESC_CODIGO", true);
            Binding MED_NUM_CUENTA = new Binding("Text", medicoModificada, "MED_NUM_CUENTA", true);
            Binding MED_CUENTA_CONTABLE = new Binding("Text", medicoModificada, "MED_CUENTA_CONTABLE", true);
            Binding MED_TELEFONO_CASA = new Binding("Text", medicoModificada, "MED_TELEFONO_CASA", true);
            Binding MED_TELEFONO_CONSULTORIO = new Binding("Text", medicoModificada, "MED_TELEFONO_CONSULTORIO", true);
            Binding MED_TELEFONO_CELULAR = new Binding("Text", medicoModificada, "MED_TELEFONO_CELULAR", true);
            Binding MED_ACCIONISTA = new Binding("Checked", medicoModificada, "MED_ACCIONISTA", true);
            Binding MED_AUTORIZACION_SRI = new Binding("Text", medicoModificada, "MED_AUTORIZACION_SRI", true);
            Binding MED_VALIDEZ_AUTORIZACION = new Binding("Text", medicoModificada, "MED_VALIDEZ_AUTORIZACION", true);
            Binding MED_FACTURA_INICIAL = new Binding("Text", medicoModificada, "MED_FACTURA_INICIAL", true);
            Binding MED_FACTURA_FINAL = new Binding("Text", medicoModificada, "MED_FACTURA_FINAL", true);
            Binding MED_PORCENTAJE_RETENCION = new Binding("Text", medicoModificada, "MED_PORCENTAJE_RETENCION", true);
            Binding MED_ESTADO = new Binding("Checked", medicoModificada, "MED_ESTADO", true);
            Binding MED_FECHA = new Binding("Text", medicoModificada, "MED_FECHA", true);
            Binding MED_FECHA_MODIFICACION = new Binding("Text", medicoModificada, "MED_FECHA_MODIFICACION", true);
            Binding MED_EMAIL = new Binding("Text", medicoModificada, "MED_EMAIL", true);
            Binding MED_CON_TRANSFERENCIA = new Binding("Checked", medicoModificada, "MED_CON_TRANSFERENCIA", true);
            Binding MED_RECIBE_LLAMADA = new Binding("Checked", medicoModificada, "MED_CON_TRANSFERENCIA", true);
            //Binding PWD1 = new Binding("Text", usuarioModificada, "PWD", true);

            txt_Apellido1.DataBindings.Clear();
            txt_Apellido2.DataBindings.Clear();
            txt_cuentacontable.DataBindings.Clear();
            txt_direccion.DataBindings.Clear();
            txt_direccionconsultorio.DataBindings.Clear();
            txt_facfin.DataBindings.Clear();
            txt_facini.DataBindings.Clear();
            txt_fecnac.DataBindings.Clear();
            txt_medautorizaret.DataBindings.Clear();
            txt_medcodigo.DataBindings.Clear();
            txt_medcuenta.DataBindings.Clear();
            txt_medruc.DataBindings.Clear();

            txt_codigoMedico.DataBindings.Clear();
            txt_CodigoLibro.DataBindings.Clear();
            txt_CodigoFolio.DataBindings.Clear();

            txt_medtelcasa.DataBindings.Clear();
            txt_medtelcelular.DataBindings.Clear();
            txt_medtelconsultorio.DataBindings.Clear();
            txt_Nombre1.DataBindings.Clear();
            txt_Nombre2.DataBindings.Clear();
            txt_ValidezAutoriz.DataBindings.Clear();
            chk_activo.DataBindings.Clear();
            chk_tranferencia.DataBindings.Clear();
            chk_recibellamadas.DataBindings.Clear();
            cmb_especialidad.DataBindings.Clear();
            cmb_retencionfuetne.DataBindings.Clear();
            cmb_bancos.DataBindings.Clear();
            cmb_tipoMedico.DataBindings.Clear();
            cmb_estadocivil.DataBindings.Clear();
            cmb_tipohonorario.DataBindings.Clear();
            txt_ingreso.DataBindings.Clear();
            txt_modificadoactivo.DataBindings.Clear();
            txt_mail.DataBindings.Clear();
            //txt_conclave.DataBindings.Clear();

            txt_medcodigo.DataBindings.Add(MED_CODIGO);
            cmb_retencionfuetne.DataBindings.Add(RET_CODIGO);
            cmb_especialidad.DataBindings.Add(ESP_CODIGO);
            cmb_bancos.DataBindings.Add(BAN_CODIGO);
            cmb_tipoMedico.DataBindings.Add(TIM_CODIGO);
            cmb_estadocivil.DataBindings.Add(ESC_CODIGO);
            cmb_tipohonorario.DataBindings.Add(TIH_CODIGO);
            txt_Nombre1.DataBindings.Add(MED_NOMBRE1);
            txt_Nombre2.DataBindings.Add(MED_NOMBRE2);
            txt_Apellido1.DataBindings.Add(MED_APELLIDO_PATERNO);
            txt_Apellido2.DataBindings.Add(MED_APELLIDO_MATERNO);
            txt_fecnac.DataBindings.Add(MED_FECHA_NACIMIENTO);
            txt_direccion.DataBindings.Add(MED_DIRECCION);
            txt_direccionconsultorio.DataBindings.Add(MED_DIRECCION_CONSULTORIO);
            txt_medruc.DataBindings.Add(MED_RUC);

            txt_codigoMedico.DataBindings.Add(MED_CODIGO_MEDICO);
            txt_CodigoLibro.DataBindings.Add(MED_CODIGO_LIBRO);
            txt_CodigoFolio.DataBindings.Add(MED_CODIGO_FOLIO);

            txt_medcuenta.DataBindings.Add(MED_NUM_CUENTA);
            txt_cuentacontable.DataBindings.Add(MED_CUENTA_CONTABLE);
            txt_medtelcasa.DataBindings.Add(MED_TELEFONO_CASA);
            txt_medtelconsultorio.DataBindings.Add(MED_TELEFONO_CONSULTORIO);
            txt_medtelcelular.DataBindings.Add(MED_TELEFONO_CELULAR);
            txt_medautorizaret.DataBindings.Add(MED_AUTORIZACION_SRI);
            txt_ValidezAutoriz.DataBindings.Add(MED_VALIDEZ_AUTORIZACION);
            txt_facini.DataBindings.Add(MED_FACTURA_INICIAL);
            txt_facfin.DataBindings.Add(MED_FACTURA_FINAL);
            chk_activo.DataBindings.Add(MED_ESTADO);
            //chk_tranferencia.DataBindings.Add(MED_CON_TRANSFERENCIA);
            //chk_recibellamadas.DataBindings.Add(MED_RECIBE_LLAMADA);
            txt_ingreso.DataBindings.Add(MED_FECHA);
            txt_modificadoactivo.DataBindings.Add(MED_FECHA_MODIFICACION);
            txt_mail.DataBindings.Add(MED_EMAIL);


            if (medicoModificada.MED_CON_TRANSFERENCIA == true)
            {
                chk_tranferencia.Checked = true;
                chk_cheque.Checked = false;
            }
            else
            {
                chk_cheque.Checked = true;
                chk_tranferencia.Checked = false;
            }

            if (medicoModificada.MED_GENERO == "H")
                rbn_h.Checked = true;
            else
                rbn_m.Checked = true;
            if (medicoModificada.MED_TIPO_CUENTA == "A")
                rbn_ahorros.Checked = true;
            else
                rbn_corriente.Checked = true;
            if (medicoModificada.MED_RECIBE_LLAMADA)
                chk_recibellamadas.Checked = true;
            else
                chk_recibellamadas.Checked = false;

        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            medicoModificada = new DtoMedicos();
            medicoOriginal = new DtoMedicos();
            medModificada = new MEDICOS();
            medOrigen = new MEDICOS();
            DatosUsuario = new USUARIOS();

            txt_Apellido1.Text = string.Empty;
            txt_Apellido2.Text = string.Empty;
            txt_cuentacontable.Text = string.Empty;
            txt_direccion.Text = string.Empty;
            txtCodVendedor.Text = string.Empty;
            txtVendedor.Text = string.Empty;
            txt_facfin.Text = string.Empty;
            txt_facini.Text = string.Empty;
            txt_fecnac.Text = string.Empty;
            txt_medautorizaret.Text = string.Empty;
            txt_medcodigo.Text = string.Empty;
            txt_medcuenta.Text = string.Empty;
            txt_medruc.Text = string.Empty;
            txt_medtelcasa.Text = string.Empty;
            txt_medtelcelular.Text = string.Empty;
            txt_medtelconsultorio.Text = string.Empty;
            txt_Nombre1.Text = string.Empty;
            txt_Nombre2.Text = string.Empty;
            txt_ValidezAutoriz.Text = string.Empty;
            txt_direccionconsultorio.Text = string.Empty;
            txt_mail.Text = string.Empty;
            chk_activo.Checked = true;
            chk_tranferencia.Checked = false;
            chk_recibellamadas.Checked = false;
            cmb_especialidad.SelectedItem = 0;
            cmb_retencionfuetne.SelectedItem = 0;
            cmb_bancos.SelectedItem = 0;
            cmb_tipoMedico.SelectedItem = 0;
            cmb_estadocivil.SelectedItem = 0;
        }
        private bool ValidarFormulario()
        {
            controlErrores.Clear();
            bool valido = true;
            //if (medicoModificada.MED_CODIGO == 0)
            //{
            //    AgregarError(txt_medcodigo);
            //    valido = false;
            //}            
            //if (txt_medcodigo.Text == "0")
            //{
            //    AgregarError(cmb_especialidad);
            //    valido = false;
            //}

            if (txt_medruc.Text == string.Empty || txt_medruc.Text.Length != 13)
            {
                AgregarError(txt_medruc);
                valido = false;
            }

            if (txt_Nombre1.Text.Trim() == string.Empty)
            {
                AgregarError(txt_Nombre1);
                valido = false;
            }
            if (txt_Apellido1.Text.Trim() == string.Empty)
            {
                AgregarError(txt_Apellido1);
                valido = false;
            }

            if (cmb_estadocivil.SelectedIndex == -1)
            {
                AgregarError(cmb_estadocivil);
                valido = false;
            }
            if (txt_direccion.Text.Trim() == string.Empty)
            {
                AgregarError(txt_direccion);
                valido = false;
            }
            if (txt_mail.Text.Trim() == string.Empty)
            {
                AgregarError(txt_mail);
                valido = false;
            }
            if (cmb_especialidad.SelectedIndex == -1)
            {
                AgregarError(cmb_especialidad);
                valido = false;
            }
            if (txt_medtelcelular.Text.Trim() == string.Empty)
            {
                AgregarError(txt_medtelcelular);
                valido = false;
            }

            if (cmb_tipoMedico.SelectedIndex == -1)
            {
                AgregarError(cmb_tipoMedico);
                valido = false;
            }

            if (cmb_tipohonorario.SelectedIndex == -1)
            {
                AgregarError(cmb_tipohonorario);
                valido = false;
            }

            if (cmb_retencionfuetne.SelectedIndex == -1)
            {
                AgregarError(cmb_retencionfuetne);
                valido = false;
            }

            if (txt_medtelconsultorio.Text.Trim() == "")
            {
                AgregarError(txt_medtelconsultorio);
                valido = false;
            }
            if (txt_medtelcasa.Text.Trim() == "")
            {
                AgregarError(txt_medtelcasa);
                valido = false;
            }

            return valido;

        }



        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        //private bool ValidarFormulario()
        //{
        //    bool valido = true;
        //    //if (medicoModificada.MED_CODIGO == 0)
        //    //{
        //    //    AgregarError(txt_medcodigo);
        //    //    valido = false;
        //    //}            
        //    if(txt_medcodigo.Text == "0")
        //    {
        //        AgregarError(cmb_especialidad);
        //        valido = false;
        //    }
        //    if (medicoModificada.ESP_CODIGO == 0)
        //    {
        //        AgregarError(cmb_especialidad);
        //        valido = false;
        //    }
        //    if (medicoModificada.ESC_CODIGO == 0)
        //    {
        //        AgregarError(cmb_estadocivil);
        //        valido = false;
        //    }
        //    if (medicoModificada.RET_CODIGO == 0)
        //    {
        //        AgregarError(cmb_retencionfuetne);
        //        valido = false;
        //    }
        //    if (medicoModificada.MED_NOMBRE1 == null || medicoModificada.MED_NOMBRE1 == string.Empty)
        //    {
        //        AgregarError(txt_Nombre1);
        //        valido = false;
        //    }
        //    if (medicoModificada.MED_APELLIDO_PATERNO == null || medicoModificada.MED_APELLIDO_PATERNO == string.Empty)
        //    {
        //        AgregarError(txt_Apellido1);
        //        valido = false;
        //    }
        //    if (medicoModificada.MED_RUC == null || medicoModificada.MED_RUC == string.Empty)
        //    {
        //        AgregarError(txt_medruc);
        //        valido = false;
        //    }
        //    if (medicoModificada.MED_TELEFONO_CASA == null || medicoModificada.MED_TELEFONO_CASA == string.Empty)
        //    {
        //        AgregarError(txt_medtelcasa);
        //        valido = false;
        //    }
        //    if (medicoModificada.MED_DIRECCION == null || medicoModificada.MED_DIRECCION == string.Empty)
        //    {
        //        AgregarError(txt_direccion);
        //        valido = false;
        //    }
        //    if (medicoModificada.TIM_CODIGO == 0)
        //    {
        //        AgregarError(cmb_tipoMedico);
        //        valido = false;
        //    }
        //    //if (medicoModificada.BAN_CODIGO == 0)
        //    //{
        //    //    AgregarError(cmb_bancos);
        //    //    valido = false;
        //    //}
        //    if (medicoModificada.TIH_CODIGO == 0)
        //    {
        //        AgregarError(cmb_tipohonorario);
        //        valido = false;
        //    }
        //    return valido;

        //}

        public bool ValidaUsuario()
        {
            if (Sesion.codDepartamento == 4 || Sesion.codDepartamento == 1)
            {
                return true;
            }
            return false;
        }

        private void GrabarDatos()
        {
            try
            {
                //MEDICOS medic = NegMedicos.RecuperaMedicoId(Convert.ToInt16(txt_medcodigo.Text));
                //if (medic == null)
                //{
                if (ValidarFormulario())
                {
                    if (ValidaUsuario())
                        guardarMedicos();
                    else
                        MessageBox.Show("Ud. notiene permitido guardar cambios en la ficha del medico", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DialogResult validar = MessageBox.Show("No se ingreso información en campos requeridos", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (err.InnerException != null)
                    MessageBox.Show(err.InnerException.Message);
            }
        }

        private bool MedicoGuarda()
        {
            if (NegMedicos.existeMedico(txt_medruc.Text.Trim()))
            {
                MessageBox.Show("El medico ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            medicoModificada.MED_NOMBRE1 = txt_Nombre1.Text;//;
            medicoModificada.MED_NOMBRE2 = txt_Nombre2.Text;//;
            medicoModificada.MED_APELLIDO_PATERNO = DatosUsuario.APELLIDOS = txt_Apellido1.Text;//.Trim();
            medicoModificada.MED_APELLIDO_MATERNO = DatosUsuario.APELLIDOS = txt_Apellido2.Text;//.Trim();                   
            medicoModificada.MED_RUC = txt_medruc.Text;//.Trim();
            medicoModificada.MED_FECHA = Convert.ToDateTime(txt_ingreso.Text);//;            
            medicoModificada.MED_DIRECCION = txt_direccion.Text;// .Trim();
            medicoModificada.MED_ESTADO = true; //;
            if (txt_fecnac.Text == "  /  /")
                medicoModificada.MED_FECHA_NACIMIENTO = Convert.ToDateTime("01-01-1990");
            else
                medicoModificada.MED_FECHA_NACIMIENTO = Convert.ToDateTime(txt_fecnac.Text);
            if (rbn_h.Checked)
                medicoModificada.MED_GENERO = "H";
            else
                medicoModificada.MED_GENERO = "M";
            medicoModificada.MED_EMAIL = txt_mail.Text;
            medicoModificada.MED_DIRECCION_CONSULTORIO = txt_direccionconsultorio.Text;
            medicoModificada.MED_TELEFONO_CASA = txt_medtelcasa.Text;
            medicoModificada.MED_TELEFONO_CONSULTORIO = txt_medtelconsultorio.Text;
            medicoModificada.MED_TELEFONO_CELULAR = txt_medtelcelular.Text;
            medicoModificada.MED_CODIGO_FOLIO = "";

            medicoModificada.MED_CODIGO_MEDICO = "";
            medicoModificada.MED_NUM_CUENTA = "";
            medicoModificada.MED_TIPO_CUENTA = "";
            medicoModificada.MED_CUENTA_CONTABLE = NegMedicos.cuentaContable();
            medicoModificada.MED_AUTORIZACION_SRI = "";
            medicoModificada.MED_VALIDEZ_AUTORIZACION = "";
            medicoModificada.MED_FACTURA_INICIAL = "";
            medicoModificada.MED_FACTURA_FINAL = "";
            if (chk_recibellamadas.Checked)
                medicoModificada.MED_RECIBE_LLAMADA = true;
            else
                medicoModificada.MED_RECIBE_LLAMADA = false;
            medicoModificada.MED_FECHA_MODIFICACION = DateTime.Now;
            medicoModificada.MED_ESTADO = true;
            medicoModificada.BAN_CODIGO = 10;
            if (ckbDepurado.Checked)
            {
                medicoModificada.MED_CODIGO_LIBRO = "1";
            }
            else
            {
                medicoModificada.MED_CODIGO_LIBRO = "0";
            }
            return true;
        }


        private void guardarMedicos()
        {
            try
            {
                DialogResult resultado;
                gridMedicos.Focus();
                resultado = MessageBox.Show("Desea guardar los Datos?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    System.DateTime fecha = System.DateTime.Now;
                    int usua = Sesion.codUsuario;
                    NegMedicos.AuditaMedico(medicoOriginal.MED_CODIGO, usua, fecha);
                    USUARIOS consultaususario = NegUsuarios.RecuperaUsuarios().Where(p => p.ID_USUARIO == Sesion.codUsuario).
                            FirstOrDefault();

                    if (consultaususario == null || medicoModificada.ID_USUARIO == 0)
                    {

                        if (!MedicoGuarda())
                            return;
                        medicoModificada.ID_USUARIO = 0;
                        Int16 codigo = NegUsuarios.RecuperaMaximoUsuario();
                        codigo++;
                        DatosUsuario.ID_USUARIO = codigo;
                        DatosUsuario.NOMBRES = txt_Nombre1.Text;//medicoModificada.MED_NOMBRE1.Trim();
                        DatosUsuario.APELLIDOS = txt_Apellido1.Text;//medicoModificada.MED_APELLIDO_PATERNO.Trim();

                        DEPARTAMENTOS depModificado = departamento.Where(dep => dep.DEP_CODIGO == 5).FirstOrDefault();
                        DatosUsuario.DEPARTAMENTOSReference.EntityKey = depModificado.EntityKey;
                        DatosUsuario.NOMBRES = txt_Nombre1.Text;//medicoModificada.MED_NOMBRE1.Trim();
                        DatosUsuario.APELLIDOS = txt_Apellido1.Text; //medicoModificada.MED_APELLIDO_PATERNO.Trim();
                        DatosUsuario.IDENTIFICACION = txt_medruc.Text;//medicoModificada.MED_RUC.Trim();
                        DatosUsuario.FECHA_INGRESO = Convert.ToDateTime(txt_ingreso.Text);//medicoModificada.MED_FECHA;
                        DatosUsuario.FECHA_VENCIMIENTO = Convert.ToDateTime(txt_ingreso.Text); //medicoModificada.MED_FECHA;
                        DatosUsuario.DIRECCION = txt_direccion.Text;// medicoModificada.MED_DIRECCION.Trim();
                        DatosUsuario.ESTADO = true; //medicoModificada.MED_ESTADO;
                        DatosUsuario.USR = txt_Nombre1.Text + txt_Apellido1.Text;
                        DatosUsuario.PWD = txt_medruc.Text;//medicoModificada.MED_RUC;
                        DatosUsuario.LOGEADO = false;
                        //NegUsuarios.CrearUsuario(DatosUsuario);  NO DESCOMENTAR YA QUE EXISTE MODULO DE CREACION DE USUARIOS 20220208 1605
                        medicoModificada.ID_USUARIO = DatosUsuario.ID_USUARIO;

                    }
                    if (rbn_h.Checked == true)
                        medicoModificada.MED_GENERO = "H";
                    else
                        medicoModificada.MED_GENERO = "M";
                    if (rbn_ahorros.Checked == true)
                        medicoModificada.MED_TIPO_CUENTA = "A";
                    else
                        medicoModificada.MED_TIPO_CUENTA = "C";

                    medModificada.MED_CODIGO = medicoModificada.MED_CODIGO;
                    ESPECIALIDADES_MEDICAS espModificado = cmb_especialidad.SelectedItem as ESPECIALIDADES_MEDICAS;
                    medModificada.ESPECIALIDADES_MEDICASReference.EntityKey = espModificado.EntityKey;
                    //Se quema el valor porque ya no se va manejar la retencion desde este formulario. 
                    RETENCIONES_FUENTE retModificado =
                        retencionesfuente.Where(ret => ret.RET_CODIGO == Convert.ToInt32(cmb_retencionfuetne.SelectedValue.ToString())).FirstOrDefault();
                    medModificada.RETENCIONES_FUENTEReference.EntityKey = retModificado.EntityKey;
                    //BANCOS bancoModificado = cmb_bancos.SelectedItem as BANCOS;
                    //medModificada.BANCOSReference.EntityKey = bancoModificado.EntityKey;
                    TIPO_MEDICO tipomModificado = cmb_tipoMedico.SelectedItem as TIPO_MEDICO;
                    medModificada.TIPO_MEDICOReference.EntityKey = tipomModificado.EntityKey;
                    //USUARIOS usuModificado = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == medicoModificada.ID_USUARIO).FirstOrDefault();
                    //medModificada.USUARIOSReference.EntityKey = usuModificado.EntityKey;
                    ESTADO_CIVIL estcivilModificado = cmb_estadocivil.SelectedItem as ESTADO_CIVIL;
                    medModificada.ESTADO_CIVILReference.EntityKey = estcivilModificado.EntityKey;
                    TIPO_HONORARIO tipohonorarioModificado = cmb_tipohonorario.SelectedItem as TIPO_HONORARIO;
                    medModificada.TIPO_HONORARIOReference.EntityKey = tipohonorarioModificado.EntityKey;
                    medModificada.MED_NOMBRE1 = medicoModificada.MED_NOMBRE1.Trim();
                    if (medicoModificada.MED_NOMBRE2 == null)
                    {
                        medModificada.MED_NOMBRE2 = "";
                    }
                    else
                        medModificada.MED_NOMBRE2 = medicoModificada.MED_NOMBRE2.Trim();
                    medModificada.MED_APELLIDO_PATERNO = medicoModificada.MED_APELLIDO_PATERNO.Trim();
                    if (medicoModificada.MED_APELLIDO_MATERNO == null)
                        medModificada.MED_APELLIDO_MATERNO = "";
                    else
                        medModificada.MED_APELLIDO_MATERNO = medicoModificada.MED_APELLIDO_MATERNO.Trim();
                    medModificada.MED_FECHA = medicoModificada.MED_FECHA;
                    medModificada.MED_FECHA_NACIMIENTO = medicoModificada.MED_FECHA_NACIMIENTO;
                    if (medModificada.MED_FECHA_NACIMIENTO == null || medModificada.MED_FECHA_NACIMIENTO.Equals(Convert.ToDateTime("01/01/0001 00:00:00")))
                        medModificada.MED_FECHA_NACIMIENTO = Convert.ToDateTime("12/12/1970 00:00:00");
                    medModificada.MED_DIRECCION = medicoModificada.MED_DIRECCION.Trim();
                    medModificada.MED_DIRECCION_CONSULTORIO = medicoModificada.MED_DIRECCION_CONSULTORIO;
                    medModificada.MED_RUC = medicoModificada.MED_RUC;
                    medModificada.MED_GENERO = medicoModificada.MED_GENERO;
                    medModificada.MED_EMAIL = medicoModificada.MED_EMAIL.Trim();
                    if (txt_medcuenta.Text.Trim() == string.Empty)
                        medModificada.MED_NUM_CUENTA = "0000";
                    else
                        medModificada.MED_NUM_CUENTA = medicoModificada.MED_NUM_CUENTA;
                    medModificada.MED_TIPO_CUENTA = medicoModificada.MED_TIPO_CUENTA;
                    if (txt_cuentacontable.Text.Trim() == string.Empty)
                        medModificada.MED_CUENTA_CONTABLE = "000000000";
                    else
                        medModificada.MED_CUENTA_CONTABLE = NegMedicos.cuentaContable();
                    medModificada.MED_TELEFONO_CASA = medicoModificada.MED_TELEFONO_CASA;
                    medModificada.MED_TELEFONO_CONSULTORIO = medicoModificada.MED_TELEFONO_CONSULTORIO;
                    medModificada.MED_TELEFONO_CELULAR = medicoModificada.MED_TELEFONO_CELULAR;
                    //medModificada.MED_CODIGO_MEDICO = medicoModificada.MED_CODIGO_MEDICO;
                    medModificada.MED_CODIGO_MEDICO = NegMedicos.MED_CODIGO_MEDICO_CG(medicoModificada.MED_RUC.ToString());
                    if (ckbDepurado.Checked)
                        medModificada.MED_CODIGO_LIBRO = "1";
                    else
                        medModificada.MED_CODIGO_LIBRO = "0";
                    medModificada.MED_CODIGO_FOLIO = medicoModificada.MED_CODIGO_FOLIO;

                    if (txt_medautorizaret.Text.Trim() == string.Empty)
                        medModificada.MED_AUTORIZACION_SRI = "000000";
                    else
                        medModificada.MED_AUTORIZACION_SRI = medicoModificada.MED_AUTORIZACION_SRI;
                    if (txt_ValidezAutoriz.Text.Trim() == string.Empty)
                        medModificada.MED_VALIDEZ_AUTORIZACION = "00000000";
                    else
                        medModificada.MED_VALIDEZ_AUTORIZACION = medicoModificada.MED_VALIDEZ_AUTORIZACION;
                    if (txt_facini.Text.Trim() == string.Empty)
                        medModificada.MED_FACTURA_INICIAL = "0000011111";
                    else
                        medModificada.MED_FACTURA_INICIAL = medicoModificada.MED_FACTURA_INICIAL != null ? medicoModificada.MED_FACTURA_INICIAL.Replace("-", "") : null;
                    if (txt_facfin.Text.Trim() == string.Empty)
                        medModificada.MED_FACTURA_FINAL = "0000011111";
                    else
                        medModificada.MED_FACTURA_FINAL = medicoModificada.MED_FACTURA_FINAL != null ? medicoModificada.MED_FACTURA_FINAL.Replace("-", "") : null;

                    medModificada.MED_CON_TRANSFERENCIA = ckbAporte.Checked;
                    medModificada.MED_RECIBE_LLAMADA = chk_recibellamadas.Checked;
                    medModificada.MED_FECHA_MODIFICACION = DateTime.Now;
                    medModificada.MED_ESTADO = medicoModificada.MED_ESTADO;
                    string xCodMed; bool xNuevo = true;
                    if (txt_medcodigo.Text == "0")
                    {
                        if (NegNumeroControl.NumerodeControlAutomatico(2))
                        {
                            medModificada.MED_CODIGO = NegNumeroControl.NumeroControlOptine(2); // int.Parse((NegMedicos.RecuperaMaximoMedicos() + 1).ToString());
                            //NegMedicos.CrearMedico(medModificada); //ENTITY DEJO DE GRABAR SE CREO CON SP
                            NegMedicos.NewMedico(medModificada, Convert.ToInt32(cmb_especialidad.SelectedValue.ToString()), Convert.ToInt32(cmb_tipoMedico.SelectedValue.ToString()), Convert.ToInt32(cmb_tipohonorario.SelectedValue.ToString()), Convert.ToInt32(cmb_retencionfuetne.SelectedValue.ToString()));
                            xCodMed = medModificada.MED_CODIGO.ToString();//alex2121
                            NegNumeroControl.LiberaNumeroControl(2);
                        }
                        else
                        {
                            DataTable MedicoCodigo = new DataTable();
                            MedicoCodigo = NegEspecialidades.RecuperaMaximoMedico();
                            txt_medcodigo.Text = (Convert.ToInt64(MedicoCodigo.Rows[0][0].ToString()) + 1).ToString();
                            medModificada.MED_CODIGO = Convert.ToInt16(txt_medcodigo.Text);
                            xCodMed = medModificada.MED_CODIGO.ToString();//alex2121
                            //medModificada.MED_DIRECCION_CONSULTORIO = txt_direccion.Text;
                            //medModificada.MED_CUENTA_CONTABLE
                            //NegMedicos.CrearMedico(medModificada); //ENTITY DEJO DE GRABAR SE CREO CON SP
                            NegMedicos.NewMedico(medModificada, Convert.ToInt32(cmb_especialidad.SelectedValue.ToString()), Convert.ToInt32(cmb_tipoMedico.SelectedValue.ToString()), Convert.ToInt32(cmb_tipohonorario.SelectedValue.ToString()), Convert.ToInt32(cmb_retencionfuetne.SelectedValue.ToString()));
                        }
                    }
                    else
                    {
                        xNuevo = false;
                        xCodMed = txt_medcodigo.Text.ToString();//alex2121
                        if (medicoModificada.MED_ESTADO != medicoOriginal.MED_ESTADO)
                            medModificada.MED_FECHA_MODIFICACION = DateTime.Now;
                        medModificada.EntityKey = new EntityKey(medico.First().ENTITYSETNAME, medico.First().ENTITYID, medicoModificada.MED_CODIGO);
                        medOrigen.MED_CODIGO = medicoOriginal.MED_CODIGO;
                        ESPECIALIDADES_MEDICAS espOriginal = especialidades.Where(dep => dep.ESP_CODIGO == Convert.ToInt16(cmb_especialidad.SelectedValue.ToString())).FirstOrDefault();
                        medOrigen.ESPECIALIDADES_MEDICASReference.EntityKey = espOriginal.EntityKey;
                        //RETENCIONES_FUENTE retOriginal = retencionesfuente.Where(ret => ret.RET_CODIGO == medicoOriginal.RET_CODIGO).FirstOrDefault();
                        //medOrigen.RETENCIONES_FUENTEReference.EntityKey = retOriginal.EntityKey;
                        //BANCOS banOriginal = bancos.Where(ban => ban.BAN_CODIGO == medicoOriginal.BAN_CODIGO).FirstOrDefault();
                        //medOrigen.BANCOSReference.EntityKey = banOriginal.EntityKey;
                        TIPO_MEDICO tipomOriginal = tipomedico.Where(tip => tip.TIM_CODIGO == Convert.ToInt16(cmb_tipoMedico.SelectedValue.ToString())).FirstOrDefault();
                        medOrigen.TIPO_MEDICOReference.EntityKey = tipomOriginal.EntityKey;
                        //USUARIOS usuOriginal = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == Sesion.codUsuario).FirstOrDefault();
                        //medOrigen.USUARIOSReference.EntityKey = usuOriginal.EntityKey;
                        //ESTADO_CIVIL estcivilOriginal = NegEstadoCivil.RecuperaEstadoCivil().Where(esc => esc.ESC_CODIGO == medicoOriginal.ESC_CODIGO).FirstOrDefault();// se cambia para que se edite el estado civil
                        //medOrigen.ESTADO_CIVILReference.EntityKey = estcivilOriginal.EntityKey;//se debe poder editar el esatdo civil // 
                        ESTADO_CIVIL estcivilModificado1 = cmb_estadocivil.SelectedItem as ESTADO_CIVIL;
                        medOrigen.ESTADO_CIVILReference.EntityKey = estcivilModificado1.EntityKey;
                        TIPO_HONORARIO tipohonorarioOriginal = NegTipoHonorario.RecuperaTipoHonorarios().Where(tip => tip.TIH_CODIGO == medicoOriginal.TIH_CODIGO).FirstOrDefault();
                        medOrigen.TIPO_HONORARIOReference.EntityKey = tipohonorarioOriginal.EntityKey;
                        medOrigen.TIPO_HONORARIOReference.EntityKey = tipohonorarioOriginal.EntityKey;
                        medOrigen.MED_NOMBRE1 = medicoOriginal.MED_NOMBRE1.Trim();
                        medOrigen.MED_NOMBRE2 = medicoOriginal.MED_NOMBRE2.Trim();
                        medOrigen.MED_APELLIDO_PATERNO = medicoOriginal.MED_APELLIDO_PATERNO;
                        medOrigen.MED_APELLIDO_MATERNO = medicoOriginal.MED_APELLIDO_MATERNO;
                        medOrigen.MED_FECHA = medicoOriginal.MED_FECHA;
                        if (medicoOriginal.MED_FECHA_NACIMIENTO == Convert.ToDateTime("12/12/1970"))
                            medOrigen.MED_FECHA_NACIMIENTO = null;
                        else
                            medOrigen.MED_FECHA_NACIMIENTO = medicoOriginal.MED_FECHA_NACIMIENTO;
                        medOrigen.MED_DIRECCION = medicoOriginal.MED_DIRECCION.Trim();
                        medOrigen.MED_DIRECCION_CONSULTORIO = medicoOriginal.MED_DIRECCION_CONSULTORIO;
                        medOrigen.MED_RUC = medicoOriginal.MED_RUC;
                        medOrigen.MED_GENERO = medicoOriginal.MED_GENERO;
                        medOrigen.MED_EMAIL = medicoOriginal.MED_EMAIL.Trim();
                        medOrigen.MED_NUM_CUENTA = medicoOriginal.MED_NUM_CUENTA;
                        medOrigen.MED_TIPO_CUENTA = medicoOriginal.MED_TIPO_CUENTA;
                        medOrigen.MED_CUENTA_CONTABLE = medicoOriginal.MED_CUENTA_CONTABLE;
                        medOrigen.MED_TELEFONO_CASA = medicoOriginal.MED_TELEFONO_CASA;
                        medOrigen.MED_TELEFONO_CONSULTORIO = medicoOriginal.MED_TELEFONO_CONSULTORIO;
                        medOrigen.MED_TELEFONO_CELULAR = medicoOriginal.MED_TELEFONO_CELULAR;
                        medOrigen.MED_AUTORIZACION_SRI = medicoOriginal.MED_AUTORIZACION_SRI;
                        medOrigen.MED_VALIDEZ_AUTORIZACION = medicoOriginal.MED_VALIDEZ_AUTORIZACION;
                        medOrigen.MED_FACTURA_INICIAL = medicoOriginal.MED_FACTURA_INICIAL;
                        medOrigen.MED_FACTURA_FINAL = medicoOriginal.MED_FACTURA_FINAL;
                        medOrigen.MED_ESTADO = medicoOriginal.MED_ESTADO;
                        medOrigen.MED_FECHA_MODIFICACION = medicoOriginal.MED_FECHA_MODIFICACION;
                        medOrigen.MED_CON_TRANSFERENCIA = ckbAporte.Checked;
                        medOrigen.MED_RECIBE_LLAMADA = medicoOriginal.MED_RECIBE_LLAMADA;
                        medOrigen.MED_CODIGO_MEDICO = medicoOriginal.MED_CODIGO_MEDICO;
                        medOrigen.EntityKey = new EntityKey(medico.First().ENTITYSETNAME, medico.First().ENTITYID, medicoOriginal.MED_CODIGO);

                        NegMedicos.GrabarMedico(medModificada, medOrigen, Convert.ToInt32(cmb_especialidad.SelectedValue.ToString()), Convert.ToInt32(cmb_tipoMedico.SelectedValue.ToString()), Convert.ToInt32(cmb_tipohonorario.SelectedValue.ToString()));
                        NegMedicos.ActualizarCampos(medOrigen.MED_CODIGO, ckbAporte.Checked, Convert.ToInt32(cmb_retencionfuetne.SelectedValue.ToString()));
                        acccion = true;
                    }
                    NegMedicos.saveMedicoVendedor(xCodMed, txtCodVendedor.Text.ToString(), xNuevo);//alex2121

                    ResetearControles();
                    HalitarControles(false, false, false, false, false, false, true, true, false);
                    RecuperaMedicos();
                    MessageBox.Show("Datos Almacenados Correctamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (err.InnerException != null)
                    MessageBox.Show(err.InnerException.Message);
            }
        }

        #endregion

        private void btn_llamaUsuarios_Click(object sender, EventArgs e)
        {

            //His.Mantenimiento.frm_Usuario frm = new His.Mantenimiento.frm_Usuario();
            //frm.Show();
        }

        private void txt_medtelconsultorio_Leave(object sender, EventArgs e)
        {
            if (txt_medtelconsultorio.Text != string.Empty && txt_medtelconsultorio.Text.Length < 9)
            {
                MessageBox.Show("Ingrese los 9 digitos del número telefónico", "Medicos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_medtelconsultorio.Focus();
            }
        }

        private void txt_medtelcelular_Leave(object sender, EventArgs e)
        {
            if (txt_medtelcelular.Text != string.Empty && txt_medtelcelular.Text.Length < 10)
            {
                MessageBox.Show("Ingrese los 10 digitos del número telefónico", "Medicos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_medtelcelular.Focus();
            }
        }

        private void txt_medautorizaret_Leave(object sender, EventArgs e)
        {
            if (txt_medautorizaret.Text != string.Empty && txt_medautorizaret.Text.Length < 10)
            {
                MessageBox.Show("Ingrese los 10 digitos del número de Autorización.", "Medicos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_medautorizaret.Focus();
            }
        }

        private void rbn_m_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void timerCapturaTeclas_Tick(object sender, EventArgs e)
        {
            //desactivo el control, al terminarse el intervalo
            timerCapturaTeclas.Enabled = false;
        }

        private void txt_direccionconsultorio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                cmb_especialidad.Focus();
            }
        }

        private void chk_recibellamadas_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chk_activo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                cmb_tipoMedico.Focus();
            }
        }

        private void ultraTabControl1_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            //int usus = Sesion.codUsuario;
            //if(usus==2526 )
            //{
            //    if(ultraTabControl1.SelectedTab =true)
            //    {

        }

        private void ultraGrid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            try
            {
                if (gridMedicos.ActiveRow.Index > -1)
                {
                    btn_AyudaMedicos.Enabled = false;
                    HalitarControles(datgenerales, dattransfer, datsri, creaModifica, creaModifica, creaModifica, false, true, true);
                    //medicoModificada = gridMedicos.CurrentRow.DataBoundItem as DtoMedicos;

                    int codMedico = (Int32)gridMedicos.ActiveRow.Cells["CODIGO"].Value;
                    medicoModificada = medico.FirstOrDefault(m => m.MED_CODIGO == codMedico);
                    //medicoModificada = NegMedicos.RecuperaDtoMedicoFormulario(codMedico);
                    medicoOriginal = medicoModificada.ClonarEntidad();
                    AgregarBindigControles();
                    DataTable Recuperar = NegMedicos.RecuperarNuevosCampos(codMedico);
                    ckbAporte.Checked = Convert.ToBoolean(Recuperar.Rows[0]["MED_CON_TRANSFERENCIA"].ToString());
                    if (Recuperar.Rows[0]["MED_CODIGO_LIBRO"].ToString() == "1")
                        ckbDepurado.Checked = true;
                    else
                        ckbDepurado.Checked = false;
                    string vendedor = NegMedicos.getMedicoVendedor(txt_medcodigo.Text);
                    if (vendedor == "0")
                    {
                        txtVendedor.Text = string.Empty;
                        txtCodVendedor.Text = string.Empty;
                    }
                    else
                    {
                        string[] words = vendedor.Split('_');
                        txtCodVendedor.Text = words[0];
                        txtVendedor.Text = words[1];
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
                //MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void gridMedicos_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            if (inicio == true)
            {
                UltraGridBand bandUno = gridMedicos.DisplayLayout.Bands[0];

                gridMedicos.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                gridMedicos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                gridMedicos.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                gridMedicos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                //bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                //bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                gridMedicos.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                gridMedicos.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
                gridMedicos.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                gridMedicos.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                gridMedicos.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                gridMedicos.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                gridMedicos.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                gridMedicos.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                gridMedicos.DisplayLayout.UseFixedHeaders = true;

                //bandUno.Columns["CODIGO"].MaxWidth = 50;
                //bandUno.Columns["CODIGO"].MinWidth = 50;

                bandUno.Columns["CODIGO"].Hidden = true; //MED_CON_TRANSFERENCIA
                bandUno.Columns["MED_CON_TRANSFERENCIA"].Hidden = true;
                bandUno.Columns["MEDICO"].MaxWidth = 250;
                bandUno.Columns["MEDICO"].MinWidth = 250;

                bandUno.Columns["ESPECIALIDAD"].MaxWidth = 150;
                bandUno.Columns["ESPECIALIDAD"].MinWidth = 150;

                //bandUno.Columns["ACTIVO"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
                //bandUno.Columns["DEPURADO"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;

                inicio = false;
            }
        }

        private void gridMedicos_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (gridMedicos.ActiveRow.Index > -1)
                    {
                        HalitarControles(datgenerales, dattransfer, datsri, creaModifica, creaModifica, creaModifica, false, true, true);
                        //medicoModificada = gridMedicos.CurrentRow.DataBoundItem as DtoMedicos;

                        int codMedico = (Int32)gridMedicos.ActiveRow.Cells["CODIGO"].Value;
                        medicoModificada = medico.FirstOrDefault(m => m.MED_CODIGO == codMedico);
                        //medicoModificada = NegMedicos.RecuperaDtoMedicoFormulario(codMedico);
                        medicoOriginal = medicoModificada.ClonarEntidad();
                        AgregarBindigControles();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Busca el directorio donde se guarda el archivo de excel
        /// </summary>
        /// <returns>retorna el directorio</returns>
        private String FindSavePath()
        {
            Stream myStream;
            string myFilepath = null;
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xls)|*.xls";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        myFilepath = saveFileDialog1.FileName;
                        myStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myFilepath;
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            gridx.DataSource = NegMedicos.getMedicosGrid();

            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    this.ultraGridExcelExporter1.Export(gridMedicos, PathExcel);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
        }

        private void cmb_bancos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_medcodigo_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnAddVendedor_Click(object sender, EventArgs e)
        {
            frm_ImagenAyuda ayuda = new frm_ImagenAyuda("VENDEDORES");
            ayuda.ShowDialog();


            if (ayuda.codigo != string.Empty)
            {
                txtVendedor.Text = ayuda.producto;
                txtCodVendedor.Text = ayuda.codigo;
            }
        }

        private void txtVendedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnAddVendedor.Visible)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    txtVendedor.Text = string.Empty;
                    txtCodVendedor.Text = string.Empty;
                    e.Handled = true;
                }
            }

        }

        private void txt_medruc_Leave_1(object sender, EventArgs e)
        {
            controlErrores.Clear();
            if (optRUC.Checked == true)
            {
                if (txt_medruc.Text.ToString() != string.Empty && txt_medruc.Text.Trim().Length == 13)
                {
                    try
                    {
                        //if (NegValidaciones.esCedulaValida(txt_medruc.Text) != true)
                        //{

                        //    MessageBox.Show("Cédula Inválida", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    txt_medruc.Focus();
                        //}
                        //if (!NegValidaciones.ValidarRuc(txt_medruc.Text.Trim()))
                        //{
                        //    controlErrores.SetError(txt_medruc, "Ruc no válido");
                        //    txt_medruc.Focus();
                        //}
                        if (NegMedicos.existeMedico(txt_medruc.Text.Trim()))
                        {
                            MessageBox.Show("El medico ya ha sido registrado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            ResetearControles();
                            HalitarControles(false, false, false, false, false, false, true, true, false);
                            RecuperaMedicos();
                        }
                    }
                    catch { }
                }
                else
                {
                    controlErrores.SetError(txt_medruc, "Ruc no válido");
                    txt_medruc.Focus();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmb_tipoMedico_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_direccionconsultorio_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_CodigoLibro_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_CodigoFolio.Focus();
            }
        }

        private void txt_CodigoFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_codigoMedico.Focus();
            }

        }

        private void txt_direccion_TextChanged(object sender, EventArgs e)
        {


        }

        private void gridx_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }

        private void btn_AyudaMedicos_Click(object sender, EventArgs e)
        {
            frm_Ayudas lista = new frm_Ayudas(NegMedicos.List_MEDICO_CG());
            lista.ShowDialog();
            if (lista.campoPadre.Text != string.Empty)
            {
                string codMedicoCg = lista.campoPadre.Text;
                DtoMedicoCuentaContable medico = NegMedicos.MEDICO_CG(codMedicoCg);
                txt_Nombre1.Text = medico.MED_NOMBRE1;
                txt_Nombre2.Text = medico.MED_NOMBRE2;
                txt_Apellido1.Text = medico.MED_APELLIDOS_PATERNO;
                txt_Apellido2.Text = medico.MED_APELLIDOS_MATERNO;
                txt_direccion.Text = medico.MED_DIRECCION;
                txt_medruc.Text = medico.MED_RUC;
                txt_medtelcasa.Text = medico.MED_TELEFONO;

                medicoModificada.MED_APELLIDO_PATERNO = medico.MED_APELLIDOS_PATERNO + " " + medico.MED_APELLIDOS_MATERNO; ;
                medicoModificada.MED_NOMBRE1 = medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                medicoModificada.MED_DIRECCION = medico.MED_DIRECCION;
                medicoModificada.MED_RUC = medico.MED_RUC;
                AgregarBindigControles();
            }
        }


        //private void gridMedicos_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar != (char)(Keys.Enter))
        //    {
        //        List<DataGridViewRow> m_listaRows = new List<DataGridViewRow>();
        //        IEnumerable<DataGridViewRow> query  = (from item in gridMedicos.Rows.Cast<DataGridViewRow>()
        //                                          where item.Cells[fieldName].Value != DBNull.Value &&
        //                                           item.Cells[fieldName].Value.ToString().Contains(gridMedicos.Rows[gridMedicos.CurrentRow.Index].Cells[gridMedicos.CurrentCell.ColumnIndex].Value.ToString())
        //                                          select item).ToList<DataGridViewRow>();
        //        // devolvemos la consulta linq ejecutada
        //        m_listaRows= query.ToList();

        //        if (m_listaRows != null)
        //        {
        //            DataGridViewRow row = m_listaRows.ElementAtOrDefault(position);
        //            if (row !=null)
        //            {
        //                dgv.CurrentCell= dgv.Rows[row.Index].Cells[0];
        //                dgv.FirstDisplayedCell= dgv.CurrentCell;

        //            }



        //        }
        //        //if (Char.IsLetterOrDigit(e.KeyChar))
        //        //{
        //        //    List<DataGridViewRow> rows = (from item in gridMedicos.Rows.Cast<DataGridViewRow>()
        //        //                                  let MED_CODIGO = Convert.ToString(item.Cells["MED_CODIGO"].Value ?? string.Empty)
        //        //                                  let MED_NOMBRE1 = Convert.ToString(item.Cells["MED_NOMBRE1"].Value ?? string.Empty)
        //        //                                  let MED_NOMBRE2 = Convert.ToString(item.Cells["MED_NOMBRE2"].Value ?? string.Empty)
        //        //                                  let MED_APELLIDO_PATERNO = Convert.ToString(item.Cells["MED_APELLIDO_PATERNO"].Value?? string.Empty)
        //        //                                  where MED_CODIGO.Contains(e.KeyChar.ToString()) ||
        //        //                                         MED_NOMBRE1.Contains(e.KeyChar.ToString()) ||
        //        //                                         MED_NOMBRE2.Contains(e.KeyChar.ToString()) ||
        //        //                                         MED_APELLIDO_PATERNO.Contains(e.KeyChar.ToString())
        //        //                                  select item).ToList<DataGridViewRow>();

        //        //    foreach (DataGridViewRow row in rows)
        //        //    {
        //        //        List<DataGridViewCell> cells = (from item in row.Cells.Cast<DataGridViewCell>()
        //        //                                        let cell = Convert.ToString(item.Value)
        //        //                                        where cell.Contains(e.KeyChar.ToString())
        //        //                                        select item).ToList<DataGridViewCell>();

        //        //        foreach (DataGridViewCell item in cells)
        //        //        {
        //        //            item.Selected = true;
        //        //        }

        //        //    }

        //            //for (int i = 0; i <= gridMedicos.RowCount - 1; i++)
        //            //{
        //            //    if (gridMedicos.Rows[i].Cells[gridMedicos.CurrentCell.ColumnIndex].Value.ToString().Contains(e.KeyChar.ToString().ToUpper()))
        //            //    {
        //            //        gridMedicos.CurrentCell = gridMedicos.Rows[i].Cells[columnabuscada];
        //            //        break;
        //            //    }
        //            //    // gridMedicos.Rows[i].Cells[columnabuscada].Selected = true;
        //            //}

        //        }
        //    }
        //}


    }
}
