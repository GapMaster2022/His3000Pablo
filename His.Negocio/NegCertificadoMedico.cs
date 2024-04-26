using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using His.Datos;
using His.Entidades;
namespace His.Negocio
{
    public class NegCertificadoMedico
    {
        DatCertificadoMedico Certificado = new DatCertificadoMedico();
        public DataTable BuscarPaciente(string ate_codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Certificado.BuscarPaciente(Convert.ToInt64(ate_codigo));
            return Tabla;
        }
        public DataTable Medico_Pacientes()
        {
            DataTable Tabla = new DataTable();
            Tabla = Certificado.Medico_Paciente();
            return Tabla;
        }
        public static DataTable PacientesMushugñan()
        {
            return new DatCertificadoMedico().Medico_PacienteMushugñan();
        }
        public static DataTable Medico_PacienteBrigada()
        {
            return new DatCertificadoMedico().Medico_PacienteBrigada();
        }
        public static DataTable Medico_PacienteTodos()
        {
            return new DatCertificadoMedico().Medico_PacienteTodos();
        }
        public void ActualizaCertificado(Int64 ate_codigo, string direccion, string telefono)
        {
            Certificado.ActualizaCertificado(ate_codigo, direccion, telefono);
        }
        public void ActualizaCertificadoIESS(int ate_codigo, string direccion, string telefono)
        {
            //Certificado.ActualizaCertificadoIESS(ate_codigo, direccion, telefono);
        }
        public void InsertarCertificado(Int32 cer_codigo, string ate_codigo, string med_codigo, string observacion, string reposo,
            string actividad, string contingencia, string tratamiento, string procedimiento, int ingreso, DateTime fechaCirugia, string empresa)
        {
            Certificado.InsertarCertificado(cer_codigo, Convert.ToInt32(ate_codigo), Convert.ToInt32(med_codigo), observacion, Convert.ToInt32(reposo),
                actividad, contingencia, tratamiento, procedimiento, ingreso, fechaCirugia, empresa);
        }
        public void InsertarCertificadoIESS(Int32 CMI_CODIGO, string ATE_CODIGO, string MED_CODIGO, string CMI_INSTITUCION_LABORAL, DateTime CMI_FECHA, string CMI_DIAS_REPOSO, string CMI_ACTIVIDAD_LABORAL,
            string CMI_CONTINGENCIA, string CMI_CONFIRMADO, DateTime CMI_FECHA_CIRUGIA, string CMI_DESCRIPCION_SINTOMAS, string CMI_NOTA, Int32 CMI_TIPO_INGRESO, bool CMI_ESTADO,
            bool CMI_ENFERMEDAD, bool CMI_SINTOMAS, bool CMI_REPOSO, bool CMI_AISLAMIENTO, bool CMI_TELETRABAJO, string DIRECCION_PACIENTE, string TELEFONO_PACIENTE, string CMI_ANULADO, DateTime CMI_FECHA_ALTA,
            string CMI_PROCEDIMIENTO, string CMI_TRATAMIENTO)
        {
            Certificado.InsertarCertificadoIESS(CMI_CODIGO, Convert.ToInt32(ATE_CODIGO), Convert.ToInt32(MED_CODIGO), CMI_INSTITUCION_LABORAL, CMI_FECHA, CMI_DIAS_REPOSO, CMI_ACTIVIDAD_LABORAL,
             CMI_CONTINGENCIA, CMI_CONFIRMADO, CMI_FECHA_CIRUGIA, CMI_DESCRIPCION_SINTOMAS, CMI_NOTA, CMI_TIPO_INGRESO, CMI_ESTADO,
             CMI_ENFERMEDAD, CMI_SINTOMAS, CMI_REPOSO, CMI_AISLAMIENTO, CMI_TELETRABAJO, DIRECCION_PACIENTE, TELEFONO_PACIENTE, CMI_ANULADO, CMI_FECHA_ALTA, CMI_PROCEDIMIENTO, CMI_TRATAMIENTO);
        }
        public void InsertarCertificadoDetalle(string cie_codigo)
        {
            Certificado.InsertarCertificadoDetalle(cie_codigo);
        }
        public void InsertarCertificadoDetalleIESS(string cie_codigo)
        {
            Certificado.InsertarCertificadoDetalleIESS(cie_codigo);
        }
        public DataTable CargarDatosCertificado(string ate_codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Certificado.CargarDatosCertificado(Convert.ToInt32(ate_codigo));
            return Tabla;
        }
        public DataTable CargarDatosCertificadoN(string ate_codigo, string cer_codigo = "")
        {
            DataTable Tabla = new DataTable();
            Tabla = Certificado.CargarDatosCertificadoN(Convert.ToInt32(ate_codigo), Convert.ToInt32(cer_codigo));
            return Tabla;
        }
        public DataTable CargarDatosCertificadoIESS(string ate_codigo, string CMI_CODIGO = "")
        {
            DataTable Tabla = new DataTable();
            Tabla = Certificado.CargarDatosCertificadoIESS(Convert.ToInt32(ate_codigo), Convert.ToInt32(CMI_CODIGO));
            return Tabla;
        }
        public DataTable CargarDatosCertificado_Detalle(Int64 cer_Codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Certificado.CargarDatosCertificado_Detalle(cer_Codigo);
            return Tabla;
        }
        public DataTable CargarDatosCertificadoIESS_Detalle(Int64 cer_Codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Certificado.CargarDatosCertificadoIESS_Detalle(cer_Codigo);
            return Tabla;
        }
        public Int32 ContadorCertificado()
        {
            return Certificado.ContadorCertificado();
        }
        public Int32 ContadorCertificadoEspecial()
        {
            return Certificado.ContadorCertificadoEspecial();
        }
        public string path()
        {
            string path = Certificado.PathImagen();
            return path;
        }

        public string pathPre()
        {
            string path = Certificado.PathImagenPre();
            return path;
        }
        public DataTable CargarCie10Hosp(string ate_codigo)
        {
            DataTable Tabla = Certificado.CargarCie10Hosp(Convert.ToInt64(ate_codigo));
            return Tabla;
        }
        public DataTable CargarCie10Emerg(string ate_codigo)
        {
            DataTable Tabla = Certificado.CargarCie10Emerg(Convert.ToInt64(ate_codigo));
            return Tabla;
        }
        public DataTable CargarCie10Consulta(string ate_codigo)
        {
            DataTable Tabla = Certificado.CargarCie10Consulta(Convert.ToInt64(ate_codigo));
            return Tabla;
        }
        public DataTable CargarHoras()
        {
            DataTable Tabla = Certificado.CargarHoras();
            return Tabla;
        }
        public DataTable CargarDias()
        {
            DataTable Tabla = Certificado.CargarDias();
            return Tabla;
        }
        public static DataTable CertificadosMedicos(DateTime fechainicio, DateTime fechafin, bool estado)
        {
            return new DatCertificadoMedico().CertificadosMedicos(fechainicio, fechafin, estado);
        }

        public static DataTable CertificadoXmedicos(DateTime fechainicio, DateTime fechafin, int codMedico, bool estado)
        {
            return new DatCertificadoMedico().CertificadoXmedicos(fechainicio, fechafin, codMedico, estado);
        }

        public static DataTable TiposContingencia()
        {
            return new DatCertificadoMedico().CargarTipoContingencia();
        }
        public static DataTable ReimpresionCertificado(int cer_codigo)
        {
            return new DatCertificadoMedico().ReimpresionCertificado(cer_codigo);
        }

        public static DataTable ReimpresionCertificadoIESS(int cer_codigo)
        {
            return new DatCertificadoMedico().ReimpresionCertificadoIESS(cer_codigo);
        }
        public static bool InhabilitaCertificado(string motivo, string medico, Int32 codigoCertificado)
        {
            return new DatCertificadoMedico().InhabilitaCertificado(motivo, medico, codigoCertificado);
        }
        public static bool InhabilitaCertificadoIESS(string motivo, string medico, Int32 codigoCertificado)
        {
            return new DatCertificadoMedico().InhabilitaCertificadoIESS(motivo, medico, codigoCertificado);
        }
        public static bool InhabilitaRecetamedica(RECETAS_ANULADAS receta)
        {
            return new DatCertificadoMedico().InhabilitaRecetamedica(receta);
        }
        public static DataTable VerificaEstado(Int64 ateCodigo, Int64 medCodigo)
        {
            return new DatCertificadoMedico().VerificaEstado(ateCodigo, medCodigo);
        }
        public static DataTable TIPO_INGRESO_IESS(Int32 ATE_CODIGO)
        {
            return new DatCertificadoMedico().TIPO_INGRESO_IESS(ATE_CODIGO);
        }
        public static DataTable VerificaEstadoIESS(Int64 ateCodigo)
        {
            return new DatCertificadoMedico().VerificaEstadoIESS(ateCodigo);
        }
        public static Int64 MedicoCodigo(string id)
        {
            return new DatCertificadoMedico().Med_Codigo(id);
        }

        public static Int64 IdReceta()
        {
            return new DatCertificadoMedico().idReceta();
        }
        public static Int64 IdRecetaDiagnostico()
        {
            return new DatCertificadoMedico().idRecetaDiagnostico();
        }
        public static Int64 IdRecetaMedicamento()
        {
            return new DatCertificadoMedico().idRecetaMedicamentos();
        }
        public static bool InsertReceta(RECETA_MEDICA receta, List<RECETA_DIAGNOSTICO> diagnostico, List<RECETA_MEDICAMENTOS> medicamentos)
        {
            return new DatCertificadoMedico().InsertarReceta(receta, diagnostico, medicamentos);
        }
        public static bool UpdateReceta(RECETA_MEDICA receta, List<RECETA_DIAGNOSTICO> diagnostico, List<RECETA_MEDICAMENTOS> medicamentos)
        {
            return new DatCertificadoMedico().UpdateReceta(receta, diagnostico, medicamentos);
        }
        public static List<VIA_ADMINISTRACION_MEDICAMENTO> RecuperarporNombre(string via)
        {
            return new DatCertificadoMedico().RecuperarPorNombre(via);
        }
        public static bool GuardaCertificadoPresentacion(CERTIFICADO_PRESENTACION obj)
        {
            return new DatCertificadoMedico().GuardaCertificadoPresentacion(obj);
        }
        public static DataTable ProductosBasicos(string filtro)
        {
            return new DatCertificadoMedico().ProductosBasicos(filtro);
        }
        public static DataTable CuadroBasico()
        {
            return new DatCertificadoMedico().CuadroBasico();
        }
        public static DataTable RecuperarPBasico(string codigo)
        {
            return new DatCertificadoMedico().RecuperarPBasico(codigo);
        }
        public static bool ExisteReceta(Int64 ate_codigo)
        {
            return new DatCertificadoMedico().ExisteReceta(ate_codigo);
        }

        public static bool ExisteRecetaMedico(Int64 ate_codigo, Int64 med_codigo)
        {
            return new DatCertificadoMedico().ExisteRecetaMedico(ate_codigo, med_codigo);
        }
        public static RECETA_MEDICA RecuperaReceta(Int64 ate_codigo, Int64 RM_CODIGO = 0)
        {
            return new DatCertificadoMedico().RecuperaCabecera(ate_codigo, RM_CODIGO);
        }
        public static List<RECETA_DIAGNOSTICO> RecuperaDiagnostico(Int64 rm_codigo)
        {
            return new DatCertificadoMedico().RecuperarDiagnostico(rm_codigo);
        }
        public static CERTIFICADO_PRESENTACION RecuperarCertificadoPresentacion(Int64 ate_codigo, Int64 medico)
        {
            return new DatCertificadoMedico().RecuperarCertificadoPresentacion(ate_codigo, medico);
        }
        public static CERTIFICADO_MEDICO_IESS RecuperaCertificadoIESSDuplicado(Int64 ate_codigo, Int64 medico)
        {
            return new DatCertificadoMedico().RecuperaCertificadoIESSDuplicado(ate_codigo, medico);
        }
        public static List<RECETA_MEDICAMENTOS> RecuperarMedicamentos(Int64 rm_codigo)
        {
            return new DatCertificadoMedico().RecuperaMedicamentos(rm_codigo);
        }
        public static List<VIA_ADMINISTRACION_MEDICAMENTO> ListarViaAdministrativa()
        {
            return new DatCertificadoMedico().ViaAdministracionTodo();
        }
        public static List<TIPO_CONSULTA> Consulta()
        {
            return new DatCertificadoMedico().consulta();
        }
        public static List<TIPO_CONSULTA> ConsultaHospitalaria()
        {
            return new DatCertificadoMedico().ConsultaHospitalaria();
        }
        public static List<TIPO_PRESENTACION> Presentacion()
        {
            return new DatCertificadoMedico().presentacion();
        }
        public static double UltimoPT()
        {
            return new DatCertificadoMedico().UltimoRegistroPT();
        }
        public static List<DtoPacienteReceta> ExploradorRecetas(DateTime desde, DateTime hasta)
        {
            return new DatCertificadoMedico().ExploradorReceta(desde, hasta);
        }
        public static TIPO_CONSULTA RecuperarConsulta(int codigo)
        {
            return new DatCertificadoMedico().RecuperarConsulta(codigo);
        }
        public static DataTable CertificadosPresentacion(DateTime fechainicio, DateTime fechafin, bool estado)
        {
            return new DatCertificadoMedico().CertificadosPresentacion(fechainicio, fechafin, estado);
        }
        public static DataTable CertificadoPresentacionXmedicos(DateTime fechainicio, DateTime fechafin, int codMedico, bool estado)
        {
            return new DatCertificadoMedico().CertificadoPresentacionXmedicos(fechainicio, fechafin, codMedico, estado);
        }
        public static MEDICOS DatosMedicos(Int64 _MED_CODIGO)
        {
            return new DatCertificadoMedico().DatosMedicos(_MED_CODIGO);
        }
        public static bool InhabilitaCertificadoPrecentacion(string motivo, string medico, Int32 codigoCertificado)
        {
            return new DatCertificadoMedico().InhabilitaCertificadoPrecentacion(motivo, medico, codigoCertificado);
        }
        public static Int64 Med_CodigoCertificadoAsistencia(string id)
        {
            return new DatCertificadoMedico().Med_CodigoCertificadoAsistencia(id);
        }
    }
}
