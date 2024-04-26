using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using His.Formulario;
using His.Entidades.Clases;
using His.Entidades;
using System.Threading;

namespace His.Dietetica
{

    public partial class frmQuirofanoProductosDetalle : Form
    {
        NegQuirofano Quirofano = new NegQuirofano();
        NegCertificadoMedico Certificado = new NegCertificadoMedico();
        public DataTable TablaProductosReposicion = new DataTable();
        public DataTable TablaProductoPacientes = new DataTable();

        //public int bodega = Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()); //bodega 12 por defecto
        public int bodega = Sesion.bodega; //bodega 12 por defecto
        public frmQuirofanoProductosDetalle()
        {
            InitializeComponent();
        }

        public frmQuirofanoProductosDetalle(int bodega)
        {
            InitializeComponent();
            this.bodega = bodega;
        }
        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmQuirofanoProductosDetalle_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = Convert.ToDateTime(String.Format("{0:g}", (DateTime.Now.Year).ToString() + "/" + DateTime.Now.Month + "/01"));
            dtpHasta.Value = DateTime.Now;
            try
            {
                if (bodega == Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()))
                {
                    cmbUsuarios.DataSource = NegQuirofano.UsuariosReposicion();
                    cmbUsuarios.ValueMember = "CODIGO";
                    cmbUsuarios.DisplayMember = "USUARIO";
                }
                else
                {
                    cmbUsuarios.DataSource = NegUsuarios.UsuarioReposicion(Sesion.codUsuario);
                    cmbUsuarios.ValueMember = "Codigo";
                    cmbUsuarios.DisplayMember = "Usuario";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio al cargar los usuarios de reposicion: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (dtpDesde.Value < dtpHasta.Value)
            {
                try
                {
                    if (NegParametros.ParametroReposicionesBienes())
                    {
                        if (Sesion.bodega == NegParametros.ParametroBodegaQuirofano())
                        {
                            His.Datos.dsProcedimiento ds = NegQuirofano.reposicionQuirofano(dtpDesde.Value, dtpHasta.Value,Sesion.bodega);
                            UltraGridProductos.DataSource = ds.Pedido;
                            TablaProductosReposicion = NegQuirofano.RecuperoReposicion(dtpDesde.Value, dtpHasta.Value, Sesion.bodega);
                            ultraGrid2.DataSource = TablaProductosReposicion;
                        }
                        if (Sesion.bodega == NegParametros.ParametroBodegaGastro())
                        {
                            His.Datos.dsProcedimiento ds = NegQuirofano.reposicionGastro(dtpDesde.Value, dtpHasta.Value,Sesion.bodega);
                            UltraGridProductos.DataSource = ds.Pedido;
                            TablaProductosReposicion = NegQuirofano.RecuperoReposicion(dtpDesde.Value, dtpHasta.Value, Sesion.bodega);
                            ultraGrid2.DataSource = TablaProductosReposicion;
                        }
                    }
                    else
                    {
                        if (Sesion.bodega == NegParametros.ParametroBodegaQuirofano())
                        {
                            His.Datos.dsProcedimiento ds = NegQuirofano.reposicionQuirofanoServicios(dtpDesde.Value, dtpHasta.Value,Sesion.bodega);
                            UltraGridProductos.DataSource = ds.Pedido;
                            TablaProductosReposicion = NegQuirofano.RecuperoReposicionServicio(dtpDesde.Value, dtpHasta.Value, Sesion.bodega);
                            ultraGrid2.DataSource = TablaProductosReposicion;
                        }
                        if (Sesion.bodega == NegParametros.ParametroBodegaGastro())
                        {
                            His.Datos.dsProcedimiento ds = NegQuirofano.reposicionGastroServicios(dtpDesde.Value, dtpHasta.Value,Sesion.bodega);
                            UltraGridProductos.DataSource = ds.Pedido;
                            TablaProductosReposicion = NegQuirofano.RecuperoReposicionServicio(dtpDesde.Value, dtpHasta.Value, Sesion.bodega);
                            ultraGrid2.DataSource = TablaProductosReposicion;
                        }
                    }
                        

                    //dtpHasta.Value = dtpHasta.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                    //UltraGridProductos.DataSource = Quirofano.RecuperarProductosUsados(dtpDesde.Value, dtpHasta.Value, Convert.ToInt32(cmbUsuarios.SelectedValue.ToString()));
                    //TablaProductosReposicion = NegQuirofano.RecuperoReposicion(dtpDesde.Value, dtpHasta.Value, Convert.ToInt32(cmbUsuarios.SelectedValue.ToString()));
                    //TablaProductoPacientes = Quirofano.RecuperarProductosUsados(dtpDesde.Value, dtpHasta.Value, Convert.ToInt32(cmbUsuarios.SelectedValue.ToString()));
                    //ultraGrid1.DataSource = NegQuirofano.DetalleExportar(dtpDesde.Value, dtpHasta.Value); //hay que preguntar si es valido que totalice todo sin tener en cuenta el usuario
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Fecha \"Desde\" no puede mayor a fecha \"Hasta\"", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (UltraGridProductos.Rows.Count > 0)
                {
                    string PathExcel = FindSavePath();
                    if (PathExcel != null)
                    {
                        this.ultraGridExcelExporter1.Export(UltraGridProductos, PathExcel);
                        MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                    }
                }
                else
                {
                    MessageBox.Show("No tiene Registros para Exportar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private String FindSavePath()
        {
            Stream myStream;
            string myFilepath = null;
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        myFilepath = saveFileDialog1.FileName;
                        myStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myFilepath;
        }

        private void btnimprimir_Click(object sender, EventArgs e)
        {
            if (UltraGridProductos.Rows.Count > 0)
            {
                try
                {
                    string path = Certificado.path();
                    QuirofanoProductos QP = new QuirofanoProductos();
                    DataRow dr;
                    foreach (var item in UltraGridProductos.Rows)
                    {
                        dr = QP.Tables["Quirofano_Productos"].NewRow();
                        dr["Codigo"] = item.Cells[0].Value.ToString();
                        dr["Producto"] = item.Cells[1].Value.ToString();
                        dr["Cantidad"] = item.Cells[2].Value.ToString();
                        dr["Logo"] = path;
                        dr["Usuario"] = His.Entidades.Clases.Sesion.nomUsuario;
                        QP.Tables["Quirofano_Productos"].Rows.Add(dr);
                    }
                    frmReportes Reporte = new frmReportes(1, "QuirofanoProductos", QP);
                    Reporte.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public DateTime fechaReposicion;
        private void btnreposicion_Click(object sender, EventArgs e)
        {
            Error.Clear();
            if (txtobservacion.Text.Trim() != "")
            {
                if (MessageBox.Show("¿Está seguro de realizar la reposición?", "HIS3000", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    fechaReposicion = DateTime.Now;
                    //SE GENERA LA REPOSICION AL SIC3000
                    GenerarReposicion();
                    this.Close();
                }
            }
            else
                Error.SetError(txtobservacion, "CAMPO OBLIGATORIO");
        }
        public void CerrarProceso( Int64 numdoc )
        {
            DSReposicion repos = new DSReposicion();
            UltraGridBand band = this.UltraGridProductos.DisplayLayout.Bands[0];
            foreach (UltraGridRow row in band.GetRowEnumerator(GridRowType.DataRow))
            {
                if (Convert.ToBoolean(row.Cells["ESTADO"].Text))
                {
                    if (!NegQuirofano.CambioEstadoReposicion(Convert.ToInt64(row.Cells["CODIGO"].Text), numdoc))
                    {
                    }
                }
            }
        }

        public void GenerarReposicion()
        {
            #region Codigo Reposicion
            if (TablaProductosReposicion.Rows.Count > 0)
            {
                try
                {
                    Int64 numdoc = NegQuirofano.NumeroControl();
                    if (numdoc == 0)
                    {
                        Thread.Sleep(1000); //Espero 2 segundos
                        GenerarReposicion();
                    }
                    else
                    {
                        DSReposicion repos = new DSReposicion();
                        DataRow cabReposicion;
                        NegQuirofano.OcuparNumControl();
                        //numdoc += 1; //Se le suma uno
                        NegQuirofano.LiberarNumControl(); //Tomado el numdoc se libera el numero y se le suma 1 para que otro pueda seguir con el proceso
                                                          //if(bodega == NegParametros.ParametroBodegaQuirofano())
                                                          //{
                                                          //    NegQuirofano.CreaPedidoReposicion(numdoc, fechaReposicion.Date, fechaReposicion.ToString("HH:mm"), @bodega, Sesion.bodega_reposicion, txtobservacion.Text, 'A', Sesion.codUsuario); //ENVIARA A LA BODEGA DE FARMACIA//Se camnia de la Bodega quemada 10 // ha trabajar por IP-Bodega//Mario 16/02/2023
                                                          //}
                                                          //else if(bodega == NegParametros.ParametroBodegaGastro())
                                                          //{
                                                          //    NegQuirofano.CreaPedidoReposicion(numdoc, fechaReposicion.Date, fechaReposicion.ToString("HH:mm"), @bodega, Sesion.bodega_reposicion, txtobservacion.Text, 'A', Sesion.codUsuario); //ENVIARA A LA BODEGA DE FARMACIA//Se camnia de la Bodega quemada 10 // ha trabajar por IP-Bodega//Mario 16/02/2023
                                                          //}
                                                          //else
                                                          //{

                        DataRow detReposicion;
                        int linea = 0;
                        
                        List<RepoQuirofano> reposicion = new List<RepoQuirofano>();
                        foreach (DataRow item in TablaProductosReposicion.Rows)
                        {
                            linea += 1;
                            //NegQuirofano.DetalleReposicion(item["PRO_CODIGO"].ToString(), item["CUE_DETALLE"].ToString(), Convert.ToDouble(item["CUE_CANTIDAD"].ToString()), linea, numdoc);
                            detReposicion = repos.Tables["detReposicion"].NewRow();
                            detReposicion["codpro"] = item["PRO_CODIGO"].ToString();
                            detReposicion["despro"] = item["CUE_DETALLE"].ToString();
                            detReposicion["cantidad"] = item["CUE_CANTIDAD"].ToString();
                            repos.Tables["detReposicion"].Rows.Add(detReposicion);

                            RepoQuirofano obj = new RepoQuirofano();
                            obj.codpro = item["PRO_CODIGO"].ToString();
                            obj.despro = item["CUE_DETALLE"].ToString();
                            obj.linea = linea;
                            obj.cantidad = Convert.ToDecimal(item["CUE_CANTIDAD"].ToString());
                            obj.numdoc = numdoc;
                            reposicion.Add(obj);

                            //Thread.Sleep(500);
                        }
                        if (NegQuirofano.CreaPedidoReposicion(numdoc, fechaReposicion.Date, fechaReposicion.ToString("HH:mm"), Sesion.bodega, Sesion.bodega_reposicion, txtobservacion.Text, 'A', Sesion.codUsuario, reposicion))//ENVIARA A LA BODEGA DE SUBSUELO LA PRINCIPAL//Se camnia de la Bodega quemada 10 // se cambia para trabajar por IP-Bodega//Mario 16/02/2023
                                                                                                                                                                                                                     //}
                        {
                            CerrarProceso(numdoc);
                        }
                        His.Entidades.USUARIOS usuario = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
                        string bodegaOrigen = NegPedidos.recureraBodega(Sesion.bodega);
                        string bodegaDestino = NegPedidos.recureraBodega(Sesion.bodega_reposicion);
                        cabReposicion = repos.Tables["cabReposicion"].NewRow();
                        cabReposicion["responsable"] = usuario.APELLIDOS + " " + usuario.NOMBRES;
                        cabReposicion["fecha"] = fechaReposicion.Date;
                        cabReposicion["borigen"] = bodegaOrigen;
                        cabReposicion["bdestino"] = bodegaDestino;
                        cabReposicion["observacion"] = txtobservacion.Text;
                        cabReposicion["npedido"] = numdoc;
                        cabReposicion["path"] = Certificado.path();
                        cabReposicion["hora"] = fechaReposicion.ToString("HH:mm");
                        repos.Tables["cabReposicion"].Rows.Add(cabReposicion);
                        

                        
                        MessageBox.Show("Reposición enviada con éxito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //DataTable ReposicionPedido = NegQuirofano.RepocisionPedido(numdoc);

                        frmReportes myreport = new frmReportes(1, "Reposicion", repos);
                        myreport.Show();

                        //foreach (DataRow item in TablaProductoPacientes.Rows)
                        //{
                        //    if (item["F. REPOSI"].ToString() == "")
                        //    {
                        //        try
                        //        {
                        //            //GUARDO LA FECHA Y EL NUMDOC DE LA REPOSICION  CON EL NUMERO DE ATENCION Y EL TIPO DE PROCEDIMIENTO.
                        //            NegQuirofano.FechaReposicion(fechaReposicion, Convert.ToInt32(item["PCI_CODIGO"].ToString()),
                        //                Convert.ToInt64(item["ATE_CODIGO"].ToString()), numdoc);
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            MessageBox.Show("Algo ocurrio al generar la reposición." + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //        }
                        //    }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Algo ocurrio al crear el detalle de la reposición.\r\nMás detalle: " + ex.Message,
                        "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No tiene productos que deban hacerse la reposición.", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            #endregion
        }


        private void ultraGrid1_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ultraGrid1.DisplayLayout.Bands[0];

            ultraGrid1.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            ultraGrid1.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            ultraGrid1.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            ultraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            ultraGrid1.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            ultraGrid1.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            ultraGrid1.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            UltraGridProductos.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

            //Caracteristicas de Filtro en la grilla
            ultraGrid1.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGrid1.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGrid1.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGrid1.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            ultraGrid1.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            ultraGrid1.DisplayLayout.UseFixedHeaders = true;

            //Dimension los registros
            ultraGrid1.DisplayLayout.Bands[0].Columns[0].Width = 50;
            ultraGrid1.DisplayLayout.Bands[0].Columns[1].Width = 100;
            ultraGrid1.DisplayLayout.Bands[0].Columns[2].Width = 60;
        }

        private void detallePorItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (UltraGridProductos.Rows.Count > 0)
                {
                    string PathExcel = FindSavePath();
                    if (PathExcel != null)
                    {
                        this.ultraGridExcelExporter1.Export(UltraGridProductos, PathExcel);
                        MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                    }
                }
                else
                {
                    MessageBox.Show("No tiene Registros para Exportar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void detalleTotalizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ultraGrid1.Rows.Count > 0)
                {
                    string PathExcel = FindSavePath();
                    if (PathExcel != null)
                    {
                        this.ultraGridExcelExporter1.Export(ultraGrid1, PathExcel);
                        MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                    }
                }
                else
                {
                    MessageBox.Show("No tiene Registros para Exportar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void UltraGridProductos_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                UltraGridBand bandUno = UltraGridProductos.DisplayLayout.Bands[0];

                //ultraGridProcedimiento.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                UltraGridProductos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                UltraGridProductos.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                UltraGridProductos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                UltraGridProductos.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                UltraGridProductos.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                UltraGridProductos.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                UltraGridProductos.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                UltraGridProductos.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                UltraGridProductos.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                UltraGridProductos.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                UltraGridProductos.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                UltraGridProductos.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                UltraGridProductos.DisplayLayout.Bands[0].Columns[0].Width = 100;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[1].Width = 450;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[2].Width = 720;

                //Ocultar columnas
                bandUno.Columns["ESTADO"].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ultraGrid2_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            e.Layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }
    }
}
