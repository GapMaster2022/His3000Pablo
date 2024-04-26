using His.Datos;
using His.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace His.Negocio
{
    public class NegMaintenance
    {
        public static DataTable getDataTable(string Tabla)
        {
            return new DatMaintenance().getDataTable(Tabla);
        }

        public static object getQuery(string tabla, int codigo = 0)
        {
            return new DatMaintenance().getQuery(tabla, codigo);
        }

        public static bool delete(string Tabla, int cod)
        {
            return new DatMaintenance().delete(Tabla, cod);
        }


        public static void setQuery(string tabla, PARAMETROS_DETALLE x)
        {
            new DatMaintenance().setQuery(tabla, x);
        }

        public static void save_CatalogoCostos(int cod, int tipo, string desc)
        {
            new DatMaintenance().save_CatalogoCostos(cod, tipo, desc);
        }

        public static void save_tipoAtencion(int cod, int tipo, string desc)
        {
            new DatMaintenance().save_TipoAtencion(cod, tipo, desc);
        }

        public static void setROW(string tabla, object[] values, string code = "")
        {
            new DatMaintenance().setROW(tabla, values, code);
        }
        public static void save_TipoCosto(int cod, string desc)
        {
            new DatMaintenance().save_TipoCosto(cod, desc);
        }

        public static int ultimoCodigoTipoCiudadano()
        {
            return new DatMaintenance().ultimoCodigoTipoCiudadano();
        }
        public static int ultimaNacionalidad()
        {
            return new DatMaintenance().ultimaNacionalidad();
        }
        public static int ultimPiso()
        {
            return new DatMaintenance().ultimoPiso();
        }
        public static void CrearTipoCiudadano(TIPO_CIUDADANO ciudadano)
        {
            new DatMaintenance().CrearCiudadano(ciudadano);
        }
        public static void ModificarTipoCiudadano(string tc_codigo, string tc_descripcion)
        {
            new DatMaintenance().ModificarTipoCiudadano(Convert.ToInt32(tc_codigo), tc_descripcion);
        }
        public static void CrearNacionalidad(PAIS paises)
        {
            new DatMaintenance().CreaNacionalidad(paises);
        }
        public static void CrearPiso(NIVEL_PISO paises)
        {
            new DatMaintenance().CreaPiso(paises);
        }
        public static void CrearPisoMaquina(int tipo, string desc, string maquina, Int32 bodegaP, Int32 bodegaD)
        {
            new DatMaintenance().CreaMaquina(tipo, desc, maquina, bodegaP,bodegaD);
        }
        public static void EditarNacionalidad(short codigo, string pais, string nacionalidad)
        {
            new DatMaintenance().EditarNacionalidad(codigo, pais, nacionalidad);
        }
        public static void EditarPiso(short codigo, string pais, short nacionalidad)
        {
            new DatMaintenance().EditarPiso(codigo, pais, nacionalidad);
        }
        public static bool existePiso(string ip)
        {
            return new DatMaintenance().existePisoMaquina(ip);
        }
        public static bool existeNivelPiso(string ip)
        {
            return new DatMaintenance().existePiso(ip);
        }
        public static void EditarPisoMaquina(int tipo, string desc, string maquina, string temp, Int32 bodegaP, Int32 bodegaD)
        {
            new DatMaintenance().EditarMaquina(tipo, desc, maquina, temp, bodegaP, bodegaD);
        }
        public static void EliminarPisoMaquina(string maquina)
        {
            new DatMaintenance().EliminarMaquina(maquina);
        }
        public static List<NIVEL_PISO> cargarComboNivelPiso()
        {
            return new DatMaintenance().cargarComboNivelPiso();
        }
        public static DataTable cargarBodegaExplorador()
        {
            return new DatMaintenance().cargarBodegaExplorador();
        }
        public static DataTable cargarBodega()
        {
            return new DatMaintenance().cargarBodega();
        }
        public static DataTable GetCiudadanos()
        {
            return new DatMaintenance().GetCiudadano();
        }
        public static DataTable GetNacionalidad()
        {
            return new DatMaintenance().GetPaises();
        }
        public static List<NIVEL_PISO> GetPiso()
        {
            return new DatMaintenance().GetPiso();
        }
        public static DataTable GetPisoMaquina()
        {
            return new DatMaintenance().GetPisoMaquina();
        }
        public static DataTable ConveniosPorCaducar(DateTime FechaInicio, DateTime FechaFin)
        {
            return new DatMaintenance().ConveniosPorCaducar(FechaInicio, FechaFin);
        }
        public static void EliminarTipoCiudadano(string tc_codigo)
        {
            new DatMaintenance().EliminarTipoCiudadano(Convert.ToInt32(tc_codigo));
        }
        public static void EliminarNacionalidad(string codigo)
        {
            new DatMaintenance().EliminarNacionalidad(Convert.ToInt16(codigo));
        }
        public static void EliminarPiso(string codigo)
        {
            new DatMaintenance().EliminarPiso(Convert.ToInt16(codigo));
        }
        public static DataTable cmdDivision()
        {
            return new DatMaintenance().cmdDivision();
        }
        public static List<SicProductoSubdivision> productosSubdivision()
        {
            return new DatMaintenance().productosSubdivision();
        }
        public static bool InsertaProductosSubdivision(Int64 codsub)
        {
            return new DatMaintenance().InsertaProductosSubdivision(codsub);
        }
        public static bool EliminarSubdivision(Int64 codsub)
        {
            return new DatMaintenance().EliminarSubdivision(codsub);
        }
        public static List<SicProductoSubdivision> productosSubdivisionExiste(int codsub)
        {
            return new DatMaintenance().productosSubdivisionExiste(codsub);
        }
        public static NIVEL_PISO recuperaPiso(Int64 NIV_CODIGO)
        {
            return new DatMaintenance().recuperaPiso(NIV_CODIGO);
        }
        public static DataTable recuperaBodega(Int64 _codlocal)
        {
            return new DatMaintenance().recuperaBodega(_codlocal);
        }
        public static ACCESO_OPCIONES listAccesoOpciones()
        {
            return new DatMaintenance().listAccesoOpciones();
        }
        public static List<ACCESO_OPCIONES> listaAccesoOpciones(Int64 id_modulo)
        {
            return new DatMaintenance().listaAccesoOpciones(id_modulo);
        }
        public static dsProcedimiento cargaPerfilesAcceso()
        {
            return new DatMaintenance().cargaPerfilesAcceso();
        }
        public static Int16 maxPerfil()
        {
            return new DatMaintenance().maxPerfil();
        }
        public static bool creaPerfil(PERFILES perfil)
        {
            return new DatMaintenance().creaPerfil(perfil);
        }
        public static bool editarPerfil(Int64 id_perfil, string descripcion)
        {
            return new DatMaintenance().editarPerfil(id_perfil, descripcion);
        }
        public static bool agregarAcceso(PERFILES_ACCESOS peracc)
        {
            return new DatMaintenance().agregarAcceso(peracc);
        }
        public static bool eliminarAcceso(Int64 id_perfil, Int64 id_acceso)
        {
            return new DatMaintenance().eliminarAcceso(id_perfil, id_acceso);
        }
        public static PERFILES_ACCESOS buscaPerfilesacceso(Int64 perfil, Int64 acceso)
        {
            return new DatMaintenance().buscaPerfilesacceso(perfil, acceso);
        }
        public static PERFILES buscaPerfiles(Int64 perfil)
        {
            return new DatMaintenance().buscaPerfiles(perfil);
        }
        public static bool eliminarPerfil(Int64 id_perfil)
        {
            return new DatMaintenance().eliminarPerfil(id_perfil);
        }
        public static bool crearPerfilSic(string nomdep)
        {
            return new DatMaintenance().crearPerfilSic(nomdep);
        }
        public static bool editarPerfilSic(Int64 coddep, string nomdep)
        {
            return new DatMaintenance().editarPerfilSic(coddep, nomdep);
        }
        public static bool eliminarPerfilSic(Int64 codgru)
        {
            return new DatMaintenance().eliminarPerfilSic(codgru);
        }
        public static bool crearModuloSic(string nommod)
        {
            return new DatMaintenance().crearModuloSic(nommod);
        }
        public static bool editarModuloSic(Int64 codmod, string nommod)
        {
            return new DatMaintenance().editarModuloSic(codmod, nommod);
        }
        public static bool eliminarModuloSic(Int64 codmod)
        {
            return new DatMaintenance().eliminarModuloSic(codmod);
        }
        public static List<DtoConsultasWeb> consultasWeb(DateTime desde, DateTime hasta)
        {
            return new DatMaintenance().consultasWeb(desde,hasta);
        }
        public static DataTable consultasWebSp(DateTime desde, DateTime hasta)
        {
            return new DatMaintenance().consultasWebSp(desde,hasta);
        }
    }
}
