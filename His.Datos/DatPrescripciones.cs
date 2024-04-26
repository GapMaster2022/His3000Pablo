using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using His.Entidades.Reportes;
using System.Data;
using System.Data.SqlClient;

namespace His.Datos
{
    public class DatPrescripciones
    {
        public void crearPrescripcion(HC_PRESCRIPCIONES nPrescripcion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_PRESCRIPCIONES(nPrescripcion);
                contexto.SaveChanges();
            }
        }

        public void editarPrescripcion(HC_PRESCRIPCIONES ePrescripcion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_PRESCRIPCIONES oPrescripcion = contexto.HC_PRESCRIPCIONES.FirstOrDefault(p=>p.PRES_CODIGO==ePrescripcion.PRES_CODIGO);
                oPrescripcion.PRES_ESTADO = ePrescripcion.PRES_ESTADO;
                oPrescripcion.PRES_FARMACOS_INSUMOS = ePrescripcion.PRES_FARMACOS_INSUMOS;
                oPrescripcion.ID_USUARIO = ePrescripcion.ID_USUARIO;
                oPrescripcion.NOM_USUARIO = ePrescripcion.NOM_USUARIO;
                oPrescripcion.PRES_FECHA_ADMINISTRACION = ePrescripcion.PRES_FECHA_ADMINISTRACION;
                contexto.SaveChanges();
            }
        }

        public void EditarIndicaciones(int pres_codigo, string indicaciones)
        {
            SqlConnection conexion;
            SqlCommand command;
            BaseContextoDatos obj = new BaseContextoDatos();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command = new SqlCommand("sp_EditarHc_Prescripcion", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@pres_codigo", pres_codigo);
            command.Parameters.AddWithValue("@indicacion", indicaciones);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }

        public List<HC_PRESCRIPCIONES> listaPrescripciones(Int64 codNotaEvolucion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.HC_PRESCRIPCIONES
                        join n in contexto.HC_EVOLUCION_DETALLE on p.HC_EVOLUCION_DETALLE.EVD_CODIGO equals n.EVD_CODIGO
                        where p.HC_EVOLUCION_DETALLE.EVD_CODIGO == codNotaEvolucion
                        select p).ToList();
            }
        }

        public HC_PRESCRIPCIONES recuperarPrescripcionID(int codPres)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.HC_PRESCRIPCIONES
                       where p.PRES_CODIGO == codPres
                       select p).FirstOrDefault();
            }
        }

        public int ultimoCodigo()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var prescripciones = (from p in contexto.HC_PRESCRIPCIONES
                                      select p.PRES_CODIGO).ToList();
                if (prescripciones.Count > 0)
                    return prescripciones.Max();

                return 0;
            }
        }

        public void IngresaImagen(Int64 CodigoAtencion, byte[] Imagen)
        {
            // Pablo Rocha / 24/02/2014 guarda imagen
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
            Sqlcmd = new SqlCommand("sp_IngresaImagen", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = CodigoAtencion;

            Sqlcmd.Parameters.Add("@IMAGEN", SqlDbType.Image);
            Sqlcmd.Parameters["@IMAGEN"].Value = Imagen;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            Sqlcon.Close();
        }

        public DataTable RegresaImagen(Int64 CodigoAtencion)
        {
            // Pablo Rocha / 25/02/2014 recupera imagen
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
            Sqlcmd = new SqlCommand("sp_RecuperaImagen", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATECODIGO", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ATECODIGO"].Value = CodigoAtencion;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            Sqlcon.Close();

            return Dts;

        }
    }
}
