using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Core.Datos;

namespace His.Mantenimiento.Datos
{
    class CambioEstadosCuentas
    {

        public int PermisosActualizacionCuentas(int Usuario, int CuentaDesde, int CuentaHacia)
        {
            // VERIFICA SI UN USUARIO TIENES LOS PERMISOS NECESARIOS PARA CAMBIAR EL ESTADO DE UNA CUENTA / GIOVANNY TAPIA / 07/08/2012

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
            Sqlcmd = new SqlCommand("sp_InsertaUsuarioEstadosCuenta", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@IdUsuario", SqlDbType.Int);
            Sqlcmd.Parameters["@IdUsuario"].Value = Usuario;

            Sqlcmd.Parameters.Add("@EstadoDesde", SqlDbType.Int);
            Sqlcmd.Parameters["@EstadoDesde"].Value = CuentaDesde;

            Sqlcmd.Parameters.Add("@EstadoHacia", SqlDbType.Int);
            Sqlcmd.Parameters["@EstadoHacia"].Value = CuentaHacia;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return 1;
        }

        public DataTable CargaUsuarios(int Operacion)
        {
            // VERIFICA SI UN USUARIO TIENES LOS PERMISOS NECESARIOS PARA CAMBIAR EL ESTADO DE UNA CUENTA / GIOVANNY TAPIA / 07/08/2012

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
            Sqlcmd = new SqlCommand("sp_CargaUsuarios", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Operacion", SqlDbType.Int);
            Sqlcmd.Parameters["@Operacion"].Value = Operacion;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return Dts;
        }

        public DataTable CargaEstadosCuentas()
        {
            // VERIFICA SI UN USUARIO TIENES LOS PERMISOS NECESARIOS PARA CAMBIAR EL ESTADO DE UNA CUENTA / GIOVANNY TAPIA / 07/08/2012

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
            Sqlcmd = new SqlCommand("sp_CargaEstadosCuentas", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

             return Dts;
            
        }


        public DataTable CargaListadoPermisosCuentas(int Usuario)
        {
            // VERIFICA SI UN USUARIO TIENES LOS PERMISOS NECESARIOS PARA CAMBIAR EL ESTADO DE UNA CUENTA / GIOVANNY TAPIA / 07/08/2012

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
            Sqlcmd = new SqlCommand("sp_ListadoPermisosCambiosEstadosCuentas", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@IdUsuario", SqlDbType.Int);
            Sqlcmd.Parameters["@IdUsuario"].Value = Usuario;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return Dts;

        }

        public int BorraPermisosCuentasUsuarios(int Usuario, int CuentaDesde, int CuentaHacia)
        {
            // VERIFICA SI UN USUARIO TIENES LOS PERMISOS NECESARIOS PARA CAMBIAR EL ESTADO DE UNA CUENTA / GIOVANNY TAPIA / 07/08/2012

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
            Sqlcmd = new SqlCommand("sp_eliminaUsariosEstadosCuenta", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@IdUsuario", SqlDbType.Int);
            Sqlcmd.Parameters["@IdUsuario"].Value = Usuario;

            Sqlcmd.Parameters.Add("@EstadoDesde", SqlDbType.Int);
            Sqlcmd.Parameters["@EstadoDesde"].Value = CuentaDesde;

            Sqlcmd.Parameters.Add("@EstadoHacia", SqlDbType.Int);
            Sqlcmd.Parameters["@EstadoHacia"].Value = CuentaHacia;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return 1;
        }


    }
}
