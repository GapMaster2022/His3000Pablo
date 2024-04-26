using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public static class FarmaciaPAR
    {
        private static Int16 pedidoPendiente = 1;
        private static Int16 pedidoEntregado = 2;
        private static Int16 pedidoAnulado = 3;
        private static Int16 pedidoMedicamentos = 3;
        private static Int16 pedidoExamenes = 2;

        public static Int16 PedidoPendiente
        {
            get { return pedidoPendiente; }
            set { pedidoPendiente = value; }
        }
        public static Int16 PedidoEntregado
        {
            get { return pedidoEntregado; }
            set { pedidoEntregado = value; }
        }
        public static Int16 PedidoAnulado
        {
            get { return pedidoAnulado; }
            set { pedidoAnulado = value; }
        }
        public static Int16 PedidoMedicamentos
        {
            get { return pedidoMedicamentos; }
            set { pedidoMedicamentos = value; }
        }
        public static Int16 PedidoExamenes
        {
            get { return pedidoExamenes; }
            set { pedidoExamenes = value; }
        }
    }
}
