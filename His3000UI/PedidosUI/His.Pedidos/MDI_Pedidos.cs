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


namespace His.Pedidos
{
    public partial class MDIPedidos : Form
    {
        #region Variables
        private int childFormNumber = 0;
        private DtoParametros parametrosGenerales = new DtoParametros();
        #endregion

        #region Constructor
         public MDIPedidos()
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
                        MessageBox.Show("El usuario no existe","Error",MessageBoxButtons.OK ,MessageBoxIcon.Error);  
                        
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
            List<ACCESO_OPCIONES> perfilesAccesos =new NegPerfilesAcceso().AccesosUsuarios(Sesion.codUsuario,Constantes.HONORARIOS);

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
                            //System.Windows.MessageBox.Show("No se asignado un perfil al usuario", "error");
                            this.Close();
                        }
                    }
                }
            }
            herramientasToolStripMenuItem.Enabled = His.Parametros.AccesosModuloPedidos.Herramienta;
            controlDeDespachoToolStripMenuItem.Enabled = His.Parametros.AccesosModuloPedidos.ControlDespscho;
            exploradorDePedidosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloPedidos.ExploradorPedidos;
            monitoreoDeDevolucionesToolStripMenuItem.Enabled = His.Parametros.AccesosModuloPedidos.MonitoreoDevoluciones;
            monitoreoDePedidosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloPedidos.MonitoreoPedidos;

            reportesToolStripMenuItem.Enabled = His.Parametros.AccesosModuloPedidos.Reportes;
            consultaPedidosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloPedidos.ConsultaPedidos;
        }
        #endregion

        //private void ultraTabbedMdiManager1_InitializeTab(object sender, Infragistics.Win.UltraWinTabbedMdi.MdiTabEventArgs e)
        //{

        //}
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

        private void monitoreoDePedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMonitoreoPedidos x = new frmMonitoreoPedidos();
            x.MdiParent = this;
            x.Show();
        }

        private void monitoreoDeDevolucionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMonitorDevoluciones x = new frmMonitorDevoluciones();
            x.MdiParent = this;
            x.Show();
        }

        private void exploradorDePedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<PEDIDOS_AREAS> lstAreaPedido = NegPedidos.recuperarListaAreasPorUsuario(Sesion.codUsuario);
                if (lstAreaPedido.Count() == 1)
                {
                    frmExplorardorPedidos exploradorPedidos = new frmExplorardorPedidos();
                    exploradorPedidos.MdiParent = this;
                    exploradorPedidos.AreaPedido = lstAreaPedido.ElementAt(0);
                    exploradorPedidos.Show();
                }
                else if (lstAreaPedido.Count() > 1)
                {
                    frmSeleccionArea ventanaAreas = new frmSeleccionArea();
                    ventanaAreas.LstPedidosAreas = lstAreaPedido;
                    ventanaAreas.ShowDialog();
                    if (ventanaAreas.Area != null)
                    {
                        frmExplorardorPedidos exploradorPedidos = new frmExplorardorPedidos();
                        exploradorPedidos.MdiParent = this;
                        exploradorPedidos.AreaPedido = ventanaAreas.Area;
                        exploradorPedidos.Show();
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar el Aréa con la que desea trabajar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor asigne un aréa al usuario", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void areasDeDistribucionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAreas ventana = new frmAreas();
            ventana.MdiParent = this;
            ventana.Show();
        }

        private void estacionesDePedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEstaciones ventana = new frmEstaciones();
            ventana.MdiParent = this;
            ventana.Show();
        }

        private void asignarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsuariosPorAreas ventana = new frmUsuariosPorAreas();
            ventana.MdiParent = this;
            ventana.Show();
        }

        private void asignarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProductosPorAreas ventana = new frmProductosPorAreas();
            ventana.MdiParent = this;
            ventana.Show();
        }

        private void consultaPedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaPedido ventana = new frmConsultaPedido();
            ventana.MdiParent = this;
            ventana.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDespachos x = new frmDespachos();
            x.MdiParent = this;
            x.Show();
        }

        private void controlDeDespachoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDespachos x = new frmDespachos();
            x.MdiParent = this;
            x.Show();

        }
    }
}
