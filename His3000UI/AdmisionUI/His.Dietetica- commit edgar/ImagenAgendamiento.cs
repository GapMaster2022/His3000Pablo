using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using His.Negocio;
using System.Windows.Forms;
using His.Admision;
using His.Formulario;
using His.Entidades;
using System.IO;
using CuentaPaciente;
using Core.Datos;
using His.Parametros;
using Core.Utilitarios;

namespace His.Dietetica
{
    public partial class ImagenAgendamiento : Form
    {
        string usuario;
        string cod_usuario;

        public ImagenAgendamiento(string x, string y)
        {
            InitializeComponent();
            //monthCalendar1.SelectionRange.Start = (DateTime.Now).AddDays(-30);
            //monthCalendar1.SelectionRange.Start = Convert.ToDateTime(String.Format("{0:g}", (DateTime.Now.Year).ToString() + "/" + DateTime.Now.Month + "/01"));
            monthCalendar1.SelectionRange.Start = DateTime.Now;
            //dtpDesde.Value = (DateTime.Now).AddDays(-30);
            //dtpDesde.Value = Convert.ToDateTime(String.Format("{0:g}", (DateTime.Now.Year).ToString() + "/" + DateTime.Now.Month + "/01"));
            dtpDesde.Value = DateTime.Now;
            monthCalendar1.SelectionRange.End = (DateTime.Now).AddDays(30);
            dtpHasta.Value = (DateTime.Now).AddDays(30);
            actualizarGrid();
            usuario = x; cod_usuario = y;
            
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            actualizarGrid();
        }

        private void actualizarGrid()
        {
            DateTime fi = monthCalendar1.SelectionRange.Start;
            DateTime ff = monthCalendar1.SelectionRange.End;
            ff = ff.AddDays(1);
            string frango = "'" + fi.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "' and '" + ff.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "'";

            grid.DataSource = NegImagen.getAgendamientos(frango);
        }

        private void grpDatos_Click(object sender, EventArgs e)
        {

        }

       

        private void limpiar()
        {
            gridEstudios.Rows.Clear();
            txtObservaciones.Clear();
            txtPaciente.Clear();
            txtRadiologo.Clear();
            txtTecnologo.Clear();
            txtCODRadiologo.Text= "0";
            txtCODTecnologo.Text = "0";
            txtHC.Text = "0";
            
        }

        private void modificar(int x = 0)
        {
            
            

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm_AyudaPacientes form = new frm_AyudaPacientes();
            form.campoPadre = txtHC;

            form.ShowDialog();
            form.Dispose();

            PACIENTES P = NegPacientes.RecuperarPacienteID((form.campoPadre.Text.Trim().ToString()));
            txtPaciente.Visible = true;
            txtPaciente.Text = P.PAC_APELLIDO_PATERNO + " " + P.PAC_APELLIDO_MATERNO + " " + P.PAC_NOMBRE1 + " " + P.PAC_NOMBRE2;
        }

        private void btnAgregaEstudio_Click(object sender, EventArgs e)
        {
            frm_ImagenAyuda ayuda = new frm_ImagenAyuda("PRODUCTOS");
            ayuda.ShowDialog();
            if (ayuda.codigo != string.Empty)
            {
                if (!BuscarItem(ayuda.codigo, gridEstudios))
                    this.gridEstudios.Rows.Add(ayuda.codigo, ayuda.producto, ayuda.adicional1);
                else
                    MessageBox.Show("El item ya fue ingresado.");
            }
        }

        private bool BuscarItem(string searchValue, DataGridView grid)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(searchValue))
                {
                    return true;
                }
            }
            return false;
        }

        private void btnTecnologo_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
                cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), txtTecnologo, txtCODTecnologo);
        }

        private void btnRadiologo_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
                cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), txtRadiologo, txtCODRadiologo);
        }

        private void cargarMedico(int codMedico, TextBox txtNombre, TextBox txtCOD)
        {
            MEDICOS medico = NegMedicos.MedicoID(codMedico);
            if (medico != null)
            {
                txtNombre.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                txtCOD.Text = Convert.ToString(medico.MED_CODIGO);
            }

        }

        private void txtObservaciones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (this.GetNextControl(ActiveControl, true) != null)
                {
                    e.Handled = true;
                    this.GetNextControl(ActiveControl, true).Focus();

                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            pnl1.Visible = true; pnl2.Visible = false;
            limpiar();
            actualizarGrid();
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            pnl1.Visible = false; pnl2.Visible = true; 
            dtpFechaNota.Value = dtpDesde.Value;
            txtIdAgendamiento.Text = "0";

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                pnl1.Visible = true; pnl2.Visible = false;
                USUARIOS u = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
                string[] a = new string[] { DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), txtHC.Text.Trim(), dtpFechaNota.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), txtCODTecnologo.Text.Trim(), txtCODRadiologo.Text.Trim(), txtObservaciones.Text.Trim(), cod_usuario };
                List<PedidoImagen_estudios> ListEstudios = new List<PedidoImagen_estudios>();
                foreach (DataGridViewRow row in gridEstudios.Rows)
                {
                    PedidoImagen_estudios estudio = new PedidoImagen_estudios();
                    estudio.PRO_CODIGO = Convert.ToInt32(row.Cells["ID"].Value);
                    estudio.PRO_CODSUB = Convert.ToInt32(row.Cells["ID"].Value);
                    ListEstudios.Add(estudio);
                }
                NegImagen.saveAgendamiento(Convert.ToInt32(txtIdAgendamiento.Text), a, ListEstudios);



                limpiar();
                actualizarGrid();
            }
            else
            {
                MessageBox.Show("Revise que todos los campos esten llenos.");
            }
            
        }

        private bool validar()
        {
            //if (txtCODRadiologo.Text.Trim() == "0")
            //    return false;
            if (txtRadiologo.Text.Trim() == string.Empty)
                return false;
            if (txtTecnologo.Text.Trim() == string.Empty)
                return false;
            if (txtHC.Text.Trim() == "0")
                return false;
            if (gridEstudios.RowCount == 0)
                return false;
            return true;
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (grid.Rows.Count > 0)
                {
                    pnl1.Visible = false; pnl2.Visible = true;
                    txtIdAgendamiento.Text = grid.ActiveRow.Cells["id"].Value.ToString();
                    DataTable ag = NegImagen.getAgendamiento(grid.ActiveRow.Cells["id"].Value.ToString());
                    txtHC.Text = ag.Rows[0]["ATE_CODIGO"].ToString();
                        PACIENTES P = NegPacientes.RecuperarPacienteID(grid.ActiveRow.Cells["HC"].Value.ToString());
                        txtPaciente.Visible = true;
                        txtPaciente.Text = P.PAC_APELLIDO_PATERNO + " " + P.PAC_APELLIDO_MATERNO + " " + P.PAC_NOMBRE1 + " " + P.PAC_NOMBRE2;
                    txtCODRadiologo.Text = ag.Rows[0]["med_radiologo"].ToString();
                        cargarMedico(Convert.ToInt32(txtCODRadiologo.Text.ToString()), txtRadiologo, txtCODRadiologo);
                    txtCODTecnologo.Text = ag.Rows[0]["med_tecnologo"].ToString();
                        cargarMedico(Convert.ToInt32(txtCODTecnologo.Text.ToString()), txtTecnologo, txtCODTecnologo);
                    txtObservaciones.Text = ag.Rows[0]["observacion"].ToString();
                    dtpFechaNota.Text = (ag.Rows[0]["fecha_agendamiento"].ToString());
                    
                    DataTable est = NegImagen.getAgendamientoEstudios(txtIdAgendamiento.Text);
                    foreach (DataRow row in est.Rows)
                    {
                        gridEstudios.Rows.Add(row["CUE_CODIGO"].ToString(), row["PRO_DESCRIPCION"].ToString());
                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            //DateTime fi = monthCalendar1.SelectionRange.Start;
            //dtpDesde.Value = fi;
            //DateTime ff = monthCalendar1.SelectionRange.End;
            //dtpHasta.Value = ff;
            //ff = ff.AddDays(1);
            //string frango = "'" + fi.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "' and '" + ff.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "'";
            //MessageBox.Show("DateChanged  " + frango);
        }


       

        private void tabulador_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {

        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {

        }

        private String FindSavePath()
        {
            Stream myStream;
            string myFilepath = null;
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xls)|*.xls";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        myFilepath = saveFileDialog1.FileName;
                        myStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myFilepath;
        }

        private void toolStripButtonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (grid.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(grid, PathExcel);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
        }

        private void btnPaciente_Click(object sender, EventArgs e)
        {
            frmAyudaPacientesFacturacion form = new frmAyudaPacientesFacturacion();
            //form.campoPadre = txt_Historia_Pc;
            //form.campoAtencion = txt_Atencion;
            form.ShowDialog();
            //NegFactura.ActualizaDescuentoAtencion(Convert.ToInt16(txt_Atencion.Text));
            //NegValidaciones.alzheimer();// LIBERA MEMORIA





            //if (txt_Historia_Pc.Text.Trim() != "")

            PACIENTES pacienteFactura = NegPacientes.RecuperarPacienteID(form.campoPadre.Text.Trim());
            //   cargarDatosPaciente(pacienteFactura);
            if (pacienteFactura != null)
            {
                txtPaciente.Text = pacienteFactura.PAC_APELLIDO_PATERNO + ' ' + pacienteFactura.PAC_APELLIDO_MATERNO + ' ' + pacienteFactura.PAC_NOMBRE1 + ' ' + pacienteFactura.PAC_NOMBRE2;
                txtHC.Text = form.campoAtencion.Text.Trim();
                HProd();
                //MessageBox.Show(form.campoAtencion.Text + ' ' + form.campoAtencionNumero.Text);
                gridEstudios.Rows.Clear();
            }
                

        }

        private void pnl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            dtpDesde.Value = monthCalendar1.SelectionRange.Start;
            dtpHasta.Value = monthCalendar1.SelectionRange.End;
            actualizarGrid();
        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            monthCalendar1.SetSelectionRange(dtpDesde.Value, dtpHasta.Value);
            actualizarGrid();
        }

        private void dtpHasta_MouseLeave(object sender, EventArgs e)
        {
           // monthCalendar1.SetSelectionRange(dtpDesde.Value, dtpHasta.Value);
        }

        private void dtpHasta_Leave(object sender, EventArgs e)
        {
           // monthCalendar1.SetSelectionRange(dtpDesde.Value, dtpHasta.Value);
        }

        private void HProd()
        {
            

            DataTable ag = NegImagen.getAteImagenCount(Convert.ToInt32(txtHC.Text));
            if ((Convert.ToInt32(ag.Rows[0]["IItems"].ToString()))==0)
                lblAlertaImagen.Visible = true;
            else
                lblAlertaImagen.Visible = false ;
           
        }



        private void btnAgregaEstudio_Click_1(object sender, EventArgs e)
        {
            Ayuda ayuda = new Ayuda("ATE_IMAGEN", Convert.ToInt32(txtHC.Text));
            ayuda.ShowDialog();
            if (ayuda.codigo != string.Empty)
            {
                    this.gridEstudios.Rows.Add(ayuda.codigo, ayuda.producto);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (grid.Rows.Count > 0)
                {
                    lblDelete.Text = "Se cancelara el agendamiento del " + grid.ActiveRow.Cells["fecha_agendamiento"].Value.ToString() + "de Pcte." + grid.ActiveRow.Cells["Paciente"].Value.ToString();
                    pnl1.Visible = false; pnl3.Visible = true;       
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            USUARIOS u = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
           
            if (txtMotivoCancelacion.Text.Trim() != string.Empty)
            {
                NegImagen.deleteAgendamiento( Convert.ToInt32(grid.ActiveRow.Cells["id"].Value), DateTime.Now.ToString() +" por " + usuario + ", motivo: " + txtMotivoCancelacion.Text);
                pnl1.Visible = true; pnl3.Visible = false;
                actualizarGrid();
            }
            else
                MessageBox.Show("Es necesario ingrese el motivo de la cancelacion.");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            pnl1.Visible = true; pnl3.Visible = false;
        }

        private void txtRadiologo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
