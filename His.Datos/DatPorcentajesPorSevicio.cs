using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;


namespace His.Datos
{
    public class DatPorcentajesPorSevicio
    {

        //public Int16 RecuperaMaximoPorcentajes()
        //{
        //    Int16 maxim;
        //    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //    {
        //        List<PORCENTAJES_POR_SERVICVIOS> porcentajes = contexto.PORCENTAJES_POR_SERVICVIOS.ToList();
        //        if (porcentajes.Count > 0)
        //            maxim = contexto.PORCENTAJES_POR_SERVICVIOS.Max(emp => emp.PPS_CODIGO);
        //        else
        //            maxim = 0;
        //        return maxim;
        //    }

        //}

       
        
        //public List<PORCENTAJES_POR_SERVICVIOS> RecuperaPorcentajes()
        //{
        //    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //    {
        //        return contexto.PORCENTAJES_POR_SERVICVIOS.Include("CATEGORIAS_SERVICIOS").Include("SERVICIOS_CLINICA").ToList();   
            
        //    }
        //}

        //public void EliminarPorcentajes(PORCENTAJES_POR_SERVICVIOS  porcentajes)
        //{
        //    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //    {
        //        contexto.Eliminar(porcentajes );
        //    }
        //}
        //public List<PORCENTAJES_POR_SERVICVIOS> RecuperarListaPorCategorias(Int16 codigoTipoCategoria)
        //{
        //    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //    {
        //        return contexto.PORCENTAJES_POR_SERVICVIOS.Include("CATEGORIAS_SERVICIOS").Where(e => e.CATEGORIAS_SERVICIOS.CAT_CODIGO == codigoTipoCategoria).ToList();

        //    }
        //}
        //public void CrearPorcentajes(PORCENTAJES_POR_SERVICVIOS porcentajes)
        //{
        //    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //    {
        //        contexto.Crear("PORCENTAJES_POR_SERVICVIOS", porcentajes);

        //    }
        //}
        //public void Grabarporcentajes(PORCENTAJES_POR_SERVICVIOS porcentajesModificada, PORCENTAJES_POR_SERVICVIOS porcentajesOriginal)
        //{
        //    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //    {
        //        contexto.Grabar(porcentajesModificada, porcentajesOriginal);
        //    }
        //}
    }
}
