using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Core.Datos;
using His.Entidades;
using System.Data.Common;
using System.Windows.Forms;

namespace His.Datos
{
    public class DatQuirofano
    {
        SqlConnection conexion;
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;
        BaseContextoDatos obj = new BaseContextoDatos();

        public DataTable MostrarProductos(double codsub)
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoProductos";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@codsub", codsub);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }

        public bool RecuperaAtencionesQuirofano(Int64 ate_codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from qui in contexto.REGISTRO_QUIROFANO
                        where qui.ate_codigo == ate_codigo && qui.estado == true
                        select qui.estado).FirstOrDefault();
            }
        }
        public int CambioEstadoReposicion(Int64 ate_codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                Int64 id = (from qui in contexto.REGISTRO_QUIROFANO
                            where qui.ate_codigo == ate_codigo 
                            orderby qui.id_registro_quirofano descending
                            select qui.id_registro_quirofano).FirstOrDefault();

                bool estado = (from qui in contexto.REGISTRO_QUIROFANO
                            where qui.ate_codigo == ate_codigo 
                            orderby qui.id_registro_quirofano descending
                            select qui.estado).FirstOrDefault();
                if (estado)
                {
                    return 1;
                }

                estado = (from qui in contexto.REPOSICION_PENDIENTE
                        where qui.id_registro_quirofano == id 
                        select qui.estado).FirstOrDefault();
                if (estado)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
        }

        public Int64 IdRegistroQuirofano(Int64 ate_codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                Int64 id = (from qui in contexto.REGISTRO_QUIROFANO
                            where qui.ate_codigo == ate_codigo
                            orderby qui.id_registro_quirofano descending
                            select qui.id_registro_quirofano).FirstOrDefault();

                return (from qui in contexto.REPOSICION_PENDIENTE
                        where qui.id_registro_quirofano == id && qui.estado == false
                        select qui.id_registro_quirofano).FirstOrDefault();
            }
        }
        public REGISTRO_QUIROFANO RecuperarRegistroQuirofano(Int64 ate_codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from qui in contexto.REGISTRO_QUIROFANO
                        where qui.ate_codigo == ate_codigo && qui.estado == true
                        select qui).FirstOrDefault();
            }
        }
        public INTERVENCIONES_REGISTRO_QUIROFANO RegistroQuirofano(Int64 id)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                return (from qui in contexto.INTERVENCIONES_REGISTRO_QUIROFANO
                        where qui.id_registro_quirofano == id
                        select qui).FirstOrDefault();
            }
        }
        public PROCEDIMIENTOS_CIRUGIA Procedimiento(Int64 intervencion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from qui in contexto.PROCEDIMIENTOS_CIRUGIA
                        where qui.PCI_CODIGO == intervencion
                        select qui).FirstOrDefault();
            }
        }
        public List<CIRUGIA_ESPECIALIDAD> MostrarEspCirugia()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from esp in contexto.CIRUGIA_ESPECIALIDAD
                        select esp).ToList();
            }
        }
        public INTERVENCIONES_REGISTRO_QUIROFANO RecuperaMaxIntervencion()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from esp in contexto.INTERVENCIONES_REGISTRO_QUIROFANO
                        orderby esp.id_intervenciones descending
                        select esp).FirstOrDefault();
            }
        }
        public DataTable Patologo()
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_MedicoPatologo";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }

        public DataTable MostrarGrupos()
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "select codsub as CODIGO, dessub AS DESCRIPCION from SicProductoSubdivision order by 2 asc";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarGruposAliansa()
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "select codsub as CODIGO, dessub AS DESCRIPCION from Sic3000..ProductoSubdivision order by 2 asc";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarProcedimientos(int bodega)
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoProcedimientos";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@bodega", bodega);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarCie10()
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "SELECT CIE_CODIGO AS Codigo, CIE_DESCRIPCION AS Descripcion FROM CIE10 order by 2 asc";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarProductosAgregados(string busqueda, bool codigo, bool descripcion, int bodega)
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();

            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            if (busqueda == "")
            {
                command = new SqlCommand("SELECT QP.CODPRO AS CODIGO, P.despro AS DESCRIPCION FROM QUIROFANO_PRODUCTOS QP INNER JOIN Sic3000..Producto P ON QP.CODPRO = P.codpro WHERE QP.QP_BODEGA = @bodega order by 2 asc", conexion);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@bodega", bodega);
                command.CommandTimeout = 180;
                reader = command.ExecuteReader();
                Tabla.Load(reader);
                reader.Close();
                conexion.Close();
            }
            else
            {
                if (descripcion)
                {
                    command = new SqlCommand("SELECT CODIGO, DESCRIPCION FROM VistaQuirofanoProductos WHERE QP.QP_BODEGA = @bodega AND DESCRIPCION like '%' + @filtro + '%' ORDER BY 2 ASC", conexion);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@filtro", busqueda);
                    command.Parameters.AddWithValue("@bodega", bodega);
                    command.CommandTimeout = 180;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);
                    reader.Close();
                    command.Parameters.Clear();
                    conexion.Close();
                }
                else if (codigo)
                {
                    command = new SqlCommand("SELECT CODIGO, DESCRIPCION FROM VistaQuirofanoProductos WHERE QP.QP_BODEGA = @bodega AND CODIGO = @filtro ORDER BY 2 ASC", conexion);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@filtro", busqueda);
                    command.Parameters.AddWithValue("@bodega", bodega);
                    command.CommandTimeout = 180;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);
                    reader.Close();
                    command.Parameters.Clear();
                    conexion.Close();
                }
            }
            return Tabla;
        }

        public DataTable MostrarAnestesias(string busqueda, bool codigo, bool descripcion)
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            if (busqueda == "")
            {
                command = new SqlCommand("select PCI_CODIGO as CODIGO, PCI_DESCRIPCION as DESCRIPCION from PROCEDIMIENTOS_CIRUGIA where PCI_ESTADO = 0 order by 2 asc", conexion);
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;
                reader = command.ExecuteReader();
                Tabla.Load(reader);
                reader.Close();
                conexion.Close();
            }
            else
            {
                if (descripcion)
                {
                    command = new SqlCommand("select PCI_CODIGO as CODIGO, PCI_DESCRIPCION as DESCRIPCION from PROCEDIMIENTOS_CIRUGIA where PCI_ESTADO = 0 and PCI_DESCRIPCION like '%' + @filtro + '%' order by 2 asc", conexion);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@filtro", busqueda);
                    command.CommandTimeout = 180;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);
                    reader.Close();
                    command.Parameters.Clear();
                    conexion.Close();
                }
                else if (codigo)
                {
                    command = new SqlCommand("select PCI_CODIGO as CODIGO, PCI_DESCRIPCION as DESCRIPCION from PROCEDIMIENTOS_CIRUGIA where PCI_ESTADO = 0 and PCI_CODIGO =  @filtro order by 2 asc", conexion);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@filtro", busqueda);
                    command.CommandTimeout = 180;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);
                    reader.Close();
                    command.Parameters.Clear();
                    conexion.Close();
                }
            }
            return Tabla;
        }


        public void AgregarProducto(string codpro, string grupo, Int32 bodega)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoAgregarProducto";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@grupo", grupo);
            command.Parameters.AddWithValue("@bodega", bodega);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }

        public DataTable Productos(int bodega)
        {
            conexion = obj.ConectarBd();
            DataTable Tabla = new DataTable();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command = new SqlCommand("select QP.CODPRO as CODIGO, P.despro AS DESCRIPCION, QP.QP_GRUPO AS GRUPO, QP.QP_CODIGO from QUIROFANO_PRODUCTOS QP INNER JOIN Sic3000..Producto P on qp.CODPRO = p.codpro WHERE QP.QP_BODEGA = @bodega", conexion);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@bodega", bodega);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public void EliminarProducto(int codigo)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoEliminarProducto";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@qp_codigo", codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void ActualizarProducto(int codigo, string grupo)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoActualizarProducto";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@qp_codigo", codigo);
            command.Parameters.AddWithValue("@grupo", grupo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void AgregarProcedimientos(int orden, string codpro, Int64 cie_codigo, int cantidad)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoAgregarProcedimiento";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@orden", orden);
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.Parameters.AddWithValue("@cantidad", cantidad);
            command.CommandTimeout = 180;
            try
            {
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public DataTable TodosProcedimientos(int bodega)
        {
            SqlCommand sqlCommand;
            SqlDataReader dataReader;
            SqlConnection sqlConnection;
            DataTable Tabla = new DataTable();
            sqlConnection = obj.ConectarBd();
            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            sqlCommand = new SqlCommand("sp_QuirofanoTodosProcedimientos", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@bodega", bodega);
            sqlCommand.CommandTimeout = 180;
            dataReader = sqlCommand.ExecuteReader();
            Tabla.Load(dataReader);
            dataReader.Close();
            sqlConnection.Close();
            return Tabla;
        }
        public DataTable TodosProductos(Int64 cie_codigo)
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoTodosProductos";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public void EiminarProcedimiento(string cie_codigo)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoProcedimientoEliminar";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }

        public IEnumerable<object> ListarProductos(int bodega)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from p in db.QUIROFANO_PRODUCTOS
                             join sp in db.PRODUCTOS_SIC3000 on p.CODPRO equals sp.codpro
                             where p.QP_BODEGA == bodega
                             select new
                             {
                                 codpro = sp.codpro,
                                 despro = sp.despro

                             }).ToList();
                return (IEnumerable<object>)lista;
            }
        }
        public int UltimoOrdenProcedimiento(int pci_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from qp in db.QUIROFANO_PROCE_PRODU
                             where qp.PCI_CODIGO == pci_codigo && qp.QPP_FECHA == null
                             select qp.QPP_ORDEN).ToList();

                if (lista.Count > 0)
                    return (int)lista.Max();
                else
                    return 0;
            }
        }
        public bool ActualizarProcedimiento(Int64 pci_codigo, string procedimiento)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();

                try
                {
                    var x = db.PROCEDIMIENTOS_CIRUGIA.FirstOrDefault(a => a.PCI_CODIGO == pci_codigo);
                    x.PCI_DESCRIPCION = procedimiento;
                    db.SaveChanges();
                    transa.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    transa.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }
        }
        public bool EliminarProcedimiento(Int64 pci_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();

                try
                {
                    PROCEDIMIENTOS_CIRUGIA x = (from p in db.PROCEDIMIENTOS_CIRUGIA
                                                where p.PCI_CODIGO == pci_codigo
                                                select p).FirstOrDefault();

                    var lista = (from q in db.QUIROFANO_PROCE_PRODU
                                 where q.PCI_CODIGO == pci_codigo && q.QPP_FECHA != null
                                 select q).ToList();

                    if (lista.Count > 0)
                    {
                        ConexionEntidades.ConexionEDM.Close();
                        return false;
                    }
                    else
                    {
                        db.DeleteObject(x);
                        db.SaveChanges();
                        transa.Commit();
                        ConexionEntidades.ConexionEDM.Close();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transa.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }
        }
        public Int64 CrearProcedimiento(string procedimiento, int bodega)
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
                command = new SqlCommand("INSERT INTO PROCEDIMIENTOS_CIRUGIA VALUES(@procedimiento, 1, @bodega);", connection);
                command.CommandType = CommandType.Text;
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@procedimiento", procedimiento);
                command.Parameters.AddWithValue("@bodega", bodega);
                command.ExecuteNonQuery();
                command.Parameters.Clear();

                transaction.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
                return 0;
            }
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                Int64 x = db.PROCEDIMIENTOS_CIRUGIA.OrderByDescending(a => a.PCI_CODIGO).First().PCI_CODIGO;
                return x;
            }
        }
        public dsProcedimiento lProcedimientos(int bodega)
        {
            dsProcedimiento xProcedimiento = new dsProcedimiento();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var maestro = (from p in db.QUIROFANO_PROCEDIMIENTOS
                               where p.BODEGA == bodega
                               select new
                               {
                                   p.CODIGO,
                                   p.PROCEDIMIENTO,
                                   p.CANTIDAD
                               }).OrderBy(x => x.PROCEDIMIENTO).ToList();
                foreach (var m in maestro)
                {
                    var detalle = (from d in db.QUIROFANO_PROCE_PRODU
                                   join p in db.PRODUCTOS_SIC3000 on d.CODPRO equals p.codpro
                                   where d.QPP_ORDEN != null &&
                                   d.QPP_FECHA == null && d.PCI_CODIGO == m.CODIGO
                                   select new
                                   {
                                       d.QPP_ORDEN,
                                       d.QPP_CANTIDAD,
                                       p.despro,
                                       d.PCI_CODIGO,
                                       p.codpro
                                   }).OrderBy(x => x.QPP_ORDEN).ToList();

                    //lleno la cabecera
                    object[] xproce = new object[]{
                        m.CODIGO,
                        m.PROCEDIMIENTO,
                        m.CANTIDAD,
                    };

                    xProcedimiento.Procedimiento.Rows.Add(xproce);

                    foreach (var item in detalle)
                    {
                        //lleno el detalle
                        object[] xdetalle = new object[]
                        {
                            item.codpro,
                            item.despro,
                            item.QPP_ORDEN,
                            item.QPP_CANTIDAD,
                            item.PCI_CODIGO,
                        };
                        xProcedimiento.Detalle.Rows.Add(xdetalle);
                    }
                }
            }
            return xProcedimiento;
        }
        public bool ActualizarProce_Producto(Int64 pci_codigo, string codpro, int orden, int cantidad)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    QUIROFANO_PROCE_PRODU x = (from q in db.QUIROFANO_PROCE_PRODU
                                               where q.PCI_CODIGO == pci_codigo &&
                                               q.CODPRO == codpro && q.QPP_FECHA == null
                                               select q).FirstOrDefault();
                    x.QPP_ORDEN = orden;
                    x.QPP_CANTIDAD = cantidad;

                    db.SaveChanges();
                    transa.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transa.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }
        }
        public bool EliminarProducto_Proce(Int64 pci_codigo, string codpro)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();

                try
                {
                    QUIROFANO_PROCE_PRODU x = (from q in db.QUIROFANO_PROCE_PRODU
                                               where q.PCI_CODIGO == pci_codigo
                                               && q.CODPRO == codpro && q.QPP_FECHA == null
                                               select q).FirstOrDefault();
                    db.DeleteObject(x);
                    db.SaveChanges();
                    transa.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transa.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }
        }

        public Int64 GuardaRegistroQuirofano(REGISTRO_QUIROFANO obj, bool edicion = false)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                if (!edicion)
                {
                    contexto.Crear("REGISTRO_QUIROFANO", obj);


                    return (from rq in contexto.REGISTRO_QUIROFANO
                            orderby rq.id_registro_quirofano descending
                            select rq.id_registro_quirofano).Take(1).FirstOrDefault();
                }
                else
                {
                    REGISTRO_QUIROFANO x = (from q in contexto.REGISTRO_QUIROFANO
                                            where q.ate_codigo == obj.ate_codigo
                                               && q.estado == true
                                            select q).FirstOrDefault();
                    x.cirujano = obj.cirujano;
                    x.anestecia = obj.anestecia;
                    x.hora_inicio = obj.hora_inicio;
                    x.hora_fin = obj.hora_fin;
                    x.instrumentista = obj.instrumentista;
                    x.ayudante = obj.ayudante;
                    x.recuperacion = obj.recuperacion;
                    x.circulante = obj.circulante;
                    x.patologia = obj.patologia;
                    x.quirofano = obj.quirofano;
                    x.ayudantia = obj.ayudantia;
                    x.intervencion = obj.intervencion;
                    x.tipo_Atencion = obj.tipo_Atencion;
                    x.anasteciologoAyudante = obj.anasteciologoAyudante;
                    x.especialidadCirugia = obj.especialidadCirugia;
                    x.observaciones = obj.observaciones;
                    x.contaminada = obj.contaminada;

                    contexto.SaveChanges();

                    return (from rq in contexto.REGISTRO_QUIROFANO
                            where rq.ate_codigo == obj.ate_codigo
                            select rq.id_registro_quirofano).Take(1).FirstOrDefault();
                }
            }
        }
        public bool GuardaRegistroIntervencionQuirofano(INTERVENCIONES_REGISTRO_QUIROFANO inter, bool editar = false)
        {
            if (editar)
            {
                conexion = obj.ConectarBd();
                try
                {
                    conexion.Open();
                    command.Connection = conexion;
                    command.CommandText = "sp_EliminaIntervenciones";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idRegistro", inter.id_registro_quirofano);
                    command.CommandTimeout = 180;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
                command.Connection = conexion;
                command.CommandText = "sp_GuardaIntervencionQuirofano";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_registro_quirofano", inter.id_registro_quirofano);
                command.Parameters.AddWithValue("@cie_10", inter.cie_10);
                command.Parameters.AddWithValue("@general", inter.general);
                command.CommandTimeout = 180;
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                conexion.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public void ActualizarProcedimiento(string cie_codigo, string codpro, string qpp_orden, int cantidad)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoEditarProcedimiento";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@qpp_orden", qpp_orden);
            command.Parameters.AddWithValue("@qpp_cantidad", cantidad);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void EliminarProduProce(string cie_codigo, string codpro)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoEliminarProdu_Proce";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.Parameters.AddWithValue("@codpro", codpro);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public DataTable QuirofanoPacientes(int bodega)
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            if (bodega == His.Entidades.Clases.Sesion.bodega)
                command.CommandText = "sp_QuirofanoPacientes";
            else
                command.CommandText = "sp_QuirofanoPacientesGastro";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@bodega", His.Entidades.Clases.Sesion.bodega);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }

        public void PedidoPaciente(Int64 cie, int paciente, int atencion, string fecha)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoPedidoPacienteAgregar";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cie_codigo", cie);
            command.Parameters.AddWithValue("@paciente", paciente);
            command.Parameters.AddWithValue("@atencion", atencion);
            command.Parameters.AddWithValue("@fecha", fecha);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public DataTable SoloProcedimientos(int bodega)
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoSoloProcedimientos";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@bodega", bodega);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public void AgregarProcedimientoPaciente(int orden, string cie_codigo, string codpro, double cantidad, int paciente,
            int atencion, int usada, string usuario, int cerrado)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoPacienteProcedimiento";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@orden", orden);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@cantidad", cantidad);
            command.Parameters.AddWithValue("@paciente", paciente);
            command.Parameters.AddWithValue("@atencion", atencion);
            command.Parameters.AddWithValue("@usada", usada);
            command.Parameters.AddWithValue("@usuario", usuario);
            command.Parameters.AddWithValue("@cerrado", cerrado);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void ModificarProcedimientoPaciente(int orden, string cie_codigo, string codpro,
            int paciente, int atencion, int usado, string usuario)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoCambioProcedimientoPaciente";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@orden", orden);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@paciente", paciente);
            command.Parameters.AddWithValue("@atencion", atencion);
            command.Parameters.AddWithValue("@cantidadusada", usado);
            command.Parameters.AddWithValue("@usuario", usuario);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public DataTable PacienteProcedimiento(Int64 cie_codigo, int atencion, int bodega)
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoMostrarProcedimientoPaciente";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.Parameters.AddWithValue("@atencion", atencion);
            command.Parameters.AddWithValue("@bodega", bodega);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public DataTable PacienteProcedimientoNew(int general, int bodega)
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoMostrarProcedimientoPacienteNew";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@general", general);
            command.Parameters.AddWithValue("@bodega", bodega);
            command.Parameters.AddWithValue("@usuar", His.Entidades.Clases.Sesion.codUsuario);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public DataTable PacienteProcedimientoRecuperado(Int64 id, Int64 ateCodigo)
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoMostrarProcedimientoPacienteRecuperado";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@pci_codigo", id);
            command.Parameters.AddWithValue("@bodega", His.Entidades.Clases.Sesion.bodega);
            command.Parameters.AddWithValue("@usuar", His.Entidades.Clases.Sesion.codUsuario);
            command.Parameters.AddWithValue("@ateCodigo", ateCodigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public string Cantidad(int atencion, int codigo)
        {
            string valor = null;
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoNumero";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@atencion", atencion);
            command.Parameters.AddWithValue("@codigo", codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                valor = reader["Valor"].ToString();
            }
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return valor;
        }
        public DataTable FiltrarPorProcedimiento(string cie_codigo)
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoPorProcedimiento";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public string ProductoRepetido(string codpro, Int64 cie_codigo)
        {
            string producto = null;
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoProductoRepetido";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.CommandTimeout = 180;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                producto = reader["CODPRO"].ToString();
            }
            command.Parameters.Clear();
            conexion.Close();
            return producto;
        }
        public string SoloProductoRepetido(string codpro, int bodega)
        {
            string producto = null;
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoNoRepetirProductos";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@bodega", bodega);
            SqlParameter v = new SqlParameter("@codigoproducto", 0); v.Direction = ParameterDirection.Output;
            command.Parameters.Add(v);
            command.ExecuteNonQuery();
            producto = command.Parameters["@codigoproducto"].Value.ToString();
            command.Parameters.Clear();
            conexion.Close();
            return producto;
        }
        public void ProductoBodega(string codpro, double existe, double bodega)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoBodega";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@existe", existe);
            command.Parameters.AddWithValue("@bodega", bodega);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }

        public void ProductoBodegaMushuñan(string codpro, double existe, double bodega)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_MushuñanBodega";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@existe", existe);
            command.Parameters.AddWithValue("@bodega", bodega);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public DataTable ProcedimientosPaciente(int ate_codigo, int pac_codigo, int bodega)
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoProcedimientosPaciente";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pac_codigo", pac_codigo);
            command.Parameters.AddWithValue("@bodega", bodega);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public string Proces(int ate_codigo, int pac_codigo, string cie_codigo) //funcion que ayuda a ver si existe procedimiento ya agregado al paciente
        {
            string valor = null;
            SqlCommand sqlCommand;
            SqlConnection sqlConnection;

            sqlConnection = obj.ConectarBd();
            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            sqlCommand = new SqlCommand("sp_QuirofanoExisteProcedimiento", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            sqlCommand.Parameters.AddWithValue("@pac_codigo", pac_codigo);
            sqlCommand.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            SqlParameter v = new SqlParameter("@valor", 0); v.Direction = ParameterDirection.Output;
            sqlCommand.Parameters.Add(v);
            sqlCommand.ExecuteNonQuery();
            valor = sqlCommand.Parameters["@valor"].Value.ToString();
            sqlCommand.Parameters.Clear();
            sqlConnection.Close();
            return valor;
        }
        public void PedidoAdicional(int atencion, int paciente, string cie_codigo, double cant_adicional, string codpro)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoPedidoAdicional";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@atencion", atencion);
            command.Parameters.AddWithValue("@paciente", paciente);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.Parameters.AddWithValue("@cant_adicional", cant_adicional);
            command.Parameters.AddWithValue("@codpro", codpro);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }

        public DataTable HabitacionQuirofano()
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoHabitacion";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable HabitacionGastro()
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_GastroHabitacion";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarMedicos()
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoMedicos";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarGastro()
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoGastro";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarPatologo()
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoPatologo";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarAnestesiologo()
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoAnestesiologo";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarUsuario()
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoUsuarios";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarAnestesia()
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoAnestesia";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }


        public void AgregarRegistro(int hab_quirofano, int cirujano, int ayudante, int ayudantia, int tipo_anestesia,
            int recuperacion, int anestesiologo, string horainicio, string horafin, string duracion, int circulante,
            int instrumentista, int patologo, string tipo_atencion, int ate_codigo, int pac_codigo, Int64 cie_codigo)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoRegistroAgregar";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hab_quirofano", hab_quirofano);
            command.Parameters.AddWithValue("@cirujano", cirujano);
            command.Parameters.AddWithValue("@ayudante", ayudante);
            command.Parameters.AddWithValue("@ayudantia", ayudantia);
            command.Parameters.AddWithValue("@tipo_anestesia", tipo_anestesia);
            command.Parameters.AddWithValue("@recuperacion", recuperacion);
            command.Parameters.AddWithValue("@anestesiologo", anestesiologo);
            command.Parameters.AddWithValue("@horainicio", horainicio);
            command.Parameters.AddWithValue("@horafin", horafin);
            command.Parameters.AddWithValue("@duracion", duracion);
            command.Parameters.AddWithValue("@circulante", circulante);
            command.Parameters.AddWithValue("@instrumentista", instrumentista);
            command.Parameters.AddWithValue("@patologo", patologo);
            command.Parameters.AddWithValue("@tipo_atencion", tipo_atencion);
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pac_codigo", pac_codigo);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public DataTable CargarRegistroPaciente(int ate_codigo, int pac_codigo, Int64 cie_codigo)
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoRegistro";
            command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            //command.Parameters.AddWithValue("@pac_codigo", pac_codigo);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public void CerrarProcedimiento(int ate_codigo, int pac_codigo, string cie_codigo)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoCierreProcedimiento";
            command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            //command.Parameters.AddWithValue("@pac_codigo", pac_codigo);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public string ProcedimientoCerrado(/*int ate_codigo, int pac_codigo,*/ string cie_codigo)
        {
            string estado;
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoProcedimientoCerrado";
            command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            //command.Parameters.AddWithValue("@pac_codigo", pac_codigo);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            SqlParameter v = new SqlParameter("@estado", 0); v.Direction = ParameterDirection.Output;
            command.Parameters.Add(v);
            command.ExecuteNonQuery();
            estado = command.Parameters["@estado"].Value.ToString();
            command.Parameters.Clear();
            conexion.Close();
            return estado;
        }
        public void ActualizarPedidoAdicional(int ate_codigo, string cie_codigo, string codpro, double cantadicional)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoControlPedidoAdicional";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.Parameters.AddWithValue("codpro", codpro);
            command.Parameters.AddWithValue("@cantadicional", cantadicional);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void AgregarPedidoPaciente(string ped_fecha, int id_usuario, int ate_codigo, int hab_codigo)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoAgregarPedido";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ped_fecha", ped_fecha);
            command.Parameters.AddWithValue("@id_usuario", id_usuario);
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@hab_codigo", hab_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public string RecuperarPedidoNum(int ate_codigo) //funcion que ayuda a ver si existe procedimiento ya agregado al paciente
        {
            string numpedido = null;
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoRecuperarPedido";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            SqlParameter v = new SqlParameter("@numpedido", 0); v.Direction = ParameterDirection.Output;
            command.Parameters.Add(v);
            command.ExecuteNonQuery();
            numpedido = command.Parameters["@numpedido"].Value.ToString();
            command.Parameters.Clear();
            conexion.Close();
            return numpedido;
        }
        public void PedidoDetalle(string codpro, string prodesc, int cantidad, double valor, double total, int ped_codigo, double iva)
        {
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoAgregarPedidoProducto";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@prodesc", prodesc);
            command.Parameters.AddWithValue("@cantidad", cantidad);
            command.Parameters.AddWithValue("@valor", valor);
            command.Parameters.AddWithValue("@total", total);
            command.Parameters.AddWithValue("@iva", iva);
            command.Parameters.AddWithValue("@ped_codigo", ped_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public DataTable VerTicketPaciente(int ate_codigo, int bodega)
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            if (bodega == ParametroBodegaGastro())
                command.CommandText = "sp_GastroVerTickets";
            else
                command.CommandText = "sp_QuirofanoVerTickets";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public int ParametroBodegaGastro() //Parametro para la bodega de gastro
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            int activo = 0;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_ParametroBodegaGastro", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                activo = Convert.ToInt32(reader["PAD_VALOR"].ToString());
            }
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return activo;
        }
        public DataTable ProductosPaciente(int ate_codigo, int ped_codigo)
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoProductosPedido";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@ped_codigo", ped_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public DataTable RecuperarPacientePedidoInfo(int ate_codigo)
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoPacientePedidoInfo";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarProductosUsados(DateTime desde, DateTime hasta, int usuario)
        {
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_QuirofanoDetalleProducto";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@desde", desde);
            command.Parameters.AddWithValue("@hasta", hasta);
            command.Parameters.AddWithValue("@usuario", usuario);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }

        public DataTable CargarProcedimientosCirugia(string busqueda, bool codigo, bool descripcion, int bodega)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            SqlDataReader reader;

            connection = obj.ConectarBd();
            connection.Open();
            if (busqueda == "")
            {
                command = new SqlCommand("select PCI_CODIGO AS CODIGO, PCI_DESCRIPCION AS DESCRIPCION, '' AS UVR from PROCEDIMIENTOS_CIRUGIA WHERE PCI_BODEGA = @bodega and PCI_ESTADO = 1 ORDER BY 2", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@bodega", bodega);
                command.CommandTimeout = 180;
                reader = command.ExecuteReader();
                Tabla.Load(reader);
                reader.Close();
                connection.Close();
            }
            else
            {
                if (descripcion)
                {
                    command = new SqlCommand("select PCI_CODIGO AS CODIGO, PCI_DESCRIPCION AS DESCRIPCION from PROCEDIMIENTOS_CIRUGIA  WHERE WHERE PCI_BODEGA = @bodega AND PCI_DESCRIPCION LIKE '%' + @filtro + '%' and PCI_ESTADO = 1 ORDER BY 2", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@filtro", busqueda);
                    command.Parameters.AddWithValue("@bodega", bodega);
                    command.CommandTimeout = 180;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);
                    reader.Close();
                    command.Parameters.Clear();
                    connection.Close();
                }
                else if (codigo)
                {
                    command = new SqlCommand("select PCI_CODIGO AS CODIGO, PCI_DESCRIPCION AS DESCRIPCION from PROCEDIMIENTOS_CIRUGIA  WHERE WHERE PCI_BODEGA = @bodega AND PCI_CODIGO = @filtro and PCI_ESTADO = 1 ORDER BY 2", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@filtro", busqueda);
                    command.Parameters.AddWithValue("@bodega", bodega);
                    command.CommandTimeout = 180;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);
                    reader.Close();
                    command.Parameters.Clear();
                    connection.Close();
                }
            }

            return Tabla;
        }
        public void ActualizarKardexSic(string numdoc, int bodega)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ActualizaKardexSic", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@numdoc", numdoc);
            command.Parameters.AddWithValue("@bodega", bodega);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();

        }
        public bool ActualizarKardexSicMushuñan(string numdoc)
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
                command = new SqlCommand("sp_ActualizaKardexSicMushuñan", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@numdoc", numdoc);
                command.Parameters.AddWithValue("@usuario", His.Entidades.Clases.Sesion.codUsuario);
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
        public bool ActualizarKardexSicBrigada(string numdoc)
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
                command = new SqlCommand("sp_ActualizaKardexSicBrigada", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@numdoc", numdoc);
                command.Parameters.AddWithValue("@usuario", His.Entidades.Clases.Sesion.codUsuario);
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
        public DataTable AnestesiaSolicitada(int pci_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select * from QUIROFANO_PROCE_PRODU where PCI_CODIGO = @pci_codigo and ATE_CODIGO is null", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@pci_codigo", pci_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;

        }

        public DataTable ListaPedidosQuirofano(int ate_codigo)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ListadoPedidosQuirofano", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

        public void QuirofanoActualizarProductos(string codpro, int pci_codigo, double cantidad, Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_QuirofanoActulizarCantidades", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@pci_codigo", pci_codigo);
            command.Parameters.AddWithValue("@cantidad", cantidad);
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
        public bool validaDecimales(string codpro)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool valido = false;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select * from SIC3000..Producto where codpro = @codpro", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader["CantDecimal"].ToString() == "True")
                    valido = true;
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return valido;
        }

        public void QuirofanoEliminaRegistro(string codpro, int pci_codigo, Int64 ate_codigo)//elimina producto del procedimiento
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_QuirofanoEliminaRegistro", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@pci_codigo", pci_codigo);
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
        public void QuirofanoEliminaRegistro2(int pci_codigo, Int64 ate_codigo)//elimina producto del procedimiento
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_QuirofanoEliminaRegistro2", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@pci_codigo", pci_codigo);
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }

        public void DatosReposicion(Int64 ate_codigo, int pci_codigo, int cantidad, DateTime fecha, int ped_codigo,
            string codpro, int usuario)//guarda el registro para tener datos como fecha en la reposicion
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_DatosReposicion", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pci_codigo", pci_codigo);
            command.Parameters.AddWithValue("@cantidad", cantidad);
            command.Parameters.AddWithValue("@fechacreacion", fecha);
            command.Parameters.AddWithValue("@ped_codigo", ped_codigo);
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@usuario", usuario);

            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
        public DataTable RecuperoReposicion(DateTime desde, DateTime hasta, Int64 bodega)//elimina producto del procedimiento
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_Reposicion", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@desde", desde);
            command.Parameters.AddWithValue("@hasta", hasta);
            command.Parameters.AddWithValue("@bodega", bodega);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

        public void FechaReposicion(DateTime fecha, int pci_codigo, Int64 ate_codigo, Int64 numdoc)//elimina producto del procedimiento
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_FechaReposicion", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechareposicion", fecha);
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pci_codigo", pci_codigo);
            command.Parameters.AddWithValue("@numdoc", numdoc);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }

        public int NumeroControl()
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();
            int numdoc = 0;
            command = new SqlCommand("select numcon from Sic3000..Numero_Control where codcon = 44 and ocupado = 0", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                numdoc = Convert.ToInt32(reader["numcon"].ToString());
            }
            reader.Close();
            connection.Close();
            return numdoc;
        }


        public void EliminarRegistro(Int64 ate_codigo, int pci_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("DELETE FROM QUIROFANO_PROCE_PRODU WHERE ATE_CODIGO = @ate_codigo AND PCI_CODIGO = @pci_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pci_codigo", pci_codigo);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }

        public void NumeroOcupado()
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("Update numero_control set ocupado=1 where codcon =44", connection);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            connection.Close();

        }

        public void NumeroLiberar()
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_QuirofanoLiberaControl", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            connection.Close();

        }
        public bool CreaPedidoReposicion(Int64 numdoc, DateTime fecha, string hora, double origen, Double destino,
            string observacion, char estado, double usuario, List<RepoQuirofano> lista)
        {
            SqlTransaction transaction;
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();
            transaction = connection.BeginTransaction();
            try
            {
                command = new SqlCommand("sp_ReposicionSic3000", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction;

                command.Parameters.AddWithValue("@numdoc", numdoc);
                command.Parameters.AddWithValue("@fecha", fecha);
                command.Parameters.AddWithValue("@hora", hora);
                command.Parameters.AddWithValue("@bodegaOrigen", origen);
                command.Parameters.AddWithValue("@bodegaDestino", destino);
                command.Parameters.AddWithValue("@observacion", observacion);
                command.Parameters.AddWithValue("@estado", estado);
                command.Parameters.AddWithValue("@usuario", usuario);

                command.ExecuteNonQuery();
                command.Parameters.Clear();

                foreach (var item in lista)
                {
                    command = new SqlCommand("sp_DetalleReposicion", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transaction;
                    command.Parameters.AddWithValue("@codpro", item.codpro);
                    command.Parameters.AddWithValue("@despro", item.despro);
                    command.Parameters.AddWithValue("@cant", item.cantidad);
                    command.Parameters.AddWithValue("@linea", item.linea);
                    command.Parameters.AddWithValue("@numdoc", numdoc);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }

                transaction.Commit();
                connection.Close();

                return true;

            }
            catch (Exception)
            {

                transaction.Rollback();
                return false;
            }



        }

        //public void DetalleReposicion(string codpro, string despro, decimal cant, int linea, Int64 numdoc)
        //{
        //    SqlCommand command;
        //    SqlConnection connection;
        //    BaseContextoDatos obj = new BaseContextoDatos();
        //    connection = obj.ConectarBd();
        //    connection.Open();
        //    command = new SqlCommand("sp_DetalleReposicion", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.AddWithValue("@codpro", codpro);
        //    command.Parameters.AddWithValue("@despro", despro);
        //    command.Parameters.AddWithValue("@cant", cant);
        //    command.Parameters.AddWithValue("@linea", linea);
        //    command.Parameters.AddWithValue("@numdoc", numdoc);
        //    command.ExecuteNonQuery();
        //    command.Parameters.Clear();
        //    connection.Close();
        //}

        public DataTable DetalleExportar(DateTime desde, DateTime hasta)//elimina producto del procedimiento
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_QuirofanoDetalleProductoExportar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@desde", desde);
            command.Parameters.AddWithValue("@hasta", hasta);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

        public DataTable UsuariosReposicion()
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();
            DataTable Tabla = new DataTable();
            command = new SqlCommand("SELECT U.ID_USUARIO AS CODIGO, U.APELLIDOS + ' ' + U.NOMBRES AS USUARIO FROM USUARIOS U INNER JOIN REPOSICION_QUIROFANO RQ ON U.ID_USUARIO = RQ.ID_USUARIO WHERE RQ_FECHAREPOSICION IS NULL GROUP BY U.ID_USUARIO, U.APELLIDOS + ' ' + U.NOMBRES", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }
        public bool NombreProcedimiento(string procedimiento, int bodega)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();
            bool valido = false;

            command = new SqlCommand("sp_QuirofanoNombreProcedimiento", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@pci_descripcion", procedimiento);
            command.Parameters.AddWithValue("@bodega", bodega);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader["ESTADO"].ToString() == "1")
                    valido = true;
                else
                    valido = false;
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return valido;

        }
        public DataTable ProductosSic(string filtro, int bodega)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_QuirofanoProductoSic", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@filtro", filtro);
            command.Parameters.AddWithValue("@bodega", bodega);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public DataTable ProcedimientosVarios(Int64 ate_codigo, int pac_codigo, string procedimiento)//VERIFICO CUANTOS PROCEDIMIENTOS TIENE
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_QuirofanoExisteVariosProce", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pac_codigo", pac_codigo);
            command.Parameters.AddWithValue("@procedimiento", procedimiento);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public DataTable Creado(string procedimiento, int bodega)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_QuiroCrearVariosProcedimientos", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@procedimiento", procedimiento);
            command.Parameters.AddWithValue("@bodega", bodega);
            try
            {
                reader = command.ExecuteReader();
                Tabla.Load(reader);
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {

                throw;
            }
            return Tabla;
        }

        public int UltimoOrden(int pci_codigo, Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            int ultimoOrden = 0;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("select top 1 QPP_ORDEN from QUIROFANO_PROCE_PRODU where ATE_CODIGO = @ate_codigo and PCI_CODIGO = @pci_codigo order by QPP_ORDEN desc", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pci_codigo", pci_codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string num = reader["QPP_ORDEN"].ToString();
                if (num == "")
                    ultimoOrden = 0;
                else
                    ultimoOrden = Convert.ToInt32(reader["QPP_ORDEN"].ToString());

            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return ultimoOrden;
        }
        public QUIROFANO_PROCE_PRODU recuperarHabitacionQuirofano(Int64 ate_codigo, Int64 pci_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                QUIROFANO_PROCE_PRODU reg = db.QUIROFANO_PROCE_PRODU.FirstOrDefault(a => a.PCI_CODIGO == pci_codigo && a.ATENCIONES.ATE_CODIGO == ate_codigo && a.QPP_ORDEN == null);
                return reg;
            }
        }
        public List<REPOSICION_QUIROFANO> ReposicionQuirofano(Int64 pci_codigo, Int64 ateCodigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from r in contexto.REPOSICION_QUIROFANO
                        where r.PCI_CODIGO == pci_codigo && r.ATE_CODIGO == ateCodigo
                        select r).ToList();
            }
        }

        public bool GuarddaReposicion(List<REPOSICION_QUIROFANO> lista)
        {
            bool resultado = false;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                SqlTransaction transaction;
                SqlCommand command;
                SqlConnection connection;
                BaseContextoDatos obj = new BaseContextoDatos();

                connection = obj.ConectarBd();
                connection.Open();
                try
                {
                    foreach (var item in lista)
                    {
                        REPOSICION_QUIROFANO repo = contexto.REPOSICION_QUIROFANO.OrderByDescending(c => c.RQ_CODIGO).FirstOrDefault();
                        Int64 repos = 0;
                        if (repo == null)
                        {
                            repos++;
                        }
                        else
                        {
                            repos = repo.RQ_CODIGO + 1;
                        }

                        command = new SqlCommand("sp_GuardaReposicionQuirofano", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RQ_CODIGO", repos);
                        command.Parameters.AddWithValue("@RQ_CANTIDAD", item.RQ_CANTIDAD);
                        command.Parameters.AddWithValue("@CODPRO", item.CODPRO);
                        command.Parameters.AddWithValue("@ATE_CODIGO", item.ATE_CODIGO);
                        command.Parameters.AddWithValue("@PCI_CODIGO", item.PCI_CODIGO);
                        command.Parameters.AddWithValue("@RQ_CANTIDADADICIONAL", item.RQ_CANTIDADADICIONAL);
                        command.Parameters.AddWithValue("@RQ_CANTIDADDEVOLUCION", item.RQ_CANTIDADDEVOLUCION);
                        command.Parameters.AddWithValue("@RQ_FECHACREACION", item.RQ_FECHACREACION);
                        command.Parameters.AddWithValue("@RQ_FECHAPEDIDO", item.RQ_FECHAPEDIDO);
                        command.Parameters.AddWithValue("@ID_USUARIO", item.ID_USUARIO);

                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }

                    connection.Close();
                    return true;

                }
                catch (Exception ex)
                {
                    connection.Close();
                    return false;
                }
                //try
                //{

                //    foreach (var item in lista)
                //    {
                //        REPOSICION_QUIROFANO repo = contexto.REPOSICION_QUIROFANO.OrderByDescending(c => c.RQ_CODIGO).FirstOrDefault();
                //        Int64 repos = 0;
                //        if (repo == null)
                //        {
                //            repos++;
                //        }
                //        else
                //        {
                //            repos = repo.RQ_CODIGO + 1;
                //        }

                //        command = new SqlCommand("sp_DetalleReposicion", connection);
                //        command.CommandType = CommandType.StoredProcedure;
                //        command.Parameters.AddWithValue("@codpro", item.codpro);
                //        command.Parameters.AddWithValue("@despro", item.despro);
                //        command.Parameters.AddWithValue("@cant", item.cantidad);
                //        command.Parameters.AddWithValue("@linea", item.linea);
                //        command.Parameters.AddWithValue("@numdoc", numdoc);
                //        command.ExecuteNonQuery();
                //        command.Parameters.Clear();
                //        var quirofano1 = new REPOSICION_QUIROFANO
                //        {
                //            RQ_CODIGO = repos,
                //            RQ_CANTIDAD = item.RQ_CANTIDAD,
                //            CODPRO = item.CODPRO,
                //            ATE_CODIGO = item.ATE_CODIGO,
                //            PCI_CODIGO = item.PCI_CODIGO,
                //            RQ_CANTIDADADICIONAL = item.RQ_CANTIDADADICIONAL,
                //            RQ_CANTIDADDEVOLUCION = item.RQ_CANTIDADDEVOLUCION,
                //            RQ_FECHACREACION = item.RQ_FECHACREACION,
                //            RQ_FECHAPEDIDO = item.RQ_FECHAPEDIDO,
                //            PED_CODIGO = item.PED_CODIGO,
                //            ID_USUARIO = item.ID_USUARIO,

                //        };
                //        contexto.AddToREPOSICION_QUIROFANO(quirofano1);
                //        contexto.SaveChanges();
                //    }
                //    transa.Commit();
                //    resultado = true;
                //}
                //catch (Exception ex)
                //{
                //    Console.Write(ex.Message);
                //    transa.Rollback();
                //}


            }
        }

        public DataTable TodoslosProcedimiento(DateTime desde, DateTime hasta)//VERIFICO CUANTOS PROCEDIMIENTOS TIENE
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_TodosProcedimientos", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@desde", desde);
            command.Parameters.AddWithValue("@hasta", hasta);
            command.CommandTimeout = 500;
            try
            {
                reader = command.ExecuteReader();
                Tabla.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
            return Tabla;
        }
        public DataTable ExploradorProcedimientos(DateTime desde, DateTime hasta,Int64 bodega,bool filtro)//VERIFICO CUANTOS PROCEDIMIENTOS TIENE
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_ExploradorProcedimientos", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@desde", desde);
            command.Parameters.AddWithValue("@hasta", hasta);
            command.Parameters.AddWithValue("@bodega", bodega);
            command.Parameters.AddWithValue("@filtro", filtro);
            command.CommandTimeout = 500;
            try
            {
                reader = command.ExecuteReader();
                Tabla.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
            return Tabla;
        }
        public DataTable procedimietosXmes(DateTime desde, DateTime hasta)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();
            DataTable Tabla = new DataTable();
            command = new SqlCommand("SELECT COUNT(PCI_CODIGO)AS CODIGO,PCI_DESCRIPCION FROM  GRAFICA_PROCESOS \n" +
            "where RQ_FECHACREACION between '" + desde + "' and '" + hasta + "' \n" +
            "GROUP BY PCI_DESCRIPCION", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }
        public DataTable RepocisionPedido(Int64 ID_PEDIDO)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();
            DataTable Tabla = new DataTable();
            command = new SqlCommand("select * from Sic3000..DetPedido \n" +
                "where ID_PEDIDO = (SELECT MAX(ID_PEDIDO) AS IDPEDIDO FROM Sic3000..CabPedido where NUMERO_PEDIDO = " + ID_PEDIDO + ")", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }
        public DataTable ProductosProcedimeinto(Int64 ID_PEDIDO)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();
            DataTable Tabla = new DataTable();
            command = new SqlCommand("select * from QUIROFANO_PROCE_PRODU where PCI_CODIGO =" + ID_PEDIDO + " and QPP_FECHA is NULL", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public void CargaPedido(int orden, string codpro, Int64 cie_codigo, int cantidad)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("IF NOT EXISTS(select * from QUIROFANO_PROCE_PRODU where CODPRO = @codpro and PCI_CODIGO = @cie_codigo) \n"
            + "BEGIN \n"
            + "INSERT INTO QUIROFANO_PROCE_PRODU VALUES(@orden, @cie_codigo, @codpro, @cantidad, NULL, NULL, NULL, NULL, NULL, NULL, \n"
            + " NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) \n"
            + "END  ", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@orden", orden);
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.Parameters.AddWithValue("@cantidad", cantidad);
            try
            {
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public bool CambioEstadoReposicion(Int64 ped_codigo, Int64 numdoc)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    REPOSICION_PENDIENTE x = (from r in db.REPOSICION_PENDIENTE
                                              where r.ped_codigo == ped_codigo
                                              select r).FirstOrDefault();
                    x.estado = true;
                    x.numdoc = numdoc;
                    db.SaveChanges();
                    transac.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transac.Rollback();
                    return false;
                }
            }
        }
        public dsProcedimiento reposicionGastro(DateTime desde, DateTime hasta, Int32 bodega)
        {
            dsProcedimiento ds = new dsProcedimiento();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                try
                {
                    #region Reposiciones Pendinetes
                    SqlCommand command;
                    SqlConnection connection;
                    BaseContextoDatos obj = new BaseContextoDatos();
                    SqlDataReader reader;
                    connection = obj.ConectarBd();
                    connection.Open();
                    DataTable Tabla = new DataTable();
                    DataTable Tabla1 = new DataTable();
                    command = new SqlCommand(" select rp.ped_codigo, CONCAT(p.PAC_APELLIDO_PATERNO, ' ', p.PAC_APELLIDO_MATERNO, ' ', p.PAC_NOMBRE1, ' ', p.PAC_NOMBRE2) as 'PACIENTE',rq.intervencion as 'PROCEDIMIENTO' \n" +
                        ",(select APELLIDOS +' '+NOMBRES from USUARIOS where ID_USUARIO=(select ID_USUARIO from CUENTAS_PACIENTES where Codigo_Pedido = rp.ped_codigo group by ID_USUARIO)) as 'USUARIO',rp.estado,pd.PED_DESCRIPCION  \n"
                    + "from REPOSICION_PENDIENTE rp inner join REGISTRO_QUIROFANO rq on rp.id_registro_quirofano = rq.id_registro_quirofano\n"
                    + "inner join ATENCIONES a on rq.ate_codigo = a.ATE_CODIGO\n"+
                    "inner join PEDIDOS pd on rp.ped_codigo = pd.PED_CODIGO \n"
                    + "inner join PACIENTES p on a.PAC_CODIGO = p.PAC_CODIGO \n" +
                    "where cast(convert(varchar(11),rq.fecha_registro ,13) as datetime)>= '" + desde + "' And cast(convert(varchar(11),rq.fecha_registro ,13) as datetime)<= '" + hasta + "' and rp.estado = 0 \n" +
                    "and (select general from INTERVENCIONES_REGISTRO_QUIROFANO where id_registro_quirofano = rq.id_registro_quirofano group by general) = " + bodega, connection);
                    command.CommandType = CommandType.Text;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);

                    foreach (DataRow row in Tabla.Rows)
                    {
                        Int64 PED_CODIGO = Convert.ToInt32(row["ped_codigo"].ToString());
                        var cuerpo = (from c in db.CUENTAS_PACIENTES
                                      where c.Codigo_Pedido == PED_CODIGO
                                      select c).ToList();
                        object[] cab = new object[]
                        {
                        row["ped_codigo"].ToString(),
                        row["PACIENTE"].ToString(),
                        row["PROCEDIMIENTO"].ToString(),
                        row["USUARIO"].ToString(),
                        true,
                        row["PED_DESCRIPCION"].ToString()
                        };
                        ds.Pedido.Rows.Add(cab);
                        foreach (var item in cuerpo)
                        {
                            Tabla1 = new DataTable();
                            command = new SqlCommand("select * from Sic3000..Producto where codpro = " + item.PRO_CODIGO + " and clasprod = 'B' ", connection);
                            command.CommandType = CommandType.Text;
                            reader = command.ExecuteReader();
                            Tabla1.Load(reader);
                            if (Tabla1.Rows.Count != 0)
                            {
                                object[] det = new object[]
                                {
                                item.PRO_CODIGO,
                                item.CUE_DETALLE,
                                item.CUE_CANTIDAD,
                                item.Codigo_Pedido
                                };
                                ds.DetPedido.Rows.Add(det);
                            }
                        }
                        //foreach (var item in cuerpo)//por cambio a cargar solo bienes y no servicios// 17/04/2023 // Mario
                        //{
                        //    object[] det = new object[]
                        //    {
                        //    item.PRO_CODIGO,
                        //    item.CUE_DETALLE,
                        //    item.CUE_CANTIDAD,
                        //    item.Codigo_Pedido
                        //    };
                        //    ds.DetPedido.Rows.Add(det);
                        //}
                    }
                    #endregion
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return ds;
        }
        public dsProcedimiento reposicionQuirofano(DateTime desde, DateTime hasta, Int32 bodega)
        {
            dsProcedimiento ds = new dsProcedimiento();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                try
                {
                    #region Reposiciones Pendinetes
                    SqlCommand command;
                    SqlConnection connection;
                    BaseContextoDatos obj = new BaseContextoDatos();
                    SqlDataReader reader;
                    connection = obj.ConectarBd();
                    connection.Open();
                    DataTable Tabla = new DataTable();
                    DataTable Tabla1 = new DataTable();
                    command = new SqlCommand(" select rp.ped_codigo, CONCAT(p.PAC_APELLIDO_PATERNO, ' ', p.PAC_APELLIDO_MATERNO, ' ', p.PAC_NOMBRE1, ' ', p.PAC_NOMBRE2) as 'PACIENTE',rq.intervencion as 'PROCEDIMIENTO' \n" +
                        ",(select APELLIDOS +' '+NOMBRES from USUARIOS where ID_USUARIO=(select ID_USUARIO from CUENTAS_PACIENTES where Codigo_Pedido = rp.ped_codigo group by ID_USUARIO)) as 'USUARIO',rp.estado, pd.PED_DESCRIPCION  \n"
                    + "from REPOSICION_PENDIENTE rp inner join REGISTRO_QUIROFANO rq on rp.id_registro_quirofano = rq.id_registro_quirofano\n"
                    + "inner join ATENCIONES a on rq.ate_codigo = a.ATE_CODIGO\n" +
                    "inner join PEDIDOS pd on rp.ped_codigo = pd.PED_CODIGO \n"
                    + "inner join PACIENTES p on a.PAC_CODIGO = p.PAC_CODIGO \n" +
                    "where cast(convert(varchar(11),rq.fecha_registro ,13) as datetime)>= '" + desde + "' And cast(convert(varchar(11),rq.fecha_registro ,13) as datetime)<= '" + hasta + "' and rp.estado = 0  \n" +
                    "and (select general from INTERVENCIONES_REGISTRO_QUIROFANO where id_registro_quirofano = rq.id_registro_quirofano group by general) = " + bodega, connection);
                    command.CommandType = CommandType.Text;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);
                    foreach (DataRow row in Tabla.Rows)
                    {
                        Int64 PED_CODIGO = Convert.ToInt32(row["ped_codigo"].ToString());
                        var cuerpo = (from c in db.CUENTAS_PACIENTES
                                      where c.Codigo_Pedido == PED_CODIGO
                                      select c).ToList();
                        object[] cab = new object[]
                        {
                        row["ped_codigo"].ToString(),
                        row["PACIENTE"].ToString(),
                        row["PROCEDIMIENTO"].ToString(),
                        row["USUARIO"].ToString(),
                        true,
                        row["PED_DESCRIPCION"].ToString()
                        };
                        ds.Pedido.Rows.Add(cab);
                        foreach (var item in cuerpo)
                        {
                            Tabla1 = new DataTable();
                            command = new SqlCommand("select codpro,despro,clasprod  from Sic3000..Producto where codpro = " + item.PRO_CODIGO + " and clasprod = 'B' ", connection);
                            command.CommandType = CommandType.Text;
                            reader = command.ExecuteReader();
                            Tabla1.Load(reader);
                            if (Tabla1.Rows.Count != 0)
                            {
                                object[] det = new object[]
                                {
                                item.PRO_CODIGO,
                                item.CUE_DETALLE,
                                item.CUE_CANTIDAD,
                                item.Codigo_Pedido
                                };
                                ds.DetPedido.Rows.Add(det);
                            }
                        }
                        //foreach (var item in cuerpo)
                        //{
                        //    object[] det = new object[]
                        //    {
                        //    item.PRO_CODIGO,
                        //    item.CUE_DETALLE,
                        //    item.CUE_CANTIDAD,
                        //    item.Codigo_Pedido
                        //    };
                        //    ds.DetPedido.Rows.Add(det);
                        //}
                    }
                    #endregion
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return ds;
        }
        public dsProcedimiento reposicionGastroServicios(DateTime desde, DateTime hasta, Int32 bodega)
        {
            dsProcedimiento ds = new dsProcedimiento();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                try
                {
                    #region Reposiciones Pendinetes
                    SqlCommand command;
                    SqlConnection connection;
                    BaseContextoDatos obj = new BaseContextoDatos();
                    SqlDataReader reader;
                    connection = obj.ConectarBd();
                    connection.Open();
                    DataTable Tabla = new DataTable();
                    DataTable Tabla1 = new DataTable();
                    command = new SqlCommand(" select rp.ped_codigo, CONCAT(p.PAC_APELLIDO_PATERNO, ' ', p.PAC_APELLIDO_MATERNO, ' ', p.PAC_NOMBRE1, ' ', p.PAC_NOMBRE2) as 'PACIENTE',rq.intervencion as 'PROCEDIMIENTO' \n" +
                        ",(select APELLIDOS +' '+NOMBRES from USUARIOS where ID_USUARIO=(select ID_USUARIO from CUENTAS_PACIENTES where Codigo_Pedido = rp.ped_codigo group by ID_USUARIO)) as 'USUARIO',rp.estado, pd.PED_DESCRIPCION  \n"
                    + "from REPOSICION_PENDIENTE rp inner join REGISTRO_QUIROFANO rq on rp.id_registro_quirofano = rq.id_registro_quirofano\n"
                    + "inner join ATENCIONES a on rq.ate_codigo = a.ATE_CODIGO\n"
                    + "inner join PEDIDOS pd on rp.ped_codigo = pd.PED_CODIGO \n"
                    + "inner join PACIENTES p on a.PAC_CODIGO = p.PAC_CODIGO \n" +
                    "where cast(convert(varchar(11),rq.fecha_registro ,13) as datetime)>= '" + desde + "' And cast(convert(varchar(11),rq.fecha_registro ,13) as datetime)<= '" + hasta + "' and rp.estado = 0 " +
                    "and (select general from INTERVENCIONES_REGISTRO_QUIROFANO where id_registro_quirofano = rq.id_registro_quirofano group by general) = " + bodega, connection);
                    command.CommandType = CommandType.Text;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);

                    foreach (DataRow row in Tabla.Rows)
                    {
                        Int64 PED_CODIGO = Convert.ToInt32(row["ped_codigo"].ToString());
                        var cuerpo = (from c in db.CUENTAS_PACIENTES
                                      where c.Codigo_Pedido == PED_CODIGO
                                      select c).ToList();
                        object[] cab = new object[]
                        {
                        row["ped_codigo"].ToString(),
                        row["PACIENTE"].ToString(),
                        row["PROCEDIMIENTO"].ToString(),
                        row["USUARIO"].ToString(),
                        true,
                        row["PED_DESCRIPCION"].ToString()
                        };
                        ds.Pedido.Rows.Add(cab);
                        foreach (var item in cuerpo)
                        {
                            object[] det = new object[]
                            {
                            item.PRO_CODIGO,
                            item.CUE_DETALLE,
                            item.CUE_CANTIDAD,
                            item.Codigo_Pedido
                            };
                            ds.DetPedido.Rows.Add(det);
                        }
                    }
                    #endregion
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return ds;
        }
        public dsProcedimiento reposicionQuirofanoServicios(DateTime desde, DateTime hasta, Int32 bodega)
        {
            dsProcedimiento ds = new dsProcedimiento();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                try
                {
                    #region Reposiciones Pendinetes
                    SqlCommand command;
                    SqlConnection connection;
                    BaseContextoDatos obj = new BaseContextoDatos();
                    SqlDataReader reader;
                    connection = obj.ConectarBd();
                    connection.Open();
                    DataTable Tabla = new DataTable();
                    DataTable Tabla1 = new DataTable();
                    command = new SqlCommand(" select rp.ped_codigo, CONCAT(p.PAC_APELLIDO_PATERNO, ' ', p.PAC_APELLIDO_MATERNO, ' ', p.PAC_NOMBRE1, ' ', p.PAC_NOMBRE2) as 'PACIENTE',rq.intervencion as 'PROCEDIMIENTO' \n" +
                        ",(select APELLIDOS +' '+NOMBRES from USUARIOS where ID_USUARIO=(select ID_USUARIO from CUENTAS_PACIENTES where Codigo_Pedido = rp.ped_codigo group by ID_USUARIO)) as 'USUARIO',rp.estado, pd.PED_DESCRIPCION  \n"
                    + "from REPOSICION_PENDIENTE rp inner join REGISTRO_QUIROFANO rq on rp.id_registro_quirofano = rq.id_registro_quirofano\n"
                    + "inner join ATENCIONES a on rq.ate_codigo = a.ATE_CODIGO\n"
                    + "inner join PEDIDOS pd on rp.ped_codigo = pd.PED_CODIGO \n"
                    + "inner join PACIENTES p on a.PAC_CODIGO = p.PAC_CODIGO \n" +
                    "where cast(convert(varchar(11),rq.fecha_registro ,13) as datetime)>= '" + desde + "' And cast(convert(varchar(11),rq.fecha_registro ,13) as datetime)<= '" + hasta + "' and rp.estado = 0 " +
                    "and (select general from INTERVENCIONES_REGISTRO_QUIROFANO where id_registro_quirofano = rq.id_registro_quirofano group by general) = " + bodega, connection);
                    command.CommandType = CommandType.Text;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);
                    foreach (DataRow row in Tabla.Rows)
                    {
                        Int64 PED_CODIGO = Convert.ToInt32(row["ped_codigo"].ToString());
                        var cuerpo = (from c in db.CUENTAS_PACIENTES
                                      where c.Codigo_Pedido == PED_CODIGO
                                      select c).ToList();
                        object[] cab = new object[]
                        {
                        row["ped_codigo"].ToString(),
                        row["PACIENTE"].ToString(),
                        row["PROCEDIMIENTO"].ToString(),
                        row["USUARIO"].ToString(),
                        true,
                        row["PED_DESCRIPCION"].ToString()
                        };
                        ds.Pedido.Rows.Add(cab);
                        foreach (var item in cuerpo)
                        {
                            object[] det = new object[]
                            {
                            item.PRO_CODIGO,
                            item.CUE_DETALLE,
                            item.CUE_CANTIDAD,
                            item.Codigo_Pedido
                            };
                            ds.DetPedido.Rows.Add(det);
                        }
                    }
                    #endregion
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return ds;
        }
        public DataTable RecuperoReposicionServicio(DateTime desde, DateTime hasta, Int64 bodega)//elimina producto del procedimiento
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_Reposicion", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@desde", desde);
            command.Parameters.AddWithValue("@hasta", hasta);
            command.Parameters.AddWithValue("@bodega", bodega);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

        public INTERVENCIONES_REGISTRO_QUIROFANO RecuperaRegistroIntervencionQuirofano( Int64 id_quirofano)
        {
            using( var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from i in db.INTERVENCIONES_REGISTRO_QUIROFANO
                        where i.id_registro_quirofano == id_quirofano
                        select i).FirstOrDefault();
            }
        }
        public List<DtoHabitaciones> listadoHAbitaciones()
        {
            List<DtoHabitaciones> hab = new List<DtoHabitaciones>();
            List<int> listadoH = new List<int>() { 61, 62, 63, 64 };
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var x = (from h in db.HABITACIONES
                         join n in db.NIVEL_PISO on h.NIVEL_PISO.NIV_CODIGO equals n.NIV_CODIGO
                         where n.NIV_CODIGO == 11
                         select h).Where(b => listadoH.Contains(b.hab_Codigo));
                foreach (var item in x)
                {
                    DtoHabitaciones hb = new DtoHabitaciones();
                    hb.CODIGO = item.hab_Codigo;
                    hb.HABITACION = item.hab_Numero;
                    hab.Add(hb);
                }
                return hab;
            }
        }
        public List<DtoHabitaciones> listadoHAbitacionesConsentimiento()
        {
            List<DtoHabitaciones> hab = new List<DtoHabitaciones>();
            List<int> listadoH = new List<int>() { 36, 38 };
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var x = (from h in db.HABITACIONES
                         join n in db.NIVEL_PISO on h.NIVEL_PISO.NIV_CODIGO equals n.NIV_CODIGO
                         select h).Where(b => !listadoH.Contains(b.NIVEL_PISO.NIV_CODIGO));
                foreach (var item in x)
                {
                    DtoHabitaciones hb = new DtoHabitaciones();
                    hb.CODIGO = item.hab_Codigo;
                    hb.HABITACION = item.hab_Numero;
                    hab.Add(hb);
                }
                return hab;
            }
        }
        public REPOSICION_QUIROFANO exieteRegistro(Int64 ATE_CODIGO, Int64 PCI_CODIGO,string CODPRO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from r in db.REPOSICION_QUIROFANO
                        where r.ATE_CODIGO == ATE_CODIGO && r.PCI_CODIGO == PCI_CODIGO && r.CODPRO == CODPRO
                        select r).FirstOrDefault();
            }
        }
        public string registroQuirofano(Int64 ATE_CODIGO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var concat = (from r in db.REGISTRO_QUIROFANO
                              where r.ate_codigo == ATE_CODIGO
                              select r.intervencion).ToList();
                //var concat = db.REGISTRO_QUIROFANO.Select(r => r.intervencion).ToList();
                // Realiza la concatenación utilizando LINQ y StringBuilder
                StringBuilder concatenatedNames = new StringBuilder();
                foreach (var item in concat)
                {
                    concatenatedNames.Append(item).Append(", ");
                }
                // Elimina la última coma y el espacio
                if (concatenatedNames.Length > 2)
                {
                    concatenatedNames.Length -= 2;
                }
                return concatenatedNames.ToString();
            }
        }
        public bool recuperaEstadoUltimoProcedimiento(Int64 ATE_CODIGO)
        {
            using(var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                try
                {
                var reg = (from r in db.REGISTRO_QUIROFANO where r.ate_codigo == ATE_CODIGO select r).OrderByDescending(x => x.id_registro_quirofano).FirstOrDefault();
                return reg.estado;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return true;
                }
            }
        }
    }
}
