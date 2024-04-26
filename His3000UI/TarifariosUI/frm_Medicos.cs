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

namespace TarifariosUI
{
    public partial class frm_Medicos : Form
    {
        public TextBox campoPadre = null;
        public TextBox campoPadre2 = null;
        public MaskedTextBox campoEspecial = null;
        public string tabla;
        public int columnabuscada;
        public string colRetorno;
        public string colRetorno2;
        //cadena de caracteres que acumula las teclas pulsadas en un segundo
        public string teclas = "";
        public frm_Medicos()
        {
            InitializeComponent();
        }
          public frm_Medicos(List<PACIENTES> pacientes)
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

        public frm_Medicos(List<MEDICOS> medicos)
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

        public frm_Medicos(List<DtoAtenciones> atenciones)
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

        public frm_Medicos(List<HABITACIONES> habitaciones)
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

        public frm_Medicos(List<DtoMedicos> dtoMedicos)
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

        public frm_Medicos(List<DtoHonorariosMedicos> dtoHonorarios)
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

        public frm_Medicos(List<DtoUsuarios> dtoUsuarios)
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

        private void grid_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
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

        private void grid_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void enviarCodigo()
        {
            if (grid.ActiveRow.Index > -1)
            {
                if (campoPadre != null)
                    campoPadre.Text = grid.ActiveRow.Cells[colRetorno].Value.ToString();

                if (campoEspecial != null)
                    campoEspecial.Text = grid.ActiveRow.Cells[colRetorno].Value.ToString();

                if (campoPadre2 != null && colRetorno2 != null)
                    campoPadre2.Text = grid.ActiveRow.Cells[colRetorno2].Value.ToString();
                this.Close();
            }
        }
        private void frm_Medicos_Load(object sender, EventArgs e)
        {
            if (tabla == "MEDICOS")
            {
                if (grid.ActiveRow != null)
                    grid.ActiveCell = grid.ActiveRow.Cells["NOMBRE"];
            }

        }

        private void timerCapturaTeclas_Tick(object sender, EventArgs e)
        {
            timerCapturaTeclas.Enabled = false;
        }
    }
}
