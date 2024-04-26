using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using His.Negocio;

namespace His.Honorarios
{
    partial class frm_ParametrosHonorarios : Form
    {

        public int caja;
        public string nomUsuario = string.Empty;
        public string nomCaja = string.Empty;
        public frm_ParametrosHonorarios()
        {
            InitializeComponent();
        }

        #region Descriptores de acceso de atributos de ensamblado

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmReportes frm = new frmReportes();
            frm.reporte = "rHonorariosDiarios";
            frm.campo1 = new DateTime(dateTimePickerDesde.Value.Date.Year, dateTimePickerDesde.Value.Date.Month, dateTimePickerDesde.Value.Date.Day, 23, 59, 59).ToString();
            frm.campo2 = new DateTime(dateTimePickerHasta.Value.Date.Year, dateTimePickerHasta.Value.Date.Month, dateTimePickerHasta.Value.Date.Day, 23, 59, 59).ToString();
            //frm.campo1 = dateTimePickerDesde.Value.Date.ToString();
            //frm.campo2 = dateTimePickerHasta.Value.Date.ToString();
            frm.campo3 = caja.ToString();
            frm.campo4 = His.Entidades.Clases.Sesion.codUsuario.ToString();
            frm.Show();
            
        }

        private void dateTimePickerDesde_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                dateTimePickerHasta.Focus();
            }
        }

        private void dateTimePickerHasta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                button1.Focus();
            }
        }

    }
}
