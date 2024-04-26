using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using System.Data;
using System.Data.SqlClient;

namespace His.Datos
{
    public class DatAseguradoras
    {
        public void Crear(ASEGURADORAS_EMPRESAS aseguradora)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("ASEGURADORAS_EMPRESAS", aseguradora);
            }
        }

        public List<ASEGURADORAS_EMPRESAS> ListaEmpresas(bool convenio)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return //contexto.ASEGURADORAS_EMPRESAS.Where(a=>a.ASE_CONVENIO==convenio).ToList();
                    (from a in contexto.ASEGURADORAS_EMPRESAS.Distinct()
                     //join c in contexto.CATEGORIAS_SERVICIOS.Distinct() on a.ASE_CODIGO equals c.ASEGURADORAS_EMPRESAS.ASE_CODIGO
                     where a.ASE_CONVENIO==convenio
                     select a).ToList();
            }
        }

        public List<ASEGURADORAS_EMPRESAS> ListaEmpresas()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.ASEGURADORAS_EMPRESAS.Include("TIPO_EMPRESA").Where(e => e.ASE_ESTADO == true).Distinct().OrderBy(e=>e.ASE_NOMBRE).ToList();
            }
        }
        
        public List<ASEGURADORAS_EMPRESAS> RecuperarListaEmpresas(Int16 codigoTipoEmpresa)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.ASEGURADORAS_EMPRESAS.Include("TIPO_EMPRESA").Where(e => e.TIPO_EMPRESA.TE_CODIGO == codigoTipoEmpresa).ToList();
            }
        }

        public ASEGURADORAS_EMPRESAS RecuperaEmpresa(Int16 codigoTipoEmpresa)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.ASEGURADORAS_EMPRESAS.Where(e => e.ASE_CODIGO == codigoTipoEmpresa).FirstOrDefault();
            }
        }

        public List<TIPO_EMPRESA> RecuperaTipoEmpresa()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.TIPO_EMPRESA.Where(t=>t.TE_ESTADO==true).ToList();
            }
        }

        public Int16 RecuperaMaximoCodigoEmpresas()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                if (contexto.ASEGURADORAS_EMPRESAS.ToList().Count() > 0)
                    maxim = contexto.ASEGURADORAS_EMPRESAS.Max(loc => loc.ASE_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }
        }

        public ASEGURADORAS_EMPRESAS RecuperarEmpresa(string RUC)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from a in contexto.ASEGURADORAS_EMPRESAS
                        where a.ASE_RUC == RUC
                        select a).FirstOrDefault();
            }
        }

        public void actualizarConvenio(ASEGURADORAS_EMPRESAS aseguradora, bool estado)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ASEGURADORAS_EMPRESAS aseg = contexto.ASEGURADORAS_EMPRESAS.FirstOrDefault(a=>a.ASE_CODIGO==aseguradora.ASE_CODIGO);
                aseg.ASE_CONVENIO = estado;
                contexto.SaveChanges();
            }
        }

        public void ModificarAseguradora(ASEGURADORAS_EMPRESAS asegModificada)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ASEGURADORAS_EMPRESAS aseguradora = contexto.ASEGURADORAS_EMPRESAS.FirstOrDefault(a=>a.ASE_CODIGO==asegModificada.ASE_CODIGO);
                aseguradora.ASE_NOMBRE = asegModificada.ASE_NOMBRE;
                aseguradora.ASE_DIRECCION = asegModificada.ASE_DIRECCION;
                aseguradora.ASE_CIUDAD = asegModificada.ASE_CIUDAD;
                aseguradora.ASE_TELEFONO = asegModificada.ASE_TELEFONO;
                contexto.SaveChanges();

            }
        }

        public List<TIPO_EMPRESA> ListaTiposEmpresa()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in contexto.TIPO_EMPRESA
                        where t.TE_ESTADO == true
                        orderby t.TE_DESCRIPCION
                        select t).ToList();
            }
        }


        public ASEGURADORAS_EMPRESAS recuperaAseguradoraPorAtencion(Int64 codAtencion)
        {
            
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from a in contexto.ATENCIONES
                        join at in contexto.ATENCION_DETALLE_CATEGORIAS on a.ATE_CODIGO equals at.ATENCIONES.ATE_CODIGO
                        join c in contexto.CATEGORIAS_CONVENIOS on at.CATEGORIAS_CONVENIOS.CAT_CODIGO equals c.CAT_CODIGO
                        join ase in contexto.ASEGURADORAS_EMPRESAS on c.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals ase.ASE_CODIGO
                        where a.ATE_CODIGO == codAtencion
                    select ase).FirstOrDefault();
            }
        }
 


    }
}
