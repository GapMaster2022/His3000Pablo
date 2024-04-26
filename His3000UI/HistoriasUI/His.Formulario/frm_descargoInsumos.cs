using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades.Clases;
using His.Entidades;

namespace His.Formulario
{
    public partial class frm_descargoInsumos : Form
    {
        string ateCodigo;
        TextBox codMedicamento = new TextBox();
        public frm_descargoInsumos()
        {
            InitializeComponent();
        }
        public frm_descargoInsumos(string ate_codigo)
        {
            InitializeComponent();
            ateCodigo = ate_codigo;            
            DataTable paciente = new DataTable();
            paciente = NegFormulariosHCU.Paciente(ateCodigo);
            lblPaciente.Text = paciente.Rows[0][0].ToString();
            lblCodPaciente.Text = paciente.Rows[0][1].ToString();
            lblFecha.Text = Convert.ToString(DateTime.Now);
            lblHora.Text = Convert.ToString(DateTime.Now.Hour + ":" + DateTime.Now.Minute);
            lblUsuario.Text = Sesion.nomUsuario.ToString();

        }

        

        private void btnMedicamento_Click(object sender, EventArgs e)
        {
            int check = 0;
            if (checkBox1.Checked)
                check = 1;
            frm_AyudaKardex kardex = new frm_AyudaKardex(ateCodigo, 27, check);
            kardex.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            
            kardex.ShowDialog();
            lblMedicamento.Text = kardex.medicamento;
            codMedicamento.Text = kardex.cue_codigo;
            //cantidad.Text = kardex.cantidad;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                lblMedicamento.ReadOnly = false;
                lblMedicamento.Text = "";
                btnMedicamento.Enabled = false;
            }
            else
            {
                lblMedicamento.ReadOnly = true;
                lblMedicamento.Text = "";
                btnMedicamento.Enabled = true;
            }
        }

        public bool Valida()
        {
            if (lblMedicamento.Text != "")
                return true;
            else
            {
                MessageBox.Show("Ingrese Insumo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (Valida())
            {
                bool ok = false;
                IngresaKardex objIngresa = new IngresaKardex();
                objIngresa.presentacion = lblMedicamento.Text;                
                objIngresa.hora = Convert.ToDateTime(DateTime.Now.ToString("hh:mm"));
                objIngresa.ate_codigo = ateCodigo;
                if (checkBox1.Checked)
                {
                    objIngresa.id_kardex = 0;
                    objIngresa.medPropio = true;
                }
                else
                {
                    objIngresa.id_kardex = Convert.ToInt64(codMedicamento.Text);//este es el codigo cue_cuenta para poder verificar que medicamento ya fue puesto en kardex                
                    objIngresa.medPropio = false;
                }
                ok = NegFormulariosHCU.IngresaKardexInsumo(objIngresa, Sesion.codUsuario);
                if (ok)
                {
                    dtgCabeceraKardex.Rows.Add(new string[]{
                    Convert.ToString(codMedicamento.Text),
                    Convert.ToString(lblMedicamento.Text),                    
                    Convert.ToString(DateTime.Now.ToString("hh:mm"))
                });
                    lblCodMedicamento.Text = "";
                    codMedicamento.Text = "";                    
                    lblMedicamento.Text = "";
                    MessageBox.Show("Kardex Actualizado Con Exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Kardex No Se Actualizo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Si Continua Perdera La Información Sin Cargar", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
            
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            frmReportes rep = new frmReportes();
            rep.campo1 = ateCodigo;
            rep.reporte = "KardexInsumos";
            rep.ShowDialog();
            rep.Dispose();
        }
    }
}
