using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace His.Datos
{
    public class DatPerfiles
    {
        public Int16 RecuperaMaximoPerfil()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PERFILES> perfiles = contexto.PERFILES.ToList();
                if (perfiles.Count > 0)
                    maxim = contexto.PERFILES.Max(emp => emp.ID_PERFIL);
                else
                    maxim = 0;
                return maxim;
            }
        }
        public List<PERFILES> RecuperaPerfiles()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.PERFILES.ToList();
            }
        }
        public PERFILES RecuperaPerfil(Int64 perfil)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PERFILES
                        where p.ID_PERFIL == perfil
                        select p).FirstOrDefault();
            }
        }
        public void CrearPerfil(PERFILES perfil)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("PERFILES", perfil);

            }
        }
        public void GrabarPerfil(PERFILES perfilModificada, PERFILES perfilOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(perfilModificada, perfilOriginal);
            }
        }
        public void EliminarPerfil(PERFILES perfil)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(perfil);
            }
        }
        /// <summary>
        /// Metodo que retorna el perfil de un usuario
        /// </summary>
        /// <param name="codigoUsuario">Codigo de usuario</param>
        /// <returns>Objeto PERFILES</returns>
        public List<PERFILES> RecuperarPerfil(int codigoUsuario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PERFILES> perfil = (from p in contexto.PERFILES
                                         join u in contexto.USUARIOS_PERFILES on p.ID_PERFIL equals u.ID_PERFIL
                                         join us in contexto.USUARIOS on u.ID_USUARIO equals us.ID_USUARIO
                                         where us.ID_USUARIO == codigoUsuario
                                         select p).ToList();
                return perfil;
            }
        }
        public void borrarPerfiles(Int64 ID_PERFIL, Int64 ID_MODULO)
        {
            //SqlConnection Sqlcon;
            //SqlCommand Sqlcmd;
            //BaseContextoDatos obj = new BaseContextoDatos();
            //Sqlcon = obj.ConectarBd();



            //Sqlcon.Open();
            //string deleteQuery = "DELETE from PERFILES_ACCESOS where ID_PERFIL=@ID_PERFIL AND ID_ACCESO in (select ID_ACCESO from ACCESO_OPCIONES where ID_MODULO=@ID_MODULO)";
            //Sqlcmd = new SqlCommand(deleteQuery, Sqlcon);
            //Sqlcmd.CommandType = CommandType.Text;
            //Sqlcmd.Parameters.AddWithValue("@ID_PERFIL", ID_PERFIL);
            //Sqlcmd.Parameters.AddWithValue("@ID_MODULO", ID_MODULO);
            //Sqlcmd.ExecuteNonQuery();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transaction = ConexionEntidades.ConexionEDM.BeginTransaction();
                List<int> accOpc = new List<int>();

                accOpc = (from a in db.ACCESO_OPCIONES
                          where a.MODULO.ID_MODULO == ID_MODULO
                          select a.ID_ACCESO).ToList();

                List<PERFILES_ACCESOS> peracc = (from p in db.PERFILES_ACCESOS where p.ID_PERFIL == ID_PERFIL select p).Where(x => accOpc.Contains(x.ID_ACCESO)).ToList();
                try
                {
                    db.EliminarLista(peracc);
                    transaction.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                }
            }

            //finally
            //{
            //    try
            //    {
            //        Sqlcon.Close();
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //}
        }
        public void BorraPerfilesSic(Int64 id_perfil, Int64 id_modulo)
        {
            SqlDataReader reader;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlCommand command;
            SqlConnection connection;
            connection = obj.ConectarBd();
            connection.Open();

            try
            {

                command = new SqlCommand("update Sic3000..SeguridadGrupoOpciones set staopc='N' where codopc In(select codopc from Sic3000..SeguridadOpciones where codmod=@codmodulo) and codgru=@perfil ", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@codmodulo", id_modulo);
                command.Parameters.AddWithValue("@perfil", id_perfil);
                reader = command.ExecuteReader();
                connection.Close();
                connection.Open();
                command = new SqlCommand("update Sic3000..SeguridadUsuarioOpciones set staopc='N' WHERE codusu IN (select codusu from Sic3000..SeguridadesUsuarioGrupo where codgru = @perfil ) ", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@perfil", id_perfil);
                reader = command.ExecuteReader();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void BorraPerfilesCg(Int64 id_perfil, Int64 id_modulo)
        {
            SqlDataReader reader;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlCommand command;
            SqlConnection connection;
            connection = obj.ConectarBd();
            connection.Open();

            try
            {

                command = new SqlCommand("update Cg3000..Cggruopc  set staopc='N' where codopc In(select codopc from Cg3000..Cgopcion where codmod=@codmodulo) and codgru=@perfil ", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@codmodulo", id_modulo);
                command.Parameters.AddWithValue("@perfil", id_perfil);
                reader = command.ExecuteReader();
                connection.Close();
                connection.Open();
                command = new SqlCommand("update Cg3000..Cgopciusu set staopc='N' WHERE codusu IN (select codusu from Cg3000..Cgusugrup WHERE codgru=@perfil )", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@perfil", id_perfil);
                reader = command.ExecuteReader();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public PERFILES recuperaPerfilesXUsuario(Int64 id_usuario)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var perf = (from p in db.PERFILES
                            join up in db.USUARIOS_PERFILES on p.ID_PERFIL equals up.ID_PERFIL
                            where up.ID_USUARIO == id_usuario
                            select p).FirstOrDefault();
                return perf;
            }
        }
    }
}
