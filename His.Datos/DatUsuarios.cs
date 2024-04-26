using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace His.Datos
{
    public class DatUsuarios
    {

        public USUARIOS RecuperaUsuario(Int64 codusu)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from u in contexto.USUARIOS
                        where u.ID_USUARIO == codusu
                        select u).FirstOrDefault();
                //return contexto.USUARIOS.FirstOrDefault(usu => usu.ID_USUARIO == codusu);
            }

        }
        public USUARIOS_FIRMA recuperaFirma(Int64 ID_USUARIO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return db.USUARIOS_FIRMA.FirstOrDefault(u => u.ID_USUARIO == ID_USUARIO);
            }
        }
        public USUARIOS RecuperaUsuarioCedula(string codusu)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from u in contexto.USUARIOS
                        where u.IDENTIFICACION == codusu
                        select u).FirstOrDefault();
                //return contexto.USUARIOS.FirstOrDefault(usu => usu.ID_USUARIO == codusu);
            }

        }
        public USUARIOS RecuperaUsuarioNombres(string Datos) // Encuentra el codigo de un usuario apartir del nombre / Giovanny Tapia /18/09/2012
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from u in contexto.USUARIOS
                        where u.NOMBRES + " " + u.APELLIDOS == Datos
                        select u).FirstOrDefault();
                //return contexto.USUARIOS.FirstOrDefault(usu => usu.ID_USUARIO == codusu);
            }

        }


        public List<USUARIOS> RecuperaUsuarios()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.USUARIOS.ToList();
            }
        }
        public List<DtoUsuarios> RecuperaUsuariosFormulario()
        {
            List<DtoUsuarios> usuariogrid = new List<DtoUsuarios>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //var usuarios = (from u in contexto.USUARIOS
                //                join d in contexto.DEPARTAMENTOS on u.DEPARTAMENTOS.DEP_CODIGO equals d.DEP_CODIGO
                //                select new
                //                {
                //                    //ENTITYSETNAME = u.EntityKey.GetFullEntitySetName(),
                //                    //ENTITYID= u.EntityKey.EntityKeyValues[0].Value,
                //                    u.APELLIDOS,
                //                    d.DEP_CODIGO,
                //                    u.DIRECCION,
                //                    u.ESTADO,
                //                    u.FECHA_INGRESO,
                //                    u.FECHA_VENCIMIENTO,
                //                    u.ID_USUARIO,
                //                    u.IDENTIFICACION,
                //                    u.LOGEADO,
                //                    u.NOMBRES,
                //                    u.PWD,
                //                    u.USR
                //                }).ToList();

                List<USUARIOS> usuarios = new List<USUARIOS>();
                usuarios = contexto.USUARIOS.Include("DEPARTAMENTOS").ToList();
                foreach (var acceso in usuarios)
                {
                    usuariogrid.Add(new DtoUsuarios()
                    {
                        ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName(),
                        ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key,
                        APELLIDOS = acceso.APELLIDOS,
                        DEP_CODIGO = acceso.DEPARTAMENTOS.DEP_CODIGO,

                        DIRECCION = acceso.DIRECCION,
                        ESTADO = acceso.ESTADO,

                        FECHA_INGRESO = acceso.FECHA_INGRESO,
                        FECHA_VENCIMIENTO = acceso.FECHA_VENCIMIENTO == null ? DateTime.Parse("01/01/2010") : DateTime.Parse(acceso.FECHA_VENCIMIENTO.ToString()),

                        ID_USUARIO = acceso.ID_USUARIO,
                        IDENTIFICACION = acceso.IDENTIFICACION,
                        LOGEADO = acceso.LOGEADO,
                        NOMBRES = acceso.NOMBRES,
                        PWD = acceso.PWD,
                        USR = acceso.USR,
                        Codigo_Rol = Convert.ToInt64(acceso.Codigo_Rol)
                    });
                }
                return usuariogrid;
            }
        }
        public List<DtoUsuariosPerfil> ListaConsultaTablasOpciones(Int16 codusuario)
        {
            List<DtoUsuariosPerfil> datos = new List<DtoUsuariosPerfil>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PERFILES> perfiles = contexto.PERFILES.OrderBy(x => x.DESCRIPCION).ToList();
                List<USUARIOS_PERFILES> usuarioperfil = contexto.USUARIOS_PERFILES.Where(per => per.ID_USUARIO == codusuario).ToList();
                foreach (var acceso in perfiles)
                {
                    bool valor = true;
                    if (usuarioperfil.Where(per => per.ID_PERFIL == acceso.ID_PERFIL).FirstOrDefault() == null)
                        valor = false;
                    datos.Add(new DtoUsuariosPerfil() { DESCRIPCION = acceso.DESCRIPCION, ID_PERFIL = acceso.ID_PERFIL, TIENE_ACCESO = valor });
                }
            }

            return datos;
        }

        public DataTable AreaAsignada(Int16 ID_USUARIO)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("select DIRECCION from USUARIOS where ID_USUARIO = " + ID_USUARIO, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }
        public Int16 RecuperaMaximoUsuario()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<USUARIOS> usuarios = contexto.USUARIOS.ToList();
                if (usuarios.Count > 0)
                    maxim = contexto.USUARIOS.Max(emp => emp.ID_USUARIO);
                else
                    maxim = 0;
                return maxim;
            }

        }
        public void CrearUsuario(USUARIOS usuario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("USUARIOS", usuario);
            }
        }
        public DataTable BuscaCedula(string cedula)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("select identificacion from CG3000..TP_USUARIOS where  identificacion = '" + cedula + "'", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }
        public DataTable BuscaUsuario(string nomusuario)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("select nombreusu from CG3000..TP_USUARIOS where nombreusu = '" + nomusuario + "'", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }
        public List<ARE_ASIGNADA> cargarAreaAsignada()
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return db.ARE_ASIGNADA.OrderBy(x => x.AS_DESCRIPCION).ToList();
            }
        }
        public Int32 insertarUsuario(Usuarios usuarios)
        {
            Int32 result = 0;
            SqlConnection con;
            BaseContextoDatos obj = new BaseContextoDatos();
            con = obj.ConectarBd();
            con.Open();
            try
            {
                //conexion
                string sql = "sp_Registro_Urs_Sistemas";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@nombre", SqlDbType.NVarChar, 150).Value = usuarios.Nombre;
                cmd.Parameters.Add("@apellidos", SqlDbType.NVarChar, 50).Value = usuarios.Apellidos;
                cmd.Parameters.Add("@identificacion", SqlDbType.NVarChar, 20).Value = usuarios.Identificacion;
                cmd.Parameters.Add("@nombreusu", SqlDbType.NVarChar, 20).Value = usuarios.Nombreusu;
                cmd.Parameters.Add("@codusu", SqlDbType.Float).Value = usuarios.Codusu;
                cmd.Parameters.Add("@clave", SqlDbType.NVarChar, 20).Value = usuarios.Clave;
                cmd.Parameters.Add("@codigo_c", SqlDbType.Float).Value = usuarios.Codigo_c;
                cmd.Parameters.Add("@fechaIngreso", SqlDbType.DateTime).Value = usuarios.FechaIngreso;
                cmd.Parameters.Add("@fechaCaducidad", SqlDbType.DateTime).Value = usuarios.FechaCaducidad;
                cmd.Parameters.Add("@estado", SqlDbType.Int).Value = usuarios.Estado;
                cmd.Parameters.Add("@tipoUsuario", SqlDbType.Int).Value = usuarios.TipoUsuario;
                cmd.Parameters.Add("@coddep", SqlDbType.Int).Value = usuarios.CodDep;
                cmd.Parameters.Add("@direccion", SqlDbType.NVarChar, 50).Value = usuarios.Direccion;

                SqlParameter outparam = cmd.Parameters.Add("@idUsuario", SqlDbType.Int);
                outparam.Direction = ParameterDirection.Output;

                result = cmd.ExecuteNonQuery();

                usuarios.IdUsuario = Convert.ToInt64(cmd.Parameters["@idUsuario"].Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar Usuario");
            }
            finally
            {
                con.Close();
            }

            return result;

        }
        public Int32 actualizar(Usuarios usuarios)
        {
            Int32 result = 0;
            SqlConnection con;
            BaseContextoDatos obj = new BaseContextoDatos();
            con = obj.ConectarBd();
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {

                string sql = "sp_Actualiza_Urs_Sistemas";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@nombre", SqlDbType.NVarChar, 150).Value = usuarios.Nombre;
                cmd.Parameters.Add("@apellidos", SqlDbType.NVarChar, 50).Value = usuarios.Apellidos;
                cmd.Parameters.Add("@identificacion", SqlDbType.NVarChar, 20).Value = usuarios.Identificacion;
                cmd.Parameters.Add("@nombreusu", SqlDbType.NVarChar, 20).Value = usuarios.Nombreusu;
                cmd.Parameters.Add("@codusu", SqlDbType.Float).Value = usuarios.Codusu;
                cmd.Parameters.Add("@clave", SqlDbType.NVarChar, 20).Value = usuarios.Clave;
                cmd.Parameters.Add("@codigo_c", SqlDbType.Float).Value = usuarios.Codigo_c;
                cmd.Parameters.Add("@fechaIngreso", SqlDbType.DateTime).Value = usuarios.FechaIngreso;
                cmd.Parameters.Add("@fechaCaducidad", SqlDbType.DateTime).Value = usuarios.FechaCaducidad;
                cmd.Parameters.Add("@estado", SqlDbType.Int).Value = usuarios.Estado;
                cmd.Parameters.Add("@tipoUsuario", SqlDbType.Int).Value = usuarios.TipoUsuario;
                cmd.Parameters.Add("@idUsuario", SqlDbType.BigInt).Value = usuarios.IdUsuario;
                cmd.Parameters.Add("@coddep", SqlDbType.BigInt).Value = usuarios.CodDep;
                cmd.Parameters.Add("@direccion", SqlDbType.NVarChar, 50).Value = usuarios.Direccion;

                result = cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw new Exception("Error al actualizar Usuario");
            }

            finally
            {
                con.Close();
            }

            return result;

        }
        public Usuarios buscarporId(Int64 idUsuario)
        {

            Usuarios usuarios = new Usuarios();
            SqlConnection con;
            BaseContextoDatos obj = new BaseContextoDatos();
            con = obj.ConectarBd();
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                string sql = "sp_buscar_Usuario_id";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@idUsuario", SqlDbType.BigInt).Value = idUsuario;

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        usuarios.IdUsuario = Convert.ToInt64(reader["idUsuario"]);
                        usuarios.Nombre = Convert.ToString(reader["nombre"]);
                        usuarios.Apellidos = Convert.ToString(reader["apellidos"]);
                        usuarios.Identificacion = Convert.ToString(reader["identificacion"]);
                        usuarios.Nombreusu = Convert.ToString(reader["nombreusu"]);
                        usuarios.Codusu = Convert.ToInt64(reader["codusu"]);
                        usuarios.Clave = Convert.ToString(reader["clave"]);
                        usuarios.Codigo_c = Convert.ToInt64(reader["codigo_c"]);
                        usuarios.FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]);
                        usuarios.FechaCaducidad = Convert.ToDateTime(reader["fechaCaducidad"]);
                        usuarios.Estado = Convert.ToInt16(reader["estado"]);
                        usuarios.TipoUsuario = Convert.ToInt16(reader["tipoUsuario"]);

                    }
                }


            }
            catch (Exception)
            {

                throw new Exception("Error al buscar usuario");
            }

            finally
            {
                con.Close();
            }

            return usuarios;

        }

        public void GrabarUsuario(USUARIOS usuarioModificada, USUARIOS usuarioOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(usuarioModificada, usuarioOriginal);
            }
        }
        public void ActualizarUsuario(USUARIOS usuario)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    USUARIOS usuarioModificado = contexto.USUARIOS.FirstOrDefault(u => u.ID_USUARIO == usuario.ID_USUARIO);
                    //contexto.Attach(usuario);
                    //usuario.SetAllModified(contexto);
                    usuarioModificado.PWD = usuario.PWD;
                    contexto.SaveChanges();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public bool EliminarUsuario(Int64 id_usuario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    USUARIOS usu = contexto.USUARIOS.FirstOrDefault(x => x.ID_USUARIO == id_usuario);
                    contexto.Eliminar(usu);
                    contexto.SaveChanges();
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
        public List<USUARIOS_PERFILES> ListaUsuarioPerfiles()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.USUARIOS_PERFILES.ToList();
            }
        }
        public void EliminaUsuarioPerfiles(List<USUARIOS_PERFILES> upModificado, List<USUARIOS_PERFILES> upOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.EliminarLista(upOriginal);
            }
        }
        public void CrearUsuarioPerfiles(USUARIOS_PERFILES upNuevo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("USUARIOS_PERFILES", upNuevo);
            }
        }
        /// <summary>
        /// Metodo que valida el login y password  de un usuario
        /// </summary>
        /// <param name="usr">login</param>
        /// <param name="pwd">clave</param>
        /// <returns>Si/No el usuario existe</returns>
        public USUARIOS ValidarUsuario(string usr, string pwd)
        {
            try
            {
                using (HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    //USUARIOS usuario = new USUARIOS(); 
                    USUARIOS usuario = (from u in contexto.USUARIOS
                                        where u.USR == usr && u.PWD == pwd && u.ESTADO == true && u.FECHA_VENCIMIENTO > DateTime.Now
                                        select u).FirstOrDefault();

                    //usuario =contexto.USUARIOS.Where(u => u.USR == usr && u.PWD == pwd).FirstOrDefault();
                    return usuario;
                }
            }
            catch (Exception ex)
            {
                return null;
                throw (ex);
            }
        }

        public USUARIOS_PERFILES perfilUsuario(int codUsuario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.USUARIOS_PERFILES
                        join u in contexto.USUARIOS on p.ID_USUARIO equals u.ID_USUARIO
                        where u.ID_USUARIO == codUsuario
                        select p).FirstOrDefault();
            }
        }

        public USUARIOS RecuperarUsuarioID(Int64 codUsuario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from u in contexto.USUARIOS
                        where u.ID_USUARIO == codUsuario
                        select u).FirstOrDefault();
            }
        }

        public List<DtoUsuarioReposion> RecuperarUsuarioActual(int id_usuario)
        {
            List<DtoUsuarioReposion> usuario = new List<DtoUsuarioReposion>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var users = (from u in db.USUARIOS
                             where u.ID_USUARIO == id_usuario
                             select u).FirstOrDefault();

                DtoUsuarioReposion dto = new DtoUsuarioReposion();
                dto.Codigo = users.ID_USUARIO.ToString();
                dto.Usuario = users.APELLIDOS + " " + users.NOMBRES;

                usuario.Add(dto);
                return usuario;
            }
        }

        public DataTable RecuperaUsuariosCajeros()
        {

            // PABLO ROCHA / 26/04/2013

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("SP_RetornaCajero", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            if (Dts.Rows.Count > 0)
            {
                return Dts;
            }
            else
            {
                return Dts;
            }


        }

        public DataTable NickName()   //Recupera todo los usuarios
        {
            SqlCommand command;
            SqlConnection connection;
            DataTable Tabla = new DataTable();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select USR from USUARIOS", connection);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public DEPARTAMENTOS ConsultaDepartamento(int dep)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return db.DEPARTAMENTOS.FirstOrDefault(x => x.DEP_CODIGO == dep);
            }
        }
        public ARE_ASIGNADA ConsultaArea(int area)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return db.ARE_ASIGNADA.FirstOrDefault(x => x.AS_ID == area);
            }
        }
        public int ConsultaUsuario(string cedula)
        {
            USUARIOS obj = new USUARIOS();
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    obj = (from u in contexto.USUARIOS
                           where u.IDENTIFICACION == cedula
                           select u).FirstOrDefault();
                    if (obj != null)
                    {
                        return 1;
                    }
                    else
                        return 0;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable ConsultaUsuarioDep(string cedula)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("select DEP_CODIGO from USUARIOS where IDENTIFICACION = '" + cedula + "'", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 280;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }
        public USUARIOS ConsultaUsuarioDepModelo(string cedula)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from u in contexto.USUARIOS
                        where u.IDENTIFICACION == cedula
                        select u).FirstOrDefault();
                //return contexto.USUARIOS.FirstOrDefault(usu => usu.ID_USUARIO == codusu);
            }
        }
        public DataTable ULtimoCodigo()   //Recupera todo los usuarios
        {
            SqlCommand command;
            SqlConnection connection;
            DataTable Tabla = new DataTable();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select ID_USUARIO from USUARIOS order by ID_USUARIO desc", connection);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }
        public bool CrearPerfilHis(int ID_PERFIL, int ID_USUARIO)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();
                command = new SqlCommand("INSERT INTO USUARIOS_PERFILES (ID_PERFIL,ID_USUARIO,ID_USUARIOS_PERFILES) VALUES(@ID_PERFIL,@ID_USUARIO,0)", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@ID_PERFIL", ID_PERFIL);
                command.Parameters.AddWithValue("@ID_USUARIO", ID_USUARIO);
                command.CommandTimeout = 380;
                reader = command.ExecuteReader();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<object> RecuperaListaUsuario()
        {

            List<object> usuariogrid = new List<object>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var usuariosConDireccion = contexto.USUARIOS.ToList().Select(u => new{Usuario = u,DireccionConvertida = int.TryParse(u.DIRECCION, out int dir) ? dir : 1}).ToList();

                var resultado = from u in usuariosConDireccion
                                join up in contexto.USUARIOS_PERFILES on u.Usuario.ID_USUARIO equals up.ID_USUARIO into userProfiles
                                from userProfile in userProfiles.DefaultIfEmpty()
                                join p in contexto.PERFILES on userProfile?.ID_PERFIL equals p.ID_PERFIL into profiles
                                from profile in profiles.DefaultIfEmpty()
                                join d in contexto.DEPARTAMENTOS on u.Usuario.DEPARTAMENTOS.DEP_CODIGO equals d.DEP_CODIGO
                                select new
                                {
                                    CODIGO = u.Usuario.ID_USUARIO,
                                    NOMBRE = u.Usuario.APELLIDOS + " " + u.Usuario.NOMBRES,
                                    u.Usuario.IDENTIFICACION,
                                    DEPARTAMENTO = d.DEP_NOMBRE,
                                    AREA_ASIGNADA = (from a in contexto.ARE_ASIGNADA
                                                     where a.AS_ID == u.DireccionConvertida
                                                     select a.AS_DESCRIPCION).FirstOrDefault(),
                                    PERFIL = profile != null ? profile.DESCRIPCION : "SIN PERFIL",
                                    ESTADO = u.Usuario.ESTADO,
                                    u.Usuario.FECHA_VENCIMIENTO
                                };

                usuariogrid = resultado.Cast<object>().ToList();
                return usuariogrid;
            }
        }
        #region Usuarios Sic-300
        public DataTable PerfilesSic()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                Sqlcmd = new SqlCommand("select codgru as 'ID', desgru AS 'PERFIL' from Sic3000..SeguridadGrupo", Sqlcon);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts);
                Sqlcon.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }
        public DataTable UsuariosSic()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("select  codusu as 'ID', APELLIDOS +' '+NOMBRES as 'USUARIO', nomusu as 'USR', CEDULA AS 'IDENTIFICACION' from Sic3000..SeguridadUsuario", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            try
            {
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts);
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }
        public List<DtoModulo> ModuloSic()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            List<DtoModulo> mod = new List<DtoModulo>();
            Sqlcmd = new SqlCommand("select codmod as 'ID', nommod as 'MODULO' from Sic3000..SeguridadesModulo where estmod = 1 ", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            try
            {
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts);
                Sqlcon.Close();
                DtoModulo dto = new DtoModulo();
                foreach (DataRow row in Dts.Rows)
                {
                    mod.Add(new DtoModulo() { ID = Convert.ToInt32(row["ID"].ToString()), MODULO = row["MODULO"].ToString(), TODO = false });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mod;

        }
        public List<DtoAccesosSic> BuscaPerfilesSic(Int64 codmod, Int64 id_usu)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Listado = new DataTable();
            DataTable Accesos = new DataTable();
            List<DtoAccesosSic> AccSic = new List<DtoAccesosSic>();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                Sqlcmd = new SqlCommand("declare @tieneAcceso bit \r\n set @tieneAcceso = 0 \r\n select codopc as 'ID', nomopc as 'ACESO', @tieneAcceso as 'ESTADO' from Sic3000..SeguridadOpciones where codmod = @codmod and estopc = 1 order by codopc", Sqlcon);
                Sqlcmd.Parameters.AddWithValue("@codmod", codmod);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Listado);

                //Sqlcmd = new SqlCommand("select so.codopc,so.nomopc,uo.staopc from Sic3000..SeguridadUsuarioOpciones uo \n" +//Cambio por modelo a Perfiles Mario 12/04/2023
                //"inner join Sic3000..SeguridadOpciones so on uo.codopc = so.codopc \n" +
                //"inner join Sic3000..SeguridadesModulo sm on so.codmod = sm.codmod \n" +
                //"where uo.codusu = @id_usu and sm.codmod = @codmod and uo.staopc = 'S'order by so.codopc", Sqlcon);
                Sqlcmd = new SqlCommand("select gr.codopc,gr.staopc from Sic3000..SeguridadGrupo g \n" +
                "inner join Sic3000..SeguridadGrupoOpciones gr on g.codgru = gr.codgru \n" +
                "inner join Sic3000..SeguridadOpciones o on gr.codopc = o.codopc \n" +
                "inner join Sic3000..SeguridadesModulo m on o.codmod = m.codmod \n" +
                "where g.codgru = @id_usu and m.codmod = @codmod and gr.staopc = 'S' and o.estopc = 1 order by gr.codgru ", Sqlcon);
                Sqlcmd.Parameters.AddWithValue("@codmod", codmod);
                Sqlcmd.Parameters.AddWithValue("@id_usu", id_usu);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Accesos);
                int i = 0;
                foreach (DataRow row in Listado.Rows)
                {
                    try
                    {
                        if (row[0].ToString() == Accesos.Rows[i][0].ToString())
                        {
                            AccSic.Add(new DtoAccesosSic() { ID = Convert.ToInt32(row[0].ToString()), ACCESO = row[1].ToString(), TIENE_ACCESO = true });
                            i++;
                        }
                        else
                            AccSic.Add(new DtoAccesosSic() { ID = Convert.ToInt32(row[0].ToString()), ACCESO = row[1].ToString(), TIENE_ACCESO = false });
                    }
                    catch (Exception)
                    {
                        AccSic.Add(new DtoAccesosSic() { ID = Convert.ToInt32(row[0].ToString()), ACCESO = row[1].ToString(), TIENE_ACCESO = false });
                        //throw;
                    }

                }
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return AccSic;

        }
        public List<DtoAccesosSic> BuscaConsidenciasSic(double codusu)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            DataTable Opciones = new DataTable();
            List<DtoAccesosSic> AccSic = new List<DtoAccesosSic>();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                //Sqlcmd = new SqlCommand("select * from Sic3000..SeguridadOpciones where codopc in (select codopc from Sic3000..SeguridadUsuarioOpciones where codusu = @codusu and staopc ='S') order by codopc", Sqlcon);
                Sqlcmd = new SqlCommand("select * from Sic3000..SeguridadesUsuarioGrupo where codusu = " + codusu + " and staopc = 1  order by codgru", Sqlcon);
                //Sqlcmd.Parameters.AddWithValue("@codusu", codusu);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts);

                Sqlcmd = new SqlCommand("select codgru,desgru from Sic3000..SeguridadGrupo order by coddep", Sqlcon);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Opciones);

                int i = 0;
                foreach (DataRow row in Opciones.Rows)
                {

                    try
                    {
                        if (row[0].ToString() == Dts.Rows[i][2].ToString())
                        {
                            AccSic.Add(new DtoAccesosSic() { ID = Convert.ToInt32(row[0].ToString()), ACCESO = row[1].ToString(), TIENE_ACCESO = true });
                            i++;
                        }
                        else
                            AccSic.Add(new DtoAccesosSic() { ID = Convert.ToInt32(row[0].ToString()), ACCESO = row[1].ToString(), TIENE_ACCESO = false });
                    }
                    catch (Exception)
                    {
                        AccSic.Add(new DtoAccesosSic() { ID = Convert.ToInt32(row[0].ToString()), ACCESO = row[1].ToString(), TIENE_ACCESO = false });
                        //throw;
                    }

                }

                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return AccSic;
        }

        public bool CrearPerfilSic(Int64 codusu, Int64 codopc, string staopc, string sw)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                SqlDataAdapter Sqldap;
                DataTable Dts = new DataTable();
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();

                //command = new SqlCommand("select * from Sic3000..SeguridadUsuarioOpciones where codusu = @codusu and codopc = @codopc", connection);
                command = new SqlCommand("select * from Sic3000..SeguridadGrupoOpciones where codgru = @codusu and codopc = @codopc", connection);
                command.Parameters.AddWithValue("@codusu", codusu);
                command.Parameters.AddWithValue("@codopc", codopc);
                command.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                command.CommandTimeout = 180;
                Sqldap.SelectCommand = command;
                Sqldap.Fill(Dts);

                if (Dts == null || Dts.Rows.Count == 0)
                {
                    command = new SqlCommand("EXEC EjecutarTR_Cggruopc_AUD @Parametro", connection);
                    command.CommandType = CommandType.Text;
                    _ = command.Parameters.AddWithValue("@Parametro", His.Entidades.Clases.Sesion.codUsuario);
                    reader = command.ExecuteReader();
                    connection.Close();
                    connection.Open();
                    command = new SqlCommand("INSERT INTO Sic3000..SeguridadGrupoOpciones(codopc,codgru,staopc)VALUES(@codopc,@codusu,@staopc)", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@codusu", codusu);
                    command.Parameters.AddWithValue("@codopc", codopc);
                    command.Parameters.AddWithValue("@staopc", staopc);
                    reader = command.ExecuteReader();
                    connection.Close();
                    connection.Open();
                    command = new SqlCommand("update Sic3000..SeguridadUsuarioOpciones set staopc='" + staopc + "' WHERE codusu IN (select codusu from Sic3000..SeguridadesUsuarioGrupo WHERE codgru=@codgru ) and codopc=@codopc", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@codopc", codopc);
                    command.Parameters.AddWithValue("@codgru", codusu);
                    reader = command.ExecuteReader();
                    connection.Close();


                }
                else
                {
                    command = new SqlCommand("EXEC EjecutarTR_Cggruopc_AUD @Parametro", connection);
                    command.CommandType = CommandType.Text;
                    _ = command.Parameters.AddWithValue("@Parametro", His.Entidades.Clases.Sesion.codUsuario);
                    reader = command.ExecuteReader();
                    connection.Close();
                    connection.Open();
                    command = new SqlCommand("update Sic3000..SeguridadGrupoOpciones set staopc = @staopc where codgru = @codusu and codopc = @codopc", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@codusu", codusu);
                    command.Parameters.AddWithValue("@codopc", codopc);
                    command.Parameters.AddWithValue("@staopc", staopc);
                    reader = command.ExecuteReader();
                    connection.Close();
                    connection.Open();
                    command = new SqlCommand("update Sic3000..SeguridadUsuarioOpciones set staopc='" + staopc + "' WHERE codusu IN (select codusu from Sic3000..SeguridadesUsuarioGrupo WHERE codgru=@codgru ) and codopc=@codopc", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@codopc", codopc);
                    command.Parameters.AddWithValue("@codgru", codusu);
                    reader = command.ExecuteReader();
                    connection.Close();


                }
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public bool CrearPerfilSicUsu(Int64 codusu, Int64 codgrup, bool staopc)
        {
            try
            {
                SqlCommand command;
                SqlConnection con = null;
                SqlCommand cmd = null;
                SqlConnection connection;
                SqlDataReader reader;
                SqlDataAdapter Sqldap;
                DataTable Dts = new DataTable();
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();
                command = new SqlCommand("EXEC EjecutarTR_Cggruopc_AUD @Parametro", connection);
                command.CommandType = CommandType.Text;
                _ = command.Parameters.AddWithValue("@Parametro", His.Entidades.Clases.Sesion.codUsuario);
                reader = command.ExecuteReader();
                connection.Close();
                connection.Open();



                //command = new SqlCommand("select * from Sic3000..SeguridadUsuarioOpciones where codusu = @codusu and codopc = @codopc", connection);
                command = new SqlCommand("select * from Sic3000..SeguridadesUsuarioGrupo where codusu = " + codusu, connection);
                command.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                command.CommandTimeout = 180;
                Sqldap.SelectCommand = command;
                Sqldap.Fill(Dts);

                if (Dts == null || Dts.Rows.Count == 0)
                {
                    command = new SqlCommand("EXEC EjecutarTR_Cggruopc_AUD @Parametro", connection);
                    command.CommandType = CommandType.Text;
                    _ = command.Parameters.AddWithValue("@Parametro", His.Entidades.Clases.Sesion.codUsuario);
                    reader = command.ExecuteReader();
                    connection.Close();
                    connection.Open();
                    command = new SqlCommand("INSERT INTO Sic3000..SeguridadesUsuarioGrupo(codusu,codgru,staopc)VALUES(@codusu,@codopc,@staopc)", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@codusu", codusu);
                    command.Parameters.AddWithValue("@codopc", codgrup);
                    command.Parameters.AddWithValue("@staopc", staopc);
                    reader = command.ExecuteReader();

                    con = obj.ConectarBd();
                    con.Open();
                    cmd = new SqlCommand("sp_ActualizarUsuarioGrupo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CODUSU", codusu);
                    cmd.Parameters.AddWithValue("@CODGRU", codgrup);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    command = new SqlCommand("EXEC EjecutarTR_Cggruopc_AUD @Parametro", connection);
                    command.CommandType = CommandType.Text;
                    _ = command.Parameters.AddWithValue("@Parametro", His.Entidades.Clases.Sesion.codUsuario);
                    reader = command.ExecuteReader();
                    connection.Close();
                    connection.Open();
                    //command = new SqlCommand("update Sic3000..SeguridadesUsuarioGrupo set staopc = @staopc where codusu = @codusu and coddep = @codopc", connection);
                    command = new SqlCommand("update Sic3000..SeguridadesUsuarioGrupo set codgru = " + codgrup + " where codusu = " + codusu, connection);
                    command.CommandType = CommandType.Text;
                    reader = command.ExecuteReader();
                    connection.Close();

                    con = obj.ConectarBd();
                    con.Open();
                    cmd = new SqlCommand("sp_ActualizarUsuarioGrupo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CODUSU", codusu);
                    cmd.Parameters.AddWithValue("@CODGRU", codgrup);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }


                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public bool BorrarPerfilSicUsu(Int64 codusu)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                DataTable Dts = new DataTable();
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();

                command = new SqlCommand("update Sic3000..SeguridadUsuarioOpciones set staopc = 'N' where codusu = " + codusu, connection);
                reader = command.ExecuteReader();

                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public bool ActualizarMedicoUsuario(Int64 codusu,string ruc)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                DataTable Dts = new DataTable();
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();

                command = new SqlCommand("update his3000..medicos set id_usuario =" + codusu + " where med_ruc = '" + ruc +"'", connection);
                reader = command.ExecuteReader();

                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public bool BorrarPerfilCgUsu(Int64 codusu)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                DataTable Dts = new DataTable();
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();
                command = new SqlCommand("update Cg3000..Cgopciusu set staopc = 'N' where codusu = " + codusu, connection);
                reader = command.ExecuteReader();
                connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public bool CrearPerfilCgUsu(Int64 codusu, Int64 codgru, bool staopc)
        {
            try
            {
                SqlCommand command;
                SqlConnection con = null;
                SqlCommand cmd = null;
                SqlConnection connection;
                SqlDataReader reader;
                SqlDataAdapter Sqldap;
                DataTable Dts = new DataTable();
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();

                command = new SqlCommand("select * from Cg3000..Cgusugrup where codusu = " + codusu, connection);
                command.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                command.CommandTimeout = 180;
                Sqldap.SelectCommand = command;
                Sqldap.Fill(Dts);

                if (Dts == null || Dts.Rows.Count == 0)
                {
                    command = new SqlCommand("INSERT INTO Cg3000..Cgusugrup(codusu,codgru,staopc)VALUES(@codusu,@codopc,@staopc)", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@codusu", codusu);
                    command.Parameters.AddWithValue("@codopc", codgru);
                    command.Parameters.AddWithValue("@staopc", staopc);
                    reader = command.ExecuteReader();
                    con = obj.ConectarBd();
                    con.Open();
                    cmd = new SqlCommand("sp_ActualizarUsuarioGrupoCg", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CODUSU", codusu);
                    cmd.Parameters.AddWithValue("@CODGRU", codgru);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    command = new SqlCommand("update Cg3000..Cgusugrup  set codgru = " + codgru + " where codusu = " + codusu, connection);
                    command.CommandType = CommandType.Text;
                    reader = command.ExecuteReader();
                    connection.Close();

                    con = obj.ConectarBd();
                    con.Open();
                    cmd = new SqlCommand("sp_ActualizarUsuarioGrupoCg", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CODUSU", codusu);
                    cmd.Parameters.AddWithValue("@CODGRU", codgru);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public void CrearPerfilSicSP(double codusu, double codopc, string staopc, string sw)
        {
            SqlConnection con;
            BaseContextoDatos obj = new BaseContextoDatos();
            con = obj.ConectarBd();
            con.Open();
            try
            {
                //conexion
                string sql = "sp_grabarPerfilSic";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@cod", SqlDbType.Float).Value = codusu;
                cmd.Parameters.Add("@ip", SqlDbType.Float).Value = codopc;
                cmd.Parameters.Add("@descrip", SqlDbType.NVarChar, 30).Value = staopc;
                cmd.Parameters.Add("@bodega", SqlDbType.NVarChar, 5).Value = sw;

                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw new Exception("Error al insertar Usuario");
            }
            finally
            {
                con.Close();
            }

        }

        public bool crearPerfilesSic(string grupo)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                SqlDataAdapter Sqldap;
                DataTable Dts = new DataTable();
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();

                command = new SqlCommand("select top 1 codgru from Sic3000..SeguridadGrupo order by codgru desc", connection);
                command.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                command.CommandTimeout = 180;
                Sqldap.SelectCommand = command;
                Sqldap.Fill(Dts);
                Int64 cont = Convert.ToInt64(Dts.Rows[0][0].ToString()) + 1;
                command = new SqlCommand("insert into Sic3000..SeguridadGrupo values (" + cont + "," + cont + ",@descrip)", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@descrip", grupo);
                reader = command.ExecuteReader();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw;
            }
        }
        public bool crearModuloSic(string modulo)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                SqlDataAdapter Sqldap;
                DataTable Dts = new DataTable();
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();

                command = new SqlCommand("select top 1 codmod from Sic3000..SeguridadesModulo order by codmod desc", connection);
                command.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                command.CommandTimeout = 180;
                Sqldap.SelectCommand = command;
                Sqldap.Fill(Dts);
                Int64 cont = Convert.ToInt64(Dts.Rows[0][0].ToString()) + 1;
                command = new SqlCommand("insert into Sic3000..SeguridadesModulo values (" + cont + ",'" + modulo + "',1)", connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw;
            }
        }


        public List<DtoUsuariosPerfilesSic> ListaSic()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;

            DataTable Opciones = new DataTable();
            List<DtoUsuariosPerfilesSic> perSic = new List<DtoUsuariosPerfilesSic>();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {


                Sqlcmd = new SqlCommand("select * from Sic3000..SeguridadDepartamento order by coddep", Sqlcon);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Opciones);

                int i = 0;
                foreach (DataRow row in Opciones.Rows)
                {

                    try
                    {

                        perSic.Add(new DtoUsuariosPerfilesSic() { ID_PERFIL = Convert.ToInt32(row[0].ToString()), DESCRIPCION = row[1].ToString(), TIENEACCESO = false });
                    }
                    catch (Exception ex)
                    {
                        //perSic.Add(new DtoAccesosSic() { ID = Convert.ToInt32(row[0].ToString()), ACCESO = row[1].ToString(), TIENE_ACCESO = false });
                        Console.WriteLine(ex.Message);
                    }

                }

                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return perSic;
        }
        public DataTable opcionesXModuloSic(Int64 codmod)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            try
            {
                command = new SqlCommand("select * from Sic3000..SeguridadOpciones where codmod = " + codmod + " and estopc = 1", connection);
                command.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                command.CommandTimeout = 180;
                Sqldap.SelectCommand = command;
                Sqldap.Fill(Dts);
                connection.Close();
                return Dts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                connection.Close();
                return Dts;
            }

        }
        public DataTable usuarioXCedulaSic(string cedula)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            try
            {
                command = new SqlCommand("select * from Sic3000..SeguridadUsuario where CEDULA = '" + cedula + "'", connection);
                command.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                command.CommandTimeout = 180;
                Sqldap.SelectCommand = command;
                Sqldap.Fill(Dts);
                connection.Close();
                return Dts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                connection.Close();
                return Dts;
            }
        }
        #endregion
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        #region Usuarios Cg-300
        public DataTable PerfilesCg()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                Sqlcmd = new SqlCommand("select codgru as 'ID', desgru AS 'PERFIL' from Cg3000..Cggrupusu", Sqlcon);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts);
                Sqlcon.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }
        public DataTable UsuariosCG()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("select  codusu as 'ID', APELLIDOS +' '+NOMBRES as 'USUARIO', nomusu as 'USR', CEDULA AS 'IDENTIFICACION' from Cg3000..Cgusuario order by 2", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            try
            {
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts);
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }
        public List<DtoModulo> ModuloCG()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            List<DtoModulo> mod = new List<DtoModulo>();
            Sqlcmd = new SqlCommand("select codmod as 'ID', nommod as 'MODULO' from Cg3000..cgmodulo where estmod = 1 ", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            try
            {
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts);
                Sqlcon.Close();
                DtoModulo dto = new DtoModulo();
                foreach (DataRow row in Dts.Rows)
                {
                    mod.Add(new DtoModulo() { ID = Convert.ToInt32(row["ID"].ToString()), MODULO = row["MODULO"].ToString(), TODO = false });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mod;
        }
        public List<DtoAccesosSic> BuscaPerfilesCg(Int64 codmod, Int64 id_usu)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Listado = new DataTable();
            DataTable Accesos = new DataTable();
            List<DtoAccesosSic> AccSic = new List<DtoAccesosSic>();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                Sqlcmd = new SqlCommand(" select codopc as 'ID', nomopc as 'ACESO' from Cg3000..Cgopcion where codmod = @codmod and estopc = 1 order by codopc", Sqlcon);
                Sqlcmd.Parameters.AddWithValue("@codmod", codmod);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Listado);

                //Sqlcmd = new SqlCommand("select ou.codopc,o.nomopc,ou.staopc from Cg3000..Cgopciusu ou \n" + //por cambio a manejo por perfiles // Mario 13-04-2023
                //"inner join Cg3000..Cgopcion o on ou.codopc = o.codopc \n" +
                //"inner join Cg3000..cgmodulo m on o.codmod = m.codmod \n" +
                //"where ou.codusu = @id_usu and m.codmod = @codmod and ou.staopc = 'S' order by ou.codopc", Sqlcon);
                Sqlcmd = new SqlCommand("select o.codopc,o.nomopc,gro.staopc from Cg3000..Cggrupusu g \n" +
                "inner join Cg3000..Cggruopc gro on g.codgru = gro.codgru \n" +
                "inner join Cg3000..Cgopcion o on gro.codopc = o.codopc \n" +
                "inner join Cg3000..cgmodulo m on o.codmod = m.codmod \n" +
                "where g.codgru = @id_usu and m.codmod = @codmod and gro.staopc = 'S' order by o.codopc", Sqlcon);
                Sqlcmd.Parameters.AddWithValue("@codmod", codmod);
                Sqlcmd.Parameters.AddWithValue("@id_usu", id_usu);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Accesos);
                int i = 0;
                foreach (DataRow row in Listado.Rows)
                {
                    try
                    {
                        if (row[0].ToString() == Accesos.Rows[i][0].ToString())
                        {
                            AccSic.Add(new DtoAccesosSic() { ID = Convert.ToInt32(row[0].ToString()), ACCESO = row[1].ToString(), TIENE_ACCESO = true });
                            i++;
                        }
                        else
                            AccSic.Add(new DtoAccesosSic() { ID = Convert.ToInt32(row[0].ToString()), ACCESO = row[1].ToString(), TIENE_ACCESO = false });
                    }
                    catch (Exception)
                    {
                        AccSic.Add(new DtoAccesosSic() { ID = Convert.ToInt32(row[0].ToString()), ACCESO = row[1].ToString(), TIENE_ACCESO = false });
                        //throw;
                    }

                }
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return AccSic;

        }
        //public DataTable BuscaPerfilesCG()
        //{
        //    SqlConnection Sqlcon;
        //    SqlCommand Sqlcmd;
        //    SqlDataAdapter Sqldap;
        //    DataTable Dts = new DataTable();
        //    BaseContextoDatos obj = new BaseContextoDatos();
        //    Sqlcon = obj.ConectarBd();
        //    try
        //    {
        //        Sqlcon.Open();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    Sqlcmd = new SqlCommand("select codopc,nomopc,UPPER(LEFT(codopc,1))as sw from Cg3000..Cgopcion order by nomopc", Sqlcon);
        //    Sqlcmd.CommandType = CommandType.Text;
        //    Sqldap = new SqlDataAdapter();
        //    Sqlcmd.CommandTimeout = 180;
        //    Sqldap.SelectCommand = Sqlcmd;
        //    Sqldap.Fill(Dts);
        //    try
        //    {
        //        Sqlcon.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return Dts;
        //}
        public List<DtoAccesosSic> BuscaConsidenciasCG(double codusu)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            DataTable Opciones = new DataTable();
            List<DtoAccesosSic> AccCg = new List<DtoAccesosSic>();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                //Sqlcmd = new SqlCommand("select * from Cg3000..Cgopcion where codopc in(select codopc from Cg3000..Cgopciusu where codusu = @codusu and staopc = 'S') order by nomopc", Sqlcon);
                Sqlcmd = new SqlCommand("select * from Cg3000..Cgusugrup where codusu = " + codusu + " and staopc = 1 order by codgru ", Sqlcon);
                //Sqlcmd.Parameters.AddWithValue("@codusu", codusu);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts);

                Sqlcmd = new SqlCommand("select * from Cg3000..Cggrupusu order by codgru", Sqlcon);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Opciones);

                int i = 0;
                foreach (DataRow row in Opciones.Rows)
                {

                    try
                    {
                        if (row[0].ToString() == Dts.Rows[i][2].ToString())
                        {
                            AccCg.Add(new DtoAccesosSic() { ID = Convert.ToInt32(row[0].ToString()), ACCESO = row[2].ToString(), TIENE_ACCESO = true });
                            i++;
                        }
                        else
                            AccCg.Add(new DtoAccesosSic() { ID = Convert.ToInt32(row[0].ToString()), ACCESO = row[2].ToString(), TIENE_ACCESO = false });
                    }
                    catch (Exception)
                    {
                        AccCg.Add(new DtoAccesosSic() { ID = Convert.ToInt32(row[0].ToString()), ACCESO = row[2].ToString(), TIENE_ACCESO = false });
                        //throw;
                    }

                }

                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return AccCg;
        }
        public bool CrearPerfilCg(Int64 codusu, Int64 codopc, string staopc, string sw)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                SqlDataAdapter Sqldap;
                DataTable Dts = new DataTable();
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();
                command = new SqlCommand("select * from Cg3000..Cggruopc where codgru = @codusu and codopc = @codopc", connection);
                command.Parameters.AddWithValue("@codusu", codusu);
                command.Parameters.AddWithValue("@codopc", codopc);
                command.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                command.CommandTimeout = 180;
                Sqldap.SelectCommand = command;
                Sqldap.Fill(Dts);

                if (Dts == null || Dts.Rows.Count == 0)
                {
                    command = new SqlCommand("EXEC EjecutarTR_Cggruopc_AUD @Parametro", connection);
                    command.CommandType = CommandType.Text;
                    _ = command.Parameters.AddWithValue("@Parametro", His.Entidades.Clases.Sesion.codUsuario);
                    reader = command.ExecuteReader();
                    connection.Close();
                    connection.Open();
                    command = new SqlCommand("INSERT INTO Cg3000..Cggruopc(codopc,codgru,staopc)VALUES(@codopc,@codusu,@staopc)", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@codusu", codusu);
                    command.Parameters.AddWithValue("@codopc", codopc);
                    command.Parameters.AddWithValue("@staopc", staopc);
                    command.Parameters.AddWithValue("@sw", sw);
                    reader = command.ExecuteReader();
                    connection.Close();
                    connection.Open();
                    command = new SqlCommand("update Cg3000..Cgopciusu set staopc='" + staopc + "' WHERE codusu IN (select codusu from Cg3000..Cgusugrup WHERE codgru=@codgru ) and codopc=@codopc", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@codopc", codopc);
                    command.Parameters.AddWithValue("@codgru", codusu);
                    reader = command.ExecuteReader();
                    connection.Close();
                }
                else
                {
                    command = new SqlCommand("EXEC EjecutarTR_Cggruopc_AUD @Parametro", connection);
                    command.CommandType = CommandType.Text;
                    _ = command.Parameters.AddWithValue("@Parametro", His.Entidades.Clases.Sesion.codUsuario);
                    reader = command.ExecuteReader();
                    connection.Close();
                    connection.Open();
                    command = new SqlCommand("update Cg3000..Cggruopc set staopc = @staopc where codgru = @codusu and codopc = @codopc", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@codusu", codusu);
                    command.Parameters.AddWithValue("@codopc", codopc);
                    command.Parameters.AddWithValue("@staopc", staopc);
                    reader = command.ExecuteReader();
                    connection.Close();

                    connection.Open();
                    command = new SqlCommand("update Cg3000..Cgopciusu set staopc='" + staopc + "' WHERE codusu IN (select codusu from Cg3000..Cgusugrup WHERE codgru=@codgru ) and codopc=@codopc", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@codopc", codopc);
                    command.Parameters.AddWithValue("@codgru", codusu);
                    reader = command.ExecuteReader();
                    connection.Close();

                }
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public void crearPerfilesCG(double codusu)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("delete from Cg3000..Cgopciusu where codusu=@descrip", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@descrip", codusu);
            reader = command.ExecuteReader();
        }
        public bool crearPerfilesCg(string grupo)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                SqlDataAdapter Sqldap;
                DataTable Dts = new DataTable();
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();

                command = new SqlCommand("select top 1 codgru from Cg3000..Cggrupusu order by codgru desc", connection);
                command.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                command.CommandTimeout = 180;
                Sqldap.SelectCommand = command;
                Sqldap.Fill(Dts);
                Int64 cont = Convert.ToInt64(Dts.Rows[0][0].ToString()) + 1;
                command = new SqlCommand("insert into Cg3000..Cggrupusu values (" + cont + "," + cont + ",@descrip)", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@descrip", grupo);
                reader = command.ExecuteReader();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw;
            }
        }
        public bool editarPerfilCg(Int64 codgru, string desgru)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();
                command = new SqlCommand("update Cg3000..Cggrupusu set desgru = '" + desgru + "' where codgru = " + codgru, connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw;
            }
        }
        public bool eliminarPerfilCg(Int64 codgru)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();
                command = new SqlCommand("update Cg3000..Cgopciusu set staopc = 'N' where codusu in(select codusu from Cg3000..Cgusugrup where codgru = " + codgru + " )", connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                connection.Open();
                command = new SqlCommand("delete from Cg3000..Cgusugrup where codgru = " + codgru, connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                connection.Open();
                command = new SqlCommand("delete from Cg3000..Cggruopc where codgru =" + codgru, connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                connection.Open();
                command = new SqlCommand("delete from Cg3000..Cggrupusu where codgru =" + codgru, connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw;
            }
        }
        public bool crearModuloCg(string modulo)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                SqlDataAdapter Sqldap;
                DataTable Dts = new DataTable();
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();

                command = new SqlCommand("select top 1 codmod from Cg3000..cgmodulo order by codmod desc", connection);
                command.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                command.CommandTimeout = 180;
                Sqldap.SelectCommand = command;
                Sqldap.Fill(Dts);
                Int64 cont = Convert.ToInt64(Dts.Rows[0][0].ToString()) + 1;
                command = new SqlCommand("insert into Cg3000..cgmodulo values (" + cont + ",'" + modulo + "',1)", connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw;
            }
        }
        public bool editarModuloCg(Int64 codmod, string nommod)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();
                command = new SqlCommand("update Cg3000..cgmodulo set nommod = '" + nommod + "' where codmod = " + codmod, connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw;
            }
        }
        public bool eliminarModuloCg(Int64 codmod)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();
                command = new SqlCommand("delete from Cg3000..Cggruopc where codopc in (select codopc from Cg3000..Cgopcion where codmod = " + codmod + ")", connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                connection.Open();
                command = new SqlCommand("delete from Cg3000..Cgopciusu where codopc in (select codopc from Cg3000..Cgopcion where codmod = " + codmod + ")", connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                connection.Open();
                command = new SqlCommand("delete from Cg3000..Cgopcion where  codmod = " + codmod, connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                connection.Open();
                command = new SqlCommand("delete from Cg3000..cgmodulo where codmod = " + codmod, connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw;
            }
        }
        public DataTable opcionesXModuloCg(Int64 codmod)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            try
            {
                command = new SqlCommand("select * from Cg3000..Cgopcion where codmod = " + codmod + " and estopc = 1", connection);
                command.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                command.CommandTimeout = 180;
                Sqldap.SelectCommand = command;
                Sqldap.Fill(Dts);
                connection.Close();
                return Dts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                connection.Close();
                return Dts;
            }

        }
        public DataTable usuarioXCedulaCg(string cedula)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            try
            {
                command = new SqlCommand("select * from Cg3000..Cgusuario where cedula = '" + cedula + "'", connection);
                command.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                command.CommandTimeout = 180;
                Sqldap.SelectCommand = command;
                Sqldap.Fill(Dts);
                connection.Close();
                return Dts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                connection.Close();
                return Dts;
            }
        }
        #endregion
    }
}
