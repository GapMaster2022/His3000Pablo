using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Negocio;
using His.Entidades.Clases;
using His.Parametros;
using System.Windows.Forms.Integration;
using Core.Utilitarios;
using MessageBox = System.Windows.MessageBox;
using His.Dietetica;
using System.IO;
using His.Admision;

//using His.AdmisionWPFUI;

namespace His.Dietetica
{
    public partial class MDI_PE : Form
    {
        #region Variables
        private int childFormNumber = 0;
        private DtoParametros parametrosGenerales = new DtoParametros();
        //public int Global_PE_codUser;
        //public string Global_PE_nomUser;
        #endregion

        #region Constructor
        public MDI_PE()
        {
            InitializeComponent();
            if (!NegUtilitarios.IpBodegas())
            {
                MessageBox.Show("Está maquina no tiene asignada una bodega, por lo que no se puede abrir el modulo", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            ultraStatusBarTarifario.Panels["empresa"].Text = Sesion.nomEmpresa;
            ultraStatusBarTarifario.Panels["usuario"].Text = Sesion.nomUsuario;
            ultraStatusBarTarifario.Panels["cod_user"].Text = Sesion.codUsuario.ToString();
            //Global_PE_codUser = Sesion.codUsuario;
            //Global_PE_nomUser = Sesion.nomUsuario;

        }
        #endregion

        #region Eventos
        //private void timHora_Tick(object sender, EventArgs e)
        //{
        //    txtHora.Text = "HORA: " + DateTime.Now.ToString("T");
        //}
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }
        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }
        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }
        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        private void smnu_salir_Click(object sender, EventArgs e)
        {
            this.Close();
            //Application.Exit();
        }

        #endregion

        #region Metodos Privados
        
        private void InicializarForma()
        {
       
            //cargo usuario
            if (Sesion.codUsuario == 0)
            {
                try
                {
                    His.Parametros.ArchivoIni archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
                    archivo.IniReadValue("Usuario", "usr");
                    string usuario = archivo.IniReadValue("Usuario", "usr");
                    string clave = EncriptadorUTF.Desencriptar(archivo.IniReadValue("Usuario", "pwd"));
                    USUARIOS usuarioarchivo = NegUsuarios.ValidarUsuario(usuario, clave);
                    //archivo.IniWriteValue("Usuario", "usr", usuario.USR);
                    //archivo.IniWriteValue("Usuario", "pwd", usuario.PWD); 
                    if (usuarioarchivo != null)
                    {
                        Sesion.codUsuario = usuarioarchivo.ID_USUARIO;
                        Sesion.nomUsuario = usuarioarchivo.NOMBRES + " " + usuarioarchivo.APELLIDOS;
                    }
                    else
                    {

                        System.Windows.Forms.MessageBox.Show("El usuario no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Sesion.TipoAcceso = His.Entidades.Acceso.DIRECTO;

            //txtFecha.Text = "FECHA: " + DateTime.Now.ToString("D");

            //timHora.Start();
            
            if (Sesion.codUsuario != 0)
            {
                USUARIOS usuario = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);

                List<PERFILES> perfilUsuario = new NegPerfil().RecuperarPerfil(usuario.ID_USUARIO);

                if (perfilUsuario != null)
                {
                    foreach (var Perfil in perfilUsuario)
                    {
                        if (perfilUsuario != null)
                            NegAccesoOpciones.asignarAccesosPorPerfil(Perfil.ID_PERFIL);
                        else
                        {
                            System.Windows.MessageBox.Show("No se asignado un perfil al usuario", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                            this.Close();
                        }
                    }
                }
                pedidosToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.Dietetica;
                dieteticaToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.Pedido;

                gastroenterologiaToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.Gastroenterologia;
                agregarProductoToolStripMenuItem1.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.GagergarProducto;
                agregarProcedimientoToolStripMenuItem1.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.GagregarProcedimiento;
                pedidoPacienteToolStripMenuItem1.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.GpedidoPaciente;
                reposicionToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.GreposicionProducto;

                mnuImagen.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.Imagen;
                agendamientoToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.Agendamiento;
                examenesRealizadosToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.ExamenesAgendados;
                informeToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.Informe;
                exploradorDePedidosToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.ExplPedidos;
                horarioDeMédicosToolStripMenuItem1.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.HorarioMedico;

                laboratorioToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.LabClinico;
                crearToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.CrearPerfiles;
                exploradorDePedidosToolStripMenuItem1.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.CexplPedidos;
                pacientesToolStripMenuItem1.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.Pacientes;
                examenesPorPerfilesToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.ExamenesPerfiles;

                mnuLabPat.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.LabPatologico;
                exploradorPedidosToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.PexplPedidos;

                quirofanoToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.Quirofano;
                agregarProductoToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.QagergarProducto;
                agregarProcedimientoToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.QagregarProcedimiento;
                pedidoPacienteToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.QpedidoPaciente;
                exploradorToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.QreposicionProducto;
                exploradorProcedimientoToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.ExpProcedimiento;
                exploradorDeProcedimientoToolStripMenuItem.Enabled = His.Parametros.AccesoModuloPedidosEspeciales.ExpRubros;

                //smnu_formularioshcu.Enabled = His.Parametros.AccesosModuloAdmision.FormulariosHCU;
                //mnu_Explorador.Enabled = His.Parametros.AccesosModuloAdmision.ExploradorPacientes;
                //mnu_admision.Enabled = His.Parametros.AccesosModuloAdmision.Admision;
                //mnu_admisionEmergencia.Enabled = His.Parametros.AccesosModuloAdmision.AdmisionEmergencia;   
                ////mnuEmergencias.Enabled = His.Parametros.AccesosModuloAdmision.Mantenimiento;
                //mnu_informacionmorbimortalidad.Enabled = His.Parametros.AccesosModuloAdmision.InfMorbimortalidad;
                //formulariosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.FormulariosHCU;
                //if (usuario.NOMBRES == usuario.APELLIDOS)
                //{
                //    txtNombres.Text = "Usr. " + string.Format("{0} ",
                //        usuario.NOMBRES);
                //}
                //else
                //{
                //    txtNombres.Text = "Usr. " + string.Format("{0} {1}",
                //        usuario.NOMBRES,
                //        usuario.APELLIDOS);
                //}
            }

            //EMPRESA empresa = NegEmpresa.RecuperaEmpresa();

            //txtEmpresa.Text = "Empresa " + string.Format("{0} ", empresa.EMP_NOMBRE);

        }
        #endregion

        private void MDI_Admision_Load(object sender, EventArgs e)
        {
            if (!NegUtilitarios.IpBodegas())
            {
                this.Close();
                return;
            }
            InicializarForma();
            //gastroenterologiaToolStripMenuItem.Visible = false; // Mario // se comenta por que se empieza a trabajar con seguridades // 07/11/2023
            //List<PERFILES> perfilUsuario = new NegPerfil().RecuperarPerfil(His.Entidades.Clases.Sesion.codUsuario);
            //foreach (var item in perfilUsuario)
            //{
            //    if (item.ID_PERFIL == 30)
            //    {
            //        if (item.DESCRIPCION.Contains("GASTRO")) //se debe tomar en cuenta que si es 29 en otra empresa no actuara de la forma como en la pasteur.
            //            gastroenterologiaToolStripMenuItem.Visible = true;
            //    }
            //}
        }
        #region CodBin
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }
        private void ingresToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void pacientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void atencionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void admisiónEmergenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void pacientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {   
        }

        private void tarifarioIESSToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void odontologiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void habitacionesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
        }

        private void historialHabitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void admisiónEmergenciaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
        }

        private void procedimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void garantiasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void formulariosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void iNENToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        #endregion


        private void dieteticaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCargaDietas x = new frmCargaDietas();
            x.MdiParent = this;
            x.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutPE x = new AboutPE();
            x.ShowDialog();
        }

        private void agendamientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImagenAgendamiento x = new ImagenAgendamiento(Sesion.nomUsuario, Sesion.codUsuario.ToString());
            x.MdiParent = this;
            x.Show();
        }

        private void informeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImagenInformeIndex x = new ImagenInformeIndex();
            x.MdiParent = this;
            x.Show();
        }

        private void pacientesToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Form frm = new FrmLaboratorio();
            frm.MdiParent = this;
            frm.Show();
        }

        private void exploradorDePedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExploradorPedidoImagen x = new frmExploradorPedidoImagen();
            x.MdiParent = this;
            x.Show();
        }

        private void planificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_HorarioLaboral x = new frm_HorarioLaboral();
            x.MdiParent = this;
            x.Show();
        }

        private void explorarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_HorarioExplorador x = new frm_HorarioExplorador();
            x.MdiParent = this;
            x.Show();
        }

        private void agregarProcedimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VerificaQuirofano())
            {
                return;
            }
            //frmQuirofanoAgregarProcedimiento x = new frmQuirofanoAgregarProcedimiento();
            frm_AgregarProductosNew x = new frm_AgregarProductosNew();
            x.MdiParent = this;
            x.Show();
        }

        private void agregarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VerificaQuirofano())
            {
                return;
            }
            frmQuirofanoAgregarProducto x = new frmQuirofanoAgregarProducto();
            x.MdiParent = this;
            x.Show();
        }

        private void pedidoPacienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VerificaQuirofano())
            {
                return;
            }
            frm_QuirofanoExploradorNew x = new frm_QuirofanoExploradorNew();
            //frmQuirofanoExplorador x = new frmQuirofanoExplorador();
            x.MdiParent = this;
            x.Show();
        }

        private void examenesRealizadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImagenExamenesRealizados x = new ImagenExamenesRealizados();
            x.MdiParent = this;
            x.Show();
        }

        private void planificarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frm_HorarioLaboral x = new frm_HorarioLaboral();
            x.MdiParent = this;
            x.Show();
        }

        private void explorarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frm_HorarioExplorador x = new frm_HorarioExplorador();
            x.MdiParent = this;
            x.Show();
        }

        private void exploradorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VerificaQuirofano())
            {
                return;
            }
            frmQuirofanoProductosDetalle x = new frmQuirofanoProductosDetalle();
            x.MdiParent = this;
            x.Show();
        }

        private void agregarProductoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //GASTRO AGREGA PRODUCTOS
            if (VericaGastro())
            {
                return;
            }
            if (NegParametros.ParametroBodegaGastro() == 0)
            {
                MessageBox.Show("Parametro de Bodega de gastro no ha sido creada.", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                frmQuirofanoAgregarProducto x = new frmQuirofanoAgregarProducto(NegParametros.ParametroBodegaGastro());
                x.MdiParent = this;
                x.Show();
            }
        }

        private void agregarProcedimientoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (VericaGastro())
            {
                return;
            }
            //GASTRO AGREGA PROCEDIMIENTOS
            if (NegParametros.ParametroBodegaGastro() == 0)
            {
                MessageBox.Show("Parametro de Bodega de gastro no ha sido creada.", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                //frmQuirofanoAgregarProcedimiento x = new frmQuirofanoAgregarProcedimiento(NegParametros.ParametroBodegaGastro());
                frm_AgregarProductosNew x = new frm_AgregarProductosNew(NegParametros.ParametroBodegaGastro());
                x.MdiParent = this;
                x.Show();
            }
        }

        private void pedidoPacienteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (VericaGastro())
            {
                return;
            }
            //GASTRO PEDIDO PACIENTE
            if (NegParametros.ParametroBodegaGastro() == 0)
            {
                MessageBox.Show("Parametro de Bodega de gastro no ha sido creada.", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                frm_QuirofanoExploradorNew x = new frm_QuirofanoExploradorNew();
                x.MdiParent = this;
                x.Show();
            }
        }

        private void reposicionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VericaGastro())
            {
                return;
            }
            //GASTRO PEDIDO PACIENTE
            //if (NegParametros.ParametroBodegaGastro() == 0)
            //{
            //    MessageBox.Show("Parametro de Bodega de gastro no ha sido creada.", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}
            //else //// Se cambia por 
            //{
            //frmQuirofanoProductosDetalle x = new frmQuirofanoProductosDetalle(NegParametros.ParametroBodegaGastro());
            frmQuirofanoProductosDetalle x = new frmQuirofanoProductosDetalle();
                x.MdiParent = this;
                x.Show();
            //}
        }

        private void exploradorDeProcedimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VerificaQuirofano())
            {
                return;
            }
            frmExploradorRubrosProcedimiento x = new frmExploradorRubrosProcedimiento();
            x.MdiParent = this;
            x.Show();
        }

        private void graficoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VerificaQuirofano())
            {
                return;
            }
            frmGraficosProcedimientos x = new frmGraficosProcedimientos();
            x.MdiParent = this;
            x.Show();
        }

        private void exploradorDePedidosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmExploradorLaboratorioClinico x = new FrmExploradorLaboratorioClinico();
            x.MdiParent = this;
            x.Show();
        }

        private void crearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPerfilesLaboratorio x = new FrmPerfilesLaboratorio();
            x.MdiParent = this;
            x.Show();
        }

        private void examenesPorPerfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exploradorPedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmExploradorLaboratorioPatologico x = new FrmExploradorLaboratorioPatologico();
            x.MdiParent = this;
            x.Show();
        }

        private void gastroenterologiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VericaGastro())
            {
                return;
            }
        }

        public bool VericaGastro()
        {
            if (Convert.ToInt32(NegParametros.ParametroBodegaGastro()) != Sesion.bodega)
            {
                MessageBox.Show("Estas opciones solo se pueden acceder desde el area de Gatroenterologia", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            return false;
        }

        private void quirofanoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VerificaQuirofano())
            {
                return;
            }
        }

        public bool VerificaQuirofano()
        {
            if (Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()) != Sesion.bodega)
            {
                MessageBox.Show("Estas opciones solo se pueden acceder desde el area de Quirofano", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            return false;
        }

        private void exploradorProcedimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (VerificaQuirofano())
            //{
            //    return;
            //}
            frmExploradorProcedimiento x = new frmExploradorProcedimiento();
            x.MdiParent = this;
            x.Show();
        }
    }
}
