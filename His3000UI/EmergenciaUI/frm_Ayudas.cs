using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;

namespace His.Emergencia
{
    public partial class frm_Ayudas : Form
    {
        #region Variables
        public TextBox campoPadre = null;
        public TextBox campoPadre2 = null;
        public MaskedTextBox campoEspecial = null;
        public string tabla;
        public int columnabuscada;
        public string colRetorno;
        public string colRetorno2;
        //cadena de caracteres que acumula las teclas pulsadas en un segundo
        public string teclas="";
        #endregion

        #region Constructor
        public frm_Ayudas()
        {
            InitializeComponent();
        }
        public frm_Ayudas(List<PACIENTES> pacientes, string tablaExt, string columnBusq)
        {
            InitializeComponent();
            tabla = tablaExt;
            grid.DataSource = pacientes.Select(p => new { HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA, NOMBRE = p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2, ID = p.PAC_IDENTIFICACION }).ToList();
            grid.Columns["HISTORIA_CLINICA"].Width = 120;
            grid.Columns["NOMBRE"].Width = 350;
            grid.Columns["ID"].Width = 160;
            colRetorno = columnBusq;

        }
        public frm_Ayudas(List<DIVISION_POLITICA> divpolitica, string tablaExt, string columnBusq)
        {
            //consultaDivision = divpolitica;
            InitializeComponent();
            tabla = tablaExt;
            colRetorno = columnBusq;
            grid.DataSource = divpolitica.Select(d => new { CODIGO= d.DIPO_CODIINEC , NOMBRE=d.DIPO_NOMBRE}).ToList();
        }
        public frm_Ayudas(List<ATENCIONES> atenciones, string tablaExt, string columnBusq)
        {
            //consultaAtenciones = atenciones;
            InitializeComponent();
            tabla = tablaExt;
            colRetorno = columnBusq;
            grid.DataSource = atenciones;
        }
        public frm_Ayudas(List<ASEGURADORAS_EMPRESAS> empresas, string tablaExt, string columnBusq)
        {
            //consultaEmpresas = empresas;
            InitializeComponent();
            tabla = tablaExt;
            colRetorno = columnBusq;
            grid.DataSource = empresas.Select(e => new { NOMBRE = e.ASE_NOMBRE, RUC = e.ASE_RUC}).ToList();
        }
        public frm_Ayudas(List<PAIS> nacionalidades, string tablaExt, string columnBusq)
        {
            InitializeComponent();
            tabla = tablaExt;
            colRetorno = columnBusq;
            grid.DataSource = nacionalidades.Select(n => new { NACIONALIDAD = n.NACIONALIDAD }).ToList();
            grid.Columns["NACIONALIDAD"].Width = 300;
        }

        #endregion

        #region Eventos
        private void grid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            enviarCodigo();
        }
        private void grid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    enviarCodigo();
                }
                else if (e.KeyCode == Keys.End)
                {
                    grid.CurrentCell = grid.Rows[grid.Rows.Count - 1].Cells[grid.CurrentCell.ColumnIndex];
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Home)
                {
                    grid.CurrentCell = grid.Rows[0].Cells[grid.CurrentCell.ColumnIndex];
                    e.Handled = true; 
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);      
            }
        }
        private void frm_Ayudas_Load(object sender, EventArgs e)
        {
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Ayuda", grid.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= grid.RowCount - 1; i++)
                    {
                        if (grid.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            grid.CurrentCell = grid.Rows[i].Cells[columnabuscada];
                        // gridMedicos.Rows[i].Cells[columnabuscada].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void grid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            { 
                    //grid.DataSource = consultaDivision;
                    //get the current column details 
                    //string strColumnName = grid.Columns[e.ColumnIndex].Name;
                    //SortOrder strSortOrder = getSortOrder(e.ColumnIndex);
                    //consulta.Sort(new StudentComparer(strColumnName, strSortOrder));
                    //grid.DataSource = null;
                    //grid.DataSource = consulta;
                    //grid.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                    //grid.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                    //grid.Columns["ID_USUARIO"].HeaderText = "CODIGO";
                    //grid.Columns["NOMBRES"].HeaderText = "NOMBRE";
                    //grid.Columns["APELLIDOS"].HeaderText = "APELLIDO";
                    //grid.Columns["IDENTIFICACION"].Visible = false;
                    //grid.Columns["ESTADO"].Visible = false;
                    //grid.Columns["DEP_CODIGO"].Visible = false;
                    //grid.Columns["FECHA_INGRESO"].Visible = false;
                    //grid.Columns["FECHA_VENCIMIENTO"].Visible = false;
                    //grid.Columns["DIRECCION"].Visible = false;
                    //grid.Columns["USR"].Visible = false;
                    //grid.Columns["PWD"].Visible = false;
                    //grid.Columns["LOGEADO"].Visible = false;
                    //grid.Columns["ENTITYSETNAME"].Visible = false;
                    //grid.Columns["ENTITYID"].Visible = false;
               
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(grid, new Point(MousePosition.X - grid.Width + grid.Columns[e.ColumnIndex].Width, e.Y));

            }
        }

        #endregion

        #region Metodos Privados
        private void enviarCodigo()
        {
            if(campoPadre!=null)
            campoPadre.Text = grid.CurrentRow.Cells[colRetorno].Value.ToString();

            if (campoEspecial != null)
                campoEspecial.Text = grid.CurrentRow.Cells[colRetorno].Value.ToString();

            if(campoPadre2!= null && colRetorno2!=null)
                campoPadre2.Text = grid.CurrentRow.Cells[colRetorno2].Value.ToString();
            this.Close();
        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (grid.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                grid.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                grid.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                grid.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                grid.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class HonorariosComparer : IComparer<DtoHonorariosMedicos>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public HonorariosComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(DtoHonorariosMedicos Student1, DtoHonorariosMedicos Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "HOM_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.HOM_CODIGO.CompareTo(Student2.HOM_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.HOM_CODIGO.CompareTo(Student1.HOM_CODIGO);
                        }

                        break;
                    case "MED_NOMBRE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.MED_NOMBRE.CompareTo(Student2.MED_NOMBRE);
                        }
                        else
                        {
                            returnValue = Student2.MED_NOMBRE.CompareTo(Student1.MED_NOMBRE);
                        }
                        break;
                    case "HOM_FACTURA_FECHA":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.HOM_FACTURA_FECHA.CompareTo(Student2.HOM_FACTURA_FECHA);
                        }
                        else
                        {
                            returnValue = Student2.HOM_FACTURA_FECHA.CompareTo(Student1.HOM_FACTURA_FECHA);
                        }
                        break;
                    case "HOM_FACTURA_MEDICO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.HOM_FACTURA_MEDICO.CompareTo(Student2.HOM_FACTURA_MEDICO);
                        }
                        else
                        {
                            returnValue = Student2.HOM_FACTURA_MEDICO.CompareTo(Student1.HOM_FACTURA_MEDICO);
                        }
                        break;
                   
                }
                return returnValue;
            }
        }
        class StudentComparer : IComparer<DtoUsuarios>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public StudentComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(DtoUsuarios Student1, DtoUsuarios Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "ID_USUARIO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.ID_USUARIO.CompareTo(Student2.ID_USUARIO);
                        }
                        else
                        {
                            returnValue = Student2.ID_USUARIO.CompareTo(Student1.ID_USUARIO);
                        }

                        break;
                    case "NOMBRES":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.NOMBRES.CompareTo(Student2.NOMBRES);
                        }
                        else
                        {
                            returnValue = Student2.NOMBRES.CompareTo(Student1.NOMBRES);
                        }
                        break;
                    case "APELLIDOS":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.APELLIDOS.CompareTo(Student2.APELLIDOS);
                        }
                        else
                        {
                            returnValue = Student2.APELLIDOS.CompareTo(Student1.APELLIDOS);
                        }
                        break;
                   
                }
                return returnValue;
            }
        }
        class MedicosComparer : IComparer<DtoMedicos>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public MedicosComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(DtoMedicos Student1, DtoMedicos Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "MED_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.MED_CODIGO.CompareTo(Student2.MED_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.MED_CODIGO.CompareTo(Student1.MED_CODIGO);
                        }

                        break;
                    case "ESP_NOMBRE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.ESP_NOMBRE.CompareTo(Student2.ESP_NOMBRE);
                        }
                        else
                        {
                            returnValue = Student2.ESP_NOMBRE.CompareTo(Student1.ESP_NOMBRE);
                        }
                        break;
                    case "MED_NOMBRE1":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.MED_NOMBRE1.CompareTo(Student2.MED_NOMBRE1);
                        }
                        else
                        {
                            returnValue = Student2.MED_NOMBRE1.CompareTo(Student1.MED_NOMBRE1);
                        }
                        break;
                    case "MED_APELLIDO_PATERNO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.MED_APELLIDO_PATERNO.CompareTo(Student2.MED_APELLIDO_PATERNO);
                        }
                        else
                        {
                            returnValue = Student2.MED_APELLIDO_PATERNO.CompareTo(Student1.MED_APELLIDO_PATERNO);
                        }
                        break;

                }
                return returnValue;
            }
        }
        class AtencionComparer : IComparer<DtoAtenciones>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public AtencionComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(DtoAtenciones Student1, DtoAtenciones Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "PAC_NOMBRE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.PAC_NOMBRE.CompareTo(Student2.PAC_NOMBRE);
                        }
                        else
                        {
                            returnValue = Student2.PAC_NOMBRE.CompareTo(Student1.PAC_NOMBRE);
                        }

                        break;
                    case "ATE_NUMERO_CONTROL":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.ATE_NUMERO_CONTROL.CompareTo(Student2.ATE_NUMERO_CONTROL);
                        }
                        else
                        {
                            returnValue = Student2.ATE_NUMERO_CONTROL.CompareTo(Student1.ATE_NUMERO_CONTROL);
                        }
                        break;
                    case "ATE_FACTURA_PACIENTE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.ATE_FACTURA_PACIENTE.CompareTo(Student2.ATE_FACTURA_PACIENTE);
                        }
                        else
                        {
                            returnValue = Student2.ATE_FACTURA_PACIENTE.CompareTo(Student1.ATE_FACTURA_PACIENTE);
                        }
                        break;
                    case "PAC_DIRECCION":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.PAC_DIRECCION.CompareTo(Student2.PAC_DIRECCION);
                        }
                        else
                        {
                            returnValue = Student2.PAC_DIRECCION.CompareTo(Student1.PAC_DIRECCION);
                        }
                        break;
                    case "ATE_FECHA_ALTA":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.ATE_FECHA_ALTA.CompareTo(Student2.ATE_FECHA_ALTA);
                        }
                        else
                        {
                            returnValue = Student2.ATE_FECHA_ALTA.CompareTo(Student1.ATE_FECHA_ALTA);
                        }
                        break;

                }
                return returnValue;
            }
        }
        class PacienteComparer : IComparer<PACIENTES>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public PacienteComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(PACIENTES Student1, PACIENTES Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "PAC_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.PAC_CODIGO.CompareTo(Student2.PAC_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.PAC_CODIGO.CompareTo(Student1.PAC_CODIGO);
                        }

                        break;
                    case "PAC_APELLIDO_PATERNO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.PAC_APELLIDO_PATERNO.CompareTo(Student2.PAC_APELLIDO_PATERNO);
                        }
                        else
                        {
                            returnValue = Student2.PAC_APELLIDO_PATERNO.CompareTo(Student1.PAC_APELLIDO_PATERNO);
                        }
                        break;
                    case "PAC_APELLIDO_MATERNO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.PAC_APELLIDO_MATERNO.CompareTo(Student2.PAC_APELLIDO_MATERNO);
                        }
                        else
                        {
                            returnValue = Student2.PAC_APELLIDO_MATERNO.CompareTo(Student1.PAC_APELLIDO_MATERNO);
                        }
                        break;
                    case "PAC_NOMBRE1":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.PAC_NOMBRE1.CompareTo(Student2.PAC_NOMBRE1);
                        }
                        else
                        {
                            returnValue = Student2.PAC_NOMBRE1.CompareTo(Student1.PAC_NOMBRE1);
                        }
                        break;
                    case "PAC_NOMBRE2":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.PAC_NOMBRE2.CompareTo(Student2.PAC_NOMBRE2);
                        }
                        else
                        {
                            returnValue = Student2.PAC_NOMBRE2.CompareTo(Student1.PAC_NOMBRE2);
                        }
                        break;
                    //case "PAC_DIRECCION":
                    //    if (sortOrder == SortOrder.Ascending)
                    //    {
                    //        returnValue = Student1.PAC_DIRECCION.CompareTo(Student2.PAC_DIRECCION);
                    //    }
                    //    else
                    //    {
                    //        returnValue = Student2.PAC_DIRECCION.CompareTo(Student1.PAC_DIRECCION);
                    //    }
                    //    break;
                    //case "PAC_HISTORIA_CLINICA":
                    //    if (sortOrder == SortOrder.Ascending)
                    //    {
                    //        returnValue = Student1.PAC_HISTORIA_CLINICA.CompareTo(Student2.PAC_HISTORIA_CLINICA);
                    //    }
                    //    else
                    //    {
                    //        returnValue = Student2.PAC_HISTORIA_CLINICA.CompareTo(Student1.PAC_HISTORIA_CLINICA);
                    //    }
                    //    break;

                }
                return returnValue;
            }
        }
        class HabitacionComparer : IComparer<HABITACIONES>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public HabitacionComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(HABITACIONES Student1, HABITACIONES Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "hab_Codigo":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.hab_Codigo.CompareTo(Student2.hab_Codigo);
                        }
                        else
                        {
                            returnValue = Student2.hab_Codigo.CompareTo(Student1.hab_Codigo);
                        }

                        break;
                    case "hab_Numero":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.hab_Numero.CompareTo(Student2.hab_Numero);
                        }
                        else
                        {
                            returnValue = Student2.hab_Numero.CompareTo(Student1.hab_Numero);
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

        //realizo la busqueda en las celdas de cada columna
        private void grid_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //solo se aceptan digitos y letras
                if (Char.IsLetterOrDigit(e.KeyChar))
                {
                    //capturo las teclas presionadas mientras el control timer este Activo (500 milisegundos)
                    if (timerCapturaTeclas.Enabled == true)
                    {
                        teclas += e.KeyChar.ToString();
                    }
                    //si el control esta inactivo, paso el control a activo y vuelvo a iniciar la captura
                    else
                    {
                        teclas = e.KeyChar.ToString();
                        timerCapturaTeclas.Enabled = true;
                    }
                    //celda seleccionada
                    DataGridViewCell celda = grid.CurrentCell;
                    int ini;
                    //valido si esta en la ultima celda
                    if (grid.CurrentRow.Index == (grid.Rows.Count - 1))
                    {
                        ini = 0;    //si esta en la ultima celda, inicia la busqueda desde la primera celda 
                    }
                    else
                    {
                        ini = grid.CurrentRow.Index + 1;    //inicia la busqueda desde la siguiente celda
                    }
                    //verifico si existen coincidencias desde la celda actual hasta la final
                    for (int i=ini; i < grid.Rows.Count; i++)
                        {
                            if (grid.Rows[i].Cells[celda.ColumnIndex].Value.ToString().ToUpper().StartsWith(teclas.ToUpper()))
                            {
                                grid.CurrentCell = grid.Rows[i].Cells[celda.ColumnIndex];
                                return;
                            }
                        }
                    //verifico si existen coincidencias desde la primera celda hasta la actual
                    for (int i = 0; i < ini; i++)
                    {
                        if (grid.Rows[i].Cells[celda.ColumnIndex].Value.ToString().ToUpper().StartsWith(teclas.ToUpper()))
                        {
                            grid.CurrentCell = grid.Rows[i].Cells[celda.ColumnIndex];
                            return;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);       
            }
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void timerCapturaTeclas_Tick(object sender, EventArgs e)
        {
            //desactivo el control, al terminarse el intervalo
            timerCapturaTeclas.Enabled = false;
        }



        

        

        

        
       

        
    }
}
