using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.Reportes;

namespace His.Negocio
{
    public class NegAtenciones
    {

        public static bool RPIS(int cod)
        {
            return new DatAtenciones().RPIS(cod);
        }
        public static string TipoAtencion(int codigo)
        {
            return new DatAtenciones().tipo_atencion(codigo);
        }
        public static DataTable TiposDiscapacidades()
        {
            return new DatAtenciones().tipos_discapacidades();
        }
        public static string TipoDiscapacidad(int codigo)
        {
            return new DatAtenciones().tipo_discapacidad(codigo);
        }
        public static void atencionDA_save(DtoAtencionDatosAdicionales pda)
        {
            new DatAtenciones().atencionDA_save(pda);
        }
        public static DtoAtencionDatosAdicionales atencionDA_find(int pda)
        {
            return new DatAtenciones().atencionDA_find(pda);
        }
        public static List<DtoAtenciones> RecuperaAtencionesFormulario()
        {
            return new DatAtenciones().RecuperaAtencionesFormulario();
        }
        public static List<DtoAtenciones> RecuperaAtenciones(int codigoCaja, DateTime fecha, Int16 codUsuario)
        {
            return new DatAtenciones().RecuperaAtenciones(codigoCaja, fecha, codUsuario);
        }
        public static string RecuperaAseguradoraAtencion(Int64 ateCodigo)
        {
            return new DatAtenciones().RecuperaAseguradoraAtencion(ateCodigo);
        }
        public static List<DtoAtenciones> RecuperaAtencionesActivas(string buscar, int criterio, int cantidad)
        {
            return new DatAtenciones().RecuperaAtencionesActivas(buscar, criterio, cantidad);
        }
        public static void CrearAtencion(ATENCIONES atencion)
        {
            new DatAtenciones().CrearAtencion(atencion);
        }

        public static DataTable CrearAtencionSP(ATENCIONES atencion, int CodigoDatosAdicionales, Boolean Nuevo, PACIENTES_DATOS_ADICIONALES PDA)
        {
            return new DatAtenciones().CrearAtencionSP(atencion, CodigoDatosAdicionales, Nuevo,  PDA);
        }

        public static void GrabarAtencion(ATENCIONES atencionModificado, ATENCIONES atencionOriginal)
        {
            new DatAtenciones().GrabarAtencion(atencionModificado, atencionOriginal);
        }
        public static void EliminarAtencion(ATENCIONES atencion)
        {
            new DatAtenciones().EliminarAtencion(atencion);
        }
        public static List<ATENCIONES> listaAtenciones()
        {
            return new DatAtenciones().listaAtenciones();
        }
        public static void actualizarAtencion(ATENCIONES atencionModificada)
        {
            try
            {
                new DatAtenciones().ActualizarAtencion(atencionModificada);
            }
            catch (Exception err) { throw err; }
        }
        public static bool ActualizarUAtencion(ATENCIONES obj)
        {
            return new DatAtenciones().ActualizarUAtencion(obj);
        }
        public static bool AutidoriaTipoIngreso(AUDITORIA_TIPO_INGRESO obj, Int16 tipoIngreso)
        {
            return new DatAtenciones().AutidoriaTipoIngreso(obj, tipoIngreso);
        }
        public static bool GuardaAtencionSubsecuente(ATENCIONES_SUBSECUENTES obj)
        {
            return new DatAtenciones().GuardaAtencionSubsecuente(obj);
        }
        public static bool EditarAtencionSubsecuente(ATENCIONES_SUBSECUENTES obj)
        {
            return new DatAtenciones().EditarAtencionSubsecuente(obj);
        }
        public static ATENCIONES RecuperarAtencionID(Int64 codigo)
        {
            return new DatAtenciones().RecuperarAtencionID(codigo);
        }
        public static DataTable RecuperaReferidoDiagnostico(int codigo)
        {
            return new DatAtenciones().RecuperaReferidoDiagnostico(codigo);
        }

        public string EstadoCuenta(string ate_codigo)
        {
            DatAtenciones atenciones = new DatAtenciones();
            string estado = atenciones.Estado_Cuenta(Convert.ToInt64(ate_codigo));
            return estado;
        }
        public static DataTable RecuperaFechaNacimiento(string hc)
        {
            return new DatAtenciones().RecuperaFechaNacimiento(hc);
        }

        public static DataTable RecuperaEmpleadoSC(string cedula)
        {
            return new DatAtenciones().RecuperaEmpleadoSC(cedula);
        }

        public static DataTable TiposAtenciones(String list)
        {
            return new DatAtenciones().tipos_atenciones(list);
        }
        public static List<DtoProtocolos> RecuperarProtocolos(int codigo) // Recupera el listado de protocolos operatorios de un paciente / Giovanny Tapia / 20/09/2012
        {
            return new DatAtenciones().RecuperarProtocolos(codigo);
        }

        public static bool VerificaProtocolos(int Atencion, int Protocolo) // Verifica si el formulario tiene datos / Giovanny Tapia / 20/09/2012
        {
            return new DatAtenciones().VerificaProtocolos(Atencion, Protocolo);
        }

        //RecuperarAtencionIDEmerg
        public static ATENCIONES RecuperarAtencionIDEmerg(int codigo)
        {
            return new DatAtenciones().RecuperarAtencionIDEmerg(codigo);
        }
        public static List<DtoAtenciones> RecuperarAtencionesPaciente(int keyPaciente)
        {
            return new DatAtenciones().RecuperaAtencionesPaciente(keyPaciente);
        }
        public static List<DtoAtenciones> RecuperarAtencionesPaciente(int idPaciente, Int16 codigoCaja, Int16 codUsuario)
        {
            try
            {
                return new DatAtenciones().RecuperaAtencionesPaciente(idPaciente, codigoCaja, codUsuario);
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public static int UltimoCodigoAtenciones()
        {
            return new DatAtenciones().UltimoCodigoAtenciones();
        }

        public static Int32 UltimoCodigoAtenciones_sp()
        {
            return new DatAtenciones().UltimoCodigoAtenciones_sp();
        }

        public static ATENCIONES RecuperarUltimaAtencion(int keyPaciente)
        {
            return new DatAtenciones().RecuperarUltimaAtencion(keyPaciente);
        }

        public static ATENCIONES RecuperarUltimaAtencionHonorarios(Int32 ateCodigo)
        {
            return new DatAtenciones().RecuperarUltimaAtencionHonorarios(ateCodigo);
        }
        /// <summary>
        /// Metodo que recupera una atencion enviando como parametro el codigo del paciente
        /// </summary>
        /// <param name="keyPaciente">codigo del paciente</param>
        /// <returns>retorna un objeto ATENCIONES incluidos ATENCION_FORMAS_LLEGADA,MEDICOS</returns>
        public static ATENCIONES RecuperarUltimaAtencionExt(int keyPaciente)
        {
            return new DatAtenciones().RecuperarUltimaAtencionExt(keyPaciente);
        }

        public static ATENCIONES RecuperarUltimaAtencionEmergencia(int keyPaciente)
        {
            return new DatAtenciones().RecuperarUltimaAtencionEmergencia(keyPaciente);
        }
        public static void EditarAtencionAdmision(ATENCIONES atencionModificada, int aux)
        {
            new DatAtenciones().EditarAtencionAdmision(atencionModificada, aux);
        }
        public static List<ATENCIONES> listaAtencionesPaciente(Int64 codPaciente)
        {
            return new DatAtenciones().listaAtencionesPaciente(codPaciente);
        }
        public static ATENCIONES RecuperarAtencionPorNumero(string numAtencion)
        {
            return new DatAtenciones().RecuperarAtencionPorNumero(numAtencion);
        }
        public static ATENCIONES RecuepraAtencionNumeroAtencion(Int64 numAtencion)
        {
            return new DatAtenciones().RecuepraAtencionNumeroAtencion(numAtencion);
        }
        public static ATENCIONES RecuepraAtencionNumeroAtencion2(string numAtencion)
        {
            return new DatAtenciones().RecuepraAtencionNumeroAtencion2(numAtencion);
        }
        public static TIPO_INGRESO RecuperaTipoIngreso(string ateNumeroAtencion)
        {
            return new DatAtenciones().RecuperaTipoIngreso(ateNumeroAtencion);
        }
        public static DataTable RecuperaMedicosEvolucion(int evo_codigo)
        {
            return new DatAtenciones().RecuperaMedicosEvolucion(evo_codigo);
        }
        public static DataTable NumeroAtencion(Int64 pacCod)
        {
            return new DatAtenciones().NumeroAtencion(pacCod);
        }

        public static DataTable RecuperaNotasEvolucion()
        {
            return new DatAtenciones().RecuperaNotasEvolucion();
        }
        public static ATENCIONES AtencionID(Int64 codAtencion)
        {
            return new DatAtenciones().AtencionID(codAtencion);
        }

        //Recupera los datos de la atencion
        public static DataTable atencionesID(Int64 ATE_CODIGO)
        {
            return new DatAtenciones().atencionesID(ATE_CODIGO);
        }
        public static DataTable RecuperaAtencionesSubsecuentes(Int64 PAC_CODIGO)
        {
            return new DatAtenciones().RecuperaAtencionesSubsecuentes(PAC_CODIGO);
        }
        public static DataTable CodigoConvenio(Int64 CodigoAtencion)
        {
            return new DatAtenciones().CodigoConvenio(CodigoAtencion);
        }

        public static bool existeAtencion(string numControl, string numFactura)
        {
            return new DatAtenciones().existeAtencion(numControl, numFactura);
        }

        public static bool existeAtencionAdmision(string numControl)
        {
            return new DatAtenciones().existeAtencionAdmision(numControl);
        }
        public static ATENCIONES_SUBSECUENTES RecuperaAtencionSub(Int64 ATE_CODIGO)
        {
            return new DatAtenciones().RecuperaAtencionSub(ATE_CODIGO);
        }
        //public static bool RecuperarPacientes(string cedula)
        //{
        //    return new DatAtenciones().RecuperarPacientes(cedula);
        //}
        public static List<DtoAtenciones> atencionesActivas()
        {
            return new DatAtenciones().atencionesActivas();
        }
        public static int ultimoNumeroAdmision(int codPaciente)
        {
            return new DatAtenciones().ultimoNumeroAdmision(codPaciente);
        }
        public static List<DtoAtenciones> atencionesPorFacturar()
        {
            return new DatAtenciones().atencionesPorFacturar();
        }
        public static void ingresarDatosFactura(ATENCIONES atencionM)
        {
            new DatAtenciones().ingresarDatosFactura(atencionM);
        }
        public static List<ATENCIONES> listaAtencionesPacienteConPedidos(int codPaciente, int estado, string busqPedido, string desde, string hasta)
        {
            return new DatAtenciones().listaAtencionesPacienteConPedidos(codPaciente, estado, busqPedido, desde, hasta);
        }

        //public static List<ReporteEgresoHospitalario> recuperarAtencionesEgresos(int codigoPaciente)
        //{
        //    return new DatAtenciones().RecuperaAtencionesPaciente(keyPaciente);
        //}

        // Recupera medicamentos para la hoja 08

        public static DataTable ListaMedicamentos(string Filtro)
        {
            return new DatAtenciones().ListaMedicamentos(Filtro);
        }

        public static DataTable ListaMedicos(string Filtro)
        {
            return new DatAtenciones().ListaMedicos(Filtro);
        }

        public static void OtrosSeguros(Int64 CodigoAtencion, Int32 CodigoAnexo, String Descripcion, String CodigoValidacion)
        {
            new DatAtenciones().OtrosSeguros(CodigoAtencion, CodigoAnexo, Descripcion, CodigoValidacion);
        }

        public static DataTable RecuperaOtrosSeguros(Int64 CodigoAtencion)
        {
            return new DatAtenciones().RecuperaOtrosSeguros(CodigoAtencion);
        }

        public static void EliminaOtrosSeguros(Int64 CodigoAtencion)
        {
            new DatAtenciones().EliminaOtrosSeguros(CodigoAtencion);
        }

        public static void EliminaDerivado(Int64 CodigoAtencion)
        {
            new DatAtenciones().EliminaDerivado(CodigoAtencion);
        }

        public static void AgregarDerivado(Int64 CodigoAtencion, Int32 CodigoAnexoDerivacion, Int32 CodigoAnexoRed, String Ruc, String Descripcion)
        {
            new DatAtenciones().AgregarDerivado(CodigoAtencion, CodigoAnexoDerivacion, CodigoAnexoRed, Ruc, Descripcion);
        }

        public static DataTable RecuperaDerivado(Int64 CodigoAtencion)
        {
            return new DatAtenciones().RecuperaDerivado(CodigoAtencion);
        }

        public static List<CATALOGO_COSTOS> TiposPaquetes()
        {
            return new DatAtenciones().TiposPaquetes();
        }
        public static string TipoPaquete(int codigo)
        {
            return new DatAtenciones().TipoPaquete(codigo);
        }

        public static DataTable RecuperaPermisos()
        {
            return new DatAtenciones().RecuperaPermisos();
        }

        public static DataTable RecuperaParametroCertificado()
        {
            return new DatAtenciones().RecuperaParametroCertificado();
        }

        public static DataTable Atencion(Int64 ate_codigo)
        {
            return new DatAtenciones().Atencion(ate_codigo);
        }

        public static bool ValidaEmergencia(Int64 ate_codigo)
        {
            return new DatAtenciones().ValidaEmergencia(ate_codigo);
        }
        public static bool IngresaDesactivacionAtencion(Int64 atencion, string motivo)
        {
            return new DatAtenciones().IngresaDesactivacionAtencion(atencion, motivo);
        }

        public static DataTable ValidaEstatusAtencion(Int64 atencion)
        {
            return new DatAtenciones().ValidaEstatusAtencion(atencion);
        }
        public static bool ValidarAsegurador(Int16 cat_codigo)
        {
            return new DatAtenciones().ValidarAseguradora(cat_codigo);
        }

        public static Boolean actualizaEscCodigoPreadmision(int ateCodigo, int valor)
        {
            return new DatAtenciones().actualizaEscCodigoPreadmision(ateCodigo, valor);
        }
        public static int TipoEmpresa(Int64 ate_codigo)
        {
            return new DatAtenciones().TipoEmpresa(ate_codigo);
        }
        public static string PacienteFacturado(Int64 ate_codigo)
        {
            return new DatAtenciones().PacienteAltaFacturado(ate_codigo);
        }
        public static string AseguradoraTelefono(int cat_codigo)
        {
            return new DatAtenciones().SeguroTelefono(cat_codigo);
        }
        public static DataTable RegistroAdmision1(Int64 pacCodigo)
        {
            return new DatAtenciones().RegistroAdmision1(pacCodigo);
        }
        public static DataTable RegistroAdmision2(Int64 ate_codigo)
        {
            return new DatAtenciones().RegistroAdmision2(ate_codigo);
        }
        public static DataTable CambiosAdmision(Int64 pac_codigo)
        {
            return new DatAtenciones().CambiosAdmision(pac_codigo);
        }
        public static void FacturarA(string nombre, string direccion, string cedula, string telefono, string email)
        {
            new DatAtenciones().FacturaA(cedula, direccion, nombre, telefono, email);
        }
        public static DataTable CargarClienteSic(string ruccli)
        {
            return new DatAtenciones().CargarClienteSic(ruccli);
        }
        public static string PacienteDatosAdicionales(Int64 ate_codigo)
        {
            return new DatAtenciones().PacienteDatosAdicionales(ate_codigo);
        }
        public static List<ASEGURADORAS_EMPRESAS> seguroDefault()
        {
            return new DatAtenciones().SeguroDefault();
        }
        public static void BarridoNumeroAdmision(Int64 pac_codigo)
        {
            new DatAtenciones().BarridoNumeroAtencion(pac_codigo);
        }
        public static CATEGORIAS_CONVENIOS tipoSeguro(Int64 ate_codigo)
        {
            return new DatAtenciones().tipoSeguro(ate_codigo);
        }
        public static void CrearCAMBIO_ESTADO_ATENCIONES(Int64 ATE_CODIGO, Int32 ESC_CODIGO, Int64 ID_USUARIO, string CEA_MODULO)
        {
            new DatAtenciones().CrearCAMBIO_ESTADO_ATENCIONES(ATE_CODIGO, ESC_CODIGO, ID_USUARIO, CEA_MODULO);
        }
        public static TIPO_INGRESO RecuperaTipoIngresoCodigoAtencion(Int64 ATE_CODIGO)
        {
            return new DatAtenciones().RecuperaTipoIngresoCodigoAtencion(ATE_CODIGO);
        }
        public static bool atencionReingreso(PACIENTES paciente)
        {
            return new DatAtenciones().atencionReingreso(paciente);
        }
        public static DataTable atencionesReingreso(Int64 PAC_CODIGO)
        {
            return new DatAtenciones().atencionesReingreso(PAC_CODIGO);
        }
        public static bool guardarReingredo(ATENCIONES_REINGRESO reIng)
        {
            return new DatAtenciones().guardarReingredo(reIng);
        }
        public static List<ATENCIONES_REINGRESO> atencionReIngreso(Int64 ATE_CODIGO)
        {
            return new DatAtenciones().atencionReIngreso(ATE_CODIGO);
        }
        public static bool deshabilitaReIngreso(Int64 ATE_CODIGO, Int64 id_usuario)
        {
            return new DatAtenciones().deshabilitaReIngreso(ATE_CODIGO, id_usuario);
        }
        public static ATENCIONES_REINGRESO buscaReIngresoXate_codigo(Int64 ate_codigo)
        {
            return new DatAtenciones().buscaReIngresoXate_codigo(ate_codigo);
        }
    }
}
