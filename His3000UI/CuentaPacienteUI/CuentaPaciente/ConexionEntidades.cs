using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.EntityClient;
using Core.Datos;

namespace CuentaPaciente
{
   public  class ConexionEntidades
   {
       public static EntityConnection ConexionEDM
       {
           get
           {
               return BaseContextoDatos.ConexionEDM();
           }

       }
    }
}
