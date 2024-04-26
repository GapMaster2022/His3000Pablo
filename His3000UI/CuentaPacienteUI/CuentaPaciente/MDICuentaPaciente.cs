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
using Core.Utilitarios;
using His.Garantia;

namespace CuentaPaciente
{
    public partial class MDICuentaPaciente : Form
    {
        private int childFormNumber = 0;
        private DtoParametros parametrosGenerales = new DtoParametros();
        System.Timers.Timer oTimer = new System.Timers.Timer();


        public MDICuentaPaciente()
        {
            InitializeComponent();
            if (!NegUtilitarios.IpBodegas())
            {
                MessageBox.Show("Está maquina no tiene asignada una bodega, por lo que no se puede abrir el modulo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            InicializarForma();
            //Cambios Edgar 20210212 codigo para enviar cada 6 horas correos sobre los convenios caducados

            //oTimer.Interval = 6000000; //intervalo de 1min = 100000 para pruebas para 6H serian = 6000000
            //oTimer.Elapsed += EventoIntervalo;
            //oTimer.Enabled = true;

            EventoEnvioCorreoConvenio();
            //______________________________________________
        }

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

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        //private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    statusStrip.Visible = statusBarToolStripMenuItem.Checked;
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

        private void btnDetalleCuenta_Click(object sender, EventArgs e)
        {
                   
        }

        private void EventoEnvioCorreoConvenio()
        {
            //Aqui va revisar el convenio cada 6 HORA
            try
            {
                int cat_codigo;
                string cat_nombre;
                DateTime fecha_caducar;
                bool enviar = false;
                //Primero obtenemos el día actual
                DateTime date = DateTime.Now;
                date = date.AddMonths(1); //SE AGREGA UN MES PARA PODER ENVIAR LOS CORREOS CON UN MES DE ANTICIPO
                //Asi obtenemos el primer dia del mes actual
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
                //Y de la siguiente forma obtenemos el ultimo dia del mes
                //agregamos 1 mes al objeto anterior y restamos 1 día.
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
                DataTable Convenios = NegMaintenance.ConveniosPorCaducar(oPrimerDiaDelMes, oUltimoDiaDelMes);//Aqui recibe los convenios por caducar durante el mes actual
                if (Convenios.Rows.Count > 0)
                {
                    foreach (DataRow item in Convenios.Rows)
                    {
                        cat_codigo = Convert.ToInt32(item[0].ToString());
                        cat_nombre = item[1].ToString();
                        fecha_caducar = Convert.ToDateTime(item[2].ToString());
                        enviar = true;
                        //Validamos si el correo ya se envio con fecha de hoy
                        NegEmail.EnviarCorreo(cat_codigo, fecha_caducar, cat_nombre, enviar);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static void EventoIntervalo(Object source, System.Timers.ElapsedEventArgs e)
        {
            //Aqui va revisar el convenio cada 6 HORA
            try
            {
                int cat_codigo;
                string cat_nombre;
                DateTime fecha_caducar;
                bool enviar = false;
                //Primero obtenemos el día actual
                DateTime date = DateTime.Now;
                date = date.AddMonths(1); //SE AGREGA UN MES PARA PODER ENVIAR LOS CORREOS CON UN MES DE ANTICIPO
                //Asi obtenemos el primer dia del mes actual
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
                //Y de la siguiente forma obtenemos el ultimo dia del mes
                //agregamos 1 mes al objeto anterior y restamos 1 día.
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
                DataTable Convenios = NegMaintenance.ConveniosPorCaducar(oPrimerDiaDelMes, oUltimoDiaDelMes);//Aqui recibe los convenios por caducar durante el mes actual
                if(Convenios.Rows.Count > 0)
                {
                    foreach (DataRow item in Convenios.Rows)
                    {
                        cat_codigo = Convert.ToInt32(item[0].ToString());
                        cat_nombre = item[1].ToString();
                        fecha_caducar = Convert.ToDateTime(item[2].ToString());
                        enviar = true;
                        //Validamos si el correo ya se envio con fecha de hoy
                        NegEmail.EnviarCorreo(cat_codigo, fecha_caducar, cat_nombre, enviar);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            List<ACCESO_OPCIONES> perfilesAccesos = new NegPerfilesAcceso().AccesosUsuarios(Sesion.codUsuario, Constantes.CUENTAS);
            foreach (var acceso in perfilesAccesos)
            {
                if (acceso.ID_ACCESO == 701)
                    facturacionToolStripMenuItem.Enabled = true;

                if (acceso.ID_ACCESO == 702)
                    btnDetalleCuenta.Enabled = true;

                if (acceso.ID_ACCESO == 703)
                atencionesIESSToolStripMenuItem .Enabled= true;

                if (acceso.ID_ACCESO == 704)
                    consolidarAtencionesToolStripMenuItem.Enabled = true;

                if (acceso.ID_ACCESO == 705)
                    gEToolStripMenuItem.Enabled = true;

                if (acceso.ID_ACCESO == 706)
                    estadosCuentaToolStripMenuItem.Enabled = true;

            }

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
                            //System.Windows.MessageBox.Show("No se asignado un perfil al usuario", "error");
                            this.Close();
                        }
                    }
                }
            }
            facturacionToolStripMenuItem.Enabled = His.Parametros.AccesosModuloCuentaPaciente.Facturacion;
            btnNuevoFactura.Enabled = His.Parametros.AccesosModuloCuentaPaciente.NuevaFactura;

            auditoriaToolStripMenuItem.Enabled = His.Parametros.AccesosModuloCuentaPaciente.DivisionCuentas;
            revisionCuentasToolStripMenuItem.Enabled = His.Parametros.AccesosModuloCuentaPaciente.RevisionCuentas;

            informesToolStripMenuItem.Enabled = His.Parametros.AccesosModuloCuentaPaciente.Informe;
            cierreDeTurnoToolStripMenuItem.Enabled = His.Parametros.AccesosModuloCuentaPaciente.CierreTurno;
            detalleCambiosCuentasToolStripMenuItem.Enabled = His.Parametros.AccesosModuloCuentaPaciente.CambioCuenta;
            detalleValoresCuentasToolStripMenuItem.Enabled = His.Parametros.AccesosModuloCuentaPaciente.ValoresCuenta;
            exploradorAuditoriaToolStripMenuItem.Enabled = His.Parametros.AccesosModuloCuentaPaciente.ExpAuditoria;

            garantiasToolStripMenuItem.Enabled = His.Parametros.AccesosModuloCuentaPaciente.Garantias;
            nuevaGarantiaToolStripMenuItem.Enabled = His.Parametros.AccesosModuloCuentaPaciente.NuevaGarantia;
            preautorizacionToolStripMenuItem.Enabled = His.Parametros.AccesosModuloCuentaPaciente.PreAutorizacion;
            reporteToolStripMenuItem.Enabled = His.Parametros.AccesosModuloCuentaPaciente.Reporte;

            btnDetalleCuenta.Enabled = His.Parametros.AccesosModuloCuentaPaciente.Auditoria;
            consolidarAtencionesToolStripMenuItem.Enabled = His.Parametros.AccesosModuloCuentaPaciente.DetalleAtencion;
            estadosCuentaToolStripMenuItem.Enabled = His.Parametros.AccesosModuloCuentaPaciente.EstadoCuenta;

        }

        private void MDICuentaPaciente_Load(object sender, EventArgs e)
        {
            if (!NegUtilitarios.IpBodegas())
            {
                this.Close();
                return;
            }
            //string departamento; // Mario // se comenta por que empieza a trabajar por accesos // 06/11/2023
            //try
            //{
            //    departamento = Convert.ToString(His.Entidades.Clases.Sesion.codDepartamento);
            //    if (departamento == "15")
            //    {
            //        btnDetalleCuenta.Enabled = false;
            //        informesToolStripMenuItem.Enabled = false;
            //        auditoriaToolStripMenuItem.Enabled = false;
            //    }
            //    else
            //    {
            //        btnDetalleCuenta.Enabled = true;
            //        informesToolStripMenuItem.Enabled = true;
            //        auditoriaToolStripMenuItem.Enabled = true;
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            //this.BackgroundImage = Recursos.Archivo.logoEmpresa;
            //this.Icon = Recursos.Archivo.IconoTarifario;
        }

        private void btnNuevoFactura_Click(object sender, EventArgs e)
        {
            //leer archivo de configuracion

           // do
           //{

            frmFactura archivo = new frmFactura(Environment.CurrentDirectory + "\\his3000.ini");
            archivo.codigoCaja = Convert.ToInt16(archivo.IniReadValue("Caja", "codigo"));
            Form frm = new frmFactura(archivo.codigoCaja);
            frm.MdiParent = this;
            //frm.ShowDialog();
            frm.Show();
           // DialogResult resultado;
           // resultado = MessageBox.Show("Desea ingresar una nueva factura...?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
           // if (resultado == DialogResult.Yes)
           // {
           //     His.Parametros.CuentasPacientes.ContinuaFacturacion = true;
           // }
           // else
           // {
           //     His.Parametros.CuentasPacientes.ContinuaFacturacion = false;
           // }

           //} while (His.Parametros.CuentasPacientes.ContinuaFacturacion==true);
           

        }

        private void atencionesIESSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmCuentaPacientesIess();
            frm.MdiParent = this;
            frm.Show();
        }

        private void consolidarAtencionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmCuentaDetalleAtencion(Sesion.codUsuario);
            frm.MdiParent = this;
            frm.Show();

        }

        private void gEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmExploradorAtenciones();
            frm.MdiParent = this;
            frm.Show();
        }

        private void estadosCuentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPaquetesCtas form = new frmPaquetesCtas();
            form.MdiParent = this;
            form.Show();
        }

        private void detalleCambiosCuentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new DetalleCuentasCambiadas();
            frm.MdiParent = this;
            frm.Show();
        }

        private void paquetesCuentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmEstadoCuenta();
            frm.MdiParent = this;
            frm.Show();
        }

        private void cierreDeTurnoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmCierreTurno();
            frm.MdiParent = this;
            frm.Show();
        }

        private void detalleValoresCuentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaCuentas Forma = new frmConsultaCuentas();
            Forma.Show();
        }

        private void consultaFacturaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //frmFacturaConsulta Forma = new frmFacturaConsulta(Environment.CurrentDirectory + "\\his3000.ini");
            //Forma.Show();

        }

        private void revisionCuentasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form frm = new frmAuditoriaCuenta();
            frm.MdiParent = this;
            frm.Show();

        }

        private void txtNombres_Click(object sender, EventArgs e)
        {

        }

        private void nuevaGarantiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 Procedimientos = new Form1();
            Procedimientos.MdiParent = this;
            Procedimientos.Show();
        }

        private void preautorizacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPreAutorizacion Procedimientos = new frmPreAutorizacion();
            Procedimientos.MdiParent = this;
            Procedimientos.Show();
        }

        private void reporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ExploradorFormularios Procedimientos = new frm_ExploradorFormularios();
            Procedimientos.MdiParent = this;
            Procedimientos.Show();
        }

        private void TimerConvenios_Tick(object sender, EventArgs e)
        {
        }

        private void exploradorAuditoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExploradorAuditoria x = new frmExploradorAuditoria();
            x.MdiParent = this;
            x.Show();
        }

        private void reversionCopagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReversionCopago x = new frmReversionCopago();
            x.MdiParent = this;
            x.Show();
        }
    }
}
