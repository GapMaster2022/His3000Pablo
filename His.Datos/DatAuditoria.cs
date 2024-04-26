using Core.Datos;
using His.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace His.Datos
{
    public class DatAuditoria
    {
        public bool regAuditoriaPacientes(DtoPacientesAud audPac)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();
            try
            {
                command = new SqlCommand("insert into SERIES3000_AUDITORIA..PACIENTES_AUD values (" + audPac.PAC_CODIGO + "," + audPac.ID_USUARIO + ",GETDATE(),'" + audPac.PAC_HISTORIA_CLINICA + 
                    "','" + audPac.DIPO_CODIINEC + "','" + audPac.E_CODIGO + "','" + audPac.PAC_NOMBRE1 + "','" + audPac.PAC_NOMBRE2 + "','" + audPac.PAC_APELLIDO_PATERNO +"','" + audPac.PAC_APELLIDO_MATERNO +
                    "','" + audPac.PAC_FECHA_NACIMIENTO + "','" + audPac.PAC_NACIONALIDAD + "','" + audPac.PAC_TIPO_IDENTIFICACION + "','" + audPac.PAC_IDENTIFICACION +"','" + audPac.PAC_EMAIL + 
                    "','" + audPac.PAC_GENERO + "','" + audPac.PAC_IMAGEN + "'," + Convert.ToInt16(audPac.PAC_ESTADO) + ",'" + audPac.PAC_DIRECTORIO + "','" + audPac.PAC_REFERENTE_NOMBRE +"','" + audPac.PAC_REFERENTE_PARENTESCO + 
                    "','" + audPac.PAC_REFERENTE_TELEFONO + "','" + audPac.PAC_ALERGIAS + "','" + audPac.PAC_OBSERVACIONES + "'," + audPac.GS_CODIGO +",'" + audPac.PAC_REFERENTE_DIRECCION + 
                    "','" + Convert.ToInt16(audPac.PAC_DATOS_INCOMPLETOS) + "')", connection);
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 380;
                reader = command.ExecuteReader();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                connection.Close();
                return false;
            }
        }
    }
}
