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


namespace His.Admision
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
        public string campo7;
        public string campo8;
        public string campo9;
        public string campo10;
        public string campo11;
        public string campo12;
        public string campo13;
        public DateTime campo14;
        public string campo15;
        public string campo16;
        public string campo17;
        public string campo18;


        public int parametro;
        public DataSet Datos;

        #endregion

        #region Constructor
        public frmReportes()
        {
            InitializeComponent();

        }

        public frmReportes(int parametro, string reporte, DataSet datos)
        {
            this.parametro = parametro;
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
                if (reporte == "Contrato")
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //Reportes.Contrato contrato = new His.Admision.Reportes.Contrato();

                    cryRpt.Load(Application.StartupPath + "\\Reportes\\Admision\\Contrato.rpt");
                    
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    cryRpt.SetParameterValue("codAtencion",campo1);
                    cryRpt.SetParameterValue("factNombre", campo2);
                    cryRpt.SetParameterValue("numPoliza", campo3);
                    cryRpt.SetParameterValue("nomAseg", campo4);
                    cryRpt.SetParameterValue("montoAseg", campo5);
                    cryRpt.SetParameterValue("telfAseg", campo6);
                    cryRpt.SetParameterValue("nomEmp", campo8);
                    cryRpt.SetParameterValue("telfEmp", campo9);
                    cryRpt.SetParameterValue("montoEmp", campo10);
                    cryRpt.SetParameterValue("correo", campo13);
                    cryRpt.SetParameterValue("fechapagare", campo14);
                    cryRpt.SetParameterValue("numcontrato", campo15);
                    cryRpt.SetParameterValue("numcelular", campo16);
                    cryRpt.SetParameterValue("objpago", campo17);
                    cryRpt.SetParameterValue("nomciudad", campo18);

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

                else if (reporte == "rAdmision")
                {
                    
                    //crystalReportViewer1.Refresh();
                    ReportDocument cryRpt = new ReportDocument();
                    //Reportes.Contrato contrato = new His.Admision.Reportes.Contrato();

                    cryRpt.Load(Application.StartupPath + "\\Reportes\\Admision\\rPrimeraAtencion2.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables = null;

                    //cryRpt.ParameterFields["cPaciente"].AllowCustomValues = true;

                    cryRpt.SetParameterValue("cPaciente", campo1);
                    cryRpt.SetParameterValue("@codigoAtencion", campo1);
                    cryRpt.SetParameterValue("@codigoAtencion1", campo1);
                    cryRpt.SetParameterValue("codPaciente3", campo1);

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
                    //crystalReportViewer1.ReportSource = cryRpt;
                    //crystalReportViewer1.RefreshReport();
                }
                else if (reporte == "rEncuesta")
                {

                    ReportDocument cryRpt = new ReportDocument();
                    //Reportes.Contrato contrato = new His.Admision.Reportes.Contrato();

                    cryRpt.Load(Application.StartupPath + "\\Reportes\\Admision\\rptEncuesta.rpt");

                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    cryRpt.SetParameterValue("codAtencion", campo1);
                    cryRpt.SetParameterValue("factNombre", campo2);
                    cryRpt.SetParameterValue("numPoliza", campo3);
                    cryRpt.SetParameterValue("nomAseg", campo4);
                    cryRpt.SetParameterValue("montoAseg", campo5);
                    cryRpt.SetParameterValue("telfAseg", campo6);
                    cryRpt.SetParameterValue("nomEmp", campo8);
                    cryRpt.SetParameterValue("telfEmp", campo9);
                    cryRpt.SetParameterValue("montoEmp", campo10);

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
                }

                else if (reporte == "rAdmisionEgreso")
                {

                    //crystalReportViewer1.Refresh();
                    ReportDocument cryRpt = new ReportDocument();
                    //Reportes.Contrato contrato = new His.Admision.Reportes.Contrato();

                    cryRpt.Load(Application.StartupPath + "\\Reportes\\Admision\\rPrimeraAtencion3.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables = null;

                    //cryRpt.ParameterFields["cPaciente"].AllowCustomValues = true;

                    cryRpt.SetParameterValue("cPaciente", campo1);
                    //cryRpt.SetParameterValue("codPaciente1", campo1);
                    //cryRpt.SetParameterValue("codPaciente2", campo1);
                    //cryRpt.SetParameterValue("codPaciente3", campo1);

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
                    //crystalReportViewer1.ReportSource = cryRpt;
                    //crystalReportViewer1.Refresh();
                }

                #region CERTIFICADO MEDICO
                else if (reporte == "certificado")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\Admision\\CertifivadoMedico.rpt";
                        //crystalReportViewer1.ReportSource = reporteAnamnesis;
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();

                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion 

                #region LETRERO
                else if (reporte == "LETRERO")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\Admision\\rpt_LETRERO.rpt";
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();

                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion


                #region EGRESO
                else if (reporte == "rptTurnoDietetica")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\rptTurnoImagen.rpt";
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();

                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion


                #region EGRESO
                else if (reporte == "EGRESO_LX")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\Admision\\RPT_EGRESO.rpt";
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();

                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion
                #region ACTA COMPROMISO
                else if (reporte == "ACTA")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\Admision\\rpt_ActaCompromiso.rpt";
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();

                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion


                

            #region Datos de Formularios - Encabezados
                else if (reporte == "Copias")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\Admision\\rpt_Solicitud_Copias.rpt";
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();

                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion

                #region Datos de Formularios - Encabezados
                else if (reporte == "EncabezadosForms")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\Admision\\rpt_Encabezados.rpt";
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();

                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion
                #region Datos de Formularios - Encabezados AdminMed
                else if (reporte == "EncabezadosForms2")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\Admision\\rpt_Encabezados_am.rpt";
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();

                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion
                #region Datos de Formularios - Encabezados AdminMed
                else if (reporte == "EncabezadosForms3")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\Admision\\rpt_Encabezados_lvs.rpt";
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();

                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion


                #region CensoDIario
                else if (reporte == "CensoDiario")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\Admision\\rptCensoDiario.rpt";
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();

                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion

                #region Brazzalete
                else if (reporte == "Brazzalete")
                {
                    try
                    {
                        //rptFORM022 myreport = new rptFORM022();
                        Brazzalete myreport = new Brazzalete();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion
                #region Etiquetas
                else if (reporte == "Etiqueta")
                {
                    try
                    {
                        //rptFORM022 myreport = new rptFORM022();
                        Etiqueta myreport = new Etiqueta();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion

                else if(reporte == "Censo")
                {
                    try
                    {
                        rptCensoDiario myreport = new rptCensoDiario();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);      
            }
        }
    }
}
