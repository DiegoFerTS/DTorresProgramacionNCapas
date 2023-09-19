using Microsoft.Win32;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BL
{
    public class Usuario
    {
        // Funcion para insertar un usuario en la base de datos
        /*
        public static ML.Result Add(ML.Usuario usuario)
        {
            // Se crea un objeto en base al ML Result
            ML.Result resultado = new ML.Result();

            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "INSERT INTO Usuario VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento, @Curp, @Rfc," +
                                        "@Direccion, @CodigoPostal, @NumeroTelefonico, @CorreoElectronico)";

                    SqlCommand command = new SqlCommand(query, conexion);

                    SqlParameter[] collection = new SqlParameter[10];
                    collection[0] = new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar);
                    collection[0].Value = usuario.Nombre;
                    collection[1] = new SqlParameter("@ApellidoPaterno", System.Data.SqlDbType.VarChar);
                    collection[1].Value = usuario.ApellidoPaterno;
                    collection[2] = new SqlParameter("@ApellidoMaterno", System.Data.SqlDbType.VarChar);
                    collection[2].Value = usuario.ApellidoMaterno;
                    collection[3] = new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.Date);
                    collection[3].Value = usuario.FechaNacimiento;
                    collection[4] = new SqlParameter("@Curp", System.Data.SqlDbType.VarChar);
                    collection[4].Value = usuario.Curp;
                    collection[5] = new SqlParameter("@Rfc", System.Data.SqlDbType.VarChar);
                    collection[5].Value = usuario.Rfc;
                    collection[6] = new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar);
                    collection[6].Value = usuario.Direccion;
                    collection[7] = new SqlParameter("CodigoPostal", System.Data.SqlDbType.Int);
                    collection[7].Value = usuario.CodigoPostal;
                    collection[8] = new SqlParameter("@NumeroTelefonico", System.Data.SqlDbType.VarChar);
                    collection[8].Value = usuario.NumeroTelefonico;
                    collection[9] = new SqlParameter("@CorreoElectronico", System.Data.SqlDbType.VarChar);
                    collection[9].Value = usuario.CorreoElectronico;

                    command.Parameters.AddRange(collection);
                    command.Connection.Open();

                    int filasAfectadas = command.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        resultado.Correct = true;
                    } else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "\nEl registro no se pudo realizar.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex.Message;
                resultado.Ex = ex;
            }

            // Se retorna el objeto resultado
            return resultado;
        }
        */

        // Funcion Add utilizando STORED PROCEDURE
        /*
        public static ML.Result AddSP(ML.Usuario usuario)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "UsuarioAdd";

                    SqlCommand command = new SqlCommand(query, conexion);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[11];
                    collection[0] = new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar);
                    collection[0].Value = usuario.Nombre;
                    collection[1] = new SqlParameter("@ApellidoPaterno", System.Data.SqlDbType.VarChar);
                    collection[1].Value = usuario.ApellidoPaterno;
                    collection[2] = new SqlParameter("@ApellidoMaterno", System.Data.SqlDbType.VarChar);
                    collection[2].Value = usuario.ApellidoMaterno;
                    collection[3] = new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.Date);
                    collection[3].Value = usuario.FechaNacimiento;
                    collection[4] = new SqlParameter("@Curp", System.Data.SqlDbType.VarChar);
                    collection[4].Value = usuario.Curp;
                    collection[5] = new SqlParameter("@Rfc", System.Data.SqlDbType.VarChar);
                    collection[5].Value = usuario.Rfc;
                    collection[6] = new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar);
                    collection[6].Value = usuario.Direccion;
                    collection[7] = new SqlParameter("@CodigoPostal", System.Data.SqlDbType.Int);
                    collection[7].Value = usuario.CodigoPostal;
                    collection[8] = new SqlParameter("@NumeroTelefonico", System.Data.SqlDbType.VarChar);
                    collection[8].Value = usuario.NumeroTelefonico;
                    collection[9] = new SqlParameter("@CorreoElectronico", System.Data.SqlDbType.VarChar);
                    collection[9].Value = usuario.CorreoElectronico;
                    collection[10] = new SqlParameter("@IdRol", System.Data.SqlDbType.Int);
                    collection[10].Value = usuario.Rol.IdRol;

                    command.Parameters.AddRange(collection);
                    command.Connection.Open();

                    int filasAfectadas = command.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "\nEl registro no se pudo realizar.";
                    }

                }
            } catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }
        */

        // Funcion Add utilizando Entity Framework
        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result resultado = new ML.Result();
            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    ObjectParameter filasAfectadas = new ObjectParameter("FilasAfectadas", typeof(int));
                    var query = context.UsuarioAdd(usuario.UserName,usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email,
                                                    usuario.Password, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.FechaNacimiento,
                                                    usuario.CURP, usuario.Rol.IdRol, usuario.Imagen, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior,
                                                    usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia,filasAfectadas);
                    if (query > 0)
                    {
                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "\nEl registro no se pudo realizar.";
                    }
                }
            }
            catch (Exception ex)
            {
                // resultado.ErrorMessage = "\n" + ex.Message;
                resultado.ErrorMessage = "\n" + ex.InnerException.Message;
                resultado.Ex = ex;
            }
            return resultado;
        }
        // Funcion Add utilizando LINQ
        /*
        public static ML.Result AddLINQ(ML.Usuario usuario)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    DLEF.Usuario nuevoUsuario = new DLEF.Usuario();

                    nuevoUsuario.Nombre = usuario.Nombre;
                    nuevoUsuario.ApellidoPaterno = usuario.ApellidoPaterno;
                    nuevoUsuario.ApellidoMaterno = usuario.ApellidoMaterno;
                    nuevoUsuario.FechaNacimiento = usuario.FechaNacimiento;
                    nuevoUsuario.Curp = usuario.Curp;
                    nuevoUsuario.Rfc = usuario.Rfc;
                    nuevoUsuario.Direccion = usuario.Direccion;
                    nuevoUsuario.CodigoPostal = usuario.CodigoPostal;
                    nuevoUsuario.NumeroTelefonico = usuario.NumeroTelefonico;
                    nuevoUsuario.CorreoElectronico = usuario.CorreoElectronico;
                    nuevoUsuario.IdRol = usuario.Rol.IdRol;

                    // Me quede aqui implementando LINQ
                    context.Usuarios.Add(nuevoUsuario);
                    context.SaveChanges();
                }
                resultado.Correct = true;
            } catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex;
                resultado.Ex = ex;
            }
            return resultado;
        }
        */


        // Funcion para actualizar un usuario en la base de datos
        /*
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "UPDATE Usuario SET Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno, " +
                                        "FechaNacimiento = @FechaNacimiento, Curp = @Curp, Rfc = @Rfc, Direccion = @Direccion, CodigoPostal = @CodigoPostal, " +
                                        "NumeroTelefonico = @NumeroTelefonico, CorreoElectronico = @CorreoElectronico WHERE IdUsuario = @IdUsuario";

                    SqlCommand command = new SqlCommand(query, conexion);

                    SqlParameter[] collection = new SqlParameter[11];
                    collection[0] = new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar);
                    collection[0].Value = usuario.Nombre;
                    collection[1] = new SqlParameter("@ApellidoPaterno", System.Data.SqlDbType.VarChar);
                    collection[1].Value = usuario.ApellidoPaterno;
                    collection[2] = new SqlParameter("@ApellidoMaterno", System.Data.SqlDbType.VarChar);
                    collection[2].Value = usuario.ApellidoMaterno;
                    collection[3] = new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.Date);
                    collection[3].Value = usuario.FechaNacimiento;
                    collection[4] = new SqlParameter("@Curp", System.Data.SqlDbType.VarChar);
                    collection[4].Value = usuario.Curp;
                    collection[5] = new SqlParameter("@Rfc", System.Data.SqlDbType.VarChar);
                    collection[5].Value = usuario.Rfc;
                    collection[6] = new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar);
                    collection[6].Value = usuario.Direccion;
                    collection[7] = new SqlParameter("CodigoPostal", System.Data.SqlDbType.Int);
                    collection[7].Value = usuario.CodigoPostal;
                    collection[8] = new SqlParameter("@NumeroTelefonico", System.Data.SqlDbType.VarChar);
                    collection[8].Value = usuario.NumeroTelefonico;
                    collection[9] = new SqlParameter("@CorreoElectronico", System.Data.SqlDbType.VarChar);
                    collection[9].Value = usuario.CorreoElectronico;
                    collection[10] = new SqlParameter("@IdUsuario", System.Data.SqlDbType.Int);
                    collection[10].Value = usuario.IdUsuario;

                    command.Parameters.AddRange(collection);
                    command.Connection.Open();

                    int filasAfectadas = command.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo realizar la actualizacion de los datos del usuario con id: " + usuario.IdUsuario + ", se puede deber a que el usuario no exista.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex.Message;
                resultado.Ex = ex;
            }

            // Se retorna el objeto resultado
            return resultado;
        }
        */

        // Funcion Update utilizando STORE PROCEDURE
        /*
        public static ML.Result UpdateSP(ML.Usuario usuario)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "UsuarioUpdate";

                    SqlCommand command = new SqlCommand(query, conexion);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[12];
                    collection[0] = new SqlParameter("@IdUsuario", System.Data.SqlDbType.Int);
                    collection[0].Value = usuario.IdUsuario;
                    collection[1] = new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar);
                    collection[1].Value = usuario.Nombre;
                    collection[2] = new SqlParameter("@ApellidoPaterno", System.Data.SqlDbType.VarChar);
                    collection[2].Value = usuario.ApellidoPaterno;
                    collection[3] = new SqlParameter("@ApellidoMaterno", System.Data.SqlDbType.VarChar);
                    collection[3].Value = usuario.ApellidoMaterno;
                    collection[4] = new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.Date);
                    collection[4].Value = usuario.FechaNacimiento;
                    collection[5] = new SqlParameter("@Curp", System.Data.SqlDbType.VarChar);
                    collection[5].Value = usuario.Curp;
                    collection[6] = new SqlParameter("@Rfc", System.Data.SqlDbType.VarChar);
                    collection[6].Value = usuario.Rfc;
                    collection[7] = new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar);
                    collection[7].Value = usuario.Direccion;
                    collection[8] = new SqlParameter("CodigoPostal", System.Data.SqlDbType.Int);
                    collection[8].Value = usuario.CodigoPostal;
                    collection[9] = new SqlParameter("@NumeroTelefonico", System.Data.SqlDbType.VarChar);
                    collection[9].Value = usuario.NumeroTelefonico;
                    collection[10] = new SqlParameter("@CorreoElectronico", System.Data.SqlDbType.VarChar);
                    collection[10].Value = usuario.CorreoElectronico;
                    collection[11] = new SqlParameter("@IdRol", System.Data.SqlDbType.Int);
                    collection[11].Value = usuario.Rol.IdRol;

                    command.Parameters.AddRange(collection);
                    command.Connection.Open();

                    int filasAfectadas = command.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo realizar la actualizacion de los datos del usuario con id: " + usuario.IdUsuario + ", se puede deber a que el usuario no exista.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }
        */

        // Funcion Update utilizando ENTITY FRAMEWORK
        public static ML.Result UpdateEF(ML.Usuario usuario)
        {

            ML.Result resultado = new ML.Result();
            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    ObjectParameter filasAfectadas = new ObjectParameter("FilasAfectadas", typeof(int));
                    var query = context.UsuarioUpdate(usuario.IdUsuario, usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, 
                                                       usuario.Email, usuario.Password, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.FechaNacimiento,
                                                    usuario.CURP, usuario.Rol.IdRol, usuario.Imagen, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, 
                                                    usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia, filasAfectadas);
                    if (query > 0)
                    {
                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo realizar la actualizacion de los datos del usuario con id: " + usuario.IdUsuario + "," +
                            " se puede deber a que el usuario no exista.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex.Message;
                resultado.Ex = ex;
            }
            return resultado;
        }

        // Funcion Update utilizando LINQ
        /*
        public static ML.Result UpdateLINQ(ML.Usuario usuario)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    var query = (from tablaUsuario in context.Usuarios
                                 where tablaUsuario.IdUsuario == usuario.IdUsuario
                                 select tablaUsuario).SingleOrDefault();

                    if (query != null)
                    {
                        query.IdUsuario = usuario.IdUsuario;
                        query.Nombre = usuario.Nombre;
                        query.ApellidoPaterno = usuario.ApellidoPaterno;
                        query.ApellidoMaterno = usuario.ApellidoMaterno;
                        query.FechaNacimiento = usuario.FechaNacimiento;
                        query.Curp = usuario.Curp;
                        query.Rfc = usuario.Rfc;
                        query.Direccion = usuario.Direccion;
                        query.CodigoPostal = usuario.CodigoPostal;
                        query.NumeroTelefonico = usuario.NumeroTelefonico;
                        query.CorreoElectronico = usuario.CorreoElectronico;
                        query.IdRol = usuario.Rol.IdRol;

                        context.SaveChanges();

                        resultado.Correct = true;
                    } else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo actualizar al usuario con el id: " + usuario.IdUsuario;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex;
                resultado.Ex = ex;
            }

            return resultado;
        }

        */


        // Funcion para eliminar un usuario en la base de datos
        /*
        public static ML.Result Delete(ML.Usuario usuario)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "DELETE FROM Usuario WHERE IdUsuario = @IdUsuario";

                    SqlCommand command = new SqlCommand(query, conexion);

                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@IdUsuario", System.Data.SqlDbType.Int);
                    collection[0].Value = usuario.IdUsuario;

                    command.Parameters.AddRange(collection);
                    command.Connection.Open();

                    int filasAfectadas = command.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "\nEl usuario con id:" + usuario.IdUsuario + " no pudo ser elimnado o no esta registrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex.Message;
                resultado.Ex = ex;
            }

            // Se retorna el objeto resultado
            return resultado;
        }
        */

        // Funcion DeleteSP utilizando STORE PROCEDURE
        /*
        public static ML.Result DeleteSP(ML.Usuario usuario)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "UsuarioDelete";

                    SqlCommand command = new SqlCommand(query, conexion);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@IdUsuario", System.Data.SqlDbType.Int);
                    collection[0].Value = usuario.IdUsuario;

                    command.Parameters.AddRange(collection);
                    command.Connection.Open();

                    int filasAfectadas = command.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "\nEl usuario con id:" + usuario.IdUsuario + " no pudo ser elimnado o no esta registrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }
        */

        // Funcion DeleteEF utilizando ENTITY FRAMEWORK
        public static ML.Result DeleteEF(int? idUsuario)
        {
            ML.Result resultado = new ML.Result();
            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    ObjectParameter filasAfectadas = new ObjectParameter("FilasAfectadas", typeof(int));
                    var query = context.UsuarioDelete(idUsuario.Value, filasAfectadas);

                    if (query > 0)
                    {
                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "\nEl usuario con id:" + idUsuario + " no pudo ser elimnado o no esta registrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex.Message;
                resultado.Ex = ex;
            }
            return resultado;
        }

        // Funcion DeleteLINQ utilizando LINQ
        /*
        public static ML.Result DeleteLINQ(ML.Usuario usuario)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    var query = (from tablaUsuario in context.Usuarios
                                 where tablaUsuario.IdUsuario == usuario.IdUsuario
                                 select tablaUsuario).First();

                    if (query != null)
                    {
                        context.Usuarios.Remove(query);
                        context.SaveChanges();
                        resultado.Correct = true;
                    } else
                    {
                        resultado.ErrorMessage = "No se pudo eliminar al usuario con el id: " + usuario.IdUsuario;
                        resultado.Correct = false;
                    }
                }
            } catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex.Message;
                resultado.Ex = ex;
            }
            return resultado;
        }
        */

        // Funcion para consultar todos los usuarios de la base de datos, funcion modificada
        /*
        public static ML.Result GetAll()
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "SELECT IdUsuario, Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento, " +
                        "Curp, Rfc, Direccion, CodigoPostal, NumeroTelefonico, CorreoElectronico FROM Usuario";

                    using (SqlCommand command = new SqlCommand(query, conexion))
                    {
                        //command.Connection.Open();

                        // En este adaptaer se almasenaran los registro de acuerdo a la consulta realizada
                        // El adapter se va a encargar de abrir y cerrar la conexion automaticamente
                        SqlDataAdapter adapter = new SqlDataAdapter(command);

                        // Tabla para organizar los datos obtenidos en el adapter
                        DataTable tablaUsuarios = new DataTable();

                        // Se va a llenar la tabla con los datos
                        adapter.Fill(tablaUsuarios);

                        if (tablaUsuarios.Rows.Count > 0)
                        {
                            resultado.Objects = new List<object>();
                            Console.WriteLine("\nA continuacion se muestran todos datos de los usuarios registrados.\n");
                            foreach (DataRow row in tablaUsuarios.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();

                                usuario.IdUsuario = int.Parse(row[0].ToString());
                                usuario.Nombre = row[1].ToString();
                                usuario.ApellidoPaterno = row[2].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.FechaNacimiento = DateTime.Parse(row[4].ToString());
                                usuario.Curp = row[5].ToString();
                                usuario.Rfc = row[6].ToString();
                                usuario.Direccion = row[7].ToString();
                                usuario.CodigoPostal = int.Parse(row[8].ToString());
                                usuario.CorreoElectronico = row[9].ToString();
                                usuario.NumeroTelefonico = row[10].ToString();

                                resultado.Objects.Add(usuario);

                            }

                            resultado.Correct = true;
                        } else
                        {
                            resultado.Correct = false;
                            resultado.ErrorMessage = "Al parecer no se encotro ningun registro.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }
        */

        /*
        // Funcion para consultar todos los usuarios de la base de datos, funcion desarrollada por mi
        public static void GetAllPropio()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "SELECT * FROM Usuario";

                    using (SqlCommand command = new SqlCommand(query, conexion))
                    {
                        command.Connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("\nA continuacion se muestran todos datos de los usuarios registrados.\n");
                            while (reader.Read())
                            {
                                int idUsuario = reader.GetInt32(0);
                                string nombre = reader.GetString(1);
                                string apellidoPaterno = reader.GetString(2);
                                string apellidoMaterno = reader.GetString(3);
                                DateTime fechaNacimiento = reader.GetDateTime(4);
                                string curp = reader.GetString(5);
                                string rfc = reader.GetString(6);
                                string direccion = reader.GetString(7);
                                int codigoPostal = reader.GetInt32(8);
                                string correoElectronico = reader.GetString(9);
                                string numeroTelefonico = reader.GetString(10);

                                Console.WriteLine("IdUsuario: {0}\nNombre: {1}\nApellido paterno: {2}\nApellido materno: {3}\nFecha de nacimiento: {4}\n" +
                                    "CURP: {5}\nRFC: {6}\nDireccion: {7}\nCodigo postal: {8}\nCorreo electronico: {9}\nNumero telefonico: {10}\n\n", 
                                    idUsuario, nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento.ToString("yyyy-MM-dd"), curp, rfc, direccion, 
                                    codigoPostal, numeroTelefonico, correoElectronico);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        */
        // Funcion GetAll utilizando STORED PROCEDURE
        /*
        public static ML.Result GetAllSP()
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "UsuarioGetAll";

                    using (SqlCommand command = new SqlCommand(query, conexion))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //command.Connection.Open();

                        // En este adaptaer se almasenaran los registro de acuerdo a la consulta realizada
                        // El adapter se va a encargar de abrir y cerrar la conexion automaticamente
                        SqlDataAdapter adapter = new SqlDataAdapter(command);

                        // Tabla para organizar los datos obtenidos en el adapter
                        DataTable tablaUsuarios = new DataTable();

                        // Se va a llenar la tabla con los datos
                        adapter.Fill(tablaUsuarios);

                        if (tablaUsuarios.Rows.Count > 0)
                        {
                            resultado.Objects = new List<object>();
                            Console.WriteLine("\nA continuacion se muestran todos datos de los usuarios registrados.\n");
                            foreach (DataRow row in tablaUsuarios.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();

                                usuario.IdUsuario = int.Parse(row[0].ToString());
                                usuario.Nombre = row[1].ToString();
                                usuario.ApellidoPaterno = row[2].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.FechaNacimiento = DateTime.Parse(row[4].ToString());
                                usuario.Curp = row[5].ToString();
                                usuario.Rfc = row[6].ToString();
                                usuario.Direccion = row[7].ToString();
                                usuario.CodigoPostal = int.Parse(row[8].ToString());
                                usuario.CorreoElectronico = row[9].ToString();
                                usuario.NumeroTelefonico = row[10].ToString();

                                // me quede aqui
                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = int.Parse(row[11].ToString());

                                resultado.Objects.Add(usuario);

                            }

                            resultado.Correct = true;
                        }
                        else
                        {
                            resultado.Correct = false;
                            resultado.ErrorMessage = "Al parecer no se encotro ningun registro.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }

        */

        // Funcion GetAll utilizando ENTITY FRAMEWORK
        public static ML.Result GetAllEF(ML.Usuario usuarioConsultado)
        {
            if (usuarioConsultado.Nombre == null)
            {
                usuarioConsultado.Nombre = "";
            }

            if(usuarioConsultado.ApellidoPaterno == null)
            {
                usuarioConsultado.ApellidoPaterno = "";
            }

            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    var query = context.UsuarioGetAll(usuarioConsultado.Nombre, usuarioConsultado.ApellidoPaterno).ToList();

                    if (query != null)
                    {
                        resultado.Objects = new List<object>();

                        foreach (var registro in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.IdUsuario = registro.IdUsuario;
                            usuario.UserName = registro.UserName;
                            usuario.Nombre = registro.Nombre;
                            usuario.ApellidoPaterno = registro.ApellidoPaterno;
                            usuario.ApellidoMaterno = registro.ApellidoMaterno;
                            usuario.Email = registro.Email;
                            usuario.Password = registro.Password;
                            usuario.Sexo = registro.Sexo;
                            usuario.Telefono = registro.Telefono;
                            usuario.Celular = registro.Celular;
                            usuario.FechaNacimiento = registro.FechaNacimiento;
                            usuario.CURP = registro.CURP;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = registro.IdRol;
                            usuario.Rol.Nombre = registro.NombreRol;

                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Calle = registro.Calle;
                            usuario.Direccion.NumeroInterior = registro.NumeroInterior;
                            usuario.Direccion.NumeroExterior = registro.NumeroExterior;
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = registro.IdColonia.Value;
                            usuario.Direccion.Colonia.Nombre = registro.NombreColonia;
                            usuario.Direccion.Colonia.CodigoPostal = registro.CodigoPostal;
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = registro.IdMunicipio.Value;
                            usuario.Direccion.Colonia.Municipio.Nombre = registro.NombreMunicipio;
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = registro.IdEstado.Value;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = registro.NombreEstado;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = registro.IdPais.Value;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = registro.NombrePais;
                            usuario.Imagen = registro.Imagen;
                            usuario.Status = registro.Status;

                            resultado.Objects.Add(usuario);
                        }

                        resultado.Correct = true;

                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "Al parecer no se encontro ningun registro.";
                    }

                }
            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex.Message;
                resultado.Ex = ex;
            }
            return resultado;
        }


        // Funcion GetAll utilizando LINQ
        /*
        public static ML.Result GetAllLINQ()
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    var query = (from tablaUsuarios in context.Usuarios
                                 join tablaRoles in context.Rols on tablaUsuarios.IdRol equals tablaRoles.IdRol
                                 select new
                                 {
                                     IdUsuario = tablaUsuarios.IdUsuario,
                                     Nombre = tablaUsuarios.Nombre,
                                     ApellidoPaterno = tablaUsuarios.ApellidoPaterno,
                                     ApellidoMaterno = tablaUsuarios.ApellidoMaterno,
                                     FechaNacimiento = tablaUsuarios.FechaNacimiento,
                                     Curp = tablaUsuarios.Curp,
                                     Rfc = tablaUsuarios.Rfc,
                                     Direccion = tablaUsuarios.Direccion,
                                     CodigoPostal = tablaUsuarios.CodigoPostal,
                                     NumeroTelefonico = tablaUsuarios.NumeroTelefonico,
                                     CorreoElectronico = tablaUsuarios.CorreoElectronico,
                                     IdRol = tablaUsuarios.IdRol,
                                     NombreRol = tablaRoles.Nombre
                                 });

                    resultado.Objects = new List<object>();

                    if (query != null && query.ToList().Count > 0)
                    {
                        foreach (var datos in query)
                        {
                            ML.Usuario usuarioResult = new ML.Usuario();
                            usuarioResult.IdUsuario = datos.IdUsuario;
                            usuarioResult.Nombre = datos.Nombre;
                            usuarioResult.ApellidoPaterno = datos.ApellidoPaterno;
                            usuarioResult.ApellidoMaterno = datos.ApellidoMaterno;
                            usuarioResult.FechaNacimiento = datos.FechaNacimiento;
                            usuarioResult.Curp = datos.Curp;
                            usuarioResult.Rfc = datos.Rfc;
                            usuarioResult.Direccion = datos.Direccion;
                            usuarioResult.CodigoPostal = datos.CodigoPostal;
                            usuarioResult.NumeroTelefonico = datos.NumeroTelefonico;
                            usuarioResult.Rol = new ML.Rol();
                            usuarioResult.Rol.IdRol = datos.IdRol;
                            usuarioResult.Rol.Nombre = datos.NombreRol;

                            resultado.Objects.Add(usuarioResult);
                        }

                        resultado.Correct = true;
                    } else
                    {
                        resultado.ErrorMessage = "No se encontro ningun dato.";
                        resultado.Correct = false;
                    }
                }

            } catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }
        */


        // Funcion GetById normal
        /*
        public static ML.Result GetById(ML.Usuario usuario)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "SELECT IdUsuario, Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento, " +
                       "Curp, Rfc, Direccion, CodigoPostal, NumeroTelefonico, CorreoElectronico FROM Usuario WHERE IdUsuario = @IdUsuario";

                    using (SqlCommand command = new SqlCommand(query, conexion))
                    {
                        SqlParameter[] collections = new SqlParameter[1];
                        collections[0] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                        collections[0].Value = usuario.IdUsuario;

                        command.Parameters.AddRange(collections);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);

                        DataTable tablaUsuario = new DataTable();

                        adapter.Fill(tablaUsuario);

                        if (tablaUsuario.Rows.Count > 0)
                        {
                            DataRow row = tablaUsuario.Rows[0];

                            ML.Usuario usuarioResult = new ML.Usuario();

                            usuarioResult.IdUsuario = int.Parse(row[0].ToString());
                            usuarioResult.Nombre = row[1].ToString();
                            usuarioResult.ApellidoPaterno = row[2].ToString();
                            usuarioResult.ApellidoMaterno = row[3].ToString();
                            usuarioResult.FechaNacimiento = DateTime.Parse(row[4].ToString());
                            usuarioResult.Curp = row[5].ToString();
                            usuarioResult.Rfc = row[6].ToString();
                            usuarioResult.Direccion = row[7].ToString();
                            usuarioResult.CodigoPostal = int.Parse(row[8].ToString());
                            usuarioResult.CorreoElectronico = row[9].ToString();
                            usuarioResult.NumeroTelefonico = row[10].ToString();

                            resultado.Object = usuarioResult;
                            resultado.Correct = true;
                        } else
                        {
                            resultado.Correct = false;
                            resultado.ErrorMessage = "\nNo se encontraron datos del usuario con el id: " + usuario.IdUsuario + ".";
                        }
                    }
                }

            } catch (Exception ex) {
                resultado.ErrorMessage = "\n" + ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }
        */

        /*
        // Funcion para consultar usuarios por id
        public static void GetByIdPropio(ML.Usuario usuario)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "SELECT * FROM Usuario WHERE IdUsuario = @IdUsuario";

                    using (SqlCommand command = new SqlCommand(query, conexion))
                    {
                        command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                        command.Connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine("\nA continuacion se muestran los datos del usuario con Id: "+usuario.IdUsuario+".\n");
                                string nombre = reader.GetString(1);
                                string apellidoPaterno = reader.GetString(2);
                                string apellidoMaterno = reader.GetString(3);
                                DateTime fechaNacimiento = reader.GetDateTime(4);
                                string curp = reader.GetString(5);
                                string rfc = reader.GetString(6);
                                string direccion = reader.GetString(7);
                                int codigoPostal = reader.GetInt32(8);
                                string correoElectronico = reader.GetString(9);
                                string numeroTelefonico = reader.GetString(10);

                                Console.WriteLine("Nombre: {0}\nApellido paterno: {1}\nApellido materno: {2}\nFecha de nacimiento: {3}\n" +
                                    "CURP: {4}\nRFC: {5}\nDireccion: {6}\nCodigo postal: {7}\nCorreo electronico: {8}\nNumero telefonico: {9}\n\n",
                                    nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento.ToString("yyyy-MM-dd"), curp, rfc, direccion,
                                    codigoPostal, numeroTelefonico, correoElectronico);
                            } else
                            {
                                Console.WriteLine("\nAl parecer no se encontraron datos de algun usuario con este Id.");
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        */
        // Funcion GetById utilizando STORED PROCEDURE
        /*
        public static ML.Result GetByIdSP(ML.Usuario usuario)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "UsuarioGetById";

                    using (SqlCommand command = new SqlCommand(query, conexion))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collections = new SqlParameter[1];
                        collections[0] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                        collections[0].Value = usuario.IdUsuario;

                        command.Parameters.AddRange(collections);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);

                        DataTable tablaUsuario = new DataTable();

                        adapter.Fill(tablaUsuario);

                        if (tablaUsuario.Rows.Count > 0)
                        {
                            DataRow row = tablaUsuario.Rows[0];

                            ML.Usuario usuarioResult = new ML.Usuario();

                            usuarioResult.IdUsuario = int.Parse(row[0].ToString());
                            usuarioResult.Nombre = row[1].ToString();
                            usuarioResult.ApellidoPaterno = row[2].ToString();
                            usuarioResult.ApellidoMaterno = row[3].ToString();
                            usuarioResult.FechaNacimiento = DateTime.Parse(row[4].ToString());
                            usuarioResult.Curp = row[5].ToString();
                            usuarioResult.Rfc = row[6].ToString();
                            usuarioResult.Direccion = row[7].ToString();
                            usuarioResult.CodigoPostal = int.Parse(row[8].ToString());
                            usuarioResult.CorreoElectronico = row[9].ToString();
                            usuarioResult.NumeroTelefonico = row[10].ToString();
                            usuarioResult.Rol = new ML.Rol();
                            usuarioResult.Rol.IdRol = int.Parse(row[11].ToString());

                            resultado.Object = usuarioResult;
                            resultado.Correct = true;
                        }
                        else
                        {
                            resultado.Correct = false;
                            resultado.ErrorMessage = "\nNo se encontraron datos del usuario con el id: " + usuario.IdUsuario + ".";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }
        */

        // Funcion GetById utilizando ENTITY FRAMEWORK
        public static ML.Result GetByIdEF(int idUsuario)
        {
            ML.Result resultado = new ML.Result();
            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {

                    var query = context.UsuarioGetById(idUsuario).FirstOrDefault();


                    if (query != null)
                    {

                        ML.Usuario usuarioResult = new ML.Usuario();

                        usuarioResult.IdUsuario = query.IdUsuario;
                        usuarioResult.UserName = query.UserName;
                        usuarioResult.Nombre = query.Nombre;
                        usuarioResult.ApellidoPaterno = query.ApellidoPaterno;
                        usuarioResult.ApellidoMaterno = query.ApellidoMaterno;
                        usuarioResult.Email = query.Email;
                        usuarioResult.Password = query.Password;
                        usuarioResult.Sexo = query.Sexo;
                        usuarioResult.Telefono = query.Telefono;
                        usuarioResult.Celular = query.Celular;
                        usuarioResult.FechaNacimiento = query.FechaNacimiento;
                        usuarioResult.CURP = query.CURP;
                        usuarioResult.Rol = new ML.Rol();
                        usuarioResult.Rol.IdRol = query.IdRol;
                        usuarioResult.Rol.Nombre = query.NombreRol;

                        usuarioResult.Direccion = new ML.Direccion();
                        usuarioResult.Direccion.Calle = query.Calle;
                        usuarioResult.Direccion.NumeroInterior = query.NumeroInterior;
                        usuarioResult.Direccion.NumeroExterior = query.NumeroExterior;
                        usuarioResult.Direccion.Colonia = new ML.Colonia();
                        usuarioResult.Direccion.Colonia.IdColonia = query.IdColonia.Value;
                        usuarioResult.Direccion.Colonia.Nombre = query.NombreColonia;
                        usuarioResult.Direccion.Colonia.CodigoPostal = query.CodigoPostal;
                        usuarioResult.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuarioResult.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio.Value;
                        usuarioResult.Direccion.Colonia.Municipio.Nombre = query.NombreMunicipio;
                        usuarioResult.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuarioResult.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado.Value;
                        usuarioResult.Direccion.Colonia.Municipio.Estado.Nombre = query.NombreEstado;
                        usuarioResult.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuarioResult.Direccion.Colonia.Municipio.Estado.Pais.IdPais = query.IdPais.Value;
                        usuarioResult.Direccion.Colonia.Municipio.Estado.Pais.Nombre = query.NombrePais;

                        usuarioResult.Imagen = query.Imagen;
                        usuarioResult.Status = query.Status;

                        resultado.Object = usuarioResult;
                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "\nNo se encontraron datos del usuario con el id: " + idUsuario + ".";
                    }   
                }
            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex.Message;
                resultado.Ex = ex;
            }
            return resultado;
        }


        // Funcion GetById utilizando LINQ
        /*
        public static ML.Result GetByIdLINQ(ML.Usuario usuario)
        {
            ML.Result resultado = new ML.Result();
            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    var query = (from tablaUsuarios in context.Usuarios
                                 join tablaRoles in context.Rols on tablaUsuarios.IdRol equals tablaRoles.IdRol
                                 where tablaUsuarios.IdUsuario == usuario.IdUsuario
                                 select new
                                 {
                                     IdUsuario = tablaUsuarios.IdUsuario,
                                     Nombre = tablaUsuarios.Nombre,
                                     ApellidoPaterno = tablaUsuarios.ApellidoPaterno,
                                     ApellidoMaterno = tablaUsuarios.ApellidoMaterno,
                                     FechaNacimiento = tablaUsuarios.FechaNacimiento,
                                     Curp = tablaUsuarios.Curp,
                                     Rfc = tablaUsuarios.Rfc,
                                     Direccion = tablaUsuarios.Direccion,
                                     CodigoPostal = tablaUsuarios.CodigoPostal,
                                     NumeroTelefonico = tablaUsuarios.NumeroTelefonico,
                                     CorreoElectronico = tablaUsuarios.CorreoElectronico,
                                     IdRol = tablaUsuarios.IdRol,
                                     NombreRol = tablaRoles.Nombre
                                 });

                    if (query != null && query.ToList().Count > 0)
                    {
                        ML.Usuario usuarioResult = new ML.Usuario();

                        var queryDatos = query.ToList().Single();

                        usuarioResult.IdUsuario = queryDatos.IdUsuario;
                        usuarioResult.Nombre = queryDatos.Nombre;
                        usuarioResult.ApellidoPaterno = queryDatos.ApellidoPaterno;
                        usuarioResult.ApellidoMaterno = queryDatos.ApellidoMaterno;
                        usuarioResult.FechaNacimiento = queryDatos.FechaNacimiento;
                        usuarioResult.Curp = queryDatos.Curp;
                        usuarioResult.Rfc = queryDatos.Rfc;
                        usuarioResult.Direccion = queryDatos.Direccion;
                        usuarioResult.CodigoPostal = queryDatos.CodigoPostal;
                        usuarioResult.NumeroTelefonico = queryDatos.NumeroTelefonico;
                        usuarioResult.Rol = new ML.Rol();
                        usuarioResult.Rol.IdRol = queryDatos.IdRol;
                        usuarioResult.Rol.Nombre = queryDatos.NombreRol;

                        resultado.Object = usuarioResult;
                        resultado.Correct = true;

                    } else
                    {
                        resultado.ErrorMessage = "\nNo se encontro el usuario con el id: " + usuario.IdUsuario;
                        resultado.Correct = false;
                    }
                }
            } catch (Exception ex)
            {
                resultado.ErrorMessage = ex.Message;
                resultado.Ex = ex;
            }
            return resultado;
        }
        */


        public static ML.Result ChangeStatus(int idUsuario, bool status)
        {
            ML.Result resultado = new ML.Result();

            using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
            {
                ObjectParameter filasAfectadas = new ObjectParameter("FilasAfectadas", typeof(int));
                var query = context.UsuarioChangeStatus(idUsuario, status, filasAfectadas);

                if (query > 0)
                {
                    resultado.Correct = true;
                } else
                {
                    resultado.Correct = false;
                }
            }
            return resultado;
        }



        public static ML.Result Login(string userName, string password)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    ML.Usuario usuario = new ML.Usuario();

                    var query = context.LoginGetByUsername(userName, password).FirstOrDefault();

                    if (query != null)
                    {
                        resultado.Correct = true;
                    } else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo iniciar sesion, revisa que tu username y contraseña sean correctos.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessage = ex.Message;
                resultado.Ex= ex;
            }

            return resultado;
        }






        // Lectura y validacion de datos de archivos excel xlsx
        public static ML.Result LeerExcel(string connectionString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableUsuario = new DataTable();

                        da.Fill(tableUsuario);

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableUsuario.Rows)
                            {

                                bool filaVacia = false;
                                int contador = 0;
                
                                foreach (var celdas in row.ItemArray)
                                {
                                    if (string.IsNullOrWhiteSpace(celdas.ToString()))
                                    {
                                        contador = contador + 1;                                        
                                    }
                                    if (contador == row.ItemArray.Length)
                                    {
                                        filaVacia = true;
                                        break;
                                    }
                                }

                                if (filaVacia)
                                {
                                    break;
                                }

                                if (!filaVacia)
                                {
                                    ML.Usuario usuario = new ML.Usuario();
                                    usuario.UserName = row[0].ToString();
                                    usuario.Nombre = row[1].ToString();
                                    usuario.ApellidoPaterno = row[2].ToString();
                                    usuario.ApellidoMaterno = row[3].ToString();
                                    usuario.Email = row[4].ToString();
                                    usuario.Password = row[5].ToString();
                                    usuario.Sexo = row[6].ToString();
                                    usuario.Telefono = row[7].ToString();
                                    usuario.Celular = row[8].ToString();
                                    if (row[9].ToString() != "")
                                    {
                                        usuario.FechaNacimiento = DateTime.Parse(row[9].ToString());
                                    }
                                    usuario.CURP = row[10].ToString();
                                    usuario.Rol = new ML.Rol();
                                    if (row[11].ToString() != "")
                                    {
                                        usuario.Rol.IdRol = int.Parse(row[11].ToString());
                                    }
                                    if (row[12].ToString() != "")
                                    {
                                        usuario.Status = (1 == int.Parse(row[12].ToString()));
                                    }
                                    usuario.Direccion = new ML.Direccion();
                                    usuario.Direccion.Calle = row[13].ToString();
                                    usuario.Direccion.NumeroInterior = row[14].ToString();
                                    usuario.Direccion.NumeroExterior = row[15].ToString();
                                    usuario.Direccion.Colonia = new ML.Colonia();
                                    if (row[16].ToString() != "")
                                    {
                                        usuario.Direccion.Colonia.IdColonia = int.Parse(row[16].ToString());
                                    }

                                    result.Objects.Add(usuario);
                                }
                                                               
                                                                   
                            }
                            result.Correct = true;

                        }

                        result.Object = tableUsuario;

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }




        public static ML.Result ValidarExcel(List<object> usuarios)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>(); //almacena los registros incompletos
                int i = 1; //guarda el numero de la fila
                foreach (ML.Usuario usuario in usuarios)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    if (usuario.UserName == "")
                    {
                        error.Mensaje += "* Ingresar el UserName\n";
                    }
                    if (usuario.Nombre == "")
                    {
                        error.Mensaje += "* Ingresar el nombre\n";
                    }
                    if (usuario.ApellidoMaterno == "")
                    {
                        error.Mensaje += "* Ingresar el Apellido materno\n";
                    }
                    if (usuario.ApellidoPaterno == "")
                    {
                        error.Mensaje += "* Ingresar el Apellido paterno\n";
                    }
                    if (usuario.Email == "")
                    {
                        error.Mensaje += "* Ingresar el Email\n";
                    }
                    if (usuario.Password == "")
                    {
                        error.Mensaje += "* Ingresar el password\n";
                    }
                    if (usuario.Sexo == "")
                    {
                        error.Mensaje += "* Ingresar el Sexo\n";
                    }
                    if (usuario.Telefono == "")
                    {
                        error.Mensaje += "* Ingresar el numero de telefono\n";
                    }
                    if (usuario.Celular == "")
                    {
                        error.Mensaje += "* Ingresar el numero de celular\n";
                    }
                    if (usuario.FechaNacimiento.ToString() == "")
                    {
                        error.Mensaje += "* Ingresar la fecha de nacimiento\n";
                    }
                    if (usuario.CURP == "")
                    {
                        error.Mensaje += "* Ingresar el CURP\n";
                    }
                    if (usuario.Rol.IdRol == 0)
                    {
                        error.Mensaje += "* Ingresar el id de rol\n";
                    }
                    if (usuario.Direccion.Calle == "")
                    {
                        error.Mensaje += "* Ingresar la calle\n";
                    }
                    if (usuario.Direccion.NumeroInterior == "")
                    {
                        error.Mensaje += "* Ingresar el numero interior\n";
                    }
                    if (usuario.Direccion.NumeroExterior == "")
                    {
                        error.Mensaje += "* Ingresar el numero exterior\n";
                    }
                    if (usuario.Direccion.Colonia.IdColonia == 0)
                    {
                        error.Mensaje += "* Ingresar elid de colonia\n";
                    }


                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }


                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }
    }
}
