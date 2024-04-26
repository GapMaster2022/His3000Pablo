using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatCajas
    {
        public Int16 RecuperaMaximoCaja()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from c in contexto.CAJAS
                             select c.CAJ_CODIGO).ToList();
                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }

        }

        public int recuperaFacturaInicial()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from c in contexto.NUMERO_CONTROL_CAJAS
                             select c.NCC_FACTURA_INICIAL).ToList();
                if (lista.Count > 0)
                    return (Int32)lista.Max();

                return 0;
            }

        }
        public List<CAJAS> ListaCajas()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.CAJAS.Include("LOCALES").ToList();
            }
        }
        public List<DtoCajas> RecuperaCajas()
        {
            List<DtoCajas> grid = new List<DtoCajas>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<CAJAS> ciudad = new List<CAJAS>();
                ciudad = contexto.CAJAS.Include("LOCALES").ToList();

                //var lista = (from c in contexto.CAJAS
                //          join l in contexto.LOCALES on c.LOCALES.LOC_CODIGO equals l.LOC_CODIGO
                //          select new
                //          {
                //              c.CAJ_AUTORIZACION_SRI,
                //              c.CAJ_CODIGO,
                //              c.CAJ_ESTADO,
                //              c.CAJ_FECHA,
                //              c.CAJ_NUMERO,
                //              c.CAJ_NOMBRE,
                //              c.CAJ_PERIDO_VALIDEZ,
                //              l.LOC_CODIGO,
                //              l.LOC_NOMBRE,
                //              c.EntityKey
                //          }).ToList();


                foreach (var acceso in ciudad)
                {
                    grid.Add(new DtoCajas()
                    {
                        CAJ_AUTORIZACION_SRI = acceso.CAJ_AUTORIZACION_SRI,
                        CAJ_CODIGO = acceso.CAJ_CODIGO,
                        CAJ_ESTADO = acceso.CAJ_ESTADO,
                        CAJ_FECHA = acceso.CAJ_FECHA,
                        CAJ_NOMBRE = acceso.CAJ_NOMBRE,
                        CAJ_NUMERO = acceso.CAJ_NUMERO,
                        CAJ_PERIDO_VALIDEZ = Convert.ToDateTime(acceso.CAJ_PERIDO_VALIDEZ),
                        LOC_CODIGO = acceso.LOCALES.LOC_CODIGO,
                        LOC_NOMBRE = acceso.LOCALES.LOC_NOMBRE,
                        ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName()
                        ,
                        ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key
                    });
                }

                return grid;
            }
        }
        public void CrearCaja(CAJAS caja)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("CAJAS", caja);
            }
        }
        public void GrabarCaja(CAJAS cajaModificada, CAJAS cajaOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(cajaModificada, cajaOriginal);
            }
        }
        public void EliminarCaja(CAJAS caja)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(caja);
            }
        }

        public CAJAS RecuperarCajaID(Int16 codCaja)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from c in contexto.CAJAS
                        where c.CAJ_CODIGO == codCaja
                        select c).FirstOrDefault();
            }
        }
    }
}
