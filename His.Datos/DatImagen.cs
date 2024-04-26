using Core.Datos;
using His.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace His.Datos
{
    public class DatImagen
    {

        public void deleteAgendamiento(int idAgendamiento, string xMotivo)
        {
            string cadena_sql = "UPDATE [dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS] SET [estado] = 2 " +
                ",[observacion] = concat((select[observacion] from[dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS] where id =" + idAgendamiento + " ),' [Anulado el " + xMotivo + "]') WHERE id = " + idAgendamiento;
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


        public void saveAgendamiento(int id_agendamiento, string[] p, List<PedidoImagen_estudios> e, DataTable dt = null)
        {
            string cadena_sql = "";
            if (id_agendamiento == 0)
            {
                string idnvo = nuevoID();
                if (p[3] == "0" && p[4] == "0")
                {
                    cadena_sql = "delete from HC_IMAGENOLOGIA_AGENDAMIENTOS where ATE_CODIGO=" + p[1];

                }

                cadena_sql += "DECLARE @idnvo int;\n" +
                "SELECT @idnvo = IsNull((select max(id) + 1 from[HC_IMAGENOLOGIA_AGENDAMIENTOS]), 1) ; \n" +
                "SET IDENTITY_INSERT [dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS] ON; \n" +
                "INSERT INTO [dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS] (id, [fecha_solicitud] ,[ATE_CODIGO] ,[fecha_agendamiento] ,[med_tecnologo] ,[med_radiologo] ,[observacion],[ID_USUARIO]) \n" +
                    "VALUES( @idnvo ,'" +
                    p[0].ToString() + "', '" +
                    p[1].ToString() + "', '" +
                    p[2].ToString() + "', " +
                    p[3].ToString() + ", " +
                    p[4].ToString() + ", '" +
                    p[5].ToString() + "', " + p[6].ToString() + "); \n" +
                    "SET IDENTITY_INSERT [dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS] OFF; \n";

                List<PedidoImagen_estudios> est = e;
                foreach (var estudio in est)
                {
                    cadena_sql += " INSERT INTO [dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS] " +
                        "([id_imagenologia_agendamientos] ,[CUE_CODIGO], ID_USUARIO, DVD, CD) VALUES (@idnvo, " +
                               estudio.PRO_CODIGO + ", " + Sesion.codUsuario.ToString() + " , 0 , 0);\n";
                }

            }
            else
            {
                cadena_sql = "delete from dbo.[HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS] WHERE  [id_imagenologia_agendamientos]=" + id_agendamiento + "; \n"
                   + "UPDATE[dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS] "
                + "SET  [fecha_solicitud] = '" + p[0].ToString()
                    + "', " + " [ATE_CODIGO] = '" + p[1].ToString()
                    + "', " + "[fecha_agendamiento] = '" + p[2].ToString()
                    + "', " + "[med_tecnologo] = " + p[3].ToString()
                    + ", " + "[med_radiologo] = " + p[4].ToString()
                    + ", " + "[observacion] = '" + p[5].ToString() + "' WHERE id = " + id_agendamiento + "; \n";

                List<PedidoImagen_estudios> est = e;
                foreach (var estudio in est)
                {
                    string xIngreso = " INSERT INTO [dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS] " +
                                        "([id_imagenologia_agendamientos] ,[CUE_CODIGO], ID_USUARIO, DVD, CD)" +
                        " VALUES (" + id_agendamiento + ", " +
                               estudio.PRO_CODIGO + ", " + Sesion.codUsuario.ToString() + " , 0 , 0);\n";
                    foreach (DataRow row in dt.Rows)
                    {
                        if (estudio.PRO_CODIGO == Convert.ToInt32(row["CUE_CODIGO"].ToString()))
                        {
                            int XDVD = 0;
                            if (Convert.ToBoolean(row["DVD"].ToString()))
                                XDVD = 1;
                            int XCD = 0;
                            if (Convert.ToBoolean(row["CD"].ToString()))
                                XCD = 1;



                            xIngreso = " INSERT INTO [dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS] " +
                                        "([id_imagenologia_agendamientos] ,[CUE_CODIGO]" +
                                        "   ,[30X40]" +
                                        "   ,[8x10]" +
                                        "   ,[14x14]" +
                                        "   ,[14x17]" +
                                        "   ,[18x24]" +
                                        "   ,[ODONT]" +
                                        "   ,[DANADAS]" +
                                        "   ,[ENVIADAS]" +
                                        "   ,[FECHA_REALIZADO]" +
                                        "   ,[MEDIO_CONTRASTE]" +
                                        "   ,[DVD]" +
                                        "   ,[CD]" +
                                                    "   ,[ID_USUARIO])" +
                                    " VALUES (" + id_agendamiento + ", " +
                                           estudio.PRO_CODIGO + ", " +
                                          row["30X40"].ToString() + ", " +
                                          row["8x10"].ToString() + ", " +
                                          row["14x14"].ToString() + ", " +
                                          row["14x17"].ToString() + ", " +
                                          row["18x24"].ToString() + ", " +
                                          row["ODONT"].ToString() + ", " +
                                          row["DANADAS"].ToString() + ", " +
                                          row["ENVIADAS"].ToString() + ", '" +
                                          row["FECHA_REALIZADO"].ToString() + "', " +
                                          row["MEDIO_CONTRASTE"].ToString() + ", " +
                                          XDVD + ", " +
                                          XCD + ", " +
                                          row["ID_USUARIO"].ToString() +
                                           "); ";
                        }
                    }

                    cadena_sql += xIngreso + "\n";
                }




            }

            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);



            try
            {
                //SqlDataReader dr = Sqlcmd.ExecuteReader();
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void saveInformeAgendamiento(string[] p, List<PedidoImagen_diagnostico> x, Int64 id, int conMed)
        {
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            string cadena_sql = "";
            SqlCommand Sqlcmd = null;
            //if (id != 0)
            //{
            //    cadena_sql = "delete from[HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME] WHERE id=" + id + "\n";
            //    cadena_sql += "delete from [HC_IMAGENOLOGIA_AGENDAMIENTOS_DIAGNOSTICOS] WHERE id=" + id;
            //    Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);
            //    try
            //    {
            //        Sqlcmd.ExecuteNonQuery();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //    cadena_sql = "";
            //}

            cadena_sql = "DECLARE @idnvo int; \n" +
                "SELECT @idnvo = IsNull((select max(id) + 1 from[HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME]), 1) ; \n" +
                "                SET IDENTITY_INSERT[dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME] ON; \n" +
                "INSERT INTO[dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME] ([id], id_IMAGENOLOGIA_AGENDAMIENTO ,[cod_med_solicitante],[prioridad],[fecha_informe],[fecha_entrega],[informe],[DB_V],[LF_V],[PA_V],[DB_EG],[LF_EG],[PA_EG],[DB_P],[LF_P],[PA_P], \n" +
                "    [PLACENTA_F],[PLACENTA_M],[PLACENTA_P],[MASCULINO],[FEMENINO],[MULTIPLE],[GRADO_MADUREZ],[ANTEVERSION],[RETROVERSION],[DIU],[FIBROMA],[MIOMA],[AUSENTE],[HIDROSALPIX],[QUISTE],[VACIA],[OCUPADA],[SACO_DOUGLAS],[recomendaciones],[PLACAS_ENVIADAS],[30X40],[8X10],[14X14],[14X17],[18X24],[ODONT],[DANADAS],[MEDIO_CONTRASTE],[conclusiones])  \n" +
                "VALUES(@idnvo , " +
                p[0].ToString() + ", " +
                p[1].ToString() + ", " +
                p[2].ToString() + ", '" +
                p[3].ToString() + "', '" +
                p[4].ToString() + "', '" +
                p[5].ToString() + "', '" +
                p[6].ToString() + "', '" +
                p[7].ToString() + "', '" +
                p[8].ToString() + "', '" +
                p[9].ToString() + "', '" +
                p[10].ToString() + "', '" +
                p[11].ToString() + "', '" +
                p[12].ToString() + "', '" +
                p[13].ToString() + "', '" +
                p[14].ToString() + "', " +
                p[15].ToString() + ", " +
                p[16].ToString() + ", " +
                p[17].ToString() + ", " +
                p[18].ToString() + ", " +
                p[19].ToString() + ", " +
                p[20].ToString() + ", '" +
                p[21].ToString() + "', " +
                p[22].ToString() + ", " +
                p[23].ToString() + ", " +
                p[24].ToString() + ", " +
                p[25].ToString() + ", " +
                p[26].ToString() + ", " +
                p[27].ToString() + ", " +
                p[28].ToString() + ", " +
                p[29].ToString() + ", " +
                p[30].ToString() + ", " +
                p[31].ToString() + ", '" +
                p[32].ToString() + "', '" +
                p[33].ToString() + "', " +
                p[34].ToString() + ", " +
                p[35].ToString() + ", " +
                p[36].ToString() + ", " +
                p[37].ToString() + ", " +
                p[38].ToString() + ", " +
                p[39].ToString() + ", " +
                p[40].ToString() + ", " +
                p[41].ToString() + ", " +
                p[42].ToString() + ", '" +
                p[43].ToString() + " '" + "); \n" +
                "SET IDENTITY_INSERT [dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME] OFF; \n" +
                "UPDATE [dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS] SET [estado] = 1 WHERE id=" + p[0].ToString() + "\n";

            List<PedidoImagen_diagnostico> diag = x;
            foreach (var diagnostico in diag)
            {
                cadena_sql += "INSERT INTO [dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS_DIAGNOSTICOS] ([id_imagenologia_agendamiento_informe] ,[CIE_CODIGO] ) VALUES ( @idnvo"
                           + ", '" + diagnostico.CIE_CODIGO + "')\n";
            }

            cadena_sql += "UPDATE HC_IMAGENOLOGIA_AGENDAMIENTOS SET med_radiologo = " + conMed + " WHERE id = " + id;

            Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void guardarCambios(string[] p, List<PedidoImagen_diagnostico> x, Int64 id)
        {
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            string cadena_sql = "";
            SqlCommand Sqlcmd = null;
            cadena_sql = "delete from[HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME] WHERE id_IMAGENOLOGIA_AGENDAMIENTO=" + id + "\n DECLARE @idnvo int; \n" +
                "SELECT @idnvo = IsNull((select max(id) + 1 from[HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME]), 1) ; \n" +
                "                SET IDENTITY_INSERT[dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME] ON; \n" +
                "INSERT INTO[dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME] ([id], id_IMAGENOLOGIA_AGENDAMIENTO , "
                 + "[cod_med_solicitante],[prioridad],[fecha_informe],[fecha_entrega],[informe],[DB_V],[LF_V], " +
                 "[PA_V],[DB_EG],[LF_EG],[PA_EG],[DB_P],[LF_P],[PA_P], \n" +
                "    [PLACENTA_F],[PLACENTA_M],[PLACENTA_P],[MASCULINO],[FEMENINO]," +
                "[MULTIPLE],[GRADO_MADUREZ],[ANTEVERSION],[RETROVERSION],[DIU],[FIBROMA],[MIOMA],[AUSENTE],[HIDROSALPIX]," +
                "[QUISTE],[VACIA],[OCUPADA],[SACO_DOUGLAS],[recomendaciones],[PLACAS_ENVIADAS],[30X40],[8X10],[14X14],[14X17]," +
                "[18X24],[ODONT],[DANADAS],[MEDIO_CONTRASTE],[conclusiones])  \n" +
                "VALUES(@idnvo , " +
                p[0].ToString() + ", " +
                p[1].ToString() + ", " +
                p[2].ToString() + ", '" +
                p[3].ToString() + "', '" +
                p[4].ToString() + "', '" +
                p[5].ToString() + "', '" +
                p[6].ToString() + "', '" +
                p[7].ToString() + "', '" +
                p[8].ToString() + "', '" +
                p[9].ToString() + "', '" +
                p[10].ToString() + "', '" +
                p[11].ToString() + "', '" +
                p[12].ToString() + "', '" +
                p[13].ToString() + "', '" +
                p[14].ToString() + "', " +
                p[15].ToString() + ", " +
                p[16].ToString() + ", " +
                p[17].ToString() + ", " +
                p[18].ToString() + ", " +
                p[19].ToString() + ", " +
                p[20].ToString() + ", '" +
                p[21].ToString() + "', " +
                p[22].ToString() + ", " +
                p[23].ToString() + ", " +
                p[24].ToString() + ", " +
                p[25].ToString() + ", " +
                p[26].ToString() + ", " +
                p[27].ToString() + ", " +
                p[28].ToString() + ", " +
                p[29].ToString() + ", " +
                p[30].ToString() + ", " +
                p[31].ToString() + ", '" +
                p[32].ToString() + "', '" +
                p[33].ToString() + "', " +
                p[34].ToString() + ", " +
                p[35].ToString() + ", " +
                p[36].ToString() + ", " +
                p[37].ToString() + ", " +
                p[38].ToString() + ", " +
                p[39].ToString() + ", " +
                p[40].ToString() + ", " +
                p[41].ToString() + ", " +
                p[42].ToString() + ", '" +
                p[43].ToString() + " '" + "); \n" +
                "SET IDENTITY_INSERT [dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME] OFF; \n" +
                "UPDATE [dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS] SET [estado] = 1 WHERE id=" + p[0].ToString() + "\n";

            List<PedidoImagen_diagnostico> diag = x;
            cadena_sql += "delete from  [dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS_DIAGNOSTICOS] where id_imagenologia_agendamiento_informe in (SELECT dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.id FROM dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME INNER JOIN dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_DIAGNOSTICOS ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.id = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_DIAGNOSTICOS.id_imagenologia_agendamiento_informe WHERE dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.id_IMAGENOLOGIA_AGENDAMIENTO = " + id + ")\n";
            foreach (var diagnostico in diag)
            {
                cadena_sql += "INSERT INTO [dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS_DIAGNOSTICOS] ([id_imagenologia_agendamiento_informe] ,[CIE_CODIGO] ) VALUES ("
                           + "@idnvo, '" + diagnostico.CIE_CODIGO + "')\n";
            }

            Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getForm012Estudios(Int64 x)
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
            Sqlcmd = new SqlCommand("SELECT        dbo.PRODUCTO.PRO_DESCRIPCION\n" +
            "FROM            dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME INNER JOIN dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS ON  dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.id_IMAGENOLOGIA_AGENDAMIENTO = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.id_imagenologia_agendamientos INNER JOIN\n" +
            "                 dbo.CUENTAS_PACIENTES ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.CUE_CODIGO = dbo.CUENTAS_PACIENTES.CUE_CODIGO INNER JOIN dbo.PRODUCTO ON dbo.CUENTAS_PACIENTES.PRO_CODIGO = dbo.PRODUCTO.PRO_CODIGO\n" +
            "WHERE        dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.id_IMAGENOLOGIA_AGENDAMIENTO = " + x + "  ", Sqlcon);
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

        public DataTable getForm012Dx(Int64 x)
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
            Sqlcmd = new SqlCommand("SELECT        dbo.CIE10.CIE_DESCRIPCION, dbo.CIE10.CIE_CODIGO\n" +
"FROM            dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME INNER JOIN\n" +
 "                        dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_DIAGNOSTICOS INNER JOIN\n" +
  "                       dbo.CIE10 ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_DIAGNOSTICOS.CIE_CODIGO = dbo.CIE10.CIE_CODIGO ON\n" +
   "                      dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.id = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_DIAGNOSTICOS.id_imagenologia_agendamiento_informe\n" +
"WHERE dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.id_IMAGENOLOGIA_AGENDAMIENTO = " + x + "  ", Sqlcon);
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

        public DataTable getForm012(Int64 x)
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
            Sqlcmd = new SqlCommand("SELECT top 1 (SELECT TOP (1) EMP_NOMBRE FROM dbo.EMPRESA) AS CLINICA, \n" +
        "ISNULL(dbo.PACIENTES_DATOS_ADICIONALES.COD_PARROQUIA, ' ') as PARROQUIA, ISNULL(dbo.PACIENTES_DATOS_ADICIONALES.COD_CANTON, ' ') AS CANTON, ISNULL(dbo.PACIENTES_DATOS_ADICIONALES.COD_PROVINCIA, ' ') AS PROVINCIA, dbo.PACIENTES.PAC_HISTORIA_CLINICA, dbo.PACIENTES.PAC_APELLIDO_PATERNO, dbo.PACIENTES.PAC_APELLIDO_MATERNO, dbo.PACIENTES.PAC_NOMBRE1, dbo.PACIENTES.PAC_NOMBRE2,\n" +
        "dbo.PACIENTES.PAC_IDENTIFICACION, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.fecha_informe, dbo.TIPO_INGRESO.TIP_DESCRIPCION, dbo.HABITACIONES.hab_Numero, concat(dbo.MEDICOS.MED_NOMBRE1, ' ', dbo.MEDICOS.MED_APELLIDO_PATERNO) as Medico_solicitante, IIF(prioridad = '0', 'X', ' ') AS P_Control, IIF(prioridad = '1', 'X', ' ') AS P_Normal, IIF(prioridad = '2', 'X', ' ') AS P_Urgente\n" +
        ", dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.fecha_entrega, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.informe, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.DB_V, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.LF_V, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.PA_V, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.DB_EG, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.LF_EG, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.PA_EG,\n" +
        "dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.DB_P, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.LF_P, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.PA_P, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.PLACENTA_F, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.PLACENTA_M, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.PLACENTA_P, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.MASCULINO, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.FEMENINO, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.MULTIPLE,\n" +
        "dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.GRADO_MADUREZ, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.ANTEVERSION, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.RETROVERSION, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.DIU, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.FIBROMA, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.MIOMA, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.AUSENTE, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.HIDROSALPIX, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.QUISTE,\n" +
        "dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.VACIA, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.OCUPADA, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.SACO_DOUGLAS, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.recomendaciones, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.PLACAS_ENVIADAS, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.[30X40], dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.[8X10], dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.[14X14], dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.[14X17],\n" +
        "dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.[18X24], dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.ODONT, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.DANADAS, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.MEDIO_CONTRASTE, concat(TRIM(MEDICOS_2.MED_APELLIDO_PATERNO), ' ',TRIM(MEDICOS_2.MED_APELLIDO_MATERNO), ' ', TRIM(MEDICOS_2.MED_NOMBRE1), ' ', TRIM(MEDICOS_2.MED_NOMBRE2), '  CI:', SUBSTRING(MEDICOS_2.MED_RUC, 1,10)) as Tecnologo, concat(TRIM(MEDICOS_1.MED_APELLIDO_PATERNO), ' ', TRIM(MEDICOS_1.MED_APELLIDO_MATERNO), ' ',TRIM(MEDICOS_1.MED_NOMBRE1), ' ', TRIM(MEDICOS_1.MED_NOMBRE2), ' ', '  CI:', SUBSTRING(MEDICOS_1.MED_RUC, 1,10)) AS Radiologo, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.[conclusiones]\n" +
        "FROM dbo.PACIENTES_DATOS_ADICIONALES INNER JOIN dbo.PACIENTES INNER JOIN dbo.ATENCIONES ON dbo.PACIENTES.PAC_CODIGO = dbo.ATENCIONES.PAC_CODIGO ON dbo.PACIENTES_DATOS_ADICIONALES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO INNER JOIN dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS INNER JOIN\n" +
        " dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.id_imagenologia_agendamientos = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.id INNER JOIN dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.id = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.id_IMAGENOLOGIA_AGENDAMIENTO ON\n" +
        " dbo.ATENCIONES.ATE_CODIGO = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.ATE_CODIGO INNER JOIN dbo.TIPO_INGRESO ON dbo.ATENCIONES.TIP_CODIGO = dbo.TIPO_INGRESO.TIP_CODIGO INNER JOIN dbo.HABITACIONES ON dbo.ATENCIONES.HAB_CODIGO = dbo.HABITACIONES.hab_Codigo INNER JOIN\n" +
        " dbo.MEDICOS ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.cod_med_solicitante = dbo.MEDICOS.MED_CODIGO INNER JOIN dbo.MEDICOS AS MEDICOS_2 ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.med_tecnologo = MEDICOS_2.MED_CODIGO INNER JOIN dbo.MEDICOS AS MEDICOS_1 ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.med_radiologo = MEDICOS_1.MED_CODIGO\n" +
        " WHERE  dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.id =  " + x + "  ", Sqlcon);
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
        public DataTable getAteImagen(int x)
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
            Sqlcmd = new SqlCommand(
                "select xprod.* from ( " +
                    "select xa.codigo as codigo, xa.Producto AS Producto ,xa.PED_CODIGO, xa.PED_FECHA, xa.PRO_CODIGO  from " +
                    "(SELECT  CUENTAS_PACIENTES.PRO_CODIGO,  dbo.CUENTAS_PACIENTES.CUE_CODIGO as codigo, dbo.PRODUCTO.PRO_DESCRIPCION AS Producto ,PEDIDOS.PED_CODIGO, PEDIDOS.PED_FECHA , dbo.ATENCIONES.ATE_CODIGO  FROM  dbo.ATENCIONES INNER JOIN dbo.CUENTAS_PACIENTES ON dbo.ATENCIONES.ATE_CODIGO = dbo.CUENTAS_PACIENTES.ATE_CODIGO INNER JOIN\n" +
                    "dbo.PRODUCTO INNER JOIN Sic3000.dbo.ProductoSubdivision ON dbo.PRODUCTO.PRO_CODSUB = Sic3000.dbo.ProductoSubdivision.codsub AND dbo.PRODUCTO.PRO_CODDIV = Sic3000.dbo.ProductoSubdivision.coddiv INNER JOIN\n" +
                    "dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = dbo.RUBROS.RUB_CODIGO INNER JOIN dbo.PEDIDOS_AREAS ON dbo.RUBROS.PED_CODIGO = dbo.PEDIDOS_AREAS.DIV_CODIGO ON dbo.CUENTAS_PACIENTES.PRO_CODIGO = dbo.PRODUCTO.PRO_CODIGO\n" +
                    " INNER JOIN  dbo.PEDIDOS ON dbo.CUENTAS_PACIENTES.Codigo_Pedido = dbo.PEDIDOS.PED_CODIGO  WHERE  dbo.RUBROS.RUB_ASOCIADO = 6 AND dbo.ATENCIONES.ATE_CODIGO = " + x + ") xa \n" +
                    "left  join \n" +
                    "( SELECT dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.CUE_CODIGO, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.ATE_CODIGO \n" +
                    "FROM dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS INNER JOIN dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.id = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.id_imagenologia_agendamientos \n" +
                    "WHERE  dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.estado <> N'2' and dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.ATE_CODIGO = " + x + " ) xb \n" +
                    " on xa.codigo = xb.CUE_CODIGO and xa.ATE_CODIGO = xb.ATE_CODIGO where xb.CUE_CODIGO is null and xb.ATE_CODIGO is null \n" +
                " ) xprod left join \n" +
                "    (select pd.Ped_Codigo, PRO_CODIGO from PEDIDO_DEVOLUCION pd inner join PEDIDO_DEVOLUCION_DETALLE pdd on pd.DevCodigo = pdd.DevCodigo) xdev \n" +
                "    on xprod.PRO_CODIGO = xdev.PRO_CODIGO and xprod.PED_CODIGO = xdev.Ped_Codigo \n" +
                "where xdev.PRO_CODIGO is null"
                    , Sqlcon);
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
        public DataTable getAteImagenCount(int x)
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
            Sqlcmd = new SqlCommand("SELECT  count(dbo.CUENTAS_PACIENTES.CUE_CODIGO) as IItems FROM  dbo.ATENCIONES INNER JOIN dbo.CUENTAS_PACIENTES ON dbo.ATENCIONES.ATE_CODIGO = dbo.CUENTAS_PACIENTES.ATE_CODIGO INNER JOIN\n" +
                    "dbo.PRODUCTO INNER JOIN Sic3000.dbo.ProductoSubdivision ON dbo.PRODUCTO.PRO_CODSUB = Sic3000.dbo.ProductoSubdivision.codsub AND dbo.PRODUCTO.PRO_CODDIV = Sic3000.dbo.ProductoSubdivision.coddiv INNER JOIN\n" +
                    "dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = dbo.RUBROS.RUB_CODIGO INNER JOIN dbo.PEDIDOS_AREAS ON dbo.RUBROS.PED_CODIGO = dbo.PEDIDOS_AREAS.DIV_CODIGO ON dbo.CUENTAS_PACIENTES.PRO_CODIGO = dbo.PRODUCTO.PRO_CODIGO\n" +
                    "WHERE  dbo.RUBROS.RUB_ASOCIADO = 6 AND dbo.ATENCIONES.ATE_CODIGO = " + x + "  ", Sqlcon);
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
        public DataTable getAgendamientos(string fechas, int x = 0)
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

            if (x == 0)
            {
                Sqlcmd = new SqlCommand("SELECT ia.id, H.hab_Numero HABITACION, IA.fecha_agendamiento 'FECHA AGENDAMIENTO', IA.fecha_solicitud 'FECHA SOLICITUD', \n" +
                                      "A.ATE_CODIGO ATENCION, \n" +
                                      "CONCAT(P.PAC_APELLIDO_PATERNO, ' ', P.PAC_APELLIDO_MATERNO, ' ', P.PAC_NOMBRE1, ' ', P.PAC_NOMBRE2) AS PACIENTE, \n" +
                                      "P.PAC_HISTORIA_CLINICA HISTORIAS, \n" +
                                      "(SELECT CONCAT(M.MED_APELLIDO_PATERNO, ' ', M.MED_APELLIDO_MATERNO, ' ', M.MED_NOMBRE1, ' ', M.MED_NOMBRE2) \n" +
                                      "FROM MEDICOS M WHERE MED_CODIGO = IA.med_radiologo) RADIOLOGO, \n" +
                                      "(SELECT CONCAT(M.MED_APELLIDO_PATERNO, ' ', M.MED_APELLIDO_MATERNO, ' ', M.MED_NOMBRE1, ' ', M.MED_NOMBRE2) \n" +
                                      "FROM MEDICOS M WHERE MED_CODIGO = IA.med_tecnologo) TECNÓLOGO, C.CUE_DETALLE EXAMEN,\n" +
                                      "(SELECT CONCAT(U.NOMBRES, ' ', APELLIDOS) FROM USUARIOS U WHERE ID_USUARIO = IA.ID_USUARIO) AGENDO  \n" +
                                      "FROM PACIENTES P \n" +
                                      "INNER JOIN ATENCIONES A ON P.PAC_CODIGO = A.PAC_CODIGO \n" +
                                      "INNER JOIN HC_IMAGENOLOGIA_AGENDAMIENTOS IA  ON A.ATE_CODIGO = IA.ATE_CODIGO \n" +
                                      "INNER JOIN HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS  IAE ON IA.id = IAE.id_imagenologia_agendamientos \n" +
                                      "INNER JOIN CUENTAS_PACIENTES C ON IAE.CUE_CODIGO = C.CUE_CODIGO \n" +
                                      "INNER JOIN Sic3000..Producto Pro ON C.PRO_CODIGO = Pro.codpro \n" +
                                      "LEFT OUTER JOIN dbo.HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo \n" +
                                      "where IA.estado = 0 and  IA.fecha_agendamiento  between " + fechas + " \n " +
                                      " AND C.RUB_CODIGO IN (20, 21, 22,23, 29) order by IA.fecha_agendamiento asc", Sqlcon);

            }
            else if (x == 2)
            {
                Sqlcmd = new SqlCommand("SELECT IA.id AS id, P.PAC_HISTORIA_CLINICA as HC, \n" +
                                      "CONCAT(P.PAC_APELLIDO_PATERNO, ' ', P.PAC_APELLIDO_MATERNO, ' ', P.PAC_NOMBRE1, ' ', P.PAC_NOMBRE2) AS PACIENTE, \n" +
                                      "A.ATE_CODIGO, A.ATE_FECHA_INGRESO, H.hab_Numero as HAB, IA.fecha_agendamiento, Pro.despro,\n" +
                                      "IAE.id_imagenologia_agendamientos, IAE.CUE_CODIGO, IAE.FECHA_REALIZADO, IAE.[30X40], IAE.[8x10],\n" +
                                      "IAE.[14x14], IAE.[14x17], IAE.[18x24], IAE.ODONT, IAE.DANADAS, IAE.ENVIADAS, IAE.MEDIO_CONTRASTE,\n" +
                                      "IAE.CD, IAE.DVD \n" +
                                      "FROM PACIENTES P \n" +
                                      "INNER JOIN ATENCIONES A ON P.PAC_CODIGO = A.PAC_CODIGO \n" +
                                      "INNER JOIN HC_IMAGENOLOGIA_AGENDAMIENTOS IA  ON A.ATE_CODIGO = IA.ATE_CODIGO \n" +
                                      "INNER JOIN HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS  IAE ON IA.id = IAE.id_imagenologia_agendamientos \n" +
                                      "INNER JOIN CUENTAS_PACIENTES C ON IAE.CUE_CODIGO = C.CUE_CODIGO \n" +
                                      "INNER JOIN Sic3000..Producto Pro ON C.PRO_CODIGO = Pro.codpro \n" +
                                      "LEFT OUTER JOIN dbo.HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo \n" +
                                      "where IA.estado = 0 and IA.fecha_agendamiento  between " + fechas + " and  IAE.FECHA_REALIZADO >= A.ATE_FECHA_INGRESO \n " +
                                      " AND C.RUB_CODIGO IN (20, 21, 22,23, 29) order by IA.fecha_agendamiento asc", Sqlcon);
            }
            else  // si es 1
            {
                Sqlcmd = new SqlCommand("SELECT IA.id AS id, P.PAC_HISTORIA_CLINICA as HC, \n" +
                                      "CONCAT(P.PAC_APELLIDO_PATERNO, ' ', P.PAC_APELLIDO_MATERNO, ' ', P.PAC_NOMBRE1, ' ', P.PAC_NOMBRE2) AS PACIENTE, \n" +
                                      "A.ATE_CODIGO, A.ATE_FECHA_INGRESO, H.hab_Numero as HAB, IA.fecha_agendamiento, Pro.despro,\n" +
                                      "IAE.id_imagenologia_agendamientos, IAE.CUE_CODIGO, IAE.FECHA_REALIZADO, IAE.[30X40], IAE.[8x10],\n" +
                                      "IAE.[14x14], IAE.[14x17], IAE.[18x24], IAE.ODONT, IAE.DANADAS, IAE.ENVIADAS, IAE.MEDIO_CONTRASTE,\n" +
                                      "IAE.CD, IAE.DVD \n" +
                                      "FROM PACIENTES P \n" +
                                      "INNER JOIN ATENCIONES A ON P.PAC_CODIGO = A.PAC_CODIGO \n" +
                                      "INNER JOIN HC_IMAGENOLOGIA_AGENDAMIENTOS IA  ON A.ATE_CODIGO = IA.ATE_CODIGO \n" +
                                      "INNER JOIN HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS  IAE ON IA.id = IAE.id_imagenologia_agendamientos \n" +
                                      "INNER JOIN CUENTAS_PACIENTES C ON IAE.CUE_CODIGO = C.CUE_CODIGO \n" +
                                      "INNER JOIN Sic3000..Producto Pro ON C.PRO_CODIGO = Pro.codpro \n" +
                                      "LEFT OUTER JOIN dbo.HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo \n" +
                                      "where IA.estado = 0 and  IA.fecha_agendamiento  between " + fechas + " \n " +
                                      " AND C.RUB_CODIGO IN (20, 21, 22,23, 29) order by IA.fecha_agendamiento asc", Sqlcon);

                //Sqlcmd = new SqlCommand("SELECT dbo.PACIENTES.PAC_HISTORIA_CLINICA as HC, CONCAT( dbo.PACIENTES.PAC_APELLIDO_PATERNO,' ', dbo.PACIENTES.PAC_APELLIDO_MATERNO,' ', dbo.PACIENTES.PAC_NOMBRE1,' ', dbo.PACIENTES.PAC_NOMBRE2) AS PACIENTE, \n" +
                //    " dbo.ATENCIONES.ATE_CODIGO, dbo.ATENCIONES.ATE_FECHA_INGRESO,dbo.HABITACIONES.hab_Numero as HAB, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.fecha_agendamiento, dbo.PRODUCTO.PRO_DESCRIPCION, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.id_imagenologia_agendamientos, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.CUE_CODIGO, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.FECHA_REALIZADO, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.[30X40], dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.[8x10],\n" +
                //    " dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.[14x14], dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.[14x17], dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.[18x24], dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.ODONT, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.DANADAS, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.ENVIADAS,  dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.MEDIO_CONTRASTE, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.CD, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.DVD\n" +
                //    " FROM dbo.PACIENTES INNER JOIN dbo.ATENCIONES ON dbo.PACIENTES.PAC_CODIGO = dbo.ATENCIONES.PAC_CODIGO INNER JOIN	dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS INNER JOIN dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.id_imagenologia_agendamientos = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.id ON \n" +
                //    "dbo.ATENCIONES.ATE_CODIGO = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.ATE_CODIGO INNER JOIN	dbo.CUENTAS_PACIENTES ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.CUE_CODIGO = dbo.CUENTAS_PACIENTES.CUE_CODIGO INNER JOIN dbo.PRODUCTO ON dbo.CUENTAS_PACIENTES.PRO_CODIGO = dbo.PRODUCTO.PRO_CODIGO LEFT OUTER JOIN dbo.HABITACIONES ON dbo.ATENCIONES.HAB_CODIGO = dbo.HABITACIONES.hab_Codigo\n" +
                //    "where dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.estado = 0 and  dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.fecha_agendamiento  between " + fechas + " order by dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.fecha_agendamiento asc", Sqlcon);

            }


            Dts.Clear();
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
        public DataTable getAgendamientosInformes(string fechas)
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
            Sqlcmd = new SqlCommand("SELECT ia.id, H.hab_Numero HABITACION,IA.fecha_solicitud 'FECHA SOLICITUD',  IA.fecha_agendamiento 'FECHA AGENDAMIENTO',AI.fecha_informe as 'FECHA INFORME', \n" +
                                      "A.ATE_CODIGO ATENCION, \n" +
                                      "CONCAT(P.PAC_APELLIDO_PATERNO, ' ', P.PAC_APELLIDO_MATERNO, ' ', P.PAC_NOMBRE1, ' ', P.PAC_NOMBRE2) AS PACIENTE, \n" +
                                      "P.PAC_HISTORIA_CLINICA HISTORIAS, \n" +
                                      "(SELECT CONCAT(M.MED_APELLIDO_PATERNO, ' ', M.MED_APELLIDO_MATERNO, ' ', M.MED_NOMBRE1, ' ', M.MED_NOMBRE2) \n" +
                                      "FROM MEDICOS M WHERE MED_CODIGO = IA.med_radiologo) RADIOLOGO, \n" +
                                      "(SELECT CONCAT(M.MED_APELLIDO_PATERNO, ' ', M.MED_APELLIDO_MATERNO, ' ', M.MED_NOMBRE1, ' ', M.MED_NOMBRE2) \n" +
                                      "FROM MEDICOS M WHERE MED_CODIGO = IA.med_tecnologo) TECNÓLOGO, C.CUE_DETALLE EXAMEN,\n" +
                                      "(SELECT CONCAT(U.NOMBRES, ' ', APELLIDOS) FROM USUARIOS U WHERE ID_USUARIO = IA.ID_USUARIO) AGENDO, IAE.EmailEnvio 'E-mail'  \n" +
                                      "FROM PACIENTES P \n" +
                                      "INNER JOIN ATENCIONES A ON P.PAC_CODIGO = A.PAC_CODIGO \n" +
                                      "INNER JOIN HC_IMAGENOLOGIA_AGENDAMIENTOS IA  ON A.ATE_CODIGO = IA.ATE_CODIGO \n" +
                                      "INNER JOIN HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS  IAE ON IA.id = IAE.id_imagenologia_agendamientos \n" +
                                      "INNER JOIN dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME AI ON IA.id = AI.id_IMAGENOLOGIA_AGENDAMIENTO \n" +
                                      "INNER JOIN CUENTAS_PACIENTES C ON IAE.CUE_CODIGO = C.CUE_CODIGO \n" +
                                      "INNER JOIN Sic3000..Producto Pro ON C.PRO_CODIGO = Pro.codpro \n" +
                                      "LEFT OUTER JOIN dbo.HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo \n" +
                                      "where IA.estado = 1 and  IA.fecha_agendamiento  between " + fechas + " and  IAE.FECHA_REALIZADO >= A.ATE_FECHA_INGRESO \n " +
                                      "order by IA.fecha_agendamiento asc", Sqlcon);
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

        public DataTable RegistraCorreoEnvio(string email, Int64 id)
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
            Sqlcmd = new SqlCommand("UPDATE HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS SET EmailEnvio = '" + email + "' WHERE id_imagenologia_agendamientos= " + id, Sqlcon);
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



        public DataTable getAgendamientoEstudios(string id_agendamiento)
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
            Sqlcmd = new SqlCommand("SELECT dbo.CUENTAS_PACIENTES.CUE_CODIGO, dbo.PRODUCTO.PRO_DESCRIPCION FROM dbo.CUENTAS_PACIENTES INNER JOIN dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS ON dbo.CUENTAS_PACIENTES.CUE_CODIGO = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.CUE_CODIGO INNER JOIN dbo.PRODUCTO ON dbo.CUENTAS_PACIENTES.PRO_CODIGO = dbo.PRODUCTO.PRO_CODIGO  \n" +
                                    "WHERE PED_CODIGO=6 and dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.id_imagenologia_agendamientos =" + id_agendamiento + " ", Sqlcon);
            ///"WHERE dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.id_imagenologia_agendamientos =" + id_agendamiento + " AND dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.ID_USUARIO IS NOT NULL", Sqlcon);
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
        public HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME ValidarEdicionHoras(int id)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME informes = new HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME();
                informes = (from i in db.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME
                            join a in db.HC_IMAGENOLOGIA_AGENDAMIENTOS on i.id_IMAGENOLOGIA_AGENDAMIENTO equals a.id
                            where a.id == id
                            select i).FirstOrDefault();
                return informes;
            }
        }
        public DataTable getAgendamiento(string id_agendamiento)
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
            //Sqlcmd = new SqlCommand("SELECT dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.*,  \n" +
            //"concat(dbo.PACIENTES.PAC_APELLIDO_PATERNO, ' ', dbo.PACIENTES.PAC_APELLIDO_MATERNO, ' ', dbo.PACIENTES.PAC_NOMBRE1, ' ', dbo.PACIENTES.PAC_NOMBRE2) as Paciente, \n" +
            //"dbo.ATENCIONES.MED_CODIGO, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.ID_USUARIO AS RAD_USUARIO  \n" +
            //"FROM dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS  \n" +
            //"INNER JOIN dbo.ATENCIONES ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO  \n" +
            //"INNER JOIN dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO  \n" +
            //"INNER JOIN dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.id = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.id_IMAGENOLOGIA_AGENDAMIENTO  \n" +
            //"INNER JOIN dbo.HABITACIONES ON dbo.ATENCIONES.HAB_CODIGO = dbo.HABITACIONES.hab_Codigo  \n" +
            //"INNER JOIN dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.id = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.id_imagenologia_agendamientos  \n" +
            //"LEFT OUTER JOIN dbo.MEDICOS ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.med_radiologo = dbo.MEDICOS.MED_CODIGO  \n" +
            //"LEFT OUTER JOIN dbo.MEDICOS AS MEDICOS_1 ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.med_tecnologo = MEDICOS_1.MED_CODIGO  \n" +
            //"where dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.id = " + id_agendamiento + " and  dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.estado = 1", Sqlcon);
            //"where [id]=" + id_agendamiento + "  AND dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.ID_USUARIO IS NOT NULL", Sqlcon);
            Sqlcmd = new SqlCommand("DECLARE @FECHA DATETIME\n" +
            "select @FECHA = a.ATE_FACTURA_FECHA from ATENCIONES a \n" +
            "inner join HC_IMAGENOLOGIA_AGENDAMIENTOS i on a.ATE_CODIGO = i.ATE_CODIGO \n" +
            "inner join HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME ii on i.id = ii.id \n" +
            "where i.id = " + id_agendamiento + " \n" +
            "IF(@FECHA IS NOT NULL) \n" +
            "BEGIN \n" +
            "SELECT dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.*,  \n" +
            "concat(dbo.PACIENTES.PAC_APELLIDO_PATERNO, ' ', dbo.PACIENTES.PAC_APELLIDO_MATERNO, ' ', dbo.PACIENTES.PAC_NOMBRE1, ' ', dbo.PACIENTES.PAC_NOMBRE2) as Paciente, \n" +
            "dbo.CUENTAS_PACIENTES.MED_CODIGO MED_CODIGO, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.ID_USUARIO AS RAD_USUARIO \n" +
            "FROM dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS \n" +
            "INNER JOIN dbo.ATENCIONES ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO \n" +
            "INNER JOIN dbo.CUENTAS_PACIENTES ON dbo.ATENCIONES.ATE_CODIGO=dbo.CUENTAS_PACIENTES.ATE_CODIGO \n" +
            "INNER JOIN dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO \n" +
            "LEFT OUTER JOIN dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.id = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.id_IMAGENOLOGIA_AGENDAMIENTO \n" +
            "INNER JOIN dbo.HABITACIONES ON dbo.ATENCIONES.HAB_CODIGO = dbo.HABITACIONES.hab_Codigo \n" +
            "LEFT OUTER JOIN dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.id = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.id_imagenologia_agendamientos \n" +
            "LEFT OUTER JOIN dbo.MEDICOS ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.med_radiologo = dbo.MEDICOS.MED_CODIGO \n" +
            "LEFT OUTER JOIN dbo.MEDICOS AS MEDICOS_1 ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.med_tecnologo = MEDICOS_1.MED_CODIGO \n" +
            "where dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.id = " + id_agendamiento + " AND DATEDIFF(DAY, @FECHA, getdate()) <= 2 AND dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.CUE_CODIGO=dbo.CUENTAS_PACIENTES.CUE_CODIGO \n" +
            "END \n" +
            "ELSE \n" +
            "BEGIN \n" +
            "SELECT dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.*,  \n" +
            "concat(dbo.PACIENTES.PAC_APELLIDO_PATERNO, ' ', dbo.PACIENTES.PAC_APELLIDO_MATERNO, ' ', dbo.PACIENTES.PAC_NOMBRE1, ' ', dbo.PACIENTES.PAC_NOMBRE2) as Paciente, \n" +
            "dbo.CUENTAS_PACIENTES.MED_CODIGO MED_CODIGO, dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.ID_USUARIO AS RAD_USUARIO \n" +
            "FROM dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS \n" +
            "INNER JOIN dbo.ATENCIONES ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO \n" +
            "INNER JOIN dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO \n" +
            "INNER JOIN dbo.CUENTAS_PACIENTES ON dbo.ATENCIONES.ATE_CODIGO=dbo.CUENTAS_PACIENTES.ATE_CODIGO \n" +
            "LEFT OUTER JOIN dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.id = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.id_IMAGENOLOGIA_AGENDAMIENTO \n" +
            "INNER JOIN dbo.HABITACIONES ON dbo.ATENCIONES.HAB_CODIGO = dbo.HABITACIONES.hab_Codigo \n" +
            "LEFT OUTER JOIN dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.id = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.id_imagenologia_agendamientos \n" +
            "LEFT OUTER JOIN dbo.MEDICOS ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.med_radiologo = dbo.MEDICOS.MED_CODIGO \n" +
            "LEFT OUTER JOIN dbo.MEDICOS AS MEDICOS_1 ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.med_tecnologo = MEDICOS_1.MED_CODIGO \n" +
            "where dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.id = " + id_agendamiento + " AND dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS.CUE_CODIGO=dbo.CUENTAS_PACIENTES.CUE_CODIGO END", Sqlcon);
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

        public DataTable CargarInforme(int id)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ImagenInformeRecover", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;

        }

        public DataTable CargarInformeDx(int id)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ImagenDx", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;

        }
        public DataTable getReporteRubros(int idImagenologia)
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
            Sqlcmd = new SqlCommand("select r.RUB_NOMBRE, sum(r.vez) as vez\n" +
                                    "from(\n" +
                                        "SELECT distinct dbo.RUBROS.RUB_NOMBRE, 1 as vez\n" +
                                        "FROM dbo.PRODUCTO INNER JOIN Sic3000.dbo.ProductoSubdivision ON dbo.PRODUCTO.PRO_CODSUB = Sic3000.dbo.ProductoSubdivision.codsub AND dbo.PRODUCTO.PRO_CODDIV = Sic3000.dbo.ProductoSubdivision.coddiv INNER JOIN\n" +
                                           "dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = dbo.RUBROS.RUB_CODIGO INNER JOIN dbo.HC_IMAGENOLOGIA_ESTUDIOS ON dbo.PRODUCTO.PRO_CODIGO = dbo.HC_IMAGENOLOGIA_ESTUDIOS.PRO_CODIGO   \n" +
                                         "WHERE (dbo.HC_IMAGENOLOGIA_ESTUDIOS.id_imagenologia = " + idImagenologia +
                                       ")\n union all\n" +
                                        "   select dbo.RUBROS.RUB_NOMBRE, 1 as vez from RUBROS where PED_CODIGO = 6\n" +
                                         "  ) r\n" +
                                    "group by r.RUB_NOMBRE\n", Sqlcon);
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

        public DataTable getReporteEncabezado(int idImagenologia)
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
            Sqlcmd = new SqlCommand(" SELECT (SELECT TOP (1) EMP_NOMBRE FROM dbo.EMPRESA) AS clinica, dbo.PACIENTES_DATOS_ADICIONALES.COD_PARROQUIA, dbo.PACIENTES_DATOS_ADICIONALES.COD_CANTON, dbo.PACIENTES_DATOS_ADICIONALES.COD_PROVINCIA, dbo.PACIENTES.PAC_HISTORIA_CLINICA, dbo.PACIENTES.PAC_FECHA_NACIMIENTO,dbo.PACIENTES.PAC_APELLIDO_PATERNO, dbo.PACIENTES.PAC_APELLIDO_MATERNO, dbo.PACIENTES.PAC_NOMBRE1, dbo.PACIENTES.PAC_IDENTIFICACION, \n" +
                                     "dbo.PACIENTES.PAC_NOMBRE2, dbo.TIPO_INGRESO.TIP_DESCRIPCION, dbo.ATENCIONES.HAB_CODIGO, dbo.HC_IMAGENOLOGIA.PRIORIDAD, dbo.HC_IMAGENOLOGIA.FECHA_CREACION,\n" +
                                     "concat('DR.', dbo.MEDICOS.MED_NOMBRE1, ' ', dbo.MEDICOS.MED_APELLIDO_PATERNO) as medico, dbo.MEDICOS.MED_RUC as COD_medico,\n" +
                                     "concat(MEDICOS_1.MED_NOMBRE1, ' ', MEDICOS_1.MED_APELLIDO_PATERNO) as tecnologo, concat(MEDICOS_1.MED_CODIGO_LIBRO, '-', MEDICOS_1.MED_CODIGO_FOLIO) as COD_tecn,\n" +
                                     "concat(MEDICOS_2.MED_NOMBRE1, ' ', MEDICOS_2.MED_APELLIDO_PATERNO) as radiologo, concat(MEDICOS_2.MED_CODIGO_LIBRO, '-', MEDICOS_2.MED_CODIGO_FOLIO) as COD_rad,\n" +
                                     "dbo.HC_IMAGENOLOGIA.motivo, dbo.HC_IMAGENOLOGIA.resumen_clinico, dbo.HC_IMAGENOLOGIA.estado_movimiento, dbo.HC_IMAGENOLOGIA.estado_retirarsevendas, dbo.HC_IMAGENOLOGIA.estado_medicopresente, dbo.HC_IMAGENOLOGIA.estado_encama, dbo.HC_IMAGENOLOGIA.concluciones\n" +
                                    "FROM dbo.HC_IMAGENOLOGIA INNER JOIN\n" +
                                     "dbo.ATENCIONES ON dbo.HC_IMAGENOLOGIA.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO INNER JOIN\n" +
                                     "dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO INNER JOIN\n" +
                                     "dbo.PACIENTES_DATOS_ADICIONALES ON dbo.ATENCIONES.DAP_CODIGO = dbo.PACIENTES_DATOS_ADICIONALES.DAP_CODIGO AND\n" +
                                     "dbo.PACIENTES.PAC_CODIGO = dbo.PACIENTES_DATOS_ADICIONALES.PAC_CODIGO INNER JOIN\n" +
                                     "dbo.TIPO_INGRESO ON dbo.ATENCIONES.TIP_CODIGO = dbo.TIPO_INGRESO.TIP_CODIGO INNER JOIN\n" +
                                     "dbo.MEDICOS ON dbo.HC_IMAGENOLOGIA.MED_CODIGO = dbo.MEDICOS.MED_CODIGO LEFT OUTER JOIN\n" +
                                     "dbo.MEDICOS AS MEDICOS_2 ON dbo.HC_IMAGENOLOGIA.MED_RADIOLOGO = MEDICOS_2.MED_CODIGO LEFT OUTER JOIN\n" +
                                     "dbo.MEDICOS AS MEDICOS_1 ON dbo.HC_IMAGENOLOGIA.MED_TECNOLOGO = MEDICOS_1.MED_CODIGO\n" +
                                    "WHERE dbo.HC_IMAGENOLOGIA.id_imagenologia = " + idImagenologia, Sqlcon);
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


        public DataTable getPedidoCabecera(int idImagenologia)
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
            Sqlcmd = new SqlCommand("SELECT        dbo.MEDICOS.MED_CODIGO, dbo.MEDICOS.MED_APELLIDO_PATERNO, dbo.MEDICOS.MED_APELLIDO_MATERNO, dbo.MEDICOS.MED_NOMBRE1, dbo.MEDICOS.MED_NOMBRE2, dbo.HC_IMAGENOLOGIA.FECHA_CREACION, \n" +
                                                             "dbo.HC_IMAGENOLOGIA.PRIORIDAD, dbo.HC_IMAGENOLOGIA.estado_movimiento, dbo.HC_IMAGENOLOGIA.estado_retirarsevendas, dbo.HC_IMAGENOLOGIA.estado_medicopresente, dbo.HC_IMAGENOLOGIA.estado_encama,\n" +
                                                             "dbo.HC_IMAGENOLOGIA.motivo, dbo.HC_IMAGENOLOGIA.resumen_clinico,\n" +
                                                             "MEDICOS_2.MED_CODIGO AS COD_TEC,\n" +
                                                             "MEDICOS_2.MED_APELLIDO_PATERNO AS TEC_AP, MEDICOS_2.MED_APELLIDO_MATERNO AS TEC_AM,\n" +
                                                             "MEDICOS_2.MED_NOMBRE1 AS TEC_N1, MEDICOS_2.MED_NOMBRE2 AS TEC_N2,\n" +
                                                             "MEDICOS_1.MED_CODIGO AS COD_RAD, MEDICOS_1.MED_APELLIDO_PATERNO AS RAD_AP,\n" +
                                                             "MEDICOS_1.MED_APELLIDO_MATERNO AS RAD_AM, MEDICOS_1.MED_NOMBRE2 AS RAD_N2, MEDICOS_1.MED_NOMBRE1 AS RAD_N1\n" +
                                    "FROM            dbo.HC_IMAGENOLOGIA INNER JOIN\n" +
                                                             "dbo.MEDICOS ON dbo.HC_IMAGENOLOGIA.MED_CODIGO = dbo.MEDICOS.MED_CODIGO LEFT OUTER JOIN\n" +
                                                             "dbo.MEDICOS AS MEDICOS_2 ON dbo.HC_IMAGENOLOGIA.MED_TECNOLOGO = MEDICOS_2.MED_CODIGO LEFT OUTER JOIN\n" +
                                                             "dbo.MEDICOS AS MEDICOS_1 ON dbo.HC_IMAGENOLOGIA.MED_RADIOLOGO = MEDICOS_1.MED_CODIGO\n" +
                                    "WHERE id_imagenologia = " + idImagenologia, Sqlcon);
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

        public DataTable getPedidoDiagnosticos(int idImagenologia)
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
            Sqlcmd = new SqlCommand("SELECT dbo.CIE10.CIE_CODIGO, dbo.CIE10.CIE_DESCRIPCION, dbo.HC_IMAGENOLOGIA_DIAGNOSTICOS.DEFINITIVO\n" +
                                    "FROM dbo.HC_IMAGENOLOGIA_DIAGNOSTICOS INNER JOIN dbo.CIE10 ON dbo.HC_IMAGENOLOGIA_DIAGNOSTICOS.CIE_CODIGO = dbo.CIE10.CIE_CODIGO\n" +
                                    "WHERE id_imagenologia = " + idImagenologia, Sqlcon);
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

        public DataTable getPedidoEstudios(int idImagenologia)
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
            Sqlcmd = new SqlCommand("SELECT dbo.HC_IMAGENOLOGIA_ESTUDIOS.PRO_CODIGO, dbo.PRODUCTO.PRO_DESCRIPCION, dbo.HC_IMAGENOLOGIA_ESTUDIOS.PRO_CODSUB, dbo.HC_IMAGENOLOGIA_ESTUDIOS.dato_adicional\n" +
                                    "FROM dbo.PRODUCTO INNER JOIN \n" +
                                    "Sic3000.dbo.ProductoSubdivision ON dbo.PRODUCTO.PRO_CODSUB = Sic3000.dbo.ProductoSubdivision.codsub AND dbo.PRODUCTO.PRO_CODDIV = Sic3000.dbo.ProductoSubdivision.coddiv INNER JOIN \n" +
                                    "dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = dbo.RUBROS.RUB_CODIGO INNER JOIN dbo.HC_IMAGENOLOGIA_ESTUDIOS ON dbo.PRODUCTO.PRO_CODIGO = dbo.HC_IMAGENOLOGIA_ESTUDIOS.PRO_CODIGO \n" +
                                    "WHERE id_imagenologia = " + idImagenologia, Sqlcon);
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

        public DataTable getSubareas()
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
            Sqlcmd = new SqlCommand("select r.RUB_CODIGO,r.RUB_NOMBRE from rubros as r where RUB_ASOCIADO=6", Sqlcon);
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

        public DataTable getSolicitudes(int atecodigo)
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
            Sqlcmd = new SqlCommand("SELECT dbo.HC_IMAGENOLOGIA.id_imagenologia as ID, concat('Fec.:',dbo.HC_IMAGENOLOGIA.FECHA_CREACION,'- [MEDICO:' ,dbo.MEDICOS.MED_APELLIDO_PATERNO,' ', dbo.MEDICOS.MED_NOMBRE1, ']   [MOTIVO:',dbo.HC_IMAGENOLOGIA.motivo,']') as datos\n" +
                                    "FROM dbo.HC_IMAGENOLOGIA INNER JOIN dbo.MEDICOS ON dbo.HC_IMAGENOLOGIA.MED_CODIGO = dbo.MEDICOS.MED_CODIGO\n" +
                                    "where dbo.HC_IMAGENOLOGIA.ATE_CODIGO =" + atecodigo, Sqlcon);
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

        public DataTable getInterconsultas(Int64 atecodigo)
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
            Sqlcmd = new SqlCommand("SELECT HI.HIN_CODIGO AS ID, '[FECHA: '+CAST(HI.HIN_FECHACREACION AS varchar)+']','[MEDICO: ' + HI.HIN_MED_INTERCONSULTADO + '] - [MOTIVO: ' + HI.HIN_DESCRIPCION_MOTIVO + ']' AS DATOS FROM HC_INTERCONSULTA HI WHERE HI.ATE_CODIGO = " + atecodigo, Sqlcon);
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
        public DataTable getHistopatologico(int atecodigo)
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
            Sqlcmd = new SqlCommand("SELECT HI.id AS ID, '[ESTUDIO SOLICITADO: ' + HI.estudio + ']' AS DATOS FROM HC_HISTOPATOLOGICO HI WHERE HI.ATE_CODIGO = " + atecodigo, Sqlcon);
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


        public DataTable getProtocolos(Int64 ate_codigo)
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
            Sqlcmd = new SqlCommand("SELECT PO.PROT_CODIGO AS ID, '[MED CIRUJANO: ' + PO.PROT_PROFESIONAL + '] - [MOTIVO: ' + PO.PROT_PREOPERATORIO + '] - [FECHA: '+CONVERT(varchar,PROT_FECHA,22)+']' AS DATOS  FROM HC_PROTOCOLO_OPERATORIO PO WHERE PO.ATE_CODIGO =" + ate_codigo + " order by PROT_FECHA desc ", Sqlcon);
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
        public DataTable getLaboratorio(Int64 ate_codigo)
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
            Sqlcmd = new SqlCommand("SELECT LC.LCL_CODIGO AS ID, '[MED SOLICITA: ' + M.MED_APELLIDO_PATERNO + ' ' + M.MED_NOMBRE1 + '] - [FECHA:'+CONVERT(varchar,LCL_FECHA_CREACION,21)+']' AS DATOS   FROM HC_LABORATORIO_CLINICO LC INNER JOIN MEDICOS M ON LC.MED_CODIGO = M.MED_CODIGO WHERE LC.ATE_CODIGO =" + ate_codigo, Sqlcon);
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
        public Int64 getCodigoProtocolo(Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            Int64 adf_codigo = 0;

            command = new SqlCommand("SELECT MAX(ADF_CODIGO)  AS ADF_CODIGO FROM ATENCION_DETALLE_FORMULARIOS_HCU ", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                adf_codigo = Convert.ToInt64(reader["ADF_CODIGO"].ToString()) + 1;
            }
            reader.Close();

            command = new SqlCommand("INSERT INTO ATENCION_DETALLE_FORMULARIOS_HCU VALUES(@adf_codigo, @ate_codigo, NULL, 21, GETDATE(), NULL, @id_usuario)", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@adf_codigo", adf_codigo);
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@id_usuario", His.Entidades.Clases.Sesion.codUsuario);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
            return adf_codigo;
        }
        public bool getProtocoloExiste(Int64 adf_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool existe = false;

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("SELECT ADF_CODIGO FROM ATENCION_DETALLE_FORMULARIOS_HCU WHERE ADF_CODIGO = @adf_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@adf_codigo", adf_codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (adf_codigo == Convert.ToInt64(reader["ADF_CODIGO"].ToString()))
                {
                    existe = true;
                }
                else
                    existe = false;
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return existe;
        }
        public DataTable getProductos()
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
            Sqlcmd = new SqlCommand(" SELECT dbo.PRODUCTO.PRO_CODIGO CODIGO, dbo.PRODUCTO.PRO_DESCRIPCION NOMBRE, dbo.RUBROS.RUB_NOMBRE SUBDIVISION, dbo.RUBROS.RUB_CODIGO CODSUBD " +
                                    "FROM dbo.PRODUCTO INNER JOIN " +
                                    "Sic3000.dbo.ProductoSubdivision ON dbo.PRODUCTO.PRO_CODSUB = Sic3000.dbo.ProductoSubdivision.codsub AND dbo.PRODUCTO.PRO_CODDIV = Sic3000.dbo.ProductoSubdivision.coddiv INNER JOIN  " +
                                    "dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = dbo.RUBROS.RUB_CODIGO " +
                                    "where dbo.RUBROS.RUB_ASOCIADO = 6", Sqlcon);
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

        public DataTable getCIE10()
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
            Sqlcmd = new SqlCommand("select * from CIE10", Sqlcon);
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

        public void saveSolicitud(PedidoImagen p)
        {
            string cadena_sql;
            if (p.id_imagenologia == 0)
            {
                string idnvo = nuevoID();

                cadena_sql = "SET IDENTITY_INSERT [dbo].[HC_IMAGENOLOGIA] ON;\n" + "INSERT INTO [dbo].[HC_IMAGENOLOGIA](id_imagenologia,[FECHA_CREACION],[ID_USUARIO],[ATE_CODIGO],[PRIORIDAD],[motivo],[resumen_clinico],[MED_CODIGO],[estado_movimiento],[estado_retirarsevendas],[estado_medicopresente],[estado_encama],[MED_TECNOLOGO],[MED_RADIOLOGO]) VALUES (" +
                    idnvo + ", '" +
                    (p.FECHA_CREACION).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "', " +
                       p.ID_USUARIO + ", " +
                       p.ATE_CODIGO + ", " +
                       p.PRIORIDAD + ", '" +
                       p.motivo + "', '" +
                       p.resumen_clinico + "', " +
                       p.MED_CODIGO + ", " +
                       p.estado_movimiento + ", " +
                       p.estado_retirarsevendas + ", " +
                       p.estado_medicopresente + ", " +
                       p.estado_encama + ", " + p.MED_TECNOLOGO + ", " + p.MED_RADIOLOGO +
                    ")\n" + "SET IDENTITY_INSERT [dbo].[HC_IMAGENOLOGIA] OFF;\n";

                List<PedidoImagen_estudios> est = p.estudios;
                foreach (var estudio in est)
                {
                    cadena_sql += "INSERT INTO [dbo].[HC_IMAGENOLOGIA_ESTUDIOS] ([id_imagenologia] ,[PRO_CODIGO] ,[PRO_CODSUB] ,[dato_adicional]) VALUES (" +
                                idnvo
                               + ", " + estudio.PRO_CODIGO
                               + ", " + estudio.PRO_CODSUB
                               + ", '" + estudio.dato_adicional
                               + "')\n";
                }
                List<PedidoImagen_diagnostico> diag = p.diagnosticos;
                foreach (var diagnostico in diag)
                {
                    cadena_sql += "INSERT INTO [dbo].[HC_IMAGENOLOGIA_DIAGNOSTICOS] ([id_imagenologia] ,[CIE_CODIGO] ,[DEFINITIVO]) VALUES (" +
                                idnvo
                               + ", '" + diagnostico.CIE_CODIGO
                               + "', " + diagnostico.DEFINITIVO
                               + ")\n";
                }


            }
            else
            {
                cadena_sql = "delete from dbo.HC_IMAGENOLOGIA_ESTUDIOS WHERE id_imagenologia=" + p.id_imagenologia + "\n"
                    + "delete from dbo.HC_IMAGENOLOGIA_DIAGNOSTICOS WHERE id_imagenologia = " + p.id_imagenologia + "\n"
                    + "delete from dbo.HC_IMAGENOLOGIA WHERE id_imagenologia = " + p.id_imagenologia + "\n" +
                    "SET IDENTITY_INSERT [dbo].[HC_IMAGENOLOGIA] ON;\n" + "INSERT INTO [dbo].[HC_IMAGENOLOGIA](id_imagenologia,[FECHA_CREACION],[ID_USUARIO],[ATE_CODIGO],[PRIORIDAD],[motivo],[resumen_clinico],[MED_CODIGO],[estado_movimiento],[estado_retirarsevendas],[estado_medicopresente],[estado_encama],[MED_TECNOLOGO],[MED_RADIOLOGO]) VALUES (" +
                       p.id_imagenologia + ", '" +
                       (p.FECHA_CREACION).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "', " +
                       p.ID_USUARIO + ", " +
                       p.ATE_CODIGO + ", " +
                       p.PRIORIDAD + ", '" +
                       p.motivo + "', '" +
                       p.resumen_clinico + "', " +
                       p.MED_CODIGO + ", " +
                       p.estado_movimiento + ", " +
                       p.estado_retirarsevendas + ", " +
                       p.estado_medicopresente + ", " +
                         p.estado_encama + ", " + p.MED_TECNOLOGO + ", " + p.MED_RADIOLOGO +
                    ")\n" + "SET IDENTITY_INSERT [dbo].[HC_IMAGENOLOGIA] OFF;\n";

                List<PedidoImagen_estudios> est = p.estudios;
                foreach (var estudio in est)
                {
                    cadena_sql += "INSERT INTO [dbo].[HC_IMAGENOLOGIA_ESTUDIOS] ([id_imagenologia] ,[PRO_CODIGO] ,[PRO_CODSUB] ,[dato_adicional]) VALUES (" +
                                p.id_imagenologia
                               + ", " + estudio.PRO_CODIGO
                               + ", " + estudio.PRO_CODSUB
                               + ", '" + estudio.dato_adicional
                               + "')\n";
                }
                List<PedidoImagen_diagnostico> diag = p.diagnosticos;
                foreach (var diagnostico in diag)
                {
                    cadena_sql += "INSERT INTO [dbo].[HC_IMAGENOLOGIA_DIAGNOSTICOS] ([id_imagenologia] ,[CIE_CODIGO] ,[DEFINITIVO]) VALUES (" +
                                p.id_imagenologia
                               + ", '" + diagnostico.CIE_CODIGO
                               + "', " + diagnostico.DEFINITIVO
                               + ")\n";
                }


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

        public string nuevoID()
        {

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

            Sqlcmd = new SqlCommand("SELECT count(id_imagenologia) as ID  FROM [His3000].[dbo].[HC_IMAGENOLOGIA] ", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);
            int rows = Convert.ToInt32(Dts.Rows[0]["ID"].ToString());

            string medi = "1";
            if (rows > 0)
            {
                Sqlcmd = new SqlCommand("SELECT max(id_imagenologia)+1 as ID  FROM [His3000].[dbo].[HC_IMAGENOLOGIA] ", Sqlcon);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataTable();
                Sqldap.Fill(Dts);
                medi = Dts.Rows[0]["ID"].ToString();
            }



            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return medi;

        }


        public void saveInformeImagen(PedidoImagen p)
        {
            string cadena_sql;
            if (p.id_imagenologia == 0)
            {
                string idnvo = nuevoID();

                cadena_sql = "INSERT INTO [dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME] ([id_IMAGENOLOGIA_AGENDAMIENTO],[cod_med_solicitante],[prioridad] ,[fecha_informe] ,[fecha_entrega],[informe] ,[DB_V] ,[LF_V],[PA_V],[DB_EG],[LF_EG],[PA_EG],[DB_P],[LF_P],[PA_P],[PLACENTA_F],[PLACENTA_M],[PLACENTA_P],[MASCULINO],[FEMENINO],[MULTIPLE] \n" +
                    ",[GRADO_MADUREZ],[ANTEVERSION],[RETROVERSION],[DIU],[FIBROMA],[MIOMA],[AUSENTE],[HIDROSALPIX],[QUISTE],[VACIA],[OCUPADA],[SACO_DOUGLAS],[recomendaciones],[PLACAS_ENVIADAS],[30X40],[8X10],[14X14],[14X17],[18X24],[ODONT],[DANADAS],[MEDIO_CONTRASTE]) VALUES( \n" +
                    idnvo + ", '" +
                    ")\n" + "SET IDENTITY_INSERT [dbo].[HC_IMAGENOLOGIA] OFF;\n";

                List<PedidoImagen_estudios> est = p.estudios;
                foreach (var estudio in est)
                {
                    cadena_sql += "INSERT INTO [dbo].[HC_IMAGENOLOGIA_ESTUDIOS] ([id_imagenologia] ,[PRO_CODIGO] ,[PRO_CODSUB] ,[dato_adicional]) VALUES (" +
                                idnvo
                               + ", " + estudio.PRO_CODIGO
                               + ", " + estudio.PRO_CODSUB
                               + ", '" + estudio.dato_adicional
                               + "')\n";
                }
                List<PedidoImagen_diagnostico> diag = p.diagnosticos;
                foreach (var diagnostico in diag)
                {
                    cadena_sql += "INSERT INTO [dbo].[HC_IMAGENOLOGIA_DIAGNOSTICOS] ([id_imagenologia] ,[CIE_CODIGO] ,[DEFINITIVO]) VALUES (" +
                                idnvo
                               + ", '" + diagnostico.CIE_CODIGO
                               + "', " + diagnostico.DEFINITIVO
                               + ")\n";
                }


            }
            else
            {
                cadena_sql = "delete from dbo.HC_IMAGENOLOGIA_ESTUDIOS WHERE id_imagenologia=" + p.id_imagenologia + "\n"
                    + "delete from dbo.HC_IMAGENOLOGIA_DIAGNOSTICOS WHERE id_imagenologia = " + p.id_imagenologia + "\n"
                    + "delete from dbo.HC_IMAGENOLOGIA WHERE id_imagenologia = " + p.id_imagenologia + "\n" +
                    "SET IDENTITY_INSERT [dbo].[HC_IMAGENOLOGIA] ON;\n" + "INSERT INTO [dbo].[HC_IMAGENOLOGIA](id_imagenologia,[FECHA_CREACION],[ID_USUARIO],[ATE_CODIGO],[PRIORIDAD],[motivo],[resumen_clinico],[MED_CODIGO],[estado_movimiento],[estado_retirarsevendas],[estado_medicopresente],[estado_encama],[MED_TECNOLOGO],[MED_RADIOLOGO]) VALUES (" +
                       p.id_imagenologia + ", '" +
                       (p.FECHA_CREACION).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "', " +
                       p.ID_USUARIO + ", " +
                       p.ATE_CODIGO + ", " +
                       p.PRIORIDAD + ", '" +
                       p.motivo + "', '" +
                       p.resumen_clinico + "', " +
                       p.MED_CODIGO + ", " +
                       p.estado_movimiento + ", " +
                       p.estado_retirarsevendas + ", " +
                       p.estado_medicopresente + ", " +
                         p.estado_encama + ", " + p.MED_TECNOLOGO + ", " + p.MED_RADIOLOGO +
                    ")\n" + "SET IDENTITY_INSERT [dbo].[HC_IMAGENOLOGIA] OFF;\n";

                List<PedidoImagen_estudios> est = p.estudios;
                foreach (var estudio in est)
                {
                    cadena_sql += "INSERT INTO [dbo].[HC_IMAGENOLOGIA_ESTUDIOS] ([id_imagenologia] ,[PRO_CODIGO] ,[PRO_CODSUB] ,[dato_adicional]) VALUES (" +
                                p.id_imagenologia
                               + ", " + estudio.PRO_CODIGO
                               + ", " + estudio.PRO_CODSUB
                               + ", '" + estudio.dato_adicional
                               + "')\n";
                }
                List<PedidoImagen_diagnostico> diag = p.diagnosticos;
                foreach (var diagnostico in diag)
                {
                    cadena_sql += "INSERT INTO [dbo].[HC_IMAGENOLOGIA_DIAGNOSTICOS] ([id_imagenologia] ,[CIE_CODIGO] ,[DEFINITIVO]) VALUES (" +
                                p.id_imagenologia
                               + ", '" + diagnostico.CIE_CODIGO
                               + "', " + diagnostico.DEFINITIVO
                               + ")\n";
                }


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

        public DataTable PacientesImagen()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataReader read;
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
            Sqlcmd = new SqlCommand("sp_ImagenPacientes", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.CommandTimeout = 180;
            read = Sqlcmd.ExecuteReader();
            Dts.Load(read);
            Sqlcon.Close();
            return Dts;
        }



        public void actualizarRadiologo(Int64 id, int medRadiologo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_IMAGENOLOGIA_AGENDAMIENTOS x = (from i in db.HC_IMAGENOLOGIA_AGENDAMIENTOS
                                                   where i.id == id
                                                   select i).FirstOrDefault();
                DbTransaction transaction;
                ConexionEntidades.ConexionEDM.Open();
                transaction = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    if (x != null)
                    {
                        x.med_radiologo = medRadiologo;

                        db.SaveChanges();
                        transaction.Commit();
                        ConexionEntidades.ConexionEDM.Close();
                    }
                    else
                    {
                        transaction.Rollback();
                        ConexionEntidades.ConexionEDM.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                }
            }
        }
    }
}
