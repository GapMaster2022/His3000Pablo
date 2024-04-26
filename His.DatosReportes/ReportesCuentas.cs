using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Data.OleDb;
using His.Entidades.Reportes;

namespace His.DatosReportes
{
    public class ReportesCuentas
    {
         string connectionString;
        OleDbConnection database;

        public ReportesCuentas()
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Reportes\\His3000Reportes.mdb";
            //string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Reportes\\bdFormulario003.mdb";
            try
            {
                database = new OleDbConnection(connectionString);
                database.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void ingresarCuentaPaciente(ReporteCuentaPaciente reporteCuenta)
        {
            try
            {
                string queryDeleteString = " DELETE * FROM CuentaPaciente ";
                OleDbCommand sqlDelete = new OleDbCommand();
                sqlDelete.CommandText = queryDeleteString;
                sqlDelete.Connection = database;
                sqlDelete.ExecuteNonQuery();

                string queryInsertString = "INSERT INTO CuentaPaciente (CUE_INSTITUCION,CUE_NOMBRE_PACIENTE,"+
                    "CUE_NTRAMITE, CUE_NEXPEDIENTE, CUE_HC, CUE_NATENCION, CUE_HABITACION, CUE_FECHA_INGRESO,"+
                    "CUE_FECHA_ALTA)" +
                    " VALUES ('" + reporteCuenta.CUE_INSTITUCION + "','" + reporteCuenta.CUE_NOMBRE_PACIENTE + "','" 
                    + reporteCuenta.CUE_NTRAMITE + "','" + reporteCuenta.CUE_NEXPEDIENTE + "','" + reporteCuenta.CUE_HC + "','" 
                    + reporteCuenta.CUE_NATENCION + "','" + reporteCuenta.CUE_HABITACION + "','" + reporteCuenta.CUE_FECHA_INGRESO + "','" 
                    + reporteCuenta.CUE_FECHA_ALTA + "')";
                OleDbCommand sqlInsert = new OleDbCommand();
                sqlInsert.CommandText = queryInsertString;
                sqlInsert.Connection = database;
                sqlInsert.ExecuteNonQuery();
                //database.Close();


                queryDeleteString = " DELETE * FROM DetalleCuentaPaciente ";

                sqlDelete = new OleDbCommand();
                sqlDelete.CommandText = queryDeleteString;
                sqlDelete.Connection = database;
                sqlDelete.ExecuteNonQuery();
                database.Close();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void ingresarDetalleCuenta(ReporteDetalleCuenta reporteD)
        {
            try
            {
                //string queryDeleteString = " DELETE * FROM Factur ";
                //OleDbCommand sqlDelete = new OleDbCommand();
                //sqlDelete.CommandText = queryDeleteString;
                //sqlDelete.Connection = database;
                //sqlDelete.ExecuteNonQuery();
                string queryInsertString = "INSERT INTO DetalleCuentaPaciente (DC_RUBRO," +
                                           "DC_FECHA_RUBRO,DC_CODIGO_RUBRO,DC_DETALLE_RUBRO, DC_COSTO_U_RUBRO,DC_CANTIDAD_RUBRO," +
                                            "DC_COSTO_T_RUBRO,DC_TOTAL_RUBRO)" +
                                            " VALUES ('" + reporteD.DC_RUBRO + "','" + reporteD.DC_FECHA_RUBRO + "','" + reporteD.DC_CODIGO_RUBRO + "','" + reporteD.DC_DETALLE_RUBRO + "','"
                                            + reporteD.DC_COSTO_U_RUBRO + "','" + reporteD.DC_CANTIDAD_RUBRO + "','" + reporteD.DC_COSTO_T_RUBRO + "','" + reporteD.DC_TOTAL_RUBRO + "')";
                OleDbCommand sqlInsert = new OleDbCommand();
                sqlInsert.CommandText = queryInsertString;
                sqlInsert.Connection = database;
                sqlInsert.ExecuteNonQuery();
                database.Close();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        


    }
}
