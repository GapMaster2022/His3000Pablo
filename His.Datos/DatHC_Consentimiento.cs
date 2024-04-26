using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace His.Datos
{
    public class DatHC_Consentimiento
    {
        public HC_EXONERACION_RETIRO CargarDatosH2(Int64 ate_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from exr in db.HC_EXONERACION_RETIRO
                        where exr.ATE_CODIGO == ate_codigo
                        select exr).FirstOrDefault();
            }
        }
        public void GuardarConsentimientoH2(Int64 ate_codigo, string pdtestigo, string pdparentesco, string pdtelefono, string pdcedula, string abtestigo, string abparentesco, string abtelefono,
        string abcedula, string ahmedico, string ahtelefono, string ahcedula, string ahtTestigo, string ahtParentesco, string ahtTelefono, string ahtCedula, string memedido, string metelefono,
        string mecedula, string metTestigo, string metParentesco, string metTelefono, string metCedula, string odmedico, string odtelefono, string odcedula, string odtTestigo, string odtParentesco,
        string odtTelefono, string odtCedula, string anmedico, string antelefono, string ancedula, string antTestigo, string antParentesco, string antTelefono, string antCedula, string parentesco,
        string representante, string identificacion, string telefomo, string Organos, string Receptor)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_GuardarForm024H2", connection);
            if (pdtelefono == null)
            {
                pdtelefono = "0999999999";
            }
            else
            {
                if (pdtelefono.Length > 10)
                {
                    pdtelefono = "0999999999";
                }
            }
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ATE_CODIGO", ate_codigo);
            command.Parameters.AddWithValue("@ER_PDTESTIGO", pdtestigo);
            command.Parameters.AddWithValue("@ER_PDPARENTESCO", pdparentesco);
            command.Parameters.AddWithValue("@ER_PDTELEFONO", pdtelefono);
            command.Parameters.AddWithValue("@ER_PDCEDULA", pdcedula);
            if (abtelefono == null)
            {
                abtelefono = "0999999999";
            }
            else
            {
                if (abtelefono.Length > 10)
                {
                    abtelefono = "0999999999";
                }
            }
            command.Parameters.AddWithValue("@ER_ABTESTIGO", abtestigo);
            command.Parameters.AddWithValue("@ER_ABPARENTESCO", abparentesco);
            command.Parameters.AddWithValue("@ER_ABTELEFONO", abtelefono);
            command.Parameters.AddWithValue("@ER_ABCEDULA", abcedula);

            if (ahtTelefono == null)
            {
                ahtTelefono = "0999999999";
            }
            else
            {
                if (ahtTelefono.Length > 10)
                {
                    ahtTelefono = "0999999999";
                }
            }
            if (ahtelefono == null)
                ahtelefono = "0";
            if (ahcedula == null)
                ahcedula = "0";
            command.Parameters.AddWithValue("@ER_AHMEDICO", ahmedico);
            command.Parameters.AddWithValue("@ER_AHTELEFONO", ahtelefono);
            command.Parameters.AddWithValue("@ER_AHCEDULA", ahcedula);
            command.Parameters.AddWithValue("@ER_AHTTESTIGO", ahtTestigo);
            command.Parameters.AddWithValue("@ER_AHTPARENTESCO", ahtParentesco);
            command.Parameters.AddWithValue("@ER_AHTTELEFONO", ahtTelefono);
            command.Parameters.AddWithValue("@ER_AHTCEDULA", ahtCedula);

            if (metTelefono == null)
            {
                metTelefono = "0999999999";
            }
            else
            {
                if (metTelefono.Length > 10)
                {
                    metTelefono = "0999999999";
                }
            }
            if (metelefono == null)
                metelefono = "0";
            if (mecedula == null)
                mecedula = "0";
            command.Parameters.AddWithValue("@ER_MEMEDICO", memedido);
            command.Parameters.AddWithValue("@ER_METELEFONO", metelefono);
            command.Parameters.AddWithValue("@ER_MECEDULA", mecedula);
            command.Parameters.AddWithValue("@ER_METTESTIGO", metTestigo);
            command.Parameters.AddWithValue("@ER_METPARENTESCO", metParentesco);
            command.Parameters.AddWithValue("@ER_METTELEFONO", metTelefono);
            command.Parameters.AddWithValue("@ER_METCEDULA", metCedula);

            if (odtTelefono == null)
            {
                odtTelefono = "0999999999";
            }
            else
            {
                if (odtTelefono.Length > 10)
                {
                    odtTelefono = "0999999999";
                }
            }
            if (odtelefono == null)
                odtelefono = "0";
            if (odcedula == null)
                odcedula = "0";
            command.Parameters.AddWithValue("@ER_ODMEDICO", odmedico);
            command.Parameters.AddWithValue("@ER_ODTELEFONO", odtelefono);
            command.Parameters.AddWithValue("@ER_ODCEDULA", odcedula);
            command.Parameters.AddWithValue("@ER_ODTTESTIGO", odtTestigo);
            command.Parameters.AddWithValue("@ER_ODTPARENTESCO", odtParentesco);
            command.Parameters.AddWithValue("@ER_ODTTELEFONO", odtTelefono);
            command.Parameters.AddWithValue("@ER_ODTCEDULA", odtCedula);
            command.Parameters.AddWithValue("@ER_ORGANOS_DONADOS", Organos);
            command.Parameters.AddWithValue("@ER_NOMBRE_RECEPTOR", Receptor);

            if (antTelefono == null)
            {
                antTelefono = "0999999999";
            }
            else
            {
                if (antTelefono.Length > 10)
                {
                    antTelefono = "0999999999";
                }
            }
            //if (antelefono == null)
            //    antelefono = "0";
            //if (ancedula == null)
            //    ancedula = "0"
            command.Parameters.AddWithValue("@ER_ANMEDICO", anmedico);
            command.Parameters.AddWithValue("@ER_ANTELEFONO", antelefono);
            command.Parameters.AddWithValue("@ER_ANCEDULA", ancedula);
            command.Parameters.AddWithValue("@ER_ANTTESTIGO", antTestigo);
            command.Parameters.AddWithValue("@ER_ANTPARENTESCO", antParentesco);
            command.Parameters.AddWithValue("@ER_ANTTELEFONO", antTelefono);
            command.Parameters.AddWithValue("@ER_ANTCEDULA", antCedula);

            command.Parameters.AddWithValue("@ER_REPRESENTANTE", representante);
            command.Parameters.AddWithValue("@ER_PARENTESCO", parentesco);
            command.Parameters.AddWithValue("@ER_IDENTIFICACION", identificacion);
            command.Parameters.AddWithValue("@ER_TELEFONO", telefomo);

            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();

        }
        public bool guardaExoneracion(HC_EXONERACION_RETIRO exRet)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    db.Crear("HC_EXONERACION_RETIRO", exRet);
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
        public bool guardaConsentimiento(HC_CONSENTIMIENTO_INFORMADO conInf)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    db.Crear("HC_CONSENTIMIENTO_INFORMADO", conInf);
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
        public void GuardarConsentimiento(Int64 ate_codigo, string servicio, string sala, string proposito1,
            string resultado1, string procedimiento, string riesgo1, string proposito2, string resultado2,
            string quirurgico, string riesgo2, string proposito3, string resultado3, string anestesia,
            string riesgo3, DateTime fecha, DateTime hora, string tratante, string tespecialidad, string ttelefono,
            string tcodigo, string cirujano, string cespecialidad, string ctelefono, string ccodigo,
            string anestesista, string aespecialidad, string atelefono, string acodigo, string representante,
            string parentesco, string identificacion, string telefono)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_GuardarForm024", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@servicio", servicio);
            command.Parameters.AddWithValue("@sala", sala);
            command.Parameters.AddWithValue("@proposito1", proposito1);
            command.Parameters.AddWithValue("@resultado1", resultado1);
            command.Parameters.AddWithValue("@procedimiento", procedimiento);
            command.Parameters.AddWithValue("@riesgo1", riesgo1);
            command.Parameters.AddWithValue("@proposito2", proposito2);
            command.Parameters.AddWithValue("@resultado2", resultado2);
            command.Parameters.AddWithValue("@quirurgico", quirurgico);
            command.Parameters.AddWithValue("@riesgo2", riesgo2);
            command.Parameters.AddWithValue("@proposito3", proposito3);
            command.Parameters.AddWithValue("@resultado3", resultado3);
            command.Parameters.AddWithValue("@anestesia", anestesia);
            command.Parameters.AddWithValue("@riesgo3", riesgo3);
            command.Parameters.AddWithValue("@fecha", fecha);
            command.Parameters.AddWithValue("@hora", hora);

            command.Parameters.AddWithValue("@tratante", tratante);
            command.Parameters.AddWithValue("@tespecialidad", tespecialidad);
            command.Parameters.AddWithValue("@ttelefono", ttelefono);
            command.Parameters.AddWithValue("@tcodigo", tcodigo);
            command.Parameters.AddWithValue("@cirujano", cirujano);
            command.Parameters.AddWithValue("@cespecialidad", cespecialidad);
            if (ctelefono == null)
            {
                ctelefono = "0999999999";
            }
            else
            {
                if (ctelefono.Length > 10)
                {
                    ctelefono = "0999999999";
                }
            }
            command.Parameters.AddWithValue("@ctelefono", ctelefono);
            command.Parameters.AddWithValue("@ccodigo", ccodigo);
            command.Parameters.AddWithValue("@anestesista", anestesia);
            command.Parameters.AddWithValue("@aespecialidad", aespecialidad);
            if (atelefono == null)
            {
                atelefono = "0999999999";
            }
            else
            {
                if (atelefono.Length > 10)
                {
                    atelefono = "0999999999";
                }
            }
            command.Parameters.AddWithValue("@atelefono", atelefono);
            command.Parameters.AddWithValue("@acodigo", acodigo);

            command.Parameters.AddWithValue("@representante", representante);
            command.Parameters.AddWithValue("@parentesco", parentesco);
            command.Parameters.AddWithValue("@identificacion", identificacion);
            command.Parameters.AddWithValue("@telefono", telefono);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();

        }

        public DataTable CargarDatos(Int64 ate_codigo, Int64 CON_CODIGO)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            DataTable Tabla = new DataTable();

            command = new SqlCommand("Select * From HC_CONSENTIMIENTO_INFORMADO where ATE_CODIGO = @ate_codigo and CON_CODIGO = " + CON_CODIGO, connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            SqlDataReader reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            if (Tabla.Rows.Count > 0)
            {
                return Tabla;
            }
            else
            {
                return Tabla = null;
            }
        }
        public bool actualizarExoneracion(Int64 ATE_CODIGO, HC_EXONERACION_RETIRO ret)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    HC_EXONERACION_RETIRO exRet = (from exr in db.HC_EXONERACION_RETIRO
                                                   where exr.ATE_CODIGO == ATE_CODIGO
                                                   select exr).FirstOrDefault();
                    exRet.ER_PDTESTIGO = ret.ER_PDTESTIGO;
                    exRet.ER_PDPARENTESCO = ret.ER_PDPARENTESCO;
                    exRet.ER_PDTELEFONO = ret.ER_PDTELEFONO;
                    exRet.ER_PDCEDULA = ret.ER_PDCEDULA;
                    exRet.ER_ABTESTIGO = ret.ER_ABTESTIGO;
                    exRet.ER_ABPARENTESCO = ret.ER_ABPARENTESCO;
                    exRet.ER_ABTELEFONO = ret.ER_ABTELEFONO;
                    exRet.ER_ABCEDULA = ret.ER_ABCEDULA;
                    exRet.ER_AHMEDICO = ret.ER_AHMEDICO;
                    exRet.ER_AHTELEFONO = ret.ER_AHTELEFONO;
                    exRet.ER_AHCEDULA = ret.ER_AHCEDULA;
                    exRet.ER_AHTTESTIGO = ret.ER_AHTTESTIGO;
                    exRet.ER_AHTPARENTESCO = ret.ER_AHTPARENTESCO;
                    exRet.ER_AHTTELEFONO = ret.ER_AHTTELEFONO;
                    exRet.ER_AHTCEDULA = ret.ER_AHTCEDULA;
                    exRet.ER_MEMEDICO = ret.ER_MEMEDICO;
                    exRet.ER_METELEFONO = ret.ER_METELEFONO;
                    exRet.ER_MECEDULA = ret.ER_MECEDULA;
                    exRet.ER_METTESTIGO = ret.ER_METTESTIGO;
                    exRet.ER_METPARENTESCO = ret.ER_METPARENTESCO;
                    exRet.ER_METTELEFONO = ret.ER_METTELEFONO;
                    exRet.ER_METCEDULA = ret.ER_METCEDULA;
                    exRet.ER_ODMEDICO = ret.ER_ODMEDICO;
                    exRet.ER_ODTELEFONO = ret.ER_ODTELEFONO;
                    exRet.ER_ODCEDULA = ret.ER_ODCEDULA;
                    exRet.ER_ODTTESTIGO = ret.ER_ODTTESTIGO;
                    exRet.ER_ODTPARENTESCO = ret.ER_ODTPARENTESCO;
                    exRet.ER_ODTTELEFONO = ret.ER_ODTTELEFONO;
                    exRet.ER_ODTCEDULA = ret.ER_ODTCEDULA;
                    exRet.ER_ANMEDICO = ret.ER_ANMEDICO;
                    exRet.ER_ANTELEFONO = ret.ER_ANTELEFONO;
                    exRet.ER_ANCEDULA = ret.ER_ANCEDULA;
                    exRet.ER_ANTTESTIGO = ret.ER_ANTTESTIGO;
                    exRet.ER_ANTPARENTESCO = ret.ER_ANTPARENTESCO;
                    exRet.ER_ANTTELEFONO = ret.ER_ANTTELEFONO;
                    exRet.ER_ANTCEDULA = ret.ER_ANTCEDULA;
                    exRet.ER_REPRESENTANTE = ret.ER_REPRESENTANTE;
                    exRet.ER_PARENTESCO = ret.ER_PARENTESCO;
                    exRet.ER_IDENTIFICACION = ret.ER_IDENTIFICACION;
                    exRet.ER_TELEFONO = ret.ER_TELEFONO;
                    exRet.ER_ORGANOS_DONADOS = ret.ER_ORGANOS_DONADOS;
                    exRet.ER_NOMBRE_RECEPTOR = ret.ER_NOMBRE_RECEPTOR;
                    exRet.ER_ESTA1 = ret.ER_ESTA1;
                    exRet.ER_ESTA2 = ret.ER_ESTA2;
                    exRet.ER_ESTA3 = ret.ER_ESTA3;
                    exRet.ER_ESTA4 = ret.ER_ESTA4;
                    exRet.ER_ESTA5 = ret.ER_ESTA5;
                    exRet.ER_ESTA6 = ret.ER_ESTA6;
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
        public bool actualizarConsentimiento(Int64 ATE_CODIGO, HC_CONSENTIMIENTO_INFORMADO conInf)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction trans = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    HC_CONSENTIMIENTO_INFORMADO cInf = (from c in db.HC_CONSENTIMIENTO_INFORMADO
                                                        where c.ATE_CODIGO == ATE_CODIGO
                                                        select c).FirstOrDefault();
                    cInf.CON_SERVICIO = conInf.CON_SERVICIO;
                    cInf.CON_SALA = conInf.CON_SALA;
                    cInf.CON_PROPOSITO1 = conInf.CON_PROPOSITO1;
                    cInf.CON_RESULTADO1 = conInf.CON_RESULTADO1;
                    cInf.CON_PROCEDIMIENTO = conInf.CON_PROCEDIMIENTO;
                    cInf.CON_RIESGO1 = conInf.CON_RIESGO1;
                    cInf.CON_PROPOSITO2 = conInf.CON_PROPOSITO2;
                    cInf.CON_RESULTADO2 = conInf.CON_RESULTADO2;
                    cInf.CON_QUIRURGICO = conInf.CON_QUIRURGICO;
                    cInf.CON_RIESGO2 = conInf.CON_RIESGO2;
                    cInf.CON_PROPOSITO3 = conInf.CON_PROPOSITO3;
                    cInf.CON_RESULTADO3 = conInf.CON_RESULTADO3;
                    cInf.CON_ANESTESIA = conInf.CON_ANESTESIA;
                    cInf.CON_RIESGO3 = conInf.CON_RIESGO3;
                    cInf.CON_FECHA = conInf.CON_FECHA;
                    cInf.CON_HORA = conInf.CON_HORA;
                    cInf.CON_TRATANTE = conInf.CON_TRATANTE;
                    cInf.CON_TESPECIALIDAD = conInf.CON_TESPECIALIDAD;
                    cInf.CON_TTELEFONO = conInf.CON_TTELEFONO;
                    cInf.CON_TCODIGO = conInf.CON_TCODIGO;
                    cInf.CON_CIRUJANO = conInf.CON_CIRUJANO;
                    cInf.CON_CESPECIALIDAD = conInf.CON_CESPECIALIDAD;
                    cInf.CON_CTELEFONO = conInf.CON_CTELEFONO;
                    cInf.CON_CCODIGO = conInf.CON_CCODIGO;
                    cInf.CON_ANESTESISTA = conInf.CON_ANESTESISTA;
                    cInf.CON_AESPECIALIDAD = conInf.CON_AESPECIALIDAD;
                    cInf.CON_ATELEFONO = conInf.CON_ATELEFONO;
                    cInf.CON_ACODIGO = conInf.CON_ACODIGO;
                    cInf.CON_REPRESENTANTE = conInf.CON_REPRESENTANTE;
                    cInf.CON_PARENTESCO = conInf.CON_PARENTESCO;
                    cInf.CON_IDENTIFICACION = conInf.CON_IDENTIFICACION;
                    cInf.CON_TELEFONO = conInf.CON_TELEFONO;
                    db.SaveChanges();
                    trans.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    trans.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }
        }
        public DataTable getConsentimiento(Int64 ate_codigo)
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
            Sqlcmd = new SqlCommand("select CON_CODIGO,a.ATE_CODIGO, '[FECHA: '+CONVERT(varchar,CON_FECHA,23)+'] - [HORA: '+CONVERT(varchar,CON_HORA,108)+'] - [ATENCION: '+a.ATE_NUMERO_ATENCION+']' " +
                "AS 'DATOS'  from HC_CONSENTIMIENTO_INFORMADO c inner join ATENCIONES a on c.ATE_CODIGO = a.ATE_CODIGO where a.ATE_CODIGO = " + ate_codigo + " order by CON_CODIGO desc ", Sqlcon);
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
        public Int32 ultimoRegistro()
        {
            Int32 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HC_CONSENTIMIENTO_INFORMADO> sv = contexto.HC_CONSENTIMIENTO_INFORMADO.ToList();
                if (sv.Count > 0)
                    maxim = contexto.HC_CONSENTIMIENTO_INFORMADO.Max(emp => emp.CON_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }
        }
    }
}
