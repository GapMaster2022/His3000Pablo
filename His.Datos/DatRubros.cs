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
    public class DatRubros
    {
        public Int16 RecuperaMaximoCaja()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from c in contexto.CAJAS
                             select c.CAJ_CODIGO).ToList();
                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }

        }

        public int recuperaFacturaInicial()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from c in contexto.NUMERO_CONTROL_CAJAS
                             select c.NCC_FACTURA_INICIAL).ToList();
                if (lista.Count > 0)
                    return (Int32)lista.Max();

                return 0;
            }

        }
        public List<CAJAS> ListaCajas()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.CAJAS.Include("LOCALES").ToList();
            }
        }
        public List<DtoCajas> RecuperaCajas()
        {
            List<DtoCajas> grid = new List<DtoCajas>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<CAJAS> ciudad = new List<CAJAS>();
                ciudad = contexto.CAJAS.Include("LOCALES").ToList();

                //var lista = (from c in contexto.CAJAS
                //          join l in contexto.LOCALES on c.LOCALES.LOC_CODIGO equals l.LOC_CODIGO
                //          select new
                //          {
                //              c.CAJ_AUTORIZACION_SRI,
                //              c.CAJ_CODIGO,
                //              c.CAJ_ESTADO,
                //              c.CAJ_FECHA,
                //              c.CAJ_NUMERO,
                //              c.CAJ_NOMBRE,
                //              c.CAJ_PERIDO_VALIDEZ,
                //              l.LOC_CODIGO,
                //              l.LOC_NOMBRE,
                //              c.EntityKey
                //          }).ToList();


                    foreach (var acceso in ciudad)
                    {
                        grid.Add(new DtoCajas()
                        {
                            CAJ_AUTORIZACION_SRI = acceso.CAJ_AUTORIZACION_SRI,
                            CAJ_CODIGO = acceso.CAJ_CODIGO,
                            CAJ_ESTADO = acceso.CAJ_ESTADO,
                            CAJ_FECHA = acceso.CAJ_FECHA,
                            CAJ_NOMBRE = acceso.CAJ_NOMBRE,
                            CAJ_NUMERO = acceso.CAJ_NUMERO,
                            CAJ_PERIDO_VALIDEZ = Convert.ToDateTime(acceso.CAJ_PERIDO_VALIDEZ),
                            LOC_CODIGO = acceso.LOCALES.LOC_CODIGO,
                            LOC_NOMBRE = acceso.LOCALES.LOC_NOMBRE,
                            ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName()
                            ,
                            ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key
                        });
                    }

                return grid;
            }
        }
        public void CrearCaja(CAJAS caja)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("CAJAS", caja);
            }
        }
        public void GrabarCaja(CAJAS cajaModificada, CAJAS cajaOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(cajaModificada, cajaOriginal);
            }
        }
        public void EliminarCaja(CAJAS caja)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(caja);
            }
        }

        public CAJAS RecuperarCajaID(Int16 codCaja)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from c in contexto.CAJAS
                        where c.CAJ_CODIGO == codCaja
                        select c).FirstOrDefault();
            }
        }

        //public List<RUBROS> recuperarRubros()
        //{
        //    try
        //    {
        //        using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //        {
        //            return contexto.RUBROS.ToList();
        //        }
        //    }
        //    catch (Exception err) { throw err; }
        //}

        public int RecuperaMaximoRubro()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from c in contexto.RUBROS
                             select c.RUB_CODIGO).ToList();
                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }

        }

        /// <summary>
        /// Método que permite recuperar todos los Rubros 
        /// </summary>
        /// <returns>retorna una lista con los Rubros</returns>
        public List<RUBROS> recuperarRubros()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.RUBROS.OrderBy(r=>r.RUB_ORDEN_IMPRESION).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }

        /// <summary>
        /// Método que recupera los rubros asociados a una Área de Pedidos
        /// </summary>
        /// <returns></returns>
        public List<RUBROS> recuperarRubros(int codPedidoArea)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var lista = (from r in contexto.RUBROS
                                 where r.PED_CODIGO == codPedidoArea
                                 select r).ToList();
                    return lista;
                }
            }
            catch (Exception err) { throw err; }
        }
        public bool ParametroServicios()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
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
            Sqlcmd = new SqlCommand("sp_ParametroServicios", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = Sqlcmd.ExecuteReader();
            bool parametro = false;
            while (reader.Read())
            {
                parametro = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            reader.Close();
            Sqlcon.Close();
            return parametro;
        }

        public bool ParametroGarantia()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
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
            Sqlcmd = new SqlCommand("sp_ParametroGarantia", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = Sqlcmd.ExecuteReader();
            bool parametro = false;
            while (reader.Read())
            {
                parametro = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            reader.Close();
            Sqlcon.Close();
            return parametro;
        }

        public DataTable getDepartamentos()
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("SELECT coddep, desdep FROM Sic3000..ProductoDepartamento", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public DataTable getCuentas(DateTime desde, DateTime hasta, bool ingreso, bool alta, string hc, bool cero)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();
            string xWhere = "";

            if (ingreso)
            {
                xWhere = xWhere + "AND A.ATE_FECHA_INGRESO BETWEEN '" + desde + "' AND '" + hasta + "'\r\n";
            }
            if (alta)
            {
                xWhere = xWhere + " AND A.ATE_FECHA_ALTA BETWEEN '" + desde + "' AND '" + hasta + "'\r\n";
            }
            if (hc != "0")
            {
                xWhere = xWhere + " AND P.PAC_HISTORIA_CLINICA = " + hc + "\r\n";
            }
            if (cero)
                xWhere = xWhere + " AND isnull(dbo.f_EncuentraValores(A.ATE_CODIGO), 0) > 0 \r\n";
            command = new SqlCommand("SELECT CONCAT(P.PAC_APELLIDO_PATERNO, ' ', P.PAC_APELLIDO_MATERNO, ' ', P.PAC_NOMBRE1, ' ', P.PAC_NOMBRE2) AS PACIENTE, P.PAC_HISTORIA_CLINICA AS HCL, " +
            "A.ATE_CODIGO CODIGO_UNICO_ATENCION, A.ATE_NUMERO_ATENCION AS 'NRO ATENCION', A.ATE_FECHA_INGRESO AS 'F. INGRESO', A.ATE_FECHA_ALTA AS 'F. ALTA', isnull(dbo.f_EncuentraValores(A.ATE_CODIGO), 0) as VALOR, " +
            "CC.CAT_NOMBRE AS ASEGURADORA, A.ATE_FACTURA_PACIENTE as CAJERO  " +
            "FROM ATENCIONES A " +
            "INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO " +
            "INNER JOIN ATENCION_DETALLE_CATEGORIAS AD ON A.ATE_CODIGO = AD.ATE_CODIGO " +
            "INNER JOIN CATEGORIAS_CONVENIOS CC ON AD.CAT_CODIGO = CC.CAT_CODIGO " +
            "WHERE ESC_CODIGO IN (2, 3) " +
            xWhere + " ORDER BY VALOR ASC ", connection);
            command.CommandType = CommandType.Text;
            //command.Parameters.AddWithValue("@desde", desde);
            //command.Parameters.AddWithValue("@hasta", desde);
            //command.Parameters.AddWithValue("@hc", hc);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }
    }
}
