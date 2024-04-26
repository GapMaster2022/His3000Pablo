using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;
using His.Parametros;

namespace His.Negocio
{
    public class NegAccesoOpciones
    {
        public static int RecuperaMaximoAccesoOpciones(Int16 modulo)
        {
            return new DatAccesoOpciones().RecuperaMaximoAccesoOpciones(modulo);
        }
        public static List<DtoAccesoOpciones> RecuperaAccesoOpciones()
        {
            return new DatAccesoOpciones().RecuperaAccesoOpciones();
        }
        public static ACCESO_OPCIONES RecuperaAccesosOpciones(Int64 acceso)
        {
            return new DatAccesoOpciones().RecuperaAccesosOpciones(acceso);
        }
        public static void CrearAccesoOpciones(ACCESO_OPCIONES accOp)
        {
            new DatAccesoOpciones().CrearAccesoOpciones(accOp);
        }
        public static void GrabarAccesoOpciones(ACCESO_OPCIONES accOpModificada, ACCESO_OPCIONES accOpOriginal)
        {
            new DatAccesoOpciones().GrabarAccesoOpciones(accOpModificada, accOpOriginal);
        }
        public static void EliminarAccesoOpciones(ACCESO_OPCIONES accOp)
        {
            new DatAccesoOpciones().EliminarAccesoOpciones(accOp);
        }
        public static List<ACCESO_OPCIONES> ListaAccesoOpciones()
        {
            return new DatAccesoOpciones().ListaAccesoOpciones();
        }
        /// <summary>
        /// Metodo que asigana todas las opciones de acceso que tiene un perfil
        /// </summary>
        /// <param name="codigoPerfil">Codigo del Perfil</param>
        public static void asignarAccesosPorPerfil(Int16 codigoPerfil)
        {
            try
            {
                //Recupero la lista de modulos 
                List<MODULO> listaModulos = new DatModulo().RecuperaModulos();
                foreach (var modulo in listaModulos)
                {
                    //Recupero la lista de accesos que tendra el perfil para un modulo en especial
                    List<ACCESO_OPCIONES> listaAccesoOpciones = new DatAccesoOpciones().ListaAccesoOpcionesPorPerfil(codigoPerfil, modulo.ID_MODULO);
                    //Asigna las opciones de acceso para el Modulo de Mantenimiento
                    if (modulo.ID_MODULO == 1)
                    {
                        deshabilitarControlesMantenimiento();
                        foreach (var acceso in listaAccesoOpciones)
                        {
                            switch (acceso.ID_ACCESO)
                            {
                                //ARCHIVO
                                case 11000:
                                    AccesosModuloMantenimiento.Archivo = true;
                                    break;
                                //EMPRESAS
                                case 11100:
                                    AccesosModuloMantenimiento.Empresas = true;
                                    break;
                                //ZONAS Y LOCALES
                                case 11200:
                                    AccesosModuloMantenimiento.ZonasLocales = true;
                                    break;
                                //DEPARTAMENTOS
                                case 11300:
                                    AccesosModuloMantenimiento.Departamento = true;
                                    break;
                                //CAJAS
                                case 11400:
                                    AccesosModuloMantenimiento.Cajas = true;
                                    break;
                                //PRECIOS Y PORCENTAJES CONVENIOS
                                case 12000:
                                    AccesosModuloMantenimiento.PreciosPorcentaje = true;
                                    break;
                                //TIPOS DE COSTO
                                case 12100:
                                    AccesosModuloMantenimiento.TipoCosto = true;
                                    break;
                                //CATALOGO DE COSTOS
                                case 12200:
                                    AccesosModuloMantenimiento.CatalogoCosto = true;
                                    break;
                                //CONVENIOS
                                case 12300:
                                    AccesosModuloMantenimiento.Convenios = true;
                                    break;
                                //PRECIOS POR CONVENIOS
                                case 12400:
                                    AccesosModuloMantenimiento.PrecioConvenio = true;
                                    break;
                                //MANTENIMIENTO DE TABLAS
                                case 13000:
                                    AccesosModuloMantenimiento.MantenimientoTablas = true;
                                    break;
                                //DIVISION POLITICA
                                case 13100:
                                    AccesosModuloMantenimiento.DivisionPolitica = true;
                                    break;
                                //NACIONALIDAD
                                case 13200:
                                    AccesosModuloMantenimiento.Nacionalidad = true;
                                    break;
                                //BANCOS
                                case 13300:
                                    AccesosModuloMantenimiento.Bancos = true;
                                    break;
                                //TIPO DE NEGOCIO
                                case 13400:
                                    AccesosModuloMantenimiento.TipoNegocio = true;
                                    break;
                                //TIPO DE MEDICO
                                case 13500:
                                    AccesosModuloMantenimiento.TipoMedico = true;
                                    break;
                                //NUMEROS DE CONTROL
                                case 13600:
                                    AccesosModuloMantenimiento.NumeroControl = true;
                                    break;
                                //TIPO DE ATENCIONES
                                case 13700:
                                    AccesosModuloMantenimiento.TipoAtenciones = true;
                                    break;
                                //TIPO DE INGRESO
                                case 13800:
                                    AccesosModuloMantenimiento.TipoIngreso = true;
                                    break;
                                //TIPOS DE COUDADANOS
                                case 13900:
                                    AccesosModuloMantenimiento.TipoCiudadano = true;
                                    break;
                                //PISO MAQUINA
                                case 14000:
                                    AccesosModuloMantenimiento.PisoMaquina = true;
                                    break;
                                //PISO
                                case 14100:
                                    AccesosModuloMantenimiento.Piso = true;
                                    break;
                                //GRUPO DE PRODUCTOS
                                case 14200:
                                    AccesosModuloMantenimiento.GrupoProductos = true;
                                    break;
                                //HABITACIONES
                                case 14300:
                                    AccesosModuloMantenimiento.Habitaciones = true;
                                    break;
                                //CAMBIO TIPO ATENCION
                                case 14400:
                                    AccesosModuloMantenimiento.CambioAtencion = true;
                                    break;
                                //HONORARIOS CONSULTA EXTERNA
                                case 14500:
                                    AccesosModuloMantenimiento.HonorarioCEX = true;
                                    break;
                                //SEGURIDADES
                                case 15000:
                                    AccesosModuloMantenimiento.Seguridades = true;
                                    break;
                                //USUARIOS
                                case 15100:
                                    AccesosModuloMantenimiento.Usuarios = true;
                                    break;
                                //PERFILES
                                case 15200:
                                    AccesosModuloMantenimiento.Perfiles = true;
                                    break;
                                //EXP USUARIOS ACCESOS
                                case 15300:
                                    AccesosModuloMantenimiento.ExpUsuariosAccesos = true;
                                    break;
                                //EXPLORADORES
                                case 16000:
                                    AccesosModuloMantenimiento.Exploradores = true;
                                    break;
                                //CONSULTAS WEB SRI
                                case 16100:
                                    AccesosModuloMantenimiento.ConsultasSRI = true;
                                    break;
                                //PRE INGRESOS
                                case 16200:
                                    AccesosModuloMantenimiento.PreIngresos = true;
                                    break;
                            }
                        }
                    }
                    else if (modulo.ID_MODULO == 2)
                    {
                        deshabilitarControlesHonorario();
                        foreach (var acceso in listaAccesoOpciones)
                        {
                            switch (acceso.ID_ACCESO)
                            {
                                //MENU
                                //ARCHIVO
                                case 21000:
                                    AccesosModuloHonorarios.Archivo = true;
                                    break;
                                //MEDICOS
                                case 21100:
                                    AccesosModuloHonorarios.Medicos = true;
                                    break;
                                //ESPECIALIDADES
                                case 21200:
                                    AccesosModuloHonorarios.Especialidades = true;
                                    break;
                                //VENDEDORES
                                case 21300:
                                    AccesosModuloHonorarios.Vendedores = true;
                                    break;
                                //TIPO DOCUMENTO
                                case 21400:
                                    AccesosModuloHonorarios.Tipo_documento = true;
                                    break;
                                //COMISION CLINICA
                                case 21500:
                                    AccesosModuloHonorarios.ComisionesClinicaReferido = true;
                                    break;
                                //TIPO RETENCION
                                case 21600:
                                    AccesosModuloHonorarios.TipoRetencion = true;
                                    break;
                                //TIPO HONORARIO
                                case 21700:
                                    AccesosModuloHonorarios.TipoHonorario = true;
                                    break;
                                //SALIR
                                case 21800:
                                    AccesosModuloHonorarios.Salir = true;
                                    break;
                                //PROCESO DIARIO
                                case 22000:
                                    AccesosModuloHonorarios.ProcesoDiario = true;
                                    break;
                                //INGRESO FACTURAS
                                case 22100:
                                    AccesosModuloHonorarios.IngresoFacturas = true;
                                    break;
                                //INGRESO HONORATIO
                                case 22200:
                                    AccesosModuloHonorarios.IngresoHonorarios = true;
                                    break;
                                //HONORARIO POR  MEDICO
                                case 22300:
                                    AccesosModuloHonorarios.HonorariosPorMedico = true;
                                    break;
                                //FACTURA POR ANULACION
                                case 22400:
                                    AccesosModuloHonorarios.FacturasAnulacion = true;
                                    break;
                                //ASIENTO CONTABLE HONORARIO
                                case 22500:
                                    AccesosModuloHonorarios.AsientoHonorario = true;
                                    break;
                                //LIQUIDACION DE HONORARIOS
                                case 23000:
                                    AccesosModuloHonorarios.LiquidacionHonorario = true;
                                    break;
                                //ASIGNAR FACTURAS LIQUIDACIONES
                                case 23100:
                                    AccesosModuloHonorarios.AsignarFacturasLiquidacion = true;
                                    break;
                                //EXP LIQUIDACIONES
                                case 23200:
                                    AccesosModuloHonorarios.ExploradorLiquidaciones = true;
                                    break;
                                //LIQUIDACIONES
                                case 23300:
                                    AccesosModuloHonorarios.Liquidaciones = true;
                                    break;
                                //EXPLORADOR
                                case 24000:
                                    AccesosModuloHonorarios.Explorador = true;
                                    break;
                                //EXP GENERAL
                                case 24100:
                                    AccesosModuloHonorarios.ExploradorGeneral = true;
                                    break;
                                //EXP POR MEDICO
                                case 24200:
                                    AccesosModuloHonorarios.ExploradorMedicos = true;
                                    break;
                                //REPORTES
                                case 25000:
                                    AccesosModuloHonorarios.Reporte = true;
                                    break;
                                //REP MEDICOS
                                case 25100:
                                    AccesosModuloHonorarios.ReporteMedicos = true;
                                    break;
                                //REP NOTAS
                                case 25200:
                                    AccesosModuloHonorarios.ReporteNotas = true;
                                    break;
                                //REP RETENCIONES
                                case 25300:
                                    AccesosModuloHonorarios.ReporteRetenciones = true;
                                    break;
                                //REP CONTABLES
                                case 25400:
                                    AccesosModuloHonorarios.ReporteContable = true;
                                    break;
                                //REP COMISIONES VENDEDORES
                                case 25500:
                                    AccesosModuloHonorarios.ReporteComisiones = true;
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                    //Asigna las opciones de acceso para el Modulo de Acceso
                    else if (modulo.ID_MODULO == 5)
                    {
                        foreach (var acceso in listaAccesoOpciones)
                        {
                            switch (acceso.ID_ACCESO)
                            {
                                //MENU
                                //Acceso Modulo de Mantenimiento
                                case 1:
                                    AccesosModuloAcceso.setModuloMantenimiento(true);
                                    break;
                                //Acceso Modulo de Honorarios
                                case 2:
                                    AccesosModuloAcceso.setModuloHonorarios(true);
                                    break;
                                //Acceso Modulo de Tarifario
                                case 3:
                                    AccesosModuloAcceso.setModuloTarifario(true);
                                    break;
                                //Acceso Modulo de Admision
                                case 4:
                                    AccesosModuloAcceso.setModuloAdmision(true);
                                    break;
                                //Acceso Modulo de Habitaciones
                                case 5:
                                    AccesosModuloAcceso.setModuloHabitaciones(true);
                                    break;
                                case 6:
                                    AccesosModuloAcceso.setModuloEmergencias(true);
                                    break;
                                case 7:
                                    AccesosModuloAcceso.setModuloPedidos(true);
                                    break;
                                case 8:
                                    AccesosModuloAcceso.setModuloCuentaPaciente(true);
                                    break;
                                case 9:
                                    AccesosModuloAcceso.ModuloPedidosEspeciales = true;
                                    break;
                                case 10:
                                    AccesosModuloAcceso.ModuloMedicos = true;
                                    break;
                                case 11:
                                    AccesosModuloAcceso.ModuloConsultaExterna = true;
                                    break;
                                case 15:
                                    AccesosModuloAcceso.ModuloEmergencia = true;
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                    else if (modulo.ID_MODULO == 3)//Se cambia por completo la estrucura para manejar seguridades // Mario Valencia // 30/10/2023
                    {
                        deshabilitarControlesAdmision();
                        foreach (var acceso in listaAccesoOpciones)
                        {
                            switch (acceso.ID_ACCESO)
                            {
                                //ARCHIVO
                                case 31000:
                                    AccesosModuloAdmision.Archivo = true;
                                    break;
                                //CHECK LIST
                                case 31100:
                                    AccesosModuloAdmision.Check = true;
                                    break;
                                //FORMULARIOS HCU
                                case 31200:
                                    AccesosModuloAdmision.Formulario = true;
                                    break;
                                //SALIR
                                case 31300:
                                    AccesosModuloAdmision.Salir = true;
                                    break;
                                //ADMISION
                                case 32000:
                                    AccesosModuloAdmision.Admision = true;
                                    break;
                                //ADMISION EMERGENCIA
                                case 33000:
                                    AccesosModuloAdmision.AdmEmergenciaM = true;
                                    break;
                                //ADMISION EMERGENCIA
                                case 33100:
                                    AccesosModuloAdmision.AdmEmergencia = true;
                                    break;
                                //SERVICIOS EXTERNOS
                                case 33200:
                                    AccesosModuloAdmision.ServiciosExtermos = true;
                                    break;
                                //PRE - INGRESO
                                case 33300:
                                    AccesosModuloAdmision.PreIngreso = true;
                                    break;
                                //ESTADISTICA
                                case 34000:
                                    AccesosModuloAdmision.Estadistica = true;
                                    break;
                                //CONTROL HC
                                case 34100:
                                    AccesosModuloAdmision.ControlHc = true;
                                    break;
                                //EXPLORADOR 
                                case 35000:
                                    AccesosModuloAdmision.Explorador = true;
                                    break;
                                //PACIENTES
                                case 35100:
                                    AccesosModuloAdmision.Pacientes = true;
                                    break;
                                //ATENCIONES
                                case 35200:
                                    AccesosModuloAdmision.Atenciones = true;
                                    break;
                                //CUENTAS POR FACTURAR
                                case 35300:
                                    AccesosModuloAdmision.CuentaFacturada = true;
                                    break;
                                //HABITACIONES
                                case 35500:
                                    AccesosModuloAdmision.Habitaciones = true;
                                    break;
                                //HISTORIAS CLINICAS
                                case 35400:
                                    AccesosModuloAdmision.Hc = true;
                                    break;
                                //RUBROS
                                case 35600:
                                    AccesosModuloAdmision.Rubros = true;
                                    break;
                                //EXP PROCEDIMIENTOS
                                case 35700:
                                    AccesosModuloAdmision.ExplProcedimientos = true;
                                    break;
                                //EXP RUBROS PROCEDIMIENTOS
                                case 35800:
                                    AccesosModuloAdmision.ExpProcRubros = true;
                                    break;
                                //REPORTES
                                case 36000:
                                    AccesosModuloAdmision.Reportes = true;
                                    break;
                                //GARANTIAS
                                case 36100:
                                    AccesosModuloAdmision.Garantias = true;
                                    break;
                                //INEC
                                case 36200:
                                    AccesosModuloAdmision.Inec = true;
                                    break;
                                //CENSO DIARIO
                                case 36300:
                                    AccesosModuloAdmision.CensoDiario = true;
                                    break;
                                //CENSO DIARIO SE
                                case 36400:
                                    AccesosModuloAdmision.CensoSxe = true;
                                    break;
                                //SOLICITUD DE HC
                                case 36500:
                                    AccesosModuloAdmision.SolicitudHc = true;
                                    break;
                                //CIERRE DE TURNO
                                case 36600:
                                    AccesosModuloAdmision.CierreTurno = true;
                                    break;
                                //RANGO DE EDADES
                                case 36700:
                                    AccesosModuloAdmision.RangoEdades = true;
                                    break;
                                //DEFUNCIONES
                                case 36800:
                                    AccesosModuloAdmision.Defunciones = true;
                                    break;
                                //RUC - CI
                                case 36900:
                                    AccesosModuloAdmision.RucCi = true;
                                    break;
                                //LABORATORIO
                                case 37000:
                                    AccesosModuloAdmision.Laboratorio = true;
                                    break;
                                //TARIFARIO
                                case 37100:
                                    AccesosModuloAdmision.Tarifario = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (modulo.ID_MODULO == 6)
                    {
                        deshabilitarControlesTarifario();
                        foreach (var acceso in listaAccesoOpciones)
                        {
                            switch (acceso.ID_ACCESO)
                            {
                                //MENU
                                //A 
                                case 601:
                                    AccesosModuloTarifario.ConsultaTipoEmpresa = true;
                                    break;
                                //A
                                case 602:
                                    AccesosModuloTarifario.ConsultaAseguradorasEmpresas = true;
                                    break;
                                //A
                                case 603:
                                    AccesosModuloTarifario.ConsultaConveniosProcedimientos = true;
                                    break;
                                //A
                                case 604:
                                    AccesosModuloTarifario.ConsultaTarifario = true;
                                    break;
                                //A
                                case 605:
                                    AccesosModuloTarifario.ConsultaProcedimientos = true;
                                    break;
                                //A
                                case 606:
                                    AccesosModuloTarifario.ConsultaCreacionHonorarios = true;
                                    break;
                                //A
                                case 607:
                                    AccesosModuloTarifario.ConsultaReportes = true;
                                    break;
                                //A
                                case 608:
                                    AccesosModuloTarifario.ConsultaEstructuraEspecialidades = true;
                                    break;
                                //A
                                case 609:
                                    AccesosModuloTarifario.TipoEmpresaCRUD = true;
                                    break;
                                //A
                                case 610:
                                    AccesosModuloTarifario.AseguradorasEmpresasCRUD = true;
                                    break;
                                //I
                                case 611:
                                    AccesosModuloTarifario.ConveniosCRUD = true;
                                    break;
                                //E
                                case 612:
                                    AccesosModuloTarifario.TarifariosCRUD = true;
                                    break;
                                //E
                                case 613:
                                    AccesosModuloTarifario.ProcedimientosCRUD = true;
                                    break;
                                //I
                                case 614:
                                    AccesosModuloTarifario.CreacionHonorarios = true;
                                    break;

                                case 615:
                                    AccesosModuloTarifario.EstructuraEspecialidades = true;
                                    break;
                                case 171:
                                    AccesosModuloTarifario.CreacionTipoCatologo = true;
                                    break;
                                case 172:
                                    AccesosModuloTarifario.ModificacionTipoCatologo = true;
                                    break;
                                case 173:
                                    AccesosModuloTarifario.EliminacionTipoCatologo = true;
                                    break;
                                case 174:
                                    AccesosModuloTarifario.CreacionCatologo = true;
                                    break;
                                case 175:
                                    AccesosModuloTarifario.ModificacionCatologo = true;
                                    break;
                                case 176:
                                    AccesosModuloTarifario.EliminacionCatologo = true;
                                    break;
                                case 177:
                                    AccesosModuloTarifario.CreacionConvenio = true;
                                    break;
                                case 178:
                                    AccesosModuloTarifario.ModificacionConvenio = true;
                                    break;
                                case 179:
                                    AccesosModuloTarifario.EliminacionConvenio = true;
                                    break;
                                case 180:
                                    AccesosModuloTarifario.CreacionPrecioConvenio = true;
                                    break;
                                case 181:
                                    AccesosModuloTarifario.ModificacionPrecioConvenio = true;
                                    break;
                                case 182:
                                    AccesosModuloTarifario.EliminacionPrecioConvenio = true;
                                    break;
                                //ADMINISTRACION
                                case 61000:
                                    AccesosModuloTarifario.Administracion = true;
                                    break;
                                //TIPO EMPRESA
                                case 61100:
                                    AccesosModuloTarifario.TipoEmpresa = true;
                                    break;
                                //ASEGURADORAS Y EMPRESAS
                                case 61200:
                                    AccesosModuloTarifario.AseguradoraEmpresa = true;
                                    break;
                                //CONVENIOS
                                case 61300:
                                    AccesosModuloTarifario.Convenio = true;
                                    break;
                                //TARIFARIO
                                case 61400:
                                    AccesosModuloTarifario.Tarifario = true;
                                    break;
                                //PROCEDIMIENTO
                                case 61500:
                                    AccesosModuloTarifario.Procedimiento = true;
                                    break;
                                //TARIFARIO
                                case 62000:
                                    AccesosModuloTarifario.MenuTarifario = true;
                                    break;
                                //CREACION HONORARIOS
                                case 62100:
                                    AccesosModuloTarifario.CreacionHonorarios = true;
                                    break;
                                //CONSULTA HONORARIOS
                                case 62200:
                                    AccesosModuloTarifario.ConsultaHonorario = true;
                                    break;
                                //PRECIOS Y PORCENTAJES
                                case 63000:
                                    AccesosModuloTarifario.PreciosProcentajes = true;
                                    break;
                                //TIPO COSTO
                                case 63100:
                                    AccesosModuloTarifario.TipoCosto = true;
                                    break;
                                //CATALOGO DE COSTO
                                case 63200:
                                    AccesosModuloTarifario.CatalogoCosto = true;
                                    break;
                                //CONVENIO
                                case 63300:
                                    AccesosModuloTarifario.Convenios = true;
                                    break;
                                //PRECIOS POR CONVENIO
                                case 63400:
                                    AccesosModuloTarifario.PreciosConvenios = true;
                                    break;
                                //REPORTES
                                case 64000:
                                    AccesosModuloTarifario.Reporte = true;
                                    break;
                                //HONORARIOS
                                case 64100:
                                    AccesosModuloTarifario.Honorario = true;
                                    break;
                                //EMPRESAS Y ASEGURADORAS
                                case 64200:
                                    AccesosModuloTarifario.EmpresaAseguradora = true;
                                    break;
                                //VENTAS
                                case 65000:
                                    AccesosModuloTarifario.Ventas = true;
                                    break;
                                //MOSAICO HORIZONTAL
                                case 65100:
                                    AccesosModuloTarifario.MosaicoHorizaontal = true;
                                    break;
                                //MOSAICO VERTICAL
                                case 65200:
                                    AccesosModuloTarifario.MosaicoVertical = true;
                                    break;
                                //CASCADA
                                case 65300:
                                    AccesosModuloTarifario.Cascada = true;
                                    break;
                                // ORGANIZAR ICONOS
                                case 65400:
                                    AccesosModuloTarifario.OrganizarIcono = true;
                                    break;
                                //AYUDA
                                case 66000:
                                    AccesosModuloTarifario.Ayuda = true;
                                    break;
                                // ACERCA DE
                                case 66100:
                                    AccesosModuloTarifario.AcercaDe = true;
                                    break;
                                default:
                                    break;
                            }


                        }
                    }
                    else if (modulo.ID_MODULO == 7)
                    {
                        deshabilitarControlesCuentaPaciente();
                        foreach (var acceso in listaAccesoOpciones)
                        {
                            switch (acceso.ID_ACCESO)
                            {
                                //FACTURACION
                                case 71000:
                                    AccesosModuloCuentaPaciente.Facturacion = true;
                                    break;
                                //NUEVA FACTURA
                                case 71100:
                                    AccesosModuloCuentaPaciente.NuevaFactura = true;
                                    break;
                                //DIVISION CUENTAS
                                case 72000:
                                    AccesosModuloCuentaPaciente.DivisionCuentas = true;
                                    break;
                                //REVISION CUENTAS
                                case 72100:
                                    AccesosModuloCuentaPaciente.RevisionCuentas = true;
                                    break;
                                //INFORME
                                case 73000:
                                    AccesosModuloCuentaPaciente.Informe = true;
                                    break;
                                //CIERRE TURNO
                                case 73100:
                                    AccesosModuloCuentaPaciente.CierreTurno = true;
                                    break;
                                //DETALLE CAMBIO CUENTAS
                                case 73200:
                                    AccesosModuloCuentaPaciente.CambioCuenta = true;
                                    break;
                                //DETALLE VALORES CUENTAS
                                case 73300:
                                    AccesosModuloCuentaPaciente.ValoresCuenta = true;
                                    break;
                                //EXPLORADOR AUDITORIA
                                case 73400:
                                    AccesosModuloCuentaPaciente.ExpAuditoria = true;
                                    break;
                                //GARANTIAS
                                case 74000:
                                    AccesosModuloCuentaPaciente.Garantias = true;
                                    break;
                                //NUEVA GARANTIA
                                case 74100:
                                    AccesosModuloCuentaPaciente.NuevaGarantia = true;
                                    break;
                                //PRE AUTORIZACION
                                case 74200:
                                    AccesosModuloCuentaPaciente.PreAutorizacion = true;
                                    break;
                                //REPORTE
                                case 74300:
                                    AccesosModuloCuentaPaciente.reporte = true;
                                    break;
                                //AUDITORIA
                                case 75000:
                                    AccesosModuloCuentaPaciente.Auditoria = true;
                                    break;
                                //DETALLE ATENCION
                                case 75100:
                                    AccesosModuloCuentaPaciente.DetalleAtencion = true;
                                    break;
                                //ESTADO CUENTA
                                case 75200:
                                    AccesosModuloCuentaPaciente.EstadoCuenta = true;
                                    break;
                            }
                        }
                    }
                    else if (modulo.ID_MODULO == 8)
                    {
                        deshabilitarControlesPedidos();
                        foreach (var acceso in listaAccesoOpciones)
                        {
                            switch (acceso.ID_ACCESO)
                            {
                                //HERRAMIENTAS
                                case 81000:
                                    AccesosModuloPedidos.Herramienta = true;
                                    break;
                                //CONTROL DE DESPACHO
                                case 81100:
                                    AccesosModuloPedidos.ControlDespscho = true;
                                    break;
                                //EXPLORADOR DE PEDIDOS
                                case 81200:
                                    AccesosModuloPedidos.ExploradorPedidos = true;
                                    break;
                                //MONITOREO PEDIDOS
                                case 81300:
                                    AccesosModuloPedidos.MonitoreoPedidos = true;
                                    break;
                                //MONITOREO DEVOLUCIONES
                                case 81400:
                                    AccesosModuloPedidos.MonitoreoDevoluciones = true;
                                    break;
                                //REPORTES
                                case 82000:
                                    AccesosModuloPedidos.Reportes = true;
                                    break;
                                //CONSULTA PEDIDOS
                                case 82100:
                                    AccesosModuloPedidos.ConsultaPedidos = true;
                                    break;
                            }
                        }
                    }
                    else if (modulo.ID_MODULO == 9)
                    {
                        deshabilitarControlesPedidosEspeciales();
                        foreach (var acceso in listaAccesoOpciones)
                        {
                            switch (acceso.ID_ACCESO)
                            {
                                //DIETETICA
                                case 91000:
                                    AccesoModuloPedidosEspeciales.Dietetica = true;
                                    break;
                                //PEDIDO
                                case 91100:
                                    AccesoModuloPedidosEspeciales.Pedido = true;
                                    break;
                                //GASTROENTEROLOGIA
                                case 92000:
                                    AccesoModuloPedidosEspeciales.Gastroenterologia = true;
                                    break;
                                //AGERGAR PRODUCTO
                                case 92100:
                                    AccesoModuloPedidosEspeciales.GagergarProducto = true;
                                    break;
                                //AREGAR PROCEDIMINETO
                                case 92200:
                                    AccesoModuloPedidosEspeciales.GagregarProcedimiento = true;
                                    break;
                                //PEDIDO PACIENTE
                                case 92300:
                                    AccesoModuloPedidosEspeciales.GpedidoPaciente = true;
                                    break;
                                //REPOSICION DE PRODUCTOS
                                case 92400:
                                    AccesoModuloPedidosEspeciales.GreposicionProducto = true;
                                    break;
                                //IMAGEN
                                case 93000:
                                    AccesoModuloPedidosEspeciales.Imagen = true;
                                    break;
                                //AENDAMIENTO
                                case 93100:
                                    AccesoModuloPedidosEspeciales.Agendamiento = true;
                                    break;
                                //EXAMENES AGENDADOS
                                case 93200:
                                    AccesoModuloPedidosEspeciales.ExamenesAgendados = true;
                                    break;
                                //INFORME
                                case 93300:
                                    AccesoModuloPedidosEspeciales.Informe = true;
                                    break;
                                //EXP PEDIDOS
                                case 93400:
                                    AccesoModuloPedidosEspeciales.ExplPedidos = true;
                                    break;
                                //HORARIO DE MEDICOS
                                case 93500:
                                    AccesoModuloPedidosEspeciales.HorarioMedico = true;
                                    break;
                                //LAB CLINICO
                                case 94000:
                                    AccesoModuloPedidosEspeciales.LabClinico = true;
                                    break;
                                //CREA PERFILES
                                case 94100:
                                    AccesoModuloPedidosEspeciales.CrearPerfiles = true;
                                    break;
                                //EXPLORADOR DE PEDIDOS
                                case 94200:
                                    AccesoModuloPedidosEspeciales.CexplPedidos = true;
                                    break;
                                //PACIENTES
                                case 94300:
                                    AccesoModuloPedidosEspeciales.Pacientes = true;
                                    break;
                                //EXAMENES POR FERFILES
                                case 94400:
                                    AccesoModuloPedidosEspeciales.ExamenesPerfiles = true;
                                    break;
                                //LAB PATOLOGICO
                                case 95000:
                                    AccesoModuloPedidosEspeciales.LabPatologico = true;
                                    break;
                                //EXPLORADOR PEDIDOS
                                case 95100:
                                    AccesoModuloPedidosEspeciales.PexplPedidos = true;
                                    break;
                                //QUIROFANO
                                case 96000:
                                    AccesoModuloPedidosEspeciales.Quirofano = true;
                                    break;
                                //AGREGAR PRODUCTO
                                case 96100:
                                    AccesoModuloPedidosEspeciales.QagergarProducto = true;
                                    break;
                                //AGREGAR PROCEDIMIENTO
                                case 96200:
                                    AccesoModuloPedidosEspeciales.QagregarProcedimiento = true;
                                    break;
                                //PEDIDO PACIENTE
                                case 96300:
                                    AccesoModuloPedidosEspeciales.QpedidoPaciente = true;
                                    break;
                                //REPOSICION DE PRODUCTOS
                                case 96400:
                                    AccesoModuloPedidosEspeciales.QreposicionProducto = true;
                                    break;
                                //EXP PROCEDIMIENTOS
                                case 96500:
                                    AccesoModuloPedidosEspeciales.ExpProcedimiento = true;
                                    break;
                                //EXP RUBROS PROCEDIMIENTO
                                case 96600:
                                    AccesoModuloPedidosEspeciales.ExpRubros = true;
                                    break;
                            }
                        }
                    }
                    else if (modulo.ID_MODULO == 10)
                    {
                        deshabilitarControlesMedicos();
                        foreach (var acceso in listaAccesoOpciones)
                        {
                            switch (acceso.ID_ACCESO)
                            {
                                //EXPLORADOR
                                case 101000:
                                    AccesoModuloMedicos.Explorador = true;
                                    break;
                                //EXP CERTIFICADO MEDICO
                                case 101100:
                                    AccesoModuloMedicos.explCertificadoMedico = true;
                                    break;
                                //EXP RECETA MEDICA
                                case 101200:
                                    AccesoModuloMedicos.explRecetaMedica = true;
                                    break;
                                //EXP HC
                                case 101300:
                                    AccesoModuloMedicos.explHc = true;
                                    break;
                                // EXP CERTIFICADO ASISTENCIA
                                case 101400:
                                    AccesoModuloMedicos.explCertificadoAsistencia = true;
                                    break;
                                //MEDICOS
                                case 102000:
                                    AccesoModuloMedicos.Medicos = true;
                                    break;
                                //CERTIFICADO COVID
                                case 102100:
                                    AccesoModuloMedicos.certificadoCovid = true;
                                    break;
                                //CERTIFICADO GENERAL
                                case 102200:
                                    AccesoModuloMedicos.certificadoGeneral = true;
                                    break;
                                // CERTIFICADO ASISTENCIA
                                case 102300:
                                    AccesoModuloMedicos.certificadoAsistencia = true;
                                    break;
                                //RECETA MEDICA
                                case 102400:
                                    AccesoModuloMedicos.certificadoRrecetaMedica = true;
                                    break;
                            }
                        }
                    }
                    else if (modulo.ID_MODULO == 11)
                    {
                        deshabilitarControlesConsultaExterna();
                        foreach (var acceso in listaAccesoOpciones)
                        {
                            switch (acceso.ID_ACCESO)
                            {
                                //AGENDA
                                case 111000:
                                    AccesosModuloConsultaExterna.Agendamiento = true;
                                    break;
                                //AGENDAMIENTO PACIENTE
                                case 111100:
                                    AccesosModuloConsultaExterna.AgendaPacientes = true;
                                    break;
                                //EXP CITAS MEDICAS
                                case 111200:
                                    AccesosModuloConsultaExterna.ExpCitasMedicas = true;
                                    break;
                                //CONSULTA EXTERNA
                                case 112000:
                                    AccesosModuloConsultaExterna.ConsultaExterna = true;
                                    break;
                                //ADMISION
                                case 112100:
                                    AccesosModuloConsultaExterna.Admision = true;
                                    break;
                                //TRIAJE
                                case 112200:
                                    AccesosModuloConsultaExterna.Triaje = true;
                                    break;
                                //SIGNOS VITALES
                                case 112300:
                                    AccesosModuloConsultaExterna.SignosVitales = true;
                                    break;
                                //HABITACIONES
                                case 112400:
                                    AccesosModuloConsultaExterna.Habitaciones = true;
                                    break;
                                //CONSULTA
                                case 112500:
                                    AccesosModuloConsultaExterna.Consulta = true;
                                    break;
                                //FACTURACION
                                case 112600:
                                    AccesosModuloConsultaExterna.Facturacion = true;
                                    break;
                                //EXP CONSULTA EXTERNA
                                case 112700:
                                    AccesosModuloConsultaExterna.ExpConsultaExterna = true;
                                    break;
                                //EXP CERTIFICADO
                                case 112800:
                                    AccesosModuloConsultaExterna.ExpCertificado = true;
                                    break;
                                //EXP RECETA MEDICA
                                case 112900:
                                    AccesosModuloConsultaExterna.ExpReceta = true;
                                    break;
                            }
                        }
                    }
                    else if (modulo.ID_MODULO == 12)
                    {
                        deshabilitarControlesEmergencia();
                        foreach (var acceso in listaAccesoOpciones)
                        {
                            switch (acceso.ID_ACCESO)
                            {
                                //EMERGENCIA
                                case 121000:
                                    AccesosModuloEmergencia.Emergencia = true;
                                    break;
                                //TRIAJE
                                case 121100:
                                    AccesosModuloEmergencia.Triaje = true;
                                    break;
                                //FORMULARIO 008
                                case 121200:
                                    AccesosModuloEmergencia.Formulario = true;
                                    break;
                                //EVOLUCION MEDICOS
                                case 121300:
                                    AccesosModuloEmergencia.Evolucion = true;
                                    break;
                                //CERTIFICADO MEDICO
                                case 121400:
                                    AccesosModuloEmergencia.Certificado = true;
                                    break;
                                //RECETA MEDICA
                                case 121500:
                                    AccesosModuloEmergencia.Receta = true;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public static bool ParametroBodega()
        {
            return new DatAccesoOpciones().ParametroBodega();
        }
        public static List<DtopAccesosOpciones> ListarAccesoOpcionesXmodulo(Int64 id_modulo, Int64 id_perfil)
        {
            return new DatAccesoOpciones().ListarAccesoOpcionesXmodulo(id_modulo, id_perfil);
        }
        public static DataTable ExploradorUsrAccSic()
        {
            return new DatAccesoOpciones().ExploradorUsrAccSic();
        }
        public static DataTable ExploradorUsrInacSic()
        {
            return new DatAccesoOpciones().ExploradorUsrInacSic();
        }
        public static DataTable ExploradorUsrAccCG()
        {
            return new DatAccesoOpciones().ExploradorUsrAccCG();
        }
        public static DataTable ExploradorUsrInacCG()
        {
            return new DatAccesoOpciones().ExploradorUsrInacCG();
        }
        public static List<DtoExplotadorUsuariosHis> ExploradorUsrAccHis()
        {
            return new DatAccesoOpciones().ExploradorUsrAccHis();
        }
        public static List<DtoExplotadorUsuariosHis> ExploradorUsrInacHis()
        {
            return new DatAccesoOpciones().ExploradorUsrInacHis();
        }
        public static List<ACCESO_OPCIONES> RecuperaAccesosOpcionesXmodulo(Int32 id_modulo)
        {
            return new DatAccesoOpciones().RecuperaAccesosOpcionesXmodulo(id_modulo);
        }
        public static void EliminarAccesoOpciones1(List<ACCESO_OPCIONES> accOp)
        {
            new DatAccesoOpciones().EliminarAccesoOpciones1(accOp);
        }
        public static List<DtoAccesosHis> recuperaAccesoUsuario(Int64 ID_USUARIO)
        {
            return new DatAccesoOpciones().recuperaAccesoUsuario(ID_USUARIO);
        }
        public static PERFILES_ACCESOS accesosModulosXperfiles(Int64 id_perfil, Int64 id_acceso)
        {
            return new DatAccesoOpciones().accesosModulosXperfiles(id_perfil, id_acceso);
        }
        public static List<ACCESO_OPCIONES> accesosXModulo(Int32 id_modulo)
        {
            return new DatAccesoOpciones().accesosXModulo(id_modulo);
        }
        public static List<ACCESO_OPCIONES> accesosXPerfil(Int32 id_modlo, Int64 id_perfil)
        {
            return new DatAccesoOpciones().accesosXPerfil(id_modlo, id_perfil);
        }
        public static DataTable accesosXModuloSic(Int32 id_modulo)
        {
            return new DatAccesoOpciones().accesosXModuloSic(id_modulo);
        }
        public static DataTable accesosXPerfilSic(Int32 id_modulo, Int64 id_perfil)
        {
            return new DatAccesoOpciones().accesosXPerfilSic(id_modulo, id_perfil);
        }
        public static DataTable accesosXModuloCg(Int32 id_modulo)
        {
            return new DatAccesoOpciones().accesosXModuloCg(id_modulo);
        }
        public static DataTable accesosXPerfilCg(Int32 id_modulo, Int64 id_perfil)
        {
            return new DatAccesoOpciones().accesosXPerfilCg(id_modulo, id_perfil);
        }
        public static void deshabilitarControlesAdmision()
        {
            //ARCHIVO
            AccesosModuloAdmision.Archivo = false;
            //CHECK LIST
            AccesosModuloAdmision.Check = false;
            //FORMULARIOS HCU
            AccesosModuloAdmision.Formulario = false;
            //SALIR
            AccesosModuloAdmision.Salir = false;
            //ADMISION
            AccesosModuloAdmision.Admision = false;
            //ADMISION EMERGENCIA
            AccesosModuloAdmision.AdmEmergenciaM = false;
            //ADMISION EMERGENCIA
            AccesosModuloAdmision.AdmEmergencia = false;
            //SERVICIOS EXTERNOS
            AccesosModuloAdmision.ServiciosExtermos = false;
            //PRE - INGRESO
            AccesosModuloAdmision.PreIngreso = false;
            //ESTADISTICA
            AccesosModuloAdmision.Estadistica = false;
            //CONTROL HC
            AccesosModuloAdmision.ControlHc = false;
            //EXPLORADOR 
            AccesosModuloAdmision.Explorador = false;
            //PACIENTES
            AccesosModuloAdmision.Pacientes = false;
            //ATENCIONES
            AccesosModuloAdmision.Atenciones = false;
            //CUENTAS POR FACTURAR
            AccesosModuloAdmision.CuentaFacturada = false;
            //HABITACIONES
            AccesosModuloAdmision.Habitaciones = false;
            //HISTORIAS CLINICAS
            AccesosModuloAdmision.Hc = false;
            //RUBROS
            AccesosModuloAdmision.Rubros = false;
            //EXP PROCEDIMIENTOS
            AccesosModuloAdmision.ExplProcedimientos = false;
            //EXP RUBROS PROCEDIMIENTOS
            AccesosModuloAdmision.ExpProcRubros = false;
            //REPORTES
            AccesosModuloAdmision.Reportes = false;
            //GARANTIAS
            AccesosModuloAdmision.Garantias = false;
            //INEC
            AccesosModuloAdmision.Inec = false;
            //CENSO DIARIO
            AccesosModuloAdmision.CensoDiario = false;
            //CENSO DIARIO SE
            AccesosModuloAdmision.CensoSxe = false;
            //SOLICITUD DE HC
            AccesosModuloAdmision.SolicitudHc = false;
            //CIERRE DE TURNO
            AccesosModuloAdmision.CierreTurno = false;
            //RANGO DE EDADES
            AccesosModuloAdmision.RangoEdades = false;
            //DEFUNCIONES
            AccesosModuloAdmision.Defunciones = false;
            //RUC - CI
            AccesosModuloAdmision.RucCi = false;
            //LABORATORIO
            AccesosModuloAdmision.Laboratorio = false;
            //TARIFARIO
            AccesosModuloAdmision.Tarifario = false;
        }
        public static void deshabilitarControlesHonorario()
        {
            //ARCHIVO
            AccesosModuloHonorarios.Archivo = false;
            //MEDICOS
            AccesosModuloHonorarios.Medicos = false;
            //ESPECIALIDADES
            AccesosModuloHonorarios.Especialidades = false;
            //VENDEDORES
            AccesosModuloHonorarios.Vendedores = false;
            //TIPO DOCUMENTO
            AccesosModuloHonorarios.Tipo_documento = false;
            //COMISION CLINICA
            AccesosModuloHonorarios.ComisionesClinicaReferido = false;
            //TIPO RETENCION
            AccesosModuloHonorarios.TipoRetencion = false;
            //TIPO HONORARIO
            AccesosModuloHonorarios.TipoHonorario = false;
            //SALIR
            AccesosModuloHonorarios.Salir = false;
            //PROCESO DIARIO
            AccesosModuloHonorarios.ProcesoDiario = false;
            //INGRESO FACTURAS
            AccesosModuloHonorarios.IngresoFacturas = false;
            //INGRESO HONORATIO
            AccesosModuloHonorarios.IngresoHonorarios = false;
            //HONORARIO POR  MEDICO
            AccesosModuloHonorarios.HonorariosPorMedico = false;
            //FACTURA POR ANULACION
            AccesosModuloHonorarios.FacturasAnulacion = false;
            //ASIENTO CONTABLE HONORARIO
            AccesosModuloHonorarios.AsientoHonorario = false;
            //LIQUIDACION DE HONORARIOS
            AccesosModuloHonorarios.LiquidacionHonorario = false;
            //ASIGNAR FACTURAS LIQUIDACIONES
            AccesosModuloHonorarios.AsignarFacturasLiquidacion = false;
            //EXP LIQUIDACIONES
            AccesosModuloHonorarios.ExploradorLiquidaciones = false;
            //LIQUIDACIONES
            AccesosModuloHonorarios.Liquidaciones = false;
            //EXPLORADOR
            AccesosModuloHonorarios.Explorador = false;
            //EXP GENERAL
            AccesosModuloHonorarios.ExploradorGeneral = false;
            //EXP POR MEDICO
            AccesosModuloHonorarios.ExploradorMedicos = false;
            //REPORTES
            AccesosModuloHonorarios.Reporte = false;
            //REP MEDICOS
            AccesosModuloHonorarios.ReporteMedicos = false;
            //REP NOTAS
            AccesosModuloHonorarios.ReporteNotas = false;
            //REP RETENCIONES
            AccesosModuloHonorarios.ReporteRetenciones = false;
            //REP CONTABLES
            AccesosModuloHonorarios.ReporteContable = false;
            //REP COMISIONES VENDEDORES
            AccesosModuloHonorarios.ReporteComisiones = false;
        }
        public static void deshabilitarControlesTarifario()
        {
            //ADMINISTRACION
            AccesosModuloTarifario.Administracion = false;
            //TIPO EMPRESA
            AccesosModuloTarifario.TipoEmpresa = false;
            //ASEGURADORAS Y EMPRESAS
            AccesosModuloTarifario.AseguradoraEmpresa = false;
            //CONVENIOS
            AccesosModuloTarifario.Convenio = false;
            //TARIFARIO
            AccesosModuloTarifario.Tarifario = false;
            //PROCEDIMIENTO
            AccesosModuloTarifario.Procedimiento = false;
            //TARIFARIO
            AccesosModuloTarifario.MenuTarifario = false;
            //CREACION HONORARIOS
            AccesosModuloTarifario.CreacionHonorarios = false;
            //CONSULTA HONORARIOS
            AccesosModuloTarifario.ConsultaHonorario = false;
            //PRECIOS Y PORCENTAJES
            AccesosModuloTarifario.PreciosProcentajes = false;
            //TIPO COSTO
            AccesosModuloTarifario.TipoCosto = false;
            //CATALOGO DE COSTO
            AccesosModuloTarifario.CatalogoCosto = false;
            //CONVENIO
            AccesosModuloTarifario.Convenios = false;
            //PRECIOS POR CONVENIO
            AccesosModuloTarifario.PreciosConvenios = false;
            //REPORTES
            AccesosModuloTarifario.Reporte = false;
            //HONORARIOS
            AccesosModuloTarifario.Honorario = false;
            //EMPRESAS Y ASEGURADORAS
            AccesosModuloTarifario.EmpresaAseguradora = false;
            //VENTAS
            AccesosModuloTarifario.Ventas = false;
            //MOSAICO HORIZONTAL
            AccesosModuloTarifario.MosaicoHorizaontal = false;
            //MOSAICO VERTICAL
            AccesosModuloTarifario.MosaicoVertical = false;
            //CASCADA
            AccesosModuloTarifario.Cascada = false;
            // ORGANIZAR ICONOS
            AccesosModuloTarifario.OrganizarIcono = false;
            //AYUDA
            AccesosModuloTarifario.Ayuda = false;
            // ACERCA DE
            AccesosModuloTarifario.AcercaDe = false;
        }
        public static void deshabilitarControlesPedidosEspeciales()
        {
            //DIETETICA
            AccesoModuloPedidosEspeciales.Dietetica = false;
            //PEDIDO
            AccesoModuloPedidosEspeciales.Pedido = false;
            //GASTROENTEROLOGIA
            AccesoModuloPedidosEspeciales.Gastroenterologia = false;
            //AGERGAR PRODUCTO
            AccesoModuloPedidosEspeciales.GagergarProducto = false;
            //AREGAR PROCEDIMINETO
            AccesoModuloPedidosEspeciales.GagregarProcedimiento = false;
            //PEDIDO PACIENTE
            AccesoModuloPedidosEspeciales.GpedidoPaciente = false;
            //REPOSICION DE PRODUCTOS
            AccesoModuloPedidosEspeciales.GreposicionProducto = false;
            //IMAGEN
            AccesoModuloPedidosEspeciales.Imagen = false;
            //AENDAMIENTO
            AccesoModuloPedidosEspeciales.Agendamiento = false;
            //EXAMENES AGENDADOS
            AccesoModuloPedidosEspeciales.ExamenesAgendados = false;
            //INFORME
            AccesoModuloPedidosEspeciales.Informe = false;
            //EXP PEDIDOS
            AccesoModuloPedidosEspeciales.ExplPedidos = false;
            //HORARIO DE MEDICOS
            AccesoModuloPedidosEspeciales.HorarioMedico = false;
            //LAB CLINICO
            AccesoModuloPedidosEspeciales.LabClinico = false;
            //CREA PERFILES
            AccesoModuloPedidosEspeciales.CrearPerfiles = false;
            //EXPLORADOR DE PEDIDOS
            AccesoModuloPedidosEspeciales.CexplPedidos = false;
            //PACIENTES
            AccesoModuloPedidosEspeciales.Pacientes = false;
            //EXAMENES POR FERFILES
            AccesoModuloPedidosEspeciales.ExamenesPerfiles = false;
            //LAB PATOLOGICO
            AccesoModuloPedidosEspeciales.LabPatologico = false;
            //EXPLORADOR PEDIDOS
            AccesoModuloPedidosEspeciales.PexplPedidos = false;
            //QUIROFANO
            AccesoModuloPedidosEspeciales.Quirofano = false;
            //AGREGAR PRODUCTO
            AccesoModuloPedidosEspeciales.QagergarProducto = false;
            //AGREGAR PROCEDIMIENTO
            AccesoModuloPedidosEspeciales.QagregarProcedimiento = false;
            //PEDIDO PACIENTE
            AccesoModuloPedidosEspeciales.QpedidoPaciente = false;
            //REPOSICION DE PRODUCTOS
            AccesoModuloPedidosEspeciales.QreposicionProducto = false;
            //EXP PROCEDIMIENTOS
            AccesoModuloPedidosEspeciales.ExpProcedimiento = false;
            //EXP RUBROS PROCEDIMIENTO
            AccesoModuloPedidosEspeciales.ExpRubros = false;
        }
        public static void deshabilitarControlesConsultaExterna()
        {
            //AGENDA
            AccesosModuloConsultaExterna.Agendamiento = false;
            //AGENDAMIENTO PACIENTE
            AccesosModuloConsultaExterna.AgendaPacientes = false;
            //EXP CITAS MEDICAS
            AccesosModuloConsultaExterna.ExpCitasMedicas = false;
            //CONSULTA EXTERNA
            AccesosModuloConsultaExterna.ConsultaExterna = false;
            //ADMISION
            AccesosModuloConsultaExterna.Admision = false;
            //TRIAJE
            AccesosModuloConsultaExterna.Triaje = false;
            //SIGNOS VITALES
            AccesosModuloConsultaExterna.SignosVitales = false;
            //HABITACIONES
            AccesosModuloConsultaExterna.Habitaciones = false;
            //CONSULTA
            AccesosModuloConsultaExterna.Consulta = false;
            //FACTURACION
            AccesosModuloConsultaExterna.Facturacion = false;
            //EXP CONSULTA EXTERNA
            AccesosModuloConsultaExterna.ExpConsultaExterna = false;
            //EXP CERTIFICADO
            AccesosModuloConsultaExterna.ExpCertificado = false;
            //EXP RECETA MEDICA
            AccesosModuloConsultaExterna.ExpReceta = false;
        }
        public static void deshabilitarControlesMedicos()
        {
            //EXPLORADOR
            AccesoModuloMedicos.Explorador = false;
            //EXP CERTIFICADO MEDICO
            AccesoModuloMedicos.explCertificadoMedico = false;
            //EXP RECETA MEDICA
            AccesoModuloMedicos.explRecetaMedica = false;
            //EXP HC
            AccesoModuloMedicos.explHc = false;
            // EXP CERTIFICADO ASISTENCIA
            AccesoModuloMedicos.explCertificadoAsistencia = false;
            //MEDICOS
            AccesoModuloMedicos.Medicos = false;
            //CERTIFICADO COVID
            AccesoModuloMedicos.certificadoCovid = false;
            //CERTIFICADO GENERAL
            AccesoModuloMedicos.certificadoGeneral = false;
            // CERTIFICADO ASISTENCIA
            AccesoModuloMedicos.certificadoAsistencia = false;
            //RECETA MEDICA
            AccesoModuloMedicos.certificadoRrecetaMedica = false;
        }
        public static void deshabilitarControlesEmergencia()
        {
            //EMERGENCIA
            AccesosModuloEmergencia.Emergencia = false;
            //TRIAJE
            AccesosModuloEmergencia.Triaje = false;
            //FORMULARIO 008
            AccesosModuloEmergencia.Formulario = false;
            //EVOLUCION MEDICOS
            AccesosModuloEmergencia.Evolucion = false;
            //CERTIFICADO MEDICO
            AccesosModuloEmergencia.Certificado = false;
            //RECETA MEDICA
            AccesosModuloEmergencia.Receta = false;
        }
        public static void deshabilitarControlesPedidos()
        {
            //HERRAMIENTAS
            AccesosModuloPedidos.Herramienta = false;
            //CONTROL DE DESPACHO
            AccesosModuloPedidos.ControlDespscho = false;
            //EXPLORADOR DE PEDIDOS
            AccesosModuloPedidos.ExploradorPedidos = false;
            //MONITOREO PEDIDOS
            AccesosModuloPedidos.MonitoreoPedidos = false;
            //MONITOREO DEVOLUCIONES
            AccesosModuloPedidos.MonitoreoDevoluciones = false;
            //REPORTES
            AccesosModuloPedidos.Reportes = false;
            //CONSULTA PEDIDOS
            AccesosModuloPedidos.ConsultaPedidos = false;
        }
        public static void deshabilitarControlesCuentaPaciente()
        {
            //FACTURACION
            AccesosModuloCuentaPaciente.Facturacion = false;
            //NUEVA FACTURA
            AccesosModuloCuentaPaciente.NuevaFactura = false;
            //DIVISION CUENTAS
            AccesosModuloCuentaPaciente.DivisionCuentas = false;
            //REVISION CUENTAS
            AccesosModuloCuentaPaciente.RevisionCuentas = false;
            //INFORME
            AccesosModuloCuentaPaciente.Informe = false;
            //CIERRE TURNO
            AccesosModuloCuentaPaciente.CierreTurno = false;
            //DETALLE CAMBIO CUENTAS
            AccesosModuloCuentaPaciente.CambioCuenta = false;
            //DETALLE VALORES CUENTAS
            AccesosModuloCuentaPaciente.ValoresCuenta = false;
            //EXPLORADOR AUDITORIA
            AccesosModuloCuentaPaciente.ExpAuditoria = false;
            //GARANTIAS
            AccesosModuloCuentaPaciente.Garantias = false;
            //NUEVA GARANTIA
            AccesosModuloCuentaPaciente.NuevaGarantia = false;
            //PRE AUTORIZACION
            AccesosModuloCuentaPaciente.PreAutorizacion = false;
            //REPORTE
            AccesosModuloCuentaPaciente.reporte = false;
            //AUDITORIA
            AccesosModuloCuentaPaciente.Auditoria = false;
            //DETALLE ATENCION
            AccesosModuloCuentaPaciente.DetalleAtencion = false;
            //ESTADO CUENTA
            AccesosModuloCuentaPaciente.EstadoCuenta = false;
        }
        public static void deshabilitarControlesMantenimiento()
        {
            //ARCHIVO
            AccesosModuloMantenimiento.Archivo = false;
            //EMPRESAS 
            AccesosModuloMantenimiento.Empresas = false;
            //ZONAS Y LOCALES
            AccesosModuloMantenimiento.ZonasLocales = false;
            //DEPARTAMENTOS
            AccesosModuloMantenimiento.Departamento = false;
            //CAJAS
            AccesosModuloMantenimiento.Cajas = false;
            //PRECIOS Y PORCENTAJES CONVENIOS
            AccesosModuloMantenimiento.PreciosPorcentaje = false;
            //TIPOS DE COSTO
            AccesosModuloMantenimiento.TipoCosto = false;
            //CATALOGO DE COSTOS
            AccesosModuloMantenimiento.CatalogoCosto = false;
            //CONVENIOS
            AccesosModuloMantenimiento.Convenios = false;
            //PRECIOS POR CONVENIOS
            AccesosModuloMantenimiento.PrecioConvenio = false;
            //MANTENIMIENTO DE TABLAS
            AccesosModuloMantenimiento.MantenimientoTablas = false;
            //DIVISION POLITICA
            AccesosModuloMantenimiento.DivisionPolitica = false;
            //NACIONALIDAD
            AccesosModuloMantenimiento.Nacionalidad = false;
            //BANCOS
            AccesosModuloMantenimiento.Bancos = false;
            //TIPO DE NEGOCIO
            AccesosModuloMantenimiento.TipoNegocio = false;
            //TIPO DE MEDICO
            AccesosModuloMantenimiento.TipoMedico = false;
            //NUMEROS DE CONTROL
            AccesosModuloMantenimiento.NumeroControl = false;
            //TIPO DE ATENCIONES
            AccesosModuloMantenimiento.TipoAtenciones = false;
            //TIPO DE INGRESO
            AccesosModuloMantenimiento.TipoIngreso = false;
            //TIPOS DE COUDADANOS
            AccesosModuloMantenimiento.TipoCiudadano = false;
            //PISO MAQUINA
            AccesosModuloMantenimiento.PisoMaquina = false;
            //PISO
            AccesosModuloMantenimiento.Piso = false;
            //GRUPO DE PRODUCTOS
            AccesosModuloMantenimiento.GrupoProductos = false;
            //HABITACIONES
            AccesosModuloMantenimiento.Habitaciones = false;
            //CAMBIO TIPO ATENCION
            AccesosModuloMantenimiento.CambioAtencion = false;
            //SEGURIDADES
            AccesosModuloMantenimiento.Seguridades = false;
            //USUARIOS
            AccesosModuloMantenimiento.Usuarios = false;
            //PERFILES
            AccesosModuloMantenimiento.Perfiles = false;
            //EXP USUARIOS ACCESOS
            AccesosModuloMantenimiento.ExpUsuariosAccesos = false;
            //EXPLORADORES
            AccesosModuloMantenimiento.Exploradores = false;
            //CONSULTAS WEB SRI
            AccesosModuloMantenimiento.ConsultasSRI = false;
            //PRE INGRESOS
            AccesosModuloMantenimiento.PreIngresos = false;
        }
    }
}
