using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades.Clases;
using His.Negocio;
using His.Entidades;
using His.Parametros;

namespace TarifariosUI
{
    public partial class frmMainTarifario : Form
    {
        int codUsuario = 0;
        public frmMainTarifario()
        {
            InitializeComponent();
            InicializarForma();
            if (!NegUtilitarios.IpBodegas())
            {
                MessageBox.Show("Está maquina no tiene asignada una bodega, por lo que no se puede abrir el modulo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void tarifarioToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAdministrarTarifario adminTarifario = new frmAdministrarTarifario();
            adminTarifario.MdiParent = this;
            adminTarifario.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void consultaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmBusquedaTarifario consulta = new frmBusquedaTarifario();
            consulta.MdiParent = this;
            consulta.Show();
        }

        private void aseguradorasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdministrarAseguradoras empresa = new frmAdministrarAseguradoras();
            empresa.MdiParent = this;
            empresa.Show();
        }

        private void conveniosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdministrarEstructura estructura = new frmAdministrarEstructura();
            estructura.MdiParent = this;
            estructura.Show();
        }

        private void aseguradorasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAdministrarAseguradoras aseguradoras = new frmAdministrarAseguradoras();
            aseguradoras.MdiParent = this;
            aseguradoras.Show();
        }

        private void conveniosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAdministrarConvenios convenios = new frmAdministrarConvenios();
            convenios.MdiParent = this;
            convenios.Show();
        }

        private void creacionDeHonorariosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmCrearHonorarios honorarios = new frmCrearHonorarios(0, 0, codUsuario);
                honorarios.MdiParent = this;
                honorarios.Show();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void tipoEmpresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdministrarTipoEmpresa tipoEmpresas = new frmAdministrarTipoEmpresa();
            tipoEmpresas.MdiParent = this;
            tipoEmpresas.Show();
        }

        private void empresasYAseguradorasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportes form = new frmReportes(0);
            form.reporte = "aseguradoras";
            form.Show();
        }

        private void honorariosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void frmMainTarifario_Load(object sender, EventArgs e)
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

                ultraStatusBarTarifario.Panels["empresa"].Text = Sesion.nomEmpresa;
                ultraStatusBarTarifario.Panels["usuario"].Text = Sesion.nomUsuario;
                codUsuario = Sesion.codUsuario;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmControlesWPF acerca = new frmControlesWPF();
            acerca.ShowDialog();
        }

        private void DeshabilitarMenu()
        {
            tipoEmpresaToolStripMenuItem.Enabled = false;
            aseguradorasToolStripMenuItem1.Enabled = false;
            conveniosToolStripMenuItem1.Enabled = false;
            estructuraEspToolStripMenuItem.Enabled = false;
            tarifarioToolStripMenuItem1.Enabled = false;
            procedimientosToolStripMenuItem.Enabled = false;
            creacionDeHonorariosToolStripMenuItem.Enabled = false;
            reportesToolStripMenuItem.Enabled = false;
        }
        private void InicializarForma()
        {
            try
            {
                DeshabilitarMenu();
                //cargo usuario
                if (Sesion.codUsuario == 0)
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
                Sesion.TipoAcceso = His.Entidades.Acceso.DIRECTO;

                //txtFecha.Text = "FECHA: " + DateTime.Now.ToString("D");

                //timHora.Start();
                //Sesion.codUsuario=1;
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

                    //archivoToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.Ayuda;
                    archivoToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.Administracion;
                    tipoEmpresaToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.TipoEmpresa;
                    aseguradorasToolStripMenuItem1.Enabled = His.Parametros.AccesosModuloTarifario.AseguradoraEmpresa;
                    conveniosToolStripMenuItem1.Enabled = His.Parametros.AccesosModuloTarifario.Convenio;
                    estructuraEspToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.EstructuraEspecialidades;
                    tarifarioToolStripMenuItem1.Enabled = His.Parametros.AccesosModuloTarifario.Tarifario;
                    procedimientosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.Procedimiento;

                    honorariosToolStripMenuItem1.Enabled = His.Parametros.AccesosModuloTarifario.MenuTarifario;
                    creacionDeHonorariosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.CreacionHonorarios;
                    consultaDeHonorariosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.ConsultaHonorario;

                    preciosYPorcentajesConveniosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.PreciosProcentajes;
                    tipoDeCostosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.TipoCosto;
                    catalogoCostosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.CatalogoCosto;
                    conveniosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.Convenios;
                    preciosPorConveniosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.PreciosConvenios;

                    reportesToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.Reporte;
                    honorariosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.Honorario;
                    empresasYAseguradorasToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.EmpresaAseguradora;

                    ventanaToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.Ventas;
                    mosaicoHorizontalToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.MosaicoHorizaontal;
                    mosaicoVerticalToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.MosaicoVertical;
                    cascadaToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.Cascada;
                    organizarIconosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.OrganizarIcono;

                    ayudaToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.Ayuda;
                    acercaDeToolStripMenuItem.Enabled = His.Parametros.AccesosModuloTarifario.AcercaDe;
                }

                EMPRESA empresa = NegEmpresa.RecuperaEmpresa();

                //txtEmpresa.Text = "Empresa " + string.Format("{0} ", empresa.EMP_NOMBRE);

            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void toolStripContainer1_BottomToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void ultraStatusBar1_Click(object sender, EventArgs e)
        {

        }

        private void procedimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdministrarTarifas ventana = new frmAdministrarTarifas();
            ventana.MdiParent = this;
            ventana.Show();

        }

        private void tipoDeCostosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCatalogosCostosTipo form = new frmCatalogosCostosTipo();
            form.MdiParent = this;
            form.Show();
        }

        private void catlogoCostosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_CatlogoCostos form = new frm_CatlogoCostos();
            form.MdiParent = this;
            form.Show();
        }


        //private void conveniosToolStripMenuItem_Click_1(object sender, EventArgs e)
        //{
        //    frm_CategoriasConvenios form = new frm_CategoriasConvenios();
        //    form.MdiParent = this;
        //    form.Show(); 
        //}


        private void preciosPorConveniosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_PreciosConvenios form = new frm_PreciosConvenios();
            form.MdiParent = this;
            form.Show();
        }

        //private void tipoDeCostosToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //}

        private void catalogoCostosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_CatlogoCostos form = new frm_CatlogoCostos();
            form.MdiParent = this;
            form.Show();
        }

        private void conveniosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmCategoriasConvenios form = new FrmCategoriasConvenios();
            form.MdiParent = this;
            form.Show();
        }

        private void consultaDeHonorariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultaHonorarios form = new ConsultaHonorarios();
            form.MdiParent = this;
            form.Show();
        }

        //private void preciosPorConveniosToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //}
    }
}
