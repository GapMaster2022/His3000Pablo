using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using His.Negocio;
using System.Data;
using System.Data.SqlClient;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Entidades.Clases;

namespace CuentaPaciente
{
    public partial class FrmImpresionDatosCuentas : Form
    {

        DataTable _Valores = new DataTable();
        String _Reporte;

        public FrmImpresionDatosCuentas(DataTable Valores, String Reporte)
        {
            _Valores = Valores;
            _Reporte = Reporte;

            InitializeComponent();
        }

        private void FrmImpresionDatosCuentas_Load(object sender, EventArgs e)
        {
            if (_Reporte == "ValoresCuentas")
            {

                DataTable ds1 = new DataTable();
                DataTable ds3 = new DataTable();
                dtsDesgloseFactura ds2 = new dtsDesgloseFactura();
                DataRow dr2;
                                
                string fact="";
                string forma="";
                ds1 = _Valores;
                string AUX = "";
                if (ds1 != null && ds1.Rows.Count > 0)
                {                    
                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        if (dr1["ATE_FACTURA_PACIENTE"].ToString() != "")
                            if (Convert.ToDecimal(dr1["CUE_CANTIDAD"].ToString()) != 0)
                            {
                                if (fact.ToString() != dr1["ATE_FACTURA_PACIENTE"].ToString())
                                {
                                    fact = dr1["ATE_FACTURA_PACIENTE"].ToString();
                                    ds3 = NegCuentasPacientes.FormaPagoSic(fact);
                                    dr2 = ds2.Tables["DatosCuentas"].NewRow();
                                    int count = 0;
                                    forma = "";
                                    if (ds3 != null && ds3.Rows.Count > 0 && count == 0)
                                    {
                                        foreach (DataRow dr3 in ds3.Rows)
                                        {
                                            forma = forma+dr3["FormaPago"] + " - ";
                                        }
                                    }
                                }
                                    //fact = dr1["ATE_FACTURA_PACIENTE"].ToString();
                                    //ds3 = NegCuentasPacientes.FormaPagoSic(fact);
                                    //dr2 = ds2.Tables["DatosCuentas"].NewRow();
                                    /*int count =0;
                                    if (ds3 != null && ds3.Rows.Count > 0 && count == 0)
                                    {
                                        foreach (DataRow dr3 in ds3.Rows)
                                        {
                                            dr2 = ds2.Tables["DatosCuentas"].NewRow();
                                            dr2["PAC_HISTORIA_CLINICA"] = dr3["FormaPago"];
                                            dr2["ATE_NUMERO_ATENCION"] = dr1["ATE_FACTURA_PACIENTE"];
                                            dr2["NOMBRES"] = dr1["NOMBRES"];
                                            dr2["MEDICO"] = dr1["MEDICO"];
                                            dr2["PRO_CODIGO_BARRAS"] = dr1["PRO_CODIGO_BARRAS"];
                                            dr2["DETALLE"] = dr1["DETALLE"];
                                            dr2["CUE_VALOR_UNITARIO"] = dr1["CUE_VALOR_UNITARIO"];
                                            dr2["CUE_CANTIDAD"] = dr1["CUE_CANTIDAD"];
                                            dr2["CUE_IVA"] = dr1["CUE_IVA"];
                                            dr2["CUE_VALOR"] = dr1["CUE_VALOR"];
                                            dr2["CATEGORIA"] = dr1["CATEGORIA"];
                                            dr2["AREA"] = dr1["AREA"];
                                            dr2["RUBRO"] = dr1["RUBRO"];
                                            dr2["FECHA_FACTURA"]=dr1["FECHA"];
                                            ds2.Tables["DatosCuentas"].Rows.Add(dr2);
                                        }
                                    }
                                

                                    dr2 = ds2.Tables["DatosCuentas"].NewRow();
                                    //dr2["PAC_HISTORIA_CLINICA"] = dr3["FormaPago"];
                                    dr2["ATE_NUMERO_ATENCION"] = dr1["ATE_FACTURA_PACIENTE"];
                                    dr2["NOMBRES"] = dr1["NOMBRES"];
                                    dr2["MEDICO"] = dr1["MEDICO"];
                                    dr2["PRO_CODIGO_BARRAS"] = dr1["PRO_CODIGO_BARRAS"];
                                    dr2["DETALLE"] = dr1["DETALLE"];
                                    dr2["CUE_VALOR_UNITARIO"] = dr1["CUE_VALOR_UNITARIO"];
                                    dr2["CUE_CANTIDAD"] = dr1["CUE_CANTIDAD"];
                                    dr2["CUE_IVA"] = dr1["CUE_IVA"];
                                    dr2["CUE_VALOR"] = dr1["CUE_VALOR"];
                                    dr2["CATEGORIA"] = dr1["CATEGORIA"];
                                    dr2["AREA"] = dr1["AREA"];
                                    dr2["RUBRO"] = dr1["RUBRO"];
                                    //count = 1;
                                    ds2.Tables["DatosCuentas"].Rows.Add(dr2);*/
                                    dr2 = ds2.Tables["DatosCuentas"].NewRow();
                                    dr2["PAC_HISTORIA_CLINICA"] = forma;
                                    dr2["ATE_NUMERO_ATENCION"] = dr1["ATE_FACTURA_PACIENTE"];
                                    dr2["NOMBRES"] = dr1["NOMBRES"];
                                    dr2["MEDICO"] = dr1["MEDICO"];
                                    dr2["PRO_CODIGO_BARRAS"] = dr1["PRO_CODIGO_BARRAS"];
                                    dr2["DETALLE"] = dr1["DETALLE"];
                                    dr2["CUE_VALOR_UNITARIO"] = dr1["CUE_VALOR_UNITARIO"];
                                    dr2["CUE_CANTIDAD"] = dr1["CUE_CANTIDAD"];
                                    dr2["CUE_IVA"] = dr1["CUE_IVA"];
                                    dr2["CUE_VALOR"] = dr1["CUE_VALOR"];
                                    dr2["NUMVALE"] = dr1["NumVale"];
                                    dr2["CATEGORIA"] = dr1["CATEGORIA"];
                                    AUX = Convert.ToString(dr1["ATE_REFERIDO"]);
                                    if (Convert.ToString(dr1["ATE_REFERIDO"]) == "True")
                                        dr2["REFERIDO"] = "I";
                                    else
                                        dr2["REFERIDO"] = "P";
                                    dr2["AREA"] = dr1["AREA"];
                                    dr2["RUBRO"] = dr1["RUBRO"];
                                    dr2["FECHA_FACTURA"] = dr1["FECHA"];
                                    ds2.Tables["DatosCuentas"].Rows.Add(dr2);

                                }
                            
                            
                    }

                    ReportDocument reporte = new ReportDocument();
                    reporte.FileName = Application.StartupPath + "\\Reportes\\CuentasPacientes\\rptValoresCuentas.rpt";
                    reporte.Database.Tables["DatosCuentas"].SetDataSource(ds2.Tables["DatosCuentas"]);

                    crystalReportViewer1.ReportSource = reporte;
                    crystalReportViewer1.Refresh();

                }
            }
        }
    }
}
