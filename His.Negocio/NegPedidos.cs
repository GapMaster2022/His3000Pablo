using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.Pedidos;
using System.Data;

namespace His.Negocio
{
    public class NegPedidos
    {
        #region PEDIDOS

        public static int PermiososUsuario(int IdUsuario, string ParametroBusqueda)
        {
            return new DatPedidos().PermiososUsuario(IdUsuario, ParametroBusqueda);
        }

        public static int ultimoCodigoPedidos()
        {
            return new DatPedidos().ultimoCodigoPedidos();
        }
        public static void crearPedido(PEDIDOS nPedido)
        {
            new DatPedidos().crearPedido(nPedido);
        }
        public static PEDIDOS recuperarPedidoID(int codPedido)
        {
            return new DatPedidos().recuperarPedidoID(codPedido);
        }
        public static List<PEDIDOS> recuperarListaPedidosAtencion(int codAtencion, int estado, string busqPedido, string desde, string hasta)
        {
            return new DatPedidos().recuperarListaPedidosAtencion(codAtencion, estado, busqPedido, desde, hasta);
        }
        //public static List<DtoPedidos> recuperarListaPedidosAtencion(string id, string historia, string nombre, int cantidad, int estado)
        //{
        //    try
        //    {
        //        return new DatPedidos().recuperarListaPedidosAtencion(id, historia, nombre, cantidad, estado);
        //    }
        //    catch (Exception err) { throw err; }
        //}

        public static List<DtoPedidos> recuperarListaCuentaAtencion(string id, string historia, string nombre, int cantidad, int estado)
        {
            try
            {
                return new DatPedidos().recuperarListaCuentaAtencion(id, historia, nombre, cantidad, estado);
            }
            catch (Exception err) { throw err; }
        }
        public static List<DtoPedidos> recuperarListaCuentaAtencionTodos(string id, string historia, string nombre, int cantidad, int estado)
        {
            try
            {
                return new DatPedidos().recuperarListaCuentaAtencionTodos(id, historia, nombre, cantidad, estado);
            }
            catch (Exception err) { throw err; }
        }

        public static List<DtoPedidos> recuperarListaPacientesFacturacion(string id, string historia, string nombre, int cantidad, int estado, string NumFac)
        {
            try
            {
                return new DatPedidos().recuperarListaPacientesFacturacion(id, historia, nombre, cantidad, estado, NumFac);
            }
            catch (Exception err) { throw err; }
        }
        public static List<DtoPedidos> recuperarListaPacientesFacturacionMushuñan(string id, string historia, string nombre, int cantidad, int estado, string NumFac)
        {
            try
            {
                return new DatPedidos().recuperarListaPacientesFacturacionMushuñan(id, historia, nombre, cantidad, estado, NumFac);
            }
            catch (Exception err) { throw err; }
        }
        public static List<DtoPedidos> recuperarListaPacientesFacturacionBrigada(string id, string historia, string nombre, int cantidad, int estado, string NumFac)
        {
            try
            {
                return new DatPedidos().recuperarListaPacientesFacturacionBrigada(id, historia, nombre, cantidad, estado, NumFac);
            }
            catch (Exception err) { throw err; }
        }

        public static void actualizarEstadoPedido(PEDIDOS pedido, Int16 estado)
        {
            new DatPedidos().actualizarEstadoPedido(pedido, estado);
        }

        public static void actualizarEstadoPedidoDetalle(Int32 codPedido, Boolean estado)
        {
            new DatPedidos().actualizarEstadoPedidoDetalle(codPedido, estado);
        }

        public static void actualizarEstadoPedido(Int32 codPedido, Int16 estado)
        {
            new DatPedidos().actualizarEstadoPedido(codPedido, estado);
        }

        public static List<PEDIDOS_DETALLE> RecuperaDetallePedidos(int NumeroPedido)
        {
            try
            {
                return new DatPedidos().RecuperaDetallePedidos(NumeroPedido);
            }
            catch (Exception err) { throw err; }
        }



        #endregion

        #region PEDIDOS_DETALLES
        public static Int64 ultimoCodigoPedidosDetalles()
        {
            return new DatPedidos().ultimoCodigoPedidosDetalles();
        }
        public static bool crearDetallePedido(List<PEDIDOS_DETALLE> ndetalle, PEDIDOS pedido, string NumValue)
        {
            DatPedidos.quirofano = true;
            return new DatPedidos().crearDetallePedido(ndetalle, pedido, NumValue);
        }

        public static void crearDetallePedidoQuirofano(PEDIDOS_DETALLE ndetalle, PEDIDOS pedido, Int16 Rubro, Int32 PedidoDivision, string NumValue)
        {
            new DatPedidos().crearDetallePedidoQuirofano(ndetalle, pedido, Rubro, PedidoDivision, NumValue);

        }
        public static bool nuevoPedidoProcedimiento(PEDIDOS pedido, List<PEDIDOS_DETALLE> pDetalle, int bodega,
            int pci_codigo, string NumVale)
        {
            return new DatPedidos().nuevoPedidoProcedimientos(pedido, pDetalle, bodega, pci_codigo, NumVale);
        }
        public static void GuardaPedDetalleHonorarios(Int64 pdd_codigo, Int64 ped_codigo, string codpro, string despro,
            decimal pdd_cantidad, decimal pdd_valor, decimal pdd_iva, decimal pdd_total, PEDIDOS pedido, Int16 Rubro,
            Int32 PedidoDivision, string NumVale, Int32 bodega)
        {
            new DatPedidos().GuardarPedidoHonorario(pdd_codigo, ped_codigo, codpro, despro,
                pdd_cantidad, pdd_valor, pdd_iva, pdd_total, pedido, Rubro, PedidoDivision, NumVale, bodega);
        }
        public static void GuardaDetalleHonorarios(DtoDetalleHonorariosMedicos DetalleHonorarios)
        {
            new DatPedidos().GuardaDetalleHonorarios(DetalleHonorarios);
        }

        public static void CreaPedido(DtoPedidoOtros Pedido, Int64 Numvale)
        {
            new DatPedidos().CreaPedido(Pedido, Numvale);
        }

        public static Int32 CrearDevolucionPedido(DtoPedidoDevolucion PedidoDevolucion, Int64 CodigoAtencion, Int64 cue_Codigo)
        {
            return new DatPedidos().CrearDevolucionPedido(PedidoDevolucion, CodigoAtencion, cue_Codigo);
        }
        public static DataTable recuperaCodRubro(Int32 codpro)
        {
            return new DatPedidos().recuperaCodRubro(codpro);
        }

        public static Int64 CrearDevolucionQuirofano(DtoPedidoDevolucion PedidoDevolucion, Int64 CodigoAtencion, Int64 cue_Codigo, int bodega, string modulo)
        {
            return new DatPedidos().CreaDevolucionQuirofano(PedidoDevolucion, CodigoAtencion, bodega, modulo, cue_Codigo);
        }
        public static List<PEDIDOS> recuperarListaPedidos(int estado, string busqPedido, string desde, string hasta)
        {
            return new DatPedidos().recuperarListaPedidos(estado, busqPedido, desde, hasta);
        }

        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo PEDIDO
        /// </summary>
        /// <param name="codigoAtencion">codigo de la atencion</param>
        /// <param name="tipo">tipo de pedido</param>
        /// <returns>lista de objetos de tipo PEDIDO</returns>
        public static List<PEDIDOS> recuperarListaPedidos(Int64 codigoAtencion, Int16 tipo)
        {
            return new DatPedidos().recuperarListaPedidos(codigoAtencion, tipo);
        }
        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo PEDIDO
        /// </summary>
        /// <param name="codigoAtencion">codigo de la atencion</param>
        /// <param name="tipo">tipo de pedido</param>
        /// <param name="tipo">tipo del Area</param>
        /// <returns>lista de objetos de tipo PEDIDO</returns>
        public static List<PEDIDOS> recuperarListaPedidosPorArea(DateTime fechaIni, DateTime fechaFin, Int16 codigoArea)
        {
            return new DatPedidos().recuperarListaPedidosPorArea(fechaIni, fechaFin, codigoArea);
        }
        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo PEDIDO
        /// </summary>
        /// <param name="fechaIni">Fecha de inicio para el filtro</param>
        /// <param name="fechaFin">Fecha Final para el filtro</param>
        /// <param name="codigoArea">tipo del Area</param>
        /// <returns>lista de objetos de tipo PEDIDO</returns>
        public static List<PEDIDOS> recuperarListaPedidosPorArea(Int64 codigoAtencion, Int16 tipo, Int16 codigoArea)
        {
            return new DatPedidos().recuperarListaPedidosPorArea(codigoAtencion, tipo, codigoArea);
        }
        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo PEDIDO
        /// </summary>
        /// <param name="codigoArea">tipo del Area</param>
        /// <returns>lista de objetos de tipo PEDIDO</returns>
        public static List<PEDIDOS> recuperarListaPedidosPorArea(Int16 codigoArea)
        {
            return new DatPedidos().recuperarListaPedidosPorArea(codigoArea);
        }
        public static List<PEDIDOS_DETALLE> RecuperarDetallePedido(int codPedido)
        {
            return new DatPedidos().RecuperarDetallePedido(codPedido);
        }
        public static void actulizarEstadoDetallePedido(int codDetalle, bool estado)
        {
            new DatPedidos().actulizarEstadoDetallePedido(codDetalle, estado);
        }
        #endregion

        #region AREAS_PEDIDOS
        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo PEDIDO_AREAS
        /// </summary>
        /// <returns>lista de objetos de tipo PEDIDOS_AREAS</returns>
        public static List<PEDIDOS_AREAS> recuperarListaAreas()
        {
            try
            {
                return new DatPedidos().recuperarListaAreas();
            }
            catch (Exception err) { throw err; }
        }

        public static List<PEDIDOS_AREAS> recuperarListaAreasTodas()
        {
            try
            {
                return new DatPedidos().recuperarListaAreasTodas();
            }
            catch (Exception err) { throw err; }
        }
        public static List<PEDIDOS_AREAS> RecuperarListaServicios()
        {
            try
            {
                return new DatPedidos().RecuperarListaServicios();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo PEDIDO_ESTACIONES
        /// </summary>
        /// <returns>lista de objetos de tipo PEDIDO_ESTACIONES</returns>
        //public static List<PEDIDOS_ESTACIONES> recuperarListaEstaciones()
        //{
        //    try
        //    {
        //        return new DatPedidos().recuperarListaEstaciones();
        //    }
        //    catch (Exception err) { throw err; }
        //}

        /// <summary>
        /// Metodo que devuelve una instancia de PEDIDOS_AREAS recibiendo como parametro el codigo 
        /// </summary>
        /// <param name="codigoArea">codigo del Area</param>
        /// <returns>devuelve un objeto PEDIDOS_AREAS</returns>
        public static PEDIDOS_AREAS recuperarAreaPorID(Int16 codigoArea)
        {
            try
            {
                return new DatPedidos().recuperarAreaPorID(codigoArea);
            }
            catch (Exception err) { throw err; }
        }
        /// <summary>
        /// Metodo que devuelve las Areas a las q el usuario puede acceder
        /// </summary>
        /// <param name="codigoUsuario">codigo del usuario</param>
        /// <returns>Lista de objetos PEDIDOS_AREAS</returns>
        public static List<PEDIDOS_AREAS> recuperarListaAreasPorUsuario(int codigoUsuario)
        {
            try
            {
                return new DatPedidos().recuperarListaAreasPorUsuario(codigoUsuario);
            }
            catch (Exception err) { throw err; }
        }
        #endregion

        #region PEDIDOS_VISTA
        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo DtoPedidos
        /// </summary>
        /// <param name="codigoArea">codigo de Area</param>
        /// <returns>lista de objetos de tipo DtoPedidos</returns>
        public static List<DtoPedidos> recuperarListaPedidosVistaPendientesPorArea(Int16 codigoArea)
        {
            try
            {
                return new DatPedidos().recuperarListaPedidosVistaPendientesPorArea(codigoArea);
            }
            catch (Exception err) { throw err; }
        }
        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo DtoPedidos
        /// </summary>
        /// <param name="codigoArea">codigo de Area</param>
        /// <param name="codigoArea">codigo de la Atencion</param>
        /// <returns>lista de objetos de tipo DtoPedidos</returns>
        public static List<DtoPedidos> recuperarListaPedidosVistaPendientesPorArea(Int16 codigoArea, int codigoAtencion)
        {
            try
            {
                return new DatPedidos().recuperarListaPedidosVistaPendientesPorArea(codigoArea, codigoAtencion);
            }
            catch (Exception err) { throw err; }
        }

        public static List<DtoPedidos> ListaPedidos(int codigoArea, int codigoAtencion)
        {
            try
            {
                return new DatPedidos().ListaPedidos(codigoArea, codigoAtencion);
            }
            catch (Exception err) { throw err; }
        }
        public static List<DtoPedidos> ListaPedidosTodosRubros(int codigoAtencion)
        {
            try
            {
                return new DatPedidos().ListaPedidosTodosRubros(codigoAtencion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo DtoPedidos
        /// </summary>
        /// <param name="codigoArea">Codigo del Area de Pedidos</param>
        /// <param name="codigoEstacion"> Codigo de la Estacion</param>
        /// <param name="estadoPedido">Estado de los pedidos</param>
        /// <param name="fechaIni">Filtro de fecha inicial </param>
        /// <param name="fechaFin">Filtro de fecha final</param>
        /// <param name="desde">numero incial de registro desde donde empezaran a tomarse los valores</param>
        /// <param name="cantidad">cantidad de registros que se recuperaran</param>
        /// <returns>lista de objetos de tipo DtoPedidos</returns>
        public static List<DtoPedidos> recuperarListaPedidosVistaPorArea(Int16 codigoArea, byte codigoEstacion, Int32 estadoPedido, DateTime fechaIni, DateTime fechaFin, int desde, Int16 cantidad)
        {
            try
            {
                return new DatPedidos().recuperarListaPedidosVistaPorArea(codigoArea, codigoEstacion, estadoPedido, fechaIni, fechaFin, desde, cantidad);
            }
            catch (Exception err) { throw err; }
        }


        #endregion

        #region ESTACIONES_PEDIDOS
        /// <summary>
        /// Metodo que devuelve una lista de objetos de tipo PEDIDOS_ESTACIONES
        /// </summary>
        /// <returns>lista de objetos de tipo PEDIDOS_ESTACIONES</returns>
        public static List<PEDIDOS_ESTACIONES> recuperarListaEstaciones()
        {
            try
            {
                return new DatPedidos().recuperarListaEstaciones();
            }
            catch (Exception err) { throw err; }
        }
        /// <summary>
        /// Metodo que devuelve una instancia de PEDIDOS_ESTACIONES recibiendo como parametro el codigo 
        /// </summary>
        /// <param name="codigoArea">codigo de la estacion</param>
        /// <returns>devuelve un objeto PEDIDOS_ESTACIONES</returns>
        public static PEDIDOS_ESTACIONES recuperarEstacionPorID(Int16 codigoArea)
        {
            try
            {
                return new DatPedidos().recuperarEstacionPorID(codigoArea);
            }
            catch (Exception err) { throw err; }
        }
        #endregion

        #region PRODUCTO ESTRUCTURAS
        //public static List<DtoLaboratorioEstructura> recuperarEstructura(short codigoEstructura)
        //{
        //    try
        //    {
        //        return new DatPedidos().recuperarEstructura(codigoEstructura);
        //    }
        //    catch (Exception err) { throw err; }
        //}  

        #endregion

        #region ImpresionPedido

        public static DataTable DatosPedido(int NumeroPedido)
        {
            try
            {
                return new DatPedidos().DatosPedido(NumeroPedido);
            }
            catch (Exception err) { throw err; }
        }

        public static DataTable DatosPedidosMushuñan(int NumeroPedido)
        {
            return new DatPedidos().DatosPedidoMushuñan(NumeroPedido);
        }

        public static DataTable DatosPedido2(int NumeroPedido)
        {
            try
            {
                return new DatPedidos().DatosPedido2(NumeroPedido);
            }
            catch (Exception err) { throw err; }
        }


        public static DataTable DatosImpresionPedido(int CodigoArea)
        {
            try
            {
                return new DatPedidos().DatosImpresionPedido(CodigoArea);
            }
            catch (Exception err) { throw err; }
        }


        public static DataTable ListaPedidosRealizados(int CodigoEstacion, int EstadoPedido, bool FiltroFechas, DateTime Fecha1, DateTime Fecha2)
        {
            try
            {
                return new DatPedidos().ListaPedidosRealizados(CodigoEstacion, EstadoPedido, FiltroFechas, Fecha1, Fecha2);
            }
            catch (Exception err) { throw err; }
        }

        public static DataTable ListaPedidos(DateTime Fecha1, DateTime Fecha2)
        {
            try
            {
                return new DatPedidos().ListaPedidos(Fecha1, Fecha2);
            }
            catch (Exception err) { throw err; }
        }

        public static DataTable CierreReporte(DateTime Fecha1, string Usuario)
        {
            try
            {
                return new DatPedidos().CierreReporte(Fecha1, Usuario);
            }
            catch (Exception err) { throw err; }
        }

        public static void GuardaCierreTurno(List<DtoCierreTurno> CierreTurno)
        {
            try
            {
                new DatPedidos().GuardaCierreTurno(CierreTurno);
            }
            catch (Exception err) { throw err; }
        }

        public static DataTable RetornarDevCodigo(Int64 ped_codigo)
        {
            return new DatPedidos().RetornaDevCodigo(ped_codigo);
        }

        public static int ValidarAseguradoraPaciente(Int64 ate_codigo)
        {
            return new DatPedidos().ValidarAseguradoraPaciente(ate_codigo);
        }

        public static bool ParametroDieta()
        {
            return new DatPedidos().ParametroDieta();
        }
        public static int ValidarRubro(string codpro)
        {
            return new DatPedidos().ValidarRubroDieta(codpro);
        }
        //public static bool QuirofanoDuplicado(Int64 ate_codigo, int pci_codigo, string codpro, Int64 ped_codigo)
        //{
        //    return new DatPedidos().ValidaRepetido(ate_codigo, pci_codigo, codpro, ped);
        //}

        public static DataTable Area_SubArea(int ped_codigo)
        {
            return new DatPedidos().Area_Subarea(ped_codigo);
        }

        public static DataTable Devoluciones(DateTime desde, DateTime hasta, string hc)
        {
            return new DatPedidos().Devoluciones(desde, hasta, hc);
        }

        public static DataTable Pedidos(DateTime fechainicio, DateTime fechafin)
        {
            return new DatPedidos().MonitorPedidos(fechainicio, fechafin);
        }

        public static void crearDetallePedido2(PEDIDOS_DETALLE ndetalle, PEDIDOS pedido, Int16 Rubro, Int32 PedidoDivision, string NumValue)
        {
            DatPedidos.quirofano = true;
            new DatPedidos().crearDetallePedido2(ndetalle, pedido, Rubro, PedidoDivision, NumValue);
        }
        #endregion

        #region Desarrollo de Despacho 2021-11-24
        public static DataTable VerDespacho(DateTime desde, DateTime hasta, bool pedido, bool despacho, string hc, string piso, string hab, int ped_codigo)
        {
            return new DatPedidos().VerDespachos(desde, hasta, pedido, despacho, hc, piso, hab, ped_codigo);
        }
        public static bool InsertarDespachos(List<DtoDespachos> dtoDespachos, int despacho, DateTime fecha)
        {
            return new DatPedidos().InsertarDespachos(dtoDespachos, despacho, fecha);
        }
        public static bool ValidaDespachado(Int64 ped_codigo)
        {
            return new DatPedidos().ValidaDespacho(ped_codigo);
        }
        public static DataTable VerPisos()
        {
            return new DatPedidos().verPisos();
        }
        public static void DeleteDespacho(Int64 ped_codigo)
        {
            new DatPedidos().DeleteDespacho(ped_codigo);
        }
        public static DataTable VerHabitaciones(int piso)
        {
            return new DatPedidos().verHabitaciones(piso);
        }

        public static bool ActualizarBodega(string numdoc, int bodega)
        {
            return new DatPedidos().ActualizarKardexSic(numdoc, bodega);
        }

        #endregion
        public static string recureraBodega(Int64 bodega)
        {
            return new DatPedidos().recureraBodega(bodega);
        }
        public static decimal validaCantidad(Int64 ATE_CODIGO, string PRO_CODIGO, Int64 Codigo_Pedido)
        {
            return new DatPedidos().validaCantidad(ATE_CODIGO, PRO_CODIGO, Codigo_Pedido);
        }
    }
}
