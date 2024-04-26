using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using His.Entidades.Clases;
using His.Parametros;
using Core.Utilitarios;
using His.Negocio;
using His.Entidades;

namespace His.Facturacion
{
    public partial class MDIFacturacion : Form
    {
        private int childFormNumber = 0;

        public MDIFacturacion()
        {
            InitializeComponent();
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

        private void exploradorDePedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //frmExplorardorPedidos exploradorPedidos = new frmExplorardorPedidos();
            //exploradorPedidos.MdiParent = this;
            //exploradorPedidos.Show();  
        }

        private void MDIPedidos_Load(object sender, EventArgs e)
        {
            //cargo usuario
            //if (Sesion.codUsuario == 0)
            //{
            //    try
            //    {
            //        His.Parametros.ArchivoIni archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
            //        archivo.IniReadValue("Usuario", "usr");
            //        string usuario = archivo.IniReadValue("Usuario", "usr");
            //        string clave = EncriptadorUTF.Desencriptar(archivo.IniReadValue("Usuario", "pwd"));
            //        USUARIOS usuarioarchivo = NegUsuarios.ValidarUsuario(usuario, clave);
            //        if (usuarioarchivo != null)
            //        {
            //            Sesion.codUsuario = usuarioarchivo.ID_USUARIO;
            //            Sesion.nomUsuario = usuarioarchivo.NOMBRES + " " + usuarioarchivo.APELLIDOS;
            //        }
            //        else
            //        {
            //            System.Windows.Forms.MessageBox.Show("El usuario no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //    catch (Exception err)
            //    {
            //        System.Windows.Forms.MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

        private void btnNuevaFactura_Click(object sender, EventArgs e)
        {
            Form form = new frmFactura();
            form.MdiParent = this;
            form.Show();
        }

        private void MDIFacturacion_Load(object sender, EventArgs e)
        {
            string usuario, id_usuario;
            try
            {
                usuario = His.Entidades.Clases.Sesion.nomUsuario;
                id_usuario = Convert.ToString(His.Entidades.Clases.Sesion.codUsuario);
                //if(id_usuario == )
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
