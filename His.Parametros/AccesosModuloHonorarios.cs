using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public class AccesosModuloHonorarios
    {
        #region Variables       

        //archivo
        private static bool archivo = false;
        private static bool medicos = false;
        private static bool especialidades = false;
        private static bool vendedores = false;
        private static bool tipo_documento = false;
        private static bool comision_clinica_referido = false;
        private static bool tipo_retencion = false;
        private static bool tipo_honorario = false;
        private static bool salir = false;

        //procesos_diarios
        private static bool proceso_diario = false;
        private static bool ingreso_facturas = false;
        private static bool honorarios_por_medico = false;
        private static bool ingreso_honorario = false;
        private static bool facturas_anulacion = false;
        private static bool asiento_honorario = false;

        //liquidacion_honorario
        private static bool liquidacionHonorario = false;
        private static bool asignar_facturas_liquidaciones = false;
        private static bool explorador_liquidaciones = false;
        private static bool liquidacion = false;

        //explorador
        private static bool explorador = false;
        private static bool expl_general = false;
        private static bool expl_medicos = false;

        //reportes
        private static bool reportes = false;
        private static bool rep_medicos = false;
        private static bool rep_notas = false;
        private static bool rep_retenciones = false;
        private static bool rep_contables = false;
        private static bool rep_comisiones = false;

        //balances
        private static bool honorarios_pend_pago = false;
        private static bool honorarios_pend_cancelar = false;
        private static bool honorarios_cancelados = false;
        private static bool balance_gerencial = false;

        ////estadisticas
        //    private static bool honorarios_por_medico=false;
        //    private static bool honorarios_por_especialidad=false;

        ////transferencias
        //    private static bool a_contabilidad=false;
        //    private static bool migracion_sistema_actual=false;
        //    private static bool t_medicos=false;
        //    private static bool t_pacientes=false;
        //    private static bool actualizacion_tarifarios = false;


        #endregion

        #region Metodos get y set

        public static bool Medicos
        {
            get
            {
                return medicos;
            }
            set
            {
                medicos = value;
            }
        }

        public static bool Especialidades
        {
            get
            {
                return especialidades;
            }
            set
            {
                especialidades = value;
            }
        }

        public static bool Tipo_documento
        {
            get
            {
                return tipo_documento;
            }
            set
            {
                tipo_documento = value;
            }
        }

        public static bool ComisionesClinicaReferido
        {
            get
            {
                return comision_clinica_referido;
            }
            set
            {
                comision_clinica_referido = value;
            }
        }

        public static bool TipoRetencion
        {
            get
            {
                return tipo_retencion;
            }
            set
            {
                tipo_retencion = value;
            }

        }

        public static bool TipoHonorario
        {
            get
            {
                return tipo_honorario;
            }
            set
            {
                tipo_honorario = value;
            }

        }

        public static bool IngresoFacturas
        {
            get
            {
                return ingreso_facturas;
            }
            set
            {
                ingreso_facturas = value;
            }

        }

        public static bool HonorariosPorMedico
        {
            get
            {
                return honorarios_por_medico;
            }
            set
            {
                honorarios_por_medico = value;
            }
        }

        public static bool Explorador
        {
            get
            {
                return explorador;
            }
            set
            {
                explorador = value;
            }
        }

        public static bool Archivo
        {
            get
            {
                return archivo;
            }
            set
            {
                archivo = value;
            }
        }

        public static bool Salir
        {
            get
            {
                return salir;
            }
            set
            {
                salir = value;
            }
        }

        public static bool ProcesoDiario
        {
            get
            {
                return proceso_diario;
            }
            set
            {
                proceso_diario = value;
            }
        }

        public static bool IngresoHonorarios
        {
            get
            {
                return ingreso_honorario;
            }
            set
            {
                ingreso_honorario = value;
            }
        }

        public static bool FacturasAnulacion
        {
            get
            {
                return facturas_anulacion;
            }
            set
            {
                facturas_anulacion = value;
            }
        }

        public static bool AsientoHonorario
        {
            get
            {
                return asiento_honorario;
            }
            set
            {
                asiento_honorario = value;
            }
        }

        public static bool ReporteComisiones
        {
            get
            {
                return rep_comisiones;
            }
            set
            {
                rep_comisiones = value;
            }
        }

        public static bool ReporteMedicos
        {
            get
            {
                return rep_medicos;
            }
            set
            {
                rep_medicos = value;
            }
        }

        public static bool ReporteNotas
        {
            get
            {
                return rep_notas;
            }
            set
            {
                rep_notas = value;
            }
        }

        public static bool ReporteRetenciones
        {
            get
            {
                return rep_retenciones;
            }
            set
            {
                rep_retenciones = value;
            }
        }

        public static bool ReporteContable
        {
            get
            {
                return rep_contables;
            }
            set
            {
                rep_contables = value;
            }
        }

        public static bool HonorariosPendientesPago
        {
            get
            {
                return honorarios_pend_pago;
            }
            set
            {
                honorarios_pend_pago = value;
            }
        }

        public static bool HonorariosPendientesCancelar
        {
            get
            {
                return honorarios_pend_cancelar;
            }
            set
            {
                honorarios_pend_cancelar = value;
            }
        }

        public static bool HonorariosCancelados
        {
            get
            {
                return honorarios_cancelados;
            }
            set
            {
                honorarios_cancelados = value;
            }
        }

        public static bool BalanceGerencial
        {
            get
            {
                return balance_gerencial;
            }
            set
            {
                balance_gerencial = value;
            }
        }
        public static bool Vendedores
        {
            get
            {
                return vendedores;
            }
            set
            {
                vendedores = value;
            }
        }
        public static bool LiquidacionHonorario
        {
            get
            {
                return liquidacionHonorario;
            }
            set
            {
                liquidacionHonorario = value;
            }
        }
        public static bool AsignarFacturasLiquidacion
        {
            get
            {
                return asignar_facturas_liquidaciones;
            }
            set
            {
                asignar_facturas_liquidaciones = value;
            }
        }
        public static bool ExploradorLiquidaciones
        {
            get
            {
                return explorador_liquidaciones;
            }
            set
            {
                explorador_liquidaciones = value;
            }
        }
        public static bool Liquidaciones
        {
            get
            {
                return liquidacion;
            }
            set
            {
                liquidacion = value;
            }
        }
        public static bool ReporteComision
        {
            get
            {
                return rep_comisiones;
            }
            set
            {
                rep_comisiones = value;
            }
        }
        public static bool ExploradorGeneral
        {
            get
            {
                return expl_general;
            }
            set
            {
                expl_general = value;
            }
        }
        public static bool ExploradorMedicos
        {
            get
            {
                return expl_medicos;
            }
            set
            {
                expl_medicos = value;
            }
        }
        public static bool Reporte
        {
            get
            {
                return reportes;
            }
            set
            {
                reportes = value;
            }
        }

        #endregion
    }
}
