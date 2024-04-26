using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Recursos;
using His.Entidades;
using His.Entidades.Clases;
using His.Negocio;
using System.Net.Mail;
namespace His.Honorarios
{
    public partial class frmCorreosNuevo : Form
    {
        private USUARIOS emisor;
        //variables 
        //variables del correo
        MailMessage correo;
        public frmCorreosNuevo()
        {
            InitializeComponent();
            //inicializo variables
            correo = new MailMessage();
            //
            if (Sesion.codUsuario > 0)
                emisor = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
            else
                MessageBox.Show("Usuario no encontrado, por favor vuelva a ingresar al sistema", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);      
            //cargo recursos
            cargarRecursos();
        }

        private void limpiarCampos()
        {
 
        }

        private void cargarRecursos() {
            try
            {
                //imagenes
                nuevoToolStripButton.Image = Archivo.imgBtnGoneNew48;
                abrirToolStripButton.Image = Archivo.imgBtnGoneOpen48;
                guardarToolStripButton.Image = Archivo.imgBtnGoneSave48;
                adjuntarToolStripButton.Image = Archivo.imgBtnGoneMailAttachment48;
                salirToolStripButton.Image = Archivo.imgBtnGoneExit48; 
                //datos
                lblEmisor.Text = emisor.APELLIDOS + " " + emisor.NOMBRES; 
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);   
            }
        }
        private void frmCorreos_Load(object sender, EventArgs e)
        {
             
        }

        private void salirToolStripButton_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void abrirToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "Archivos plantilla (*.plt)|*.plt";
                file.ShowDialog();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);   
            }
        }

        private void adjuntarToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "Todos los archivos (*.*)|*.*";
                file.ShowDialog();
                if (file.FileName!="")
                {
                    lblAdjuntos.Text = lblAdjuntos.Text + " " + file.SafeFileName;
                    correo.Attachments.Add(new Attachment(file.FileName));     
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void guardarToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog file = new SaveFileDialog();
                file.Filter = "Archivos plantilla(*.plt)|*.plt";
                file.ShowDialog();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);   
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            bool noEnviar = false;
            if (txtPara.Text.Equals(""))
            {
                noEnviar = true;
            }
            if (txtAsunto.Text.Equals(""))
            {
                noEnviar = true;
            }
            if (noEnviar == true)
            {
                return;
            }
            try
            {
                //
                //
                SmtpClient clienteSMTP = new SmtpClient(); 
                //
                clienteSMTP.Host =  Parametros.HonorariosPAR.SmtpDireccionHost;
                clienteSMTP.Port = Parametros.HonorariosPAR.SmtpPuerto;   
                if(Parametros.HonorariosPAR.SmtpCredencial)
                {
                    clienteSMTP.Credentials = new System.Net.NetworkCredential(Parametros.HonorariosPAR.SmtpCorreoUsr,Parametros.HonorariosPAR.SmtpCorreoPwd);

                }
                //Parametriso los campos del correo electronico
                        //Definimos nuestro correo

        //Añadimos un adjunto.
                if (!lblAdjuntos.Text.Equals("") && lblAdjuntos.Text.Length >7)
                {
                    //
                    Attachment adjuntos = new Attachment(lblAdjuntos.Text); 
                    if (adjuntos != null)
                    {
                        correo.Attachments.Add(adjuntos);

                    }
                }
        //Direccion desde la que enviamos.
        correo.From = new MailAddress(Parametros.HonorariosPAR.SmtpCorreo);
        correo.Subject = txtAsunto.Text;
        correo.Body = cuerpoCorreo.Text;
        correo.To.Add(new MailAddress(txtPara.Text));
        correo.CC.Add(new MailAddress(txtCopiaCorreo.Text ));
        //correo.Bcc.Add(new MailAddress(txt  

        clienteSMTP.Send(correo);
        
        MessageBox.Show("Mensaje enviado", "Info", MessageBoxButtons.OK, MessageBoxIcon.None);
        

                //mm.From = fromText.Text; 
                //mm.Cc = ccText.Text;
                //mm.Bcc = bccText.Text;
                //mm.Subject = subjectText.Text;

                ////The body of the message text
                //mm.Body = messageText.Text;

                ////The Priority of the mail
                //switch (settingDialog.PrEnum)
                //{
                //    case Priorities.High:
                //        mm.Priority = MailPriority.High;
                //        break;
                //    case Priorities.Normal:
                //        mm.Priority = MailPriority.Normal;
                //        break;
                //    case Priorities.Low:
                //        mm.Priority = MailPriority.Low;
                //        break;
                //    default:
                //        break;
                //}

                ////The encoding format of the mail
                //switch (settingDialog.EdEnum)
                //{
                //    case Encodings.ASCII:
                //        mm.BodyEncoding = Encoding.ASCII;
                //        break;
                //    case Encodings.Unicode:
                //        mm.BodyEncoding = Encoding.Unicode;
                //        break;
                //    case Encodings.UTF7:
                //        mm.BodyEncoding = Encoding.UTF7;
                //        break;
                //    case Encodings.UTF8:
                //        mm.BodyEncoding = Encoding.UTF8;
                //        break;
                //    default:
                //        break;
                //}

                ////Format of mail is in HTML format
                //mm.BodyFormat = MailFormat.Html;


                ////to Send out the mail
                //SmtpMail.Send(mm);
                //MessageBox.Show("Message Sent", "Info", MessageBoxButtons.OK, MessageBoxIcon.None);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void cuerpoCorreo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPara_Click(object sender, EventArgs e)
        {
            frmCorreosContactos contactos = new frmCorreosContactos();
            contactos.ShowDialog();
            if (contactos.ToLista != null)
                txtPara.Text = contactos.ToLista;
            if (contactos.CCLista != null)
                txtCopiaCorreo.Text = contactos.CCLista;  
        }

        private void nuevoToolStripButton_Click(object sender, EventArgs e)
        {
            txtPara.Text = "";
            txtCopiaCorreo.Text = "";
            txtAsunto.Text = "";
            lblAdjuntos.Text = "";
            cuerpoCorreo.Text = "";
        }

        private void btnCopiaCorreo_Click(object sender, EventArgs e)
        {
            frmCorreosContactos contactos = new frmCorreosContactos();
            contactos.ShowDialog(); 
        }
    }
}
