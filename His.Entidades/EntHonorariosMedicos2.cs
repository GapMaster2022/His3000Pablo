//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data;
//using System.ComponentModel; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class EntHonorariosMedicos2
    {
        //public const string EntitySetName = "HONORARIOS_MEDICOS";
        public int HOM_CODIGO { get; set; }
        public int ATE_CODIGO { get; set; }
        public int MED_CODIGO { get; set; }
        public int FOR_CODIGO { get; set; }
        public Int16 ID_USUARIO { get; set; }
        public string FOR_DESCRIPCION { get; set; }
        public String MED_NOMBRE { get; set; }
        public DateTime HOM_FECHA_INGRESO { get; set; }
        public string HOM_FACTURA_MEDICO { get; set; }
        public DateTime HOM_FACTURA_FECHA { get; set; }
        public decimal HOM_VALOR_NETO { get; set; }
        public decimal HOM_COMISION_CLINICA { get; set; }
        public decimal HOM_APORTE_LLAMADA { get; set; }
        public decimal HOM_RETENCION { get; set; }
        public decimal HOM_VALOR_TOTAL { get; set; }
        public decimal HOM_VALOR_PAGADO { get; set;}
        public Boolean HOM_ESTADO { get; set; }
        public String HOM_LOTE { get; set; }
        public string RET_CODIGO { get; set; }
        //public string ENTITYSETNAME { get; set; }
        //public string ENTITYID { get; set; }

        //[Browsable(true)]
        //public string MedicosNombre
        //{
        //    get;
        //    set;
            //get
            //{
            //    string res = "";
            //    if (this.MEDICOS != null)
            //    {
            //        res = this.MEDICOS.MED_APELLIDO_PATERNO + this.MEDICOS.MED_APELLIDO_MATERNO + this.MEDICOS.MED_NOMBRE1 + this.MEDICOS.MED_NOMBRE2;
            //    }
            //    else if (this.MEDICOSReference != null)
            //    {
            //        this.MEDICOSReference.Load();

            //        if (this.MEDICOS  != null)
            //        {
            //            res = this.MEDICOS.MED_APELLIDO_PATERNO + this.MEDICOS.MED_APELLIDO_MATERNO + this.MEDICOS.MED_NOMBRE1 + this.MEDICOS.MED_NOMBRE2;
            //        }
            //    }
            //    return res;
            //}
            //set
            //{
            //    this.MEDICOSReference.EntityKey = new EntityKey("HIS3000BDEntities.MEDICOS", "MED_CODIGO", value);
            //}

        //}
    }
}
