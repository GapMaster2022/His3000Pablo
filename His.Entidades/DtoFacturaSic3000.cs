using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoFacturaSic3000
    {
        public string numnot { get; set; }
        public decimal tipdoc { get; set; }
        public string codloc { get; set; }
        public string codven { get; set; }
        public string numfac { get; set; }
        public string codcli { get; set; }
        public string tipcli { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string ruc { get; set; }
        public string pordes { get; set; }
        public decimal subtotal { get; set; }
        public decimal desctot { get; set; }
        public decimal totsiva { get; set; }
        public decimal totciva { get; set; }
        public decimal iva { get; set; }
        public decimal total { get; set; }
        public decimal regalia { get; set; }
        public string fecha1 { get; set; }
        public string cancelado { get; set; }
        public decimal items { get; set; }
        public string caja { get; set; }
        public string nomcli { get; set; }
        public string dircli { get; set; }
        public string telcli { get; set; }
        public string ruccli { get; set; }
        public string obs { get; set; }
        public string numguirem { get; set; }
        public string motivo { get; set; }
        public string ructra { get; set; }
        public string nomtra { get; set; }
        public decimal codcobcli { get; set; }
        public decimal codvencli { get; set; }
        public DateTime fecven { get; set; }
        public string numorden { get; set; }
        public decimal porven { get; set; }
        public string formpagPro { get; set; }
        public string validez { get; set; }
        public string tiempoentrega { get; set; }
        public string numfac2 { get; set; }
        public Boolean porcobrar { get; set; }
        public Boolean Impresa { get; set; }
        public decimal cajero { get; set; }
        public decimal subt_Dev { get; set; }
        public decimal coniva_Dev { get; set; }
        public decimal siniva_Dev { get; set; }
        public decimal desct_Dev { get; set; }
        public decimal iva_Dev { get; set; }
        public decimal Tot_Dev { get; set; }
        public Boolean pormayor { get; set; }
        public Boolean facturada { get; set; }
        public Boolean coniva { get; set; }
        public Boolean imprimedesct { get; set; }
        public string autorizacion { get; set; }
        public Boolean GrupoCliente { get; set; }
        public Int32 EmpId { get; set; }
        public Int32 ConvId { get; set; }
        public decimal porcentajeIva { get; set; }
        public List<DtoFacturaDetalleSic3000> DetalleFactura { get; set; }
    }
}
