using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using His.Entidades.Reportes;

namespace His.DatosReportes
{
    public class ReportesForm008
    {
        string connectionString;
        OleDbConnection database;

        public ReportesForm008()
        {

            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Reportes\\HistoriasClinicas\\His3000Reportes.mdb";
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

        public void ingresarFor008(ReporteForm008 reporte)
        {
            try
            {
              

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void ingresarEmergencia(ReporteForm008 reporte)
        {
            try
            {
                string queryDeleteString = " DELETE * FROM formulario008 ";
                OleDbCommand sqlDelete = new OleDbCommand();
                sqlDelete.CommandText = queryDeleteString;
                sqlDelete.Connection = database;
                sqlDelete.ExecuteNonQuery();

    //            string queryInsertString = "INSERT INTO formulario008
    //SET CODIGO = reporte.codigo,
    //FOR_IS = reporte.forIs,
    //FOR_UO = reporte.forUo,
    //FOR_CODUO = reporte.forCoduo,
    //FOR_PARR = reporte.forParr,
    //FOR_CAT = reporte.forCat,
    //FOR_PROV = reporte.forProv,
    //FOR_HISTO = reporte.forHisto,
    //FOR_APEUNO = reporte.forApeuno,
    //FOR_APEDOS = reporte.forApedos,
    //FOR_NOMUNO = reporte.forNomuno,
    //FOR_NOMDOS = reporte.forNomdos,
    //FOR_CEDULA = reporte.forCedula,
    //FOR_DIRECCION = reporte.forDireccion,
    //FOR_BARRIO = reporte.forBarrio,
    //FOR_PARROQUIA = reporte.forParroquia,
    //FOR_CANTON = reporte.forCanton,
    //FOR_PROVINCIA = reporte.forProvincia,
    //FOR_ZONAU = reporte.forZonau,
    //FOR_TELEFONO = reporte.forTelefono,
    //FOR_FECHAN = reporte.forFechan,
    //FOR_LUGNAC = reporte.for_lugnac";

                OleDbCommand sqlInsert = new OleDbCommand();
               // sqlInsert.CommandText = queryInsertString;
                sqlInsert.Connection = database;
                sqlInsert.ExecuteNonQuery();

            }
            catch (Exception err)
            {
                throw err;
            }
        }

    }
}
