using His.Entidades;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Dietetica
{
    public partial class frm_HorarioLaboral : Form
    {

        DataTable dtMed;
        DataTable dtReg;

        public frm_HorarioLaboral()
        {
            InitializeComponent();
            dtMed = null;
            //dtMed.Columns.Add("MED_CODIGO", string);
            //dtMed.Columns.Add("MEDICO");
            //dtMed.Columns.Add("ESPECIALIDAD");
            ResetControls();
        }

        private void ResetControls()
        {
            
            dtReg = NegDietetica.getDataTable("GetMedicos");
            dtMed = NegDietetica.getDataTable("GetMedicos");
            dtMed.Clear();

            gridMedicos.DataSource = dtMed;
            gridRegistro.DataSource = dtReg;

            while (chkDaysWeek.CheckedIndices.Count > 0)
                chkDaysWeek.SetItemChecked(chkDaysWeek.CheckedIndices[0], false);
        }


        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = gridRegistro.DisplayLayout.Bands[0];

            gridRegistro.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            gridRegistro.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            gridRegistro.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            gridRegistro.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            gridRegistro.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            gridRegistro.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
            gridRegistro.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            gridRegistro.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            gridRegistro.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            gridRegistro.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            gridRegistro.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            gridRegistro.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            gridRegistro.DisplayLayout.UseFixedHeaders = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //gridRegistro.Rows.Band.RowSelectorWidthResolved;
            if (this.gridRegistro.Selected.Rows.Count > 0)
            {
                //this.gridRegistro.Text = "Band, Row \n";
                foreach (UltraGridRow rowSelected in this.gridRegistro.Selected.Rows)
                {
                    dtMed.Rows.Add( gridRegistro.Rows[rowSelected.Index].Cells["MED_CODIGO"].Value.ToString(),
                                    gridRegistro.Rows[rowSelected.Index].Cells["MEDICO"].Value.ToString(),
                                    gridRegistro.Rows[rowSelected.Index].Cells["ESPECIALIDAD"].Value.ToString());   
                }

                foreach (DataRow row in dtMed.Rows)
                {
                    string _MED_CODIGO = row["MED_CODIGO"].ToString();

                    DataRow[] rows;
                    rows = dtReg.Select("MED_CODIGO = '" + _MED_CODIGO + "'");

                    foreach (DataRow rowx in rows)
                        dtReg.Rows.Remove(rowx);

                    dtReg.AcceptChanges();
                }
            }
       

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {




            try
            {
                List<string> diasNulos = new List<string>();
                diasNulos.Add("X");
                foreach (int indexChecked in chkDaysWeek.CheckedIndices)
                {
                    diasNulos.Add(indexChecked.ToString());
                }


                DateTime _desde = Convert.ToDateTime(dtpDesde.Value.ToShortDateString());
                DateTime _hasta = Convert.ToDateTime(dtpHasta.Value.ToShortDateString());

                if (_hasta >= _desde)
                {

                    for (int i = 0; i <= (_hasta - _desde).Days; i++)
                    {
                        DateTime _fec = _desde.AddDays(i);
                        string _daux;
                        /////doy valor Index al DayOfWeek
                        if (_fec.DayOfWeek == DayOfWeek.Monday)
                            _daux = "0";
                        else if (_fec.DayOfWeek == DayOfWeek.Tuesday)
                            _daux = "1";
                        else if (_fec.DayOfWeek == DayOfWeek.Wednesday)
                            _daux = "2";
                        else if (_fec.DayOfWeek == DayOfWeek.Thursday)
                            _daux = "3";
                        else if (_fec.DayOfWeek == DayOfWeek.Friday)
                            _daux = "4";
                        else if (_fec.DayOfWeek == DayOfWeek.Saturday)
                            _daux = "5";
                        else
                            _daux = "6";
                        /////

                        // Console.WriteLine(diasNulos.Contains(_daux) + "    "  + _fec.ToLongDateString());
                        if (!diasNulos.Contains(_daux))//si lo encuentra
                        {
                            foreach (DataRow row in dtMed.Rows)
                            {

                                string[] _time = txtEntrada.Text.Split(':');
                                string[] _time2 = txtSalida.Text.Split(':');
                                string _inicio = _fec.ToString("yyyy'-'MM'-'dd'T'" + _time[0] + "':'" + _time[1] + "':'00");
                                string _fin = _fec.ToString("yyyy'-'MM'-'dd'T'" + _time2[0] + "':'" + _time2[1] + "':'00");

                                if (Convert.ToDateTime(_inicio) > Convert.ToDateTime(_fin))
                                    _fin = _fec.AddDays(1).ToString("yyyy'-'MM'-'dd'T'" + _time2[0] + "':'" + _time2[1] + "':'00");

                                Console.WriteLine(
                                    row["MED_CODIGO"].ToString() + row["MEDICO"].ToString()
                                    + " desde: " + _inicio
                                    + " hasta: " + _fin
                                    );
                                string[] aux = new string[] { row["MED_CODIGO"].ToString(), _inicio, _fin };

                                NegDietetica.setROW("InsertHorarioMedico", aux);
                            }
                        }

                    }
                }

                MessageBox.Show("Se proceso exitosamente.");
                ResetControls();

            }
            catch (Exception ex)
            {

                MessageBox.Show("No fue posible guardar. /n Error:" + ex);
            }


           

        }


        

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

   

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //gridRegistro.Rows.Band.RowSelectorWidthResolved;
            if (this.gridMedicos.Selected.Rows.Count > 0)
            {
                //this.gridRegistro.Text = "Band, Row \n";
                foreach (UltraGridRow rowSelected in this.gridMedicos.Selected.Rows)
                {
                    dtReg.Rows.Add(gridMedicos.Rows[rowSelected.Index].Cells["MED_CODIGO"].Value.ToString(),
                                    gridMedicos.Rows[rowSelected.Index].Cells["MEDICO"].Value.ToString(),
                                    gridMedicos.Rows[rowSelected.Index].Cells["ESPECIALIDAD"].Value.ToString());
                }

                foreach (DataRow row in dtReg.Rows)
                {
                    string _MED_CODIGO = row["MED_CODIGO"].ToString();

                    DataRow[] rows;
                    rows = dtMed.Select("MED_CODIGO = '" + _MED_CODIGO + "'");

                    foreach (DataRow rowx in rows)
                        dtMed.Rows.Remove(rowx);

                    dtMed.AcceptChanges();
                }
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            tabulador.SelectedTab = tabulador.Tabs["horario"];
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {




            try
            {
                List<string> diasNulos = new List<string>();
                diasNulos.Add("X");
                foreach (int indexChecked in chkDaysWeek.CheckedIndices)
                {
                    diasNulos.Add(indexChecked.ToString());
                }


                DateTime _desde = Convert.ToDateTime(dtpDesde.Value.ToShortDateString());
                DateTime _hasta = Convert.ToDateTime(dtpHasta.Value.ToShortDateString());

                if (_hasta >= _desde)
                {

                    for (int i = 0; i <= (_hasta - _desde).Days; i++)
                    {
                        DateTime _fec = _desde.AddDays(i);
                        string _daux;
                        /////doy valor Index al DayOfWeek
                        if (_fec.DayOfWeek == DayOfWeek.Monday)
                            _daux = "0";
                        else if (_fec.DayOfWeek == DayOfWeek.Tuesday)
                            _daux = "1";
                        else if (_fec.DayOfWeek == DayOfWeek.Wednesday)
                            _daux = "2";
                        else if (_fec.DayOfWeek == DayOfWeek.Thursday)
                            _daux = "3";
                        else if (_fec.DayOfWeek == DayOfWeek.Friday)
                            _daux = "4";
                        else if (_fec.DayOfWeek == DayOfWeek.Saturday)
                            _daux = "5";
                        else
                            _daux = "6";
                        /////

                        // Console.WriteLine(diasNulos.Contains(_daux) + "    "  + _fec.ToLongDateString());
                        if (!diasNulos.Contains(_daux))//si lo encuentra
                        {
                            foreach (DataRow row in dtMed.Rows)
                            {

                                string[] _time = txtEntrada.Text.Split(':');
                                string[] _time2 = txtSalida.Text.Split(':');
                                string _inicio = _fec.ToString("yyyy'-'MM'-'dd'T'" + _time[0] + "':'" + _time[1] + "':'00");
                                string _fin = _fec.ToString("yyyy'-'MM'-'dd'T'" + _time2[0] + "':'" + _time2[1] + "':'00");

                                if (Convert.ToDateTime(_inicio) > Convert.ToDateTime(_fin))
                                    _fin = _fec.AddDays(1).ToString("yyyy'-'MM'-'dd'T'" + _time2[0] + "':'" + _time2[1] + "':'00");

                                Console.WriteLine(
                                    row["MED_CODIGO"].ToString() + row["MEDICO"].ToString()
                                    + " desde: " + _inicio
                                    + " hasta: " + _fin
                                    );
                                string[] aux = new string[] { row["MED_CODIGO"].ToString(), _inicio, _fin };

                                NegDietetica.setROW("InsertHorarioMedico", aux);
                            }
                        }

                    }
                }

                MessageBox.Show("Se proceso exitosamente.");
                ResetControls();
                tabulador.SelectedTab = tabulador.Tabs["medicos"];

            }
            catch (Exception ex)
            {

                MessageBox.Show("No fue posible guardar. /n Error:" + ex);
            }


        }
    }
}
