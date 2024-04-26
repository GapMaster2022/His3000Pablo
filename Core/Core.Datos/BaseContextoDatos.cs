using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.EntityClient;
using System.Configuration;
using Core.Utilitarios;
using System.Data.SqlClient;


namespace Core.Datos
{
    public class BaseContextoDatos
    {
        /// <summary>
        /// Tipo de Autenticación Windows WIN
        /// </summary>
        private const string WINDOWS_AUTHENTICATION = "WIN";
        private const string PROVIDER = "System.Data.SqlClient";
        public static EntityConnection Conexion = null;
        SqlConnection conexion;
        /// <summary>
        /// Conexion al Modelo de Datos de Evaluadora
        /// </summary>
        /// <returns></returns>
        public static EntityConnection ConexionEDM()
        {
            if (Conexion == null)
            {
                string conexion = EncriptadorUTF.Desencriptar(ConfigurationManager.AppSettings["Conexion"]);
                string DataSource = conexion.Split(';')[0];
                string DatabaseName = conexion.Split(';')[1];
                string UserName = conexion.Split(';')[2];
                string Password = conexion.Split(';')[3];
                string Metadata = conexion.Split(';')[4];
                Sesion.nombreBaseDatos = DatabaseName;
                Sesion.nombreServidor = DataSource;
                Sesion.usrBaseDatos = UserName;
                Sesion.pwdBaseDatos = Password; 
                string conexionRecuperada = ConstruirConfiguracionConexionSqlServer(DataSource, DatabaseName, UserName, Password, Metadata);
                Conexion = new EntityConnection(conexionRecuperada);
                return Conexion;
            }
            else
                return Conexion;
        }

        
        private static string ConstruirConfiguracionConexionSqlServer(string DataSource, string DatabaseName, string UserName, string Password, string Metadata)
        {
            SqlConnectionStringBuilder constructorCadena = new SqlConnectionStringBuilder();
            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();

            constructorCadena.DataSource = DataSource;
            constructorCadena.InitialCatalog = DatabaseName;
            if (!constructorCadena.IntegratedSecurity)
            {
                constructorCadena.UserID = UserName;
                constructorCadena.Password = Password;
            }
            constructorCadena.MultipleActiveResultSets = true;

            entityBuilder.Metadata = string.Format("res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl", Metadata);
            entityBuilder.ProviderConnectionString = constructorCadena.ConnectionString;
            entityBuilder.Provider = PROVIDER;

            return string.Format("{0} {1}", entityBuilder.ConnectionString, "");
          
         
        }

        /// <summary>
        /// Método para Conexion Base de Datos His3000
        /// </summary>
        /// <returns></returns>
        public SqlConnection ConectarBd()
        {
            string conexionBd = EncriptadorUTF.Desencriptar(ConfigurationManager.AppSettings["Conexion"]);
            string DataSource = conexionBd.Split(';')[0];
            string DatabaseName = conexionBd.Split(';')[1];
            string UserName = conexionBd.Split(';')[2];
            string Password = conexionBd.Split(';')[3];

            string cadena_conexion = "Data Source="+DataSource+";Initial Catalog="+DatabaseName+";User ID="+UserName+";Password="+Password;

           
            conexion = new SqlConnection(cadena_conexion);
            return conexion;
        }



        /// <summary>
        /// Método para Conexion Base de Datos Sic3000
        /// </summary>
        /// <returns></returns>
        public SqlConnection ConectarBdSic3000()
        {
            string conexionBd = EncriptadorUTF.Desencriptar(ConfigurationManager.AppSettings["ConexionSic3000"]);
            string DataSource = conexionBd.Split(';')[0];
            string DatabaseName = conexionBd.Split(';')[1];
            string UserName = conexionBd.Split(';')[2];
            string Password = conexionBd.Split(';')[3];

            string cadena_conexion = "Data Source=" + DataSource + ";Initial Catalog=" + DatabaseName + ";User ID=" + UserName + ";Password=" + Password;


            conexion = new SqlConnection(cadena_conexion);
            return conexion;
        }

        public static EntityConnection ConexionSic3000()
        {
            if (Conexion == null)
            {
                string conexion = EncriptadorUTF.Desencriptar(ConfigurationManager.AppSettings["ConexionSic3000"]);
                string DataSource = conexion.Split(';')[0];
                string DatabaseName = conexion.Split(';')[1];
                string UserName = conexion.Split(';')[2];
                string Password = conexion.Split(';')[3];
                string Metadata = conexion.Split(';')[4];
                Sesion.nombreBaseDatos = DatabaseName;
                Sesion.nombreServidor = DataSource;
                Sesion.usrBaseDatos = UserName;
                Sesion.pwdBaseDatos = Password;
                string conexionRecuperada = ConstruirConfiguracionConexionSqlServer(DataSource, DatabaseName, UserName, Password, Metadata);
                Conexion = new EntityConnection(conexionRecuperada);
                return Conexion;
            }
            else
                return Conexion;
        }
        public SqlConnection ConectarBdCg3000()
        {
            string conexionBd = EncriptadorUTF.Desencriptar(ConfigurationManager.AppSettings["ConexionCg3000"]);
            string DataSource = conexionBd.Split(';')[0];
            string DatabaseName = conexionBd.Split(';')[1];
            string UserName = conexionBd.Split(';')[2];
            string Password = conexionBd.Split(';')[3];

            string cadena_conexion = "Data Source=" + DataSource + ";Initial Catalog=" + DatabaseName + ";User ID=" + UserName + ";Password=" + Password;


            conexion = new SqlConnection(cadena_conexion);
            return conexion;
        }
        public static EntityConnection ConexionCg3000()
        {
            if (Conexion == null)
            {
                string conexion = EncriptadorUTF.Desencriptar(ConfigurationManager.AppSettings["ConexionCg3000"]);
                string DataSource = conexion.Split(';')[0];
                string DatabaseName = conexion.Split(';')[1];
                string UserName = conexion.Split(';')[2];
                string Password = conexion.Split(';')[3];
                string Metadata = conexion.Split(';')[4];
                Sesion.nombreBaseDatos = DatabaseName;
                Sesion.nombreServidor = DataSource;
                Sesion.usrBaseDatos = UserName;
                Sesion.pwdBaseDatos = Password;
                string conexionRecuperada = ConstruirConfiguracionConexionSqlServer(DataSource, DatabaseName, UserName, Password, Metadata);
                Conexion = new EntityConnection(conexionRecuperada);
                return Conexion;
            }
            else
                return Conexion;
        }

        public SqlConnection ConectarBdSeries3000()
        {
            string conexionBd = EncriptadorUTF.Desencriptar(ConfigurationManager.AppSettings["ConexionSeries3000"]);
            string DataSource = conexionBd.Split(';')[0];
            string DatabaseName = conexionBd.Split(';')[1];
            string UserName = conexionBd.Split(';')[2];
            string Password = conexionBd.Split(';')[3];

            string cadena_conexion = "Data Source=" + DataSource + ";Initial Catalog=" + DatabaseName + ";User ID=" + UserName + ";Password=" + Password;


            conexion = new SqlConnection(cadena_conexion);
            return conexion;
        }
        public static EntityConnection ConexionSeries3000()
        {
            if (Conexion == null)
            {
                string conexion = EncriptadorUTF.Desencriptar(ConfigurationManager.AppSettings["ConexionSeries3000"]);
                string DataSource = conexion.Split(';')[0];
                string DatabaseName = conexion.Split(';')[1];
                string UserName = conexion.Split(';')[2];
                string Password = conexion.Split(';')[3];
                string Metadata = conexion.Split(';')[4];
                Sesion.nombreBaseDatos = DatabaseName;
                Sesion.nombreServidor = DataSource;
                Sesion.usrBaseDatos = UserName;
                Sesion.pwdBaseDatos = Password;
                string conexionRecuperada = ConstruirConfiguracionConexionSqlServer(DataSource, DatabaseName, UserName, Password, Metadata);
                Conexion = new EntityConnection(conexionRecuperada);
                return Conexion;
            }
            else
                return Conexion;
        }
        public SqlConnection ConectarBdSERIES3000()
        {
            string conexionBd = EncriptadorUTF.Desencriptar(ConfigurationManager.AppSettings["ConexionSeries3000"]);
            string DataSource = conexionBd.Split(';')[0];
            string DatabaseName = conexionBd.Split(';')[1];
            string UserName = conexionBd.Split(';')[2];
            string Password = conexionBd.Split(';')[3];

            string cadena_conexion = "Data Source=" + DataSource + ";Initial Catalog=" + DatabaseName + ";User ID=" + UserName + ";Password=" + Password;


            conexion = new SqlConnection(cadena_conexion);
            return conexion;
        }
        public string ObtenerIP()
        {
            string conexionBd = EncriptadorUTF.Desencriptar(ConfigurationManager.AppSettings["Conexion"]);
            string DataSource = conexionBd.Split(';')[0];
            return DataSource;
      

         
        }


    }
}
