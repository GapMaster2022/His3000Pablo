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


namespace TarifariosUI
{
    public partial class frmReportes : Form
    {

        public int parametro;
        public int parametro2;
        public string reporte;
        //ReportDocument rpt = new ReportDocument();
        public frmReportes(int parParametro)
        {
            this.parametro = parParametro;
            InitializeComponent();
            //cargarReporte();
        }

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

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            cargarReporte();
        }

        private void frmReportes_Load_1(object sender, EventArgs e)
        {
            cargarReporte();
            //try
            //{
            //    string directorio = Environment.CurrentDirectory.ToString() + "\\Reportes\\crpHonorariosTarifario.rpt";
            //    //MessageBox.Show(directorio);  
            //    //rpt.Load(@directorio);
            //    rpt.Load(@directorio);
            //    rpt.SetDatabaseLogon("sa", "Gap2010", "SQLSERVERPROD\\GAP_SERVER2008", "HIS3000BD", false);
            //    rpt.SetParameterValue("codigo", 5);
            //    crystalReportViewer1.ReportSource = rpt;
            //    crystalReportViewer1.RefreshReport();
            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message);
            //}
        }

        private void cargarReporte()
        {
            try
            {

                if (reporte == "aseguradoras")
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    //cryRpt.Load(Environment.CurrentDirectory.ToString() + "\\Reportes\\Tarifario\\EmpresasAseguradoras.rpt");
                    cryRpt.Load(Application.StartupPath + "\\Reportes\\Tarifario\\EmpresasAseguradoras.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    //ParameterFieldDefinitions crParameterFieldDefinitions;
                    //ParameterFieldDefinition crParameterFieldDefinition;
                    //ParameterValues crParameterValues = new ParameterValues();
                    //ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    //crParameterDiscreteValue.Value = parametro;
                    //crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    //crParameterFieldDefinition = crParameterFieldDefinitions["codigo"];
                    //crParameterValues = crParameterFieldDefinition.CurrentValues;

                    //crParameterValues.Clear();
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
                    crystalReportViewer1.Refresh();

                }
                else if(reporte=="convenio")
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    cryRpt.Load(Application.StartupPath + "\\Reportes\\Tarifario\\ConvenioAseguradora.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue(); 
                    
                    crParameterDiscreteValue.Value = parametro;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["codTarifario"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crParameterDiscreteValue.Value = parametro2;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["codAseguradora"];
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
                else
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    cryRpt.Load(Application.StartupPath + "\\Reportes\\Tarifario\\crpHonorariosTarifario.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                    crParameterDiscreteValue.Value = parametro;
                    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["codigo"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    crParameterValues.Clear();
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
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);     
            }
        }

        //private void btnRefresh_Click(object sender, EventArgs e)
        //{
        //    //Begin: Bind the reports dynamically
        //    DataTable dtReports = BindReports();
        //    rpt.SetDataSource(dtReports);
        //    //End: Bind the reports dynamically

        //    //Begin: Update the report label value from All to the entered employee name in the text box
        //    TextObject myTextObjectOnReport;
        //    if (rpt.ReportDefinition.ReportObjects["txtEmpName"] != null)
        //    {
        //        myTextObjectOnReport = (TextObject)rpt.ReportDefinition.ReportObjects["txtEmpName"];
        //        myTextObjectOnReport.Text = txtName.Text;
        //    }

        //    reportViewer1.ReportSource = rpt;
        //    reportViewer1.Refresh();
        //    //End
        //}
        //Filtered the report details based on the employee name entered in the textbox 
        //private DataTable BindReports()
        //{
        //    string conString = @"Data Source=SQLSERVERPROD\GAP_SERVER2008;Initial Catalog=HIS3000BD;Integrated Security=True;";
        //    SqlConnection con = new SqlConnection(conString);
        //    con.Open();
        //    string query = "SELECT EmployeeID, DepID, Name, Mark FROM Employee E WHERE E.Name='" + txtName.Text + "'";
        //    SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, con);

        //    DataTable dtReport = new DataTable();
        //    sqlAdapter.Fill(dtReport);
        //    con.Close();

        //    return dtReport;
        //}
        //private void btnExport_Click(object sender, EventArgs e)
        //{
        //    ExportOptions rptExportOption;
        //    DiskFileDestinationOptions rptFileDestOption = new DiskFileDestinationOptions();
        //    PdfRtfWordFormatOptions rptFormatOption = new PdfRtfWordFormatOptions();

        //    If we want to generate the report as pdf, change the file extention type as "D:\Muthu\SampleReport.pdf"

        //    If we want to generate the report as excel, change the file extention type as "D:\Muthu\SampleReport.xls"
        //    string reportFileName = @"D:\Muthu\SampleReport.doc";

        //    rptFileDestOption.DiskFileName = reportFileName;
        //    rptExportOption = rpt.ExportOptions;
        //    {
        //        rptExportOption.ExportDestinationType = ExportDestinationType.DiskFile;
        //        if we want to generate the report as PDF, change the ExportFormatType as "ExportFormatType.PortableDocFormat"
        //        if we want to generate the report as Excel, change the ExportFormatType as "ExportFormatType.Excel"
        //        rptExportOption.ExportFormatType = ExportFormatType.RichText;
        //        rptExportOption.ExportDestinationOptions = rptFileDestOption;
        //        rptExportOption.ExportFormatOptions = rptFormatOption;
        //    }
        //    rpt.Export();
        //}
    }
}
