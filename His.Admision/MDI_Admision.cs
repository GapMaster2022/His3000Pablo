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
using His.Formulario;
using System.Windows.Forms.Integration;
using Core.Utilitarios;
using MessageBox = System.Windows.MessageBox;
using His.Dietetica;

//using His.AdmisionWPFUI;

namespace His.Admision
{
    public partial class MDI_Admision : Form
    {
        #region Variables
        private int childFormNumber = 0;
        private DtoParametros parametrosGenerales = new DtoParametros();
        #endregion

        #region Constructor
        public MDI_Admision()
        {
            InitializeComponent();
            if (!NegUtilitarios.IpBodegas())
            {
                MessageBox.Show("Está maquina no tiene asignada una bodega, por lo que no se puede abrir el modulo", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
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
            mnu_admision.Enabled = false;
            admisiónEmergenciaToolStripMenuItem.Enabled = false;
            //mnu_informacionmorbimortalidad.Enabled = false;
           // mnu_Explorador.Enabled = false;
            //mnu_Explorador.Enabled = false; 
        }
        private void InicializarForma()
        {
            DeshabilitarMenu();
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
                //Se cambia para utilizar nuevos parametros para seguridades //Mario Valencia // 30/10/2023
                //smnu_formularioshcu.Enabled = His.Parametros.AccesosModuloAdmision.FormulariosHCU;
                //mnu_Explorador.Enabled = His.Parametros.AccesosModuloAdmision.ExploradorPacientes;
                //mnu_admision.Enabled = His.Parametros.AccesosModuloAdmision.Admision;
                //mnu_admisionEmergencia.Enabled = His.Parametros.AccesosModuloAdmision.AdmisionEmergencia;   
                ////mnuEmergencias.Enabled = His.Parametros.AccesosModuloAdmision.Mantenimiento;
                //mnu_informacionmorbimortalidad.Enabled = His.Parametros.AccesosModuloAdmision.InfMorbimortalidad;
                //formulariosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.FormulariosHCU;
                mnu_archivo.Enabled = His.Parametros.AccesosModuloAdmision.Archivo;
                checkListHCToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.Check;
                smnu_formularioshcu.Enabled = His.Parametros.AccesosModuloAdmision.Formulario;
                smnu_salir.Enabled = His.Parametros.AccesosModuloAdmision.Salir;
                mnu_admision.Enabled = His.Parametros.AccesosModuloAdmision.Admision;
                mnu_admisionEmergencia.Enabled = His.Parametros.AccesosModuloAdmision.AdmEmergenciaM;
                admisiónEmergenciaToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.AdmEmergencia;
                procedimientosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.ServiciosExtermos;
                preAdmisiónToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.PreIngreso;
                estadisticaToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.Estadistica;
                controlHistoriasCliniasCheckListToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.ControlHc;
                mnu_Explorador.Enabled = His.Parametros.AccesosModuloAdmision.Explorador;
                pacientesToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.Pacientes;
                atencionesToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.Atenciones;
                cuentasPorFacturarToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.CuentaFacturada;
                habitacionesToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.Habitaciones;
                formulariosToolStripMenuItem1.Enabled = His.Parametros.AccesosModuloAdmision.Hc;
                rubrosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.Rubros;
                exploradorProcedimientoToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.ExplProcedimientos;
                exploradorDeRubrosProcedimientoToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.ExpProcRubros;
                reportesToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.Reportes;
                mnu_informacionmorbimortalidad.Enabled = His.Parametros.AccesosModuloAdmision.Laboratorio;
                tarifarioIESSToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.Tarifario;
                garantiasToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.Garantias;
                iNENToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.Inec;
                censoDiarioToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.CensoDiario;
                censoDiarioSxEToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.CensoSxe;
                solicitudDeHCToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.SolicitudHc;
                cierreDeTurnoToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.CierreTurno;
                rangoDeEdadesToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.RangoEdades;
                defuncionesToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.Defunciones;
                rUCCIToolStripMenuItem.Enabled = His.Parametros.AccesosModuloAdmision.RucCi;

                ultraStatusBarTarifario.Panels["empresa"].Text = Sesion.nomEmpresa;
                ultraStatusBarTarifario.Panels["usuario"].Text = Sesion.nomUsuario;
                //Se comenta para utilizar nuevos parametros para seguridades //Mario Valencia // 30/10/2023
                //if (NegParametros.ParametroAdmisionAcceso())
                //    ValidacionUsuario(perfilUsuario);
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
            // this.ultraStatusBarTarifario.Panels.Add("Usuario", Text)="adasd";

        }
        public void ValidacionUsuario(List<PERFILES> perfiles)
        {
            DeshabilitarMenu();
            foreach (var item in perfiles)
            {
                if (item.ID_PERFIL == 1)
                {
                    mnu_admision.Enabled = true;
                    mnu_admisionEmergencia.Enabled = true;
                    admisiónEmergenciaToolStripMenuItem.Enabled = true;
                    break;
                }
                if (item.ID_PERFIL == 6)
                    mnu_admision.Enabled = true;

                if (item.ID_PERFIL == 14)
                    admisiónEmergenciaToolStripMenuItem.Enabled = true;


            }
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
            //this.BackgroundImage = Recursos.Archivo.logoEmpresa;
        }

        private void smnu_explorapacientes_Click(object sender, EventArgs e)
        {
            
        }

        private void mnu_admision_Click(object sender, EventArgs e)
        {
            Form frm = new frm_Admision();
            frm.MdiParent = this;
            frm.Show();
        }

        private void habitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frm_Habitaciones();
            frm.MdiParent = this;
            frm.Show(); 
        }

        private void smnu_expHabitaciones_Click(object sender, EventArgs e)
        {
            
        }

        private void pruebaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frm_FormulariosHCU();
            //frm.MdiParent = this;
            frm.Show();
        }

        private void exploradorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void evolucionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            His.Formulario.frm_Evolucion frm = new His.Formulario.frm_Evolucion();
            frm.Show();
        }

        private void anamnesisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void epicrisisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            His.Formulario.frm_Epicrisis frm = new His.Formulario.frm_Epicrisis();
            frm.Show();
        }

        private void protocoloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            His.Formulario.frm_Protocolo frm = new His.Formulario.frm_Protocolo();
            frm.Show();
        }

        private void laboratorioClinicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            His.Formulario.frm_LaboratorioClinico frm = new His.Formulario.frm_LaboratorioClinico();
            frm.Show();
        }

        private void estadisticasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ingresToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pacientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frm_ExploraPacientes();
            frm.MdiParent = this;
            frm.Show();
        }

        private void atencionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var ingresos = new frm_ExploradorIngresos {MdiParent = this};
                ingresos.Show();  
            }
            catch (Exception err)
            {
                MessageBox.Show("Error",err.Message) ;
            }
        }

        private void admisiónEmergenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmAdmisionEmergencia emergencia = new frmAdmisionEmergencia();
            //emergencia.MdiParent = this;
            //emergencia.Show();  
        }

        private void pacientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new FrmLaboratorio();
            frm.MdiParent = this;
            frm.Show();
            
        }

        private void tarifarioIESSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new FrmTarifario();
            frm.MdiParent = this;
            frm.Show();
        }

        private void odontologiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frm_Odontograma();
            frm.MdiParent = this;
            frm.Show();
        }

        private void habitacionesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //frm_ExploradorHabitaciones habitaciones = new frm_ExploradorHabitaciones();
            //habitaciones.MdiParent = this;
            //habitaciones.Show();

            frmHistorialHabitaciones habitaciones = new frmHistorialHabitaciones();
            habitaciones.MdiParent = this;
            habitaciones.Show();
        }

        private void historialHabitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHistorialHabitaciones habitaciones = new frmHistorialHabitaciones();
            habitaciones.MdiParent = this;
            habitaciones.Show(); 
        }

        private void admisiónEmergenciaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmAdmisionEmergencia emergencia = new frmAdmisionEmergencia();
            emergencia.MdiParent = this;
            emergencia.Show();  
        }

        private void procedimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmEmergenciaProcedimientos Procedimientos = new frmEmergenciaProcedimientos();
            Procedimientos.MdiParent = this;
            Procedimientos.Show();  

        }

        private void garantiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportaGarantias form = new frmReportaGarantias();
            form.Show();
        }

        private void formulariosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            His.Honorarios.frm_ExploradorFormularios Procedimientos = new Honorarios.frm_ExploradorFormularios();
            //frm_ExploradorFormularios Procedimientos = new frm_ExploradorFormularios();
            Procedimientos.MdiParent = this;
            Procedimientos.Show();
        }

        private void iNENToolStripMenuItem_Click(object sender, EventArgs e)
        {
            INEN Procedimientos = new INEN();
            Procedimientos.MdiParent = this;
            Procedimientos.Show();
        }

        private void rubrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ExploradorRubros Procedimientos = new frm_ExploradorRubros();
            Procedimientos.MdiParent = this;
            Procedimientos.Show();
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            His.Garantia.Form1 Procedimientos = new His.Garantia.Form1();
            Procedimientos.MdiParent = this;
            Procedimientos.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            His.Garantia.frmPreAutorizacion Procedimientos = new His.Garantia.frmPreAutorizacion();
            Procedimientos.MdiParent = this;
            Procedimientos.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            His.Garantia.frmReporteGarantia Procedimientos = new His.Garantia.frmReporteGarantia();
            Procedimientos.MdiParent = this;
            Procedimientos.Show();
        }

        private void solicitudDeHCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
               
                //llamo al reporte
                frmReportes form = new frmReportes();
                form.reporte = "Copias";
                form.Show();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error");
            }
        }

        private void censoDiarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_CensoDiario x = new frm_CensoDiario();
            x.MdiParent = this;
            x.Show();
        }

        private void defuncionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReporteDefunciones x = new frmReporteDefunciones();
            x.MdiParent = this;
            x.Show();
        }

        private void cierreDeTurnoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_CierreAdmisiones x = new frm_CierreAdmisiones();
            x.MdiParent = this;
            x.Show();
        }

        private void checkListHCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCheckListHc x = new frmCheckListHc();
            x.Show();
        }

        private void controlHistoriasCliniasCheckListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExploradorControlHC x = new frmExploradorControlHC();
            x.MdiParent = this;
            x.Show();
        }

        private void rangoDeEdadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            His.Formulario.frm_Ingresos_Hospitalarios_Edades x = new frm_Ingresos_Hospitalarios_Edades();
            x.MdiParent = this;
            x.Show();
        }

        private void cuentasPorFacturarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExploradorCuentaxFacturar x = new frmExploradorCuentaxFacturar();
            x.MdiParent = this;
            x.Show();
        }

        private void censoDiarioSxEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCensoDiarioSxE x = new frmCensoDiarioSxE();
            x.MdiParent = this;
            x.Show();
        }

        private void preAdmisiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPreAdmision x = new frmPreAdmision();
            x.ShowDialog();
        }

        private void rUCCIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new RUC___CI();
            frm.MdiParent = this;
            frm.Show();
        }

        private void consultasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEcploradorConsultasWeb frm = new frmEcploradorConsultasWeb();
            frm.MdiParent = this;
            frm.Show();
        }

        private void preIngresosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExploradorPreAdmision frm = new frmExploradorPreAdmision();
            frm.MdiParent = this;
            frm.Show();
        }

        private void exploradorProcedimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExploradorProcedimiento frm = new frmExploradorProcedimiento();
            frm.MdiParent = this;
            frm.Show();
        }

        private void exploradorDeRubrosProcedimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExploradorRubrosProcedimiento frm = new frmExploradorRubrosProcedimiento();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
