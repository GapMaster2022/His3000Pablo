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
            InicializarForma();
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

        private void mnuDiet_Click(object sender, EventArgs e)
        {

        }

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
            frmQuirofanoAgregarProcedimiento x = new frmQuirofanoAgregarProcedimiento();
            x.MdiParent = this;
            x.Show();
        }

        private void agregarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuirofanoAgregarProducto x = new frmQuirofanoAgregarProducto();
            x.MdiParent = this;
            x.Show();
        }

        private void pedidoPacienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuirofanoExplorador x = new frmQuirofanoExplorador();
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
    }
}
