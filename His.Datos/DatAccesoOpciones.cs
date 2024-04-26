using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using System.Data.SqlClient;
using Core.Entidades;
using System.Data;
using System.Data.Common;

namespace His.Datos
{
    public class DatAccesoOpciones
    {
        public int RecuperaMaximoAccesoOpciones(Int16 modulo)
        {
            int maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ACCESO_OPCIONES> ac = new List<ACCESO_OPCIONES>();
                ac = contexto.ACCESO_OPCIONES.Where(i => i.MODULO.ID_MODULO == modulo).ToList();
                if (ac.Count > 0)
                    maxim = contexto.ACCESO_OPCIONES.Include("MODULO").Where(i => i.MODULO.ID_MODULO == modulo).Max(emp => emp.ID_ACCESO);
                else
                    maxim = 0;
                return maxim;
            }
        }
        public List<DtoAccesoOpciones> RecuperaAccesoOpciones()
        {
            List<DtoAccesoOpciones> accesogrid = new List<DtoAccesoOpciones>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ACCESO_OPCIONES> accesoopciones = new List<ACCESO_OPCIONES>();
                accesoopciones = contexto.ACCESO_OPCIONES.Include("MODULO").ToList();
                foreach (var acceso in accesoopciones)
                {
                    accesogrid.Add(new DtoAccesoOpciones()
                    {
                        ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName(),
                        ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key,
                        ID_ACCESO = acceso.ID_ACCESO,
                        DESCRIPCION = acceso.DESCRIPCION,
                        ID_MODULO = acceso.MODULO.ID_MODULO,
                        DESCRIPCIONMod = acceso.MODULO.DESCRIPCION

                    });
                }
                return accesogrid;
            }
        }
        public void CrearAccesoOpciones(ACCESO_OPCIONES accOp)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("ACCESO_OPCIONES", accOp);
            }
        }
        public void GrabarAccesoOpciones(ACCESO_OPCIONES accOpModificada, ACCESO_OPCIONES accOpOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                contexto.Grabar(accOpModificada, accOpOriginal);
            }
        }
        public void EliminarAccesoOpciones(ACCESO_OPCIONES accOp)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(accOp);
            }
        }
        public List<ACCESO_OPCIONES> ListaAccesoOpciones()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.ACCESO_OPCIONES.ToList();
            }
        }
        public ACCESO_OPCIONES RecuperaAccesosOpciones(Int64 acceso)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from a in contexto.ACCESO_OPCIONES
                        where a.ID_ACCESO == acceso
                        select a).FirstOrDefault();
            }
        }
        /// <summary>
        /// Metodo que recupera el listado de accesos por perfil y modulo
        /// </summary>
        /// <param name="codigoPerfil">Codigo del perfil</param>
        /// <param name="codigoModulo">Codigo del modulo</param>
        /// <returns>Retorna ul listado de Acceso_opciones</returns>
        public List<ACCESO_OPCIONES> ListaAccesoOpcionesPorPerfil(Int32 codigoPerfil, Int32 codigoModulo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ACCESO_OPCIONES> accesoOpcionesLista = (from p in contexto.PERFILES_ACCESOS
                                                             join a in contexto.ACCESO_OPCIONES on p.ID_ACCESO equals a.ID_ACCESO
                                                             where p.ID_PERFIL == codigoPerfil && a.MODULO.ID_MODULO == codigoModulo
                                                             select a).ToList();
                return accesoOpcionesLista;
            }
        }

        public bool ParametroBodega()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool bodega = false;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("SELECT PAD_ACTIVO FROM PARAMETROS_DETALLE WHERE PAD_CODIGO = 24", connection);
            command.CommandType = System.Data.CommandType.Text;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                bodega = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            reader.Close();
            connection.Close();
            return bodega;
        }
        public List<DtopAccesosOpciones> ListarAccesoOpcionesXmodulo(Int64 id_modulo, Int64 id_perfil)
        {

            List<DtopAccesosOpciones> AccOpc = new List<DtopAccesosOpciones>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var acceso = (from a in db.ACCESO_OPCIONES
                              where a.MODULO.ID_MODULO == id_modulo
                              select a).OrderBy(x => x.ID_ACCESO).ToList();

                var estados = (from p in db.PERFILES
                               join pa in db.PERFILES_ACCESOS on p.ID_PERFIL equals pa.PERFILES.ID_PERFIL
                               join ac in db.ACCESO_OPCIONES on pa.ACCESO_OPCIONES.ID_ACCESO equals ac.ID_ACCESO
                               join m in db.MODULO on ac.MODULO.ID_MODULO equals m.ID_MODULO
                               where m.ID_MODULO == id_modulo && p.ID_PERFIL == id_perfil
                               select new { p, pa, ac, m }).OrderBy(x => x.ac.ID_ACCESO).ToList();

                DataTable tblEstados = new DataTable();
                DataColumn dc = new DataColumn();
                dc.ColumnName = "ID_ACCESO";
                dc.DataType = typeof(double);
                tblEstados.Columns.AddRange(new DataColumn[] { dc });

                foreach (var est in estados)
                {
                    tblEstados.Rows.Add(new object[] { est.ac.ID_ACCESO });
                }
                int i = 0;
                foreach (var item in acceso)
                {
                    try
                    {
                        if (item.ID_ACCESO == Convert.ToInt64(tblEstados.Rows[i][0].ToString()))
                        {
                            AccOpc.Add(new DtopAccesosOpciones() { ID = item.ID_ACCESO, ACCESO = item.DESCRIPCION, TIPO = item.TIPO, TIENE_ACCESO = true });
                            i++;
                        }
                        else
                            AccOpc.Add(new DtopAccesosOpciones() { ID = item.ID_ACCESO, ACCESO = item.DESCRIPCION, TIPO = item.TIPO, TIENE_ACCESO = false });

                    }
                    catch (Exception ex)
                    {
                        AccOpc.Add(new DtopAccesosOpciones() { ID = item.ID_ACCESO, ACCESO = item.DESCRIPCION, TIPO = item.TIPO, TIENE_ACCESO = false });
                        //throw;
                    }
                }
            }
            return AccOpc;
        }
        public List<ACCESO_OPCIONES> accesosXModulo(Int32 id_modulo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from a in db.ACCESO_OPCIONES
                        where a.MODULO.ID_MODULO == id_modulo
                        select a).OrderBy(x => x.ID_ACCESO).ToList();
            }
        }
        public List<ACCESO_OPCIONES> accesosXPerfil(Int32 id_modulo, Int64 id_perfil)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in db.PERFILES
                        join pa in db.PERFILES_ACCESOS on p.ID_PERFIL equals pa.PERFILES.ID_PERFIL
                        join ac in db.ACCESO_OPCIONES on pa.ACCESO_OPCIONES.ID_ACCESO equals ac.ID_ACCESO
                        join m in db.MODULO on ac.MODULO.ID_MODULO equals m.ID_MODULO
                        where m.ID_MODULO == id_modulo && p.ID_PERFIL == id_perfil
                        select ac).OrderBy(x => x.ID_ACCESO).ToList();
            }
        }
        public DataTable accesosXModuloSic(Int32 id_modulo)
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

            Sqlcmd = new SqlCommand("select * from Sic3000..SeguridadOpciones where codmod = "+id_modulo+" and estopc = 1 ", Sqlcon);
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
        public DataTable accesosXPerfilSic(Int32 id_modulo, Int64 id_perfil)
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

            Sqlcmd = new SqlCommand("select * from Sic3000..SeguridadGrupoOpciones sgo inner join Sic3000..SeguridadOpciones \n" +
                " so on sgo.codopc = so.codopc where codmod = "+id_modulo+" and estopc = 1  and codgru = "+id_perfil+" and staopc = 'S'", Sqlcon);
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
        public DataTable accesosXModuloCg(Int32 id_modulo)
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

            Sqlcmd = new SqlCommand("select * from Cg3000..Cgopcion where codmod = " + id_modulo + " and estopc = 1 ", Sqlcon);
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
        public DataTable accesosXPerfilCg(Int32 id_modulo, Int64 id_perfil)
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

            Sqlcmd = new SqlCommand("select * from Cg3000..Cggruopc gop inner join  Cg3000..Cgopcion op on gop.codopc = op.codopc \n" +
                " where codmod = " + id_modulo + " and estopc = 1  and codgru = " + id_perfil + " and staopc = 'S'", Sqlcon);
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
        public DataTable ExploradorUsrAccSic()
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

            Sqlcmd = new SqlCommand("select u.codusu as 'ID',u.apellidos+' '+u.nombres as 'USUARIO',(SELECT DEP_NOMBRE FROM His3000..DEPARTAMENTOS D where d.DEP_CODIGO = u.coddep) as 'DEPARTAMENTO',u.nomusu AS 'USU',m.nommod AS 'MODULO',o.codopc AS 'ID ACCESO',o.nomopc AS 'ACCESO',uo.staopc AS 'ESTADO',u.feccad as 'FECHA CADUCIDAD' from Sic3000..SeguridadUsuario u \n" +
                "inner join Sic3000..SeguridadUsuarioOpciones uo on u.codusu = uo.codusu \n" +
                "inner join Sic3000..SeguridadOpciones o on uo.codopc = o.codopc \n" +
                "inner join Sic3000..SeguridadesModulo m on o.codmod = m.codmod \n" +
                "where m.estmod = 1 and estopc = 1 AND feccad >= GETDATE()", Sqlcon);
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
        public DataTable ExploradorUsrInacSic()
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

            Sqlcmd = new SqlCommand("select u.codusu as 'ID',u.apellidos+' '+u.nombres as 'USUARIO',(SELECT DEP_NOMBRE FROM His3000..DEPARTAMENTOS D where d.DEP_CODIGO = u.coddep) as 'DEPARTAMENTO',u.nomusu AS 'USU',m.nommod AS 'MODULO',o.codopc AS 'ID ACCESO',o.nomopc AS 'ACCESO',uo.staopc AS 'ESTADO',u.feccad as 'FECHA CADUCIDAD' from Sic3000..SeguridadUsuario u \n" +
                "inner join Sic3000..SeguridadUsuarioOpciones uo on u.codusu = uo.codusu \n" +
                "inner join Sic3000..SeguridadOpciones o on uo.codopc = o.codopc \n" +
                "inner join Sic3000..SeguridadesModulo m on o.codmod = m.codmod \n" +
                "where m.estmod = 1 and estopc = 1 AND feccad < GETDATE()", Sqlcon);
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
        public DataTable ExploradorUsrAccCG()
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

            Sqlcmd = new SqlCommand("select  u.codusu as 'ID',u.apellidos+' '+u.nombres as 'USUARIO',u.nomusu AS 'USU',(SELECT DEP_NOMBRE FROM His3000..DEPARTAMENTOS D where d.DEP_CODIGO = u.coddep) as 'DEPARTAMENTO',m.nommod AS 'MODULO',o.codopc AS 'ID ACCESO',o.nomopc AS 'ACCESO',uo.staopc AS 'ESTADO',u.feccad as 'FECHA CADUCIDAD' from Cg3000..Cgusuario u  \n" +
                "inner join Cg3000..Cgopciusu uo on u.codusu = uo.codusu \n" +
                "inner join Cg3000..Cgopcion o on uo.codopc = o.codopc \n" +
                "inner join Cg3000..cgmodulo m on o.codmod = m.codmod \n" +
                "where m.estmod = 1 and estopc = 1 AND feccad >= GETDATE()", Sqlcon);
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
        public DataTable ExploradorUsrInacCG()
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

            Sqlcmd = new SqlCommand("select  u.codusu as 'ID',u.apellidos+' '+u.nombres as 'USUARIO',u.nomusu AS 'USU',(SELECT DEP_NOMBRE FROM His3000..DEPARTAMENTOS D where d.DEP_CODIGO = u.coddep) as 'DEPARTAMENTO',m.nommod AS 'MODULO',o.codopc AS 'ID ACCESO',o.nomopc AS 'ACCESO',uo.staopc AS 'ESTADO',u.feccad as 'FECHA CADUCIDAD' from Cg3000..Cgusuario u  \n" +
                "inner join Cg3000..Cgopciusu uo on u.codusu = uo.codusu \n" +
                "inner join Cg3000..Cgopcion o on uo.codopc = o.codopc \n" +
                "inner join Cg3000..cgmodulo m on o.codmod = m.codmod \n" +
                "where m.estmod = 1 and estopc = 1 AND feccad < GETDATE()", Sqlcon);
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
        public List<DtoExplotadorUsuariosHis> ExploradorUsrAccHis()
        {
            List<DtoExplotadorUsuariosHis> usr = new List<DtoExplotadorUsuariosHis>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var ex = (from u in db.USUARIOS
                          join up in db.USUARIOS_PERFILES on u.ID_USUARIO equals up.ID_USUARIO
                          join p in db.PERFILES on up.ID_PERFIL equals p.ID_PERFIL
                          join pa in db.PERFILES_ACCESOS on p.ID_PERFIL equals pa.ID_PERFIL
                          join ao in db.ACCESO_OPCIONES on pa.ID_ACCESO equals ao.ID_ACCESO
                          join m in db.MODULO on ao.MODULO.ID_MODULO equals m.ID_MODULO
                          where u.FECHA_VENCIMIENTO >= DateTime.Now
                          select new
                          {
                              u,
                              p,
                              m,
                              ao
                          }).ToList();
                foreach (var item in ex)
                {
                    usr.Add(new DtoExplotadorUsuariosHis() { ID = item.u.ID_USUARIO, USUARIO = item.u.APELLIDOS + " " + item.u.NOMBRES, USR = item.u.USR, PERFIL = item.p.DESCRIPCION, MODULO = item.m.DESCRIPCION, ID_ACCESO = item.ao.ID_ACCESO, ACCESO = item.ao.DESCRIPCION, ESTADO = item.u.ESTADO, FECHA_CADUCIDAD = (DateTime)item.u.FECHA_VENCIMIENTO });
                }
            }
            return usr;
        }
        public List<DtoExplotadorUsuariosHis> ExploradorUsrInacHis()
        {
            List<DtoExplotadorUsuariosHis> usr = new List<DtoExplotadorUsuariosHis>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var ex = (from u in db.USUARIOS
                          join up in db.USUARIOS_PERFILES on u.ID_USUARIO equals up.ID_USUARIO
                          join p in db.PERFILES on up.ID_PERFIL equals p.ID_PERFIL
                          join pa in db.PERFILES_ACCESOS on p.ID_PERFIL equals pa.ID_PERFIL
                          join ao in db.ACCESO_OPCIONES on pa.ID_ACCESO equals ao.ID_ACCESO
                          join m in db.MODULO on ao.MODULO.ID_MODULO equals m.ID_MODULO
                          where u.FECHA_VENCIMIENTO < DateTime.Now
                          select new
                          {
                              u,
                              p,
                              m,
                              ao
                          }).ToList();
                foreach (var item in ex)
                {
                    usr.Add(new DtoExplotadorUsuariosHis() { ID = item.u.ID_USUARIO, USUARIO = item.u.APELLIDOS + " " + item.u.NOMBRES, USR = item.u.USR, PERFIL = item.p.DESCRIPCION, MODULO = item.m.DESCRIPCION, ID_ACCESO = item.ao.ID_ACCESO, ACCESO = item.ao.DESCRIPCION, ESTADO = item.u.ESTADO, FECHA_CADUCIDAD = (DateTime)item.u.FECHA_VENCIMIENTO });
                }
            }
            return usr;
        }
        public List<ACCESO_OPCIONES> RecuperaAccesosOpcionesXmodulo(Int32 id_modulo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ACCESO_OPCIONES> ao = (from a in db.ACCESO_OPCIONES
                                            where a.MODULO.ID_MODULO == id_modulo
                                            select a).ToList();
                return ao;
            }
        }
        public void EliminarAccesoOpciones1(List<ACCESO_OPCIONES> accopc)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.EliminarLista(accopc);
            }
        }
        public List<DtoAccesosHis> recuperaAccesoUsuario(Int64 ID_USUARIO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PERFILES_ACCESOS> uspr = (from up in db.USUARIOS_PERFILES
                                               join pa in db.PERFILES_ACCESOS on up.PERFILES.ID_PERFIL equals pa.ID_PERFIL
                                               where up.ID_USUARIO == ID_USUARIO
                                               select pa).ToList();
                List<DtoAccesosHis> ach = new List<DtoAccesosHis>();
                foreach (var item in uspr)
                {
                    DtoAccesosHis ac = new DtoAccesosHis();
                    ac.ID_ACCESO = item.ID_ACCESO;
                    ac.ID_PERFIL = item.ID_PERFIL;
                    ach.Add(ac);
                }
                return ach;
            }
        }
        public PERFILES_ACCESOS accesosModulosXperfiles(Int64 id_perfil, Int64 id_acceso)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return db.PERFILES_ACCESOS.FirstOrDefault(x => x.ID_PERFIL == id_perfil && x.ACCESO_OPCIONES.ID_ACCESO == id_acceso);
            }
        }
    }
}
