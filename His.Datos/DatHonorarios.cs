using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Core.Datos;
using His.Entidades;

namespace His.Datos
{
    public class DatHonorarios
    {
        #region Afectaciones
        /// <summary>
        /// Inserta usuarios
        /// </summary>
        /// <param name="procedimiento">Nombre del procedimiento</param>
        /// <param name="usuario">Datos del ususario</param>
        //public void InsertaMedicos(string procedimiento, MEDICOS medicos)
        //{
        //    FuenteDatos conexion = new FuenteDatos();
        //    //llenamos los argumentos con los datos del usuario
        //    List<object> parametros = new List<object>();
        //    parametros.Add(medicos.MED_CODIGO);
        //    parametros.Add(medicos.ID_USUARIO);
        //    parametros.Add(medicos.ESP_CODIGO);
        //    parametros.Add(medicos.MED_RUC);
        //    parametros.Add(medicos.MED_NUM_CUENTA);
        //    parametros.Add(medicos.MED_TIPO_CUENTA);
        //    parametros.Add(medicos.MED_TELEFONO_CASA);
        //    parametros.Add(medicos.MED_TELEFONO_CONSULTORIO);
        //    parametros.Add(medicos.MED_TELEFONO_CELULAR);
        //    parametros.Add(medicos.MED_AUTORIZACION_RETENCION);
        //    parametros.Add(medicos.MED_ELIMINADO);

        //    conexion.Ejecutar(procedimiento, parametros.ToArray());
        //}
        /// <summary>
        /// Modificar Usuarios
        /// </summary>
        /// <param name="procedimiento">Nombre del procedimiento</param>
        /// <param name="Args">Datos del usuario</param>
        //public void ModificaMedicos(string procedimiento, Medicos medicos)
        //{
        //    FuenteDatos conexion = new FuenteDatos();
        //    //llenamos los argumentos con los datos del usuario
        //    List<object> parametros = new List<object>();
        //    parametros.Add(medicos.MED_CODIGO);
        //    parametros.Add(medicos.ESP_CODIGO);
        //    parametros.Add(medicos.MED_RUC);
        //    parametros.Add(medicos.MED_NUM_CUENTA);
        //    parametros.Add(medicos.MED_TIPO_CUENTA);
        //    parametros.Add(medicos.MED_TELEFONO_CASA);
        //    parametros.Add(medicos.MED_TELEFONO_CONSULTORIO);
        //    parametros.Add(medicos.MED_TELEFONO_CELULAR);
        //    parametros.Add(medicos.MED_AUTORIZACION_RETENCION);
        //    parametros.Add(medicos.MED_ELIMINADO);


        //    conexion.Ejecutar(procedimiento, parametros.ToArray());
        //}
        #endregion
        public List<DtoHCEX> listaHCEX()
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<DtoHCEX> hcex = new List<DtoHCEX>();
                List<HONORARIOS_CONSULTA_EXTERNA> cex = db.HONORARIOS_CONSULTA_EXTERNA.ToList();
                foreach (var item in cex)
                {
                    DtoHCEX hx = new DtoHCEX();
                    hx.COD_PRODUCTO = item.PRO_CODIGO;
                    hx.DESCRIPCION = item.PRO_DESCRIPCION;
                    hcex.Add(hx);
                }
                return hcex;
            }
        }
        public bool insertarHCEX(HONORARIOS_CONSULTA_EXTERNA hcex)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    db.Crear("HONORARIOS_CONSULTA_EXTERNA", hcex);
                    db.SaveChanges();
                    transac.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transac.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }

            }
        }
        public bool eliminarHCEX(string PRO_CODIGO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    HONORARIOS_CONSULTA_EXTERNA hcex = db.HONORARIOS_CONSULTA_EXTERNA.FirstOrDefault(x => x.PRO_CODIGO == PRO_CODIGO);
                    db.DeleteObject(hcex);
                    db.SaveChanges();
                    transac.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transac.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }
        }
        public HONORARIOS_CONSULTA_EXTERNA existeProductoHonorarrio(string PRO_CODIGO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return db.HONORARIOS_CONSULTA_EXTERNA.FirstOrDefault(x => x.PRO_CODIGO == PRO_CODIGO);
            }
        }
    }
}
