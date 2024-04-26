using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades.Clases;
using His.Admision;
using CuentaPaciente;
using System.Diagnostics;
using His.Entidades;

namespace His.ConsultaExterna
{
    public partial class mdiConsultaExterna : Form
    {
        #region VARIABLES GLOBALES

        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;
        int lx, ly;
        int sw, sh;

        #endregion
        public mdiConsultaExterna()
        {
            InitializeComponent();
            if (!NegUtilitarios.IpBodegas())
            {
                MessageBox.Show("Está maquina no tiene asignada una bodega, por lo que no se puede abrir el modulo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            InicializarForma();
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            DatosUsuario();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
            oculta();
        }
        private void InicializarForma()
        {
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
                            MessageBox.Show("No se asignado un perfil al usuario", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                    }
                }
                btnAgenda.Enabled = His.Parametros.AccesosModuloConsultaExterna.Agendamiento;
                btnAgendaConsulta.Enabled = His.Parametros.AccesosModuloConsultaExterna.AgendaPacientes;
                button4.Enabled = His.Parametros.AccesosModuloConsultaExterna.ExpCitasMedicas;
                btnConsultaExterna.Enabled = His.Parametros.AccesosModuloConsultaExterna.ConsultaExterna;
                btnAdmisionCE.Enabled = His.Parametros.AccesosModuloConsultaExterna.Admision;
                btnTriaje.Enabled = His.Parametros.AccesosModuloConsultaExterna.Triaje;
                btnIngresaOrden.Enabled = His.Parametros.AccesosModuloConsultaExterna.SignosVitales;
                button3.Enabled = His.Parametros.AccesosModuloConsultaExterna.Habitaciones;
                btnConsultaEx.Enabled = His.Parametros.AccesosModuloConsultaExterna.Consulta;
                btnFacturaCE.Enabled = His.Parametros.AccesosModuloConsultaExterna.Facturacion;
                btnReportes.Enabled = His.Parametros.AccesosModuloConsultaExterna.ExpConsultaExterna;
                button1.Enabled = His.Parametros.AccesosModuloConsultaExterna.ExpCertificado;
                button2.Enabled = His.Parametros.AccesosModuloConsultaExterna.ExpReceta;
            }
        }
        #region REDIMENCIONAR EL FORMULARIO Y ARRASTRAR

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));
            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);
            region.Exclude(sizeGripRectangle);
            this.P_Central.Region = region;
            this.Invalidate();
        }
        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(13, 13, 43));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);
            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);

        }
        private void P_Barra_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void btnNormal_Click(object sender, EventArgs e)
        {
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
            btnNormal.Visible = false;
            btnMaximiza.Visible = true;
        }
        private void btnMinizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnMaximiza_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            btnNormal.Visible = true;
            btnMaximiza.Visible = false;
        }
        #endregion

        #region ABRIR FORMULARIO DENTRO DE PANEL Y VOLVER LOS COLORES AL ORIGINAL

        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            //BUSCA EN LA COLECCION EL FORMULARIO
            formulario = P_Formularios.Controls.OfType<MiForm>().FirstOrDefault();
            //VERIFICO SI EL FORMULARIO EXISTE O NO EXISTE PARA NO VOLVER ABRIRLO SI NO SOLO LLAMARLO
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                P_Formularios.Controls.Add(formulario);
                P_Formularios.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
                formulario.FormClosed += new FormClosedEventHandler(CloseForm);
            }
            else
            {
                formulario.BringToFront();
            }
        }

        private void CloseForm(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Emergencia.frm_Triaje"] == null)
            {
                btnTriaje.BackColor = Color.FromArgb(31, 195, 216);
            }
            if (Application.OpenForms["frm_AgendaCita"] == null)
            {
                btnAgendaConsulta.BackColor = Color.FromArgb(31, 195, 216);
            }
            if (Application.OpenForms["frm_AgendaCitaMedica"] == null)
            {
                btnAgendaConsulta.BackColor = Color.FromArgb(31, 195, 216);
            }
            if (Application.OpenForms["frm_ExploradorCitaMedica"] == null)
            {
                button4.BackColor = Color.FromArgb(31, 195, 216);
            }
            if (Application.OpenForms["frm_Admision"] == null)
            {
                btnAdmisionCE.BackColor = Color.FromArgb(31, 195, 216);
            }
            if (Application.OpenForms["frmFactura"] == null)
            {
                btnFacturaCE.BackColor = Color.FromArgb(31, 195, 216);
            }
            if (Application.OpenForms["Consulta"] == null)
            {
                btnConsultaEx.BackColor = Color.FromArgb(31, 195, 216);
            }
            if (Application.OpenForms["His.Formulario.frm_Receta"] == null)
            {
                button1.BackColor = Color.FromArgb(31, 195, 216);
            }
            if (Application.OpenForms["His.Formulario.frm_Certificados"] == null)
            {
                button2.BackColor = Color.FromArgb(31, 195, 216);
            }
            if (Application.OpenForms["frm_ExploradorFormularios"] == null)
            {
                btnReportes.BackColor = Color.FromArgb(31, 195, 216);
            }
        }

        #endregion
        private void oculta()
        {
            P_Cliente.Visible = false;
            P_Inventario.Visible = false;


        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿ESTA SEGURO DE CERRA CONSULTA EXTERNA?", "S2P", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void ocultaSubMenu()
        {
            if (P_Cliente.Visible == true)
            {
                P_Cliente.Visible = false;
            }
            if (P_Inventario.Visible == true)
            {
                P_Inventario.Visible = false;
            }
        }

        private void btnAgenda_Click(object sender, EventArgs e)
        {
            muestraSubMenu(P_Cliente);
        }

        private void btnConsultaExterna_Click(object sender, EventArgs e)
        {
            muestraSubMenu(P_Inventario);
        }


        private void btnAgendaConsulta_Click(object sender, EventArgs e)
        {
            btnAgendaConsulta.BackColor = Color.Gainsboro;
            //AbrirFormulario<frm_AgendaCita>();
            frm_AgendaCitaMedica formulario = new frm_AgendaCitaMedica();
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            P_Formularios.Controls.Add(formulario);
            P_Formularios.Tag = formulario;
            //formulario.cmb_tipoingreso.Text = "CONSULTA EXTERNA";
            formulario.Show();
            formulario.BringToFront();
            formulario.FormClosed += new FormClosedEventHandler(CloseForm);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.Gainsboro;
            frm_ExploradorCitaMedica formulario = new frm_ExploradorCitaMedica();
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            P_Formularios.Controls.Add(formulario);
            P_Formularios.Tag = formulario;
            //formulario.cmb_tipoingreso.Text = "CONSULTA EXTERNA";
            formulario.Show();
            formulario.BringToFront();
            formulario.FormClosed += new FormClosedEventHandler(CloseForm);
        }

        private void btnAdmisionCE_Click(object sender, EventArgs e)
        {
            btnAdmisionCE.BackColor = Color.Gainsboro;
            frm_Admision formulario = new frm_Admision();
            formulario.ConsultaExterna = true;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            P_Formularios.Controls.Add(formulario);
            P_Formularios.Tag = formulario;
            //formulario.cmb_tipoingreso.Text = "CONSULTA EXTERNA";
            formulario.Show();
            formulario.BringToFront();
            formulario.FormClosed += new FormClosedEventHandler(CloseForm);
        }

        private void btnTriaje_Click(object sender, EventArgs e)
        {
            btnTriaje.BackColor = Color.Gainsboro;
            Emergencia.frm_Triaje formulario = new Emergencia.frm_Triaje(false);
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.None;
            P_Formularios.Controls.Add(formulario);
            P_Formularios.Tag = formulario;
            //formulario.cmb_tipoingreso.Text = "CONSULTA EXTERNA";
            formulario.Show();
            formulario.BringToFront();
            formulario.FormClosed += new FormClosedEventHandler(CloseForm);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Gainsboro;
            HabitacionesUI.ExploradorHabitaciones x = new HabitacionesUI.ExploradorHabitaciones();
            x.Show();
        }

        private void btnConsultaEx_Click(object sender, EventArgs e)
        {
            btnConsultaEx.BackColor = Color.Gainsboro;
            Consulta formulario = new Consulta();
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            P_Formularios.Controls.Add(formulario);
            P_Formularios.Tag = formulario;
            //formulario.cmb_tipoingreso.Text = "CONSULTA EXTERNA";
            formulario.Show();
            formulario.BringToFront();
            formulario.FormClosed += new FormClosedEventHandler(CloseForm);
        }

        private void btnFacturaCE_Click(object sender, EventArgs e)
        {
            btnFacturaCE.BackColor = Color.Gainsboro;
            frmFactura archivo = new frmFactura(Environment.CurrentDirectory + "\\his3000.ini");
            archivo.codigoCaja = Convert.ToInt16(archivo.IniReadValue("Caja", "codigo"));
            frmFactura formulario = new frmFactura(archivo.codigoCaja);
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            P_Formularios.Controls.Add(formulario);
            P_Formularios.Tag = formulario;
            formulario.Show();
            formulario.BringToFront();
            formulario.FormClosed += new FormClosedEventHandler(CloseForm);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            btnReportes.BackColor = Color.Gainsboro;
            His.Honorarios.frm_ExploradorFormularios formulario = new His.Honorarios.frm_ExploradorFormularios();
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            P_Formularios.Controls.Add(formulario);
            P_Formularios.Tag = formulario;
            formulario.Show();
            formulario.BringToFront();
            formulario.FormClosed += new FormClosedEventHandler(CloseForm);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.Gainsboro;
            Honorarios.frm_ExploradorCertificado formulario = new Honorarios.frm_ExploradorCertificado();
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            P_Formularios.Controls.Add(formulario);
            P_Formularios.Tag = formulario;
            formulario.Show();
            formulario.BringToFront();
            formulario.FormClosed += new FormClosedEventHandler(CloseForm);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.BackColor = Color.Gainsboro;
            Formulario.frmExploradorReceta formulario = new Formulario.frmExploradorReceta();
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            P_Formularios.Controls.Add(formulario);
            P_Formularios.Tag = formulario;
            formulario.Show();
            formulario.BringToFront();
            formulario.FormClosed += new FormClosedEventHandler(CloseForm);
        }

        private void mdiConsultaExterna_Load(object sender, EventArgs e)
        {
            //if (!NegUtilitarios.IpBodegas())
            //{
            //    this.Close();
            //    return;
            //}
            InicializarForma();
        }

        private void muestraSubMenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                ocultaSubMenu();
                submenu.Visible = true;
            }
            else
                submenu.Visible = false;
        }
        private void DatosUsuario()
        {
            lblDepartamento.Text = Sesion.nomDepartamento;
            lblUsuario.Text = Sesion.nomUsuario;
        }
    }
}
