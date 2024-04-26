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
using His.Formulario;


namespace His.Emergencia
{
    public partial class MDI_Emergencia : Form
    {
        #region Variables
        private int childFormNumber = 0;
        private DtoParametros parametrosGenerales = new DtoParametros();
        #endregion      

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>


        public MDI_Emergencia()
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

        private void timHora_Tick(object sender, EventArgs e)
        {
            txtHora.Text = "HORA: " + DateTime.Now.ToString("T");
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
        private void smnu_salir_Click(object sender, EventArgs e)
        {
            this.Close();
            //Application.Exit();
        }

        # endregion

        # region Metodos Privados

        private void DeshabilitarMenu()
        {

            //mnu_archivo.Enabled = false;
            //mnu_balances.Enabled = false;
            //mnu_estadisticas.Enabled = false;
            //mnu_pagos.Enabled = false;
            //mnu_procesosdiarios.Enabled = false;
            //mnu_reportescontables.Enabled = false;
            //mnu_transferencias.Enabled = false;
            //mnu_estadisticas.Enabled = false;
            //smnu_medicos.Enabled = false;

        }

        private void InicializarForma()
        {
            //DeshabilitarMenu();
            //cargo usuario
            if (Sesion.codUsuario == 0)
            {
                try
                {
                    His.Parametros.ArchivoIni archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
                    archivo.IniReadValue("Usuario", "usr");
                    USUARIOS usuarioarchivo = NegUsuarios.RecuperaUsuarios().Where(usu => usu.USR == archivo.IniReadValue("Usuario", "usr")).FirstOrDefault();
                    //archivo.IniWriteValue("Usuario", "usr", usuario.USR);
                    //archivo.IniWriteValue("Usuario", "pwd", usuario.PWD); 
                    if (usuarioarchivo != null)
                    {
                        Sesion.codUsuario = usuarioarchivo.ID_USUARIO;
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
            // cargo iva
            parametrosGenerales = NegParametros.RecuperaParametros(1).Where(cod => cod.PAD_CODIGO == 1).FirstOrDefault();
            Sesion.porIva = decimal.Parse(parametrosGenerales.PAD_VALOR);

            Sesion.TipoAcceso = His.Entidades.Acceso.DIRECTO;
            #region codigoAntiguo no estaba en funcionamineto Mario  06/11/2023 
            //List<ACCESO_OPCIONES> perfilesAccesos = new NegPerfilesAcceso().AccesosUsuarios(Sesion.codUsuario, Constantes.ADMISION);
            //foreach (var acceso in perfilesAccesos)
            //{
            //    //if (acceso.ID_ACCESO == 200002)
            //    //    mnu_archivo.Enabled = true;

            //    //if (acceso.ID_ACCESO == 200003)
            //    //    mnu_procesosdiarios.Enabled = true;

            //    ////if (acceso.ID_ACCESO == 200004)
            //    ////    mnu_estadocuenta.Enabled = true;

            //    //if (acceso.ID_ACCESO == 200005)
            //    //    mnu_pagos.Enabled = true;

            //    //if (acceso.ID_ACCESO == 200006)
            //    //    mnu_reportescontables.Enabled = true;

            //    //if (acceso.ID_ACCESO == 200007)
            //    //    mnu_balances.Enabled = true;

            //    //if (acceso.ID_ACCESO == 200008)
            //    //    mnu_estadisticas.Enabled = true;

            //    //if (acceso.ID_ACCESO == 200009)
            //    //    mnu_transferencias.Enabled = true;

            //    //if (acceso.ID_ACCESO == 200010)
            //    //    smnu_medicos.Enabled = true;
            //}
            #endregion
            #region codigo actual
            if (Sesion.codUsuario != 0)
            {
                USUARIOS usuario1 = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);

                List<PERFILES> perfilUsuario = new NegPerfil().RecuperarPerfil(usuario1.ID_USUARIO);

                if (perfilUsuario != null)
                {
                    foreach (var Perfil in perfilUsuario)
                    {
                        if (perfilUsuario != null)
                            NegAccesoOpciones.asignarAccesosPorPerfil(Perfil.ID_PERFIL);
                        else
                        {
                            //System.Windows.MessageBox.Show("No se asignado un perfil al usuario", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                            this.Close();
                        }
                    }
                }
            }
            mnu_admision.Enabled = His.Parametros.AccesosModuloEmergencia.Emergencia;
            triajeYSignosVitalesToolStripMenuItem.Enabled = His.Parametros.AccesosModuloEmergencia.Triaje;
            ingresoFormO8ToolStripMenuItem.Enabled = His.Parametros.AccesosModuloEmergencia.Formulario;
            evoluciónMedicosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloEmergencia.Evolucion;
            certificadoMedicoToolStripMenuItem.Enabled = His.Parametros.AccesosModuloEmergencia.Certificado;
            recetaMedicaToolStripMenuItem.Enabled = His.Parametros.AccesosModuloEmergencia.Receta;
            #endregion

            txtFecha.Text = "FECHA: " + DateTime.Now.ToString("D");

            timHora.Start();
            USUARIOS usuario = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);

            if (usuario.NOMBRES == usuario.APELLIDOS)
            {
                txtNombres.Text = "Usr. " + string.Format("{0} ",
                    usuario.NOMBRES);
            }
            else
            {
                txtNombres.Text = "Usr. " + string.Format("{0} {1}",
                    usuario.NOMBRES,
                    usuario.APELLIDOS);
            }

            EMPRESA empresa = NegEmpresa.RecuperaEmpresa();

            txtEmpresa.Text = "Empresa " + string.Format("{0} ", empresa.EMP_NOMBRE);

        }

        #endregion

        private void registroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frm_Registro();
            frm.MdiParent = this;
            frm.Show();
        }

        private void ingresoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frm_Ingreso();
            frm.Show();
        }

        private void MDI_Emergencia_Load(object sender, EventArgs e)
        {
            if (!NegUtilitarios.IpBodegas())
            {
                this.Close();
                return;
            }
            try
            {
                //cargo imagen pro defecto
                //this.BackgroundImage = Recursos.Archivo.logoEmpresa;
                this.Icon = Recursos.Archivo.IconoTarifario;
                //cargo la barra de estado
                ////ultraStatusBarTarifario.Panels["empresa"].Text = Sesion.nomEmpresa;
                ////ultraStatusBarTarifario.Panels["usuario"].Text = Sesion.nomUsuario;
                if (Sesion.codDepartamento != 6)
                {
                    médicosToolStripMenuItem.Enabled = true;
                }
                else
                {
                    médicosToolStripMenuItem.Enabled = false;
                    //MessageBox.Show("Opción Solo para personal Médico", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void toolDistribucionSexo_Click(object sender, EventArgs e)
        {
            frm_InformeMorbilidad form = new frm_InformeMorbilidad();
            form.Show();
        }

        private void smnu_explorapacientes_Click(object sender, EventArgs e)
        {

        }

        private void mnu_informacionmorbimortalidad_Click(object sender, EventArgs e)
        {
            //frm_InformeMorbilidad form = new frm_InformeMorbilidad();
            Form form = new frm_InformeMorbilidad();
            form.MdiParent = this;
            form.Show();
        }

        private void ingresoFormO8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frm_EmergenciaNew();
            form.MdiParent = this;
            form.Show();
        }

        private void triajeYSignosVitalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Triaje triaje = new frm_Triaje();
            triaje.MdiParent = this;
            triaje.Show();
        }

        private void evoluciónMédicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Formulario.frm_Evolucion x = new frm_Evolucion();
            x.ShowDialog();

        }

        private void epicrisisYTrasferenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Epicrisis epicrisis = new frm_Epicrisis();
            epicrisis.ShowDialog();
        }

        private void certificadoMédicoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frm_Certificados certificado = new frm_Certificados();
            certificado.MdiParent = this;
            certificado.Show();
        }

        private void recetaMédicaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (NegParametros.ParametroReceta())
            {
                His.Formulario.frmRecetaNew x = new frmRecetaNew();
                x.MdiParent = this;
                x.Show();
            }
            else
            {
                frm_RecetaMedica receta = new frm_RecetaMedica();
                receta.MdiParent = this;
                receta.Show();
            }

        }
        public bool abrir = true;
        private void evoluciónMedicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Formulario.frm_Evolucion x = new frm_Evolucion();
            x.Show();
            if (x.cerrar)
                x.Close();
        }

        private void certificadoMedicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //frm_Certificados certificado = new frm_Certificados();
                frm_CertificadoIESS frm = new frm_CertificadoIESS();
                frm.MdiParent = this;
                frm.Pacientes = true;
                if (frm.abre)
                    frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ud. No tiene acceso a generar certificados medicos ya que no esta registrado como un usuario medico", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void recetaMedicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NegParametros.ParametroReceta())
            {
                His.Formulario.frmRecetaNew x = new frmRecetaNew();
                x.MdiParent = this;
                x.Show();
            }
            else
            {
                frm_RecetaMedica receta = new frm_RecetaMedica();
                receta.MdiParent = this;
                receta.Show();
            }
        }
    }
}
