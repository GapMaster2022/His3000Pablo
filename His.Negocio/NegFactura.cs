using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.General;
using System.Data;

namespace His.Negocio
{
    public class NegFactura
    {
        public static int recuperaMaximoFactura()
        {
            return new DatFactura().recuperaMaximoFactura();
        }

        public static  bool RecuperaAtencion(Int64 ateCodigo)
        {
            return new DatFactura().RecuperaAtencion(ateCodigo);
        }
        
        public static  bool AgrupacionCuentas(Int64 ateCodigo)
        {
            return new DatFactura().AgrupacionCuentas(ateCodigo);
        }

        public static void crearFactura(FACTURA Factura)
        {
            new DatFactura().crearFactura(Factura);
        }

        public static int CrearFacturaSic3000(DtoFacturaSic3000 Factura, Int64 HistoriaClinica, Int64 CodigoAtencion, List<DtoFacturaPago> FacturaPago, List<DtoEstadoCuenta> EstadoCuenta, List<DtoCXC> CxC, int Prefactura, int ateCodigo, string RucPaciente, string nombrePaciente, string Direc_Cliente, string txt_Telef_P, DateTime ATE_FECHA_INGRESO, DateTime FechaAlta, string Medico, Int16 caja, string identificador, string email)
        {
            return new DatFactura().CrearFacturaSic3000(Factura, HistoriaClinica, CodigoAtencion, FacturaPago, EstadoCuenta, CxC, Prefactura, ateCodigo, RucPaciente, nombrePaciente, Direc_Cliente, txt_Telef_P, ATE_FECHA_INGRESO, FechaAlta, Medico, caja, identificador, email);
        }
        public static DataTable VerificaFactura(Int64 Ate_Codigo)
        {
            return new DatFactura().VerificaFactura(Ate_Codigo);
        }

        public static DataTable RecuperaDescuentoXrubro(Int64 ate_codigo, int rubro)
        {
            return new DatFactura().RecuperaDescuentoXrubro(ate_codigo, rubro);
        }

        public static DataTable RecuperaDescuentoXrubroSinIva(Int64 ate_codigo, int rubro)
        {
            return new DatFactura().RecuperaDescuentoXrubroSinIva(ate_codigo, rubro);
        }

        public static DataTable RecuperaDescuentoXrubroConIva(Int64 ate_codigo, int rubro)
        {
            return new DatFactura().RecuperaDescuentoXrubroConIva(ate_codigo, rubro);
        }

        public static bool ProductoConSinIVA(Int32 pro_codigo)
        {
            return new DatFactura().ProductoConSinIVA(pro_codigo);
        }

        public static DataTable VerificaFactura2(Int64 Ate_Codigo)
        {
            return new DatFactura().VerificaFactura2(Ate_Codigo);
        }
        public static DataTable FacturaDuplicada(string FacturaActual)
        {
            return new DatFactura().FacturaDuplicada(FacturaActual);
        }
        
        public static DataTable RecuperaCodigoGrupoSic3000(String Descripcion)
        {
            return new DatFactura().RecuperaCodigoGrupoSic3000(Descripcion);
        }

        public static DataTable RecuperaInformacionCaja(String NumeroCaja)
        {
            return new DatFactura().RecuperaInformacionCaja(NumeroCaja);
        }

        public static string RecuperaInformacionIVA(string IVA)
        {
            return new DatFactura().RecuperaInformacionIVA(IVA);
        }

        public static DataTable RecuperaInformacionUsuario(Int32 CodigoUsuario)
        {
            return new DatFactura().RecuperaInformacionUsuario(CodigoUsuario);
        }

        // RECUPERA DATOS EMPRESA PABLO ROCHA 04/09/2014
        public static DataTable RecuperaEmpresa()
        {
            return new DatFactura().RecuperaEmpresa();
        }

        //RECUPERA DATOS DE LAS FECHAS DE ALTA INGRESO Y ATENCION
        public static DataTable FechaPaciente(Int64 ATE_CODIGO)
        {
            return new DatFactura().FechaPaciente(ATE_CODIGO);
        }

        //RECUPERA LA PRIORIDAD DEL DESCEUNTO A ASIGNARSE
        public static DataTable RecuperaPrioridadDescuento()
        {
            return new DatFactura().RecuperaPrioridadDescuento();
        }

        //RECUPERA RUBROS ORDEN DE DESCUENTOS PABLO ROCHA 10/09/2014
        public static DataTable RecuperaRubrosDescuentos()
        {
            return new DatFactura().RecuperaRubrosDescuento();
        }

        //RECUPERA RUBROS CON IVA PABLO ROCHA 10/09/2014
        public static DataTable RecuperaRubrosIva(string NumFacturaActual)
        {
            return new DatFactura().RecuperaRubrosIva(NumFacturaActual);
        }
        public static DataTable RecuperaRubrosConIva(string NumRubro)
        {
            return new DatFactura().RecuperaRubrosConIva(NumRubro);
        }

        //RECUPERAtipo de indentificacion y email de la factura PABLO ROCHA 10/05/2018
        public static DataTable RecuperatipoIdentificacionEmail(string NumFacturaActual)
        {
            return new DatFactura().RecuperatipoIdentificacionEmail(NumFacturaActual);
        }

        //RECUPERA email paciente y medico de ingreso PABLO ROCHA 10/09/2014
        public static DataTable RecuperaEmailMed(string NumFacturaActual)
        {
            return new DatFactura().RecuperaEmailMed(NumFacturaActual);
        }

        //RECUPERA TABLA 17 DEL SRI

        public static DataTable Tabla17SRI()
        {
            return new DatFactura().Tabla17SRI();
        }

        //RECUPERA PARAMETROS PABLO ROCHA 22/09/2014
        public static DataTable RecuperaParametros()
        {
            return new DatFactura().RecuperaParametros();
        }

        // Impresion desglose de la factura / Giovany Tapia / 04/01/2013
        public static DataTable DetalleCuentaAgrupada(Int64 CodigoAtencion)
        {
            return new DatFactura().DetalleCuentaAgrupada(CodigoAtencion);
        }
        public static DataTable DatosAgrupaFacturaDesglose(int CodigoAtencion)
        {
            return new DatFactura().DatosAgrupaFacturaDesglose(CodigoAtencion);
        }
        public static DataTable DatosFacturaDesglose(Int64 CodigoAtencion)
        {
            return new DatFactura().DatosFacturaDesglose(CodigoAtencion);
        }

        public static DataTable DatosFacturaDesglosexFecha(Int64 CodigoAtencion, DateTime f_inicio, DateTime f_fin)
        {
            return new DatFactura().DatosFacturaDesglosexFecha(CodigoAtencion, f_inicio, f_fin);
        }

        public static string Medico(int CodMedico)
        {
            return new DatFactura().Medico(CodMedico);
        }

        public static DataTable DatosFacturaTotal(Int64 CodigoAtencion, string NumeroFactura)
        {
            return new DatFactura().DatosFacturaTotal(CodigoAtencion, NumeroFactura);
        }

        public static DataTable DatosFacturaTotalxFecha(Int64 CodigoAtencion, string NumeroFactura, DateTime f_Inicio, DateTime F_Fin)
        {
            return new DatFactura().DatosFacturaTotalxFecha(CodigoAtencion, NumeroFactura, f_Inicio, F_Fin);
        }

        public static DataTable RecuperaTemporales(Int64 CodigoAtencion)
        {
            return new DatFactura().RecuperaTemporales(CodigoAtencion);
        }

        public static DataTable BorraTemporales(Int64 CodigoAtencion)
        {
            return new DatFactura().BorraTemporales(CodigoAtencion);
        }

        public static DataTable DatosConsolidado(string CadenaAtenciones)
        {
            return new DatFactura().DatosConsolidado(CadenaAtenciones);
        }

        public static DataTable GeneraHonorariosMedicos(Int32 CodigoMedico, DateTime Fecha1, DateTime Fecha2, Int64 NumeroTramite)
        {
            return new DatFactura().GeneraHonorariosMedicos(CodigoMedico, Fecha1, Fecha2, NumeroTramite);
        }

        public static DataTable DatosFacturaTotalPago(string NumeroFactura)
        {
            return new DatFactura().DatosFacturaTotalPago(NumeroFactura);
        }


        #region Detalle Factura

        public static int recuperaMaximoDetalleFactura()
        {
            return new DatFactura().recuperaMaximoDetalleFactura();
        }

        public static void crearFacturaDetalle(FACTURA_DETALLE facturaDetalle)
        {
            new DatFactura().crearFacturaDetalle(facturaDetalle);
        }

        #endregion

        #region Detalle Factura Formas de Pago

        public static int recuperaMaximoFacturaPago()
        {
            return new DatFactura().recuperaMaximoFacturaPago();
        }

        public static void crearFacturaPago(FACTURA_FORMA_PAGO facturaPago)
        {
            new DatFactura().crearFacturaPago(facturaPago);
        }
        #endregion

        #region SecuencialesFactura

        public static DataTable Datosfactura(String NumeroFactura, String Caja, Int16 TipoDocumento)
        {
            return new DatFactura().Datosfactura(NumeroFactura, Caja, TipoDocumento);
        }

        #endregion

        #region GuardaFactura

        public static void CreaFacturaHis3000(DtoFactura Factura)
        {
            new DatFactura().CreaFacturaHis3000(Factura);
        }

        #endregion

        #region FormasPago

        public static DataTable FormaPagoSic(Boolean TipoBusqueda, int Filtro)
        {
            return new DatFactura().FormaPagoSic(TipoBusqueda, Filtro);
        }

        public static DataTable AnticiposSic_sp(string Ate_Codigo)
        {
            return new DatFactura().AnticiposSic_sp(Ate_Codigo);
        }

        public static DataTable PlazoPagoSic(int codigo)
        {
            return new DatFactura().PlazoPagoSic(codigo);
        }

        public static int PlazoPagoSicVerifica(string VerificaPlazoPago)
        {
            return new DatFactura().PlazoPagoSicVerifica(VerificaPlazoPago);
        }

        public static DataTable RecuperaCodigoClienteSic(string Filtro)
        {
            return new DatFactura().RecuperaCodigoClienteSic(Filtro);
        }

        public static int GeneraCuentasSic(List<DtoFacturaPago> FacturaPago, List<DtoEstadoCuenta> EstadoCuenta, List<DtoCXC> CxC)
        {
            return new DatFactura().GeneraCuentasSic(FacturaPago, EstadoCuenta, CxC);
        }

        //GUARDA TIPO DE INDENTIFICADOR Y EMAIL PABLO ROCHA 10-05-2018

        public static int GuardaIdentificadorEmail(string codIdentificador, string factura, string email)
        {
            return new DatFactura().GuardaIdentificadorEmail(codIdentificador, factura, email);
        }


        public static int RecuperaCodigoClienteSic(List<DtoFacturaPago> FacturaPago, List<DtoEstadoCuenta> EstadoCuenta, List<DtoCXC> CxC)
        {
            return new DatFactura().GeneraCuentasSic(FacturaPago, EstadoCuenta, CxC);
        }

        public static int ActualizaEstadoCuenta(Int64 CodigoAtencion, int Estado, string NumeroFactura, int usuario)
        {
            return new DatFactura().ActualizaEstadoCuenta(CodigoAtencion, Estado, NumeroFactura, usuario);
        }

        public static int ActualizaEstadoCuenta2(Int64 CodigoAtencion, string Ruc, string NumeroFactura, int Esc_Codigo, string usuario)
        {
            return new DatFactura().ActualizaEstadoCuenta2(CodigoAtencion, Ruc, NumeroFactura, Esc_Codigo, usuario);
        }


        public static int GuardaAuditoriaCuenta(Int32 cue_codigo, string observacion, int auditada, decimal cantidad)
        {
            return new DatFactura().GuardaAuditoriaCuenta(cue_codigo, observacion, auditada, cantidad);
        }
        public static int GuardaObservacionAtencion(Int32 cue_codigo, string observacion)
        {
            return new DatFactura().GuardaObservacionAtencion(cue_codigo, observacion);
        }
        //Graba desceuntos en la tabla detalle del sic PABLO ROCHA 08-05-2018
        public static int GuardaDescuentoenDetalles(string codRubro, double Descuento, string Factura)
        {
            return new DatFactura().GuardaDescuentoenDetalles(codRubro, Descuento, Factura);
        }
        public static int ActualizaDescuentoAtencion(Int64 CodigoAtencion)
        {
            return new DatFactura().ActualizaDescuentoAtencion(CodigoAtencion);
        }
        public static int ActualizaValorMSP(int CodigoAtencion)
        {
            return new DatFactura().ActualizaValorMSP(CodigoAtencion);
        }

        //CAMBIOS HR 25102019-------------
        public static int GuardaDescuentoProductos(Int32 PcodRubro, string PCodpro, double Pdescuento, double Pporcentaje, Int64 PCueCodigo, Int32 Patencion)

        {   
            return new DatFactura().GuardaDescuentoProductos(PcodRubro, PCodpro, Pdescuento, Pporcentaje, PCueCodigo, Patencion);
        }


        public static int GuardaDatosAdicionales(string NFactura, string Ruc, string Nombres, string Direccion, string Telefono, Int64 HistoriaClinica, DateTime FechaIngreso, DateTime FechaAlta, string MedicoTratante, Int64 ATE_CODIGO)
        {
            return new DatFactura().GuardaDatosAdicionales(NFactura, Ruc, Nombres, Direccion, Telefono, HistoriaClinica, FechaIngreso, FechaAlta, MedicoTratante, ATE_CODIGO);
        }

        public static DataTable AnticiposCliente(string IdCliente)
        {
            return new DatFactura().AnticiposCliente(IdCliente);
        }
        public static DataTable ProductosFactura(Int64 CodigoAtencion)  // genera los datos para el reporte de la prefactura
        {
            return new DatFactura().ProductosFactura(CodigoAtencion);
        }
        public static DataTable RecuperaAteCodigo(Int32 CodigoAtencion)
        {
            return new DatFactura().RecuperaAteCodigo(CodigoAtencion);
        }

        public static DataTable ObservacioAtencion(Int32 CodigoAtencion)  // genera los datos para el reporte de la prefactura
        {
            return new DatFactura().ObservacioAtencion(CodigoAtencion);
        }
        public short RecuperarHabitacion(Int64 ate_codigo)
        {
            DatFactura factura = new DatFactura();
            short ate_codi = factura.RecuperarHabitacion(ate_codigo);
            return ate_codi;
        }
        public short RecuperarEstado(short hab_codigo)
        {
            DatFactura factura = new DatFactura();
            short estado = factura.RecuperarEstado(hab_codigo);
            return estado;
        }
        public string Recuperarclaspro(string codpro)
        {
            DatFactura factura = new DatFactura();
            string clase = factura.RecuperarClasPro(codpro);
            return clase;
        }
        public static DataTable LXfacturada(Int64 CodigoAtencion)  // genera los datos para el reporte de la prefactura
        {
            return new DatFactura().LXfacturada(CodigoAtencion);
        }
        public static DataTable LXtotales(Int64 CodigoAtencion)  // genera los datos para el reporte de la prefactura
        {
            return new DatFactura().LXtotales(CodigoAtencion);
        }

        public static DataTable RecuperaDescuentos()
        {
            return new DatFactura().RecuperaDescuentos();
        }
        public static bool VerificaAltaPaciente(Int64 ate_Codigo)
        {
            return new DatFactura().VerificaAltaPaciente(ate_Codigo);
        }
        public static DataTable RecuperaDescuentosCodigo(Int32 CodigoDescuento)
        {
            return new DatFactura().RecuperaDescuentosCodigo(CodigoDescuento);
        }


        public static DataTable RecuperasUMAS(Int32 CodigoDescuento)
        {
            return new DatFactura().RecuperasUMAS(CodigoDescuento);
        }

        public static int GeneraPrefactura(DtoPreFactura Prefactura)
        {
            return new DatFactura().GeneraPrefactura(Prefactura);
        }

        public static DataTable DatosPreFactura(String NumeroPrefactura)  // genera los datos para el reporte de la prefactura
        {
            return new DatFactura().DatosPreFactura(NumeroPrefactura);
        }

        public static DataTable DatosFacturaCambio(Int64 CodigoAtencion, Int32 CodigoArea, Int32 CodigoSubArea)  // genera los datos para el reporte de la prefactura
        {
            return new DatFactura().DatosFacturaCambio(CodigoAtencion, CodigoArea, CodigoSubArea);
        }

        public static DataTable DatosFacDescuento(Int32 CodigoAtencion, Int32 CodigoRubro)  // genera los datos para el reporte de la prefactura
        {
            return new DatFactura().DatosDescuento(CodigoAtencion, CodigoRubro);
        }

        public static DataTable DatosHonorarios(Int64 ate_codigo, int rub_codigo)
        {
            return new DatFactura().DatosDescuentoHonorario(ate_codigo, rub_codigo);
        }
        public static DataTable recuperaCodRubro(Int32 codPro)  // genera los datos para el reporte de la prefactura
        {
            return new DatFactura().recuperaCodRubro(codPro);
        }

        public static DataTable FormasDePago(String numfac)  // genera los datos para el reporte de la prefactura
        {
            return new DatFactura().FormasDePago(numfac);
        }
        public static DataTable TiposDescuento()  // genera los datos para el reporte de la prefactura
        {
            return new DatFactura().TiposDescuento();
        }
        public static DataTable fechasINOUT(Int32 codigoatencion)  // genera los datos para el reporte de la prefactura
        {
            return new DatFactura().fechasINOUT(codigoatencion);
        }

        public static DataTable Bancos()  // genera los datos para el reporte de la prefactura
        {
            return new DatFactura().Bancos();
        }

        public static DataTable TipoDescuentoAtencionVer(Int32 CodigoAtencion)  // genera los datos para el reporte de la prefactura
        {
            return new DatFactura().TipoDescuentoAtencionVer(CodigoAtencion);
        }

        public static void TipoDescuentoAtencionActualizar(Int32 CodigoAtencion, Int32 CodigoDescuento)  // actualiza tipo de desuento
        {
            new DatFactura().TipoDescuentoAtencionActualizar(CodigoAtencion, CodigoDescuento);
        }

        public static void GuardaCambiosFactura(Int64 CodigoAtencion, DataTable ListaItems, int coddep)  // genera los datos para el reporte de la prefactura
        {
            new DatFactura().GuardaCambiosFactura(CodigoAtencion, ListaItems, coddep);
        }

        public static void EliminaValoresCuentas(Int64 CodigoAtencion, List<DtoItemEliminadoCuentas> ListaItems)  // genera los datos para el reporte de la prefactura
        {
            new DatFactura().EliminaValoresCuentas(CodigoAtencion, ListaItems);
        }

        #endregion

        public static DataTable GeneraCierreTurno(int CodigoCajero, DateTime Fecha)
        {
            return new DatFactura().GeneraCierreTurno(CodigoCajero, Fecha);
        }
        public static DataTable VerificaCierreTurno(int CodigoCajero, DateTime Fecha)
        {
            return new DatFactura().VerificaCierreTurno(CodigoCajero, Fecha);
        }
        public static void ActualizaEstadoHabitacion(Int64 Ate_Codigo)
        {
            new DatFactura().ActualizaEstadoHabitacion(Ate_Codigo);
        }
        public static void ActualizaEstadoHabitacionAuditoria(Int64 Ate_Codigo)
        {
            new DatFactura().ActualizaEstadoHabitacionAuditoria(Ate_Codigo);
        }
        //public static int ArreglaIVA(string Atencion, Int64 cueCod, string codPro)
        //{
        //    return new DatFactura().ArreglaIVA(Atencion, cueCod, codPro);
        //}
        public static int ValidaSeguroConvenio(string Atencion, string cod_pro)
        {
            return new DatFactura().ValidaSeguroConvenio(Atencion, cod_pro);
        }
        public static string ValidaCopago(string Atencion)
        {
            return new DatFactura().ValidaCopago(Atencion);
        }
        public static string RecuperaPorcentajeIVA(string Atencion)
        {
            return new DatFactura().RecuperaPorcentajeIVA(Atencion);
        }
        public static int EstadoAnticipos(int Indicador, string Factura, decimal valor)
        {
            return new DatFactura().EstadoAnticipos(Indicador, Factura, valor);
        }
        public static int AltaProgramada(Int64 Ate_Codigo)
        {
            return new DatFactura().AltaProgramada(Ate_Codigo);
        }
        public static int GuardaAuxAgrupa(Int64 Ate_Codigo)
        {
            return new DatFactura().GuardaAuxAgrupa(Ate_Codigo);
        }

        public static bool GuardaAuxAgrupacion(Int64 Ate_Codigo, Int64 ate_codigo, int usuario)
        {
            return new DatFactura().GuardaAuxAgrupacion(Ate_Codigo, ate_codigo, usuario);
        }
        public static DataTable BuscaPaciente(string HistoriaClinica, string NombrePaciente, string Identificacion)
        {
            return new DatFactura().BuscaPaciente(HistoriaClinica, NombrePaciente, Identificacion);
        }
        public static int EliminaAuxAgrupa()
        {
            return new DatFactura().EliminaAuxAgrupa();
        }
        public static DataTable RecuperaAuxAgrupa()
        {
            return new DatFactura().RecuperaAuxAgrupa();
        }
        public static DataTable AgrupaCuentas(Int64 Ate_Codigo1, Int64 Ate_Codigo2)
        {
            return new DatFactura().AgrupaCuentas(Ate_Codigo1, Ate_Codigo2);
        }

        public static DataTable RecuperaNumeroFactura(int caja)
        {
            return new DatFactura().RecuperaNumeroFactura(caja);
        }

        public static DataTable IncrementaNumeroFactura(int caja)
        {
            return new DatFactura().IncrementaNumeroFactura(caja);
        }

        public static DataTable RecuperaResolucion2020SRI()
        {
            return new DatFactura().RecuperaResolucion2020SRI();
        }

        public static bool CreaCopagoIva(string ateCodigo, string totalConIva, string iva)
        {
            return new DatFactura().CreaCopagoIva(ateCodigo, totalConIva, iva);
        }

        public static DataTable ValidaAnticipos(Int64 indicador)
        {
            return new DatFactura().ValidaAnticipos(indicador);
        }

        public static string RecuperaNumeroAtencion(Int64 ateCodigo)
        {
            return new DatFactura().RecuperaNumeroAtencion(ateCodigo);
        }

        public static Boolean actualizaEscCodigo(int ateCodigo)
        {
            return new DatFactura().actualizaEscCodigo(ateCodigo);
        }

        public static Boolean RevertirEscCodigo(Int64 ateCodigo)
        {
            return new DatFactura().RevertirEscCodigo(ateCodigo);
        }
        public static void EliminaMEDICOS_ALTA(Int64 codigo)
        {
          new DatFactura().EliminaMEDICOS_ALTA(codigo);
        }
        public static int ArreglaIVA(string Atencion, Int64 cueCod, string codPro)
        {
            return new DatPedidos().ArreglaIVA(Atencion, cueCod, codPro);
        }
        public static List<CUENTAS_PACIENTES> RecuepraCuenta(Int64 ateCodigo)
        {
            return new DatFactura().RecuepraCuenta(ateCodigo);
        }

        public static DataTable DetalleItem(int ate_codigo)
        {
            return new DatFactura().DetalleItem(ate_codigo);
        }
        public static DataTable DetalleArea(int ate_codigo)
        {
            return new DatFactura().DetalleArea(ate_codigo);
        }

        public static int ArreglaIVABase(string Atencion)
        {
            return new DatPedidos().ArreglaIVABase(Atencion);
        }
        public static void VerifcaCuentasFacturadas()
        {
            new DatPedidos().VerifcaCuentasFacturadas();
        }
        public static DataTable FormasPagoFactura(string numFactura)
        {
            return new DatFactura().FormaPagoFactura(numFactura);
        }
        public static string ValidaPEmision(string numcaja)
        {
            return new DatFactura().ValidaPEmision(numcaja);
        }

        public static DataTable RecuperaLeyenda()
        {
            return new DatFactura().RecuperarLeyendas();
        }
        public static List<CUENTAS_PACIENTES> Honorarios(Int64 ate_codigo)
        {
            return new DatFactura().Honorarios(ate_codigo);
        }
        public static DataTable FacturaDetalleAgrupado(string numfac)
        {
            return new DatFactura().FacturaDetalleAgrupado(numfac);
        }
        public static DataTable FormasPagoFacturaHonorarios(string numfac)
        {
            return new DatFactura().FormasPagoFacturaHonorarios(numfac);
        }

        public static bool ExportaCuentaPaciente(Int64 ateCodigo, Int64 pacCodigo)
        {
            return new DatFactura().ExportaCuentaPaciente(ateCodigo, pacCodigo);
        }
        public static List<DtoVistaCopago> cargaCuentaPacienteXAtencion(Int64 ate_codigo)
        {
            return new DatFactura().cargaCuentaPacienteXAtencion(ate_codigo);
        }
    }
}
