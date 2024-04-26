using His.Admision;
using His.Entidades;
using His.Entidades.Clases;
using His.Entidades.Pedidos;
using His.HabitacionesUI;
using His.Honorarios;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using Recursos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Pedidos
{
    public partial class frmDespachos : Form
    {
        Timer Actualizar = new Timer();
        bool Busqueda = true;
        public frmDespachos()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Busqueda = false;
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dtpdesde.Value.Date > dtphasta.Value)
                MessageBox.Show("La fecha \"Desde\" no pueder ser mayor a fecha \"Hasta\"", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                //frmTiempodeEspera xy = new frmTiempodeEspera();
                UltraGridDespachos.Visible = false;
                VerDespachos();
                //xy.ShowDialog();
                UltraGridDespachos.Visible = true;
                Busqueda = true;
            }
        }
        public void VerDespachos()
        {
            try
            {
                string piso = "0";
                string hab = "0";
                if (chkPiso.Checked)
                    piso = cmbPiso.SelectedValue.ToString();
                if (chkHab.Checked)
                {
                    hab = cmbHabitacion.SelectedValue.ToString();
                }
                UltraGridDespachos.DataSource = null;
                UltraGridDespachos.DataSource = NegPedidos.VerDespacho(dtpdesde.Value.Date, dtphasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59), 
                    chkIngreso.Checked, chkDespacho.Checked, txt_historiaclinica.Text.Trim(), piso, hab, Convert.ToInt32(txtped_codigo.Text.Trim()) );

                foreach (var item in UltraGridDespachos.Rows)
                {
                    if (item.Cells["despachados"].Text == "True")
                    {
                        item.Cells["DESPACHADO"].Value = true;
                        UltraGridDespachos.DisplayLayout.Bands[0].Columns["DESPACHADO"].CellActivation = Activation.NoEdit;
                        //UltraGridDespachos.DisplayLayout.Bands[0].Columns["OBSERVACIONES"].CellActivation = Activation.NoEdit;
                        //if (item.Cells["OBSERVACION"].Value.ToString() != "")
                        //    item.Cells["OBSERVACIONES"].Value = item.Cells["OBSERVACION"].Value.ToString();
                        item.Appearance.BackColor = Color.FromArgb(247, 189, 86);
                    }
                    else
                    {
                        if(item.Cells["OBSERVACION"].Value.ToString() != "")
                        {
                            item.Cells["DESPACHADO"].Value = false;
                            item.Appearance.BackColor = Color.FromArgb(231, 120, 153);
                        }
                        else
                            item.Cells["DESPACHADO"].Value = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void frmDespachos_Load(object sender, EventArgs e)
        {
            dtpdesde.Value = DateTime.Now.Date;
            dtphasta.Value = DateTime.Now.Date;
            frmTiempodeEspera x = new frmTiempodeEspera();
            VerDespachos();
            x.ShowDialog();
            ActivarTimer();
        }
        public void ActivarTimer()
        {
            if (Busqueda)
            {
                Actualizar.Interval = 30000;
                Actualizar.Tick += Refrescar;
                Actualizar.Enabled = true;
            }
        }

        private void Refrescar(object sender, EventArgs e)
        {
            if (Busqueda)
            {
                lbltimer.Text = DateTime.Now.ToLongTimeString();
                VerDespachos();
            }
        }
        private void UltraGridDespachos_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = UltraGridDespachos.DisplayLayout.Bands[0];

            UltraGridColumn check = e.Layout.Bands[0].Columns.Add("DESPACHADO", "DESPACHADO");
            check.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            check.CellActivation = Activation.AllowEdit;
            check.Header.VisiblePosition = 0;

            //UltraGridColumn observa = e.Layout.Bands[0].Columns.Add("OBSERVACIONES", "OBSERVACIONES");
            //observa.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Default;
            //observa.CellActivation = Activation.AllowEdit;
            //observa.Header.VisiblePosition = 1;

            UltraGridDespachos.DisplayLayout.Bands[0].Columns["despachados"].Hidden = true;
            UltraGridDespachos.DisplayLayout.Bands[0].Columns["hab_Codigo"].Hidden = true;
            UltraGridDespachos.DisplayLayout.Bands[0].Columns["NIV_CODIGO"].Hidden = true;
            UltraGridDespachos.DisplayLayout.Bands[0].Columns["MED_CODIGO"].Hidden = true;
            UltraGridDespachos.DisplayLayout.Bands[0].Columns["ATE_CODIGO"].Hidden = true;
            UltraGridDespachos.DisplayLayout.Bands[0].Columns["OBSERVACION"].Hidden = true;

            UltraGridDespachos.DisplayLayout.Bands[0].Columns["CODIGO PEDIDO"].CellActivation = Activation.ActivateOnly;
            UltraGridDespachos.DisplayLayout.Bands[0].Columns["CANTIDAD"].CellActivation = Activation.NoEdit;
            UltraGridDespachos.DisplayLayout.Bands[0].Columns["FECHA"].CellActivation = Activation.NoEdit;
            UltraGridDespachos.DisplayLayout.Bands[0].Columns["HORA"].CellActivation = Activation.NoEdit;
            UltraGridDespachos.DisplayLayout.Bands[0].Columns["PACIENTE"].CellActivation = Activation.NoEdit;
            UltraGridDespachos.DisplayLayout.Bands[0].Columns["PISO"].CellActivation = Activation.NoEdit;
            UltraGridDespachos.DisplayLayout.Bands[0].Columns["HAB"].CellActivation = Activation.NoEdit;
            UltraGridDespachos.DisplayLayout.Bands[0].Columns["HIST. CLINICA"].CellActivation = Activation.NoEdit;
            UltraGridDespachos.DisplayLayout.Bands[0].Columns["Nº ATENCION"].CellActivation = Activation.NoEdit;
            UltraGridDespachos.DisplayLayout.Bands[0].Columns["PEDIDO POR"].CellActivation = Activation.NoEdit;


            this.UltraGridDespachos.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
        }

        private void btnDespachar_Click(object sender, EventArgs e)
        {
            if (chkDespacho.Checked)
            {
                MessageBox.Show("Los productos ya han sido despachados", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(MessageBox.Show("¿Está seguro de generar el despacho?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                //frmAyudaDespacho x = new frmAyudaDespacho();
                //x.ShowDialog();

                //if(x.despachado == 0)
                //{
                    
                //}


                List<DtoDespachos> despachos = new List<DtoDespachos>();
                foreach (var item in UltraGridDespachos.Rows)
                {
                    if (item.Cells["DESPACHADO"].Text == "True" && item.Cells["DESPACHADO POR"].Value.ToString() == "")
                    {
                        if (!NegPedidos.ValidaDespachado(Convert.ToInt64(item.Cells["CODIGO PEDIDO"].Value.ToString())))
                        {
                            DtoDespachos xdespacho = new DtoDespachos();
                            xdespacho.observacion = "";
                            xdespacho.ped_codigo = Convert.ToInt64(item.Cells["CODIGO PEDIDO"].Value.ToString());
                            despachos.Add(xdespacho);
                        }
                        else
                            MessageBox.Show("El pedido: " + item.Cells["CODIGO PEDIDO"].Value.ToString() + " ya fue despachado.",
                                "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                if (despachos.Count > 0)
                {
                    if (NegPedidos.InsertarDespachos(despachos, 0, DateTime.Now))
                    {
                        MessageBox.Show("Productos despachados correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //frmTiempodeEspera xy = new frmTiempodeEspera();
                        UltraGridDespachos.Visible = false;
                        VerDespachos();
                        //xy.ShowDialog();
                        UltraGridDespachos.Visible = true;
                        Busqueda = true;
                    }
                    else
                        MessageBox.Show("No se cargaron los despachos. Intente nuevamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
        }

        public void ImprimirPedido()
        {
            try
            {
                if (Sesion.codDepartamento == 1 || Sesion.codDepartamento == 14)
                {
                    UltraGridRow fila = this.UltraGridDespachos.ActiveRow;
                    int ped_codigo = Convert.ToInt32(fila.Cells["CODIGO PEDIDO"].Value.ToString());

                    if (ped_codigo > 0)
                    {
                        var codigoArea = 100;

                        HabitacionesUI.frmImpresionPedidos frmPedidos = new HabitacionesUI.frmImpresionPedidos(ped_codigo, codigoArea, 1, 1);
                        HabitacionesUI.frmImpresionPedidos.reimpresion = true;
                        frmPedidos.Show();
                    }
                }
                else if (Sesion.codDepartamento == 9 || Sesion.codDepartamento == 7)//SOLO PARA IMAGEN
                {
                    UltraGridRow fila = this.UltraGridDespachos.ActiveRow;
                    int ped_codigo = Convert.ToInt32(fila.Cells["CODIGO PEDIDO"].Value.ToString());

                    if (ped_codigo > 0)
                    {
                        var codigoArea = 100;

                        HabitacionesUI.frmImpresionPedidos frmPedidos = new HabitacionesUI.frmImpresionPedidos(ped_codigo, codigoArea, 1, 3);
                        HabitacionesUI.frmImpresionPedidos.reimpresion = true;
                        frmPedidos.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Reimpresion valida unicamente para Farmacia.\r\nComuniquese con sistemas",
                        "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No puede selecionar varias filas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirPedido();
        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            frm_AyudaPacientes form = new frm_AyudaPacientes();
            form.campoPadre = txt_historiaclinica;
            form.ShowDialog();
            form.Dispose();
            txt_historiaclinica.Text = txt_historiaclinica.Text.Trim();
        }

        private void chkHC_CheckedChanged(object sender, EventArgs e)
        {
            txt_historiaclinica.Enabled = chkHC.Checked;
            ayudaPacientes.Visible = chkHC.Checked;
            txt_historiaclinica.Text = "0";
            if (chkHC.Checked)
                Busqueda = false;
            else
                Busqueda = true;
        }

        private void txt_historiaclinica_Leave(object sender, EventArgs e)
        {
            if (txt_historiaclinica.Text == "")
                txt_historiaclinica.Text = "0";
        }

        private void chkPiso_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPiso.Checked)
            {
                try
                {
                    cmbPiso.DataSource = NegPedidos.VerPisos();
                    cmbPiso.DisplayMember = "NIV_NOMBRE";
                    cmbPiso.ValueMember = "NIV_CODIGO";
                    cmbPiso.Enabled = true;
                    Busqueda = false;
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
            }
            else
            {
                cmbPiso.Enabled = false;
                Busqueda = true;
            }
                
        }

        private void chkHab_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPiso.Checked)
                {
                    if (chkHab.Checked)
                    {
                        cmbHabitacion.DataSource = NegPedidos.VerHabitaciones(Convert.ToInt32(cmbPiso.SelectedValue.ToString()));
                        cmbHabitacion.DisplayMember = "hab_Numero"; 
                        cmbHabitacion.ValueMember = "hab_Codigo";
                        cmbHabitacion.Enabled = true;
                    }
                    else
                        cmbHabitacion.Enabled = false;
                }
                else
                {
                    if (chkHab.Checked)
                    {
                        cmbHabitacion.DataSource = NegPedidos.VerHabitaciones(0);
                        cmbHabitacion.DisplayMember = "hab_Numero";
                        cmbHabitacion.ValueMember = "hab_Codigo";
                        cmbHabitacion.Enabled = true;
                        Busqueda = false;
                    }
                    else
                    {
                        Busqueda = true;
                        cmbHabitacion.Enabled = false;
                    }
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (UltraGridDespachos.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(UltraGridDespachos, PathExcel);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtped_codigo.Enabled = checkBox1.Checked;
            txtped_codigo.Text = "0";
            if (checkBox1.Checked)
                Busqueda = false;
            else
                Busqueda = true;
        }

        private void chkDespacho_CheckedChanged(object sender, EventArgs e)
        {
            //frmTiempodeEspera xy = new frmTiempodeEspera();
            UltraGridDespachos.Visible = false;
            VerDespachos();
            //xy.ShowDialog();
            UltraGridDespachos.Visible = true;
        }

        private void txtped_codigo_Enter(object sender, EventArgs e)
        {
            txtped_codigo.Text = "";
        }

        private void txtped_codigo_Leave(object sender, EventArgs e)
        {
            if(txtped_codigo.Text.Trim() == "")
                txtped_codigo.Text = "0";
        }

        private void txtped_codigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txtped_codigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //frmTiempodeEspera xy = new frmTiempodeEspera();
                    UltraGridDespachos.Visible = false;
                    VerDespachos();
                    //xy.ShowDialog();
                    UltraGridDespachos.Visible = true;
                }
            }
        }

        private void txt_historiaclinica_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_historiaclinica_KeyDown(object sender, KeyEventArgs e)
        {
            if (chkHC.Checked)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //frmTiempodeEspera xy = new frmTiempodeEspera();
                    UltraGridDespachos.Visible = false;
                    VerDespachos();
                    //xy.ShowDialog();
                    UltraGridDespachos.Visible = true;
                }
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkDespacho.Checked)
                {
                    if (MessageBox.Show("¿Está seguro de desmarcar el despacho?",
    "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        UltraGridRow Fila = UltraGridDespachos.ActiveRow;

                        NegPedidos.DeleteDespacho(Convert.ToInt64(Fila.Cells["CODIGO PEDIDO"].Value.ToString()));
                        MessageBox.Show("Despacho desmarcado con exito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //frmTiempodeEspera xy = new frmTiempodeEspera();
                        UltraGridDespachos.Visible = false;
                        VerDespachos();
                        //xy.ShowDialog();
                        UltraGridDespachos.Visible = true;
                    }
                }
                else
                    MessageBox.Show("No se puede desmarcar pedido sin despachar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo desmarcar despacho.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Busqueda = false;
            if(Sesion.codDepartamento == 14 || Sesion.codDepartamento == 1) //FARMACIA Y SISTEMAS PUEDEN HACER DEVOLUCIONES
            {
                UltraGridRow Fila = UltraGridDespachos.ActiveRow;
                ATENCIONES validaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(Fila.Cells["ATE_CODIGO"].Value.ToString()));
                if(validaAtencion.ESC_CODIGO == 1)
                {
                    if (MessageBox.Show("¿Está seguro de realizar la devolución del Nº: " + Fila.Cells["CODIGO PEDIDO"].Value.ToString(),
                    "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        His.Formulario.frmDevolucion x = new Formulario.frmDevolucion();
                        x.ped_codigo = Fila.Cells["CODIGO PEDIDO"].Value.ToString();
                        x.ate_codigo = Fila.Cells["ATE_CODIGO"].Value.ToString();
                        MEDICOS DtoMedico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(Fila.Cells["MED_CODIGO"].Value.ToString()));
                        x.medico = DtoMedico.MED_APELLIDO_PATERNO + " " + DtoMedico.MED_APELLIDO_MATERNO + " " + DtoMedico.MED_NOMBRE1 + " " + DtoMedico.MED_NOMBRE2;
                        x.ShowDialog();
                        Busqueda = true;
                    }
                }
                else
                {
                    MessageBox.Show("Paciente ha sido dado de alta. Comuniquese con caja", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    Busqueda = true;
                    return;
                }
            }
            else
            {
                MessageBox.Show("Solo personal de farmacia puede realzar devoluciones.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Busqueda = true;
                return;
            }
        }

        private void UltraGridDespachos_AfterCellUpdate(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Key == "DESPACHADO")
            {
                if (e.Cell.Value.ToString() == "True")
                {
                    Busqueda = false;
                }
            }
        }

        private void frmDespachos_FormClosed(object sender, FormClosedEventArgs e)
        {
            Busqueda = false;
        }
    }
}
