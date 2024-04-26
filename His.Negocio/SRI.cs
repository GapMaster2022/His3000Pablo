using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Negocio
{
    public class SRI
    {
        public string numeroRuc { get; set; }
        public string razonSocial { get; set; }
        public string estadoContribuyenteRuc { get; set; }
        public string actividadEconomicaPrincipal { get; set; }
        public string tipoContribuyente { get; set; }
        public string regimen { get; set; }
        public string categoria { get; set; }
        public string obligadoLlevarContabilidad { get; set; }
        public string agenteRetencion { get; set; }
        public string contribuyenteEspecial { get; set; }
        public InformacionContribuyente informacionFechasContribuyente { get; set; }
        public List<representanteLegal> representantesLegales { get; set; }
        public string motivoCancelacionSuspension { get; set; }
        public string contribuyenteFantasma { get; set; }
        public string transaccionesInexistente { get; set; }
    }
}
