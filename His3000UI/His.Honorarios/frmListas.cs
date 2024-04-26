using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Honorarios
{
    public partial class frmListas : Form
    {
        TextBox campoPadre = new TextBox();
        String campoClave;
        List<DtoAtenciones> listaAtenciones = new List<DtoAtenciones>();
        List<DtoMedicos> listaMedicos = new List<DtoMedicos>();
        List<PACIENTES> listaPacientes= new List<PACIENTES>();
        List<HABITACIONES> listaHabitaciones = new List<HABITACIONES>();
        int band;
        int columnaBuscada;

        public frmListas()
        {
            InitializeComponent();
        }

        public frmListas(List<DtoAtenciones> lista, TextBox campo)
        {
            InitializeComponent();
            listaAtenciones = lista;
            band = 1;
            dataGridViewList.DataSource = lista;
            editarColumnasAtenciones();
            campoClave = "ATE_NUMERO_CONTROL";
            campoPadre = campo;  
        }

        public frmListas(List<DtoMedicos> lista, TextBox campo)
        {
            InitializeComponent();
            dataGridViewList.DataSource = lista;
            listaMedicos = lista;
            band = 2;
            editarColumnasMedicos();
            campoPadre = campo;
            campoClave = "MED_CODIGO";
        }

        public frmListas(List<PACIENTES> lista, TextBox campo)
        {
            InitializeComponent();
            listaPacientes = lista;
            band = 4;
            dataGridViewList.DataSource = lista;
            editarColumnasPacientes();
            campoClave = "PAC_CODIGO";
            campoPadre = campo;

        }

        public frmListas(List<HABITACIONES> lista, TextBox campo)
        {
            InitializeComponent();
            listaHabitaciones = lista;
            band = 5;
            dataGridViewList.DataSource = lista;
            editarColumnasHabitaciones();
            campoClave = "hab_Codigo";
            campoPadre = campo;
        }


        private void enviarCodigo()
        {
            campoPadre.Text = dataGridViewList.CurrentRow.Cells[campoClave].Value.ToString();
            this.Close();
        }

        private void dataGridViewList_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            enviarCodigo();
        }

        private void dataGridViewList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
                enviarCodigo();
        }

        private void dataGridViewList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (band == 1)
            {
                if (e.Button == MouseButtons.Left)
                {
                    //get the current column details 
                    string strColumnName = dataGridViewList.Columns[e.ColumnIndex].Name;
                    SortOrder strSortOrder = getSortOrder(e.ColumnIndex);
                    listaAtenciones.Sort(new AtencionComparer(strColumnName, strSortOrder));
                    dataGridViewList.DataSource = null;
                    dataGridViewList.DataSource = listaAtenciones;
                    dataGridViewList.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                    editarColumnasAtenciones();
                }
                else
                {
                    columnaBuscada = e.ColumnIndex;
                    //dataGridViewList.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                    mnuContexsecundario.Show(dataGridViewList, new Point(MousePosition.X - e.X - dataGridViewList.Columns[e.ColumnIndex].Width, e.Y));

                }
            }

            if (band == 2)
            {
                if (e.Button == MouseButtons.Left)
                {

                        //get the current column details 
                        string strColumnName = dataGridViewList.Columns[e.ColumnIndex].Name;
                        SortOrder strSortOrder = getSortOrder(e.ColumnIndex);
                        listaMedicos.Sort(new MedicoComparer(strColumnName, strSortOrder));
                        dataGridViewList.DataSource = null;
                        dataGridViewList.DataSource = listaMedicos;
                        editarColumnasMedicos();
                        dataGridViewList.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;

                }
                else
                {
                    columnaBuscada = e.ColumnIndex;
                    //dataGridViewList.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                    mnuContexsecundario.Show(dataGridViewList, new Point(MousePosition.X - e.X - dataGridViewList.Columns[e.ColumnIndex].Width, e.Y));

                }
            }

            if (band == 4)
            {
                if (e.Button == MouseButtons.Left)
                {
                    //get the current column details 
                    string strColumnName = dataGridViewList.Columns[e.ColumnIndex].Name;
                    SortOrder strSortOrder = getSortOrder(e.ColumnIndex);
                    listaPacientes.Sort(new PacienteComparer(strColumnName, strSortOrder));
                    dataGridViewList.DataSource = null;
                    dataGridViewList.DataSource = listaPacientes;
                    dataGridViewList.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                    editarColumnasPacientes();
                }
                else
                {
                    columnaBuscada = e.ColumnIndex;
                    //dataGridViewList.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                    mnuContexsecundario.Show(dataGridViewList, new Point(MousePosition.X - e.X - dataGridViewList.Columns[e.ColumnIndex].Width, e.Y));

                }
            }

            if (band == 5)
            {
                if (e.Button == MouseButtons.Left)
                {
                    //get the current column details 
                    string strColumnName = dataGridViewList.Columns[e.ColumnIndex].Name;
                    SortOrder strSortOrder = getSortOrder(e.ColumnIndex);
                    listaHabitaciones.Sort(new HabitacionComparer(strColumnName, strSortOrder));
                    dataGridViewList.DataSource = null;
                    dataGridViewList.DataSource = listaHabitaciones;
                    dataGridViewList.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                    editarColumnasHabitaciones();
                }
                else
                {
                    columnaBuscada = e.ColumnIndex;
                    //dataGridViewList.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                    mnuContexsecundario.Show(dataGridViewList, new Point(MousePosition.X - e.X - dataGridViewList.Columns[e.ColumnIndex].Width, e.Y));

                }
            }

        }

        private SortOrder getSortOrder(int columnIndex)
        {
            if (dataGridViewList.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                dataGridViewList.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                dataGridViewList.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                dataGridViewList.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
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

        class MedicoComparer : IComparer<DtoMedicos>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public MedicoComparer(string strMemberName, SortOrder sortingOrder)
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
                    case "MED_APELLIDO_MATERNO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.MED_APELLIDO_MATERNO.CompareTo(Student2.MED_APELLIDO_MATERNO);
                        }
                        else
                        {
                            returnValue = Student2.MED_APELLIDO_MATERNO.CompareTo(Student1.MED_APELLIDO_MATERNO);
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
                    case "MED_DIRECCION":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.MED_DIRECCION.CompareTo(Student2.MED_DIRECCION);
                        }
                        else
                        {
                            returnValue = Student2.MED_DIRECCION.CompareTo(Student1.MED_DIRECCION);
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

        public void editarColumnasAtenciones()
        {
            dataGridViewList.Columns["ATE_NUMERO_CONTROL"].HeaderText = "NUMERO CONTROL";
            dataGridViewList.Columns["PAC_NOMBRE"].HeaderText = "PACIENTE";
            dataGridViewList.Columns["PAC_HCL"].HeaderText = "HISTORIA CLINICA";
            dataGridViewList.Columns["PAC_DIRECCION"].HeaderText = "DIRECCION";
            dataGridViewList.Columns["PAC_TELEFONO"].HeaderText = "TELEFONO";
            dataGridViewList.Columns["PAC_CEDULA"].HeaderText = "CEDULA";
            dataGridViewList.Columns["HAB_NUMERO"].HeaderText = "HABITACION";
            dataGridViewList.Columns["ATE_FACTURA_PACIENTE"].HeaderText = "FACTURA";
            dataGridViewList.Columns["ATE_FECHA_INGRESO"].HeaderText = "FECHA DE INGRESO";
            dataGridViewList.Columns["ATE_FECHA_ALTA"].HeaderText = "FECHA DE ALTA";

            dataGridViewList.Columns["ATE_CODIGO"].Visible = false;
            dataGridViewList.Columns["PAC_CODIGO"].Visible = false;
            dataGridViewList.Columns["HAB_CODIGO"].Visible = false;
            dataGridViewList.Columns["ATE_FECHA"].Visible = false;
            dataGridViewList.Columns["ATE_ESTADO"].Visible = false;
            dataGridViewList.Columns["ENTITYSETNAME"].Visible = false;
            dataGridViewList.Columns["ENTITYID"].Visible = false;
        }

        public void editarColumnasMedicos()
        {
            dataGridViewList.Columns["MED_CODIGO"].HeaderText = "CODIGO";
            dataGridViewList.Columns["MED_NOMBRE1"].HeaderText = "NOMBRE";
            dataGridViewList.Columns["MED_NOMBRE2"].HeaderText = "";
            dataGridViewList.Columns["MED_APELLIDO_PATERNO"].HeaderText = "APELLIDO PATERNO";
            dataGridViewList.Columns["MED_APELLIDO_MATERNO"].HeaderText = "APELLIDO MATERNO";
            dataGridViewList.Columns["ESP_NOMBRE"].HeaderText = "ESPECIALIDAD";
            dataGridViewList.Columns["MED_DIRECCION"].HeaderText = "DIRECCION";
            dataGridViewList.Columns["MED_TELEFONO_CASA"].HeaderText = "TLF. CASA";
            dataGridViewList.Columns["MED_AUTORIZACION_SRI"].HeaderText = "AUTORIZACION SRI";
            dataGridViewList.Columns["MED_EMAIL"].HeaderText = "EMAIL";
            dataGridViewList.Columns["RET_PORCENTAJE"].HeaderText = "PORCENTAJE DE RETENCION";

            dataGridViewList.Columns["MED_ESTADO"].Visible = false;
            dataGridViewList.Columns["MED_CON_TRANSFERENCIA"].Visible = false;
            dataGridViewList.Columns["MED_RECIBE_LLAMADA"].Visible = false;
            dataGridViewList.Columns["TIH_CODIGO"].Visible = false;
            dataGridViewList.Columns["MED_FECHA_MODIFICACION"].Visible = false;
            dataGridViewList.Columns["MED_DIRECCION_CONSULTORIO"].Visible=false;
            dataGridViewList.Columns["MED_TELEFONO_CONSULTORIO"].Visible=false;
            dataGridViewList.Columns["MED_TELEFONO_CELULAR"].Visible = false;
            dataGridViewList.Columns["ESP_CODIGO"].Visible = false;
            dataGridViewList.Columns["ID_USUARIO"].Visible = false;
            dataGridViewList.Columns["BAN_CODIGO"].Visible = false;
            dataGridViewList.Columns["TIM_CODIGO"].Visible = false;
            dataGridViewList.Columns["MED_FECHA"].Visible = false;
            dataGridViewList.Columns["MED_RUC"].Visible = false;
            dataGridViewList.Columns["MED_GENERO"].Visible = false;
            dataGridViewList.Columns["ESC_CODIGO"].Visible = false;
            dataGridViewList.Columns["MED_NUM_CUENTA"].Visible = false;
            dataGridViewList.Columns["MED_TIPO_CUENTA"].Visible = false;
            dataGridViewList.Columns["MED_CUENTA_CONTABLE"].Visible = false;
            dataGridViewList.Columns["MED_FECHA_NACIMIENTO"].Visible = false;
            dataGridViewList.Columns["MED_VALIDEZ_AUTORIZACION"].Visible = false;
            dataGridViewList.Columns["RET_CODIGO"].Visible = false;
            dataGridViewList.Columns["RET_DESCRIPCION"].Visible = false;
            dataGridViewList.Columns["MED_FACTURA_INICIAL"].Visible = false;
            dataGridViewList.Columns["MED_FACTURA_FINAL"].Visible = false;
            dataGridViewList.Columns["ENTITYSETNAME"].Visible = false;
            dataGridViewList.Columns["ENTITYID"].Visible = false;
        }

        public void editarColumnasPacientes()
        {
             dataGridViewList.Columns["PAC_CODIGO"].HeaderText = "CODIGO";
            dataGridViewList.Columns["PAC_NOMBRE1"].HeaderText = "NOMBRE";
            dataGridViewList.Columns["PAC_NOMBRE2"].HeaderText = "";
            dataGridViewList.Columns["PAC_APELLIDO_PATERNO"].HeaderText = "APELLIDO PATERNO";
            dataGridViewList.Columns["PAC_APELLIDO_MATERNO"].HeaderText = "APELLIDO MATERNO";
            dataGridViewList.Columns["PAC_HISTORIA_CLINICA"].HeaderText = "HISTORIA CLINICA";
            dataGridViewList.Columns["PAC_CEDULA"].HeaderText = "DIRECCION";
            dataGridViewList.Columns["PAC_TELEFONO"].HeaderText = "TLF. CASA";
            dataGridViewList.Columns["PAC_DIRECCION"].HeaderText = "AUTORIZACION SRI";
            dataGridViewList.Columns["PAC_EMAIL"].HeaderText = "EMAIL";
            dataGridViewList.Columns["PAC_GENERO"].HeaderText = "SEXO";

            dataGridViewList.Columns["PAC_FECHA_CREACION"].Visible = false;
            dataGridViewList.Columns["ATENCIONES"].Visible = false;
            dataGridViewList.Columns["CONTACTOS_EMERGENCIAS"].Visible = false;
            dataGridViewList.Columns["EMPRESA"].Visible = false;
            //dataGridViewList.Columns["HONORARIOS_TARIFARIO"].Visible = false;
            dataGridViewList.Columns["PACIENTES_ASEGURADORAS"].Visible = false;
            dataGridViewList.Columns["TIPO_PACIENTE"].Visible = false;
            dataGridViewList.Columns["PAC_TELEFONO2"].Visible=false;
            dataGridViewList.Columns["PAC_CON_SEGURO"].Visible = false;
            dataGridViewList.Columns["PAC_CIVIL"].Visible = false;
            dataGridViewList.Columns["PAC_HIJOS"].Visible = false;
            dataGridViewList.Columns["PAC_OCUPACION"].Visible = false;
            dataGridViewList.Columns["PAC_REPRESENTANTE"].Visible = false;
            dataGridViewList.Columns["PAC_REPRES_PARENTESCO"].Visible = false;
            dataGridViewList.Columns["PAC_REPRES_CEDULA"].Visible = false;
            dataGridViewList.Columns["PAC_IMAGEN"].Visible = false;
            dataGridViewList.Columns["PAC_OBSERVACIONES"].Visible = false;
            dataGridViewList.Columns["PAC_ESTADO"].Visible = false;
            
        }

        public void editarColumnasHabitaciones()
        {
            dataGridViewList.Columns["hab_Codigo"].HeaderText = "CODIGO";
            dataGridViewList.Columns["hab_Numero"].HeaderText = "NUMERO";
            dataGridViewList.Columns["hab_Libre"].Visible = false;
            dataGridViewList.Columns["ATENCIONES"].Visible = false;
            dataGridViewList.Columns["hab_Padre"].Visible = false;
            dataGridViewList.Columns["hab_Estado"].Visible = false;
        }
        
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Busqueda", dataGridViewList.Columns[columnaBuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= dataGridViewList.RowCount - 1; i++)
                    {
                        if (dataGridViewList.Rows[i].Cells[columnaBuscada].Value.ToString().StartsWith(columna))
                            dataGridViewList.Rows[i].Cells[columnaBuscada].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void frmListas_Load(object sender, EventArgs e)
        {

        }

    }
}
