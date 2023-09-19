using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Conexion
    {
        // Traemos la cadena de conexion de App.config de PL
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DTorresProgramacionNCapas"].ConnectionString.ToString();
        }
    }

}
