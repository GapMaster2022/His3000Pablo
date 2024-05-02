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
using System.Net.Mail;
using His.Entidades;
using System.IO;
using System.Net;

namespace His.Formulario
{
    public partial class frmReportes : Form
    {
        public frmReportes()
        {
            InitializeComponent();
        }

        public bool _envia;
        public int parametro;
        public string reporte;
        public string campo1;
        public DataSet Datos;
        public DataSet Datos1;
        public DataSet Datos2;
        string para = "";
        string mensaje = "";
        string asunto = "";
        string nombrePac = "";
        MEDICOS med = new MEDICOS();

        //ReportDocument rpt = new ReportDocument();
        public frmReportes(int parParametro, string reporte, MEDICOS _med = null, DataSet datos1 = null)
        {
            InitializeComponent();
            this.parametro = parParametro;
            this.reporte = reporte;
            med = _med;
            this.Datos = datos1;
        }

        public frmReportes(int parParametro, string reporte, bool enviaEmail, string _para, string _mensaje, string _asunto, string nombre,DataSet datos)
        {
            this.parametro = parParametro;
            this.reporte = reporte;
            InitializeComponent();
            _envia = enviaEmail;
            para = _para;
            mensaje = _mensaje;
            asunto = _asunto;
            nombrePac = nombre;
            this.Datos = datos;
        }

        public frmReportes(int parParamentro, string reporte, DataSet datos)
        {
            this.parametro = parParamentro;
            this.reporte = reporte;
            this.Datos = datos;
            InitializeComponent();
        }
        public frmReportes(int parParametro, string reporte, DataSet Cabecera, DataSet Cuerpo, DataSet Detalle)
        {
            this.parametro = parParametro;
            this.reporte = reporte;
            this.Datos = Cabecera;
            this.Datos1 = Cuerpo;
            this.Datos2 = Detalle;
            InitializeComponent();
        }
        private void cargarReporte()
        {
            try
            {
                #region ADMISION
                if (reporte == "admision")
                {
                    ReportDocument cryRpt = new ReportDocument();

                    cryRpt.Load(Application.StartupPath + "\\Reportes\\Admision\\rPrimeraAtencion2.rpt");

                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables = null;

                    string campo1 = Convert.ToString(parametro);
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
                    crystalReportViewer1.RefreshReport();
                    crystalReportViewer1.PrintReport();
                }
                #endregion

                #region FACTURAR A NOMBRE DE 

                else if (reporte == "facturaNombre")
                {
                    try
                    {
                        FacturaNombreDe myreport = new FacturaNombreDe();
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

                #region EPICRISIS 1 - 2

                //else if (reporte == "epicrisis1")
                //{
                //    ReportDocument reporteEpicrisis = new ReportDocument();
                //    reporteEpicrisis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\epicrisis.rpt";
                //    //crystalReportViewer1.ReportSource = reporteAnamnesis;
                //    TableLogOnInfo logInCon = new TableLogOnInfo();
                //    logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                //    foreach (Table tb in reporteEpicrisis.Database.Tables)
                //    {
                //        tb.ApplyLogOnInfo(logInCon);
                //    }
                //    reporteEpicrisis.Refresh();
                //    crystalReportViewer1.ReportSource = reporteEpicrisis;
                //    crystalReportViewer1.RefreshReport();

                //}
                //else
                //    if (reporte == "epicrisis2")
                //{
                //    try
                //    {
                //        ReportDocument reporteEpicrisis = new ReportDocument();
                //        reporteEpicrisis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\epicrisis2.rpt";
                //        //crystalReportViewer1.ReportSource = reporteAnamnesis;
                //        TableLogOnInfo logInCon = new TableLogOnInfo();
                //        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                //        foreach (Table tb in reporteEpicrisis.Database.Tables)
                //        {
                //            tb.ApplyLogOnInfo(logInCon);
                //        }
                //        reporteEpicrisis.Refresh();
                //        crystalReportViewer1.ReportSource = reporteEpicrisis;
                //        crystalReportViewer1.RefreshReport();
                //    }
                //    catch (Exception err)
                //    { throw err; }

                //}

                #endregion
                #region EPICRISIS NUEVA
                else if (reporte == "Epicrisis")
                {
                    try
                    {
                        Form006 myreport = new Form006();
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

                #region EVOLUCION
                else if (reporte == "evolucion")
                {
                    try
                    {
                        NotasIndividualesMedicos myreport = new NotasIndividualesMedicos();
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
                #region QuirofanoProductos
                else if (reporte == "QuirofanoProductos")
                {
                    try
                    {
                        ControlProductosQuirofano myreport = new ControlProductosQuirofano();
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
                #region PedidoQuirofano
                else if (reporte == "PedidoQuirofano")
                {
                    try
                    {
                        QuirofanoPedidoImpresion myreport = new QuirofanoPedidoImpresion();
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

                #region RecetaMedica
                else if (reporte == "Receta")
                {
                    try
                    {
                        ReporteReceta myreport = new ReporteReceta();
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
                else if (reporte == "RecetaPasteur")
                {
                    try
                    {
                        frmRecetaPasteur myreport = new frmRecetaPasteur();
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
                #region CertificadoMedicoHospitalarioIESS
                else if (reporte == "CertificadoIESS")
                {
                    try
                    {
                        rptCertificadoIESS myreport = new rptCertificadoIESS();
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
                else if (reporte == "CertificadoProcesoIESS")
                {
                    try
                    {
                        rptCertificadoProcedimientoIESS myreport = new rptCertificadoProcedimientoIESS();
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
                else if (reporte == "CertificadoProcesoClinicoIESS")
                {
                    try
                    {
                        rptCertificadoProcedimientoClinicoIESS myreport = new rptCertificadoProcedimientoClinicoIESS();
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
                else if (reporte == "Frm0012Imagen")
                {
                    try
                    {
                        rptFrom12Imagen myreport = new rptFrom12Imagen();
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
                else if (reporte == "formImagen")
                {
                    try
                    {
                        rptForm012 myreport = new rptForm012();
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
                else if (reporte == "PedidoConValores")
                {
                    try
                    {
                        rptImpresionPredido_valores myreport = new rptImpresionPredido_valores();
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
                #region CertificadoMedicoHospitalarioAlta
                else if (reporte == "CertificadoHA")
                {
                    try
                    {
                        CertificadoHospitalarioAlta myreport = new CertificadoHospitalarioAlta();
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
                #region CertificadoMedicoPresentacion
                else if (reporte == "CertificadoPresentacion")
                {
                    try
                    {
                        frm_CerfificadoPresentacion myreport = new frm_CerfificadoPresentacion();
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
                #region CertificadoMedicoEmergenciaAlta
                else if (reporte == "CertificadoEA")
                {
                    try
                    {
                        CertificadoEmergenciaAlta myreport = new CertificadoEmergenciaAlta();
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
                #region CertificadoMedicoMushuñan
                else if (reporte == "CertificadoEAM")
                {
                    try
                    {
                        CertificadoEmergenciaAltaMushuñan myreport = new CertificadoEmergenciaAltaMushuñan();
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
                #region CertificadoHospitalarioSinAlta
                else if (reporte == "CertificadoHSA")
                {
                    try
                    {
                        CertificadoHospitalarioSinAlta myreport = new CertificadoHospitalarioSinAlta();
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
                #region CIERREDIETA
                else if (reporte == "CierreDietetica")
                {
                    try
                    {
                        rptCierreTurnoDietetica myreport = new rptCierreTurnoDietetica();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
                #endregion
                #region NotasIndividualesEvolucionMedicos
                else if (reporte == "NotasIndividualesEvolucionMedicos")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\rEvolucionNotas.rpt";
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
                #region IMAGENResumen



                else if (reporte == "imagenologiaResumen")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\rptImagenResumen.rpt";
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
                #region IMAGEN
                else if (reporte == "imagenologia")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\rptImagen.rpt";
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



                #region PRECIO CONVENIO
                else if (reporte == "PreciosConvenio")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\Mantenimiento\\rptPreciosConvenios.rpt";
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

                #region Form012
                else if (reporte == "form012")
                {
                    mailImagen();
                    //try
                    //{
                    //    ReportDocument reporteAnamnesis = new ReportDocument();
                    //    reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\rptForm012.rpt";
                    //    TableLogOnInfo logInCon = new TableLogOnInfo();
                    //    logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                    //    foreach (Table tb in reporteAnamnesis.Database.Tables)
                    //    {
                    //        tb.ApplyLogOnInfo(logInCon);
                    //    }
                    //    reporteAnamnesis.Refresh();
                    //    crystalReportViewer1.ReportSource = reporteAnamnesis;
                    //    crystalReportViewer1.RefreshReport();
                    //    if (_envia)
                    //    {
                    //        List<DtoParametros> Informacion = new List<DtoParametros>();
                    //        Informacion = NegUtilitarios.RecuperaInformacionCorreo();
                    //        string[] datos = new string[10];
                    //        foreach (var item in Informacion)
                    //        {
                    //            datos = item.PAD_VALOR.Split(';');
                    //        }
                    //        ExportOptions CrExportOptions;
                    //        DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    //        PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    //        CrDiskFileDestinationOptions.DiskFileName = datos[0].ToString() + nombrePac + ".pdf";
                    //        CrExportOptions = reporteAnamnesis.ExportOptions;
                    //        {
                    //            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    //            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    //            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    //            CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    //        }
                    //        reporteAnamnesis.Export();
                    //        reporteAnamnesis.Close();
                    //        SmtpClient smtp = new SmtpClient();
                    //        MailMessage mail = new MailMessage();
                    //        Attachment anexar = new Attachment(datos[0].ToString() + nombrePac + ".pdf");
                    //        mail.Attachments.Add(anexar);
                    //        smtp.Host = datos[1].ToString();
                    //        smtp.Port = Convert.ToInt16(datos[2].ToString());
                    //        smtp.EnableSsl = true;
                    //        smtp.UseDefaultCredentials = false;
                    //        smtp.Credentials = new System.Net.NetworkCredential(datos[3].ToString(), datos[4].ToString());
                    //        mail.From = new MailAddress(datos[3].ToString());
                    //        mail.To.Add(new MailAddress(para));
                    //        mail.Subject = asunto;
                    //        mail.Body = mensaje;

                    //        smtp.Send(mail);

                    //        //File.Delete(datos[0].ToString() + nombrePac + ".pdf");
                    //        this.Close();
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.ToString());
                    //}

                }
                #endregion
                #region REFERENCIA
                else if (reporte == "Referencia")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\rptReferencia.rpt";
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

                #region TEMP DIETETICA
                else if (reporte == "rptTempDietetica")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\rptTempDietetica.rpt";
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

                #region CONTRAREFERENCIA
                else if (reporte == "Contrareferencia")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\rptContrareferencia.rpt";
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

                #region vendedres comison
                else if (reporte == "Vendedores")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\Honorarios\\rptVendedoresFacturas.rpt";
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

                #region ADMISIONEGRESO
                else if (reporte == "admisionE")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\Admision\\rPrimeraAtencion3.rpt";
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
                        crystalReportViewer1.PrintReport();
                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion

                #region INTERCONSULTA
                else if (reporte == "ExamenSangre")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\RPT_PEDIDO_SANGRE_detalle.rpt";
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
                    catch (Exception ex)
                    { throw ex; }
                }
                else if (reporte == "interconsulta")
                {
                    try
                    {
                        imprimirreporte();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else if (reporte == "mail")
                {
                    try
                    {
                        mail();
                    }
                    catch (Exception ex)
                    {
                        //throw ex;
                    }
                }
                else if (reporte == "interconsulta2")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\interconsulta2.rpt";
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
                    catch (Exception ex)
                    { throw ex; }
                }
                #endregion
                #region InterconsultaBNew
                else if (reporte == "InterconsultaB")
                {
                    rptInterconsultaB myreport = new rptInterconsultaB();
                    myreport.Refresh();
                    myreport.SetDataSource(Datos);
                    crystalReportViewer1.ReportSource = myreport;
                    crystalReportViewer1.RefreshReport();
                }
                #endregion
                #region ProtocoloOperatorio2021
                else if (reporte == "ProtocoloOperatorio2021")
                {
                    ProtocoloOperatorio2021 myreport = new ProtocoloOperatorio2021();
                    myreport.Refresh();
                    myreport.SetDataSource(Datos);
                    crystalReportViewer1.ReportSource = myreport;
                    crystalReportViewer1.RefreshReport();
                }
                #endregion
                #region InterconsultaBNewCorreo
                else if (reporte == "InterconsultaBCorreo")
                {
                    mailB();
                    //rptInterconsultaB myreport = new rptInterconsultaB();
                    //myreport.Refresh();
                    //myreport.SetDataSource(Datos);

                    ////Recuepero de donde se va a enviar el correo Electronico
                    //List<DtoParametros> Informacion = new List<DtoParametros>();
                    //Informacion = NegUtilitarios.RecuperaInformacionCorreo();
                    //string[] datos = new string[10];
                    //foreach (var item in Informacion)
                    //{
                    //    datos = item.PAD_VALOR.Split(';');
                    //}
                    ////Creacion de la Carpeda de Origen
                    //string carpeta = @"C:\adjuntos";

                    //// Verificar si la carpeta existe
                    //if (!Directory.Exists(carpeta))
                    //{
                    //    // Crear la carpeta si no existe
                    //    Directory.CreateDirectory(carpeta);
                    //    Console.WriteLine("Carpeta creada correctamente.");
                    //}
                    ////Exporta a PDF
                    //string pathAutorizado = carpeta;
                    //string nombrenuevo = "INTERCONSULTA";
                    //myreport.ExportToDisk(ExportFormatType.PortableDocFormat, pathAutorizado + "\\" + nombrenuevo + ".pdf");
                    /////envio mail
                    //string rutaCarpeta = pathAutorizado;
                    //string remitente = datos[3];


                    //string destinatario = med.MED_EMAIL;
                    //string asunto = "Interconsulta: ";
                    //string cuerpoMensaje = "Adjunto se encuentra la Interconsulta: ";


                    //// Crear instancia de MailMessage
                    //destinatario = Microsoft.VisualBasic.Interaction.InputBox("Correo", "Enviar Mail a:", destinatario);
                    ////if (destinatario == "")
                    ////{
                    ////    MessageBox.Show("No tiene mail", "Aviso");
                    ////}
                    //if (IsValidEmail(destinatario))
                    //{
                    //    MailMessage mensaje = new MailMessage(remitente, destinatario, asunto, cuerpoMensaje);

                    //    // Obtener lista de archivos en la carpeta
                    //    string[] archivos = Directory.GetFiles(rutaCarpeta);

                    //    // Adjuntar cada archivo al correo electrónico
                    //    foreach (string archivo in archivos)
                    //    {
                    //        mensaje.Attachments.Add(new Attachment(archivo));
                    //    }

                    //    // Configurar el cliente de correo electrónico
                    //    SmtpClient clienteSmtp = new SmtpClient(datos[1], 25);
                    //    clienteSmtp.EnableSsl = true;
                    //    clienteSmtp.UseDefaultCredentials = false;
                    //    string clave = datos[4];
                    //    clienteSmtp.Credentials = new NetworkCredential(remitente, clave);
                    //    try
                    //    {
                    //        // Enviar el correo electrónico
                    //        clienteSmtp.Send(mensaje);
                    //        Console.WriteLine("Correo electrónico enviado correctamente.");
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Console.WriteLine("Error al enviar el correo electrónico: " + ex.Message);
                    //    }
                    //    finally
                    //    {
                    //        // Liberar recursos
                    //        mensaje.Dispose();
                    //    }
                    //    string carpetad = @"C:\adjuntos";

                    //    // Obtener la lista de archivos en la carpeta
                    //    string[] archivosd = Directory.GetFiles(carpetad);

                    //    // Eliminar cada archivo de la carpeta
                    //    foreach (string archivo in archivosd)
                    //    {
                    //        File.Delete(archivo);
                    //    }

                    //    myreport.Close();
                    //    myreport.Dispose();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Mail Incorrecto", "Aviso");
                    //}
                }
                #endregion
                #region ANAMNESIS
                else if (reporte == "anamnesis")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\historiaClinica.rpt";
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
                #region AnamnesisNew
                else if (reporte == "AnamnesisNew")
                {
                    FrmAnamnesi003 myreport = new FrmAnamnesi003();
                    myreport.Refresh();
                    myreport.SetDataSource(Datos);
                    crystalReportViewer1.ReportSource = myreport;
                    crystalReportViewer1.RefreshReport();
                }
                #endregion
                #region FORMULARIO 8 DINAMICO
                else if (reporte == "Hoja 008")
                {
                    try
                    {

                        rptForm008 myreport = new rptForm008(); 
                        //FrmEmergencia008 myreport = new FrmEmergencia008();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    #region CODIGO ANTIGUO
                    //try
                    //{
                    //    ReportDocument reporteEmergencia = new ReportDocument();
                    //    reporteEmergencia.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\Formulario8.rpt";
                    //    Database s = new Database;
                    //    reporteEmergencia.Database = "";
                    //    TableLogOnInfo logInCon = new TableLogOnInfo();
                    //    logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                    //    foreach (Table tb in reporteEmergencia.Database.Tables)
                    //    {
                    //        tb.ApplyLogOnInfo(logInCon);
                    //    }
                    //    reporteEmergencia.Refresh();
                    //    crystalReportViewer1.ReportSource = reporteEmergencia;
                    //    crystalReportViewer1.RefreshReport();
                    //    crystalReportViewer1.Refresh();
                    //}
                    //catch (Exception err)
                    //{ throw err; }
                    #endregion
                }
                #endregion
                #region FORMULARIO 008 del 2021
                else if (reporte == "Hoja 008New")
                {
                    try
                    {

                        //rptForm008 myreport = new rptForm008();
                        FrmEmergencia008 myreport = new FrmEmergencia008();
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

                //Hoja 008E
                #region FORMULARIO 008E
                else if (reporte == "Hoja 008E")
                {
                    try
                    {
                        ReportDocument reporteEmergencia = new ReportDocument();
                        reporteEmergencia.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\Formulario008E.rpt";
                        //Database s = new Database;     
                        //reporteEmergencia.Database = ""; 
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteEmergencia.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteEmergencia.Refresh();
                        crystalReportViewer1.ReportSource = reporteEmergencia;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion


                #region PROTOCOLO
                else if (reporte == "protocolo")
                {
                    try
                    {
                        ReportDocument reporteProtocolo = new ReportDocument();
                        reporteProtocolo.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\ProtocoloOperatorio.rpt";
                        //crystalReportViewer1.ReportSource = reporteAnamnesis;
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteProtocolo.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteProtocolo.Refresh();
                        crystalReportViewer1.ReportSource = reporteProtocolo;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion

                #region FACTURA
                else if (reporte == "Factura")
                {
                    try
                    {
                        //ReportDocument reporteFactura = new ReportDocument();
                        //reporteFactura.FileName = Application.StartupPath + "\\Reportes\\Facturas\\Factura.rpt";
                        ////Database s = new Database;     
                        ////reporteEmergencia.Database = ""; 
                        //TableLogOnInfo logInCon = new TableLogOnInfo();
                        //logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        //foreach (Table tb in reporteFactura.Database.Tables)
                        //{
                        //    tb.ApplyLogOnInfo(logInCon);
                        //}
                        //reporteFactura.Refresh();
                        //crystalReportViewer1.ReportSource = reporteFactura;
                        //crystalReportViewer1.RefreshReport();




                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion

                #region CERTIFICADO MEDICO
                else if (reporte == "certificado")
                {
                    try
                    {
                        ReportDocument CertifivadoMedico = new ReportDocument();
                        CertifivadoMedico.FileName = Application.StartupPath + "\\Reportes\\Admision\\CertifivadoMedico.rpt";
                        //crystalReportViewer1.ReportSource = reporteAnamnesis;
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in CertifivadoMedico.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        CertifivadoMedico.Refresh();
                        crystalReportViewer1.ReportSource = CertifivadoMedico;
                        crystalReportViewer1.RefreshReport();

                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion

                #region PRE-FACTURA RED

                else if (reporte == "PrefacturaRed")
                {
                    DataTable reporteDatos = new DataTable();
                    NegCertificadoMedico C = new NegCertificadoMedico();
                    PrefacturaAuditoria pre = new PrefacturaAuditoria();

                    try
                    {
                        reporteDatos = NegFormulariosHCU.RecuperaPrefacturaDatos(Convert.ToInt32(parametro));
                        if (reporteDatos.Rows.Count > 0)
                        {
                            for (int i = 0; i < reporteDatos.Rows.Count; i++)
                            {

                                DataRow drKardex;
                                drKardex = pre.Tables["DatosPrefactura"].NewRow();
                                drKardex["Remitente"] = reporteDatos.Rows[i][0].ToString();
                                drKardex["Prestador"] = reporteDatos.Rows[i][1].ToString();
                                drKardex["TipoSeguro"] = reporteDatos.Rows[i][2].ToString();
                                drKardex["Cedula"] = reporteDatos.Rows[i][4].ToString();
                                drKardex["FechaNac"] = reporteDatos.Rows[i][5].ToString();
                                drKardex["Titular"] = reporteDatos.Rows[i][3].ToString();
                                drKardex["CedulaTitular"] = reporteDatos.Rows[i][4].ToString();
                                drKardex["HC"] = reporteDatos.Rows[i][6].ToString();
                                drKardex["FechaIngreso"] = reporteDatos.Rows[i][7].ToString();
                                drKardex["FechaEgreso"] = reporteDatos.Rows[i][8].ToString();
                                drKardex["Factura"] = reporteDatos.Rows[i][10].ToString();
                                drKardex["Total"] = reporteDatos.Rows[i][9].ToString();
                                drKardex["CieIngreso"] = reporteDatos.Rows[i][11].ToString();
                                drKardex["PathImagen"] = C.path();
                                drKardex["Paciente"] = reporteDatos.Rows[i][3].ToString();
                                pre.Tables["DatosPrefactura"].Rows.Add(drKardex);
                            }
                        }
                        reporteDatos = null;
                        reporteDatos = NegFormulariosHCU.RecuperaPrefacturaRubros(Convert.ToInt32(parametro));
                        if (reporteDatos.Rows.Count > 0)
                        {
                            for (int i = 0; i < reporteDatos.Rows.Count; i++)
                            {

                                DataRow drKardex;
                                drKardex = pre.Tables["Rubros"].NewRow();
                                drKardex["Fecha"] = reporteDatos.Rows[i][0].ToString();
                                drKardex["Procedimiento"] = reporteDatos.Rows[i][1].ToString();
                                drKardex["Codigo"] = reporteDatos.Rows[i][2].ToString();
                                drKardex["Nivel"] = reporteDatos.Rows[i][3].ToString();
                                drKardex["Detalle"] = reporteDatos.Rows[i][4].ToString();
                                drKardex["ProcAnes"] = reporteDatos.Rows[i][5].ToString();
                                drKardex["Cantidad"] = reporteDatos.Rows[i][6].ToString();
                                drKardex["ValorUnitario"] = reporteDatos.Rows[i][7].ToString();
                                drKardex["ValorTotal"] = reporteDatos.Rows[i][8].ToString();

                                pre.Tables["Rubros"].Rows.Add(drKardex);
                            }
                            Cry_PrefacturaRed myreport = new Cry_PrefacturaRed();
                            myreport.Refresh();
                            myreport.SetDataSource(pre);
                            crystalReportViewer1.ReportSource = myreport;
                            crystalReportViewer1.RefreshReport();
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }

                #endregion

                #region KARDEX MEDICAMENTO

                else if (reporte == "KardexMedicamento")
                {
                    DataTable reporteDatos = new DataTable();
                    NegCertificadoMedico C = new NegCertificadoMedico();
                    reporteDatos = NegFormulariosHCU.ReporteDatos(campo1);
                    KardexMedicinas kardex = new KardexMedicinas();

                    for (int i = 0; i < reporteDatos.Rows.Count; i++)
                    {
                        DataRow drKardex;
                        drKardex = kardex.Tables["KARDEXMEDICAMENTOS"].NewRow();
                        drKardex["Usuario"] = reporteDatos.Rows[i][0].ToString();
                        drKardex["Departamento"] = reporteDatos.Rows[i][1].ToString();
                        drKardex["AteCodigo"] = reporteDatos.Rows[i][2].ToString();
                        drKardex["CueCodigo"] = reporteDatos.Rows[i][3].ToString();
                        drKardex["Presentacion"] = reporteDatos.Rows[i][4].ToString();
                        drKardex["Dosis"] = reporteDatos.Rows[i][5].ToString();
                        drKardex["Frecuencia"] = reporteDatos.Rows[i][6].ToString();
                        drKardex["Via"] = reporteDatos.Rows[i][7].ToString();
                        drKardex["Hora"] = reporteDatos.Rows[i][8].ToString();
                        drKardex["FechaAdministracion"] = reporteDatos.Rows[i][9].ToString();
                        drKardex["MedicamentoEventual"] = reporteDatos.Rows[i][10].ToString();
                        drKardex["MedicamentoPropio"] = reporteDatos.Rows[i][11].ToString();
                        drKardex["Paciente"] = reporteDatos.Rows[i][12].ToString();
                        drKardex["Hclinica"] = reporteDatos.Rows[i][13].ToString();
                        drKardex["Logo"] = C.path();
                        //drKardex["Otro"] = reporteDatos.Rows[i][14].ToString();
                        kardex.Tables["KARDEXMEDICAMENTOS"].Rows.Add(drKardex);

                    }
                    //Cambios Edgar 20210128
                    KardexMedicamentos myreport = new KardexMedicamentos();
                    myreport.Refresh();
                    myreport.SetDataSource(kardex);
                    crystalReportViewer1.ReportSource = myreport;
                    crystalReportViewer1.RefreshReport();
                }

                #endregion

                //#region KARDEX INSUMOS

                //else if (reporte == "KardexInsumos")
                //{
                //    DataTable reporteDatos = new DataTable();
                //    NegCertificadoMedico C = new NegCertificadoMedico();
                //    reporteDatos = NegFormulariosHCU.ReporteDatosInsumos(campo1);
                //    //His.Formulario.KardexInsumos kardex = new KardexInsumos();

                //    for (int i = 0; i < reporteDatos.Rows.Count; i++)
                //    {
                //        DataRow drKardex;
                //        drKardex = kardex.Tables["KARDEXINSUMOS"].NewRow();
                //        drKardex["Usuario"] = reporteDatos.Rows[i][0].ToString();
                //        drKardex["Departamento"] = reporteDatos.Rows[i][1].ToString();
                //        drKardex["AteCodigo"] = reporteDatos.Rows[i][2].ToString();
                //        drKardex["CueCodigo"] = reporteDatos.Rows[i][3].ToString();
                //        drKardex["Presentacion"] = reporteDatos.Rows[i][4].ToString();                        
                //        drKardex["Hora"] = reporteDatos.Rows[i][5].ToString();
                //        drKardex["FechaAdministracion"] = reporteDatos.Rows[i][6].ToString();                        
                //        drKardex["MedicamentoPropio"] = reporteDatos.Rows[i][7].ToString();
                //        drKardex["Paciente"] = reporteDatos.Rows[i][8].ToString();
                //        drKardex["Hclinica"] = reporteDatos.Rows[i][9].ToString();
                //        drKardex["Logo"] = C.path();
                //        //drKardex["Otro"] = reporteDatos.Rows[i][14].ToString();
                //        kardex.Tables["KARDEXINSUMOS"].Rows.Add(drKardex);

                //    }
                //    //Cambios Edgar 20210128
                //    KardexInsumosReporte myreport = new KardexInsumosReporte();
                //    myreport.Refresh();
                //    myreport.SetDataSource(kardex);
                //    crystalReportViewer1.ReportSource = myreport;
                //    crystalReportViewer1.RefreshReport();
                //}

                //#endregion

                #region CIERRE ADMISIONES
                else if (reporte == "CierreAdmision")
                {
                    try
                    {
                        CierredeTurno myreport = new CierredeTurno();
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
                #region FORM.021 EVOLUCION ENFERMERIA
                else if (reporte == "FORM021")
                {
                    try
                    {
                        RPTevolucionEnfermeria myreport = new RPTevolucionEnfermeria();
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
                #region FORM.022 KARDEX MEDICAMENTOS
                else if (reporte == "FORM022")
                {
                    try
                    {
                        //rptFORM022 myreport = new rptFORM022();
                        CrystalReport1 myreport = new CrystalReport1();
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
                #region FORM.022 KARDEX INSUMOS
                else if (reporte == "FORM022i")
                {
                    try
                    {
                        //rptFORM022 myreport = new rptFORM022();
                        Form022i myreport = new Form022i();
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
                #region Reporte de Edades
                else if (reporte == "ReporteEdades")
                {
                    try
                    {
                        ReporteEdadesAtenciones myreport = new ReporteEdadesAtenciones();
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
                #region ConsultaExterna
                else if (reporte == "ConsultaExterna")
                {
                    try
                    {

                        //HCU_Form002MSPrpt myreport = new HCU_Form002MSPrpt();
                        FrmConsultaExterna002 myreport = new FrmConsultaExterna002();
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
                #region Encabezado Consulta Externa
                else if (reporte == "EncConsultaExterna")
                {
                    try
                    {
                        ReportDocument myreport = new ReportDocument();
                        myreport.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\EncabezadoFrm002.rpt";
                        //TableLogOnInfo logInCon = new TableLogOnInfo();
                        //logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        //foreach (Table tb in reporteProtocolo.Database.Tables)
                        //{
                        //    tb.ApplyLogOnInfo(logInCon);
                        //}
                        //reporteProtocolo.Refresh();
                        //crystalReportViewer1.ReportSource = reporteProtocolo;
                        //crystalReportViewer1.RefreshReport();
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
                #region Copago x Item
                else if (reporte == "Copago")
                {
                    try
                    {
                        ReportDocument myreport = new ReportDocument();
                        myreport.FileName = Application.StartupPath + "\\Reportes\\CuentasPacientes\\rptDesgloseCopago.rpt";
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
                #region Ticket Dieta
                else if (reporte == "TicketDieta")
                {
                    try
                    {
                        Ticket_Dieta myreport = new Ticket_Dieta();
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

                else if (reporte == "Consentimiento")
                {
                    try
                    {
                        form024_Consentimiento_Informado myreport = new form024_Consentimiento_Informado();
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
                else if (reporte == "SignosVitalesA")
                {
                    Form020_SignosVitales myreport = new Form020_SignosVitales();
                    myreport.Refresh();
                    myreport.SetDataSource(Datos);
                    crystalReportViewer1.ReportSource = myreport;
                    crystalReportViewer1.RefreshReport();
                }
                else if(reporte == "CurvaTermica")
                {
                    rptCurvaTermica myreport = new rptCurvaTermica();                    
                    myreport.Refresh();
                    myreport.SetDataSource(Datos);
                    crystalReportViewer1.ReportSource = myreport;
                    crystalReportViewer1.RefreshReport();
                }             
                else if (reporte == "SignosVitalesB")
                {
                    rptSignosVitales myreport = new rptSignosVitales();
                    myreport.Refresh();
                    myreport.SetDataSource(Datos);
                    crystalReportViewer1.ReportSource = myreport;
                    crystalReportViewer1.RefreshReport();
                }
                else if (reporte == "IngestaEliminacion")
                {
                    rptIngestaEliminacion myreport = new rptIngestaEliminacion();
                    myreport.Refresh();
                    myreport.SetDataSource(Datos);
                    crystalReportViewer1.ReportSource = myreport;
                    crystalReportViewer1.RefreshReport();
                }
                else if (reporte == "Auditoria")
                {
                    try
                    {
                        CryAuditoriaCambios myreport = new CryAuditoriaCambios();
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
                #region Admision Dinamico
                else if (reporte == "Admision")
                {
                    try
                    {
                        //Form001 myreport = new Form001();
                        FrmAdmision001A myreport = new FrmAdmision001A();
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
                #region Egreso
                else if (reporte == "Egreso")
                {
                    try
                    {
                        FrmEgreso001B myreport = new FrmEgreso001B();
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
                else if (reporte == "TarifarioHonorario")
                {
                    try
                    {
                        HonorarioTarifario myreport = new HonorarioTarifario();
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
                else if (reporte == "HonorarioReporte")
                {
                    try
                    {
                        rptSubHonorario myreport = new rptSubHonorario();
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
                #region DETALLEITEM
                else if (reporte == "DetalleItem")
                {
                    try
                    {
                        rpt_DetalleItem myreport = new rpt_DetalleItem();
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
                #region Detalle Areas
                else if (reporte == "DetalleArea")
                {
                    try
                    {
                        rptDetalleArea myreport = new rptDetalleArea();
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
                else if (reporte == "HonorariosAsiento")
                {
                    try
                    {
                        SubAsiento myreport = new SubAsiento();
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
                else if (reporte == "PreAdmision")
                {
                    try
                    {
                        rptPreadmision myreport = new rptPreadmision();
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
                else if (reporte == "Reposicion")
                {
                    try
                    {
                        rptReposiciones myreport = new rptReposiciones();
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

                #region Laboratorio Clinico 
                else if (reporte == "Laboratorio")
                {
                    try
                    {
                        Form010A myreport = new Form010A();
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
                #region Laboratorio Clinico Excedentes
                else if (reporte == "Excedentes")
                {
                    try
                    {
                        form_ExcedentesLaboratorio myreport = new form_ExcedentesLaboratorio();
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
                #region BrazzaleteE
                else if (reporte == "BrazzaleteE")
                {
                    try
                    {
                        ReportDocument myreport = new ReportDocument();
                        myreport.FileName = Application.StartupPath + "\\Reportes\\Admision\\rpt_BrazzaleteE.rpt";
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
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            cargarReporte();
            //crystalReportViewer1.RefreshReport();            
        }

        private void imprimirreporte()
        {
            
        rptInterconsultaA myreport = new rptInterconsultaA();
            myreport.Refresh();
            myreport.SetDataSource(Datos);
            crystalReportViewer1.ReportSource = myreport;
            crystalReportViewer1.RefreshReport();
            
            
        }
        private void mailB()
        {

            try
            {
                //CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                rptInterconsultaB myreport = new rptInterconsultaB();
                myreport.Refresh();
                myreport.SetDataSource(Datos);
                DataTable dataTable = Datos.Tables["InterconsultaB"];
                string nombre = "";
                string apellido = "";
                foreach (DataRow item in dataTable.Rows)
                {
                    nombre = item["nombre1"].ToString();
                    apellido = item["apellido1"].ToString();
                }


                crystalReportViewer1.ReportSource = myreport;
                crystalReportViewer1.RefreshReport();
                crystalReportViewer1.Visible = true;

                List<DtoParametros> Informacion = new List<DtoParametros>();
                Informacion = NegUtilitarios.RecuperaInformacionCorreo();
                string[] datos = new string[10];
                foreach (var item in Informacion)
                {
                    datos = item.PAD_VALOR.Split(';');
                }




                string carpeta = @"C:\adjuntos";

                // Verificar si la carpeta existe
                if (!Directory.Exists(carpeta))
                {
                    // Crear la carpeta si no existe
                    Directory.CreateDirectory(carpeta);
                    Console.WriteLine("Carpeta creada correctamente.");
                }


                string pathAutorizado = carpeta;
                string nombrenuevo = "inteconsulta";


                myreport.ExportToDisk(ExportFormatType.PortableDocFormat, pathAutorizado + "\\" + nombrenuevo + ".pdf");


                ///envio mail
                string rutaCarpeta = pathAutorizado;
                string remitente = datos[3];


                string destinatario = med.MED_EMAIL;
                string asunto = "InterconsultaA del Paciente: " + nombre + " " + apellido;
                string cuerpoMensaje = "Adjunto se encuentra la Interconsulta del paciente: ";


                // Crear instancia de MailMessage



                destinatario = Microsoft.VisualBasic.Interaction.InputBox("Correo", "Enviar Mail a:", destinatario);
                //if (destinatario == "")
                //{
                //    MessageBox.Show("No tiene mail", "His3000",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                //}

                if (IsValidEmail(destinatario))
                {


                    MailMessage mensaje = new MailMessage(remitente, destinatario, asunto, cuerpoMensaje);

                    // Obtener lista de archivos en la carpeta
                    string[] archivos = Directory.GetFiles(rutaCarpeta);

                    // Adjuntar cada archivo al correo electrónico
                    foreach (string archivo in archivos)
                    {
                        mensaje.Attachments.Add(new Attachment(archivo));
                    }

                    // Configurar el cliente de correo electrónico
                    SmtpClient clienteSmtp = new SmtpClient(datos[1], 25);
                    clienteSmtp.EnableSsl = true;
                    clienteSmtp.UseDefaultCredentials = false;
                    string clave = datos[4];
                    clienteSmtp.Credentials = new NetworkCredential(remitente, clave);



                    try
                    {
                        // Enviar el correo electrónico
                        clienteSmtp.Send(mensaje);
                        Console.WriteLine("Correo electrónico enviado correctamente.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al enviar el correo electrónico: " + ex.Message);
                    }
                    finally
                    {
                        // Liberar recursos
                        mensaje.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("Mail Incorrecto", "Aviso");
                }

                ///

                string carpetad = @"C:\adjuntos";

                // Obtener la lista de archivos en la carpeta
                string[] archivosd = Directory.GetFiles(carpetad);

                // Eliminar cada archivo de la carpeta
                foreach (string archivo in archivosd)
                {
                    File.Delete(archivo);
                }

                myreport.Close();
                //myreport.Dispose();
            }
            catch (Exception ex)
            {

                //throw;
            }
        }
        private void mail()
        {
            //CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            rptInterconsultaA myreport = new rptInterconsultaA();
            myreport.Refresh();
            myreport.SetDataSource(Datos);
            DataTable dataTable = Datos.Tables["InterconsultaA"];
            string nombre = "";
            string apellido = "";
            foreach (DataRow item in dataTable.Rows)
            {
                nombre = item["nombre1"].ToString();
                apellido = item["apellido1"].ToString();
            }


            crystalReportViewer1.ReportSource = myreport;
            crystalReportViewer1.RefreshReport();
            crystalReportViewer1.Visible = true;

            List<DtoParametros> Informacion = new List<DtoParametros>();
            Informacion = NegUtilitarios.RecuperaInformacionCorreo();
            string[] datos = new string[10];
            foreach (var item in Informacion)
            {
                datos = item.PAD_VALOR.Split(';');
            }




            string carpeta = @"C:\adjuntos";

            // Verificar si la carpeta existe
            if (!Directory.Exists(carpeta))
            {
                // Crear la carpeta si no existe
                Directory.CreateDirectory(carpeta);
                Console.WriteLine("Carpeta creada correctamente.");
            }


            string pathAutorizado = carpeta;
            string nombrenuevo = "inteconsulta";


            myreport.ExportToDisk(ExportFormatType.PortableDocFormat, pathAutorizado + "\\" + nombrenuevo + ".pdf");


            ///envio mail
            string rutaCarpeta = pathAutorizado;
            string remitente = datos[3];


            string destinatario = med.MED_EMAIL;
            string asunto = "InterconsultaA del Paciente: " + nombre + " " + apellido;
            string cuerpoMensaje = "Adjunto se encuentra la Interconsulta del paciente: ";


            // Crear instancia de MailMessage



            destinatario = Microsoft.VisualBasic.Interaction.InputBox("Correo", "Enviar Mail a:", destinatario);
            //if (destinatario == "")
            //{
            //    MessageBox.Show("No tiene mail", "His3000",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            //}

            if (IsValidEmail(destinatario))
            {


                MailMessage mensaje = new MailMessage(remitente, destinatario, asunto, cuerpoMensaje);

                // Obtener lista de archivos en la carpeta
                string[] archivos = Directory.GetFiles(rutaCarpeta);

                // Adjuntar cada archivo al correo electrónico
                foreach (string archivo in archivos)
                {
                    mensaje.Attachments.Add(new Attachment(archivo));
                }

                // Configurar el cliente de correo electrónico
                SmtpClient clienteSmtp = new SmtpClient(datos[1], 25);
                clienteSmtp.EnableSsl = true;
                clienteSmtp.UseDefaultCredentials = false;
                string clave = datos[4];
                clienteSmtp.Credentials = new NetworkCredential(remitente, clave);



                try
                {
                    // Enviar el correo electrónico
                    clienteSmtp.Send(mensaje);
                    Console.WriteLine("Correo electrónico enviado correctamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al enviar el correo electrónico: " + ex.Message);
                }
                finally
                {
                    // Liberar recursos
                    mensaje.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Mail Incorrecto", "Aviso");
            }

            ///

            string carpetad = @"C:\adjuntos";

            // Obtener la lista de archivos en la carpeta
            string[] archivosd = Directory.GetFiles(carpetad);

            // Eliminar cada archivo de la carpeta
            foreach (string archivo in archivosd)
            {
                File.Delete(archivo);
            }

            myreport.Close();
           //myreport.Dispose();
        }
        private void mailImagen()
        {

            try
            {
                //CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                rptFrom12Imagen myreport = new rptFrom12Imagen();
                myreport.Refresh();
                myreport.SetDataSource(Datos);

                crystalReportViewer1.ReportSource = myreport;
                crystalReportViewer1.RefreshReport();
                crystalReportViewer1.Visible = true;

                List<DtoParametros> Informacion = new List<DtoParametros>();
                Informacion = NegUtilitarios.RecuperaInformacionCorreo();
                string[] datos = new string[10];
                foreach (var item in Informacion)
                {
                    datos = item.PAD_VALOR.Split(';');
                }
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = datos[0].ToString() + nombrePac + ".pdf";
                CrExportOptions = myreport.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                myreport.Export();
                myreport.Close();
                SmtpClient smtp = new SmtpClient();
                MailMessage mail = new MailMessage();
                Attachment anexar = new Attachment(datos[0].ToString() + nombrePac + ".pdf");
                mail.Attachments.Add(anexar);
                smtp.Host = datos[1].ToString();
                smtp.Port = Convert.ToInt16(datos[2].ToString());
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(datos[3].ToString(), datos[4].ToString());
                mail.From = new MailAddress(datos[3].ToString());
                mail.To.Add(new MailAddress(para));
                mail.Subject = asunto;
                mail.Body = mensaje;

                smtp.Send(mail);

                this.Close();
            }
            catch (Exception ex)
            {

                //throw;
            }
        }
        private void frmReportes_FormClosed(object sender, FormClosedEventArgs e)
        {
            crystalReportViewer1.Dispose();
        }

        public static bool IsValidEmail(string emailAddress)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
}
