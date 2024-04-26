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
    public class DatAnamnesis
    {
        public int ultimoCodigo()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.HC_ANAMNESIS
                             select d.ANE_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }

        public int ultimoCodigoMotivoConsuta()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.HC_ANAMNESIS_MOTIVOS_CONSULTA
                             select d.MOC_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }

        public int ultimoCodigoMujer()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.HC_ANAMNESIS_ANTEC_MUJER
                             select d.AMU_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }

        public void actualizarAnamnesis(HC_ANAMNESIS anamnesis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_ANAMNESIS anamnesisDestino = contexto.HC_ANAMNESIS.FirstOrDefault(a => a.ANE_CODIGO == anamnesis.ANE_CODIGO);
                anamnesisDestino.ANE_FECHA = anamnesis.ANE_FECHA;
                anamnesisDestino.ANE_FREC_CARDIACA = anamnesis.ANE_FREC_CARDIACA;
                anamnesisDestino.ANE_FREC_RESPIRATORIA = anamnesis.ANE_FREC_RESPIRATORIA;
                anamnesisDestino.ANE_PERIMETRO = anamnesis.ANE_PERIMETRO;
                anamnesisDestino.ANE_PESO = anamnesis.ANE_PESO;
                anamnesisDestino.ANE_PLAN_TRATAMIENTO = anamnesis.ANE_PLAN_TRATAMIENTO;
                anamnesisDestino.ANE_PRESION_A = anamnesis.ANE_PRESION_A;
                anamnesisDestino.ANE_PRESION_B = anamnesis.ANE_PRESION_B;
                anamnesisDestino.ANE_PROBLEMA = anamnesis.ANE_PROBLEMA;
                anamnesisDestino.ANE_OTROS_SIGNOS = anamnesis.ANE_OTROS_SIGNOS;
                anamnesisDestino.ANE_ANALISIS = anamnesis.ANE_ANALISIS;
                anamnesisDestino.ANE_TALLA = anamnesis.ANE_TALLA;
                anamnesisDestino.ANE_TEMP_AXILAR = anamnesis.ANE_TEMP_AXILAR;
                anamnesisDestino.ANE_TEMP_BUCAL = anamnesis.ANE_TEMP_BUCAL;
                anamnesisDestino.ANE_FECHA_ULT_ANTIGENO_PROSTATICO = anamnesis.ANE_FECHA_ULT_ANTIGENO_PROSTATICO;
                anamnesisDestino.ANE_FECHA_ULT_ECO_PROSTATICO = anamnesis.ANE_FECHA_ULT_ECO_PROSTATICO;
                anamnesisDestino.ID_USUARIO = anamnesis.ID_USUARIO;
                contexto.SaveChanges();
            }
        }
        

        //public List<HC_ANAMNESIS> listaAnamnesis(int codAnamnesis)
        //{
        //    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //    {
        //        return contexto.HC_ANAMNESIS.Include("HC_ANAMNESIS").Where(n => n.ANE_CODIGO == codAnamnesis).ToList();
        //    }
        //}

        public HC_ANAMNESIS recuperarAnamnesisPorAtencion(int codAtencion)
        {
            HC_ANAMNESIS anamnesis;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                anamnesis = (from d in contexto.HC_ANAMNESIS
                             where d.ATENCIONES.ATE_CODIGO == codAtencion
                             select d).FirstOrDefault();
                return anamnesis;
            }
        }


        public HC_ANAMNESIS_DIAGNOSTICOS recuperarAnamnesisDiagnostico(int CodigoAnamnesis)
        {
            HC_ANAMNESIS_DIAGNOSTICOS Diagnostico;

            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                Diagnostico = (from d in contexto.HC_ANAMNESIS_DIAGNOSTICOS
                               where d.HC_ANAMNESIS.ANE_CODIGO == CodigoAnamnesis
                               select d).FirstOrDefault();
                return Diagnostico;
            }
        }

        /// <summary>
        /// Metodo que retorna los antecedentes de una mujer por codigo de Anamnesis
        /// </summary>
        /// <param name="codAnamnesis">Codigo de Anamnesis</param>
        /// <returns>Objeto tipo HC_ANAMNESIS_ANTEC_MUJER</returns>
        public HC_ANAMNESIS_ANTEC_MUJER recuperarAnamnesisDatosMujer(int codAnamnesis)
        {
            try
            {
                HC_ANAMNESIS_ANTEC_MUJER anamnesisDatosMujer;
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    anamnesisDatosMujer = (from d in contexto.HC_ANAMNESIS_ANTEC_MUJER
                                           where d.HC_ANAMNESIS.ANE_CODIGO == codAnamnesis
                                           select d).FirstOrDefault();
                    return anamnesisDatosMujer;
                }
            }
            catch (Exception err){throw err;}
        }


        public void actualizarAnamnesisDatosMujer(HC_ANAMNESIS_ANTEC_MUJER anamnesisDatosM)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_ANAMNESIS_ANTEC_MUJER anamnesisDestino = contexto.HC_ANAMNESIS_ANTEC_MUJER.FirstOrDefault(a => a.AMU_CODIGO == anamnesisDatosM.AMU_CODIGO);
                anamnesisDestino.AMU_ABORTOS = anamnesisDatosM.AMU_ABORTOS;
                anamnesisDestino.AMU_BIOPSIA = anamnesisDatosM.AMU_BIOPSIA;
                anamnesisDestino.AMU_CESAREAS = anamnesisDatosM.AMU_CESAREAS;
                anamnesisDestino.AMU_CICLOS = anamnesisDatosM.AMU_CICLOS;
                //anamnesisDestino.AMU_CODIGO = anamnesisDatosM.AMU_CODIGO;
                anamnesisDestino.AMU_COLPOSCOPIA = anamnesisDatosM.AMU_COLPOSCOPIA;
                anamnesisDestino.AMU_FUC = anamnesisDatosM.AMU_FUC;
                anamnesisDestino.AMU_FUM = anamnesisDatosM.AMU_FUM;
                anamnesisDestino.AMU_FUP = anamnesisDatosM.AMU_FUP;   
                anamnesisDestino.AMU_GESTA = anamnesisDatosM.AMU_GESTA;
                anamnesisDestino.AMU_HIJOSVIVOS = anamnesisDatosM.AMU_HIJOSVIVOS;
                anamnesisDestino.AMU_MAMOGRAFIA = anamnesisDatosM.AMU_MAMOGRAFIA;
                anamnesisDestino.AMU_MENARQUIA = anamnesisDatosM.AMU_MENARQUIA;
                anamnesisDestino.AMU_EDAD_VIDA_SEXUAL = anamnesisDatosM.AMU_EDAD_VIDA_SEXUAL;
                anamnesisDestino.AMU_FECHA_COLPOSCOPIA = anamnesisDatosM.AMU_FECHA_COLPOSCOPIA;
                anamnesisDestino.AMU_FECHA_MAMOGRAFIA = anamnesisDatosM.AMU_FECHA_MAMOGRAFIA;
                anamnesisDestino.AMU_MENOPAUSIA = anamnesisDatosM.AMU_MENOPAUSIA;
                anamnesisDestino.AMU_MET_PREVENCION = anamnesisDatosM.AMU_MET_PREVENCION;
                anamnesisDestino.AMU_PARTOS = anamnesisDatosM.AMU_PARTOS;
                anamnesisDestino.AMU_TERAPIAHORMONAL = anamnesisDatosM.AMU_TERAPIAHORMONAL;
                anamnesisDestino.AMU_VIDASEXUAL = anamnesisDatosM.AMU_VIDASEXUAL;
                anamnesisDestino.AMU_SCORE_MAMA = anamnesisDatosM.AMU_SCORE_MAMA;
                anamnesisDestino.AMU_CICLOS_DETALLE = anamnesisDatosM.AMU_CICLOS_DETALLE;
                contexto.SaveChanges();                
            }
        }

        public void saveDA(DtoAnamnesis_DA p)
        {
            string cadena_sql;
          
                cadena_sql = "delete from [HC_ANAMNESIS_DATOSADICIONALES] WHERE [ANE_CODIGO]=" + p.ANE_CODIGO + "\n"+
                            "INSERT INTO [dbo].[HC_ANAMNESIS_DATOSADICIONALES] ([ANE_CODIGO] ,[MEDICO]) VALUES(" +
                               p.ANE_CODIGO + ", '" + p.MEDICO.Trim() + "')";
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


        public DtoAnamnesis_DA getDA(int ANE_CODIGO)
        {
            DtoAnamnesis_DA pda = null;
            int r;
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

            Sqlcmd = new SqlCommand("select COUNT(*) from [dbo].[HC_ANAMNESIS_DATOSADICIONALES] WHERE [ANE_CODIGO]=" + ANE_CODIGO + " ", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            r = Convert.ToInt16(Dts.Rows[0][0]);

            if (r > 0)
            {
                DataTable Dts2 = new DataTable();
                Sqlcmd = new SqlCommand("SELECT top 1 * from [dbo].[HC_ANAMNESIS_DATOSADICIONALES] where ANE_CODIGO=" + ANE_CODIGO + " ", Sqlcon);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts2);

                pda = new DtoAnamnesis_DA();

               
                pda.ANE_CODIGO = ANE_CODIGO;
                pda.MEDICO = Convert.ToString(Dts2.Rows[0]["MEDICO"]);
            }

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return pda;



        }

    }
}
