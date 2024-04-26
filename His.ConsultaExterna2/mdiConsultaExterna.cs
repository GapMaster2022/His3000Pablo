using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades.Clases;

namespace ConsultaExterna
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

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            DatosUsuario();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
            oculta();
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
            if (Application.OpenForms["frmCreaPersona"] == null)
            {
                btnCreaCliente.BackColor = Color.FromArgb(31, 195, 216);
            }
            
        }

        #endregion
        private void oculta()
        {
            P_Cliente.Visible = false;
            P_Inventario.Visible = false;
            
            
        }

        

        

        private void btnCreaCliente_Click(object sender, EventArgs e)
        {
            btnCreaCliente.BackColor = Color.Gainsboro;
            //AbrirFormulario<frm_TriajeYSignosVitales>();            
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
