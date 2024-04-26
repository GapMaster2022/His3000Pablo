using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatLocales
    {
        public Int16 RecuperaMaximoLocal()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            { 
                List<LOCALES> locls = new List<LOCALES>();
                locls = contexto.LOCALES.ToList();
                if (locls.Count > 0)
                    maxim = contexto.LOCALES.Max(loc => loc.LOC_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }
            
        }
        public List<LOCALES> ListaLocales()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.LOCALES.ToList();
            }
        }
        public List<DtoLocales> RecuperaLocales()
        {
            List<DtoLocales> dtolocales = new List<DtoLocales>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<LOCALES> locales = contexto.LOCALES.Include("ZONAS").Include("TIPO_NEGOCIO").ToList();

                foreach (var acceso in locales)
                {
                    dtolocales.Add(new DtoLocales() { LOC_CODIGO = acceso.LOC_CODIGO, CODTIPNEG =Int16.Parse(acceso.TIPO_NEGOCIO.EntityKey.EntityKeyValues[0].Value.ToString()),
                        CODZONA =acceso.ZONAS.CODZONA, NOMZONA = acceso.ZONAS.NOMZONA,
                        ID_USUARIO = acceso.ID_USUARIO == null ? Int16.Parse("0") : Int16.Parse(acceso.ID_USUARIO.ToString()), 
                        LOC_AREA=acceso.LOC_AREA==null ? float.Parse("0"):float.Parse(acceso.LOC_AREA.ToString()), 
                        LOC_BODEGA= acceso.LOC_BODEGA, LOC_DIRECCION =acceso.LOC_DIRECCION, LOC_FAX=acceso.LOC_FAX,
                        LOC_MATRIZ = acceso.LOC_MATRIZ == null ? false : bool.Parse(acceso.LOC_MATRIZ.ToString()),
                        LOC_NOMBRE= acceso.LOC_NOMBRE,
                        LOC_NUMEMPLE = acceso.LOC_NUMEMPLE == null ? Int16.Parse("0") : Int16.Parse(acceso.LOC_NUMEMPLE.ToString()),
                        LOC_PORCENTAJE_DIS = acceso.LOC_PORCENTAJE_DIS == null ? int.Parse("0") : int.Parse(acceso.LOC_PORCENTAJE_DIS.ToString()), 
                        LOC_PRINCIPAL= acceso.LOC_PRINCIPAL,
                        LOC_PRIORIDAD = acceso.LOC_PRIORIDAD == null ? int.Parse("0") : int.Parse(acceso.LOC_PRIORIDAD.ToString()), 
                        LOC_RUC = acceso.LOC_RUC, LOC_TEL1= acceso.LOC_TEL1, LOC_TEL2= acceso.LOC_TEL2,
                        LOC_TELEFONO = acceso.LOC_TELEFONO,
                        ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName(),
                        ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key });
                }
                return dtolocales;
                
            }
        }
        public void CrearLocal(LOCALES datlocal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //Core.Datos.ExtensionEntiy.Crear(contexto, "LOCALES", datlocal);
                contexto.Crear("LOCALES", datlocal);

            }
        }
        public void GrabarLocal(LOCALES empresaModificada, LOCALES empresaOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
               // ExtensionEntiy.Grabar(contexto, empresaModificada, empresaOriginal);   
               contexto.Grabar(empresaModificada, empresaOriginal);
            }
        }
        public void EliminarLocal(LOCALES datlocal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //Core.Datos.ExtensionEntiy.Eliminar(contexto, datlocal);
                contexto.Eliminar(datlocal);
            }
        }
        public LOCALES RecuperarLocalID(Int16 codLocal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from l in contexto.LOCALES
                            where l.LOC_CODIGO == codLocal
                            select l).FirstOrDefault();
            }
        }
    }
}
