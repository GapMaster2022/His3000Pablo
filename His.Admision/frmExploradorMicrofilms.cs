using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.General;
using Recursos;

namespace His.Admision
{
    public partial class frmExploradorMicrofilms : Form
    {
        string paciente;
        string numeroAtencion;
        string numeroHistoria;
        int codPaciente;
        int codAtencion;
        public frmExploradorMicrofilms()
        {
            InitializeComponent();
        }
        public frmExploradorMicrofilms(string parPaciente,string parNumeroAtencion,string parNumeroHistoria,int parCodPaciente,int parCodAtencion)
        {
            InitializeComponent();
            //
            paciente = parPaciente;
            numeroAtencion = parNumeroAtencion;
            numeroHistoria = parNumeroHistoria; 
            codPaciente = parCodPaciente;
            codAtencion = parCodAtencion;
        }

        private void lvwListaMicrofilms_DragEnter(object sender, DragEventArgs e)
        {
            //if (e.Data.GetDataPresent(DataFormats.Text))
            //{
            //    e.Effect = DragDropEffects.Copy;
            //}
            //else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            //{
            //    e.Effect = DragDropEffects.Copy;
            //}
            //else
            //{
            //    e.Effect = DragDropEffects.None;
            //}

        }

        private void lvwListaMicrofilms_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmExploradorMicrofilms_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void frmExploradorMicrofilms_Load(object sender, EventArgs e)
        {
            //
           lblPaciente.Text =  paciente;
           lblAtencion.Text  = numeroAtencion;
            //
            cargarListaMicrofilms();
            btnAdd.Image = Archivo.imgBtnAdd;
            btnDelete.Image = Archivo.imgBtnDelete;
            //btnOpen.Image = Archivo.imgBtnSelect;   
        }

        private void cargarListaMicrofilms()
        {
            try
            {
                FtpClient clienteFTP = new FtpClient(Parametros.GeneralPAR.getServidorFTP(), Parametros.GeneralPAR.getUsuarioFTP(), Parametros.GeneralPAR.getClaveFTP());
                string direcctorio = Parametros.GeneralPAR.DirectorioMicrofilms + "\\" + numeroHistoria.Trim() + "\\" + numeroAtencion.Trim();
                clienteFTP.ChangeDir(direcctorio);
                string[] listado = clienteFTP.GetFileList();
                //foreach (var item in listado)
                //{
                //    MessageBox.Show(item.ToString()); 
                //}
                lvwListaMicrofilms.DataSource = listado;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);       
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDirectorio.Text.Trim() != "")
                {
                    FtpClient clienteFTP = new FtpClient(Parametros.GeneralPAR.getServidorFTP(), Parametros.GeneralPAR.getUsuarioFTP(), Parametros.GeneralPAR.getClaveFTP());
                    DialogResult respuesta;
                    respuesta = MessageBox.Show("Desea subir el archivo al servidor?", "Subir Archivo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.OK)
                    {

                        clienteFTP.ChangeDir(Parametros.GeneralPAR.DirectorioMicrofilms + "\\" + numeroHistoria.Trim() + "\\" + numeroAtencion.Trim());
                        clienteFTP.Upload(txtDirectorio.Text.Trim());
                        txtDirectorio.Text = null; 
                        cargarListaMicrofilms();
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);   
            }
        }

        private void lvwListaMicrofilms_DoubleClick(object sender, EventArgs e)
        {
            abrirMicrofilms(lvwListaMicrofilms.SelectedItem.ToString());   
        }

        private void lvwListaMicrofilms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                abrirMicrofilms(lvwListaMicrofilms.SelectedItem.ToString());
            }
            else if (e.KeyCode == Keys.Delete )
            {
                eliminarMicrofilms(lvwListaMicrofilms.SelectedItem.ToString());
            }
        }

        private void eliminarMicrofilms(string archivo)
        {
            try
            {
                DialogResult respuesta = new DialogResult();
                respuesta = MessageBox.Show("Desea eliminar el archivo del servidor?", "Eliminar Archivo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (respuesta == DialogResult.OK)
                {
                    FtpClient clienteFTP = new FtpClient(Parametros.GeneralPAR.getServidorFTP(), Parametros.GeneralPAR.getUsuarioFTP(), Parametros.GeneralPAR.getClaveFTP());
                    clienteFTP.DeleteFile(Parametros.GeneralPAR.DirectorioMicrofilms + "\\" + numeroHistoria.Trim() + "\\" + numeroAtencion.Trim() + "\\" + archivo);
                }
                
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void abrirMicrofilms(string archivo)
        {
            try
            {
                //compruebo que existe el directorio
                if (!System.IO.Directory.Exists(Application.LocalUserAppDataPath + "\\"+ Parametros.GeneralPAR.DirectorioMicrofilms))
                {
                    System.IO.Directory.CreateDirectory(Application.LocalUserAppDataPath + "\\" + Parametros.GeneralPAR.DirectorioMicrofilms);
                }
                FtpClient clienteFTP = new FtpClient(Parametros.GeneralPAR.getServidorFTP(), Parametros.GeneralPAR.getUsuarioFTP(), Parametros.GeneralPAR.getClaveFTP());
                clienteFTP.Download(Parametros.GeneralPAR.DirectorioMicrofilms + "\\" + numeroHistoria.Trim() + "\\" + numeroAtencion.Trim() + "\\" + archivo, Application.LocalUserAppDataPath + "\\" + Parametros.GeneralPAR.DirectorioMicrofilms + archivo);
                System.Diagnostics.Process.Start(Application.LocalUserAppDataPath + "\\" + Parametros.GeneralPAR.DirectorioMicrofilms + archivo);
                DialogResult respuesta = new DialogResult();

                respuesta = MessageBox.Show("Desea guardar los cambios en el archivo al servidor?", "Subir Archivo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (respuesta == DialogResult.OK)
                {
                    clienteFTP.ChangeDir(Parametros.GeneralPAR.DirectorioMicrofilms + "\\" + numeroHistoria.Trim() + "\\" + numeroAtencion.Trim());
                    clienteFTP.Upload(Application.LocalUserAppDataPath + "\\" + Parametros.GeneralPAR.DirectorioMicrofilms + archivo);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            DialogResult resultado;
            resultado =file.ShowDialog();
            if (resultado == DialogResult.OK )
            {
                txtDirectorio.Text = file.FileName.ToString(); 
            }
            else
            {
                txtDirectorio.Text = null; 
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvwListaMicrofilms.SelectedItem != null)
            {
                eliminarMicrofilms(lvwListaMicrofilms.SelectedItem.ToString());
                cargarListaMicrofilms();
            }
        }
    }
}
