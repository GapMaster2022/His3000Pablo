using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades.Clases;
using His.Entidades;
using His.Negocio;
using His.Parametros;
using His.Formulario;


namespace His.Honorarios
{
    public partial class MDI_Medicos : Form
    {
        #region Variables
        private int childFormNumber = 0;
        private DtoParametros parametrosGenerales = new DtoParametros();
        #endregion

        #region Constructor
        public MDI_Medicos()
        {
            InitializeComponent();
            if (!NegUtilitarios.IpBodegas())
            {
                MessageBox.Show("Está maquina no tiene asignada una bodega, por lo que no se puede abrir el modulo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            InicializarForma();

        }
        #endregion

        #region Eventos

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }
        private void smnu_salir_Click(object sender, EventArgs e)
        {
            this.Close();
            //Application.Exit();
        }
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
        private void MDI_Honorarios_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
        }

        private void MDI_Honorarios_Load(object sender, EventArgs e)
        {
            if (!NegUtilitarios.IpBodegas())
            {
                this.Close();
                return;
            }
            //this.BackgroundImage = Recursos.Archivo.logoEmpresa;
            ultraStatusBarTarifario.Panels["empresa"].Text = Sesion.nomEmpresa;
            ultraStatusBarTarifario.Panels["usuario"].Text = Sesion.nomUsuario;

            //USUARIOS usuario = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
            //List<PERFILES> perfilUsuario = new NegPerfil().RecuperarPerfil(usuario.ID_USUARIO);
            //if (perfilUsuario != null)
            //{
            //    foreach (var Perfil in perfilUsuario)
            //    {
            //        if (Perfil.ID_PERFIL == 32)
            //        {
            //            certificadosToolStripMenuItem.Visible = true;
            //        }
            //    }
            //}
        }
        #endregion

        #region Metodos Privados
        private void DeshabilitarMenu()
        {
            //bloquear el menu o submenu dependiendo del control de seguridad del usuario logeado

        }
        private void InicializarForma()
        {
            DeshabilitarMenu();
            //cargo usuario
            if (Sesion.codUsuario == 0)
            {
                try
                {
                    His.Parametros.ArchivoIni archivo = new ArchivoIni(Application.StartupPath + @"\his3000.ini");
                    archivo.IniReadValue("Usuario", "usr");
                    archivo.IniReadValue("Caja", "codigo");
                    var usuarioarchivo = NegUsuarios.RecuperaUsuarios().Where(usu => usu.USR == archivo.IniReadValue("Usuario", "usr")).FirstOrDefault();
                    //archivo.IniWriteValue("Usuario", "usr", usuario.USR);
                    //archivo.IniWriteValue("Usuario", "pwd", usuario.PWD); 

                    if (usuarioarchivo != null)
                    {
                        Sesion.codUsuario = usuarioarchivo.ID_USUARIO;
                    }
                    else
                    {
                        MessageBox.Show("El usuario no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    Sesion.codLocal = Convert.ToInt16(archivo.IniReadValue("Caja", "codigo"));
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // cargo iva
            parametrosGenerales = NegParametros.RecuperaParametros(1).Where(cod => cod.PAD_CODIGO == 1).FirstOrDefault();
            Sesion.porIva = decimal.Parse(parametrosGenerales.PAD_VALOR);
            Sesion.TipoAcceso = His.Entidades.Acceso.DIRECTO;
            List<ACCESO_OPCIONES> perfilesAccesos = new NegPerfilesAcceso().AccesosUsuarios(Sesion.codUsuario, Constantes.HONORARIOS);

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
                            System.Windows.MessageBox.Show("No se asignado un perfil al usuario", "error");
                            this.Close();
                        }
                    }
                }
            }
            exploradorToolStripMenuItem.Enabled = His.Parametros.AccesoModuloMedicos.explorador;
            certificadoMédicoToolStripMenuItem.Enabled = His.Parametros.AccesoModuloMedicos.explCertificadoMedico;
            recetaMédicaToolStripMenuItem1.Enabled = His.Parametros.AccesoModuloMedicos.explRecetaMedica;
            historiasClinicasToolStripMenuItem.Enabled = His.Parametros.AccesoModuloMedicos.ExpHc;
            certificadoDeAsistenciaToolStripMenuItem.Enabled = His.Parametros.AccesoModuloMedicos.ExpCertificadoAsistencia;

            toolStripMenuItem1.Enabled = His.Parametros.AccesoModuloMedicos.Medicos;
            certificadoMedicoIESSToolStripMenuItem.Enabled = His.Parametros.AccesoModuloMedicos.certificadoCovid;
            certificadosToolStripMenuItem.Enabled = His.Parametros.AccesoModuloMedicos.CertGeneral;
            certificadoDePresentacionToolStripMenuItem.Enabled = His.Parametros.AccesoModuloMedicos.CertAsistencia;
            recetaMédicaToolStripMenuItem.Enabled = His.Parametros.AccesoModuloMedicos.certificadoRrecetaMedica;
        }
        #endregion

        private void ultraTabbedMdiManager1_InitializeTab(object sender, Infragistics.Win.UltraWinTabbedMdi.MdiTabEventArgs e)
        {

        }
        //CAMBIOS EDGAR RAMOS
        #region Metodo Formulario
        //cambios aplicados por Edgar... para que el usuario al dar click en un formulario este se vuelva abrir de nuevo
        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            formulario = System.Windows.Forms.Application.OpenForms.Cast<MiForm>().FirstOrDefault();//Busca en la colecion el formulario
                                                                                                    //si el formulario/instancia no existe
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.Show();
                formulario.BringToFront();
            }
            //si el formulario/instancia existe
            else
            {
                formulario.BringToFront();
            }
        }
        #endregion

        private void certificadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Certificados x = new frm_Certificados();
            x.MdiParent = this;
            x.Show();
        }

        private void recetaMédicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NegParametros.ParametroReceta())
            {
                frmRecetaNew x = new frmRecetaNew();
                x.MdiParent = this;
                x.Show();
            }
            else
            {
                His.Formulario.frm_RecetaMedica x = new His.Formulario.frm_RecetaMedica();
                x.MdiParent = this;
                x.Show();
            }

        }

        private void certificadoMédicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ExploradorCertificado x = new frm_ExploradorCertificado();
            x.MdiParent = this;
            x.Show();
        }

        private void historiasClinicasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ExploradorFormularios x = new frm_ExploradorFormularios();
            x.MdiParent = this;
            x.Show();
        }

        private void recetaMédicaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmExploradorReceta x = new frmExploradorReceta();
            x.MdiParent = this;
            x.Show();
        }

        private void exploradorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void certificadoMedicoIESSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frm_CertificadoIESS frm = new frm_CertificadoIESS();
                frm.MdiParent = this;
                if (frm.abre)
                    frm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Ud. No tiene acceso a generar certificados medicos ya que no esta registrado como un usuario medico", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void certificadoDePresentacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frm_CertificadoPresentacion frm = new frm_CertificadoPresentacion();
                frm.MdiParent = this;
                if (frm.abre)
                    frm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Ud. No tiene acceso a generar certificados medicos ya que no esta registrado como un usuario medico", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void certificadoDeAsistenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ExploradorCertificadoAsistencia frm = new frm_ExploradorCertificadoAsistencia();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
