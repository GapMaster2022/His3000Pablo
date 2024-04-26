using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Datos;
using His.Entidades;

namespace His.Datos
{
    public class DatModulo
    {
        public List<MODULO> RecuperaModulos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.MODULO.ToList();
            }
        }
        public List<DtoModulo> ListaModulo()
        {
            List<DtoModulo> mod = new List<DtoModulo>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var dm = (from m in db.MODULO
                          where m.ESTADO == true
                          select m).ToList();
                DtoModulo dto = new DtoModulo();
                foreach (var item in dm)
                {
                    mod.Add(new DtoModulo() { ID = item.ID_MODULO, MODULO = item.DESCRIPCION, TODO = false });
                }
            }
            return mod;
        }
        public void acccesoTotale()
        {

        }
        public bool CrearModulo(MODULO modulo)
        {
            try
            {
                using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    db.AddToMODULO(modulo);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                //throw;
            }
        }
        public bool EditarModulo(Int64 id_modulo, string descripcion)
        {
            try
            {
                using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    MODULO mod = (from m in db.MODULO
                                  where m.ID_MODULO == id_modulo
                                  select m).FirstOrDefault();
                    mod.DESCRIPCION = descripcion;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
                //throw;
            }
        }
        public Int32 maxModulo()
        {
            Int32 id_modulo;
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                Int32 cod = (from m in db.MODULO
                             orderby m.ID_MODULO descending
                             select m.ID_MODULO).FirstOrDefault();
                id_modulo = Convert.ToInt32(cod + 1);
                return id_modulo;
            }
        }
        public bool EliminarModulo(Int32 id_modulo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                try
                {
                    MODULO mod = db.MODULO.SingleOrDefault(x => x.ID_MODULO == id_modulo);
                    if (mod != null)
                    {
                        db.DeleteObject(mod);
                        db.SaveChanges();
                        return true;
                    }
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                    throw;
                }
            }
        }
    }
}
