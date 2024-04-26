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
    public partial class frm_Catalogo : Form
    {
        List<HC_CATALOGOS> catalogo = new List<HC_CATALOGOS>();
        public HC_CATALOGOS catalogoFinal;
        
        public frm_Catalogo(int valor)
        {
            InitializeComponent();
            cargarCatalogo(valor);
        }

        private void cargarCatalogo(int valor)
        {
            try
            {
                if(valor == 1)
                    catalogo = NegCatalogos.RecuperarHcCatalogosPorTipo(17);
                if(valor == 2)
                    catalogo = NegCatalogos.RecuperarHcCatalogosPorTipo(18);
                if (valor == 3)
                    catalogo = NegCatalogos.RecuperarHcCatalogosPorTipo(21);
                if (valor == 4)
                    catalogo = NegCatalogos.RecuperarHcCatalogosPorTipo(21);
                if (valor == 5)
                    catalogo = NegCatalogos.RecuperarHcCatalogosPorTipo(14);
                    
                ultraGridCatalogo.DataSource = catalogo;               
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ultraGridCatalogo_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                ultraGridCatalogo.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                ultraGridCatalogo.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;

                ultraGridCatalogo.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
                ultraGridCatalogo.DisplayLayout.Bands[0].Columns["HCC_CODIGO"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;
                //ultraGridCatalogo.DisplayLayout.Bands[0].Columns["HCT_TIPO"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;
                ultraGridCatalogo.DisplayLayout.Bands[0].Columns["HCC_NOMBRE"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;
                ultraGridCatalogo.DisplayLayout.Bands[0].Columns["HCC_ESTADO"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;


                ultraGridCatalogo.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded1;

                ultraGridCatalogo.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
                ultraGridCatalogo.DisplayLayout.Override.DefaultRowHeight = 20;
                ultraGridCatalogo.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                ultraGridCatalogo.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded1;

                ultraGridCatalogo.DisplayLayout.Bands[0].Override.CellAppearance.BackColor = Color.LightCyan;
                ultraGridCatalogo.DisplayLayout.Bands[0].Override.CellAppearance.BackColor2 = Color.Azure;
                ultraGridCatalogo.DisplayLayout.Bands[0].Override.CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;


                ultraGridCatalogo.DisplayLayout.Bands[0].Columns["HCC_CODIGO"].Hidden = false;
                //ultraGridCatalogo.DisplayLayout.Bands[0].Columns["HCT_TIPO"].Hidden = false;
                ultraGridCatalogo.DisplayLayout.Bands[0].Columns["HCC_NOMBRE"].Hidden = false;
                ultraGridCatalogo.DisplayLayout.Bands[0].Columns["HCC_ESTADO"].Hidden = true;

               
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }     

        private void ultraGridCatalogo_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            try
            {
                catalogoFinal = new HC_CATALOGOS();
                catalogoFinal = (HC_CATALOGOS)ultraGridCatalogo.ActiveRow.ListObject;
                //MessageBox.Show(catalogoFinal.HCC_NOMBRE);
                this.Close();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    

        }

        
    }
}
