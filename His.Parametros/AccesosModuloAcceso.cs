using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public static  class AccesosModuloAcceso
    {
        //VARIABLES
        //Variables tipo MENU 
        private static bool moduloMantenimiento = false;
        private static bool moduloHonorarios = false;
        private static bool moduloTarifario = false;
        private static bool moduloAdmision = false;
        private static bool moduloHabitaciones = false;

        private static bool moduloEmergencias = false;
        private static bool moduloPedidos = false;
        private static bool moduloCuentaPaciente = false;

        private static bool moduloPedidosEspeciales = false;
        private static bool moduloMedicos = false;
        private static bool moduloConsultaExterna = false;
        private static bool moduloEmergencia = false;

        public static bool ModuloPedidosEspeciales
        {
            get
            {
                return moduloPedidosEspeciales;
            }

            set
            {
                moduloPedidosEspeciales = value;
            }
        }

        public static bool ModuloMedicos
        {
            get
            {
                return moduloMedicos;
            }

            set
            {
                moduloMedicos = value;
            }
        }

        public static bool ModuloConsultaExterna
        {
            get
            {
                return moduloConsultaExterna;
            }

            set
            {
                moduloConsultaExterna = value;
            }
        }

        public static bool ModuloEmergencia
        {
            get
            {
                return moduloEmergencia;
            }

            set
            {
                moduloEmergencia = value;
            }
        }

        public static bool getModuloEmergencias()
        {
            return moduloEmergencias;
        }
        public static void setModuloEmergencias(bool estado)
        {
           moduloEmergencias=estado;
        }


        public static bool getModuloPedidos()
        {
            return moduloPedidos;
        }
        public static void setModuloPedidos(bool estado)
        {
            moduloPedidos = estado;
        }



        public static bool getModuloCuentaPaciente()
        {
            return moduloCuentaPaciente;
        }
        public static void setModuloCuentaPaciente(bool estado)
        {
            moduloCuentaPaciente = estado;
        }
        


        //Retorna Acceso Modulo de Mantenimiento
        public static bool getModuloMantenimiento()
        {
            return moduloMantenimiento; 
        }
        //Ingresa Acceso Modulo de Mantenimiento
        public static void setModuloMantenimiento(bool estado)
        {
            moduloMantenimiento = estado;
        }

        //Retorna Acceso Modulo de Honorarios
        public static bool getModuloHonorarios()
        {
            return moduloHonorarios;
        }
        //Ingresa Acceso Modulo de Honorarios
        public static void setModuloHonorarios(bool estado)
        {
            moduloHonorarios = estado;
        }

        //Retorna Acceso Modulo de Tarifario
        public static bool getModuloTarifario()
        {
            return moduloTarifario;
        }
        //Ingresa Acceso Modulo de Tarifario
        public static void setModuloTarifario(bool estado)
        {
            moduloTarifario = estado;
        }

        //Retorna Acceso Modulo de Admision
        public static bool getModuloAdmision()
        {
            return moduloAdmision;
        }
        //Ingresa Acceso Modulo de Admision
        public static void setModuloAdmision(bool estado)
        {
            moduloAdmision = estado;
        }

        //Retorna Acceso Modulo de Habitaciones
        public static bool getModuloHabitaciones()
        {
            return moduloHabitaciones;
        }
        //Ingresa Acceso Modulo de Habitaciones
        public static void setModuloHabitaciones(bool estado)
        {
            moduloHabitaciones = estado;
        }


    }
}
