using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace His.Parametros
{
    public static class AdmisionParametros
    {
        //Estados de habitaciones
        static Int16 estadoHabitacionOcupado=1;
        static Int16 estadoHabitacionAlta = 2;
        static Int16 estadoHabitacionCancelada = 3;
        static Int16 estadoHabitacionDesocupada = 4;
        static Int16 estadoHabotacionDisponible = 5;
        static Int16 estadoHabitacionEnLimpieza = 6;
        //Codigos de los Formularios de Historias Clinicas
        static Int16 hcAdmisionAltaEgreso =1;
        static Int16 hcConsultaExternaAnamnesisExamenFisico = 2;
        static Int16 hcAnamnesisExamenFisico = 3;
        static Int16 hcEvolucionPrescripciones = 4;
        static Int16 hcEpicrisis = 5;
        static Int16 hcInterconsultaSolicitudInforme = 6;
        static Int16 hcEmergencia = 7;
        static Int16 hcLaboratorioClinicoSolicitud = 8;
        static Int16 hcImagenologia = 9;
        static Int16 hcHispatologia = 10;
        static Int16 hcSignosVitales = 11;
        static Int16 hcAdministracionMedicamentos = 12;
        static Int16 hcAutorizacionesExoneraciones = 13;
        static Int16 hcOdontologia = 14;
        static Int16 hcTrabajoSocialEvaluacion = 15;
        static Int16 hcReferenciaContrarreferencia = 16;
        static Int16 hcConcentradoDeLaboratorio = 17;
        static Int16 hcConcentradoDeExamenesEspeciales = 18;
        static Int16 hcatencionPrehospitalaria = 19;

        //codigo del pais
        private static Int16 codigoEcuador = 234;
        private static Int16 codigoQuito = 1;
        private static string defPais = "57";
        private static string defProvincia = "17";
        private static string defCanton = "1701";
        private static Int16 codEtniaDefault = 3;
        private static Int16 codEstadoCivilDefault = 1;
        private static Int16 codTipoPagoDefault = 1; 
        //parametros de pre admision
        private static Int16 codigoPreHabitacion = 9999;
        private static Int16 codigoAmbHabitacion = 148;
        private static Int16 numHoras = 24;
        private static Int16 codigoTipoTraEmerg = 4;

        //parametros de convenio IESS
        private static string codigoConvenioIess = "21";

        private static int codigoAnexoDependencia = 1;
        private static int codigoAnexoTipoSeguro = 2;
        private static int codigoAnexoProdecimiento = 3;
        private static int codigoAnexoNProcedimeinto = 4;
        private static int codigoAnexoParentesco = 5;
        private static int codigoAnexoDerivacion = 6;
        private static int codigoAnexoContingencia = 7;
        private static int codigoAnexoTipoExamen = 246;
        private static int codigoAnexoIdentificaciones = 276;


        public static Int16 getEstadoHabitacionOcupado()
        {
            return estadoHabitacionOcupado;
        }
        public static Int16 getEstadoHabitacionAlta()
        {
            return estadoHabitacionAlta; 
        }
        public static Int16 getEstadoHabitacionCancelada()
        {
            return estadoHabitacionCancelada; 
        }
        public static Int16 getEstadoHabitacionDesocupada()
        {
            return estadoHabitacionDesocupada;
        }
        public static Int16 getEstadoHabitacionDisponible()
        {
            return estadoHabotacionDisponible;
        }
        public static Int16 getEstadoHabitacionEnLimpieza()
        {
            return estadoHabitacionEnLimpieza;
        }

        public static Int16 getHcAdmisionAltaEgreso() 
        { 
            return hcAdmisionAltaEgreso; 
        }
        public static Int16 getHcConsultaExternaAnamnesisExamenFisico() 
        {
            return hcConsultaExternaAnamnesisExamenFisico; 
        }
        public static Int16 getHcAnamnesisExamenFisico() 
        { 
            return hcAnamnesisExamenFisico; 
        }
        public static Int16 getHcEvolucionPrescripciones() 
        { 
            return hcEvolucionPrescripciones; 
        }
        public static Int16 getHcEpicrisis() 
        { 
            return hcEpicrisis; 
        }
        public static Int16 getHcInterconsultaSolicitudInforme() 
        { 
            return hcInterconsultaSolicitudInforme; 
        }
        public static Int16 getHcEmergencia() 
        { 
            return hcEmergencia; 
        }
        public static Int16 getHcLaboratorioClinicoSolicitud() 
        { 
            return hcLaboratorioClinicoSolicitud; 
        }
        public static Int16 getHcImagenologia() 
        { 
            return hcImagenologia; 
        }
        public static Int16 getHcHispatologia() 
        { 
            return hcHispatologia; 
        }
        public static Int16 getHcSignosVitales() 
        { 
            return hcSignosVitales; 
        }
        public static Int16 getHcAdministracionMedicamentos() 
        { 
            return hcAdministracionMedicamentos; 
        }
        public static Int16 getHcAutorizacionesExoneraciones() 
        { 
            return hcAutorizacionesExoneraciones; 
        }
        public static Int16 getHcOdontologia() 
        { 
            return hcOdontologia; 
        }
        public static Int16 getHcTrabajoSocialEvaluacion() 
        { 
            return hcTrabajoSocialEvaluacion; 
        }
        public static Int16 getHcReferenciaContrarreferencia() 
        { 
            return hcReferenciaContrarreferencia; 
        }
        public static Int16 getHcConcentradoDeLaboratorio() 
        { 
            return hcConcentradoDeLaboratorio; 
        }
        public static Int16 getHcConcentradoDeExamenesEspeciales() 
        { 
            return hcConcentradoDeExamenesEspeciales; 
        }
        public static Int16 getHcatencionPrehospitalaria() 
        { 
            return hcatencionPrehospitalaria; 
        }
        
        public static Int16 CodigoEcuador
        {
            get { return codigoEcuador; }
            set { codigoEcuador = value; }
        }
        public static Int16 CodigoQuito
        {
            get { return codigoQuito; }
            set { codigoQuito = value; }
        }
        public static string DefPais
        {
            get { return defPais; }
            set { defPais = value; }
        }
        public static string DefProvincia
        {
            get { return defProvincia; }
            set { defProvincia = value; }
        }
        public static string DefCanton
        {
            get { return defCanton; }
            set { defCanton = value; }
        }
        public static Int16 CodEtniaDefault
        {
            get { return codEtniaDefault; }
            set { codEtniaDefault = value; }
        }
        public static Int16 CodEstadoCivilDefault
        {
            get { return codEstadoCivilDefault; }
            set { codEstadoCivilDefault = value; }
        }
        public static Int16 CodTipoPagoDefault
        {
            get { return codTipoPagoDefault; }
            set { codTipoPagoDefault = value; }
        }
        public static Int16 CodigoPreHabitacion
        {
            get { return codigoPreHabitacion; }
            set { codigoPreHabitacion = value; }
        }

        public static Int16 CodigoAmbHabitacion
        {
            get { return codigoAmbHabitacion; }
            set { codigoAmbHabitacion = value; }
        }

        public static Int16 NumeroHoras
        {
            get { return numHoras; }
            set { numHoras = value; }
        }

        //codigoTipoTratamientoE
        public static Int16 CodigoTipoTraEmerg
        {
            get { return codigoTipoTraEmerg; }
            set { codigoTipoTraEmerg = value; }
        }


        public static string CodigoConvenioIess
        {
            get { return codigoConvenioIess; }
            set { codigoConvenioIess = value; }
        }

        public static int CodigoAnexoDependencia
        {
            get { return codigoAnexoDependencia; }
            set { codigoAnexoDependencia = value; }
        }

        public static int CodigoAnexoTipoSeguro
        {
            get { return codigoAnexoTipoSeguro; }
            set { codigoAnexoTipoSeguro = value; }
        }

        public static int CodigoAnexoProdecimiento
        {
            get { return codigoAnexoProdecimiento; }
            set { codigoAnexoProdecimiento = value; }
        }

        public static int CodigoAnexoNProcedimeinto
        {
            get { return codigoAnexoNProcedimeinto; }
            set { codigoAnexoNProcedimeinto = value; }
        }

        public static int CodigoAnexoParentesco
        {
            get { return codigoAnexoParentesco; }
            set { codigoAnexoParentesco = value; }
        }

        public static int CodigoAnexoDerivacion
        {
            get { return codigoAnexoDerivacion; }
            set { codigoAnexoDerivacion = value; }
        }

        public static int   CodigoAnexoContingencia
        {
            get { return codigoAnexoContingencia; }
            set { codigoAnexoContingencia = value; }
        }

        public static int CodigoAnexoTipoExamen
        {
            get { return codigoAnexoTipoExamen; }
            set { codigoAnexoTipoExamen = value; }
        }

        public static int CodigoAnexoIdentificaciones
        {
            get { return codigoAnexoIdentificaciones; }
            set { codigoAnexoIdentificaciones = value; }
        }
    }
}
 