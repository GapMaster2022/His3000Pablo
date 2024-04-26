using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;

namespace His.Honorarios
{
    public partial class frmAsientoContable : Form
    {
        public Int32 codMedico = 0;
        public string facturaMedico = "";
        public frmAsientoContable(string factura, Int32 medico)
        {
            InitializeComponent();
            codMedico = medico;
            facturaMedico = factura;
            DataTable datos = new DataTable();
            datos = NegHonorariosMedicos.ConsultaAsiento(facturaMedico, codMedico);
            if (datos.Rows.Count > 0)
            {
                lblAsiento.Text = datos.Rows[0][0].ToString();
                lblFechaAsiento.Text = datos.Rows[0][1].ToString().Substring(0,10);
                datos = NegHonorariosMedicos.ConsultaRetencion(Convert.ToInt64(datos.Rows[0][2].ToString()));
                if (datos.Rows.Count > 0)
                {
                    lblRetencion.Text = datos.Rows[0][0].ToString();
                    string fecha = datos.Rows[0][1].ToString();
                    lblFechaRet.Text = fecha.Substring(0,10);
                }
                else
                {
                    lblRetencion.Text = "SIN RETENCIÓN";
                    lblFechaRet.Text = "";
                }
            }
            else
            {
                MessageBox.Show("No hay información para mostrar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }



        
    }
}
