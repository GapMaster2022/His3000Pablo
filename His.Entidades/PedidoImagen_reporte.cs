using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class PedidoImagen_reporte
    {
        public string clinica { get; set; }
        public string COD_PARROQUIA { get; set; }
        public string COD_CANTON { get; set; }
        public string COD_PROVINCIA { get; set; }
        public string PAC_HISTORIA_CLINICA { get; set; }
        public string edad { get; set; }
        public string PAC_APELLIDO_PATERNO { get; set; }
        public string PAC_APELLIDO_MATERNO { get; set; }
        public string PAC_NOMBRE1 { get; set; }
        public string PAC_NOMBRE2 { get; set; }
        public string TIP_DESCRIPCION  { get; set; }
        public string HAB_CODIGO { get; set; }
        public DateTime FECHA_CREACION { get; set; }
       
        public string medico { get; set; }
        public string motivo { get; set; }
        public string resumen_clinico { get; set; }
        public string estado_movimiento { get; set; }
        public string estado_retirarsevendas { get; set; }
        public string estado_medicopresente { get; set; }
        public string estado_encama { get; set; }
        public string urgente { get; set; }
        public string rutina { get; set; }
        public string control { get; set; }

        public string rubros { get; set; }
        public string estudios { get; set; }

         public string id_imagenologia { get; set; }

        public string cod_medico { get; set; }
        public string tecnologo { get; set; }
        public string cod_tecnologo { get; set; }
        public string radiologo { get; set; }
        public string cod_radiologo { get; set; }
        public string path { get; set; }

    }
}
