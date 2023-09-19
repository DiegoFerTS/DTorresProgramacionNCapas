using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    internal class Usuario
    {
        public static void Menu()
        {
            Console.WriteLine("**** CRUD A SQL SERVER MANAGMENT STUDIO CON C# ****\n");
            Console.WriteLine("Ingresa el numero de la opcion correspondiente:");
            Console.WriteLine("1.- Insertar\n2.- Actualizar\n3.- Eliminar\n4.- Consultar\n5.- Consultar por Id\n");

            try
            {
                int opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Add();
                        break;
                    case 2:
                        Update();
                        break;
                    case 3:
                        Delete();
                        break;
                    case 4:
                        GetAll();
                        break;
                    case 5:
                        GetById();
                        break;
                    default:
                        Console.WriteLine("No se ingreso una opcion disponible del menu.");
                        break;
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }



        // Formulario para registrar usuarios
        public static void Add()
        {
            // Se llaman a las propiedades de la clase ML para la asignacion de valores
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("\nIngresa tu nombre de usuario:");
            usuario.UserName = Console.ReadLine();
            Console.WriteLine("\nIngresa tu nombre:");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("\nIngresa tu apellido paterno:");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("\nIngresa tu apellido materno:");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("\nIngresa tu correo electronico:");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("\nIngresa tu contraseña:");
            usuario.Password = Console.ReadLine();
            Console.WriteLine("\nIngresa tu sexo:\nH = Para hombre\nM = Para mujer\n");
            usuario.Sexo = Console.ReadLine();
            Console.WriteLine("\nIngresa tu numero de telefono:");
            usuario.Telefono = Console.ReadLine();
            Console.WriteLine("\nIngresa tu numero de celular (opcional):");
            usuario.Celular = Console.ReadLine();
            Console.WriteLine("\nIngresa tu fecha de nacimiento:");
            usuario.FechaNacimiento = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("\nIngresa tu CURP:");
            usuario.CURP = Console.ReadLine();
            Console.WriteLine("\nIngresa tu numero de Id de rol:");
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = int.Parse(Console.ReadLine());

            //ML.Result resultado = BL.Usuario.Add(usuario);
            //ML.Result resultado = BL.Usuario.AddSP(usuario);
            ML.Result resultado = BL.Usuario.AddEF(usuario);
            //ML.Result resultado = BL.Usuario.AddLINQ(usuario);
            if (resultado.Correct == true)
            {
                Console.WriteLine("\nEl registro se ha realizado exitosamente.");
            } else
            {
                Console.WriteLine(resultado.ErrorMessage);
            }
        }

        // Formulario para actualizar usuarios usuarios
        public static void Update()
        {
            // Se llaman a las propiedades de la clase ML para la asignacion de valores
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("\nIngresa tu id de usuario:");
            usuario.IdUsuario = int.Parse(Console.ReadLine());
            Console.WriteLine("\nIngresa tu nuevo nombre de usuario:");
            usuario.UserName = Console.ReadLine();
            Console.WriteLine("\nIngresa tu nuevo nombre:");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("\nIngresa tu nuevo apellido paterno:");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("\nIngresa tu nuevo apellido materno:");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("\nIngresa tu nuevo correo electronico:");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("\nIngresa tu nuevo contraseña:");
            usuario.Password = Console.ReadLine();
            Console.WriteLine("\nIngresa tu nuevo sexo:\nH = Para hombre\nM = Para mujer\n");
            usuario.Sexo = Console.ReadLine();
            Console.WriteLine("\nIngresa tu nuevo numero de telefono:");
            usuario.Telefono = Console.ReadLine();
            Console.WriteLine("\nIngresa tu nuevo numero de celular (opcional):");
            usuario.Celular = Console.ReadLine();
            Console.WriteLine("\nIngresa tu nueva fecha de nacimiento:");
            usuario.FechaNacimiento = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("\nIngresa tu nuevo CURP:");
            usuario.CURP = Console.ReadLine();
            Console.WriteLine("\nIngresa tu nuevo numero de Id de rol:");
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = int.Parse(Console.ReadLine());


            //ML.Result resultado = BL.Usuario.Update(usuario);
            //ML.Result resultado = BL.Usuario.UpdateSP(usuario);
            ML.Result resultado = BL.Usuario.UpdateEF(usuario);
            //ML.Result resultado = BL.Usuario.UpdateLINQ(usuario); dale click derecho al pl mvc porfa

            if (resultado.Correct == true)
            {
                Console.WriteLine("\nSe ha actualizado correctamente al usuario con id: " + usuario.IdUsuario);
            }
            else
            {
                Console.WriteLine(resultado.ErrorMessage);
            }
        }

        // Formulario para eliminar usuarios
        public static void Delete()
        {
            // Se llaman a las propiedades de la clase ML para la asignacion de valores
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("\nIngresa tu id de usuario:");
            usuario.IdUsuario = int.Parse(Console.ReadLine());

            //ML.Result resultado = BL.Usuario.DeleteSP(usuario);
            ML.Result resultado = BL.Usuario.DeleteEF(usuario.IdUsuario);//arregla este y prueba porfa
            //ML.Result resultado = BL.Usuario.DeleteLINQ(usuario);

            if (resultado.Correct)
            {
                Console.WriteLine("\nEl usuario con el id: " + usuario.IdUsuario + " ha sido eliminado.");
            } else
            {
                Console.WriteLine(resultado.ErrorMessage);
            }
        }

        public static void GetAll()
        {
            ML.Result resultado = new ML.Result(); 
            //resultado = BL.Usuario.GetAll();
            //resultado = BL.Usuario.GetAllSP();
            resultado = BL.Usuario.GetAllEF();
            //resultado = BL.Usuario.GetAllLINQ();

            List<object> objectsResult = resultado.Objects;
            foreach (ML.Usuario usuario in objectsResult)
            {
                Console.WriteLine("\n********************");
                Console.WriteLine("Id usuario: " + usuario.IdUsuario);
                Console.WriteLine("Nombre de usuario: " + usuario.UserName);
                Console.WriteLine("Nombre(s): " + usuario.Nombre);
                Console.WriteLine("Apellido paterno: " + usuario.ApellidoPaterno);
                Console.WriteLine("Apellido materno: " + usuario.ApellidoMaterno);
                Console.WriteLine("Correo electronico: " + usuario.Email);
                Console.WriteLine("Contraseña: " + usuario.Password);
                Console.WriteLine("Sexo: " + usuario.Sexo);
                Console.WriteLine("Numero de telefono: " + usuario.Telefono);
                Console.WriteLine("Numero de celular: " + usuario.Celular);
                Console.WriteLine("Fecha de nacimiento: " + usuario.FechaNacimiento.ToString("yyyy-MM-dd"));
                Console.WriteLine("CURP: " + usuario.CURP);
                Console.WriteLine("Id rol: " + usuario.Rol.IdRol);
                Console.WriteLine("Nombre rol: " + usuario.Rol.Nombre);
                Console.WriteLine("********************\n");
            }

        }

        /*
        public static void GetAllPropio()
        {
            // Se llaman al metodo GetAll de BL para mostrar todos los datos registrados en la tabla Usuario
            BL.Usuario.GetAll();
        }
        */

        public static void GetById()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("\nIngresa el id del usuario del que requieres consultar sus datos:");
            usuario.IdUsuario = int.Parse(Console.ReadLine());

            ML.Result resultado = new ML.Result();
            //resultado = BL.Usuario.GetById(usuario);
            //resultado = BL.Usuario.GetByIdSP(usuario);
            resultado = BL.Usuario.GetByIdEF(usuario);
            //resultado = BL.Usuario.GetByIdLINQ(usuario);

            if (resultado.Correct)
            {
                usuario = (ML.Usuario)resultado.Object;
                Console.WriteLine("\n********************");
                Console.WriteLine("Id usuario: " + usuario.IdUsuario);
                Console.WriteLine("Nombre de usuario: " + usuario.UserName);
                Console.WriteLine("Nombre(s): " + usuario.Nombre);
                Console.WriteLine("Apellido paterno: " + usuario.ApellidoPaterno);
                Console.WriteLine("Apellido materno: " + usuario.ApellidoMaterno);
                Console.WriteLine("Correo electronico: " + usuario.Email);
                Console.WriteLine("Contraseña: " + usuario.Password);
                Console.WriteLine("Sexo: " + usuario.Sexo);
                Console.WriteLine("Numero de telefono: " + usuario.Telefono);
                Console.WriteLine("Numero de celular: " + usuario.Celular);
                Console.WriteLine("Fecha de nacimiento: " + usuario.FechaNacimiento.ToString("yyyy-MM-dd"));
                Console.WriteLine("CURP: " + usuario.CURP);
                Console.WriteLine("Id rol: " + usuario.Rol.IdRol);
                Console.WriteLine("Nombre rol: " + usuario.Rol.Nombre);
                Console.WriteLine("********************\n");
            } else
            {
                Console.WriteLine(resultado.ErrorMessage);
            }

        }




    }
}
