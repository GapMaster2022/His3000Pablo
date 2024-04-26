using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using System.Data;
using System.Data.SqlClient;

namespace His.Datos
{
    public class DatParametros
    {
        public List<DtoParametros> RecuperaParametros(Int16 codigopar)
        {
            try
            {
                List<DtoParametros> parametros = new List<DtoParametros>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<PARAMETROS_DETALLE> medicos = new List<PARAMETROS_DETALLE>();
                    medicos = contexto.PARAMETROS_DETALLE.Include("PARAMETROS").Where(cod => cod.PARAMETROS.PAR_CODIGO == codigopar).ToList();
                    foreach (var acceso in medicos)
                    {
                        parametros.Add(new DtoParametros()
                        {
                            PAD_ACTIVO = acceso.PAD_ACTIVO == null ? false : bool.Parse(acceso.PAD_ACTIVO.ToString()),
                            PAD_CODIGO = acceso.PAD_CODIGO,
                            PAD_NOMBRE = acceso.PAD_NOMBRE,
                            PAD_TIPO = acceso.PAD_TIPO,
                            PAD_VALOR = acceso.PAD_VALOR,
                            PAR_CODIGO = acceso.PARAMETROS.PAR_CODIGO,

                            ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName(),
                            ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key
                        });
                    }
                    return parametros;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public PARAMETROS_DETALLE RecuperaPorCodigo(int codigo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from c in contexto.PARAMETROS_DETALLE
                            where c.PAD_CODIGO == codigo
                            select c).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public FORMULARIOS_HCU RecuperaFechaPorCodigo(int codigo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from c in contexto.FORMULARIOS_HCU
                            where c.FH_CODIGO == codigo
                            select c).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable RecuepraHorasyLitros(int hora)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
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

            Sqlcmd = new SqlCommand("sp_RecuepraHorasyLitros", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.AddWithValue("@hora", hora);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts.Tables["tabla"];
        }

        public bool EDITAEVOLUCION()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool valido = false;
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("SELECT PAD_ACTIVO FROM PARAMETROS_DETALLE WHERE PAD_CODIGO = 23 ", Sqlcon); //SIRVE PARA EDITAR LA EVOLUCION
            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                valido = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return valido;
        }
        public bool ParametroAdmisionDesactivacion()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool valido = false;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_ParametroAdmisionDesactivar", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                valido = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            reader.Close();
            connection.Close();
            return valido;
        }

        public double ParametroIva()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            double valido = 0;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("select iva from Sic3000..Parametros", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                valido = Convert.ToDouble(reader["iva"].ToString());
            }
            reader.Close();
            connection.Close();
            return valido;
        }

        public decimal ProductoPagaIVA(Int64 codPro)
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
            Sqlcmd = new SqlCommand("select paga_iva from Sic3000..Producto where codpro = " + codPro, Sqlcon);

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

            if (Dts.Rows.Count > 0)
            {
                return (Convert.ToDecimal(Dts.Rows[0][0]));
            }
            else
            {
                return 0;
            }

        }

        public bool ParametroDevolucionBienes()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool valido = false;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_ParametroDevolucionBienes", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                valido = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            reader.Close();
            connection.Close();
            return valido;
        }

        public double ParametroBodegaQuirofano()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            double valido = 0;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_ParametroBodegaQuirofano", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                valido = Convert.ToDouble(reader["PAD_VALOR"].ToString());
            }
            reader.Close();
            connection.Close();
            return valido;
        }
        public bool ParametroComboQuirofano()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool valido = false;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_ParametroComboQuirofano", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                valido = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            reader.Close();
            connection.Close();
            return valido;
        }

        public string RutaLogo(string tipo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            string valido = "";
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("RutasLogo", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@tipo", tipo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                valido = reader["LEM_RUTA"].ToString();
            }
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return valido;
        }
        public DataTable Congregaciones(int tipo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_MostrarCongregaciones", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@tipo", tipo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public bool ParametroAuditoria() //Parametro para ver pcientes en auditoria
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool activo = false;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_ParametroAuditoria", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                activo = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return activo;
        }
        public bool ParametroAdmisionAcceso() //Parametro para accesos a admision
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool activo = false;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_ParametroAdmisionAcceso", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                activo = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return activo;
        }

        public DataTable ParametroHabitacionDefault()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_ParametroHabitacionPiso", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }
        public bool ParametroReceta() //Parametro para ver pcientes en auditoria
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool activo = false;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_ParametroReceta", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                activo = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return activo;
        }

        public bool ParametroFacturaHisSic() //Parametro para ver pcientes en auditoria
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool activo = false;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_ParametroFacturaHisSic", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                activo = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return activo;
        }
        public bool ParametroFormularios() //Parametro para LOS FORMULARIOS MSP
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool activo = false;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_ParametroFormularios", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                activo = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return activo;
        }
        public int ParametroBodegaGastro() //Parametro para la bodega de gastro
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            int activo = 0;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_ParametroBodegaGastro", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                activo = Convert.ToInt32(reader["PAD_VALOR"].ToString());
            }
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return activo;
        }
        public bool ParametroFacturaFecha()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool activo = false;

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ParametroFechaFactura", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                activo = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return activo;
        }
        public bool ParametroReposicionesBienes()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool activo = false;

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ParametroReposicionBienes", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                activo = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return activo;
        }
        public Int64 RecuperaValorParSvXcodigo(int codigo)
        {
            Int64 val = 0;
            try
            {
                using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var valor = (from p in db.PARAMETROS_DETALLE
                                 where p.PAD_CODIGO == codigo && p.PAD_ACTIVO == true
                                 select p).FirstOrDefault();
                    val = Convert.ToInt64(valor.PAD_VALOR);
                    return val;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public LOGOS_EMPRESA logosEmpresa(Int64 LEM_CODIGO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from le in db.LOGOS_EMPRESA
                        where le.LEM_CODIGO == LEM_CODIGO
                        select le).FirstOrDefault();
            }
        }
        public TABLA17sri tablaIva()
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in db.TABLA17sri
                        where t.ACTIVO == 1
                        select t).FirstOrDefault();
            }
        }
    }
}
