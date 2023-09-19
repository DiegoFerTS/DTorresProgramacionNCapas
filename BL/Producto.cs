using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    ObjectParameter filasAfectadas = new ObjectParameter("FilasAfectadas", typeof(int));
                    var query = context.ProductoAdd(producto.Nombre, producto.PrecioUnitario, producto.Stock, producto.Proveedor.IdProveedor,
                                                    producto.Departamento.IdDepartamento, producto.Descripcion, producto.Imagen, filasAfectadas);

                    if (query > 0)
                    {
                        resultado.Correct = true;
                    } else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo registrar el producto: " + producto.Nombre;
                    }
                }
            } catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessage = ex.InnerException.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }


        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    ObjectParameter filasAfectadas = new ObjectParameter("FilasAfectadas", typeof(int));
                    var query = context.ProductoUpdate(producto.IdProducto, producto.Nombre, producto.PrecioUnitario, producto.Stock, producto.Proveedor.IdProveedor,
                                                    producto.Departamento.IdDepartamento, producto.Descripcion, producto.Imagen, filasAfectadas);

                    if (query > 0)
                    {
                        resultado.Correct = true;
                    } else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo actualizar los datos del producto: " + producto.Nombre;
                    }
                }
            } catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessage = ex.InnerException.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }


        public static ML.Result Delete(int idProducto)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    ObjectParameter filasAfectadas = new ObjectParameter("FilasAfectadas", typeof(int));
                    var query = context.ProductoDelete(idProducto, filasAfectadas);

                    if (query > 0)
                    {
                        resultado.Correct = true;
                    } else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo eliminar el producto con el Id: " + idProducto;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessage = ex.InnerException.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }


        //Falta crear el getAll y getById

        public static ML.Result GetAll()
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    var query = context.ProductoGetAll().ToList();

                    if (query != null)
                    {
                        resultado.Objects = new List<object>();
                        foreach (var productoResult in query)
                        {
                            ML.Producto producto = new ML.Producto();

                            producto.IdProducto = productoResult.IdProducto;
                            producto.Nombre = productoResult.Nombre;
                            producto.PrecioUnitario = productoResult.PrecioUnitario;
                            producto.Stock = productoResult.Stock;
                            producto.Proveedor = new ML.Proveedor();
                            producto.Proveedor.IdProveedor = productoResult.IdProveedor;
                            producto.Proveedor.Nombre = productoResult.NombreProveedor;
                            producto.Proveedor.Telefono = productoResult.TelefonoProveedor;
                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.IdDepartamento = productoResult.IdDepartamento;
                            producto.Departamento.Nombre = productoResult.NombreDepartamento;
                            producto.Departamento.Area = new ML.Area();
                            producto.Departamento.Area.IdArea = productoResult.IdArea;
                            producto.Departamento.Area.Nombre = productoResult.NombreArea;
                            producto.Descripcion = productoResult.Descripcion;
                            producto.Imagen = productoResult.Imagen;

                            resultado.Objects.Add(producto);
                        }

                        resultado.Correct = true;
                    } else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se encontraron datos";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }



        public static ML.Result GetById(int idProducto)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    var query = context.ProductoGetById(idProducto).FirstOrDefault();

                    if (query != null)
                    {
                    
                        ML.Producto producto = new ML.Producto();

                        producto.IdProducto = query.IdProducto;
                        producto.Nombre = query.Nombre;
                        producto.PrecioUnitario = query.PrecioUnitario;
                        producto.Stock = query.Stock;
                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = query.IdProveedor;
                        producto.Proveedor.Nombre = query.NombreProveedor;
                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = query.IdDepartamento;
                        producto.Departamento.Nombre = query.NombreDepartamento;
                        producto.Departamento.Area = new ML.Area();
                        producto.Departamento.Area.IdArea = query.IdArea;
                        producto.Departamento.Area.Nombre = query.NombreArea;
                        producto.Descripcion = query.Descripcion;
                        producto.Imagen = query.Imagen;

                        resultado.Object = producto;
                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se encontraron datos";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }


        public static ML.Result GetByIdArea(int idArea)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    var query = context.ProductoGetByIdArea(idArea).ToList();

                    if (query != null)
                    {

                        resultado.Objects = new List<object>();
                        foreach (var productoResult in query)
                        {
                            ML.Producto producto = new ML.Producto();

                            producto.IdProducto = productoResult.IdProducto;
                            producto.Nombre = productoResult.Nombre;
                            producto.PrecioUnitario = productoResult.PrecioUnitario;
                            producto.Stock = productoResult.Stock;
                            producto.Proveedor = new ML.Proveedor();
                            producto.Proveedor.IdProveedor = productoResult.IdProveedor;
                            producto.Proveedor.Nombre = productoResult.NombreProveedor;
                            producto.Proveedor.Telefono = productoResult.TelefonoProveedor;
                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.IdDepartamento = productoResult.IdDepartamento;
                            producto.Departamento.Nombre = productoResult.NombreDepartamento;
                            producto.Departamento.Area = new ML.Area();
                            producto.Departamento.Area.IdArea = productoResult.IdArea;
                            producto.Departamento.Area.Nombre = productoResult.NombreArea;
                            producto.Descripcion = productoResult.Descripcion;
                            producto.Imagen = productoResult.Imagen;

                            resultado.Objects.Add(producto);
                        }

                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se encontraron datos";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }
    }
}
