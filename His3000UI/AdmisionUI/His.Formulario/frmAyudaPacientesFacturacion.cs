using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Negocio;
using His.Parametros;

namespace His.ConsultaExterna
{
    public partial class frmAyudaPacientesFacturacion : Form
    {
        #region Variables
        
        List<DtoPedidos> consultaPacientes = new List<DtoPedidos>();
        public TextBox campoCodigo = new TextBox();
        public TextBox campoAseguradora = new TextBox();
        public TextBox campoPadre = new TextBox();
        public TextBox campoAtencion = new TextBox();
        public TextBox campoNombre = new TextBox();
        public TextBox campoAtencionNumero = new TextBox();        
        public string columnabuscada = "HCL";
        public string columnaNombre = "NOMBRE";
        public string columnaAtencion = "ATENCION";
        public string columnaAseguradora = "ASEGURADORA";
        public string columnaCodigo = "CODIGO";
        public string columnaFecha = "F_INGRESO";
        public string columnaAtencionNumero = "ATE_NUMERO";
        public bool inicio = true;
        public int agrupacion = 0;
        public int primeraCuenta = 0;
        

        #endregion

        #region Constructor
        public frmAyudaPacientesFacturacion()
        {
            InitializeComponent();
            cb_numFilas.SelectedIndex = 0;            
            Buscar();
            cb_numFilas.Text = "1000";
        }

        private void Buscar()
        {
            try
            {
                string id = txt_busqCi.Text.ToString();
                string historia = txt_busqHist.Text.ToString();
                string nombre = txt_busqNom.Text.ToString();
                
                int cantidad = 100;

                if (cb_numFilas.SelectedItem != null)
                {                    
                    cantidad = Convert.ToInt32(cb_numFilas.SelectedItem);
                }
                consultaPacientes = NegPedidos.recuperarListaPacientesFacturacion(id, historia, nombre, cantidad, 1, "ConsultaExterna");

                grid.DataSource = consultaPacientes.Select(
                    p => new
                    {
                        F_ATENCION = p.PED_FECHA,                        
                        HCL = p.HISTORIA_CLINICA,
                        ATENCION = p.ATE_CODIGO,
                        HABITACION = p.HABITACION,
                        NOMBRE = p.PACIENTE,
                        ID = p.IDENTIFICACION,
                        ASEGURADORA = p.PRO_DESCRIPCION,
                        ATE_NUMERO = p.ATE_NUMERO,                
                        F_ALTA = p.PED_FECHA_ALTA,
                        CODIGO = p.CODIGO,
                        ESTADO = p.ESC_CODIGO,
                        TIPOATENCION=p.TipoAtencion
                    }

                    ).Where(p => p.TIPOATENCION == "9" && p.ESTADO == 1).OrderByDescending(p => p.ATENCION).Distinct().ToList();

                grid.DisplayLayout.Bands[0].Columns["F_ATENCION"].Width = 200;
                grid.DisplayLayout.Bands[0].Columns["HCL"].Width = 150;
                grid.DisplayLayout.Bands[0].Columns["ATENCION"].Width = 100;
                grid.DisplayLayout.Bands[0].Columns["HABITACION"].Width = 150;
                grid.DisplayLayout.Bands[0].Columns["NOMBRE"].Width = 700;
                grid.DisplayLayout.Bands[0].Columns["ID"].Width = 200;
                grid.DisplayLayout.Bands[0].Columns["ASEGURADORA"].Width = 450;
                grid.DisplayLayout.Bands[0].Columns["ATE_NUMERO"].Width = 150;
                grid.DisplayLayout.Bands[0].Columns["F_ALTA"].Width = 180;
                grid.DisplayLayout.Bands[0].Columns["CODIGO"].Hidden = true;
                grid.DisplayLayout.Bands[0].Columns["ESTADO"].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }

        #endregion

        #region Metodos Privados

        private void cb_numFilas_SelectedValueChanged(object sender, EventArgs e)
        {
            if (inicio == false)
                Buscar();
        }

        private void txt_busqHist_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                Buscar();
            }
        }

        private void txt_busqNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                Buscar();
            }
        }

        private void txt_busqCi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                Buscar();
            }
        }
        private void enviarCodigo()
        {
            try
            {
                enviaCodigo();
                this.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }   
        private void ultraGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    enviarCodigo();
                }
                else if (e.KeyCode == Keys.End)
                {
                    grid.ActiveCell = grid.Rows[grid.Rows.Count - 1].Cells[grid.ActiveCell.Column.Index];
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Home)
                {
                    grid.ActiveCell = grid.Rows[0].Cells[grid.ActiveCell.Column.Index];
                    e.Handled = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGrid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            enviarCodigo();           
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            grid.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
            grid.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;
        }

        #endregion

        private void frmAyudaPacientesFacturacion_Load(object sender, EventArgs e)
        {

        }

        private void grid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            Int64 CodigoEstado = 0;
            CodigoEstado = Convert.ToInt64(e.Row.Cells["ESTADO"].Value);

            if (CodigoEstado == 6)
            {
                e.Row.Appearance.BackColor = Color.LightCoral;
            }
            else if(CodigoEstado==2)
            {
                e.Row.Appearance.BackColor = Color.LightBlue;
            }
            else if (CodigoEstado == 1)
            {
                e.Row.Appearance.BackColor = Color.LightGoldenrodYellow;
            }
            else if (CodigoEstado == 7)
            {
                e.Row.Appearance.BackColor = Color.LightSalmon;
            }

        }

        

        //private void btnAgrupar_Click(object sender, EventArgs e)
        //{
        //    btnActualizar.Visible = false;
                    
        //    Buscar();
            
        //    if (MessageBox.Show("Usted Va Realizar Agrupación De Cuentas. ESTE PROCESO NO SE PUEDE REVERTIR\r\n ¿Desea Continuar?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        //    {
        //        agrupacion = 1;
        //        primeraCuenta = 1;
        //        MessageBox.Show("CON DOBLE CLIC.\r\nPRIMERO ESCOJA CUENTA PRINCIPAL.\r\nSEGUIDO TODAS LAS CUENTAS QUE VA AGRUPAR", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}        

        //private void btnTermina_Click(object sender, EventArgs e)
        //{
        //    if (MessageBox.Show("Esta Cerrando Proceso De Agrupación No Va Poder Añadir Más Cuentas\r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        //        this.Close();
        //}

        private void enviaCodigo()
        {
            campoPadre.Tag = true;
            campoAtencion.Text = grid.ActiveRow.Cells[columnaAtencion].Value.ToString();
            campoAtencionNumero.Text = grid.ActiveRow.Cells[columnaAtencionNumero].Value.ToString();
            campoNombre.Text = grid.ActiveRow.Cells[columnaNombre].Value.ToString();
            campoPadre.Text = grid.ActiveRow.Cells[columnabuscada].Value.ToString();
            campoCodigo.Text = grid.ActiveRow.Cells[columnaCodigo].Value.ToString();
            campoAseguradora.Text = grid.ActiveRow.Cells[columnaAseguradora].Value.ToString();
        }
    }
}
