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
using His.Formulario;
using His.Entidades.Clases;
using System.Threading;
using System.IO;

namespace CuentaPaciente
{
    public partial class frmReporteDesgloseFactura : Form
    {
        string _NumeroFactura = "";
        string _Cliente = "";
        string _Ruc = "";
        string _Telefono = "";
        string _Direccion = "";
        Int64 _CodigoAtencion = 0;
        string _Tipo = "";
        string _CadenaAtenciones = "";
        Int32 _MedicoCodigo = 0;
        string _Fecha1 = "";
        string _Fecha2 = "";
        string DirectorioReporte = "";
        Int64 _NumeroTramite = 0;
        string _Medico = "";
        string _Procedimiento = "";
        string _SubTotal = "";
        string _Desceunto = "";
        string _SinIva = "";
        string _ConIva = "";
        string _Iva = "";
        string _Total = "";
        string _CONVENIO = "";
        public string pEmision = "";
        string pathAutorizado = "";
        string nombrenuevo = "";
        public frmReporteDesgloseFactura(string NumeroFactura, string Cliente, string Ruc, string Telefono, string Direccion, Int32 CodigoAtencion, string Tipo, string CadenaAtenciones, Int32 MedicoCodigo, string Fecha1, string Fecha2, Int64 NumeroTramite, int CodMedico, string Procedimiento, string IVA, string CONIVA, string CONVENIO, string SINIVA, string SUBTOTAL, string DESCUENTO, string TOTAL, string _pathAutorizado = "", string _nombrenuevo = "")
        {
            _NumeroFactura = NumeroFactura;
            _CONVENIO = CONVENIO;
            _Cliente = Cliente;
            _Ruc = Ruc;
            _Telefono = Telefono;
            _Direccion = Direccion;
            _CodigoAtencion = CodigoAtencion;
            _Tipo = Tipo;
            _CadenaAtenciones = CadenaAtenciones;
            //_MedicoCodigo=NegFactura.MedicoCodigo(MedicoCodigo);
            _Fecha1 = Fecha1;
            _Fecha2 = Fecha2;
            _NumeroTramite = NumeroTramite;
            _Medico = NegFactura.Medico(CodigoAtencion);
            _Procedimiento = Procedimiento;
            _SubTotal = SUBTOTAL;
            _Desceunto = DESCUENTO;
            _SinIva = SINIVA;
            _Iva = IVA;
            _ConIva = CONIVA;
            _Total = TOTAL;
            pathAutorizado = _pathAutorizado;
            nombrenuevo = _nombrenuevo;
            InitializeComponent();
        }

        public static double SignificantTruncate(double num, int significantDigits)
        {
            double y = Math.Pow(10, significantDigits);
            return Math.Round(num * y) / y;
        }
        public string clave;
        public void GeneraCodigo(string tbCadena)
        {
            tbCadena = tbCadena.Replace("-", "").ToString();
            string cadena = tbCadena;
            string cadenaInversa = "";
            string tbInversa;
            string tbCadenaMultiplos = "";
            string tbSumatoria;
            string tbMod11;
            string tbDigitoVerificador;
            string tbResta11MenosMod;
            string tbClaveAcceso;
            for (int i = cadena.Length - 1; i >= 0; i--)
            {
                cadenaInversa += cadena[i];
            }
            tbInversa = cadenaInversa.ToString();
            string[] cadenaParalela = { "2", "3", "4", "5", "6", "7", "2", "3", "4", "5", "6", "7", "2", "3", "4", "5", "6", "7", "2", "3", "4", "5", "6", "7", "2", "3", "4", "5", "6", "7", "2", "3", "4", "5", "6", "7", "2", "3", "4", "5", "6", "7", "2", "3", "4", "5", "6", "7" };
            foreach (string valorMultiplo in cadenaParalela)
            {
                tbCadenaMultiplos += "[" + valorMultiplo + "]";
            }

            char[] arregloInverso = cadenaInversa.ToCharArray();

            int sumatoria = 0;
            for (int i = 0; i < arregloInverso.Length; i++)
            {
                sumatoria += int.Parse(arregloInverso[i].ToString()) * int.Parse(cadenaParalela[i]);
            }

            tbSumatoria = sumatoria.ToString();
            int restoDevision = sumatoria % 11;

            tbMod11 = restoDevision.ToString();
            int resta11Mod = 11 - restoDevision;
            tbResta11MenosMod = resta11Mod.ToString();

            int digitoVericadorNum = resta11Mod;
            if (resta11Mod == 11)
                digitoVericadorNum = 0;
            if (resta11Mod == 10)
                digitoVericadorNum = 1;
            tbDigitoVerificador = digitoVericadorNum.ToString();
            tbClaveAcceso = tbCadena + digitoVericadorNum.ToString();
            //txtfacturaelectronica.AppendText("  <claveAcceso>" + tbClaveAcceso + "</claveAcceso>\r\n");
            clave = tbClaveAcceso;
        }
        private void frmReporteDesgloseFactura_Load(object sender, EventArgs e)
        {
            string numCaja = _NumeroFactura.Substring(0, 3);
            pEmision = NegFactura.ValidaPEmision(numCaja);
            EMPRESA empresa = NegEmpresa.RecuperaEmpresa();
            DataTable Tabla = NegFactura.RecuperaLeyenda();
            string leyenda = "";
            string contablidad = "";
            string contribuyente = "";
            for (int i = 0; i < Tabla.Rows.Count; i++)
            {
                if (Tabla.Rows[i]["LEYENDA"].ToString() != "")
                    leyenda = Tabla.Rows[i]["LEYENDA"].ToString();
                if (Tabla.Rows[i]["CONTRIBUYENTE"].ToString() != "")
                    contribuyente = "Contribuyente Especial #: " + Tabla.Rows[i]["CONTRIBUYENTE"].ToString();
                if (Tabla.Rows[i]["CONTABILIDAD"].ToString() != "")
                    contablidad = "Obligado a llevar Contabilidad: " + Tabla.Rows[i]["CONTABILIDAD"].ToString();
            }
            if (_Tipo == "FACTURA")
            {
                DataTable ds1 = new DataTable();
                // string detalleiva;
                dtsDesgloseFactura ds2 = new dtsDesgloseFactura();
                DataRow dr2;

                ds1 = NegFactura.DatosFacturaDesglose(_CodigoAtencion);



                if (ds1 != null && ds1.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        dr2 = ds2.Tables["DatosFactura"].NewRow();
                        dr2["LOGO"] = NegUtilitarios.RutaLogo("General");
                        dr2["NUMERO_FACTURA"] = _NumeroFactura;
                        dr2["CLIENTE"] = _Cliente;
                        dr2["RUC"] = _Ruc;
                        dr2["TELEFONO"] = _Telefono;
                        dr2["PACIENTE"] = dr1["DATOS"];
                        dr2["HC"] = dr1["HISTORIA"];
                        dr2["ATENCION"] = dr1["ATENCION"];
                        dr2["FECHA_INGRESO"] = dr1["INGRESO"];
                        dr2["FECHA_ALTA"] = dr1["ALTA"];
                        dr2["GRUPO"] = dr1["GRUPO"];
                        dr2["FECHA_CUENTA"] = dr1["FECHA_INGRESO"];
                        dr2["DESCRIPCION"] = dr1["DESCRIPCION"];
                        dr2["CANTIDAD"] = dr1["CANTIDAD"];
                        dr2["VALOR_UNITARIO"] = Convert.ToString(SignificantTruncate(Convert.ToDouble(dr1["VALOR_UNITARIO"]), 6));
                        dr2["TOTAL"] = Convert.ToString(SignificantTruncate(Convert.ToDouble(dr1["TOTAL"]), 6));  ///definir cambio ALEX
                        dr2["USUARIO"] = His.Entidades.Clases.Sesion.nomUsuario;
                        dr2["MEDICO"] = dr1["MEDICO"];
                        dr2["IVA"] = Convert.ToDouble(SignificantTruncate(Convert.ToDouble(dr1["IVA"]), 4));
                        dr2["DESCUENTO"] = Convert.ToDouble(SignificantTruncate(Convert.ToDouble(dr1["DESCUENTO"]), 4));
                        //dr2["CLAVEACCESO"] = clave;
                        ds2.Tables["DatosFactura"].Rows.Add(dr2);
                    }

                    ReportDocument reporte = new ReportDocument();

                    if (pEmision != "003")
                        reporte.FileName = Application.StartupPath + "\\Reportes\\CuentasPacientes\\rptDesgloseFactura.rpt";
                    else
                        reporte.FileName = Application.StartupPath + "\\Reportes\\CuentasPacientes\\rptDesgloseFacturaMushuñan.rpt";
                    reporte.Database.Tables["DatosFactura"].SetDataSource(ds2.Tables["DatosFactura"]);

                    crystalReportViewer1.ReportSource = reporte;
                    crystalReportViewer1.Refresh();

                }
            }
            if (_Tipo == "AgrupacionCuentasDetalle")
            {
                DataTable ds1 = new DataTable();
                DataTable AGRUPACION_CUENTAS = new DataTable();
                // string detalleiva;
                dtsDesgloseFactura ds2 = new dtsDesgloseFactura();
                DataRow dr2;

                AGRUPACION_CUENTAS = NegFactura.DetalleCuentaAgrupada(_CodigoAtencion);

                ds1 = NegFactura.DatosAgrupaFacturaDesglose(Convert.ToInt32(AGRUPACION_CUENTAS.Rows[0][1].ToString()));
                //foreach (DataRow item in AGRUPACION_CUENTAS.Rows)
                //{
                //    dr2 = ds2.Tables["DatosFacturaAgrupa"].NewRow();
                //    //dr2["DIRECCION"] = item["DATOS"];
                //    dr2["ATENCION"] = item["ATE_NUMERO_ATENCION"];
                //    ds2.Tables["DatosFacturaAgrupa"].Rows.Add(dr2);
                //}


                if (ds1 != null && ds1.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        dr2 = ds2.Tables["DatosFactura"].NewRow();
                        dr2["LOGO"] = NegUtilitarios.RutaLogo("General");
                        dr2["NUMERO_FACTURA"] = _NumeroFactura;
                        dr2["CLIENTE"] = _Cliente;
                        dr2["RUC"] = _Ruc;
                        dr2["TELEFONO"] = _Telefono;
                        dr2["PACIENTE"] = dr1["DATOS"];
                        dr2["HC"] = dr1["HISTORIA"];
                        dr2["ATENCION"] = dr1["ATENCION"];
                        dr2["FECHA_INGRESO"] = dr1["INGRESO"];
                        dr2["FECHA_ALTA"] = dr1["ALTA"];
                        dr2["GRUPO"] = dr1["GRUPO"];
                        dr2["FECHA_CUENTA"] = dr1["FECHA_INGRESO"];
                        dr2["DESCRIPCION"] = dr1["DESCRIPCION"];
                        dr2["CANTIDAD"] = dr1["CANTIDAD"];
                        dr2["VALOR_UNITARIO"] = Convert.ToString(SignificantTruncate(Convert.ToDouble(dr1["VALOR_UNITARIO"]), 6));
                        dr2["TOTAL"] = Convert.ToString(SignificantTruncate(Convert.ToDouble(dr1["TOTAL"]), 6));  ///definir cambio ALEX
                        dr2["USUARIO"] = His.Entidades.Clases.Sesion.nomUsuario;
                        dr2["MEDICO"] = AGRUPACION_CUENTAS.Rows[0][11].ToString();
                        dr2["IVA"] = Convert.ToDouble(SignificantTruncate(Convert.ToDouble(dr1["IVA"]), 4));
                        dr2["DESCUENTO"] = Convert.ToDouble(SignificantTruncate(Convert.ToDouble(dr1["DESCUENTO"]), 4));
                        //dr2["CLAVEACCESO"] = clave;
                        ds2.Tables["DatosFactura"].Rows.Add(dr2);
                    }
                    rptDesgloseFacturAgrupa myreport = new rptDesgloseFacturAgrupa();
                    myreport.Refresh();
                    myreport.SetDataSource(ds2);
                    crystalReportViewer1.ReportSource = myreport;
                    crystalReportViewer1.RefreshReport();
                    //ReportDocument reporte = new ReportDocument();

                    //if (pEmision != "003")
                    //    reporte.FileName = Application.StartupPath + "\\Reportes\\CuentasPacientes\\rptDesgloseFacturAgrupa.rpt";
                    //else
                    //    reporte.FileName = Application.StartupPath + "\\Reportes\\CuentasPacientes\\rptDesgloseFacturAgrupa.rpt";
                    //reporte.Database.Tables["DatosFactura"].SetDataSource(ds2.Tables["DatosFactura"]);

                    //crystalReportViewer1.ReportSource = reporte;
                    //crystalReportViewer1.Refresh();
                }
            }
            if (_Tipo == "FACTURAxFECHA")
            {
                DataTable ds1 = new DataTable();
                // string detalleiva;
                dtsDesgloseFactura ds2 = new dtsDesgloseFactura();
                DataRow dr2;

                ds1 = NegFactura.DatosFacturaDesglosexFecha(_CodigoAtencion, Convert.ToDateTime(_Fecha1).Date, Convert.ToDateTime(_Fecha2).Date);



                if (ds1 != null && ds1.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        dr2 = ds2.Tables["DatosFactura"].NewRow();
                        dr2["LOGO"] = NegUtilitarios.RutaLogo("General");
                        dr2["NUMERO_FACTURA"] = _NumeroFactura;
                        dr2["CLIENTE"] = _Cliente;
                        dr2["RUC"] = _Ruc;
                        dr2["TELEFONO"] = _Telefono;
                        dr2["PACIENTE"] = dr1["DATOS"];
                        dr2["HC"] = dr1["HISTORIA"];
                        dr2["ATENCION"] = dr1["ATENCION"];
                        dr2["FECHA_INGRESO"] = dr1["INGRESO"];
                        dr2["FECHA_ALTA"] = dr1["ALTA"];
                        dr2["GRUPO"] = dr1["GRUPO"];
                        dr2["FECHA_CUENTA"] = dr1["FECHA_INGRESO"];
                        dr2["DESCRIPCION"] = dr1["DESCRIPCION"];
                        dr2["CANTIDAD"] = dr1["CANTIDAD"];
                        dr2["VALOR_UNITARIO"] = Convert.ToString(SignificantTruncate(Convert.ToDouble(dr1["VALOR_UNITARIO"]), 6));
                        dr2["TOTAL"] = Convert.ToString(SignificantTruncate(Convert.ToDouble(dr1["TOTAL"]), 6));  ///definir cambio ALEX
                        dr2["USUARIO"] = His.Entidades.Clases.Sesion.nomUsuario;
                        dr2["MEDICO"] = dr1["MEDICO"];
                        dr2["IVA"] = Convert.ToDouble(SignificantTruncate(Convert.ToDouble(dr1["IVA"]), 4));
                        dr2["DESCUENTO"] = dr1["DESCUENTO"];
                        //dr2["CLAVEACCESO"] = clave;
                        ds2.Tables["DatosFactura"].Rows.Add(dr2);
                    }

                    ReportDocument reporte = new ReportDocument();

                    if (pEmision != "003")
                        reporte.FileName = Application.StartupPath + "\\Reportes\\CuentasPacientes\\rptDesgloseFactura.rpt";
                    else
                        reporte.FileName = Application.StartupPath + "\\Reportes\\CuentasPacientes\\rptDesgloseFacturaMushuñan.rpt";

                    reporte.Database.Tables["DatosFactura"].SetDataSource(ds2.Tables["DatosFactura"]);

                    crystalReportViewer1.ReportSource = reporte;
                    crystalReportViewer1.Refresh();
                }
            }

            if (_Tipo == "FACTURA_IMPRESION")
            {
                DataTable ds1 = new DataTable();
                DataTable TablaFormasPago = new DataTable();

                dtsDesgloseFactura ds2 = new dtsDesgloseFactura();
                DataRow dr2;
                _NumeroFactura = _NumeroFactura.Replace("-", "");
                ds1 = NegFactura.DatosFacturaTotal(_CodigoAtencion, _NumeroFactura);

                DataTable Empresa = NegFactura.RecuperaEmpresa();
                DataTable FechaPaciente = NegFactura.FechaPaciente(_CodigoAtencion);
                DataTable Parametros = NegFactura.RecuperaParametros();

                ATENCIONES DatosAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(_CodigoAtencion));


                TablaFormasPago = NegFactura.FormasPagoFactura(_NumeroFactura);


                string FormaP = "";
                foreach (DataRow item in TablaFormasPago.Rows)
                {
                    if (item[0].ToString() == "01")
                        FormaP = FormaP + "SIN UTILIZACION DEL SISTEMA FINANCIERO - ";
                    else if (item[0].ToString() == "15")
                        FormaP = FormaP + "COMPENSACIÓN DE DEUDAS - ";
                    else if (item[0].ToString() == "16")
                        FormaP = FormaP + "TARJETA DE DÉBITO - ";
                    else if (item[0].ToString() == "17")
                        FormaP = FormaP + "DINERO ELECTRÓNICO - ";
                    else if (item[0].ToString() == "18")
                        FormaP = FormaP + "TARJETA PREPAGO - ";
                    else if (item[0].ToString() == "19")
                        FormaP = FormaP + "TARJETA DE CRÉDITO - ";
                    else if (item[0].ToString() == "20")
                        FormaP = FormaP + "OTROS CON UTILIZACION DEL SISTEMA FINANCIERO - ";
                    else if (item[0].ToString() == "21")
                        FormaP = FormaP + "ENDOSO DE TÍTULOS - ";

                }
                string referido = "";

                if (pathAutorizado != "" && nombrenuevo != "")
                {

                    FormaP += "\r\nDOCUMENTO SIN VALIDES TRIBUTARIA.";
                }


                    DataTable Ref = NegDetalleCuenta.ReferidoPaciente(_CodigoAtencion);
                if (Ref.Rows.Count > 0)
                    referido = Ref.Rows[0][0].ToString();

                string numcontrol = _NumeroFactura.Substring(3, _NumeroFactura.Length - 3);

                string fecha = FechaPaciente.Rows[0][4].ToString();
                fecha = fecha.Substring(0, 10);
                string cadena = fecha.Substring(0, 2) + fecha.Substring(3, 2) + fecha.Substring(6, 4) +
                    "01" + Empresa.Rows[0][3].ToString() + Parametros.Rows[0][3].ToString() + "001" + _NumeroFactura.Substring(0, 3)
                    + numcontrol + numcontrol.Substring(1, 8) + "1";
                GeneraCodigo(cadena);

                if (ds1 != null && ds1.Rows.Count > 0)
                {
                    DataTable ds3 = new DataTable();
                    ds3 = NegFactura.DatosFacturaTotalPago(_NumeroFactura);
                    string tarjeta = "";
                    tarjeta = ds1.Rows[0]["TARJETA"].ToString();
                    string contado = "";
                    string cheque = "";
                    string credito = "";
                    int contadorpago = 0;
                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        dr2 = ds2.Tables["DatosFacturaTotal"].NewRow();

                        dr2["NUMERO_FACTURA"] = _NumeroFactura;
                        dr2["LOGO"] = NegUtilitarios.RutaLogo("General");
                        dr2["EMPRESA"] = Sesion.nomEmpresa;
                        dr2["RUC_EMPRESA"] = empresa.EMP_RUC;
                        dr2["TELF_EMPRESA"] = empresa.EMP_TELEFONO;
                        dr2["DIRECCION_EMPRESA"] = empresa.EMP_DIRECCION;
                        dr2["CONTRIBUYENTE"] = contribuyente;
                        dr2["CONTABILIDAD"] = contablidad;
                        dr2["LEYENDA"] = leyenda;
                        dr2["CLIENTE"] = _Cliente;
                        dr2["RUC"] = _Ruc;
                        dr2["TELEFONO"] = _Telefono;
                        dr2["PACIENTE"] = dr1["PACIENTE"];
                        dr2["HC"] = dr1["HISTORIA"];
                        dr2["ATENCION"] = dr1["ATENCION"];
                        dr2["HABITACION"] = dr1["HABITACION"];
                        dr2["PACIENTE_DIRECCION"] = _Direccion;
                        dr2["FECHA_INGRESO"] = dr1["INGRESO"];
                        dr2["FECHA_ALTA"] = dr1["ALTA"];
                        //if (dr1["RUBRO"].ToString() == "SERVICIO DE CLINICA" && _CONVENIO != "CATEGORIA 0 ")
                        //    dr2["RUBRO"] = "COPIAS HISTORIA CLINICA";
                        dr2["RUBRO"] = dr1["RUBRO"];
                        dr2["DESCUENTO"] = _Desceunto;
                        dr2["VALOR"] = dr1["VALOR"]; ;
                        dr2["IVA"] = _Iva;//alex  _IVA cambio por dr1["IVA"]
                        dr2["IVA0"] = _SinIva;
                        dr2["IVA12"] = _ConIva;//alex  _CONIVA cambio por dr1["IVA12"]
                        dr2["MEDICO"] = _Medico;
                        dr2["PROCEDIMIENTO"] = _Procedimiento;

                        //if (contadorpago == 0)
                        //{
                        //    contadorpago = 1;
                        //    foreach (DataRow dr3 in ds3.Rows)
                        //    {

                        //            contado = contado + "  " + dr3["4"].ToString() + "  " + dr3["1"].ToString();                                                                    
                        //    }
                        //}
                        //dr2["CONTADO"] = contado + tarjeta + cheque + credito + " \n\r ";
                        dr2["CONTADO"] = dr1["CONTADO"];
                        dr2["TARJETA"] = dr1["TARJETA"];
                        dr2["CHEQUE"] = dr1["CHEQUE"];
                        dr2["CREDITO"] = dr1["CREDITO"];
                        dr2["BANCO"] = dr1["BANCO"];
                        dr2["USUARIO"] = TablaFormasPago.Rows[0][1].ToString();
                        dr2["CLAVEACCESO"] = clave;
                        dr2["FORMA_PAGO"] = FormaP;
                        dr2["REFERIDO"] = referido;
                        string[] fe = TablaFormasPago.Rows[0][2].ToString().Split(' ');
                        dr2["FECHA_FACTURA"] = fe[0];
                        dr2["BRIGADAS_MEDICAS"] = "BRIGADA MEDICA";
                        ds2.Tables["DatosFacturaTotal"].Rows.Add(dr2);
                    }
                }
                ReportDocument reporte = new ReportDocument();

                //VALIDA USUARIO DE MUSHUÑAN
                if (pEmision != "003")
                    reporte.FileName = Application.StartupPath + "\\Reportes\\CuentasPacientes\\rptFacturaFinal.rpt";
                else
                    reporte.FileName = Application.StartupPath + "\\Reportes\\CuentasPacientes\\rptFacturaFinalMushuñan.rpt";


                reporte.Database.Tables["DatosFacturaTotal"].SetDataSource(ds2.Tables["DatosFacturaTotal"]);
                if (pathAutorizado == "" && nombrenuevo == "")
                {
                    crystalReportViewer1.ReportSource = reporte;
                    crystalReportViewer1.Refresh();
                }
                else
                {
                    reporte.ExportToDisk(ExportFormatType.PortableDocFormat, pathAutorizado + "\\" + nombrenuevo + ".pdf");
                    reporte.Close();
                    reporte.Dispose();
                }
            }
            if (_Tipo == "ESTADO_CUENTA")
            {
                DataTable ds1 = new DataTable();

                dtsDesgloseFactura ds2 = new dtsDesgloseFactura();
                DataRow dr2;

                ds1 = NegFactura.DatosFacturaTotal(_CodigoAtencion, _NumeroFactura);

                if (ds1 != null && ds1.Rows.Count > 0)
                {
                    DataTable ds3 = new DataTable();
                    ds3 = NegFactura.DatosFacturaTotalPago(_NumeroFactura);
                    string tarjeta = "";
                    tarjeta = ds1.Rows[0]["TARJETA"].ToString();
                    string contado = "";
                    string cheque = "";
                    string credito = "";
                    int contadorpago = 0;
                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        dr2 = ds2.Tables["DatosFacturaTotal"].NewRow();

                        dr2["NUMERO_FACTURA"] = _NumeroFactura;
                        dr2["LOGO"] = NegUtilitarios.RutaLogo("General");
                        dr2["EMPRESA"] = Sesion.nomEmpresa;
                        dr2["RUC_EMPRESA"] = empresa.EMP_RUC;
                        dr2["TELF_EMPRESA"] = empresa.EMP_TELEFONO;
                        dr2["DIRECCION_EMPRESA"] = empresa.EMP_DIRECCION;
                        dr2["CONTRIBUYENTE"] = contribuyente;
                        dr2["CONTABILIDAD"] = contablidad;
                        dr2["LEYENDA"] = leyenda;
                        dr2["CLIENTE"] = _Cliente;
                        dr2["RUC"] = _Ruc;
                        dr2["TELEFONO"] = _Telefono;
                        dr2["PACIENTE"] = dr1["PACIENTE"];
                        dr2["HC"] = dr1["HISTORIA"];
                        dr2["ATENCION"] = dr1["ATENCION"];
                        dr2["HABITACION"] = dr1["HABITACION"];
                        dr2["PACIENTE_DIRECCION"] = _Direccion;
                        dr2["FECHA_INGRESO"] = dr1["INGRESO"];
                        dr2["FECHA_ALTA"] = dr1["ALTA"];
                        //if (dr1["RUBRO"].ToString() == "SERVICIO DE CLINICA" && _CONVENIO != "CATEGORIA 0 ")
                        //    dr2["RUBRO"] = "COPIAS HISTORIA CLINICA";
                        //else
                        dr2["RUBRO"] = dr1["RUBRO"];
                        dr2["DESCUENTO"] = _Desceunto;
                        dr2["VALOR"] = dr1["VALOR"];
                        dr2["IVA"] = _Iva;//alex  _IVA cambio por dr1["IVA"]
                        dr2["IVA0"] = _SinIva;
                        dr2["IVA12"] = _ConIva;//alex  _CONIVA cambio por dr1["IVA12"]
                        dr2["MEDICO"] = _Medico;
                        dr2["PROCEDIMIENTO"] = _Procedimiento;

                        //if (contadorpago == 0)
                        //{
                        //    contadorpago = 1;
                        //    foreach (DataRow dr3 in ds3.Rows)
                        //    {

                        //            contado = contado + "  " + dr3["4"].ToString() + "  " + dr3["1"].ToString();                                                                    
                        //    }
                        //}
                        //dr2["CONTADO"] = contado + tarjeta + cheque + credito + " \n\r ";
                        dr2["CONTADO"] = dr1["CONTADO"];
                        dr2["TARJETA"] = dr1["TARJETA"];
                        dr2["CHEQUE"] = dr1["CHEQUE"];
                        dr2["CREDITO"] = dr1["CREDITO"];
                        dr2["BANCO"] = dr1["BANCO"];
                        dr2["USUARIO"] = His.Entidades.Clases.Sesion.nomUsuario;
                        ds2.Tables["DatosFacturaTotal"].Rows.Add(dr2);
                    }
                }
                ReportDocument reporte = new ReportDocument();
                reporte.FileName = Application.StartupPath + "\\Reportes\\CuentasPacientes\\rptFacturaEstado.rpt";

                reporte.Database.Tables["DatosFacturaTotal"].SetDataSource(ds2.Tables["DatosFacturaTotal"]);
                crystalReportViewer1.ReportSource = reporte;
                crystalReportViewer1.Refresh();
            }
            if (_Tipo == "ESTADO_CUENTA_X_Fecha")
            {
                DataTable ds1 = new DataTable();
                DataTable ds4 = new DataTable();
                dtsDesgloseFactura ds2 = new dtsDesgloseFactura();
                DataRow dr2;

                ds1 = NegFactura.DatosFacturaTotalxFecha(_CodigoAtencion, _NumeroFactura, Convert.ToDateTime(_Fecha1).Date, Convert.ToDateTime(_Fecha2).Date);
                ds4 = NegFactura.RecuperaTemporales(_CodigoAtencion);

                decimal iva = 0, iva0 = 0, iva12 = 0;

                foreach (DataRow item in ds1.Rows)
                {
                    iva += Convert.ToDecimal(item["IVA"]);
                    iva0 += Convert.ToDecimal(item["IVA0"]);
                    iva12 += Convert.ToDecimal(item["IVA12"]);
                }
                foreach (DataRow item in ds4.Rows)
                {
                    iva0 += Convert.ToDecimal(item["Total"]);
                }

                if (ds1 != null && ds1.Rows.Count > 0)
                {
                    DataTable ds3 = new DataTable();
                    ds3 = NegFactura.DatosFacturaTotalPago(_NumeroFactura);
                    string tarjeta = "";
                    tarjeta = ds1.Rows[0]["TARJETA"].ToString();
                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        dr2 = ds2.Tables["DatosFacturaTotal"].NewRow();

                        dr2["NUMERO_FACTURA"] = _NumeroFactura;
                        dr2["LOGO"] = NegUtilitarios.RutaLogo("General");
                        dr2["EMPRESA"] = Sesion.nomEmpresa;
                        dr2["RUC_EMPRESA"] = empresa.EMP_RUC;
                        dr2["TELF_EMPRESA"] = empresa.EMP_TELEFONO;
                        dr2["DIRECCION_EMPRESA"] = empresa.EMP_DIRECCION;
                        dr2["CONTRIBUYENTE"] = contribuyente;
                        dr2["CONTABILIDAD"] = contablidad;
                        dr2["LEYENDA"] = leyenda;
                        dr2["CLIENTE"] = _Cliente;
                        dr2["RUC"] = _Ruc;
                        dr2["TELEFONO"] = _Telefono;
                        dr2["PACIENTE"] = dr1["PACIENTE"];
                        dr2["HC"] = dr1["HISTORIA"];
                        dr2["ATENCION"] = dr1["ATENCION"];
                        dr2["HABITACION"] = dr1["HABITACION"];
                        dr2["PACIENTE_DIRECCION"] = _Direccion;
                        dr2["FECHA_INGRESO"] = dr1["INGRESO"];
                        dr2["FECHA_ALTA"] = dr1["ALTA"];
                        dr2["RUBRO"] = dr1["RUBRO"];
                        dr2["DESCUENTO"] = 0;
                        dr2["VALOR"] = dr1["VALOR"];
                        dr2["IVA"] = iva;//alex  _IVA cambio por dr1["IVA"]
                        dr2["IVA0"] = iva0;
                        dr2["IVA12"] = iva12;//alex  _CONIVA cambio por dr1["IVA12"]
                        dr2["MEDICO"] = 0;
                        dr2["PROCEDIMIENTO"] = "";
                        dr2["CONTADO"] = dr1["CONTADO"];
                        dr2["TARJETA"] = dr1["TARJETA"];
                        dr2["CHEQUE"] = dr1["CHEQUE"];
                        dr2["CREDITO"] = dr1["CREDITO"];
                        dr2["BANCO"] = dr1["BANCO"];
                        dr2["USUARIO"] = "";
                        ds2.Tables["DatosFacturaTotal"].Rows.Add(dr2);
                    }
                }
                if (ds1 != null && ds1.Rows.Count > 0)
                {
                    DataTable ds3 = new DataTable();
                    ds3 = NegFactura.DatosFacturaTotalPago(_NumeroFactura);
                    string tarjeta = "";
                    tarjeta = ds1.Rows[0]["TARJETA"].ToString();
                    foreach (DataRow dr1 in ds4.Rows)
                    {
                        dr2 = ds2.Tables["DatosFacturaTotal"].NewRow();

                        dr2["NUMERO_FACTURA"] = _NumeroFactura;
                        dr2["LOGO"] = NegUtilitarios.RutaLogo("General");
                        dr2["EMPRESA"] = Sesion.nomEmpresa;
                        dr2["RUC_EMPRESA"] = empresa.EMP_RUC;
                        dr2["TELF_EMPRESA"] = empresa.EMP_TELEFONO;
                        dr2["DIRECCION_EMPRESA"] = empresa.EMP_DIRECCION;
                        dr2["CONTRIBUYENTE"] = contribuyente;
                        dr2["CONTABILIDAD"] = contablidad;
                        dr2["LEYENDA"] = leyenda;
                        dr2["CLIENTE"] = _Cliente;
                        dr2["RUC"] = _Ruc;
                        dr2["TELEFONO"] = _Telefono;
                        dr2["PACIENTE"] = "";
                        dr2["HC"] = "";
                        dr2["ATENCION"] = "";
                        dr2["HABITACION"] = "";
                        dr2["PACIENTE_DIRECCION"] = _Direccion;
                        dr2["FECHA_INGRESO"] = DateTime.Now;
                        dr2["FECHA_ALTA"] = DateTime.Now;
                        dr2["RUBRO"] = dr1["Detalle"];
                        dr2["DESCUENTO"] = 0;
                        dr2["VALOR"] = dr1["Total"];
                        dr2["IVA"] = iva;//alex  _IVA cambio por dr1["IVA"]
                        dr2["IVA0"] = iva0;
                        dr2["IVA12"] = iva12;//alex  _CONIVA cambio por dr1["IVA12"]
                        dr2["MEDICO"] = 0;
                        dr2["PROCEDIMIENTO"] = "";
                        dr2["CONTADO"] = "";
                        dr2["TARJETA"] = "";
                        dr2["CHEQUE"] = "";
                        dr2["CREDITO"] = "";
                        dr2["BANCO"] = "";
                        dr2["USUARIO"] = "";
                        ds2.Tables["DatosFacturaTotal"].Rows.Add(dr2);
                    }
                }
                NegFactura.BorraTemporales(_CodigoAtencion);
                ReportDocument reporte = new ReportDocument();
                reporte.FileName = Application.StartupPath + "\\Reportes\\CuentasPacientes\\rptFacturaEstado.rpt";

                reporte.Database.Tables["DatosFacturaTotal"].SetDataSource(ds2.Tables["DatosFacturaTotal"]);
                crystalReportViewer1.ReportSource = reporte;
                crystalReportViewer1.Refresh();
            }
            if (_Tipo == "CUENTAS")
            {
                DataTable ds1 = new DataTable();
                dtsDesgloseFactura ds2 = new dtsDesgloseFactura();
                DataRow dr2;

                ds1 = NegFactura.DatosConsolidado(_CadenaAtenciones);

                if (ds1 != null && ds1.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        dr2 = ds2.Tables["ConsolidadoCtas"].NewRow();

                        dr2["PACIENTE"] = dr1["PACIENTE"];
                        dr2["GRUPO"] = dr1["RUB_GRUPO"];
                        dr2["RADICACION"] = dr1["RADICACION"];
                        dr2["TRAMITE"] = dr1["TRAMITE"];
                        dr2["IVA"] = dr1["iva"];
                        dr2["TOTAL"] = dr1["TotalCuenta"];

                        ds2.Tables["ConsolidadoCtas"].Rows.Add(dr2);
                    }

                    ReportDocument reporte = new ReportDocument();
                    reporte.FileName = Application.StartupPath + "\\Reportes\\CuentasPacientes\\rptConsolidadoCuentas.rpt";
                    reporte.Database.Tables["ConsolidadoCtas"].SetDataSource(ds2.Tables["ConsolidadoCtas"]);

                    crystalReportViewer1.ReportSource = reporte;
                    crystalReportViewer1.Refresh();
                }
            }

            if (_Tipo == "HONORARIOS")
            {
                DataTable ds1 = new DataTable();
                dtsDesgloseFactura ds2 = new dtsDesgloseFactura();
                DataRow dr2;

                ds1 = NegFactura.GeneraHonorariosMedicos(_MedicoCodigo, Convert.ToDateTime(_Fecha1), Convert.ToDateTime(_Fecha2), _NumeroTramite);

                if (ds1 != null && ds1.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        dr2 = ds2.Tables["HonorariosMedicos"].NewRow();

                        dr2["PACIENTE"] = dr1["PACIENTE"];
                        dr2["HISTORIA"] = dr1["HISTORIA"];
                        dr2["ATENCION"] = dr1["ATENCION"];
                        dr2["FECHA"] = dr1["FECHA"];
                        dr2["DETALLE"] = dr1["DETALLE"];
                        dr2["VALOR_UNITARIO"] = dr1["VALOR_UNITARIO"];
                        dr2["CANTIDAD"] = dr1["CANTIDAD"];
                        dr2["IVA"] = dr1["IVA"];
                        dr2["TOTAL"] = dr1["TOTAL"];
                        dr2["MEDICO"] = dr1["MEDICO"];
                        dr2["TRAMITE"] = dr1["TRAMITE"];
                        dr2["RADICACION"] = dr1["RADICACION"];

                        ds2.Tables["HonorariosMedicos"].Rows.Add(dr2);
                    }

                    ReportDocument reporte = new ReportDocument();
                    reporte.FileName = Application.StartupPath + "\\Reportes\\CuentasPacientes\\rptHonorariosMedicos.rpt";
                    reporte.Database.Tables["HonorariosMedicos"].SetDataSource(ds2.Tables["HonorariosMedicos"]);
                    /**/

                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.InitialDirectory = @"C:\";
                    saveFileDialog1.Title = "Save text Files";
                    saveFileDialog1.DefaultExt = "txt";
                    saveFileDialog1.FilterIndex = 2;
                    saveFileDialog1.RestoreDirectory = true;

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        DirectorioReporte = saveFileDialog1.FileName;
                    }

                    if (DirectorioReporte != "")
                    {
                        reporte.ExportToDisk(ExportFormatType.Excel, DirectorioReporte);
                        MessageBox.Show("El reporte se a generado exitosamente", "His3000");
                    }

                    crystalReportViewer1.ReportSource = reporte;
                    crystalReportViewer1.Refresh();

                    this.Dispose();
                }
                else
                {
                    this.Dispose();
                }
            }

            if (_Tipo == "PREFACTURA")
            {
                DataTable ds1 = new DataTable();

                dtsDesgloseFactura ds2 = new dtsDesgloseFactura();
                DataRow dr2;

                ds1 = NegFactura.DatosFacturaTotal(_CodigoAtencion, _NumeroFactura);

                if (ds1 != null && ds1.Rows.Count > 0)
                {
                    DataTable ds3 = new DataTable();
                    ds3 = NegFactura.DatosFacturaTotalPago(_NumeroFactura);
                    string tarjeta = "";
                    tarjeta = ds1.Rows[0]["TARJETA"].ToString();
                    string contado = "";
                    string cheque = "";
                    string credito = "";
                    int contadorpago = 0;
                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        dr2 = ds2.Tables["DatosFacturaTotal"].NewRow();

                        dr2["NUMERO_FACTURA"] = _NumeroFactura;
                        dr2["CLIENTE"] = _Cliente;
                        dr2["RUC"] = _Ruc;
                        dr2["TELEFONO"] = _Telefono;
                        dr2["PACIENTE"] = dr1["PACIENTE"];
                        dr2["HC"] = dr1["HISTORIA"];
                        dr2["ATENCION"] = dr1["ATENCION"];
                        dr2["HABITACION"] = dr1["HABITACION"];
                        dr2["PACIENTE_DIRECCION"] = _Direccion;
                        dr2["FECHA_INGRESO"] = dr1["INGRESO"];
                        dr2["FECHA_ALTA"] = dr1["ALTA"];
                        dr2["RUBRO"] = dr1["RUBRO"];
                        dr2["DESCUENTO"] = _Desceunto;
                        dr2["VALOR"] = dr1["VALOR"];
                        dr2["IVA"] = _Iva;//alex  _IVA cambio por dr1["IVA"]
                        dr2["IVA0"] = _SinIva;
                        dr2["IVA12"] = _ConIva;//alex  _CONIVA cambio por dr1["IVA12"]
                        dr2["MEDICO"] = _Medico;
                        dr2["PROCEDIMIENTO"] = _Procedimiento;

                        dr2["CONTADO"] = dr1["CONTADO"];
                        dr2["TARJETA"] = dr1["TARJETA"];
                        dr2["CHEQUE"] = dr1["CHEQUE"];
                        dr2["CREDITO"] = dr1["CREDITO"];
                        dr2["BANCO"] = dr1["BANCO"];
                        dr2["USUARIO"] = His.Entidades.Clases.Sesion.nomUsuario;
                        ds2.Tables["DatosFacturaTotal"].Rows.Add(dr2);
                    }
                }
                ReportDocument reporte = new ReportDocument();
                reporte.FileName = Application.StartupPath + "\\Reportes\\CuentasPacientes\\rptPreFactura.rpt";

                reporte.SetDataSource(ds2.Tables["DatosFacturaTotal"]);
                //reporte.Database.Tables["DatosFacturaTotal"].SetDataSource(ds2.Tables["DatosFacturaTotal"]);
                crystalReportViewer1.ReportSource = reporte;
                crystalReportViewer1.Refresh();
            }
        }
    }
}
