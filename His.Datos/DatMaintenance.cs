using Core.Datos;
using His.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace His.Datos
{
    public class DatMaintenance
    {

        public void setROW(string tabla, object[] values, string code = "")
        {
            string query = "";
            switch (tabla)
            {
                case "SetTipoIngreso":

                    if (values[0].ToString() == "-1")
                    {
                        query = "INSERT INTO TIPO_INGRESO(TIP_CODIGO,TIP_DESCRIPCION,TIP_ESTADO)  VALUES((SELECT MAX(TIP_CODIGO)+1 FROM TIPO_INGRESO) ,'" + values[1].ToString() + "'," + values[2].ToString() + " )";
                    }
                    else
                    {
                        query = "update TIPO_INGRESO set TIP_DESCRIPCION = '" + values[1].ToString() + "' \n" +
                                     ", TIP_ESTADO = " + values[2].ToString() + " where TIP_CODIGO=" + values[0].ToString() + " ";
                    }
                    break;

                default:
                    query = ("Nothing");
                    break;
            }
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(query, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        public object getQuery(string tabla, int codigo = 0)
        {
            object query = null;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                switch (tabla)
                {

                    case "ParametrosArchivos":
                        query = (from c in contexto.PARAMETROS_DETALLE
                                 where (c.PARAMETROS.PAR_CODIGO) == codigo
                                 select c).ToList();
                        break;
                    case "POR_FTP":
                        query = (from X in contexto.PARAMETROS_DETALLE
                                 where (X.PAD_NOMBRE) == "CONEXION FTP"
                                 select X).ToList();
                        break;
                    case "Parametros":
                        query = (from c in contexto.PARAMETROS
                                 select c).ToList();
                        break;
                    case "Ingreso":
                        query = (from c in contexto.TIPO_INGRESO
                                 select c).ToList();
                        break;

                    default:
                        query = ("Nothing");
                        break;
                }


            }
            return query;
        }



        public void setQuery(string tabla, PARAMETROS_DETALLE pd)
        {


            //using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            //{

            //            //var query = (from c in contexto.PARAMETROS_DETALLE
            //            //         where (c.PARAMETROS.PAR_CODIGO) == pd.PAD_CODIGO
            //            //         select c).FirstOrDefault();
            //            //query.PAD_VALOR = pd.PAD_VALOR;
            //            contexto.AttachUpdated(pd);
            //            contexto.SaveChanges();

            //}

            string cadena_sql;

            switch (tabla)
            {

                case "DetalleParametros":
                    cadena_sql = ("update PARAMETROS_DETALLE set pad_valor='" + pd.PAD_VALOR + "' where PAD_CODIGO=" + pd.PAD_CODIGO + " ");
                    break;



                default:
                    cadena_sql = ("");
                    break;
            }



            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //throw ex;
                Console.WriteLine(ex.Message);

            }





        }

        public DataTable getDataTable(string tabla)
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
            string query = "";
            switch (tabla)
            {

                case "CatalogoCostos":
                    query = ("SELECT dbo.CATALOGO_COSTOS.CAC_CODIGO AS CODIGO, dbo.CATALOGO_COSTOS.CAC_NOMBRE AS CATALOGO, dbo.CATALOGO_COSTOS_TIPO.CCT_NOMBRE AS TIPO\n" +
                                        "FROM dbo.CATALOGO_COSTOS INNER JOIN dbo.CATALOGO_COSTOS_TIPO ON dbo.CATALOGO_COSTOS.CCT_CODIGO = dbo.CATALOGO_COSTOS_TIPO.CCT_CODIGO ORDER BY CODIGO");
                    break;
                case "TipoIngreso":
                    query = ("SELECT TIP_CODIGO,TIP_DESCRIPCION,TIP_ESTADO from TIPO_INGRESO ORDER BY 1");
                    break;
                case "Usuarios":
                    query = ("select ID_USUARIO AS ID, CONCAT(APELLIDOS,' ',NOMBRES) AS NOMBRE from USUARIOS order by 2");
                    break;
                case "TipoCatalogoCostos":
                    query = ("SELECT [CCT_CODIGO] as CODIGO ,[CCT_NOMBRE] AS TIPO  FROM [His3000].[dbo].[CATALOGO_COSTOS_TIPO] order by [CCT_NOMBRE]");
                    break;
                case "TiposAtenciones":
                    query = ("SELECT        dbo.tipos_atenciones.id AS CODIGO,dbo.tipos_atenciones.name AS DESCRIPCION ,  dbo.TIPO_INGRESO.TIP_DESCRIPCION AS INGRESO " +
                               " FROM dbo.TIPO_INGRESO right JOIN dbo.tipos_atenciones ON dbo.TIPO_INGRESO.TIP_CODIGO = dbo.tipos_atenciones.list");
                    break;
                case "TiposIngresos":
                    query = ("select * from TIPO_INGRESO");
                    break;
                case "DescripcionCatalogoCostos":
                    query = ("select despro as PRODUCTO from (select sp.despro from sic3000..PRODUCTO sp inner join His3000..PRODUCTO hp on hp.PRO_DESCRIPCION=sp.despro) tabla " +
                             " left join (select CAC_NOMBRE from His3000..CATALOGO_COSTOS) hcc on tabla.despro = hcc.CAC_NOMBRE where hcc.CAC_NOMBRE is null");
                    break;

                default:
                    query = ("Nothing");
                    break;
            }



            Sqlcmd = new SqlCommand(query, Sqlcon);
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

        public bool delete(string tabla, int codigo)
        {
            string cadena_sql;



            switch (tabla)
            {

                case "CatalogoCostos":
                    cadena_sql = ("DELETE FROM [dbo].[CATALOGO_COSTOS] WHERE CAC_CODIGO= " + codigo + " ");
                    break;

                case "TipoCatalogoCostos":
                    cadena_sql = ("DELETE  FROM [His3000].[dbo].[CATALOGO_COSTOS_TIPO]  WHERE [CCT_CODIGO]= " + codigo + " ");
                    break;


                case "TipoAtencion":
                    cadena_sql = ("DELETE  FROM [His3000].[dbo].[tipos_atenciones]  WHERE [id]= " + codigo + " ");
                    break;
                case "TipoIngreso":
                    cadena_sql = ("DELETE  FROM [His3000].[dbo].[TIPO_INGRESO]  WHERE [TIP_CODIGO]= " + codigo + " ");
                    break;
                default:
                    cadena_sql = ("");
                    break;
            }



            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public void save_CatalogoCostos(int cod, int tipo, string desc)
        {
            string cadena_sql;
            if (cod == -1)
            {
                cadena_sql = "INSERT INTO [dbo].[CATALOGO_COSTOS] ([CAC_CODIGO] ,[CCT_CODIGO] ,[CAC_NOMBRE]) VALUES ( (select MAX(cac_codigo)+1 from [CATALOGO_COSTOS]), " +
                               "" + tipo + "" +
                               ",'" + desc + "')";
            }
            else
            {
                cadena_sql = "UPDATE [dbo].[CATALOGO_COSTOS] \n" +
                             "SET [CCT_CODIGO] = " + tipo + "\n" +
                             ", [CAC_NOMBRE] = '" + desc + "'\n" +
                             "WHERE CAC_CODIGO= '" + cod + "' ";
            }
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void save_TipoAtencion(int cod, int tipo, string desc)
        {
            string cadena_sql;
            if (cod == -1)
            {
                cadena_sql = "INSERT INTO [dbo].[tipos_atenciones] ([name] ,[list]) VALUES('"
                               + desc + "','" + tipo + "')";
            }
            else
            {
                cadena_sql = "UPDATE [dbo].[tipos_atenciones]\n" +
                               "SET [name] = '" + desc + "'\n" +
                                ", [list] = '" + tipo + "'\n" +
                             "WHERE id= '" + cod + "' ";
            }
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void save_TipoCosto(int cod, string desc)
        {
            string cadena_sql;
            if (cod == -1)
            {
                cadena_sql = "INSERT INTO [dbo].[CATALOGO_COSTOS_TIPO] ([CCT_CODIGO] ,[CCT_NOMBRE]) VALUES ( (select max([CCT_CODIGO])+1 from [dbo].[CATALOGO_COSTOS_TIPO]), '" + desc + "')";
            }
            else
            {
                cadena_sql = "UPDATE [dbo].[CATALOGO_COSTOS_TIPO]\n" +
                               "SET [CCT_NOMBRE] = '" + desc + "'\n" +
                             "WHERE CCT_CODIGO= '" + cod + "' ";
            }
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public int ultimoCodigoTipoCiudadano()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from p in contexto.TIPO_CIUDADANO
                             select p.TC_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }

        public int ultimaNacionalidad()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from p in contexto.PAIS
                             select p.CODPAIS).ToList();
                if (lista.Count > 0)
                    return lista.Max();
                return 0;
            }
        }
        public int ultimoPiso()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from p in contexto.NIVEL_PISO
                             select p.NIV_CODIGO).ToList();
                if (lista.Count > 0)
                    return lista.Max();
                return 0;
            }
        }
        public DataTable GetPisoMaquina()
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
            Sqlcmd = new SqlCommand("select  ISNULL(NP.NIV_CODIGO,'99') as NIV_CODIGO,ISNULL(NP.NIV_NOMBRE,'TODOS') as PISO, IP_MAQUINA,NIV_DESCRIPCION AS DESCRIPCION,L.nomlocal AS 'BODEGA PEDIDO', NIV_BODEGA AS 'Nº BODEGA PEDIDO', \n" +
                                    "(SELECT nomlocal FROM Sic3000..Locales WHERE codlocal = PM.BODEGA_REPO)AS 'BODEGA REPOSICION', PM.BODEGA_REPO AS 'Nº BODEGA REPOSICION' \n" +
                                    "from NIVEL_PISO_MAQUINA PM LEFT JOIN NIVEL_PISO NP ON PM.NIV_CODIGO = NP.NIV_CODIGO LEFT JOIN Sic3000..Locales L ON PM.NIV_BODEGA = L.codlocal", Sqlcon);
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
        public void CreaMaquina(int tipo, string maquina, string desc, Int32 bodegaP, Int32 bodegaD)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("INSERT INTO NIVEL_PISO_MAQUINA(NIV_CODIGO,IP_MAQUINA,NIV_DESCRIPCION,NIV_BODEGA,BODEGA_REPO)VALUES(@cod,@ip,@descrip,@bodegaP,@bodegaD)", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@cod", tipo);
            command.Parameters.AddWithValue("@ip", maquina);
            command.Parameters.AddWithValue("@descrip", desc);
            command.Parameters.AddWithValue("@bodegaP", bodegaP);
            command.Parameters.AddWithValue("@bodegaD", bodegaD);
            reader = command.ExecuteReader();

        }
        public void EditarMaquina(int tipo, string desc, string maquina, string temp, Int32 bodegaP, Int32 bodegaD)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("UPDATE NIVEL_PISO_MAQUINA SET NIV_CODIGO = @cod, IP_MAQUINA = @ip, NIV_DESCRIPCION = @descrip, NIV_BODEGA = @bodegaP, BODEGA_REPO = @bodegaD WHERE IP_MAQUINA = @temp", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@cod", tipo);
            command.Parameters.AddWithValue("@ip", desc);
            command.Parameters.AddWithValue("@descrip", maquina);
            command.Parameters.AddWithValue("@temp", temp);
            command.Parameters.AddWithValue("@bodegaP", bodegaP);
            command.Parameters.AddWithValue("@bodegaD", bodegaD);
            reader = command.ExecuteReader();
        }
        public void EliminarMaquina(string maquina)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("DELETE FROM NIVEL_PISO_MAQUINA WHERE IP_MAQUINA = @descrip", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@descrip", maquina);
            reader = command.ExecuteReader();
        }
        public bool existePisoMaquina(string ip)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool existe = false;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select * from NIVEL_PISO_MAQUINA where IP_MAQUINA = @ip", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@ip", ip);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                existe = true;
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return existe;
        }
        public List<NIVEL_PISO> cargarComboNivelPiso()
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<NIVEL_PISO> lista = new List<NIVEL_PISO>();
                lista = (from n in db.NIVEL_PISO
                         select n
                         ).ToList();
                NIVEL_PISO x = new NIVEL_PISO();
                x.NIV_CODIGO = 99;
                x.NIV_NOMBRE = "TODOS";
                x.NIV_NUMERO_PISO = 99;
                lista.Add(x);
                return lista;
            }

        }
        public DataTable cargarBodega()
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
            Sqlcmd = new SqlCommand("select codlocal,nomlocal from Sic3000..Locales order by nomlocal", Sqlcon);
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
        public DataTable cargarBodegaExplorador()
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
            Sqlcmd = new SqlCommand("select codlocal,nomlocal from Sic3000..Locales where codlocal in (12,19) order by nomlocal", Sqlcon);
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
        public bool existePiso(string ip)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool existe = false;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select * from NIVEL_PISO where NIV_NOMBRE =  @ip", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@ip", ip);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                existe = true;
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return existe;
        }
        public List<NIVEL_PISO> GetPiso()
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from n in db.NIVEL_PISO select n).OrderBy(x => x.NIV_NOMBRE).ToList();
                return lista;
            }
        }


        public DataTable GetPaises()
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
            Sqlcmd = new SqlCommand("select CODPAIS as CODIGO, NOMPAIS as PAIS, NACIONALIDAD, CODAREA as 'CODIGO AREA' from PAIS order by 2", Sqlcon);
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
        public void CrearCiudadano(TIPO_CIUDADANO ciudadano)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.AddToTIPO_CIUDADANO(ciudadano);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }
        public void CreaNacionalidad(PAIS paises)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.AddToPAIS(paises);
                    contexto.SaveChanges();
                }
            }
            catch (Exception error)
            {
                Console.Write(error);
            }
        }
        public void CreaPiso(NIVEL_PISO paises)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.AddToNIVEL_PISO(paises);
                    contexto.SaveChanges();
                }
            }
            catch (Exception error)
            {
                Console.Write(error);
            }
        }
        public void ModificarTipoCiudadano(int tc_codigo, string tc_descripcion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                TIPO_CIUDADANO ciudadano = contexto.TIPO_CIUDADANO.FirstOrDefault(p => p.TC_CODIGO == tc_codigo);
                ciudadano.TC_DESCRIPCION = tc_descripcion;
                contexto.SaveChanges();
            }
        }
        public void EditarNacionalidad(short codigo, string pais, string nacionalidad)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PAIS paises = contexto.PAIS.FirstOrDefault(p => p.CODPAIS == codigo);
                paises.NOMPAIS = pais; paises.NACIONALIDAD = nacionalidad;
                contexto.SaveChanges();
            }
        }
        public void EditarPiso(short codigo, string pais, short nacionalidad)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                NIVEL_PISO paises = contexto.NIVEL_PISO.FirstOrDefault(p => p.NIV_CODIGO == codigo);
                paises.NIV_NOMBRE = pais; paises.NIV_NUMERO_PISO = nacionalidad;
                contexto.SaveChanges();
            }
        }
        public void EliminarTipoCiudadano(int tc_codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                TIPO_CIUDADANO ciudadano = contexto.TIPO_CIUDADANO.SingleOrDefault(x => x.TC_CODIGO == tc_codigo);
                if (ciudadano != null)
                {
                    contexto.DeleteObject(ciudadano);
                    contexto.SaveChanges();
                }
            }
        }
        public void EliminarNacionalidad(short codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PAIS paises = contexto.PAIS.SingleOrDefault(x => x.CODPAIS == codigo);
                if (paises != null)
                {
                    contexto.DeleteObject(paises);
                    contexto.SaveChanges();
                }
            }
        }
        public void EliminarPiso(short codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                NIVEL_PISO paises = contexto.NIVEL_PISO.SingleOrDefault(x => x.NIV_CODIGO == codigo);
                if (paises != null)
                {
                    contexto.DeleteObject(paises);
                    contexto.SaveChanges();
                }
            }
        }
        public DataTable GetCiudadano()
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
            Sqlcmd = new SqlCommand("SELECT TC_CODIGO AS CODIGO, TC_DESCRIPCION AS DESCRIPCION FROM TIPO_CIUDADANO", Sqlcon);
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
        public static bool validadorEmail(string correo)
        {
            bool ok = false;
            try
            {
                String expresion;
                expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                if (Regex.IsMatch(correo, expresion))
                {
                    if (Regex.Replace(correo, expresion, String.Empty).Length == 0)
                    {
                        ok = true;
                    }
                    else
                    {
                        ok = false;
                    }
                }
                else
                {
                    ok = false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return ok;
        }
        public void EnviarCorreo(int cat_codigo, DateTime fechacaducar, string cat_nombre, bool enviar)
        {
            SqlConnection connection;
            SqlCommand command;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            string mail = "";
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select CCE_CORREORECIBE from CONVENIO_CORREO_ENVIA";
            command.CommandType = CommandType.Text;
            SqlDataReader reader1 = command.ExecuteReader();
            if (reader1.Read() == true)
            {
                mail = reader1.GetString(0);
            }
            reader1.Close();


            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select * from CONVENIO_CORREO where CAT_CODIGO =@cat_codigo and CC_FECHA_ENVIO between @fecha and GETDATE()";
            command.Parameters.AddWithValue("@cat_codigo", cat_codigo);
            command.Parameters.AddWithValue("@fecha", DateTime.Now.Date);
            command.CommandType = CommandType.Text;

            SqlDataReader reader = command.ExecuteReader();
            bool caduco = false;
            int dias = DateTime.Now.Day - fechacaducar.Day;
            if (reader.Read() != true)
            {
                foreach (var address in mail.Split(';'))
                {
                    if (address != "")
                        if (validadorEmail(address.Trim()))
                        {
                            if (fechacaducar > DateTime.Now)
                            {
                                DatSoporteMail mailService = new DatSoporteMail();
                                mailService.sendMail(
                                  subject: "HIS3000 : CADUCIDAD DE CONVENIO - " + cat_nombre,
                                          body: "Se comunica que el convenio " + cat_nombre + " esta próximo a caducar en " + Math.Abs(dias) + "día(s) , por favor ponerse en contacto con el encargado de los convenios o consulte con el administrador.\r\nGRACIAS POR USAR NUESTRO SISTEMA",
                                          recipientMail: new List<string> { address }
                                          );
                                caduco = true;
                            }
                            else
                            {
                                DatSoporteMail mailService = new DatSoporteMail();
                                mailService.sendMail(
                                  subject: "HIS3000 : CADUCIDAD DE CONVENIO - " + cat_nombre,
                                          body: "Se comunica que el convenio " + cat_nombre + " esta próximo a caducar en " + Math.Abs(dias) + "día(s) , por favor ponerse en contacto con el encargado de los convenios o consulte con el administrador.\r\nGRACIAS POR USAR NUESTRO SISTEMA",
                                          recipientMail: new List<string> { address }
                                          );
                                caduco = true;
                            }
                        }
                }

            }
            reader.Close();
            command.Parameters.Clear();


            if (caduco == true && enviar == true)
            {
                command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO CONVENIO_CORREO VALUES(@cat_codigo, @cat_nombre, @fechahoy)";
                command.Parameters.AddWithValue("@cat_codigo", cat_codigo);
                command.Parameters.AddWithValue("@cat_nombre", cat_nombre);
                command.Parameters.AddWithValue("@fechahoy", DateTime.Now);
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                connection.Close();
            }
            else
                connection.Close();
        }
        public DataTable ConveniosPorCaducar(DateTime FechaInicio, DateTime FechaFin)
        {
            DataTable TablaConvenios = new DataTable();
            SqlConnection connection;
            SqlCommand command;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select CAT_CODIGO, CAT_NOMBRE, CAT_FECHA_FIN from CATEGORIAS_CONVENIOS where CAT_FECHA_FIN BETWEEN @fechainicio AND @fechafin AND CAT_ESTADO = 1";
            command.Parameters.AddWithValue("@fechainicio", FechaInicio);
            command.Parameters.AddWithValue("@fechafin", FechaFin);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            SqlDataReader reader = command.ExecuteReader();
            TablaConvenios.Load(reader);
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return TablaConvenios;
        }
        public DataTable cmdDivision()
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
            Sqlcmd = new SqlCommand("select codsub,dessub from Sic3000..ProductoSubdivision", Sqlcon);
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
        public List<SicProductoSubdivision> productosSubdivision()
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<SicProductoSubdivision> list = (from sp in db.SicProductoSubdivision select sp).ToList();
                return list;
            }
        }
        public bool InsertaProductosSubdivision(Int64 codsub)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;
            connection = obj.ConectarBd();
            connection.Open();

            transaction = connection.BeginTransaction();
            try
            {
                command = new SqlCommand("sp_guardarProductosSubdivision", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@codsub", codsub);
                command.CommandTimeout = 180;
                command.ExecuteNonQuery();
                command.Parameters.Clear();

                transaction.Commit();
                connection.Close();
                return true;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false;
            }
        }
        public bool EliminarSubdivision(Int64 codsub)
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
            Sqlcmd = new SqlCommand("delete from SicProductoSubdivision where codsub = " + codsub, Sqlcon);
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
        public List<SicProductoSubdivision> productosSubdivisionExiste(int codsub)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<SicProductoSubdivision> list = (from sp in db.SicProductoSubdivision
                                                     where sp.codsub == codsub
                                                     select sp).ToList();
                return list;
            }
        }
        public NIVEL_PISO recuperaPiso(Int64 NIV_CODIGO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from n in db.NIVEL_PISO
                        where n.NIV_CODIGO == NIV_CODIGO
                        select n).FirstOrDefault();
            }
        }
        public DataTable recuperaBodega(Int64 _codlocal)
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
            Sqlcmd = new SqlCommand("select nomlocal from Sic3000..Locales where codlocal = " + _codlocal, Sqlcon);
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
        public ACCESO_OPCIONES listAccesoOpciones()
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from a in db.ACCESO_OPCIONES
                        select a).FirstOrDefault();
            }
        }
        public List<ACCESO_OPCIONES> listaAccesoOpciones(Int64 id_modulo)
        {
            try
            {
                using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return db.ACCESO_OPCIONES.Where(x => x.MODULO.ID_MODULO == id_modulo).OrderBy(r => r.DESCRIPCION).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }
        public dsProcedimiento cargaPerfilesAcceso()
        {
            dsProcedimiento ds = new dsProcedimiento();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var cabecera = (from p in db.PERFILES
                                select p).OrderBy(x => x.DESCRIPCION).ToList();
                foreach (var x in cabecera)
                {
                    #region left join
                    //var prb = (from w in db.PERFILES_ACCESOS
                    //           from y in db.ACCESO_OPCIONES.Where(z => z.ID_ACCESO == w.ID_ACCESO).DefaultIfEmpty()
                    //           select new {
                    //               y.ID_ACCESO,
                    //               y.DESCRIPCION,
                    //               y.TIPO,
                    //               w.ID_PERFIL
                    //           } ).ToList();
                    #endregion
                    var detalle = (from ap in db.PERFILES_ACCESOS
                                   join a in db.ACCESO_OPCIONES on ap.ACCESO_OPCIONES.ID_ACCESO equals a.ID_ACCESO into left
                                   from y in left.DefaultIfEmpty()
                                   where ap.ID_PERFIL == x.ID_PERFIL
                                   select new
                                   {
                                       y.ID_ACCESO,
                                       y.DESCRIPCION,
                                       y.TIPO,
                                       ap.ID_PERFIL
                                   }).OrderBy(i => i.DESCRIPCION).ToList();
                    object[] cab = new object[]
                    {
                        x.ID_PERFIL,
                        x.DESCRIPCION
                    };
                    ds.Perfiles.Rows.Add(cab);
                    foreach (var y in detalle)
                    {
                        object[] det = new object[] {
                            y.ID_ACCESO,
                            y.DESCRIPCION,
                            y.TIPO,
                            y.ID_PERFIL
                        };
                        ds.Acceso.Rows.Add(det);
                    }
                }
            }
            return ds;
        }
        public Int16 maxPerfil()
        {
            Int16 id_perfil;
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                Int16 cod = (from p in db.PERFILES
                             orderby p.ID_PERFIL descending
                             select p.ID_PERFIL).FirstOrDefault();
                id_perfil = Convert.ToInt16(cod + 1);
                return id_perfil;
            }
        }
        public bool creaPerfil(PERFILES perfil)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                try
                {
                    db.AddToPERFILES(perfil);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                    //throw;
                }
            }
        }
        public bool editarPerfil(Int64 id_perfil, string descripcion)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                try
                {
                    PERFILES per = (from p in db.PERFILES
                                    where p.ID_PERFIL == id_perfil
                                    select p).FirstOrDefault();
                    per.DESCRIPCION = descripcion;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                    //throw;
                }
            }
        }
        public bool agregarAcceso(PERFILES_ACCESOS peracc)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                try
                {
                    db.Crear("PERFILES_ACCESOS", peracc);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                    //throw;
                }
            }
        }
        public bool eliminarAcceso(Int64 id_perfil, Int64 id_acceso)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                try
                {
                    PERFILES_ACCESOS peracc = db.PERFILES_ACCESOS.SingleOrDefault(x => x.ID_ACCESO == id_acceso && x.ID_PERFIL == id_perfil);
                    if (peracc != null)
                    {
                        db.DeleteObject(peracc);
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
                    //throw;
                }
            }
        }
        public PERFILES_ACCESOS buscaPerfilesacceso(Int64 perfil, Int64 acceso)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in db.PERFILES_ACCESOS
                        where p.ID_PERFIL == perfil && p.ID_ACCESO == acceso
                        select p).FirstOrDefault();
            }
        }
        public PERFILES buscaPerfiles(Int64 perfil)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in db.PERFILES
                        where p.ID_PERFIL == perfil
                        select p).FirstOrDefault();
            }
        }
        public bool eliminarPerfil(Int64 id_perfil)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                try
                {
                    PERFILES per = db.PERFILES.SingleOrDefault(x => x.ID_PERFIL == id_perfil);
                    if (per != null)
                    {
                        db.DeleteObject(per);
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
        public bool crearPerfilSic(string nomdep)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();
                command = new SqlCommand("insert into Sic3000..SeguridadGrupo values ((select top 1 codgru+1 from Sic3000..SeguridadGrupo order by 1 desc),(select top 1 codgru+1 from Sic3000..SeguridadGrupo order by 1 desc),'" + nomdep + "')", connection);
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
        public bool editarPerfilSic(Int64 coddep, string nomdep)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();
                command = new SqlCommand("update Sic3000..SeguridadGrupo set desgru ='" + nomdep + "' where codgru  = " + coddep, connection);
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
        public bool eliminarPerfilSic(Int64 codgru)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();
                command = new SqlCommand("update Sic3000..SeguridadUsuarioOpciones set staopc = 'N' where codusu in (select codusu from Sic3000..SeguridadesUsuarioGrupo where codgru = " + codgru + ") ", connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                connection.Open();
                command = new SqlCommand("delete from Sic3000..SeguridadesUsuarioGrupo where codgru = " + codgru, connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                connection.Open();
                command = new SqlCommand("delete from Sic3000..SeguridadGrupoOpciones where codgru = " + codgru, connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                connection.Open();
                command = new SqlCommand("delete from Sic3000..SeguridadGrupo where codgru = " + codgru, connection);
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
        public bool crearModuloSic(string nommod)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();
                command = new SqlCommand("insert into Sic3000..SeguridadesModulo values ((select top 1 codmod+1 from Sic3000..SeguridadesModulo order by 1 desc),'" + nommod + "',1)", connection);
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
        public bool editarModuloSic(Int64 codmod, string nommod)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();
                command = new SqlCommand("update Sic3000..SeguridadesModulo set nommod = '" + nommod + "' where codmod = " + codmod, connection);
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
        public bool eliminarModuloSic(Int64 codmod)
        {
            try
            {
                SqlCommand command;
                SqlConnection connection;
                SqlDataReader reader;
                BaseContextoDatos obj = new BaseContextoDatos();
                connection = obj.ConectarBd();
                connection.Open();
                command = new SqlCommand("delete from Sic3000..SeguridadUsuarioOpciones where codopc in(select codopc from Sic3000..SeguridadOpciones where codmod = " + codmod + ")", connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                connection.Open();
                command = new SqlCommand("delete from Sic3000..SeguridadGrupoOpciones where codopc in(select codopc from Sic3000..SeguridadOpciones where codmod = " + codmod + ")", connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                connection.Open();
                command = new SqlCommand("delete from Sic3000..SeguridadOpciones where codmod = " + codmod, connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                connection.Open();
                command = new SqlCommand("delete from Sic3000..SeguridadesModulo where codmod = " + codmod, connection);
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
        public List<DtoConsultasWeb> consultasWeb(DateTime desde, DateTime hasta)
        {
            List<DtoConsultasWeb> cw = new List<DtoConsultasWeb>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var cn = (from c in db.CONTROL_CONSULTA
                          join u in db.USUARIOS on c.usuario equals u.ID_USUARIO
                          join n in db.NIVEL_PISO_MAQUINA on c.ip equals n.IP_MAQUINA
                          where c.fechaConsulta > desde && c.fechaConsulta < hasta
                          select new { c, u, n }).ToList();
                foreach (var item in cn)
                {
                    DtoConsultasWeb cb = new DtoConsultasWeb();
                    cb.FECHA = item.c.fechaConsulta;
                    cb.USUARIO = item.u.APELLIDOS + " " + item.u.NOMBRES;
                    cb.TIPO_CONSULTA = item.c.tipoConsulta;
                    cb.IP_MAQUINA = item.c.ip;
                    cb.DESCRIPCION = item.n.NIV_DESCRIPCION;
                    cw.Add(cb);
                }
                return cw;
            }
        }
        public DataTable consultasWebSp(DateTime desde, DateTime hasta)
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
            Sqlcmd = new SqlCommand("select fechaConsulta as 'FECHA',U.APELLIDOS+' '+U.NOMBRES AS 'USUARIO',C.tipoConsulta AS 'TIPO CONSULTA',C.ip AS 'IP MAQUINA',N.NIV_DESCRIPCION AS 'DESCRIPCION' from His3000..CONTROL_CONSULTA c \n" +
"inner join  His3000..NIVEL_PISO_MAQUINA n on c.ip = n.IP_MAQUINA \n" +
"inner join His3000..USUARIOS u on c.usuario = u.ID_USUARIO \n" +
"WHERE C.fechaConsulta BETWEEN '" + desde + "' AND '" + hasta + "'", Sqlcon);
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
    }
}
