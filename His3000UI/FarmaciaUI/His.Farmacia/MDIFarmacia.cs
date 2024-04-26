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
//using His.AdmisionWPFUI;

namespace His.Farmacia
{
    public partial class MDIFarmacia : Form
    {
        #region Variables
        private int childFormNumber = 0;
        private DtoParametros parametrosGenerales = new DtoParametros();
        #endregion

        #region Constructor
        public MDIFarmacia()
        {
            InitializeComponent();
            //InicializarForma();
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
        private void DeshabilitarMenu()
        {
            //mnu_admision.Enabled = false;
            //mnu_mantenimiento.Enabled = false;
            //mnu_informacionmorbimortalidad.Enabled = false;
            //mnu_Explorador.Enabled = false;
            //mnu_Explorador.Enabled = false; 
        }
        private void InicializarForma()
        {
            //DeshabilitarMenu();
            ////cargo usuario
            //if (Sesion.codUsuario == 0)
            //{
            //    try
            //    {
            //        His.Parametros.ArchivoIni archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
            //        archivo.IniReadValue("Usuario", "usr");
            //        USUARIOS usuarioarchivo = NegUsuarios.RecuperaUsuarios().Where(usu => usu.USR == archivo.IniReadValue("Usuario", "usr")).FirstOrDefault();
            //        //archivo.IniWriteValue("Usuario", "usr", usuario.USR);
            //        //archivo.IniWriteValue("Usuario", "pwd", usuario.PWD); 
            //        if (usuarioarchivo != null)
            //        {
            //            Sesion.codUsuario = usuarioarchivo.ID_USUARIO;
            //            Sesion.nomUsuario = usuarioarchivo.NOMBRES + " " + usuarioarchivo.APELLIDOS;
            //        }
            //        else
            //        {

            //            System.Windows.Forms.MessageBox.Show("El usuario no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //        }
            //    }
            //    catch (Exception err)
            //    {
            //        System.Windows.Forms.MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //Sesion.TipoAcceso = His.Entidades.Acceso.DIRECTO;

            ////txtFecha.Text = "FECHA: " + DateTime.Now.ToString("D");

            ////timHora.Start();
            
            //if (Sesion.codUsuario != 0)
            //{
            //    USUARIOS usuario = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
            //    PERFILES perfilUsuario = new NegPerfil().RecuperarPerfil(usuario.ID_USUARIO);
            //    NegAccesoOpciones.asignarAccesosPorPerfil(perfilUsuario.ID_PERFIL);

            //    smnu_formularioshcu.Enabled = His.Parametros.AccesosModuloAdmision.FormulariosHCU;
            //    mnu_Explorador.Enabled = His.Parametros.AccesosModuloAdmision.ExploradorPacientes;
            //    mnu_admision.Enabled = His.Parametros.AccesosModuloAdmision.Admision;
            //    mnu_mantenimiento.Enabled = His.Parametros.AccesosModuloAdmision.Mantenimiento;
            //    mnu_informacionmorbimortalidad.Enabled = His.Parametros.AccesosModuloAdmision.InfMorbimortalidad;

            //    //if (usuario.NOMBRES == usuario.APELLIDOS)
            //    //{
            //    //    txtNombres.Text = "Usr. " + string.Format("{0} ",
            //    //        usuario.NOMBRES);
            //    //}
            //    //else
            //    //{
            //    //    txtNombres.Text = "Usr. " + string.Format("{0} {1}",
            //    //        usuario.NOMBRES,
            //    //        usuario.APELLIDOS);
            //    //}
            //}

            ////EMPRESA empresa = NegEmpresa.RecuperaEmpresa();

            ////txtEmpresa.Text = "Empresa " + string.Format("{0} ", empresa.EMP_NOMBRE);

        }
        #endregion

        private void MDI_Admision_Load(object sender, EventArgs e)
        {

        }

        private void smnu_explorapacientes_Click(object sender, EventArgs e)
        {
            
        }

        private void mnu_admision_Click(object sender, EventArgs e)
        {
            Form frm = new frm_RealizarPedidos();
            frm.MdiParent = this;
            frm.Show();
        }

        private void habitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void smnu_expHabitaciones_Click(object sender, EventArgs e)
        {
            
        }

        private void pruebaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void exploradorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void evolucionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void anamnesisToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void epicrisisToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void protocoloToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void laboratorioClinicoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mnu_mantenimiento_Click(object sender, EventArgs e)
        {

        }

        private void exploradorToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frm_ExploradorPedidos frm = new frm_ExploradorPedidos();
            frm.MdiParent = this;
            frm.Show();
        }



        
        

        











    }
}
