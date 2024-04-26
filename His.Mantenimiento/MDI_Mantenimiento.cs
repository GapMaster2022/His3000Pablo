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

namespace His.Mantenimiento
{
    public partial class MDI_Mantenimiento : Form
    {
        #region Variables
        private int childFormNumber = 0;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MDI_Mantenimiento()
        {
            InitializeComponent();
            InicializarForma();
            
        }
        #endregion
        #region Metodos Privados
        private void DeshabilitarMenu()
        {
            //menEmpresa.Enabled = false;
            //menZonas.Enabled = false;
            //menDepartamentos.Enabled = false;
            //mnuCajas.Enabled = false;
            //menAccesos.Enabled = false;
            //menMantenimiento.Enabled = false;
            //menSeguridad.Enabled = false;
        }
        private void InicializarForma()
        {
            try
            {
                DeshabilitarMenu();
                if (Sesion.codUsuario == 0)
                {
                    His.Parametros.ArchivoIni archivo = new His.Parametros.ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
                    archivo.IniReadValue("Usuario", "usr");
                    var usuarioarchivo = NegUsuarios.RecuperaUsuarios().Where(usu => usu.USR == archivo.IniReadValue("Usuario", "usr")).FirstOrDefault();
                    //archivo.IniWriteValue("Usuario", "usr", usuario.USR);
                    //archivo.IniWriteValue("Usuario", "pwd", usuario.PWD); 
                    Sesion.codUsuario = usuarioarchivo.ID_USUARIO;
                }
                Sesion.TipoAcceso = His.Entidades.Acceso.DIRECTO;
                List<ACCESO_OPCIONES> perfilesAccesos = new NegPerfilesAcceso().AccesosUsuarios(Sesion.codUsuario, Constantes.MANTENIMIENTO);
                foreach (var acceso in perfilesAccesos)
                {
                    if (acceso.ID_ACCESO == 100002)
                        menAccesos.Enabled = true;

                    if (acceso.ID_ACCESO == 100003)
                        menMantenimiento.Enabled = true;

                    if (acceso.ID_ACCESO == 100004)
                        menSeguridad.Enabled = true;


                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);        
            }
        
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


        private void menUsuarios_Click(object sender, EventArgs e)
        {
            Form frm = new frm_Usuario();
            frm.MdiParent = this;
            frm.Show();
        }

        private void menPerfiles_Click(object sender, EventArgs e)
        {
            Form frm = new frm_Perfiles();
            frm.MdiParent = this;
            frm.Show();
        }

        private void menOpcionesAcceso_Click(object sender, EventArgs e)
        {

            Form frm = new frm_OpcionesAcceso();
            frm.MdiParent = this;
            frm.Show();
        }

        private void menPais_Click(object sender, EventArgs e)
        {

        }

        private void menTipoNegocio_Click(object sender, EventArgs e)
        {
            Form frm = new frm_TipoNegocio();
            frm.MdiParent = this;
            frm.Show();
        }

        private void menEmpresa_Click(object sender, EventArgs e)
        {
            Form frm = new frm_Empresa();
            frm.MdiParent = this;
            frm.Show();
        }

        private void menZonas_Click(object sender, EventArgs e)
        {
            Form frm = new frm_Zona();
            frm.MdiParent = this;
            frm.Show();
        }

        private void menDepartamentos_Click(object sender, EventArgs e)
        {
            Form frm = new frm_Departamentos();
            frm.MdiParent = this;
            frm.Show();
        }

        private void menSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
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

        

       
        private void MDI_Mantenimiento_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void mnuBancos_Click(object sender, EventArgs e)
        {
            Form frm = new frm_Bancos();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnuTipoMedico_Click(object sender, EventArgs e)
        {
            Form frm = new frm_TipoMedico();
            frm.MdiParent = this;
            frm.Show();
        }
        #endregion

        private void mnuNumerocontrol_Click(object sender, EventArgs e)
        {
            Form frm = new frm_NumeroControl();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnuCajas_Click(object sender, EventArgs e)
        {
            Form frm = new frm_Cajas();
            frm.MdiParent = this;
            frm.Show();
        }


        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Categoria form = new frm_Categoria();
            form.Show();
        }

        //private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    frm_Categoria form = new frm_Categoria();
        //    form.Show();
        //}

        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Servicios form = new frm_Servicios();
            form.Show();
        }

        private void preciosYPorcentajesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_precios form = new frm_precios();
            form.Show();
        }

        private void tipoCategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                frm_Categoria form = new frm_Categoria();
                form.MdiParent = this;
                form.Show();

        }

        private void tipoServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Servicios from = new frm_Servicios();
            from.Show();                  
        }

        private void preciosYPorcentajesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void porcentajesPorServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frm_Porcentajes_Por_Servicios form = new frm_Porcentajes_Por_Servicios();
            //form.Show();
        }

        //private void catalogosCostosTipoToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    frmCatalogosCostosTipo form = new frmCatalogosCostosTipo();
        //    form.Show(); 
        //}

        //private void catalogosCostosToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    frm_CatlogoCostos form = new frm_CatlogoCostos();
        //    form.Show();
        //}

        //private void conveniosToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    frm_CategoriasConvenios form = new frm_CategoriasConvenios();
        //    form.Show();
        //}

        //private void preciosConveniosToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    frm_PreciosConvenios form = new frm_PreciosConvenios();
        //    form.Show();
        //}


        //private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    frm_Servicios form = new frm_Servicios();
        //    form.Show();
        //}


        //private void preciosYPorcentajesToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    frm_precios form = new frm_precios();
        //    form.Show();
        //}

        //private void tipoCategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        //{
            
        //        frm_Categoria form = new frm_Categoria();
        //        form.MdiParent = this;
        //        form.Show();

        //}

        //private void tipoServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    frm_Servicios from = new frm_Servicios();
        //    from.Show();                  
        //}

        //private void preciosYPorcentajesToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
            
        //}

        //private void porcentajesPorServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //frm_Porcentajes_Por_Servicios form = new frm_Porcentajes_Por_Servicios();
        //    //form.Show();
        //}

        //private void catalogosCostosTipoToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //frmCatalogosCostosTipo form = new frmCatalogosCostosTipo();
        //    //form.Show(); 
        //}

        //private void catalogosCostosToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //frm_CatlogoCostos form = new frm_CatlogoCostos();
        //    //form.Show();
        //}

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

        private void conveniosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_CategoriasConvenios form = new frm_CategoriasConvenios();
            form.MdiParent = this; 
            form.Show(); 
        }

        private void preciosPorConveniosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_PreciosConvenios form = new frm_PreciosConvenios();
            form.MdiParent  = this;
            form.Show();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {

        }

        private void divisiónPolíticaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_DivisionPolitica form = new frm_DivisionPolitica();
            form.MdiParent = this;
            form.Show();
        }

        private void MDI_Mantenimiento_Load(object sender, EventArgs e)
        {
            try
            {
                //cargo imagen pro defecto
                //this.BackgroundImage = Recursos.Archivo.logoEmpresa;
                this.Icon = Recursos.Archivo.IconoTarifario;
                //cargo la barra de estado
                ultraStatusBarTarifario.Panels["empresa"].Text = Sesion.nomEmpresa;
                ultraStatusBarTarifario.Panels["usuario"].Text = Sesion.nomUsuario;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }   
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menCatalogo_Click(object sender, EventArgs e)
        {
            Form frm = new frm_Catalogos();
            frm.MdiParent = this;
            frm.Show();            
        }

        private void menProcedimeinto_Click(object sender, EventArgs e)
        {
            //Form frm = new frm_Procedimientos();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void btn_CuentasPacientes_Click(object sender, EventArgs e)
        {
            //Form frm = new frm_PedidosCuenta();            
            //frm.Show();
        }

        private void areasDePedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frm_PedidosCuenta();
            frm.MdiParent = this;
            frm.Show();
          
        }

        private void estacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frm_Estaciones();
            frm.MdiParent = this;
            frm.Show();
        }

    
        private void permisosCambiosEstadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frm_PermisosCambioEstadosCuentas();
            frm.MdiParent = this;
            frm.Show();
        }     

        //private void conveniosToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //frm_CategoriasConvenios form = new frm_CategoriasConvenios();
        //    //form.Show();
        //}

        //private void preciosConveniosToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //frm_PreciosConvenios form = new frm_PreciosConvenios();
        //    //form.Show();
        //}     
   

    }
}
