using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;

namespace His.Dietetica
{
    public partial class frmQuirofanoAyudaRegistro : Form
    {
        NegQuirofano Quirofano = new NegQuirofano();
        internal static bool medico; //Variable que mostrara la tabla de medicos si este fuese verdadero.
        internal static bool ayudante; //Variable que mostrara la tabla de medicos(ayudante) si este fuese verdadero.
        internal static bool ayudantia; //Variable que mostrara la tabla de medicos(ayudantias) si este fuese verdaero.
        internal static bool anestesiologo; //Variable que mostrara la tabla con el anestesiologo
        internal static bool anestesiologo2; //Variable que mostrara la tabla con el anestesiologo
        internal static bool circulante; //Variable que mostrara la tabla con el circulante
        internal static bool instrumentista; //Variable que mostrara la tabla con el instrumentista
        internal static bool patologo; //Variable que mostrara la tabla de patologos 
        internal static bool gastro; //Variable que mostrara la tabla de patologos 
        public frmQuirofanoAyudaRegistro()
        {
            InitializeComponent();
        }
        public void Redimensionar()
        {
            //Redimiensionamos la tabla
            UltraGridBand bandUno = TablaRegistro.DisplayLayout.Bands[0];

            TablaRegistro.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            TablaRegistro.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            TablaRegistro.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            TablaRegistro.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            TablaRegistro.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            TablaRegistro.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            TablaRegistro.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;


            //Caracteristicas de Filtro en la grilla
            TablaRegistro.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            TablaRegistro.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            TablaRegistro.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            TablaRegistro.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            TablaRegistro.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            TablaRegistro.DisplayLayout.UseFixedHeaders = true;

            //Dimension los registros
            TablaRegistro.DisplayLayout.Bands[0].Columns[1].Width = 250;
            //Ocultamos el codigo del medico
            TablaRegistro.DisplayLayout.Bands[0].Columns[0].Hidden = true;
        }
        private void frmQuirofanoAyudaRegistro_Load(object sender, EventArgs e)
        {
            if(medico == true || ayudante == true || ayudantia == true || patologo == true)
            {
                TablaRegistro.DataSource = Quirofano.MostrarMedicos();
                Redimensionar();
            }
            if (gastro == true)
            {
                TablaRegistro.DataSource = Quirofano.MostrarGastro();
                Redimensionar();
            }
            if (patologo == true)
            {
                TablaRegistro.DataSource = Quirofano.MostrarPatologo();
                Redimensionar();
            }
            if (anestesiologo == true)
            {
                TablaRegistro.DataSource = Quirofano.MostrarAnestesiologo();
                Redimensionar();
            }
            if (anestesiologo2 == true)
            {
                TablaRegistro.DataSource = Quirofano.MostrarAnestesiologo();
                Redimensionar();
            }
            if(circulante == true || instrumentista == true)
            {
                TablaRegistro.DataSource = Quirofano.MostrarUsuario();
                Redimensionar();
            }
        }

        private void TablaRegistro_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if(TablaRegistro.Selected.Rows.Count > 0)
                {
                    UltraGridRow Fila = TablaRegistro.ActiveRow;
                    if(medico == true)
                    {
                        //Se Envian valores al siguiente formulario
                        frmQuirofanoRegistro.cod_cirujano = Fila.Cells[0].Value.ToString();
                        frmQuirofanoRegistro.nom_cirujano = Fila.Cells[1].Value.ToString();
                        this.Close();
                    }
                    if (gastro == true)
                    {
                        //Se Envian valores al siguiente formulario
                        frmQuirofanoRegistro.cod_cirujano = Fila.Cells[0].Value.ToString();
                        frmQuirofanoRegistro.nom_cirujano = Fila.Cells[1].Value.ToString();
                        this.Close();
                    }
                    if (ayudante == true)
                    {
                        //se envian valores al siguiente formulario
                        frmQuirofanoRegistro.cod_ayudante = Fila.Cells[0].Value.ToString();
                        frmQuirofanoRegistro.nom_ayudante = Fila.Cells[1].Value.ToString();
                        this.Close();
                    }
                    if(ayudantia == true)
                    {
                        frmQuirofanoRegistro.cod_ayudantia = Fila.Cells[0].Value.ToString();
                        frmQuirofanoRegistro.nom_ayudantia = Fila.Cells[1].Value.ToString();
                        this.Close();
                    }
                    if(anestesiologo == true)
                    {
                        frmQuirofanoRegistro.cod_anestesiologo = Fila.Cells[0].Value.ToString();
                        frmQuirofanoRegistro.nom_anestesiologo = Fila.Cells[1].Value.ToString();
                        this.Close();
                    }
                    if(anestesiologo2 == true)
                    {
                        frmQuirofanoRegistro.cod_anestesiologo2 = Fila.Cells[0].Value.ToString();
                        frmQuirofanoRegistro.nom_anestesiologo2 = Fila.Cells[1].Value.ToString();
                        this.Close();
                    }
                    if(circulante == true)
                    {
                        frmQuirofanoRegistro.cod_circulante = Fila.Cells[0].Value.ToString();
                        frmQuirofanoRegistro.nom_circulante = Fila.Cells[1].Value.ToString();
                        this.Close();
                    }
                    if(instrumentista == true)
                    {
                        frmQuirofanoRegistro.cod_instrumentista = Fila.Cells[0].Value.ToString();
                        frmQuirofanoRegistro.nom_instrumentista = Fila.Cells[1].Value.ToString();
                        this.Close();
                    }
                    if(patologo == true)
                    {
                        frmQuirofanoRegistro.cod_patologo = Fila.Cells[0].Value.ToString();
                        frmQuirofanoRegistro.nom_patologo = Fila.Cells[1].Value.ToString();
                        this.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
