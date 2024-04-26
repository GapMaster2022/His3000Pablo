using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Entidades.Reportes;
using Core.Datos;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace His.Datos
{
    public class DatFormulariosHCU
    {
        public void Crear(FORMULARIOS_HCU form)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("FORMULARIOS_HCU", form);
            }
        }

        public bool CrearHistopatologico(HC_HISTOPATOLOGICO form, List<HC_HISTOPATOLOGICO_DIAGNOSTICOS> lista)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    form.fecha_creado = DateTime.Now;
                    form.fecha_pedido = DateTime.Now;
                    contexto.AddToHC_HISTOPATOLOGICO(form);
                    contexto.SaveChanges();

                    HC_HISTOPATOLOGICO obj = (from h in contexto.HC_HISTOPATOLOGICO
                                              where h.ate_codigo == form.ate_codigo
                                              orderby h.id descending
                                              select h).FirstOrDefault();
                    foreach (var item in lista)
                    {
                        HC_HISTOPATOLOGICO_DIAGNOSTICOS list = new HC_HISTOPATOLOGICO_DIAGNOSTICOS();
                        list.id_hc_histopatologico = obj.id;
                        list.codigo = item.codigo;
                        list.definitivo = item.definitivo;
                        list.presuntivo = item.presuntivo;
                        contexto.AddToHC_HISTOPATOLOGICO_DIAGNOSTICOS(list);
                        contexto.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }

        }
        public bool CrearHistopatologicoB(HC_HISTOPATOLOGICO_B form, List<HC_HISTOPATOLOGIA_B_DETALLE> lista)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    form.fecha_creacion = DateTime.Now;
                    //form.fecha_pedido = DateTime.Now;
                    contexto.AddToHC_HISTOPATOLOGICO_B(form);
                    contexto.SaveChanges();

                    HC_HISTOPATOLOGICO_B obj = (from h in contexto.HC_HISTOPATOLOGICO_B
                                                where h.ate_codigo == form.ate_codigo
                                                orderby h.id descending
                                                select h).FirstOrDefault();
                    foreach (var item in lista)
                    {
                        HC_HISTOPATOLOGIA_B_DETALLE list = new HC_HISTOPATOLOGIA_B_DETALLE();
                        list.id_histopatologico = obj.id;
                        list.codigo = item.codigo;
                        contexto.AddToHC_HISTOPATOLOGIA_B_DETALLE(list);
                        contexto.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }

        }

        public List<FORMULARIOS_HCU> RecuperarFormulariosHCU()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from f in contexto.FORMULARIOS_HCU
                        where f.FH_ESTADO == true
                        select f).ToList();

            }
        }
        public List<FRECUENCIA> RecuperarFrecuencias()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from f in contexto.FRECUENCIA
                        select f).ToList();

            }
        }
        public List<LISTADO_COMPUESTOS> RecuperaListaCompuestso()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from f in contexto.LISTADO_COMPUESTOS
                        where f.estado == true
                        select f).ToList();

            }
        }
        public List<FRECUENCIA_HORAS> RecuperarFrecuenciasHoras(int codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from f in contexto.FRECUENCIA_HORAS
                        where f.if_frecuencia == codigo
                        select f).ToList();

            }
        }

        public HC_HISTOPATOLOGICO RecuperaHistopatologico(Int64 id)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from h in contexto.HC_HISTOPATOLOGICO
                        where h.id == id
                        select h).FirstOrDefault();
            }
        }
        public CIE10 RecuperaCieCodigo(string codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from h in contexto.CIE10
                        where h.CIE_CODIGO == codigo
                        select h).FirstOrDefault();
            }
        }

        public FORMULARIOS_HCU RecupararFormularioID(int codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from f in contexto.FORMULARIOS_HCU
                        where f.FH_CODIGO == codigo
                        select f).FirstOrDefault();
            }
        }
        public int ultimoCodigoFormulario()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<FORMULARIOS_HCU> fo = (from f in contexto.FORMULARIOS_HCU
                                            select f).ToList();

                if (fo.Count > 0)
                    return fo.Max(f => f.FH_CODIGO);
                else
                    return 0;

            }
        }

        public void Editar(FORMULARIOS_HCU formulario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                FORMULARIOS_HCU formOriginal = contexto.FORMULARIOS_HCU.FirstOrDefault(f => f.FH_CODIGO == formulario.FH_CODIGO);
                formOriginal.FH_FORM = formulario.FH_FORM;
                formOriginal.FH_NOMBRE = formulario.FH_NOMBRE;
                formOriginal.FH_DESCRIPCION = formulario.FH_DESCRIPCION;
                formOriginal.FH_DIRECTORIO = formulario.FH_DIRECTORIO;
                contexto.SaveChanges();
            }
        }
        public bool EditarKardexMedicamento(int idUser, Int64 id, bool valor, string via, int aux = 1, string frecuencia = "", string observacion = "")
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                KARDEXMEDICAMENTOS obj = contexto.KARDEXMEDICAMENTOS.FirstOrDefault(f => f.ID_KARDEX_MEDICAMENTOS == id);
                obj.usuario_modifica = idUser;
                if (aux == 1)
                {
                    if (valor)
                    {
                        obj.NoAdministrado = !valor;
                        obj.Administrado = valor;
                    }
                    else
                    {
                        obj.NoAdministrado = !valor;
                        obj.Administrado = valor;
                    }
                    obj.Observacion = observacion;
                }
                obj.Via = via;
                obj.Frecuencia = frecuencia;
                obj.fecha_modifica = DateTime.Now;
                contexto.SaveChanges();
                return true;
            }
        }

        public void Eliminar(FORMULARIOS_HCU formulario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                FORMULARIOS_HCU form = (from f in contexto.FORMULARIOS_HCU
                                        select f).FirstOrDefault(f => f.FH_CODIGO == formulario.FH_CODIGO);
                contexto.DeleteObject(form);
                contexto.SaveChanges();
            }
        }

        public List<KeyValuePair<int, string>> RecuperarFormulariosLista()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<KeyValuePair<int, string>> listaFormularios = new List<KeyValuePair<int, string>>();
                var pacienteQuery = from p in contexto.FORMULARIOS_HCU
                                    select new { p.FH_CODIGO, p.FH_DESCRIPCION };
                //listaFormularios.Add(new KeyValuePair<int, string>(0, "Todos"));
                foreach (var paciente in pacienteQuery)
                {
                    listaFormularios.Add(new KeyValuePair<int, string>(paciente.FH_CODIGO, paciente.FH_DESCRIPCION));
                }
                return listaFormularios;
            }
        }


        public DataTable LlenaCombos(string tipo)
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

            Sqlcmd = new SqlCommand("sp_LlenaCombos", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@tipo", SqlDbType.VarChar);
            Sqlcmd.Parameters["@tipo"].Value = (tipo);

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

        public DataTable Paciente(string ate_codigo)
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

            Sqlcmd = new SqlCommand("sp_PacienteKardexMed", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ate_codigo", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ate_codigo"].Value = (ate_codigo);

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


        public List<KardexEnfermeriaMEdicamentos> RecuperaMedicamentos(string ate_codigo, int rubro, int check)
        {
            List<KardexEnfermeriaMEdicamentos> lista = new List<KardexEnfermeriaMEdicamentos>();
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataReader dr = null;
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

            Sqlcmd = new SqlCommand("sp_RecuperaMedicamentosKardex", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ate_codigo", SqlDbType.VarChar);
            Sqlcmd.Parameters["@ate_codigo"].Value = (ate_codigo);

            Sqlcmd.Parameters.Add("@rubro", SqlDbType.Int);
            Sqlcmd.Parameters["@rubro"].Value = (rubro);

            Sqlcmd.Parameters.Add("@check", SqlDbType.Int);
            Sqlcmd.Parameters["@check"].Value = (check);

            dr = Sqlcmd.ExecuteReader();
            while (dr.Read())
            {
                KardexEnfermeriaMEdicamentos objKardexEnfermeriaMEdicamentos = new KardexEnfermeriaMEdicamentos();
                objKardexEnfermeriaMEdicamentos.Producto = dr["PRODUCTO"].ToString();
                objKardexEnfermeriaMEdicamentos.Id = dr["CODIGO"].ToString();
                objKardexEnfermeriaMEdicamentos.Cantidad = Convert.ToInt16(dr["CANTIDAD"].ToString());
                objKardexEnfermeriaMEdicamentos.id_producto = dr["PRO_CODIGO"].ToString();

                lista.Add(objKardexEnfermeriaMEdicamentos);
            }
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lista;
        }

        public bool IngresaKardex(IngresaKardex ingresa, int usuario)
        {
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection con = null;

            SqlCommand cmd = null;
            bool response = false;
            try
            {
                con = obj.ConectarBd();
                cmd = new SqlCommand("sp_IngresaKardex", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cue_codigo", ingresa.id_kardex);
                cmd.Parameters.AddWithValue("@presentacion", ingresa.presentacion);
                cmd.Parameters.AddWithValue("@via", ingresa.via);
                cmd.Parameters.AddWithValue("@dosis", ingresa.dosis);
                cmd.Parameters.AddWithValue("@frecuencia", ingresa.frecuencia);
                cmd.Parameters.AddWithValue("@hora", ingresa.hora);
                cmd.Parameters.AddWithValue("@codUsuario", usuario);
                cmd.Parameters.AddWithValue("@ateCodigo", ingresa.ate_codigo);
                cmd.Parameters.AddWithValue("@fechaAdmi", ingresa.fecha);
                cmd.Parameters.AddWithValue("@eventual", ingresa.eventual);
                cmd.Parameters.AddWithValue("@medPropio", ingresa.medPropio);
                con.Open();
                int filas = cmd.ExecuteNonQuery();
                if (filas > 0)
                    response = true;
            }
            catch (Exception ex)
            {
                response = false;
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return response;
        }

        public DataTable ReporteDatos(string ate_codigo)
        {
            Int64 ateCodigo = Convert.ToInt64(ate_codigo);
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

            Sqlcmd = new SqlCommand("sp_RecuperaKardexCrystal", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ateCodigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ateCodigo"].Value = (ateCodigo);

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


        public bool IngresaKardexInsumo(IngresaKardex ingresa, int usuario)
        {
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection con = null;

            SqlCommand cmd = null;
            bool response = false;
            try
            {
                con = obj.ConectarBd();
                cmd = new SqlCommand("sp_IngresaKardexInsumo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cue_codigo", ingresa.id_kardex);
                cmd.Parameters.AddWithValue("@presentacion", ingresa.presentacion);
                cmd.Parameters.AddWithValue("@hora", ingresa.hora);
                cmd.Parameters.AddWithValue("@codUsuario", usuario);
                cmd.Parameters.AddWithValue("@ateCodigo", ingresa.ate_codigo);
                cmd.Parameters.AddWithValue("@medPropio", ingresa.medPropio);
                con.Open();
                int filas = cmd.ExecuteNonQuery();
                if (filas > 0)
                    response = true;
            }
            catch (Exception ex)
            {
                response = false;
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return response;
        }

        public DataTable ReporteDatosInsumos(string ate_codigo)
        {
            Int64 ateCodigo = Convert.ToInt64(ate_codigo);
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

            Sqlcmd = new SqlCommand("sp_RecuperaKardexCrystalInsumos", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ateCodigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ateCodigo"].Value = (ateCodigo);

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

        public DataTable RecuperaKardex(string ate_codigo)
        {
            Int64 ateCodigo = Convert.ToInt64(ate_codigo);
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

            Sqlcmd = new SqlCommand("sp_RecuperaKardex", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ateCodigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ateCodigo"].Value = (ateCodigo);

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

        public bool ActualizaMedicamento(DateTime hora, bool check, string observacion, Int64 codigo)
        {
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection con = null;

            SqlCommand cmd = null;
            bool response = false;
            try
            {
                con = obj.ConectarBd();
                cmd = new SqlCommand("sp_ActualizaKardex", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@check", check);
                cmd.Parameters.AddWithValue("@hora", hora);
                cmd.Parameters.AddWithValue("@observacion", observacion);
                con.Open();
                int filas = cmd.ExecuteNonQuery();
                if (filas > 0)
                    response = true;
            }
            catch (Exception ex)
            {
                response = false;
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return response;
        }

        public void EliminarProductoKardexM(int Codigo_KardexMedicamento)
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

            Sqlcmd = new SqlCommand("delete from KARDEXMEDICAMENTOS where ID_KARDEX_MEDICAMENTOS = @codigo", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.Parameters.AddWithValue("@codigo", Codigo_KardexMedicamento);
            Sqlcmd.CommandTimeout = 180;
            Sqlcmd.ExecuteNonQuery();
            Sqlcmd.Parameters.Clear();
            Sqlcon.Close();
        }

        public DataTable RecuperaPrefacturaRubros(Int32 ate_codigo)
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

            Sqlcmd = new SqlCommand("sp_RecuperaPrefacturaRubros", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ateCodigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ateCodigo"].Value = (ate_codigo);

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

        public DataTable RecuperaPrefacturaDatos(Int32 ate_codigo)
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

            Sqlcmd = new SqlCommand("sp_RecuperaPrefacturaDatos", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ateCodigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ateCodigo"].Value = (ate_codigo);

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

        public bool GuardaReservas(RESERVA_KARDEX_MEDICAMENTO obj)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.Crear("RESERVA_KARDEX_MEDICAMENTO", obj);
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public bool GuardaMedCompuestoEncabezado(MEDICAMENTO_COMPUESTO_ENCABEZADO obj)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.Crear("MEDICAMENTO_COMPUESTO_ENCABEZADO", obj);
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public bool GuardaMedCompuesatoDetalle(List<MEDICAMENTO_COMPUESTO_DETALLE> obj)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    foreach (var item in obj)
                    {
                        contexto.Crear("MEDICAMENTO_COMPUESTO_DETALLE", item);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public bool ModificaReservas(Int64 idReserva)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    RESERVA_KARDEX_MEDICAMENTO obj = contexto.RESERVA_KARDEX_MEDICAMENTO.FirstOrDefault(r => r.id_reserva == idReserva);
                    obj.estado = false;
                    obj.usuario_ocupa = Sesion.codUsuario;
                    obj.fecha_ocupa = DateTime.Now;
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public List<RESERVA_KARDEX_MEDICAMENTO> RecuperaReservas(Int64 ateCodigo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from r in contexto.RESERVA_KARDEX_MEDICAMENTO
                            where r.ate_codigo == ateCodigo && r.estado == true
                            select r).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }
        public MEDICAMENTO_COMPUESTO_ENCABEZADO RecuperaMedCompuestoEncabezado(Int64 ateCodigo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from r in contexto.MEDICAMENTO_COMPUESTO_ENCABEZADO
                            where r.ate_codigo == ateCodigo && r.estado == true
                            orderby r.id_med_compuesto descending
                            select r).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }
        public List<MEDICAMENTO_COMPUESTO_DETALLE> RecuperaMedCompuestoDetalle(Int64 id_Encabezado)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from r in contexto.MEDICAMENTO_COMPUESTO_DETALLE
                            where r.id_med_compuesto == id_Encabezado
                            orderby r.id_med_compuesto descending
                            select r).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public bool IngresoKardexInsumo(List<KARDEX_INSUMOS> lista)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            BaseContextoDatos obj = new BaseContextoDatos();
            // SqlTransaction transaction;

            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //   transaction = Sqlcon.BeginTransaction();

            try
            {
                foreach (var item in lista)
                {
                    Sqlcmd = new SqlCommand("sp_KARDEX_INSUMOS", Sqlcon);
                    Sqlcmd.CommandType = CommandType.StoredProcedure;

                    //   Sqlcmd.Transaction = transaction;

                    Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
                    Sqlcmd.Parameters["@ATE_CODIGO"].Value = item.ATE_CODIGO;

                    Sqlcmd.Parameters.Add("@CUE_CODIGO", SqlDbType.Int);
                    Sqlcmd.Parameters["@CUE_CODIGO"].Value = item.CUE_CODIGO;

                    Sqlcmd.Parameters.Add("@PRO_CODIGO", SqlDbType.VarChar);
                    Sqlcmd.Parameters["@PRO_CODIGO"].Value = item.PRO_CODIGO;

                    Sqlcmd.Parameters.Add("@PRO_DESCRIPCION", SqlDbType.VarChar);
                    Sqlcmd.Parameters["@PRO_DESCRIPCION"].Value = item.PRO_DESCRIPCION;

                    Sqlcmd.Parameters.Add("@Administrado", SqlDbType.Bit);
                    Sqlcmd.Parameters["@Administrado"].Value = item.Administrado;

                    Sqlcmd.Parameters.Add("@NoAdministrado", SqlDbType.Bit);
                    Sqlcmd.Parameters["@NoAdministrado"].Value = item.NoAdministrado;

                    Sqlcmd.Parameters.Add("@fecha", SqlDbType.DateTime);
                    Sqlcmd.Parameters["@fecha"].Value = item.fecha;

                    Sqlcmd.Parameters.Add("@ID_USUARIO", SqlDbType.Int);
                    Sqlcmd.Parameters["@ID_USUARIO"].Value = item.ID_USUARIO;

                    Sqlcmd.Parameters.Add("@observacion", SqlDbType.VarChar);
                    Sqlcmd.Parameters["@observacion"].Value = item.observacion;

                    Sqldap = new SqlDataAdapter();
                    Sqldap.SelectCommand = Sqlcmd;
                    Dts = new DataSet();
                    Sqldap.Fill(Dts, "tabla");
                }

                // transaction.Commit();
                return true;
            }

            catch (Exception ex)
            {
                //   transaction.Rollback();
                return false;
            }
        }
    }
}
