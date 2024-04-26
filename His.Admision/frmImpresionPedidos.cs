using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using His.Negocio;
using System.Data.SqlClient;
using His.Entidades;
using His.Formulario;

namespace His.Admision
{
    public partial class frmImpresionPedidos : Form
    {
        int NumeroPedidoReporte = 0;
        int AreaPedido = 0;
        int VisualizaPantalla = 0;
        int tipo;
        public static bool reimpresion = false;
                public string impresoraDefecto = "";

        public frmImpresionPedidos(int NumeroPedido, int Area,int Visualiza,int pedev)
        {
            NumeroPedidoReporte = NumeroPedido;
            AreaPedido = Area;
            VisualizaPantalla = Visualiza;
            tipo = pedev;
            InitializeComponent();
        }
        public frmImpresionPedidos(int NumeroPedido, int Area, string nombreImpresora, int pedev)
        {
            NumeroPedidoReporte = NumeroPedido;
            AreaPedido = Area;
            impresoraDefecto = nombreImpresora;
            tipo = pedev;
            InitializeComponent();
        }


        private void frmImpresionPedidos_Load(object sender, EventArgs e)
        {
            if (impresoraDefecto == "")
                CargarReporte(NumeroPedidoReporte);//Imprimira de manera normal
            else
                ImpresoraDefectoReporte(NumeroPedidoReporte);
        }
        private void ImpresoraDefectoReporte(int NumeroPedido)
        {
            string PrinterName = impresoraDefecto;
            string NombreReporte = "";
            DataTable ds1 = new DataTable();
            DataTable ImpresoraArea = new DataTable();
            dtsImpresionPedido ds2 = new dtsImpresionPedido();
            DataRow dr2;
            if (tipo == 1)
            {
                ds1 = NegPedidos.DatosPedido(NumeroPedido);
            }
            else if (tipo == 3)
                ds1 = NegPedidos.DatosPedido(NumeroPedido);
            else
            {
                ds1 = NegPedidos.DatosPedido2(NumeroPedido);
            }
            ImpresoraArea = NegPedidos.DatosImpresionPedido(AreaPedido);

            if (ImpresoraArea.Rows.Count > 0)
            {
                if (tipo == 3)
                    NombreReporte = "rptImpresionPedido_con valores.rpt";
                else
                    NombreReporte = ImpresoraArea.Rows[0]["OBSERVACION2"].ToString();
            }
            else
            {
                NombreReporte = "rptImpresionPedidoGrande.rpt";
            }

            if (tipo == 0)
                NombreReporte = "rptImpresionDevolucion.rpt";



            //ds1 = ds2.Tables.Add("Pedidos");
            USUARIOS datosUsuario = new USUARIOS();

            if (ds1 != null && ds1.Rows.Count > 0)
            {
                datosUsuario = NegUsuarios.RecuperaUsuario(Convert.ToInt16(ds1.Rows[0][4].ToString()));
                if (tipo == 1)
                {
                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        dr2 = ds2.Tables["Pedidos"].NewRow();

                        dr2["PACIENTE"] = dr1["PACIENTE"];
                        dr2["IDENTIFICACION"] = dr1["IDENTIFICACION"];
                        dr2["FECHA"] = dr1["PED_FECHA"];
                        dr2["PEDIDO"] = dr1["NUMERO PEDIDO"];
                        dr2["CODIGO_USUARIO"] = dr1["CODIGO USUARIO"];
                        dr2["USUARIO"] = datosUsuario.APELLIDOS + " " + datosUsuario.NOMBRES;
                        dr2["MEDICO"] = dr1["MEDICOS"];
                        dr2["PRO_CODIGO"] = dr1["PRO_CODIGO"];
                        dr2["PRODUCTO"] = dr1["Producto"];
                        dr2["PDD_CANTIDAD"] = dr1["PDD_CANTIDAD"];
                        dr2["PDD_VALOR"] = dr1["PDD_VALOR"];
                        dr2["PDD_IVA"] = dr1["PDD_IVA"];
                        dr2["PDD_TOTAL"] = dr1["PDD_TOTAL"];
                        dr2["ASEGURADORA"] = dr1["ASEGURADORA"];
                        dr2["HISTORIA"] = dr1["HISTORIA"];
                        dr2["ATENCION"] = dr1["ATENCION"];
                        dr2["HABITACION"] = dr1["HABITACION"];
                        if (reimpresion)
                            dr2["ESTACION"] = "REIMPRESION - " + dr1["ESTACION"];
                        else
                            dr2["ESTACION"] = dr1["ESTACION"];
                        dr2["OBSERVACION"] = dr1["OBSERVACION"];

                        ds2.Tables["Pedidos"].Rows.Add(dr2);
                        reimpresion = false;
                    }
                }
                else
                {
                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        dr2 = ds2.Tables["Pedidos"].NewRow();

                        dr2["PACIENTE"] = dr1["PACIENTE"];
                        dr2["IDENTIFICACION"] = dr1["IDENTIFICACION"];
                        dr2["FECHA"] = dr1["PED_FECHA"];
                        dr2["PEDIDO"] = dr1["NUMERO PEDIDO"];
                        dr2["CODIGO_USUARIO"] = dr1["CODIGO USUARIO"];
                        dr2["USUARIO"] = datosUsuario.APELLIDOS + " " + datosUsuario.NOMBRES;
                        dr2["MEDICO"] = dr1["MEDICOS"];
                        dr2["PRO_CODIGO"] = dr1["PRO_CODIGO"];
                        dr2["PRODUCTO"] = dr1["Producto"];
                        dr2["PDD_CANTIDAD"] = dr1["PDD_CANTIDAD"];
                        dr2["PDD_VALOR"] = dr1["PDD_VALOR"];
                        dr2["PDD_IVA"] = dr1["PDD_IVA"];
                        dr2["PDD_TOTAL"] = dr1["PDD_TOTAL"];

                        dr2["HISTORIA"] = dr1["HISTORIA"];
                        dr2["ATENCION"] = dr1["ATENCION"];
                        dr2["HABITACION"] = dr1["HABITACION"];
                        if (reimpresion)
                            dr2["ESTACION"] = "REIMPRESION - " + dr1["ESTACION"];
                        else
                            dr2["ESTACION"] = dr1["ESTACION"];
                        dr2["OBSERVACION"] = dr1["OBSERVACION"];

                        ds2.Tables["Pedidos"].Rows.Add(dr2);
                        reimpresion = false;
                    }
                }
                //ReportDocument reporte = new ReportDocument();
                //Reporte = reporte;
                //Reporte.Load(Server.MapPath("..\\Crystals\\crpListadoMedidas2.rpt"));
                //Reporte.Database.Tables["Medida"].SetDataSource(ds2.Tables["Medida"]);
                //crvReporte.ReportSource = Reporte;
                //crvReporte.DataBind();
                if (tipo == 3)
                {
                    if (impresoraDefecto != "")
                    {
                        rptImpresionPredido_valores myreport = new rptImpresionPredido_valores();
                        myreport.Refresh();
                        myreport.SetDataSource(ds2);
                        myreport.PrintOptions.PrinterName = "";
                        myreport.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize;
                        myreport.PrintOptions.PrinterName = PrinterName;
                        myreport.PrintToPrinter(1, false, 0, 0);
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("No se a definido una impresora para esta area. El reporte se mostrara en pantalla.");
                        His.Formulario.frmReportes myreport = new His.Formulario.frmReportes(1, "PedidoConValores", ds2);
                        myreport.Show();
                    }
                }
                else
                {
                    ReportDocument reporte = new ReportDocument();
                    reporte.FileName = Application.StartupPath + "\\Reportes\\Pedidos\\" + NombreReporte;
                    reporte.Database.Tables["Pedidos"].SetDataSource(ds2.Tables["Pedidos"]);

                    if (impresoraDefecto != "")
                    {
                        reporte.PrintOptions.PrinterName = "";
                        reporte.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize;
                        reporte.PrintOptions.PrinterName = PrinterName;
                        reporte.PrintToPrinter(1, false, 0, 0);
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("No se a definido una impresora para esta area. El reporte se mostrara en pantalla.");
                        crystalReportViewer1.ReportSource = reporte;
                        crystalReportViewer1.Refresh();
                    }
                }                
            }
            else
            {
                //MostarMensaje("No existen registros para esta Matriz.");
            }





            ////ds1 = ds2.Tables.Add("Pedidos");               

            //if (ds1 != null && ds1.Rows.Count > 0)
            //{
            //    if (tipo == 1)
            //    {
            //        foreach (DataRow dr1 in ds1.Rows)
            //        {
            //            dr2 = ds2.Tables["Pedidos"].NewRow();

            //            dr2["PACIENTE"] = dr1["PACIENTE"];
            //            dr2["IDENTIFICACION"] = dr1["IDENTIFICACION"];
            //            dr2["FECHA"] = dr1["PED_FECHA"];
            //            dr2["PEDIDO"] = dr1["NUMERO PEDIDO"];
            //            dr2["CODIGO_USUARIO"] = dr1["CODIGO USUARIO"];
            //            dr2["USUARIO"] = dr1["USUARIO"];
            //            dr2["MEDICO"] = dr1["MEDICOS"];
            //            dr2["PRO_CODIGO"] = dr1["PRO_CODIGO"];
            //            dr2["PRODUCTO"] = dr1["Producto"];
            //            dr2["PDD_CANTIDAD"] = dr1["PDD_CANTIDAD"];
            //            dr2["PDD_VALOR"] = dr1["PDD_VALOR"];
            //            dr2["PDD_IVA"] = dr1["PDD_IVA"];
            //            dr2["PDD_TOTAL"] = dr1["PDD_TOTAL"];

            //            dr2["HISTORIA"] = dr1["HISTORIA"];
            //            dr2["ATENCION"] = dr1["ATENCION"];
            //            dr2["HABITACION"] = dr1["HABITACION"];
            //            dr2["ESTACION"] = dr1["ESTACION"];
            //            dr2["OBSERVACION"] = dr1["OBSERVACION"];

            //            ds2.Tables["Pedidos"].Rows.Add(dr2);
            //        }
            //    }
            //    else if (tipo == 3)
            //    {
            //        foreach (DataRow dr1 in ds1.Rows)
            //        {
            //            dr2 = ds2.Tables["Pedidos"].NewRow();

            //            dr2["PACIENTE"] = dr1["PACIENTE"];
            //            dr2["IDENTIFICACION"] = dr1["IDENTIFICACION"];
            //            dr2["FECHA"] = dr1["PED_FECHA"];
            //            dr2["PEDIDO"] = dr1["NUMERO PEDIDO"];
            //            dr2["CODIGO_USUARIO"] = dr1["CODIGO USUARIO"];
            //            dr2["USUARIO"] = dr1["USUARIO"];
            //            dr2["MEDICO"] = dr1["MEDICOS"];
            //            dr2["PRO_CODIGO"] = dr1["PRO_CODIGO"];
            //            dr2["PRODUCTO"] = dr1["Producto"];
            //            dr2["PDD_CANTIDAD"] = dr1["PDD_CANTIDAD"];
            //            dr2["PDD_VALOR"] = dr1["PDD_VALOR"];
            //            dr2["PDD_IVA"] = dr1["PDD_IVA"];
            //            dr2["PDD_TOTAL"] = dr1["PDD_TOTAL"];

            //            dr2["HISTORIA"] = dr1["HISTORIA"];
            //            dr2["ATENCION"] = dr1["ATENCION"];
            //            dr2["HABITACION"] = dr1["HABITACION"];
            //            dr2["ESTACION"] = dr1["ESTACION"];
            //            dr2["OBSERVACION"] = dr1["OBSERVACION"];

            //            ds2.Tables["Pedidos"].Rows.Add(dr2);
            //        }
            //    }
            //    else
            //    {
            //        foreach (DataRow dr1 in ds1.Rows)
            //        {
            //            dr2 = ds2.Tables["Pedidos"].NewRow();

            //            dr2["PACIENTE"] = dr1["PACIENTE"];
            //            dr2["IDENTIFICACION"] = dr1["IDENTIFICACION"];
            //            dr2["FECHA"] = dr1["FECHA"];
            //            dr2["PEDIDO"] = dr1["NUMERO PEDIDO"];
            //            dr2["CODIGO_USUARIO"] = dr1["CODIGO USUARIO"];
            //            dr2["USUARIO"] = dr1["USUARIO"];
            //            dr2["MEDICO"] = dr1["MEDICOS"];
            //            dr2["PRO_CODIGO"] = dr1["PRO_CODIGO"];
            //            dr2["PRODUCTO"] = dr1["Producto"];
            //            dr2["PDD_CANTIDAD"] = dr1["PDD_CANTIDAD"];
            //            dr2["PDD_VALOR"] = dr1["PDD_VALOR"];
            //            dr2["PDD_IVA"] = dr1["PDD_IVA"];
            //            dr2["PDD_TOTAL"] = dr1["PDD_TOTAL"];

            //            dr2["HISTORIA"] = dr1["HISTORIA"];
            //            dr2["ATENCION"] = dr1["ATENCION"];
            //            dr2["HABITACION"] = dr1["HABITACION"];
            //            dr2["ESTACION"] = dr1["ESTACION"];
            //            dr2["OBSERVACION"] = dr1["OBSERVACION"];

            //            ds2.Tables["Pedidos"].Rows.Add(dr2);
            //        }
            //    }
            //    //ReportDocument reporte = new ReportDocument();
            //    //Reporte = reporte;
            //    //Reporte.Load(Server.MapPath("..\\Crystals\\crpListadoMedidas2.rpt"));
            //    //Reporte.Database.Tables["Medida"].SetDataSource(ds2.Tables["Medida"]);
            //    //crvReporte.ReportSource = Reporte;
            //    //crvReporte.DataBind();

            //    ReportDocument reporte = new ReportDocument();
            //    reporte.FileName = Application.StartupPath + "\\Reportes\\Pedidos\\" + NombreReporte;
            //    reporte.Database.Tables["Pedidos"].SetDataSource(ds2.Tables["Pedidos"]);

            //    if (ImpresoraArea.Rows.Count > 0)
            //    {
            //        if (Convert.ToBoolean(ImpresoraArea.Rows[0]["IMPRIME"]) == true)
            //        {

            //            VisualizaPantalla = Convert.ToInt32(ImpresoraArea.Rows[0]["OBSERVACION1"]); // si OBSERVACION1=1 muestra en pantalla

            //            if (VisualizaPantalla == 1)
            //            {
            //                crystalReportViewer1.ReportSource = reporte;
            //                crystalReportViewer1.Refresh();
            //            }
            //            else // caso contrario imprime en la impresora designada
            //            {
            //                reporte.PrintOptions.PrinterName = "";
            //                reporte.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize;
            //                reporte.PrintOptions.PrinterName = ImpresoraArea.Rows[0]["NOMBRE_IMPRESORA"].ToString();
            //                reporte.PrintToPrinter(1, false, 1, 1);
            //                this.Dispose();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("No se a definido una impresora para esta area. El reporte se mostrara en pantalla.");
            //        crystalReportViewer1.ReportSource = reporte;
            //        crystalReportViewer1.Refresh();
            //    }
            //}
            //else
            //{
            //    //MostarMensaje("No existen registros para esta Matriz.");
            //}
        }

        private void CargarReporte(int NumeroPedido)
        {
            string PrinterName = "";
            string NombreReporte = "";
            DataTable ds1 = new DataTable();
            DataTable ImpresoraArea = new DataTable();
            dtsImpresionPedido ds2 = new dtsImpresionPedido();
            DataRow dr2;
            if (tipo == 1)
            {
                ds1 = NegPedidos.DatosPedido(NumeroPedido);
            }
            else if (tipo == 3)
                ds1 = NegPedidos.DatosPedido(NumeroPedido);
            else
            {
                ds1 = NegPedidos.DatosPedido2(NumeroPedido);
            }
            ImpresoraArea = NegPedidos.DatosImpresionPedido(AreaPedido);

            if (ImpresoraArea.Rows.Count > 0)
            {
                if (tipo == 3)
                    NombreReporte = "rptImpresionPedido_con valores.rpt";
                else
                    NombreReporte = ImpresoraArea.Rows[0]["OBSERVACION2"].ToString();
            }
            else
            {
                NombreReporte = "rptImpresionPedidoGrande.rpt";
            }

            if (tipo == 0)
            {
                NombreReporte = "rptImpresionDevolucion.rpt";
                
            }



            //ds1 = ds2.Tables.Add("Pedidos");               
            USUARIOS datosUsuario = new USUARIOS();

            if (ds1 != null && ds1.Rows.Count > 0)
            {
                datosUsuario = NegUsuarios.RecuperaUsuario(Convert.ToInt16(ds1.Rows[0][4].ToString()));
                if (tipo == 1)
                {
                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        dr2 = ds2.Tables["Pedidos"].NewRow();

                        dr2["PACIENTE"] = dr1["PACIENTE"];
                        dr2["IDENTIFICACION"] = dr1["IDENTIFICACION"];
                        dr2["FECHA"] = dr1["PED_FECHA"];
                        dr2["PEDIDO"] = dr1["NUMERO PEDIDO"];
                        dr2["CODIGO_USUARIO"] = dr1["CODIGO USUARIO"];
                        dr2["USUARIO"] = datosUsuario.APELLIDOS + " " + datosUsuario.NOMBRES;
                        //dr2["USUARIO"] = dr1["USUARIO"];
                        dr2["MEDICO"] = dr1["MEDICOS"];
                        dr2["PRO_CODIGO"] = dr1["PRO_CODIGO"];
                        dr2["PRODUCTO"] = dr1["Producto"];
                        dr2["PDD_CANTIDAD"] = dr1["PDD_CANTIDAD"];
                        dr2["PDD_VALOR"] = dr1["PDD_VALOR"];
                        dr2["PDD_IVA"] = dr1["PDD_IVA"];
                        dr2["PDD_TOTAL"] = dr1["PDD_TOTAL"];

                        dr2["HISTORIA"] = dr1["HISTORIA"];
                        dr2["ATENCION"] = dr1["ATENCION"];
                        dr2["HABITACION"] = dr1["HABITACION"];
                        if (reimpresion)
                            dr2["ESTACION"] = "REIMPRESION - " + dr1["ESTACION"];
                        else
                            dr2["ESTACION"] = dr1["ESTACION"];
                        dr2["OBSERVACION"] = dr1["OBSERVACION"];

                        ds2.Tables["Pedidos"].Rows.Add(dr2);
                    }
                }
                else if(tipo == 3)
                {
                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        dr2 = ds2.Tables["Pedidos"].NewRow();

                        dr2["PACIENTE"] = dr1["PACIENTE"];
                        dr2["IDENTIFICACION"] = dr1["IDENTIFICACION"];
                        dr2["FECHA"] = dr1["PED_FECHA"];
                        dr2["PEDIDO"] = dr1["NUMERO PEDIDO"];
                        dr2["CODIGO_USUARIO"] = dr1["CODIGO USUARIO"];
                        dr2["USUARIO"] = datosUsuario.APELLIDOS + " " + datosUsuario.NOMBRES;
                        //dr2["USUARIO"] = dr1["USUARIO"];
                        dr2["MEDICO"] = dr1["MEDICOS"];
                        dr2["PRO_CODIGO"] = dr1["PRO_CODIGO"];
                        dr2["PRODUCTO"] = dr1["Producto"];
                        dr2["PDD_CANTIDAD"] = dr1["PDD_CANTIDAD"];
                        dr2["PDD_VALOR"] = dr1["PDD_VALOR"];
                        dr2["PDD_IVA"] = dr1["PDD_IVA"];
                        dr2["PDD_TOTAL"] = dr1["PDD_TOTAL"];

                        dr2["HISTORIA"] = dr1["HISTORIA"];
                        dr2["ATENCION"] = dr1["ATENCION"];
                        dr2["HABITACION"] = dr1["HABITACION"];
                        if (reimpresion)
                            dr2["ESTACION"] = "REIMPRESION - " + dr1["ESTACION"];
                        else
                            dr2["ESTACION"] = dr1["ESTACION"];
                        dr2["OBSERVACION"] = dr1["OBSERVACION"];
                        ds2.Tables["Pedidos"].Rows.Add(dr2);
                    }
                }
                else
                {
                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        dr2 = ds2.Tables["Pedidos"].NewRow();

                        dr2["PACIENTE"] = dr1["PACIENTE"];
                        dr2["IDENTIFICACION"] = dr1["IDENTIFICACION"];
                        dr2["FECHA"] = dr1["PED_FECHA"];
                        dr2["PEDIDO"] = dr1["NUMERO PEDIDO"];
                        dr2["CODIGO_USUARIO"] = dr1["CODIGO USUARIO"];
                        dr2["USUARIO"] = datosUsuario.APELLIDOS + " " + datosUsuario.NOMBRES;
                        //dr2["USUARIO"] = dr1["USUARIO"];
                        dr2["MEDICO"] = dr1["MEDICOS"];
                        dr2["PRO_CODIGO"] = dr1["PRO_CODIGO"];
                        dr2["PRODUCTO"] = dr1["Producto"];
                        dr2["PDD_CANTIDAD"] = dr1["PDD_CANTIDAD"];
                        dr2["PDD_VALOR"] = dr1["PDD_VALOR"];
                        dr2["PDD_IVA"] = dr1["PDD_IVA"];
                        dr2["PDD_TOTAL"] = dr1["PDD_TOTAL"];

                        dr2["HISTORIA"] = dr1["HISTORIA"];
                        dr2["ATENCION"] = dr1["ATENCION"];
                        dr2["HABITACION"] = dr1["HABITACION"];
                        if (reimpresion)
                            dr2["ESTACION"] = "REIMPRESION - " + dr1["ESTACION"];
                        else
                            dr2["ESTACION"] = dr1["ESTACION"];
                        dr2["OBSERVACION"] = dr1["OBSERVACION"];
                        ds2.Tables["Pedidos"].Rows.Add(dr2);
                    }
                    //foreach (DataRow dr1 in ds1.Rows)
                    //{
                    //    dr2 = ds2.Tables["Pedidos"].NewRow();

                    //    dr2["PACIENTE"] = dr1["PACIENTE"];
                    //    dr2["IDENTIFICACION"] = dr1["IDENTIFICACION"];
                    //    dr2["FECHA"] = dr1["PED_FECHA"];
                    //    dr2["PEDIDO"] = dr1["NUMERO PEDIDO"];
                    //    dr2["CODIGO_USUARIO"] = dr1["CODIGO USUARIO"];
                    //    dr2["USUARIO"] = dr1["USUARIO"];
                    //    dr2["MEDICO"] = dr1["MEDICOS"];
                    //    dr2["PRO_CODIGO"] = dr1["PRO_CODIGO"];
                    //    dr2["PRODUCTO"] = dr1["Producto"];
                    //    dr2["PDD_CANTIDAD"] = dr1["PDD_CANTIDAD"];
                    //    dr2["PDD_VALOR"] = dr1["PDD_VALOR"];
                    //    dr2["PDD_IVA"] = dr1["PDD_IVA"];
                    //    dr2["PDD_TOTAL"] = dr1["PDD_TOTAL"];

                    //    dr2["HISTORIA"] = dr1["HISTORIA"];
                    //    dr2["ATENCION"] = dr1["ATENCION"];
                    //    dr2["HABITACION"] = dr1["HABITACION"];
                    //    dr2["ESTACION"] = dr1["ESTACION"];
                    //    dr2["OBSERVACION"] = dr1["OBSERVACION"];

                    //    ds2.Tables["Pedidos"].Rows.Add(dr2);
                    //}
                }
                //ReportDocument reporte = new ReportDocument();
                //Reporte = reporte;
                //Reporte.Load(Server.MapPath("..\\Crystals\\crpListadoMedidas2.rpt"));
                //Reporte.Database.Tables["Medida"].SetDataSource(ds2.Tables["Medida"]);
                //crvReporte.ReportSource = Reporte;
                //crvReporte.DataBind();

                ReportDocument reporte = new ReportDocument();
                reporte.FileName = Application.StartupPath + "\\Reportes\\Pedidos\\" + NombreReporte;
                reporte.Database.Tables["Pedidos"].SetDataSource(ds2.Tables["Pedidos"]);

                if (impresoraDefecto != "")
                {
                    reporte.PrintOptions.PrinterName = "";
                    reporte.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize;
                    reporte.PrintOptions.PrinterName = PrinterName;
                    reporte.PrintToPrinter(1, false, 1, 1);
                    this.Dispose();
                }
                else
                {
                    //MessageBox.Show("No se a definido una impresora para esta area. El reporte se mostrara en pantalla.");
                    crystalReportViewer1.ReportSource = reporte;
                    crystalReportViewer1.Refresh();
                }
            }
            else
            {
                //MostarMensaje("No existen registros para esta Matriz.");
            }
        }
    }
}
