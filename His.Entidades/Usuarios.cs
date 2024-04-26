using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class Usuarios
    {
        private Int64 idUsuario;
        private string nombre;
        private string apellidos;
        private string identificacion;
        private string nombreusu;
        private double codusu;
        private string clave;
        private double codigo_c;
        private DateTime fechaIngreso;
        private DateTime fechaCaducidad;
        private int estado;
        private int tipoUsuario;
        private double coddep;
        private string direccion;

        public long IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Identificacion { get => identificacion; set => identificacion = value; }
        public string Nombreusu { get => nombreusu; set => nombreusu = value; }
        public double Codusu { get => codusu; set => codusu = value; }
        public string Clave { get => clave; set => clave = value; }
        public double Codigo_c { get => codigo_c; set => codigo_c = value; }
        public DateTime FechaIngreso { get => fechaIngreso; set => fechaIngreso = value; }
        public DateTime FechaCaducidad { get => fechaCaducidad; set => fechaCaducidad = value; }
        public int Estado { get => estado; set => estado = value; }
        public int TipoUsuario { get => tipoUsuario; set => tipoUsuario = value; }
        public double CodDep { get => coddep; set => coddep = value; }
        public string Direccion { get => direccion; set => direccion = value; }
    }
}
