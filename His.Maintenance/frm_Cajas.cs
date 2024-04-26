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
using Core.Datos;
using Core.Entidades;

namespace His.Maintenance
{
    public partial class frm_Cajas : Form
    {
        #region Varibles
        public DtoCajas cajaOriginal = new DtoCajas();
        public DtoCajas cajaModificada = new DtoCajas();
        public CAJAS cajOrigen = new CAJAS();
        public CAJAS cajModificada = new CAJAS();
        public List<LOCALES> locales = new List<LOCALES>();
        public List<DtoCajas> cajas = new List<DtoCajas>();
        public int columnabuscada;
        private Boolean cargadatoscombo;
        private Boolean estadoGrid = false;
        #endregion

        #region Constructor
        public frm_Cajas()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frm_Cajas_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(false, false, false, false, false, true, true,true);
                //HalitarControles(false, false, false, false, true, true, false, true);
                cargadatoscombo = false;
                RecuperarCajas();
                RecuperarTiposDocumento();
                // carga combo PAISES
                locales = NegLocales.ListaLocales();
                cmb_locales.DataSource = locales;
                cmb_locales.ValueMember = "LOC_CODIGO";
                cmb_locales.DisplayMember = "LOC_NOMBRE";
                txt_autorizacion.MaxLength = 10;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                GrabarDatos();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                GrabarDatos();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void grid_ciudades_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false, true,true);
                cajaModificada = grid_ciudades.CurrentRow.DataBoundItem as DtoCajas;
                cajaOriginal = cajaModificada.ClonarEntidad();
                AgregarBindigControles();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Desea eliminar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    cajModificada.CAJ_CODIGO = cajaModificada.CAJ_CODIGO;
                    LOCALES locModificada = cmb_locales.SelectedItem as LOCALES;
                    cajModificada.LOCALESReference.EntityKey = locModificada.EntityKey;
                    cajModificada.CAJ_ESTADO = cajaModificada.CAJ_ESTADO;
                    cajModificada.CAJ_AUTORIZACION_SRI = cajaModificada.CAJ_AUTORIZACION_SRI;
                    cajModificada.CAJ_FECHA = cajaModificada.CAJ_FECHA;
                    cajModificada.CAJ_NOMBRE = cajaModificada.CAJ_NOMBRE;
                    cajModificada.CAJ_NUMERO = cajaModificada.CAJ_NUMERO;
                    cajModificada.CAJ_PERIDO_VALIDEZ = cajaModificada.CAJ_PERIDO_VALIDEZ;
                    cajModificada.CAJ_FECHA = cajaModificada.CAJ_FECHA;
                    cajModificada.EntityKey = new EntityKey(cajas.First().ENTITYSETNAME
                        , cajas.First().ENTITYID, cajaModificada.CAJ_CODIGO);

                    NegCajas.EliminarCaja(cajModificada.ClonarEntidad()); //.ClonarEntidad()
                    RecuperarCajas();
                    ResetearControles();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true, false,true);
                    MessageBox.Show("Datos Eliminados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Operación Invalida", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HalitarControles(true, true, false, true, false, false, true,true);
                cajaOriginal = new DtoCajas();
                cajaModificada = new DtoCajas();

                ResetearControles();
                cajaModificada.CAJ_CODIGO = Int16.Parse((NegCajas.RecuperaMaximoCaja() + 1).ToString());
                cajaModificada.CAJ_FECHA = DateTime.Now;
                AgregarBindigControles();
                cargadatoscombo = true;
                cmb_locales.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearControles();
            RecuperarCajas();
            RecuperarTiposDocumento();
            HalitarControles(false, false, false, false, false, true, false,true);
        }
        private void cmb_locales_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txt_descripcion.Focus();
            }
        }
        private void txt_descripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txt_numcaja.Focus();
            }
        }
        private void txt_numcaja_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
        private void txt_autorizacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(Keys.Enter))
                {
                    e.Handled = true;
                    dtp_ValidezAutoriz.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_ValidezAutoriz_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }
        private void cmb_locales_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cargadatoscombo == true)
            //{
            //    cajaModificada.CAJ_NOMBRE = cmb_locales.Text;
            //    AgregarBindigControles();
            //}
        }
        private void txt_ValidezAutoriz_Leave(object sender, EventArgs e)
        {
            
        }
        private void grid_ciudades_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = grid_ciudades.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                cajas.Sort(new CajaComparer(strColumnName, strSortOrder));
                grid_ciudades.DataSource = null;
                grid_ciudades.DataSource = cajas;
                grid_ciudades.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                grid_ciudades.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                grid_ciudades.Columns["CAJ_CODIGO"].HeaderText = "CODIGO";
                grid_ciudades.Columns["CAJ_NOMBRE"].HeaderText = "NOMBRE";
                grid_ciudades.Columns["CAJ_NUMERO"].HeaderText = "No CAJA";
                //Desahbilitamos las demas columnas
                grid_ciudades.Columns["LOC_CODIGO"].Visible = false;
                grid_ciudades.Columns["CAJ_ESTADO"].Visible = false;
                grid_ciudades.Columns["CAJ_AUTORIZACION_SRI"].Visible = false;
                grid_ciudades.Columns["CAJ_FECHA"].Visible = false;
                grid_ciudades.Columns["CAJ_PERIDO_VALIDEZ"].Visible = false;
                grid_ciudades.Columns["ENTITYSETNAME"].Visible = false;
                grid_ciudades.Columns["ENTITYID"].Visible = false;
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(grid_ciudades, new Point(MousePosition.X - e.X - grid_ciudades.Columns[e.ColumnIndex].Width - grid_ciudades.Width, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Caja", grid_ciudades.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= grid_ciudades.RowCount - 1; i++)
                    {
                        if (grid_ciudades.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            grid_ciudades.CurrentCell = grid_ciudades.Rows[i].Cells[columnabuscada];
                        // gridMedicos.Rows[i].Cells[columnabuscada].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        #endregion

        #region Metodos Privados
        private void GrabarDatos()
        {
            DialogResult resultado;
            grid_ciudades.Focus();
            cajModificada = new CAJAS();
            cajOrigen = new CAJAS();
            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    if(validar_grid()==1) 
                {

                        
                    cajModificada.CAJ_CODIGO = cajaModificada.CAJ_CODIGO;
                    LOCALES locModificada = cmb_locales.SelectedItem as LOCALES;
                    cajModificada.LOCALESReference.EntityKey = locModificada.EntityKey;
                    cajModificada.CAJ_ESTADO = cajaModificada.CAJ_ESTADO;
                    cajModificada.CAJ_AUTORIZACION_SRI = cajaModificada.CAJ_AUTORIZACION_SRI;
                    cajModificada.CAJ_FECHA = cajaModificada.CAJ_FECHA;
                    cajModificada.CAJ_NOMBRE = cajaModificada.CAJ_NOMBRE;
                    cajModificada.CAJ_NUMERO = txt_numcaja.Text.Replace("-", string.Empty).ToString();
                    cajModificada.CAJ_PERIDO_VALIDEZ = cajaModificada.CAJ_PERIDO_VALIDEZ;
                    cajModificada.CAJ_FECHA = cajaModificada.CAJ_FECHA;

                    if (cajaOriginal.CAJ_CODIGO == 0)
                    {
                        NegCajas.CrearCaja(cajModificada);
                    }
                    else
                    {
                        cajModificada.EntityKey = new EntityKey(cajas.First().ENTITYSETNAME
                        , cajas.First().ENTITYID, cajaModificada.CAJ_CODIGO);


                        cajOrigen.CAJ_CODIGO = cajaOriginal.CAJ_CODIGO;
                        LOCALES locOriginal = locales.Where(p => p.LOC_CODIGO == cajaOriginal.LOC_CODIGO).FirstOrDefault();
                        cajOrigen.LOCALESReference.EntityKey = locOriginal.EntityKey;
                        cajOrigen.CAJ_ESTADO = cajaOriginal.CAJ_ESTADO;
                        cajOrigen.CAJ_AUTORIZACION_SRI = cajaOriginal.CAJ_AUTORIZACION_SRI;
                        cajOrigen.CAJ_FECHA = cajaOriginal.CAJ_FECHA;
                        cajOrigen.CAJ_NOMBRE = cajaOriginal.CAJ_NOMBRE;
                        cajOrigen.CAJ_NUMERO = cajaOriginal.CAJ_NUMERO;
                        cajOrigen.CAJ_PERIDO_VALIDEZ = cajaOriginal.CAJ_PERIDO_VALIDEZ;
                        cajOrigen.CAJ_FECHA = cajaOriginal.CAJ_FECHA;
                        cajOrigen.EntityKey = new EntityKey(cajas.First().ENTITYSETNAME
                          , cajas.First().ENTITYID, cajaOriginal.CAJ_CODIGO);
                        
                        NegCajas.GrabarCaja(cajModificada, cajOrigen);
                    }            
                    GrabarNumerosControlCajas();
                    RecuperarCajas();
                    RecuperarTiposDocumento();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true, false,true);
                    MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }  
              
                    else
                    {
                        MessageBox.Show("Rango de facturas Invalidas", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                    }

                }
            }
        }
        /// <summary>
        /// fuencion para habilitar los botones
        /// </summary>
        /// <param name="control">boton que se acciona</param>
        protected virtual void HalitarControles(bool datosPrincipales, bool datosSecundarios, bool Modificar, bool Grabar, bool Eliminar, bool Nuevo, bool Cancelar, bool Cerrar)
        {
            btnNuevo.Enabled = Nuevo;
            btnActualizar.Enabled = Modificar;
            btnEliminar.Enabled = Eliminar;
            btnGuardar.Enabled = Grabar;
            btnCancelar.Enabled = Cancelar;
            btnCerrar.Enabled = Cancelar;
            grpDatosPrincipales.Enabled = datosPrincipales;

        }
        public int  validar_rango_factura()
        {
            int  valor=0;
                     for (int i = 0; i < gridTiposDocumento.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gridTiposDocumento.Rows[i].Cells["columnaCheck"].Value) == true)
                    {
                      
                       

                    int factura_maxima = NegCajas.recuperaFacturaInicial();
                    int validar = (Int32)gridTiposDocumento.Rows[i].Cells["columnaFactInicial"].Value;
                    if(factura_maxima<= validar)
                    {
                        valor = 1;
                    }
                        

                    else
                    {
                        valor = 0;
                    }
                    }
                }
                     return valor;
        }
        public int validar_grid()
        {
            int valor = 0;
          
            for (int i = 0; i < gridTiposDocumento.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gridTiposDocumento.Rows[i].Cells["columnaCheck"].Value) == true)
                    {
                        int validar_final = (Int32)gridTiposDocumento.Rows[i].Cells["columnaFactFinal"].Value;
                        int validar_Inicial = (Int32)gridTiposDocumento.Rows[i].Cells["columnaFactInicial"].Value;
                   if(validar_final>validar_Inicial)
                   {
                       if(validar_rangos(validar_Inicial,validar_final)==false)

                        
                       {
                           valor = 1;
                       }
                         
                   }
                   else
                   {
                       valor = 0;
                   }
                    
                    }
                }
            return valor;
        }

        private void RecuperarCajas()
        {
            cajas =NegCajas.RecuperaCajas();
            grid_ciudades.DataSource = cajas;
            grid_ciudades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grid_ciudades.Columns["CAJ_CODIGO"].HeaderText = "CODIGO";
            grid_ciudades.Columns["CAJ_NOMBRE"].HeaderText = "NOMBRE";
            grid_ciudades.Columns["CAJ_NUMERO"].HeaderText = "No CAJA";
            grid_ciudades.Columns["CAJ_AUTORIZACION_SRI"].HeaderText = "N° AUTORIZACION";
            grid_ciudades.Columns["LOC_NOMBRE"].HeaderText = "LOCAL";
            //Desahbilitamos las demas columnas
            grid_ciudades.Columns["LOC_CODIGO"].Visible = false;
            grid_ciudades.Columns["CAJ_ESTADO"].Visible = false;
            
            grid_ciudades.Columns["CAJ_FECHA"].Visible = false;
            grid_ciudades.Columns["CAJ_PERIDO_VALIDEZ"].Visible = false;
            grid_ciudades.Columns["ENTITYSETNAME"].Visible = false;
            grid_ciudades.Columns["ENTITYID"].Visible = false;
        }
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (cajaModificada.CAJ_CODIGO == 0)
            {
                AgregarError(txt_codigo);
                valido = false;
            }
            if (cajaModificada.LOC_CODIGO == 0)
            {
                AgregarError(cmb_locales);
                valido = false;
            }
            if (cajaModificada.CAJ_NOMBRE == null)
            {
                AgregarError(txt_descripcion);
                valido = false;
            }
            if (cajaModificada.CAJ_NUMERO == null)
            {
                AgregarError(txt_numcaja);
                valido = false;
            }
            if (cajaModificada.CAJ_AUTORIZACION_SRI == null)
            {
                AgregarError(txt_autorizacion);
                valido = false;
            }
            return valido;

        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            cajaModificada = new DtoCajas();
            cajaOriginal = new DtoCajas();
            txt_codigo.Text = string.Empty;
            txt_descripcion.Text = string.Empty;
            txt_autorizacion.Text = string.Empty;
            txt_numcaja.Text = string.Empty;
            dtp_ValidezAutoriz.Value = DateTime.Now;
            chk_activo.Checked = false;

        }
        private void AgregarBindigControles()
        {
            Binding CAJ_CODIGO = new Binding("Text", cajaModificada, "CAJ_CODIGO", true);
            Binding LOC_CODIGO = new Binding("SelectedValue", cajaModificada, "LOC_CODIGO", true);
            Binding CAJ_NOMBRE = new Binding("Text", cajaModificada, "CAJ_NOMBRE", true);
            Binding CAJ_NUMERO = new Binding("Text", cajaModificada, "CAJ_NUMERO", true);
            Binding CAJ_AUTORIZACION_SRI = new Binding("Text", cajaModificada, "CAJ_AUTORIZACION_SRI", true);
            Binding CAJ_FECHA = new Binding("Text", cajaModificada, "CAJ_FECHA", true);
            Binding CAJ_PERIDO_VALIDEZ = new Binding("Value", cajaModificada, "CAJ_PERIDO_VALIDEZ", true);
            Binding CAJ_ESTADO = new Binding("Checked", cajaModificada, "CAJ_ESTADO", true);
            
            txt_codigo.DataBindings.Clear();
            cmb_locales.DataBindings.Clear();
            txt_descripcion.DataBindings.Clear();
            txt_autorizacion.DataBindings.Clear();
            txt_numcaja.DataBindings.Clear();
            dtp_ValidezAutoriz.DataBindings.Clear();
            chk_activo.DataBindings.Clear();

            txt_codigo.DataBindings.Add(CAJ_CODIGO);
            cmb_locales.DataBindings.Add(LOC_CODIGO);
            txt_descripcion.DataBindings.Add(CAJ_NOMBRE);
            txt_autorizacion.DataBindings.Add(CAJ_AUTORIZACION_SRI);
            txt_numcaja.DataBindings.Add(CAJ_NUMERO);
            dtp_ValidezAutoriz.DataBindings.Add(CAJ_PERIDO_VALIDEZ);
            chk_activo.DataBindings.Add(CAJ_ESTADO);

            CargarNumerosControlCajas();
        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (grid_ciudades.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                grid_ciudades.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                grid_ciudades.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                grid_ciudades.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                grid_ciudades.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class CajaComparer : IComparer<DtoCajas>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public CajaComparer(string strMemberName, SortOrder sortingOrder)
            {
                memberName = strMemberName;
                sortOrder = sortingOrder;
            }

            /// <summary> 
            /// Compares two Students based on member name and sort order 
            /// and return the result. 
            /// </summary> 
            /// <param name="Student1"></param> 
            /// <param name="Student2"></param> 
            /// <returns></returns> 
            public int Compare(DtoCajas Student1, DtoCajas Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "CAJ_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.CAJ_CODIGO.CompareTo(Student2.CAJ_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.CAJ_CODIGO.CompareTo(Student1.CAJ_CODIGO);
                        }

                        break;
                    case "CAJ_NOMBRE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.CAJ_NOMBRE.CompareTo(Student2.CAJ_NOMBRE);
                        }
                        else
                        {
                            returnValue = Student2.CAJ_NOMBRE.CompareTo(Student1.CAJ_NOMBRE);
                        }
                        break;
                    case "CAJ_NUMERO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.CAJ_NUMERO.CompareTo(Student2.CAJ_NUMERO);
                        }
                        else
                        {
                            returnValue = Student2.CAJ_NUMERO.CompareTo(Student1.CAJ_NUMERO);
                        }
                        break;
                    case "CAJ_PERIDO_VALIDEZ":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.CAJ_PERIDO_VALIDEZ.CompareTo(Student2.CAJ_PERIDO_VALIDEZ);
                        }
                        else
                        {
                            returnValue = Student2.CAJ_PERIDO_VALIDEZ.CompareTo(Student1.CAJ_PERIDO_VALIDEZ);
                        }
                        break;


                }
                return returnValue;
            }
        }
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        public bool validar_rangos(int factura_inicial ,int factura_final)
         {
             bool valor = false;
             int validar = 0;

           
             List<NUMERO_CONTROL_CAJAS> RangoFacturas = NegNumControlCajas.RecuperarNumControlCajas();

             for (int i = 0; i < RangoFacturas.Count(); i++)
             {
                 int ncci = Convert.ToInt16(RangoFacturas[i].NCC_FACTURA_INICIAL.ToString());
                 int nccf = Convert.ToInt16(RangoFacturas[i].NCC_FACTURA_FINAL.ToString());
              if((factura_inicial>= ncci && factura_inicial <=nccf)|| (factura_final >= ncci && factura_final <= nccf))

              {
                  validar++;
              }

                 if(validar>0)
                 {
                     valor = true;
                 }

             }

             return valor;
        }
        public void RecuperarTiposDocumento()
        {
            try
            {
                List<TIPO_DOCUMENTO> tiposDocumento = NegTipoDocumentos.RecuperaTipoDocumentos();
                int  a =  Convert.ToInt16( tiposDocumento[0].TID_CODIGO.ToString());
                gridTiposDocumento.DataSource = tiposDocumento.Select(t => new { CODIGO = t.TID_CODIGO, DOCUMENTO = t.TID_DESCRIPCION }).ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void gridTiposDocumento_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            
            if (estadoGrid == false)
            {
                //gridTiposDocumento.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                gridTiposDocumento.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
                gridTiposDocumento.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;

                gridTiposDocumento.DisplayLayout.Bands[0].Columns.Add("columnaCheck", "");
                gridTiposDocumento.DisplayLayout.Bands[0].Columns["columnaCheck"].DataType = typeof(bool);
                gridTiposDocumento.DisplayLayout.Bands[0].Columns["columnaCheck"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;

                gridTiposDocumento.DisplayLayout.Bands[0].Columns.Add("columnaFactInicial", "");
                gridTiposDocumento.DisplayLayout.Bands[0].Columns["columnaFactInicial"].DataType = typeof(Int32);
                gridTiposDocumento.DisplayLayout.Bands[0].Columns["columnaFactInicial"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.IntegerNonNegative;
                gridTiposDocumento.DisplayLayout.Bands[0].Columns["columnaFactInicial"].Header.Caption = "FACTURA INICIAL";

                gridTiposDocumento.DisplayLayout.Bands[0].Columns.Add("columnaFactFinal", "");
                gridTiposDocumento.DisplayLayout.Bands[0].Columns["columnaFactFinal"].DataType = typeof(Int32);
                gridTiposDocumento.DisplayLayout.Bands[0].Columns["columnaFactFinal"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.IntegerNonNegative;
                gridTiposDocumento.DisplayLayout.Bands[0].Columns["columnaFactFinal"].Header.Caption = "FACTURA FINAL";

                gridTiposDocumento.DisplayLayout.Bands[0].Columns.Add("columnaNumControl", "");
                gridTiposDocumento.DisplayLayout.Bands[0].Columns["columnaNumControl"].DataType = typeof(Int32);
                gridTiposDocumento.DisplayLayout.Bands[0].Columns["columnaNumControl"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.IntegerNonNegative;
                gridTiposDocumento.DisplayLayout.Bands[0].Columns["columnaNumControl"].Header.Caption = "NUMERO CONTROL";

                gridTiposDocumento.DisplayLayout.Bands[0].Columns.Add("TIPO", "");
                gridTiposDocumento.DisplayLayout.Bands[0].Columns["TIPO"].DataType = typeof(string);
                gridTiposDocumento.DisplayLayout.Bands[0].Columns["TIPO"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
                
                gridTiposDocumento.DisplayLayout.Bands[0].Columns["TIPO"].Header.Caption = "TIPO";

                gridTiposDocumento.DisplayLayout.Bands[0].Columns.Add("NCC_CODIGO", "");
                gridTiposDocumento.DisplayLayout.Bands[0].Columns["NCC_CODIGO"].DataType = typeof(Int32);
                gridTiposDocumento.DisplayLayout.Bands[0].Columns["NCC_CODIGO"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.IntegerNonNegative;
                gridTiposDocumento.DisplayLayout.Bands[0].Columns["NCC_CODIGO"].Header.Caption = "NCC_CODIGO";

                gridTiposDocumento.DisplayLayout.Bands[0].Override.CellAppearance.BackColor = Color.LightCyan;
                gridTiposDocumento.DisplayLayout.Bands[0].Override.CellAppearance.BackColor2 = Color.Azure;
                gridTiposDocumento.DisplayLayout.Bands[0].Override.CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;

                estadoGrid = true;
            }

            gridTiposDocumento.DisplayLayout.Bands[0].Columns["CODIGO"].Hidden = true;
            gridTiposDocumento.DisplayLayout.Bands[0].Columns["NCC_CODIGO"].Hidden = true;

            gridTiposDocumento.DisplayLayout.Bands[0].Columns["columnaCheck"].Header.VisiblePosition = 1;
            gridTiposDocumento.DisplayLayout.Bands[0].Columns["DOCUMENTO"].Header.VisiblePosition = 2;
            gridTiposDocumento.DisplayLayout.Bands[0].Columns["columnaFactInicial"].Header.VisiblePosition = 3;
            gridTiposDocumento.DisplayLayout.Bands[0].Columns["columnaFactFinal"].Header.VisiblePosition = 4;
            gridTiposDocumento.DisplayLayout.Bands[0].Columns["columnaNumControl"].Header.VisiblePosition = 5;
            gridTiposDocumento.DisplayLayout.Bands[0].Columns["TIPO"].Header.VisiblePosition = 6;
            gridTiposDocumento.DisplayLayout.Bands[0].Columns["DOCUMENTO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            for (int i = 0; i < gridTiposDocumento.Rows.Count; i++)
            {
                gridTiposDocumento.Rows[i].Cells["columnaCheck"].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                gridTiposDocumento.Rows[i].Cells["columnaFactInicial"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                gridTiposDocumento.Rows[i].Cells["columnaFactFinal"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                gridTiposDocumento.Rows[i].Cells["columnaNumControl"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                gridTiposDocumento.Rows[i].Cells["TIPO"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            }

        }

        private void gridTiposDocumento_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            if (e.Cell.Column.Index == 2)
            {
                if (Convert.ToBoolean(gridTiposDocumento.ActiveRow.Cells[2].Value) == false)
                {
                    gridTiposDocumento.ActiveRow.Cells["columnaFactInicial"].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    gridTiposDocumento.ActiveRow.Cells["columnaFactFinal"].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    gridTiposDocumento.ActiveRow.Cells["columnaNumControl"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    gridTiposDocumento.ActiveRow.Cells["TIPO"].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;

                    gridTiposDocumento.ActiveRow.Cells["columnaFactInicial"].Value = 0;
                    gridTiposDocumento.ActiveRow.Cells["columnaFactFinal"].Value = 0;
                    gridTiposDocumento.ActiveRow.Cells["columnaNumControl"].Value = 0;
                    gridTiposDocumento.ActiveRow.Cells["TIPO"].Value = "A";
                }
                else
                {
                    gridTiposDocumento.ActiveRow.Cells["columnaFactInicial"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    gridTiposDocumento.ActiveRow.Cells["columnaFactFinal"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    gridTiposDocumento.ActiveRow.Cells["columnaNumControl"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    gridTiposDocumento.ActiveRow.Cells["TIPO"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                    gridTiposDocumento.ActiveRow.Cells["columnaFactInicial"].Value = null;
                    gridTiposDocumento.ActiveRow.Cells["columnaFactFinal"].Value = null;
                    gridTiposDocumento.ActiveRow.Cells["columnaNumControl"].Value = null;
                    gridTiposDocumento.ActiveRow.Cells["TIPO"].Value = null;

                }
            }
        }

        private void CargarNumerosControlCajas()
        {
            try
            {
                RecuperarTiposDocumento();

                List<DtoNumeroControlCajas> numerosControlCajas = NegNumControlCajas.RecuperaNumControlCajas(cajaModificada.CAJ_CODIGO);

                for (int i = 0; i < gridTiposDocumento.Rows.Count; i++)
                {
                    foreach (var acceso in numerosControlCajas)
                    {
                        if (acceso.TID_CODIGO == Convert.ToInt32(gridTiposDocumento.Rows[i].Cells["CODIGO"].Value))
                        {
                            gridTiposDocumento.Rows[i].Cells["columnaCheck"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                            gridTiposDocumento.Rows[i].Cells["columnaFactInicial"].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                            gridTiposDocumento.Rows[i].Cells["columnaFactFinal"].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                            gridTiposDocumento.Rows[i].Cells["columnaNumControl"].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                            gridTiposDocumento.Rows[i].Cells["TIPO"].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;

                            gridTiposDocumento.Rows[i].Cells["columnaCheck"].Value = true;
                            gridTiposDocumento.Rows[i].Cells["columnaFactInicial"].Value = acceso.NCC_FACTURA_INICIAL;
                            gridTiposDocumento.Rows[i].Cells["columnaFactFinal"].Value = acceso.NCC_FACTURA_FINAL;
                            gridTiposDocumento.Rows[i].Cells["columnaNumControl"].Value = acceso.NCC_NUMERO;
                            gridTiposDocumento.Rows[i].Cells["NCC_CODIGO"].Value = acceso.NCC_CODIGO;
                            gridTiposDocumento.Rows[i].Cells["TIPO"].Value = acceso.NCC_TIPO;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void GrabarNumerosControlCajas()
        {
            try
            {
                for (int i = 0; i < gridTiposDocumento.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gridTiposDocumento.Rows[i].Cells["columnaCheck"].Value) == true)
                    {
                        NUMERO_CONTROL_CAJAS registro = new NUMERO_CONTROL_CAJAS();

                        if (gridTiposDocumento.Rows[i].Cells["NCC_CODIGO"].Value == null)
                        {

                            registro.NCC_CODIGO = (Int16)(NegNumControlCajas.RecuperarMaximoNumControlCajas() + 1);

                            Int16 codTipoDoc = (Int16)(gridTiposDocumento.Rows[i].Cells["CODIGO"].Value);
                            registro.TIPO_DOCUMENTOReference.EntityKey = NegTipoDocumentos.RecuperarTipoDocumentoID(codTipoDoc).EntityKey;
                            registro.CAJASReference.EntityKey = NegCajas.RecuperarCajaID(cajaModificada.CAJ_CODIGO).EntityKey;
                            registro.NCC_FACTURA_INICIAL = (Int32)gridTiposDocumento.Rows[i].Cells["columnaFactInicial"].Value;
                            registro.NCC_FACTURA_FINAL = (Int32)gridTiposDocumento.Rows[i].Cells["columnaFactFinal"].Value;
                            registro.NCC_NUMERO = (Int32)gridTiposDocumento.Rows[i].Cells["columnaNumControl"].Value;
                            registro.NCC_TIPO = gridTiposDocumento.Rows[i].Cells["TIPO"].Value.ToString();
                            registro.NCC_ESTADO = true;
                            NegNumControlCajas.CrearNumControlCajas(registro);

                             
                        }
                        else
                        {
                            Int16 codNumControl = Convert.ToInt16(gridTiposDocumento.Rows[i].Cells["NCC_CODIGO"].Value);
                            registro = NegNumControlCajas.RecuperarNumeroControlCajaID(codNumControl);
                            registro.NCC_FACTURA_INICIAL = (Int32)gridTiposDocumento.Rows[i].Cells["columnaFactInicial"].Value;
                            registro.NCC_FACTURA_FINAL = (Int32)gridTiposDocumento.Rows[i].Cells["columnaFactFinal"].Value;
                            registro.NCC_NUMERO = (Int32)gridTiposDocumento.Rows[i].Cells["columnaNumControl"].Value;
                            registro.NCC_TIPO = gridTiposDocumento.Rows[i].Cells["TIPO"].Value.ToString();
                            NegNumControlCajas.EditarNumControlCaja(registro);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void txt_numcaja_Leave(object sender, EventArgs e)
        {
            if (txt_numcaja.Text != "   -")
            {
                if (txt_numcaja.Text.Replace("-", string.Empty).Length < 6)
                {
                    MessageBox.Show("Numero Incorrecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_numcaja.Focus();
                }
            }
        }

        private void txt_numcaja_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txt_autorizacion.Focus();
            }
        }

        private void frm_Cajas_Fill_Panel_PaintClient(object sender, PaintEventArgs e)
        {

        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void gridTiposDocumento_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < gridTiposDocumento.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gridTiposDocumento.Rows[i].Cells["columnaCheck"].Value) == true)
                    {
                       int validar= (Int32)gridTiposDocumento.Rows[i].Cells["columnaFactInicial"].Value;
                        if(validar>0) 
                        {
                            gridTiposDocumento.ActiveRow.Cells["columnaNumControl"].Value = (Int32)gridTiposDocumento.Rows[i].Cells["columnaFactInicial"].Value;
                            gridTiposDocumento.Rows[i].Cells["columnaNumControl"].Value = (Int32)gridTiposDocumento.Rows[i].Cells["columnaFactInicial"].Value;
                            gridTiposDocumento.Rows[i].Cells["columnaNumControl"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            
                        }
                    }
            }


        }

    }
}
