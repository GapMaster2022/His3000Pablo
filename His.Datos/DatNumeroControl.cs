using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace His.Datos
{
     public class DatNumeroControl
    {
        public int RecuperaMaximoNumeroControl()
        {
            int maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<NUMERO_CONTROL> numerocontrol = contexto.NUMERO_CONTROL.ToList();
                if (numerocontrol.Count > 0)
                    maxim = contexto.NUMERO_CONTROL.Max(emp => emp.CODCON);
                else
                    maxim = 0;
                return maxim;
            }
        }
        public NUMERO_CONTROL RecuperaNumeroControlID(int codigoNumControl)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from n in contexto.NUMERO_CONTROL
                        where n.CODCON == codigoNumControl
                        select n).FirstOrDefault();
            }
        }
        public NUMERO_CONTROL OcuparNControl(int codcon)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                NUMERO_CONTROL numeroControl = new NUMERO_CONTROL();
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transaccion = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    var control = db.NUMERO_CONTROL.FirstOrDefault(x => x.CODCON == codcon);

                    if (!control.OCUPADO)
                    {
                        control.OCUPADO = true;
                        db.SaveChanges();
                        transaccion.Commit();
                        ConexionEntidades.ConexionEDM.Close();
                        return control;
                    }
                    else
                    {
                        transaccion.Rollback();
                        ConexionEntidades.ConexionEDM.Close();
                        return numeroControl;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaccion.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return numeroControl;
                }
            }
        }
        public bool LiberoNControl(int codcon)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transaccion = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    var control = db.NUMERO_CONTROL.FirstOrDefault(x => x.CODCON == codcon);
                    control.OCUPADO = false;
                    int numdoc = Convert.ToInt32(control.NUMCON);
                    control.NUMCON = (numdoc + 1).ToString();
                    db.SaveChanges();
                    transaccion.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    transaccion.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }
        }
        public List<NUMERO_CONTROL> RecuperaNumeroControl()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.NUMERO_CONTROL.ToList();
            }
        }

        public DataTable RecuperaNumeroControlPablo()
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

            Sqlcmd = new SqlCommand("Sp_RecuperaUltimoControl", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
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
            //using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            //{
            //    return contexto.NUMERO_CONTROL.ToList();
            //}
        }


        public void CrearNumeroControl(NUMERO_CONTROL numerocontrol)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("NUMERO_CONTROL", numerocontrol);
            }
        }
        public void GrabarNumeroControl(NUMERO_CONTROL numerocontrolModificada, NUMERO_CONTROL numerocontrolOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(numerocontrolModificada, numerocontrolOriginal);
            }
        }
        public void EliminarNumeroControl(NUMERO_CONTROL numerocontrol)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(numerocontrol);
            }
        }

        public bool CreaControlConsulta( CONTROL_CONSULTA obj)
        {
            using(var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("CONTROL_CONSULTA", obj);
                return true;
            }
        }
    }
}
