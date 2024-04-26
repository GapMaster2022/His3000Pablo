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

namespace His.Admision
{
    public partial class frm_AyudaPacientesSubsecuentes : Form
    {
        public Int64 ateCodigo = 0;
        public frm_AyudaPacientesSubsecuentes(Int64 PAC_CODIGO, bool reIngreso = false)
        {

            InitializeComponent();
            if (!reIngreso)
            {
                DataTable obj = new DataTable();
                obj = NegAtenciones.RecuperaAtencionesSubsecuentes(PAC_CODIGO);

                grid_Consultas.DataSource = obj;
                grid_Consultas.DisplayLayout.Bands[0].Columns["MEDICO"].Width = 350;
                grid_Consultas.DisplayLayout.Bands[0].Columns["TIPO DE INGRESO"].Width = 200;
            }
            else
            {
                DataTable obj = new DataTable();
                obj = NegAtenciones.atencionesReingreso(PAC_CODIGO);

                grid_Consultas.DataSource = obj;
                grid_Consultas.DisplayLayout.Bands[0].Columns["MEDICO"].Width = 300;
                grid_Consultas.DisplayLayout.Bands[0].Columns["TIPO DE INGRESO"].Width = 200;

                UltraGridColumn buttonColumn = grid_Consultas.DisplayLayout.Bands[0].Columns.Add("BotonColumna", "Frm 008");
                buttonColumn.ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
                buttonColumn.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;

                //grid_Consultas.InitializeTemplateAddRow += (sender, e) =>
                //{
                //    e.TemplateAddRow.Cells["BotonColumna"].Value = "Evolucion";
                //    e.TemplateAddRow.Cells["BotonColumna"].ButtonAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                //    e.TemplateAddRow.Cells["BotonColumna"].ButtonAppearance.TextVAlign = Infragistics.Win.VAlign.Middle;
                //    e.TemplateAddRow.Cells["BotonColumna"].ButtonAppearance.ForeColor = System.Drawing.Color.Black;
                //};
            }
        }

        private void grid_Consultas_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            ateCodigo = Convert.ToInt64(grid_Consultas.ActiveRow.Cells["ID"].Value.ToString());
            this.Close();
        }

        private void grid_Consultas_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (e.Cell.Column.Key == "BotonColumna")
            {
                // Lógica a ejecutar cuando se hace clic en el botón
                // Puedes usar e.Cell.Row para obtener la fila correspondiente
                // e.Cell.Row.Cells["OtraColumna"].Value para obtener valores de otras columnas, etc.
                MessageBox.Show("Botón clickeado en fila: " + e.Cell.Row.Index.ToString());
            }
        }

        private void grid_Consultas_ClickCellButton(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Key == "BotonColumna")
            {
                // Lógica a ejecutar cuando se hace clic en el botón
                // Puedes usar e.Cell.Row para obtener la fila correspondiente
                // e.Cell.Row.Cells["OtraColumna"].Value para obtener valores de otras columnas, etc.
                //MessageBox.Show("Botón clickeado en fila: " + e.Cell.Row.Cells["ID"].Value.ToString());
                His.Formulario.frm_Emergencia evolucion = new His.Formulario.frm_Emergencia(Convert.ToInt32(e.Cell.Row.Cells["ID"].Value.ToString()));
                evolucion.Show();
            }
        }
    }
}
