using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using System.Data.SqlClient;
using System.Data;

namespace His.Datos
{
    public class DatEvolucionDetalle
    {
        public void crearEvolucionDetalle(HC_EVOLUCION_DETALLE evDetalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_EVOLUCION_DETALLE(evDetalle);
                contexto.SaveChanges();
            }
            //SqlConnection con = null;
            //SqlCommand cmd = null;
            //BaseContextoDatos obj = new BaseContextoDatos();
            //bool ok = false;
            //try
            //{
            //    con = obj.ConectarBd();
            //    cmd = new SqlCommand("sp_GuardaFechasEvolucion", con);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@nuevaEvolucion", evDetalle.EVD_CODIGO);
            //    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            //    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
            //    con.Open();
            //    int oks = cmd.ExecuteNonQuery();

            //    if (oks > 0)
            //        ok = true;
            //}
            //catch (Exception ex)
            //{
            //    ok = false;
            //    throw ex;
            //}
            //finally
            //{
            //    con.Close();
            //}
        }

        public List<HC_EVOLUCION_DETALLE> listaNotasEvolucion(Int64 codEvolucion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.HC_EVOLUCION_DETALLE.Include("HC_PRESCRIPCIONES").OrderByDescending(x => x.EVD_FECHA).Where(n => n.HC_EVOLUCION.EVO_CODIGO == codEvolucion).ToList();
            }
        }


        //public HC_EVOLUCION_DETALLE ultimaNotaEvolucion(int codEvolucion)
        public EVOLUCIONDETALLE ultimaNotaEvolucion(Int64 codEvolucion)
        {
            //using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            //{
            //    return (from n in contexto.HC_EVOLUCION_DETALLE
            //            join e in contexto.HC_EVOLUCION on n.HC_EVOLUCION.EVO_CODIGO equals e.EVO_CODIGO
            //            where n.HC_EVOLUCION.EVO_CODIGO == codEvolucion
            //            orderby n.EVD_FECHA descending
            //            select n).FirstOrDefault();
            //}
            EVOLUCIONDETALLE objDetalle = new EVOLUCIONDETALLE();
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            BaseContextoDatos obj = new BaseContextoDatos();            
            try
            {
                con = obj.ConectarBd();
                cmd = new SqlCommand("sp_ultimaNotaEvolucion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codEvolucion", codEvolucion);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objDetalle.evd_codigo = Convert.ToInt64(dr["EVD_CODIGO"].ToString());
                    objDetalle.evo_codigo = Convert.ToInt16(dr["EVO_CODIGO"].ToString());
                    objDetalle.id_usuario = Convert.ToInt16(dr["ID_USUARIO"].ToString());
                    objDetalle.nom_usuario = dr["NOM_USUARIO"].ToString();
                    objDetalle.evd_fecha = Convert.ToDateTime(dr["EVD_FECHA"].ToString());
                    objDetalle.evd_descripcion = dr["EVD_DESCRIPCION"].ToString();
                    objDetalle.fechInicio = Convert.ToDateTime(dr["FECHA_INICIO"].ToString());
                    objDetalle.fechaFin = Convert.ToDateTime(dr["FECHA_FIN"].ToString());
                    objDetalle.med_codigo = (long)Convert.ToUInt64(dr["MED_TRATANTE"].ToString());
                }
            }
            catch(Exception ex)
            {
                objDetalle = null;
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return objDetalle;

        }

        public int ultimoCodigo()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.HC_EVOLUCION_DETALLE
                             select d.EVD_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }

        

        public DataTable FechasResponsabilidad(Int64 evoCodigo, Int64 medCodigo)
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

            Sqlcmd = new SqlCommand("sp_Fechas_Medico_Evolucion", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@CODIGO"].Value = (evoCodigo);
            
            Sqlcmd.Parameters.Add("@MEDICO", SqlDbType.Int);
            Sqlcmd.Parameters["@MEDICO"].Value = (medCodigo);
            
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
        public DataTable verificaEvolucionEnfermeria(Int64 ATE_CODIGO)
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

            Sqlcmd = new SqlCommand("sp_VerificaEvolucionEnfermeria", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = (ATE_CODIGO);
            
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

        public List<HC_EVOLUCION_DETALLE> RecuperoTodasEvolucionDetalle(Int64 evoCodigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from ed in contexto.HC_EVOLUCION_DETALLE
                        where ed.HC_EVOLUCION.EVO_CODIGO == evoCodigo
                        select ed).ToList();
            }
        }
        public DataTable RecuperaEvdCodigo(Int64 EVO_CODIGO)
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

            Sqlcmd = new SqlCommand("sp_RecuperaEvdCodigo", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@EVO_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@EVO_CODIGO"].Value = (EVO_CODIGO);

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

        public DataTable RecuperaPrescripciones(Int64 EVD_CODIGO)
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

            Sqlcmd = new SqlCommand("sp_RecuperaPrescripciones", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@EVD_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@EVD_CODIGO"].Value = (EVD_CODIGO);

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

        public int GuardaEvolucionEnfermeria(int ate_codigo, int pac_codigo, int id_usuario, string nom_usuario)
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

            Sqlcmd = new SqlCommand("sp_GrabaEvolucionEnfermeria", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ate_codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@ate_codigo"].Value = (ate_codigo);

            Sqlcmd.Parameters.Add("@pac_codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@pac_codigo"].Value = (pac_codigo);

            Sqlcmd.Parameters.Add("@id_usuario", SqlDbType.Int);
            Sqlcmd.Parameters["@id_usuario"].Value = (id_usuario);

            Sqlcmd.Parameters.Add("@nom_usuario", SqlDbType.VarChar);
            Sqlcmd.Parameters["@nom_usuario"].Value = (nom_usuario);

            Sqlcmd.Parameters.Add("@evo_fecha", SqlDbType.DateTime);
            Sqlcmd.Parameters["@evo_fecha"].Value = (DateTime.Now);

            Sqlcmd.Parameters.Add("@evo_estado", SqlDbType.Int);
            Sqlcmd.Parameters["@evo_estado"].Value = (1);

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
                Console.WriteLine(ex.Message+ " sp_GrabaEvolucionEnfermeria");
                return 0;
            }
                        
            return 1;
        }
        public int GuardaEvolucionEnfermeriaDetalle(int EVO_CODIGO, int ID_USUARIO, string NOM_USUARIO, string EVD_DESCRIPCION)
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

            Sqlcmd = new SqlCommand("sp_GrabaEvolucionEnfermeriaDetalle", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@EVO_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@EVO_CODIGO"].Value = (EVO_CODIGO);

            Sqlcmd.Parameters.Add("@ID_USUARIO", SqlDbType.Int);
            Sqlcmd.Parameters["@ID_USUARIO"].Value = (ID_USUARIO);

            Sqlcmd.Parameters.Add("@NOM_USUARIO", SqlDbType.VarChar);
            Sqlcmd.Parameters["@NOM_USUARIO"].Value = (NOM_USUARIO);

            Sqlcmd.Parameters.Add("@EVD_FECHA", SqlDbType.VarChar);
            Sqlcmd.Parameters["@EVD_FECHA"].Value = (DateTime.Now);

            Sqlcmd.Parameters.Add("@EVD_DESCRIPCION", SqlDbType.Text);
            Sqlcmd.Parameters["@EVD_DESCRIPCION"].Value = (EVD_DESCRIPCION);

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
                Console.WriteLine(ex.Message+ " sp_GrabaEvolucionEnfermeriaDetalle");
                return 0;
            }

            return 1;
        }

        public int GuardaEvolucionEnfermeriaPrescripciones(int EVD_CODIGO, string PRES_FARMACOTERAPIA_INDICACIONES, string PRES_FARMACOS_INSUMOS, Boolean PRES_ESTADO, string PRES_FECHA_ADMINISTRACION)
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

            Sqlcmd = new SqlCommand("sp_GrabaEvolucionEnfermeriaPrescripciones", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@EVD_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@EVD_CODIGO"].Value = (EVD_CODIGO);

            Sqlcmd.Parameters.Add("@PRES_FARMACOTERAPIA_INDICACIONES", SqlDbType.Text);
            Sqlcmd.Parameters["@PRES_FARMACOTERAPIA_INDICACIONES"].Value = (PRES_FARMACOTERAPIA_INDICACIONES);

            Sqlcmd.Parameters.Add("@PRES_FARMACOS_INSUMOS", SqlDbType.Text);
            Sqlcmd.Parameters["@PRES_FARMACOS_INSUMOS"].Value = (PRES_FARMACOS_INSUMOS);

            Sqlcmd.Parameters.Add("@PRES_FECHA", SqlDbType.DateTime);
            Sqlcmd.Parameters["@PRES_FECHA"].Value = (DateTime.Now);

            Sqlcmd.Parameters.Add("@PRES_ESTADO", SqlDbType.Bit);
            Sqlcmd.Parameters["@PRES_ESTADO"].Value = (PRES_ESTADO);

            Sqlcmd.Parameters.Add("@PRES_FECHA_ADMINISTRACION", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PRES_FECHA_ADMINISTRACION"].Value = (PRES_FECHA_ADMINISTRACION);

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
                Console.WriteLine(ex.Message + " sp_GrabaEvolucionEnfermeriaPrescripciones");
                return 0;
            }

            return 1;
        }

        public void editarNotaEvolucionMedica(string notaModificada, DateTime fechaInicio, DateTime fechaFin, int evd_codigo, string docs, string medicoTratante)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_EVOLUCION_DETALLE notaOriginal = (from e in contexto.HC_EVOLUCION_DETALLE
                                                     where e.EVD_CODIGO == evd_codigo
                                                     select e).FirstOrDefault();
                notaOriginal.EVD_DESCRIPCION = notaModificada;
                notaOriginal.NOM_USUARIO = docs;
                notaOriginal.FECHA_INICIO = fechaInicio;
                notaOriginal.FECHA_FIN = fechaFin;
                notaOriginal.MED_TRATANTE = Convert.ToInt32(medicoTratante);

                contexto.SaveChanges();

            }

            //SqlConnection con = null;
            //SqlCommand cmd = null;
            //BaseContextoDatos obj = new BaseContextoDatos();
            //bool ok = false;
            //try
            //{
            //    con = obj.ConectarBd();
            //    cmd = new SqlCommand("sp_GuardaFechasEvolucionMedica", con);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@nuevaEvolucion", evd_codigo);
            //    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            //    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
            //    cmd.Parameters.AddWithValue("@evdescripcion", notaModificada);
            //    cmd.Parameters.AddWithValue("@docs", docs);
            //    con.Open();
            //    int oks = cmd.ExecuteNonQuery();

            //    if (oks > 0)
            //        ok = true;
            //}
            //catch (Exception ex)
            //{
            //    ok = false;
            //    throw ex;
            //}
            //finally
            //{
            //    cmd.Parameters.Clear();
            //    con.Close();
            //}
        }
        public void editarNotaEvolucion(HC_EVOLUCION_DETALLE notaModificada, DateTime fechaInicio, DateTime fechaFin)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_EVOLUCION_DETALLE notaOriginal = (from e in contexto.HC_EVOLUCION_DETALLE
                                                     where e.EVD_CODIGO == notaModificada.EVD_CODIGO
                                                     select e).FirstOrDefault();
                notaOriginal.EVD_DESCRIPCION = notaModificada.EVD_DESCRIPCION;
                contexto.SaveChanges();
            }

            SqlConnection con = null;
            SqlCommand cmd = null;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool ok = false;
            try
            {
                con = obj.ConectarBd();
                cmd = new SqlCommand("sp_GuardaFechasEvolucion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nuevaEvolucion", notaModificada.EVD_CODIGO);
                cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                con.Open();
                int oks = cmd.ExecuteNonQuery();

                if (oks > 0)
                    ok = true;
            }
            catch (Exception ex)
            {
                ok = false;
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void EliminarEvolucion(string observacion, int id_usuario, Int64 ate_codigo, int evo_codigo, Int64 evd_codigo)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool ok = false;
            try
            {
                con = obj.ConectarBd();
                cmd = new SqlCommand("sp_EvolucionEliminar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@observacion", observacion);
                cmd.Parameters.AddWithValue("@id_usuario", id_usuario);
                cmd.Parameters.AddWithValue("@ate_codigo", ate_codigo);
                cmd.Parameters.AddWithValue("@evo_codigo", evo_codigo);
                cmd.Parameters.AddWithValue("@evd_codigo", evd_codigo);
                cmd.CommandTimeout = 180;
                con.Open();
                int oks = cmd.ExecuteNonQuery();

                if (oks > 0)
                    ok = true;
            }
            catch (Exception ex)
            {
                ok = false;
                throw ex;
            }
            finally
            {
                cmd.Parameters.Clear();
                con.Close();
            }
        }
        public List<HC_EVOLUCION_DETALLE> RecuperaEvolucion(int evo_codigo)
        {
            try
            {
                using(var datos = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from d in datos.HC_EVOLUCION_DETALLE
                            where d.HC_EVOLUCION.EVO_CODIGO == evo_codigo
                            select d).OrderByDescending(x => x.EVD_CODIGO).ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public HC_EVOLUCION_DETALLE RecuperaEvolucionDetalle(int evo_codigo)
        {
            try
            {
                HC_EVOLUCION_DETALLE obj = new HC_EVOLUCION_DETALLE();
                using(var datos = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    obj = (from d in datos.HC_EVOLUCION_DETALLE
                            where d.EVD_CODIGO == evo_codigo
                            select d).FirstOrDefault();
                }
                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<HC_PRESCRIPCIONES> RecuperaEvoPrescripciones(int evd_codigo)
        {
            try
            {
                using(var datos = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from d in datos.HC_PRESCRIPCIONES
                            where d.HC_EVOLUCION_DETALLE.EVD_CODIGO == evd_codigo
                            select d).ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public USUARIOS RecuperarUsuario(int id_usuario)
        {
            try
            {
                USUARIOS obj = new USUARIOS();
                using(var datos = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    obj = (from d in datos.USUARIOS
                           where d.ID_USUARIO == id_usuario
                           select d).FirstOrDefault();
                }
                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable FechaYHora(int evd_codigo)
        {
            DataTable Tabla = new DataTable();
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            try
            {
                con = obj.ConectarBd();
                cmd = new SqlCommand("sp_FechaYHoraEvolucion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@evd_codigo", evd_codigo);
                cmd.CommandTimeout = 180;
                con.Open();
                reader = cmd.ExecuteReader();
                Tabla.Load(reader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Parameters.Clear();
                con.Close();
            }
            return Tabla;
        }
    }
}
