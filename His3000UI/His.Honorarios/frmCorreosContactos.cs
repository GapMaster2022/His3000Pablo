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

namespace His.Honorarios
{
    public partial class frmCorreosContactos : Form
    {
        public string ToLista=null;
        public string CCLista = null;
        public string CCOLista = null;
        public frmCorreosContactos()
        {
            InitializeComponent();
            cargarControles();
        }

        private void cargarControles()
        {
            cboTipo.Items.Clear();
            cboTipo.Items.Add("Medicos");
            cboTipo.Items.Add("Usuarios");
            cboTipo.SelectedIndex = 0; 
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ToLista=null;
            CCLista = null;
            CCOLista = null;
            this.Close(); 
        }

        private void frmCorreosContactos_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ToLista = txtTo.Text;
            CCLista = txtCC.Text;
            CCOLista = txtCCO.Text;
            this.Close();
        }

        private void cboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTipo.SelectedIndex == 0)
                {
                    List<ESPECIALIDADES_MEDICAS> lista = NegMedicos.RecuperaTipoEspecialidad();
                    ESPECIALIDADES_MEDICAS todos = new ESPECIALIDADES_MEDICAS();
                    todos.ESP_CODIGO = 0;
                    todos.ESP_NOMBRE = " Todas ";
                    lista.Add(todos); 
                    cboCategoria.DataSource = lista.OrderBy( l=>l.ESP_NOMBRE).ToList() ;
                    cboCategoria.DisplayMember = "ESP_NOMBRE";
                    cboCategoria.ValueMember = "ESP_CODIGO";
                }
                else if (cboTipo.SelectedIndex == 1)
                {
                    cboCategoria.DataSource = null;
                }
                else
                {
                    cboCategoria.DataSource = null;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboTipo_DisplayMemberChanged(object sender, EventArgs e)
        {

        }

        private void cboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                ESPECIALIDADES_MEDICAS especialidad= (ESPECIALIDADES_MEDICAS)cboCategoria.SelectedItem;
                List<MEDICOS> listaMedicos;
                if (especialidad.ESP_CODIGO > 0)
                {
                    listaMedicos = NegMedicos.listaCorreosMedicosPorEspecialidad(especialidad.ESP_CODIGO.ToString());
                    
                }
                else
                {
                    listaMedicos = NegMedicos.listaCorreosMedicosPorEspecialidad(null);
                }
                dbdgListaContactos.DataSource = listaMedicos.Select(m => new { Medico = (m.MED_APELLIDO_PATERNO + " " + m.MED_APELLIDO_MATERNO + " " + m.MED_NOMBRE1 + " " + m.MED_NOMBRE2), Email = m.MED_EMAIL }).ToList();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);      
            }
        }

        private void dbdgListaContactos_DoubleClick(object sender, EventArgs e)
        {
            //try { 
            //    if( dbdgListaContactos.CurrentRow !=null)
            //    {
            //        if (txtTo.Text.Trim()=="" )
            //        {
            //            txtTo.Text = txtTo.Text + dbdgListaContactos.CurrentRow.Cells["Email"].Value.ToString() ; 
            //        }
            //        else
            //        {
            //            txtTo.Text = txtTo.Text + ";"+ dbdgListaContactos.CurrentRow.Cells["Email"].Value.ToString(); 
            //        }
            //    }
            //}
            //catch(Exception err) {
            //    MessageBox.Show(err.Message );  
            //}
            addEmail();
        }

        private void dbdgListaContactos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTo_Click(object sender, EventArgs e)
        {
            addEmail();
        }

        private void addEmail()
        {
            try
            {
                foreach (DataGridViewRow fila in dbdgListaContactos.SelectedRows)
                {
                    if (txtTo.Text.Trim() == "")
                    {
                        txtTo.Text = txtTo.Text + fila.Cells["Email"].Value.ToString();
                    }
                    else
                    {
                        if(!txtTo.Text.Contains(fila.Cells["Email"].Value.ToString().Trim()))   
                            txtTo.Text = txtTo.Text + ";" + fila.Cells["Email"].Value.ToString();
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnCC_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow fila in dbdgListaContactos.SelectedRows)
                {
                    if (txtCC.Text.Trim() == "")
                        txtCC.Text = txtCC.Text + fila.Cells["Email"].Value.ToString();
                    else
                        txtCC.Text = txtCC.Text + ";" + fila.Cells["Email"].Value.ToString();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnCCO_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow fila in dbdgListaContactos.SelectedRows)
                {
                    if (txtCCO.Text.Trim() == "")
                        txtCCO.Text = txtCCO.Text + fila.Cells["Email"].Value.ToString();
                    else
                        txtCCO.Text = txtCCO.Text + ";" + fila.Cells["Email"].Value.ToString();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

    }
}
