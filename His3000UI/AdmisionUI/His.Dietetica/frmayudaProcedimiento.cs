using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;

namespace His.Dietetica
{
    public partial class frmayudaProcedimiento : Form
    {
        NegQuirofano Quirofano = new NegQuirofano();
        internal static bool pedidopaciente = false;
        internal static string ate_codigo; //Variable que contiene el codigo de atencion del paciente
        internal static string pac_codigo; //Variable que contiene el codigo del paciente
        private static string cie_codigo; //variable privada que contiene el codigo del procedimiento para poder determinar si existe dentro del paciente

        public string pci_codigo;
        public int bodega = Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()); //12 bodega de quirofano por defecto
        public frmayudaProcedimiento()
        {
            InitializeComponent();
        }

        public frmayudaProcedimiento(int bodega)
        {
            InitializeComponent();
            this.bodega = bodega;
        }
        public void RedimencionarTabla()
        {
            try
            {
                UltraGridBand bandUno = TablaProcedimiento.DisplayLayout.Bands[0];

                TablaProcedimiento.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                TablaProcedimiento.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                TablaProcedimiento.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                TablaProcedimiento.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                TablaProcedimiento.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                TablaProcedimiento.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                TablaProcedimiento.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                TablaProcedimiento.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                TablaProcedimiento.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                TablaProcedimiento.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                TablaProcedimiento.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                TablaProcedimiento.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                TablaProcedimiento.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                TablaProcedimiento.DisplayLayout.Bands[0].Columns[0].Width = 60;
                //TablaProcedimiento.DisplayLayout.Bands[0].Columns[1].Width = 100;


                ////Ocultar columnas, que son fundamentales al levantar informacion
                //TablaProcedimiento.DisplayLayout.Bands[0].Columns[2].Hidden = true;
                //TablaProcedimiento.DisplayLayout.Bands[0].Columns[3].Hidden = true;
                //TablaProductos.DisplayLayout.Bands[0].Columns[13].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmayudaProcedimiento_Load(object sender, EventArgs e)
        {
            if(pedidopaciente == false)
            {
                CargarTablaProcedimientos();
            }
            else
            {
                CargarSoloProcedimientos();
            }
        }
        public void CargarTablaProcedimientos()
        {
            TablaProcedimiento.DataSource = Quirofano.MostrarProcedimientos(bodega);
            RedimencionarTabla();
        }
        public void CargarSoloProcedimientos()
        {
            TablaProcedimiento.DataSource = Quirofano.SoloProcedimientos(bodega);
            RedimencionarTabla();
        }
        private void TablaProcedimiento_DoubleClick(object sender, EventArgs e)
        {
            string existe;
            string procedimiento = "";
            if(TablaProcedimiento.Selected.Rows.Count > 0)
            {
                UltraGridRow Fila = TablaProcedimiento.ActiveRow; //eligue el numero de fila que esta seleccionada
                if (pedidopaciente == false)
                {
                    frmQuirofanoAgregarProcedimiento.cie_codigo = Fila.Cells[0].Value.ToString();
                    frmQuirofanoAgregarProcedimiento.cie_descripcion = Fila.Cells[1].Value.ToString();
                    this.Close();
                }
                else
                {
                    ATENCIONES ultimaatencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
                    PACIENTES datosPaciente = new PACIENTES();
                    datosPaciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));
                    cie_codigo = Fila.Cells[0].Value.ToString();
                    procedimiento = Fila.Cells[1].Value.ToString();
                    existe = Quirofano.ExisteProcedimiento(ate_codigo, datosPaciente.PAC_CODIGO.ToString(), Fila.Cells[0].Value.ToString());
                    if(existe == "")
                    {
                        //Regresa valores del Cie10 al siguiente formulario
                        //frmQuirofanoExplorador.cie_codigo = Fila.Cells[0].Value.ToString();
                        //frmQuirofanoExplorador.cie_descripcion = Fila.Cells[1].Value.ToString();

                        //Envia valores del cie10 al siguiente formulario
                        frmQuirofanoRegistro.cie_codigo = Fila.Cells[0].Value.ToString();
                        frmQuirofanoRegistro.cie_descripcion = Fila.Cells[1].Value.ToString();
                        frmQuirofanoRegistro.ate_codigo = ate_codigo;
                        this.Close();

                        frmQuirofanoRegistro x = new frmQuirofanoRegistro(bodega);
                        x.Show();
                    }
                    else
                    {
                        //MessageBox.Show("Este Procedimiento ya ha sido Agregado!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        if (MessageBox.Show("¿Desea crear otro procedimiento de: " + procedimiento + " ?", 
                            "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                int num = 0;
                                DataTable Varios = new DataTable();
                                Varios = NegQuirofano.ProcedimientosVarios(Convert.ToInt64(ate_codigo), datosPaciente.PAC_CODIGO, procedimiento);
                                if(Varios != null || Varios.Rows.Count > 0)
                                {
                                    foreach (DataRow item in Varios.Rows)
                                    {
                                        num++;
                                        existe = Quirofano.ExisteProcedimiento(ate_codigo, datosPaciente.PAC_CODIGO.ToString(), item[0].ToString());
                                        if (existe == "")
                                            break;
                                    }
                                    if(existe != "")
                                    {
                                        DataTable Proce = new DataTable();
                                        Proce = NegQuirofano.NProcedimientos(procedimiento + " " + num.ToString(),bodega);
                                        DataTable producto = new DataTable();
                                        producto = NegQuirofano.ProductosProcedimeinto(Convert.ToInt64(Fila.Cells[0].Value.ToString()));
                                        foreach (DataRow dr in producto.Rows)
                                        {
                                            NegQuirofano.CargaPedido("1",dr["CODPRO"].ToString(),Proce.Rows[0][0].ToString(),dr["QPP_CANTIDAD"].ToString());
                                        }
                                        if(Proce.Rows.Count > 0)
                                        {
                                            //Envia valores del cie10 al siguiente formulario
                                            frmQuirofanoRegistro.cie_codigo = Proce.Rows[0][0].ToString();
                                            frmQuirofanoRegistro.cie_descripcion = Proce.Rows[0][1].ToString();
                                            frmQuirofanoRegistro.ate_codigo = ate_codigo;
                                            this.Close();

                                            frmQuirofanoRegistro x = new frmQuirofanoRegistro(bodega);
                                            x.Show();
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se creo el procedimiento " + procedimiento + " " + num.ToString(), "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Algo ocurrio al verificar los procedimientos de: " + procedimiento + ".\r\nMas detalle: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }
    }
}
