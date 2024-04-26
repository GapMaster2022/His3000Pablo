using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using His.Entidades.General;
using Microsoft.Data.Extensions;

namespace His.Datos
{
    public class DatPacientes
    {

        //RECURPERA DATOS PARA MOSTRAR EN GRID
        public List<DtoPacientesInfo> ListaPacientesInfo(string tiponivel)
        {
            List<DtoPacientesInfo> medicoc = new List<DtoPacientesInfo>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //List<PACIENTES> pacientes = new List<PACIENTES>();
                if (tiponivel == "1")
                {
                    //pacientes = contexto.PACIENTES.Include("PACIENTES_DATOS_ADICIONALES").OrderBy(cod => cod.PAC_NOMBRE1).OrderBy(co => co.PAC_APELLIDO_MATERNO).OrderBy(c => c.PAC_APELLIDO_PATERNO).ToList();
                    var pacientes = from p in contexto.PACIENTES
                                    join a in contexto.PACIENTES_DATOS_ADICIONALES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                    where p.PAC_CODIGO == a.PACIENTES.PAC_CODIGO && a.DAP_ESTADO == true
                                    orderby p.PAC_NOMBRE1
                                    orderby p.PAC_APELLIDO_MATERNO
                                    orderby p.PAC_APELLIDO_PATERNO
                                    select new
                                    {
                                        p.PAC_CODIGO,
                                        p.PAC_APELLIDO_PATERNO,
                                        p.PAC_APELLIDO_MATERNO,
                                        p.PAC_HISTORIA_CLINICA,
                                        p.PAC_TIPO_IDENTIFICACION,
                                        p.PAC_IDENTIFICACION,
                                        p.PAC_EMAIL,
                                        p.PAC_FECHA_CREACION,
                                        p.PAC_GENERO,
                                        p.PAC_NOMBRE1,
                                        p.PAC_NOMBRE2,
                                        a.DAP_DIRECCION_DOMICILIO,
                                        a.DAP_TELEFONO1,
                                        a.DAP_TELEFONO2
                                    };
                    foreach (var acceso in pacientes)
                    {
                        medicoc.Add(new DtoPacientesInfo()
                        {
                            //ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName(),
                            //ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key,
                            PAC_HISTORIA_CLINICA = acceso.PAC_HISTORIA_CLINICA,
                            PAC_APELLIDO_MATERNO = acceso.PAC_APELLIDO_MATERNO,
                            PAC_APELLIDO_PATERNO = acceso.PAC_APELLIDO_PATERNO,
                            PAC_CEDULA = acceso.PAC_IDENTIFICACION,
                            //PAC_CIVIL = acceso.PAC_CIVIL,
                            PAC_CODIGO = acceso.PAC_CODIGO,
                            PAC_DIRECCION = acceso.DAP_DIRECCION_DOMICILIO,
                            //PAC_EDAD= DateTime.Parse((DateTime.Now.Date- acceso.PAC_FECHA_NACIMIENTO).ToString()),
                            PAC_EMAIL = acceso.PAC_EMAIL,
                            PAC_FECHA_CREACION = (DateTime)acceso.PAC_FECHA_CREACION,
                            PAC_GENERO = acceso.PAC_GENERO,
                            PAC_NOMBRE1 = acceso.PAC_NOMBRE1,
                            PAC_NOMBRE2 = acceso.PAC_NOMBRE2,
                            PAC_TELEFONO = acceso.DAP_TELEFONO1,
                            PAC_TELEFONO2 = acceso.DAP_TELEFONO2
                            //TIP_CODIGO = acceso.TIPO_PACIENTE.TIP_CODIGO

                        });
                    }
                }
                else if (tiponivel == "2")
                {
                    var pacientes = from p in contexto.PACIENTES
                                    join a in contexto.PACIENTES_DATOS_ADICIONALES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                    where p.PAC_CODIGO == a.PACIENTES.PAC_CODIGO && a.DAP_ESTADO == true
                                    orderby p.PAC_NOMBRE1
                                    orderby p.PAC_APELLIDO_MATERNO
                                    orderby p.PAC_APELLIDO_PATERNO
                                    select new
                                    {
                                        p.PAC_CODIGO,
                                        p.PAC_APELLIDO_PATERNO,
                                        p.PAC_APELLIDO_MATERNO,
                                        p.PAC_HISTORIA_CLINICA,
                                        p.PAC_TIPO_IDENTIFICACION,
                                        p.PAC_IDENTIFICACION,
                                        p.PAC_EMAIL,
                                        p.PAC_FECHA_CREACION,
                                        p.PAC_GENERO,
                                        p.PAC_NOMBRE1,
                                        p.PAC_NOMBRE2,
                                        a.DAP_DIRECCION_DOMICILIO,
                                        a.DAP_TELEFONO1,
                                        a.DAP_TELEFONO2
                                    };
                    //List<HONORARIOS_MEDICOS> honorarios = contexto.HONORARIOS_MEDICOS.Include("MEDICOS").Include("ATENCIONES").Include("ATENCIONES.PACIENTES").ToList();
                    var consulta = from h in contexto.HONORARIOS_MEDICOS
                                   join m in contexto.MEDICOS on h.MEDICOS.MED_CODIGO equals m.MED_CODIGO
                                   join a in contexto.ATENCIONES on h.ATE_CODIGO equals a.ATE_CODIGO
                                   join p in pacientes on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                   select new
                                   {
                                       p.DAP_DIRECCION_DOMICILIO,
                                       p.DAP_TELEFONO1,
                                       p.DAP_TELEFONO2,
                                       p.PAC_APELLIDO_MATERNO,
                                       p.PAC_APELLIDO_PATERNO,
                                       p.PAC_CODIGO,
                                       p.PAC_EMAIL,
                                       p.PAC_FECHA_CREACION,
                                       p.PAC_GENERO,
                                       p.PAC_HISTORIA_CLINICA,
                                       p.PAC_IDENTIFICACION,
                                       p.PAC_NOMBRE1,
                                       p.PAC_NOMBRE2,
                                       m.MED_CODIGO
                                   };


                    foreach (var acceso in consulta)
                    {

                        medicoc.Add(new DtoPacientesInfo()
                        {
                            //PAC_HISTORIA_CLINICA = acceso.PAC_HISTORIA_CLINICA,
                            PAC_APELLIDO_MATERNO = acceso.PAC_APELLIDO_MATERNO,
                            PAC_APELLIDO_PATERNO = acceso.PAC_APELLIDO_PATERNO,
                            PAC_CEDULA = acceso.PAC_IDENTIFICACION,
                            //PAC_CIVIL = acceso.PAC_CIVIL,
                            PAC_CODIGO = acceso.PAC_CODIGO,
                            //PAC_CON_SEGURO = acceso.PAC_CON_SEGURO == null ? false : bool.Parse(acceso.PAC_CON_SEGURO.ToString()),
                            PAC_DIRECCION = acceso.DAP_DIRECCION_DOMICILIO,
                            //PAC_EDAD= DateTime.Parse((DateTime.Now.Date- acceso.PAC_FECHA_NACIMIENTO).ToString()),
                            PAC_EMAIL = acceso.PAC_EMAIL,
                            PAC_FECHA_CREACION = (DateTime)acceso.PAC_FECHA_CREACION,
                            PAC_GENERO = acceso.PAC_GENERO,
                            PAC_NOMBRE1 = acceso.PAC_NOMBRE1,
                            PAC_NOMBRE2 = acceso.PAC_NOMBRE2,
                            PAC_TELEFONO = acceso.DAP_TELEFONO1,
                            PAC_TELEFONO2 = acceso.DAP_TELEFONO2,
                            //TIP_CODIGO = acceso.TIPO_PACIENTE.TIP_CODIGO,
                            MED_CODIGO = acceso.MED_CODIGO
                        });

                    }
                }
            }
            return medicoc;
        }

        //. Recupera la lista por defecto de pacientes
        public List<PACIENTES> RecuperarPacientesLista()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PACIENTES
                        select p).ToList();
            }
        }
        public bool EliminarPAciente(Int64 pacCodigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                try
                {
                    PACIENTES pac = (from p in contexto.PACIENTES
                                     where p.PAC_CODIGO == pacCodigo
                                     select p).FirstOrDefault();
                    contexto.DeleteObject(pac);
                    contexto.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {

                    throw;
                }
                
            }
        }
        //. Recupera la lista por defecto de pacientes
        public List<PACIENTES> RecuperarPacientesLista(string id, string historia, string nombre, int cantidad)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<PACIENTES> pacientes = new List<PACIENTES>();


                    if (id == string.Empty && historia == string.Empty && nombre == string.Empty)
                    {
                        var result = from p in contexto.PACIENTES
                                     join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                     where a.ATE_ESTADO == true
                                     orderby a.ATE_FECHA descending
                                     select p;



                        return result.Take(cantidad).ToList();
                    }
                    else
                    {

                        var result = from p in contexto.PACIENTES
                                     select p;

                        if (id != string.Empty)
                            result = result.Where(p => p.PAC_IDENTIFICACION.StartsWith(id));

                        if (historia != string.Empty)
                            result = result.Where(p => p.PAC_HISTORIA_CLINICA.Contains(historia));

                        if (nombre != string.Empty)
                        {
                            string[] cadena = nombre.Split();

                            if (cadena.Length == 1)
                            {
                                string n = cadena[0].Trim();
                                var porApellidoPaterno = result.Where(p => (p.PAC_APELLIDO_PATERNO).StartsWith(n)).OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                                if (porApellidoPaterno.Count == 0)
                                {
                                    var porNombreUno = result.Where(p => (p.PAC_NOMBRE1).StartsWith(n)).OrderBy(p => p.PAC_NOMBRE1).Take(cantidad).ToList();
                                    if (porNombreUno.Count > 0)
                                    {
                                        return porNombreUno;
                                    }
                                }
                                else
                                {
                                    return porApellidoPaterno;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < cadena.Length; i++)
                                {
                                    string n = cadena[i].Trim();
                                    result = result.Where(p => (p.PAC_APELLIDO_PATERNO + p.PAC_APELLIDO_MATERNO + p.PAC_NOMBRE1 + p.PAC_NOMBRE2).Contains(n));
                                }
                            }
                        }

                        return result.OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Recupera la lista de pacientes por tipo
        /// </summary>
        /// <param name="id">numero de identificacion</param>
        /// <param name="historia">numero de historia clinica</param>
        /// <param name="nombre">nombre del paciente</param>
        /// <param name="cantidad">cantidad de registrar a recuperar</param>
        /// <returns>list de objectis PACIENTES</returns>
        public List<PACIENTES> RecuperarPacientesLista(string id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<PACIENTES> pacientes = new List<PACIENTES>();


                    if (id == string.Empty && historia == string.Empty && nombre == string.Empty)
                    {
                        //lista de pacientes con atenciones activas
                        var result = from p in contexto.PACIENTES
                                     join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                     where a.ATE_ESTADO == true && a.TIPO_INGRESO.TIP_CODIGO == tipoCodigo
                                     orderby a.ATE_FECHA descending
                                     select p;
                        return result.Take(cantidad).ToList();
                    }
                    else
                    {

                        var result = from p in contexto.PACIENTES
                                     join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                     where a.TIPO_INGRESO.TIP_CODIGO == tipoCodigo
                                     select p;

                        if (id != string.Empty)
                            result = result.Where(p => p.PAC_IDENTIFICACION.StartsWith(id));

                        if (historia != string.Empty)
                            result = result.Where(p => p.PAC_HISTORIA_CLINICA.Contains(historia));

                        if (nombre != string.Empty)
                        {
                            string[] cadena = nombre.Split();

                            if (cadena.Length == 1)
                            {
                                string n = cadena[0].Trim();
                                var porApellidoPaterno = result.Where(p => (p.PAC_APELLIDO_PATERNO).StartsWith(n)).OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                                if (porApellidoPaterno.Count == 0)
                                {
                                    var porNombreUno = result.Where(p => (p.PAC_NOMBRE1).StartsWith(n)).OrderBy(p => p.PAC_NOMBRE1).Take(cantidad).ToList();
                                    if (porNombreUno.Count > 0)
                                    {
                                        return porNombreUno;
                                    }
                                }
                                else
                                {
                                    return porApellidoPaterno;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < cadena.Length; i++)
                                {
                                    string n = cadena[i].Trim();
                                    result = result.Where(p => (p.PAC_APELLIDO_PATERNO + p.PAC_APELLIDO_MATERNO + p.PAC_NOMBRE1 + p.PAC_NOMBRE2).Contains(n));
                                }
                            }
                        }

                        return result.OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        /// <summary>
        /// Recupera la lista de pacientes por tipo
        /// </summary>
        /// <param name="id">numero de identificacion</param>
        /// <param name="historia">numero de historia clinica</param>
        /// <param name="nombre">nombre del paciente</param>
        /// <param name="cantidad">cantidad de registrar a recuperar</param>
        /// <returns>list de objectis PACIENTES</returns>
        /// 

        public DataTable EstadosHoja()
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

            Sqlcmd = new SqlCommand("sp_VerificaEstadoHoja", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        //Recupera Resposnsable de ingresar la Nota de Evolucion Pablo Rocha 31-05-2020
        public DataTable RecuperaResponsable(Int64 Pac_codigo, string detalle)
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

            Sqlcmd = new SqlCommand("sp_RecuperaResponsable", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@PAC_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@PAC_CODIGO"].Value = Pac_codigo;

            Sqlcmd.Parameters.Add("@DETALLE", SqlDbType.Text);
            Sqlcmd.Parameters["@DETALLE"].Value = detalle;

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
        }

        public List<DtoPacientesEmergencia> RecuperarPacientesListaEmerg(string id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            try
            {
                List<DtoPacientesEmergencia> pacientes = new List<DtoPacientesEmergencia>();
                List<DtoPacientesEmergencia> pacientes1 = new List<DtoPacientesEmergencia>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    if (id == string.Empty && historia == string.Empty && nombre == string.Empty)
                    {
                        //lista de pacientes con atenciones activas
                        var result = (from p in contexto.PACIENTES
                                      join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                      where a.TIPO_INGRESO.TIP_CODIGO == 1 && a.ESC_CODIGO == 1 /*solo muestra tipo de ingresos emergencia*/
                                      orderby a.ATE_FECHA_INGRESO, p.PAC_APELLIDO_PATERNO descending
                                      select new DtoPacientesEmergencia
                                      {
                                          PAC_CODIGO = p.PAC_CODIGO,
                                          PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
                                          PAC_NOMBRE1 = p.PAC_NOMBRE1,
                                          PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                          PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                          PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                          PAC_HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
                                          ATE_CODIGO = a.ATE_CODIGO,
                                          PAC_FECHA_ATENCION = a.ATE_FECHA_INGRESO.Value,
                                          PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO.Value
                                      }).Take(cantidad);


                        return result.ToList();
                        ////pacientes1 = pacientes;
                        //foreach (var item in pacientes)
                        //{
                        //    string valido = EmergenciaEstado(Convert.ToInt64(item.ATE_CODIGO));
                        //    if (valido != "1")
                        //    {
                        //        pacientes1.Add(item);
                        //    }
                        //}
                        //return pacientes1;
                    }
                    else
                    {
                        var result = from p in contexto.PACIENTES
                                     join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                     where a.TIPO_INGRESO.TIP_CODIGO == tipoCodigo && a.TIPO_INGRESO.TIP_CODIGO == 1
                                     orderby a.ATE_FECHA_INGRESO, p.PAC_APELLIDO_PATERNO descending
                                     select new DtoPacientesEmergencia
                                     {
                                         PAC_CODIGO = p.PAC_CODIGO,
                                         PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
                                         PAC_NOMBRE1 = p.PAC_NOMBRE1,
                                         PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                         PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                         PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                         PAC_HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
                                         ATE_CODIGO = a.ATE_CODIGO,
                                         PAC_FECHA_ATENCION = a.ATE_FECHA_INGRESO.Value,
                                         PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO.Value
                                     };

                        if (id != string.Empty)
                            result = result.Where(p => (p.PAC_IDENTIFICACION).StartsWith(id));

                        if (historia != string.Empty)
                            result = result.Where(p => (p.PAC_HISTORIA_CLINICA).Trim().Contains(historia));

                        if (nombre != string.Empty)
                        {
                            string[] cadena = nombre.Split();

                            if (cadena.Length == 1)
                            {
                                string n = cadena[0].Trim();
                                var porApellidoPaterno = result.Where(p => (p.PAC_APELLIDO_PATERNO).StartsWith(n)).OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                                if (porApellidoPaterno.Count == 0)
                                {
                                    var porNombreUno = result.Where(p => (p.PAC_NOMBRE1).StartsWith(n)).OrderBy(p => p.PAC_NOMBRE1).Take(cantidad).ToList();
                                    if (porNombreUno.Count > 0)
                                    {
                                        return porNombreUno;
                                    }
                                }
                                else
                                {
                                    return porApellidoPaterno;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < cadena.Length; i++)
                                {
                                    string n = cadena[i].Trim();
                                    result = result.Where(p => (p.PAC_APELLIDO_PATERNO + p.PAC_APELLIDO_MATERNO + p.PAC_NOMBRE1 + p.PAC_NOMBRE2).Contains(n));
                                }
                            }
                        }
                        return result.OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<DtoPacientesEmergencia> RecuperarPacientesListaSubSecuentes(string id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            List<DtoPacientesEmergencia> pacientes = new List<DtoPacientesEmergencia>();
            List<DtoPacientesEmergencia> pacientes1 = new List<DtoPacientesEmergencia>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                if (id == string.Empty && historia == string.Empty && nombre == string.Empty)
                {
                    //lista de pacientes con atenciones activas
                    var result = (from p in contexto.PACIENTES
                                  join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                  where a.TIPO_INGRESO.TIP_CODIGO == 4 && a.ESC_CODIGO == 6 /*solo muestra tipo de ingresos emergencia*/

                                  orderby a.ATE_FECHA_INGRESO descending
                                  select new DtoPacientesEmergencia
                                  {
                                      PAC_CODIGO = p.PAC_CODIGO,
                                      PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
                                      PAC_NOMBRE1 = p.PAC_NOMBRE1,
                                      PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                      PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                      PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                      PAC_HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
                                      ATE_CODIGO = a.ATE_CODIGO,
                                      PAC_FECHA_ATENCION = a.ATE_FECHA_INGRESO.Value,
                                      PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO.Value
                                  }).Take(cantidad);

                    pacientes = result.ToList();
                    ////pacientes1 = pacientes;
                    //foreach (var item in pacientes)
                    //{
                    //    string valido = EmergenciaEstado(Convert.ToInt64(item.ATE_CODIGO));
                    //    if (valido != "1")
                    //    {
                    //        pacientes1.Add(item);
                    //    }
                    //}
                    return pacientes;
                }
                else
                {
                    var result = from p in contexto.PACIENTES
                                 join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                 where a.TIPO_INGRESO.TIP_CODIGO == 4 && a.ESC_CODIGO == 6
                                 orderby a.ATE_FECHA ascending
                                 select new DtoPacientesEmergencia
                                 {
                                     PAC_CODIGO = p.PAC_CODIGO,
                                     PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
                                     PAC_NOMBRE1 = p.PAC_NOMBRE1,
                                     PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                     PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                     PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                     PAC_HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
                                     ATE_CODIGO = a.ATE_CODIGO,
                                     PAC_FECHA_ATENCION = a.ATE_FECHA_INGRESO.Value,
                                     PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO.Value
                                 };

                    if (id != string.Empty)
                        result = result.Where(p => (p.PAC_IDENTIFICACION).StartsWith(id));

                    if (historia != string.Empty)
                        result = result.Where(p => (p.PAC_HISTORIA_CLINICA).Trim().Contains(historia));

                    if (nombre != string.Empty)
                    {
                        string[] cadena = nombre.Split();

                        if (cadena.Length == 1)
                        {
                            string n = cadena[0].Trim();
                            var porApellidoPaterno = result.Where(p => (p.PAC_APELLIDO_PATERNO).StartsWith(n)).OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                            if (porApellidoPaterno.Count == 0)
                            {
                                var porNombreUno = result.Where(p => (p.PAC_NOMBRE1).StartsWith(n)).OrderBy(p => p.PAC_NOMBRE1).Take(cantidad).ToList();
                                if (porNombreUno.Count > 0)
                                {
                                    return porNombreUno;
                                }
                            }
                            else
                            {
                                return porApellidoPaterno;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < cadena.Length; i++)
                            {
                                string n = cadena[i].Trim();
                                result = result.Where(p => (p.PAC_APELLIDO_PATERNO + p.PAC_APELLIDO_MATERNO + p.PAC_NOMBRE1 + p.PAC_NOMBRE2).Contains(n));
                            }
                        }
                    }
                    return result.OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                }

            }
        }

        public List<DtoPacientesEmergencia> RecuperarPacientesListaEmergTriaje(string id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            try
            {
                List<DtoPacientesEmergencia> pacientes = new List<DtoPacientesEmergencia>();
                List<DtoPacientesEmergencia> pacientes1 = new List<DtoPacientesEmergencia>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    if (id == string.Empty && historia == string.Empty && nombre == string.Empty)
                    {
                        //lista de pacientes con atenciones activas
                        var result = (from p in contexto.PACIENTES
                                      join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                      where a.TIPO_INGRESO.TIP_CODIGO == 1 && a.ESC_CODIGO == 1 /*solo muestra tipo de ingresos emergencia*/

                                      orderby a.ATE_FECHA_INGRESO descending
                                      select new DtoPacientesEmergencia
                                      {
                                          PAC_CODIGO = p.PAC_CODIGO,
                                          PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
                                          PAC_NOMBRE1 = p.PAC_NOMBRE1,
                                          PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                          PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                          PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                          PAC_HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
                                          ATE_CODIGO = a.ATE_CODIGO,
                                          PAC_FECHA_ATENCION = a.ATE_FECHA_INGRESO.Value,
                                          PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO.Value
                                      }).Take(cantidad);

                        pacientes = result.ToList();
                        //pacientes1 = pacientes;
                        foreach (var item in pacientes)
                        {
                            string valido = EmergenciaEstado(Convert.ToInt64(item.ATE_CODIGO));
                            if (valido != "1")
                            {
                                pacientes1.Add(item);
                            }
                        }
                        return pacientes1;

                    }
                    else
                    {
                        var result = from p in contexto.PACIENTES
                                     join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                     where a.TIPO_INGRESO.TIP_CODIGO == tipoCodigo && a.TIPO_INGRESO.TIP_CODIGO == 1
                                     orderby a.ATE_FECHA ascending
                                     select new DtoPacientesEmergencia
                                     {
                                         PAC_CODIGO = p.PAC_CODIGO,
                                         PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
                                         PAC_NOMBRE1 = p.PAC_NOMBRE1,
                                         PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                         PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                         PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                         PAC_HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
                                         ATE_CODIGO = a.ATE_CODIGO,
                                         PAC_FECHA_ATENCION = a.ATE_FECHA_INGRESO.Value,
                                         PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO.Value
                                     };

                        if (id != string.Empty)
                            result = result.Where(p => (p.PAC_IDENTIFICACION).StartsWith(id));

                        if (historia != string.Empty)
                            result = result.Where(p => (p.PAC_HISTORIA_CLINICA).Trim().Contains(historia));

                        if (nombre != string.Empty)
                        {
                            string[] cadena = nombre.Split();

                            if (cadena.Length == 1)
                            {
                                string n = cadena[0].Trim();
                                var porApellidoPaterno = result.Where(p => (p.PAC_APELLIDO_PATERNO).StartsWith(n)).OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                                if (porApellidoPaterno.Count == 0)
                                {
                                    var porNombreUno = result.Where(p => (p.PAC_NOMBRE1).StartsWith(n)).OrderBy(p => p.PAC_NOMBRE1).Take(cantidad).ToList();
                                    if (porNombreUno.Count > 0)
                                    {
                                        return porNombreUno;
                                    }
                                }
                                else
                                {
                                    return porApellidoPaterno;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < cadena.Length; i++)
                                {
                                    string n = cadena[i].Trim();
                                    result = result.Where(p => (p.PAC_APELLIDO_PATERNO + p.PAC_APELLIDO_MATERNO + p.PAC_NOMBRE1 + p.PAC_NOMBRE2).Contains(n));
                                }
                            }
                        }
                        return result.OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public List<DtoPacientesEmergencia> RecuperarPacientesListaTodos(string id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            try
            {
                List<DtoPacientesEmergencia> pacientes = new List<DtoPacientesEmergencia>();
                List<DtoPacientesEmergencia> pacientes1 = new List<DtoPacientesEmergencia>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    if (id == string.Empty && historia == string.Empty && nombre == string.Empty)
                    {
                        //lista de pacientes con atenciones activas
                        var result = (from p in contexto.PACIENTES
                                      join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                      where a.ESC_CODIGO == 1 && a.TIPO_INGRESO.TIP_CODIGO == 4 || a.ESC_CODIGO == 1 && a.TIPO_INGRESO.TIP_CODIGO == 10 || a.ESC_CODIGO == 1 && a.TIPO_INGRESO.TIP_CODIGO == 12 || a.ESC_CODIGO == 1 && a.TIPO_INGRESO.TIP_CODIGO == 1  /*Para usuario admnistrador muestra todos los pacientes con atenciones Activas*/

                                      orderby a.ATE_FECHA_INGRESO descending
                                      select new DtoPacientesEmergencia
                                      {
                                          PAC_CODIGO = p.PAC_CODIGO,
                                          PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
                                          PAC_NOMBRE1 = p.PAC_NOMBRE1,
                                          PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                          PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                          PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                          PAC_HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
                                          ATE_CODIGO = a.ATE_CODIGO,
                                          PAC_FECHA_ATENCION = a.ATE_FECHA_INGRESO.Value,
                                          PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO.Value
                                      }).Take(cantidad);

                        pacientes = result.ToList();
                        //pacientes1 = pacientes;
                        foreach (var item in pacientes)
                        {
                            string valido = EmergenciaEstado(Convert.ToInt64(item.ATE_CODIGO));
                            if (valido != "1")
                            {
                                pacientes1.Add(item);
                            }
                        }
                        return pacientes1;

                    }
                    else
                    {
                        var result = from p in contexto.PACIENTES
                                     join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                     where a.ESC_CODIGO == 1 && a.TIPO_INGRESO.TIP_CODIGO == 4 || a.ESC_CODIGO == 1 && a.TIPO_INGRESO.TIP_CODIGO == 10 || a.ESC_CODIGO == 1 && a.TIPO_INGRESO.TIP_CODIGO == 12 /*Para usuario admnistrador muestra todos los pacientes con atenciones Activas*/
                                     orderby a.ATE_FECHA ascending
                                     select new DtoPacientesEmergencia
                                     {
                                         PAC_CODIGO = p.PAC_CODIGO,
                                         PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
                                         PAC_NOMBRE1 = p.PAC_NOMBRE1,
                                         PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                         PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                         PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                         PAC_HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
                                         ATE_CODIGO = a.ATE_CODIGO,
                                         PAC_FECHA_ATENCION = a.ATE_FECHA_INGRESO.Value,
                                         PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO.Value
                                     };

                        if (id != string.Empty)
                            result = result.Where(p => (p.PAC_IDENTIFICACION).StartsWith(id));

                        if (historia != string.Empty)
                            result = result.Where(p => (p.PAC_HISTORIA_CLINICA).Trim().Contains(historia));

                        if (nombre != string.Empty)
                        {
                            string[] cadena = nombre.Split();

                            if (cadena.Length == 1)
                            {
                                string n = cadena[0].Trim();
                                var porApellidoPaterno = result.Where(p => (p.PAC_APELLIDO_PATERNO).StartsWith(n)).OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                                if (porApellidoPaterno.Count == 0)
                                {
                                    var porNombreUno = result.Where(p => (p.PAC_NOMBRE1).StartsWith(n)).OrderBy(p => p.PAC_NOMBRE1).Take(cantidad).ToList();
                                    if (porNombreUno.Count > 0)
                                    {
                                        return porNombreUno;
                                    }
                                }
                                else
                                {
                                    return porApellidoPaterno;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < cadena.Length; i++)
                                {
                                    string n = cadena[i].Trim();
                                    result = result.Where(p => (p.PAC_APELLIDO_PATERNO + p.PAC_APELLIDO_MATERNO + p.PAC_NOMBRE1 + p.PAC_NOMBRE2).Contains(n));
                                }
                            }
                        }
                        return result.OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public List<DtoPacientesEmergencia> RecuperarPacientesListaConsultaExterna(string id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            try
            {
                List<DtoPacientesEmergencia> pacientes = new List<DtoPacientesEmergencia>();
                List<DtoPacientesEmergencia> pacientes1 = new List<DtoPacientesEmergencia>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    if (id == string.Empty && historia == string.Empty && nombre == string.Empty)
                    {
                        //lista de pacientes con atenciones activas
                        var result = (from p in contexto.PACIENTES
                                      join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                      where a.TIPO_INGRESO.TIP_CODIGO == 4 && a.ESC_CODIGO == 1 /*solo muestra tipo de ingresos consulta etxerna*/

                                      orderby a.ATE_FECHA_INGRESO, p.PAC_APELLIDO_PATERNO descending
                                      select new DtoPacientesEmergencia
                                      {
                                          PAC_CODIGO = p.PAC_CODIGO,
                                          PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
                                          PAC_NOMBRE1 = p.PAC_NOMBRE1,
                                          PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                          PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                          PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                          PAC_HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
                                          ATE_CODIGO = a.ATE_CODIGO,
                                          PAC_FECHA_ATENCION = a.ATE_FECHA_INGRESO.Value,
                                          PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO.Value
                                      }).Take(cantidad);

                        pacientes = result.ToList();
                        //pacientes1 = pacientes;
                        foreach (var item in pacientes)
                        {
                            string valido = EmergenciaEstado(Convert.ToInt64(item.ATE_CODIGO));
                            if (valido != "1")
                            {
                                pacientes1.Add(item);
                            }
                        }
                        return pacientes1;

                    }
                    else
                    {
                        var result = from p in contexto.PACIENTES
                                     join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                     where a.TIPO_INGRESO.TIP_CODIGO == 4 && a.ESC_CODIGO == 1
                                     orderby a.ATE_FECHA_INGRESO, p.PAC_APELLIDO_PATERNO descending
                                     select new DtoPacientesEmergencia
                                     {
                                         PAC_CODIGO = p.PAC_CODIGO,
                                         PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
                                         PAC_NOMBRE1 = p.PAC_NOMBRE1,
                                         PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                         PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                         PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                         PAC_HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
                                         ATE_CODIGO = a.ATE_CODIGO,
                                         PAC_FECHA_ATENCION = a.ATE_FECHA_INGRESO.Value,
                                         PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO.Value
                                     };

                        if (id != string.Empty)
                            result = result.Where(p => (p.PAC_IDENTIFICACION).StartsWith(id));

                        if (historia != string.Empty)
                            result = result.Where(p => (p.PAC_HISTORIA_CLINICA).Trim().Contains(historia));

                        if (nombre != string.Empty)
                        {
                            string[] cadena = nombre.Split();

                            if (cadena.Length == 1)
                            {
                                string n = cadena[0].Trim();
                                var porApellidoPaterno = result.Where(p => (p.PAC_APELLIDO_PATERNO).StartsWith(n)).OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                                if (porApellidoPaterno.Count == 0)
                                {
                                    var porNombreUno = result.Where(p => (p.PAC_NOMBRE1).StartsWith(n)).OrderBy(p => p.PAC_NOMBRE1).Take(cantidad).ToList();
                                    if (porNombreUno.Count > 0)
                                    {
                                        return porNombreUno;
                                    }
                                }
                                else
                                {
                                    return porApellidoPaterno;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < cadena.Length; i++)
                                {
                                    string n = cadena[i].Trim();
                                    result = result.Where(p => (p.PAC_APELLIDO_PATERNO + p.PAC_APELLIDO_MATERNO + p.PAC_NOMBRE1 + p.PAC_NOMBRE2).Contains(n));
                                }
                            }
                        }
                        return result.OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<DtoPacientesEmergencia> RecuperarPacientesListaMushunia(string id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            try
            {
                List<DtoPacientesEmergencia> pacientes = new List<DtoPacientesEmergencia>();
                List<DtoPacientesEmergencia> pacientes1 = new List<DtoPacientesEmergencia>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    if (id == string.Empty && historia == string.Empty && nombre == string.Empty)
                    {
                        //lista de pacientes con atenciones activas
                        var result = (from p in contexto.PACIENTES
                                      join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                      where a.TIPO_INGRESO.TIP_CODIGO == 10 && a.ESC_CODIGO == 1 /*solo muestra tipo de ingresos mushunia*/

                                      orderby a.ATE_FECHA_INGRESO descending
                                      select new DtoPacientesEmergencia
                                      {
                                          PAC_CODIGO = p.PAC_CODIGO,
                                          PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
                                          PAC_NOMBRE1 = p.PAC_NOMBRE1,
                                          PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                          PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                          PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                          PAC_HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
                                          ATE_CODIGO = a.ATE_CODIGO,
                                          PAC_FECHA_ATENCION = a.ATE_FECHA_INGRESO.Value,
                                          PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO.Value
                                      }).Take(cantidad);

                        pacientes = result.ToList();
                        //pacientes1 = pacientes;
                        foreach (var item in pacientes)
                        {
                            string valido = EmergenciaEstado(Convert.ToInt64(item.ATE_CODIGO));
                            if (valido != "1")
                            {
                                pacientes1.Add(item);
                            }
                        }
                        return pacientes1;

                    }
                    else
                    {
                        var result = from p in contexto.PACIENTES
                                     join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                     where a.TIPO_INGRESO.TIP_CODIGO == 10 && a.ESC_CODIGO == 1
                                     orderby a.ATE_FECHA ascending
                                     select new DtoPacientesEmergencia
                                     {
                                         PAC_CODIGO = p.PAC_CODIGO,
                                         PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
                                         PAC_NOMBRE1 = p.PAC_NOMBRE1,
                                         PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                         PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                         PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                         PAC_HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
                                         ATE_CODIGO = a.ATE_CODIGO,
                                         PAC_FECHA_ATENCION = a.ATE_FECHA_INGRESO.Value,
                                         PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO.Value
                                     };

                        if (id != string.Empty)
                            result = result.Where(p => (p.PAC_IDENTIFICACION).StartsWith(id));

                        if (historia != string.Empty)
                            result = result.Where(p => (p.PAC_HISTORIA_CLINICA).Trim().Contains(historia));

                        if (nombre != string.Empty)
                        {
                            string[] cadena = nombre.Split();

                            if (cadena.Length == 1)
                            {
                                string n = cadena[0].Trim();
                                var porApellidoPaterno = result.Where(p => (p.PAC_APELLIDO_PATERNO).StartsWith(n)).OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                                if (porApellidoPaterno.Count == 0)
                                {
                                    var porNombreUno = result.Where(p => (p.PAC_NOMBRE1).StartsWith(n)).OrderBy(p => p.PAC_NOMBRE1).Take(cantidad).ToList();
                                    if (porNombreUno.Count > 0)
                                    {
                                        return porNombreUno;
                                    }
                                }
                                else
                                {
                                    return porApellidoPaterno;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < cadena.Length; i++)
                                {
                                    string n = cadena[i].Trim();
                                    result = result.Where(p => (p.PAC_APELLIDO_PATERNO + p.PAC_APELLIDO_MATERNO + p.PAC_NOMBRE1 + p.PAC_NOMBRE2).Contains(n));
                                }
                            }
                        }
                        return result.OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public List<DtoPacientesEmergencia> RecuperarPacientesListaBrigada(string id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            try
            {
                List<DtoPacientesEmergencia> pacientes = new List<DtoPacientesEmergencia>();
                List<DtoPacientesEmergencia> pacientes1 = new List<DtoPacientesEmergencia>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    if (id == string.Empty && historia == string.Empty && nombre == string.Empty)
                    {
                        //lista de pacientes con atenciones activas
                        var result = (from p in contexto.PACIENTES
                                      join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                      where a.TIPO_INGRESO.TIP_CODIGO == 12 && a.ESC_CODIGO == 1 /*solo muestra tipo de ingresos brigadas*/

                                      orderby a.ATE_FECHA_INGRESO descending
                                      select new DtoPacientesEmergencia
                                      {
                                          PAC_CODIGO = p.PAC_CODIGO,
                                          PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
                                          PAC_NOMBRE1 = p.PAC_NOMBRE1,
                                          PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                          PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                          PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                          PAC_HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
                                          ATE_CODIGO = a.ATE_CODIGO,
                                          PAC_FECHA_ATENCION = a.ATE_FECHA_INGRESO.Value,
                                          PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO.Value
                                      }).Take(cantidad);

                        pacientes = result.ToList();
                        //pacientes1 = pacientes;
                        foreach (var item in pacientes)
                        {
                            string valido = EmergenciaEstado(Convert.ToInt64(item.ATE_CODIGO));
                            if (valido != "1")
                            {
                                pacientes1.Add(item);
                            }
                        }
                        return pacientes1;

                    }
                    else
                    {
                        var result = from p in contexto.PACIENTES
                                     join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                     where a.TIPO_INGRESO.TIP_CODIGO == 12 && a.ESC_CODIGO == 1
                                     orderby a.ATE_FECHA ascending
                                     select new DtoPacientesEmergencia
                                     {
                                         PAC_CODIGO = p.PAC_CODIGO,
                                         PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
                                         PAC_NOMBRE1 = p.PAC_NOMBRE1,
                                         PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                         PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                         PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                         PAC_HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
                                         ATE_CODIGO = a.ATE_CODIGO,
                                         PAC_FECHA_ATENCION = a.ATE_FECHA_INGRESO.Value,
                                         PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO.Value
                                     };

                        if (id != string.Empty)
                            result = result.Where(p => (p.PAC_IDENTIFICACION).StartsWith(id));

                        if (historia != string.Empty)
                            result = result.Where(p => (p.PAC_HISTORIA_CLINICA).Trim().Contains(historia));

                        if (nombre != string.Empty)
                        {
                            string[] cadena = nombre.Split();

                            if (cadena.Length == 1)
                            {
                                string n = cadena[0].Trim();
                                var porApellidoPaterno = result.Where(p => (p.PAC_APELLIDO_PATERNO).StartsWith(n)).OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                                if (porApellidoPaterno.Count == 0)
                                {
                                    var porNombreUno = result.Where(p => (p.PAC_NOMBRE1).StartsWith(n)).OrderBy(p => p.PAC_NOMBRE1).Take(cantidad).ToList();
                                    if (porNombreUno.Count > 0)
                                    {
                                        return porNombreUno;
                                    }
                                }
                                else
                                {
                                    return porApellidoPaterno;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < cadena.Length; i++)
                                {
                                    string n = cadena[i].Trim();
                                    result = result.Where(p => (p.PAC_APELLIDO_PATERNO + p.PAC_APELLIDO_MATERNO + p.PAC_NOMBRE1 + p.PAC_NOMBRE2).Contains(n));
                                }
                            }
                        }
                        return result.OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public List<DtoPacientesEmergencia> PacientesListaEmerg(string id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            try
            {
                //List<DtoPacientesEmergencia> pacientes = new List<DtoPacientesEmergencia>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    if (id == string.Empty && historia == string.Empty && nombre == string.Empty)
                    {
                        //lista de pacientes con atenciones activas
                        var result = (from p in contexto.PACIENTES
                                      join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                      where a.TIPO_INGRESO.TIP_CODIGO == 1 /*solo muestra tipo de ingresos emergencia*/
                                      orderby a.ATE_FECHA_INGRESO descending
                                      select new DtoPacientesEmergencia
                                      {
                                          PAC_CODIGO = p.PAC_CODIGO,
                                          PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
                                          PAC_NOMBRE1 = p.PAC_NOMBRE1,
                                          PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                          PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                          PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                          PAC_HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
                                          ATE_CODIGO = a.ATE_CODIGO,
                                          PAC_FECHA_ATENCION = a.ATE_FECHA_INGRESO.Value,
                                          PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO.Value
                                      }).Take(cantidad);

                        return result.ToList();
                    }
                    else
                    {
                        var result = from p in contexto.PACIENTES
                                     join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                     where a.TIPO_INGRESO.TIP_CODIGO == tipoCodigo && a.TIPO_INGRESO.TIP_CODIGO == 1
                                     orderby a.ATE_FECHA ascending
                                     select new DtoPacientesEmergencia
                                     {
                                         PAC_CODIGO = p.PAC_CODIGO,
                                         PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
                                         PAC_NOMBRE1 = p.PAC_NOMBRE1,
                                         PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                         PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                         PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                         PAC_HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
                                         ATE_CODIGO = a.ATE_CODIGO,
                                         PAC_FECHA_ATENCION = a.ATE_FECHA_INGRESO.Value,
                                         PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO.Value
                                     };

                        if (id != string.Empty)
                            result = result.Where(p => (p.PAC_IDENTIFICACION).StartsWith(id));

                        if (historia != string.Empty)
                            result = result.Where(p => (p.PAC_HISTORIA_CLINICA).Trim().Contains(historia));

                        if (nombre != string.Empty)
                        {
                            string[] cadena = nombre.Split();

                            if (cadena.Length == 1)
                            {
                                string n = cadena[0].Trim();
                                var porApellidoPaterno = result.Where(p => (p.PAC_APELLIDO_PATERNO).StartsWith(n)).OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                                if (porApellidoPaterno.Count == 0)
                                {
                                    var porNombreUno = result.Where(p => (p.PAC_NOMBRE1).StartsWith(n)).OrderBy(p => p.PAC_NOMBRE1).Take(cantidad).ToList();
                                    if (porNombreUno.Count > 0)
                                    {
                                        return porNombreUno;
                                    }
                                }
                                else
                                {
                                    return porApellidoPaterno;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < cadena.Length; i++)
                                {
                                    string n = cadena[i].Trim();
                                    result = result.Where(p => (p.PAC_APELLIDO_PATERNO + p.PAC_APELLIDO_MATERNO + p.PAC_NOMBRE1 + p.PAC_NOMBRE2).Contains(n));
                                }
                            }
                        }
                        return result.OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<PACIENTES> listaPacientesFiltros(string id, string historia, string apellido, string nombre, int cantidad)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PACIENTES> pacientes = new List<PACIENTES>();

                var result = from p in contexto.PACIENTES
                             select p;

                if (id != string.Empty)
                    result = result.Where(p => p.PAC_IDENTIFICACION.StartsWith(id));

                if (historia != string.Empty)
                    result = result.Where(p => p.PAC_HISTORIA_CLINICA.Contains(historia));

                if (apellido != string.Empty)
                    result = result.Where(p => (p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO).Contains(apellido));

                if (nombre != string.Empty)
                    result = result.Where(p => (p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2).Contains(nombre));


                result = result.OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad);
                pacientes = result.ToList();
                return pacientes;
            }
        }

        //. Recupera la lista por defecto de pacientes
        public List<PACIENTES> RecuperarPacientesLista(int desde, int cantidad)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.PACIENTES.OrderByDescending(p => p.PAC_FECHA_CREACION).Skip(desde).Take(cantidad).ToList();
                //return (from p in contexto.PACIENTES
                //        orderby p.PAC_FECHA_CREACION descending
                //        select p).OrderBy(p => p.PAC_APELLIDO_PATERNO).Skip(desde).Take(cantidad)
                //        .ToList();
            }
        }

        /// <summary>
        /// Metodo que recupera un listado de los pacientes que se encuentran ingresados
        /// </summary>
        /// <returns>Listado de objectos DtoPacientesAtencionesActivas </returns>
        public List<DtoPacientesAtencionesActivas> RecuperarPacientesAtencionActiva(string conEpicrisis)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<DtoPacientesAtencionesActivas> pacientes;
                    if (conEpicrisis == null)
                    {
                        pacientes = (from p in contexto.PACIENTES
                                     join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                     join c in contexto.ATENCION_DETALLE_CATEGORIAS on a.ATE_CODIGO equals c.ATENCIONES.ATE_CODIGO
                                     where a.ATE_ESTADO == true && p.PAC_ESTADO == true && c.ADA_ESTADO == true && a.ATE_CIERRE_HC == false
                                     select new DtoPacientesAtencionesActivas
                                     {
                                         codigoHabitacion = a.HABITACIONES.hab_Codigo,
                                         numeroHabitacion = a.HABITACIONES.hab_Numero,
                                         cedula = p.PAC_IDENTIFICACION,
                                         nombrePaciente = (p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2),
                                         historiaClincia = p.PAC_HISTORIA_CLINICA,
                                         numeroAtencion = a.ATE_NUMERO_ATENCION,
                                         sexo = p.PAC_GENERO,
                                         aseguradora = c.CATEGORIAS_CONVENIOS.CAT_NOMBRE,
                                         fechaIngreso = a.ATE_FECHA_INGRESO.Value,

                                         medicoTratante = (a.MEDICOS.MED_APELLIDO_PATERNO + " " + a.MEDICOS.MED_APELLIDO_MATERNO + " " + a.MEDICOS.MED_NOMBRE1 + " " + a.MEDICOS.MED_NOMBRE2),
                                         tipoTratamiento = a.TIPO_TRATAMIENTO.TIA_DESCRIPCION,
                                         diagnosticoInicial = a.ATE_DIAGNOSTICO_INICIAL
                                     }).ToList();
                    }
                    else
                    {
                        if (conEpicrisis == "si")
                        {
                            pacientes = (from p in contexto.PACIENTES
                                         join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                         join c in contexto.ATENCION_DETALLE_CATEGORIAS on a.ATE_CODIGO equals c.ATENCIONES.ATE_CODIGO
                                         join e in contexto.HC_EPICRISIS on p.PAC_CODIGO equals e.PAC_CODIGO
                                         where a.ATE_ESTADO == true && p.PAC_ESTADO == true && c.ADA_ESTADO == true && a.ATE_CIERRE_HC == false
                                         select new DtoPacientesAtencionesActivas
                                         {
                                             codigoHabitacion = a.HABITACIONES.hab_Codigo,
                                             numeroHabitacion = a.HABITACIONES.hab_Numero,

                                             cedula = p.PAC_IDENTIFICACION,
                                             nombrePaciente = (p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2),
                                             historiaClincia = p.PAC_HISTORIA_CLINICA,
                                             numeroAtencion = a.ATE_NUMERO_ATENCION,
                                             sexo = p.PAC_GENERO,
                                             aseguradora = c.CATEGORIAS_CONVENIOS.CAT_NOMBRE,
                                             fechaIngreso = a.ATE_FECHA_INGRESO.Value,

                                             medicoTratante = (a.MEDICOS.MED_APELLIDO_PATERNO + " " + a.MEDICOS.MED_APELLIDO_MATERNO + " " + a.MEDICOS.MED_NOMBRE1 + " " + a.MEDICOS.MED_NOMBRE2),
                                             tipoTratamiento = a.TIPO_TRATAMIENTO.TIA_DESCRIPCION,
                                             diagnosticoInicial = a.ATE_DIAGNOSTICO_INICIAL
                                         }).ToList();
                        }
                        else
                        {
                            List<DtoPacientesAtencionesActivas> listaPacientes = (from p in contexto.PACIENTES
                                                                                  join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                                                                  join c in contexto.ATENCION_DETALLE_CATEGORIAS on a.ATE_CODIGO equals c.ATENCIONES.ATE_CODIGO
                                                                                  where a.ATE_ESTADO == true && p.PAC_ESTADO && c.ADA_ESTADO == true
                                                                                  select new DtoPacientesAtencionesActivas
                                                                                  {
                                                                                      codigoHabitacion = a.HABITACIONES.hab_Codigo,
                                                                                      numeroHabitacion = a.HABITACIONES.hab_Numero,

                                                                                      cedula = p.PAC_IDENTIFICACION,
                                                                                      nombrePaciente = (p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2),
                                                                                      historiaClincia = p.PAC_HISTORIA_CLINICA,
                                                                                      numeroAtencion = a.ATE_NUMERO_ATENCION,
                                                                                      sexo = p.PAC_GENERO,
                                                                                      aseguradora = c.CATEGORIAS_CONVENIOS.CAT_NOMBRE,
                                                                                      fechaIngreso = a.ATE_FECHA_INGRESO.Value,

                                                                                      medicoTratante = (a.MEDICOS.MED_APELLIDO_PATERNO + " " + a.MEDICOS.MED_APELLIDO_MATERNO + " " + a.MEDICOS.MED_NOMBRE1 + " " + a.MEDICOS.MED_NOMBRE2),
                                                                                      tipoTratamiento = a.TIPO_TRATAMIENTO.TIA_DESCRIPCION,
                                                                                      diagnosticoInicial = a.ATE_DIAGNOSTICO_INICIAL
                                                                                  }).ToList();
                            List<string> pacientesConEpicrisis = (from p in contexto.PACIENTES
                                                                  join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                                                  join c in contexto.ATENCION_DETALLE_CATEGORIAS on a.ATE_CODIGO equals c.ATENCIONES.ATE_CODIGO
                                                                  join e in contexto.HC_EPICRISIS on p.PAC_CODIGO equals e.PAC_CODIGO
                                                                  where a.ATE_ESTADO == true && p.PAC_ESTADO && c.ADA_ESTADO == true
                                                                  select p.PAC_HISTORIA_CLINICA
                                                                            ).ToList();
                            return (from p in listaPacientes
                                    where !pacientesConEpicrisis.Contains(p.historiaClincia)
                                    select p).ToList();

                        }

                    }
                    return pacientes;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public DataTable registropaciente(string atencion, int condicion)
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
            Sqlcmd = new SqlCommand("sp_registropaciente", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;


            Sqlcmd.Parameters.Add("@atencion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@atencion"].Value = atencion;

            Sqlcmd.Parameters.Add("@condicion", SqlDbType.Int);
            Sqlcmd.Parameters["@condicion"].Value = condicion;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }
        //RECUPERA FARMACOS PABLO ROCHA 01-06-2020
        public DataTable RecuperaFarmacos(string fecha, string usuario, string detalle)
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
            Sqlcmd = new SqlCommand("sp_RecuperaFarmacos", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;


            Sqlcmd.Parameters.Add("@fecha", SqlDbType.VarChar);
            Sqlcmd.Parameters["@fecha"].Value = fecha;

            Sqlcmd.Parameters.Add("@usuario", SqlDbType.VarChar);
            Sqlcmd.Parameters["@usuario"].Value = usuario;

            Sqlcmd.Parameters.Add("@detalle", SqlDbType.VarChar);
            Sqlcmd.Parameters["@detalle"].Value = detalle;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
            return Dts.Tables["tabla"];
        }

        public DataTable RecuperaLetrasEvolucion()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataReader Sqldap;
            DataTable Tabla = new DataTable();
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
            Sqlcmd = new SqlCommand("LetrasEvolucion", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.CommandTimeout = 180;

            Sqldap = Sqlcmd.ExecuteReader();
            Tabla.Load(Sqldap);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
            return Tabla;
        }

        public DataTable RecuperaResponsable(int evo_codigo)
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
            Sqlcmd = new SqlCommand("sp_RecuperaResponsable", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@detalle", SqlDbType.Int);
            Sqlcmd.Parameters["@detalle"].Value = evo_codigo;

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
            return Dts.Tables["tabla"];
        }

        /// <summary>
        /// Metodo que recupera un listado de los pacientes y sus ultimos ingresos
        /// </summary>
        /// <returns>Listado de objectos DtoPacientesAtencionesActivas </returns>
        public List<DtoPacientesAtencionesActivas> RecuperarPacientesAtencionUltimas(int cantidad, bool? estadoAtencion, int? codAtencion, Int16? codHabitacion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var entityConnection = (EntityConnection)contexto.Connection;
                    DbConnection storeConnection = entityConnection.StoreConnection;
                    DbCommand command = storeConnection.CreateCommand();
                    command.CommandText = "sp_DtoPacientesAtencionesActivas";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("codAtencion", codAtencion));
                    command.Parameters.Add(new SqlParameter("cantidad", cantidad));
                    command.Parameters.Add(new SqlParameter("estadoAtencion", estadoAtencion));
                    command.Parameters.Add(new SqlParameter("codHabitacion", codHabitacion));

                    using (contexto.Connection.CreateConnectionScope())
                    {
                        return command.Materialize(r => new DtoPacientesAtencionesActivas
                        {
                            codigoHabitacion = r.Field<Int16>("codigoHabitacion"),
                            numeroHabitacion = r.Field<string>("numeroHabitacion"),
                            cedula = r.Field<string>("cedula"),
                            nombrePaciente = r.Field<string>("nombrePaciente"),
                            historiaClincia = r.Field<string>("historiaClincia"),
                            numeroAtencion = r.Field<string>("numeroAtencion"),
                            codAtencion = r.Field<int>("codAtencion"),
                            sexo = r.Field<string>("sexo"),
                            aseguradora = r.Field<string>("aseguradora"),
                            fechaIngreso = r.Field<DateTime>("fechaIngreso"),
                            medicoTratante = r.Field<string>("medicoTratante"),
                            tipoTratamiento = r.Field<string>("tipoTratamiento"),
                            diagnosticoInicial = r.Field<string>("diagnosticoInicial")
                        }).ToList();

                    }
                }
            }
            catch (Exception err) { throw err; }
        }

        public List<DtoPacientesAtencionesActivas> RecuperarPacientesAtencionUltimasReporte(int cantidad, bool? estadoAtencion, int? codAtencion, Int16? codHabitacion, int piso)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var entityConnection = (EntityConnection)contexto.Connection;
                    DbConnection storeConnection = entityConnection.StoreConnection;
                    DbCommand command = storeConnection.CreateCommand();
                    command.CommandText = "sp_DtoPacientesAtencionesActivas_1";

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@PISO", cantidad));

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@NIVEL_MAQUINA", piso));

                    using (contexto.Connection.CreateConnectionScope())
                    {
                        return command.Materialize(r => new DtoPacientesAtencionesActivas
                        {
                            codigoHabitacion = r.Field<Int16>("CodigoHabitacion"),
                            numeroHabitacion = r.Field<string>("NumeroHabitacion"),
                            cedula = r.Field<string>("Cedula"),
                            nombrePaciente = r.Field<string>("NombrePaciente"),
                            historiaClincia = r.Field<string>("HistoriaClincia"),
                            numeroAtencion = r.Field<string>("NumeroAtencion"),
                            codAtencion = r.Field<int>("Atencion"),
                            sexo = r.Field<string>("Sexo"),
                            aseguradora = r.Field<string>("Aseguradora"),
                            fechaIngreso = r.Field<DateTime>("FechaIngreso"),
                            medicoTratante = r.Field<string>("MedicoTratante"),
                            tipoTratamiento = r.Field<string>("TipoTratamiento"),
                            diagnosticoInicial = r.Field<string>("DiagnosticoInicial"),
                            DiasHospitalizado = r.Field<int>("DiasHospitalizado"),
                            FechaIngresoString = Convert.ToString(r.Field<DateTime>("FechaIngreso"))
                        }).ToList();

                    }
                }
            }
            catch (Exception err) { throw err; }
        }

        public List<DtoPacientesImagen> RecuperarPacientesImagen()
        {
            List<DtoPacientesImagen> lista = new List<DtoPacientesImagen>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            BaseContextoDatos obj = new BaseContextoDatos();
            try
            {
                conn = obj.ConectarBd();
                cmd = new SqlCommand("sp_Pacientes_con_Imagen", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DtoPacientesImagen objPacienteImagen = new DtoPacientesImagen();
                    objPacienteImagen.Agendamiento = Convert.ToDateTime(dr["Agendamiento"].ToString());
                    objPacienteImagen.Atencion = Convert.ToInt32(dr["Atencion"].ToString());
                    objPacienteImagen.Estado = dr["Estado"].ToString();
                    objPacienteImagen.Habitacion = dr["Hab"].ToString();
                    objPacienteImagen.HC = dr["HC"].ToString();
                    objPacienteImagen.ID = Convert.ToInt32(dr["ID"].ToString());
                    objPacienteImagen.Identificacion = dr["Identificacion"].ToString();
                    objPacienteImagen.Observacion = dr["Observacion"].ToString();
                    objPacienteImagen.Paciente = dr["Paciente"].ToString();
                    objPacienteImagen.Radiologo = dr["Radiologo"].ToString();
                    objPacienteImagen.Tecnologo = dr["Tecnologo"].ToString();
                    objPacienteImagen.Solicitud = Convert.ToDateTime(dr["Solicitud"].ToString());
                    lista.Add(objPacienteImagen);
                }
            }
            catch (Exception ex)
            {
                lista = null;
                Console.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return lista;
            //DataTable Tabla = new DataTable();
            //SqlConnection Sqlcon;
            //SqlCommand Sqlcmd;
            //SqlDataReader Sqldap;
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
            //Sqlcmd = new SqlCommand("sp_Pacientes_con_Imagen", Sqlcon);
            //Sqlcmd.CommandType = CommandType.StoredProcedure;
            //Sqldap = Sqlcmd.ExecuteReader();
            //Tabla.Load(Sqldap);
            //try
            //{
            //    Sqlcon.Close();
            //}
            //catch (Exception ex)
            //{ Console.WriteLine(ex.Message); }
            //return Tabla;
            //try
            //{

            //    using ()
            //    {
            //        SqlConnection conexion = 
            //        DbConnection storeConnection = entityConnection.StoreConnection;
            //        DbCommand command = storeConnection.CreateCommand();
            //        DataTable Tabla = new DataTable();
            //        SqlDataReader reader;
            //        command.CommandText = "sp_Pacientes_con_Imagen";

            //        using (contexto.Connection.CreateConnectionScope())
            //        {
            //            reader = command.ExecuteReader();
            //            //return command.Materialize(r => new DtoPacientesImagen
            //            //{
            //            //    Habitacion = r.Field<string>("Hab"),
            //            //    ID = r.Field<int>("ID"),
            //            //    Agendamiento = r.Field<DateTime>("Agendamiento"),
            //            //    Solicitud = r.Field<DateTime>("Solicitud"),
            //            //    Atencion = r.Field<int>("Atencion"),
            //            //    HC = r.Field<string>("HC"),
            //            //    Paciente = r.Field<string>("Paciente"),
            //            //    Identificacion = r.Field<string>("Identificacion"),
            //            //    Radiologo = r.Field<string>("Radiologo"),
            //            //    Tecnologo = r.Field<string>("Tecnologo"),
            //            //    Observacion = r.Field<string>("Observacion"), 
            //            //    //Estado = r.Field<string>("Estado")
            //            //}).ToList();

            //        }
            //    }
            //}
            //catch (Exception err) { throw err; }
        }
        public List<RecuperaDietetica> RecuperaDietetica(string historiaClinica)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var entityConnection = (EntityConnection)contexto.Connection;
                    DbConnection storeConnection = entityConnection.StoreConnection;
                    DbCommand command = storeConnection.CreateCommand();
                    command.CommandText = "sp_RecuperaDieteticaDetalle";

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@historiaClinica", historiaClinica));

                    using (contexto.Connection.CreateConnectionScope())
                    {
                        return command.Materialize(r => new RecuperaDietetica
                        {
                            fechaPedido = Convert.ToString(r.Field<DateTime>("Fecha")),
                            detallePedido = r.Field<string>("Detalle"),
                            observacionPedido = r.Field<string>("Observacion")
                        }).ToList();

                    }
                }
            }
            catch (Exception err) { throw err; }
        }


        /// <summary>
        /// Metodo que recupera un listado de las atenciones de los pacientes
        /// </summary>
        /// <returns>Listado de objectos DtoPacientesAtenciones </returns>
        public List<ATENCIONES_PACIENTES_VISTAS_DETALLE> RecuperarPacientesAtenciones(DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<ATENCIONES_PACIENTES_VISTAS_DETALLE> atenciones;
                    string tempFechaIni = fechaIni.ToString(Parametros.GeneralPAR.FormatoDateTime);
                    fechaFin = fechaFin.AddDays(1);
                    string tempFechaFin = fechaFin.ToString(Parametros.GeneralPAR.FormatoDateTime);

                    atenciones = contexto.RecuperarListaAtenciones(tempFechaIni, tempFechaFin, null, null, null, null, null).ToList();
                    //if (fechaIni == null || fechaFin == null)
                    //{
                    //    pacientes = (from a in contexto.ATENCIONES
                    //                  join p in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO  
                    //                  //join c in contexto.ATENCION_DETALLE_CATEGORIAS on a.ATE_CODIGO equals c.ATENCIONES.ATE_CODIGO
                    //                  join u in contexto.USUARIOS on a.USUARIOS.ID_USUARIO equals u.ID_USUARIO
                    //                  select new DtoPacientesAtenciones
                    //                  {
                    //                      codigoHabitacion = a.HABITACIONES.hab_Codigo,
                    //                      numeroHabitacion = a.HABITACIONES.hab_Numero,
                    //                      cedula = p.PAC_IDENTIFICACION,
                    //                      nombrePaciente = (p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2),
                    //                      historiaClincia = p.PAC_HISTORIA_CLINICA,
                    //                      numeroAtencion = a.ATE_NUMERO_ATENCION,
                    //                      sexo = p.PAC_GENERO,
                    //                      aseguradora = null,
                    //                      fechaIngreso = a.ATE_FECHA_INGRESO.Value,

                    //                      medicoTratante = (a.MEDICOS.MED_APELLIDO_PATERNO + " " + a.MEDICOS.MED_APELLIDO_MATERNO + " " + a.MEDICOS.MED_NOMBRE1 + " " + a.MEDICOS.MED_NOMBRE2),
                    //                      tipoTratamiento = a.TIPO_TRATAMIENTO.TIA_DESCRIPCION,
                    //                      diagnosticoInicial = a.ATE_DIAGNOSTICO_INICIAL,
                    //                      usuario = u.APELLIDOS + " " + u.NOMBRES  
                    //                  }).ToList();
                    //}
                    //else
                    //{

                    //    pacientes = (from a in contexto.ATENCIONES.Include("ATENCION_DETALLE_CATEGORIAS")
                    //                 join p in contexto.PACIENTES on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                    //                 join u in contexto.USUARIOS on a.USUARIOS.ID_USUARIO equals u.ID_USUARIO
                    //                 where   a.ATE_FECHA_INGRESO >= fechaIni && a.ATE_FECHA_INGRESO <= fechaFin
                    //                 select new DtoPacientesAtenciones
                    //                 {
                    //                     codigoHabitacion = a.HABITACIONES.hab_Codigo,
                    //                     numeroHabitacion = a.HABITACIONES.hab_Numero,
                    //                     cedula = p.PAC_IDENTIFICACION,
                    //                     nombrePaciente = (p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2),
                    //                     historiaClincia = p.PAC_HISTORIA_CLINICA,
                    //                     numeroAtencion = a.ATE_NUMERO_ATENCION,
                    //                     sexo = p.PAC_GENERO,
                    //                     aseguradora = null,
                    //                     fechaIngreso = a.ATE_FECHA_INGRESO.Value,

                    //                     medicoTratante = (a.MEDICOS.MED_APELLIDO_PATERNO + " " + a.MEDICOS.MED_APELLIDO_MATERNO + " " + a.MEDICOS.MED_NOMBRE1 + " " + a.MEDICOS.MED_NOMBRE2),
                    //                     tipoTratamiento = a.TIPO_TRATAMIENTO.TIA_DESCRIPCION,
                    //                     diagnosticoInicial = a.ATE_DIAGNOSTICO_INICIAL,
                    //                     usuario = u.APELLIDOS + " " + u.NOMBRES 
                    //                 }).ToList();
                    //}

                    return atenciones;

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        /// <summary>
        /// Método que recupera un listado de las atenciones de los pacientes según Tipo de Ingreso
        /// </summary>
        /// <returns>Listado de objectos DtoPacientesAtenciones </returns>
        public List<ATENCIONES_PACIENTES_VISTAS_DETALLE> RecuperarPacientesAtenciones(DateTime fechaIni, DateTime fechaFin, string codTipoIngreso)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<ATENCIONES_PACIENTES_VISTAS_DETALLE> atenciones;
                    string tempFechaIni = fechaIni.ToString(Parametros.GeneralPAR.FormatoDateTime);
                    string tempFechaFin = fechaFin.ToString(Parametros.GeneralPAR.FormatoDateTime);
                    atenciones = contexto.RecuperarListaAtenciones(tempFechaIni, tempFechaFin, null, null, null, null, null).ToList();
                    List<ATENCIONES_PACIENTES_VISTAS_DETALLE> listaAtenciones = new List<ATENCIONES_PACIENTES_VISTAS_DETALLE>();
                    for (int i = 0; i < atenciones.Count; i++)
                    {
                        ATENCIONES_PACIENTES_VISTAS_DETALLE atencion = new ATENCIONES_PACIENTES_VISTAS_DETALLE();
                        atencion = atenciones.ElementAt(i);
                        if (atencion.TIP_DESCRIPCION == codTipoIngreso)
                            listaAtenciones.Add(atencion);
                    }
                    //p => (p.PAC_APELLIDO_PATERNO + p.PAC_APELLIDO_MATERNO + p.PAC_NOMBRE1 + p.PAC_NOMBRE2

                    return listaAtenciones;

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }




        //. Recupera la lista por defecto de pacientes
        public List<PACIENTES> RecuperarPacientesLista(Int16 codigoTipoTratamiento)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var pacientes = from p in contexto.PACIENTES
                                join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                where a.ATE_ESTADO == true && a.TIPO_TRATAMIENTO.TIA_CODIGO == codigoTipoTratamiento
                                select p;
                return (List<PACIENTES>)pacientes.ToList();
            }
        }
        //. Recupera la lista por defecto de pacientes
        public List<PACIENTES_VISTA> RecuperarPacientesLista(DateTime fechaCreacionIni, DateTime fechaCreacionFin,
            DateTime fechaIngresoIni, DateTime fechaIngresoFin, DateTime fechaAltaIni, DateTime fechaAltaFin, bool atencionActiva, string codigoMedico, string codigoAseguradoraEmpresa)
        {
            try
            {
                List<PACIENTES_VISTA> pac = new List<PACIENTES_VISTA>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    if (fechaCreacionIni != Convert.ToDateTime("01/01/0001 00:00:00"))
                    {
                        pac = (from p in contexto.PACIENTES_VISTA
                               join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                               where p.PAC_FECHA_CREACION >= fechaCreacionIni && p.PAC_FECHA_CREACION <= fechaCreacionFin
                               && a.ATE_ESTADO == atencionActiva
                               orderby p.NOMBRE
                               select p).ToList();

                    }
                    else if (fechaIngresoIni != Convert.ToDateTime("01/01/0001 00:00:00"))
                    {
                        pac = (from p in contexto.PACIENTES_VISTA
                               join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                               where a.ATE_ESTADO == atencionActiva &&
                               (a.ATE_FECHA_INGRESO >= fechaIngresoIni && a.ATE_FECHA_INGRESO <= fechaIngresoFin)
                               orderby p.NOMBRE
                               select p).ToList();
                    }
                    else if (fechaAltaIni != Convert.ToDateTime("01/01/0001 00:00:00"))
                    {
                        pac = (from p in contexto.PACIENTES_VISTA
                               join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                               where a.ATE_ESTADO == atencionActiva &&
                               (a.ATE_FECHA_ALTA >= fechaAltaIni && a.ATE_FECHA_ALTA <= fechaAltaFin)
                               orderby p.NOMBRE
                               select p).ToList();
                    }
                    return pac;
                    //return contexto.RecuperarListaPacientes(fechaCreacionIni, fechaCreacionFin,
                    //fechaIngresoIni, fechaIngresoFin, fechaAltaIni, fechaAltaFin, atencionActiva, codigoMedico, codigoAseguradoraEmpresa).ToList();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        //. Recupera el listado de pacientes y su codigo
        public List<KeyValuePair<int, string>> RecuperarPacientesNombresLista()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<KeyValuePair<int, string>> listaPacientes = new List<KeyValuePair<int, string>>();
                var pacienteQuery = from p in contexto.PACIENTES
                                    where p.PAC_ESTADO == true
                                    select new { p.PAC_CODIGO, Nombre = p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2 };
                listaPacientes.Add(new KeyValuePair<int, string>(0, "Todos"));
                foreach (var paciente in pacienteQuery)
                {
                    listaPacientes.Add(new KeyValuePair<int, string>(paciente.PAC_CODIGO, paciente.Nombre));
                }
                return listaPacientes;
            }
        }

        //. Recupera el numero mayor del codigo de pacientes
        public int RecuperaMaximoPacienteCodigo()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from p in contexto.PACIENTES
                             select p.PAC_CODIGO).ToList();
                if (lista.Count > 0)
                    return lista.Max();

                return 0;

            }

        }
        //. Recupera el numero mayor de historia clinica del pacientes
        //public Int64 RecuperaMaximoPacienteHistoriaClinica()
        //{
        //    string maxim;
        //    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //    {
        //        List<PACIENTES> paciente = new List<PACIENTES>();
        //        paciente = contexto.PACIENTES.ToList();
        //        if (paciente.Count > 0)
        //            maxim = contexto.PACIENTES.Max(loc => loc.PAC_HISTORIA_CLINICA);
        //        else
        //            maxim = "0";
        //        return Convert.ToInt64(maxim);
        //    }

        //}

        public DataTable RecuperaMaximoPacienteHistoriaClinica()
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

            Sqlcmd = new SqlCommand("sp_ReduperaMaximoHC", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];
        }

        public DataTable RecuperaMaximoPacienteNumeroAtencion()
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

            Sqlcmd = new SqlCommand("sp_ReduperaMaximo", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];
        }

        //public Int64 RecuperaMaximoPacienteNumeroAtencion()
        //{
        //    Int64 maxim;
        //    Int64 maximFinal = 0;
        //    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //    {
        //        List<ATENCIONES> atencion = new List<ATENCIONES>();
        //        atencion = contexto.ATENCIONES.ToList();
        //        if (atencion.Count > 0)
        //        {
        //            maxim = contexto.ATENCIONES.Max(loc => Int64.Parse(loc.ATE_NUMERO_ATENCION));
        //            maximFinal = Convert.ToInt64(maxim);
        //            if (maximFinal < 1000)
        //                maximFinal = 1000;

        //        }
        //        else
        //            maxim = 0;
        //        return maximFinal;
        //    }

        //}

        public void CrearPaciente(PACIENTES paciente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PACIENTES contador = (from f in contexto.PACIENTES
                                      where f.PAC_HISTORIA_CLINICA == paciente.PAC_HISTORIA_CLINICA
                                      select f).FirstOrDefault();
                if (contador == null)
                {
                    contexto.AddToPACIENTES(paciente);
                    contexto.SaveChanges();
                }
                else
                {
                    DataTable numHC = new DataTable();
                    numHC = RecuperaMaximoPacienteHistoriaClinica();
                    string numcontrol = numHC.Rows[0][0].ToString();
                    paciente.PAC_HISTORIA_CLINICA = numcontrol;
                    contexto.AddToPACIENTES(paciente);
                    contexto.SaveChanges();
                }
            }
        }

        public DataTable CrearPacienteSP(PACIENTES paciente)
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

            Sqlcmd = new SqlCommand("sp_DatosPacienteSimplificada", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@PAC_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@PAC_CODIGO"].Value = paciente.PAC_CODIGO + 1;

            Sqlcmd.Parameters.Add("@PAC_HISTORIA_CLINICA", SqlDbType.NChar);
            Sqlcmd.Parameters["@PAC_HISTORIA_CLINICA"].Value = paciente.PAC_HISTORIA_CLINICA;

            Sqlcmd.Parameters.Add("@ID_USUARIO", SqlDbType.SmallInt);
            Sqlcmd.Parameters["@ID_USUARIO"].Value = paciente.USUARIOSReference.EntityKey.EntityKeyValues[0].Value;

            Sqlcmd.Parameters.Add("@DIPO_CODIINEC", SqlDbType.VarChar);
            Sqlcmd.Parameters["@DIPO_CODIINEC"].Value = paciente.DIPO_CODIINEC;

            Sqlcmd.Parameters.Add("@E_CODIGO", SqlDbType.SmallInt);
            Sqlcmd.Parameters["@E_CODIGO"].Value = 3;

            Sqlcmd.Parameters.Add("@PAC_FECHA_CREACION", SqlDbType.DateTime);
            Sqlcmd.Parameters["@PAC_FECHA_CREACION"].Value = paciente.PAC_FECHA_CREACION;

            Sqlcmd.Parameters.Add("@PAC_NOMBRE1", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PAC_NOMBRE1"].Value = paciente.PAC_NOMBRE1;

            Sqlcmd.Parameters.Add("@PAC_NOMBRE2", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PAC_NOMBRE2"].Value = paciente.PAC_NOMBRE2;

            Sqlcmd.Parameters.Add("@PAC_APELLIDO_PATERNO", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PAC_APELLIDO_PATERNO"].Value = paciente.PAC_APELLIDO_PATERNO;

            Sqlcmd.Parameters.Add("@PAC_APELLIDO_MATERNO", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PAC_APELLIDO_MATERNO"].Value = paciente.PAC_APELLIDO_MATERNO;

            Sqlcmd.Parameters.Add("@PAC_FECHA_NACIMIENTO", SqlDbType.DateTime);
            Sqlcmd.Parameters["@PAC_FECHA_NACIMIENTO"].Value = paciente.PAC_FECHA_NACIMIENTO;

            Sqlcmd.Parameters.Add("@PAC_NACIONALIDAD", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PAC_NACIONALIDAD"].Value = paciente.PAC_NACIONALIDAD;

            Sqlcmd.Parameters.Add("@PAC_TIPO_IDENTIFICACION", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PAC_TIPO_IDENTIFICACION"].Value = paciente.PAC_TIPO_IDENTIFICACION;

            Sqlcmd.Parameters.Add("@PAC_IDENTIFICACION", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PAC_IDENTIFICACION"].Value = paciente.PAC_IDENTIFICACION;

            Sqlcmd.Parameters.Add("@PAC_EMAIL", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PAC_EMAIL"].Value = paciente.PAC_EMAIL;

            Sqlcmd.Parameters.Add("@PAC_GENERO", SqlDbType.Char);
            Sqlcmd.Parameters["@PAC_GENERO"].Value = paciente.PAC_GENERO;

            Sqlcmd.Parameters.Add("@PAC_IMAGEN", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PAC_IMAGEN"].Value = "";

            Sqlcmd.Parameters.Add("@PAC_ESTADO", SqlDbType.Bit);
            Sqlcmd.Parameters["@PAC_ESTADO"].Value = paciente.PAC_ESTADO;

            Sqlcmd.Parameters.Add("@PAC_DIRECTORIO", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PAC_DIRECTORIO"].Value = "";

            Sqlcmd.Parameters.Add("@PAC_REFERENTE_NOMBRE", SqlDbType.NChar);
            Sqlcmd.Parameters["@PAC_REFERENTE_NOMBRE"].Value = paciente.PAC_REFERENTE_NOMBRE;

            Sqlcmd.Parameters.Add("@PAC_REFERENTE_PARENTESCO", SqlDbType.NChar);
            Sqlcmd.Parameters["@PAC_REFERENTE_PARENTESCO"].Value = paciente.PAC_REFERENTE_PARENTESCO;

            Sqlcmd.Parameters.Add("@PAC_REFERENTE_TELEFONO", SqlDbType.NChar);
            Sqlcmd.Parameters["@PAC_REFERENTE_TELEFONO"].Value = paciente.PAC_REFERENTE_TELEFONO;

            Sqlcmd.Parameters.Add("@PAC_ALERGIAS", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PAC_ALERGIAS"].Value = paciente.PAC_ALERGIAS;

            Sqlcmd.Parameters.Add("@PAC_OBSERVACIONES", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PAC_OBSERVACIONES"].Value = paciente.PAC_OBSERVACIONES;

            Sqlcmd.Parameters.Add("@GS_CODIGO", SqlDbType.SmallInt);
            Sqlcmd.Parameters["@GS_CODIGO"].Value = 0;

            Sqlcmd.Parameters.Add("@PAC_REFERENTE_DIRECCION", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PAC_REFERENTE_DIRECCION"].Value = paciente.PAC_REFERENTE_DIRECCION;

            Sqlcmd.Parameters.Add("@PAC_DATOS_INCOMPLETOS", SqlDbType.Bit);
            Sqlcmd.Parameters["@PAC_DATOS_INCOMPLETOS"].Value = paciente.PAC_DATOS_INCOMPLETOS;
            
            Sqlcmd.Parameters.Add("@PAC_ESTADO_NIVEDU", SqlDbType.Bit);
            Sqlcmd.Parameters["@PAC_ESTADO_NIVEDU"].Value = paciente.PAC_DATOS_INCOMPLETOS;
            
            Sqlcmd.Parameters.Add("@PAC_TIP_EMPRESA", SqlDbType.Bit);
            Sqlcmd.Parameters["@PAC_TIP_EMPRESA"].Value = paciente.PAC_DATOS_INCOMPLETOS;
            
            Sqlcmd.Parameters.Add("@PAC_SEG_SALUD", SqlDbType.Bit);
            Sqlcmd.Parameters["@PAC_SEG_SALUD"].Value = paciente.PAC_DATOS_INCOMPLETOS;
            
            Sqlcmd.Parameters.Add("@PAC_TIP_BONO", SqlDbType.Bit);
            Sqlcmd.Parameters["@PAC_TIP_BONO"].Value = paciente.PAC_DATOS_INCOMPLETOS;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];
        }


        public void ActualizarPacienteAtencion(PACIENTES paciente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    PACIENTES pacienteDestino = contexto.PACIENTES.FirstOrDefault(h => h.PAC_CODIGO == paciente.PAC_CODIGO);
                    pacienteDestino.PAC_NOMBRE1 = paciente.PAC_NOMBRE1;
                    pacienteDestino.PAC_NOMBRE2 = paciente.PAC_NOMBRE2;
                    pacienteDestino.PAC_APELLIDO_PATERNO = paciente.PAC_APELLIDO_PATERNO;
                    pacienteDestino.PAC_APELLIDO_MATERNO = paciente.PAC_APELLIDO_MATERNO;
                    pacienteDestino.PAC_IDENTIFICACION = paciente.PAC_IDENTIFICACION;
                    pacienteDestino.PAC_FECHA_NACIMIENTO = paciente.PAC_FECHA_NACIMIENTO;
                    pacienteDestino.PAC_DIRECTORIO = paciente.PAC_DIRECTORIO;
                    pacienteDestino.PAC_EMAIL = paciente.PAC_EMAIL;
                    pacienteDestino.PAC_REFERENTE_DIRECCION = paciente.PAC_REFERENTE_DIRECCION;
                    pacienteDestino.PAC_REFERENTE_TELEFONO = paciente.PAC_REFERENTE_TELEFONO;
                    contexto.SaveChanges();
                    transac.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                }
                catch (Exception ex)
                {
                    transac.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    Console.WriteLine(ex.Message);
                }
                
            }
        }

        public DtoPacientes RecuperarDtoPacienteID(int codPaciente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                var pacientes = (from p in contexto.PACIENTES
                                 join a in contexto.PACIENTES_DATOS_ADICIONALES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                 where a.DAP_ESTADO == true && p.PAC_CODIGO == codPaciente
                                 select new
                                 {
                                     p.PAC_CODIGO,
                                     p.PAC_APELLIDO_PATERNO,
                                     p.PAC_APELLIDO_MATERNO,
                                     p.PAC_HISTORIA_CLINICA,
                                     p.PAC_TIPO_IDENTIFICACION,
                                     p.PAC_IDENTIFICACION,
                                     p.PAC_EMAIL,
                                     p.PAC_FECHA_CREACION,
                                     p.PAC_GENERO,
                                     p.PAC_NOMBRE1,
                                     p.PAC_NOMBRE2,
                                     a.DAP_DIRECCION_DOMICILIO,
                                     a.DAP_TELEFONO1,
                                     a.DAP_TELEFONO2
                                 }).FirstOrDefault();

                DtoPacientes paciente = new DtoPacientes();

                if (pacientes != null)
                {

                    paciente.PAC_CODIGO = pacientes.PAC_CODIGO;
                    paciente.PAC_APELLIDO_PATERNO = pacientes.PAC_APELLIDO_PATERNO;
                    paciente.PAC_APELLIDO_MATERNO = pacientes.PAC_APELLIDO_MATERNO;
                    paciente.PAC_HISTORIA_CLINICA = pacientes.PAC_HISTORIA_CLINICA;
                    paciente.PAC_IDENTIFICACION = pacientes.PAC_IDENTIFICACION;
                    paciente.PAC_EMAIL = pacientes.PAC_EMAIL;
                    paciente.PAC_FECHA_CREACION = (DateTime)pacientes.PAC_FECHA_CREACION;
                    paciente.PAC_GENERO = pacientes.PAC_GENERO;
                    paciente.PAC_NOMBRE1 = pacientes.PAC_NOMBRE1;
                    paciente.PAC_NOMBRE2 = pacientes.PAC_NOMBRE2;
                    paciente.PAC_DIRECCION = pacientes.DAP_DIRECCION_DOMICILIO;
                    paciente.PAC_TELEFONO = pacientes.DAP_TELEFONO1;
                    paciente.PAC_TELEFONO2 = pacientes.DAP_TELEFONO2;
                }
                else
                {
                    paciente = null;
                }


                return paciente;
            }


        }

        public DtoPacientes RecuperarDtoPacienteID(string historiaClinica)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                var pacientes = (from p in contexto.PACIENTES
                                 join a in contexto.PACIENTES_DATOS_ADICIONALES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                 join e in contexto.ESTADO_CIVIL on a.ESTADO_CIVIL.ESC_CODIGO equals e.ESC_CODIGO
                                 join g in contexto.GRUPO_SANGUINEO on p.GRUPO_SANGUINEO.GS_CODIGO equals g.GS_CODIGO
                                 join r in contexto.ETNIA on p.ETNIA.E_CODIGO equals r.E_CODIGO
                                 where a.DAP_ESTADO == true && p.PAC_HISTORIA_CLINICA == historiaClinica
                                 select new
                                 {
                                     p.PAC_CODIGO,
                                     p.PAC_APELLIDO_PATERNO,
                                     p.PAC_APELLIDO_MATERNO,
                                     p.PAC_HISTORIA_CLINICA,
                                     p.PAC_TIPO_IDENTIFICACION,
                                     p.PAC_IDENTIFICACION,
                                     p.PAC_EMAIL,
                                     p.PAC_FECHA_NACIMIENTO,
                                     p.PAC_FECHA_CREACION,
                                     p.PAC_GENERO,
                                     p.PAC_NOMBRE1,
                                     p.PAC_NOMBRE2,
                                     g.GS_NOMBRE,
                                     r.E_NOMBRE,
                                     p.PAC_NACIONALIDAD,
                                     a.DAP_CODIGO,
                                     a.DAP_ESTADO,
                                     a.DAP_FECHA_INGRESO,
                                     a.DAP_INSTRUCCION,
                                     a.DAP_OCUPACION,
                                     //a.DAP_ZONA_URBANA,
                                     a.DAP_DIRECCION_DOMICILIO,
                                     a.DAP_TELEFONO1,
                                     a.DAP_TELEFONO2,
                                     e.ESC_CODIGO,
                                     e.ESC_NOMBRE
                                 }).FirstOrDefault();

                DtoPacientes paciente = new DtoPacientes();

                if (pacientes != null)
                {

                    paciente.PAC_CODIGO = pacientes.PAC_CODIGO;
                    paciente.PAC_APELLIDO_PATERNO = pacientes.PAC_APELLIDO_PATERNO;
                    paciente.PAC_APELLIDO_MATERNO = pacientes.PAC_APELLIDO_MATERNO;
                    paciente.PAC_HISTORIA_CLINICA = pacientes.PAC_HISTORIA_CLINICA;
                    paciente.PAC_IDENTIFICACION = pacientes.PAC_IDENTIFICACION;
                    paciente.PAC_EMAIL = pacientes.PAC_EMAIL;
                    paciente.PAC_FECHA_CREACION = (DateTime)pacientes.PAC_FECHA_CREACION;
                    paciente.PAC_GENERO = pacientes.PAC_GENERO;
                    paciente.PAC_NOMBRE1 = pacientes.PAC_NOMBRE1;
                    paciente.PAC_NOMBRE2 = pacientes.PAC_NOMBRE2;
                    paciente.PAC_DIRECCION = pacientes.DAP_DIRECCION_DOMICILIO;
                    paciente.PAC_TELEFONO = pacientes.DAP_TELEFONO1;
                    paciente.PAC_TELEFONO2 = pacientes.DAP_TELEFONO2;
                    paciente.PAC_FECHA_NACIMIENTO = Convert.ToDateTime(pacientes.PAC_FECHA_NACIMIENTO);
                    paciente.PAC_GRUPOSANQUINEO = pacientes.GS_NOMBRE;
                    paciente.PAC_GENERO = pacientes.PAC_GENERO;
                    paciente.PAC_ETNIA = pacientes.E_NOMBRE;
                    paciente.PAC_NACIONALIDAD = pacientes.PAC_NACIONALIDAD;
                    paciente.PAC_INSTRUCCION = pacientes.DAP_INSTRUCCION;
                    paciente.PAC_OCUPACION = pacientes.DAP_OCUPACION;
                    //paciente.PAC_ZONA_URBANA = pacientes.DAP_ZONA_URBANA;
                    paciente.ESC_CODIGO = pacientes.ESC_CODIGO;

                }
                else
                {
                    paciente = null;
                }


                return paciente;
            }
        }

        public PACIENTES RecuperarPacienteID(Int64 codPaciente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PACIENTES
                        join a in contexto.PACIENTES_DATOS_ADICIONALES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                        //join g in contexto.GRUPO_SANGUINEO on p.GRUPO_SANGUINEO.GS_CODIGO equals g.GS_CODIGO
                        //join e in contexto.ETNIA on p.ETNIA.E_CODIGO equals e.E_CODIGO
                        where p.PAC_CODIGO == a.PACIENTES.PAC_CODIGO && a.DAP_ESTADO == true && p.PAC_CODIGO == codPaciente
                        select p).FirstOrDefault();
            }


        }
        public PACIENTES RecuperarPacienteCedula(string codPaciente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PACIENTES
                        where p.PAC_IDENTIFICACION == codPaciente
                        select p).FirstOrDefault();
            }
        }
        public PACIENTES RecuperarPacienteID(string historiaClinica)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PACIENTES
                            //join a in contexto.PACIENTES_DATOS_ADICIONALES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                        join g in contexto.GRUPO_SANGUINEO on p.GRUPO_SANGUINEO.GS_CODIGO equals g.GS_CODIGO
                        join e in contexto.ETNIA on p.ETNIA.E_CODIGO equals e.E_CODIGO
                        where p.PAC_HISTORIA_CLINICA == historiaClinica
                        select p).FirstOrDefault();
            }

        }

        public List<DtoPacientes> listaPacientes()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<DtoPacientes> dtoPacientes = new List<DtoPacientes>();
                var listaPacientes = from p in contexto.PACIENTES
                                     join a in contexto.PACIENTES_DATOS_ADICIONALES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                     join e in contexto.ESTADO_CIVIL on a.ESTADO_CIVIL.ESC_CODIGO equals e.ESC_CODIGO
                                     join g in contexto.GRUPO_SANGUINEO on p.GRUPO_SANGUINEO.GS_CODIGO equals g.GS_CODIGO
                                     join r in contexto.ETNIA on p.ETNIA.E_CODIGO equals r.E_CODIGO
                                     where a.DAP_ESTADO == true
                                     select new
                                     {
                                         p.PAC_HISTORIA_CLINICA,
                                         p.PAC_CODIGO,
                                         p.PAC_APELLIDO_PATERNO,
                                         p.PAC_APELLIDO_MATERNO,
                                         p.PAC_TIPO_IDENTIFICACION,
                                         p.PAC_IDENTIFICACION,
                                         p.PAC_EMAIL,
                                         p.PAC_FECHA_NACIMIENTO,
                                         p.PAC_FECHA_CREACION,
                                         p.PAC_GENERO,
                                         p.PAC_NOMBRE1,
                                         p.PAC_NOMBRE2,
                                         g.GS_NOMBRE,
                                         r.E_NOMBRE,
                                         p.PAC_NACIONALIDAD,
                                         a.DAP_CODIGO,
                                         a.DAP_ESTADO,
                                         a.DAP_FECHA_INGRESO,
                                         a.DAP_INSTRUCCION,
                                         a.DAP_OCUPACION,
                                         //a.DAP_ZONA_URBANA,
                                         a.DAP_DIRECCION_DOMICILIO,
                                         a.DAP_TELEFONO1,
                                         a.DAP_TELEFONO2,
                                         e.ESC_CODIGO,
                                         e.ESC_NOMBRE
                                     };



                if (listaPacientes != null)
                {
                    foreach (var pacientes in listaPacientes)
                    {
                        dtoPacientes.Add(new DtoPacientes()
                        {
                            PAC_HISTORIA_CLINICA = pacientes.PAC_HISTORIA_CLINICA,
                            PAC_CODIGO = pacientes.PAC_CODIGO,
                            PAC_APELLIDO_PATERNO = pacientes.PAC_APELLIDO_PATERNO,
                            PAC_APELLIDO_MATERNO = pacientes.PAC_APELLIDO_MATERNO,
                            PAC_IDENTIFICACION = pacientes.PAC_IDENTIFICACION,
                            PAC_EMAIL = pacientes.PAC_EMAIL,
                            PAC_FECHA_CREACION = (DateTime)pacientes.PAC_FECHA_CREACION,
                            PAC_GENERO = pacientes.PAC_GENERO,
                            PAC_NOMBRE1 = pacientes.PAC_NOMBRE1,
                            PAC_NOMBRE2 = pacientes.PAC_NOMBRE2,
                            PAC_DIRECCION = pacientes.DAP_DIRECCION_DOMICILIO,
                            PAC_TELEFONO = pacientes.DAP_TELEFONO1,
                            PAC_TELEFONO2 = pacientes.DAP_TELEFONO2,
                            PAC_FECHA_NACIMIENTO = Convert.ToDateTime(pacientes.PAC_FECHA_NACIMIENTO),
                            PAC_GRUPOSANQUINEO = pacientes.GS_NOMBRE,
                            PAC_ETNIA = pacientes.E_NOMBRE,
                            PAC_NACIONALIDAD = pacientes.PAC_NACIONALIDAD,
                            PAC_INSTRUCCION = pacientes.DAP_INSTRUCCION,
                            PAC_OCUPACION = pacientes.DAP_OCUPACION,
                            //PAC_ZONA_URBANA = pacientes.DAP_ZONA_URBANA,
                            ESC_CODIGO = pacientes.ESC_CODIGO
                        });
                    }

                }
                else
                {
                    dtoPacientes = null;
                }


                return dtoPacientes;
            }

        }

        public int ultimoCodigoPacientes()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from p in contexto.PACIENTES
                             select p.PAC_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();
                return 0;
            }
        }

        public void EditarPaciente(PACIENTES pacienteModificado)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PACIENTES paciente = contexto.PACIENTES.FirstOrDefault(p => p.PAC_CODIGO == pacienteModificado.PAC_CODIGO);
                paciente.PAC_HISTORIA_CLINICA = pacienteModificado.PAC_HISTORIA_CLINICA;
                paciente.PAC_APELLIDO_MATERNO = pacienteModificado.PAC_APELLIDO_MATERNO;
                paciente.PAC_APELLIDO_PATERNO = pacienteModificado.PAC_APELLIDO_PATERNO;
                paciente.ETNIAReference.EntityKey = pacienteModificado.ETNIAReference.EntityKey;
                paciente.PAC_FECHA_NACIMIENTO = pacienteModificado.PAC_FECHA_NACIMIENTO;
                paciente.PAC_GENERO = pacienteModificado.PAC_GENERO;
                paciente.GRUPO_SANGUINEOReference.EntityKey = pacienteModificado.GRUPO_SANGUINEOReference.EntityKey;
                paciente.PAC_IDENTIFICACION = pacienteModificado.PAC_IDENTIFICACION;
                paciente.PAC_NACIONALIDAD = pacienteModificado.PAC_NACIONALIDAD;
                paciente.PAC_NOMBRE1 = pacienteModificado.PAC_NOMBRE1;
                paciente.PAC_NOMBRE2 = pacienteModificado.PAC_NOMBRE2;
                paciente.PAC_EMAIL = pacienteModificado.PAC_EMAIL;
                paciente.DIPO_CODIINEC = pacienteModificado.DIPO_CODIINEC;
                //paciente.DIPO_CODIINEC = pacienteModificado.DIPO_CODIINEC;
                paciente.PAC_TIPO_IDENTIFICACION = pacienteModificado.PAC_TIPO_IDENTIFICACION;
                paciente.USUARIOSReference.EntityKey = pacienteModificado.USUARIOSReference.EntityKey;
                paciente.PAC_DIRECTORIO = pacienteModificado.PAC_DIRECTORIO;
                paciente.PAC_ALERGIAS = pacienteModificado.PAC_ALERGIAS;
                paciente.PAC_REFERENTE_NOMBRE = pacienteModificado.PAC_REFERENTE_NOMBRE;
                paciente.PAC_REFERENTE_PARENTESCO = pacienteModificado.PAC_REFERENTE_PARENTESCO;
                paciente.PAC_REFERENTE_TELEFONO = pacienteModificado.PAC_REFERENTE_TELEFONO;
                paciente.PAC_REFERENTE_DIRECCION = pacienteModificado.PAC_REFERENTE_DIRECCION;
                contexto.SaveChanges();

                PACIENTES obj = contexto.PACIENTES.FirstOrDefault(p => p.PAC_CODIGO == pacienteModificado.PAC_CODIGO);


                List<Form002MSP> pac = new List<Form002MSP>();
                pac = (from p in contexto.Form002MSP
                       where p.Historia == obj.PAC_HISTORIA_CLINICA
                       select p).ToList();
                if (pac != null)
                {
                    foreach (var item in pac)
                    {
                        item.Nombre = obj.PAC_NOMBRE1 + " " + obj.PAC_NOMBRE2;
                        item.Apellido = obj.PAC_APELLIDO_PATERNO + " " + obj.PAC_APELLIDO_MATERNO;
                        contexto.SaveChanges();

                    }
                }
            }
        }

        public List<PACIENTES> listaPacientesFiltros(string id, string historia, string apellido, string nombre)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PACIENTES> pacientes = new List<PACIENTES>();

                var result = from p in contexto.PACIENTES
                             select p;

                if (id != string.Empty)
                    result = result.Where(p => p.PAC_IDENTIFICACION.StartsWith(id));

                if (historia != string.Empty)
                    result = result.Where(p => p.PAC_HISTORIA_CLINICA.Contains(historia));

                if (apellido != string.Empty)
                    result = result.Where(p => (p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO).Contains(apellido));

                if (nombre != string.Empty)
                    result = result.Where(p => (p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2).Contains(nombre));

                result = result.OrderBy(p => p.PAC_APELLIDO_PATERNO);
                pacientes = result.ToList();
                return pacientes;
            }
        }

        public bool existeHCL(string HCL)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PACIENTES pac = (from p in contexto.PACIENTES
                                 where p.PAC_HISTORIA_CLINICA == HCL
                                 select p).FirstOrDefault();
                if (pac != null)
                    return true;

                return false;
            }
        }

        public PACIENTES pacientePorIdentificacion(string id)
        {
            PACIENTES objPaciente = new PACIENTES();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return objPaciente = (from p in contexto.PACIENTES
                                      where p.PAC_IDENTIFICACION == id
                                      select p).FirstOrDefault();

            }
        }

        public PACIENTES recuperarPacientePorAtencion(Int64 codAtencion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PACIENTES
                        join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                        where a.ATE_CODIGO == codAtencion
                        select p).FirstOrDefault();

            }
        }

        SqlConnection Sqlcon;
        SqlCommand Sqlcmd;
        SqlDataAdapter Sqldap;
        DataSet Dts;
        public DataTable PacienteJire(string cedula)
        {
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
            string sql = "select * from PACIENTES_JIRE where CEDULA = '" + cedula + "'";

            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "atenciones");
            return Dts.Tables["atenciones"];

        }

        public DataTable RecuperaResultadosImagen(Int64 ateCodigo)
        {
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
            string sql = "select CUE_DETALLE from CUENTAS_PACIENTES c \n" +
                         "inner join HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS i on c.CUE_CODIGO = i.CUE_CODIGO \n" +
                         "where c.ATE_CODIGO = " + ateCodigo;

            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "atenciones");
            return Dts.Tables["atenciones"];

        }

        public DataTable BuscaPacienteJire()
        {
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
            string sql = "select HC HCL, CONCAT(apellido1,' ', apellido2, ' ', nombre1, ' ', nombre2) as NOMBRE, cedula as ID from PACIENTES_JIRE order by 1 asc";

            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "atenciones");
            return Dts.Tables["atenciones"];

        }

        public DataTable BuscaPacienteEmergencia()
        {
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
            string sql = "select p.PAC_HISTORIA_CLINICA HCL, CONCAT(p.PAC_APELLIDO_PATERNO, ' ', p.PAC_APELLIDO_MATERNO, ' ', p.PAC_NOMBRE1, ' ', p.PAC_NOMBRE2) NOMBRE," +
                "p.PAC_IDENTIFICACION ID, a.ATE_CODIGO from ATENCIONES a INNER JOIN PACIENTES p ON a.PAC_CODIGO = p.PAC_CODIGO INNER JOIN HC_EMERGENCIA_FORM h ON a.ATE_CODIGO = h.ATE_CODIGO " +
                "where a.TIP_CODIGO=1 and a.ESC_CODIGO=1 and h.EMER_ESTADO = 1 UNION select p.PAC_HISTORIA_CLINICA HCL, CONCAT(p.PAC_APELLIDO_PATERNO, ' ', p.PAC_APELLIDO_MATERNO," +
                " ' ', p.PAC_NOMBRE1, ' ', p.PAC_NOMBRE2) NOMBRE,p.PAC_IDENTIFICACION ID, a.ATE_CODIGO from ATENCIONES a INNER JOIN ATENCIONES_REINGRESO ar on a.ATE_CODIGO = ar.ATE_CODIGO_REING " +
                "INNER JOIN PACIENTES p ON a.PAC_CODIGO = p.PAC_CODIGO where a.TIP_CODIGO=1 and a.ESC_CODIGO=1 ORDER BY NOMBRE";

            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "atenciones");
            return Dts.Tables["atenciones"];

        }

        public DataTable BuscaPacienteJireParametro(string hc, string nombre, string cedula)
        {
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
            string where = "";
            if (hc != "")
            {
                where = "where hc like ('%" + hc + "%')";
            }
            else if (cedula != "")
            {
                where = "where cedula like ('%" + cedula + "%')";
            }
            else if (nombre != "")
            {
                where = "where CONCAT(apellido1,' ', apellido2, ' ', nombre1, ' ', nombre2) like ('%" + nombre + "%')";
            }
            else
            {
                where = "order by 2 asc";
            }
            string sql = "select distinct HC HCL, CONCAT(apellido1,' ', apellido2, ' ', nombre1, ' ', nombre2) as NOMBRE, cedula as ID from PACIENTES_JIRE " + where;

            Sqlcmd = new SqlCommand(sql, Sqlcon);
            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "atenciones");
            return Dts.Tables["atenciones"];

        }


        public List<PACIENTES> recuperarListaPacientesPedidos(int estado, string busqPedido, string desde, string hasta)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                if (isNumeric(busqPedido) && desde != null && hasta != null)
                {
                    int codPedido = Convert.ToInt32(busqPedido);
                    DateTime fdesde = Convert.ToDateTime(desde);
                    DateTime fhasta = Convert.ToDateTime(hasta);

                    return (from p in contexto.PACIENTES
                            join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                            join ped in contexto.PEDIDOS on a.ATE_CODIGO equals ped.ATE_CODIGO
                            where ped.PED_ESTADO == estado
                            && ped.PED_CODIGO == codPedido
                            && ped.PED_FECHA >= fdesde
                            && ped.PED_FECHA <= fhasta
                            select p).Distinct().ToList();
                }
                else if (isNumeric(busqPedido))
                {
                    int codPedido = Convert.ToInt32(busqPedido);

                    return (from p in contexto.PACIENTES
                            join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                            join ped in contexto.PEDIDOS on a.ATE_CODIGO equals ped.ATE_CODIGO
                            where ped.PED_ESTADO == estado
                            && ped.PED_CODIGO == codPedido
                            select p).Distinct().ToList();
                }
                else if (desde != null && hasta != null)
                {
                    DateTime fdesde = Convert.ToDateTime(desde);
                    DateTime fhasta = Convert.ToDateTime(hasta);

                    return (from p in contexto.PACIENTES
                            join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                            join ped in contexto.PEDIDOS on a.ATE_CODIGO equals ped.ATE_CODIGO
                            where ped.PED_ESTADO == estado
                            && ped.PED_FECHA >= fdesde
                            && ped.PED_FECHA <= fhasta
                            select p).Distinct().ToList();
                }

                return (from p in contexto.PACIENTES
                        join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                        join ped in contexto.PEDIDOS on a.ATE_CODIGO equals ped.ATE_CODIGO
                        where ped.PED_ESTADO == estado
                        select p).Distinct().ToList();
            }
        }

        //valida si un string es numerico
        public static bool isNumeric(object value)
        {
            try
            {
                double d = System.Double.Parse(value.ToString(), System.Globalization.NumberStyles.Any);
                return true;
            }
            catch (System.FormatException)
            {
                return false;
            }
        }

        public string RecuperarAseguradora(int codPaciente)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var entityConnection = (EntityConnection)contexto.Connection;
                    DbConnection storeConnection = entityConnection.StoreConnection;
                    DbCommand command = storeConnection.CreateCommand();
                    command.CommandText = "sp_oper_recuperar_AseEmpre";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("codPaciente", codPaciente));

                    string codAseguradora;
                    using (contexto.Connection.CreateConnectionScope())
                    {
                        codAseguradora = command.Materialize(r => r.Field<string>("resultado")).FirstOrDefault();
                    }
                    return codAseguradora;
                }
            }
            catch (Exception err) { throw err; }
        }
        public string RecuperarTarifario(int codAseguradora)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var entityConnection = (EntityConnection)contexto.Connection;
                    DbConnection storeConnection = entityConnection.StoreConnection;
                    DbCommand command = storeConnection.CreateCommand();
                    command.CommandText = "sp_oper_recuperar_tarifario";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@codAseguradora", codAseguradora));

                    string codtarifario;
                    using (contexto.Connection.CreateConnectionScope())
                    {
                        codtarifario = command.Materialize(r => r.Field<string>("resultado")).FirstOrDefault();
                    }
                    return codtarifario;
                }
            }
            catch (Exception err) { throw err; }
        }

        public List<PACIENTES_VISTA> recuperarPacientePorHistoria(string HistoriaClinica)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PACIENTES_VISTA
                        where p.PAC_HISTORIA_CLINICA == HistoriaClinica
                        select p).ToList();
            }
        }

        public DataTable getAtencionesIngresos(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fhc, int hc, bool Festado, bool divididas)
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
            string xWhere = "";
            int count = 0;
            if (ingreso)
            {
                xWhere += "  A.ATE_FECHA_INGRESO BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (alta)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  A.ATE_FECHA_ALTA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (facturacion)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  A.ATE_FACTURA_FECHA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (count > 0)
                xWhere = " (" + xWhere + ") ";


            if (Fingreso)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIP_CODIGO=" + Cod_Ingreso + " ";
                count++;
            }

            if (Ftratamiento)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIA_CODIGO=" + Cod_Tratamiento + " ";
                count++;
            }

            if (Fhc)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  P.PAC_HISTORIA_CLINICA=" + hc + " ";
                count++;
            }

            if (Festado)
            {
                xWhere += " AND ";
                xWhere += "A.ATE_CODIGO in (select ate_codigo from STATUS_ATENCION) ";
            }
            if (divididas)
            {
                //xWhere += " AND ";
                //xWhere += "A.ATE_FACTURA_PACIENTE is not null order by A.ATE_NUMERO_ATENCION ";
                xWhere += "order by A.ATE_NUMERO_ATENCION desc";
            }
            if (count > 0)
                xWhere = "Where " + xWhere;

            Sqlcmd = new SqlCommand("SELECT A.ATE_CODIGO as CODIGO, A.HAB_CODIGO, H.hab_Numero AS HAB, A.PAC_CODIGO, P.PAC_IDENTIFICACION AS IDENTIFICACION, P.PAC_APELLIDO_PATERNO + SPACE(1) + P.PAC_APELLIDO_MATERNO + SPACE(1) + P.PAC_NOMBRE1 + SPACE(1) "
            + " + P.PAC_NOMBRE2 AS PACIENTE, P.PAC_HISTORIA_CLINICA AS HC, A.ATE_NUMERO_ATENCION AS 'Nº ATENCION', P.PAC_GENERO AS GENERO, CC.CAT_NOMBRE AS SEGURO, A.TIF_OBSERVACION AS OBSERVACION, CONVERT(int, ROUND(DATEDIFF(hour, P.PAC_FECHA_NACIMIENTO, A.ATE_FECHA_INGRESO) / 8766.0, 0)) AS EDAD,convert(varchar,A.ATE_FECHA_INGRESO,20) AS 'F. INGRESO',convert(varchar,A.ATE_FECHA_ALTA,20) AS 'F. ALTA', "
            + " A.ATE_FACTURA_FECHA AS 'F. FACTURA', A.ATE_FACTURA_PACIENTE AS FACTURA, A.MED_CODIGO, M.MED_APELLIDO_PATERNO + SPACE(1) + M.MED_APELLIDO_MATERNO + SPACE(1) + M.MED_NOMBRE1 + SPACE(1) "
            + " + M.MED_NOMBRE2 AS MEDICO, TT.TIA_DESCRIPCION AS 'PROCEDIMIENTO', A.ATE_DIAGNOSTICO_INICIAL AS 'DIAG. INICIAL', U.APELLIDOS + SPACE(1) + U.NOMBRES AS USUARIO, TIP.TIP_DESCRIPCION AS 'T. INGRESO', TIA.name AS 'T. ATENCION', ADS.ADS_ASEGURADO_NOMBRE AS 'ASEGURADO',"
            + " ADS.ADS_ASEGURADO_CEDULA AS 'ASEGURADO IDENTIFICACION', AIP.ANI_DESCRIPCION AS PARENTESCO, ADC.ADA_AUTORIZACION AS AUTORIZACION, AI.ANI_DESCRIPCION AS TIPO_SEGURO, AID.ANI_DESCRIPCION AS DEPENDENCIA,"
            + " HE.HES_NOMBRE AS ESTADO, REF.TIR_NOMBRE AS REFERIDO, p.PAC_EMAIL as 'CORREO',(SELECT TOP 1 DAP_TELEFONO2 FROM PACIENTES_DATOS_ADICIONALES  WHERE PAC_CODIGO = P.PAC_CODIGO)as 'TELEFONO' "
                         + "           FROM            dbo.ANEXOS_IESS AS AIP RIGHT OUTER JOIN\n"
                         + "                            dbo.ATENCIONES_DETALLE_SEGUROS AS ADS ON AIP.ANI_CODIGO = ADS.ADS_ASEGURADO_PARENTESCO RIGHT OUTER JOIN\n"
                         + "                            dbo.ATENCIONES AS A INNER JOIN\n"
                         + "                            dbo.TIPO_REFERIDO AS REF ON A.TIR_CODIGO=REF.TIR_CODIGO INNER JOIN"
                         + "                            dbo.PACIENTES AS P ON P.PAC_CODIGO = A.PAC_CODIGO LEFT OUTER JOIN\n"
                         + "                            dbo.TIPO_INGRESO AS TIP ON TIP.TIP_CODIGO = A.TIP_CODIGO LEFT OUTER JOIN\n"
                         + "                            dbo.MEDICOS AS M ON A.MED_CODIGO = M.MED_CODIGO LEFT OUTER JOIN\n"
                         + "                            dbo.HABITACIONES_ESTADO AS HE INNER JOIN\n"
                         + "                            dbo.HABITACIONES AS H ON HE.HES_CODIGO = H.HES_CODIGO ON A.HAB_CODIGO = H.hab_Codigo LEFT OUTER JOIN\n"
                         + "                            dbo.USUARIOS AS U ON U.ID_USUARIO = A.ID_USUSARIO LEFT OUTER JOIN\n"
                         + "                            dbo.TIPO_TRATAMIENTO AS TT ON TT.TIA_CODIGO = A.TIA_CODIGO LEFT OUTER JOIN\n"
                         + "                            dbo.ATENCION_DETALLE_CATEGORIAS AS ADC ON ADC.ATE_CODIGO = A.ATE_CODIGO LEFT OUTER JOIN\n"
                          + "                           dbo.CATEGORIAS_CONVENIOS AS CC ON CC.CAT_CODIGO = ADC.CAT_CODIGO LEFT OUTER JOIN\n"
                          + "                           dbo.ANEXOS_IESS AS AI ON ADC.HCC_CODIGO_TS = AI.ANI_CODIGO LEFT OUTER JOIN\n"
                          + "                           dbo.ANEXOS_IESS AS AID ON ADC.HCC_CODIGO_DE = AID.ANI_CODIGO ON ADS.ATE_CODIGO = A.ATE_CODIGO\n" +
                          "                             LEFT JOIN dbo.tipos_atenciones AS TIA ON A.TipoAtencion = TIA.id "
                    + xWhere, Sqlcon);
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
        public DataTable getAtencionesCierre(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fhc, int hc)
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
            string xWhere = "";
            int count = 0;
            if (ingreso)
            {
                xWhere += "  A.ATE_FECHA_INGRESO BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (alta)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  A.ATE_FECHA_ALTA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (facturacion)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  A.ATE_FACTURA_FECHA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (count > 0)
                xWhere = " (" + xWhere + ") ";


            if (Fingreso)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIP_CODIGO=" + Cod_Ingreso + " ";
                count++;
            }

            if (Ftratamiento)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIA_CODIGO=" + Cod_Tratamiento + " ";
                count++;
            }

            if (Fhc)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  U.ID_USUARIO =" + hc + " ";
                count++;
            }

            if (count > 0)
                xWhere = "Where " + xWhere;

            Sqlcmd = new SqlCommand("SELECT   ISNULL((select top 1 OBSERVACION FROM [His3000].[dbo].[Bitacora_Admisiones]  where ATE_CODIGO = A.ATE_CODIGO),'')   AS OBSERVACION_, A.ATE_FECHA_INGRESO AS FECHA_INGRESO, A.ATE_FECHA_ALTA AS FECHA_ALTA,A.ATE_FACTURA_FECHA AS FEC_FACTURACION, P.PAC_IDENTIFICACION AS IDENTIFICACION, P.PAC_APELLIDO_PATERNO + SPACE(1) + P.PAC_APELLIDO_MATERNO + SPACE(1) + P.PAC_NOMBRE1 + SPACE(1) + P.PAC_NOMBRE2 AS NOMBRE_PACIENTE, CONVERT(int,ROUND(DATEDIFF(hour,P.PAC_FECHA_NACIMIENTO,A.ATE_FECHA_INGRESO)/8766.0,0)) AS EDAD,\n"
                         + "                            P.PAC_HISTORIA_CLINICA AS HC, A.ATE_NUMERO_ATENCION AS ATE_NUMERO,A.ATE_CODIGO, H.hab_Numero as HABIT, HE.HES_NOMBRE AS HABESTADO,CC.CAT_NOMBRE AS CONVENIO_SEGURO, A.TIF_OBSERVACION AS OBSERVACION, A.ATE_FACTURA_PACIENTE AS NUM_FACTURA, M.MED_APELLIDO_PATERNO + SPACE(1) + M.MED_APELLIDO_MATERNO + SPACE(1) + M.MED_NOMBRE1 + SPACE(1) + M.MED_NOMBRE2 AS MEDICO_TRATANTE, TT.TIA_DESCRIPCION AS TRATAMIENTO ,TIP.TIP_DESCRIPCION AS TIPO_INGRESO,U.APELLIDOS + SPACE(1) + U.NOMBRES AS NOMBRE_USUARIO,U.ID_USUARIO \n"
                         + "           FROM            dbo.ANEXOS_IESS AS AIP RIGHT OUTER JOIN\n"
                         + "                            dbo.ATENCIONES_DETALLE_SEGUROS AS ADS ON AIP.ANI_CODIGO = ADS.ADS_ASEGURADO_PARENTESCO RIGHT OUTER JOIN\n"
                         + "                            dbo.ATENCIONES AS A INNER JOIN\n"
                         + "                            dbo.PACIENTES AS P ON P.PAC_CODIGO = A.PAC_CODIGO LEFT OUTER JOIN\n"
                         + "                            dbo.TIPO_INGRESO AS TIP ON TIP.TIP_CODIGO = A.TIP_CODIGO LEFT OUTER JOIN\n"
                         + "                            dbo.MEDICOS AS M ON A.MED_CODIGO = M.MED_CODIGO LEFT OUTER JOIN\n"
                         + "                            dbo.HABITACIONES_ESTADO AS HE INNER JOIN\n"
                         + "                            dbo.HABITACIONES AS H ON HE.HES_CODIGO = H.HES_CODIGO ON A.HAB_CODIGO = H.hab_Codigo LEFT OUTER JOIN\n"
                         + "                            dbo.USUARIOS AS U ON U.ID_USUARIO = A.ID_USUSARIO LEFT OUTER JOIN\n"
                         + "                            dbo.TIPO_TRATAMIENTO AS TT ON TT.TIA_CODIGO = A.TIA_CODIGO LEFT OUTER JOIN\n"
                         + "                            dbo.ATENCION_DETALLE_CATEGORIAS AS ADC ON ADC.ATE_CODIGO = A.ATE_CODIGO LEFT OUTER JOIN\n"
                          + "                           dbo.CATEGORIAS_CONVENIOS AS CC ON CC.CAT_CODIGO = ADC.CAT_CODIGO LEFT OUTER JOIN\n"
                          + "                           dbo.ANEXOS_IESS AS AI ON ADC.HCC_CODIGO_TS = AI.ANI_CODIGO LEFT OUTER JOIN\n"
                          + "                           dbo.ANEXOS_IESS AS AID ON ADC.HCC_CODIGO_DE = AID.ANI_CODIGO ON ADS.ATE_CODIGO = A.ATE_CODIGO\n"
                    + xWhere, Sqlcon);
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

        public DataTable getCierreFacturacion(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fuser, int user)
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
            ingreso = false; alta = false;
            string xWhere = "";
            int count = 0;
            if (ingreso)
            {
                xWhere += "  A.ATE_FECHA_INGRESO BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (alta)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  A.ATE_FECHA_ALTA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (facturacion)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  A.ATE_FACTURA_FECHA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (count > 0)
                xWhere = " (" + xWhere + ") ";


            if (Fingreso)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIP_CODIGO=" + Cod_Ingreso + " ";
                count++;
            }

            if (Ftratamiento)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIA_CODIGO=" + Cod_Tratamiento + " ";
                count++;
            }

            if (Fuser)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "(SELECT TOP 1 dbo.USUARIOS.ID_USUARIO\n" +
                        "      FROM            dbo.ATENCIONES INNER JOIN Sic3000.dbo.Detalle ON dbo.ATENCIONES.ATE_FACTURA_PACIENTE = Sic3000.dbo.Detalle.numnot INNER JOIN dbo.USUARIOS ON Sic3000.dbo.Detalle.cajero = dbo.USUARIOS.Codigo_Rol\n" +
                        "      WHERE        dbo.ATENCIONES.ATE_CODIGO = A.ATE_CODIGO ) \n" + "  ='" + user + "' ";  ///cambio de int a comillas simples la comparaion con la cosulta
                count++;
            }

            if (count > 0)
                xWhere += " AND ";
            xWhere += " A.ATE_FACTURA_FECHA is not null ";
            count++;


            if (count > 0)
                xWhere = "Where " + xWhere;

            Sqlcmd = new SqlCommand(" SELECT    ISNULL((select OBSERVACION FROM [His3000].[dbo].[Bitacora_Admisiones]  where TIPO = 'FACTURACION' AND ATE_CODIGO = A.ATE_CODIGO),'')   AS OBSERVACION_,'FACTURACION' AS TIPO_,	A.ATE_FACTURA_FECHA  AS FECHA, P.PAC_IDENTIFICACION AS IDENTIFICACION, P.PAC_APELLIDO_PATERNO + SPACE(1) + P.PAC_APELLIDO_MATERNO + SPACE(1) + P.PAC_NOMBRE1 + SPACE(1) + P.PAC_NOMBRE2 AS NOMBRE_PACIENTE, CONVERT(int, ROUND(DATEDIFF(hour, P.PAC_FECHA_NACIMIENTO, A.ATE_FECHA_INGRESO) / 8766.0, 0)) AS EDAD,\n" +
                        "P.PAC_HISTORIA_CLINICA AS HC, A.ATE_NUMERO_ATENCION AS ATE_NUMERO, A.ATE_CODIGO AS ATE_CODIGO, H.hab_Numero as HABIT, HE.HES_NOMBRE AS HABESTADO, CC.CAT_NOMBRE AS CONVENIO_SEGURO, A.TIF_OBSERVACION AS OBS_CONVENIO, A.ATE_FACTURA_PACIENTE AS NUM_FACTURA, M.MED_APELLIDO_PATERNO + SPACE(1) + M.MED_APELLIDO_MATERNO + SPACE(1) + M.MED_NOMBRE1 + SPACE(1) + M.MED_NOMBRE2 AS MEDICO_TRATANTE, TT.TIA_DESCRIPCION AS TRATAMIENTO, TIP.TIP_DESCRIPCION AS TIPO_INGRESO,\n" +
                        "(SELECT TOP 1 concat(dbo.USUARIOS.APELLIDOS, ' ', dbo.USUARIOS.NOMBRES)\n" +
                        "       FROM            dbo.ATENCIONES INNER JOIN Sic3000.dbo.Detalle ON dbo.ATENCIONES.ATE_FACTURA_PACIENTE = Sic3000.dbo.Detalle.numnot INNER JOIN dbo.USUARIOS ON Sic3000.dbo.Detalle.cajero = dbo.USUARIOS.Codigo_Rol\n" +
                        "        WHERE        dbo.ATENCIONES.ATE_CODIGO = A.ATE_CODIGO) AS NOMBRE_USUARIO,\n" +
                        "(SELECT TOP 1 dbo.USUARIOS.ID_USUARIO\n" +
                        "      FROM            dbo.ATENCIONES INNER JOIN Sic3000.dbo.Detalle ON dbo.ATENCIONES.ATE_FACTURA_PACIENTE = Sic3000.dbo.Detalle.numnot INNER JOIN dbo.USUARIOS ON Sic3000.dbo.Detalle.cajero = dbo.USUARIOS.Codigo_Rol\n" +
                        "      WHERE        dbo.ATENCIONES.ATE_CODIGO = A.ATE_CODIGO ) as ID_USUARIO\n" +
                        "FROM dbo.ANEXOS_IESS AS AIP RIGHT OUTER JOIN dbo.ATENCIONES_DETALLE_SEGUROS AS ADS ON AIP.ANI_CODIGO = ADS.ADS_ASEGURADO_PARENTESCO RIGHT OUTER JOIN dbo.ATENCIONES AS A INNER JOIN dbo.PACIENTES AS P ON P.PAC_CODIGO = A.PAC_CODIGO LEFT OUTER JOIN dbo.TIPO_INGRESO AS TIP ON TIP.TIP_CODIGO = A.TIP_CODIGO LEFT OUTER JOIN dbo.MEDICOS AS M ON A.MED_CODIGO = M.MED_CODIGO LEFT OUTER JOIN\n" +
                        "            dbo.HABITACIONES_ESTADO AS HE INNER JOIN dbo.HABITACIONES AS H ON HE.HES_CODIGO = H.HES_CODIGO ON A.HAB_CODIGO = H.hab_Codigo LEFT OUTER JOIN dbo.USUARIOS AS U ON U.ID_USUARIO = A.ID_USUSARIO LEFT OUTER JOIN dbo.TIPO_TRATAMIENTO AS TT ON TT.TIA_CODIGO = A.TIA_CODIGO LEFT OUTER JOIN\n" +
                        "            dbo.ATENCION_DETALLE_CATEGORIAS AS ADC ON ADC.ATE_CODIGO = A.ATE_CODIGO LEFT OUTER JOIN dbo.CATEGORIAS_CONVENIOS AS CC ON CC.CAT_CODIGO = ADC.CAT_CODIGO LEFT OUTER JOIN dbo.ANEXOS_IESS AS AI ON ADC.HCC_CODIGO_TS = AI.ANI_CODIGO LEFT OUTER JOIN dbo.ANEXOS_IESS AS AID ON ADC.HCC_CODIGO_DE = AID.ANI_CODIGO ON ADS.ATE_CODIGO = A.ATE_CODIGO\n" +
                     xWhere, Sqlcon);
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

        public DataTable getCierreAlta(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fuser, int user)
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
            string xWhere = "";
            ingreso = false; facturacion = false;
            int count = 0;
            if (ingreso)
            {
                xWhere += "  A.ATE_FECHA_INGRESO BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (alta)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  A.ATE_FECHA_ALTA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (facturacion)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  A.ATE_FACTURA_FECHA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (count > 0)
                xWhere = " (" + xWhere + ") ";


            if (Fingreso)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIP_CODIGO=" + Cod_Ingreso + " ";
                count++;
            }

            if (Ftratamiento)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIA_CODIGO=" + Cod_Tratamiento + " ";
                count++;
            }

            if (Fuser)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += " (SELECT TOP 1 dbo.USUARIOS.ID_USUARIO\n" +
                            "FROM  dbo.HABITACIONES_DETALLE INNER JOIN dbo.USUARIOS ON dbo.HABITACIONES_DETALLE.ID_USUARIO = dbo.USUARIOS.ID_USUARIO\n" +
                            "WHERE(dbo.HABITACIONES_DETALLE.HAD_CODIGO =\n" +
                            "     (SELECT MAX(HAD_CODIGO) AS Expr1 FROM dbo.HABITACIONES_DETALLE AS HABITACIONES_DETALLE_1 WHERE(ATE_CODIGO = A.ATE_CODIGO)))) =" + user + " ";
                count++;
            }


            if (count > 0)
                xWhere += " AND ";
            xWhere += " A.ATE_FECHA_ALTA is not null ";
            count++;

            if (count > 0)
                xWhere = "Where " + xWhere;

            Sqlcmd = new SqlCommand(" SELECT    ISNULL((select OBSERVACION FROM [His3000].[dbo].[Bitacora_Admisiones]  where TIPO = 'ALTA' AND ATE_CODIGO = A.ATE_CODIGO),'')   AS OBSERVACION_,'ALTA' AS TIPO_,	A.ATE_FECHA_ALTA  AS FECHA, P.PAC_IDENTIFICACION AS IDENTIFICACION, P.PAC_APELLIDO_PATERNO + SPACE(1) + P.PAC_APELLIDO_MATERNO + SPACE(1) + P.PAC_NOMBRE1 + SPACE(1) + P.PAC_NOMBRE2 AS NOMBRE_PACIENTE, CONVERT(int, ROUND(DATEDIFF(hour, P.PAC_FECHA_NACIMIENTO, A.ATE_FECHA_INGRESO) / 8766.0, 0)) AS EDAD,\n" +
                          "P.PAC_HISTORIA_CLINICA AS HC, A.ATE_NUMERO_ATENCION AS ATE_NUMERO, A.ATE_CODIGO AS ATE_CODIGO, H.hab_Numero as HABIT, HE.HES_NOMBRE AS HABESTADO, CC.CAT_NOMBRE AS CONVENIO_SEGURO, A.TIF_OBSERVACION AS OBS_CONVENIO, A.ATE_FACTURA_PACIENTE AS NUM_FACTURA, M.MED_APELLIDO_PATERNO + SPACE(1) + M.MED_APELLIDO_MATERNO + SPACE(1) + M.MED_NOMBRE1 + SPACE(1) + M.MED_NOMBRE2 AS MEDICO_TRATANTE, TT.TIA_DESCRIPCION AS TRATAMIENTO, TIP.TIP_DESCRIPCION AS TIPO_INGRESO,\n" +
                            "(SELECT TOP 1 concat(dbo.USUARIOS.APELLIDOS, ' ', dbo.USUARIOS.NOMBRES)\n" +
                            "FROM  dbo.HABITACIONES_DETALLE INNER JOIN dbo.USUARIOS ON dbo.HABITACIONES_DETALLE.ID_USUARIO = dbo.USUARIOS.ID_USUARIO\n" +
                            "WHERE(dbo.HABITACIONES_DETALLE.HAD_CODIGO =\n" +
                            "     (SELECT MAX(HAD_CODIGO) AS Expr1 FROM dbo.HABITACIONES_DETALLE AS HABITACIONES_DETALLE_1 WHERE(ATE_CODIGO = A.ATE_CODIGO)))) as NOMBRE_USUARIO,\n" +
                            "(SELECT TOP 1 dbo.USUARIOS.ID_USUARIO\n" +
                            "FROM  dbo.HABITACIONES_DETALLE INNER JOIN dbo.USUARIOS ON dbo.HABITACIONES_DETALLE.ID_USUARIO = dbo.USUARIOS.ID_USUARIO\n" +
                            "WHERE(dbo.HABITACIONES_DETALLE.HAD_CODIGO =\n" +
                            "     (SELECT MAX(HAD_CODIGO) AS Expr1 FROM dbo.HABITACIONES_DETALLE AS HABITACIONES_DETALLE_1 WHERE(ATE_CODIGO = A.ATE_CODIGO)))) as ID_USUARIO\n" +

                          "FROM dbo.ANEXOS_IESS AS AIP RIGHT OUTER JOIN dbo.ATENCIONES_DETALLE_SEGUROS AS ADS ON AIP.ANI_CODIGO = ADS.ADS_ASEGURADO_PARENTESCO RIGHT OUTER JOIN dbo.ATENCIONES AS A INNER JOIN dbo.PACIENTES AS P ON P.PAC_CODIGO = A.PAC_CODIGO LEFT OUTER JOIN dbo.TIPO_INGRESO AS TIP ON TIP.TIP_CODIGO = A.TIP_CODIGO LEFT OUTER JOIN dbo.MEDICOS AS M ON A.MED_CODIGO = M.MED_CODIGO LEFT OUTER JOIN\n" +
                          "            dbo.HABITACIONES_ESTADO AS HE INNER JOIN dbo.HABITACIONES AS H ON HE.HES_CODIGO = H.HES_CODIGO ON A.HAB_CODIGO = H.hab_Codigo LEFT OUTER JOIN dbo.USUARIOS AS U ON U.ID_USUARIO = A.ID_USUSARIO LEFT OUTER JOIN dbo.TIPO_TRATAMIENTO AS TT ON TT.TIA_CODIGO = A.TIA_CODIGO LEFT OUTER JOIN\n" +
                          "            dbo.ATENCION_DETALLE_CATEGORIAS AS ADC ON ADC.ATE_CODIGO = A.ATE_CODIGO LEFT OUTER JOIN dbo.CATEGORIAS_CONVENIOS AS CC ON CC.CAT_CODIGO = ADC.CAT_CODIGO LEFT OUTER JOIN dbo.ANEXOS_IESS AS AI ON ADC.HCC_CODIGO_TS = AI.ANI_CODIGO LEFT OUTER JOIN dbo.ANEXOS_IESS AS AID ON ADC.HCC_CODIGO_DE = AID.ANI_CODIGO ON ADS.ATE_CODIGO = A.ATE_CODIGO\n" +
                       xWhere, Sqlcon);
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
        public DataTable getCierreAdmision(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fuser, int user)
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
            string xWhere = "";
            int count = 0;
            alta = false; facturacion = false;

            if (ingreso)
            {
                xWhere += "  A.ATE_FECHA_INGRESO BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (alta)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  A.ATE_FECHA_ALTA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (facturacion)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  A.ATE_FACTURA_FECHA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (count > 0)
                xWhere = " (" + xWhere + ") ";


            if (Fingreso)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIP_CODIGO=" + Cod_Ingreso + " ";
                count++;
            }

            if (Ftratamiento)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIA_CODIGO=" + Cod_Tratamiento + " ";
                count++;
            }

            if (Fuser)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  U.ID_USUARIO =" + user + " ";
                count++;
            }

            if (count > 0)
                xWhere = "Where " + xWhere;

            Sqlcmd = new SqlCommand("SELECT   ISNULL((select OBSERVACION FROM [His3000].[dbo].[Bitacora_Admisiones]  where TIPO = 'ADMISION' AND ATE_CODIGO = A.ATE_CODIGO),'')   AS OBSERVACION_,'ADMISION' AS TIPO_,	A.ATE_FECHA_INGRESO AS FECHA, P.PAC_IDENTIFICACION AS IDENTIFICACION, P.PAC_APELLIDO_PATERNO + SPACE(1) + P.PAC_APELLIDO_MATERNO + SPACE(1) + P.PAC_NOMBRE1 + SPACE(1) + P.PAC_NOMBRE2 AS NOMBRE_PACIENTE,CONVERT(int,ROUND(DATEDIFF(hour,P.PAC_FECHA_NACIMIENTO,A.ATE_FECHA_INGRESO)/8766.0,0)) AS EDAD,\n"
                        + "P.PAC_HISTORIA_CLINICA AS HC, A.ATE_NUMERO_ATENCION AS ATE_NUMERO,A.ATE_CODIGO AS ATE_CODIGO, H.hab_Numero as HABIT, HE.HES_NOMBRE AS HABESTADO,CC.CAT_NOMBRE AS CONVENIO_SEGURO, A.TIF_OBSERVACION AS OBS_CONVENIO, A.ATE_FACTURA_PACIENTE AS NUM_FACTURA,  M.MED_APELLIDO_PATERNO + SPACE(1) + M.MED_APELLIDO_MATERNO + SPACE(1) + M.MED_NOMBRE1 + SPACE(1) + M.MED_NOMBRE2 AS MEDICO_TRATANTE, TT.TIA_DESCRIPCION AS TRATAMIENTO ,TIP.TIP_DESCRIPCION AS TIPO_INGRESO , U.APELLIDOS + SPACE(1) + U.NOMBRES AS NOMBRE_USUARIO, U.ID_USUARIO\n"
                         + "           FROM            dbo.ANEXOS_IESS AS AIP RIGHT OUTER JOIN\n"
                         + "                            dbo.ATENCIONES_DETALLE_SEGUROS AS ADS ON AIP.ANI_CODIGO = ADS.ADS_ASEGURADO_PARENTESCO RIGHT OUTER JOIN\n"
                         + "                            dbo.ATENCIONES AS A INNER JOIN\n"
                         + "                            dbo.PACIENTES AS P ON P.PAC_CODIGO = A.PAC_CODIGO LEFT OUTER JOIN\n"
                         + "                            dbo.TIPO_INGRESO AS TIP ON TIP.TIP_CODIGO = A.TIP_CODIGO LEFT OUTER JOIN\n"
                         + "                            dbo.MEDICOS AS M ON A.MED_CODIGO = M.MED_CODIGO LEFT OUTER JOIN\n"
                         + "                            dbo.HABITACIONES_ESTADO AS HE INNER JOIN\n"
                         + "                            dbo.HABITACIONES AS H ON HE.HES_CODIGO = H.HES_CODIGO ON A.HAB_CODIGO = H.hab_Codigo LEFT OUTER JOIN\n"
                         + "                            dbo.USUARIOS AS U ON U.ID_USUARIO = A.ID_USUSARIO LEFT OUTER JOIN\n"
                         + "                            dbo.TIPO_TRATAMIENTO AS TT ON TT.TIA_CODIGO = A.TIA_CODIGO LEFT OUTER JOIN\n"
                         + "                            dbo.ATENCION_DETALLE_CATEGORIAS AS ADC ON ADC.ATE_CODIGO = A.ATE_CODIGO LEFT OUTER JOIN\n"
                          + "                           dbo.CATEGORIAS_CONVENIOS AS CC ON CC.CAT_CODIGO = ADC.CAT_CODIGO LEFT OUTER JOIN\n"
                          + "                           dbo.ANEXOS_IESS AS AI ON ADC.HCC_CODIGO_TS = AI.ANI_CODIGO LEFT OUTER JOIN\n"
                          + "                           dbo.ANEXOS_IESS AS AID ON ADC.HCC_CODIGO_DE = AID.ANI_CODIGO ON ADS.ATE_CODIGO = A.ATE_CODIGO\n"
                    + xWhere, Sqlcon);
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

        public DataTable getPedidosImagen(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fhc, int hc, bool Fformulario, string formulario)
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
            string xWhere = "";
            int count = 0;
            if (ingreso)
            {
                xWhere += "  A.ATE_FECHA_INGRESO BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (alta)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  A.ATE_FECHA_ALTA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (facturacion)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  A.ATE_FACTURA_FECHA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (count > 0)
                xWhere = " (" + xWhere + ") ";
            if (Fingreso)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIP_CODIGO=" + Cod_Ingreso + " ";
                count++;
            }

            if (Ftratamiento)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIA_CODIGO=" + Cod_Tratamiento + " ";
                count++;
            }

            if (Fhc)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.PAC_HISTORIA_CLINICA=" + hc + " ";
                count++;
            }

            if (Fformulario)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  F.TIPO='" + formulario + "' ";
                count++;
            }

            if (count > 0)
                xWhere = "Where " + xWhere;

            Sqlcmd = new SqlCommand("SELECT (select case F.PRIORIDAD when 1 then 'Control' when 2 then 'Rutina' when 3 then 'Urgente' else '0'end)as 'PRIORIDAD',F.FECHA_CREACION ,F.TIPO,  A.PAC_HISTORIA_CLINICA, A.ATE_NUMERO_ATENCION,  A.NOMBRE_PACIENTE,   F.ID_Formulario, A.ATE_CODIGO, A.HAB_CODIGO, A.hab_Numero,A.HABESTADO,A.PAC_CODIGO,A.PAC_IDENTIFICACION,A.PAC_GENERO, A.ATE_DIAGNOSTICO_INICIAL, A.MED_CODIGO,A.NOMBRE_MEDICO,A.TIA_DESCRIPCION,A.ATE_FECHA_INGRESO ,  A.ATE_FECHA_ALTA, A.ATE_FACTURA_FECHA,A.ATE_FACTURA_PACIENTE,A.NOMBRE_USUARIO,A.TIP_DESCRIPCION,A.ADS_ASEGURADO_NOMBRE,A.ADS_ASEGURADO_CEDULA, A.PARENTESCO FROM \n"
                        + " (SELECT PRIORIDAD,ATE_CODIGO, id_imagenologia AS ID_Formulario, 'IMAGENOLOGIA' AS TIPO , dbo.HC_IMAGENOLOGIA.FECHA_CREACION  FROM dbo.HC_IMAGENOLOGIA where dbo.HC_IMAGENOLOGIA.FECHA_CREACION between '" + desde + "' and '" + hasta + "') F\n"
                        + "    JOIN(\n"
                        + "           SELECT        A.ATE_CODIGO, A.HAB_CODIGO, H.hab_Numero, A.PAC_CODIGO, P.PAC_IDENTIFICACION, P.PAC_APELLIDO_PATERNO + SPACE(1) + P.PAC_APELLIDO_MATERNO + SPACE(1) + P.PAC_NOMBRE1 + SPACE(1) \n"
                     + "+ P.PAC_NOMBRE2 AS NOMBRE_PACIENTE, P.PAC_HISTORIA_CLINICA, A.ATE_NUMERO_ATENCION, P.PAC_GENERO, A.TIF_OBSERVACION, A.ATE_FECHA_INGRESO, A.ATE_FECHA_ALTA, A.ATE_FACTURA_FECHA,\n"
                     + "A.ATE_FACTURA_PACIENTE, A.MED_CODIGO, M.MED_APELLIDO_PATERNO + SPACE(1) + M.MED_APELLIDO_MATERNO + SPACE(1) + M.MED_NOMBRE1 + SPACE(1) + M.MED_NOMBRE2 AS NOMBRE_MEDICO,\n"
                     + "TT.TIA_DESCRIPCION, A.ATE_DIAGNOSTICO_INICIAL, U.APELLIDOS + SPACE(1) + U.NOMBRES AS NOMBRE_USUARIO, TIP.TIP_DESCRIPCION, ADS.ADS_ASEGURADO_NOMBRE, ADS.ADS_ASEGURADO_CEDULA,\n"
                     + "AIP.ANI_DESCRIPCION AS PARENTESCO,TT.TIA_CODIGO ,TIP.TIP_CODIGO, HE.HES_NOMBRE AS HABESTADO\n"
                     + " FROM            dbo.ANEXOS_IESS AS AIP RIGHT OUTER JOIN\n"
                     + "dbo.ATENCIONES_DETALLE_SEGUROS AS ADS ON AIP.ANI_CODIGO = ADS.ADS_ASEGURADO_PARENTESCO RIGHT OUTER JOIN\n"
                     + "dbo.ATENCIONES AS A INNER JOIN\n"
                     + "dbo.PACIENTES AS P ON P.PAC_CODIGO = A.PAC_CODIGO LEFT OUTER JOIN\n"
                     + "dbo.TIPO_INGRESO AS TIP ON TIP.TIP_CODIGO = A.TIP_CODIGO LEFT OUTER JOIN\n"
                     + "dbo.MEDICOS AS M ON A.MED_CODIGO = M.MED_CODIGO LEFT OUTER JOIN\n"
                     + "dbo.HABITACIONES_ESTADO AS HE INNER JOIN\n"
                     + "dbo.HABITACIONES AS H ON HE.HES_CODIGO = H.HES_CODIGO ON A.HAB_CODIGO = H.hab_Codigo LEFT OUTER JOIN\n"
                     + "dbo.USUARIOS AS U ON U.ID_USUARIO = A.ID_USUSARIO LEFT OUTER JOIN\n"
                     + "dbo.TIPO_TRATAMIENTO AS TT ON TT.TIA_CODIGO = A.TIA_CODIGO ON ADS.ATE_CODIGO = A.ATE_CODIGO ) A\n"
                    + "ON  F.ATE_CODIGO = A.ATE_CODIGO\n"
                    + xWhere, Sqlcon);
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



        public DataTable getAtencionesFormularios(DateTime desde, DateTime hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fhc, int hc, bool Fformulario, string formulario, bool mushugñan, Int16 areaAsignada = 0)
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
            string xWhere = "";
            int count = 0;
            if (ingreso)
            {
                xWhere += "  cast(convert(varchar(11),A.ATE_FECHA_INGRESO,13) as datetime)>= @fechadesde And cast(convert(varchar(11),A.ATE_FECHA_INGRESO,13) as datetime)<= @fechahasta";
                count++;
            }
            if (alta)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "   CONVERT(date,A.ATE_FECHA_ALTA) BETWEEN @fechadesde AND @fechahasta";
                count++;
            }
            if (facturacion)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "   CONVERT(date,A.ATE_FACTURA_FECHA) BETWEEN @fechadesde AND @fechahasta";
                count++;
            }
            if (count > 0)
                xWhere = " (" + xWhere + ") ";


            if (Fingreso)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIP_CODIGO=" + Cod_Ingreso + " ";
                count++;
            }

            if (Ftratamiento)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIA_CODIGO=" + Cod_Tratamiento + " ";
                count++;
            }

            if (Fhc)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.PAC_HISTORIA_CLINICA=" + hc + " ";
                count++;
            }

            if (Fformulario)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  F.TIPO='" + formulario + "' ";
                count++;
            }
            if (mushugñan)
            {
                switch (areaAsignada)
                {
                    case 2:
                        if (count > 0)
                            xWhere += " AND ";
                        xWhere += " A.TIP_CODIGO = 10";
                        break;
                    case 3:
                        if (count > 0)
                            xWhere += " AND ";
                        xWhere += " A.TIP_CODIGO = 12";
                        break;
                    default:
                        if (count > 0)
                            xWhere += " AND ";
                        xWhere += " A.TIP_CODIGO = 10";
                        break;
                }

            }

            if (count > 0)
                xWhere = "Where " + xWhere;

            Sqlcmd = new SqlCommand("SELECT A.ATE_FECHA_INGRESO AS 'FECHA INGRESO' ,F.TIPO,  A.PAC_HISTORIA_CLINICA AS HC, A.ATE_NUMERO_ATENCION AS 'NUMERO ATENCION', " +
                "(ISNULL((select top 1 STRING_AGG(ED_DESCRIPCION, ', ')  from HC_EMERGENCIA_FORM e inner join  HC_EMERGENCIA_FORM_DIAGNOSTICOS d on e.EMER_CODIGO = d.EMER_CODIGO where e.ATE_CODIGO = a.ATE_CODIGO and ED_TIPO = 'I'), f.DICEX ))as 'DIAGNOSTICO INICIAL' " +
                ",(ISNULL((select top 1 STRING_AGG(ED_DESCRIPCION, ', ') from HC_EMERGENCIA_FORM e inner join  HC_EMERGENCIA_FORM_DIAGNOSTICOS d on e.EMER_CODIGO = d.EMER_CODIGO where e.ATE_CODIGO = a.ATE_CODIGO and ED_TIPO = 'A'),' ' )) as 'DIAGNOSTICO FINAL' , " +
                "A.NOMBRE_MEDICO AS MEDICO, A.NOMBRE_PACIENTE AS PACIENTE, A.TIP_DESCRIPCION AS 'TIP. INGRESO', A.ATE_FECHA_ALTA AS 'FECHA ALTA', F.ID_Formulario, A.ATE_CODIGO, A.HAB_CODIGO, A.MED_CODIGO, A.hab_Numero AS HAB,A.HABESTADO AS 'HAB ESTADO',A.PAC_CODIGO,A.PAC_IDENTIFICACION AS IDENTIFICACION,A.PAC_GENERO AS SEXO," +
                "A.TIA_DESCRIPCION AS 'TIP. TRATAMIENTO',  (select concat(CONVERT(DATE, fecha), ' ', isnull(hora, '00:00:00')) from Sic3000..Nota where numfac=A.ATE_FACTURA_PACIENTE) as ATE_FACTURA_FECHA, A.ATE_FACTURA_PACIENTE,A.NOMBRE_USUARIO AS USUARIO , A.ADS_ASEGURADO_NOMBRE,A.ADS_ASEGURADO_CEDULA, A.PARENTESCO FROM \n"
                         + "     (\n"
                          + "           SELECT        ATE_CODIGO, ANE_CODIGO AS ID_Formulario, 'ANAMNESIS' AS TIPO,'' as'DICEX' FROM            dbo.HC_ANAMNESIS\n"
                         + "            UNION ALL\n"
                        + "             SELECT        ATE_CODIGO, EMER_CODIGO AS ID_Formulario, 'EMERGENCIA' AS TIPO,'' as'DICEX'  FROM            dbo.HC_EMERGENCIA_FORM\n"
                         + "            UNION ALL\n"
                         + "            SELECT        ATE_CODIGO, EPI_CODIGO AS ID_Formulario, 'EPICRISIS' AS TIPO,'' as'DICEX'  FROM            dbo.HC_EPICRISIS\n"
                         + "            UNION ALL\n"
                         + "            SELECT        ATE_CODIGO, EVO_CODIGO AS ID_Formulario, 'EVOLUCION' AS TIPO,'' as'DICEX'  FROM            dbo.HC_EVOLUCION\n"
                         + "            UNION ALL\n"
                         + "            SELECT        ATE_CODIGO, id_imagenologia AS ID_Formulario, 'IMAGENOLOGIA' AS TIPO,'' as'DICEX'  FROM            dbo.HC_IMAGENOLOGIA\n"
                         + "            UNION ALL\n"
                         + "            SELECT        ATE_CODIGO, HIN_CODIGO AS ID_Formulario, 'INTERCONSULTA' AS TIPO,'' as'DICEX'  FROM            dbo.HC_INTERCONSULTA\n"
                         + "            UNION ALL\n"
                         + "            SELECT        ATE_CODIGO, LCL_CODIGO AS ID_Formulario, 'LABORATORIO' AS TIPO,'' as'DICEX'  FROM            dbo.HC_LABORATORIO_CLINICO\n"
                         + "            UNION ALL\n"
                         + "            SELECT        ATE_CODIGO, ADF_CODIGO AS ID_Formulario, 'PROTOCOLO' AS TIPO,'' as'DICEX'  FROM            dbo.HC_PROTOCOLO_OPERATORIO\n"
                         + "              UNION ALL\n"
                         + "              SELECT AteCodigo, ID_FORM002, 'CONSULTA EXTERNA' AS TIPO,(diagnostico1cie+', '+diagnostico1+'// '+diagnostico2cie+', '+diagnostico2+'// '+diagnostico3cie+', '+diagnostico3+'// '+diagnostico4cie+', '+diagnostico4)as 'DICEX' FROM Form002MSP\n"
                         + "              UNION ALL\n"
                         + "              SELECT ATE_CODIGO, EVO_CODIGO, 'EVOLUCION ENFERMERIA' AS TIPO,'' as'DICEX' FROM HC_EVOLUCION_ENFERMERA\n"
                         + "              UNION ALL\n"
                         + "               SELECT ATE_CODIGO, MA_CODIGO, 'HOJA DE ALTA' AS TIPO,'' as'DICEX' FROM MEDICOS_ALTA\n"
                         + "              UNION ALL\n"
                         + "              SELECT ATE_CODIGO, id, 'KARDEX INSUMOS' AS TIPO,'' as'DICEX' FROM KARDEX_INSUMOS\n"
                         + "              UNION ALL\n"
                         + "              SELECT AteCodigo, ID_KARDEX_MEDICAMENTOS, 'KARDEX MEDICAMENTOS' AS TIPO,'' as'DICEX' FROM KARDEXMEDICAMENTOS\n"
                         + "    ) F\n"
                         + "    JOIN(\n"
                         + "           SELECT        A.ATE_CODIGO, A.HAB_CODIGO, H.hab_Numero, A.PAC_CODIGO, P.PAC_IDENTIFICACION, P.PAC_APELLIDO_PATERNO + SPACE(1) + P.PAC_APELLIDO_MATERNO + SPACE(1) + P.PAC_NOMBRE1 + SPACE(1) \n"
                      + "+ P.PAC_NOMBRE2 AS NOMBRE_PACIENTE, P.PAC_HISTORIA_CLINICA, A.ATE_NUMERO_ATENCION, P.PAC_GENERO, A.TIF_OBSERVACION, A.ATE_FECHA_INGRESO, A.ATE_FECHA_ALTA, A.ATE_FACTURA_FECHA,\n"
                      + "A.ATE_FACTURA_PACIENTE, A.MED_CODIGO, M.MED_APELLIDO_PATERNO + SPACE(1) + M.MED_APELLIDO_MATERNO + SPACE(1) + M.MED_NOMBRE1 + SPACE(1) + M.MED_NOMBRE2 AS NOMBRE_MEDICO,\n"
                      + "TT.TIA_DESCRIPCION, A.ATE_DIAGNOSTICO_INICIAL, U.APELLIDOS + SPACE(1) + U.NOMBRES AS NOMBRE_USUARIO, TIP.TIP_DESCRIPCION, ADS.ADS_ASEGURADO_NOMBRE, ADS.ADS_ASEGURADO_CEDULA,\n"
                      + "AIP.ANI_DESCRIPCION AS PARENTESCO,TT.TIA_CODIGO ,TIP.TIP_CODIGO, HE.HES_NOMBRE AS HABESTADO\n"
                      + " FROM            dbo.ANEXOS_IESS AS AIP RIGHT OUTER JOIN\n"
                      + "dbo.ATENCIONES_DETALLE_SEGUROS AS ADS ON AIP.ANI_CODIGO = ADS.ADS_ASEGURADO_PARENTESCO RIGHT OUTER JOIN\n"
                      + "dbo.ATENCIONES AS A INNER JOIN\n"
                      + "dbo.PACIENTES AS P ON P.PAC_CODIGO = A.PAC_CODIGO LEFT OUTER JOIN\n"
                      + "dbo.TIPO_INGRESO AS TIP ON TIP.TIP_CODIGO = A.TIP_CODIGO LEFT OUTER JOIN\n"
                      + "dbo.MEDICOS AS M ON A.MED_CODIGO = M.MED_CODIGO LEFT OUTER JOIN\n"
                      + "dbo.HABITACIONES_ESTADO AS HE INNER JOIN\n"
                      + "dbo.HABITACIONES AS H ON HE.HES_CODIGO = H.HES_CODIGO ON A.HAB_CODIGO = H.hab_Codigo LEFT OUTER JOIN\n"
                      + "dbo.USUARIOS AS U ON U.ID_USUARIO = A.ID_USUSARIO LEFT OUTER JOIN\n"
                      + "dbo.TIPO_TRATAMIENTO AS TT ON TT.TIA_CODIGO = A.TIA_CODIGO ON ADS.ATE_CODIGO = A.ATE_CODIGO\n"
                        + "     ) A\n"
                     + "ON  F.ATE_CODIGO = A.ATE_CODIGO\n"
                     + xWhere, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.Parameters.AddWithValue("@fechadesde", desde);
            Sqlcmd.Parameters.AddWithValue("@fechahasta", hasta);
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 280;
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

        public DataTable getINEN(string desde, string hasta)
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


            //            Sqlcmd = new SqlCommand("SELECT A.ATE_NUMERO_Atencion ,p.PAC_IDENTIFICACION , p.PAC_NOMBRE1, p.pac_nombre2 , p.PAC_APELLIDO_PATERNO, p.PAC_APELLIDO_MATERNO, p.PAC_NACIONALIDAD , p.PAC_GENERO , p.PAC_FECHA_NACIMIENTO ,A.ATE_EDAD_PACIENTE ,\n" +
            //"(select dp.DIPO_NOMBRE from DIVISION_POLITICA dp where dp.DIPO_CODIINEC = pda.COD_PAIS) pais, (select dp.DIPO_NOMBRE from DIVISION_POLITICA dp where dp.DIPO_CODIINEC = pda.COD_PROVINCIA) provincia, (select dp.DIPO_NOMBRE from DIVISION_POLITICA dp where dp.DIPO_CODIINEC = pda.COD_CANTON) canton,\n" +
            //"(select dp.DIPO_NOMBRE from DIVISION_POLITICA dp where dp.DIPO_CODIINEC = pda.COD_PARROQUIA) parroquia, p.PAC_REFERENTE_DIRECCION ,A.ATE_FECHA ,A.ATE_FECHA_INGRESO ,A.ATE_FECHA_ALTA , A.ATE_FACTURA_PACIENTE,\n" +
            //"(select m.MED_APELLIDO_PATERNO from MEDICOS M where M.MED_CODIGO = A.MED_CODIGO)as apemedico, (select m.MED_NOMBRE1 from MEDICOS M where M.MED_CODIGO = A.MED_CODIGO)as nommedico, (select  em.ESP_NOMBRE from MEDICOS m, ESPECIALIDADES_MEDICAS em where m.ESP_CODIGO = em.ESP_CODIGO and M.MED_CODIGO = A.MED_CODIGO ) as especialidad ,\n" +
            //"(select top 1  e.E_NOMBRE from PACIENTES p, ETNIA e where e.E_CODIGO = p.E_CODIGO ) as etnia , (select h.hab_Numero from HABITACIONES h where h.hab_Codigo = A.HAB_CODIGO)as nomhab, (select P.PAC_HISTORIA_CLINICA from PACIENTES P where A.PAC_CODIGO = P.PAC_CODIGO)as hclinica,\n" +
            //"(select sum(cp.CUE_VALOR) + sum(cp.CUE_IVA) from CUENTAS_PACIENTES CP where CP.ate_codigo = A.ATE_CODIGO) as valor, (SELECT Sum(cp.CUE_VALOR) from CUENTAS_PACIENTES CP where CP.ate_codigo = A.ATE_CODIGO)\n" +
            //" AS 'SIN IVA', (SELECT Sum(cp.CUE_IVA) from CUENTAS_PACIENTES CP where CP.ate_codigo = A.ATE_CODIGO) AS 'IVA', (SELECT SUM(precos) FROM CUENTAS_PACIENTES CP, Sic3000..PRODUCTO\n" +
            //" where codpro = pro_codigo AND CP.ATE_CODIGO = A.ATE_CODIGO) as 'COSTO', cc.CAT_NOMBRE, A.TipoAtencion , (select ti.TIP_DESCRIPCION from TIPO_INGRESO ti where A.TipoAtencion = ti.TIP_CODIGO) as NombreAtencion\n" +
            //" FROM ATENCIONES A, pacientes p, CATEGORIAS_CONVENIOS cc, ATENCION_DETALLE_CATEGORIAS adc, PACIENTES_DATOS_ADICIONALES pda , HABITACIONES h\n" +
            //" where A.ATE_FECHA >= '" + desde + "' AND A.ATE_FECHA <= '" + hasta + "' and\n" +
            //" p.PAC_CODIGO = a.PAC_CODIGO and a.ATE_CODIGO = adc.ATE_CODIGO and adc.CAT_CODIGO = cc.CAT_CODIGO and a.PAC_CODIGO = pda.PAC_CODIGO and a.HAB_CODIGO = h.hab_Codigo and a.PAC_CODIGO = p.PAC_CODIGO\n" +
            //" ORDER BY ATE_FECHA, ATE_FACTURA_PACIENTE, cc.CAT_NOMBRE", Sqlcon);

            //CAMBIOS EDGAR 20211026
            Sqlcmd = new SqlCommand("SELECT A.ATE_NUMERO_ATENCION AS 'Nº ATENCION', H.hab_Numero AS 'HABIT.',"
            + " P.PAC_HISTORIA_CLINICA AS HC, A.ATE_FECHA_INGRESO AS 'F. INGRESO',"
            + " A.ATE_FECHA_ALTA AS 'F. ALTA', A.ATE_FACTURA_PACIENTE AS 'Nº FACTURA',"
            + " P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS PACIENTE,"
            + " P.PAC_IDENTIFICACION AS IDENTIFICACION, P.PAC_GENERO AS SEXO, P.PAC_FECHA_NACIMIENTO AS 'F. NACIMIENTO',"
            + " (CAST(CONVERT(VARCHAR(8), GETDATE(), 112) AS INT) - CAST(CONVERT(VARCHAR(8), P.PAC_FECHA_NACIMIENTO, 112) AS INT)) / 10000 AS EDAD,"
            + " (SELECT DP.DIPO_NOMBRE FROM DIVISION_POLITICA DP WHERE PDA.COD_PAIS = DP.DIPO_CODIINEC) AS PAIS, "
            + " P.PAC_NACIONALIDAD AS NACIONALIDAD, (SELECT DP.DIPO_NOMBRE FROM DIVISION_POLITICA DP WHERE PDA.COD_PROVINCIA = DP.DIPO_CODIINEC) AS PROVINCIA,"
            + " (SELECT DP.DIPO_NOMBRE FROM DIVISION_POLITICA DP WHERE PDA.COD_CANTON = DP.DIPO_CODIINEC) AS CANTON,"
            + " (SELECT DP.DIPO_NOMBRE FROM DIVISION_POLITICA DP WHERE PDA.COD_PARROQUIA = DP.DIPO_CODIINEC) AS PARROQUIA, E.E_NOMBRE AS ETNIA,"
            + " PDA.DAP_DIRECCION_DOMICILIO AS DIRECCION, CC.CAT_NOMBRE AS ASEGURADORA, TIT.TIA_DESCRIPCION AS 'TIPO TRATAMIENTO',"
            + " M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO,"
            + " EM.ESP_NOMBRE AS ESPECIALIDAD, TI.TIP_DESCRIPCION AS 'TIPO INGRESO', A.ATE_DIAGNOSTICO_INICIAL AS 'DIAG. INICIAL',"
            + " 'ESTADO' = CASE WHEN PDA2.FALLECIDO = 0 THEN '1' ELSE "
            + " (CASE WHEN DATEDIFF(hh, FECHA_FALLECIDO, GETDATE()) < 48 THEN '2' ELSE '3' END) END "
            + " FROM PACIENTES P"
            + " INNER JOIN ATENCIONES A ON P.PAC_CODIGO = A.PAC_CODIGO"
            + " INNER JOIN PACIENTES_DATOS_ADICIONALES PDA ON P.PAC_CODIGO = PDA.PAC_CODIGO"
            + " INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO"
            + " INNER JOIN ESPECIALIDADES_MEDICAS EM ON M.ESP_CODIGO = EM.ESP_CODIGO"
            + " INNER JOIN ETNIA E ON P.E_CODIGO = E.E_CODIGO"
            + " INNER JOIN HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo"
            + " INNER JOIN ATENCION_DETALLE_CATEGORIAS ADC ON A.ATE_CODIGO = ADC.ATE_CODIGO"
            + " INNER JOIN CATEGORIAS_CONVENIOS CC ON ADC.CAT_CODIGO = CC.CAT_CODIGO"
            + " INNER JOIN TIPO_INGRESO TI ON A.TIP_CODIGO = TI.TIP_CODIGO"
            + " INNER JOIN PACIENTES_DATOS_ADICIONALES2 PDA2 ON P.PAC_CODIGO = PDA2.PAC_CODIGO"
            + " INNER JOIN TIPO_TRATAMIENTO TIT ON A.TIA_CODIGO = TIT.TIA_CODIGO"
            + " WHERE A.ATE_FECHA BETWEEN '" + desde + "' AND '" + hasta + "' ORDER BY 1 ASC", Sqlcon);
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

        public List<DtoPacientesAtencionesActivas> SinAnamnesis()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataReader reader;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            List<DtoPacientesAtencionesActivas> lista = new List<DtoPacientesAtencionesActivas>();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("sp_SinAnamnesis", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.CommandTimeout = 180;
            reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                DtoPacientesAtencionesActivas objpacientes = new DtoPacientesAtencionesActivas();
                objpacientes.codigoHabitacion = Convert.ToInt16(reader["hab_Codigo"].ToString());
                objpacientes.numeroHabitacion = reader["HABITACION"].ToString();
                objpacientes.cedula = reader["IDENTIFICACION"].ToString();
                objpacientes.nombrePaciente = reader["PACIENTE"].ToString();
                objpacientes.historiaClincia = reader["HC"].ToString();
                objpacientes.numeroAtencion = reader["ATENUMERO"].ToString();
                objpacientes.codAtencion = Convert.ToInt32(reader["ATE_CODIGO"].ToString());
                objpacientes.sexo = reader["GENERO"].ToString();
                objpacientes.aseguradora = reader["ASEGURADORA"].ToString();
                objpacientes.fechaIngreso = Convert.ToDateTime(reader["INGRESO"].ToString());
                objpacientes.medicoTratante = reader["MEDICO"].ToString();
                objpacientes.tipoTratamiento = reader["TRATAMIENTO"].ToString();
                objpacientes.diagnosticoInicial = reader["DIAGNOSTICO"].ToString();
                objpacientes.DiasHospitalizado = Convert.ToInt32(reader["DIAS"].ToString());
                objpacientes.FechaIngresoString = reader["INGRESO"].ToString();
                lista.Add(objpacientes);
            }
            reader.Close();
            Sqlcon.Close();
            return lista;
        }
        public List<DtoPacientesAtencionesActivas> SinEpicrisis()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataReader reader;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            List<DtoPacientesAtencionesActivas> lista = new List<DtoPacientesAtencionesActivas>();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("sp_SinEpicrisis", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.CommandTimeout = 180;
            reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                DtoPacientesAtencionesActivas objpacientes = new DtoPacientesAtencionesActivas();
                objpacientes.codigoHabitacion = Convert.ToInt16(reader["hab_Codigo"].ToString());
                objpacientes.numeroHabitacion = reader["HABITACION"].ToString();
                objpacientes.cedula = reader["IDENTIFICACION"].ToString();
                objpacientes.nombrePaciente = reader["PACIENTE"].ToString();
                objpacientes.historiaClincia = reader["HC"].ToString();
                objpacientes.numeroAtencion = reader["ATENUMERO"].ToString();
                objpacientes.codAtencion = Convert.ToInt32(reader["ATE_CODIGO"].ToString());
                objpacientes.sexo = reader["GENERO"].ToString();
                objpacientes.aseguradora = reader["ASEGURADORA"].ToString();
                objpacientes.fechaIngreso = Convert.ToDateTime(reader["INGRESO"].ToString());
                objpacientes.medicoTratante = reader["MEDICO"].ToString();
                objpacientes.tipoTratamiento = reader["TRATAMIENTO"].ToString();
                objpacientes.diagnosticoInicial = reader["DIAGNOSTICO"].ToString();
                objpacientes.DiasHospitalizado = Convert.ToInt32(reader["DIAS"].ToString());
                objpacientes.FechaIngresoString = reader["INGRESO"].ToString();
                lista.Add(objpacientes);
            }
            reader.Close();
            Sqlcon.Close();
            return lista;
        }

        public List<DtoPacientesAtencionesActivas> SinEmergencia()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataReader reader;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            List<DtoPacientesAtencionesActivas> lista = new List<DtoPacientesAtencionesActivas>();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("sp_SinEmergencia", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.CommandTimeout = 180;
            reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                DtoPacientesAtencionesActivas objpacientes = new DtoPacientesAtencionesActivas();
                objpacientes.codigoHabitacion = Convert.ToInt16(reader["hab_Codigo"].ToString());
                objpacientes.numeroHabitacion = reader["HABITACION"].ToString();
                objpacientes.cedula = reader["IDENTIFICACION"].ToString();
                objpacientes.nombrePaciente = reader["PACIENTE"].ToString();
                objpacientes.historiaClincia = reader["HC"].ToString();
                objpacientes.numeroAtencion = reader["ATENUMERO"].ToString();
                objpacientes.codAtencion = Convert.ToInt32(reader["ATE_CODIGO"].ToString());
                objpacientes.sexo = reader["GENERO"].ToString();
                objpacientes.aseguradora = reader["ASEGURADORA"].ToString();
                objpacientes.fechaIngreso = Convert.ToDateTime(reader["INGRESO"].ToString());
                objpacientes.medicoTratante = reader["MEDICO"].ToString();
                objpacientes.tipoTratamiento = reader["TRATAMIENTO"].ToString();
                objpacientes.diagnosticoInicial = reader["DIAGNOSTICO"].ToString();
                objpacientes.DiasHospitalizado = Convert.ToInt32(reader["DIAS"].ToString());
                objpacientes.FechaIngresoString = reader["INGRESO"].ToString();
                lista.Add(objpacientes);
            }
            reader.Close();
            Sqlcon.Close();
            return lista;
        }

        public List<DtoPacientesAtencionesActivas> SinProtocolo()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataReader reader;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            List<DtoPacientesAtencionesActivas> lista = new List<DtoPacientesAtencionesActivas>();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("sp_SinProtocolo", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.CommandTimeout = 180;
            reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                DtoPacientesAtencionesActivas objpacientes = new DtoPacientesAtencionesActivas();
                objpacientes.codigoHabitacion = Convert.ToInt16(reader["hab_Codigo"].ToString());
                objpacientes.numeroHabitacion = reader["HABITACION"].ToString();
                objpacientes.cedula = reader["IDENTIFICACION"].ToString();
                objpacientes.nombrePaciente = reader["PACIENTE"].ToString();
                objpacientes.historiaClincia = reader["HC"].ToString();
                objpacientes.numeroAtencion = reader["ATENUMERO"].ToString();
                objpacientes.codAtencion = Convert.ToInt32(reader["ATE_CODIGO"].ToString());
                objpacientes.sexo = reader["GENERO"].ToString();
                objpacientes.aseguradora = reader["ASEGURADORA"].ToString();
                objpacientes.fechaIngreso = Convert.ToDateTime(reader["INGRESO"].ToString());
                objpacientes.medicoTratante = reader["MEDICO"].ToString();
                objpacientes.tipoTratamiento = reader["TRATAMIENTO"].ToString();
                objpacientes.diagnosticoInicial = reader["DIAGNOSTICO"].ToString();
                objpacientes.DiasHospitalizado = Convert.ToInt32(reader["DIAS"].ToString());
                objpacientes.FechaIngresoString = reader["INGRESO"].ToString();
                lista.Add(objpacientes);
            }
            reader.Close();
            Sqlcon.Close();
            return lista;
        }

        public List<DtoPacientesAtencionesActivas> Todos()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataReader reader;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            List<DtoPacientesAtencionesActivas> lista = new List<DtoPacientesAtencionesActivas>();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("sp_Todos", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.CommandTimeout = 180;
            reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                DtoPacientesAtencionesActivas objpacientes = new DtoPacientesAtencionesActivas();
                objpacientes.codigoHabitacion = Convert.ToInt16(reader["hab_Codigo"].ToString());
                objpacientes.numeroHabitacion = reader["HABITACION"].ToString();
                objpacientes.cedula = reader["IDENTIFICACION"].ToString();
                objpacientes.nombrePaciente = reader["PACIENTE"].ToString();
                objpacientes.historiaClincia = reader["HC"].ToString();
                objpacientes.numeroAtencion = reader["ATENUMERO"].ToString();
                objpacientes.codAtencion = Convert.ToInt32(reader["ATE_CODIGO"].ToString());
                objpacientes.sexo = reader["GENERO"].ToString();
                objpacientes.aseguradora = reader["ASEGURADORA"].ToString();
                objpacientes.fechaIngreso = Convert.ToDateTime(reader["INGRESO"].ToString());
                objpacientes.medicoTratante = reader["MEDICO"].ToString();
                objpacientes.tipoTratamiento = reader["TRATAMIENTO"].ToString();
                objpacientes.diagnosticoInicial = reader["DIAGNOSTICO"].ToString();
                objpacientes.DiasHospitalizado = Convert.ToInt32(reader["DIAS"].ToString());
                objpacientes.FechaIngresoString = reader["INGRESO"].ToString();
                lista.Add(objpacientes);
            }
            reader.Close();
            Sqlcon.Close();
            return lista;
        }

        public List<DtoPacientesAtencionesActivas> TipoEmergencia()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataReader reader;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            List<DtoPacientesAtencionesActivas> lista = new List<DtoPacientesAtencionesActivas>();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("sp_TipoEmergencia", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.CommandTimeout = 180;
            reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                DtoPacientesAtencionesActivas objpacientes = new DtoPacientesAtencionesActivas();
                objpacientes.codigoHabitacion = Convert.ToInt16(reader["hab_Codigo"].ToString());
                objpacientes.numeroHabitacion = reader["HABITACION"].ToString();
                objpacientes.cedula = reader["IDENTIFICACION"].ToString();
                objpacientes.nombrePaciente = reader["PACIENTE"].ToString();
                objpacientes.historiaClincia = reader["HC"].ToString();
                objpacientes.numeroAtencion = reader["ATENUMERO"].ToString();
                objpacientes.codAtencion = Convert.ToInt32(reader["ATE_CODIGO"].ToString());
                objpacientes.sexo = reader["GENERO"].ToString();
                objpacientes.aseguradora = reader["ASEGURADORA"].ToString();
                objpacientes.fechaIngreso = Convert.ToDateTime(reader["INGRESO"].ToString());
                objpacientes.medicoTratante = reader["MEDICO"].ToString();
                objpacientes.tipoTratamiento = reader["TRATAMIENTO"].ToString();
                objpacientes.diagnosticoInicial = reader["DIAGNOSTICO"].ToString();
                objpacientes.DiasHospitalizado = Convert.ToInt32(reader["DIAS"].ToString());
                objpacientes.FechaIngresoString = reader["INGRESO"].ToString();
                lista.Add(objpacientes);
            }
            reader.Close();
            Sqlcon.Close();
            return lista;
        }

        public List<DtoPacientesAtencionesActivas> TipoHospitalizacion()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataReader reader;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            List<DtoPacientesAtencionesActivas> lista = new List<DtoPacientesAtencionesActivas>();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("sp_TipoHospitalizacion", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.CommandTimeout = 180;
            reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                DtoPacientesAtencionesActivas objpacientes = new DtoPacientesAtencionesActivas();
                objpacientes.codigoHabitacion = Convert.ToInt16(reader["hab_Codigo"].ToString());
                objpacientes.numeroHabitacion = reader["HABITACION"].ToString();
                objpacientes.cedula = reader["IDENTIFICACION"].ToString();
                objpacientes.nombrePaciente = reader["PACIENTE"].ToString();
                objpacientes.historiaClincia = reader["HC"].ToString();
                objpacientes.numeroAtencion = reader["ATENUMERO"].ToString();
                objpacientes.codAtencion = Convert.ToInt32(reader["ATE_CODIGO"].ToString());
                objpacientes.sexo = reader["GENERO"].ToString();
                objpacientes.aseguradora = reader["ASEGURADORA"].ToString();
                objpacientes.fechaIngreso = Convert.ToDateTime(reader["INGRESO"].ToString());
                objpacientes.medicoTratante = reader["MEDICO"].ToString();
                objpacientes.tipoTratamiento = reader["TRATAMIENTO"].ToString();
                objpacientes.diagnosticoInicial = reader["DIAGNOSTICO"].ToString();
                objpacientes.DiasHospitalizado = Convert.ToInt32(reader["DIAS"].ToString());
                objpacientes.FechaIngresoString = reader["INGRESO"].ToString();
                lista.Add(objpacientes);
            }
            reader.Close();
            Sqlcon.Close();
            return lista;
        }

        public List<DtoPacientesAtencionesActivas> TipoOtros()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataReader reader;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            List<DtoPacientesAtencionesActivas> lista = new List<DtoPacientesAtencionesActivas>();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("sp_TipoOtros", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.CommandTimeout = 180;
            reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                DtoPacientesAtencionesActivas objpacientes = new DtoPacientesAtencionesActivas();
                objpacientes.codigoHabitacion = Convert.ToInt16(reader["hab_Codigo"].ToString());
                objpacientes.numeroHabitacion = reader["HABITACION"].ToString();
                objpacientes.cedula = reader["IDENTIFICACION"].ToString();
                objpacientes.nombrePaciente = reader["PACIENTE"].ToString();
                objpacientes.historiaClincia = reader["HC"].ToString();
                objpacientes.numeroAtencion = reader["ATENUMERO"].ToString();
                objpacientes.codAtencion = Convert.ToInt32(reader["ATE_CODIGO"].ToString());
                objpacientes.sexo = reader["GENERO"].ToString();
                objpacientes.aseguradora = reader["ASEGURADORA"].ToString();
                objpacientes.fechaIngreso = Convert.ToDateTime(reader["INGRESO"].ToString());
                objpacientes.medicoTratante = reader["MEDICO"].ToString();
                objpacientes.tipoTratamiento = reader["TRATAMIENTO"].ToString();
                objpacientes.diagnosticoInicial = reader["DIAGNOSTICO"].ToString();
                objpacientes.DiasHospitalizado = Convert.ToInt32(reader["DIAS"].ToString());
                objpacientes.FechaIngresoString = reader["INGRESO"].ToString();
                lista.Add(objpacientes);
            }
            reader.Close();
            Sqlcon.Close();
            return lista;
        }
        public string EmergenciaEstado(Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            string valido = "";

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_EmergenciaEstado", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                valido = reader["EMER_ESTADO"].ToString();
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return valido;
        }

        public List<DTOPacientesAtencion> RecuperarPacientesAntencionesMushuñan(string id, string historia, string nombre, int cantida, int estado)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();
            string xWhere = "";
            if (id != "")
            {
                if (xWhere.Length <= 0)
                    xWhere = "AND P.PAC_IDENTIFICACION = '" + id + "' ";

            }
            if (historia != "")
            {
                xWhere += " AND P.PAC_HISTORIA_CLINICA = '" + historia + "' ";
            }
            if (nombre != "")
            {
                xWhere += "AND P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO  LIKE '%" + nombre + "%' ";
            }
            if (estado == 6)
            {
                xWhere += "AND A.ESC_CODIGO = " + estado + " ";
            }



            command = new SqlCommand("SELECT p.PAC_CODIGO, A.ATE_CODIGO, ISNULL(A.ATE_FECHA_ALTA, GETDATE()) AS ATE_FECHA_ALTA, A.ATE_FECHA_INGRESO, "
            + "P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 as PACIENTE, "
            + "P.PAC_IDENTIFICACION, P.PAC_HISTORIA_CLINICA, H.hab_Numero, A.ATE_NUMERO_ATENCION, CC.CAT_NOMBRE, A.ESC_CODIGO, "
            + "A.ATE_FACTURA_PACIENTE, ISNULL(A.ATE_FACTURA_FECHA, GETDATE()) AS ATE_FACTURA_FECHA "
            + "FROM ATENCIONES A "
            + "INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO "
            + "INNER JOIN ATENCION_DETALLE_CATEGORIAS ADC ON A.ATE_CODIGO = ADC.ATE_CODIGO "
            + "INNER JOIN CATEGORIAS_CONVENIOS CC ON ADC.CAT_CODIGO = CC.CAT_CODIGO "
            + "INNER JOIN HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo "
            + "WHERE A.TIP_CODIGO = 10 " + xWhere, connection);
            command.CommandType = CommandType.Text;
            //command = new SqlCommand("sp_PacientesMushuñan", connection);
            //command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();

            Tabla.Load(reader);
            reader.Close();
            connection.Close();

            List<DTOPacientesAtencion> result = new List<DTOPacientesAtencion>();
            foreach (DataRow item in Tabla.Rows)
            {
                DTOPacientesAtencion Apacientes = new DTOPacientesAtencion();
                Apacientes.ate_codigo = item["ATE_CODIGO"].ToString();
                Apacientes.NOMBRE = item["PACIENTE"].ToString();
                Apacientes.F_Ingreso = Convert.ToDateTime(item["ATE_FECHA_INGRESO"].ToString());
                Apacientes.ID = item["PAC_IDENTIFICACION"].ToString();
                Apacientes.HCL = item["PAC_HISTORIA_CLINICA"].ToString();
                Apacientes.ATENCION = item["ATE_NUMERO_ATENCION"].ToString();

                result.Add(Apacientes);
            }
            return result.Take(cantida).ToList();
        }
        public List<DTOPacientesAtencion> RecuperarPacientesAtenciones(string id, string historia, string nombre, int cantidad)
        {
            List<DTOPacientesAtencion> pacientes = new List<DTOPacientesAtencion>();
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {

                    if (id == string.Empty && historia == string.Empty && nombre == string.Empty)
                    {
                        var result = (from a in contexto.ATENCIONES
                                      join p in contexto.PACIENTES
                                      on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                      where a.ESC_CODIGO == 1 || a.ESC_CODIGO == 2 || a.ESC_CODIGO == 6
                                      select new
                                      {
                                          a,
                                          p
                                      } into x
                                      group x by new
                                      {
                                          x.a.ATE_NUMERO_ATENCION,
                                          x.a.ATE_FECHA_INGRESO,
                                          x.p.PAC_HISTORIA_CLINICA,
                                          x.p.PAC_APELLIDO_PATERNO,
                                          x.p.PAC_APELLIDO_MATERNO,
                                          x.p.PAC_NOMBRE1,
                                          x.p.PAC_NOMBRE2,
                                          x.p.PAC_IDENTIFICACION
                                      } into y
                                      select y);
                        //var result = (contexto.ATENCIONES.GroupJoin(contexto.PACIENTES, a => a.PACIENTES.PAC_CODIGO,
                        //    p => p.PAC_CODIGO, (a, p) => new { a, p }).FirstOrDefault(x => x.a.ESC_CODIGO == 1));


                        foreach (var item in result)
                        {
                            var atencion = (from at in contexto.ATENCIONES
                                            where at.ATE_NUMERO_ATENCION == item.Key.ATE_NUMERO_ATENCION
                                            select at).FirstOrDefault();
                            DTOPacientesAtencion pacientesAtencion = new DTOPacientesAtencion();
                            pacientesAtencion.ATENCION = item.Key.ATE_NUMERO_ATENCION;
                            pacientesAtencion.ate_codigo = atencion.ATE_CODIGO.ToString();
                            pacientesAtencion.HCL = item.Key.PAC_HISTORIA_CLINICA;
                            pacientesAtencion.ID = item.Key.PAC_IDENTIFICACION;
                            pacientesAtencion.NOMBRE = item.Key.PAC_APELLIDO_PATERNO + " " + item.Key.PAC_APELLIDO_MATERNO + " " + item.Key.PAC_NOMBRE1 + " " + item.Key.PAC_NOMBRE2;
                            pacientesAtencion.F_Ingreso = (DateTime)atencion.ATE_FECHA_INGRESO;
                            pacientes.Add(pacientesAtencion);
                        }

                        return pacientes.Take(cantidad).ToList();
                    }
                    else
                    {
                        if (id != string.Empty)
                        {
                            var result = (from a in contexto.ATENCIONES
                                          join p in contexto.PACIENTES
                                          on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                          where a.ESC_CODIGO == 1 && p.PAC_IDENTIFICACION == id
                                          select new
                                          {
                                              a,
                                              p
                                          } into x
                                          group x by new
                                          {
                                              x.a.ATE_NUMERO_ATENCION,
                                              x.a.ATE_FECHA_INGRESO,
                                              x.p.PAC_HISTORIA_CLINICA,
                                              x.p.PAC_APELLIDO_PATERNO,
                                              x.p.PAC_APELLIDO_MATERNO,
                                              x.p.PAC_NOMBRE1,
                                              x.p.PAC_NOMBRE2,
                                              x.p.PAC_IDENTIFICACION
                                          } into y
                                          select y);
                            //var result = (contexto.ATENCIONES.GroupJoin(contexto.PACIENTES, a => a.PACIENTES.PAC_CODIGO,
                            //    p => p.PAC_CODIGO, (a, p) => new { a, p }).FirstOrDefault(x => x.a.ESC_CODIGO == 1));


                            foreach (var item in result)
                            {
                                var atencion = (from at in contexto.ATENCIONES
                                                where at.ATE_NUMERO_ATENCION == item.Key.ATE_NUMERO_ATENCION
                                                select at).FirstOrDefault();
                                DTOPacientesAtencion pacientesAtencion = new DTOPacientesAtencion();
                                pacientesAtencion.ATENCION = item.Key.ATE_NUMERO_ATENCION;
                                pacientesAtencion.ate_codigo = atencion.ATE_CODIGO.ToString();
                                pacientesAtencion.HCL = item.Key.PAC_HISTORIA_CLINICA;
                                pacientesAtencion.ID = item.Key.PAC_IDENTIFICACION;
                                pacientesAtencion.NOMBRE = item.Key.PAC_APELLIDO_PATERNO + " " + item.Key.PAC_APELLIDO_MATERNO + " " + item.Key.PAC_NOMBRE1 + " " + item.Key.PAC_NOMBRE2;
                                pacientesAtencion.F_Ingreso = (DateTime)atencion.ATE_FECHA_INGRESO;
                                pacientes.Add(pacientesAtencion);
                            }
                            return pacientes.Take(cantidad).ToList();
                        }

                        if (historia != string.Empty)
                        {
                            var result = (from a in contexto.ATENCIONES
                                          join p in contexto.PACIENTES
                                          on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                          where a.ESC_CODIGO == 1 && p.PAC_HISTORIA_CLINICA == historia
                                          select new
                                          {
                                              a,
                                              p
                                          } into x
                                          group x by new
                                          {
                                              x.a.ATE_NUMERO_ATENCION,
                                              x.a.ATE_FECHA_INGRESO,
                                              x.p.PAC_HISTORIA_CLINICA,
                                              x.p.PAC_APELLIDO_PATERNO,
                                              x.p.PAC_APELLIDO_MATERNO,
                                              x.p.PAC_NOMBRE1,
                                              x.p.PAC_NOMBRE2,
                                              x.p.PAC_IDENTIFICACION
                                          } into y
                                          select y);
                            //var result = (contexto.ATENCIONES.GroupJoin(contexto.PACIENTES, a => a.PACIENTES.PAC_CODIGO,
                            //    p => p.PAC_CODIGO, (a, p) => new { a, p }).FirstOrDefault(x => x.a.ESC_CODIGO == 1));


                            foreach (var item in result)
                            {
                                var atencion = (from at in contexto.ATENCIONES
                                                where at.ATE_NUMERO_ATENCION == item.Key.ATE_NUMERO_ATENCION
                                                select at).FirstOrDefault();
                                DTOPacientesAtencion pacientesAtencion = new DTOPacientesAtencion();
                                pacientesAtencion.ATENCION = item.Key.ATE_NUMERO_ATENCION;
                                pacientesAtencion.ate_codigo = atencion.ATE_CODIGO.ToString();
                                pacientesAtencion.HCL = item.Key.PAC_HISTORIA_CLINICA;
                                pacientesAtencion.ID = item.Key.PAC_IDENTIFICACION;
                                pacientesAtencion.NOMBRE = item.Key.PAC_APELLIDO_PATERNO + " " + item.Key.PAC_APELLIDO_MATERNO + " " + item.Key.PAC_NOMBRE1 + " " + item.Key.PAC_NOMBRE2;
                                pacientesAtencion.F_Ingreso = (DateTime)atencion.ATE_FECHA_INGRESO;
                                pacientes.Add(pacientesAtencion);
                            }

                            return pacientes.Take(cantidad).ToList();
                        }

                        if (nombre != string.Empty)
                        {
                            if (nombre.Length == 1)
                            {
                                var result = (from a in contexto.ATENCIONES
                                              join p in contexto.PACIENTES
                                              on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                              where a.ESC_CODIGO == 1 && p.PAC_APELLIDO_PATERNO.StartsWith(nombre)
                                              select new
                                              {
                                                  a,
                                                  p
                                              } into x
                                              group x by new
                                              {
                                                  x.a.ATE_NUMERO_ATENCION,
                                                  x.a.ATE_FECHA_INGRESO,
                                                  x.p.PAC_HISTORIA_CLINICA,
                                                  x.p.PAC_APELLIDO_PATERNO,
                                                  x.p.PAC_APELLIDO_MATERNO,
                                                  x.p.PAC_NOMBRE1,
                                                  x.p.PAC_NOMBRE2,
                                                  x.p.PAC_IDENTIFICACION
                                              } into y
                                              select y);
                                //var result = (contexto.ATENCIONES.GroupJoin(contexto.PACIENTES, a => a.PACIENTES.PAC_CODIGO,
                                //    p => p.PAC_CODIGO, (a, p) => new { a, p }).FirstOrDefault(x => x.a.ESC_CODIGO == 1));


                                foreach (var item in result)
                                {
                                    var atencion = (from at in contexto.ATENCIONES
                                                    where at.ATE_NUMERO_ATENCION == item.Key.ATE_NUMERO_ATENCION
                                                    select at).FirstOrDefault();
                                    DTOPacientesAtencion pacientesAtencion = new DTOPacientesAtencion();
                                    pacientesAtencion.ATENCION = item.Key.ATE_NUMERO_ATENCION;
                                    pacientesAtencion.ate_codigo = atencion.ATE_CODIGO.ToString();
                                    pacientesAtencion.HCL = item.Key.PAC_HISTORIA_CLINICA;
                                    pacientesAtencion.ID = item.Key.PAC_IDENTIFICACION;
                                    pacientesAtencion.NOMBRE = item.Key.PAC_APELLIDO_PATERNO + " " + item.Key.PAC_APELLIDO_MATERNO + " " + item.Key.PAC_NOMBRE1 + " " + item.Key.PAC_NOMBRE2;
                                    pacientesAtencion.F_Ingreso = (DateTime)atencion.ATE_FECHA_INGRESO;
                                    pacientes.Add(pacientesAtencion);
                                }

                                return pacientes.Take(cantidad).ToList();
                            }
                            else
                            {
                                var result = (from a in contexto.ATENCIONES
                                              join p in contexto.PACIENTES
                                              on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                              where a.ESC_CODIGO == 1 && p.PAC_APELLIDO_PATERNO.Contains(nombre)
                                              select new
                                              {
                                                  a,
                                                  p
                                              } into x
                                              group x by new
                                              {
                                                  x.a.ATE_NUMERO_ATENCION,
                                                  x.a.ATE_FECHA_INGRESO,
                                                  x.p.PAC_HISTORIA_CLINICA,
                                                  x.p.PAC_APELLIDO_PATERNO,
                                                  x.p.PAC_APELLIDO_MATERNO,
                                                  x.p.PAC_NOMBRE1,
                                                  x.p.PAC_NOMBRE2,
                                                  x.p.PAC_IDENTIFICACION
                                              } into y
                                              select y);
                                //var result = (contexto.ATENCIONES.GroupJoin(contexto.PACIENTES, a => a.PACIENTES.PAC_CODIGO,
                                //    p => p.PAC_CODIGO, (a, p) => new { a, p }).FirstOrDefault(x => x.a.ESC_CODIGO == 1));


                                foreach (var item in result)
                                {
                                    var atencion = (from at in contexto.ATENCIONES
                                                    where at.ATE_NUMERO_ATENCION == item.Key.ATE_NUMERO_ATENCION
                                                    select at).FirstOrDefault();
                                    DTOPacientesAtencion pacientesAtencion = new DTOPacientesAtencion();
                                    pacientesAtencion.ATENCION = item.Key.ATE_NUMERO_ATENCION;
                                    pacientesAtencion.ate_codigo = atencion.ATE_CODIGO.ToString();
                                    pacientesAtencion.HCL = item.Key.PAC_HISTORIA_CLINICA;
                                    pacientesAtencion.ID = item.Key.PAC_IDENTIFICACION;
                                    pacientesAtencion.NOMBRE = item.Key.PAC_APELLIDO_PATERNO + " " + item.Key.PAC_APELLIDO_MATERNO + " " + item.Key.PAC_NOMBRE1 + " " + item.Key.PAC_NOMBRE2;
                                    pacientesAtencion.F_Ingreso = (DateTime)atencion.ATE_FECHA_INGRESO;
                                    pacientes.Add(pacientesAtencion);
                                }
                                return pacientes.Take(cantidad).ToList();
                            }
                        }
                    }
                }
                return pacientes.Take(cantidad).ToList();
            }
            catch (Exception err)
            {
                Console.Write(err.Message);
                return pacientes.Take(cantidad).ToList();
            }
        }
        public PACIENTES recuperarPorIdentificacion(string identificacion)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PACIENTES pa = (from p in db.PACIENTES
                                where p.PAC_IDENTIFICACION == identificacion
                                select p).FirstOrDefault();
                return pa;
            }
        }

        public DataTable getPedidosLaboratorioClinico(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fhc, int hc, bool Fformulario, string formulario, bool chkCerrado)
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
            string xWhere = "";
            int count = 0;
            if (ingreso)
            {
                xWhere += "  A.ATE_FECHA_INGRESO BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (alta)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  A.ATE_FECHA_ALTA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (facturacion)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  A.ATE_FACTURA_FECHA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (count > 0)
                xWhere = " (" + xWhere + ") ";
            if (Fingreso)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIP_CODIGO=" + Cod_Ingreso + " ";
                count++;
            }

            if (Ftratamiento)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIA_CODIGO=" + Cod_Tratamiento + " ";
                count++;
            }

            if (Fhc)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.PAC_HISTORIA_CLINICA=" + hc + " ";
                count++;
            }

            if (Fformulario)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  F.TIPO='" + formulario + "' ";
                count++;
            }
            if (chkCerrado)
            {
                xWhere += " AND ";
                xWhere += "  F.LCL_ESTADO = 1 ";
                count++;
            }
            if (count > 0)
                xWhere = "Where " + xWhere;

            Sqlcmd = new SqlCommand("SELECT F.LCL_CODIGO,F.LCL_ESTADO,F.LCL_FECHA_CREACION ,F.TIPO,  A.PAC_HISTORIA_CLINICA, A.ATE_NUMERO_ATENCION,  A.NOMBRE_PACIENTE, F.ID_Formulario,\n"
                        + "A.ATE_CODIGO, A.HAB_CODIGO, A.hab_Numero, A.HABESTADO, A.PAC_CODIGO, A.PAC_IDENTIFICACION, A.PAC_GENERO, A.ATE_DIAGNOSTICO_INICIAL,\n"
                        + "A.MED_CODIGO, A.NOMBRE_MEDICO, A.TIA_DESCRIPCION, A.ATE_FECHA_INGRESO, A.ATE_FECHA_ALTA, A.ATE_FACTURA_FECHA, A.ATE_FACTURA_PACIENTE,\n"
                        + "A.NOMBRE_USUARIO, A.TIP_DESCRIPCION, A.ADS_ASEGURADO_NOMBRE, A.ADS_ASEGURADO_CEDULA, A.PARENTESCO\n"
                        + "FROM \n"
                        + " (SELECT LCL_CODIGO,LCL_ESTADO,LCL_PRIORIDAD_U,LCL_PRIORIDAD_R,LCL_PRIORIDAD_C, ATE_CODIGO, LCL_CODIGO AS ID_Formulario, 'LABORATORIO CLINICO' AS TIPO,\n"
                        + "dbo.HC_LABORATORIO_CLINICO.LCL_FECHA_CREACION  FROM dbo.HC_LABORATORIO_CLINICO where dbo.HC_LABORATORIO_CLINICO.LCL_FECHA_CREACION between'" + desde + "' and '" + hasta + "') F\n"
                        + "    JOIN(\n"
                        + "SELECT        A.ATE_CODIGO, A.HAB_CODIGO, H.hab_Numero, A.PAC_CODIGO, P.PAC_IDENTIFICACION, P.PAC_APELLIDO_PATERNO + SPACE(1) + P.PAC_APELLIDO_MATERNO + SPACE(1) + P.PAC_NOMBRE1 + SPACE(1) \n"
                     + "+ P.PAC_NOMBRE2 AS NOMBRE_PACIENTE, P.PAC_HISTORIA_CLINICA, A.ATE_NUMERO_ATENCION, P.PAC_GENERO, A.TIF_OBSERVACION, A.ATE_FECHA_INGRESO, A.ATE_FECHA_ALTA, A.ATE_FACTURA_FECHA,\n"
                     + "A.ATE_FACTURA_PACIENTE, A.MED_CODIGO, M.MED_APELLIDO_PATERNO + SPACE(1) + M.MED_APELLIDO_MATERNO + SPACE(1) + M.MED_NOMBRE1 + SPACE(1) + M.MED_NOMBRE2 AS NOMBRE_MEDICO,\n"
                     + "TT.TIA_DESCRIPCION, A.ATE_DIAGNOSTICO_INICIAL, U.APELLIDOS + SPACE(1) + U.NOMBRES AS NOMBRE_USUARIO, TIP.TIP_DESCRIPCION, ADS.ADS_ASEGURADO_NOMBRE, ADS.ADS_ASEGURADO_CEDULA,\n"
                     + "AIP.ANI_DESCRIPCION AS PARENTESCO,TT.TIA_CODIGO ,TIP.TIP_CODIGO, HE.HES_NOMBRE AS HABESTADO\n"
                     + " FROM            dbo.ANEXOS_IESS AS AIP RIGHT OUTER JOIN\n"
                     + "dbo.ATENCIONES_DETALLE_SEGUROS AS ADS ON AIP.ANI_CODIGO = ADS.ADS_ASEGURADO_PARENTESCO RIGHT OUTER JOIN\n"
                     + "dbo.ATENCIONES AS A INNER JOIN\n"
                     + "dbo.PACIENTES AS P ON P.PAC_CODIGO = A.PAC_CODIGO LEFT OUTER JOIN\n"
                     + "dbo.TIPO_INGRESO AS TIP ON TIP.TIP_CODIGO = A.TIP_CODIGO LEFT OUTER JOIN\n"
                     + "dbo.MEDICOS AS M ON A.MED_CODIGO = M.MED_CODIGO LEFT OUTER JOIN\n"
                     + "dbo.HABITACIONES_ESTADO AS HE INNER JOIN\n"
                     + "dbo.HABITACIONES AS H ON HE.HES_CODIGO = H.HES_CODIGO ON A.HAB_CODIGO = H.hab_Codigo LEFT OUTER JOIN\n"
                     + "dbo.USUARIOS AS U ON U.ID_USUARIO = A.ID_USUSARIO LEFT OUTER JOIN\n"
                     + "dbo.TIPO_TRATAMIENTO AS TT ON TT.TIA_CODIGO = A.TIA_CODIGO ON ADS.ATE_CODIGO = A.ATE_CODIGO ) A\n"
                    + "ON  F.ATE_CODIGO = A.ATE_CODIGO\n"
                    + xWhere, Sqlcon);
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
        public DataTable getPedidosLaboratorioPatologico(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fhc, int hc, bool Fformulario, string formulario, bool chkCerrado)
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
            string xWhere = "";
            int count = 0;
            if (ingreso)
            {
                xWhere += "  A.ATE_FECHA_INGRESO BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (alta)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  A.ATE_FECHA_ALTA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (facturacion)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  A.ATE_FACTURA_FECHA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (count > 0)
                xWhere = " (" + xWhere + ") ";
            if (Fingreso)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIP_CODIGO=" + Cod_Ingreso + " ";
                count++;
            }

            if (Ftratamiento)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIA_CODIGO=" + Cod_Tratamiento + " ";
                count++;
            }

            if (Fhc)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.PAC_HISTORIA_CLINICA=" + hc + " ";
                count++;
            }

            if (Fformulario)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  F.TIPO='" + formulario + "' ";
                count++;
            }
            if (count > 0)
                xWhere = "Where " + xWhere;

            Sqlcmd = new SqlCommand("SELECT F.ID_Formulario,F.estado,F.histopatologia,F.citologia,F.descripcion ,F.TIPO,  A.PAC_HISTORIA_CLINICA, A.ATE_NUMERO_ATENCION,  A.NOMBRE_PACIENTE, F.ID_Formulario,\n"
                        + "A.ATE_CODIGO, A.HAB_CODIGO, A.hab_Numero, A.HABESTADO, A.PAC_CODIGO, A.PAC_IDENTIFICACION, A.PAC_GENERO, A.ATE_DIAGNOSTICO_INICIAL,\n"
                        + "A.MED_CODIGO, A.NOMBRE_MEDICO, A.TIA_DESCRIPCION, A.ATE_FECHA_INGRESO, A.ATE_FECHA_ALTA, A.ATE_FACTURA_FECHA, A.ATE_FACTURA_PACIENTE,\n"
                        + "A.NOMBRE_USUARIO, A.TIP_DESCRIPCION, A.ADS_ASEGURADO_NOMBRE, A.ADS_ASEGURADO_CEDULA, A.PARENTESCO\n"
                        + "FROM \n"
                        + " (SELECT estado,histopatologia,citologia,descripcion, ate_codigo,id AS ID_Formulario, 'HISTOPATOLOGICO' AS TIPO, \n"
                        + "fecha_creado  FROM dbo.HC_HISTOPATOLOGICO where fecha_creado between'" + desde + "' and '" + hasta + "') F\n"
                        + "    JOIN(\n"
                        + "SELECT        A.ATE_CODIGO, A.HAB_CODIGO, H.hab_Numero, A.PAC_CODIGO, P.PAC_IDENTIFICACION, P.PAC_APELLIDO_PATERNO + SPACE(1) + P.PAC_APELLIDO_MATERNO + SPACE(1) + P.PAC_NOMBRE1 + SPACE(1) \n"
                     + "+ P.PAC_NOMBRE2 AS NOMBRE_PACIENTE, P.PAC_HISTORIA_CLINICA, A.ATE_NUMERO_ATENCION, P.PAC_GENERO, A.TIF_OBSERVACION, A.ATE_FECHA_INGRESO, A.ATE_FECHA_ALTA, A.ATE_FACTURA_FECHA,\n"
                     + "A.ATE_FACTURA_PACIENTE, A.MED_CODIGO, M.MED_APELLIDO_PATERNO + SPACE(1) + M.MED_APELLIDO_MATERNO + SPACE(1) + M.MED_NOMBRE1 + SPACE(1) + M.MED_NOMBRE2 AS NOMBRE_MEDICO,\n"
                     + "TT.TIA_DESCRIPCION, A.ATE_DIAGNOSTICO_INICIAL, U.APELLIDOS + SPACE(1) + U.NOMBRES AS NOMBRE_USUARIO, TIP.TIP_DESCRIPCION, ADS.ADS_ASEGURADO_NOMBRE, ADS.ADS_ASEGURADO_CEDULA,\n"
                     + "AIP.ANI_DESCRIPCION AS PARENTESCO,TT.TIA_CODIGO ,TIP.TIP_CODIGO, HE.HES_NOMBRE AS HABESTADO\n"
                     + " FROM            dbo.ANEXOS_IESS AS AIP RIGHT OUTER JOIN\n"
                     + "dbo.ATENCIONES_DETALLE_SEGUROS AS ADS ON AIP.ANI_CODIGO = ADS.ADS_ASEGURADO_PARENTESCO RIGHT OUTER JOIN\n"
                     + "dbo.ATENCIONES AS A INNER JOIN\n"
                     + "dbo.PACIENTES AS P ON P.PAC_CODIGO = A.PAC_CODIGO LEFT OUTER JOIN\n"
                     + "dbo.TIPO_INGRESO AS TIP ON TIP.TIP_CODIGO = A.TIP_CODIGO LEFT OUTER JOIN\n"
                     + "dbo.MEDICOS AS M ON A.MED_CODIGO = M.MED_CODIGO LEFT OUTER JOIN\n"
                     + "dbo.HABITACIONES_ESTADO AS HE INNER JOIN\n"
                     + "dbo.HABITACIONES AS H ON HE.HES_CODIGO = H.HES_CODIGO ON A.HAB_CODIGO = H.hab_Codigo LEFT OUTER JOIN\n"
                     + "dbo.USUARIOS AS U ON U.ID_USUARIO = A.ID_USUSARIO LEFT OUTER JOIN\n"
                     + "dbo.TIPO_TRATAMIENTO AS TT ON TT.TIA_CODIGO = A.TIA_CODIGO ON ADS.ATE_CODIGO = A.ATE_CODIGO ) A\n"
                    + "ON  F.ATE_CODIGO = A.ATE_CODIGO\n"
                    + xWhere, Sqlcon);
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

        public List<DtoPacientesEmergencia> RecuperaPacienteCunsultaExterna(Int32 id, string historia, string nombre, int cantidad, Int16 tipoCodigo)
        {
            try
            {
                List<DtoPacientesEmergencia> pacientes = new List<DtoPacientesEmergencia>();
                List<DtoPacientesEmergencia> pacientes1 = new List<DtoPacientesEmergencia>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {

                    var result = from p in contexto.PACIENTES
                                 join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
                                 where a.TIPO_INGRESO.TIP_CODIGO == 4 && p.PAC_CODIGO == id
                                 orderby a.ATE_FECHA ascending
                                 select new DtoPacientesEmergencia
                                 {
                                     PAC_CODIGO = p.PAC_CODIGO,
                                     PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
                                     PAC_NOMBRE1 = p.PAC_NOMBRE1,
                                     PAC_NOMBRE2 = p.PAC_NOMBRE2,
                                     PAC_APELLIDO_MATERNO = p.PAC_APELLIDO_MATERNO,
                                     PAC_APELLIDO_PATERNO = p.PAC_APELLIDO_PATERNO,
                                     PAC_HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
                                     ATE_CODIGO = a.ATE_CODIGO,
                                     PAC_FECHA_ATENCION = a.ATE_FECHA_INGRESO.Value,
                                     PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO.Value
                                 };
                    return result.OrderBy(p => p.PAC_APELLIDO_PATERNO).Take(cantidad).ToList();

                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public DataTable getExploradorFormulariosCext(DateTime desde, DateTime hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fhc, int hc, bool Fformulario, string formulario, bool mushugñan, Int16 areaAsignada = 0)
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
            string xWhere = "";
            int count = 0;
            if (ingreso)
            {
                xWhere += "  cast(convert(varchar(11),A.ATE_FECHA_INGRESO,13) as datetime)>= @fechadesde And cast(convert(varchar(11),A.ATE_FECHA_INGRESO,13) as datetime)<= @fechahasta";
                count++;
            }
            if (alta)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "   CONVERT(date,A.ATE_FECHA_ALTA) BETWEEN @fechadesde AND @fechahasta";
                count++;
            }
            if (facturacion)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "   CONVERT(date,A.ATE_FACTURA_FECHA) BETWEEN @fechadesde AND @fechahasta";
                count++;
            }
            if (count > 0)
                xWhere = " (" + xWhere + ") ";


            if (Fingreso)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIP_CODIGO=" + Cod_Ingreso + " ";
                count++;
            }

            if (Ftratamiento)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.TIA_CODIGO=" + Cod_Tratamiento + " ";
                count++;
            }

            if (Fhc)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  A.PAC_HISTORIA_CLINICA=" + hc + " ";
                count++;
            }

            if (Fformulario)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  F.TIPO='" + formulario + "' ";
                count++;
            }
            if (mushugñan)
            {
                switch (areaAsignada)
                {
                    case 2:
                        if (count > 0)
                            xWhere += " AND ";
                        xWhere += " A.TIP_CODIGO = 10";
                        break;
                    case 3:
                        if (count > 0)
                            xWhere += " AND ";
                        xWhere += " A.TIP_CODIGO = 12";
                        break;
                    default:
                        if (count > 0)
                            xWhere += " AND ";
                        xWhere += " A.TIP_CODIGO = 10";
                        break;
                }

            }

            if (count > 0)
                xWhere = "Where " + xWhere;

            Sqlcmd = new SqlCommand("SELECT A.ATE_FECHA_INGRESO AS 'FECHA INGRESO' ,F.TIPO,  A.PAC_HISTORIA_CLINICA AS HC, A.ATE_NUMERO_ATENCION AS 'NUMERO ATENCION', " +
                "(ISNULL((select top 1 STRING_AGG(ED_DESCRIPCION, ', ')  from HC_EMERGENCIA_FORM e inner join  HC_EMERGENCIA_FORM_DIAGNOSTICOS d on e.EMER_CODIGO = d.EMER_CODIGO where e.ATE_CODIGO = a.ATE_CODIGO and ED_TIPO = 'I'), f.DICEX ))as 'DIAGNOSTICO INICIAL' " +
                ",(ISNULL((select top 1 STRING_AGG(ED_DESCRIPCION, ', ') from HC_EMERGENCIA_FORM e inner join  HC_EMERGENCIA_FORM_DIAGNOSTICOS d on e.EMER_CODIGO = d.EMER_CODIGO where e.ATE_CODIGO = a.ATE_CODIGO and ED_TIPO = 'A'),' ' )) as 'DIAGNOSTICO FINAL' , " +
                "A.NOMBRE_MEDICO AS 'MEDICO TRATANTE',F.[F.FORMULARIO],F.RESPONSABLE, A.NOMBRE_PACIENTE AS PACIENTE, A.TIP_DESCRIPCION AS 'TIP. INGRESO', A.ATE_FECHA_ALTA AS 'FECHA ALTA', F.ID_Formulario, A.ATE_CODIGO, A.HAB_CODIGO, A.MED_CODIGO, A.hab_Numero AS HAB,A.HABESTADO AS 'HAB ESTADO',A.PAC_CODIGO,A.PAC_IDENTIFICACION AS IDENTIFICACION,A.PAC_GENERO AS SEXO," +
                "A.TIA_DESCRIPCION AS 'TIP. TRATAMIENTO',  (select concat(CONVERT(DATE, fecha), ' ', isnull(hora, '00:00:00')) from Sic3000..Nota where numfac=A.ATE_FACTURA_PACIENTE) as ATE_FACTURA_FECHA, A.ATE_FACTURA_PACIENTE,A.NOMBRE_USUARIO AS USUARIO , A.ADS_ASEGURADO_NOMBRE,A.ADS_ASEGURADO_CEDULA, A.PARENTESCO FROM \n"
                         + "     (\n"
                          + "           SELECT        ATE_CODIGO, ANE_CODIGO AS ID_Formulario, 'ANAMNESIS' AS TIPO,'' as'DICEX',ANE_FECHA as 'F.FORMULARIO',(select APELLIDOS+' '+NOMBRES  from USUARIOS where ID_USUARIO = an.ID_USUARIO)as 'RESPONSABLE' FROM            dbo.HC_ANAMNESIS an\n"
                         + "            UNION ALL\n"
                        + "             SELECT        ATE_CODIGO, EMER_CODIGO AS ID_Formulario, 'EMERGENCIA' AS TIPO,'' as'DICEX',EMER_FECHA as 'F.FORMULARIO',(select APELLIDOS+' '+NOMBRES  from USUARIOS where ID_USUARIO = EM.ID_USUARIO)as 'RESPONSABLE'  FROM            dbo.HC_EMERGENCIA_FORM EM\n"
                         + "            UNION ALL\n"
                         + "            SELECT        ATE_CODIGO, EPI_CODIGO AS ID_Formulario, 'EPICRISIS' AS TIPO,'' as'DICEX',EPI_FECHA_CREACION as 'F.FORMULARIO',(select APELLIDOS+' '+NOMBRES  from USUARIOS where ID_USUARIO = EP.ID_USUARIO)as 'RESPONSABLE'  FROM            dbo.HC_EPICRISIS EP\n"
                         + "            UNION ALL\n"
                         + "            SELECT        ATE_CODIGO, EVO_CODIGO AS ID_Formulario, 'EVOLUCION' AS TIPO,'' as'DICEX',EVO_FECHA_CREACION as 'F.FORMULARIO',(select APELLIDOS+' '+NOMBRES  from USUARIOS where ID_USUARIO = EV.ID_USUARIO)as 'RESPONSABLE'  FROM            dbo.HC_EVOLUCION EV\n"
                         + "            UNION ALL\n"
                         + "            SELECT        ATE_CODIGO, id_imagenologia AS ID_Formulario, 'IMAGENOLOGIA' AS TIPO,'' as'DICEX',FECHA_CREACION as 'F.FORMULARIO',(select APELLIDOS+' '+NOMBRES  from USUARIOS where ID_USUARIO = IM.ID_USUARIO)as 'RESPONSABLE'  FROM            dbo.HC_IMAGENOLOGIA IM\n"
                         + "            UNION ALL\n"
                         + "            SELECT        ATE_CODIGO, HIN_CODIGO AS ID_Formulario, 'INTERCONSULTA' AS TIPO,'' as'DICEX',HIN_FECHACREACION as 'F.FORMULARIO',(select APELLIDOS+' '+NOMBRES  from USUARIOS where ID_USUARIO = IT.ID_USUARIO)as 'RESPONSABLE'  FROM            dbo.HC_INTERCONSULTA IT\n"
                         + "            UNION ALL\n"
                         + "            SELECT        ATE_CODIGO, LCL_CODIGO AS ID_Formulario, 'LABORATORIO' AS TIPO,'' as'DICEX',LCL_FECHA_CREACION as 'F.FORMULARIO',(select APELLIDOS+' '+NOMBRES  from USUARIOS where ID_USUARIO = LC.ID_USUARIO)as 'RESPONSABLE'  FROM            dbo.HC_LABORATORIO_CLINICO LC\n"
                         + "            UNION ALL\n"
                         + "            SELECT        ATE_CODIGO, ADF_CODIGO AS ID_Formulario, 'PROTOCOLO' AS TIPO,'' as'DICEX',PROT_FECHA as 'F.FORMULARIO',PO.PROT_PROFESIONAL as 'RESPONSABLE'  FROM            dbo.HC_PROTOCOLO_OPERATORIO PO\n"
                         + "            UNION ALL\n"
                         + "            SELECT AteCodigo, ID_FORM002, 'CONSULTA EXTERNA' AS TIPO,(diagnostico1cie+', '+diagnostico1+'// '+diagnostico2cie+', '+diagnostico2+'// '+diagnostico3cie+', '+diagnostico3+'// '+diagnostico4cie+', '+diagnostico4)as 'DICEX',fecha as 'F.FORMULARIO', CE.dr as 'RESPONSABLE' FROM Form002MSP CE\n"
                         + "    ) F\n"
                         + "    JOIN(\n"
                         + "           SELECT        A.ATE_CODIGO, A.HAB_CODIGO, H.hab_Numero, A.PAC_CODIGO, P.PAC_IDENTIFICACION, P.PAC_APELLIDO_PATERNO + SPACE(1) + P.PAC_APELLIDO_MATERNO + SPACE(1) + P.PAC_NOMBRE1 + SPACE(1) \n"
                      + "+ P.PAC_NOMBRE2 AS NOMBRE_PACIENTE, P.PAC_HISTORIA_CLINICA, A.ATE_NUMERO_ATENCION, P.PAC_GENERO, A.TIF_OBSERVACION, A.ATE_FECHA_INGRESO, A.ATE_FECHA_ALTA, A.ATE_FACTURA_FECHA,\n"
                      + "A.ATE_FACTURA_PACIENTE, A.MED_CODIGO, M.MED_APELLIDO_PATERNO + SPACE(1) + M.MED_APELLIDO_MATERNO + SPACE(1) + M.MED_NOMBRE1 + SPACE(1) + M.MED_NOMBRE2 AS NOMBRE_MEDICO,\n"
                      + "TT.TIA_DESCRIPCION, A.ATE_DIAGNOSTICO_INICIAL, U.APELLIDOS + SPACE(1) + U.NOMBRES AS NOMBRE_USUARIO, TIP.TIP_DESCRIPCION, ADS.ADS_ASEGURADO_NOMBRE, ADS.ADS_ASEGURADO_CEDULA,\n"
                      + "AIP.ANI_DESCRIPCION AS PARENTESCO,TT.TIA_CODIGO ,TIP.TIP_CODIGO, HE.HES_NOMBRE AS HABESTADO\n"
                      + " FROM            dbo.ANEXOS_IESS AS AIP RIGHT OUTER JOIN\n"
                      + "dbo.ATENCIONES_DETALLE_SEGUROS AS ADS ON AIP.ANI_CODIGO = ADS.ADS_ASEGURADO_PARENTESCO RIGHT OUTER JOIN\n"
                      + "dbo.ATENCIONES AS A INNER JOIN\n"
                      + "dbo.PACIENTES AS P ON P.PAC_CODIGO = A.PAC_CODIGO LEFT OUTER JOIN\n"
                      + "dbo.TIPO_INGRESO AS TIP ON TIP.TIP_CODIGO = A.TIP_CODIGO LEFT OUTER JOIN\n"
                      + "dbo.MEDICOS AS M ON A.MED_CODIGO = M.MED_CODIGO LEFT OUTER JOIN\n"
                      + "dbo.HABITACIONES_ESTADO AS HE INNER JOIN\n"
                      + "dbo.HABITACIONES AS H ON HE.HES_CODIGO = H.HES_CODIGO ON A.HAB_CODIGO = H.hab_Codigo LEFT OUTER JOIN\n"
                      + "dbo.USUARIOS AS U ON U.ID_USUARIO = A.ID_USUSARIO LEFT OUTER JOIN\n"
                      + "dbo.TIPO_TRATAMIENTO AS TT ON TT.TIA_CODIGO = A.TIA_CODIGO ON ADS.ATE_CODIGO = A.ATE_CODIGO\n"
                        + "     ) A\n"
                     + "ON  F.ATE_CODIGO = A.ATE_CODIGO\n"
                     + xWhere, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.Parameters.AddWithValue("@fechadesde", desde);
            Sqlcmd.Parameters.AddWithValue("@fechahasta", hasta);
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 280;
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

    }
}
