using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public class AccesoModuloPedidosEspeciales
    {
        #region variables
        private static bool dietetica = false;
        private static bool pedido = false;

        private static bool gastroenterologia = false;
        private static bool gagregarProducto = false;
        private static bool gagregarProcedimiento = false;
        private static bool gpedidoPaciente = false;
        private static bool greposicionProductos = false;

        private static bool imagen = false;
        private static bool agendamiento = false;
        private static bool examenesAgendados = false;
        private static bool informe = false;
        private static bool exploradorPedidos = false;
        private static bool horarioMedico = false;

        private static bool labClinico = false;
        private static bool crearPerfiles = false;
        private static bool cexpPedidos = false;
        private static bool pacientes = false;
        private static bool examenesPerfiles = false;

        private static bool labPatologico = false;
        private static bool pexpPedidos = false;

        private static bool quirofano = false;
        private static bool qagregarProducto = false;
        private static bool qagregarProcedimiento = false;
        private static bool qpedidoPaciente = false;
        private static bool qreposicionProductos = false;
        private static bool expProcedimiento = false;
        private static bool explRubros = false;
        #endregion
        #region Metodos get y set
        public static bool Dietetica
        {
            get { return dietetica; }
            set { dietetica = value; }
        }
        public static bool Pedido
        {
            get { return pedido; }
            set { pedido = value; }
        }
        public static bool Gastroenterologia
        {
            get { return gastroenterologia; }
            set { gastroenterologia = value; }
        }
        public static bool GagergarProducto
        {
            get { return gagregarProducto; }
            set { gagregarProducto = value; }
        }
        public static bool GagregarProcedimiento
        {
            get { return gagregarProcedimiento; }
            set { gagregarProcedimiento = value; }
        }
        public static bool GpedidoPaciente
        {
            get { return gpedidoPaciente; }
            set { gpedidoPaciente = value; }
        }
        public static bool GreposicionProducto
        {
            get { return greposicionProductos; }
            set { greposicionProductos = value; }
        }
        public static bool Imagen
        {
            get { return imagen; }
            set { imagen = value; }
        }
        public static bool Agendamiento
        {
            get { return agendamiento; }
            set { agendamiento = value; }
        }
        public static bool ExamenesAgendados
        {
            get { return examenesAgendados; }
            set { examenesAgendados = value; }
        }
        public static bool Informe
        {
            get { return informe; }
            set { informe = value; }
        }
        public static bool ExplPedidos
        {
            get { return exploradorPedidos; }
            set { exploradorPedidos = value; }
        }
        public static bool HorarioMedico
        {
            get { return horarioMedico; }
            set { horarioMedico = value; }
        }
        public static bool LabClinico
        {
            get { return labClinico; }
            set { labClinico = value; }
        }
        public static bool CrearPerfiles
        {
            get { return crearPerfiles; }
            set { crearPerfiles = value; }
        }
        public static bool CexplPedidos
        {
            get { return cexpPedidos; }
            set { cexpPedidos = value; }
        }
        public static bool Pacientes
        {
            get { return pacientes; }
            set { pacientes = value; }
        }
        public static bool ExamenesPerfiles
        {
            get { return examenesPerfiles; }
            set { examenesPerfiles = value; }
        }
        public static bool LabPatologico
        {
            get { return labPatologico; }
            set { labPatologico = value; }
        }
        public static bool PexplPedidos
        {
            get { return pexpPedidos; }
            set { pexpPedidos = value; }
        }
        public static bool Quirofano
        {
            get { return quirofano; }
            set { quirofano = value; }
        }
        public static bool QagergarProducto
        {
            get { return qagregarProducto; }
            set { qagregarProducto = value; }
        }
        public static bool QagregarProcedimiento
        {
            get { return qagregarProcedimiento; }
            set { qagregarProcedimiento = value; }
        }
        public static bool QpedidoPaciente
        {
            get { return qpedidoPaciente; }
            set { qpedidoPaciente = value; }
        }
        public static bool QreposicionProducto
        {
            get { return qreposicionProductos; }
            set { qreposicionProductos = value; }
        }
        public static bool ExpProcedimiento
        {
            get { return expProcedimiento; }
            set { expProcedimiento = value; }
        }public static bool ExpRubros
        {
            get { return explRubros; }
            set { explRubros = value; }
        }
        #endregion
    }
}
