using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public class AccesosModuloTarifario
    {
        #region Variables

        private static bool consultaTipoEmpresa = false;
        private static bool consultaAseguradorasEmpresas = false;
        private static bool consultaConveniosProcedimientos = false;
        private static bool consultaTarifario = false;
        private static bool consultaProcedimientos = false;
        private static bool consultaCreacionHonorarios = false;
        private static bool consultaReportes = false;
        private static bool tipoEmpresaCRUD = false;
        private static bool aseguradorasEmpresasCRUD = false;
        private static bool conveniosCRUD = false;
        private static bool tarifariosCRUD = false;
        private static bool procedimientosCRUD = false;
        private static bool consultaHonorariosRealizados = false;
        private static bool consultaEstructuraEspecialidades = false;
        private static bool estructuraEspecialidades = false;

        private static bool creacionTipoCatologo = false;
        private static bool modificacionTipoCatologo = false;
        private static bool eliminacionTipoCatologo = false;

        private static bool creacionCatologo = false;
        private static bool modificacionCatologo = false;
        private static bool eliminacionCatologo = false;

        private static bool creacionConvenio = false;
        private static bool modificacionConvenio = false;
        private static bool eliminacionConvenio = false;

        private static bool creacionPrecioConvenio = false;
        private static bool modificacionPrecioConvenio = false;
        private static bool eliminacionPrecioConvenio = false;


        private static bool administracion = false;
        private static bool tipoEmpresa = false;
        private static bool aseguradoraEmpresa = false;
        private static bool convenio = false;
        private static bool tarifario = false;
        private static bool procedimiento = false;
        private static bool tarifarioM = false;
        private static bool creacionHonorarios = false;
        private static bool consultaHonorarios = false;
        private static bool preciosYporcentajes = false;
        private static bool tipoCosto = false;
        private static bool catalogoCosto = false;
        private static bool convenios = false;
        private static bool preciosConvenios = false;
        private static bool reporte = false;
        private static bool honorario = false;
        private static bool empresasAseguradoras = false;
        private static bool ventas = false;
        private static bool mosaicoHorizontal = false;
        private static bool mosaicoVertical = false;
        private static bool cascada = false;
        private static bool organizarIconos = false;
        private static bool ayuda = false;
        private static bool acercaDe = false;

        #endregion

        #region Metodos get y set
        public static bool CreacionConvenio
        {
            get { return AccesosModuloTarifario.creacionConvenio; }
            set { AccesosModuloTarifario.creacionConvenio = value; }
        }
        public static bool ModificacionConvenio
        {
            get { return AccesosModuloTarifario.modificacionConvenio; }
            set { AccesosModuloTarifario.modificacionConvenio = value; }
        }
        public static bool EliminacionConvenio
        {
            get { return AccesosModuloTarifario.eliminacionConvenio; }
            set { AccesosModuloTarifario.eliminacionConvenio = value; }
        }

        public static bool ModificacionPrecioConvenio
        {
            get { return AccesosModuloTarifario.modificacionPrecioConvenio; }
            set { AccesosModuloTarifario.modificacionPrecioConvenio = value; }
        }
        public static bool CreacionPrecioConvenio
        {
            get { return AccesosModuloTarifario.creacionPrecioConvenio; }
            set { AccesosModuloTarifario.creacionPrecioConvenio = value; }
        }
        public static bool EliminacionPrecioConvenio
        {
            get { return AccesosModuloTarifario.eliminacionPrecioConvenio; }
            set { AccesosModuloTarifario.eliminacionPrecioConvenio = value; }
        }

        public static bool EliminacionCatologo
        {
            get { return AccesosModuloTarifario.eliminacionCatologo; }
            set { AccesosModuloTarifario.eliminacionCatologo = value; }
        }
        public static bool ModificacionCatologo
        {
            get { return AccesosModuloTarifario.modificacionCatologo; }
            set { AccesosModuloTarifario.modificacionCatologo = value; }
        }
        public static bool CreacionCatologo
        {
            get { return AccesosModuloTarifario.creacionCatologo; }
            set { AccesosModuloTarifario.creacionCatologo = value; }
        }

        public static bool EliminacionTipoCatologo
        {
            get { return AccesosModuloTarifario.eliminacionTipoCatologo; }
            set { AccesosModuloTarifario.eliminacionTipoCatologo = value; }
        }
        public static bool CreacionTipoCatologo
        {
            get { return AccesosModuloTarifario.creacionTipoCatologo; }
            set { AccesosModuloTarifario.creacionTipoCatologo = value; }
        }
        public static bool ModificacionTipoCatologo
        {
            get { return AccesosModuloTarifario.modificacionTipoCatologo; }
            set { AccesosModuloTarifario.modificacionTipoCatologo = value; }
        }
        public static bool Administracion
        {
            get { return AccesosModuloTarifario.administracion; }
            set { AccesosModuloTarifario.administracion = value; }
        }
        public static bool TipoEmpresa
        {
            get { return AccesosModuloTarifario.tipoEmpresa; }
            set { AccesosModuloTarifario.tipoEmpresa = value; }
        }
        public static bool AseguradoraEmpresa
        {
            get { return AccesosModuloTarifario.aseguradoraEmpresa; }
            set { AccesosModuloTarifario.aseguradoraEmpresa = value; }
        }
        public static bool Convenio
        {
            get { return AccesosModuloTarifario.convenio; }
            set { AccesosModuloTarifario.convenio = value; }
        }
        public static bool Tarifario
        {
            get { return AccesosModuloTarifario.tarifario; }
            set { AccesosModuloTarifario.tarifario = value; }
        }
        public static bool Procedimiento
        {
            get { return AccesosModuloTarifario.procedimiento; }
            set { AccesosModuloTarifario.procedimiento = value; }
        }
        public static bool MenuTarifario
        {
            get { return AccesosModuloTarifario.tarifarioM; }
            set { AccesosModuloTarifario.tarifarioM = value; }
        }
        public static bool ConsultaHonorario
        {
            get { return AccesosModuloTarifario.consultaHonorarios; }
            set { AccesosModuloTarifario.consultaHonorarios = value; }
        }
        public static bool PreciosProcentajes
        {
            get { return AccesosModuloTarifario.preciosYporcentajes; }
            set { AccesosModuloTarifario.preciosYporcentajes = value; }
        }
        public static bool TipoCosto
        {
            get { return AccesosModuloTarifario.tipoCosto; }
            set { AccesosModuloTarifario.tipoCosto = value; }
        }
        public static bool CatalogoCosto
        {
            get { return AccesosModuloTarifario.catalogoCosto; }
            set { AccesosModuloTarifario.catalogoCosto = value; }
        }
        public static bool Convenios
        {
            get { return AccesosModuloTarifario.convenios; }
            set { AccesosModuloTarifario.convenios = value; }
        }
        public static bool PreciosConvenios
        {
            get { return AccesosModuloTarifario.preciosConvenios; }
            set { AccesosModuloTarifario.preciosConvenios = value; }
        }
        public static bool ModificacionTipoCatalogo
        {
            get { return AccesosModuloTarifario.modificacionTipoCatologo; }
            set { AccesosModuloTarifario.modificacionTipoCatologo = value; }
        }
        public static bool Reporte
        {
            get { return AccesosModuloTarifario.reporte; }
            set { AccesosModuloTarifario.reporte = value; }
        }
        public static bool Honorario
        {
            get { return AccesosModuloTarifario.honorario; }
            set { AccesosModuloTarifario.honorario = value; }
        }
        public static bool EmpresaAseguradora
        {
            get { return AccesosModuloTarifario.empresasAseguradoras; }
            set { AccesosModuloTarifario.empresasAseguradoras = value; }
        }
        public static bool Ventas
        {
            get { return AccesosModuloTarifario.ventas; }
            set { AccesosModuloTarifario.ventas = value; }
        }
        public static bool MosaicoHorizaontal
        {
            get { return AccesosModuloTarifario.mosaicoHorizontal; }
            set { AccesosModuloTarifario.mosaicoHorizontal = value; }
        }
        public static bool MosaicoVertical
        {
            get { return AccesosModuloTarifario.mosaicoVertical; }
            set { AccesosModuloTarifario.mosaicoVertical = value; }
        }
        public static bool Cascada
        {
            get { return AccesosModuloTarifario.cascada; }
            set { AccesosModuloTarifario.cascada = value; }
        }
        public static bool OrganizarIcono
        {
            get
            {
                return AccesosModuloTarifario.organizarIconos;
            }
            set { AccesosModuloTarifario.organizarIconos = value; }
        }
        public static bool Ayuda
        {
            get { return AccesosModuloTarifario.ayuda; }
            set { AccesosModuloTarifario.ayuda = value; }
        }
        public static bool AcercaDe
        {
            get { return AccesosModuloTarifario.acercaDe; }
            set { AccesosModuloTarifario.acercaDe = value; }
        }
        public static bool ConsultaTipoEmpresa//
        {
            get
            {
                return consultaTipoEmpresa;
            }
            set
            {
                consultaTipoEmpresa = value;
            }
        }

        public static bool ConsultaAseguradorasEmpresas//
        {
            get
            {
                return consultaAseguradorasEmpresas;
            }
            set
            {
                consultaAseguradorasEmpresas = value;
            }
        }

        public static bool ConsultaConveniosProcedimientos//
        {
            get
            {
                return consultaConveniosProcedimientos;
            }
            set
            {
                consultaConveniosProcedimientos = value;
            }
        }

        public static bool ConsultaTarifario//
        {
            get
            {
                return consultaTarifario;
            }
            set
            {
                consultaTarifario = value;
            }
        }

        public static bool ConsultaProcedimientos//
        {
            get
            {
                return consultaProcedimientos;
            }
            set
            {
                consultaProcedimientos = value;
            }

        }

        public static bool ConsultaCreacionHonorarios//
        {
            get
            {
                return consultaCreacionHonorarios;
            }
            set
            {
                consultaCreacionHonorarios = value;
            }

        }

        public static bool ConsultaReportes//
        {
            get
            {
                return consultaReportes;
            }
            set
            {
                consultaReportes = value;
            }
        }

        public static bool TipoEmpresaCRUD//
        {
            get
            {
                return tipoEmpresaCRUD;
            }
            set
            {
                tipoEmpresaCRUD = value;
            }

        }

        public static bool AseguradorasEmpresasCRUD//
        {
            get
            {
                return aseguradorasEmpresasCRUD;
            }
            set
            {
                aseguradorasEmpresasCRUD = value;
            }
        }

        public static bool ConveniosCRUD//
        {
            get
            {
                return conveniosCRUD;
            }
            set
            {
                conveniosCRUD = value;
            }
        }

        public static bool TarifariosCRUD//
        {
            get
            {
                return tarifariosCRUD;
            }
            set
            {
                tarifariosCRUD = value;
            }
        }

        public static bool ProcedimientosCRUD//
        {
            get
            {
                return procedimientosCRUD;
            }
            set
            {
                procedimientosCRUD = value;
            }
        }

        public static bool CreacionHonorarios//
        {
            get
            {
                return creacionHonorarios;
            }
            set
            {
                creacionHonorarios = value;
            }
        }

        public static bool ConsultaHonorariosRealizados
        {
            get
            {
                return consultaHonorariosRealizados;
            }
            set
            {
                consultaHonorariosRealizados = value;
            }
        }
        public static bool ConsultaEstructuraEspecialidades//
        {
            get
            {
                return consultaEstructuraEspecialidades;
            }
            set
            {
                consultaEstructuraEspecialidades = value;
            }
        }

        public static bool EstructuraEspecialidades//
        {
            get
            {
                return estructuraEspecialidades;
            }
            set
            {
                estructuraEspecialidades = value;
            }
        }

        #endregion
    }
}
