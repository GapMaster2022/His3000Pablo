using His.Entidades;
using His.Negocio;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace His.Formulario
{
    public partial class WaitForm : Form
    {
        private int borderRadius = 20;
        private int borderSize = 2;
        private Color borderColor = Color.FromArgb(128, 128, 255);

        public WaitForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Padding = new Padding(borderSize);
            
            this.BackColor = borderColor;
        }

        private void WaitForm_Load(object sender, EventArgs e)
        {
            LOGOS_EMPRESA logEmp = NegParametros.logosEmpresa(10);

            pictureBox2.Image = new Bitmap(logEmp.LEM_RUTA);

            // Inicia la animación del PictureBox
            ImageAnimator.Animate(pictureBox2.Image, OnFrameChanged);
        }
        private void OnFrameChanged(object sender, EventArgs e)
        {
            // Redibuja el PictureBox cuando cambia el marco (frame) del GIF
            pictureBox2.Invalidate();
        }


        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            // Dibuja el marco actual del GIF en el PictureBox
            ImageAnimator.UpdateFrames();
            e.Graphics.DrawImage(pictureBox2.Image, pictureBox2.ClientRectangle);
        }
        private GraphicsPath GetRoundedPath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
        private void FormRegionAndBorder(Form form, float radius, Graphics graph, Color borderColor, float borderSize)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                using (GraphicsPath roundPath = GetRoundedPath(form.ClientRectangle, radius))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                using (Matrix transform = new Matrix())
                {
                    graph.SmoothingMode = SmoothingMode.AntiAlias;
                    form.Region = new Region(roundPath);
                    if (borderSize >= 1)
                    {
                        Rectangle rect = form.ClientRectangle;
                        float scaleX = 1.0F - ((borderSize + 1) / rect.Width);
                        float scaleY = 1.0F - ((borderSize + 1) / rect.Height);
                        transform.Scale(scaleX, scaleY);
                        transform.Translate(borderSize / 1.6F, borderSize / 1.6F);
                        graph.Transform = transform;
                        graph.DrawPath(penBorder, roundPath);
                    }
                }
            }
        }

        private void WaitForm_Paint(object sender, PaintEventArgs e)
        {
            FormRegionAndBorder(this, borderRadius, e.Graphics, borderColor, borderSize);
        }
    }
}
