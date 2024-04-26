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
    public partial class MDI_Honorarios : Form
    {
        #region Variables
        private int childFormNumber = 0;
        private DtoParametros parametrosGenerales = new DtoParametros();
        #endregion

        #region Constructor
         public MDI_Honorarios()
        {
             InitializeComponent();
             InicializarForma();
            if (!NegUtilitarios.IpBodegas())
            {
                MessageBox.Show("Está maquina no tiene asignada una bodega, por lo que no se puede abrir el modulo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
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
         private void smnu_medicos_Click(object sender, EventArgs e)
         {
             Form frm = new frm_Medicos();
             frm.MdiParent = this;
             frm.Show();
         }
         private void smnu_especialidades_Click(object sender, EventArgs e)
         {
             Form frm = new frm_Especialidades();
             frm.MdiParent = this;
             frm.Show();
         }
         private void smnu_tipodocumento_Click(object sender, EventArgs e)
         {
             Form frm = new frm_TipoDocumento();
             frm.MdiParent = this;
             frm.Show();
         }
         private void smnu_ComisionAportes_Click(object sender, EventArgs e)
         {
             Form frm = new frm_ComisionAprtes();
             frm.MdiParent = this;
             frm.Show();
         }
         private void smnu_TipoRetencion_Click(object sender, EventArgs e)
         {
             Form frm = new frm_TipoRetencion();
             frm.MdiParent = this;
             frm.Show();
         }
         private void smnu_ingresofacturas_Click(object sender, EventArgs e)
         {
             Form frm = new frm_IngresoHonorarios();
             frm.MdiParent = this;
             //frm.MdiParent = this;
             frm.Show();
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
         private void smnu_TipoHonorario_Click(object sender, EventArgs e)
         {
             Form frm = new frm_TipoHonorario();
             frm.MdiParent = this;
             frm.Show();
         }
         private void smnu_notaCredito_Click(object sender, EventArgs e)
         {
             frm_NotaCreditoDebitoHonorario frm = new frm_NotaCreditoDebitoHonorario();
             frm.chkmedico = true;
             frm.tipodoc = 2;
             frm.tiponota = 1;
             frm.activaiva = false;
             frm.MdiParent = this;
             frm.Show();
         }

         private void smnu_notasdebito_Click(object sender, EventArgs e)
         {
             frm_NotaCreditoDebitoHonorario frm = new frm_NotaCreditoDebitoHonorario();
             frm.chkmedico = true;
             frm.tipodoc = 3;
             frm.tiponota = 2;
             frm.MdiParent = this;
             frm.activaiva = false;
             frm.Show();
         }
         private void manualesToolStripMenuItem_Click(object sender, EventArgs e)
         {
             frm_EmisionRetenciones frm = new frm_EmisionRetenciones();
             frm.chkmedico = true;
             frm.MdiParent = this;
             frm.Show();
         }

         private void automaticasToolStripMenuItem_Click(object sender, EventArgs e)
         {
             Form frm = new frm_EmisionRetencionesAutomaticas();
             frm.MdiParent = this;
             frm.Show();
         }
         private void nDToolStripMenuItem_Click(object sender, EventArgs e)
         {
             Form frm = new frm_NotaDautomaticas();
             frm.MdiParent = this;
             frm.Show();

         }
        #endregion

        #region Metodos Privados
        private void DeshabilitarMenu()
        {
            smnu_medicos.Enabled = false;
            smnu_especialidades.Enabled = false;
            smnu_tipodocumento.Enabled = false;
            smnu_ComisionAportes.Enabled = false;
            smnu_TipoRetencion.Enabled = false;
            smnu_TipoHonorario.Enabled = false;

            smnu_ingresofacturas.Enabled = false;
            smnu_honorariosPorMedico.Enabled = false;

            mnu_explorador.Enabled = false;

            smnu_emisionDeRetenciones.Enabled = false;

            smnu_notaCredito.Enabled = false;
            smnu_notasdebito.Enabled = false;
            smnu_nD.Enabled = false;
            smnu_ndcomisionreferido.Enabled = false;

            smnu_nuevoCorreo.Enabled = false;
            smnu_opciones.Enabled = false;

            smnu_repmedico.Enabled = false;
            smnu_repnotacd.Enabled = false;
            smnu_repretenciones.Enabled = false;
            smnu_rContables.Enabled = false;

            smnu_h_pagar.Enabled = false;
            smnu_Honorariospagados.Enabled = false;
            smnu_pagosrealizdos.Enabled = false;
            smnu_balanceGerencial.Enabled = false;

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
                            System.Windows.MessageBox.Show("No se asignado un perfil al usuario", "error");
                            this.Close();
                        }
                    }
                }

                //NegAccesoOpciones.asignarAccesosPorPerfil(perfilUsuario.ID_PERFIL);

                smnu_medicos.Enabled = His.Parametros.AccesosModuloHonorarios.Medicos;
                smnu_especialidades.Enabled = His.Parametros.AccesosModuloHonorarios.Especialidades;
                smnu_tipodocumento.Enabled = His.Parametros.AccesosModuloHonorarios.Tipo_documento;
                smnu_ComisionAportes.Enabled = His.Parametros.AccesosModuloHonorarios.ComisionesClinicaReferido;
                smnu_TipoRetencion.Enabled = His.Parametros.AccesosModuloHonorarios.TipoRetencion;
                smnu_TipoHonorario.Enabled = His.Parametros.AccesosModuloHonorarios.TipoHonorario;
                smnu_ingresofacturas.Enabled = His.Parametros.AccesosModuloHonorarios.IngresoHonorarios;
                smnu_honorariosPorMedico.Enabled = His.Parametros.AccesosModuloHonorarios.HonorariosPorMedico;
                smnu_repmedico.Enabled = His.Parametros.AccesosModuloHonorarios.ReporteMedicos;
                smnu_repnotacd.Enabled = His.Parametros.AccesosModuloHonorarios.ReporteNotas;
                smnu_repretenciones.Enabled = His.Parametros.AccesosModuloHonorarios.ReporteRetenciones;
                smnu_rContables.Enabled = His.Parametros.AccesosModuloHonorarios.ReporteContable;
                smnu_h_pagar.Enabled = His.Parametros.AccesosModuloHonorarios.HonorariosPendientesPago;
                smnu_Honorariospagados.Enabled = His.Parametros.AccesosModuloHonorarios.HonorariosPendientesCancelar;
                smnu_pagosrealizdos.Enabled = His.Parametros.AccesosModuloHonorarios.HonorariosCancelados;
                smnu_balanceGerencial.Enabled = His.Parametros.AccesosModuloHonorarios.BalanceGerencial;
                mnuVendedores.Enabled = His.Parametros.AccesosModuloHonorarios.Vendedores;
                ingresoDeFacturasToolStripMenuItem.Enabled = His.Parametros.AccesosModuloHonorarios.IngresoFacturas;
                facturasCambiadasPorAnulacionToolStripMenuItem.Enabled = His.Parametros.AccesosModuloHonorarios.FacturasAnulacion;
                asientoContableHonorariosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloHonorarios.AsientoHonorario;
                asignarFacturasLiquidacionesToolStripMenuItem.Enabled = His.Parametros.AccesosModuloHonorarios.AsignarFacturasLiquidacion;
                exploradorDeLiquidacionesToolStripMenuItem.Enabled = His.Parametros.AccesosModuloHonorarios.ExploradorLiquidaciones;
                liquidaciónToolStripMenuItem.Enabled = His.Parametros.AccesosModuloHonorarios.Liquidaciones;
                reporteDeComisionesVendedoresToolStripMenuItem.Enabled = His.Parametros.AccesosModuloHonorarios.ReporteComision;
                mnu_archivo.Enabled = His.Parametros.AccesosModuloHonorarios.Archivo;
                mnu_procesosdiarios.Enabled = His.Parametros.AccesosModuloHonorarios.ProcesoDiario;
                liquidacion.Enabled = His.Parametros.AccesosModuloHonorarios.LiquidacionHonorario;
                mnu_explorador.Enabled = His.Parametros.AccesosModuloHonorarios.Explorador;
                mnu_reportescontables.Enabled = His.Parametros.AccesosModuloHonorarios.Reporte;
                smnu_salir.Enabled = His.Parametros.AccesosModuloHonorarios.Salir;
                exploradorPorMédicosToolStripMenuItem.Enabled = His.Parametros.AccesosModuloHonorarios.ExploradorMedicos;
            }


            //txtFecha.Text = "FECHA: " + DateTime.Now.ToString("D");

            //timHora.Start();
            //USUARIOS usuario =NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
            //Sesion.nomUsuario = usuario.NOMBRES + " " + usuario.APELLIDOS;
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

            //EMPRESA empresa = NegEmpresa.RecuperaEmpresa();
        
            //txtEmpresa.Text = "Empresa " + string.Format("{0} ",empresa.EMP_NOMBRE);


        
        }
        #endregion

        
        private void smnu_repmedico_Click(object sender, EventArgs e)
        {
            //AbrirFormulario<frm_RepMedicos>();
            Form frm = new frm_RepMedicos(); 
             frm.MdiParent = this;
            frm.Show();

        }

        private void smnu_repnotacd_Click(object sender, EventArgs e)
        {
            //AbrirFormulario<frm_RepNotasCreditoDebito>();
            Form frm = new frm_RepNotasCreditoDebito(); 
             frm.MdiParent = this;
            frm.Show();

        }

        private void smnu_repretenciones_Click(object sender, EventArgs e)
        {
            //AbrirFormulario<frm_RepRetenciones>();
            Form frm = new frm_RepRetenciones();
            frm.MdiParent = this;
            frm.Show();
        }

        private void smnu_apagar_Click(object sender, EventArgs e)
        {
            Form frm = new frm_BalanceApagar();
            frm.MdiParent = this;
            //AbrirFormulario<frm_BalanceApagar>();
            frm.Show();
        }

        private void smnu_Honorariospagados_Click(object sender, EventArgs e)
        {

            Form frm = new frm_BalancePagados();
            frm.MdiParent = this;
            //AbrirFormulario<frm_BalancePagados>();
            frm.Show(); 
        }

        private void smnu_pagosrealizdos_Click(object sender, EventArgs e)
        {

            Form frm = new frm_BalanceCancelados();
            frm.MdiParent = this;
            //AbrirFormulario<frm_BalanceCancelados>();
            frm.Show(); //CAMBIOS EDGAR RAMOS
        }

        private void smnu_ndcomisionreferido_Click(object sender, EventArgs e)
        {
            frm_NotaDautomaticas frm = new frm_NotaDautomaticas();
            frm.comisionreferido = true;
            frm.MdiParent = this;
           // AbrirFormulario<frm_NotaDautomaticas>();
            frm.Show();
        }

        private void smnu_precibidosSeguros_Click(object sender, EventArgs e)
        {

        }

        private void smnu_pRecibidosParticularesClinica_Click(object sender, EventArgs e)
        {

        }

        private void smn_pRecibidosTarjetasCréditoClinica_Click(object sender, EventArgs e)
        {

        }

        private void smnu_pagosEnChequesGiradosAlMedico_Click(object sender, EventArgs e)
        {

        }

        private void smnu_pagosEnTransferenciaRealizadosAlMedico_Click(object sender, EventArgs e)
        {

        }

        private void correosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void honorariosPorMédicoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frm_ConsultaHonorariosMedico ventana = new frm_ConsultaHonorariosMedico();
            ventana.MdiParent = this;
            //AbrirFormulario<frm_ConsultaHonorariosMedico>();
            ventana.Show(); 
        }

        private void ultraTabbedMdiManager1_InitializeTab(object sender, Infragistics.Win.UltraWinTabbedMdi.MdiTabEventArgs e)
        {

        }

        private void rToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void honorariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_BalanceGerencial ventana = new frm_BalanceGerencial();
            ventana.MdiParent = this;
            //AbrirFormulario<frm_BalanceGerencial>();
            ventana.Show(); //CAMBIOS EDGAR RAMOS
        }

        private void correoClasicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCorreosNuevo correos = new frmCorreosNuevo();
            correos.MdiParent = this;
            //AbrirFormulario<frmCorreosNuevo>();
            correos.Show(); //CAMBIOS EDGAR RAMOS
        }

        private void multiplesAdjuntosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCorreoMultipleNuevo correos = new frmCorreoMultipleNuevo();
            correos.MdiParent = this;
            //AbrirFormulario<frmCorreoMultipleNuevo>();
            correos.Show(); // CAMBIO EDGAR RAMOS
        }

        private void ingresoDeFacturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frm_IngresoFacturasClinica();
            frm.MdiParent = this;
           /// AbrirFormulario<frm_IngresoFacturasClinica>();
            frm.Show();

        }

        private void exploradorToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //frmHonorariosNuevo frm = new frmHonorariosNuevo();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void estadoCuentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEstadoCuenta frm = new frmEstadoCuenta();
            frm.MdiParent = this;
            //AbrirFormulario<frmEstadoCuenta>();
            frm.Show(); //CAMBIO EDGAR RAMOS
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

        private void mnuVendedores_Click(object sender, EventArgs e)
        {
            Form frm = new frm_Vendedores();
            frm.MdiParent = this;
            frm.Show();
        }

        private void reporteDeComisionesVendedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AbrirFormulario<frm_RepMedicos>();
            Form frm = new Reporte_FactVendedores();
            frm.MdiParent = this;
            frm.Show();
        }

        private void exploradorPorMédicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form explorador = new frmExploradorHonorarios();
            explorador.MdiParent = this;
            //explorador.MdiParent = this;
            explorador.Show();
        }

        private void exploradorGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExploradorGeneral x = new frmExploradorGeneral();
            x.MdiParent = this;
            x.Show();
        }

        private void certificadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Certificados x = new frm_Certificados();
            x.MdiParent = this;
            x.Show();
        }

        private void asientoContableHonorariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_AsientoHonMed x = new frm_AsientoHonMed();
            x.MdiParent = this;
            x.Show();
        }

        private void mnu_pagos_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void facturasCambiadasPorAnulacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_FacturaCambiar x = new frm_FacturaCambiar();
            x.MdiParent = this;
            x.Show();
        }

        private void liquidaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Liquidaciones x = new frm_Liquidaciones();
            x.MdiParent = this;
            x.Show();
        }

        private void exploradorDeLiquidacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExploradorLiquidacion x = new frmExploradorLiquidacion();
            x.MdiParent = this;
            x.Show();
        }

        private void asignarFacturasLiquidacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_LiquidacionFacturas x = new frm_LiquidacionFacturas();
            x.MdiParent = this;
            x.Show();
        }
    }
}
