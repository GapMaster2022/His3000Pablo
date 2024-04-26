using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using Infragistics.Win.UltraWinGrid;

namespace His.Formulario
{
    public partial class frm_AyudaMedicos : Form
    {
        #region Variables
        public TextBox campoPadre = null;
        public TextBox campoPadre2 = null;
        public MaskedTextBox campoEspecial = null;
        public string tabla;
        public int columnabuscada;
        public string colRetorno;
        public string colRetorno2;
        //cadena de caracteres que acumula las teclas pulsadas en un segundo
        public string teclas = "";
        #endregion

        #region Constructor
        public frm_AyudaMedicos()
        {
            InitializeComponent();
        }
        public frm_AyudaMedicos(List<PACIENTES> pacientes, string tablaExt, string columnBusq)
        {
            InitializeComponent();
            tabla = tablaExt;
            grid.DataSource = pacientes.Select(p => new { HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA, NOMBRE = p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2, ID = p.PAC_IDENTIFICACION }).ToList();
            //grid.Columns["HISTORIA_CLINICA"].Width = 120;
            //grid.Columns["NOMBRE"].Width = 350;
            //grid.Columns["ID"].Width = 160;
            colRetorno = columnBusq;

        }

        public frm_AyudaMedicos(List<DIVISION_POLITICA> divpolitica, string tablaExt, string columnBusq)
        {
            //consultaDivision = divpolitica;
            InitializeComponent();
            tabla = tablaExt;
            colRetorno = columnBusq;
            grid.DataSource = divpolitica.Select(d => new { CODIGO = d.DIPO_CODIINEC, NOMBRE = d.DIPO_NOMBRE }).ToList();
        }
        public frm_AyudaMedicos(List<ATENCIONES> atenciones, string tablaExt, string columnBusq)
        {
            //consultaAtenciones = atenciones;
            InitializeComponent();
            tabla = tablaExt;
            colRetorno = columnBusq;
            grid.DataSource = atenciones;
        }
        public frm_AyudaMedicos(List<ASEGURADORAS_EMPRESAS> empresas, string tablaExt, string columnBusq)
        {
            //consultaEmpresas = empresas;
            InitializeComponent();
            tabla = tablaExt;
            colRetorno = columnBusq;
            grid.DataSource = empresas.Select(e => new { NOMBRE = e.ASE_NOMBRE, RUC = e.ASE_RUC }).ToList();
        }
        public frm_AyudaMedicos(List<PAIS> nacionalidades, string tablaExt, string columnBusq)
        {
            InitializeComponent();
            tabla = tablaExt;
            colRetorno = columnBusq;
            grid.DataSource = nacionalidades.Select(n => new { NACIONALIDAD = n.NACIONALIDAD }).OrderBy(p => p.NACIONALIDAD).ToList();
            //grid.Columns["NACIONALIDAD"].Width = 300;
        }
        public frm_AyudaMedicos(List<MEDICOS> medicos, string tablaExt, string columnBusq)
        {
            try
            {
                InitializeComponent();
                tabla = tablaExt;
                colRetorno = columnBusq;
                grid.DataSource = medicos.Select(n => new
                {
                    CODIGO = n.MED_CODIGO,
                    ESPECIALIDAD = n.ESPECIALIDADES_MEDICAS.ESP_NOMBRE,
                    NOMBRE = n.MED_APELLIDO_PATERNO + " " + n.MED_APELLIDO_MATERNO + " " + n.MED_NOMBRE1 + " " + n.MED_NOMBRE2,
                    
                    RUC = n.MED_RUC
                }).OrderBy(n=>n.ESPECIALIDAD).ToList();
                campoPadre = new TextBox();
                //grid.Columns["NOMBRE"].Width = 300;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }

        #endregion

        #region Eventos

        private void frm_Ayudas_Load(object sender, EventArgs e)
        {
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string columna;
            //    string value = "";
            //    if (InputBox("Ayuda", grid.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
            //    {
            //        columna = value.ToUpper();
            //        for (int i = 0; i <= grid.RowCount - 1; i++)
            //        {
            //            if (grid.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
            //                grid.CurrentCell = grid.Rows[i].Cells[columnabuscada];
            //            // gridMedicos.Rows[i].Cells[columnabuscada].Selected = true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.InnerException.Message);
            //}
        }

        #endregion

        #region Metodos Privados
        private void enviarCodigo()
        {
            if (grid.ActiveRow.Index > -1)
            {
                if (campoPadre != null)
                    campoPadre.Text = grid.ActiveRow.Cells[colRetorno].Value.ToString();

                if (campoEspecial != null)
                    campoEspecial.Text = grid.ActiveRow.Cells[colRetorno].Value.ToString();

                if (campoPadre2 != null && colRetorno2 != null)
                    campoPadre2.Text = grid.ActiveRow.Cells[colRetorno2].Value.ToString();
                this.Close();
            }
        }

        #endregion

        //realizo la busqueda en las celdas de cada columna

        private void timerCapturaTeclas_Tick(object sender, EventArgs e)
        {
            //desactivo el control, al terminarse el intervalo
            timerCapturaTeclas.Enabled = false;
        }

        private void grid2_KeyDown(object sender, KeyEventArgs e)
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

        private void grid2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    //solo se aceptan digitos y letras
            //    if (Char.IsLetterOrDigit(e.KeyChar))
            //    {
            //        //capturo las teclas presionadas mientras el control timer este Activo (500 milisegundos)
            //        if (timerCapturaTeclas.Enabled == true)
            //        {
            //            teclas += e.KeyChar.ToString();
            //        }
            //        //si el control esta inactivo, paso el control a activo y vuelvo a iniciar la captura
            //        else
            //        {
            //            teclas = e.KeyChar.ToString();
            //            timerCapturaTeclas.Enabled = true;
            //        }
            //        //celda seleccionada
            //        DataGridViewCell celda = grid.ActiveCell;
            //        int ini;
            //        //valido si esta en la ultima celda
            //        if (grid.CurrentRow.Index == (grid.Rows.Count - 1))
            //        {
            //            ini = 0;    //si esta en la ultima celda, inicia la busqueda desde la primera celda 
            //        }
            //        else
            //        {
            //            ini = grid.CurrentRow.Index + 1;    //inicia la busqueda desde la siguiente celda
            //        }
            //        //verifico si existen coincidencias desde la celda actual hasta la final
            //        for (int i = ini; i < grid.Rows.Count; i++)
            //        {
            //            if (grid.Rows[i].Cells[celda.ColumnIndex].Value.ToString().ToUpper().StartsWith(teclas.ToUpper()))
            //            {
            //                grid.CurrentCell = grid.Rows[i].Cells[celda.ColumnIndex];
            //                return;
            //            }
            //        }
            //        //verifico si existen coincidencias desde la primera celda hasta la actual
            //        for (int i = 0; i < ini; i++)
            //        {
            //            if (grid.Rows[i].Cells[celda.ColumnIndex].Value.ToString().ToUpper().StartsWith(teclas.ToUpper()))
            //            {
            //                grid.CurrentCell = grid.Rows[i].Cells[celda.ColumnIndex];
            //                return;
            //            }
            //        }
            //    }
            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void grid2_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            enviarCodigo();
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = grid.DisplayLayout.Bands[0];

            grid.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            grid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            grid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            grid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            grid.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
            grid.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

            bandUno.Columns["NOMBRE"].Width = 300;
            bandUno.Columns["CODIGO"].Width = 100;
            bandUno.Columns["ESPECIALIDAD"].Width = 130;
            bandUno.Columns["RUC"].Width = 120;

            grid.DisplayLayout.UseFixedHeaders = true;
        }

        private void frm_AyudaMedicos_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape) {
                this.Close();
            }
      
        }
    }
}
