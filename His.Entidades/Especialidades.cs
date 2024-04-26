using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class Especialidades
    {
        public Int16 ESP_CODIGO { get; set; }
        public string ESP_NOMBRE { get; set; }
        public string ESP_DESCRIPCION { get; set; }
        public Int16 ESP_PADRE { get; set; }
        public Boolean ESP_ELIMINADO { get; set; }
    }
}
