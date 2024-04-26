using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
namespace His.Datos
{
    public class DatDepartamentos
    {
        public Int16 RecuperaMaximoDepartamento()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<DEPARTAMENTOS> departamentos = contexto.DEPARTAMENTOS.ToList();
                if (departamentos.Count > 0)
                    maxim = contexto.DEPARTAMENTOS.Max(emp => emp.DEP_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }
           
        }
        public List<DtoDepartamentos> RecuperaDepartamentos()
        {
            List<DtoDepartamentos> departamentosgrid = new List<DtoDepartamentos>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<DEPARTAMENTOS> departamento = new List<DEPARTAMENTOS>();
                departamento=  contexto.DEPARTAMENTOS.Include("EMPRESA").ToList();
                foreach (var acceso in departamento)
                {
                  departamentosgrid.Add(new DtoDepartamentos() {DEP_CODIGO= acceso.DEP_CODIGO, DEP_NOMBRE= acceso.DEP_NOMBRE, EMP_CODIGO= acceso.EMPRESA.EMP_CODIGO, EMP_NOMBRE= acceso.EMPRESA.EMP_NOMBRE
                      , ENTITYSETNAME=acceso.EntityKey.GetFullEntitySetName()
                      , ENTITYID= acceso.EntityKey.EntityKeyValues[0].Key
                      , DEP_PADRE= acceso.DEP_PADRE==null?Int16.Parse("0"):Int16.Parse(acceso.DEP_PADRE.ToString()), DEP_ESTADO= acceso.DEP_ESTADO});
                }
                return departamentosgrid;
            }
        }
        /// <summary>
        /// Metodo que devuelve un objeto de tipo departamento
        /// </summary>
        /// <param name="codigoUsuario">Codigo del usuario</param>
        /// <returns>objeto de tipo departamento</returns>
        public DEPARTAMENTOS RecuperarDepartamento(int codigoUsuario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                DEPARTAMENTOS  departamento = (from d in contexto.DEPARTAMENTOS
                               join u in contexto.USUARIOS on d.DEP_CODIGO equals u.DEPARTAMENTOS.DEP_CODIGO
                               where u.ID_USUARIO == codigoUsuario
                               select d).FirstOrDefault();
                return departamento; 
            }
        }
        public void CrearDepartamento(DEPARTAMENTOS departamento)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                Core.Datos.ExtensionEntiy.Crear(contexto,"DEPARTAMENTOS",departamento);
                //contexto.Crear("DEPARTAMENTOS", departamento);
            }
        }
        public void GrabarDepartamento(DEPARTAMENTOS departamentoModificada, DEPARTAMENTOS departamentoOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                contexto.Grabar(departamentoModificada, departamentoOriginal);
            }
        }
        public void EliminarDepartamento(DEPARTAMENTOS departamento)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(departamento);
            }
        }
        public List<DEPARTAMENTOS> ListaDepartamentos() 
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.DEPARTAMENTOS.OrderBy(x => x.DEP_NOMBRE).ToList();
            }
        }
    }
}
