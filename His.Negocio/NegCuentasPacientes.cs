using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;
using System.Data;

namespace His.Negocio
{
    public class NegCuentasPacientes
    {
        /// <summary>
        /// Método que crea un nuevo registro de estado de cuenta del paciente
        /// </summary>
        /// <param name="cuenta">Objeto CUENTAS_PACIENTES que se guardara en la base de datos</param>
        public static Int64 CrearCuenta(CUENTAS_PACIENTES cuenta)
        {
            try
            {
                return new DatCuentasPacientes().CrearCuenta(cuenta);
            }
            catch (Exception err) { throw err; }
        }

        /// <summary>
        /// Método utilizado para lo modificación de cuentas existentes de los pacientes
        /// </summary>
        /// <param name="cuenta">Recibe como parámetro la cuentas del Paciente</param>
        public static void ModificarCuenta(CUENTAS_PACIENTES cuenta)
        {
            try
            {
                new DatCuentasPacientes().ModificarCuenta(cuenta);
            }
            catch (Exception err) { throw err; }
        }

        public static DataTable ValoresHabitacionUCI(Int64 ateCodigo)
        {
            return new DatCuentasPacientes().ValoresHabitacionUCI(ateCodigo);
        }

        public static DataTable ValoresHabitacionUCIxFecha(Int64 ateCodigo, DateTime f_Ingreso, DateTime f_Fin)
        {
            return new DatCuentasPacientes().ValoresHabitacionUCIxFecha(ateCodigo, f_Ingreso, f_Fin);
        }

        public static void GuardaTotalUCI(Int64 ateCodigo, int CodigoUsuario, int NumeroDias, int HAB_CODIGO, int CodigoConvenio)
        {
            new DatCuentasPacientes().GuardaTotalUCI(ateCodigo, CodigoUsuario, NumeroDias, HAB_CODIGO, CodigoConvenio);
        }

        public static void GuardaTotalUCIxFecha(Int64 ateCodigo, int NumeroDias, int CodigoConvenio)
        {
            new DatCuentasPacientes().GuardaTotalUCIxFecha(ateCodigo, NumeroDias, CodigoConvenio);
        }

        public static DataTable ValoresHabitacion(Int64 ateCodigo)
        {
            return new DatCuentasPacientes().ValoresHabitacion(ateCodigo);
        }

        public static DataTable ValoresHabitacionxFecha(Int64 ateCodigo, DateTime f_Ingreso, DateTime f_fin)
        {
            return new DatCuentasPacientes().ValoresHabitacionxFecha(ateCodigo, f_Ingreso, f_fin);
        }

        public static DataTable ValidaHabitacion(int p_Convenio, string p_Habitacion)
        {
            return new DatCuentasPacientes().ValidaHabitacion(p_Convenio, p_Habitacion);
        }

        public static int RecuperaCodigoHabitacion(Int64 ateCodigo)
        {
            return new DatCuentasPacientes().RecuperaCodigoHabitacion(ateCodigo);
        }

        public static CUENTAS_PACIENTES RecuperarCuentaId(int codCuenta)
        {
            try
            {
                return new DatCuentasPacientes().RecuperarCuentaId(codCuenta);
            }
            catch (Exception err) { throw err; }
        }


        public static List<CUENTAS_PACIENTES> RecuperarCuenta(Int64 codAtencion)
        {
            try
            {
                return new DatCuentasPacientes().RecuperarCuenta(codAtencion);
            }
            catch (Exception err) { throw err; }
        }


        public static List<CUENTAS_PACIENTES> RecuperarCuentasRubros(int codAtencion, int codRubro)
        {
            try
            {
                return new DatCuentasPacientes().RecuperarCuentasRubros(codAtencion, codRubro);
            }
            catch (Exception err) { throw err; }
        }


        public static CUENTAS_PACIENTES RecuperarCuentasIvaS(int codAtencion, string codIvaS)
        {
            try
            {
                return new DatCuentasPacientes().RecuperarCuentasIvaS(codAtencion, codIvaS);
            }
            catch (Exception err) { throw err; }
        }

        public static List<CUENTAS_PACIENTES> RecuperarCuentaArea(int codAtencion, int codArea)
        {
            try
            {
                return new DatCuentasPacientes().RecuperarCuentaArea(codAtencion, codArea);
            }
            catch (Exception err) { throw err; }
        }


        /// <summary>
        /// Eliminar un Item de la Cuenta del Paciente según la atención
        /// </summary>
        /// <param name="codCuenta">codigo de cuentas pacientes</param>

        public static void EliminarCuenta(Int64 codCuenta)
        {
            try
            {
                new DatCuentasPacientes().EliminarCuenta(codCuenta);
            }
            catch (Exception err) { throw err; }
        }


        public static void EliminarCuentaArea(int codPedido, int codAtencion)
        {
            try
            {
                new DatCuentasPacientes().EliminarCuentaArea(codPedido, codAtencion);
            }
            catch (Exception err) { throw err; }
        }

        public static void ActualizarFechaCuentas(int codAtencion)
        {
            try
            {
                new DatCuentasPacientes().ActualizarFechaCuentas(codAtencion);
            }
            catch (Exception err) { throw err; }
        }


        public static List<DtoCuentasPacientes> recuperarListaCuentaAtencion1(string id, string historia, string nombre, string habitacion, int cantidad, int estado)
        {
            try
            {
                return new DatCuentasPacientes().recuperarListaCuentaAtencion1(id, historia, nombre, habitacion, cantidad, estado);
            }
            catch (Exception err) { throw err; }
        }

        public static DataTable GeneraValoresCuentas(string Fecha1, string Fecha2, int Area, int Rubro, int todosRubros)
        {
            return new DatCuentasPacientes().GeneraValoresCuentas(Fecha1, Fecha2, Area, Rubro, todosRubros);
        }

        public static DataTable FormaPagoSic(string Factura)
        {
            return new DatCuentasPacientes().FormaPagoSic(Factura);
        }


        #region ESTADOS CUENTA

        public static List<ESTADOS_CUENTA> RecuperarEstadosCuenta()
        {
            try
            {
                return new DatCuentasPacientes().RecuperarEstadosCuenta();
            }
            catch (Exception err) { throw err; }
        }

        public static List<ASEGURADORAS_EMPRESAS> RecuperarCategoriasConvenios()
        {
            try
            {
                return new DatCuentasPacientes().RecuperarCategoriasConvenios();
            }
            catch (Exception err) { throw err; }
        }


        public static DataTable Datos_reporte(string fechaIni, string fechaFin, int estado)
        {
            return new DatCuentasPacientes().Datos_reporte(fechaIni, fechaFin, estado);
        }
        public static DataTable AtencionesCuentas(string fechaIni, string fechaFin, int estadoFactura, string Nombre, string HC, string Atencion, int TipoSeguro)
        {
            return new DatCuentasPacientes().AtencionesCuentas(fechaIni, fechaFin, estadoFactura, Nombre, HC, Atencion, TipoSeguro);
        }

        public static DataTable AtencionesCuentas_Paquetes(DateTime fechaIni, DateTime fechaFin, int estadoFactura, string Nombre, string HC, string Atencion, int TipoSeguro)
        {
            return new DatCuentasPacientes().AtencionesCuentas_Paquetes(fechaIni, fechaFin, estadoFactura, Nombre, HC, Atencion, TipoSeguro);
        }

        public static DataTable AtencionesCuentasPaquete(Int64 NumeroTramite)
        {
            return new DatCuentasPacientes().AtencionesCuentasPaquete(NumeroTramite);
        }

        public static DataTable RecuperarPaquete(Int64 NumeroTramite)
        {
            return new DatCuentasPacientes().RecuperarPaquete(NumeroTramite);
        }

        public static DataTable DetalleCuentasModificadas(string fechaIni, string fechaFin, string HC, string Atencion, string area)//detalle de las cuentas de pacientes modificadas David Mantilla 28/11/2012
        {
            return new DatCuentasPacientes().DetalleCuentasModificadas(fechaIni, fechaFin, HC, Atencion, area);
        }

        public static DataTable AtencionesCuentasTodos(string fechaIni, string fechaFin, int estadoFactura, string Nombre, string HC, string Atencion, int TipoSeguro)
        {
            return new DatCuentasPacientes().AtencionesCuentasTodos(fechaIni, fechaFin, estadoFactura, Nombre, HC, Atencion, TipoSeguro);
        }

        public static DataTable AtencionesCuentasTodosPaquete(DateTime fechaIni, DateTime fechaFin, int estadoFactura, string Nombre, string HC, string Atencion, int TipoSeguro)
        {
            return new DatCuentasPacientes().AtencionesCuentasTodosPaquete(fechaIni, fechaFin, estadoFactura, Nombre, HC, Atencion, TipoSeguro);
        }

        public static int PermisosActualizacionCuentas(int Usuario, int CuentaDesde, int CuentaHacia)
        {
            // VERIFICAR SI UN USUARIO TIENES LOS PERMISOS NECESARIOS PARA CAMBIAR EL ESTADO DE UNA CUENTA / GIOVANNY TAPIA / 07/08/2012
            return new DatCuentasPacientes().PermisosActualizacionCuentas(Usuario, CuentaDesde, CuentaHacia);
        }

        public static DataTable AtencionesConsolidar(string hc)
        {
            return new DatCuentasPacientes().AtencionesConsolidar(hc);
        }

        public static DataTable AtencionesValorTotal(string atenciones)
        {
            return new DatCuentasPacientes().AtencionesValorTotal(atenciones);
        }
        public static DataTable CuentasAtenciones(string atencion)
        {
            return new DatCuentasPacientes().CuentasAtenciones(atencion);
        }
        public static DataTable DatosExportar(string atencion)
        {
            return new DatCuentasPacientes().DatosExportar(atencion);
        }
        public static DataTable DatosExportarAseguradora(string atencion, int CodigoAseguradora)
        {
            return new DatCuentasPacientes().DatosExportarAseguradora(atencion, CodigoAseguradora);
        }
        public static void actualizarEstadoFactura(Int32 codAtencion, Int16 estado)
        {
            new DatCuentasPacientes().actualizarEstadofactura(codAtencion, estado);
        }
        public static void actualizarProseso(string codAtencion, string estado)
        {
            new DatCuentasPacientes().actualizarProseso(codAtencion, estado);
        }
        public static void actualizarCuentasPaciente(Int32 atencionA, Int32 atencionC)
        {
            new DatCuentasPacientes().actualizarCuentasPaciente(atencionA, atencionC);
        }
        public static void actualizarCuenta(Int32 atencionA, Int32 atencionC)
        {
            new DatCuentasPacientes().actualizarCuenta(Convert.ToString(atencionA), Convert.ToString(atencionC));
        }

        public static void ActualizaFechasCuenta(int CodigoAtencion, int CodigoPedidoArea, int CodigoRubro, DateTime Fecha, int CodigoUsuario)// Permite actualizar las fechas de una cuenta en forma masiva // Giovanny Tapia // 15/01/2013
        {
            new DatCuentasPacientes().ActualizaFechasCuenta(CodigoAtencion, CodigoPedidoArea, CodigoRubro, Fecha, CodigoUsuario);
        }

        public static void actualizarCuentaNumFac(Int64 atencion, string numFac)
        {
            new DatCuentasPacientes().actualizarCuentaNumFac(atencion, numFac);
        }


        #endregion

        #region CUENTAS PACIENTES HISTORIAL
        /// <summary>
        /// Método utilizado para crear cuentas de pacientes cuando han sido modificlados algun cambio en la cuenta
        /// </summary>
        /// <param name="cuenta">cuenta que se registro cambio</param>
        public static void CrearCuentaHistorial(CUENTAS_PACIENTES_HISTORIAL cuentaHistorial)
        {
            try
            {
                new DatCuentasPacientes().CrearCuentaHistorial(cuentaHistorial);
            }
            catch (Exception err) { throw err; }
        }

        public static void DetalleCuentasModificadas(CUENTAS_PACIENTES cuenta)
        {
            try
            {
                new DatCuentasPacientes().Detallemodificadas(cuenta);
            }
            catch (Exception err) { throw err; }
        }

        #endregion


        #region CUENTAS_PACIENTES_TOTALES

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cuenta"></param>
        public static void CrearCuentaPT(CUENTAS_PACIENTES_TOTALES cuentaPaciente)
        {
            try
            {
                new DatCuentasPacientes().CrearCuentaPT(cuentaPaciente);
            }
            catch (Exception err) { throw err; }
        }


        public static CUENTAS_PACIENTES_TOTALES RecuperarCuentasTotal(int codAtencion)
        {
            try
            {
                return new DatCuentasPacientes().RecuperarCuentasTotal(codAtencion);
            }
            catch (Exception err) { throw err; }
        }

        public static List<CUENTAS_PACIENTES> RecuperaCuenta(Int64 ateCodigo)
        {
            try
            {
                return new DatCuentasPacientes().RecuperaCuenta(ateCodigo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int RecuperarCodigoPaquete()
        {
            try
            {
                return new DatCuentasPacientes().RecuperarCodigoPaquete();
            }
            catch (Exception err) { throw err; }
        }

        public static Int64 RecuperarCodigoPaqueteRadicacion()
        {
            try
            {
                return new DatCuentasPacientes().RecuperarCodigoPaqueteRadicacion();
            }
            catch (Exception err) { throw err; }
        }


        public static int CrearPaqueteCuentas(Int64 PaqueteId, string FechaControlPaquete, Int64 NumeroRadicacionPaquete, Int64 NumeroTramitePaquete, string Observacion, List<Int32> ListaAtenciones, Int32 Usuario)
        {
            try
            {
                return new DatCuentasPacientes().CrearPaqueteCuentas(PaqueteId, FechaControlPaquete, NumeroRadicacionPaquete, NumeroTramitePaquete, Observacion, ListaAtenciones, Usuario);
            }
            catch (Exception err) { throw err; }
        }

        public static int ActualizarPaquete(Int64 NumeroRadicacionPaquete, Int64 NumeroTramitePaquete, Int32 Id_Usuario)
        {
            try
            {
                return new DatCuentasPacientes().ActualizarPaquete(NumeroRadicacionPaquete, NumeroTramitePaquete, Id_Usuario);
            }
            catch (Exception err) { throw err; }
        }

        public static int EliminarCuentasPaquete(Int64 PaqueteId, List<Int32> ListaAtenciones, Int32 Id_Usuario)
        {
            try
            {
                return new DatCuentasPacientes().EliminarCuentasPaquete(PaqueteId, ListaAtenciones, Id_Usuario);
            }
            catch (Exception err) { throw err; }
        }

        public static int EliminarPaquete(Int64 PaqueteId, Int32 Id_Usuario)
        {
            try
            {
                return new DatCuentasPacientes().EliminarPaquete(PaqueteId, Id_Usuario);
            }
            catch (Exception err) { throw err; }
        }


        public static int VerificaAtencionesPaquete(Int32 NumeroAtencion)
        {
            try
            {
                return new DatCuentasPacientes().VerificaAtencionesPaquete(NumeroAtencion);
            }
            catch (Exception err) { throw err; }
        }

        public static int IngresaHonorario(Int64 MEDCODIGO, Int64 ATECODIGO, Int64 IDUSUARIO, DateTime FECHA,
                                            String CODIGO, String VALE)
        {
            try
            {
                return new DatCuentasPacientes().IngresaHonorario(MEDCODIGO, ATECODIGO, IDUSUARIO, FECHA,
                                                                  CODIGO, VALE);
            }
            catch (Exception err) { throw err; }
        }

        //public static DataTable GeneraValoresCuentas(string Fecha1,string Fecha2,int Area , int Rubro)
        //{
        //    return new DatCuentasPacientes().GeneraValoresCuentas(Fecha1, Fecha2, Area, Rubro);
        //}

        public static void GeneraValoresautomaticos(Int64 CodigoAtencion, Int32 CodigoUsuario, Int32 NumeroDias, string Habitacion, Int32 Aseguradora, Int32 Empresa)
        {
            new DatCuentasPacientes().GeneraValoresautomaticos(CodigoAtencion, CodigoUsuario, NumeroDias, Habitacion, Aseguradora, Empresa);
        }

        public static void GeneraValoresautomaticosxFechas(Int64 CodigoAtencion, Int32 CodigoUsuario, Int32 NumeroDias, string Habitacion, Int32 Aseguradora, Int32 Empresa)
        {
            new DatCuentasPacientes().GeneraValoresautomaticosxFechas(CodigoAtencion, CodigoUsuario, NumeroDias, Habitacion, Aseguradora, Empresa);
        }

        public static void AdministracionMedicamentos(Int64 CodigoAtencion, Int32 CodigoUsuario, Int32 Aseguradora, Int32 Empresa)
        {
            new DatCuentasPacientes().AdministracionMedicamentos(CodigoAtencion, CodigoUsuario, Aseguradora, Empresa);
        }

        public static void AdministracionMedicamentosxFechas(Int64 CodigoAtencion, Int32 CodigoUsuario, Int32 Aseguradora, Int32 Empresa)
        {
            new DatCuentasPacientes().AdministracionMedicamentos(CodigoAtencion, CodigoUsuario, Aseguradora, Empresa);
        }

        public static DataTable GeneraValoresAutomaticosCuentas(Int64 CodigoAtencion, Int32 CodigoUsuario, Int32 NumeroDias, String Habitacion, Int32 Aseguradora, Int32 Empresa)
        {
            return new DatCuentasPacientes().GeneraValoresAutomaticosCuentas(CodigoAtencion, CodigoUsuario, NumeroDias, Habitacion, Aseguradora, Empresa);
        }

        public static int VerificaValorAutomatico(Int64 Atenciones)
        {
            return new DatCuentasPacientes().VerificaValorAutomatico(Atenciones);
        }

        public static double RecuperaCosto(string Costo, int ruc_cod)
        {
            return new DatCuentasPacientes().RecuperaCosto(Costo, ruc_cod);
        }

        public static DataTable CargaTipoMedico()
        {
            return new DatCuentasPacientes().CargaTipoMedico();
        }

        public static DataTable GeneraArchivoPlano(String Atenciones)
        {
            return new DatCuentasPacientes().GeneraArchivoPlano(Atenciones);
        }

        public static DataTable GeneraArchivoPlanoISSPOL(String Atenciones, int conta, string periodo)
        {
            return new DatCuentasPacientes().GeneraArchivoPlanoISSPOL(Atenciones, conta, periodo);
        }

        public static DataTable GeneraArchivoPlanoISSPOL(String Atenciones)
        {
            return new DatCuentasPacientes().GeneraArchivoPlanoISSPOL(Atenciones);
        }

        public static DataTable RecuperaGinecologia(string HCG)
        {
            return new DatCuentasPacientes().RecuperaGinecologia(HCG);
        }

        public static int GuardaGinecologia(string HistoriaClinica, int Menarquia, string Cliclos, DateTime FUM, int G, int P,
                                    int A, int C, int HV, int GO, int DIU, string OM, string CV, string APP, string APF, string GS,
                                    string HOPITALIZACION, string OPERACIONES, string RECOMENDADO)
        {
            return new DatCuentasPacientes().GuardaGinecologia(HistoriaClinica, Menarquia, Cliclos, FUM, G, P,
                                       A, C, HV, GO, DIU, OM, CV, APP, APF, GS, HOPITALIZACION, OPERACIONES, RECOMENDADO);
        }

        public static List<PRODUCTOCOPAGO> DivideFactura1(int Ate_Codigo, int Rub_Codigo)
        {
            return new DatCuentasPacientes().DivideFactura1(Ate_Codigo, Rub_Codigo);
        }

        public static int CopagoFactura1(int Ate_Codigo, double Cue_Valor, string Cue_Detalle, double Cue_Valor1, int Cue_Rubro)
        {
            return new DatCuentasPacientes().CopagoFactura1(Ate_Codigo, Cue_Valor, Cue_Detalle, Cue_Valor1, Cue_Rubro);
        }

        public static int DivideFactura2(int Ate_Codigo)
        {
            return new DatCuentasPacientes().DivideFactura2(Ate_Codigo);
        }

        public static int DivideFactura4(int Ate_Codigo)
        {
            return new DatCuentasPacientes().DivideFactura4(Ate_Codigo);
        }

        public static int DivideFactura3(int Ate_Codigo, int rubro)
        {
            return new DatCuentasPacientes().DivideFactura3(Ate_Codigo, rubro);
        }

        public static int DivideFacturaNO(int Ate_Codigo)
        {
            return new DatCuentasPacientes().DivideFacturaNO(Ate_Codigo);
        }
        public static int DivideFacturaProd1(int Ate_Codigo, Int64 codpro)
        {
            return new DatCuentasPacientes().DivideFacturaProd1(Ate_Codigo, codpro);
        }
        public static int ValoresAutomaticos(int ATE_CODIGO, int RUBRO, int TOTALDIAS, int USUARIO, string DESCRIPCION, string PRO_CODIGO, double CUE_VALOR_UNITARIO)
        {
            return new DatCuentasPacientes().ValoresAutomaticos(ATE_CODIGO, RUBRO, TOTALDIAS, USUARIO, DESCRIPCION, PRO_CODIGO, CUE_VALOR_UNITARIO);
        }

        public static List<CUENTAS_PACIENTES> RecuperarCuentaArea2(int codAtencion, int codArea)
        {
            try
            {
                return new DatCuentasPacientes().RecuperarCuentaArea2(codAtencion, codArea);
            }
            catch (Exception err) { throw err; }
        }

        public static bool ActualizaProductos(List<PRODUCTOCOPAGO> lista, int ateCodigo)
        {
            try
            {
                return new DatCuentasPacientes().ActualizaProductos(lista, ateCodigo);
            }
            catch (Exception err) { throw err; }
        }

        #endregion

        public static bool LlamaParametro()
        {
            try
            {
                return new DatCuentasPacientes().LlamaParametro();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool LlamarParametroInventariable()
        {
            try
            {
                return new DatCuentasPacientes().LlamaParametroInventariable();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string VerificaBien(Int64 codpro)
        {
            try
            {
                return new DatCuentasPacientes().VerificaBien(codpro);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void DivideFactura(Int64 ate_codigo, decimal cantidad, Int64 cue_codigo)
        {
            new DatCuentasPacientes().DividirFactura(ate_codigo, cantidad, cue_codigo);
        }

        public static void CreaHistorialNuevoAuditoria(NuevoAuditoria nuevo)
        {
            new DatCuentasPacientes().CreaHistorialNuevoAuditoria(nuevo);
        }
        public static Int64 generaCuentaAuditoria(Int64 ate_codigo, List<DtoCopago> copado, List<DtopCuenta> cuenta, Int64 id_usuario)
        {
            return new DatCuentasPacientes().generaCuentaAuditoria(ate_codigo, copado, cuenta, id_usuario);
        }
    }
}
