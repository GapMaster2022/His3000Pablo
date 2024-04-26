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

namespace His.Formulario
{
    public partial class frmAyudaReceta : Form
    {
        public int codigoArea;
        public int _Rubro;
        public int _CodigoConvenio;
        public int _CodigoEmpresa;
        Boolean _todos = false;

        public string medicamento = "";
        public string codpro = "";
        public frmAyudaReceta()
        {
            InitializeComponent();
        }
        public string producto = "";
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!chkAutoBusqueda.Checked)
                buscar();
            else
                BuscarBasicos();
        }

        public void BuscarBasicos()
        {
            try
            {
                UltraGridDatos.DataSource = NegCertificadoMedico.ProductosBasicos(txtBuscar.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void buscar()
        {
            try
            {
                //cargo la lista de cantidad a paginar
                DataTable dtProductos = new DataTable();

                List<DataRow> List = new List<DataRow>();

                if (codigoArea != 1)
                {
                    if (_CodigoConvenio == 0)
                    {
                        if (codigoArea > 0)
                        {
                            //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null,codigoArea);

                            if (chkAutoBusqueda.Checked == true)
                            {
                                if (_todos)  ///alexalex
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(2, txtBuscar.Text.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio); // Busca por el generico / Giovanny Tapia / 18/10/2012
                                }
                                else
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(2, txtBuscar.Text.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio); // Busca por el generico / Giovanny Tapia / 18/10/2012
                                }
                            }

                            else
                            {
                                if (_todos)  ///alexalex
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(1, txtBuscar.Text.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
                                }
                                else
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, txtBuscar.Text.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (codigoArea > 0)
                    {

                        if (chkAutoBusqueda.Checked == true)
                        {
                            dtProductos = Negocio.NegProducto.RecuperarProductosListaSP_Farmacia(2, txtBuscar.Text.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
                        }

                        else
                        {
                            //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null);
                            dtProductos = Negocio.NegProducto.RecuperarProductosListaSP_Farmacia(1, txtBuscar.Text.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
                        }

                    }
                }

                //xamDataPresenterProductos.DataSource = listaProductosDisponibles;
                //xamDataPresenterProductos.DataSource = dtProductos;
                UltraGridDatos.DataSource = dtProductos;
                UltraGridDatos.DisplayLayout.Bands[0].Columns[3].Hidden = true;
                UltraGridDatos.DisplayLayout.Bands[0].Columns[4].Hidden = true;
                UltraGridDatos.DisplayLayout.Bands[0].Columns[5].Hidden = true;
                UltraGridDatos.DisplayLayout.Bands[0].Columns[6].Hidden = true;
                UltraGridDatos.DisplayLayout.Bands[0].Columns[7].Hidden = true;
                if (UltraGridDatos.Rows.Count > 0)
                {
                    UltraGridDatos.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void UltraGridDatos_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = UltraGridDatos.DisplayLayout.Bands[0];

            UltraGridDatos.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            UltraGridDatos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            UltraGridDatos.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            UltraGridDatos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            UltraGridDatos.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            UltraGridDatos.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            UltraGridDatos.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            UltraGridDatos.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

            //Caracteristicas de Filtro en la grilla
            UltraGridDatos.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            UltraGridDatos.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            UltraGridDatos.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            UltraGridDatos.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            UltraGridDatos.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            UltraGridDatos.DisplayLayout.UseFixedHeaders = true;

            UltraGridDatos.DisplayLayout.Bands[0].Columns[0].Hidden = true;


            UltraGridDatos.DisplayLayout.Bands[0].Columns[1].Width = 80;
            UltraGridDatos.DisplayLayout.Bands[0].Columns[2].Width = 200;
        }

        private void frmAyudaReceta_Load(object sender, EventArgs e)
        {
            txtBuscar.Text = producto;
            if (!chkAutoBusqueda.Checked)
                buscar();
            else
                BuscarBasicos();
        }
        public string presentacion = "";
        private void UltraGridDatos_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if(UltraGridDatos.Selected.Rows.Count == 1)
            {
                UltraGridRow fila = UltraGridDatos.ActiveRow;

                if (!chkAutoBusqueda.Checked)
                {
                    medicamento = fila.Cells[2].Value.ToString();
                    codpro = fila.Cells[1].Value.ToString();
                    this.Close();
                }
                else
                {
                    presentacion = fila.Cells[3].Value.ToString();
                    medicamento = fila.Cells[1].Value.ToString();
                    codpro = fila.Cells[0].Value.ToString();
                    this.Close();
                }

            }
        }
    }
}
