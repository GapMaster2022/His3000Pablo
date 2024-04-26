using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Formulario;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;

namespace His.Admision
{
    public partial class frm_AyudaCatalogoSubNivel : Form
    {
        //List<ANEXOS_IESS> listaAnexos = new List<ANEXOS_IESS>();
        List<ANEXOS_IESS> listaAnexos;
        //atributo que guarda el resultado seleccionado
        public string resultado;
        public string descripcion;
        public ANEXOS_IESS anexos = new ANEXOS_IESS();

        public frm_AyudaCatalogoSubNivel()
        {
            InitializeComponent();
            listaAnexos = new List<ANEXOS_IESS>();
            //inicializo opciones pro defecto del ultragrid
            ulgAnexos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            ulgAnexos.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;

            //timer1.Interval = 1500;            
            //cb_Sintomas.Focus();
        }

        public frm_AyudaCatalogoSubNivel(int codigoPadre)
        {
            InitializeComponent();
            listaAnexos = new List<ANEXOS_IESS>();
            //inicializo opciones pro defecto del ultragrid
            ulgAnexos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            ulgAnexos.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
           
            cargarCatalogo(Convert.ToString(codigoPadre));
        }


        private void cargarSubNiveles(int codigoPadre)
        {
            try
            {
                
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void cargarCatalogo(string codigo)
        {
            try
            {
                ulgAnexos.DataSource = null;
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var lista = (from a in contexto.ANEXOS_IESS
                                 where a.ANI_COD_PADRE == codigo
                                 select a).ToList();
                    ulgAnexos.DataSource = lista;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

       
        //private void ulgdbListadoCatSub_DoubleClick(object sender, EventArgs e)
        //{
        //    try 
        //    {
        //        resultado = ulgdbListadoCatSub.ActiveRow.Cells[1].Text;
        //        frm_DescripcionSubSintoma desSubSintoma = new frm_DescripcionSubSintoma(resultado);
        //        desSubSintoma.ShowDialog();
        //        descripcion = desSubSintoma.descripcion;
        //        if (descripcion != null)
        //        {
        //            this.Close();
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }             

        //}

      

        private void ulgdbListadoCatSub_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            try
            {
                anexos = (ANEXOS_IESS)ulgAnexos.ActiveRow.ListObject;
                if (anexos != null)
                    this.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cb_Sintomas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ulgAnexos_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
             try
             {
                 ulgAnexos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                 ulgAnexos.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;

                 ulgAnexos.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                 ulgAnexos.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                 ulgAnexos.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                 ulgAnexos.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
                 ulgAnexos.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;


                 ulgAnexos.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
                 ulgAnexos.DisplayLayout.Bands[0].Columns["ANI_CODIGO"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;
                 ulgAnexos.DisplayLayout.Bands[0].Columns["ANI_COD_PRO"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;
                 ulgAnexos.DisplayLayout.Bands[0].Columns["ANI_DESCRIPCION"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;
                 ulgAnexos.DisplayLayout.Bands[0].Columns["ANI_COD_PADRE"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;
                 ulgAnexos.DisplayLayout.Bands[0].Columns["ANI_ESTADO"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;


                 ulgAnexos.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
                 ulgAnexos.DisplayLayout.Override.DefaultRowHeight = 20;
                 ulgAnexos.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                 ulgAnexos.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded1;

                 ulgAnexos.DisplayLayout.Bands[0].Override.CellAppearance.BackColor = Color.LightCyan;
                 ulgAnexos.DisplayLayout.Bands[0].Override.CellAppearance.BackColor2 = Color.Azure;
                 ulgAnexos.DisplayLayout.Bands[0].Override.CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;


                 ulgAnexos.DisplayLayout.Bands[0].Columns["ANI_CODIGO"].Hidden = false;
                 ulgAnexos.DisplayLayout.Bands[0].Columns["ANI_COD_PRO"].Hidden = false;
                 ulgAnexos.DisplayLayout.Bands[0].Columns["ANI_DESCRIPCION"].Hidden = false;
                 ulgAnexos.DisplayLayout.Bands[0].Columns["ANI_COD_PADRE"].Hidden = true;
                 ulgAnexos.DisplayLayout.Bands[0].Columns["ANI_ESTADO"].Hidden = true;

                 //ulgdbListadoCatSub.DisplayLayout.Rows.ExpandAll(false);

             }
             catch (Exception err)
             {
                 MessageBox.Show(err.Message);
             }
        }

        private void ulgAnexos_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (Char)Keys.Enter)
                {
                    anexos = (ANEXOS_IESS)ulgAnexos.ActiveRow.ListObject;
                    if (anexos != null)
                        this.Close();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frm_AyudaCatalogoSubNivel_Load(object sender, EventArgs e)
        {

        }
    }
}
