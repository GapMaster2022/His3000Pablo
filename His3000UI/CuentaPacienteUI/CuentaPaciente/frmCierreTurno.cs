using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Negocio;
using His.Parametros;
using Infragistics.Win.UltraWinGrid;
using Recursos;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using frmImpresionPedidos = His.HabitacionesUI.frmImpresionPedidos;

namespace CuentaPaciente
{
    public partial class frmCierreTurno : Form
    {

        DataTable dtUsuarios = new DataTable();
        DataTable dtVerifica = new DataTable();

        public frmCierreTurno()
        {
            InitializeComponent();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtVerifica = NegFactura.VerificaCierreTurno(Convert.ToInt32(this.cmbCajeros.SelectedValue.ToString()), dateTimePicker1.Value.Date);

            if (dtVerifica.Rows.Count > 0)
            {
                MessageBox.Show("El cierre de turno ya fue realizado.");
                dataGridView1.DataSource = dtVerifica;

                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[4].ReadOnly = true;
                dataGridView1.Columns[5].ReadOnly = true;
                dataGridView1.Columns[6].ReadOnly = true;
                dataGridView1.Columns[8].ReadOnly = true;
                dataGridView1.Columns[9].ReadOnly = true;
                dataGridView1.Columns[10].ReadOnly = true;
                dataGridView1.Columns[11].ReadOnly = true;

                return;

            }
            else
            {
                dtVerifica = null;
            }

            DataTable dt = new DataTable();
            dt = NegPedidos.CierreReporte(dateTimePicker1.Value.Date, cmbCajeros.SelectedValue.ToString());
            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[5].ReadOnly = true;
            dataGridView1.Columns[6].ReadOnly = true;
            dataGridView1.Columns[7].ReadOnly = true;
            dataGridView1.Columns[8].ReadOnly = true;
            dataGridView1.Columns[6].Visible = false;

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dtVerifica == null) // verifica si es un nuevo cierre de turno
            {
                if ((MessageBox.Show("Esta seguro de generar el cierre de turno?", "HIS3000", MessageBoxButtons.YesNo) == DialogResult.No))
                {
                    return;
                }

                List<DtoCierreTurno> ListaCierre = new List<DtoCierreTurno>();
                for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {

                    if (dataGridView1.Rows[i].Cells[1].Value != null)
                    {

                        DtoCierreTurno Cierre = new DtoCierreTurno();

                        Cierre.Fecha = Convert.ToDateTime(dataGridView1.Rows[i].Cells[0].Value);
                        Cierre.PAC_HISTORIA_CLINICA = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        Cierre.ATE_NUMERO_ATENCION = dataGridView1.Rows[i].Cells[2].Value.ToString();
                        Cierre.hab_Numero = dataGridView1.Rows[i].Cells[3].Value.ToString();
                        Cierre.PACIENTE = dataGridView1.Rows[i].Cells[4].Value.ToString();
                        Cierre.CATEGORIA = dataGridView1.Rows[i].Cells[5].Value.ToString();
                        Cierre.CAJERO_NOMBRE = dataGridView1.Rows[i].Cells[6].Value.ToString();
                        Cierre.CAJERO_CODIGO = Convert.ToInt32(this.cmbCajeros.SelectedValue.ToString());
                        Cierre.ESTADO = dataGridView1.Rows[i].Cells[7].Value.ToString();
                        Cierre.ID_USUARIO = Convert.ToInt32(His.Entidades.Clases.Sesion.codUsuario);
                        Cierre.MEDICO_TURNO = dataGridView1.Rows[i].Cells[8].Value.ToString();
                        Cierre.OBSERVACION = dataGridView1.Rows[i].Cells[9].Value.ToString();

                        ListaCierre.Add(Cierre);
                        Cierre = null;
                    }

                }

                NegPedidos.GuardaCierreTurno(ListaCierre);

            }

            ImprimirCierreTurno();

        }

        private void frmCierreTurno_Load(object sender, EventArgs e)
        {
            dtUsuarios = NegUsuarios.RecuperaUsuariosCajeros();

            cmbCajeros.DataSource = dtUsuarios;
            cmbCajeros.ValueMember = "USUARIO";
            cmbCajeros.DisplayMember = "CAJERO";


        }

        private void ImprimirCierreTurno()
        {
            /*GENERA EL REPORTE*/

            DataTable ds1 = new DataTable();
            dtsDesgloseFactura ds2 = new dtsDesgloseFactura();
            DataRow dr2;

            ds1 = NegFactura.GeneraCierreTurno(Convert.ToInt32(this.cmbCajeros.SelectedValue.ToString()), dateTimePicker1.Value.Date);

            if (ds1 != null && ds1.Rows.Count > 0)
            {
                foreach (DataRow dr1 in ds1.Rows)
                {
                    dr2 = ds2.Tables["CierreTurno"].NewRow();

                    dr2["Fecha"] = dr1["Fecha"];
                    dr2["FechaCierre"] = dr1["FechaCierre"];
                    dr2["PAC_HISTORIA_CLINICA"] = dr1["PAC_HISTORIA_CLINICA"];
                    dr2["ATE_NUMERO_ATENCION"] = dr1["ATE_NUMERO_ATENCION"];
                    dr2["hab_Numero"] = dr1["hab_Numero"];
                    dr2["PACIENTE"] = dr1["PACIENTE"];
                    dr2["CATEGORIA"] = dr1["CATEGORIA"];
                    dr2["CAJERO_NOMBRE"] = dr1["CAJERO_NOMBRE"];
                    dr2["CAJERO_CODIGO"] = dr1["CAJERO_CODIGO"];
                    dr2["ESTADO"] = dr1["ESTADO"];
                    dr2["USUARIO"] = dr1["USUARIO"];
                    dr2["MEDICO_TRATANTE"] = dr1["MEDICO TRATANTE"];
                    dr2["OBSERVACION"] = dr1["OBSERVACION"];

                    ds2.Tables["CierreTurno"].Rows.Add(dr2);
                }

                ReportDocument reporte = new ReportDocument();
                reporte.FileName = Application.StartupPath + "\\Reportes\\CuentasPacientes\\rptCierreTurno.rpt";
                reporte.Database.Tables["CierreTurno"].SetDataSource(ds2.Tables["CierreTurno"]);

                reporte.PrintToPrinter(1, false, 1, 1);


            }
        }
    }
}