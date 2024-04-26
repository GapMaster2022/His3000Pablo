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
using His.Maintenance;

//using His.AdmisionWPFUI;

namespace His.Maintenance
{
    public partial class MDI_Maintenance : Form
    {
        #region Variables
        private int childFormNumber = 0;
        private DtoParametros parametrosGenerales = new DtoParametros();
        #endregion

        #region Constructor
        public MDI_Maintenance()
        {
            InitializeComponent();

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

                mnu_archivo.Enabled = His.Parametros.AccesosModuloMantenimiento.Archivo;
                toolStripMenuItem1.Enabled = His.Parametros.AccesosModuloMantenimiento.Empresas;
                toolStripMenuItem2.Enabled = His.Parametros.AccesosModuloMantenimiento.ZonasLocales;
                toolStripMenuItem3.Enabled = His.Parametros.AccesosModuloMantenimiento.Departamento;
                toolStripMenuItem4.Enabled = His.Parametros.AccesosModuloMantenimiento.Cajas;

                preciosYPorcentajesConveniosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.PreciosPorcentaje;
                tiposDeCostosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.TipoCosto;
                catalogoDeCostosToolStripMenuItem1.Enabled = His.Parametros.AccesosModuloMantenimiento.CatalogoCosto;
                conveniosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.Convenios;
                preciosPorConvenioToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.PrecioConvenio;

                mantenimientoDeTablasToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.MantenimientoTablas;
                divisionPoliticaToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.DivisionPolitica;
                Nacionalidadmenu.Enabled = His.Parametros.AccesosModuloMantenimiento.Nacionalidad;
                bancosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.Bancos;
                tipoDeNegocioToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.TipoNegocio;
                toolStripMenuItem5.Enabled = His.Parametros.AccesosModuloMantenimiento.TipoMedico;
                toolStripMenuItem6.Enabled = His.Parametros.AccesosModuloMantenimiento.NumeroControl;
                tiposDeAtencionesToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.TipoAtenciones;
                tiposDeIngresosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.TipoIngreso;
                tiposDeCiudadanosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.TipoCiudadano;
                pisoMaquinaToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.PisoMaquina;
                pisoToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.Piso;
                productosSubToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.GrupoProductos;
                habitacionesToolStripMenuItem1.Enabled = His.Parametros.AccesosModuloMantenimiento.Habitaciones;
                cambioTipoAtenciónToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.CambioAtencion;
                honorariosConsultaExternaToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.HonorarioCEX;

                seguridadesToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.Seguridades;
                usuariosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.Usuarios;
                perfilesToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.Perfiles;
                permisosUsuariosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.ExpUsuariosAccesos;

                exploradoresToolStripMenuItem.Enabled = His.Parametros.AccesosModuloMantenimiento.Exploradores;
                consultasWebToolStripMenuItem1.Enabled = His.Parametros.AccesosModuloMantenimiento.ConsultasSRI;
                preIngresosToolStripMenuItem1.Enabled = His.Parametros.AccesosModuloMantenimiento.PreIngresos;
                
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

        private void catalogoDeCostosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmCatalogoCostos window = new frmCatalogoCostos();
            window.MdiParent = this;
            window.Show();
        }

        private void tiposDeCostosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTipoCostos window = new frmTipoCostos();
            window.MdiParent = this;
            window.Show();
        }

        private void tipoDeNegocioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_TipoNegocio window = new frm_TipoNegocio();
            window.MdiParent = this;
            window.Show();
        }

        private void divisionPoliticaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDivisionPolitica window = new frmDivisionPolitica();
            window.MdiParent = this;
            window.Show();
        }

        private void bancosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBancos window = new frmBancos();
            window.MdiParent = this;
            window.Show();
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            frm_Empresa window = new frm_Empresa();
            window.MdiParent = this;
            window.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frm_Zona window = new frm_Zona();
            window.MdiParent = this;
            window.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frm_Departamentos window = new frm_Departamentos();
            window.MdiParent = this;
            window.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frm_Cajas window = new frm_Cajas();
            window.MdiParent = this;
            window.Show();
        }

        private void conveniosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frm_CategoriasConvenios window = new frm_CategoriasConvenios();
            window.MdiParent = this;
            window.Show();
        }

        private void preciosPorConvenioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_PreciosConvenios window = new frm_PreciosConvenios();
            window.MdiParent = this;
            window.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            frm_TipoMedico window = new frm_TipoMedico();
            window.MdiParent = this;
            window.Show();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            frm_NumeroControl window = new frm_NumeroControl();
            window.MdiParent = this;
            window.Show();
        }

        private void catalogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Catalogos window = new frm_Catalogos();
            window.MdiParent = this;
            window.Show();
        }

        private void areasDePedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_PedidosCuenta window = new frm_PedidosCuenta();
            window.MdiParent = this;
            window.Show();
        }

        private void estacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Estaciones window = new frm_Estaciones();
            window.MdiParent = this;
            window.Show();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Usuario window = new frm_Usuario();
            window.MdiParent = this;
            window.Show();
        }

        private void perfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCreacionPerfiles window = new frmCreacionPerfiles();
            //frm_Perfiles window = new frm_Perfiles();
            window.MdiParent = this;
            window.Show();
        }

        private void opcionesDeAccesosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_OpcionesAcceso window = new frm_OpcionesAcceso();
            window.MdiParent = this;
            window.Show();
        }

        private void permisoCambiosDeEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_PermisosCambioEstadosCuentas window = new frm_PermisosCambioEstadosCuentas();
            window.MdiParent = this;
            window.Show();
        }

        private void parametrosDelSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form window = new frmParametros();
            window.MdiParent = this;
            window.Show();
        }

        private void tiposDeAtencionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTiposAtenciones x = new frmTiposAtenciones();
            x.MdiParent = this;
            x.Show();
        }

        private void tiposDeIngresosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_TipoIngreso x = new frm_TipoIngreso();
            x.MdiParent = this;
            x.Show();
        }

        private void tiposDeCiudadanosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Ciudadanos x = new frm_Ciudadanos();
            x.MdiParent = this;
            x.Show();
        }

        private void Nacionalidadmenu_Click(object sender, EventArgs e)
        {
            frm_Nacionalidad x = new frm_Nacionalidad();
            x.MdiParent = this;
            x.Show();
        }

        private void pisoMaquinaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPisoMaquina x = new frmPisoMaquina();
            x.MdiParent = this;
            x.Show();
        }

        private void pisoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNivelPiso x = new frmNivelPiso();
            x.MdiParent = this;
            x.Show();
        }

        private void productosSubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ProductosSub frm = new Frm_ProductosSub();
            frm.MdiParent = this;
            frm.Show();
        }

        private void generarUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_CreaUsuarios frm = new Frm_CreaUsuarios();
            frm.MdiParent = this;
            frm.Show();
        }

        private void habitacionesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Habitaciones frm = new Habitaciones();
            frm.MdiParent = this;
            frm.Show();
        }

        private void accesosSic3000ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_AccesosSic frm = new frm_AccesosSic();
            frm.MdiParent = this;
            frm.Show();
        }

        private void accesosCg3000ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_AccesosCg frm = new frm_AccesosCg();
            frm.MdiParent = this;
            frm.Show();
        }

        private void cambioTipoAtenciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_CambioTipoAtencion frm = new frm_CambioTipoAtencion();
            frm.MdiParent = this;
            frm.Show();
        }

        private void permisosUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ExploradorUsrAcc frm = new frm_ExploradorUsrAcc();
            frm.MdiParent = this;
            frm.Show();
        }

        private void consultasWebToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            His.Admision.frmEcploradorConsultasWeb frm = new Admision.frmEcploradorConsultasWeb();
            frm.MdiParent = this;
            frm.Show();
        }

        private void preIngresosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            His.Admision.frmExploradorPreAdmision frm = new Admision.frmExploradorPreAdmision();
            frm.MdiParent = this;
            frm.Show();
        }

        private void honorariosConsultaExternaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_HonorarioConsultaExterna frm = new frm_HonorarioConsultaExterna();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
