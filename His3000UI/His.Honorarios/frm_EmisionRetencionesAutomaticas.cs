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
using His.Entidades.Clases;
using Core.Entidades;


namespace His.Honorarios
{
    public partial class frm_EmisionRetencionesAutomaticas : Form
    {
        #region Variables
        public RETENCIONES retModificada = new RETENCIONES();
        public List<DtoRetencionesAutomaticas> honorarios = new List<DtoRetencionesAutomaticas>();
        public CAJAS caja = new CAJAS();
        public int columnabuscada;
        #endregion

        #region Constructores
        public frm_EmisionRetencionesAutomaticas()
        {
            InitializeComponent();
        }
        #endregion

        

        #region Eventos
        private void frm_EmisionRetencionesAutomaticas_Load(object sender, EventArgs e)
        {
            try
            {
                if (NegValidaciones.localAsignado() == false)
                {
                    frm_AsignaLocal lista = new frm_AsignaLocal();
                    lista.ShowDialog();
                }
                HalitarControles(true, false, false,true, false, true, false);
                label2.Text = (NegGeneracionesAutomaticas.RecuperaMaximo() + 1).ToString();
                NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();
                caja = NegCajas.ListaCajas().Where(cod => cod.LOCALES.LOC_CODIGO == Sesion.codLocal).FirstOrDefault();
                txt_ingreso.Text = DateTime.Now.ToString();
                RecuperaHonorariossinRetencion();
                if (NegNumeroControl.NumerodeControlAutomatico(5))
                {
                    numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 5).FirstOrDefault();
                    txt_codigo.Text = string.Format("{0:000}-{1:000}-{2:0000000}", caja.CAJ_NUMERO.ToString().Substring(0, 3), caja.CAJ_NUMERO.ToString().Substring(3), Int16.Parse(numerocontrol.NUMCON));
                    chk_Maratodas.Focus();
                }
                else
                {
                    txt_codigo.ReadOnly = false;
                    txt_codigo.Focus();
                }

                

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void chk_Maratodas_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Maratodas.Checked == true)
            {
                for (int i = 0; i <= gridnotacd.RowCount - 1; i++)
                {
                    gridnotacd.Rows[i].Cells[9].Value = true;
                }
            }
        }
        private void txt_codigo_Leave(object sender, EventArgs e)
        {
            if (txt_codigo.Text != string.Empty && txt_codigo.Text.Replace(" ","").Replace("-","").Length < 13)
            {
               txt_codigo.Text = string.Format("{0:000}-{1:000}-{2:0000000}", caja.CAJ_NUMERO.ToString().Substring(0, 3), caja.CAJ_NUMERO.ToString().Substring(3), Int16.Parse(txt_codigo.Text));
            }
        }
        private void txt_codigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(Keys.Enter))
                {
                    e.Handled = true;
                    chk_Maratodas.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void gridnotacd_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridnotacd.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                honorarios.Sort(new HonorariosComparer(strColumnName, strSortOrder));
                gridnotacd.DataSource = null;
                gridnotacd.DataSource = honorarios;
                // redimensiona el ancho de la columna con los datos que carga
                //gridMedicos.AutoResizeColumns();
                //gridMedicos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                gridnotacd.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridnotacd.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                
                gridnotacd.Columns["MED_CODIGO"].HeaderText = "CODIGO";
                gridnotacd.Columns["SUJETO_RETENCION"].HeaderText = "SUJETO DE RETENCION";
                gridnotacd.Columns["MED_RUC"].HeaderText = "RUC";
                gridnotacd.Columns["HOM_FACTURA_MEDICO"].HeaderText = "FACTURA";
                gridnotacd.Columns["HOM_FACTURA_FECHA"].HeaderText = "FECHA FAC";
                gridnotacd.Columns["HOM_VALOR_NETO"].HeaderText = "VALOR";
                gridnotacd.Columns["RET_PORCENTAJE"].HeaderText = "% RETENCION";
                gridnotacd.Columns["VALOR_RETENCION"].HeaderText = "VALOR RET.";
                gridnotacd.Columns["CONRETENCION"].HeaderText = " ";
                gridnotacd.Columns["RET_CODIGO"].HeaderText = "RETENCION";
                gridnotacd.Columns["HOM_CODIGO"].Visible = false;
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridnotacd, new Point(MousePosition.X - e.X - gridnotacd.Columns[e.ColumnIndex].Width, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Honorarios", gridnotacd.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridnotacd.RowCount - 1; i++)
                    {
                        if (gridnotacd.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridnotacd.CurrentCell = gridnotacd.Rows[i].Cells[columnabuscada];
                        // gridMedicos.Rows[i].Cells[columnabuscada].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            frmReportes frm = new frmReportes();
            frm.reporte = "rRetencionesAutomaticas";
            frm.campo1 = label2.Text;
            frm.Show();

            frmReportes frm1 = new frmReportes();
            frm1.reporte = "rRetencionesAInforme";
            frm1.campo1 = label2.Text;
            frm1.Show();


        }
        #endregion

        #region Metodos privados
        bool validagrid = false;
        private bool ValidarFormulario()
        {
            bool valido = true;
            bool validagrid = false;
            for (int i = 0; i <= gridnotacd.RowCount - 1; i++)
            {
                if (gridnotacd.Rows[i].Cells[9].Value.ToString() == true.ToString())
                {
                    validagrid = true;
                }
                valido = validagrid;
            }
            if (txt_codigo.Text == null || txt_codigo.Text == string.Empty)
            {
                AgregarError(txt_codigo);
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
            HONORARIOS_MEDICOS honorario = new HONORARIOS_MEDICOS();
            int j;
            chk_Maratodas.Focus();
            j = 0;
            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    GENERACIONES_AUTOMATICAS genNuevo = new GENERACIONES_AUTOMATICAS();

                    genNuevo.GEN_CODIGO = int.Parse(label2.Text);
                    genNuevo.GEN_ESTADO = true;
                    genNuevo.GEN_FECHA = DateTime.Parse(txt_ingreso.Text);
                    TIPO_DOCUMENTO tipdocgenera = NegTipoDocumentos.RecuperaTipoDocumentos().Where(cod => cod.TID_CODIGO == 4).FirstOrDefault();
                    genNuevo.TIPO_DOCUMENTOReference.EntityKey = tipdocgenera.EntityKey;
                    USUARIOS usugenera = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == Sesion.codUsuario).FirstOrDefault();
                    genNuevo.USUARIOSReference.EntityKey = usugenera.EntityKey;
                    NegGeneracionesAutomaticas.CrearGeneracionAutomatica(genNuevo);
                    for (int i = 0; i <= gridnotacd.RowCount - 1; i++)
                    {
                        retModificada = new RETENCIONES();
                        honorario = new HONORARIOS_MEDICOS();
                        honorario = new HONORARIOS_MEDICOS();
                        if (gridnotacd.Rows[i].Cells[9].Value.ToString() == true.ToString())
                        {
                            honorario = NegHonorariosMedicos.RecuperaHonorariosMedicos().Where(cod => cod.HOM_CODIGO == int.Parse(gridnotacd.Rows[i].Cells[0].Value.ToString())).FirstOrDefault();
                            MEDICOS medico = NegMedicos.listaMedicos().Where(cod => cod.MED_CODIGO == honorario.MEDICOS.MED_CODIGO).FirstOrDefault();
                            USUARIOS usuModificada = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == Sesion.codUsuario).FirstOrDefault();
                            retModificada.USUARIOSReference.EntityKey = usuModificada.EntityKey;
                            CAJAS cajaModificada = NegCajas.ListaCajas().Where(cod => cod.CAJ_CODIGO == caja.CAJ_CODIGO).FirstOrDefault();
                            retModificada.CAJASReference.EntityKey = cajaModificada.EntityKey;
                            RETENCIONES_FUENTE retfuenteModificada = NegRetencionesFuente.RecuperaRetencionesFuente().Where(cod => cod.RET_CODIGO == medico.RETENCIONES_FUENTE.RET_CODIGO).FirstOrDefault();
                            retModificada.RETENCIONES_FUENTEReference.EntityKey = retfuenteModificada.EntityKey;
                            retModificada.RET_BASE = honorario.HOM_VALOR_NETO;
                            retModificada.RET_DOCUMENTO_AFECTADO = honorario.HOM_FACTURA_MEDICO;
                            retModificada.RET_EJERCICIO_FISCAL = Int16.Parse(DateTime.Now.Year.ToString());
                            retModificada.RET_FECHA = DateTime.Parse(txt_ingreso.Text); //DateTime.Now;
                            retModificada.RET_PORCENTAJE = retfuenteModificada.RET_PORCENTAJE;
                            retModificada.RET_RUC = medico.MED_RUC;
                            retModificada.RET_SUJETO_RETENCION = medico.MED_APELLIDO_PATERNO + " " + medico.MED_NOMBRE1;
                            retModificada.RET_VALOR = retModificada.RET_BASE * retModificada.RET_PORCENTAJE/100;
                                                        

                            if (NegNumeroControl.NumerodeControlAutomatico(5))
                            {
                                retModificada.RET_CODIGO = caja.CAJ_NUMERO + string.Format("{0:0000000}", NegNumeroControl.NumeroControlOptine(5)); // int.Parse((NegMedicos.RecuperaMaximoMedicos() + 1).ToString());
                                NegRetenciones.CrearRetencion(retModificada);
                                NegNumeroControl.LiberaNumeroControl(5);
                            }
                            else
                            {
                                retModificada.RET_CODIGO = caja.CAJ_NUMERO + string.Format("{0:0000000}", int.Parse(txt_codigo.Text.Substring(8)) + j); // int.Parse((NegMedicos.RecuperaMaximoMedicos() + 1).ToString());
                                NegRetenciones.CrearRetencion(retModificada);
                            }
                            j = j + 1;
                            gridnotacd.Rows[i].Cells[10].Value = retModificada.RET_CODIGO;

                            //***
                            GENERACIONES_AUTOMATICAS_DETALLE genNuevodet = new GENERACIONES_AUTOMATICAS_DETALLE();
                            genNuevodet.GAD_CODIGO = NegGeneracionesAutomaticasDetalle.RecuperaMaximoDetalle() + 1;
                            genNuevodet.GAD_NUM_DOCUMENTO = retModificada.RET_CODIGO;
                            GENERACIONES_AUTOMATICAS genek = NegGeneracionesAutomaticas.ListaGeneracionesAutomaticas().Where(cod => cod.GEN_CODIGO == int.Parse(label2.Text)).FirstOrDefault();
                            genNuevodet.GENERACIONES_AUTOMATICASReference.EntityKey = genek.EntityKey;

                            NegGeneracionesAutomaticasDetalle.CrearGeneracionAutomaticaDetalle(genNuevodet);

                            //***
                            DtoHonorariosMedicos honorarioM = NegHonorariosMedicos.RecuperaHonorariosMedicosFormulario(null,"sinRetnecion").Where(cod => cod.HOM_CODIGO == honorario.HOM_CODIGO).FirstOrDefault();
                            DtoHonorariosMedicos honorarioO = honorarioM.ClonarEntidad();
                            honorarioM.RET_CODIGO = retModificada.RET_CODIGO;

                            HONORARIOS_MEDICOS hModificado = new HONORARIOS_MEDICOS();
                            HONORARIOS_MEDICOS hOriginal = new HONORARIOS_MEDICOS();


                            ATENCIONES atModdifi = NegAtenciones.listaAtenciones().Where(cod => cod.ATE_CODIGO == honorarioM.ATE_CODIGO).FirstOrDefault();
//                            hModificado.ATENCIONESReference.EntityKey = atModdifi.EntityKey;
                            hModificado.ATE_CODIGO  = atModdifi.ATE_CODIGO;
                            USUARIOS usuModifi = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == honorario.USUARIOS.ID_USUARIO).FirstOrDefault();
                            hModificado.USUARIOSReference.EntityKey = usuModifi.EntityKey;
                            MEDICOS medModifi = NegMedicos.listaMedicos().Where(cod => cod.MED_CODIGO == honorarioM.MED_CODIGO).FirstOrDefault();
                            hModificado.MEDICOSReference.EntityKey = medModifi.EntityKey;
                            FORMA_PAGO forModifi = NegFormaPago.listaFormasPago().Where(cod => cod.FOR_CODIGO == honorarioM.FOR_CODIGO).FirstOrDefault();
                            hModificado.FOR_CODIGO = forModifi.FOR_CODIGO;
                            //hModificado.FORMA_PAGOReference.EntityKey = forModifi.EntityKey;
                            hModificado.RET_CODIGO1 = honorarioM.RET_CODIGO;
                            hModificado.HOM_APORTE_LLAMADA = honorarioM.HOM_APORTE_LLAMADA;
                            hModificado.HOM_CODIGO = honorarioM.HOM_CODIGO;
                            hModificado.HOM_COMISION_CLINICA = honorarioM.HOM_COMISION_CLINICA;
                            hModificado.HOM_ESTADO = honorarioM.HOM_ESTADO;
                            hModificado.HOM_FACTURA_FECHA = honorarioM.HOM_FACTURA_FECHA;
                            hModificado.HOM_FACTURA_MEDICO = honorarioM.HOM_FACTURA_MEDICO;
                            hModificado.HOM_FECHA_INGRESO = honorarioM.HOM_FECHA_INGRESO;
                            hModificado.HOM_RETENCION = honorarioM.HOM_RETENCION;
                            hModificado.HOM_VALOR_NETO = honorarioM.HOM_VALOR_NETO;
                            hModificado.HOM_VALOR_TOTAL = honorarioM.HOM_VALOR_TOTAL;
                            hModificado.HOM_VALOR_PAGADO = honorarioM.HOM_VALOR_PAGADO;
                            hModificado.HOM_LOTE = honorarioM.HOM_LOTE;
                            hModificado.HOM_VALOR_CANCELADO = honorarioM.HOM_VALOR_CANCELADO;
                            hModificado.EntityKey = new EntityKey("HIS3000BDEntities.HONORARIOS_MEDICOS"
                                , "HOM_CODIGO", honorarioM.HOM_CODIGO);

                            ATENCIONES atOrig = NegAtenciones.listaAtenciones().Where(cod => cod.ATE_CODIGO == honorarioO.ATE_CODIGO).FirstOrDefault();
                            //hOriginal.ATENCIONESReference.EntityKey = atOrig.EntityKey;
                            hOriginal.ATE_CODIGO  = atOrig.ATE_CODIGO;
                            USUARIOS usuOrig = NegUsuarios.RecuperaUsuarios().Where(cod => cod.ID_USUARIO == honorario.USUARIOS.ID_USUARIO).FirstOrDefault();
                            hOriginal.USUARIOSReference.EntityKey = usuOrig.EntityKey;
                            MEDICOS medOrig = NegMedicos.listaMedicos().Where(cod => cod.MED_CODIGO == honorarioO.MED_CODIGO).FirstOrDefault();
                            hOriginal.MEDICOSReference.EntityKey = medOrig.EntityKey;
                            FORMA_PAGO forOrig = NegFormaPago.listaFormasPago().Where(cod => cod.FOR_CODIGO == honorarioO.FOR_CODIGO).FirstOrDefault();
                            //hOriginal.FORMA_PAGOReference.EntityKey = forOrig.EntityKey;
                            hOriginal.FOR_CODIGO = forOrig.FOR_CODIGO;
                            hOriginal.RET_CODIGO1 = honorarioO.RET_CODIGO;
                            hOriginal.HOM_APORTE_LLAMADA = honorarioO.HOM_APORTE_LLAMADA;
                            hOriginal.HOM_CODIGO = honorarioO.HOM_CODIGO;
                            hOriginal.HOM_COMISION_CLINICA = honorarioO.HOM_COMISION_CLINICA;
                            hOriginal.HOM_ESTADO = honorarioO.HOM_ESTADO;
                            hOriginal.HOM_FACTURA_FECHA = honorarioO.HOM_FACTURA_FECHA;
                            hOriginal.HOM_FACTURA_MEDICO = honorarioO.HOM_FACTURA_MEDICO;
                            hOriginal.HOM_FECHA_INGRESO = honorarioO.HOM_FECHA_INGRESO;
                            hOriginal.HOM_RETENCION = honorarioO.HOM_RETENCION;
                            hOriginal.HOM_VALOR_NETO = honorarioO.HOM_VALOR_NETO;
                            hOriginal.HOM_VALOR_TOTAL = honorarioO.HOM_VALOR_TOTAL;
                            hOriginal.HOM_VALOR_PAGADO = honorarioO.HOM_VALOR_PAGADO;
                            hOriginal.HOM_LOTE = honorarioO.HOM_LOTE;
                            hOriginal.HOM_VALOR_CANCELADO = honorarioO.HOM_VALOR_CANCELADO;
                            hOriginal.EntityKey = new EntityKey("HIS3000BDEntities.HONORARIOS_MEDICOS"
                                , "HOM_CODIGO", honorarioO.HOM_CODIGO);

                            NegHonorariosMedicos.GrabarHonorarioMedico(hModificado, hOriginal);
                        }
                    }


                    HalitarControles(false, false, false, false, true, false, false);
                    MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Revise los Datos a guardar, debe estar por lo menos una factura marcada", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
          
        }
        protected virtual void HalitarControles(bool datosPrincipales, bool datosSecundarios, bool Modificar, bool Grabar, bool Eliminar, bool Nuevo, bool Cancelar)
        {
            btnNuevo.Enabled = Nuevo;
            btnActualizar.Enabled = Modificar;
            btnImprimir.Enabled = Eliminar;
            btnGuardar.Enabled = Grabar;
            btnCancelar.Enabled = Cancelar;
            grpDatosPrincipales.Enabled = datosPrincipales;

        }
        private void RecuperaHonorariossinRetencion()
        {

            honorarios =NegHonorariosMedicos.HonorariossinRetencion();
            gridnotacd.DataSource = honorarios;
            gridnotacd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridnotacd.Columns["MED_CODIGO"].HeaderText = "CODIGO";
            gridnotacd.Columns["SUJETO_RETENCION"].HeaderText = "SUJETO DE RETENCION";
            gridnotacd.Columns["MED_RUC"].HeaderText = "RUC";
            gridnotacd.Columns["HOM_FACTURA_MEDICO"].HeaderText = "FACTURA";
            gridnotacd.Columns["HOM_FACTURA_FECHA"].HeaderText = "FECHA FAC";
            gridnotacd.Columns["HOM_VALOR_NETO"].HeaderText = "VALOR";
            gridnotacd.Columns["RET_PORCENTAJE"].HeaderText = "% RETENCION";
            gridnotacd.Columns["VALOR_RETENCION"].HeaderText = "VALOR RET.";
            gridnotacd.Columns["CONRETENCION"].HeaderText = " ";
            gridnotacd.Columns["RET_CODIGO"].HeaderText = "No RETENCION";
            gridnotacd.Columns["HOM_CODIGO"].Visible = false;
        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridnotacd.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridnotacd.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridnotacd.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridnotacd.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridnotacd.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class HonorariosComparer : IComparer<DtoRetencionesAutomaticas>
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
            public int Compare(DtoRetencionesAutomaticas Student1, DtoRetencionesAutomaticas Student2)
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
                    case "SUJETO_RETENCION":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.SUJETO_RETENCION.CompareTo(Student2.SUJETO_RETENCION);
                        }
                        else
                        {
                            returnValue = Student2.SUJETO_RETENCION.CompareTo(Student1.SUJETO_RETENCION);
                        }
                        break;
                    case "MED_RUC":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.MED_RUC.CompareTo(Student2.MED_RUC);
                        }
                        else
                        {
                            returnValue = Student2.MED_RUC.CompareTo(Student1.MED_RUC);
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
                    case "HOM_VALOR_NETO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.HOM_VALOR_NETO.CompareTo(Student2.HOM_VALOR_NETO);
                        }
                        else
                        {
                            returnValue = Student2.HOM_VALOR_NETO.CompareTo(Student1.HOM_VALOR_NETO);
                        }
                        break;
                    case "RET_PORCENTAJE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.RET_PORCENTAJE.CompareTo(Student2.RET_PORCENTAJE);
                        }
                        else
                        {
                            returnValue = Student2.RET_PORCENTAJE.CompareTo(Student1.RET_PORCENTAJE);
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

        private void frm_EmisionRetencionesAutomaticas_Fill_Panel_PaintClient(object sender, PaintEventArgs e)
        {

        }

          
    }
}
