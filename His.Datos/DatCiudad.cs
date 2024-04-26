using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatCiudad
    {
        public Int16 RecuperaMaximoCiudad()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<CIUDAD> ciudades = contexto.CIUDAD.ToList();
                if (ciudades.Count > 0)
                    maxim = contexto.CIUDAD.Max(emp => emp.CODCIUDAD);
                else
                    maxim = 0;
                return maxim;
            }
            
        }
        public List<CIUDAD> ListaCiudades()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.CIUDAD.Include("PAIS").ToList();
            }
        }
        public List<DtoCiudad> RecuperaCiudades()
        {
            List<DtoCiudad> ciudadgrid = new List<DtoCiudad>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<CIUDAD> ciudad = new List<CIUDAD>();
                ciudad = contexto.CIUDAD.Include("PAIS").ToList();
                foreach (var acceso in ciudad)
                {
                    ciudadgrid.Add(new DtoCiudad() { CODCIUDAD=acceso.CODCIUDAD,NOMCIU=acceso.NOMCIU,
                        CODPAIS=acceso.PAIS.CODPAIS, 
                        NOMPAIS= acceso.PAIS.NOMPAIS,
                     ENTITYSETNAME=acceso.EntityKey.GetFullEntitySetName()
                      , ENTITYID= acceso.EntityKey.EntityKeyValues[0].Key});
                }
                return ciudadgrid;
            }
        }
        public void CrearCiudad(CIUDAD ciudad)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("CIUDAD", ciudad);
            }
        }
        public void GrabarCiudad(CIUDAD ciudadModificada, CIUDAD ciudadOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(ciudadModificada, ciudadOriginal);
            }
        }
        public void EliminarCiudad(CIUDAD ciudad)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(ciudad);
            }
        }
        public CIUDAD RecuperarCiudadID(int codigoCiudad)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.CIUDAD.FirstOrDefault(c=>c.CODCIUDAD==codigoCiudad);
            }
        }
    }
}
