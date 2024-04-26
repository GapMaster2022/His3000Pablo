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
using Core.Entidades;
using His.DatosReportes;

namespace His.Maintenance
{
    public partial class frm_PreciosConvenios : Form
    {
        #region Variables
        //public PRECIOS_POR_CONVENIOS pconveniosOriginal = new PRECIOS_POR_CONVENIOS();
        //public PRECIOS_POR_CONVENIOS pconveniosModificada = new PRECIOS_POR_CONVENIOS();
        //public List<DtoPreciosConvenios> pconvenios = new List<DtoPreciosConvenios>();
        private List<CATEGORIAS_CONVENIOS> convenios = new List<CATEGORIAS_CONVENIOS>();
        private List<ASEGURADORAS_EMPRESAS> aseguradoras = new List<ASEGURADORAS_EMPRESAS>();
        //private List<CATALOGO_COSTOS_TIPO> estructuraCatalogos = new List<CATALOGO_COSTOS_TIPO>();

        private bool estadoGrid = false;
        private bool nuevo;
        #endregion
        #region constructores
        public frm_PreciosConvenios()
        {
            InitializeComponent();
            btnImprimir.Image = Recursos.Archivo.imgBtnImprimir32;
            inicializarControles();
        }
        #endregion
        #region Eventos
        private void inicializarControles()
        {
            //
            cboTipoEmpresa.DataSource = NegAseguradoras.RecuperaTipoEmpresa();
            cboTipoEmpresa.ValueMember = "TE_CODIGO";
            cboTipoEmpresa.DisplayMember = "TE_DESCRIPCION";
            cboTipoEmpresa.SelectedIndex = 0;
            cboTipoEmpresa.DropDownStyle = ComboBoxStyle.DropDownList;
            //Cmb_Convenios.DropDownStyle = ComboBoxStyle.DropDownList;
            Cmb_Convenios.Text = " ";
            btnEliminar.Visible = false;
            btnImprimir.Enabled = false;
            cboTipoEmpresa.Text = " ";
            btnActualizar.Enabled = false;
            //
            //TIPO_EMPRESA tipoEmpresa = (TIPO_EMPRESA)cboTipoEmpresa.SelectedItem; 
            //convenios = NegCategorias.ListaCategoriasConPrecios(tipoEmpresa.TE_CODIGO );
            //Cmb_Convenios.DataSource = convenios;   
            //Cmb_Convenios.ValueMember = "CAT_CODIGO";
            //Cmb_Convenios.DisplayMember = "CAT_NOMBRE"; 

            //
            gridPconvenios.DataSource = NegCatalogoCostos.RecuperarEstructuraCatalogos();

        }
        private void frm_PreciosConvenios_Load(object sender, EventArgs e)
        {
            HalitarControles(true, false, false, false, true, true, false);
            Cmb_Convenios.Text = " ";
            txtDescripcion.Text = " ";
            try
            {
                aseguradoras = NegAseguradoras.ListaEmpresas();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {

                lblDescripcion.Visible = false;
                txtDescripcion.Visible = false;
                lblFechaInicio.Visible = false;
                txtFechaInicio.Visible = false;
                lblFechaFin.Visible = false;
                txtFechaFin.Visible = false;


                gridPconvenios.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
                limpiarCamposGrid();
                nuevo = true;
                TIPO_EMPRESA tipoEmpresa = (TIPO_EMPRESA)cboTipoEmpresa.SelectedItem;
                cboTipoEmpresa_SelectedIndexChanged(null, null);
                //convenios = NegCategorias.ListaCategoriasSinPrecios(tipoEmpresa.TE_CODIGO ) ;

                //Cmb_Convenios.DataSource = null;
                //Cmb_Convenios.DataSource = convenios;
                //if (null != convenios.ToList())
                //{
                //    Cmb_Convenios.ValueMember = "CAT_CODIGO";
                //    Cmb_Convenios.DisplayMember = "CAT_NOMBRE";
                //}
                Cmb_Convenios.BackColor = Color.White;
                cboTipoEmpresa.BackColor = Color.White;
                HalitarControles(true, true, false, true, false, false, true);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                gridPconvenios.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
                HalitarControles(false, true, false, true, false, false, true);
                //GrabarDatos();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                GrabarDatos();
                nuevo = false;
                Cmb_Convenios.BackColor = Color.Gainsboro;
                cboTipoEmpresa.BackColor = Color.Gainsboro;
                HalitarControles(true, true, true, false, true, true, false);
                lblDescripcion.Visible = true;
                txtDescripcion.Visible = true;
                lblFechaInicio.Visible = true;
                txtFechaInicio.Visible = true;
                lblFechaFin.Visible = true;
                txtFechaFin.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);

            }
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            HalitarControles(false, false, false, false, false, true, false);
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                CATEGORIAS_CONVENIOS convenio = (CATEGORIAS_CONVENIOS)Cmb_Convenios.SelectedItem;
                if (convenio != null)
                {
                    DialogResult mensaje = new DialogResult();
                    mensaje = MessageBox.Show("Esta seguro de eliminar los precios del convenio?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (mensaje == DialogResult.OK)
                        NegPrecioConvenios.EliminarPreciosPorConvenio(convenio.CAT_CODIGO);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
        #region Metodos
        private void GrabarDatos()
        {
            try
            {

                CATEGORIAS_CONVENIOS convenio = (CATEGORIAS_CONVENIOS)Cmb_Convenios.SelectedItem;
                //if (nuevo == false)
                //{
                //    NegPrecioConvenios.EliminarPreciosPorConvenio(convenio.CAT_CODIGO);
                //}
                int codigo = NegPrecioConvenios.RecuperaMaximoPconvenios() + 1;
                List<PRECIOS_POR_CONVENIOS> precon = new List<PRECIOS_POR_CONVENIOS>();
                for (Int16 i = 0; i < gridPconvenios.Rows.Count; i++)
                {
                    for (Int16 j = 0; j < gridPconvenios.Rows[i].ChildBands[0].Rows.Count; j++)
                    {
                        if (Convert.ToBoolean(gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells[7].Value) == true)
                        {
                            codigo++;
                            CATALOGO_COSTOS catalogo = (CATALOGO_COSTOS)gridPconvenios.Rows[i].ChildBands[0].Rows[j].ListObject;
                            PRECIOS_POR_CONVENIOS preciosConvenios = new PRECIOS_POR_CONVENIOS();
                            preciosConvenios.PRE_PORCENTAJE = (Decimal)gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPorcentaje"].Value;
                            preciosConvenios.PRE_VALOR = (Decimal)gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPrecio"].Value;
                            preciosConvenios.CATALOGO_COSTOSReference.EntityKey = catalogo.EntityKey;
                            preciosConvenios.CATEGORIAS_CONVENIOSReference.EntityKey = convenio.EntityKey;
                            preciosConvenios.PRE_CODIGO = codigo;
                            precon.Add(preciosConvenios);
                            //NegPrecioConvenios.CrearPconvenios(preciosConvenios);

                            ///NegPrecioConvenios.IngresaCodigo(catalogo.CAC_NOMBRE, convenio.CAT_CODIGO, catalogo.CAC_CODIGO);

                        }
                    }
                }
                if (NegPrecioConvenios.creaPreciosConvenio(convenio.CAT_CODIGO, precon))
                    MessageBox.Show("La in formación se guardo exitosamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    MessageBox.Show("La in formación no se pudo guardar", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                NegPrecioConvenios.ActualizaCodigosSic();
                nuevo = false;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected virtual void HalitarControles(bool datosPrincipales, bool datosSecundarios, bool Modificar, bool Grabar, bool Eliminar, bool Nuevo, bool Cancelar)
        {
            btnNuevo.Enabled = Nuevo;
            //btnActualizar.Enabled = Modificar;
            btnEliminar.Enabled = Eliminar;
            btnGuardar.Enabled = Grabar;
            btnCancelar.Enabled = Cancelar;
            grpDatosPrincipales.Enabled = datosPrincipales;

            // this.btnImprimir.Enabled = false;
            //  btnActualizar.Enabled = false;

        }

        # endregion
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void gridPconvenios_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            if (estadoGrid == false)
            {
                gridPconvenios.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                gridPconvenios.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
                gridPconvenios.DisplayLayout.Bands[2].Hidden = true;

                //Añado la columna check
                gridPconvenios.DisplayLayout.Bands[1].Columns.Add("columnaCheck", "");
                gridPconvenios.DisplayLayout.Bands[1].Columns["columnaCheck"].DataType = typeof(bool);
                gridPconvenios.DisplayLayout.Bands[1].Columns["columnaCheck"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;

                gridPconvenios.DisplayLayout.Bands[1].Columns.Add("columnaPrecio", "");
                gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPrecio"].DataType = typeof(Decimal);
                gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPrecio"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CurrencyNonNegative;
                gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPrecio"].Header.Caption = "Precio";

                gridPconvenios.DisplayLayout.Bands[1].Columns.Add("columnaPorcentaje", "");
                gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].DataType = typeof(Decimal);
                gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CurrencyNonNegative;
                gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].Header.Caption = "Porcentaje";

                gridPconvenios.DisplayLayout.Bands[0].Override.CellAppearance.BackColor = Color.LightCyan;
                gridPconvenios.DisplayLayout.Bands[0].Override.CellAppearance.BackColor2 = Color.Azure;
                gridPconvenios.DisplayLayout.Bands[0].Override.CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;

                estadoGrid = true;
            }
            gridPconvenios.DisplayLayout.Bands[0].Columns["CCT_CODIGO"].Hidden = true;
            gridPconvenios.DisplayLayout.Bands[1].Columns["CATALOGO_COSTOS_TIPO"].Hidden = true;
            gridPconvenios.DisplayLayout.Bands[1].Columns["CAC_CODIGO"].Hidden = true;
            //
            gridPconvenios.DisplayLayout.Bands[0].Columns["CCT_NOMBRE"].Header.Caption = "Tipo Costo";
            gridPconvenios.DisplayLayout.Bands[1].Columns["CAC_NOMBRE"].Header.Caption = "Descripcion";

            //
            gridPconvenios.DisplayLayout.Bands[1].Columns["columnaCheck"].Header.VisiblePosition = 2;
            gridPconvenios.DisplayLayout.Bands[1].Columns["CAC_NOMBRE"].Header.VisiblePosition = 1;
            gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPrecio"].Header.VisiblePosition = 3;
            gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].Header.VisiblePosition = 4;

            //
            gridPconvenios.DisplayLayout.Bands[1].Columns["columnaCheck"].Width = 30;
            gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPrecio"].Width = 70;
            gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].Width = 70;
            gridPconvenios.DisplayLayout.Bands[1].Columns["CAC_NOMBRE"].Width = 380;

            //
            gridPconvenios.DisplayLayout.Bands[0].Columns["CCT_NOMBRE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            //gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPrecio"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            //gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            gridPconvenios.DisplayLayout.Bands[1].Columns["CAC_NOMBRE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            for (Int16 i = 0; i < gridPconvenios.Rows.Count; i++)
            {
                for (Int16 j = 0; j < gridPconvenios.Rows[i].ChildBands[0].Rows.Count; j++)
                {
                    gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPrecio"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPorcentaje"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                }
            }

        }
        private void gridPconvenios_InitializeGroupByRow(object sender, Infragistics.Win.UltraWinGrid.InitializeGroupByRowEventArgs e)
        {
            e.Row.ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
            e.Row.Expanded = true;
        }
        private void gridPconvenios_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {

            if (e.Cell.Column.Index == 7)
            {
                if (Convert.ToBoolean(gridPconvenios.ActiveRow.Cells[7].Value) == false)
                {
                    gridPconvenios.ActiveRow.Cells["columnaPrecio"].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    gridPconvenios.ActiveRow.Cells["columnaPorcentaje"].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    gridPconvenios.ActiveRow.Cells["columnaPrecio"].Value = 0;
                    gridPconvenios.ActiveRow.Cells["columnaPorcentaje"].Value = 0;
                }
                else
                {
                    gridPconvenios.ActiveRow.Cells["columnaPrecio"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    gridPconvenios.ActiveRow.Cells["columnaPorcentaje"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    gridPconvenios.ActiveRow.Cells["columnaPrecio"].Value = null;
                    gridPconvenios.ActiveRow.Cells["columnaPorcentaje"].Value = null;
                }
            }
        }
        private void cboTipoEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TIPO_EMPRESA tipoEmpresa = (TIPO_EMPRESA)cboTipoEmpresa.SelectedItem;


                Cmb_Convenios.DataSource = null;
                if (nuevo == true)
                {
                    Cmb_Convenios.DataSource = NegCategorias.ListaCategoriasSinPrecios(tipoEmpresa.TE_CODIGO);
                }
                else
                {
                    Cmb_Convenios.DataSource = NegCategorias.ListaCategoriasConPrecios(tipoEmpresa.TE_CODIGO);
                }

                if (null != Cmb_Convenios.DataSource)
                {
                    Cmb_Convenios.ValueMember = "CAT_CODIGO";
                    Cmb_Convenios.DisplayMember = "CAT_NOMBRE";
                }

                Cmb_Convenios.Text = "";
                txtDescripcion.Text = "";
                btnActualizar.Enabled = false;
                btnImprimir.Enabled = false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cargarPreciosPorConvenio()
        {
            try
            {
                CATEGORIAS_CONVENIOS convenio = (CATEGORIAS_CONVENIOS)Cmb_Convenios.SelectedItem;
                List<PRECIOS_POR_CONVENIOS> preciosConveniosLista = NegPrecioConvenios.RecuperarPreciosPorConvenio(convenio.CAT_CODIGO);

                for (Int16 i = 0; i < gridPconvenios.Rows.Count; i++)
                {
                    for (Int16 j = 0; j < gridPconvenios.Rows[i].ChildBands[0].Rows.Count; j++)
                    {
                        foreach (var item in preciosConveniosLista)
                        {
                            if (Convert.ToInt32(gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["CAC_CODIGO"].Value) == item.CATALOGO_COSTOS.CAC_CODIGO)
                            {
                                gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaCheck"].Value = true;
                                gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPrecio"].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                                gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPorcentaje"].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                                gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPrecio"].Value = item.PRE_VALOR;
                                gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPorcentaje"].Value = item.PRE_PORCENTAJE;

                            }
                        }

                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Cmb_Convenios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (nuevo == true)
                {
                    return;
                }

                CATEGORIAS_CONVENIOS convenio = (CATEGORIAS_CONVENIOS)Cmb_Convenios.SelectedItem;
                limpiarCamposGrid();
                if (Cmb_Convenios.DataSource == null)
                {
                    return;
                }
                cargarPreciosPorConvenio();
                txtDescripcion.Text = convenio.CAT_DESCRIPCION == null ? "" : convenio.CAT_DESCRIPCION;
                txtFechaInicio.Text = convenio.CAT_FECHA_INICIO == null ? "" : convenio.CAT_FECHA_INICIO.Value.Date.ToString();
                txtFechaFin.Text = convenio.CAT_FECHA_FIN == null ? "" : convenio.CAT_FECHA_FIN.Value.Date.ToString();
                btnActualizar.Enabled = true;
                btnImprimir.Enabled = true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void limpiarCamposGrid()
        {
            for (Int16 i = 0; i < gridPconvenios.Rows.Count; i++)
            {
                for (Int16 j = 0; j < gridPconvenios.Rows[i].ChildBands[0].Rows.Count; j++)
                {
                    gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaCheck"].Value = false;
                    gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPrecio"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPorcentaje"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPrecio"].Value = null;
                    gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPorcentaje"].Value = null;
                }
            }
        }
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            nuevo = false;
            limpiarCamposGrid();
            Cmb_Convenios.BackColor = Color.Gainsboro;
            cboTipoEmpresa.BackColor = Color.Gainsboro;
            HalitarControles(true, true, true, false, true, true, false);
            lblDescripcion.Visible = true;
            txtDescripcion.Visible = true;
            lblFechaInicio.Visible = true;
            txtFechaInicio.Visible = true;
            lblFechaFin.Visible = true;
            txtFechaFin.Visible = true;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            CATEGORIAS_CONVENIOS categoria = (CATEGORIAS_CONVENIOS)Cmb_Convenios.SelectedItem;
            //if (categoria != null)
            //{
            //   frmReportes frmReportes = new frmReportes();
            //   frmReportes.reporte = "preciosConvenios";
            //   frmReportes.parametro = categoria.CAT_CODIGO;
            //   frmReportes.ShowDialog();     
            //}



            DataTable x = NegDietetica.getDataTable("GetPreciosConvenio", Convert.ToString(categoria.CAT_CODIGO));

            ReportesHistoriaClinica imagenL = new ReportesHistoriaClinica();
            imagenL.DeleteTable("PreciosConvenio");

            foreach (DataRow row in x.Rows)
            {
                string[] y = new string[]{
                    row["ASE_NOMBRE"].ToString(),
                    row["ASE_RUC"].ToString(),
                    row["ASE_DIRECCION"].ToString(),
                    row["CAT_NOMBRE"].ToString(),
                    row["PRE_VALOR"].ToString(),
                    row["PRE_PORCENTAJE"].ToString(),
                    row["CAC_NOMBRE"].ToString(),
                    row["CCT_NOMBRE"].ToString()
                };
                ReportesHistoriaClinica imagendx = new ReportesHistoriaClinica();
                imagendx.InsertTable("PreciosConvenio", y);
            }

            His.Formulario.frmReportes ventana = new His.Formulario.frmReportes(1, "PreciosConvenio");
            ventana.Show();


        }



















    }
}
