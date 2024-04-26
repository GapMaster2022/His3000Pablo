using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;

namespace His.Formulario
{
    public partial class frm_Ayuda_Certificado : Form
    {
        NegCertificadoMedico Certificado = new NegCertificadoMedico();
        public static string id_usuario; //Almacena el codigo del medico
        internal static bool patologo = false; //valida si es verdadero mostrara los patologos asignados
        internal static bool cie10 = false; //valida si es verdadero mostrara el cie10 
        internal static bool tarifafio = false; //levanta los tarifarios 
        internal static bool Personal = false;
        public string hc = "";
        public string ate_codigo = "";
        public bool emergencia = false;
        public bool mushugñan = false;
        public frm_Ayuda_Certificado()
        {
            InitializeComponent();
        }
        public Int16 AreaUsuario()
        {
            Int16 AreaUsuario = 1;
            DataTable codigoAreaAsignada = NegUsuarios.AreaAsignada(Convert.ToInt16(id_usuario));
            bool parse = Int16.TryParse(codigoAreaAsignada.Rows[0][0].ToString(), out AreaUsuario);
            if (parse)
            {
                return AreaUsuario;
            }
            else
                return AreaUsuario;
        }
        private void frm_Ayuda_Certificado_Load(object sender, EventArgs e)
        {
            if(patologo == false && cie10 == false && tarifafio == false && Personal == false)
            {
                if (mushugñan)
                {
                    Int16 _AreaAsignada = AreaUsuario();
                    switch (_AreaAsignada)
                    {
                        case 1:
                            CargarPacientes();
                            break;
                        case 2:
                            MushugñanPacientes();
                            break;
                        case 3:
                            BrigadaPacientes();
                            break;
                        case 4:
                            TodosPacientes();
                            break;
                        default:
                            CargarPacientes();
                            break;
                    }
                }
                else
                    CargarPacientes();
            }
            else if(patologo == true)
            {
                CargarPatologos();
                if (Pacientes.Rows.Count <= 0)
                {
                    MessageBox.Show("No se han encontrado Médicos Patologos asignados.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                }
            }
            else if(cie10 == true)
            {
                CargarCie10();
            }
            else if(tarifafio == true)
            {
                CargarTarifario();
            }
            else if(Personal == true)
            {
                CargarPersonal();
            }
        }

        public void CargarPersonal()
        {
            try
            {
                Pacientes.DataSource = NegMedicos.Personal();

                UltraGridBand bandUno = Pacientes.DisplayLayout.Bands[0];



                Pacientes.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                Pacientes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                Pacientes.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                Pacientes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                Pacientes.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                Pacientes.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                Pacientes.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;


                Pacientes.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

                //Caracteristicas de Filtro en la grilla
                Pacientes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                Pacientes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                Pacientes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                Pacientes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                Pacientes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                Pacientes.DisplayLayout.UseFixedHeaders = true;


                Pacientes.DisplayLayout.Bands[0].Columns[0].Width = 80;
                Pacientes.DisplayLayout.Bands[0].Columns[1].Width = 300;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CargarPacienteEmergencia()
        {
            try
            {
                Pacientes.DataSource = NegMedicos.Personal();

                UltraGridBand bandUno = Pacientes.DisplayLayout.Bands[0];



                Pacientes.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                Pacientes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                Pacientes.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                Pacientes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                Pacientes.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                Pacientes.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                Pacientes.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;


                Pacientes.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

                //Caracteristicas de Filtro en la grilla
                Pacientes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                Pacientes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                Pacientes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                Pacientes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                Pacientes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                Pacientes.DisplayLayout.UseFixedHeaders = true;


                Pacientes.DisplayLayout.Bands[0].Columns[0].Width = 80;
                Pacientes.DisplayLayout.Bands[0].Columns[1].Width = 300;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CargarTarifario()
        {
            try
            {
                NegMedicos medicos = new NegMedicos();
                Pacientes.DataSource = medicos.Tarifario();

                UltraGridBand bandUno = Pacientes.DisplayLayout.Bands[0];



                Pacientes.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                Pacientes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                Pacientes.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                Pacientes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                Pacientes.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                Pacientes.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                Pacientes.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;


                Pacientes.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

                //Caracteristicas de Filtro en la grilla
                Pacientes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                Pacientes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                Pacientes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                Pacientes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                Pacientes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                Pacientes.DisplayLayout.UseFixedHeaders = true;


                Pacientes.DisplayLayout.Bands[0].Columns[0].Width = 400;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CargarCie10()
        {
            try
            {
                NegMedicos medicos = new NegMedicos();
                Pacientes.DataSource = medicos.Cie10();

                UltraGridBand bandUno = Pacientes.DisplayLayout.Bands[0];



                Pacientes.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                Pacientes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                Pacientes.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                Pacientes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                Pacientes.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                Pacientes.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                Pacientes.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;


                Pacientes.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

                //Caracteristicas de Filtro en la grilla
                Pacientes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                Pacientes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                Pacientes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                Pacientes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                Pacientes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                Pacientes.DisplayLayout.UseFixedHeaders = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CargarPacientes()
        {
            try
            {
                if (!emergencia)
                    Pacientes.DataSource = Certificado.Medico_Pacientes();
                else
                    Pacientes.DataSource = NegMedicos.CargarPacienteEmergencia();
                Redimensionar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void MushugñanPacientes()
        {
            try
            {
                Pacientes.DataSource = NegCertificadoMedico.PacientesMushugñan();
                Redimensionar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void BrigadaPacientes()
        {
            try
            {
                Pacientes.DataSource = NegCertificadoMedico.Medico_PacienteBrigada();
                Redimensionar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void TodosPacientes()
        {
            try
            {
                Pacientes.DataSource = NegCertificadoMedico.Medico_PacienteTodos();
                Redimensionar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CargarPatologos()
        {
            try
            {
                NegQuirofano Ayuda = new NegQuirofano();
                Pacientes.DataSource = Ayuda.Patologos();
                UltraGridBand bandUno = Pacientes.DisplayLayout.Bands[0];



                Pacientes.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                Pacientes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                Pacientes.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                Pacientes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                Pacientes.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                Pacientes.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                Pacientes.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;


                Pacientes.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

                //Caracteristicas de Filtro en la grilla
                Pacientes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                Pacientes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                Pacientes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                Pacientes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                Pacientes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                Pacientes.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                Pacientes.DisplayLayout.Bands[0].Columns[0].Width = 60;
                Pacientes.DisplayLayout.Bands[0].Columns[1].Width = 400;
                Pacientes.DisplayLayout.Bands[0].Columns[2].Width = 200;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Redimensionar()
        {
            try
            {
                UltraGridBand bandUno = Pacientes.DisplayLayout.Bands[0];

                Pacientes.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                Pacientes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                Pacientes.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                Pacientes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                Pacientes.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                Pacientes.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                Pacientes.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;


                Pacientes.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

                //Caracteristicas de Filtro en la grilla
                Pacientes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                Pacientes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                Pacientes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                Pacientes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                Pacientes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                Pacientes.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                Pacientes.DisplayLayout.Bands[0].Columns[0].Width = 100;
                Pacientes.DisplayLayout.Bands[0].Columns[1].Width = 100;
                Pacientes.DisplayLayout.Bands[0].Columns[2].Width = 100;
                Pacientes.DisplayLayout.Bands[0].Columns[3].Width = 250;
                Pacientes.DisplayLayout.Bands[0].Columns[4].Width = 100;


                //Ocultar columnas, que son fundamentales al levantar informacion

                //ultraGridHonorarios.DisplayLayout.Bands[0].Columns[9].Hidden = true;
                Pacientes.DisplayLayout.Bands[0].Columns[5].Hidden = true;
                Pacientes.DisplayLayout.Bands[0].Columns[6].Hidden = true;
                Pacientes.DisplayLayout.Bands[0].Columns[7].Hidden = true;
                Pacientes.DisplayLayout.Bands[0].Columns[8].Hidden = true;
                Pacientes.DisplayLayout.Bands[0].Columns[9].Hidden = true;
                //ultraGridHonorarios.DisplayLayout.Bands[0].Columns[6].Hidden = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Pacientes_DoubleClick_1(object sender, EventArgs e)
        {
            try
            {
                if(Pacientes.Selected.Rows.Count == 1)
                {
                    UltraGridRow Fila = Pacientes.ActiveRow;
                    if (patologo == false && cie10 == false && tarifafio == false && Personal == false)
                    {
                        frm_Certificados.hc = Fila.Cells[0].Value.ToString();
                        frm_Certificados.fechaingreso = Fila.Cells[4].Value.ToString();
                        frm_Certificados.ate_codigo = Fila.Cells[5].Value.ToString();
                        frm_Certificados.fechaalta = Fila.Cells[6].Value.ToString();
                        frm_Certificados.med_email = Fila.Cells[7].Value.ToString();
                        frm_Certificados.med_identificacion = Fila.Cells[8].Value.ToString();
                        frm_Certificados.nom_medico = Fila.Cells[9].Value.ToString();

                        frm_CertificadoIESS.hc = Fila.Cells[0].Value.ToString();
                        frm_CertificadoIESS.fechaingreso = Fila.Cells[4].Value.ToString();
                        frm_CertificadoIESS.ate_codigo = Fila.Cells[5].Value.ToString();
                        frm_CertificadoIESS.fechaalta = Fila.Cells[6].Value.ToString();
                        frm_CertificadoIESS.med_email = Fila.Cells[7].Value.ToString();
                        frm_CertificadoIESS.med_identificacion = Fila.Cells[8].Value.ToString();
                        frm_CertificadoIESS.nom_medico = Fila.Cells[9].Value.ToString();

                        ate_codigo = Fila.Cells[5].Value.ToString();
                        hc = Fila.Cells[0].Value.ToString();
                        ate_codigo = Fila.Cells[1].Value.ToString();
                        this.Close();
                    }
                    else if (patologo == true)
                    {
                        //if(Pacientes.Rows.Count < 0)
                        //{
                        //    MessageBox.Show("No se han encontrado Médicos Patologos asignados.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //    this.Close();
                        //}
                        //else
                        //{
                        frm_Protocolo.medicopatologo = Fila.Cells[1].Value.ToString();
                        this.Close();
                        //}
                    }
                    else if (cie10 == true)
                    {
                        frm_Protocolo.cie10 = Fila.Cells[0].Value.ToString();
                        this.Close();
                    }

                    else if (tarifafio == true)
                    {
                        frm_Protocolo.tarifario = Fila.Cells[0].Value.ToString();
                        this.Close();
                    }
                    else if (Personal == true)
                    {
                        frm_Protocolo.personal = Fila.Cells[1].Value.ToString();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
