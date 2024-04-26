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
namespace His.Maintenance
{
    public partial class frm_local : Form
    {
        #region Variables
        /// <summary>
        /// Local original
        /// </summary>
        public DtoLocales localOriginal = new DtoLocales();
        /// <summary>
        /// Local Modificado
        /// </summary>
        public DtoLocales localModificada = new DtoLocales();
         /// <summary>
         /// Zona Seleccionada
         /// </summary>
        private ZONAS zonaSeleccionada;
        List< EMPRESA> empresa = new List<EMPRESA>();
        List<CIUDAD> ciudad = new List<CIUDAD>();
        List<TIPO_NEGOCIO> tipnegocio = new List<TIPO_NEGOCIO>();
        List<USUARIOS> usuario = new List<USUARIOS>();
        List<DtoLocales> locales = new List<DtoLocales>();
        public int columnabuscada;
        #endregion

        #region constructores
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="zona">Zona selecionada</param>
        public frm_local(ZONAS zona)
        {
            InitializeComponent();
            zonaSeleccionada = zona;
            
        }
        /// <summary>
        /// Constructor defecto
        /// </summary>
        public frm_local()
        {
            InitializeComponent();
           
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Cuando se carga la forma
        /// </summary>
        /// <param name="sender">Ventana</param>
        /// <param name="e">e</param>
        private void frm_local_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(false, false, false, false, false, true,false);
                RecuperaLocales();
                txt_codzona.Enabled = false;
                
                //carga combo  tipo negocio
                tipnegocio = NegTipoNegocio.RecuperaTipoNegocios();
                cmb_tiponegocio.DataSource = tipnegocio;
                cmb_tiponegocio.ValueMember = "CODTIPNEG";
                cmb_tiponegocio.DisplayMember = "TIPNEG";
                //carga combo usuarios
                usuario = NegUsuarios.RecuperaUsuarios();
                cmb_usuarios.DataSource = usuario;
                cmb_usuarios.ValueMember = "ID_USUARIO";
                cmb_usuarios.DisplayMember = "nomusu";
                
                txt_codzona.Text = zonaSeleccionada.NOMZONA;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
            
        }
        /// <summary>
        /// Cuando se actualiza
        /// </summary>
        /// <param name="sender">Boton</param>
        /// <param name="e">e</param>
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
        /// <summary>
        /// Cuando se graba
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Cuando se selecciona un registro a editar
        /// </summary>
        /// <param name="sender">GridDataView</param>
        /// <param name="e">e</param>
        private void gridlocales_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false,true);
                localModificada = gridlocales.CurrentRow.DataBoundItem as DtoLocales;
                localOriginal = localModificada.ClonarEntidad();
                AgregarBindigControles();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }

        }
        /// <summary>
        /// Eliminamos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult resultado;

                resultado = MessageBox.Show("Desea eliminar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    LOCALES locmodificado = new LOCALES();
                    LOCALES locoriginal = new LOCALES();

                    locmodificado.LOC_CODIGO = localModificada.LOC_CODIGO;
                    
                    TIPO_NEGOCIO tipnegModifica = cmb_tiponegocio.SelectedItem as TIPO_NEGOCIO;
                    locmodificado.TIPO_NEGOCIOReference.EntityKey = tipnegModifica.EntityKey;
                    locmodificado.ZONASReference.EntityKey = zonaSeleccionada.EntityKey;
                    locmodificado.LOC_NOMBRE = localModificada.LOC_NOMBRE;
                    locmodificado.LOC_DIRECCION = localModificada.LOC_DIRECCION;
                    locmodificado.LOC_TELEFONO = localModificada.LOC_TELEFONO;
                    locmodificado.LOC_RUC = localModificada.LOC_RUC;
                    locmodificado.LOC_AREA = localModificada.LOC_AREA;
                    locmodificado.ID_USUARIO = localModificada.ID_USUARIO;
                    locmodificado.LOC_TEL1 = localModificada.LOC_TEL1;
                    locmodificado.LOC_TEL2 = localModificada.LOC_TEL2;
                    locmodificado.LOC_FAX = localModificada.LOC_FAX;
                    locmodificado.LOC_NUMEMPLE = localModificada.LOC_NUMEMPLE;
                    locmodificado.LOC_BODEGA = localModificada.LOC_BODEGA;
                    locmodificado.LOC_PRINCIPAL = localModificada.LOC_PRINCIPAL;
                    locmodificado.LOC_PRIORIDAD = localModificada.LOC_PRIORIDAD;
                    locmodificado.LOC_PORCENTAJE_DIS = localModificada.LOC_PORCENTAJE_DIS;
                    locmodificado.LOC_MATRIZ = localModificada.LOC_MATRIZ;
                    locmodificado.EntityKey = new EntityKey(locales.First().ENTITYSETNAME
                            , locales.First().ENTITYID, localModificada.LOC_CODIGO);

                    NegLocales.EliminarLocal(locmodificado);
                    
                    RecuperaLocales();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true,false);
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
                HalitarControles(true, true, false, true, false, false,true);
                localOriginal = new DtoLocales();
                localModificada = new DtoLocales();

                ResetearControles();
                localModificada.LOC_CODIGO = Int16.Parse((NegLocales.RecuperaMaximoLocal() + 1).ToString());
                AgregarBindigControles();
                
                txt_codzona.Text = zonaSeleccionada.NOMZONA;
                txt_nomlocal.Focus();
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
            RecuperaLocales();
            HalitarControles(false, false, false, false, false, true, false);
        }
        private void txt_nomlocal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                cmb_tiponegocio.Focus();
            }
        }
        private void txt_codzona_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
        private void cmb_tiponegocio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txt_ruc.Focus();
            }
        }
        private void txt_ruc_Leave(object sender, EventArgs e)
        {
            if (txt_ruc.Text.ToString() != string.Empty)
            {
                if (NegValidaciones.esCedulaValida(txt_ruc.Text) != true)
                {
                    MessageBox.Show("RUC Incorrecto");
                    txt_ruc.Focus();
                    txt_ruc.Text = string.Empty;
                }
            }
        }
        private void cmb_ciudad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txt_direccion.Focus();
            }
        }
        private void txt_direccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txt_telefono1.Focus();
            }
        }
        private void txt_area_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(Keys.Enter))
                {
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_telefono1_Leave(object sender, EventArgs e)
        {
            if (txt_telefono1.Text.ToString() != "  -   -")
            {
                if (NegValidaciones.esTelefonoValido(txt_telefono1.Text.Replace("-", string.Empty).ToString()) == false)
                {
                    MessageBox.Show("Número de teléfono incorrecto");
                    txt_telefono1.Focus();
                    txt_telefono1.Text = string.Empty;
                }
            }
        }

        private void txt_telefono2_Leave(object sender, EventArgs e)
        {
            if (txt_telefono2.Text.ToString() != "  -   -")
            {
                if (NegValidaciones.esTelefonoValido(txt_telefono2.Text.Replace("-", string.Empty).ToString()) == false)
                {
                    MessageBox.Show("Número de teléfono incorrecto");
                    txt_telefono2.Focus();
                    txt_telefono2.Text = string.Empty;
                }
            }
        }

        private void txt_telefono3_Leave(object sender, EventArgs e)
        {
            if (txt_telefono3.Text.ToString() != "  -   -")
            {
                if (NegValidaciones.esTelefonoValido(txt_telefono3.Text.Replace("-", string.Empty).ToString()) == false)
                {
                    MessageBox.Show("Número de teléfono incorrecto");
                    txt_telefono3.Focus();
                    txt_telefono3.Text = string.Empty;
                }
            }
        }
        private void txt_fax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(Keys.Enter))
                {
                    e.Handled = true;
                    txt_pordistribucion.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_pordistribucion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(Keys.Enter))
                {
                    e.Handled = true;
                    cmb_usuarios.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void cmb_usuarios_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txt_nempleados.Focus();
            }
        }
        private void txt_nempleados_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
        private void chk_principal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(Keys.Enter))
                {
                    e.Handled = true;
                    txt_area.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void chk_matriz_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
        private void cmb_empresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
                {
                    e.Handled = true;
                    cmb_tiponegocio.Focus();
                }
            
        }
        private void gridlocales_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridlocales.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                locales.Sort(new LocalesComparer(strColumnName, strSortOrder));
                gridlocales.DataSource = null;
                gridlocales.DataSource = locales;
                gridlocales.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridlocales.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                gridlocales.Columns["LOC_CODIGO"].HeaderText = "CODIGO";
                gridlocales.Columns["LOC_NOMBRE"].HeaderText = "NOMBRE";
                gridlocales.Columns["LOC_DIRECCION"].HeaderText = "DIRECCION";
                gridlocales.Columns["LOC_TELEFONO"].HeaderText = "TELEFONO";
                gridlocales.Columns["LOC_RUC"].HeaderText = "RUC";
                gridlocales.Columns["LOC_PRINCIPAL"].HeaderText = "PRINCIPAL";
                gridlocales.Columns["LOC_MATRIZ"].HeaderText = "MATRIZ";
                //Desahbilitamos las demas columnas
                gridlocales.Columns["CODZONA"].Visible = false;
                gridlocales.Columns["CODTIPNEG"].Visible = false;
                //gridlocales.Columns["EMP_CODIGO"].Visible = false;
                gridlocales.Columns["LOC_AREA"].Visible = false;
                gridlocales.Columns["CODCIUDAD"].Visible = false;
                gridlocales.Columns["ID_USUARIO"].Visible = false;
                gridlocales.Columns["LOC_TEL1"].Visible = false;
                gridlocales.Columns["LOC_TEL2"].Visible = false;
                gridlocales.Columns["LOC_FAX"].Visible = false;
                gridlocales.Columns["LOC_NUMEMPLE"].Visible = false;
                gridlocales.Columns["LOC_BODEGA"].Visible = false;
                gridlocales.Columns["LOC_PRIORIDAD"].Visible = false;
                gridlocales.Columns["LOC_PORCENTAJE_DIS"].Visible = false;
                gridlocales.Columns["NOMZONA"].Visible = false;
                gridlocales.Columns["ENTITYSETNAME"].Visible = false;
                gridlocales.Columns["ENTITYID"].Visible = false;
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridlocales, new Point(MousePosition.X - this.Left - gridlocales.Left, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Locales", gridlocales.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridlocales.RowCount - 1; i++)
                    {
                        if (gridlocales.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridlocales.CurrentCell = gridlocales.Rows[i].Cells[columnabuscada];
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

        #region Metodos privados
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (localModificada.LOC_CODIGO == 0)
            {
                AgregarError(txt_codlocal);
                valido = false;
            }
            if (localModificada.LOC_NOMBRE == null || localModificada.LOC_NOMBRE == string.Empty)
            {
                AgregarError(txt_nomlocal);
                valido = false;
            }
            if (localModificada.CODTIPNEG <= 0)            
            {
                AgregarError(cmb_tiponegocio);
                valido = false;
            }
            if (localModificada.LOC_DIRECCION == null || localModificada.LOC_DIRECCION == string.Empty)
            {
                AgregarError(txt_direccion);
                valido = false;
            }
            if (localModificada.LOC_RUC == null || localModificada.LOC_RUC == string.Empty)
            {
                AgregarError(txt_ruc);
                valido = false;
            }         

            return valido;

        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridlocales.Focus();

            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    LOCALES locmodificado = new LOCALES();
                    LOCALES locoriginal = new LOCALES();

                    locmodificado.LOC_CODIGO = localModificada.LOC_CODIGO;
                    
                    TIPO_NEGOCIO tipnegModifica = cmb_tiponegocio.SelectedItem as TIPO_NEGOCIO;
                    locmodificado.TIPO_NEGOCIOReference.EntityKey = tipnegModifica.EntityKey;
                    
                    locmodificado.ZONASReference.EntityKey = zonaSeleccionada.EntityKey;
                    locmodificado.LOC_NOMBRE = localModificada.LOC_NOMBRE;
                    locmodificado.LOC_DIRECCION = localModificada.LOC_DIRECCION;
                    locmodificado.LOC_TELEFONO = localModificada.LOC_TELEFONO;
                    locmodificado.LOC_RUC = localModificada.LOC_RUC;
                    locmodificado.LOC_AREA = localModificada.LOC_AREA;
                    locmodificado.ID_USUARIO = localModificada.ID_USUARIO;
                    locmodificado.LOC_TEL1 = localModificada.LOC_TEL1;
                    locmodificado.LOC_TEL2 = localModificada.LOC_TEL2;
                    locmodificado.LOC_FAX = localModificada.LOC_FAX;
                    locmodificado.LOC_NUMEMPLE = localModificada.LOC_NUMEMPLE;
                    locmodificado.LOC_BODEGA = localModificada.LOC_BODEGA;
                    locmodificado.LOC_PRINCIPAL = localModificada.LOC_PRINCIPAL;
                    locmodificado.LOC_PRIORIDAD = localModificada.LOC_PRIORIDAD;
                    locmodificado.LOC_PORCENTAJE_DIS = localModificada.LOC_PORCENTAJE_DIS;
                    locmodificado.LOC_MATRIZ = localModificada.LOC_MATRIZ;



                    if (localOriginal.LOC_CODIGO == 0)
                    {
                        NegLocales.CrearLocal(locmodificado);
                        
                    }
                    else
                    {
                        locmodificado.EntityKey = new EntityKey(locales.First().ENTITYSETNAME
                            , locales.First().ENTITYID, localModificada.LOC_CODIGO);

                        locoriginal.LOC_CODIGO = localOriginal.LOC_CODIGO;
                        CIUDAD ciuOriginal = ciudad.Where(ciu => ciu.CODCIUDAD == localOriginal.CODCIUDAD).FirstOrDefault();
                        //locoriginal.CIUDADReference.EntityKey = ciuOriginal.EntityKey;
                        //EMPRESA empresaOriginal = empresa.Where(emp => emp.EMP_CODIGO == localOriginal.EMP_CODIGO).FirstOrDefault();
                        //locoriginal.EMPRESAReference.EntityKey = empresaOriginal.EntityKey;
                        TIPO_NEGOCIO tipOriginal = tipnegocio.Where(tip => tip.CODTIPNEG == localOriginal.CODTIPNEG).FirstOrDefault();
                        locoriginal.TIPO_NEGOCIOReference.EntityKey = tipOriginal.EntityKey;

                        locoriginal.ZONASReference.EntityKey = zonaSeleccionada.EntityKey;
                        locoriginal.LOC_NOMBRE = localOriginal.LOC_NOMBRE;
                        locoriginal.LOC_DIRECCION = localOriginal.LOC_DIRECCION;
                        locoriginal.LOC_TELEFONO = localOriginal.LOC_TELEFONO;
                        locoriginal.LOC_RUC = localOriginal.LOC_RUC;
                        locoriginal.LOC_AREA = localOriginal.LOC_AREA;
                        locoriginal.ID_USUARIO = localOriginal.ID_USUARIO;
                        locoriginal.LOC_TEL1 = localOriginal.LOC_TEL1;
                        locoriginal.LOC_TEL2 = localOriginal.LOC_TEL2;
                        locoriginal.LOC_FAX = localOriginal.LOC_FAX;
                        locoriginal.LOC_NUMEMPLE = localOriginal.LOC_NUMEMPLE;
                        locoriginal.LOC_BODEGA = localOriginal.LOC_BODEGA;
                        locoriginal.LOC_PRINCIPAL = localOriginal.LOC_PRINCIPAL;
                        locoriginal.LOC_PRIORIDAD = localOriginal.LOC_PRIORIDAD;
                        locoriginal.LOC_PORCENTAJE_DIS = localOriginal.LOC_PORCENTAJE_DIS;
                        locoriginal.LOC_MATRIZ = localOriginal.LOC_MATRIZ;
                        locoriginal.EntityKey = new EntityKey(locales.First().ENTITYSETNAME
                          , locales.First().ENTITYID, localOriginal.LOC_CODIGO);

                        NegLocales.GrabarLocal(locmodificado, locoriginal);
                        
                    }

                    RecuperaLocales();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true,false);
                    MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        protected virtual void HalitarControles(bool datosPrincipales, bool datosSecundarios, bool Modificar, bool Grabar, bool Eliminar, bool Nuevo, bool Cancelar)
        {
            btnNuevo.Enabled = Nuevo;
            btnActualizar.Enabled = Modificar;
            btnEliminar.Enabled = Eliminar;
            btnGuardar.Enabled = Grabar;
            btnCancelar.Enabled = Cancelar;
            grpDatosPrincipales.Enabled = datosPrincipales;

        }
        private void RecuperaLocales()
        {
            locales = NegLocales.RecuperaLocales().Where(zon=>zon.CODZONA==zonaSeleccionada.CODZONA).ToList();
            
                gridlocales.DataSource = locales;
                gridlocales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                gridlocales.Columns["LOC_CODIGO"].HeaderText = "CODIGO";
                gridlocales.Columns["LOC_NOMBRE"].HeaderText = "NOMBRE";
                gridlocales.Columns["LOC_DIRECCION"].HeaderText = "DIRECCION";
                gridlocales.Columns["LOC_TELEFONO"].HeaderText = "TELEFONO";
                gridlocales.Columns["LOC_RUC"].HeaderText = "RUC";
                gridlocales.Columns["LOC_PRINCIPAL"].HeaderText = "PRINCIPAL";
                gridlocales.Columns["LOC_MATRIZ"].HeaderText = "MATRIZ";
                //Desahbilitamos las demas columnas
                gridlocales.Columns["CODZONA"].Visible = false;
                gridlocales.Columns["CODTIPNEG"].Visible = false;
                //gridlocales.Columns["EMP_CODIGO"].Visible = false;
                gridlocales.Columns["LOC_AREA"].Visible = false;
                gridlocales.Columns["CODCIUDAD"].Visible = false;
                gridlocales.Columns["ID_USUARIO"].Visible = false;
                gridlocales.Columns["LOC_TEL1"].Visible = false;
                gridlocales.Columns["LOC_TEL2"].Visible = false;
                gridlocales.Columns["LOC_FAX"].Visible = false;
                gridlocales.Columns["LOC_NUMEMPLE"].Visible = false;
                gridlocales.Columns["LOC_BODEGA"].Visible = false;
                gridlocales.Columns["LOC_PRIORIDAD"].Visible = false;
                gridlocales.Columns["LOC_PORCENTAJE_DIS"].Visible = false;
                gridlocales.Columns["NOMZONA"].Visible = false;
                gridlocales.Columns["ENTITYSETNAME"].Visible = false;
                gridlocales.Columns["ENTITYID"].Visible = false;
            
            
        }
        private void AgregarBindigControles()
        {
            Binding LOC_CODIGO = new Binding("Text", localModificada, "LOC_CODIGO", true);
            //Binding CODZONA = new Binding("Text", localModificada, "CODZONA", true) { };
            Binding NOMZONA = new Binding("Text", localModificada, "NOMZONA", true) { };
            Binding CODTIPNEG = new Binding("SelectedValue", localModificada, "CODTIPNEG", true);
            //Binding EMP_CODIGO = new Binding("SelectedValue", localModificada, "EMP_CODIGO", true);
            Binding LOC_NOMBRE = new Binding("Text", localModificada, "LOC_NOMBRE", true);
            Binding LOC_DIRECCION = new Binding("Text", localModificada, "LOC_DIRECCION", true);
            Binding LOC_TELEFONO = new Binding("Text", localModificada, "LOC_TELEFONO", true);
            Binding LOC_RUC = new Binding("Text", localModificada, "LOC_RUC", true);
            Binding LOC_AREA = new Binding("Text", localModificada, "LOC_AREA", true);
            //Binding CODCIUDAD = new Binding("SelectedValue", localModificada, "CODCIUDAD", true);
            Binding ID_USUARIO = new Binding("SelectedValue", localModificada, "ID_USUARIO", true);
            Binding LOC_TEL1 = new Binding("Text", localModificada, "LOC_TEL1", true);
            Binding LOC_TEL2 = new Binding("Text", localModificada, "LOC_TEL2", true);
            Binding LOC_FAX = new Binding("Text", localModificada, "LOC_FAX", true) { };
            Binding LOC_NUMEMPLE = new Binding("Text", localModificada, "LOC_NUMEMPLE", true);
            Binding LOC_BODEGA = new Binding("Text", localModificada, "LOC_BODEGA", true);
            Binding LOC_PRINCIPAL = new Binding("Checked", localModificada, "LOC_PRINCIPAL", true);
            Binding LOC_PRIORIDAD = new Binding("Text", localModificada, "LOC_PRIORIDAD", true);
            Binding LOC_PORCENTAJE_DIS = new Binding("Text", localModificada, "LOC_PORCENTAJE_DIS", true);
            Binding LOC_MATRIZ = new Binding("Checked", localModificada, "LOC_MATRIZ", true);

            txt_codlocal.DataBindings.Clear();
       
            //txt_codzona.DataBindings.Clear();
            cmb_tiponegocio.DataBindings.Clear();
            
            txt_nomlocal.DataBindings.Clear();
            txt_direccion.DataBindings.Clear();
            txt_telefono1.DataBindings.Clear();
            txt_ruc.DataBindings.Clear();
            txt_area.DataBindings.Clear();
            
            cmb_usuarios.DataBindings.Clear();
            txt_telefono2.DataBindings.Clear();
            txt_telefono3.DataBindings.Clear();
            txt_fax.DataBindings.Clear();
            txt_nempleados.DataBindings.Clear();
            chk_principal.DataBindings.Clear();
            txt_pordistribucion.DataBindings.Clear();
            chk_matriz.DataBindings.Clear();
            

            txt_codlocal.DataBindings.Add(LOC_CODIGO);
           
            //txt_codzona.DataBindings.Add(NOMZONA);
            cmb_tiponegocio.DataBindings.Add(CODTIPNEG);
            //cmb_empresa.DataBindings.Add(EMP_CODIGO);
            txt_nomlocal.DataBindings.Add(LOC_NOMBRE);
            txt_direccion.DataBindings.Add(LOC_DIRECCION);
            txt_telefono1.DataBindings.Add(LOC_TELEFONO);
            txt_ruc.DataBindings.Add(LOC_RUC);
            txt_area.DataBindings.Add(LOC_AREA);
            
            cmb_usuarios.DataBindings.Add(ID_USUARIO);
            txt_telefono2.DataBindings.Add(LOC_TEL1);
            txt_telefono3.DataBindings.Add(LOC_TEL2);
            txt_fax.DataBindings.Add(LOC_FAX);
            txt_nempleados.DataBindings.Add(LOC_NUMEMPLE);
            //CHK.DataBindings.Add(LOC_BODEGA);
            chk_principal.DataBindings.Add(LOC_PRINCIPAL);
            //txt_codigo.DataBindings.Add(LOC_PRIORIDAD);
            txt_pordistribucion.DataBindings.Add(LOC_PORCENTAJE_DIS);
            chk_matriz.DataBindings.Add(LOC_MATRIZ);
            
        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            localModificada = new DtoLocales();
            localOriginal = new DtoLocales();

            txt_area.Text = string.Empty;
            txt_codlocal.Text = string.Empty;
            

            txt_direccion.Text = string.Empty;
            txt_fax.Text = string.Empty;
            txt_nempleados.Text = string.Empty;
            txt_nomlocal.Text = string.Empty;
            txt_pordistribucion.Text = string.Empty;
            txt_ruc.Text = string.Empty;
            txt_telefono1.Text = string.Empty;
            txt_telefono2.Text = string.Empty;

            txt_telefono3.Text = string.Empty;
            chk_matriz.Checked = false;
            chk_principal.Checked = false;
            
            cmb_tiponegocio.SelectedItem = -1;
            cmb_usuarios.SelectedItem = -1;
        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridlocales.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridlocales.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridlocales.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridlocales.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridlocales.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class LocalesComparer : IComparer<DtoLocales>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public LocalesComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(DtoLocales Student1, DtoLocales Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "LOC_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.LOC_CODIGO.CompareTo(Student2.LOC_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.LOC_CODIGO.CompareTo(Student1.LOC_CODIGO);
                        }

                        break;
                    case "LOC_NOMBRE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.LOC_NOMBRE.CompareTo(Student2.LOC_NOMBRE);
                        }
                        else
                        {
                            returnValue = Student2.LOC_NOMBRE.CompareTo(Student1.LOC_NOMBRE);
                        }
                        break;
                    case "LOC_DIRECCION":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.LOC_DIRECCION.CompareTo(Student2.LOC_DIRECCION);
                        }
                        else
                        {
                            returnValue = Student2.LOC_DIRECCION.CompareTo(Student1.LOC_DIRECCION);
                        }
                        break;
                    case "LOC_TELEFONO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.LOC_TELEFONO.CompareTo(Student2.LOC_TELEFONO);
                        }
                        else
                        {
                            returnValue = Student2.LOC_TELEFONO.CompareTo(Student1.LOC_TELEFONO);
                        }
                        break;
                    case "LOC_RUC":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.LOC_RUC.CompareTo(Student2.LOC_RUC);
                        }
                        else
                        {
                            returnValue = Student2.LOC_RUC.CompareTo(Student1.LOC_RUC);
                        }
                        break;
                    case "LOC_PRINCIPAL":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.LOC_PRINCIPAL.CompareTo(Student2.LOC_PRINCIPAL);
                        }
                        else
                        {
                            returnValue = Student2.LOC_PRINCIPAL.CompareTo(Student1.LOC_PRINCIPAL);
                        }
                        break;
                    case "LOC_MATRIZ":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.LOC_MATRIZ.CompareTo(Student2.LOC_MATRIZ);
                        }
                        else
                        {
                            returnValue = Student2.LOC_MATRIZ.CompareTo(Student1.LOC_MATRIZ);
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

        #endregion

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        

        

       

        
       

        

        
       
        

        
        

        
        

        

        
        

        
       
        

       

        

   

        

        

        

        

    }
}
