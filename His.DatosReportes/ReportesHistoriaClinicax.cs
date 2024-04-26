using System;
using System.Data.OleDb;
using His.Entidades.Reportes;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using His.Entidades;
//using System.SqlConnection; 

namespace His.DatosReportes
{

    public class ReportesHistoriaClinica
    {
        string connectionString;
        OleDbConnection database;

        public ReportesHistoriaClinica()
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

        public void ingresarAnamnesis(ReporteAnamnesis reporte)
        {
            try
            {
                string queryDeleteString = " DELETE * FROM bdformulario003 ";

                OleDbCommand sqlDelete = new OleDbCommand();
                sqlDelete.CommandText = queryDeleteString;
                sqlDelete.Connection = database;
                sqlDelete.ExecuteNonQuery();

                string queryInsertString = "INSERT INTO bdformulario003 (FOR_ESTABLECIMIENTO, FOR_NOMBRES, FOR_APELLIDOS, FOR_SEXO, FOR_NUMERO_HOJA," +
                        " FOR_NUMERO_HISTORIA, FOR_MC_A, FOR_MC_B, FOR_MC_C, FOR_MC_D, FOR_AP_MENARQUIA, FOR_AP_MENOPAUSIA,FOR_AP_CICLOS,For_Ap_Vida_Sexual," +
                        "For_Ap_Gesta,For_Ap_Partos,For_Ap_Abortos,For_Ap_Cesareaas,For_Ap_Hijos_Vivos,For_Ap_Fum,For_Ap_Fup,For_Ap_Fuc,For_Ap_Biopsia," +
                        "For_Ap_Metodo_Planifi_Familiar,For_Ap_Terapia_Hormonal,For_Ap_Colpos_Copia,For_Ap_Mamografia," +
                        "For_Ap_Descripcion,FOR_AF_CARDIOPATIA,FOR_AF_DIABETES, FOR_AF_ENFVASCULARES,FOR_AF_HIPERTENSION," +
                        "FOR_AF_CANCER,FOR_AF_TUBERCULOSIS,FOR_AF_ENFMENTAL, FOR_AF_ENFINFECCIOSA,FOR_AF_MALINFOR,FOR_AF_OTRO," +
                        "FOR_AF_DESCRIPCION,For_Enf_Pro_Act," +
                        "FOR_RAOS_CPORGANISMO_SENTIDOS,FOR_RAOS_SPORGANISMO_SENTIDOS,FOR_RAOS_CPPRESPIRATORIO,FOR_RAOS_SPPRESPIRATORIO,FOR_RAOS_CPCARDIO_VASCULAR,FOR_RAOS_SPCARDIO_VASCULAR," +
                        "FOR_RAOS_CPDIGESTIVO,FOR_RAOS_SPDIGESTIVO,FOR_RAOS_CPGENITAL,FOR_RAOS_SPGENITAL,FOR_RAOS_CPURINARIO,FOR_RAOS_SPURINARIO,FOR_RAOS_CPMUSCULO_ESQUELETIVO,FOR_RAOS_SPMUSCULO_ESQUELETIVO," +
                        "FOR_RAOS_CPENDOCRINO,FOR_RAOS_SPENDOCRINO,FOR_RAOS_CPHEMO_LINFATICO,FOR_RAOS_SPHEMO_LINFATICO,FOR_RAOS_CPNERVIOSO,FOR_RAOS_SPNERVIOSO,FOR_RAOS_DESCRIPCION," +
                        "For_Svm_Presion_Arterialuno,For_Svm_Presion_Arterialdos,For_Svm_Frecuencia_Cardiaca,For_Svm_Frecuencia_Respiratoria,For_Svm_Temp_Rbucal,For_Svm_Temp_Raxilar," +
                        "For_Svm_Peso,For_Svm_Talla,For_Svm_Perimetro_Cefalico," +
                        "FOR_EXAMEN_FISICO_PIEL_CPFANERAS,FOR_EXAMEN_FISICO_PIEL_SPFANERAS,FOR_EXAMEN_FISICO_CPCABEZA,FOR_EXAMEN_FISICO_SPCABEZA,FOR_EXAMEN_FISICO_CPOJOS,FOR_EXAMEN_FISICO_SPOJOS," +
                        "FOR_EXAMEN_FISICO_CPOIDOS,FOR_EXAMEN_FISICO_SPOIDOS,FOR_EXAMEN_FISICO_CPNARIZ,FOR_EXAMEN_FISICO_SPNARIZ,FOR_EXAMEN_FISICO_CPBOCA,FOR_EXAMEN_FISICO_SPBOCA," +
                        "FOR_EXAMEN_FISICO_ORO_CPFARINGE,FOR_EXAMEN_FISICO_ORO_SPFARINGE,FOR_EXAMEN_FISICO_CPCUELLO,FOR_EXAMEN_FISICO_SPCUELLO,FOR_EXAMEN_FISICO_CPAXILAS_MAMAS,FOR_EXAMEN_FISICO_SPAXILAS_MAMAS," +
                        "FOR_EXAMEN_FISICO_CPTORAX,FOR_EXAMEN_FISICO_SPTORAX,FOR_EXAMEN_FISICO_CPABDOMEN,FOR_EXAMEN_FISICO_SPABDOMEN,FOR_EXAMEN_FISICO_CPCOL_VER,FOR_EXAMEN_FISICO_SPCOL_VER," +
                        "FOR_EXAMEN_FISICO_CPING_PERINE,FOR_EXAMEN_FISICO_SPING_PERINE,FOR_EXAMEN_FISICO_CPMIEMB_SUPER,FOR_EXAMEN_FISICO_SPMIEMB_SUPER,FOR_EXAMEN_FISICO_CPMIEMB_INF,FOR_EXAMEN_FISICO_SPMIEMB_INF," +
                        "FOR_EXAMEN_FISICO_CPORG_SENTIDOS,FOR_EXAMEN_FISICO_SPORG_SENTIDOS,FOR_EXAMEN_FISICO_CPRESPIRATORIO,FOR_EXAMEN_FISICO_SPRESPIRATORIO,FOR_EXAMEN_FISICO_CPCARDIO_VASC,FOR_EXAMEN_FISICO_SPCARDIO_VASC," +
                        "FOR_EXAMEN_FISICO_CPDIGESTIVO,FOR_EXAMEN_FISICO_SPDIGESTIVO,FOR_EXAMEN_FISICO_CPGENITAL,FOR_EXAMEN_FISICO_SPGENITAL,FOR_EXAMEN_FISICO_CPURINARIO,FOR_EXAMEN_FISICO_SPURINARIO," +
                        "FOR_EXAMEN_FISICO_CPMUSC_ESQUEL,FOR_EXAMEN_FISICO_SPMUSC_ESQUEL,FOR_EXAMEN_FISICO_CPENDOCRINO,FOR_EXAMEN_FISICO_SPENDOCRINO,FOR_EXAMEN_FISICO_CPHEMO_LINFAT,FOR_EXAMEN_FISICO_SPHEMO_LINFAT," +
                        "FOR_EXAMEN_FISICO_CPNEUROLOGICO,FOR_EXAMEN_FISICO_SPNEUROLOGICO,FOR_EXAMEN_FISICO_DESCRIPCION," +
                        "For_Diagnostico_Cie_Uno,For_Diagnostico_Cie_Uno_Desc,For_Diagnostico_Cie_Uno_Pre,For_Diagnostico_Cie_Uno_Def,For_Diagnostico_Cie_Dos," +
                        "For_Diagnostico_Cie_Dos_Desc,For_Diagnostico_Cie_Dos_Pre,For_Diagnostico_Cie_Dos_Def,For_Diagnostico_Cie_Tres," +
                        "For_Diagnostico_Cie_Tres_Desc,For_Diagnostico_Cie_Tres_Pre,For_Diagnostico_Cie_Tres_Def," +
                        "For_Diagnostico_Cie_Cuatro,For_Diagnostico_Cie_Cuatro_Desc,For_Diagnostico_Cie_Cuatro_Pre,For_Diagnostico_Cie_Cuatro_Def," +
                        "For_Diagnostico_Cie_Cinco,For_Diagnostico_Cie_Cinco_Dec,For_Diagnostico_Cie_Cinco_Pre,For_Diagnostico_Cie_Cinco_Def," +
                        "For_Diagnostico_Cie_Seis,For_Diagnostico_Cie_Seis_Desc,For_Diagnostico_Cie_Seis_Pre,For_Diagnostico_Cie_Seis_Def,For_Planes_Tratamiento," +
                        "FOR_FECHA,FOR_HORA,FOR_NOMBRE_PROF,FOR_CODIGO_PROF, FOR_NUMERO)" +
                        " VALUES ('" + reporte.ForEstablecimiento + "','" + reporte.ForNombres + "','" + reporte.ForApellidos + "','" + reporte.ForSexo + "', 1, " +
                        " '" + reporte.ForNumeroHistoria + "', '" + reporte.ForMcA + "', '" + reporte.ForMcB + "','" + reporte.ForMcC + "','" + reporte.ForMcD + "','"
                        + reporte.ForApMenarquia + "','" + reporte.ForApMenopausia + "','" + reporte.ForApCiclos + "','" + reporte.ForApVidaSexual + "','" + reporte.ForApGesta + "','"
                        + reporte.ForApPartos + "','" + reporte.ForApAbortos + "','" + reporte.ForApCesareaas + "','" + reporte.ForApHijosVivos + "','" + reporte.ForApFum + "','"
                        + reporte.ForApFup + "','" + reporte.ForApFuc + "','" + reporte.ForApBiopsia + "','" + reporte.ForApMetodoPlanifiFamiliar + "','"
                        + reporte.ForApTerapiaHormonal + "','" + reporte.ForApColposCopia + "','" + reporte.ForApMamografia + "','"
                        + reporte.ForApDescripcion + "','" + reporte.ForAfCardiopatia + "','" + reporte.ForAfDiabetes + "','" + reporte.ForAfEnfvasculares + "','" + reporte.ForAfHipertension + "','"
                        + reporte.ForAfCancer + "','" + reporte.ForAfTuberculosis + "','" + reporte.ForAfEnfmental + "','" + reporte.ForAfEnfinfecciosa + "','"
                        + reporte.ForAfMalinfor + "','" + reporte.ForAfOtro + "','" + reporte.ForAfDescripcion + "','" + reporte.ForEnfProAct + "','"
                        + reporte.ForRaosCporganismoSentidos + "','" + reporte.ForRaosSporganismoSentidos + "','" + reporte.ForRaosCpprespiratorio + "','" + reporte.ForRaosSpprespiratorio + "','"
                        + reporte.ForRaosCpcardioVascular + "','" + reporte.ForRaosSpcardioVascular + "','" + reporte.ForRaosCpdigestivo + "','" + reporte.ForRaosSpdigestivo + "','" + reporte.ForRaosCpgenital + "','" + reporte.ForRaosSpgenital + "','"
                        + reporte.ForRaosCpurinario + "','" + reporte.ForRaosSpurinario + "','" + reporte.ForRaosCpmusculoEsqueletivo + "','" + reporte.ForRaosSpmusculoEsqueletivo + "','" + reporte.ForRaosCpendocrino + "','" + reporte.ForRaosSpendocrino + "','"
                        + reporte.ForRaosCphemoLinfatico + "','" + reporte.ForRaosSphemoLinfatico + "','" + reporte.ForRaosCpnervioso + "','" + reporte.ForRaosSpnervioso + "','" + reporte.ForRaosDescripcion + "',"
                        + reporte.ForSvmPresionArterialuno + "," + reporte.ForSvmPresionArterialdos + "," + reporte.ForSvmFrecuenciaCardiaca + ","
                        + reporte.ForSvmFrecuenciaRespiratoria + "," + reporte.ForSvmTempRbucal + "," + reporte.ForSvmTempRaxilar + ","
                        + reporte.ForSvmPeso + "," + reporte.ForSvmTalla + "," + reporte.ForSvmPerimetroCefalico + ",'"
                        + reporte.ForExamenFisicoPielCpfaneras + "','" + reporte.ForExamenFisicoPielSpfaneras + "','" + reporte.ForExamenFisicoCpcabeza + "','" + reporte.ForExamenFisicoSpcabeza + "','" + reporte.ForExamenFisicoCpojos + "','" + reporte.ForExamenFisicoSpojos + "','"
                        + reporte.ForExamenFisicoCpoidos + "','" + reporte.ForExamenFisicoSpoidos + "','" + reporte.ForExamenFisicoCpnariz + "','" + reporte.ForExamenFisicoSpnariz + "','" + reporte.ForExamenFisicoCpboca + "','" + reporte.ForExamenFisicoSpboca + "','"
                        + reporte.ForExamenFisicoOroCpfaringe + "','" + reporte.ForExamenFisicoOroSpfaringe + "','" + reporte.ForExamenFisicoCpcuello + "','" + reporte.ForExamenFisicoSpcuello + "','" + reporte.ForExamenFisicoCpaxilasMamas + "','"
                        + reporte.ForExamenFisicoSpaxilasMamas + "','" + reporte.ForExamenFisicoCptorax + "','" + reporte.ForExamenFisicoSptorax + "','" + reporte.ForExamenFisicoCpabdomen + "','" + reporte.ForExamenFisicoSpabdomen + "','" + reporte.ForExamenFisicoCpcolVer + "','"
                        + reporte.ForExamenFisicoSpcolVer + "','" + reporte.ForExamenFisicoCpingPerine + "','" + reporte.ForExamenFisicoSpingPerine + "','" + reporte.ForExamenFisicoCpmiembSuper + "','" + reporte.ForExamenFisicoSpmiembSuper + "','" + reporte.ForExamenFisicoCpmiembInf + "','"
                        + reporte.ForExamenFisicoSpmiembInf + "','" + reporte.ForExamenFisicoCporgSentidos + "','" + reporte.ForExamenFisicoSporgSentidos + "','" + reporte.ForExamenFisicoCprespiratorio + "','" + reporte.ForExamenFisicoSprespiratorio + "','" + reporte.ForExamenFisicoCpcardioVasc + "','"
                        + reporte.ForExamenFisicoSpcardioVasc + "','" + reporte.ForExamenFisicoCpdigestivo + "','" + reporte.ForExamenFisicoSpdigestivo + "','" + reporte.ForExamenFisicoCpgenital + "','" + reporte.ForExamenFisicoSpgenital + "','" + reporte.ForExamenFisicoCpurinario + "','"
                        + reporte.ForExamenFisicoSpurinario + "','" + reporte.ForExamenFisicoCpmuscEsquel + "','" + reporte.ForExamenFisicoSpmuscEsquel + "','" + reporte.ForExamenFisicoCpendocrino + "','" + reporte.ForExamenFisicoSpendocrino + "','" + reporte.ForExamenFisicoCphemoLinfat + "','"
                        + reporte.ForExamenFisicoSphemoLinfat + "','" + reporte.ForExamenFisicoCpneurologico + "','" + reporte.ForExamenFisicoSpneurologico + "','"
                        + reporte.ForExamenFisicoDescripcion + "','" + reporte.ForDiagnosticoCieUno + "','" + reporte.ForDiagnosticoCieUnoDesc + "','"
                        + reporte.ForDiagnosticoCieUnoPre + "','" + reporte.ForDiagnosticoCieUnoDef + "','" + reporte.ForDiagnosticoCieDos + "','"
                        + reporte.ForDiagnosticoCieDosDesc + "','" + reporte.ForDiagnosticoCieDosPre + "','" + reporte.ForDiagnosticoCieDosDef + "','"
                        + reporte.ForDiagnosticoCieTres + "','" + reporte.ForDiagnosticoCieTresDesc + "','" + reporte.ForDiagnosticoCieTresPre + "','"
                        + reporte.ForDiagnosticoCieTresDef + "','" + reporte.ForDiagnosticoCieCuatro + "','" + reporte.ForDiagnosticoCieCuatroDesc + "','"
                        + reporte.ForDiagnosticoCieCuatroPre + "','" + reporte.ForDiagnosticoCieCuatroDef + "','" + reporte.ForDiagnosticoCieCinco + "','"
                        + reporte.ForDiagnosticoCieCincoDec + "','" + reporte.ForDiagnosticoCieCincoPre + "','" + reporte.ForDiagnosticoCieCincoDef + "','"
                        + reporte.ForDiagnosticoCieSeis + "','" + reporte.ForDiagnosticoCieSeisDesc + "','" + reporte.ForDiagnosticoCieSeisPre + "','"
                        + reporte.ForDiagnosticoCieSeisDef + "','" + reporte.ForPlanesTratamiento + "','" + reporte.ForFecha + "','" + reporte.ForHora + "','" + reporte.ForNombreProf + "','" + reporte.ForCodProf + "','" + reporte.ForHoja + "')";

                OleDbCommand sqlInsert = new OleDbCommand();
                sqlInsert.CommandText = queryInsertString;
                sqlInsert.Connection = database;
                sqlInsert.ExecuteNonQuery();
                database.Close();
            }
            catch (Exception err)
            {
                //throw err;
            }
        }


        public void ingresarImagenologia(PedidoImagen_reporte reporte)
        {
            try
            {
                string queryInsertString = "INSERT INTO IMAGEN (clinica,COD_PARROQUIA,COD_CANTON,COD_PROVINCIA,PAC_HISTORIA_CLINICA,edad,PAC_APELLIDO_PATERNO,PAC_APELLIDO_MATERNO,PAC_NOMBRE1,PAC_NOMBRE2,TIP_DESCRIPCION,HAB_CODIGO,FECHA_CREACION,medico,motivo,resumen_clinico,estado_movimiento,estado_retirarsevendas,estado_medicopresente,estado_encama,URGENTE,RUTINA,CONTROL,rubros,estudios,id_imagenologia)" +
                    " VALUES ('" + reporte.clinica + "','" + reporte.COD_PARROQUIA + "','" + reporte.COD_CANTON + "','"
                    + reporte.COD_PROVINCIA + "','" + reporte.PAC_HISTORIA_CLINICA + "','" + reporte.edad + "','"
                    + reporte.PAC_APELLIDO_PATERNO + "','" + reporte.PAC_APELLIDO_MATERNO + "','" + reporte.PAC_NOMBRE1 + "','"
                    + reporte.PAC_NOMBRE2 + "','" + reporte.TIP_DESCRIPCION + "','"
                    + reporte.HAB_CODIGO + "',#" + reporte.FECHA_CREACION + "#,'" + reporte.medico + "','"
                    + reporte.motivo + "','" + reporte.resumen_clinico + "','" + reporte.estado_movimiento + "','"
                    + reporte.estado_retirarsevendas + "','" + reporte.estado_medicopresente + "','" + reporte.estado_encama + "','"
                    + reporte.urgente + "','" + reporte.rutina + "','" + reporte.control + "','"
                    + reporte.rubros + "','" + reporte.estudios + "','" + reporte.id_imagenologia + "')";
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

        public void limpiarImagenologia()
        {
            try
            {
                string queryDeleteString = " DELETE * FROM IMAGEN ";

                OleDbCommand sqlDelete = new OleDbCommand();
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

        public void ingresarImagenologiaDiagnostico(PedidoImagen_reporteDiagnosticos reporte)
        {
            try
            {


                string queryInsertString = "INSERT INTO IMAGEN_dx (diagnostico,CIE,presuntivo,definitivo,id_imagenologia)" +
                    " VALUES ('" + reporte.diagnostico + "','" + reporte.CIE + "','" + reporte.presuntivo + "','"
                    + reporte.definitivo + "','" + reporte.id_imagenologia + "')";
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
        public void limpiarImagenologiaDiagnostico()
        {
            try
            {
                string queryDeleteString = " DELETE * FROM IMAGEN_dx ";

                OleDbCommand sqlDelete = new OleDbCommand();
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


        public void ingresarEpicrisis2(ReporteEpicrisis2 reporte)
        {
            try
            {
                string queryDeleteString = " DELETE * FROM Epicrisis2 ";

                OleDbCommand sqlDelete = new OleDbCommand();
                sqlDelete.CommandText = queryDeleteString;
                sqlDelete.Connection = database;
                sqlDelete.ExecuteNonQuery();

                string queryInsertString = "INSERT INTO Epicrisis2 (FOR_ESTABLECI,FOR_NOMBRES,FOR_APELLIDOS,FOR_SEXO," +
                    "FOR_NUM_HISTORIA,FOR_RESUMEN_CUADRO,FOR_EVOLUCION,FOR_HALLAZGOS," +
                    "FOR_RESU_TRATAM,FOR_DIAG_INGRESO_UNO_DESC," +
                    "FOR_DIAG_INGRESO_UNO_CIE,FOR_DIAG_INGRESO_UNO_PRE,FOR_DIAG_INGRESO_UNO_DEF,FOR_DIAG_INGRESO_DOS_DESC," +
                    "FOR_DIAG_INGRESO_DOS_CIE,FOR_DIAG_INGRESO_DOS_PRE,FOR_DIAG_INGRESO_DOS_DEF,FOR_DIAG_INGRESO_TRES_DESC," +
                    "FOR_DIAG_INGRESO_TRES_CIE,FOR_DIAG_INGRESO_TRES_PRE,FOR_DIAG_INGRESO_TRES_DEF,FOR_DIAG_INGRESO_CUATRO_DESC," +
                    "FOR_DIAG_INGRESO_CUATRO_CIE,FOR_DIAG_INGRESO_CUATRO_PRE,FOR_DIAG_INGRESO_CUATRO_DEF,FOR_DIAG_INGRESO_CINCO_DESC," +
                    "FOR_DIAG_INGRESO_CINCO_CIE,FOR_DIAG_INGRESO_CINCO_PRE,FOR_DIAG_INGRESO_CINCO_DEF,FOR_DIAG_INGRESO_SEIS_DESC," +
                    "FOR_DIAG_INGRESO_SEIS_CIE,FOR_DIAG_INGRESO_SEIS_PRE,FOR_DIAG_INGRESO_SEIS_DEF,FOR_DIAG_EGRESO_UNO_DESC," +
                    "FOR_DIAG_EGRESO_UNO_CIE,FOR_DIAG_EGRESO_UNO_PRE,FOR_DIAG_EGRESO_UNO_DEF,FOR_DIAG_EGRESO_DOS_DESC," +
                    "FOR_DIAG_EGRESO_DOS_CIE,FOR_DIAG_EGRESO_DOS_PRE,FOR_DIAG_EGRESO_DOS_DEF,FOR_DIAG_EGRESO_TRES_DESC," +
                    "FOR_DIAG_EGRESO_TRES_CIE,FOR_DIAG_EGRESO_TRES_PRE,FOR_DIAG_EGRESO_TRES_DEF,FOR_DIAG_EGRESO_CUATRO_DESC," +
                    "FOR_DIAG_EGRESO_CUATRO_CIE,FOR_DIAG_EGRESO_CUATRO_PRE,FOR_DIAG_EGRESO_CUATRO_DEF,FOR_DIAG_EGRESO_CINCO_DESC," +
                    "FOR_DIAG_EGRESO_CINCO_CIE,FOR_DIAG_EGRESO_CINCO_PRE,FOR_DIAG_EGRESO_CINCO_DEF,FOR_DIAG_EGRESO_SEIS_DESC," +
                    "FOR_DIAG_EGRESO_SEIS_CIE,FOR_DIAG_EGRESO_SEIS_PRE,FOR_DIAG_EGRESO_SEIS_DEF,FOR_COND_EGRESO_PRON," +
                    "FOR_MEDICOS_TRAT_UNO_NOMB,FOR_MEDICOS_TRAT_UNO_ESPEC,FOR_MEDICOS_TRAT_UNO_CODIGO,FOR_MEDICOS_TRAT_UNO_RESPO," +
                    "FOR_MEDICOS_TRAT_DOS_NOMB,FOR_MEDICOS_TRAT_DOS_ESPEC,FOR_MEDICOS_TRAT_DOS_CODIGO,FOR_MEDICOS_TRAT_DOS_RESPO," +
                    "FOR_MEDICOS_TRAT_TRES_NOMB,FOR_MEDICOS_TRAT_TRES_ESPEC,FOR_MEDICOS_TRAT_TRES_CODIGO,FOR_MEDICOS_TRAT_TRES_RESPO," +
                    "FOR_MEDICOS_TRAT_CUATRO_NOMB,FOR_MEDICOS_TRAT_CUATRO_ESPEC,FOR_MEDICOS_TRAT_CUATRO_CODIGO,FOR_MEDICOS_TRAT_CUATRO_RESPO," +
                    "FOR_ALTA_DEF,FOR_ALTA_TRA,FOR_ASINTOMATICO,FOR_DISC_LEVE,FOR_DISC_MODERADA,FOR_DISC_GRAVE,FOR_RETIRO_AUT," +
                    "FOR_RETIRO_NOAUTO,FOR_DEFUNCION_MENOS,FOR_DEFUNCION_MAS,FOR_DIAS_ESTANCIA,FOR_DIAS_INCAPACIDAD,FOR_FECHA,HORA," +
                    "FOR_NOM_PROFESIONAL,FOR_COD_PROFESIONAL,FOR_NUM_HOJA)" +
                    " VALUES ('" + reporte.forEstablec + "','" + reporte.forNombres + "','" + reporte.forApellidos + "','"
                    + reporte.forSexo + "','" + reporte.forHistoria + "','" + reporte.forResumenCadro + "','"
                    + reporte.forEvolucion + "','" + reporte.forHallazgo + "','" + reporte.ForResumenTra + "','"
                    + reporte.ForDiagIngresoUnoDesc + "','" + reporte.ForDiagIngresoUnoCie + "','"
                    + reporte.ForDiagIngresoUnoPre + "','" + reporte.ForDiagIngresoUnoDef + "','" + reporte.ForDiagIngresoDosDesc + "','"
                    + reporte.ForDiagIngresoDosCie + "','" + reporte.ForDiagIngresoDosPre + "','" + reporte.ForDiagIngresoDosDef + "','"
                    + reporte.ForDiagIngresoTresDesc + "','" + reporte.ForDiagIngresoTresCie + "','" + reporte.ForDiagIngresoTresPre + "','"
                    + reporte.ForDiagIngresoTresDef + "','" + reporte.ForDiagIngresoCuatroDesc + "','" + reporte.ForDiagIngresoCuatroCie + "','"
                    + reporte.ForDiagIngresoCuatroPre + "','" + reporte.ForDiagIngresoCuatroDef + "','" + reporte.ForDiagIngresoCincoDesc + "','"
                    + reporte.ForDiagIngresoCincoCie + "','" + reporte.ForDiagIngresoCincoPre + "','" + reporte.ForDiagIngresoCincoDef + "','"
                    + reporte.ForDiagIngresoSeisDesc + "','" + reporte.ForDiagIngresoSeisCie + "','" + reporte.ForDiagIngresoSeisPre + "','"
                    + reporte.ForDiagIngresoSeisDef + "','" + reporte.ForDiagEgresoUnoDesc + "','" + reporte.ForDiagEgresoUnoCie + "','"
                    + reporte.ForDiagEgresoUnoPre + "','" + reporte.ForDiagEgresoUnoDef + "','" + reporte.ForDiagEgresoDosDesc + "','"
                    + reporte.ForDiagEgresoDosCie + "','" + reporte.ForDiagEgresoDosPre + "','" + reporte.ForDiagEgresoDosDef + "','"
                    + reporte.ForDiagEgresoTresDesc + "','" + reporte.ForDiagEgresoTresCie + "','" + reporte.ForDiagEgresoTresPre + "','"
                    + reporte.ForDiagEgresoTresDef + "','" + reporte.ForDiagEgresoCuatroDesc + "','" + reporte.ForDiagEgresoCuatroCie + "','"
                    + reporte.ForDiagEgresoCuatroPre + "','" + reporte.ForDiagEgresoCuatroDef + "','" + reporte.ForDiagEgresoCincoDesc + "','"
                    + reporte.ForDiagEgresoCincoCie + "','" + reporte.ForDiagEgresoCincoPre + "','" + reporte.ForDiagEgresoCincoDef + "','"
                    + reporte.ForDiagEgresoSeisDesc + "','" + reporte.ForDiagEgresoSeisCie + "','" + reporte.ForDiagEgresoSeisPre + "','"
                    + reporte.ForDiagEgresoSeisDef + "','" + reporte.ForCondEgresoPron + "','" + reporte.ForMedicoTratUnoNom + "','"
                    + reporte.ForMedicoTratUnoEspec + "','" + reporte.ForMedicoTratUnoCodigo + "','" + reporte.ForMedicoTratUnoRespo + "','"
                    + reporte.ForMedicoTratDosNom + "','" + reporte.ForMedicoTratDosEspec + "','" + reporte.ForMedicoTratDosCodigo + "','"
                    + reporte.ForMedicoTratDosRespo + "','" + reporte.ForMedicoTratTresNom + "','" + reporte.ForMedicoTratTresEspec + "','"
                    + reporte.ForMedicoTratTresCodigo + "','" + reporte.ForMedicoTratTresRespo + "','" + reporte.ForMedicoTratCuatroNom + "','"
                    + reporte.ForMedicoTratCuatroEspec + "','" + reporte.ForMedicoTratCuatroCodigo + "','" + reporte.ForMedicoTratCuatroRespo + "','"
                    + reporte.ForAlataDef + "','" + reporte.ForAlataTra + "','" + reporte.ForAsintomatico + "','" + reporte.ForDiscLeve + "','"
                    + reporte.ForDisccModerada + "','" + reporte.ForDiscGrave + "','" + reporte.ForRetiroAut + "','" + reporte.ForRetiroNoAuto + "','"
                    + reporte.ForDefuncionMenos + "','" + reporte.ForDefucnionMas + "'," + reporte.ForDiasEstancia + "," + reporte.ForDiasIncapacidad + ",'"
                    + reporte.ForFecha + "','" + reporte.ForHora + "','" + reporte.ForNomProfesional + "','" + reporte.ForCodProfesional + "',"
                    + reporte.ForNumHoja + ")";
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


        public void ingresarEmergencia(ReporteForm008 hoja008)
        {
            try
            {
                string queryDeleteString = " DELETE * FROM formulario008 ";
                OleDbCommand sqlDelete = new OleDbCommand();
                sqlDelete.CommandText = queryDeleteString;
                sqlDelete.Connection = database;
                sqlDelete.ExecuteNonQuery();
                string queryInsertString = "INSERT INTO formulario008 (FOR_IS, FOR_UO, FOR_CODUO,FOR_PARR,FOR_CAT, FOR_PROV, FOR_HISTO," +
                    "FOR_APEUNO, FOR_APEDOS, FOR_NOMUNO, FOR_NOMDOS,FOR_CEDULA, FOR_DIRECCION, FOR_BARRIO, FOR_PARROQUIA, FOR_CANTON, FOR_PROVINCIA, " +
                    "FOR_ZONAU, FOR_TELEFONO, FOR_FECHAN, FOR_LUGNAC, FOR_NACIONAL, FOR_GRUPCULT, FOR_EDADA, FOR_GF, FOR_GM, FOR_ECSOL, FOR_ECCAS, FOR_ECDIV, " +
                    "FOR_ECVIU, FOR_ECUL, FOR_INSTRUC, FOR_FECHADM, FOR_OCUPACION, FOR_EMPRESAT, FOR_SEGURO, FOR_REFERIDO, FOR_AVISARA, FOR_PARENTESCO, " +
                    "FOR_PARDIREC, FOR_PARTELEFONO, FOR_AMBULATORIA, FOR_AMBULANCIA, FOR_OTROTRANS, FOR_FINF, FOR_INSTPERSO, FOR_INSTTELEFONO, FOR_IAM_HORA, " +
                    "FOR_IAM_GSF,USUARIO)" +
                    " VALUES ('" + hoja008.forIs + "','" + hoja008.forUo + "','" + hoja008.forCoduo + "','" + hoja008.forParr + "','" + hoja008.forCat + "','"
                    + hoja008.forProv + "','" + hoja008.forHisto + "','" + hoja008.forApeuno + "','" + hoja008.forApedos + "','" + hoja008.forNomuno + "','"
                    + hoja008.forNomdos + "','" + hoja008.forCedula + "','" + hoja008.forDireccion + "','" + hoja008.forBarrio + "','" + hoja008.forParroquia + "','"
                    + hoja008.forCanton + "','" + hoja008.forProvincia + "','" + hoja008.forZonau + "','" + hoja008.forTelefono + "','" + hoja008.forFechan + "','"
                    + hoja008.forLugnac + "','" + hoja008.forNacional + "','" + hoja008.forGrupcult + "','" + hoja008.forEdada + "','" + hoja008.forGf + "','"
                    + hoja008.forGm + "','" + hoja008.forEcsol + "','" + hoja008.forEccas + "','" + hoja008.forEcdiv + "','" + hoja008.forEcviu + "','"
                    + hoja008.forEcul + "','" + hoja008.forInstruc + "','" + hoja008.forFechadm + "','" + hoja008.forOcupacion + "','" + hoja008.forEmpresat + "','"
                    + hoja008.forSeguro + "','" + hoja008.forReferido + "','" + hoja008.forAvisara + "','" + hoja008.forParentesco + "','" + hoja008.forPardirec + "','"
                    + hoja008.forPartelefono + "','" + hoja008.forAmbulatoria + "','" + hoja008.forAmbulancia + "','" + hoja008.forOtrotrans + "','" + hoja008.forFinf + "','"
                    + hoja008.forInstperso + "','" + hoja008.forInsttelefono + "','" + hoja008.forIamHora + "','" + hoja008.forIamGsf + "','" + hoja008.usuario + "')";
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

        public void ingresarInterConsulta(ReporteInterconsulta reporte)
        {
            try
            {
                string queryDeleteString = " DELETE * FROM Interconsulta ";

                OleDbCommand sqlDelete = new OleDbCommand();
                sqlDelete.CommandText = queryDeleteString;
                sqlDelete.Connection = database;
                sqlDelete.ExecuteNonQuery();

                string queryInsertString = "INSERT INTO Interconsulta (for_nombres,for_apellidos,for_sexo,for_edad,for_historia," +
                       "for_est_destino,for_ser_consulta,for_ser_solicita,for_sala,for_cama,for_normal,for_urgente," +
                       "for_med_int,for_desc_motivo,for_cuadro_clinico,for_resultado,for_diag_uno_desc,for_diag_uno_cie," +
                       "for_diag_uno_pre,for_diag_uno_def,for_diag_dos_desc,for_diag_dos_cie,for_diag_dos_pre,for_diag_dos_def," +
                       "for_diag_tres_desc,for_diag_tres_cie,for_diag_tres_pre,for_diag_tres_def,for_diag_cuatro_desc," +
                       "for_diag_cuatro_cie,for_diag_cuatro_pre,for_diag_cuatro_def,for_diag_cinco_desc,for_diag_cinco_cie," +
                       "for_diag_cinco_pre,for_diag_cinco_def,for_diag_seis_desc,for_diag_seis_cie,for_diag_seis_pre," +
                       "for_diag_seis_def,for_planes_terap,for_cuadro_clinico_inter,for_resumen_criterio,for_diagnos_uno_desc," +
                       "for_diagnos_uno_cie,for_diagnos_uno_pre,for_diagnos_uno_def,for_diagnos_dos_desc,for_diagnos_dos_cie," +
                       "for_diagnos_dos_pre,for_diagnos_dos_def,for_diagnos_tres_desc,for_diagnos_tres_cie,for_diagnos_tres_pre," +
                       "for_diagnos_tres_def,for_diagnos_cuatro_desc,for_diagnos_cuatro_cie,for_diagnos_cuatro_pre,for_diagnos_cuatro_def," +
                       "for_diagnos_cinco_desc,for_diagnos_cinco_cie,for_diagnos_cinco_pre,for_diagnos_cinco_def,for_diagnos_seis_desc," +
                       "for_diagnos_seis_cie,for_diagnos_seis_pre,for_diagnos_seis_def,for_plan_diag,for_plan_tratamiento,for_fecha," +
                       "for_hora,for_nom_prof,for_codigo_prof,for_num_hoja)" +
                       " VALUES ('" + reporte.for_nombres + "','" + reporte.for_apellidos + "','" + reporte.for_sexo + "','" + reporte.for_edad + "','" + reporte.for_historia + "','"
                       + reporte.for_est_destino + "','" + reporte.for_ser_consulta + "','" + reporte.for_ser_solicita + "','" + reporte.for_sala + "','" + reporte.for_cama + "','"
                       + reporte.for_normal + "','" + reporte.for_urgente + "','" + reporte.for_med_int + "','" + reporte.for_desc_motivo + "','" + reporte.for_cuadro_clinico + "','"
                       + reporte.for_resultado + "','" + reporte.for_diag_uno_desc + "','" + reporte.for_diag_uno_cie + "','" + reporte.for_diag_uno_pre + "','" + reporte.for_diag_uno_def + "','"
                       + reporte.for_diag_dos_desc + "','" + reporte.for_diag_dos_cie + "','" + reporte.for_diag_dos_pre + "','" + reporte.for_diag_dos_def + "','" + reporte.for_diag_tres_desc + "','"
                       + reporte.for_diag_tres_cie + "','" + reporte.for_diag_tres_pre + "','" + reporte.for_diag_tres_def + "','" + reporte.for_diag_cuatro_desc + "','" + reporte.for_diag_cuatro_cie + "','"
                       + reporte.for_diag_cuatro_pre + "','" + reporte.for_diag_cuatro_def + "','" + reporte.for_diag_cinco_desc + "','" + reporte.for_diag_cinco_cie + "','" + reporte.for_diag_cinco_pre + "','"
                       + reporte.for_diag_cinco_def + "','" + reporte.for_diag_seis_desc + "','" + reporte.for_diag_seis_cie + "','" + reporte.for_diag_seis_pre + "','" + reporte.for_diag_seis_def + "','"
                       + reporte.for_planes_terap + "','" + reporte.for_cuadro_clinico_inter + "','" + reporte.for_resumen_criterio + "','" + reporte.for_diagnos_uno_desc + "','"
                       + reporte.for_diagnos_uno_cie + "','" + reporte.for_diagnos_uno_pre + "','" + reporte.for_diagnos_uno_def + "','" + reporte.for_diagnos_dos_desc + "','"
                       + reporte.for_diagnos_dos_cie + "','" + reporte.for_diagnos_dos_pre + "','" + reporte.for_diagnos_dos_def + "','" + reporte.for_diagnos_tres_desc + "','"
                       + reporte.for_diagnos_tres_cie + "','" + reporte.for_diagnos_tres_pre + "','" + reporte.for_diagnos_tres_def + "','" + reporte.for_diagnos_cuatro_desc + "','"
                       + reporte.for_diagnos_cuatro_cie + "','" + reporte.for_diagnos_cuatro_pre + "','" + reporte.for_diagnos_cuatro_def + "','" + reporte.for_diagnos_cinco_desc + "','"
                       + reporte.for_diagnos_cinco_cie + "','" + reporte.for_diagnos_cinco_pre + "','" + reporte.for_diagnos_cinco_def + "','" + reporte.for_diagnos_seis_desc + "','"
                       + reporte.for_diagnos_seis_cie + "','" + reporte.for_diagnos_seis_pre + "','" + reporte.for_diagnos_seis_def + "','" + reporte.for_plan_diag + "','"
                       + reporte.for_plan_tratamiento + "','" + reporte.for_fecha + "','" + reporte.for_hora + "','" + reporte.for_nom_prof + "','" + reporte.for_codigo_prof + "','" + reporte.for_num_hoja + "')";
                OleDbCommand sqlInsert = new OleDbCommand();
                sqlInsert.CommandText = queryInsertString;
                sqlInsert.Connection = database;
                sqlInsert.ExecuteNonQuery();
                database.Close();
            }
            catch (Exception err)
            {
                //throw err;
            }
        }


        public void ingresarProtocolo(ReporteProtocoloOperatorio reporte)
        {
            try
            {
                string queryDeleteString = " DELETE * FROM ProtocoloOperatorio ";

                OleDbCommand sqlDelete = new OleDbCommand();
                sqlDelete.CommandText = queryDeleteString;
                sqlDelete.Connection = database;
                sqlDelete.ExecuteNonQuery();

                string queryInsertString = "INSERT INTO ProtocoloOperatorio (PROT_HISTORIA_CLINICA,PROT_NUMHOJA,PROT_NOMBREPACIENTE," +
                    "PROT_APELLIDOPACIENTE, PROT_GENERO, PROT_MEDICO, PROT_SERVICIO, PROT_SALA, PROT_HABITACION, PROT_PREOPERATORIO, " +
                    "PROT_POSTOPERATORIO, PROT_PROYECTADA, PROT_ELECTIVA, PROT_EMERGENCIA, PROT_PALEATIVA, PROT_REALIZADO, PROT_CIRUJANO," +
                    "PROT_PAYUDANTE, PROT_SAYUDANTE, PROT_TAYUDANTE, PROT_INSTRUMENTISTA, PROT_CIRCULANTE, PROT_ANESTESISTA, PROT_AYUANESTESISTA, " +
                    "PROT_FECHA, PROT_HORAI, PROT_HORAT,PROT_TIPOANESTESIA,PROT_DIERESIS, PROT_EXPOSICION, PROT_EXPLORACION, PROT_PROCEDIMIENTO,PROT_PROCEDIMIENTO2," +
                    "PROT_SINTESIS,PROT_COMPLICACIONES, PROT_EXAMENHISSI, PROT_EXAMENHISNO, PROT_DIAGNOSTICOSHIS, PROT_DICTADO, PROT_FECHADIC, " +
                    "PROT_HORADIC,PROT_ESCRITA, PROT_PROFESIONAL, PROT_FECHA_OD, PROT_FECHA_OM, PROT_FECHA_OA, PROT_FECHA_DH," +
                    "PROT_FECHA_DM, PROT_FECHA_DD, PROT_FECHA_DA)" +
                    " VALUES ('" + reporte.PROT_HISTORIA_CLINICA + "','" + reporte.PROT_NUMHOJA + "','" + reporte.PROT_NOMBREPACIENTE + "','"
                    + reporte.PROT_APELLIDOPACIENTE + "','" + reporte.PROT_GENERO + "','" + reporte.PROT_MEDICO + "','" + reporte.PROT_SERVICIO + "','"
                    + reporte.PROT_SALA + "','" + reporte.PROT_HABITACION + "','" + reporte.PROT_PREOPERATORIO + "','" + reporte.PROT_POSTOPERATORIO + "','"
                    + reporte.PROT_PROYECTADA + "','" + reporte.PROT_ELECTIVA + "','" + reporte.PROT_EMERGENCIA + "','" + reporte.PROT_PALEATIVA + "','"
                    + reporte.PROT_REALIZADO + "','" + reporte.PROT_CIRUJANO + "','" + reporte.PROT_PAYUDANTE + "','" + reporte.PROT_SAYUDANTE + "','"
                    + reporte.PROT_TAYUDANTE + "','" + reporte.PROT_INSTRUMENTISTA + "','" + reporte.PROT_CIRCULANTE + "','" + reporte.PROT_ANESTESISTA + "','"
                    + reporte.PROT_AYUANESTESISTA + "','" + reporte.PROT_FECHA + "','" + reporte.PROT_HORAI + "','" + reporte.PROT_HORAT + "','"
                    + reporte.PROT_TIPOANESTESIA + "','" + reporte.PROT_DIERESIS + "','" + reporte.PROT_EXPOSICION + "','" + reporte.PROT_EXPLORACION + "','"
                    + reporte.PROT_PROCEDIMIENTO + "','" + reporte.PROT_PROCEDIMIENTO2 + "','" + reporte.PROT_SINTESIS + "','" + reporte.PROT_COMPLICACIONES + "','" + reporte.PROT_EXAMENHISSI + "','" + reporte.PROT_EXAMENHISNO + "','"
                    + reporte.PROT_DIAGNOSTICOSHIS + "','" + reporte.PROT_DICTADO + "','" + reporte.PROT_FECHADIC + "','" + reporte.PROT_HORADIC + "','"
                    + reporte.PROT_ESCRITA + "','" + reporte.PROT_PROFESIONAL + "','" + reporte.PROT_FECHA_OD + "','" + reporte.PROT_FECHA_OM + "','" + reporte.PROT_FECHA_OA + "','"
                    + reporte.PROT_FECHA_DH + "','" + reporte.PROT_FECHA_DM + "','" + reporte.PROT_FECHA_DD + "','" + reporte.PROT_FECHA_DA + "')";
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

        public void EliminarDatosAdmisionEgreso()
        {
            try
            {
                string queryDeleteString = " DELETE * FROM AdmisionEgreso ";

                OleDbCommand sqlDelete = new OleDbCommand();
                sqlDelete.CommandText = queryDeleteString;
                sqlDelete.Connection = database;
                sqlDelete.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void ingresarAdmisionEgreso(ReporteAdmisionEgreso reporte)
        {
            try
            {
                //string queryDeleteString = " DELETE * FROM AdmisionEgreso ";

                //OleDbCommand sqlDelete = new OleDbCommand();
                //sqlDelete.CommandText = queryDeleteString;
                //sqlDelete.Connection = database;
                //sqlDelete.ExecuteNonQuery();

                string queryInsertString = "INSERT INTO AdmisionEgreso (FORM_FECHA_INGRESO,FORM_FECHA_EGRESO," +
                    "FORM_DIAS, FORM_SERVICIO, FORM_ALTA, FORM_MIERTE_MENOS, FORM_MUERTE_MAS, FORM_DIAG_ING," +
                    "FORM_DIAG_ING_CIE,FORM_DIAG_ING_PRES, FORM_DIAG_ING_DEF, FORM_DIAG_EGRE, FORM_DIAG_EGRE_CIE," +
                    "FORM_DIAG_EGRE_PRES,FORM_DIAG_EGRE_DEF, FORM_TRAT_CLINICO, FORM_TRAT_QUIRUR, FORM_TRAT_PROCED," +
                    "FORM_COD_RESP)" +
                    " VALUES ('" + reporte.FORM_FECHA_INGRESO + "','" + reporte.FORM_FECHA_EGRESO + "','"
                    + reporte.FORM_DIAS + "','" + reporte.FORM_SERVICIO + "','" + reporte.FORM_ALTA + "','"
                    + reporte.FORM_MIERTE_MENOS + "','" + reporte.FORM_MUERTE_MAS + "','" + reporte.FORM_DIAG_ING + "','"
                    + reporte.FORM_DIAG_ING_CIE + "','" + reporte.FORM_DIAG_ING_PRES + "','" + reporte.FORM_DIAG_ING_DEF + "','"
                    + reporte.FORM_DIAG_EGRE + "','" + reporte.FORM_DIAG_EGRE_CIE + "','" + reporte.FORM_DIAG_EGRE_PRES + "','"
                    + reporte.FORM_DIAG_EGRE_DEF + "','" + reporte.FORM_TRAT_CLINICO + "','" + reporte.FORM_TRAT_QUIRUR + "','"
                    + reporte.FORM_TRAT_PROCED + "','" + reporte.FORM_COD_RESP + "')";
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

        public void ingresarEvolucion(ReporteEvolucion reporte)
        {
            try
            {
                string queryDeleteString = " DELETE * FROM Evolucion ";

                OleDbCommand sqlDelete = new OleDbCommand();
                sqlDelete.CommandText = queryDeleteString;
                sqlDelete.Connection = database;
                sqlDelete.ExecuteNonQuery();

                string queryInsertString = "INSERT INTO Evolucion (FOR_ESTABLECIMIENTO," +
                    "FOR_NOMBRE,FOR_APELLIDO,FOR_SEXO,FOR_HOJA,FOR_HISTORIA,FOR_FECHA,FOR_HORA," +
                    "FOR_NOTAS_EVOLUCION,FOR_FARMACOS,FOR_ADM_FAR,FOR_PROFESIONAL)" +
                    " VALUES ('" + reporte.FOR_ESTABLECIMIENTO + "','" + reporte.FOR_NOMBRE + "','"
                    + reporte.FOR_APELLIDO + "','" + reporte.FOR_SEXO + "','" + reporte.FOR_HOJA + "','"
                    + reporte.FOR_HISTORIA + "','" + reporte.FOR_FECHA + "','" + reporte.FOR_HORA + "','"
                    + reporte.FOR_NOTAS_EVOLUCION + "','" + reporte.FOR_FARMACOS + "','"
                    + reporte.FOR_ADM_FAR + "','" + reporte.FOR_PROFESIONAL + "')";
                OleDbCommand sqlInsert = new OleDbCommand();
                sqlInsert.CommandText = queryInsertString;
                sqlInsert.Connection = database;
                sqlInsert.ExecuteNonQuery();
                //database.Close();

                queryDeleteString = " DELETE * FROM DetalleEvolucion ";

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


        public void ingresarEvolucionDetalle(ReporteEvolucionDetalle reporte)
        {
            try
            {
                string queryInsertString = "INSERT INTO DetalleEvolucion (DETE_FECHA," +
                                            "DETE_HORA,DETE_NORAS_EVOLUCION,DETE_FARMACOS, DETE_ADMIN_FARM,DETE_PROFESIONAL" +
                                            ")" +
                    " VALUES ('" + reporte.DETE_FECHA + "','" + reporte.DETE_HORA + "','"
                    + reporte.DETE_NORAS_EVOLUCION + "','" + reporte.DETE_FARMACOS + "','"
                    + reporte.DETE_ADMIN_FARM + "','" + reporte.DETE_PROFESIONAL + "')";
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


        public void ingresarFactura(ReporteFactura reporteFactura)
        {
            try
            {
                string queryDeleteString = " DELETE * FROM Factura ";
                OleDbCommand sqlDelete = new OleDbCommand();
                sqlDelete.CommandText = queryDeleteString;
                sqlDelete.Connection = database;
                sqlDelete.ExecuteNonQuery();
                string queryInsertString = "INSERT INTO Factura (Fac_NFactura,Fac_NombreCliente," +
                    "Fac_Ruc,Fac_Telefono,Fac_NombrePaciente,Fac_Habitacion,Fac_Control,Fac_DireccionP," +
                    "Fac_CIPaciente,Fac_TelefonoP,Fac_CodMedicoT,Fac_MedicoT,Fac_HistoriaClinica,Fac_FechaEmision," +
                    "Fac_FechaIngreso,Fac_FechaAlta,Fac_Dias,Fac_Hos_Pac,Fac_Hos_Acom,Fac_Die_Pac,Fac_Die_Acom," +
                    "Fac_Lab_Clin,Fac_Lab_Pat,Fac_Imagen,Fac_Farmacia,Fac_Sal_Cirugia,Fac_Emergencia,Fac_Neonatologia," +
                    "Fac_Sal_Partos,Fac_Ter_Intensiva,Fac_Ter_Resp,Fac_Oxigeno,Fac_Monitor,Fac_EKG,Fac_Sum_Mater," +
                    "Fac_Tel,Fac_Ambulancia,Fac_Adm_Medicamentos,Fac_Der_Aneste,Fac_Der_Recuper,Fac_Ser_Clinica,Fac_Contado," +
                    "Fac_Credito,Fac_Cheque,Fac_Banco,Fac_TCredito,Fac_SubTotal,Fac_IvaUno,Fac_IvaDos,Fac_IvaTres,Fac_Total, " +
                    "Fac_IVA,Fac_Num_Contro,Fac_Usuario)" +
                    " VALUES ('" + reporteFactura.Fac_NFactura + "','" + reporteFactura.Fac_NombreCliente + "','"
                    + reporteFactura.Fac_Ruc + "','" + reporteFactura.Fac_Telefono + "','" + reporteFactura.Fac_NombrePaciente + "','"
                    + reporteFactura.Fac_Habitacion + "','" + reporteFactura.Fac_Control + "','" + reporteFactura.Fac_DireccionP + "','"
                    + reporteFactura.Fac_CIPaciente + "','" + reporteFactura.Fac_TelefonoP + "','" + reporteFactura.Fac_CodMedicoT + "','"
                    + reporteFactura.Fac_MedicoT + "','" + reporteFactura.Fac_HistoriaClinica + "','" + reporteFactura.Fac_FechaEmision + "','"
                    + reporteFactura.Fac_FechaIngreso + "','" + reporteFactura.Fac_FechaAlta + "','" + reporteFactura.Fac_Dias + "',"
                    + reporteFactura.Fac_Hos_Pac + "," + reporteFactura.Fac_Hos_Acom + "," + reporteFactura.Fac_Die_Pac + ","
                    + reporteFactura.Fac_Die_Acom + "," + reporteFactura.Fac_Lab_Clin + "," + reporteFactura.Fac_Lab_Pat + ","
                    + reporteFactura.Fac_Imagen + "," + reporteFactura.Fac_Farmacia + "," + reporteFactura.Fac_Sal_Cirugia + ","
                    + reporteFactura.Fac_Emergencia + "," + reporteFactura.Fac_Neonatologia + "," + reporteFactura.Fac_Sal_Partos + ","
                    + reporteFactura.Fac_Ter_Intensiva + "," + reporteFactura.Fac_Ter_Resp + "," + reporteFactura.Fac_Oxigeno + ","
                    + reporteFactura.Fac_Monitor + "," + reporteFactura.Fac_EKG + "," + reporteFactura.Fac_Sum_Mater + ","
                    + reporteFactura.Fac_Tel + "," + reporteFactura.Fac_Ambulancia + "," + reporteFactura.Fac_Adm_Medicamentos + ","
                    + reporteFactura.Fac_Der_Aneste + "," + reporteFactura.Fac_Der_Recuper + "," + reporteFactura.Fac_Ser_Clinica + ",'"
                    + reporteFactura.Fac_Contado + "','" + reporteFactura.Fac_Credito + "','" + reporteFactura.Fac_Cheque + "','"
                    + reporteFactura.Fac_Banco + "','" + reporteFactura.Fac_TCredito + "'," + reporteFactura.Fac_SubTotal + ","
                    + reporteFactura.Fac_IvaUno + "," + reporteFactura.Fac_IvaDos + "," + reporteFactura.Fac_IvaTres + ","
                    + reporteFactura.Fac_Total + ",'" + reporteFactura.Fac_IVA + "','" + reporteFactura.Fac_Num_Contro + "','"
                    + reporteFactura.Fac_Usuario + "')";
                OleDbCommand sqlInsert = new OleDbCommand();
                sqlInsert.CommandText = queryInsertString;
                sqlInsert.Connection = database;
                sqlInsert.ExecuteNonQuery();
                //database.Close();


                queryDeleteString = " DELETE * FROM DetalleEvolucion ";

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

        public void ingresarOdontologia(ReporteOdontologia reporte)
        {
            try
            {
                string queryDeleteString = " DELETE * FROM bdformulario003 ";

                OleDbCommand sqlDelete = new OleDbCommand();
                sqlDelete.CommandText = queryDeleteString;
                sqlDelete.Connection = database;
                sqlDelete.ExecuteNonQuery();

                string queryInsertString = "INSERT INTO bdformulario003 (FOR_ESTABLECIMIENTO, FOR_NOMBRES, FOR_APELLIDOS, FOR_SEXO, FOR_NUMERO_HOJA," +
                        " FOR_NUMERO_HISTORIA)" +
                        " VALUES ('" + reporte.ForEstablecimiento + "','" + reporte.ForNombres + "','" + reporte.ForApellidos + "', '" + reporte.ForSexo + "', 1, " +
                        " '" + reporte.ForNumeroHistoria + "')";
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


        public void ingresarFormEmergencia(ReporteForm008E hoja008)
        {
            try
            {
                string queryDeleteString = " DELETE * FROM Form008Emergencia ";
                OleDbCommand sqlDelete = new OleDbCommand();
                sqlDelete.CommandText = queryDeleteString;
                sqlDelete.Connection = database;
                sqlDelete.ExecuteNonQuery();
                string queryInsertString = "INSERT INTO Form008Emergencia (FOR_IS, FOR_UO, FOR_CODUO, FOR_PARR, FOR_CAT,FOR_PROV, " +
                    "FOR_HISTO,FOR_APEUNO,FOR_APEDOS,FOR_NOMUNO,FOR_NOMDOS,FOR_CEDULA,FOR_DIRECCION,FOR_BARRIO,FOR_PARROQUIA,FOR_CANTON,FOR_PROVINCIA, " +
                    "FOR_ZONAU,FOR_TELEFONO,FOR_FECHAN,FOR_LUGNAC,FOR_NACIONAL,FOR_GRUPCULT,FOR_EDADA,FOR_GM,FOR_GF,FOR_ECSOL,FOR_ECCAS,FOR_ECDIV, " +
                    "FOR_ECVIU,FOR_ECUL,FOR_INSTRUC,FOR_FECHADM,FOR_OCUPACION,FOR_EMPRESAT,FOR_SEGURO,FOR_REFERIDO,FOR_AVISARA,FOR_PARENTESCO,FOR_PARDIREC, " +
                    "FOR_PARTELEFONO,FOR_AMBULATORIA,FOR_AMBULANCIA,FOR_OTROTRANS,FOR_FINF,FOR_INSTPERSO,FOR_INSTTELEFONO,FOR_IAM_HORA,FOR_IAM_TRAUMA," +
                    "FOR_IAM_CCLINICA,FOR_IAM_COBSTET,FOR_IAM_CQUIR,FOR_IAM_NPOLICIA,FOR_IAM_OMOTIVO,FOR_IAM_OMOT,FOR_IAM_GSF,FOR_AVIE_FHEVEN,FOR_AVIE_LUGEV,FOR_AVIE_DEVEN," +
                    "FOR_AVIE_CPOLICIAL,FOR_AVIE_AT,FOR_AVIE_VAR,FOR_AVIE_IAL,FOR_AVIE_CAIDA,FOR_AVIE_VAARM,FOR_AVIE_IALI,FOR_AVIE_QUEM,FOR_AVIE_VRI,FOR_AVIE_INXD," +
                    "FOR_AVIE_MORDE,FOR_AVIE_VF,FOR_AVIE_IGASES,FOR_AVIE_AHOGA,FOR_AVIE_AF,FOR_AVIE_OINT,FOR_AVIE_CEXT,FOR_AVIE_APSIC,FOR_AVIE_ENVE,FOR_AVIE_APLAST," +
                    "FOR_AVIE_ASEXUAL,FOR_AVIE_PICAD,FOR_AVIE_OACC,FOR_AVIE_OVIOL,FOR_AVIE_ANAFLAX,FOR_AVIE_OBSERVA,FOR_AVIE_AETILICO,FOR_AVIE_VALCOH,FOR_APF_ALERG," +
                    "FOR_APF_CLIN,FOR_APF_GINEC,FOR_APF_TRAUM,FOR_APF_QUIR,FOR_APF_FARM,FOR_APF_PSQUI,FOR_APF_OTRO,FOR_APF_DESCIPCION,FOR_EARS_VAL,FOR_EARS_VAO," +
                    "FOR_EARS_CE,FOR_EARS_CI,FOR_EARS_DESCRIPCION,FOR_SVMV_PAUNO,FOR_SVMV_PADOS,FOR_SVMV_FC,FOR_SVMV_FR,FOR_SVMV_TB,FOR_SVMV_TA,FOR_SVMV_PESO," +
                    "FOR_SVMV_TALLA,FOR_SVMV_OCULAR,FOR_SVMV_VERBAL,FOR_SVMV_MOT,FOR_SVMV_TOT,FOR_SVMV_RPULD,FOR_SVMV_RPULI,FOR_SVMV_TCAP,FOR_SVMV_SOXI," +
                    "FOR_EFD_VAO,FOR_EFD_CABEZA,FOR_EFD_CUELLO,FOR_EFD_TORAX,FOR_EFD_ABD,FOR_EFD_COLUM,FOR_EFD_PELVIS,FOR_EFD_EXTREM,FOR_EFD_DESCRIPCION," +
                    "FOR_LL_HP,FOR_LL_HC,FOR_LL_FX,FOR_LL_FC,FOR_LL_CE,FOR_LL_H,FOR_LL_M,FOR_LL_P,FOR_LL_E,FOR_LL_DM,FOR_LL_HEM,FOR_LL_EI,FOR_LL_LE,FOR_LL_QUEM," +
                    "FOR_LL_OTROL,FOR_LL_OTROLQ,FOR_EO_GESTA,FOR_EO_PARTOS,FOR_EO_ABORTOS,FOR_EO_CESAREAS,FOR_EO_FUMESTRU,FOR_EO_SEMGEST,FOR_EO_MFETAL,FOR_EO_FCF," +
                    "FOR_EO_MSROTAS,FOR_EO_TIEMPO,FOR_EO_AU,FOR_EO_PRESEN,FOR_EO_DILAT,FOR_EO_BORRAM,FOR_EO_PLANO,FOR_EO_PELV,FOR_EO_SV,FOR_EO_CONTRAC,FOR_EO_DESCRIPCION," +
                    "FOR_SE_BIOM,FOR_SE_UR,FOR_SE_QUIM,FOR_SE_ELEC,FOR_SE_GAS,FOR_SE_EC,FOR_SE_END,FOR_SE_RXT,FOR_SE_RXA,FOR_SE_RXO,FOR_SE_TOM,FOR_SE_RES,FOR_SE_EP," +
                    "FOR_SE_EA,FOR_SE_INT,FOR_SE_OTROS,FOR_SE_DESC,FOR_DI_UNO,FOR_DI_UNOCIE,FOR_DI_UNOPRE,FOR_DI_UNODEF,FOR_DI_DOS,FOR_DI_DOSCIE,FOR_DI_DOSPRE," +
                    "FOR_DI_DOSDEF,FOR_DI_TRES,FOR_DI_TRESCIE,FOR_DI_TRESPRE,FOR_DI_TRESDEF,FOR_DA_UNO,FOR_DA_UNOCIE,FOR_DA_UNOPRE,FOR_DA_UNODEF,FOR_DA_DOS,FOR_DA_DOSCIE," +
                    "FOR_DA_DOSPRE,FOR_DA_DOSDEF,FOR_DA_TRES,FOR_DA_TRESCIE,FOR_DA_TRESPRE,FOR_DA_TRESDEF,FOR_PT_INDICAD,FOR_PT_MEDICAM,FOR_PT_POSOL,FOR_ALTA_DOM,FOR_ALTA_CE," +
                    "FOR_ALTA_OBS,FOR_ALTA_INT,FOR_ALTA_REF,FOR_ALTA_EGRE,FOR_ALTA_EE,FOR_ALTA_EI,FOR_ALTA_DI,FOR_ALTA_SR,FOR_ALTA_ESTEB,FOR_ALTA_ME,FOR_ALTA_CAUSA," +
                    "FOR_FECHA,FOR_HORA,FOR_NPROF,FOR_PROFCOD,USUARIO)" +
                    " VALUES ('" + hoja008.FOR_IS + "','" + hoja008.FOR_UO + "','" + hoja008.FOR_CODUO + "','" + hoja008.FOR_PARR + "','" + hoja008.FOR_CAT + "','" + hoja008.FOR_PROV + "','"
                    + hoja008.FOR_HISTO + "','" + hoja008.FOR_APEUNO + "','" + hoja008.FOR_APEDOS + "','" + hoja008.FOR_NOMUNO + "','" + hoja008.FOR_NOMDOS + "','" + hoja008.FOR_CEDULA + "','"
                    + hoja008.FOR_DIRECCION + "','" + hoja008.FOR_BARRIO + "','" + hoja008.FOR_PARROQUIA + "','" + hoja008.FOR_CANTON + "','" + hoja008.FOR_PROVINCIA + "','" + hoja008.FOR_ZONAU + "','"
                    + hoja008.FOR_TELEFONO + "','" + hoja008.FOR_FECHAN + "','" + hoja008.FOR_LUGNAC + "','" + hoja008.FOR_NACIONAL + "','" + hoja008.FOR_GRUPCULT + "','" + hoja008.FOR_EDADA + "','"
                    + hoja008.FOR_GM + "','" + hoja008.FOR_GF + "','" + hoja008.FOR_ECSOL + "','" + hoja008.FOR_ECCAS + "','" + hoja008.FOR_ECDIV + "','" + hoja008.FOR_ECVIU + "','" + hoja008.FOR_ECUL + "','"
                    + hoja008.FOR_INSTRUC + "','" + hoja008.FOR_FECHADM + "','" + hoja008.FOR_OCUPACION + "','" + hoja008.FOR_EMPRESAT + "','" + hoja008.FOR_SEGURO + "','" + hoja008.FOR_REFERIDO + "','"
                    + hoja008.FOR_AVISARA + "','" + hoja008.FOR_PARENTESCO + "','" + hoja008.FOR_PARDIREC + "','" + hoja008.FOR_PARTELEFONO + "','" + hoja008.FOR_AMBULATORIA + "','" + hoja008.FOR_AMBULANCIA + "','"
                    + hoja008.FOR_OTROTRANS + "','" + hoja008.FOR_FINF + "','" + hoja008.FOR_INSTPERSO + "','" + hoja008.FOR_INSTTELEFONO + "','" + hoja008.FOR_IAM_HORA + "','" + hoja008.FOR_IAM_TRAUMA + "','"
                    + hoja008.FOR_IAM_CCLINICA + "','" + hoja008.FOR_IAM_COBSTET + "','" + hoja008.FOR_IAM_CQUIR + "','" + hoja008.FOR_IAM_NPOLICIA + "','" + hoja008.FOR_IAM_OMOTIVO + "','" + hoja008.FOR_IAM_OMOT + "','" + hoja008.FOR_IAM_GSF + "','"
                    + hoja008.FOR_AVIE_FHEVEN + "','" + hoja008.FOR_AVIE_LUGEV + "','" + hoja008.FOR_AVIE_DEVEN + "','" + hoja008.FOR_AVIE_CPOLICIAL + "','" + hoja008.FOR_AVIE_AT + "','" + hoja008.FOR_AVIE_VAR + "','"
                    + hoja008.FOR_AVIE_IAL + "','" + hoja008.FOR_AVIE_CAIDA + "','" + hoja008.FOR_AVIE_VAARM + "','" + hoja008.FOR_AVIE_IALI + "','" + hoja008.FOR_AVIE_QUEM + "','" + hoja008.FOR_AVIE_VRI + "','"
                    + hoja008.FOR_AVIE_INXD + "','" + hoja008.FOR_AVIE_MORDE + "','" + hoja008.FOR_AVIE_VF + "','" + hoja008.FOR_AVIE_IGASES + "','" + hoja008.FOR_AVIE_AHOGA + "','" + hoja008.FOR_AVIE_AF + "','"
                    + hoja008.FOR_AVIE_OINT + "','" + hoja008.FOR_AVIE_CEXT + "','" + hoja008.FOR_AVIE_APSIC + "','" + hoja008.FOR_AVIE_ENVE + "','" + hoja008.FOR_AVIE_APLAST + "','" + hoja008.FOR_AVIE_ASEXUAL + "','"
                    + hoja008.FOR_AVIE_PICAD + "','" + hoja008.FOR_AVIE_OACC + "','" + hoja008.FOR_AVIE_OVIOL + "','" + hoja008.FOR_AVIE_ANAFLAX + "','" + hoja008.FOR_AVIE_OBSERVA + "','"
                    + hoja008.FOR_AVIE_AETILICO + "','" + hoja008.FOR_AVIE_VALCOH + "','" + hoja008.FOR_APF_ALERG + "','" + hoja008.FOR_APF_CLIN + "','" + hoja008.FOR_APF_GINEC + "','"
                    + hoja008.FOR_APF_TRAUM + "','" + hoja008.FOR_APF_QUIR + "','" + hoja008.FOR_APF_FARM + "','" + hoja008.FOR_APF_PSQUI + "','" + hoja008.FOR_APF_OTRO + "','"
                    + hoja008.FOR_APF_DESCIPCION + "','" + hoja008.FOR_EARS_VAL + "','" + hoja008.FOR_EARS_VAO + "','" + hoja008.FOR_EARS_CE + "','" + hoja008.FOR_EARS_CI + "','"
                    + hoja008.FOR_EARS_DESCRIPCION + "','" + hoja008.FOR_SVMV_PAUNO + "','" + hoja008.FOR_SVMV_PADOS + "','" + hoja008.FOR_SVMV_FC + "','" + hoja008.FOR_SVMV_FR + "','"
                    + hoja008.FOR_SVMV_TB + "','" + hoja008.FOR_SVMV_TA + "','" + hoja008.FOR_SVMV_PESO + "','" + hoja008.FOR_SVMV_TALLA + "','" + hoja008.FOR_SVMV_OCULAR + "','"
                    + hoja008.FOR_SVMV_VERBAL + "','" + hoja008.FOR_SVMV_MOT + "','" + hoja008.FOR_SVMV_TOT + "','" + hoja008.FOR_SVMV_RPULD + "','" + hoja008.FOR_SVMV_RPULI + "','"
                    + hoja008.FOR_SVMV_TCAP + "','" + hoja008.FOR_SVMV_SOXI + "','" + hoja008.FOR_EFD_VAO + "','" + hoja008.FOR_EFD_CABEZA + "','" + hoja008.FOR_EFD_CUELLO + "','"
                    + hoja008.FOR_EFD_TORAX + "','" + hoja008.FOR_EFD_ABD + "','" + hoja008.FOR_EFD_COLUM + "','" + hoja008.FOR_EFD_PELVIS + "','" + hoja008.FOR_EFD_EXTREM + "','"
                    + hoja008.FOR_EFD_DESCRIPCION + "','" + hoja008.FOR_LL_HP + "','" + hoja008.FOR_LL_HC + "','" + hoja008.FOR_LL_FX + "','" + hoja008.FOR_LL_FC + "','"
                    + hoja008.FOR_LL_CE + "','" + hoja008.FOR_LL_H + "','" + hoja008.FOR_LL_M + "','" + hoja008.FOR_LL_P + "','" + hoja008.FOR_LL_E + "','" + hoja008.FOR_LL_DM + "','"
                    + hoja008.FOR_LL_HEM + "','" + hoja008.FOR_LL_EI + "','" + hoja008.FOR_LL_LE + "','" + hoja008.FOR_LL_QUEM + "','" + hoja008.FOR_LL_OTROL + "','" + hoja008.FOR_LL_OTROLQ + "','"
                    + hoja008.FOR_EO_GESTA + "','" + hoja008.FOR_EO_PARTOS + "','" + hoja008.FOR_EO_ABORTOS + "','" + hoja008.FOR_EO_CESAREAS + "','" + hoja008.FOR_EO_FUMESTRU + "','"
                    + hoja008.FOR_EO_SEMGEST + "','" + hoja008.FOR_EO_MFETAL + "','" + hoja008.FOR_EO_FCF + "','" + hoja008.FOR_EO_MSROTAS + "','" + hoja008.FOR_EO_TIEMPO + "','" + hoja008.FOR_EO_AU + "','"
                    + hoja008.FOR_EO_PRESEN + "','" + hoja008.FOR_EO_DILAT + "','" + hoja008.FOR_EO_BORRAM + "','" + hoja008.FOR_EO_PLANO + "','" + hoja008.FOR_EO_PELV + "','" + hoja008.FOR_EO_SV + "','"
                    + hoja008.FOR_EO_CONTRAC + "','" + hoja008.FOR_EO_DESCRIPCION + "','" + hoja008.FOR_SE_BIOM + "','" + hoja008.FOR_SE_UR + "','" + hoja008.FOR_SE_QUIM + "','" + hoja008.FOR_SE_ELEC + "','"
                    + hoja008.FOR_SE_GAS + "','" + hoja008.FOR_SE_EC + "','" + hoja008.FOR_SE_END + "','" + hoja008.FOR_SE_RXT + "','" + hoja008.FOR_SE_RXA + "','" + hoja008.FOR_SE_RXO + "','" + hoja008.FOR_SE_TOM + "','"
                    + hoja008.FOR_SE_RES + "','" + hoja008.FOR_SE_EP + "','" + hoja008.FOR_SE_EA + "','" + hoja008.FOR_SE_INT + "','" + hoja008.FOR_SE_OTROS + "','" + hoja008.FOR_SE_DESC + "','"
                    + hoja008.FOR_DI_UNO + "','" + hoja008.FOR_DI_UNOCIE + "','" + hoja008.FOR_DI_UNOPRE + "','" + hoja008.FOR_DI_UNODEF + "','" + hoja008.FOR_DI_DOS + "','" + hoja008.FOR_DI_DOSCIE + "','"
                    + hoja008.FOR_DI_DOSPRE + "','" + hoja008.FOR_DI_DOSDEF + "','" + hoja008.FOR_DI_TRES + "','" + hoja008.FOR_DI_TRESCIE + "','" + hoja008.FOR_DI_TRESPRE + "','" + hoja008.FOR_DI_TRESDEF + "','"
                    + hoja008.FOR_DA_UNO + "','" + hoja008.FOR_DA_UNOCIE + "','" + hoja008.FOR_DA_UNOPRE + "','" + hoja008.FOR_DA_UNODEF + "','" + hoja008.FOR_DA_DOS + "','" + hoja008.FOR_DA_DOSCIE + "','"
                    + hoja008.FOR_DA_DOSPRE + "','" + hoja008.FOR_DA_DOSDEF + "','" + hoja008.FOR_DA_TRES + "','" + hoja008.FOR_DA_TRESCIE + "','" + hoja008.FOR_DA_TRESPRE + "','" + hoja008.FOR_DA_TRESDEF + "','"
                    + hoja008.FOR_PT_INDICAD + "','" + hoja008.FOR_PT_MEDICAM + "','" + hoja008.FOR_PT_POSOL + "','" + hoja008.FOR_ALTA_DOM + "','" + hoja008.FOR_ALTA_CE + "','" + hoja008.FOR_ALTA_OBS + "','"
                    + hoja008.FOR_ALTA_INT + "','" + hoja008.FOR_ALTA_REF + "','" + hoja008.FOR_ALTA_EGRE + "','" + hoja008.FOR_ALTA_EE + "','" + hoja008.FOR_ALTA_EI + "','" + hoja008.FOR_ALTA_DI + "','"
                    + hoja008.FOR_ALTA_SR + "','" + hoja008.FOR_ALTA_ESTEB + "','" + hoja008.FOR_ALTA_ME + "','" + hoja008.FOR_ALTA_CAUSA + "','" + hoja008.FOR_FECHA + "','" + hoja008.FOR_HORA + "','"
                    + hoja008.FOR_NPROF + "','" + hoja008.FOR_PROFCOD + "','" + hoja008.USUARIO + "')";
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



        public void ingresarCertificado(ReporteCertiticadoMedico reporte)
        {
            try
            {
                string queryDeleteString = " DELETE * FROM CertificidoMedico ";

                OleDbCommand sqlDelete = new OleDbCommand();
                sqlDelete.CommandText = queryDeleteString;
                sqlDelete.Connection = database;
                sqlDelete.ExecuteNonQuery();

                string queryInsertString = "INSERT INTO CertificidoMedico (CER_FECHA, " +
                                           "CER_NOMBRE, CER_PACIENTE, CER_EDAD, CER_IDENTIDAD, " +
                                           "CER_DIAGNOSTICO, CER_INGRESO, CER_EGRESO, CER_MEDICO_RESP, " +
                                           "CER_PROFESIONAL, CER_ESPECIALIDAD, CER_NOM_HOP)" +
                                           " VALUES ('" + reporte.CER_FECHA + "','" + reporte.CER_NOMBRE + "','"
                                           + reporte.CER_PACIENTE + "','" + reporte.CER_EDAD + "','"
                                           + reporte.CER_IDENTIDAD + "','" + reporte.CER_DIAGNOSTICO + "','"
                                           + reporte.CER_INGRESO + "','" + reporte.CER_EGRESO + "','"
                                           + reporte.CER_MEDICO_RESP + "','" + reporte.CER_PROFESIONAL + "','"
                                           + reporte.CER_ESPECIALIDAD + "','" + reporte.CER_NOM_HOP + "')";
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
