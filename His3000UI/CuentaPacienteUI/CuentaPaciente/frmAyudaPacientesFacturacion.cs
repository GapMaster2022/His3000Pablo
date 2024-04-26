using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Negocio;
using His.Parametros;

namespace CuentaPaciente
{
    public partial class frmAyudaPacientesFacturacion : Form
    {
        #region Variables
        frmDetalleCuenta detalleC = new frmDetalleCuenta();
        List<DtoPedidos> consultaPacientes = new List<DtoPedidos>();
        public TextBox campoCodigo = new TextBox();
        public TextBox campoAseguradora = new TextBox();
        public TextBox campoPadre = new TextBox();
        public TextBox campoAtencion = new TextBox();
        public TextBox campoNombre = new TextBox();
        public TextBox campoAtencionNumero = new TextBox();
        public string columnabuscada = "HCL";
        public string columnaNombre = "NOMBRE";
        public string columnaAtencion = "ATENCION";
        public string columnaAseguradora = "ASEGURADORA";
        public string columnaCodigo = "CODIGO";
        public string columnaFecha = "F_INGRESO";
        public string columnaAtencionNumero = "ATE_NUMERO";
        public bool inicio = true;
        public int agrupacion = 0;
        public int primeraCuenta = 0;


        #endregion

        #region Constructor
        //public frm_AyudaPacientes(List<PACIENTES> pacientes)
        //{
        public bool mushuñan = false;
        public frmAyudaPacientesFacturacion()
        {
            InitializeComponent();
            cb_numFilas.SelectedIndex = 0;
            cb_numFilas.Text = "100";
            Buscar();
            cb_numFilas.Text = "1000";
        }

        public frmAyudaPacientesFacturacion(Boolean aux) //para llamarse desde auditoria
        {
            InitializeComponent();
            cb_numFilas.SelectedIndex = 0;
            cb_numFilas.Text = "100";
            Buscar();
            cb_numFilas.Text = "1000";
        }

        private void Buscar()
        {
            try
            {
                NegFactura.VerifcaCuentasFacturadas();
                string id = txt_busqCi.Text.ToString();
                string historia = txt_busqHist.Text.ToString();
                string nombre = txt_busqNom.Text.ToString();
                string factura = ultraTextEditor1.Text.ToString();
                int cantidad = 100;

                //valido el usuario de mushuñan
                List<PERFILES> perfilUsuario = new NegPerfil().RecuperarPerfil(His.Entidades.Clases.Sesion.codUsuario);

                foreach (var item in perfilUsuario)
                {
                    List<ACCESO_OPCIONES> accop = NegUtilitarios.ListaAccesoOpcionesPorPerfil(item.ID_PERFIL, 7);
                    foreach (var items in accop)
                    {
                        if (items.ID_ACCESO == 71110)// se cambia del perfil  29 a opcion 71110// Mario Valencia 14/11/2023 // cambio en seguridades.
                        {
                            mushuñan = true;
                        }
                    }
                    //if (item.ID_PERFIL == 29)
                    //{
                    //    if (item.DESCRIPCION.Contains("SUCURSALES")) //se debe tomar en cuenta que si es 29 en otra empresa no actuara de la forma como en la pasteur.
                    //        mushuñan = true;
                    //}
                }

                if (cb_numFilas.SelectedItem != null)
                {
                    cantidad = Convert.ToInt32(cb_numFilas.SelectedItem);
                }
                if (!mushuñan)
                    consultaPacientes = NegPedidos.recuperarListaPacientesFacturacion(id, historia, nombre, cantidad, 1, factura);
                else
                {
                    Int16 AreaUsuario = 1;
                    DataTable codigoAreaAsignada = NegUsuarios.AreaAsignada(Convert.ToInt16(His.Entidades.Clases.Sesion.codUsuario));
                    bool parse = Int16.TryParse(codigoAreaAsignada.Rows[0][0].ToString(), out AreaUsuario);
                    if (parse)
                    {
                        switch (AreaUsuario)
                        {
                            case 2:
                                consultaPacientes = NegPedidos.recuperarListaPacientesFacturacionMushuñan(id, historia, nombre, cantidad, 1, factura);
                                break;
                            case 3:
                                consultaPacientes = NegPedidos.recuperarListaPacientesFacturacionBrigada(id, historia, nombre, cantidad, 1, factura);
                                break;
                            default:
                                break;
                        }
                    }
                }


                grid.DataSource = consultaPacientes.Select(
                    p => new
                    {
                        F_ATENCION = p.PED_FECHA,
                        HCL = p.HISTORIA_CLINICA,
                        ATENCION = p.ATE_CODIGO,
                        HABITACION = p.HABITACION,
                        NOMBRE = p.PACIENTE,
                        ID = p.IDENTIFICACION,
                        ASEGURADORA = p.PRO_DESCRIPCION,
                        FACTURAS = p.FACTURA,
                        ATE_NUMERO = p.ATE_NUMERO, // Tomo el numero de atencion /30/10/2012 / GIOVANNY TAPIA                        
                        F_ALTA = p.PED_FECHA_ALTA,
                        CODIGO = p.CODIGO,
                        ESTADO = p.ESC_CODIGO
                    }

                    ).Where(p => p.ESTADO == 2 || p.ESTADO == 1).OrderByDescending(p => p.F_ATENCION).Distinct().ToList();

                grid.DisplayLayout.Bands[0].Columns["F_ATENCION"].Width = 200;
                grid.DisplayLayout.Bands[0].Columns["HCL"].Width = 150;
                grid.DisplayLayout.Bands[0].Columns["ATENCION"].Width = 100;
                grid.DisplayLayout.Bands[0].Columns["HABITACION"].Width = 150;
                grid.DisplayLayout.Bands[0].Columns["NOMBRE"].Width = 500;
                grid.DisplayLayout.Bands[0].Columns["ID"].Width = 200;
                grid.DisplayLayout.Bands[0].Columns["ASEGURADORA"].Width = 450;
                grid.DisplayLayout.Bands[0].Columns["FACTURAS"].Width = 500;
                grid.DisplayLayout.Bands[0].Columns["ATE_NUMERO"].Width = 150;
                grid.DisplayLayout.Bands[0].Columns["F_ALTA"].Width = 180;
                grid.DisplayLayout.Bands[0].Columns["CODIGO"].Hidden = true;
                grid.DisplayLayout.Bands[0].Columns["ESTADO"].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void BuscarFacturadas()
        {
            try
            {
                string id = txt_busqCi.Text.ToString();
                string historia = txt_busqHist.Text.ToString();
                string nombre = txt_busqNom.Text.ToString();
                string factura = ultraTextEditor1.Text.ToString();
                int cantidad = 100;

                if (cb_numFilas.SelectedItem != null)
                {
                    cantidad = Convert.ToInt32(cb_numFilas.SelectedItem);
                }

                consultaPacientes = NegPedidos.recuperarListaPacientesFacturacion(id, historia, nombre, cantidad, 1, factura);

                grid.DataSource = consultaPacientes.Select(
                    p => new
                    {
                        F_ATENCION = p.PED_FECHA,
                        HCL = p.HISTORIA_CLINICA,
                        ATENCION = p.ATE_CODIGO,
                        HABITACION = p.HABITACION,
                        NOMBRE = p.PACIENTE,
                        ID = p.IDENTIFICACION,
                        ASEGURADORA = p.PRO_DESCRIPCION,
                        ATE_NUMERO = p.ATE_NUMERO, // Tomo el numero de atencion /30/10/2012 / GIOVANNY TAPIA                        
                        F_ALTA = p.PED_FECHA_ALTA,
                        FACTURAS = p.FACTURA,
                        CODIGO = p.CODIGO,
                        ESTADO = p.ESC_CODIGO,
                        F_FACTURA = p.ATE_FECHA_FACTURA
                    }
                    ).Where(p => p.ESTADO == 6).OrderByDescending(p => p.FACTURAS).Distinct().ToList();

                grid.DisplayLayout.Bands[0].Columns["F_ATENCION"].Width = 200;
                grid.DisplayLayout.Bands[0].Columns["HCL"].Width = 150;
                grid.DisplayLayout.Bands[0].Columns["ATENCION"].Width = 100;
                grid.DisplayLayout.Bands[0].Columns["HABITACION"].Width = 150;
                grid.DisplayLayout.Bands[0].Columns["NOMBRE"].Width = 700;
                grid.DisplayLayout.Bands[0].Columns["ID"].Width = 200;
                grid.DisplayLayout.Bands[0].Columns["ASEGURADORA"].Width = 450;
                grid.DisplayLayout.Bands[0].Columns["ATE_NUMERO"].Width = 150;
                grid.DisplayLayout.Bands[0].Columns["F_ALTA"].Width = 150;
                grid.DisplayLayout.Bands[0].Columns["FACTURAS"].Width = 300;
                grid.DisplayLayout.Bands[0].Columns["CODIGO"].Hidden = true;
                grid.DisplayLayout.Bands[0].Columns["ESTADO"].Hidden = true;
                grid.DisplayLayout.Bands[0].Columns["F_FACTURA"].Width = 150;

                //grid.DisplayLayout.Bands[0].Columns["NOMBRE"].Width = 350;
                //grid.DisplayLayout.Bands[0].Columns["ASEGURADORA"].Width = 300;
                //grid.DisplayLayout.Bands[0].Columns["ATENCION"].Hidden = true;// Oculto el codigo de atencion /30/10/2012 / GIOVANNY TAPIA
                //grid.DisplayLayout.Bands[0].Columns["ESTADO"].Hidden = true;// Oculto el estado de la factura /22/05/2013 / GIOVANNY TAPIA

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void BuscaPreFacturadas()
        {
            try
            {
                string id = txt_busqCi.Text.ToString();
                string historia = txt_busqHist.Text.ToString();
                string nombre = txt_busqNom.Text.ToString();
                string factura = ultraTextEditor1.Text.ToString();
                int cantidad = 100;

                if (cb_numFilas.SelectedItem != null)
                {
                    cantidad = Convert.ToInt32(cb_numFilas.SelectedItem);
                }
                consultaPacientes = NegPedidos.recuperarListaPacientesFacturacion(id, historia, nombre, cantidad, 1, factura);

                grid.DataSource = consultaPacientes.Select(
                    p => new
                    {
                        F_ATENCION = p.PED_FECHA,
                        HCL = p.HISTORIA_CLINICA,
                        ATENCION = p.ATE_CODIGO,
                        HABITACION = p.HABITACION,
                        NOMBRE = p.PACIENTE,
                        ID = p.IDENTIFICACION,
                        ASEGURADORA = p.PRO_DESCRIPCION,
                        ATE_NUMERO = p.ATE_NUMERO, // Tomo el numero de atencion /30/10/2012 / GIOVANNY TAPIA                        
                        F_ALTA = p.PED_FECHA_ALTA,
                        FACTURAS = p.FACTURA,
                        CODIGO = p.CODIGO,
                        ESTADO = p.ESC_CODIGO
                    }
                    ).Where(p => p.ESTADO == 7 || p.ESTADO == 3).OrderByDescending(p => p.FACTURAS).Distinct().ToList();

                grid.DisplayLayout.Bands[0].Columns["F_ATENCION"].Width = 200;
                grid.DisplayLayout.Bands[0].Columns["HCL"].Width = 150;
                grid.DisplayLayout.Bands[0].Columns["ATENCION"].Width = 80;
                grid.DisplayLayout.Bands[0].Columns["HABITACION"].Width = 150;
                grid.DisplayLayout.Bands[0].Columns["NOMBRE"].Width = 600;
                grid.DisplayLayout.Bands[0].Columns["ID"].Width = 200;
                grid.DisplayLayout.Bands[0].Columns["ASEGURADORA"].Width = 450;
                grid.DisplayLayout.Bands[0].Columns["ATE_NUMERO"].Width = 200;
                grid.DisplayLayout.Bands[0].Columns["F_ALTA"].Width = 200;
                grid.DisplayLayout.Bands[0].Columns["FACTURAS"].Width = 300;
                grid.DisplayLayout.Bands[0].Columns["CODIGO"].Hidden = true;
                grid.DisplayLayout.Bands[0].Columns["ESTADO"].Hidden = true;

                //grid.DisplayLayout.Bands[0].Columns["NOMBRE"].Width = 350;
                //grid.DisplayLayout.Bands[0].Columns["ASEGURADORA"].Width = 300;
                //grid.DisplayLayout.Bands[0].Columns["ATENCION"].Hidden = true;// Oculto el codigo de atencion /30/10/2012 / GIOVANNY TAPIA
                //grid.DisplayLayout.Bands[0].Columns["ESTADO"].Hidden = true;// Oculto el estado de la factura /22/05/2013 / GIOVANNY TAPIA

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }

        #endregion

        #region Metodos Privados

        private void cb_numFilas_SelectedValueChanged(object sender, EventArgs e)
        {
            if (inicio == false)
                Buscar();
        }

        private void txt_busqNom_AfterExitEditMode(object sender, EventArgs e)
        {

        }

        private void txt_busqHist_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                if (cmbTipoFactura.SelectedItem.ToString() == "POR FACTURAR")
                {
                    Buscar();
                }
                else if (cmbTipoFactura.SelectedItem.ToString() == "FACTURADA")
                {
                    BuscarFacturadas();
                }
                else if (cmbTipoFactura.SelectedItem.ToString() == "PRE-FACTURADA")
                {
                    BuscaPreFacturadas();
                }
            }
        }

        private void txt_busqNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                if (cmbTipoFactura.SelectedItem.ToString() == "POR FACTURAR")
                {
                    Buscar();
                }
                else if (cmbTipoFactura.SelectedItem.ToString() == "FACTURADA")
                {
                    BuscarFacturadas();
                }
                else if (cmbTipoFactura.SelectedItem.ToString() == "PRE-FACTURADA")
                {
                    BuscaPreFacturadas();
                }
            }
        }

        private void txt_busqCi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                if (cmbTipoFactura.SelectedItem.ToString() == "POR FACTURAR")
                {
                    Buscar();
                }
                else if (cmbTipoFactura.SelectedItem.ToString() == "FACTURADA")
                {
                    BuscarFacturadas();
                }
                else if (cmbTipoFactura.SelectedItem.ToString() == "PRE-FACTURADA")
                {
                    BuscaPreFacturadas();
                }
            }
        }

        private void grid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void grid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void enviarCodigo()
        {
            try
            {
                enviaCodigo();
                this.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void grid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (cmbTipoFactura.SelectedItem.ToString() == "POR FACTURAR")
            {
                Buscar();
            }
            else if (cmbTipoFactura.SelectedItem.ToString() == "FACTURADA")
            {
                BuscarFacturadas();
            }
            else if (cmbTipoFactura.SelectedItem.ToString() == "PRE-FACTURADA")
            {
                BuscaPreFacturadas();
            }
        }

        private void ultraGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    enviarCodigo();
                }
                else if (e.KeyCode == Keys.End)
                {
                    grid.ActiveCell = grid.Rows[grid.Rows.Count - 1].Cells[grid.ActiveCell.Column.Index];
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Home)
                {
                    grid.ActiveCell = grid.Rows[0].Cells[grid.ActiveCell.Column.Index];
                    e.Handled = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGrid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            enviarCodigo();
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            grid.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
            grid.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;
        }

        #endregion

        private void frmAyudaPacientesFacturacion_Load(object sender, EventArgs e)
        {
            cmbTipoFactura.SelectedIndex = 0;
        }

        private void grid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            Int64 CodigoEstado = 0;
            CodigoEstado = Convert.ToInt64(e.Row.Cells["ESTADO"].Value);
            COPAGO copago = NegCopago.recuperaCopago(Convert.ToInt64(e.Row.Cells["ATENCION"].Value));
            if (copago != null)
            {
                e.Row.Appearance.BackColor = ObtenerNaranjaPastel();
            }
            else if (CodigoEstado == 6)
            {
                e.Row.Appearance.BackColor = Color.LightCoral;
            }
            else if (CodigoEstado == 2)
            {
                e.Row.Appearance.BackColor = Color.LightBlue;
            }
            else if (CodigoEstado == 1)
            {
                e.Row.Appearance.BackColor = Color.LightGoldenrodYellow;
            }
            else if (CodigoEstado == 7)
            {
                e.Row.Appearance.BackColor = Color.LightSalmon;
            }

        }

        private void ultraTextEditor1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    SendKeys.SendWait("{TAB}");
                    Buscar();
                }
            }
            else
            {
                //el resto de teclas pulsadas se desactivan 
                e.Handled = true;
            }
        }

        private void ultraTextEditor1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void ultraTextEditor1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnAgrupar_Click(object sender, EventArgs e)
        {
            btnActualizar.Visible = false;
            btnAgrupar.Visible = false;
            btnTermina.Visible = true;
            Buscar();
            cmbTipoFactura.Text = "POR FACTURAR";
            if (MessageBox.Show("Usted Va Realizar Agrupación De Cuentas. ESTE PROCESO NO SE PUEDE REVERTIR\r\n ¿Desea Continuar?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                agrupacion = 1;
                primeraCuenta = 1;
                MessageBox.Show("CON DOBLE CLIC.\r\nPRIMERO ESCOJA CUENTA PRINCIPAL.\r\nSEGUIDO TODAS LAS CUENTAS QUE VA AGRUPAR", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnTermina_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta Cerrando Proceso De Agrupación No Va Poder Añadir Más Cuentas\r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
        }
        public string FacturaDato = "";
        private void enviaCodigo()
        {
            campoPadre.Tag = true;
            campoAtencion.Text = grid.ActiveRow.Cells[columnaAtencion].Value.ToString();
            campoAtencionNumero.Text = grid.ActiveRow.Cells[columnaAtencionNumero].Value.ToString();
            campoNombre.Text = grid.ActiveRow.Cells[columnaNombre].Value.ToString();
            detalleC.codigoAtencionA = campoAtencion.Text;
            campoPadre.Text = grid.ActiveRow.Cells[columnabuscada].Value.ToString();
            campoCodigo.Text = grid.ActiveRow.Cells[columnaCodigo].Value.ToString();
            campoAseguradora.Text = grid.ActiveRow.Cells[columnaAseguradora].Value.ToString();
            if (cmbTipoFactura.SelectedItem.ToString() == "FACTURADA")
            {
                FacturaDato = grid.ActiveRow.Cells["FACTURAS"].Value.ToString();
            }
        }
        private Color ObtenerNaranjaPastel()
        {
            // Puedes ajustar estos valores para obtener el tono deseado
            int rojo = 255;
            int verde = 187;
            int azul = 108;

            return Color.FromArgb(rojo, verde, azul);
        }
    }
}
