using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;



namespace His.Datos
{
    public class DatPais
    {
        public Int16 RecuperaMaximoPais()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PAIS> paises = contexto.PAIS.ToList();
                if (paises.Count > 0)
                    maxim = contexto.PAIS.Max(emp => emp.CODPAIS);
                else
                    maxim = 0;
                return maxim;
            }
            
        }
        public List<PAIS> RecuperaPaises()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PAIS
                        orderby p.NOMPAIS
                        select p).ToList();

            }
        }
        public void CrearPais(PAIS pais)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("PAIS", pais);

            }
        }
        public void GrabarPais(PAIS paisModificada, PAIS paisOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(paisModificada, paisOriginal);
            }
        }
        public void EliminarPais(PAIS pais)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(pais);
            }
        }
        public PAIS RecuperarPaisID(int codigoPais)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.PAIS.FirstOrDefault(p=>p.CODPAIS==codigoPais);
            }
        }
        public List<PAIS> ListaNacionalidades()
        {
            using(var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PAIS
                        where p.NACIONALIDAD!=null
                        select p).ToList();
            }
        }
    }
}
