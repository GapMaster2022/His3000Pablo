using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegConsultaExterna
    {
        public static DataTable RecuperaPaciente(Int64 Ate_Codigo)
        {
            return new DatConsultaExterna().RecuperaPaciente(Ate_Codigo);
        }

        public static DataTable RecuperaEspecialidades()
        {
            return new DatConsultaExterna().RecuperaEspecialidades();
        }
        
        public static DataTable RecuperaConsultorios()
        {
            return new DatConsultaExterna().RecuperaConsultorios();
        }

        public static DataTable RecuperaHorario(string tiempo, DateTime fechaCita, string consultorio)
        {
            return new DatConsultaExterna().RecuperaHorario(tiempo, fechaCita, consultorio);
        }

        public static DataTable BuscaPaciente(string cedula)
        {
            return new DatConsultaExterna().BuscaPaciente(cedula);
        }

        public static bool GuardaAgendamientoPaciente(string txtIdentificacion, string txtNombres, string txtApellidos, string txtEmail, string txtTelefono, string txtCelular, string txtDireccion, DateTime dtpFechaCita, string cmbEspecialidades, string lblMedico, string lblMailMed, string cmbConsultorios, string cmbHora, string txtMotivo, string txtNotas, string medicoCelular, Int64 med_codigo)
        {
            return new DatConsultaExterna().GuardaAgendamientoPaciente(txtIdentificacion, txtNombres, txtApellidos, txtEmail, txtTelefono, txtCelular, txtDireccion, dtpFechaCita, cmbEspecialidades, lblMedico, lblMailMed, cmbConsultorios, cmbHora, txtMotivo, txtNotas, medicoCelular, med_codigo);
        }

        public static DataTable RecuperaNumAgenda()
        {
            return new DatConsultaExterna().RecuperaNumAgenda();
        }

        public static bool GuardaTriajeSignosVitales(string lblHistoria, Int64 lblAtencion, int nourgente, int urgente, int critico, int muerto, int alcohol, int drogas, int otros, string txtOtrasActual, string txtObserEnfer, decimal txt_PresionA1, decimal txt_PresionA2, decimal txt_FCardiaca, decimal txt_FResp, decimal txt_TBucal, decimal txt_TAxilar, decimal txt_SaturaO, decimal txt_PesoKG, decimal txt_Talla, decimal txtIMCorporal, decimal txt_PerimetroC, decimal txt_Glicemia, decimal txt_TotalG, int cmb_Motora, int cmb_Verbal, int cmb_Ocular, int txt_DiamPDV, string cmb_ReacPDValor, int txt_DiamPIV, string cmb_ReacPIValor, int txt_Gesta, int txt_Partos, int txt_Abortos, int txt_Cesareas, DateTime dtp_ultimaMenst1, decimal txt_SemanaG, int movFetal, int txt_FrecCF, int memRotas, string txt_Tiempo, int txt_AltU, int txt_Presentacion, int txt_Dilatacion, int txt_Borramiento, string txt_Plano, int pelvis, int sangrado, string txt_Contracciones, int urgente2, decimal perimetro, decimal hemoglobina, decimal glucosa, Int32 id_usuario)
        {
            return new DatConsultaExterna().GuardaTriajeSignosVitales(lblHistoria, lblAtencion, nourgente, urgente, critico, muerto, alcohol, drogas, otros, txtOtrasActual, txtObserEnfer, txt_PresionA1, txt_PresionA2, txt_FCardiaca, txt_FResp, txt_TBucal, txt_TAxilar, txt_SaturaO, txt_PesoKG, txt_Talla, txtIMCorporal, txt_PerimetroC, txt_Glicemia, txt_TotalG, cmb_Motora, cmb_Verbal, cmb_Ocular, txt_DiamPDV, cmb_ReacPDValor, txt_DiamPIV, cmb_ReacPIValor, txt_Gesta, txt_Partos, txt_Abortos, txt_Cesareas, dtp_ultimaMenst1, txt_SemanaG, movFetal, txt_FrecCF, memRotas, txt_Tiempo, txt_AltU, txt_Presentacion, txt_Dilatacion, txt_Borramiento, txt_Plano, pelvis, sangrado, txt_Contracciones, urgente2, perimetro, hemoglobina, glucosa, id_usuario);
        }

        public static bool EditarTriajeSignosVitales(string lblHistoria, Int64 lblAtencion, int nourgente, int urgente, int critico, int muerto, int alcohol, int drogas, int otros, string txtOtrasActual, string txtObserEnfer, decimal txt_PresionA1, decimal txt_PresionA2, decimal txt_FCardiaca, decimal txt_FResp, decimal txt_TBucal, decimal txt_TAxilar, decimal txt_SaturaO, decimal txt_PesoKG, decimal txt_Talla, decimal txtIMCorporal, decimal txt_PerimetroC, decimal txt_Glicemia, decimal txt_TotalG, int cmb_Motora, int cmb_Verbal, int cmb_Ocular, int txt_DiamPDV, string cmb_ReacPDValor, int txt_DiamPIV, string cmb_ReacPIValor, int txt_Gesta, int txt_Partos, int txt_Abortos, int txt_Cesareas, DateTime dtp_ultimaMenst1, decimal txt_SemanaG, int movFetal, int txt_FrecCF, int memRotas, string txt_Tiempo, int txt_AltU, int txt_Presentacion, int txt_Dilatacion, int txt_Borramiento, string txt_Plano, int pelvis, int sangrado, string txt_Contracciones, int urgente2,decimal perimetro,decimal hemoglobina,decimal glucosa)
        {
            return new DatConsultaExterna().EditarTriajeSignosVitales(lblHistoria, lblAtencion, nourgente, urgente, critico, muerto, alcohol, drogas, otros, txtOtrasActual, txtObserEnfer, txt_PresionA1, txt_PresionA2, txt_FCardiaca, txt_FResp, txt_TBucal, txt_TAxilar, txt_SaturaO, txt_PesoKG, txt_Talla, txtIMCorporal, txt_PerimetroC, txt_Glicemia, txt_TotalG, cmb_Motora, cmb_Verbal, cmb_Ocular, txt_DiamPDV, cmb_ReacPDValor, txt_DiamPIV, cmb_ReacPIValor, txt_Gesta, txt_Partos, txt_Abortos, txt_Cesareas, dtp_ultimaMenst1, txt_SemanaG, movFetal, txt_FrecCF, memRotas, txt_Tiempo, txt_AltU, txt_Presentacion, txt_Dilatacion, txt_Borramiento, txt_Plano, pelvis, sangrado, txt_Contracciones, urgente2,perimetro,hemoglobina,glucosa);
        }
        public static DataTable RecuperaTriaje(Int64 lblAteCodigo)
        {
            return new DatConsultaExterna().RecuperaTriaje(lblAteCodigo);
        }

        public static DataTable RecuperaSignos(Int64 lblAteCodigo)
        {
            return new DatConsultaExterna().RecuperaSignos(lblAteCodigo);
        }

        public static DataTable RecuperaObstetrica(Int64 lblAteCodigo)
        {
            return new DatConsultaExterna().RecuperaObstetrica(lblAteCodigo);
        }

        public static void GuardaDatos002(Form002MSP datos, SIGNOSVITALES_CONSULTAEXTERNA sigconsulta)
        {
            new DatConsultaExterna().GuardaDatos002(datos, sigconsulta);
        }
        public static bool EditarForm002(Form002MSP datos, Int64 id, SIGNOSVITALES_CONSULTAEXTERNA sigconsulta)
        {
            return new DatConsultaExterna().EditarDatos002(datos, id, sigconsulta);
        }
        public static DataTable ExistePaciente(Int64 atecodigo)
        {
            return new DatConsultaExterna().PacienteExiste(atecodigo);
        }
        public static DataTable PacienteConsultaExterna(Int64 ateCodigo)
        {
            return new DatConsultaExterna().PacientesConsultaExterna(ateCodigo);
        }
        public static Form002MSP DatosPaciente(int id)
        {
            return new DatConsultaExterna().PacienteConsultaExterna(id);
        }
        public static Int64 RecuperarId()
        {
            return new DatConsultaExterna().RecuperarId();
        }
        public static void GuardarPrescripcion(Int64 id_form002, int id_usuario, string usuario, string indicacion, string farmaco, DateTime fecha_admin, bool administrado)
        {
            new DatConsultaExterna().GuardarPrescripciones(id_form002, id_usuario, usuario, indicacion, farmaco, fecha_admin, administrado);
        }
        public static DataTable getConsultaExterna(int atecodigo)
        {
            return new DatConsultaExterna().getConsultasExternas(atecodigo);
        }
        public static Form002MSP PacienteExisteCxE(string ate_codigo)
        {
            return new DatConsultaExterna().PacienteExisteCxE(ate_codigo);
        }
        public static SIGNOSVITALES_CONSULTAEXTERNA signoscitalesCex(Int64 ate_codigo)
        {
            return new DatConsultaExterna().signoscitalesCex(ate_codigo);
        }
        public static bool PacienteCerradaCxE(Int64 ate_codigo)
        {
            return new DatConsultaExterna().PacienteCerradaCxE(ate_codigo);

        }
        public static bool CerrarCxe(string formulario, Int64 ate_codigo)
        {
            return new DatConsultaExterna().CerrarCxE(formulario, ate_codigo);
        }
        public static bool AbrirCxE(Int64 ate_codigo)
        {
            return new DatConsultaExterna().AbrirCxE(ate_codigo);
        }
        public static FORMULARIOS_MSP_CERRADOS ValidaCerrado(Int64 ate_codigo)
        {
            return new DatConsultaExterna().ValidarCerrados(ate_codigo);
        }
        public static DataTable ViewAgendamiento(DateTime desde, DateTime hasta)
        {
            return new DatConsultaExterna().ViewAgendamiento(desde, hasta);
        }
        public static List<HORARIO_ATENCION> HorariosDisponibles(string tiempo, string consultorio, DateTime fechaCita, string medico, string cedula)
        {
            return new DatConsultaExterna().Horarios(tiempo, consultorio, fechaCita, medico, cedula);
        }
        public static bool EliminarCita(Int64 codigo)
        {
            return new DatConsultaExterna().eliminarCita(codigo);
        }
        public static List<DtoAgendados> reagendar(string cedula)
        {
            return new DatConsultaExterna().Reagendar(cedula);
        }
        public static AGENDAMIENTO recuperaAgendamiento(Int64 codigo)
        {
            return new DatConsultaExterna().RecuperaAgendamiento(codigo);
        }
        public static AGENDA_PACIENTE recuperarPacienteAgendado(string identificacion)
        {
            return new DatConsultaExterna().recuperarPacienteAgendado(identificacion);
        }
        public static HABITACIONES recuperarConsultorio(string nombre)
        {
            return new DatConsultaExterna().recuperarConsultorio(nombre);
        }
        public static HORARIO_ATENCION recuperarHorarioPorNombre(string horario)
        {
            return new DatConsultaExterna().recuperarHorarioID(horario);
        }
        public static MEDICOS recuperarMedico(string medico)
        {
            return new DatConsultaExterna().recuperarMedico(medico);
        }
        public SIGNOSVITALES_CONSULTAEXTERNA signosCEX(Int64 ate_codigo)
        {
            return new DatConsultaExterna().signosCEX(ate_codigo);
        }
    }
}
