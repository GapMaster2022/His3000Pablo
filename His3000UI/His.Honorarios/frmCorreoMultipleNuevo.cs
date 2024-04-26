using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using System.Net.Mail;
using Recursos;

namespace His.Honorarios
{
    public partial class frmCorreoMultipleNuevo : Form
    {
        List<MEDICOS> listaMedicos; 
        List<string[]> listaAdjuntos;
        int codigoMedico;
        int contadorAdjuntos;
        public frmCorreoMultipleNuevo()
        {
            InitializeComponent();
            //inicializo variables
            listaAdjuntos = new List<string[]>();
            contadorAdjuntos = 0;
        }

        private void frmCorreoMultipleNuevo_Load(object sender, EventArgs e)
        {
            //
            //cargo recursos
            nuevoToolStripButton.Image = Archivo.imgBtnGoneNew48;
            abrirToolStripButton.Image = Archivo.imgBtnGoneOpen48;
            guardarToolStripButton.Image = Archivo.imgBtnGoneSave48;
            salirToolStripButton.Image = Archivo.imgBtnGoneExit48; 
            //recupera la lista de medicos con correos
            listaMedicos = Negocio.NegMedicos.listaCorreosMedicosPorEspecialidad(null);
            //ultraGridMedicos.DataSource
            ultraGridMedicos.DataSource = listaMedicos.Select(m => new
            {
                        Codigo = m.MED_CODIGO   
                        ,Medico = m.MED_APELLIDO_PATERNO + " " + m.MED_APELLIDO_MATERNO + " "+ m.MED_NOMBRE1 + " "+m.MED_NOMBRE2
                        ,Correo = m.MED_EMAIL}).ToList();

  
        }
        private void cargarListaAdjuntos(int parCodigoMedico)
        {
            listViewArchivos.Items.Clear();
            if (listaAdjuntos.Count > 0)
            {
                var adjuntosPorMedico = (from a in listaAdjuntos
                                        where a[3] == parCodigoMedico.ToString()
                                        select a).ToList() ;
                if (adjuntosPorMedico.Count > 0)
                {
                    foreach (var fila in adjuntosPorMedico)
                    {
                        ListViewItem item = new ListViewItem();
                        item = listViewArchivos.Items.Add(fila[0]);
                        item.SubItems.Add(fila[1]);
                        item.SubItems.Add(fila[2]);

                    }
                }
            }

        }

        private void ultraGridMedicos_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

                //Caracteristicas de Filtro en la grilla
                ultraGridMedicos.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                ultraGridMedicos.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                ultraGridMedicos.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                ultraGridMedicos.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
                //Añado la columna check
                if (ultraGridMedicos.DisplayLayout.Bands[0].Columns.Count<=3)
                {
                    ultraGridMedicos.DisplayLayout.Bands[0].Columns.Add("check", "");
                    ultraGridMedicos.DisplayLayout.Bands[0].Columns["check"].DataType = typeof(bool);
                    ultraGridMedicos.DisplayLayout.Bands[0].Columns["check"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;

                    ultraGridMedicos.DisplayLayout.Bands[0].Columns.Add("Cantidad", "Cantidad");
                    ultraGridMedicos.DisplayLayout.Bands[0].Columns["Cantidad"].DataType = typeof(int);
                    ultraGridMedicos.DisplayLayout.Bands[0].Columns["Cantidad"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Integer;
                }
                ultraGridMedicos.DisplayLayout.Bands[0].Columns["Codigo"].Hidden = true; 

                ultraGridMedicos.DisplayLayout.Bands[0].Columns["Medico"].Width = 200;
                ultraGridMedicos.DisplayLayout.Bands[0].Columns["Correo"].Width = 200;
                ultraGridMedicos.DisplayLayout.Bands[0].Columns["check"].Width = 50;
                ultraGridMedicos.DisplayLayout.Bands[0].Columns["Cantidad"].Width = 30;

                ultraGridMedicos.DisplayLayout.Bands[0].Columns["check"].Header.VisiblePosition = 0;
                ultraGridMedicos.DisplayLayout.Bands[0].Columns["Medico"].Header.VisiblePosition = 1;
                ultraGridMedicos.DisplayLayout.Bands[0].Columns["Correo"].Header.VisiblePosition = 2;
                ultraGridMedicos.DisplayLayout.Bands[0].Columns["Cantidad"].Header.VisiblePosition = 3;


            foreach (UltraGridRow fila in ultraGridMedicos.Rows)
            {
                fila.Cells["check"].Value = false;
                fila.Cells["Cantidad"].Value = 0;
            }
        }

        private void ultraGridMedicos_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (ultraGridMedicos.ActiveRow != null)
            {
                if (ultraGridMedicos.ActiveRow.Cells["Codigo"].Text != "")
                {
                    codigoMedico = Convert.ToInt32(ultraGridMedicos.ActiveRow.Cells["Codigo"].Text);

                    if (Convert.ToBoolean(ultraGridMedicos.ActiveRow.Cells["check"].Value))
                    {
                        cargarListaAdjuntos(codigoMedico);
                        toolStripMedicos.Enabled = true;
                    }
                    else
                    {
                        toolStripMedicos.Enabled = false;
                        listViewArchivos.Items.Clear();
                    }
                }
            }

        }

        private void toolStripButtonNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialogo = new OpenFileDialog();
                dialogo.Filter = "Todos los archivos|*.*";
                DialogResult resultado = dialogo.ShowDialog();
                if (resultado==DialogResult.OK )
                {
                    String[] elementosPorDefecto = new String[4] {contadorAdjuntos.ToString(), dialogo.SafeFileName, dialogo.FileName, codigoMedico.ToString() };
                    contadorAdjuntos++;
                    listaAdjuntos.Add(elementosPorDefecto);
                    cargarListaAdjuntos(codigoMedico); 
                }
                
            }
            catch (Exception err)
            {
                MessageBox.Show("error", err.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (listViewArchivos.SelectedItems.Count > 0)
            {
                foreach (var item in listViewArchivos.SelectedItems)
                { 

                }
            }
        }

        private void ultraGridMedicos_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                if (ultraGridMedicos.ActiveRow != null)
                {
                    if (ultraGridMedicos.ActiveRow.Cells["check"].Value.ToString() != "")
                    {
                        if (Convert.ToBoolean(ultraGridMedicos.ActiveRow.Cells["check"].Value) == true)
                        {
                            listViewArchivos.Items.Clear();
                            var limpiarAdjuntos = (from a in listaAdjuntos
                                                   where a[3] == codigoMedico.ToString()
                                                   select a).ToList();
                            if (limpiarAdjuntos.Count > 0)
                            {
                                foreach (var item in limpiarAdjuntos)
                                {
                                    listaAdjuntos.Remove(item);
                                }
                            }
                            toolStripMedicos.Enabled = false;
                        }
                        else
                        {
                            toolStripMedicos.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message); 
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {

            if (!txtAsunto.Text.Equals(""))
            {
                MessageBox.Show("Por favor ingrese el asunto antes de enviar el correo","Inf",MessageBoxButtons.OK,MessageBoxIcon.Information);     
                return;
            }
            if (listaAdjuntos.Count == 0)
            {
                MessageBox.Show("Por favor adjunte un archivo antes de enviar el correo", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            try
            {
                SmtpClient clienteSMTP = new SmtpClient();
                //
                clienteSMTP.Host = Parametros.HonorariosPAR.SmtpDireccionHost;
                clienteSMTP.Port = Parametros.HonorariosPAR.SmtpPuerto;
                if (Parametros.HonorariosPAR.SmtpCredencial)
                {
                    clienteSMTP.Credentials = new System.Net.NetworkCredential(Parametros.HonorariosPAR.SmtpCorreoUsr, Parametros.HonorariosPAR.SmtpCorreoPwd);
                }
                //Parametriso los campos del correo electronico
                //Definimos nuestro correo
                int medCodigo;
                //Añadimos un adjunto.
                foreach (var medicos in ultraGridMedicos.Rows)
                {
                    MailMessage correo = new MailMessage();
                    if (Convert.ToBoolean(medicos.Cells["check"].Value) == true)
                    {
                        medCodigo = Convert.ToInt32(medicos.Cells["Codigo"].Text);
                        var adjuntosPorMedico = (from a in listaAdjuntos
                                                 where a[3] == medCodigo.ToString()
                                                 select a).ToList();
                        if (adjuntosPorMedico.Count > 0)
                        {
                            foreach (var fila in adjuntosPorMedico)
                            {
                                Attachment adjuntos = new Attachment(fila[2]);
                                if (adjuntos != null)
                                {
                                    correo.Attachments.Add(adjuntos);
                                }
                            }
                        }
                        //Direccion desde la que enviamos.
                        correo.From = new MailAddress(Parametros.HonorariosPAR.SmtpCorreo);
                        correo.Subject = txtAsunto.Text;
                        correo.Body = cuerpoCorreo.Text;
                        correo.To.Add(new MailAddress(medicos.Cells["Correo"].Value.ToString()));
                        correo.CC.Add(new MailAddress(txtCopiaCorreo.Text));
                        clienteSMTP.Send(correo);
                    }
                }

                MessageBox.Show("Mensaje enviado", "Info", MessageBoxButtons.OK, MessageBoxIcon.None);


                           }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void nuevoToolStripButton_Click(object sender, EventArgs e)
        {
            txtCopiaCorreo.Text = "";
            txtAsunto.Text = "";
            cuerpoCorreo.Text = "";
            listaAdjuntos = new List<string[]>();
            contadorAdjuntos = 0;
            listaMedicos = Negocio.NegMedicos.listaCorreosMedicosPorEspecialidad(null);
            //ultraGridMedicos.DataSource
            ultraGridMedicos.DataSource = listaMedicos.Select(m => new
            {
                Codigo = m.MED_CODIGO
                        ,
                Medico = m.MED_APELLIDO_PATERNO + " " + m.MED_APELLIDO_MATERNO + " " + m.MED_NOMBRE1 + " " + m.MED_NOMBRE2
                        ,
                Correo = m.MED_EMAIL
            }).ToList();
        }

        private void salirToolStripButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
