using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace His.Datos
{
    public class DatFormaPago
    {
        public List<TIPO_FORMA_PAGO> RecuperaTipoFormaPagos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in contexto.TIPO_FORMA_PAGO
                        where t.TIF_ACTIVO == true
                        orderby t.TIF_NOMBRE
                        select t).ToList();
                //return contexto.TIPO_FORMA_PAGO.OrderBy(t=>t.TIF_NOMBRE).ToList();
            }
        }

        public DataTable RecuperarClasificacion()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("SELECT codclas as TIF_CODIGO, desclas as TIF_NOMBRE FROM Sic3000..Clasificacion where cartera = 1 order by 2 asc", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }
        public List<DtoFormaPago> RecuperaFormaPagos()
        {
            List<DtoFormaPago> formapagogrid = new List<DtoFormaPago>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                List<FORMA_PAGO> formap = new List<FORMA_PAGO>();
                formap = contexto.FORMA_PAGO.Include("TIPO_FORMA_PAGO").ToList();
                foreach (var acceso in formap)
                {
                    formapagogrid.Add(new DtoFormaPago()
                    {
                        ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName(),
                        ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key,
                        FOR_ACTIVO = acceso.FOR_ACTIVO,
                        FOR_CODIGO = acceso.FOR_CODIGO,
                        FOR_COMISION = acceso.FOR_COMISION == null ? decimal.Parse("0") : decimal.Parse(acceso.FOR_COMISION.ToString()),
                        FOR_CUENTA_CONTABLE = acceso.FOR_CUENTA_CONTABLE,
                        FOR_DESCRIPCION = acceso.FOR_DESCRIPCION,
                        FOR_ESTADO = acceso.FOR_ESTADO,
                        FOR_REFERIDO = acceso.FOR_REFERIDO == null ? decimal.Parse("0") : decimal.Parse(acceso.FOR_REFERIDO.ToString()),
                        TIF_CODIGO = acceso.TIPO_FORMA_PAGO.TIF_CODIGO,
                        TIF_NOMBRE = acceso.TIPO_FORMA_PAGO.TIF_NOMBRE,
                    });
                }
                return formapagogrid;
            }
        }
        public void GrabarFormaPago(FORMA_PAGO formapagoModificada, FORMA_PAGO formapagoOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                contexto.Grabar(formapagoModificada, formapagoOriginal);
            }
        }

        public List<FORMA_PAGO> listaFormasPago()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<FORMA_PAGO> pagos = new List<FORMA_PAGO>();
                pagos = contexto.FORMA_PAGO.Include("TIPO_FORMA_PAGO").ToList();
                return pagos;
            }

        }
        /// <summary>
        /// Retorna la lista de Formas de Pago
        /// </summary>
        /// <param name="codigoTipo">filtro por tipo</param>
        /// <returns>lista de Formas de Pago</returns>
        public List<FORMA_PAGO> RecuperaFormaPago(Int16 codigoTipo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from f in contexto.FORMA_PAGO
                        join t in contexto.TIPO_FORMA_PAGO on f.TIPO_FORMA_PAGO.TIF_CODIGO equals t.TIF_CODIGO
                        where t.TIF_CODIGO == codigoTipo && f.FOR_ESTADO == true
                        orderby f.FOR_DESCRIPCION
                        select f).ToList();
            }
        }
        /// <summary>
        /// Retorna el objeto FORMA_PAGO
        /// </summary>
        /// <param name="codigoFormaPago">id del objeto</param>
        /// <returns>Objeto Forma de Pago</returns>
        public FORMA_PAGO RecuperaFormaPagoID(Int16 codigoFormaPago)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                FORMA_PAGO pago = contexto.FORMA_PAGO.Include("TIPO_FORMA_PAGO").FirstOrDefault(f => f.FOR_CODIGO == codigoFormaPago);
                return pago;
            }
        }

        public FORMA_PAGO FormaPagoID(int codPago)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from f in contexto.FORMA_PAGO
                        join t in contexto.TIPO_FORMA_PAGO on f.TIPO_FORMA_PAGO.TIF_CODIGO equals t.TIF_CODIGO
                        where f.FOR_CODIGO == codPago
                        select f).FirstOrDefault();
            }
        }

        public FORMA_PAGO FormaPagoAseguradoraEmpresa(int codAsegEmp)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from f in contexto.FORMA_PAGO
                        where f.ASE_CODIGO == codAsegEmp
                        select f).FirstOrDefault();
            }
        }

        public DataTable RecuperaDescuento(string ATE_CODIGO, int CodigoAseguradora)
        {
            // RECUPERO DESCUENTO DESDE EL SIC 18/06/2013 PABLO ROCHA

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_RecuperaDescuentoSic", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = ATE_CODIGO;

            Sqlcmd.Parameters.Add("@CodigoAseguradora", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoAseguradora"].Value = CodigoAseguradora;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return Dts;
        }

        public DataTable RecuperaFormaPagoSIC(string ATE_FACTURA)
        {
            // RECUPERO FORMA PAGO DESDE EL SIC 19/06/2013 PABLO ROCHA

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_RecuperaFormaPagoSic", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_FACTURA", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_FACTURA"].Value = ATE_FACTURA;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return Dts;
        }

        public DataTable ClienteFacturado(int ATE_FACTURA)
        {
            // RECUPERO CLIENTE FACTURADO DESDE EL SIC 31/05/2018 PABLO ROCHA

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_RecuperaDatosClienteFacturado", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_FACTURA", SqlDbType.Int);
            Sqlcmd.Parameters["@ATE_FACTURA"].Value = ATE_FACTURA;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return Dts;
        }

        public DataTable DescuentoClienteFacturado(int ATE_FACTURA)
        {
            // RECUPERO DESCUENTO CLIENTE FACTURADO DESDE EL SIC 31/05/2018 PABLO ROCHA

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_RecuperaDatosDescuentoClienteFacturado", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_FACTURA", SqlDbType.Int);
            Sqlcmd.Parameters["@ATE_FACTURA"].Value = ATE_FACTURA;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return Dts;
        }

        public DataTable RecuperaCodiSri(string ATE_FACTURA)
        {
            // RECUPERO CODIGO SRI DESDE EL SIC 09/02/2018 PABLO ROCHA

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_RecuperaCodiSri", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_FACTURA", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATE_FACTURA"].Value = ATE_FACTURA;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return Dts;
        }

        public void ActualizarFormaPago(string forpag, int for_codigo, string descripcion, double comision, double referido)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_FormaPagoActualizar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@forpag", forpag);
            command.Parameters.AddWithValue("@for_codigo", for_codigo);
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.Parameters.AddWithValue("@comision", comision);
            command.Parameters.AddWithValue("@referido", referido);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }

        public void CrearFormaPago(string forpag, int for_codigo, string descripcion, double comision, double referido)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_FormaPagoCrear", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@forpag", forpag);
            command.Parameters.AddWithValue("@for_codigo", for_codigo);
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.Parameters.AddWithValue("@comision", comision);
            command.Parameters.AddWithValue("@referido", referido);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
        public bool NoRepetir(string descripcion)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            bool existe = false;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("SELECT FOR_DESCRIPCION FROM FORMA_PAGO WHERE FOR_DESCRIPCION = @descripcion", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@descripcion", descripcion.Trim());
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (descripcion.Trim() == reader["FOR_DESCRIPCION"].ToString())
                    existe = true;
                else
                    existe = false;
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return existe;
        }
        public int UltimocodigoFormaPago()
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            int for_codigo = 0;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("SELECT MAX(FOR_CODIGO) AS FOR_CODIGO FROM FORMA_PAGO", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                for_codigo = Convert.ToInt32(reader["FOR_CODIGO"].ToString()) + 1;
            }
            reader.Close();
            connection.Close();
            return for_codigo;
        }
        public DataTable AnticipoFormaPago(string numrec)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select forpag, despag from SIC3000..Anticipo where numrec = @numrec", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@numrec", numrec);
            reader = command.ExecuteReader();
            tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            return tabla;
        }
        public FORMA_PAGO recuperarFormaPago_forpag(string forpag)
        {
            using(var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                FORMA_PAGO fp = (from f in db.FORMA_PAGO
                                 where f.forpag == forpag
                                 select f).FirstOrDefault();

                return fp;
            }
        }
    }
}
