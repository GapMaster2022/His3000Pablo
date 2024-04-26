using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatBancos
    {
        public Int16 RecuperaMaximoBanco()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<BANCOS> bancos = contexto.BANCOS.ToList();
                if (bancos.Count > 0)
                    maxim = contexto.BANCOS.Max(emp => emp.BAN_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }

        }
        public List<BANCOS> RecuperaBancos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.BANCOS.ToList();
            }
        }
        public void CrearBanco(BANCOS banco)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("BANCOS", banco);

            }
        }
        public void GrabarBanco(BANCOS bancoModificada, BANCOS bancoOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(bancoModificada, bancoOriginal);
            }
        }
        public void EliminarBanco(BANCOS banco)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(banco);
            }
        }
    }
}
