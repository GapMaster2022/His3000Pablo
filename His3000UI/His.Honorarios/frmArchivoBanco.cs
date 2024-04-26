using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Recursos;

namespace His.Honorarios
{
    public partial class frmArchivoBanco : Form
    {
        
        public frmArchivoBanco(string texto)
        {
            InitializeComponent();
            rtbBancos.Text = texto; 
        }

        private void frmArchivoBanco_Load(object sender, EventArgs e)
        {
            toolStripButtonGuardar.Image = Archivo.imgBtnGuardar32; 
            toolStripButtonImprimir.Image = Archivo.imgBtnImprimir32;
            toolStripButtonCortar.Image = Archivo.imgBtnSearch; 
            toolStripButtonCopiar.Image = Archivo.imgBtnSearch; 
            toolStripButtonPegar.Image = Archivo.imgBtnSearch;
            toolStripButtonSalir.Image = Archivo.imgBtnSalir32; 
        }

        private void toolStripButtonCortar_Click(object sender, EventArgs e)
        {
            rtbBancos.Cut();  
        }

        private void toolStripButtonPegar_Click(object sender, EventArgs e)
        {
            rtbBancos.Paste();  
        }

        private void toolStripButtonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog guardar = new SaveFileDialog();

                guardar.Title = "Guardar archivo de texto como";
                guardar.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";

                DialogResult dr = guardar.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(guardar.FileName);

                    sw.Write(rtbBancos.Text);
                    sw.Close();

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);        
            }

        }

        private void toolStripButtonImprimir_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            pd.ShowDialog();
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
