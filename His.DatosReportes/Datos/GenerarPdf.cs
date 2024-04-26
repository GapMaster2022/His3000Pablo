using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Net;
using AlexPilotti.FTPS.Client;


namespace His.DatosReportes.Datos
{
    public class GenerarPdf
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
        public string clinica;
        public string nuemro_atencion;

        public int contador;
        #endregion

        public void generar()
        {
            Datos.ParametrosFTP datos = new Datos.ParametrosFTP();
            DataTable dts_parametros = new DataTable();
            dts_parametros = datos.cargar_parametrosFtP("4");// 4  cooresponde el codigo  del parametros en la BD en la tabla parametros_detalles 
            string aux_parametros = dts_parametros.Rows[0]["PAD_VALOR"].ToString();
            int x = aux_parametros.IndexOf("/");
            string aux_parametros2 = aux_parametros.Substring(x + 1);
            int y = aux_parametros2.IndexOf("/");
            string direccionFTP = aux_parametros2.Substring(y);
            string pathFtp = aux_parametros2.Substring(y + 1);

            dts_parametros = datos.cargar_parametrosFtP("1");
            string servidorFTP = dts_parametros.Rows[0]["PAD_VALOR"].ToString();

            dts_parametros = datos.cargar_parametrosFtP("2");
            string usuarioFTP = dts_parametros.Rows[0]["PAD_VALOR"].ToString();

            dts_parametros = datos.cargar_parametrosFtP("3");
            string contraseniaFTP = dts_parametros.Rows[0]["PAD_VALOR"].ToString();

            contraseniaFTP = contraseniaFTP.Trim();
            usuarioFTP = usuarioFTP.Trim();
            servidorFTP = servidorFTP.Trim();

            if (reporte == "Contrato")
            {
                try
                {
                    ReportDocument cryRpt = new ReportDocument();
                    //Reportes.Contrato contrato = new His.Admision.Reportes.Contrato();

                    cryRpt.Load(Application.StartupPath + "\\Reportes\\Admision\\Contrato.rpt");

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
                    //cryRpt.SetParameterValue("numContrato", campo7);
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



                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    //CrDiskFileDestinationOptions.DiskFileName = "c:\\CRT" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";
                    CrDiskFileDestinationOptions.DiskFileName = Application.StartupPath + "\\Reportes\\Admision\\CRT" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";
                    //string path = Application.StartupPath + "\\Reportes\\Admision\\prueba.pdf";
                    CrExportOptions = cryRpt.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    }
                    cryRpt.Export();
                }
                catch (Exception ex) { }


                try
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(servidorFTP,
                        new NetworkCredential(usuarioFTP,
                        contraseniaFTP),
                        ESSLSupportMode.ControlAndDataChannelsRequested);
                        client.MakeDir(direccionFTP.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim());
                    }
                }
                catch (Exception ex) { }

                try
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(servidorFTP,
                        new NetworkCredential(usuarioFTP, contraseniaFTP),
                        ESSLSupportMode.ControlAndDataChannelsRequested);
                        //client.PutFile("c:\\CRT" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "CRT" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        client.PutFile(Application.StartupPath + "\\Reportes\\Admision\\CRT" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "CRT" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        //System.IO.File.Delete(@"C:\CRT" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        System.IO.File.Delete(@Application.StartupPath + "\\Reportes\\Admision\\CRT" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");

                    }
                }
                catch (Exception es) { }
            }

            if (reporte == "rAdmision")
            {
                try
                {

                    ReportDocument cryRpt = new ReportDocument();

                    cryRpt.Load(Application.StartupPath + "\\Reportes\\Admision\\rPrimeraAtencion2.rpt");
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables = null;

                    cryRpt.SetParameterValue("cPaciente", campo1);
                    cryRpt.SetParameterValue("codPaciente1", campo1);
                    cryRpt.SetParameterValue("codPaciente2", campo1);
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


                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    //CrDiskFileDestinationOptions.DiskFileName = "D:\\prueba\\F001" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";
                    CrDiskFileDestinationOptions.DiskFileName = Application.StartupPath + "\\Reportes\\Admision\\F001" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";

                    CrExportOptions = cryRpt.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    }
                    cryRpt.Export();
                }
                catch (Exception ex) { }

                try
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(servidorFTP,
                        new NetworkCredential(usuarioFTP,
                        contraseniaFTP),
                        ESSLSupportMode.ControlAndDataChannelsRequested);
                        client.MakeDir(direccionFTP.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim());
                    }
                }
                catch (Exception ex) { }

                try
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(servidorFTP,
                        new NetworkCredential(usuarioFTP, contraseniaFTP),
                        ESSLSupportMode.ControlAndDataChannelsRequested);
                        //client.PutFile("c:\\F001" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F001" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        //System.IO.File.Delete(@"c:\\F001" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        client.PutFile(Application.StartupPath + "\\Reportes\\Admision\\F001" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F001" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        System.IO.File.Delete(@Application.StartupPath + "\\Reportes\\Admision\\F001" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");

                    }
                }
                catch (Exception es) { }
            }

            if (reporte == "anamnesis")
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

                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    //CrDiskFileDestinationOptions.DiskFileName = "c:\\F003" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";
                    CrDiskFileDestinationOptions.DiskFileName = Application.StartupPath + "\\Reportes\\Admision\\F003" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";

                    CrExportOptions = reporteAnamnesis.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    }
                    reporteAnamnesis.Export();
                }
                catch (Exception err)
                { throw err; }

                try
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(servidorFTP,
                        new NetworkCredential(usuarioFTP,
                        contraseniaFTP),
                        ESSLSupportMode.ControlAndDataChannelsRequested);
                        client.MakeDir(direccionFTP.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim());
                    }
                }
                catch (Exception ex) { }

                try
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(servidorFTP,
                        new NetworkCredential(usuarioFTP, contraseniaFTP),
                        ESSLSupportMode.ControlAndDataChannelsRequested);

                        //client.PutFile("c:\\F003" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F003" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        //        System.IO.File.Delete(@"c:\\F003" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        client.PutFile(Application.StartupPath + "\\Reportes\\Admision\\F003" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F003" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        System.IO.File.Delete(@Application.StartupPath + "\\Reportes\\Admision\\F003" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");


                    }
                }
                catch (Exception es) { }
            }

            //#endregion
            //#region EPICRISIS 1 - 2

            if (reporte == "epicrisis1")
            {
                try
                {
                    ReportDocument reporteEpicrisis = new ReportDocument();
                    reporteEpicrisis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\epicrisis.rpt";
                    //crystalReportViewer1.ReportSource = reporteAnamnesis;
                    TableLogOnInfo logInCon = new TableLogOnInfo();
                    logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                    foreach (Table tb in reporteEpicrisis.Database.Tables)
                    {
                        tb.ApplyLogOnInfo(logInCon);
                    }
                    reporteEpicrisis.Refresh();
                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    //CrDiskFileDestinationOptions.DiskFileName = "c:\\F0061" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";
                    CrDiskFileDestinationOptions.DiskFileName = Application.StartupPath + "\\Reportes\\Admision\\F0061" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";

                    CrExportOptions = reporteEpicrisis.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    }
                    reporteEpicrisis.Export();
                }
                catch (Exception err)
                { throw err; }
                try
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(servidorFTP,
                        new NetworkCredential(usuarioFTP,
                        contraseniaFTP),
                        ESSLSupportMode.ControlAndDataChannelsRequested);
                        client.MakeDir(direccionFTP.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim());
                    }
                }
                catch (Exception ex) { }

                try
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(servidorFTP,
                        new NetworkCredential(usuarioFTP, contraseniaFTP),
                        ESSLSupportMode.ControlAndDataChannelsRequested);
                        //client.PutFile("c:\\F0061" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F0061" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        //System.IO.File.Delete(@"c:\\F0061" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        client.PutFile(Application.StartupPath + "\\Reportes\\Admision\\F0061" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F0061" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        System.IO.File.Delete(@Application.StartupPath + "\\Reportes\\Admision\\F0061" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");

                    }
                }
                catch (Exception es) { }
            }
            if (reporte == "epicrisis2")
            {
                try
                {
                    ReportDocument reporteEpicrisis = new ReportDocument();
                    reporteEpicrisis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\epicrisis2.rpt";
                    //crystalReportViewer1.ReportSource = reporteAnamnesis;
                    TableLogOnInfo logInCon = new TableLogOnInfo();
                    logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                    foreach (Table tb in reporteEpicrisis.Database.Tables)
                    {
                        tb.ApplyLogOnInfo(logInCon);
                    }
                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    //CrDiskFileDestinationOptions.DiskFileName = "c:\\F0062" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";
                    CrDiskFileDestinationOptions.DiskFileName = Application.StartupPath + "\\Reportes\\Admision\\F0062" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";

                    CrExportOptions = reporteEpicrisis.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    }
                    reporteEpicrisis.Export();
                }
                catch (Exception err)
                { throw err; }

                try
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(servidorFTP,
                        new NetworkCredential(usuarioFTP,
                        contraseniaFTP),
                        ESSLSupportMode.ControlAndDataChannelsRequested);
                        client.MakeDir(direccionFTP.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim());
                    }
                }
                catch (Exception ex) { }

                try
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(servidorFTP,
                        new NetworkCredential(usuarioFTP, contraseniaFTP),
                        ESSLSupportMode.ControlAndDataChannelsRequested);
                        //client.PutFile("c:\\F0062" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F0062" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        //System.IO.File.Delete(@"c:\\F0062" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        client.PutFile(Application.StartupPath + "\\Reportes\\Admision\\F0062" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F0062" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        System.IO.File.Delete(@Application.StartupPath + "\\Reportes\\Admision\\F0062" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");

                    }
                }
                catch (Exception es) { }
            }

            if (reporte == "protocolo")
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

                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    //CrDiskFileDestinationOptions.DiskFileName = "c:\\F017" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";
                    CrDiskFileDestinationOptions.DiskFileName = Application.StartupPath + "\\Reportes\\Admision\\F017" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";

                    CrExportOptions = reporteProtocolo.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    }
                    reporteProtocolo.Export();
                }
                catch (Exception err)
                { throw err; }

                try
                {
                    //using (FTPSClient client = new FTPSClient())
                    //{
                    //    client.Connect(servidorFTP,
                    //    new NetworkCredential(usuarioFTP,
                    //    contraseniaFTP),
                    //    ESSLSupportMode.ControlAndDataChannelsRequested);
                    //    client.MakeDir(direccionFTP.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim());
                    //}
                }
                catch (Exception ex) { }

                try
                {
                    //using (FTPSClient client = new FTPSClient())
                    //{
                    //    client.Connect(servidorFTP,
                    //    new NetworkCredential(usuarioFTP, contraseniaFTP),
                    //    ESSLSupportMode.ControlAndDataChannelsRequested);
                    //    //client.PutFile("c:\\F017" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F017" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                    //    //System.IO.File.Delete(@"c:\\F017" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                    //    client.PutFile(Application.StartupPath + "\\Reportes\\Admision\\F017" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F017" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                    //    System.IO.File.Delete(@Application.StartupPath + "\\Reportes\\Admision\\F017" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");

                    //}
                }
                catch (Exception es) { }
            }

            if (reporte == "evolucion")
            {
                try
                {
                    ReportDocument reporteEvolucion = new ReportDocument();
                    reporteEvolucion.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\rEvolucion.rpt";
                    //crystalReportViewer1.ReportSource = reporteAnamnesis;
                    TableLogOnInfo logInCon = new TableLogOnInfo();
                    logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                    foreach (Table tb in reporteEvolucion.Database.Tables)
                    {
                        tb.ApplyLogOnInfo(logInCon);
                    }
                    reporteEvolucion.Refresh();
                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    //CrDiskFileDestinationOptions.DiskFileName = "c:\\F005" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";
                    CrDiskFileDestinationOptions.DiskFileName = Application.StartupPath + "\\Reportes\\Admision\\F005" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";

                    CrExportOptions = reporteEvolucion.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    }
                    reporteEvolucion.Export();
                }
                catch (Exception err)
                { throw err; }

                try
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(servidorFTP,
                        new NetworkCredential(usuarioFTP,
                        contraseniaFTP),
                        ESSLSupportMode.ControlAndDataChannelsRequested);
                        client.MakeDir(direccionFTP.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim());
                    }
                }
                catch (Exception ex) { }

                try
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(servidorFTP,
                        new NetworkCredential(usuarioFTP, contraseniaFTP),
                        ESSLSupportMode.ControlAndDataChannelsRequested);
                        //client.PutFile("c:\\F005" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F005" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        //System.IO.File.Delete(@"c:\\F005" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        client.PutFile(Application.StartupPath + "\\Reportes\\Admision\\F005" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F005" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        System.IO.File.Delete(@Application.StartupPath + "\\Reportes\\Admision\\F005" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");

                    }
                }
                catch (Exception es) { }

            }

            if (reporte == "interconsulta")
            {
                try
                {
                    ReportDocument reporteInterconsulta1 = new ReportDocument();
                    reporteInterconsulta1.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\interconsulta.rpt";
                    //crystalReportViewer1.ReportSource = reporteAnamnesis;
                    TableLogOnInfo logInCon = new TableLogOnInfo();
                    logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                    foreach (Table tb in reporteInterconsulta1.Database.Tables)
                    {
                        tb.ApplyLogOnInfo(logInCon);
                    }
                    reporteInterconsulta1.Refresh();
                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    //CrDiskFileDestinationOptions.DiskFileName = "c:\\F0071" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";
                    CrDiskFileDestinationOptions.DiskFileName = Application.StartupPath + "\\Reportes\\Admision\\F0071" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";

                    CrExportOptions = reporteInterconsulta1.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    }
                    reporteInterconsulta1.Export();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                try
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(servidorFTP,
                        new NetworkCredential(usuarioFTP,
                        contraseniaFTP),
                        ESSLSupportMode.ControlAndDataChannelsRequested);
                        client.MakeDir(direccionFTP.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim());
                    }
                }
                catch (Exception ex) { }

                try
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(servidorFTP,
                        new NetworkCredential(usuarioFTP, contraseniaFTP),
                        ESSLSupportMode.ControlAndDataChannelsRequested);
                        //client.PutFile("c:\\F0071" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F0071" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        //System.IO.File.Delete(@"c:\\F0071" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        client.PutFile(Application.StartupPath + "\\Reportes\\Admision\\F0071" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F0071" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        System.IO.File.Delete(@Application.StartupPath + "\\Reportes\\Admision\\F0071" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");

                    }
                }
                catch (Exception es) { }

            }
            if (reporte == "interconsulta2")
            {
                try
                {
                    ReportDocument reporteInterconsulta2 = new ReportDocument();
                    reporteInterconsulta2.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\interconsulta2.rpt";
                    //crystalReportViewer1.ReportSource = reporteAnamnesis;
                    TableLogOnInfo logInCon = new TableLogOnInfo();
                    logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                    foreach (Table tb in reporteInterconsulta2.Database.Tables)
                    {
                        tb.ApplyLogOnInfo(logInCon);
                    }
                    reporteInterconsulta2.Refresh();
                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    //CrDiskFileDestinationOptions.DiskFileName = "c:\\F0072" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";
                    CrDiskFileDestinationOptions.DiskFileName = Application.StartupPath + "\\Reportes\\Admision\\F0072" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";

                    CrExportOptions = reporteInterconsulta2.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    }
                    reporteInterconsulta2.Export();

                }
                catch (Exception ex)
                { throw ex; }

                try
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(servidorFTP,
                        new NetworkCredential(usuarioFTP,
                        contraseniaFTP),
                        ESSLSupportMode.ControlAndDataChannelsRequested);
                        client.MakeDir(direccionFTP.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim());
                    }
                }
                catch (Exception ex) { }

                try
                {
                    using (FTPSClient client = new FTPSClient())
                    {
                        client.Connect(servidorFTP,
                        new NetworkCredential(usuarioFTP, contraseniaFTP),
                        ESSLSupportMode.ControlAndDataChannelsRequested);
                        //client.PutFile("c:\\F0072" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F0072" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        //System.IO.File.Delete(@"c:\\F0072" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        client.PutFile(Application.StartupPath + "\\Reportes\\Admision\\F0072" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F0072" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                        System.IO.File.Delete(@Application.StartupPath + "\\Reportes\\Admision\\F0072" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");

                    }
                }
                catch (Exception es) { }

            }

            //if (reporte == "epicrisis1")
            //{
            //    try
            //    {
            //        ReportDocument reporteEpicrisis = new ReportDocument();
            //        reporteEpicrisis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\epicrisis.rpt";
            //        //crystalReportViewer1.ReportSource = reporteAnamnesis;
            //        TableLogOnInfo logInCon = new TableLogOnInfo();
            //        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

            //        foreach (Table tb in reporteEpicrisis.Database.Tables)
            //        {
            //            tb.ApplyLogOnInfo(logInCon);
            //        }

            //        ExportOptions CrExportOptions;
            //        DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            //        PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            //        CrDiskFileDestinationOptions.DiskFileName = "c:\\F0051" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";
            //        CrExportOptions = reporteEpicrisis.ExportOptions;
            //        {
            //            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            //            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            //            CrExportOptions.FormatOptions = CrFormatTypeOptions;
            //        }
            //        reporteEpicrisis.Export();
            //    }
            //    catch (Exception err)
            //    { throw err; }
            //    try
            //    {
            //        using (FTPSClient client = new FTPSClient())
            //        {
            //            client.Connect(servidorFTP,
            //            new NetworkCredential(usuarioFTP,
            //            contraseniaFTP),
            //            ESSLSupportMode.ControlAndDataChannelsRequested);
            //            client.MakeDir(direccionFTP.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim());
            //        }
            //    }
            //    catch (Exception ex) { }

            //    try
            //    {
            //        using (FTPSClient client = new FTPSClient())
            //        {
            //            client.Connect(servidorFTP,
            //            new NetworkCredential(usuarioFTP, contraseniaFTP),
            //            ESSLSupportMode.ControlAndDataChannelsRequested);
            //            client.PutFile("c:\\F0051" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F0051" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
            //        }
            //    }
            //    catch (Exception es) { }
            //}
            //if (reporte == "epicrisis2")
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
            //        ExportOptions CrExportOptions;
            //        DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            //        PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            //        CrDiskFileDestinationOptions.DiskFileName = "c:\\F0052" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";
            //        CrExportOptions = reporteEpicrisis.ExportOptions;
            //        {
            //            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            //            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            //            CrExportOptions.FormatOptions = CrFormatTypeOptions;
            //        }
            //        reporteEpicrisis.Export();
            //    }
            //    catch (Exception err)
            //    { throw err; }

            //    try
            //    {
            //        using (FTPSClient client = new FTPSClient())
            //        {
            //            client.Connect(servidorFTP,
            //            new NetworkCredential(usuarioFTP,
            //            contraseniaFTP),
            //            ESSLSupportMode.ControlAndDataChannelsRequested);
            //            client.MakeDir(direccionFTP.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim());
            //        }
            //    }
            //    catch (Exception ex) { }

            //    try
            //    {
            //        using (FTPSClient client = new FTPSClient())
            //        {
            //            client.Connect(servidorFTP,
            //            new NetworkCredential(usuarioFTP, contraseniaFTP),
            //            ESSLSupportMode.ControlAndDataChannelsRequested);
            //            client.PutFile("c:\\F0052" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F0051" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
            //        }
            //    }
            //    catch (Exception es) { }
            //}




        }
    }
}
