using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeserWard.Controls;
using His.Entidades;
using His.Datos;
using System.Data;


namespace His.Negocio
{
    public class NegMedicos
    {
        public static List<DtoMedicos> MedicosdeAtenciones()
        {
            return new DatMedicos().MedicosdeAtenciones();
        }
        public static List<MEDICOS> listarMedicosAnesteciologos()
        {
            try
            {
                return new DatMedicos().listarMedicosAnestecilogos();
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public static int RecuperaMaximoMedicos()
        {
            return new DatMedicos().RecuperaMaximoMedicos();
        }
        public static List<DtoMedicos> RecuperaMedicosFormulario()
        {
            return new DatMedicos().RecuperaMedicosFormulario();
        }
        public static DtoMedicos RecuperaDtoMedicoFormulario(int codMedico)
        {
            return new DatMedicos().RecuperaDtoMedicoFormulario(codMedico);
        }
        public static void CrearMedico(MEDICOS medico)
        {
            new DatMedicos().CrearMedico(medico);
        }
        public static void NewMedico(MEDICOS medico, int esp_codigo, int tim_codigo, int tih_codigo, int ret_codigo)
        {
            new DatMedicos().NuevoMedico(medico, esp_codigo, tim_codigo, tih_codigo, ret_codigo);
        }
        public static void GrabarMedico(MEDICOS medicoModificado, MEDICOS medicoOriginal, int esp_codigo, int tim_codigo, int tih_codigo)
        {
            new DatMedicos().GrabarMedico(medicoModificado, medicoOriginal, esp_codigo, tim_codigo, tih_codigo);
        }
        public static void EliminarMedico(MEDICOS medico)
        {
            new DatMedicos().EliminarMedico(medico);
        }
        public static DataTable VistaHonorarioMedico(int med_codigo, DateTime fechaInicio, DateTime fechaFin, int for_codigo,
            int tif_codigo, string lote, string numControl)
        {
            return new DatHonorariosMedicos().VistaHonorariosMedicos(med_codigo, fechaInicio, fechaFin, for_codigo,
                tif_codigo, lote, numControl);
        }
        public static List<DtoUsuarios> ListaUsuariosNoMedicos()
        {
            return  new DatMedicos().ListaUsuariosNoMedicos();
        }

        public static List<MEDICOS> listaMedicos()
        {
            return new DatMedicos().listaMedicos();
        }

        /// <summary>
        /// lista de medicos incluido el Tipo de honorario
        /// </summary>
        /// <returns>lista de medicos</returns>
        public static List<MEDICOS> listaMedicosIncTipoHonorario()
        {
            try
            {
                return new DatMedicos().listaMedicosIncTipoHonorario(); 
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// lista de medicos incluido la especialidad medica
        /// </summary>
        /// <returns>lista de medicos</returns>
        public static List<MEDICOS> listaMedicosIncEspecialidadesMedicas()
        {
            try
            {
                return new DatMedicos().listaMedicosIncEspecialidadesMedicas();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// lista de medicos incluido el tipo medico    
        /// </summary>
        /// <returns>lista de medicos</returns>
        public static List<MEDICOS> listaMedicosIncTipoMedico()
        {
            try
            {
                return new DatMedicos().listaMedicosIncTipoMedico();
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        /// <summary>
        /// lista de medicos incluido el tipo medico    
        /// </summary>
        /// <returns>lista de medicos</returns>
        public static List<MEDICOS> listaMedicosIncTipoMedico(Int16 codTipoMedico)
        {
            try
            {
                return new DatMedicos().listaMedicosIncTipoMedico(codTipoMedico);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        ///<summary >
        ///Recupera el listado de medicos por un tipo especifico
        ///</summary>
        public static List<MEDICOS> listaMedicos(int codigo, string tipo)
        {
            return new DatMedicos().listaMedicos(codigo, tipo);    
        }
        ///<summary >
        ///Recupera el listado de medicos con honorarios por un tipo especifico
        ///</summary>
        public static List<MEDICOS> listaMedicosConHonorarios(int codigo, string tipo)
        {
            return new DatMedicos().listaMedicosConHonorarios(codigo, tipo);
        }
        //.Recupero medicos por id
        public static MEDICOS RecuperaMedicoId(int codigo)
        {
            return new DatMedicos().RecuperaMedicoId(codigo);
        }
        public static DataTable RecuperaEspecialidadMed(int codigo)
        {
            return new DatMedicos().RecuperaEspecialidadMed(codigo);
        }
        //Ingresa en Auditoria
        public static MEDICOS AuditaMedico(int codigo, int usuario, DateTime fecha)
        {
            return new DatMedicos().AuditaMedico(codigo, usuario, fecha);
        }
        
        //.Recupera la informaciòn del medico por codigo de usuario
        public static  MEDICOS RecuperaMedicoIdUsuario(int codigoUsuario)
        {
            return new DatMedicos().RecuperaMedicoIdUsuario(codigoUsuario);
        }
        ///<summary>
        ///.Recupera el Tipo de Honorario de los medicos
        ///</summary> 
        public static List<TIPO_HONORARIO> RecuperaTipoHonorario()
        {
            return new DatMedicos().RecuperarTipoHonorario(); 
        }
        ///<summary>
        ///. Recupera el Tipo de Especilidad de los medicos
        ///</summary> 
        public static List<ESPECIALIDADES_MEDICAS> RecuperaTipoEspecialidad()
        {
            return new DatMedicos().RecuperarTipoEspecialidad();  
        }
        ///<sumary>
        ///. Recupera el tipo de medico
        ///</sumary>
        public static List<TIPO_MEDICO> RecuperaTipoMedico()
        {
            return new DatMedicos().RecuperarTipoMedico();  
        }
        public static List<DtoMedicosConsulta> MedicosConsulta()
        {
            return new DatMedicos().MedicosConsulta();
        }
        public static List<MEDICOS> listaMedicosTratantes()
        {
            return new DatMedicos().listaMedicosTratantes();
        }

        /// <summary>
        /// Metodo que devuelve el listado de medicos por especialidad
        /// </summary>
        /// <param name="codigoEspecialidad">Codigo de la especialidad</param>
        /// <returns>Lista de objetos MEDICOS</returns>
        public static List<MEDICOS> listaMedicosPorEspecialidad(Int16 especialidadCodigo)
        {
            try
            {
                return new DatMedicos().listaMedicosPorEspecialidad(especialidadCodigo);  
      
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        public static MEDICOS MedicoID(int codigo)
        {
            return new DatMedicos().MedicoID(codigo);
        }

        public static DataTable MedicoIDValida(int MedCodigo)
        {
            return new DatMedicos().MedicoIDValida(MedCodigo);
        }

        /// <summary>
        /// Metodo que devuelve el listado de medicos y sus correo 
        /// </summary>
        /// <param name="codigoEspecialidad">Codigo de la especialidad</param>
        /// <returns>Lista de objetos MEDICOS</returns>
        public static  List<MEDICOS> listaCorreosMedicosPorEspecialidad(string codigoEspecialidad)
        {
            return new DatMedicos().listaCorreosMedicosPorEspecialidad(codigoEspecialidad);  
        }

        public DataTable Cie10()
        {
            DatMedicos medico = new DatMedicos();
            DataTable Tabla = medico.CargarCie10();
            return Tabla;
        }
        public DataTable Tarifario()
        {
            DatMedicos medico = new DatMedicos();
            DataTable tabla = medico.CargarTarifario();
            return tabla;
        }
        public static DataTable Personal()
        {
            return new DatMedicos().CargarPersonal();
        }
        public static DataTable CargarPacienteEmergencia()
        {
            return new DatMedicos().CargarPacienteEmergencia();
        }
        public static List<MEDICOS> medicosLista()
        {
            return new DatMedicos().medicosLista();
        }

        public static MEDICOS medicoPorAtencion(int codAtencion)
        {
            return new DatMedicos().medicoPorAtencion(codAtencion);
        }
        
        public static MEDICOS recuperarMedico(int codigoMedico)
        {
            return new DatMedicos().recuperarMedico(codigoMedico);
        }
        public static MEDICOS MedicoNombreApellido(string[] medico)
        {
            return new DatMedicos().MedicoNombreApellido(medico);
        }
        public static MEDICOS recuperarMedicoRUC(string medRuc)
        {
            return new DatMedicos().recuperarMedicoRUC(medRuc);
        }
        public static MEDICOS recuperarMedicoID_Usuario(int idUsuario)
        {
            return new DatMedicos().recuperarMedicoID_Usuario(idUsuario);
        }

        public static Int32 RecuperaMedicoHorario()
        {
            return new DatMedicos().RecuperaMedicoHorario();
        }

        public static DataTable RecuperaMedicoAtencion(string CodigoHistoria, Int32 CodigoAtencion)
        {
            return new DatMedicos().RecuperaMedicoAtencion(CodigoHistoria, CodigoAtencion);
        }

        public static DataTable getMedicosGrid()
        {
            return new DatMedicos().getMedicosGrid();
        }

        public static void saveMedicoVendedor(string codMed,string codVendedor, bool Nuevo)
        {
            new DatMedicos().saveMedicoVendedor(codMed, codVendedor, Nuevo);
        }

        public static string getMedicoVendedor(string codMed)
        {
            return new DatMedicos().getMedicoVendedor(codMed);
        }
        public static DataTable RecuperarNuevosCampos(int med_codigo)
        {
            return new DatMedicos().RecuperarNuevosCampos(med_codigo);
        }

        public static void ActualizarCampos(int med_codigo, bool aporte, int ret_codigo)
        {
            new DatMedicos().ActualizarCampos(med_codigo, aporte, ret_codigo);
        }
        public static void ActualizarRucMedico(MEDICOS med, string idUsuario)
        {
            new DatMedicos().ActualizarRucMedico(med, idUsuario);
        }
        public static string CuentaContableSic(string forpag)
        {
            return new DatMedicos().CuentaContableSic(forpag);
        }
        public static DataTable VerMedicos()
        {
            return new DatMedicos().VerMedicos();
        }
        public static List<MEDICOS> MedicosCitaMedica(int esp_codigo)
        {
            return new DatMedicos().MedicosCitaMedica(esp_codigo);
        }
        public static List<ESPECIALIDADES_MEDICAS> EspecialidadesCita()
        {
            return new DatMedicos().EspecialidadesCita();
        }
        public static bool existeMedico(string identificacion)
        {
            return new DatMedicos().existeMedico(identificacion);
        }
        public static string cuentaContable()
        {
            return new DatMedicos().cuentaContable();
        }
        public static string MED_CODIGO_MEDICO_CG(string med_ruc)
        {
            return new DatMedicos().MED_CODIGO_MEDICO_CG(med_ruc);
        }
        public static MEDICOS Medicos()
        {
            return new DatMedicos().Medicos();
        }
        public static List<MEDICOS> listMedicos()
        {
            return new DatMedicos().listaMedicos();
        }
        public static DtoMedicoCuentaContable MEDICO_CG(string codigo_c)
        {
            return new DatMedicos().MEDICO_CG(codigo_c);
        }
        public static List<DtoMedicoCuentaContable> List_MEDICO_CG()
        {
            return new DatMedicos().List_MEDICO_CG();
        }
        public static string TipoMoviemientoSic(string codcue)
        {
            return new DatMedicos().TipoMoviemientoSic(codcue);
        }
        public static List<MEDICOS> listaMedicosIncTipoMedicoXEsp(Int64 esp_codigo)
        {
            return new DatMedicos().listaMedicosIncTipoMedicoXEsp(esp_codigo);
        }
    }


    #region Implementacion Controles
    public class LinqToEntitiesResultsProvider : IIntelliboxResultsProvider
    {
        public IEnumerable<object> DoSearch(string searchTerm, int maxResults, object extraInfo)
        {
            return new DatMedicos().DoSearch(searchTerm,maxResults, extraInfo);
        }
    }
   #endregion

    
}
