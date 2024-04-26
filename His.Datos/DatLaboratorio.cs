using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using His.Entidades.General;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace His.Datos
{
    /// <summary>
    /// Recupera datos de laboratorio
    /// </summary>
    public class DatLaboratorio
    {
        /// <summary>
        /// 
        /// Método para recuperar por fechas 
        /// </summary>
        /// <param name="fechaIni"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public List<DtoLaboratorio> RecuperarPacientes(string fechaIni, string fechaFin)
        {
            try
            {

                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    if (fechaIni != null)
                    {
                        DateTime fechainicio = Convert.ToDateTime(fechaIni);
                        DateTime fechafinal = Convert.ToDateTime(fechaFin);
                        return (from l in contexto.LABORATORIOS
                                where fechainicio <= l.FECHA && fechafinal >= l.FECHA
                                orderby l.FECHA descending
                                select new DtoLaboratorio
                                {
                                    HISTORIA_CLINICA = l.HISTORIA_CLINICA,
                                    FECHA = l.FECHA.Value,
                                    APELLIDO = l.APELLIDO,
                                    NOMBRE = l.NOMBRE,
                                    NO_ORDEN = l.NO_ORDEN,
                                    AÑO_ORDEN = l.AÑO_ORDEN.Value,
                                    COD_EXAMEN = l.CODIGO_EXAMEN,
                                    SOAT = l.SOAT.Value,
                                    IESS = l.IESS.Value,
                                    NOM_EXA = l.EXAMEN,
                                    COD_TARIFA = l.COD_TARIFA.Value,
                                    NOM_TARIFA = l.NOM_TARIFA,
                                    TARIFA = l.TARIFA.Value,
                                    COD_IESS = l.COD_IESS.Value,
                                    TAR_IESS = l.TAR_IESS.Value,
                                    TAR_DIFERENCIA = l.TAR_DIFERENCIA.Value,
                                    CANTIDAD = 1,
                                    TOTAL = l.TAR_IESS.Value
                                }).ToList();
                    }
                    else
                    {
                        return (from l in contexto.LABORATORIOS
                                select new DtoLaboratorio
                                {
                                    HISTORIA_CLINICA = l.HISTORIA_CLINICA,
                                    FECHA = l.FECHA.Value,
                                    APELLIDO = l.APELLIDO,
                                    NOMBRE = l.NOMBRE,
                                    NO_ORDEN = l.NO_ORDEN,
                                    AÑO_ORDEN = l.AÑO_ORDEN.Value,
                                    COD_EXAMEN = l.CODIGO_EXAMEN,
                                    SOAT = l.SOAT.Value,
                                    IESS = l.IESS.Value,
                                    NOM_EXA = l.EXAMEN,
                                    COD_TARIFA = l.COD_TARIFA.Value,
                                    NOM_TARIFA = l.NOM_TARIFA,
                                    TARIFA = l.TARIFA.Value,
                                    COD_IESS = l.COD_IESS.Value,
                                    TAR_IESS = l.TAR_IESS.Value,
                                    TAR_DIFERENCIA = l.TAR_DIFERENCIA.Value,
                                    CANTIDAD = 1,
                                    TOTAL = l.TARIFA.Value
                                }).ToList();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public HC_LABORATORIO_CLINICO recuperarlaboratorioPorAtencion(int codigoAtencion)
        {
            HC_LABORATORIO_CLINICO laboratorio;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                laboratorio = (from d in contexto.HC_LABORATORIO_CLINICO
                               where d.ATE_CODIGO == codigoAtencion
                               select d).FirstOrDefault();
                return laboratorio;
            }
        }
        public HC_LABORATORIO_CLINICO recuperarLaboratorio(Int64 lcl)
        {
            HC_LABORATORIO_CLINICO laboratorio;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                laboratorio = (from d in contexto.HC_LABORATORIO_CLINICO
                               where d.LCL_CODIGO == lcl
                               select d).FirstOrDefault();
                return laboratorio;
            }
        }
        public List<HC_LABORATORIO_CLINICO_DETALLE> recuperarLaboratorioDetalle(Int64 lcl)
        {
            List<HC_LABORATORIO_CLINICO_DETALLE> lista = new List<HC_LABORATORIO_CLINICO_DETALLE>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                lista = (from cd in db.HC_LABORATORIO_CLINICO_DETALLE
                         where cd.LCL_CODIGO == lcl
                         select cd).ToList();
            }
            return lista;
        }
        public List<HC_CATALOGOS> listarCatalogoLaboratorio(int hct_tipo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HC_CATALOGOS> catalogos = (from c in db.HC_CATALOGOS
                                                where c.HC_CATALOGOS_TIPO.HCT_CODIGO == hct_tipo
                                                select c).ToList();
                return catalogos;
            }
        }
        public List<DtoLaboratorioEstructura> listarProducto(int codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            List<DtoLaboratorioEstructura> lab = new List<DtoLaboratorioEstructura>();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_LaboratorioGrupos", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Codigo", codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                DtoLaboratorioEstructura l = new DtoLaboratorioEstructura();
                l.CODIGO_AREA = Convert.ToInt64(reader["coddep"].ToString());
                l.AREA = reader["desdep"].ToString();
                l.COD_EXAMEN = reader["codsec"].ToString();
                l.COD_PRODUCTO = reader["codpro"].ToString();
                l.EXAMEN = reader["despro"].ToString();

                lab.Add(l);
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return lab;
        }
        public bool crearLaboratorio(HC_LABORATORIO_CLINICO cabecera, List<HC_LABORATORIO_CLINICO_DETALLE> detalle)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transaction = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    db.AddToHC_LABORATORIO_CLINICO(cabecera);
                    db.SaveChanges();

                    var x = db.HC_LABORATORIO_CLINICO
            .OrderByDescending(a => a.LCL_CODIGO)
            .First().LCL_CODIGO;

                    foreach (var item in detalle)
                    {
                        item.LCL_CODIGO = x;
                        db.AddToHC_LABORATORIO_CLINICO_DETALLE(item);
                        db.SaveChanges();
                    }
                    transaction.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }
        }
        public bool editarLaboratorio(HC_LABORATORIO_CLINICO cabecera, List<HC_LABORATORIO_CLINICO_DETALLE> detalle, Int64 lclCodigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transaction = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    HC_LABORATORIO_CLINICO original = (from cl in db.HC_LABORATORIO_CLINICO
                                                       where cl.LCL_CODIGO == lclCodigo
                                                       select cl).FirstOrDefault();

                    original.LCL_FECHA_MUESTRA = cabecera.LCL_FECHA_MUESTRA;
                    original.LCL_SALA = cabecera.LCL_SALA;
                    original.LCL_PRIORIDAD_C = cabecera.LCL_PRIORIDAD_C;
                    original.LCL_PRIORIDAD_R = cabecera.LCL_PRIORIDAD_R;
                    original.LCL_PRIORIDAD_U = cabecera.LCL_PRIORIDAD_U;
                    original.LCL_NOMBRE_RECIBE = cabecera.LCL_NOMBRE_RECIBE;
                    original.LCL_SERVICIO = cabecera.LCL_SERVICIO;
                    original.MED_CODIGO = cabecera.MED_CODIGO;
                    original.LCL_MUESTRA = cabecera.LCL_MUESTRA;

                    db.SaveChanges();

                    List<HC_LABORATORIO_CLINICO_DETALLE> _ori = new List<HC_LABORATORIO_CLINICO_DETALLE>();
                    _ori = (from cld in db.HC_LABORATORIO_CLINICO_DETALLE
                            where cld.LCL_CODIGO == lclCodigo
                            select cld).ToList();
                    foreach (var item in _ori)
                    {
                        db.DeleteObject(item);
                        db.SaveChanges();
                    }
                    foreach (var item in detalle)
                    {
                        item.LCL_CODIGO = lclCodigo;
                        db.AddToHC_LABORATORIO_CLINICO_DETALLE(item);
                        db.SaveChanges();
                    }
                    transaction.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }

            }
        }
        public DataTable recuperarProductoLaboratorio(string codpro)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            DataTable dt = new DataTable();

            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_LaboratorioProducto", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            reader = command.ExecuteReader();
            dt.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return dt;
        }
        public DataTable CargaDepartamento()
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
            Sqlcmd = new SqlCommand("select * from Sic3000..ProductoDepartamento where coddep in (401001,401002,401003,401004,401005,401006,401007)", Sqlcon);
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


        public bool RepProcedimiento(string proce)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var maestro = (from p in db.PERFILES_LABORATORIO
                               where p.PL_PERFIL == proce
                               select p).ToList();
                if (maestro.Count > 0)
                    return true;
                else
                    return false;

            }
        }
        public bool CreaPerfil(PERFILES_LABORATORIO perLab)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    db.Crear("PERFILES_LABORATORIO", perLab);
                    db.SaveChanges();
                    transa.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transa.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }
        }
        public bool ActualizarPerfil(Int64 PL_CODIGO, string PL_PERFIL)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    PERFILES_LABORATORIO pl = (from p in db.PERFILES_LABORATORIO
                                               where p.PL_CODIGO == PL_CODIGO
                                               select p).FirstOrDefault();
                    pl.PL_PERFIL = PL_PERFIL;
                    db.SaveChanges();
                    transa.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transa.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
            }
        }
        public bool EliminarPerfil(Int64 PL_CODIGO)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
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
            Sqlcmd = new SqlCommand("delete from PERFILES_PRODUCTOS where PL_CODIGO =" + PL_CODIGO + " \n"
            + "delete from PERFILES_LABORATORIO where PL_CODIGO =" + PL_CODIGO, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            try
            {
                Sqlcmd.ExecuteReader();
                Sqldap.SelectCommand = Sqlcmd;
                Sqlcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public int UltimoOrdenPerfil(int pci_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from qp in db.PERFILES_LABORATORIO
                             where qp.PL_CODIGO == pci_codigo
                             select qp.PL_CODIGO).ToList();

                if (lista.Count > 0)
                    return (int)lista.Max();
                else
                    return 0;
            }
        }
        public bool AgregarProducto(PERFILES_PRODUCTOS pproduc)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    db.Crear("PERFILES_PRODUCTOS", pproduc);
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
        public string RepProducto(Int64 PL_CODIGO, Int64 codrpo)
        {
            string producto = null;
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
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
            Sqlcmd = new SqlCommand("select codrpo from PERFILES_PRODUCTOS where PL_CODIGO = " + PL_CODIGO + " and codrpo = " + codrpo + "", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            SqlDataReader reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                producto = reader["codrpo"].ToString();
            }
            Sqlcmd.Parameters.Clear();
            Sqlcon.Close();
            return producto;
        }

        public DataTable listarPerfiles()
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
            Sqlcmd = new SqlCommand("select PL_PERFIL AS 'PERFIL',PP.PL_CODIGO as 'CODIGO', despro AS 'DESCRIPCION',desdep as 'DIVISION',p.codpro,PD.coddep" +
                ",(u.USR+'-['+u.APELLIDOS+' '+u.NOMBRES+']') as 'USUARIO',u.ID_USUARIO  from PERFILES_LABORATORIO pl\n"
            + "inner join PERFILES_PRODUCTOS pp on pl.PL_CODIGO = pp.PL_CODIGO \n"
            + "inner join Sic3000..Producto p on pp.codrpo = p.codpro\n"
            + "INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep inner " +
            "join USUARIOS u on pl.ID_USUARIO = u.ID_USUARIO order by 1 ", Sqlcon);
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
        public Int32 UltimoCodigo()
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PERFILES_LABORATORIO> prl = (from p in db.PERFILES_LABORATORIO
                                                  select p).ToList();
                if (prl.Count > 0)
                    return prl.Max(x => x.PL_CODIGO);
                else
                    return 0;
            }
        }
        public bool ActualizarPerfil_Producto(Int64 PL_CODIGO, Int64 codpro, Int32 _CODPRO, Int32 coddep)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    PERFILES_PRODUCTOS x = (from q in db.PERFILES_PRODUCTOS
                                            where q.PL_CODIGO == PL_CODIGO &&
                                            q.codrpo == codpro
                                            select q).FirstOrDefault();
                    x.codrpo = _CODPRO;
                    x.coddep = coddep;
                    db.SaveChanges();
                    transa.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transa.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }
        }
        public bool EliminarPerfil_producto(Int64 PL_CODIGO, Int32 codpro)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
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
            Sqlcmd = new SqlCommand("delete PERFILES_PRODUCTOS where PL_CODIGO =" + PL_CODIGO + " and codrpo =" + codpro, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            try
            {
                Sqlcmd.ExecuteReader();
                Sqldap.SelectCommand = Sqlcmd;
                Sqlcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public List<PERFILES_LABORATORIO> cargaPerfiles(Int64 ID_USUARIO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PERFILES_LABORATORIO> list = (from p in db.PERFILES_LABORATORIO
                                                   where p.ID_USUARIO == ID_USUARIO
                                                   select p).ToList();
                return list;
            }
        }
        #region Carga Gris Perfiles
        public List<DtoLaboratorioEstructura> cargadgvHematologia(Int64 PL_CODIGO)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            List<DtoLaboratorioEstructura> lab = new List<DtoLaboratorioEstructura>();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("select PD.coddep,pd.desdep,p.codsec,p.codpro,p.despro from PERFILES_PRODUCTOS pp \n"
            + "inner join Sic3000..Producto p on pp.codrpo = p.codpro\n"
            + "INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep \n"
            + "where pp.PL_CODIGO = " + PL_CODIGO + " and pd.coddep = 401001", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                DtoLaboratorioEstructura l = new DtoLaboratorioEstructura();
                l.CODIGO_AREA = Convert.ToInt64(reader["coddep"].ToString());
                l.AREA = reader["desdep"].ToString();
                l.COD_EXAMEN = reader["codsec"].ToString();
                l.COD_PRODUCTO = reader["codpro"].ToString();
                l.EXAMEN = reader["despro"].ToString();

                lab.Add(l);
            }
            try
            {
                reader.Close();
                Sqlcon.Close();
                return lab;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return lab;
            }

        }
        public List<DtoLaboratorioEstructura> cargadgvUroanalisis(Int64 PL_CODIGO)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            List<DtoLaboratorioEstructura> lab = new List<DtoLaboratorioEstructura>();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("select PD.coddep,pd.desdep,p.codsec,p.codpro,p.despro from PERFILES_PRODUCTOS pp \n"
            + "inner join Sic3000..Producto p on pp.codrpo = p.codpro\n"
            + "INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep \n"
            + "where pp.PL_CODIGO = " + PL_CODIGO + " and pd.coddep = 401002", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                DtoLaboratorioEstructura l = new DtoLaboratorioEstructura();
                l.CODIGO_AREA = Convert.ToInt64(reader["coddep"].ToString());
                l.AREA = reader["desdep"].ToString();
                l.COD_EXAMEN = reader["codsec"].ToString();
                l.COD_PRODUCTO = reader["codpro"].ToString();
                l.EXAMEN = reader["despro"].ToString();

                lab.Add(l);
            }
            try
            {
                reader.Close();
                Sqlcon.Close();
                return lab;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return lab;
            }

        }
        public List<DtoLaboratorioEstructura> cargadvgCoprologico(Int64 PL_CODIGO)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            List<DtoLaboratorioEstructura> lab = new List<DtoLaboratorioEstructura>();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("select PD.coddep,pd.desdep,p.codsec,p.codpro,p.despro from PERFILES_PRODUCTOS pp \n"
            + "inner join Sic3000..Producto p on pp.codrpo = p.codpro\n"
            + "INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep \n"
            + "where pp.PL_CODIGO = " + PL_CODIGO + " and pd.coddep = 401003", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                DtoLaboratorioEstructura l = new DtoLaboratorioEstructura();
                l.CODIGO_AREA = Convert.ToInt64(reader["coddep"].ToString());
                l.AREA = reader["desdep"].ToString();
                l.COD_EXAMEN = reader["codsec"].ToString();
                l.COD_PRODUCTO = reader["codpro"].ToString();
                l.EXAMEN = reader["despro"].ToString();

                lab.Add(l);
            }
            try
            {
                reader.Close();
                Sqlcon.Close();
                return lab;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return lab;
            }

        }
        public List<DtoLaboratorioEstructura> cargadgvQSanguinea(Int64 PL_CODIGO)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            List<DtoLaboratorioEstructura> lab = new List<DtoLaboratorioEstructura>();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("select PD.coddep,pd.desdep,p.codsec,p.codpro,p.despro from PERFILES_PRODUCTOS pp \n"
            + "inner join Sic3000..Producto p on pp.codrpo = p.codpro\n"
            + "INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep \n"
            + "where pp.PL_CODIGO = " + PL_CODIGO + " and pd.coddep = 401004", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                DtoLaboratorioEstructura l = new DtoLaboratorioEstructura();
                l.CODIGO_AREA = Convert.ToInt64(reader["coddep"].ToString());
                l.AREA = reader["desdep"].ToString();
                l.COD_EXAMEN = reader["codsec"].ToString();
                l.COD_PRODUCTO = reader["codpro"].ToString();
                l.EXAMEN = reader["despro"].ToString();

                lab.Add(l);
            }
            try
            {
                reader.Close();
                Sqlcon.Close();
                return lab;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return lab;
            }

        }
        public List<DtoLaboratorioEstructura> cargadgvSerologia(Int64 PL_CODIGO)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            List<DtoLaboratorioEstructura> lab = new List<DtoLaboratorioEstructura>();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("select PD.coddep,pd.desdep,p.codsec,p.codpro,p.despro from PERFILES_PRODUCTOS pp \n"
            + "inner join Sic3000..Producto p on pp.codrpo = p.codpro\n"
            + "INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep \n"
            + "where pp.PL_CODIGO = " + PL_CODIGO + " and pd.coddep = 401005", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                DtoLaboratorioEstructura l = new DtoLaboratorioEstructura();
                l.CODIGO_AREA = Convert.ToInt64(reader["coddep"].ToString());
                l.AREA = reader["desdep"].ToString();
                l.COD_EXAMEN = reader["codsec"].ToString();
                l.COD_PRODUCTO = reader["codpro"].ToString();
                l.EXAMEN = reader["despro"].ToString();

                lab.Add(l);
            }
            try
            {
                reader.Close();
                Sqlcon.Close();
                return lab;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return lab;
            }

        }
        public List<DtoLaboratorioEstructura> cargadgvBacteriologia(Int64 PL_CODIGO)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            List<DtoLaboratorioEstructura> lab = new List<DtoLaboratorioEstructura>();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("select PD.coddep,pd.desdep,p.codsec,p.codpro,p.despro from PERFILES_PRODUCTOS pp \n"
            + "inner join Sic3000..Producto p on pp.codrpo = p.codpro\n"
            + "INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep \n"
            + "where pp.PL_CODIGO = " + PL_CODIGO + " and pd.coddep = 401006", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                DtoLaboratorioEstructura l = new DtoLaboratorioEstructura();
                l.CODIGO_AREA = Convert.ToInt64(reader["coddep"].ToString());
                l.AREA = reader["desdep"].ToString();
                l.COD_EXAMEN = reader["codsec"].ToString();
                l.COD_PRODUCTO = reader["codpro"].ToString();
                l.EXAMEN = reader["despro"].ToString();

                lab.Add(l);
            }
            try
            {
                reader.Close();
                Sqlcon.Close();
                return lab;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return lab;
            }

        }
        public List<DtoLaboratorioEstructura> cargadgvOtros(Int64 PL_CODIGO)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            List<DtoLaboratorioEstructura> lab = new List<DtoLaboratorioEstructura>();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("select PD.coddep,pd.desdep,p.codsec,p.codpro,p.despro from PERFILES_PRODUCTOS pp \n"
            + "inner join Sic3000..Producto p on pp.codrpo = p.codpro\n"
            + "INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep \n"
            + "where pp.PL_CODIGO = " + PL_CODIGO + " and pd.coddep = 401007", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                DtoLaboratorioEstructura l = new DtoLaboratorioEstructura();
                l.CODIGO_AREA = Convert.ToInt64(reader["coddep"].ToString());
                l.AREA = reader["desdep"].ToString();
                l.COD_EXAMEN = reader["codsec"].ToString();
                l.COD_PRODUCTO = reader["codpro"].ToString();
                l.EXAMEN = reader["despro"].ToString();

                lab.Add(l);
            }
            try
            {
                reader.Close();
                Sqlcon.Close();
                return lab;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return lab;
            }

        }
        #endregion

        public bool actualizarPerfilesLaboratorio(int LCL_ESTADO, Int64 LCL_CODIGO)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
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
            Sqlcmd = new SqlCommand("UPDATE HC_LABORATORIO_CLINICO SET LCL_ESTADO = " + LCL_ESTADO + " where LCL_CODIGO = " + LCL_CODIGO, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            try
            {
                Sqlcmd.ExecuteReader();
                Sqldap.SelectCommand = Sqlcmd;
                Sqlcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public DataTable listarProductoDt(int codigo)
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

            Sqlcmd = new SqlCommand("sp_LaboratorioGrupos", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = codigo;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            Sqlcon.Close();
            return Dts;

        }
        #region CargaGrid por ayudas
        public DtoLaboratorioEstructura cargadgvHematologiaAyuda(Int64 codpro)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            SqlDataReader reader;
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
            Sqlcmd = new SqlCommand("select PD.coddep,pd.desdep,p.codsec,p.codpro,p.despro \n"
            + "from Sic3000..Producto p \n"
            + "INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep \n"
            + "where  pd.coddep = 401001 and codpro = " + codpro, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            reader = Sqlcmd.ExecuteReader();
            reader.Read();
            DtoLaboratorioEstructura l = new DtoLaboratorioEstructura();
            l.CODIGO_AREA = Convert.ToInt64(reader["coddep"].ToString());
            l.AREA = reader["desdep"].ToString();
            l.COD_EXAMEN = reader["codsec"].ToString();
            l.COD_PRODUCTO = reader["codpro"].ToString();
            l.EXAMEN = reader["despro"].ToString();

            try
            {
                reader.Close();
                Sqlcon.Close();
                return l;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return l;
            }

        }
        public DtoLaboratorioEstructura cargadgvUroanalisiAyuda(Int64 codpro)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            SqlDataReader reader;
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
            Sqlcmd = new SqlCommand("select PD.coddep,pd.desdep,p.codsec,p.codpro,p.despro \n"
            + "from Sic3000..Producto p \n"
            + "INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep \n"
            + "where  pd.coddep = 401002 and codpro = " + codpro, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            reader = Sqlcmd.ExecuteReader();
            reader.Read();
            DtoLaboratorioEstructura l = new DtoLaboratorioEstructura();
            l.CODIGO_AREA = Convert.ToInt64(reader["coddep"].ToString());
            l.AREA = reader["desdep"].ToString();
            l.COD_EXAMEN = reader["codsec"].ToString();
            l.COD_PRODUCTO = reader["codpro"].ToString();
            l.EXAMEN = reader["despro"].ToString();

            try
            {
                reader.Close();
                Sqlcon.Close();
                return l;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return l;
            }

        }
        public DtoLaboratorioEstructura cargadgvCoprologicoAyuda(Int64 codpro)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            SqlDataReader reader;
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
            Sqlcmd = new SqlCommand("select PD.coddep,pd.desdep,p.codsec,p.codpro,p.despro \n"
            + "from Sic3000..Producto p \n"
            + "INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep \n"
            + "where  pd.coddep = 401003 and codpro = " + codpro, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            reader = Sqlcmd.ExecuteReader();
            reader.Read();
            DtoLaboratorioEstructura l = new DtoLaboratorioEstructura();
            l.CODIGO_AREA = Convert.ToInt64(reader["coddep"].ToString());
            l.AREA = reader["desdep"].ToString();
            l.COD_EXAMEN = reader["codsec"].ToString();
            l.COD_PRODUCTO = reader["codpro"].ToString();
            l.EXAMEN = reader["despro"].ToString();

            try
            {
                reader.Close();
                Sqlcon.Close();
                return l;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return l;
            }

        }
        public DtoLaboratorioEstructura cargadgvQsanguineaAyuda(Int64 codpro)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            SqlDataReader reader;
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
            Sqlcmd = new SqlCommand("select PD.coddep,pd.desdep,p.codsec,p.codpro,p.despro \n"
            + "from Sic3000..Producto p \n"
            + "INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep \n"
            + "where  pd.coddep = 401004 and codpro = " + codpro, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            reader = Sqlcmd.ExecuteReader();
            reader.Read();
            DtoLaboratorioEstructura l = new DtoLaboratorioEstructura();
            l.CODIGO_AREA = Convert.ToInt64(reader["coddep"].ToString());
            l.AREA = reader["desdep"].ToString();
            l.COD_EXAMEN = reader["codsec"].ToString();
            l.COD_PRODUCTO = reader["codpro"].ToString();
            l.EXAMEN = reader["despro"].ToString();

            try
            {
                reader.Close();
                Sqlcon.Close();
                return l;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return l;
            }

        }
        public DtoLaboratorioEstructura cargadgvSerologiaAyuda(Int64 codpro)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            SqlDataReader reader;
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
            Sqlcmd = new SqlCommand("select PD.coddep,pd.desdep,p.codsec,p.codpro,p.despro \n"
            + "from Sic3000..Producto p \n"
            + "INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep \n"
            + "where  pd.coddep = 401005 and codpro = " + codpro, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            reader = Sqlcmd.ExecuteReader();
            reader.Read();
            DtoLaboratorioEstructura l = new DtoLaboratorioEstructura();
            l.CODIGO_AREA = Convert.ToInt64(reader["coddep"].ToString());
            l.AREA = reader["desdep"].ToString();
            l.COD_EXAMEN = reader["codsec"].ToString();
            l.COD_PRODUCTO = reader["codpro"].ToString();
            l.EXAMEN = reader["despro"].ToString();

            try
            {
                reader.Close();
                Sqlcon.Close();
                return l;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return l;
            }

        }
        public DtoLaboratorioEstructura cargadgvBacteriologiaAyuda(Int64 codpro)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            SqlDataReader reader;
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
            Sqlcmd = new SqlCommand("select PD.coddep,pd.desdep,p.codsec,p.codpro,p.despro \n"
            + "from Sic3000..Producto p \n"
            + "INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep \n"
            + "where  pd.coddep = 401006 and codpro = " + codpro, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            reader = Sqlcmd.ExecuteReader();
            reader.Read();
            DtoLaboratorioEstructura l = new DtoLaboratorioEstructura();
            l.CODIGO_AREA = Convert.ToInt64(reader["coddep"].ToString());
            l.AREA = reader["desdep"].ToString();
            l.COD_EXAMEN = reader["codsec"].ToString();
            l.COD_PRODUCTO = reader["codpro"].ToString();
            l.EXAMEN = reader["despro"].ToString();

            try
            {
                reader.Close();
                Sqlcon.Close();
                return l;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return l;
            }

        }
        public DtoLaboratorioEstructura cargadgvOtrosAyuda(Int64 codpro)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            SqlDataReader reader;
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
            Sqlcmd = new SqlCommand("select PD.coddep,pd.desdep,p.codsec,p.codpro,p.despro \n"
            + "from Sic3000..Producto p \n"
            + "INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep \n"
            + "where  pd.coddep = 401007 and codpro = " + codpro, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            reader = Sqlcmd.ExecuteReader();
            reader.Read();
            DtoLaboratorioEstructura l = new DtoLaboratorioEstructura();
            l.CODIGO_AREA = Convert.ToInt64(reader["coddep"].ToString());
            l.AREA = reader["desdep"].ToString();
            l.COD_EXAMEN = reader["codsec"].ToString();
            l.COD_PRODUCTO = reader["codpro"].ToString();
            l.EXAMEN = reader["despro"].ToString();

            try
            {
                reader.Close();
                Sqlcon.Close();
                return l;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return l;
            }

        }
        #endregion
    }
}
