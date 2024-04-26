using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using His.Negocio;
using His.Entidades;
using Core.Entidades;

namespace His.Maintenance
{
    public partial class frm_Empresa : Form
    {
        #region Variables
        public EMPRESA empresaOriginal = new EMPRESA();
        public EMPRESA empresaModificada = new EMPRESA();
        private int temp;
        private int fila;
        private DataSet mDatos;
        private string pathlogo;
        private bool modificaDatos;
        public List<EMPRESA> empresas = new List<EMPRESA>();
        public int columnabuscada;
        #endregion
        #region Constructor
        public frm_Empresa()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frm_empresa_Load(object sender, EventArgs e)
        {
            try
            {
                ResetearControles();
                empresaModificada.EMP_CODIGO = Int16.Parse((NegEmpresa.RecuperaMaximoEmpresa() + 1).ToString());
                AgregarBindigControles();
                txt_nombre.Focus();

                HalitarControles(false, false, true, false, true, true,false,true);
                RecuperarEmpresas();
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
                //GrabarDatos();
                //adminMenu("actualizar");
               //cargarItem();
               RecuperarEmpresas();
                
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
        private void gridEmpresa_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false,true, false);
                empresaModificada = gridEmpresa.CurrentRow.DataBoundItem as EMPRESA;
                empresaOriginal = empresaModificada.ClonarEntidad();
                AgregarBindigControles();
                //pathlogo = empresaOriginal.EMP_LOGO;
                if (File.Exists(pathlogo))
                {
                    if (pathlogo != string.Empty)
                    {
                        //Bitmap logo = new Bitmap(pathlogo);
                        //pctB_logo.SizeMode = PictureBoxSizeMode.StretchImage;
                        //pctB_logo.Image = (Image)logo;
                        //pctB_logo.ImageLocation = pathlogo;  
                        Bitmap logo = new Bitmap(pathlogo);
                        pctB_logo.SizeMode = PictureBoxSizeMode.StretchImage;
                        pctB_logo.Image = (Image)logo;
                    }
                }
                else
                   pctB_logo.Image = null;
               
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
                    NegEmpresa.EliminarEmpresa(empresaModificada.ClonarEntidad());
                    RecuperarEmpresas();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true,false,true);
                    MessageBox.Show("Datos Eliminados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
                //MessageBox.Show("Operación Invalida", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }
        
        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HalitarControles(true, true, false, true, false, false,true,true);
                empresaOriginal = new EMPRESA();
                empresaModificada = new EMPRESA();

                ResetearControles();
                empresaModificada.EMP_CODIGO = Int16.Parse((NegEmpresa.RecuperaMaximoEmpresa() + 1).ToString());
                AgregarBindigControles();
                txt_nombre.Focus();
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
            
        }
        private void btn_logo_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Archivos JPEG(*.jpg)|*.jpg";
            abrir.InitialDirectory = "C:/His3000/imagenes";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                pathlogo = abrir.FileName;
                Bitmap logo = new Bitmap(pathlogo);
                pctB_logo.SizeMode = PictureBoxSizeMode.StretchImage;
                pctB_logo.Image = (Image)logo;

            }
        }
        private void txt_nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txt_ruc.Focus();
            }

        }
        private void txt_direccionemp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
                {
                    e.Handled = true;
                    txt_telefono1.Focus();
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
                    txt_mail.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_mail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
                {
                    e.Handled = true;
                    txt_nombrege.Focus();
                }
            
        }
        private void txt_nombrege_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
                {
                    e.Handled = true;
                    txt_cedula.Focus();
                }

        }
        private void txt_dirger_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txt_nombreCont.Focus();
            }

        }
        private void gridEmpresa_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridEmpresa.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                empresas.Sort(new EmpresaComparer(strColumnName, strSortOrder));
                gridEmpresa.DataSource = null;
                gridEmpresa.DataSource = empresas;
                gridEmpresa.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridEmpresa.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                gridEmpresa.Columns["EMP_NOMBRE"].HeaderText = "Empresa";
                gridEmpresa.Columns["EMP_DIRECCION"].HeaderText = "Dirección";
                gridEmpresa.Columns["EMP_RUC"].HeaderText = "Ruc";
                //Desahbilitamos las demas columnas
                gridEmpresa.Columns["EMP_TELEFONO"].HeaderText = "Telefono";
                gridEmpresa.Columns["EMP_TELEFONO2"].Visible = false;
                gridEmpresa.Columns["EMP_FAX"].Visible = false;
                gridEmpresa.Columns["EMP_EMAIL"].Visible = false;
                gridEmpresa.Columns["EMP_GERENTE"].Visible = false;
                gridEmpresa.Columns["EMP_GERENTE_CEDULA"].Visible = false;
                gridEmpresa.Columns["EMP_GERENTE_TELEFONO"].Visible = false;
                gridEmpresa.Columns["EMP_LOGO"].Visible = false;
                gridEmpresa.Columns["EMP_ELIMINADO"].Visible = false;
                //gridEmpresa.Columns["DEPARTAMENTOS"].Visible = false;
                //gridEmpresa.Columns["LOCALES"].Visible = false;
                gridEmpresa.Columns["EMP_CODIGO"].Visible = false;
                gridEmpresa.Columns["DEPARTAMENTOS"].Visible = false;
                gridEmpresa.Columns["PACIENTES"].Visible = false;
                gridEmpresa.Columns["ZONAS"].Visible = false;
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridEmpresa, new Point(MousePosition.X - this.Left - gridEmpresa.Left, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Departamento", gridEmpresa.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridEmpresa.RowCount - 1; i++)
                    {
                        if (gridEmpresa.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridEmpresa.CurrentCell = gridEmpresa.Rows[i].Cells[columnabuscada];
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
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridEmpresa.Focus();

            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    empresaModificada.EMP_TELEFONO = txt_telefono1.Text.Replace("-", string.Empty);
                    empresaModificada.EMP_TELEFONO2 = txt_telefono2.Text.Replace("-", string.Empty);
                    empresaModificada.EMP_GERENTE_TELEFONO = txt_telefonoger.Text.Replace("-", string.Empty);
                    empresaModificada.EMP_CONTADOR_TELEFONO = txt_telefonoCont.Text.Replace("-", string.Empty);
                    //empresaModificada.EMP_LOGO = pctB_logo.ImageLocation.ToString();
                    empresaModificada.EMP_EMAIL = txt_mail.Text.Replace("_", string.Empty);   

                    if (empresaModificada.EntityKey == null)
                    {
                        NegEmpresa.CrearEmpresa(empresaModificada);
                    }
                    else
                    {
                        NegEmpresa.GrabarEmpresa(empresaModificada, empresaOriginal);
                    }

                    RecuperarEmpresas();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true, false,true);
                    MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (empresaModificada.EMP_CODIGO == 0)
            {
                AgregarError(txt_codigo);
                valido = false;
            }
            if (empresaModificada.EMP_NOMBRE == null || empresaModificada.EMP_NOMBRE == string.Empty)
            {
                //MessageBox.Show("Ingrese el Nombre de la Empresa");                
                txt_nombre.Focus();
                AgregarError(txt_nombre);
                valido = false;
            }
            if (empresaModificada.EMP_RUC == null || empresaModificada.EMP_RUC == string.Empty)
            {
                AgregarError(txt_ruc);
                valido = false;
            }
            else
            {
                if (NegValidaciones.esCedulaValida(txt_ruc.Text) != true)
                {
                    MessageBox.Show("RUC Incorrecto");
                    txt_ruc.Text = string.Empty;
                    txt_ruc.Focus();
                    valido = false;
                }
                else
                {
                    txt_direccionemp.Focus();
                }
            }
            if (empresaModificada.EMP_DIRECCION == null || empresaModificada.EMP_DIRECCION == string.Empty)
            {
                //MessageBox.Show("Ingrese la Dirección de la Empresa");                
                txt_direccionemp.Focus();
                AgregarError(txt_direccionemp);
                valido = false;
            }            
            if (txt_telefono1.Text.ToString() == "  -   -" && txt_telefono2.Text.ToString() != "  -   -")
            {
                if (NegValidaciones.esTelefonoValido(txt_telefono1.Text.Replace("-", string.Empty).ToString()) == false)
                {
                    MessageBox.Show("Ingresar el primer Número de teléfono");                    
                    txt_telefono1.Text = string.Empty;
                    txt_mail.Focus();
                    valido = false;
                }
            }
            else
            {
                if (NegValidaciones.esTelefonoValido(txt_telefono1.Text.Replace("-", string.Empty).ToString()) == false)
                {
                    MessageBox.Show("Numero de teléfono incorrecto");
                    txt_telefono1.Text = string.Empty;
                    valido = false;
                }
                else 
                {
                    if (NegValidaciones.esTelefonoValido(txt_telefono2.Text.Replace("-", string.Empty).ToString()) == false)
                    {
                        MessageBox.Show("Numero de teléfono incorrecto");
                        txt_telefono2.Text = string.Empty;
                        valido = false;
                    }
                }
                
            }
            return valido;
        
        }      


        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido" );
        }
        private void RecuperarEmpresas()
        {
            empresas = NegEmpresa.RecuperaEmpresas();
            gridEmpresa.DataSource = empresas;
            //gridEmpresa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridEmpresa.Columns["EMP_NOMBRE"].HeaderText = "Empresa";
            gridEmpresa.Columns["EMP_DIRECCION"].HeaderText = "Dirección";
            gridEmpresa.Columns["EMP_RUC"].HeaderText = "Ruc";
            //Desahbilitamos las demas columnas
            gridEmpresa.Columns["EMP_TELEFONO"].HeaderText = "Telefono";
            gridEmpresa.Columns["EMP_TELEFONO2"].Visible = false;
            gridEmpresa.Columns["EMP_FAX"].Visible = false;
            gridEmpresa.Columns["EMP_EMAIL"].Visible = false;
            gridEmpresa.Columns["EMP_GERENTE"].Visible = false;
            gridEmpresa.Columns["EMP_GERENTE_CEDULA"].Visible = false;
            gridEmpresa.Columns["EMP_GERENTE_TELEFONO"].Visible = false;
            gridEmpresa.Columns["EMP_LOGO"].Visible = false;
            gridEmpresa.Columns["EMP_ELIMINADO"].Visible = false;
            //gridEmpresa.Columns["DEPARTAMENTOS"].Visible = false;
            //gridEmpresa.Columns["LOCALES"].Visible = false;
            gridEmpresa.Columns["EMP_CODIGO"].Visible = false;
            gridEmpresa.Columns["DEPARTAMENTOS"].Visible = false;
            //gridEmpresa.Columns["PACIENTES"].Visible = false;
            gridEmpresa.Columns["ZONAS"].Visible = false;
        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            empresaModificada = new EMPRESA();
            empresaOriginal = new EMPRESA();
            txt_cedula.Text = string.Empty;
            txt_codigo.Text = string.Empty;
            txt_direccionemp.Text = string.Empty;

            txt_fax.Text = string.Empty;
            txt_mail.Text = string.Empty;
            txt_nombre.Text = string.Empty;
            txt_nombrege.Text = string.Empty;
            txt_ruc.Text = string.Empty;
            txt_telefono1.Text = string.Empty;
            txt_telefono2.Text = string.Empty;
            txt_telefonoger.Text = string.Empty;
            txt_nombreCont.Text = string.Empty;
            txt_cedulaCont.Text = string.Empty;
            txt_telefonoCont.Text = string.Empty;
            txt_dirCont.Text = string.Empty;
            txt_numCont.Text = string.Empty;
            pathlogo = string.Empty;
            pctB_logo.Image = null;
           

        }
        Binding EMP_NOMBRE = null;
        private void AgregarBindigControles()
        {
            Binding EMP_CODIGO = new Binding("Text", empresaModificada, "EMP_CODIGO",true);
            EMP_NOMBRE = new Binding("Text", empresaModificada, "EMP_NOMBRE", true) { };
            Binding EMP_DIRECCION = new Binding("Text", empresaModificada, "EMP_DIRECCION", true);
            Binding EMP_TELEFONO = new Binding("Text", empresaModificada, "EMP_TELEFONO", true);
            Binding EMP_TELEFONO2 = new Binding("Text", empresaModificada, "EMP_TELEFONO2", true);
            Binding EMP_RUC = new Binding("Text", empresaModificada, "EMP_RUC", true);
            Binding EMP_FAX = new Binding("Text", empresaModificada, "EMP_FAX", true);
            Binding EMP_EMAIL = new Binding("Text", empresaModificada, "EMP_EMAIL", true);
            Binding EMP_GERENTE = new Binding("Text", empresaModificada, "EMP_GERENTE", true);
            Binding EMP_GERENTE_CEDULA = new Binding("Text", empresaModificada, "EMP_GERENTE_CEDULA", true);
            Binding EMP_GERENTE_TELEFONO = new Binding("Text", empresaModificada, "EMP_GERENTE_TELEFONO", true);
            Binding EMP_GERENTE_DIRECCION = new Binding("Text", empresaModificada, "EMP_GERENTE_DIRECCION", true);
            Binding EMP_CONTADOR = new Binding("Text", empresaModificada, "EMP_CONTADOR", true);
            Binding EMP_CONTADOR_CEDULA = new Binding("Text", empresaModificada, "EMP_CONTADOR_CEDULA", true);
            Binding EMP_CONTADOR_TELEFONO = new Binding("Text", empresaModificada, "EMP_CONTADOR_TELEFONO", true);
            Binding EMP_CONTADOR_DIRECCION = new Binding("Text", empresaModificada, "EMP_CONTADOR_DIRECCION", true);
            Binding EMP_CONTADOR_NUMERO = new Binding("Text", empresaModificada, "EMP_CONTADOR_NUMERO", true);
            Binding EMP_LOGO = new Binding("Text", empresaModificada, "EMP_LOGO", true);


            txt_codigo.DataBindings.Clear();
            txt_nombre.DataBindings.Clear();
            txt_direccionemp.DataBindings.Clear();
            txt_telefono1.DataBindings.Clear();
            txt_telefono2.DataBindings.Clear();
            txt_ruc.DataBindings.Clear();
            txt_fax.DataBindings.Clear();
            txt_mail.DataBindings.Clear();
            txt_nombrege.DataBindings.Clear();
            txt_cedula.DataBindings.Clear();
            txt_telefonoger.DataBindings.Clear();
            txt_dirger.DataBindings.Clear();
            txt_nombreCont.DataBindings.Clear();
            txt_dirCont.DataBindings.Clear();
            txt_cedulaCont.DataBindings.Clear();
            txt_telefonoCont.DataBindings.Clear();
            txt_numCont.DataBindings.Clear();

            txt_codigo.DataBindings.Add(EMP_CODIGO);
            txt_nombre.DataBindings.Add(EMP_NOMBRE);
            txt_direccionemp.DataBindings.Add(EMP_DIRECCION);
            txt_telefono1.DataBindings.Add(EMP_TELEFONO);
            txt_telefono2.DataBindings.Add(EMP_TELEFONO2);
            txt_ruc.DataBindings.Add(EMP_RUC);
            txt_fax.DataBindings.Add(EMP_FAX);
            txt_mail.DataBindings.Add(EMP_EMAIL);
            txt_nombrege.DataBindings.Add(EMP_GERENTE);
            txt_cedula.DataBindings.Add(EMP_GERENTE_CEDULA);
            txt_telefonoger.DataBindings.Add(EMP_GERENTE_TELEFONO);
            txt_dirger.DataBindings.Add(EMP_GERENTE_DIRECCION);

            txt_nombreCont.DataBindings.Add(EMP_CONTADOR);
            txt_cedulaCont.DataBindings.Add(EMP_CONTADOR_CEDULA);
            txt_telefonoCont.DataBindings.Add(EMP_CONTADOR_TELEFONO);
            txt_dirCont.DataBindings.Add(EMP_CONTADOR_DIRECCION);
            txt_numCont.DataBindings.Add(EMP_CONTADOR_NUMERO);
        }
        /// <summary>
        /// fuencion para habilitar los botones
        /// </summary>
        /// <param name="control">boton que se acciona</param>
        protected virtual void HalitarControles(bool datosPrincipales, bool datosSecundarios, bool Modificar, bool Grabar, bool Eliminar, bool Nuevo, bool Cancelar, bool Salir)
        {
            btnNuevo.Enabled = Nuevo;
            btnActualizar.Enabled = Modificar;
            btnEliminar.Enabled = Eliminar;
            btnGuardar.Enabled = Grabar;
            btnCancelar.Enabled = Cancelar;
            btnSalir.Enabled = Salir;
            grpDatosPrincipales.Enabled = datosPrincipales;
            ugprDatosContador.Enabled = datosPrincipales;
            ugprDatosRepresentante.Enabled = datosPrincipales;            
        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridEmpresa.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridEmpresa.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridEmpresa.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridEmpresa.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridEmpresa.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class EmpresaComparer : IComparer<EMPRESA>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public EmpresaComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(EMPRESA Student1, EMPRESA Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "EMP_NOMBRE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.EMP_NOMBRE.CompareTo(Student2.EMP_NOMBRE);
                        }
                        else
                        {
                            returnValue = Student2.EMP_NOMBRE.CompareTo(Student1.EMP_NOMBRE);
                        }

                        break;
                    case "EMP_DIRECCION":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.EMP_DIRECCION.CompareTo(Student2.EMP_DIRECCION);
                        }
                        else
                        {
                            returnValue = Student2.EMP_DIRECCION.CompareTo(Student1.EMP_DIRECCION);
                        }
                        break;
                    case "EMP_RUC":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.EMP_RUC.CompareTo(Student2.EMP_RUC);
                        }
                        else
                        {
                            returnValue = Student2.EMP_RUC.CompareTo(Student1.EMP_RUC);
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

        private void txt_telefono1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (txt_telefono1.Text.ToString() != "  -   -")
                {
                    if (NegValidaciones.esTelefonoValido(txt_telefono1.Text.Replace("-", string.Empty).ToString()) == false)
                    {
                        MessageBox.Show("Numero de teléfono incorrecto");
                        txt_telefono1.Text = string.Empty;
                        txt_telefono1.Focus();                        
                    }                    
                }
                    txt_telefono2.Enabled = true;
                    txt_telefono2.Focus();                
            }
        }

        private void txt_telefono2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (txt_telefono2.Text.ToString() != "  -   -")
                {
                    if (NegValidaciones.esTelefonoValido(txt_telefono2.Text.Replace("-", string.Empty).ToString()) == false)
                    {
                        MessageBox.Show("Numero de teléfono incorrecto");
                        txt_telefono2.Text = string.Empty;
                        txt_telefono2.Focus();                        
                    }                    
                }                
                    txt_fax.Focus();                
            }
        }

        private void txt_ruc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {              
                if (txt_ruc.Text.ToString() != string.Empty)
                {
                    if (NegValidaciones.esCedulaValida(txt_ruc.Text) != true)
                    {
                        MessageBox.Show("RUC Incorrecto");
                        txt_ruc.Text = string.Empty;
                        txt_ruc.Focus();
                    }
                    else 
                    {
                        txt_direccionemp.Focus();
                    }
                }          
            }            
        }

        private void txt_telefonoger_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (txt_telefonoger.Text.ToString() != "  -   -")
                {
                    if (NegValidaciones.esTelefonoValido(txt_telefonoger.Text.Replace("-", string.Empty).ToString()) == false)
                    {
                        MessageBox.Show("Numero de teléfono incorrecto");
                        txt_telefonoger.Focus();
                        txt_telefonoger.Text = string.Empty;
                    }
                    else
                    {                        
                        txt_dirger.Focus();
                    }
                }
            }
        }

        private void txt_telefonocont_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (txt_telefonoCont.Text.ToString() != "  -   -")
                {
                    if (NegValidaciones.esTelefonoValido(txt_telefonoCont.Text.Replace("-", string.Empty).ToString()) == false)
                    {
                        MessageBox.Show("Numero de teléfono incorrecto");                        
                        txt_telefonoCont.Text = string.Empty;
                    }                    
                }
                txt_dirCont.Focus();                
            }
        }

        private void txt_cedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (txt_cedula.Text.ToString() != string.Empty)
                {
                    if (NegValidaciones.esCedulaValida(txt_cedula.Text) != true)
                    {
                        MessageBox.Show("Cédula Incorrecta");                        
                        txt_cedula.Text = string.Empty;
                    }
                }                
                txt_telefonoger.Focus();                
            }
        }

        private void txt_cedulaCont_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (txt_cedulaCont.Text.ToString() != string.Empty)
                {
                    if (NegValidaciones.esCedulaValida(txt_cedulaCont.Text) != true)
                    {
                        MessageBox.Show("Cédula Incorrecta");                        
                        txt_cedulaCont.Text = string.Empty;
                    }                    
                }
                txt_telefonoCont.Focus();
            }
        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void gridEmpresa_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EMPRESA empresa = (EMPRESA) gridEmpresa.CurrentRow.DataBoundItem;
            //EMPRESA empresa = NegEmpresa.RecuperaEmpresa();            
            txt_nombre.Text = empresa.EMP_NOMBRE;
            txt_ruc.Text = empresa.EMP_RUC;
            txt_direccionemp.Text = empresa.EMP_DIRECCION;
            txt_telefono1.Text = empresa.EMP_TELEFONO;
            txt_telefono2.Text = empresa.EMP_TELEFONO2;
            txt_fax.Text = empresa.EMP_FAX;
            txt_mail.Text = empresa.EMP_EMAIL;
            txt_nombrege.Text = empresa.EMP_GERENTE;
            txt_cedula.Text = empresa.EMP_GERENTE_CEDULA;
            txt_telefonoger.Text = empresa.EMP_GERENTE_TELEFONO;
            txt_dirger.Text = empresa.EMP_GERENTE_DIRECCION;
            txt_nombreCont.Text = empresa.EMP_CONTADOR;
            txt_dirCont.Text = empresa.EMP_GERENTE_DIRECCION;
            txt_cedulaCont.Text = empresa.EMP_GERENTE_CEDULA;
            txt_telefonoCont.Text = empresa.EMP_GERENTE_TELEFONO;
            txt_numCont.Text = empresa.EMP_CONTADOR_NUMERO;
        }

        private void ultraGroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            ResetearControles();
            empresaModificada.EMP_CODIGO = Int16.Parse((NegEmpresa.RecuperaMaximoEmpresa() + 1).ToString());
            AgregarBindigControles();
            txt_nombre.Focus();
            
            //ResetearControles();
            RecuperarEmpresas();
            HalitarControles(false, false, false, false, false, true, false, true);
        }

        private void gridEmpresa_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void gridEmpresa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }        
    }
}
