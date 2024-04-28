using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegQuirofano
    {
        DatQuirofano Quirofano = new DatQuirofano();

        public static DataTable MostrarProducto(double codsub)
        {
            return new DatQuirofano().MostrarProductos(codsub);
        }

        public static DataTable MostrarGrupo()
        {
            return new DatQuirofano().MostrarGrupos();
        }
        public static DataTable MostrarGruposAliansa()
        {
            return new DatQuirofano().MostrarGruposAliansa();
        }
        public DataTable MostrarProcedimientos(int bodega)
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.MostrarProcedimientos(bodega);
            return Tabla;
        }
        public static DataTable MostrarCie10()
        {
            return new DatQuirofano().MostrarCie10();
        }
        public DataTable Patologos()
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.Patologo();
            return Tabla;
        }
        public DataTable CargarTablaProductos(string busqueda, bool codigo, bool descripcion, int bodega)
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.MostrarProductosAgregados(busqueda, codigo, descripcion, bodega);
            return Tabla;
        }
        public DataTable CargarAnestesias(string busqueda, bool codigo, bool descripcion)
        {
            return new DatQuirofano().MostrarAnestesias(busqueda, codigo, descripcion);
        }
        public static DataTable Productos(int bodega)
        {
            return new DatQuirofano().Productos(bodega);
        }
        public static void AgregarProducto(string codpro, string grupo, int bodega)
        {
            new DatQuirofano().AgregarProducto(codpro, grupo, bodega);
        }
        public void EliminarProducto(string qp_codigo)
        {
            Quirofano.EliminarProducto(Convert.ToInt32(qp_codigo));
        }
        public void ActualizarProducto(string qp_codigo, string grupo)
        {
            Quirofano.ActualizarProducto(Convert.ToInt32(qp_codigo), grupo);
        }
        public void AgregarProcedimiento(string orden, string codpro, string cie_codigo, string cantidad)
        {
            Quirofano.AgregarProcedimientos(Convert.ToInt32(orden), codpro, Convert.ToInt64(cie_codigo), Convert.ToInt32(cantidad));
        }
        public DataTable TodosProcedimiento(int bodega)
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.TodosProcedimientos(bodega);
            return Tabla;
        }
        public DataTable TodosProductos(string cie_codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.TodosProductos(Convert.ToInt64(cie_codigo));
            return Tabla;
        }
        public static dsProcedimiento listarProcedimiento(int bodega)
        {
            return new DatQuirofano().lProcedimientos(bodega);
        }
        public static bool ActualizaProce_Producto(Int64 pci_codigo, string codpro, int orden, int cantidad)
        {
            return new DatQuirofano().ActualizarProce_Producto(pci_codigo, codpro, orden, cantidad);
        }
        public static bool RecuperaAtencionesQuirofano(Int64 ate_codigo)
        {
            return new DatQuirofano().RecuperaAtencionesQuirofano(ate_codigo);
        }
        public static int CambioEstadoReposicion(Int64 ate_codigo)
        {
            return new DatQuirofano().CambioEstadoReposicion(ate_codigo);
        }
        public static Int64 IdRegistroQuirofano(Int64 ate_codigo)
        {
            return new DatQuirofano().IdRegistroQuirofano(ate_codigo);
        }
        
        public static INTERVENCIONES_REGISTRO_QUIROFANO RegistroQuirofano(Int64 id)
        {
            return new DatQuirofano().RegistroQuirofano(id);
        }
        public static PROCEDIMIENTOS_CIRUGIA Procedimiento(Int64 intervencion)
        {
            return new DatQuirofano().Procedimiento(intervencion);
        }
        public static bool EliminarProducto_Proce(Int64 pci_codigo, string codpro)
        {
            return new DatQuirofano().EliminarProducto_Proce(pci_codigo, codpro);
        }
        public static IEnumerable<object> ListarProductos(int bodega)
        {
            return new DatQuirofano().ListarProductos(bodega);
        }
        public void EliminarProcedimiento(string cie_codigo)
        {
            Quirofano.EiminarProcedimiento(cie_codigo);
        }
        public static Int64 CrearProcedimiento(string procedimiento, int bodega)
        {
            return new DatQuirofano().CrearProcedimiento(procedimiento, bodega);
        }
        public static bool EliminaProcedimiento(Int64 pci_codigo)
        {
            return new DatQuirofano().EliminarProcedimiento(pci_codigo);
        }
        public static bool ActualizaProcedimiento(Int64 pci_codigo, string procedimiento)
        {
            return new DatQuirofano().ActualizarProcedimiento(pci_codigo, procedimiento);
        }
        public static int UltimoOrdenProcedimiento(int pci_codigo)
        {
            return new DatQuirofano().UltimoOrdenProcedimiento(pci_codigo);
        }
        public void ActualizarProcedimiento(string cie_codigo, string codpro, string qpp_orden, string cantidad)
        {
            Quirofano.ActualizarProcedimiento(cie_codigo, codpro, qpp_orden, Convert.ToInt32(cantidad));
        }
        public void EliminarProduProce(string cie_codigo, string codpro)
        {
            Quirofano.EliminarProduProce(cie_codigo, codpro);
        }
        public static DataTable QuirofanoPacientes(int bodega)
        {
            return new DatQuirofano().QuirofanoPacientes(bodega);
        }
        public void PedidoPaciente(string cie, string paciente, string atencion, string fecha)
        {
            Quirofano.PedidoPaciente(Convert.ToInt64(cie), Convert.ToInt32(paciente), Convert.ToInt32(atencion), fecha);
        }
        public DataTable SoloProcedimientos(int bodega)
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.SoloProcedimientos(bodega);
            return Tabla;
        }
        public void AgregarProcedimientoPaciente(string orden, string cie_codigo, string codpro, string cantidad,
            string paciente, string atencion, string usada, string usuario, int cerrado)
        {
            if (orden == "")
            {
                Quirofano.AgregarProcedimientoPaciente(1, cie_codigo, codpro, Convert.ToDouble(cantidad), Convert.ToInt32(paciente), Convert.ToInt32(atencion), Convert.ToInt32(usada), usuario, cerrado);
            }
            else
            {
                Quirofano.AgregarProcedimientoPaciente(Convert.ToInt32(orden), cie_codigo, codpro, Convert.ToDouble(cantidad), Convert.ToInt32(paciente), Convert.ToInt32(atencion), Convert.ToInt32(usada), usuario, cerrado);
            }
        }
        public void ModificarProcedimientoPaciente(string orden, string cie_codigo, string codpro,
            string paciente, string atencion, string usado, string usuario)
        {
            Quirofano.ModificarProcedimientoPaciente(Convert.ToInt32(orden), cie_codigo, codpro,
                Convert.ToInt32(paciente), Convert.ToInt32(atencion), Convert.ToInt32(usado), usuario);
        }
        public DataTable PacienteProcedimiento(string cie_codigo, string atencion, int bodega)
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.PacienteProcedimiento(Convert.ToInt64(cie_codigo), Convert.ToInt32(atencion), bodega);
            return Tabla;
        }
        public static DataTable PacienteProcedimientoNew(int general, int bodega)
        {
            return new DatQuirofano().PacienteProcedimientoNew(general, bodega);
        }
        public static DataTable PacienteProcedimientoRecuperado(Int64 id, Int64 ateCodigo)
        {
            return new DatQuirofano().PacienteProcedimientoRecuperado(id, ateCodigo);
        }
        public static string Cantidad(string atencion, int codigo)
        {
            return new DatQuirofano().Cantidad(Convert.ToInt32(atencion), codigo);
        }
        public DataTable PorProcedimiento(string cie_codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.FiltrarPorProcedimiento(cie_codigo);
            return Tabla;
        }
        public string ProductoRepetido(string codpro, string cie_codigo)
        {
            string producto = Quirofano.ProductoRepetido(codpro, Convert.ToInt32(cie_codigo));
            return producto;
        }
        public static string SoloProductoRepetido(string codpro, int bodega)
        {
            return new DatQuirofano().SoloProductoRepetido(codpro, bodega);
        }
        public void ProductoBodega(string codpro, string existe, double bodega)
        {
            Quirofano.ProductoBodega(codpro, Convert.ToDouble(existe), bodega);
        }
        public DataTable ProcedimientosPaciente(string ate_codigo, string pac_codigo, int bodega)
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.ProcedimientosPaciente(Convert.ToInt32(ate_codigo), Convert.ToInt32(pac_codigo), bodega);
            return Tabla;
        }
        public string ExisteProcedimiento(string ate_codigo, string pac_codigo, string cie_codigo)
        {
            string valor;
            valor = Quirofano.Proces(Convert.ToInt32(ate_codigo), Convert.ToInt32(pac_codigo), cie_codigo);
            return valor;
        }
        public void PedidoAdicional(string atencion, string paciente, string cie_codigo, string cant_adicional, string codpro)
        {
            Quirofano.PedidoAdicional(Convert.ToInt32(atencion), Convert.ToInt32(paciente), cie_codigo, Convert.ToDouble(cant_adicional), codpro);
        }
        public DataTable QuirofanoHabitacion()
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.HabitacionQuirofano();
            return Tabla;
        }
        public static DataTable GastroHabitacion()
        {
            return new DatQuirofano().HabitacionGastro();
        }
        public DataTable MostrarMedicos()
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.MostrarMedicos();
            return Tabla;
        }
        public DataTable MostrarGastro()
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.MostrarGastro();
            return Tabla;
        }
        public DataTable MostrarPatologo()
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.MostrarPatologo();
            return Tabla;
        }
        public DataTable MostrarAnestesiologo()
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.MostrarAnestesiologo();
            return Tabla;
        }
        public DataTable MostrarUsuario()
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.MostrarUsuario();
            return Tabla;
        }

        public List<CIRUGIA_ESPECIALIDAD> MostrarEspCirugia()
        {
            return new DatQuirofano().MostrarEspCirugia();
        }
        public static INTERVENCIONES_REGISTRO_QUIROFANO RecuperaMaxIntervencion()
        {
            return new DatQuirofano().RecuperaMaxIntervencion();
        }
        public DataTable MostrarAnestesia()
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.MostrarAnestesia();
            return Tabla;
        }
        public void AgregarRegistro(string hab_quirofano, string cirujano, string ayudante, string ayudantia, string tipo_anestesia,
            string recuperacion, string anestesiologo, string horainicio, string horafin, string duracion, string circulante,
            string instrumentista, string patologo, string tipo_atencion, string ate_codigo, string pac_codigo, string cie_codigo)
        {
            if (ayudante == "")
                ayudante = "0";
            if (ayudantia == "")
                ayudantia = "0";
            if (circulante == "")
                circulante = "0";
            if (instrumentista == "")
                instrumentista = "0";
            if (patologo == "")
                patologo = "0";
            Quirofano.AgregarRegistro(Convert.ToInt32(hab_quirofano), Convert.ToInt32(cirujano), Convert.ToInt32(ayudante),
                Convert.ToInt32(ayudantia), Convert.ToInt32(tipo_anestesia), Convert.ToInt32(recuperacion),
                Convert.ToInt32(anestesiologo), horainicio, horafin, duracion, Convert.ToInt32(circulante), Convert.ToInt32(instrumentista),
                Convert.ToInt32(patologo), tipo_atencion, Convert.ToInt32(ate_codigo), Convert.ToInt32(pac_codigo), Convert.ToInt64(cie_codigo));
        }
        public DataTable RegistroPaciente(string ate_codigo, string pac_codigo, string cie_codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.CargarRegistroPaciente(Convert.ToInt32(ate_codigo), Convert.ToInt32(pac_codigo), Convert.ToInt64(cie_codigo));
            return Tabla;
        }
        public void CerrarProcedimiento(string ate_codigo, string pac_codigo, string cie_codigo)
        {
            Quirofano.CerrarProcedimiento(Convert.ToInt32(ate_codigo), Convert.ToInt32(pac_codigo), cie_codigo);
        }
        public string ProcedimientoCerrado(string ate_codigo, string pac_codigo, string cie_codigo)
        {
            string estado = Quirofano.ProcedimientoCerrado(/*Convert.ToInt32(ate_codigo), Convert.ToInt32(pac_codigo),*/ cie_codigo);
            return estado;
        }


        public void ActualizarPedidoAdicional(string ate_codigo, string cie_codigo, string codpro, string cantadicional)
        {
            Quirofano.ActualizarPedidoAdicional(Convert.ToInt32(ate_codigo), cie_codigo, codpro, Convert.ToDouble(cantadicional));
        }
        public void AgregarPedidoPaciente(string ped_fecha, string id_usuario, string ate_codigo, string hab_codigo)
        {
            Quirofano.AgregarPedidoPaciente(ped_fecha, Convert.ToInt32(id_usuario), Convert.ToInt32(ate_codigo), Convert.ToInt32(hab_codigo));
        }
        public void PedidoDetalle(string codpro, string prodesc, string cantidad, string valor, string total, string ped_codigo, string iva)
        {
            Quirofano.PedidoDetalle(codpro, prodesc, Convert.ToInt32(cantidad), Convert.ToDouble(valor), Convert.ToDouble(total), Convert.ToInt32(ped_codigo), Convert.ToDouble(iva));
        }
        public DataTable VerTicketsPaciente(string ate_codigo, int bodega)
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.VerTicketPaciente(Convert.ToInt32(ate_codigo), bodega);
            return Tabla;
        }
        public DataTable ProductosPaciente(string ate_codigo, string ped_codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.ProductosPaciente(Convert.ToInt32(ate_codigo), Convert.ToInt32(ped_codigo));
            return Tabla;
        }
        public string RecuperarPedidoNum(string ate_codigo)
        {
            string numpedido;
            numpedido = Quirofano.RecuperarPedidoNum(Convert.ToInt32(ate_codigo));
            return numpedido;
        }
        public static REGISTRO_QUIROFANO RecuperarRegistroQuirofano(Int64 ate_codigo)
        {
            return new DatQuirofano().RecuperarRegistroQuirofano(ate_codigo);
            
        }
        public DataTable RecuperarInfoPaciente(string ate_codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.RecuperarPacientePedidoInfo(Convert.ToInt32(ate_codigo));
            return Tabla;
        }
        public DataTable RecuperarProductosUsados(DateTime desde, DateTime hasta, int usuario)
        {
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.CargarProductosUsados(desde, hasta, usuario);
            return Tabla;
        }
        public static DataTable ProcedimientosCirugia(string busqueda, bool codigo, bool descripcion, int bodega)
        {
            return new DatQuirofano().CargarProcedimientosCirugia(busqueda, codigo, descripcion, bodega);
        }
        public static Int64 GuardaRegistroQuirofano(REGISTRO_QUIROFANO obj, bool edicion = false)
        {
            return new DatQuirofano().GuardaRegistroQuirofano(obj, edicion);
        }
        public static bool GuardaRegistroIntervencionQuirofano(INTERVENCIONES_REGISTRO_QUIROFANO obj, bool edicion = false)
        {
            return new DatQuirofano().GuardaRegistroIntervencionQuirofano(obj, edicion);
        }
        public static INTERVENCIONES_REGISTRO_QUIROFANO RecuperaRegistroIntervencionQuirofano(Int64 id_quirofano)
        {
            return new DatQuirofano().RecuperaRegistroIntervencionQuirofano(id_quirofano);
        }
       
        public static void ActualizarKardexSic(string numdoc, int bodega)
        {
            new DatQuirofano().ActualizarKardexSic(numdoc, bodega);
        }
        public static bool ActualizarKardexSicMushuñan(string numdoc)
        {
            return new DatQuirofano().ActualizarKardexSicMushuñan(numdoc);
        }
        public static bool ActualizarKardexSicBrigada(string numdoc)
        {
            return new DatQuirofano().ActualizarKardexSicBrigada(numdoc);
        }
        public static DataTable AnestesiaSolicitada(int pci_codigo)
        {
            return new DatQuirofano().AnestesiaSolicitada(pci_codigo);
        }
        public static DataTable ListaPedidosQuirofano(int ate_codigo)
        {
            return new DatQuirofano().ListaPedidosQuirofano(ate_codigo);
        }
        public static void QuirofanoActualizaProductos(string codpro, int pci_codigo, double cantidad, Int64 ate_codigo)
        {
            new DatQuirofano().QuirofanoActualizarProductos(codpro, pci_codigo, cantidad, ate_codigo);
        }
        public static void QuirofanoEliminaRegistro(string codpro, int pci_codigo, Int64 ate_codigo)
        {
            new DatQuirofano().QuirofanoEliminaRegistro(codpro, pci_codigo, ate_codigo);
        }
        public static void QuirofanoEliminaRegistro2(int pci_codigo, Int64 ate_codigo)
        {
            new DatQuirofano().QuirofanoEliminaRegistro2(pci_codigo, ate_codigo);
        }
        public static void DatosReposicion(Int64 ate_codigo, int pci_codigo, int cantidad, DateTime fecha, int ped_codigo, string codpro, int usuario)
        {
            new DatQuirofano().DatosReposicion(ate_codigo, pci_codigo, cantidad, fecha, ped_codigo, codpro, usuario);
        }
        public static DataTable RecuperoReposicion(DateTime desde, DateTime hasta, int usuario)
        {
            return new DatQuirofano().RecuperoReposicion(desde, hasta, usuario);
        }
        public static void FechaReposicion(DateTime fecha, int pci_codigo, Int64 ate_codigo, Int64 numdoc)
        {
            new DatQuirofano().FechaReposicion(fecha, pci_codigo, ate_codigo, numdoc);
        }
        public static int NumeroControl()
        {
            return new DatQuirofano().NumeroControl();
        }
        public static void LiberarNumControl()
        {
            new DatQuirofano().NumeroLiberar();
        }

        public static void OcuparNumControl()
        {
            new DatQuirofano().NumeroOcupado();
        }
        public static bool CreaPedidoReposicion(Int64 numdoc, DateTime fecha, string hora, double origen, Double destino,
            string observacion, char estado, double usuario, List<RepoQuirofano> lista)
        {
            return new DatQuirofano().CreaPedidoReposicion(numdoc, fecha, hora, origen, destino, observacion, estado, usuario, lista);
        }

        //public static void DetalleReposicion(string codpro, string despro, double cant, int linea, Int64 numdoc)
        //{
        //    new DatQuirofano().DetalleReposicion(codpro, despro, cant, linea, numdoc);
        //}
        public static DataTable DetalleExportar(DateTime desde, DateTime hasta)
        {
            return new DatQuirofano().DetalleExportar(desde, hasta);
        }
        public static DataTable UsuariosReposicion()
        {
            return new DatQuirofano().UsuariosReposicion();
        }
        public static void EliminarRegistro(Int64 ate_codigo, int pci_codigo)
        {
            new DatQuirofano().EliminarRegistro(ate_codigo, pci_codigo);
        }
        public static bool nombreProcedimiento(string procedimiento, int bodega)
        {
            return new DatQuirofano().NombreProcedimiento(procedimiento, bodega);
        }
        public static DataTable ProductosSic(string filtro, int bodega)
        {
            return new DatQuirofano().ProductosSic(filtro, bodega);
        }
        public static DataTable ProcedimientosVarios(Int64 ate_codigo, int pac_codigo, string procedimiento)
        {
            return new DatQuirofano().ProcedimientosVarios(ate_codigo, pac_codigo, procedimiento);
        }
        public static DataTable NProcedimientos(string procedimiento, int bodega)
        {
            return new DatQuirofano().Creado(procedimiento, bodega);
        }
        public static int ultimoOrden(int pci_codigo, Int64 ate_codigo)
        {
            return new DatQuirofano().UltimoOrden(pci_codigo, ate_codigo);
        }
        public static bool validaDecimales(string codpro)
        {
            return new DatQuirofano().validaDecimales(codpro);
        }
        public static QUIROFANO_PROCE_PRODU habitacionProcedimiento(Int64 ate_codigo, Int64 pci_codigo)
        {
            return new DatQuirofano().recuperarHabitacionQuirofano(ate_codigo, pci_codigo);
        }
        public static List<REPOSICION_QUIROFANO> ReposicionQuirofano(Int64 pci_codigo, Int64 ate_codigo)
        {
            return new DatQuirofano().ReposicionQuirofano(pci_codigo, ate_codigo);
        }

        public static bool GuarddaReposicion(List<REPOSICION_QUIROFANO> lista)
        {
            return new DatQuirofano().GuarddaReposicion(lista);
        }
        public static DataTable ExploradorProcedimientos(DateTime desde, DateTime hasta, Int64 bodega, bool filtro)
        {
            return new DatQuirofano().ExploradorProcedimientos(desde, hasta,bodega,filtro);
        }
        public static DataTable TodoslosProcedimiento(DateTime desde, DateTime hasta)
        {
            return new DatQuirofano().TodoslosProcedimiento(desde, hasta);
        }
        public static DataTable procedimietosXmes(DateTime desde, DateTime hasta)
        {
            return new DatQuirofano().procedimietosXmes(desde, hasta);
        }
        public static DataTable RepocisionPedido(Int64 ID_PEDIDO)
        {
            return new DatQuirofano().RepocisionPedido(ID_PEDIDO);
        }
        public static DataTable ProductosProcedimeinto(Int64 ID_PEDIDO)
        {
            return new DatQuirofano().ProductosProcedimeinto(ID_PEDIDO);
        }
        public static void CargaPedido(string orden, string codpro, string cie_codigo, string cantidad)
        {
            new DatQuirofano().CargaPedido(Convert.ToInt32(orden), codpro, Convert.ToInt64(cie_codigo), Convert.ToInt32(cantidad));
        }
        public static bool CambioEstadoReposicion(Int64 ped_codigo, Int64 numdoc)
        {
            return new DatQuirofano().CambioEstadoReposicion(ped_codigo, numdoc);
        }
        public static dsProcedimiento reposicionGastro(DateTime desde, DateTime hasta, Int32 bodega)
        {
            return new DatQuirofano().reposicionGastro(desde, hasta,bodega);
        }
        public static dsProcedimiento reposicionQuirofano(DateTime desde, DateTime hasta, Int32 bodega)
        {
            return new DatQuirofano().reposicionQuirofano(desde, hasta,bodega);
        }
        public static dsProcedimiento reposicionGastroServicios(DateTime desde, DateTime hasta, Int32 bodega)
        {
            return new DatQuirofano().reposicionGastroServicios(desde, hasta, bodega);
        }
        public static dsProcedimiento reposicionQuirofanoServicios(DateTime desde, DateTime hasta, Int32 bodega)
        {
            return new DatQuirofano().reposicionQuirofanoServicios(desde, hasta, bodega);
        }
        public static DataTable RecuperoReposicionServicio(DateTime desde, DateTime hasta, int usuario)
        {
            return new DatQuirofano().RecuperoReposicionServicio(desde, hasta, usuario);
        }
        public static List<DtoHabitaciones> listadoHAbitaciones()
        {
            return new DatQuirofano().listadoHAbitaciones();
        }
        public static List<DtoHabitaciones> listadoHAbitacionesConsentimiento()
        {
            return new DatQuirofano().listadoHAbitacionesConsentimiento();
        }
        public static REPOSICION_QUIROFANO exieteRegistro(Int64 ATE_CODIGO, Int64 PCI_CODIGO, string CODPRO)
        {
            return new DatQuirofano().exieteRegistro(ATE_CODIGO, PCI_CODIGO, CODPRO);
        }
        public static string registroQuirofano(Int64 ATE_CODIGO)
        {
            return new DatQuirofano().registroQuirofano(ATE_CODIGO);
        }
        public static bool recuperaEstadoUltimoProcedimiento(Int64 ATE_CODIGO)
        {
            return new DatQuirofano().recuperaEstadoUltimoProcedimiento(ATE_CODIGO);
        }
    }
}
