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
using Core.Entidades;
using Infragistics.Win.UltraWinGrid;
using System.Threading;

namespace His.Maintenance
{
    public partial class frm_Usuario : Form
    {
        #region variables
        public DtoUsuarios usuarioOriginal = new DtoUsuarios();
        //public DtoUsuarios usuarioModificada = new DtoUsuarios();
        public USUARIOS usuOrigen = new USUARIOS();
        public USUARIOS usuModificada = new USUARIOS();
        public USUARIOS usuarioDTs = new USUARIOS();
        public List<DEPARTAMENTOS> departamento = new List<DEPARTAMENTOS>();
        public List<DtoUsuarios> usuario = new List<DtoUsuarios>();
        private int temp;
        private int fila;
        private DataSet mDatos;
        private bool modificaDatos;
        public int columnabuscada;
        private bool inicio = true;
        MEDICOS medico = new MEDICOS();
        Int64 id_usuario;
        #endregion

        #region constructor
        public frm_Usuario()
        {
            InitializeComponent();

        }
        #endregion

        #region eventos
        private void frm_Usuario_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(false, false, false, false, false, true, false, false, false, false);
                habilitarTab(false, false, false);
                RecuperaUsuarios();

                departamento = NegDepartamentos.ListaDepartamentos();
                //carga los departamentos en el combobox
                cmb_departamento.DataSource = departamento;
                cmb_departamento.ValueMember = "DEP_CODIGO";
                cmb_departamento.DisplayMember = "DEP_NOMBRE";
                txt_fecing.Enabled = false;
                
                cmbArea.DataSource = NegUsuarios.cargarAreaAsignada();
                cmbArea.DisplayMember = "AS_DESCRIPCION";
                cmbArea.ValueMember = "AS_ID";
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
                //GrabarDatos();

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
                if (usuarioNuevo == true)
                {
                    //GrabarDatos();
                    //GrabarPerfil();
                    GuardarDatosNuevo();
                    //usuarioNuevo = false;
                }
                else
                {
                    //GrabarPerfil();
                    //GrabarDatos();
                    GuardarDatosNuevo();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult resultado;

                resultado = MessageBox.Show("Desea eliminar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {

                    //usuModificada.ID_USUARIO = usuarioModificada.ID_USUARIO; // se comenta para mejorar el proceso tardaba demasiado// MarioValencia // 22-01-2024
                    //DEPARTAMENTOS depModificado = cmb_departamento.SelectedItem as DEPARTAMENTOS;
                    //usuModificada.DEPARTAMENTOSReference.EntityKey = depModificado.EntityKey;
                    //usuModificada.NOMBRES = usuarioModificada.NOMBRES;
                    //usuModificada.APELLIDOS = usuarioModificada.APELLIDOS;
                    //usuModificada.IDENTIFICACION = usuarioModificada.IDENTIFICACION;
                    //usuModificada.FECHA_INGRESO = usuarioModificada.FECHA_INGRESO;
                    //usuModificada.FECHA_VENCIMIENTO = usuarioModificada.FECHA_VENCIMIENTO;
                    //usuModificada.DIRECCION = usuarioModificada.DIRECCION;
                    //usuModificada.ESTADO = usuarioModificada.ESTADO;
                    //usuModificada.USR = usuarioModificada.USR;
                    //usuModificada.PWD = usuarioModificada.PWD;
                    //usuModificada.LOGEADO = usuarioModificada.LOGEADO;
                    //usuModificada.EntityKey = new EntityKey(usuario.First().ENTITYSETNAME
                    //        , usuario.First().ENTITYID, usuarioModificada.ID_USUARIO);

                    if (NegUsuarios.EliminarUsuario(id_usuario))
                    {
                        RecuperaUsuarios();
                        ResetearControles();

                        HalitarControles(false, false, false, false, false, true, false, false, false, false);
                        MessageBox.Show("Datos Eliminados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Operación Invalida", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public bool usuarioNuevo = false;
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HalitarControles(true, true, false, true, false, false, true, false, false, false);
                usuarioOriginal = new DtoUsuarios();
                //usuarioModificada = new DtoUsuarios();

                ResetearControles();
                limpiar();
                if (NegNumeroControl.NumerodeControlAutomatico(1))
                    //usuarioModificada.ID_USUARIO = Int16.Parse((NegUsuarios.RecuperaMaximoUsuario() + 1).ToString());
                    txt_id.Text = Convert.ToString(Int16.Parse((NegUsuarios.RecuperaMaximoUsuario() + 1).ToString()));
                else
                    txt_id.ReadOnly = false;

                //usuarioModificada.FECHA_INGRESO = DateTime.Now;
                //usuarioModificada.ESTADO = true;
                //AgregarBindigControles();
                txt_fecing.Text = DateTime.Now.ToString("d");
                DateTime oPrimerDiaDelMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1);
                txt_fecven.Text = oUltimoDiaDelMes.ToString();
                txt_nombres.Focus();
                chk_activo.Checked = true;
                usuarioNuevo = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }

        }
        public void limpiar()
        {
            txt_id.DataBindings.Clear();
            cmb_departamento.DataBindings.Clear();
            txt_nombres.DataBindings.Clear();
            txt_apellidos.DataBindings.Clear();
            txt_identificacion.DataBindings.Clear();
            txt_fecing.DataBindings.Clear();
            txt_fecven.DataBindings.Clear();
            txt_direccion.DataBindings.Clear();
            txt_usuario.DataBindings.Clear();
            txt_clave.DataBindings.Clear();
            chk_activo.DataBindings.Clear();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //RecuperaUsuarios();
            HalitarControles(false, false, false, false, false, true, false, false, false, false);
            habilitarTab(false, false, false);
            controlErrores.Clear();
            tabControl1.SelectedTab = tabControl1.Tabs[0];
            SendKeys.SendWait("{TAB}");
            ResetearControles();
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //private void txt_usuario_Leave(object sender, EventArgs e) // se comenta por que no se ocupa // Mario Valencia // 22-01-2023
        //{

        //    if (txt_usuario.Text != string.Empty)
        //    {
        //        //valido si el usr ya existe en otro usuario

        //        List<DtoUsuarios> usr = new List<DtoUsuarios>();
        //        usr = usuario.Where(us => us.USR == txt_usuario.Text).ToList();
        //        if (usr.Count > 0)
        //        {
        //            MessageBox.Show("Usuario ya existente, porfavor cambie en nombre del usuario", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            txt_usuario.Text = "";
        //            txt_usuario.Focus();
        //        }

        //    }
        //}
        public void GrabaPerfilSic()
        {

            //if (txt_codigoUsuario.Text != "")
            //{
            //    NegUsuarios.EliminarPerfilSic(Convert.ToInt32(txt_id.Text));
            //    for (int i = 0; i < grd_PerfilesSic.Rows.Count; i++)
            //    {
            //        if (grd_PerfilesSic.Rows[i].Cells[2].Value.ToString() == true.ToString())
            //        {
            //            NegUsuarios.CrearPerfilSicSP(Convert.ToDouble(txt_id.Text), Convert.ToDouble(grd_PerfilesSic.Rows[i].Cells[0].Value.ToString()), "S", "");
            //        }
            //        else
            //        {
            //            NegUsuarios.CrearPerfilSicSP(Convert.ToDouble(txt_id.Text), Convert.ToDouble(grd_PerfilesSic.Rows[i].Cells[0].Value.ToString()), "N", "");
            //        }
            //    }
            //}

        }
        public void GrabaPerfilCg()
        {
            //if (txt_codigoUsuario.Text != "")
            //{
            //    NegUsuarios.EliminarPerfilCg(Convert.ToInt32(txt_id.Text));
            //    for (int i = 0; i < dtg_PerfilesCg.Rows.Count; i++)
            //    {
            //        if (dtg_PerfilesCg.Rows[i].Cells[2].Value.ToString() == true.ToString())
            //        {
            //            NegUsuarios.CrearPerfilCg(Convert.ToInt32(txt_id.Text), Convert.ToInt32(dtg_PerfilesCg.Rows[i].Cells[0].Value.ToString()), "S", dtg_PerfilesCg.Rows[i].Cells[3].Value.ToString());
            //        }
            //        else
            //        {
            //            NegUsuarios.CrearPerfilCg(Convert.ToInt32(txt_id.Text), Convert.ToInt32(dtg_PerfilesCg.Rows[i].Cells[0].Value.ToString()), "N", dtg_PerfilesCg.Rows[i].Cells[3].Value.ToString());
            //        }
            //    }
            //}
        }

        public void ActualizaMedico()
        {
            if (medico != null)
            {
                NegMedicos.ActualizarRucMedico(medico, txt_codigoUsuario.Text);
            }
        }

        public void GrabarPerfil()
        {
            try
            {
                //DialogResult resultado;
                if (id_usuario != 0)
                {
                    List<USUARIOS_PERFILES> upOriginal = NegUsuarios.ListaUsuarioPerfiles().Where(p => p.ID_USUARIO == Int16.Parse(txt_id.Text)).ToList().ClonarEntidad();
                    List<USUARIOS_PERFILES> upModificada = new List<USUARIOS_PERFILES>();
                    USUARIOS_PERFILES upNuevo = new USUARIOS_PERFILES();

                    NegUsuarios.EliminaUsuarioPerfiles(upModificada, upOriginal);


                    for (int i = 0; i <= grid_perfiles.Rows.Count - 1; i++)
                    {
                        upNuevo = new USUARIOS_PERFILES();
                        if (grid_perfiles.Rows[i].Cells[2].Value.ToString() == true.ToString())
                        {
                            USUARIOS nusuario = NegUsuarios.RecuperaUsuarios().Where(p => p.ID_USUARIO == Int16.Parse(txt_id.Text)).FirstOrDefault();
                            upNuevo.USUARIOSReference.EntityKey = nusuario.EntityKey;
                            upNuevo.ID_USUARIO = Int16.Parse(txt_id.Text);
                            PERFILES nperfil = NegPerfil.RecuperaPerfiles().Where(a => a.ID_PERFIL == Int16.Parse(grid_perfiles.Rows[i].Cells[0].Value.ToString())).FirstOrDefault();
                            upNuevo.PERFILESReference.EntityKey = nperfil.EntityKey;
                            upNuevo.ID_PERFIL = Int16.Parse(grid_perfiles.Rows[i].Cells[0].Value.ToString());

                            NegUsuarios.CrearUsuarioPerfiles(upNuevo);

                        }
                    }
                    //resultado = MessageBox.Show("Desea guardar los Perfil para el Usuario?", "Perfil del Usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (resultado == DialogResult.Yes)
                    //{

                    //    MessageBox.Show("Datos Almacenados Correctamente");

                    //}
                }
                //else // se comenta se cambia el proceso// MarioValencia // 22-01-2024
                //{
                //    if (usuarioModificada.ID_USUARIO != 0)
                //    {
                //        List<USUARIOS_PERFILES> upOriginal = NegUsuarios.ListaUsuarioPerfiles().Where(p => p.ID_USUARIO == Int16.Parse(txt_id.Text)).ToList().ClonarEntidad();
                //        List<USUARIOS_PERFILES> upModificada = new List<USUARIOS_PERFILES>();
                //        USUARIOS_PERFILES upNuevo = new USUARIOS_PERFILES();

                //        NegUsuarios.EliminaUsuarioPerfiles(upModificada, upOriginal);

                //        ActualizaMedico();

                //        for (int i = 0; i <= grid_perfiles.Rows.Count - 1; i++)
                //        {
                //            upNuevo = new USUARIOS_PERFILES();
                //            if (grid_perfiles.Rows[i].Cells[2].Value.ToString() == true.ToString())
                //            {
                //                USUARIOS nusuario = NegUsuarios.RecuperaUsuarios().Where(p => p.ID_USUARIO == Int16.Parse(txt_id.Text)).FirstOrDefault();
                //                upNuevo.USUARIOSReference.EntityKey = nusuario.EntityKey;
                //                upNuevo.ID_USUARIO = Int16.Parse(txt_id.Text);
                //                PERFILES nperfil = NegPerfil.RecuperaPerfiles().Where(a => a.ID_PERFIL == Int16.Parse(grid_perfiles.Rows[i].Cells[0].Value.ToString())).FirstOrDefault();
                //                upNuevo.PERFILESReference.EntityKey = nperfil.EntityKey;
                //                upNuevo.ID_PERFIL = Int16.Parse(grid_perfiles.Rows[i].Cells[0].Value.ToString());

                //                NegUsuarios.CrearUsuarioPerfiles(upNuevo);

                //            }
                //        }
                //    }
                //    else
                //        MessageBox.Show("Antes de Grabar el perfil del Usuario, ud debe Grabar el Usuario");
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btn_grabaperfil_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult resultado;
                if (usuarioOriginal.ID_USUARIO != 0)
                {
                    resultado = MessageBox.Show("Desea guardar los Perfil para el Usuario?", "Perfil del Usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        List<USUARIOS_PERFILES> upOriginal = NegUsuarios.ListaUsuarioPerfiles().Where(p => p.ID_USUARIO == Int16.Parse(txt_id.Text)).ToList().ClonarEntidad();
                        List<USUARIOS_PERFILES> upModificada = new List<USUARIOS_PERFILES>();
                        USUARIOS_PERFILES upNuevo = new USUARIOS_PERFILES();

                        NegUsuarios.EliminaUsuarioPerfiles(upModificada, upOriginal);


                        for (int i = 0; i <= grid_perfiles.Rows.Count - 1; i++)
                        {
                            upNuevo = new USUARIOS_PERFILES();
                            if (grid_perfiles.Rows[i].Cells[2].Value.ToString() == true.ToString())
                            {
                                USUARIOS nusuario = NegUsuarios.RecuperaUsuarios().Where(p => p.ID_USUARIO == Int16.Parse(txt_id.Text)).FirstOrDefault();
                                upNuevo.USUARIOSReference.EntityKey = nusuario.EntityKey;
                                upNuevo.ID_USUARIO = Int16.Parse(txt_id.Text);
                                PERFILES nperfil = NegPerfil.RecuperaPerfiles().Where(a => a.ID_PERFIL == Int16.Parse(grid_perfiles.Rows[i].Cells[0].Value.ToString())).FirstOrDefault();
                                upNuevo.PERFILESReference.EntityKey = nperfil.EntityKey;
                                upNuevo.ID_PERFIL = Int16.Parse(grid_perfiles.Rows[i].Cells[0].Value.ToString());

                                NegUsuarios.CrearUsuarioPerfiles(upNuevo);

                            }
                        }
                        MessageBox.Show("Datos Almacenados Correctamente");

                    }

                }
                else
                {
                    MessageBox.Show("Antes de Grabar el perfil del Usuario, ud debe Grabar el Usuario");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
            //DialogResult resultado;

            //if (txt_id.Text != string.Empty)
            //{
            //    resultado = MessageBox.Show("Desea guardar los Perfil para el Usuario?", "Perfiles de Usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (resultado == DialogResult.Yes)
            //    {
            //        List<object> lstb = new List<object>();
            //        lstb.Add(Int16.Parse(txt_id.Text));
            //        //elimina los datos guardados del perfil del usuario


            //        ProxyAcceso.InsertaenTabla("sp_Usuario_perfilesEliminar", lstb);

            //        for (int i = 0; i <= grid_perfiles.RowCount - 2; i++)
            //        {
            //            if (grid_perfiles.Rows[i].Cells[2].Value.ToString() == true.ToString())
            //            {
            //                //guarda los perfiles del usuario
            //                List<object> lst = new List<object>();
            //                lst.Add(Int16.Parse(txt_id.Text));
            //                lst.Add(Int16.Parse(grid_perfiles.Rows[i].Cells[0].Value.ToString()));
            //                lst.Add(i);

            //                ProxyAcceso.InsertaenTabla("sp_Usuario_perfilesInsertar", lst);

            //            }

            //        }
            //    }
            //}
        }
        private void txt_nombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txt_apellidos.Focus();
            }

        }
        private void txt_apellidos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txt_identificacion.Focus();
            }

        }
        private void txt_identificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(Keys.Enter))
                {
                    e.Handled = true;
                    if (NegValidaciones.esCedulaValida(txt_identificacion.Text) == true)
                        if (NegUsuarios.ConsultaUsuario(txt_identificacion.Text) == 0)
                            txt_direccion.Focus();
                        else
                        {
                            MessageBox.Show("Ya se registro a un usuario con este numero de cédula", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //RecuperaUsuarios();
                            HalitarControles(false, false, false, false, false, true, false, false, false, false);
                            ResetearControles();
                        }
                    else
                    {
                        txt_identificacion.Text = "";
                        MessageBox.Show("Cédula Incorrecta", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_direccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                cmb_departamento.Focus();
            }

        }
        private void cmb_departamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txt_usuario.Focus();
            }

        }
        private void txt_usuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                //Valida si el usuario existe dentro de la base de datos.
                e.Handled = true;
                txt_clave.Focus();
            }
        }
        private void txt_clave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txt_conclave.Focus();
            }
        }
        private void txt_conclave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (txt_clave.Text == txt_conclave.Text)
                {
                    e.Handled = true;
                    txt_fecven.Focus();
                }
                else
                    controlErrores.SetError(txt_conclave, "La clave no es igual a la de confirmación");
            }
        }

        #endregion

        #region metodos privados
        private void AgregarBindigControles()
        {
            Binding ID_USUARIO = new Binding("Text", usuarioDTs, "ID_USUARIO", true);
            //Binding DEP_CODIGO = new Binding("SelectedValue", usuarioModificada, "DEP_CODIGO", true);
            Binding NOMBRES = new Binding("Text", usuarioDTs, "NOMBRES", true);
            Binding APELLIDOS = new Binding("Text", usuarioDTs, "APELLIDOS", true);
            Binding IDENTIFICACION = new Binding("Text", usuarioDTs, "IDENTIFICACION", true);
            Binding FECHA_INGRESO = new Binding("Text", usuarioDTs, "FECHA_INGRESO", true);
            Binding FECHA_VENCIMIENTO = new Binding("Text", usuarioDTs, "FECHA_VENCIMIENTO", true);
            //Binding DIRECCION = new Binding("Text", usuarioModificada, "DIRECCION", true);
            Binding ESTADO = new Binding("Checked", usuarioDTs, "ESTADO", true);
            Binding USR = new Binding("Text", usuarioDTs, "USR", true);
            Binding PWD = new Binding("Text", usuarioDTs, "PWD", true);
            Binding PWD1 = new Binding("Text", usuarioDTs, "PWD", true);

            txt_id.DataBindings.Clear();
            cmb_departamento.DataBindings.Clear();
            txt_nombres.DataBindings.Clear();
            txt_apellidos.DataBindings.Clear();
            txt_identificacion.DataBindings.Clear();
            txt_fecing.DataBindings.Clear();
            txt_fecven.DataBindings.Clear();
            txt_direccion.DataBindings.Clear();
            txt_usuario.DataBindings.Clear();
            txt_clave.DataBindings.Clear();
            chk_activo.DataBindings.Clear();
            //txt_conclave.DataBindings.Clear();

            txt_id.DataBindings.Add(ID_USUARIO);
            //cmb_departamento.DataBindings.Add(DEP_CODIGO);
            txt_nombres.DataBindings.Add(NOMBRES);
            txt_apellidos.DataBindings.Add(APELLIDOS);
            txt_identificacion.DataBindings.Add(IDENTIFICACION);
            txt_fecing.DataBindings.Add(FECHA_INGRESO);
            txt_fecven.DataBindings.Add(FECHA_VENCIMIENTO);
            //txt_direccion.DataBindings.Add(DIRECCION);
            chk_activo.DataBindings.Add(ESTADO);
            txt_usuario.DataBindings.Add(USR);
            txt_clave.DataBindings.Add(PWD);

            txt_conclave.Text = txt_clave.Text;
        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            //usuarioModificada = new DtoUsuarios();
            usuarioOriginal = new DtoUsuarios();
            usuModificada = new USUARIOS();
            usuOrigen = new USUARIOS();

            txt_apellidos.Text = string.Empty;
            txt_clave.Text = string.Empty;
            txt_conclave.Text = string.Empty;
            txt_direccion.Text = string.Empty;
            txt_fecing.Text = string.Empty;
            txt_fecven.Text = string.Empty;
            txt_id.Text = "";
            txt_identificacion.Text = string.Empty;
            txt_nombres.Text = string.Empty;
            txt_usuario.Text = string.Empty;
            txt_codigoUsuario.Text = "";
            cmb_departamento.SelectedItem = -1;

        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        private bool validarFormularioTotal()
        {
            bool valida = true;
            //if (txt_id.Text == "")
            //{
            //    AgregarError(txt_id);
            //    valida = false;
            //}
            if (txt_nombres.Text == "")
            {
                AgregarError(txt_nombres);
                valida = false;
            }
            if (txt_apellidos.Text == "")
            {
                AgregarError(txt_apellidos);
                valida = false;
            }
            if (txt_identificacion.Text == "")
            {
                AgregarError(txt_identificacion);
                valida = false;
            }
            if (txt_usuario.Text == "")
            {
                AgregarError(txt_usuario);
                valida = false;
            }
            if (txt_clave.Text == "" || txt_conclave.Text == "")
            {
                AgregarError(txt_clave);
                AgregarError(txt_conclave);
                valida = false;
            }
            //if (txt_fecven.Text != "  /  /")
            //{
            //    if (Convert.ToDateTime(txt_fecing.Text) > Convert.ToDateTime(txt_fecven.Text))
            //    {
            //        controlErrores.SetError(txt_fecing, "Fecha de ingreso no puede ser mayor a la de vencimiento");
            //        valida = false;
            //    }
            //    if (Convert.ToDateTime(txt_fecven.Text) < Convert.ToDateTime(txt_fecing.Text))
            //    {
            //        controlErrores.SetError(txt_fecven, "Fecha de vencimiento no puede ser menor a la de ingreso");
            //        valida = false;
            //    }
            //}
            if ((Convert.ToDateTime(txt_fecing.Text)).Date > DateTime.Now.Date)
            {
                controlErrores.SetError(txt_fecing, "Fecha de ingreso no puede ser mayor a la fecha actual.");
                valida = false;
            }
            if (txt_clave.Text != txt_conclave.Text)
            {
                controlErrores.SetError(txt_conclave, "La clave no es igual a la de confirmación");
                valida = false;
            }
            return valida;
        }
        //private bool ValidarFormulario()// se comenta no se esta utilizando// MarioValencia // 22-01-2024
        //{
        //    bool valido = true;
        //    controlErrores.Clear();
        //    if (usuarioModificada.ID_USUARIO == 0)
        //    {
        //        AgregarError(txt_id);
        //        valido = false;
        //    }
        //    if (usuarioModificada.DEP_CODIGO == null)
        //    {
        //        AgregarError(cmb_departamento);
        //        valido = false;
        //    }
        //    if (usuarioModificada.NOMBRES == null || usuarioModificada.NOMBRES == string.Empty)
        //    {
        //        AgregarError(txt_nombres);
        //        valido = false;
        //    }
        //    if (usuarioModificada.APELLIDOS == null || usuarioModificada.APELLIDOS == string.Empty)
        //    {
        //        AgregarError(txt_apellidos);
        //        valido = false;
        //    }
        //    if (usuarioModificada.IDENTIFICACION == null || usuarioModificada.APELLIDOS == string.Empty)
        //    {
        //        AgregarError(txt_identificacion);
        //        valido = false;
        //    }
        //    if (usuarioModificada.USR == null || usuarioModificada.USR == string.Empty)
        //    {
        //        AgregarError(txt_usuario);
        //        valido = false;
        //    }
        //    if (usuarioModificada.PWD == null || usuarioModificada.PWD == string.Empty)
        //    {
        //        AgregarError(txt_clave);
        //        AgregarError(txt_conclave);
        //        valido = false;
        //    }
        //    if (Convert.ToDateTime(txt_fecing.Text) > Convert.ToDateTime(txt_fecven.Text))
        //    {
        //        controlErrores.SetError(txt_fecing, "Fecha de ingreso no puede ser mayor a la de vencimiento");
        //        valido = false;
        //    }
        //    if (Convert.ToDateTime(txt_fecven.Text) < Convert.ToDateTime(txt_fecing.Text))
        //    {
        //        controlErrores.SetError(txt_fecven, "Fecha de vencimiento no puede ser menor a la de ingreso");
        //        valido = false;
        //    }
        //    if ((Convert.ToDateTime(txt_fecing.Text)).Date > DateTime.Now.Date)
        //    {
        //        controlErrores.SetError(txt_fecing, "Fecha de ingreso no puede ser mayor a la fecha actual.");
        //        valido = false;
        //    }
        //    if (txt_clave.Text != txt_conclave.Text)
        //    {
        //        controlErrores.SetError(txt_conclave, "La clave no es igual a la de confirmación");
        //        valido = false;
        //    }

        //    return valido;

        //}
        //public void QuitarEspacios()
        //{

        //}
        //private void GrabarDatos()// se comenta no se esta utilizando// MarioValencia // 22-01-2024
        //{
        //    try
        //    {
        //        DialogResult resultado;
        //        gridUsuarios.Focus();

        //        if (ValidarFormulario())
        //        {
        //            resultado = MessageBox.Show("Desea nodificar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //            if (resultado == DialogResult.Yes)
        //            {

        //                usuModificada.ID_USUARIO = usuarioModificada.ID_USUARIO;
        //                DEPARTAMENTOS depModificado = cmb_departamento.SelectedItem as DEPARTAMENTOS;
        //                usuModificada.DEPARTAMENTOSReference.EntityKey = depModificado.EntityKey;
        //                usuModificada.NOMBRES = usuarioModificada.NOMBRES.Trim();
        //                usuModificada.APELLIDOS = usuarioModificada.APELLIDOS.Trim();
        //                usuModificada.IDENTIFICACION = usuarioModificada.IDENTIFICACION.Trim();
        //                usuModificada.FECHA_INGRESO = usuarioModificada.FECHA_INGRESO;
        //                usuModificada.FECHA_VENCIMIENTO = usuarioModificada.FECHA_VENCIMIENTO;
        //                usuModificada.DIRECCION = usuarioModificada.DIRECCION.Trim();
        //                usuModificada.ESTADO = usuarioModificada.ESTADO;
        //                usuModificada.USR = usuarioModificada.USR.Trim();
        //                usuModificada.PWD = usuarioModificada.PWD.Trim();
        //                usuModificada.LOGEADO = usuarioModificada.LOGEADO;
        //                usuModificada.Codigo_Rol = usuarioModificada.Codigo_Rol;
        //                if (usuarioOriginal.ID_USUARIO == 0)
        //                {
        //                    if (NegNumeroControl.NumerodeControlAutomatico(2))
        //                        usuModificada.ID_USUARIO = Int16.Parse((NegUsuarios.RecuperaMaximoUsuario() + 1).ToString());
        //                    NegUsuarios.CrearUsuario(usuModificada);
        //                }
        //                else
        //                {


        //                    //usuModificada.EntityKey = new EntityKey(usuario.First().ENTITYSETNAME, usuario.First().ENTITYID, usuarioModificada.ID_USUARIO);

        //                    usuOrigen.ID_USUARIO = usuarioOriginal.ID_USUARIO;
        //                    DEPARTAMENTOS depOriginal = departamento.Where(dep => dep.DEP_CODIGO == usuarioOriginal.DEP_CODIGO).FirstOrDefault();
        //                    usuOrigen.DEPARTAMENTOSReference.EntityKey = depOriginal.EntityKey;
        //                    usuOrigen.NOMBRES = usuarioOriginal.NOMBRES.Trim();
        //                    usuOrigen.APELLIDOS = usuarioOriginal.APELLIDOS.Trim();
        //                    usuOrigen.IDENTIFICACION = usuarioOriginal.IDENTIFICACION.Trim();
        //                    usuOrigen.FECHA_INGRESO = usuarioOriginal.FECHA_INGRESO;
        //                    usuOrigen.FECHA_VENCIMIENTO = usuarioOriginal.FECHA_VENCIMIENTO;
        //                    usuOrigen.DIRECCION = usuarioOriginal.DIRECCION;
        //                    usuOrigen.ESTADO = usuarioOriginal.ESTADO;
        //                    usuOrigen.USR = usuarioOriginal.USR.Trim();
        //                    usuOrigen.PWD = usuarioOriginal.PWD.Trim();
        //                    usuOrigen.LOGEADO = usuarioOriginal.LOGEADO;
        //                    usuOrigen.Codigo_Rol = usuarioOriginal.Codigo_Rol;
        //                    //usuOrigen.EntityKey = new EntityKey(usuario.First().ENTITYSETNAME, usuario.First().ENTITYID, usuarioOriginal.ID_USUARIO);

        //                    NegUsuarios.GrabarUsuario(usuModificada, usuOrigen);
        //                }
        //                //GrabarPerfil();
        //                RecuperaUsuarios();
        //                ActualizaMedico();
        //                MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                HalitarControles(false, false, false, false, false, true, false, false, false, false);
        //                ResetearControles();
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Datos incompletos, por favor ingrese información en los campos obligatorios", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        MessageBox.Show("Err", err.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        private void GuardarDatosNuevo()
        {
            try
            {
                DialogResult resultado;
                gridUsuarios.Focus();
                if (validarFormularioTotal())
                {
                    resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        Int32 resp;

                        string ced;
                        DataTable buscaCed = new DataTable();
                        buscaCed = NegUsuarios.BuscaCedula(txt_identificacion.Text);
                        if (usuarioNuevo == true)
                        {
                            if (buscaCed.Rows.Count > 0)
                            {
                                ced = buscaCed.Rows[0]["identificacion"].ToString();

                                if (ced != "")
                                {
                                    MessageBox.Show("El No. Cedula ya esta registrada ", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    txt_identificacion.Focus();
                                    usuarioNuevo = true;
                                    return;
                                }
                            }
                            string nomUsuario;
                            DataTable buscaUsr = new DataTable();
                            buscaUsr = NegUsuarios.BuscaUsuario(txt_usuario.Text);

                            if (buscaUsr.Rows.Count > 0)
                            {
                                nomUsuario = buscaUsr.Rows[0]["nombreusu"].ToString();

                                if (nomUsuario != "")
                                {
                                    MessageBox.Show("El usuarios ya esta registrado ", "Ingreso Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    txt_usuario.Focus();
                                    usuarioNuevo = true;
                                    return;
                                }
                            }

                        }
                        Usuarios usuarios = new Usuarios();

                        usuarios.Nombre = Convert.ToString(txt_nombres.Text);
                        usuarios.Apellidos = Convert.ToString(txt_apellidos.Text);
                        usuarios.Identificacion = Convert.ToString(txt_identificacion.Text);
                        usuarios.Nombreusu = Convert.ToString(txt_usuario.Text);
                        usuarios.Codusu = Convert.ToInt32(txt_id.Text);
                        usuarios.Clave = Convert.ToString(txt_clave.Text);
                        usuarios.Codigo_c = 0;
                        usuarios.FechaIngreso = Convert.ToDateTime(txt_fecing.Text);
                        usuarios.FechaCaducidad = Convert.ToDateTime(txt_fecven.Text);
                        usuarios.TipoUsuario = 0;
                        if (chk_activo.Checked == true)
                            usuarios.Estado = 1;
                        else
                            usuarios.Estado = 0;
                        usuarios.CodDep = Convert.ToDouble(cmb_departamento.SelectedValue);
                        usuarios.Direccion = cmbArea.SelectedValue.ToString();

                        NegUsuarios usuariosBLL = new NegUsuarios();



                        if (usuarioNuevo == true)
                        {
                            resp = usuariosBLL.insertarUsuario(usuarios);
                            txt_id.Text = usuarios.IdUsuario.ToString();
                            usuarioNuevo = false;
                            usuarioOriginal.ID_USUARIO = Convert.ToInt16(usuarios.IdUsuario);
                        }
                        else
                        {
                            usuarios.IdUsuario = Convert.ToInt64(txt_codigoUsuario.Text);
                            resp = usuariosBLL.actualizar(usuarios);
                            //usuarioModificada.ID_USUARIO = Convert.ToInt16(usuarios.IdUsuario);
                        }
                        //GrabarPerfil();
                        RecuperaUsuarios();
                        MessageBox.Show("Datos Guardados correctamente !!!!!");
                        tabControl1.SelectedTab = tabControl1.Tabs[0];
                        SendKeys.SendWait("{TAB}");
                        HalitarControles(false, false, false, false, false, true, false, false, false, false);
                        ResetearControles();
                    }
                }
                else
                {
                    MessageBox.Show("Datos incompletos, por favor ingrese información en los campos obligatorios", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerException.Message);
            }
        }
        //private void GuardarDatosOtro()
        //{
        //    try
        //    {
        //        Int32 resp;

        //        string ced;
        //        DataTable buscaCed = new DataTable();
        //        buscaCed = NegUsuarios.BuscaCedula(txt_identificacion.Text);
        //        if (usuarioNuevo == true)
        //        {
        //            if (buscaCed.Rows.Count > 0)
        //            {
        //                ced = buscaCed.Rows[0]["identificacion"].ToString();

        //                if (ced != "")
        //                {
        //                    MessageBox.Show("El No. Cedula ya esta registrada ", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    txt_identificacion.Focus();
        //                    usuarioNuevo = true;
        //                    return;
        //                }
        //            }
        //            string nomUsuario;
        //            DataTable buscaUsr = new DataTable();
        //            buscaUsr = NegUsuarios.BuscaUsuario(txt_usuario.Text);

        //            if (buscaUsr.Rows.Count > 0)
        //            {
        //                nomUsuario = buscaUsr.Rows[0]["nombreusu"].ToString();

        //                if (nomUsuario != "")
        //                {
        //                    MessageBox.Show("El usuarios ya esta registrado ", "Ingreso Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    txt_usuario.Focus();
        //                    usuarioNuevo = true;
        //                    return;
        //                }
        //            }

        //        }
        //        Usuarios usuarios = new Usuarios();

        //        usuarios.Nombre = Convert.ToString(txt_nombres.Text);
        //        usuarios.Apellidos = Convert.ToString(txt_apellidos.Text);
        //        usuarios.Identificacion = Convert.ToString(txt_identificacion.Text);
        //        usuarios.Nombreusu = Convert.ToString(txt_usuario.Text);
        //        usuarios.Codusu = Convert.ToInt32(txt_id.Text);
        //        usuarios.Clave = Convert.ToString(txt_clave.Text);
        //        usuarios.Codigo_c = 0;
        //        usuarios.FechaIngreso = Convert.ToDateTime(txt_fecing.Text);
        //        usuarios.FechaCaducidad = Convert.ToDateTime(txt_fecven.Text);
        //        usuarios.TipoUsuario = 0;
        //        if (chk_activo.Checked == true)
        //            usuarios.Estado = 1;
        //        else
        //            usuarios.Estado = 0;
        //        usuarios.CodDep = Convert.ToDouble(cmb_departamento.SelectedValue);
        //        usuarios.Direccion = cmbArea.SelectedValue.ToString();

        //        NegUsuarios usuariosBLL = new NegUsuarios();

        //        if (usuarioNuevo == true)
        //        {
        //            resp = usuariosBLL.insertarUsuario(usuarios);
        //            txt_id.Text = usuarios.IdUsuario.ToString();
        //            usuarioNuevo = false;
        //            usuarioOriginal.ID_USUARIO = Convert.ToInt16(usuarios.IdUsuario);
        //        }
        //        else
        //        {
        //            usuarios.IdUsuario = Convert.ToInt64(txt_codigoUsuario.Text);
        //            resp = usuariosBLL.actualizar(usuarios);
        //            usuarioModificada.ID_USUARIO = Convert.ToInt16(usuarios.IdUsuario);
        //        }
        //        GrabarPerfil();
        //        // RecuperaUsuarios();

        //        //HalitarControles(false, false, false, false, false, true, false, false, false, false);
        //        //ResetearControles();


        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.InnerException.Message);
        //    }
        //}
        protected virtual void HalitarControles(bool datosPrincipales, bool datosSecundarios, bool Modificar, bool Grabar, bool Eliminar, bool Nuevo, bool Cancelar, bool perfileshis, bool perfilSic, bool perfilCg)
        {
            btnNuevo.Enabled = Nuevo;
            btnActualizar.Enabled = Modificar;
            btnEliminar.Enabled = Eliminar;
            btnGuardar.Enabled = Grabar;
            btnCancelar.Enabled = Cancelar;
            grpDatosPrincipales.Enabled = datosPrincipales;
            grpDatosSecundarios.Enabled = datosSecundarios;
            grid_perfiles.Enabled = perfileshis;
            grd_PerfilesSic.Enabled = perfilSic;
            dtg_PerfilesCg1.Enabled = perfilCg;

        }
        private void RecuperaUsuarios()
        {
            try
            {
                //ARE_ASIGNADA AreaAsignada = new ARE_ASIGNADA(); // se comenta por que tardaba demasiado // Mario Valencia // 22-01-2024
                //string perfil;
                //DataTable usu = new DataTable();
                //usu.Columns.Add("CODIGO");
                //usu.Columns.Add("NOMBRE");
                //usu.Columns.Add("CI");
                //usu.Columns.Add("ACTIVO");
                //usu.Columns.Add("DEPARTAMENTO");
                //usu.Columns.Add("AREA_ASIGNADA");
                //usu.Columns.Add("PERFIL");
                //usuario = NegUsuarios.RecuperaUsuariosFormulario();
                //bool parse;
                //foreach (var item in usuario)
                //{
                //    Int32 ValorArea = 1;
                //    DEPARTAMENTOS dep = NegUsuarios.ConsultaDepartamento(item.DEP_CODIGO);
                //    PERFILES per = NegPerfil.recuperaPerfilesXUsuario(item.ID_USUARIO);
                //    if (per != null)
                //        perfil = per.DESCRIPCION;
                //    else
                //        perfil = "SIN PERFIL";
                //    parse = Int32.TryParse(item.DIRECCION, out ValorArea);
                //    if (parse)
                //        AreaAsignada = NegUsuarios.ConsultaArea(ValorArea);
                //    else
                //        AreaAsignada = NegUsuarios.ConsultaArea(1);
                //    usu.Rows.Add(new object[] { item.ID_USUARIO, item.APELLIDOS + ' ' + item.NOMBRES, item.IDENTIFICACION, item.ESTADO, dep.DEP_NOMBRE, AreaAsignada.AS_DESCRIPCION, perfil });
                //}
                gridUsuarios.DataSource = NegUsuarios.RecuperaListaUsuario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ultraGrid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            try
            {
                if (gridUsuarios.ActiveRow.Index > -1)
                {
                    UltraGridRow fila = gridUsuarios.ActiveRow;
                    if (gridUsuarios.Selected.Rows.Count > 0)
                    {
                        //usuario = NegUsuarios.RecuperaUsuariosFormulario();
                        id_usuario = Convert.ToInt64(gridUsuarios.ActiveRow.Cells["CODIGO"].Value.ToString());
                        usuarioDTs = NegUsuarios.RecuperaUsuario(id_usuario);
                        HalitarControles(true, true, true, true, true, false, true, true, true, true);
                        habilitarTab(true, true, true);
                        txt_id.Text = fila.Cells[0].Value.ToString();
                        txt_codigoUsuario.Text = fila.Cells[0].Value.ToString();
                        NegUsuarios negusus = new NegUsuarios();
                        AgregarBindigControles();
                        CargaGrid(Int16.Parse(txt_id.Text));
                        usuarioNuevo = false;
                        int index = cmbArea.FindString(gridUsuarios.ActiveRow.Cells["AREA_ASIGNADA"].Value.ToString());
                        cmbArea.SelectedIndex = index;
                        int depindex = cmb_departamento.FindString(gridUsuarios.ActiveRow.Cells["DEPARTAMENTO"].Value.ToString());
                        cmb_departamento.SelectedIndex = depindex;
                    }
                    //usuario = NegUsuarios.RecuperaUsuariosFormulario();
                    //HalitarControles(true, true, true, true, true, false, true);
                    ////usuarioModificada = gridUsuarios.CurrentRow.DataBoundItem as DtoUsuarios;
                    //int codUsuario = Convert.ToInt16(gridUsuarios.ActiveRow.Cells["CODIGO"].Value.ToString());
                    //usuarioModificada = usuario.FirstOrDefault(u => u.ID_USUARIO == codUsuario);
                    //usuarioOriginal = usuarioModificada.ClonarEntidad();
                    //AgregarBindigControles();
                    //CargaGrid(Int16.Parse(txt_id.Text));
                    //usuarioNuevo = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        /// <summary>
        /// Carga los datos del perfil en el grid deacuerdo a un usuario determinado
        /// </summary>
        /// <param name="codusu">codigo del usuario</param>
        private void CargaGrid(Int16 codusu)
        {
            try
            {
                List<DtoUsuariosPerfil> usuariosperfiles = new List<DtoUsuariosPerfil>();
                usuariosperfiles = NegUsuarios.ListaConsultaTablasOpciones(codusu);
                grid_perfiles.DataSource = usuariosperfiles;
                // grid_perfiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                #region CargarGridSic


                // ultraGridSic.DataSource = NegUsuarios.ListaSic();



                //DataTable CabeceraSic = new DataTable();
                //DataTable DetalleSic = new DataTable();
                //DataTable ContenidoSic = new DataTable();

                //DataColumn dc = new DataColumn();
                //dc.ColumnName = "Id_PERFIL";
                //dc.DataType = typeof(double);

                //DataColumn dc1 = new DataColumn();
                //dc1.ColumnName = "DESCRIPCION";
                //dc1.DataType = typeof(string);

                //DataColumn dc2 = new DataColumn();
                //dc2.ColumnName = "TIENEACCESO";
                //dc2.DataType = typeof(bool);

                //ContenidoSic.Columns.AddRange(new DataColumn[] { dc, dc1, dc2 });

                //CabeceraSic = NegUsuarios.BuscaPerfilesSic();
                //DetalleSic = NegUsuarios.BuscaConsidenciasSic(codusu);
                //if (DetalleSic.Rows.Count == 0)
                //{
                //    for (int i = 0; i < CabeceraSic.Rows.Count; i++)
                //    {
                //        bool tieneAcceso = false;
                //        ContenidoSic.Rows.Add(new object[] { Convert.ToDouble(CabeceraSic.Rows[i][0]), Convert.ToString(CabeceraSic.Rows[i][1]), tieneAcceso });
                //    }
                //}
                //else
                //{
                //    int cont = 0;
                //    for (int i = 0; i < CabeceraSic.Rows.Count; i++)
                //    {
                //        bool tieneAcceso = false;
                //        int cb = Convert.ToInt32(CabeceraSic.Rows[i][0]);
                //        int dt = Convert.ToInt32(DetalleSic.Rows[cont][0]);
                //        if (cb == dt)
                //        {
                //            tieneAcceso = true;
                //            if (cont < DetalleSic.Rows.Count - 1)
                //            {
                //                cont++;
                //            }
                //        }
                //        ContenidoSic.Rows.Add(new object[] { Convert.ToDouble(CabeceraSic.Rows[i][0]), Convert.ToString(CabeceraSic.Rows[i][1]), tieneAcceso });
                //    }
                //}
                //DataView dv = ContenidoSic.DefaultView;
                //dv.Sort = "DESCRIPCION";
                //DataTable desc = dv.ToTable();
                //grd_PerfilesSic.DataSource = desc;
                //grd_PerfilesSic.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                #endregion
                #region CargarGridCg
                //DataTable CabeceraCg = new DataTable();
                //DataTable DetalleCg = new DataTable();
                //DataTable ContenidoCg = new DataTable();

                //DataColumn dcg = new DataColumn();
                //dcg.ColumnName = "Id_PERFIL";
                //dcg.DataType = typeof(double);

                //DataColumn dcg1 = new DataColumn();
                //dcg1.ColumnName = "DESCRIPCION";
                //dcg1.DataType = typeof(string);

                //DataColumn dcg2 = new DataColumn();
                //dcg2.ColumnName = "TIENEACCESO";
                //dcg2.DataType = typeof(bool);

                //DataColumn dcg3 = new DataColumn();
                //dcg3.ColumnName = "SW";
                //dcg3.DataType = typeof(string);

                //ContenidoCg.Columns.AddRange(new DataColumn[] { dcg, dcg1, dcg2, dcg3 });

                //CabeceraCg = NegUsuarios.BuscaPerfilesCg();
                //DetalleCg = NegUsuarios.BuscaConsidenciasCG(codusu);
                //if (DetalleCg.Rows.Count == 0)
                //{
                //    for (int i = 0; i < CabeceraCg.Rows.Count; i++)
                //    {
                //        bool tieneAcceso = false;
                //        ContenidoCg.Rows.Add(new object[] { Convert.ToDouble(CabeceraCg.Rows[i][0]), Convert.ToString(CabeceraCg.Rows[i][1]), tieneAcceso, Convert.ToString(CabeceraCg.Rows[i][2]) });
                //    }
                //}
                //else
                //{
                //    int contg = 0;
                //    for (int i = 0; i < CabeceraCg.Rows.Count; i++)
                //    {
                //        bool tieneAcceso = false;
                //        int cb = Convert.ToInt32(CabeceraCg.Rows[i][0]);
                //        int dt = Convert.ToInt32(DetalleCg.Rows[contg][0]);
                //        if (cb == dt)
                //        {
                //            tieneAcceso = true;
                //            if (contg < DetalleCg.Rows.Count - 1)
                //            {
                //                contg++;
                //            }
                //        }
                //        ContenidoCg.Rows.Add(new object[] { Convert.ToDouble(CabeceraCg.Rows[i][0]), Convert.ToString(CabeceraCg.Rows[i][1]), tieneAcceso, Convert.ToString(CabeceraCg.Rows[i][2]) });
                //    }
                //}
                //dtg_PerfilesCg.DataSource = ContenidoCg;
                //dtg_PerfilesCg.Columns[3].Visible = false;
                //dtg_PerfilesCg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                #endregion

                //grd_PerfilesSic.DataSource = NegUsuarios.BuscaConsidenciasSic(codusu);



                ultraGridSic.DataSource = NegUsuarios.BuscaConsidenciasSic(codusu);
                dtg_PerfilesCg.DataSource = NegUsuarios.BuscaConsidenciasCG(codusu);




            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }

        }
        private void CargaGridSic()
        {

        }
        private void CargaGridCg()
        {

        }
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        #endregion

        private void ultraTabControl1_Click(object sender, EventArgs e)
        {
            //if (txt_id.Text != string.Empty)
            //    CargaGrid(Int16.Parse(txt_id.Text));
        }



        private void gridUsuarios_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            if (inicio == true)
            {
                UltraGridBand bandUno = gridUsuarios.DisplayLayout.Bands[0];

                gridUsuarios.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                gridUsuarios.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                gridUsuarios.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                gridUsuarios.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                gridUsuarios.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                gridUsuarios.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
                gridUsuarios.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                gridUsuarios.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                gridUsuarios.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                gridUsuarios.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                gridUsuarios.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                gridUsuarios.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                gridUsuarios.DisplayLayout.UseFixedHeaders = true;

                bandUno.Columns["CODIGO"].MaxWidth = 50;
                bandUno.Columns["CODIGO"].MinWidth = 50;

                bandUno.Columns["NOMBRE"].MaxWidth = 250;
                bandUno.Columns["NOMBRE"].MinWidth = 250;

                inicio = false;
            }
            e.Layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }
        public void habilitarTab(bool pg1, bool pg2, bool pg3)
        {
            ultraTabPageControl4.Enabled = pg1;
            ultraTabPageControl2.Enabled = pg2;
            ultraTabPageControl5.Enabled = pg3;
        }

        private void txt_fecven_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (Convert.ToDateTime(txt_fecing.Text) > Convert.ToDateTime(txt_fecven.Text))
                {
                    controlErrores.SetError(txt_fecing, "Fecha de ingreso no puede ser mayor a la de vencimiento");
                }
                if (Convert.ToDateTime(txt_fecven.Text) < Convert.ToDateTime(txt_fecing.Text))
                {
                    controlErrores.SetError(txt_fecven, "Fecha de vencimiento no puede ser menor a la de ingreso");
                }
                if ((Convert.ToDateTime(txt_fecing.Text)).Date > DateTime.Now.Date)
                {
                    controlErrores.SetError(txt_fecing, "Fecha de ingreso no puede ser mayor a la fecha actual.");
                }
            }
        }

        private void txt_usuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                string nomUsuario;
                DataTable buscaUsr = new DataTable();
                buscaUsr = NegUsuarios.BuscaUsuario(txt_usuario.Text);

                if (buscaUsr.Rows.Count > 0)
                {
                    nomUsuario = buscaUsr.Rows[0]["nombreusu"].ToString();

                    if (nomUsuario != "")
                    {
                        txt_usuario.Focus();
                        controlErrores.SetError(txt_usuario, "El usuario ya existe");
                    }
                }
            }
        }

        private void chk_Medico_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Medico.Checked)
            {
                string medicoRuc = txt_identificacion.Text + "001";

                medico = NegMedicos.recuperarMedicoRUC(medicoRuc);
                if (medico == null)
                {
                    MessageBox.Show("La identificación ingresada no pertenece a un médico, verifique la misma y vuelva a intentar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    chk_Medico.Checked = false;
                    medicoRuc = "";
                }
                else
                {
                    NegUsuarios.ActualizarMedicoUsuario(Int64.Parse(txt_id.Text), medicoRuc);
                    MessageBox.Show("Medico: " + medico.MED_APELLIDO_PATERNO + " " + medico.MED_NOMBRE1 + ". Encontrado con exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void frm_Usuario_Fill_Panel_PaintClient(object sender, PaintEventArgs e)
        {

        }

        private void grd_PerfilesSic_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            GrabaPerfilSic();

        }

        private void dtg_PerfilesCg_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            GrabaPerfilCg();
        }

        private void ultraGridSic_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                bool estado;
                if (bool.TryParse(e.Cell.Value.ToString(), out estado))
                {
                    foreach (var item in ultraGridSic.Rows)
                    {

                        if (item.Cells["ID"].Value.ToString() == e.Cell.Row.Cells["ID"].Value.ToString())
                        {
                            if (!Convert.ToBoolean(item.Cells["TIENE_ACCESO"].Value.ToString()))
                            {
                                NegUsuarios.BorrarPerfilSicUsu(Int64.Parse(txt_id.Text));
                                if (!NegUsuarios.CrearPerfilSicUsu(Int64.Parse(txt_id.Text), Convert.ToInt64(e.Cell.Row.Cells["ID"].Value.ToString()), true))
                                {
                                    MessageBox.Show("No se ha podido crear el acceso ", "Sic-3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                ultraGridSic.DataSource = NegUsuarios.BuscaConsidenciasSic(Int64.Parse(txt_id.Text));

                            }
                            else
                            {
                                if (!NegUsuarios.CrearPerfilSicUsu(Int64.Parse(txt_id.Text), Convert.ToInt64(e.Cell.Row.Cells["ID"].Value.ToString()), false))
                                {
                                    MessageBox.Show("No se ha podido crear el acceso ", "Sic-3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                //throw;
            }
        }

        private void tabControl1_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {

        }

        private void ultraGridSic_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }

        private void dtg_PerfilesCg_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                bool estado;
                if (bool.TryParse(e.Cell.Value.ToString(), out estado))
                {
                    foreach (var item in dtg_PerfilesCg.Rows)
                    {
                        if (item.Cells["ID"].Value.ToString() == e.Cell.Row.Cells["ID"].Value.ToString())
                        {
                            if (!Convert.ToBoolean(item.Cells["TIENE_ACCESO"].Value.ToString()))
                            {
                                NegUsuarios.BorrarPerfilCgUsu(Int64.Parse(txt_id.Text));

                                if (!NegUsuarios.CrearPerfilCgUsu(Int64.Parse(txt_id.Text), Convert.ToInt64(e.Cell.Row.Cells["ID"].Value.ToString()), true))
                                {
                                    MessageBox.Show("No se ha podido crear el acceso ", "Cg-3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                dtg_PerfilesCg.DataSource = NegUsuarios.BuscaConsidenciasCG(Int64.Parse(txt_id.Text));

                            }
                            else
                            {
                                if (!NegUsuarios.CrearPerfilCgUsu(Int64.Parse(txt_id.Text), Convert.ToInt64(e.Cell.Row.Cells["ID"].Value.ToString()), false))
                                {
                                    MessageBox.Show("No se ha podido crear el acceso ", "Cg-3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                //throw;
            }
        }

        private void dtg_PerfilesCg_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }

        private void grid_perfiles_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                bool estado;
                if (bool.TryParse(e.Cell.Value.ToString(), out estado))
                {
                    foreach (var item in grid_perfiles.Rows)
                    {

                        if (item.Cells["ID_PERFIL"].Value.ToString() == e.Cell.Row.Cells["ID_PERFIL"].Value.ToString())
                        {
                            DataTable usuSic = NegUsuarios.usuarioXCedulaSic(txt_identificacion.Text.Substring(0, 10));
                            DataTable usuCg = NegUsuarios.usuarioXCedulaSic(txt_identificacion.Text.Substring(0, 10));
                            if (!Convert.ToBoolean(item.Cells["TIENE_ACCESO"].Value.ToString()))
                            {
                                if (usuSic.Rows.Count != 0)
                                {
                                    NegUsuarios.BorrarPerfilSicUsu(Int64.Parse(usuSic.Rows[0]["codusu"].ToString()));
                                    if (!NegUsuarios.CrearPerfilSicUsu(Int64.Parse(usuSic.Rows[0]["codusu"].ToString()), Convert.ToInt64(e.Cell.Row.Cells["ID_PERFIL"].Value.ToString()), true))
                                    {
                                        MessageBox.Show("No se ha podido crear el acceso ", "Sic-3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                if (usuCg.Rows.Count != 0)
                                {
                                    NegUsuarios.BorrarPerfilCgUsu(Int64.Parse(usuCg.Rows[0]["codusu"].ToString()));
                                    if (!NegUsuarios.CrearPerfilCgUsu(Int64.Parse(usuCg.Rows[0]["codusu"].ToString()), Convert.ToInt64(e.Cell.Row.Cells["ID_PERFIL"].Value.ToString()), true))
                                    {
                                        MessageBox.Show("No se ha podido crear el acceso ", "Cg-3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            else
                            {
                                if (usuSic.Rows.Count != 0)
                                {
                                    NegUsuarios.BorrarPerfilSicUsu(Int64.Parse(usuSic.Rows[0]["codusu"].ToString()));
                                    if (!NegUsuarios.CrearPerfilSicUsu(Int64.Parse(usuSic.Rows[0]["codusu"].ToString()), Convert.ToInt64(e.Cell.Row.Cells["ID_PERFIL"].Value.ToString()), false))
                                    {
                                        MessageBox.Show("No se ha podido crear el acceso ", "Sic-3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                if (usuCg.Rows.Count != 0)
                                {
                                    NegUsuarios.BorrarPerfilCgUsu(Int64.Parse(usuCg.Rows[0]["codusu"].ToString()));
                                    if (!NegUsuarios.CrearPerfilCgUsu(Int64.Parse(usuCg.Rows[0]["codusu"].ToString()), Convert.ToInt64(e.Cell.Row.Cells["ID_PERFIL"].Value.ToString()), false))
                                    {
                                        MessageBox.Show("No se ha podido crear el acceso ", "Cg-3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                //throw;
            }
            try
            {
                int fila;
                fila = grid_perfiles.ActiveRow.Index;
                UltraGridBand band = grid_perfiles.DisplayLayout.Bands[0];

                UltraGridColumn checkBoxColumn = band.Columns["TIENE_ACCESO"];


                foreach (UltraGridRow row in band.GetRowEnumerator(GridRowType.DataRow))
                {
                    row.Cells[checkBoxColumn].Value = false;

                }


                grid_perfiles.Rows[fila].Cells["TIENE_ACCESO"].Value = true;
                GrabarPerfil();
                MensajeGuardarConPausa("PERFIL GUARDADO");
                RecuperaUsuarios();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void grid_perfiles_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            e.Layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }
        private void MensajeGuardarConPausa(string mensaje)
        {
            lblMensaje.Text = mensaje;

            // Iniciar un hilo para la pausa en segundo plano
            Thread thread = new Thread(() =>
            {
                // Pausa de 3 segundos
                Thread.Sleep(6000);

                // Limpiar el texto después de la pausa
                lblMensaje.Invoke((MethodInvoker)delegate
                {
                    lblMensaje.Text = "-";
                });
            });

            // Iniciar el hilo
            thread.Start();
        }
    }
}
