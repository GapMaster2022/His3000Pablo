using Core.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using His.Entidades;
using System.Data.Common;

namespace His.Datos
{
    public class DatIngestaEliminacion
    {
        public HC_INGESTA_ELIMINACION recuperaIngestaXcodigo (Int64 IE_CODIGO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from h in db.HC_INGESTA_ELIMINACION where h.IE_CODIGO == IE_CODIGO select h).FirstOrDefault();
            }
        }
        //public static int compararFechas(DateTime x, DateTime y)
        //{
        //    //Compara las fechas primero
        //    int dateComparison = x.Date.CompareTo(y.Date);
        //    if (dateComparison != 0)
        //        return dateComparison;

        //    //Si las fechas son iguales, compara las horas
        //    return x.TimeOfDay.CompareTo(y.TimeOfDay);
        //}
        public void GrabarIngElm(int ATE_CODIGO, int PAC_CODIGO, DateTime IE_FECHA)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw;
            }
            command = new SqlCommand("sp_guardarIngElm", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ATE_CODIGO", ATE_CODIGO);
            command.Parameters.AddWithValue("@PAC_CODIGO", PAC_CODIGO);
            command.Parameters.AddWithValue("@IE_FECHA", IE_FECHA);
            command.CommandTimeout = 180;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            command.Parameters.Clear();
            connection.Close();

        }
        public DataTable ExistenciaIngElm(int ATE_CODIGO, int PAC_CODIGO)
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
            Sqlcmd = new SqlCommand("select * from HC_INGESTA_ELIMINACION WHERE ATE_CODIGO =" + ATE_CODIGO + " AND PAC_CODIGO=" + PAC_CODIGO, Sqlcon);
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
        public DataTable getIngElm(Int64 ate_codigo)
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
            Sqlcmd = new SqlCommand("select IE_CODIGO,'[PACIENTE:'+P.PAC_NOMBRE1+' '+P.PAC_APELLIDO_PATERNO+'] - [FECHA:'+CONVERT(VARCHAR,I.IE_FECHA,23)+']' AS DATOS  from HC_INGESTA_ELIMINACION i inner join PACIENTES p on i.PAC_CODIGO = p.PAC_CODIGO WHERE i.ATE_CODIGO = " + ate_codigo + " ORDER BY I.IE_FECHA DESC ", Sqlcon);
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
        public bool GrabarDetalleIngElm(HC_INGESTA_ELIMINACION_DETALLE ied, string MODO)
        {
            if (MODO == "SAVE")
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    ConexionEntidades.ConexionEDM.Open();
                    DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                    try
                    {
                        //var maxNumeroIngesta = contexto.HC_INGESTA_ELIMINACION_DETALLE.OrderByDescending(z => z.IED_CODIGO).FirstOrDefault();
                        //ied.IED_CODIGO = maxNumeroIngesta.IED_CODIGO + 1; //Aumenta automaticamente al último código
                        contexto.Crear("HC_INGESTA_ELIMINACION_DETALLE", ied);
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
            else
            {
                using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    ConexionEntidades.ConexionEDM.Open();
                    DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();
                    try
                    {
                        HC_INGESTA_ELIMINACION_DETALLE x = (from d in db.HC_INGESTA_ELIMINACION_DETALLE
                                                            where d.IED_CODIGO == ied.IED_CODIGO
                                                            select d).FirstOrDefault();
                        x.IED_HORA = ied.IED_HORA;
                        x.IED_CLASE = ied.IED_CLASE;
                        x.IED_CANTIDAD = ied.IED_CANTIDAD;
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
            //Codigo se comenta se pasa a trabajar con entity// Mario // 26-06-2023
            //SqlConnection con;
            //BaseContextoDatos obj = new BaseContextoDatos();
            //con = obj.ConectarBd();
            //con.Open();
            //try
            //{
            //    string sql = "sp_grabarDetalleEliminacion";
            //    SqlCommand cmd = new SqlCommand(sql, con);
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    cmd.Parameters.Add("@IED_CODIGO", SqlDbType.Int).Value = ied.IED_CODIGO;
            //    cmd.Parameters.Add("@IED_TIPO", SqlDbType.NVarChar, 10).Value = ied.IED_TIPO;
            //    cmd.Parameters.Add("@IED_HORA", SqlDbType.NVarChar).Value = Convert.ToString(ied.IED_HORA);
            //    cmd.Parameters.Add("@IED_CLASE", SqlDbType.NVarChar, 250).Value = ied.IED_CLASE;
            //    cmd.Parameters.Add("@IED_CANTIDAD", SqlDbType.NVarChar, 10).Value = ied.IED_CANTIDAD;
            //    cmd.Parameters.Add("@IE_CODIGO", SqlDbType.Int).Value = ied.IE_CODIGO;
            //    cmd.Parameters.Add("@ID_USUARIO", SqlDbType.Int).Value = ied.ID_USUARIO;
            //    cmd.Parameters.Add("@MODO", SqlDbType.NVarChar, 10).Value = MODO;
            //    cmd.Parameters.Add("@IED_DETALLE", SqlDbType.NVarChar, 10).Value = ied.IED_DETALLE;
            //    //cmd.Parameters.Add("@IED_FECHA", SqlDbType.NVarChar).Value = ied.IED_FECHA;
            //    cmd.ExecuteNonQuery();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //    throw new Exception("Error", ex);
            //}
            //finally
            //{
            //    con.Close();
            //}

        }
        public DataTable cargaGrid(string IED_TIPO, Int32 IE_CODIGO)
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
            Sqlcmd = new SqlCommand("select * from HC_INGESTA_ELIMINACION_DETALLE  WHERE IED_TIPO = '" + IED_TIPO + "' and IE_CODIGO=" + IE_CODIGO + " order by IED_FECHA", Sqlcon);
            //Sqlcmd = new SqlCommand("SELECT * FROM HC_INGESTA_ELIMINACION_DETALLE WHERE IED_TIPO = + '" + IED_TIPO + "' + AND IE_CODIGO = " + IE_CODIGO + " AND IED_HORA BETWEEN CONVERT(datetime, CONCAT("+Fecha+ ", ' ', '19:01:00'), 120) AND CONVERT(datetime, CONCAT(" + fecha1 + ", ' ', '07:01:00'), 120) ORDER BY IED_HORA;", Sqlcon);
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
        public DataTable cargarNoche(string IED_TIPO, Int32 IE_CODIGO, String Fecha)
        {

            Fecha = Fecha.Substring(0,10) +' '+"19:01:00";
            DateTime fecha1;
            string fecha2;
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
            
            fecha1 = Convert.ToDateTime(Fecha).AddDays(1);

            fecha2 = Convert.ToString(fecha1).Substring(0, 10) + ' ' + "07:01:00";


                        

            //Sqlcmd = new SqlCommand("select * from HC_INGESTA_ELIMINACION_DETALLE  WHERE IED_TIPO = '" + IED_TIPO + "' and IE_CODIGO=" + IE_CODIGO + " order by IED_HORA", Sqlcon);
            Sqlcmd = new SqlCommand("SELECT * FROM HC_INGESTA_ELIMINACION_DETALLE WHERE IED_TIPO = '" + IED_TIPO + "' AND IE_CODIGO = " + IE_CODIGO + " AND IED_HORA BETWEEN '"+Fecha+"' AND '"+fecha2+"' ORDER BY IED_HORA;", Sqlcon);
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
        public DataTable UltimoRegistro()
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
            Sqlcmd = new SqlCommand("select top 1 IE_CODIGO from HC_INGESTA_ELIMINACION order by IE_CODIGO desc", Sqlcon);
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
        public bool eliminarIngElm(Int32 IED_CODIGO)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            try
            {
                command = new SqlCommand("DELETE FROM HC_INGESTA_ELIMINACION_DETALLE WHERE IED_CODIGO = " + IED_CODIGO, connection);
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                connection.Close();
                return false;
            }
        }
        public Int32 SumaTotales(Int32 IE_CODIGO, string par)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            BaseContextoDatos obj = new BaseContextoDatos();
            Int32 CME = 0;
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                command.Connection = conexion;
                command.CommandText = "select ISNULL(SUM(cast(IED_CANTIDAD as int)),0) from His3000..HC_INGESTA_ELIMINACION_DETALLE where IE_CODIGO = " + IE_CODIGO + " and IED_TIPO like '%" + par + "%'";
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())//Obtenemos los datos de la columna y asignamos a los campos de la Cache de Usuario
                    {
                        CME = reader.GetInt32(0);
                    }
                    conexion.Close();
                    return CME;
                }
                else
                {
                    conexion.Close();
                    return CME;
                }
            }
            catch (Exception ex)
            {
                conexion.Close();
                return CME;
                Console.WriteLine(ex.Message); ;
            }

        }
        public Int32 ExistenciaParaSignosVitales(Int64 ATE_CODIGO, DateTime IE_FECHA)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            BaseContextoDatos obj = new BaseContextoDatos();
            Int32 CME = 0;
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                command.Connection = conexion;
                command.CommandText = "select IE_CODIGO from His3000..HC_INGESTA_ELIMINACION where ATE_CODIGO = " + ATE_CODIGO + "and YEAR(IE_FECHA) = " + IE_FECHA.Year + " and MONTH(IE_FECHA) = " + IE_FECHA.Month + " and DAY(IE_FECHA) =" + IE_FECHA.Day;
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())//Obtenemos los datos de la columna y asignamos a los campos de la Cache de Usuario
                    {
                        CME = reader.GetInt32(0);
                    }
                    conexion.Close();
                    return CME;
                }
                else
                {
                    conexion.Close();
                    return CME;
                }
            }
            catch (Exception ex)
            {
                conexion.Close();
                return CME;
                Console.WriteLine(ex.Message);
            }

        }
        public Int32 sumaIngesta(Int32 IE_CODIGO, string par)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Int32 ncomidas = 0;
            Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            try
            {
                Sqlcmd = new SqlCommand("select IED_TIPO from His3000..HC_INGESTA_ELIMINACION_DETALLE where IE_CODIGO = " + IE_CODIGO + " and IED_TIPO like '%" + par + "%' group by IED_TIPO", Sqlcon);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts);
                Sqlcon.Close();
                ncomidas = Dts.Rows.Count;
            }
            catch (Exception ex)
            {
                Sqlcon.Close();
                return ncomidas;
                Console.WriteLine(ex.Message);
            }
            return ncomidas;
        }
        public Int32 sumaOrinaDeposiciones(Int32 IE_CODIGO, string par)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Int32 ncomidas = 0;
            Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            try
            {
                Sqlcmd = new SqlCommand("select IED_TIPO from His3000..HC_INGESTA_ELIMINACION_DETALLE where IE_CODIGO = " + IE_CODIGO + " and IED_TIPO like '%" + par + "%'", Sqlcon);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts);
                Sqlcon.Close();
                ncomidas = Dts.Rows.Count;
            }
            catch (Exception ex)
            {
                Sqlcon.Close();
                return ncomidas;
                Console.WriteLine(ex.Message);
            }
            return ncomidas;
        }
        public HC_INGESTA_ELIMINACION_DETALLE recuperaDetalleUsuario(Int32 IED_CODIGO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_INGESTA_ELIMINACION_DETALLE x = (from i in db.HC_INGESTA_ELIMINACION_DETALLE
                                                    where i.IED_CODIGO == IED_CODIGO
                                                    select i).FirstOrDefault();
                return x;
            }
        }
        public Int32 idUsuario(Int32 IE_CODIGO, string par)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            BaseContextoDatos obj = new BaseContextoDatos();
            Int32 ID_USUARIO = 0;
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                command.Connection = conexion;
                command.CommandText = "select top 1 ISNULL(ID_USUARIO,0) from His3000..HC_INGESTA_ELIMINACION_DETALLE where IE_CODIGO = " + IE_CODIGO + " and IED_TIPO like '%" + par + "%' order by IED_CODIGO";
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())//Obtenemos los datos de la columna y asignamos a los campos de la Cache de Usuario
                    {
                        ID_USUARIO = reader.GetInt32(0);
                    }
                    conexion.Close();
                    return ID_USUARIO;
                }
                else
                {
                    conexion.Close();
                    return ID_USUARIO;
                }
            }
            catch (Exception ex)
            {
                conexion.Close();
                return ID_USUARIO;
                Console.WriteLine(ex.Message); ;
            }

        }
        public List<HC_INGESTA_ELIMINACION> recuperaIngesta(Int64 ATE_CODIGO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HC_INGESTA_ELIMINACION> x = (from i in db.HC_INGESTA_ELIMINACION
                                                  where i.ATE_CODIGO == ATE_CODIGO
                                                  select i).ToList();
                return x;
            }
        }
        public DataTable ingestaXfecha(Int64 ate_codigo, DateTime fecha)
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
            Sqlcmd = new SqlCommand("select IE_CODIGO from His3000..HC_INGESTA_ELIMINACION where ATE_CODIGO = " + ate_codigo + " and \n" +
                " YEAR(IE_FECHA) = " + fecha.Year + " and MONTH(IE_FECHA) = " + fecha.Month + " and DAY (IE_FECHA) = " + fecha.Day, Sqlcon);
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
        public DSAyudaIngElm cargaKardexCompuesto(Int64 ATE_CODIGO)
        {
            DSAyudaIngElm ds = new DSAyudaIngElm();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var cv = (from c in db.MEDICAMENTO_COMPUESTO_ENCABEZADO
                          where c.ate_codigo == ATE_CODIGO && c.estado == true
                          select c).ToList();
                foreach (var item in cv)
                {
                    var dt = (from d in db.MEDICAMENTO_COMPUESTO_DETALLE
                              where d.id_med_compuesto == item.id_med_compuesto
                              select d).ToList();
                    LISTADO_COMPUESTOS ls = (from cm in db.LISTADO_COMPUESTOS
                                             where cm.id_compuesto == item.id_lista_compuestos + 1
                                             select cm).FirstOrDefault();
                    object[] cab = new object[]
                    {
                        item.id_med_compuesto,
                        ls.detalle,
                        item.fecha_registro
                    };
                    ds.Cabecera.Rows.Add(cab);
                    foreach (var det in dt)
                    {
                        object[] dtl = new object[]
                        {
                            det.cue_detalle,
                            det.id_med_compuesto
                        };
                        ds.Detalle.Rows.Add(dtl);
                    }
                }
            }
            return ds;
        }
        public string compuestoKardex(Int64 ID)
        {
            string compuesto = "";
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var x = (from dc in db.MEDICAMENTO_COMPUESTO_DETALLE
                         where dc.id_med_compuesto == ID
                         select dc).ToList();
                foreach (var item in x)
                {
                    compuesto = compuesto + " mas " + item.cue_detalle;
                }
            }
            return compuesto;
        }
        public List<ABREVIACIONES> listadoAbrevioaciones()
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return db.ABREVIACIONES.ToList();
            }
        }
        public bool desacticaKardex(Int64 ID)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    MEDICAMENTO_COMPUESTO_ENCABEZADO m = (from kd in db.MEDICAMENTO_COMPUESTO_ENCABEZADO
                                                          select kd).FirstOrDefault();
                    m.estado = false;
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
        public List<TIPO_DRENAJE> cargaDrenaje()
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from td in db.TIPO_DRENAJE select td).OrderBy(x => x.TDR_DESCRIPCION).ToList();
            }
        }
    }
}
