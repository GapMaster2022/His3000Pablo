using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Core.Datos;
using His.Entidades;
using His.Entidades.Clases;
using System.Data.Common;

namespace His.Datos
{
    public class DatConsultaExterna
    {
        public DataTable RecuperaPaciente(Int64 Ate_Codigo)
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

            Sqlcmd = new SqlCommand("sp_RecuperaPacienteConsultaExterna", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@Ate_Codigo"].Value = (Ate_Codigo);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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


        public DataTable PacienteExiste(Int64 Ate_Codigo)
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

            Sqlcmd = new SqlCommand("select Historia as Hc, fecha as Fecha, Apellido + ' ' + Nombre as Paciente, Edad, Sexo, ID_FORM002,MED_CODIGO from Form002MSP where AteCodigo = " + Ate_Codigo, Sqlcon);

            Sqlcmd.CommandType = CommandType.Text;
            SqlDataReader reader = Sqlcmd.ExecuteReader();
            Sqlcmd.CommandTimeout = 180;
            Dts.Load(reader);
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

        public DataTable PacientesConsultaExterna(Int64 Ate_Codigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
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

            Sqlcmd = new SqlCommand("select Historia as Hc, Apellido + ' ' + Nombre as Paciente, Edad, Sexo from Form002MSP where AteCodigo = " + Ate_Codigo + " group by Historia, Apellido + ' ' + Nombre, Edad, Sexo", Sqlcon);

            Sqlcmd.CommandType = CommandType.Text;
            SqlDataReader reader = Sqlcmd.ExecuteReader();
            Sqlcmd.CommandTimeout = 180;
            Dts.Load(reader);
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
        public Form002MSP PacienteConsultaExterna(int id)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return db.Form002MSP.FirstOrDefault(x => x.ID_FORM002 == id);
            }
            #region Codigo Anterior al modelo Mario 20/10/2023
            //SqlConnection Sqlcon;
            //SqlCommand Sqlcmd;
            //DataTable Dts = new DataTable();
            //BaseContextoDatos obj = new BaseContextoDatos();
            //Sqlcon = obj.ConectarBd();
            //try
            //{
            //    Sqlcon.Open();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            //Sqlcmd = new SqlCommand("select * from Form002MSP where ID_FORM002 = " + id, Sqlcon);

            //Sqlcmd.CommandType = CommandType.Text;
            //SqlDataReader reader = Sqlcmd.ExecuteReader();
            //Sqlcmd.CommandTimeout = 180;
            //Dts.Load(reader);
            //try
            //{
            //    Sqlcon.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //return Dts;
            #endregion
        }

        public Int64 RecuperarId()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            BaseContextoDatos obj = new BaseContextoDatos();
            Int64 id_form002 = 0;
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("select max(ID_FORM002) as codigo from Form002MSP", Sqlcon);

            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.CommandTimeout = 180;
            SqlDataReader reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                id_form002 = Convert.ToInt64(reader["codigo"].ToString());
            }
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return id_form002;
        }
        public void GuardarPrescripciones(Int64 id_form002, int id_usuario, string usuario, string indicacion, string farmaco, DateTime fecha_admin, bool administrado)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
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

            Sqlcmd = new SqlCommand("INSERT INTO PRESCRIPCIONES_CONSULTA_EXTERNA VALUES(@id_usuario, @usuario, @indicacion, @farmacos, @fecha_admin, @administrado, @id_form002)", Sqlcon);

            Sqlcmd.CommandType = CommandType.Text;

            Sqlcmd.Parameters.AddWithValue("@id_usuario", id_usuario);
            Sqlcmd.Parameters.AddWithValue("@usuario", usuario);
            Sqlcmd.Parameters.AddWithValue("@indicacion", indicacion);
            Sqlcmd.Parameters.AddWithValue("@farmacos", farmaco);
            Sqlcmd.Parameters.AddWithValue("@fecha_admin", fecha_admin);
            Sqlcmd.Parameters.AddWithValue("@administrado", administrado);
            Sqlcmd.Parameters.AddWithValue("@id_form002", id_form002);
            Sqlcmd.CommandTimeout = 180;
            Sqlcmd.ExecuteNonQuery();
            Sqlcmd.Parameters.Clear();
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public DataTable RecuperaEspecialidades()
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

            Sqlcmd = new SqlCommand("sp_EspecialidadesMedicas", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        public DataTable RecuperaConsultorios()
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

            Sqlcmd = new SqlCommand("sp_RecuperaConsultorios", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        public DataTable RecuperaHorario(string Tiempo, DateTime fechaCita, string consultorio)
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

            Sqlcmd = new SqlCommand("sp_RecuperaHorario", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Tiempo", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Tiempo"].Value = (Tiempo);

            Sqlcmd.Parameters.Add("@fechaCita", SqlDbType.Date);
            Sqlcmd.Parameters["@fechaCita"].Value = (fechaCita);

            Sqlcmd.Parameters.Add("@consultorio", SqlDbType.VarChar);
            Sqlcmd.Parameters["@consultorio"].Value = (consultorio);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        public DataTable BuscaPaciente(string cedula)
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

            Sqlcmd = new SqlCommand("sp_BuscaPacienteConsultaExterna", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@cedula", SqlDbType.VarChar);
            Sqlcmd.Parameters["@cedula"].Value = (cedula);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        public bool GuardaAgendamientoPaciente(string txtIdentificacion, string txtNombres, string txtApellidos, string txtEmail, string txtTelefono,
            string txtCelular, string txtDireccion, DateTime dtpFechaCita, string cmbEspecialidades, string lblMedico,
            string lblMailMed, string cmbConsultorios, string cmbHora, string txtMotivo, string txtNotas, string medicoCelular, Int64 med_codigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            SqlTransaction transaction;
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch
            {
                return false;
            }

            transaction = Sqlcon.BeginTransaction();

            try
            {

                Sqlcmd = new SqlCommand("sp_GrabaPacienteConsultaExterna", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@txtIdentificacion", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtIdentificacion"].Value = (txtIdentificacion);

                Sqlcmd.Parameters.Add("@txtNombres", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtNombres"].Value = (txtNombres);

                Sqlcmd.Parameters.Add("@txtApellidos", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtApellidos"].Value = (txtApellidos);

                Sqlcmd.Parameters.Add("@txtEmail", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtEmail"].Value = (txtEmail);

                Sqlcmd.Parameters.Add("@txtTelefono", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtTelefono"].Value = (txtTelefono);

                Sqlcmd.Parameters.Add("@txtCelular", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtCelular"].Value = (txtCelular);

                Sqlcmd.Parameters.Add("@txtDireccion", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtDireccion"].Value = (txtDireccion);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts);

                //GUARDA AGENDAMIENTO SIEMPRE QUE SE GUARDE DATOS DEL PACIENTE

                Sqlcmd = new SqlCommand("sp_GrabaAgendaConsultaExterna", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@dtpFechaCita", SqlDbType.Date);
                Sqlcmd.Parameters["@dtpFechaCita"].Value = (dtpFechaCita);

                Sqlcmd.Parameters.Add("@cmbEspecialidades", SqlDbType.VarChar);
                Sqlcmd.Parameters["@cmbEspecialidades"].Value = (cmbEspecialidades);

                Sqlcmd.Parameters.Add("@lblMedico", SqlDbType.VarChar);
                Sqlcmd.Parameters["@lblMedico"].Value = (lblMedico);

                Sqlcmd.Parameters.Add("@lblMailMed", SqlDbType.VarChar);
                Sqlcmd.Parameters["@lblMailMed"].Value = (lblMailMed);

                Sqlcmd.Parameters.Add("@cmbConsultorios", SqlDbType.VarChar);
                Sqlcmd.Parameters["@cmbConsultorios"].Value = (cmbConsultorios);

                Sqlcmd.Parameters.Add("@cmbHora", SqlDbType.VarChar);
                Sqlcmd.Parameters["@cmbHora"].Value = (cmbHora);

                Sqlcmd.Parameters.Add("@txtMotivo", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtMotivo"].Value = (txtMotivo);

                Sqlcmd.Parameters.Add("@txtNotas", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtNotas"].Value = (txtNotas);

                Sqlcmd.Parameters.Add("@txtIdentificacion", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtIdentificacion"].Value = (txtIdentificacion);

                Sqlcmd.Parameters.Add("@medicoCelular", SqlDbType.VarChar);
                Sqlcmd.Parameters["@medicoCelular"].Value = (medicoCelular);

                Sqlcmd.Parameters.Add("@med_codigo", SqlDbType.BigInt);
                Sqlcmd.Parameters["@med_codigo"].Value = (med_codigo);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts);
                transaction.Commit();
                try
                {
                    Sqlcon.Close();
                }
                catch
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false; // los datos no se almacenaron
            }
        }

        public DataTable RecuperaNumAgenda()
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

            Sqlcmd = new SqlCommand("sp_RecuperaNumAgenda", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        public bool GuardaTriajeSignosVitales(string lblHistoria, Int64 lblAtencion, int nourgente, int urgente, int critico, int muerto, int alcohol, int drogas, int otros, string txtOtrasActual, string txtObserEnfer, decimal txt_PresionA1, decimal txt_PresionA2, decimal txt_FCardiaca, decimal txt_FResp, decimal txt_TBucal, decimal txt_TAxilar, decimal txt_SaturaO, decimal txt_PesoKG, decimal txt_Talla, decimal txtIMCorporal, decimal txt_PerimetroC, decimal txt_Glicemia, decimal txt_TotalG, int cmb_Motora, int cmb_Verbal, int cmb_Ocular, int txt_DiamPDV, string cmb_ReacPDValor, int txt_DiamPIV, string cmb_ReacPIValor, int txt_Gesta, int txt_Partos, int txt_Abortos, int txt_Cesareas, DateTime dtp_ultimaMenst1, decimal txt_SemanaG, int movFetal, int txt_FrecCF, int memRotas, string txt_Tiempo, int txt_AltU, int txt_Presentacion, int txt_Dilatacion, int txt_Borramiento, string txt_Plano, int pelvis, int sangrado, string txt_Contracciones, int urgente2, decimal perimetro, decimal hemoglobina, decimal glucosa, Int32 id_usuario)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            SqlTransaction transaction;
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch
            {
                return false;
            }

            transaction = Sqlcon.BeginTransaction();

            try
            {

                Sqlcmd = new SqlCommand("sp_GrabaTriajeConsultaExterna", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@lblHistoria", SqlDbType.VarChar);
                Sqlcmd.Parameters["@lblHistoria"].Value = (lblHistoria);

                Sqlcmd.Parameters.Add("@lblAtencion", SqlDbType.BigInt);
                Sqlcmd.Parameters["@lblAtencion"].Value = (lblAtencion);

                Sqlcmd.Parameters.Add("@nourgente", SqlDbType.Int);
                Sqlcmd.Parameters["@nourgente"].Value = (nourgente);

                Sqlcmd.Parameters.Add("@urgente", SqlDbType.Int);
                Sqlcmd.Parameters["@urgente"].Value = (urgente);

                Sqlcmd.Parameters.Add("@critico", SqlDbType.Int);
                Sqlcmd.Parameters["@critico"].Value = (critico);

                Sqlcmd.Parameters.Add("@muerto", SqlDbType.Int);
                Sqlcmd.Parameters["@muerto"].Value = (muerto);

                Sqlcmd.Parameters.Add("@alcohol", SqlDbType.Int);
                Sqlcmd.Parameters["@alcohol"].Value = (alcohol);

                Sqlcmd.Parameters.Add("@drogas", SqlDbType.Int);
                Sqlcmd.Parameters["@drogas"].Value = (drogas);

                Sqlcmd.Parameters.Add("@otros", SqlDbType.Int);
                Sqlcmd.Parameters["@otros"].Value = (otros);

                Sqlcmd.Parameters.Add("@txtOtrasActual", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtOtrasActual"].Value = (txtOtrasActual);

                Sqlcmd.Parameters.Add("@txtObserEnfer", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtObserEnfer"].Value = (txtObserEnfer);

                Sqlcmd.Parameters.Add("@urgente2", SqlDbType.Int);
                Sqlcmd.Parameters["@urgente2"].Value = (urgente2);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts);

                //GUARDA SIGNOS VITALES SIEMPRE QUE SE GUARDE TRIAJE DEL PACIENTE
                #region graba signos antiguo
                //Sqlcmd = new SqlCommand("sp_GrabaSignosVitalesConsultaExterna", Sqlcon); // se comenta pára que grabe de la nueva forma // Mario Valencia 14-02-2024
                //Sqlcmd.CommandType = CommandType.StoredProcedure;
                //Sqlcmd.Transaction = transaction;

                //Sqlcmd.Parameters.Add("@lblHistoria", SqlDbType.VarChar);
                //Sqlcmd.Parameters["@lblHistoria"].Value = (lblHistoria);

                //Sqlcmd.Parameters.Add("@lblAtencion", SqlDbType.BigInt);
                //Sqlcmd.Parameters["@lblAtencion"].Value = (lblAtencion);

                //Sqlcmd.Parameters.Add("@txt_PresionA1", SqlDbType.Decimal);
                //Sqlcmd.Parameters["@txt_PresionA1"].Value = (txt_PresionA1);

                //Sqlcmd.Parameters.Add("@txt_PresionA2", SqlDbType.Decimal);
                //Sqlcmd.Parameters["@txt_PresionA2"].Value = (txt_PresionA2);

                //Sqlcmd.Parameters.Add("@txt_FCardiaca", SqlDbType.Decimal);
                //Sqlcmd.Parameters["@txt_FCardiaca"].Value = (txt_FCardiaca);

                //Sqlcmd.Parameters.Add("@txt_FResp", SqlDbType.Decimal);
                //Sqlcmd.Parameters["@txt_FResp"].Value = (txt_FResp);

                //Sqlcmd.Parameters.Add("@txt_TBucal", SqlDbType.Decimal);
                //Sqlcmd.Parameters["@txt_TBucal"].Value = (txt_TBucal);

                //Sqlcmd.Parameters.Add("@txt_TAxilar", SqlDbType.Decimal);
                //Sqlcmd.Parameters["@txt_TAxilar"].Value = (txt_TAxilar);

                //Sqlcmd.Parameters.Add("@txt_SaturaO", SqlDbType.Decimal);
                //Sqlcmd.Parameters["@txt_SaturaO"].Value = (txt_SaturaO);

                //Sqlcmd.Parameters.Add("@txt_PesoKG", SqlDbType.Decimal);
                //Sqlcmd.Parameters["@txt_PesoKG"].Value = (txt_PesoKG);

                //Sqlcmd.Parameters.Add("@txt_Talla", SqlDbType.Decimal);
                //Sqlcmd.Parameters["@txt_Talla"].Value = (txt_Talla);

                //Sqlcmd.Parameters.Add("@txtIMCorporal", SqlDbType.Decimal);
                //Sqlcmd.Parameters["@txtIMCorporal"].Value = (txtIMCorporal);

                //Sqlcmd.Parameters.Add("@txt_PerimetroC", SqlDbType.Decimal);
                //Sqlcmd.Parameters["@txt_PerimetroC"].Value = (txt_PerimetroC);

                //Sqlcmd.Parameters.Add("@txt_Glicemia", SqlDbType.Decimal);
                //Sqlcmd.Parameters["@txt_Glicemia"].Value = (txt_Glicemia);

                //Sqlcmd.Parameters.Add("@txt_TotalG", SqlDbType.Decimal);
                //Sqlcmd.Parameters["@txt_TotalG"].Value = (txt_TotalG);

                //Sqlcmd.Parameters.Add("@cmb_Ocular", SqlDbType.Int);
                //Sqlcmd.Parameters["@cmb_Ocular"].Value = (cmb_Ocular);

                //Sqlcmd.Parameters.Add("@cmb_Verbal", SqlDbType.Int);
                //Sqlcmd.Parameters["@cmb_Verbal"].Value = (cmb_Verbal);

                //Sqlcmd.Parameters.Add("@cmb_Motora", SqlDbType.Int);
                //Sqlcmd.Parameters["@cmb_Motora"].Value = (cmb_Motora);

                //Sqlcmd.Parameters.Add("@txt_DiamPDV", SqlDbType.Int);
                //Sqlcmd.Parameters["@txt_DiamPDV"].Value = (txt_DiamPDV);

                //Sqlcmd.Parameters.Add("@cmb_ReacPDValor", SqlDbType.VarChar);
                //Sqlcmd.Parameters["@cmb_ReacPDValor"].Value = (cmb_ReacPDValor);

                //Sqlcmd.Parameters.Add("@txt_DiamPIV", SqlDbType.Int);
                //Sqlcmd.Parameters["@txt_DiamPIV"].Value = (txt_DiamPIV);

                //Sqlcmd.Parameters.Add("@cmb_ReacPIValor", SqlDbType.VarChar);
                //Sqlcmd.Parameters["@cmb_ReacPIValor"].Value = (cmb_ReacPIValor);

                ///// se aumenta para formularios 2021 //////01/02/2024       Cristian Ruiz

                //Sqlcmd.Parameters.Add("@perimetro", SqlDbType.Decimal);
                //Sqlcmd.Parameters["@perimetro"].Value = (perimetro);

                //Sqlcmd.Parameters.Add("@hemoglobina", SqlDbType.Decimal);
                //Sqlcmd.Parameters["@hemoglobina"].Value = (hemoglobina);

                //Sqlcmd.Parameters.Add("@glucosa", SqlDbType.Decimal);
                //Sqlcmd.Parameters["@glucosa"].Value = (glucosa);

                //Sqlcmd.Parameters.Add("@pulsioximetria", SqlDbType.Decimal);
                //Sqlcmd.Parameters["@pulsioximetria"].Value = (pulsioximetria);

                //Sqlcmd.Parameters.Add("@id_usuario", SqlDbType.Int);
                //Sqlcmd.Parameters["@id_usuario"].Value = (id_usuario);


                //Sqldap = new SqlDataAdapter();
                //Sqlcmd.CommandTimeout = 180;
                //Sqldap.SelectCommand = Sqlcmd;
                //Dts = new DataSet();
                //Sqldap.Fill(Dts);
                #endregion
                //GUARDA OBSTETRICA SI SE GUARDA LO ANTERIOR

                Sqlcmd = new SqlCommand("sp_GrabaObstetricaConsultaExterna", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@lblHistoria", SqlDbType.VarChar);
                Sqlcmd.Parameters["@lblHistoria"].Value = (lblHistoria);

                Sqlcmd.Parameters.Add("@lblAtencion", SqlDbType.BigInt);
                Sqlcmd.Parameters["@lblAtencion"].Value = (lblAtencion);

                Sqlcmd.Parameters.Add("@txt_Gesta", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Gesta"].Value = (txt_Gesta);

                Sqlcmd.Parameters.Add("@txt_Partos", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Partos"].Value = (txt_Partos);

                Sqlcmd.Parameters.Add("@txt_Abortos", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Abortos"].Value = (txt_Abortos);

                Sqlcmd.Parameters.Add("@txt_Cesareas", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Cesareas"].Value = (txt_Cesareas);

                Sqlcmd.Parameters.Add("@dtp_ultimaMenst1", SqlDbType.Date);
                Sqlcmd.Parameters["@dtp_ultimaMenst1"].Value = (dtp_ultimaMenst1);

                Sqlcmd.Parameters.Add("@txt_SemanaG", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_SemanaG"].Value = (txt_SemanaG);

                Sqlcmd.Parameters.Add("@movFetal", SqlDbType.Int);
                Sqlcmd.Parameters["@movFetal"].Value = (movFetal);

                Sqlcmd.Parameters.Add("@txt_FrecCF", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_FrecCF"].Value = (txt_FrecCF);

                Sqlcmd.Parameters.Add("@memRotas", SqlDbType.Int);
                Sqlcmd.Parameters["@memRotas"].Value = (memRotas);

                Sqlcmd.Parameters.Add("@txt_Tiempo", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txt_Tiempo"].Value = (txt_Tiempo);

                Sqlcmd.Parameters.Add("@txt_AltU", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_AltU"].Value = (txt_AltU);

                Sqlcmd.Parameters.Add("@txt_Presentacion", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Presentacion"].Value = (txt_Presentacion);

                Sqlcmd.Parameters.Add("@txt_Dilatacion", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Dilatacion"].Value = (txt_Dilatacion);

                Sqlcmd.Parameters.Add("@txt_Borramiento", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Borramiento"].Value = (txt_Borramiento);

                Sqlcmd.Parameters.Add("@txt_Plano", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txt_Plano"].Value = (txt_Plano);

                Sqlcmd.Parameters.Add("@pelvis", SqlDbType.Int);
                Sqlcmd.Parameters["@pelvis"].Value = (pelvis);

                Sqlcmd.Parameters.Add("@sangrado", SqlDbType.Int);
                Sqlcmd.Parameters["@sangrado"].Value = (sangrado);

                Sqlcmd.Parameters.Add("@txt_Contracciones", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txt_Contracciones"].Value = (txt_Contracciones);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts);

                using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    ConexionEntidades.ConexionEDM.Open();
                    DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                    try
                    {
                        long? idAgendaPaciente = db.AGENDA_PACIENTE.Where(ap => ap.Identificacion == db.PACIENTES.Where(p => p.PAC_HISTORIA_CLINICA == lblHistoria).Select(p => p.PAC_IDENTIFICACION).FirstOrDefault()).Select(ap => (long?)ap.ID_AGENDAMIENTO).FirstOrDefault();

                        // Si el ID_AGENDA_PACIENTE es nulo, establecerlo en 1
                        if (!idAgendaPaciente.HasValue)
                        {
                            idAgendaPaciente = 1;
                        }

                        SIGNOSVITALES_CONSULTAEXTERNA sv = new SIGNOSVITALES_CONSULTAEXTERNA();
                        sv.ID_AGENDA_PACIENTE = (long)idAgendaPaciente;
                        sv.ATE_CODIGO = lblAtencion;
                        sv.Presion1 = txt_PresionA1;
                        sv.Presion2 = txt_PresionA2;
                        sv.F_Cardiaca = txt_FCardiaca;
                        sv.F_Respiratoria = txt_FResp;
                        sv.S_Oxigeno = txt_SaturaO;
                        sv.PerimetroA = perimetro;
                        sv.Hemoglobina = hemoglobina;
                        sv.TallaM = txt_Talla;
                        sv.Ind_Masa = txtIMCorporal;
                        sv.Perimetro_Cef = txt_PerimetroC;
                        sv.Glisemia_Capilar = txt_Glicemia;
                        sv.Glasgow = txt_TotalG;
                        sv.Ocular = (short)cmb_Ocular;
                        sv.Verbal = (short)cmb_Verbal;
                        sv.Motora = (short)cmb_Motora;
                        sv.Diametro_Der = (short)txt_DiamPDV;
                        sv.Reaccion_Der = cmb_ReacPDValor;
                        sv.Diametro_Iz = (short)txt_DiamPIV;
                        sv.Reaccion_Iz = cmb_ReacPIValor;
                        sv.PesoKG = txt_PesoKG;
                        sv.T_Bucal = txt_TBucal;
                        sv.T_Axilar = txt_TAxilar;
                        sv.ID_USUARIO = id_usuario;
                        sv.Glucosa_Capilar = glucosa;

                        SIGNOSVITALES_CONSULTAEXTERNA sv1 = db.SIGNOSVITALES_CONSULTAEXTERNA.FirstOrDefault(x => x.ATE_CODIGO == lblAtencion);
                        if (sv1 != null)
                        {
                            sv1.Presion1 = sv.Presion1;
                            sv1.Presion2 = sv.Presion2;
                            sv1.F_Cardiaca = sv.F_Cardiaca;
                            sv1.F_Respiratoria = sv.F_Respiratoria;
                            sv1.S_Oxigeno = sv.S_Oxigeno;
                            sv1.PerimetroA = sv.PerimetroA;
                            sv1.Hemoglobina = sv.Hemoglobina;
                            sv1.TallaM = sv.TallaM;
                            sv1.Ind_Masa = sv.Ind_Masa;
                            sv1.Perimetro_Cef = sv.Perimetro_Cef;
                            sv1.Glisemia_Capilar = sv.Glisemia_Capilar;
                            sv1.Glasgow = sv.Glasgow;
                            sv1.Ocular = sv.Ocular;
                            sv1.Verbal = sv.Verbal;
                            sv1.Motora = sv.Motora;
                            sv1.Diametro_Der = sv.Diametro_Der;
                            sv1.Reaccion_Der = sv.Reaccion_Der;
                            sv1.Diametro_Iz = sv.Diametro_Iz;
                            sv1.Reaccion_Iz = sv.Reaccion_Iz;
                            sv1.PesoKG = sv.PesoKG;
                            sv1.T_Bucal = sv.T_Bucal;
                            sv1.T_Axilar = sv.T_Axilar;
                            sv1.ID_USUARIO = sv.ID_USUARIO;
                            sv1.Glucosa_Capilar = sv.Glucosa_Capilar;
                        }
                        else
                        {
                            db.Crear("SIGNOSVITALES_CONSULTAEXTERNA", sv);
                        }

                        db.SaveChanges();
                        transac.Commit();
                        transaction.Commit();
                        Sqlcon.Close();
                        ConexionEntidades.ConexionEDM.Close();
                    }
                    catch (Exception ex)
                    {
                        transac.Rollback();
                        ConexionEntidades.ConexionEDM.Close();
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                        Sqlcon.Close();
                        return false;
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Sqlcon.Close();
                return false; // los datos no se almacenaron
            }
        }

        public bool EditarTriajeSignosVitales(string lblHistoria, Int64 lblAtencion, int nourgente, int urgente, int critico, int muerto, int alcohol, int drogas, int otros, string txtOtrasActual, string txtObserEnfer, decimal txt_PresionA1, decimal txt_PresionA2, decimal txt_FCardiaca, decimal txt_FResp, decimal txt_TBucal, decimal txt_TAxilar, decimal txt_SaturaO, decimal txt_PesoKG, decimal txt_Talla, decimal txtIMCorporal, decimal txt_PerimetroC, decimal txt_Glicemia, decimal txt_TotalG, int cmb_Motora, int cmb_Verbal, int cmb_Ocular, int txt_DiamPDV, string cmb_ReacPDValor, int txt_DiamPIV, string cmb_ReacPIValor, int txt_Gesta, int txt_Partos, int txt_Abortos, int txt_Cesareas, DateTime dtp_ultimaMenst1, decimal txt_SemanaG, int movFetal, int txt_FrecCF, int memRotas, string txt_Tiempo, int txt_AltU, int txt_Presentacion, int txt_Dilatacion, int txt_Borramiento, string txt_Plano, int pelvis, int sangrado, string txt_Contracciones, int urgente2, decimal perimetro, decimal hemoglobina, decimal glucosa)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            SqlTransaction transaction;
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch
            {
                return false;
            }

            transaction = Sqlcon.BeginTransaction();

            try
            {

                Sqlcmd = new SqlCommand("sp_EditaTriajeConsultaExterna", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@lblHistoria", SqlDbType.VarChar);
                Sqlcmd.Parameters["@lblHistoria"].Value = (lblHistoria);

                Sqlcmd.Parameters.Add("@lblAtencion", SqlDbType.BigInt);
                Sqlcmd.Parameters["@lblAtencion"].Value = (lblAtencion);

                Sqlcmd.Parameters.Add("@nourgente", SqlDbType.Int);
                Sqlcmd.Parameters["@nourgente"].Value = (nourgente);

                Sqlcmd.Parameters.Add("@urgente", SqlDbType.Int);
                Sqlcmd.Parameters["@urgente"].Value = (urgente);

                Sqlcmd.Parameters.Add("@critico", SqlDbType.Int);
                Sqlcmd.Parameters["@critico"].Value = (critico);

                Sqlcmd.Parameters.Add("@muerto", SqlDbType.Int);
                Sqlcmd.Parameters["@muerto"].Value = (muerto);

                Sqlcmd.Parameters.Add("@alcohol", SqlDbType.Int);
                Sqlcmd.Parameters["@alcohol"].Value = (alcohol);

                Sqlcmd.Parameters.Add("@drogas", SqlDbType.Int);
                Sqlcmd.Parameters["@drogas"].Value = (drogas);

                Sqlcmd.Parameters.Add("@otros", SqlDbType.Int);
                Sqlcmd.Parameters["@otros"].Value = (otros);

                Sqlcmd.Parameters.Add("@txtOtrasActual", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtOtrasActual"].Value = (txtOtrasActual);

                Sqlcmd.Parameters.Add("@txtObserEnfer", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtObserEnfer"].Value = (txtObserEnfer);

                Sqlcmd.Parameters.Add("@urgente2", SqlDbType.Int);
                Sqlcmd.Parameters["@urgente2"].Value = (urgente2);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts);

                //GUARDA SIGNOS VITALES SIEMPRE QUE SE GUARDE TRIAJE DEL PACIENTE

                Sqlcmd = new SqlCommand("sp_EditaSignosVitalesConsultaExterna", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@lblHistoria", SqlDbType.VarChar);
                Sqlcmd.Parameters["@lblHistoria"].Value = (lblHistoria);

                Sqlcmd.Parameters.Add("@lblAtencion", SqlDbType.BigInt);
                Sqlcmd.Parameters["@lblAtencion"].Value = (lblAtencion);

                Sqlcmd.Parameters.Add("@txt_PresionA1", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_PresionA1"].Value = (txt_PresionA1);

                Sqlcmd.Parameters.Add("@txt_PresionA2", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_PresionA2"].Value = (txt_PresionA2);

                Sqlcmd.Parameters.Add("@txt_FCardiaca", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_FCardiaca"].Value = (txt_FCardiaca);

                Sqlcmd.Parameters.Add("@txt_FResp", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_FResp"].Value = (txt_FResp);

                Sqlcmd.Parameters.Add("@txt_TBucal", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_TBucal"].Value = (txt_TBucal);

                Sqlcmd.Parameters.Add("@txt_TAxilar", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_TAxilar"].Value = (txt_TAxilar);

                Sqlcmd.Parameters.Add("@txt_SaturaO", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_SaturaO"].Value = (txt_SaturaO);

                Sqlcmd.Parameters.Add("@txt_PesoKG", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_PesoKG"].Value = (txt_PesoKG);

                Sqlcmd.Parameters.Add("@txt_Talla", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_Talla"].Value = (txt_Talla);

                Sqlcmd.Parameters.Add("@txtIMCorporal", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txtIMCorporal"].Value = (txtIMCorporal);

                Sqlcmd.Parameters.Add("@txt_PerimetroC", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_PerimetroC"].Value = (txt_PerimetroC);

                Sqlcmd.Parameters.Add("@txt_Glicemia", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_Glicemia"].Value = (txt_Glicemia);

                Sqlcmd.Parameters.Add("@txt_TotalG", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_TotalG"].Value = (txt_TotalG);

                Sqlcmd.Parameters.Add("@cmb_Ocular", SqlDbType.Int);
                Sqlcmd.Parameters["@cmb_Ocular"].Value = (cmb_Ocular);

                Sqlcmd.Parameters.Add("@cmb_Verbal", SqlDbType.Int);
                Sqlcmd.Parameters["@cmb_Verbal"].Value = (cmb_Verbal);

                Sqlcmd.Parameters.Add("@cmb_Motora", SqlDbType.Int);
                Sqlcmd.Parameters["@cmb_Motora"].Value = (cmb_Motora);

                Sqlcmd.Parameters.Add("@txt_DiamPDV", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_DiamPDV"].Value = (txt_DiamPDV);

                Sqlcmd.Parameters.Add("@cmb_ReacPDValor", SqlDbType.VarChar);
                Sqlcmd.Parameters["@cmb_ReacPDValor"].Value = (cmb_ReacPDValor);

                Sqlcmd.Parameters.Add("@txt_DiamPIV", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_DiamPIV"].Value = (txt_DiamPIV);

                Sqlcmd.Parameters.Add("@cmb_ReacPIValor", SqlDbType.VarChar);
                Sqlcmd.Parameters["@cmb_ReacPIValor"].Value = (cmb_ReacPIValor);
                /// se aumenta para formularios 2021 //////01/02/2024       Cristian Ruiz

                Sqlcmd.Parameters.Add("@perimetro", SqlDbType.Decimal);
                Sqlcmd.Parameters["@perimetro"].Value = (perimetro);

                Sqlcmd.Parameters.Add("@hemoglobina", SqlDbType.Decimal);
                Sqlcmd.Parameters["@hemoglobina"].Value = (hemoglobina);

                Sqlcmd.Parameters.Add("@glucosa", SqlDbType.Decimal);
                Sqlcmd.Parameters["@glucosa"].Value = (glucosa);

                Sqlcmd.Parameters.Add("@pulsioximetria", SqlDbType.Decimal);
                Sqlcmd.Parameters["@pulsioximetria"].Value = (0);


                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts);

                //GUARDA OBSTETRICA SI SE GUARDA LO ANTERIOR

                Sqlcmd = new SqlCommand("sp_EditarObstetricaConsultaExterna", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@lblHistoria", SqlDbType.VarChar);
                Sqlcmd.Parameters["@lblHistoria"].Value = (lblHistoria);

                Sqlcmd.Parameters.Add("@lblAtencion", SqlDbType.BigInt);
                Sqlcmd.Parameters["@lblAtencion"].Value = (lblAtencion);

                Sqlcmd.Parameters.Add("@txt_Gesta", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Gesta"].Value = (txt_Gesta);

                Sqlcmd.Parameters.Add("@txt_Partos", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Partos"].Value = (txt_Partos);

                Sqlcmd.Parameters.Add("@txt_Abortos", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Abortos"].Value = (txt_Abortos);

                Sqlcmd.Parameters.Add("@txt_Cesareas", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Cesareas"].Value = (txt_Cesareas);

                Sqlcmd.Parameters.Add("@dtp_ultimaMenst1", SqlDbType.Date);
                Sqlcmd.Parameters["@dtp_ultimaMenst1"].Value = (dtp_ultimaMenst1);

                Sqlcmd.Parameters.Add("@txt_SemanaG", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_SemanaG"].Value = (txt_SemanaG);

                Sqlcmd.Parameters.Add("@movFetal", SqlDbType.Int);
                Sqlcmd.Parameters["@movFetal"].Value = (movFetal);

                Sqlcmd.Parameters.Add("@txt_FrecCF", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_FrecCF"].Value = (txt_FrecCF);

                Sqlcmd.Parameters.Add("@memRotas", SqlDbType.Int);
                Sqlcmd.Parameters["@memRotas"].Value = (memRotas);

                Sqlcmd.Parameters.Add("@txt_Tiempo", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txt_Tiempo"].Value = (txt_Tiempo);

                Sqlcmd.Parameters.Add("@txt_AltU", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_AltU"].Value = (txt_AltU);

                Sqlcmd.Parameters.Add("@txt_Presentacion", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Presentacion"].Value = (txt_Presentacion);

                Sqlcmd.Parameters.Add("@txt_Dilatacion", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Dilatacion"].Value = (txt_Dilatacion);

                Sqlcmd.Parameters.Add("@txt_Borramiento", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Borramiento"].Value = (txt_Borramiento);

                Sqlcmd.Parameters.Add("@txt_Plano", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txt_Plano"].Value = (txt_Plano);

                Sqlcmd.Parameters.Add("@pelvis", SqlDbType.Int);
                Sqlcmd.Parameters["@pelvis"].Value = (pelvis);

                Sqlcmd.Parameters.Add("@sangrado", SqlDbType.Int);
                Sqlcmd.Parameters["@sangrado"].Value = (sangrado);

                Sqlcmd.Parameters.Add("@txt_Contracciones", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txt_Contracciones"].Value = (txt_Contracciones);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts);


                transaction.Commit();
                try
                {
                    Sqlcon.Close();
                }
                catch
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false; // los datos no se almacenaron
            }
        }
        public DataTable RecuperaTriaje(Int64 Ate_Codigo)
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

            Sqlcmd = new SqlCommand("sp_RecuperaTriajeConsultaExterna", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@Ate_Codigo"].Value = (Ate_Codigo);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        public DataTable RecuperaSignos(Int64 Ate_Codigo)
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

            Sqlcmd = new SqlCommand("sp_RecuperaSignosVitalesConsultaExterna", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@Ate_Codigo"].Value = (Ate_Codigo);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        public DataTable RecuperaObstetrica(Int64 Ate_Codigo)
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

            Sqlcmd = new SqlCommand("sp_RecuperaObstetricaConsultaExterna", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@Ate_Codigo"].Value = (Ate_Codigo);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        public void GuardaDatos002(Form002MSP consultaExt, SIGNOSVITALES_CONSULTAEXTERNA sigconsulta)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                DbTransaction transaction;
                ConexionEntidades.ConexionEDM.Open();
                transaction = ConexionEntidades.ConexionEDM.BeginTransaction();
                Int64 ate_codigo = Convert.ToInt64(consultaExt.AteCodigo);
                SIGNOSVITALES_CONSULTAEXTERNA sv = db.SIGNOSVITALES_CONSULTAEXTERNA.FirstOrDefault(x => x.ATE_CODIGO == ate_codigo);
                try
                {
                    if (sv != null)
                    {
                        sv.Presion1 = sigconsulta.Presion1;
                        sv.Presion2 = sigconsulta.Presion2;
                        sv.F_Cardiaca = sigconsulta.F_Cardiaca;
                        sv.F_Respiratoria = sigconsulta.F_Respiratoria;
                        sv.S_Oxigeno = sigconsulta.S_Oxigeno;
                        sv.PerimetroA = sigconsulta.PerimetroA;
                        sv.Hemoglobina = sigconsulta.Hemoglobina;
                        sv.TallaM = sigconsulta.TallaM;
                        sv.Ind_Masa = sigconsulta.Ind_Masa;
                        sv.Ocular = sigconsulta.Ocular;
                        sv.Verbal = sigconsulta.Verbal;
                        sv.Motora = sigconsulta.Motora;
                        sv.Glasgow = sigconsulta.Glasgow;
                        sv.Glucosa_Capilar = sigconsulta.Glucosa_Capilar;
                        sv.PesoKG = sigconsulta.PesoKG;
                        sv.T_Bucal = sigconsulta.T_Bucal;
                        sv.T_Axilar = sigconsulta.T_Axilar;
                        sv.ID_USUARIO = sigconsulta.ID_USUARIO;
                        sv.Diametro_Der = sigconsulta.Diametro_Der;
                        sv.Reaccion_Der = sigconsulta.Reaccion_Der;
                        sv.Diametro_Iz = sigconsulta.Diametro_Iz;
                        sv.Reaccion_Iz = sigconsulta.Reaccion_Iz;
                    }
                    else
                    {
                        db.Crear("SIGNOSVITALES_CONSULTAEXTERNA", sigconsulta);
                    }
                    db.Crear("Form002MSP", consultaExt);
                    db.SaveChanges();
                    transaction.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    Console.WriteLine(ex.Message);
                }
            }
            #region Codigo anterior al modeo
            //SqlConnection Sqlcon;
            //SqlCommand Sqlcmd;
            //SqlDataAdapter Sqldap;
            //DataSet Dts;
            //BaseContextoDatos obj = new BaseContextoDatos();
            //Sqlcon = obj.ConectarBd();
            //try
            //{
            //    Sqlcon.Open();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            //Sqlcmd = new SqlCommand("sp_GuardaForm002ConsultaExterna", Sqlcon);

            //Sqlcmd.CommandType = CommandType.StoredProcedure;

            //Sqlcmd.Parameters.Add("@Nombre", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Nombre"].Value = (datos.nombrePaciente);

            //Sqlcmd.Parameters.Add("@Apellido", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Apellido"].Value = (datos.apellidoPaciemte);

            //Sqlcmd.Parameters.Add("@Sexo", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Sexo"].Value = (datos.sexoPaciente);

            //Sqlcmd.Parameters.Add("@Edad", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Edad"].Value = (datos.edadPaciente);

            //Sqlcmd.Parameters.Add("@Historia", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Historia"].Value = (datos.historiaClinica);

            //Sqlcmd.Parameters.Add("@AteCodigo", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@AteCodigo"].Value = (datos.ateCodigo);

            //Sqlcmd.Parameters.Add("@Motivo", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Motivo"].Value = (datos.motivoConsulta);

            //Sqlcmd.Parameters.Add("@AntecedentesPersonales", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@AntecedentesPersonales"].Value = (datos.antecedentesPersonales);

            //Sqlcmd.Parameters.Add("@Cardiopatia", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Cardiopatia"].Value = (datos.cardiopatia);

            //Sqlcmd.Parameters.Add("@Diabetes", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Diabetes"].Value = (datos.diabetes);

            //Sqlcmd.Parameters.Add("@Vascular", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Vascular"].Value = (datos.vascular);

            //Sqlcmd.Parameters.Add("@Hipertencion", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Hipertencion"].Value = (datos.hipertension);

            //Sqlcmd.Parameters.Add("@Cancer", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Cancer"].Value = (datos.cancer);

            //Sqlcmd.Parameters.Add("@Tuberculosis", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Tuberculosis"].Value = (datos.tuberculosis);

            //Sqlcmd.Parameters.Add("@mental", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@mental"].Value = (datos.mental);

            //Sqlcmd.Parameters.Add("@infecciosa", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@infecciosa"].Value = (datos.infeccionsa);

            //Sqlcmd.Parameters.Add("@malformacion", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@malformacion"].Value = (datos.malFormado);

            //Sqlcmd.Parameters.Add("@otro", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@otro"].Value = (datos.otro);

            //Sqlcmd.Parameters.Add("@antecedentesFamiliares", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@antecedentesFamiliares"].Value = (datos.antecedentesFamiliares);

            //Sqlcmd.Parameters.Add("@enfermedadActual", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@enfermedadActual"].Value = (datos.enfermedadProblemaActual);

            //Sqlcmd.Parameters.Add("@sentidos", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@sentidos"].Value = (datos.sentidos);

            //Sqlcmd.Parameters.Add("@sentidossp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@sentidossp"].Value = (datos.sentidossp);

            //Sqlcmd.Parameters.Add("@respiratorio", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@respiratorio"].Value = (datos.respiratorio);

            //Sqlcmd.Parameters.Add("@respiratoriosp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@respiratoriosp"].Value = (datos.respiratoriosp);

            //Sqlcmd.Parameters.Add("@cardioVascular", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@cardioVascular"].Value = (datos.cardioVascular);

            //Sqlcmd.Parameters.Add("@cardioVascularsp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@cardioVascularsp"].Value = (datos.cardioVascularsp);

            //Sqlcmd.Parameters.Add("@digestivo", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@digestivo"].Value = (datos.digestivo);

            //Sqlcmd.Parameters.Add("@digestivosp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@digestivosp"].Value = (datos.digestivosp);

            //Sqlcmd.Parameters.Add("@genital", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@genital"].Value = (datos.genital);

            //Sqlcmd.Parameters.Add("@genitalsp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@genitalsp"].Value = (datos.genitalsp);

            //Sqlcmd.Parameters.Add("@urinario", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@urinario"].Value = (datos.urinario);

            //Sqlcmd.Parameters.Add("@urinariosp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@urinariosp"].Value = (datos.urinariosp);

            //Sqlcmd.Parameters.Add("@esqueletico", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@esqueletico"].Value = (datos.esqueletico);

            //Sqlcmd.Parameters.Add("@esqueleticosp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@esqueleticosp"].Value = (datos.esqueleticosp);

            //Sqlcmd.Parameters.Add("@endocrino", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@endocrino"].Value = (datos.endocrino);

            //Sqlcmd.Parameters.Add("@endocrinosp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@endocrinosp"].Value = (datos.endocrinosp);

            //Sqlcmd.Parameters.Add("@linfatico", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@linfatico"].Value = (datos.linfatico);

            //Sqlcmd.Parameters.Add("@linfaticosp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@linfaticosp"].Value = (datos.linfaticosp);

            //Sqlcmd.Parameters.Add("@nervioso", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@nervioso"].Value = (datos.nervioso);

            //Sqlcmd.Parameters.Add("@nerviososp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@nerviososp"].Value = (datos.nerviososp);

            //Sqlcmd.Parameters.Add("@revisionactual", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@revisionactual"].Value = (datos.detalleRevisionOrganos);

            //Sqlcmd.Parameters.Add("@fechamedicion", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@fechamedicion"].Value = (datos.fechaMedicion);

            //Sqlcmd.Parameters.Add("@temperatura", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@temperatura"].Value = (datos.temperatura);

            //Sqlcmd.Parameters.Add("@presion1", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@presion1"].Value = (datos.presionArterial1);

            //Sqlcmd.Parameters.Add("@presion2", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@presion2"].Value = (datos.presionArterial2);

            //Sqlcmd.Parameters.Add("@pulso", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@pulso"].Value = (datos.pulso);

            //Sqlcmd.Parameters.Add("@frecuenciaRespiratoria", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@frecuenciaRespiratoria"].Value = (datos.frecuenciaRespiratoria);

            //Sqlcmd.Parameters.Add("@peso", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@peso"].Value = (datos.peso);

            //Sqlcmd.Parameters.Add("@talla", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@talla"].Value = (datos.talla);

            //Sqlcmd.Parameters.Add("@cabeza", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@cabeza"].Value = (datos.cabeza);

            //Sqlcmd.Parameters.Add("@cabezasp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@cabezasp"].Value = (datos.cabezasp);

            //Sqlcmd.Parameters.Add("@cuello", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@cuello"].Value = (datos.cuello);

            //Sqlcmd.Parameters.Add("@cuellosp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@cuellosp"].Value = (datos.cuellosp);

            //Sqlcmd.Parameters.Add("@torax", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@torax"].Value = (datos.torax);

            //Sqlcmd.Parameters.Add("@toraxsp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@toraxsp"].Value = (datos.toraxsp);

            //Sqlcmd.Parameters.Add("@abdomen", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@abdomen"].Value = (datos.abdomen);

            //Sqlcmd.Parameters.Add("@abdomensp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@abdomensp"].Value = (datos.abdomensp);

            //Sqlcmd.Parameters.Add("@pelvis", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@pelvis"].Value = (datos.pelvis);

            //Sqlcmd.Parameters.Add("@pelvissp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@pelvissp"].Value = (datos.pelvissp);

            //Sqlcmd.Parameters.Add("@extremidades", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@extremidades"].Value = (datos.extremidades);

            //Sqlcmd.Parameters.Add("@extremidadessp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@extremidadessp"].Value = (datos.extremidadessp);

            //Sqlcmd.Parameters.Add("@examenFisico", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@examenFisico"].Value = (datos.examenFisico);

            //Sqlcmd.Parameters.Add("@diagnostico1", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico1"].Value = (datos.diagnosticco1);

            //Sqlcmd.Parameters.Add("@diagnostico1cie", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico1cie"].Value = (datos.diagnosticco1cie);

            //Sqlcmd.Parameters.Add("@diagnostico1pre", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico1pre"].Value = (datos.diagnosticco1prepre);

            //Sqlcmd.Parameters.Add("@diagnostico1def", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico1def"].Value = (datos.diagnosticco1predef);

            //Sqlcmd.Parameters.Add("@diagnostico2", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico2"].Value = (datos.diagnosticco2);

            //Sqlcmd.Parameters.Add("@diagnostico2cie", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico2cie"].Value = (datos.diagnosticco2cie);

            //Sqlcmd.Parameters.Add("@diagnostico2pre", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico2pre"].Value = (datos.diagnosticco2prepre);

            //Sqlcmd.Parameters.Add("@diagnostico2def", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico2def"].Value = (datos.diagnosticco2predef);

            //Sqlcmd.Parameters.Add("@diagnostico3", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico3"].Value = (datos.diagnosticco3);

            //Sqlcmd.Parameters.Add("@diagnostico3cie", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico3cie"].Value = (datos.diagnosticco3cie);

            //Sqlcmd.Parameters.Add("@diagnostico3pre", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico3pre"].Value = (datos.diagnosticco3prepre);

            //Sqlcmd.Parameters.Add("@diagnostico3def", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico3def"].Value = (datos.diagnosticco3predef);

            //Sqlcmd.Parameters.Add("@diagnostico4", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico4"].Value = (datos.diagnosticco4);

            //Sqlcmd.Parameters.Add("@diagnostico4cie", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico4cie"].Value = (datos.diagnosticco4cie);

            //Sqlcmd.Parameters.Add("@diagnostico4pre", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico4pre"].Value = (datos.diagnosticco4prepre);

            //Sqlcmd.Parameters.Add("@diagnostico4def", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico4def"].Value = (datos.diagnosticco4predef);

            //Sqlcmd.Parameters.Add("@planesTratamiento", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@planesTratamiento"].Value = (datos.planesTratamiento);

            //Sqlcmd.Parameters.Add("@evolucion", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@evolucion"].Value = (datos.evolucion);

            //Sqlcmd.Parameters.Add("@prescripciones", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@prescripciones"].Value = (datos.prescripciones);

            //Sqlcmd.Parameters.Add("@fecha", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@fecha"].Value = (DateTime.Now);

            //Sqlcmd.Parameters.Add("@hora", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@hora"].Value = ("");

            //Sqlcmd.Parameters.Add("@dr", SqlDbType.VarChar);
            ////Sqlcmd.Parameters["@dr"].Value = (Entidades.Clases.Sesion.nomUsuario);
            //Sqlcmd.Parameters["@dr"].Value = (datos.drTratatnte);

            //Sqlcmd.Parameters.Add("@codigo", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@codigo"].Value = ("");
            //try
            //{
            //    Sqldap = new SqlDataAdapter();
            //    Sqlcmd.CommandTimeout = 180;
            //    Sqldap.SelectCommand = Sqlcmd;
            //    Dts = new DataSet();
            //    Sqldap.Fill(Dts);

            //    Sqlcon.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            #endregion
        }

        public DataTable getConsultasExternas(int atecodigo)
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
            Sqlcmd = new SqlCommand("SELECT ID_FORM002 AS ID,  '[MEDICO: ' + dr + '] - [MOTIVO: ' + Motivo + ']' AS DATOS FROM Form002MSP WHERE AteCodigo = " + atecodigo, Sqlcon);
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
        public Form002MSP PacienteExisteCxE(string ate_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                Form002MSP consulta = (from cxe in db.Form002MSP
                                       where cxe.AteCodigo == ate_codigo
                                       select cxe).OrderByDescending(x => x.ID_FORM002).FirstOrDefault();
                return consulta;

            }
        }
        public bool PacienteCerradaCxE(Int64 ate_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                FORMULARIOS_MSP_CERRADOS obj = (from cerrado in db.FORMULARIOS_MSP_CERRADOS
                                                where cerrado.ATE_CODIGO == ate_codigo
                                                select cerrado).FirstOrDefault();
                if (obj != null)
                {
                    return true;
                }
                return false;
            }
        }
        public SIGNOSVITALES_CONSULTAEXTERNA signoscitalesCex(Int64 ate_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                SIGNOSVITALES_CONSULTAEXTERNA sv = (from s in db.SIGNOSVITALES_CONSULTAEXTERNA
                                                    where s.ATE_CODIGO == ate_codigo
                                                    select s).FirstOrDefault();
                return sv;
            }
        }
        public bool EditarDatos002(Form002MSP consulta, Int64 id, SIGNOSVITALES_CONSULTAEXTERNA sigconsulta)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    Int64 ate_codigo = Convert.ToInt64(consulta.AteCodigo);
                    SIGNOSVITALES_CONSULTAEXTERNA sv = db.SIGNOSVITALES_CONSULTAEXTERNA.FirstOrDefault(x => x.ATE_CODIGO == ate_codigo);
                    if (sv != null)
                    {
                        sv.Presion1 = sigconsulta.Presion1;
                        sv.Presion2 = sigconsulta.Presion2;
                        sv.F_Cardiaca = sigconsulta.F_Cardiaca;
                        sv.F_Respiratoria = sigconsulta.F_Respiratoria;
                        sv.S_Oxigeno = sigconsulta.S_Oxigeno;
                        sv.PerimetroA = sigconsulta.PerimetroA;
                        sv.Hemoglobina = sigconsulta.Hemoglobina;
                        sv.TallaM = sigconsulta.TallaM;
                        sv.Ind_Masa = sigconsulta.Ind_Masa;
                        sv.Ocular = sigconsulta.Ocular;
                        sv.Verbal = sigconsulta.Verbal;
                        sv.Motora = sigconsulta.Motora;
                        sv.Glasgow = sigconsulta.Glasgow;
                        sv.Glucosa_Capilar = sigconsulta.Glucosa_Capilar;
                        sv.PesoKG = sigconsulta.PesoKG;
                        sv.T_Bucal = sigconsulta.T_Bucal;
                        sv.T_Axilar = sigconsulta.T_Axilar;
                        sv.ID_USUARIO = sigconsulta.ID_USUARIO;
                        sv.Diametro_Der = sigconsulta.Diametro_Der;
                        sv.Reaccion_Der = sigconsulta.Reaccion_Der;
                        sv.Diametro_Iz = sigconsulta.Diametro_Iz;
                        sv.Reaccion_Iz = sigconsulta.Reaccion_Iz;
                    }
                    else
                    {
                        db.Crear("SIGNOSVITALES_CONSULTAEXTERNA", sigconsulta);
                    }
                    Form002MSP externa = db.Form002MSP.FirstOrDefault(x => x.ID_FORM002 == id);
                    externa.Motivo = consulta.Motivo;
                    externa.cardiopatiaP = consulta.cardiopatiaP;
                    externa.endocrinoP = consulta.endocrinoP;
                    externa.vascularP = consulta.vascularP;
                    externa.hipertencionP = consulta.hipertencionP;
                    externa.cancerP = consulta.cancerP;
                    externa.tuberculosisP = consulta.tuberculosisP;
                    externa.mentalP = consulta.mentalP;
                    externa.infecciosaP = consulta.infecciosaP;
                    externa.malformacionP = consulta.malformacionP;
                    externa.otroP = consulta.otroP;
                    externa.AntecedentesPersonales = consulta.AntecedentesPersonales;
                    externa.Cardiopatia = consulta.Cardiopatia;
                    externa.endocrinoF = consulta.endocrinoF;
                    externa.Vascular = consulta.Vascular;
                    externa.Hipertencion = consulta.Hipertencion;
                    externa.Cancer = consulta.Cancer;
                    externa.tuberculosis = consulta.tuberculosis;
                    externa.mental = consulta.mental;
                    externa.infecciosa = consulta.infecciosa;
                    externa.malformacion = consulta.malformacion;
                    externa.otro = consulta.otro;
                    externa.antecedentesFamiliares = consulta.antecedentesFamiliares;
                    externa.enfermedadActual = consulta.enfermedadActual;
                    externa.sentidos = consulta.sentidos;
                    externa.sentidossp = consulta.sentidos;
                    externa.respiratorio = consulta.respiratorio;
                    externa.respiratoriosp = consulta.respiratoriosp;
                    externa.cardioVascular = consulta.cardioVascular;
                    externa.cardioVascularsp = consulta.cardioVascularsp;
                    externa.digestivo = consulta.digestivo;
                    externa.digestivosp = consulta.digestivosp;
                    externa.genital = consulta.genital;
                    externa.genitalsp = consulta.genitalsp;
                    externa.urinario = consulta.urinario;
                    externa.urinariosp = consulta.urinariosp;
                    externa.esqueletico = consulta.esqueletico;
                    externa.esqueleticosp = consulta.esqueleticosp;
                    externa.endocrino = consulta.endocrino;
                    externa.endocrinosp = consulta.endocrinosp;
                    externa.linfatico = consulta.linfatico;
                    externa.linfaticosp = consulta.linfaticosp;
                    externa.nervioso = consulta.nervioso;
                    externa.nerviososp = consulta.nerviososp;
                    externa.piel = consulta.piel;
                    externa.pielsp = consulta.pielsp;
                    externa.revisionactual = consulta.revisionactual;
                    externa.fechamedicion = consulta.fechamedicion;
                    externa.temperatura = consulta.temperatura;
                    externa.presion1 = consulta.presion1;
                    externa.presion2 = consulta.presion2;
                    externa.pulso = consulta.pulso;
                    externa.frecuenciaRespiratoria = consulta.frecuenciaRespiratoria;
                    externa.peso = consulta.peso;
                    externa.talla = consulta.talla;
                    externa.cabezasp = consulta.cabezasp;
                    externa.cabeza = consulta.cabeza;
                    externa.cuellosp = consulta.cuellosp;
                    externa.cuello = consulta.cuello;
                    externa.toraxsp = consulta.toraxsp;
                    externa.torax = consulta.torax;
                    externa.abdomensp = consulta.abdomensp;
                    externa.abdomen = consulta.abdomen;
                    externa.pelvissp = consulta.pelvissp;
                    externa.pelvis = consulta.pelvissp;
                    externa.extremidadessp = consulta.extremidadessp;
                    externa.extremidades = consulta.extremidades;
                    externa.rPiel = consulta.rPiel;
                    externa.rOjos = consulta.rOjos;
                    externa.rOidos = consulta.rOidos;
                    externa.rNariz = consulta.rNariz;
                    externa.rBoca = consulta.rBoca;
                    externa.rOrofaringe = consulta.rOrofaringe;
                    externa.rAxilas = consulta.rAxilas;
                    externa.rColumna = consulta.rColumna;
                    externa.rIngle = consulta.rIngle;
                    externa.rInferior = consulta.rInferior;
                    externa.sSentidos = consulta.sSentidos;
                    externa.sRespiratorio = consulta.respiratorio;
                    externa.sCardio = consulta.sCardio;
                    externa.sDigestivo = consulta.sDigestivo;
                    externa.sGenital = consulta.sGenital;
                    externa.sUrinario = consulta.sUrinario;
                    externa.sMusculo = consulta.sMusculo;
                    externa.sEndocrino = consulta.sEndocrino;
                    externa.sLimfatico = consulta.sLimfatico;
                    externa.sNeurologico = consulta.sNeurologico;
                    externa.examenFisico = consulta.examenFisico;
                    externa.diagnostico1 = consulta.diagnostico1;
                    externa.diagnostico1cie = consulta.diagnostico1cie;
                    externa.diagnostico1pre = consulta.diagnostico1pre;
                    externa.diagnostico1def = consulta.diagnostico1def;
                    externa.diagnostico2 = consulta.diagnostico2;
                    externa.diagnostico2cie = consulta.diagnostico2cie;
                    externa.diagnostico2pre = consulta.diagnostico2pre;
                    externa.diagnostico2def = consulta.diagnostico2def;
                    externa.diagnostico3 = consulta.diagnostico3;
                    externa.diagnostico3cie = consulta.diagnostico3cie;
                    externa.diagnostico3pre = consulta.diagnostico3pre;
                    externa.diagnostico3def = consulta.diagnostico3def;
                    externa.diagnostico4 = consulta.diagnostico4;
                    externa.diagnostico4cie = consulta.diagnostico4cie;
                    externa.diagnostico4pre = consulta.diagnostico4pre;
                    externa.diagnostico4def = consulta.diagnostico4def;
                    externa.diagnostico5 = consulta.diagnostico5;
                    externa.diagnostico5cie = consulta.diagnostico5cie;
                    externa.diagnostico5pre = consulta.diagnostico5pre;
                    externa.diagnostico5def = consulta.diagnostico5def;
                    externa.diagnostico6 = consulta.diagnostico6;
                    externa.diagnostico6cie = consulta.diagnostico6cie;
                    externa.diagnostico6pre = consulta.diagnostico6pre;
                    externa.diagnostico6def = consulta.diagnostico6def;
                    externa.planesTratamiento = consulta.planesTratamiento;
                    externa.evolucion = consulta.evolucion;
                    externa.prescripciones = consulta.prescripciones;
                    externa.dr = consulta.dr;
                    //externa.fecha = Convert.ToString(DateTime.Now);
                    db.SaveChanges();
                    transac.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    transac.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

            #region Edicion codigo antiguo Mario 20/10/2023
            //SqlConnection Sqlcon;
            //SqlCommand Sqlcmd;
            //SqlDataAdapter Sqldap;
            //DataSet Dts;
            //BaseContextoDatos obj = new BaseContextoDatos();
            //SqlTransaction transaction;
            //Sqlcon = obj.ConectarBd();
            //Sqlcon.Open();
            //transaction = Sqlcon.BeginTransaction();

            //Sqlcmd = new SqlCommand("sp_EditarForm002ConsultaExterna", Sqlcon);

            //Sqlcmd.CommandType = CommandType.StoredProcedure;

            //Sqlcmd.Transaction = transaction;

            //Sqlcmd.Parameters.Add("@Motivo", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Motivo"].Value = (datos.motivoConsulta);

            //Sqlcmd.Parameters.Add("@AntecedentesPersonales", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@AntecedentesPersonales"].Value = (datos.antecedentesPersonales);

            //Sqlcmd.Parameters.Add("@Cardiopatia", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Cardiopatia"].Value = (datos.cardiopatia);

            //Sqlcmd.Parameters.Add("@Diabetes", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Diabetes"].Value = (datos.diabetes);

            //Sqlcmd.Parameters.Add("@Vascular", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Vascular"].Value = (datos.vascular);

            //Sqlcmd.Parameters.Add("@Hipertencion", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Hipertencion"].Value = (datos.hipertension);

            //Sqlcmd.Parameters.Add("@Cancer", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Cancer"].Value = (datos.cancer);

            //Sqlcmd.Parameters.Add("@Tuberculosis", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@Tuberculosis"].Value = (datos.tuberculosis);

            //Sqlcmd.Parameters.Add("@mental", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@mental"].Value = (datos.mental);

            //Sqlcmd.Parameters.Add("@infecciosa", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@infecciosa"].Value = (datos.infeccionsa);

            //Sqlcmd.Parameters.Add("@malformacion", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@malformacion"].Value = (datos.malFormado);

            //Sqlcmd.Parameters.Add("@otro", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@otro"].Value = (datos.otro);

            //Sqlcmd.Parameters.Add("@antecedentesFamiliares", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@antecedentesFamiliares"].Value = (datos.antecedentesFamiliares);

            //Sqlcmd.Parameters.Add("@enfermedadActual", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@enfermedadActual"].Value = (datos.enfermedadProblemaActual);

            //Sqlcmd.Parameters.Add("@sentidos", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@sentidos"].Value = (datos.sentidos);

            //Sqlcmd.Parameters.Add("@sentidossp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@sentidossp"].Value = (datos.sentidossp);

            //Sqlcmd.Parameters.Add("@respiratorio", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@respiratorio"].Value = (datos.respiratorio);

            //Sqlcmd.Parameters.Add("@respiratoriosp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@respiratoriosp"].Value = (datos.respiratoriosp);

            //Sqlcmd.Parameters.Add("@cardioVascular", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@cardioVascular"].Value = (datos.cardioVascular);

            //Sqlcmd.Parameters.Add("@cardioVascularsp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@cardioVascularsp"].Value = (datos.cardioVascularsp);

            //Sqlcmd.Parameters.Add("@digestivo", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@digestivo"].Value = (datos.digestivo);

            //Sqlcmd.Parameters.Add("@digestivosp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@digestivosp"].Value = (datos.digestivosp);

            //Sqlcmd.Parameters.Add("@genital", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@genital"].Value = (datos.genital);

            //Sqlcmd.Parameters.Add("@genitalsp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@genitalsp"].Value = (datos.genitalsp);

            //Sqlcmd.Parameters.Add("@urinario", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@urinario"].Value = (datos.urinario);

            //Sqlcmd.Parameters.Add("@urinariosp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@urinariosp"].Value = (datos.urinariosp);

            //Sqlcmd.Parameters.Add("@esqueletico", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@esqueletico"].Value = (datos.esqueletico);

            //Sqlcmd.Parameters.Add("@esqueleticosp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@esqueleticosp"].Value = (datos.esqueleticosp);

            //Sqlcmd.Parameters.Add("@endocrino", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@endocrino"].Value = (datos.endocrino);

            //Sqlcmd.Parameters.Add("@endocrinosp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@endocrinosp"].Value = (datos.endocrinosp);

            //Sqlcmd.Parameters.Add("@linfatico", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@linfatico"].Value = (datos.linfatico);

            //Sqlcmd.Parameters.Add("@linfaticosp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@linfaticosp"].Value = (datos.linfaticosp);

            //Sqlcmd.Parameters.Add("@nervioso", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@nervioso"].Value = (datos.nervioso);

            //Sqlcmd.Parameters.Add("@nerviososp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@nerviososp"].Value = (datos.nerviososp);

            //Sqlcmd.Parameters.Add("@revisionactual", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@revisionactual"].Value = (datos.detalleRevisionOrganos);

            //Sqlcmd.Parameters.Add("@fechamedicion", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@fechamedicion"].Value = (datos.fechaMedicion);

            //Sqlcmd.Parameters.Add("@temperatura", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@temperatura"].Value = (datos.temperatura);

            //Sqlcmd.Parameters.Add("@presion1", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@presion1"].Value = (datos.presionArterial1);

            //Sqlcmd.Parameters.Add("@presion2", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@presion2"].Value = (datos.presionArterial2);

            //Sqlcmd.Parameters.Add("@pulso", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@pulso"].Value = (datos.pulso);

            //Sqlcmd.Parameters.Add("@frecuenciaRespiratoria", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@frecuenciaRespiratoria"].Value = (datos.frecuenciaRespiratoria);

            //Sqlcmd.Parameters.Add("@peso", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@peso"].Value = (datos.peso);

            //Sqlcmd.Parameters.Add("@talla", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@talla"].Value = (datos.talla);

            //Sqlcmd.Parameters.Add("@cabeza", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@cabeza"].Value = (datos.cabeza);

            //Sqlcmd.Parameters.Add("@cabezasp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@cabezasp"].Value = (datos.cabezasp);

            //Sqlcmd.Parameters.Add("@cuello", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@cuello"].Value = (datos.cuello);

            //Sqlcmd.Parameters.Add("@cuellosp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@cuellosp"].Value = (datos.cuellosp);

            //Sqlcmd.Parameters.Add("@torax", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@torax"].Value = (datos.torax);

            //Sqlcmd.Parameters.Add("@toraxsp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@toraxsp"].Value = (datos.toraxsp);

            //Sqlcmd.Parameters.Add("@abdomen", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@abdomen"].Value = (datos.abdomen);

            //Sqlcmd.Parameters.Add("@abdomensp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@abdomensp"].Value = (datos.abdomensp);

            //Sqlcmd.Parameters.Add("@pelvis", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@pelvis"].Value = (datos.pelvis);

            //Sqlcmd.Parameters.Add("@pelvissp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@pelvissp"].Value = (datos.pelvissp);

            //Sqlcmd.Parameters.Add("@extremidades", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@extremidades"].Value = (datos.extremidades);

            //Sqlcmd.Parameters.Add("@extremidadessp", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@extremidadessp"].Value = (datos.extremidadessp);

            //Sqlcmd.Parameters.Add("@examenFisico", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@examenFisico"].Value = (datos.examenFisico);

            //Sqlcmd.Parameters.Add("@diagnostico1", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico1"].Value = (datos.diagnosticco1);

            //Sqlcmd.Parameters.Add("@diagnostico1cie", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico1cie"].Value = (datos.diagnosticco1cie);

            //Sqlcmd.Parameters.Add("@diagnostico1pre", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico1pre"].Value = (datos.diagnosticco1prepre);

            //Sqlcmd.Parameters.Add("@diagnostico1def", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico1def"].Value = (datos.diagnosticco1predef);

            //Sqlcmd.Parameters.Add("@diagnostico2", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico2"].Value = (datos.diagnosticco2);

            //Sqlcmd.Parameters.Add("@diagnostico2cie", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico2cie"].Value = (datos.diagnosticco2cie);

            //Sqlcmd.Parameters.Add("@diagnostico2pre", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico2pre"].Value = (datos.diagnosticco2prepre);

            //Sqlcmd.Parameters.Add("@diagnostico2def", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico2def"].Value = (datos.diagnosticco2predef);

            //Sqlcmd.Parameters.Add("@diagnostico3", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico3"].Value = (datos.diagnosticco3);

            //Sqlcmd.Parameters.Add("@diagnostico3cie", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico3cie"].Value = (datos.diagnosticco3cie);

            //Sqlcmd.Parameters.Add("@diagnostico3pre", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico3pre"].Value = (datos.diagnosticco3prepre);

            //Sqlcmd.Parameters.Add("@diagnostico3def", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico3def"].Value = (datos.diagnosticco3predef);

            //Sqlcmd.Parameters.Add("@diagnostico4", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico4"].Value = (datos.diagnosticco4);

            //Sqlcmd.Parameters.Add("@diagnostico4cie", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico4cie"].Value = (datos.diagnosticco4cie);

            //Sqlcmd.Parameters.Add("@diagnostico4pre", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico4pre"].Value = (datos.diagnosticco4prepre);

            //Sqlcmd.Parameters.Add("@diagnostico4def", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@diagnostico4def"].Value = (datos.diagnosticco4predef);

            //Sqlcmd.Parameters.Add("@planesTratamiento", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@planesTratamiento"].Value = (datos.planesTratamiento);

            //Sqlcmd.Parameters.Add("@evolucion", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@evolucion"].Value = (datos.evolucion);

            //Sqlcmd.Parameters.Add("@prescripciones", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@prescripciones"].Value = (datos.prescripciones);

            //Sqlcmd.Parameters.Add("@fecha", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@fecha"].Value = (DateTime.Now);

            //Sqlcmd.Parameters.Add("@hora", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@hora"].Value = ("");

            //Sqlcmd.Parameters.Add("@dr", SqlDbType.VarChar);
            ////Sqlcmd.Parameters["@dr"].Value = (Entidades.Clases.Sesion.nomUsuario);
            //Sqlcmd.Parameters["@dr"].Value = (datos.drTratatnte);

            //Sqlcmd.Parameters.Add("@codigo", SqlDbType.VarChar);
            //Sqlcmd.Parameters["@codigo"].Value = ("");

            //Sqlcmd.Parameters.Add("@id", SqlDbType.BigInt);
            //Sqlcmd.Parameters["@id"].Value = (id);
            //try
            //{
            //    Sqldap = new SqlDataAdapter();
            //    Sqlcmd.CommandTimeout = 180;
            //    Sqldap.SelectCommand = Sqlcmd;
            //    Dts = new DataSet();
            //    Sqldap.Fill(Dts);

            //    transaction.Commit();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    transaction.Rollback();
            //    return false;
            //}
            #endregion
        }

        public bool CerrarCxE(string formulario, Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;
            connection = obj.ConectarBd();
            connection.Open();

            transaction = connection.BeginTransaction();
            try
            {
                command = new SqlCommand("sp_CxE_Cerrar", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@formulario", formulario);
                command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
                command.Parameters.AddWithValue("@id_usuario", His.Entidades.Clases.Sesion.codUsuario);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                transaction.Rollback();
                return false;
            }
        }
        public bool AbrirCxE(Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;
            connection = obj.ConectarBd();
            connection.Open();

            transaction = connection.BeginTransaction();
            try
            {
                command = new SqlCommand("sp_CxE_Abrir", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                transaction.Rollback();
                return false;
            }
        }
        public FORMULARIOS_MSP_CERRADOS ValidarCerrados(Int64 ate_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                FORMULARIOS_MSP_CERRADOS cerrado = (from c in db.FORMULARIOS_MSP_CERRADOS
                                                    where c.ATE_CODIGO == ate_codigo
                                                    select c).FirstOrDefault();
                return cerrado;
            }
        }
        public DataTable ViewAgendamiento(DateTime desde, DateTime hasta)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable table = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_AgendamientoView", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@desde", desde);
            command.Parameters.AddWithValue("@hasta", hasta);
            reader = command.ExecuteReader();
            table.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return table;
        }

        public List<HORARIO_ATENCION> Horarios(string tiempo, string consultorio, DateTime fechaCita, string medico, string cedula)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HORARIO_ATENCION> ha = new List<HORARIO_ATENCION>();
                List<HORARIO_ATENCION> nueva = new List<HORARIO_ATENCION>();
                List<AGENDAMIENTO> agenda = (from a in db.AGENDAMIENTO
                                             where a.FechaAgenda == fechaCita.Date
                                             && a.Consultorio == consultorio
                                             select a).ToList();
                List<AGENDAMIENTO> agendaMedico = (from a in db.AGENDAMIENTO
                                                   where a.FechaAgenda == fechaCita.Date
                                                   && a.Medico == medico
                                                   select a).ToList();
                List<AGENDAMIENTO> agendaPaciente = (from a in db.AGENDAMIENTO
                                                     join ap in db.AGENDA_PACIENTE on a.ID_AGENDAMIENTO equals ap.ID_AGENDAMIENTO
                                                     where a.FechaAgenda == fechaCita.Date
                                                     && ap.Identificacion == cedula
                                                     select a).ToList();

                if (agenda.Count > 0) //tengo citas agendadas
                {
                    if (tiempo != "") //valida si es otro dia es decir no es la fecha actual
                    {
                        ha = (from h in db.HORARIO_ATENCION
                              select h).ToList();

                        foreach (var item in ha)
                        {
                            string[] hora = item.Horarios.Split('-');
                            DateTime h = Convert.ToDateTime(hora[0]);
                            if (DateTime.Now.Hour > h.Hour)
                                nueva.Add(item);
                            else if (DateTime.Now.Minute >= h.Minute && DateTime.Now.Hour == h.Hour)
                                nueva.Add(item);
                        }
                    }
                    else //validara todo dentro del dia actual
                    {
                        ha = (from h in db.HORARIO_ATENCION
                              select h).ToList();
                    }
                    foreach (var item in agenda)
                    {
                        foreach (var x in ha)
                        {
                            if (item.Hora.Trim() == x.Horarios.Trim())
                                nueva.Add(x);
                        }
                    }
                }
                if (agendaMedico.Count > 0)
                {
                    if (tiempo != "") //valida si es otro dia es decir no es la fecha actual
                    {
                        ha = (from h in db.HORARIO_ATENCION
                              select h).ToList();

                        foreach (var item in ha)
                        {
                            string[] hora = item.Horarios.Split('-');
                            DateTime h = Convert.ToDateTime(hora[0]);
                            if (DateTime.Now.Hour > h.Hour)
                                nueva.Add(item);
                            else if (DateTime.Now.Minute >= h.Minute && DateTime.Now.Hour == h.Hour)
                                nueva.Add(item);
                        }
                    }
                    else //validara todo dentro del dia actual
                    {
                        ha = (from h in db.HORARIO_ATENCION
                              select h).ToList();
                    }
                    foreach (var item in agendaMedico)
                    {
                        foreach (var x in ha)
                        {
                            if (item.Hora.Trim() == x.Horarios.Trim())
                                nueva.Add(x);
                        }
                    }
                }
                if (agendaPaciente.Count > 0)
                {
                    if (tiempo != "") //valida si es otro dia es decir no es la fecha actual
                    {
                        ha = (from h in db.HORARIO_ATENCION
                              select h).ToList();

                        foreach (var item in ha)
                        {
                            string[] hora = item.Horarios.Split('-');
                            DateTime h = Convert.ToDateTime(hora[0]);
                            if (DateTime.Now.Hour > h.Hour)
                                nueva.Add(item);
                            else if (DateTime.Now.Minute >= h.Minute && DateTime.Now.Hour == h.Hour)
                                nueva.Add(item);
                        }
                    }
                    else //validara todo dentro del dia actual
                    {
                        ha = (from h in db.HORARIO_ATENCION
                              select h).ToList();
                    }
                    foreach (var item in agendaPaciente)
                    {
                        foreach (var x in ha)
                        {
                            if (item.Hora.Trim() == x.Horarios.Trim())
                                nueva.Add(x);
                        }
                    }
                }
                else
                {
                    if (tiempo != "")
                    {
                        ha = (from h in db.HORARIO_ATENCION
                              select h).ToList();
                        foreach (var item in ha)
                        {
                            string[] hora = item.Horarios.Split('-');
                            DateTime h = Convert.ToDateTime(hora[0]);
                            if (DateTime.Now.Hour > h.Hour)
                            {
                                nueva.Add(item);

                            }
                            else if (DateTime.Now.Minute >= h.Minute && DateTime.Now.Hour == h.Hour)
                            {
                                nueva.Add(item);
                            }
                        }
                    }
                    else
                    {
                        ha = (from h in db.HORARIO_ATENCION
                              select h).ToList();
                    }
                }
                if (nueva.Count > 0)
                    ha = ha.Except(nueva).ToList();
                return ha;
            }
        }
        public bool eliminarCita(Int64 codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transaction = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    AGENDAMIENTO ap = db.AGENDAMIENTO.FirstOrDefault(x => x.ID_AGENDAMIENTO == codigo);

                    db.DeleteObject(ap);

                    db.SaveChanges();

                    transaction.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public List<DtoAgendados> Reagendar(string cedula)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                DateTime Hoy = DateTime.Now;
                List<DtoAgendados> agendados = new List<DtoAgendados>();
                var agendado = (from a in db.AGENDA_PACIENTE
                                join ag in db.AGENDAMIENTO on a.ID_AGENDAMIENTO equals ag.ID_AGENDA_PACIENTE
                                where a.Identificacion == cedula && ag.FechaAgenda >= Hoy.Date
                                select new
                                {
                                    Codigo = ag.ID_AGENDAMIENTO,
                                    Medico = ag.Medico,
                                    Especialidad = ag.Especialidad,
                                    Consultorio = ag.Consultorio,
                                    Fecha = ag.FechaAgenda,
                                    Hora = ag.Hora
                                }).ToList();
                foreach (var item in agendado)
                {
                    DtoAgendados a = new DtoAgendados();
                    a.Codigo = item.Codigo;
                    a.Medico = item.Medico;
                    a.Especialidad = item.Especialidad;
                    a.Consultorio = item.Consultorio;
                    a.Fecha = item.Fecha;
                    a.Hora = item.Hora;

                    agendados.Add(a);
                }
                List<DtoAgendados> excluidos = new List<DtoAgendados>();
                foreach (var item in agendados)
                {
                    HORARIO_ATENCION horarios = db.HORARIO_ATENCION.FirstOrDefault(x => x.Horarios == item.Hora);
                    string[] hora = horarios.Horarios.Split('-');
                    DateTime h = Convert.ToDateTime(hora[0]);
                    if (Convert.ToDateTime(item.Fecha).Date <= Hoy.Date)
                    {
                        if (h.Hour <= Hoy.Hour)
                            excluidos.Add(item);
                        else if (h.Minute <= Hoy.Minute && h.Hour == Hoy.Hour)
                            excluidos.Add(item);
                    }
                }
                agendados = agendados.Except(excluidos).ToList();
                return agendados;
            }
        }
        public AGENDAMIENTO RecuperaAgendamiento(Int64 codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                AGENDAMIENTO a = (from ag in db.AGENDAMIENTO
                                  where ag.ID_AGENDAMIENTO == codigo
                                  select ag).FirstOrDefault();
                return a;
            }
        }
        public AGENDA_PACIENTE recuperarPacienteAgendado(string identificacion)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                AGENDA_PACIENTE x = db.AGENDA_PACIENTE.FirstOrDefault(a => a.Identificacion == identificacion);
                return x;
            }
        }
        public HABITACIONES recuperarConsultorio(string num_Nombre)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HABITACIONES x = db.HABITACIONES.FirstOrDefault(a => a.hab_Numero == num_Nombre);
                return x;
            }
        }
        public HORARIO_ATENCION recuperarHorarioID(string horario)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HORARIO_ATENCION x = db.HORARIO_ATENCION.FirstOrDefault(a => a.Horarios.Contains(horario));
                return x;
            }
        }
        public MEDICOS recuperarMedico(string medico)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                string[] med = medico.Split(' ');
                string apellido1, apellido2, nombre1, nombre2;
                apellido1 = med[0].Trim();
                apellido2 = med[1].Trim();
                if (med[2].Trim() != "")
                {
                    nombre1 = med[2].Trim();
                    nombre2 = med[3].Trim();
                }
                else
                {
                    nombre1 = med[3].Trim();
                    nombre2 = med[4].Trim();
                }
                MEDICOS x = db.MEDICOS.Include("ESPECIALIDADES_MEDICAS").FirstOrDefault(a => a.MED_APELLIDO_PATERNO == apellido1 && a.MED_APELLIDO_MATERNO == apellido2 && a.MED_NOMBRE1 == nombre1 && a.MED_NOMBRE2 == nombre2 && a.MED_ESTADO == true);
                return x;
            }
        }
        public SIGNOSVITALES_CONSULTAEXTERNA signosCEX(Int64 ate_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return db.SIGNOSVITALES_CONSULTAEXTERNA.FirstOrDefault(x => x.ATE_CODIGO == ate_codigo);
            }
        }
    }
}
