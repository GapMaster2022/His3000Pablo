using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class PedidoImagen
    {
        public int id_imagenologia { get; set; }
        public DateTime FECHA_CREACION { get; set; }
        public DateTime FECHA_CIERRE { get; set; }
        public int ID_USUARIO { get; set; }
        public int estado { get; set; } ///por defecto 1 activo
        public int ATE_CODIGO { get; set; }
        public int PRIORIDAD { get; set; }
        public string motivo { get; set; }
        public int estado_movimiento { get; set; }
        public int estado_retirarsevendas { get; set; }
        public int estado_medicopresente { get; set; }
        public int estado_encama { get; set; }
        public string resumen_clinico { get; set; }
        public int MED_CODIGO { get; set; }
        public List<PedidoImagen_estudios> estudios { get; set; }
        public List<PedidoImagen_diagnostico> diagnosticos { get; set; }
        public int MED_RADIOLOGO { get; set; }
        public int MED_TECNOLOGO { get; set; }
        public string CONCLUCIONES { get; set; }
    }
}
