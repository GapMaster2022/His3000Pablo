using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Reportes;
using His.Negocio;

namespace CuentaPaciente
{
    public partial class EstadoFechas : Form
    {
        string paciente = "";
        Int32 ateCodigo = 0;
        string pacHistoria = "";
        int CodigoAseguradora = 0;

        ATENCIONES obj = new ATENCIONES();
        PACIENTES objPac = new PACIENTES();
        public EstadoFechas(string _paciente, Int32 _ateCodigo, string _pacHistoria, int _codigoAseguradora)
        {
            InitializeComponent();
            CodigoAseguradora = _codigoAseguradora;
            paciente = _paciente;
            lblPaciente.Text = _paciente;
            ateCodigo = _ateCodigo;
            pacHistoria = _pacHistoria;

            obj = NegAtenciones.AtencionID(_ateCodigo);
            f_Ingreso.Value = obj.ATE_FECHA_INGRESO.Value;
            try
            {
                f_Alta.Value = obj.ATE_FECHA_ALTA.Value;
            }
            catch
            {
                f_Alta.Value = DateTime.Now;
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (f_Ingreso.Value.Date < obj.ATE_FECHA_INGRESO.Value.Date)
            {
                MessageBox.Show("La fecha Inicial no puede ser menor a la fecha de ingreso del paciente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                if (obj.ATE_FECHA_ALTA != null)
                {
                    if (f_Alta.Value < f_Ingreso.Value)
                    {
                        MessageBox.Show("La fecha Fin no puede ser mayor a la fecha actual", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (f_Alta.Value < f_Ingreso.Value)
                    {
                        MessageBox.Show("La fecha Fin no puede ser menor a la fecha de ingreso", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    if (obj.ATE_FECHA_ALTA > DateTime.Now)
                    {
                        MessageBox.Show("La fecha Fin no puede ser mayor a la fecha actual", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (f_Alta.Value.Date < f_Ingreso.Value.Date)
                    {
                        MessageBox.Show("La fecha Fin no puede ser menor a la fecha de ingreso", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("La fecha Fin no puede ser mayor a la fecha actual", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ReporteFactura reporteFactura = new ReporteFactura();
            objPac = NegPacientes.RecuperarPacienteID(pacHistoria);
            string nombrePaciente = objPac.PAC_APELLIDO_PATERNO + " " + objPac.PAC_APELLIDO_MATERNO + " " + objPac.PAC_NOMBRE1 + " " + objPac.PAC_NOMBRE2;

            if (chb_Valores.Checked)
                ValoresAutomaticos();

            if (obj.ESC_CODIGO == 6)
            {
                frmReporteDesgloseFactura FACTURA = new frmReporteDesgloseFactura(obj.ATE_FACTURA_PACIENTE, nombrePaciente, objPac.PAC_IDENTIFICACION, "", "", obj.ATE_CODIGO, "ESTADO_CUENTA_X_Fecha", "", 0, f_Ingreso.Value.ToString(), f_Alta.Value.ToString(), 0, 0, "ESTADO DE CUENTA SIN VALIDES TRIBUTARIA", "", "", "", "", "", "", "");
                FACTURA.Show();
                if ((MessageBox.Show("Desea imprimir el desglose de articulos del Estado de Cuenta?", "HIS3000", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    frmReporteDesgloseFactura DesgloseFactura = new frmReporteDesgloseFactura(obj.ATE_FACTURA_PACIENTE, nombrePaciente, objPac.PAC_IDENTIFICACION, "", "", obj.ATE_CODIGO, "FACTURAxFECHA", "", 0, f_Ingreso.Value.ToString(), f_Alta.Value.ToString(), 0, 0, "ESTADO DE CUENTA SIN VALIDES TRIBUTARIA", "", "", "", "", "", "", "");
                    DesgloseFactura.Show();
                }
            }
            else
            {
                frmReporteDesgloseFactura FACTURA = new frmReporteDesgloseFactura("CUENTA SIN FACTURA", nombrePaciente, objPac.PAC_IDENTIFICACION, "", "", obj.ATE_CODIGO, "ESTADO_CUENTA_X_Fecha", "", 0, f_Ingreso.Value.ToString(), f_Alta.Value.ToString(), 0, 0, "ESTADO DE CUENTA SIN VALIDES TRIBUTARIA", "", "", "", "", "", "", "");
                FACTURA.Show();
                if ((MessageBox.Show("Desea imprimir el desglose de articulos del Estado de Cuenta?", "HIS3000", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    frmReporteDesgloseFactura DesgloseFactura = new frmReporteDesgloseFactura("CUENTA SIN FACTURA", nombrePaciente, objPac.PAC_IDENTIFICACION, "", "", obj.ATE_CODIGO, "FACTURAxFECHA", "", 0, f_Ingreso.Value.ToString(), f_Alta.Value.ToString(), 0, 0, "ESTADO DE CUENTA SIN VALIDES TRIBUTARIA", "", "", "", "", "", "", "");
                    DesgloseFactura.Show();
                }
            }            
            this.Close();
        }

        public void ValoresAutomaticos()
        {
            DataTable UCI = new DataTable();
            int totalDiasUCI = 0;
            UCI = NegCuentasPacientes.ValoresHabitacionUCIxFecha(ateCodigo, f_Ingreso.Value, f_Alta.Value);
            TimeSpan difFechas = DateTime.Now - DateTime.Now;
            if (UCI.Rows.Count > 0)
            {
                for (int i = 0; i < UCI.Rows.Count; i++)
                {
                    if (UCI.Rows[i][1].ToString() != "")
                        difFechas += Convert.ToDateTime(UCI.Rows[i][1].ToString()) - Convert.ToDateTime(UCI.Rows[i][0].ToString());
                    else
                        difFechas += DateTime.Now - Convert.ToDateTime(UCI.Rows[i][0].ToString());
                }
                totalDiasUCI = Convert.ToInt16(difFechas.Days) + 1;
                NegCuentasPacientes.GuardaTotalUCIxFecha(ateCodigo, totalDiasUCI, CodigoAseguradora);
            }
            DataTable habitacion = new DataTable();
            habitacion = NegCuentasPacientes.ValoresHabitacionxFecha(ateCodigo, f_Ingreso.Value, f_Alta.Value);
            string habit = "";
            if (habitacion.Rows.Count > 0)
            {
                for (int i = 0; i < habitacion.Rows.Count; i++)
                {
                    DataTable habita = new DataTable();
                    habita = NegCuentasPacientes.ValidaHabitacion(CodigoAseguradora, habitacion.Rows[i][2].ToString());
                    if (habita.Rows.Count > 0)
                    {
                        habit = habita.Rows[0][1].ToString();
                        break;
                    }
                }
            }
            difFechas = f_Alta.Value.Date - f_Ingreso.Value.Date;
            int dias = difFechas.Days + 1;
            if (habit != "")
                NegCuentasPacientes.GeneraValoresautomaticosxFechas(ateCodigo, His.Entidades.Clases.Sesion.codUsuario, dias - totalDiasUCI, habit, CodigoAseguradora, 0);
            else
                NegCuentasPacientes.AdministracionMedicamentosxFechas(ateCodigo, His.Entidades.Clases.Sesion.codUsuario, CodigoAseguradora, 0);

        }
    }
}
