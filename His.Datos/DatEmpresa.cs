using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;

namespace His.Datos
{
    public class DatEmpresa
    {
        public Int16 RecuperaMaximoEmpresa()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<EMPRESA> empresas  = contexto.EMPRESA.ToList();
                if (empresas.Count > 0)
                    maxim = contexto.EMPRESA.Max(emp => emp.EMP_CODIGO);
                else
                    maxim = 0;
                return maxim;

            }
            
        }
        public EMPRESA RecuperaEmpresa()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    EMPRESA x = (from e in contexto.EMPRESA
                                 select e).FirstOrDefault();
                    return x;
                }
            }
            catch (Exception err) { throw err; }
        }
        public List<EMPRESA> RecuperaEmpresas()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.EMPRESA.ToList();  
            }
        }
        public void CrearEmpresa(EMPRESA empresa)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("EMPRESA", empresa);
               
            }
        }
        public void GrabarEmpresa(EMPRESA empresaModificada, EMPRESA empresaOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(empresaModificada, empresaOriginal);
            }
        }
        public void EliminarEmpresa(EMPRESA empresa)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(empresa);
            }
        }
        public SUCURSALES RecuperarEmpresaID(int codigo)
        {
            using(var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                SUCURSALES empresa = (from e in db.SUCURSALES
                                   where e.SUC_CODIGO == codigo
                                   select e).FirstOrDefault();
                return empresa;
            }
        }
    }
}
