using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatZonas
    {
        public List<DtoZonas> RecuperaZonasFormulario()
        {
            List<DtoZonas> zonasgrid = new List<DtoZonas>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ZONAS> zonas = new List<ZONAS>();
                zonas = contexto.ZONAS.Include("CIUDAD").Include("EMPRESA").ToList();
                foreach (var acceso in zonas)
                {
                    zonasgrid.Add(new DtoZonas()
                    {
                        CODZONA=acceso.CODZONA,
                        CODCIUDAD = acceso.CIUDAD.CODCIUDAD,
                        EMP_CODIGO=acceso.EMPRESA.EMP_CODIGO,
                        NOMZONA=acceso.NOMZONA,
                        CODIGO=acceso.CODIGO,
                        ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName()
                        ,
                        ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key
                    });
                }
                return zonasgrid;
            }
        }
        public Int16 RecuperaMaximoZona()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ZONAS> zona = contexto.ZONAS.ToList();
                if (zona.Count > 0)
                    maxim = contexto.ZONAS.Max(emp => emp.CODZONA);
                else
                    maxim = 0;
                return maxim;
            }
            
        }   
        public List<ZONAS> RecuperaZonas()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.ZONAS.ToList().OrderBy(zon => zon.NOMZONA).ToList();
            }
        }
        public void CrearZona(ZONAS zona)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                Core.Datos.ExtensionEntiy.Crear(contexto, "ZONAS", zona);
               // contexto.Crear("ZONA", zona);

            }
        }
        public void GrabarZona(ZONAS zonaModificada, ZONAS zonaOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                Core.Datos.ExtensionEntiy.Grabar(contexto, zonaModificada, zonaOriginal);
                //contexto.Grabar(zonaModificada, zonaOriginal);
            }
        }
        public void EliminarZona(ZONAS zona)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                Core.Datos.ExtensionEntiy.Eliminar(contexto, zona);
                //contexto.Eliminar(empresa);
            }
        }

    }
}
