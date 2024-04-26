using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.General;
using Infragistics.Win.UltraWinGrid;
using His.Negocio; //Edgar Ramos 20201109

namespace His.Honorarios
{
    public partial class frm_Ayudas : Form
    {
        NegQuirofano Med = new NegQuirofano(); //Reutilizo sp para cargar medicos en el grid. Edgar Ramos 20201109
        #region Variables
        public TextBox campoPadre = null;
        public TextBox campoPadre2 = null;
        public MaskedTextBox campoEspecial = null;
        public string tabla;
        public int columnabuscada;
        public string colRetorno;
        public string colRetorno2;
        internal static bool medico; //devuelve la tabla de medicos desde el explorador de honorarios --- cambios Edgar 20201109
        //cadena de caracteres que acumula las teclas pulsadas en un segundo
        public string teclas = "";

        #endregion

        #region Constructor
        public frm_Ayudas()
        {
            InitializeComponent();
        }

        public frm_Ayudas(List<PACIENTES> pacientes)
        {
            //consultaPacientes = pacientes;
            InitializeComponent();
            //grid.DataSource = consultaPacientes.Select(p => new { 
            //    CODIGO = p.PAC_CODIGO, 
            //    HCL = p.PAC_HISTORIA_CLINICA,
            //    NOMBRE= p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2,
            //    CEDULA = p.PAC_IDENTIFICACION
            //}).ToList();
            //columnabuscada = "CODIGO";
            //tabla = "PACIENTES";
            //grid.Columns["NOMBRE"].Width = 250;
            
        }

        public frm_Ayudas(List<MEDICOS> medicos)
        {
            InitializeComponent();
            tabla = "MEDICOS";
            colRetorno = "CODIGO";
            grid.DataSource = medicos.Select(n => new
            {
                CODIGO = n.MED_CODIGO,
                NOMBRE = n.MED_APELLIDO_PATERNO + " " + n.MED_APELLIDO_MATERNO + " " + n.MED_NOMBRE1 + " " + n.MED_NOMBRE2,
                //ESPECIALIDAD = n.ESPECIALIDADES_MEDICAS.ESP_NOMBRE,
                RUC = n.MED_RUC
            }).ToList();
            campoPadre = new TextBox();
            //grid.Columns["NOMBRE"].Width = 300;
        }

        public frm_Ayudas(List<DtoAtenciones> atenciones)
        {
            //consultaAtenciones = atenciones;
            InitializeComponent();
            tabla = "ATENCIONES";
            colRetorno = "NUM_CONTROL";
            grid.DataSource = atenciones.Select(a => new
            {
                NUM_CONTROL = a.ATE_NUMERO_CONTROL,
                FACTURA = a.ATE_FACTURA_PACIENTE,
                PACIENTE = a.PAC_APELLIDO_PATERNO + " " + a.PAC_APELLIDO_MATERNO + " " + a.PAC_NOMBRE + " " + a.PAC_NOMBRE2,
                HCL= a.PAC_HCL,
                HABITACION = a.HAB_NUMERO,
                FEC_INGRESO = a.ATE_FECHA_INGRESO,
                FEC_ALTA = a.ATE_FECHA_ALTA
            }).ToList();
            campoPadre = new TextBox();
            //grid.Columns["PACIENTE"].Width = 250;
        }

        public frm_Ayudas(List<HABITACIONES> habitaciones)
        {
            //consultaHabitaciones = habitaciones;
            InitializeComponent();
            tabla = "HABITACIONES";
            colRetorno = "CODIGO";
            grid.DataSource = habitaciones.Select(h => new
            {
                CODIGO = h.hab_Codigo,
                NUMERO = h.hab_Numero
            }).ToList();
            campoPadre = new TextBox();
        }

        public frm_Ayudas(List<DtoMedicos> dtoMedicos)
        {
            //consultamedicos = dtoMedicos;
            InitializeComponent();
            tabla = "MEDICOS";
            colRetorno = "CODIGO";
            grid.DataSource = dtoMedicos.Select(h => new
            {
                CODIGO = h.MED_CODIGO,
                NOMBRE = h.MED_APELLIDO_PATERNO+" "+h.MED_NOMBRE1,
                RUC = h.MED_RUC
            }).ToList();
            campoPadre = new TextBox();
            //grid.Columns["NOMBRE"].Width = 300;
        }

        public frm_Ayudas(List<DtoHonorariosMedicos> dtoHonorarios)
        {
            //consultaHonorarios = dtoHonorarios;
            InitializeComponent();
            tabla = "HONORARIOSNONOTADEBITO";
            colRetorno = "CODIGO";
            grid.DataSource = dtoHonorarios.Select(h => new
            {
                CODIGO = h.HOM_CODIGO,
                NUM_DOCUMENTO = h.HOM_FACTURA_MEDICO,
                FECHA = h.HOM_FACTURA_FECHA,
                VALOR = h.HOM_VALOR_NETO
            }).ToList();
            campoPadre = new TextBox();
            
            //grid.Columns["MEDICO"].Width = 300;
        }
        public frm_Ayudas(List<DtoMedicoCuentaContable> dtoMedicosCG)
        {
            //consultamedicos = dtoMedicos;
            InitializeComponent();
            tabla = "MEDICOS_CG";
            colRetorno = "CODIGO";
            grid.DataSource = dtoMedicosCG.Select(h => new
            {
                CODIGO = h.MED_CODIGO_C,
                NOMBRES = h.MED_NOMBRE1 + " " + h.MED_NOMBRE2,
                APELLIDOS = h.MED_APELLIDOS_PATERNO + " " + h.MED_APELLIDOS_MATERNO,
            }).ToList();
            campoPadre = new TextBox();
            //grid.Columns["NOMBRE"].Width = 300;
        }
        public frm_Ayudas(List<DtoUsuarios> dtoUsuarios)
        {
            //consultamedicos = dtoMedicos;
            InitializeComponent();
            tabla = "USUARIOS";
            colRetorno = "CODIGO";
            grid.DataSource = dtoUsuarios.Select(h => new
            {
                CODIGO = h.ID_USUARIO,
                NOMBRE = h.APELLIDOS + " " + h.NOMBRES,
                ID = h.IDENTIFICACION
            }).ToList();
            campoPadre = new TextBox();
            //grid.Columns["NOMBRE"].Width = 300;
        }
        #endregion

        #region Eventos
        private void grid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }
        private void grid_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
        private void frm_Ayudas_Load(object sender, EventArgs e)
        {
            if (tabla == "MEDICOS")
            {
                if(grid.ActiveRow!=null)
                    grid.ActiveCell = grid.ActiveRow.Cells["NOMBRE"];
            }
            if(medico == true)
            {
                CargarMedicos();
            }
        }
        private void CargarMedicos()
        {
            grid.DataSource = Med.MostrarMedicos();
        }
        private void grid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        #endregion

        #region Metodos Privados
        private void enviarCodigo()
        {
            if (grid.ActiveRow.Index > -1)
            {
                if(medico == false)
                {
                    if (campoPadre != null)
                        campoPadre.Text = grid.ActiveRow.Cells[colRetorno].Value.ToString();

                    if (campoEspecial != null)
                        campoEspecial.Text = grid.ActiveRow.Cells[colRetorno].Value.ToString();

                    if (campoPadre2 != null && colRetorno2 != null)
                        campoPadre2.Text = grid.ActiveRow.Cells[colRetorno2].Value.ToString();
                    this.Close();
                }
                else
                {
                    frmExploradorGeneral.cod_medico = grid.ActiveRow.Cells[0].Value.ToString();
                    this.Close();
                }
            }
        }

        #endregion

        //realizo la busqueda en las celdas de cada columna
        private void grid_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void timerCapturaTeclas_Tick(object sender, EventArgs e)
        {
            //desactivo el control, al terminarse el intervalo
            timerCapturaTeclas.Enabled = false;
        }

        private void ultraGrid_KeyDown(object sender, KeyEventArgs e)
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

        private void ultraGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    //solo se aceptan digitos y letras
            //    if (Char.IsLetterOrDigit(e.KeyChar))
            //    {
            //        //capturo las teclas presionadas mientras el control timer este Activo (500 milisegundos)
            //        if (timerCapturaTeclas.Enabled == true)
            //        {
            //            teclas += e.KeyChar.ToString();
            //        }
            //        //si el control esta inactivo, paso el control a activo y vuelvo a iniciar la captura
            //        else
            //        {
            //            teclas = e.KeyChar.ToString();
            //            timerCapturaTeclas.Enabled = true;
            //        }
            //        //celda seleccionada
            //        DataGridViewCell celda = grid.CurrentCell;
            //        int ini;
            //        //valido si esta en la ultima celda
            //        if (grid.CurrentRow.Index == (grid.Rows.Count - 1))
            //        {
            //            ini = 0;    //si esta en la ultima celda, inicia la busqueda desde la primera celda 
            //        }
            //        else
            //        {
            //            ini = grid.CurrentRow.Index + 1;    //inicia la busqueda desde la siguiente celda
            //        }
            //        //verifico si existen coincidencias desde la celda actual hasta la final
            //        for (int i = ini; i < grid.Rows.Count; i++)
            //        {
            //            if (grid.Rows[i].Cells[celda.ColumnIndex].Value.ToString().ToUpper().StartsWith(teclas.ToUpper()))
            //            {
            //                grid.CurrentCell = grid.Rows[i].Cells[celda.ColumnIndex];
            //                return;
            //            }
            //        }
            //        //verifico si existen coincidencias desde la primera celda hasta la actual
            //        for (int i = 0; i < ini; i++)
            //        {
            //            if (grid.Rows[i].Cells[celda.ColumnIndex].Value.ToString().ToUpper().StartsWith(teclas.ToUpper()))
            //            {
            //                grid.CurrentCell = grid.Rows[i].Cells[celda.ColumnIndex];
            //                return;
            //            }
            //        }
            //    }
            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void ultraGrid_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            enviarCodigo();
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = grid.DisplayLayout.Bands[0];

            grid.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            grid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            grid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            grid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            grid.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
            grid.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            grid.DisplayLayout.UseFixedHeaders = true;
        }
    }
}
