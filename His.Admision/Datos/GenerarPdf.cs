using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



using System.ComponentModel;
using System.Drawing;
using His.Parametros;
using System.Reflection;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using His.Negocio;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Net;
using His.General;
using AlexPilotti.FTPS.Client;


namespace His.Admision.Datos
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

            dts_parametros = datos.cargar_parametrosFtP("6");
            string servidorFTP = dts_parametros.Rows[0]["PAD_VALOR"].ToString();

            dts_parametros = datos.cargar_parametrosFtP("7");
            string usuarioFTP = dts_parametros.Rows[0]["PAD_VALOR"].ToString();

            dts_parametros = datos.cargar_parametrosFtP("8");
            string contraseniaFTP = dts_parametros.Rows[0]["PAD_VALOR"].ToString();



            if (reporte == "Contrato")
            {
                try
                {
                    ReportDocument cryRpt = new ReportDocument();
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
                    CrDiskFileDestinationOptions.DiskFileName = "c:\\CRT" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";
                    CrExportOptions = cryRpt.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    }
                    cryRpt.Export();
                }
                catch (Exception ex) { Console.WriteLine(ex); }


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
                        client.PutFile("c:\\CRT" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "CRT" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
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
                    CrDiskFileDestinationOptions.DiskFileName = "c:\\F001" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf";
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
                        client.PutFile("c:\\F001" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf", pathFtp.Trim() + clinica.Trim() + "/" + nuemro_atencion.Trim() + "/" + "F001" + clinica.Trim() + "" + nuemro_atencion.Trim() + ".pdf");
                    }
                }
                catch (Exception es) { }
            }
        }
    }
}
                 

                      