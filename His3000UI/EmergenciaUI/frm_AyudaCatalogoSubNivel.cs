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
using His.Entidades.Clases;
using Recursos;
using Infragistics;
using His.General;
using System.Reflection;
using His.Parametros;
using His.Formulario;
using Infragistics.Win.UltraWinGrid;

namespace His.Emergencia
{
    public partial class frm_AyudaCatalogoSubNivel : Form
    {
        List<HC_CATALOGOS> catalogoSintomas = new List<HC_CATALOGOS>();
        List<HC_CATALOGO_SUBNIVEL> listaCatSub;
        //atributo que guarda el resultado seleccionado
        public string resultado;
        public string descripcion;

        public frm_AyudaCatalogoSubNivel()
        {
            InitializeComponent();
            listaCatSub = new List<HC_CATALOGO_SUBNIVEL>();
            //inicializo opciones pro defecto del ultragrid
            ulgdbListadoCatSub.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            ulgdbListadoCatSub.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
          
            //timer1.Interval = 1500;            
            cb_Sintomas.Focus();
            cargarSubNiveles();            
            cargarCatalogo();
        }

        private void cargarSubNiveles(){
            try
            {
                catalogoSintomas = NegCatalogos.RecuperarHcCatalogosPorTipo(23);
                cb_Sintomas.DataSource = catalogoSintomas;
                cb_Sintomas.ValueMember = "HCC_CODIGO";
                cb_Sintomas.DisplayMember = "HCC_NOMBRE";
                cb_Sintomas.AutoCompleteSource = AutoCompleteSource.ListItems;
                cb_Sintomas.AutoCompleteMode = AutoCompleteMode.SuggestAppend;                
                //using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                //{
                //    string buscar = txtBuscar.Text.ToString();
                //    var listaFinal = contexto.HC_CATALOGOS.Include("HC_CATALOGO_SUBNIVEL").Where(h => h.HC_CATALOGOS_TIPO.HCT_CODIGO == 23).ToList();
                //    ulgdbListadoCatSub.DataSource = listaFinal;
                //}
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarCatalogo();            
        }

        public void cargarCatalogo() 
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var listaCabecera = contexto.HC_CATALOGOS.Include("HC_CATALOGO_SUBNIVEL").Where(h => h.HC_CATALOGOS_TIPO.HCT_CODIGO == 23).ToList();                    
                    if (cb_Sintomas.SelectedIndex > (-1))
                    {
                        string sintoma = cb_Sintomas.Text;
                        var lista = (from t in contexto.HC_CATALOGOS_TIPO.Include("HC_CATALOGO_SUBNIVEL")
                                     join c in contexto.HC_CATALOGOS on t.HCT_CODIGO equals c.HC_CATALOGOS_TIPO.HCT_CODIGO
                                     join s in contexto.HC_CATALOGO_SUBNIVEL on c.HCC_CODIGO equals s.HC_CATALOGOS.HCC_CODIGO
                                     where c.HCC_NOMBRE.Contains(sintoma)
                                     select s).ToList();
                        ulgdbListadoCatSub.DataSource = null;

                        ulgdbListadoCatSub.DataSource = lista;
                    }                   

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ulgdbListadoCatSub_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {

                ulgdbListadoCatSub.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                ulgdbListadoCatSub.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;

                ulgdbListadoCatSub.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
                ulgdbListadoCatSub.DisplayLayout.Bands[0].Columns["CA_CODIGO"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;
                ulgdbListadoCatSub.DisplayLayout.Bands[0].Columns["CA_DESCRIPCION"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;
                ulgdbListadoCatSub.DisplayLayout.Bands[0].Columns["CA_INF"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;
                ulgdbListadoCatSub.DisplayLayout.Bands[0].Columns["HC_CATALOGOS"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;


                ulgdbListadoCatSub.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
                ulgdbListadoCatSub.DisplayLayout.Override.DefaultRowHeight = 20;
                ulgdbListadoCatSub.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                ulgdbListadoCatSub.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded1;

                ulgdbListadoCatSub.DisplayLayout.Bands[0].Override.CellAppearance.BackColor = Color.LightCyan;
                ulgdbListadoCatSub.DisplayLayout.Bands[0].Override.CellAppearance.BackColor2 = Color.Azure;
                ulgdbListadoCatSub.DisplayLayout.Bands[0].Override.CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;


                ulgdbListadoCatSub.DisplayLayout.Bands[0].Columns["CA_CODIGO"].Hidden = false;
                ulgdbListadoCatSub.DisplayLayout.Bands[0].Columns["CA_DESCRIPCION"].Hidden = false;
                ulgdbListadoCatSub.DisplayLayout.Bands[0].Columns["CA_INF"].Hidden = true;
                ulgdbListadoCatSub.DisplayLayout.Bands[0].Columns["HC_CATALOGOS"].Hidden = true; 

                //ulgdbListadoCatSub.DisplayLayout.Rows.ExpandAll(false);
                
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
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

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            cargarCatalogo(); 
        }

        private void ulgdbListadoCatSub_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            try
            {
                resultado = ulgdbListadoCatSub.ActiveRow.Cells[1].Text;
                frm_DescripcionSubSintoma desSubSintoma = new frm_DescripcionSubSintoma(resultado);
                desSubSintoma.ShowDialog();
                descripcion = desSubSintoma.descripcion;
                if (descripcion != null)
                {
                    this.Close();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }     

        }

        //private void ulgdbListadoCatSub_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        resultado = ulgdbListadoCatSub.ActiveRow.Cells[1].Text;
        //        this.Close();
        //    }
        //}    

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        btnBuscar_Click(null, null);
        //        timer1.Stop();
        //    }
        //    catch (Exception err)
        //    {
        //        MessageBox.Show(err.Message);
        //    }
        //}

        //private void txtBuscar_TextChanged(object sender, EventArgs e)
        //{


        //}

        //private void txtBuscar_Leave(object sender, EventArgs e)
        //{
        //    if (timer1.Enabled)
        //    {
        //        timer1.Stop();
        //    }
        //}

        //private void chbTodos_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chbTodos.Checked)
        //    {
        //        using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //        {
        //            var listaCabecera = contexto.HC_CATALOGOS.Include("HC_CATALOGO_SUBNIVEL").Where(h => h.HC_CATALOGOS_TIPO.HCT_CODIGO == 23).ToList();
        //            ulgdbListadoCatSub.DataSource = listaCabecera;
        //        }
        //        //timer1.Enabled = true;
        //    }
        //}
    }
}
