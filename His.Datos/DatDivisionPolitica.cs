using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;

namespace His.Datos
{
    public class DatDivisionPolitica
    {
        public List<DIVISION_POLITICA> listaDivisionPolitica()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from d in contexto.DIVISION_POLITICA
                        orderby d.DIPO_NOMBRE
                        select d).ToList();
            }
        }
        public List<DIVISION_POLITICA> listaDivisionPolitica(string codClase)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return  (from d in contexto.DIVISION_POLITICA
                        join c in contexto.CLASE_LOCALIDAD on d.CLASE_LOCALIDAD.CLLO_CODIGO equals c.CLLO_CODIGO
                        where d.CLASE_LOCALIDAD.CLLO_CODIGO == codClase
                        orderby d.DIPO_NOMBRE
                        select d).ToList();
            }
        }
        public List<CLASE_LOCALIDAD> listaClasesLocalidad()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from c in contexto.CLASE_LOCALIDAD
                        select c).ToList();
            }
        }
        public List<DIVISION_POLITICA> RecuperarDivisionPolitica(string codPadre)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from d in contexto.DIVISION_POLITICA
                        where d.DIPO_DIPO_CODIINEC==codPadre || d.DIPO_CODIINEC =="0"
                        orderby d.DIPO_NOMBRE
                        select d).ToList();
            }
        }
        public DIVISION_POLITICA DivisionPolitica(string codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from d in contexto.DIVISION_POLITICA
                        join c in contexto.CLASE_LOCALIDAD on d.CLASE_LOCALIDAD.CLLO_CODIGO equals c.CLLO_CODIGO
                        join t in contexto.TIPO_LOCALIDAD on d.TIPO_LOCALIDAD.TILO_CODIGO equals t.TILO_CODIGO
                        where d.DIPO_CODIINEC == codigo
                        select d).FirstOrDefault();
            }
        }

        public List<CLASE_LOCALIDAD> clasesLocalidad()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from c in contexto.CLASE_LOCALIDAD
                        orderby c.CLLO_NOMBRE
                        select c).ToList();
            }
        }

        public List<TIPO_LOCALIDAD> tiposLocalidad()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from c in contexto.TIPO_LOCALIDAD
                        orderby c.TILO_NOMBRE
                        select c).ToList();
            }
        }

        public CLASE_LOCALIDAD claseLocalidad(int codDivPolitica)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from c in contexto.CLASE_LOCALIDAD
                        join d in contexto.DIVISION_POLITICA on c.CLLO_CODIGO equals d.CLASE_LOCALIDAD.CLLO_CODIGO
                        where d.DIPO_CODIGO == codDivPolitica
                        select c).FirstOrDefault();
            }
        }
        public TIPO_LOCALIDAD tipoLocalidad(int codDivPolitica)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from c in contexto.TIPO_LOCALIDAD
                        join d in contexto.DIVISION_POLITICA on c.TILO_CODIGO equals d.TIPO_LOCALIDAD.TILO_CODIGO
                        where d.DIPO_CODIGO ==codDivPolitica
                        select c).FirstOrDefault();
            }
        }

        public void EditarDivisionPolitica(DIVISION_POLITICA dpModificada)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                DIVISION_POLITICA dpOriginal = contexto.DIVISION_POLITICA.FirstOrDefault(d=>d.DIPO_CODIGO == dpModificada.DIPO_CODIGO);
                dpOriginal.DIPO_NOMBRE = dpModificada.DIPO_NOMBRE;
                dpOriginal.DIPO_LATITUD = dpModificada.DIPO_LATITUD;
                dpOriginal.DIPO_LONGITUD = dpModificada.DIPO_LONGITUD;
                //dpOriginal.CLASE_LOCALIDADReference.EntityKey = dpModificada.CLASE_LOCALIDADReference.EntityKey;
                dpOriginal.TIPO_LOCALIDADReference.EntityKey = dpModificada.TIPO_LOCALIDADReference.EntityKey;
                contexto.SaveChanges();
            }
        }

        public void CrearDivisionPolitica(DIVISION_POLITICA dpNueva)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToDIVISION_POLITICA(dpNueva);
                contexto.SaveChanges();
            }
        }

        public int maxCodigo()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.DIVISION_POLITICA
                             select d.DIPO_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }

        public void EliminarDivisionPolitica(string codInec)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                DIVISION_POLITICA divPolitica = (from c in contexto.DIVISION_POLITICA
                                                 where c.DIPO_CODIINEC == codInec
                                                 select c).FirstOrDefault();

                contexto.DeleteObject(divPolitica);
                contexto.SaveChanges();
            }
        }

    }
}
