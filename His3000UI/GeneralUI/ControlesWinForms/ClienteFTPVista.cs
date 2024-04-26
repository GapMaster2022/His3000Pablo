using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AlexPilotti.FTPS.Client;
using System.Net;
using His.Parametros;
using System.IO;
using His.Entidades;
using System.Diagnostics;

namespace GeneralApp.ControlesWinForms
{
    public partial class ClienteFTPVista : Form
    {
        #region variables
        private string modo;
        private string carpetaServidor;
        private string dirServidor;
        Int32 _Codigoatencion = 0;
        #endregion

        #region propiedades
        /// <summary>
        /// Propiedad que indica en que modo trabajara la vista
        /// </summary>
        public string Modo
        {
            get { return modo; }
            set { modo = value; }
        }
        /// <summary>
        /// Retorna o ingresa la carpeta remota con la que se trabajara
        /// </summary>
        public string CarpetaServidor
        {
            get { return carpetaServidor; }
            set { carpetaServidor = value; }
        }
        #endregion

        #region constructor
        public ClienteFTPVista(Int32 CodigoAtencion)
        {
            _Codigoatencion = CodigoAtencion;
            InitializeComponent();
        }
        #endregion

        #region metodos

        private void ClienteFTPVista_Load(object sender, EventArgs e)
        {
            switch (modo)
            {
                case "Pedidos":
                    dirServidor = "/" + GeneralPAR.DirectorioPedidos + "/" + carpetaServidor + "/";
                    break;
                case "1":
                    dirServidor = "/" + GeneralPAR.DirectorioMicrofilms + "/" + carpetaServidor + "/";
                    break;
                case "Microfilms":
                    dirServidor = "/" + GeneralPAR.DirectorioMicrofilms + "/" + carpetaServidor + "/";
                    break;

            }
            //cargo reursos
            btnSubir.Image = Recursos.Archivo.btnNuevo16;
            btnEliminar.Image = Recursos.Archivo.btnEliminar16;
            //inicializo componentes
            crearDirectorioRemoto(dirServidor);
            cargarLstArchivos();
        }

        private bool crearDirectorioRemoto(string directorioRemoto)
        {
            try
            {
                using (FTPSClient client = new FTPSClient())
                {
                    client.Connect(GeneralPAR.getServidorFTP(),
                                   new NetworkCredential(GeneralPAR.getUsuarioFTP(),
                                                         GeneralPAR.getClaveFTP()),
                                   ESSLSupportMode.ControlAndDataChannelsRequested);
                    client.MakeDir(directorioRemoto);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        private void cargarLstArchivos()
        {
            string CarpetaMicrofilm = "";
            try
            {
                //using (FTPSClient client = new FTPSClient())
                //{
                //    client.Connect(GeneralPAR.getServidorFTP(),
                //                   new NetworkCredential(GeneralPAR.getUsuarioFTP(),
                //                                         GeneralPAR.getClaveFTP()),
                //                   ESSLSupportMode.ControlAndDataChannelsRequested);
                //    List<AlexPilotti.FTPS.Common.DirectoryListItem> lista = client.GetDirectoryList("x:\\pruebas").ToList();
                //    dgvArchivos.DataSource = lista.Select(a => new { Nombre=a.Name, Creacion=a.CreationTime }).ToList();
                //}

                List<DtoDatosMicrofilms> ListaArchivos = new List<DtoDatosMicrofilms>();

                string[] dirs = Directory.GetDirectories(@"X:\HISTORIAS DIGITALES", "*" + _Codigoatencion.ToString() + "*.*");


                if (dirs.Count() > 0)
                {

                    foreach (string dir in dirs)
                    {
                        CarpetaMicrofilm = dir;
                    }

                }

                string[] dirs1 = Directory.GetFiles(@CarpetaMicrofilm, "*.*");

                foreach (string dir in dirs1)
                {

                    DtoDatosMicrofilms Archivos = new DtoDatosMicrofilms();
                    Archivos.Codigo = 0;
                    Archivos.Nombre = dir;

                    ListaArchivos.Add(Archivos);

                    Archivos = null;

                }

                dgvArchivos.DataSource = ListaArchivos;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void cargarLstArchivos()
        //{



        //}

        private void BajarArchivo(string nombreArchivo)
        {
            try
            {
                using (FTPSClient client = new FTPSClient())
                {
                    //compruebo que existe el directorio
                    if (!System.IO.Directory.Exists(Application.LocalUserAppDataPath + "\\" + GeneralPAR.DirectorioPedidos))
                    {
                        System.IO.Directory.CreateDirectory(Application.LocalUserAppDataPath + "\\" + GeneralPAR.DirectorioPedidos);
                    }
                    client.Connect(GeneralPAR.getServidorFTP(),
                                   new NetworkCredential(GeneralPAR.getUsuarioFTP(),
                                                         GeneralPAR.getClaveFTP()),
                                   ESSLSupportMode.ControlAndDataChannelsRequested);
                    this.Cursor = Cursors.WaitCursor;
                    client.GetFile(dirServidor + nombreArchivo, Application.LocalUserAppDataPath + "\\" + GeneralPAR.DirectorioPedidos + "\\" + nombreArchivo);
                    System.Diagnostics.Process.Start(Application.LocalUserAppDataPath + "\\" + GeneralPAR.DirectorioPedidos + "\\" + nombreArchivo);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
            }
        }
        private void BorrarArchivo(string nombreArchivo)
        {
            try
            {
                DialogResult resultado = MessageBox.Show("Esta seguro que desea eliminar el archivo?", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (resultado == DialogResult.OK)
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(GeneralPAR.getServidorFTP(),
                                       new NetworkCredential(GeneralPAR.getUsuarioFTP(),
                                                             GeneralPAR.getClaveFTP()),
                                       ESSLSupportMode.ControlAndDataChannelsRequested);
                        client.DeleteFile(dirServidor + nombreArchivo);
                        cargarLstArchivos();
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region eventos

       

        private void btnSubir_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog expArchivos = new OpenFileDialog
                {
                    Filter = "txt files (*.pdf)|*.pdf|All files (*.*)|*.*"
                };
                if (expArchivos.ShowDialog() == DialogResult.OK)
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(GeneralPAR.getServidorFTP(),
                                       new NetworkCredential(GeneralPAR.getUsuarioFTP(),
                                                             GeneralPAR.getClaveFTP()),
                                       ESSLSupportMode.ControlAndDataChannelsRequested);
                        this.Cursor = Cursors.WaitCursor;
                        client.PutFile(expArchivos.FileName, dirServidor + expArchivos.SafeFileName);
                        this.Cursor = Cursors.Default;
                        cargarLstArchivos();
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvArchivos.CurrentRow != null)
            {
                BorrarArchivo(dgvArchivos.CurrentRow.Cells["Nombre"].Value.ToString());
            }
            else
            {
                MessageBox.Show("Por favor seleccione el archivo que desea eliminar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvArchivos_DoubleClick(object sender, EventArgs e)
        {
            if (dgvArchivos.CurrentRow != null)
            {
                BajarArchivo(dgvArchivos.CurrentRow.Cells["Nombre"].Value.ToString());
            }
            else
            {
                MessageBox.Show("Por favor seleccione el archivo que desea abrir", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvArchivos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgvArchivos.CurrentRow != null)
                {
                    BajarArchivo(dgvArchivos.CurrentRow.Cells["Nombre"].Value.ToString());
                }
                else
                {
                    MessageBox.Show("Por favor seleccione el archivo que desea abrir", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (dgvArchivos.CurrentRow != null)
                {
                    BorrarArchivo(dgvArchivos.CurrentRow.Cells["Nombre"].Value.ToString());
                }
                else
                {
                    MessageBox.Show("Por favor seleccione el archivo que desea eliminar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnMostrarPdf_Click(object sender, EventArgs e)
        {
            string Path = "";
            Path = dgvArchivos.Rows[Convert.ToInt32(dgvArchivos.CurrentRow.Index)].Cells[1].Value.ToString();
            Process.Start(Path);
        }


    }
}
