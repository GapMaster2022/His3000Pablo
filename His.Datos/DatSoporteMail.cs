using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Core.Datos;

namespace His.Datos
{
    class DatSoporteMail : MasterEmail
    {
        public DatSoporteMail()
        {
            CargarCorreo();
            initializeSmtpClient();
        }

        public void CargarCorreo()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            BaseContextoDatos obj = new BaseContextoDatos();
            try
            {
                conn = obj.ConectarBd();
                cmd = new SqlCommand("select * from CONVENIO_CORREO_ENVIA", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    senderMail = dr.GetString(1);
                    password = dr.GetString(2);
                    host = dr.GetString(3);
                    port = dr.GetInt32(4);
                    if (dr.GetBoolean(5))
                    {
                        ssl = true;
                    }
                    else
                    {
                        ssl = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
