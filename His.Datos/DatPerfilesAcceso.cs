using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace His.Datos
{
    public class DatPerfilesAcceso
    {
        #region Consultas
        /// <summary>
        /// Consulta los accesos que tiene un usuario deacuerdo al modulo
        /// </summary>
        /// <param name="usuario">codigo del usuario</param>
        /// <param name="modulo">modulo del que se quiere los accesos</param>
        /// <returns></returns>
        public List<ACCESO_OPCIONES> AccesosUsuarios(Int16 usuario, Int16 modulo)
        {
            try
            {
                List<ACCESO_OPCIONES> perfilesAccesos = new List<ACCESO_OPCIONES>();

                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    perfilesAccesos = contexto.USUARIOS.Include("USUARIOS_PERFILES").Where(usu => usu.ID_USUARIO == usuario).First().USUARIOS_PERFILES

                        .Join(contexto.PERFILES_ACCESOS, b => b.ID_PERFIL, (PERFILES_ACCESOS a) => a.ID_PERFIL, (b, a) => a)
                        .Join(contexto.ACCESO_OPCIONES.Include("MODULO"), b => b.ID_ACCESO, (ACCESO_OPCIONES a) => a.ID_ACCESO, (b, a) => a)
                       .Where(per => per.MODULO.ID_MODULO == modulo).ToList();
                }
                return perfilesAccesos;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public List<DtoAccesosPorPerfil> ListaConsultaTablasOpciones(int idModulo, int idPerfil)
        {
            List<DtoAccesosPorPerfil> datos = new List<DtoAccesosPorPerfil>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ACCESO_OPCIONES> accesoOpciones= contexto.ACCESO_OPCIONES.Where(acc => acc.MODULO.ID_MODULO == idModulo).ToList();
                List<PERFILES_ACCESOS> perfilAccesos = contexto.PERFILES_ACCESOS.Where(per => per.ID_PERFIL == idPerfil).ToList();
                foreach (var acceso in accesoOpciones)
                {
                    bool valor=true;
                    if (perfilAccesos.Where(per => per.ID_ACCESO == acceso.ID_ACCESO).FirstOrDefault() == null)
                        valor = false;
                    datos.Add(new DtoAccesosPorPerfil() { DESCRIPCION = acceso.DESCRIPCION, ID_ACCESO = acceso.ID_ACCESO, TIENEACCESO = valor });
                }
            }

            //DEPARTAMENTOS departamento = new DEPARTAMENTOS();
            //departamento.EMPRESAReference.EntityKey = new System.Data.EntityKey("EMPRESAS", "EMP_CODIGO",12);
            //ACCESO_OPCIONES acceso1 = new ACCESO_OPCIONES();
            //object d=acceso1.MODULOReference.EntityKey.EntityKeyValues[0].Value;
         return datos;
        }
        public List<PERFILES_ACCESOS> ListaPerfilesAccesos() 
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.PERFILES_ACCESOS.Include("ACCESO_OPCIONES.MODULO").ToList();
            }
        }
        
        #endregion

        #region Afectaciones
        public void EliminaListaPerfilesAccesos(List<PERFILES_ACCESOS> acperfModificado, List<PERFILES_ACCESOS> acperfOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.EliminarLista(acperfOriginal);
            }
        }
        public void CrearPerfilesAccesos(PERFILES_ACCESOS perfilacceso)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                Core.Datos.ExtensionEntiy.Crear(contexto, "PERFILES_ACCESOS", perfilacceso);
                //contexto.Crear("DEPARTAMENTOS", departamento);
            }
        }
        #endregion

        public List<PERFILES_ACCESOS> ListaPerfilesAccesosXmodulo(Int32 id_modulo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PERFILES_ACCESOS> peracc = (from p in db.PERFILES_ACCESOS
                                                 join a in db.ACCESO_OPCIONES on p.ACCESO_OPCIONES.ID_ACCESO equals a.ID_ACCESO
                                                 where a.MODULO.ID_MODULO == id_modulo
                                                 select p).ToList();
                return peracc;
            }
        }
        public bool EliminarPerfiAcceso(Int64 id_ferfil, Int64 id_acceso)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    PERFILES_ACCESOS peracc = db.PERFILES_ACCESOS.FirstOrDefault(x => x.ID_PERFIL == id_ferfil && x.ID_ACCESO == id_acceso);
                    if (peracc == null)
                    {
                        ConexionEntidades.ConexionEDM.Close();
                        return true;
                    }
                    db.Eliminar(peracc);
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
        public bool crearPerfilesAccessoXlista(List<ACCESO_OPCIONES> accop, Int32 id_perfil)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {                    
                    PERFILES perfil = db.PERFILES.FirstOrDefault(x => x.ID_PERFIL == id_perfil);
                    SqlCommand command;
                    SqlConnection connection;
                    SqlDataReader reader;
                    
                    
                    BaseContextoDatos obj = new BaseContextoDatos();
                    connection = obj.ConectarBd();
                    connection.Open();
                    foreach (var item in accop)
                    {
                        command = new SqlCommand("EXEC EjecutarTR_Cggruopc_AUD @Parametro", connection);
                        command.CommandType = CommandType.Text;
                        _ = command.Parameters.AddWithValue("@Parametro", His.Entidades.Clases.Sesion.codUsuario);
                        reader = command.ExecuteReader();
                        connection.Close();
                        connection.Open();
                        ACCESO_OPCIONES acceso = db.ACCESO_OPCIONES.FirstOrDefault(x => x.ID_ACCESO == item.ID_ACCESO);
                        PERFILES_ACCESOS peracc = new PERFILES_ACCESOS();
                        peracc.PERFILESReference.EntityKey = perfil.EntityKey;
                        peracc.ID_PERFIL = (short)id_perfil;
                        peracc.ACCESO_OPCIONESReference.EntityKey = acceso.EntityKey;
                        peracc.ID_ACCESO = item.ID_ACCESO;
                        db.Crear("PERFILES_ACCESOS", peracc);
                    }
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
    }
}
