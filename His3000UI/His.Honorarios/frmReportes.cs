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


namespace His.Honorarios
{
    partial class frmReportes : Form
    {
        #region Variables
        public string reporte;
        public string campo1;
        public string campo2;
        public string campo3;
        public string campo4;
        public string campo5;
        public string campo6;

        public DataSet Datos;
        #endregion

        #region Constructor
        public frmReportes()
        {
            InitializeComponent();

        }

        public frmReportes(string reporte, DataSet datos)
        {
            this.reporte = reporte;
            this.Datos = datos;
            InitializeComponent();
        }
        #endregion

        #region Descriptores de acceso de atributos de ensamblado

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void frmReportes_Load(object sender, EventArgs e)
        {
            try
            {
                if (reporte == "rNotaCreditoDebitoInterna")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rNotaCreditoDebitoInterna.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["not_numero"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo2;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["tid_codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo3;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["titulo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rBalancePagosCancelados")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rBalancePagosCancelados.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["fecini"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo2;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["fecfin"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo3;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["med_codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rBalancePagosRealizados")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rBalancePagosRealizados.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["fecini"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo2;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["fecfin"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo3;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["med_codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo4;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["estado"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo5;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["titulo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rBalanceApagarOpagado")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rBalanceApagarOpagado.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["fecini"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo2;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["fecfin"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo3;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["med_codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo4;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["pagado"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo5;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["titulo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rRetencionAutReimpresion")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rRetencionAutReimpresion.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["fecini"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo2;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["fecfin"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo3;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["tid_codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rRetencionGeneral")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rRetencionGeneral.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["fecini"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo2;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["fecfin"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo3;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["med_ruc"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rNotasDAutReimpresion")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rNotasDAutReimpresion.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["fecini"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo2;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["fecfin"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo3;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["tid_codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();

                }
                else if (reporte == "rNotaNDGeneral")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rNotaNDGeneral.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["tid_codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo2;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["fecini"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo3;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["fecfin"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo4;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["med_ruc"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rMedicosTipoH")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rMedicosTipoH.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["tih_codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    //crParameterDiscreteValue.Value = textBox2.Text;
                    //crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    //crParameterFieldDefinition = crParameterFieldDefinitions["toDate"];
                    //crParameterValues = crParameterFieldDefinition.CurrentValues;

                    //crParameterValues.Add(crParameterDiscreteValue);
                    //crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rMedicosTipoM")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rMedicosTipoM.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["tim_codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    //crParameterDiscreteValue.Value = textBox2.Text;
                    //crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    //crParameterFieldDefinition = crParameterFieldDefinitions["toDate"];
                    //crParameterValues = crParameterFieldDefinition.CurrentValues;

                    //crParameterValues.Add(crParameterDiscreteValue);
                    //crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rMedicosEspecialidad")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rMedicosEspecialidad.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["esp_codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    //crParameterDiscreteValue.Value = textBox2.Text;
                    //crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    //crParameterFieldDefinition = crParameterFieldDefinitions["toDate"];
                    //crParameterValues = crParameterFieldDefinition.CurrentValues;

                    //crParameterValues.Add(crParameterDiscreteValue);
                    //crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rMedicosTodos")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rMedicosTodos.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rMedicoFicha1")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rMedicoFicha.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();



                    //crParameterDiscreteValue.Value = textBox2.Text;
                    //crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    //crParameterFieldDefinition = crParameterFieldDefinitions["toDate"];
                    //crParameterValues = crParameterFieldDefinition.CurrentValues;

                    //crParameterValues.Add(crParameterDiscreteValue);
                    //crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["med_codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();

                }

                else if (reporte == "rMedicoFicha")
                {

                    DataTable ds1 = new DataTable();
                    dst_honorarios ds2 = new dst_honorarios();
                    DataRow dr2;

                    ds1 = NegHonorariosMedicos.RecuperaFichaMedico(Convert.ToInt32(campo1));
                    string path = NegUtilitarios.RutaLogo("General");
                    if (ds1 != null && ds1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in ds1.Rows)
                        {
                            dr2 = ds2.Tables["dstFichaMedico"].NewRow();

                            dr2["MED_CODIGO"] = dr1["MED_CODIGO"];
                            dr2["DATOS"] = dr1["DATOS"];
                            dr2["RUC"] = dr1["RUC"];
                            dr2["FECHA_NACIMIENTO"] = dr1["FECHA_NACIMIENTO"];
                            dr2["GENERO"] = dr1["GENERO"];
                            dr2["MAIL"] = dr1["MAIL"];
                            dr2["FechaIngreso"] = dr1["FechaIngreso"];
                            dr2["DIRECCION"] = dr1["DireccionConsultorio"];
                            dr2["DireccionConsultorio"] = dr1["OBSERVACION"];
                            dr2["Especialidad"] = dr1["Especialidad"];
                            dr2["TelefonoCasa"] = dr1["TelefonoCasa"];
                            dr2["TelefonoConsultorio"] = dr1["TelefonoConsultorio"];
                            dr2["TelefonoCelular"] = dr1["TelefonoCelular"];
                            dr2["TipoMedico"] = dr1["TipoMedico"];
                            dr2["TipoHonorario"] = dr1["TipoHonorario"];
                            dr2["BANCO"] = dr1["BANCO"];
                            dr2["CuentaMedico"] = dr1["CuentaMedico"];
                            dr2["Tipo_Cuenta"] = dr1["Tipo_Cuenta"];
                            dr2["CuentaContable"] = dr1["CuentaContable"];
                            dr2["AutorizacionSri"] = dr1["AutorizacionSri"];
                            dr2["ValidezAutorizacion"] = dr1["ValidezAutorizacion"];
                            dr2["FacturaInicial"] = dr1["FacturaInicial"];
                            dr2["FacturaFinal"] = dr1["FacturaFinal"];
                            dr2["Retencion"] = dr1["Retencion"];
                            dr2["Porcentaje"] = dr1["Porcentaje"];
                            dr2["MED_ESTADO"] = dr1["CIVIL"];
                            dr2["path"] = path;

                            ds2.Tables["dstFichaMedico"].Rows.Add(dr2);
                        }

                        ReportDocument reporte1 = new ReportDocument();
                        reporte1.FileName = Application.StartupPath + "\\Reportes\\Honorarios\\rptFichaMedico.rpt";
                        reporte1.Database.Tables["dstFichaMedico"].SetDataSource(ds2.Tables["dstFichaMedico"]);

                        crystalReportViewer1.ReportSource = reporte1;
                        crystalReportViewer1.Refresh();
                    }

                }

                else if (reporte == "rNotaCreditoDebito")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rNotaCreditoDebito.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["Not_numero"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo2;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["tid_codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();

                }
                else if (reporte == "rRetenciones")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rRetenciones.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["ret_codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    //crParameterDiscreteValue.Value = textBox2.Text;
                    //crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    //crParameterFieldDefinition = crParameterFieldDefinitions["toDate"];
                    //crParameterValues = crParameterFieldDefinition.CurrentValues;

                    //crParameterValues.Add(crParameterDiscreteValue);
                    //crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rRetencionesAutomaticas")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rRetencionesAutomaticas.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["gen_codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    //crParameterDiscreteValue.Value = textBox2.Text;
                    //crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    //crParameterFieldDefinition = crParameterFieldDefinitions["toDate"];
                    //crParameterValues = crParameterFieldDefinition.CurrentValues;

                    //crParameterValues.Add(crParameterDiscreteValue);
                    //crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();

                }
                else if (reporte == "rRetencionesAInforme")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rRetencionesAInforme.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["gen_codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    //crParameterDiscreteValue.Value = textBox2.Text;
                    //crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    //crParameterFieldDefinition = crParameterFieldDefinitions["toDate"];
                    //crParameterValues = crParameterFieldDefinition.CurrentValues;

                    //crParameterValues.Add(crParameterDiscreteValue);
                    //crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rNotasDebitoAutomaticas")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rNotasDebitoAutomaticas.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["gen_codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    //crParameterDiscreteValue.Value = textBox2.Text;
                    //crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    //crParameterFieldDefinition = crParameterFieldDefinitions["toDate"];
                    //crParameterValues = crParameterFieldDefinition.CurrentValues;

                    //crParameterValues.Add(crParameterDiscreteValue);
                    //crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rNotasDebitoAInforme")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rNotasDebitoAInforme.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["gen_codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    //crParameterDiscreteValue.Value = textBox2.Text;
                    //crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    //crParameterFieldDefinition = crParameterFieldDefinitions["toDate"];
                    //crParameterValues = crParameterFieldDefinition.CurrentValues;

                    //crParameterValues.Add(crParameterDiscreteValue);
                    //crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rHonorariosAtencion")
                {
                    //C:\Documents and Settings\Negrita\Mis documentos\Visual Studio 2008\Projects\HIS3000\His3000UI\HonorariosUI\honorariosDiarios.rpt

                    //string directorio = ;
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\honorariosAtencion.rpt");

                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    cryRpt.ParameterFields["codAtencion"].AllowCustomValues = true;
                    cryRpt.SetParameterValue("codAtencion", campo1);

                    //ParameterFieldDefinitions crParameterFieldDefinitions;
                    //ParameterFieldDefinition crParameterFieldDefinition;
                    //ParameterValues crParameterValues = new ParameterValues();
                    //ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();


                    //crParameterValues.Clear();
                    //crParameterDiscreteValue.Value = campo1;
                    //crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    //crParameterFieldDefinition = crParameterFieldDefinitions["codAtencion"];
                    //crParameterValues = crParameterFieldDefinition.CurrentValues;


                    //crParameterValues.Add(crParameterDiscreteValue);
                    //crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    //crParameterDiscreteValue.Value = textBox2.Text;
                    //crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    //crParameterFieldDefinition = crParameterFieldDefinitions["toDate"];
                    //crParameterValues = crParameterFieldDefinition.CurrentValues;

                    //crParameterValues.Add(crParameterDiscreteValue);
                    //crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    //crystalReportViewer1.Refresh();
                }
                else if (reporte == "rCheque")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rCheque.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["nombre"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = campo2;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["valor"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rRecuperacionHonorarios1")
                {
                    DataTable ds1 = new DataTable();
                    dst_honorarios ds2 = new dst_honorarios ();
                    DataRow dr2;

                    ds1 = NegHonorariosMedicos.DatosRecuperaHonorarios(Convert.ToInt32(campo1));

                    if (ds1 != null && ds1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in ds1.Rows)
                        {
                            dr2 = ds2.Tables["rRecuperacionHonorarios"].NewRow();

                            dr2["Documento"] = dr1["PAM_NUM_DOCUMENTO"];
                            dr2["Fecha_pago"] = dr1["PAM_FECHA_PAGO"];
                            dr2["Descripcion"] = dr1["PAM_DESCRIPCION"];
                            dr2["Observaciones"] = dr1["PAM_OBSERVACIONES"];
                            dr2["Fecha_impresion"] = Convert.ToDateTime(DateTime.Now.ToString());
                            dr2["Medico"] = dr1["Medico"];
                            dr2["Valor_recuperar"] = dr1["HOM_POR_RECUPERAR"]; ;
                            dr2["Recorte"] = dr1["PMD_RECORTE"];
                            dr2["Recuperado"] = dr1["PMD_VALOR"];
                            
                           

                            ds2.Tables["rRecuperacionHonorarios"].Rows.Add(dr2);
                        }

                        ReportDocument reporte1 = new ReportDocument();
                        reporte1.FileName = Application.StartupPath + "\\Reportes\\Honorarios\\rRecuperacionHonorarios1.rpt";
                        reporte1.Database.Tables["rRecuperacionHonorarios"].SetDataSource(ds2.Tables["rRecuperacionHonorarios"]);

                        crystalReportViewer1.ReportSource = reporte1;
                        crystalReportViewer1.Refresh();
                    }
                }




                else if (reporte == "rRecuperacionHonorarios")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rRecuperacionHonorarios.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["codPago"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rPagoHonorarios")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rPagoHonorarios.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["codCancelacion"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if (reporte == "rCancelacionMedico")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rCancelacionMedico.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = campo1;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["codCancelacion"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.Refresh();
                }
                else if(reporte == "HonorarioDetalle")
                {
                    Reportes.rptHonorarioDetalle myreport = new Reportes.rptHonorarioDetalle();
                    myreport.Refresh();
                    myreport.SetDataSource(Datos);
                    crystalReportViewer1.ReportSource = myreport;
                    crystalReportViewer1.RefreshReport();
                }
                else if(reporte == "Liquidacion")
                {
                    His.Formulario.rptPrincipalLiquidacion myreport = new His.Formulario.rptPrincipalLiquidacion();
                    myreport.Refresh();
                    myreport.SetDataSource(Datos);
                    crystalReportViewer1.ReportSource = myreport;
                    crystalReportViewer1.RefreshReport();
                }
                else
                {
                    //C:\Documents and Settings\Negrita\Mis documentos\Visual Studio 2008\Projects\HIS3000\His3000UI\HonorariosUI\honorariosDiarios.rpt

                    //string directorio = ;
                    ReportDocument cryRpt = new ReportDocument();
                    //cryRpt.Load("C:\\Documents and Settings\\Administrador\\Mis documentos\\Visual Studio 2008\\Projects\\His3000(24)\\His.Honorarios\\rNotaCreditoDebito.rpt");
                    cryRpt.Load(Application.StartupPath + @"\Reportes\Honorarios\rHonorariosDiarios.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    //cryRpt.ParameterFields["@fechaDesde"].AllowCustomValues = true;
                    cryRpt.SetParameterValue("@fechaDesde", campo1);

                    //cryRpt.ParameterFields["@fechaHasta"].AllowCustomValues = true;
                    cryRpt.SetParameterValue("@fechaHasta", campo2);

                    //cryRpt.ParameterFields["@codCaja"].AllowCustomValues = true;
                    cryRpt.SetParameterValue("@codCaja", campo3);

                    //cryRpt.ParameterFields["@codUsuario"].AllowCustomValues = true;
                    cryRpt.SetParameterValue("@codUsuario", campo4);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    crystalReportViewer1.ReportSource = cryRpt;
                    //crystalReportViewer1.Refresh();
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);      
            }
        }
    }
}
