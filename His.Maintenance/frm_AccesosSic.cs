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

namespace His.Maintenance
{
    public partial class frm_AccesosSic : Form
    {
        Int64 id_modulo;
        Int64 id_usu;
        public frm_AccesosSic()
        {
            InitializeComponent();
            CargaUsuarios();
        }
        public void CargaUsuarios()
        {
            ultraGridUsuario.DataSource = NegUsuarios.UsuariosSic();
        }

        private void ultraGridUsuario_DoubleClickCell(object sender, Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs e)
        {
            foreach (UltraGridRow item in ultraGridUsuario.Rows)
            {
                if (item.Cells["ID"].Value.ToString() == e.Cell.Row.Cells["ID"].Value.ToString())
                {
                    try
                    {
                        ultraGridAccesos.DataSource = null;
                        ultraGridModulo.DataSource = NegUsuarios.ModuloSic();
                        id_usu = Convert.ToInt32(e.Cell.Row.Cells["ID"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        //throw;
                    }
                }
            }
        }

        private void ultraGridModulo_DoubleClickCell(object sender, Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs e)
        {
            foreach (UltraGridRow item in ultraGridModulo.Rows)
            {
                if (item.Cells["ID"].Value.ToString() == e.Cell.Row.Cells["ID"].Value.ToString())
                {
                    try
                    {
                        ultraGridAccesos.DataSource = null;
                        ultraGridAccesos.DataSource = NegUsuarios.BuscaPerfilesSic(Convert.ToInt64(e.Cell.Row.Cells["ID"].Value.ToString()), id_usu);
                        id_modulo = Convert.ToInt32(e.Cell.Row.Cells["ID"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        //throw;
                    }
                }
            }
        }

        private void ultraGridAccesos_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                bool estado;
                if (bool.TryParse(e.Cell.Value.ToString(), out estado))
                {
                    foreach (var item in ultraGridAccesos.Rows)
                    {
                        if (item.Cells["ID"].Value.ToString() == e.Cell.Row.Cells["ID"].Value.ToString())
                        {
                            if (!Convert.ToBoolean(item.Cells["TIENE_ACCESO"].Value.ToString()))
                            {

                                if (!NegUsuarios.CrearPerfilSic(id_usu,Convert.ToInt64(e.Cell.Row.Cells["ID"].Value.ToString()),"S"," "))
                                {
                                    MessageBox.Show("No se ha podido crear el acceso ", "Sic-3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                ultraGridAccesos.DataSource = NegUsuarios.BuscaPerfilesSic(id_modulo, id_usu);
                            }
                            else
                            {
                                if (!NegUsuarios.CrearPerfilSic(id_usu, Convert.ToInt64(e.Cell.Row.Cells["ID"].Value.ToString()), "N", " "))
                                {
                                    MessageBox.Show("No se ha podido crear el acceso ", "Sic-3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                ultraGridAccesos.DataSource = NegUsuarios.BuscaPerfilesSic(id_modulo, id_usu);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                //throw;
            }
        }

        private void ultraGridUsuario_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ultraGridUsuario.DisplayLayout.Bands[0];

            //ultraGridProcedimiento.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            ultraGridUsuario.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            ultraGridUsuario.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            ultraGridUsuario.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            ultraGridUsuario.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            ultraGridUsuario.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            ultraGridUsuario.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            ultraGridUsuario.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGridUsuario.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGridUsuario.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGridUsuario.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            ultraGridUsuario.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            ultraGridUsuario.DisplayLayout.UseFixedHeaders = true;

            //Dimension los registros
            ultraGridUsuario.DisplayLayout.Bands[0].Columns[0].Width = 60;
            ultraGridUsuario.DisplayLayout.Bands[0].Columns[1].Width = 300;
        }

        private void ultraGridModulo_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            e.Layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }

        private void ultraGridAccesos_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            e.Layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ultraGridAccesos.DataSource = null;
            ultraGridModulo.DataSource = null;
        }
    }
}
