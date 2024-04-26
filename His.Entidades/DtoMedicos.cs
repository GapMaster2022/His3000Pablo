using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoMedicos
    {
        public int MED_CODIGO { get; set; }
        public Int16 RET_CODIGO { get; set; }
        public Int16 ID_USUARIO { get; set; }
        public Int16 ESP_CODIGO { get; set; }
        public Int16 BAN_CODIGO { get; set; }
        public Int16 TIM_CODIGO { get; set; }
        public Int16 ESC_CODIGO { get; set; }
        public Int16 TIH_CODIGO { get; set; }
        public string MED_CODIGO_MEDICO { get; set; }

        public string MED_CODIGO_LIBRO { get; set; }
        public string MED_CODIGO_FOLIO { get; set; }
        
        public string RET_DESCRIPCION { get; set; }
        public decimal RET_PORCENTAJE { get; set; }
        public string MED_APELLIDO_PATERNO { get; set; }
        public string MED_APELLIDO_MATERNO { get; set; }
        public string MED_NOMBRE1 { get; set; }
        public string MED_NOMBRE2 { get; set; }
        public string ESP_NOMBRE { get; set; }
        public DateTime MED_FECHA { get; set; }
        public DateTime MED_FECHA_MODIFICACION { get; set; }
        public DateTime MED_FECHA_NACIMIENTO { get; set; }
        public string MED_DIRECCION { get; set; }
        public string MED_DIRECCION_CONSULTORIO { get; set; }
        public string MED_RUC { get; set; }
        public string MED_EMAIL { get; set; }
        public string MED_GENERO { get; set; }
        public string MED_NUM_CUENTA { get; set; }
        public string MED_TIPO_CUENTA { get; set; }
        public string MED_CUENTA_CONTABLE { get; set; }
        public string MED_TELEFONO_CASA { get; set; }
        public string MED_TELEFONO_CONSULTORIO { get; set; }
        public string MED_TELEFONO_CELULAR { get; set; }
        public string MED_AUTORIZACION_SRI { get; set; }
        public string MED_VALIDEZ_AUTORIZACION { get; set; }
        public string MED_FACTURA_INICIAL { get; set; }
        public string MED_FACTURA_FINAL { get; set; }
        public bool MED_CON_TRANSFERENCIA { get; set; }
        public bool MED_RECIBE_LLAMADA { get; set; }
        public bool MED_ESTADO { get; set; }
        public string ENTITYSETNAME { get; set; }
        public string ENTITYID { get; set; }
    }
}
