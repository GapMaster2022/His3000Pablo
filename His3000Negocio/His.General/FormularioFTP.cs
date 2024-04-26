using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.General
{
    public class FormularioFTP
    {
        MyExcel excel;

        public FormularioFTP()
        {
            excel = new MyExcel();
        }

        #region Form. 001 ADMISION Y ALTA EGRESO

        public void setFormularioAdmision(
            string directorioFormulario,
            string historiaclinica, 
            string apellidoPaterno, 
            string apellidoMaterno,
            string nombre1,
            string nombre2,
            string identificacion,
            string direccion,
            string codsector,
            string codparroquia,
            string codcanton,
            string codprovincia,
            string telefono1,
            string fechaNacimiento,
            string pais,
            string nacionalidad,
            string etnia,
            string edad,
            string sexo,
            string codEstadoCivil,
            string instruccion,
            string fechaIngreso,
            string ocupacion,
            string empresa
        )
        {
            try
            {
                if (excel.Open(directorioFormulario, 1))
                {
                    excel.ChooseSheet(3);
                    excel.WriteCell("A2", historiaclinica);
                    excel.WriteCell("B2", apellidoPaterno);
                    excel.WriteCell("C2", apellidoMaterno);
                    excel.WriteCell("D2", nombre1);
                    excel.WriteCell("E2", nombre2);
                    excel.WriteCell("F2", identificacion);
                    excel.WriteCell("G2", direccion);
                    excel.WriteCell("H2", codsector);
                    excel.WriteCell("I2", codparroquia);
                    excel.WriteCell("J2", codcanton);
                    excel.WriteCell("K2", codprovincia);
                    excel.WriteCell("L2", telefono1);
                    excel.WriteCell("M2", fechaNacimiento);
                    excel.WriteCell("N2", pais);
                    excel.WriteCell("O2", nacionalidad);
                    excel.WriteCell("P2", etnia);
                    excel.WriteCell("Q2", edad);
                    excel.WriteCell("R2", sexo);
                    excel.WriteCell("S2", codEstadoCivil);
                    excel.WriteCell("T2", instruccion);
                    excel.WriteCell("U2", fechaIngreso);
                    excel.WriteCell("V2", ocupacion);
                    excel.WriteCell("W2", empresa);

                    excel.Save();
                }
                excel.CloseWorkbook();
                excel.Close();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        #endregion

        #region Form. 002 CONSULTA EXTERNA

        public void setFormularioConsultaExterna(
            string directorioFormulario,
            string nombres,
            string apellidos,
            string sexo,
            string edad,
            string historiaclinica)
        {
            if (excel.Open(directorioFormulario, 1))
            {
                excel.ChooseSheet(3);
                excel.WriteCell("A2", nombres);
                excel.WriteCell("B2", apellidos);
                excel.WriteCell("C2", sexo);
                excel.WriteCell("D2", edad);
                excel.WriteCell("E2", historiaclinica);

                excel.Save();
            }
            excel.CloseWorkbook();
            excel.Close();
        }

        #endregion

        #region Form. 003 ANAMNESIS EXAMEN FISICO

        public void setFormularioAnamnesis(
            string directorioFormulario,
            string nombres,
            string apellidos,
            string sexo,
            string historiaclinica)
        {

            if (excel.Open(directorioFormulario, 1))
            {
                excel.ChooseSheet(3);
                excel.WriteCell("A2", nombres);
                excel.WriteCell("B2", apellidos);
                excel.WriteCell("C2", sexo);
                excel.WriteCell("D2", historiaclinica);

                excel.Save();
            }
            excel.CloseWorkbook();
            excel.Close();
        }

        #endregion

        #region Form. 005 EVOLUCION Y PRESCRIPCIONES

        public void setFormularioEvolucion(
            string directorioFormulario,
            string nombres,
            string apellidos,
            string sexo,
            string historiaclinica)
        {

            if (excel.Open(directorioFormulario, 1))
            {
                excel.ChooseSheet(3);
                excel.WriteCell("A2", nombres);
                excel.WriteCell("B2", apellidos);
                excel.WriteCell("C2", sexo);
                excel.WriteCell("D2", historiaclinica);

                excel.Save();
            }
            excel.CloseWorkbook();
            excel.Close();
        }

        #endregion

        #region Form. 006 EPICRISIS

        public void setFormularioEpicrisis(
            string directorioFormulario,
            string nombres,
            string apellidos,
            string sexo,
            string historiaclinica)
        {

            if (excel.Open(directorioFormulario, 1))
            {
                excel.ChooseSheet(3);
                excel.WriteCell("A2", nombres);
                excel.WriteCell("B2", apellidos);
                excel.WriteCell("C2", sexo);
                excel.WriteCell("D2", historiaclinica);

                excel.Save();
            }
            excel.CloseWorkbook();
            excel.Close();
        }

        #endregion

        #region Form. 007 INTERCONSULTA

        public void setFormularioInterconsulta(
            string directorioFormulario,
            string nombres,
            string apellidos,
            string sexo,
            string edad,
            string historiaclinica)
        {
            if (excel.Open(directorioFormulario, 1))
            {
                excel.ChooseSheet(3);
                excel.WriteCell("A2", nombres);
                excel.WriteCell("B2", apellidos);
                excel.WriteCell("C2", sexo);
                excel.WriteCell("D2", edad);
                excel.WriteCell("E2", historiaclinica);

                excel.Save();
            }
            excel.CloseWorkbook();
            excel.Close();
        }

        #endregion

        #region Form. 008 EMERGENCIA

        public void setFormularioEmergencia(
            string directorioFormulario,
            string historiaclinica,
            string apellidoPaterno,
            string apellidoMaterno,
            string nombre1,
            string nombre2,
            string identificacion,
            string direccion,
            string codsector,
            string codparroquia,
            string codcanton,
            string codprovincia,
            string telefono1,
            string fechaNacimiento,
            string pais,
            string nacionalidad,
            string etnia,
            string edad,
            string sexo,
            string codEstadoCivil,
            string instruccion,
            string fechaIngreso,
            string ocupacion,
            string empresa
        )
        {
            if (excel.Open(directorioFormulario, 1))
            {
                excel.ChooseSheet(3);
                excel.WriteCell("A2", historiaclinica);
                excel.WriteCell("B2", apellidoPaterno);
                excel.WriteCell("C2", apellidoMaterno);
                excel.WriteCell("D2", nombre1);
                excel.WriteCell("E2", nombre2);
                excel.WriteCell("F2", identificacion);
                excel.WriteCell("G2", direccion);
                excel.WriteCell("H2", codsector);
                excel.WriteCell("I2", codparroquia);
                excel.WriteCell("J2", codcanton);
                excel.WriteCell("K2", codprovincia);
                excel.WriteCell("L2", telefono1);
                excel.WriteCell("M2", fechaNacimiento);
                excel.WriteCell("N2", pais);
                excel.WriteCell("O2", nacionalidad);
                excel.WriteCell("P2", etnia);
                excel.WriteCell("Q2", edad);
                excel.WriteCell("R2", sexo);
                excel.WriteCell("S2", codEstadoCivil);
                excel.WriteCell("T2", instruccion);
                excel.WriteCell("U2", fechaIngreso);
                excel.WriteCell("V2", ocupacion);
                excel.WriteCell("W2", empresa);

                excel.Save();
            }
            excel.CloseWorkbook();
            excel.Close();
        }

        #endregion

        #region Form. 010 LABORATORIO CLINICO

        public void setFormularioLaboratorio(
            string directorioFormulario,
            string historiaclinica,
            string apellidoPaterno,
            string apellidoMaterno,
            string nombre1,
            string nombre2,
            string identificacion,
            string edad
        )
        {
            if (excel.Open(directorioFormulario, 1))
            {
                excel.ChooseSheet(3);
                excel.WriteCell("A2", historiaclinica);
                excel.WriteCell("B2", apellidoPaterno);
                excel.WriteCell("C2", apellidoMaterno);
                excel.WriteCell("D2", nombre1);
                excel.WriteCell("E2", nombre2);
                excel.WriteCell("F2", edad);
                excel.WriteCell("G2", identificacion);

                excel.Save();
            }
            excel.CloseWorkbook();
            excel.Close();
        }

        #endregion

        #region Form. 012 IMAGENOLOGIA

        public void setFormularioImagenologia(
            string directorioFormulario,
            string historiaclinica,
            string apellidoPaterno,
            string apellidoMaterno,
            string nombre1,
            string nombre2,
            string identificacion,
            string edad
        )
        {
            if (excel.Open(directorioFormulario, 1))
            {
                excel.ChooseSheet(3);
                excel.WriteCell("A2", historiaclinica);
                excel.WriteCell("B2", apellidoPaterno);
                excel.WriteCell("C2", apellidoMaterno);
                excel.WriteCell("D2", nombre1);
                excel.WriteCell("E2", nombre2);
                excel.WriteCell("F2", edad);
                excel.WriteCell("G2", identificacion);

                excel.Save();
            }
            excel.CloseWorkbook();
            excel.Close();
        }

        #endregion

        #region Form. 013 HISPATOLOGIA

        public void setFormularioHispatologia(
            string directorioFormulario,
            string historiaclinica,
            string apellidoPaterno,
            string apellidoMaterno,
            string nombre1,
            string nombre2,
            string identificacion,
            string edad
        )
        {
            if (excel.Open(directorioFormulario, 1))
            {
                excel.ChooseSheet(3);
                excel.WriteCell("A2", historiaclinica);
                excel.WriteCell("B2", apellidoPaterno);
                excel.WriteCell("C2", apellidoMaterno);
                excel.WriteCell("D2", nombre1);
                excel.WriteCell("E2", nombre2);
                excel.WriteCell("F2", edad);
                excel.WriteCell("G2", identificacion);

                excel.Save();
            }
            excel.CloseWorkbook();
            excel.Close();
        }

        #endregion

        #region Form. 020 SIGNOS VITALES

        public void setFormularioSignosVitales(
            string directorioFormulario,
            string historiaclinica,
            string nombres,
            string apellidos,
            string sexo
        )
        {
            if (excel.Open(directorioFormulario, 1))
            {
                excel.ChooseSheet(3);
                excel.WriteCell("A2", nombres);
                excel.WriteCell("B2", apellidos);
                excel.WriteCell("C2", sexo);
                excel.WriteCell("D2", historiaclinica);

                excel.Save();
            }
            excel.CloseWorkbook();
            excel.Close();
        }

        #endregion

        #region Form. 022 ADMINISTRACION DE MEDICAMENTOS

        public void setFormularioAdministracionMedicamentos(
            string directorioFormulario,
            string historiaclinica,
            string nombres,
            string apellidos,
            string sexo
        )
        {
            if (excel.Open(directorioFormulario, 1))
            {
                excel.ChooseSheet(3);
                excel.WriteCell("A2", nombres);
                excel.WriteCell("B2", apellidos);
                excel.WriteCell("C2", sexo);
                excel.WriteCell("D2", historiaclinica);

                excel.Save();
            }
            excel.CloseWorkbook();
            excel.Close();
        }

        #endregion

        #region Form. 024 AUTORIZACIONES Y CONSENTIMIENTO INFORMADO

        public void setFormularioAutorizaciones(
            string directorioFormulario,
            string historiaclinica,
            string apellidoPaterno,
            string apellidoMaterno,
            string nombres
        )
        {
            if (excel.Open(directorioFormulario, 1))
            {
                excel.ChooseSheet(3);
                excel.WriteCell("A2", historiaclinica);
                excel.WriteCell("B2", apellidoPaterno);
                excel.WriteCell("C2", apellidoMaterno);
                excel.WriteCell("D2", nombres);

                excel.Save();
            }
            excel.CloseWorkbook();
            excel.Close();
        }

        #endregion

        #region Form. 033 ODONTOLOGIA

        public void setFormularioOdontologia(
            string directorioFormulario,
            string nombres,
            string apellidos,
            string sexo,
            string edad,
            string historiaclinica
        )
        {
            if (excel.Open(directorioFormulario, 1))
            {
                excel.ChooseSheet(3);
                excel.WriteCell("A2", nombres);
                excel.WriteCell("B2", apellidos);
                excel.WriteCell("C2", sexo);
                excel.WriteCell("D2", edad);
                excel.WriteCell("E2", historiaclinica);

                excel.Save();
            }
            excel.CloseWorkbook();
            excel.Close();
        }

        #endregion

        #region Form. 038 TRABAJO SOCIAL

        public void setFormularioTrabajoSocial(
            string directorioFormulario,
            string historiaclinica,
            string apellidoPaterno,
            string apellidoMaterno,
            string nombre1,
            string nombre2,
            string identificacion,
            string direccion,
            string barrio,
            string parroquia,
            string canton,
            string provincia,
            string telefono,
            string representante,
            string parentescoRep,
            string direccionRep,
            string telefonoRep
        )
        {
            if (excel.Open(directorioFormulario, 1))
            {
                excel.ChooseSheet(3);
                excel.WriteCell("A2", historiaclinica);
                excel.WriteCell("B2", apellidoPaterno);
                excel.WriteCell("C2", apellidoMaterno);
                excel.WriteCell("D2", nombre1);
                excel.WriteCell("E2", nombre2);
                excel.WriteCell("F2", identificacion);
                excel.WriteCell("G2", direccion);
                excel.WriteCell("H2", barrio);
                excel.WriteCell("I2", parroquia);
                excel.WriteCell("J2", canton);
                excel.WriteCell("K2", provincia);
                excel.WriteCell("L2", telefono);
                excel.WriteCell("M2", representante);
                excel.WriteCell("N2", parentescoRep);
                excel.WriteCell("O2", direccionRep);
                excel.WriteCell("P2",telefonoRep);
                excel.Save();
            }
            excel.CloseWorkbook();
            excel.Close();
        }

        #endregion

        #region Form. 053 REFERENCIA

        public void setFormularioReferencia(
            string directorioFormulario,
            string historiaclinica,
            string apellidoPaterno,
            string apellidoMaterno,
            string nombre1,
            string nombre2,
            string identificacion,
            string edad,
            string sexo,
            string estadocivil,
            string instruccion,
            string empresa,
            string telefonoSeguro
        )
        {
            if (excel.Open(directorioFormulario, 1))
            {
                excel.ChooseSheet(3);
                excel.WriteCell("A2", historiaclinica);
                excel.WriteCell("B2", apellidoPaterno);
                excel.WriteCell("C2", apellidoMaterno);
                excel.WriteCell("D2", nombre1);
                excel.WriteCell("E2", nombre2);
                excel.WriteCell("F2", identificacion);
                excel.WriteCell("G2", edad);
                excel.WriteCell("H2", sexo);
                excel.WriteCell("I2", estadocivil);
                excel.WriteCell("J2", instruccion);
                excel.WriteCell("K2", empresa);
                excel.WriteCell("L2", telefonoSeguro);
                excel.Save();
            }
            excel.CloseWorkbook();
            excel.Close();
        }

        #endregion

        #region Form. 054 CONCENTRADO DE LABORATORIO

        public void setFormularioConcentradoLab(
            string directorioFormulario,
            string nombres,
            string apellidos,
            string sexo,
            string historiaclinica
        )
        {
            if (excel.Open(directorioFormulario, 1))
            {
                excel.ChooseSheet(3);
                excel.WriteCell("A2", nombres);
                excel.WriteCell("B2", apellidos);
                excel.WriteCell("C2", sexo);
                excel.WriteCell("D2", historiaclinica);

                excel.Save();
            }
            excel.CloseWorkbook();
            excel.Close();
        }

        #endregion

        #region Form. 055 CONCENTRADOP DE EXAMENES ESPECIALES

        public void setFormularioConcentradoExamEspeciales(
            string directorioFormulario,
            string nombres,
            string apellidos,
            string sexo,
            string historiaclinica
        )
        {
            if (excel.Open(directorioFormulario, 1))
            {
                excel.ChooseSheet(3);
                excel.WriteCell("A2", nombres);
                excel.WriteCell("B2", apellidos);
                excel.WriteCell("C2", sexo);
                excel.WriteCell("D2", historiaclinica);

                excel.Save();
            }
            excel.CloseWorkbook();
            excel.Close();
        }

        #endregion

    }
}
