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
using System.IO;
using System.Collections;
using His.Entidades.Clases;


namespace His.Honorarios
{
    public partial class frm_AsignaLocal : Form
    {
        #region Variables
        public List<LOCALES> locales = new List<LOCALES>();
        #endregion

        #region Constructon
        public frm_AsignaLocal()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frm_AsignaLocal_Load(object sender, EventArgs e)
        {
            try
            {
                locales = NegLocales.ListaLocales();
                cmb_locales.DataSource = locales;
                cmb_locales.ValueMember = "LOC_CODIGO";
                cmb_locales.DisplayMember = "LOC_NOMBRE";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Desea Asignar el Local como predeterminado?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    //const string fic = @"C:\\Archivos de programa\\GapSystem\\His3000\\Control.ini";
                    //string texto = cmb_locales.SelectedValue.ToString();
                    //StreamWriter sw = new StreamWriter(fic, true);
                    //sw.WriteLine(texto);
                    //sw.Close();
                    Sesion.codLocal = Int16.Parse(cmb_locales.SelectedValue.ToString());
                    His.Parametros.ArchivoIni archivo = new His.Parametros.ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
                    archivo.IniWriteValue("Caja", "codigo", cmb_locales.SelectedValue.ToString());
                    
                    MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        #endregion

        
    }
}
