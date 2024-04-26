using His.Entidades;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using His.Parametros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace His.Dietetica
{
    public partial class frmCargaDietas : Form
    {
        private bool xSelection = true;
        public frmCargaDietas()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            grpDatos.Enabled = true;
            //LOAD GRID
            grid.DataSource = NegDietetica.getDataTable("getHabitaciones");
            var gridBand = grid.DisplayLayout.Bands[0];


            gridBand.Columns["HABITACION"].Width = 60;
            gridBand.Columns["IDENTIFICACION"].Hidden = true;
            gridBand.Columns["INGRESO"].Width = 70;
            gridBand.Columns["HC"].Width = 50;
            gridBand.Columns["ATENCION"].Width = 50;
            gridBand.Columns["PAC_GENERO"].Width = 30;
            gridBand.Columns["MEDICO"].Width = 150;
            gridBand.Columns["TRATAMIENTO"].Hidden = true;
            gridBand.Columns["MED_CODIGO"].Hidden = true;
            gridBand.Columns["CONVENIO"].Hidden = true;
            gridBand.Columns["ATE_DIAGNOSTICO_INICIAL"].Hidden = true;
            gridBand.Columns["PACIENTE"].Width = 200;
            gridBand.Columns["NACIMIENTO"].Width = 70;

            if (!gridBand.Columns.Exists("COD_PRO"))
            {
                gridBand.Columns.Add("COD_PRO", "COD_PRO");
                
            }
            if (!gridBand.Columns.Exists("PRODUCTO"))
            {
                gridBand.Columns.Add("PRODUCTO", "ALIMENTACION");
                gridBand.Columns["PRODUCTO"].Width = 200;
            }
            if (!gridBand.Columns.Exists("PED_OBSERVACION"))
            {
                gridBand.Columns.Add("PED_OBSERVACION", "OBSERVACION");
                
            }
            if (!gridBand.Columns.Exists("FECHA_PEDIDO"))
            {
                gridBand.Columns.Add("FECHA_PEDIDO", "FECHA PEDIDO");

            }
            ///all columns read only
            for (int i = 0; i < gridBand.Columns.Count; i++)
            {
                gridBand.Columns[i].CellActivation = Activation.NoEdit;
            }
            //i activate the check column
            gridBand.Columns["Seleccion"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            gridBand.Columns["Seleccion"].CellActivation = Activation.AllowEdit;
            gridBand.Columns["Seleccion"].Width = 70;
            gridBand.Columns["PED_OBSERVACION"].CellActivation = Activation.AllowEdit;


            if (!gridBand.Columns.Exists("PED_CODIGO"))
            {
                gridBand.Columns.Add("PED_CODIGO", "PEDIDO");
                gridBand.Columns["PED_CODIGO"].Width = 100;
                
                gridBand.Columns["PED_CODIGO"].CellActivation = Activation.NoEdit;
         }
            gridBand.Columns["COD_PRO"].Hidden = true;
            gridBand.Columns["PED_CODIGO"].Hidden = true;
            gridBand.Columns["FECHA_PEDIDO"].Hidden = true;
            //LOAD PRODUCTS COMBO 
            cmbTipo.DataSource = NegDietetica.getDataTable("getProductosAlimentacion");
            cmbTipo.ValueMember = "CODIGO";
            cmbTipo.DisplayMember = "NOMBRE";

            cmb_Areas.DataSource = NegPedidos.recuperarListaAreasTodas().OrderBy(a => a.PEA_NOMBRE).ToList();
            cmb_Areas.ValueMember = "PEA_CODIGO";
            cmb_Areas.DisplayMember = "PEA_NOMBRE";
            cmb_Areas.Text = "ALIMENTACION";
            //cmb_Areas.VAL = "3";

        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //// Check if the column exists, if not, add it
            //if (!gridBand.Columns.Exists("Select"))
            //    gridBand.Columns.Add("Select", "Select");

            //// Not needed, the ADD adds the Key and the Caption
            //// gridBand.Columns["Select"].Header.Caption = "Select"; 

            //// Now you can reference the column with the Key = "Select"
            //gridBand.Columns["Select"].Header.VisiblePosition = gridBand.Columns.Count + 1;
            //gridBand.Columns["Select"].Hidden = false;



            //gridBand.Columns["Select"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            //gridBand.Columns["Select"].AutoSizeMode = ColumnAutoSizeMode.AllRowsInBand;
            //gridBand.Columns["Select"].CellClickAction = CellClickAction.EditAndSelectText;
            //gridBand.Columns["Select"].DefaultCellValue = true;

            ////gridBand.Columns["Select"].ResetCellActivation();// = Activation.ActivateOnly;
            //gridBand.Columns["Select"].CellActivation = Activation.AllowEdit;

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            UltraGridBand band = this.grid.DisplayLayout.Bands[0];
            foreach (UltraGridRow row in band.GetRowEnumerator(GridRowType.DataRow))
            {
                row.Cells["Seleccion"].Value = xSelection;
            }
            xSelection = !xSelection;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[,] arrayColors = new int[,] {{255, 218, 185},{255, 245, 238},{230, 230, 250},{253, 245, 230},{211, 211, 211},{255, 250, 240},{255, 255, 240},{250, 235, 215},{250, 240, 230},{152, 251, 152},{255, 228, 225},
                                            {255, 248, 220},{192, 192, 192},{255, 228, 196},{255, 222, 173},{255, 255, 224},{176, 224, 230},{250, 250, 210},{255, 239, 213},{255, 228, 181},{238, 232, 170},{240, 230, 140},
                                            {144, 238, 144},{176, 196, 222},{173, 216, 230},{220, 220, 220}};
            UltraGridBand band = this.grid.DisplayLayout.Bands[0];
            foreach (UltraGridRow row in band.GetRowEnumerator(GridRowType.DataRow))
            {
                if (Convert.ToBoolean(row.Cells["Seleccion"].Value) )
                {
                    row.Cells["COD_PRO"].Value = cmbTipo.SelectedValue;
                    row.Cells["PRODUCTO"].Value = cmbTipo.Text;
                    if (cmbTipo.SelectedIndex<31)
                        row.Appearance.BackColor = Color.FromArgb(arrayColors[cmbTipo.SelectedIndex, 0], arrayColors[cmbTipo.SelectedIndex, 1], arrayColors[cmbTipo.SelectedIndex, 2]);
                    row.Cells["Seleccion"].Value = false;
                }
            }
            
        }

        private object[] generarPedido(int codPro, int ATE_CODIGO, int MED_CODIGO, string ped_observacion)
        {
                PRODUCTO Prod = NegProducto.RecuperarProductoID(codPro);// Creo un objeto con el item seleccionado / Giovanny Tapia / 04/10/2012
        ///////////////////////////////
        //pendiente verificar STOCK
        ///////////////////////////////
            PEDIDOS_DETALLE PedidosDetalleItem = new PEDIDOS_DETALLE();
                PedidosDetalleItem.PDD_CODIGO = 1;
                PedidosDetalleItem.PRODUCTOReference.EntityKey = Prod.EntityKey;
                PedidosDetalleItem.PDD_CANTIDAD = 1;
                PedidosDetalleItem.PRO_DESCRIPCION = Prod.PRO_DESCRIPCION;
                PedidosDetalleItem.PDD_VALOR = Prod.PRO_PRECIO;// /*Convert.ToDecimal(Item.Cells["VALOR"].Value)*/ Convert.ToDecimal(this.txtValorUnitario.Text);
                if (Prod.PRO_IVA > 0)
                    PedidosDetalleItem.PDD_IVA = Math.Round( Convert.ToDecimal((Prod.PRO_PRECIO * Prod.PRO_IVA) / 100) , 2);
                else
                    PedidosDetalleItem.PDD_IVA = 0;     
                PedidosDetalleItem.PDD_TOTAL = Math.Round((Convert.ToDecimal(Prod.PRO_PRECIO)), 2) + PedidosDetalleItem.PDD_IVA;
                PedidosDetalleItem.PDD_ESTADO = true;
                PedidosDetalleItem.PDD_COSTO = 0;
                PedidosDetalleItem.PDD_FACTURA = null;
                PedidosDetalleItem.PDD_ESTADO_FACTURA = 0;
                PedidosDetalleItem.PDD_FECHA_FACTURA = null;
                PedidosDetalleItem.PDD_RESULTADO = null;
                PedidosDetalleItem.PRO_CODIGO_BARRAS = Prod.PRO_CODIGO_BARRAS;
                List<PEDIDOS_DETALLE> PedidosDetalle = new List<PEDIDOS_DETALLE>();
                PedidosDetalle.Add(PedidosDetalleItem);
            List<PEDIDOS_DETALLE> listaProductosSolicitados = PedidosDetalle;
            //creando pedido
            ArchivoIni archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
            byte codigoEstacion = Convert.ToByte(archivo.IniReadValue("Pedidos", "estacion")); ///muestra
            PEDIDOS pedido = new PEDIDOS();
            pedido.PED_CODIGO = NegPedidos.ultimoCodigoPedidos() + 1;
            pedido.PED_FECHA = DateTime.Now; /*PARA GUARDAR EL PEDIDO SE NECESITA FECHA Y HORA/ GIOVANNY TAPIA / 12/04/2013*/
            pedido.PED_DESCRIPCION = ped_observacion;
            pedido.PED_ESTADO = 2;
            pedido.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
            pedido.ATE_CODIGO = ATE_CODIGO;
            pedido.PEE_CODIGO = codigoEstacion; /* CODIGO DE LA ESTACION DESDE DONDE SE REALIZA EL PEDIDO / GIOVANNY TAPIA / 04/01/2012 */
            pedido.TIP_PEDIDO = His.Parametros.FarmaciaPAR.PedidoMedicamentos;
            pedido.PED_PRIORIDAD = 1;
            pedido.MED_CODIGO = MED_CODIGO;
            pedido.PEDIDOS_AREASReference.EntityKey = ((PEDIDOS_AREAS)(cmb_Areas.SelectedItem)).EntityKey;
            pedido.PED_TRANSACCION = pedido.PED_CODIGO;
            //Int32 Pedido = pedido.PED_CODIGO;
            NegPedidos.crearPedido(pedido);
            foreach (var solicitud in listaProductosSolicitados)
            {   
                solicitud.PEDIDOSReference.EntityKey = pedido.EntityKey;
                solicitud.PDD_CODIGO = NegPedidos.ultimoCodigoPedidosDetalles() + 1;
                Int32 codpro = Convert.ToInt32(solicitud.PRO_CODIGO_BARRAS.ToString());

                Int32 xcodDiv = 0;
                Int16 XRubro = 0;
                DataTable auxDT = NegFactura.recuperaCodRubro(codPro);
                foreach (DataRow row in auxDT.Rows)
                {
                    XRubro = Convert.ToInt16(row[0].ToString());
                    xcodDiv = Convert.ToInt32(row[1].ToString());
                }
                NegPedidos.crearDetallePedido(solicitud, pedido, XRubro, xcodDiv, "Pedido dietetica");
            }




            object[] r = new object[] { (pedido.PED_CODIGO), DateTime.Now };


            return r;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Desea generar el pedido realizado?", "His3000", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {

                UltraGridBand band = this.grid.DisplayLayout.Bands[0];

                band.Columns["COD_PRO"].Hidden = false;
                band.Columns["PED_CODIGO"].Hidden = false;
                band.Columns["FECHA_PEDIDO"].Hidden = false;

                foreach (UltraGridRow row in band.GetRowEnumerator(GridRowType.DataRow))
                {
                    if (Convert.ToString(row.Cells["COD_PRO"].Value).Trim() != string.Empty)
                    {
                        //row.Cells["PED_CODIGO"].Value = generarPedido(Convert.ToInt32(row.Cells["COD_PRO"].Value), Convert.ToInt32(row.Cells["ATENCION"].Value), Convert.ToInt32(row.Cells["MED_CODIGO"].Value));
                        object[] r= generarPedido(Convert.ToInt32(row.Cells["COD_PRO"].Value), Convert.ToInt32(row.Cells["ATENCION"].Value), Convert.ToInt32(row.Cells["MED_CODIGO"].Value), Convert.ToString(row.Cells["PED_OBSERVACION"].Value));
                        row.Cells["PED_CODIGO"].Value = r[0].ToString();
                        row.Cells["FECHA_PEDIDO"].Value = r[1].ToString();
                    }
                }
                grpDatos.Enabled = false;
                band.Columns["PED_OBSERVACION"].CellActivation = Activation.NoEdit;
                band.Columns["Seleccion"].CellActivation = Activation.NoEdit;
            }

            

        }

        private void toolStripButtonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    this.ultraGridExcelExporter1.Export(grid, PathExcel);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
        }


        private String FindSavePath()
        {
            Stream myStream;
            string myFilepath = null;
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xls)|*.xls";
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

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            InitializeControls();
        }

        private void grid_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {

                grid.ActiveRow.Cells["COD_PRO"].Value="";
                grid.ActiveRow.Cells["PRODUCTO"].Value="";

            }
        }

        private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (grid.Rows.Count > 0)
            {
                try
                {
                    frmAuxViewer Form = new frmAuxViewer(Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["ATENCION"].Value.ToString()));
                    Form.ShowDialog();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
