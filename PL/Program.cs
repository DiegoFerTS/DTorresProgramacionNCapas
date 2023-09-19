using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Se llama a la funcion menu de la clase usuario de PL
            PL.Usuario.Menu();
            Console.ReadKey();
        }
    }
}
