using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.General;
using System.Data;

namespace His.Negocio
{
   public class NegLaboratorio
    {
       public static List<DtoLaboratorio> RecuperarPacientes(string fechaIni, string fechaFin)
        {
            return new DatLaboratorio().RecuperarPacientes(fechaIni, fechaFin);
        }
        public static HC_LABORATORIO_CLINICO recuperarlaboratorioPorAtencion(int ateCodigo)
        {
            return new DatLaboratorio().recuperarlaboratorioPorAtencion(ateCodigo);
        }
        public static HC_LABORATORIO_CLINICO recuperarLaboratorio(Int64 lcl)
        {
            return new DatLaboratorio().recuperarLaboratorio(lcl);
        }
        public static List<HC_CATALOGOS> listarCatalogoLaboratorio(int hct_tipo)
        {
            return new DatLaboratorio().listarCatalogoLaboratorio(hct_tipo);
        }

        //public static List<Vista_Laboratorio > RecuperarPacientesFecha(DateTime fechaIni, DateTime fechaFin)
        //{
        //    try
        //    {
        //        return new DatLaboratorio().RecuperarPacientesFecha(fechaIni, fechaFin);
        //    }
        //    catch (Exception err) { throw err; }
        //}
        public static List<DtoLaboratorioEstructura> listarProductos(int codigo)
        {
            return new DatLaboratorio().listarProducto(codigo);
        }
        public static DataTable recuperaProductoLaboratorio(string codpro)
        {
            return new DatLaboratorio().recuperarProductoLaboratorio(codpro);
        }
        public static bool crearLaboratorio(HC_LABORATORIO_CLINICO cabecera, List<HC_LABORATORIO_CLINICO_DETALLE> detalle)
        {
            return new DatLaboratorio().crearLaboratorio(cabecera, detalle);
        }
        public static List<HC_LABORATORIO_CLINICO_DETALLE> recuperarLaboratorioDetalle(Int64 lcl)
        {
            return new DatLaboratorio().recuperarLaboratorioDetalle(lcl);
        }
        public static bool editarLaboratorio(HC_LABORATORIO_CLINICO cabecera, List<HC_LABORATORIO_CLINICO_DETALLE> detalle, Int64 lcl)
        {
            return new DatLaboratorio().editarLaboratorio(cabecera, detalle, lcl);
        }
        public static DataTable CargaDepartamento()
        {
            return new DatLaboratorio().CargaDepartamento();
        }
        
        public static bool RepProcedimiento(string proce)
        {
            return new DatLaboratorio().RepProcedimiento(proce);
        }
        public static bool CreaPerfil(PERFILES_LABORATORIO perLab)
        {
            return new DatLaboratorio().CreaPerfil(perLab);
        }
        public static bool ActualizarPerfil(Int64 PL_CODIGO, string PL_PERFIL)
        {
            return new DatLaboratorio().ActualizarPerfil(PL_CODIGO, PL_PERFIL);
        }
        public static bool EliminarPerfil(Int64 pci_codigo)
        {
            return new DatLaboratorio().EliminarPerfil(pci_codigo);
        }
        public static int UltimoOrdenProcedimiento(int pci_codigo)
        {
            return new DatQuirofano().UltimoOrdenProcedimiento(pci_codigo);
        }
        public static bool AgregarProducto(PERFILES_PRODUCTOS pproduc)
        {
            return new DatLaboratorio().AgregarProducto(pproduc);
        }
        public static string RepProducto(Int64 PL_CODIGO, Int64 coddep)
        {
            return new DatLaboratorio().RepProducto(PL_CODIGO,coddep);
        }
        public static DataTable listarPerfiles()
        {
            return new DatLaboratorio().listarPerfiles();
        }
        public static Int32 UltimoCodigo()
        {
            return new DatLaboratorio().UltimoCodigo();

        }
        public static bool ActualizarPerfil_Producto(Int64 PL_CODIGO, Int64 codpro, Int32 _CODPRO, Int32 coddep)
        {
            return new DatLaboratorio().ActualizarPerfil_Producto(PL_CODIGO, codpro, _CODPRO,coddep);
        }
        public static bool EliminarPerfil_producto(Int64 PL_CODIGO, Int32 codpro)
        {
            return new DatLaboratorio().EliminarPerfil_producto(PL_CODIGO,codpro);
        }

        public static List<PERFILES_LABORATORIO> cargaPerfiles(Int64 ID_USUARIO)
        {
            return new DatLaboratorio().cargaPerfiles(ID_USUARIO);
        }
        public static List<DtoLaboratorioEstructura> cargadgvHematologia(Int64 PL_CODIGO)
        {
            return new DatLaboratorio().cargadgvHematologia(PL_CODIGO);
        }
        public static List<DtoLaboratorioEstructura> cargadgvUroanalisis(Int64 PL_CODIGO)
        {
            return new DatLaboratorio().cargadgvUroanalisis(PL_CODIGO);
        }
        public static List<DtoLaboratorioEstructura> cargadvgCoprologico(Int64 PL_CODIGO)
        {
            return new DatLaboratorio().cargadvgCoprologico(PL_CODIGO);
        }
        public static List<DtoLaboratorioEstructura> cargadgvQSanguinea(Int64 PL_CODIGO)
        {
            return new DatLaboratorio().cargadgvQSanguinea(PL_CODIGO);
        }
        public static List<DtoLaboratorioEstructura> cargadgvSerologia(Int64 PL_CODIGO)
        {
            return new DatLaboratorio().cargadgvSerologia(PL_CODIGO);
        }
        public static List<DtoLaboratorioEstructura> cargadgvBacteriologia(Int64 PL_CODIGO)
        {
            return new DatLaboratorio().cargadgvBacteriologia(PL_CODIGO);
        }
        public static List<DtoLaboratorioEstructura> cargadgvOtros(Int64 PL_CODIGO)
        {
            return new DatLaboratorio().cargadgvOtros(PL_CODIGO);
        }
        public static bool actualizarPerfilesLaboratorio(int LCL_ESTADO, Int64 LCL_CODIGO)
        {
            return new DatLaboratorio().actualizarPerfilesLaboratorio(LCL_ESTADO, LCL_CODIGO);
        }
        public static DataTable listarProductoDt(int codigo)
        {
            return new DatLaboratorio().listarProductoDt(codigo);
        }
        public static DtoLaboratorioEstructura cargadgvHematologiaAyuda(Int64 codpro)
        {
            return new DatLaboratorio().cargadgvHematologiaAyuda(codpro);
        }
        public static DtoLaboratorioEstructura cargadgvUroanalisiAyuda(Int64 codpro)
        {
            return new DatLaboratorio().cargadgvUroanalisiAyuda(codpro);
        }
        public static DtoLaboratorioEstructura cargadgvCoprologicoAyuda(Int64 codpro)
        {
            return new DatLaboratorio().cargadgvCoprologicoAyuda(codpro);
        }
        public static DtoLaboratorioEstructura cargadgvQsanguineaAyuda(Int64 codpro)
        {
            return new DatLaboratorio().cargadgvQsanguineaAyuda(codpro);
        }
        public static DtoLaboratorioEstructura cargadgvSerologiaAyuda(Int64 codpro)
        {
            return new DatLaboratorio().cargadgvSerologiaAyuda(codpro);
        }
        public static DtoLaboratorioEstructura cargadgvBacteriologiaAyuda(Int64 codpro)
        {
            return new DatLaboratorio().cargadgvBacteriologiaAyuda(codpro);
        }
        public static DtoLaboratorioEstructura cargadgvOtrosAyuda(Int64 codpro)
        {
            return new DatLaboratorio().cargadgvOtrosAyuda(codpro);
        }
    }
}
