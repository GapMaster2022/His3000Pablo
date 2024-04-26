using His.Entidades;
using His.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace His.Maintenance
{
    public partial class Frm_CreaUsuarios : Form
    {
        public MEDICOS med = new MEDICOS();
        public List<MEDICOS> listMed = new List<MEDICOS>();

        public Frm_CreaUsuarios()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime oPrimerDiaDelMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddYears(1);
            Int32 resp;
            NegUsuarios usuariosBLL = new NegUsuarios();
            med = NegMedicos.Medicos();
            listMed = NegMedicos.listaMedicos();
            foreach (var item in listMed)
            {
                Usuarios usuarios = new Usuarios();
                usuarios.Nombre = item.MED_NOMBRE1 + " " + item.MED_NOMBRE2;
                usuarios.Apellidos = item.MED_APELLIDO_PATERNO + " " + item.MED_APELLIDO_MATERNO;
                usuarios.Identificacion = item.MED_RUC.Substring(1, 10);
                usuarios.Nombreusu = item.MED_NOMBRE1.Substring(0, 1) + item.MED_APELLIDO_PATERNO;
                usuarios.Codusu = 1;
                usuarios.Clave = item.MED_RUC.Substring(0, 10);
                usuarios.Codigo_c = Convert.ToInt64(item.MED_CODIGO_MEDICO);
                usuarios.FechaIngreso = DateTime.Now;
                usuarios.FechaCaducidad = oUltimoDiaDelMes;
                usuarios.Estado = 1;
                usuarios.TipoUsuario = 0;
                usuarios.CodDep = 23;
                usuarios.Direccion = "1";
                DataTable usu = new DataTable();
                usu = NegUsuarios.ConsultaUsuarioDep(usuarios.Identificacion);
                if (usu.Rows.Count == 0)
                {
                    DataTable ultimoCod = new DataTable();
                    resp = usuariosBLL.insertarUsuario(usuarios);
                    ultimoCod = NegUsuarios.ULtimoCodigo();
                    perfiles(Convert.ToInt32(ultimoCod.Rows[0][0].ToString()));
                    //Thread.Sleep(2000);
                }
               // Thread.Sleep(1000);
            }
            MessageBox.Show("Usuarios Medicos Generados Correctamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void perfiles(Int32 id)
        {
            DataTable perfiles = carga();

            for (int i = 0; i < perfiles.Rows.Count-1; i++)
            {
                if (NegUsuarios.CrearPerfilHis(Convert.ToInt32(perfiles.Rows[i][0].ToString()), id))
                {
                    
                }
                else
                {
                   // MessageBox.Show("No se creo el perfil", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
        }
        private DataTable carga()
        {
            DataTable PerfilesHis = new DataTable();

            DataColumn dc = new DataColumn();
            dc.ColumnName = "ID_PERFIL";
            dc.DataType = typeof(double);

            PerfilesHis.Columns.AddRange(new DataColumn[] { dc });

            PerfilesHis.Rows.Add(new object[] { 14 });
            PerfilesHis.Rows.Add(new object[] { 6 });
            PerfilesHis.Rows.Add(new object[] { 4 });
            PerfilesHis.Rows.Add(new object[] { 11 });
            PerfilesHis.Rows.Add(new object[] { 19 });
            PerfilesHis.Rows.Add(new object[] { 18 });

            return PerfilesHis;
        }

    }
}
